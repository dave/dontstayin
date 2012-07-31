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
using Spotted.Controls;

namespace Spotted.Pages
{
	public partial class Spotters : DsiUserControl
	{
		public bool WeAreSendingCards = true;

		#region Page_Load
		private void Page_Load(object sender, System.EventArgs e)
		{
			DialingCodeDropDown.Attributes["onclick"] = "if (document.forms[0].elements['" + DialingCodeDropDown.ClientID + "'][document.forms[0].elements['" + DialingCodeDropDown.ClientID + "'].selectedIndex].value=='0') {document.getElementById('" + DialingCodeOtherSpan.ClientID + "').style.display=''}else{document.getElementById('" + DialingCodeOtherSpan.ClientID + "').style.display='none'}";
			if (Page.IsPostBack)
				Usr.KickUserIfNotLoggedIn();

			if (Usr.Current == null)
			{
				ChangePanel(PanelIntro1);
				return;
			}

			if (!Page.IsPostBack)
			{
				if (ContainerPage.Url["EventK"].IsInt)
				{
					SignUpEvent();
				}
				else if (Usr.Current.IsSpotter)
				{
					ChangePanel(PanelEnabled);
				}
				else
				{
					ChangePanel(PanelIntro1);
				}
			}
			if (Usr.Current.IsSpotter)
				BindEvents();

			this.RequestCardsPanel_Load(sender, e);
			this.PanelEnabled_Load(sender, e);
			this.PanelSignUpForm_Load(sender, e);



		}
		#endregion

		#region CurrentEvent
		Event CurrentEvent
		{
			get
			{
				if (currentEvent == null && ContainerPage.Url["EventK"].IsInt)
					currentEvent = new Event(ContainerPage.Url["EventK"]);
				return currentEvent;
			}
		}
		Event currentEvent;
		#endregion
		#region CurrentUsr
		protected Usr CurrentUsr
		{
			get
			{
				if (Usr.Current == null)
					currentUsr = new Usr();
				else
					currentUsr = Usr.Current;
				return currentUsr;
			}
		}
		Usr currentUsr;
		#endregion

		#region PanelIntro1
		protected Panel PanelIntro1;
		public void PanelIntro1Click(object o, System.EventArgs e)
		{
			ChangePanel(PanelSignUpForm);
		}
		#endregion
		
		#region PanelSignUpForm


		public void ChangeSpotterDetails(object o, System.EventArgs e)
		{
			ChangePanel(PanelSignUpForm);
		}

