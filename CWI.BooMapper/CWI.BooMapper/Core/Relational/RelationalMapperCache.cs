using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace CWI.BooMapper.Core.Relational
{
    public static class RelationalMapperCache
    {
        private static readonly ConcurrentDictionary<string, RelationalMapper> cache
            = new ConcurrentDictionary<string, RelationalMapper>();

        public static void Add(string key, RelationalMapper func)
        {
            cache.TryAdd(key, func);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RelationalMapper Get(string key)
        {
            return cache[key];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGet(string key, out RelationalMapper mapper)
        {
            return cache.TryGetValue(key, out mapper);
        }

        public static void Clear()
        {
            cache.Clear();
        }
    }
}
