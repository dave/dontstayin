using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using NUnit.Framework;

namespace UnitTestUtilities.Sql.Tests
{
	[TestFixture]
	public class SqlTrace_Tests
	{
		[Test]
		public void TestTotalReads()
		{
			SqlConnection conn = new SqlConnection(Common.Properties.ConnectionString);
			SqlTrace trace = new SqlTrace(System.Environment.MachineName, conn);
			SqlCommand cmd = new SqlCommand("SELECT * FROM master..sysdatabases", conn);

			Assert.AreEqual(trace.TotalReads, 0);
			trace.Start();

			try
			{
				conn.Open();
				cmd.ExecuteNonQuery();
			}
			finally
			{
				conn.Close();
			}

			trace.Stop();


			Assert.AreNotEqual(trace.TotalReads, 0);
		}
		[Test]
		public void TraceIsReusable()
		{
			SqlConnection conn = new SqlConnection(Common.Properties.ConnectionString);
			SqlTrace trace = new SqlTrace(System.Environment.MachineName,conn);
			SqlCommand cmd = new SqlCommand("SELECT * FROM master..sysdatabases", conn);

			Assert.AreEqual(trace.TotalReads, 0);
			trace.Start();

			try
			{
				conn.Open();
				cmd.ExecuteNonQuery();
			}
			finally
			{
				conn.Close();
			}

			trace.Stop();


			Assert.AreNotEqual(trace.TotalReads, 0);
			trace.ClearData();
			Assert.AreEqual(trace.TotalReads, 0);
			trace.Start();

			try
			{
				conn.Open();
				cmd.ExecuteNonQuery();
			}
			finally
			{
				conn.Close();
			}

			trace.Stop();


			Assert.AreNotEqual(trace.TotalReads, 0);

		}


		
	}
}
