using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Trace;
using System.Threading;


namespace UnitTestUtilities.Sql
{
	public class SqlTrace 
	{
		SqlConnectionInfo ci;
		TraceServer reader;
		long totalReads;
		bool running = false;
		string filterHostname;
		const string ProfilerApplicationName = "SQL Server Profiler";

		public SqlTrace(string filterHostname, string connectionString)
			: this(filterHostname, new SqlConnection(connectionString))
		{
		}

		public SqlTrace(string filterHostname, SqlConnection conn)
		{
			this.filterHostname = filterHostname.ToLower();

			ci = new SqlConnectionInfo(conn.DataSource);

			ClearData();
		}
		public SqlTrace(string connectionString)
			: this(new SqlConnection(connectionString))
		{
		}

		public SqlTrace(SqlConnection conn) 
			: this(System.Environment.MachineName, conn)
		{
		}


		public void ClearData()
		{
			totalReads = 0;
		}

		public void Start()
		{
			running = true;
			reader = new TraceServer();
			System.IO.FileInfo fi = new System.IO.FileInfo(@".\Sql\SqlTraceTemplate.tdf");
			reader.InitializeAsReader(ci, fi.FullName);
			Thread t = new Thread(new ThreadStart(DoTrace));
			t.Start();
		}

		private void DoTrace()
		{
			while (running && reader.Read())
			{
				if (reader["Reads"] != null && reader["EventClass"].ToString() != "Audit Logout")
				{					
					if (reader["HostName"].ToString().ToLower() == filterHostname)
					{
						totalReads += (long)reader["Reads"];
					}
				}
			}
			
			reader.Close();
		}

		public void Stop()
		{
			System.Threading.Thread.Sleep(20); //allow sql to send any remaining trace info to the SqlTrace before closing the link
			running = false;
			reader.Stop();
		}

		public long TotalReads
		{
			get
			{
				return totalReads;
			}
		}

	}
}
