using System.Collections.Generic;
using FormEngine.Attributes.Generators;
using FormEngine.Attributes.Processors;

namespace FormEngine;

public class RenderMultipleFieldsExample
{
    [RenderMultiple(typeof(TextBoxAttribute), typeof(TextBoxAttribute))]
    public string FirstName { get; set; }

    [TextBox]
    public string LastName { get; set; }

    [SelectBox]
    public bool Admin { get; set; }

    public static IEnumerable<Field> ExpectedResult = new[]{
        new Field(){ Name = "FirstName", FieldType = "TextBox"},
        new Field(){ Name = "FirstName", FieldType = "TextBox"},
        new Field(){ Name = "LastName", FieldType = "TextBox"},
        new Field(){ Name = "Admin", FieldType = "SelectBox"},
    };
}