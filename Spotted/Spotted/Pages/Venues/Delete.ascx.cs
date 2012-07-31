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
	public partial class Delete : DsiUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn();
			if (!Usr.Current.IsSuper)
				throw new Exception("Only event moderators may delete venues!");

			if (!Page.IsPostBack)
			{
				ChangePanel(PanelDelete);
				VenueDescriptionP.InnerHtml = ContainerPage.Url.ObjectFilterVenue.FriendlyHtml();
			}
			ErrorBackAnchor.HRef = ContainerPage.Url.ObjectFilterVenue.UrlApp("delete");

		}
		public void DeleteNow(object o, EventArgs e)
		{
			if (Usr.Current.CheckPassword(Password.Text))
			{
				Bobs.Venue.DeleteReturnStatus d = ContainerPage.Url.ObjectFilterVenue.DeleteAllUsr(Usr.Current);
				if (d.Equals(Bobs.Venue.DeleteReturnStatus.Success))
					ChangePanel(PanelDone);
				else
				{
					ChangePanel(PanelError);
					if (d.Equals(Bobs.Venue.DeleteReturnStatus.FailComments))
						DeleteFailedP.InnerText = "This venue has too many comments to delete. Venues with over 10 comments must be manually deleted by an admin. If this venue is a duplicate, consider merging it.";
					else if (d.Equals(Bobs.Venue.DeleteReturnStatus.FailEvents))
						DeleteFailedP.InnerText = "This venue has too many events to delete. Venues with over 3 events must be manually deleted by an admin. If this venue is a duplicate, consider merging it.";
					else if (d.Equals(Bobs.Venue.DeleteReturnStatus.FailException))
						DeleteFailedP.InnerText = "General exception while deleting venue... Contact an admin to get this Venue deleted.";
					else if (d.Equals(Bobs.Venue.DeleteReturnStatus.FailNoPermission))
						DeleteFailedP.InnerText = "You don't have permission to delete this venue. Maybe you are not logged in as an event moderator?";
					else if (d.Equals(Bobs.Venue.DeleteReturnStatus.FailPhotos))
						DeleteFailedP.InnerText = "This venue has too many photos to delete. Venues with more than 5 photos must be deleted manually by an admin. If this venue is a duplicate, consider merging it.";
					else if (d.Equals(Bobs.Venue.DeleteReturnStatus.FailPromoter))
						DeleteFailedP.InnerText = "This venue has promoter activity - e.g. banners, competitions, guestlists, ticket runs etc. To delete it, contact an admin. If this venue is a duplicate, consider merging it.";
				}
			}
			else
			{
				ChangePanel(PanelError);
				DeleteFailedP.InnerText = "Wrong password.";
			}


		}
		#region ChangePanel
		void ChangePanel(Panel p)
		{
			PanelDelete.Visible = p.Equals(PanelDelete);
			PanelError.Visible = p.Equals(PanelError);
			PanelDone.Visible = p.Equals(PanelDone);
		}
		#endregion
	}
}
