using System.Collections.Generic;

namespace FormEngine;

public abstract class FieldOption
{
    public FieldOption(string optionName)
    {
        this.OptionName = optionName;
    }
    public string OptionName { get; set; }
}

public abstract class FieldSource
{
    public string SourceType { get; set; }
}

public enum DisplayTypeEnum { All, ViewOnly, SubmissionOnly };

public class Field
{
    public Field()
    {
        this.FieldOptions = new List<FieldOption>();
    }

    public string Label { get; set; }
    public string ViewOnlyLabel { get; set; }
    public string Description { get; set; }
    public string Name { get; set; }

    public int? Step { get; set; }

    public string DataType { get; set; }
    public string FieldType { get; set; }

    public object DefaultValue { get; set; }

    public object Value { get; set; }

    public List<FieldOption> FieldOptions { get; set; }

    public FieldSource Source { get; set; }

    public DisplayTypeEnum DisplayType { get; set; }

    public bool ViewMode { get; set; }

    public IEnumerable<Field> Children { get; set; }

    public bool Ignore { get; set; }
}