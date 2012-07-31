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

namespace Spotted.Controls
{
	public partial class AttendedEventControl : System.Web.UI.UserControl
	{
		#region ThisEvent
		public Event ThisEvent
		{
			get
			{
				if (thisEvent == null && ViewState["EventK"] != null)
				{
					thisEvent = new Event((int)ViewState["EventK"]);
				}
				return thisEvent;
			}
			set
			{
				thisEvent = value;
				ViewState["EventK"] = value.K;
			}
		}
		private Event thisEvent;
		#endregion

		public void GalleryUpdate_Change(object o, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn("You have to be logged in to do this!");
			try
			{
				UsrEventAttended u1 = new UsrEventAttended(Usr.Current.K, ThisEvent.K);
				u1.SendUpdate = GalleryUpdateCheckBox.Checked;
				u1.Update();
			}
			catch { }
		}

		public override void DataBind()
		{
			bool found = false;
			bool sendUpdates = false;
			if (Usr.Current != null)
			{
				try
				{
					UsrEventAttended u1 = new UsrEventAttended(Usr.Current.K, ThisEvent.K);
					sendUpdates = u1.SendUpdate;
					found = true;
				}
				catch { }
			}
			UsrEventAttendYes.Checked = found;// && Usr.Current != null;
			UsrEventAttendNo.Checked = !found;// && Usr.Current != null;
			GalleryUpdateP.Visible = found;
			GalleryUpdateCheckBox.Checked = sendUpdates;

			UsrEventAttendFutureLabel.Visible = ThisEvent.IsFuture;
			UsrEventAttendPastLabel.Visible = !ThisEvent.IsFuture;
			UsrEventAttendYes.Text = ThisEvent.IsFuture ? "I'll be there" : "I was there";
			UsrEventAttendNo.Text = ThisEvent.IsFuture ? "Not this one" : "Nope, missed it";

			UsrEventAttendYes.Attributes["onclick"] = "try { return WhenLoggedInRadio(this); } catch(ex) { return false; }";
			UsrEventAttendNo.Attributes["onclick"] = "try { return WhenLoggedInRadio(this); } catch(ex) { return false; }";
		}

		public event EventHandler UsrAttendingEvent;

		#region UsrEventAttendClick
		public void UsrEventAttendClick(object o, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn("You'll have to be logged in to do this...");

			Usr.Current.AttendEvent(ThisEvent.K, UsrEventAttendYes.Checked, null, null);

			if (UsrEventAttendYes.Checked)
				CommentAlert.Enable(Usr.Current, ThisEvent.K, Model.Entities.ObjectType.Event);
			else
				CommentAlert.Disable(Usr.Current, ThisEvent.K, Model.Entities.ObjectType.Event);

			if (UsrEventAttendYes.Checked && UsrAttendingEvent != null)
				UsrAttendingEvent(null, EventArgs.Empty);

			this.DataBind();
		}
		#endregion


	}
}
