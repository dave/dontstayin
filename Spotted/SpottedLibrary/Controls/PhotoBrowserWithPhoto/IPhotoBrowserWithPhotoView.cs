using System;
using System.Collections.Generic;
using System.Text;
using SpottedLibrary.Controls.PhotoControl;
using SpottedLibrary.Controls.PhotoBrowserControl;
using SpottedLibrary.Controls.PagedRepeater;
using Bobs;
using Common;
using SpottedLibrary.Controls.PhotoPage;

namespace SpottedLibrary.Controls.PhotoBrowserWithPhoto
{
	public interface IPhotoBrowserWithPhotoView : IPhotoPageView
	{
		IPhotoControl PhotoControl { get; }
		void Redirect(string url);
		IRelevanceHolder RelevanceHolder { get; }
		
	}
}
