using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Bobs;

namespace Spotted.Pages.Promoters
{
	public partial class EventOptions : PromoterUserControl
	{
		#region Page_Init
		protected override void Page_Init(object sender, System.EventArgs e)
		{
			base.Page_Init(sender, e);
		}
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Mode.Equals(Modes.None) || Mode.Equals(Modes.Event))
					ChangePanel(PanelEvent);
				LoadEventTicketRuns();
			}
			AdminLinksPanel.Visible = Usr.Current.IsAdmin;
			if (!EnsureSecure)
                throw new DsiUserFriendlyException("You can't view this page!");


			SpotterRequestYesPanel.Visible = CurrentEvent.SpotterRequest.HasValue && CurrentEvent.SpotterRequest.Value;
			SpotterRequestNoPanel.Visible = !CurrentEvent.SpotterRequest.HasValue || !CurrentEvent.SpotterRequest.Value;
			if (CurrentEvent.SpotterRequest.HasValue && CurrentEvent.SpotterRequest.Value)
			{
				SpotterRequestDetails.Text = CurrentEvent.SpotterRequestName + " on " + CurrentEvent.SpotterRequestNumber;
				SpotterRequestYesLink.HRef = CurrentEvent.UrlApp("edit", "page", "details", "promoterk", CurrentPromoter.K.ToString()) + "#SpotterRequest";
			}
			else
			{
				SpotterRequestNoLink.HRef = CurrentEvent.UrlApp("edit", "page", "details", "promoterk", CurrentPromoter.K.ToString()) + "#SpotterRequest";
			}

		}

		#region PanelEvent
		protected Panel PanelEvent;
		protected Spotted.CustomControls.PromoterIntro PromoterIntro;
		protected HtmlGenericControl EventLinksP;
		protected HtmlTableCell EventDetailsPicCell;
		private void PanelEvent_Load(object sender, System.EventArgs e)
		{
			if (EnsureSecure)
			{
				if (CurrentEvent.HasPic)
					EventDetailsPicCell.InnerHtml = "<img src=\"" + CurrentEvent.PicPath + "\" class=\"BorderBlack All\" width=\"50\" height=\"50\">";
				else
					EventDetailsPicCell.InnerHtml = "<small>[none]</small>";
				EventLinksP.InnerHtml = CurrentEvent.FriendlyHtml(true, true, true, false);
			}
		}
		#endregion

		#region TicketRunPanel
		#region TicketRunPanel_Load
		public void TicketRunPanel_Load(object o, System.EventArgs e)
		{
			LoadEventTicketRuns();
		}
		#endregion
		#region LoadEventTicketRuns
		private void LoadEventTicketRuns()
		{
			if (EnsureSecure)
			{
				SellTicketsP.Visible = CurrentEvent.LatestEndOfTicketRunDateTime > DateTime.Now;
				NoTicketRunsP.Visible = SellTicketsP.Visible && CurrentEvent.TicketRuns.Count == 0;
				NoSellTicketsP.Visible = !SellTicketsP.Visible;
				H10.InnerHtml = (NoTicketRunsP.Visible ? "No Tickets!" : "Tickets");

				SellTicketsLink.HRef = CurrentPromoter.UrlApp("ticketrun", "eventk", CurrentEventK.ToString(), "ReferringPage", Convert.ToInt32(TicketRun.ReferringPageType.EventOptions).ToString());
				DoorlistLink.HRef = UrlInfo.PageUrl(UrlInfo.PageTypes.Blank, "doorlist", "eventk", CurrentEventK.ToString());

				Query eventTicketRunsQuery = new Query(new And(new Q(Bobs.TicketRun.Columns.EventK, CurrentEvent.K),
															   new Q(Bobs.TicketRun.Columns.PromoterK, CurrentPromoter.K)));
				eventTicketRunsQuery.OrderBy = new OrderBy(new OrderBy(Bobs.TicketRun.Columns.ListOrder),
														   new OrderBy(Bobs.TicketRun.Columns.StartDateTime),
														   new OrderBy(Bobs.TicketRun.Columns.Price));

				TicketRunSet eventTicketRuns = new TicketRunSet(eventTicketRunsQuery);

				TicketRunsGridView.DataSource = eventTicketRuns;
				TicketRunsGridView.DataBind();

				DoorlistP.Visible = false;
				foreach (Bobs.TicketRun ticketRun in eventTicketRuns)
				{
					if (ticketRun.SoldTickets > 0)
					{
						DoorlistP.Visible = true;
						break;
					}
				}
			}
		}
		#endregion

		#region TicketRunsGridView Event Handlers
		protected void TicketRunsGridView_RowCommand(object sender, GridViewCommandEventArgs e)
		{
			if (e.CommandName == "StopTicketRun" || e.CommandName == "PauseResumeTicketRun")
			{
				GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;

				Bobs.TicketRun ticketRun = new Bobs.TicketRun(Convert.ToInt32(((TextBox)row.FindControl("TicketRunKTextBox")).Text));
				if (e.CommandName == "StopTicketRun")
				{
					ticketRun.EndTicketRun();
				}
				else if (e.CommandName == "PauseResumeTicketRun")
				{
					ticketRun.Paused = !ticketRun.Paused;
					ticketRun.Update();
				}
				else
					return;

				LoadEventTicketRuns();
			}
		}
		#endregion
		#endregion

		#region BannersPanel
		protected Panel BannersPanel, NoBannersPanel;
		protected HtmlAnchor BannerAddLink, BannerAddLink1;
		protected DataGrid BannerDataGrid;

		public void BannersPanel_Load(object o, System.EventArgs e)
		{
			if (EnsureSecure)
			{
				BannerAddLink.HRef = CurrentPromoter.UrlApp("banneredit", "mode", "add", "eventk", CurrentEvent.K.ToString());
				BannerAddLink1.HRef = CurrentPromoter.UrlApp("banneredit", "mode", "add", "eventk", CurrentEvent.K.ToString());
				BannerListBind();
			}
		}
		void BannerListBind()
		{
			if (EnsureSecure)
			{
				Query q = new Query();
				q.QueryCondition = new And(new Q(Banner.Columns.PromoterK, CurrentPromoter.K), new Q(Banner.Columns.EventK, CurrentEvent.K));
				q.OrderBy = new OrderBy(Banner.Columns.LastDay, OrderBy.OrderDirection.Descending);
				BannerSet bs = new BannerSet(q);
				BannersPanel.Visible = bs.Count > 0;
				NoBannersPanel.Visible = bs.Count == 0;
				if (bs.Count > 0)
				{
					BannerDataGrid.AllowPaging = bs.Count > BannerDataGrid.PageSize;
					BannerDataGrid.DataSource = bs;
					BannerDataGrid.DataBind();
				}
			}
		}
		public void BannerDataGridChangePage(object o, DataGridPageChangedEventArgs e)
		{
			if (EnsureSecure)
			{
				BannerDataGrid.CurrentPageIndex = e.NewPageIndex;
				BannerListBind();
			}
		}

		#endregion

		#region EventHighlightPanel
		#region EventHighlightPanel_Load
		public void EventHighlightPanel_Load(object o, System.EventArgs e)
		{
			if (EnsureSecure)
			{
				EventHighlightPanel.Visible = CurrentEvent.HasHilight;
				NoEventHighlightPanel.Visible = !CurrentEvent.HasHilight;

				if (CurrentEvent.Donated)
				{
			//		List<Event> events = new List<Event>();
			//		events.Add(CurrentEvent);
			//		HighlightedEventDataList.ItemTemplate = this.LoadTemplate("/Templates/Events/UsrPageAttendedList.ascx");
			//		HighlightedEventDataList.DataSource = events;
			//		HighlightedEventDataList.DataBind();
				}
				else
				{
					EventHighlightLink.HRef = CurrentPromoter.UrlApp("eventhighlight", "eventk", CurrentEvent.K.ToString());
				}
			}
		}
		#endregion
		#endregion

		#region NewsPanel
		protected Panel NewsPanel, NoNewsPanel;
		protected DataGrid NewsDataGrid;

		protected Controls.AddThread NewsAddThread;
		protected HtmlGenericControl
			NewsAddThreadLinkP;
		protected HtmlInputHidden
			NewsAddThreadStatusHidden;
		protected Panel
			NewsAddThreadPanel,
			NewsPostPanel,
			NoNewsPostPanel;


		public void NewsPanel_Load(object o, System.EventArgs e)
		{
			if (EnsureSecure)
			{

				NewsListBind();

				Query q = new Query();
				q.TableElement = new Join(
					new TableElement(TablesEnum.Group),
					new TableElement(TablesEnum.Brand),
					QueryJoinType.Inner,
					new And(
						new Q(Group.Columns.BrandK, Brand.Columns.K, true),
						new Q(Brand.Columns.PromoterK, ContainerPage.Url.ObjectFilterPromoter.K),
						new Q(Brand.Columns.PromoterStatus, Brand.PromoterStatusEnum.Confirmed)
					)
				);
				q.ReturnCountOnly = true;
				GroupSet gs = new GroupSet(q);

				if (gs.Count > 0)
				{
					NewsPostPanel.Visible = true;
					NoNewsPostPanel.Visible = false;

					if (!Page.IsPostBack)
					{
						NewsAddThread.AddThreadAdvancedCheckBox.Checked = true;
						NewsAddThread.AddThreadNewsCheckBox.Checked = true;
						NewsAddThread.AddThreadGroupRadioButton.Checked = true;
					}

					NewsAddThread.ForceParentType = Model.Entities.ObjectType.Event;
					NewsAddThread.ForceParentK = CurrentEvent.K;

					if (NewsAddThreadStatusHidden.Value.Equals("1"))
					{
						NewsAddThreadPanel.Style["display"] = "";
						NewsAddThreadLinkP.Style["display"] = "none";
					}
					else
					{
						NewsAddThreadPanel.Style["display"] = "none";
						NewsAddThreadLinkP.Style["display"] = "";
					}

				}
				else
				{
					NewsPostPanel.Visible = false;
					NoNewsPostPanel.Visible = true;
				}


			}
		}
		void NewsListBind()
		{
			if (EnsureSecure)
			{
				Query q = new Query();
				q.QueryCondition = new And(new Q(Thread.Columns.EventK, CurrentEvent.K), new Q(Thread.Columns.IsNews, true), new Q(Brand.Columns.PromoterK, CurrentPromoter.K));
				q.TableElement = new Join(
					new TableElement(TablesEnum.Thread),
					new Join(Group.Columns.BrandK, Brand.Columns.K),
					QueryJoinType.Inner,
					new Q(Thread.Columns.GroupK, Group.Columns.K, true)
				);
				q.OrderBy = new OrderBy(Thread.Columns.DateTime, OrderBy.OrderDirection.Descending);
				ThreadSet ts = new ThreadSet(q);
				NewsPanel.Visible = ts.Count > 0;
				NoNewsPanel.Visible = ts.Count == 0;
				if (ts.Count > 0)
				{
					NewsDataGrid.AllowPaging = ts.Count > NewsDataGrid.PageSize;
					NewsDataGrid.DataSource = ts;
					NewsDataGrid.DataBind();
				}
			}
		}
		public void NewsDataGridChangePage(object o, DataGridPageChangedEventArgs e)
		{
			if (EnsureSecure)
			{
				NewsDataGrid.CurrentPageIndex = e.NewPageIndex;
				NewsListBind();
			}
		}
		#endregion

		#region ArticlePanel
		protected Panel ArticlePanel, NoArticlePanel;
		protected HtmlAnchor ArticleAddLink, ArticleAddLink1;
		protected DataGrid ArticleDataGrid;
		public void ArticlePanel_Load(object o, System.EventArgs e)
		{
			if (EnsureSecure)
			{
				ArticleAddLink.HRef = UrlInfo.PageUrl("myarticles", "mode", "add", "eventk", CurrentEvent.K);
				ArticleAddLink1.HRef = UrlInfo.PageUrl("myarticles", "mode", "add", "eventk", CurrentEvent.K);
				ArticleListBind();
			}
		}
		void ArticleListBind()
		{
			if (EnsureSecure)
			{
				Query q = new Query();
				q.QueryCondition = new Q(Article.Columns.EventK, CurrentEvent.K);
				q.OrderBy = new OrderBy(Article.Columns.AddedDateTime, OrderBy.OrderDirection.Descending);
				ArticleSet ars = new ArticleSet(q);
				ArticlePanel.Visible = ars.Count > 0;
				NoArticlePanel.Visible = ars.Count == 0;
				if (ars.Count > 0)
				{
					ArticleDataGrid.AllowPaging = ars.Count > ArticleDataGrid.PageSize;
					ArticleDataGrid.DataSource = ars;
					ArticleDataGrid.DataBind();
				}
			}
		}
		public void ArticleDataGridChangePage(object o, DataGridPageChangedEventArgs e)
		{
			if (EnsureSecure)
			{
				ArticleDataGrid.CurrentPageIndex = e.NewPageIndex;
				ArticleListBind();
			}
		}
		#endregion

		#region CompPanel
		protected Panel CompPanel, NoCompPanel;
		protected HtmlAnchor CompAddLink, CompAddLink1;
		protected DataGrid CompDataGrid;
		public void CompPanel_Load(object o, System.EventArgs e)
		{
			if (EnsureSecure)
			{
				CompAddLink.HRef = CurrentPromoter.UrlApp("competitions", "mode", "add", "eventk", CurrentEvent.K.ToString());
				CompAddLink1.HRef = CurrentPromoter.UrlApp("competitions", "mode", "add", "eventk", CurrentEvent.K.ToString());
				CompListBind();
			}
		}
		void CompListBind()
		{
			if (EnsureSecure)
			{
				Query q = new Query();
				q.QueryCondition = new Q(Comp.Columns.EventK, CurrentEvent.K);
				q.OrderBy = new OrderBy(Comp.Columns.DateTimeAdded, OrderBy.OrderDirection.Descending);
				CompSet cs = new CompSet(q);
				CompPanel.Visible = cs.Count > 0;
				NoCompPanel.Visible = cs.Count == 0;
				if (cs.Count > 0)
				{
					CompDataGrid.AllowPaging = cs.Count > CompDataGrid.PageSize;
					CompDataGrid.DataSource = cs;
					CompDataGrid.DataBind();
				}
			}
		}
		public void CompDataGridChangePage(object o, DataGridPageChangedEventArgs e)
		{
			if (EnsureSecure)
			{
				CompDataGrid.CurrentPageIndex = e.NewPageIndex;
				CompListBind();
			}
		}
		#endregion

		#region GuestlistPanel
		protected Panel GuestlistPanel, NoGuestlistPanel;
		protected HtmlAnchor GuestlistAddLink;
		protected DataGrid GuestlistDataGrid;
		public void GuestlistPanel_Load(object o, System.EventArgs e)
		{
			if (EnsureSecure)
			{
				GuestlistAddLink.HRef = CurrentPromoter.UrlApp("guestlists", "mode", "add", "eventk", CurrentEvent.K.ToString());
				GuestlistBind();
			}
		}
		void GuestlistBind()
		{
			if (EnsureSecure)
			{
				Query q = new Query();
				q.QueryCondition = new And(new Q(Event.Columns.HasGuestlist, true), new Q(Event.Columns.K, CurrentEvent.K), new Q(Event.Columns.GuestlistPromoterK, CurrentPromoter.K));
				EventSet es = new EventSet(q);
				GuestlistPanel.Visible = es.Count > 0;
				NoGuestlistPanel.Visible = false;
				if (es.Count > 0)
				{
					GuestlistDataGrid.AllowPaging = es.Count > CompDataGrid.PageSize;
					GuestlistDataGrid.DataSource = es;
					GuestlistDataGrid.DataBind();
				}
			}
		}
		#endregion

		#region EnsureSecure
		public bool EnsureSecure
		{
			get
			{
				if (!doneEnsureSecure)
				{
					ensureSecure = Usr.Current.IsAdmin || (Usr.Current.IsEnabledPromoter(CurrentPromoter.K) && CurrentEvent.IsConfirmedPromoter(CurrentPromoter.K));
					doneEnsureSecure = true;
				}
				return ensureSecure;
			}
			set
			{
				ensureSecure = value;
			}
		}
		private bool doneEnsureSecure = false;
		private bool ensureSecure;
		#endregion
		#region CurrentEvent
		public Event CurrentEvent
		{
			get
			{
				if (currentEvent == null && CurrentEventK > 0)
				{
					currentEvent = new Event(CurrentEventK);
				}
				return currentEvent;
			}
			set
			{
				currentEvent = value;
			}
		}
		private Event currentEvent;
		int CurrentEventK
		{
			get
			{
				return ContainerPage.Url["EventK"];
			}
		}
		#endregion
		#region PageMode
		Modes Mode
		{
			get
			{
				if (ContainerPage.Url[0].Equals("Event"))
					return Modes.Event;
				else
					return Modes.None;
			}
		}
		public enum Modes
		{
			None,
			Event
		}
		#endregion
		#region ChangePanel
		void ChangePanel(Panel p)
		{
			PanelEvent.Visible = p.Equals(PanelEvent);
		}
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
			this.Load += new System.EventHandler(this.PanelEvent_Load);
			this.Load += new System.EventHandler(this.TicketRunPanel_Load);
			this.Load += new System.EventHandler(this.EventHighlightPanel_Load);
			this.Load += new System.EventHandler(this.NewsPanel_Load);
			this.Load += new System.EventHandler(this.ArticlePanel_Load);
			this.Load += new System.EventHandler(this.BannersPanel_Load);
			this.Load += new System.EventHandler(this.CompPanel_Load);
			this.Load += new System.EventHandler(this.GuestlistPanel_Load);
		}
		#endregion
	}
}
