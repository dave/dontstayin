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

namespace Spotted.Controls.Promoters
{
	public partial class AccountsWarning : System.Web.UI.UserControl
	{
		private const string REDIRECT_PAGE = "/popup/overdueaccounts";

		protected void Page_Load(object sender, EventArgs e)
		{
			this.PromoterOutstandingAccountPanel();
		}

		#region Promoter Outstanding Account Panel
		public void PromoterOutstandingAccountPanel()
		{
			if (Usr.Current != null && !Usr.Current.IsAdmin && !Usr.Current.IsSuperAdmin && Usr.Current.IsEnabledPromoter())
			{
				PromoterSet promoters = Usr.Current.Promoters(new ColumnSet(Promoter.Columns.UrlName, Promoter.Columns.Name, Promoter.Columns.K, Promoter.Columns.OverrideApplyTicketFundsToInvoices));

				Query outstandingInvoiceQuery = new Query();
				Q promoterOr = new Q(Invoice.Columns.PromoterK, -1);
				foreach (Promoter promoter in promoters)
				{
					promoterOr = new Or(promoterOr, new Q(Invoice.Columns.PromoterK, promoter.K));
				}
				outstandingInvoiceQuery.QueryCondition = new And(promoterOr,
																 new Q(Invoice.Columns.Type, Invoice.Types.Invoice),
																 new Q(Invoice.Columns.Paid, false),
																 new Q(Invoice.Columns.DueDateTime, QueryOperator.LessThanOrEqualTo, DateTime.Today.AddDays(8).AddMilliseconds(-1)));
				outstandingInvoiceQuery.Columns = new ColumnSet(Invoice.Columns.PromoterK);
				outstandingInvoiceQuery.ExtraSelectElements.Add("MinDueDate", "MIN(DueDateTime)");

				outstandingInvoiceQuery.OrderBy = new OrderBy("MIN([Invoice].[DueDateTime])");
				outstandingInvoiceQuery.GroupBy = new GroupBy("[Invoice].[PromoterK]");

				InvoiceSet invoices = new InvoiceSet(outstandingInvoiceQuery);

				bool isRedirectPage = false;
				PromoterAccountsOutstandingPanel.Visible = invoices.Count > 0;
				this.Visible = invoices.Count > 0;
				if (invoices.Count > 0)
				{
					string tableClass = "dataGrid";
					string headerText = "";
					string messageText = "";

					// Set urgency
					DateTime minDueDate = Utilities.GetStartOfDay((DateTime)invoices[0].ExtraSelectElements["MinDueDate"]);
					DateTime startOfToday = DateTime.Today;
					this.PromoterAccountsOutstandingMessage.Visible = true;
					this.AccountLockoutHelp.Visible = true;

					// If SuperAdmin has set "DisableOverdueRedirect" then it prevents the suspension of the promoter's access to the site, but will still show wanring message with all outstanding / overdue accounts.
					if (minDueDate < startOfToday.AddDays(-7) && !invoices[0].Promoter.DisableOverdueRedirect)
					{
						// Far overdue - redirect
						headerText = "WARNING! Your promoter account invoices are overdue. You must pay now!";
						messageText = "Your access to DontStayIn has been suspended due to invoices that have been overdue for an extended period.<br>You must pay the account balance immediately.";
						
						if (!this.IsPostBack && !Usr.Current.IsAdmin && !Usr.Current.IsSuperAdmin && !this.IsAllowedPage(invoices))
						{
							HttpContext.Current.Response.Redirect(REDIRECT_PAGE);
						}
						else
						{
							// Hide panel if they are redirected to pay immediately
							if (IsOnPromoterPayPage(invoices))
							{
								PromoterAccountsOutstandingPanel.Visible = false;
								this.Visible = false;
								return;
							}
							isRedirectPage = true;
						}
					}
					else if (minDueDate < startOfToday)
					{
						// Overdue
						headerText = "WARNING! Your promoter account invoices are overdue. You must pay now!";
						messageText = "Your promoter account invoices are now overdue.<br>You must pay the account balance immediately to prevent a suspension of your DontStayIn account.";
					}
					else if (minDueDate < startOfToday.AddDays(3))
					{
						// Almost overdue
						headerText = "URGENT! Your promoter account invoices are almost due. Please pay now.";
						messageText = "Your promoter account invoices are almost overdue.";
					}
					else
					{
						// Outstanding
						headerText = "Your promoter account invoices are nearly due. Please pay now.";
						//messageText = "Your promoter account invoices are nearing the due dates. Please pay the account balance.";
						this.PromoterAccountsOutstandingMessage.Visible = false;
						this.AccountLockoutHelp.Visible = false;
					}
					this.PromoterAccountsOutstandingHeader.InnerHtml = headerText;
					this.PromoterAccountsOutstandingMessage.InnerHtml = messageText;
					//this.AccountLockoutHelp.Visible = isRedirectPage;
					this.PromoterAccountsOutstandingTable.Rows.Clear();
					this.PromoterAccountsOutstandingTable.Attributes.Add("class", tableClass);

					#region Header Row
					HtmlTableRow headerRow = new HtmlTableRow();
					headerRow.Attributes.Add("class", "dataGridHeader");
					this.PromoterAccountsOutstandingTable.Rows.Add(headerRow);
					HtmlTableCell[] headerTableCells = new HtmlTableCell[4];
					for (int i = 0; i < headerTableCells.Length; i++)
					{
						headerTableCells[i] = new HtmlTableCell("th");
						headerTableCells[i].Align = "left";
						headerRow.Cells.Add(headerTableCells[i]);
					}
					headerTableCells[0].InnerHtml = "<nobr>Promoter account</nobr>";
					headerTableCells[1].InnerHtml = "Balance";
					headerTableCells[2].InnerHtml = "<nobr>Payment due</nobr>";
					headerTableCells[3].InnerHtml = "<nobr>Pay now</nobr>";
					#endregion

					#region Data Rows
					HtmlTableRow[] dataRows = new HtmlTableRow[invoices.Count];
					for (int i = 0; i < invoices.Count; i++)
					{
						dataRows[i] = new HtmlTableRow();
						if (i % 2 == 0)
							dataRows[i].Attributes.Add("class", "dataGridItem");
						else
							dataRows[i].Attributes.Add("class", "dataGridAltItem");

						this.PromoterAccountsOutstandingTable.Rows.Add(dataRows[i]);
						HtmlTableCell[] dataTableCells = new HtmlTableCell[4];
						for (int j = 0; j < dataTableCells.Length; j++)
						{
							dataTableCells[j] = new HtmlTableCell();
							dataRows[i].Cells.Add(dataTableCells[j]);
						}

						promoters.Reset();
						string[] urlAppParams = new string[]{ "PayOutstanding", "true" };

						foreach (Promoter promoter in promoters)
						{
							if (invoices[i].PromoterK == promoter.K)
							{
								// if isRedirect remove link, cause they are only allowed to go to the payment page
								if(isRedirectPage)
									dataTableCells[0].InnerHtml = "<nobr>" + promoter.Name + "</nobr>";
								else
									dataTableCells[0].InnerHtml = "<nobr><a href=\"" + promoter.Url() + "\">" + promoter.Name + "</a></nobr>";
								dataTableCells[1].InnerHtml = "<nobr><font color=\"#ff0000;\"><b>" + Math.Abs(promoter.GetBalance()).ToString("c") + "</b></font></nobr>";

								if (Utilities.GetStartOfDay((DateTime)invoices[i].ExtraSelectElements["MinDueDate"]) >= DateTime.Today)
									urlAppParams = new string[] { "PayOutstanding", "true"};
								else
									urlAppParams = new string[] { "PayOutstanding", "true", "Overdue", "true" };

								dataTableCells[3].InnerHtml = "<button onclick=\"window.location.href='" + promoter.UrlApp("invoices", urlAppParams) + "';return false;\">Pay now</button>";
								break;
							}
						}
						dataTableCells[1].Align = "right";
						dataTableCells[2].InnerHtml = Utilities.DateToString((DateTime)invoices[i].ExtraSelectElements["MinDueDate"]);
					}

					#endregion
				}
			}
			else
			{
				PromoterAccountsOutstandingPanel.Visible = false;
				this.Visible = false;
			}
		}

		public bool IsAllowedPage(InvoiceSet invoices)
		{
			bool isAllowed = HttpContext.Current.Request.Url.ToString().IndexOf(REDIRECT_PAGE) >= 0;

			if (!isAllowed)
				isAllowed = IsOnPromoterPayPage(invoices);
			return isAllowed;
		}

		private bool IsOnPromoterPayPage(InvoiceSet invoices)
		{
			invoices.Reset();
			foreach (Invoice invoice in invoices)
			{
				if (Utilities.GetStartOfDay((DateTime)invoice.ExtraSelectElements["MinDueDate"]) >= DateTime.Today)
				{
					if (HttpContext.Current.Request.Url.ToString().IndexOf(invoice.Promoter.UrlApp("invoices", "PayOutstanding", "true")) >= 0)
						return true;
				}
				else
				{
					if (HttpContext.Current.Request.Url.ToString().IndexOf(invoice.Promoter.UrlApp("invoices", "PayOutstanding", "true", "Overdue", "true")) >= 0)
						return true;
				}
			}

			return false;
		}
		#endregion
	}
}
