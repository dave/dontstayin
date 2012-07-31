using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Bobs.Tests
{
	[TestFixture]
	public class Q_Tests : UnitTestUtilities.DatabaseRollbackTestClass
	{
		string value1 = "value1";
		string value2 = "value2";
		[Test]
		public void Query_DifferentValues_GetCacheKeysDifferent()
		{
			Assert.AreNotEqual(new Q(Bobs.Banner.Columns.AdminNote, value1).GetCacheKey(), new Q(Bobs.Banner.Columns.AdminNote, value2).GetCacheKey());
			Assert.AreNotEqual(new Q(Bobs.Banner.Columns.AdminNote, new int[] { 1, 2, 3 }).GetCacheKey(), new Q(Bobs.Banner.Columns.AdminNote, new int[] { 1, 2, 3, 4 }).GetCacheKey());

		}


		[Test]
		public void Query_DifferentColumns_GetCacheKeysDifferent()
		{
			Bobs.Q q1 = new Q(Bobs.Banner.Columns.AdminNote, value1);
			Bobs.Q q2 = new Q(Bobs.Banner.Columns.BrandK, value1);

			Assert.AreNotEqual(q1.GetCacheKey(), q2.GetCacheKey());
		}

		[Test]
		public void Query_SameOperatorsAndValues_GetCacheKeysSame()
		{
			Assert.AreEqual(new Q(Bobs.Banner.Columns.AdminNote, new int[] { 1, 2, 3 }).GetCacheKey(), new Q(Bobs.Banner.Columns.AdminNote, new int[] { 1, 2, 3 }).GetCacheKey());
			Assert.AreEqual(new Q(Bobs.Banner.Columns.AdminNote, new int[] { 1, 2, 3 }).GetCacheKey(), new Q(Bobs.Banner.Columns.AdminNote, new int[] { 3, 2, 1 }).GetCacheKey());
			Assert.AreEqual(new Q(Bobs.Banner.Columns.AdminNote, value1).GetCacheKey(), new Q(Bobs.Banner.Columns.AdminNote, value1).GetCacheKey());
		}
		[Test]
		public void CanGetColumnNamesOutOfAQ()
		{
			List<Column> columnsInEnabledQuery = new List<Column>(Photo.EnabledQueryCondition.Columns());
			Assert.AreEqual(1, columnsInEnabledQuery.Count);

			Assert.AreEqual(new Column(Photo.Columns.Status).InternalSqlName, columnsInEnabledQuery[0].InternalSqlName);
		}


		[Test]
		public void WhereColumnInStringArrayDoesntCauseFormattingExceptions()
		{
			string emailWithOpenBrace = "OpenBrace{@hotmail.com";
			Usr u1 = new Usr {Email = emailWithOpenBrace};
			u1.Update();
			string emailWithCloseBrace = "CloseBrace}@hotmail.com";
			Usr u2 = new Usr {Email = emailWithCloseBrace};
			u2.Update();

			UsrSet us = new UsrSet(new Query(new Q(Usr.Columns.Email, new[] {emailWithOpenBrace, emailWithCloseBrace})){OrderBy = new OrderBy(Usr.Columns.K)});
			Assert.AreEqual(u1.K, us[0].K);
			Assert.AreEqual(u2.K, us[1].K);
		}
	}
}
