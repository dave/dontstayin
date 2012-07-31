using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
namespace Bobs
{
	[TestFixture]
	public class Flyer_Tests : UnitTestUtilities.DatabaseRollbackTestClass
	{
		[Test]
		public void CountUsrs_NoConditions()
		{
			new Delete(TablesEnum.Usr, new Q(true)).Run();
			int usrsToCreate = 15;
			usrsToCreate.Times(() =>
			{
				Usr u = new Usr()
				{
					Email = Guid.NewGuid().ToString(),
					IsSkeleton = false,
					IsEmailVerified = true,
					SendFlyers = true
				};
				u.Update();
			});

			Query q = new Query();
			q.QueryCondition = new And(new Q(Usr.Columns.IsSkeleton, false), new Q(Usr.Columns.SendFlyers, true));
			q.ReturnCountOnly = true;

			int totalUsrs = new UsrSet(q).Count;
			Assert.AreEqual(usrsToCreate, totalUsrs);
			Assert.AreEqual(totalUsrs, Flyer.CountUsrs(new List<int>(), new List<int>(), false));
		}

		[Test]
		public void CountUsrs_PlaceConditions_NotIncludingIfUsrHasBeenToEventInTown()
		{
			new Delete(TablesEnum.Usr, new Q(true)).Run();

			Random r = new Random();
			List<int> placeKs = new List<int>() { 1, 2, 3, 4 };
			List<int> notPlaceKs = new List<int>() { 5, 6, 7, 8, 9, 10, 11 };

			int usrsToCreate = 15;
			int usrs = 0;
			usrsToCreate.Times(() =>
			{
				Usr u = new Usr()
				{
					Email = Guid.NewGuid().ToString(),
					IsSkeleton = false,
					IsEmailVerified = true,
					SendFlyers = true
				};
				u.Update();

				int i = r.Next(placeKs.Count + notPlaceKs.Count);
				int placeK;
				if (i < placeKs.Count)
				{
					usrs++;
					placeK = placeKs[i];
				}
				else
				{
					placeK = notPlaceKs[i - placeKs.Count];
				}
				UsrPlaceVisit up = new UsrPlaceVisit()
				{
					UsrK = u.K,
					PlaceK = placeK
				};
				up.Update();
			});

			Assert.AreEqual(usrs, Flyer.CountUsrs(placeKs, new List<int>(), false));
		}

		[Test]
		public void CountUsrs_PlaceConditions_IncludingIfUsrHasBeenToEventInTown()
		{
			new Delete(TablesEnum.Usr, new Q(true)).Run();

			Random r = new Random();
			List<int> placeKs = new List<int>() { 1, 2, 3, 4 };

			Venue v = new Venue()
			{
				PlaceK = placeKs[0]
			};
			v.Update();

			Event e = new Event()
			{
				VenueK = v.K
			};
			e.Update();

			int usrsToCreate = 15;
			int usrs = 0;
			usrsToCreate.Times(() =>
			{
				Usr u = new Usr()
				{
					Email = Guid.NewGuid().ToString(),
					IsSkeleton = false,
					IsEmailVerified = true,
					SendFlyers = true
				};
				u.Update();

				int i = r.Next(placeKs.Count * 2);
				if (i < placeKs.Count)
				{
					UsrEventAttended ue = new UsrEventAttended()
					{
						UsrK = u.K,
						EventK = e.K
					};
					ue.Update();
					usrs++;
				}
			});

			Assert.AreEqual(usrs, Flyer.CountUsrs(placeKs, new List<int>(), false));
		}

		[Test]
		public void CountUsrs_MusicConditions_NotIncludingAllMusic()
		{
			new Delete(TablesEnum.Usr, new Q(true)).Run();

			Random r = new Random();
			List<int> musicTypeKs = new List<int>() { 2, 3, 4 };

			int usrsToCreate = 15;
			int usrs = 0;
			usrsToCreate.Times(() =>
			{
				Usr u = new Usr()
				{
					Email = Guid.NewGuid().ToString(),
					IsSkeleton = false,
					IsEmailVerified = true,
					SendFlyers = true
				};
				u.Update();

				int i = r.Next(musicTypeKs.Count * 2);
				if (i < musicTypeKs.Count)
				{
					UsrMusicTypeFavourite um = new UsrMusicTypeFavourite()
					{
						UsrK = u.K,
						MusicTypeK = musicTypeKs[i]
					};
					um.Update();
					usrs++;
				}
			});

			Assert.AreEqual(usrs, Flyer.CountUsrs(new List<int>(), musicTypeKs, false));
		}

		[Test]
		public void CountUsrs_MusicConditions_TestParentImpliesChildMusicType()
		{
			new Delete(TablesEnum.Usr, new Q(true)).Run();

			// 4 is parent of 5
			List<int> musicTypeKs = new List<int>() { 4 };

			Usr u = new Usr()
			{
				Email = Guid.NewGuid().ToString(),
				IsSkeleton = false,
				IsEmailVerified = true,
				SendFlyers = true
			};
			u.Update();

			UsrMusicTypeFavourite um = new UsrMusicTypeFavourite()
			{
				UsrK = u.K,
				MusicTypeK = 5
			};
			um.Update();

			Assert.AreEqual(1, Flyer.CountUsrs(new List<int>(), musicTypeKs, false));
		}

