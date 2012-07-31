using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using Bobs;
using Bobs.BannerServer;
using Common.Clocks;
using Common;
namespace Spotted.Admin
{
	public partial class BannerHitViewer : AdminUserControl
	{
		public struct TimeslotInfo
		{
			public DateTime TimeslotStart { get; set; }
			public long Required { get; set; }
			public long Actual { get; set; }
			public long NotShown { get; set; }
			public long Timeouts { get; set; }
			public long CallsToBannerServer { get; set; }

		}

		public struct BannerInfo
		{
			public int K { get; set; }
			public string Name { get; set; }
			public Banner.Positions Position { get; set; }
			public string Url { get; set; }
			public bool IsPlaceTargetted { get; set; }
			public bool IsMusicTargetted { get; set; }
			public int UniqueHitsSoFar { get; set; }
			public int HitsRequired { get; set; }
			public long HitsSoFar { get; set; }
			public int HitsSoFarDb { get; set; }
			public int ClicksSoFar { get; set; }
			public int ClicksSoFarDb { get; set; }
			public float Lifespan { get; set; }
			public float ElapsedLifespan { get; set; }
			public bool IsTotalHitRateBad { get; set; }
			public float HitsRequiredCurrentTimeslot { get; set; }
			public float HitsDesiredCurrentTimeslot { get; set; }
			public float ActualHitsCurrentTimeslot { get; set; }
			public float HitsRequiredPreviousTimeslot { get; set; }
			public float HitsDesiredPreviousTimeslot { get; set; }
			public float ActualHitsPreviousTimeslot { get; set; }
			public bool IsPreviousTimeslotBad { get; set; }
			public bool IsCurrentTimeslotBad { get; set; }
			public long TimesConsidered { get; set; }
			public long TimesConsideredPrev { get; set; }
			public double Rate { get; set; }
			public double RatePrev { get; set; }
			public uint RemainingSecondsWhenPreviousBannerCompleted { get; set; }
			public double CreditsPerClick { get; set; }

		}

