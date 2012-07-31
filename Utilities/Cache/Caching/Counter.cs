using System;
using System.Collections.Generic;
using System.Text;

namespace Caching
{
	public class Counter
	{
		public Counter(string key)
			: this(() => 0, key) { }

		public Counter(Cache.Create<uint> retreiveValueDelegate, string key)
			: this(retreiveValueDelegate, key, null) { }

		public Counter(Cache.Create<uint> retrieveValueDelegate, string key, TimeSpan? localCacheLifespan)
		{
			this.RetrieveValue = retrieveValueDelegate;
			this.localCacheLifespan = localCacheLifespan;
			this.Key = key;
		}

		Cache.Create<uint> RetrieveValue;
		TimeSpan? localCacheLifespan;
		uint? value = null;
		public uint Value
		{
			get
			{
				if (value == null)
				{
					value = Instances.LocalCache.Get(Key) as uint?;
					if (value == null)
					{
						value = Instances.MainCounterStore.GetCounter(Key, RetrieveValue);
					}
				}
				return value.Value;
			}
			set
			{
				this.value = value;
				Instances.LocalCache.Store(Key, value);
				Instances.MainCounterStore.SetCounter(Key, value);
			}
		}
		public uint Increment()
		{
			value = Instances.MainCounterStore.Increment(Key, RetrieveValue);
			if (localCacheLifespan != null)
			{
				Instances.LocalCache.Delete(Key);
			}
			return Value;
			
		}
		private string Key { get; set; }
		public void ClearCacheAndRetrieveValue()
		{
			Value = RetrieveValue();
		}
	}
}
