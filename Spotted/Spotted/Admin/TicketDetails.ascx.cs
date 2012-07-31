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
	public partial class TicketDetails : AdminUserControl
	{
		#region Page Load
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{
                if (this.ContainerPage.Url["ticketk"].Exists && this.ContainerPage.Url["ticketk"].IsInt)
				{
					CurrentTicket = new Ticket(Convert.ToInt32(this.ContainerPage.Url["ticketk"].Value));
				}
                this.ChargePromoterAmountTextBox.Text = Ticket.DEFAULT_REFUND_CHARGE.ToString("c");
			}
			LoadTicketToScreen();
		}
		#endregion

		#region Properties
		public Ticket CurrentTicket
		{
			get
			{
				if (currentTicket == null && CurrentTicketK > 0)
				{
					currentTicket = new Ticket(CurrentTicketK);
				}
				return currentTicket;
			}
			set
			{
				currentTicket = value;
				if (currentTicket != null)
					CurrentTicketK = currentTicket.K;
				else
					CurrentTicketK = 0;
			}
		}
		public Ticket currentTicket;
		public int CurrentTicketK = 0;


		#endregion

		#region ViewState
		#region SaveViewState()
		protected override object SaveViewState()
		{
			this.ViewState["CurrentTicketK"] = CurrentTicketK;
			return base.SaveViewState();
		}
		#endregion
		#region LoadViewState()
		protected override void LoadViewState(object savedState)
		{
			base.LoadViewState(savedState);
			if (this.ViewState["CurrentTicketK"] != null) CurrentTicketK = (int)this.ViewState["CurrentTicketK"];
		}
		#endregion
		#endregion

		#region Methods
		#region LoadTicketToScreen
		public void LoadTicketToScreen()
		{
			this.TicketDetailsPanel.Visible = CurrentTicket != null;

			if (CurrentTicket != null)
			{
				CancelledRow.Visible = CurrentTicket.Cancelled;
				RefundButton.Enabled = !CurrentTicket.Cancelled && Usr.Current.IsSuperAdmin;
				RefundFullButton.Enabled = !CurrentTicket.Cancelled && Usr.Current.IsSuperAdmin;
                ChargePromoterForRefundCheckBox.Visible = !CurrentTicket.Cancelled;
                ChargePromoterAmountTextBox.Visible = !CurrentTicket.Cancelled;

				TicketKLabel.Text = CurrentTicket.K.ToString();
				UserNickNameLabel.Text = CurrentTicket.BuyerUsr.Link();
				FullNameLabel.Text = CurrentTicket.FirstName + " " + CurrentTicket.LastName;
                PurchaseDateLabel.Text = CurrentTicket.BuyDateTime.ToString("ddd dd/MM/yy HH:mm");
				EventLabel.Text = CurrentTicket.Event.LinkFriendlyName;
				TicketRunLabel.Text = CurrentTicket.TicketRun.LinkPriceBrandName;
				QuantityLabel.Text = CurrentTicket.Quantity.ToString();
				PriceLabel.Text = CurrentTicket.Price.ToString("c");
				BookingFeeLabel.Text = CurrentTicket.BookingFee.ToString("c");
				if (CurrentTicket.Invoice != null)
				{
					InvoiceLabel.Text = CurrentTicket.Invoice.AdminLink();
					InvoiceLabel.ForeColor = System.Drawing.Color.Black;
				}
				else
				{
					InvoiceLabel.Text = "* INVOICE COULD NOT BE FOUND";
					InvoiceLabel.ForeColor = System.Drawing.Color.Red;
					RefundButton.Enabled = false;
					RefundFullButton.Enabled = false;
				}
				CardNumberEndLabel.Text = CurrentTicket.CardNumberEnd;
				CodeLabel.Text = CurrentTicket.Code;
				AddressLabel.Text = CurrentTicket.AddressInHtml;
				if (CurrentTicket.Feedback != Ticket.FeedbackEnum.None)
				{
					FeedbackLabel.Text = Utilities.TickCrossHtml(CurrentTicket.Feedback == Ticket.FeedbackEnum.Good);
				}
				else
					FeedbackLabel.Text = "None";

				FeedbackNoteRow.Visible = CurrentTicket.FeedbackNote.Length > 0;
				FeedbackNoteLabel.Text = CurrentTicket.FeedbackNote;
			}
		}
		#endregion

		private void DoRefund(bool refundIncludeBookingFee)
		{
			if (Usr.Current.IsSuperAdmin && CurrentTicket.Cancelled == false)
			{
				decimal chargeToPromoter = 0;
                if (ChargePromoterForRefundCheckBox.Checked)
                {
                    chargeToPromoter = Utilities.ConvertMoneyStringToDecimal(this.ChargePromoterAmountTextBox.Text);
                }

				//double transferTotals = 0;
				ProcessingVal.ErrorMessage = "Error refunding. See admin email for details.";
				try
				{
					CurrentTicket.Refund(Usr.Current, refundIncludeBookingFee, chargeToPromoter);
				}
				catch (Exception ex)
				{
					ProcessingVal.IsValid = false;
					ProcessingVal.ErrorMessage = "Exception during refund process: " + ex.Message;
				}

				if (ProcessingVal.IsValid)
					ProcessingVal.IsValid = CurrentTicket.Cancelled;

				LoadTicketToScreen();
			}
		}
		#endregion

		#region Page Event Handlers
		#region Refund
		public void RefundButton_Click(object sender, EventArgs e)
		{
            DoRefund(false);
		}

		public void RefundFullButton_Click(object sender, EventArgs e)
		{
			DoRefund(true);
		}

		#endregion
		#endregion
	}
}
