

using System;
namespace Caching.Memcached.Commands
{
	internal class Store : StoreDataCommand
	{
		


		internal Store(Key key, object objectToBeStored, DateTime expiry)
			: base(key, objectToBeStored, expiry)
		{
			
		}


		public override void Execute(MemcachedSocket memcachedSocket)
		{
			string response = SendCommandAndReadResponse(memcachedSocket, "add", "STORED", "NOT_STORED", "EXISTS");
			if (response == "EXISTS" || response == "NOT_STORED")
			{
				SendCommandAndReadResponse(memcachedSocket, "replace", "STORED", "NOT_STORED");
			}
		}


	}
}
