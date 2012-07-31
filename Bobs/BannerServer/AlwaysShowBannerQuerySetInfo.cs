using System;
using System.Collections.Generic;
using System.Text;
using Bobs.DataHolders;
using Bobs.BannerServer.Rules;

namespace Bobs.BannerServer
{
	public class AlwaysShowBannerQuerySetInfo : BannerQuerySetInfo
	{

		public AlwaysShowBannerQuerySetInfo(List<BannerDataHolder> banners)
			: base(banners)
		{
			List<int> indexesOfBannersThatHaveHitTheirTargetsForTheCurrentTimeslot = new List<int>();
			for (int i = 0; i < allBannersMatchingQuery.Count; i++)
			{
				BannerDataHolder banner = allBannersMatchingQuery[i];
				if (banner.AlwaysShow == true)
				{
					AddBannner(banner);
				}
			}
		}


		protected void AddBannner(BannerDataHolder banner)
		{
			Banners.Add(banner);
			TotalRemainingImpressionsForTheCurrentTimeSlot += 1;
			ActualHitsList.Add(0);
			RequiredHitsList.Add(1);
		}

		internal override bool BannerNeedsToDropAHit(int index, double hitProportion)
		{
			return false;
			
		}
	}
}
