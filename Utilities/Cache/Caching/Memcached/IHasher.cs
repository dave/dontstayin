using System;
using System.Collections.Generic;
using System.Text;

namespace Caching.Memcached
{
	public interface IHasher
	{
		byte[] GetBytes(string key);
	}
}
