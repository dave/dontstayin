using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using UnitTestUtilities;
namespace Bobs.BannerServer.Rules.TypesOfRule.Tests
{
    [TestFixture]
    public class IdentityPropertyRules_Tests : DatabaseRollbackTestClass
    {
		[Test]
		public void TargettingBitInfoType_Usr_Test1()
		{
			Common.Automation.Sql.Database db = new Common.Automation.Sql.Database(Common.Properties.ConnectionString);
			db.ExecuteScalar("DELETE FROM Banner");

			Bobs.Usr u = new Bobs.Usr()
							   {
								   IsMale = false,
								   IsFemale = true
							   };
			u.Update();

			UsrIdentity id = new UsrIdentity(u);

			Bobs.Banner b = new Bobs.Banner();
			b.Update();

			IdentityPropertyRules t = new IdentityPropertyRules(id);
			RequestRules rr = new RequestRules();
			rr.Add(t);
			Assert.AreEqual(1, rr.GetBannersSatsfyingQueryConditionsInTimeslot(Timeslots.GetCurrentTimeslot()).Count);
		}

		[Test]
		public void TargettingBitInfoType_Usr_Test2()
		{
			Common.Automation.Sql.Database db = new Common.Automation.Sql.Database(Common.Properties.ConnectionString);
			db.ExecuteScalar("DELETE FROM Banner");
			Bobs.Usr u = new Bobs.Usr()
							   {
								   IsMale = true,
								   IsFemale = false
							   };
			u.Update();
			UsrIdentity id = new UsrIdentity(u);
			Bobs.Banner b = new Bobs.Banner();
			b.SetTargettingProperty(Banner.TargettingProperty.Gender_Unknown, true);
			b.SetTargettingProperty(Banner.TargettingProperty.Gender_Female, true);
			b.SetTargettingProperty(Banner.TargettingProperty.Gender_Male, true);
			b.Update();

			IdentityPropertyRules t = new IdentityPropertyRules(id);
			Assert.AreEqual(0, new BannerSet(new Query(t.Q)).Count);
		}

		[Test]
		public void TargettingBitInfoType_Demographics_Test1()
		{
			Common.Automation.Sql.Database db = new Common.Automation.Sql.Database(Common.Properties.ConnectionString);
			db.ExecuteScalar("DELETE FROM Banner");

			Bobs.Usr u = new Bobs.Usr();
			u.Update();

			Bobs.Banner b = new Bobs.Banner();
			b.Update();

			IdentityPropertyRules t = new IdentityPropertyRules(new UsrIdentity(u));
			RequestRules rr = new RequestRules();
			rr.Add(t);
			Assert.AreEqual(1, rr.GetBannersSatsfyingQueryConditionsInTimeslot(Timeslots.GetCurrentTimeslot()).Count);

			b.SetTargettingProperty(Bobs.Banner.TargettingProperty.Employment_4, true);
			b.Update();

			Assert.AreEqual(1, rr.GetBannersSatsfyingQueryConditionsInTimeslot(Timeslots.GetCurrentTimeslot()).Count);
		}

		[Test]
		public void TargettingBitInfoType_Demographics_Test2()
		{
			Common.Automation.Sql.Database db = new Common.Automation.Sql.Database(Common.Properties.ConnectionString);
			db.ExecuteScalar("DELETE FROM Banner");

			Bobs.Usr u = new Bobs.Usr();
			u.Guid = Guid.NewGuid();
			u.Update();

			Bobs.Banner b = new Bobs.Banner();
			b.SetTargettingProperty(Bobs.Banner.TargettingProperty.Employment_4, true);
			b.Update();

			Bobs.Demographics d = new Bobs.Demographics()
								  {
									  Guid = u.Guid,
									  Employment = 4
								  };
			d.Update();

			IdentityPropertyRules t = new IdentityPropertyRules(new UsrIdentity(u));
			RequestRules rr = new RequestRules();
			rr.Add(t);
			Assert.AreEqual(0, rr.GetBannersSatsfyingQueryConditionsInTimeslot(Timeslots.GetCurrentTimeslot()).Count);
		}


		[Test]
		public void TargettingBitInfoType_Demographics_SpendCds1()
		{
			Common.Automation.Sql.Database db = new Common.Automation.Sql.Database(Common.Properties.ConnectionString);
			db.ExecuteScalar("DELETE FROM Banner");

			Bobs.Usr u = new Bobs.Usr();
			u.Update();

			Bobs.Banner b = new Bobs.Banner();
			b.Update();

			IdentityPropertyRules t = new IdentityPropertyRules(new UsrIdentity(u));
			RequestRules rr = new RequestRules();
			rr.Add(t);
			Assert.AreEqual(1, rr.GetBannersSatsfyingQueryConditionsInTimeslot(Timeslots.GetCurrentTimeslot()).Count);

			b.SetTargettingProperty(Bobs.Banner.TargettingProperty.SpendMusicCd_MoreThanZero, true);
			b.Update();

			Assert.AreEqual(1, rr.GetBannersSatsfyingQueryConditionsInTimeslot(Timeslots.GetCurrentTimeslot()).Count);
		}

		[Test]
		public void TargettingBitInfoType_Demographics_SpendCds2()
		{
			Common.Automation.Sql.Database db = new Common.Automation.Sql.Database(Common.Properties.ConnectionString);
			db.ExecuteScalar("DELETE FROM Banner");

			Bobs.Usr u = new Bobs.Usr();
			u.Guid = Guid.NewGuid();
			u.Update();

			Bobs.Banner b = new Bobs.Banner();
			b.SetTargettingProperty(Bobs.Banner.TargettingProperty.SpendMusicCd_MoreThanZero, true);
			b.Update();

			Bobs.Demographics d = new Bobs.Demographics()
			{
				Guid = u.Guid,
				SpendMusicCd = 1 //"Nothing"
			};
			d.Update();

			IdentityPropertyRules t = new IdentityPropertyRules(new UsrIdentity(u));
			RequestRules rr = new RequestRules();
			rr.Add(t);
			Assert.AreEqual(1, rr.GetBannersSatsfyingQueryConditionsInTimeslot(Timeslots.GetCurrentTimeslot()).Count);
		}

		[Test]
		public void TargettingBitInfoType_Demographics_SpendCds3()
		{
			Common.Automation.Sql.Database db = new Common.Automation.Sql.Database(Common.Properties.ConnectionString);
			db.ExecuteScalar("DELETE FROM Banner");

			Bobs.Usr u = new Bobs.Usr();
			u.Guid = Guid.NewGuid();
			u.Update();

			Bobs.Banner b = new Bobs.Banner();
			b.SetTargettingProperty(Bobs.Banner.TargettingProperty.SpendMusicCd_MoreThanZero, true);
			b.Update();

			Bobs.Demographics d = new Bobs.Demographics()
			{
				Guid = u.Guid,
				SpendMusicCd = 7
			};
			d.Update();

			IdentityPropertyRules t = new IdentityPropertyRules(new UsrIdentity(u));
			RequestRules rr = new RequestRules();
			rr.Add(t);
			Assert.AreEqual(0, rr.GetBannersSatsfyingQueryConditionsInTimeslot(Timeslots.GetCurrentTimeslot()).Count);
		}
    }
}
