using System;
using System.Collections.Generic;
using System.Text;

namespace Bobs.DataHolders
{
	/// This class is automatically-generated from the database. The contents 
	/// should be copied into the correct DataHolder class and modified to suit. You'll 
	/// probably have to change some int types to enum's etc.
	#region InvoiceItemDataHolder
	/// <summary>
	/// Invoice / credit note lineDataHolder
	/// </summary>
	[Serializable]
	public partial class InvoiceItemDataHolder: DataHolder<InvoiceItem>
	{
		
		[NonSerializedAttribute]
		InvoiceItem bob;
		#region Constructors
		public InvoiceItemDataHolder()
		{
			this.dataHolder = new InvoiceItem();
		}
		
		public InvoiceItemDataHolder(InvoiceItem invoiceItem)
			: this()
		{
			this[Bobs.InvoiceItem.Columns.K] = invoiceItem[Bobs.InvoiceItem.Columns.K];
			this[Bobs.InvoiceItem.Columns.InvoiceK] = invoiceItem[Bobs.InvoiceItem.Columns.InvoiceK];
			this[Bobs.InvoiceItem.Columns.Type] = invoiceItem[Bobs.InvoiceItem.Columns.Type];
			this[Bobs.InvoiceItem.Columns.KeyData] = invoiceItem[Bobs.InvoiceItem.Columns.KeyData];
			this[Bobs.InvoiceItem.Columns.CustomData] = invoiceItem[Bobs.InvoiceItem.Columns.CustomData];
			this[Bobs.InvoiceItem.Columns.ItemProcessed] = invoiceItem[Bobs.InvoiceItem.Columns.ItemProcessed];
			this[Bobs.InvoiceItem.Columns.Description] = invoiceItem[Bobs.InvoiceItem.Columns.Description];
			this[Bobs.InvoiceItem.Columns.Price] = invoiceItem[Bobs.InvoiceItem.Columns.Price];
			this[Bobs.InvoiceItem.Columns.Vat] = invoiceItem[Bobs.InvoiceItem.Columns.Vat];
			this[Bobs.InvoiceItem.Columns.Total] = invoiceItem[Bobs.InvoiceItem.Columns.Total];
			this[Bobs.InvoiceItem.Columns.RevenueStartDate] = invoiceItem[Bobs.InvoiceItem.Columns.RevenueStartDate];
			this[Bobs.InvoiceItem.Columns.RevenueEndDate] = invoiceItem[Bobs.InvoiceItem.Columns.RevenueEndDate];
			this[Bobs.InvoiceItem.Columns.VatCode] = invoiceItem[Bobs.InvoiceItem.Columns.VatCode];
			this[Bobs.InvoiceItem.Columns.BuyableObjectType] = invoiceItem[Bobs.InvoiceItem.Columns.BuyableObjectType];
			this[Bobs.InvoiceItem.Columns.BuyableObjectK] = invoiceItem[Bobs.InvoiceItem.Columns.BuyableObjectK];
			this[Bobs.InvoiceItem.Columns.PriceBeforeDiscount] = invoiceItem[Bobs.InvoiceItem.Columns.PriceBeforeDiscount];
			this[Bobs.InvoiceItem.Columns.PriceBeforeAgencyDiscount] = invoiceItem[Bobs.InvoiceItem.Columns.PriceBeforeAgencyDiscount];
			this[Bobs.InvoiceItem.Columns.Discount] = invoiceItem[Bobs.InvoiceItem.Columns.Discount];
			this[Bobs.InvoiceItem.Columns.AgencyDiscount] = invoiceItem[Bobs.InvoiceItem.Columns.AgencyDiscount];
			//this.SetTotal(invoiceItem.Total);
		}
		#endregion
		#region Wrapped method/properties
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
		/// The invoice
		/// </summary>
		public int InvoiceK
		{
			get { return dataHolder.InvoiceK; }
			set { this.dataHolder.InvoiceK = value; }
		}
		/// <summary>
		/// The type of the invoiceitem (Misc=0, Banner=1, etc.)
		/// </summary>
		public InvoiceItem.Types Type
		{
			get { return dataHolder.Type; }
			set { this.dataHolder.Type = value; }
		}
		/// <summary>
		/// Key of the item (e.g. BannerK)
		/// </summary>
		public int KeyData
		{
			get { return dataHolder.KeyData; }
			set { this.dataHolder.KeyData = value; }
		}
		/// <summary>
		/// Additional data needed to enable the item on the site
		/// </summary>
		public string CustomData
		{
			get { return dataHolder.CustomData; }
			set { this.dataHolder.CustomData = value; }
		}
		/// <summary>
		/// Has the code been processed?
		/// </summary>
		public bool ItemProcessed
		{
			get { return dataHolder.ItemProcessed; }
			set { this.dataHolder.ItemProcessed = value; }
		}
		/// <summary>
		/// Description of the item (for when there isn't a code)
		/// </summary>
		public string Description
		{
			get { return dataHolder.Description; }
			set { this.dataHolder.Description = value; }
		}
		/// <summary>
		/// Price excluding VAT (+ve for invoices, -ve for credits)
		/// </summary>
		public decimal Price
		{
			get { return dataHolder.Price; }
			//set { this.dataHolder.Price = value; }
		}
		/// <summary>
		/// Vat (+ve for invoices, -ve for credits)
		/// </summary>
		public decimal Vat
		{
			get { return dataHolder.Vat; }
			//set { this.dataHolder.Vat = value; }
		}
		/// <summary>
		/// Price including VAT (+ve for invoices, -ve for credits)
		/// </summary>
		public decimal Total
		{
			get { return dataHolder.Total; }
			//set { this.dataHolder.Total = value; }
		}
		/// <summary>
		/// The revenue start date of the invoice item
		/// </summary>
		public DateTime RevenueStartDate
		{
			get { return dataHolder.RevenueStartDate; }
			set { this.dataHolder.RevenueStartDate = value; }
		}
		/// <summary>
		/// The revenue end date of the invoice item
		/// </summary>
		public DateTime RevenueEndDate
		{
			get { return dataHolder.RevenueEndDate; }
			set { this.dataHolder.RevenueEndDate = value; }
		}
		/// <summary>
		/// T0, T1, T9
		/// </summary>
		public InvoiceItem.VATCodes VatCode
		{
			get { return dataHolder.VatCode; }
			set { this.dataHolder.VatCode = value; }
		}
		/// <summary>
		/// The IBuyable Bob type of that the invoiceitem points to (Banner=1, etc.)
		/// </summary>
		public Model.Entities.ObjectType BuyableObjectType
		{
			get { return dataHolder.BuyableObjectType; }
			set { this.dataHolder.BuyableObjectType = value; }
		}
		/// <summary>
		/// The IBuyable ObjectType reference Key
		/// </summary>
		public int BuyableObjectK
		{
			get { return dataHolder.BuyableObjectK; }
			set { this.dataHolder.BuyableObjectK = value; }
		}
		/// <summary>
		/// Price before applying discount
		/// </summary>
		public decimal PriceBeforeDiscount
		{
			get { return dataHolder.PriceBeforeDiscount; }
			set { this.dataHolder.PriceBeforeDiscount = value; }
		}
		/// <summary>
		/// Discount to apply to this item, between 0.0 and 1.0
		/// </summary>
		public double Discount
		{
			get { return dataHolder.Discount; }
			set { this.dataHolder.Discount = value; }
		}

