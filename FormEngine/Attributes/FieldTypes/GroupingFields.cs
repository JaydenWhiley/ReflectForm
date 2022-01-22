using System;
using System.Threading.Tasks;

namespace FormEngine.Attributes.Processors;

public class StepperAttribute : ChangeFieldTypeAttribute
{
    public string[] Steps { get; set; }
    public StepperAttribute(params string[] steps) => this.Steps = steps;
}

public class StepAttribute : Attribute
{
    public int Index { get; set; }
    public StepAttribute(int index) => this.Index = index;
}

