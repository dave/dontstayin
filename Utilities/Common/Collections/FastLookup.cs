using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Classes
{
	public class FastLookup<T>
	{
		Dictionary<T, object> hashTable = new Dictionary<T, object>();

		public bool Contains(T value)
		{
			return hashTable.ContainsKey(value);
		}

		public void Add(T value)
		{
			hashTable.Add(value, null);
		}
		
	}
}
