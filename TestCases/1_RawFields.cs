using System.Collections.Generic;

namespace FormEngine;


public class RawClassExample
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool Admin { get; set; }

    public static IEnumerable<Field> ExpectedResult = new[]{
        new Field(){ Name = "FirstName", FieldType = "String"},
        new Field(){ Name = "LastName", FieldType = "String"},
        new Field(){ Name = "Admin", FieldType = "Boolean"},
    };
}