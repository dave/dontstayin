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

namespace Spotted.Pages.Groups
{
	public partial class Edit : DsiUserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn();
			ContainerPage.SetPageTitle("Add a new group");
			

			if (!Page.IsPostBack)
			{
				#region Security for editing
				if (IsEdit)
				{
					GroupUsr gu = CurrentGroup.GetGroupUsr(Usr.Current);
					if (!Usr.Current.CanGroupOwner(gu))
					{
						throw new Exception("You can't edit this group!");
					}
				}
				#endregion
				#region Choose initial panel
				if (IsEdit && CurrentGroup.BrandK > 0)
				{
					if (Mode.Equals(Modes.Location))
						ChangePanel(PanelLocation);
					else if (Mode.Equals(Modes.MusicType))
						ChangePanel(PanelMusicType);
					else if (Mode.Equals(Modes.Details))
						ChangePanel(PanelDetails);
					else if (Mode.Equals(Modes.Membership))
						ChangePanel(PanelMembership);
					else if (Mode.Equals(Modes.Pic))
						ChangePanel(PanelPic);
					else
						ChangePanel(PanelLocation);
				}
				else
				{
					if (Mode.Equals(Modes.Theme))
						ChangePanel(PanelTheme);
					else if (Mode.Equals(Modes.Location))
						ChangePanel(PanelLocation);
					else if (Mode.Equals(Modes.MusicType))
						ChangePanel(PanelMusicType);
					else if (Mode.Equals(Modes.Details))
						ChangePanel(PanelDetails);
					else if (Mode.Equals(Modes.Membership))
						ChangePanel(PanelMembership);
					else if (Mode.Equals(Modes.Private))
						ChangePanel(PanelPrivate);
					else if (Mode.Equals(Modes.Pic) && IsEdit)
						ChangePanel(PanelPic);
					else
						ChangePanel(PanelTheme);
				}
				#endregion
			}
		}
		public void Page_PreRender(object o, System.EventArgs e)
		{
			ContainerPage.ViewStatePublic["GroupDuplicateGuid"] = Guid.NewGuid();
		}

		#region GroupIntro
		protected HtmlAnchor OptionsGroupAnchor;
		protected HtmlGenericControl OptionsMenuP, OptionsInviteP, EditOptionsP;
		protected Spotted.CustomControls.GroupIntro GroupIntro;
		private void GroupIntro_Load(object sender, System.EventArgs e)
		{
			if (IsEdit)
			{
				OptionsGroupAnchor.HRef = CurrentGroup.Url();
				OptionsGroupAnchor.InnerText = CurrentGroup.FriendlyName;

				ArrayList sb = new ArrayList();

				if (HasOptions)
				{
					sb.Add("<a href=\"" + CurrentGroup.UrlApp("admin", "mode", "options") + "\">member</a>");
				}
				sb.Add("<a href=\"" + CurrentGroup.UrlApp("admin", "mode", "moderator") + "\">moderator</a>");
				sb.Add("<a href=\"" + CurrentGroup.UrlApp("admin", "mode", "news") + "\">news</a>");
				sb.Add("<a href=\"" + CurrentGroup.UrlApp("admin", "mode", "membership") + "\">membership</a>");
				sb.Add("<a href=\"" + CurrentGroup.UrlApp("admin", "mode", "owner") + "\">owner</a>");

				if (sb.Count > 0)
				{
					OptionsMenuP.InnerHtml = "";
					foreach (string s in sb)
					{
						OptionsMenuP.InnerHtml += (OptionsMenuP.InnerHtml.Length > 0 ? " | " : "") + s;
					}
					OptionsMenuP.InnerHtml = "Options: " + OptionsMenuP.InnerHtml;
				}
				else
					OptionsMenuP.Visible = false;


				if (CurrentGroup.CanMember(Usr.Current, CurrentGroupUsr))
				{
					OptionsInviteP.InnerHtml = "Invite: ";
					if (Usr.Current.HasBuddiesFull)
					{
						OptionsInviteP.InnerHtml += "<a href=\"" + CurrentGroup.UrlApp("admin", "mode", "buddies") + "\">my buddies</a> | ";
					}
					OptionsInviteP.InnerHtml += "<a href=\"" + CurrentGroup.UrlApp("admin", "mode", "email") + "\">by email</a>";
				}
				else
					OptionsInviteP.Visible = false;

			}
			else
				GroupIntro.Visible = false;

		}
		private void GroupIntro_PreRender(object sender, System.EventArgs e)
		{
			if (IsEdit)
			{
				EditOptionsP.InnerHtml = "Edit group: ";
				if (PanelTheme.Visible)
					EditOptionsP.InnerHtml += "<b>theme</b> | ";
				else if (CurrentGroup.BrandK == 0)
					EditOptionsP.InnerHtml += "<a href=\"" + CurrentGroup.UrlApp("edit", "mode", "theme") + "\">theme</a> | ";

				if (PanelLocation.Visible)
					EditOptionsP.InnerHtml += "<b>location</b> | ";
				else
					EditOptionsP.InnerHtml += "<a href=\"" + CurrentGroup.UrlApp("edit", "mode", "location") + "\">location</a> | ";

				if (PanelMusicType.Visible)
					EditOptionsP.InnerHtml += "<b>music type</b> | ";
				else
					EditOptionsP.InnerHtml += "<a href=\"" + CurrentGroup.UrlApp("edit", "mode", "musictype") + "\">music type</a> | ";

				if (PanelDetails.Visible)
					EditOptionsP.InnerHtml += "<b>details</b> | ";
				else
					EditOptionsP.InnerHtml += "<a href=\"" + CurrentGroup.UrlApp("edit", "mode", "details") + "\">details</a> | ";

				if (PanelMembership.Visible)
					EditOptionsP.InnerHtml += "<b>membership</b> | ";
				else
					EditOptionsP.InnerHtml += "<a href=\"" + CurrentGroup.UrlApp("edit", "mode", "membership") + "\">membership</a> | ";

				if (PanelPrivate.Visible)
					EditOptionsP.InnerHtml += "<b>privacy</b> | ";
				else if (CurrentGroup.BrandK == 0)
					EditOptionsP.InnerHtml += "<a href=\"" + CurrentGroup.UrlApp("edit", "mode", "private") + "\">privacy</a> | ";

				if (PanelPic.Visible)
					EditOptionsP.InnerHtml += "<b>pic</b>";
				else
					EditOptionsP.InnerHtml += "<a href=\"" + CurrentGroup.UrlApp("edit", "mode", "pic") + "\">pic</a>";
			}
		}
		public bool HasOptions
		{
			get
			{
				return (CurrentGroupUsr != null && CurrentGroupUsr.IsMember);
			}
		}
		#endregion

		#region PanelTheme
		protected Panel PanelTheme;
		protected HtmlGenericControl PanelThemeSaveP;
		protected RadioButtonList ThemesRadioButtonList;
		#region PanelTheme_Load
		private void PanelTheme_Load(object sender, System.EventArgs e)
		{
			PanelThemeSaveP.Visible = IsEdit;
			if (!Page.IsPostBack)
			{
				Query q = new Query();
				q.OrderBy = new OrderBy(Theme.Columns.Order);
				ThemeSet ts = new ThemeSet(q);

				ThemesRadioButtonList.DataTextField = "RadioButtonText";
				ThemesRadioButtonList.DataValueField = "K";
				ThemesRadioButtonList.DataSource = ts;
				ThemesRadioButtonList.DataBind();

				if (IsEdit)
				{
					if (CurrentGroup.ThemeK == 0)
						ThemesRadioButtonList.SelectedValue = "18";
					else
						ThemesRadioButtonList.SelectedValue = CurrentGroup.ThemeK.ToString();
				}
				if (IsEdit && CurrentGroup.BrandK > 0)
				{
					foreach (ListItem li in ThemesRadioButtonList.Items)
					{
						if (!li.Value.Equals("1"))
							li.Attributes["disabled"] = "true";
					}
				}
			}
		}
		#endregion
		#region PanelTheme_Val
		public void PanelTheme_Val(object o, ServerValidateEventArgs e)
		{
			e.IsValid = ThemesRadioButtonList.SelectedIndex > -1;
		}
		#endregion
		#region PanelTheme_Next
		public void PanelTheme_Next(object o, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				ChangePanel(PanelLocation);
			}
		}
		#endregion
		#endregion

		#region PanelLocation
		protected Panel PanelLocation;
		protected DropDownList LocationCountryDropDown, LocationPlaceDropDown;
		protected RadioButton LocationTypeNone, LocationTypeCountry, LocationTypePlace;
		protected HtmlGenericControl LocationCountryP, LocationPlaceP;
		protected HtmlButton PanelLocationBackButton;
		protected HtmlGenericControl PanelLocationSaveP;
		#region PanelLocation_Load
		private void PanelLocation_Load(object sender, System.EventArgs e)
		{
			PanelLocationSaveP.Visible = IsEdit;
			PanelLocationBackButton.Visible = !(IsEdit && CurrentGroup.BrandK > 0);
			if (!Page.IsPostBack)
			{
				Query q = new Query();
				q.Columns = new ColumnSet(Country.Columns.K, Country.Columns.Name);
				q.OrderBy = new OrderBy(Country.Columns.Name);
				q.QueryCondition = new Q(Country.Columns.Enabled, true);
				CountrySet cs = new CountrySet(q);
				LocationCountryDropDown.DataTextField = "Name";
				LocationCountryDropDown.DataValueField = "K";
				LocationCountryDropDown.DataSource = cs;
				LocationCountryDropDown.DataBind();

				if (IsEdit)
				{
					LocationTypeNone.Checked = CurrentGroup.CountryK == 0;
					LocationTypeCountry.Checked = CurrentGroup.CountryK != 0 && CurrentGroup.PlaceK == 0;
					LocationTypePlace.Checked = CurrentGroup.PlaceK != 0;

					if (CurrentGroup.CountryK != 0 && LocationCountryDropDown.Items.FindByValue(CurrentGroup.CountryK.ToString()) != null)
						LocationCountryDropDown.SelectedValue = CurrentGroup.CountryK.ToString();
					else if (Country.Current.K != 0 && LocationCountryDropDown.Items.FindByValue(Country.Current.K.ToString()) != null)
						LocationCountryDropDown.SelectedValue = Country.Current.K.ToString();
					else
						LocationCountryDropDown.SelectedValue = "224";
				}
				else
				{
					if (Country.Current.K != 0 && LocationCountryDropDown.Items.FindByValue(Country.Current.K.ToString()) != null)
						LocationCountryDropDown.SelectedValue = Country.Current.K.ToString();
					else
						LocationCountryDropDown.SelectedValue = "224";
				}
				UpdateLocationDropDowns();
			}
		}
		#endregion
		#region LocationType_Change
		public void LocationType_Change(object o, System.EventArgs e)
		{
			UpdateLocationDropDowns();
		}
		#endregion
		#region UpdateLocationDropDowns()
		public void UpdateLocationDropDowns()
		{
			LocationCountryP.Visible = LocationTypeCountry.Checked || LocationTypePlace.Checked;
			LocationPlaceP.Visible = LocationTypePlace.Checked;

			if (LocationTypePlace.Checked)
				BindLocationPlaceDropDown();
		}
		#endregion
		#region LocationCountryDropDown_Change
		public void LocationCountryDropDown_Change(object o, System.EventArgs e)
		{
			if (LocationTypePlace.Checked)
			{
				BindLocationPlaceDropDown();
			}
		}
		#endregion
		#region BindLocationPlaceDropDown()
		private void BindLocationPlaceDropDown()
		{
			Query q = new Query();
			q.Columns = new ColumnSet(Place.Columns.K, Place.Columns.Name);
			q.OrderBy = new OrderBy(Place.Columns.Name);
			q.QueryCondition = new And(
				new Q(Place.Columns.Enabled, true),
				new Q(Place.Columns.CountryK, int.Parse(LocationCountryDropDown.SelectedValue)));
			PlaceSet cs = new PlaceSet(q);
			LocationPlaceDropDown.DataTextField = "NamePlain";
			LocationPlaceDropDown.DataValueField = "K";
			LocationPlaceDropDown.DataSource = cs;
			LocationPlaceDropDown.DataBind();
			if (IsEdit && !Page.IsPostBack)
			{
				if (CurrentGroup.PlaceK != 0 && LocationPlaceDropDown.Items.FindByValue(CurrentGroup.PlaceK.ToString()) != null)
					LocationPlaceDropDown.SelectedValue = CurrentGroup.PlaceK.ToString();
			}
		}
		#endregion
		#region ShowMusicTypes
		bool ShowMusicTypes
		{
			get
			{
				if (IsEdit)
					return CurrentGroup.ThemeK == 0 || CurrentGroup.ThemeK == 1 || CurrentGroup.ThemeK == 2;
				else
					return ThemesRadioButtonList.SelectedValue == "1" || ThemesRadioButtonList.SelectedValue == "2" || ThemesRadioButtonList.SelectedValue == "18";
			}
		}
		#endregion
		#region PanelLocation_Val
		public void PanelLocation_Val(object o, ServerValidateEventArgs e)
		{
			Country c = new Country();
			bool validRadio = LocationTypeNone.Checked || LocationTypeCountry.Checked || LocationTypePlace.Checked;
			bool validCountry = true;
			bool validPlace = true;
			int selectedCountryK = 0;
			if (LocationTypeCountry.Checked || LocationTypePlace.Checked)
			{
				try
				{
					Country country = new Country(int.Parse(LocationCountryDropDown.SelectedValue));
					if (!country.Enabled)
						validCountry = false;
					selectedCountryK = country.K;
				}
				catch
				{
					validCountry = false;
				}
			}
			if (LocationTypePlace.Checked)
			{
				try
				{
					Place place = new Place(int.Parse(LocationPlaceDropDown.SelectedValue));
					if (!place.Enabled)
						validPlace = false;
					if (selectedCountryK != place.CountryK)
						validPlace = false;
				}
				catch
				{
					validPlace = false;
				}
			}
			e.IsValid = validRadio && validCountry && validPlace;
		}
		#endregion
		#region PanelLocation_Next
		public void PanelLocation_Next(object sender, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				if (ThemesRadioButtonList.SelectedValue.Equals("1") || ThemesRadioButtonList.SelectedValue.Equals("2"))
					ChangePanel(PanelMusicType);//If selected theme is music or nightlife
				else
					ChangePanel(PanelDetails);
			}
		}
		#endregion
		#region PanelLocation_Back
		public void PanelLocation_Back(object sender, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				ChangePanel(PanelTheme);
			}
		}
		#endregion
		#endregion

		#region PanelMusicType
		protected Panel PanelMusicType;
		protected RadioButtonList MusicTypesRadioButtonList;
		protected HtmlGenericControl PanelMusicTypeSaveP;
		#region PanelMusicType_Load
		private void PanelMusicType_Load(object sender, System.EventArgs e)
		{
			PanelMusicTypeSaveP.Visible = IsEdit;
			if (!Page.IsPostBack)
			{
				Query q = new Query();
				q.OrderBy = new OrderBy(MusicType.Columns.Order);
				q.QueryCondition = new Q(MusicType.Columns.ParentK, 1);
				MusicTypeSet mts = new MusicTypeSet(q);

				MusicTypesRadioButtonList.DataTextField = "RadioButtonText";
				MusicTypesRadioButtonList.DataValueField = "K";
				MusicTypesRadioButtonList.DataSource = mts;
				MusicTypesRadioButtonList.DataBind();

				MusicTypesRadioButtonList.Items.Insert(0, new ListItem("<span style=\"font-size:14px;font-weight:bold;\">&nbsp;All music</span><p style=\"margin-left:26px;margin-top:0px;margin-bottom:10px;\">e.g. the group is about all music, or several of the music types below</p>", "1"));
				MusicTypesRadioButtonList.Items.Insert(0, new ListItem("<span style=\"font-size:14px;font-weight:bold;\">&nbsp;Not about music</span><p style=\"margin-left:26px;margin-top:0px;margin-bottom:10px;\">e.g. the group is not about music</p>", "0"));

				if (IsEdit)
				{
					if (MusicTypesRadioButtonList.Items.FindByValue(CurrentGroup.MusicTypeK.ToString()) != null)
						MusicTypesRadioButtonList.SelectedValue = CurrentGroup.MusicTypeK.ToString();
				}
			}
		}
		#endregion
		#region PanelMusicType_Val
		public void PanelMusicType_Val(object o, ServerValidateEventArgs e)
		{
			e.IsValid = MusicTypesRadioButtonList.SelectedItem != null;
		}
		#endregion
		#region PanelMusicType_Next
		public void PanelMusicType_Next(object sender, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				ChangePanel(PanelDetails);
			}
		}
		#endregion
		#region PanelMusicType_Back
		public void PanelMusicType_Back(object sender, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				ChangePanel(PanelLocation);
			}
		}
		#endregion
		#endregion

		#region PanelDetails
		protected Panel PanelDetails, NamePanel;
		protected TextBox NameTextBox, DescriptionTextBox, RulesTextBox;
		protected HtmlGenericControl PanelDetailsSaveP;
		#region PanelDetails_Load
		private void PanelDetails_Load(object sender, System.EventArgs e)
		{
			PanelDetailsSaveP.Visible = IsEdit;
			if (!Page.IsPostBack)
			{
				if (IsEdit)
				{
					NameTextBox.Text = CurrentGroup.Name;
					DescriptionTextBox.Text = CurrentGroup.Description;
					IntroHtml.LoadHtml(CurrentGroup.LongDescriptionHtml);
					RulesTextBox.Text = CurrentGroup.PostingRules;
					NamePanel.Visible = CurrentGroup.BrandK == 0;
				}
			}
		}
		#endregion
		#region NameLength_Val
		public void NameLength_Val(object o, ServerValidateEventArgs e)
		{
			string s = NameTextBox.Text;
			System.Text.RegularExpressions.Regex doubleSpaces = new System.Text.RegularExpressions.Regex("[ ]{2,}");
			System.Text.RegularExpressions.Regex valid = new System.Text.RegularExpressions.Regex("^.{5,50}$");
			s = Cambro.Web.Helpers.StripHtml(s);
			s = doubleSpaces.Replace(s, "");
			e.IsValid = valid.IsMatch(s);
		}
		#endregion
		#region DescriptionLength_Val
		public void DescriptionLength_Val(object o, ServerValidateEventArgs e)
		{
			string s = DescriptionTextBox.Text;
			System.Text.RegularExpressions.Regex doubleSpaces = new System.Text.RegularExpressions.Regex("[ ]{2,}");
			System.Text.RegularExpressions.Regex valid = new System.Text.RegularExpressions.Regex("^.{20,200}$");
			s = Cambro.Web.Helpers.StripHtml(s);
			s = doubleSpaces.Replace(s, "");
			e.IsValid = valid.IsMatch(s);
		}
		#endregion
		#region Punctuation_Val
		public void Punctuation_Val(object o, ServerValidateEventArgs e)
		{
			System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("[a-z ]");
			System.Text.RegularExpressions.Regex doubleSpaces = new System.Text.RegularExpressions.Regex("[ ]{2,}");
			string s = e.Value;
			s = Cambro.Web.Helpers.StripHtml(s);
			s = doubleSpaces.Replace(s, "");
			int lowerCaseLetters = 0;
			for (int i = 0; i < s.Length; i++)
			{
				if (r.IsMatch(s[i].ToString()))
					lowerCaseLetters++;
			}
			e.IsValid = (double)lowerCaseLetters / (double)s.Length >= 0.5;
		}
		#endregion
		#region StartEnd_Val
		public void StartEnd_Val(object o, ServerValidateEventArgs e)
		{
			string s = Cambro.Web.Helpers.Strip(e.Value, true, true, true, true).ToLower();
			if (s.StartsWith("the "))
				e.IsValid = false;
			else if (s.EndsWith(" group"))
				e.IsValid = false;
			else
				e.IsValid = true;
		}
		#endregion
		#region PanelDetails_Next
		public void PanelDetails_Next(object sender, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				ChangePanel(PanelMembership);
			}
		}
		#endregion
		#region PanelDetails_Back
		public void PanelDetails_Back(object sender, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				if (ThemesRadioButtonList.SelectedValue.Equals("1") || ThemesRadioButtonList.SelectedValue.Equals("2"))
					ChangePanel(PanelMusicType);
				else
					ChangePanel(PanelLocation);
			}
		}
		#endregion
		#endregion

		#region PanelMembership
		protected Panel PanelMembership;
		protected RadioButton MembershipPublic, MembershipMember, MembershipModerator;
		protected HtmlGenericControl PanelMembershipSaveP;
		#region PanelMembership_Load
		private void PanelMembership_Load(object sender, System.EventArgs e)
		{
			PanelMembershipSaveP.Visible = IsEdit;
			if (!Page.IsPostBack)
			{
				if (IsEdit)
				{
					MembershipPublic.Checked = CurrentGroup.Restriction.Equals(Group.RestrictionEnum.None);
					MembershipMember.Checked = CurrentGroup.Restriction.Equals(Group.RestrictionEnum.Member);
					MembershipModerator.Checked = CurrentGroup.Restriction.Equals(Group.RestrictionEnum.Moderator);
				}
			}
		}
		#endregion
		#region PanelMembership_Val
		public void PanelMembership_Val(object o, ServerValidateEventArgs e)
		{
			e.IsValid = MembershipPublic.Checked || MembershipMember.Checked || MembershipModerator.Checked;
		}
		#endregion
		#region PanelMembership_Next
		public void PanelMembership_Next(object sender, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				if (IsEdit && CurrentGroup.BrandK > 0)
				{
					Save(true);
				}
				else
					ChangePanel(PanelPrivate);
			}
		}
		#endregion
		#region PanelMembership_Back
		public void PanelMembership_Back(object sender, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				ChangePanel(PanelDetails);
			}
		}
		#endregion
		#endregion

		#region PanelPrivate
		protected Panel PanelPrivate;
		protected RadioButton GroupPagePublic, GroupPagePrivate, ChatForumPublic, ChatForumPrivate,
			MembersListPublic, MembersListPrivate;
		protected HtmlGenericControl MembersListRadioSpan, ChatForumRadioSpan;
		protected HtmlGenericControl PanelPrivateSaveP;
		#region PanelPrivate_Load
		private void PanelPrivate_Load(object sender, System.EventArgs e)
		{
			PanelPrivateSaveP.Visible = IsEdit;
			GroupPagePublic.Attributes["onclick"] = "UpdateRadioButtons();";
			GroupPagePrivate.Attributes["onclick"] = "UpdateRadioButtons();";
			if (!Page.IsPostBack)
			{
				if (IsEdit)
				{
					GroupPagePublic.Checked = !CurrentGroup.PrivateGroupPage;
					GroupPagePrivate.Checked = CurrentGroup.PrivateGroupPage;

					if (GroupPagePrivate.Checked)
					{
						ChatForumPublic.Checked = false;
						ChatForumPrivate.Checked = true;
						Cambro.Web.Helpers.ChangeState(ChatForumRadioSpan, ChatForumPublic, false);

						MembersListPublic.Checked = false;
						MembersListPrivate.Checked = true;
						Cambro.Web.Helpers.ChangeState(MembersListRadioSpan, MembersListPublic, false);
					}
					else
					{
						ChatForumPublic.Checked = !CurrentGroup.PrivateChat;
						ChatForumPrivate.Checked = CurrentGroup.PrivateChat;

						MembersListPublic.Checked = !CurrentGroup.PrivateMemberList;
						MembersListPrivate.Checked = CurrentGroup.PrivateMemberList;
					}
				}
			}
			if (GroupPagePrivate.Checked)
			{
				ChatForumPublic.Checked = false;
				Cambro.Web.Helpers.ChangeState(ChatForumRadioSpan, ChatForumPublic, false);

				MembersListPublic.Checked = false;
				Cambro.Web.Helpers.ChangeState(MembersListRadioSpan, MembersListPublic, false);
			}
			else
			{
				Cambro.Web.Helpers.ChangeState(ChatForumRadioSpan, ChatForumPublic, true);
				Cambro.Web.Helpers.ChangeState(MembersListRadioSpan, MembersListPublic, true);
			}
		}
		#endregion
		#region PanelPrivate_Val
		public void PanelPrivate_Val(object o, ServerValidateEventArgs e)
		{
			bool checks = (GroupPagePublic.Checked || GroupPagePrivate.Checked)
				&& (ChatForumPublic.Checked || ChatForumPrivate.Checked)
				&& (MembersListPublic.Checked || MembersListPrivate.Checked);
			bool secure = true;
			if (GroupPagePrivate.Checked && (ChatForumPublic.Checked || MembersListPublic.Checked))
				secure = false;

			e.IsValid = checks && secure;
		}
		#endregion
		#region PanelPrivate_Next
		public void PanelPrivate_Next(object sender, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				Save(true);
			}
		}
		#endregion
		#region PanelPrivate_Back
		public void PanelPrivate_Back(object sender, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				ChangePanel(PanelMembership);
			}
		}
		#endregion
		#endregion

		#region Save

		public void SaveAndExit_Click(object o, System.EventArgs e)
		{
			if (Page.IsValid)
				Save(false);
		}

		void Save(bool RedirectToPic)
		{
			if (IsEdit)
			{
				string newName = Cambro.Web.Helpers.Strip(NameTextBox.Text);
				bool changedName = !CurrentGroup.Name.Equals(newName);
				CurrentGroup.Name = newName;

				CurrentGroup.Description = Cambro.Web.Helpers.Strip(DescriptionTextBox.Text);
				CurrentGroup.PostingRules = Cambro.Web.Helpers.Strip(RulesTextBox.Text);
				CurrentGroup.LongDescriptionHtml = IntroHtml.GetHtml();

				bool newPrivateChat;
				if (GroupPagePrivate.Checked)
				{
					CurrentGroup.PrivateGroupPage = true;
					CurrentGroup.PrivateMemberList = true;
					newPrivateChat = true;
				}
				else
				{
					CurrentGroup.PrivateGroupPage = false;
					CurrentGroup.PrivateMemberList = MembersListPrivate.Checked;
					newPrivateChat = ChatForumPrivate.Checked;
				}
				bool changedPrivateChat = newPrivateChat != CurrentGroup.PrivateChat;
				CurrentGroup.PrivateChat = newPrivateChat;

				if (MembershipMember.Checked)
					CurrentGroup.Restriction = Group.RestrictionEnum.Member;
				else if (MembershipModerator.Checked)
					CurrentGroup.Restriction = Group.RestrictionEnum.Moderator;
				else
					CurrentGroup.Restriction = Group.RestrictionEnum.None;

				int newTheme;
				if (ThemesRadioButtonList.SelectedValue.Equals("18"))
					newTheme = 0;
				else
				{
					Theme t = new Theme(int.Parse(ThemesRadioButtonList.SelectedValue));
					newTheme = t.K;
				}
				bool changedTheme = newTheme != CurrentGroup.ThemeK;
				CurrentGroup.ThemeK = newTheme;


				int newCountry;
				int oldCountry = CurrentGroup.CountryK;
				if (LocationTypeCountry.Checked || LocationTypePlace.Checked)
				{
					Country c = new Country(int.Parse(LocationCountryDropDown.SelectedValue));
					if (!c.Enabled)
						throw new Exception("invalid country!");
					newCountry = c.K;
				}
				else
				{
					newCountry = 0;
				}
				bool changedCountry = CurrentGroup.CountryK != newCountry;
				CurrentGroup.CountryK = newCountry;

				int newPlace;
				int oldPlace = CurrentGroup.PlaceK;
				if (LocationTypePlace.Checked)
				{
					Place p = new Place(int.Parse(LocationPlaceDropDown.SelectedValue));
					if (!p.Enabled || p.CountryK != CurrentGroup.CountryK)
						throw new Exception("invalid place!");
					newPlace = p.K;
				}
				else
				{
					newPlace = 0;
				}
				bool changedPlace = CurrentGroup.PlaceK != newPlace;
				CurrentGroup.PlaceK = newPlace;

				int newMusicType;
				if (CurrentGroup.ThemeK == 1 || CurrentGroup.ThemeK == 2)
				{
					if (!MusicTypesRadioButtonList.SelectedValue.Equals("0"))
					{
						MusicType mt = new MusicType(int.Parse(MusicTypesRadioButtonList.SelectedValue));
						if (!(mt.ParentK == 0 || mt.ParentK == 1))
							throw new Exception("Invalid music type");
						newMusicType = mt.K;
					}
					else
					{
						newMusicType = 0;
					}
				}
				else
				{
					newMusicType = 0;
				}
				bool changedMusic = CurrentGroup.MusicTypeK != newMusicType;
				CurrentGroup.MusicTypeK = newMusicType;

				if (changedName)
					CurrentGroup.CreateUniqueUrlName(false);

				Transaction transaction = null;//new Transaction();
				try
				{
					if (changedPrivateChat)
					{
						Update update = new Update();
						update.Table = TablesEnum.Thread;
						update.Changes.Add(new Assign(Thread.Columns.PrivateGroup, CurrentGroup.PrivateChat));
						update.Where = new Q(Thread.Columns.GroupK, CurrentGroup.K);
						update.Run(transaction);
					}

					if (changedTheme)
					{
						Update update = new Update();
						update.Table = TablesEnum.Thread;
						update.Changes.Add(new Assign(Thread.Columns.ThemeK, CurrentGroup.ThemeK));
						update.Where = new Q(Thread.Columns.GroupK, CurrentGroup.K);
						update.Run(transaction);
					}

					if (changedCountry)
					{
						Update update = new Update();
						update.Table = TablesEnum.Thread;
						update.Changes.Add(new Assign(Thread.Columns.CountryK, CurrentGroup.CountryK));
						update.Where = new And(new Q(Thread.Columns.ParentObjectType, Model.Entities.ObjectType.Group), new Q(Thread.Columns.ParentObjectK, CurrentGroup.K));
						update.Run(transaction);
					}

					if (changedPlace)
					{
						Update update = new Update();
						update.Table = TablesEnum.Thread;
						update.Changes.Add(new Assign(Thread.Columns.PlaceK, CurrentGroup.PlaceK));
						update.Where = new And(new Q(Thread.Columns.ParentObjectType, Model.Entities.ObjectType.Group), new Q(Thread.Columns.ParentObjectK, CurrentGroup.K));
						update.Run(transaction);

						if (oldPlace > 0)
						{
							Place oldP = new Place(oldPlace);
							oldP.UpdateTotalComments(null);
						}
						if (newPlace > 0)
						{
							Place newP = new Place(newPlace);
							newP.UpdateTotalComments(null);
						}
					}

					if (changedMusic)
					{
						Update update = new Update();
						update.Table = TablesEnum.Thread;
						update.Changes.Add(new Assign(Thread.Columns.MusicTypeK, CurrentGroup.MusicTypeK));
						update.Where = new Q(Thread.Columns.GroupK, CurrentGroup.K);
						update.Run(transaction);
					}

					if (changedName)
					{
						Utilities.UpdateChildUrlFragmentsJob job = new Utilities.UpdateChildUrlFragmentsJob(Model.Entities.ObjectType.Group, CurrentGroup.K, true);
						job.ExecuteAsynchronously();
					}
					CurrentGroup.Update(transaction);

					//transaction.Commit();
				}
				catch (Exception ex)
				{
					//transaction.Rollback();
					throw ex;
				}
				finally
				{
					//transaction.Close();
				}
				if (RedirectToPic)
				{
					if (ContainerPage.Url["promoterk"].IsInt)
						Response.Redirect(CurrentGroup.UrlApp("edit", "pic", "", "promoterk", ContainerPage.Url["promoterk"]));
					else
						Response.Redirect(CurrentGroup.UrlApp("edit", "pic", ""));
				}
				else
				{
					RedirectSaved();
				}


			}
			else
			{
				GroupSet gsDup = new GroupSet(new Query(new Q(Group.Columns.DuplicateGuid, (Guid)ContainerPage.ViewStatePublic["GroupDuplicateGuid"])));
				if (gsDup.Count != 0)
				{
					Response.Redirect(gsDup[0].UrlApp("edit", "pic", ""));
				}
				else
				{
					Group g = new Group();
					g.Name = Cambro.Web.Helpers.Strip(NameTextBox.Text);
					g.Description = Cambro.Web.Helpers.Strip(DescriptionTextBox.Text);

					g.LongDescriptionHtml = IntroHtml.GetHtml();
					
					g.PostingRules = Cambro.Web.Helpers.Strip(RulesTextBox.Text, true, true, false, true);
					g.DateTimeCreated = DateTime.Now;
					g.PrivateGroupPage = GroupPagePrivate.Checked;
					if (GroupPagePrivate.Checked)
					{
						g.PrivateMemberList = true;
						g.PrivateChat = true;
					}
					else
					{
						g.PrivateMemberList = MembersListPrivate.Checked;
						g.PrivateChat = ChatForumPrivate.Checked;
					}

					if (MembershipMember.Checked)
						g.Restriction = Group.RestrictionEnum.Member;
					else if (MembershipModerator.Checked)
						g.Restriction = Group.RestrictionEnum.Moderator;
					else
						g.Restriction = Group.RestrictionEnum.None;

					if (ThemesRadioButtonList.SelectedValue.Equals("18"))
						g.ThemeK = 0;
					else
					{
						Theme t = new Theme(int.Parse(ThemesRadioButtonList.SelectedValue));
						g.ThemeK = t.K;
					}

					if (LocationTypeCountry.Checked || LocationTypePlace.Checked)
					{
						Country c = new Country(int.Parse(LocationCountryDropDown.SelectedValue));
						if (!c.Enabled)
							throw new Exception("invalid country!");
						g.CountryK = c.K;
					}
					if (LocationTypePlace.Checked)
					{
						Place p = new Place(int.Parse(LocationPlaceDropDown.SelectedValue));
						if (!p.Enabled || p.CountryK != g.CountryK)
							throw new Exception("invalid place!");
						g.PlaceK = p.K;
					}

					if (g.ThemeK == 1 || g.ThemeK == 2)
					{
						if (!MusicTypesRadioButtonList.SelectedValue.Equals("0"))
						{
							MusicType mt = new MusicType(int.Parse(MusicTypesRadioButtonList.SelectedValue));
							if (!(mt.ParentK == 0 || mt.ParentK == 1))
								throw new Exception("Invalid music type");
							g.MusicTypeK = mt.K;
						}
					}

					g.CreateUniqueUrlName(false);

					g.DuplicateGuid = (Guid)ContainerPage.ViewStatePublic["GroupDuplicateGuid"];
					g.EmailOnAllThreads = false;

					g.Update();

					g.ChangeUsr(false, Usr.Current.K, true, true, true, true, Bobs.GroupUsr.StatusEnum.Member, DateTime.Now, true);

					Response.Redirect(g.UrlApp("edit", "pic", ""));
				}
			}
		}
		#endregion

		#region PanelPic
		protected Panel PanelPic;
		protected Controls.Pic GroupPic;
		#region PanelPic_Load
		private void PanelPic_Load(object sender, System.EventArgs e)
		{
			if (Mode.Equals(Modes.Pic))
			{
				if (!IsEdit)
					throw new Exception("you can only access this page when editing a current event");

				GroupPic.InputObject = CurrentGroup;
				if (!Page.IsPostBack)
				{
					GroupPic.InitPic();
				}
			}
		}
		#endregion
		public void PicSaved(object o, System.EventArgs e)
		{
			if (CurrentGroup.BrandK > 0)
				Bobs.Utilities.CopyPic(CurrentGroup, CurrentGroup.Brand);
			RedirectSaved();
		}
		public void PicNoPic(object o, System.EventArgs e)
		{
			if (CurrentGroup.BrandK > 0)
				Bobs.Utilities.DeletePic(CurrentGroup.Brand);
			RedirectSaved();
		}
		#endregion

		protected void RedirectSaved()
		{
			if (ContainerPage.Url["promoterk"].IsInt && ContainerPage.Url["promoterk"] > 0)
			{
				Promoter p = new Promoter(ContainerPage.Url["promoterk"]);
				if (Usr.Current.IsAdmin || Usr.Current.IsPromoterK(ContainerPage.Url["promoterk"]))
					Response.Redirect(p.Url());
				else
					throw new DsiUserFriendlyException("Can't redirect to this promoter!");
			}
			else
				Response.Redirect(CurrentGroup.Url());
		}

		#region CurrentGroupUsr
		public GroupUsr CurrentGroupUsr
		{
			get
			{
				if (!gotGroupUsr)
				{
					gotGroupUsr = true;
					currentGroupUsr = CurrentGroup.GetGroupUsr(Usr.Current);
				}
				return currentGroupUsr;
			}
		}
		GroupUsr currentGroupUsr;
		bool gotGroupUsr = false;
		#endregion

		#region CurrentGroup
		public Bobs.Group CurrentGroup
		{
			get
			{
				if (ContainerPage.Url.HasGroupObjectFilter)
					return ContainerPage.Url.ObjectFilterGroup;
				else
					return null;
			}
		}
		#endregion

		#region IsEdit
		public bool IsEdit
		{
			get
			{
				return ContainerPage.Url.HasGroupObjectFilter;
			}
		}
		#endregion

		#region PageMode
		Modes Mode
		{
			get
			{
				if (ContainerPage.Url[0].Equals("Theme"))
					return Modes.Theme;
				else if (ContainerPage.Url[0].Equals("Location"))
					return Modes.Location;
				else if (ContainerPage.Url[0].Equals("MusicType"))
					return Modes.MusicType;
				else if (ContainerPage.Url[0].Equals("Details"))
					return Modes.Details;
				else if (ContainerPage.Url[0].Equals("Membership"))
					return Modes.Membership;
				else if (ContainerPage.Url[0].Equals("Private"))
					return Modes.Private;
				else if (ContainerPage.Url[0].Equals("Pic"))
					return Modes.Pic;
				else
					return Modes.None;
			}
		}
		public enum Modes
		{
			None,
			Theme,
			Location,
			MusicType,
			Details,
			Membership,
			Private,
			Pic
		}
		#endregion

		#region ChangePanel
		void ChangePanel(Panel p)
		{
			PanelTheme.Visible = p.Equals(PanelTheme);
			PanelLocation.Visible = p.Equals(PanelLocation);
			PanelMusicType.Visible = p.Equals(PanelMusicType);
			PanelDetails.Visible = p.Equals(PanelDetails);
			PanelMembership.Visible = p.Equals(PanelMembership);
			PanelPrivate.Visible = p.Equals(PanelPrivate);
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
			this.Load += new System.EventHandler(this.GroupIntro_Load);
			this.Load += new System.EventHandler(this.PanelTheme_Load);
			this.Load += new System.EventHandler(this.PanelLocation_Load);
			this.Load += new System.EventHandler(this.PanelMusicType_Load);
			this.Load += new System.EventHandler(this.PanelDetails_Load);
			this.Load += new System.EventHandler(this.PanelMembership_Load);
			this.Load += new System.EventHandler(this.PanelPrivate_Load);
			this.Load += new System.EventHandler(this.PanelPic_Load);

			this.PreRender += new System.EventHandler(this.GroupIntro_PreRender);
		}
		#endregion
	}
}
