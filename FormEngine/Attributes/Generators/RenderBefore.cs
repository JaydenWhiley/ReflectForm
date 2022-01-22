using System.Collections.Generic;
using System.Linq;

namespace FormEngine.Attributes.Generators;


public class RenderBefore<T> : FieldInjectionAttribute<T>
{
    public RenderBefore(params object[] args) : base(args)
    {
    }

    protected override IEnumerable<MetaField> CombineFields(IEnumerable<MetaField> original, IEnumerable<MetaField> injected)
    {
        return injected.Concat(original);
    }
}
