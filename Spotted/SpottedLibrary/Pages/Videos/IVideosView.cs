using System;
using System.Collections.Generic;
using System.Text;
using Bobs;
using SpottedLibrary.Controls.PhotoBrowserControl;
using SpottedLibrary.Controls.PhotoControl;
using SpottedLibrary.Controls.PagedRepeater;
using SpottedLibrary.Controls.ThreadControl;
using Common;
using SpottedLibrary.Controls.PhotoPage;

namespace SpottedLibrary.Pages.Videos
{
	public interface IVideosView : IPhotoPageView
	{
		void Redirect(string url);
		Photo CurrentPhoto { set; }
	}
}
