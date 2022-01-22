using System;
using System.Threading.Tasks;

namespace FormEngine.Attributes.Processors;

public abstract class ChangeFieldTypeAttribute : FieldProcessorAttribute
{
    public string FieldType { get; set; }

    public ChangeFieldTypeAttribute()
    {
        this.FieldType = this.GetType().Name.Replace("Attribute", "");
    }

    public ChangeFieldTypeAttribute(string fieldType)
    {
        this.FieldType = fieldType;
    }

    public override void ProcessField(IFieldContext context) => context.CurrentField.FieldType = FieldType;
}