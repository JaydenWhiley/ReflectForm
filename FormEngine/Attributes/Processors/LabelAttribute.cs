using System;
using System.Threading.Tasks;

namespace FormEngine.Attributes.Processors;

public class LabelAttribute : FieldProcessorAttribute
{
    public LabelAttribute(string label) => this.Label = label;
    public string Label { get; set; }

    public override void ProcessField(IFieldContext context) => context.CurrentField.Label = Label;
}
