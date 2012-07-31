using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using Common.Pooling;
using System.Net;
using System.Data;
using Caching.Memcached.Commands;

namespace Caching.Memcached
{
	public class MemcachedInstances : IDisposable
	{
		List<MemcachedInstance> memcachedInstances = new List<MemcachedInstance>();
		public MemcachedInstances(int maximumSizeOfConnectionPool, params IPEndPoint[] endPoints)
		{
			foreach (IPEndPoint endPoint in endPoints)
			{
				memcachedInstances.Add(new MemcachedInstance(endPoint, maximumSizeOfConnectionPool));
			}
			
		}
		internal int GetMemcachedInstanceIndex(Key key)
		{
			return (int)(key.IntValue % memcachedInstances.Count);
		}

		internal MemcachedInstance GetMemcachedInstance(Key key)
		{
			return memcachedInstances[GetMemcachedInstanceIndex(key)];
		}
		internal MemcachedInstance GetMemcachedInstance(int index)
		{
			return memcachedInstances[index];
		}
		internal int Count
		{
			get
			{
				return memcachedInstances.Count;
			}
		}
		public void Dispose()
		{
			foreach (var memcachedInstance in memcachedInstances)
			{
				memcachedInstance.Dispose();
			}
		}
 
		internal DataTable Stats
		{
			get
			{
				DataTable stats = new DataTable("Stats");
				foreach (MemcachedInstance memcachedInstance in memcachedInstances)
				{
					using (Pooled<MemcachedSocket> pooledSocket = memcachedInstance.SocketPool.Get())
					{
						GetStats getStats = new GetStats();
						getStats.Execute(pooledSocket.Item);
						getStats.Stats.Insert(0, new KeyValuePair<string,string>("server_ip", pooledSocket.Item.EndPoint.ToString()));
						foreach (var pair in getStats.Stats)
						{
							if (!stats.Columns.Contains(pair.Key))
							{
								stats.Columns.Add(pair.Key);
							}
						}
						DataRow row = stats.NewRow();
						foreach (var pair in getStats.Stats)
						{
							row[pair.Key] = pair.Value;
						}
						stats.Rows.Add(row);
						pooledSocket.ItemCanBeReturnedToPool = true;
					}
				}
				return stats;
			}
		}

		internal void FlushAll()
		{
			foreach (var memcachedInstance in memcachedInstances)
			{
				using (Pooled<MemcachedSocket> pooledSocket = memcachedInstance.SocketPool.Get())
				{
					Flush flush = new Flush();
					flush.Execute(pooledSocket.Item);
				}
			}
		}


	}
}
