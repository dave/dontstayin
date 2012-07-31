using System;
using System.Collections.Generic;
using System.Text;
using Common;

namespace Bobs.DataHolders
{

	/// This class is automatically-generated from the database. The contents 
	/// should be copied into the correct DataHolder class and modified to suit. You'll 
	/// probably have to change some int types to enum's etc.
	#region InvoiceDataHolder
	/// <summary>
	/// Invoice or credit noteDataHolder
	/// </summary>
	[Serializable]
	public partial class InvoiceDataHolder : DataHolder<Invoice>
	{
		
		[NonSerializedAttribute]
		Invoice bob;

		public InvoiceDataHolder()
		{
			this.dataHolder = new Invoice();
			this.InvoiceItemDataHolderList = new List<InvoiceItemDataHolder>();
		}

		public InvoiceDataHolder(Invoice invoice)
			: this()
		{
			this.K = invoice.K;
			this.Type = invoice.Type;
			this.UsrK = invoice.UsrK;
			this.PromoterK = invoice.PromoterK;
			this.ActionUsrK = invoice.ActionUsrK;
			this.Name = invoice.Name;
			this.Address = invoice.Address;
			this.Postcode = invoice.Postcode;
			this.PaymentType = invoice.PaymentType;
			this.Paid = invoice.Paid;
			this.CreatedDateTime = invoice.CreatedDateTime;
			this.DueDateTime = invoice.DueDateTime;
			this.PaidDateTime = invoice.PaidDateTime;
			this.Price = invoice.Price;
			this.Vat = invoice.Vat;
			this.Total = invoice.Total;
			this.DuplicateGuid = invoice.DuplicateGuid;
			//this.Notes = invoice.Notes;
			this.VatCode = invoice.VatCode;
			this.SalesUsrK = invoice.SalesUsrK;
			this.SalesUsrAmount = invoice.SalesUsrAmount;
			this.IsImmediateCreditCardPayment = invoice.IsImmediateCreditCardPayment;
			this.TaxDateTime = invoice.TaxDateTime;
			this.PurchaseOrderNumber = invoice.PurchaseOrderNumber;
			this.InsertionOrderK = invoice.InsertionOrderK;
            this.AmountPaid = invoice.AmountPaid;

			this.InvoiceItemDataHolderList.AddRange(invoice.Items.ToList().ConvertAll(i=>new InvoiceItemDataHolder(i)));
		}

