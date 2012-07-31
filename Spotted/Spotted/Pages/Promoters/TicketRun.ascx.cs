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

namespace Spotted.Pages.Promoters
{
	public partial class TicketRun : PromoterUserControl
	{

		#region Variables and Constants
		private const decimal MINIMUM_TICKET_PRICE = 1.0m;
        public const string NO_TICKET_SALES_FOR_NONUK_PROMOTERS = "Ticket sales are not currently available to promoters not based in the UK.";

		Bobs.TicketRun CurrentTicketRun = new Bobs.TicketRun();
		bool EditMode = true;
		bool EventSpecific = false;
		int EventK = 0;
        bool AdvancedOptions = false;
		ReferringPageType referringPage = ReferringPageType.PromoterAllTicketRuns;
		#endregion

		#region Enums
		public enum ReferringPageType
		{
			EventOptions = 1,
			Promoter = 2,
			PromoterAllTicketRuns = 3
		}
		#endregion

		#region Page_Init
		protected override void Page_Init(object sender, System.EventArgs e)
		{
			base.Page_Init(sender, e);
		}
		#endregion

		#region Page_Load
		protected void Page_Load(object sender, EventArgs e)
		{	
			this.ContactEmailRegularExpressionValidator.ValidationExpression = Cambro.Misc.RegEx.Email;
			this.TicketPriceCustomValidator.ErrorMessage = "Ticket price must be at least " + MINIMUM_TICKET_PRICE.ToString("c"); 
			this.NonEventSpecificDiv.Visible = false;
			this.JavascriptLabel.Visible = false;
            this.TicketRunDefaultNoteLabel.Visible = false;

            if (CurrentPromoter.AddressCountryK != 224 || (CurrentPromoter.VatCountryK != 224 && CurrentPromoter.VatCountryK != 0))
            {

                ErrorMessageP.Visible = true;
                this.AddEditTicketRunPanel.Visible = false;
                return;
            }
			if (CurrentTicketRun.PromoterK <= 0)
				CurrentTicketRun.PromoterK = CurrentPromoter.K;

            ShowHideAdvancedOptions();
			if (!ContainerPage.Url["ReferringPage"].IsNull && ContainerPage.Url["ReferringPage"].IsInt)
			{
				try
				{
					referringPage = (ReferringPageType)Convert.ToInt32(ContainerPage.Url["ReferringPage"].Value);
				}
				catch { }
			}

			if (!ContainerPage.Url["K"].IsNull && ContainerPage.Url["K"].IsInt)
			{
				try
				{
					CurrentTicketRun = new Bobs.TicketRun(Convert.ToInt32(ContainerPage.Url["K"].Value));
				}
				catch
				{
					throw new DsiUserFriendlyException("Invalid ticket run number");
				}
				
				CurrentTicketRun.CalculateSoldTicketsAndUpdate();
				List<Event> ticketRunEvents = new List<Event>();
				ticketRunEvents.Add(CurrentTicketRun.Event);
				SetupEventDropDownList(ticketRunEvents);
				ProcessSavedTicketRun();
				
				if (!Usr.Current.IsAdmin && CurrentPromoter.K != CurrentTicketRun.PromoterK)
					throw new DsiUserFriendlyException("You cannot edit this ticket run! It does not belong to your promoter account.");
			}
			else if (!ContainerPage.Url["EventK"].IsNull && ContainerPage.Url["EventK"].IsInt)
			{
				try
				{
					EventK = Convert.ToInt32(ContainerPage.Url["EventK"].Value);
					CurrentTicketRun.Event = new Event(EventK);
					if (!this.IsPostBack)
					{
						EventSpecific = true;
						List<Event> ticketRunEvents = new List<Event>();
						ticketRunEvents.Add(CurrentTicketRun.Event);
						SetupEventDropDownList(ticketRunEvents);
						this.NonEventSpecificDiv.Visible = false;
					}
				}
				catch
				{
					throw new DsiUserFriendlyException("Invalid event");
				}
				if (!Usr.Current.IsAdmin && !CurrentTicketRun.Event.IsPromoter(CurrentPromoter.K))
					throw new DsiUserFriendlyException("You cannot edit this ticket run! It does not belong to your promoter account.");
			}
			else 
			{
				this.NonEventSpecificDiv.Visible = EditMode;
				if(!this.IsPostBack)
					SetupPromoterEvents();		
			}

            if (CurrentTicketRun.Event == null && EventDropDownList.Items.Count > 0)
            {
                CurrentTicketRun.Event = new Event(Convert.ToInt32(EventDropDownList.SelectedValue));
            }
            if (CurrentTicketRun.Event != null)
            {
                this.EventLabel.Text = CurrentTicketRun.Event.LinkShortNewWindow(50, true);
            }

			if (!this.IsPostBack)
			{
				ViewState["DuplicateGuid"] = Guid.NewGuid();
				if (CurrentTicketRun.K == 0)
				{
					this.LoadScreenFromEvent();
					ShowHideControlsForTicketRun();
				}
			}

            if(CurrentTicketRun.K > 0)
                ((Spotted.Master.DsiPage)this.Page).SetPageTitle("Edit Ticket Run");
            else
                ((Spotted.Master.DsiPage)this.Page).SetPageTitle("Add Ticket Run");
		}
		#endregion