		public void PanelSignUpForm_Load(object o, System.EventArgs e)
		{
			if (Usr.Current == null)
				return;

			#region set up mobile number dialing code
			if (!Page.IsPostBack)
			{
				if (Usr.Current.Mobile.Length > 0)
				{
					if (
						Usr.Current.MobileCountryCode.Equals("44") ||
						Usr.Current.MobileCountryCode.Equals("61") ||
						Usr.Current.MobileCountryCode.Equals("33") ||
						Usr.Current.MobileCountryCode.Equals("49") ||
						Usr.Current.MobileCountryCode.Equals("353") ||
						Usr.Current.MobileCountryCode.Equals("39") ||
						Usr.Current.MobileCountryCode.Equals("34") ||
						Usr.Current.MobileCountryCode.Equals("1")
						)
					{
						DialingCodeDropDown.SelectedValue = Usr.Current.MobileCountryCode;
						DialingCodeOtherSpan.Style["display"] = "none";
					}
					else
					{
						DialingCodeDropDown.SelectedValue = "0";
						DialingCodeOtherSpan.Style["display"] = null;
						DialingCodeOther.Text = Usr.Current.MobileCountryCode;
					}
					MobileNumber.Text = Usr.Current.MobileNumber;
				}
				else
				{
					if (Country.Current != null && Country.Current.DialingCode > 0)
					{
						DialingCodeDropDown.SelectedValue = Country.Current.DialingCode.ToString();
						DialingCodeOtherSpan.Style["display"] = "none";
					}
					else
					{
						DialingCodeDropDown.SelectedValue = "0";
						DialingCodeOtherSpan.Style["display"] = null;
					}
				}
			}
			if (DialingCodeDropDown.SelectedValue.Equals("0"))
			{
				DialingCodeOtherSpan.Style["display"] = null;
			}
			else
			{
				DialingCodeOtherSpan.Style["display"] = "none";
			}
			#endregion
			#region set up address
			if (!Page.IsPostBack)
			{
				CountrySet cs = new CountrySet(new Query());
				AddressCountry.DataSource = cs;
				AddressCountry.DataTextField = "Name";
				AddressCountry.DataValueField = "K";
				AddressCountry.DataBind();

				FirstName.Text = Usr.Current.FirstName;
				LastName.Text = Usr.Current.LastName;
				AddressStreet.Text = Usr.Current.AddressStreet;
				AddressArea.Text = Usr.Current.AddressArea;
				AddressTown.Text = Usr.Current.AddressTown;
				AddressCounty.Text = Usr.Current.AddressCounty;
				AddressPostcode.Text = Usr.Current.AddressPostcode;

				PhotoUsageUse.Checked = Usr.Current.PhotoUsage == Model.Entities.Usr.PhotoUsageEnum.Use;
				PhotoUsageContact.Checked = Usr.Current.PhotoUsage == Model.Entities.Usr.PhotoUsageEnum.Contact;
				PhotoUsageDoNotUse.Checked = Usr.Current.PhotoUsage == Model.Entities.Usr.PhotoUsageEnum.DoNotUse;

				if (Usr.Current.DateOfBirth > DateTime.MinValue)
				{
					DateOfBirthDay.Text = Usr.Current.DateOfBirth.Day.ToString();
					DateOfBirthMonth.Text = Usr.Current.DateOfBirth.Month.ToString();
					DateOfBirthYear.Text = Usr.Current.DateOfBirth.Year.ToString();
				}
				if (Usr.Current.AddressCountryK > 0)
				{
					try
					{
						AddressCountry.SelectedValue = Usr.Current.AddressCountryK.ToString();
					}
					catch { }
				}
				else if (Country.FilterK > 0)
				{
					try
					{
						AddressCountry.SelectedValue = Country.FilterK.ToString();
					}
					catch { }
				}
			}
			#endregion
		}

		#region PanelSignUpFormClick
		public void PanelSignUpFormClick(object o, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				if (Usr.Current.IsSpotter)
				{
					StoreSpotter();
				}
				else
				{
					ChangePanel(PanelChecklist);
				}
			}
		}
		#endregion

