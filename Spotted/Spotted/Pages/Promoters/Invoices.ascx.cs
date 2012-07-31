using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Bobs;
using Bobs.DataHolders;

namespace Spotted.Pages.Promoters
{
	public partial class Invoices : PromoterUserControl
	{
		#region Page_Init
		protected override void Page_Init(object sender, System.EventArgs e)
		{
			base.Page_Init(sender, e);
		}
		#endregion

		protected Controls.Payment Payment;

		//InvoiceSet SummaryInvoiceSet;
		//TransferSet SummaryTransferSet;
		List<InvoiceDataHolder> invoiceDataHolderList = new List<InvoiceDataHolder>();
		int RecordsPerPage { get { return this.GetAllOutstanding ? 1000 : 20; } } 
		int pageNumber = 1;

		bool GetAllOutstanding = false;

		protected void Page_Load(object sender, EventArgs e)
		{
            Response.CacheControl = "no-cache";
            Response.AddHeader("Pragma", "no-cache");
            Response.Expires = -1;

			ContainerPage.SslPage = true;

			

			//if (!Usr.Current.IsPromoter && !Usr.Current.IsAdmin)
			//{
			//    throw new Exception("You must be a promoter to view this page");
			//}
			if (CurrentPromoter != null)
			{
				//if (!CurrentPromoter.IsUsrAllowedAccess(Usr.Current))
				//    throw new Exception(Vars.CANT_VIEW_DETAILS);
				this.Payment.CurrentPromoter = CurrentPromoter;
			}

			GetAllOutstanding = false;

			try
			{
				if (ViewState["Page"] == null)
					pageNumber = 1;
				else
					pageNumber = Convert.ToInt32(ViewState["Page"]);
			}
			catch (Exception)
			{
				pageNumber = 1;
			}

			if (!this.IsPostBack)
			{
				if (CurrentPromoter != null)
				{
					this.ViewStatementHyperLink.NavigateUrl = CurrentPromoter.UrlStatementReport(DateTime.Now);
					//this.ViewStatementButton.Attributes.Add("onclick", "javascript::window.open('" + CurrentPromoter.UrlStatementReport(DateTime.Now) + "','Statement')");

					CurrentPromoter.ApplyAvailableMoneyToUnpaidInvoices();
				}

				BalanceValueLabel.Style["text-align"] = "right";
				CreditLimitValueLabel.Style["text-align"] = "right";
				FundsAvailableValueLabel.Style["text-align"] = "right";

				SetupYearDropDownList();
				SetupFilterDropDownList();

				if (!ContainerPage.Url["PayOutstanding"].IsNull && Convert.ToBoolean(ContainerPage.Url["PayOutstanding"].Value) == true)
				{
					this.GetAllOutstandingInvoices();
					if (!ContainerPage.Url["Overdue"].IsNull && Convert.ToBoolean(ContainerPage.Url["Overdue"].Value) == true)
					{
						this.SetupTransferButton.Visible = false;
					}						
				}
				else
					GetSummaryResults(false);

                LoadAdminSection();
			}

			GetBalanceAndDisplay();

			//Query qInv = new Query();
			//qInv.QueryCondition = new Q(Invoice.Columns.PromoterK, CurrentPromoter.K);
			//InvoiceSet ins = new InvoiceSet(qInv);

			//Query qTr = new Query();
			//qTr.TableElement = new Join.Series(Transfer.Columns.K, InvoiceTransfer.Columns.TransferK, InvoiceTransfer.Columns.InvoiceK, Invoice.Columns.K);
			//qTr.QueryCondition = new Q(Invoice.Columns.PromoterK, CurrentPromoter.K);
			//qTr.Columns = new ColumnSet(Transfer.Columns.K, Transfer.Columns.Notes, InvoiceTransfer.Columns.InvoiceK);
			//TransferSet ts = new TransferSet(qTr);

			//foreach (Invoice i in ins)
			//{
			//    Response.Write("Invoice = " + i.K + "<br>");

			//    ts.KillAll();
			//    string columnName = Tables.GetTableName(TablesEnum.InvoiceTransfer) + "_" + Tables.GetColumnName(InvoiceTransfer.Columns.InvoiceK);
			//    ts.DataSet.Tables[0].DefaultView.RowFilter = columnName + " = "+ i.K.ToString();
			//    foreach (Transfer t in ts)
			//    {
			//        Response.Write("Transfer = " + t.K + "<br>");

			//    }
			//}

			//ContainerPage.Url["pay"].
		}

		#region Properties
		public List<InvoiceDataHolder> InvoiceDataHolderList
		{
			get
			{
				if (ViewState["InvoiceDataHolderList"] != null)
				{
					invoiceDataHolderList = (List<InvoiceDataHolder>)ViewState["InvoiceDataHolderList"];
				}
				else
				{
					invoiceDataHolderList = new List<InvoiceDataHolder>();
					ViewState["InvoiceDataHolderList"] = invoiceDataHolderList;
				}
				
				return invoiceDataHolderList;
			}
			set
			{
				ViewState["InvoiceDataHolderList"] = value;
				invoiceDataHolderList = value;					
			}
		}
		#endregion

		#region Setup DropDownLists
		public void SetupYearDropDownList()
		{
			this.YearDropDownList.Items.Clear();
			this.YearDropDownList.Items.Add(new ListItem(""));
			for (int i = DateTime.Now.Year; i > 2002; i--)
			{
				ListItem li = new ListItem(i.ToString(), i.ToString());
				this.YearDropDownList.Items.Add(li);
			}
		}

