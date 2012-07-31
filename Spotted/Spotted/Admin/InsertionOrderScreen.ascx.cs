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

namespace Spotted.Admin
{
    public partial class InsertionOrderScreen : AdminUserControl
    {
		//note this isn't a real MVC pattern, just my own creation. ist a bit of a mess at the mo and i'll fix it!
		#region Model
		public InsertionOrder InsertionOrder { get; set; }
		public List<Usr> SalesUsrs { get; set; }
		public List<InsertionOrderItem> InsertionOrderItems { get; set; }
		#endregion
		#region View

		protected override void OnInit(EventArgs e)
		{
			Page.PreLoad += new EventHandler(Page_PreLoad);
			Page.PreRender += new EventHandler(Page_PreRender);
			this.uiPromoterAutoComplete.ValueChanged +=new EventHandler(uiPromoterAutoComplete_ValueChanged);
			base.OnInit(e);
		}
		void Page_PreRender(object sender, EventArgs e)
		{
			if (IsPostBack) { this.DataBind(); }
		}

		void Page_PreLoad(object sender, EventArgs e)
		{
			SetUpModel();
			if (InsertionOrder.Status != InsertionOrder.InsertionOrderStatus.Proforma)
			{
				Response.Redirect(InsertionOrder.UrlAdminInsertionOrderReport());
			}
		}

		void uiPromoterAutoComplete_ValueChanged(object sender, EventArgs e)
		{
			InsertionOrder.PromoterK = int.Parse(uiPromoterAutoComplete.Value);
			InsertionOrder.AgencyDiscount = this.InsertionOrder.Promoter.IsAgency ? 0.3d : 0.0d;
			InsertionOrder.UsrK = 0;
		}



		protected override void OnDataBinding(EventArgs e)
		{

			foreach (InsertionOrderItem item in InsertionOrderItems)
			{
				item.AgencyDiscount = InsertionOrder.AgencyDiscount;
			}


			#region uiUserDropDownList
			if (InsertionOrder.PromoterK > 0){
				this.uiUserDropDownList.Items.Clear();
				this.uiUserDropDownList.Items.Add(new ListItem("<enter manually>", "-1"));
				string name = InsertionOrder.Promoter.AccountsName.Length > 0 ? InsertionOrder.Promoter.AccountsName + " (accounts contact)" : InsertionOrder.Promoter.ContactName + " (default contact)";
				if (name.Length > 0)
				{
					this.uiUserDropDownList.Items.Add(new ListItem(name, "0"));
				}
				List<Usr> usrs = new List<Usr>(InsertionOrder.Promoter.AdminUsrs);
				this.uiUserDropDownList.Items.AddRange(usrs.ConvertAll(u => new ListItem(u.NickName + "(" + u.Name + ")", u.K.ToString())).ToArray());
				this.uiUserDropDownList.SelectedValue = InsertionOrder.UsrK.ToString();
			}

			#endregion
			

			this.uiInsertionOrderItemGridView.DataSource = ((this.InsertionOrderItems.Count == 0) ? new List<InsertionOrderItem>(){null} : this.InsertionOrderItems);
			this.uiInsertionOrderItemGridView.DataBind();



			this.uiCampaignCredits.Text = CalculateCampaignCredits().ToString();

			
			uiCampaignCreditsOverride.Text = (!this.InsertionOrder.CampaignCreditsOverriden) ? uiCampaignCredits.Text : uiCampaignCreditsOverride.Text = this.InsertionOrder.CampaignCredits.ToString();
			
			#region uiNotesAddOnlyTextBox
			this.uiNotesAddOnlyTextBox.ReadOnlyTextBox.Text = InsertionOrder.Notes;
			if (!this.IsPostBack)
			{
				uiNotesAddOnlyTextBox.ReadOnlyTextBox.CssClass = "readOnlyNotesTextBox";
				uiNotesAddOnlyTextBox.AddTextBox.CssClass = "addNotesTextBox";
				uiNotesAddOnlyTextBox.TimeStampFormat = "dd/MM/yy HH:mm";
				uiNotesAddOnlyTextBox.AuthorName = Usr.Current.NickName;
				uiNotesAddOnlyTextBox.InsertOption = AddOnlyTextBox.InsertOptions.AddAtBeginning;
			}
			#endregion

			this.uiAgencyDiscount.Text = (this.InsertionOrder.AgencyDiscount * 100d).ToString();

			foreach (DataControlField dcf in this.uiInsertionOrderItemGridView.Columns)
			{
				if (dcf.HeaderText == "Discounted cost" || dcf.HeaderText == "Agency discount") { 
					dcf.Visible = (this.InsertionOrder.PromoterK != 0 && this.InsertionOrder.Promoter.IsAgency); 
				}
			}
			base.OnDataBinding(e);
		}



		double CalculateCampaignCredits()
		{
			double calculatedCampaignCredits = 0.0;
			InsertionOrderItems.ForEach(i => calculatedCampaignCredits += i.GetCampaignCredits());
			return calculatedCampaignCredits;
		}
		

