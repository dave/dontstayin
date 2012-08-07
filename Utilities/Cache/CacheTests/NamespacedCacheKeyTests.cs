using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Caching;
using Common.Automation.Sql;
using Bobs;
using System.Data.SqlClient;
using UnitTestUtilities;

namespace CacheTests
{
	[TestFixture]
	public class NamespacedCacheKeyTests : DatabaseRollbackTestClass
	{

 
		[Test]
		public void TestThatGettingANamespacedKeySetsTheMissingNamespaceKeyValues()
		{
			Caching.Instances.Main.FlushAll();
			CacheKey[] namespaceKeys = new CacheKey[] { new CacheKey(CacheKeyPrefix.TagVersion, "1") };
			Assert.IsNull(Caching.Instances.Main.Get(namespaceKeys[0]));
			CacheKey key = new NamespacedCacheKey(Caching.CacheKeyPrefix.BobCacheItem, namespaceKeys, "2");
			Assert.IsNotNull(Caching.Instances.Main.Get(new CacheKey(CacheKeyPrefix.TagVersion, "1")));
		}
		[Test]
		public void TestThatSettingANamespaceKeyClearsTheNamespacedKeyValue()
		{
			Caching.Instances.Main.FlushAll();
			CacheKey[] namespaceKeys = new CacheKey[] { new CacheKey(CacheKeyPrefix.TagVersion, "1") };
			CacheKey key = new NamespacedCacheKey(Caching.CacheKeyPrefix.BobCacheItem, namespaceKeys, "2");
			Caching.Instances.Main.Store(key, "hello");
			Assert.AreEqual("hello", Caching.Instances.Main.Get(key));
			Caching.Instances.Main.Set(namespaceKeys[0].ToString(), Guid.NewGuid().ToString());
			key = new NamespacedCacheKey(Caching.CacheKeyPrefix.BobCacheItem, namespaceKeys, "2");
			Assert.IsNull(Caching.Instances.Main.Get(key));
		}
		[Test]
		public void TestThatNamespaceCacheKeysAreInvalidatedBySqlCommands()
		{
			Caching.Instances.Main.FlushAll();
			Usr u1 = new Usr()
			{
				NickName= Guid.NewGuid().ToString(),
				Email = Guid.NewGuid().ToString()
			};
			u1.Update();
			Usr u2 = new Usr()
			{
				NickName = Guid.NewGuid().ToString(),
				Email = Guid.NewGuid().ToString()
			};
			u1.Update();
			u2.Update();
			Assert.IsNull(Caching.Instances.Main.Get(Caching.CacheKeys.Usr.Banners(u1.K)));
			Assert.IsNull(Caching.Instances.Main.Get(Caching.CacheKeys.Usr.Banners(u2.K)));
			Banner b = new Banner() { UsrK = u1.K };
			b.Update();
			Assert.IsNotNull(Caching.Instances.Main.Get(Caching.CacheKeys.Usr.Banners(u1.K)));
			Assert.IsNull(Caching.Instances.Main.Get(Caching.CacheKeys.Usr.Banners(u2.K)));
			
			b.UsrK= u2.K;
			b.Update();
			Assert.IsNotNull(Caching.Instances.Main.Get(Caching.CacheKeys.Usr.Banners(u1.K)));
			Assert.IsNotNull(Caching.Instances.Main.Get(Caching.CacheKeys.Usr.Banners(u2.K)));
			
			
		}
		[Test]
		public void TestThatGeneratedCacheKeysAreInvalidatedWhenAForeignKeyIsChangedInAChildTable()
		{
			Caching.Instances.Main.FlushAll();
			Venue venue = new Venue();
			venue.Update();
			Venue venue2 = new Venue();
			venue2.Update();
			Event ev = new Event() { VenueK = venue.K };
			ev.Update();
			string value = Guid.NewGuid().ToString();
			Caching.Instances.Main.Store(new NamespacedCacheKey(CacheKeyPrefix.BobCacheItem, Caching.CacheKeys.Venue.Events(venue.K)), value);
			Caching.Instances.Main.Store(new NamespacedCacheKey(CacheKeyPrefix.BobCacheItem, Caching.CacheKeys.Venue.Events(venue2.K)), value);
			ev.VenueK = venue2.K;
			ev.Update();
			Assert.IsNull(Caching.Instances.Main.Get(new NamespacedCacheKey(CacheKeyPrefix.BobCacheItem, Caching.CacheKeys.Venue.Events(venue.K))));
			Assert.IsNull(Caching.Instances.Main.Get(new NamespacedCacheKey(CacheKeyPrefix.BobCacheItem, Caching.CacheKeys.Venue.Events(venue2.K))));
		}

		[Test]
		public void TestThatColumnsIncludedInWhereClausesClearTheCacheWhenTheyAreChanged()
		{
			Caching.Instances.Main.FlushAll();
			Gallery gallery = new Gallery();
			gallery.Update();
			Photo photo = new Photo();
			photo.Status = Photo.StatusEnum.Moderate;
			photo.GalleryK = gallery.K;
			photo.Update();
			Assert.AreEqual(1, gallery.ChildPhotos().Count);
			Assert.AreEqual(1, gallery.ChildPhotos(new Q(Photo.Columns.Status, Photo.StatusEnum.Moderate)).Count);
			Caching.Instances.Main.FlushAll();
			Assert.AreEqual(0, gallery.ChildPhotos(new Q(Photo.Columns.Status, Photo.StatusEnum.Enabled)).Count);
			photo.Status = Photo.StatusEnum.Enabled;
			photo.Update();
			Assert.AreEqual(0, gallery.ChildPhotos(new Q(Photo.Columns.Status, Photo.StatusEnum.Moderate)).Count);
			Assert.AreEqual(1, gallery.ChildPhotos(new Q(Photo.Columns.Status, Photo.StatusEnum.Enabled)).Count);
		}
		[Test, ExpectedException(typeof(System.Exception))]
		public void UseOfAColumnWhichDoesNotHaveCausesInvalidationInAWhereClauseSetThrowsAnException()
		{
			Caching.Instances.Main.FlushAll();
			Gallery gallery = new Gallery();
			gallery.Update();
			Photo photo = new Photo();
			photo.Status = Photo.StatusEnum.Moderate;
			photo.GalleryK = gallery.K;
			photo.Update();
			int count = gallery.ChildPhotos(new Q(Photo.Columns.ParentDateTime, DateTime.Now)).Count;
		}
		
	}
}
