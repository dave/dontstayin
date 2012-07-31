using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using Common;
namespace Caching
{
	public partial class Cache : ICounterStore, IDisposable
	{
		ICacheClient cacheClient;
		public string KeyNamespace { get; set; }
		public ICacheClient Client { get { return cacheClient; } }
		
		public bool CacheHit { get; private set; }
		
		public Cache(ICacheClient cacheClient, string keyNamespace)
		{
			KeyNamespace = keyNamespace;
			this.cacheClient = cacheClient;
		}
		string AddNamespace(string key)
		{
			return key + KeyNamespace;
		}

		#region Generic Cache Get
		public delegate T Create<T>();
		public T Get<T>(CacheKey key, Create<T> create)
		{
			return Get(key.ToString(), create);
		}
		public T Get<T>(string key, Create<T> create) 
		{
			return Get<T>(key, create, null);
		}

		public T Get<T>(string key, Create<T> create, TimeSpan? lifeSpan)
		{
			if (!lifeSpan.HasValue || lifeSpan.Value.Ticks > 0)
			{
				object o = cacheClient.Get(AddNamespace(key));
				if (o == null)
				{
					CacheHit = false;
					o = create();
					if (lifeSpan.HasValue)
					{
						Store(key, (T)o, lifeSpan.Value);
					}
					else
					{
						Store(key, (T)o);
					}

				}
				else
				{
					CacheHit = true;
				}
				return (T)o;
			}
			else
			{
				return create();
			}
		}
		#endregion



		public void Set(string key, object value)
		{
			cacheClient.Set(AddNamespace(key), value, DateTime.MaxValue);
		}

		public void Store<T>(string key, T value)
		{
			cacheClient.Store(AddNamespace(key), value, DateTime.MaxValue);
		}
		public void Store<T>(string key, T value, DateTime expiry)
		{
			cacheClient.Store(AddNamespace(key), value, expiry);
		}
		public void Store<T>(string key, T value, TimeSpan lifetime)
		{
			cacheClient.Store(AddNamespace(key), value, DateTime.Now.Add(lifetime));
		}
		public void Store<T>(CacheKey key, T value)
		{
			Store(key.ToString(), value);
		}
		public void Store<T>(CacheKey key, T value, DateTime expiry)
		{
			cacheClient.Store(AddNamespace(key.ToString()), value, expiry);
		}
		public void Store<T>(CacheKey key, T value, TimeSpan timespan)
		{
			cacheClient.Store(AddNamespace(key.ToString()), value, DateTime.Now.Add(timespan));
		}


		public object Get(string key) 
		{
			return cacheClient.Get(AddNamespace(key));
		}
		public T Get<T>(string key) where T : class
		{
			return cacheClient.Get(AddNamespace(key)) as T;
		}

		public void Delete(CacheKey cacheKey)
		{
			Delete(cacheKey.ToString());
		}