		#region Simple members
		/// <summary>
		/// The primary key
		/// </summary>
		public int K
		{
			get { return dataHolder.K; }
			set { this.dataHolder.K = value; }
		}
		/// <summary>
		/// Invoice, Credit
		/// </summary>
		public Invoice.Types Type
		{
			get { return dataHolder.Type; }
			set { this.dataHolder.Type = value; }
		}
		/// <summary>
		/// The user that created this invoice
		/// </summary>
		public int UsrK
		{
			get { return dataHolder.UsrK; }
			set { this.dataHolder.UsrK = value; }
		}
		/// <summary>
		/// The If this is a promoter invoice - this is the promoter
		/// </summary>
		public int PromoterK
		{
			get { return dataHolder.PromoterK; }
			set { this.dataHolder.PromoterK = value; }
		}
		/// <summary>
		/// Link to the user that initiated this transfer (e.g. the admin user if it's done manually!)
		/// </summary>
		public int ActionUsrK
		{
			get { return dataHolder.ActionUsrK; }
			set { this.dataHolder.ActionUsrK = value; }
		}
		/// <summary>
		/// TO BE REMOVED - Name from credit card payment control
		/// </summary>
		public string Name
		{
			get { return dataHolder.Name; }
			set { this.dataHolder.Name = value; }
		}
		/// <summary>
		/// TO BE REMOVED - First line of the address from credit card payment control
		/// </summary>
		public string Address
		{
			get { return dataHolder.Address; }
			set { this.dataHolder.Address = value; }
		}
		/// <summary>
		/// TO BE REMOVED - Postcode from credit card payment control
		/// </summary>
		public string Postcode
		{
			get { return dataHolder.Postcode; }
			set { this.dataHolder.Postcode = value; }
		}
		/// <summary>
		/// TO BE REMOVED - Payment type - 1=CreditCard, 2=Invoiced
		/// </summary>
		public Invoice.PaymentTypes PaymentType
		{
			get { return dataHolder.PaymentType; }
			set { this.dataHolder.PaymentType = value; }
		}
		/// <summary>
		/// Has this invoice been fully paid?
		/// </summary>
		public bool Paid
		{
			get { return dataHolder.Paid; }
			set { this.dataHolder.Paid = value; }
		}
		/// <summary>
		/// When was the invoice created - the tax point
		/// </summary>
		public DateTime CreatedDateTime
		{
			get { return dataHolder.CreatedDateTime; }
			set { this.dataHolder.CreatedDateTime = value; }
		}
		/// <summary>
		/// When is the invoice due to be paid (4 weeks). After this we can charge interest.
		/// </summary>
		public DateTime DueDateTime
		{
			get { return dataHolder.DueDateTime; }
			set { this.dataHolder.DueDateTime = value; }
		}
		/// <summary>
		/// When the invoice was fully paid
		/// </summary>
		public DateTime PaidDateTime
		{
			get { return dataHolder.PaidDateTime; }
			set { this.dataHolder.PaidDateTime = value; }
		}
		/// <summary>
		/// Price excluding VAT (+ve for invoices, -ve for credits)
		/// </summary>
		public decimal Price
		{
            get
            {
                this.dataHolder.Price = 0;
                foreach (InvoiceItemDataHolder iidh in this.InvoiceItemDataHolderList)
                    this.dataHolder.Price += iidh.Price;
                this.dataHolder.Price = Math.Round(this.dataHolder.Price, 2);
                return this.dataHolder.Price;
            }
            set { this.dataHolder.Price = Math.Round(value, 2); }
		}
		/// <summary>
		/// Vat (+ve for invoices, -ve for credits)
		/// </summary>
		public decimal Vat
		{
            get
            {
                this.dataHolder.Vat = 0;
                foreach (InvoiceItemDataHolder iidh in this.InvoiceItemDataHolderList)
                    this.dataHolder.Vat += iidh.Vat;
                this.dataHolder.Vat = Math.Round(this.dataHolder.Vat, 2);
                return this.dataHolder.Vat;
            }
            set { this.dataHolder.Vat = Math.Round(value, 2); }
		}
		/// <summary>
		/// Price including VAT (+ve for invoices, -ve for credits)
		/// </summary>
		public decimal Total
		{
            get
            {
                this.dataHolder.Total = 0;
                foreach (InvoiceItemDataHolder iidh in this.InvoiceItemDataHolderList)
                    this.dataHolder.Total += iidh.Total;
                this.dataHolder.Total = Math.Round(this.dataHolder.Total, 2);
                return this.dataHolder.Total;
            }
			set { this.dataHolder.Total = Math.Round(value, 2); }
		}
		/// <summary>
		/// Guid to catch duplicate "pay now" clicks
		/// </summary>
		public Guid DuplicateGuid
		{
			get { return dataHolder.DuplicateGuid; }
			set { this.dataHolder.DuplicateGuid = value; }
		}
		///// <summary>
		///// Additional Notes
		///// </summary>
		//public string Notes
		//{
		//    get { return dataHolder.Notes; }
		//    set { this.dataHolder.Notes = value; }
		//}
		/// <summary>
		/// T0, T1, T4, T9
		/// </summary>
		public Invoice.VATCodes VatCode
		{
			get { return dataHolder.VatCode; }
			set { this.dataHolder.VatCode = value; }
		}
		/// <summary>
		/// Who is the account manager for this invoice?
		/// </summary>
		public int SalesUsrK
		{
			get { return dataHolder.SalesUsrK; }
			set { this.dataHolder.SalesUsrK = value; }
		}
		/// <summary>
		/// How much is contributed to the account managers target?
		/// </summary>
		public decimal SalesUsrAmount
		{
			get { return dataHolder.SalesUsrAmount; }
			set { this.dataHolder.SalesUsrAmount = value; }
		}
		/// <summary>
		/// Flag for immediate credit card payments. This flag to be used for exports to Sage
		/// </summary>
		public bool IsImmediateCreditCardPayment
		{
			get { return dataHolder.IsImmediateCreditCardPayment; }
			set { this.dataHolder.IsImmediateCreditCardPayment = value; }
		}
		/// <summary>
		/// Tax date - to be used for exporting to Sage
		/// </summary>
		public DateTime TaxDateTime
		{
			get { return dataHolder.TaxDateTime; }
			set { this.dataHolder.TaxDateTime = value; }
		}
		/// <summary>
		/// Invoice purchase order number
		/// </summary>
		public string PurchaseOrderNumber
		{
			get { return dataHolder.PurchaseOrderNumber; }
			set { this.dataHolder.PurchaseOrderNumber = value; }
		}
		/// <summary>
		/// Used when the item is a campaign credit top-up
		/// </summary>
		public int InsertionOrderK
		{
			get { return dataHolder.InsertionOrderK; }
			set { this.dataHolder.InsertionOrderK = value; }
		}
        /// <summary>
        /// Used when the item is a campaign credit top-up
        /// </summary>

