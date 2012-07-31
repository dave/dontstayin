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
using Local;

namespace Spotted.Admin
{
    public partial class BankExport : AdminUserControl
    {
        //private const string NEXT_BATCH = "Next Batch";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Usr.Current == null || !Usr.Current.IsSuperAdmin)
                throw new DsiUserFriendlyException("You do not have permission to view this page.");

            SearchBankExportHeader.Style["color"] = "Black";

            if (!this.IsPostBack)
            {
                //Bobs.BankExport.GenerateBankExportsForAllNonTicketFundsToDate();
                
                SetupBankExportRadioButtonList();
                SetupTypeDropDownList();
                SetupStatusDropDownList();

                LoadGridViewOfBatchesOnStart();
            }
            GenerateNextBatchUrl();
        }

        #region Enums
        private enum Options
        {
            NextBatch = 1,
            AwaitingConfirmation = 2,
            Search = 3
        }
        #endregion

        #region Methods
        #region Setup Lists
        private void SetupTypeDropDownList()
        {
            this.TypeDropDownList.Items.Clear();
            this.TypeDropDownList.Items.Add(new ListItem("", ""));
            this.TypeDropDownList.Items.AddRange(Bobs.BankExport.TypesAsListItemArray());
        }
        
        private void SetupStatusDropDownList()
        {
            this.StatusDropDownList.Items.Clear();
            this.StatusDropDownList.Items.Add(new ListItem("", ""));
            //this.StatusDropDownList.Items.Add(new ListItem(NEXT_BATCH, NEXT_BATCH));
            this.StatusDropDownList.Items.AddRange(Bobs.BankExport.StatusesAsListItemArray());

            //this.StatusDropDownList.SelectedValue = NEXT_BATCH;
        }

        private void SetupBankExportRadioButtonList()
        {
            this.BankExportRadioButtonList.Items.Clear();
            this.BankExportRadioButtonList.Items.Add(new ListItem(Utilities.CamelCaseToString(Options.NextBatch.ToString()), Convert.ToInt32(Options.NextBatch).ToString()));
            this.BankExportRadioButtonList.Items.Add(new ListItem(Utilities.CamelCaseToString(Options.AwaitingConfirmation.ToString()), Convert.ToInt32(Options.AwaitingConfirmation).ToString()));
            this.BankExportRadioButtonList.Items.Add(new ListItem(Utilities.CamelCaseToString(Options.Search.ToString()), Convert.ToInt32(Options.Search).ToString()));

            this.BankExportRadioButtonList.SelectedValue = Convert.ToInt32(Options.AwaitingConfirmation).ToString();
        }
        #endregion

        private void GenerateNextBatchUrl()
        {
            string randomString =Cambro.Misc.Utility.GenRandomText(6);
            BankExportGeneratorLinkLabel.Text = Utilities.LinkNewWindow("/popup/BankExportToBarclays?" + randomString, "http://" + Vars.DomainName + "/popup/BankExportToBarclays?" + randomString);
            BankExportGeneratorLinkLabel.Visible = true;
        }

        private void LoadGridViewOfBatchesOnStart()
        {
            LoadGridViewOfAwaitingConfirmation();
            if (this.SearchBankExportGridView.DataSource == null || ((BankExportSet)this.SearchBankExportGridView.DataSource).Count == 0)
            {
                LoadGridViewOfNextBatch();
            }
        }

        private void ClearSearchInputs()
        {
            this.TypeDropDownList.SelectedValue = "";
            this.BatchRefTextBox.Text = "";
            this.StatusDropDownList.SelectedValue = "";
            this.ExportDateCal.Date = DateTime.MinValue;
            this.uiPromoterHtmlAutoComplete.Value = "";
			this.uiPromoterHtmlAutoComplete.Text = "";
        }

