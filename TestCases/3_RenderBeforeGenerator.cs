using System.Collections.Generic;
using FormEngine.Attributes.Generators;

namespace FormEngine;



public class RenderBeforeGeneratorExample
{
    [RenderAfter<ExampleAfterFields>]
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [RenderBefore<ExampleBeforeFields>]
    public bool Admin { get; set; }

    public static IEnumerable<Field> ExpectedResult = new[]{
        new Field(){ Name = "FirstName", FieldType = "String"},
        new Field(){ Name = "NewUser", FieldType = "Boolean"},
        new Field(){ Name = "LastName", FieldType = "String"},
        new Field(){ Name = "Username", FieldType = "String"},
        new Field(){ Name = "Email", FieldType = "String"},
        new Field(){ Name = "Admin", FieldType = "Boolean"},
    };
}

public class ExampleAfterFields
{
    public bool NewUser { get; set; }
}

public class ExampleBeforeFields
{
    public string Username { get; set; }
    public string Email { get; set; }
}