		public void SetupFilterDropDownList()
		{
			this.FilterDropDownList.Items.Clear();

			ListItem[] listItems = new ListItem[2];
			listItems[0] = new ListItem("ALL", "");
			listItems[1] = new ListItem(Invoice.Statuses.Outstanding.ToString() + " Invoices");

			this.FilterDropDownList.Items.AddRange(listItems);
		}
		#endregion

		#region Methods
        private void LoadAdminSection()
        {
            this.AdminPanel.Visible = false;
            if (Usr.Current != null && Usr.Current.IsAdmin)
            {
                this.AdminPanel.Visible = true;
                this.AdminInvoiceLinkLabel.Text = Utilities.Link(UrlInfo.PageUrl(UrlInfo.PageTypes.Admin, "adminmainaccounting", "SearchType", Convert.ToInt32(Admin.AdminMainAccounting.SearchType.Invoice).ToString(),
                                                                                "PromoterK", this.CurrentPromoter.K.ToString()),
                                                                                "[Admin link to invoices]");
                this.AdminTransferLinkLabel.Text = Utilities.Link(UrlInfo.PageUrl(UrlInfo.PageTypes.Admin, "adminmainaccounting", "SearchType", Convert.ToInt32(Admin.AdminMainAccounting.SearchType.Transfer).ToString(),
                                                                                "PromoterK", this.CurrentPromoter.K.ToString()),
                                                                                "[Admin link to transfers]");
            }
        }
		public string FormatStatus(Invoice invoice)
		{
			string output = "";

			if (invoice.Type.Equals(Invoice.Types.Invoice))
			{
				if (invoice.Status.Equals(Invoice.Statuses.Outstanding))
					output = "<b>" + invoice.Status.ToString() + ",<br />due " + invoice.DueDateTime.ToString("dd/MM/yy") + "</b>";
				else if (invoice.Status.Equals(Invoice.Statuses.Overdue))
					output = "<font color=\"#ff0000;\"><b>" + invoice.Status.ToString() + ",<br />due " + invoice.DueDateTime.ToString("dd/MM/yy") + "</b></font>";
				else
					output = invoice.Status.ToString();
			}
			else
			{
				if (invoice.Status.Equals(Invoice.Statuses.Paid))
					output = "Refunded";
				else
					output = "Credited";
			}
			
			return output;
		}

		private void GetBalanceAndDisplay()
		{
			decimal balance = CurrentPromoter.GetBalance();

			if (balance < 0)
			{
				this.BalanceValueLabel.ForeColor = System.Drawing.Color.Red;
				this.OutstandingBalanceLabel.Text = "You have an outstanding balance. Please pay your balance now.";
			}
			else
			{
				this.BalanceValueLabel.ForeColor = System.Drawing.Color.Black;
				if (balance > 0)
					this.OutstandingBalanceLabel.Text = "(Credit balance)";
				else
					this.OutstandingBalanceLabel.Text = "";
			}

			BalanceValueLabel.Text = Math.Abs(balance).ToString("c");

			if (Math.Round(CurrentPromoter.CreditLimit,2) > 0)
			{
				this.FundsAvailableTR.Visible = true;
				this.CreditLimitTR.Visible = true;

				this.CreditLimitValueLabel.Text = CurrentPromoter.CreditLimit.ToString("c");

				decimal fundsAvailable = Math.Round(CurrentPromoter.CreditLimit + balance, 2);
				if (fundsAvailable < 0)
					fundsAvailable = 0;

				this.FundsAvailableValueLabel.Text = fundsAvailable.ToString("c");
			}
			else
			{
                this.FundsAvailableTR.Visible = false;
                this.CreditLimitTR.Visible = false;
			}

            this.TicketFundsTR.Visible = CurrentPromoter.OverrideApplyTicketFundsToInvoices;
            if (CurrentPromoter.OverrideApplyTicketFundsToInvoices)
            {
                this.TicketFundsValueLabel.Text = CurrentPromoter.GetAvailableTicketFunds().ToString("c");
            }
		}

		private void GetSummaryResults()
		{
			GetSummaryResults(true);			
		}

