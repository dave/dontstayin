using System;
using System.Collections.Generic;
using System.Text;

namespace Caching.CacheKeys
{
	public class BobCacheKey : CacheKey
	{
		public BobCacheKey(string tableName, int k, string tableHash)
			: base(CacheKeyPrefix.TableRow, tableName, tableHash, k.ToString())
		{

		}
	}
}
