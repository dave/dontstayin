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
using System.Collections.Generic;
using Bobs.DataHolders;
using Common.Clocks;
using Common;

namespace Spotted.Pages.Promoters
{
    public partial class BannerOptions : PromoterUserControl
    {
        #region Page_Init
        protected override void Page_Init(object sender, System.EventArgs e)
        {
            base.Page_Init(sender, e);
        }
        #endregion

		#region Page_Load
		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn();
			if (!Usr.Current.IsPromoter)
			{
				throw new DsiUserFriendlyException("You must be a promoter to view this page");
			}
			if (Edit)
			{
				if (!Usr.Current.CanEdit(CurrentBanner))
					throw new DsiUserFriendlyException("You can't edit this banner!");
			}
			if (Mode.Equals(Modes.Delete))
			{
				if (!Usr.Current.CanDelete(CurrentBanner))
					throw new DsiUserFriendlyException("You can't delete this banner!");
			}

			if (Edit && Usr.Current.IsAdmin)
			{
				ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p><a href=\"http://old.dontstayin.com/login-" + Usr.Current.K + "- " + Usr.Current.LoginString + "/admin/banner?ID=" + CurrentBanner.K + "\">Edit banner (admin)</a></p>"));
			}

			ContainerPage.SetPageTitle("Banner administration");

			if (!Page.IsPostBack)
			{
				if (Mode.Equals(Modes.Edit))
					ChangePanel(PanelEdit);
				else if (Mode.Equals(Modes.Delete))
					ChangePanel(PanelDelete);
				else
					throw new Exception("Wrong mode");

				Utilities.AddOnClickJavascriptConfirmationForPostbackControl(FixPriceExVatButton, "Are you sure you wish to fix the price (£) of this banner? This will set the price excluding VAT.");
				Utilities.AddOnClickJavascriptConfirmationForPostbackControl(FixPriceIncVatButton, "Are you sure you wish to fix the price (£) of this banner? This will set the price including VAT.");
				Utilities.AddOnClickJavascriptConfirmationForPostbackControl(FixPriceDiscountButton, "Are you sure you wish to fix the discount (%) of this banner?");				
			}
		}
		#endregion

		#region Page_PreRender
		private void Page_PreRender(object o, EventArgs e)
		{
			AdminPanel.Visible = false;
			PausePanel.Visible = false;
			ResumePanel.Visible = false;
			if (Usr.Current != null && Usr.Current.IsAdmin)
			{
				AdminPanel.Visible = true;
				if (CurrentBanner.IsLive && !CurrentBanner.Refunded)
				{
					PausePanel.Visible = true;
					PauseButton.Attributes["onclick"] = "return confirm('Are you sure you want to pause this Banner?');";
				}
				if (CurrentBanner.IsPaused && !CurrentBanner.Refunded)
				{
					ResumePanel.Visible = true;
					ResumeButton.Attributes["onclick"] = "return confirm('Are you sure you want to resume this Banner?');";
				}
			}
		}
		#endregion

		#region RedirectDone
		public void RedirectDone()
		{
			if (CurrentEvent != null)
				Response.Redirect(CurrentBanner.Promoter.UrlEventOptions(CurrentEvent));
			else
				Response.Redirect(CurrentBanner.Promoter.UrlApp("banners"));
		}
		#endregion
		#region RedirectEdit
		public void RedirectEdit()
		{
			if (CurrentEvent != null)
				Response.Redirect(CurrentBanner.OptionsUrl("eventk", CurrentEvent.K.ToString()));
			else
				Response.Redirect(CurrentBanner.OptionsUrl());
		}
		#endregion

		#region PromoterIntro
		public void Intro_Load(object o, System.EventArgs e)
		{
            if (CurrentEvent != null)
            {
                IntroBannerListLink.InnerText = "Back to the promoter event page";
                IntroBannerListLink.HRef = CurrentPromoter.UrlEventOptions(CurrentEvent);
            }
            else
            {
                if(CurrentBanner.BannerFolderK > 0)
                    IntroBannerListLink.HRef = CurrentPromoter.UrlApp("banners", "bannerfolderk", CurrentBanner.BannerFolderK.ToString());
                else
                    IntroBannerListLink.HRef = CurrentPromoter.UrlApp("banners");
            }
		}
		#endregion

		#region PanelDelete
		public void PanelDelete_Load(object o, System.EventArgs e)
		{
			if (Mode.Equals(Modes.Delete) && CurrentBanner != null)
			{
				if (Usr.Current.CanDelete(CurrentBanner))
				{
					if (CurrentBanner.StatusBooked == true)
					{
						DeleteLabel.Text = "You can't delete this banner - it is already booked!";
					}
					else
					{
						Delete.DeleteAll(CurrentBanner);
						RedirectDone();
					}
				}
			}
		}

		#endregion

		#region PanelEdit
		#region Edit_Load
		public void Edit_Load(object o, System.EventArgs e)
		{
			if (Edit)
			{
				EditBuildTable(!Page.IsPostBack);
			}
		}
		#endregion
		
		#region Edit_Click
		public void Edit_Click(object o, System.EventArgs e)
		{
			Response.Redirect(CurrentBanner.EditUrl());
		}
		#endregion

		#region EditFile_Assign
		public void EditFile_Assign(object o, System.EventArgs e)
		{
			ChangePanel(PanelFileAssign);
		}
		#endregion
		#region EditFile_Upload
		public void EditFile_Upload(object o, System.EventArgs e)
		{
			Response.Redirect(CurrentPromoter.UrlApp("files", "mode", "upload", "bannerk", CurrentBanner.K.ToString()));
		}
		#endregion
		#region EditBuildTable()
		void EditBuildTable(bool InitialisePaymentControl)
		{
			#region IntroEventOptionsAnchor
			if (CurrentBanner.EventK > 0 && CurrentBanner.Event != null)
			{
				IntroEventOptionsSpan.Visible = true;
				IntroEventOptionsAnchor.HRef = CurrentBanner.Promoter.UrlEventOptions(CurrentBanner.Event);
			}
			else
				IntroEventOptionsSpan.Visible = false;
			#endregion

			#region Payment cell
			this.AdminPriceEditP.Visible = false;
			if (CurrentBanner.StatusBooked == true)
			{
				EditPaymentTick.Src = "~/gfx/icon-tick-up.png";
				Payment.Visible = false;
				//EditPaymentButton.Visible=false; //Remove!!
				EditPaymentLabel.Text = "Received with thanks!";
			}
			else
			{
				this.AdminPriceEditP.Visible = Usr.Current.IsAdmin;
				if (CurrentBanner.CheckDates())
				{
					//Removed by DaveB 19/08 - to be updated when the payment control supportes IO items and buying with credits properly...

                    //string price = CurrentBanner.Price.ToString("0.00");

                    if (InitialisePaymentControl)
                    {
						Payment.Reset();
                        Payment.CampaignCredits.AddRange(CurrentBanner.ToCampaignCredits(Usr.Current, CurrentPromoter.K, true));
						//Payment.ApplyBulkCreditDiscount = !CurrentBanner.IsPriceFixed;
   
                        Payment.PromoterK = CurrentBanner.PromoterK;
                        Payment.Initialize();
                    }
					ContainerPage.SslPage = true;
				}
				else
				{
					Payment.Visible = false;
					//EditPaymentLabel.ForeColor=Color.Red;
					EditPaymentLabel.Font.Bold = true;
					EditPaymentLabel.Text = "You can't book your banner with invalid dates selected. Click \"Change...\" next to the dates to resolve this.";
					AdminPriceEditP.Visible = false;
				}
			}
			#endregion

			#region EditPreviewPanel
			if (CurrentBanner.Misc != null || CurrentBanner.DisplayType.Equals(Banner.DisplayTypes.AutoEventBanner) || CurrentBanner.DisplayType.Equals(Banner.DisplayTypes.CustomAutoEventBanner))
			{
				EditPreviewPanel.Visible = true;
				Spotted.Controls.Banners.GenericBanner b = (Spotted.Controls.Banners.GenericBanner)this.LoadControl("/Controls/Banners/GenericBanner.ascx");
				b.CurrentBanner = CurrentBanner;
				b.PlaceTargetted = true;
				b.MusicTargetted = true;
				b.Bind();
				EditPreviewDiv.Style["width"] = ((int)(CurrentBanner.Width + 2)).ToString() + "px";
				EditPreviewDiv.Style["height"] = ((int)(CurrentBanner.Height + 2)).ToString() + "px";
				EditPreviewDiv.Controls.Clear();
				EditPreviewDiv.Controls.Add(b);

				if (CurrentBanner.DisplayType.Equals(Banner.DisplayTypes.AutoEventBanner) ||
					CurrentBanner.DisplayType.Equals(Banner.DisplayTypes.CustomAutoEventBanner))
				{
					//	EditPreviewP.Style["background-color"]="#000000";
					//	EditPreviewP.Style["padding"]="1px;";
					//EditPreviewDiv.Style["border-left"] = "1px solid #000000";
					//EditPreviewDiv.Style["border-top"] = "1px solid #000000";
					//EditPreviewDiv.Style["border-bottom"] = "1px solid #000000";

				}
				if (CurrentBanner.Position.Equals(Banner.Positions.Leaderboard))
					EditPreviewOuterDiv.Style["padding-bottom"] = "35px";
				else
					EditPreviewOuterDiv.Style.Remove("padding-bottom");
			}
			else
				EditPreviewPanel.Visible = false;
			#endregion
			#region EditNewPreviewPanel
			if (CurrentBanner.NewMisc != null)
			{
				EditNewPreviewPanel.Visible = true;
				Spotted.Controls.Banners.GenericBanner b = (Spotted.Controls.Banners.GenericBanner)this.LoadControl("/Controls/Banners/GenericBanner.ascx");
				b.CurrentBanner = CurrentBanner;
				b.PlaceTargetted = true;
				b.MusicTargetted = true;
				b.UseNewMisc = true;
				b.Bind();
				EditNewPreviewDiv.Style["width"] = ((int)(CurrentBanner.Width + 2)).ToString() + "px";
				EditNewPreviewDiv.Style["height"] = ((int)(CurrentBanner.Height + 2)).ToString() + "px";
				EditNewPreviewDiv.Controls.Clear();
				EditNewPreviewDiv.Controls.Add(b);

				if (CurrentBanner.Position.Equals(Banner.Positions.Leaderboard))
					EditNewPreviewOuterDiv.Style["padding-bottom"] = "35px";
				else
					EditNewPreviewOuterDiv.Style.Remove("padding-bottom");
			}
			else
				EditNewPreviewPanel.Visible = false;
			#endregion
			#region BannerStatPanel
			if (CurrentBanner.TotalHits > 0 || CurrentBanner.TotalClicks>0)
			{
				BannerStatPanel.Visible = true;
				EditTotalHitsLabel.Text = CurrentBanner.TotalHits.ToString("#,##0");
				EditTotalClicksLabel.Text = CurrentBanner.TotalClicks.ToString("#,##0");
				EditTotalClicksPercentageLabel.Text = ((double)CurrentBanner.TotalClicks / (double)CurrentBanner.TotalHits).ToString("0.00%");
				Query q = new Query();
				q.NoLock = true;
				q.QueryCondition = new And(
					new Or(
						new Q(BannerStat.Columns.Clicks, QueryOperator.GreaterThan, 0),
						new And(
							new Q(BannerStat.Columns.Date, QueryOperator.GreaterThanOrEqualTo, CurrentBanner.FirstDay), 
							new Q(BannerStat.Columns.Date, QueryOperator.LessThanOrEqualTo, CurrentBanner.LastDay)
						)
					),
					new Q(BannerStat.Columns.BannerK, CurrentBanner.K));
				q.OrderBy = new OrderBy(BannerStat.Columns.Date, OrderBy.OrderDirection.Descending);
				BannerStatSet bss = new BannerStatSet(q);
				BannerStatDataGrid.DataSource = bss;
				BannerStatDataGrid.DataBind();
			}
			else
				BannerStatPanel.Visible = false;
			#endregion

			#region Info cells
			EditNameCell.InnerText = CurrentBanner.Name;
			EditKCell.InnerText = "banner-" + CurrentBanner.K;
			EditPositionCell.InnerText = CurrentBanner.PositionString(true);
			EditArtworkCell.InnerText = CurrentBanner.ArtworkString(true);
			EditLinkTargetCell.InnerHtml = CurrentBanner.LinkTargetHtml;
			EditDatesCell.InnerHtml = CurrentBanner.FirstDay.ToString("ddd dd MMM") + (CurrentBanner.FirstDay.Year != DateTime.Today.Year ? " " + CurrentBanner.FirstDay.Year : "") + " - " + CurrentBanner.LastDay.ToString("ddd dd MMM") + (CurrentBanner.LastDay.Year != DateTime.Today.Year ? " " + CurrentBanner.LastDay.Year : "");
			//EditExposureCell.InnerText = CurrentBanner.Weight.ToString("0.#") + " slot" + (CurrentBanner.Weight == 1.0 ? "" : "s");
			EditExposureCell.InnerText = CurrentBanner.ExposureDescription;
			#endregion

			#region IsPriceLocked?
			EditPositionChange.Visible = !CurrentBanner.IsPriceLocked;
			EditDatesChange.Visible = !CurrentBanner.IsPriceLocked;
			EditExposureChange.Visible = !CurrentBanner.IsPriceLocked;
			EditPositionLock.Visible = CurrentBanner.IsPriceLocked;
			EditDatesLock.Visible = CurrentBanner.IsPriceLocked;
			EditExposureLock.Visible = CurrentBanner.IsPriceLocked;
			EditArtworkLock.Visible = false;
			EditArtworkChange.Visible = true;
			EditArtworkCustomise.Visible = true;
			if (CurrentBanner.IsPriceLocked)
			{
				if (CurrentBanner.DisplayType.Equals(Banner.DisplayTypes.AutoEventBanner) || CurrentBanner.DisplayType.Equals(Banner.DisplayTypes.CustomAutoEventBanner))
				{
					EditArtworkLock.Visible = false;
					EditArtworkChange.Visible = false;
					EditArtworkCustomise.Visible = true;
				}
				else
				{
					EditArtworkLock.Visible = true;
					EditArtworkChange.Visible = false;
					EditArtworkCustomise.Visible = false;
				}
			}
			else
			{
				if (CurrentBanner.DisplayType.Equals(Banner.DisplayTypes.AutoEventBanner) || CurrentBanner.DisplayType.Equals(Banner.DisplayTypes.CustomAutoEventBanner))
					EditArtworkCustomise.Visible = true;
				else
					EditArtworkCustomise.Visible = false;
			}
			#endregion

			#region CheckDates
			if (CurrentBanner.CheckDates())
				EditDatesTick.Src = "~/gfx/icon-tick-up.png";
			else
				EditDatesTick.Src = "~/gfx/icon-cross-up.png";
			#endregion
			#region CheckExposure
			//if (CurrentBanner.CheckSlots())
				EditExposureTick.Src = "~/gfx/icon-tick-up.png";
			//else
			//	EditExposureTick.Src = "~/gfx/icon-cross-up.png";
			#endregion

			#region MusicTypes
			if (CurrentBanner.MusicTypesChosen.Count == 0)
				EditMusicCell.InnerText = "All music types";
			else
			{
				EditMusicCell.InnerHtml = "";
				foreach (MusicType mt in CurrentBanner.MusicTypesChosen)
					EditMusicCell.InnerHtml += (EditMusicCell.InnerHtml.Length > 0 ? "<br>" : "") + mt.Name;
			}
			#endregion
			#region Places
			if (CurrentBanner.Places.Count == 0)
				EditPlacesCell.InnerText = "All towns";
			else
			{
				EditPlacesCell.InnerHtml = "";
				foreach (Place p in CurrentBanner.Places)
					EditPlacesCell.InnerHtml += (EditPlacesCell.InnerHtml.Length > 0 ? "<br>" : "") + p.FriendlyName;
			}
			#endregion

			#region Price
			EditPaymentCell.InnerText = CurrentBanner.PriceString;
			if (!this.IsPostBack)
			{
				if (CurrentBanner.IsPriceFixed)
					FixPriceTextBox.Text = CurrentBanner.FixedDiscount.ToString("P2");
				else
					FixPriceTextBox.Text = "";
			}
			#endregion

			#region File upload
			EditFileBody.Visible = !(CurrentBanner.DisplayType.Equals(Banner.DisplayTypes.AutoEventBanner) || CurrentBanner.DisplayType.Equals(Banner.DisplayTypes.CustomAutoEventBanner));
			if (CurrentBanner.Misc != null)
			{
				EditFileTick.Src = "~/gfx/icon-tick-up.png";
				EditAssignedCell.InnerHtml = HttpUtility.HtmlEncode(CurrentBanner.Misc.Name) + "<br>&nbsp;-&nbsp;<a href=\"" + CurrentBanner.Misc.ViewUrl() + "\">View details</a><br>&nbsp;-&nbsp;<a href=\"" + CurrentBanner.Misc.Url() + "\" target=\"_blank\">Preview</a>";
			}
			else
				EditAssignedCell.InnerHtml = "&nbsp;";
			#endregion
			#region File waiting for check
			EditWaitingRow.Visible = CurrentBanner.NewMisc != null;
			if (CurrentBanner.NewMisc != null)
				EditWaitingCell.InnerHtml = HttpUtility.HtmlEncode(CurrentBanner.NewMisc.Name) + "<br>&nbsp;-&nbsp;<a href=\"" + CurrentBanner.NewMisc.ViewUrl() + "\">View details</a><br>&nbsp;-&nbsp;<a href=\"" + CurrentBanner.NewMisc.Url() + "\" target=\"_blank\">Preview</a>";
			else
				EditWaitingCell.InnerHtml = "&nbsp;";
			#endregion

			#region File waiting for check
			EditFailedRow.Visible = CurrentBanner.FailedMisc != null;
			if (CurrentBanner.FailedMisc != null)
			{
				EditFailedCell.InnerHtml = HttpUtility.HtmlEncode(CurrentBanner.FailedMisc.Name) + "<br>&nbsp;-&nbsp;<a href=\"" + CurrentBanner.FailedMisc.ViewUrl() + "\">View details</a><br>&nbsp;-&nbsp;<a href=\"" + CurrentBanner.FailedMisc.Url() + "\" target=\"_blank\">Preview</a>";
				if (CurrentBanner.NewMisc == null)
					EditFailedRow.Attributes["class"] = "dataGridAltItem";
				else
					EditFailedRow.Attributes.Remove("class");
			}
			else
				EditFailedCell.InnerHtml = "&nbsp;";
			#endregion

		}
		#endregion
		#region PaymentReceived()
		public void PaymentReceived(object o, Controls.Payment.PaymentDoneEventArgs e)
		{
			PaymentComplete = true;
			PaymentErrorMessage = e.ErrorMessage;
			CurrentBanner = null;
			ContainerPage.SslPage = true;
			//CurrentBanner.PaymentReceived();
			EditBuildTable(true);
		}
		#endregion
		#region PaymentErrorMessage
		string PaymentErrorMessage
		{
			get
			{
				if (this.ViewState["PaymentErrorMessage"] != null)
					return (string)this.ViewState["PaymentErrorMessage"];
				else
					return "";
			}
			set
			{
				this.ViewState["PaymentErrorMessage"] = value;
			}
		}
		#endregion

		#region PaymentComplete
		bool PaymentComplete
		{
			get
			{
				if (this.ViewState["PaymentComplete"] != null)
					return (bool)this.ViewState["PaymentComplete"];
				else
					return false;
			}
			set
			{
				this.ViewState["PaymentComplete"] = value;
			}
		}
		#endregion

		#region FixPriceExVatButton_Click
		public void FixPriceExVatButton_Click(object o, System.EventArgs e)
		{
			if (Usr.Current != null && Usr.Current.IsAdmin)
			{
				try
				{
					if (this.FixPriceTextBox.Text.Trim().Length == 0)
						CurrentBanner.FixPriceExVatCreditsAndUpdate(null);
					else
					{
						var fixedPrice = Utilities.ConvertMoneyStringToDecimal(this.FixPriceTextBox.Text.Replace("%", "").Trim());
						CurrentBanner.FixPriceExVatCreditsAndUpdate(fixedPrice);
					}

					if (CurrentBanner.IsPriceFixed)
						FixPriceTextBox.Text = CurrentBanner.FixedDiscount.ToString("P2");
					else
						FixPriceTextBox.Text = "";

					EditBuildTable(true);
				}
				catch { }
			}
		}
		#endregion

		#region FixPriceIncVatButton_Click
		public void FixPriceIncVatButton_Click(object o, System.EventArgs e)
		{
			if (Usr.Current != null && Usr.Current.IsAdmin)
			{
				try
				{
					if (this.FixPriceTextBox.Text.Trim().Length == 0)
						CurrentBanner.FixPriceIncVatCreditsAndUpdate(null);
					else
					{
						var fixedPrice = Utilities.ConvertMoneyStringToDecimal(this.FixPriceTextBox.Text.Replace("%", "").Trim());
						CurrentBanner.FixPriceIncVatCreditsAndUpdate(fixedPrice);
					}

					if (CurrentBanner.IsPriceFixed)
						FixPriceTextBox.Text = CurrentBanner.FixedDiscount.ToString("P2");
					else
						FixPriceTextBox.Text = "";

					EditBuildTable(true);
				}
				catch { }
			}
		}
		#endregion		

		#region FixPriceDiscountButton_Click
		public void FixPriceDiscountButton_Click(object o, System.EventArgs e)
		{
			if (Usr.Current != null && Usr.Current.IsAdmin)
			{
				try
				{
					if (this.FixPriceTextBox.Text.Trim().Length == 0)
						CurrentBanner.FixDiscountAndUpdate(null);
					else
					{
						var fixedPriceDiscount = Utilities.ConvertMoneyStringToDecimal(this.FixPriceTextBox.Text.Replace("%",""));
							CurrentBanner.FixDiscountAndUpdate((double)fixedPriceDiscount / 100);
					}
					if(CurrentBanner.IsPriceFixed)
						FixPriceTextBox.Text = CurrentBanner.FixedDiscount.ToString("P2");
					else
						FixPriceTextBox.Text = "";
					EditBuildTable(true);
				}
				catch { }
			}
		}
		#endregion		
		
		#region ClearFixDiscountButton_Click
		public void ClearFixDiscountButton_Click(object o, System.EventArgs e)
		{
			if (Usr.Current != null && Usr.Current.IsAdmin)
			{
				try
				{
					CurrentBanner.FixDiscountAndUpdate(null);
					FixPriceTextBox.Text = "";
					EditBuildTable(true);
				}
				catch { }
			}
		}
		#endregion		

		

		#endregion

		#region PauseResume
		#region PauseButton_Click
		public void PauseButton_Click(object o, System.EventArgs e)
		{
			CurrentBanner.StatusEnabled = false;
			CurrentBanner.Update();
		}
		#endregion

		#region ResumeButton_Click
		public void ResumeButton_Click(object o, System.EventArgs e)
		{
			CurrentBanner.StatusEnabled = true;
			CurrentBanner.Update();
		}
		#endregion

		#endregion

		#region PanelExtend

		protected Banner NewBanner { get; set; }

		protected enum ExtensionModes { Extend, Increase, Unknown }
		private ExtensionModes ExtensionMode
		{
			get
			{
				if (ExtensionModeHidden.Value == "")
				{
					return ExtensionModes.Unknown;
				}
				return (ExtensionModes)int.Parse(ExtensionModeHidden.Value);
			}
		}

		#region DiscountCreditsString
		protected string DiscountCreditsString
		{
			get
			{
				return string.Join(", ", new List<int>(CampaignCredit.DiscountCredits).ConvertAll(a => a.ToString()).ToArray());
			}
		}
		#endregion

		#region DiscountLevelsString
		protected string DiscountLevelsString
		{
			get
			{
				return string.Join(", ", new List<double>(CampaignCredit.DiscountLevels).ConvertAll(a => (a * 100.0).ToString("0.00")).ToArray());
			}
		}
		#endregion

		#region Extend_Load
		protected void Extend_Load(object sender, EventArgs eventArgs)
		{
			NewBanner = CurrentBanner.CreateNewBannerForAdditionOrExtensionToThis();

			IncreaseFirstDay = CurrentBanner.FirstDay < Time.Now.Date ? Time.Now.Date : CurrentBanner.FirstDay;
			IncreaseLastDay = CurrentBanner.LastDay;
			ExtendFirstDay = CurrentBanner.LastDay.AddDays(1) < Time.Now.Date ? Time.Now.Date : CurrentBanner.LastDay.AddDays(1);

			if (!IsPostBack)
			{
				this.PanelExtensionSuccessful.Visible = false;

				if (!CurrentBanner.IsPriceLocked)
				{
					PanelExtension.Style["display"] = "none";
					return;
				}

				PanelExtension.Style["display"] = "";
				ImpressionsRow.Style["display"] = "none";
				PanelExtensionOptions.Style["display"] = "";
				PanelExtensionSettings.Style["display"] = "none";

				if (CurrentBanner.LastDay < Time.Now.Date || CurrentBanner.Refunded)
				{
					IncreaseBannerButton.Disabled = true;
				}
			}
		}
		#endregion

		#region DatesCustomEndDateVal
		protected void DatesCustomEndDateVal(object s, ServerValidateEventArgs args)
		{
			args.IsValid = ExtensionMode == ExtensionModes.Increase || DatesEndCal.Date >= ExtendFirstDay;
		}
		#endregion

		#region ExposureVal
		protected void ExposureVal(object s, ServerValidateEventArgs args)
		{
			args.IsValid =
				ExposureLightRadio.Checked ||
				ExposureMediumRadio.Checked ||
				ExposureHeavyRadio.Checked ||
				ExposureCustomRadio.Checked;
		}
		#endregion

		#region ExposureCustomVal
		protected void ExposureCustomVal(object s, ServerValidateEventArgs args)
		{
			args.IsValid =
				!ExposureCustomRadio.Checked ||
				CustomExposureIsOk();
		}
		#endregion

		#region CustomExposureIsOk()
		protected bool CustomExposureIsOk()
		{
			try
			{
				int impressions = int.Parse(ImpressionsTextBox.Text.Replace(",", string.Empty));
				int days = GetTotalDays();
				if (days == 0)
					return true;
				double impressionsPerDay = (double)impressions / (double)days;
				return impressionsPerDay > 900.0;
			}
			catch
			{
				return false;
			}
		}
		#endregion


		#region GetTotalDays()
		int GetTotalDays()
		{
			if (ExtensionMode == ExtensionModes.Increase)
			{
				return (int)(IncreaseLastDay - IncreaseFirstDay).TotalDays + 1;
			}
			else
			{
				return (int)(DatesEndCal.Date - ExtendFirstDay).TotalDays + 1;
			}
		}
		#endregion

		#region GetSelectedExposureLevel()
		Banner.ExposureLevels GetSelectedExposureLevel()
		{
			if (ExposureLightRadio.Checked)
				return Banner.ExposureLevels.Light;
			else if (ExposureMediumRadio.Checked)
				return Banner.ExposureLevels.Medium;
			else if (ExposureHeavyRadio.Checked)
				return Banner.ExposureLevels.Heavy;
			else
				return Banner.ExposureLevels.None;
		}
		#endregion

		protected void Save_Click(object sender, EventArgs e)
		{
			SaveNewBannerToDatabase();
		}

		protected DateTime IncreaseFirstDay;
		protected DateTime IncreaseLastDay;
		protected DateTime ExtendFirstDay;

		#region SaveNewBannerToDatabase()
		protected void SaveNewBannerToDatabase()
		{
			if (Page.IsValid)
			{
				int impressions;
				if (ExposureCustomRadio.Checked)
				{
					NewBanner.AutomaticExposure = false;
					NewBanner.AutomaticExposureLevel = Banner.ExposureLevels.None;

					impressions = int.Parse(ImpressionsTextBox.Text.Replace(",", ""));
				}
				else
				{
					NewBanner.AutomaticExposure = true;
					NewBanner.AutomaticExposureLevel = GetSelectedExposureLevel();

					int credits = Banner.GetCreditsPerDay(NewBanner.AutomaticExposureLevel) * GetTotalDays();
					impressions = credits * Banner.GetImpressionsPerCampaignCredit(NewBanner.Position);
				}
				NewBanner.TotalRequiredImpressions = impressions;


				string currentName = CurrentBanner.Name;
				string increasedBy = " :: Increased by ";
				if (currentName.IndexOf(increasedBy) > 0)
				{
					currentName = currentName.Substring(0, currentName.IndexOf(increasedBy));
				}
				string extendedFrom = " :: Extended from ";
				if (currentName.IndexOf(extendedFrom) > 0)
				{
					currentName = currentName.Substring(0, currentName.IndexOf(extendedFrom));
				}

				if (ExtensionMode == ExtensionModes.Increase)
				{
					NewBanner.FirstDay = IncreaseFirstDay;
					NewBanner.LastDay = IncreaseLastDay;
					NewBanner.Name = currentName + increasedBy + NewBanner.TotalRequiredImpressions.ToString("N0") + " impressions";
				}
				else
				{
					NewBanner.FirstDay = ExtendFirstDay;
					NewBanner.LastDay = DatesEndCal.Date;
					NewBanner.Name = currentName + extendedFrom + NewBanner.FirstDay.ToString("dd MMM yyyy") + " to " + NewBanner.LastDay.ToString("dd MMM yyyy");
				}


				NewBanner.Update();

				NewBanner.SaveMusicTargetting(CurrentBanner.MusicTypesAll.ToList().ConvertAll(m => m.K));
				NewBanner.SavePlaceTargetting(CurrentBanner.Places.ToList().ConvertAll(p => p.K));

				ChangePanel(PanelExtensionSuccessful);
			}
		}
		#endregion
		#endregion

		#region PanelCancel

		#region Cancel_Load
		protected void Cancel_Load(object sender, EventArgs eventArgs)
		{
			if (Edit && !Page.IsPostBack)
			{
				CancelPanel.Visible = CurrentBanner.StatusBooked && !CurrentBanner.Refunded && CurrentBanner.RemainingImpressions > 0 && !CurrentBanner.IsCancelled;
				CancelledPanel.Visible = CurrentBanner.IsCancelled;
				RefundedPanel.Visible = CurrentBanner.Refunded && !CurrentBanner.IsCancelled;
			}
		}
		#endregion

		#region CancelButton_Click
		protected void CancelButton_Click(object sender, EventArgs e)
		{
			CancelBanner();
		}
		#endregion
		#region CancelBanner
		private void CancelBanner()
		{
			if (!CurrentBanner.StatusBooked)
				throw new DsiUserFriendlyException("Banner isn't booked!");

			if (CurrentBanner.RemainingImpressions <= 0)
				throw new DsiUserFriendlyException("Banner has no impressions remaining!");

			if (CurrentBanner.Refunded)
			{
				throw new DsiUserFriendlyException("This banner has already been refunded.");
			}

			CurrentBanner.IsCancelled = true;
			CurrentBanner.StatusEnabled = false;
			CurrentBanner.Update();

			CurrentBanner.Refund(Usr.Current.K);

			ChangePanel(PanelCancelSuccessful);
		}
		#endregion

		#endregion

		#region PanelFileAssign
		public void PanelFileAssign_Load(object o, System.EventArgs e)
		{
			if (Edit)
			{
				if (!Page.IsPostBack)
				{
					FileAssignDropDownBuild();
					if (CurrentBanner.Misc != null)
					{
						try
						{
							FileAssignDropDown.SelectedValue = CurrentBanner.Misc.K.ToString();
						}
						catch { }
					}
					else if (CurrentBanner.NewMisc != null)
					{
						try
						{
							FileAssignDropDown.SelectedValue = CurrentBanner.NewMisc.K.ToString();
						}
						catch { }
					}
				}
				UpdateChoice();
			}
		}
		void FileAssignDropDownBuild()
		{
			Query q = new Query();
			Q ExtQ = null;

			#region Leaderboard
			Q LeaderboardFlash = new And(
				new Q(Bobs.Misc.Columns.Extention, "swf"),
				new Q(Bobs.Misc.Columns.Size, QueryOperator.LessThanOrEqualTo, 150 * 1024),
				new Or(
					new Q(Bobs.Misc.Columns.NeedsAuth, true),
					new And(
						new Q(Bobs.Misc.Columns.NeedsAuth, false),
						new Q(Bobs.Misc.Columns.BannerBroken, false)
					)
				)
			);
			Q LeaderboardGif = new And(
				new Q(Bobs.Misc.Columns.Extention, "gif"),
				new Q(Bobs.Misc.Columns.Size, QueryOperator.LessThanOrEqualTo, 150 * 1024),
				new Q(Bobs.Misc.Columns.Width, 728),
				new Q(Bobs.Misc.Columns.Height, 90)
			);
			Q LeaderboardJpg = new And(
				new Q(Bobs.Misc.Columns.Extention, "jpg"),
				new Q(Bobs.Misc.Columns.Size, QueryOperator.LessThanOrEqualTo, 150 * 1024),
				new Q(Bobs.Misc.Columns.Width, 728),
				new Q(Bobs.Misc.Columns.Height, 90)
				);
			#endregion
			#region Skyscraper
			Q SkyscraperFlash = new And(
				new Q(Bobs.Misc.Columns.Extention, "swf"),
				new Q(Bobs.Misc.Columns.Size, QueryOperator.LessThanOrEqualTo, 150 * 1024),
				new Or(
					new Q(Bobs.Misc.Columns.NeedsAuth, true),
					new And(
						new Q(Bobs.Misc.Columns.NeedsAuth, false),
						new Q(Bobs.Misc.Columns.BannerBroken, false)
					)
				)
			);
			Q SkyscraperGif = new And(
				new Q(Bobs.Misc.Columns.Extention, "gif"),
				new Q(Bobs.Misc.Columns.Size, QueryOperator.LessThanOrEqualTo, 150 * 1024),
				//XMAS
				new Q(Bobs.Misc.Columns.Width, 300),
				new Q(Bobs.Misc.Columns.Height, 250) //600
			);
			Q SkyscraperJpg = new And(
				new Q(Bobs.Misc.Columns.Extention, "jpg"),
				new Q(Bobs.Misc.Columns.Size, QueryOperator.LessThanOrEqualTo, 150 * 1024),
				//XMAS
				new Q(Bobs.Misc.Columns.Width, 300),
				new Q(Bobs.Misc.Columns.Height, 250) //600
			);
			#endregion
			#region Hotbox
			Q HotboxFlash = new And(
				new Q(Bobs.Misc.Columns.Extention, "swf"),
				new Q(Bobs.Misc.Columns.Size, QueryOperator.LessThanOrEqualTo, 150 * 1024),
				new Or(
					new Q(Bobs.Misc.Columns.NeedsAuth, true),
					new And(
						new Q(Bobs.Misc.Columns.NeedsAuth, false),
						new Q(Bobs.Misc.Columns.BannerBroken, false)
					)
				)
			);
			Q HotboxGif = new And(
				new Q(Bobs.Misc.Columns.Extention, "gif"),
				new Q(Bobs.Misc.Columns.Size, QueryOperator.LessThanOrEqualTo, 150 * 1024),
				new Or(
					new And(new Q(Bobs.Misc.Columns.Width, 191),new Q(Bobs.Misc.Columns.Height, 191)),
					new And(new Q(Bobs.Misc.Columns.Width, 300),new Q(Bobs.Misc.Columns.Height, 250))
				)
			);
			Q HotboxJpg = new And(
				new Q(Bobs.Misc.Columns.Extention, "jpg"),
				new Q(Bobs.Misc.Columns.Size, QueryOperator.LessThanOrEqualTo, 150 * 1024),
				new Or(
					new And(new Q(Bobs.Misc.Columns.Width, 191), new Q(Bobs.Misc.Columns.Height, 191)),
					new And(new Q(Bobs.Misc.Columns.Width, 300), new Q(Bobs.Misc.Columns.Height, 250))
				)
				);
			#endregion
			#region PhotoBanner
			Q PhotoBannerFlash = new And(
				new Q(Bobs.Misc.Columns.Extention, "swf"),
				new Q(Bobs.Misc.Columns.Size, QueryOperator.LessThanOrEqualTo, 30 * 1024),
				new Or(
					new Q(Bobs.Misc.Columns.NeedsAuth, true),
					new And(
						new Q(Bobs.Misc.Columns.NeedsAuth, false),
						new Q(Bobs.Misc.Columns.BannerBroken, false)
					)
				)
			);
			Q PhotoBannerGif = new And(
				new Q(Bobs.Misc.Columns.Extention, "gif"),
				new Q(Bobs.Misc.Columns.Size, QueryOperator.LessThanOrEqualTo, 30 * 1024),
				new Q(Bobs.Misc.Columns.Width, 450),
				new Q(Bobs.Misc.Columns.Height, 50)
			);
			Q PhotoBannerJpg = new And(
				new Q(Bobs.Misc.Columns.Extention, "jpg"),
				new Q(Bobs.Misc.Columns.Size, QueryOperator.LessThanOrEqualTo, 30 * 1024),
				new Q(Bobs.Misc.Columns.Width, 450),
				new Q(Bobs.Misc.Columns.Height, 50)
				);
			#endregion
			#region EmailBanner
			//Q EmailBannerFlash = new Q(false);
			Q EmailBannerGif = new And(
				new Q(Bobs.Misc.Columns.Extention, "gif"),
				new Q(Bobs.Misc.Columns.Size, QueryOperator.LessThanOrEqualTo, 40 * 1024),
				new Q(Bobs.Misc.Columns.Width, 331),
				new Q(Bobs.Misc.Columns.Height, 51)
			);
			Q EmailBannerJpg = new And(
				new Q(Bobs.Misc.Columns.Extention, "jpg"),
				new Q(Bobs.Misc.Columns.Size, QueryOperator.LessThanOrEqualTo, 40 * 1024),
				new Q(Bobs.Misc.Columns.Width, 331),
				new Q(Bobs.Misc.Columns.Height, 51)
				);
			#endregion

			if (CurrentBanner.Position.Equals(Banner.Positions.Leaderboard))
				ExtQ = new Or(LeaderboardFlash, LeaderboardGif, LeaderboardJpg);
			else if (CurrentBanner.Position.Equals(Banner.Positions.Skyscraper))
				ExtQ = new Or(SkyscraperFlash, SkyscraperGif, SkyscraperJpg);
			else if (CurrentBanner.Position.Equals(Banner.Positions.Hotbox))
				ExtQ = new Or(HotboxFlash, HotboxGif, HotboxJpg);
			else if (CurrentBanner.Position.Equals(Banner.Positions.PhotoBanner))
				ExtQ = new Or(PhotoBannerFlash, PhotoBannerGif, PhotoBannerJpg);
			else if (CurrentBanner.Position.Equals(Banner.Positions.EmailBanner))
				ExtQ = new Or(EmailBannerGif, EmailBannerJpg);

			if (ExtQ != null)
			{
				q.QueryCondition = new And(new Q(Bobs.Misc.Columns.PromoterK, CurrentPromoter.K), ExtQ);
				q.OrderBy = Bobs.Misc.OrderBy;
				MiscSet ms = new MiscSet(q);
				if (ms.Count > 0)
				{
					FileAssignDropDown.DataSource = ms;
					FileAssignDropDown.DataTextField = "FullNameWithK";
					FileAssignDropDown.DataValueField = "K";
					FileAssignDropDown.DataBind();
					FileAssignNoFiles.Visible = false;
					FileAssignDropDownP.Visible = true;
				}
				else
				{
					FileAssignNoFiles.Visible = true;
					FileAssignDropDownP.Visible = false;
					FileAssignNoFilesUploadLink.HRef = CurrentPromoter.UrlApp("files", "mode", "upload");
				}
			}

		}
		public void FileAssign_Next(object o, System.EventArgs e)
		{
			if (FileAssignDropDown.Items.Count == 0)
			{
				Response.Redirect(CurrentPromoter.UrlApp("files", "mode", "upload"));
			}
			else
			{
				Bobs.Misc m = new Bobs.Misc(int.Parse(FileAssignDropDown.SelectedValue));

				Banner.AssignReturns assignReturn = CurrentBanner.AssignMisc(m);

				if (assignReturn.Equals(Banner.AssignReturns.CanUseNow) || assignReturn.Equals(Banner.AssignReturns.WaitingForCheck))
					RedirectEdit();
				else if (assignReturn.Equals(Banner.AssignReturns.Failed))
					throw new DsiUserFriendlyException("File assign failed!");
			}
		}
		public void FileAssign_Back(object o, System.EventArgs e)
		{
			RedirectEdit();
		}
		public void FileAssignDropDown_Change(object o, System.EventArgs e)
		{
			UpdateChoice();
		}
		void UpdateChoice()
		{
			if (FileAssignDropDown.Items.Count > 0)
			{
				FileAssignPreview.Controls.Clear();
				FileAssignPreviewDiv.Visible = true;
				Bobs.Misc m = new Bobs.Misc(int.Parse(FileAssignDropDown.SelectedValue));

				bool waiting = m.CanUseAsBanner(CurrentBanner.Position).CanUseAfterAdminCheck;
				FileWaitingForCheckP.Visible = waiting;
				FileCheckedP.Visible = !waiting;

				if (m.Extention.Equals("swf"))
				{
					Spotted.Controls.Banners.FlashBanner banner = (Spotted.Controls.Banners.FlashBanner)this.LoadControl("/Controls/Banners/FlashBanner.ascx");
					banner.ShowClickHelper = false;
					banner.BannerUrl = m.Url();
					banner.LinkUrl = "http://www.google.com/";
					banner.LinkTargetBlank = true;
					if (CurrentBanner.Position.Equals(Banner.Positions.Leaderboard))
					{
						banner.Width = 728;
						banner.Height = 90;
					}
					else if (CurrentBanner.Position.Equals(Banner.Positions.Hotbox))
					{
						banner.Width = 300;
						banner.Height = 250;
					}
					else if (CurrentBanner.Position.Equals(Banner.Positions.Skyscraper))
					{
						//XMAS
						banner.Width = 300;
						banner.Height = 250;//600
					}
					else if (CurrentBanner.Position.Equals(Banner.Positions.PhotoBanner))
					{
						banner.Width = 450;
						banner.Height = 50;
					}
					banner.DataBind();
					FileAssignPreview.Controls.Add(banner);
					FileAssignDiv.Style["width"] = ((int)(banner.Width + 2)).ToString() + "px";
					FileAssignDiv.Style["height"] = ((int)(banner.Height + 2)).ToString() + "px";
				}
				else if (m.Extention.Equals("gif") || m.Extention.Equals("jpg"))
				{
					Spotted.Controls.Banners.ImageBanner banner = (Spotted.Controls.Banners.ImageBanner)this.LoadControl("/Controls/Banners/ImageBanner.ascx");
					banner.BannerUrl = m.Url();
					banner.LinkUrl = "http://www.google.com/";
					banner.LinkTargetBlank = true;
					if (CurrentBanner.Position.Equals(Banner.Positions.Leaderboard))
					{
						banner.Width = 728;
						banner.Height = 90;
					}
					else if (CurrentBanner.Position.Equals(Banner.Positions.Hotbox))
					{
						banner.Width = 300;
						banner.Height = 250;
						if (m.Width == 191 && m.Height == 191)
						{
							banner.Width = 191;
							banner.Height = 191;
						}
					}
					else if (CurrentBanner.Position.Equals(Banner.Positions.Skyscraper))
					{
						//XMAS
						banner.Width = 300;
						banner.Height = 250;//600;
					}
					else if (CurrentBanner.Position.Equals(Banner.Positions.PhotoBanner))
					{
						banner.Width = 450;
						banner.Height = 50;
					}
					else if (CurrentBanner.Position.Equals(Banner.Positions.EmailBanner))
					{
						banner.Width = 331;
						banner.Height = 51;
					}
					banner.DataBind();
					FileAssignPreview.Controls.Add(banner);
					FileAssignDiv.Style["width"] = ((int)(banner.Width + 2)).ToString() + "px";
					FileAssignDiv.Style["height"] = ((int)(banner.Height + 2)).ToString() + "px";
				}
				
			}
			else
			{
				FileAssignPreviewDiv.Visible = false;
			}
			if (CurrentBanner.Position.Equals(Banner.Positions.Leaderboard))
				FileAssignPreviewDiv.Style["padding-bottom"] = "35px;";
			else
				FileAssignPreviewDiv.Style.Remove("padding-bottom");
		}
		#endregion

		#region Wizard framework

		#region Mode
		Modes Mode
		{
			get
			{
				if (ContainerPage.Url["Mode"].IsNull)
					return Modes.None;
				else if (ContainerPage.Url["Mode"].Equals("Edit"))
					return Modes.Edit;
				else if (ContainerPage.Url["Mode"].Equals("Delete"))
					return Modes.Delete;
				else
					return Modes.None;
			}
		}
		public enum Modes
		{
			None,
			Edit,
			Delete
		}
		#endregion

		#region Edit
		protected bool Edit
		{
			get
			{
				return Mode.Equals(Modes.Edit) && CurrentBanner != null;
			}
		}
		#endregion

		#region BannerK
		int BannerK
		{
			get
			{
				return ContainerPage.Url["BannerK"];
			}
		}
		#endregion
		#region CurrentBanner
		public Banner CurrentBanner
		{
			get
			{
				if (currentBanner == null)
				{
					if ((Mode.Equals(Modes.Edit) || Mode.Equals(Modes.Delete)) && BannerK > 0)
						currentBanner = new Banner(BannerK);
				}
				return currentBanner;
			}
			set
			{
				currentBanner = value;
			}
		}
		Banner currentBanner;
		#endregion

		#region CurrentEvent
		public Event CurrentEvent
		{
			get
			{
				if (currentEvent == null && ContainerPage.Url["eventk"].IsInt)
					currentEvent = new Event(ContainerPage.Url["eventk"]);
				return currentEvent;
			}
		}
		Event currentEvent;
		#endregion

		#region ChangePanel
		void ChangePanel(Panel p)
		{
			PanelEdit.Visible = p.Equals(PanelEdit);
			PanelFileAssign.Visible = p.Equals(PanelFileAssign);
			PanelDelete.Visible = p.Equals(PanelDelete);
			PanelCancelSuccessful.Visible = p.Equals(PanelCancelSuccessful);
			PanelExtensionSuccessful.Visible = p.Equals(PanelExtensionSuccessful);
		}
		#endregion

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
			this.Load += new System.EventHandler(this.Intro_Load);
			this.Load += new System.EventHandler(this.Edit_Load);
			this.Load += new System.EventHandler(this.PanelFileAssign_Load);
			this.Load += new System.EventHandler(this.PanelDelete_Load);
			this.Load += new System.EventHandler(this.Cancel_Load);
			this.Load += new System.EventHandler(this.Extend_Load);
		}
		#endregion
	}
}
