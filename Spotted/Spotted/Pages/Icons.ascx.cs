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

namespace Spotted.Pages
{
	public partial class Icons : DsiUserControl
	{
		protected DonationIcon DonationIcon
		{
			get
			{
				return uiDonateText.DonationIcon;
			}
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn();
			ContainerPage.SslPage = true;

			if (!Page.IsPostBack)
			{
				DonateLoggedOut.Visible = Usr.Current == null;
				DonateLoggedIn.Visible = Usr.Current != null;

				if (Usr.Current != null)
				{
					UsrDonationIcon udi = new UsrDonationIcon
					{
						DonationIconK = DonationIcon.K,
						UsrK = Usr.Current.K,
						BuyableLockDateTime = DateTime.Now,
						Enabled = false
					};
					udi.Update();

					InvoiceDataHolder i = new InvoiceDataHolder();
					InvoiceItemDataHolder iidh = new InvoiceItemDataHolder();
					if (DonationIcon.Vatable.Value)
						iidh.VatCode = InvoiceItem.VATCodes.T1;
					else
						iidh.VatCode = InvoiceItem.VATCodes.T9;

					if (DonationIcon.Charity)
					{
						iidh.Type = InvoiceItem.Types.CharityDonation;
						iidh.Description = "Charity donation";
						iidh.ShortDescription = "Charity donation";
					}
					else
					{
						iidh.Type = InvoiceItem.Types.UsrDonate;
						iidh.Description = "Profile icon";
						iidh.ShortDescription = "Profile icon";
					}

					iidh.SetTotal(DonationIcon.Price);
					iidh.KeyData = Usr.Current.K;
					iidh.BuyableObjectK = udi.K;
					iidh.BuyableObjectType = Model.Entities.ObjectType.UsrDonationIcon;
					iidh.RevenueStartDate = DateTime.Today;
					iidh.RevenueEndDate = DateTime.Today;
					i.InvoiceItemDataHolderList.Add(iidh);
					i.Type = Invoice.Types.Invoice;
					i.UsrK = Usr.Current.K;

					Payment.Invoices.Clear();
					Payment.Invoices.Add(i);
					Payment.PromoterK = 0;
					Payment.Initialize();
				}
			}
		}
		public void Page_PreRender(object sender, System.EventArgs e)
		{
			if (Usr.Current == null)
			{
				DonateRemainPanel.Visible = false;
			}
			else
			{
				var dis = DonationIcon.GetAllDonationIcons();
				var udis = DonationIcon.GetIconsForUsr(Usr.Current.K);

				bool foundOne = false;
				List<DonationIcon> icons = new List<DonationIcon>();
				foreach (DonationIcon di in dis)
				{
					bool found = false;
					foreach (DonationIcon udi in udis)
					{
						if (udi.K == di.K)
						{
							found = true;
							
							break;
						}
					}
					if (!found)
					{
						foundOne = true;
						icons.Add(di);
						//user doesn't have this icon...
					}
				}

				DonationIconsHtml.Text = "";

				int i = 0;
				foreach (DonationIcon d in icons)
				{
					DonationIconsHtml.Text +=
						string.Format(
							"<td style='padding-right:6px;padding-bottom:6px;' width='130'>{0}</td>",
							d.IconHtml);

					i++;

					if (i % 3 == 0)
						DonationIconsHtml.Text += "</tr><tr>";

				}

				DonateRemainPanel.Visible = foundOne;

			}
		}
		public void PaymentReceived(object o, Controls.Payment.PaymentDoneEventArgs e)
		{
			DonateLoggedIn.Visible = false;
			DonatedMessagePanel.Visible = true;
			ContainerPage.SslPage = true;
		}
	}
}
