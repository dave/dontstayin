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
using Bobs.DataHolders;
using Common;

namespace Spotted.Styled
{
	public partial class Pay : StyledUserControl
	{
		private const string GENERIC_ERROR_MESSAGE = "If you wish to continue purchasing a ticket please click cancel, go back to the event page and start again.";

		public void Page_Init(object o, System.EventArgs e)
		{
			this.MyTicketsRepeater.ItemTemplate = this.LoadTemplate("/Templates/Usrs/EventTicketsRepeater.ascx");
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			ContainerPage.SslPage = true;

			if(Usr.Current == null)
			{
				// Redirect to login page
				Response.Redirect(this.StyledObject.UrlStyledApp("login") + "?url=" + HttpUtility.UrlEncode(HttpContext.Current.Request.Url.ToString()));
			}

			if (!this.IsPostBack)
			{
				ViewState["DuplicateGuid"] = Guid.NewGuid();
				EventHeader.InnerHtml = CurrentEvent.ToStyledHtml();
				Payment.BackgroundColour = "transparent";
				Payment.BorderColour = "black";

				try
				{
					if (TicketsToPurchase == null || TicketsToPurchase.Count == 0)
						throw new DsiUserFriendlyException("No ticket ref #.");

					ValidateTickets();
					DisplayTicketsToPurchase();
					LoadTicketsToPaymentControl();
					LoadMyTickets();
				}
				catch (DsiUserFriendlyException dsiufe)
				{
					// display error
					this.ErrorMessageLabel.Text = "<p>" + dsiufe.Message + " " + GENERIC_ERROR_MESSAGE + "</p>";
					this.ErrorMessageLabel.Visible = true;
					this.Payment.Visible = false;
				}
				catch (Exception)
				{
					// display generic error
					this.ErrorMessageLabel.Text = "<p>An error occurred. " + GENERIC_ERROR_MESSAGE + "</p>";
					this.ErrorMessageLabel.Visible = true;
					this.Payment.Visible = false;
				}
			}
		}

		#region Properties
		#region CurrentEvent
		public Event CurrentEvent
		{
			get
			{
				if (currentEvent == null)
				{
					if (EventK > 0)
						currentEvent = new Event(EventK);
				}
				return currentEvent;
			}
			set
			{
				currentEvent = value;
			}
		}
		Event currentEvent;
		#endregion

		#region EventK
		public int EventK
		{
			get
			{
				if (eventK == 0 && TicketKs.Length > 0)
				{
					eventK = new Ticket(Convert.ToInt32(TicketKs[0])).EventK;
				}

				return eventK;
			}
		}
		private int eventK = 0;
		#endregion

		#region CurrentUsrTickets
		public TicketSet CurrentUsrTickets
		{
			get
			{
				if (currentUsrTickets == null && Usr.Current != null)
					currentUsrTickets = Usr.Current.Tickets(CurrentEvent.K);
				return currentUsrTickets;
			}
			set
			{
				currentUsrTickets = null;
			}
		}
		private TicketSet currentUsrTickets;

		#endregion

		#region TicketKs
		public string[] TicketKs
		{
			get
			{
				return this.ContainerPage.Url["ticketk"].Value.Split(',');
			}
		}
		#endregion

		#region TicketsToPurchase
		public TicketSet TicketsToPurchase
		{
			get
			{
				if (ticketsToPurchase == null && TicketKs.Length > 0)
				{
					Query ticketsToPurchaseQuery = new Query(new Q(Ticket.Columns.K, TicketKs[0]));
					for (int i = 1; i < TicketKs.Length; i++)
					{
						ticketsToPurchaseQuery.QueryCondition = new Or(ticketsToPurchaseQuery.QueryCondition,
																	   new Q(Ticket.Columns.K, TicketKs[i]));
					}
					ticketsToPurchase = new TicketSet(ticketsToPurchaseQuery);
				}
				return ticketsToPurchase;
			}
		}
		private TicketSet ticketsToPurchase;
		#endregion

		#endregion

