using System;
using System.Collections.Generic;
using System.Text;
using Bobs;
using Common;
using SpottedLibrary.Controls.PhotoBrowserControl;
using SpottedLibrary.Controls.PhotoPage;
using Pair = System.Collections.Generic.KeyValuePair<object, Bobs.OrderBy.OrderDirection>;
namespace SpottedLibrary.Pages.Videos
{
	public class VideosController : PhotoPageController
	{
		public VideosController(IVideosView view)
			: base(view)
		{ }

		protected override IPagedDataService<Photo> GetPagedDataService()
		{
			return Photo.GetRecentVideos();
		}

		protected override string GetTitle()
		{
			return "Recent videos";
		}
	}
}
