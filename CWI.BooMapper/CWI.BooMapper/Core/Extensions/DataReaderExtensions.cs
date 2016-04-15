using System.Collections.Generic;
using System.Data;

namespace CWI.BooMapper.Core.Extensions
{
    internal static class DataReaderExtensions
    {
        public static IEnumerable<string> GetNames(this IDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                yield return reader.GetName(i);
            }
        }
    }
}
