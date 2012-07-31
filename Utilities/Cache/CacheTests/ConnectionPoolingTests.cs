using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Threading;
using Caching.Memcached;

namespace CacheTests
{
	[TestFixture]
	public class ConnectionPoolingTests
	{
		[Test, Ignore("Takes a while, not a real test anyway")]
		public void RunManyCacheCommands()
		{
			RunCacheCommands(0);
			RunCacheCommands(1);
			RunCacheCommands(5);
			RunCacheCommands(10);
			RunCacheCommands(15);

		}

		private static void RunCacheCommands(int poolSize)
		{
			MemcachedInstances mi = new MemcachedInstances(poolSize, Common.Properties.MainCacheServerIPEndPoints);
			MemcachedClient mc = new MemcachedClient(mi, new SHA1Hasher());

			int numRuns = 10000;
			DateTime start = DateTime.Now;
			bool threadRunning = false;
			object threadRunningLock = new object();
			for (int i = 0; i < numRuns; i++)
			{
				lock (threadRunningLock)
				{
					threadRunning = true;
				}
				new Thread(() =>
				{
					
					string key = Guid.NewGuid().ToString();
					mc.Store(key, key, DateTime.MaxValue);
					mc.Get(key);
					mc.BlockingDelete(key);
					lock (threadRunningLock)
					{
						threadRunning = false;
					}
				}).Start();
			}
			while (threadRunning)
			{
				Thread.Sleep(100);
			}
			Console.WriteLine();
			TimeSpan duration = DateTime.Now - start;
			Console.WriteLine("Pool size: {0}, duration: {1} seconds", poolSize.ToString().PadLeft(3), duration.TotalMilliseconds.ToString());
		}
	}
}
