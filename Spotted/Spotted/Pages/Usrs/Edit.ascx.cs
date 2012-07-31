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

namespace Spotted.Pages.Usrs
{
	public partial class Edit : UsrUserControl
	{
		protected override void Page_Init(object sender, System.EventArgs e)
		{
			base.Page_Init(sender, e);
		}
		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn("You must be logged in to view your user preferences.");
			if (Usr.Current.K != ThisUsr.K && !Usr.Current.IsAdmin)
                throw new DsiUserFriendlyException("You can't use this page");

			// Put user code to initialize the page here
			SuccessDiv.Visible = false;

			if (!Page.IsPostBack && Request.QueryString["done"] != null && Request.QueryString["done"] == "1")
				SuccessDiv.Visible = true;

			EmailRegex.ValidationExpression = Cambro.Misc.RegEx.Email;
			//PostcodeRegex.ValidationExpression = Cambro.Misc.RegEx.Postcode;
			//TelephoneRegex.ValidationExpression=Cambro.Misc.RegEx.Telephone;
			

			#region Hometown
			//int selectedHomeTown = 0;
			//if (!HomeTownDropDownList.SelectedValue.Equals("0") && !HomeTownDropDownList.SelectedValue.Equals(""))
			//    selectedHomeTown = int.Parse(HomeTownDropDownList.SelectedValue);

			//PlaceSet ts = new PlaceSet(
			//    new Query(
			//        new And(
			//        new Q(Place.Columns.Enabled, true),
			//        Country.PlaceFilterQ),
			//        new OrderBy(Bobs.Place.Columns.Name, OrderBy.OrderDirection.Ascending)
			//    ));
			//HomeTownDropDownList.DataSource = ts;
			//HomeTownDropDownList.DataValueField = "K";
			//HomeTownDropDownList.DataTextField = "Name";
			//HomeTownDropDownList.DataBind();
			//HomeTownDropDownList.Items.Insert(0, new ListItem("", "0"));

			//if (selectedHomeTown > 0)
			//{
			//    try
			//    {
			//        HomeTownDropDownList.SelectedValue = selectedHomeTown.ToString();
			//    }
			//    catch
			//    {
			//        try
			//        {
			//            Place p = new Place(selectedHomeTown);
			//            HomeTownDropDownList.Items.Insert(1, new ListItem(p.Name, p.K.ToString()));
			//            HomeTownDropDownList.SelectedValue = selectedHomeTown.ToString();
			//        }
			//        catch { }
			//    }
			//}
			#endregion

			#region FavouriteMusic
			int selectedFavouriteMusic = 0;
			if (!FavouriteMusicDropDownList.SelectedValue.Equals("0") && !FavouriteMusicDropDownList.SelectedValue.Equals(""))
				selectedFavouriteMusic = int.Parse(FavouriteMusicDropDownList.SelectedValue);


			MusicTypeSet mts = new MusicTypeSet(
				new Query(
				new Or(
				new Q(MusicType.Columns.ParentK, 1),
				new Q(MusicType.Columns.ParentK, 0)
				)
				,
				new OrderBy(MusicType.Columns.Order)
			));
			FavouriteMusicDropDownList.DataSource = mts;
			FavouriteMusicDropDownList.DataValueField = "K";
			FavouriteMusicDropDownList.DataTextField = "DescriptiveText";
			FavouriteMusicDropDownList.DataBind();
			FavouriteMusicDropDownList.Items.Insert(0, new ListItem("", "0"));

			if (selectedFavouriteMusic > 0)
			{
				FavouriteMusicDropDownList.SelectedValue = selectedFavouriteMusic.ToString();
			}

			#endregion

			DialingCodeDropDown.Attributes["onchange"] = "if (document.forms[0].elements['" + DialingCodeDropDown.ClientID + "'][document.forms[0].elements['" + DialingCodeDropDown.ClientID + "'].selectedIndex].value=='0') {document.getElementById('" + DialingCodeOtherSpan.ClientID + "').style.display=''}else{document.getElementById('" + DialingCodeOtherSpan.ClientID + "').style.display='none'}";