		private void GetSummaryResults(bool forSpecifiedDates)
		{
			DateTime startDate = new DateTime(1900, 1, 1);
			DateTime endDate = DateTime.MaxValue;

			string headerLabelText = "";

			if(this.FilterDropDownList.SelectedValue.Equals(Invoice.Statuses.Outstanding.ToString() + " Invoices"))
				headerLabelText += "Outstanding invoices";
			else
				headerLabelText += "Invoices, credits, payments, and refunds";

			if (forSpecifiedDates == true)
			{
				if (this.YearDropDownList.SelectedValue.Length > 0)
				{
					if (this.MonthDropDownList.SelectedValue.Length > 0)
					{
						startDate = new DateTime(Convert.ToInt32(YearDropDownList.SelectedValue), Convert.ToInt32(MonthDropDownList.SelectedValue), 1);
						endDate = new DateTime(Convert.ToInt32(YearDropDownList.SelectedValue), Convert.ToInt32(MonthDropDownList.SelectedValue), 1).AddMonths(1).AddMilliseconds(-1);
					}
					// no month is chosen, only year
					else
					{
						startDate = new DateTime(Convert.ToInt32(YearDropDownList.SelectedValue), 1, 1);
						endDate = new DateTime(Convert.ToInt32(YearDropDownList.SelectedValue) + 1, 1, 1).AddMilliseconds(-1);
					}

					headerLabelText += " : from " + startDate.ToShortDateString() + " to " + endDate.ToShortDateString();
				}
				// If no year has been selected, then we cant evaluate the month.  So set month selection to No Month.
				else
					this.MonthDropDownList.SelectedIndex = 0;
			}
			
			//Query InvoiceSummaryQuery = new Query();

			//if (this.GetAllOutstanding == true)
			//{
			//    // Show all records on one page, no pagination
			//    InvoiceSummaryQuery.Paging.RecordsPerPage = 0;
			//    InvoiceSummaryQuery.Paging.RequestedPage = 1;
			//}
			//else
			//{
			//    // Paginate results
			//    InvoiceSummaryQuery.Paging.RecordsPerPage = RecordsPerPage;
			//    InvoiceSummaryQuery.Paging.RequestedPage = pageNumber;
			//}
			//// Descending date order: Lastest first, oldest last
			//// Replacing CreatedDateTime with TaxDateTime, as per Gee's request for OASIS v1.5
			//InvoiceSummaryQuery.OrderBy = new OrderBy(Invoice.Columns.TaxDateTime, OrderBy.OrderDirection.Descending);
		
			//Q InvoiceQ = new Q(Invoice.Columns.PromoterK, CurrentPromoter.K);
			//Q DateQ = new And(new Q(Invoice.Columns.TaxDateTime, QueryOperator.GreaterThanOrEqualTo, startDate),
			//                  new Q(Invoice.Columns.TaxDateTime, QueryOperator.LessThanOrEqualTo, endDate));

			//// Outstanding only applies to Invoices
			//if (this.FilterDropDownList.SelectedValue.Equals(Invoice.Statuses.Outstanding.ToString() + " Invoices"))
			//    InvoiceSummaryQuery.QueryCondition = new And(InvoiceQ, DateQ, new Q(Invoice.Columns.Paid, false), new Q(Invoice.Columns.Type, Invoice.Types.Invoice));//, new Q(Invoice.Columns.Type, Invoice.Types.Invoice));
			//else
			//    InvoiceSummaryQuery.QueryCondition = new And(InvoiceQ, DateQ);

			//SummaryInvoiceSet = new InvoiceSet(InvoiceSummaryQuery);
			//// Hide Pay Now button if there are no invoices to pay
			//if (SummaryInvoiceSet.Count == 0)
			//{
			//    this.PayNowButton.Visible = false;
			//    this.MakeOtherPaymentLabel.Visible = false;
			//}
			//InvoiceDataHolderList.Clear();
			//foreach (Invoice inv in SummaryInvoiceSet)
			//{
			//    InvoiceDataHolderList.Add(new InvoiceDataHolder(inv));
			//}

			//Query TransferSummaryQuery = new Query();
			//TransferSummaryQuery.OrderBy = new OrderBy(Transfer.Columns.DateTimeCreated, OrderBy.OrderDirection.Descending);

			//TransferSummaryQuery.TableElement = new Join.Series(Transfer.Columns.K, InvoiceTransfer.Columns.TransferK, InvoiceTransfer.Columns.InvoiceK, Invoice.Columns.K);
			//TransferSummaryQuery.QueryCondition = InvoiceSummaryQuery.QueryCondition;
			//TransferSummaryQuery.Columns = new ColumnSet(Transfer.Columns.K, Transfer.Columns.Method, Transfer.Columns.Amount, Transfer.Columns.Status, Transfer.Columns.DateTimeComplete, Transfer.Columns.PromoterK, InvoiceTransfer.Columns.InvoiceK, InvoiceTransfer.Columns.Amount);
			//SummaryTransferSet = new TransferSet(TransferSummaryQuery);

			//this.SummaryRepeater.DataSource = InvoiceDataHolderList;

			//this.SummaryRepeater.DataBind();

			//if (this.GetAllOutstanding == true)
			//{
			//    this.PaginationPanel.Visible = false;
			//}
			//else
			//{
			//    this.PaginationPanel.Visible = true;
			//    this.PrevPageLinkButton.Enabled = SummaryInvoiceSet.Paging.ShowPrevPageLink;
			//    this.NextPageLinkButton.Enabled = SummaryInvoiceSet.Paging.ShowNextPageLink;
			//}
			
			this.SummaryHeaderLabel.Text = headerLabelText;
			//BindDataToInvoiceCreditTransferTable(GetPromoterAccountItemList(startDate, endDate));

			ViewState["PromoterAccountItemCounter"] = "0";
			this.PromoterAccountItemRepeater.DataSource = GetPromoterAccountItemList(startDate, endDate);
			this.PromoterAccountItemRepeater.DataBind();

			if (this.PromoterAccountItemRepeater.DataSource != null && this.PromoterAccountItemRepeater.DataSource is List<PromoterAccountItem> && ((List<PromoterAccountItem>)this.PromoterAccountItemRepeater.DataSource).Count == 0)
			{
				this.PayNowButton.Visible = false;
				this.SetupTransferButton.Visible = false;
			}
			//this.PayOutstandingButton.Visible = ((List<PromoterAccountItem>)this.PromoterAccountItemRepeater.DataSource).Count > 0;
		}

		private List<PromoterAccountItem> GetPromoterAccountItemList()
		{
			return GetPromoterAccountItemList(new DateTime(1900,1,1), DateTime.MaxValue, null);
		}

		private List<PromoterAccountItem> GetPromoterAccountItemList(IBob referenceBob)
		{
			return GetPromoterAccountItemList(new DateTime(1900, 1, 1), DateTime.MaxValue, referenceBob);
		}

		private List<PromoterAccountItem> GetPromoterAccountItemList(DateTime startDate, DateTime endDate)
		{
			return GetPromoterAccountItemList(startDate, endDate, null);
		}

