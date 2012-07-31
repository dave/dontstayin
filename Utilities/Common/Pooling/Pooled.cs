using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Pooling
{
	public class Pooled<T> : IDisposable where T: IDisposable
	{
		Action<T> AddItemToPool;
		public Pooled(T t, Action<T> addItemToPool)
		{
			this.Item = t;
			this.AddItemToPool = addItemToPool;
			this.ItemCanBeReturnedToPool = false;
		}


		public void Dispose()
		{
			if (ItemCanBeReturnedToPool)
			{
				AddItemToPool(this.Item);
			}
			else
			{
				this.Item.Dispose();
			}
		}
		public bool ItemCanBeReturnedToPool { get; set; }
		public T Item { get; private set; }
	}
}