		/// <summary>
		/// Price before agency discount but after item discount has been applied. Changing this will recaclculate dependent values
		/// </summary>
		public decimal PriceBeforeAgencyDiscount
		{
			get { return dataHolder.PriceBeforeAgencyDiscount; }
			set { this.dataHolder.PriceBeforeAgencyDiscount = value; }
		}

		/// <summary>
		/// AgencyDiscount - percentage - stored between 0 and 1. Changing this will recaclculate dependent values
		/// </summary>
		public double AgencyDiscount
		{
			get { return dataHolder.AgencyDiscount; }
			set { this.dataHolder.AgencyDiscount = value; }
		}
		#endregion

		public Invoice.VATCodes InvoiceVatCode
		{
			get
			{
				return dataHolder.InvoiceVatCode;
			}
			set
			{
				dataHolder.InvoiceVatCode = value;
			}
		}
		#endregion

		public InvoiceItem InvoiceItem
		{
			get
			{
				if (bob == null || dataHolder.IsDirty())
				{
					if (K > 0)
					{
						bob = new InvoiceItem(K);
					}
					else
					{
						bob = new InvoiceItem();
					}
					bob[Bobs.InvoiceItem.Columns.K] = this[Bobs.InvoiceItem.Columns.K];
					bob[Bobs.InvoiceItem.Columns.InvoiceK] = this[Bobs.InvoiceItem.Columns.InvoiceK];
					bob[Bobs.InvoiceItem.Columns.Type] = this[Bobs.InvoiceItem.Columns.Type];
					bob[Bobs.InvoiceItem.Columns.KeyData] = this[Bobs.InvoiceItem.Columns.KeyData];
					bob[Bobs.InvoiceItem.Columns.CustomData] = this[Bobs.InvoiceItem.Columns.CustomData];
					bob[Bobs.InvoiceItem.Columns.ItemProcessed] = this[Bobs.InvoiceItem.Columns.ItemProcessed];
					bob[Bobs.InvoiceItem.Columns.Description] = this[Bobs.InvoiceItem.Columns.Description];
					bob[Bobs.InvoiceItem.Columns.Price] = this[Bobs.InvoiceItem.Columns.Price];
					bob[Bobs.InvoiceItem.Columns.Vat] = this[Bobs.InvoiceItem.Columns.Vat];
					bob[Bobs.InvoiceItem.Columns.Total] = this[Bobs.InvoiceItem.Columns.Total];
					bob[Bobs.InvoiceItem.Columns.RevenueStartDate] = this[Bobs.InvoiceItem.Columns.RevenueStartDate];
					bob[Bobs.InvoiceItem.Columns.RevenueEndDate] = this[Bobs.InvoiceItem.Columns.RevenueEndDate];
					bob[Bobs.InvoiceItem.Columns.VatCode] = this[Bobs.InvoiceItem.Columns.VatCode];
					bob[Bobs.InvoiceItem.Columns.BuyableObjectType] = this[Bobs.InvoiceItem.Columns.BuyableObjectType];
					bob[Bobs.InvoiceItem.Columns.BuyableObjectK] = this[Bobs.InvoiceItem.Columns.BuyableObjectK];
					bob[Bobs.InvoiceItem.Columns.PriceBeforeDiscount] = this[Bobs.InvoiceItem.Columns.PriceBeforeDiscount];
					bob[Bobs.InvoiceItem.Columns.PriceBeforeAgencyDiscount] = this[Bobs.InvoiceItem.Columns.PriceBeforeAgencyDiscount];
					bob[Bobs.InvoiceItem.Columns.Discount] = this[Bobs.InvoiceItem.Columns.Discount];
					bob[Bobs.InvoiceItem.Columns.AgencyDiscount] = this[Bobs.InvoiceItem.Columns.AgencyDiscount];
					//bob.SetTotal(this.Total);
				}
				return bob;
			}
		}

		public string ShortDescription { get; set; }



		public InvoiceItem UpdateInsertDelete()
		{
			InvoiceItem invoiceItem = InvoiceItem;
			if (invoiceItem.InvoiceK <= 0)
			{
				throw new Exception("Invalid InvoiceK #" + invoiceItem.InvoiceK.ToString());
			}
			if (this.State == DataHolderState.Deleted)
			{
				if (invoiceItem.K > 0)
					invoiceItem.Delete();
			}
			else
			{
				invoiceItem.Update();
				this.K = invoiceItem.K;
				this.State = DataHolderState.Unchanged;
			}
			return invoiceItem;
		}

		public bool DoesApplyToSalesUsrAmount
		{
			get
			{
				return dataHolder.DoesApplyToSalesUsrAmount;
			}
		}

		//public void SetTotal(double p, Invoice.VATCodes vATCodes)
		//{
		//    dataHolder.SetTotal(p, vATCodes);
		//}

		public void SetTotal(decimal p)
		{
			dataHolder.SetTotal(p);
		}

 
	}
	#endregion
}
	