		private List<PromoterAccountItem> GetPromoterAccountItemList(DateTime startDate, DateTime endDate, IBob referenceBob)
		{
			int startingRecord = this.RecordsPerPage * (this.pageNumber -1);
			List<PromoterAccountItem> promoterAccountItems = new List<PromoterAccountItem>();
			InvoiceSet invoices;
			TransferSet transfers;

			Query invoiceSummaryQuery = new Query();
			Query transferSummaryQuery = new Query();

			// Descending date order: Lastest first, oldest last
			// Replacing CreatedDateTime with TaxDateTime, as per Gee's request for OASIS v1.5
			// Order By CreatedDateTime instead of TaxDateTime, as per Dave's request 7/2/07
			invoiceSummaryQuery.OrderBy = new OrderBy(Invoice.Columns.CreatedDateTime, OrderBy.OrderDirection.Descending);
			invoiceSummaryQuery.Paging.RecordsPerPage = 0;
			invoiceSummaryQuery.Paging.RequestedPage = 1;

			transferSummaryQuery.OrderBy = new OrderBy("IsNull([Transfer].[DateTimeComplete], [Transfer].[DateTimeCreated]) DESC");
			transferSummaryQuery.Paging.RecordsPerPage = 0;
			transferSummaryQuery.Paging.RequestedPage = 1;

			if (referenceBob != null)
			{
				if (referenceBob is Invoice)
				{
					Invoice invoice = (Invoice)referenceBob;
					if (invoice.Type.Equals(Invoice.Types.Invoice))
					{
						invoiceSummaryQuery.QueryCondition = new Q(InvoiceCredit.Columns.InvoiceK, invoice.K);
						invoiceSummaryQuery.TableElement = new Join(Invoice.Columns.K, InvoiceCredit.Columns.CreditInvoiceK);						
					}
					else
					{
						invoiceSummaryQuery.QueryCondition = new Q(InvoiceCredit.Columns.CreditInvoiceK, invoice.K);
						invoiceSummaryQuery.TableElement = new Join(Invoice.Columns.K, InvoiceCredit.Columns.InvoiceK);
					}

					// Replacing TaxDateTime with CreatedDateTime, as per Dave's request 7/2/07
					invoiceSummaryQuery.Columns = new ColumnSet(Invoice.Columns.K, Invoice.Columns.Paid, Invoice.Columns.PromoterK, Invoice.Columns.CreatedDateTime, Invoice.Columns.DueDateTime,
								Invoice.Columns.Total, Invoice.Columns.Type, InvoiceCredit.Columns.Amount);

					invoiceSummaryQuery.Paging.RecordsPerPage = 0;
					invoiceSummaryQuery.Paging.RequestedPage = 1;
					
					transferSummaryQuery.QueryCondition = new And(new Q(InvoiceTransfer.Columns.InvoiceK, invoice.K),
                                                                  new Q(Transfer.Columns.Amount, QueryOperator.NotEqualTo, 0),
																  new Or(new Q(Transfer.Columns.Status, Transfer.StatusEnum.Success),
																		 new And(new Q(Transfer.Columns.Type, Transfer.TransferTypes.Refund),
																				 new Q(Transfer.Columns.Status, Transfer.StatusEnum.Pending))));
					transferSummaryQuery.TableElement = new Join(Transfer.Columns.K, InvoiceTransfer.Columns.TransferK);
					transferSummaryQuery.Columns = new ColumnSet(Transfer.Columns.K, Transfer.Columns.Status, Transfer.Columns.PromoterK, Transfer.Columns.DateTimeComplete, Transfer.Columns.DateTimeCreated, Transfer.Columns.Method,
																 Transfer.Columns.TransferRefundedK, Transfer.Columns.Amount, Transfer.Columns.Type, InvoiceTransfer.Columns.Amount);
					transferSummaryQuery.Paging.RecordsPerPage = 0;
					transferSummaryQuery.Paging.RequestedPage = 1;
				}
				else if (referenceBob is Transfer)
				{
					Transfer transfer = (Transfer)referenceBob;
					if (transfer.Type.Equals(Transfer.TransferTypes.Payment))
					{
						transferSummaryQuery.QueryCondition = new Q(Transfer.Columns.TransferRefundedK, transfer.K);
					}
					else
					{
						transferSummaryQuery.QueryCondition = new Q(Transfer.Columns.K, transfer.TransferRefundedK);
					}
					
					transferSummaryQuery.Paging.RecordsPerPage = 0;
					transferSummaryQuery.Paging.RequestedPage = 1;

					invoiceSummaryQuery.QueryCondition = new Q(InvoiceTransfer.Columns.TransferK, transfer.K);
					invoiceSummaryQuery.TableElement = new Join(Invoice.Columns.K, InvoiceTransfer.Columns.InvoiceK);
					// Replacing TaxDateTime with CreatedDateTime, as per Dave's request 7/2/07
					invoiceSummaryQuery.Columns = new ColumnSet(Invoice.Columns.K, Invoice.Columns.Paid, Invoice.Columns.PromoterK, Invoice.Columns.CreatedDateTime, Invoice.Columns.DueDateTime,
								Invoice.Columns.Total, Invoice.Columns.Type, InvoiceTransfer.Columns.Amount);
					invoiceSummaryQuery.Paging.RecordsPerPage = 0;
					invoiceSummaryQuery.Paging.RequestedPage = 1;
				}
			}
			else
			{
				Q InvoiceQ = new Q(Invoice.Columns.PromoterK, CurrentPromoter.K);
				// Replacing TaxDateTime with CreatedDateTime, as per Dave's request 7/2/07
				Q DateQ = new And(new Q(Invoice.Columns.CreatedDateTime, QueryOperator.GreaterThanOrEqualTo, startDate),
								  new Q(Invoice.Columns.CreatedDateTime, QueryOperator.LessThanOrEqualTo, endDate));

				// Outstanding only applies to Invoices
				if (this.FilterDropDownList.SelectedValue.Equals(Invoice.Statuses.Outstanding.ToString() + " Invoices"))
				{
					invoiceSummaryQuery.QueryCondition = new And(InvoiceQ, DateQ, new Q(Invoice.Columns.Paid, false), new Q(Invoice.Columns.Type, Invoice.Types.Invoice));//, new Q(Invoice.Columns.Type, Invoice.Types.Invoice));
					transferSummaryQuery.QueryCondition = new Q(Transfer.Columns.K, -1);
				}
				else
				{
					invoiceSummaryQuery.QueryCondition = new And(InvoiceQ, DateQ);
					transferSummaryQuery.QueryCondition = new And(new Q(Transfer.Columns.PromoterK, CurrentPromoter.K),
                                                                  new Q(Transfer.Columns.Amount, QueryOperator.NotEqualTo, 0),
																  new Or(new And(new Q(Transfer.Columns.Status, Transfer.StatusEnum.Success),
                                                                                 new Q(Transfer.Columns.DateTimeComplete, QueryOperator.GreaterThanOrEqualTo, startDate),
																                 new Q(Transfer.Columns.DateTimeComplete, QueryOperator.LessThanOrEqualTo, endDate)),
																		 new And(new Q(Transfer.Columns.Type, Transfer.TransferTypes.Refund),
                                                                                 new Q(Transfer.Columns.DateTimeCreated, QueryOperator.GreaterThanOrEqualTo, startDate),
																                 new Q(Transfer.Columns.DateTimeCreated, QueryOperator.LessThanOrEqualTo, endDate),
																				 new Or(new Q(Transfer.Columns.Status, Transfer.StatusEnum.Pending),
                                                                                        new Q(Transfer.Columns.Status, Transfer.StatusEnum.Success)))));
				}
			}

			invoiceSummaryQuery.FillMaxRecords = this.pageNumber * this.RecordsPerPage + 1;
			transferSummaryQuery.FillMaxRecords = this.pageNumber * this.RecordsPerPage + 1;

			invoices = new InvoiceSet(invoiceSummaryQuery);
			transfers = new TransferSet(transferSummaryQuery);

			if (referenceBob == null)
			{
				if (this.GetAllOutstanding)
				{
					this.PaginationPanel.Visible = false;
				}
				else
				{
					this.PaginationPanel.Visible = true;

					this.PrevPageLinkButton.Enabled = this.pageNumber != 1;
					this.NextPageLinkButton.Enabled = invoices.Count + transfers.Count > this.pageNumber * this.RecordsPerPage;
				}
			}
			
			// Merge into one Bob List
			int invoiceCounter = 0;
			int transferCounter = 0;

			while (startingRecord > invoices.Count + transfers.Count)
			{
				startingRecord -= this.RecordsPerPage;
			}

			int endingRecord = startingRecord + this.RecordsPerPage;

			while ((invoices.Count > invoiceCounter || transfers.Count > transferCounter) && invoiceCounter + transferCounter < endingRecord)
			{
				if (invoices.Count > invoiceCounter && transfers.Count > transferCounter)
				{
					// Replacing TaxDateTime with CreatedDateTime, as per Dave's request 7/2/07
					if (invoices[invoiceCounter].CreatedDateTime > (transfers[transferCounter].DateTimeComplete > transfers[transferCounter].DateTimeCreated ? transfers[transferCounter].DateTimeComplete : transfers[transferCounter].DateTimeCreated))
					{
						if (invoiceCounter + transferCounter >= startingRecord)
						{
							if (referenceBob != null)
							{
								FixFiguresForSubItem(invoices[invoiceCounter], referenceBob);
							}
							promoterAccountItems.Add(new PromoterAccountItem(invoices[invoiceCounter]));
						}
						invoiceCounter++;
					}
					else
					{
						if (invoiceCounter + transferCounter >= startingRecord)
						{
							if (referenceBob != null)
							{
								FixFiguresForSubItem(transfers[transferCounter], referenceBob);
							}
							promoterAccountItems.Add(new PromoterAccountItem(transfers[transferCounter]));
						}
						transferCounter++;
					}
				}
				else if (invoices.Count > invoiceCounter)
				{
					if (invoiceCounter + transferCounter >= startingRecord)
					{
						if (referenceBob != null)
						{
							FixFiguresForSubItem(invoices[invoiceCounter], referenceBob);
						}
						promoterAccountItems.Add(new PromoterAccountItem(invoices[invoiceCounter]));
					}
					invoiceCounter++;
				}
				else
				{
					if (invoiceCounter + transferCounter >= startingRecord)
					{
						if (referenceBob != null)
						{
							FixFiguresForSubItem(transfers[transferCounter], referenceBob);
						}
						promoterAccountItems.Add(new PromoterAccountItem(transfers[transferCounter]));
					}
					transferCounter++;
				}
			}

			return promoterAccountItems;
		}

