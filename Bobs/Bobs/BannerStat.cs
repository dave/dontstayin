using System;
using System.Collections.Generic;
using System.Text;

using Common;

namespace Bobs
{
	#region BannerStat
	[Serializable]
	public partial class BannerStat
	{

		#region simple members
		/// <summary>
		///	Tha banner that this stat is recording
		/// </summary>
		public override int BannerK
		{
			get { return (int)this[BannerStat.Columns.BannerK]; }
			set { throw new NotImplementedException(); }
		}
		/// <summary>
		/// The date that these stats were recorded in
		/// </summary>
		public override DateTime Date
		{
			get { return (DateTime)this[BannerStat.Columns.Date]; }
			set { throw new NotImplementedException(); }
		}
		/// <summary>
		/// Number of times that the banner was displayed
		/// </summary>
		public override int Hits
		{
			get { return (int)this[BannerStat.Columns.Hits]; }
			set { throw new NotImplementedException(); }
		}
		/// <summary>
		/// Number of times that the banner was clicked on - must remind user that this may not be recorded if they don't link to the required url.
		/// </summary>
		public override int Clicks
		{
			get { return (int)this[BannerStat.Columns.Clicks]; }
			set { throw new NotImplementedException(); }
		}
		/// <summary>
		/// Number of times that the banner was displayed on a relevant page
		/// </summary>
		public override int HitsTargetted
		{
			get { return (int)this[BannerStat.Columns.HitsTargetted]; }
			set { throw new NotImplementedException(); }
		}
		/// <summary>
		/// Number of times that the banner was displayed on a place-relevant page
		/// </summary>
		public override int HitsPlaceTargetted
		{
			get { return (int)this[BannerStat.Columns.HitsPlaceTargetted]; }
			set { throw new NotImplementedException(); }
		}
		/// <summary>
		/// Number of times that the banner was displayed on a music-relevant page
		/// </summary>
		public override int HitsMusicTargetted
		{
			get { return (int)this[BannerStat.Columns.HitsMusicTargetted]; }
			set { throw new NotImplementedException(); }
		}
		/// <summary>
		/// Number of times that the banner was clicked after being displayed on a place-relevant page
		/// </summary>
		public override int ClicksPlaceTargetted
		{
			get { return (int)this[BannerStat.Columns.ClicksPlaceTargetted]; }
			set { throw new NotImplementedException(); }
		}
		/// <summary>
		/// Number of times that the banner was clicked after being displayed on a music-relevant page
		/// </summary>
		public override int ClicksMusicTargetted
		{
			get { return (int)this[BannerStat.Columns.ClicksMusicTargetted]; }
			set { throw new NotImplementedException(); }
		}

		#endregion





		public static List<BannerTotalStat> GetBannerStatTotals(params int[] bannerKs)
		{
			return GetBannerStatTotalsBetweenDates(bannerKs, System.Data.SqlTypes.SqlDateTime.MinValue.Value, System.Data.SqlTypes.SqlDateTime.MaxValue.Value);
		}

