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
using Bobs.DataHolders;
using Common.Clocks;
using Common;

namespace Spotted.Pages.Promoters
{
	public partial class CampaignCredits : PromoterUserControl
	{
		protected RadioButton CustomRadioButton = new RadioButton();
		protected TextBox CustomCreditsTextBox = new TextBox();
		protected Label CustomPriceLabel = new Label();
		protected Label CustomDiscountLabel = new Label();
		//protected Label CustomSavingLabel = new Label();
		protected TextBox CustomTotalTextBox = new TextBox();

		private Guid DuplicateGuid
		{
			get
			{
				if (ViewState["DuplicateGuid"] == null)
					return Guid.Empty;
				return (Guid)ViewState["DuplicateGuid"];
			}
			set
			{
				ViewState["DuplicateGuid"] = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
            Response.CacheControl = "no-cache";
            Response.AddHeader("Pragma", "no-cache");
            Response.Expires = -1;

			ContainerPage.SetPageTitle("Buy Campaign Credits");
			ContainerPage.SslPage = true; // anticipate the payment control. doing this only when payment control becomes visible causes ViewState to be lost after redirection

			if (!IsPostBack)
			{
				DuplicateGuid = Guid.NewGuid();

				CreditsPanel.Visible = true;
				PaymentPanel.Visible = false;
				SuccessPanel.Visible = false;

				AdminPriceEditP.Visible = Usr.Current.IsAdmin;
			}			
		}

		protected void Page_PreRender(object sender, EventArgs e)
		{
			if (CreditsPanel.Visible)
			{
				CreateCreditsTable();
			}
		}

		#region CampaignCreditK
		int CampaignCreditK
		{
			get
			{
				if (ViewState["CampaignCreditK"] != null)
					return (int)ViewState["CampaignCreditK"];
				else
					return 0;
			}
			set
			{
				ViewState["CampaignCreditK"] = value;
			}
		}
		#endregion
		#region CurrentCampaignCredit
		public CampaignCredit CurrentCampaignCredit
		{
			get
			{
				if (currentCampaignCredit == null && CampaignCreditK > 0)
				{
					currentCampaignCredit = new CampaignCredit(CampaignCreditK);
				}
				return currentCampaignCredit;
			}
			set
			{
				currentCampaignCredit = value;
				if (value != null)
					CampaignCreditK = currentCampaignCredit.K;
				else
					CampaignCreditK = 0;
			}
		}
		CampaignCredit currentCampaignCredit;
		#endregion

		private void CreateCreditsTable()
		{
			Table t = CreditsTable;

			t.CssClass = "dataGrid";
			t.CellPadding = 5;
			t.CellSpacing = 0;
			t.BorderWidth = 0;

			TableHeaderRow thr = new TableHeaderRow() { CssClass = "dataGridHeader" };
			thr.Cells.Add(new TableCell() { Text = "", HorizontalAlign = HorizontalAlign.Right });
			thr.Cells.Add(new TableCell() { Text = "", HorizontalAlign = HorizontalAlign.Right });
			thr.Cells.Add(new TableCell() { Text = "Credits", HorizontalAlign = HorizontalAlign.Right });
			thr.Cells.Add(new TableCell() { Text = "Price", HorizontalAlign = HorizontalAlign.Right });
			//thr.Cells.Add(new TableCell() { Text = "Discount", HorizontalAlign = HorizontalAlign.Right });
			//thr.Cells.Add(new TableCell() { Text = "Saving", HorizontalAlign = HorizontalAlign.Right });
			thr.Cells.Add(new TableCell() { Text = "Total", HorizontalAlign = HorizontalAlign.Right });

			t.Rows.Add(thr);

			bool radioButtonChecked = false;
			int[] packages = { 200, 500, 1000, 2000 };
			string[] packageNames = { "Package&nbsp;A", "Package&nbsp;B", "Package&nbsp;C", "Package&nbsp;D" };
			for (int i = 0; i < packages.Length; i++)
			{
				TableRow tr = new TableRow();
				tr.CssClass = (i % 2 == 0) ? "dataGridAltItem" : "dataGridItem";

				RadioButton rb = new RadioButton();
				rb.GroupName = "CampaignCreditsGroup";
				double discountCredits = packages[i];
				rb.Attributes["onclick"] = this.SelectedCredits.ClientID + ".value = '" + discountCredits.ToString() + "';";
				rb.Checked = discountCredits.ToString() == SelectedCredits.Value; // in the event of back button being hit from payment section
				if (rb.Checked)
					radioButtonChecked = true;

				TableCell tc = new TableCell();
				tc.Controls.Add(rb);
				tr.Cells.Add(tc);

				tr.Cells.Add(new TableCell() { Text = packageNames[i] });
				tr.Cells.Add(new TableCell() { Text = discountCredits.ToString("#,##0"), HorizontalAlign = HorizontalAlign.Right });
				double discountForThisPromoter = CampaignCredit.GetDiscountForCredits(packages[i], CurrentPromoter);
				tr.Cells.Add(new TableCell() { Text = ("<small><strike>" + (CurrentPromoter.CostPerCampaignCredit * packages[i]).ToString("c") + "</strike></small>") + "&nbsp;" + (discountForThisPromoter == 0 ? "n/a" : (discountForThisPromoter.ToString("P0").Replace(" ", string.Empty) + "&nbsp;off")), HorizontalAlign = HorizontalAlign.Right });
				//tr.Cells.Add(new TableCell() { Text = (discountForThisPromoter == 0) ? "n/a" : (discountForThisPromoter.ToString("P0").Replace(" ", string.Empty) + " off"), HorizontalAlign = HorizontalAlign.Right });
				//tr.Cells.Add(new TableCell() { Text = "<small>" + (CampaignCredit.CostPerCredit * packages[i] - CampaignCredit.CalculateTotalCostForCredits(packages[i], CurrentPromoter)).ToString("c") + "</small>", HorizontalAlign = HorizontalAlign.Right });
				tr.Cells.Add(new TableCell() { Text = "<b>" + (CampaignCredit.CalculateTotalCostForCredits(packages[i], CurrentPromoter)).ToString("c") + "</b>", HorizontalAlign = HorizontalAlign.Right });
				t.Rows.Add(tr);
			}
			TableRow customTableRow = new TableRow();
			customTableRow.CssClass = (packages.Length % 2 == 0) ? "dataGridAltItem" : "dataGridItem";

			TableCell[] tableCells = new TableCell[6];
			for (int i = 0; i < tableCells.Length; i++ )
			{
				tableCells[i] = new TableCell();
				customTableRow.Cells.Add(tableCells[i]);
			}
			
			//double discountCredits = CampaignCredit.DiscountCredits[i];
			//rb.Attributes["onclick"] = this.SelectedCredits.ClientID + ".value = '" + discountCredits.ToString() + "';";


		//            <asp:RadioButton ID="CustomRadioButton" runat="server" GroupName="CampaignCreditsGroup" />
		//<asp:TextBox ID="CustomCreditsTextBox" runat="server" Width="46" style="text-align:right;" MaxLength="6" ></asp:TextBox>
		//<asp:Label ID="CustomPriceLabel" runat="server" style="text-align:right;"></asp:Label>
		//<asp:Label ID="CustomDiscountLabel" runat="server" style="text-align:right;" ></asp:Label>
		//<asp:Label ID="CustomSavingLabel" runat="server" style="text-align:right;" ></asp:Label>
		//<asp:TextBox ID="CustomTotalTextBox" runat="server" Width="70" style="text-align:right;" MaxLength="10" ></asp:TextBox>

			CustomRadioButton.GroupName = "CampaignCreditsGroup";
			int customCredits = 0;
			if (!radioButtonChecked && int.TryParse(SelectedCredits.Value, out customCredits))
			{
				CustomRadioButton.Checked = true;
				CustomCreditsTextBox.Text = customCredits.ToString();
			}

			CustomCreditsTextBox.Width = 42;
			CustomCreditsTextBox.Style["text-align"] = "right";
			CustomCreditsTextBox.MaxLength = 5;

			CustomPriceLabel.Text = "<small><strike>£0.00</strike></small> ";
			CustomPriceLabel.Style["text-align"] = "right";
			
			CustomDiscountLabel.Text = "0% off";
			CustomDiscountLabel.Style["text-align"] = "right";

			//CustomSavingLabel.Text = "<small>0</small>";
			//CustomSavingLabel.Style["text-align"] = "right";

			CustomTotalTextBox.Width = 70;
			CustomTotalTextBox.Style["text-align"] = "right";
			CustomTotalTextBox.MaxLength = 10;

			tableCells[0].Style["text-align"] = "right";
			tableCells[2].Style["text-align"] = "right";
			tableCells[3].Style["text-align"] = "right";
			//tableCells[2].Width = 70;
			tableCells[4].Style["text-align"] = "right";
			//tableCells[4].Style["text-align"] = "right";
			//tableCells[4].Width = 80;
			//tableCells[5].Style["text-align"] = "right";
			tableCells[5].CssClass = "dataGridPlainCell";
			tableCells[5].Style["background-color"] = "transparent";


			tableCells[0].Controls.Add(CustomRadioButton);
			tableCells[1].Text = "Custom<br>package";
			tableCells[2].Controls.Add(CustomCreditsTextBox);
			tableCells[3].Controls.Add(CustomPriceLabel);
			tableCells[3].Controls.Add(CustomDiscountLabel);
			//tableCells[3].Controls.Add(CustomSavingLabel);
			tableCells[4].Controls.Add(CustomTotalTextBox);
			tableCells[5].Text = "<small>(enter a custom quantity or budget here)</small>";


			CustomCreditsTextBox.Attributes["onkeyup"] = "CustomCampaignCreditsEnter();";
			CustomTotalTextBox.Attributes["onkeyup"] = "CustomCampaignCreditsMoneyEnter();";
			//CustomCreditsTextBox.Attributes["onselected"] = "CalculateCustomCampaignCredits();";
			
							//@"alert(<%= CustomRadioButton.ClientID %>); document.getElementById(<%= CustomRadioButton.ClientID %>).checked = true; ";
															//+ tableCells[2].ClientID + ".value = " + customCreditsTextBox.ClientID + ".value * SingleCreditPrice; "
															//+ tableCells[3].ClientID + ".value = GetDiscount(" + customCreditsTextBox.ClientID + ".value); "
															//+ customTotalTextBox.ClientID + ".value = " + customCreditsTextBox.ClientID + ".value * SingleCreditPrice * (1.0 - " + tableCells[3].ClientID + ".value); "
															//+ tableCells[4].ClientID + ".value = " + tableCells[2].ClientID + ".value - " + customTotalTextBox.ClientID + ".value;";
			CustomRadioButton.Checked = !radioButtonChecked && SelectedCredits.Value.Length > 0 && SelectedCredits.Value == CustomCreditsTextBox.Text; // in the event of back button being hit from payment section

			
			//customTableRow.Cells.Add(tableCells[1]);
			//customTableRow.Cells.Add(tableCells[5]);
			t.Rows.Add(customTableRow);
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

		//#region DiscountMoneysString
		//protected string DiscountMoneysString
		//{
		//    get
		//    {
		//        return string.Join(", ", CampaignCredit.DiscountMoneys.ConvertAll(a => a.ToString()));
		//    }
		//}
		//#endregion

		#region DiscountLevelsString
		protected string DiscountLevelsString
		{
			get
			{
				return string.Join(", ", new List<double>(CampaignCredit.DiscountLevels).ConvertAll(a => (a * 100.0).ToString("0.00")).ToArray());
			}
		}
		#endregion

		#region CustomValidators
		protected void EnsureCampaignCreditsSelected(object sender, ServerValidateEventArgs args)
		{
			int credits;
			//double discount;
			args.IsValid = int.TryParse(SelectedCredits.Value, out credits);
		}

		protected void CustomCampaignCreditsIsValid(object sender, ServerValidateEventArgs args)
		{
			int credits;
			if (this.CustomRadioButton.Checked)
			{
				args.IsValid = int.TryParse(this.CustomCreditsTextBox.Text, System.Globalization.NumberStyles.Integer | System.Globalization.NumberStyles.AllowThousands, System.Globalization.CultureInfo.CurrentCulture, out credits);
			}
			else
				args.IsValid = true;
		}
		
		#endregion
		protected void BuyButton_Click(object sender, EventArgs e)
		{
			if (Page.IsValid)
			{
				LoadPaymentControl();
			}
		}

		private void LoadPaymentControl()
		{
			int credits = int.Parse(SelectedCredits.Value);

			Payment.Reset();
			if (CurrentCampaignCredit == null)
			{
				CampaignCredit cc = new CampaignCredit()
				{
					Credits = credits,
					Enabled = false,
					ActionDateTime = Time.Now,
					PromoterK = CurrentPromoter.K,
					Description = credits.ToString() + " credits",
					DisplayOrder = 0,
					BuyableObjectType = Model.Entities.ObjectType.Invoice
				};
				cc.SetUsrAndActionUsr(Usr.Current);
				cc.UpdateWithRecalculateBalance();
				CurrentCampaignCredit = cc;
			}
			if (CurrentCampaignCredit.Credits != credits)
			{
				CurrentCampaignCredit.Credits = credits;
				CurrentCampaignCredit.UpdateWithRecalculateBalance();
			}

			InvoiceDataHolder idh = new InvoiceDataHolder()
			{
				CreatedDateTime = Time.Now,
				PromoterK = CurrentPromoter.K,
                VatCode = Invoice.VATCodes.T1,
                ActionUsrK = Usr.Current.K,
                Type = Invoice.Types.Invoice,
                UsrK = Usr.Current.K
			};

			InvoiceItemDataHolder iidh = new InvoiceItemDataHolder()
			{
				Description = credits + " credits",
				ShortDescription = credits + " credits",
				BuyableObjectK = CurrentCampaignCredit.K,
				BuyableObjectType = Model.Entities.ObjectType.CampaignCredit,
                VatCode = InvoiceItem.VATCodes.T1,
                Type = InvoiceItem.Types.CampaignCredits,
                RevenueStartDate = Time.Now,
                RevenueEndDate = Time.Now
			};

			iidh.PriceBeforeDiscount = (credits * CurrentPromoter.CostPerCampaignCredit);
			if (CurrentCampaignCredit.IsPriceFixed)
				iidh.Discount = CurrentCampaignCredit.FixedDiscount;
			else
				iidh.Discount = CampaignCredit.GetDiscountForCredits(credits, CurrentPromoter);
			
			idh.InvoiceItemDataHolderList.Add(iidh);
            
			idh.DuplicateGuid = DuplicateGuid;
			Payment.Invoices.Add(idh);

			Payment.PromoterK = CurrentPromoter.K;

			Payment.Initialize();

			CreditsPanel.Visible = false;
			PaymentPanel.Visible = true;
			SuccessPanel.Visible = false;
		}

		protected void BackToCreditOptionsButton_Click(object sender, EventArgs e)
		{
			Payment.Reset();
			CreditsPanel.Visible = true;
			PaymentPanel.Visible = false;
			SuccessPanel.Visible = false;
		}

		#region PaymentReceived()
		protected void PaymentReceived(object o, Controls.Payment.PaymentDoneEventArgs e)
		{
			ShowSuccessPanel();
		}

		private void ShowSuccessPanel()
		{
			CreditsPanel.Visible = false;
			PaymentPanel.Visible = false;
			SuccessPanel.Visible = true;
			AdminPriceEditP.Visible = false;
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
						CurrentCampaignCredit.FixPriceExVatCreditsAndUpdate(null);
					else
					{
						decimal fixedPrice = Utilities.ConvertMoneyStringToDecimal(this.FixPriceTextBox.Text.Replace("%", "").Trim());
						CurrentCampaignCredit.FixPriceExVatCreditsAndUpdate(fixedPrice);
					}

					if (CurrentCampaignCredit.IsPriceFixed)
						FixPriceTextBox.Text = CurrentCampaignCredit.FixedDiscount.ToString("P2");
					else
						FixPriceTextBox.Text = "";
					LoadPaymentControl();
				}
				catch (Exception ex)
				{
					SpottedException.TryToSaveExceptionAndChildExceptions(ex, HttpContext.Current, Usr.Current, Visit.HasCurrent ? Visit.Current : null, "", "", "", 0, null);
				}
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
						CurrentCampaignCredit.FixPriceIncVatCreditsAndUpdate(null);
					else
					{
						var fixedPrice = Utilities.ConvertMoneyStringToDecimal(this.FixPriceTextBox.Text.Replace("%", "").Trim());
						CurrentCampaignCredit.FixPriceIncVatCreditsAndUpdate(fixedPrice);
					}

					if (CurrentCampaignCredit.IsPriceFixed)
						FixPriceTextBox.Text = CurrentCampaignCredit.FixedDiscount.ToString("P2");
					else
						FixPriceTextBox.Text = "";
					LoadPaymentControl();
				}
				catch (Exception ex)
				{
					SpottedException.TryToSaveExceptionAndChildExceptions(ex, HttpContext.Current, Usr.Current, Visit.HasCurrent ? Visit.Current : null, "", "", "", 0, null);
				}
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
						CurrentCampaignCredit.FixDiscountAndUpdate(null);
					else
					{
						var fixedPriceDiscount = Utilities.ConvertMoneyStringToDecimal(this.FixPriceTextBox.Text.Replace("%", ""));
						CurrentCampaignCredit.FixDiscountAndUpdate((double)fixedPriceDiscount / 100);
					}
					if (CurrentCampaignCredit.IsPriceFixed)
						FixPriceTextBox.Text = CurrentCampaignCredit.FixedDiscount.ToString("P2");
					else
						FixPriceTextBox.Text = "";
					LoadPaymentControl();
				}
				catch (Exception ex)
				{
					SpottedException.TryToSaveExceptionAndChildExceptions(ex, HttpContext.Current, Usr.Current, Visit.HasCurrent ? Visit.Current : null, "", "", "", 0, null);
				}
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
					CurrentCampaignCredit.FixDiscountAndUpdate(null);
					FixPriceTextBox.Text = "";
					LoadPaymentControl();
				}
				catch (Exception ex)
				{
					SpottedException.TryToSaveExceptionAndChildExceptions(ex, HttpContext.Current, Usr.Current, Visit.HasCurrent ? Visit.Current : null, "", "", "", 0, null);
				}
			}
		}
		#endregion
	}
}
