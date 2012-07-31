using System;
using System.Collections.Generic;
using System.Text;
using Bobs.BannerServer.Rules.TypesOfRule;
using Bobs;
using Caching;
using Bobs.DataHolders;
using Common;
using System.Collections.ObjectModel;

namespace Bobs.BannerServer.Rules
{
	public class RequestRules: ICacheKeyProvider
	{
		internal SortedList<Rule, Rule> rulesInfo;

		MusicTypesFavouredByIdentityRule musicTypes;
		public MusicTypesFavouredByIdentityRule MusicTypes
		{
			get
			{
				return this.musicTypes;
			}
			set
			{
				this.musicTypes = value;
				this.cacheKey = null;
				bannersSatsfyingQueryConditionsInTimeslot = null;
			}
		}
		PlacesVisitedByIdentityRule placesVisited;
		public PlacesVisitedByIdentityRule PlacesVisited
		{
			get
			{
				return this.placesVisited;
			}
			set
			{
				this.placesVisited = value;
				this.cacheKey = null;
				bannersSatsfyingQueryConditionsInTimeslot = null;
			}
		}
		public RequestRules()
		{
			rulesInfo = new SortedList<Rule, Rule>();
			MusicTypes = new MusicTypesFavouredByIdentityRule();
			PlacesVisited = new PlacesVisitedByIdentityRule();
		}


		internal void Add(Rule rule)
		{
			rulesInfo[rule] = rule;
			this.cacheKey = null;
			bannersSatsfyingQueryConditionsInTimeslot = null;
		}

		Rule Get(Rule rule)
		{
			try
			{
				return rulesInfo[rule];
			}
			catch (KeyNotFoundException)
			{
				return null;
			}
		}

		internal void Add(RequestRules rr)
		{
			foreach (Rule rule in rr.Rules)
			{
				rulesInfo[rule] = rr.Get(rule);
			}
			this.MusicTypes.Add(rr.MusicTypes);
			this.PlacesVisited.Add(rr.PlacesVisited);
			cacheKey = null;
			bannersSatsfyingQueryConditionsInTimeslot = null;
		}

		IList<Rule> Rules
		{
			get { return rulesInfo.Keys; }
		}

		private List<Q> QueryConditions
		{
			get
			{
				List<Q> queries = new List<Q>(rulesInfo.Count);
				foreach (Rule rule in rulesInfo.Keys)
				{
					queries.Add(rule.Q);
				}
				queries.Add(this.MusicTypes.Query);
				queries.Add(this.PlacesVisited.Query);
				return queries;
			}
		}



		#region ICacheKeyProvider Members
		string cacheKey;
		public string GetCacheKey()
		{
			if (cacheKey == null)
			{
				cacheKey = "RequestRules" + String.Join(",", QueryConditions.ConvertAll(q => q.GetCacheKey()).ToArray());
			}
			return cacheKey;
		}

		#endregion

		#region REMOVED DUE TO BANNER SERVER BUG
		#region for bug tracking
		//List<string> actions = new List<string>();
		#endregion
		#endregion

		//notice that this has a complementary set method below
		List<BannerDataHolder> bannersSatsfyingQueryConditionsInTimeslot = null;
		internal ReadOnlyCollection<BannerDataHolder> GetBannersSatsfyingQueryConditionsInTimeslot(Timeslot timeslot)
		{
			if (bannersSatsfyingQueryConditionsInTimeslot == null)
			{
				bannersSatsfyingQueryConditionsInTimeslot = Caching.Instances.Main.GetWithLocalCaching(
					"BannersSatsfyingQueryConditionsInTimeslot" + this.GetCacheKey() + timeslot.GetCacheKey(),
					() => GetBannersSatisfyingQueryConditions(),
					Time.Seconds(15),
					Timeslot.Duration
				);
			}
			return bannersSatsfyingQueryConditionsInTimeslot.AsReadOnly();
		}
		#region REMOVED DUE TO BANNER SERVER BUG
		//private void UpdateBannersSatsfyingQueryConditionsInTimeslot(Timeslot timeslot)
		//{
		//    string key = "BannersSatsfyingQueryConditionsInTimeslot" + this.GetCacheKey() + timeslot.GetCacheKey();
		//    Caching.Instances.Main.Store(
		//        key,
		//        bannersSatsfyingQueryConditionsInTimeslot,
		//        Time.Minutes(5)
		//    );
		//    Caching.Instances.LocalCache.Delete(key);
		//}
		#endregion
		List<BannerDataHolder> GetBannersSatisfyingQueryConditions()
		{
			string key = "GetBannersSatisfyingQueryConditions" + this.GetCacheKey();
			return Caching.Instances.Main.GetWithLocalCaching(
				key,
				() =>
				{
					Query q = new Query();
					q.Distinct = true;
					q.DistinctColumn = Bobs.Banner.Columns.K;
					q.QueryCondition = new And(this.QueryConditions.ToArray());
					q.TableElement = new Join(
											  new JoinLeft(Bobs.Banner.Columns.K, BannerPlace.Columns.BannerK),
											  new TableElement(TablesEnum.BannerMusicType),
											  QueryJoinType.Left,
											  new Q(Bobs.Banner.Columns.K, BannerMusicType.Columns.BannerK, true)
										  );
					q.OrderBy = new OrderBy(new OrderBy(Banner.Columns.Priority, OrderBy.OrderDirection.Descending), new OrderBy(Banner.Columns.AlwaysShow, OrderBy.OrderDirection.Descending), new OrderBy(Banner.Columns.K));
					return new BannerSet(q).ToList().ConvertAll(b => new BannerDataHolder(b));
				},
				Time.Seconds(15),
				Time.Minutes(15)
			);
		}



