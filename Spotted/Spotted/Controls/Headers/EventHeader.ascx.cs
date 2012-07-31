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

namespace Spotted.Controls.Headers
{

	public partial class EventHeader : ContainerUserControl
	{
		public bool SuppressLink = false;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (SuppressLink)
			{
				EventPicAnchor.Visible = false;
			}
			else
			{
				EventPicAnchor.Controls.Add(EventPicImg);
				EventPicAnchor.HRef = CurrentEvent.Url();
			}
			ReviewsPanel.Visible = (CurrentEvent.DateTime <= DateTime.Now);
			AddReviewLink.HRef = CurrentEvent.UrlApp("review");
			DiscussionLink.HRef = CurrentEvent.UrlDiscussion();
			if (CurrentEvent.TotalComments > 0)
				DiscussionLinkCommentsLabel.Text = " - " + CurrentEvent.TotalComments.ToString("#,##0") + " comment" + (CurrentEvent.TotalComments == 1 ? "" : "s");
			EventName.Text = CurrentEvent.Name;
			EventDate.Text = CurrentEvent.FriendlyDate(true);
			uiHotelLinkSpan.Visible = CurrentEvent.ShowHotelLink;
			uiHotelLink.NavigateUrl = CurrentEvent.FindHotelLink(Event.HotelLinkSources.Icon);
			uiHotelLinkText.Text = "Find a hotel near " + CurrentEvent.Venue.Name;
			if (!CurrentEvent.StartTime.Equals(Event.StartTimes.Evening))
			{
				if (CurrentEvent.StartTime.Equals(Event.StartTimes.Daytime))
					EventStartTime.Text = "(daytime)";
				else if (CurrentEvent.StartTime.Equals(Event.StartTimes.Morning))
					EventStartTime.Text = "(morning)";
			}
			#region EventVenue
			EventVenueLink.InnerText = CurrentEvent.Venue.Name;
			EventVenueLink.HRef = CurrentEvent.Venue.Url();
			#endregion
			#region EventPlace
			EventPlaceLink.InnerText = CurrentEvent.Venue.Place.Name;
			EventPlaceLink.HRef = CurrentEvent.Venue.Place.Url();
			#endregion
			#region Pic
			if (CurrentEvent.HasAnyPic)
			{
				EventPicCell.Visible = true;
				EventPicImg.Src = CurrentEvent.AnyPicPath;
			}
			else
				EventPicCell.Visible = false;
			#endregion
			#region Map link
			if (CurrentEvent.Venue.Postcode.Length > 0 || CurrentEvent.Venue.OverrideMapUrl.Length > 0)
			{
				if (CurrentEvent.Venue.OverrideMapUrl.Length > 0)
				{
					MapLink.Attributes["onclick"] = "overrideMapOpen('" + CurrentEvent.Venue.OverrideMapUrl + "');return false;";
					MapLink.HRef = CurrentEvent.Venue.OverrideMapUrl;
				}
				else
				{
					MapLink.Attributes["onclick"] = "mapOpen('http://maps.google.co.uk/maps?q=" + CurrentEvent.Venue.Postcode.Replace(" ", "").ToUpper() + "(" + HttpUtility.UrlEncode(CurrentEvent.Name + " @ " + CurrentEvent.Venue.Name).Replace("'", "\\'").Replace("(", string.Empty).Replace(")", string.Empty) + ")');return false;";
					MapLink.HRef = "http://maps.google.co.uk/maps?q=" + CurrentEvent.Venue.Postcode.Replace(" ", "").ToUpper() + "(" + HttpUtility.UrlEncode(CurrentEvent.Name + " @ " + CurrentEvent.Venue.Name).Replace("(", string.Empty).Replace(")", string.Empty) + ")";
				}

				if (Usr.Current != null && Usr.Current.AddressPostcode.Length > 0)
				{
					DirectionsLink.Attributes["onclick"] = "mapOpen('http://maps.google.co.uk/maps?saddr=" + Usr.Current.AddressPostcode.Replace(" ", "").ToUpper() + "(" + HttpUtility.UrlEncode(Usr.Current.NickName + "'s house").Replace("'", "\\'") + ")&daddr=" + CurrentEvent.Venue.Postcode.Replace(" ", "").ToUpper() + "(" + HttpUtility.UrlEncode(CurrentEvent.Name + " @ " + CurrentEvent.Venue.Name).Replace("'", "\\'").Replace("(", string.Empty).Replace(")", string.Empty) + ")');return false;";
					DirectionsLink.HRef = "http://maps.google.co.uk/maps?saddr=" + Usr.Current.AddressPostcode.Replace(" ", "").ToUpper() + "(" + HttpUtility.UrlEncode(Usr.Current.NickName + "'s house") + ")&daddr=" + CurrentEvent.Venue.Postcode.Replace(" ", "").ToUpper() + "(" + HttpUtility.UrlEncode(CurrentEvent.Name + " @ " + CurrentEvent.Venue.Name).Replace("(", string.Empty).Replace(")", string.Empty) + ")";
				}
				//http://maps.google.co.uk/maps?saddr=FY13PR(HEDKANDI+%40+Syndicate)&daddr=SW66NU(Daves+House)

			}
			else
				MapSpan.Visible = false;
			#endregion
			#region Owner link
			if (CurrentEvent.OwnerUsrK > 0)
			{
				EventSelectedPanelAddedByLink.InnerText = CurrentEvent.Owner.NickName;
				EventSelectedPanelAddedByLink.HRef = CurrentEvent.Owner.Url();
				CurrentEvent.Owner.MakeRollover(EventSelectedPanelAddedByLink);
			}
			else
				EventSelectedPanelAddedByP.Visible = false;
			#endregion