		private void FixFiguresForSubItem(IBob bob, IBob referenceBob)
		{
			if(bob is Invoice)
			{
				if(referenceBob is Invoice)
				{
					((Invoice)bob).Total = Convert.ToDecimal(bob.ExtraSelectElements["InvoiceCredit_Amount"]);
					//if(((Invoice)reference).Total < 0)

				}
				else if (referenceBob is Transfer)
				{
					((Invoice)bob).Total = Convert.ToDecimal(bob.ExtraSelectElements["InvoiceTransfer_Amount"]);
				}
			}
			else if (bob is Transfer)
			{
				if (referenceBob is Invoice)
				{
					((Transfer)bob).Amount = Convert.ToDecimal(bob.ExtraSelectElements["InvoiceTransfer_Amount"]);
				}
				else if (referenceBob is Transfer)
				{
					//((Transfer)bob).Amount = Convert.ToDecimal(bob.ExtraSelectElements["InvoiceTransfer_Amount"]);
				}
			}
		}

		private void GetAllOutstandingInvoices()
		{
			SetupAllOutstandingInvoices();
			GetSummaryResults(false);
		}
		private void SetupAllOutstandingInvoices()
		{
			GetAllOutstanding = true;

			// Hide header panel
			this.HeaderPanel.Visible = false;
			this.FilterDropDownList.Visible = false;
			this.FilterLabel.Visible = false;
			this.PayOutstandingButton.Visible = false;
			this.PayNowButton.Visible = true;
			this.SetupTransferButton.Visible = true;
			//this.MakeOtherPaymentLabel.Visible = true;

			// Requery for all outstanding invoices and add checkboxes.
			this.MonthDropDownList.SelectedIndex = 0;
			this.YearDropDownList.SelectedIndex = 0;

			SetupFilterDropDownList();
			this.FilterDropDownList.Text = "Outstanding Invoices";

			this.pageNumber = 1;
			ViewState["Page"] = this.pageNumber;
		}
		#endregion

