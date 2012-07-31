using System;
using System.Collections.Generic;
using System.Text;
using Common.Pooling;
using System.Net;
using System.Net.Sockets;

namespace Caching.Memcached
{
	public class MemcachedInstance  : IDisposable
	{
		public Pool<MemcachedSocket> SocketPool { get; private set; }
		public IPEndPoint IPEndPoint { get; private set; }
		public MemcachedInstance(IPEndPoint endPoint, int maximumSizeOfPool)
		{
			this.IPEndPoint = endPoint;
			SocketPool = new Pool<MemcachedSocket>(() => new MemcachedSocket(IPEndPoint), CleanUpSocket, maximumSizeOfPool);
		}

		void CleanUpSocket(MemcachedSocket socket)
		{
			socket.DiscardDataInStream();
		}
	
		public void Dispose()
		{
			SocketPool.Dispose();
		}

	}
}
