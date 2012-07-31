using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Collections
{
	public class PriorityList<T> : List<T> where T : IHasPriority
	{
		
		
		public new void Add(T value)
		{
			for (int i = Count - 1;i>-1; i--)
			{
				if (i==0 || this[i].Priority <= value.Priority)
				{
					Insert(i, value);
					return;
				}
			}
		}

		 
	}
	public class PriorityItem<T>
	{
		public PriorityItem(int priority, T value)
		{
			this.Priority = priority;
			this.Value = value;
		}
		public int Priority { get; private set; }
		public T Value { get; private set; }
	}
	public interface IHasPriority
	{
		int Priority { get; }
	}
}
