using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Common;
namespace Bobs.BannerServer.Traffic
{
	[TestFixture]
	public class DataDrivenTrafficShapeTests
	{
		[Test]
		public void GetNumberOfTrafficBlocksBetweenDates_Test()
		{
			DataDrivenTrafficShape ts = new DataDrivenTrafficShape();
			DateTime startDate = Time.Today;
			DateTime endDate = Time.Today.AddDays(19);
			double numberOfTrafficBlocksBetweenStartAndEnd = ts.GetNumberOfTrafficBlocksBetweenDates(startDate, endDate, Banner.Positions.Leaderboard);
			Random r = new Random();
			double summedNumberOfTrafficBlocks = 0.0d;
			while (startDate < endDate)
			{
				DateTime nextDate = startDate.AddMinutes(r.Next((int)(endDate - startDate).TotalMinutes) + 1);
				summedNumberOfTrafficBlocks += ts.GetNumberOfTrafficBlocksBetweenDates(startDate, nextDate, Banner.Positions.Leaderboard);
				startDate = nextDate;
			}
			Assert.AreEqual(numberOfTrafficBlocksBetweenStartAndEnd, summedNumberOfTrafficBlocks, 0.05);
		}

	}
}
