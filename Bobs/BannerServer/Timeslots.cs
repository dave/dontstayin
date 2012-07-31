using System;
using System.Collections.Generic;
using System.Text;

using Caching;
using Bobs.DataHolders;
using Common;
namespace Bobs.BannerServer
{
	public static class Timeslots
	{
		public static class Today
		{
			public static Counter BannerHitsForIdentity(int bannerK, Guid id)
			{
				return new Counter(MyKey + "BannerHits(bannerK=" + bannerK + ", id=" + id.ToString() + ")");
			}

			public static Counter BannerUniqueVisitorsToday(int bannerK)
			{
				return new Counter(MyKey + "BannerUniqueVisitorsToday(bannerK=" + bannerK.ToString() + ")");
			}

			static string MyKey
			{
				get { return "DayTimeslot(day=" + Time.Today.ToString() + ")"; ; }
			}
		}
		public static Timeslot GetCurrentTimeslot()
		{
			return new Timeslot(Time.Now);
		}
	}

	
}
