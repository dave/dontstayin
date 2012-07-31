using System;
using System.Collections.Generic;
using System.Text;
using Bobs.DataHolders;
using Bobs.BannerServer.Rules;

namespace Bobs.BannerServer
{
	public class TargetHitsBannerQuerySetInfo :BannerQuerySetInfo
	{
		Func<BannerTimeslotInfoWithDesiredHits, long> getTargetHits;
		public List<BannerTimeslotInfoWithDesiredHits> BannerTimeslotInfos { get; private set; }
		public TargetHitsBannerQuerySetInfo(List<BannerDataHolder> banners, Timeslot currentTimeslot, Func<BannerTimeslotInfoWithDesiredHits, long> getTargetHits)
			: base(banners)
		{
			this.getTargetHits = getTargetHits;
			this.BannerTimeslotInfos = new List<BannerTimeslotInfoWithDesiredHits>(allBannersMatchingQuery.Count);
			for (int i = 0; i < allBannersMatchingQuery.Count; i++)
			{
				BannerDataHolder banner = allBannersMatchingQuery[i];
				if (banner.AlwaysShow == false)
				{
					AddBannner(banner, currentTimeslot, i);
				}
			}
		}

		void AddBannner(BannerDataHolder banner, Timeslot currentTimeslot, int i)
		{
			BannerTimeslotInfoWithDesiredHits info = new BannerTimeslotInfoWithDesiredHits(banner, currentTimeslot);
			info.Considerations.Increment();
			long actualHits = info.ActualHits.Value;
			long targetHits = (long)Math.Ceiling(getTargetHits(info) * Server.ServiceMultiplier);
			long remainingHits = (actualHits < targetHits) ? (targetHits - actualHits) : 0;
			if (remainingHits == 0)
			{
				if (Common.Settings.SpreadBannerHits)
				{
					info.NotifyAllHitsServed();
					#region REMOVED DUE TO BANNER SERVER BUG
					//IndicesOfBannersThatHaveReachedTheirLimits.Add(i);
					#endregion
				}
			}
			else
			{
				Banners.Add(banner);
				BannerTimeslotInfos.Add(info);
				TotalRemainingImpressionsForTheCurrentTimeSlot += remainingHits;
				ActualHitsList.Add(actualHits);
				RequiredHitsList.Add(targetHits);
			}
		}


	
		internal override bool BannerNeedsToDropAHit(int index, double hitProportion)
		{
			return (Common.Settings.SpreadBannerHits && this.BannerTimeslotInfos[index].ProportionToBeServed < hitProportion);
		}

		
	}
}
