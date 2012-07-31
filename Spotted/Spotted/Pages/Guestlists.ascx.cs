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

namespace Spotted.Pages
{
	public partial class Guestlists : DsiUserControl
	{
		protected DataList OtherEventDataList, CurrentEventDataList;
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (OtherEvent != null)
				{
					EventSet current = new EventSet(new Query(new Q(Event.Columns.K, CurrentEvent.K)));
					CurrentEventDataList.DataSource = current;
					CurrentEventDataList.ItemTemplate = this.LoadTemplate("/Templates/Events/Guestlist.ascx");
					CurrentEventDataList.DataBind();

					EventSet other = new EventSet(new Query(new Q(Event.Columns.K, OtherEvent.K)));
					OtherEventDataList.DataSource = other;
					OtherEventDataList.ItemTemplate = this.LoadTemplate("/Templates/Events/Guestlist.ascx");
					OtherEventDataList.DataBind();


				}
				else
				{
					Usr.Current.Guestlist(CurrentEvent.K, true, null);
					Response.Redirect(CurrentEvent.Url());
				}
			}
		}

		public void CurrentEvent_Click(object o, System.EventArgs e)
		{
			Usr.Current.Guestlist(OtherEvent.K, false, null);
			Usr.Current.Guestlist(CurrentEvent.K, true, null);
			Response.Redirect(CurrentEvent.Url());
		}

		public void OtherEvent_Click(object o, System.EventArgs e)
		{
			Response.Redirect(OtherEvent.Url());
		}

		#region CurrentEvent
		public Event CurrentEvent
		{
			get
			{
				if (currentEvent == null && ContainerPage.Url["EventK"].IsInt)
					currentEvent = new Event(ContainerPage.Url["EventK"]);
				return currentEvent;
			}
			set
			{
				currentEvent = value;
			}
		}
		private Event currentEvent;
		#endregion

		#region OtherEvent
		public Event OtherEvent
		{
			get
			{
				if (otherEvent == null)
				{
					EventSet es = Usr.Current.GuestlistEvents(CurrentEvent.DateTime, CurrentEvent.StartTime);
					if (es.Count > 0)
						otherEvent = es[0];
				}
				return otherEvent;
			}
			set
			{
				otherEvent = value;
			}
		}
		private Event otherEvent;
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
		}
		#endregion
	}
}
