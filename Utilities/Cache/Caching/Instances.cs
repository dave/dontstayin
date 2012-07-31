using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using System.Text;
using System.Configuration;
using Caching.Memcached;



namespace Caching
{
	public static partial class Instances
	{
		static Instances()
		{
			RefreshServers();
			GetMainCacheClient = () => new MemcachedClient(mainMemcachedInstances, new SHA1Hasher());
			GetViewStateCacheClient = () => new MemcachedClient(viewStateMemcachedInstances, new SHA1Hasher());

		}
		public static Common.Getter<ICacheClient> GetMainCacheClient { get; set; }
		public static Common.Getter<ICacheClient> GetViewStateCacheClient { get; set; }
		static MemcachedInstances mainMemcachedInstances;
		static MemcachedInstances viewStateMemcachedInstances;

		static readonly int maximumSizeOfConnectionPool = 100;

		public static void RefreshServers()
		{
			mainMemcachedInstances = new MemcachedInstances(maximumSizeOfConnectionPool, Common.Properties.MainCacheServerIPEndPoints);
			viewStateMemcachedInstances = new MemcachedInstances(maximumSizeOfConnectionPool, Common.Properties.ViewStateCacheServerIPEndPoints);

		}


		public static Cache Main
		{
			get
			{
				return new Cache(GetMainCacheClient(), "Main");
			}
		}
		public static Cache ViewState
		{
			get
			{
				return new Cache(GetViewStateCacheClient(), "ViewState");
			}
		}

		public static IHasher Hasher { get { return new SHA1Hasher(); } }
		public static ICounterStore MainCounterStore
		{
			get
			{
				return new Cache(new MemcachedClient(mainMemcachedInstances, Hasher), "MainCounterStore");
			}
		}




		internal static MemcachedInstances GetInstances()
		{
			return mainMemcachedInstances;
		}
	}
}
