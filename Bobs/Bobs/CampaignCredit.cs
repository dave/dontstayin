using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Bobs
{
	/// This class is automatically-generated from the database. The contents 
	/// should be copied into the correct Bob class and modified to suit. You'll 
	/// probably have to change some int types to enum's etc.

	#region CampaignCredit
	/// <summary>
	/// Used to track how many campaign credits each promoter has bought / spent
	/// </summary>
	[Serializable]
	public partial class CampaignCredit : IBuyable, ILinkableAdmin, IHasObjectType, IReadableReference, IAdminPage
	{

		#region Simple members
		/// <summary>
		/// auto incrementing primary key
		/// </summary>
		public override int K
		{
			get { return this[CampaignCredit.Columns.K] as int? ?? 0; }
			set { this[CampaignCredit.Columns.K] = value; }
		}
		/// <summary>
		/// the promoter
		/// </summary>
		public override int PromoterK
		{
			get { return (int)this[CampaignCredit.Columns.PromoterK]; }
			set { this[CampaignCredit.Columns.PromoterK] = value; }
		}
		/// <summary>
		/// DateTime at which the Credit addition or subtraction occurred
		/// </summary>
		public override DateTime ActionDateTime
		{
			get { return (DateTime)this[CampaignCredit.Columns.ActionDateTime]; }
			set { this[CampaignCredit.Columns.ActionDateTime] = value; }
		}
		/// <summary>
		/// the type of the linked object - InsertionOrder = 19, Banner = 16, EmailSpotlight = 20, Event = 2
		/// </summary>
		public override Model.Entities.ObjectType BuyableObjectType
		{
            get { return (Model.Entities.ObjectType)this[CampaignCredit.Columns.BuyableObjectType]; }
            set { this[CampaignCredit.Columns.BuyableObjectType] = value; }
		}
		/// <summary>
		/// the key of the linked object
		/// </summary>
		public override int BuyableObjectK
		{
            get { return (int)this[CampaignCredit.Columns.BuyableObjectK]; }
            set { this[CampaignCredit.Columns.BuyableObjectK] = value; }
		}
        /// <summary>
        /// DateTime til when this buyable object is locked
        /// </summary>
        public override DateTime BuyableLockDateTime
        {
            get { return (DateTime)this[CampaignCredit.Columns.BuyableLockDateTime]; }
            set { this[CampaignCredit.Columns.BuyableLockDateTime] = value; }
        }
        /// <summary>
        /// the invoice item type
        /// </summary>
        public override InvoiceItem.Types InvoiceItemType
        {
            get { return (InvoiceItem.Types)this[CampaignCredit.Columns.InvoiceItemType]; }
            set { this[CampaignCredit.Columns.InvoiceItemType] = value; }
        }
		/// <summary>
		/// if not linked to an object, this is the description
		/// </summary>
		public override string Description
		{
			get { return (string)this[CampaignCredit.Columns.Description]; }
			set { this[CampaignCredit.Columns.Description] = value; }
		}
		/// <summary>
		/// the credits for this item (+ve for credits being bought, -ve for credits being spent)
		/// </summary>
		public override int Credits
		{
			get { return (int)this[CampaignCredit.Columns.Credits]; }
			set { this[CampaignCredit.Columns.Credits] = value; }
		}
		/// <summary>
		/// used when buying CampaignCredits - otherwise always true
		/// </summary>
		public override bool Enabled
		{
			get { return (bool)this[CampaignCredit.Columns.Enabled]; }
			set { this[CampaignCredit.Columns.Enabled] = value; }
		}
        /// <summary>
        /// running total of the promoters balance to date, including this CampaignCredit
        /// </summary>
        public override int BalanceToDate
        {
            get { return (int)this[CampaignCredit.Columns.BalanceToDate]; }
            set { this[CampaignCredit.Columns.BalanceToDate] = value; }
        }
        /// <summary>
        /// Display ascending ordering to order records processed at the same action datetime
        /// </summary>
        public override int DisplayOrder
        {
            get { return (int)this[CampaignCredit.Columns.DisplayOrder]; }
            set { this[CampaignCredit.Columns.DisplayOrder] = value; }
        }
		/// <summary>
		/// UsrK of usr who this campaign credit was created for
		/// </summary>
		public override int UsrK
		{
			get { return (int)this[CampaignCredit.Columns.UsrK]; }
			set { this[CampaignCredit.Columns.UsrK] = value; }
		}
		/// <summary>
		/// ActionUsrK of usr who created this campaign credit
		/// </summary>
		public override int ActionUsrK
		{
			get { return (int)this[CampaignCredit.Columns.ActionUsrK]; }
			set { this[CampaignCredit.Columns.ActionUsrK] = value; }
		}
		/// <summary>
		/// Notes for this campaign credit
		/// </summary>
		public override string Notes
		{
			get { return (string)this[CampaignCredit.Columns.Notes]; }
			set { this[CampaignCredit.Columns.Notes] = value; }
		}
		/// <summary>
		/// Admin override to fix discount level for price of credits for this campaign credit
		/// </summary>
		public override double FixedDiscount
		{
			get { return (double)this[CampaignCredit.Columns.FixedDiscount]; }
			set { this[CampaignCredit.Columns.FixedDiscount] = value; }
		}
		/// <summary>
		/// Flag to indicate if price is fixed and if to use FixedDiscount
		/// </summary>
		public override bool IsPriceFixed
		{
			get { return (bool)this[CampaignCredit.Columns.IsPriceFixed]; }
			set { this[CampaignCredit.Columns.IsPriceFixed] = value; }
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

        #region Properties
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
		#region BuyableObject
		public IBob BuyableObject
        {
            get
            {
                if (buyableObject == null && BuyableObjectK > 0)
                {
					buyableObject = Bob.Get(BuyableObjectType, BuyableObjectK);
                }
                return buyableObject;
            }
        }
        private IBob buyableObject = null;
        #endregion
        #endregion

        //public static double SingleCreditPrice = 1.00;
		//public static int[] DiscountCredits = { 0, 50, 100, 250, 500, 1000, 2000 };
		//public static int[] DiscountLevels = { 0, 5, 10, 15, 20, 25, 30 };

		//#region GetDiscount(int credits, int promoterDiscount)
		//public static int GetDiscount(int credits, int promoterDiscount)
		//{
		//    var i = 0;
		//    var discountLevel = 0;
		//    while (credits >= DiscountCredits[i] && i < DiscountCredits.Length)
		//    {
		//        discountLevel = DiscountLevels[i];
		//        i++;
		//    }
		//    if (promoterDiscount > discountLevel)
		//        return promoterDiscount;
		//    else
		//        return discountLevel;
		//}
		//#endregion

		//public static double SingleCreditPrice = 1.00;
		public static int[] DiscountCredits = { 0, 50, 100, 250, 500, 1000, 2000 };
		//public static decimal[] DiscountMoneys = { 0, 47.50m, 90, 212.50m, 400, 750, 1400 };
		public static double[] DiscountLevels = { 
			Common.Settings.DiscountAt0000CreditsPercentage / 100d, 
			Common.Settings.DiscountAt0050CreditsPercentage / 100d, 
			Common.Settings.DiscountAt0100CreditsPercentage / 100d, 
			Common.Settings.DiscountAt0250CreditsPercentage / 100d, 
			Common.Settings.DiscountAt0500CreditsPercentage / 100d, 
			Common.Settings.DiscountAt1000CreditsPercentage / 100d, 
			Common.Settings.DiscountAt2000CreditsPercentage / 100d
											};


		#region GetDiscount
		public static double GetDiscountForCredits(int credits, Promoter promoter)
		{

			double discountLevel = 0.0;
			for (int i = DiscountLevels.Length - 1; i >= 0; i--)
			{
				if (credits >= DiscountCredits[i])
				{
					discountLevel = DiscountLevels[i];
					break;
				}
			}
			if ((promoter.Discount / 100.0) > discountLevel)
				return promoter.Discount / 100.0;
			else
				return discountLevel;
		}
		//public static double GetDiscountForMoney(decimal money, Promoter promoter)
		//{
		//    double discountLevel = 0.0;
		//    for (int i = DiscountLevels.Length - 1; i >= 0; i--)
		//    {
		//        if (money >= DiscountMoneys[i])
		//        {
		//            discountLevel = DiscountLevels[i];
		//            break;
		//        }
		//    }
		//    if ((promoter.Discount / 100.0) > discountLevel)
		//        return promoter.Discount / 100.0;
		//    else
		//        return discountLevel;
		//}
		#endregion

		#region Discounts
		#region FixDiscountAndUpdate
		public void FixDiscountAndUpdate(double? discount)
		{
			if (discount != null)
			{
				this.FixedDiscount = Math.Round(discount.Value, 4);
				this.IsPriceFixed = true;
			}
			else
			{
				this.FixedDiscount = 0;
				this.IsPriceFixed = false;
			}
			this.Update();
		}
		#endregion

		#region FixPriceCreditsAndUpdate
		public void FixPriceExVatCreditsAndUpdate(decimal? price)
		{
			if (price != null)
				this.FixDiscountAndUpdate(1.0 - ((double)price / (this.Credits)));
			else
				FixDiscountAndUpdate(null);
		}
		public void FixPriceIncVatCreditsAndUpdate(decimal? price)
		{
			if (price != null)
			{
				price = price / (decimal)(1 + Invoice.VATRate(Invoice.VATCodes.T1, DateTime.Now));
				this.FixDiscountAndUpdate(1 - ((double)price / this.Credits));
			}
			else
				FixDiscountAndUpdate(null);
		}
		#endregion

		public static decimal CalculateTotalCostForCredits(int credits, Promoter promoter)
		{
			return CalculateTotalCostForCredits(credits, GetDiscountForCredits(credits, promoter), promoter);
		}
		public static decimal CalculateTotalCostForCredits(int credits, double discount, Promoter promoter)
		{
			return Math.Round(credits * promoter.CostPerCampaignCredit * (decimal)(1.0 - discount), 2);
		}
		//public static int CalculateTotalCreditsForMoney(decimal money, Promoter promoter)
		//{
		//    return CalculateTotalCreditsForMoney(money, GetDiscountForMoney(money, promoter), promoter);
		//}
		public static int CalculateTotalCreditsForMoney(decimal money, double discount, Promoter promoter)
		{
			decimal costPerCredit = promoter.CostPerCampaignCredit * (decimal)(1.0 - discount);
			if(costPerCredit > 0)
				return Convert.ToInt32(Math.Ceiling(money / costPerCredit));
			else
				throw new Exception("Exception occurred in CalculateTotalCreditsForMoney. Cannot calculate cost per credit");
		}
		//public static double CalculateTotalCostForCredits(int credits, double discount)
		//{
		//    return Math.Round(credits * CostPerCredit * (1.0 - discount), 2);
		//}
		//public static double CostPerCredit
		//{
		//    get
		//    {
		//        return 1.0;
		//    }
		//}
		#endregion

        private int GetBalanceUpToButNotIncludingThisK()
        {
            Query getSumOfAllPromotersCampaignCreditsQuery = new Query(new And(new Q(CampaignCredit.Columns.PromoterK, this.PromoterK),
                                                                               new Q(CampaignCredit.Columns.Enabled, true),
                                                                               new Or(new Q(CampaignCredit.Columns.ActionDateTime, QueryOperator.LessThan, this.ActionDateTime),
                                                                                      new And(new Q(CampaignCredit.Columns.ActionDateTime, this.ActionDateTime),
                                                                                              new Q(CampaignCredit.Columns.DisplayOrder, QueryOperator.LessThan, this.DisplayOrder)))));
            getSumOfAllPromotersCampaignCreditsQuery.Columns = new ColumnSet();
            getSumOfAllPromotersCampaignCreditsQuery.ExtraSelectElements.Add("Balance", "SUM(" + GetColumnName(Columns.Credits) + ")");
            getSumOfAllPromotersCampaignCreditsQuery.GroupBy = new GroupBy(CampaignCredit.Columns.PromoterK);
            CampaignCreditSet ccs = new CampaignCreditSet(getSumOfAllPromotersCampaignCreditsQuery);
            return ccs.Count != 1 || (ccs.Count == 1 && ccs[0].ExtraSelectElements["Balance"] == DBNull.Value) ? 0 : Convert.ToInt32(ccs[0].ExtraSelectElements["Balance"]);
        }

        public void UpdateWithRecalculateBalance()
        {
            this.BalanceToDate = GetBalanceUpToButNotIncludingThisK() + this.Credits;
            this.Update();
        }

		#region SetUsrAndActionUsr
		public void SetUsrAndActionUsr(Usr CurrentUsr)
		{
			SetUsrAndActionUsr(CurrentUsr, true);
		}
		public void SetUsrAndActionUsr(Usr CurrentUsr, bool overrideExisting)
		{
			if (CurrentUsr != null)
			{
				if (this.PromoterK > 0)
				{
					if (CurrentUsr.K > 0 && CurrentUsr.IsPromoterK(this.PromoterK))
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
		}
		#endregion

		#region ILinkableAdmin
		public string TypeAndK
        {
            get
            {
                return "Campaign Credit #" + this.K.ToString();
            }
        }

		public string UrlAdmin(params string[] par)
		{
			string[] fullParams = Cambro.Misc.Utility.JoinStringArrays(new string[] { "K", this.K.ToString() }, par);
			return UrlInfo.PageUrl(UrlInfo.PageTypes.Admin, "campaigncreditscreen", fullParams);
		}
        
        public static string UrlAdminNewCampaignCredit()
        {
            return UrlInfo.PageUrl(UrlInfo.PageTypes.Admin, "campaigncreditscreen");
        }


        #endregion

        #region FriendlyLinkDescription
        public string FriendlyLinkDescription
        {
            get
            {
				if (this.BuyableObjectK == 0)
					return this.Description;

                switch (this.BuyableObjectType)
                {
					case Model.Entities.ObjectType.Banner:
                        Banner banner = new Banner(this.BuyableObjectK);
                        if (InvoiceItem.BaseType(this.InvoiceItemType) != InvoiceItem.Types.Design)
                            return "Banner " + Utilities.CamelCaseToString(banner.Position.ToString()).ToLower() + ": " + banner.LinkNewWindow();
                        else
                            return banner.DesignToString(true) + ": " + banner.LinkNewWindow();
                    case Model.Entities.ObjectType.Event: return Utilities.Link(Promoter.UrlEventOptions(this.BuyableObjectK), "Event highlight #" + this.BuyableObjectK.ToString());
					case Model.Entities.ObjectType.None: return this.Description;
                    case Model.Entities.ObjectType.Invoice: return new Invoice(this.BuyableObjectK).LinkNewWindow();
					case Model.Entities.ObjectType.GuestlistCredit: return this.Description;
                    default: return this.Description;
                }
            }
        }
        #endregion

        #region IBuyable Members

        public bool IsLocked
		{
            get
            {
                if (K == 0)
                    return false;

                Query iBuyableLockDateTimeQuery = new Query(new And(new Q(CampaignCredit.Columns.K, this.K),
                                                                    new Q(CampaignCredit.Columns.BuyableLockDateTime, QueryOperator.GreaterThanOrEqualTo, DateTime.Now.AddSeconds(-1 * Vars.IBUYABLE_LOCK_SECONDS))));
                iBuyableLockDateTimeQuery.Columns = new ColumnSet(CampaignCredit.Columns.BuyableLockDateTime);

                CampaignCreditSet lockedCampaignCreditSet = new CampaignCreditSet(iBuyableLockDateTimeQuery);
                if (lockedCampaignCreditSet.Count > 0)
                {
                    this.BuyableLockDateTime = lockedCampaignCreditSet[0].BuyableLockDateTime;
                    return true;
                }
                else
                    return false;
            }
		}

		public bool IsReadyForProcessing(InvoiceItem.Types invoiceItemType, decimal price, decimal total)
		{
            return !this.Enabled && this.BuyableObjectType == Model.Entities.ObjectType.Invoice && this.BuyableObjectK == 0 && VerifyPrice(invoiceItemType, price, total);
		}

		public bool Process(InvoiceItem.Types invoiceItemType, decimal price, decimal total) 
		{
			if (IsReadyForProcessing(invoiceItemType, price, total))
			{
                this.ActionDateTime = DateTime.Now;
				this.Enabled = true;
                this.BuyableObjectType = Model.Entities.ObjectType.Invoice;
				this.UpdateWithRecalculateBalance();

				return true;
			}
			return false;
		}

		public bool Unprocess(InvoiceItem.Types invoiceItemType)
		{
			if (IsProcessed(invoiceItemType))
			{
				this.Credits = 0;
				this.Enabled = false;
				this.Update();
			}
			return true;
		}

		public bool IsProcessed(InvoiceItem.Types invoiceItemType)
		{
            return invoiceItemType == InvoiceItem.Types.CampaignCredits && K != 0 && this.Enabled && this.Credits > 0;
		}

		public bool VerifyPrice(InvoiceItem.Types invoiceItemType, decimal price, decimal total)
		{
			return invoiceItemType == InvoiceItem.Types.CampaignCredits && price >= 0 && Math.Round(price, 2) == (this.IsPriceFixed ? CampaignCredit.CalculateTotalCostForCredits(this.Credits, this.FixedDiscount, this.Promoter) : CampaignCredit.CalculateTotalCostForCredits(this.Credits, this.Promoter));
		}

		public void Lock()
		{
            this.BuyableLockDateTime = DateTime.Now;
            this.Update();
		}

		public void Unlock()
		{
            this.BuyableLockDateTime = DateTime.MinValue;
            this.Update();
		}

		#endregion

		#region IHasObjectType Members

		public Model.Entities.ObjectType ObjectType
		{
			get { return Model.Entities.ObjectType.CampaignCredit; }
		}

		#endregion

		#region IReadableReference Members

		public string ReadableReference
		{
			get { return TypeAndK; }
		}

		#endregion

	}
	#endregion

}