		#region ViewState
		#region SaveViewState()
		protected override object SaveViewState()
		{
			this.ViewState["EditMode"] = EditMode;
			this.ViewState["EventK"] = EventK;
			this.ViewState["EventSpecific"] = EventSpecific;
            this.ViewState["AdvancedOptions"] = AdvancedOptions;

			return base.SaveViewState();
		}
		#endregion
		#region LoadViewState()
		protected override void LoadViewState(object savedState)
		{
			base.LoadViewState(savedState);
			if (this.ViewState["EditMode"] != null) EditMode = (bool)this.ViewState["EditMode"];
			if (this.ViewState["EventK"] != null) EventK = (int)this.ViewState["EventK"];
			if (this.ViewState["EventSpecific"] != null) EventSpecific = (bool)this.ViewState["EventSpecific"];
            if (this.ViewState["AdvancedOptions"] != null) AdvancedOptions = (bool)this.ViewState["AdvancedOptions"];
		}
		#endregion
		#endregion

		#region Methods
		#region SetupDropDownLists
		private void SetupPromoterEvents()
		{
			if (CurrentPromoter != null)
			{
				this.NonEventSpecificDiv.Visible = true;

				Query promoterEventsQuery = new Query(new And(new Q(Promoter.Columns.K, CurrentPromoter.K),
															  new Q(Event.Columns.DateTime, QueryOperator.GreaterThanOrEqualTo, DateTime.Today)));
				promoterEventsQuery.Columns = new ColumnSet(Event.Columns.Name, Event.Columns.K, Event.Columns.DateTime);
				promoterEventsQuery.TableElement = Event.PromoterJoinWithVenue;
				promoterEventsQuery.OrderBy = Event.FutureEventOrder;

				promoterEventsQuery.NoLock = true;
				promoterEventsQuery.Distinct = true;
				promoterEventsQuery.DistinctColumn = Event.Columns.K;

				EventSet promoterEvents = new EventSet(promoterEventsQuery);
				this.NoEventsPanel.Visible = promoterEvents.Count == 0;
				this.HasEventsPanel.Visible = !this.NoEventsPanel.Visible;
				this.AddEditTicketRunPanel.Visible = promoterEvents.Count > 0;
				List<Event> promoterEventList = new List<Event>();
				promoterEvents.Reset();
				foreach (Event promoterEvent in promoterEvents)
					promoterEventList.Add(promoterEvent);

				SetupEventDropDownList(promoterEventList);
			}
		}

		private void SetupEventDropDownList(List<Event> promoterEvents)
		{
			this.EventDropDownList.Items.Clear();

			if (promoterEvents.Count > 0)
			{
				if (CurrentTicketRun.Event == null || CurrentTicketRun.EventK != promoterEvents[0].K)
					CurrentTicketRun.Event = new Event(promoterEvents[0].K);
				
				if (promoterEvents.Count == 1)
				{
					this.EventLabel.Visible = true;
					this.EventDropDownList.Items.Add(promoterEvents[0].ToListItem(40, true));
					this.EventLabel.Text = CurrentTicketRun.Event.LinkShortNewWindow(50, true);
				}
				else
				{
					this.EventDropDownList.Visible = true;
					ListItem[] eventListItems = new ListItem[promoterEvents.Count];
					for (int i = 0; i < promoterEvents.Count; i++)
					{
						eventListItems[i] = promoterEvents[i].ToListItem(40, true);
					}

					this.EventDropDownList.Items.AddRange(eventListItems);
				}
			}
		}

		private void SetupBrandsDropDownList()
		{
			if (CurrentTicketRun.Event != null)
			{
                this.BrandsRow.Visible = false;// CurrentTicketRun.Event.Brands.Count > 1;
				if (CurrentTicketRun.Event.Brands.Count > 0)
				{
					// Admins can assign any brand linked to the event
					BrandSet promoterEventBrands = CurrentTicketRun.Event.Brands;
					
					if (!Usr.Current.IsAdmin)
					{
						// Get all brands linked to the event and promoter
						Query q = new Query();
						q.NoLock = true;
						q.QueryCondition = new And(new Q(EventBrand.Columns.EventK, CurrentTicketRun.EventK),
													new Q(Brand.Columns.PromoterK, CurrentPromoter.K));
						q.OrderBy = new OrderBy(Group.Columns.TotalMembers, OrderBy.OrderDirection.Descending);
						q.TableElement = new Join(new Join(Brand.Columns.K, EventBrand.Columns.BrandK), Group.Columns.K, Brand.Columns.GroupK);
						promoterEventBrands = new BrandSet(q);
					}
					this.BrandDropDownList.DataSource = promoterEventBrands;
					this.BrandDropDownList.DataTextField = "Name";
					this.BrandDropDownList.DataValueField = "K";
					this.BrandDropDownList.DataBind();
				}
			}
		}

		private void SetupFollowsTicketRunDropDownList()
		{
			this.FollowsTicketRunRow.Visible = false;

			if (CurrentTicketRun.Event != null && CurrentPromoter != null)
			{
				int currentTicketRunK = 0;
				if(CurrentTicketRun != null)
					currentTicketRunK = CurrentTicketRun.K;

				Query otherTicketRunsQuery = new Query(new And(new Q(Bobs.TicketRun.Columns.EventK, CurrentTicketRun.EventK),
															   new Q(Bobs.TicketRun.Columns.K, QueryOperator.NotEqualTo, currentTicketRunK)));
				otherTicketRunsQuery.Columns = new ColumnSet(Bobs.TicketRun.Columns.K, Bobs.TicketRun.Columns.Price, Bobs.TicketRun.Columns.Name, Bobs.TicketRun.Columns.StartDateTime, Bobs.TicketRun.Columns.EndDateTime);
				otherTicketRunsQuery.OrderBy = new OrderBy(Bobs.TicketRun.Columns.Price);
				TicketRunSet otherTicketRuns = new TicketRunSet(otherTicketRunsQuery);

                this.FollowsTicketRunDropDownList.Items.Clear();
                this.FollowsTicketRunDropDownList.Items.Add(new ListItem("", "0"));

				if (otherTicketRuns.Count > 0)
				{
					this.FollowsTicketRunRow.Visible = true;

					ListItem[] otherTicketRunListItems = new ListItem[otherTicketRuns.Count];
					for (int i = 0; i < otherTicketRuns.Count; i++)
					{
						otherTicketRunListItems[i] = otherTicketRuns[i].ToListItem(30, true);
					}

					this.FollowsTicketRunDropDownList.Items.AddRange(otherTicketRunListItems);
				}
			}
		}

