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

namespace SpottedLibrary.Pages.Articles.Photos
{
	public interface IArticlePhotosView : IPhotoPageView
	{
		Bobs.Article ArticleFromUrl { get; }
	}
}
