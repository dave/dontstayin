using System;
using System.Collections.Generic;
using System.Text;
using Common.Pooling;
using System.Net.Sockets;
using Caching.Memcached.Commands;
using System.IO;
using System.Data;

namespace Caching.Memcached
{
	public class MemcachedClient : ICacheClient
	{
		MemcachedInstances instances;
		IHasher hasher;
		public MemcachedClient(MemcachedInstances instances, IHasher hasher)
		{
			this.instances = instances;
			this.hasher = hasher;
		}

	
		



		public void BlockingDelete(string key)
		{
			BlockingDelete blockingDelete = new BlockingDelete(new Key(key, hasher));
			CommandExecuter ce = new CommandExecuter(instances);
			try
			{
				ce.Execute(blockingDelete);
			}
			catch { }
		}
		public void Store(string key, object value, DateTime expiry)
		{
			Store set = new Store(new Key(key, hasher), value, expiry);
			CommandExecuter ce = new CommandExecuter(instances);
			try
			{
				ce.Execute(set);
			}
			catch { }
		}

		public object Get(string key)
		{
			Get get = new Get(new Key(key, hasher));
			CommandExecuter ce = new CommandExecuter(instances);
			try
			{
				ce.Execute(get);
				return get.RetrievedObject;
			}
			catch
			{
				return null;
			}
		}

		public void FlushAll()
		{
			instances.FlushAll();
		}

		public uint? Increment(string key, long value)
		{
			Increment inc = new Increment(new Key(key, hasher), value);
			CommandExecuter ce = new CommandExecuter(instances);
			try
			{
				ce.Execute(inc);
				return inc.RetrievedValue;
			}
			catch
			{
				return null;
			}
		}



		public DataTable GetStats()
		{
			return instances.Stats;
		}



		public void Dispose()
		{
			instances.Dispose();
		}

		public void Set(string key, string value, DateTime dateTime)
		{
			Set set = new Set(new Key(key, hasher), value, dateTime);
			CommandExecuter ce = new CommandExecuter(instances);
			try
			{
				ce.Execute(set);
			}
			catch { }
		}
		public void MultiSet(KeyValuePair<string, object>[] pairs, DateTime dateTime)
		{
			Set[] sets = new Set[pairs.Length];
			for (int i=0;i<sets.Length;i++)
			{
				KeyValuePair<string, object> pair = pairs[i];
				sets[i] = new Set(new Key(pair.Key, hasher), pair.Value, dateTime);
			}
			CommandExecuter ce = new CommandExecuter(instances);
			try
			{
				ce.ExecuteCommands(sets);
			}
			catch { }
		}
		public object[] MultiGet(string[] keys)
		{
			Get[] gets = new Get[keys.Length];
			for (int i = 0; i < keys.Length; i++)
			{
				gets[i] = new Get(new Key(keys[i], hasher));
			}
			CommandExecuter ce = new CommandExecuter(instances);
			try
			{
				ce.ExecuteCommands(gets);
			}
			catch { }
			object[] returnedObjects = new object[keys.Length];
			for (int i = 0; i < gets.Length; i++)
			{
				returnedObjects[i] = gets[i].RetrievedObject;
			}
			return returnedObjects;
		}
		 




		public void Set(string key, object value, DateTime expiry)
		{
			Set set = new Set(new Key(key, hasher), value, expiry);
			CommandExecuter ce = new CommandExecuter(instances);
			try
			{
				ce.Execute(set);
			}
			catch { }
		}

	}
	

	public class MemcachedClientException : Exception
	{
		internal MemcachedClientException(Key key, System.Net.IPEndPoint endPoint, Exception ex) : 
			base(string.Format("Key: {0} Hash: {1} IntValue: {2} IPEndPoint: {3}", key.Value, key.Hash, key.IntValue, endPoint.ToString()), ex) {}
	}
}

