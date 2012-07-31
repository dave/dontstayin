using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Facebook
{
    /// <summary>Provides a caching abstraction that effectively separates cached objects from different applications by API key.</summary>
    [Serializable]
    public class FacebookCache
    {
        /// <summary>Initializes an instance of the <see cref="FacebookCache" /> class using the specified <paramref name="apiKey" /> and
        /// <see cref="ICacheWrapper" /> instance.</summary>
        /// <param name="apiKey">The API key of the application using the cache.</param>
        /// <param name="cache">An instance of <see cref="ICacheWrapper" /> used to abstract access to the cache object used by the application.</param>
        public FacebookCache(String apiKey, ICacheWrapper cache)
        {
            this.ApiKey = apiKey;
            this.InnerCache = cache;
        }

        private String ApiKey { get; set; }
        private ICacheWrapper InnerCache { get; set; }

        private const String CACHE_KEY_FORMAT = "__FBCACHE__{0}__{1}";

        private String GetInternalKey(String key)
        {
            return String.Format(CACHE_KEY_FORMAT, this.ApiKey, key);
        }

        /// <summary>Adds the <paramref name="key"/>/<paramref name="value"/> pair to the cache object.</summary>
        /// <param name="key">The cache key used to reference the item.</param>
        /// <param name="value">The item to be added to the cache.</param>
        public void Add(String key, Object value)
        {
            this.InnerCache.Add(this.GetInternalKey(key), value);
        }

        /// <summary>Removes the specified item from the cache object.</summary>
        /// <param name="key">A <see cref="String" /> identifier for the cache item to remove.</param>
        /// <returns>The item removed from the cache object. If the value in the key parameter is not found, returns <c>null</c>.</returns>
        public Object Remove(String key)
        {
            return this.InnerCache.Remove(this.GetInternalKey(key));
        }

        /// <summary>Determines whether the cache object contains an item with the specified key.</summary>
        /// <param name="key">The key to locate in the cache object.</param>
        /// <returns><c>true</c> if the cache object contains an item with the key; otherwise, <c>false</c>.</returns>
        public Boolean ContainsKey(String key)
        {
            return this.InnerCache.ContainsKey(this.GetInternalKey(key));
        }

        /// <summary>Gets the number of items stored in the cache object.</summary>
        public Int32 Count
        {
            get { return this.InnerCache.Count; }
        }

        /// <summary>Gets or sets the cache item at the specified key.</summary>
        /// <param name="key">A <see cref="String" /> identifier that represents the key for the cache item.</param>
        /// <returns>The specfied cache item.</returns>
        public Object this[String key]
        {
            get{ return this.InnerCache[this.GetInternalKey(key)]; }
            set { this.InnerCache[this.GetInternalKey(key)] = value; }
        }
    }
}
