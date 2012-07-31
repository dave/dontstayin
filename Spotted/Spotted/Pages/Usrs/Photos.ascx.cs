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
using Bobs;
using SpottedLibrary.Controls.PhotoWithComments;
using SpottedLibrary.Controls.PhotoBrowserWithPhoto;
using SpottedLibrary.Pages.Usrs.Photos;

namespace Spotted.Pages.Usrs
{
	[ClientScript]
	public partial class Photos : DsiUserControl, IPhotoBrowserWithPhotoView, IPhotoWithCommentsView, IUsrPhotosView
	{
		public Photos()
		{
			new UsrPhotosController(this);
			new PhotoBrowserWithPhotoController(this);
			new PhotoWithCommentsController(this);
			this.Init += new EventHandler(Photos_Init);
		}

		void Photos_Init(object sender, EventArgs e)
		{
			
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				uiUsrK.Value = UsrFromUrl.K.ToString();
				uiSpottedByUsrK.Value = SpottedByUsrK.ToString();

				UsrIntro.Header = "Photos of " + UsrFromUrl.NickName;
				if (SpottedByUsr != null)
				{
					TakenBySpan.InnerHtml =
						" that were taken by " +
						SpottedByUsr.Link() +
						". We can remove this filter and <a href=\"" +
						ContainerPage.Url.CurrentUrl("by", null) +
						"\">show all " +
						UsrFromUrl.HisString(false) +
						" photos</a>";
				}
			}
		}

		public string GetMonthUrl(DateTime d, params object[] par)
		{
			if (SpottedByUsr != null)
				return UsrFromUrl.UrlMyPhotosTakenByMonth(SpottedByUsr, d);
			else
				return UsrFromUrl.UrlMyPhotosMonth(d);
		}

		#region IPhotoBrowserWithPhotoView Members

		public SpottedLibrary.Controls.PhotoControl.IPhotoControl PhotoControl
		{
			get { return this.uiPhotoControl; }
		}

		public Bobs.IRelevanceHolder RelevanceHolder
		{
			get { return ContainerPage; }
		}

		#endregion

		#region IPhotoPageView Members

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

		#endregion

		#region IPhotoWithCommentsView Members


		public SpottedLibrary.Controls.LatestChat.ILatestChat LatestChat
		{
			get { return this.uiLatestChat; }
		}

		#endregion

		#region IUsrPhotosView Members

		public Bobs.Usr UsrFromUrl
		{
			get
			{
				return ContainerPage.Url.ObjectFilterUsr;
			}
		}

		#region SpottedByUsr
		private bool hasCheckedSpottedByUsr;
		private Usr spottedByUsr;
		public Usr SpottedByUsr
		{
			get
			{
				if (!hasCheckedSpottedByUsr)
				{
					hasCheckedSpottedByUsr = true;
					string spotterNickname = ContainerPage.Url["by"];
					if (!string.IsNullOrEmpty(spotterNickname))
					{
						spottedByUsr = Bobs.Usr.GetFromNickName(spotterNickname);
					}
				}
				return spottedByUsr;
			}
		}
		public int SpottedByUsrK
		{
			get { return (SpottedByUsr != null) ? SpottedByUsr.K : -1; }
		}
		#endregion

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
