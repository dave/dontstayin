using System;
using System.Collections.Generic;
using System.Text;

namespace Caching.CacheKeys
{
	public class NamespaceCacheKey : CacheKey
	{
		public NamespaceCacheKey(CacheKeyPrefix prefix, params string[] keyParts) : base(prefix, keyParts) { }
		
		public void Invalidate()
		{
			Caching.Instances.Main.Set(this, Guid.NewGuid().ToString());
		}
	}
}
 