		#region AddressPostcodeVal
		protected void AddressPostcodeVal(object o, ServerValidateEventArgs e)
		{
			Country c = new Country(int.Parse(AddressCountry.SelectedValue));
			if (c.PostcodeType == 1)
			{
				Regex r = new Regex(Cambro.Misc.RegEx.Postcode);
				e.IsValid = r.IsMatch(AddressPostcode.Text);
			}
			else
				e.IsValid = true;
		}
		#endregion
		#region DateOfBirthVal
		public void DateOfBirthVal(object o, ServerValidateEventArgs e)
		{
			try
			{
				int day = int.Parse(DateOfBirthDay.Text);
				int month = int.Parse(DateOfBirthMonth.Text);
				int year = int.Parse(DateOfBirthYear.Text);
				DateTime d = new DateTime(year, month, day);
				if (year < 1850 || DateOfBirthYear.Text.Length != 4)
					e.IsValid = false;
				else
					e.IsValid = true;
			}
			catch
			{
				e.IsValid = false;
			}
		}
		#endregion
		#region DateOfBirthAdultVal
		public void DateOfBirthAdultVal(object o, ServerValidateEventArgs e)
		{
			try
			{
				int day = int.Parse(DateOfBirthDay.Text);
				int month = int.Parse(DateOfBirthMonth.Text);
				int year = int.Parse(DateOfBirthYear.Text);
				DateTime d = new DateTime(year, month, day);
				if (year < 1850 || DateOfBirthYear.Text.Length != 4)
				{
					e.IsValid = true;
				}
				else
				{
					if (DateTime.Now.AddYears(-18) < d)
						e.IsValid = false;
					else
						e.IsValid = true;
				}
			}
			catch
			{
				e.IsValid = true;
			}
		}
		#endregion
		#region StoreMobileNumber()
		protected void StoreMobileNumber()
		{
			Regex rNumbers = new Regex("[^0123456789]");
			string mobileNumber = rNumbers.Replace(MobileNumber.Text.Trim(), "");
			string dialingCode = rNumbers.Replace(DialingCodeDropDown.SelectedValue, "");
			string dialingCodeOther = rNumbers.Replace(DialingCodeOther.Text.Trim(), "");
			string fullMobile = "";
			if (mobileNumber.StartsWith("0"))
			{
				mobileNumber = mobileNumber.Substring(1);
			}
			if (dialingCode.Equals("0"))
			{
				dialingCode = dialingCodeOther;
			}
			if (mobileNumber.Length > 0)
			{
				fullMobile = dialingCode + mobileNumber;
			}
			if (MobileNumber.Text != mobileNumber)
				MobileNumber.Text = mobileNumber;
			if (DialingCodeDropDown.SelectedValue.Equals("0") && DialingCodeOther.Text != dialingCode)
				DialingCodeOther.Text = dialingCode;

			if (!Usr.Current.Mobile.Equals(fullMobile))
				Usr.Current.AdminNote += "\n\nUsr has changed mobile number from " + Usr.Current.Mobile + " to: " + fullMobile + " on " + DateTime.Now.ToString() + ".\n";

			Usr.Current.Mobile = fullMobile;
			Usr.Current.MobileCountryCode = dialingCode;
			Usr.Current.MobileNumber = mobileNumber;
			Usr.Current.Update();
		}
		#endregion
		#endregion

