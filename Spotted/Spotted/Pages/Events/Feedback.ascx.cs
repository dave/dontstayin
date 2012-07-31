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

namespace Spotted.Pages.Events
{
	public partial class Feedback : EventUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{
				if (Usr.Current == null)
				{
					Usr.KickUserIfNotLoggedIn("You must be logged in to give feedback.");
				}
				else if (CurrentUsrTickets == null || CurrentUsrTickets.Count == 0)
				{
                    TicketFeedbackP.InnerHtml = "<h2>You must have bought tickets to give feedback.</h2>";
				}

				// Ticket feedback
				else if (this.ContainerPage.Url["ticketfeedback"].Exists)
				{
					try
					{
						if (Convert.ToBoolean(this.ContainerPage.Url["ticketfeedback"].Value))
							SetTicketFeedback(Ticket.FeedbackEnum.Good);
						else
							SetTicketFeedback(Ticket.FeedbackEnum.Bad);
					}
					catch
					{ }
				}
				else
				{
                    bool flag = false;
                    foreach(Ticket ticket in CurrentUsrTickets)
                    {
                        if (!ticket.Cancelled)
                        {
                            LoadTicketFeedback();
                            flag = true;
                            break;
                        }
                    }
					if (!flag)
					{
						TicketFeedbackP.InnerHtml = "<h2>Your ticket(s) were refunded. You cannot give feedback.</h2>";
						Usr.Current.SetPrefsNextTicketFeedbackDate();
					}
				}
			}
			LoadEventDetails();
			LoadEventBrandGroups();
		}

		#region Methods
		private void SetTicketFeedback(Ticket.FeedbackEnum feedback)
		{
			SetTicketFeedback(feedback, "");
		}
		private void SetTicketFeedback(Ticket.FeedbackEnum feedback, string feedbackNote)
		{
			if (CurrentEvent.DateTime < DateTime.Today && CurrentUsrTickets != null && CurrentUsrTickets.Count > 0)
			{
				if (CurrentUsrTickets != null && CurrentUsrTickets.Count > 0)
				{
					// User cannot change their response once its been set, but can add a feedback note if they havent already
					if (CurrentUsrTickets[0].Feedback == Ticket.FeedbackEnum.None || (CurrentUsrTickets[0].Feedback == feedback && CurrentUsrTickets[0].FeedbackNote.Length == 0 && feedbackNote.Length > 0))
					{
						// If user has not added a feedback note, then update all user's Tickets for this Event
						foreach (Ticket ticket in CurrentUsrTickets)
						{
                            if (!ticket.Cancelled)
                            {
                                ticket.Feedback = feedback;
                                ticket.FeedbackNote = Cambro.Misc.Utility.Snip(Cambro.Web.Helpers.StripHtml(feedbackNote), 4000);
                                ticket.Update();
                                Usr.Current.SetPrefsNextTicketFeedbackDate();
                                this.ContainerPage.ShowTicketFeedbackToDo();
                            }
						}
					}
				}
			}
			LoadTicketFeedback();			
		}

		private void LoadEventDetails()
		{
			List<Event> events = new List<Event>();
			events.Add(CurrentEvent);
			TicketEventDataList.ItemTemplate = this.LoadTemplate("/Templates/Events/UsrPageAttendedList.ascx");
			TicketEventDataList.DataSource = events;
			TicketEventDataList.DataBind();
		}

		private void LoadTicketFeedback()
		{
			this.UsrTicketResponseGoodLinkButtonDiv.Visible = false;
			this.UsrTicketResponseGoodDiv.Visible = false;
			this.UsrTicketResponseBadLinkButtonDiv.Visible = false;
			this.UsrTicketResponseBadDiv.Visible = false;
			this.UsrTicketFeedbackTextDiv.Visible = false;
			//this.JoinBrandRegularsGroup.Visible = false;

			if (CurrentEvent.DateTime < DateTime.Today && CurrentUsrTickets != null && CurrentUsrTickets.Count > 0)
			{
				TicketFeedbackP.Visible = true;
				//CardReminderP.Visible = false;
				
				// Since all tickets should have the same feedback, use the first one to get feedback details.
				if (CurrentUsrTickets[0].Feedback == Ticket.FeedbackEnum.Good)
				{
					this.UsrTicketResponseGoodDiv.Visible = true;
					this.UsrTicketFeedbackHeaderLabel.Visible = false;
					this.SuccessfulTicketEventPanel.Visible = true;
					//LoadEventBrandGroups();
				}
				else if (CurrentUsrTickets[0].Feedback == Ticket.FeedbackEnum.Bad)
				{
					this.UsrTicketResponseBadDiv.Visible = true;
					this.UsrTicketFeedbackTextDiv.Visible = true;
					this.UsrTicketFeedbackLabel.Text = CurrentUsrTickets[0].FeedbackNote;
					this.UsrTicketFeedbackTextBox.Text = CurrentUsrTickets[0].FeedbackNote;

					this.UsrTicketFeedbackHeaderLabel.Visible = CurrentUsrTickets[0].FeedbackNote.Length == 0;
					this.UsrTicketFeedbackLabel.Visible = CurrentUsrTickets[0].FeedbackNote.Length > 0;
					this.UsrTicketFeedbackTextBox.Visible = CurrentUsrTickets[0].FeedbackNote.Length == 0;
					this.UsrTicketFeedbackTextSubmitButton.Visible = CurrentUsrTickets[0].FeedbackNote.Length == 0;
				}
				else
				{
					this.UsrTicketResponseGoodLinkButtonDiv.Visible = true;
					this.UsrTicketResponseBadLinkButtonDiv.Visible = true;
				}
			}
			else
			{
				Response.Redirect(CurrentEvent.Url());
				//TicketFeedbackP.Visible = false;
			}
		}

		public void LoadEventBrandGroups()
		{
			List<Group> brandGroups = new List<Group>();
			foreach (Brand brand in CurrentEvent.Brands)
			{
				if (brand.Group != null)
				{
					//if(!brand.Group.IsMember(Usr.Current))
					brandGroups.Add(brand.Group);
				}
			}

			JoinBrandRegularsGroup.Visible = brandGroups.Count > 0;

			if (brandGroups.Count > 0)
			{
				BrandGroupRepeater.DataSource = brandGroups;
				BrandGroupRepeater.DataBind();
			}
		}
		#endregion

		#region Page Event Handlers
		protected void UsrTicketResponseGoodLinkButton_Click(object sender, EventArgs e)
		{
			SetTicketFeedback(Ticket.FeedbackEnum.Good, "");
		}

		protected void UsrTicketResponseBadLinkButton_Click(object sender, EventArgs e)
		{
			SetTicketFeedback(Ticket.FeedbackEnum.Bad, "");
		}

		public void UsrTicketFeedbackTextSubmitButton_Click(object sender, EventArgs e)
		{
			SetTicketFeedback(Ticket.FeedbackEnum.Bad, this.UsrTicketFeedbackTextBox.Text);
		}		

		#endregion

		#region BrandGroupRepeater Event Handlers
		protected void BrandGroupRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			// Execute the following logic for Items and Alternating Items.
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				PlaceHolder brandGroupJoinPlaceHolder = (PlaceHolder)e.Item.FindControl("BrandGroupJoinPlaceHolder");
				Spotted.Controls.GroupJoin groupJoinControl = (Spotted.Controls.GroupJoin)this.LoadControl("~/Controls/GroupJoin.ascx");
				groupJoinControl.CurrentGroup = (Group)e.Item.DataItem;
				groupJoinControl.IsInList = true;
				brandGroupJoinPlaceHolder.Controls.Add(groupJoinControl);
			}
		}
		#endregion
	}
}
