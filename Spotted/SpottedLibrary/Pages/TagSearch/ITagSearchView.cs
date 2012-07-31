
using SpottedLibrary.Controls.SearchBoxControl;
using SpottedLibrary.Controls.TaggingControl;
using System;
using Bobs;
using SpottedLibrary.Controls.PhotoPage;
using SpottedLibrary.Controls.TagCloud;
namespace SpottedLibrary.Pages.TagSearch
{
	public interface ITagSearchView : IView, IPhotoPageView
	{
		ITagCloud TagCloud { get; }
		//ITaggingControl TaggingControl { get; }
		void Reload();
		bool SearchBoxControlVisible { set; }
		ISearchBoxControl SearchBoxControl { get; }
	}

}
