using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Caching.Memcached
{
	public class Key
	{
		public Key(string value, IHasher hasher)
		{
			this.Value = value;
			this.bytes = hasher.GetBytes(string.Format("{0}_{1}_{2}",
				value, Common.Properties.SqlServer, Common.Properties.SqlDatabase));
			this.Hash = BitConverter.ToString(this.bytes).Replace("-", ""); ;
		}

		
		byte[] bytes;
		public string Value { get; private set; }

		
		public string Hash { get; private set; }

		uint? intValue;
		public uint IntValue
		{
			get
			{
				if (intValue == null)
				{
					intValue = BitConverter.ToUInt32(bytes, 0);
				}
				return intValue.Value;
			}
		}

	}
}
