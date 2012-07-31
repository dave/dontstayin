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

namespace Spotted.Pages
{
	public partial class Camp : DsiUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			ContainerPage.SslPage = true;
			Usr.KickUserIfNotLoggedIn();

			if (!Page.IsPostBack)
			{
				Bobs.Global g = new Bobs.Global(Bobs.Global.Records.CampDsiTickets);
				if (g.ValueInt >= 500)
					ChangePanel(SoldOutPanel);
				else
					ChangePanel(QuantityPanel);
			}

			NameLabel.Text = Usr.Current.FullName;

			if (Usr.Current.CampTickets > 0)
			{
				AlreadyHaveTicketsP.Visible = true;
				AlreadyHaveTicketsP.InnerHtml = "You already have " + Usr.Current.CampTickets + " ticket" + (Usr.Current.CampTickets == 1 ? "" : "s") + " in your account. You can use this page to buy additional tickets. (5 tickets max).";
				AlreadyHaveTicketsLabel.Text = "additional";

			}
			//Nominal ledger: 4011
		}

		#region QuantityNext_Click
		protected void QuantityNext_Click(object sender, EventArgs eventArgs)
		{
			if (Usr.Current.CampTickets + int.Parse(TicketsQuantityTextBox.Text) > 5)
			{
				ChangePanel(QuantityErrorPanel);
				if (Usr.Current.CampTickets > 0)
					QuantityErrorP.InnerHtml="You already have " + Usr.Current.CampTickets + " ticket" + (Usr.Current.CampTickets == 1 ? "" : "s") + " in your account, and you've attempted to buy "+int.Parse(TicketsQuantityTextBox.Text).ToString()+" more.";
				else
					QuantityErrorP.InnerHtml="You attempted to buy " + int.Parse(TicketsQuantityTextBox.Text).ToString() + " tickets.";
			}
			else
			{

				//ChangePanel(PayPanel);
				//TicketPayment.Reset();
				//Controls.Payment.Item ticketPaymentItem = new Controls.Payment.Item();
				//ticketPaymentItem.Description = int.Parse(TicketsQuantityTextBox.Text).ToString() + " ticket" + (int.Parse(TicketsQuantityTextBox.Text) == 1 ? "" : "s")+ " for Camp DSI 2006.";
				//ticketPaymentItem.SetTotal(int.Parse(TicketsQuantityTextBox.Text) * 20);
				//ticketPaymentItem.Type=InvoiceItem.Types.DsiEventTickets;
				//ticketPaymentItem.CustomData=int.Parse(TicketsQuantityTextBox.Text).ToString();

				//TicketPayment.Items.Add(ticketPaymentItem);


			}
		}
		#endregion

		#region PayCancel_Click
		protected void PayCancel_Click(object sender, EventArgs eventArgs)
		{
			ChangePanel(QuantityPanel);
		}
		#endregion

		#region PaymentReceived
		protected void PaymentReceived(object sender, Controls.Payment.PaymentDoneEventArgs e)
		{
			if (!e.Duplicate)
			{
				Mailer m = new Mailer();
				m.Body = @" 
<p>Thank you!</p>
<p>Payment receipt for £" + ((int)(int.Parse(TicketsQuantityTextBox.Text) * 20)).ToString() + @".</p>
<p>Reference: " + int.Parse(TicketsQuantityTextBox.Text) + @" ticket" + (int.Parse(TicketsQuantityTextBox.Text)==1?"":"s") + @" for Camp DSI 2006.</p>
<p>Payment reference: "+e.Invoices[0].K.ToString()+@".</p>
<p>Please regularly check the Camp DSI event listing for further information and updates.</p>
<p>See you soon!</p>";
				m.Bulk = false;
				m.RedirectUrl = "/uk/barnstaple/a-secret-location/2006/jun/17/event-29398";
				m.Subject = "Receipt for Camp DSI tickets";
				m.UsrRecipient = Usr.Current;
				m.Send();

				Usr.Current.CampTickets += int.Parse(TicketsQuantityTextBox.Text);
				Usr.Current.Update();
			}

			Update u = new Update();
			u.Changes.Add(new Assign.Override(Bobs.Global.Columns.ValueInt, "(select sum(CampTickets) from Usr)"));
			u.Table=TablesEnum.Global;
			u.Where=new Q(Bobs.Global.Columns.K,Bobs.Global.Records.CampDsiTickets);
			u.Run();

			Usr.Current.AttendEvent(29398, true, null, null);
			DoneQuantityLabel.Text = int.Parse(TicketsQuantityTextBox.Text).ToString() + " ticket" + (int.Parse(TicketsQuantityTextBox.Text) == 1 ? "" : "s");
			ChangePanel(DonePanel);
		}
		#endregion

		#region ChangePanel
		void ChangePanel(Panel p)
		{
			QuantityPanel.Visible = p.Equals(QuantityPanel);
			PayPanel.Visible = p.Equals(PayPanel);
			DonePanel.Visible = p.Equals(DonePanel);
			QuantityErrorPanel.Visible = p.Equals(QuantityErrorPanel);
			SoldOutPanel.Visible = p.Equals(SoldOutPanel);
		}
		#endregion
	}
}