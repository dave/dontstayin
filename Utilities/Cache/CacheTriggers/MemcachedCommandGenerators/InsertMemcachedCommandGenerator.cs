using System;
using System.Collections.Generic;
using System.Text;

namespace CacheTriggers.MemcachedCommandGenerators
{
	class InsertMemcachedCommandGenerator : InsertDeleteMemcachedCommandGenerator
	{
		public InsertMemcachedCommandGenerator(string tableName, string tableHash, string[] parentTableNames, string[] whereColumns)
					: base(tableName, tableHash, parentTableNames, whereColumns) { }
		public override string BuildSql
		{
			get
			{
				StringBuilder sql = new StringBuilder();
				sql.Append("SELECT Inserted.K");
				foreach (string s in parentTableNames)
				{
					sql.AppendFormat(", Inserted.{0}K as {0}K", s);
				}
				sql.Append(" FROM Inserted" );
				return sql.ToString(); 
			}
		}



		protected override IEnumerable<Caching.Memcached.Commands.ICanBeUsedInMultiCommand> GetDeleteCommands(int?[] row)
		{
			//nothing to be deleted on inserts
			yield break;
		}
	}
}
