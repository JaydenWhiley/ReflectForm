using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FormEngine.Attributes.Generators;
using FormEngine.Attributes.Processors;

namespace FormEngine;


public abstract class DelegateProcessor : Attribute, IFieldProcessor
{
    public abstract Delegate Handle { get; }

    public async Task ProcessFieldAsync(IFieldContext context)
    {
        var resolver = context.ServiceResolver;
        var callData = new List<object>();
        var handleParams = this.Handle.Method.GetParameters();
        foreach (var parameter in handleParams)
        {
            var resolveResult = resolver.ResolveService(parameter.ParameterType);
            var callDataEntry = resolveResult.Success ? resolveResult.Value : null;
            callData.Add(callDataEntry);
        }
        var handleResult = this.Handle.DynamicInvoke(callData.ToArray());
        if (handleResult is Task handleTask) await handleTask;
    }
}

public class AsyncStringSource : DelegateProcessor
{
    public override Delegate Handle => (Field field) =>
    {
        field.Name = "DelegateSource";
    };
}

public class RenderFieldsWithAsyncSources
{
    [RenderMultiple(typeof(TextBoxAttribute), typeof(TextBoxAttribute))]
    public string FirstName { get; set; }

    [TextBox]
    [AsyncStringSource]
    public string LastName { get; set; }

    [SelectBox]
    public bool Admin { get; set; }

    public static IEnumerable<Field> ExpectedResult = new[]{
        new Field(){ Name = "FirstName", FieldType = "TextBox"},
        new Field(){ Name = "FirstName", FieldType = "TextBox"},
        new Field(){ Name = "DelegateSource", FieldType = "TextBox"},
        new Field(){ Name = "Admin", FieldType = "SelectBox"},
    };
}