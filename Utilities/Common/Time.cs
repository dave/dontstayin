using System;
using System.Collections.Generic;

using System.Text;
using Common.Clocks;

namespace Common
{
	public static class Time
	{

		public static DateTime Now
		{
			get
			{
				return clock.Now;
			}
		}
		public static DateTime Today
		{
			get
			{
				return clock.Now.Date;
			}

		}
		private static Clock clock = new SystemClock();

		public static Clock Clock
		{
			get
			{
				return clock;
			}
			set
			{
				lock (clock)
				{
					clock = value;
				}
			}
		}

		public static readonly DateTime SqlMinDate = new DateTime(1900,1,1);






		public static TimeSpan Hours(int n)
		{
			return new TimeSpan(n, 0, 0);
		}
		public static TimeSpan Minutes(int n)
		{
			return new TimeSpan(0, n, 0);
		}
		public static TimeSpan Seconds(int n)
		{
			return new TimeSpan(0, 0, n);
		}

	}
}
