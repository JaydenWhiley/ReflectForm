using System.Reflection;
using System.Threading.Tasks;

namespace FormEngine;

public class FieldContext : IFieldContext
{
    public Field CurrentField { get; set; }
    public PropertyInfo PropInfo { get; set; }
    public IServiceResolver ServiceResolver { get; set; }
}

public interface IFieldContext
{
    Field CurrentField { get; }
    IServiceResolver ServiceResolver { get; }
    // PropertyInfo PropInfo { get; }
}

public interface IFieldProcessor
{
    Task ProcessFieldAsync(IFieldContext context);
}