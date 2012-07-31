using System;
using System.Collections.Generic;
using System.Text;
using Caching;
using Bobs.DataHolders;
using Common;

namespace Bobs.BannerServer
{
	public class BannerTimeslotInfo : ICacheKeyProvider
	{
		protected int bannerK;
		protected Timeslot timeslot;
		string cacheKey;
		
		public BannerTimeslotInfo(int bannerK, Timeslot timeslot)
		{
			this.bannerK = bannerK;
			this.timeslot = timeslot;
			this.cacheKey = "BannerTimeslotInfo(" + timeslot.GetCacheKey() + "," + this.bannerK + ")";
		}

		public Counter ActualHits
		{
			get
			{
				return new Counter(GetCacheKey() + "BannerHits");
			}
		}
		
		public Counter Considerations
		{
			get
			{
				return new Counter(GetCacheKey() + "BannerTimesConsidered");
			}
		}
		
		#region ICacheKeyProvider Members

		public string GetCacheKey()
		{
			return cacheKey;
		}

		#endregion
	}
	public class BannerTimeslotInfoWithDesiredHits : BannerTimeslotInfo
	{
		IBannerDesiredHitsRequiredInformation bdh;
		public BannerTimeslotInfoWithDesiredHits(IBannerDesiredHitsRequiredInformation bannerDataHolder, Timeslot timeslot)
			: base(bannerDataHolder.K, timeslot)
		{
			this.bdh = bannerDataHolder;
		}
		public long DesiredHits
		{
			get
			{
				string key = GetCacheKey() + "DesiredBannerHits";
				return (long)Caching.Instances.LocalCache.Get
				(
					key,
					() =>
					{
						return Caching.Instances.MainCounterStore.GetCounter
						(
							key,
							() =>
							{
								double numberOfRemainingTrafficBlocks = Static.TrafficShape.GetNumberOfTrafficBlocksBetweenDates(timeslot.StartTime, bdh.LastDay.AddDays(1), bdh.Position);
								if (numberOfRemainingTrafficBlocks == 0) { numberOfRemainingTrafficBlocks = 0.0001 * (bdh.LastDay.AddDays(1).Subtract(timeslot.StartTime)).TotalSeconds; }
								return Convert.ToUInt32(bdh.RemainingImpressionsWithMultiplier(Server.ServiceMultiplier) * (this.timeslot.NumberOfTrafficBlocks(bdh.Position) / numberOfRemainingTrafficBlocks));
							}
						);
					},
					Time.Seconds(5)
				);
			}
		}

	
		internal void NotifyAllHitsServed()
		{
			NumberOfSecondsLeftWhenAllHitsWereServed.Value = (uint) Timeslot.Duration.Subtract(Time.Now - timeslot.StartTime).Seconds;
		}
		
		public Counter NumberOfSecondsLeftWhenAllHitsWereServed
		{
			get
			{
				return new Counter(() => 0, GetCacheKey() + "NumberOfSecondsLeftWhenAllBannersWereServed", Time.Seconds(30));
			}
		}


		double? proportionToBeServed;
		public double ProportionToBeServed
		{
			get
			{
				if (proportionToBeServed == null)
				{
					string key = "proportionToBeServed";
					proportionToBeServed = CapBetweenZeroAndOne(
						Caching.Instances.Main.GetWithLocalCaching
						(
							GetCacheKey() + key,
							() =>
							{
								Timeslot previousTimeslot = timeslot.GetPreviousTimeslot();
								BannerTimeslotInfoWithDesiredHits previousInfo = new BannerTimeslotInfoWithDesiredHits(this.bdh, previousTimeslot);
								return CalculateNewServiceRate
								(
									Timeslot.Duration,
									Convert.ToInt32(previousInfo.NumberOfSecondsLeftWhenAllHitsWereServed.Value),
									Caching.Instances.Main.Get(previousInfo.GetCacheKey() + key, () => 1.0d)
								);
							},
							Time.Seconds(30),
							Time.Minutes(15)
						)
					);
				}
					
				return proportionToBeServed.Value;
			}
		}
		static double CalculateNewServiceRate(TimeSpan timeslotDuration, int numberOfSecondsRemainingSeconds, double previousRate)
		{
			double proportionOfTimeslotThatWasUnused = Convert.ToDouble(numberOfSecondsRemainingSeconds) / timeslotDuration.TotalSeconds;
			if (proportionOfTimeslotThatWasUnused == 0.0d)
			{
				return previousRate + 0.05d;
			}
			else if (proportionOfTimeslotThatWasUnused <= 0.05d)
			{
				return previousRate + 0.01d;
			}
			else if (proportionOfTimeslotThatWasUnused <= 0.10)
			{
				return previousRate - 0.03d;
			}
			else
			{
				return previousRate - 0.05d;
			}
		}
		static double CapBetweenZeroAndOne(double d)
		{
			if (d < 0) { return 0.0d; }
			if (d > 1) { return 1.0d; }
			return d;
		}

		public long RequiredHits
		{
			get
			{
				string key = GetCacheKey() + "RequiredBannerHits";
				return Caching.Instances.LocalCache.Get
				(
					key,
					() =>
					{
						return Caching.Instances.MainCounterStore.GetCounter
						(
							key,
							() =>
							{
								double totalTrafficBlocks = Static.TrafficShape.GetNumberOfTrafficBlocksBetweenDates(bdh.FirstDay, bdh.LastDay.AddDays(1), bdh.Position);
								if (totalTrafficBlocks == 0)
								{
									SpottedException.TryToSaveExceptionAndChildExceptions(new Exception(string.Format("Banner totalTrafficBlocks == 0: {0} - {1}, {2}, K={3}", bdh.FirstDay.ToString("F"), bdh.LastDay.ToString("F"), bdh.Position.ToString(), bdh.K)));
									return (uint) DesiredHits;
								}

								double proportionOfTrafficBlocksInThisTimeslotComparedToLifespanOfBanner = Static.TrafficShape.GetNumberOfTrafficBlocksBetweenDates(timeslot.StartTime, timeslot.EndTime, bdh.Position) / totalTrafficBlocks;
								long required = Convert.ToInt64(bdh.TotalRequiredImpressions * proportionOfTrafficBlocksInThisTimeslotComparedToLifespanOfBanner);
								return (uint) Math.Min(required, DesiredHits);
							}
						);
					},
					5.Seconds()
				);
			}
		}
	}
}
