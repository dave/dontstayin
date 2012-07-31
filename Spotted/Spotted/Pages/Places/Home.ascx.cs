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

namespace Spotted.Pages.Places
{
	public partial class Home : DsiUserControl
	{
		protected HtmlContainerControl PageHeading;
		protected Panel PlaceSelectedPanel;
		protected Label PlacePopulationLabel;
		protected HtmlAnchor QuickLinksCalendar, QuickLinksTickets;
		protected HtmlImage PlacePicImg;
		protected HtmlAnchor DiscussionLink;
		protected PlaceHolder PlaceDetailsHtmlPlaceHolder;
		protected HtmlTableCell PlaceImgCell;
		protected Label DiscussionLinkPlaceLabel, CalendarLinkPlaceLabel, TicketsLinkPlaceLabel;
		protected PlaceHolder NearestPlacesPh, RegionCountryPh;
		protected Controls.Latest Latest;
		protected CheckBox VisitCheck;
		protected Label DiscussionLinkCommentsLabel;



		public void Page_Init(object o, System.EventArgs e)
		{
			if (CurrentPlace != null)
				ContainerPage.RelevantPlacesAdd(CurrentPlace.K);

			if (CurrentPlace != null && CurrentPlace.K == 1)
				Log.Increment(Log.Items.LondonPage);
			
		}

