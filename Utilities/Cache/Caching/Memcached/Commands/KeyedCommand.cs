using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace Caching.Memcached.Commands
{
	abstract class KeyedCommand : Command
	{
		public Key Key { get; private set; }
		public KeyedCommand(Key key)
		{
			this.Key = key;
		}
	
	}
}
