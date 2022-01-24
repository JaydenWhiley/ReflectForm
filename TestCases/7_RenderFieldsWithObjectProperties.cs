using System.Collections.Generic;
using FormEngine.Attributes.Generators;
using FormEngine.Attributes.Processors;

namespace FormEngine;

public class ExampleUserDetails
{
    public string Email { get; set; }
    public string Username { get; set; }
}

public class RenderFieldsWithObjectPropertiesExample
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public ExampleUserDetails Admin { get; set; }

    public static IEnumerable<Field> ExpectedResult = new[]{
        new Field(){ Name = "FirstName", FieldType = "String"},
        new Field(){ Name = "LastName", FieldType = "String"},
        new Field(){ Name = "Admin", FieldType = "Object", Children = new[]{
            new Field(){ Name = "Email", FieldType = "String"},
            new Field(){ Name = "Username", FieldType = "String"},
        }},
    };
}