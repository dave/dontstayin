using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Web;
using System.Transactions;
using Microsoft.SqlServer.Server;
using Caching;
using Caching.Memcached.Commands;
using Caching.Memcached;
using CacheTriggers.MemcachedCommandGenerators;
using System.Diagnostics;


namespace CacheTriggers
{
	public class Trigger
	{
		[Conditional("DEBUG")]
		static void WriteKeysToPipeWhenInDebug(ICanBeUsedInMultiCommand[] commands)
		{
			foreach (var command in commands)
			{
				SqlContext.Pipe.Send(command.Key.Value);
			}

		}
		internal static void UpdateMemcachedOnTableChange(string tableName, string tableHash, string[] parentTableNames, string[] whereClauseColumns)
		{
			try
			{
				MemcachedCommandGenerator commandGenerator = null;
				switch (SqlContext.TriggerContext.TriggerAction)
				{
					case TriggerAction.Update: commandGenerator = new UpdateMemcachedCommandGenerator(tableName, tableHash, parentTableNames, whereClauseColumns); break;
					case TriggerAction.Delete: commandGenerator = new DeleteMemcachedCommandGenerator(tableName, tableHash, parentTableNames, whereClauseColumns); break;
					case TriggerAction.Insert: commandGenerator = new InsertMemcachedCommandGenerator(tableName, tableHash, parentTableNames, whereClauseColumns); break;
					default: throw new NotImplementedException();
				}
				CommandExecuter ex = new CommandExecuter(Caching.Instances.GetInstances());

				var commands = commandGenerator.Commands.ToList().ToArray();
				WriteKeysToPipeWhenInDebug(commands);
				ex.ExecuteCommands(commands);
			}
			catch (Exception ex)
			{
				LogException(ex);
				SqlContext.Pipe.Send("Caught: " + ex.ToString());
				try
				{
					// Get the current transaction and roll it back.
					Transaction.Current.Rollback();
				}
				catch (Exception)
				{
					// Catch the expected exception. (http://msdn2.microsoft.com/en-us/library/ms131085.aspx)
				}
			}
		}

		private static void LogException(Exception ex)
		{
			string connectionString = Common.Properties.BuildConnectionString(Common.Properties.SqlServer, Common.Properties.SqlDatabase);
			
			using (new TransactionScope(TransactionScopeOption.Suppress))
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();
					using (SqlCommand cmd = new SqlCommand(String.Format("INSERT INTO SpottedException (Message, ExceptionDateTime, Source) VALUES ('{0}', GETDATE(), 'CLR Trigger')", ex.Message.Replace("'", "''")), conn))
					{
						cmd.ExecuteNonQuery();
					}
				}
			}
		}
	}
	public static class Extentions
	{
		public static System.Collections.Generic.List<T> ToList<T>(this IEnumerable<T> enumerable)
		{
			return new System.Collections.Generic.List<T>(enumerable);
		}
	}
	
}
