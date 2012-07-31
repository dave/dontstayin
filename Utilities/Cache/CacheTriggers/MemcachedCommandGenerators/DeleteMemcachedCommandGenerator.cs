using System;
using System.Collections.Generic;
using System.Text;

namespace CacheTriggers.MemcachedCommandGenerators
{
	class DeleteMemcachedCommandGenerator : InsertDeleteMemcachedCommandGenerator
	{
		public DeleteMemcachedCommandGenerator(string tableName, string tableHash, string[] parentTableNames, string[] whereColumns)
					: base(tableName, tableHash, parentTableNames, whereColumns) { }
		public override string BuildSql
		{
			get
			{
				StringBuilder sql = new StringBuilder();
				sql.Append("SELECT Deleted.K");
				foreach (string s in parentTableNames)
				{
					sql.AppendFormat(", Deleted.{0}K as {0}K", s);
				}
				sql.Append(" FROM Deleted");
				return sql.ToString(); 
			}
		}



		protected override IEnumerable<Caching.Memcached.Commands.ICanBeUsedInMultiCommand> GetDeleteCommands(int?[] row)
		{
			return this.DeleteCommands((int)row[0]);
		}
	}
}
