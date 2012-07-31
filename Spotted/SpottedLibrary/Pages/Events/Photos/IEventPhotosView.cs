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

namespace SpottedLibrary.Pages.Events.Photos
{
	public interface IEventPhotosView : IPhotoPageView
	{
		List<KeyValuePair<string, int>> GalleryNamesAndKs { set; }
		void Redirect(string url);
		int SelectedGalleryK { set; get; }
		Photo CurrentPhoto { set; }
		event EventHandler<EventArgs<int>> SelectedGalleryChanged;
		Bobs.Event EventFromUrl { get; }
		Bobs.Gallery GalleryFromUrl { get; }
		
	}
}
