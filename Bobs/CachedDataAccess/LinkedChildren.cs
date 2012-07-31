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
	public class LinkedChildren<T> : SqlCachedQueryDefinition<T>
	{

		Bobs.TablesEnum linkTable;
		string linkTableHash;
		public LinkedChildren(Bobs.TablesEnum parentTable, int parentK, Bobs.TablesEnum childTable, string childTableHash, Getter<T, DataRow> createTFromDataRow, Q where, KeyValuePair<object, OrderBy.OrderDirection>[] orderBy, Bobs.TablesEnum linkTable, string linkTableHash)
			: base(
				GetSelectCommand(parentTable, childTable, linkTable, parentK, orderBy),
				parentK,
				Tables.GetTableName(parentTable),
				where,
				new Caching.CacheKeys.BobChildren(Tables.GetTableName(linkTable), parentK, Tables.GetTableName(childTable), childTableHash)
			)
		{
			this.Table = childTable;
			this.CreateTFromDataRow = createTFromDataRow;
			this.TableHash = childTableHash;
			this.OrderBy = orderBy;
			this.linkTable = linkTable;
			this.linkTableHash = linkTableHash;
		}

		private static SqlCommand GetSelectCommand(Bobs.TablesEnum parentTable, Bobs.TablesEnum childTable, Bobs.TablesEnum linkTable, int parentK, KeyValuePair<object, OrderBy.OrderDirection>[] orderBy)
		{
			var selectCommand = new SqlCommand
					 (
						 String.Format
						 (
							 "SELECT {0}.* {3} FROM {0} WITH (NOLOCK) INNER JOIN {1} WITH (NOLOCK) ON {0}.K = {1}.{0}K WHERE {1}.{2}K = @parentK ",
							 Tables.GetTableName(childTable),
							 Tables.GetTableName(linkTable),
							 Tables.GetTableName(parentTable),
							 GetExtraOrderByColumnsForSelectList(orderBy, parentTable)
						 )
					 );
			selectCommand.Parameters.AddWithValue("@parentK", parentK);
			return selectCommand;
		}

		private static string GetExtraOrderByColumnsForSelectList(KeyValuePair<object, OrderBy.OrderDirection>[] orderBy, TablesEnum parentTable)
		{
			if (orderBy == null)
			{
				return "";
			}
			StringBuilder sb = new StringBuilder();
			foreach (KeyValuePair<object, OrderBy.OrderDirection> k in orderBy)
			{
				if (Tables.GetTableEnum(k.Key) != parentTable)
					sb.Append(string.Format(", {0} AS [{0}]", GetTableAndColumnName(k.Key)));
			}
			return sb.ToString();
		}

		private static string GetTableAndColumnName(object columnEnum)
		{
			return Tables.GetTableName(Tables.GetTableEnum(columnEnum)) + "." + Tables.GetColumnName(columnEnum);
		}

		public override TablesEnum Table { get; protected set; }
		public override string TableHash { get; protected set; }
		public override IEnumerable<KeyValuePair<object, OrderBy.OrderDirection>> OrderBy { get; protected set; }
		public override Getter<T, DataRow> CreateTFromDataRow { get; protected set; }



		public override CacheKey CacheKey
		{
			get {
				List<CacheKey> namespaceCacheKeys = new List<CacheKey>();
				namespaceCacheKeys.AddRange(extraNamespaceCacheKeys);
				namespaceCacheKeys.Add(new BobChildren(parentTableName, parentTableK, Tables.GetTableName(linkTable), linkTableHash));
				
				

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
	}
}
