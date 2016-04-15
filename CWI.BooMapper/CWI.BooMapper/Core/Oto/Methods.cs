using System;
using System.Collections.Generic;
using System.Reflection;

namespace CWI.BooMapper.Core.Oto
{
    internal static class Methods
    {
        internal static readonly Type[] EmptyTypeArray = new Type[0];

        internal static readonly HashSet<Type> CopiablePrimitives = new HashSet<Type>()
        {
            typeof(short),
            typeof(int),
            typeof(long),
            typeof(ushort),
            typeof(uint),
            typeof(ulong),
            typeof(float),
            typeof(double),
            typeof(decimal),
        };

        internal static readonly MethodInfo OTOMapperBuilder_GetNullableValue =
            typeof(OtoMapBuilder).GetMethod(nameof(OtoMapBuilder.GetNullableValue),
                                            BindingFlags.NonPublic | BindingFlags.Static);

        internal static readonly MethodInfo OTOMapperBuilder_CopyList =
            typeof(OtoMapBuilder).GetMethod(nameof(OtoMapBuilder.CopyList),
                                            BindingFlags.NonPublic | BindingFlags.Static);

        internal static readonly MethodInfo OTOMapperBuilder_CopyIEnumerable =
            typeof(OtoMapBuilder).GetMethod(nameof(OtoMapBuilder.CopyIEnumerable),
                                            BindingFlags.NonPublic | BindingFlags.Static);

        internal static readonly MethodInfo OTOMapperBuilder_CopyIList =
           typeof(OtoMapBuilder).GetMethod(nameof(OtoMapBuilder.CopyIList),
                                            BindingFlags.NonPublic | BindingFlags.Static);

        internal static readonly MethodInfo OTOMapperBuilder_CopyICollection =
           typeof(OtoMapBuilder).GetMethod(nameof(OtoMapBuilder.CopyICollection),
                                            BindingFlags.NonPublic | BindingFlags.Static);

        internal static readonly MethodInfo OTOMapperBuilder_CopyArray =
           typeof(OtoMapBuilder).GetMethod(nameof(OtoMapBuilder.CopyArray),
                                            BindingFlags.NonPublic | BindingFlags.Static);

        internal static readonly Dictionary<Type, MethodInfo> CopyMethods = new Dictionary<Type, MethodInfo>()
        {
            { typeof(IEnumerable<>),OTOMapperBuilder_CopyIEnumerable  },
            { typeof(IList<>),OTOMapperBuilder_CopyIList  },
            { typeof(List<>),OTOMapperBuilder_CopyList  },
            { typeof(ICollection<>),OTOMapperBuilder_CopyICollection  },
            { typeof(Array),OTOMapperBuilder_CopyArray  }
        };

        internal static MethodInfo GetCopyMethod(Type type)
        {
            if(type.IsArray)
            {
                return CopyMethods[typeof(Array)];
            }
            if(type.IsGenericType)
            {
                return CopyMethods[type.GetGenericTypeDefinition()];
            }
            return CopyMethods[type];
        }
    }
}
