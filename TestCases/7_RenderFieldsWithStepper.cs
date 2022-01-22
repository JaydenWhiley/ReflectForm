using System.Collections.Generic;
using FormEngine.Attributes.Generators;
using FormEngine.Attributes.Processors;

namespace FormEngine;

[Stepper("Enter Data", "Review Data")]
public class RenderFieldsWithStepper
{
    [Step(0)]
    public string FirstName { get; set; }

    [TextBox]
    [Step(0)]
    public string LastName { get; set; }

    [SelectBox]
    [Step(1)]
    public bool Admin { get; set; }

    public static IEnumerable<Field> ExpectedResult = new[]{
        new Field(){ Name = "FirstName", FieldType = "TextBox"},
        new Field(){ Name = "FirstName", FieldType = "TextBox"},
        new Field(){ Name = "DelegateSource", FieldType = "TextBox"},
        new Field(){ Name = "Admin", FieldType = "SelectBox"},
    };
}