        private void LoadGridViewOfNextBatch()
        {
            //this.ClearSearchInputs();
            SearchBankExportHeader.InnerHtml = "Next batch! Please review and verify details before exporting to bank.";
            if (this.BankExportRadioButtonList.SelectedValue != Convert.ToInt32(Options.NextBatch).ToString())
                this.BankExportRadioButtonList.SelectedValue = Convert.ToInt32(Options.NextBatch).ToString();
            LoadGridViewOfSearchBankExport();

            if (SearchBankExportGridView.DataSource != null && ((BankExportSet)this.SearchBankExportGridView.DataSource).Count > 0)
            {
                GenerateNextBatchUrl();
                BankExportLinkP.Visible = true;
            }
        }

        private void LoadGridViewOfAwaitingConfirmation()
        {
            //this.ClearSearchInputs();
            SearchBankExportHeader.InnerHtml = "Awaiting confirmation! Please submit the results of the last batch export.";

            if(this.BankExportRadioButtonList.SelectedValue != Convert.ToInt32(Options.AwaitingConfirmation).ToString())
                this.BankExportRadioButtonList.SelectedValue = Convert.ToInt32(Options.AwaitingConfirmation).ToString();
            LoadGridViewOfSearchBankExport();
        }

        private void LoadGridViewOfSearchBankExport()
        {
            Query searchBankExportQuery = new Query();

            List<Q> QueryConditionList = new List<Q>();

            searchBankExportQuery.OrderBy = new OrderBy(new OrderBy(Bobs.BankExport.Columns.Type, OrderBy.OrderDirection.Descending), new OrderBy(Bobs.BankExport.Columns.ReferenceDateTime), new OrderBy(Bobs.BankExport.Columns.K, OrderBy.OrderDirection.Descending));

            if (this.BankExportRadioButtonList.SelectedValue == Convert.ToInt32(Options.NextBatch).ToString())
            {
                QueryConditionList.Add(new Or(new Q(Bobs.BankExport.Columns.Status, Bobs.BankExport.Statuses.Added),
                                              new Q(Bobs.BankExport.Columns.Status, Bobs.BankExport.Statuses.AwaitingConfirmation),
                                              new Q(Bobs.BankExport.Columns.Status, Bobs.BankExport.Statuses.Failed)));
            }
            else if (this.BankExportRadioButtonList.SelectedValue == Convert.ToInt32(Options.AwaitingConfirmation).ToString())
            {
                QueryConditionList.Add(new Q(Bobs.BankExport.Columns.Status, Bobs.BankExport.Statuses.AwaitingConfirmation));
            }
            else
            {
                searchBankExportQuery.OrderBy = new OrderBy(new OrderBy(Bobs.BankExport.Columns.ReferenceDateTime), new OrderBy(Bobs.BankExport.Columns.K, OrderBy.OrderDirection.Descending));

				if (this.uiPromoterHtmlAutoComplete.Value != null && !uiPromoterHtmlAutoComplete.Value.Equals(""))
					QueryConditionList.Add(new Q(Bobs.BankExport.Columns.PromoterK, Convert.ToInt32(uiPromoterHtmlAutoComplete.Value)));
                if (this.StatusDropDownList.SelectedValue != "")
                {
                    QueryConditionList.Add(new Q(Bobs.BankExport.Columns.Status, Convert.ToInt32(this.StatusDropDownList.SelectedValue)));
                }
                if (this.TypeDropDownList.SelectedValue != "")
                    QueryConditionList.Add(new Q(Bobs.BankExport.Columns.Type, Convert.ToInt32(this.TypeDropDownList.SelectedValue)));
                if (this.ExportDateCal.Date != DateTime.MinValue)
                {
                    QueryConditionList.Add(new And(new Q(Bobs.BankExport.Columns.OutputDateTime, QueryOperator.GreaterThanOrEqualTo, this.ExportDateCal.Date),
                                                   new Q(Bobs.BankExport.Columns.OutputDateTime, QueryOperator.LessThan, this.ExportDateCal.Date.AddDays(1))));
                }
                if (this.BatchRefTextBox.Text.Trim().Length > 0)
                    QueryConditionList.Add(new Q(Bobs.BankExport.Columns.BatchRef, QueryOperator.TextContains, this.BatchRefTextBox.Text.Trim()));
            }
            searchBankExportQuery.QueryCondition = new And(QueryConditionList.ToArray());      

            BankExportSet searchBankExports = new BankExportSet(searchBankExportQuery);

            
                if (this.BankExportRadioButtonList.SelectedValue == Convert.ToInt32(Options.NextBatch).ToString())
                {
                    try
                    {
                        foreach (Bobs.BankExport be in searchBankExports)
                        {
                            be.Validate();
                        }
                    }
                    catch (Exception ex)
                    {
                        SearchBankExportHeader.InnerHtml = ex.Message;
                        SearchBankExportHeader.Style["color"] = "Red";
                        BankExportLinkP.Visible = false;
                    }
                    searchBankExports.Reset();
                }
                this.SearchBankExportGridView.DataSource = searchBankExports;
                this.SearchBankExportGridView.DataBind();

                SearchResultsMessageLabel.Visible = searchBankExports.Count == 0;
                if (searchBankExports.Count == 0)
                {
                    SearchResultsMessageLabel.Text = "<br>* Zero search results returned.";
                }
                SumBankExports(searchBankExports);
            
        }