		protected void Page_Load(object sender, EventArgs e)
		{
			Query q = Bobs.BannerServer.Server.GetQueryForBannersActiveBetweenTwoTimes(DateTime.Today, DateTime.Now);
			q.OrderBy = new OrderBy(new OrderBy(Banner.Columns.Position), new OrderBy(Banner.Columns.K));
			BannerSet bannerSet = new BannerSet(q);
			List<Banner> banners = bannerSet.ToList();

			Timeslot timeslotMinus0 = Timeslots.GetCurrentTimeslot();
			Timeslot timeslotMinus1 = timeslotMinus0.GetPreviousTimeslot();

			if (!IsPostBack){
				this.TimeslotStart.Items.Add(timeslotMinus0.StartTime.ToString());
				this.TimeslotEnd.Items.Add(timeslotMinus0.StartTime.ToString());
				DateTime date = timeslotMinus0.StartTime.Date.AddDays(-7);
				while (date < timeslotMinus0.StartTime)
				{
					this.TimeslotStart.Items.Insert(1, date.ToString());
					this.TimeslotEnd.Items.Insert(1, date.ToString());
					date = date.AddHours(8);
				}
				this.TimeslotStart.SelectedIndex = (int) global::Caching.Instances.MainCounterStore.GetCounter("BannerHitViewer.TimeslotStart.SelectedValue for Usr " + Usr.Current.K, () => 0u);
				this.TimeslotEnd.SelectedIndex = (int)global::Caching.Instances.MainCounterStore.GetCounter("BannerHitViewer.TimeslotEnd.SelectedValue for Usr " + Usr.Current.K, () => 1u);
			}

			global::Caching.Instances.MainCounterStore.SetCounter("BannerHitViewer.TimeslotStart.SelectedValue for Usr " + Usr.Current.K, (uint) this.TimeslotStart.SelectedIndex);
			global::Caching.Instances.MainCounterStore.SetCounter("BannerHitViewer.TimeslotEnd.SelectedValue for Usr " + Usr.Current.K, (uint) this.TimeslotEnd.SelectedIndex);


			List<BannerInfo> bannerData = new List<BannerInfo>();
			List<BannerTotalStat> bannerTotalStat = BannerStat.GetBannerStatTotals(banners.ConvertAll(b=>b.K).ToArray());
			

			for (int i=0;i<banners.Count;i++)
			{
				Banner banner = banners[i];
				BannerTimeslotInfoWithDesiredHits bannerTimeslotInfo = new BannerTimeslotInfoWithDesiredHits(banner, timeslotMinus0);
				BannerTimeslotInfoWithDesiredHits previousBannerTimeslotInfo = new BannerTimeslotInfoWithDesiredHits(banner, timeslotMinus1);
				long actualHitsPreviousTimeslot = previousBannerTimeslotInfo.ActualHits.Value;
				long hitsRequiredPreviousTimeslot = previousBannerTimeslotInfo.RequiredHits;
				long hitsDesiredPreviousTimeslot = previousBannerTimeslotInfo.DesiredHits;
				long actualHitsCurrentTimeslot = bannerTimeslotInfo.ActualHits.Value;
				long hitsRequiredCurrentTimeslot = bannerTimeslotInfo.RequiredHits;
				long hitsDesiredCurrentTimeslot = bannerTimeslotInfo.DesiredHits;

				float elapsedLifespan = (float) (Time.Now - banner.FirstDay).TotalDays;
				float lifespan = (float) (banner.LastDay - banner.FirstDay).TotalDays + 1;

				BannerInfo bannerInfo = new BannerInfo()
					{
						K = banner.K,
						Name = banner.Name,
						Position = banner.Position,
						Url = banner.Url(),
						IsMusicTargetted = banner.IsMusicTargetted,
						IsPlaceTargetted = banner.IsPlaceTargetted,
						HitsRequired = banner.TotalRequiredImpressions,
						HitsSoFar = banner.TotalHits,
						UniqueHitsSoFar = bannerTotalStat[i].UniqueVisitors,
						HitsSoFarDb = bannerTotalStat[i].Hits,
						ClicksSoFar = (int)banner.TotalClicks,
						ClicksSoFarDb = bannerTotalStat[i].Clicks,
						ElapsedLifespan = elapsedLifespan,
						Lifespan = lifespan,
						IsTotalHitRateBad = elapsedLifespan / lifespan > 0.5 && ((float)banner.TotalHits / (float)banner.TotalRequiredImpressions) < (0.9 * elapsedLifespan / lifespan),
						ActualHitsCurrentTimeslot = actualHitsCurrentTimeslot,
						HitsRequiredCurrentTimeslot = hitsRequiredCurrentTimeslot,
						HitsDesiredCurrentTimeslot = hitsDesiredCurrentTimeslot,
						ActualHitsPreviousTimeslot = actualHitsPreviousTimeslot,
						HitsRequiredPreviousTimeslot = hitsRequiredPreviousTimeslot,
						HitsDesiredPreviousTimeslot = hitsDesiredPreviousTimeslot,
						IsPreviousTimeslotBad = actualHitsPreviousTimeslot < 0.9 * hitsDesiredPreviousTimeslot,
						IsCurrentTimeslotBad = actualHitsCurrentTimeslot < 0.9 * (Timeslot.ElapsedTimeSinceStartOfCurrentTimeInterval(Time.Now, Timeslot.Duration).TotalMinutes / Timeslot.Duration.TotalMinutes) * hitsDesiredCurrentTimeslot,
						TimesConsidered = bannerTimeslotInfo.Considerations.Value,
						TimesConsideredPrev = previousBannerTimeslotInfo.Considerations.Value,
						Rate = bannerTimeslotInfo.ProportionToBeServed,
						RatePrev = previousBannerTimeslotInfo.ProportionToBeServed,
						RemainingSecondsWhenPreviousBannerCompleted = bannerTimeslotInfo.NumberOfSecondsLeftWhenAllHitsWereServed.Value,
						CreditsPerClick =  ((double)banner.PriceCredits * ((double)banner.TotalHits / (double)banner.TotalRequiredImpressions)) / (double)banner.TotalClicks
					};
				bannerData.Add(bannerInfo);
			}

			this.BannerInfoRepeater.DataSource = bannerData;
			this.BannerInfoRepeater.DataBind();

			

			List<TimeslotInfo> timeslotInfos = new List<TimeslotInfo>();
			Timeslot first;
			try
			{
				first = new Timeslot(DateTime.Parse(TimeslotStart.SelectedValue));
			}
			catch
			{
				first = timeslotMinus0;
			}
			Timeslot last;
			try
			{
				last = new Timeslot(DateTime.Parse(TimeslotStart.SelectedValue));
			}
			catch
			{
				last = timeslotMinus1;
			}
			
			if (first.StartTime < last.StartTime)
			{
				Timeslot temp = first;
				first = last;
				last = temp;
			}

			List<Timeslot> timeslots = new List<Timeslot>();
			while(first.StartTime >= last.StartTime)
			{
				timeslots.Add(first);
				first = first.GetPreviousTimeslot();
			}
			foreach (Timeslot ts in timeslots)
			{
				long actualHits = 0;
				long requiredHits = 0;

				if (ts.StartTime != timeslotMinus0.StartTime)
				{
					actualHits = global::Caching.Instances.Main.Get(ts.GetCacheKey() + "actualHitsForBannerHitViewer", () =>
						{
							long hits = 0;
							banners.ForEach(b =>
							{
								BannerTimeslotInfo info = new BannerTimeslotInfo(b.K, ts);
								hits += info.ActualHits.Value; 
							});
							return hits;
						}
					);
					requiredHits = global::Caching.Instances.Main.Get(ts.GetCacheKey() + "requiredHitsForBannerHitViewer", () =>
						{
							long hits = 0;
							banners.ForEach(b => {
								BannerTimeslotInfoWithDesiredHits info = new BannerTimeslotInfoWithDesiredHits(b, ts);
								hits += info.DesiredHits; 
							});
							return hits;
						}
					);
				}
				else
				{

					banners.ForEach(b => {
						BannerTimeslotInfo info = new BannerTimeslotInfo(b.K, ts); 
						actualHits += info.ActualHits.Value; 
					});
					banners.ForEach(b => {
						BannerTimeslotInfoWithDesiredHits info = new BannerTimeslotInfoWithDesiredHits(b, ts); 
						requiredHits += info.DesiredHits;
					});
				}
				

				
				
				TimeslotInfo ti = new TimeslotInfo()
								  {
									  TimeslotStart = ts.StartTime,
									  NotShown = ts.TotalNotShown().Value,
									  Actual = actualHits,
									  Required = requiredHits,
									  Timeouts = ts.BannerTimeouts().Value,
									  CallsToBannerServer = ts.CallsToBannerServer().Value

								  };

				
				timeslotInfos.Add(ti);
			}


			this.TimeslotInfoRepeater.DataSource = timeslotInfos;
			this.TimeslotInfoRepeater.DataBind();
			
			
		}

	}
}
