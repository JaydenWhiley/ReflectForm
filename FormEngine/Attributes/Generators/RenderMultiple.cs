using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FormEngine.Attributes.Generators;

public class RenderMultiple<T, T1> : RenderMultiple
{
    public RenderMultiple() : base(typeof(T), typeof(T1))
    {
    }

}

public class RenderMultiple : Attribute, IMetaFieldGenerator
{
    public Type[] Args { get; set; }

    public RenderMultiple(params Type[] args)
    {
        this.Args = args;
    }

    public IEnumerable<MetaField> GetMetaFields(PropertyInfo info, FormGenerationContext context)
    {
        //Create a new field for each of the Args
        var childProcessors = this.Args.Select(type => Activator.CreateInstance(type)).Where(x => x is IFieldProcessor).Select(x => x as IFieldProcessor);
        var duplicateFields = childProcessors.SelectMany(procType =>
        {
            var childContext = context.Clone();
            childContext.Processors = childContext.Processors.Append(procType);
            var defaultFields = childContext.ParentContext.MetaGenerator.GetMetaFields(info, childContext);
            return defaultFields;
        });
        return duplicateFields;
    }
}