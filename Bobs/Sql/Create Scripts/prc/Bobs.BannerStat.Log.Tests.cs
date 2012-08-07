using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using UnitTestUtilities.Sql;
namespace Bobs.Sql.Create_Scripts.prc
{
	[TestFixture]
	public class Bobs_Banner_LogImpressionsTests : UnitTestUtilities.DatabaseRollbackTestClass
	{
		[Test]
		public void NewEntriesAreInsertedTest()
		{
			SqlHelper.Execute(Common.Properties.ConnectionString, "DELETE FROM [" + Bobs.Tables.GetTableName(Bobs.TablesEnum.BannerStat) + "]");
			Bobs.StoredProcedures.Bobs.BannerStat.Log.Execute(1, DateTime.Parse("31 Dec 1999"), 1, 1, 1);
			Assert.AreEqual(1, SqlHelper.ExecuteScalar(
									Common.Properties.ConnectionString,
									"SELECT Hits FROM " + Bobs.Tables.GetTableName(Bobs.TablesEnum.BannerStat) + " WHERE BannerK = 1 AND Date = '31 Dec 1999'"));
		}

		[Test]
		public void ExistingEntriesAreUpdatedTest()
		{
			NewEntriesAreInsertedTest();
			Bobs.StoredProcedures.Bobs.BannerStat.Log.Execute(1, DateTime.Parse("31 Dec 1999"), 1, 1, 1);
			Assert.AreEqual(2, SqlHelper.ExecuteScalar(
									Common.Properties.ConnectionString,
									"SELECT Hits FROM " + Bobs.Tables.GetTableName(Bobs.TablesEnum.BannerStat) + " WHERE BannerK = 1 AND Date = '31 Dec 1999'"));

		}
	}
}
