using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FormEngine.Attributes.Generators;

public abstract class FieldInjectionAttribute<T> : Attribute, IMetaFieldGenerator
{
    public FieldInjectionAttribute(params object[] args)
    {
    }

    protected abstract IEnumerable<MetaField> CombineFields(IEnumerable<MetaField> original, IEnumerable<MetaField> injected);

    public IEnumerable<MetaField> GetMetaFields(PropertyInfo info, FormGenerationContext context)
    {
        var defaultFields = context.ParentContext.MetaGenerator.GetMetaFields(info, context);
        var propsToInsert = typeof(T).GetProperties();
        var fieldsToInsert = propsToInsert.SelectMany(x => context.ParentContext.MetaGenerator.GetMetaFields(x, context));
        return this.CombineFields(defaultFields, fieldsToInsert);
    }
}