		#endregion

		#region ShowHideControls
		private void ShowHideControlsForTicketRun()
		{
			this.EventDropDownList.Visible = EditMode && !AdvancedOptions;
			//this.AdvOptionsCheckRow.Visible = EditMode;
			this.TicketPriceTextBox.Visible = EditMode;
			this.PriceAndBookingFeeLabel.Visible = !EditMode;
			this.TicketDescriptionTextBox.Visible = EditMode && (CurrentTicketRun.IsUpdateable && CurrentTicketRun.SoldTickets == 0);
			this.TicketDescriptionHelperLabel.Visible = EditMode && (CurrentTicketRun.IsUpdateable && CurrentTicketRun.SoldTickets == 0);
			this.TicketNameHelperLabel.Visible = EditMode && (CurrentTicketRun.IsUpdateable && CurrentTicketRun.SoldTickets == 0);
			this.TicketDescriptionLabel.Visible = !(EditMode && (CurrentTicketRun.IsUpdateable && CurrentTicketRun.SoldTickets == 0));
			this.BrandDropDownList.Visible = EditMode;
			this.BrandLabel.Visible = !EditMode;
			this.FollowsTicketRunDropDownList.Visible = EditMode;
			this.FollowsTicketRunLabel.Visible = !EditMode;
			this.FollowsTicketRunHelperLabel.Visible = EditMode && (CurrentTicketRun.IsUpdateable && CurrentTicketRun.SoldTickets == 0);
			this.StartTicketRunTable.Visible = EditMode;
			this.StartTicketRunLabel.Visible = !EditMode;
			this.EndTicketRunTable.Visible = EditMode;
			this.EndTicketRunLabel.Visible = !EditMode;
			this.UpdateButtonsRow.Visible = EditMode;
			this.ConfirmationButtonsRow.Visible = !EditMode;
			this.TicketBookingFeeRow.Visible = EditMode && Usr.Current.IsAdmin;
			this.OrderInTheListHelperLabel.Visible = EditMode && CurrentTicketRun.IsUpdateable;

            this.NonEventSpecificDiv.Visible = EditMode && !AdvancedOptions && CurrentTicketRun.K == 0 && !EventSpecific;
			this.EventLabel.Visible = !this.NonEventSpecificDiv.Visible;

            ShowHideAdvancedOptions();

			if (!EditMode)
			{
                //AdvancedOptions = true;
				this.NonEventSpecificDiv.Visible = false;

				if (CurrentTicketRun.FollowsTicketRunK == 0)
					this.FollowsTicketRunRow.Visible = false;

				if (CurrentTicketRun.Event.Brands.Count <= 1)
					this.BrandsRow.Visible = false;

				if (Math.Round(CurrentTicketRun.ListOrder, 2).Equals(1.0))
					this.OrderInTheListRow.Visible = false;

				if (CurrentTicketRun.Name.Trim().Length == 0)
					this.TicketNameRow.Visible = false;

				if (CurrentTicketRun.Description.Trim().Length == 0)
					this.TicketDescriptionRow.Visible = false;
                this.StartDateRow.Visible = true;
                this.EndDateRow.Visible = true;
                this.MaxTicketsRow.Visible = true;
			}
			else if(AdvancedOptions)
			{
				this.FollowsTicketRunRow.Visible = FollowsTicketRunDropDownList.Items.Count > 1 && ((CurrentTicketRun.IsUpdateable && CurrentTicketRun.SoldTickets == 0) || CurrentTicketRun.FollowsTicketRunK > 0);
                this.BrandsRow.Visible = false;//BrandDropDownList.Items.Count > 1 && ((CurrentTicketRun.IsUpdateable && CurrentTicketRun.SoldTickets == 0) || CurrentTicketRun.BrandK > 0);
				this.OrderInTheListRow.Visible = Usr.Current.IsAdmin;
				this.TicketNameRow.Visible = CurrentTicketRun.Name.Trim().Length > 0 || (CurrentTicketRun.IsUpdateable && CurrentTicketRun.SoldTickets == 0);
				this.TicketDescriptionRow.Visible = CurrentTicketRun.Description.Trim().Length > 0 || (CurrentTicketRun.IsUpdateable && CurrentTicketRun.SoldTickets == 0);
			}

			Utilities.EnableDisableControls(this.AddEditTicketRunPanel, EditMode);
			Utilities.EnableDisableChildControls(this.UpdateButtonsRow, true);
			Utilities.EnableDisableChildControls(this.ConfirmationButtonsRow, true);

			// Disabling the setting of Brands as per request by Dave Brophy on May 10, 2007.
			// No need for brand selection, as promoter will include ticket name details and all brands associated with an event will be available to join after positive feedback
			this.BrandsRow.Visible = false;
		}

