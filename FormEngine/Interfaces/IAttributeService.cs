using System;
using System.Collections.Generic;
using System.Reflection;

namespace FormEngine.Interfaces;

public interface IAttributeExtractor
{
    IEnumerable<object> ExtractServices(MemberInfo memberInfo);
    Type ServiceType { get; }
}

public interface IAttributeService
{
    public IEnumerable<T> GetAttributeServices<T>(MemberInfo memberInfo) where T : class;
}