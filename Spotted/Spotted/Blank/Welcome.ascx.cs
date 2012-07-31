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

namespace Spotted.Blank
{
	public partial class Welcome : BlankUserControl
	{

		#region uncheckedBuddyUsrKs, uncheckedGroupKs
		// use this while generating the page - collect unchecked values before performing new DataBind
		// then recheck checkboxes accordingly on RowDataBind
		List<int> uncheckedBuddyUsrKs = new List<int>();
		List<int> uncheckedGroupKs = new List<int>();
		#endregion

		protected void Unsubscribe(object sender, System.EventArgs e)
		{
			Usr.Current.Unsubscribe();
			Log.Increment(Log.Items.WelcomeDelete);
			Usr.SignOut();
		}
		protected void LogOff(object sender, System.EventArgs e)
		{
			Log.Increment(Log.Items.WelcomeLogOff);
			Usr.SignOut();
		}

		protected string xxSmall
		{
			get
			{
				return Vars.IE ? "xx-small" : "x-small";
			}
		}
		protected string xSmall
		{
			get
			{
				return Vars.IE ? "x-small" : "small";
			}
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn("You must be logged in to view your user preferences.");
			if (!Page.IsPostBack && !Usr.Current.IsSkeleton && !Vars.DevEnv)
				throw new DsiUserFriendlyException("You can't view this page, you've already completed your details");

			LogOffButton.Attributes["onclick"] = "return confirm('Are you sure?');";
			UnsubscribeButton.Attributes["onclick"] = "return confirm('Are you sure?');";
			EmailRegex.ValidationExpression = Cambro.Misc.RegEx.Email;
			DialingCodeDropDown.Attributes["onclick"] = "if (document.forms[0].elements['" + DialingCodeDropDown.ClientID + "'][document.forms[0].elements['" + DialingCodeDropDown.ClientID + "'].selectedIndex].value=='0') {document.getElementById('" + DialingCodeOtherSpan.ClientID + "').style.display=''}else{document.getElementById('" + DialingCodeOtherSpan.ClientID + "').style.display='none'}";

			#region Set up page... don't show options 2 and 3 to people who have come from the sign-up page unless they have verified their email

			WelcomeHeaderInvite.Visible = !Usr.Current.IsSkeletonFromSignup;
			WelcomeHeaderSignUp.Visible = Usr.Current.IsSkeletonFromSignup;

			if (ContainerPage.Url[0] == "Signup")
			{
				WelcomePart2And3.Visible = false;
				WelcomePart1Header.Visible = false;
			}

			//if (Usr.Current.IsSkeletonFromSignup && !Usr.Current.IsEmailVerified)
			//{
				//WelcomePart2And3.Visible = false;
				//WelcomePart1Header.Visible = false;
			//}
			#endregion

			if (!Page.IsPostBack)
			{
				string text = Cambro.Misc.Utility.GenRandomChars(5).ToUpper();
				string encryptedText = Cambro.Misc.Utility.Encrypt(text, DateTime.Now.AddHours(1));
				this.ViewState["HipChallengeExcryptedText"] = encryptedText;
				HipImage.Src = "/support/hipimage.aspx?a=" + encryptedText;
			}

			#region Auto add group / buddy
			uiAddedByGroupsDiv.Visible = false;
			uiAddedByUsrsDiv.Visible = false;
			try
			{
				int groupsCount = Usr.Current.GroupsWhoHavePendingInvitationsForMe.Count;
				if (groupsCount > 0)
				{
					uiAddedByGroupsDiv.Visible = true;

					if (groupsCount == 1)
					{
						uiAddedByGroupLabel.Text = "<b>" + Usr.Current.GroupsWhoHavePendingInvitationsForMe[0].FriendlyName + "</b> group";
					}
					else
					{
						uiAddedByGroupLabel.Text = "following groups";
					}

					int height = 36 + Math.Min(groupsCount, 2) * 60 + 20;
					uiAddedByGroupsDiv.Attributes["style"] = "border-top:3px solid #000000;padding-top:10px;padding-bottom:10px; height:" + height + "; overflow:auto;";

					foreach (GridViewRow gvr in uiAddedByGroupsGridView.Rows)
					{
						if (!((CheckBox)gvr.FindControl("uiCheckBox")).Checked) uncheckedGroupKs.Add((int)uiAddedByGroupsGridView.DataKeys[gvr.RowIndex].Value);
					}
					uiAddedByGroupsGridView.DataSource = Usr.Current.GroupsWhoHavePendingInvitationsForMe;
					uiAddedByGroupsGridView.DataBind();
				}
				int usrsCount = Usr.Current.UsrsWhoHavePendingBuddyRequestsForMe.Count;
				if (usrsCount > 0)
				{
					if (usrsCount == 1)
					{
						uiAddedByUsrLabel.Text = "<b>" + Usr.Current.UsrsWhoHavePendingBuddyRequestsForMe[0].NickName + "</b>";
					}
					else
					{
						uiAddedByUsrLabel.Text = "the following people";
					}

					int height = 36 + Math.Min(usrsCount, 2) * 60 + 20;
					uiAddedByUsrsDiv.Attributes["style"] = "border-top:3px solid #000000;padding-top:10px;padding-bottom:10px; height:" + height + "; overflow:auto;";

					foreach (GridViewRow gvr in uiAddedByUsrsGridView.Rows)
					{
						if (!((CheckBox)gvr.FindControl("uiCheckBox")).Checked) uncheckedBuddyUsrKs.Add((int)uiAddedByUsrsGridView.DataKeys[gvr.RowIndex].Value);
					}
					uiAddedByUsrsDiv.Visible = true;
					uiAddedByUsrsGridView.DataSource = Usr.Current.UsrsWhoHavePendingBuddyRequestsForMe;
					uiAddedByUsrsGridView.DataBind();

				}
			}
			catch
			{
				uiAddedByGroupsDiv.Visible = false;
				uiAddedByUsrsDiv.Visible = false;
			}
			#endregion

			#region Populate hometown drop-down
			int selectedHomeTown = 0;
			if (!HomeTownDropDownList.SelectedValue.Equals("0") && !HomeTownDropDownList.SelectedValue.Equals(""))
				selectedHomeTown = int.Parse(HomeTownDropDownList.SelectedValue);

			//	OrderBy o = new OrderBy(Bobs.Place.Columns.Name,OrderBy.OrderDirection.Ascending);

			if (!Page.IsPostBack)
			{
				PlaceSet ts = new PlaceSet(
					new Query(
						new And(
						new Q(Place.Columns.Enabled, true),
						Country.PlaceFilterQ),
						new OrderBy(Bobs.Place.Columns.Name, OrderBy.OrderDirection.Ascending)
					)
				);
				HomeTownDropDownList.DataSource = ts;
				HomeTownDropDownList.DataValueField = "K";
				HomeTownDropDownList.DataTextField = "Name";
				HomeTownDropDownList.DataBind();
			}

			if (selectedHomeTown > 0)
			{
				try
				{
					HomeTownDropDownList.SelectedValue = selectedHomeTown.ToString();
				}
				catch
				{
					try
					{
						Place p = new Place(selectedHomeTown);
						HomeTownDropDownList.Items.Insert(1, new ListItem(p.Name, p.K.ToString()));
						HomeTownDropDownList.SelectedValue = selectedHomeTown.ToString();
					}
					catch { }
				}
			}
			#endregion

			#region Populate favourite music drop-down
			int selectedFavouriteMusic = 0;
			if (!FavouriteMusicDropDownList.SelectedValue.Equals("0") && !FavouriteMusicDropDownList.SelectedValue.Equals(""))
				selectedFavouriteMusic = int.Parse(FavouriteMusicDropDownList.SelectedValue);


			if (!Page.IsPostBack)
			{
				MusicTypeSet mts = new MusicTypeSet(
					new Query(
						new Or(
						new Q(MusicType.Columns.ParentK, 1),
						new Q(MusicType.Columns.ParentK, 0)
						)
						,
						new OrderBy(MusicType.Columns.Order)
					)
				);
				FavouriteMusicDropDownList.DataSource = mts;
				FavouriteMusicDropDownList.DataValueField = "K";
				FavouriteMusicDropDownList.DataTextField = "DescriptiveText";
				FavouriteMusicDropDownList.DataBind();
			}

			if (selectedFavouriteMusic > 0)
			{
				FavouriteMusicDropDownList.SelectedValue = selectedFavouriteMusic.ToString();
			}

			#endregion

			if (!Page.IsPostBack)
			{
				FirstName.Text = Usr.Current.FirstName;
				LastName.Text = Usr.Current.LastName;
				NickName.Text = Usr.Current.NickName;
				Email.Text = Usr.Current.Email;
				SexMale.Checked = Usr.Current.IsMale;
				SexFemale.Checked = Usr.Current.IsFemale;
				SendSpottedEmails.Checked = Usr.Current.SendSpottedEmails;
				SendSpottedTexts.Checked = Usr.Current.SendSpottedTexts;
				SendFlyers.Checked = Usr.Current.SendFlyers;
				SendInvites.Checked = Usr.Current.SendInvites;
				IsDjYes.Checked = Usr.Current.IsDj.HasValue && Usr.Current.IsDj.Value;
				IsDjNo.Checked = Usr.Current.IsDj.HasValue && !Usr.Current.IsDj.Value;



				#region Initialise hometown drop-down
				if (Usr.Current.HomePlaceK > 0)
				{
					try
					{
						HomeTownDropDownList.SelectedValue = Usr.Current.HomePlaceK.ToString();
					}
					catch
					{
						HomeTownDropDownList.Items.Insert(1, new ListItem(Usr.Current.Home.Name, Usr.Current.HomePlaceK.ToString()));
						HomeTownDropDownList.SelectedValue = Usr.Current.HomePlaceK.ToString();
					}
				}
				#endregion
				#region Initialise favourite music drop-down
				if (Usr.Current.FavouriteMusicTypeK > 0)
				{
					FavouriteMusicDropDownList.SelectedValue = Usr.Current.FavouriteMusicTypeK.ToString();
				}
				#endregion
				#region Initialise mobile number box
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
				#endregion

			}

			DialingCodeOtherSpan.Style["display"] = DialingCodeDropDown.SelectedValue.Equals("0") ? null : "none";

			this.DataBind();
			if (!Page.IsPostBack)
			{
				HomeTownDropDownList.Items.Insert(0, new ListItem(" ", "0"));
				FavouriteMusicDropDownList.Items.Insert(0, new ListItem(" ", "0"));
			}
		}

