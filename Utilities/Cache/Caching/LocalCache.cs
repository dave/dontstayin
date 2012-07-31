using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Caching
{
	#region extension to cache class
	public partial class Cache
	{
	
		#region GetWithLocalCaching
		public T GetWithLocalCaching<T>(string key, Create<T> create, TimeSpan localCacheLifespan, TimeSpan distributedCacheLifespan)
		{
			return Instances.LocalCache.Get(key, () => Get<T>(key, create, distributedCacheLifespan), localCacheLifespan);
			
		}
		#endregion
	}
	#endregion
	#region extension to instances cache class
	public static partial class Instances
	{
		public static Common.Getter<ICacheClient> GetLocalCacheClient = GetAspCache;
		public static Cache LocalCache
		{
			get
			{
				return new Cache(GetLocalCacheClient(), "");
			}
		}

		private static AspCache GetAspCache()
		{
			return new AspCache();
		}
	}
	#endregion
	public class AspCache : ICacheClient
	{
		System.Web.Caching.Cache localCache = System.Web.HttpRuntime.Cache;
		public AspCache()
		{
			if (Salt == null)
			{
				FlushAll();
			}
		}
		private static object saltLock = new object();
		private static string Salt { get; set; }
 
		public void FlushAll()
		{
			Salt = Guid.NewGuid().ToString();
		}



		private string Hash(string key)
		{
			return key + Salt;
		}




		public void BlockingDelete(string key)
		{
			localCache.Remove(Hash(key));
		}

		public object Get(string key)
		{
			return localCache.Get(Hash(key));
		}

		public uint? Increment(string key, long value)
		{
			uint? o = (uint?)Get(key);
			if (o.HasValue)
			{
				Store(key, o + value, DateTime.MaxValue);
			}
			return o;
		}

		public void Store(string key, object value, DateTime expiry)
		{
			localCache.Add(Hash(key), value, null, expiry, System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
		}


		public DataTable GetStats()
		{
			return new DataTable();
		}



		public void Dispose()
		{
			
		}

		#region ICacheClient Members


		public void MultiSet(KeyValuePair<string, object>[] pairs, DateTime expiry)
		{
			foreach (var pair in pairs)
			{
				Store(pair.Key, pair.Value, expiry);
			}
		}

		public object[] MultiGet(string[] keys)
		{
			return (new List<string>(keys).ConvertAll(key => Get(key)).ToArray());
		}

		#endregion





		public void Set(string key, object value, DateTime expiry)
		{
			Store(key, value, expiry);
		}

	}
}
