using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlTypes;

namespace Bobs
{
	#region stored procedures
	public static partial class StoredProcedures
	{
		public static partial class BannerServer
		{
			public static partial class Banner
			{
				public static partial class RecalculateTrafficLevelRelativeToMinuteOfDay
				{
					public static void Execute(DateTime? useWeekStartingFromDate)
					{
						ExecuteNonQuery(useWeekStartingFromDate);
					}
				 
				}
			}
		}
		public static partial class Bobs
		{
			public static partial class BannerStat
			{
				public static partial class Log
				{
					public static void Execute(int bannerK, DateTime? date, int hits, int uniqueVisitors, int clicks)
					{
						ExecuteNonQuery(bannerK, date, hits, uniqueVisitors, clicks);
					}
				 
				}
			}
			public static partial class Flyer
			{
				public static partial class LogView
				{
					public static void Execute(int flyerK)
					{
						ExecuteNonQuery(flyerK);
					}

				}
				public static partial class LogClick
				{
					public static void Execute(int flyerK)
					{
						ExecuteNonQuery(flyerK);
					}

				}
				public static partial class LogSend
				{
					public static void Execute(int flyerK)
					{
						ExecuteNonQuery(flyerK);
					}

				}
				public static partial class LogUnsubscribe
				{
					public static void Execute(int flyerK)
					{
						ExecuteNonQuery(flyerK);
					}

				}
			}
		}
	}
	
	#endregion
 
}
