using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Common;
using Bobs.DataHolders;
namespace Bobs.BannerServer
{
	[TestFixture]
	public class LoadTests
	{

		[Test, Ignore]
		public void MakeLoadsOfMakeBannerServerCalls()
		{
			Caching.Instances.Main.FlushAll();

			int reps = 200000;
			TimeSpan delay = new TimeSpan(0,0,0,0,	(int)Time.Minutes(4).TotalMilliseconds / 800);
			Identity id = new BrowserGuidIdentity(Guid.NewGuid());
			
			List<int> placeKs = (new PlaceSet(new Query(new Q(Place.Columns.K, QueryOperator.LessThan, 100)))).ToList().ConvertAll(p => p.K);
			placeKs.ForEach(p => id.AddPlaceVisited(p));
			
			List<int> musicTypeKs = (new MusicTypeSet(new Query())).ToList().ConvertAll(mt => mt.K);
			musicTypeKs.ForEach(p => id.AddFavouriteMusicType(p));
			
			for (int i = 0; i < reps; i++)
			{
				MakeBannerServerCall(id, GetPosition(i));
				System.Threading.Thread.Sleep(delay);
			}
		}

		private void MakeBannerServerCall(Identity id, Banner.Positions position)
		{
		
			Server server = new Server();
			
			BannerDataHolder bdh = server.GetBanner(position, false, id, new Bobs.BannerServer.Rules.RequestRules());
			if (bdh != null)
			{
				LogBannerHit(id, bdh);
			}
			else
			{
				Timeslots.GetCurrentTimeslot().TotalNotShown().Increment();
			}
		}

		private static void LogBannerHit(Identity id, BannerDataHolder bdh)
		{
			Banner b = new Banner(bdh.K);

			b.RegisterHit(id);
		}

		private static Banner.Positions GetPosition(int run)
		{
			switch (run % 2)
			{
				case 0 : return Banner.Positions.Hotbox ;
				case 1: return Banner.Positions.Leaderboard ;
				default: throw new NotImplementedException();
			}
		}

	
	}
}
