using System;
using System.Collections.Generic;
using System.Text;
using Common;

namespace Bobs.BannerServer
{
	public class UsrIdentity : Identity
	{
		public UsrIdentity(Usr u) : base()
		{
			this.Usr = u;
			this.Guid = u.Guid;
		}
		
		public override List<int> FavouriteMusicTypes
		{
			get
			{
				return Caching.Instances.Main.GetWithLocalCaching(
							"GetKsOfMusicTypeFavouredByUsr" + this.Usr.K.ToString(),
							() =>
								{
									Query q = new Query();
									q.Columns = new ColumnSet(UsrMusicTypeFavourite.Columns.MusicTypeK);
									q.QueryCondition = new Q(UsrMusicTypeFavourite.Columns.UsrK, this.Usr.K);
									UsrMusicTypeFavouriteSet set = new UsrMusicTypeFavouriteSet(q);
									List<int> list = new List<int>(set.Count);
									foreach (UsrMusicTypeFavourite item in set)
									{
										list.Add(item.MusicTypeK);
									}
									return list;
								},
							Time.Minutes(1),
							Time.Minutes(15)
								);
			}
		}

		public override void AddFavouriteMusicType(int musicTypeK)
		{
			throw new Exception("This method should not be used when Usr is known.");
		}

		public override List<int> PlacesVisited
		{
			get
			{
				return Caching.Instances.Main.GetWithLocalCaching(
				 "GetKsOfPlacesVisitedByUsr" + this.Usr.K.ToString(),
				 () =>
				 {
					 Query q = new Query();
					 q.Columns = new ColumnSet(UsrPlaceVisit.Columns.PlaceK);
					 q.QueryCondition = new Q(UsrPlaceVisit.Columns.UsrK, this.Usr.K);
					 UsrPlaceVisitSet set = new UsrPlaceVisitSet(q);
					 return set.ToList().ConvertAll(upv => upv.PlaceK);
				 },
				 Time.Seconds(15),
				 Time.Minutes(15)
					 );
			}
		}

		public override void AddPlaceVisited(int placeK)
		{
			throw new Exception("This method should not be used when Usr is known.");
		}

	}
}