		private void ProcessSavedTicketRun()
		{
			TicketRunNote.Visible = false;

			if (CurrentTicketRun.K > 0)
			{
                AdvancedOptions = true;
				if(!this.IsPostBack)
					LoadScreenFromTicketRun();
				ShowHideControlsForTicketRun();

				TicketRunNote.Visible = true;
				
				if (CurrentTicketRun.Status == Bobs.TicketRun.TicketRunStatus.Refunded)
				{
					TicketRunNote.Text = "<p>This ticket run has finished and all tickets have been refunded.</p>";
					EditMode = false;
					ShowHideControlsForTicketRun();
					this.UpdateButtonsRow.Visible = false;
					this.ConfirmationButtonsRow.Visible = false;
					RefundButtonRow.Visible = false;
				}
				else if (CurrentTicketRun.Status == Bobs.TicketRun.TicketRunStatus.Ended)
				{
					TicketRunNote.Text = "<p>This ticket run has finished.</p>";
					EditMode = false;
					ShowHideControlsForTicketRun();
					this.UpdateButtonsRow.Visible = false;
					this.ConfirmationButtonsRow.Visible = false;
					if (Usr.Current.IsSuperAdmin && CurrentTicketRun.SoldTickets > CurrentTicketRun.CancelledTicketQuantity)
						RefundButtonRow.Visible = true;
				}
				else if (CurrentTicketRun.SoldTickets > 0)
				{
					TicketRunNote.Text = "<p>This ticket run has already sold some tickets.</p>";
					Utilities.EnableDisableControls(this.TicketPriceTextBox, false);
					Utilities.EnableDisableControls(this.BookingFeeTextBox, false);
					Utilities.EnableDisableControls(this.TicketNameTextBox, false);
					this.TicketBookingFeeRow.Visible = false;
					BrandDropDownList.Visible = false;
					BrandLabel.Visible = true;
					FollowsTicketRunDropDownList.Visible = false;
					FollowsTicketRunLabel.Visible = true;
					StartTicketRunTable.Visible = false;
					StartTicketRunLabel.Visible = true;				
				}
				else if (CurrentTicketRun.LockPrice)
				{
					TicketRunNote.Text = "<p>This ticket run's price has been locked by an administrator.</p>";
					if(!Usr.Current.IsAdmin)
						Utilities.EnableDisableControls(this.TicketPriceTextBox, false);
				}
				else
				{
					TicketRunNote.Visible = false;
					TicketRunNote.Text = "";
				}
			}
		}

		private void ShowHideForConfirmTicketRun()
		{
			this.TicketNameRow.Visible = CurrentTicketRun.Name.Trim().Length > 0;
			this.TicketDescriptionRow.Visible = CurrentTicketRun.Description.Trim().Length > 0;
            this.BrandsRow.Visible = false;//CurrentTicketRun.Event.Brands.Count > 1;
			this.FollowsTicketRunRow.Visible = CurrentTicketRun.FollowsTicketRunK > 0;
			this.OrderInTheListRow.Visible = Math.Round(CurrentTicketRun.ListOrder, 2) != 1.0;
		}

        #region ShowHideAdvancedOptions
        private void ShowHideAdvancedOptions()
        {
            this.TicketBookingFeeRow.Visible = AdvancedOptions && EditMode && Usr.Current.IsAdmin;
            this.FollowsTicketRunRow.Visible = AdvancedOptions || !EditMode;
            this.TicketDescriptionRow.Visible = AdvancedOptions || !EditMode;
            this.TicketNameRow.Visible = AdvancedOptions || !EditMode;
            this.StartDateRow.Visible = AdvancedOptions || !EditMode;
            this.EndDateRow.Visible = AdvancedOptions || !EditMode;
            this.OrderInTheListRow.Visible = (AdvancedOptions || !EditMode) && Usr.Current.IsAdmin;
            this.MaxTicketsRow.Visible = AdvancedOptions || !EditMode;

            //this.BackButton.Visible = !AdvancedOptions;
           
        }
        #endregion

		private void ShowAllRows()
		{
			foreach (HtmlTableRow tr in this.TicketRunTable.Rows)
				tr.Visible = true;
		}

		#endregion

