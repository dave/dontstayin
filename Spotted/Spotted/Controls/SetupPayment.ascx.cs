using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Bobs;
using Local;
using System.Text.RegularExpressions;
using Bobs.DataHolders;

namespace Spotted.Controls
{
	public partial class SetupPayment : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Usr.Current != null)
			{
				if (!Page.IsPostBack)
				{
					this.ViewState["DuplicateGuidTransfer"] = Guid.NewGuid();
				}
				else
				{
					this.CheckCompleted();
				}
			}
		}
		
		#region Page_PreRender
		public void Page_PreRender(object o, System.EventArgs e)
		{
			this.BindPromoterItems();
		}
		#endregion

		#region Properties
		#region PromoterK
		public int PromoterK
		{
			get
			{
				if (this.ViewState["PromoterK"] != null)
					return (int)this.ViewState["PromoterK"];
				else
					return 0;
			}
			set
			{
				this.ViewState["PromoterK"] = value;
			}
		}
		#endregion

		#region Transfer
		public Transfer CurrentTransfer
		{
			get
			{
				if (transfer == null && this.ViewState["TransferK"] != null)
					transfer = new Transfer((int)this.ViewState["TransferK"]);
				return transfer;
			}
			set
			{
				transfer = value;
				this.ViewState["TransferK"] = transfer.K;
			}
		}
		private Transfer transfer = null;
		#endregion

		#region Invoices
		public List<InvoiceDataHolder> Invoices
		{
			get
			{
				if (invoices == null)
					invoices = new List<InvoiceDataHolder>();
				return this.invoices;
			}
			set
			{
				this.invoices = value;
			}
		}
		private List<InvoiceDataHolder> invoices;
		#endregion

		#region Total
		/// <summary>
		/// Raw total from invoices (taking into account partial transfers and credits)
		/// </summary>
		decimal InvoiceTotal
		{
			get
			{
				if (!invoiceTotalDone)
				{
					invoiceTotalDone = true;

					invoiceTotal = 0.0m;

					foreach (InvoiceDataHolder idh in Invoices)
						invoiceTotal += idh.AmountDue;
				}
				return invoiceTotal;
			}
		}
		private decimal invoiceTotal = 0.0m;
		private bool invoiceTotalDone = false;
		#endregion

		#region BackgroundColour
		//FED551 light
		//FECA26 dark
		public string BackgroundColour
		{
			get
			{
				return backgroundColour;
			}
			set
			{
				backgroundColour = value;
			}
		}
		private string backgroundColour = "FED551";
		#endregion
		#region AltColour
		public string AltColour
		{
			get
			{
				if (AltColour.Length == 0)
				{
					if (backgroundColour.Equals("FED551"))
						return "FECA26";
					else
						return "FED551";
				}
				else
					return altColour;
			}
			set
			{
				altColour = value;
			}
		}
		private string altColour = "";
		#endregion
		#region ContainerPage
		Spotted.Master.DsiPage ContainerPage
		{
			get
			{
				if (Page is Spotted.Master.DsiPage)
					return (Spotted.Master.DsiPage)Page;
				else
					return null;
			}
		}
		#endregion
		#endregion

		#region Methods
		#region BindPromoterItems()
		void BindPromoterItems()
		{
			if (PromoterK > 0)
			{
				this.PromoterItemListPanel.Visible = true;

				if (Invoices.Count == 1 && Invoices[0].K == 0)
				{
					this.PromoterItemListColumn2HeaderLabel.Text = "";
					this.PromoterItemListColumn3HeaderLabel.Text = "Price";

					foreach (InvoiceItemDataHolder iidh in Invoices[0].InvoiceItemDataHolderList)
					{
						//HtmlTableRow itemsTr = new HtmlTableRow();
						HtmlTableRow invoiceTr = new HtmlTableRow();
						InvoicesBody.Controls.Add(invoiceTr);
						//InvoicesBody.Controls.Add(itemsTr);

						//itemsTr.ID = "PaymentItem" + idh.K;
						//itemsTr.Style["display"] = "none";

						#region Name
						HtmlTableCell invoiceNameTd = new HtmlTableCell();
						invoiceNameTd.Style["background-color"] = "transparent";
						invoiceNameTd.InnerHtml = iidh.Description;
						#endregion

						#region Price
						HtmlTableCell invoicePriceTd = new HtmlTableCell();
						invoicePriceTd.Align = "right";
						invoicePriceTd.Style["background-color"] = "transparent";
						invoicePriceTd.InnerHtml = iidh.Price.ToString("c");
						#endregion

						#region Empty HtmlTableCell
						HtmlTableCell emptyTd = new HtmlTableCell();
						emptyTd.Style["background-color"] = "transparent";
						emptyTd.InnerHtml = "&nbsp;";
						#endregion

						invoiceTr.Cells.Add(invoiceNameTd);
						invoiceTr.Cells.Add(emptyTd);
						invoiceTr.Cells.Add(invoicePriceTd);

						//if (idh.K == 0)

						//    itemsTd.InnerHtml = sb.ToString();
						//itemsTr.Cells.Add(itemsTd);
					}

					HtmlTableRow vatTr = new HtmlTableRow();
					InvoicesBody.Controls.Add(vatTr);
					vatTr.ID = "PaymentVATTotal";

					#region Empty HtmlTableCell
					HtmlTableCell emptyVatTd = new HtmlTableCell();
					emptyVatTd.Style["background-color"] = "transparent";
					emptyVatTd.InnerHtml = "&nbsp;";
					#endregion

					#region VAT Name
					HtmlTableCell vatNameTd = new HtmlTableCell();
					vatNameTd.Style["background-color"] = "transparent";
					vatNameTd.Align = "right";
					vatNameTd.InnerHtml = "VAT";
					#endregion

					#region VAT Total
					HtmlTableCell vatTotalTd = new HtmlTableCell();
					vatTotalTd.Align = "right";
					vatTotalTd.Style["background-color"] = "transparent";
					vatTotalTd.InnerHtml = Invoices[0].Vat.ToString("c");
					#endregion

					vatTr.Cells.Add(emptyVatTd);
					vatTr.Cells.Add(vatNameTd);
					vatTr.Cells.Add(vatTotalTd);
				}
				else
				{
					foreach (InvoiceDataHolder idh in Invoices)
					{
						HtmlTableRow itemsTr = new HtmlTableRow();
						HtmlTableRow invoiceTr = new HtmlTableRow();
						InvoicesBody.Controls.Add(invoiceTr);
						InvoicesBody.Controls.Add(itemsTr);

						itemsTr.ID = "PaymentItem" + idh.K;
						itemsTr.Style["display"] = "none";

						#region Name
						HtmlTableCell invoiceNameTd = new HtmlTableCell();
						invoiceNameTd.Style["background-color"] = "transparent";
						string invoiceHtml = "New invoice";
						if (idh.K > 0 && idh.PromoterK > 0)
                            invoiceHtml = Utilities.LinkNewWindow(idh.UrlReport(), "Invoice #" + idh.K.ToString());
						invoiceNameTd.InnerHtml = "<a href=\"#\" onclick=\"var elem = document.getElementById('" + itemsTr.ClientID + "'); var img = document.getElementById('" + this.ClientID + "_PaymentPlusMinus" + idh.K + "'); img.src = elem.style.display == 'none' ? '/gfx/minus.gif' : '/gfx/plus.gif'; elem.style.display = elem.style.display == 'none' ? '' : 'none'; return false;\"><img id=\"" + this.ClientID + "_PaymentPlusMinus" + idh.K + "\" src=\"/gfx/plus.gif\" alt=\"Show items\" border=\"0\" align=\"absmiddle\" style=\"margin-right:4px;\" /></a>" + invoiceHtml;
						#endregion

						#region Total
						HtmlTableCell invoiceTotalTd = new HtmlTableCell();
						invoiceTotalTd.Align = "right";
						invoiceTotalTd.Style["background-color"] = "transparent";
						invoiceTotalTd.InnerHtml = idh.Total.ToString("c");
						#endregion

						#region Due
						HtmlTableCell invoiceDueTd = new HtmlTableCell();
						invoiceDueTd.Align = "right";
						invoiceDueTd.Style["background-color"] = "transparent";
						invoiceDueTd.InnerHtml = idh.AmountDue.ToString("c");
						#endregion

						invoiceTr.Cells.Add(invoiceNameTd);
						invoiceTr.Cells.Add(invoiceTotalTd);
						invoiceTr.Cells.Add(invoiceDueTd);

						HtmlTableCell itemsTd = new HtmlTableCell();
						itemsTd.Style["background-color"] = "transparent";
						itemsTd.Style["padding"] = "0px";
						itemsTd.Style["padding-left"] = "14px";
						itemsTd.Style["width"] = "376px";
						itemsTd.ColSpan = 3;
						itemsTd.Align = "right";

						#region Items table
						StringBuilder sb = new StringBuilder();
						sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">");
						foreach (InvoiceItemDataHolder iidh in idh.InvoiceItemDataHolderList)
						{
							sb.Append("<tr><td align=\"left\" style=\"background-color:transparent;padding-top:1px;padding-bottom:1px;\"><small>");
							sb.Append(iidh.Description);
							sb.Append("</small></td><td align=\"right\" style=\"background-color:transparent;padding-top:1px;padding-bottom:1px;\"><small><nobr>");
							// Show IncVat for Promoters and other users. As per David Brophy on Oct 24, 2006
							// Show ExVat for all, then add VAT as its own line item after all invoice items. this is for OASIS v1.5, Dec 12, 2006
							sb.Append(iidh.Price.ToString("c"));
							sb.Append("</nobr></small></td></tr>");
						}

						sb.Append("<tr><td align=\"left\" style=\"background-color:transparent;padding-top:1px;padding-bottom:1px;\"><small>VAT</small></td><td align=\"right\" style=\"background-color:transparent;padding-top:1px;padding-bottom:1px;\"><small><nobr>");
						sb.Append(idh.Vat.ToString("c"));
						sb.Append("</nobr></small></td></tr>");

						// Only if the some money has been paid or credited, then we go to DB to get successful transfer applied
						if (idh.AmountDue != idh.Total)
						{
							Query invoiceTransferQuery = new Query();
							invoiceTransferQuery.QueryCondition = new And(new Q(InvoiceTransfer.Columns.InvoiceK, idh.K),
																		  new Q(Transfer.Columns.Status, Transfer.StatusEnum.Success));
							invoiceTransferQuery.TableElement = new Join(InvoiceTransfer.Columns.TransferK, Transfer.Columns.K, QueryJoinType.Inner);
							invoiceTransferQuery.Columns = new ColumnSet(InvoiceTransfer.Columns.Amount, InvoiceTransfer.Columns.TransferK, Transfer.Columns.Type);

							InvoiceTransferSet invoiceTransferSet = new InvoiceTransferSet(invoiceTransferQuery);
							decimal invoiceTransferTotal = 0;
							foreach (InvoiceTransfer invoiceTransfer in invoiceTransferSet)
							{
								decimal amount = invoiceTransfer.Amount;
								if (((Transfer.TransferTypes)invoiceTransfer.ExtraSelectElements["Transfer_Type"]).Equals(Transfer.TransferTypes.Payment))
									amount = -1 * Math.Abs(amount);
								else
									amount = Math.Abs(amount);
								sb.Append("<tr><td align=\"left\" style=\"background-color:transparent;padding-top:1px;padding-bottom:1px;\"><small><nobr>Transfer #");
								sb.Append(invoiceTransfer.TransferK.ToString());
								sb.Append("</nobr></td><td align=\"right\" style=\"background-color:transparent;padding-top:1px;padding-bottom:1px;\"><small><nobr>");
								sb.Append(amount.ToString("c"));
								sb.Append("</nobr></small></td></tr>");

								invoiceTransferTotal += invoiceTransfer.Amount;
							}

							// Test if there is still money unaccounted for, then go to DB for credits applied
							if (invoiceTransferTotal + idh.AmountDue < idh.Total)
							{
								Query invoiceCreditQuery = new Query();
								invoiceCreditQuery.QueryCondition = new Q(InvoiceCredit.Columns.InvoiceK, idh.K);
								invoiceCreditQuery.Columns = new ColumnSet(InvoiceCredit.Columns.Amount, InvoiceCredit.Columns.CreditInvoiceK);

								InvoiceCreditSet invoiceCreditSet = new InvoiceCreditSet(invoiceCreditQuery);

								foreach (InvoiceCredit invoiceCredit in invoiceCreditSet)
								{
									sb.Append("<tr><td align=\"left\" style=\"background-color:transparent;padding-top:1px;padding-bottom:1px;\"><small><nobr>Credit #");
									sb.Append(invoiceCredit.CreditInvoiceK.ToString());
									sb.Append("</nobr></td><td align=\"right\" style=\"background-color:transparent;padding-top:1px;padding-bottom:1px;\"><small><nobr>");
									sb.Append(invoiceCredit.Amount.ToString("c"));
									sb.Append("</nobr></small></td></tr>");
								}
							}
						}

						sb.Append("</table>");
						#endregion

						//if (idh.K == 0)
						itemsTd.InnerHtml = sb.ToString();
						itemsTr.Cells.Add(itemsTd);

					}
				}
				InvoiceTotalLabel.Text = InvoiceTotal.ToString("c");
			}
		}
		#endregion

		#region Reset()
		public void Reset()
		{
			this.Invoices.Clear();
			this.PayOptionRadioButtonSetupBankTransferPayment.Checked = false;
			this.PayOptionRadioButtonSetupChequePayment.Checked = false;
			this.ViewState["DuplicateGuidTransfer"] = Guid.NewGuid();
		}
		#endregion
		/// <summary>
		/// Checks if there are any transfers with the DuplicateGUID from the current page ViewState. This prevents transfers being issued multiple times from user error.
		/// </summary>
		/// <returns></returns>
		private int GetDuplicateGuidTransferK()
		{
			TransferSet ts = new TransferSet(new Query(new Q(Transfer.Columns.DuplicateGuid, (Guid)this.ViewState["DuplicateGuidTransfer"])));
			if (ts.Count > 0)
				return ts[0].K;
			else
				return 0;
		}

		private void RollbackTransfer()
		{
			try
			{
				if (CurrentTransfer != null && CurrentTransfer.K > 0)
				{
					InvoiceTransferSet invoiceTransferSet = new InvoiceTransferSet(new Query(new Q(InvoiceTransfer.Columns.TransferK, CurrentTransfer.K)));
					for (int i = invoiceTransferSet.Count - 1; i >= 0; i--)
					{
						invoiceTransferSet[i].Delete();
					}
					CurrentTransfer.Delete();
				}
			}
			catch (Exception ex)
			{
				// rollback failed.  email admins
				Utilities.AdminEmailAlert("<p>Exception occurred during rollback of transfer in SetupPayment control</p><p>Transfer K=" + CurrentTransfer.K.ToString() + "</p>",
										  "Exception occurred during rollback of transfer (K=" + CurrentTransfer.K.ToString() + ") in SetupPayment control", ex, CurrentTransfer);
			}
		}

		private void CheckCompleted()
		{
			if (CurrentTransfer != null && CurrentTransfer.K > 0)
			{
				CurrentTransfer = CurrentTransfer;
				this.ReferenceNumberValueLabel.Text = CurrentTransfer.K.ToString();
				this.ReferenceNumberRow.Visible = true;
				PayOptionRadioButtonSetupChequePayment.Visible = false;
				PayOptionRadioButtonSetupBankTransferPayment.Visible = false;
				PaymentOptionsHeaderTable.Visible = false;
				TransferClickConfirmLabel.Visible = false;
				ChequeClickConfirmLabel.Visible = false;
				if (CurrentTransfer.Method == Transfer.Methods.BankTransfer)
					BankTransferPaymentInfoRow.Style.Clear();
				else if (CurrentTransfer.Method == Transfer.Methods.Cheque)
					ChequePaymentInfoRow.Style.Clear();
			}
		}
		#endregion

		#region Page Event Handlers
		public void ConfirmButton_Click(object o, System.EventArgs e)
		{
			if (Usr.Current != null)
			{
				// Check for duplicate invoices
				int transferK = GetDuplicateGuidTransferK();

				// Create new transfer
				if (transferK == 0)
				{
					CurrentTransfer = new Transfer();
					try
					{
						CurrentTransfer.Status = Transfer.StatusEnum.Pending;

						decimal totalAmountDue = 0;
						foreach (InvoiceDataHolder idh in Invoices)
						{
							totalAmountDue += idh.AmountDue;
						}
						CurrentTransfer.Amount = Math.Round(totalAmountDue, 2);
						CurrentTransfer.UsrK = Usr.Current.K;
						CurrentTransfer.ActionUsrK = Usr.Current.K;
						CurrentTransfer.DuplicateGuid = (Guid)this.ViewState["DuplicateGuidTransfer"];
						if (this.PayOptionRadioButtonSetupBankTransferPayment.Checked)
							CurrentTransfer.Method = Transfer.Methods.BankTransfer;
						else if (this.PayOptionRadioButtonSetupChequePayment.Checked)
							CurrentTransfer.Method = Transfer.Methods.Cheque;
						CurrentTransfer.PromoterK = PromoterK;
						CurrentTransfer.Type = Transfer.TransferTypes.Payment;
						CurrentTransfer.DateTimeCreated = DateTime.Now;

						CurrentTransfer.Update();
						CurrentTransfer = CurrentTransfer;
						
						foreach (InvoiceDataHolder idh in Invoices)
						{
							InvoiceTransfer invoiceTransfer = new InvoiceTransfer();
							invoiceTransfer.Amount = idh.AmountDue;
							invoiceTransfer.InvoiceK = idh.K;
							invoiceTransfer.TransferK = CurrentTransfer.K;

							invoiceTransfer.Update();

							totalAmountDue -= idh.AmountDue;
						}

						if (Math.Round(totalAmountDue, 2) != 0)
						{
							throw new Exception("Transfer amount not correctly applied. Difference = " + Math.Round(totalAmountDue, 2).ToString("c"));
						}
						Utilities.EmailTransfer(CurrentTransfer, true, false);
						CheckCompleted();
					}
					catch (Exception ex)
					{
						// Rollback transfer and invoiceTransfers
						this.RollbackTransfer();
						Utilities.AdminEmailAlert("<p>Exception occurred during setup of transfer in SetupPayment control</p><p>Transfer K=" + CurrentTransfer.K.ToString() + "</p>",
													"Exception occurred during setup of transfer (K=" + CurrentTransfer.K.ToString() + ") in SetupPayment control", ex, CurrentTransfer);
					}
				}
				else if (CurrentTransfer == null)
				{
					CurrentTransfer = new Transfer(transferK);
					CheckCompleted();
				}
			}
			if (ContainerPage != null)
				ContainerPage.AnchorSkip(this.ClientID + "_SetupPaymentAnchor");
		}
		#endregion

		#region ViewState
		protected override object SaveViewState()
		{
			this.ViewState["Invoices"] = Invoices;
			return base.SaveViewState();
		}

		protected override void LoadViewState(object savedState)
		{
			base.LoadViewState(savedState);
			if (this.ViewState["Invoices"] != null) Invoices = (List<InvoiceDataHolder>)this.ViewState["Invoices"];
		}
		#endregion
	}
}
