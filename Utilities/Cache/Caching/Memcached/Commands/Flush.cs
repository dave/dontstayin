using System;
using System.Collections.Generic;
using System.Text;

namespace Caching.Memcached.Commands
{
	internal class Flush : Command
	{
		public override void Execute(MemcachedSocket memcachedSocket)
		{
			memcachedSocket.WriteLine("flush_all");
			memcachedSocket.Flush();

			CheckResponse(memcachedSocket.ReadLine(), "OK");
		}
	}
}