		#region LoadScreen
		private void LoadScreenFromTicketRun()
		{
			if (CurrentTicketRun != null)
			{
				if (CurrentTicketRun.K > 0)
					AddEditTicketRunH1.InnerHtml = "Update ticket run";
				else
					AddEditTicketRunH1.InnerHtml = "Add a ticket run";					

				if (CurrentTicketRun.Event != null)
				{
					this.EventLabel.Text = CurrentTicketRun.Event.LinkShortNewWindow(50, true);

					if (EditMode && (CurrentTicketRun.K > 0 || EventK > 0))
					{
						List<Event> ticketRunEvents = new List<Event>();
						ticketRunEvents.Add(CurrentTicketRun.Event);
						SetupEventDropDownList(ticketRunEvents);					
						//SetupBrandsDropDownList();
						SetupFollowsTicketRunDropDownList();

						this.EventLabel.Visible = true;
						this.EventDropDownList.Visible = false;
					}
				}
				if (Usr.Current.IsAdmin && CurrentTicketRun.LockPrice)
					this.OverrideBookingFeeCheckBox.Checked = true;

				this.TicketPriceTextBox.Text = CurrentTicketRun.Price.ToString("c");
				this.BookingFeeTextBox.Text = CurrentTicketRun.BookingFee.ToString("c");
				this.TicketNameTextBox.Text = CurrentTicketRun.Name;
				this.TicketDescriptionTextBox.Text = CurrentTicketRun.Description;
				this.TicketDescriptionLabel.Text = CurrentTicketRun.Description;
				if (CurrentTicketRun.Brand != null)
				{
					this.BrandDropDownList.SelectedValue = CurrentTicketRun.BrandK.ToString();
					this.BrandLabel.Text = CurrentTicketRun.Brand.Link();
				}
				this.FollowsTicketRunDropDownList.SelectedValue = CurrentTicketRun.FollowsTicketRunK.ToString();
				if (CurrentTicketRun.FollowsTicketRun != null)
				{
					this.FollowsTicketRunLabel.Text = this.CurrentTicketRun.FollowsTicketRun.LinkPriceBrandName;
					this.FollowsTicketRunRow.Visible = true;
				}
				else
					this.FollowsTicketRunRow.Visible = false;
				this.StartTicketRunCal.Date = CurrentTicketRun.StartDateTime;
				this.StartTicketRunTime.Time = CurrentTicketRun.StartDateTime;
				if (CurrentTicketRun.K == 0 && CurrentTicketRun.StartDateTime <= DateTime.Now)
					this.StartTicketRunLabel.Text = "Now";
				else
					this.StartTicketRunLabel.Text = CurrentTicketRun.StartDateTime.ToString("dd/MM/yy HH:mm");
								
				this.EndTicketRunCal.Date = CurrentTicketRun.EndDateTime;
				this.EndTicketRunTime.Time = CurrentTicketRun.EndDateTime;
				this.EndTicketRunLabel.Text = CurrentTicketRun.EndDateTime.ToString("dd/MM/yy HH:mm");
				if (CurrentTicketRun.EndDateTime > DateTime.Now)
					this.EndTicketRunLabel.Text += "&nbsp; We will send you an email at this time. You will need to print the list of names before the event. Make sure you have time!";
				else if (CurrentTicketRun.EndDateTime > DateTime.Now.AddMinutes(-1))
					this.EndTicketRunLabel.Text += "&nbsp; This will end your ticket run. You won't be able to restart it.";
				
				this.TicketsSoldLabel.Text = CurrentTicketRun.SoldTickets.ToString();
				if (CurrentTicketRun.CancelledTicketQuantity > 0)
					this.TicketsSoldLabel.Text += " (" + CurrentTicketRun.CancelledTicketQuantity.ToString() + ")";

				if (CurrentTicketRun.K > 0)
					this.TicketsSoldRow.Visible = true;

				this.MaxTicketsTextBox.Text = CurrentTicketRun.MaxTickets.ToString();

				if (this.ContactEmailTextBox.Text.Trim().Length == 0)
					this.ContactEmailTextBox.Text = GetDefaultContactEmail();

				this.OrderInTheListTextBox.Text = CurrentTicketRun.ListOrder.ToString("0.00");
				this.PriceAndBookingFeeLabel.Text = CurrentTicketRun.Price.ToString("c") + " + booking fee " + CurrentTicketRun.BookingFee.ToString("c");

				if (CurrentTicketRun.K > 0)
				{
					this.StopButton.Visible = true;
					this.PauseResumeButton.Visible = true;

					if (CurrentTicketRun.Paused)
					{
						this.PauseResumeButton.Text = "Resume ticket run";
						this.PauseResumeButton.Attributes.Remove("OnClientClick");
					}
					else
					{
						this.PauseResumeButton.Text = "Pause ticket run";
						this.PauseResumeButton.Attributes.Add("OnClientClick", "return confirm('Are you sure you want to pause this ticket run?')");
					}

					this.GoToConfirmationButton.Text = "Update ticket run";
				}
			}
		}
		
		private void LoadScreenFromEvent()
		{
			if (CurrentTicketRun.K == 0 && CurrentTicketRun.Event != null)
			{
                this.EventLabel.Text = CurrentTicketRun.Event.LinkShortNewWindow(50, true);

				if (EventSpecific && CurrentTicketRun.Event.LatestEndOfTicketRunDateTime <= DateTime.Now && CurrentTicketRun.K == 0)
				{
					EditMode = false;
					this.TicketRunNote.Text = "<p>You can no longer start ticket runs for this event.</p>";
				}
				else
				{
					//this.SetupBrandsDropDownList();
					this.SetupFollowsTicketRunDropDownList();

					this.StartTicketRunCal.Date = DateTime.Now;
					this.StartTicketRunTime.Time = DateTime.Now;
					this.EndTicketRunCal.Date = CurrentTicketRun.Event.DefaultEndOfTicketRunDateTime;
					this.EndTicketRunTime.Time = CurrentTicketRun.Event.DefaultEndOfTicketRunDateTime;

					this.EndDateCustomValidator.ErrorMessage = "End date must not be after " + CurrentTicketRun.Event.LatestEndOfTicketRunDateTime.ToString("dd/MM/yy HH:mm");
					this.MaxTicketsTextBox.Text = CurrentTicketRun.Event.Capacity > 0 ? CurrentTicketRun.Event.Capacity.ToString() : "100";

					if (this.ContactEmailTextBox.Text.Trim().Length == 0)
						this.ContactEmailTextBox.Text = GetDefaultContactEmail();
				}
			}
		}
		#endregion

		#region GetDefaultContactEmail
		private string GetDefaultContactEmail()
		{
			if (CurrentTicketRun.TicketPromoterEvent != null)
				return CurrentTicketRun.TicketPromoterEvent.ContactEmail;

			if (Usr.Current.IsPromoterK(this.CurrentPromoter.K))
				return Usr.Current.Email;
			else if (this.CurrentPromoter.PrimaryUsr != null)
			{
				// For instance when an admin does it on a promoter's behalf
				return this.CurrentPromoter.PrimaryUsr.Email;
			}
			else
				return Usr.Current.Email;					
		}
		#endregion 

