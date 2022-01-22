using System.Collections.Generic;
using System.Linq;

namespace FormEngine.Attributes.Generators;

public class RenderAfter<T> : FieldInjectionAttribute<T>
{
    public RenderAfter(params object[] args) : base(args)
    {
    }

    protected override IEnumerable<MetaField> CombineFields(IEnumerable<MetaField> original, IEnumerable<MetaField> injected)
    {
        return original.Concat(injected);
    }
}
