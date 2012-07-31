using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Collections;
using Cambro;
using Cambro.Web;
using Cambro.Misc;

using System.Net;
using System.IO;
using System.Text;
using System.Net.Sockets;

using System.Configuration;
using System.Diagnostics;
using System.ComponentModel;
using System.Xml;
using Bobs.DataHolders;
using Common;

namespace Bobs
{

	#region Invoice
	/// <summary>
	/// Invoice or credit note
	/// </summary>
	[Serializable]
	public partial class Invoice : IBobReport, ILinkable, ILinkableAdmin, IBobAsHTML, IAdminPage, IPage, IReadableReference
	{

		#region simple members
		/// <summary>
		/// The primary key
		/// </summary>
		public override int K
		{
			get { return this[Invoice.Columns.K] as int? ?? 0; }
			set { this[Invoice.Columns.K] = value; }
		}
		/// <summary>
		/// Invoice, Credit
		/// </summary>
		public override Types Type
		{
			get { return (Types)this[Invoice.Columns.Type]; }
			set { this[Invoice.Columns.Type] = value; }
		}
		/// <summary>
		/// The user that created this invoice
		/// </summary>
		public override int UsrK
		{
			get { return (int)this[Invoice.Columns.UsrK]; }
			set { usr = null; this[Invoice.Columns.UsrK] = value; }
		}
		/// <summary>
		/// The If this is a promoter invoice - this is the promoter
		/// </summary>
		public override int PromoterK
		{
			get { return (int)this[Invoice.Columns.PromoterK]; }
			set { promoter = null; this[Invoice.Columns.PromoterK] = value; }
		}
		/// <summary>
		/// Link to the user that initiated this transfer (e.g. the admin user if it's done manually!)
		/// </summary>
		public override int ActionUsrK
		{
			get { return (int)this[Invoice.Columns.ActionUsrK]; }
			set { this[Invoice.Columns.ActionUsrK] = value; }
		}
		/// <summary>
		/// Name from credit card payment control
		/// </summary>
		public override string Name
		{
			get { return (string)this[Invoice.Columns.Name]; }
			set { this[Invoice.Columns.Name] = value; }
		}
		/// <summary>
		/// First line of the address from credit card payment control
		/// </summary>
		public override string Address
		{
			get { return (string)this[Invoice.Columns.Address]; }
			set { this[Invoice.Columns.Address] = value; }
		}
		/// <summary>
		/// Postcode from credit card payment control
		/// </summary>
		public override string Postcode
		{
			get { return (string)this[Invoice.Columns.Postcode]; }
			set { this[Invoice.Columns.Postcode] = value; }
		}
		/// <summary>
		/// Payment type - 1=Invoice, 2=Credit
		/// </summary>
		public override PaymentTypes PaymentType
		{
			get { return (PaymentTypes)this[Invoice.Columns.PaymentType]; }
			set { this[Invoice.Columns.PaymentType] = value; }
		}
		/// <summary>
		/// Has this invoice been paid?
		/// </summary>
		public override bool Paid
		{
			get { return (bool)this[Invoice.Columns.Paid]; }
			set { this[Invoice.Columns.Paid] = value; }
		}
		/// <summary>
		/// Date/time the invoice was created
		/// </summary>
		public override DateTime CreatedDateTime
		{
			get { return (DateTime)this[Invoice.Columns.CreatedDateTime]; }
			set { this[Invoice.Columns.CreatedDateTime] = value; }
		}
		/// <summary>
		/// When is the invoice due to be paid (4 weeks). After this we can charge interest.
		/// </summary>
		public override DateTime DueDateTime
		{
			get { return (DateTime)this[Invoice.Columns.DueDateTime]; }
			set { this[Invoice.Columns.DueDateTime] = value; }
		}
		/// <summary>
		/// Date/time that payment was received
		/// </summary>
		public override DateTime PaidDateTime
		{
			get { return (DateTime)this[Invoice.Columns.PaidDateTime]; }
			set { this[Invoice.Columns.PaidDateTime] = value; }
		}
		/// <summary>
		/// Total price excluding VAT
		/// </summary>
		public override decimal Price
		{
			get { return (decimal)this[Invoice.Columns.Price]; }
			set { this[Invoice.Columns.Price] = value; }
		}
		/// <summary>
		/// Total VAT
		/// </summary>
		public override decimal Vat
		{
			get { return (decimal)this[Invoice.Columns.Vat]; }
			set { this[Invoice.Columns.Vat] = value; }
		}
		/// <summary>
		/// Total price including VAT
		/// </summary>
		public override decimal Total
		{
			get { return (decimal)this[Invoice.Columns.Total]; }
			set { this[Invoice.Columns.Total] = value; }
		}
		/// <summary>
		/// Guid to catch duplicate "pay now" clicks
		/// </summary>
		public override Guid DuplicateGuid
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[Invoice.Columns.DuplicateGuid]); }
			set { this[Invoice.Columns.DuplicateGuid] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// Additional Notes
		/// </summary>
		public override string Notes
		{
			get { return (string)this[Invoice.Columns.Notes]; }
			set { this[Invoice.Columns.Notes] = value; }
		}
		/// <summary>
        /// T1: Normal
        /// T3: Rest of world - forces all InvoiceItems to 0%
        /// T4: EU VAT registered businesses - forces all InvoiceItems to 0%
		/// </summary>
		public override VATCodes VatCode
		{
			get { return (VATCodes)this[Invoice.Columns.VatCode]; }
			set { this[Invoice.Columns.VatCode] = value; }
		}
		/// <summary>
		/// Who is the account manager for this invoice?
		/// </summary>
		public override int SalesUsrK
		{
			get { return (int)this[Invoice.Columns.SalesUsrK]; }
			set { this[Invoice.Columns.SalesUsrK] = value; }
		}
		/// <summary>
		/// How much is contributed to the account managers target?
		/// </summary>
		public override decimal SalesUsrAmount
		{
			get { return (decimal)this[Invoice.Columns.SalesUsrAmount]; }
			set { this[Invoice.Columns.SalesUsrAmount] = value; }
		}
		/// <summary>
		/// Flag for immediate credit card payments. This flag to be used for exports to Sage
		/// </summary>
		public override bool IsImmediateCreditCardPayment
		{
			get { return (bool)this[Invoice.Columns.IsImmediateCreditCardPayment]; }
			set { this[Invoice.Columns.IsImmediateCreditCardPayment] = value; }
		}
		/// <summary>
		/// Tax date - to be used for exporting to Sage
		/// </summary>
		public override DateTime TaxDateTime
		{
			get { return (DateTime)this[Invoice.Columns.TaxDateTime]; }
			set { this[Invoice.Columns.TaxDateTime] = value; }
		}
		/// <summary>
		/// Invoice purchase order number
		/// </summary>
		public override string PurchaseOrderNumber
		{
			get { return (string)this[Invoice.Columns.PurchaseOrderNumber]; }
			set { this[Invoice.Columns.PurchaseOrderNumber] = value; }
		}
		/// <summary>
		/// Used when the item is a campaign credit top-up
		/// </summary>
		public override int InsertionOrderK
		{
			get { return (int)this[Invoice.Columns.InsertionOrderK]; }
			set { this[Invoice.Columns.InsertionOrderK] = value; }
		}
		/// <summary>
		/// Type of the buyer: AgencyPromoter = 1, NonAgencyPromoter = 2, TicketUsr = 3, NonTicketUsr = 4
		/// </summary>
		public override BuyerTypes BuyerType
		{
			get { return (BuyerTypes)this[Invoice.Columns.BuyerType]; }
			set { this[Invoice.Columns.BuyerType] = value; }
		}
		/// <summary>
		/// Price before any discounts have been applied
		/// </summary>
		public override decimal PriceBeforeDiscount
		{
			get { return (decimal)this[Invoice.Columns.PriceBeforeDiscount]; }
			set { this[Invoice.Columns.PriceBeforeDiscount] = value; }
		}
		/// <summary>
		/// average of item level Discount - percentage - stored between 0 and 1
		/// </summary>
		public override double Discount
		{
			get { return (double)this[Invoice.Columns.Discount]; }
			set { this[Invoice.Columns.Discount] = value; }
		}
		/// <summary>
		/// sum of Invoice Item PriceBeforeAgencyDiscount but after item discount has been applied
		/// </summary>
		public override decimal PriceBeforeAgencyDiscount
		{
			get { return (decimal)this[Invoice.Columns.PriceBeforeAgencyDiscount]; }
			set { this[Invoice.Columns.PriceBeforeAgencyDiscount] = value; }
		}
		/// <summary>
		/// average of item level agency discount - percentage - stored between 0 and 1
		/// </summary>
		public override double AgencyDiscount
		{
			get { return (double)this[Invoice.Columns.AgencyDiscount]; }
			set { this[Invoice.Columns.AgencyDiscount] = value; }
		}


		#endregion

		#region ILinkable Members

		public string Link(params string[] par)
		{
			return ILinkableExtentions.Link(this, par);
		}
		public string LinkNewWindow(params string[] par)
		{
			return ILinkableExtentions.LinkNewWindow(this, par);
		}

		#endregion

		#region ILinkableAdmin Members

		public string AdminLink(params string[] par)
		{
			return ILinkableAdminExtentions.AdminLink(this, par);
		}
		public string AdminLinkNewWindow(params string[] par)
		{
			return ILinkableAdminExtentions.AdminLinkNewWindow(this, par);
		}

		#endregion

		#region IBobAsHTML methods
		public string AsHTML()
		{
			string lineReturn = Vars.HTML_LINE_RETURN;
			StringBuilder sb = new StringBuilder();

			sb.Append(lineReturn);
			sb.Append(lineReturn);
			sb.Append("<u>Invoice details</u>");
			sb.Append(lineReturn);
			sb.Append(this.TypeToString);
			sb.Append(" K: ");
			sb.Append(this.K.ToString());
			sb.Append(lineReturn);
			if (this.Promoter != null)
			{
				sb.Append("Promoter: ");
				sb.Append(this.Promoter.Name);
				sb.Append(" (K: ");
				sb.Append(this.PromoterK.ToString());
				sb.Append(")");
				sb.Append(lineReturn);
			}
			if (this.Usr != null)
			{
				sb.Append("Usr: ");
				sb.Append(this.Usr.NickName);
				sb.Append(" (K: ");
				sb.Append(this.UsrK.ToString());
				sb.Append(")");
				sb.Append(lineReturn);
			}
			sb.Append("Date created: ");
			sb.Append(this.CreatedDateTime.ToString("ddd dd/MM/yyyy HH:mm:ss"));
			sb.Append(lineReturn);
			if (PaidDateTime >= this.CreatedDateTime)
			{
				sb.Append("Date paid: ");
				sb.Append(this.PaidDateTime.ToString("ddd dd/MM/yyyy HH:mm:ss"));
				sb.Append(lineReturn);
			}
			
			sb.Append("Price: ");
			sb.Append(Utilities.MoneyToHTML(this.Price));
			sb.Append(lineReturn);
			sb.Append("VAT: ");
			sb.Append(Utilities.MoneyToHTML(this.Vat));
			sb.Append(lineReturn);
			sb.Append("Total: ");
			sb.Append(Utilities.MoneyToHTML(this.Total));
			sb.Append(lineReturn);
			sb.Append("Status: ");
			sb.Append(this.Status.ToString());
			sb.Append(lineReturn);


			return sb.ToString();
		}
		#endregion

		private const string CREDIT = "Credit";

		#region Properties

		#region AmountPaid & AmountDue
		public decimal AmountDue
		{
			get
			{
				if (Total - AmountPaid < 0)
					return 0;
				else
					return Math.Round(Total - AmountPaid, 2);
			}
		}

		public decimal AmountPaid
		{
			get
			{
				InvoiceCreditSet invoiceCreditSet = new InvoiceCreditSet(new Query(new Q(InvoiceCredit.Columns.InvoiceK, this.K)));

				decimal totalPaid = 0;

				foreach (InvoiceTransfer invoiceTransfer in this.SuccessfulInvoiceTransfers)
				{
					totalPaid += invoiceTransfer.Amount;
				}
				// Subtract for Credits as their amounts will be negative
				foreach (InvoiceCredit invoiceCredit in invoiceCreditSet)
				{
					totalPaid -= invoiceCredit.Amount;
					totalPaid += new Invoice(invoiceCredit.CreditInvoiceK).AmountPaid;
				}

				return Math.Round(totalPaid, 2);
			}
		}
		#endregion

		public string TypeAndK
		{
			get
			{
				return Utilities.CamelCaseToString(this.Type.ToString()) + " #" + this.K.ToString();
			}
		}

		public decimal AmountOfCreditApplied
		{
			get
			{
				if (amountOfCreditApplied == 0 && this.CreditsApplied != null)
				{
					foreach (Invoice credit in this.CreditsApplied)
					{
						amountOfCreditApplied += credit.Total;
					}
				}
				return Math.Abs(Math.Round(amountOfCreditApplied, 2));
			}
		}
		private decimal amountOfCreditApplied = 0;

		public decimal AmountAllowedToCredit
		{
			get
			{
				return Math.Round(this.Total - this.AmountOfCreditApplied, 2);
			}
		}
		#endregion

		#region IsUsrAllowedAccess
		public bool IsUsrAllowedAccess(Usr usr)
		{
			return usr.IsAdmin || ((this.PromoterK > 0 && usr.IsPromoter && usr.IsPromoterK(this.PromoterK)) || (this.PromoterK == 0 && this.UsrK > 0 && usr.K == this.UsrK));
		}
		#endregion

		#region SalesUsr
		public Usr SalesUsr
		{
			get
			{
				if (salesUsr == null && SalesUsrK > 0)
					salesUsr = new Usr(SalesUsrK, this, Invoice.Columns.SalesUsrK);
				return salesUsr;
			}
			set
			{
				salesUsr = value;
			}
		}
		Usr salesUsr;
		#endregion

		#region SendSalesSummaries()
		public static void SendSalesSummaries()
		{
			return; //deleted this!

			//StringBuilder sb = new StringBuilder();

			//sb.Append("<p>Sales summaries for " + DateTime.Today.ToString("dddd dd MMM yyyy") + "</p>");
			//sb.Append("[/div]");


			//#region Yesterday
			//DateTime minDate = DateTime.Today.AddDays(-1);

			//if (DateTime.Today.DayOfWeek.Equals(DayOfWeek.Monday))
			//{
			//    minDate = DateTime.Today.AddDays(-3);
			//    sb.Append("<h1>Over the weekend</h1>");
			//    sb.Append("[div]");
			//    sb.Append("<h2>Sales</h2>");
			//    sb.Append("<p>" + DateTime.Today.AddDays(-3).ToString("dddd dd MMMM yyyy") + " - " + DateTime.Today.AddDays(-1).ToString("dddd dd MMMM yyyy") + "</p>");
			//}
			//else
			//{
			//    sb.Append("<h1>Yesterday</h1>");
			//    sb.Append("[div]");
			//    sb.Append("<h2>Sales</h2>");
			//    sb.Append("<p>" + DateTime.Today.AddDays(-1).ToString("dddd dd MMMM yyyy") + "</p>");
			//}

			//DateTime maxDate = DateTime.Today;


			//#region Sales
			//try
			//{


			//    Query q = new Query();
			//    q.TableElement = new Join(InvoiceItem.Columns.InvoiceK, Invoice.Columns.K);
			//    q.QueryCondition = new And(
			//        new Or(
			//            new Q(InvoiceItem.Columns.Type, InvoiceItem.Types.Banner),
			//            new Q(InvoiceItem.Columns.Type, InvoiceItem.Types.BannerTop),
			//            new Q(InvoiceItem.Columns.Type, InvoiceItem.Types.BannerHotbox),
			//            new Q(InvoiceItem.Columns.Type, InvoiceItem.Types.BannerSkyscraper),
			//            new Q(InvoiceItem.Columns.Type, InvoiceItem.Types.BannerPhoto),
			//            new Q(InvoiceItem.Columns.Type, InvoiceItem.Types.BannerEmail),
			//            new Q(InvoiceItem.Columns.Type, InvoiceItem.Types.EventDonate),
			//            new Q(InvoiceItem.Columns.Type, InvoiceItem.Types.GuestlistCredit),
			//            new Q(InvoiceItem.Columns.Type, InvoiceItem.Types.OtherWebAdvertising),
			//            new Q(InvoiceItem.Columns.Type, InvoiceItem.Types.NonWebAdvertising)
			//        ),
			//        new Q(Invoice.Columns.PaidDateTime, QueryOperator.GreaterThanOrEqualTo, minDate),
			//        new Q(Invoice.Columns.PaidDateTime, QueryOperator.LessThan, maxDate),
			//        new Q(Invoice.Columns.PromoterK, QueryOperator.GreaterThan, 0)
			//    );
			//    q.OrderBy = new OrderBy(
			//        new OrderBy(Invoice.Columns.PromoterK),
			//        new OrderBy(InvoiceItem.Columns.Total, OrderBy.OrderDirection.Descending));
			//    InvoiceItemSet iis = new InvoiceItemSet(q);
			//    if (iis.Count == 0)
			//    {
			//        sb.Append("<p>no sales yesterday</p>");
			//    }
			//    else
			//    {
			//        sb.Append("<table>");
			//        double grandTotal = 0.0;
			//        double promoterTotal = 0.0;
			//        int currentPromoterK = iis[0].Invoice.PromoterK;
			//        sb.Append("<tr><td><b><a href=\"[LOGIN(" + iis[0].Invoice.Promoter.Url() + ")]\">" + iis[0].Invoice.Promoter.Name + "</a></b></td><td>&nbsp;</td></tr>");
			//        foreach (InvoiceItem ii in iis)
			//        {
			//            if (ii.Invoice.PromoterK != currentPromoterK)
			//            {
			//                //finish that promoter
			//                sb.Append("<tr><td align=right><i>" + ii.Invoice.Promoter.Name + " total: </i></td><td><i>" + promoterTotal.ToString("c") + "</i></td></tr>");
			//                currentPromoterK = ii.Invoice.PromoterK;
			//                promoterTotal = 0.0;
			//                sb.Append("<tr><td><b><a href=\"" + ii.Invoice.Promoter.Url() + "\">" + ii.Invoice.Promoter.Name + "</a></b></td><td>&nbsp;</td></tr>");
			//            }
			//            promoterTotal += ii.Price;
			//            grandTotal += ii.Price;
			//            sb.Append("<tr><td>" + ii.Description + "</td><td>" + ii.Price.ToString("c") + "</td></tr>");
			//        }
			//        sb.Append("<tr><td align=right><i>" + iis[iis.Count - 1].Invoice.Promoter.Name + " total:</i></td><td><i>" + promoterTotal.ToString("c") + "</i></td></tr>");
			//        sb.Append("</table>");
			//        sb.Append("<p><b>Grand total: " + grandTotal.ToString("c") + "</b></p>");

			//    }


			//}
			//catch (Exception ex)
			//{
			//    sb.Append("<p>Exception!</p><p><pre>" + ex.ToString() + "</pre></p>");
			//}
			//#endregion

			//#region Promoters
			//try
			//{
			//    Query q1 = new Query();
			//    q1.ReturnCountOnly = true;
			//    q1.QueryCondition = new And(
			//        new Q(Promoter.Columns.Status, Promoter.StatusEnum.Active),
			//        new Q(Promoter.Columns.EnabledDateTime, QueryOperator.GreaterThanOrEqualTo, minDate),
			//        new Q(Promoter.Columns.EnabledDateTime, QueryOperator.LessThan, maxDate)
			//    );
			//    PromoterSet ps1 = new PromoterSet(q1);

			//    Query q2 = new Query();
			//    q2.ReturnCountOnly = true;
			//    q2.QueryCondition = new And(
			//        new Q(Promoter.Columns.DateTimeSignUp, QueryOperator.GreaterThanOrEqualTo, minDate),
			//        new Q(Promoter.Columns.DateTimeSignUp, QueryOperator.LessThan, maxDate)
			//    );
			//    PromoterSet ps2 = new PromoterSet(q2);


			//    sb.Append("<h2>New promoters</h2>");
			//    sb.Append("<p>" + ps1.Count.ToString() + " promoter accounts enabled.</p>");
			//    sb.Append("<p>" + ps2.Count.ToString() + " new promoters joined the site.</p>");

			//}
			//catch (Exception ex)
			//{
			//    sb.Append("<p>Exception!</p><p><pre>" + ex.ToString() + "</pre></p>");
			//}
			//#endregion

			//sb.Append("[/div]");

			//#endregion

			//#region This week

			//sb.Append("<h1>This week to date</h1>");
			//sb.Append("[div]");

			//#region Sales
			//try
			//{
			//    sb.Append("<h2>Sales</h2>");
			//    sb.Append("<p>coming soon</p>");

			//}
			//catch (Exception ex)
			//{
			//    sb.Append("<p>Exception!</p><p><pre>" + ex.ToString() + "</pre></p>");
			//}
			//#endregion

			//#region Promoters
			//try
			//{
			//    sb.Append("<h2>Promoters</h2>");
			//    sb.Append("<p>coming soon</p>");

			//}
			//catch (Exception ex)
			//{
			//    sb.Append("<p>Exception!</p><p><pre>" + ex.ToString() + "</pre></p>");
			//}
			//#endregion

			//sb.Append("[/div]");

			//#endregion

			//#region This month

			//sb.Append("<h1>This month to date (" + DateTime.Today.ToString("MMMM") + ")</h1>");
			//sb.Append("[div]");

			//#region Sales
			//try
			//{
			//    sb.Append("<h2>Sales</h2>");
			//    sb.Append("<p>coming soon</p>");

			//}
			//catch (Exception ex)
			//{
			//    sb.Append("<p>Exception!</p><p><pre>" + ex.ToString() + "</pre></p>");
			//}
			//#endregion

			//#region Promoters
			//try
			//{
			//    sb.Append("<h2>Promoters</h2>");
			//    sb.Append("<p>coming soon</p>");

			//}
			//catch (Exception ex)
			//{
			//    sb.Append("<p>Exception!</p><p><pre>" + ex.ToString() + "</pre></p>");
			//}
			//#endregion

			//sb.Append("[/div]");

			//#endregion

			//#region Last week

			//sb.Append("<h1>Last week</h1>");
			//sb.Append("[div]");

			//#region Sales
			//try
			//{
			//    sb.Append("<h2>Sales</h2>");
			//    sb.Append("<p>coming soon</p>");

			//}
			//catch (Exception ex)
			//{
			//    sb.Append("<p>Exception!</p><p><pre>" + ex.ToString() + "</pre></p>");
			//}
			//#endregion

			//#region Promoters
			//try
			//{
			//    sb.Append("<h2>Promoters</h2>");
			//    sb.Append("<p>coming soon</p>");

			//}
			//catch (Exception ex)
			//{
			//    sb.Append("<p>Exception!</p><p><pre>" + ex.ToString() + "</pre></p>");
			//}
			//#endregion

			//sb.Append("[/div]");

			//#endregion

			//#region Last month

			//sb.Append("<h1>Last month (" + DateTime.Today.AddMonths(-1).ToString("MMMM") + ")</h1>");
			//sb.Append("[div]");

			//#region Sales
			//try
			//{
			//    sb.Append("<h2>Sales</h2>");
			//    sb.Append("<p>coming soon</p>");

			//}
			//catch (Exception ex)
			//{
			//    sb.Append("<p>Exception!</p><p><pre>" + ex.ToString() + "</pre></p>");
			//}
			//#endregion

			//#region Promoters
			//try
			//{
			//    sb.Append("<h2>Promoters</h2>");
			//    sb.Append("<p>coming soon</p>");

			//}
			//catch (Exception ex)
			//{
			//    sb.Append("<p>Exception!</p><p><pre>" + ex.ToString() + "</pre></p>");
			//}
			//#endregion

			//sb.Append("[/div]");

			//#endregion


			//Mailer m = new Mailer();
			//m.Subject = "Sales summary, " + DateTime.Today.ToString("dddd dd MMM yyyy");
			//m.Body = sb.ToString();
			//m.UsrRecipient = new Usr(289079);
			//m.Send();

		}
		#endregion

		#region AssignSalesUsrAndAmount
		public void AssignSalesUsrAndAmount()
		{
			if (this.PromoterK > 0 && this.Promoter != null)
			{
				if (this.Promoter.SalesStatus.Equals(Promoter.SalesStatusEnum.Proactive) || this.Promoter.SalesStatus.Equals(Promoter.SalesStatusEnum.Active))
				{
					this.Items = null;
					decimal salesTotal = 0m;
					foreach (InvoiceItem ii in this.Items)
					{
						if (ii.DoesApplyToSalesUsrAmount)
						{
							salesTotal += ii.Price;
						}
					}
					this.SalesUsrK = this.Promoter.SalesUsrK;
					this.SalesUsrAmount = salesTotal;
					this.Update();
				}
				UpdatePromoterStatusAndSalesStatus();
			}
		}
		#endregion

		#region UpdatePromoterStatusAndSalesStatus
		public void UpdatePromoterStatusAndSalesStatus()
		{
			if (this.PromoterK > 0 && this.Promoter != null)
			{
				if (this.Promoter.Status.Equals(Promoter.StatusEnum.Enabled))
				{
					//enable the promoter
					this.Promoter.Status = Promoter.StatusEnum.Active;
					this.Promoter.Update();
					this.Promoter.UpdateModerators();
				}

				if (this.Promoter.SalesStatus.Equals(Promoter.SalesStatusEnum.Proactive) || this.Promoter.SalesStatus.Equals(Promoter.SalesStatusEnum.Active))
				{
					this.Promoter.SalesStatus = Promoter.SalesStatusEnum.Active;
					this.Promoter.SetSalesStatusExpires(DateTime.Now.AddMonths(3));
					this.Promoter.Update();
				}
			}
		}
		#endregion

		#region DoesDuplicateGuidExistInDb
		/// <summary>
		/// Checks if this invoice has already been saved, based on the ViewState "DuplicateGuid".
		/// This is used to prevent a new invoice being saved twice and creating 2 DB records
		/// </summary>
		/// <returns></returns>
		public static bool DoesDuplicateGuidExistInDb(Guid checkGuid)
		{
			//select duplicate invoices
			Query duplicateInvoiceQuery = new Query(new Q(Invoice.Columns.DuplicateGuid, checkGuid));
			duplicateInvoiceQuery.ReturnCountOnly = true;

			return new InvoiceSet(duplicateInvoiceQuery).Count > 0;
		}
		#endregion

		#region AddNote
		// Add new note at top, so most recent notes are most visible
		public void AddNote(string note, string usrName)
		{
			string savedNotes = "";
			if (this.Notes.Length > 0)
				savedNotes += "\n" + this.Notes;
			this.Notes = "(" + DateTime.Now.ToString("dd/MM/yy HH:mm") + ") " + usrName + ": " + note + savedNotes;
		}
		#endregion

		#region SetPaidAndPaidDateTime
		public void SetPaidAndPaidDateTime(bool paid)
		{
			// If it was paid, but now isnt then reset PaidDateTime
			if (this.Paid == true && paid == false)
				this.PaidDateTime = DateTime.MinValue;
			// If it wasnt paid, but now is then set PaidDateTime
			else if (this.Paid == false && paid == true)
				this.PaidDateTime = DateTime.Now;

			// Paid date cannot be before Tax date
			//this.PaidDateTime = this.PaidDateTime < this.TaxDateTime ? this.TaxDateTime : this.PaidDateTime;

			this.Paid = paid;
		}
		#endregion

		#region GenerateReportInHTML
		public StringBuilder GenerateReportStringBuilder(bool linksEnabled)
		{
			DateTime taxDate = DateTime.Now;
			if (this.TaxDateTime != null && this.TaxDateTime > DateTime.MinValue)
				taxDate = this.TaxDateTime;


			StringBuilder sb = new StringBuilder();
			decimal amountPaid = 0;

			sb.Append(@"<form id='form1' runat='server'><div style='font-family:Verdana;'><table width='100%' border='0' cellspacing='0' cellpadding='0' height='100%'><tr><td valign='top'>
						<table width='100%'>");

			sb.Append(Utilities.GenerateHTMLHeaderRowString(this.Type.Equals(Invoice.Types.Credit) ? this.TypeToString.ToUpper() + "&nbsp;NOTE" : this.TypeToString.ToUpper()));

			sb.Append(@"<tr>
								<td colspan=1 align='left' valign='top' width='450' style='padding-left:48px;'>");
            if (this.Promoter != null)
            {
                if (this.Promoter.AccountsName.Length > 0)
                {
                    sb.Append(this.Promoter.AccountsName);
                    sb.Append("<br>");
                }
                else if (this.Usr != null && this.Usr.FullName.Length > 0 && this.Usr.IsPromoter && this.Usr.IsPromoterK(this.K))
                {
                    sb.Append(this.Usr.FullName);
                    sb.Append("<br>");
                }
                else if (this.Promoter.PrimaryUsr != null && this.Promoter.PrimaryUsr.FullName.Length > 0)
                {
                    sb.Append(this.Promoter.PrimaryUsr.FullName);
                    sb.Append("<br>");
                }

                if (this.Promoter.Name.Length > 0)
                {
                    sb.Append(this.Promoter.Name);
                    sb.Append("<br>");
                }
                sb.Append(this.Promoter.AddressHtml);
            }
            else if (this.Usr != null)
            {
                if (this.Usr.FullName.Length > 0)
                {
                    sb.Append(this.Usr.FullName);
                    sb.Append("<br>");
                }
                sb.Append(this.Usr.AddressHtml());
            }

			// Addition of Created and renaming of "Date" to "Tax Date", as per Dave's request 7/2/07
			sb.Append(@"</td><td width='350'></td><td valign='top' width='100'>" + this.TypeToString + "&nbsp;No.");
			if (this.PurchaseOrderNumber.Length > 0)
				sb.Append("<br><br>Purchase&nbsp;Order&nbsp;No.");
			sb.Append("<br><br>Acc&nbsp;No.<br><br>Created<br><br>Tax&nbsp;Date");
			if (this.Type.Equals(Invoice.Types.Invoice))
				sb.Append("<br><br>Due Date");
			sb.Append(@"</td><td align='right' valign='top' width='125'>");

			if (this.Type.Equals(Invoice.Types.Invoice))
				sb.Append("INV");
			else
				sb.Append("CRD");

			sb.Append(this.K.ToString());
			if (this.PurchaseOrderNumber.Length > 0)
				sb.Append("<br><br><nobr>" + this.PurchaseOrderNumber.Replace("-", "&#8209;"));
			sb.Append("</nobr><br><br>");
			if (this.Promoter != null)
				sb.Append(this.PromoterK.ToString());
			else if (this.Usr != null)
				sb.Append(this.UsrK.ToString());
			else
				sb.Append("&nbsp;");

			// Addition of Created, as per Dave's request 7/2/07
			sb.Append("<br><br>");
			sb.Append(this.CreatedDateTime.ToString("dd/MM/yy"));

			sb.Append("<br><br>");

			// Replacing CreatedDateTime with TaxDateTime, as per Gee's request for OASIS v1.5
			//sb.Append(this.CreatedDateTime.ToString("dd/MM/yy"));
			sb.Append(this.TaxDateTime.ToString("dd/MM/yy"));

			if (this.Type.Equals(Invoice.Types.Invoice))
			{
				sb.Append("<br><br>");
				sb.Append(this.DueDateTime.ToString("dd/MM/yy"));
			}
			sb.Append("</td></tr>");

			//if(this.PaidDateTime != null && this.PaidDateTime > DateTime.MinValue)
			//{
			//    sb.Append(@"<br><br><b>Date Completed</b><br>" + this.PaidDateTime.ToShortDateString());
			//}

			sb.Append(@"</table><br><br>
						<table width='100%' cellspacing='0' cellpadding='3' class='BorderBlack Top Right Bottom'>
							<tr>
								<td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='350'><b>Item</b></td>
								<td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='55' align='left'><b>Tax&nbsp;Code</b></td>
								<td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='90' align='left'><b>Price</b></td>
								<td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='85' align='left'><b>VAT</b></td>
								<td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='90' align='left'><b>Total</b></td>
							</tr>");

			Query InvoiceItemQuery = new Query(new Q(InvoiceItem.Columns.InvoiceK, this.K));
			InvoiceItemSet invoiceItemSet = new InvoiceItemSet(InvoiceItemQuery);

			List<InvoiceItem.VATCodes> invoiceItemVatCodes = new List<InvoiceItem.VATCodes>();

			foreach (InvoiceItem invoiceItem in invoiceItemSet)
			{
				if (!invoiceItemVatCodes.Contains(invoiceItem.VatCode))
					invoiceItemVatCodes.Add(invoiceItem.VatCode);

				sb.Append(@"<tr>
								<td class='BorderBlack Left'>" + invoiceItem.Description +
								(invoiceItem.Discount > 0 ? " <small>@ " + invoiceItem.Discount.ToString("P2") + " discount</small>" : "") +
								@"</td>");

				if (!this.VatCode.Equals(Invoice.VATCodes.T1))
				{
					sb.Append(@"<td class='BorderBlack Left' width='55' align='left'>" + this.VatCode.ToString() + @"</td>");
				}
				else
				{
					sb.Append(@"<td class='BorderBlack Left' width='55' align='left'>" + invoiceItem.VatCode.ToString() + @"</td>");
				}

				sb.Append(@"<td class='BorderBlack Left' width='90' align='right'>" + Utilities.MoneyToHTML(invoiceItem.Price) + @"</td>
							<td class='BorderBlack Left' width='85' align='right'>" + Utilities.MoneyToHTML(invoiceItem.Vat) + @"</td>
							<td class='BorderBlack Left' width='90' align='right'>" + Utilities.MoneyToHTML(invoiceItem.Total) + @"</td>
						</tr>");
			}

			sb.Append(@"<tr>
							<td class='BorderBlack Top Left' colspan='2' align='right'><b>TOTAL:</b></td>
							<td class='BorderBlack Top Left' width='90' align='right'><b>" + Utilities.MoneyToHTML(this.Price) + @"</b></td>
							<td class='BorderBlack Top Left' width='85' align='right'><b>" + Utilities.MoneyToHTML(this.Vat) + @"</b></td>
							<td class='BorderBlack Top Left' width='90' align='right'><b>" + Utilities.MoneyToHTML(this.Total) + @"</b></td>
						</tr></table>");

			InvoiceItem.VATCodes[] invoiceItemVatCodeArray = invoiceItemVatCodes.ToArray();
			Array.Sort(invoiceItemVatCodeArray);

			sb.Append("<small><i><b>VAT Rate:</b> ");
			if (this.VatCode.Equals(Invoice.VATCodes.T1) && invoiceItemVatCodeArray.Length > 0)
			{
				foreach (InvoiceItem.VATCodes vatCode in invoiceItemVatCodeArray)
				{
					sb.Append(vatCode.ToString());
					sb.Append("=");
					sb.Append(InvoiceItem.VATRate(vatCode, taxDate).ToString("0.0%"));
					sb.Append("&nbsp;&nbsp;");
				}
			}
			else
			{
				sb.Append(this.VatCode.ToString());
				sb.Append("=");
				sb.Append(Invoice.VATRate(this.VatCode, taxDate).ToString("0.0%"));
			}
			sb.Append("</i></small><br>");

			sb.Append("<br>");

			Query InvoiceTransferQuery = new Query(new And(new Q(InvoiceTransfer.Columns.InvoiceK, this.K),
														   new Or(new Q(Transfer.Columns.Status, Transfer.StatusEnum.Pending),
																  new Q(Transfer.Columns.Status, Transfer.StatusEnum.Success),
																  new Q(Transfer.Columns.Status, Transfer.StatusEnum.Cancelled))));
			InvoiceTransferQuery.TableElement = new Join(InvoiceTransfer.Columns.TransferK, Transfer.Columns.K);
			InvoiceTransferSet invoiceTransferSet = new InvoiceTransferSet(InvoiceTransferQuery);

			bool nonSuccessfulTransfer = false;

			if (invoiceTransferSet.Count > 0)
			{
				sb.Append(@"<br><table width='100%' cellspacing='0' cellpadding='3' class='BorderBlack Top Right Bottom'>
							<tr>
								<td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='205'><b>Transfer</b></td>
								<td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='65' align='left'><b>Date</b></td>
								<td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='75' align='left'><b>Method</b></td>
								<td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='150' align='left'><b>Method Ref#</b></td>
								<td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='90' align='left'><b>Status</b></td>
								<td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='85' align='left'><b>Amount</b></td>
							</tr>");

				foreach (InvoiceTransfer invoiceTransfer in invoiceTransferSet)
				{
					Transfer transfer = new Transfer(invoiceTransfer.TransferK);

					sb.Append(@"<tr>
								<td class='BorderBlack Left'>");
					if (linksEnabled)
						sb.Append(Utilities.Link(transfer.UrlReport(), transfer.Type.ToString() + " #" + transfer.K.ToString()));
					else
						sb.Append(transfer.Type.ToString() + " #" + transfer.K.ToString());

					sb.Append(@"</td>
								<td class='BorderBlack Left' width='65' align='left'>" + transfer.DateTimeCreated.ToString("dd/MM/yy") + @"</td>
								<td class='BorderBlack Left' width='75' align='left'><nobr>" + Utilities.CamelCaseToString(transfer.Method.ToString()) + @"</nobr></td>
								<td class='BorderBlack Left' width='150' align='left'>");

					sb.Append(transfer.ReferenceNumberToHtml());

					sb.Append(@"</td>
								<td class='BorderBlack Left' width='90' align='left'>" + transfer.Status.ToString() + @"</td>
								<td class='BorderBlack Left' width='85' align='right'>" + Utilities.MoneyToHTML(invoiceTransfer.Amount) + @"</td>
							</tr>");

					if (!transfer.Status.Equals(Transfer.StatusEnum.Success))
						nonSuccessfulTransfer = true;
					else
						amountPaid += invoiceTransfer.Amount;
				}

				sb.Append(@"</table>");

				// For now, this note only pertains to Invoices
				if (this.Type.Equals(Invoice.Types.Invoice) && nonSuccessfulTransfer == true)
					sb.Append(@"<small><i>(<b>Note:</b> Only successful transfers will be applied to the payment total. Pending and cancelled transfers will not be applied)</i></small><br>");

				sb.Append("<br>");
			}

			Query InvoiceCreditQuery = new Query();

			if (this.Type.Equals(Invoice.Types.Invoice))
			{
				InvoiceCreditQuery = new Query(new Q(InvoiceCredit.Columns.InvoiceK, this.K));
			}
			else
			{
				InvoiceCreditQuery = new Query(new Q(InvoiceCredit.Columns.CreditInvoiceK, this.K));
			}

			InvoiceCreditSet invoiceCreditSet = new InvoiceCreditSet(InvoiceCreditQuery);

			if (invoiceCreditSet.Count > 0)
			{
				string invoiceHeader = "Invoice";
				Invoice invoice = new Invoice();

				if (this.Type.Equals(Invoice.Types.Invoice))
					invoiceHeader = "Credit";

				sb.Append(@"<br><table width='100%' cellspacing='0' cellpadding='3' class='BorderBlack Top Bottom Right'>
						<tr>
							<td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='335'><b>" + invoiceHeader + @"</b></td>
							<td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='70' align='left'><b>Date</b></td>
							<td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='90' align='left'><b>Price</b></td>
							<td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='85' align='left'><b>VAT</b></td>
							<td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='90' align='left'><b>Total</b></td>
						</tr>");

				foreach (InvoiceCredit invoiceCredit in invoiceCreditSet)
				{
					if (this.Type.Equals(Invoice.Types.Invoice))
					{
						// Credit amounts are negative
						amountPaid -= invoiceCredit.Amount;

						invoice = new Invoice(invoiceCredit.CreditInvoiceK);
					}
					else
						invoice = new Invoice(invoiceCredit.InvoiceK);

					sb.Append(@"<tr>
							<td class='BorderBlack Left'>");
					if (linksEnabled)
						sb.Append(Utilities.Link(invoice.UrlReport(), invoiceHeader + " #" + invoice.K.ToString()));
					else
						sb.Append(invoiceHeader + " #" + invoice.K.ToString());

					// Replacing CreatedDateTime with TaxDateTime, as per Gee's request for OASIS v1.5
					sb.Append(@"</td>
							<td class='BorderBlack Left' width='70' align='left'>" + invoice.TaxDateTime.ToString("dd/MM/yy") + @"</td>
							<td class='BorderBlack Left' width='90' align='right'>" + Utilities.MoneyToHTML(invoice.Price) + @"</td>
							<td class='BorderBlack Left' width='85' align='right'>" + Utilities.MoneyToHTML(invoice.Vat) + @"</td>
							<td class='BorderBlack Left' width='90' align='right'>" + Utilities.MoneyToHTML(invoice.Total) + @"</td>
						</tr>");
				}
				sb.Append(@"</table><br><br>");
			}

			if (this.Type.Equals(Invoice.Types.Invoice))
			{
				// Invoice Summary
				sb.Append(@"<br><table width='250'>
							<tr>
								<td colspan=2><b>Summary</b></td>
							</tr>
							<tr>
								<td width='135'>Invoice&nbsp;Total:</td>
								<td width='115' align='right'>" + Utilities.MoneyToHTML(this.Total) + @"</td>
							</tr>
							<tr>
								<td width='135'>Payment&nbsp;Total:</td>
								<td width='115' align='right'>" + Utilities.MoneyToHTML(amountPaid) + @"</td>
							</tr>
							<tr>
								<td width='135'><b>Outstanding:</b></td>                                
								<td width='115' align='right'><b>" + Utilities.MoneyToHTML(this.Total - amountPaid) + @"</b></td>
							</tr></table><br><br>");
			}

			sb.Append(@"</td></tr>");

			sb.Append(@"
<tr>
	<td valign='bottom' align='center'>
		<div style='width:50%;padding:20px;border:2px solid #000000;text-align:left;'>
			Bank details for payments:<br>
			Development Hell Limited<br>
			Barlcays Bank PLC, Commercial Bank Basingstoke<br>
			Sort Code: 20-37-63<br>
			Account number: 00478377<br>
			For international payments:<br>
			IBAN - GB04BARC20376300478377<br>
			Swift - BARCGB22
		</div>
	</td>
</tr>");

			// DSI Registration Footer
			sb.Append(Utilities.GenerateHTMLFooterRowString());

			sb.Append(@"</table></div></form>");

			return sb;
		}
		#endregion

		#region SetUsrAndActionUsr
		public void SetUsrAndActionUsr(Usr CurrentUsr)
		{
			SetUsrAndActionUsr(CurrentUsr, true);
		}
		public void SetUsrAndActionUsr(Usr CurrentUsr, bool overrideExisting)
		{
			if (this.PromoterK > 0)
			{
				if (CurrentUsr.IsPromoterK(this.PromoterK))
					this.UsrK = CurrentUsr.K;
				else if (this.Promoter != null && this.Promoter.PrimaryUsrK != 0)
				{
					// For instance when an admin does it on a promoter's behalf
					this.UsrK = this.Promoter.PrimaryUsrK;
				}
				else
					this.UsrK = CurrentUsr.K;
			}
			// If there is no Promoter
			else if (overrideExisting || this.UsrK == 0)
			{
				this.UsrK = CurrentUsr.K;
			}
			if (overrideExisting || this.ActionUsrK == 0)
				this.ActionUsrK = CurrentUsr.K;
		}
		#endregion

		#region Process Invoice and Invoice Items
		public bool IsReadyForProcessing()
		{
			foreach (InvoiceItem ii in this.Items)
			{
				if (ii.BuyableObject != null && !ii.BuyableObject.IsReadyForProcessing(ii.Type, ii.Price, ii.Total))
					return false;
			}

			return true;
		}

		public void Process()
		{
			decimal prevTotal = Math.Round(this.Total, 2);
			this.UpdatePrice();
			if (this.Total != prevTotal)
			{
				Utilities.AdminEmailAlert("<p>Invoice (K=" + this.K.ToString() + ") not processed - price wrong</p><p>Calculated total " + this.Total.ToString("c") + " != expected total " + prevTotal.ToString("c") + "</p>",
										  "Invoice (K=" + this.K.ToString() + ") not processed - price wrong", new DsiUserFriendlyException("Price wrong"), this);
			}
			else
			{
				foreach (InvoiceItem ii in Items)
				{
					try
					{
						ii.Process();
					}
					catch (Exception ex)
					{
						Global.Log("8a721961-5135-408c-8abc-8cdebf1eb713", ex);
						Utilities.AdminEmailAlert("<p>Exception occurred while processing Invoice (K=" + this.K.ToString() + ")</p><p>InvoiceItem (K=" + ii.K.ToString() + "): " + ii.Description + "</p>",
												  "Exception occurred while processing Invoice (K=" + this.K.ToString() + ")", ex, this);
					}
				}

				this.AssignSalesUsrAndAmount();
			}
		}

		public void Unprocess()
		{
			foreach (InvoiceItem ii in Items)
			{
				try
				{
					ii.Unprocess();
				}
				catch (Exception ex)
				{
					Global.Log("8a721961-5135-408c-8abc-8cdebf1eb713", ex);
					Utilities.AdminEmailAlert("<p>Exception occurred while unprocessing Invoice (K=" + this.K.ToString() + ")</p><p>InvoiceItem (K=" + ii.K.ToString() + "): " + ii.Description + "</p>",
											  "Exception occurred while unprocessing Invoice (K=" + this.K.ToString() + ")", ex, this);
				}
			}
		}

		public bool AreAllItemsProcessed
		{
			get
			{
				foreach (InvoiceItem ii in this.Items)
				{
					if (ii.BuyableObject != null && !ii.BuyableObject.IsProcessed(ii.Type))
						return false;
				}
				return true;
			}
		}
		#endregion

		#region PaymentTypes
		#endregion

		#region Status
		/// <summary>
		/// Status
		/// </summary>
		public Invoice.Statuses Status
		{
			get
			{
				if (this.Paid == true)
					return Invoice.Statuses.Paid;
				else
				{
					if (new DateTime(DueDateTime.Year, DueDateTime.Month, DueDateTime.Day) < new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day))
						return Invoice.Statuses.Overdue;
					else
						return Invoice.Statuses.Outstanding;
				}
			}
		}
		/// <summary>
		/// Statuses Enum (Paid, Outstanding, Overdue)
		/// </summary>
		#endregion

		#region Types

		public string TypeToString
		{
			get
			{
				if (this.K > 0 && this.PromoterK == 0 && this.UsrK > 0 && this.Type.Equals(Invoice.Types.Invoice))
				{
					return "Receipt";
				}
				else
					return this.Type.ToString();
			}
		}
		#endregion

		#region BuyerTypes
        public void AssignBuyerType()
        {
            if (this.BuyerType == BuyerTypes.None)
            {
                if (this.Promoter != null)
                {
                    if (this.Promoter.IsAgency)
                        this.BuyerType = BuyerTypes.AgencyPromoter;
                    else
                        this.BuyerType = BuyerTypes.NonAgencyPromoter;
                }
                else
                {
                    this.BuyerType = BuyerTypes.NonTicketUsr;
                    if (this.Items != null)
                    {
						Items.Reset();
                        foreach (InvoiceItem ii in Items)
                        {
                            if (ii.Type == InvoiceItem.Types.EventTickets)
                            {
                                this.BuyerType = BuyerTypes.TicketUsr;
                                break;
                            }
                        }
                    }
                }
            }
        }
        #endregion

		#region VATCodes
		/// <summary>
		/// T1: Normal
		/// T3: Rest of world - forces all InvoiceItems to 0%
		/// T4: EU VAT registered businesses - forces all InvoiceItems to 0%
		/// </summary>

		public static ListItem[] VATCodesAsListItemArray()
		{
			ListItem[] ListItems = new ListItem[3];
			ListItems[0] = new ListItem(VATCodes.T1.ToString(), Convert.ToInt32(VATCodes.T1).ToString());
			ListItems[1] = new ListItem(VATCodes.T3.ToString(), Convert.ToInt32(VATCodes.T3).ToString());
			ListItems[2] = new ListItem(VATCodes.T4.ToString(), Convert.ToInt32(VATCodes.T4).ToString());

			// default VAT Code is T1
			ListItems[0].Selected = true;

			return ListItems;
		}

		// Returns the decimal value of the VAT Rate for a given VAT Code.  Ex: T1 (17.5%) returns 0.175
		public static double VATRate(VATCodes VATCode, DateTime taxDate)
		{
			if (VATCode.Equals(VATCodes.T1))
				return Vars.VatMultipT1(taxDate);
			else if (VATCode.Equals(VATCodes.T3))
				return 0;
			else if (VATCode.Equals(VATCodes.T4))
				return 0;
			else
				return 0;
		}
		#endregion

		#region UpdatePrice()
		public void UpdatePrice()
		{
			Items = null;
			decimal priceBeforeDiscount = 0.0m;
			decimal priceBeforeAgencyDiscount = 0.0m;
			decimal price = 0.0m;
			decimal vat = 0.0m;
			decimal total = 0.0m;
			
			foreach (InvoiceItem invoiceItem in Items)
			{
				priceBeforeDiscount += invoiceItem.PriceBeforeDiscount;
				priceBeforeAgencyDiscount += invoiceItem.PriceBeforeAgencyDiscount;
				price += invoiceItem.Price;
				vat += invoiceItem.Vat;
				total += invoiceItem.Total;
			}
			this.PriceBeforeDiscount = priceBeforeDiscount;
			this.PriceBeforeAgencyDiscount = priceBeforeAgencyDiscount;
			this.Price = Math.Round(price, 2);
			this.Vat = Math.Round(vat, 2);
			this.Total = Math.Round(total, 2);
			if (PriceBeforeDiscount == 0)
				this.Discount = 0;
			else
				this.Discount = 1.0 - (double)(this.PriceBeforeAgencyDiscount / this.PriceBeforeDiscount);
			
			if (PriceBeforeAgencyDiscount == 0)
				this.AgencyDiscount = 0;
			else
				this.AgencyDiscount = 1.0 - (double)(this.Price / this.PriceBeforeAgencyDiscount);

			this.Update();
		}
		#endregion

		#region UpdateAndSetPaidStatus
		public void UpdateAndSetPaidStatus()
		{
			decimal totalPaid = this.AmountPaid;

			if (Math.Round(this.Total, 2) == Math.Round(totalPaid, 2))
			{
				this.SetPaidAndPaidDateTime(true);
			}
			// If Invoice has been overpaid, remove InvoiceTransfer link amounts until totalPaid == this.Total
			else if (Math.Round(this.Total, 2) < Math.Round(totalPaid, 2) && this.Type.Equals(Invoice.Types.Invoice))
			{
				Query invoiceTransfersAppliedQuery = new Query(new And(new Q(InvoiceTransfer.Columns.InvoiceK, this.K),
																	   new Q(Transfer.Columns.Type, Transfer.TransferTypes.Payment)));
				invoiceTransfersAppliedQuery.TableElement = new Join(InvoiceTransfer.Columns.TransferK, Transfer.Columns.K);
				invoiceTransfersAppliedQuery.OrderBy = new OrderBy(Transfer.Columns.DateTimeComplete, OrderBy.OrderDirection.Ascending);

				InvoiceTransferSet invoiceTransferSet = new InvoiceTransferSet(invoiceTransfersAppliedQuery);

				string invoiceNote = "Invoice overpaid by " + ((double)(totalPaid - this.Total)).ToString("c") + ".";
				decimal amountToUnApply = 0;
				for (int i = invoiceTransferSet.Count - 1; i >= 0; i--)
				{
					if (totalPaid > this.Total)
					{
						Transfer transfer = new Transfer(invoiceTransferSet[i].TransferK);

						if (invoiceTransferSet[i].Amount <= totalPaid - this.Total)
						{
							amountToUnApply = invoiceTransferSet[i].Amount;
							invoiceTransferSet[i].Delete();
						}
						else
						{
							amountToUnApply = totalPaid - this.Total;
							invoiceTransferSet[i].Amount -= amountToUnApply;
							invoiceTransferSet[i].Update();
						}
                    //    if(transfer.Method == Transfer.Methods.TicketSales)
                     //       BankExport.GenerateBankExportForTicketFundsUsed(transfer, -1 * amountToUnApply, this);
						invoiceNote += string.Format("\nAutomatically unapplied {0} from transfer #{1}.", amountToUnApply.ToString("c"), transfer.K);
						totalPaid -= amountToUnApply;

						transfer.AddNote(string.Format("Invoice #{0} was overpaid. {1:} has been unapplied from this transfer.", this.K, amountToUnApply.ToString("c")), "System");
						transfer.IsFullyApplied = false;
						transfer.Update();
					}
				}
				this.AddNote(invoiceNote, "System");
				this.SetPaidAndPaidDateTime(true);
				//this.AddNote("This invoice has been overpaid by the sum of " + ((decimal)Math.Round(totalPaid - this.Total, 2)).ToString("0.00"), "Admin");
			}
			else
				this.SetPaidAndPaidDateTime(false);

			this.Update();
		}

		#endregion	

		#region SuccessfulInvoiceTransfers
		public InvoiceTransferSet SuccessfulInvoiceTransfers
		{
			get
			{
				//if (successfulInvoiceTransfers == null)
				//{
					// Get all successful transfers
					Query invoiceTransferQuery = new Query(new And(new Q(InvoiceTransfer.Columns.InvoiceK, this.K),
																   new Q(Transfer.Columns.Status, Transfer.StatusEnum.Success)));
					invoiceTransferQuery.TableElement = new Join(InvoiceTransfer.Columns.TransferK, Transfer.Columns.K, QueryJoinType.Inner);
					return new InvoiceTransferSet(invoiceTransferQuery);
				//}
				//return successfulInvoiceTransfers;
			}
		}

		//private InvoiceTransferSet successfulInvoiceTransfers;
		#endregion

		#region UpdateAndAutoApplySuccessfulTransfersWithAvailableMoney
		public void UpdateAndAutoApplySuccessfulTransfersWithAvailableMoney()
		{
			if (this.Type.Equals(Invoice.Types.Invoice) && this.Paid == false)
			{
				decimal amountDue = this.AmountDue;

				if (amountDue > 0)
				{
					decimal availableMoney = 0m;
					if (this.Promoter != null)
						availableMoney = this.Promoter.GetAvailableMoney();
					else if (this.Usr != null)
						availableMoney = this.Usr.GetBalance();

					// Only apply if they have positive available money
					if (Math.Round(availableMoney, 2) > 0)
					{
						Query transferQuery = new Query();
						if (this.Promoter != null)
						{
							transferQuery = new Query(new And(new Q(Transfer.Columns.IsFullyApplied, false),
															  new Q(Transfer.Columns.PromoterK, this.PromoterK),
															  new Q(Transfer.Columns.Status, Transfer.StatusEnum.Success),
															  new Q(Transfer.Columns.Type, Transfer.TransferTypes.Payment)));

                            if (this.Promoter.OverrideApplyTicketFundsToInvoices)
                            {
                                transferQuery.QueryCondition = new And(transferQuery.QueryCondition,
                                                                       new Q(Transfer.Columns.Method, QueryOperator.NotEqualTo, Transfer.Methods.TicketSales));
                            }
						}
						else if (this.Usr != null)
						{
							transferQuery = new Query(new And(new Q(Transfer.Columns.IsFullyApplied, false),
															  new Q(Transfer.Columns.PromoterK, 0),
															  new Q(Transfer.Columns.UsrK, this.UsrK),
															  new Q(Transfer.Columns.Status, Transfer.StatusEnum.Success),
															  new Q(Transfer.Columns.Type, Transfer.TransferTypes.Payment)));
						}
						transferQuery.OrderBy = new OrderBy(Transfer.Columns.DateTimeComplete, OrderBy.OrderDirection.Ascending);
						TransferSet transferSet = new TransferSet(transferQuery);

						if (transferSet.Count > 0)
						{
							bool complete = false;

							ApplyTransfersToThisInvoice(transferSet);

							this.UpdateAndSetPaidStatus();

							// Remove or decrease amount of any Pending Invoice Transfers that, if successful, would bring the amount paid over the total invoice cost
							Query invoiceTransferPendingQuery = new Query();
							invoiceTransferPendingQuery.QueryCondition = new And(new Q(InvoiceTransfer.Columns.InvoiceK, this.K),
																				 new Q(Transfer.Columns.Status, Transfer.StatusEnum.Pending));
							invoiceTransferPendingQuery.TableElement = new Join(InvoiceTransfer.Columns.TransferK, Transfer.Columns.K);

							InvoiceTransferSet invoiceTransferSet = new InvoiceTransferSet(invoiceTransferPendingQuery);
							var amountPaid = this.AmountPaid;
							var amountPending = 0m;

							foreach (InvoiceTransfer invoiceTransfer in invoiceTransferSet)
							{
								var invoiceTransferAmount = Math.Round(invoiceTransfer.Amount, 2);
								amountPending += Math.Round(invoiceTransfer.Amount, 2);

								Transfer transfer = new Transfer(invoiceTransfer.TransferK);

								if (Math.Round(amountPaid, 2) >= Math.Round(Total, 2))
								{
									invoiceTransfer.Delete();
									this.AddNote("Transfer #" + invoiceTransfer.TransferK.ToString() + " has been unapplied from this invoice", "System");
								}
								else if (Math.Round(amountPaid + amountPending, 2) > Math.Round(Total, 2))
								{
									invoiceTransfer.Amount += Math.Round((Total - amountPaid) - amountPending, 2);
									amountPending += Math.Round(invoiceTransfer.Amount - invoiceTransferAmount, 2);

									if (Math.Round(invoiceTransfer.Amount, 2) == 0)
									{
										this.AddNote("Transfer #" + invoiceTransfer.TransferK.ToString() + " has been unapplied from this invoice", "System");
										invoiceTransfer.Delete();
									}
									else
									{
										this.AddNote("Transfer #" + invoiceTransfer.TransferK.ToString() + " amount applied has been updated", "System");
										invoiceTransfer.Update();
									}
								}

								if (invoiceTransferAmount != 0 && transfer.IsFullyApplied == true)
								{
									transfer.IsFullyApplied = false;
									transfer.Update();
								}
							}
						}
					}
				}
			}
			this.UpdateAndSetPaidStatus();
		}

		public void ApplyTransfersToThisInvoice(Transfer transfer)
		{
			List<Transfer> transfers = new List<Transfer>();
			transfers.Add(transfer);
			ApplyTransfersToThisInvoice(transfers);
		}

		public void ApplyTransfersToThisInvoice(TransferSet transferSet)
		{
			List<Transfer> transfers = new List<Transfer>();
			transferSet.Reset();
			foreach (Transfer t in transferSet)
			{
				transfers.Add(t);
			}
			ApplyTransfersToThisInvoice(transfers);
		}
		public void ApplyTransfersToThisInvoice(List<Transfer> transfers)
		{
			bool complete = false;
			var amountDue = this.AmountDue;
			var availableMoney = 0m;
			if (this.Promoter != null)
				availableMoney = this.Promoter.GetAvailableMoney();
			else if (this.Usr != null)
				availableMoney = this.Usr.GetBalance();

			foreach (Transfer transfer in transfers)
			{
				decimal amountRemaining = transfer.AmountRemaining();

				// This transfer should be fully applied
				if (Math.Round(amountRemaining, 2) <= 0)
				{
					transfer.IsFullyApplied = true;
					transfer.Update();
					continue;
				}

				if (Math.Round(availableMoney, 2) <= 0)
					break;

				// As refunds can be given and not applied specifically as credits, we need to check the promoter balance so that we dont apply more money than is actually available on their account
				if (Math.Round(amountRemaining, 2) > Math.Round(availableMoney, 2))
					amountRemaining = Math.Round(availableMoney, 2);

				InvoiceTransfer invoiceTransfer;

				// If there already exists an InvoiceTransfer but for not the total amount, then update it with more ���
				try
				{
					invoiceTransfer = new InvoiceTransfer(this.K, transfer.K);
				}
				catch (Exception)
				{
					invoiceTransfer = new InvoiceTransfer();
					invoiceTransfer.InvoiceK = this.K;
					invoiceTransfer.TransferK = transfer.K;
					invoiceTransfer.Amount = 0;
				}

				if (Math.Round(amountRemaining, 2) >= Math.Round(amountDue, 2))
				{
					invoiceTransfer.Amount += Math.Round(amountDue, 2);
					complete = true;
				}
				else
				{
					invoiceTransfer.Amount += Math.Round(amountRemaining, 2);
					amountDue -= Math.Round(invoiceTransfer.Amount, 2);
					availableMoney -= Math.Round(amountRemaining, 2);
				}

				if (transfer.Method == Transfer.Methods.TicketSales)
				{
				//	decimal amountApplied = Math.Round(amountRemaining, 2) >= Math.Round(amountDue, 2) ? Math.Round(amountDue, 2) : Math.Round(amountRemaining, 2);
				//	BankExport.GenerateBankExportForTicketFundsUsed(transfer, amountApplied, this);
				}
				this.AddNote("Automatically added " + invoiceTransfer.Amount.ToString("c") + " from transfer #" + invoiceTransfer.TransferK.ToString() + " to this invoice", "System");

				if (invoiceTransfer.TransferK > 0 && invoiceTransfer.InvoiceK > 0)
					invoiceTransfer.Update();

				if (transfer.AmountRemaining() == 0)
				{
					transfer.IsFullyApplied = true;
					transfer.Update();
				}

				if (complete == true)
					break;
			}
			//return complete;
		}
		#endregion

		#region Urls
		public string UrlAdmin(params string[] par)
		{
			string[] fullParams = Cambro.Misc.Utility.JoinStringArrays(new string[] { "K", this.K.ToString() }, par);
			return UrlInfo.PageUrl(UrlInfo.PageTypes.Admin, ((Invoice.Types)this.Type).ToString() + "screen", fullParams);
		}
		public string UrlAdminCreditMe(params string[] par)
		{
			string[] fullParams = Cambro.Misc.Utility.JoinStringArrays(new string[] { "InvoiceK", this.K.ToString() }, par);
			return UrlInfo.PageUrl(UrlInfo.PageTypes.Admin, "creditscreen", fullParams);
		}
		public string UrlAdminCreateTransfer(params string[] par)
		{
			string[] fullParams = Cambro.Misc.Utility.JoinStringArrays(new string[] { "InvoiceK", this.K.ToString() }, par);
			return UrlInfo.PageUrl(UrlInfo.PageTypes.Admin, "transferscreen", fullParams);
		}
		public static string UrlAdminNewInvoice(params string[] par)
		{
			return UrlInfo.PageUrl(UrlInfo.PageTypes.Admin, "invoicescreen", par);
		}
		public string Url(params string[] par)
		{
			return UrlReport(par);
		}
		public string UrlReport(params string[] par)
		{
			string[] fullParams = Cambro.Misc.Utility.JoinStringArrays(new string[] { "K", this.K.ToString(), "type", this.Type.ToString() }, par);
			return UrlInfo.PageUrl(UrlInfo.PageTypes.Blank, "reportgenerator", fullParams);
		}
		#endregion

		#region Items
		public InvoiceItemSet Items
		{
			get
			{
				if (items == null)
				{
					Query q = new Query();
					q.QueryCondition = new Q(InvoiceItem.Columns.InvoiceK, this.K);
					items = new InvoiceItemSet(q);
				}
				return items;
			}
			set
			{
				items = value;
			}
		}
		private InvoiceItemSet items;
		#endregion

		#region SuccessfulAppliedTransfers
		public TransferSet SuccessfulAppliedTransfers
		{
			get
			{
				if (successfulAppliedTransfers == null)
				{
					Query q = new Query();
					q.QueryCondition = new And(new Q(InvoiceTransfer.Columns.InvoiceK, this.K),
												new Q(Transfer.Columns.Status, Transfer.StatusEnum.Success));
					q.TableElement = new Join(InvoiceTransfer.Columns.TransferK, Transfer.Columns.K);
					successfulAppliedTransfers = new TransferSet(q);
				}
				return successfulAppliedTransfers;
			}
			set
			{
				successfulAppliedTransfers = value;
			}
		}
		private TransferSet successfulAppliedTransfers;
		#endregion

		#region Credit this Invoice
		public void ApplyCreditToThisInvoice(Invoice credit)
		{
			InvoiceCredit invoiceCredit;

			try
			{
				invoiceCredit = new InvoiceCredit(this.K, credit.K);
			}
			catch (Exception)
			{
				invoiceCredit = new InvoiceCredit();
				invoiceCredit.InvoiceK = this.K;
				invoiceCredit.CreditInvoiceK = credit.K;
			}
			invoiceCredit.Amount = credit.Total;
			invoiceCredit.Update();
			this.UpdateAndSetPaidStatus();
		}

		public InvoiceDataHolder CreateCredit()
		{
			if (this.Type.Equals(Invoice.Types.Credit))
				throw new Exception("Cannot credit a credit.");

			InvoiceDataHolder credit = new InvoiceDataHolder();
			credit.PromoterK = this.PromoterK;
			credit.UsrK = this.UsrK;
			credit.VatCode = this.VatCode;
			credit.Type = Invoice.Types.Credit;
			credit.CreatedDateTime = Time.Now;

			credit.InvoiceItemDataHolderList = CreateCreditInvoiceItems();

			return credit;
		}

		private List<InvoiceItemDataHolder> CreateCreditInvoiceItems()
		{
			Query InvoiceItemQuery = new Query(new Q(InvoiceItem.Columns.InvoiceK, this.K));
			InvoiceItemSet InvoiceItems = new InvoiceItemSet(InvoiceItemQuery);
			List<InvoiceItemDataHolder> creditItems = new List<InvoiceItemDataHolder>();
			foreach (InvoiceItem ii in InvoiceItems)
			{
				InvoiceItemDataHolder creditItem = new InvoiceItemDataHolder();
				creditItem.BuyableObjectK = ii.BuyableObjectK;
				creditItem.BuyableObjectType = ii.BuyableObjectType;

				creditItem.Description = CREDIT + " INV#" + this.K.ToString() + ": " + ii.Description;
				creditItem.ShortDescription = "CRD for item #" + ii.K.ToString();
				//creditItem.Price = -1 * ii.Price;
				//creditItem.Vat = -1 * ii.Vat;
				creditItem.RevenueStartDate = DateTime.Now;
				creditItem.RevenueEndDate = DateTime.Now;
				creditItem.Type = ii.Type;
				creditItem.VatCode = ii.VatCode;
				creditItem.SetTotal(-1 * ii.Total);
				creditItems.Add(creditItem);				
			}
			return creditItems;
		}

		#region RefundTransfers
		public InvoiceSet CreditsApplied
		{
			get
			{
				if (creditsApplied == null)
				{
					// Get all successful transfers
					Query creditsAppliedQuery = new Query(new Q(InvoiceCredit.Columns.InvoiceK, this.K));
					creditsAppliedQuery.TableElement = new Join(InvoiceCredit.Columns.CreditInvoiceK, Invoice.Columns.K);

					creditsApplied = new InvoiceSet(creditsAppliedQuery);
				}
				return creditsApplied;
			}
		}
		private InvoiceSet creditsApplied;
		#endregion
		#endregion

		//#region RefundThisInvoice
		//public void RefundThisInvoice()
		//{
		//    if (this.Type == Invoice.Types.Credit)
		//    {
		//        throw new Exception("Cannot refund a credit.");
		//    }
		//    else if (this.AmountDue > 0)
		//    {
		//        throw new Exception("Cannot automate refund for an invoice with money still due.");
		//    }
		//    else
		//    {
		//        if (CreditsApplied != null && creditsApplied.Count > 0)
		//        {
		//            throw new Exception("Cannot automate refund for an invoice with a credit already applied.");
		//        }
		//        // Get all successful transfers
		//        InvoiceTransferSet invoiceTransferSet = this.SuccessfulInvoiceTransfers;
		//        foreach (InvoiceTransfer it in invoiceTransferSet)
		//        {
		//            Transfer refundTransfer = new Transfer(it.TransferK).RefundThisTransfer(it.Amount);;
					
					

		//            secPay.MakeRefund(TransferToRefund, (Guid)ViewState["DuplicateGuid"], Usr.Current.K, it.Amount);
		//            CurrentTransfer = secPay.Transfer;

		//            if (this.NotesAddOnlyTextBox.ReadOnlyTextBox.Text.Length > 0)
		//            {
		//                // Clear old notes, as they will be in the Notes textbox
		//                if (TransferToRefund.Notes.Length > 0)
		//                    CurrentTransfer.Notes = CurrentTransfer.Notes.Replace(TransferToRefund.Notes, "") + "\n";
		//                else
		//                    CurrentTransfer.Notes = "";

		//                CurrentTransfer.Notes += this.NotesAddOnlyTextBox.ReadOnlyTextBox.Text;
		//            }
		//            TransferToRefund.AddNote("This transfer has been refunded " + CurrentTransfer.Amount.ToString("c") + " on refund transfer #" + CurrentTransfer.K.ToString(), "System");
		//            TransferToRefund.UpdateAndResolveOverapplied();
		//            Utilities.EmailTransfer(TransferToRefund, false, false);
		//            refundTransfer.Update();
		//        }
		//    }
		//}
		//#endregion

		#region Links to Bob
		#region ActionUsr
		public Usr ActionUsr
		{
			get
			{
				if (actionUsr == null && ActionUsrK > 0)
					actionUsr = new Usr(ActionUsrK);
				return actionUsr;
			}
			set
			{
				actionUsr = value;
			}
		}
		private Usr actionUsr;
		#endregion
		#region Usr
		public Usr Usr
		{
			get
			{
				if (usr == null && UsrK > 0)
					usr = new Usr(UsrK);
				return usr;
			}
			set
			{
				usr = value;
			}
		}
		private Usr usr;
		#endregion
		#region Promoter
		public Promoter Promoter
		{
			get
			{
				if (promoter == null && PromoterK > 0)
					promoter = new Promoter(PromoterK);
				return promoter;
			}
			set
			{
				promoter = value;
			}
		}
		private Promoter promoter;
		#endregion
		#endregion

		#region Links to Bobset
		public InvoiceItemSet GetInvoiceItems(ColumnSet columns)
		{
			Query q = new Query();
			q.QueryCondition = new Q(InvoiceItem.Columns.InvoiceK, this.K);
			q.Columns = new ColumnSet(InvoiceItem.Columns.CustomData,
										InvoiceItem.Columns.Description,
										InvoiceItem.Columns.InvoiceK,
										InvoiceItem.Columns.ItemProcessed,
										InvoiceItem.Columns.K,
										InvoiceItem.Columns.KeyData,
										InvoiceItem.Columns.Price,
										InvoiceItem.Columns.RevenueEndDate,
										InvoiceItem.Columns.RevenueStartDate,
										InvoiceItem.Columns.Total,
										InvoiceItem.Columns.Type,
										InvoiceItem.Columns.Vat);
			return new InvoiceItemSet(q);

		}
		#endregion

		#region IReadableReference Members

		public string ReadableReference
		{
			get { return "Invoice-" + K.ToString(); }
		}

		#endregion

	}
	#endregion

	



}
