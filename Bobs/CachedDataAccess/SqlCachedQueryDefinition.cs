using System;
using System.Collections.Generic;
using System.Text;
using Common;
using Caching;
using System.Data.SqlClient;
using Caching.CacheKeys;

namespace Bobs.CachedDataAccess
{
	public abstract class SqlCachedQueryDefinition<T> : ICachedSqlSelectDefinition<T>
	{
		protected string parentTableName;
		protected int parentTableK;

		Q whereClause;
		protected CacheKey[] extraNamespaceCacheKeys;
		public SqlCachedQueryDefinition(SqlCommand selectCommand, int parentTableK, string parentTableName, Q whereClause, params CacheKey[] extraNamespaceCacheKeys)
		{
			this.parentTableName = parentTableName;
			this.parentTableK = parentTableK;
			this.extraNamespaceCacheKeys = extraNamespaceCacheKeys;
			var qParams = new Dictionary<string, SqlParameter>();
			whereClause = whereClause ?? new Q(true);
			this.SelectCommand = selectCommand;
			this.SelectCommand.CommandText += " AND " + whereClause.ToString(ref qParams);
			this.whereClause = whereClause;
			foreach (var qParam in qParams)
			{
				this.SelectCommand.Parameters.Add(qParam.Value);
			}
		}
		public abstract TablesEnum Table { get; protected set; }
		public abstract string TableHash { get; protected set; }
		public System.Data.SqlClient.SqlCommand SelectCommand { get; private set; }
		public abstract IEnumerable<KeyValuePair<object, OrderBy.OrderDirection>> OrderBy { get; protected set; }
		public abstract Getter<T, System.Data.DataRow> CreateTFromDataRow { get; protected set; }

		public abstract CacheKey CacheKey { get; }

		protected void AddFieldVersionNamespaceKeys(List<CacheKey> namespaceCacheKeys)
		{
			foreach (Column c in whereClause.Columns())
			{
				if (!Tables.DoesColumnCauseInvalidation(c.ColumnEnum))
				{
					throw new Exception("Cannot use " + c.ColumnEnum.ToString() + " in a where clause on a CachedQuerySet as column is not marked with the CausesInvalidation extended property");
				}
				namespaceCacheKeys.Add(
					new Caching.CacheKeys.BobChildFieldVersion(parentTableName, parentTableK, c.TableName, Tables.GetTableDef(c.TableEnum).TableCacheKey , c.ColumnName)
				);
			}
		}

		protected void AddCacheKeyPartsFromSqlCommand(List<string> cacheKeyParts)
		{
			cacheKeyParts.Add(SelectCommand.CommandText);
			foreach (SqlParameter p in SelectCommand.Parameters)
			{
				cacheKeyParts.Add(p.ParameterName + "=" + p.Value.ToString());
			}
		}

		protected void AddCacheKeyPartsFromOrderBy(List<string> cacheKeyParts)
		{
			if (OrderBy != null)
			{
				foreach (var item in OrderBy)
				{
					cacheKeyParts.Add("ORDERBY");
					cacheKeyParts.Add(new Column(item.Key).InternalSqlName);
					cacheKeyParts.Add(((int)item.Value).ToString());
				}
			}
		}

	}
}
