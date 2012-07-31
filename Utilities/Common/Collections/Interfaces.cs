using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Collections
{
	public interface ICountable
	{
		int Count { get; }
	}

	public interface IIndexable<T> : IEnumerable<T>
	{
		T this[int index] { get; }
	}

}
