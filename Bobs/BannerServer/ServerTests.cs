using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Reflection;
using Common.Clocks;
using Common;
using Bobs.BannerServer.Traffic;
using Bobs.DataHolders;
using Common.General;
namespace Bobs.BannerServer
{
	[TestFixture]
	public class ServerTests : UnitTestUtilities.DatabaseRollbackTestClass
	{
		Common.Getter<Caching.ICacheClient> getMainCacheClient = Caching.Instances.GetMainCacheClient;
		Common.Getter<Caching.ICacheClient> getLocalCacheClient = Caching.Instances.GetLocalCacheClient;

		[TestFixtureSetUp]
		public void TestFixtureSetUp()
		{
			Caching.Instances.GetMainCacheClient = () => new Caching.DummyCache();
			Caching.Instances.GetLocalCacheClient = () => new Caching.DummyCache();
		}
		[TestFixtureTearDown]
		public void TestFixtureTearDown()
		{
			Caching.Instances.GetMainCacheClient = getMainCacheClient;
			Caching.Instances.GetLocalCacheClient = getLocalCacheClient;
		}

		[SetUp]
		public void SetUp()
		{
			base.SetUp();
			Caching.Instances.Main.FlushAll();
			Caching.Instances.MainCounterStore.FlushAll();
			Caching.Instances.LocalCache.FlushAll();
		}

		[Test]
		public void Test_DesiredBannerHitsForTimeslot_TheNumberOfHitsReturnedIsProportionalToTheTrafficShape()
		{
			using (new Context<Clock>(() => Common.Time.Clock, (c) => Common.Time.Clock = c, new ShiftedClock(DateTime.Today.AddMinutes(1))))
			using (new Context<TrafficShape>(() => Static.TrafficShape, (t) => Static.TrafficShape = t, new ConstantTrafficShape()))
			using (new Context<bool>(() => Common.Settings.SpreadBannerHits, (b) => Common.Settings.SpreadBannerHits = b, false))
			{
				int numberOfHitsPerTimeSlot = 100;

				Banner banner = new Banner()
								{
									Position = Banner.Positions.Leaderboard,
									FirstDay = Time.Today,
									LastDay = Time.Today,
									TotalRequiredImpressions = Convert.ToInt32(numberOfHitsPerTimeSlot / Timeslot.Duration.TotalDays),
								};
				banner.Update();
				Type t = banner.TotalHitsCounter.GetType();

				FieldInfo fi = t.GetField("value", BindingFlags.NonPublic | BindingFlags.Instance);
				fi.SetValue(banner.TotalHitsCounter, 0u);

				BannerDataHolder bdh = new BannerDataHolder(banner);
				BannerTimeslotInfoWithDesiredHits bannerTimeslotInfo = new BannerTimeslotInfoWithDesiredHits(bdh, Timeslots.GetCurrentTimeslot());
				Assert.AreEqual(numberOfHitsPerTimeSlot * Server.ServiceMultiplier, bannerTimeslotInfo.DesiredHits);
			}
		}

		//[Test]
		public void BannersAreServed()
		{
			Caching.Instances.Main.FlushAll();
			Caching.Instances.LocalCache.FlushAll();

			using (new Context<bool>(() => Common.Settings.SpreadBannerHits, (b) => Common.Settings.SpreadBannerHits = b, false))
			{
				int numberOfHitsPerTimeslot = 5;
				UnitTestUtilities.Sql.SqlHelper.Execute(Common.Properties.ConnectionString, "DELETE FROM Banner");
				Banner b = AddValidBanner(numberOfHitsPerTimeslot);
				Server server = new Server();
				BannerDataHolder bdh = server.GetBanner(b.Position, false, new BrowserGuidIdentity(Guid.NewGuid()), new Bobs.BannerServer.Rules.RequestRules());
				Assert.IsNotNull(bdh);
			}
		}

