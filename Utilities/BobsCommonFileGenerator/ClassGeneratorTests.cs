using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Configuration;
namespace BobsCommonFileGenerator.Tests
{
	[TestFixture]
	public class ClassGeneratorTests
	{
		string exampleTableHash;
		[TestFixtureSetUp]
		public void TestFixtureSetUp()
		{
			try
			{
				ExecuteSQL(System.IO.File.ReadAllText(@"TestFiles\TestDatabaseDropScript.sql"), MasterDatabaseConnectionString);
			}
			catch (Exception e)
			{

			}

			ExecuteSQL(System.IO.File.ReadAllText(@"TestFiles\TestDatabaseCreateScript.sql"), MasterDatabaseConnectionString);
			ExecuteSQL(System.IO.File.ReadAllText(@"TestFiles\TestDatabaseTableCreateScript.sql"), TestDatabaseConnectionString);
			
			ExecuteSQL(System.IO.File.ReadAllText(@"TestFiles\TestDatabaseProcedureCreateScript.sql"), TestDatabaseConnectionString);
		}

		[TestFixtureTearDown]
		public void TestFixtureTearDown()
		{
            ExecuteSQL(System.IO.File.ReadAllText(@"TestFiles\TestDatabaseDropScript.sql"), MasterDatabaseConnectionString);
		}




		[Test]
		public void ProducedFileIsSameAsExpected()
		{
			string exampleOutputContent = System.IO.File.ReadAllText(@"TestFiles\ExampleOutput.cs");
			Common.Automation.Sql.Database database = new Common.Automation.Sql.Database(TestDatabaseConnectionString);
			exampleOutputContent = exampleOutputContent.Replace("{TableCacheKey}", database.Tables[0].Hash);
			
			System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(TestDatabaseConnectionString);

			ClassGenerator cg = new ClassGenerator(conn.ConnectionString);
			string output = cg.GetPartialClassesFromDatabase();
			System.IO.File.WriteAllText(@"TestFiles\ActualOutput.cs", output);
			output = System.IO.File.ReadAllText(@"TestFiles\ActualOutput.cs");

			
			Assert.AreEqual(exampleOutputContent, output);
		}



		void ExecuteSQL(string SQL, string connectionString)
		{
			System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(connectionString);
			System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(SQL, conn);
			try
			{
				conn.Open();
				cmd.ExecuteNonQuery();
			}
			finally
			{
				conn.Close();
				conn.Dispose();
			}
		}

        string MasterDatabaseConnectionString
        {
            get
            {
                return String.Format(DefaultDatabaseConnectionString, "Master");
            }
        }
        string TestDatabaseConnectionString
        {
            get
            {
                return String.Format(DefaultDatabaseConnectionString, "CreateCommonDotCsProjectTestDatabase");
            }

        }
        string DefaultDatabaseConnectionString
        {
            get
            {
                using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(Common.Properties.ConnectionString))
                {
                    return Common.Properties.ConnectionString.Replace(conn.Database, "{0}");
                }

            }
        }
	}
}