		#region LoadTicketRunFromScreen
		private void LoadTicketRunFromScreen()
		{
			CurrentTicketRun.PromoterK = CurrentPromoter.K;
			CurrentTicketRun.EventK = Convert.ToInt32(this.EventDropDownList.SelectedValue);

			if (CurrentTicketRun.SoldTickets == 0)
			{
				if (Usr.Current.IsAdmin)
				{
					CurrentTicketRun.Price = Utilities.ConvertMoneyStringToDecimal(Cambro.Web.Helpers.StripHtml(this.TicketPriceTextBox.Text));

					if (this.OverrideBookingFeeCheckBox.Checked)
						CurrentTicketRun.SetBookingFee(Utilities.ConvertMoneyStringToDecimal(Cambro.Web.Helpers.StripHtml(this.BookingFeeTextBox.Text)));
					else
						CurrentTicketRun.SetBookingFee();

					CurrentTicketRun.LockPrice = this.OverrideBookingFeeCheckBox.Checked;
				}
				else if (!CurrentTicketRun.LockPrice)
				{
					decimal price = Utilities.ConvertMoneyStringToDecimal(Cambro.Web.Helpers.StripHtml(this.TicketPriceTextBox.Text.Trim()));
					if (CurrentTicketRun.Price != price)
					{
						CurrentTicketRun.Price = price;
						CurrentTicketRun.SetBookingFee();
					}
					else
						CurrentTicketRun.Price = price;
				}

                if (AdvancedOptions)
				{
					CurrentTicketRun.Name = Cambro.Web.Helpers.StripHtml(this.TicketNameTextBox.Text.Trim());
					CurrentTicketRun.Description = Cambro.Web.Helpers.StripHtml(this.TicketDescriptionTextBox.Text.Trim());
					// ASP.NET MultiLine TextBox does not support MaxLength, so must truncate it here to prevent DB exception
					if (CurrentTicketRun.Description.Length > 255)
						CurrentTicketRun.Description = CurrentTicketRun.Description.Substring(0, 255);
					if (this.BrandDropDownList.Items.Count > 0)
						CurrentTicketRun.BrandK = Convert.ToInt32(this.BrandDropDownList.SelectedValue);
					if (this.FollowsTicketRunDropDownList.Items.Count > 0 && Convert.ToInt32(this.FollowsTicketRunDropDownList.SelectedValue) > 0)
						CurrentTicketRun.FollowsTicketRun = new Bobs.TicketRun(Convert.ToInt32(this.FollowsTicketRunDropDownList.SelectedValue));
					else
						CurrentTicketRun.FollowsTicketRun = null;
					CurrentTicketRun.StartDateTime = GetStartDateTimeFromScreen();
				}
			}
			
			// When starting a ticket run, you cannot set the start time to the past.
			if (CurrentTicketRun.K == 0 && CurrentTicketRun.StartDateTime <= DateTime.Now)
                // Set it to Now -1 minute (to account for possible server time synchronization deviations).
				CurrentTicketRun.StartDateTime = DateTime.Now.AddMinutes(-1);

			// Only set Advanced Options for new TicketRuns or when Advanced options is checked.
            if (CurrentTicketRun.K == 0 || AdvancedOptions)
			{
				DateTime endDT = GetEndDateTimeFromScreen();

				// If user changes End Date Time to the past, then set it to now to mark the end of the ticket run.
				if (endDT <= DateTime.Now && CurrentTicketRun.EndDateTime != endDT)
				{
                    // Set it to Now -1 minute (to account for possible server time synchronization deviations).
					CurrentTicketRun.EndDateTime = DateTime.Now.AddMinutes(-1);
					this.JavascriptLabel.Text = "<script type=\"text/javascript\">alert('Setting the end date to the past will effectively end the ticket run now. This cannot be undone. Please make sure you want to end this ticket run before saving.');</script>";
					this.JavascriptLabel.Visible = true;
				}
				else
					CurrentTicketRun.EndDateTime = endDT;

				this.TicketsSoldRow.Visible = CurrentTicketRun.K > 0;
				CurrentTicketRun.MaxTickets = Convert.ToInt32(Cambro.Web.Helpers.StripHtml(this.MaxTicketsTextBox.Text.Trim()));
				if (this.OrderInTheListTextBox.Text.Trim().Length > 0)
					CurrentTicketRun.ListOrder = Convert.ToDouble(Cambro.Web.Helpers.StripHtml(this.OrderInTheListTextBox.Text.Trim()));
				else
					CurrentTicketRun.ListOrder = 1.0;
			}

			CurrentTicketRun.DuplicateGuid = (Guid)ViewState["DuplicateGuid"];
		}
		#endregion

        #region GetStartDateTimeFromScreen
        private DateTime GetStartDateTimeFromScreen()
		{
			return new DateTime(StartTicketRunCal.Date.Year, StartTicketRunCal.Date.Month, StartTicketRunCal.Date.Day, StartTicketRunTime.Time.Hour, StartTicketRunTime.Time.Minute, 0);
		}
		#endregion

		#region GetEndDateTimeFromScreen
		private DateTime GetEndDateTimeFromScreen()
		{
			return new DateTime(EndTicketRunCal.Date.Year, EndTicketRunCal.Date.Month, EndTicketRunCal.Date.Day, EndTicketRunTime.Time.Hour, EndTicketRunTime.Time.Minute, 0);
		}
		#endregion

