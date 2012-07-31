using System;
using System.Collections.Generic;
using System.Text;

namespace Caching.Memcached.Commands
{
	class Increment : KeyedCommand, ICanBeUsedInMultiCommand
	{
		long amount;
		internal uint? RetrievedValue { get; private set; }
		internal Increment(Key key, long amount)
			: base(key)
		{
			this.amount = amount;
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
			string command;
			uint absAmount;
			if (amount >= 0)
			{
				command = "incr";
				absAmount = (uint)amount;
			}
			else
			{
				command = "decr";
				absAmount = (uint)-amount;
			}
			memcachedSocket.WriteLine(String.Format("{0} {1} {2}", command, this.Key.Hash, absAmount));
		}

		public void AfterFlush(MemcachedSocket memcachedSocket)
		{
			string response = memcachedSocket.ReadLine();
			if (response == "NOT_FOUND") { return; }
			uint result;
			if (uint.TryParse(response, out result))
			{
				RetrievedValue = result;
			}
			else
			{
				throw new UnexpectedResponseMatchException(response, "NOT_FOUND", "<uint value>");
			}
		}

		#endregion
	}
}