		#region Page Event Handlers
		protected void ViewSummaryButton_Click(object sender, EventArgs e)
		{
			if (this.MonthDropDownList.SelectedValue.Length > 0 && this.YearDropDownList.SelectedValue.Length == 0)
			{
				this.ViewSummaryCustomValidator.IsValid = false;
			}
			else
			{
				this.pageNumber = 1;
				ViewState["Page"] = this.pageNumber;
				GetSummaryResults();
			}
		}

		protected void PayOutstandingButton_Click(object sender, EventArgs e)
		{
			GetAllOutstandingInvoices();
		}

		protected void ViewSummaryOptionsRadionButtonList_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetupFilterDropDownList();
		}

		protected void PayNowButton_Click(object sender, EventArgs e)
		{
			SetupAllOutstandingInvoices();

			Payment.Reset();

			List<int> invoiceKList = new List<int>();

			// Loop through Repeater dataset, get all checked, and pass those K's to the payment screen
			foreach (RepeaterItem rpi in this.PromoterAccountItemRepeater.Items)
			{
				CheckBox outstandingCheckBox = (CheckBox)rpi.FindControl("OutstandingCheckBox");
				if (outstandingCheckBox.Checked == true)
				{
					Label invoiceKLabel = (Label)rpi.FindControl("InvoiceKLabel");
					invoiceKList.Add(Convert.ToInt32(invoiceKLabel.Text));
				}
			}
			
			foreach (int invoiceK in invoiceKList)
			{
				Invoice invoice = new Invoice(invoiceK);

				InvoiceCreditSet invoiceCreditSet = new InvoiceCreditSet(new Query(new Q(InvoiceCredit.Columns.InvoiceK, invoice.K)));
				InvoiceTransferSet invoiceTransferSet = new InvoiceTransferSet(new Query(new Q(InvoiceTransfer.Columns.InvoiceK, invoice.K)));
				var total = invoice.Total;
				var vat = invoice.Vat;
				var price = invoice.Price;

				foreach (InvoiceCredit invoiceCredit in invoiceCreditSet)
				{
					Invoice credit = new Invoice(invoiceCredit.CreditInvoiceK);
					//price += credit.Price;
					//vat += credit.Vat;
					total += credit.Total;
				}
				foreach (InvoiceTransfer invoiceTransfer in invoiceTransferSet)
				{
					Transfer transfer = new Transfer(invoiceTransfer.TransferK);
					if (transfer.Status.Equals(Transfer.StatusEnum.Success))
					{
						total -= transfer.Amount;
					}
				}
				InvoiceDataHolder idh = new InvoiceDataHolder(invoice);
				//idh.K = 0;
				this.Payment.Invoices.Add(idh);
				
			}
			this.Payment.PromoterK = CurrentPromoter.K;
			this.SummaryPanel.Visible = false;
			this.PaymentPanel.Visible = true;

			this.Payment.Initialize();
		}

		protected void SetupTransferButton_Click(object sender, EventArgs e)
		{
			SetupAllOutstandingInvoices();

			SetupPayment.Reset();

			List<int> invoiceKList = new List<int>();

			// Loop through Repeater dataset, get all checked, and pass those K's to the payment screen
			foreach (RepeaterItem rpi in this.PromoterAccountItemRepeater.Items)
			{
				CheckBox outstandingCheckBox = (CheckBox)rpi.FindControl("OutstandingCheckBox");
				if (outstandingCheckBox.Checked == true)
				{
					Label invoiceKLabel = (Label)rpi.FindControl("InvoiceKLabel");
					invoiceKList.Add(Convert.ToInt32(invoiceKLabel.Text));
				}
			}

			foreach (int invoiceK in invoiceKList)
			{
				Invoice invoice = new Invoice(invoiceK);

				InvoiceCreditSet invoiceCreditSet = new InvoiceCreditSet(new Query(new Q(InvoiceCredit.Columns.InvoiceK, invoice.K)));
				InvoiceTransferSet invoiceTransferSet = new InvoiceTransferSet(new Query(new Q(InvoiceTransfer.Columns.InvoiceK, invoice.K)));
				var total = invoice.Total;
				var vat = invoice.Vat;
				var price = invoice.Price;

				foreach (InvoiceCredit invoiceCredit in invoiceCreditSet)
				{
					Invoice credit = new Invoice(invoiceCredit.CreditInvoiceK);
					//price += credit.Price;
					//vat += credit.Vat;
					total += credit.Total;
				}
				foreach (InvoiceTransfer invoiceTransfer in invoiceTransferSet)
				{
					Transfer transfer = new Transfer(invoiceTransfer.TransferK);
					if (transfer.Status.Equals(Transfer.StatusEnum.Success))
					{
						total -= transfer.Amount;
					}
				}
				InvoiceDataHolder idh = new InvoiceDataHolder(invoice);
				//idh.K = 0;
				this.SetupPayment.Invoices.Add(idh);

			}
			this.SetupPayment.PromoterK = CurrentPromoter.K;
			this.SummaryPanel.Visible = false;
			this.SetupTransferPanel.Visible = true;

	//		this.SetupPayment.Initialize();
		}

