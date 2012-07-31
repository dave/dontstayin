using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Bobs;
using Bobs.Jobs;

namespace Spotted.Pages.Promoters
{
	public partial class Edit : PromoterUserControl
	{

		#region Page_Init
		protected override void Page_Init(object sender, System.EventArgs e)
		{
			// For creating new promoters, they wont have a PromoterObjectFilter
			if(CurrentPromoter != null)
				base.Page_Init(sender, e);
			else
				Usr.KickUserIfNotLoggedIn();
		}
		#endregion

		#region Page_Load
		private void Page_Load(object sender, System.EventArgs e)
		{
			Response.CacheControl = "no-cache";
			Response.AddHeader("Pragma", "no-cache");
			Response.Expires = -1;

			if (Mode.Equals(Modes.None))
			{
				if (Usr.Current.IsPromoter)
					ChangePanel(PanelAlreadyPromoter);
				else
					ChangePanel(PanelSignUpForm);
			}
			else if (Mode.Equals(Modes.Add) || Mode.Equals(Modes.Edit))
				ChangePanel(PanelSignUpForm);
			else if (Mode.Equals(Modes.ConfirmBrand))
			{
				if (!Usr.Current.IsAdmin)
					throw new Exception("Only admin!");

				Brand b = new Brand(ContainerPage.Url["k"]);
				b.PromoterStatus = Brand.PromoterStatusEnum.Confirmed;
				b.Update();
				Response.Redirect(b.Promoter.Url());
			}
			else if (Mode.Equals(Modes.ConfirmVenue))
			{
				if (!Usr.Current.IsAdmin)
					throw new Exception("Only admin!");

				Venue v = new Venue(ContainerPage.Url["k"]);
				v.PromoterStatus = Venue.PromoterStatusEnum.Confirmed;
				v.Update();
				Response.Redirect(v.Promoter.Url());
			}
			this.SignUpForm_Load(sender, e);
			this.Pic_Load(sender, e);
			if (Usr.Current.IsAdmin)
			{
				this.uiAccessUsersMultiSelector.WebServiceMethod = "GetUsrsPublic";
			}
		}
		#endregion

		#region Sign up form
		#region SignUpForm_Load
		public void SignUpForm_Load(object o, System.EventArgs e)
		{
			if (Mode.Equals(Modes.None) || Mode.Equals(Modes.Add) || Mode.Equals(Modes.Edit))
			{
                DuplicateAccountP.Visible = Mode.Equals(Modes.Add);

				if (IsEdit && !CanEdit)
					throw new Exception("You can't edit this promoter!");

				if (IsEdit)
				{
					PanelSignUpFormHeading.InnerText = "Edit promoter details";
					ContainerPage.SetPageTitle("Edit promoter details");
				}
				else
				{
					PanelSignUpFormHeading.InnerText = "Promoter account application";
					ContainerPage.SetPageTitle("Promoter account application");
				}

				//Commented out as BuddiesUsrK does ont exist for a js:multiselector
				//if (!Usr.Current.IsAdmin)
				//    this.uiAccessUsersMultiSelector.BuddiesUsrK = Usr.Current.K;
				
				AdminOnlyDataEntrySetup();
				if (!Page.IsPostBack)
				{
					BuildSignUpForm();
					ViewState["PromoterDuplicateGuid"] = Guid.NewGuid();
				}
				SetupPrimaryUserDropDown();

				TermsPanel.Visible = Mode.Equals(Modes.Add);
			}
			else if (Mode.Equals(Modes.Pic) && CurrentPromoter != null)
			{
				if (!CanEdit)
					throw new Exception("You can't edit this promoter!");

				PanelSignUpFormHeading.InnerText = "Add / change promoter picture";
				ContainerPage.SetPageTitle("Add / change promoter picture");

				ChangePanel(PanelPic);
			}
			DisableValidatorsForAdmins();
		}

		private void SetupPrimaryUserDropDown()
		{

			string selectedValue = Page.IsPostBack ? uiPrimaryUserDropDown.SelectedValue : CurrentPromoter == null ? "" : CurrentPromoter.PrimaryUsrK.ToString();
			this.uiPrimaryUserDropDown.Items.Clear();

			foreach (var pair in uiAccessUsersMultiSelector.Selections)
			{
				this.uiPrimaryUserDropDown.Items.Add(new ListItem(pair.Key, pair.Value));
			}
			try
			{
				this.uiPrimaryUserDropDown.SelectedValue = selectedValue;
			}
			catch { }
		} 
		
		#endregion

		#region DisableValidatorsForAdmins
		private void DisableValidatorsForAdmins()
		{
			if (Usr.Current.IsAdmin || Usr.Current.IsSuperAdmin)
			{
				//this.RequiredFieldValidator1.Enabled = false;
				//this.Requiredfieldvalidator2.Enabled = false;
				//this.Requiredfieldvalidator5.Enabled = false;
				this.Requiredfieldvalidator6.Enabled = false;
				this.Requiredfieldvalidator7.Enabled = false;
				this.Requiredfieldvalidator8.Enabled = false;
				this.Requiredfieldvalidator9.Enabled = false;
				//this.Customfieldvalidator3.Enabled = false;
				this.Customvalidator1.Enabled = false;
				//this.CustomValidator2.Enabled = false;
				//this.Customvalidator7.Enabled = false;
			}
		}
		#endregion

