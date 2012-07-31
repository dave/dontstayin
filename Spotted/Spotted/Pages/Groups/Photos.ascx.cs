using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SpottedLibrary.Controls.PhotoBrowserControl;
using SpottedLibrary.Controls.PhotoControl;
using SpottedLibrary.Controls.PhotoBrowserWithPhoto;
using SpottedLibrary.Controls.PagedRepeater;
using SpottedLibrary.Controls.PhotoWithComments;
using SpottedLibrary.Controls.LatestChat;
using SpottedLibrary.Controls.ThreadControl;
using Bobs;
using SpottedLibrary.Pages.Groups.Photos;

namespace Spotted.Pages.Groups
{
	[ClientScript]
	public partial class Photos : DsiUserControl, IPhotoBrowserWithPhotoView, IPhotoWithCommentsView, IGroupPhotosView
	{
		GroupPhotosController controller;
		PhotoBrowserWithPhotoController photoBrowserWithPhotoController;
		PhotoWithCommentsController photoWithCommentsController;
		public Photos()
		{
			controller = new GroupPhotosController(this);
			photoBrowserWithPhotoController = new PhotoBrowserWithPhotoController(this);
			photoWithCommentsController = new PhotoWithCommentsController(this);
			this.Init += new EventHandler(Photos_Init);
		}

		void Photos_Init(object sender, EventArgs e)
		{
		}

		protected Group CurrentGroup { get { return Url.ObjectFilterGroup; } }

		protected void Page_Load()
		{
			this.Page.Title = CurrentGroup.Name + " photos";
			this.uiGroupK.Value = CurrentGroup.K.ToString();
		}

		public static int NumberOfPhotosPerPage = Common.Properties.PhotoBrowser.IconsPerPage;

		public IPhotoBrowser PhotoBrowser { get { return uiPhotoBrowser; } }
		public IPhotoControl PhotoControl { get { return uiPhotoControl; } }

		public Photo CurrentPhoto { get; set; }
		public IThreadControl ThreadControl { get { return uiThreadControl; } }
		public ILatestChat LatestChat { get { return uiLatestChat; } }


		public string Title
		{
			get { return ContainerPage.Title; }
			set { ; }
		}

		public IRelevanceHolder RelevanceHolder
		{
			get { return ContainerPage; }
		}

		void IPhotoBrowserWithPhotoView.Redirect(string url)
		{
			Response.Redirect(url);
		}



		#region IGroupPhotosView Members

		public Group GroupFromUrl
		{
			get { return CurrentGroup; }
		}

		#endregion

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


	}
}
