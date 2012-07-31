using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
namespace Bobs.BannerServer
{
	[TestFixture]
	public class TimeslotTests : UnitTestUtilities.DatabaseRollbackTestClass
	{
		[Test]
		public void TestThatDifferentTimeslotsHaveDifferentStoredValues()
		{
			Caching.Instances.Main.FlushAll();
			Timeslot currentTimeslot = Timeslots.GetCurrentTimeslot();
			Timeslot nextTimeslot = currentTimeslot.GetNextTimeslot();
			BannerTimeslotInfo bannerTimeslotInfo = new BannerTimeslotInfo(-1, currentTimeslot);
			bannerTimeslotInfo.ActualHits.Increment();
			bannerTimeslotInfo.ActualHits.Increment();
			bannerTimeslotInfo.ActualHits.Increment();
			BannerTimeslotInfo nextBannerTimeslotInfo = new BannerTimeslotInfo(-1, nextTimeslot);
			nextBannerTimeslotInfo.ActualHits.Increment();
			Assert.AreEqual(3, bannerTimeslotInfo.ActualHits.Value);
			Assert.AreEqual(1, nextBannerTimeslotInfo.ActualHits.Value);

			Assert.AreNotEqual(bannerTimeslotInfo.ActualHits.Value, nextBannerTimeslotInfo.ActualHits.Value);

		}
	}
}
