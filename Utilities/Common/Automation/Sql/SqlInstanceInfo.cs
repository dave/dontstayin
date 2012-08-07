using System;
using System.Collections.Generic;

using System.Text;
using System.Data.SqlClient;

namespace Common.Automation.Sql
{
	public class SqlInstanceInfo
	{
		readonly public string ServerName = null;
		readonly public  string InstanceName = null;
		public SqlInstanceInfo(SqlConnection conn)
		{
			ServerName = GetNameOfServerFromConnection(conn);
			InstanceName = conn.DataSource;
		}

		static string GetNameOfServerFromConnection(SqlConnection conn)
        {
            if (conn.DataSource.IndexOf("\\") > -1)
            {
                return conn.DataSource.Substring(0, conn.DataSource.IndexOf("\\"));
            }
            else
            {
                return conn.DataSource;
            }
        }
	}
}