		[Test]
		public void BannersAreServedAndCappedOnceTheyHaveReachedTheLimitForTheirTimeslot()
		{
			Caching.Instances.Main.FlushAll();
			Caching.Instances.LocalCache.FlushAll();

			using (new Context<Clock>(() => Time.Clock, (c) => Time.Clock = c, new ShiftedClock(Time.Today)))
			using (new Context<bool>(() => Common.Settings.SpreadBannerHits, (b) => Common.Settings.SpreadBannerHits = b, false))
			using (new Context<TrafficShape>(() => Static.TrafficShape, (t) => Static.TrafficShape = t, new ConstantTrafficShape()))
			{
				int numberOfHitsPerTimeslot = 5;
				UnitTestUtilities.Sql.SqlHelper.Execute(Common.Properties.ConnectionString, "DELETE FROM Banner");
				Banner b = AddValidBanner(numberOfHitsPerTimeslot);
				Server server = new Server();
				int counter = 0;
				BannerDataHolder bdh = null;
				while (true)
				{
					Identity id = new BrowserGuidIdentity(Guid.NewGuid());
					bdh = server.GetBanner(b.Position, false, id, new Bobs.BannerServer.Rules.RequestRules());
					if (bdh == null) { break; }
					Banner banner = new Banner(bdh.K);
					banner.RegisterHit(id);
					counter++;
				}
				Assert.AreEqual((int)Math.Ceiling(numberOfHitsPerTimeslot * Server.ServiceMultiplier), counter);
			}
		}
		[Test]
		public void BannersAreServedAndFrequencyCappingWorks()
		{
			BannersAreServedAndFrequencyCapped(1);
			BannersAreServedAndFrequencyCapped(4);
		}
		public void BannersAreServedAndFrequencyCapped(int frequencyCap)
		{
			Caching.Instances.Main.FlushAll();
			Caching.Instances.LocalCache.FlushAll();

			using (new Context<Clock>(() => Time.Clock, (c) => Time.Clock = c, new ShiftedClock(Time.Today)))
			using (new Context<bool>(() => Common.Settings.SpreadBannerHits, (b) => Common.Settings.SpreadBannerHits = b, false))
			using (new Context<TrafficShape>(() => Static.TrafficShape, (t) => Static.TrafficShape = t, new ConstantTrafficShape()))
			{
				int numberOfHitsPerTimeslot = 5;
				UnitTestUtilities.Sql.SqlHelper.Execute(Common.Properties.ConnectionString, "DELETE FROM Banner");
				Banner b = AddValidBanner(numberOfHitsPerTimeslot);

				b.FrequencyCapPerIdentifierPerDay = frequencyCap;
				b.Update();
				Server server = new Server();
				int counter = 0;
				BannerDataHolder bdh = null;
				Identity id = new BrowserGuidIdentity(Guid.NewGuid());
				while (true)
				{
					bdh = server.GetBanner(b.Position, false, id, new Bobs.BannerServer.Rules.RequestRules());
					if (bdh == null) { break; }
					Banner banner = new Banner(bdh.K);
					banner.RegisterHit(id);
					counter++;
				}
				Assert.AreEqual(b.FrequencyCapPerIdentifierPerDay, counter);
			}
		}
		private static Banner AddValidBanner(int numberOfHitsPerTimeslot)
		{
			return AddValidBanner(numberOfHitsPerTimeslot, 0);
		}
		private static Banner AddValidBanner(int numberOfHitsPerTimeslot, int priority)
		{
			Banner b = new Banner()
								{
									FirstDay = Time.Today,
									LastDay = Time.Today,
									TotalRequiredImpressions = numberOfHitsPerTimeslot * 12 * 24,
									Position = Banner.Positions.Hotbox,
									StatusArtwork = true,
									StatusEnabled = true,
									StatusBooked = true,
									Priority = priority
								};
			b.Update();
			return b;
		}

		//[Test]
		public void TestAlwaysShowBannersTakePriorityOverOthers()
		{
			Assert.IsTrue(AlwaysShow(true));
			Assert.IsTrue(AlwaysShow(false));
		}

		private static bool AlwaysShow(bool alwaysShow)
		{
			int numberOfHitsPerTimeslot = 5;
			UnitTestUtilities.Sql.SqlHelper.Execute(Common.Properties.ConnectionString, "DELETE FROM Banner");

			Banner b = AddValidBanner(numberOfHitsPerTimeslot);
			b.AlwaysShow = alwaysShow;
			b.Update();

			15.Times(() => AddValidBanner(numberOfHitsPerTimeslot));

			Server server = new Server();

			bool allBannersServedWereB = true;
			for (int i = 0; i < 5; i++)
			{
				BannerDataHolder bdh = server.GetBanner(b.Position, false, new BrowserGuidIdentity(Guid.NewGuid()), new Bobs.BannerServer.Rules.RequestRules());
				if (bdh.K != b.K)
				{
					allBannersServedWereB = false;
				}
			}
			return allBannersServedWereB == alwaysShow;
		}
		[Test]
		public void AlwaysShowWithTwoBannersSplitsHitsBetweenThem()
		{
			int numberOfHitsPerTimeslot = 5;
			UnitTestUtilities.Sql.SqlHelper.Execute(Common.Properties.ConnectionString, "DELETE FROM Banner");

			Banner b0 = AddValidBanner(numberOfHitsPerTimeslot);
			b0.AlwaysShow = true;
			b0.Update();

			Banner b1 = AddValidBanner(numberOfHitsPerTimeslot);
			b1.AlwaysShow = true;
			b1.Update();

			15.Times(() => AddValidBanner(numberOfHitsPerTimeslot));

			Server server = new Server();
			List<int> Ks = new List<int>();
			int repetitons = 50;
			for (int i = 0; i < repetitons; i++)
			{
				BannerDataHolder bdh = server.GetBanner(b0.Position, false, new BrowserGuidIdentity(Guid.NewGuid()), new Bobs.BannerServer.Rules.RequestRules());
				Ks.Add(bdh.K);
			}
			Ks.ForEach(k => Assert.IsTrue(k == b0.K || k == b1.K, "One of the non always show banners was shown :("));
			Assert.AreEqual(Convert.ToDouble(Ks.FindAll(k => k == b0.K).Count), Convert.ToDouble(Ks.FindAll(k => k == b1.K).Count), repetitons * 0.4d);
		}


