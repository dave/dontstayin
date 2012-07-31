using System;
using System.Collections.Generic;
using System.Text;
using SpottedLibrary.Controls.PagedRepeater;
using Bobs;

namespace SpottedLibrary.Controls.PhotoBrowserControl
{
	public interface IPhotoBrowser : IPagedDataControl<Photo>
	{
		//int RowLength { set; }
		event EventHandler<EventArgs<int>> ItemClicked;
		Photo[] CurrentPageItems { get; }
		int PageSize { get; }
	}
}
