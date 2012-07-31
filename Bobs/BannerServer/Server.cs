using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Bobs.BannerServer.Rules;
using Bobs.BannerServer.Rules.TypesOfRule;
using Caching;
using System.Web;
using Bobs.DataHolders;
using Common.Clocks;
using Common;


namespace Bobs.BannerServer
{
	public class Server
	{
		internal static double ServiceMultiplier = 1.01;

		public BannerDataHolder GetBanner(Bobs.Banner.Positions position, bool isHttpsContext, Identity identity, RequestRules requestRules)
		{
			List<BannerDataHolder> banners = GetBanners(position, isHttpsContext, 1, identity, requestRules);
			return banners[0];
		}

		public List<BannerDataHolder> GetBanners(Bobs.Banner.Positions position, bool isHttpsContext, int numberOfBannersToGet, Identity id, RequestRules rules)
		{
			Timeslot currentTimeslot = Timeslots.GetCurrentTimeslot();
			currentTimeslot.CallsToBannerServer().Increment();

			SetUpRequestRules(position, isHttpsContext, id, rules);

			List<BannerDataHolder> chosenBanners = new List<BannerDataHolder>(numberOfBannersToGet);

			foreach (BannerQuerySetInfo bannerQuerySetInfo in rules.BannerQuerySetInfosInDescendingPriorityOrder(currentTimeslot))
			{
				AddBanners(numberOfBannersToGet, id, bannerQuerySetInfo, chosenBanners);
				if (chosenBanners.Count == chosenBanners.Capacity) { return chosenBanners; }
			}
			#region REMOVED DUE TO BANNER SERVER BUG
			//rules.RemoveBannersWhichHaveReachedTargetsFromCachedQuery(currentTimeslot);
			#endregion
			PadOutChosenBannersWithNulls(chosenBanners);
			return chosenBanners;
		}

		private void PadOutChosenBannersWithNulls(List<BannerDataHolder> chosenBanners)
		{
			while (chosenBanners.Count < chosenBanners.Capacity) chosenBanners.Add(null);
		}


		private static void SetUpRequestRules(Bobs.Banner.Positions position, bool isHttpsContext, Identity id, RequestRules requestRules)
		{
			requestRules.Add(new BannerPositionRule(position));
			requestRules.Add(id.IdentityRules);
			requestRules.Add(new Rules.TypesOfRule.BannerIsLiveRule());
			if (isHttpsContext)
			{
				requestRules.Add(new BannerIsNotHtmlRule());
			}
		}

	
		

		private void AddBanners(int numberOfBannersToGet, Identity id, BannerQuerySetInfo info, List<BannerDataHolder> chosenBanners)
		{
			for (int i = 0; i < numberOfBannersToGet - chosenBanners.Count; i++)
			{
				BannerDataHolder banner = PickBannerK(id, info);
				if (banner != null)
				{
					chosenBanners.Add(banner);
				}
			}
		}

		private BannerDataHolder PickBannerK(Identity id, BannerQuerySetInfo info)
		{
			int? indexOfBannerToReturn = null;
			double target = ThreadSafeRandom.NextDouble() * info.TotalRemainingImpressionsForTheCurrentTimeSlot;
			Stack<int> suitableBannersIndexes = new Stack<int>();
			for (int i = 0; i < info.Banners.Count && indexOfBannerToReturn == null; i++)
			{
				long remainingHits = info.GetRemainingHits(i);
				if (remainingHits == 0) { continue; }
				target -= remainingHits;
				suitableBannersIndexes.Push(i);
				if (target <= 0)
				{
					indexOfBannerToReturn = ReturnFirstBannerThatNeedsAHit(id, suitableBannersIndexes, info);
				}
			}
			if (indexOfBannerToReturn.HasValue)
			{
				return info.Banners[indexOfBannerToReturn.Value];
			}
			else
			{
				return null;
			}
		}


	

		private int? ReturnFirstBannerThatNeedsAHit(Identity id, Stack<int> suitableBanners,  BannerQuerySetInfo info)
		{
			double hitProportion = Common.ThreadSafeRandom.NextDouble();
			while (suitableBanners.Count > 0)
			{
				int index = suitableBanners.Peek();
				BannerDataHolder banner = info.Banners[index];
				
				if (BannerHasBeenShownToAParticularIdentityTooManyTimesToday(banner, id) || info.BannerNeedsToDropAHit(index, hitProportion))
				{
					suitableBanners.Pop();
					continue;
				}
				return suitableBanners.Peek();
			}
			return null;
		}

			
		
		bool BannerHasBeenShownToAParticularIdentityTooManyTimesToday(BannerDataHolder bdh, Identity id)
		{
			if (bdh.FrequencyCapPerIdentifierPerDay > -1)
			{
				long numberOfTimesBannerHasBeenShownToUser = Timeslots.Today.BannerHitsForIdentity(bdh.K, id.Guid).Value;
				if (numberOfTimesBannerHasBeenShownToUser >= bdh.FrequencyCapPerIdentifierPerDay)
				{
					return true;
				}
			}
			return false;
		}





		public static Query GetQueryForBannersActiveBetweenTwoTimes(DateTime time1, DateTime time2)
		{
			return new Query(
				new And(
					new Q(Banner.Columns.StatusArtwork, true),
					new Q(Banner.Columns.StatusBooked, true),
					new Q(Banner.Columns.StatusEnabled, true),
					new Or(
						new And(
							new Q(Banner.Columns.FirstDay, QueryOperator.LessThanOrEqualTo, time1.Date),
							new Q(Banner.Columns.LastDay, QueryOperator.GreaterThanOrEqualTo, time1.Date)
						),
						new And(
							new Q(Banner.Columns.FirstDay, QueryOperator.LessThanOrEqualTo, time2.Date),
							new Q(Banner.Columns.LastDay, QueryOperator.GreaterThanOrEqualTo, time2.Date)
						)
					)
				)
			);
		}
	}
}
