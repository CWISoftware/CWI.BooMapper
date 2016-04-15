using System;
using System.Data;
using System.Reflection;

namespace CWI.BooMapper.Core.Relational
{
    internal static class Methods
    {
        internal static readonly Type TypeOfMapper = typeof(RelationalMapper);
        internal static readonly Type TypeOfGuid = typeof(Guid);
        internal static readonly Type TypeOfGuidNullable = typeof(Guid?);
        internal static readonly Type TypeOfObject = typeof(object);
        internal static readonly Type TypeOfString = typeof(string);
        internal static readonly Type TypeOfDateTime = typeof(DateTime);
        internal static readonly Type TypeOfDateTimeNullable = typeof(DateTime?);

        internal static readonly MethodInfo IDataRecord_GetValue =
            typeof(IDataRecord).GetMethod(nameof(IDataRecord.GetValue));

        internal static readonly MethodInfo IDataRecord_IsDbNull =
            typeof(IDataRecord).GetMethod(nameof(IDataRecord.IsDBNull));

        internal static readonly MethodInfo RelationalMapBuilder_GetGuid =
            typeof(RelationalMapBuilder).GetMethod(nameof(RelationalMapBuilder.GetGuid), BindingFlags.NonPublic | BindingFlags.Static);

        internal static readonly MethodInfo RelationalMapBuilder_ThrowDataException =
            typeof(RelationalMapBuilder).GetMethod(nameof(RelationalMapBuilder.ThrowDataException), BindingFlags.NonPublic | BindingFlags.Static);

        internal static readonly MethodInfo Convert_ToDateTime =
            typeof(Convert).GetMethod(nameof(Convert.ToDateTime), new[] { TypeOfObject });

        internal static readonly MethodInfo MapperCache_Get =
            typeof(RelationalMapperCache).GetMethod(nameof(RelationalMapperCache.Get));

        internal static readonly MethodInfo Mapper_Invoke =
            typeof(RelationalMapper).GetMethod(nameof(RelationalMapper.Invoke));
    }
}
