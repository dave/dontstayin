using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
	public interface IPagedDataService<T>
	{
		int Count { get; }
		T[] Page(int pageNumber, int pageSize);
	}
}
