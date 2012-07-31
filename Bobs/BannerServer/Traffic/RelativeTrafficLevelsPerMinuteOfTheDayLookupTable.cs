using System;
using System.Collections.Generic;
using System.Text;
using Common.Clocks;
using Common;

namespace Bobs.BannerServer.Traffic
{
	class RelativeTrafficLevelsPerMinuteOfTheDayLookupTable
	{
		int[] data;
		internal RelativeTrafficLevelsPerMinuteOfTheDayLookupTable()
		{
			data = Caching.Instances.Main.GetWithLocalCaching(
			    "GetTrafficLevelsRelativeToMinuteOfDay",
			    () =>
					{
						Bobs.Query q = new Query();
						q.Columns = new ColumnSet(Bobs.TrafficLevelRelativeToMinuteOfDay.Columns.Minute, Bobs.TrafficLevelRelativeToMinuteOfDay.Columns.TrafficLevel);
						Bobs.TrafficLevelRelativeToMinuteOfDaySet set = new TrafficLevelRelativeToMinuteOfDaySet(q);

						int[] trafficLevelAtMinute = new int[set.Count];
						foreach (Bobs.TrafficLevelRelativeToMinuteOfDay t in set)
						{
							trafficLevelAtMinute[t.Minute] = t.TrafficLevel;
						}

						return trafficLevelAtMinute;
			        },
			    Time.Minutes(5),
			    Time.Hours(24)
			);
		}

		private int? sumOfAllMinutes = null;
		internal int SumOfAllMinutes
		{
			get
			{
				if (sumOfAllMinutes == null)
				{
					sumOfAllMinutes = GetSumBetweenTwoMinutes(0, data.Length);
				}
				return sumOfAllMinutes.Value;
			}
		}

		internal int GetSumBetweenTwoMinutes(int min0Index, int min1Index)
		{
			int sum = 0;
			for (int i = min0Index; i < min1Index; i++)
			{
				sum += data[i];
			}
			return sum;
		}

		internal int LastMinuteIndex
		{
			get{
				return data.Length;
			}
		}
	}
}
