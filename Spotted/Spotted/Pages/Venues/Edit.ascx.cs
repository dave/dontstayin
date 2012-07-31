using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Bobs;
using System.Text.RegularExpressions;
using Bobs.JobProcessor;
using Spotted.Controls;

namespace Spotted.Pages.Venues
{
	public partial class Edit : DsiUserControl
	{
		protected Spotted.CustomControls.h1 HeaderH1;
		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn();

			if (IsEdit)
			{
				HeaderH1.InnerText = "Edit venue - " + CurrentVenue.Name;
				ContainerPage.SetPageTitle("Edit venue - " + CurrentVenue.Name);
			}
			else
			{
				HeaderH1.InnerText = "Add venue";
				ContainerPage.SetPageTitle("Add venue");
			}

			if (!Page.IsPostBack)
			{
				SelectInitialPanel();
			}
		}
		public void Page_PreRender(object o, System.EventArgs e)
		{
			ContainerPage.ViewStatePublic["VenueDuplicateGuid"] = Guid.NewGuid();
		}
		#region SelectInitialPanel()
		void SelectInitialPanel()
		{
			if (IsNew)
			{
				ChangePanel(PanelDetails);
			}
			else if (IsEdit)
			{
				if (Usr.Current.CanEdit(CurrentVenue))
				{
					if (ContainerPage.Url["Page"].Equals("Pic"))
					{
						ChangePanel(PanelPic);
						PicUc.InputObject = CurrentVenue;
						PicUc.InitPic();
					}
					else
					{
						ChangePanel(PanelDetails);
					}
				}
				else
					ChangePanel(PanelEditError);
			}
		}
		#endregion

		#region New, Edit
		bool IsNew
		{
			get
			{
				return !ContainerPage.Url.HasVenueObjectFilter;
			}
		}
		bool IsEdit
		{
			get
			{
				return ContainerPage.Url.HasVenueObjectFilter;
			}
		}
		#endregion

		#region CurrentVenue
		Venue CurrentVenue
		{
			get
			{
				return ContainerPage.Url.ObjectFilterVenue;
			}
		}
		#endregion

		#region PanelEditError
		protected Panel PanelEditError;
		#endregion

		protected Picker PanelDetailsPlacePicker;
		public void PostcodeVal(object o, ServerValidateEventArgs e)
		{
			Place p = PanelDetailsPlacePicker.Place;
			if (p == null)
			{
				e.IsValid = true;
			}
			else
			{
				if (p.Country.PostcodeType == 1)
				{
					Regex r = new Regex(Cambro.Misc.RegEx.Postcode);
					e.IsValid = r.IsMatch(PanelDetailsPostcodeTextBox.Text);
				}
				else
					e.IsValid = true;
			}
			if (!e.IsValid)
				ContainerPage.AnchorSkip("SubmitButton");
		}

		public bool CanEditPlace
		{
			get
			{
				if (Usr.Current != null && Usr.Current.IsJunior)
					return true;
				if (!IsEdit)
					return true;
				else
					return CurrentVenue.AddedDateTime.AddDays(7) > DateTime.Now;
			}
		}