		private decimal amountPaid = 0m;
		public decimal AmountPaid
        {
            get { return amountPaid; }
            set { this.amountPaid = value; }
        }
		#endregion
		public Invoice Invoice
		{
			get
			{
				if (bob == null || dataHolder.IsDirty())
				{
					if (K > 0)
					{
						bob = new Invoice(K);
					}
					else
					{
						bob = new Invoice();
					}
					bob.K = this.K;
					if (Convert.ToInt32(this.Type) > 0)
						bob.Type = this.Type;
					else
						bob.Type = Invoice.Types.Invoice;
					bob.UsrK = this.UsrK;
					bob.PromoterK = this.PromoterK;
					bob.ActionUsrK = this.ActionUsrK;
					bob.Name = this.Name;
					bob.Address = this.Address;
					bob.Postcode = this.Postcode;
					bob.PaymentType = this.PaymentType;
					bob.Paid = this.Paid;
					bob.CreatedDateTime = this.CreatedDateTime;
					bob.DueDateTime = this.DueDateTime;
					bob.PaidDateTime = this.PaidDateTime;
					bob.Price = this.Price;
					bob.Vat = this.Vat;
					bob.Total = this.Total;
					bob.DuplicateGuid = this.DuplicateGuid;
					//bob.Notes = this.Notes;
					bob.VatCode = this.VatCode;
					bob.SalesUsrK = this.SalesUsrK;
					bob.SalesUsrAmount = this.SalesUsrAmount;
					bob.IsImmediateCreditCardPayment = this.IsImmediateCreditCardPayment;
					bob.TaxDateTime = this.TaxDateTime;
					bob.PurchaseOrderNumber = this.PurchaseOrderNumber;
					bob.InsertionOrderK = this.InsertionOrderK;
				}
				return bob;
			}
		}

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
				if (promoter != null)
					PromoterK = promoter.K;
				else
					PromoterK = 0;
			}
		}
		private Promoter promoter;
		#endregion


		public List<InvoiceItemDataHolder> InvoiceItemDataHolderList { get; set; }

		public Invoice UpdateInsertDelete()
		{
			Invoice invoice = Invoice;
			if (this.State == DataHolderState.Deleted)
			{
				if (invoice.K > 0)
				{
					invoice.Delete();
					for (int i = InvoiceItemDataHolderList.Count - 1; i >= 0; i--)
					{
						InvoiceItemDataHolderList[i].State = DataHolderState.Deleted;
						InvoiceItemDataHolderList[i].UpdateInsertDelete();
					}
				}
			}
			else
			{
				if (invoice.CreatedDateTime == DateTime.MinValue)
					invoice.CreatedDateTime = Time.Now;
				if (invoice.TaxDateTime == DateTime.MinValue)
					invoice.TaxDateTime = invoice.CreatedDateTime;
				if (invoice.Type == Invoice.Types.Invoice)
				{
					if (invoice.DueDateTime == DateTime.MinValue)
					{
						if (invoice.Promoter != null)
							invoice.DueDateTime = invoice.TaxDateTime.AddDays(invoice.Promoter.InvoiceDueDaysEffective);
						else
							invoice.DueDateTime = invoice.TaxDateTime;
					}
				}
				//invoice.AssignBuyerType();
				invoice.Update();
				foreach (InvoiceItemDataHolder iidh in this.InvoiceItemDataHolderList)
				{
					iidh.InvoiceK = invoice.K;
					iidh.UpdateInsertDelete();
				}
				invoice.Items = null;
				invoice.AssignBuyerType();
				invoice.Update();
				this.K = invoice.K;
				this.State = DataHolderState.Unchanged;
			}
			return new Invoice(this.K);
		}

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
			
			// for all invoices, if action user not set then capture whomever is performing this action
			if (overrideExisting || this.ActionUsrK == 0)
				this.ActionUsrK = Usr.Current.K;
		}

		public decimal AmountDue
		{
			get
			{
				return Math.Round(this.Total - this.AmountPaid, 2);;
			}
		}

		public bool IsReadyToProcess()
		{
			foreach (InvoiceItemDataHolder iidh in this.InvoiceItemDataHolderList)
			{
				if (iidh.BuyableObjectK > 0)
				{
					var BuyableObject = Bobs.Bob.Get(iidh.BuyableObjectType, iidh.BuyableObjectK);
					if (!((IBuyable)BuyableObject).IsReadyForProcessing(iidh.Type, iidh.Price, iidh.Total))
						return false;
				}
			}

			return true;
		}

		public string UrlReport()
		{
			return dataHolder.UrlReport();
		}
	}
	#endregion
}
