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

namespace Spotted.Pages.Events
{
	[ClientScript]
	public partial class Photos : EventUserControl, IEventPhotosView, IPhotoBrowserWithPhotoView, IPhotoWithCommentsView, IIncludesJs
	{
		EventPhotosController controller;
		PhotoBrowserWithPhotoController photoBrowserWithPhotoController;
		PhotoWithCommentsController photoWithCommentsController;
		public Photos()
		{
			controller = new EventPhotosController(this);
			photoBrowserWithPhotoController = new PhotoBrowserWithPhotoController(this);
			photoWithCommentsController = new PhotoWithCommentsController(this);
			this.Init += new EventHandler(Photos_Init);
		}

		public void IncludeJsInternal() { IncludeJs(this.Page); }
		public static void IncludeJs(Page page)
		{
			Spotted.Controls.PhotoBrowser.IncludeJs(page);
			Spotted.Controls.ThreadControl.IncludeJs(page);
			Spotted.Controls.LatestChat.IncludeJs(page);
			Spotted.Pages.Events.EventUserControl.IncludeJs(page);

			ScriptSharp.RegisterInclude(page, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		}

		protected string EventInfoHtml
		{
			get { return uiPhotoControl.EventInfoHtml; }
		}

		void Photos_Init(object sender, EventArgs e)
		{
			//using Js.Controls.PhotoBrowser;
			//using PhotoControlController = Js.Controls.PhotoControl.Controller;
			//using PhotoBrowserController = Js.Controls.PhotoBrowser.Controller;
			//using ThreadControlController = Js.Controls.ThreadControl.Controller;
			//using LatestChatController = Js.Controls.LatestChat.Controller;
			//using PhotoBrowserPhotoProvider = Js.Controls.PhotoBrowser.PhotoProvider;
			

			if (GalleryFromUrl != null && GalleryFromUrl.LivePhotos == 0)
			{
				GalleryHasPhotos.Visible = false;
				GalleryHasNoPhotos.Visible = true;

				NoPhotosRetryLink.HRef = GalleryFromUrl.Url();
				NoPhotosEventLink.HRef = GalleryFromUrl.Event.Url();
				NoPhotosGalleryEditLink.HRef = GalleryFromUrl.UrlApp("edit");

				return;
			}

			this.uiCurrentGallery.SelectedIndexChanged += new EventHandler(uiCurrentGallery_SelectedIndexChanged);

			this.uiEventK.Value = EventFromUrl == null ? "0" : EventFromUrl.K.ToString();

			this.uiGalleryK.Value = GalleryFromUrl == null ? "-1" : GalleryFromUrl.K.ToString();

			if (GalleryFromUrl != null)
				GalleryFromUrl.SetGalleryUsr(GalleryFromUrl.LivePhotos);

			this.photoBrowserWithPhotoController.GallerySelected = GalleryFromUrl != null;

			

		}


		public static int NumberOfPhotosPerPage = Common.Properties.PhotoBrowser.IconsPerPage;

		public Gallery GalleryFromUrl 
		{ 
			get 
			{
				if (ContainerPage.Url["gallery"].IsInt)
				{
					if (galleryFromUrl == null && ContainerPage.Url["gallery"] > 0)
						galleryFromUrl = new Gallery(ContainerPage.Url["gallery"]);
					return galleryFromUrl;
				}
				else if (ContainerPage.Url.HasGalleryObjectFilter)
					return ContainerPage.Url.ObjectFilterGallery;
				else if (ContainerPage.Url.HasPhotoObjectFilter)
					return ContainerPage.Url.ObjectFilterPhoto.Gallery;
				else
					return null;
				//return ContainerPage.Url["gallery"].IsInt ? (int?)ContainerPage.Url["gallery"] : null; 
			} 
		}
		Gallery galleryFromUrl;

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

		public Event EventFromUrl
		{
			get
			{
				if (ContainerPage.Url.HasEventObjectFilter)
					return ContainerPage.Url.ObjectFilterEvent;
				else if (ContainerPage.Url.HasGalleryObjectFilter)
					return ContainerPage.Url.ObjectFilterGallery.Event;
				else if (ContainerPage.Url.HasPhotoObjectFilter)
					return ContainerPage.Url.ObjectFilterPhoto.Event;
				else
					return null;
				
				//return ContainerPage.Url["photo"].IsInt ? (int?)ContainerPage.Url["photo"] : null;
			}
		}
	 
		public List<KeyValuePair<string, int>> GalleryNamesAndKs
		{
			set {
				this.uiCurrentGallery.Items.Clear();
				this.uiCurrentGallery.Items.Add(new ListItem("<All galleries>", "-1"));
				value.ForEach(pair => uiCurrentGallery.Items.Add(new ListItem(pair.Key, pair.Value.ToString())));
			}
		}

		public void Redirect(string url) 
		{
			Response.Redirect(url);
		}


		protected void uiCurrentGallery_SelectedIndexChanged(object sender, EventArgs e)
		{
			
			if(SelectedGalleryChanged != null) SelectedGalleryChanged(this, new EventArgs<int>(int.Parse(uiCurrentGallery.SelectedValue)));
		}


		public int SelectedGalleryK
		{
			set { this.uiCurrentGallery.SelectedValue = value.ToString(); }
			get { return int.Parse(this.uiCurrentGallery.SelectedValue); }
		}


		public IPhotoBrowser PhotoBrowser { get { return uiPhotoBrowser; } }
		public IPhotoControl PhotoControl { get { return uiPhotoControl; } }




		

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

		public event EventHandler<EventArgs<int>> SelectedGalleryChanged;

	}
}