		#region PanelDetails
		public void PanelDetailsLoad(object o, System.EventArgs e)
		{
			bool passedPlaceHasPostcode = false;
			if (ContainerPage.Url["PlaceK"].IsInt)
			{
				Place passedPlace = new Place(ContainerPage.Url["PlaceK"]);
				if (passedPlace.Country.PostcodeType == 1)
					passedPlaceHasPostcode = true;
			}

			if (IsEdit)
			{
				PanelDetailsPostcodeDiv.Visible = CurrentVenue.Place.Country.PostcodeType == 1 || passedPlaceHasPostcode;
			}
			else
			{
				PanelDetailsPostcodeDiv.Visible = (Country.Current != null && Country.Current.PostcodeType == 1) || passedPlaceHasPostcode;
			}

			if (CanEditPlace)
			{
				PanelDetailsPlaceDiv.Visible = true;
				PanelDetailsPlaceLockedDiv.Visible = false;

				if (!Page.IsPostBack)
				{
					Query q = new Query();
					q.NoLock = true;
					q.Columns = new ColumnSet(Place.Columns.K, Place.Columns.Name, Place.Columns.CountryK, Place.Columns.RegionAbbreviation);
					q.QueryCondition = new And(Country.PlaceFilterQ, new Q(Place.Columns.Enabled, true));
					q.OrderBy = new OrderBy(Place.Columns.Name);
					PlaceSet ts = new PlaceSet(q);
					
					Place placeToSelect = null;
					if (IsEdit)
					{
						placeToSelect = CurrentVenue.Place;
					}
					else if (ContainerPage.Url["PlaceK"].IsInt)
					{
						placeToSelect = new Place(ContainerPage.Url["PlaceK"]);
					}
					if (placeToSelect != null)
					{
						PanelDetailsPlacePicker.Place = placeToSelect;
					}
				}
			}
			else
			{
				PanelDetailsPlacePicker.Place = CurrentVenue.Place;

				PanelDetailsPlaceDiv.Visible = false;
				PanelDetailsPlaceLockedDiv.Visible = true;

				PanelDetailsPlaceLockedLabel.Text = CurrentVenue.Place.Name;
			}
			if (IsEdit)
			{

			}
			if (IsEdit && !Page.IsPostBack)
			{

				PanelDetailsPostcodeTextBox.Text = CurrentVenue.Postcode;
				PanelDetailsVenueName.Text = CurrentVenue.Name;
				PanelDetailsVenueCapacity.Text = CurrentVenue.Capacity.ToString();
				PanelDetailsVenueDetailsHtml.LoadHtml(CurrentVenue.DetailsHtml);
				PanelDetailsVenueRegularEventsYes.Checked = CurrentVenue.RegularEvents;
				PanelDetailsVenueRegularEventsNo.Checked = !CurrentVenue.RegularEvents;
			}
		}
		public void PanelDetailsNext(object o, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				if (HasDuplicatePostcode)
				{
					PanelPostcodeCheckDataGrid.DataSource = DuplicatePostcodeVenueSet;
					PanelPostcodeCheckDataGrid.DataBind();
					ChangePanel(PanelPostcodeCheck);
				}
				else
				{
					SaveNow();
				}

			}
		}
		public void RegularEventsVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = PanelDetailsVenueRegularEventsYes.Checked || PanelDetailsVenueRegularEventsNo.Checked;
			if (!e.IsValid)
				ContainerPage.AnchorSkip("SubmitButton");
		}
		#endregion

		#region PanelPostcodeCheck
		protected Panel PanelPostcodeCheck;
		protected DataGrid PanelPostcodeCheckDataGrid;
		protected CheckBox PanelPostcodeCheckNewCheckBox;
		public void PanelPostcodeCheckLoad(object o, System.EventArgs e)
		{
			if (Page.IsPostBack && PanelPostcodeCheck.Visible)
			{
				PanelPostcodeCheckDataGrid.DataSource = DuplicatePostcodeVenueSet;
				PanelPostcodeCheckDataGrid.DataBind();
			}
		}
		public void PanelPostcodeCheckBack(object o, System.EventArgs e)
		{
			ChangePanel(PanelDetails);
		}
		public void PanelPostcodeCheckNext(object o, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				SaveNow();
			}
		}
		public void PanelPostcodeCheckNewCheckBoxVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = PanelPostcodeCheckNewCheckBox.Checked;
			if (!e.IsValid)
				ContainerPage.AnchorSkip("SubmitButton");
		}
		#region DuplicatePostcode
		VenueSet DuplicatePostcodeVenueSet
		{
			get
			{
				Q notThisVenue = new Q(true);
				if (IsEdit)
					notThisVenue = new Q(Venue.Columns.K, QueryOperator.NotEqualTo, CurrentVenue.K);

				if (duplicatePostcodeVenueSet == null)
					duplicatePostcodeVenueSet = Venue.SimilarVenuesStatic(
						Cambro.Web.Helpers.StripHtml(PanelDetailsVenueName.Text),
						PanelDetailsPlacePicker.Place,
						IsEdit ? CurrentVenue.K : 0,
						Cambro.Web.Helpers.StripHtml(PanelDetailsPostcodeTextBox.Text));
				return duplicatePostcodeVenueSet;
			}
		}
		VenueSet duplicatePostcodeVenueSet;
		bool HasDuplicatePostcode
		{
			get
			{
				return DuplicatePostcodeVenueSet.Count > 0;
			}
		}
		#endregion
		#endregion

		#region SaveNow
	 
		public void SaveNow()
		{
			if (Page.IsValid)
			{
				if (IsEdit)
				{
					if (!Usr.Current.CanEdit(CurrentVenue))
						throw new Exception("You can't edit this venue!");
					else
					{
						bool changedName = false;
						bool changedPlace = false;
						string newName = Cambro.Web.Helpers.StripHtml(PanelDetailsVenueName.Text.Trim());
						if (!CurrentVenue.Name.Equals(newName))
						{
							changedName = true;
							CurrentVenue.Name = newName;
						}
						CurrentVenue.Postcode = Cambro.Web.Helpers.StripHtml(PanelDetailsPostcodeTextBox.Text);
						CurrentVenue.Capacity = int.Parse(PanelDetailsVenueCapacity.Text);
						CurrentVenue.RegularEvents = PanelDetailsVenueRegularEventsYes.Checked;
						CurrentVenue.DetailsHtml = PanelDetailsVenueDetailsHtml.GetHtml();
						if (CurrentVenue.AdminNote.Length > 0)
							CurrentVenue.AdminNote += "\n";
						CurrentVenue.AdminNote += "Venue modified by " + Usr.Current.NickName + " (K=" + Usr.Current.K.ToString() + ") " + DateTime.Now.ToString();

						if (Usr.Current.IsSuper)
						{
							CurrentVenue.IsNew = false;
							CurrentVenue.IsEdited = false;
						}
						else
						{
							CurrentVenue.IsEdited = true;
							CurrentVenue.ModeratorUsrK = Usr.GetEventModeratorUsrK();
						}


						CurrentVenue.Update();

						if (changedName)
							CurrentVenue.CreateUniqueUrlName(false);

						if (CanEditPlace)
						{
							Place p = PanelDetailsPlacePicker.Place;
							if (p.K != CurrentVenue.PlaceK)
							{
								changedPlace = true;
								CurrentVenue.ChangePlace(p.K, false);
							}
						}
						if (changedName || changedPlace)
						{
							CurrentVenue.UpdateUrlFragment(false);

							Utilities.UpdateChildUrlFragmentsJob job = new Utilities.UpdateChildUrlFragmentsJob(Model.Entities.ObjectType.Venue, CurrentVenue.K, true);
							job.ExecuteAsynchronously();


						}
						Response.Redirect(CurrentVenue.UrlApp("edit", "page", "pic"));
					}
				}
				else if (IsNew)
				{
					//Duplicate?
					VenueSet vsDup = new VenueSet(new Query(new Q(Venue.Columns.DuplicateGuid, (Guid)ContainerPage.ViewStatePublic["VenueDuplicateGuid"])));
					if (vsDup.Count == 0)
					{
						Venue v = Venue.Add(
							Usr.Current,
							Cambro.Web.Helpers.StripHtml(PanelDetailsVenueName.Text.Trim()),
							int.Parse(PanelDetailsVenueCapacity.Text),
							PanelDetailsPlacePicker.Place.K,
							Cambro.Web.Helpers.StripHtml(PanelDetailsPostcodeTextBox.Text),
							PanelDetailsVenueRegularEventsYes.Checked,
							(Guid)ContainerPage.ViewStatePublic["VenueDuplicateGuid"],
							PanelDetailsVenueDetailsHtml.GetHtml()
						);
						Response.Redirect(v.UrlApp("edit", "page", "pic"));

					}
					else
					{
						Response.Redirect(vsDup[0].UrlApp("edit", "page", "pic"));
					}
				}

			}
		}
		#endregion

		#region PanelPic
		 public void PanelPicLoad(object o, System.EventArgs e)
		{
			if (CurrentVenue != null)
				PicUc.InputObject = CurrentVenue;
		}
		void PicNext()
		{
			if (ContainerPage.Url["AddEvent"] == 1)
			{
				string urlPart = "";
				if (ContainerPage.Url["SignUp"] == 1)
					urlPart = "/signup-1";
				Response.Redirect("/pages/events/edit/venuek-" + CurrentVenue.K.ToString() + urlPart);
			}
			ChangePanel(PanelSaved);
		}
		public void PanelPicNoPic(object o, System.EventArgs e)
		{
			PicNext();
		}
		public void PanelPicSaved(object o, System.EventArgs e)
		{
			if (CurrentVenue != null)
			{
				CurrentVenue.IsEdited = true;
				CurrentVenue.ModeratorUsrK = Usr.GetEventModeratorUsrK();
				CurrentVenue.Update();
			}
			PicNext();
		}
		#endregion

		#region PanelSaved
		 void PanelSavedLoad(object o, System.EventArgs e)
		{

		}
		void PanelSavedPreRender(object o, System.EventArgs e)
		{
			if (CurrentVenue != null)
			{
				PanelSavedAddEventLink.HRef = CurrentVenue.AddEventLink;
				PanelSavedVenueLink.InnerText = CurrentVenue.FriendlyName;
				PanelSavedVenueLink.HRef = CurrentVenue.Url();
			}
		}

		#endregion

		#region ChangePanel
		void ChangePanel(Panel p)
		{
			PanelEditError.Visible = p.Equals(PanelEditError);
			PanelPostcodeCheck.Visible = p.Equals(PanelPostcodeCheck);
			PanelDetails.Visible = p.Equals(PanelDetails);
			PanelSaved.Visible = p.Equals(PanelSaved);
			PanelPic.Visible = p.Equals(PanelPic);
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
			this.Load += new System.EventHandler(PanelPostcodeCheckLoad);
			this.Load += new System.EventHandler(PanelDetailsLoad);
			this.Load += new System.EventHandler(PanelPicLoad);
			this.Load += new System.EventHandler(PanelSavedLoad);
			this.PreRender += new System.EventHandler(PanelSavedPreRender);

		}
		#endregion
	}

}