        private void SumBankExports(BankExportSet bankExports)
        {
			//decimal clientToCurrent = 0;
			//decimal currentToClient = 0;
			//decimal clientToPromoter = 0;
			decimal currentToPromoter = 0;
            
            if (bankExports != null)
            {




                bankExports.Reset();
                foreach (Bobs.BankExport be in bankExports)
                {
					currentToPromoter += be.Amount;


					//if (be.Type == Bobs.BankExport.Types.ExternalBACSRefundToPromoter || be.Type == Bobs.BankExport.Types.InternalTransferRefundToPromoter)
					//    currentToPromoter += be.Amount;
					//else if (be.Type == Bobs.BankExport.Types.ExternalBACSTicketFundsToPromoter || be.Type == Bobs.BankExport.Types.InternalTransferTicketFundsToPromoter)
					//    clientToPromoter += be.Amount;
					//else
					//{
					//    if (be.BankAccountNumber == Vars.DSI_CLIENT_BANK_ACCOUNT_NUMBER && be.BankAccountSortCode == Vars.DSI_CLIENT_BANK_SORT_CODE)
					//        currentToClient += be.Amount;
					//    else
					//        clientToCurrent += be.Amount;
					//}
                }
            }

            //this.FundsClientToCurrentLabel.Text = clientToCurrent.ToString("c");
            //this.FundsClientToPromoterLabel.Text = clientToPromoter.ToString("c");
            //this.FundsCurrentToClientLabel.Text = currentToClient.ToString("c");
            this.FundsCurrentToPromoterLabel.Text = currentToPromoter.ToString("c");
        }
        #endregion

        #region Page Event Handlers
        protected void SearchButton_Click(object sender, EventArgs e)
        {
            SearchBankExportGridView.PageIndex = 0;
            LoadGridViewOfSearchBankExport();
            //this.SearchBankExportHeader.InnerHtml = "Search results";
        }
        protected void ClearButton_Click(object sender, EventArgs e)
        {
            this.ClearSearchInputs();
        }
        protected void BankExportRadioButtonList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchCriteriaTable.Visible = false;
            SearchBankExportHeader.InnerHtml = "";
            SearchResultsMessageLabel.Text = "";
            BankExportLinkP.Visible = false;
            SearchBankExportGridView.AllowPaging = false;
            SearchBankExportGridView.ShowFooter = true;

