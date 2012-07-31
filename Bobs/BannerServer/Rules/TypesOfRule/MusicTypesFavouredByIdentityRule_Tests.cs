using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Common;
using Bobs.DataHolders;
using System.Collections.ObjectModel;

namespace Bobs.BannerServer.Rules.TypesOfRule.Tests
{
	[TestFixture]
	public class MusicTypes_Tests : UnitTestUtilities.DatabaseRollbackTestClass
	{
		//[Test]
		//public void MusicTypesTest()
		//{
		//    using (new Dal.DalContext(new Dal.StandardDal()))
		//    {
		//        int usrK = 1;
		//        int musicTypeK = 1;

		//        try
		//        {
		//            Bobs.UsrMusicTypeFavourite upv1 = new Bobs.UsrMusicTypeFavourite(usrK, musicTypeK);
		//            upv1.Delete();
		//        }
		//        catch (BobNotFound) { }


		//        Banner_Tests.DeleteAllBobsBannersFromDatabase();
		//        Bobs.Banner b = Banner_Tests.AddNewBobsBanner(Bobs.Banner.Positions.Hotbox);
		//        b.IsMusicTargetted = false;
		//        b.Update();

		//        Assert.AreEqual(1, Static.Dal.GetBannersSatisfyingQueryConditions(new List<Bobs.Q> { new MusicTypesFavouredByIdentityRule(usrK).Query }).Count);
		//        b.IsMusicTargetted = true;
		//        b.Update();

		//        Cache.Instances.Bobs.FlushAll();

		//        Assert.AreEqual(0, Static.Dal.GetBannersSatisfyingQueryConditions(new List<Bobs.Q> { new MusicTypesFavouredByIdentityRule(usrK).Query }).Count);

		//        Bobs.BannerMusicType bp = new Bobs.BannerMusicType()
		//                              {
		//                                  BannerK = b.K,
		//                                  MusicTypeK = musicTypeK
		//                              };
		//        bp.Update();

		//        Assert.AreEqual(0, Static.Dal.GetBannersSatisfyingQueryConditions(new List<Bobs.Q> { new MusicTypesFavouredByIdentityRule(usrK).Query }).Count);

		//        Bobs.UsrMusicTypeFavourite upv = new Bobs.UsrMusicTypeFavourite()
		//                                 {
		//                                     UsrK = usrK,
		//                                     MusicTypeK = musicTypeK
		//                                 };
		//        upv.Update();

		//        Cache.Instances.Bobs.FlushAll();

		//        Assert.AreEqual(1, Static.Dal.GetBannersSatisfyingQueryConditions(new List<Bobs.Q> { new MusicTypesFavouredByIdentityRule(usrK).Query }).Count);
		//    }
		//}

		[Test]
		public void UsrFavoursParentMusicType_BannerTargetsChild_BannerIsServed()
		{
			Usr u = new Usr();
			u.Name = Guid.NewGuid().ToString();
			u.Update();

			MusicType parentMusicType = new MusicType() { ParentK = 1 };
			parentMusicType.Update();

			MusicType childMusicType = new MusicType()
			{
				ParentK = parentMusicType.K
			};
			childMusicType.Update();

			UsrMusicTypeFavourite umtf = new UsrMusicTypeFavourite()
			{
				MusicTypeK = parentMusicType.K,
				UsrK = u.K
			};
			umtf.Update();

			Banner b = new Banner()
			{
				IsMusicTargetted = true
			};
			b.Update();

			b.SaveMusicTargetting(new List<int>() { childMusicType.K });

			MusicTypesFavouredByIdentityRule rule = new MusicTypesFavouredByIdentityRule(new UsrIdentity(u));
			RequestRules rr = new RequestRules();
			rr.MusicTypes = rule;
			ReadOnlyCollection<BannerDataHolder> results = rr.GetBannersSatsfyingQueryConditionsInTimeslot(Timeslots.GetCurrentTimeslot());
			Assert.IsTrue(ContainsBanner(b.K, results));
		}

		private bool ContainsBanner(int k, ReadOnlyCollection<BannerDataHolder> results)
		{
			foreach (BannerDataHolder bdh in results)
			{
				if (bdh.K == k) return true;
			}
			return false;
		}

		[Test]
		public void UsrFavoursChildMusicType_BannerTargetsParent_BannerIsServed()
		{
			Usr u = new Usr();
			u.Name = Guid.NewGuid().ToString();
			u.Update();

			MusicType parentMusicType = new MusicType() { ParentK = 1 };
			parentMusicType.Update();

			MusicType childMusicType = new MusicType() { ParentK = parentMusicType.K };
			childMusicType.Update();

			UsrMusicTypeFavourite umtf = new UsrMusicTypeFavourite()
			{
				MusicTypeK = childMusicType.K,
				UsrK = u.K
			};
			umtf.Update();

			Banner b = new Banner()
			{
				IsMusicTargetted = true
			};
			b.Update();

			b.SaveMusicTargetting(new List<int>() { parentMusicType.K });

			MusicTypesFavouredByIdentityRule rule = new MusicTypesFavouredByIdentityRule(new UsrIdentity(u));
			RequestRules rr = new RequestRules() { MusicTypes = rule };
			ReadOnlyCollection<BannerDataHolder> results = rr.GetBannersSatsfyingQueryConditionsInTimeslot(Timeslots.GetCurrentTimeslot());
			Assert.IsTrue(ContainsBanner(b.K, results));
		}

		[Test]
		public void UsrFavoursOneMusicType_BannerTargetsAnother_BannerIsNotServed()
		{
			Usr u = new Usr();
			u.Name = Guid.NewGuid().ToString();
			u.Update();

			MusicType musicTypeA = new MusicType() { ParentK = 1 };
			musicTypeA.Update();

			MusicType musicTypeB = new MusicType() { ParentK = 1 };
			musicTypeB.Update();

			UsrMusicTypeFavourite umtf = new UsrMusicTypeFavourite()
			{
				MusicTypeK = musicTypeA.K,
				UsrK = u.K
			};
			umtf.Update();

			Banner b = new Banner()
			{
				IsMusicTargetted = true
			};
			b.Update();

			BannerMusicType bmt = new BannerMusicType()
			{
				BannerK = b.K,
				MusicTypeK = musicTypeB.K
			};
			bmt.Update();

			MusicTypesFavouredByIdentityRule rule = new MusicTypesFavouredByIdentityRule(new UsrIdentity(u));
			RequestRules rr = new RequestRules() { MusicTypes = rule };
			ReadOnlyCollection<BannerDataHolder> results = rr.GetBannersSatsfyingQueryConditionsInTimeslot(Timeslots.GetCurrentTimeslot());
			Assert.IsFalse(ContainsBanner(b.K, results));
		}
	}
}