			if (CurrentEvent.Brands.Count > 0)
			{
				//if (CurrentEvent.Brands.Count > 10)
				//	BrandsHolder.Style["font-size"] = "10px";
				if (CurrentEvent.Brands.Count > 5)
					BrandsHolder.Style["font-size"] = "12px";

				string s = "";
				for (int i = 0; i < CurrentEvent.Brands.Count; i++)
				{
					s += (i == 0 ? "" : (i == (CurrentEvent.Brands.Count - 1) ? " and " : ", ")) + "<a href=\"" + CurrentEvent.Brands[i].Url() + "\">" + CurrentEvent.Brands[i].Name + "</a>";
				}
				if (s.Length > 0)
					EventSelectedPanelBrandsPlaceHolder.Controls.Add(new LiteralControl(s));
				else
					BrandsPanel.Visible = false;
			}
			else
				BrandsPanel.Visible = false;

			
		}
		protected void Page_PreRender(object sender, EventArgs e)
		{
			CommentAlertButton.FunctionArgs = CurrentEvent.K.ToString() + ",2";
			CommentAlertButton.InitialState = Usr.Current != null && CommentAlert.IsEnabled(Usr.Current.K, CurrentEvent.K, Model.Entities.ObjectType.Event);
		}

		protected string CommentAlertButtonState
		{
			get
			{
				return (Usr.Current != null && CommentAlert.IsEnabled(Usr.Current.K, CurrentEvent.K, Model.Entities.ObjectType.Event)) ? "1" : "0";
			}
		}

		public Bobs.Event CurrentEvent
		{
			get
			{
				return ((Master.DsiPage)Page).Url.ObjectFilterEvent;
			}
		}

	}

	[ParseChildren(false)]
	public class ContainerUserControl : System.Web.UI.UserControl
	{
		public ContainerUserControl()
			: base()
		{
			this.PreRender += new EventHandler(ContainerUserControl_PreRender);
		}

		void ContainerUserControl_PreRender(object sender, EventArgs e)
		{
			foreach (Control c in Children)
				Content.Controls.Add(c);
		}

		public PlaceHolder Content;
		List<Control> Children = new List<Control>();
		bool PastEnd = false;

		protected override void AddParsedSubObject(object obj)
		{
			Control c = obj as Control;
			if (c != null)
			{
				if (PastEnd)
					Children.Add(c);
				else if (c is PlaceHolder && c.ID.Equals("ControlEnd"))
					PastEnd = true;
				else
					this.Controls.Add(c);

			}
		}

	}
}
