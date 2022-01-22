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

public class AttributeExtractionService : IAttributeService
{
    public AttributeExtractionService() => Extractors = Enumerable.Empty<IAttributeExtractor>();

    public IEnumerable<IAttributeExtractor> Extractors { get; set; }

    public IEnumerable<T> GetAttributeServices<T>(MemberInfo memberInfo) where T : class
    {
        var matchedExtractors = Extractors.Where(x => x.ServiceType == typeof(T));
        var foundServices = matchedExtractors.SelectMany(x => x.ExtractServices(memberInfo));
        return foundServices.Where(x => x is T).Select(x => x as T);
    }
}