		private void SaveTicketRun()
		{
			bool redirect = true;

			Page.Validate("SaveTicketRun");
			if (Page.IsValid)
			{
				try
				{
					base.VerifyUserPermissions();

					// Do not save for new ticket run when duplicate guid exists in DB
					if (!((CurrentTicketRun.K == 0) && Bobs.TicketRun.DoesDuplicateGuidExistInDb((Guid)this.ViewState["DuplicateGuid"])))
					{
						if (!CurrentTicketRun.IsUpdateable)
						{
							LoadScreenFromTicketRun();
							ErrorMessageCustomValidator.ErrorMessage = "This ticket run is not updateable.";
							ErrorMessageCustomValidator.IsValid = false;
							return;
						}

						LoadTicketRunFromScreen();

                        //try
                        //{
							// UK countryK = 224
							if (CurrentPromoter.AddressCountryK != 224 || (CurrentPromoter.VatCountryK != 224 && CurrentPromoter.VatCountryK != 0))
							{
                                ErrorMessageCustomValidator.ErrorMessage = NO_TICKET_SALES_FOR_NONUK_PROMOTERS;
								ErrorMessageCustomValidator.IsValid = false;
								return;
							}
                        //}
                        //catch
                        //{
                        //    ErrorMessageCustomValidator.ErrorMessage = "Unable to find the country where the venue is located. Ticket sales are not currently available outside the UK.";
                        //    ErrorMessageCustomValidator.IsValid = false;
                        //    return;
                        //}

						if (CurrentTicketRun.IsCircularDependancy())
						{
							LoadScreenFromTicketRun();
							ErrorMessageCustomValidator.ErrorMessage = "Cannot follow ticket run " + CurrentTicketRun.FollowsTicketRun.PriceBrandName + ". This would create a circular dependancy.";
							ErrorMessageCustomValidator.IsValid = false;
							return;
						}

						CurrentTicketRun.Update();

						try
						{
							TicketPromoterEvent tpe = new TicketPromoterEvent(CurrentTicketRun.PromoterK, CurrentTicketRun.EventK);
							if (ContactEmailTextBox.Text.Trim().Length != 0)
							{
								tpe.ContactEmail = ContactEmailTextBox.Text.Trim();
								tpe.Update();
							}
							if (CurrentTicketRun.EndDateTime <= DateTime.Now && !CurrentTicketRun.EmailSent)
							{
								Utilities.EmailTicketRunStatusUpdate(tpe);
								CurrentTicketRun.EmailSent = true;
								CurrentTicketRun.Update();
							}
						}
						catch
						{
							TicketPromoterEvent tpe = new TicketPromoterEvent();
							tpe.PromoterK = CurrentTicketRun.PromoterK;
							tpe.EventK = CurrentTicketRun.EventK;
							if (ContactEmailTextBox.Text.Trim().Length != 0)
								tpe.ContactEmail = ContactEmailTextBox.Text.Trim();

							tpe.Update();

							if (CurrentPromoter == null)
								throw new Exception("SaveTicketRunButton_Click(): CurrentPromoter==null");

							if (!CurrentPromoter.EnableTickets)
							{
								// send out email reminding them to fill out ticket application form
								Utilities.EmailPromoterReminderToSubmitTicketApplicationForm(tpe);
							}
						}

						CurrentTicketRun.Event.UpdateIsTicketsAvailable(false);
						CurrentTicketRun.Event.UpdateHasHighlight(false);
						CurrentTicketRun.Event.Update();
					}
				}
				catch (Exception ex)
				{
					SavingErrorCustomValidator.IsValid = false;
					Utilities.AdminEmailAlert("Exception occurred in TicketRunScreen SaveTicketRun for promoter " + CurrentPromoter.Name + " (" + CurrentPromoter.K.ToString() + ")" + " TicketRunK=" + CurrentTicketRun.K.ToString(),
												"Exception occurred in TicketRunScreen SaveTicketRun for promoter " + CurrentPromoter.Name + " (" + CurrentPromoter.K.ToString() + ")" + " TicketRunK=" + CurrentTicketRun.K.ToString(), ex, CurrentTicketRun);
				}
				if (redirect)
				{
					// Redirect to page user was on before entering this page
					RedirectToReferringPage();
				}
			}
		}

		private void RedirectToReferringPage()
		{
			if (CurrentTicketRun != null)
			{
				if (referringPage.Equals(ReferringPageType.EventOptions) && CurrentTicketRun.Event != null && CurrentTicketRun.Promoter != null)
				{
                    Response.Redirect(CurrentTicketRun.Promoter.UrlEventOptions(CurrentTicketRun.Event));
				}
				else if (referringPage.Equals(ReferringPageType.Promoter) && CurrentTicketRun.Promoter != null)
				{
                    Response.Redirect(CurrentTicketRun.Promoter.Url() + "#MoreOptions");
				}
				else if (CurrentTicketRun.Promoter != null)
				{
					Response.Redirect(CurrentTicketRun.Promoter.UrlApp("allticketruns", null));
				}
			}
			Response.Redirect(CurrentPromoter.Url());			
		}
		#endregion

		#region Page Events
		#region EventDropDownList_SelectedIndexChanged
		protected void EventDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
			CurrentTicketRun.Event = new Event(Convert.ToInt32(EventDropDownList.SelectedValue));
			LoadScreenFromEvent();
		}
		#endregion

