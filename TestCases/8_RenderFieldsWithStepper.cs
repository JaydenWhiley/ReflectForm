using System.Collections.Generic;
using FormEngine.Attributes.Generators;
using FormEngine.Attributes.Processors;

namespace FormEngine;

[Stepper("Enter Data", "Review Data")]
public class RenderFieldsWithStepper
{
    public string NoGrouping { get; set; }

    [Step(0)]
    [TextBox]
    public string FirstName { get; set; }

    [TextBox]
    [Step(0)]
    public string LastName { get; set; }

    [SelectBox]
    [Step(1)]
    public bool Admin { get; set; }

    public static IEnumerable<Field> ExpectedResult = new[]{
        new Field(){ Name = "NoGrouping", FieldType = "String"},
        new Field(){
            FieldType = "Stepper",
            Children = new []{
                new Field(){
                    FieldType = "Step",
                    Label = "Enter Data",
                    Children = new []{
                        new Field(){ Name = "FirstName", FieldType = "TextBox"},
                        new Field(){ Name = "LastName", FieldType = "TextBox"},
                    }
                },
                new Field(){
                    Label = "Review Data",
                    FieldType = "Step",
                    Children = new []{
                        new Field(){ Name = "Admin", FieldType = "SelectBox"},
                    }
                },
            }
        }
    };
}