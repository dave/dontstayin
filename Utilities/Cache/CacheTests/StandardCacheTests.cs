using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Caching.Memcached;
using Common;
using Caching;

namespace CacheTests.Memcached
{
	[TestFixture]
	public class StandardMemcachedCacheTests
	{
		[Test]
		public void TestCachingWorks()
		{
			Caching.Instances.Main.FlushAll();
			
			Assert.AreEqual("once", (Caching.Instances.Main.Get("TestKey", () => "once")));
			Assert.AreEqual("once", (Caching.Instances.Main.Get("TestKey", () => "twice")));

			Assert.AreEqual("once", (Caching.Instances.LocalCache.Get("TestKey", () => "once")));
			Assert.AreEqual("once", (Caching.Instances.LocalCache.Get("TestKey", () => "twice")));

			Assert.AreEqual(1u, (Caching.Instances.MainCounterStore.GetCounter("TestKey", () => 1u)));
			Assert.AreEqual(1u, (Caching.Instances.MainCounterStore.GetCounter("TestKey", () => 2u)));
		}

		[Test]
		public void TestCounter()
		{
			Caching.Instances.Main.FlushAll();

			string key = "TestCounter" + Time.Now.Ticks.ToString();
			uint initialValue = 5u;

			Assert.AreEqual(initialValue, Caching.Instances.MainCounterStore.GetCounter(key, () => initialValue));
			for (int i = 1; i < 10; i++)
			{
				Caching.Instances.MainCounterStore.Increment(key, () => initialValue);
				Assert.AreEqual(i + initialValue, Caching.Instances.MainCounterStore.GetCounter(key, () => 0u));
			}
		}

		[Test]
		public void TestCounterIncrementWithoutSetting()
		{
			string key = Guid.NewGuid().ToString();
			Assert.AreEqual(1, Caching.Instances.MainCounterStore.Increment(key, () => 0));
		}

		[Test]
		public void TestCounterClass()
		{
			Caching.Instances.Main.FlushAll();

			string key = Guid.NewGuid().ToString();
			Counter counter = new Counter(key);
			counter.Value = (uint)10;
			Counter counter2 = new Counter(key);
			Assert.AreEqual(10L, counter2.Value);
			Assert.AreEqual(11L, counter2.Increment());
			Assert.AreEqual(11L, counter2.Value);

		}


		[Test]
		public void TestBlockingOnDelete()
		{
			Caching.Instances.Main.FlushAll();

			string key = Guid.NewGuid().ToString();

			Caching.Instances.Main.Store(key, key);
			Caching.Instances.Main.Delete(key);
			Caching.Instances.Main.Store(key, key); // this should get blocked
			Assert.IsNull(Caching.Instances.Main.Get(key));
		}

		[Test]
		public void TestBlockingOnDelete2()
		{
			Caching.Instances.Main.FlushAll();

			string key = Guid.NewGuid().ToString();

			Caching.Instances.Main.Delete(key);
			Caching.Instances.Main.Store(key, key); // this should get blocked
			Assert.IsNull(Caching.Instances.Main.Get(key));
		}
	}
}
