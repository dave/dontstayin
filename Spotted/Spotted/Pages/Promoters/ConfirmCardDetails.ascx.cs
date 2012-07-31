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

namespace Spotted.Pages.Promoters
{
	public partial class ConfirmCardDetails : PromoterUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				if (!CurrentPromoter.WillCheckCardsForPurchasedTickets)
				{
					throw new Exception("Not for you");
				}

				ContainerPage.SetPageTitle("Confirm Card Details");

				Query q = Event.GetEventsWithTicketsQuery(CurrentPromoter, DateTime.Today.AddDays(-14), DateTime.Today);
				q.OrderBy = new OrderBy(Event.Columns.K, OrderBy.OrderDirection.Descending);
				EventSet es = new EventSet(q);
				if (es.Count > 0)
				{
					this.uiEvents.DataSource = es;
					this.uiEvents.DataTextField = "Name";
					this.uiEvents.DataValueField = "K";
					this.uiEvents.DataBind();

					this.uiEvents.Items.Insert(0, new ListItem("", ""));

					this.uiSelect.Visible = true;
					this.uiNoEvents.Visible = false;
				}
				else
				{
					this.uiSelect.Visible = false;
					this.uiNoEvents.Visible = true;
				}
			}

			this.uiSomeWrongLabel.Visible = false;
		}
		protected void LoadTickets(object sender, EventArgs e)
		{
			if (uiEvents.SelectedValue == "")
			{
				uiDoorlistPanel.Visible = false;
			}
			else
			{
				uiDoorlist.CurrentEvent = new Event(int.Parse(uiEvents.SelectedValue));
				uiDoorlist.PromoterK = CurrentPromoter.K;
				uiDoorlist.DataBind();
				uiDoorlistPanel.Visible = true;
				uiSave.Visible = !uiDoorlist.NoTicketsToDisplay;
			}
		}

		protected void Save(object sender, EventArgs e)
		{
			bool mismatch = false;
			bool canHaveAnotherAttempt = false;
			Hashtable cv2s = uiDoorlist.CV2s;
			foreach (var key in cv2s.Keys)
			{
				string cv2 = (string)uiDoorlist.CV2s[key];
				if (cv2.Length == 0)
					continue;

				Ticket t = new Ticket((int)key);
				if (t.EventK != int.Parse(uiEvents.SelectedValue))
				{
					throw new Exception("Mismatch in event K");
				}
				if (t.TicketRun.PromoterK != CurrentPromoter.K)
				{
					throw new Exception("Mismatch in promoter K");
				}
				if (t.ConfirmCv2(cv2) == false)
				{
					mismatch = true;
					if (!t.HasExceededCardCheckAttempts)
					{
						canHaveAnotherAttempt = true;
					}
				}
			}

			this.uiDoorlist.DataBind();
			this.uiDoorlist.CV2s = cv2s;
			this.uiSomeWrongLabel.Visible = mismatch;
			this.uiSomeWrongLabel.Text = canHaveAnotherAttempt ? "Some CV2s didn't match, please check the list" : "You have exceeded maximum attempts";
		}
	}
}
