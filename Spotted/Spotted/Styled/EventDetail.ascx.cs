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
	public partial class EventDetail : StyledUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if(!this.StyledObject.IsEvent(CurrentEvent))
				throw new DsiUserFriendlyException("This event is not part of this microsite.");

			if (!this.IsPostBack)
			{
				EventHeader.InnerHtml = CurrentEvent.ToStyledHtml();
				EventPic.Src = CurrentEvent.AnyPicPath;
				EventShortDescription.InnerHtml = CurrentEvent.ShortDetailsHtml;
				LoadRunningTicketRuns();				
			}
		}

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
				return Convert.ToInt32(ContainerPage.Url[0].Raw);
			}
		}
		#endregion

		private void LoadRunningTicketRuns()
		{
			if (CurrentEvent.LatestEndOfTicketRunDateTime < Time.Now || CurrentEvent.TicketRuns.Count == 0)
			{
				RunningTicketRunsForPromoterRepeater.Visible = false;
				NoTicketsAvailableDiv.Visible = true;
			}
			else
			{
				RunningTicketRunsForPromoterRepeater.Visible = true;				

				bool currentUsrMaxTickets = false;
				if (Usr.Current != null)
					currentUsrMaxTickets = CurrentEvent.TicketsSoldForUsr(Usr.Current.K) >= Vars.TICKETS_MAX_PER_USR;
				List<TicketRun> runningTicketRunsForPromoter = new List<TicketRun>();
				if (this.StyledObject is Venue)
					runningTicketRunsForPromoter = CurrentEvent.RunningTicketRuns;
				else
					runningTicketRunsForPromoter = CurrentEvent.RunningTicketRunsForPromoter(this.StyledObject.PromoterK);

				RunningTicketRunsForPromoterRepeater.Visible = runningTicketRunsForPromoter.Count > 0 && !currentUsrMaxTickets;
				NoTicketsAvailableDiv.Visible = !RunningTicketRunsForPromoterRepeater.Visible;
				//TicketReminderP.Visible = RunningTicketRunsForPromoterRepeater.Visible;
				TicketNoteP.Visible = !RunningTicketRunsForPromoterRepeater.Visible;

				if (CurrentEvent.RunningTicketRuns.Count > 0)
				{
					if (currentUsrMaxTickets)
						TicketNoteP.InnerHtml = "You have already purchased the maximum number of tickets for this event";
					else
					{
						RunningTicketRunsForPromoterRepeater.DataSource = runningTicketRunsForPromoter;
						RunningTicketRunsForPromoterRepeater.DataBind();						

						if (CurrentEvent.RunningTicketRuns.Count == 1)
						{
							foreach (RepeaterItem rpi in RunningTicketRunsForPromoterRepeater.Items)
							{
								if (rpi.ItemType == ListItemType.Item || rpi.ItemType == ListItemType.AlternatingItem)
								{
									try
									{
										((TextBox)rpi.FindControl("NumberOfTicketsTextBox")).Text = "1";
									}
									catch { }
								}
							}
						}
					}
				}
				else
				{
					string noTicketsPresentlyAvailable = "Tickets for this event are not presently available. Check again at a later date.";
					if (CurrentEvent.HaveTicketRunsSoldOut || CurrentEvent.HaveTicketRunsFinished)
						//TicketNoteLabel.Text = "Tickets for this event have SOLD OUT";
						TicketNoteLabel.Text = noTicketsPresentlyAvailable; //"Tickets for this event have ENDED";
					else
					{
						DateTime ticketsStart = DateTime.MaxValue;
						foreach (TicketRun ticketRun in CurrentEvent.TicketRuns)
						{
							if ((ticketRun.Status.Equals(TicketRun.TicketRunStatus.WaitingStartDate) || ticketRun.Status.Equals(TicketRun.TicketRunStatus.WaitingToFollowOtherTicketRun)) && ticketRun.StartDateTime < ticketsStart)
								ticketsStart = ticketRun.StartDateTime;
						}
						if (ticketsStart < DateTime.MaxValue)
							TicketNoteLabel.Text = "Tickets for this event will be available for purchase on " + Cambro.Misc.Utility.FriendlyDate(ticketsStart) + " at " + ticketsStart.ToString("hh:mm");
						else
							TicketNoteLabel.Text = noTicketsPresentlyAvailable;
					}
				}
			}
		}

		protected void BuyButton_Click(object sender, EventArgs e)
		{
			LoadTicketsToPaymentControl();
		}

		public void LoadTicketsToPaymentControl()
		{
			// Validate number of tickets > 0
			// Validate if TicketRun is still running and has tickets available
			// Validate user and card have not exceeded maximum number of tickets
			List<Ticket> ticketsToPurchase = new List<Ticket>();
			string ticketKs = "";
			try
			{
				if (Usr.Current == null)
				{
					// Redirect to login page
					Response.Redirect(this.StyledObject.UrlStyledApp("login") + "?url=" + HttpUtility.UrlEncode(HttpContext.Current.Request.Url.ToString()));
				}

				ticketsToPurchase = GetTicketsFromScreen();
				if (ticketsToPurchase.Count == 0)
					throw new DsiUserFriendlyException("No tickets were loaded. Please check the number of tickets you have selected and try again.");

				bool readyForProcessing = true;
				int totalQuantity = CurrentEvent.TicketsSoldForUsr(Usr.Current.K);

				// Unreserve any previous pending tickets
				TicketSet pendingTickets = CurrentEvent.TicketsAwaitingPaymentForUsr(Usr.Current);
				foreach (Ticket pendingTicket in pendingTickets)
					pendingTicket.Unreserve();

				foreach (Ticket ticket in ticketsToPurchase)
				{
					totalQuantity += ticket.Quantity;
					if (ticket.Quantity == 0)
						throw new DsiUserFriendlyException("You cannot purchase 0 tickets");
					else if (totalQuantity > Vars.TICKETS_MAX_PER_USR)
						throw new DsiUserFriendlyException("This will exceed your maximum of " + Vars.TICKETS_MAX_PER_USR.ToString() + " tickets.");

					readyForProcessing = readyForProcessing && ticket.IsReadyForProcessing(InvoiceItem.Types.EventTickets, ticket.Price, ticket.Price);
					readyForProcessing = readyForProcessing && ticket.IsReadyForProcessing(InvoiceItem.Types.EventTicketsBookingFee, ticket.BookingFee, ticket.BookingFee);
				}

				if (readyForProcessing)
				{
					//ContainerPage.SslPage = true;
					ticketKs = ticketsToPurchase[0].K.ToString();
					for (int i = 1; i < ticketsToPurchase.Count; i++)
					{
						ticketKs += "," + ticketsToPurchase[i].K.ToString();
					}
					if (ticketKs.Length > 0)
					{
						Response.Redirect(this.StyledObject.UrlStyledApp("login") + "?url=" + this.StyledObject.UrlStyledApp("Pay", "ticketk", ticketKs), false);
					}
				}
				else
				{
					LoadRunningTicketRuns();

					this.AssignTicketsToPurchase(ticketsToPurchase);
				}
			}
			catch (Exception ex)
			{
				ProcessingVal.ErrorMessage = ex.Message;
				ProcessingVal.IsValid = false;

				LoadRunningTicketRuns();

				this.AssignTicketsToPurchase(ticketsToPurchase);

				foreach (Ticket ticket in ticketsToPurchase)
				{
					if (ticket.K > 0)
						ticket.Delete();
				}
			}
		}

		public List<Ticket> GetTicketsFromScreen()
		{
			List<Ticket> tickets = new List<Ticket>();

			if (Usr.Current == null)
			{
				// Redirect to login page
				Response.Redirect(this.StyledObject.UrlStyledApp("login") + "?url=" + HttpUtility.UrlEncode(HttpContext.Current.Request.Url.ToString()));
			}

			try
			{
				foreach (RepeaterItem rpi in RunningTicketRunsForPromoterRepeater.Items)
				{
					if (rpi.ItemType == ListItemType.Item || rpi.ItemType == ListItemType.AlternatingItem)
					{
						Ticket ticket = new Ticket();
						int quantity = 0;
						try
						{
							quantity = Convert.ToInt32(((HtmlSelect)rpi.FindControl("NumberOfTicketsDropDownList")).Value);
						}
						catch { }

						if (quantity > 0)
						{
							TicketRun ticketRun = new TicketRun(Convert.ToInt32(((TextBox)rpi.FindControl("TicketRunKTextBox")).Text));
							ticket = ticketRun.CreateTicket(Usr.Current, quantity);
							//double ticketPrice = Utilities.ConvertMoneyStringToDecimal(((Label)rpi.FindControl("TicketRunPriceLabel")).Text);
							//if (Math.Round(ticketPrice * quantity, 2) != Math.Round(ticket.Price, 2))
							//    throw new DsiUserFriendlyException("Price doesn't match.");

							ticket.SetIpAddressAndCountry();
							ticket.SetBrowserGuidToDsiCookieGuid();

							tickets.Add(ticket);
						}
					}
				}
			}
			catch
			{
				throw new DsiUserFriendlyException("Failed to get tickets. Please try again.");
			}

			return tickets;
		}

		public void AssignTicketsToPurchase(List<Ticket> ticketsToPurchase)
		{
			if (ticketsToPurchase != null && ticketsToPurchase.Count > 0)
			{
				if (Usr.Current == null)
					Response.Redirect(this.StyledObject.UrlStyledApp("login") + "?url=" + HttpUtility.UrlEncode(HttpContext.Current.Request.Url.ToString()));

				try
				{
					foreach (RepeaterItem rpi in RunningTicketRunsForPromoterRepeater.Items)
					{
						if (rpi.ItemType == ListItemType.Item || rpi.ItemType == ListItemType.AlternatingItem)
						{
							foreach (Ticket ticket in ticketsToPurchase)
							{
								if (ticket.TicketRunK == 0)
									break;

								try
								{
									TicketRun ticketRun = new TicketRun(Convert.ToInt32(((TextBox)rpi.FindControl("TicketRunKTextBox")).Text));
									if (ticket.TicketRunK == ticketRun.K)
									{
										if (ticket.Quantity > ticketRun.MaxTickets - ticketRun.SoldTickets)
										{
											ticket.Quantity = ticketRun.MaxTickets - ticketRun.SoldTickets;
										}

										int currentUsrTicketQuantity = CurrentEvent.TicketsSoldForUsr(Usr.Current.K);
										if (ticket.Quantity > Vars.TICKETS_MAX_PER_USR - currentUsrTicketQuantity)
										{
											ticket.Quantity = Vars.TICKETS_MAX_PER_USR - currentUsrTicketQuantity;
										}

										try
										{
											((HtmlSelect)rpi.FindControl("NumberOfTicketsDropDownList")).Value = ticket.Quantity.ToString();
										}
										catch { }

										break;
									}
								}
								catch { }
							}
						}
					}
				}
				catch { }
			}
		}
	}
}
