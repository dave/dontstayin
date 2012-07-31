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

namespace Spotted.Pages.Venues
{
	public partial class Home : DsiUserControl
	{
		protected Label VenueHeader, VenueHeader1;
		protected Panel VenueSelectedPanel;
		protected HtmlImage VenuePicImg;
		protected HtmlTableCell VenuePicCell;

		protected HtmlAnchor DiscussionLink;

		protected HtmlGenericControl MapSpan;
		protected HtmlAnchor MapLink, DirectionsLink;

		protected Label DiscussionLinkVenueLabel, CalendarLinkVenueLabel;

		public void Page_Init(object o, System.EventArgs e)
		{
			if (CurrentVenue != null)
				CurrentVenue.AddRelevant(ContainerPage);
		}
		private void Page_Load(object sender, System.EventArgs e)
		{
			VenuePicCell.Visible = false;



			if (Usr.Current != null && Usr.Current.IsAdmin)
			{
				ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p><b>VenueK: "+CurrentVenue.K+"</b></p>"));

				if (CurrentVenue.PromoterK > 0)
				{
					ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p><b>Promoter: <a href=\"" + CurrentVenue.Promoter.Url() + "\">" + CurrentVenue.Promoter.Name + "</a></b>"));

					if (CurrentVenue.PromoterStatus.Equals(Venue.PromoterStatusEnum.Unconfirmed))
						ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<br><font color=0000ff><b>Status unconfirmed</b></font>"));
					
					ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("</p>"));
				}
				else
					ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p>Promoter: n/a</p>"));

				ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p><a href=\"/admin/addpic?Type=Venue&K=" + CurrentVenue.K + "\">Add pic to this venue</a></p>"));
				ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p><a href=\"http://old.dontstayin.com/login-" + Usr.Current.K + "- " + Usr.Current.LoginString + "/admin/venue?ID=" + CurrentVenue.K + "\">Edit this venue</a></p>"));
				ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p><a onclick=\"return confirm('This will delete ALL attached objects.\\nARE YOU SURE?');\" href=\"/admin/multidelete?ObjectType=Venue&ObjectK=" + CurrentVenue.K + "\">Delete this venue</a><br>Be careful - deletes all events, photos, threads etc.</p>"));
			}

			if (Usr.Current != null && Usr.Current.IsSuper)
			{
				ContainerPage.Menu.Admin.SuperAdmin.Controls.Add(new LiteralControl("<p><a href=\"" + CurrentVenue.UrlApp("edit") + "\">Edit this venue</a></p>"));
				ContainerPage.Menu.Admin.SuperAdmin.Controls.Add(new LiteralControl("<p><a href=\"" + CurrentVenue.UrlApp("delete") + "\">Delete this venue</a></p>"));
				ContainerPage.Menu.Admin.SuperAdmin.Controls.Add(new LiteralControl("<p><a href=\"" + CurrentVenue.UrlApp("edit","page","pic") + "\">Edit picture for this venue</a></p>"));
			}


			BindVenueData(CurrentVenue.K);


			if (CurrentVenue.HasPic)
			{
				VenuePicCell.Visible = true;
				VenuePicImg.Src = CurrentVenue.PicPath;
			}
			else
				VenuePicCell.Visible = false;

			DiscussionLink.HRef = CurrentVenue.UrlDiscussion();
			DiscussionLinkVenueLabel.Text = CurrentVenue.Name;
			CalendarLinkVenueLabel.Text = CurrentVenue.Name;
			//TicketsLinkVenueLabel.Text = CurrentVenue.Name;
			//HotTicketsLinkVenueLabel.Text = CurrentVenue.Name;
			//FreeGuestlistLinkVenueLabel.Text = CurrentVenue.Name;
			

			if (CurrentVenue.Postcode.Length > 0 || CurrentVenue.OverrideMapUrl.Length > 0)
			{
				if (CurrentVenue.OverrideMapUrl.Length > 0)
				{
					MapLink.Attributes["onclick"] = "overrideMapOpen('" + CurrentVenue.OverrideMapUrl + "');return false;";
					MapLink.HRef = CurrentVenue.OverrideMapUrl;
				}
				else
				{

					MapLink.Attributes["onclick"] = "mapOpen('http://maps.google.co.uk/maps?q=" + CurrentVenue.Postcode.Replace(" ", "").ToUpper() + "(" + HttpUtility.UrlEncode(CurrentVenue.Name).Replace("'", "\\'").Replace("(", string.Empty).Replace(")", string.Empty) + ")');return false;";
					MapLink.HRef = "http://maps.google.co.uk/maps?q=" + CurrentVenue.Postcode.Replace(" ", "").ToUpper() + "(" + HttpUtility.UrlEncode(CurrentVenue.Name) + ")";
				}

				if (Usr.Current != null && Usr.Current.AddressPostcode.Length > 0)
				{
					DirectionsLink.Attributes["onclick"] = "mapOpen('http://maps.google.co.uk/maps?saddr=" + Usr.Current.AddressPostcode.Replace(" ", "").ToUpper() + "(" + HttpUtility.UrlEncode(Usr.Current.NickName + "'s house").Replace("'", "\\'") + ")&daddr=" + CurrentVenue.Postcode.Replace(" ", "").ToUpper() + "(" + HttpUtility.UrlEncode(CurrentVenue.Name).Replace("'", "\\'").Replace("(", string.Empty).Replace(")", string.Empty) + ")');return false;";
					DirectionsLink.HRef = "http://maps.google.co.uk/maps?saddr=" + Usr.Current.AddressPostcode.Replace(" ", "").ToUpper() + "(" + HttpUtility.UrlEncode(Usr.Current.NickName + "'s house") + ")&daddr=" + CurrentVenue.Postcode.Replace(" ", "").ToUpper() + "(" + HttpUtility.UrlEncode(CurrentVenue.Name).Replace("(", string.Empty).Replace(")", string.Empty) + ")";
				}
				//http://maps.google.co.uk/maps?saddr=FY13PR(HEDKANDI+%40+Syndicate)&daddr=SW66NU(Daves+House)

			}
			else
				MapSpan.Visible = false;



		}



		#region InfoPanel
		protected Panel InfoPanel;
		protected Spotted.CustomControls.h1 EventBodyTitle;
		protected PlaceHolder VenueBody;
		protected Label MusicTypeLabel;
		//protected HtmlTableCell LatestTd;
		protected Controls.Latest Latest;
		//protected HtmlTableCell InfoLeftCell;
		protected Label DiscussionLinkCommentsLabel;
		public void Info_Load(object o, System.EventArgs e)
		{
			VenueBody.Controls.Clear();

			HtmlRenderer r = new HtmlRenderer();
			r.Formatting = !CurrentVenue.DetailsPlain;
			r.Container = !CurrentVenue.DetailsPlain;
			r.LoadHtml(CurrentVenue.DetailsHtml);

			if (r.Container)
			{
				InfoPanel.Visible = true;
				VenueBody.Controls.Add(new LiteralControl(r.Render(VenueBody)));
			}
			else
			{
				InfoPanel.Visible = false;
				VenueDetailsPlainPh.Controls.Add(new LiteralControl("<div style=\"width:634px; overflow:hidden;\">" + r.Render(VenueDetailsPlainPh) + "</div>"));
			}
			if (CurrentVenue.TotalComments > 0)
				DiscussionLinkCommentsLabel.Text = " - " + CurrentVenue.TotalComments.ToString("#,##0") + " comment" + (CurrentVenue.TotalComments == 1 ? "" : "s");

			Latest.Parent= CurrentVenue;


		}
		public void Info_PreRender(object o, System.EventArgs e)
		{

		}
		#endregion

		#region BindVenueData
		protected Panel UpcomingAllEventsPanel, PreviousAllEventsPanel;
		protected Label VenueNameLabel;
		protected HtmlAnchor PlaceNameLink, CalendarLink;
		void BindVenueData(int VenueK)
		{
			Venue v = new Venue(VenueK);
			if (v != null)
			{
				CalendarLink.HRef = v.UrlCalendar();
				//TicketsLink.HRef = v.UrlCalendar(true, false);
				//HotTicketsLink.HRef = v.UrlApp("hottickets");
				//FreeGuestlistLink.HRef = v.UrlCalendar(false, true);

				ChangePanel(VenueSelectedPanel);
				this.SetPageTitle(v.Name + " in " + v.Place.Name, v.Name);
				VenueHeader.Text = CurrentVenue.Name;
				VenueNameLabel.Text = CurrentVenue.Name;
				PlaceNameLink.HRef = CurrentVenue.Place.Url();
				PlaceNameLink.InnerText = CurrentVenue.Place.Name;
			}
		}
		#endregion

		#region CurrentVenue
		public Venue CurrentVenue
		{
			get
			{
				return ContainerPage.Url.ObjectFilterVenue;
			}
		}
		#endregion

		void ChangePanel(Panel p)
		{
			if (p.Equals(VenueSelectedPanel))
				p.Visible = true;
			else
				VenueSelectedPanel.Visible = false;

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
			this.Load += new System.EventHandler(this.Info_Load);
			this.PreRender += new System.EventHandler(this.Info_PreRender);
		}
		#endregion
	}
}
