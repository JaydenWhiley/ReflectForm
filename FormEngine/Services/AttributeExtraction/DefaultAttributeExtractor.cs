using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FormEngine.Interfaces;

namespace FormEngine.Services;


public class DefaultAttributeExtractor : IAttributeExtractor
{
    public Type ServiceType => typeof(IFieldProcessor);

    public IEnumerable<object> ExtractServices(MemberInfo memberInfo)
    {
        return memberInfo.GetAttributesByInterface<IFieldProcessor>();
    }
}