		void StoreSpotter()
		{

			bool NewSpotter = !Usr.Current.IsSpotter;

			Country c = new Country(int.Parse(AddressCountry.SelectedValue));

			#region update AdminNote when edited
			if (!NewSpotter)
			{
				Usr.Current.AdminNote += "\n\n******Spotter changed details on " + DateTime.Now.ToString() + " - old details: *****\n";
				if (!Usr.Current.FirstName.Equals(Cambro.Web.Helpers.StripHtml(FirstName.Text))
					|| !Usr.Current.LastName.Equals(Cambro.Web.Helpers.StripHtml(LastName.Text)))
					Usr.Current.AdminNote += "Name: " + Usr.Current.FirstName + " " + Usr.Current.LastName + "\n";
				if (!Usr.Current.AddressStreet.Equals(Cambro.Web.Helpers.StripHtml(AddressStreet.Text)))
					Usr.Current.AdminNote += "Street: " + Usr.Current.AddressStreet + "\n";
				if (!Usr.Current.AddressArea.Equals(Cambro.Web.Helpers.StripHtml(AddressArea.Text)))
					Usr.Current.AdminNote += "Area: " + Usr.Current.AddressArea + "\n";
				if (!Usr.Current.AddressTown.Equals(Cambro.Web.Helpers.StripHtml(AddressTown.Text)))
					Usr.Current.AdminNote += "Town: " + Usr.Current.AddressTown + "\n";
				if (!Usr.Current.AddressCounty.Equals(Cambro.Web.Helpers.StripHtml(AddressCounty.Text)))
					Usr.Current.AdminNote += "County: " + Usr.Current.AddressCounty + "\n";
				if (!Usr.Current.AddressPostcode.Equals(Cambro.Web.Helpers.StripHtml(AddressPostcode.Text)))
					Usr.Current.AdminNote += "Postcode: " + Usr.Current.AddressPostcode + "\n";
				if (!Usr.Current.DateOfBirth.Equals(new DateTime(int.Parse(DateOfBirthYear.Text), int.Parse(DateOfBirthMonth.Text), int.Parse(DateOfBirthDay.Text))))
					Usr.Current.AdminNote += "Date of birth: " + Usr.Current.DateOfBirth.ToString() + "\n";
				if (Usr.Current.AddressCountryK != c.K)
					Usr.Current.AdminNote += "Country: " + c.K + " (" + c.Name + ")\n";
			}
			#endregion

			Usr.Current.IsSpotter = true;
			Usr.Current.FirstName = Cambro.Web.Helpers.StripHtml(FirstName.Text);
			Usr.Current.LastName = Cambro.Web.Helpers.StripHtml(LastName.Text);
			StoreMobileNumber();
			Usr.Current.AddressStreet = Cambro.Web.Helpers.StripHtml(AddressStreet.Text);
			Usr.Current.AddressArea = Cambro.Web.Helpers.StripHtml(AddressArea.Text);
			Usr.Current.AddressTown = Cambro.Web.Helpers.StripHtml(AddressTown.Text);
			Usr.Current.AddressCounty = Cambro.Web.Helpers.StripHtml(AddressCounty.Text);
			Usr.Current.AddressPostcode = Cambro.Web.Helpers.StripHtml(AddressPostcode.Text);
			Usr.Current.DateOfBirth = new DateTime(int.Parse(DateOfBirthYear.Text), int.Parse(DateOfBirthMonth.Text), int.Parse(DateOfBirthDay.Text));
			Usr.Current.AddressCountryK = c.K;

			if (PhotoUsageDoNotUse.Checked)
				Usr.Current.PhotoUsage = Model.Entities.Usr.PhotoUsageEnum.DoNotUse;
			else if (PhotoUsageContact.Checked)
				Usr.Current.PhotoUsage = Model.Entities.Usr.PhotoUsageEnum.Contact;
			else
				Usr.Current.PhotoUsage = Model.Entities.Usr.PhotoUsageEnum.Use;

			if (NewSpotter)
			{
				Usr.Current.CardStatus = Usr.CardStatusEnum.New;

				
				Usr dsiUsr = new Usr(8);

				if (true)
				{
					Bobs.Group spottersGroup = new Bobs.Group(3480);
					GroupUsr guTarget = spottersGroup.GetGroupUsr(Usr.Current);
					GroupUsr guDsi = spottersGroup.GetGroupUsr(dsiUsr);

					spottersGroup.Invite(Usr.Current, guTarget, dsiUsr, guDsi, "Chat about being a Spotter and all things Spotting in the DontStayIn Spotters group!", false);

				}


				if (Usr.Current.AddressCountryK == 225)
				{
					Bobs.Group spottersGroup = new Bobs.Group(4537);
					GroupUsr guTarget = spottersGroup.GetGroupUsr(Usr.Current);
					GroupUsr guDsi = spottersGroup.GetGroupUsr(dsiUsr);

					spottersGroup.Invite(Usr.Current, guTarget, dsiUsr, guDsi, "Chat about being a USA based DontStayIn Spotter in the USA Spotters group!", false);
				}

				
			}

			Usr.Current.Update();

			if (CurrentEvent != null)
				Response.Redirect(CurrentEvent.SpotterSignUpUrl);
			else
				Response.Redirect("/pages/spotters");

		}

