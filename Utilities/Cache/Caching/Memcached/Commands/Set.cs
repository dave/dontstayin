

using System;
namespace Caching.Memcached.Commands
{
	internal class Set : StoreDataCommand, ICanBeUsedInMultiCommand
	{

		internal const string Command = "set";
		internal const string Response0 = "STORED";
		internal const string Response1 = "NOT_STORED";
		internal const string Response2 = "EXISTS";

		internal Set(Key key, object objectToBeStored, DateTime expiry)
			: base(key, objectToBeStored, expiry)
		{

		}


		public override void Execute(MemcachedSocket memcachedSocket)
		{
			string response = SendCommandAndReadResponse(memcachedSocket, Command, Response0, Response1, Response2);
		}



		#region ICanBeUsedInMultiCommand Members

		public void BeforeFlush(MemcachedSocket memcachedSocket)
		{
			this.WriteCommandToStream(memcachedSocket, Set.Command);
		}

		public void AfterFlush(MemcachedSocket memcachedSocket)
		{
			string response = this.ReadResponseFromStream(memcachedSocket);
			CheckResponse(response, Response0, Response1, Response2);
		}

		#endregion
	}
}
