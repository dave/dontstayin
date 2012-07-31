using System;
using System.Collections.Generic;
using System.Text;

namespace Caching.CacheKeys
{
	public class BobChildFieldVersion : CacheKey
	{
		public BobChildFieldVersion(string parentTableName, int parentTableK, string childTableName, string childTableVersionHash, string fieldName)
			: base(CacheKeyPrefix.BobChildrenField, parentTableName, parentTableK.ToString(), childTableName, childTableVersionHash, fieldName)
		{

		}

	}
}

