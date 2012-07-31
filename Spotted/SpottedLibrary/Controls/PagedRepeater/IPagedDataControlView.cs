using SpottedLibrary.Controls.PhotoBrowserControl;
using System;
using System.Collections.Generic;
using System.Text;
using Common;
using SpottedLibrary.Controls.PaginationControl2;

namespace SpottedLibrary.Controls.PagedRepeater
{
	public interface IPagedDataControlView<T> : IView
	{
		int NumberOfItems { set; }
		int PageSize { set; get; }
		
		T[] CurrentPageItems { set; get; }
		IPaginationControl2 PaginationControl { get; }
		event EventHandler<EventArgs<IPagedDataService<T>>> PagedDataServiceChanged;
		IPagedDataService<T> PagedDataService { get; }
		bool DisplayNoResultsMessage { set; }
	}
}
