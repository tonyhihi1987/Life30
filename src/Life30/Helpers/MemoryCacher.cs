using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace Life30.Helpers
{
    public static class MemoryCacher
    {
        public static object GetValue(string key)
        {
            try
            {
                MemoryCache memoryCache = MemoryCache.Default;
                return memoryCache.Get(key);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Enable to get cache with message : {0}", ex.Message), ex);
            }
        }
        public static bool Add(string key, object value, DateTimeOffset absExpiration)
        {
            try
            {
                MemoryCache memoryCache = MemoryCache.Default;
                if (memoryCache.Contains(key))
                    memoryCache.Remove(key);

                    return memoryCache.Add(key, value, absExpiration);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Enable to add to cache with message : {0}", ex.Message), ex);
            }
        }
        public static bool Contain(string key)
        {
           
                MemoryCache memoryCache = MemoryCache.Default;
                return memoryCache.Contains(key);
           
        }
        public static void Delete(string key)
        {
            try
            {
                MemoryCache memoryCache = MemoryCache.Default;
                if (memoryCache.Contains(key))
                {
                    memoryCache.Remove(key);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Enable to Delete cache with message : {0}", ex.Message), ex);
            }
        }

    }
}
