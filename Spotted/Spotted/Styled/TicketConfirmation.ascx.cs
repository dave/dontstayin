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
using Common;

namespace Spotted.Styled
{
	public partial class TicketConfirmation : StyledUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
				DisplayTicketMessage();
		}

		private void DisplayTicketMessage()
		{
			string[] ticketKs = new string[] { };
			List<Ticket> tickets = new List<Ticket>();

			try
			{
				ticketKs = this.ContainerPage.Url["ticketk"].Value.Split(',');
				foreach (string ticketK in ticketKs)
					tickets.Add(new Ticket(Convert.ToInt32(ticketK)));

				if (tickets.Count == 0)
					throw new DsiUserFriendlyException("No ticket ref #");
			}
			catch
			{
				this.TicketConfirmationLabel.Text = "Invalid ticket reference number.";
				return;
			}

			if (Usr.Current == null)
			{
				Response.Redirect(this.StyledObject.UrlStyledApp("login") + "?url=" + HttpUtility.UrlEncode(HttpContext.Current.Request.Url.ToString()));
			}
			foreach (Ticket ticket in tickets)
			{
				if (Usr.Current.K != ticket.BuyerUsrK)
				{
					this.TicketConfirmationLabel.Text = "You did not make this ticket purchase.";
					return;
				}
				if (!this.StyledObject.IsEvent(ticket.Event))
				{
					this.TicketConfirmationLabel.Text = "This ticket is for an event not belonging to this microsite.";
					return;
				}
			}
			EventLinkLabel.Text = this.StyledObject.UrlStyledEventLink(tickets[0].Event);
			try
			{
				int quantity = 0;
				string codes = "";

				foreach (Ticket ticket in tickets)
				{
					ticket.VerifyTicketPurchase();
					quantity += ticket.Quantity;
					if (ticket.Code.Length > 0)
						codes += ticket.Code + ", ";
				}

				this.TicketConfirmationLabel.Text = "Thanks for buying " + (quantity > 1 ? "tickets. " + Ticket.ETICKET_CARD_REMINDER_PLURAL : "a ticket. " + Ticket.ETICKET_CARD_REMINDER_SINGULAR)
													+ " (it ends \"" + tickets[0].CardNumberEnd + "\").";

				if (codes.Length > 0)
				{
					codes = codes.Substring(0, codes.Length - 2);
					this.TicketConfirmationLabel.Text += "<p><br><font size='+1'>" + (codes.IndexOf(",") > 0 ? Ticket.ETICKET_CODE_REMINDER_PLURAL : Ticket.ETICKET_CODE_REMINDER_SINGULAR) + ": \"" + codes + "\"</font><br><br></p>";
				}

			}
			catch (DsiUserFriendlyException dufe)
			{
				this.TicketConfirmationLabel.Text = dufe.Message;
			}
			catch (Exception)
			{
				this.TicketConfirmationLabel.Text = "The ticket purchase did not complete successfully.";
			}
		}
	}
}
