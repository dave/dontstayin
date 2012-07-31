using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace Bobs
{
	#region InvoiceItem
	/// <summary>
	/// Invoice / credit note line
	/// </summary>
	[Serializable]
	public partial class InvoiceItem 
	{

		#region simple members
		/// <summary>
		/// The primary key
		/// </summary>
		public override int K
		{
			get { return this[InvoiceItem.Columns.K] as int? ?? 0; }
			set { this[InvoiceItem.Columns.K] = value; }
		}
		/// <summary>
		/// The invoice
		/// </summary>
		public override int InvoiceK
		{
			get { return (int)this[InvoiceItem.Columns.InvoiceK]; }
			set { invoice = null; this[InvoiceItem.Columns.InvoiceK] = value; }
		}
		/// <summary>
		/// The type of the invoiceitem (Misc=0, Banner=1, etc.)
		/// </summary>
		public override Types Type
		{
			get { return (Types)this[InvoiceItem.Columns.Type]; }
			set { this[InvoiceItem.Columns.Type] = value; }
		}
		/// <summary>
		/// Key of the item (e.g. BannerK)
		/// </summary>
		public override int KeyData
		{
			get { return (int)this[InvoiceItem.Columns.KeyData]; }
			set { this[InvoiceItem.Columns.KeyData] = value; }
		}
		/// <summary>
		/// Additional data needed to enable the item on the site
		/// </summary>
		public override string CustomData
		{
			get { return (string)this[InvoiceItem.Columns.CustomData]; }
			set { this[InvoiceItem.Columns.CustomData] = value; }
		}
		/// <summary>
		/// Has the code been processed?
		/// </summary>
		public override bool ItemProcessed
		{
			get { return (bool)this[InvoiceItem.Columns.ItemProcessed]; }
			set { this[InvoiceItem.Columns.ItemProcessed] = value; }
		}
		/// <summary>
		/// Description of the item (for when there isn't a code)
		/// </summary>
		public override string Description
		{
			get { return (string)this[InvoiceItem.Columns.Description]; }
			set { this[InvoiceItem.Columns.Description] = value; }
		}
		/// <summary>
		/// Price excluding VAT
		/// </summary>
		public override decimal Price
		{
			get { return (decimal)this[InvoiceItem.Columns.Price]; }
			set { this[InvoiceItem.Columns.Price] = value;}
		}
		private void SetPrice(decimal value)
		{
			if (Price != Math.Round(value, 2))
			{
				Price = Math.Round(value, 2);
				if (this.doRecalculations) { RecalculateVat(); }
			}
		}
		/// <summary>
		/// Vat
		/// </summary>
		public override decimal Vat
		{
			get { return (decimal)this[InvoiceItem.Columns.Vat]; }
			set { this[InvoiceItem.Columns.Vat] = value; }
		}
		private void SetVat(decimal value)
		{
			if (Vat != Math.Round(value, 2))
			{
				Vat = Math.Round(value, 2);
				if (this.doRecalculations) { RecalculateTotal(); }
			}
		}
		/// <summary>
		/// Price including VAT
		/// </summary>
		public override decimal Total
		{
			get { return (decimal)this[InvoiceItem.Columns.Total]; }
			set { this[InvoiceItem.Columns.Total] = value; }
		}
		/// <summary>
		/// The revenue start date of the invoice item
		/// </summary>
		public override DateTime RevenueStartDate
		{
			get { return (DateTime)this[InvoiceItem.Columns.RevenueStartDate]; }
			set { this[InvoiceItem.Columns.RevenueStartDate] = value; }
		}
		/// <summary>
		/// The revenue end date of the invoice item
		/// </summary>
		public override DateTime RevenueEndDate
		{
			get { return (DateTime)this[InvoiceItem.Columns.RevenueEndDate]; }
			set { this[InvoiceItem.Columns.RevenueEndDate] = value; }
		}
		/// <summary>
		/// T0, T1, T4, T9. Changing this will recaclculate dependent values
		/// </summary>
		public override VATCodes VatCode
		{
			get { return (VATCodes)this[InvoiceItem.Columns.VatCode]; }
			set
			{
				if ((VATCodes)this[InvoiceItem.Columns.VatCode] != value)
				{
					this[InvoiceItem.Columns.VatCode] = value;
					if (doRecalculations) { RecalculateVat(); }
				}
			}
		}
		/// <summary>
		/// The IBuyable Bob type of that the invoiceitem points to (Banner=1, etc.)
		/// </summary>
		public override Model.Entities.ObjectType BuyableObjectType
		{
			get { return (Model.Entities.ObjectType)this[InvoiceItem.Columns.BuyableObjectType]; }
			set { this[InvoiceItem.Columns.BuyableObjectType] = value; }
		}
		/// <summary>
		/// The IBuyable ObjectType reference Key
		/// </summary>
		public override int BuyableObjectK
		{
			get { return (int)this[InvoiceItem.Columns.BuyableObjectK]; }
			set { this[InvoiceItem.Columns.BuyableObjectK] = value; }
		}
		/// <summary>
		/// Price before applying discount. Changing this will recaclculate dependent values
		/// </summary>
		public override decimal PriceBeforeDiscount
		{
			get { return (decimal)this[InvoiceItem.Columns.PriceBeforeDiscount]; }
			set
			{
				if ((decimal)this[InvoiceItem.Columns.PriceBeforeDiscount] != Math.Round(value, 2))
				{
					this[InvoiceItem.Columns.PriceBeforeDiscount] = Math.Round(value, 2);
					if (doRecalculations) { RecalculatePriceBeforeAgencyDiscount(); }
				}
			}
		}
		/// <summary>
		/// Discount to apply to this item, between 0.0 and 1.0. Changing this will recaclculate dependent values
		/// </summary>
		public override double Discount
		{
			get { return (double)this[InvoiceItem.Columns.Discount]; }
			set
			{
				if ((double)this[InvoiceItem.Columns.Discount] != value)
				{
					this[InvoiceItem.Columns.Discount] = value;
					if (doRecalculations) { RecalculatePriceBeforeAgencyDiscount(); }
				}
			}
		}
		/// <summary>
		/// Price before agency discount but after item discount has been applied. Changing this will recaclculate dependent values
		/// </summary>
		public override decimal PriceBeforeAgencyDiscount
		{
			get { return (decimal)this[InvoiceItem.Columns.PriceBeforeAgencyDiscount]; }
			set { this[InvoiceItem.Columns.PriceBeforeAgencyDiscount] = value; }
		}
		public void SetPriceBeforeAgencyDiscount(decimal value)
		{
			if (PriceBeforeAgencyDiscount != Math.Round(value, 2))
			{
				PriceBeforeAgencyDiscount = Math.Round(value, 2);
				if (doRecalculations) { RecalculatePrice(); }
			}
		}
		/// <summary>
		/// AgencyDiscount - percentage - stored between 0 and 1. Changing this will recaclculate dependent values
		/// </summary>
		public override double AgencyDiscount
		{
			get { return (double)this[InvoiceItem.Columns.AgencyDiscount]; }
			set
			{
				if ((double)this[InvoiceItem.Columns.AgencyDiscount] != value)
				{
					this[InvoiceItem.Columns.AgencyDiscount] = value;
					if (doRecalculations) { RecalculatePrice(); }
				}
			}
		}
		#endregion

		#region VATCodes

		public static ListItem[] VATCodesAsListItemArray()
		{
			ListItem[] ListItems = new ListItem[4];
			ListItems[0] = new ListItem(VATCodes.T0.ToString(), Convert.ToInt32(VATCodes.T0).ToString());
			ListItems[1] = new ListItem(VATCodes.T1.ToString(), Convert.ToInt32(VATCodes.T1).ToString());
			ListItems[2] = new ListItem(VATCodes.T2.ToString(), Convert.ToInt32(VATCodes.T2).ToString());
			ListItems[3] = new ListItem(VATCodes.T9.ToString(), Convert.ToInt32(VATCodes.T9).ToString());

			// default VAT Code is T1
			ListItems[1].Selected = true;

			return ListItems;
		}

		// Returns the decimal value of the VAT Rate for a given VAT Code.  Ex: T1 (17.5%) returns 0.175
		public static double VATRate(VATCodes VATCode, DateTime taxDate)
		{
			if (VATCode.Equals(VATCodes.T0))
				return 0;
			else if (VATCode.Equals(VATCodes.T1))
				return Vars.VatMultipT1(taxDate);
			else if (VATCode.Equals(VATCodes.T2))
				return 0;
			else if (VATCode.Equals(VATCodes.T9))
				return 0;
			else
				return 0;
		}
		#endregion

		#region TaxCode
		public int TaxCode
		{
			get
			{
				if (this.Type.Equals(Types.CharityDonation))
					return 9;
				else if (this.Invoice.PaidDateTime > new DateTime(2005, 11, 1))
					return 1;
				else
					return 0;
			}
		}
		#endregion

		#region NominalCode
		public int NominalCode
		{
			get
			{
				if (this.Type.Equals(Types.Banner))
				{
					if (this.Banner == null)
						return 4060;
					else
					{
						if (this.Banner.Position.Equals(Banner.Positions.Leaderboard))
							return 4000;
						else if (this.Banner.Position.Equals(Banner.Positions.Hotbox))
							return 4001;
						else if (this.Banner.Position.Equals(Banner.Positions.PhotoBanner))
							return 4002;
						else if (this.Banner.Position.Equals(Banner.Positions.EmailBanner))
							return 4003;
						else if (this.Banner.Position.Equals(Banner.Positions.Skyscraper))
							return 4004;
						else
							return 4060;
					}
				}
				else if (this.Type.Equals(Types.CampaignCredits))
					return 4150;
				else if (this.Type.Equals(Types.EventDonate))
					return 4201;
				else if (this.Type.Equals(Types.GuestlistCredit))
					return 4150;
				else if (this.Type.Equals(Types.Misc))
					return 4900;
				else if (this.Type.Equals(Types.UsrDonate))
					return 4200;
				else if (this.Type.Equals(Types.CharityDonation))
					return 4202;
				else if (this.Type.Equals(Types.BannerTop))
					return 4000;
				else if (this.Type.Equals(Types.BannerHotbox))
					return 4001;
				else if (this.Type.Equals(Types.BannerPhoto))
					return 4002;
				else if (this.Type.Equals(Types.BannerEmail))
					return 4003;
				else if (this.Type.Equals(Types.BannerSkyscraper))
					return 4004;
				else if (this.Type.Equals(Types.Eflyer))
					return 4005;
				else if (this.Type.Equals(Types.Design))
					return 4050;
				else if (this.Type.Equals(Types.DesignBannerJpg))
					return 4050;
				else if (this.Type.Equals(Types.DesignBannerAnimatedGif))
					return 4050;
				else if (this.Type.Equals(Types.DesignBannerFlash))
					return 4050;
				else if (this.Type.Equals(Types.OtherWebAdvertising))
					return 4060;
				else if (this.Type.Equals(Types.NonWebAdvertising))
					return 4070;
				else if (this.Type.Equals(Types.Broadcast))
					return 4100;
				else if (this.Type.Equals(Types.DsiEventTickets))
					return 4011;
				else if (this.Type.Equals(Types.EventTickets))
					return 4010;
				else if (this.Type.Equals(Types.EventTicketsBookingFee))
					return 4015;
				else if (this.Type.Equals(Types.EventTicketsDelivery))
					return 4016;
				else
					return 4900;
			}
		}
		#endregion

		#region GetTypesFromNominalCode
		public static Types[] GetTypesFromNominalCode(int nominalCode)
		{
			List<Types> TypeList = new List<Types>();

			if (nominalCode == 4201)
				TypeList.Add(Types.EventDonate);
			else if (nominalCode == 4150)
			{
				TypeList.Add(Types.CampaignCredits);
				TypeList.Add(Types.GuestlistCredit);
			}
			else if (nominalCode == 4900)
				TypeList.Add(Types.Misc);
			else if (nominalCode == 4200)
				TypeList.Add(Types.UsrDonate);
			else if (nominalCode == 4202)
				TypeList.Add(Types.CharityDonation);
			else if (nominalCode == 4000)
				TypeList.Add(Types.BannerTop);
			else if (nominalCode == 4001)
				TypeList.Add(Types.BannerHotbox);
			else if (nominalCode == 4002)
				TypeList.Add(Types.BannerPhoto);
			else if (nominalCode == 4003)
				TypeList.Add(Types.BannerEmail);
			else if (nominalCode == 4004)
				TypeList.Add(Types.BannerSkyscraper);
			else if (nominalCode == 4005)
				TypeList.Add(Types.Eflyer);
			else if (nominalCode == 4050)
			{
				TypeList.Add(Types.Design);
				TypeList.Add(Types.DesignBannerJpg);
				TypeList.Add(Types.DesignBannerAnimatedGif);
				TypeList.Add(Types.DesignBannerFlash);
			}
			else if (nominalCode == 4060)
				TypeList.Add(Types.OtherWebAdvertising);
			else if (nominalCode == 4070)
				TypeList.Add(Types.NonWebAdvertising);
			else if (nominalCode == 4100)
				TypeList.Add(Types.Broadcast);
			else if (nominalCode == 4011)
				TypeList.Add(Types.DsiEventTickets);
			else if (nominalCode == 4010)
				TypeList.Add(Types.EventTickets);
			else if (nominalCode == 4015)
				TypeList.Add(Types.EventTicketsBookingFee);
			else if (nominalCode == 4016)
				TypeList.Add(Types.EventTicketsDelivery);
			else
				TypeList.Add(Types.Misc);

			return TypeList.ToArray();
		}
		#endregion

		Invoice.VATCodes? invoiceVatCode;
		public Invoice.VATCodes InvoiceVatCode
		{
			get
			{
				if (invoiceVatCode == null)
				{
					if (this.InvoiceK == 0)
						return Invoice.VATCodes.T1;
					else
						return new Invoice(this.InvoiceK).VatCode;
				}
				else
				{
					return invoiceVatCode.Value;
				}
			}
			set
			{
				if (invoiceVatCode != value)
				{
					invoiceVatCode = value;
					if (doRecalculations) { RecalculateVat(); }
				}
			}
		}

		#region IBuyable Methods + Properties
		private IBuyable buyableObject;

		public IBuyable BuyableObject
		{
			get
			{
				if (buyableObject == null)
				{
					if (this.BuyableObjectK > 0)
					{
						buyableObject = (IBuyable)Bob.Get(this.BuyableObjectType, this.BuyableObjectK);
					}
				}
				return buyableObject;
			}
			set
			{
				buyableObject = value;
			}
		}

		/// <summary>
		/// Checks if invoice item BuyableObject != null, then check if that IBuyable Bob IsReadyForProcessing
		/// </summary>
		/// <returns>Returns false if BuyableObject != null and is not ready for processing.  Otherwise returns true</returns>
		public bool IsReadyToProcess()
		{
			if (this.BuyableObject != null)
				return this.BuyableObject.IsReadyForProcessing(this.Type, this.Price, this.Total);
			else
				return true;
		}

		public void Process()
		{
			if (!this.ItemProcessed && KeyData > 0)
			{
				if (this.BuyableObject != null)
				{
					try
					{
						this.ItemProcessed = this.BuyableObject.Process(this.Type, this.Price, this.Total);
					}
					catch (Exception ex)
					{
						IBuyableNotProcessed(ex);
					}
				}
				if (this.BuyableObject == null || this.ItemProcessed == false)
				{
					throw new Exception("Unable to process " + Utilities.CamelCaseToString(this.Type.ToString()) + " (InvoiceK=" + this.InvoiceK.ToString() + ", InvoiceItemK=" + this.K.ToString() + ", ObjectK=" + BuyableObject.K.ToString() + ")");
				}
				this.Update();
			}
		}

		public void Unprocess()
		{
			if (this.ItemProcessed)
			{
				if (KeyData > 0)
				{
					if (this.BuyableObject != null)
					{
						this.ItemProcessed = this.BuyableObject.Unprocess(this.Type);
					}

					if (this.BuyableObject == null || this.ItemProcessed == true)
					{
						throw new Exception("Unable to unprocess " + Utilities.CamelCaseToString(this.Type.ToString()) + " (InvoiceK=" + this.InvoiceK.ToString() + ", InvoiceItemK=" + this.K.ToString() + ", ObjectK=" + BuyableObject.K.ToString() + ")");
					}
				}
				else
				{
					this.ItemProcessed = false;
				}
				this.Update();
			}
		}

		private void IBuyableNotProcessed(Exception ex)
		{
			Utilities.AdminEmailAlert("<p>" + Utilities.CamelCaseToString(this.Type.ToString()) + " (InvoiceK=" + this.InvoiceK.ToString() + ", InvoiceItemK=" + this.K.ToString() + ", ObjectK=" + BuyableObject.K.ToString() + ") not processed</p>",
								Utilities.CamelCaseToString(this.Type.ToString()) + " not processed - " + ex.Message, ex);
		}
		#endregion

		#region RecalculationFunctions
		bool doRecalculations = true;
		private void RecalculatePriceBeforeAgencyDiscount()
		{
			if (doRecalculations)
			{
				this.SetPriceBeforeAgencyDiscount(this.PriceBeforeDiscount * (decimal)(1.0 - this.Discount));
			}
		}

		private void RecalculatePrice()
		{
			if (doRecalculations)
			{
				this.SetPrice(this.PriceBeforeAgencyDiscount * (decimal)(1.0 - this.AgencyDiscount));
			}
		}

		private void RecalculateVat()
		{
			DateTime taxDate = DateTime.Now;
			if (this.Invoice != null && this.Invoice.TaxDateTime != null && this.Invoice.TaxDateTime > DateTime.MinValue)
				taxDate = this.Invoice.TaxDateTime;


			if (doRecalculations)
			{
				var vatRate = InvoiceItem.VATRate(this.VatCode, taxDate);
				if (!InvoiceVatCode.Equals(Invoice.VATCodes.T1))
					vatRate = Invoice.VATRate(InvoiceVatCode, taxDate);
				this.SetVat(this.Price * (decimal)vatRate);
			}
		}

		private void RecalculateTotal()
		{
			if (doRecalculations)
			{
				Total = this.Vat + this.Price;
			}
		}
		#endregion

		

		#region SetTotal(double total)
		

	
		/// <summary>
		/// Set total and recalculate other property values appropriately
		/// </summary>
		/// <param name="total"></param>
		public void SetTotal(decimal total)
		{
			DateTime taxDate = DateTime.Now;
			if (this.Invoice != null && this.Invoice.TaxDateTime != null && this.Invoice.TaxDateTime > DateTime.MinValue)
				taxDate = this.Invoice.TaxDateTime;

			total = Math.Round(total, 2);
			this.doRecalculations = false;
			if (!(total == 0 && Math.Round(this.Price * (decimal)(1 - this.Discount) * (decimal)(1 - this.AgencyDiscount), 2) == 0))
			{
				this.Total = total;
				double vatRate = InvoiceItem.VATRate(this.VatCode, taxDate);
				if (InvoiceVatCode != Invoice.VATCodes.T1)
				{
					vatRate = Invoice.VATRate(InvoiceVatCode, taxDate);
				}
				this.SetPrice(this.Total / (decimal)(vatRate + 1.0));
				this.SetVat(this.Total - this.Price);
				this.PriceBeforeAgencyDiscount = (this.Price == 0) ? 0 : this.Price / (decimal)(1.0 - AgencyDiscount);
				this.PriceBeforeDiscount = (this.PriceBeforeAgencyDiscount == 0) ? 0 : this.PriceBeforeAgencyDiscount / (decimal)(1.0 - Discount);
			}
			this.doRecalculations = true;
		}
		#endregion

		#region Types
		static List<ListItem> typesAsListItemArray = null;
		public static ListItem[] TypesAsListItemArray()
		{
			if (typesAsListItemArray == null)
			{
				typesAsListItemArray = new List<ListItem>();
				foreach (var item in Enum.GetValues(typeof(Types)))
				{
					typesAsListItemArray.Add
					(
						new ListItem
						(
							Utilities.CamelCaseToString(((Types) item).ToString()), 
							((int)item).ToString()
						)
					);
				}
			}
			return typesAsListItemArray.ToArray();
			//List<ListItem> ListItems = new List<ListItem>();
			
			//ListItems.Add(new ListItem(Utilities.CamelCaseToString(Types.BannerEmail.ToString()), Convert.ToInt32(Types.BannerEmail).ToString()));
			//ListItems.Add(new ListItem(Utilities.CamelCaseToString(Types.BannerHotbox.ToString()), Convert.ToInt32(Types.BannerHotbox).ToString()));
			//ListItems.Add(new ListItem(Utilities.CamelCaseToString(Types.BannerPhoto.ToString()), Convert.ToInt32(Types.BannerPhoto).ToString()));
			//ListItems.Add(new ListItem(Utilities.CamelCaseToString(Types.BannerSkyscraper.ToString()), Convert.ToInt32(Types.BannerSkyscraper).ToString()));
			//ListItems.Add(new ListItem(Utilities.CamelCaseToString(Types.BannerTop.ToString()), Convert.ToInt32(Types.BannerTop).ToString()));
			//ListItems.Add(new ListItem(Types.Broadcast.ToString(), Convert.ToInt32(Types.Broadcast).ToString()));
			//ListItems.Add(new ListItem(Utilities.CamelCaseToString(Types.CampaignCredits.ToString()), Convert.ToInt32(Types.CampaignCredits).ToString()));
			//ListItems.Add(new ListItem(Types.Design.ToString(), Convert.ToInt32(Types.Design).ToString()));
			//ListItems.Add(new ListItem(Utilities.CamelCaseToString(Types.DesignBannerJpg.ToString()), Convert.ToInt32(Types.DesignBannerJpg).ToString()));
			//ListItems.Add(new ListItem(Utilities.CamelCaseToString(Types.DesignBannerAnimatedGif.ToString()), Convert.ToInt32(Types.DesignBannerAnimatedGif).ToString()));
			//ListItems.Add(new ListItem(Utilities.CamelCaseToString(Types.DesignBannerFlash.ToString()), Convert.ToInt32(Types.DesignBannerFlash).ToString()));

			//ListItems.Add(new ListItem(Utilities.CamelCaseToString(Types.DsiEventTickets.ToString()), Convert.ToInt32(Types.DsiEventTickets).ToString()));
			//ListItems.Add(new ListItem(Utilities.CamelCaseToString(Types.Eflyer.ToString()), Convert.ToInt32(Types.Eflyer).ToString()));
			//ListItems.Add(new ListItem(Utilities.CamelCaseToString(Types.EventDonate.ToString()), Convert.ToInt32(Types.EventDonate).ToString()));
			//ListItems.Add(new ListItem(Utilities.CamelCaseToString(Types.EventTickets.ToString()), Convert.ToInt32(Types.EventTickets).ToString()));
			//ListItems.Add(new ListItem(Utilities.CamelCaseToString(Types.EventTicketsBookingFee.ToString()), Convert.ToInt32(Types.EventTicketsBookingFee).ToString()));
			//ListItems.Add(new ListItem(Utilities.CamelCaseToString(Types.EventTicketsDelivery.ToString()), Convert.ToInt32(Types.EventTicketsDelivery).ToString()));
			//ListItems.Add(new ListItem(Utilities.CamelCaseToString(Types.GuestlistCredit.ToString()), Convert.ToInt32(Types.GuestlistCredit).ToString()));
			//ListItems.Add(new ListItem(Types.Misc.ToString(), Convert.ToInt32(Types.Misc).ToString()));
			//ListItems.Add(new ListItem(Utilities.CamelCaseToString(Types.NonWebAdvertising.ToString()), Convert.ToInt32(Types.NonWebAdvertising).ToString()));
			//ListItems.Add(new ListItem(Utilities.CamelCaseToString(Types.OtherWebAdvertising.ToString()), Convert.ToInt32(Types.OtherWebAdvertising).ToString()));
			//ListItems.Add(new ListItem(Utilities.CamelCaseToString(Types.UsrDonate.ToString()), Convert.ToInt32(Types.UsrDonate).ToString()));
			//ListItems.Add(new ListItem("<" + Types.Banner.ToString() + " : Obsolete>", Convert.ToInt32(Types.Banner).ToString()));

			//return ListItems.ToArray();
		}

		public static bool DoesTypeHaveRevenueDateRange(InvoiceItem.Types invoiceItemType)
		{
			switch (invoiceItemType)
			{
				case Types.CharityDonation:
				case Types.Design:
				case Types.DesignBannerAnimatedGif:
				case Types.DesignBannerFlash:
				case Types.DesignBannerJpg:
				case Types.DsiEventTickets:
				case Types.EventDonate:
				case Types.EventTickets:
				case Types.EventTicketsBookingFee:
				case Types.EventTicketsDelivery:
				case Types.GuestlistCredit:
				case Types.UsrDonate:
					return false;

				default:
					return true;
			}
		}

		#endregion

		#region AdminUrl
		public string AdminUrl
		{
			get
			{
				try
				{
					if (KeyData > 0)
					{
						if (InvoiceItem.BaseType(Type).Equals(InvoiceItem.Types.Banner))
						{
							Banner b = new Banner(KeyData);
							return b.Url();
						}
						else if (Type.Equals(Types.UsrDonate))
						{
							Usr u = new Usr(KeyData);
							return u.Url();
						}
						else if (Type.Equals(Types.EventDonate))
						{
							Event e = new Event(KeyData);
							return e.Url();
						}
						else if (Type.Equals(Types.GuestlistCredit))
						{
							GuestlistCredit glc = new GuestlistCredit(KeyData);
							return glc.Promoter.UrlApp("guestlists");
						}
						else
							return "/admin/blank";
					}
					else
						return "/admin/blank";
				}
				catch
				{
					return "/admin/blank";
				}

			}
		}
		#endregion

		#region BaseType
		public static InvoiceItem.Types BaseType(Types type)
		{
			if (type.Equals(Types.Banner) ||
					type.Equals(Types.BannerEmail) ||
					type.Equals(Types.BannerHotbox) ||
					type.Equals(Types.BannerPhoto) ||
					type.Equals(Types.BannerSkyscraper) ||
				type.Equals(Types.BannerTop))
				return Types.Banner;
			else if (type.Equals(Types.Design) ||
				type.Equals(Types.DesignBannerJpg) ||
				type.Equals(Types.DesignBannerAnimatedGif) ||
				type.Equals(Types.DesignBannerFlash))
				return Types.Design;
			else
				return Types.Misc;
		}
		#endregion

		#region DoesApplyToSalesUsrAmount
		public bool DoesApplyToSalesUsrAmount
		{
			get
			{
				return DoesItemApplyToSalesUsrAmount(this.Type);
			}
		}

		public static bool DoesItemApplyToSalesUsrAmount(InvoiceItem.Types invoiceItemType)
		{
			return invoiceItemType.Equals(InvoiceItem.Types.EventDonate) ||
				   invoiceItemType.Equals(InvoiceItem.Types.GuestlistCredit) ||
				   invoiceItemType.Equals(InvoiceItem.Types.BannerTop) ||
				   invoiceItemType.Equals(InvoiceItem.Types.BannerHotbox) ||
				   invoiceItemType.Equals(InvoiceItem.Types.BannerPhoto) ||
				   invoiceItemType.Equals(InvoiceItem.Types.BannerEmail) ||
				   invoiceItemType.Equals(InvoiceItem.Types.OtherWebAdvertising) ||
				   invoiceItemType.Equals(InvoiceItem.Types.NonWebAdvertising) ||
				   invoiceItemType.Equals(InvoiceItem.Types.BannerSkyscraper) ||
				   invoiceItemType.Equals(InvoiceItem.Types.Eflyer) ||
				   invoiceItemType.Equals(InvoiceItem.Types.CampaignCredits);

		}

		public static Q ApplyToSalesUsrAmountQ
		{
			get
			{
				return new Bobs.InListQ(InvoiceItem.Columns.Type,
					InvoiceItem.Types.EventDonate,
					InvoiceItem.Types.GuestlistCredit,
					InvoiceItem.Types.BannerTop,
					InvoiceItem.Types.BannerHotbox,
					InvoiceItem.Types.BannerPhoto,
					InvoiceItem.Types.BannerEmail,
					InvoiceItem.Types.OtherWebAdvertising,
					InvoiceItem.Types.NonWebAdvertising,
					InvoiceItem.Types.BannerSkyscraper,
					InvoiceItem.Types.Eflyer,
					InvoiceItem.Types.CampaignCredits);
			}
		}
		#endregion

		#region Links to Bob
		#region Invoice
		public Invoice Invoice
		{
			get
			{
				if (invoice == null && InvoiceK > 0)
					invoice = new Invoice(InvoiceK, this, InvoiceItem.Columns.InvoiceK);
				return invoice;
			}
			set
			{
				invoice = value;
			}
		}
		private Invoice invoice;
		#endregion
		#region Banner
		public Banner Banner
		{
			get
			{
				if (banner == null && KeyData > 0 && InvoiceItem.BaseType(this.Type).Equals(InvoiceItem.Types.Banner))
					banner = new Banner(KeyData, this, Columns.KeyData);
				return banner;
			}
			set
			{
				banner = value;
			}
		}
		private Banner banner;
		#endregion
		#endregion


	}

	#endregion

}
