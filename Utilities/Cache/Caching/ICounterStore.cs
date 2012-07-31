
using System;
namespace Caching
{
	public interface ICounterStore: IDisposable
	{
		uint GetCounter(string key, Cache.Create<uint> create);

		void SetCounter(string key, uint value);
		uint Increment(string key, Cache.Create<uint> create);

		void FlushAll();
	}
	public static class ICounterStoreExtensions
	{
		public static uint Increment(this ICounterStore counterStore, CacheKey cacheKey, Cache.Create<uint> create)
		{
			return counterStore.Increment(cacheKey.ToString(), create);
		}
		public static uint GetCounter(this ICounterStore counterStore, CacheKey key, Cache.Create<uint> create)
		{
			return counterStore.GetCounter(key.ToString(), create);
		}
	}
}
