using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Search
{
	public class BreadthFirstSearch<T> where T : class
	{
		private readonly T head;
		private readonly Func<T, IEnumerable<T>> getChildren;
		private readonly Predicate<T> isMatch;

		public BreadthFirstSearch(T head, Func<T, IEnumerable<T>> getChildren, Predicate<T> isMatch)
		{
			this.head = head;
			this.getChildren = getChildren;
			this.isMatch = isMatch;
		}

		public IEnumerable<T> Execute()
		{
			var item = this.head;
			var queue = new Queue<T>();
			do
			{
				if (isMatch(item)) yield return this.head;
				foreach (var child in getChildren(item))
				{
					queue.Enqueue(child);
				}
				item = queue.Dequeue();
			} while (queue.Count > 0);
		}
	}

	public delegate TResult Func<TParam, TResult>(TParam param);
}
