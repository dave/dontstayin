using System;
using System.Collections.Generic;
using System.Text;
using Bobs.DataHolders;
using Caching;
using Common.Clocks;
using Common;

namespace Bobs.BannerServer
{
	public class Timeslot : ICacheKeyProvider
	{
		public static TimeSpan Duration = Time.Minutes(5);
		public DateTime StartTime { get; private set; }
		public DateTime EndTime { get; private set; }
		string cacheKey;
		public Timeslot(DateTime timeDuringTimeslot)
		{
			this.StartTime = GetStartOfTimeInterval(timeDuringTimeslot, Timeslot.Duration);
			this.EndTime = this.StartTime.Add(Timeslot.Duration);
			this.cacheKey = "CurrentTimeslot(timeslotStart=" + StartTime + ")";
		}
		public Timeslot GetPreviousTimeslot()
		{
			return new Timeslot(StartTime.Subtract(Timeslot.Duration));
		}
		public Timeslot GetNextTimeslot()
		{
			return new Timeslot(StartTime.Add(Timeslot.Duration));
		}

		#region GetStartOfCurrentTimeInterval
		public static DateTime GetStartOfCurrentTimeInterval(TimeSpan timeSlotDuration)
		{
			return GetStartOfTimeInterval(Time.Now, timeSlotDuration);
		}
		public static DateTime GetStartOfTimeInterval(DateTime dt, TimeSpan timeSlotDuration)
		{
			return dt.Add(-ElapsedTimeSinceStartOfCurrentTimeInterval(dt, timeSlotDuration));
		}
		#endregion
		#region RemainingTimeInCurrentInterval
		public static TimeSpan RemainingTimeInCurrentInterval(TimeSpan timeIntervalDuration)
		{
			return RemainingTimeInCurrentInterval(Time.Now, timeIntervalDuration);
		}
		public static TimeSpan RemainingTimeInCurrentInterval(DateTime dt, TimeSpan timeIntervalDuration)
		{
			return timeIntervalDuration - ElapsedTimeSinceStartOfCurrentTimeInterval(dt, timeIntervalDuration);
		}
		#endregion

		#region ElapsedTimeSinceStartOfCurrentTimeInterval
	
		public static TimeSpan ElapsedTimeSinceStartOfCurrentTimeInterval(DateTime dt, TimeSpan timeIntervalDuration)
		{
			if (timeIntervalDuration.TotalDays >= 1.0)
			{
				throw new Exception("This function only works with intervals less than a day");
			}
			return new TimeSpan((dt - dt.Date).Ticks % timeIntervalDuration.Ticks);
		}
		#endregion

	
		public double NumberOfTrafficBlocks(Banner.Positions position)
		{
			return Static.TrafficShape.GetNumberOfTrafficBlocksBetweenDates(StartTime, EndTime, position);
		}


		public Counter TotalNotShown()
		{
			return new Counter(GetCacheKey() + "TotalNotShown()");
		}
		public Counter TotalShown()
		{
			return new Counter(GetCacheKey() + "TotalShown()");
		}


		public Counter CallsToBannerServer()
		{
			return new Counter(GetCacheKey() + "CallsToBannerServer");
		}

		#region ICacheKeyProvider Members

		public string GetCacheKey()
		{
			return cacheKey;
		}

		#endregion

		public Counter BannerTimeouts()
		{
			return new Counter(GetCacheKey() + "BannerTimeouts()");
		}
	}


}
