using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Caching.Memcached
{
	public class SHA1Hasher : IHasher
	{
		#region IHasher Members

		public byte[] GetBytes(string key)
		{
			byte[] data = System.Text.Encoding.ASCII.GetBytes(key.ToCharArray());
			return new SHA1CryptoServiceProvider().ComputeHash(data);
		}

		#endregion
	}
}