		#region AdminOnlyDataEntrySetup
		private void AdminOnlyDataEntrySetup()
		{
			if (!Page.IsPostBack)
			{
				this.ExtraContactDetailsPanel.Visible = Usr.Current.IsAdmin || Usr.Current.IsSuperAdmin;
                this.BankAccountDetailsPanel.Visible = Usr.Current.IsAdmin || Usr.Current.IsSuperAdmin;
				this.NoAccountUsers.Visible = Usr.Current.IsAdmin || Usr.Current.IsSuperAdmin;
				this.SingleAccountUser.Visible = !(Usr.Current.IsAdmin || Usr.Current.IsSuperAdmin);
				if (Usr.Current.IsAdmin || Usr.Current.IsSuperAdmin)
				{
					this.AccessJustMeRadio.Checked = false;
					this.AccessMultiRadio.Text = " These users <small>(1st in list is primary user)</small>";
					this.AccessMultiRadio.Checked = true;
					MultiAccess.Visible = this.AccessMultiRadio.Checked;
				}

				this.Sector.Items.Clear();
                Utilities.AddEnumValuesToDropDownList(this.Sector, typeof(Promoter.ClientSectorEnum));
				this.Sector.SelectedValue = ((int)Promoter.ClientSectorEnum.Promoter).ToString();

				SalesCampaignDropDown.Items.Clear();
				SalesCampaignSet scs = new SalesCampaignSet(new Query() { OrderBy = new OrderBy(SalesCampaign.Columns.DateStart, OrderBy.OrderDirection.Descending) });
				SalesCampaignDropDown.DataSource = scs;
				SalesCampaignDropDown.DataTextField = "Name";
				SalesCampaignDropDown.DataValueField = "K";
				SalesCampaignDropDown.DataBind();
				SalesCampaignDropDown.Items.Insert(0, new ListItem("(none)", "0"));

				//SalesCampaignDropDown.SelectedValue = Cu

			}
		}
		#endregion
		#region PanelSignUpFormClick
		public void PanelSignUpFormClick(object o, System.EventArgs e)
		{
			Page.Validate();
			if (Page.IsValid)
			{
				if (IsEdit)
				{
					if (!CanEdit)
						throw new Exception("You can't edit this promoter!");
					#region Store changes to promoter in admin note
					string oldDetails = "";
					if (!Cambro.Web.Helpers.StripHtml(Name.Text).Equals(CurrentPromoter.Name))
						oldDetails += "Name: [" + CurrentPromoter.Name + "] - [" + Cambro.Web.Helpers.StripHtml(Name.Text) + "]\n";
					if (!Cambro.Web.Helpers.StripHtml(ContactName.Text).Equals(CurrentPromoter.ContactName))
						oldDetails += "ContactName: [" + CurrentPromoter.ContactName + "] - [" + Cambro.Web.Helpers.StripHtml(ContactName.Text) + "]\n";
					if (!Cambro.Web.Helpers.StripHtml(PhoneNumber.Text).Equals(CurrentPromoter.PhoneNumber))
						oldDetails += "PhoneNumber: [" + CurrentPromoter.PhoneNumber + "] - [" + Cambro.Web.Helpers.StripHtml(PhoneNumber.Text) + "]\n";
					if (!Cambro.Web.Helpers.StripHtml(AddressStreet.Text).Equals(CurrentPromoter.AddressStreet))
						oldDetails += "AddressStreet: [" + CurrentPromoter.AddressStreet + "] - [" + Cambro.Web.Helpers.StripHtml(AddressStreet.Text) + "]\n";
					if (!Cambro.Web.Helpers.StripHtml(AddressArea.Text).Equals(CurrentPromoter.AddressArea))
						oldDetails += "AddressArea: [" + CurrentPromoter.AddressArea + "] - [" + Cambro.Web.Helpers.StripHtml(AddressArea.Text) + "]\n";
					if (!Cambro.Web.Helpers.StripHtml(AddressTown.Text).Equals(CurrentPromoter.AddressTown))
						oldDetails += "AddressTown: [" + CurrentPromoter.AddressTown + "] - [" + Cambro.Web.Helpers.StripHtml(AddressTown.Text) + "]\n";
					if (!Cambro.Web.Helpers.StripHtml(AddressCounty.Text).Equals(CurrentPromoter.AddressCounty))
						oldDetails += "AddressCounty: [" + CurrentPromoter.AddressCounty + "] - [" + Cambro.Web.Helpers.StripHtml(AddressCounty.Text) + "]\n";
					if (!Cambro.Web.Helpers.StripHtml(AddressPostcode.Text).Equals(CurrentPromoter.AddressPostcode))
						oldDetails += "AddressPostcode: [" + CurrentPromoter.AddressPostcode + "] - [" + Cambro.Web.Helpers.StripHtml(AddressPostcode.Text) + "]\n";
					if (!int.Parse(AddressCountry.SelectedValue).Equals(CurrentPromoter.AddressCountryK))
						oldDetails += "CountryK: [" + CurrentPromoter.AddressCountryK + "] - [" + int.Parse(AddressCountry.SelectedValue).ToString() + "]\n";
					if (!((Promoter.VatStatusEnum)Convert.ToInt32(VatStatusDropDownList.SelectedValue)).Equals(CurrentPromoter.VatStatus))
						oldDetails += "VatStatus: [" + CurrentPromoter.VatStatus.ToString() + "] - [" + ((Promoter.VatStatusEnum)Convert.ToInt32(VatStatusDropDownList.SelectedValue)).ToString() + "]\n";
					if (!Cambro.Web.Helpers.StripHtml(VatNumberTextBox.Text.Trim()).Equals(CurrentPromoter.VatNumber))
						oldDetails += "VatNumber: [" + CurrentPromoter.VatNumber + "] - [" + Cambro.Web.Helpers.StripHtml(VatNumberTextBox.Text.Trim()) + "]\n";
					if (!Convert.ToInt32(VatCountryDropDownList.SelectedValue).Equals(CurrentPromoter.VatCountryK))
						oldDetails += "VatCountryK: [" + CurrentPromoter.VatCountryK.ToString() + "] - [" + Convert.ToInt32(VatCountryDropDownList.SelectedValue).ToString() + "]\n";
                    if (!Cambro.Web.Helpers.StripHtml(BankNameTextBox.Text).Equals(CurrentPromoter.BankName))
                        oldDetails += "BankName: [" + CurrentPromoter.BankName + "] - [" + Cambro.Web.Helpers.StripHtml(BankNameTextBox.Text) + "]\n";
                    if (!Cambro.Web.Helpers.StripHtml(BankAccountNameTextBox.Text).Equals(CurrentPromoter.BankAccountName))
                        oldDetails += "BankAccountName: [" + CurrentPromoter.BankAccountName + "] - [" + Cambro.Web.Helpers.StripHtml(BankAccountNameTextBox.Text) + "]\n";
                    if (!Cambro.Web.Helpers.StripHtml(BankAccountNumberTextBox.Text).Equals(CurrentPromoter.BankAccountNumber))
                        oldDetails += "BankAccountNumber: [" + CurrentPromoter.BankAccountNumber + "] - [" + Cambro.Web.Helpers.StripHtml(BankAccountNumberTextBox.Text) + "]\n";
                    if (!Cambro.Web.Helpers.StripHtml(BankAccountSortCodeTextBox.Text).Equals(CurrentPromoter.BankAccountSortCode))
                        oldDetails += "BankAccountSortCode: [" + CurrentPromoter.BankAccountSortCode + "] - [" + Cambro.Web.Helpers.StripHtml(BankNameTextBox.Text) + "]\n";
					if (uiAgency.Checked != CurrentPromoter.IsAgency)
						oldDetails += "IsAgency: [" + CurrentPromoter.IsAgency.ToString() + "] - [" + uiAgency.Checked.ToString() + "]\n";
					if (oldDetails.Length > 0)
						CurrentPromoter.AdminNote += "\n" + Usr.Current.NickName + " (" + Usr.Current.K.ToString() + ") changed these details on " + DateTime.Now.ToString() + ":\n" + oldDetails;
					#endregion
				}
				else
				{
					Guid DuplicateGuid = (Guid)ViewState["PromoterDuplicateGuid"];
					PromoterSet ps = new PromoterSet(new Query(new Q(Promoter.Columns.DuplicateGuid, DuplicateGuid)));
					if (ps.Count > 0)
					{
						Response.Redirect(ps[0].UrlApp("edit"));
						return;
					}
					else
					{
						#region Initialise promoter record
						CurrentPromoter = new Promoter();
						CurrentPromoter.DateTimeSignUp = DateTime.Now;
						CurrentPromoter.AddedByUsrK = Usr.Current.K;
						CurrentPromoter.HasGuestlist = true;
						CurrentPromoter.GuestlistCharge = 0.25m;
						CurrentPromoter.GuestlistCredit = 20;
						CurrentPromoter.GuestlistCreditLimit = 0;
						CurrentPromoter.Status = Promoter.StatusEnum.Enabled;
						CurrentPromoter.PricingMultiplier = 1.0;
						CurrentPromoter.TotalPaid = 0;
						CurrentPromoter.DuplicateGuid = (Guid)ViewState["PromoterDuplicateGuid"];
						
						CurrentPromoter.LetterType = Promoter.LetterTypes.CurrentNewPromoter;
						CurrentPromoter.LetterStatus = Promoter.LetterStatusEnum.New;
						CurrentPromoter.IsSkeleton = false;
						CurrentPromoter.OfferType = Promoter.OfferTypes.None;
						Random r = new Random();
						CurrentPromoter.AccessCodeRandom = r.Next(1000, 9999).ToString() + r.Next(1000, 9999).ToString();
						
						CurrentPromoter.ClientSector = (Promoter.ClientSectorEnum)Convert.ToInt32(Sector.SelectedValue);
						CurrentPromoter.SalesCampaignK = int.Parse(SalesCampaignDropDown.SelectedValue);
						

						if (!Usr.Current.IsAdmin)
						{
							CurrentPromoter.PrimaryUsrK = Usr.Current.K;
							CurrentPromoter.AddedMethod = Promoter.AddedMedhods.EndUser;
						}
						else
						{
							CurrentPromoter.AddedMethod = Promoter.AddedMedhods.SalesUser;
						}

						// assign new Promoter to Usr.Current if they are on a promoter sales team or to a randomly assigned promoter sales person, as requested by Dave 7/2/07
						if (Usr.Current.SalesTeam > 0)
						{
							CurrentPromoter.SalesStatus = Promoter.SalesStatusEnum.Proactive;
							CurrentPromoter.SalesUsrK = Usr.Current.K;
						}
						else
						{
							CurrentPromoter.SalesStatus = Promoter.SalesStatusEnum.New;
							// Randomly assign a sales usr
							List<Usr> promoterSalesUsrs;
							if (CurrentPromoter.ClientSector.Equals(Promoter.ClientSectorEnum.Promoter))
								promoterSalesUsrs = Usr.GetNewPromoterSalesUsrsNameAndK().ToList();
							else
								promoterSalesUsrs = new List<Usr>() { new Usr(1) };
							CurrentPromoter.SalesUsrK = promoterSalesUsrs[r.Next(0, promoterSalesUsrs.Count)].K;
						}											

						CurrentPromoter.SalesStatusExpires = DateTime.Today.AddMonths(3);
						CurrentPromoter.SalesNextCall = DateTime.Now.AddDays(3);
						// If first call is on a weekend, then make it Monday
						if(CurrentPromoter.SalesNextCall.DayOfWeek == DayOfWeek.Saturday)
							CurrentPromoter.SalesNextCall = DateTime.Now.AddDays(2);
						else if(CurrentPromoter.SalesNextCall.DayOfWeek == DayOfWeek.Sunday)
							CurrentPromoter.SalesNextCall = DateTime.Now.AddDays(1);

						#endregion
					}
				}
				#region Update promoter record with form contents
				CurrentPromoter.Name = Cambro.Web.Helpers.StripHtml(Name.Text);
				CurrentPromoter.ContactName = Cambro.Web.Helpers.StripHtml(ContactName.Text);
				CurrentPromoter.PhoneNumber = Cambro.Web.Helpers.StripHtml(PhoneNumber.Text);
				CurrentPromoter.AddressStreet = Cambro.Web.Helpers.StripHtml(AddressStreet.Text);
				CurrentPromoter.AddressArea = Cambro.Web.Helpers.StripHtml(AddressArea.Text);
				CurrentPromoter.AddressTown = Cambro.Web.Helpers.StripHtml(AddressTown.Text);
				CurrentPromoter.AddressCounty = Cambro.Web.Helpers.StripHtml(AddressCounty.Text);
				CurrentPromoter.AddressPostcode = Cambro.Web.Helpers.StripHtml(AddressPostcode.Text);

				bool updateTicketInvoices = false;
				// new VAT details, for Ticket System. 22/5/07
				if (CurrentPromoter.VatStatus != Promoter.VatStatusEnum.Registered && CurrentPromoter.VatStatus != (Promoter.VatStatusEnum)Convert.ToInt32(VatStatusDropDownList.SelectedValue))
					updateTicketInvoices = true;

				CurrentPromoter.VatStatus = (Promoter.VatStatusEnum)Convert.ToInt32(VatStatusDropDownList.SelectedValue);
				CurrentPromoter.VatCountryK = Convert.ToInt32(VatCountryDropDownList.SelectedValue);
				CurrentPromoter.VatNumber = Cambro.Web.Helpers.StripHtml(VatNumberTextBox.Text.Trim());
				
				// new Admin only data entry fields, as requested by Dave 7/2/07
				if (Usr.Current.IsAdmin || Usr.Current.IsSuperAdmin)
				{
					CurrentPromoter.AccountsEmail = Cambro.Web.Helpers.StripHtml(AccountsEmail.Text);
					CurrentPromoter.AccountsName = Cambro.Web.Helpers.StripHtml(AccountsName.Text);
					CurrentPromoter.AccountsPhone = Cambro.Web.Helpers.StripHtml(AccountsPhone.Text);
					CurrentPromoter.ContactPersonalTitle = Cambro.Web.Helpers.StripHtml(PersonalTitle.Text);
					CurrentPromoter.ContactTitle = Cambro.Web.Helpers.StripHtml(JobTitle.Text);
					CurrentPromoter.PhoneNumber2 = Cambro.Web.Helpers.StripHtml(PhoneNumber2.Text);
					CurrentPromoter.WebAddress = Cambro.Web.Helpers.StripHtml(WebAddress.Text);
					CurrentPromoter.ClientSector = (Promoter.ClientSectorEnum)Convert.ToInt32(Sector.SelectedValue);
					CurrentPromoter.SalesCampaignK = int.Parse(SalesCampaignDropDown.SelectedValue);
					CurrentPromoter.IsAgency = uiAgency.Checked;

                    // new admin only bank details, as requested by Dave 15/6/07
                    CurrentPromoter.BankName = Cambro.Web.Helpers.StripHtml(BankNameTextBox.Text);
                    CurrentPromoter.BankAccountName = Cambro.Web.Helpers.StripHtml(BankAccountNameTextBox.Text);
                    CurrentPromoter.BankAccountNumber = Cambro.Web.Helpers.StripHtml(BankAccountNumberTextBox.Text);
                    CurrentPromoter.BankAccountSortCode = Cambro.Web.Helpers.StripHtml(BankAccountSortCodeTextBox.Text);

					
					if (this.AccessMultiRadio.Checked && this.uiAccessUsersMultiSelector.Count > 0 && (Usr.Current.IsAdmin || Usr.Current.IsSuperAdmin)){
						CurrentPromoter.PrimaryUsrK = int.Parse(uiPrimaryUserDropDown.SelectedValue);
					}else
					{
						CurrentPromoter.PrimaryUsrK = 0;
						CurrentPromoter.AdminNote += "\n" + Usr.Current.NickName + " (" + Usr.Current.K.ToString() + ") has setup this account with no primary user - " + DateTime.Now.ToString() + "\n";
					}
				}

				if (!IsEdit)
				{
					if (CurrentPromoter.PrimaryUsrK > 0)
						CurrentPromoter.AddQuestionsThread(CurrentPromoter.PrimaryUsr, Cambro.Web.Helpers.StripHtml(Name.Text));
					else
						CurrentPromoter.AddQuestionsThread(Usr.Current, Cambro.Web.Helpers.StripHtml(Name.Text));
				}

				Country newCountry = new Country(int.Parse(AddressCountry.SelectedValue));
				CurrentPromoter.AddressCountryK = newCountry.K;
				CurrentPromoter.Update();
				if (updateTicketInvoices)
					CurrentPromoter.UpdateTicketInvoiceItemTaxCode();
				CurrentPromoter.CreateUniqueUrlName();
				#endregion
				#region Add / remove selected users
				if (CurrentPromoter.PrimaryUsrK == Usr.Current.K || Usr.Current.IsAdmin || Usr.Current.IsSuperAdmin)
				{
					ArrayList SelectedUsers = new ArrayList();
					ArrayList CurrentUsers = new ArrayList();
					if (CurrentPromoter.PrimaryUsrK > 0)
					{
						SelectedUsers.Add(CurrentPromoter.PrimaryUsrK);
					}
					if (AccessMultiRadio.Checked)
					{
						foreach (var pair in this.uiAccessUsersMultiSelector.Selections)
						{
							Usr u = new Usr(int.Parse(pair.Value));
							if (!SelectedUsers.Contains(u.K))
								SelectedUsers.Add(u.K);
						}
					}
					foreach (Usr u in CurrentPromoter.AdminUsrs)
					{
						if (!CurrentUsers.Contains(u.K))
							CurrentUsers.Add(u.K);
					}

					if (SelectedUsers.Count > 0)
					{
						Usr JohnB = new Usr(1);
						Thread t = new Thread(CurrentPromoter.QuestionsThreadK);
						foreach (int usrK in SelectedUsers)
						{
							if (!CurrentUsers.Contains(usrK))
							{
								PromoterUsr pu = new PromoterUsr();
								pu.PromoterK = CurrentPromoter.K;
								pu.UsrK = usrK;
								pu.Update();

								t.Invite(new List<int>(new int[] { usrK }), JohnB, DateTime.Now, new List<int>(), !IsEdit, null, false);

								ThreadUsr tu = new ThreadUsr(CurrentPromoter.QuestionsThreadK, usrK);
								tu.Favourite = true;
								tu.Update();

								CurrentPromoter.AdminNote += "\n" + Usr.Current.NickName + " (" + Usr.Current.K.ToString() + ") added new user to admins - " + DateTime.Now.ToString() + " - " + pu.UsrK.ToString() + " (" + pu.Usr.NickName + ")\n";
								CurrentPromoter.Update();

								pu.Usr.UpdateIsPromoter();
							}
						}
					}
					//if (!AccessNoAccountUsersRadio.Checked)
					//{
						foreach (int usrK in CurrentUsers)
						{
							if (!SelectedUsers.Contains(usrK))
							{
								PromoterUsr pu = new PromoterUsr(CurrentPromoter.K, usrK);

								CurrentPromoter.AdminNote += "\n" + Usr.Current.NickName + " (" + Usr.Current.K.ToString() + ") removed user from the admins - " + DateTime.Now.ToString() + " - " + pu.UsrK.ToString() + " (" + pu.Usr.NickName + ")\n";
								CurrentPromoter.Update();

								pu.Delete();
								pu.Update();

								pu.Usr.UpdateIsPromoter();

								try
								{
									ThreadUsr tu = new ThreadUsr(CurrentPromoter.QuestionsThreadK, usrK);
									tu.Delete();
									tu.Update();

									UpdateTotalParticipantsJob job = new UpdateTotalParticipantsJob(tu.Thread);
									job.ExecuteSynchronously();

								}
								catch { }
							}
						}
					//}
					//else
					//{
					//    foreach (int usrK in CurrentUsers)
					//    {
					//        PromoterUsr pu = new PromoterUsr(CurrentPromoter.K, usrK);

					//        CurrentPromoter.AdminNote += "\n" + Usr.Current.NickName + " (" + Usr.Current.K.ToString() + ") removed user from the admins - " + DateTime.Now.ToString() + " - " + pu.UsrK.ToString() + " (" + pu.Usr.NickName + ")\n";
					//        CurrentPromoter.Update();

					//        pu.Delete();
					//        pu.Update();

					//        pu.Usr.UpdateIsPromoter();

					//        try
					//        {
					//            ThreadUsr tu = new ThreadUsr(CurrentPromoter.QuestionsThreadK, usrK);
					//            tu.Delete();
					//            tu.Update();
					//            tu.Thread.UpdateTotalParticipants();
					//        }
					//        catch { }
					//    }
					//}
				}
				#endregion
				#region Add / remove selected brands
					ArrayList SelectedBrands = new ArrayList();
					ArrayList CurrentBrands = new ArrayList();
					if (AccountTypeRadioEvents.Checked)
					{
						foreach (var pair in this.uiBrandMultiSelector.Selections)
						{
							Brand b = new Brand(int.Parse(pair.Value));
							if (!SelectedBrands.Contains(b.K))
								SelectedBrands.Add(b.K);
						}
					}
					CurrentPromoter.AllBrands = null;
					foreach (Brand b in CurrentPromoter.AllBrands)
					{
						if (!CurrentBrands.Contains(b.K))
							CurrentBrands.Add(b.K);
					}

					string failedBrands = "";

					foreach (int brandK in SelectedBrands)
					{
						if (!CurrentBrands.Contains(brandK))
						{
							Brand b = new Brand(brandK);
							bool changeStatus = false;
							if (b.PromoterK == 0)
							{
								CurrentPromoter.AdminNote += "\n" + Usr.Current.NickName + " (" + Usr.Current.K.ToString() + ") added brand - " + DateTime.Now.ToString() + " - " + b.K + " (" + b.Name + ") - ";
								b.PromoterK = CurrentPromoter.K;
								changeStatus = true;
							}
							else if (b.PromoterStatus.Equals(Brand.PromoterStatusEnum.Unconfirmed))
							{
								CurrentPromoter.AdminNote += "\n" + Usr.Current.NickName + " (" + Usr.Current.K.ToString() + ") added brand - " + DateTime.Now.ToString() + " - " + b.K + " (" + b.Name + ") - removed from promoter " + b.PromoterK + " (" + b.Promoter.Name + ") - ";
								b.PromoterK = CurrentPromoter.K;
								changeStatus = true;
							}
							else //brand owned by someone else!
							{
								CurrentPromoter.AdminNote += "\n" + Usr.Current.NickName + " (" + Usr.Current.K.ToString() + ") attempted to add brand - " + DateTime.Now.ToString() + " - " + b.K + " (" + b.Name + "), but it's already confirmed to promoter " + b.PromoterK + " (" + b.Promoter.Name + ")\n";

								failedBrands = (failedBrands.Length == 0 ? "" : ", ") + b.Name;
							}
							if (changeStatus)
							{
								bool foundBrandOwner = false;
								CurrentPromoter.AdminUsrs = null;
								foreach (Usr u in CurrentPromoter.AdminUsrs)
								{
									if (b.OwnerUsrK == u.K)
										foundBrandOwner = true;
								}
								if (foundBrandOwner || Usr.Current.IsAdmin)
								{
									b.PromoterStatus = Brand.PromoterStatusEnum.Confirmed;
									CurrentPromoter.AdminNote += "(status confirmed)\n";
								}
								else
								{
									b.PromoterStatus = Brand.PromoterStatusEnum.Unconfirmed;
									CurrentPromoter.AdminNote += "(status new)\n";
								}
							}
							b.Update();
							CurrentPromoter.Update();
						}
					}
					foreach (int brandK in CurrentBrands)
					{
						if (!SelectedBrands.Contains(brandK))
						{
							Brand b = new Brand(brandK);
							CurrentPromoter.AdminNote += "\n" + Usr.Current.NickName + " (" + Usr.Current.K.ToString() + ") removed brand - " + DateTime.Now.ToString() + " - " + b.K + " (" + b.Name + ")";
							b.PromoterStatus = Brand.PromoterStatusEnum.Unconfirmed;
							b.PromoterK = 0;
							b.Update();
							CurrentPromoter.Update();
						}
					}
					#endregion
				#region Add / remove selected venues
				ArrayList SelectedVenues = new ArrayList();
				ArrayList CurrentVenues = new ArrayList();
				if (VenuesRadioYes.Checked)
				{
					foreach (var pair in this.uiVenuesMultiSelector.Selections)
					{
						Venue v = new Venue(int.Parse(pair.Value));
						if (!SelectedVenues.Contains(v.K))
							SelectedVenues.Add(v.K);
					}
				}
				CurrentPromoter.AllVenues = null;
				foreach (Venue v in CurrentPromoter.AllVenues)
				{
					if (!CurrentVenues.Contains(v.K))
						CurrentVenues.Add(v.K);
				}

				string failedVenues = "";

				foreach (int venueK in SelectedVenues)
				{
					if (!CurrentVenues.Contains(venueK))
					{
						Venue v = new Venue(venueK);
						bool changeStatus = false;
						if (v.PromoterK == 0)
						{
							CurrentPromoter.AdminNote += "\n" + Usr.Current.NickName + " (" + Usr.Current.K.ToString() + ") added venue - " + DateTime.Now.ToString() + " - " + v.K + " (" + v.Name + ") - ";
							v.PromoterK = CurrentPromoter.K;
							changeStatus = true;
						}
						else if (v.PromoterStatus.Equals(Venue.PromoterStatusEnum.Unconfirmed))
						{
							CurrentPromoter.AdminNote += "\n" + Usr.Current.NickName + " (" + Usr.Current.K.ToString() + ") added venue - " + DateTime.Now.ToString() + " - " + v.K + " (" + v.Name + ") - removed from promoter " + v.PromoterK + " (" + v.Promoter.Name + ") - ";
							v.PromoterK = CurrentPromoter.K;
							changeStatus = true;
						}
						else //venue owned by someone else!
						{
							CurrentPromoter.AdminNote += "\n" + Usr.Current.NickName + " (" + Usr.Current.K.ToString() + ") attempted to add venue - " + DateTime.Now.ToString() + " - " + v.K + " (" + v.Name + "), but it's already confirmed to promoter " + v.PromoterK + " (" + v.Promoter.Name + ")\n";

							failedVenues = (failedVenues.Length == 0 ? "" : ", ") + v.Name + " in " + v.Place.Name;
						}
						if (changeStatus)
						{
							bool foundVenueOwner = false;
							CurrentPromoter.AdminUsrs = null;
							foreach (Usr u in CurrentPromoter.AdminUsrs)
							{
								if (v.OwnerUsrK == u.K)
									foundVenueOwner = true;
							}
							if (foundVenueOwner || Usr.Current.IsAdmin)
							{
								v.PromoterStatus = Venue.PromoterStatusEnum.Confirmed;
								CurrentPromoter.AdminNote += "(status confirmed)\n";
							}
							else
							{
								v.PromoterStatus = Venue.PromoterStatusEnum.Unconfirmed;
								CurrentPromoter.AdminNote += "(status new)\n";
							}
						}
						v.Update();
						CurrentPromoter.Update();
					}
				}
				foreach (int venueK in CurrentVenues)
				{
					if (!SelectedVenues.Contains(venueK))
					{
						Venue v = new Venue(venueK);
						CurrentPromoter.AdminNote += "\n" + Usr.Current.NickName + " (" + Usr.Current.K.ToString() + ") removed venue - " + DateTime.Now.ToString() + " - " + v.K + " (" + v.Name + ")";
						v.PromoterStatus = Venue.PromoterStatusEnum.Unconfirmed;
						v.PromoterK = 0;
						v.Update();
						CurrentPromoter.Update();
					}
				}
				#endregion

				this.ViewState["CurrentPromoterK"] = CurrentPromoter.K;
				CurrentPromoter = new Promoter(CurrentPromoter.K);
				CurrentPromoter.FixQuestionsThreadUsrs();
				CurrentPromoter.UpdateModerators();

				Usr.Current.LegalTermsPromoter2 = true;
				Usr.Current.Update();


				if (failedBrands.Length > 0 || failedVenues.Length > 0)
				{
					//show error form...
					BrandErrorLabel.Text = failedBrands;
					VenueErrorLabel.Text = failedVenues;
					BrandErrorPanel.Visible = failedBrands.Length > 0;
					VenueErrorPanel.Visible = failedVenues.Length > 0;

					ChangePanel(PanelBrandVenueError);
				}
				else
				{
					if (IsEdit)
					{
						if (Usr.Current.IsAdmin)
							Response.Redirect(CurrentPromoter.Url());
						else
							ChangePanel(PanelEditDone);
					}
					else
					{
						if (!Usr.Current.IsAdmin)
						{
							Thread t = new Thread(CurrentPromoter.QuestionsThreadK);
							Response.Redirect(t.Url());
						}
						else
						{
							Response.Redirect(CurrentPromoter.Url());
						}
					}
				}

			}
		}
		#endregion
		#region BuildSignUpForm()
		void BuildSignUpForm()
		{
			CountrySet cs = new CountrySet(new Query());
			AddressCountry.DataSource = cs;
			AddressCountry.DataTextField = "Name";
			AddressCountry.DataValueField = "K";
			AddressCountry.DataBind();

			VatCountryDropDownList.DataSource = cs;
			VatCountryDropDownList.DataTextField = "Name";
			VatCountryDropDownList.DataValueField = "K";
			VatCountryDropDownList.DataBind();
			VatCountryDropDownList.Items.Insert(0, new ListItem("United Kingdom", "224")); // put this also at top of list - save Tim a bit of time
			VatCountryDropDownList.Items.Insert(0, new ListItem("", "0"));

			VatStatusDropDownList.Items.Clear();
			VatStatusDropDownList.Items.Add(new ListItem("", "0"));
			VatStatusDropDownList.Items.Add(new ListItem(Utilities.CamelCaseToString(Promoter.VatStatusEnum.NotRegistered.ToString()), Convert.ToInt32(Promoter.VatStatusEnum.NotRegistered).ToString()));
			VatStatusDropDownList.Items.Add(new ListItem(Promoter.VatStatusEnum.Registered.ToString(), Convert.ToInt32(Promoter.VatStatusEnum.Registered).ToString()));

			if (IsEdit)
			{
				ContactName.Text = CurrentPromoter.ContactName;
				Name.Text = CurrentPromoter.Name;
				PhoneNumber.Text = CurrentPromoter.PhoneNumber;
				AddressStreet.Text = CurrentPromoter.AddressStreet;
				AddressArea.Text = CurrentPromoter.AddressArea;
				AddressTown.Text = CurrentPromoter.AddressTown;
				AddressCounty.Text = CurrentPromoter.AddressCounty;
				AddressPostcode.Text = CurrentPromoter.AddressPostcode;
				try
				{
					AddressCountry.SelectedValue = CurrentPromoter.AddressCountryK.ToString();
				}
				catch { }

				// Vat details
				VatStatusDropDownList.SelectedValue = Convert.ToInt32(CurrentPromoter.VatStatus).ToString();
				VatNumberTextBox.Text = CurrentPromoter.VatNumber;
				try
				{
                    VatCountryDropDownList.SelectedValue = CurrentPromoter.VatCountryK.ToString();
				}
				catch { }

				// new Admin only data entry fields, as requested by Dave 7/2/07
				AccountsEmail.Text = CurrentPromoter.AccountsEmail;
				AccountsName.Text = CurrentPromoter.AccountsName;
				AccountsPhone.Text = CurrentPromoter.AccountsPhone;
				PersonalTitle.Text = CurrentPromoter.ContactPersonalTitle;
				JobTitle.Text = CurrentPromoter.ContactTitle;
				PhoneNumber2.Text = CurrentPromoter.PhoneNumber2;
				WebAddress.Text = CurrentPromoter.WebAddress;
				uiAgency.Checked = CurrentPromoter.IsAgency;
				Sector.SelectedValue = Convert.ToInt32(CurrentPromoter.ClientSector).ToString();
				SalesCampaignDropDown.SelectedValue = CurrentPromoter.SalesCampaignK.ToString();

                // new admin only bank details, as requested by Dave 15/6/07
                BankNameTextBox.Text = CurrentPromoter.BankName;
                BankAccountNameTextBox.Text = CurrentPromoter.BankAccountName;
                BankAccountNumberTextBox.Text = CurrentPromoter.BankAccountNumber;
                BankAccountSortCodeTextBox.Text = CurrentPromoter.BankAccountSortCode;

				AccountTypeRadioEvents.Checked = CurrentPromoter.AllBrands.Count > 0;
				AccountTypeRadioNoEvents.Checked = CurrentPromoter.AllBrands.Count == 0;
				BrandsTr.Visible = AccountTypeRadioEvents.Checked;
				if (AccountTypeRadioEvents.Checked)
				{
					foreach (Brand b in CurrentPromoter.AllBrands)
					{
						this.uiBrandMultiSelector.Add(b.Name, b.K.ToString());
					}
				}


				VenuesRadioYes.Checked = CurrentPromoter.AllVenues.Count > 0;
				VenuesRadioNo.Checked = CurrentPromoter.AllVenues.Count == 0;
				VenuesTr.Visible = VenuesRadioYes.Checked;
				if (VenuesRadioYes.Checked)
				{
					foreach (Venue v in CurrentPromoter.AllVenues)
					{
						this.uiVenuesMultiSelector.Add(v.Name + " in " + v.Place.Name, v.K.ToString());
					}
				}

				AccessP.Visible = CurrentPromoter.PrimaryUsrK == Usr.Current.K || Usr.Current.IsAdmin || Usr.Current.IsSuperAdmin;
				NoAccessP.Visible = CurrentPromoter.PrimaryUsrK != Usr.Current.K && !Usr.Current.IsAdmin && !Usr.Current.IsSuperAdmin;
				try
				{
					PrimaryUsrSpan.InnerHtml = CurrentPromoter.PrimaryUsrLink;
				}
				catch{}

				if (CurrentPromoter.PrimaryUsrK == Usr.Current.K || Usr.Current.IsAdmin || Usr.Current.IsSuperAdmin)
				{
					if (CurrentPromoter.AdminUsrs.Count > 1 || Usr.Current.IsAdmin || Usr.Current.IsSuperAdmin)
					{
						if (CurrentPromoter.PrimaryUsrK > 0 && (Usr.Current.IsAdmin || Usr.Current.IsSuperAdmin))
						{
							this.uiAccessUsersMultiSelector.Add(CurrentPromoter.PrimaryUsr.NickName, CurrentPromoter.PrimaryUsrK.ToString());
							this.uiPrimaryUserDropDown.Items.Add(new ListItem(CurrentPromoter.PrimaryUsr.NickName, CurrentPromoter.PrimaryUsrK.ToString()));
							
						}
						foreach (Usr u in CurrentPromoter.AdminUsrs)
						{
							if (u.K != CurrentPromoter.PrimaryUsrK)
							{
								this.uiAccessUsersMultiSelector.Add(u.NickName, u.K.ToString());
							}
						}
					}
					AccessMultiRadio.Checked = CurrentPromoter.AdminUsrs.Count > 1 || (CurrentPromoter.AdminUsrs.Count == 1 && (Usr.Current.IsAdmin || Usr.Current.IsSuperAdmin));
					AccessJustMeRadio.Checked = CurrentPromoter.AdminUsrs.Count == 1 && !AccessMultiRadio.Checked;
					AccessNoAccountUsersRadio.Checked = CurrentPromoter.AdminUsrs.Count == 0 && !AccessMultiRadio.Checked;
					MultiAccess.Visible = AccessMultiRadio.Checked;
				}
			}
			else
			{
				ContactName.Text = Usr.Current.FirstName + " " + Usr.Current.LastName;

				if (Country.FilterK > 0)
					AddressCountry.SelectedValue = Country.FilterK.ToString();

				AccountTypeRadioEvents.Checked = true;
				AccountTypeRadioNoEvents.Checked = false;
			}
		}
		#endregion
		#region AccountTypeRadioChanged
		public void AccountTypeRadioChanged(object o, System.EventArgs e)
		{
			BrandsTr.Visible = AccountTypeRadioEvents.Checked;
			ContainerPage.AnchorSkip("AccountType");
		}
		#endregion
		#region VenuesRadioChanged
		public void VenuesRadioChanged(object o, System.EventArgs e)
		{
			VenuesTr.Visible = VenuesRadioYes.Checked;
			ContainerPage.AnchorSkip("Venues");
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
		#region VatStatusVal
		protected void VatStatusVal(object o, ServerValidateEventArgs e)
		{
			if (((Promoter.VatStatusEnum)Convert.ToInt32(this.VatStatusDropDownList.SelectedValue)) == Promoter.VatStatusEnum.Registered)
			{
				string errorMessage = "";
				if (this.VatNumberTextBox.Text.Trim().Length == 0)
				{
					errorMessage += "<p>Please enter your VAT registration number.</p>";
					e.IsValid = false;
				}
				if (this.VatCountryDropDownList.SelectedValue == "0")
				{
					errorMessage += "<p>Please enter the country where you are VAT registered.</p>";
					e.IsValid = false;
				}
				this.VatStatusCustomValidator.ErrorMessage = errorMessage;
			}
			else if (((Promoter.VatStatusEnum)Convert.ToInt32(this.VatStatusDropDownList.SelectedValue)) == Promoter.VatStatusEnum.Unknown)
			{
				this.VatStatusCustomValidator.ErrorMessage = "<p>Please enter your VAT registration status.</p>";
				e.IsValid = false;
			}
		}
		#endregion
		#region EmailCustVal
		protected void EmailCustVal(object o, ServerValidateEventArgs e)
		{
			if (e.Value.Length > 0)
			{
				Regex EmailRegex = new Regex(Cambro.Misc.RegEx.Email);
				e.IsValid = EmailRegex.IsMatch(e.Value.Trim());
			}
			else
				e.IsValid = true;
		}
		#endregion
		#region VenuesMultiVal
		protected void VenuesMultiVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = VenuesRadioNo.Checked || this.uiVenuesMultiSelector.Count > 0;
		}
		#endregion
		#region BrandMultiVal
		protected void BrandMultiVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = AccountTypeRadioNoEvents.Checked || this.uiBrandMultiSelector.Count > 0;
		}
		#endregion
		#region AccessRadioChanged
		public void AccessRadioChanged(object o, System.EventArgs e)
		{
			ContainerPage.AnchorSkip("PeopleWithAccess");
			MultiAccess.Visible = AccessMultiRadio.Checked;
		}
		#endregion
		#region TermsVal
		public void TermsVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = TermsCheckbox.Checked;
		}
		#endregion
		#endregion

		#region PanelBrandError
		#region PanelBrandErrorNext_Click
		protected void PanelBrandErrorNext_Click(object sender, EventArgs eventArgs)
		{
			if (IsEdit)
			{
				if (Usr.Current.IsAdmin)
					Response.Redirect(CurrentPromoter.Url());
				else
					ChangePanel(PanelEditDone);
			}
			else
			{
				CurrentPromoter = new Promoter((int)this.ViewState["CurrentPromoterK"]);
				Thread t = new Thread(CurrentPromoter.QuestionsThreadK);
				Response.Redirect(t.Url());
			}
		}
		#endregion
		#endregion

		#region PanelPic
		#region Pic_Load
		public void Pic_Load(object o, System.EventArgs e)
		{
			if (Mode.Equals(Modes.Pic) && CurrentPromoter != null)
			{
				if (!CanEdit)
					throw new Exception("You can't edit this promoter!");

				Pic.InputObject = CurrentPromoter;
				if (!Page.IsPostBack)
				{
					Pic.InitPic();
				}
			}
		}
		#endregion
		#region PicSaved
		public void PicSaved(object o, System.EventArgs e)
		{
			PicNext();
		}
		#endregion
		#region PicNoPic
		public void PicNoPic(object o, System.EventArgs e)
		{
			PicNext();
		}
		#endregion
		#region PicNext
		void PicNext()
		{
			if (IsEdit)
			{
				if (Usr.Current.IsAdmin)
					Response.Redirect(CurrentPromoter.Url());
				else
					ChangePanel(PanelEditDone);
			}
			else
				ChangePanel(PanelAddDone);
		}
		#endregion
		#endregion

		#region ChangePanel
		void ChangePanel(Panel p)
		{
			PanelSignUpForm.Visible = p.Equals(PanelSignUpForm);
			PanelAlreadyPromoter.Visible = p.Equals(PanelAlreadyPromoter);
			PanelEditDone.Visible = p.Equals(PanelEditDone);
			PanelAddDone.Visible = p.Equals(PanelAddDone);
			PanelPic.Visible = p.Equals(PanelPic);
			PanelBrandVenueError.Visible = p.Equals(PanelBrandVenueError);
		}
		#endregion

		#region CanEdit
		protected bool CanEdit
		{
			get
			{
				return Usr.Current != null && (Usr.Current.IsAdmin || Usr.Current.IsPromoterK(CurrentPromoter.K));
			}
		}
		#endregion
		#region IsEdit
		bool IsEdit
		{
			get
			{
				return ContainerPage.Url.HasPromoterObjectFilter;
			}
		}
		#endregion
		#region Modes
		public enum Modes
		{
			None,
			Add,
			List,
			Edit,
			Pic,
			ConfirmBrand,
			ConfirmVenue
		}
		#endregion
		#region Mode
		Modes Mode
		{
			get
			{
				if (ContainerPage.Url["Mode"].Equals("confirmbrand"))
					return Modes.ConfirmBrand;
				else if (ContainerPage.Url["Mode"].Equals("confirmvenue"))
					return Modes.ConfirmVenue;
				else if (ContainerPage.Url["Mode"].Equals("Add") || !ContainerPage.Url.HasPromoterObjectFilter)
					return Modes.Add;
				else if (ContainerPage.Url["Mode"].Equals("List"))
					return Modes.List;
				else if (ContainerPage.Url["Mode"].Equals("Pic"))
					return Modes.Pic;
				else if (ContainerPage.Url["Mode"].Equals("Edit") || ContainerPage.Url.CurrentApplication.Equals("edit"))
					return Modes.Edit;
				else
					return Modes.None;
			}
		}
		#endregion

	}
}
