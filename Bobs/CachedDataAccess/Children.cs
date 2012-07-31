using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Common;
using System.Data;
using Common.Collections;
using Caching;
using Caching.CacheKeys;

namespace Bobs.CachedDataAccess
{
	[Serializable]
	public class Children<T> : SqlCachedQueryDefinition<T>
	{
		public Children(Bobs.TablesEnum parentTable, int parentK, Bobs.TablesEnum childTable, Getter<T, DataRow> createTFromDataRow, string childTableHash, Q where, IEnumerable<KeyValuePair<object, OrderBy.OrderDirection>> orderBy)
			: base(GetSelectCommand(parentTable, childTable, parentK), parentK, Tables.GetTableName(parentTable), where)
		{
			this.Table = childTable;
			this.CreateTFromDataRow = createTFromDataRow;
			this.TableHash = childTableHash;
			this.OrderBy = orderBy;
		}

		private static SqlCommand GetSelectCommand(Bobs.TablesEnum parentTable, Bobs.TablesEnum childTable, int parentK)
		{
			SqlCommand selectCommand = new SqlCommand
					 (
						 String.Format
						 (
							 "SELECT {0}.* FROM {0} WITH (NOLOCK)WHERE {1}K = @parentK ",
							 Tables.GetTableName(childTable),
							 Tables.GetTableName(parentTable)
						 )
					 );
			selectCommand.Parameters.AddWithValue("@parentK", parentK);
			return selectCommand;
		}



		public override CacheKey CacheKey
		{
			get
			{
				List<CacheKey> namespaceCacheKeys = new List<CacheKey>();
				namespaceCacheKeys.AddRange(extraNamespaceCacheKeys);
				namespaceCacheKeys.Add(new BobChildren(parentTableName, parentTableK, Tables.GetTableName(Table), TableHash));
				AddFieldVersionNamespaceKeys(namespaceCacheKeys);
				List<string> cacheKeyParts = new List<string>();
				AddCacheKeyPartsFromSqlCommand(cacheKeyParts);
				AddCacheKeyPartsFromOrderBy(cacheKeyParts);
				return new NamespacedCacheKey
				(
					CacheKeyPrefix.CachedQuerySet,
					namespaceCacheKeys.ToArray(),
					cacheKeyParts.ToArray()
				);
			}
		}
		public override TablesEnum Table { get; protected set; }
		public override string TableHash { get; protected set; }
		public override IEnumerable<KeyValuePair<object, OrderBy.OrderDirection>> OrderBy { get; protected set; }
		public override Getter<T, DataRow> CreateTFromDataRow {get; protected set;}
	}
}
