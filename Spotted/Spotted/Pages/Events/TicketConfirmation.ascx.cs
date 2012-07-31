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
using System.Text;

namespace Spotted.Pages.Events
{
	public partial class TicketConfirmation : EventUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
				DisplayTicketMessage();
		}

		private void DisplayTicketMessage()
		{
			string[] ticketKs = new string[]{};
			List<Ticket> tickets = new List<Ticket>();
			
			try
			{
				ticketKs = this.ContainerPage.Url["ticketk"].Value.Split(',');
				foreach (string ticketK in ticketKs)
					tickets.Add(new Ticket(Convert.ToInt32(ticketK)));

				if (tickets.Count == 0)
					throw new Exception("No ticket ref #");
			}
			catch
			{
				this.TicketConfirmationLabel.Text = "Invalid ticket reference number.";
				return;
			}

			if (Usr.Current == null)
			{
				this.TicketConfirmationLabel.Text = "You must be logged in to see this page details.";
				return;
			}
			foreach(Ticket ticket in tickets)
			{
				if(Usr.Current.K != ticket.BuyerUsrK)
				{
					this.TicketConfirmationLabel.Text = "You did not make this ticket purchase.";
					return;
				}
			}
			try
			{
				int quantity = 0;
				string codes = "";
				bool hasDeliveryTickets = false;
				bool hasETickets = false;
				foreach(Ticket ticket in tickets)
				{
					
					

					ticket.VerifyTicketPurchase();
					quantity += ticket.Quantity;
					if (ticket.Code.Length > 0)
						codes += ticket.Code + ", ";
				}
				StringBuilder confirmationMessage = new StringBuilder();

				switch (tickets[0].TicketRun.DeliveryMethod)
				{
					case TicketRun.DeliveryMethodType.E_Ticket:
						confirmationMessage.Append("Thanks for buying " + (quantity > 1 ? "tickets. " + Ticket.ETICKET_CARD_REMINDER_PLURAL : "a ticket. " + Ticket.ETICKET_CARD_REMINDER_SINGULAR) + " (it ends \"" + tickets[0].CardNumberEnd + "\").");
						break;
					case TicketRun.DeliveryMethodType.SpecialDelivery:
						string[] address = new string[] { tickets[0].AddressName, tickets[0].AddressStreet, tickets[0].AddressTown, tickets[0].AddressArea, tickets[0].AddressCounty, new Country(tickets[0].AddressCountryK).Name, tickets[0].AddressPostcode };
						confirmationMessage.Append("Thanks for buying " + (quantity > 1 ? "tickets. " : "a ticket.") + " Your tickets will be delivered to: ");
						confirmationMessage.Append("<small>");
						foreach (string addressPart in address)
						{
							if (addressPart != "")
							{
								confirmationMessage.Append("<br>" + addressPart);
							}
						}
						confirmationMessage.Append("</small>");
						confirmationMessage.Append("<br>on " + tickets[0].TicketRun.DeliveryDate.ToLongDateString() + ". Please allow 1 week for delivery.");
						break;

					default: throw new NotImplementedException();
				}
				this.TicketConfirmationLabel.Text = confirmationMessage.ToString();

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
			catch (Exception ex)
			{
				this.TicketConfirmationLabel.Text = "The ticket purchase did not complete successfully.";
			}
		}
	}
}
