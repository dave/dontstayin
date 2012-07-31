using System;
using System.Collections.Generic;
using System.Text;

namespace Common.General
{
	[Serializable]
	public class LazyLoad<T>
	{
		bool loaded = false;
		Getter<T> getter = null;
		public Getter<T> Getter { 
			set
			{
				lock (getter)
				{
					getter = value;
				}
				Reset();
			}
		}
		T value;
		public T Value
		{
			get
			{
				if (!loaded)
				{
					lock (getter)
					{
						value = getter();
						loaded = true;
					}
				}
				return this.value;
			}
		}
		public LazyLoad(Getter<T> getter)
		{
			this.getter = getter;
		}
		public void Reset()
		{
			loaded = false;
		}
		static public implicit operator T(LazyLoad<T> lazyLoad)
		{
			return lazyLoad.Value;
		}

	}
}
