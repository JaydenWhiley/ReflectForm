using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormEngine.Attributes.Processors;

public class StepperAttribute : Attribute, IMetaPostProcessor
{
    public string[] Steps { get; set; }
    public StepperAttribute(params string[] steps) => this.Steps = steps;

    protected string GetStepName(int index)
    {
        if (index < 0) return "";
        return this.Steps?.Skip(index)?.FirstOrDefault() ?? "";
    }

    public IEnumerable<MetaField> ProcessMetaFields(IEnumerable<MetaField> metaFields, FormGenerationContext context)
    {
        var groupedFields = metaFields.GroupBy(f => f.PropInfo?.GetAttributeByInterface<StepAttribute>()?.Index ?? -1);

        //Fields with step attributes
        var stepFields = groupedFields.Where(g => g.Key >= 0);
        //Fields without step attributes
        var otherFields = groupedFields.Where(g => g.Key < 0).SelectMany(x => x);

        var mySteps = stepFields.Select(group => new MetaField()
        {
            Processors = new IFieldProcessor[] { new ChangeFieldTypeAttribute("Step"), new LabelAttribute(this.GetStepName(group.Key)) },
            Children = group.ToArray()
        });

        return otherFields.Append(
            new MetaField()
            {
                Processors = context.Processors.Prepend(new ChangeFieldTypeAttribute("Stepper")),
                Children = mySteps,
            }
        );
    }
}

public class StepAttribute : Attribute
{
    public int Index { get; set; }
    public StepAttribute(int index) => this.Index = index;
}

