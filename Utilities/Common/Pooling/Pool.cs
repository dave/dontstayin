using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Pooling
{
	public delegate T Getter<T>();
	public class Pool<T> : IDisposable where T : IDisposable
	{
		Getter<T> GetNewInstance;
		int MaximumSizeOfPool { get; set; }
		Queue<T> pool = new Queue<T>();
		Action<T> CleanUpItemForReuse;
		public Pool(Getter<T> getNewInstance, Action<T> cleanUpItemForReuse, int maximumSizeOfPool)
		{
			this.GetNewInstance = getNewInstance;
			this.MaximumSizeOfPool = maximumSizeOfPool;
			this.CleanUpItemForReuse = cleanUpItemForReuse;
		}

		public class AttemptMadeToGetItemFromDisposedPoolException : Exception { }
		bool disposed = false;
		
		public Pooled<T> Get()
		{
			lock (pool)
			{
				if (disposed) { throw new AttemptMadeToGetItemFromDisposedPoolException(); }
				if (pool.Count == 0)
				{
					return new Pooled<T>(GetNewInstance(), AddItemToPool);
				}
				else
				{
					return new Pooled<T>(pool.Dequeue(), AddItemToPool);
				}
			}
		}
		private void AddItemToPool(T t)
		{
			CleanUpItemForReuse(t);
			lock (pool)
			{
				if (pool.Count < MaximumSizeOfPool)
				{
					pool.Enqueue(t);
				}
				else
				{
					t.Dispose();
				}
			}
		}

		public void Dispose()
		{
			lock (pool)
			{
				while (pool.Count > 0)
				{
					pool.Dequeue().Dispose();
				}
				disposed = true;
			}
		}
	}
	
}