		#region GoToConfirmationButton_Click
		protected void GoToConfirmationButton_Click(object sender, EventArgs eventArgs)
		{
			if (CurrentTicketRun != null && CurrentTicketRun.K > 0)
				SaveTicketRun();

			else
			{
                if(!AdvancedOptions)
                    LoadScreenFromEvent();
                Page.Validate("SaveTicketRun");
				if (Page.IsValid)
				{
                    LoadTicketRunFromScreen();
					LoadScreenFromTicketRun();
					if (CurrentTicketRun.IsCircularDependancy())
					{
						ErrorMessageCustomValidator.ErrorMessage = "Cannot follow ticket run " + CurrentTicketRun.FollowsTicketRun.PriceBrandName + ". This would create a circular dependancy.";
						ErrorMessageCustomValidator.IsValid = false;
						return;
					}
					EditMode = false;
					ShowHideControlsForTicketRun();
					ShowHideForConfirmTicketRun();

                    this.TicketRunDefaultNoteLabel.Visible = !AdvancedOptions;
				}
			}
		}
		#endregion

		#region BackButton_Click
		protected void BackButton_Click(object sender, EventArgs eventArgs)
		{
            //this.AdvancedOptions = false;
			this.EditMode = true;
			this.ShowHideControlsForTicketRun();
			if (CurrentTicketRun.K > 0)
				ProcessSavedTicketRun();
		}
		#endregion

		#region SaveTicketRunButton_Click
		protected void SaveTicketRunButton_Click(object sender, EventArgs eventArgs)
		{
			SaveTicketRun();
		}
		#endregion

		#region PauseResumeButton_Click
		protected void PauseResumeButton_Click(object sender, EventArgs eventArgs)
		{
			CurrentTicketRun.Paused = !CurrentTicketRun.Paused;
			//EditMode = !CurrentTicketRun.Paused;
			CurrentTicketRun.Update();
			//LoadScreenFromTicketRun();

			// Redirect to page user was on before entering this page
			RedirectToReferringPage();
		}
		#endregion

		#region StopButton_Click
		protected void StopButton_Click(object sender, EventArgs eventArgs)
		{
			CurrentTicketRun.EndDateTime = DateTime.Now;
//			EditMode = false;
			CurrentTicketRun.Update();

			// Redirect to page user was on before entering this page
			RedirectToReferringPage();
		}
		#endregion

		#region RefundButton_Click
		protected void RefundButton_Click(object sender, EventArgs eventArgs)
		{
			if (Usr.Current.IsSuperAdmin && CurrentTicketRun.Status == Bobs.TicketRun.TicketRunStatus.Ended)
			{
				if (CurrentTicketRun.K > 0)
				{
					Ticket.Refund(Usr.Current, CurrentTicketRun.Tickets);
					ProcessSavedTicketRun();
				}
			}
		}
		#endregion

        #region AdvancedOptionsButton_Click
        protected void AdvancedOptionsButton_Click(object sender, EventArgs eventArgs)
        {
            this.AdvancedOptions = true;
            this.EditMode = true;
            this.ShowHideControlsForTicketRun();
            if (CurrentTicketRun.K > 0)
                ProcessSavedTicketRun(); 
        }
        #endregion
		#endregion

		#region Custom Validators
		#region MoneyTextBoxVal
		public void MoneyTextBoxVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = Utilities.IsPositiveMoneyText(e.Value, false);
			if (e.IsValid)
				e.IsValid = Utilities.ConvertMoneyStringToDecimal(e.Value) >= MINIMUM_TICKET_PRICE;
		}
		#endregion
		#region DescriptionTextBoxVal
		public void DescriptionTextBoxVal(object o, ServerValidateEventArgs e)
		{
			this.TicketDescriptionTextBox.Text = Cambro.Web.Helpers.StripHtml(e.Value.Trim());
			e.IsValid = this.TicketDescriptionTextBox.Text.Length <= 255;
		}
		#endregion		
		#region StartDateVal
		public void StartDateVal(object o, ServerValidateEventArgs e)
		{
			DateTime startDate = GetStartDateTimeFromScreen();
			DateTime endDate = GetEndDateTimeFromScreen();

			e.IsValid = startDate < endDate;
		}
		#endregion
		#region EndDateVal
		public void EndDateVal(object o, ServerValidateEventArgs e)
		{
			DateTime startDate = GetStartDateTimeFromScreen();
			DateTime endDate = GetEndDateTimeFromScreen();

            if (CurrentTicketRun != null && CurrentTicketRun.Event != null)
            {
                if (endDate > CurrentTicketRun.Event.LatestEndOfTicketRunDateTime)
                {
                    this.EndDateCustomValidator.ErrorMessage = "End date must be before " + CurrentTicketRun.Event.LatestEndOfTicketRunDateTime.ToString("dd/MM/yy HH:mm");
                    e.IsValid = false;
                }
                else if (endDate <= DateTime.Now && CurrentTicketRun.K == 0)
                {
                    this.EndDateCustomValidator.ErrorMessage = "End date cannot be in the past.";
                    e.IsValid = false;
                }
            }
		}
		#endregion
		#region CircularTicketRunDependencyVal
		public void CircularTicketRunDependencyVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = true;
		}
		#endregion
		#region MaxTicketsVal
		public void MaxTicketsVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = CurrentTicketRun.K == 0 || Convert.ToInt32(e.Value.Trim()) >= CurrentTicketRun.SoldTickets;
			if (!e.IsValid && CurrentTicketRun != null)
				this.TicketsSoldLabel.Text = CurrentTicketRun.SoldTickets.ToString();
		}
		#endregion	
		
		#endregion
	}
}
