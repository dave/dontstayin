using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Caching.Memcached.Commands
{
	class GetStats : Command
	{
		internal GetStats()
		{
			Stats = new List<KeyValuePair<string, string>>();
		}
		internal List<KeyValuePair<string, string>> Stats { get; set; }
		public override void Execute(MemcachedSocket memcachedSocket)
		{
			memcachedSocket.WriteLine("stats");
			string line = memcachedSocket.ReadLine();
			while (line.StartsWith("STAT"))
			{
				int indexOfStatName = line.IndexOf(" ") + 1;
				int indexOfStatValue = line.IndexOf(" ", indexOfStatName);
				string name = line.Substring(indexOfStatName, indexOfStatValue - indexOfStatName);
				string value = line.Substring(indexOfStatValue, line.Length - indexOfStatValue);
				Stats.Add(new KeyValuePair<string, string>(name, value));
				line = memcachedSocket.ReadLine();
			}
			CheckResponse(line, "END");
		}
	}
}
