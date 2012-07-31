using System;
using System.Data;
using System.Collections.Generic;
namespace Caching
{
	public interface ICacheClient : IDisposable
	{
		void BlockingDelete(string key);
		void FlushAll();
		object Get(string key);
		uint? Increment(string key, long value);
		void Store(string key, object value, DateTime expiry);
		void MultiSet(KeyValuePair<string, object>[] pairs, DateTime expiry);
		object[] MultiGet(string[] keys);
		DataTable GetStats();
		void Set(string key, object value, DateTime expiry);
		void Dispose();
	}
}
