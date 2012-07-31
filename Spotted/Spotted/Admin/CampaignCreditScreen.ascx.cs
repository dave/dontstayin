using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Bobs;
using Local;
using Spotted.Controls;
using Spotted;
using Bobs.DataHolders;
using Common;

namespace Spotted.Admin
{
    public partial class CampaignCreditScreen : AdminUserControl
    {
		public CampaignCreditScreen()
		{
			this.Init += new EventHandler(CampaignCreditScreen_Init);
			
		}

		void CampaignCreditScreen_Init(object sender, EventArgs e)
		{
			this.uiPromotersAutoComplete.ValueChanged += new EventHandler(uiPromotersAutoComplete_ValueChanged);
		}
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
				ActionDateTimeLabel.Text = Time.Now.ToString("dd/MM/yyyy HH:mm");
                SetupBuyableObjectDropDownList();
				SetupInvoiceItemTypesDropDownList();
				SetupNotesAddOnly();

				if (Usr.Current != null)
				{
					this.uiActionUserAutoComplete.Value = Usr.Current.K.ToString();
					this.uiActionUserAutoComplete.Text = Usr.Current.Name;
					this.ActionUsrValueLabel.Text = Usr.Current.Link();
					this.ActionUsrValueLabel.Visible = false;
				}

                if(CurrentCampaignCredit.K > 0 && CurrentCampaignCredit.Enabled)
                    LoadScreenFromCampaignCredit();
            }
            ShowHideControls();
        }

        #region Properties
        public CampaignCredit CurrentCampaignCredit
        {
            get
            {
                if (ContainerPage.Url["K"].IsInt)
                {
                    try
                    {
                        currentCampaignCredit = new CampaignCredit(Convert.ToInt32(ContainerPage.Url["K"].Value));
                    }
                    catch { }
                }
                return currentCampaignCredit;
            }
            set
            {
                currentCampaignCredit = value;
            }
        }
        private CampaignCredit currentCampaignCredit = new CampaignCredit();

        protected Promoter CurrentPromoter
        {
            get
            {
				if (currentPromoter == null && this.uiPromotersAutoComplete.Value.Length > 0)
					currentPromoter = new Promoter(Convert.ToInt32(this.uiPromotersAutoComplete.Value));
                return currentPromoter;
            }
            set
            {
                currentPromoter = value;
				this.uiPromotersAutoComplete.Value = currentPromoter.K.ToString();
            }
        }
        private Promoter currentPromoter;
        #endregion

        #region Methods
        #region Setup DropDownLists
        private void SetupBuyableObjectDropDownList()
        {
            this.BuyableObjectTypeDropDownList.Items.Clear();
            this.BuyableObjectTypeDropDownList.Items.AddRange(Vars.BuyableCreditObjectsToListItemArray());
        }

		#region Setup UsrDropDownList
		private void SetupUsrDropDownList()
		{
			this.UsrDropDownList.Items.Clear();
			if (CurrentPromoter != null)
			{
				foreach (Usr promoterUser in CurrentPromoter.AdminUsrs)
				{
					this.UsrDropDownList.Items.Add(new ListItem(promoterUser.Name, promoterUser.K.ToString()));
				}

				if (CurrentPromoter.PrimaryUsrK != 0)
					this.UsrDropDownList.SelectedValue = CurrentPromoter.PrimaryUsrK.ToString();
			}
		}
		#endregion

		#region Setup InvoiceItemTypes
		private void SetupInvoiceItemTypesDropDownList()
		{
			this.InvoiceItemTypeDropDownList.Items.Clear();

			if (Convert.ToInt32(this.BuyableObjectTypeDropDownList.SelectedValue) == Convert.ToInt32(Model.Entities.ObjectType.Banner))
			{
				this.InvoiceItemTypeDropDownList.Items.Add(new ListItem(Utilities.CamelCaseToString(InvoiceItem.Types.BannerEmail.ToString()), Convert.ToInt32(InvoiceItem.Types.BannerEmail).ToString()));
				this.InvoiceItemTypeDropDownList.Items.Add(new ListItem(Utilities.CamelCaseToString(InvoiceItem.Types.BannerHotbox.ToString()), Convert.ToInt32(InvoiceItem.Types.BannerHotbox).ToString()));
				this.InvoiceItemTypeDropDownList.Items.Add(new ListItem(Utilities.CamelCaseToString(InvoiceItem.Types.BannerPhoto.ToString()), Convert.ToInt32(InvoiceItem.Types.BannerPhoto).ToString()));
				this.InvoiceItemTypeDropDownList.Items.Add(new ListItem(Utilities.CamelCaseToString(InvoiceItem.Types.BannerSkyscraper.ToString()), Convert.ToInt32(InvoiceItem.Types.BannerSkyscraper).ToString()));
				this.InvoiceItemTypeDropDownList.Items.Add(new ListItem(Utilities.CamelCaseToString(InvoiceItem.Types.BannerTop.ToString()), Convert.ToInt32(InvoiceItem.Types.BannerTop).ToString()));
			}
			else if (Convert.ToInt32(this.BuyableObjectTypeDropDownList.SelectedValue) == Convert.ToInt32(Model.Entities.ObjectType.Invoice))
			{
				this.InvoiceItemTypeDropDownList.Items.Add(new ListItem(Utilities.CamelCaseToString(InvoiceItem.Types.CampaignCredits.ToString()), Convert.ToInt32(InvoiceItem.Types.CampaignCredits).ToString()));
			}
			else if (Convert.ToInt32(this.BuyableObjectTypeDropDownList.SelectedValue) == Convert.ToInt32(Model.Entities.ObjectType.Event))
			{
				this.InvoiceItemTypeDropDownList.Items.Add(new ListItem(Utilities.CamelCaseToString(InvoiceItem.Types.EventDonate.ToString()), Convert.ToInt32(InvoiceItem.Types.EventDonate).ToString()));
			}
		}
		#endregion
        #endregion

		#region Setup NotesAddOnly
		private void SetupNotesAddOnly()
		{
			NotesAddOnlyTextBox.ReadOnlyTextBox.CssClass = "readOnlyNotesTextBox";
			NotesAddOnlyTextBox.AddTextBox.CssClass = "addNotesTextBox";
			NotesAddOnlyTextBox.TimeStampFormat = "dd/MM/yy HH:mm";
			NotesAddOnlyTextBox.AuthorName = Usr.Current.NickName;
			NotesAddOnlyTextBox.InsertOption = AddOnlyTextBox.InsertOptions.AddAtBeginning;
		}
		#endregion



		private void LoadScreenFromCampaignCredit()
        {
            if (CurrentCampaignCredit.Promoter != null)
            {
				this.uiPromotersAutoComplete.Value = CurrentCampaignCredit.PromoterK.ToString();
				this.uiPromotersAutoComplete.Text = CurrentCampaignCredit.Promoter.Name;
                this.PromoterValueLabel.Text = CurrentCampaignCredit.Promoter.LinkNewWindow();
				this.PromoterCampaignCreditsLabel.Text = "<br />" + CurrentCampaignCredit.Promoter.CampaignCredits.ToString("N0") + " credits";
				this.SetupUsrDropDownList();
            }
			try
			{
				if (CurrentCampaignCredit.Usr != null)
				{
					this.UsrDropDownList.SelectedValue = CurrentCampaignCredit.UsrK.ToString();
					this.UsrValueLabel.Text = CurrentCampaignCredit.Usr.LinkNewWindow();
				}
			}
			catch{}

			if (CurrentCampaignCredit.ActionUsr != null)
			{
				this.uiActionUserAutoComplete.Value = CurrentCampaignCredit.ActionUsrK.ToString();
				this.uiActionUserAutoComplete.Text = CurrentCampaignCredit.ActionUsr.NickName;
				this.ActionUsrValueLabel.Text = CurrentCampaignCredit.ActionUsr.LinkNewWindow();
			}
            this.CampaignCreditKLabel.Text = CurrentCampaignCredit.K.ToString();
            this.CampaignCreditKLabel.Visible = CurrentCampaignCredit.K > 0;
            this.BuyableObjectTypeDropDownList.SelectedValue = Convert.ToInt32(CurrentCampaignCredit.BuyableObjectType).ToString();

            if (CurrentCampaignCredit.BuyableObjectK > 0)
            {
				string buyableObjectString = Utilities.CamelCaseToString(CurrentCampaignCredit.BuyableObjectType.ToString()) + " #" + CurrentCampaignCredit.BuyableObjectK.ToString();
				this.BuyableObjectTypeValueLabel.Text = CurrentCampaignCredit.BuyableObject != null && CurrentCampaignCredit.BuyableObject is ILinkable ? Utilities.Link(((ILinkable)CurrentCampaignCredit.BuyableObject).Url(), buyableObjectString) : buyableObjectString;
            }
            else
            {
                this.BuyableObjectTypeValueLabel.Text = Utilities.CamelCaseToString(CurrentCampaignCredit.BuyableObjectType.ToString());
            }
            this.BuyableObjectKTextBox.Text = CurrentCampaignCredit.BuyableObjectK.ToString();
            this.InvoiceItemTypeDropDownList.SelectedValue = Convert.ToInt32(CurrentCampaignCredit.InvoiceItemType).ToString();
            this.InvoiceItemTypeValueLabel.Text = Utilities.CamelCaseToString(CurrentCampaignCredit.InvoiceItemType.ToString());
            this.CreditsTextBox.Text = CurrentCampaignCredit.Credits.ToString();
            this.DescriptionTextBox.Text = CurrentCampaignCredit.Description;
			this.NotesAddOnlyTextBox.ReadOnlyTextBox.Text = CurrentCampaignCredit.Notes;
			this.ActionDateTimeLabel.Text = CurrentCampaignCredit.ActionDateTime.ToString("dd/MM/yyyy HH:mm");
        }

        private void LoadCampaignCreditFromScreen()
        {
			CurrentCampaignCredit.PromoterK = Convert.ToInt32(this.uiPromotersAutoComplete.Value);
            CurrentCampaignCredit.BuyableObjectType = (Model.Entities.ObjectType)Convert.ToInt32(this.BuyableObjectTypeDropDownList.SelectedValue);
			if (this.BuyableObjectKTextBox.Text.Trim().Length > 0)
				CurrentCampaignCredit.BuyableObjectK = Convert.ToInt32(this.BuyableObjectKTextBox.Text.Trim());
			else
				CurrentCampaignCredit.BuyableObjectK = 0;
            CurrentCampaignCredit.InvoiceItemType = (InvoiceItem.Types)Convert.ToInt32(this.InvoiceItemTypeDropDownList.SelectedValue);
            CurrentCampaignCredit.Credits = Convert.ToInt32(this.CreditsTextBox.Text.Replace(",", ""));
            CurrentCampaignCredit.Description = this.DescriptionTextBox.Text;
			CurrentCampaignCredit.Notes = this.NotesAddOnlyTextBox.ReadOnlyTextBox.Text;
			CurrentCampaignCredit.UsrK = Convert.ToInt32(this.UsrDropDownList.SelectedValue);
			CurrentCampaignCredit.ActionUsrK = Convert.ToInt32(this.uiActionUserAutoComplete.Value);
			if(CurrentCampaignCredit.K == 0)
				CurrentCampaignCredit.ActionDateTime = Time.Now;
        }

        private void ShowHideControls()
        {
            bool isSaved = CurrentCampaignCredit.K > 0;
			this.uiPromotersAutoComplete.Visible = !isSaved;
            this.PromoterValueLabel.Visible = isSaved;
			this.UsrDropDownList.Visible = !isSaved;
			this.UsrValueLabel.Visible = isSaved;
			this.uiActionUserAutoComplete.Visible = !isSaved;
			this.ActionUsrValueLabel.Visible = isSaved;
            this.BuyableObjectTypeDropDownList.Visible = !isSaved;
            this.BuyableObjectTypeValueLabel.Visible = isSaved;
            this.InvoiceItemTypeDropDownList.Visible = !isSaved;
            this.InvoiceItemTypeValueLabel.Visible = isSaved;
            this.BuyableObjectKRow.Visible = !isSaved;

            Utilities.EnableDisableControls(this.BuyableObjectKTextBox, !isSaved);
            Utilities.EnableDisableControls(this.CreditsTextBox, !isSaved);            
        }
        #endregion

        #region Page Event Handlers
        protected void uiPromotersAutoComplete_ValueChanged(object sender, EventArgs e)
        {
            if (CurrentPromoter != null)
            {
                this.PromoterValueLabel.Text = CurrentPromoter.Name;
				this.PromoterCampaignCreditsLabel.Text = "<br />" + CurrentPromoter.CampaignCredits.ToString("N0") + " credits";
				SetupUsrDropDownList();
            }
        }

		protected void BuyableObjectTypeDropDownList_SelectedItemChanged(object sender, EventArgs e)
		{
			SetupInvoiceItemTypesDropDownList();
		}

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            Page.Validate("");
            if (Page.IsValid)
            {
                LoadCampaignCreditFromScreen();
				if (CurrentCampaignCredit.K == 0)
				{
					CurrentCampaignCredit.Enabled = true;
					CurrentCampaignCredit.UpdateWithRecalculateBalance();
				}
				else
					CurrentCampaignCredit.Update();

                string response = "<script type=\"text/javascript\">alert('Campaign Credit #" + CurrentCampaignCredit.K.ToString() + " saved successfully'); open('" + CurrentCampaignCredit.UrlAdmin() + "?" + Cambro.Misc.Utility.GenRandomText(5) + "', '_self');</script>";
                ViewState["DuplicateGuid"] = Guid.NewGuid();
                Response.Write(response);
                Response.End();
            }
        }

		protected void CancelButton_Click(object sender, EventArgs e)
		{
			if (CurrentCampaignCredit.K > 0)
			{
				Response.Redirect(CurrentCampaignCredit.UrlAdmin());
			}
			else
			{
				Response.Redirect(CampaignCredit.UrlAdminNewCampaignCredit());
			}
		}
        #endregion

		#region Custom Validators
		#region BuyableObjectVal
		public void BuyableObjectVal(object o, ServerValidateEventArgs e)
		{
			try
			{
				int k = Convert.ToInt32(e.Value);
				if (k > 0)
				{
					var bob = Bob.Get((Model.Entities.ObjectType)Convert.ToInt32(this.BuyableObjectTypeDropDownList.SelectedValue), k);
					e.IsValid = bob != null;
				}
				else
					e.IsValid = false;
			}
			catch
			{
				e.IsValid = false;
			}
		}
		#endregion
		#endregion
	}
}
