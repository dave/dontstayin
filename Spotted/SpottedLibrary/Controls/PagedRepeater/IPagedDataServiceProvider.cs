using System;
using System.Collections.Generic;
using System.Text;
using Common;

namespace SpottedLibrary.Controls.PagedRepeater
{
	public interface IPagedDataServiceProvider<T>
	{
		IPagedDataService<T> PagedDataService { get; }
	}
}
