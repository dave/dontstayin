using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Pair = System.Collections.Generic.KeyValuePair<object, Bobs.OrderBy.OrderDirection>;
namespace Bobs.CachedDataAccess
{
	[TestFixture]
	public class ChildrenTests : UnitTestUtilities.DatabaseRollbackTestClass
	{
		[Test]
		public void AddingAChildUsingDatabaseAddsToChildren()
		{
			Venue venue = new Venue();
			venue.Update();
			Assert.AreEqual(0, venue.ChildEvents().Count);
			Event ev = new Event();
			ev.VenueK = venue.K;
			ev.Update();
			Assert.AreEqual(1, venue.ChildEvents().Count);
		}
		[Test]
		public void RemovingAChildUsingDatabaseRemovesFromChildren()
		{
			Venue venue = new Venue();
			venue.Update();
			Venue venue2 = new Venue();
			venue2.Update();
			Event ev = new Event();
			ev.VenueK = venue.K;
			ev.Update();
			Assert.AreEqual(1, venue.ChildEvents().Count);
			ev.VenueK = venue2.K;
			ev.Update();
			Assert.AreEqual(0, venue.ChildEvents().Count);
		}
		[Test]
		public void PagingWorks()
		{
			Venue venue = new Venue();
			venue.Update();
			Random r = new Random();
			int pageSize = r.Next(10) + 5;
			int numberOfItems = r.Next(50) + 50;
			if (numberOfItems % pageSize == 0) { numberOfItems++; }
			List<Event> events = new List<Event>();
			for (int i = 0; i < numberOfItems; i++)
			{
				Event ev = new Event();
				ev.VenueK = venue.K;
				ev.Update();
				events.Add(ev);
			}
			Assert.AreEqual(numberOfItems, venue.ChildEvents().Count);
			Assert.AreEqual(pageSize, venue.ChildEvents().Page(1, pageSize).Length);
			int highestPageNumber = (int)  Math.Ceiling((float) numberOfItems / (float)pageSize);
			Assert.AreEqual(numberOfItems % pageSize, venue.ChildEvents().Page(highestPageNumber, pageSize).Length);
		}
		[Test]
		public void OrderBysWork()
		{
			Venue venue = new Venue();
			venue.Update();
			List<Event> events = new List<Event>();
			for (int i = 0; i < 10; i++)
			{
				Event ev = new Event();
				ev.VenueK = venue.K;
				ev.Update();
				events.Add(ev);
			}

			Event[] childEvents = venue.ChildEvents().AllItems();
			Event[] pagedChildEvents = venue.ChildEvents().Page(1, 10);
			for (int i = 0; i < events.Count; i++)
			{
				Assert.AreEqual(events[i].K, childEvents[i].K);
			}
			var orderBy = new Pair(Event.Columns.K, OrderBy.OrderDirection.Descending);
			childEvents = venue.ChildEvents(orderBy).AllItems();
			pagedChildEvents = venue.ChildEvents(orderBy).Page(1, 10);
			for (int i = 0; i < events.Count; i++)
			{
				Assert.AreEqual(events[9 - i].K, childEvents[i].K);
			}
		}
		[Test]
		public void WheresWork()
		{

			Venue venue = new Venue();
			venue.Update();
			List<Event> events = new List<Event>();
			for (int i = 0; i < 10; i++)
			{
				Event ev = new Event();
				ev.VenueK = venue.K;
				ev.Update();
				events.Add(ev);
			}

			Assert.AreEqual(0, venue.ChildEvents(new Q("1 = 0", null, null)).AllItems().Length);
			Assert.AreEqual(0, venue.ChildEvents(new Q("1 = 0", null, null)).Page(1, 10).Length);
			 
		}
		[Test]
		public void LinkTablesWork()
		{
			//return;
			Caching.Instances.Main.FlushAll();
			Tag tag = new Tag() { TagText = "hello" };
			tag.Update();
			List<Photo> photos = new List<Photo>();
			for (int i = 0; i < 3; i++)
			{
				Photo photo = new Photo();
				photo.Update();
				photos.Add(photo);
			}

			for (int i = 0; i < photos.Count; i++)
			{
				Assert.AreEqual(i, tag.ChildPhotos().Count);
				TagPhoto tagPhoto = new TagPhoto() { PhotoK = photos[i].K, TagK = tag.K };
				tagPhoto.Update();

			}
			Assert.AreEqual(photos.Count, tag.ChildPhotos().Count);
		}
		[Test]
		public void InvalidationOfSomethingOnALinkTableInvalidatesSet()
		{
			Caching.Instances.Main.FlushAll();
			Tag tag = new Tag() { TagText = "hello" };
			tag.Update();

			Photo photo = new Photo();
			photo.Update();
			Q where = new Q(TagPhoto.Columns.Disabled, false);
			Assert.AreEqual(0, tag.ChildPhotos(where).Count);
			TagPhoto tagPhoto = new TagPhoto() { PhotoK = photo.K, TagK = tag.K, Disabled = false};
			tagPhoto.Update();
			Assert.AreEqual(1, tag.ChildPhotos(where).Count);
			tagPhoto.Disabled = true;
			tagPhoto.Update();
			Assert.AreEqual(0, tag.ChildPhotos(where).Count);
		}

	 
		[Test]
		public void OrderByColumnInChildTableWorks()
		{
			Group group = new Group();
			group.Update();

			10.Times(i => CreatePhotoInGroup(group.K, i));
			var orderBy = new KeyValuePair<object, OrderBy.OrderDirection>(GroupPhoto.Columns.DateTime, OrderBy.OrderDirection.Descending);
			DateTime prevDate = DateTime.MaxValue;
			foreach (Photo p in group.ChildPhotos(orderBy))
			{
				DateTime d = new GroupPhoto(group.K, p.K).DateTime;
				Assert.Greater(prevDate, d);
				prevDate = d;
			}
		}

		private void CreatePhotoInGroup(int groupK, int i)
		{
			Photo p = new Photo();
			p.Update();
			new GroupPhoto {GroupK = groupK, PhotoK = p.K, DateTime = DateTime.Today.AddHours(i)}.Update();
		}

	}
}
