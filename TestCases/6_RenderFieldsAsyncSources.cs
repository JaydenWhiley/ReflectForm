using System;
using System.Collections.Generic;
using FormEngine.Attributes.Generators;
using FormEngine.Attributes.Processors;
using FormEngine.Services;

namespace FormEngine;

public class AsyncStringSource : Attribute, IDelegateHandler
{
    public Delegate Handle => (Field field) =>
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