using System;
using System.Collections.Generic;
using System.Text;

namespace Caching.Memcached.Commands
{
	internal interface ICanBeUsedInMultiCommand
	{
		Key Key { get; }
		void BeforeFlush(MemcachedSocket memcachedSocket);
		void AfterFlush(MemcachedSocket memcachedSocket);
		
	}
}
