using System;
using System.Collections.Generic;
using System.Text;
using Bobs;
using Common.Clocks;
using Common;

namespace Bobs.BannerServer.Traffic
{
	public class DataDrivenTrafficShape : TrafficShape 
	{
		public override double GetNumberOfTrafficBlocksBetweenDates(DateTime startDateTime, DateTime endDateTime, Banner.Positions position)
		{
			return Caching.Instances.Main.GetWithLocalCaching(
				String.Format("GetNumberOfTrafficBlocksBetweenDates(startDateTime={0}, endDateTime={1}, position={2}, UseBannerTypeStats={3})", startDateTime, endDateTime, position, Settings.UsePerBannerTypeStats),
				() => this.GetPredictedCountOfLogItemBetweenDates(Banner.GetLogItemTypeFromPositionType(position), startDateTime, endDateTime),
				5.Minutes(),
				1.Days()
			);
		}

		public double GetPredictedCountOfLogItemBetweenDates(Log.Items logItem, DateTime startDateTime, DateTime endDateTime)
		{
			return GetPredictedCountOfLogItemBetweenDates(logItem, startDateTime, endDateTime, Time.Now);
		}
		public double GetPredictedCountOfLogItemBetweenDates(Log.Items logItem, DateTime startDateTime, DateTime endDateTime, DateTime now)
		{
			return Caching.Instances.Main.GetWithLocalCaching(
				String.Format("GetPredictedCountOfLogItemBetweenDates(logItem={0}, startDateTime={1}, endDateTime={2})", logItem, startDateTime, endDateTime),
				() =>
					{
						Dictionary<DateTime, DateTime> substitutionDates = GetTrafficExceptionDaysBetweenDates(startDateTime, endDateTime);
						List<DateTime> dates = GetListOfPastDatesToUseToModelFutureTrafficLevels(startDateTime, endDateTime, substitutionDates, now);
						Dictionary<DateTime, int> logCounts = GetCountOfLogItemForDates(logItem, dates);
						return ScaleLogCountsByDailyTrafficFluctuations(startDateTime.Date == endDateTime.Date, logCounts, dates);
					},
				5.Minutes(),
				1.Days()
			);
		}

		public Dictionary<DateTime, DateTime> GetTrafficExceptionDaysBetweenDates(DateTime startDate, DateTime endDate)
		{
			return Caching.Instances.Main.GetWithLocalCaching(
				String.Format("GetTrafficExceptionDaysBetweenDates(startDateTime={0}, endDateTime={1})", startDate, endDate),
				() =>
					{
						Query query = new Query();
						query.Columns = new ColumnSet(TrafficExceptionDay.Columns.ExceptionDate, TrafficExceptionDay.Columns.DateToUseInstead);
						query.QueryCondition = new And(
													new Q(TrafficExceptionDay.Columns.ExceptionDate, QueryOperator.GreaterThanOrEqualTo, startDate.Date),
													new Q(TrafficExceptionDay.Columns.ExceptionDate, QueryOperator.LessThanOrEqualTo, endDate.Date)
												   );
						var ts = new TrafficExceptionDaySet(query);
						var substitutionDates = new Dictionary<DateTime, DateTime>();
						foreach (var t in ts)
						{
							substitutionDates.Add(t.ExceptionDate, t.DateToUseInstead);
						}
						return substitutionDates;
					},
				5.Minutes(),
				1.Days()
			);
		}

		public Dictionary<DateTime, int> GetCountOfLogItemForDates(Log.Items logItemType, List<DateTime> dates)
		{
			return Caching.Instances.Main.GetWithLocalCaching(
				String.Format("GetCountOfLogItemForDates(logItemType={0}, dates={1})", logItemType, string.Join(",", dates.ConvertAll(d => d.ToShortDateString()).ToArray())),
				() =>
				{
					var query = new Query();
					query.Columns = new ColumnSet(Log.Columns.Count, Log.Columns.Date);
					query.QueryCondition = new And(
												new Q(Log.Columns.Item, logItemType),
												   new Or(
													   dates.ConvertAll(date => new Q(Log.Columns.Date, date.Date)).ToArray()
												   )
											   );
					var ls = new LogSet(query);
					var results = new Dictionary<DateTime, int>();
					foreach (Log l in ls)
					{
						results.Add(l.Date, l.Count);
					}
					foreach (DateTime d in dates)
					{
						if (!results.ContainsKey(d.Date))
						{
							results.Add(d.Date, 0);
						}
					}
					return results;
				},
				5.Minutes(),
				1.Days()
			);
		}

