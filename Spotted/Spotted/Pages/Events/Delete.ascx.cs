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

namespace Spotted.Pages.Events
{
	public partial class Delete : DsiUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn();
			if (!Usr.Current.IsSuper)
				throw new Exception("Only event moderators may delete events!");

			if (!Page.IsPostBack)
			{
				ChangePanel(PanelDelete);
				EventDescriptionP.InnerHtml = ContainerPage.Url.ObjectFilterEvent.FriendlyHtml();
			}
			ErrorBackAnchor.HRef = ContainerPage.Url.ObjectFilterEvent.UrlApp("delete");

		}
		public void DeleteNow(object o, EventArgs e)
		{
			if (Usr.Current.CheckPassword(Password.Text))
			{
				Bobs.Event.DeleteReturnStatus d = ContainerPage.Url.ObjectFilterEvent.DeleteAllUsr(Usr.Current);
				if (d.Equals(Bobs.Event.DeleteReturnStatus.Success))
					ChangePanel(PanelDone);
				else
				{
					ChangePanel(PanelError);
					if (d.Equals(Bobs.Event.DeleteReturnStatus.FailComments))
						DeleteFailedP.InnerText = "This event has too many comments to delete. Events with over 10 comments must be manually deleted by an admin. If this event is a duplicate, consider merging it.";
					else if (d.Equals(Bobs.Event.DeleteReturnStatus.FailException))
						DeleteFailedP.InnerText = "General exception while deleting event... Contact an admin to get this event deleted.";
					else if (d.Equals(Bobs.Event.DeleteReturnStatus.FailNoPermission))
						DeleteFailedP.InnerText = "You don't have permission to delete this event. Maybe you are not logged in as an event moderator?";
					else if (d.Equals(Bobs.Event.DeleteReturnStatus.FailPhotos))
						DeleteFailedP.InnerText = "This event has too many photos to delete. Events with more than 5 photos must be deleted manually by an admin. If this event is a duplicate, consider merging it.";
					else if (d.Equals(Bobs.Event.DeleteReturnStatus.FailPromoter))
						DeleteFailedP.InnerText = "This event has promoter activity - e.g. banners, competitions, guestlists, ticket runs etc. To delete it, contact an admin. If this event is a duplicate, consider merging it.";
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
