using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace UnitTestUtilities.Sql
{
	public class SqlHelper
	{
		public static void ResetSqlServerForTesting(string connString)
		{
			Execute(connString, "checkpoint; dbcc dropCleanBuffers; DBCC FREEPROCCACHE;");
		}

		public static int Execute(string connString, string p)
		{
			return GetDatabase(connString).Execute(p);
		}
		public static object ExecuteScalar(string connString, string p)
		{
			return GetDatabase(connString).ExecuteScalar(p);
		}
		public static System.Data.DataSet ExecuteAdapter(string connString, string p)
		{
			return GetDatabase(connString).ExecuteAdapter(p);
		}
		private static Common.Automation.Sql.Database GetDatabase(string connectionString)
		{
			return new Common.Automation.Sql.Database(new SqlConnection(connectionString));
		}

        public static void ClearTable(string connStr, string tableName)
        {
            Execute(connStr, "DELETE FROM " + tableName);
        }
	}
}
