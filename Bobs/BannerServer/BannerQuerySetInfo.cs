using System;
using System.Collections.Generic;
using System.Text;
using Bobs.DataHolders;
using Bobs.BannerServer.Rules;

namespace Bobs.BannerServer
{
	public abstract class BannerQuerySetInfo
	{
		public List<BannerDataHolder> Banners { get; private set; }
		public List<long> ActualHitsList { get; private set; }
		public List<long> RequiredHitsList { get; private set; }
		public long TotalRemainingImpressionsForTheCurrentTimeSlot { get; protected set; }
		
		public long GetRemainingHits(int i)
		{
			long actualHits = ActualHitsList[i];
			long requiredHits = RequiredHitsList[i];
			return (actualHits < requiredHits) ? (requiredHits - actualHits) : 0;
		}
		
		protected List<BannerDataHolder> allBannersMatchingQuery = null;
		protected BannerQuerySetInfo(List<BannerDataHolder> banners)
		{
			allBannersMatchingQuery = banners;
			this.ActualHitsList = new List<long>(allBannersMatchingQuery.Count);
			this.RequiredHitsList = new List<long>(allBannersMatchingQuery.Count);
			this.Banners = new List<BannerDataHolder>(allBannersMatchingQuery.Count);
			#region REMOVED DUE TO BANNER SERVER BUG
			//IndicesOfBannersThatHaveReachedTheirLimits = new List<int>();
			#endregion
			this.TotalRemainingImpressionsForTheCurrentTimeSlot = 0;
		}
		#region REMOVED DUE TO BANNER SERVER BUG
		//internal List<int> IndicesOfBannersThatHaveReachedTheirLimits { get; private set; }
		#endregion

		internal abstract bool BannerNeedsToDropAHit(int index, double hitProportion);

	}
}