			if (!Page.IsPostBack)
			{
				SetupCountryDropDownList();

				ChangePanel(PrefsUpdatePanel);

				FirstName.Text = ThisUsr.FirstName;
				LastName.Text = ThisUsr.LastName;
				NickName.Text = ThisUsr.NickName;
				Email.Text = ThisUsr.Email;
				//Postcode.Text = ThisUsr.AddressPostcode;
				SexMale.Checked = ThisUsr.IsMale;
				SexFemale.Checked = ThisUsr.IsFemale;
				IsDjYes.Checked = ThisUsr.IsDj.HasValue && ThisUsr.IsDj.Value;
				IsDjNo.Checked = ThisUsr.IsDj.HasValue && !ThisUsr.IsDj.Value;

				if (ThisUsr.HomePlaceK > 0 && ThisUsr.Home != null)
					HomeTownPlacePicker.Place = ThisUsr.Home;
				else if (ThisUsr.AddressCountryK > 0 && ThisUsr.AddressCountry != null)
					HomeTownPlacePicker.Country = ThisUsr.AddressCountry;
				else if (Prefs.Current["HomeCountryK"].IsInt)
				{
					try
					{
						HomeTownPlacePicker.Country = new Country(Prefs.Current["HomeCountryK"]);
					}
					catch { }
				}
				else if (Country.Current != null)
					HomeTownPlacePicker.Country = Country.Current;


				AddressAreaTextBox.Text = ThisUsr.AddressArea;
				AddressCountyTextBox.Text = ThisUsr.AddressCounty;
				if(ThisUsr.AddressCountryK > 0)
					AddressCountryDropDownList.SelectedValue = ThisUsr.AddressCountryK.ToString();
				else
					AddressCountryDropDownList.SelectedValue = Country.FilterK.ToString();

				AddressPostcodeTextBox.Text = ThisUsr.AddressPostcode;
				AddressStreetTextBox.Text = ThisUsr.AddressStreet;
				AddressTownTextBox.Text = ThisUsr.AddressTown;

                if (!ThisUsr.DateOfBirth.Equals(DateTime.MinValue))
				{
					DateOfBirthDay.Text = ThisUsr.DateOfBirth.Day.ToString();
					DateOfBirthMonth.Text = ThisUsr.DateOfBirth.Month.ToString();
					DateOfBirthYear.Text = ThisUsr.DateOfBirth.Year.ToString();
				}
				
				//if (ThisUsr.HomePlaceK > 0)
				//{
				//    try
				//    {
				//        HomeTownDropDownList.SelectedValue = ThisUsr.HomePlaceK.ToString();
				//    }
				//    catch
				//    {
				//        HomeTownDropDownList.Items.Insert(1, new ListItem(ThisUsr.Home.Name, ThisUsr.HomePlaceK.ToString()));
				//        HomeTownDropDownList.SelectedValue = ThisUsr.HomePlaceK.ToString();
				//    }
				//}
				if (ThisUsr.FavouriteMusicTypeK > 0)
				{
					FavouriteMusicDropDownList.SelectedValue = ThisUsr.FavouriteMusicTypeK.ToString();
				}
				if (ThisUsr.Mobile.Length > 0)
				{
					if (
						ThisUsr.MobileCountryCode.Equals("44") ||
						ThisUsr.MobileCountryCode.Equals("61") ||
						ThisUsr.MobileCountryCode.Equals("33") ||
						ThisUsr.MobileCountryCode.Equals("49") ||
						ThisUsr.MobileCountryCode.Equals("353") ||
						ThisUsr.MobileCountryCode.Equals("39") ||
						ThisUsr.MobileCountryCode.Equals("34") ||
						ThisUsr.MobileCountryCode.Equals("1")
						)
					{
						DialingCodeDropDown.SelectedValue = ThisUsr.MobileCountryCode;
						DialingCodeOtherSpan.Style["display"] = "none";
					}
					else
					{
						DialingCodeDropDown.SelectedValue = "0";
						DialingCodeOtherSpan.Style["display"] = null;
						DialingCodeOther.Text = ThisUsr.MobileCountryCode;
					}
					MobileNumber.Text = ThisUsr.MobileNumber;
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
		}

		private void SetupCountryDropDownList()
		{
			AddressCountryDropDownList.DataSource = Country.Countries();
			AddressCountryDropDownList.DataTextField = "Name";
			AddressCountryDropDownList.DataValueField = "K";
			AddressCountryDropDownList.DataBind();
        }

        #region Validators
		//public void HomeTownVal(object o, ServerValidateEventArgs e)
		//{
		//    e.IsValid = !HomeTownDropDownList.SelectedValue.Equals("0");
		//}
		public void FavouriteMusicVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = !FavouriteMusicDropDownList.SelectedValue.Equals("0");
		}
		public void SexVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = (SexMale.Checked || SexFemale.Checked);
		}
		public void IsDjVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = (IsDjYes.Checked || IsDjNo.Checked);
		}
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
		public void DateOfBirthRangeVal(object o, ServerValidateEventArgs e)
		{
			try
			{
				int day = int.Parse(DateOfBirthDay.Text);
				int month = int.Parse(DateOfBirthMonth.Text);
				int year = int.Parse(DateOfBirthYear.Text);
				DateTime d = new DateTime(year, month, day);
				if (d > DateTime.Now.AddYears(-13))
					e.IsValid = false;
				else if (d < DateTime.Now.AddYears(-120))
					e.IsValid = false;
				else
					e.IsValid = true;
			}
			catch
			{
				e.IsValid = true;
			}
		}

		public void NickNameDuplicateVal(object o, ServerValidateEventArgs e)
		{
			string nick = Usr.GetCompliantNickName(NickName.Text);
			NickName.Text = nick;
			if (!Usr.NickNameRegex.IsMatch(nick))
				e.IsValid = false;
			else
			{
				Query q = new Query();
				q.QueryCondition = new And(
					new Q(Usr.Columns.NickName, nick),
					new Q(Usr.Columns.K, QueryOperator.NotEqualTo, ThisUsr.K)
				);
				q.ReturnCountOnly = true;
				UsrSet us = new UsrSet(q);
				e.IsValid = us.Count == 0;
			}
        }
        #endregion
        public void PrefsUpdateClick(object o, System.EventArgs e)
		{
			string PreviousUrlFilterPart = ThisUsr.UrlFilterPart;
			ChangePanel(PrefsUpdatePanel);
			Page.Validate();
			//bool doUpdate = false;
			bool sendVerifyEmail = false;
			if (Page.IsValid)
			{
				if (ThisUsr.Email != Email.Text)
				{
					//Email has changed - check email

					//Check for duplicates email addresses in the database
					UsrSet ds = new UsrSet(new Query(new Q(Usr.Columns.Email, Email.Text)));

					if (ds.Count == 0)
					{
						//No duplicate - update email address
						ThisUsr.AdminNote += "\nThis user changed their email address from " + ThisUsr.Email + " to " + Email.Text + " on " + DateTime.Now.ToString();
						ThisUsr.Email = Email.Text;
						ThisUsr.EmailDateTime = DateTime.Now;
						if (HttpContext.Current != null)
							ThisUsr.EmailIp = Utilities.TruncateIp(HttpContext.Current.Request.ServerVariables["REMOTE_HOST"]);
						ThisUsr.IsEmailVerified = false;
						ThisUsr.IsEmailBroken = false;
						sendVerifyEmail = true;
						//doUpdate = true;
					}
					else
					{
						//Duplicate - display error
						emailDuplicateValidator.IsValid = false;
					}
				}
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

				string nick = Usr.GetCompliantNickName(NickName.Text);
                if (ThisUsr.NickName != nick && nick.ToUpper().Contains("-DSI") && !ThisUsr.IsAdmin)
                {
                    throw new DsiUserFriendlyException("Invalid user name. Please try another.");
                }

				//Database will only update if all validators are valid and doUpdate has been set to true by a change
				if (Page.IsValid)
				{
					ThisUsr.FirstName = Cambro.Web.Helpers.StripHtml(FirstName.Text);
					ThisUsr.LastName = Cambro.Web.Helpers.StripHtml(LastName.Text);
					if (NickName.Text.Length > 20)
                        throw new DsiUserFriendlyException("Nickname must be 20 chars or less!");

					ThisUsr.NickName = nick;

					if (!ThisUsr.Mobile.Equals(fullMobile))
						ThisUsr.AdminNote += "\n\nUsr has changed mobile number from " + ThisUsr.Mobile + " to: " + fullMobile + " on " + DateTime.Now.ToString() + ".\n";

					ThisUsr.Mobile = fullMobile;
					ThisUsr.MobileCountryCode = dialingCode;
					ThisUsr.MobileNumber = mobileNumber;

					ThisUsr.AddressArea = Cambro.Web.Helpers.StripHtml(AddressAreaTextBox.Text);
					ThisUsr.AddressPostcode = Cambro.Web.Helpers.StripHtml(AddressPostcodeTextBox.Text);
					ThisUsr.AddressCountryK = Convert.ToInt32(AddressCountryDropDownList.SelectedValue);
					ThisUsr.AddressCounty = Cambro.Web.Helpers.StripHtml(AddressCountyTextBox.Text);
					ThisUsr.AddressStreet = Cambro.Web.Helpers.StripHtml(AddressStreetTextBox.Text);
					ThisUsr.AddressTown = Cambro.Web.Helpers.StripHtml(AddressTownTextBox.Text);
					
					ThisUsr.IsMale = SexMale.Checked;
					ThisUsr.IsFemale = SexFemale.Checked;
					ThisUsr.IsDj = IsDjYes.Checked;
					ThisUsr.DateOfBirth = new DateTime(int.Parse(DateOfBirthYear.Text), int.Parse(DateOfBirthMonth.Text), int.Parse(DateOfBirthDay.Text));
					
					//Place p = new Place(int.Parse(HomeTownDropDownList.SelectedValue));
					Place p = HomeTownPlacePicker.Place;
					if (ThisUsr.HomePlaceK != p.K)
					{
						ThisUsr.HomePlaceK = p.K;

						ThisUsr.Home = null;

						try
						{
							UsrPlaceVisit upv = new UsrPlaceVisit(ThisUsr.K, p.K);
						}
						catch
						{
							UsrPlaceVisit upv = new UsrPlaceVisit();
							upv.UsrK = ThisUsr.K;
							upv.PlaceK = p.K;
							upv.Update();
						}
						ThisUsr.UpdatePlacesVisitCount(false);
					}
					
					if (ThisUsr.K == Usr.Current.K && !ThisUsr.IsOfLegalDrinkingAgeInHomeCountry)
						Prefs.Current.Remove("Drink");

					bool changeMusicPref = false;
					MusicType mt = new MusicType(int.Parse(FavouriteMusicDropDownList.SelectedValue));
					if (ThisUsr.FavouriteMusicTypeK != mt.K)
					{
						changeMusicPref = true;
						ThisUsr.FavouriteMusicTypeK = mt.K;
					}

					if (sendVerifyEmail)
					{
						ThisUsr.LoginString = Cambro.Misc.Utility.GenRandomText(6);
					}

					ThisUsr.Update();
					Usr.Current = null;

					if (changeMusicPref)
					{
						if (ThisUsr.K == Usr.Current.K)
							Prefs.Current["MusicPref"] = mt.K;

						try
						{
							UsrMusicTypeFavourite newMtf = new UsrMusicTypeFavourite(ThisUsr.K, mt.K);
						}
						catch
						{
							UsrMusicTypeFavourite newMtf = new UsrMusicTypeFavourite();
							newMtf.UsrK = ThisUsr.K;
							newMtf.MusicTypeK = mt.K;
							newMtf.Update();
						}
						ThisUsr.UpdateMusicTypesFavouriteCount(true);
					}


					SuccessDiv.Visible = true;
					if (sendVerifyEmail)
					{
						Mailer mail = new Mailer();
						mail.Subject = "You changed your DontStayIn registered email address...";
						mail.Body = @"<h1>You changed your email address...</h1><p>Please click the following link to verify your email address and allow posting to our discussion boards:</p>
<p align=""center"" style=""padding:8px 0px 9px 0px;""><a href=""[LOGIN]"" style=""font-size:14px;font-weight:bold;"">Click here to verify your email</a></p>";
						mail.To = ThisUsr.Email;
						mail.UsrRecipient = ThisUsr;
						mail.TemplateType = Mailer.TemplateTypes.AnotherSiteUser;
						mail.SendEvenIfUnverifiedOrBroken = true;
						mail.Send();
					}
					if (Request.QueryString["Url"] != null && Request.QueryString["Url"].Length > 0)
						Response.Redirect(Request.QueryString["Url"]);
					else
						Response.Redirect(ThisUsr.UrlApp("edit") + "?done=1");

					//if (!PreviousUrlFilterPart.Equals(ThisUsr.UrlFilterPart))
						//Response.Redirect(ThisUsr.UrlApp("edit"));
				}
			}
		}
		protected void ChangePanel(Panel panel)
		{
			if (panel == PrefsUpdatePanel)
			{
				PrefsUpdatePanel.Visible = true;
				Page.ClientScript.RegisterHiddenField("__EVENTTARGET", PrefsUpdateButton.UniqueID);
			}
			else
			{
				PrefsUpdatePanel.Visible = false;
			}
		}
	}
}