		[Test]
		public void AlwaysShowWithFrequencyCap()
		{
			int numberOfHitsPerTimeslot = 5;
			UnitTestUtilities.Sql.SqlHelper.Execute(Common.Properties.ConnectionString, "DELETE FROM Banner");

			Banner b = AddValidBanner(numberOfHitsPerTimeslot);
			b.AlwaysShow = true;
			b.FrequencyCapPerIdentifierPerDay = 1;
			b.Update();

			Server server = new Server();
			BrowserGuidIdentity id = new BrowserGuidIdentity(Guid.NewGuid());
			server.GetBanner(b.Position, false, id, new Bobs.BannerServer.Rules.RequestRules()).Banner.RegisterHit(id);

			Assert.IsNull(server.GetBanner(b.Position, false, id, new Bobs.BannerServer.Rules.RequestRules()));
		}


		[Test]
		public void TestVariousPrioritiedBannersAreServedInOrderOfPriority()
		{
			Caching.Instances.Main.FlushAll();
			Caching.Instances.LocalCache.FlushAll();

			using (new Context<double>(() => Server.ServiceMultiplier, d => Server.ServiceMultiplier = d, 1.0))
			using (new Context<Clock>(() => Time.Clock, (c) => Time.Clock = c, new ShiftedClock(Time.Today)))
			using (new Context<TrafficShape>(() => Static.TrafficShape, (t) => Static.TrafficShape = t, new ConstantTrafficShape()))
			{
				int numberOfHitsPerTimeslot = 1;
				UnitTestUtilities.Sql.SqlHelper.Execute(Common.Properties.ConnectionString, "delete from banner");

				Banner.Positions pos = Banner.Positions.None;

				5.Times(i => AddValidBanner(numberOfHitsPerTimeslot, i));
				5.Times(i =>
				{
					Banner b = AddValidBanner(numberOfHitsPerTimeslot, i);
					b.AlwaysShow = true;
					b.FrequencyCapPerIdentifierPerDay = 1;
					b.Update();
					if (pos == Banner.Positions.None) pos = b.Position;
				});

				BrowserGuidIdentity id = new BrowserGuidIdentity(Guid.NewGuid());
				Server server = new Server();
				int repetitons = 10;
				int previousPriorityScore = int.MaxValue;
				int previousK = int.MaxValue;
				for (int i = 0; i < repetitons; i++)
				{
					BannerDataHolder bdh = server.GetBanner(pos, false, id, new Bobs.BannerServer.Rules.RequestRules());
					bdh.Banner.RegisterHit(id);
					int priorityScore = bdh.Priority * 2 + ((bdh.AlwaysShow) ? 1 : 0);
					Assert.IsTrue(previousPriorityScore > priorityScore || previousK == bdh.K);
					previousPriorityScore = priorityScore;
					previousK = bdh.K;
				}
				Assert.AreEqual(0, previousPriorityScore);
			}
		}


		//[Test]
		public void BannerQuerySetInfosInDescendingPriorityOrderWorksAsIEnumerable()
		{
			Common.Automation.Sql.Database db = new Common.Automation.Sql.Database(Common.Properties.ConnectionString);
			db.ExecuteScalar("DELETE FROM Banner");

			10.Times(i => AddValidBanner(1, i));

			Rules.RequestRules rr = new Bobs.BannerServer.Rules.RequestRules();
			int expectedPriority = 9;
			bool firstOfThisPriority = true;
			// due to Required then Desired Sets, we expect each banner to turn up twice
			foreach (BannerQuerySetInfo bqsi in rr.BannerQuerySetInfosInDescendingPriorityOrder(Timeslots.GetCurrentTimeslot()))
			{
				Assert.AreEqual(1, bqsi.Banners.Count);
				Assert.AreEqual(expectedPriority, bqsi.Banners[0].Priority);
				if (firstOfThisPriority)
				{
					firstOfThisPriority = false;
				}
				else
				{
					firstOfThisPriority = true;
					expectedPriority--;
				}
			}
		}
	}
}
