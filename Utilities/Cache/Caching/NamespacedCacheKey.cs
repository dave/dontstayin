using System;
using System.Collections.Generic;
using System.Text;
using Common;
using Caching.CacheKeys;

namespace Caching
{
	public class NamespacedCacheKey : CacheKey
	{
		public NamespacedCacheKey(CacheKeyPrefix prefix, CacheKey[] namespaceCacheKeys, params string[] keyParts)
			: base(prefix, GetParts(namespaceCacheKeys, keyParts))
		{

		}
		
		public NamespacedCacheKey(CacheKeyPrefix prefix, CacheKey namespaceCacheKey, params string[] keyParts)
			: base(prefix, GetParts(new CacheKey[] { namespaceCacheKey }, keyParts))
		{

		}

		private static string[] GetParts(CacheKey[] namespaceCacheKeys, string[] keyParts)
		{
			List<string> returnList = new List<string>();
			string[] versionKeys = namespaceCacheKeys.ConvertAll(key => key.ToString());
			Getter<string>[] getters = new Getter<string>[namespaceCacheKeys.Length];
			getters.Length.Times(i => getters[i] = () => Guid.NewGuid().ToString());
			string[] versionValues = Caching.Instances.Main.MultiGet<string>(versionKeys, getters, DateTime.MaxValue);
			returnList.AddRange(versionValues);
			returnList.AddRange(keyParts);
			return returnList.ToArray();
		}
	}
}
