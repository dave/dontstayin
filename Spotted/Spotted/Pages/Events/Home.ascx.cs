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
using System.Text;
using System.Text.RegularExpressions;

using Bobs;
using Common;

namespace Spotted.Pages.Events
{
	public partial class Home : EventUserControl
	{
		protected Panel EventSelectedPanel;

		public void Page_Init(object o, System.EventArgs e)
		{
			if (CurrentEvent != null)
				CurrentEvent.AddRelevant(ContainerPage);
			this.MyTicketsRepeater.ItemTemplate = this.LoadTemplate("/Templates/Usrs/EventTicketsRepeater.ascx");


			PhotoUploadPanel.Visible = !CurrentEvent.IsFuture;
		}

		#region Page_Load
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (CurrentEvent == null)
			{
				throw new Exception("No event selected");
			}

			ChangePanel(EventSelectedPanel);

			SetPageTitle(CurrentEvent.Name);

			if (Usr.Current != null && Usr.Current.IsAdmin)
			{
				ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p><a href=\"/admin/addpic?Type=Event&K=" + EventK + "\">Add pic to this event</a></p>"));
				ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p><a href=\"http://old.dontstayin.com/login-" + Usr.Current.K + "- " + Usr.Current.LoginString + "/admin/event?ID=" + EventK + "\">Edit this event</a></p>"));
				ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p><a onclick=\"return confirm('This will delete ALL attached objects.\\nARE YOU SURE?');\" href=\"/admin/multidelete?ObjectType=Event&ObjectK=" + CurrentEvent.K + "\">Delete this event</a><br>Be careful - deletes all galleries, photos, threads, comments etc.</p>"));
			}

			if (Usr.Current != null && Usr.Current.IsSuper)
			{
				ContainerPage.Menu.Admin.SuperAdmin.Controls.Add(new LiteralControl("<p><b>Edit this event:</b><br>"));
				ContainerPage.Menu.Admin.SuperAdmin.Controls.Add(new LiteralControl("<a href=\"/event-" + EventK + "/edit/page-date\">Date</a><br>"));
				ContainerPage.Menu.Admin.SuperAdmin.Controls.Add(new LiteralControl("<a href=\"/event-" + EventK + "/edit/page-details\">Details</a><br>"));
				ContainerPage.Menu.Admin.SuperAdmin.Controls.Add(new LiteralControl("<a href=\"/event-" + EventK + "/edit/page-musictype\">MusicTypes</a><br>"));
				ContainerPage.Menu.Admin.SuperAdmin.Controls.Add(new LiteralControl("<a href=\"/event-" + EventK + "/edit/page-pic\">Picture</a></p>"));

				ContainerPage.Menu.Admin.SuperAdmin.Controls.Add(new LiteralControl("<p><a href=\"" + CurrentEvent.UrlApp("delete") + "\">Delete this event</a></p>"));
			}

			if (!this.IsPostBack)
			{
				LoadRunningTicketRuns();
				LoadTicketFeedback();
				ViewState["DuplicateGuid"] = Guid.NewGuid();

				//if (CurrentEvent.ShowHotelLink)
				//{
				//    uiHotelBannerLink.HRef = CurrentEvent.FindHotelLink(Model.Entities.Event.HotelLinkSources.Banner);
				//}
				//else
				//{
				//    uiHotelBannerDiv.Visible = false;
				//}
			}
			LoadMyTickets();
			TicketPurchaseResults.Visible = false;
			TicketPurchaseJavascriptLabel.Visible = false;

			SpotterRequestNonSpotter.Visible = Usr.Current == null || !Usr.Current.IsSpotter;
			SpotterRequestSpotter.Visible = Usr.Current != null && Usr.Current.IsSpotter;
			SpotterRequestPanel.Visible = CurrentEvent.SpotterRequest.HasValue && CurrentEvent.SpotterRequest.Value;
			SpotterRequestName.Text = CurrentEvent.SpotterRequestName;
			SpotterRequestPhone.Text = CurrentEvent.SpotterRequestNumber;
		}
		#endregion

		#region TicketsPanels
		private void LoadTicketFeedback()
		{
			this.UsrTicketResponseGoodLinkButtonDiv.Visible = false;
			this.UsrTicketResponseGoodDiv.Visible = false;
			this.UsrTicketResponseBadLinkButtonDiv.Visible = false;
			this.UsrTicketResponseBadDiv.Visible = false;
			
			if (CurrentEvent.DateTime < DateTime.Today && CurrentUsrTickets != null && CurrentUsrTickets.Count > 0)
			{
   				TicketFeedbackP.Visible = false;

                foreach (Ticket ticket in CurrentUsrTickets)
                {
                    if (!ticket.Cancelled)
                    {
                        TicketFeedbackP.Visible = true;
                        break;
                    }
                }

				this.UsrTicketFeedbackHeaderLabel.Visible = CurrentUsrTickets[0].Feedback == Ticket.FeedbackEnum.None;

				// Since all tickets should have the same feedback, use the first one to get feedback details.
				if (CurrentUsrTickets[0].Feedback == Ticket.FeedbackEnum.Good)
				{
					this.UsrTicketResponseGoodDiv.Visible = true;
				}
				else if (CurrentUsrTickets[0].Feedback == Ticket.FeedbackEnum.Bad)
				{
					this.UsrTicketResponseBadDiv.Visible = true;
					this.UsrTicketFeedbackLabel.Text = CurrentUsrTickets[0].FeedbackNote;
					this.UsrTicketFeedbackLabel.Visible = CurrentUsrTickets[0].FeedbackNote.Length > 0;
				}
				else
				{
					this.UsrTicketResponseGoodLinkButtonDiv.Visible = true;
					this.UsrTicketResponseBadLinkButtonDiv.Visible = true;
				}
			}
			else
			{
				TicketFeedbackP.Visible = false;
			}
		}

		public void LoadRunningTicketRuns()
		{
			if (CurrentEvent.LatestEndOfTicketRunDateTime < Time.Now || CurrentEvent.TicketRuns.Count == 0)
			{
				TicketsPanel.Visible = false;
			}
			else
			{
				TicketsPanel.Visible = true;
				bool currentUsrMaxTickets = false;
				if (Usr.Current !=null)
					currentUsrMaxTickets = CurrentEvent.TicketsSoldForUsr(Usr.Current.K) >= Vars.TICKETS_MAX_PER_USR;

				BuyTicketsP.Visible = CurrentEvent.RunningTicketRuns.Count > 0 && !currentUsrMaxTickets;
				ETicketReminderP.Visible  = CurrentEvent.RunningTicketRuns.Exists(tr => tr.DeliveryMethod == TicketRun.DeliveryMethodType.E_Ticket);
				TicketNoteP.Visible = !BuyTicketsP.Visible;

				if (CurrentEvent.RunningTicketRuns.Count > 0 )
				{
					if (currentUsrMaxTickets)
						TicketNoteLabel.Text = "You have already purchased the maximum number of tickets for this event";
					else
					{
						//ViewState["TicketRunCounter"] = "0";
						this.RunningTicketRunsRepeater.DataSource = CurrentEvent.RunningTicketRuns;
						this.RunningTicketRunsRepeater.DataBind();
						//this.ContainerPage.SslPage = true;

						if (CurrentEvent.RunningTicketRuns.Count == 1)
						{
							foreach (RepeaterItem rpi in RunningTicketRunsRepeater.Items)
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
                    string noTicketsPresentlyAvailable = "Tickets for this event are not presently available on DontStayIn. Check again at a later date.";
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

		public void LoadMyTickets()
		{
			if (CurrentUsrTickets == null || CurrentUsrTickets.Count == 0)
			{
				this.MyTicketsPanel.Visible = false;
			}
			else
			{
				this.MyTicketsPanel.Visible = true;
				ViewState["MyTicketsCounter"] = "0";

				
				this.MyTicketsRepeater.DataSource = CurrentUsrTickets;
				this.MyTicketsRepeater.DataBind();

				List<string> cardsEndingWith = new List<string>();
				int quantity = 0;
				foreach (Ticket ticket in CurrentUsrTickets)
				{
					if (ticket.CardNumberEnd.Length > 0 && !cardsEndingWith.Contains(ticket.CardNumberEnd))
						cardsEndingWith.Add(ticket.CardNumberEnd);
					quantity += ticket.Quantity;
				}
			}
		}

		protected void UsrTicketResponseGoodLinkButton_Click(object sender, EventArgs e)
		{
			Response.Redirect(CurrentEvent.UrlTicketFeedback(Ticket.FeedbackEnum.Good));
		}

		protected void UsrTicketResponseBadLinkButton_Click(object sender, EventArgs e)
		{
			Response.Redirect(CurrentEvent.UrlTicketFeedback(Ticket.FeedbackEnum.Bad));
		}

		protected void RunningTicketRunsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			// Execute the following logic for Items and Alternating Items.
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				HtmlAnchor addTicketImageLink = (HtmlAnchor)e.Item.FindControl("AddTicketImageLink");
				HtmlAnchor subtractTicketImageLink = (HtmlAnchor)e.Item.FindControl("SubtractTicketImageLink");

				if (addTicketImageLink != null)
					addTicketImageLink.Attributes["onclick"] = "EventHomeUpdateTextBoxNumber('" + e.Item.FindControl("NumberOfTicketsTextBox").ClientID + "', 1);return false;";
				if (subtractTicketImageLink != null)
					subtractTicketImageLink.Attributes["onclick"] = "EventHomeUpdateTextBoxNumber('" + e.Item.FindControl("NumberOfTicketsTextBox").ClientID + "', -1);return false;";

				//ViewState["TicketRunCounter"] = ((int)(Convert.ToInt32(ViewState["TicketRunCounter"]) + 1)).ToString();
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

		public void BuyTicketsButton_Click(object o, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn();
			LoadTicketsToPaymentControl();
		}

		public void LoadTicketsToPaymentControl()
		{
			if (Usr.Current == null)
				throw new DsiUserFriendlyException("You must be logged in to purchase tickets.");

			if (!Usr.Current.IsEmailVerified || Usr.Current.IsEmailBroken)
				HttpContext.Current.Response.Redirect("/pages/emailverify?url=" + HttpContext.Current.Server.UrlEncode(HttpContext.Current.Request.Url.ToString()) + "&er=" + HttpContext.Current.Server.UrlEncode("You need to verify your email to buy tickets"));

			// Validate number of tickets > 0
			// Validate if TicketRun is still running and has tickets available
			// Validate user and card have not exceeded maximum number of tickets
			List<Ticket> ticketsToPurchase = new List<Ticket>();
            string ticketKs = "";
			try
			{
				this.TicketsPanel.Visible = true;

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
                        Response.Redirect(CurrentEvent.UrlApp("buytickets", "ticketk", ticketKs), false);
                    }
					//LoadTicketsToPaymentControl(ticketsToPurchase);
				}
				else
				{
					LoadRunningTicketRuns();

					this.AssignTicketsToPurchase(ticketsToPurchase);
				}
			}
			catch(Exception ex)
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

		public void AssignTicketsToPurchase(List<Ticket> ticketsToPurchase)
		{
			if (ticketsToPurchase != null && ticketsToPurchase.Count > 0)
			{
				if (Usr.Current == null)
					throw new DsiUserFriendlyException("You must be logged in to purchase tickets.");

				try
				{
					foreach (RepeaterItem rpi in RunningTicketRunsRepeater.Items)
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
											((TextBox)rpi.FindControl("NumberOfTicketsTextBox")).Text = ticket.Quantity.ToString();
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

		public List<Ticket> GetTicketsFromScreen()
		{
			List<Ticket> tickets = new List<Ticket>();

			if (Usr.Current == null)
				throw new DsiUserFriendlyException("You must be logged in to purchase tickets.");

			try
			{
				foreach (RepeaterItem rpi in RunningTicketRunsRepeater.Items)
				{
					if (rpi.ItemType == ListItemType.Item || rpi.ItemType == ListItemType.AlternatingItem)
					{
						Ticket ticket = new Ticket();
						int quantity = 0;
						try
						{
							quantity = Convert.ToInt32(((TextBox)rpi.FindControl("NumberOfTicketsTextBox")).Text);
						}
						catch { }

						if (quantity > 0)
						{
							TicketRun ticketRun = new TicketRun(Convert.ToInt32(((TextBox)rpi.FindControl("TicketRunKTextBox")).Text));
							ticket = ticketRun.CreateTicket(Usr.Current, quantity);
							var ticketPrice = Utilities.ConvertMoneyStringToDecimal(((Label)rpi.FindControl("TicketRunPriceLabel")).Text);
							if (Math.Round(ticketPrice * quantity, 2) != Math.Round(ticket.Price, 2))
								throw new DsiUserFriendlyException("Price doesn't match.");

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

		#endregion

		#region UsrEventAttendListPanel
		public void UsrEventAttendList_Load(object o, System.EventArgs e)
		{
			if (ContainerPage.Url["ignore"].Exists)
			{
				Usr.KickUserIfNotLoggedIn("You have to be logged in to do this!");
				try
				{
					UsrEventAttended u1 = new UsrEventAttended(Usr.Current.K, EventK);
					u1.SendUpdate = false;
					u1.Update();
				}
				catch { };
			}

			

			#region UsrEventAttend radiobuttons
			UsrEventAttendListLabelPast.Visible = !CurrentEvent.IsFuture;
			UsrEventAttendListLabelFuture.Visible = CurrentEvent.IsFuture;
			#endregion

			#region Spotter radiobuttons
			UsrEventSpotterFutureLabel.Visible = CurrentEvent.IsFuture;
			UsrEventSpotterPastLabel.Visible = !CurrentEvent.IsFuture;
			UsrEventSpotterYes.Text = CurrentEvent.IsFuture ? "Yes - camera ready!" : "Yes";
			UsrEventSpotterNo.Text = CurrentEvent.IsFuture ? "Not this one" : "No";
			#endregion

			UsrEventSpotterButtonsPanel.Visible = (Usr.Current != null && Usr.Current.IsSpotter && !CurrentEvent.NoPhotos);
		}
		public void UsrEventAttendList_PreRender(object o, System.EventArgs e)
		{
			uiAttendedEvent.ThisEvent = CurrentEvent;
			uiAttendedEvent.DataBind();
			uiAttendedEvent.UsrAttendingEvent += new EventHandler(uiAttendedEvent_UsrAttendingEvent);
			UsrEventAttendListBuild();
			UsrEventSpotterButtons();
		}

		void uiAttendedEvent_UsrAttendingEvent(object sender, EventArgs e)
		{
			
		}

		public void UsrEventSpotterClick(object o, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn("You'll have to be logged in to do this...");
			if (!Usr.Current.IsSpotter)
				throw new DsiUserFriendlyException("You must be a spotter to do this!");

			Usr.Current.AttendEvent(CurrentEvent.K, true, UsrEventSpotterYes.Checked, null);

		}


		#region UsrEventAttendButtons()
		void UsrEventSpotterButtons()
		{
			if (Usr.Current != null)
			{
				bool found = false;
				try
				{
					UsrEventAttended u1 = new UsrEventAttended(Usr.Current.K, EventK);
					found = u1.Spotter;
				}
				catch { };
				UsrEventSpotterYes.Checked = found;
				UsrEventSpotterNo.Checked = !found;

			}
		}
		#endregion
		#region UsrEventAttendListBuild()
		void UsrEventAttendListBuild()
		{
			Event ev = new Event(EventK);
			bool first = true;

			Query q = new Query();
			q.QueryCondition = Usr.IsDisplayedInUsrLists;

			q.TableElement = new Join(
				new TableElement(TablesEnum.Usr),
				new TableElement(TablesEnum.UsrEventAttended),
				QueryJoinType.Inner,
				new And(new Q(Usr.Columns.K, UsrEventAttended.Columns.UsrK, true), new Q(UsrEventAttended.Columns.EventK, CurrentEvent.K))
			);
			if (Usr.Current != null)
			{
				q.TableElement = new Join(
					q.TableElement,
					new TableElement(TablesEnum.Buddy),
					QueryJoinType.Left,
					new And(
						new Q(Usr.Columns.K, Buddy.Columns.UsrK, true),
						new Q(Buddy.Columns.BuddyUsrK, Usr.Current.K),
						new Q(Buddy.Columns.FullBuddy, true)
						)
					);

				q.OrderBy = new OrderBy(Usr.Columns.NickName);

				q.Columns = new ColumnSet(
					UsrEventAttended.Columns.Spotter,
					Usr.LinkColumns,
					Buddy.Columns.FullBuddy);
			}
			else
			{
				q.OrderBy = new OrderBy(Usr.Columns.NickName);
				q.Columns = new ColumnSet(UsrEventAttended.Columns.Spotter, Usr.LinkColumns);
			}

			UsrSet usrAttended = new UsrSet(q);

			StringBuilder b = new StringBuilder();


			if (usrAttended.Count > 0)
			{
				b.Append("<small>");
				foreach (Usr u in usrAttended)
				{
					if (!first)
						b.Append(", ");
					if (Usr.Current != null && (u.JoinedBuddy.FullBuddy || u.K == Usr.Current.K))
						b.Append("</small>");

					if (u.JoinedUsrEventAttend.Spotter)
						b.Append("<img src=\"" + (u.IsProSpotter ? "/gfx/icon10-prospotter.png" : "/gfx/icon10-spotter.png") + "\" border=\"0\" height=\"10\" width=\"12\" align=\"absmiddle\" style=\"margin-right:2px;\" onmouseover=\"stt('" + u.NickName + " is a " + (u.IsProSpotter ? "pro " : "") + "spotter')\" onmouseout=\"htm();\">");
					b.Append("<a href=\"");
					b.Append(u.Url());
					b.Append("\"");
					if (Usr.Current != null && (u.JoinedBuddy.FullBuddy || u.K == Usr.Current.K))
						b.Append(" style=\"font-weight:bold;\"");
					u.MakeRollover(b);
					b.Append(">");
					b.Append(u.NickName);
					b.Append("</a>");


					if (Usr.Current != null && (u.JoinedBuddy.FullBuddy || u.K == Usr.Current.K))
						b.Append("<small>");
					first = false;
				}
				b.Append("</small>");
				UsrEventAttendList.Controls.Add(new LiteralControl(b.ToString()));
			}
			else
				UsrEventAttendP.Visible = false;

			string BoldBuddies = "";
			if (Usr.Current != null)
				BoldBuddies = " (your buddies are in bold)";

			if (ev.Capacity == 0)
			{
				if (usrAttended.Count == 0)
					EventCapacityP.Visible = false;
				else if (usrAttended.Count == 1)
					EventCapacityLabel.Text = "One of our members " + (CurrentEvent.IsFuture ? "will be" : "was") + " there:";
				else
					EventCapacityLabel.Text = usrAttended.Count.ToString("#,##0") + " of our members " + (CurrentEvent.IsFuture ? "will be" : "were") + " there" + BoldBuddies + ":";
			}
			else
			{
				if (usrAttended.Count == 0)
					EventCapacityLabel.Text = "Event capacity - about " + ev.Capacity.ToString("#,##0") + " people.";
				else if (usrAttended.Count == 1)
					EventCapacityLabel.Text = "Event capacity - about " + ev.Capacity.ToString("#,##0") + " people, and one of our members " + (CurrentEvent.IsFuture ? "will be" : "was") + " there:";
				else
					EventCapacityLabel.Text = "Event capacity - about " + ev.Capacity.ToString("#,##0") + " people, and " + usrAttended.Count.ToString("#,##0") + " of our members " + (CurrentEvent.IsFuture ? "will be" : "were") + " there" + BoldBuddies + ":";
			}
		}
		#endregion

		#endregion

		#region SpotterSignUpPanel
		protected Panel SpotterSignUpPanel, NoSpotterSignUpPanel;
		protected HtmlAnchor SpotterSignUpLink1, SpotterSignUpLink2;
		public void SpotterSignUp_Load(object o, System.EventArgs e)
		{
			bool isSpotter = Usr.Current != null && Usr.Current.IsSpotter;

			SpotterSignUpPanel.Visible = false;
			NoSpotterSignUpPanel.Visible = false;

			if (CurrentEvent.IsFuture && !isSpotter)
			{
				NoSpotterSignUpPanel.Visible = !CurrentEvent.HasSpotter;
				SpotterSignUpPanel.Visible = CurrentEvent.HasSpotter;
			}

			SpotterSignUpLink1.HRef = "/pages/spotters/eventk-" + CurrentEvent.K.ToString();
			SpotterSignUpLink2.HRef = "/pages/spotters/eventk-" + CurrentEvent.K.ToString();
		}

		#endregion

		#region InfoPanel
		protected Panel InfoPanel;
		protected Spotted.CustomControls.h1 EventBodyTitle;
		protected PlaceHolder EventBody, EventStartTimeDescription, EventDetailsPlainPh;
		protected Label MusicTypeLabel;
		protected Panel ReviewsPanel;
		protected HtmlGenericControl MusicTypeP;
		protected Controls.Latest Latest;
		public void Info_Load(object o, System.EventArgs e)
		{
			#region StartTime
			if (!CurrentEvent.StartTime.Equals(Event.StartTimes.Evening))
			{
				if (CurrentEvent.StartTime.Equals(Event.StartTimes.Daytime))
					EventStartTimeDescription.Controls.Add(new LiteralControl("<p><b>This is a daytime event (e.g. starts late morning or early afternoon - see below for times).</b></p>"));
				else if (CurrentEvent.StartTime.Equals(Event.StartTimes.Morning))
					EventStartTimeDescription.Controls.Add(new LiteralControl("<p><b>This is a morning event (e.g. starts early - see below for times).</b></p>"));
			}
			#endregion

			#region MusicTypesString
			if (CurrentEvent.MusicTypesString.Length > 0)
			{
				MusicTypeP.Visible = true;
				MusicTypeLabel.Text = CurrentEvent.MusicTypesString;
			}
			else
				MusicTypeP.Visible = false;
			#endregion

			EventBodyTitle.InnerText = "Info";

			HtmlRenderer r = new HtmlRenderer();
			r.Formatting = !CurrentEvent.LongDetailsPlain;
			r.Container = !CurrentEvent.LongDetailsPlain;
			r.LoadHtml(CurrentEvent.LongDetailsHtml);

			if (r.Container)
			{
				EventBody.Controls.Add(new LiteralControl(r.Render(EventBody)));
			}
			else
			{
				EventDetailsPlainPh.Controls.Add(new LiteralControl("<div style=\"width:634px; overflow:hidden;\">" + r.Render(EventDetailsPlainPh) + "</div>"));
			}

			Latest.Parent = CurrentEvent;

			//ReviewsPanel.Visible = (CurrentEvent.DateTime <= DateTime.Now);
		}
		//public void AddReviewClick(object o, System.EventArgs e)
		//{
		//    Response.Redirect(CurrentEvent.UrlApp("review"));
		//}
		public void Info_PreRender(object o, System.EventArgs e)
		{

		}
		#endregion

		#region GalleriesPanel
		protected Panel GalleriesPanel;
		protected DataList GalleryDataList;
		public void Galleries_Load(object o, System.EventArgs e)
		{
			if (CurrentEvent.DateTime <= DateTime.Today)
			{
				Query q = new Query();
				q.QueryCondition = new And(
					new Q(Gallery.Columns.EventK, CurrentEvent.K),
					Gallery.ShowOnSiteQ);
				q.TableElement = Templates.Galleries.EventHome.PerformJoins(null);
				q.Columns = Templates.Galleries.EventHome.Columns;
				q.OrderBy = Gallery.Order;
				GallerySet gs = new GallerySet(q);

				GalleryDataList.DataSource = gs;
				if (gs.Count > 18)
				{
					GalleryDataList.ItemTemplate = this.LoadTemplate("/Templates/Galleries/EventHomeSmall.ascx");
					GalleryDataList.RepeatColumns = 2;
					GalleryDataList.ItemStyle.HorizontalAlign = HorizontalAlign.Left;
					GalleryDataList.ItemStyle.Width = new Unit("50%");
					GalleryDataList.CellPadding = 0;
				}
				else
				{
					GalleryDataList.ItemTemplate = this.LoadTemplate("/Templates/Galleries/EventHome.ascx");
				}
				GalleryDataList.DataBind();
			}
			else
				GalleriesPanel.Visible = false;
		}
		public void AddGalleryClick(object o, System.EventArgs e)
		{
			Response.Redirect(CurrentEvent.UrlGalleryEdit);
		}

		#endregion

		#region Misc panels
		protected Panel NoSpotterPanel;
		public void Misc_Load(object o, System.EventArgs e)
		{

		}
		#endregion

		#region TodayEventsPanel
		protected Panel TodayEventsPanel;
		protected Spotted.CustomControls.h1 TodayEventsHeader;
		protected DataList TodayEventsDataList;
		public void TodayEvents_Load(object o, System.EventArgs e)
		{
			if (CurrentEvent.IsFuture)
			{
				Query q = new Query();
				q.QueryCondition = new And(
					new Q(Event.Columns.DateTime, CurrentEvent.DateTime),
					new Q(Venue.Columns.PlaceK, CurrentEvent.Venue.PlaceK),
					TodayEventsMusicFilterQ,
					new Q(Event.Columns.K, QueryOperator.NotEqualTo, CurrentEvent.K)
					);
				if (TodayEventsFilterByMusic)
				{
					q.TableElement = Event.PlaceAndMusicTypeJoin;
					q.Distinct = true;
					q.DistinctColumn = Event.Columns.K;
					q.DataTableElement = Event.PlaceJoin;
				}
				else
					q.TableElement = Event.PlaceJoin;
				q.Columns = Templates.Events.EventHomeToday.Columns;
				q.OrderBy = Event.FutureEventOrder;
				EventSet es = new EventSet(q);
				if (es.Count == 0)
					TodayEventsPanel.Visible = false;
				else
				{
					TodayEventsHeader.InnerText = TodayEventsHeader.InnerText.Replace("$", CurrentEvent.FriendlyDate(false));
					TodayEventsHeader.InnerText = TodayEventsHeader.InnerText.Replace("?", CurrentEvent.Venue.Place.Name);

					TodayEventsDataList.ItemTemplate = this.LoadTemplate("/Templates/Events/EventHomeToday.ascx");
					TodayEventsDataList.DataSource = es;
					TodayEventsDataList.DataBind();
				}
			}
			else
				TodayEventsPanel.Visible = false;
		}
		bool TodayEventsFilterByMusic
		{
			get
			{
				return !(CurrentEvent.MusicTypes.Count == 0 || (CurrentEvent.MusicTypes.Count == 1 && CurrentEvent.MusicTypes[0].K == 1));
			}
		}
		protected Q TodayEventsMusicFilterQ
		{
			get
			{
				if (!TodayEventsFilterByMusic)
					return new Q(true);
				else
				{
					ArrayList AlMusicTypes = new ArrayList();
					ArrayList AlMusicTypesQ = new ArrayList();
					foreach (MusicType mt in CurrentEvent.MusicTypes)
					{
						if (!AlMusicTypes.Contains(mt.K))
						{
							AlMusicTypesQ.Add(new Q(EventMusicType.Columns.MusicTypeK, mt.K));
							AlMusicTypes.Add(mt.K);
							AddMusicTypeQChildren(AlMusicTypesQ, AlMusicTypes, mt);
							AddMusicTypeQParents(AlMusicTypesQ, AlMusicTypes, mt);
						}
					}
					Or MusicTypesOr = new Or((Q[])AlMusicTypesQ.ToArray(typeof(Q)));
					return MusicTypesOr;
				}
			}
		}
		static void AddMusicTypeQParents(ArrayList alQ, ArrayList al, MusicType mt)
		{
			if (mt.HasParent)
			{
				if (!al.Contains(mt.Parent.K))
				{
					alQ.Add(new Q(EventMusicType.Columns.MusicTypeK, mt.Parent.K));
					al.Add(mt.Parent.K);
					AddMusicTypeQParents(alQ, al, mt.Parent);
				}
			}
		}
		static void AddMusicTypeQChildren(ArrayList alQ, ArrayList al, MusicType mt)
		{
			if (mt.Children.Count > 0)
			{
				foreach (MusicType mtChild in mt.Children)
				{
					if (!al.Contains(mtChild.K))
					{
						alQ.Add(new Q(EventMusicType.Columns.MusicTypeK, mtChild.K));
						al.Add(mtChild.K);
						AddMusicTypeQChildren(alQ, al, mtChild);
					}
				}
			}
		}

		#endregion

		#region AfterPartyPanel
		protected Panel AfterPartyPanel;
		protected DataList AfterPartyDataList;
		public void AfterParty_Load(object o, System.EventArgs e)
		{

			if (CurrentEvent.IsFuture)
			{
				Query q = new Query();
				q.QueryCondition = new And(
					new Q(Event.Columns.DateTime, CurrentEvent.DateTime.AddDays(1)),
					new Q(Venue.Columns.PlaceK, CurrentEvent.Venue.PlaceK),
					new Or(
					new Q(Event.Columns.StartTime, Event.StartTimes.Morning),
					new Q(Event.Columns.StartTime, Event.StartTimes.Daytime)
					)
					);
				q.TableElement = Event.PlaceJoin;
				q.Columns = Templates.Events.EventHomeToday.Columns;
				q.OrderBy = Event.FutureEventOrder;
				EventSet es = new EventSet(q);
				if (es.Count == 0)
					AfterPartyPanel.Visible = false;
				else
				{
					AfterPartyDataList.ItemTemplate = this.LoadTemplate("/Templates/Events/EventHomeToday.ascx");
					AfterPartyDataList.DataSource = es;
					AfterPartyDataList.DataBind();
				}
			}
			else
				AfterPartyPanel.Visible = false;
		}

		#endregion
		#region ChangePanel
		void ChangePanel(Panel p)
		{
			if (p.Equals(EventSelectedPanel))
				p.Visible = true;
			else
				EventSelectedPanel.Visible = false;

		}
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
			this.Load += new System.EventHandler(Info_Load);
			this.Load += new System.EventHandler(UsrEventAttendList_Load);
			this.Load += new System.EventHandler(SpotterSignUp_Load);
			this.Load += new System.EventHandler(Misc_Load);
			this.Load += new System.EventHandler(Galleries_Load);
			this.Load += new System.EventHandler(TodayEvents_Load);
			this.Load += new System.EventHandler(AfterParty_Load);

			
			this.PreRender += new System.EventHandler(UsrEventAttendList_PreRender);
			this.PreRender += new System.EventHandler(Info_PreRender);


		}
		#endregion
	}
}