		internal IEnumerable<BannerQuerySetInfo> BannerQuerySetInfosInDescendingPriorityOrder(Timeslot currentTimeslot)
		{
			ReadOnlyCollection<BannerDataHolder> banners = this.GetBannersSatsfyingQueryConditionsInTimeslot(currentTimeslot);
			List<BannerDataHolder> bannerSetBanners = null;
			for (int i = 0; i < banners.Count; i++)
			{
				bannerSetBanners = new List<BannerDataHolder>();
				int priority = banners[i].Priority;
				bool alwaysShow = banners[i].AlwaysShow;
				bannerSetBanners.Add(banners[i]);
				while (i + 1 < banners.Count && banners[i + 1].Priority == priority && banners[i + 1].AlwaysShow == alwaysShow)
				{
					i++;
					bannerSetBanners.Add(banners[i]);
				}
				if (alwaysShow)
				{
					yield return new AlwaysShowBannerQuerySetInfo(bannerSetBanners);
				}
				else
				{
					TargetHitsBannerQuerySetInfo reqInfo = new TargetHitsBannerQuerySetInfo(bannerSetBanners, currentTimeslot, info => info.RequiredHits);
					#region REMOVED DUE TO BANNER SERVER BUG
					//AddNewIndicesToListForRemoval(bannerSetBanners.Count, i, reqInfo.IndicesOfBannersThatHaveReachedTheirLimits);
					#endregion
					yield return reqInfo;

					TargetHitsBannerQuerySetInfo desInfo = new TargetHitsBannerQuerySetInfo(bannerSetBanners, currentTimeslot, info => info.DesiredHits);
					#region REMOVED DUE TO BANNER SERVER BUG
					//// bump up the indexes from this set to make them apply to master set
					//AddNewIndicesToListForRemoval(bannerSetBanners.Count, i, desInfo.IndicesOfBannersThatHaveReachedTheirLimits);
					#endregion
					yield return desInfo;
				}
			}
		}

		#region REMOVED DUE TO BANNER SERVER BUG
		//private void AddNewIndicesToListForRemoval(int numberOfBannersInSet, int offset, List<int> indices)
		//{
		//    actions.Add(string.Format("AddIndices({0},{1},{2})", numberOfBannersInSet.ToString(), offset.ToString(), string.Join(",", indices.ConvertAll(j => j.ToString()).ToArray())));
		//    foreach (int index in indices)
		//    {
		//        int newIndex = index + offset + 1 - numberOfBannersInSet;
		//        if (!this.indicesOfBannersWhichHaveReachedTheirTargetsThisTimeslot.Contains(newIndex))
		//            indicesOfBannersWhichHaveReachedTheirTargetsThisTimeslot.Add(newIndex);
		//    }
		//}
		//List<int> indicesOfBannersWhichHaveReachedTheirTargetsThisTimeslot = new List<int>();
		//internal void RemoveBannersWhichHaveReachedTargetsFromCachedQuery(Timeslot currentTimeslot)
		//{
		//    int length = -1;
		//    if (bannersSatsfyingQueryConditionsInTimeslot != null)
		//    {
		//        length= bannersSatsfyingQueryConditionsInTimeslot.Count;
		//    }
		//    if (indicesOfBannersWhichHaveReachedTheirTargetsThisTimeslot.Count > 0)
		//    {
		//        try
		//        {
		//            bannersSatsfyingQueryConditionsInTimeslot.RemoveAt(indicesOfBannersWhichHaveReachedTheirTargetsThisTimeslot);
		//            UpdateBannersSatsfyingQueryConditionsInTimeslot(currentTimeslot);
		//            SpottedException.TryToSaveExceptionAndChildExceptions(
		//                new Exception(string.Format("{2} : {0}; {1}; count:{3}", string.Join(",", indicesOfBannersWhichHaveReachedTheirTargetsThisTimeslot.ConvertAll(i => i.ToString()).ToArray()), string.Join(",", actions.ToArray()), "", length)),
		//                System.Web.HttpContext.Current, Usr.Current, Visit.HasCurrent ? Visit.Current : null, "CountBugSuccessful", "", "", 0, Model.Entities.ObjectType.None);
		//        }
		//        catch (ArgumentOutOfRangeException ex)
		//        {
		//            SpottedException.TryToSaveExceptionAndChildExceptions(
		//                new Exception(string.Format("{2} : {0}; {1}; count:{3}", string.Join(",", indicesOfBannersWhichHaveReachedTheirTargetsThisTimeslot.ConvertAll(i => i.ToString()).ToArray()), string.Join(",", actions.ToArray()), ex.Message, length)),
		//                System.Web.HttpContext.Current, Usr.Current, Visit.HasCurrent ? Visit.Current : null, "CountBug", "", "", 0, Model.Entities.ObjectType.None);
		//        }
		//    }
		//}
		#endregion
	}
}