		protected void uiInsertionOrderItemGridView_RowCreated(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.Footer)
			{
				
				DropDownList uiBannerPosition = (DropDownList) e.Row.FindControl("uiBannerPosition");
				uiBannerPosition.Items.Add(new ListItem("", "0"));
				
				foreach (Banner.Positions position in Enum.GetValues(typeof(Banner.Positions))){
					uiBannerPosition.Items.Add(new ListItem(position.ToString(), ((int)position).ToString()));
				}
				
			}
		}
		#endregion
		#region Controller
		#region PageEvents

		

		private void SetUpModel()
		{
			InsertionOrderItems = ViewState["InsertionOrderItems"] as List<InsertionOrderItem>;

			if (ContainerPage.Url["K"].IsNull) { SetupModelWithNewObjects(); }
			else { LoadModelFromDatabase(); }
			if (IsPostBack)
			{
				UpdateModelWithPostbackValues();
			}
			


			ViewState["InsertionOrderItems"] = InsertionOrderItems;
		}
		#region SetupModelWithNewObjects
		private void SetupModelWithNewObjects()
		{
			if (!IsPostBack)
			{
				DuplicateGuid = Guid.NewGuid();
			}
			InsertionOrder = new InsertionOrder()
									{
										ActionUsrK = Usr.Current.K,
										DateTimeCreated = DateTime.Now.Date,
										TrafficUsrK = Common.Properties.TrafficUsrK,
										Status = InsertionOrder.InsertionOrderStatus.Proforma,
										CampaignCredits = 0 
									};
			SalesUsrs = new List<Usr>(Usr.GetCurrentSalesUsrsNameAndK());
			if (InsertionOrderItems == null) { InsertionOrderItems = new List<InsertionOrderItem>(); }
		}

		#endregion
		#region LoadModelFromDatabase
		private void LoadModelFromDatabase()
		{
			InsertionOrder = new InsertionOrder(Convert.ToInt32(ContainerPage.Url["K"].Value));

			SalesUsrs = new List<Usr>(Usr.GetCurrentAndPreviousSalesUsrsNameAndK());
			
			if (InsertionOrderItems == null) { InsertionOrderItems = new List<InsertionOrderItem>(InsertionOrder.InsertionOrderItems); }
			if (InsertionOrder.PromoterK > 0 && InsertionOrder.Promoter.IsAgency) { InsertionOrder.AgencyDiscount = 0.3d; }
		}
		#endregion
		#region UpdateModelWithPostbackValues
		private void UpdateModelWithPostbackValues()
		{
			if (this.uiPromoterAutoComplete.Value.Length > 0) this.InsertionOrder.PromoterK = Convert.ToInt32(this.uiPromoterAutoComplete.Value);
			this.InsertionOrder.ClientRef = this.uiCustomerRef.Text;
			this.InsertionOrder.CampaignStartDate = this.uiCampaignStartCal.Date;
			this.InsertionOrder.CampaignEndDate = this.uiCampaignEndCal.Date;
			this.InsertionOrder.NextInvoiceDue = this.uiNextInvoiceDue.Date;
			this.InsertionOrder.PaymentTerms = this.uiPaymentTerms.Text;
			this.InsertionOrder.InvoicePeriod = this.uiInvoicePeriod.Text;
			this.InsertionOrder.UsrNameOverride = this.uiUsrNameOverride.Text;
			this.InsertionOrder.CampaignName = this.uiCampaignName.Text;
			if (this.uiActionUserAutoComplete.Text.Length > 0) { this.InsertionOrder.ActionUsrK = int.Parse(this.uiActionUserAutoComplete.Value); }

			if (this.InsertionOrder.PromoterK > 0)
			{
				this.InsertionOrder.UsrK = (this.uiUserDropDownList.SelectedValue.Length > 0) ? Convert.ToInt32(this.uiUserDropDownList.SelectedValue) : 0;
			}
			this.InsertionOrder.AgencyDiscount = Convert.ToDouble(uiAgencyDiscount.Text) / 100d;
			this.InsertionOrder.Notes = this.uiNotesAddOnlyTextBox.ReadOnlyTextBox.Text;
			this.InsertionOrder.CampaignCreditsOverriden = this.uiCampaignCreditsOverrideCheckBox.Checked;
			this.InsertionOrder.CampaignCredits = this.InsertionOrder.CampaignCreditsOverriden ? (int)Convert.ToDecimal(this.uiCampaignCreditsOverride.Text) : (int) CalculateCampaignCredits();
		}
		#endregion

		#endregion
		#region User
		protected void uiPromoterDbCombo_SelectedItemChanged(object sender, EventArgs e)
		{
			InsertionOrder.AgencyDiscount = this.InsertionOrder.Promoter.IsAgency ? 0.3d : 0.0d;
			InsertionOrder.UsrK = 0;
		}
		protected void uiSaveButton_Click(object sender, EventArgs e)
		{
			
			bool isNewInsertionOrder = InsertionOrder.K == 0;
			SaveForm();
			if (isNewInsertionOrder) { Response.Redirect(ContainerPage.Url.CurrentUrl("K", InsertionOrder.K)); }
		}

