using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SpottedLibrary.Pages.TagSearch;
using SpottedLibrary.Controls.PhotoBrowserControl;
using SpottedLibrary.Controls.PagedRepeater;
using System.Collections.Generic;
using Common;
using SpottedLibrary.Controls.PhotoControl;
using SpottedLibrary.Controls.SearchBoxControl;
using SpottedLibrary.Controls.PhotoBrowserWithPhoto;
using SpottedLibrary.Controls.PhotoWithComments;
using SpottedLibrary.Controls.LatestChat;
using SpottedLibrary.Controls.ThreadControl;
using SpottedLibrary.Controls.TaggingControl;
using Bobs;
using SpottedLibrary.Controls.TagCloud;

namespace Spotted.Pages
{
	public partial class TagSearch : DsiUserControl, ITagSearchView, IPhotoWithCommentsView, IPhotoBrowserWithPhotoView
	{
		TagSearchController controller;
		PhotoWithCommentsController photoWithCommentsController;
		PhotoBrowserWithPhotoController photoBrowserWithPhotoController;
		public TagSearch()
		{
			this.controller = new TagSearchController(this);
			this.photoWithCommentsController = new PhotoWithCommentsController(this);
			this.photoBrowserWithPhotoController = new PhotoBrowserWithPhotoController(this);
			this.Init += new EventHandler(TagSearch_Init);
		}

		void TagSearch_Init(object sender, EventArgs e)
		{
			
		}

		public ISearchBoxControl SearchBoxControl { get { return uiSearchBoxControl; } }
		public Uri RequestUrl { get { return Request.Url; } }
		public IPhotoBrowser PhotoBrowser { get { return uiPhotoBrowser; } }
		public IPhotoControl PhotoControl { get { return uiPhotoControl; } }


		Photo photoFromUrl;
		public Bobs.Photo PhotoFromUrl
		{
			get
			{
				if (photoFromUrl == null && ContainerPage.Url["photo"].IsInt && ContainerPage.Url["photo"] > 0)
				{
					photoFromUrl = new Photo(ContainerPage.Url["photo"]);
				}
				return photoFromUrl;
			}
		}



		public IThreadControl ThreadControl { get { return uiThreadControl; } }
		public ILatestChat LatestChat { get { return uiLatestChat; } }




		//public ITaggingControl TaggingControl
		//{
		//    get { return uiPhotoControl.TaggingControl; }
		//}

		public void Reload()
		{
			Response.Redirect(Request.Url.ToString());
		}



		public string Title
		{
			get { return ContainerPage.Title; }
			set { ; }
		}

		public IRelevanceHolder RelevanceHolder
		{
			get { return ContainerPage; }
		}



		public ITagCloud TagCloud
		{
			get { return uiTagCloud; }
		}

		#region IPageWithTagSearch Members


		public bool SearchBoxControlVisible
		{
			set
			{
				this.uiSearchBoxPanel.Visible = value;
				this.uiTitle.Visible = value;
			}
		}


		protected void Page_PreRender(object o, EventArgs e)
		{
			string searchString = SearchQuery.Parse(this.Url).SearchString;
			this.ContainerPage.Title = "Photos tagged \"" + searchString + "\"";
			try
			{
				uiTagK.Value = Tag.GetTag(searchString).K.ToString();
			}
			catch(InvalidTagException)
			{
			}
		}
		#endregion
	}
}
