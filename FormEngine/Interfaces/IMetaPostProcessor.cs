using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace FormEngine;

public interface IMetaPostProcessor
{
    IEnumerable<MetaField> ProcessMetaFields(IEnumerable<MetaField> metaFields);
}