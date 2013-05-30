
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace Membrane.Foundation.Runtime.Caching
{
    /// <summary>
    /// Managed cache to store key/value pair.
    /// </summary>
    public static class Cache
    {
        #region - Constants & static fields -

        /// <summary>
        /// The internal instance of the cache.
        /// </summary>
        private static readonly ObjectCache cache = MemoryCache.Default;

        /// <summary>
        /// The default experation timeout.
        /// </summary>
        private static readonly TimeSpan DEFAULT_EXPIRATION_TIMEOUT = new TimeSpan(0, 5, 0);

        #endregion

        /// <summary>
        /// Retrieve cached item.
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="key">Name of cached item</param>
        /// <returns>Cached item as type</returns>
        public static T Get<T>(string key) where T : class
        {
            try
            {
                return (T)Cache.cache[key];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Insert value into the cache using
        /// appropriate name/value pairs
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="key">Name of item</param>
        /// <param name="instance">Item to be cached</param>
        public static void Add<T>(string key, T instance) where T : class
        {
            Cache.cache.Add(key, instance, DateTime.Now.Add(Cache.DEFAULT_EXPIRATION_TIMEOUT));
        }

        /// <summary>
        /// Insert value into the cache using
        /// appropriate name/value pairs
        /// </summary>
        /// <param name="key">Name of item</param>
        /// <param name="instance">Item to be cached</param>
        public static void Add(string key, object instance)
        {
            Cache.cache.Add(key, instance, DateTime.Now.Add(Cache.DEFAULT_EXPIRATION_TIMEOUT));
        }

        /// <summary>
        /// Remove item from cache.
        /// </summary>
        /// <param name="key">Name of cached item</param>
        public static void Remove(string key)
        {
            Cache.cache.Remove(key);
        }

        /// <summary>
        /// Check for item in cache.
        /// </summary>
        /// <param name="key">Name of cached item</param>
        /// <returns></returns>
        public static bool Contains(string key)
        {
            return Cache.cache.Contains(key);
        }

        /// <summary>
        /// Gets all cached items as a list by their key.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAll()
        {
            return Cache.cache.Select(keyValuePair => keyValuePair.Key).ToList();
        }
    }
}