            if (BankExportRadioButtonList.SelectedValue == Convert.ToInt32(Options.AwaitingConfirmation).ToString())
            {
                this.LoadGridViewOfAwaitingConfirmation();
            }
            else if (BankExportRadioButtonList.SelectedValue == Convert.ToInt32(Options.NextBatch).ToString())
            {
                this.LoadGridViewOfNextBatch();
                SearchBankExportGridView.ShowFooter = false;
            }
            else
            {
                // do not perform search. only display search criteria options.
                this.SearchBankExportGridView.DataSource = null;
                this.SearchBankExportGridView.DataBind();
                SumBankExports(null);
                SearchCriteriaTable.Visible = true;
                SearchBankExportGridView.AllowPaging = true;
            }
        }
        #endregion

        #region Search Bank Export GridView Event Handlers
        protected void SearchBankExportGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                CheckBox selectCheckBox = (CheckBox)e.Row.FindControl("BankExportSelectAllCheckBox");
                selectCheckBox.Attributes.Remove("onclick");
                selectCheckBox.Attributes.Add("onclick", "SelectAllCheckBoxes(" + SearchBankExportGridView.ClientID + ", " + selectCheckBox.ClientID + ");");
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox selectCheckBox = (CheckBox)e.Row.FindControl("BankExportSelectCheckBox");
                selectCheckBox.Attributes.Remove("onclick");
                selectCheckBox.Attributes.Add("onclick", "SetAllCheckBox(" + SearchBankExportGridView.ClientID + ", " + SearchBankExportGridView.HeaderRow.FindControl("BankExportSelectAllCheckBox").ClientID + ");");
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                DropDownList statusDropDownList = (DropDownList)e.Row.FindControl("BankExportStatusDropDownList");
                statusDropDownList.Items.Clear();
                statusDropDownList.Items.AddRange(Bobs.BankExport.StatusesAsListItemArray());
                statusDropDownList.SelectedValue = Convert.ToInt32(Bobs.BankExport.Statuses.Successful).ToString();
            }
        }

        protected void SearchBankExportGridView_DataBound(object sender, EventArgs e)
        {
            // Column 2=Exported, 3=Processed, 12=Update
            SearchBankExportGridView.Columns[2].Visible = true;
            SearchBankExportGridView.Columns[3].Visible = true;
            SearchBankExportGridView.Columns[12].Visible = true;

            if (this.BankExportRadioButtonList.SelectedValue == Convert.ToInt32(Options.NextBatch).ToString())
            {
                SearchBankExportGridView.Columns[2].Visible = false;
                SearchBankExportGridView.Columns[3].Visible = false;
                SearchBankExportGridView.Columns[12].Visible = false;
            }
            else if (this.BankExportRadioButtonList.SelectedValue == Convert.ToInt32(Options.AwaitingConfirmation).ToString())
            {
                SearchBankExportGridView.Columns[3].Visible = false;
            }
        }		

        protected void SearchBankExportGrivView_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
            // Cancel the paging operation if the user attempts to navigate
            // to another page while the GridView control is in edit mode. 
            if (SearchBankExportGridView.EditIndex != -1)
            {
                // Use the Cancel property to cancel the paging operation.
                e.Cancel = true;

                // Display an error message.
                int newPageNumber = e.NewPageIndex + 1;
                SearchResultsMessageLabel.Text = "* Please update the record before moving to page " + newPageNumber.ToString() + ".";
                SearchResultsMessageLabel.Visible = true;
            }
            else
            {
                SearchBankExportGridView.PageIndex = e.NewPageIndex;
                LoadGridViewOfSearchBankExport();
                if (SearchBankExportGridView.PageIndex > SearchBankExportGridView.PageCount)
                    SearchBankExportGridView.PageIndex = 1;

                // Clear the error message.
                SearchResultsMessageLabel.Text = "";
                SearchResultsMessageLabel.Visible = false;
            }
		}


        protected void UpdateSearchGridViewStatusButton_Click(object sender, EventArgs e)
        {
            DropDownList BankExportStatusDDL = (DropDownList)SearchBankExportGridView.FooterRow.FindControl("BankExportStatusDropDownList");
            foreach (GridViewRow row in this.SearchBankExportGridView.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    try
                    {
                        if (((CheckBox)row.FindControl("BankExportSelectCheckBox")).Checked)
                        {
                            Bobs.BankExport be = new Bobs.BankExport(Convert.ToInt32(((TextBox)row.FindControl("BankExportKTextBox")).Text));
                            be.UpdateStatus((Bobs.BankExport.Statuses)Convert.ToInt32(BankExportStatusDDL.SelectedValue));
                        }
                    }
                    catch { }
                }
            }
            this.LoadGridViewOfSearchBankExport();
        }

        #endregion
    }
}
