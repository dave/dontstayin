using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Bobs;
using Caching;

namespace CacheTests
{
	[TestFixture]
	public class TriggerTests : UnitTestUtilities.DatabaseRollbackTestClass
	{
		[Test]
		public void TestInsertsFireInvalidationTriggers()
		{
			Tag tag = new Tag();
			tag.Update();
			Photo photo = new Photo();
			photo.Update();
			Caching.Instances.Main.FlushAll();
			string version = Guid.NewGuid().ToString();
			Caching.Instances.Main.Set(Caching.CacheKeys.Tag.TagPhotos(tag.K), version);
			TagPhoto tagPhoto = new TagPhoto() { TagK = tag.K, PhotoK = photo.K};
			Assert.AreEqual(version, Caching.Instances.Main.Get(Caching.CacheKeys.Tag.TagPhotos(tag.K)));
			tagPhoto.Update();
			Assert.AreNotEqual(version, Caching.Instances.Main.Get(Caching.CacheKeys.Tag.TagPhotos(tag.K)));

		}
		[Test]
		public void TestDeletesFireInvalidationTriggers()
		{
			Tag tag = new Tag();
			tag.Update();
			Photo photo2 = new Photo();
			photo2.Update();
			Photo photo = new Photo();
			photo.Update();
			Caching.Instances.Main.FlushAll();
			TagPhoto tagPhoto = new TagPhoto() { TagK = tag.K, PhotoK = photo.K};
			tagPhoto.Update();
			string version = Caching.Instances.Main.Get(Caching.CacheKeys.Tag.TagPhotos(tag.K)) as string;
			tagPhoto.Delete();
			Assert.AreNotEqual(version, Caching.Instances.Main.Get(Caching.CacheKeys.Tag.TagPhotos(tag.K)));

		}
		[Test]
		public void TestDeletesRemoveBobsFromCache()
		{
			Usr usr = new Usr();
			usr.Update();
			UsrTableDef def = new UsrTableDef();
			Caching.Instances.Main.Set(new Caching.CacheKeys.BobCacheKey("Usr", usr.K, def.TableCacheKey), Guid.NewGuid().ToString());
			Assert.IsNotNull(Caching.Instances.Main.Get(new Caching.CacheKeys.BobCacheKey("Usr", usr.K, def.TableCacheKey)));
			usr.Delete();
			Assert.IsNull(Caching.Instances.Main.Get(new Caching.CacheKeys.BobCacheKey("Usr", usr.K, def.TableCacheKey)));
		}
	}
}
