using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Bobs;
using Bobs.BannerServer;
using Common.Clocks;

namespace SpottedLibrary.Admin.BannerImpressionStats
{
	public class BannerImpressionStatsService
	{
		internal DataTable GetImpressionStatsBetween(DateTime firstDate, DateTime secondDate)
		{
			firstDate = firstDate.Date;
			secondDate = secondDate.Date;

			// get traffic data
			Bobs.BannerServer.Traffic.DataDrivenTrafficShape ts = new Bobs.BannerServer.Traffic.DataDrivenTrafficShape();

			List<Log> actualHitLogCounts = GetActualLogCounts(firstDate, secondDate).ToList();
			BannerSet actualBannerImpressions = GetActualBannerImpressions(firstDate, secondDate);

			DataTable dt = CreateStatsDataTable();

			// foreach day between first and second date
			for (DateTime date = firstDate; date <= secondDate; date = date.AddDays(1))
			{
				// foreach banner position
				foreach (Banner.Positions position in Enum.GetValues(typeof(Banner.Positions)))
				{
					if (position == Banner.Positions.None) continue;

					Log.Items actualHitLogItem = Banner.GetLogItemTypeFromPositionType(position);

					DataRow dr = dt.NewRow();
					dr["Position"] = position.ToString();
					dr["Date"] = date;

					// get calculated page hit stats for this date
					using (new Common.General.Context<Clock>(() => Common.Time.Clock, (c) => Common.Time.Clock = c, new FixedClock(date)))
					{
						dr["ExpectedPageHits"] = ts.GetPredictedCountOfLogItemBetweenDates(actualHitLogItem, date, date.AddDays(1));
					}

					// get actual page hit stats for this date
					Log actualHitLog = actualHitLogCounts.Find(l => l.Date == date && l.ItemType == actualHitLogItem);
					if (actualHitLog != null)
					{
						dr["ActualPageHits"] = actualHitLog.Count;
					}

					// get total required impressions for all banners of this position for this day
					dr["RequiredImpressions"] = Banner.GetPredictedRequiredImpressions(date, position);

					// get actual impressions for all banners of this position for this day
					Banner banner = actualBannerImpressions.ToList().Find(b => b.Position == position && ((DateTime)b.ExtraSelectElements["BannerStatDate"]).CompareTo(date) == 0);
					if (banner != null)
					{
						dr["ActualImpressions"] = banner.ExtraSelectElements["SumHits"];
					}

					dt.Rows.Add(dr);
				}
			}

			return dt;
		}

		private long GetTotalRequiredImpressions(Banner.Positions position, DateTime date)
		{
			return Caching.Instances.Main.Get("RequiredImpressionsForBannerImpressionStats" + date.ToString("yyyymmdd") + position.ToString(), () =>
			{
				long desiredHits = 0;
				using (new Common.General.Context<Clock>(() => Common.Time.Clock, (c) => Common.Time.Clock = c, new FixedClock(date)))
				{
					Query q = new Query();
					q.QueryCondition = new And(Banner.IsLiveQ, new Q(Banner.Columns.Position, position));
					BannerSet bs = new BannerSet(q);

					foreach (Banner b in bs)
					{
						for (Timeslot timeslot = new Timeslot(date); timeslot.StartTime < date.AddDays(1); timeslot = timeslot.GetNextTimeslot())
						{
							BannerTimeslotInfoWithDesiredHits bannerTimeslotInfo = new BannerTimeslotInfoWithDesiredHits(b, timeslot);
							desiredHits += bannerTimeslotInfo.DesiredHits;
						}
					}
				}
				return desiredHits;
				}
			);
		}

		private BannerSet GetActualBannerImpressions(DateTime firstDate, DateTime secondDate)
		{
			Query q = new Query();
			q.Columns = new ColumnSet(Banner.Columns.Position);
			q.ExtraSelectElements.Add("SumHits", "Sum([Hits])"); // need not worry about nulls
			q.ExtraSelectElements.Add("BannerStatDate", "[Date]"); // need not worry about nulls
			q.TableElement = new Join(Banner.Columns.K, BannerStat.Columns.BannerK);
			q.QueryCondition = (new And(
									 new Q(BannerStat.Columns.Date, QueryOperator.GreaterThanOrEqualTo, firstDate),
									 new Q(BannerStat.Columns.Date, QueryOperator.LessThanOrEqualTo, secondDate)));
			q.GroupBy = new GroupBy(new GroupBy(Banner.Columns.Position), new GroupBy(BannerStat.Columns.Date));
			return new BannerSet(q);
		}

		private DataTable CreateStatsDataTable()
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("Date", typeof(DateTime));
			dt.Columns.Add("Position", typeof(string));
			dt.Columns.Add("ExpectedPageHits", typeof(int));
			dt.Columns.Add("ActualPageHits", typeof(int));
			dt.Columns.Add("RequiredImpressions", typeof(long));
			dt.Columns.Add("ActualImpressions", typeof(long));
			return dt;
		}

		private LogSet GetActualLogCounts(DateTime firstDate, DateTime secondDate)
		{
			List<Log.Items> logItems = new List<Log.Items>();

			// foreach banner position
			foreach (Banner.Positions position in Enum.GetValues(typeof(Banner.Positions)))
			{
				if (position == Banner.Positions.None) continue;

				Log.Items logItem = Banner.GetLogItemTypeFromPositionType(position);
				if (!logItems.Contains(logItem))
				{
					logItems.Add(logItem);
				}
			}
			return GetLogCounts(firstDate, secondDate, logItems);
		}

		private LogSet GetLogCounts(DateTime firstDate, DateTime secondDate, List<Log.Items> logItems)
		{
			Query q = new Query();
			q.QueryCondition = new And(
									   new Q(Log.Columns.Date, QueryOperator.GreaterThanOrEqualTo, firstDate),
									   new Q(Log.Columns.Date, QueryOperator.LessThanOrEqualTo, secondDate),
									   new Or(logItems.ConvertAll(l => new Q(Log.Columns.Item, l)).ToArray()));
			return new LogSet(q);
		}
	}
}