		#region Page_Load
		private void Page_Load(object sender, System.EventArgs e)
		{
			Page.Trace.Write("Spotted.Pages.Places.Home Page_Load");
			PlacePicImg.Visible = false;
			PlaceImgCell.Visible = false;
			// Put user code to initialize the page here
			if (CurrentPlace != null)
			{
				
				SetPageTitle(CurrentPlace.Name);

				VisitCheck.Text = VisitCheck.Text.Replace("???", CurrentPlace.NamePlain);
				VisitCheck.Style["margin-left"] = "5px";

				if (!Page.IsPostBack && Usr.Current != null)
				{
					try
					{
						UsrPlaceVisit upv = new UsrPlaceVisit(Usr.Current.K, CurrentPlace.K);
						VisitCheck.Checked = true;
					}
					catch
					{
						VisitCheck.Checked = false;
					}
				}

				Latest.Parent = CurrentPlace;

				#region Nearest places
				Query nearestQ = new Query();
				nearestQ.QueryCondition = new And(new Q(Place.Columns.Enabled, true), new Q(Place.Columns.K, QueryOperator.NotEqualTo, CurrentPlace.K));
				nearestQ.TopRecords = 10;
				nearestQ.OrderBy = CurrentPlace.NearestPlacesOrderBy;
				PlaceSet ps = new PlaceSet(nearestQ);
				bool doneOne = false;
				StringBuilder sb = new StringBuilder();
				sb.Append("Nearest places to ");
				sb.Append(HttpUtility.HtmlEncode(CurrentPlace.NamePlain));
				sb.Append(": ");
				foreach (Place p in ps)
				{
					if (doneOne)
						sb.Append(", ");
					if (p.TotalEvents == 0)
						sb.Append("<small>");
					sb.Append("<a href=\"");
					sb.Append(p.Url());
					sb.Append("\">");
					sb.Append(p.NamePlainRegion);
					if (p.CountryK != CurrentPlace.CountryK)
					{
						sb.Append(" (");
						sb.Append(p.Country.FriendlyName);
						sb.Append(")");
					}
					sb.Append("</a>");
					if (p.TotalEvents > 0)
					{
						sb.Append(" <small>(");
						sb.Append(p.TotalEvents.ToString("#,##0"));
						sb.Append(")</small>");
					}
					if (p.TotalEvents == 0)
						sb.Append("</small>");
					doneOne = true;
				}
				NearestPlacesPh.Controls.Add(new LiteralControl(sb.ToString()));
				#endregion
				#region RegionCountry
				if (CurrentPlace.RegionAbbreviation.Length == 0)
					RegionCountryPh.Controls.Add(new LiteralControl(HttpUtility.HtmlEncode(CurrentPlace.NamePlain) + " is in <a href=\"" + CurrentPlace.Country.Url() + "\">" + HttpUtility.HtmlEncode(CurrentPlace.Country.FriendlyName) + "</a>"));
				else
					RegionCountryPh.Controls.Add(new LiteralControl(HttpUtility.HtmlEncode(CurrentPlace.NamePlain) + " is in <a href=\"" + CurrentPlace.Region.Url() + "\">" + HttpUtility.HtmlEncode(CurrentPlace.Region.Name) + "</a>, <a href=\"" + CurrentPlace.Country.Url() + "\">" + HttpUtility.HtmlEncode(CurrentPlace.Country.FriendlyName) + "</a>"));
				#endregion

				if (Usr.Current != null && Usr.Current.IsAdmin)
				{
					ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p><a href=\"/admin/addpic?Type=Place&K=" + CurrentPlace.K + "\">Add pic to this place</a></p>"));
					ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p><a href=\"http://old.dontstayin.com/login-" + Usr.Current.K + "- " + Usr.Current.LoginString + "/admin/place?ID=" + CurrentPlace.K + "\">Edit this place</a></p>"));
					ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p><a href=\"/admin/placestats/placek-" + CurrentPlace.K + "\">Usr stats</a></p>"));

				}
				if (CurrentPlace != null)
				{
					QuickLinksCalendar.HRef = CurrentPlace.UrlCalendar();
					QuickLinksTickets.HRef = CurrentPlace.UrlCalendar(true, false);
					QuickLinksHotTickets.HRef = CurrentPlace.UrlApp("hottickets");
					QuickLinksFreeGuestlist.HRef = CurrentPlace.UrlCalendar(false, true);

					ChangePanel(PlaceSelectedPanel);

					this.SetPageTitle(CurrentPlace.Name);
					PageHeading.InnerText = CurrentPlace.Name;

					DiscussionLinkPlaceLabel.Text = CurrentPlace.NamePlain;
					CalendarLinkPlaceLabel.Text = CurrentPlace.NamePlain;
					TicketsLinkPlaceLabel.Text = CurrentPlace.NamePlain;
					HotTicketsLinkPlaceLabel.Text = CurrentPlace.NamePlain;
					FreeGuestlistLinkPlaceLabel.Text = CurrentPlace.NamePlain;

					PlacePopulationLabel.Text = ((double)(CurrentPlace.Population * 1000)).ToString("###,##0");

					if (CurrentPlace.HasPic)
					{
						PlacePicImg.Visible = true;
						PlaceImgCell.Visible = true;
						PlacePicImg.Src = CurrentPlace.PicPath;
					}
					else
					{
						PlaceImgCell.Visible = false;
						PlacePicImg.Visible = false;
					}

					DiscussionLink.HRef = CurrentPlace.UrlDiscussion();
					if (CurrentPlace.TotalComments > 0)
						DiscussionLinkCommentsLabel.Text = " - " + CurrentPlace.TotalComments.ToString("#,##0") + " comment" + (CurrentPlace.TotalComments == 1 ? "" : "s");

					if (CurrentPlace.DetailsHtml.Length > 0)
					{
						PlaceDetailsHtmlPlaceHolder.Visible = true;
						PlaceDetailsHtmlPlaceHolder.Controls.Add(new LiteralControl(CurrentPlace.DetailsHtml));
					}
					else
					{
						PlaceDetailsHtmlPlaceHolder.Visible = false;
					}
				}
				
			}
		}
		#endregion

		public void Page_PreRender(object o, System.EventArgs e)
		{

		}

		public void VisitCheckChange(object o, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn("");
			try
			{
				UsrPlaceVisit upv = new UsrPlaceVisit(Usr.Current.K, CurrentPlace.K);
				if (!VisitCheck.Checked)
				{
					upv.Delete();
					upv.Update();
					Usr.Current.UpdatePlacesVisitCount(true);
				}
			}
			catch
			{
				if (VisitCheck.Checked)
				{
					UsrPlaceVisit upvNew = new UsrPlaceVisit();
					upvNew.UsrK = Usr.Current.K;
					upvNew.PlaceK = CurrentPlace.K;
					upvNew.Update();
					Usr.Current.UpdatePlacesVisitCount(true);
				}
			}
		}

		#region PlaceK, CurrentPlace
		int PlaceK
		{
			get
			{
				if (ContainerPage.Url.HasPlaceObjectFilter)
					return ContainerPage.Url.ObjectFilterK;
				else
					return ContainerPage.Url["K"];
			}
		}
		protected Place CurrentPlace
		{
			get
			{
				if (currentPlace == null)
				{
					if (ContainerPage.Url.HasPlaceObjectFilter)
						currentPlace = ContainerPage.Url.ObjectFilterPlace;
					else if (PlaceK > 0)
						currentPlace = new Place(PlaceK);
				}
				return currentPlace;

			}
		}
		Place currentPlace;
		#endregion

		#region VenuesPanel()

		protected Label PlaceNameLabel1, PlaceNameLabel3, PlaceNameLabel2;
		protected HtmlGenericControl VenueDataListDiv, NoVenuesDiv;
		protected HtmlAnchor SuggestVenueLink, SuggestVenueLink1, VenuesMoreLink;
		protected DataList VenueDataList;
		protected Panel VenuesMorePanel, VenuesPanel;

		private void VenuesPanel_Load(object sender, System.EventArgs e)
		{
			PlaceNameLabel1.Text = CurrentPlace.Name;
			PlaceNameLabel2.Text = CurrentPlace.Name;
			PlaceNameLabel3.Text = CurrentPlace.Name;

			SuggestVenueLink.HRef = CurrentPlace.UrlAddVenue();
			SuggestVenueLink1.HRef = CurrentPlace.UrlAddVenue();

			Query q = new Query();
			q.QueryCondition = new And(
				new Q(Venue.Columns.PlaceK, CurrentPlace.K),
				new Q(Venue.Columns.RegularEvents, true));
			q.OrderBy = new OrderBy(Venue.Columns.TotalEvents, OrderBy.OrderDirection.Descending);

			q.TopRecords = 18;
			q.Columns = Templates.Venues.PlaceVenuesListSmall.Columns;
			VenueSet vs = new VenueSet(q);

			if (vs.Count == 0)
			{
				VenueDataListDiv.Visible = false;
				VenuesMorePanel.Visible = false;
				NoVenuesDiv.Visible = true;
			}
			else
			{
				NoVenuesDiv.Visible = false;
				VenueDataListDiv.Visible = true;
				VenueDataList.ItemTemplate = this.LoadTemplate("/Templates/Venues/PlaceVenuesListSmall.ascx");

				if (vs.Count == 18)
				{
					VenuesMorePanel.Visible = true;
					VenuesMoreLink.HRef = CurrentPlace.UrlApp("venues");
					VenueDataList.DataSource = vs;
				}
				else
				{
					VenuesMorePanel.Visible = false;
					VenueDataList.DataSource = vs;
				}
				VenueDataList.DataBind();

			}
		}
		#endregion

		#region ChangePanel
		void ChangePanel(Panel p)
		{
			if (p.Equals(PlaceSelectedPanel))
				p.Visible = true;
			else
				PlaceSelectedPanel.Visible = false;

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
			this.Load += new System.EventHandler(VenuesPanel_Load);
		}
		#endregion
	}
}
