using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


    public static class CSharpExends
    {
        public static void Set<T>(this IMemoryCache cache, string key, T value, int second)
        {
            var datetime = DateTime.Now.AddSeconds(second);
            cache.Set(key, value, datetime);
        }
    }
