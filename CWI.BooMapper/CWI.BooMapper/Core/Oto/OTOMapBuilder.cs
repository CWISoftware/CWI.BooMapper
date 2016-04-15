using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CWI.BooMapper.Core.Oto
{
    internal abstract class OtoMapBuilder
    {
        protected OtoMapBuilder() { }

        internal static List<T> CopyList<T>(IEnumerable<T> from)
        {
            if (from == null)
            {
                return null;
            }
            return new List<T>(from);
        }

        internal static IEnumerable<T> CopyIEnumerable<T>(IEnumerable<T> from)
        {
            return CopyList(from);
        }

        internal static IList<T> CopyIList<T>(IList<T> from)
        {
            return CopyList(from);
        }

        internal static ICollection<T> CopyICollection<T>(ICollection<T> from)
        {
            return CopyList(from);
        }

        internal static T[] CopyArray<T>(T[] from)
        {
            if (from == null)
            {
                return null;
            }

            Type typeOfT = typeof(T);
            T[] result = new T[from.Length];

            if (Methods.CopiablePrimitives.Contains(typeOfT))
            {
                Buffer.BlockCopy(from, 0, result, 0, from.Length * Marshal.SizeOf(typeOfT));
            }
            else
            {
                Array.Copy(from, result, from.Length);
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static T GetNullableValue<T>(T? value) where T : struct
        {
            return value.GetValueOrDefault();
        }
    }
}