		public T[] MultiGet<T>(string[] keys)
		{
			//******************************
			//This uses cacheClient.MultiGet
			//******************************
			//object[] resultsAsObjects = cacheClient.MultiGet(keys.ConvertAll(key => AddNamespace(key)));
			//T[] results = new T[resultsAsObjects.Length];
			//for (int i = 0; i < resultsAsObjects.Length; i++)
			//{
			//    if (resultsAsObjects[i] != null)
			//    {
			//        results[i] = (T)resultsAsObjects[i];
			//    }
			//}
			//return results;

			//*************************
			//This uses cacheClient.Get
			//*************************
			T[] results = new T[keys.Length];
			for (int i = 0; i < keys.Length; i++)
			{
				object resultAsObject = cacheClient.Get(AddNamespace(keys[i]));
				if (resultAsObject != null)
				{
					results[i] = (T)resultAsObject;
				}
			}
			return results;
		}
		public T[] MultiGet<T, U>(U[] items, Getter<string, U> getCacheKey, Getter<T[], U[]> getValues, DateTime expiry)
		{
			string[] keys = items.ConvertAll(item => getCacheKey(item));
			T[] values = MultiGet<T>(keys);
			List<int> indexesOfMissingItems = values.FindAllIndexes(value => value == null);
			if (indexesOfMissingItems.Count > 0)
			{
				var tmp = indexesOfMissingItems.ConvertAll(index => items[index]).ToArray();
				T[] missingItems = getValues(tmp);
				for(int i=0;i<missingItems.Length;i++)
				{
					int index = indexesOfMissingItems[i];
					values[index] = missingItems[i];
				}
				KeyValuePair<string, T>[] pairsToBeSet = new KeyValuePair<string, T>[indexesOfMissingItems.Count];
				for (int i = 0; i < indexesOfMissingItems.Count; i++)
				{
					pairsToBeSet[i] = new KeyValuePair<string, T>(keys[indexesOfMissingItems[i]], values[indexesOfMissingItems[i]]);
				}
				MultiSet(pairsToBeSet, expiry);
			}
			return values;

		}

		public T[] MultiGet<T>(string[] keys, Getter<T>[] getters, DateTime expiry)
		{
			T[] values = MultiGet<T>(keys);
			List<int> indexesOfMissingItems = values.FindAllIndexes(value => value == null);
			if (indexesOfMissingItems.Count > 0)
			{
				foreach (int index in indexesOfMissingItems)
				{
					values[index] = getters[index]();
				}
				KeyValuePair<string, T>[] pairsToBeSet = new KeyValuePair<string, T>[indexesOfMissingItems.Count];
				for (int i = 0; i < indexesOfMissingItems.Count;i++ )
				{
					pairsToBeSet[i] = new KeyValuePair<string, T>(keys[indexesOfMissingItems[i]], values[indexesOfMissingItems[i]]);
				}
				MultiSet(pairsToBeSet, expiry);
			}
			return values;
		}
		public void MultiSet<T>(KeyValuePair<string, T>[] pairs, DateTime expiry)
		{
			cacheClient.MultiSet(
				pairs.ConvertAll(pair => new KeyValuePair<string, object>(AddNamespace(pair.Key), (object)pair.Value)),
				expiry
			);
			//foreach (KeyValuePair<string, T> pair in pairs)
			//{
			//    cacheClient.Set(AddNamespace(pair.Key), (object)pair.Value, expiry);
			//}

		}
		//public void MultiStore<T>(KeyValuePair<string, T>[] pairs, DateTime expiry)
		//{
		//    foreach (KeyValuePair<string, T> pair in pairs)
		//    {
		//        cacheClient.Store(AddNamespace(pair.Key), (T)pair.Value, expiry);
		//    }
		//}


		public static string GetBobsCacheKey(string tableName, string primaryKey)
		{
			return "DavesBobsCache-" + tableName + "-" + primaryKey;
		}
		
		

		public void Delete(string key)
		{
			cacheClient.BlockingDelete(AddNamespace(key));
		}

		public DataTable GetStats()
		{
			return cacheClient.GetStats();
		}

		#region ICounterStore Members

		public uint GetCounter(string key, Cache.Create<uint> create)
		{
			return Get<uint?>(key, () => create()).Value;
		}

		public void SetCounter(string key, uint value)
		{
			cacheClient.Store(AddNamespace(key), value, DateTime.MaxValue);
		}

		public uint Increment(string key, Cache.Create<uint> create)
		{
			uint? value = cacheClient.Increment(AddNamespace(key), 1);
			if (value == null)
			{
				value = create() + 1;
				cacheClient.Store(AddNamespace(key), value, DateTime.MaxValue);
				
			}
			return value.Value;
		}

		#endregion

		public void FlushAll()
		{
			cacheClient.FlushAll();
		}

		public void Dispose()
		{
			cacheClient.Dispose();
		}
	}
}
