using System;
using System.Threading.Tasks;

namespace FormEngine.Attributes.Processors;

public abstract class AsyncFieldProcessor : Attribute, IFieldProcessor
{
    public abstract void ProcessField(IFieldContext context);

    public Task ProcessFieldAsync(IFieldContext context)
    {
        this.ProcessField(context);
        return Task.CompletedTask;
    }
}

public abstract class FieldProcessorAttribute : Attribute, IFieldProcessor
{
    public abstract void ProcessField(IFieldContext context);

    public Task ProcessFieldAsync(IFieldContext context)
    {
        this.ProcessField(context);
        return Task.CompletedTask;
    }
}