		private void SaveForm()
		{
			
			InsertionOrder.Update();
			foreach (InsertionOrderItem item in InsertionOrderItems)
			{
				item.InsertionOrderK = InsertionOrder.K;
				item.UpdateWithRecalculation();
			}
			
		}

		protected void uiCancelButton_Click(object sender, EventArgs e)
		{
			Response.Redirect(Admin.AdminMainAccounting.Uri);
		}

		protected void uiAddLinkButton_Click(object sender, EventArgs e)
		{
			GridViewRow row = uiInsertionOrderItemGridView.FooterRow;
			InsertionOrderItem insertionOrderItem = new InsertionOrderItem()
										 {
											 Description = ((TextBox)row.FindControl("uiDescription")).Text,
											 Discount = Convert.ToDouble(((TextBox)row.FindControl("uiDiscount")).Text) / 100,
											 BannerPosition = int.Parse(((DropDownList)row.FindControl("uiBannerPosition")).SelectedValue),
											 AgencyDiscount = Convert.ToDouble(this.uiAgencyDiscount.Text) / 100
										 };
			if (insertionOrderItem.BannerPosition == 0)
			{
				insertionOrderItem.PriceBeforeDiscount = decimal.Parse(((TextBox)row.FindControl("uiGrossCost")).Text);
			}
			else
			{
				insertionOrderItem.Cpm = decimal.Parse(((TextBox)row.FindControl("uiCpm")).Text);
				insertionOrderItem.ImpressionQuantity = int.Parse(((TextBox)row.FindControl("uiImpressionQuantity")).Text);
			}
			this.InsertionOrderItems.Add(insertionOrderItem);
		}

		#endregion

		protected void uiRaiseButton_Click(object sender, EventArgs e)
		{
			Page.Validate("OnRaiseValidators");
			if (Page.IsValid)
			{
				
				
				
				
				InsertionOrder duplicate = InsertionOrderWithMatchingDuplicateGuid;
				if (duplicate != null)
				{
					Response.Redirect(ContainerPage.Url.CurrentUrl("K", duplicate.K));
				}
				else
				{
					
					InsertionOrder.Status = InsertionOrder.InsertionOrderStatus.Raised;
					InsertionOrder.DuplicateGuid = DuplicateGuid.Value;
					SaveForm();
					CampaignCredit credit = new CampaignCredit()
											{
												ActionDateTime = DateTime.Now,
												Credits = InsertionOrder.CampaignCredits,
                                                BuyableObjectK = InsertionOrder.K,
                                                BuyableObjectType = Model.Entities.ObjectType.InsertionOrder,
												PromoterK = InsertionOrder.PromoterK,
												Enabled = true
											};
					credit.UpdateWithRecalculateBalance();
														
												     
					
					Response.Redirect(ContainerPage.Url.CurrentUrl("K", InsertionOrder.K)); 
				}
			}
		}

		protected InsertionOrder InsertionOrderWithMatchingDuplicateGuid
		{
			get
			{
				Query query = new Query(new Q(Bobs.InsertionOrder.Columns.DuplicateGuid, DuplicateGuid.Value));
				InsertionOrderSet set = new InsertionOrderSet(query);
				return (set.Count > 0) ? set[0] : null;
			}
		}

		private Guid? DuplicateGuid{
			get
			{
				Guid? guid = ViewState["DuplicateGuid"] as Guid?;
				if (guid == null)
				{
					DuplicateGuid = Guid.NewGuid();
					return DuplicateGuid;
				}
				else
				{
					return guid;
				}
			}
			set
			{
				ViewState["DuplicateGuid"] = value;
			}
		}
#endregion
		#region Validators

		protected void uiCalHasValue_ServerValidate(object source, ServerValidateEventArgs args)
		{
			args.IsValid = DateTime.Parse(args.Value) > DateTime.Parse("01/01/0001 00:00:00");
		}
		

		protected void uiCampaignEndDateAfterStartValidator_ServerValidate(object source, ServerValidateEventArgs args)
		{
			args.IsValid = InsertionOrder.CampaignStartDate <= InsertionOrder.CampaignEndDate;
		}

		protected void uiUsrValidator_ServerValidate(object source, ServerValidateEventArgs args)
		{
			args.IsValid = (InsertionOrder.PromoterK > 0) || InsertionOrder.UsrK > 0;
		}



		protected void uiActionUsrValidator_ServerValidate(object source, ServerValidateEventArgs args)
		{
			args.IsValid = Convert.ToInt32(args.Value) > 0;
		}

		#endregion

		protected void uiAgencyDiscount_TextChanged(object sender, EventArgs e)
		{
	
		}
		
	}
}
