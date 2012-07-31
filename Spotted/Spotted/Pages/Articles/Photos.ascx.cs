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
using SpottedLibrary.Pages.Events.Photos;
using Bobs;
using System.Collections.Generic;
using SpottedLibrary.Controls.PhotoBrowserControl;
using SpottedLibrary.Controls.PhotoControl;
using SpottedLibrary.Controls.PhotoBrowserWithPhoto;
using SpottedLibrary.Controls.PagedRepeater;
using SpottedLibrary.Controls.PhotoWithComments;
using SpottedLibrary.Controls.LatestChat;
using SpottedLibrary.Controls.ThreadControl;
using Common;
using SpottedLibrary.Pages.Articles.Photos;
using SpottedLibrary.Controls.PhotoPage;

namespace Spotted.Pages.Articles
{
	[ClientScript]
	public partial class Photos : DsiUserControl, IArticlePhotosView, IPhotoBrowserWithPhotoView, IPhotoWithCommentsView
	{
		ArticlePhotosController controller;
		PhotoBrowserWithPhotoController photoBrowserWithPhotoController;
		PhotoWithCommentsController photoWithCommentsController;
		public Photos()
		{
			controller = new ArticlePhotosController(this);
			photoBrowserWithPhotoController = new PhotoBrowserWithPhotoController(this);
			photoWithCommentsController = new PhotoWithCommentsController(this);
			this.Init += new EventHandler(Photos_Init);
		}
		protected void Photos_Init(object o, EventArgs e)
		{

		}

		protected string ArticleInfoHtml
		{
			get { return uiPhotoControl.ArticleInfoHtml; }
		}

		public static int NumberOfPhotosPerPage = Common.Properties.PhotoBrowser.IconsPerPage;
		public void Redirect(string url) { Response.Redirect(url); }
		public event EventHandler<EventArgs<int?>> DifferentGalleryChosen;
 

	 

		public string ArticleName
		{
			set { this.uiTitle.InnerText = value; }
		}


		public IPhotoBrowser PhotoBrowser { get { return uiPhotoBrowser; } }
		public IPhotoControl PhotoControl { get { return uiPhotoControl; } }


		public Photo PhotoFromUrl
		{
			get
			{
				if (ContainerPage.Url["photo"].IsInt)
				{
					if (photoFromUrl == null && ContainerPage.Url["photo"] > 0)
						photoFromUrl = new Photo(ContainerPage.Url["photo"]);
					return photoFromUrl;
				}
				else if (ContainerPage.Url.HasPhotoObjectFilter)
					return ContainerPage.Url.ObjectFilterPhoto;
				else
					return null;

				//return ContainerPage.Url["photo"].IsInt ? (int?)ContainerPage.Url["photo"] : null;
			}
		}
		Photo photoFromUrl;

		public Article ArticleFromUrl
		{
			get
			{
				if (ContainerPage.Url.HasArticleObjectFilter)
					return ContainerPage.Url.ObjectFilterArticle;
				else if (ContainerPage.Url.HasGalleryObjectFilter)
					return ContainerPage.Url.ObjectFilterGallery.Article;
				else if (ContainerPage.Url.HasPhotoObjectFilter)
					return ContainerPage.Url.ObjectFilterPhoto.Article;
				else
					return null;

				//return ContainerPage.Url["photo"].IsInt ? (int?)ContainerPage.Url["photo"] : null;
			}
		}





		public IPagedDataService<Bobs.Photo> CurrentDataService { get; set; }
		public Photo CurrentPhoto { get; set; }
		public IThreadControl ThreadControl { get { return uiThreadControl; } }
		public ILatestChat LatestChat { get { return uiLatestChat; } }



		public string Title
		{
			get { return Page.Title; }
			set { this.Page.Title = value; }
		}

		public IRelevanceHolder RelevanceHolder
		{
			get { return ContainerPage; }
		}

		protected void Page_PreRender(object o, EventArgs e)
		{
			this.uiArticleK.Value = ArticleFromUrl.K.ToString();
		}

	}
}
