using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FormEngine.Interfaces;

namespace FormEngine.Services;

public interface IDelegateHandler
{
    Delegate Handle { get; }
}

public class DelegateHandlerProxy : IFieldProcessor
{
    public IDelegateHandler Handler { get; set; }

    public DelegateHandlerProxy(IDelegateHandler handler) => this.Handler = handler;

    public async Task ProcessFieldAsync(IFieldContext context)
    {
        var resolver = context.ServiceResolver;
        var callData = new List<object>();
        var handleParams = this.Handler.Handle.Method.GetParameters();
        foreach (var parameter in handleParams)
        {
            var resolveResult = resolver.ResolveService(parameter.ParameterType);
            var callDataEntry = resolveResult.Success ? resolveResult.Value : null;
            callData.Add(callDataEntry);
        }
        var handleResult = this.Handler.Handle.DynamicInvoke(callData.ToArray());
        if (handleResult is Task handleTask) await handleTask;
    }
}


public class DelegateProcessorExtractor : IAttributeExtractor
{
    public Type ServiceType => typeof(IFieldProcessor);

    public IEnumerable<object> ExtractServices(MemberInfo memberInfo)
    {
        var handlers = memberInfo.GetAttributesByInterface<IDelegateHandler>();
        return handlers.Select(x => new DelegateHandlerProxy(x));
    }
}