using System;
using System.Collections.Generic;
using System.Text;
using Caching.Memcached.Commands;
using System.Data.SqlClient;
using Caching;
using Caching.Memcached;
using Microsoft.SqlServer.Server;
using System.Diagnostics;

namespace CacheTriggers.MemcachedCommandGenerators
{
	abstract class MemcachedCommandGenerator
	{
		
		protected string tableName;
		protected string tableHash;
		protected string[] parentTableNames;
		protected string[] whereColumns;
		public MemcachedCommandGenerator(string tableName, string tableHash, string[] parentTableNames, string[] whereColumns)
		{
			this.tableName = tableName;
			this.tableHash = tableHash;
			this.parentTableNames = parentTableNames;
			this.whereColumns = whereColumns;


		}
	

		public IEnumerable<ICanBeUsedInMultiCommand> Commands
		{
			get
			{
				var rows = ReadTablesToArrays(BuildSql);
				foreach (var row in rows)
				{
					foreach (ICanBeUsedInMultiCommand command in GetDeleteCommands(row))
					{
						yield return command;
					}
					foreach (ICanBeUsedInMultiCommand command in GetInvalidateCommands(row))
					{
						yield return command;
					}
				}
			}
		}

		protected abstract IEnumerable<ICanBeUsedInMultiCommand> GetInvalidateCommands(int?[] row);
		protected abstract IEnumerable<ICanBeUsedInMultiCommand> GetDeleteCommands(int?[] row);
		public abstract string BuildSql { get; }
		protected static List<int?[]> ReadTablesToArrays(String sql)
		{
			List<int?[]> rows = new List<int?[]>();
			using (SqlConnection connection = new SqlConnection(Common.Properties.ConnectionString))
			{
				connection.Open();
				using (SqlCommand command = new SqlCommand(sql, connection))
				{
					SqlDataReader reader = command.ExecuteReader();
					while (reader.Read())
					{
						int?[] dataRow = new int?[reader.FieldCount];
						for (int i = 0; i < reader.FieldCount; i++)
						{
							dataRow[i] = reader[i] as int?;
						}
						rows.Add(dataRow);
					}
					reader.Close();
				}
				connection.Close();
			}
			return rows;
		}
		protected Set GetBobChildrenSetCommand(string parentTableName, int parentTableK)
		{
			CacheKey cacheKey = new Caching.CacheKeys.BobChildren(parentTableName, parentTableK, tableName, tableHash);
			Key key = new Key(cacheKey + "Main", Caching.Instances.Hasher);
			Set set = new Set(key, Guid.NewGuid().ToString(), DateTime.MaxValue);
			return set;
		}
		protected Set GetBobChildFieldVersionSetCommand(string parentTableName, int parentTableK, string fieldName)
		{
			CacheKey cacheKey = new Caching.CacheKeys.BobChildFieldVersion(parentTableName, parentTableK, tableName, tableHash, fieldName);
			//SqlContext.Pipe.Send(cacheKey.ToString());
			Key key = new Key(cacheKey + "Main", Caching.Instances.Hasher);
			Set set = new Set(key, Guid.NewGuid().ToString(), DateTime.MaxValue);
			return set;
		}
		protected IEnumerable<ICanBeUsedInMultiCommand> DeleteCommands(int k)
		{
			yield return new BlockingDelete(new Key(Cache.GetBobsCacheKey(tableName, k.ToString()) + "Main", Caching.Instances.Hasher));
			yield return new BlockingDelete(new Key(new Caching.CacheKeys.BobCacheKey(tableName, k, tableHash) + "Main", Caching.Instances.Hasher));
		}
		
		
	}
}
