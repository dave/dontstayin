using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlScriptRunner
{
    internal  class Global
    {
        private static object _globalLock = new object();
        private static string _connectionString;

        internal static string ConnectionString
        {
            get
            {
                return _connectionString;
            }
            set
            {
                lock ((_globalLock))
                {
                    _connectionString = value;
                }
            }
        }

		internal static System.Data.SqlClient.SqlConnection GetConnection(){
			return new System.Data.SqlClient.SqlConnection (ConnectionString);
		}

        internal static DateTime ToolStarted { get; set; }
    }
}
