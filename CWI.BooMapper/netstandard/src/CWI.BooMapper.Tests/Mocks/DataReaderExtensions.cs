using System;
using System.Data;

namespace CWI.BooMapper.Tests.Mocks
{
    internal static class DataReaderExtensions
    {
        private static readonly Type Type_DateTime = typeof(DateTime);
        private static readonly Type Type_DateTimeNullable = typeof(DateTime?);

        public static TResult GetValue<TResult>(this IDataReader reader, string columnName)
        {
            object value = reader.GetValue(reader.GetOrdinal(columnName));

            if (value == DBNull.Value)
            {
                return default(TResult);
            }

            if (typeof(TResult) == Type_DateTime || typeof(TResult) == Type_DateTimeNullable)
            {
                value = Convert.ToDateTime(value);
            }

            return (TResult)value;
        }
    }
}