		protected void FilterDropDownList_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.pageNumber = 1;
			ViewState["Page"] = this.pageNumber;
			GetSummaryResults();
		}

		protected void NextPageLinkButton_Click(object sender, EventArgs e)
		{
			ViewState["Page"] = ++this.pageNumber;
			GetSummaryResults();
		}

		protected void PrevPageLinkButton_Click(object sender, EventArgs e)
		{
			ViewState["Page"] = --this.pageNumber;
			GetSummaryResults();
		}

		#endregion

		#region PromoterAccountItemRepeater Event Handlers
		protected void PromoterAccountItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType.Equals(ListItemType.Header) && (Usr.Current.IsAdmin || Usr.Current.IsSuperAdmin))
			{
				((HtmlTableCell)e.Item.FindControl("AdminEditTH")).Visible = true;
			}
			// Execute the following logic for Items and Alternating Items.
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				if (Usr.Current.IsAdmin || Usr.Current.IsSuperAdmin)
				{
					((HtmlTableCell)e.Item.FindControl("AdminEditTD")).Visible = true;
				}

				CheckBox outstandingCheckBox = (CheckBox)e.Item.FindControl("OutstandingCheckBox");

				if (GetAllOutstanding == false)
				{
					outstandingCheckBox.Visible = false;
				}
				else
				{
					outstandingCheckBox.Visible = true;
					outstandingCheckBox.Checked = true;
				}

				PromoterAccountItem pai = (PromoterAccountItem)e.Item.DataItem;

				ViewState["PromoterAccountItemCounter"] = ((int)(Convert.ToInt32(ViewState["PromoterAccountItemCounter"]) + 1)).ToString();
				if (Convert.ToInt32(ViewState["PromoterAccountItemCounter"]) % 2 == 0)
				{
					((HtmlTableRow)e.Item.FindControl("PromoterAccountItemRow")).Attributes["class"] = "dataGridAltItem";
				}

				List<PromoterAccountItem> promoterAccountItems = new List<PromoterAccountItem>();
				if (pai.OriginalType == PromoterAccountItem.Type.Invoice || pai.OriginalType == PromoterAccountItem.Type.Credit)
					promoterAccountItems = this.GetPromoterAccountItemList(new Invoice(pai.K));
				else
					promoterAccountItems = this.GetPromoterAccountItemList(new Transfer(pai.K));
				//HtmlAnchor toggleLink = (HtmlAnchor)e.Item.FindControl("ToggleLink");
				//toggleLink.NamingContainer.AppRelativeTemplat
				//    .Attributes.Add("onclick", "InvoicesTogglePlusMinus(" + pai.OriginalType.ToString() + pai.K.ToString() + ");return false;");

				string subRowIDs = "";

				if (promoterAccountItems.Count > 0)
				{
					Repeater promoterAccountSubItemRepeater = (Repeater)e.Item.FindControl("PromoterAccountSubItemRepeater");
					promoterAccountSubItemRepeater.DataSource = promoterAccountItems;
					promoterAccountSubItemRepeater.DataBind();

					foreach (RepeaterItem rpi in promoterAccountSubItemRepeater.Items)
					{
						subRowIDs += rpi.FindControl("PromoterAccountSubItemRow").ClientID + ",";
					}

					subRowIDs = subRowIDs.Substring(0, subRowIDs.Length - 1);
				}
				else
				{
					if(e.Item.FindControl("NoSubItemRow") != null)
						subRowIDs = e.Item.FindControl("NoSubItemRow").ClientID;
					if (Convert.ToInt32(ViewState["PromoterAccountItemCounter"]) % 2 == 0)
					{
						((HtmlTableRow)e.Item.FindControl("NoSubItemRow")).Attributes["class"] = "dataGridAltItem";
					}
				}

				
				HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("PromoterAccountItemRow");
				Literal literalControl = (Literal)tr.Cells[0].FindControl("PromoterAccountItemLink");
				literalControl.Text = "<a href=\"#\" onclick=\"InvoicesTogglePlusMinus('" + subRowIDs + "', '" + pai.OriginalType.ToString() + pai.K.ToString() + "');return false;\"><img id='showHideImage" 
									+ pai.OriginalType.ToString() + pai.K.ToString() + "' src=\"/gfx/plus.gif\" width=\"9\" height=\"9\" alt=\"Expand / Collapse\" border=\"0\" align=\"absmiddle\" style=\"margin-right:4px;\" />" 
									+ Utilities.CamelCaseToString(pai.OriginalType.ToString()) + " #" + pai.K.ToString() + "</a>";
			}
		}
		#endregion

		#region PromoterAccountSubItemRepeater Event Handlers
		protected void PromoterAccountSubItemRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			// Execute the following logic for Items and Alternating Items.
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				if (Usr.Current.IsAdmin || Usr.Current.IsSuperAdmin)
				{
					((HtmlTableCell)e.Item.FindControl("AdminEditTD")).Visible = true;
				}

				if (Convert.ToInt32(ViewState["PromoterAccountItemCounter"]) % 2 == 0)
				{
					((HtmlTableRow)e.Item.FindControl("PromoterAccountSubItemRow")).Attributes["class"] = "dataGridAltItem";
				}

			}
		}
		#endregion

		#region Payment Control Methods
		public void Pay_Cancel(object o, System.EventArgs e)
		{
			this.SummaryPanel.Visible = true;
			PaymentPanel.Visible = false;
		}
		public void PaymentReceived(object o, Controls.Payment.PaymentDoneEventArgs e)
		{
			PaymentPanel.Visible = false;
			PaidMessagePanel.Visible = true;
			ContainerPage.SslPage = true;

			GetBalanceAndDisplay();

			// Redo DsiPage.PromoterOutstandingAccountPanel() and DsiPage.NavAdmin.GeneratePromoterAccountsTable()
			this.ContainerPage.PromoterAccountsWarningControl.PromoterOutstandingAccountPanel();
			//this.ContainerPage.Menu.Admin.GeneratePromoterAccountsTable();
		}

		#endregion

		public class PromoterAccountItem
		{
			#region Enums
			public enum Type
			{
				Invoice = 1,
				Credit = 2,
				Payment = 3,
				Refund = 4,
                TicketSales = 5,
                TicketSalesRelease = 6
			}
			#endregion

			#region Variables
			private int k = 0;
			private Type originalType = Type.Invoice;
			private DateTime date = DateTime.MinValue;
			private string total = "";
			private string outstanding = "";
			private string status = "";
			private string viewLink = "";
			private string editLink = "";
			#endregion

			#region Properties
			/// <summary>
			/// The primary key
			/// </summary>
			public int K
			{
				get { return this.k; }
				set { this.k = value; }
			}
			/// <summary>
			/// 
			/// </summary>
			public Type OriginalType
			{
				get { return this.originalType; }
				set { this.originalType = value; }
			}
			/// <summary>
			/// 
			/// </summary>
			public DateTime Date
			{
				get { return this.date; }
				set { this.date = value; }
			}
			/// <summary>
			/// 
			/// </summary>
			public string Total
			{
				get { return this.total; }
				set { this.total = value; }
			}
			/// <summary>
			/// 
			/// </summary>
			public string Outstanding
			{
				get { return this.outstanding; }
				set { this.outstanding = value; }
			}
			/// <summary>
			/// 
			/// </summary>
			public string Status
			{
				get { return this.status; }
				set { this.status = value; }
			}
			/// <summary>
			/// 
			/// </summary>
			public string ViewLink
			{
				get { return this.viewLink; }
				set { this.viewLink = value; }
			}
			/// <summary>
			/// 
			/// </summary>
			public string EditLink
			{
				get { return this.editLink; }
				set { this.editLink = value; }
			}
			#endregion

			#region Constructors
			public PromoterAccountItem(Invoice invoice)
			{
				this.K = invoice.K;
				if (invoice.Type.Equals(Invoice.Types.Invoice))
				{
					this.OriginalType = Type.Invoice;
					this.Outstanding = invoice.AmountDue.ToString("c");
				}
				else
				{
					this.OriginalType = Type.Credit;
					this.Outstanding = "&nbsp;";
				}
				// Replacing TaxDateTime with CreatedDateTime, as per Dave's request 7/2/07
				this.Date = invoice.CreatedDateTime;
				this.Total = Math.Abs(invoice.Total).ToString("c");
				this.Status = FormatStatus(invoice);
				this.ViewLink = Utilities.LinkNewWindow(invoice.UrlReport(), "View");
				this.EditLink = "<small>" + Utilities.LinkNewWindow(invoice.UrlAdmin(), "[Edit]") + "</small>";
			}

			public PromoterAccountItem(Transfer transfer)
			{
				this.K = transfer.K;
                this.Outstanding = transfer.AmountRemaining().ToString("c");
				if (transfer.Type.Equals(Transfer.TransferTypes.Payment))
				{
					this.OriginalType = Type.Payment;
                    if (transfer.Method == Transfer.Methods.TicketSales)
                        this.OriginalType = Type.TicketSales;
				}
				else
				{
					this.OriginalType = Type.Refund;
                    if (transfer.TransferRefunded != null && transfer.TransferRefunded.Method == Transfer.Methods.TicketSales)
                        this.OriginalType = Type.TicketSalesRelease;
                    // We dont show outstanding money for refunds that having been successfully processed
                    if(transfer.Status == Transfer.StatusEnum.Success)
                        this.Outstanding = (0).ToString("c");
				}				
                
                this.Date = transfer.DateTimeComplete > DateTime.MinValue ? transfer.DateTimeComplete : transfer.DateTimeCreated;
				this.Total = Math.Abs(transfer.Amount).ToString("c");
				this.Status = transfer.Status.ToString();
				this.ViewLink = Utilities.LinkNewWindow(transfer.UrlReport(), "View");
                if(this.OriginalType == Type.TicketSales && transfer.TicketPromoterEvent != null)
                    this.ViewLink = Utilities.LinkNewWindow(transfer.TicketPromoterEvent.UrlReport(), "View");
				this.EditLink = "<small>" + Utilities.LinkNewWindow(transfer.UrlAdmin(), "[Edit]") + "</small>";
			}

			#endregion

			#region Methods
			public string FormatStatus(Invoice invoice)
			{
				string output = "";

				if (invoice.Type.Equals(Invoice.Types.Invoice))
				{
					if (invoice.Status.Equals(Invoice.Statuses.Outstanding))
						output = "<b>" + invoice.Status.ToString() + ",<br />due " + invoice.DueDateTime.ToString("dd/MM/yy") + "</b>";
					else if (invoice.Status.Equals(Invoice.Statuses.Overdue))
						output = "<font color=\"#ff0000;\"><b>" + invoice.Status.ToString() + ",<br />due " + invoice.DueDateTime.ToString("dd/MM/yy") + "</b></font>";
					else
						output = invoice.Status.ToString();
				}
				else
				{
					if (invoice.Status.Equals(Invoice.Statuses.Paid))
						output = "Refunded";
					else
						output = "Credited";
				}

				return output;
			}
			#endregion

		}
	}

	
}
