using System;
using System.Collections.Generic;
using System.Text;
using SpottedLibrary.Controls.PaginationControl2;
using Common;

namespace SpottedLibrary.Controls.PagedRepeater
{
	public interface IPagedDataControl<T>
	{
		string Title { set; }
		IPaginationControl2 PaginationControl { get; }
		IPagedDataService<T> PagedDataService { set; get; }
		event EventHandler<EventArgs<IPagedDataService<T>>> PagedDataServiceChanged;

	}
	
}