		#region PanelChecklist
		public void CheklistVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = Checklist.AllTicked;
		}
		#region SaveSpotter
		public void SaveSpotter(object o, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				StoreSpotter();
			}
		}
		#endregion
		#endregion

		#region PanelEnabled


		#region PanelEnabled_Load
		public void PanelEnabled_Load(object o, System.EventArgs e)
		{
			if (Usr.Current != null && Usr.Current.IsSpotter)
			{
				//bool goodSpotter = Usr.Current.SpotterStars >= 4 || Usr.Current.IsProSpotter;
				//NoGuestlistPanel.Visible = !goodSpotter;
				//GuestlistPanel.Visible = goodSpotter;

				try
				{
					SpotterAddressCountryName.Text = Usr.Current.AddressCountry.Name;
				}
				catch { }
				OptionsPanel.Visible = true;
			//	if (Usr.Current.AddressCountryK != 224 && Usr.Current.AddressCountryK != 225)
			//		PanelInternational.Visible = true;
				//this.DataBind();
			}
		}
		#endregion
		#region OptionsPanel
		protected Picker uiEventPicker;
		public void PanelEnabledSignUpClick(object o, System.EventArgs e)
		{
			if (Page.IsValid && this.uiEventPicker.Event != null)
			{
				Response.Redirect(this.uiEventPicker.Event.SpotterSignUpUrl);
			}
		}
		#endregion
		#region EventsPanel
		protected string PhotosHtml(Event e)
		{
			if (e.DateTime <= DateTime.Today)
				return "<a href=\"/pages/galleries/add/eventk-" + e.K.ToString() + "\">Add&nbsp;photos</a>";
			else
				return "";
		}
		protected string ReviewHtml(Event e)
		{
			if (e.DateTime <= DateTime.Today)
				return "<a href=\"" + e.UrlApp("review") + "\">Review</a>";
			else
				return "";
		}
		void BindEvents()
		{
			Query q = new Query();
			q.QueryCondition = new Q(Usr.Columns.K, Usr.Current.K);
			q.TableElement = Event.UsrSpotterJoin;
			q.OrderBy = Event.PastEventOrder;
			EventSet es = new EventSet(q);
			if (es.Count > 0)
			{
				EventsDataGrid.AllowPaging = (es.Count > EventsDataGrid.PageSize);
				EventsDataGrid.DataSource = es;
				EventsDataGrid.DataBind();
			}
			else
				EventsPanel.Visible = false;
		}
		public void EventsDataGridChangePage(object o, DataGridPageChangedEventArgs e)
		{
			EventsDataGrid.CurrentPageIndex = e.NewPageIndex;
			BindEvents();
		}
		#endregion

		#region RequestCardsPanel

		public void RequestCardsPanel_Load(object o, System.EventArgs e)
		{
			RequestCardsError2.Visible = false;
			RequestCardsError3.Visible = false;
			RequestCardsError4.Visible = false;
			if (Usr.Current != null && Usr.Current.IsSpotter)
			{
			//	if (Usr.Current.AddressCountryK != 224 && Usr.Current.AddressCountryK != 225)
			//		RequestCardsPanel.Visible = false;
			//	else
			//	{
					RequestCardsStatusLabelUpdate();
			//	}
			}
			PanelStatusNoCardsInPost.Visible = (Usr.Current != null && Usr.Current.IsSpotter && Usr.Current.CardStatus.Equals(Usr.CardStatusEnum.NeedCards) && !WeAreSendingCards);
		}
		public void RequestCards(object o, System.EventArgs e)
		{
			if (Usr.Current != null && Usr.Current.IsSpotter)
			{
				if (Usr.Current.CardStatus.Equals(Usr.CardStatusEnum.New) || Usr.Current.CardStatus.Equals(Usr.CardStatusEnum.PrintingWelcomePack))
				{
					RequestCardsError2.Visible = true;
					AnchorSkip("RequestCards");
					return;
				}
				//else if (Usr.Current.CardStatus.Equals(Usr.CardStatusEnum.CardsInPost) || Usr.Current.CardStatus.Equals(Usr.CardStatusEnum.WelcomePackInPost))
				//{
				//    RequestCardsError3.Visible = true;
				//    AnchorSkip("RequestCards");
				//    return;
				//}
				else if (Usr.Current.CardStatus.Equals(Usr.CardStatusEnum.NeedCards) || Usr.Current.CardStatus.Equals(Usr.CardStatusEnum.PrintingRefill))
				{
					RequestCardsError4.Visible = true;
					AnchorSkip("RequestCards");
					return;
				}
				else
				{
					Usr.Current.CardStatus = Usr.CardStatusEnum.NeedCards;
					Usr.Current.Update();
					if (!WeAreSendingCards)
						PanelStatusNoCardsInPost.Visible = true;
					RequestCardsStatusLabelUpdate();
					AnchorSkip("RequestCards");
				}

			}
		}
		void RequestCardsStatusLabelUpdate()
		{
			PanelStatusCardsInPost.Visible = false;
			if (Usr.Current.CardStatus.Equals(Usr.CardStatusEnum.CardsInPost) || Usr.Current.CardStatus.Equals(Usr.CardStatusEnum.WelcomePackInPost))
			{
				PanelStatusCardsInPost.Visible = true;
				RequestCardsStatusLabel.Text = "<b>Cards in the post</b> - we've posted your cards, and you should get them in a couple of days.";
			}
			else if (Usr.Current.CardStatus.Equals(Usr.CardStatusEnum.NeedCards) || Usr.Current.CardStatus.Equals(Usr.CardStatusEnum.New) || Usr.Current.CardStatus.Equals(Usr.CardStatusEnum.PrintingRefill) || Usr.Current.CardStatus.Equals(Usr.CardStatusEnum.PrintingWelcomePack))
			{
				RequestCardsStatusLabel.Text = "<b>Need cards</b> - we know you need cards, and we'll post them as soon as we can.";
			}
			else
			{
				RequestCardsStatusLabel.Text = "<b>Have cards</b> - you have enough cards.";
			}

		}
		public void ResetCards(object o, System.EventArgs e)
		{
			if (Usr.Current != null && Usr.Current.IsSpotter)
			{
				Usr.Current.CardStatus = Usr.CardStatusEnum.HaveCards;
				Usr.Current.Update();
				RequestCardsStatusLabelUpdate();
				AnchorSkip("RequestCards");
			}
		}
		#endregion

		#endregion

		#region Event sign up functions



		#region PanelPastEventConfirm
		public void PanelPastEventConfirmClick(object o, System.EventArgs e)
		{
			SignUp();
		}
		public void PanelPastEventBackClick(object o, System.EventArgs e)
		{
			ChangePanel(PanelEnabled);
		}
		#endregion

		#region SignUpEvent(), SignUp()
		void SignUpEvent()
		{
			if (CurrentEvent.IsFuture)
			{
				SignUp();
			}
			else
			{
				ChangePanel(PanelPastEventConfirm);
				PanelPastEventConfirmLabel.Text = CurrentEvent.FriendlyName;
			}
		}
		void SignUp()
		{
			//Try to sign-up to cover an event.
			if (!Usr.Current.IsSpotter)
			{
				ChangePanel(PanelIntro1);
			}
			else if (CurrentEvent.NoPhotos)
			{
				ChangePanel(PanelNoPhoto);
			}
			else
			{
				UsrEventAttended es1 = null;
				try
				{
					es1 = new UsrEventAttended(Usr.Current.K, CurrentEvent.K);
					if (es1.Spotter)
					{
						ChangePanel(PanelAlreadySignedUp);
						PanelAlreadySignedUpEventLabel.Text = CurrentEvent.FriendlyName;
						PanelAlreadySignedUpEventLink.HRef = CurrentEvent.Url();
					}
				}
				catch { }
				if (es1 == null || !es1.Spotter)
				{
					Usr.Current.AttendEvent(CurrentEvent.K, true, true, null);

					ChangePanel(PanelSignedUp);

					PanelSignedUpEventLink.InnerText = CurrentEvent.FriendlyName;
					PanelSignedUpEventLink.HRef = CurrentEvent.Url();
				}
			}
		}
		#endregion

		#endregion

		#region ChangePanel
		void ChangePanel(Panel p)
		{
			PanelEnabled.Visible = p.Equals(PanelEnabled);

			PanelNoPhoto.Visible = p.Equals(PanelNoPhoto);

			PanelChecklist.Visible = p.Equals(PanelChecklist);
			PanelIntro1.Visible = p.Equals(PanelIntro1);
			PanelSignUpForm.Visible = p.Equals(PanelSignUpForm);
			PanelSignedUp.Visible = p.Equals(PanelSignedUp);
			PanelAlreadySignedUp.Visible = p.Equals(PanelAlreadySignedUp);
			PanelPastEventConfirm.Visible = p.Equals(PanelPastEventConfirm);
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
			this.Load += new System.EventHandler(this.RequestCardsPanel_Load);
			this.Load += new System.EventHandler(this.PanelEnabled_Load);
			this.Load += new System.EventHandler(this.PanelSignUpForm_Load);
		}
		#endregion
	}
}
