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
using System.Text;

namespace Spotted.Pages.Galleries
{
	public partial class Paged : DsiUserControl
	{
		public void Page_Init(object o, System.EventArgs e)
		{
			if (CurrentGallery != null)
				CurrentGallery.AddRelevant(ContainerPage);
		}
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (CurrentGallery.ShowOnSite)
			{
				if (CurrentGallery.Event != null)
					SetPageTitle(CurrentGallery.Name + " from " + CurrentGallery.Event.FriendlyName);
				else if (CurrentGallery.Article != null)
					SetPageTitle(CurrentGallery.Name + " from " + CurrentGallery.Article.Title);

				CurrentGallery.SetGalleryUsr(CurrentGallery.LivePhotos);
			}
			else
				SetPageTitle("Gallery");

			if (Usr.Current != null && Usr.Current.IsAdmin)
			{
				ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p><a href=\"" + CurrentGallery.UrlApp("edit") + "\">Edit gallery</a></p>"));
                ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p><a onclick=\"return confirm('This will delete ALL attached objects.\\nARE YOU SURE?');\" href=\"/admin/multidelete?ObjectType=Gallery&ObjectK=" + CurrentGallery.K + "\">Delete this gallery</a><br>Be careful - deletes all photos, comments etc.</p>"));
			}

		}

		#region MiscInfoPanel
		protected Spotted.CustomControls.h1 Header;
		protected HtmlImage GalleryPicImg;
		protected HtmlAnchor EventLink, EventVenueLink, EventPlaceLink, DiscussionLink, OwnerLink, LinkBack, QuickBrowserLink;
		protected Label EventDate;
		protected HtmlTableCell PicCell;
		protected HtmlGenericControl EventLinkP, ArticleLinkP;
		protected HtmlAnchor ArticleLink;
		protected Label DiscussionLinkCommentsLabel, DiscussionLinkTargetLabel;
		public void MiscInfo_Load(object o, System.EventArgs e)
		{
			if (CurrentGallery.Event != null)
			{
				EventLinkP.Visible = true;
				ArticleLinkP.Visible = false;
				if (CurrentGallery.Event.TotalComments > 0)
					DiscussionLinkCommentsLabel.Text = " - " + CurrentGallery.Event.TotalComments.ToString("#,##0") + " comment" + (CurrentGallery.Event.TotalComments == 1 ? "" : "s");

				EventLink.InnerText = CurrentGallery.Event.Name;
				EventLink.HRef = CurrentGallery.Event.Url();
				EventVenueLink.InnerText = CurrentGallery.Event.Venue.Name;
				EventVenueLink.HRef = CurrentGallery.Event.Venue.Url();
				LinkBack.HRef = CurrentGallery.Event.Url();
				LinkBack.InnerText = "Back to the " + CurrentGallery.Event.Name + " main page";
				EventPlaceLink.InnerText = CurrentGallery.Event.Venue.Place.FriendlyName;
				EventPlaceLink.HRef = CurrentGallery.Event.Venue.Place.Url();
				EventDate.Text = CurrentGallery.Event.FriendlyDate(false);

				DiscussionLink.HRef = CurrentGallery.Event.UrlDiscussion();
				DiscussionLinkTargetLabel.Text = "event";
			}
			else if (CurrentGallery.Article != null)
			{
				EventLinkP.Visible = false;
				ArticleLinkP.Visible = true;

				if (CurrentGallery.Article.TotalComments > 0)
					DiscussionLinkCommentsLabel.Text = " - " + CurrentGallery.Article.TotalComments.ToString("#,##0") + " comment" + (CurrentGallery.Article.TotalComments == 1 ? "" : "s");

				ArticleLink.InnerText = CurrentGallery.Article.Name;
				ArticleLink.HRef = CurrentGallery.Article.Url();

				LinkBack.HRef = CurrentGallery.Article.Url();
				LinkBack.InnerText = "Back to the article page";

				DiscussionLink.HRef = CurrentGallery.Article.UrlDiscussion();
				DiscussionLinkTargetLabel.Text = "article";
			}

			QuickBrowserLink.HRef = CurrentGallery.Url();

			OwnerLink.HRef = CurrentGallery.Owner.Url();
			OwnerLink.InnerText = CurrentGallery.Owner.NickName;
			CurrentGallery.Owner.MakeRollover(OwnerLink);

			if (CurrentGallery.ShowOnSite)
			{
				Header.InnerText = CurrentGallery.Name;
				if (CurrentGallery.HasPic)
					GalleryPicImg.Src = CurrentGallery.PicPath;
				else
					PicCell.Visible = false;
			}
			else
			{
				PicCell.Visible = false;
			}
		}
		#endregion

		#region NoPhotosPanel
		protected Panel NoPhotosPanel;
		public void NoPhotos_Load(object o, System.EventArgs e)
		{
			NoPhotosPanel.Visible = !CurrentGallery.ShowOnSite;
		}
		#endregion

		#region PhotosPanel
		protected Panel PhotosPanel;
		protected DataList PhotosDataList;
		protected HtmlGenericControl PhotoPageLinksP, PhotoPageLinksP1;

		public void Photos_Load(object o, System.EventArgs e)
		{
			PhotosPanel.Visible = CurrentGallery.ShowOnSite;
			if (CurrentGallery.ShowOnSite)
			{
				BindPhotos();
			}
		}

		int CurrentPage
		{
			get
			{
				if (currentPage > 0)
					return currentPage;
				else if (ContainerPage.Url["P"].IsNull)
					return 1;
				else
					return ContainerPage.Url["P"];
			}
			set
			{
				currentPage = value;
			}
		}
		int currentPage = 0;

		void BindPhotos()
		{
			Query q = new Query();

			q.Paging.RecordsPerPage = Vars.GalleryPageSize;
			q.Paging.RequestedPage = CurrentPage;

			q.Columns = Templates.Photos.Default.Columns;
			q.TableElement = Templates.Photos.Default.PerformJoins(new TableElement(TablesEnum.Photo));
			q.OrderBy = Photo.DateTimeOrder(OrderBy.OrderDirection.Ascending);
			q.QueryCondition = new And(new Q(Photo.Columns.GalleryK, GalleryK), Photo.EnabledQueryCondition);

			PhotoSet ps = new PhotoSet(q);

			CurrentPage = ps.Paging.ReturnedPage;

			if (ps.Paging.ShowNoLinks)
			{
				PhotoPageLinksP.Visible = false;
				PhotoPageLinksP1.Visible = false;
			}
			else
			{
				int endLinks = 5;
				int midLinks = 4;
				PageLinkWriter p = new PageLinkWriter();
				p.SetLastPage(Vars.GalleryPageSize, CurrentGallery.LivePhotos);
				p.CurrentPageForLinks = CurrentPage;
				p.Zones.Add(new PageLinkWriter.Zone(1, endLinks));
				p.Zones.Add(new PageLinkWriter.Zone(p.LastPage - endLinks + 1, p.LastPage));
				p.Zones.Add(new PageLinkWriter.Zone(CurrentPage - midLinks, CurrentPage + midLinks));
				StringBuilder sb = new StringBuilder();
				sb.Append("Pages: ");
				p.Build(new PageLinkWriter.LinkWriter(PageLinkWriter), new PageLinkWriter.SeperatorWriter(PageSeperatorWriter), sb);
				PhotoPageLinksP.Controls.Clear();
				PhotoPageLinksP1.Controls.Clear();
				PhotoPageLinksP.Controls.Add(new LiteralControl(sb.ToString()));
				PhotoPageLinksP1.Controls.Add(new LiteralControl(sb.ToString()));
			}


			PhotosDataList.DataSource = ps;
			PhotosDataList.ItemTemplate = this.LoadTemplate("/Templates/Photos/Default.ascx");
			PhotosDataList.DataBind();

		}

		string GetPageUrl(int page)
		{
			if (page == 1)
				return CurrentGallery.PagedUrl();
			else
				return CurrentGallery.PagedUrl("p", page.ToString());
		}

		public void PageSeperatorWriter(int PreviousPage, int NextPage, StringBuilder Builder)
		{
			Builder.Append("...&nbsp; ");
		}
		public void PageLinkWriter(int Page, int CurrentPage, StringBuilder Builder)
		{
			if (CurrentPage == Page)
			{
				Builder.Append("<span class=\"CurrentPage\">");
			}
			else
			{
				Builder.Append("<a href=\"");
				Builder.Append(GetPageUrl(Page));
				Builder.Append("\">");
			}
			Builder.Append(Page.ToString());
			if (CurrentPage == Page)
			{
				Builder.Append("</span>");
			}
			else
			{
				Builder.Append("</a>");

			}
			Builder.Append("&nbsp; ");
		}

		#endregion

		#region GalleryK
		int GalleryK
		{
			get
			{
				if (ContainerPage.Url.HasGalleryObjectFilter)
					return ContainerPage.Url.ObjectFilterK;
				else
					return ContainerPage.Url["K"];
			}
		}
		#endregion
		#region CurrentGallery
		public Gallery CurrentGallery
		{
			get
			{
				if (currentGallery == null)
				{
					if (ContainerPage.Url.HasGalleryObjectFilter)
						currentGallery = ContainerPage.Url.ObjectFilterGallery;
					else if (GalleryK > 0)
						currentGallery = new Gallery(GalleryK);
				}
				return currentGallery;
			}
		}
		Gallery currentGallery;
		#endregion

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
			this.Load += new System.EventHandler(Photos_Load);
			this.Load += new System.EventHandler(MiscInfo_Load);
			this.Load += new System.EventHandler(NoPhotos_Load);
		}
		#endregion
	}
}
