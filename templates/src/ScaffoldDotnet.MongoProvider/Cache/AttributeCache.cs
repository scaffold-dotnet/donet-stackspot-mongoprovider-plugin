using System.Collections.Generic;

namespace ScaffoldDotnet.MongoProvider.Cache
{
    internal static class AttributeCache
    {
        private static readonly Dictionary<string, string> cache = new Dictionary<string, string>();

        public static bool Add(string key, string value)
        {
            return cache.TryAdd(key, value);
        }

        public static bool TryGet(string key, out string output)
        {
            if (cache.TryGetValue(key, out string value))
            {
                output = value;
                return true;
            }

            output = default;
            return false;
        }
    }
}
