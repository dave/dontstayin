using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace Caching.Memcached.Commands
{
	internal class BlockingDelete : KeyedCommand, ICanBeUsedInMultiCommand
	{
		public BlockingDelete(Key key) : base(key)
		{

		}

		public override void Execute(MemcachedSocket memcachedSocket)
		{
			BeforeFlush(memcachedSocket);
			memcachedSocket.Flush();
			AfterFlush(memcachedSocket);

		}


		#region ICanBeUsedInMultiCommand Members


		public void BeforeFlush(MemcachedSocket memcachedSocket)
		{
			memcachedSocket.WriteLine("set " + this.Key.Hash + " 0 0 0");
			memcachedSocket.WriteLine("");
			memcachedSocket.WriteLine("delete " + this.Key.Hash + " 1");
		}

		public void AfterFlush(MemcachedSocket memcachedSocket)
		{
			CheckResponse(memcachedSocket.ReadLine(), "STORED");
			CheckResponse(memcachedSocket.ReadLine(), "DELETED");
		}

		#endregion
	}
}
