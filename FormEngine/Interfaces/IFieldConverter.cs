using System.Threading.Tasks;

namespace FormEngine.Interfaces;

public interface IFieldConverter
{
    Task<Field> ConvertToField(MetaField meta, FormGenerationContext context);
}