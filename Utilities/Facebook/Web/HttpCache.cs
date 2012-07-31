using System;
using System.Web;
using System.Web.Caching;

namespace Facebook.Web
{
    /// <summary>An implementation of <see cref="ICacheWrapper" /> that provides access to the ASP.NET HTTP Cache (<see cref="HttpRuntime.Cache" />).</summary>
    public class HttpCache : ICacheWrapper
    {
        #region [ ICacheWrapper Members ]

        void ICacheWrapper.Add(String key, Object value)
        {
            this.Cache.Add(key, value, null, Cache.NoAbsoluteExpiration, TimeSpan.MaxValue, CacheItemPriority.Normal, null);
        }

        Object ICacheWrapper.Remove(String key)
        {
            return this.Cache.Remove(key);
        }

        Boolean ICacheWrapper.ContainsKey(String key)
        {
            return this.Cache[key] != null;
        }

        Int32 ICacheWrapper.Count
        {
            get { return this.Cache.Count; }
        }

        Object ICacheWrapper.this[String key]
        {
            get { return this.Cache[key]; }
            set { this.Cache[key] = value; }
        }

        #endregion

        private Cache Cache
        {
            get { return HttpRuntime.Cache; }
        }
    }
}
