using System;
using System.Collections.Generic;
using System.Text;

namespace CacheTriggers.MemcachedCommandGenerators
{
	abstract class InsertDeleteMemcachedCommandGenerator : MemcachedCommandGenerator
	{
		public InsertDeleteMemcachedCommandGenerator(string tableName, string tableHash, string[] parentTableNames, string[] whereColumns)
			: base(tableName, tableHash, parentTableNames, whereColumns) { }
	 
		protected override IEnumerable<Caching.Memcached.Commands.ICanBeUsedInMultiCommand> GetInvalidateCommands(int?[] row)
		{
			for (int i = 0; i < parentTableNames.Length; i++)
			{
				int? parentK = row[i + 1];
				if (parentK > 0)
				{
					yield return GetBobChildrenSetCommand(parentTableNames[i], parentK.Value);
				}
			}
		}
	}
}
