using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormEngine.Attributes.Processors;

public class StepperAttribute : Attribute, IMetaPostProcessor
{
    public string[] Steps { get; set; }
    public StepperAttribute(params string[] steps) => this.Steps = steps;

    public IEnumerable<MetaField> ProcessMetaFields(IEnumerable<MetaField> metaFields)
    {
        var groupedFields = metaFields.GroupBy(f => f.PropInfo.GetAttributeByInterface<StepAttribute>()?.Index ?? -1);
        var mySteps = groupedFields.Select(group => new MetaField()
        {
            Children = group.ToArray()
        });
        return mySteps;
    }
}

public class StepAttribute : Attribute
{
    public int Index { get; set; }
    public StepAttribute(int index) => this.Index = index;
}

