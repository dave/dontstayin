using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Common;
using Common.Collections;
using Caching;
using System.Linq;

namespace Bobs.CachedDataAccess
{
	
	public class CachedSqlSelect<T>: IPagedDataService<T>, IIndexable<T> where T : IBob
	{
		ICachedSqlSelectDefinition<T> definition;
		string tableName;
		string orderByClause;
		private string extraSelectsForOrderBy;
		public CachedSqlSelect(ICachedSqlSelectDefinition<T> definition)
		{
			this.definition = definition;
			this.tableName = Tables.GetTableName(definition.Table);
			if (definition.OrderBy != null)
			{
				this.orderByClause = "ORDER BY " + String.Join(", ", definition.OrderBy.ToList().ConvertAll(pair =>
				( Tables.GetTableName(Tables.GetTableEnum(pair.Key)) == tableName ? Tables.GetColumnName(pair.Key) :
					"[" + GetTableAndColumnName(pair.Key) + "]")

					+ (pair.Value == OrderBy.OrderDirection.Ascending ? " ASC" : " DESC")).ToArray());

				this.extraSelectsForOrderBy = GetExtraOrderByColumnsForSelectList(definition.OrderBy, definition.Table);
			}
			else
			{
				this.orderByClause = "ORDER BY [" + this.tableName + "].K";
			}
		}

		private static string GetExtraOrderByColumnsForSelectList(IEnumerable<KeyValuePair<object, OrderBy.OrderDirection>> orderBy, TablesEnum parentTable)
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

		int[] GetKs(SqlCommand inCmd, List<string> cacheKeyParts)
		{
			cacheKeyParts.Add(definition.CacheKey.ToString());

			return Caching.Instances.Main.Get
			(
				new CacheKey(CacheKeyPrefix.CachedQuerySet, cacheKeyParts.ToArray()),
				() =>
				{
					var cmd = DeepCopySqlCommand(inCmd);
					
					cmd.CommandText = String.Format("{0} {2}", cmd.CommandText, tableName, orderByClause);
					
					using (SqlConnection conn = new SqlConnection(Common.Properties.ConnectionString))
					{
						cmd.Connection = conn;
						conn.Open();
						using (SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
						{
							List<int> Ks = new List<int>();
							while (reader.Read())
							{
								Ks.Add((int)reader[0]);
							}
							return Ks.ToArray();
						}
					}
				},
				null
			);
		}
		T[] GetItems(int[] Ks)
		{
			if (Ks.Length == 0)
			{
				return new T[] { };
			}
			else
			{
				T[] items = Caching.Instances.Main.MultiGet<T, int>
				(
					Ks,
					new Getter<string, int>(i => new Caching.CacheKeys.BobCacheKey(tableName, i, definition.TableHash)),
					keys => Get(keys),
					DateTime.MaxValue
				);

				return items;
			}
		}


		T[] Get(int[] keys)
		{
			string sql;
			string childTable = GetChildTableIfOrderByContainsOne(definition.OrderBy);
			if (childTable != null)
			{
				sql = String.Format(
					"SELECT {0}.* {4} FROM {0} WITH (NOLOCK) INNER JOIN {3} WITH (NOLOCK) ON [{0}].K = [{3}].{0}K WHERE [{0}].K IN ({1}) {2}",
					tableName,
					String.Join(",", keys.ConvertAll(k => k.ToString())),
					orderByClause,
					childTable,
					extraSelectsForOrderBy
					);
			}
			else
			{
				sql = String.Format(
					"SELECT * FROM {0} WITH (NOLOCK) WHERE K IN ({1}) {2}",
					tableName,
					String.Join(",", keys.ConvertAll(k => k.ToString())),
					orderByClause
					);
			}

			using (SqlConnection conn = new SqlConnection(Common.Properties.ConnectionString))
			using (SqlCommand cmd = new SqlCommand(sql, conn))
			{
				conn.Open();
				using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
				{
					DataTable dt = new DataTable();
					adapter.Fill(dt);
					List<T> items = new List<T>();
					foreach (DataRow row in dt.Rows)
					{
						items.Add(definition.CreateTFromDataRow(row));
					}

					return items.ToArray();
				}
			}
		}

		private string GetChildTableIfOrderByContainsOne(IEnumerable<KeyValuePair<object, OrderBy.OrderDirection>> orderBy)
		{
			if (orderBy == null) return null;
			foreach (var kvp in orderBy)
			{
				if (Tables.GetTableEnum(kvp.Key) != definition.Table)
				{
					return Tables.GetTableName(Tables.GetTableEnum(kvp.Key));
				}
			}
			return null;
		}

		[NonSerialized]
		int? cachedCount = null;
		public int Count 
		{ 
			get 
			{
				if (cachedCount == null)
				{
					cachedCount = GetKs(definition.SelectCommand, new List<string>()).Length;
				} 
				return cachedCount.Value;
			} 
		}
		[NonSerialized]
		T[] cachedAllItems = null;
		public T[] AllItems()
		{
			if (cachedAllItems == null)
			{
				int[] Ks = GetKs(definition.SelectCommand, new List<string>());
				cachedAllItems = GetItems(Ks);
			}
			return cachedAllItems;
		}
		[NonSerialized]
		Dictionary<KeyValuePair<int, int>, T[]> cachedPages = new Dictionary<KeyValuePair<int, int>, T[]>();
		public T[] Page(int pageNumber, int pageSize)
		{
			
			var localCacheKey = new KeyValuePair<int, int>(pageNumber, pageSize);

			if (!cachedPages.ContainsKey(localCacheKey))
			{
				var cmd = DeepCopySqlCommand(definition.SelectCommand);
				cmd.CommandText = String.Format("SELECT *, ROW_NUMBER() OVER ({0}) AS RowNumber FROM ({1}) as {2}", orderByClause, cmd.CommandText, tableName);
				cmd.CommandText = String.Format("SELECT * FROM ({0}) as {1} WHERE RowNumber BETWEEN @MinRow AND @MaxRow", cmd.CommandText, tableName);
				cmd.Parameters.AddWithValue("@MinRow", (pageNumber - 1) * pageSize + 1);
				cmd.Parameters.AddWithValue("@MaxRow", pageNumber * pageSize);

				int[] Ks = GetKs(cmd, new List<string>() { "pageNumber=" + pageNumber, "pageSize=" + pageSize });
				cachedPages[localCacheKey] = GetItems(Ks);

			}
			return cachedPages[localCacheKey];
		}

		private static SqlCommand DeepCopySqlCommand(SqlCommand baseSqlCmd)
		{
			var cmd = new SqlCommand(baseSqlCmd.CommandText, baseSqlCmd.Connection);
			foreach (SqlParameter param in baseSqlCmd.Parameters)
			{
				cmd.Parameters.AddWithValue(param.ParameterName, param.Value);
			}
			return cmd;
		}

		

		public T this[int index]
		{
			get { return this.AllItems()[index]; }
		}

		public IEnumerator<T> GetEnumerator()
		{
			return new List<T>(this.AllItems()).GetEnumerator();
		}


		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.AllItems().GetEnumerator();
		}
	}
}