		#region Event Handlers
		protected void MyTicketsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Header)
			{
				if (CurrentUsrTickets != null && CurrentUsrTickets.Count > 0)
				{
					CurrentUsrTickets.Reset();
					foreach (Ticket ticket in CurrentUsrTickets)
					{
						if (ticket.Code.Length > 0)
						{
							try
							{
								((HtmlTableCell)e.Item.FindControl("CodeHeader")).InnerHtml = "Code";
							}
							catch { }
							break;
						}
					}
				}
			}
		}

		protected void CancelTicketPaymentButton_Click(object sender, EventArgs e)
		{
			Payment.Reset();
			Response.Redirect(StyledObject.UrlStyledEvent(this.EventK) + "?" + Cambro.Misc.Utility.GenRandomText(5));
		}

		#region PaymentReceived()
		public void PaymentReceived(object o, Controls.Payment.PaymentDoneEventArgs e)
		{
			List<Ticket> ticketsPurchased = UpdateTicketsFromPaymentControl();
			bool success = ticketsPurchased.Count > 0;
			if (ticketsPurchased.Count == 0)
			{
				if (TicketsToPurchase != null && TicketsToPurchase.Count > 0)
				{
					foreach (Ticket ticket in TicketsToPurchase)
					{
						if (ticket.Enabled)
						{
							if (ticket.VerifyTicketPurchase())
							{
								ticketsPurchased.Add(ticket);
								success = true;
							}
						}
					}
				}
			}
			if (success)
			{
				// If they are a registered DSI user, then treat it as if they bought tickets on DSI
				if (Usr.Current != null && Usr.Current.NickName.Length > 0)
				{
					Usr.Current.AttendEvent(CurrentEvent.K, true, null, null);
					Usr.Current.SetPrefsNextTicketFeedbackDate();
				}

				// Final attempt to fix unknown bug where ticket has InvoiceItemK = 0
				foreach (Ticket ticket in ticketsPurchased)
				{
					if (ticket.InvoiceItemK == 0)
					{
						bool flag = true;

						foreach (InvoiceDataHolder idh in Payment.Invoices)
						{
							if (flag)
							{
								foreach (InvoiceItemDataHolder iidh in idh.InvoiceItemDataHolderList)
								{
									if (iidh.K > 0 && iidh.BuyableObjectType == Model.Entities.ObjectType.Ticket && iidh.BuyableObjectK == ticket.K && iidh.Type == InvoiceItem.Types.EventTickets)
									{
										ticket.InvoiceItemK = iidh.K;
										flag = false;
										Utilities.AdminEmailAlert("Ticket.InvoiceItemK fixed", "Ticket.InvoiceItemK fixed", new Exception(), ticket);
										break;
									}
								}
							}
							else
								break;
						}
					}
				}

				ViewState["DuplicateGuid"] = Guid.NewGuid();
				Payment.Reset();

				string ticketKs = ticketsPurchased[0].K.ToString();
				for (int i = 1; i < ticketsPurchased.Count; i++)
					ticketKs += "," + ticketsPurchased[i].K.ToString();

				Response.Redirect(StyledObject.UrlStyledApp("ticketconfirmation", "ticketk", ticketKs), false);
			}
			else
			{
				try
				{
					Utilities.AdminEmailAlert("Error occurred on Styled.Pay in PaymentReceived()", "Error occurred on Styled.Pay in PaymentReceived()", new Exception(), ticketsPurchased.ConvertAll(ticket => (IBobAsHTML)ticket));
				}
				catch { }

				// display generic error
				this.ErrorMessageLabel.Text = "<p>An error occurred. " + GENERIC_ERROR_MESSAGE + "</p>";
				this.ErrorMessageLabel.Visible = true;
				this.Payment.Visible = false;
			}
		}
		#endregion
		#endregion

		#region Methods
		private void LoadMyTickets()
		{
			if (CurrentUsrTickets == null || CurrentUsrTickets.Count == 0)
			{
				this.MyTicketsPanel.Visible = false;
			}
			else
			{
				this.MyTicketsPanel.Visible = true;

				this.MyTicketsRepeater.DataSource = CurrentUsrTickets;
				this.MyTicketsRepeater.DataBind();
			}
		}

		private void DisplayTicketsToPurchase()
		{
			TicketDetails.InnerHtml = "";
			if (TicketsToPurchase != null && TicketsToPurchase.Count > 0)
			{
				TicketDetails.InnerHtml = "You've selected " + TicketsToPurchase[0].QuantityPriceName;
				for(int i=1; i<TicketsToPurchase.Count; i++)
					TicketDetails.InnerHtml += ", " + TicketsToPurchase[i].QuantityPriceName;
			}
		}

		private void LoadTicketsToPaymentControl()
		{	

			Payment.Reset();

			if (Usr.Current == null)
				throw new DsiUserFriendlyException("You must be logged in to purchase tickets.");

			InvoiceDataHolder idh = new InvoiceDataHolder();
			idh.ActionUsrK = Usr.Current.K;
			idh.CreatedDateTime = DateTime.Now;
			idh.DuplicateGuid = (Guid)ViewState["DuplicateGuid"];
			idh.PromoterK = 0;
			idh.Type = Invoice.Types.Invoice;
			idh.VatCode = Invoice.VATCodes.T1;
			DateTime now = DateTime.Now;

			foreach (Ticket ticket in TicketsToPurchase)
			{
				InvoiceItemDataHolder iidhTicket = new InvoiceItemDataHolder();
				InvoiceItemDataHolder iidhBookingFee = new InvoiceItemDataHolder();

				if (ticket.TicketRun.Promoter.VatStatus == Promoter.VatStatusEnum.Registered)
					iidhTicket.VatCode = InvoiceItem.VATCodes.T1;
				else
					iidhTicket.VatCode = InvoiceItem.VATCodes.T0;
				iidhTicket.SetTotal(ticket.Price);
				iidhTicket.Type = InvoiceItem.Types.EventTickets;
				iidhTicket.KeyData = ticket.K;
				iidhTicket.BuyableObjectK = ticket.K;
				iidhTicket.BuyableObjectType = Model.Entities.ObjectType.Ticket;
				iidhTicket.RevenueStartDate = now;
				iidhTicket.RevenueEndDate = now;
				iidhTicket.Description = ticket.Description;
				iidhTicket.ShortDescription = ticket.ShortDescription;

				iidhBookingFee.VatCode = InvoiceItem.VATCodes.T1;
				iidhBookingFee.SetTotal(ticket.BookingFee);
				iidhBookingFee.Type = InvoiceItem.Types.EventTicketsBookingFee;
				iidhBookingFee.KeyData = ticket.K;
				iidhBookingFee.BuyableObjectK = ticket.K;
				iidhBookingFee.BuyableObjectType = Model.Entities.ObjectType.Ticket;
				iidhBookingFee.RevenueStartDate = now;
				iidhBookingFee.RevenueEndDate = now;
				iidhBookingFee.Description = "Booking fee";
				iidhBookingFee.ShortDescription = "Booking fee";

				idh.InvoiceItemDataHolderList.Add(iidhTicket);
				idh.InvoiceItemDataHolderList.Add(iidhBookingFee);
			}

			//how many visits has this user had?
			Query qVisits = new Query();
			qVisits.QueryCondition = new Q(Visit.Columns.UsrK, Usr.Current.K);
			qVisits.ReturnCountOnly = true;
			VisitSet vsCount = new VisitSet(qVisits);

			if (vsCount.Count <= 20)
				Payment.FraudCheck = Transfer.FraudCheckEnum.Strict;

			Payment.Invoices.Add(idh);
			Payment.PromoterK = 0;
			Payment.AllowPayWithBalance = false;
			Payment.ShowItemsIncVat = true;
			Payment.UsrK = Usr.Current.K;
			Payment.Initialize();
			Payment.LoadBuyerDetailsToScreen();

			//this.TicketsPanel.Visible = false;
			//this.PayForTicketsPanel.Visible = true;
		}



		public void ValidateTickets()
		{
			foreach (Ticket ticket in TicketsToPurchase)
			{
				if (Usr.Current.K != ticket.BuyerUsrK)
				{
					// Not same user
					throw new DsiUserFriendlyException("This ticket was reserved for another user.");
				}
				else if (!ticket.IsReadyForProcessing(InvoiceItem.Types.EventTickets, ticket.Price, ticket.Price)
					 || !ticket.IsReadyForProcessing(InvoiceItem.Types.EventTicketsBookingFee, ticket.BookingFee, ticket.BookingFee))
				{
					// Ticket not ready for processing
					throw new DsiUserFriendlyException("Ticket is not able to be processed.");
				}
			}
		}

		public List<Ticket> UpdateTicketsFromPaymentControl()
		{
			List<Ticket> ticketsPurchased = new List<Ticket>();
			foreach (InvoiceDataHolder idh in this.Payment.Invoices)
			{
				if (idh.K > 0)
				{
					Invoice invoice = new Invoice(idh.K);
					//string ticketPurchaseMessage = "You just purchased ";
					//int ticketCounter = 0;
					foreach (InvoiceItem invoiceItem in invoice.Items)
					{
						if (invoiceItem.BuyableObjectType.Equals(Model.Entities.ObjectType.Ticket) && invoiceItem.Type.Equals(InvoiceItem.Types.EventTickets))
						{
							Ticket ticket = new Ticket(invoiceItem.BuyableObjectK);

							// Only update once. If page is refreshed, it shouldnt redo the updating of this ticket.
							if (ticket.InvoiceItemK == 0)
							{
								Transfer transfer = new Transfer();

								if (this.Payment.SecPay != null && this.Payment.SecPay.Transfer != null && this.Payment.SecPay.Transfer.K > 0)
									transfer = this.Payment.SecPay.Transfer;
								else if (invoice.SuccessfulAppliedTransfers.Count > 0 && invoice.SuccessfulAppliedTransfers[0].K > 0)
									transfer = invoice.SuccessfulAppliedTransfers[0];

								if (transfer.K > 0)
								{
									bool ticketWasEnabled = ticket.Enabled;
									ticket.AddressArea = Payment.CurrentUsr.AddressArea;
									if (ticket.AddressCountryK == 0)
										ticket.AddressCountryK = Payment.CurrentUsr.AddressCountryK;
									ticket.AddressPostcode = transfer.CardPostcode;
									ticket.AddressStreet = transfer.CardAddress1;
									ticket.AddressTown = Payment.CurrentUsr.AddressTown;
									ticket.CardNumberDigits = transfer.CardDigits;
									ticket.CardNumberEnd = transfer.CardNumberEnd;
									ticket.CardNumberHash = transfer.CardNumberHash;
									ticket.CardCV2 = transfer.CardCV2;
									string cardName = Utilities.StripTitleFromName(transfer.CardName);
									ticket.FirstName = Cambro.Misc.Utility.Snip(Utilities.GetFirstName(cardName), 100);
									ticket.LastName = Cambro.Misc.Utility.Snip(Utilities.GetLastName(cardName), 100);
									if (!ticket.Enabled && invoiceItem.K > 0 && transfer.Status == Transfer.StatusEnum.Success && Math.Round(transfer.Amount, 2) >= Math.Round(ticket.Price + ticket.BookingFee, 2) && ticket.Invoice != null && ticket.Invoice.Paid)
									{
										ticket.Enabled = true;
									}
									ticket.InvoiceItemK = invoiceItem.K;
									ticket.Mobile = Payment.CurrentUsr.Mobile;
									ticket.MobileCountryCode = Payment.CurrentUsr.MobileCountryCode;
									ticket.Update();

									if (ticket.Enabled)
									{
										Utilities.EmailStyledTicket(this.StyledObject, ticket);
										ticketsPurchased.Add(ticket);
									}

									if (!ticketWasEnabled && ticket.Enabled)
									{
										ticket.TicketRun.CalculateSoldTicketsAndUpdate();
									}
								}
								else
								{
									Utilities.AdminEmailAlert("<p>Error in Styled.Pay.UpdateTicketsFromPaymentControl(). Transfer not found.</p>",
																"Error in Styled.Pay.UpdateTicketsFromPaymentControl(). Transfer not found.", new DsiUserFriendlyException("Error in UpdateTicketsFromPaymentControl()"),
																new List<IBobAsHTML>() { ticket, invoice }, new string[] { Vars.EMAIL_ADDRESS_TIMI });
								}
							}
						}
					}
				}
			}

			return ticketsPurchased;
		}

		#endregion
	}
}
