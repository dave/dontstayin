using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Net;
using System.Threading;

namespace Caching.Memcached.Tests
{
	[TestFixture]
	public class ClientTests
	{
		IHasher hasher;
		MemcachedClient mc;
		MemcachedInstances instances;
		const string key = "key";
		const string value = "value";
		[TestFixtureSetUp]
		public void TestFixtureSetUp()
		{
			hasher = new SHA1Hasher();
			instances = new MemcachedInstances(100, Common.Properties.MainCacheServerIPEndPoints);
			mc = new MemcachedClient(instances, hasher);
			
		}
		[TestFixtureTearDown]
		public void TestFixtureTearDown()
		{
			instances.Dispose();
		}

		[SetUp]
		public void SetUp()
		{
			mc.FlushAll();
		}
		[Test]
		public void KeysDoNotAllMapToSameItem()
		{
			mc.Store(key, value, DateTime.MaxValue);
			Assert.IsNull(mc.Get(Guid.NewGuid().ToString()));
		}
		
		[Test]
		public void GetReturnsSetData()
		{
			mc.Store(key, value, DateTime.MaxValue);
			Assert.AreEqual(value, mc.Get(key));
		}
		[Test]
		public void GetReturnsNullIfNoDataSet()
		{
			Assert.IsNull(mc.Get(key));
		}
		[Test]
		public void DeleteRemovesValue()
		{
			mc.Store(key, value, DateTime.MaxValue);
			mc.BlockingDelete(key);
			Assert.IsNull(mc.Get(key));
		}
		[Test]
		public void DeletePreventsValueBeingAdded()
		{
			mc.Store(key, value, DateTime.MaxValue);
			mc.BlockingDelete(key);
			mc.Store(key, value, DateTime.MaxValue);
			Assert.IsNull(mc.Get(key));
		}
		[Test]
		public void ThingsCanStillBeAddedAfterADelete()
		{
			mc.Store(key, value, DateTime.MaxValue);
			mc.BlockingDelete(key);
			Thread.Sleep(2000);
			mc.Store(key, value, DateTime.MaxValue);
			Assert.AreEqual(value, mc.Get(key));
		}

		[Test]
		public void IncrementCounter_NoCounterSet_NullReturned()
		{
			Assert.IsNull(mc.Increment(key, 1));
		}

		[Test]
		public void IncrementCounter_CounterSet_CounterIncrementsByValue()
		{
			mc.Store(key, (uint)20, DateTime.MaxValue);
			Assert.AreEqual(21, mc.Increment(key, 1));
			Assert.AreEqual(26, mc.Increment(key, 5));
		}

		[Test]
		public void IncrementCounter_CounterSet_NegativeValue_CounterDecrementsByValue()
		{
			mc.Store(key, 15, DateTime.MaxValue);
			Assert.AreEqual(10, mc.Increment(key, -5));
		}

		[Test]
		public void IncrementCounter_SetValueZero_Decrement_StillZero()
		{
			mc.Store(key, 2, DateTime.MaxValue);
			Assert.AreEqual(1, mc.Increment(key, -1));
			Assert.AreEqual(0, mc.Increment(key, -1));
			Assert.AreEqual(0, mc.Increment(key, -1));
		}
		[Test]
		public void IncrementCounter_SetValueThree_Increment_Four()
		{
			mc.Store(key, 1, DateTime.MaxValue);
			Assert.AreEqual(2, mc.Increment(key, 1));
			mc.Store(key, 3, DateTime.MaxValue);
			Assert.AreEqual(4, mc.Increment(key, 1));
		}
		[Test]
		public void Set_DoSomeSetsAndGets()
		{
			Assert.AreEqual(null, mc.Get(key));
			mc.Set(key, value, DateTime.MaxValue);
			Assert.AreEqual(value, mc.Get(key));
			mc.BlockingDelete(key);
			Assert.AreEqual(null, mc.Get(key));
			mc.Set(key, value, DateTime.MaxValue);
			Assert.AreEqual(value, mc.Get(key)); //set is not affected by blocking
		}
		[Test]
		public void MultiSet_Test()
		{
			int numberOfKeys = 4;
			KeyValuePair<string, object>[] pairs = new KeyValuePair<string, object>[numberOfKeys];
			numberOfKeys.Times((i) => pairs[i] = new KeyValuePair<string, object>(key + i.ToString(), value + i.ToString()));
			foreach(var pair in pairs){
				Assert.AreEqual(null, mc.Get(pair.Key));
			}
			mc.MultiSet(pairs, DateTime.MaxValue);
			foreach(var pair in pairs){
				Assert.AreEqual(pair.Value, mc.Get(pair.Key));
			}
		}
		[Test]
		public void MultiGet_Test()
		{
			int numberOfKeys = 4;
			List<KeyValuePair<string, object>> pairs = new List<KeyValuePair<string, object>>(numberOfKeys);
			numberOfKeys.Times((i) => pairs.Add(new KeyValuePair<string, object>(key + i.ToString(), value + i.ToString())));
			foreach (object o in mc.MultiGet(pairs.ConvertAll(pair => pair.Key).ToArray()))
			{
				Assert.IsNull(o);
			}
			mc.MultiSet(pairs.ToArray(), DateTime.MaxValue);
			object[] returnedObjects = mc.MultiGet(pairs.ConvertAll(pair => pair.Key).ToArray());
			for(int i=0;i<returnedObjects.Length;i++)
			{
				Assert.AreEqual(pairs[i].Value, returnedObjects[i]);
			}
			
		}

	}
}