		[Test]
		public void CountUsrs_MusicConditions_TestChildImpliesParentMusicType()
		{
			new Delete(TablesEnum.Usr, new Q(true)).Run();

			// 4 is parent of 5
			List<int> musicTypeKs = new List<int>() { 5 };

			Usr u = new Usr()
			{
				Email = Guid.NewGuid().ToString(),
				IsSkeleton = false,
				IsEmailVerified = true,
				SendFlyers = true
			};
			u.Update();

			UsrMusicTypeFavourite um = new UsrMusicTypeFavourite()
			{
				UsrK = u.K,
				MusicTypeK = 4
			};
			um.Update();

			Assert.AreEqual(1, Flyer.CountUsrs(new List<int>(), musicTypeKs, false));
		}

		[Test]
		public void CountUsrs_MusicConditions_QueryIncludingAllMusic_GetsAllUsrs()
		{
			new Delete(TablesEnum.Usr, new Q(true)).Run();

			Random r = new Random();
			List<int> musicTypeKs = new List<int>() { 1, 2, 3, 4 };

			int usrsToCreate = 15;
			usrsToCreate.Times(() =>
			{
				Usr u = new Usr()
				{
					Email = Guid.NewGuid().ToString(),
					IsSkeleton = false,
					IsEmailVerified = true,
					SendFlyers = true
				};
				u.Update();

				int i = r.Next(musicTypeKs.Count * 2);
				if (i < musicTypeKs.Count)
				{
					UsrMusicTypeFavourite um = new UsrMusicTypeFavourite()
					{
						UsrK = u.K,
						MusicTypeK = musicTypeKs[i]
					};
					um.Update();
				}
			});

			Assert.AreEqual(usrsToCreate, Flyer.CountUsrs(new List<int>(), musicTypeKs, false));
		}


		[Test]
		public void CountUsrs_PlaceConditionsAndMusicConditions()
		{
			new Delete(TablesEnum.Usr, new Q(true)).Run();
			Place p = new Place
			{
				MeridianFeatureId = 1,
				Lat = 1,
				Lon = 1
			};
			p.Update();
			
			CreateUsr(0, 1, 1, 1); //no
			CreateUsr(1, 1, p.K, p.K); //yes
			CreateUsr(1, p.K, 1, p.K); //yes
			CreateUsr(1, p.K, p.K, 1); //yes
			CreateUsr(4, 1, p.K, p.K); //yes
			CreateUsr(4, p.K, 1, p.K); //yes
			CreateUsr(4, p.K, p.K, 1); //yes
			CreateUsr(5, 1, p.K, p.K); //yes
			CreateUsr(5, p.K, 1, p.K); //yes
			CreateUsr(5, p.K, p.K, 1); //yes
			CreateUsr(4, 2, p.K, p.K); //no
			CreateUsr(4, p.K, 2, p.K); //no
			CreateUsr(4, p.K, p.K, 2); //no
			CreateUsr(5, 2, p.K, p.K); //no
			CreateUsr(5, p.K, 2, p.K); //no
			CreateUsr(5, p.K, p.K, 2); //no
			Assert.AreEqual(9, Flyer.CountUsrs(new List<int>() { 1 }, new List<int>() { 4 }, false));
		}

		[Test]
		public void CountUsrs_PlaceConditionsAndMusicConditionsAndPromotersOnly()
		{
			new Delete(TablesEnum.Usr, new Q(true)).Run();
			Place p = new Place
			{
				MeridianFeatureId = 1,
				Lat = 1,
				Lon = 1
			};
			p.Update();
			CreateUsr(0, 1, 1, 1); //no
			CreateUsr(1, 1, p.K, p.K, false); //no
			CreateUsr(1, p.K, 1, p.K, true); //yes
			CreateUsr(1, p.K, p.K, 1, false); //no
			CreateUsr(4, 1, p.K, p.K, false); //no
			CreateUsr(4, p.K, 1, p.K, false); //no
			CreateUsr(4, p.K, p.K, 1, true); //yes
			CreateUsr(5, 1, p.K, p.K, true); //yes
			CreateUsr(5, p.K, 1, p.K, true); //yes
			CreateUsr(5, p.K, p.K, 1, true); //yes
			CreateUsr(4, 2, p.K, p.K); //no
			CreateUsr(4, p.K, 2, p.K); //no
			CreateUsr(4, p.K, p.K, 2); //no
			CreateUsr(5, 2, p.K, p.K); //no
			CreateUsr(5, p.K, 2, p.K); //no
			CreateUsr(5, p.K, p.K, 2); //no

			Assert.AreEqual(5, Flyer.CountUsrs(new List<int>() { 1 }, new List<int>() { 4 }, true));
		}

		private void CreateUsr(int musicTypeK, int homePlaceK, int eventPlaceK, int visitPlaceK)
		{
			CreateUsr(musicTypeK, homePlaceK, eventPlaceK, visitPlaceK, false);
		}
		private void CreateUsr(int musicTypeK, int homePlaceK, int eventPlaceK, int visitPlaceK, bool isPromoter)
		{
			Usr u = new Usr()
			{
				Email = Guid.NewGuid().ToString(),
				IsSkeleton = false,
				IsEmailVerified = true,
				SendFlyers = true,
				HomePlaceK = homePlaceK,
				IsPromoter = isPromoter
			};
			u.Update();

			UsrMusicTypeFavourite um = new UsrMusicTypeFavourite()
			{
				UsrK = u.K,
				MusicTypeK = musicTypeK
			};
			um.Update();



			Venue v = new Venue()
			{
				PlaceK = eventPlaceK
			};
			v.Update();

			Event e = new Event()
			{
				VenueK = v.K
			};
			e.Update();

			UsrEventAttended ue = new UsrEventAttended()
			{
				UsrK = u.K,
				EventK = e.K
			};
			ue.Update();


			UsrPlaceVisit up = new UsrPlaceVisit()
			{
				UsrK = u.K,
				PlaceK = visitPlaceK
			};
			up.Update();
		}

	}
}
