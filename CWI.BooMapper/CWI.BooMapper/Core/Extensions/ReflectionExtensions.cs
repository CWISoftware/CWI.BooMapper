﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CWI.BooMapper.Core.Relational;

namespace CWI.BooMapper.Core.Extensions
{
    internal static class ReflectionExtensions
    {
        public static ConstructorInfo GetParameterlessConstructor(this Type type)
        {
            var constructor = type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                                  .FirstOrDefault(c => c.GetParameters().Length == 0);

            if (constructor == null)
            {
                throw new ArgumentException($"{type.FullName} não possui um construtor sem parâmetros.");
            }

            return constructor;
        }

        public static IEnumerable<PropertyInfo> GetWriteableInstanceProperties(this Type type)
        {
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                       .Where(p => p.CanWrite);
        }

        public static bool IsValueTypeOrString(this Type type)
        {
            return type.IsValueType || type == Methods.TypeOfString;
        }

        public static bool IsEnumerable(this Type type)
        {
            return type.GetInterfaces().Any(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEnumerable<>));
        }

        public static ConstructorInfo GetCollectionConstructor(this Type type)
        {
            return null;
        }

        public static PropertyInfo GetWriteableInstanceProperty(this Type type, string name)
        {
            PropertyInfo prop = type.GetProperty(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            if (prop != null && prop.CanWrite)
            {
                return prop;
            }

            return null;
        }

        public static Type GetTypeOrUnderlyingType(this Type type)
        {
            if (type.IsGenericType)
            {
                return type.GetGenericArguments()[0].GetTypeOrUnderlyingType();
            }
            if (type.IsEnum)
            {
                return type.GetEnumUnderlyingType().GetTypeOrUnderlyingType();
            }
            if(type.IsArray)
            {
                return type.GetElementType();
            }
            return type;
        }

        public static bool IsNullable(this Type type)
        {
            if (!type.IsGenericType)
            {
                return false;
            }

            return type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static ConstructorInfo GetNullableConstructor(this Type type)
        {
            if (!type.IsNullable())
            {
                throw new ArgumentException($"{type.FullName} não é nullable");
            }

            return type.GetConstructors()
                       .FirstOrDefault(c => c.GetParameters().Count() == 1);
        }

        public static int GetNestedLevel(this string propertyName)
        {
            return propertyName.Count(c => c == '.');
        }

        public static MethodInfo ResolveGenericMethod(this MethodInfo method, params Type[] types)
        {
            if(method.IsGenericMethodDefinition)
            {
                return method.MakeGenericMethod(types);
            }
            return method;
        }
    }
}
