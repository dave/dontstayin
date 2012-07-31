using System;
using System.Collections.Generic;
using System.Text;

namespace CacheTriggers.MemcachedCommandGenerators
{
	class UpdateMemcachedCommandGenerator : MemcachedCommandGenerator
	{
		public UpdateMemcachedCommandGenerator(string tableName, string tableHash, string[] parentTableNames, string[] whereColumns)
			: base(tableName, tableHash, parentTableNames, whereColumns) { }

		public override string BuildSql
		{
			get
			{
				StringBuilder sql = new StringBuilder();
				sql.Append("SELECT Inserted.K");
				foreach (string s in parentTableNames)
				{
					sql.AppendFormat(", Inserted.{0}K as New{0}K, Deleted.{0}K as Old{0}K", s);
				}
				foreach (string s in this.whereColumns)
				{
					sql.AppendFormat(", CASE WHEN (Inserted.{0} is null AND Deleted.{0} is null) OR Inserted.{0} = Deleted.{0} THEN 0 ELSE 1 END as {0}HasChanged", s);
				}
				sql.Append(" FROM INSERTED INNER JOIN DELETED ON DELETED.K = INSERTED.K"); 
				return sql.ToString();
			}
		}



		protected override IEnumerable<Caching.Memcached.Commands.ICanBeUsedInMultiCommand> GetInvalidateCommands(int?[] row)
		{
			for (int i = 0; i < parentTableNames.Length; i++)
			{
				string parentTableName = parentTableNames[i];
				int? newK = row[i * 2 + 1] as int?;
				int? oldK = row[i * 2 + 1 + 1] as int?;
				if ((newK ?? 0) != (oldK ?? 0))
				{
					if (newK > 0) { yield return GetBobChildrenSetCommand(parentTableName, newK.Value); }
					if (oldK > 0) { yield return GetBobChildrenSetCommand(parentTableName, oldK.Value); }
				}
				for(int j=0;j<whereColumns.Length;j++)
				{
					string whereColumn = whereColumns[j];
					int indexOfFirstWhereColumn = 1 + parentTableNames.Length * 2;
					if (row[indexOfFirstWhereColumn + j] > 0)
					{
						if (newK > 0)
						{
							yield return GetBobChildFieldVersionSetCommand(parentTableName, newK.Value, whereColumn);
						}
						if (oldK > 0 && oldK != newK)
						{
							yield return GetBobChildFieldVersionSetCommand(parentTableName, oldK.Value, whereColumn);

						}
					}
				}
			}
		}

		protected override IEnumerable<Caching.Memcached.Commands.ICanBeUsedInMultiCommand> GetDeleteCommands(int?[] row)
		{
			return this.DeleteCommands((int)row[0]);
		}
	}
}
