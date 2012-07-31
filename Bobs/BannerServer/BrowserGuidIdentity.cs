using System;
using System.Collections.Generic;
using System.Text;
using Caching;
using Common.Collections;

namespace Bobs.BannerServer
{
	public class BrowserGuidIdentity : Identity
	{
		internal BrowserGuidIdentity(Guid browserGuid)
		{
			this.Guid = browserGuid;
		}

		private CounteredList<int> FavouriteMusicTypesCounteredList
		{
			get { return Caching.Instances.Main.Get(new CacheKey(CacheKeyPrefix.FavouredMusicTypeForIdentityRuleByGuid, this.Guid.ToString()), () => new CounteredList<int>()); }
			set { Caching.Instances.Main.Store(new CacheKey(CacheKeyPrefix.FavouredMusicTypeForIdentityRuleByGuid, this.Guid.ToString()), value); }
		}

		public override List<int> FavouriteMusicTypes
		{
			get { return FavouriteMusicTypesCounteredList.GetTopPercentile(.75); }
		}

		public override void AddFavouriteMusicType(int musicTypeK)
		{
			CounteredList<int> favouriteMusicTypes = FavouriteMusicTypesCounteredList;
			favouriteMusicTypes.Add(musicTypeK);
			FavouriteMusicTypesCounteredList = favouriteMusicTypes;
		}

		private CounteredList<int> PlacesVisitedCounteredList
		{
			get { return Caching.Instances.Main.Get(new CacheKey(CacheKeyPrefix.PlacesVisitedForIdentityRuleByGuid, this.Guid.ToString()), () => new CounteredList<int>()); }
			set { Caching.Instances.Main.Store(new CacheKey(CacheKeyPrefix.PlacesVisitedForIdentityRuleByGuid, this.Guid.ToString()), value); }
		}

		public override List<int> PlacesVisited
		{
			get { return PlacesVisitedCounteredList.GetTopPercentile(.75); }
		}

		public override void AddPlaceVisited(int placeK)
		{
			CounteredList<int> placesVisited = PlacesVisitedCounteredList;
			placesVisited.Add(placeK);
			PlacesVisitedCounteredList = placesVisited;
		}

	}
}
