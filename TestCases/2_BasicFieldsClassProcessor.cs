using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FormEngine;

[UppercaseLabelProcessor]
public class BasicClassProcessorExample
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool Admin { get; set; }

    public static IEnumerable<Field> ExpectedResult = new[]{
        new Field(){ Name = "FIRSTNAME", FieldType = "String"},
        new Field(){ Name = "LASTNAME", FieldType = "String"},
        new Field(){ Name = "ADMIN", FieldType = "Boolean"},
    };
}


public class UppercaseLabelProcessor : Attribute, IFieldProcessor
{
    public Task ProcessFieldAsync(IFieldContext context)
    {
        context.CurrentField.Name = context.CurrentField.Name.ToUpper();
        return Task.CompletedTask;
    }
}