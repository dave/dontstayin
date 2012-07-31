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
using SpottedLibrary.Controls.PhotoBrowserWithPhoto;
using SpottedLibrary.Controls.PhotoBrowserControl;
using SpottedLibrary.Controls.PhotoControl;
using SpottedLibrary.Controls.PhotoWithComments;
using SpottedLibrary.Controls.ThreadControl;
using SpottedLibrary.Pages.Videos;


namespace Spotted.Pages
{
	public partial class Videos : DsiUserControl, IPhotoBrowserWithPhotoView, IVideosView, IPhotoWithCommentsView
	{
		public Videos()
		{
			new PhotoBrowserWithPhotoController(this);
			new PhotoWithCommentsController(this);
			new VideosController(this);
			this.Init += new EventHandler(Videos_Init);
		}

		void Videos_Init(object sender, EventArgs e)
		{
		}

		protected void Page_Load(object sender, EventArgs e)
		{

		}

		public string Title
		{
			get { return this.uiTitle.InnerText; }
			set { this.uiTitle.InnerText = value; }
		}

		public IThreadControl ThreadControl
		{
			get { return this.uiThreadControl; }
		}

		public IPhotoBrowser PhotoBrowser
		{
			get { return this.uiVideoBrowser; }
		}

		public Photo PhotoFromUrl
		{
			get { return null; }
		}

		public IPhotoControl PhotoControl
		{
			get { return this.uiVideoControl; }
		}

		public IRelevanceHolder RelevanceHolder
		{
			get { return ContainerPage; }
		}

		public Photo CurrentPhoto
		{
			set { }
		}


		public SpottedLibrary.Controls.LatestChat.ILatestChat LatestChat
		{
			get { return this.uiLatestChat; }
		}
	}
}
