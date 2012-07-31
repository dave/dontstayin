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
using Bobs;

namespace Spotted.Pages.Galleries
{
	public partial class Add : DsiUserControl
	{
		protected Panel NoEditArticlePanel;
		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn();

			if (CurrentEvent != null)
			{
				if (CurrentEvent.NoPhotos)
				{
					ChangePanel(PanelNoPhoto);
				}
				else if (!IsFuture && !HasGalleries)
				{
					AddGallery();
				}
				else if (!IsFuture && HasGalleries && ContainerPage.Url["New"] == 1)
				{
					if (!Usr.Current.IsSpotter)
					{
						ChangePanel(CantAddGallery);

						EditCurrentGalleryLink.InnerText = galleries[0].Name;
						EditCurrentGalleryLink.HRef = galleries[0].UrlApp("edit");
					}
					else
						AddGallery();
				}
				else
				{
					if (HasGalleries && !IsFuture)
						ChangePanel(EventHasGalleriesPanel);
					else if (IsFuture)
						ChangePanel(FutureEventPanel);
				}
			}
			else if (CurrentArticle != null)
			{
				if (!Usr.Current.CanEdit(CurrentArticle))
					ChangePanel(NoEditArticlePanel);
				else
				{
					//try to find gallery in this article...
					GallerySet gs = new GallerySet(new Query(new Q(Gallery.Columns.ArticleK, ArticleK)));
					if (gs.Count == 0)
						AddGallery();
					else
						Response.Redirect(gs[0].UrlApp("edit"));
				}
			}
			else
			{
				AddGallery();
			}
			//throw new Exception("Must upload to either an event or an article");
		}

		protected Panel CantAddGallery, PanelNoPhoto;
		protected HtmlAnchor EditCurrentGalleryLink;

		void AddGallery()
		{
			Gallery g = new Gallery();
			g.CreateDateTime = DateTime.Now;
			g.Name = Usr.Current.NickName + "'s photos";
			g.OwnerUsrK = Usr.Current.K;
			g.RunFinishedUploadingTask = false;

			if (CurrentEvent != null)
				g.EventK = EventK;
			else if (CurrentArticle != null)
				g.ArticleK = ArticleK;

			g.LivePhotos = 0;
			g.TotalPhotos = 0;
			g.UpdateUrlFragmentNoUpdate();
			g.Update();

			if (CurrentEvent != null)
			{
				Usr.Current.AttendEvent(CurrentEvent.K, true, Usr.Current.IsSpotter, null);
			}

			Response.Redirect(g.UrlApp("edit"));
		}

		#region EventHasGalleriesPanel
		protected Panel EventHasGalleriesPanel;
		protected DataGrid GalleriesDataGrid;
		public void EventHasGalleries_Load(object o, System.EventArgs e)
		{
			if (HasGalleries && !IsFuture)
			{
				GalleriesDataGrid.DataSource = galleries;
				GalleriesDataGrid.DataBind();
			}
		}
		protected bool HasGalleries
		{
			get
			{
				Query q = new Query();
				q.QueryCondition = new And(new Q(Gallery.Columns.OwnerUsrK, Usr.Current.K), new Q(Gallery.Columns.EventK, EventK));
				q.TableElement = Gallery.EventVenueJoin;
				q.OrderBy = new OrderBy(Event.Columns.DateTime, OrderBy.OrderDirection.Descending);
				galleries = new GallerySet(q);
				return (galleries.Count > 0);
			}
		}
		GallerySet galleries;
		#endregion

		#region FutureEventPanel
		protected Panel FutureEventPanel;
		public void FutureEvent_Load(object o, System.EventArgs e)
		{

		}

		bool IsFuture
		{
			get
			{
				return (CurrentEvent.DateTime > DateTime.Today);
			}
		}
		#endregion

		#region CurrentEvent
		public Event CurrentEvent
		{
			get
			{
				if (currentEvent == null && EventK > 0)
					currentEvent = new Event(EventK);
				return currentEvent;
			}
			set
			{
				currentEvent = value;
			}
		}
		private Event currentEvent;
		int EventK
		{
			get
			{
				return ContainerPage.Url["EventK"];
			}
		}
		#endregion

		#region CurrentArticle
		public Article CurrentArticle
		{
			get
			{
				if (currentArticle == null && ArticleK > 0)
					currentArticle = new Article(ArticleK);
				return currentArticle;
			}
			set
			{
				currentArticle = value;
			}
		}
		private Article currentArticle;
		int ArticleK
		{
			get
			{
				return ContainerPage.Url["ArticleK"];
			}
		}
		#endregion

		protected IBob CurrentParent
		{
			get
			{
				if (CurrentEvent != null)
					return CurrentEvent;
				else if (CurrentArticle != null)
					return CurrentArticle;
				else
					return Usr.Current;
			}
		}

		void ChangePanel(Panel p)
		{
			FutureEventPanel.Visible = p.Equals(FutureEventPanel);
			CantAddGallery.Visible = p.Equals(CantAddGallery);
			EventHasGalleriesPanel.Visible = p.Equals(EventHasGalleriesPanel);
			PanelNoPhoto.Visible = p.Equals(PanelNoPhoto);
			NoEditArticlePanel.Visible = p.Equals(NoEditArticlePanel);
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.EventHasGalleries_Load);
			this.Load += new System.EventHandler(this.FutureEvent_Load);
		}
		#endregion
	}
}
