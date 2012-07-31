using System;
using System.Collections.Generic;
using System.Text;
using Bobs;
using SpottedLibrary.Controls.PhotoBrowserControl;
using SpottedLibrary.Controls.PhotoControl;
using SpottedLibrary.Controls.PagedRepeater;
using SpottedLibrary.Controls.ThreadControl;
using Common;

namespace SpottedLibrary.Controls.PhotoPage
{
	public interface IPhotoPageView : IView
	{
		string Title { get; set; }
		IThreadControl ThreadControl { get; }
		IPhotoBrowser PhotoBrowser { get; }
		Bobs.Photo PhotoFromUrl { get; }
	}
}
