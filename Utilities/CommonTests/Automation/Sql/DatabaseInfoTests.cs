using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Common.Automation.Sql;
namespace CommonTests.Automation.Sql
{
	[TestFixture]
	public class DatabaseAssemblyLoaderTests
	{
		[Test]
		public void ClrIsEnabledInDatabaseTest_ClrIsNotEnabled_ReturnsFalse()
        {
			
            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Common.Properties.ConnectionString);
			Database di = new Database(conn);
            SetClrEnabled(conn, false);
            Assert.IsFalse(di.ClrIsEnabledInDatabase);
        }
		[Test]
		public void ClrIsEnabledInDatabaseTest_ClrIsEnabled_Returns()
		{
			System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Common.Properties.ConnectionString);
			Database di = new Database(conn);
			di.CreateDatabaseIfDoesNotExist();
			SetClrEnabled(conn, true);
			Assert.IsTrue(di.ClrIsEnabledInDatabase);
		}
		private void SetClrEnabled(System.Data.SqlClient.SqlConnection conn, bool p)
		{
			UnitTestUtilities.Sql.SqlHelper.Execute(conn.ConnectionString, "EXEC sp_configure 'clr enabled', " + Convert.ToInt16(p) + " ; RECONFIGURE");
		}


	}
}