		public static List<BannerTotalStat> GetBannerStatTotalsBetweenDates(int[] bannerKs, DateTime dateTimeGreaterThanOrEqualTo, DateTime dateDimeLessThan)
		{
			Query q = new Query();
			q.Columns = new ColumnSet(Columns.BannerK);
			q.ExtraSelectElements.Add("TotalHits", "sum(isnull(" + GetColumnName(Columns.Hits) + ", 0))");
			q.ExtraSelectElements.Add("TotalClicks", "sum(isnull(" + GetColumnName(Columns.Clicks) + ", 0))");
			q.ExtraSelectElements.Add("TotalUniqueVisitors", "sum(isnull(UniqueVisitors, 0))");			

			q.QueryCondition = new And(
									new Q(Columns.BannerK, bannerKs),
									new Q(BannerStat.Columns.Date, QueryOperator.GreaterThanOrEqualTo, dateTimeGreaterThanOrEqualTo),
									new Q(BannerStat.Columns.Date, QueryOperator.LessThan, dateDimeLessThan));
			q.GroupBy = new GroupBy(Columns.BannerK);
			Dictionary<int, BannerStat> existingBannerStats = new Dictionary<int, BannerStat>();
			BannerStatSet set = new BannerStatSet(q);
			foreach (BannerStat stat in set)
			{
				existingBannerStats[stat.BannerK] = stat;
			}
			List<BannerTotalStat> results = new List<BannerTotalStat>();
			foreach (int bannerK in bannerKs)
			{
				if (existingBannerStats.ContainsKey(bannerK))
				{
					BannerStat stat = existingBannerStats[bannerK];
					results.Add(new BannerTotalStat() { 
									BannerK = bannerK, 
									Clicks = (int)stat.ExtraSelectElements["TotalClicks"], 
									Hits = (int) stat.ExtraSelectElements["TotalHits"],
									UniqueVisitors = (int)stat.ExtraSelectElements["TotalUniqueVisitors"],
								});
				}
				else
				{
					results.Add(new BannerTotalStat() { BannerK = bannerK, Clicks = 0, Hits = 0, UniqueVisitors= 0 });
				}
			}
			return results;
		}

	#endregion
		
		private static Dictionary<int, BannerTotalStat> bannerStatTemp = new Dictionary<int, BannerTotalStat>();

		private static DateTime lastSavedToDatabase = DateTime.Now;
		private static object lastSavedToDatabaseLock = new object();
		public static void Log(int bannerK, Banner.Positions position, DateTime date, int hits, int uniqueHits, int clicks)
		{
		
			if (Common.Settings.BatchBannerStatUpdates)
			{
				BannerTotalStat stat = null;
				lock (bannerStatTemp)
				{
					if (bannerStatTemp.ContainsKey(bannerK))
					{
						stat = bannerStatTemp[bannerK];
					}
					else
					{
						stat = new BannerTotalStat() { BannerK = bannerK };
						bannerStatTemp.Add(bannerK, stat);
					}
					System.Threading.Monitor.Enter(stat);
				}
				try
				{
					stat.Clicks += clicks;
					stat.Hits += hits;
					stat.UniqueVisitors += uniqueHits;
				}
				finally
				{
					System.Threading.Monitor.Exit(stat);
				}
				lock (lastSavedToDatabaseLock)
				{
					if (lastSavedToDatabase < Time.Now.Subtract(Time.Minutes(1)))
					{
						lock (bannerStatTemp)
						{
							BannerTotalStat[] bannerStats = new BannerTotalStat[bannerStatTemp.Count];
							bannerStatTemp.Values.CopyTo(bannerStats, 0);
							bannerStatTemp.Clear();
							System.Threading.Thread thread = Utilities.GetSafeThread(() => SaveStatsToDatabase(bannerStats, lastSavedToDatabase));
							thread.Start();
							lastSavedToDatabase = Time.Now;
						}
					}
				}
			}
			else
			{
				Bobs.StoredProcedures.Bobs.BannerStat.Log.Execute(bannerK, DateTime.Today, hits, uniqueHits, clicks);
			}
		}

		internal static void SaveStatsToDatabase(BannerTotalStat[] bannerStatsToBeSaved, DateTime today){
			foreach (BannerTotalStat stat in bannerStatsToBeSaved)
			{
				lock (stat)
				{
					Bobs.StoredProcedures.Bobs.BannerStat.Log.Execute(stat.BannerK, today, stat.Hits, stat.UniqueVisitors, stat.Clicks);
				}
			}
		}
	}
	public class BannerDailyStat : BannerTotalStat
	{
		public DateTime Date { get; set; }
	}

	public class BannerTotalStat
	{
		public int BannerK { get; set; }
		public int Hits { get; set; }
		public int Clicks { get; set; }
		public int UniqueVisitors { get; set; }
	}
	

}