		protected void uiAddedByUsrsGridView_RowDataBound(object o, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				CheckBox cb = (CheckBox)e.Row.FindControl("uiCheckBox");
				cb.Text = " Add " + ((Usr)e.Row.DataItem).NickName + " to my buddy list.";

				cb.Checked = !uncheckedBuddyUsrKs.Contains(((Usr)e.Row.DataItem).K);
			}
		}
		protected void uiAddedByGroupsGridView_RowDataBound(object o, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				CheckBox cb = (CheckBox)e.Row.FindControl("uiCheckBox");
				cb.Text = " Join the <b>" + ((Group)e.Row.DataItem).FriendlyName + "</b> group.";

				cb.Checked = !uncheckedGroupKs.Contains(((Group)e.Row.DataItem).K);
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
					new Q(Usr.Columns.K, QueryOperator.NotEqualTo, Usr.Current.K)
				);
				q.ReturnCountOnly = true;
				UsrSet us = new UsrSet(q);
				e.IsValid = us.Count == 0;
			}
		}
		private void Page_PreRender(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Usr.Current.IsSkeletonFromSignup)
				{
					PasswordTr.Visible = false;
				}
			}
			else
			{
				Password1.Attributes["value"] = Password1.Text;
				Password2.Attributes["value"] = Password2.Text;
			}
		}
		public void HipVal(object o, ServerValidateEventArgs e)
		{
			string realText = Cambro.Misc.Utility.Decrypt((string)this.ViewState["HipChallengeExcryptedText"]);
			e.IsValid = HipChallengeTextBox.Text.ToUpper().Equals(realText);
			if (Page.IsPostBack && !e.IsValid)
			{
				string text = Cambro.Misc.Utility.GenRandomChars(5).ToUpper();
				string encryptedText = Cambro.Misc.Utility.Encrypt(text, DateTime.Now.AddHours(1));
				this.ViewState["HipChallengeExcryptedText"] = encryptedText;
				HipImage.Src = "/support/hipimage.aspx?a=" + encryptedText;
				HipChallengeTextBox.Text = string.Empty;
			}
		}
		public void TermsVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = TermsCheckbox.Checked;
		}
		public void HomeTownVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = !HomeTownDropDownList.SelectedValue.Equals("0");
		}
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

		public void ServerValidatePassword(object o, ServerValidateEventArgs e)
		{
			e.IsValid = (Password1.Text == Password2.Text);
		}
		public void PrefsUpdateClick(object o, System.EventArgs e)
		{
			Page.Validate();
			bool sendVerifyEmail = false;
			if (Page.IsValid)
			{
				#region Handle change of email address
				if (Usr.Current.Email != Email.Text)
				{
					//Check for duplicate email addresses in the database
					Query q = new Query();
					q.QueryCondition = new Q(Usr.Columns.Email, Email.Text);
					q.ReturnCountOnly = true;
					UsrSet ds = new UsrSet(q);

					if (ds.Count == 0)
					{
						//No duplicate - update email address
						Usr.Current.AdminNote += "\nThis user changed their email address from " + Usr.Current.Email + " to " + Email.Text + " on " + DateTime.Now.ToString();
						Usr.Current.Email = Email.Text;
						Usr.Current.EmailDateTime = DateTime.Now;
						if (HttpContext.Current != null)
							Usr.Current.EmailIp = Utilities.TruncateIp(HttpContext.Current.Request.ServerVariables["REMOTE_HOST"]);
						Usr.Current.IsEmailVerified = false;
						Usr.Current.IsEmailBroken = false;
						sendVerifyEmail = true;
					}
					else
					{
						//Duplicate - display error
						EmailDuplicateValidator.IsValid = false;
					}
				}
				#endregion
				#region Handle phone number entry
				System.Text.RegularExpressions.Regex rNumbers = new System.Text.RegularExpressions.Regex("[^0123456789]");
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
				#endregion

				//Database will only update if all validators are valid
				if (Page.IsValid)
				{
					Usr.Current.FirstName = Cambro.Web.Helpers.StripHtml(FirstName.Text).Trim();
					Usr.Current.LastName = Cambro.Web.Helpers.StripHtml(LastName.Text).Trim();
					string nick = Usr.GetCompliantNickName(NickName.Text);
					Usr.Current.NickName = nick;
					Usr.Current.IsSkeleton = false;

					if (!Usr.Current.Mobile.Equals(fullMobile))
						Usr.Current.AdminNote += "\n\nUsr has changed mobile number from " + Usr.Current.Mobile + " to: " + fullMobile + " on " + DateTime.Now.ToString() + ".\n";

					Usr.Current.Mobile = fullMobile;
					Usr.Current.MobileCountryCode = dialingCode;
					Usr.Current.MobileNumber = mobileNumber;
					Usr.Current.IsMale = SexMale.Checked;
					Usr.Current.IsFemale = SexFemale.Checked;
					Usr.Current.DateOfBirth = new DateTime(int.Parse(DateOfBirthYear.Text), int.Parse(DateOfBirthMonth.Text), int.Parse(DateOfBirthDay.Text));
					Usr.Current.SendSpottedEmails = SendSpottedEmails.Checked;
					Usr.Current.SendSpottedTexts = SendSpottedTexts.Checked;
					Usr.Current.SendFlyers = SendFlyers.Checked;
					Usr.Current.SendInvites = SendInvites.Checked;
					Usr.Current.LegalTermsUser2 = true;
					Usr.Current.IsDj = IsDjYes.Checked;

					#region Update hometown and add UsrPlaceVisit record for this place
					Place p = new Place(int.Parse(HomeTownDropDownList.SelectedValue));
					if (Usr.Current.HomePlaceK != p.K)
					{
						Usr.Current.HomePlaceK = p.K;

						try
						{
							UsrPlaceVisit upv = new UsrPlaceVisit(Usr.Current.K, p.K);
						}
						catch
						{
							UsrPlaceVisit upv = new UsrPlaceVisit();
							upv.UsrK = Usr.Current.K;
							upv.PlaceK = p.K;
							upv.Update();
						}
					}
					Usr.Current.UpdatePlacesVisitCount(false);
					#endregion

					#region Update favourite music and add UsrMusicTypeFavourite record for this musictype
					MusicType mt = new MusicType(int.Parse(FavouriteMusicDropDownList.SelectedValue));
					if (Usr.Current.FavouriteMusicTypeK != mt.K)
					{
						Usr.Current.FavouriteMusicTypeK = mt.K;

						Prefs.Current["MusicPref"] = mt.K;

						try
						{
							UsrMusicTypeFavourite newMtf = new UsrMusicTypeFavourite(Usr.Current.K, mt.K);
						}
						catch
						{
							UsrMusicTypeFavourite newMtf = new UsrMusicTypeFavourite();
							newMtf.UsrK = Usr.Current.K;
							newMtf.MusicTypeK = mt.K;
							newMtf.Update();
						}
					}
					Usr.Current.UpdateMusicTypesFavouriteCount(false);
					#endregion

					if (!Usr.Current.IsSkeletonFromSignup && Password2.Text.Length > 0)
					{
						//Remove all saved cards...
						Usr.Current.DeleteAllSavedCards();
						Usr.Current.SetPassword(Password2.Text.Trim(), false);
					}

					Usr.Current.Update();

					if (Usr.Current.GroupsWhoHavePendingInvitationsForMe.Count > 0)
					{
						foreach (GridViewRow gvr in uiAddedByGroupsGridView.Rows)
						{
							if (((CheckBox)gvr.FindControl("uiCheckBox")).Checked)
							{
								int groupK = (int)uiAddedByGroupsGridView.DataKeys[gvr.RowIndex].Value;
								try
								{
									Group g = new Group(groupK);
									GroupUsr gu = g.GetGroupUsr(Usr.Current);
									if (Bobs.Group.AllowJoinRequest(Usr.Current, g, gu))
										g.Join(Usr.Current, gu);
								}
								catch { }
							}
						}
					}
					if (Usr.Current.UsrsWhoHavePendingBuddyRequestsForMe.Count > 0)
					{
						foreach (GridViewRow gvr in uiAddedByUsrsGridView.Rows)
						{
							if (((CheckBox)gvr.FindControl("uiCheckBox")).Checked)
							{
								int buddyUsrK = (int)uiAddedByUsrsGridView.DataKeys[gvr.RowIndex].Value;
								try
								{
									Usr.Current.AddBuddy(new Usr(buddyUsrK), Usr.AddBuddySource.WelcomePage, Buddy.BuddyFindingMethod.Nickname, null);
								}
								catch (Exception ex) { SpottedException.TryToSaveExceptionAndChildExceptions(ex, HttpContext.Current, Usr.Current, Visit.Current, "", "Welcome page", "", 0, null); }
							}
						}
					}

					#region Send email verify email, if needed
					if (sendVerifyEmail)
					{
						Mailer mail = new Mailer();
						mail.SendEvenIfUnverifiedOrBroken = true;
						mail.Subject = "You changed your DontStayIn email address...";
						mail.Body = @"<h1>You changed your email address...</h1><p>Please click the following link to verify your email address and allow posting to our discussion boards:</p>
<p align=""center"" style=""padding:8px 0px 9px 0px;""><a href=""[LOGIN]"" style=""font-size:14px;font-weight:bold;"">Click here to verify your email</a></p>";
						mail.To = Usr.Current.Email;
						mail.UsrRecipient = Usr.Current;
						mail.TemplateType = Mailer.TemplateTypes.AnotherSiteUser;
						mail.Send();
					}
					#endregion

					Log.Increment(Log.Items.WelcomeSignUp);

					if (Usr.Current.AddedByGroupK > 0)
					{
						if (Request.QueryString["Url"] != null && Request.QueryString["Url"].Length > 0)
							Response.Redirect(Request.QueryString["Url"]);
						else
							Response.Redirect(Usr.Current.AddedByGroup.Url());
					}
					else
					{
						if (Request.QueryString["Url"] != null && Request.QueryString["Url"].Length > 0)
							Response.Redirect("/popup/mixmag?url=" + HttpUtility.UrlEncode(Request.QueryString["Url"]));
						else
							Response.Redirect("/popup/mixmag");
					}
				}
			}
		}

	}
}