		private List<DateTime> GetListOfPastDatesToUseToModelFutureTrafficLevels(DateTime startDateTime, DateTime endDateTime, Dictionary<DateTime, DateTime> substitutionDates, DateTime now)
		{
			var list = new List<DateTime>();
			if (substitutionDates.ContainsKey(startDateTime.Date))
			{
				DateTime newStartDate = substitutionDates[startDateTime.Date].AddMilliseconds((startDateTime - startDateTime.Date).TotalMilliseconds);
				// if this substitution date is still a future date, use most recent one.
				// otherwise leave it as it is - the substitution date can be ages in the past and it should be left like that. -T.I.
				if (newStartDate.Date >= now.Date)
				{
					newStartDate = GetMostRecentDateOfSameDayOfWeek(newStartDate, now);
				}
				list.Add(newStartDate);
			}
			else
			{
				list.Add(GetMostRecentDateOfSameDayOfWeek(startDateTime, now));
			}
			for (DateTime testDate = startDateTime.Date.AddDays(1);
				testDate < endDateTime.Date;
				testDate = testDate.AddDays(1))
			{
				if (substitutionDates.ContainsKey(testDate.Date))
				{
					list.Add(substitutionDates[testDate.Date]);
				}
				else
				{
					list.Add(GetMostRecentDateOfSameDayOfWeek(testDate, now));
				}
			}
			if (substitutionDates.ContainsKey(endDateTime.Date))
			{
				DateTime newEndDate = substitutionDates[endDateTime.Date].AddMilliseconds((endDateTime - endDateTime.Date).TotalMilliseconds);
				// if this substitution date is still a future date, use most recent one.
				// otherwise leave it as it is - the substitution date can be ages in the past and it should be left like that. -T.I.
				if (newEndDate.Date >= now.Date)
				{
					newEndDate = GetMostRecentDateOfSameDayOfWeek(newEndDate, now);
				}
				list.Add(newEndDate);
			}
			else
			{
				list.Add(GetMostRecentDateOfSameDayOfWeek(endDateTime, now));
			}
			return list;
		}

		private DateTime GetMostRecentDateOfSameDayOfWeek(DateTime date, DateTime now)
		{
			while (date < now.Date.AddDays(-7))
			{
				date = date.AddDays(7);
			}
			while (date >= now.Date)
			{
				date = date.AddDays(-7);
			}
			return date;
		}

		private double ScaleLogCountsByDailyTrafficFluctuations(bool startAndEndAreSameDay, Dictionary<DateTime, int> logCounts, List<DateTime> orderedListOfPotentiallySubstitutedDates)
		{
			if (logCounts.Count == 0) return 0;

			DateTime startDate = orderedListOfPotentiallySubstitutedDates[0];
			DateTime endDate = orderedListOfPotentiallySubstitutedDates[orderedListOfPotentiallySubstitutedDates.Count - 1];

			var trafficLevelsLookupTable = new RelativeTrafficLevelsPerMinuteOfTheDayLookupTable();

			int startMinute = (int)Math.Floor((startDate - startDate.Date).TotalMinutes);
			int endMinute = (int)Math.Floor((endDate - endDate.Date).TotalMinutes);

			if (startAndEndAreSameDay)
			{
				return logCounts[startDate.Date] * ((double)trafficLevelsLookupTable.GetSumBetweenTwoMinutes(startMinute, endMinute)) / ((double)trafficLevelsLookupTable.SumOfAllMinutes);
			}
			else
			{
				int sumCountForWholeDaysInBetween = 0;
				for (int i = 1; i < orderedListOfPotentiallySubstitutedDates.Count - 1; i++)
				{
					sumCountForWholeDaysInBetween += logCounts[orderedListOfPotentiallySubstitutedDates[i]];
				}
				int totalProportionOfTrafficInFirstDay = trafficLevelsLookupTable.GetSumBetweenTwoMinutes(startMinute, trafficLevelsLookupTable.LastMinuteIndex);

				int totalProportionOfTrafficInLastDay = trafficLevelsLookupTable.GetSumBetweenTwoMinutes(0, endMinute);
				return
					(logCounts[startDate.Date] * ((double)totalProportionOfTrafficInFirstDay) / trafficLevelsLookupTable.SumOfAllMinutes) +
					sumCountForWholeDaysInBetween +
					(logCounts[endDate.Date] * ((double)totalProportionOfTrafficInLastDay) / trafficLevelsLookupTable.SumOfAllMinutes);
			}

		}
		
	}
}
