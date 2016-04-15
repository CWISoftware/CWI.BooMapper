using System;

namespace CWI.BooMapper.Core
{
    internal static class ThrowHelper
    {
        public static void ThrowArgumentNullException(string value, string name)
        {
            if(string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(name);
            }
        }

        public static void ThrowArgumentNullException(object value, string name)
        {
            if (value == null)
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}
