using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FormEngine.Interfaces;
using FormEngine.Services;

namespace FormEngine;

public interface IMetaFieldGenerator
{
    IEnumerable<MetaField> GetMetaFields(PropertyInfo info, FormGenerationContext context);
}

public interface IFieldConverter
{
    Task<Field> ConvertToField(MetaField meta, FormGenerationContext context);
}

public class DefaultMetaFieldGeneration : IMetaFieldGenerator
{
    public IEnumerable<MetaField> GetMetaFields(PropertyInfo propInfo, FormGenerationContext context)
    {
        var extractedProcessors = context.AttributeExtractor.GetAttributeServices<IFieldProcessor>(propInfo);
        return new[]{
                new MetaField(){
                    PropInfo = propInfo,
                    Processors = extractedProcessors.Concat(context.Processors)
                }
            };
    }
}

public class DefaultFieldConversion : IFieldConverter
{
    public async Task<Field> ConvertToField(MetaField meta, FormGenerationContext context)
    {
        var myField = new Field()
        {
            Name = meta.PropInfo.Name,
            FieldType = meta.PropInfo.PropertyType.Name
        };

        var serviceContext = context.ServiceResolver.CreateContext();
        serviceContext.AddType<Field>(myField);

        var fieldContext = new FieldContext()
        {
            PropInfo = meta.PropInfo,
            CurrentField = myField,
            ServiceResolver = serviceContext
        };

        var processingTasks = meta.Processors.Select(x => x.ProcessFieldAsync(fieldContext));

        await Task.WhenAll(processingTasks);

        return myField;
    }
}

public record ResolverResult(bool Success, object Value);

public interface IServiceResolver
{
    IServiceResolver CreateContext();
    void AddType<T>(T instance);
    ResolverResult ResolveParameter(ParameterInfo param);
    ResolverResult ResolveService(Type type);
}

public static class ServiceResolverExtentions
{
    public static ResolverResult ResolveService<T>(this IServiceResolver resolver)
    {
        return resolver.ResolveService(typeof(T));
    }
}

public class DefaultServiceResolution : IServiceResolver
{
    public Dictionary<Type, object> RegisteredServices { get; set; }

    public DefaultServiceResolution()
    {
        this.RegisteredServices = new Dictionary<Type, object>();
    }

    public void AddType<T>(T instance)
    {
        RegisteredServices.Add(typeof(T), instance);
    }

    public IServiceResolver CreateContext()
    {
        return new DefaultServiceResolution()
        {
            RegisteredServices = this.RegisteredServices.ToDictionary(x => x.Key, x => x.Value)
        };
    }

    public ResolverResult ResolveParameter(ParameterInfo param)
    {
        throw new NotImplementedException();
    }

    public ResolverResult ResolveService(Type type)
    {
        var typeEntry = RegisteredServices.GetValueOrDefault(type);
        return typeEntry is null ? new ResolverResult(false, null) : new ResolverResult(true, typeEntry);
    }
}


public class FormGenerationContext
{
    public FormGenerationContext() => this.Processors = Enumerable.Empty<IFieldProcessor>();
    public IMetaFieldGenerator MetaGenerator { get; set; }
    public IFieldConverter FieldConverter { get; set; }
    public IEnumerable<IFieldProcessor> Processors { get; set; }
    public IAttributeService AttributeExtractor { get; set; }
    public IServiceResolver ServiceResolver { get; set; }
    public FormGenerationContext ParentContext { get; set; }
}

public static class FormGenerationExtensions
{
    public static T GetAttributeByInterface<T>(this MemberInfo myType) where T : class
    {
        var matchedAttribute = myType.GetCustomAttributes(true).FirstOrDefault(x => x is T);
        return matchedAttribute as T;
    }

    public static IEnumerable<T> GetAttributesByInterface<T>(this MemberInfo myType) where T : class
    {
        return myType.GetCustomAttributes(true).Where(x => x is T).Select(x => x as T);
    }

    public static FormGenerationContext Clone(this FormGenerationContext context)
    {
        return new FormGenerationContext()
        {
            ServiceResolver = context.ServiceResolver,
            AttributeExtractor = context.AttributeExtractor,
            MetaGenerator = context.MetaGenerator,
            FieldConverter = context.FieldConverter,
            Processors = context.Processors.ToArray(),
            ParentContext = context.ParentContext
        };
    }

    public static FormGenerationContext UpdateContext(this FormGenerationContext context, MemberInfo info)
    {
        return new FormGenerationContext()
        {
            ServiceResolver = context.ServiceResolver,
            AttributeExtractor = context.AttributeExtractor,
            MetaGenerator = info.GetAttributeByInterface<IMetaFieldGenerator>() ?? context.MetaGenerator,
            FieldConverter = info.GetAttributeByInterface<IFieldConverter>() ?? context.FieldConverter,
            Processors = context.Processors.Concat(context.AttributeExtractor.GetAttributeServices<IFieldProcessor>(info)),
            ParentContext = context
        };
    }
}

public class FormEngineInstance
{
    protected IMetaFieldGenerator DefaultMetaFieldGenerator { get; set; }
    protected IFieldConverter DefaultFieldConverter { get; set; }
    protected IServiceResolver DefaultServiceResolver { get; set; }
    protected IAttributeService DefaultAttributeExtractor { get; set; }

    public FormEngineInstance()
    {
        this.DefaultFieldConverter = new DefaultFieldConversion();
        this.DefaultMetaFieldGenerator = new DefaultMetaFieldGeneration();
        this.DefaultServiceResolver = new DefaultServiceResolution();
        this.DefaultAttributeExtractor = new AttributeExtractionService()
        {
            Extractors = new IAttributeExtractor[]{
                new DefaultAttributeExtractor(),
                new DelegateProcessorExtractor()
            }
        };
    }

    public async Task<IEnumerable<Field>> GetFields(Type formType)
    {
        var genContext = new FormGenerationContext()
        {
            MetaGenerator = formType.GetAttributeByInterface<IMetaFieldGenerator>() ?? this.DefaultMetaFieldGenerator,
            FieldConverter = formType.GetAttributeByInterface<IFieldConverter>() ?? this.DefaultFieldConverter,
            Processors = formType.GetAttributesByInterface<IFieldProcessor>(),
            ServiceResolver = this.DefaultServiceResolver,
            AttributeExtractor = this.DefaultAttributeExtractor
        };

        var allProperties = formType.GetProperties();

        var allMetaFields = allProperties.SelectMany(x =>
        {
            var fieldContext = genContext.UpdateContext(x);
            return fieldContext.MetaGenerator.GetMetaFields(x, fieldContext);
        });

        var postProcessor = formType.GetAttributesByInterface<IMetaPostProcessor>();

        // var processedMeta = postProcessor.Aggregate((proc, proc2) => proc(allMetaFields))

        var convertedFields = allMetaFields.Select(x => genContext.FieldConverter.ConvertToField(x, genContext));

        return await Task.WhenAll(convertedFields);




        //Generation
        //multi Field
        //RenderBefore / After

        //Async Field Children??


        //Field Converters (Default, Object, Array)

        //get all the properties for the form

        //Organise

        //stepper
        //Priority



    }
}

public class MetaField
{
    public IEnumerable<IFieldProcessor> Processors { get; set; }
    public IEnumerable<MetaField> Children { get; set; }
    public PropertyInfo PropInfo { get; set; }
    public bool Ignore { get; set; }
}