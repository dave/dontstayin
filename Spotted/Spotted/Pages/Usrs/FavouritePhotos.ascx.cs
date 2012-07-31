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
using SpottedLibrary.Controls.PhotoBrowserWithPhoto;
using SpottedLibrary.Controls.PhotoWithComments;
using SpottedLibrary.Pages.Usrs.FavouritePhotos;

namespace Spotted.Pages.Usrs
{
	[ClientScript]
	public partial class FavouritePhotos : DsiUserControl, IPhotoBrowserWithPhotoView, IPhotoWithCommentsView, IUsrFavouritePhotosView
	{
		public FavouritePhotos()
		{
			new UsrFavouritePhotosController(this);
			new PhotoBrowserWithPhotoController(this);
			new PhotoWithCommentsController(this);
			this.Init += new EventHandler(FavouritePhotos_Init);
		}

		void FavouritePhotos_Init(object sender, EventArgs e)
		{
			
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				uiUsrK.Value = UsrFromUrl.K.ToString();
			}
		}

		public SpottedLibrary.Controls.PhotoControl.IPhotoControl PhotoControl
		{
			get { return this.uiPhotoControl; }
		}

		public Bobs.IRelevanceHolder RelevanceHolder
		{
			get { return ContainerPage; }
		}

		public string Title
		{
			get { return ContainerPage.Title; }
			set { this.ContainerPage.Title = value; }
		}

		public SpottedLibrary.Controls.ThreadControl.IThreadControl ThreadControl
		{
			get { return this.uiThreadControl; }
		}

		public SpottedLibrary.Controls.PhotoBrowserControl.IPhotoBrowser PhotoBrowser
		{
			get { return this.uiPhotoBrowser; }
		}


		public SpottedLibrary.Controls.LatestChat.ILatestChat LatestChat
		{
			get { return this.uiLatestChat; }
		}

		public Bobs.Usr UsrFromUrl
		{
			get
			{
				return ContainerPage.Url.ObjectFilterUsr;
			}
		}

		Bobs.Photo photoFromUrl;
		public Bobs.Photo PhotoFromUrl
		{
			get
			{
				if (photoFromUrl == null && ContainerPage.Url["photo"].IsInt && ContainerPage.Url["photo"] > 0)
				{
					photoFromUrl = new Bobs.Photo(ContainerPage.Url["photo"]);
				}
				return photoFromUrl;
			}
		}

	}
}
