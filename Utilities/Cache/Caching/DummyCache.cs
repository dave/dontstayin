using System;
using System.Collections.Generic;
using System.Text;

namespace Caching
{
	public class DummyCache : ICacheClient
	{
		#region ICacheClient Members

		public void BlockingDelete(string key)
		{
		}

		public void FlushAll()
		{
		}

		public object Get(string key)
		{
			return null;
		}

		public uint? Increment(string key, long value)
		{
			return null;
		}

		public void Store(string key, object value, DateTime expiry)
		{
		}

		public System.Data.DataTable GetStats()
		{
			return new System.Data.DataTable();
		}

		#endregion


		public void Dispose()
		{
			throw new NotImplementedException();
		}

		#region ICacheClient Members


		public void MultiSet(KeyValuePair<string, object>[] pairs, DateTime expiry)
		{
		
		}

		public object[] MultiGet(string[] keys)
		{
			return new object[keys.Length];
		}



		public void Set(string key, object value, DateTime expiry)
		{
		}

		#endregion
	}
}
