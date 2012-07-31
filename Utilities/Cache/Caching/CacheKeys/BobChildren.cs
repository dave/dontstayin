using System;
using System.Collections.Generic;
using System.Text;

namespace Caching.CacheKeys
{
	public class BobChildren : CacheKey
	{
		public BobChildren(string parentTableName, int parentTableK, string childTableName, string childTableVersionHash)
			: base(CacheKeyPrefix.BobChildren, parentTableName, parentTableK.ToString(), childTableName, childTableVersionHash)
		{

		}

	}
}
