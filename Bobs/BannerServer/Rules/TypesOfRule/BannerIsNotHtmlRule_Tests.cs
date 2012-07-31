using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using UnitTestUtilities;
namespace Bobs.BannerServer.Rules.TypesOfRule.Tests
{
	[TestFixture]
	public class BannerIsHtmlRule_Tests : DatabaseRollbackTestClass
	{
		[Test]
		public void TargettingBitInfoType_Usr_Test()
		{
			Common.Automation.Sql.Database db = new Common.Automation.Sql.Database(Common.Properties.ConnectionString);
			db.ExecuteScalar("DELETE FROM Banner");

			Bobs.Banner b = new Bobs.Banner()
							{
								DisplayType = Bobs.Banner.DisplayTypes.CustomHtml
							};
			b.Update();

			RequestRules rr = new RequestRules();
			Assert.AreEqual(1, rr.GetBannersSatsfyingQueryConditionsInTimeslot(Timeslots.GetCurrentTimeslot()).Count);

			BannerIsNotHtmlRule r = new BannerIsNotHtmlRule();
			rr.Add(r);
			Assert.AreEqual(0, rr.GetBannersSatsfyingQueryConditionsInTimeslot(Timeslots.GetCurrentTimeslot()).Count);
		}
	}
}
