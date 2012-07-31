using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Bobs;
using Cambro.Web.DbCombo;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Spotted.Pages.Events
{

	public partial class Edit : DsiUserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn();
			this.PanelDateLoad(sender, e);
			this.PanelConflictLoad(sender, e);
			this.PanelDetailsLoad(sender, e);
			this.PanelMusicTypesLoad(sender, e);
			this.PanelPicLoad(sender, e);

			int i = uiBrandsMultiSelector.Count;//need this to persist the brands? (Bug)


			if (!Page.IsPostBack)
			{
				#region Select initial panel
				if (IsNew)
					ChangePanel(PanelDate);
				else if (IsEdit)
				{
					if (Usr.Current.CanEdit(CurrentEvent))
					{
						if (ContainerPage.Url["Page"].Equals("Pic"))
						{
							ChangePanel(PanelPic);
							PanelPicLoad(null, null);
							PanelPicBind();
						}
						else if (ContainerPage.Url["Page"].Equals("Details"))
							ChangePanel(PanelDetails);
						else if (ContainerPage.Url["Page"].Equals("MusicType"))
							ChangePanel(PanelMusicTypes);
						else if (ContainerPage.Url["Page"].Equals("Date"))
							ChangePanel(PanelDate);
						else if (CanEditDateVenue)
							ChangePanel(PanelDate);
						else
							ChangePanel(PanelDetails);
					}
					else
						ChangePanel(PanelEditError);
				}
				else
				{
					ChangePanel(PanelSelectionError);
				}
				#endregion
			}
			if (IsEdit)
			{
				HeaderH1.InnerText = "Edit event - " + CurrentEvent.Name;
				ContainerPage.SetPageTitle("Edit event - " + CurrentEvent.Name);
			}
			else
			{
				HeaderH1.InnerText = "Add event";
				ContainerPage.SetPageTitle("Add event");
			}
			if (Request.Form["__EVENTTARGET"] != null && Request.Form["__EVENTTARGET"].Equals("PanelBrandDbComboNewBrandPostback"))
			{
				Brand newBrand = new Brand(int.Parse(Request.Form["__EVENTARGUMENT"]));
				uiBrandsMultiSelector.Add(newBrand.Name, newBrand.K.ToString());

			}
			this.DataBind();
		}

		public void Page_PreRender(object o, System.EventArgs e)
		{
			ContainerPage.ViewStatePublic["EventDuplicateGuid"] = Guid.NewGuid();
		}

		public bool CanEditDateVenue
		{
			get
			{
				if (!canEditDateVenue.HasValue)
				{
					if (!IsEdit || CurrentEvent == null)
						canEditDateVenue = true;
					else if (Usr.Current != null && !Usr.Current.IsAdmin && CurrentEvent.TicketRuns.Count > 0)
						canEditDateVenue = false;
					else if (Usr.Current != null && Usr.Current.IsJunior)
						canEditDateVenue = true;
					else
						canEditDateVenue = CurrentEvent.AddedDateTime.AddDays(7) > DateTime.Now;
				}
				return (bool)canEditDateVenue;
			}
		}
		private bool? canEditDateVenue;

		#region PanelSelectionError
		public void PanelSelectionErrorNext(object o, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				string urlPart = "";
				if (ContainerPage.Url["SignUp"] == 1)
					urlPart = "/signup-1";
				if (ContainerPage.Url["promoterk"].IsInt)
					urlPart += "/promoterk-" + ContainerPage.Url["promoterk"];
				Response.Redirect("/pages/events/edit/venuek-" + PanelSelectionErrorPicker.Venue.K.ToString() + urlPart);
			}
		}
		#endregion

		public void CapsVal(TextBox tb, ServerValidateEventArgs e)
		{
			string textOnly = Cambro.Web.Helpers.StripHtmlDoubleSpacesLineFeeds(tb.Text);

			int lower = new Regex("[^a-z]").Replace(textOnly, String.Empty).Length;
			int upper = new Regex("[^A-Z]").Replace(textOnly, String.Empty).Length;
			int total = lower + upper;

			if ((total > 20 && upper > 0.4 * total) ||
				(total > 10 && upper > 0.5 * total))
			{
				e.IsValid = false;
				tb.Text = tb.Text.ToLower();
			}
			else
				e.IsValid = true;

		}
		public void PunctuationVal(TextBox tb, ServerValidateEventArgs e)
		{
			string textOnly = Cambro.Web.Helpers.StripHtmlDoubleSpacesLineFeeds(tb.Text);

			string withoutSpaces = new Regex("[ ]").Replace(textOnly, String.Empty);

			int chars = new Regex("[^a-zA-Z]").Replace(withoutSpaces, String.Empty).Length;
			int punc = new Regex("[a-zA-Z]").Replace(withoutSpaces, String.Empty).Length;
			int total = chars + punc;

			if ((total > 20 && punc > 0.2 * total) ||
				(total > 10 && punc > 0.3 * total))
				e.IsValid = false;
			else
				e.IsValid = true;

		}

		#region EventNameCapsVal
		public void EventNameCapsVal(object o, ServerValidateEventArgs e)
		{
			CapsVal(EventName, e);
		}
		#endregion
		#region EventShortDetailsCapsVal
		public void EventShortDetailsCapsVal(object o, ServerValidateEventArgs e)
		{
			CapsVal(EventShortDetailsHtml, e);
		}
		#endregion
		#region EventNamePunctuationVal
		public void EventNamePunctuationVal(object o, ServerValidateEventArgs e)
		{
			PunctuationVal(EventName, e);
		}
		#endregion
		#region EventShortDetailsPunctuationVal
		public void EventShortDetailsPunctuationVal(object o, ServerValidateEventArgs e)
		{
			PunctuationVal(EventShortDetailsHtml, e);
		}
		#endregion


		#region PanelDate
		void PanelDateLoad(object o, EventArgs e)
		{
			PanelDateSaveButtonP.Visible = IsEdit && CanEditDateVenue;

			if (CurrentEvent != null)
			{
				PanelDateLine1P.Visible = IsEdit && CanEditDateVenue;
				PanelDateCalendarP.Visible = IsEdit && CanEditDateVenue;
				PanelDateNextButtonP.Visible = IsEdit && CanEditDateVenue;

				if (IsEdit && CanEditDateVenue && !Page.IsPostBack)
				{
					PanelDateCal.SelectedDate = CurrentEvent.DateTime;

					if (Usr.Current.IsAdmin && CurrentEvent.TicketRuns.Count > 0)
					{
						PanelDateMessage.Visible = true;
						PanelDateMessage.Text = "Warning! This event is selling tickets. Please be absolutely sure before changing the date!";
					}
				}
				else
				{
					PanelDateMessage.Visible = true;
					PanelDateMessage.Text = "You are not allowed to edit this event's date. Please contact an admin to change the date.";
					if (CurrentEvent != null && CurrentEvent.TicketRuns.Count > 0)
						PanelDateMessage.Text += " This event is selling tickets.";
				}
			}
		}
		public void PanelDateUpdateFutureConfirm(object o, System.EventArgs e)
		{
			if (PanelDateCal.SelectedDate < new DateTime(1995, 1, 1))
			{
				PanelDateNonSelectedError.Visible = true;
			}
			else
			{
				PanelDateNonSelectedError.Visible = false;
				if (PanelDateCal.SelectedDate < DateTime.Now.Date && !PanelDateFutureConfirmCheckBox.Checked && (IsNew || (IsEdit && CurrentEvent != null && CurrentEvent.DateTime >= DateTime.Today)))
					PanelDateFutureConfirmPanel.Visible = true;
				else
					PanelDateFutureConfirmPanel.Visible = false;
			}
		}
		public void PanelDateSave(object o, System.EventArgs e)
		{
			PanelDateVerify(true);
		}
		protected void PanelDateNext(object o, EventArgs e)
		{
			PanelDateVerify(false);
		}
		void PanelDateVerify(bool saveNow)
		{
			if (PanelDateCal.SelectedDate < new DateTime(1995, 1, 1))
			{
				PanelDateNonSelectedError.Visible = true;
			}
			else
			{
				PanelDateNonSelectedError.Visible = false;
				if (PanelDateCal.SelectedDate < DateTime.Now.Date && !PanelDateFutureConfirmCheckBox.Checked && (IsNew || (IsEdit && CurrentEvent != null && CurrentEvent.DateTime >= DateTime.Today)))
				{
					PanelDateFutureConfirmPanel.Visible = true;
				}
				else
				{
					if (ConflictingEvents.Count == 0)
					{
						if (saveNow && CanEditDateVenue)
						{
							SaveEvent();
							SavedRedirect();
						}
						else
							ChangePanel(PanelDetails);
					}
					else
						ChangePanel(PanelConflict);
				}
			}
		}
		#region ConflictingEvents
		public EventSet ConflictingEvents
		{
			get
			{
				if (conflictingEvents == null && PanelDateCal.SelectedDate > (new DateTime(1995, 1, 1)))
				{
					Q editQ = new Q(true);
					if (IsEdit)
						editQ = new Q(Event.Columns.K, QueryOperator.NotEqualTo, CurrentEvent.K);
					conflictingEvents = new EventSet(new Query(new And(new Q(Event.Columns.VenueK, CurrentVenue.K), new Q(Event.Columns.DateTime, PanelDateCal.SelectedDate), editQ)));
				}
				return conflictingEvents;
			}
		}
		EventSet conflictingEvents;
		#endregion

		#endregion
		#region PanelConflict
		void PanelConflictLoad(object o, EventArgs e)
		{
			if (ConflictingEvents != null && ConflictingEvents.Count > 0)
			{
				PanelConflictDataGrid.DataSource = ConflictingEvents;
				PanelConflictDataGrid.DataBind();
			}
		}
		public void PanelConflictVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = PanelConflictCheckbox.Checked;
		}
		protected void PanelConflictBack(object o, EventArgs e)
		{
			ChangePanel(PanelDate);
		}
		protected void PanelConflictNext(object o, EventArgs e)
		{
			if (Page.IsValid)
			{
				ChangePanel(PanelDetails);
			}
		}
		#endregion
		#region PanelDetails
		protected Panel SpotterRequestPanel;
		protected TextBox SpotterRequestName, SpotterRequestNumber;
		protected RadioButton SpotterRequestYes, SpotterRequestNo;
		void PanelDetailsLoad(object o, EventArgs e)
		{
			PanelDetailsSaveP.Visible = IsEdit;
			if (IsEdit && !Page.IsPostBack && CurrentEvent != null)
			{
				this.PanelDetailsVenuePicker.Venue = CurrentEvent.Venue;
				//this.uiVenueAutoComplete.Text = CurrentEvent.Venue.Name + " in " + CurrentEvent.Venue.Place.Name;
				EventName.Text = CurrentEvent.Name;
				EventShortDetailsHtml.Text = CurrentEvent.ShortDetailsHtml;
				EventLongDetailsHtml.LoadHtml(CurrentEvent.LongDetailsHtml);
				if (CurrentEvent.Capacity > 0)
					EventCapacity.Text = CurrentEvent.Capacity.ToString();
				StartTimeEvening.Checked = CurrentEvent.StartTime.Equals(Event.StartTimes.Evening);
				StartTimeMorning.Checked = CurrentEvent.StartTime.Equals(Event.StartTimes.Morning);
				StartTimeDaytime.Checked = CurrentEvent.StartTime.Equals(Event.StartTimes.Daytime);
				DetailsDateLockedLabel.Text = CurrentEvent.FriendlyDate(true);

				DetailsVenueLockedLabel.Text = CurrentEvent.Venue.FriendlyName;
			}
			if (IsNew && !Page.IsPostBack)
			{
				if (CurrentVenue.Capacity > 0)
					EventCapacity.Text = CurrentVenue.Capacity.ToString();
			}

			SpotterRequestPanel.Visible = Usr.Current.IsPromoter;
			if (!Page.IsPostBack)
			{
				if (IsEdit)
				{
					SpotterRequestYes.Checked = CurrentEvent.SpotterRequest.HasValue && CurrentEvent.SpotterRequest.Value;
					SpotterRequestNo.Checked = CurrentEvent.SpotterRequest.HasValue && !CurrentEvent.SpotterRequest.Value;
				}

				if (IsEdit && CurrentEvent.SpotterRequestName != null && CurrentEvent.SpotterRequestName.Length > 0 && CurrentEvent.SpotterRequestNumber != null && CurrentEvent.SpotterRequestNumber.Length > 0)
				{
					SpotterRequestName.Text = CurrentEvent.SpotterRequestName;
					SpotterRequestNumber.Text = CurrentEvent.SpotterRequestNumber;
				}
				else
				{
					SpotterRequestName.Text = Usr.Current.FullName;
					SpotterRequestNumber.Text = Usr.Current.MobileDial;
				}
			}

			PanelDetailsVenueDiv.Visible = IsEdit && CanEditDateVenue;
			PanelDetailsVenueLockedDiv.Visible = IsEdit && !CanEditDateVenue;
			DateLockedDiv.Visible = IsEdit && !CanEditDateVenue;
			if (IsEdit && !CanEditDateVenue && CurrentEvent != null && CurrentEvent.TicketRuns.Count > 0)
			{
				DetailsDateLockedLabel2.Text = "<small>This is now locked - it cannot be changed because this event is selling tickets. Please contact an administrator to change.</small>";
			}
			else
			{
				DetailsDateLockedLabel2.Text = "<small>This is now locked - to change it, contact one of our <a href=\"/pages/contact\">event moderators</a>.</small>";
			}
			DetailsVenueLockedLabel2.Text = DetailsDateLockedLabel2.Text;

			if (Usr.Current.IsAdmin && CurrentEvent != null && CurrentEvent.TicketRuns.Count > 0)
			{
				PanelDetailsVenueWarningP.Visible = true;
				PanelDetailsVenueWarningP.InnerHtml = "Warning! This event is selling tickets. Please be absolutely sure before changing the venue!";
			}
			if (IsEdit && !Page.IsPostBack && CurrentEvent != null)
			{
				foreach (Brand b in CurrentEvent.Brands)
				{
					this.uiBrandsMultiSelector.Add(b.Name, b.K.ToString());
					this.uiHasBrandsRadioButton.Checked = true;
				}
			}
			if (!IsNew && !this.uiHasBrandsRadioButton.Checked)
			{
				this.uiNoBrandsRadioButton.Checked = true;
			}


		}
		protected void SpotterRequestVal(object o, ServerValidateEventArgs e)
		{
			if (Usr.Current.IsPromoter)
			{

				if (!SpotterRequestYes.Checked && !SpotterRequestNo.Checked)
				{
					e.IsValid = false;
					return;
				}

				if (SpotterRequestYes.Checked && (SpotterRequestName.Text.Length == 0 || SpotterRequestNo.Text.Length == 0))
				{
					e.IsValid = false;
					return;
				}

			}
			else
			{
				e.IsValid = true;
			}
		}
		protected void PanelDetailsBack(object o, EventArgs e)
		{
			ChangePanel(PanelDate);
		}
		protected void PanelDetailsNext(object o, EventArgs e)
		{
			if (Page.IsValid)
			{
				ChangePanel(PanelMusicTypes);
			}
			else
				ContainerPage.AnchorSkip("SaveButton");
		}
		public void PanelDetailsSave(object o, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				SaveEvent();
				SavedRedirect();
			}
			else
				ContainerPage.AnchorSkip("SaveButton");
		}
		Event.StartTimes StartTime
		{
			get
			{
				if (StartTimeMorning.Checked)
					return Event.StartTimes.Morning;
				else if (StartTimeDaytime.Checked)
					return Event.StartTimes.Daytime;
				else
					return Event.StartTimes.Evening;
			}
		}

		#endregion
		#region PanelMusicTypes
		void PanelMusicTypesLoad(object o, EventArgs e)
		{
			PanelMusicTypesSaveP.Visible = IsEdit;
			if (IsEdit && !Page.IsPostBack)
				MusicTypesUc.InitialMusicTypes = CurrentEvent.MusicTypes;
		}
		protected void PanelMusicTypesBack(object o, EventArgs e)
		{
			ChangePanel(PanelDetails);
		}
		protected void PanelMusicTypesNext(object o, EventArgs e)
		{
			if (Page.IsValid)
			{
				SaveEvent();
				ChangePanel(PanelPic);
				PanelPicLoad(null, null);
				PanelPicBind();
			}
		}
		public void PanelMusicTypesSave(object o, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				SaveEvent();
				SavedRedirect();
			}
		}
		public void PanelMusicTypeVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = MusicTypesUc.SelectedMusicTypes.Count > 0;
			if (!e.IsValid)
				ContainerPage.AnchorSkip("PanelMusicTypeValError");
		}
		#endregion
		#region SaveEvent()
		void SaveEvent()
		{
			bool duplicate = false;
			//Save the event
			if (IsNew)
			{
				//Duplicate?
				EventSet esDup = new EventSet(new Query(new Q(Event.Columns.DuplicateGuid, (Guid)ContainerPage.ViewStatePublic["EventDuplicateGuid"])));
				if (esDup.Count == 0)
				{
					Event ev = Event.AddEvent(
						Cambro.Web.Helpers.StripHtml(EventName.Text).Trim(),
						CurrentVenue.K,
						StartTime,
						PanelDateCal.SelectedDate,
						Cambro.Misc.Utility.Snip(Cambro.Web.Helpers.StripHtml(EventShortDetailsHtml.Text), 500),
						EventLongDetailsHtml.GetHtml(),
						(Guid)ContainerPage.ViewStatePublic["EventDuplicateGuid"],
						EventCapacity.Text.Trim().Length > 0 ? int.Parse(EventCapacity.Text.Trim()) : null as int?,
						Usr.Current, 
						MusicTypesUc.SelectedMusicTypes.ToArray(), 
						null,
						SpotterRequestYes.Checked,
						SpotterRequestYes.Checked ? Cambro.Web.Helpers.StripHtml(SpotterRequestName.Text).Truncate(100) : "",
						SpotterRequestYes.Checked ? Cambro.Web.Helpers.StripHtml(SpotterRequestNumber.Text).Truncate(100) : ""
					);
					ViewState["InsertedEventK"] = ev.K;
				}
				else
				{
					ViewState["InsertedEventK"] = esDup[0].K;
					duplicate = true;
				}
			}
			else if (IsEdit)
			{

				if (!Usr.Current.CanEdit(CurrentEvent))
					throw new Exception("You may not edit this event!");

				bool changedDate = false;
				bool changedVenue = false;
				CurrentEvent.Name = Cambro.Web.Helpers.StripHtml(EventName.Text).Trim();
				CurrentEvent.StartTime = StartTime;
				if (CanEditDateVenue)
				{
					if (CurrentEvent.DateTime != PanelDateCal.SelectedDate)
					{
						CurrentEvent.DateTime = PanelDateCal.SelectedDate;
						changedDate = true;
					}
				}
				CurrentEvent.ShortDetailsHtml = Cambro.Misc.Utility.Snip(Cambro.Web.Helpers.StripHtml(EventShortDetailsHtml.Text), 500);

				CurrentEvent.LongDetailsHtml = EventLongDetailsHtml.GetHtml();

				CurrentEvent.SpotterRequest = SpotterRequestYes.Checked;
				if (SpotterRequestYes.Checked)
				{
					CurrentEvent.SpotterRequestName = Cambro.Web.Helpers.StripHtml(SpotterRequestName.Text).Truncate(100);
					CurrentEvent.SpotterRequestNumber = Cambro.Web.Helpers.StripHtml(SpotterRequestNumber.Text).Truncate(100);
				}
				else
				{
					CurrentEvent.SpotterRequestName = "";
					CurrentEvent.SpotterRequestNumber = "";
				}

				if (EventCapacity.Text.Length > 0)
					CurrentEvent.Capacity = int.Parse(EventCapacity.Text);

				if (CurrentEvent.AdminNote.Length > 0)
					CurrentEvent.AdminNote += "\n";
				CurrentEvent.AdminNote += "Event modified by " + Usr.Current.NickName + " (K=" + Usr.Current.K.ToString() + ") " + DateTime.Now.ToString();

				if (Usr.Current.IsSuper)
				{
					CurrentEvent.IsNew = false;
					CurrentEvent.IsEdited = false;
				}
				else
				{
					CurrentEvent.IsEdited = true;
					CurrentEvent.ModeratorUsrK = Usr.GetEventModeratorUsrK();
				}

				CurrentEvent.Update();

				if (CanEditDateVenue)
				{
					Venue v = this.PanelDetailsVenuePicker.Venue;
					if (v!= null && CurrentEvent.VenueK != v.K)
					{
						changedVenue = true;
						CurrentEvent.ChangeVenue(v.K, false);
					}
				}

				if (changedDate || changedVenue)
				{
					CurrentEvent.UpdateUrlFragment(false);
					Utilities.UpdateChildUrlFragmentsJob job = new Utilities.UpdateChildUrlFragmentsJob(Model.Entities.ObjectType.Event, CurrentEvent.K, true);
					job.ExecuteAsynchronously();
				}

				foreach (int ob in MusicTypesUc.SelectedMusicTypes)
				{
					MusicType mt = new MusicType(ob);
					try
					{
						EventMusicType emt = new EventMusicType(CurrentEvent.K, mt.K);
					}
					catch
					{
						EventMusicType emt = new EventMusicType();
						emt.EventK = CurrentEvent.K;
						emt.MusicTypeK = mt.K;
						emt.Update();
					}
				}
				CurrentEvent.MusicTypes = null;
				foreach (EventMusicType emt in CurrentEvent.EventMusicTypes)
				{
					if (!MusicTypesUc.SelectedMusicTypes.Contains(emt.MusicTypeK))
					{
						emt.Delete();
						emt.Update();
					}
				}

				CurrentEvent.UpdateMusicTypesString(null);
			}
			if (IsNew || IsEdit)
			{
				if (!duplicate)
				{
					//Update brands...
					ArrayList selectedBrands = new ArrayList();
					ArrayList eventBrands = new ArrayList();
					ArrayList allBrandKs = new ArrayList();
					ArrayList allBrands = new ArrayList();

					foreach (var kvp in uiBrandsMultiSelector.Selections)
					{
						try
						{
							Brand b = new Brand(int.Parse(kvp.Value));
							selectedBrands.Add(b.K);
							if (!allBrandKs.Contains(b.K))
							{
								allBrandKs.Add(b.K);
								allBrands.Add(b);
							}
						}
						catch
						{ }
					}
					foreach (Brand b in CurrentEvent.Brands)
					{
						eventBrands.Add(b.K);
						if (!selectedBrands.Contains(b.K))
							CurrentEvent.AssignBrand(b.K, false, null);
					}
					foreach (int i in selectedBrands)
					{
						if (!eventBrands.Contains(i))
							CurrentEvent.AssignBrand(i, true, null);

					}
					foreach (Brand b in allBrands)
					{
						b.UpdateTotalComments(null);
					}


				}
			}

		}
		#endregion
		#region PanelPic
		void PanelPicLoad(object o, EventArgs e)
		{
			if (CurrentEvent != null)
			{
				CurrentEvent = null;
				PicUcEvent.InputObject = CurrentEvent;
				PanelPicBindDefaultPics();
			}
		}
		public void PanelPicBind()
		{
			PicUcEvent.InitPic();
			PicUploadPanel.Visible = true;
		}
		void PanelPicBindDefaultPics()
		{
			Query q = new Query();
			q.NoLock = true;
			q.QueryCondition = new And(new Q(Event.Columns.K, CurrentEvent.K), new Or(new Q(Brand.Columns.Pic, QueryOperator.IsNotNull, null), new Q(Brand.Columns.Pic, QueryOperator.NotEqualTo, Guid.Empty)));
			q.OrderBy = new OrderBy(Brand.Columns.Name);
			q.TableElement = Brand.EventJoin;
			BrandSet PicBrands = new BrandSet(q);

			if (PicBrands.Count > 0)
			{
				PicUploadDefaultDataList.DataSource = PicBrands;
				PicUploadDefaultDataList.DataBind();
				PicUploadDefaultPanel.Visible = true;
			}
			else
				PicUploadDefaultPanel.Visible = false;
		}
		protected void PanelPicEventNoPic(object o, EventArgs e)
		{
			SavedRedirect();
		}
		protected void PanelPicEventSaved(object o, EventArgs e)
		{
			if (CurrentEvent != null)
			{
				CurrentEvent.IsEdited = true;
				CurrentEvent.ModeratorUsrK = Usr.GetEventModeratorUsrK();
				CurrentEvent.Update();

			}
			SavedRedirect();
		}
		public void PicUploadDefaultSelect(object o, DataListCommandEventArgs e)
		{
			Brand b = new Brand(int.Parse(e.CommandArgument.ToString()));
			if (b.HasPic)
			{
				bool hasOldPic = CurrentEvent.HasPic;
				Guid oldPic = CurrentEvent.HasPic ? CurrentEvent.Pic : Guid.Empty;

				CurrentEvent.Pic = Guid.NewGuid();

				Storage.AddToStore(
					Storage.GetFromStore(Storage.Stores.Pix, b.Pic, "jpg"),
					Storage.Stores.Pix,
					CurrentEvent.Pic,
					"jpg",
					CurrentEvent,
					"Pic");

				CurrentEvent.Update();

				if (hasOldPic)
					Storage.RemoveFromStore(Storage.Stores.Pix, oldPic, "jpg");

				SavedRedirect();
			}
		}
		#endregion
		

		protected void SavedRedirect()
		{
			if (ContainerPage.Url["promoterk"].IsInt && ContainerPage.Url["promoterk"] > 0)
			{
				Promoter p = new Promoter(ContainerPage.Url["promoterk"]);
				if (Usr.Current.IsAdmin || Usr.Current.IsPromoterK(ContainerPage.Url["promoterk"]))
					Response.Redirect(p.UrlEventOptions(CurrentEvent));
				else
					throw new Exception("Can't redirect to this promoter!");
			}
			else if (ContainerPage.Url["signup"] == 1)
				Response.Redirect(CurrentEvent.SpotterSignUpUrl);
			else
				Response.Redirect(CurrentEvent.Url()); 
		}

		#region IsNew, IsEdit
		bool IsNew
		{
			get
			{
				return ContainerPage.Url["VenueK"].IsInt;
			}
		}
		bool IsEdit
		{
			get
			{
				return ContainerPage.Url.HasEventObjectFilter;
			}
		}
		#endregion

		#region VenueK
		int VenueK
		{
			get
			{
				if (IsNew)
					return ContainerPage.Url["VenueK"];
				else if (IsEdit)
					return CurrentEvent.VenueK;
				else
					return 0;
			}
		}
		#endregion
		#region CurrentVenue
		Venue CurrentVenue
		{
			get
			{
				if (currentVenue == null && VenueK > 0)
					currentVenue = new Venue(VenueK);
				return currentVenue;
			}
		}
		Venue currentVenue;
		#endregion
		#region EventK
		int EventK
		{
			get
			{
				if (IsNew && ViewState["InsertedEventK"] != null)
					return (int)ViewState["InsertedEventK"];
				else if (IsNew)
					return 0;
				else if (IsEdit)
					return ContainerPage.Url.ObjectFilterK;
				else
					return 0;
			}
		}
		#endregion
		#region CurrentEvent
		Event CurrentEvent
		{
			get
			{
				if (currentEvent == null && EventK > 0)
					currentEvent = new Event(EventK);
				return currentEvent;
			}
			set
			{
				currentEvent = value;
			}
		}
		Event currentEvent;
		#endregion

		#region ChangePanel
		void ChangePanel(Panel p)
		{
			PanelSelectionError.Visible = p.Equals(PanelSelectionError);
			PanelDate.Visible = p.Equals(PanelDate);
			PanelConflict.Visible = p.Equals(PanelConflict);
			PanelDetails.Visible = p.Equals(PanelDetails);
			PanelMusicTypes.Visible = p.Equals(PanelMusicTypes);
			PanelPic.Visible = p.Equals(PanelPic);
			PanelEditError.Visible = p.Equals(PanelEditError);
		}
		#endregion


	}
}
