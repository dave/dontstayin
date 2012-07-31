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
    public partial class AdminMainAccounting : AdminUserControl
	{
		public const string Uri = "/admin/adminmainaccounting";

		#region Enums
		public enum SearchType
		{
			Invoice = 1,
			//Credit = 2,
			Transfer = 3,
			InvoiceItem = 4,
			InsertionOrder = 5,
            CampaignCredit = 6
		}

		private enum InvoiceDateSearchType
		{
			CreatedDate = 1,
			PaidDate = 2,
			DueDate = 3,
			TaxDate = 4
		}
		
		private enum InvoiceItemDateSearchType
		{
			RevenueStartDate = 1,
			RevenueEndDate = 2
		}

		private enum TransferDateSearchType
		{
			CreatedDate = 1,
			CompletedDate = 2
		}

		private enum InsertionOrderDateSearchType
		{
			NextInvoiceDueDate,
			CreatedDate,
			CampaignStartDate,
			CampaignEndDate
		}
		#endregion

		#region Page_Load
		protected void Page_Load(object sender, EventArgs e)
        {
            SearchResultsMessageLabel.Text = "";
            SearchResultsMessageLabel.Visible = false;
			this.SageErrorLabel.Visible = false;

			if (!this.IsPostBack)
            {

               // SetupYearDropDownList();
                SetupTypeDropDownList();
				SetupExportToSageTypeDropDownList();
				SetupStatusDropDownList();
                SetupInvoiceTypeDropDownList();
                SetupTransferTypeDropDownList();
				SetupTransferMethodDropDownList();
                //SetupTransferBankAccountDropDownList();
				SetupSalesUserDropDownList();
                SetupInvoiceItemTypeDropDownList();
				SetupDateTypeDropDownList(SearchType.Invoice);

                // To help reduce load on database and speed up results, we set the from date to 1 month ago (as 99% of time admins are searching all records when not necessary)
                this.FromDateCal.Date = DateTime.Today.AddMonths(-1);

                CaptureUrlParameters();
            }
		}
		#endregion

		#region Setup Drop Down Lists
		//private void SetupYearDropDownList()
		//{
		//    this.YearDropDownList.Items.Clear();
		//    int CurrentYear = DateTime.Now.Year;

		//    while (CurrentYear >= 2003)
		//    {
		//        this.YearDropDownList.Items.Add(CurrentYear.ToString());
		//        CurrentYear--;
		//    }
		//}

        private void SetupTypeDropDownList()
        {
            this.TypeDropDownList.Items.Clear();
			Utilities.AddEnumValuesToDropDownList(this.TypeDropDownList, typeof(SearchType), false, false);
        }

		private void SetupStatusDropDownList()
		{
			this.StatusDropDownList.Items.Clear();
			this.StatusDropDownList.Items.Add("");
			this.StatusDropDownList.Items.Add("Paid");
			this.StatusDropDownList.Items.Add("Not Paid");
		}

        private void SetupInvoiceTypeDropDownList()
        {
            this.InvoiceTypeDropDownList.Items.Clear();
			Utilities.AddEnumValuesToDropDownList(this.InvoiceTypeDropDownList, typeof(Invoice.Types), true, false);
        }

		private void SetupTransferTypeDropDownList()
		{
			this.TransferTypeDropDownList.Items.Clear();
            Utilities.AddEnumValuesToDropDownList(this.TransferTypeDropDownList, typeof(Transfer.TransferTypes), true, true);
		}

		private void SetupTransferMethodDropDownList()
		{
			this.TransferMethodDropDownList.Items.Clear();
            Utilities.AddEnumValuesToDropDownList(this.TransferMethodDropDownList, typeof(Transfer.Methods), true, true);
		}

        private void SetupTransferBankAccountDropDownList()
		{
            //this.BankAccountDropDownList.Items.Clear();
            //this.BankAccountDropDownList.Items.Add(new ListItem("", ""));
            //this.BankAccountDropDownList.Items.AddRange(Transfer.DSIBankAccountsAsListItemArray());
            //this.BankAccountDropDownList.SelectedIndex = 0;
		}

        private void SetupInvoiceItemTypeDropDownList()
		{
            this.InvoiceItemTypeDropDownList.Items.Clear();
            this.InvoiceItemTypeDropDownList.Items.Add(new ListItem("", ""));
            this.InvoiceItemTypeDropDownList.Items.AddRange(InvoiceItem.TypesAsListItemArray());
            this.InvoiceItemTypeDropDownList.SelectedIndex = 0;
		}

		private void SetupExportToSageTypeDropDownList()
		{
			this.ExportToSageTypeDropDownList.Items.Clear();
			this.ExportToSageTypeDropDownList.Items.Add(new ListItem("ALL", ""));
            Utilities.AddEnumValuesToDropDownList(this.ExportToSageTypeDropDownList, typeof(Utilities.ExportToSageType), false, false);
		}

		private void SetupSalesUserDropDownList()
		{
			List<UsrDataHolder> salesUsrList = new List<UsrDataHolder>();
			if (ViewState["SalesUsrList"] == null)
			{
				UsrSet salesUsrs = Usr.GetCurrentAndPreviousSalesUsrsNameAndK();
				foreach (Usr salesUsr in salesUsrs)
					salesUsrList.Add(new UsrDataHolder(salesUsr));
				ViewState["SalesUsrList"] = salesUsrList;
			}
			else
			{
				salesUsrList = (List<UsrDataHolder>)ViewState["SalesUsrList"];
			}
			this.SalesUserDropDownList.Items.Clear();
			this.SalesUserDropDownList.Items.Add(new ListItem("", ""));
			foreach (UsrDataHolder udh in salesUsrList)
			{
				this.SalesUserDropDownList.Items.Add(new ListItem(udh.NickName, udh.K.ToString()));
			}
		}

		private void SetupDateTypeDropDownList(SearchType searchType)
		{
			DateTypeDropDownList.Items.Clear();
			Type enumType = null;
			switch (searchType)
			{
				case SearchType.Transfer:
					enumType = typeof(TransferDateSearchType);
					break;
				case SearchType.InvoiceItem:
					enumType = typeof(InvoiceItemDateSearchType);
					break;
				case SearchType.InsertionOrder:
					enumType = typeof(InsertionOrderDateSearchType);
					break;
				case SearchType.Invoice:
					enumType = typeof(InvoiceDateSearchType);
					break;

				default: break;
			}
			if (enumType != null)
			{
				Utilities.AddEnumValuesToDropDownList(DateTypeDropDownList, enumType);
			}
		}
		#endregion

		#region Methods
        private void CaptureUrlParameters()
        {
            try
            {
                if (ContainerPage.Url["SearchType"].IsInt)
                {
                    TypeDropDownList.SelectedValue = ContainerPage.Url["SearchType"].Value;
                    DisplaySearchCriteria();
                }
                if (ContainerPage.Url["K"].IsInt)
                    KNumberTextBox.Text = ContainerPage.Url["K"].Value;
                if (ContainerPage.Url["SalesUsrK"].IsInt)
                    SalesUserDropDownList.SelectedValue = ContainerPage.Url["SalesUsrK"].Value;
                if (!ContainerPage.Url["FromDate"].IsNull)
                    FromDateCal.Date = Convert.ToDateTime(ContainerPage.Url["FromDate"].Value.Replace("_", "/"));
                if (!ContainerPage.Url["ToDate"].IsNull)
                    ToDateCal.Date = Convert.ToDateTime(ContainerPage.Url["ToDate"].Value.Replace("_", "/"));
                //if (!ContainerPage.Url["DSIBankAccount"].IsNull && ContainerPage.Url["DSIBankAccount"].IsInt)
                //    BankAccountDropDownList.SelectedValue = ContainerPage.Url["DSIBankAccount"].Value;
                if (ContainerPage.Url["Method"].IsInt)
                    TransferMethodDropDownList.SelectedValue = ContainerPage.Url["Method"].Value;
                if (ContainerPage.Url["Status"].IsInt)
                    StatusDropDownList.SelectedValue = ContainerPage.Url["Status"].Value;
                if (ContainerPage.Url["PromoterK"].IsInt)
                {
                    this.uiPromotersAutoComplete.Value = ContainerPage.Url["PromoterK"].Value;
                    this.uiPromotersAutoComplete.Text = new Promoter(Convert.ToInt32(ContainerPage.Url["PromoterK"].Value)).Name;
                }
                if (ContainerPage.Url["UsrK"].IsInt)
                {
                    this.uiUserAutoComplete.Value = ContainerPage.Url["UsrK"].Value;
					this.uiUserAutoComplete.Text = new Usr(Convert.ToInt32(ContainerPage.Url["UsrK"].Value)).NickName;
                }
                if (ContainerPage.Url.Count > 0)
                {
                    GetSearchResults();
                }
            }
            catch
            {}
        }

		private SearchType SelectedSearchType
		{
			get
			{
				return (SearchType)Convert.ToInt32(this.TypeDropDownList.SelectedValue);
			}
		}

        private void GetSearchResults()
        {
            // Query the database based on the search parameters
            // Set the returned results to be the data source of the GridView
            Query q = new Query();
            List<Q> queryConditionList = new List<Q>();
			decimal totalVAT = 0;
			decimal totalPrice = 0;
			decimal totalAmount = 0;
			decimal totalTicketSalesVAT = 0;
			decimal totalTicketSalesExVAT = 0;
			decimal totalTicketSales = 0;
			decimal totalBookingFeeSalesVAT = 0;
			decimal totalBookingFeeSalesExVAT = 0;
			decimal totalBookingFeeSales = 0;

			SearchResultsInvoiceGridView.Visible = false;
			SearchResultsTransferGridView.Visible = false;
			SearchResultsInvoiceItemGridView.Visible = false;
			SearchResultsInsertionOrderGridView.Visible = false;
            SearchResultsCampaignCreditGridView.Visible = false;
			
			BobSet bobSet = null;

			switch (SelectedSearchType)
            {
                case SearchType.Invoice:
				//case SearchType.Credit:
					q.OrderBy = new OrderBy(Invoice.Columns.K, OrderBy.OrderDirection.Descending);
                    if (this.KNumberTextBox.Text.Length > 0)
                    {
                        q.QueryCondition = new Q(Invoice.Columns.K, Convert.ToInt32(KNumberTextBox.Text));
                    }
                    else
                    {
                        if (!this.uiPromotersAutoComplete.Value.Equals(""))
                            queryConditionList.Add(new Q(Invoice.Columns.PromoterK, Convert.ToInt32(this.uiPromotersAutoComplete.Value)));
						if (!this.uiUserAutoComplete.Value.Equals(""))
                            queryConditionList.Add(new Q(Invoice.Columns.UsrK, Convert.ToInt32(this.uiUserAutoComplete.Value)));
						if (this.SalesUserDropDownList.SelectedValue != null && !this.SalesUserDropDownList.SelectedValue.Equals(""))
							queryConditionList.Add(new Q(Invoice.Columns.SalesUsrK, Convert.ToInt32(this.SalesUserDropDownList.SelectedValue)));
                        if (this.StatusDropDownList.SelectedValue != null && !StatusDropDownList.SelectedValue.Equals(""))
                            queryConditionList.Add(new Q(Invoice.Columns.Paid, StatusDropDownList.SelectedValue.ToUpper().Equals("PAID") ? true : false));
                        if (this.InvoiceTypeDropDownList.SelectedValue != null && !InvoiceTypeDropDownList.SelectedValue.Equals(""))
                            queryConditionList.Add(new Q(Invoice.Columns.Type, InvoiceTypeDropDownList.SelectedValue));
						if (this.DateTypeDropDownList.SelectedValue == Convert.ToInt32(InvoiceDateSearchType.CreatedDate).ToString())
						{
							if (this.FromDateCal.Date > DateTime.MinValue)
								queryConditionList.Add(new Q(Invoice.Columns.CreatedDateTime, QueryOperator.GreaterThanOrEqualTo, this.FromDateCal.Date));
							if (this.ToDateCal.Date > DateTime.MinValue)
								queryConditionList.Add(new Q(Invoice.Columns.CreatedDateTime, QueryOperator.LessThan, this.ToDateCal.Date.AddDays(1)));
							q.OrderBy = new OrderBy(new OrderBy(Invoice.Columns.CreatedDateTime, OrderBy.OrderDirection.Descending),
												  new OrderBy(Invoice.Columns.K, OrderBy.OrderDirection.Descending));
						}
						else if (this.DateTypeDropDownList.SelectedValue == Convert.ToInt32(InvoiceDateSearchType.DueDate).ToString())
						{
							if (this.FromDateCal.Date > DateTime.MinValue)
								queryConditionList.Add(new Q(Invoice.Columns.DueDateTime, QueryOperator.GreaterThanOrEqualTo, this.FromDateCal.Date));
							if (this.ToDateCal.Date > DateTime.MinValue)
								queryConditionList.Add(new Q(Invoice.Columns.DueDateTime, QueryOperator.LessThan, this.ToDateCal.Date.AddDays(1)));
							q.OrderBy = new OrderBy(new OrderBy(Invoice.Columns.DueDateTime, OrderBy.OrderDirection.Descending),
												  new OrderBy(Invoice.Columns.K, OrderBy.OrderDirection.Descending));
						}
						else if (this.DateTypeDropDownList.SelectedValue == Convert.ToInt32(InvoiceDateSearchType.PaidDate).ToString())
						{
							if (this.FromDateCal.Date > DateTime.MinValue)
								queryConditionList.Add(new Q(Invoice.Columns.PaidDateTime, QueryOperator.GreaterThanOrEqualTo, this.FromDateCal.Date));
							if (this.ToDateCal.Date > DateTime.MinValue)
								queryConditionList.Add(new Q(Invoice.Columns.PaidDateTime, QueryOperator.LessThan, this.ToDateCal.Date.AddDays(1)));
							q.OrderBy = new OrderBy(new OrderBy(Invoice.Columns.PaidDateTime, OrderBy.OrderDirection.Descending),
												  new OrderBy(Invoice.Columns.K, OrderBy.OrderDirection.Descending));
						}
						else if (this.DateTypeDropDownList.SelectedValue == Convert.ToInt32(InvoiceDateSearchType.TaxDate).ToString())
						{
							if (this.FromDateCal.Date > DateTime.MinValue)
								queryConditionList.Add(new Q(Invoice.Columns.TaxDateTime, QueryOperator.GreaterThanOrEqualTo, this.FromDateCal.Date));
							if (this.ToDateCal.Date > DateTime.MinValue)
								queryConditionList.Add(new Q(Invoice.Columns.TaxDateTime, QueryOperator.LessThan, this.ToDateCal.Date.AddDays(1)));
							q.OrderBy = new OrderBy(new OrderBy(Invoice.Columns.TaxDateTime, OrderBy.OrderDirection.Descending),
												  new OrderBy(Invoice.Columns.K, OrderBy.OrderDirection.Descending));
						}

                        //if(SelectedSearchType == SearchType.Credit)
                        //    QueryConditionList.Add(new Q(Invoice.Columns.Type, (int)Invoice.Types.Credit));
                        //else if(SelectedSearchType == SearchType.Invoice)
                        //    QueryConditionList.Add(new Q(Invoice.Columns.Type, (int)Invoice.Types.Invoice));

 						q.QueryCondition = new And(queryConditionList.ToArray());                       
                    }

                    q.Columns = new ColumnSet(Invoice.Columns.K,
											  Invoice.Columns.CreatedDateTime,
											  Invoice.Columns.TaxDateTime,
											  Invoice.Columns.PaidDateTime,
											  Invoice.Columns.Type,
											  Invoice.Columns.Price,
											  Invoice.Columns.Vat,
                                              Invoice.Columns.Total,
                                              Invoice.Columns.Paid,
											  Invoice.Columns.DueDateTime,
											  Invoice.Columns.PromoterK,
											  Invoice.Columns.UsrK);

                    InvoiceSet iSet = new InvoiceSet(q);
					bobSet = iSet;
					//SearchResultsGridView.AllowPaging = (iSet.Count > SearchResultsGridView.PageSize);
					//SearchResultsGridView.DataSource = iSet.DataSet.Tables[0];

					// Column 5 is Due date. Do not show for Credits
					//SearchResultsInvoiceGridView.Columns[5].Visible = SelectedSearchType.Equals(SearchType.Invoice);
					SearchResultsInvoiceGridView.AllowPaging = (iSet.Count > SearchResultsInvoiceGridView.PageSize);
					SearchResultsInvoiceGridView.DataSource = iSet;
					SearchResultsInvoiceGridView.Visible = true;
					SearchResultsInvoiceGridView.DataBind();

					iSet.Reset();
					foreach (Invoice invoice in iSet)
					{
                        // Check for user ticket purchases
                        if (invoice.PromoterK == 0)
                        {
                            foreach (InvoiceItem invoiceItem in invoice.Items)
                            {
                                if (invoiceItem.Type == InvoiceItem.Types.EventTickets)
                                {
                                    totalTicketSales += Math.Round(invoiceItem.Total, 2);
                                    totalTicketSalesExVAT += Math.Round(invoiceItem.Price, 2);
                                    totalTicketSalesVAT += Math.Round(invoiceItem.Vat, 2);
                                }
                                else if (invoiceItem.Type == InvoiceItem.Types.EventTicketsBookingFee)
                                {
                                    totalBookingFeeSales += Math.Round(invoiceItem.Total, 2);
                                    totalBookingFeeSalesExVAT += Math.Round(invoiceItem.Price, 2);
                                    totalBookingFeeSalesVAT += Math.Round(invoiceItem.Vat, 2);
                                }
                            }
                        }
                        totalPrice += Math.Round(invoice.Price, 2);
                        totalVAT += Math.Round(invoice.Vat, 2);
                        totalAmount += Math.Round(invoice.Total, 2);						
					}
                    break;

                case SearchType.Transfer:
					q.OrderBy = new OrderBy(Transfer.Columns.K, OrderBy.OrderDirection.Descending);
                    if (this.KNumberTextBox.Text.Length > 0)
                    {
                        q.QueryCondition = new Q(Transfer.Columns.K, Convert.ToInt32(KNumberTextBox.Text));
                    }
                    else
                    {
                        if (!this.uiPromotersAutoComplete.Value.Equals(""))
							queryConditionList.Add(new Q(Transfer.Columns.PromoterK, Convert.ToInt32(this.uiPromotersAutoComplete.Value)));
						if (this.uiUserAutoComplete.Value != null && !this.uiUserAutoComplete.Value.Equals(""))
							queryConditionList.Add(new Q(Transfer.Columns.UsrK, Convert.ToInt32(this.uiUserAutoComplete.Value)));
                        if (this.StatusDropDownList.SelectedValue != null && !StatusDropDownList.SelectedValue.Equals(""))
                            queryConditionList.Add(new Q(Transfer.Columns.Status, StatusDropDownList.SelectedValue));
						if (this.TransferTypeDropDownList.SelectedValue != null && !TransferTypeDropDownList.SelectedValue.Equals(""))
							queryConditionList.Add(new Q(Transfer.Columns.Type, TransferTypeDropDownList.SelectedValue));
						if (this.TransferCompanyDropDownList.SelectedValue != null && !TransferCompanyDropDownList.SelectedValue.Equals(""))
							queryConditionList.Add(new Q(Transfer.Columns.Company, TransferCompanyDropDownList.SelectedValue));
						if (this.TransferMethodDropDownList.SelectedValue != null && !TransferMethodDropDownList.SelectedValue.Equals(""))
							queryConditionList.Add(new Q(Transfer.Columns.Method, TransferMethodDropDownList.SelectedValue));
						if (this.DateTypeDropDownList.SelectedValue == Convert.ToInt32(TransferDateSearchType.CreatedDate).ToString())
						{
							if (this.FromDateCal.Date > DateTime.MinValue)
								queryConditionList.Add(new Q(Transfer.Columns.DateTimeCreated, QueryOperator.GreaterThanOrEqualTo, this.FromDateCal.Date));
							if (this.ToDateCal.Date > DateTime.MinValue)
								queryConditionList.Add(new Q(Transfer.Columns.DateTimeCreated, QueryOperator.LessThan, this.ToDateCal.Date.AddDays(1)));
							q.OrderBy = new OrderBy(new OrderBy(Transfer.Columns.DateTimeCreated, OrderBy.OrderDirection.Descending),
												  new OrderBy(Transfer.Columns.K, OrderBy.OrderDirection.Descending));
						}
						else if (this.DateTypeDropDownList.SelectedValue == Convert.ToInt32(TransferDateSearchType.CompletedDate).ToString())
						{
							if (this.FromDateCal.Date > DateTime.MinValue)
								queryConditionList.Add(new Q(Transfer.Columns.DateTimeComplete, QueryOperator.GreaterThanOrEqualTo, this.FromDateCal.Date));
							if (this.ToDateCal.Date > DateTime.MinValue)
								queryConditionList.Add(new Q(Transfer.Columns.DateTimeComplete, QueryOperator.LessThan, this.ToDateCal.Date.AddDays(1)));
							q.OrderBy = new OrderBy(new OrderBy(Transfer.Columns.DateTimeComplete, OrderBy.OrderDirection.Descending),
												  new OrderBy(Transfer.Columns.K, OrderBy.OrderDirection.Descending));
						}
                        if (queryConditionList.Count > 0)
                        {
							q.QueryCondition = new And(queryConditionList.ToArray());
                        }
                    }

                    q.Columns = new ColumnSet(Transfer.Columns.K,
                                              Transfer.Columns.DateTimeCreated,
											  Transfer.Columns.DateTimeComplete,
											  Transfer.Columns.Type,
											  Transfer.Columns.Method,
											  Transfer.Columns.Company,
                                              Transfer.Columns.Amount,
											  Transfer.Columns.Status,
											  Transfer.Columns.PromoterK,
											  Transfer.Columns.UsrK,
                                              Transfer.Columns.TransferRefundedK,
                                              Transfer.Columns.DSIBankAccount);

                    TransferSet tSet = new TransferSet(q);
					bobSet = tSet;
					//SearchResultsGridView.AllowPaging = (tSet.Count > SearchResultsGridView.PageSize);
					//SearchResultsGridView.DataSource = tSet;

					SearchResultsTransferGridView.AllowPaging = (tSet.Count > SearchResultsTransferGridView.PageSize);
					SearchResultsTransferGridView.DataSource = tSet;
					SearchResultsTransferGridView.Visible = true;
					SearchResultsTransferGridView.DataBind();

					tSet.Reset();
					foreach (Transfer transfer in tSet)
					{
						totalAmount += Math.Round(transfer.Amount, 2);
					}
                    break;

                case SearchType.InvoiceItem:
					q.OrderBy = new OrderBy(InvoiceItem.Columns.InvoiceK, OrderBy.OrderDirection.Descending);
                    if (this.KNumberTextBox.Text.Length > 0)
                    {
                        q.QueryCondition = new Q(InvoiceItem.Columns.K, Convert.ToInt32(KNumberTextBox.Text));
                    }
                    else
                    {
						List<Q> OrList = new List<Q>();

                        if (this.StatusDropDownList.SelectedValue != null && !StatusDropDownList.SelectedValue.Equals(""))
                            queryConditionList.Add(new Q(InvoiceItem.Columns.ItemProcessed, StatusDropDownList.SelectedValue.ToUpper().Equals("PROCESSED") ? true : false));
						if (this.NominalCodeTextBox.Text.Length > 0)
						{
							InvoiceItem.Types[] Types = InvoiceItem.GetTypesFromNominalCode(Convert.ToInt32(NominalCodeTextBox.Text));

							foreach (InvoiceItem.Types type in Types)
							{
								OrList.Add(new Q(InvoiceItem.Columns.Type, (int)type));
							}
						}
						if (this.SalesUserDropDownList.SelectedValue != null && !this.SalesUserDropDownList.SelectedValue.Equals(""))
						{
							queryConditionList.Add(new Q(Invoice.Columns.SalesUsrK, Convert.ToInt32(this.SalesUserDropDownList.SelectedValue)));
							q.TableElement = new Join(InvoiceItem.Columns.InvoiceK, Invoice.Columns.K);
						}
                        if (this.InvoiceItemTypeDropDownList.SelectedValue != null && !InvoiceItemTypeDropDownList.SelectedValue.Equals(""))
                            queryConditionList.Add(new Q(InvoiceItem.Columns.Type, InvoiceItemTypeDropDownList.SelectedValue));
						if (this.DateTypeDropDownList.SelectedValue == Convert.ToInt32(InvoiceItemDateSearchType.RevenueStartDate).ToString())
						{
							if (this.FromDateCal.Date > DateTime.MinValue)
								queryConditionList.Add(new Q(InvoiceItem.Columns.RevenueStartDate, QueryOperator.GreaterThanOrEqualTo, this.FromDateCal.Date));
							if (this.ToDateCal.Date > DateTime.MinValue)
								queryConditionList.Add(new Q(InvoiceItem.Columns.RevenueStartDate, QueryOperator.LessThan, this.ToDateCal.Date.AddDays(1)));
							q.OrderBy = new OrderBy(new OrderBy(InvoiceItem.Columns.RevenueStartDate, OrderBy.OrderDirection.Descending),
											new OrderBy(InvoiceItem.Columns.InvoiceK, OrderBy.OrderDirection.Descending));
						}
						else if (this.DateTypeDropDownList.SelectedValue == Convert.ToInt32(InvoiceItemDateSearchType.RevenueEndDate).ToString())
						{
							if (this.FromDateCal.Date > DateTime.MinValue)
								queryConditionList.Add(new Q(InvoiceItem.Columns.RevenueEndDate, QueryOperator.GreaterThanOrEqualTo, this.FromDateCal.Date));
							if (this.ToDateCal.Date > DateTime.MinValue)
								queryConditionList.Add(new Q(InvoiceItem.Columns.RevenueEndDate, QueryOperator.LessThan, this.ToDateCal.Date.AddDays(1)));
							q.OrderBy = new OrderBy(new OrderBy(InvoiceItem.Columns.RevenueEndDate, OrderBy.OrderDirection.Descending),
											new OrderBy(InvoiceItem.Columns.InvoiceK, OrderBy.OrderDirection.Descending));
						}

						if (OrList.Count > 0)
							q.QueryCondition = new And(new And(queryConditionList.ToArray()), new Or(OrList.ToArray()));
						else
							q.QueryCondition = new And(queryConditionList.ToArray());

                    }

                    q.Columns = new ColumnSet(InvoiceItem.Columns.K,
                                              InvoiceItem.Columns.RevenueStartDate,
											  InvoiceItem.Columns.RevenueEndDate,
											  InvoiceItem.Columns.Price,
											  InvoiceItem.Columns.Vat,
                                              InvoiceItem.Columns.Total,
                                              InvoiceItem.Columns.Description,
                                              InvoiceItem.Columns.InvoiceK,
											  InvoiceItem.Columns.Type);
                    
                    InvoiceItemSet iiSet = new InvoiceItemSet(q);
					bobSet = iiSet;
					//SearchResultsGridView.AllowPaging = (iiSet.Count > SearchResultsGridView.PageSize);
					//SearchResultsGridView.DataSource = iiSet;

					SearchResultsInvoiceItemGridView.AllowPaging = (iiSet.Count > SearchResultsInvoiceItemGridView.PageSize);
					SearchResultsInvoiceItemGridView.DataSource = iiSet;
					SearchResultsInvoiceItemGridView.Visible = true;
					SearchResultsInvoiceItemGridView.DataBind();

					iiSet.Reset();
					foreach(InvoiceItem invoiceItem in iiSet)
					{
                        if (invoiceItem.Type == InvoiceItem.Types.EventTickets)
                        {
                            totalTicketSales += Math.Round(invoiceItem.Total, 2);
                            totalTicketSalesExVAT += Math.Round(invoiceItem.Price, 2);
                            totalTicketSalesVAT += Math.Round(invoiceItem.Vat, 2);
                        }
                        else if (invoiceItem.Type == InvoiceItem.Types.EventTicketsBookingFee)
                        {
                            totalBookingFeeSales += Math.Round(invoiceItem.Total, 2);
                            totalBookingFeeSalesExVAT += Math.Round(invoiceItem.Price, 2);
                            totalBookingFeeSalesVAT += Math.Round(invoiceItem.Vat, 2);
                        }
						
						totalPrice += Math.Round(invoiceItem.Price, 2);
						totalVAT += Math.Round(invoiceItem.Vat, 2);
						totalAmount += Math.Round(invoiceItem.Total, 2);
					}
                    break;

				case SearchType.InsertionOrder:
					q.OrderBy = new OrderBy(InsertionOrder.Columns.K, OrderBy.OrderDirection.Descending);
					if (this.KNumberTextBox.Text.Length > 0)
					{
						q.QueryCondition = new Q(InsertionOrder.Columns.K, Convert.ToInt32(KNumberTextBox.Text));
					}
					else
					{
						if (this.StatusDropDownList.SelectedValue != null && !StatusDropDownList.SelectedValue.Equals(""))
							queryConditionList.Add(new Q(InsertionOrder.Columns.Status, Convert.ToInt32(StatusDropDownList.SelectedValue)));

					
						switch ((InsertionOrderDateSearchType)Convert.ToInt32(this.DateTypeDropDownList.SelectedValue))
						{
							case InsertionOrderDateSearchType.NextInvoiceDueDate:
								AddDateRangeToQueryConditionList(queryConditionList, InsertionOrder.Columns.NextInvoiceDue);
								q.OrderBy = new OrderBy(new OrderBy(InsertionOrder.Columns.NextInvoiceDue, OrderBy.OrderDirection.Descending),
									new OrderBy(InsertionOrder.Columns.K, OrderBy.OrderDirection.Descending));
								break;
							case InsertionOrderDateSearchType.CreatedDate:
								AddDateRangeToQueryConditionList(queryConditionList, InsertionOrder.Columns.DateTimeCreated);
								q.OrderBy = new OrderBy(new OrderBy(InsertionOrder.Columns.DateTimeCreated, OrderBy.OrderDirection.Descending),
									new OrderBy(InsertionOrder.Columns.K, OrderBy.OrderDirection.Descending));
								break;
							case InsertionOrderDateSearchType.CampaignStartDate:
								AddDateRangeToQueryConditionList(queryConditionList, InsertionOrder.Columns.CampaignStartDate);
								q.OrderBy = new OrderBy(new OrderBy(InsertionOrder.Columns.CampaignStartDate, OrderBy.OrderDirection.Descending),
									new OrderBy(InsertionOrder.Columns.K, OrderBy.OrderDirection.Descending));
								break;
							case InsertionOrderDateSearchType.CampaignEndDate:
								AddDateRangeToQueryConditionList(queryConditionList, InsertionOrder.Columns.CampaignEndDate);
								q.OrderBy = new OrderBy(new OrderBy(InsertionOrder.Columns.CampaignEndDate, OrderBy.OrderDirection.Descending),
									new OrderBy(InsertionOrder.Columns.K, OrderBy.OrderDirection.Descending));
								break;
							default: break;
						}

						if (this.uiPromotersAutoComplete.Value != "")
						{
							queryConditionList.Add(new Q(InsertionOrder.Columns.PromoterK, int.Parse(this.uiPromotersAutoComplete.Value)));
						}
						if (this.uiUserAutoComplete.Value != "")
						{
							queryConditionList.Add(new Q(InsertionOrder.Columns.UsrK, int.Parse(this.uiUserAutoComplete.Value)));
						}

						q.QueryCondition = new And(queryConditionList.ToArray());

					}

					

					InsertionOrderSet ioSet = new InsertionOrderSet(q);
					bobSet = ioSet;

					SearchResultsInsertionOrderGridView.AllowPaging = (ioSet.Count > SearchResultsInsertionOrderGridView.PageSize);
					SearchResultsInsertionOrderGridView.DataSource = ioSet;
					SearchResultsInsertionOrderGridView.Visible = true;
					SearchResultsInsertionOrderGridView.DataBind();

					ioSet.Reset();
					break;

                case SearchType.CampaignCredit:
					q.OrderBy = new OrderBy(new OrderBy(CampaignCredit.Columns.ActionDateTime, OrderBy.OrderDirection.Descending), new OrderBy(CampaignCredit.Columns.DisplayOrder, OrderBy.OrderDirection.Descending));
					queryConditionList.Add(new Q(CampaignCredit.Columns.Enabled, true));
                    if (this.KNumberTextBox.Text.Length > 0)
                    {
                        q.QueryCondition = new Q(CampaignCredit.Columns.K, Convert.ToInt32(KNumberTextBox.Text));
                    }
                    else
                    {
                        if (this.uiPromotersAutoComplete.Value != "")
                        {
							queryConditionList.Add(new Q(CampaignCredit.Columns.PromoterK, int.Parse(this.uiPromotersAutoComplete.Value)));
                        }
                        if (this.uiUserAutoComplete.Value != "")
                        {
							queryConditionList.Add(new Q(CampaignCredit.Columns.UsrK, int.Parse(this.uiUserAutoComplete.Value)));
                        }

                        q.QueryCondition = new And(queryConditionList.ToArray());

                    }
					AddDateRangeToQueryConditionList(queryConditionList, CampaignCredit.Columns.ActionDateTime);

                    CampaignCreditSet ccSet = new CampaignCreditSet(q);
                    bobSet = ccSet;

					foreach (CampaignCredit cc in ccSet)
					{
						totalAmount += cc.Credits;
					}

                    SearchResultsCampaignCreditGridView.AllowPaging = (ccSet.Count > SearchResultsCampaignCreditGridView.PageSize);
                    SearchResultsCampaignCreditGridView.DataSource = ccSet;
                    SearchResultsCampaignCreditGridView.Visible = true;
                    SearchResultsCampaignCreditGridView.DataBind();

                    ccSet.Reset();
                    break;
                default:
					//SearchResultsGridView.DataSource = null;
                    break;
            }

			//SearchResultsGridView.DataBind();
			//SearchResultsGridView.Visible = true;

			if (bobSet == null || bobSet.Count == 0)
			{
				SearchResultsMessageLabel.Text = "* Zero results for your search.  Please verify your search criteria.";
				SearchResultsMessageLabel.Visible = true;

				this.TotalRow.Visible = false;
				this.TotalExVatRow.Visible = false;
				this.TotalVatRow.Visible = false;
                this.TotalTransferRow.Visible = false;
				//totalAmount = 0;
				//this.TotalValueLabel.Text = totalAmount.ToString("c");
			}
			else
			{
				if (SelectedSearchType != SearchType.InsertionOrder)
				{
					this.TotalRow.Visible = true;
					if(SelectedSearchType == SearchType.CampaignCredit)
						this.TotalValueLabel.Text = totalAmount.ToString("N0") + " credits";
					else
						this.TotalValueLabel.Text = totalAmount.ToString("c");
				}

				if (SelectedSearchType == SearchType.Invoice || SelectedSearchType == SearchType.InvoiceItem)
				{
					if (Math.Round(totalPrice + totalVAT, 2) != Math.Round(totalAmount, 2))
						totalAmount = totalPrice + totalVAT;

                    this.TotalTransferRow.Visible = false;
					this.TotalVatRow.Visible = true;
					this.TotalExVatRow.Visible = true;
                    this.TotalRow.Visible = true;
                    this.TotalValueLabel.Text = totalAmount.ToString("c");
					this.TotalExVatValueLabel.Text = totalPrice.ToString("c");
					this.TotalVatValueLabel.Text = totalVAT.ToString("c");

                    this.TicketSalesExVATValueLabel.Text = totalTicketSalesExVAT.ToString("c");
                    this.TicketSalesVATValueLabel.Text = totalTicketSalesVAT.ToString("c");
                    this.TicketSalesTotalValueLabel.Text = totalTicketSales.ToString("c");

                    this.BookingFeeExVATValueLabel.Text = totalBookingFeeSalesExVAT.ToString("c");
                    this.BookingFeeVATValueLabel.Text = totalBookingFeeSalesVAT.ToString("c");
					this.BookingFeeTotalValueLabel.Text = totalBookingFeeSales.ToString("c");
				}
				else
				{
                    this.TotalTransferRow.Visible = true;
                    this.TotalTransferValueLabel.Text = totalAmount.ToString("c");
                    this.TotalRow.Visible = false;
					this.TotalVatRow.Visible = false;
					this.TotalExVatRow.Visible = false;
				}
			}
        }

		private void AddDateRangeToQueryConditionList(List<Q> queryConditionList, object dateColumnEnum)
		{
			if (this.FromDateCal.Date > DateTime.MinValue)
				queryConditionList.Add(new Q(dateColumnEnum, QueryOperator.GreaterThanOrEqualTo, this.FromDateCal.Date));
			if (this.ToDateCal.Date > DateTime.MinValue)
				queryConditionList.Add(new Q(dateColumnEnum, QueryOperator.LessThan, this.ToDateCal.Date.AddDays(1)));
		}

		private void UpdateAllTransfersForIsFullyApplied()
		{
			TransferSet transferSet = new TransferSet(new Query());

			foreach (Transfer transfer in transferSet)
			{
				if (transfer.IsFullyApplied != true)
				{
					decimal amountRemaining = transfer.AmountRemaining();
					if (Math.Round(amountRemaining, 2) == 0)
					{
						transfer.IsFullyApplied = true;
						transfer.Update();
					}
					else if (Math.Round(amountRemaining, 2) > 0)
					{
						transfer.IsFullyApplied = false;
						transfer.Update();
					}
					if (Math.Round(amountRemaining, 2) < 0)
					{
						amountRemaining = 0;
					}
				}
			}
		}

		private void UpdateAllOldInvoicesPaidByCreditCard()
		{
			Query InvoiceQuery = new Query(new And(new Q(Invoice.Columns.Paid, true),
												   new Q(Invoice.Columns.Type, Invoice.Types.Invoice)));

			InvoiceSet invoiceSet = new InvoiceSet(InvoiceQuery);

			foreach (Invoice invoice in invoiceSet)
			{
				invoice.IsImmediateCreditCardPayment = true;
				invoice.ActionUsrK = invoice.UsrK;
				invoice.VatCode = Invoice.VATCodes.T1;

				foreach (InvoiceItem invoiceItem in invoice.Items)
				{
					if (invoiceItem.Type == InvoiceItem.Types.CharityDonation)
						invoiceItem.VatCode = InvoiceItem.VATCodes.T9;
					else
						invoiceItem.VatCode = InvoiceItem.VATCodes.T1;

					if (invoiceItem.Type == InvoiceItem.Types.Banner || invoiceItem.Type == InvoiceItem.Types.BannerEmail || invoiceItem.Type == InvoiceItem.Types.BannerHotbox
						|| invoiceItem.Type == InvoiceItem.Types.BannerPhoto || invoiceItem.Type == InvoiceItem.Types.BannerSkyscraper || invoiceItem.Type == InvoiceItem.Types.BannerTop)
					{
						try
						{
							Banner banner = new Banner(invoiceItem.KeyData);
							invoiceItem.RevenueStartDate = banner.FirstDay;
							invoiceItem.RevenueEndDate = banner.LastDay;
						}
						catch (Exception)
						{ }
					}
				}

				invoice.UpdateAndSetPaidStatus();

				// If there arent transfers paying for this invoices yet, then create them.
				decimal amountPaid = invoice.AmountPaid;
				if (invoice.Total > amountPaid)
				{
					InvoiceTransferSet invoiceTransferSet = new InvoiceTransferSet(new Query(new Q(InvoiceTransfer.Columns.InvoiceK, invoice.K)));
					if (invoiceTransferSet.Count == 0)
					{
						Transfer transfer = new Transfer();
						transfer.ActionUsrK = invoice.UsrK;
						transfer.Amount = invoice.Total - amountPaid;
						transfer.DateTimeComplete = invoice.TaxDateTime;
						transfer.DateTimeCreated = invoice.TaxDateTime;
						transfer.Method = Transfer.Methods.Card;
						transfer.AddNote("Autogenerated Transfer to create transfer for old system invoice.", "System");
						transfer.PromoterK = invoice.PromoterK;
						transfer.Status = Transfer.StatusEnum.Success;
						transfer.Type = Transfer.TransferTypes.Payment;
						transfer.UsrK = invoice.UsrK;
                        //transfer.SetDSIBankAccount();
						transfer.Update();

						InvoiceTransfer invoiceTransfer = new InvoiceTransfer();
						invoiceTransfer.InvoiceK = invoice.K;
						invoiceTransfer.TransferK = transfer.K;
						invoiceTransfer.Amount = transfer.Amount;

						invoiceTransfer.Update();
						
						invoice.UpdateAndSetPaidStatus();
					}
				}
			}
		}

		public string BuyableObjectLink(CampaignCredit cc)
		{
			string output = cc.BuyableObjectType.ToString() + " #" + cc.BuyableObjectK.ToString();

			if (cc.BuyableObject != null)
			{
				if (cc.BuyableObject is ILinkableAdmin)
					output = ((ILinkableAdmin)cc.BuyableObject).AdminLinkNewWindow();
				else if (cc.BuyableObject is ILinkable)
					output = ((ILinkable)cc.BuyableObject).LinkNewWindow();
			}
			return output;
		}
		#endregion

        private void DisplaySearchCriteria()
        {
            this.StatusDropDownList.Items.Clear();
            this.StatusDropDownList.Items.Add("");

            this.NominalCodeLabel.Visible = false;
            this.NominalCodeTextBox.Visible = false;
            this.uiPromotersAutoComplete.Visible = true;
            this.PromoterLabel.Visible = true;
            this.uiUserAutoComplete.Visible = true;
            this.UserLabel.Visible = true;
            this.SalesUserLabel.Visible = false;
            this.SalesUserDropDownList.Visible = false;
            this.InvoiceItemTypeLabel.Visible = false;
            this.InvoiceItemTypeDropDownList.Visible = false;
            this.InvoiceTypeDropDownList.Visible = false;
            this.InvoiceTypeLabel.Visible = false;

            this.TransferTypeLabel.Visible = false;
            this.TransferTypeDropDownList.Visible = false;
			this.TransferCompanyDropDownList.Visible = false;
            this.TransferMethodLabel.Visible = false;
            this.TransferMethodDropDownList.Visible = false;
            this.BankAccountLabel.Visible = false;
            this.BankAccountDropDownList.Visible = false;

			this.DateTypeLabel.Visible = true;
			this.DateTypeDropDownList.Visible = true;

            this.StatusDropDownList.Visible = true;
			this.StatusLabel.Visible = true;
            this.StatusLabel.Text = "Status";

            this.FromDateLabel.Text = "<nobr>From date</nobr>";
            this.ToDateLabel.Text = "<nobr>To date</nobr>";

			switch (SelectedSearchType)
            {
                case SearchType.InvoiceItem:
                    SetupDateTypeDropDownList(SearchType.InvoiceItem);
                    this.NominalCodeLabel.Visible = true;
                    this.NominalCodeTextBox.Visible = true;
                    this.InvoiceItemTypeLabel.Visible = true;
                    this.InvoiceItemTypeDropDownList.Visible = true;

                    this.StatusDropDownList.Items.Add("Processed");
                    this.StatusDropDownList.Items.Add("Not Processed");

                    this.StatusLabel.Text = "<nobr>Item processed</nobr>";

                    this.SalesUserLabel.Visible = true;
                    this.SalesUserDropDownList.Visible = true;

                    this.uiPromotersAutoComplete.Visible = false;
                    this.PromoterLabel.Visible = false;
                    this.uiUserAutoComplete.Visible = false;
                    this.UserLabel.Visible = false;
                    break;

                case SearchType.Invoice:
				//case SearchType.Credit:
					SetupDateTypeDropDownList(SearchType.Invoice);
                    this.StatusDropDownList.Items.Add("Paid");
                    this.StatusDropDownList.Items.Add("Not Paid");
                    this.SalesUserLabel.Visible = true;
                    this.SalesUserDropDownList.Visible = true;
                    this.InvoiceTypeDropDownList.Visible = true;
                    this.InvoiceTypeLabel.Visible = true;
                    break;

                case SearchType.Transfer:
                    SetupDateTypeDropDownList(SearchType.Transfer);
                    this.StatusDropDownList.Items.AddRange(Transfer.StatusesAsListItemArray());
                    this.TransferTypeLabel.Visible = true;
                    this.TransferTypeDropDownList.Visible = true;
					this.TransferCompanyDropDownList.Visible = true;
                    this.TransferMethodLabel.Visible = true;
                    this.TransferMethodDropDownList.Visible = true;
                    //this.BankAccountLabel.Visible = true;
                    //this.BankAccountDropDownList.Visible = true;
                    break;
                
				case SearchType.InsertionOrder:
					SetupDateTypeDropDownList(SearchType.InsertionOrder);
					this.StatusDropDownList.Items.Clear();
					Utilities.AddEnumValuesToDropDownList(StatusDropDownList, typeof(InsertionOrder.InsertionOrderStatus), true, false);
					this.SalesUserDropDownList.Visible = false;
					break;

				case SearchType.CampaignCredit:
					this.DateTypeLabel.Visible = false;
					this.DateTypeDropDownList.Visible = false;
					this.StatusDropDownList.Visible = false;
					this.StatusLabel.Visible = false;
					break;
                default:
                    break;
            }
        }

		#region Page Event Handlers

		protected void SearchButton_Click(object sender, EventArgs e)
		{
			SearchResultsInvoiceGridView.PageIndex = 0;
			SearchResultsTransferGridView.PageIndex = 0;
			SearchResultsInvoiceItemGridView.PageIndex = 0;
			GetSearchResults();
		}
		protected void ClearButton_Click(object sender, EventArgs e)
		{
			this.uiUserAutoComplete.Value = "";
			this.uiUserAutoComplete.Text = "";
			this.uiPromotersAutoComplete.Value = "";
			this.uiPromotersAutoComplete.Text = "";
			this.SalesUserDropDownList.SelectedIndex = 0; ;
			this.KNumberTextBox.Text = "";
			this.TransferMethodDropDownList.SelectedIndex = 0;
            //this.BankAccountDropDownList.SelectedIndex = 0;
			this.StatusDropDownList.SelectedIndex = 0;
			this.ToDateCal.Date = DateTime.MinValue;
			this.FromDateCal.Date = DateTime.MinValue;
			this.NominalCodeTextBox.Text = "";
			this.DateTypeDropDownList.SelectedIndex = 0;
			this.TransferTypeDropDownList.SelectedIndex = 0;
			this.TransferCompanyDropDownList.SelectedIndex = 0;
            this.InvoiceTypeDropDownList.SelectedIndex = 0;
            this.InvoiceItemTypeDropDownList.SelectedIndex = 0;

			SearchResultsMessageLabel.Visible = false;
		}
		protected void TypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplaySearchCriteria();
		}

		protected void CreateNewInvoiceButton_Click(object sender, EventArgs e)
		{
			Response.Redirect(Invoice.UrlAdminNewInvoice());
		}

		protected void CreateNewTransferButton_Click(object sender, EventArgs e)
		{
			Response.Redirect(Transfer.UrlAdminNewTransfer());
		}

		protected void SubmitSuccessfulTransfersButton_Click(object sender, EventArgs e)
		{
			Response.Redirect(UrlInfo.PageUrl(UrlInfo.PageTypes.Admin, "submitsuccessfultransfers"));
		}

		protected void CreateNewInsertionOrderButton_Click(object sender, EventArgs e)
		{
			Response.Redirect(InsertionOrder.UrlAdminNewInsertionOrder());
		}

        protected void CreateNewCampaignCreditButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(CampaignCredit.UrlAdminNewCampaignCredit());
        }

		protected void ExportToSageButton_Click(object sender, EventArgs e)
		{
			if (SageFromDateCal.Date > DateTime.MinValue && SageToDateCal.Date > DateTime.MinValue && SageFromDateCal.Date <= SageToDateCal.Date)
			{
				string fileName = "accounts-" + SageFromDateCal.Date.ToString("yyyy-MM-dd") + " to " + SageToDateCal.Date.ToString("yyyy-MM-dd")
								+ " " + ExportToSageTypeDropDownList.SelectedItem.Text + " -created_on-" + DateTime.Now.ToString("yyyy-MM-dd");

				string output = "";

				if (this.ExportToSageTypeDropDownList.SelectedValue.Equals(""))
				{
					output = Utilities.ExportToSage(SageFromDateCal.Date, SageToDateCal.Date);
				}
				else
				{
					output = Utilities.ExportToSage(SageFromDateCal.Date, SageToDateCal.Date, (Utilities.ExportToSageType)Convert.ToInt32(this.ExportToSageTypeDropDownList.SelectedValue));
				}

				Response.Clear(); // just in case there's already output

				Response.ContentType = "application/ms-excel";
				Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName + ".csv");

				Response.Write(output);
				Response.Flush();
				// multiple Response.Write statements outputting the CSV data

				Response.End();
				Response.Close();
			}
			else
			{
				this.SageErrorLabel.Visible = true;
				this.SageErrorLabel.Text = "* Must enter valid dates";
			}
		}		
		#endregion

		#region Search Results GridView Event Handlers
		private void SearchResultsGridViewPageIndexChanging(GridView searchGridView, GridViewPageEventArgs e)
        {
            // Cancel the paging operation if the user attempts to navigate
            // to another page while the GridView control is in edit mode. 
			if (searchGridView.EditIndex != -1)
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
				searchGridView.PageIndex = e.NewPageIndex;
                GetSearchResults();
				if (searchGridView.PageIndex > searchGridView.PageCount)
					searchGridView.PageIndex = 1;
                
                // Clear the error message.
                SearchResultsMessageLabel.Text = "";
                SearchResultsMessageLabel.Visible = false;
            }
		}

		protected void SearchResultsInvoiceGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			SearchResultsGridViewPageIndexChanging(this.SearchResultsInvoiceGridView, e);
		}

		protected void SearchResultsInvoiceGridView_DataBound(object sender, EventArgs e)
        {
			// Column 1=CreatedDateTime, 2=PaidDateTime, 3=TaxDateTime
			SearchResultsInvoiceGridView.Columns[1].Visible = false;
			SearchResultsInvoiceGridView.Columns[2].Visible = false;
			SearchResultsInvoiceGridView.Columns[3].Visible = false;

			if (this.DateTypeDropDownList.SelectedValue == Convert.ToInt32(InvoiceDateSearchType.CreatedDate).ToString())
			{
				SearchResultsInvoiceGridView.Columns[1].Visible = true;
			}
			else if (this.DateTypeDropDownList.SelectedValue == Convert.ToInt32(InvoiceDateSearchType.PaidDate).ToString())
			{
				SearchResultsInvoiceGridView.Columns[2].Visible = true;
			}
			else
			{
				SearchResultsInvoiceGridView.Columns[3].Visible = true;
			}
		}


		protected void SearchResultsTransferGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			SearchResultsGridViewPageIndexChanging(this.SearchResultsTransferGridView, e);
		}

		protected void SearchResultsInvoiceItemGridView_DataBound(object sender, EventArgs e)
        {
			// Column 1=RevenueStartDateTime, 2=RevenueEndDateTime
			SearchResultsInvoiceItemGridView.Columns[1].Visible = false;
			SearchResultsInvoiceItemGridView.Columns[2].Visible = false;

			if (this.DateTypeDropDownList.SelectedValue == Convert.ToInt32(InvoiceItemDateSearchType.RevenueStartDate).ToString())
			{
				SearchResultsInvoiceItemGridView.Columns[1].Visible = true;
			}
			else
			{
				SearchResultsInvoiceItemGridView.Columns[2].Visible = true;
			}
		}		

		protected void SearchResultsInvoiceItemGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			SearchResultsGridViewPageIndexChanging(this.SearchResultsInvoiceItemGridView, e);
		}

		protected void SearchResultsInsertionOrderGridView_DataBound(object sender, EventArgs e)
		{

		}

        protected void SearchResultsCampaignCreditGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
            SearchResultsGridViewPageIndexChanging(this.SearchResultsCampaignCreditGridView, e);
		}

		protected void SearchResultsInsertionOrderGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			SearchResultsGridViewPageIndexChanging(this.SearchResultsInsertionOrderGridView, e);
		}
		#endregion
	}
}
