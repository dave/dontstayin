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

namespace Spotted.Pages.Events
{
    public partial class BuyTickets : EventUserControl
    {
        private const string GENERIC_ERROR_MESSAGE = "If you wish to continue purchasing a ticket please click cancel, go back to the event page and start again.";

		public void Page_Init(object o, System.EventArgs e)
		{
			this.MyTicketsRepeater.ItemTemplate = this.LoadTemplate("/Templates/Usrs/EventTicketsRepeater.ascx");
		}

		protected void Page_Load(object sender, EventArgs e)
        {
            ContainerPage.SslPage = true;

            if (!this.IsPostBack)
            {
                ViewState["DuplicateGuid"] = Guid.NewGuid();

				//string[] ticketKs = new string[] { };
				//List<Ticket> ticketsToPurchase = new List<Ticket>();

                try
                {
                    //ticketKs = this.ContainerPage.Url["ticketk"].Value.Split(',');
                    //foreach (string ticketK in ticketKs)
                    //    ticketsToPurchase.Add(new Ticket(Convert.ToInt32(ticketK)));

                    if (TicketsToPurchase == null || TicketsToPurchase.Count == 0)
                        throw new DsiUserFriendlyException("No ticket ref #.");

                    ValidateTickets(TicketsToPurchase);
                    LoadTicketsToPaymentControl(TicketsToPurchase);
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

		public void LoadMyTickets()
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
								((HtmlTableCell)e.Item.FindControl("CodeHeader")).InnerHtml = "<small>Code</small>";
							}
							catch { }
							break;
						}
					}
				}
			}
		}

        public void ValidateTickets(TicketSet ticketsToPurchase)
        {
            foreach (Ticket ticket in ticketsToPurchase)
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
		const double deliveryFee = 5.0;
        public void LoadTicketsToPaymentControl(TicketSet ticketsToPurchase)
        {
            Payment.Reset();

            if (Usr.Current == null)
                throw new DsiUserFriendlyException("You must be logged in to purchase tickets.");

			if (!Usr.Current.IsEmailVerified || Usr.Current.IsEmailBroken)
				HttpContext.Current.Response.Redirect("/pages/emailverify?url=" + HttpContext.Current.Server.UrlEncode(HttpContext.Current.Request.Url.ToString()) + "&er=" + HttpContext.Current.Server.UrlEncode("You need to verify your email to buy tickets"));

            InvoiceDataHolder idh = new InvoiceDataHolder();
            idh.ActionUsrK = Usr.Current.K;
            idh.CreatedDateTime = DateTime.Now;
            idh.DuplicateGuid = (Guid)ViewState["DuplicateGuid"];
            idh.PromoterK = 0;
			idh.Type = Invoice.Types.Invoice;
            idh.VatCode = Invoice.VATCodes.T1;
            DateTime now = DateTime.Now;
			bool requiresDeliveryAddress = false;
            foreach (Ticket ticket in ticketsToPurchase)
            {
                InvoiceItemDataHolder ticketInvoiceItem = new InvoiceItemDataHolder();
                InvoiceItemDataHolder bookingFeeInvoiceItem = new InvoiceItemDataHolder();

                if (ticket.TicketRun.Promoter.VatStatus == Promoter.VatStatusEnum.Registered)
                    ticketInvoiceItem.VatCode = InvoiceItem.VATCodes.T1;
                else
                    ticketInvoiceItem.VatCode = InvoiceItem.VATCodes.T0;
                ticketInvoiceItem.SetTotal(ticket.Price);
                ticketInvoiceItem.Type = InvoiceItem.Types.EventTickets;
                ticketInvoiceItem.KeyData = ticket.K;
                ticketInvoiceItem.BuyableObjectK = ticket.K;
                ticketInvoiceItem.BuyableObjectType = Model.Entities.ObjectType.Ticket;
                ticketInvoiceItem.RevenueStartDate = now;
                ticketInvoiceItem.RevenueEndDate = now;
                ticketInvoiceItem.Description = ticket.Description;
                ticketInvoiceItem.ShortDescription = ticket.ShortDescription;

                bookingFeeInvoiceItem.VatCode = InvoiceItem.VATCodes.T1;
                bookingFeeInvoiceItem.SetTotal(ticket.BookingFee);
                bookingFeeInvoiceItem.Type = InvoiceItem.Types.EventTicketsBookingFee;
                bookingFeeInvoiceItem.KeyData = ticket.K;
                bookingFeeInvoiceItem.BuyableObjectK = ticket.K;
                bookingFeeInvoiceItem.BuyableObjectType = Model.Entities.ObjectType.Ticket;
                bookingFeeInvoiceItem.RevenueStartDate = now;
                bookingFeeInvoiceItem.RevenueEndDate = now;
                bookingFeeInvoiceItem.Description = "Booking fee";
                bookingFeeInvoiceItem.ShortDescription = "Booking fee";

                idh.InvoiceItemDataHolderList.Add(ticketInvoiceItem);
                idh.InvoiceItemDataHolderList.Add(bookingFeeInvoiceItem);
				if (ticket.TicketRun.DeliveryMethod == TicketRun.DeliveryMethodType.SpecialDelivery) 
				{ 
					requiresDeliveryAddress = true;
				}
            }
			if (requiresDeliveryAddress){
				TicketRun ticketRun = ticketsToPurchase[0].TicketRun;
				InvoiceItemDataHolder deliveryFeeInvoiceItem = new InvoiceItemDataHolder()
				{
					VatCode = InvoiceItem.VATCodes.T1,
					Type = InvoiceItem.Types.EventTicketsDelivery,
					Description = "Delivery by " + ticketRun.DeliveryMethod.ToString(),
					ShortDescription = "Delivery by " + ticketRun.DeliveryMethod.ToString(),
					RevenueStartDate = now,
					RevenueEndDate = now/*,
					BuyableObjectK = ticketsToPurchase[0].K,
					BuyableObjectType = Model.Entities.ObjectType.Ticket*/
				};
				deliveryFeeInvoiceItem.SetTotal(Convert.ToDecimal(ticketsToPurchase[0].TicketRun.DeliveryCharge));
				idh.InvoiceItemDataHolderList.Add(deliveryFeeInvoiceItem);

				Payment.GetFullAddress = true;
				Payment.LockCountryK(224);
				Payment.FraudCheck = Transfer.FraudCheckEnum.Strict;
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
            this.PayForTicketsPanel.Visible = true;
        }

        public void CancelTicketPaymentButton_Click(object o, System.EventArgs e)
        {
            Payment.Reset();
            Response.Redirect(CurrentEvent.Url() + "?" + Cambro.Misc.Utility.GenRandomText(5));
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
            if(success)
            {
                if (Usr.Current != null)
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

                string ticketConfirmationUrl = UrlInfo.MakeUrl(CurrentEvent.UrlFilterPart, "ticketconfirmation", "ticketk", ticketKs);

                if (ContainerPage is Master.DsiPage)
                    ((Master.DsiPage)ContainerPage).OverrideUsrRedirect = false;

                if (Usr.Current != null && Usr.Current.IsSkeleton)
                    Response.Redirect("/popup/welcome?url=" + HttpUtility.UrlEncode(ticketConfirmationUrl), false);
                else
                    Response.Redirect(ticketConfirmationUrl, false);
            }
            else
            {
				try
				{
					Utilities.AdminEmailAlert("Error occurred on BuyTickets in PaymentReceived()", "Error occurred on BuyTickets in PaymentReceived()", new Exception(), ticketsPurchased.ConvertAll(ticket => (IBobAsHTML)ticket));
				}
				catch { }

                // display generic error
                this.ErrorMessageLabel.Text = "<p>An error occurred. " + GENERIC_ERROR_MESSAGE + "</p>";
                this.ErrorMessageLabel.Visible = true;
                this.Payment.Visible = false;
            }
        }

	 
		 
        #endregion

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
									//ticket.AddressArea = Payment.CurrentUsr.AddressArea;
									ticket.AddressPostcode = transfer.CardPostcode;
									ticket.AddressStreet = transfer.CardAddress1;
									ticket.AddressCounty = transfer.CardAddressCounty;
									ticket.AddressCountryK = transfer.CardAddressCountryK;
									ticket.AddressTown = transfer.CardAddressTown;
									ticket.AddressArea = transfer.CardAddressArea;
									ticket.CardNumberDigits = transfer.CardDigits;
									ticket.CardNumberEnd = transfer.CardNumberEnd;
									ticket.CardNumberHash = transfer.CardNumberHash;
									ticket.AddressName = transfer.CardName;
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
										Utilities.EmailTicket(ticket);
										ticketsPurchased.Add(ticket);
									}

									ticket.TicketRun.CalculateSoldTicketsAndUpdate();
								}
								else
								{
									Utilities.AdminEmailAlert("<p>Error in UpdateTicketsFromPaymentControl(). Transfer not found.</p>",
																"Error in UpdateTicketsFromPaymentControl(). Transfer not found.", new DsiUserFriendlyException("Error in UpdateTicketsFromPaymentControl()"),
																new List<IBobAsHTML>() { ticket, invoice }, new string[] { Vars.EMAIL_ADDRESS_TIMI });
								}
                            }
                        }
                    }
                }
            }

            return ticketsPurchased;
        }
    }
}
