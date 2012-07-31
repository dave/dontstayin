using System;
using System.Collections.Generic;
using System.Text;

namespace Bobs
{
	/// This class is automatically-generated from the database. The contents 
	/// should be copied into the correct Bob class and modified to suit. You'll 
	/// probably have to change some int types to enum's etc.

	#region InsertionOrderItem
	/// <summary>
	/// Corporate IOs are split up into items
	/// </summary>
	[Serializable]
	public partial class InsertionOrderItem
	{

		#region Not so Simple members - several of the values are computed and recalculate themselves when changed
		/// <summary>
		/// auto incrementing primary key
		/// </summary>
		public override int K
		{
			get { return this[InsertionOrderItem.Columns.K] as int? ?? 0; }
			set { this[InsertionOrderItem.Columns.K] = value; }
		}
		/// <summary>
		/// Insertion Order K where one exists
		/// </summary>
		public override int InsertionOrderK
		{
			get { return (int)this[InsertionOrderItem.Columns.InsertionOrderK]; }
			set { this[InsertionOrderItem.Columns.InsertionOrderK] = value; }
		}
		/// <summary>
		/// Description
		/// </summary>
		public override string Description
		{
			get { return (string)this[InsertionOrderItem.Columns.Description]; }
			set { this[InsertionOrderItem.Columns.Description] = value; }
		}
		/// <summary>
		/// BannerPosition
		/// </summary>
		public override int BannerPosition
		{
			get { return (int)this[InsertionOrderItem.Columns.BannerPosition]; }
			set
			{
				this[InsertionOrderItem.Columns.BannerPosition] = value;
				if (BannerPosition > 0)
				{
					RecalculatePriceBeforeDiscount();
				}
			}
		}
		/// <summary>
		/// ImpressionQuantity
		/// </summary>
		public override int ImpressionQuantity
		{
			get { return (int)this[InsertionOrderItem.Columns.ImpressionQuantity]; }
			set
			{
				this[InsertionOrderItem.Columns.ImpressionQuantity] = value;
				RecalculatePriceBeforeDiscount();
			}
		}
		/// <summary>
		/// Cost per mille
		/// </summary>
		public override decimal Cpm
		{
			get { return (decimal)this[InsertionOrderItem.Columns.Cpm]; }
			set
			{
				this[InsertionOrderItem.Columns.Cpm] = Math.Round(value, 2);
				RecalculatePriceBeforeDiscount();
			}
		}
		/// <summary>
		/// Discount
		/// </summary>
		public override double Discount
		{
			get { return (double)this[InsertionOrderItem.Columns.Discount]; }
			set { this[InsertionOrderItem.Columns.Discount] = value; }
		}
		public void SetDiscount(double value)
		{
			Discount = value;
			RecalculatePriceBeforeAgencyDiscount();
		}
		/// <summary>
		/// GrossCost = (Cpm * Impressions / 1000)
		/// </summary>
		public override decimal PriceBeforeDiscount
		{
			get { return (decimal)this[InsertionOrderItem.Columns.PriceBeforeDiscount]; }
			set
			{
				if (BannerPosition != 0 && value != this.Cpm * Convert.ToDecimal(this.ImpressionQuantity) /1000m)
				{
					throw new Exception("Cannot set price before discount unless BannerPosition == 0");
				}
				this[InsertionOrderItem.Columns.PriceBeforeDiscount] = Math.Round(value, 2);
				RecalculatePriceBeforeAgencyDiscount();
			}
		}
		/// <summary>
		/// DiscountedCost = (GrossCost - (1 - discount))
		/// </summary>
		public override decimal PriceBeforeAgencyDiscount
		{
			get { return (decimal)this[InsertionOrderItem.Columns.PriceBeforeAgencyDiscount]; }
			set { this[InsertionOrderItem.Columns.PriceBeforeAgencyDiscount] = value; }
		}
		private void SetPriceBeforeAgencyDiscount(decimal value)
		{
			PriceBeforeAgencyDiscount = Math.Round(value, 2);
			RecalculatePrice();
		}
		/// <summary>
		/// agency discount - copied from InsertionOrder
		/// </summary>
		public override double AgencyDiscount
		{
			get { return (double)this[InsertionOrderItem.Columns.AgencyDiscount]; }
			set { this[InsertionOrderItem.Columns.AgencyDiscount] = value; }
		}
		public void SetAgencyDiscount(double value)
		{
			this.AgencyDiscount = value;
			RecalculatePrice();
		}
		/// <summary>
		/// NetCost = (DiscountedCost * (1-agency discount)
		/// </summary>
		public override decimal Price
		{
			get { return (decimal)this[InsertionOrderItem.Columns.Price]; }
			set { this[InsertionOrderItem.Columns.Price] = value; }
		}
		private void SetPrice(decimal value)
		{
			Price = Math.Round(value, 2);
		}

		#endregion
		#region private calculation methods
		void RecalculatePriceBeforeDiscount()
		{
			if (BannerPosition == 0)
			{
				throw new Exception("Canot calculate GrossCost when BannerPosition == 0 - BannerPosition is not calculated");
			}
			this.PriceBeforeDiscount = Math.Round(Cpm * ImpressionQuantity / 1000m, 2);
		}

		void RecalculatePriceBeforeAgencyDiscount()
		{
			SetPriceBeforeAgencyDiscount(PriceBeforeDiscount * (1m - Convert.ToDecimal(Discount)));
		}

		void RecalculatePrice()
		{
			SetPrice(PriceBeforeAgencyDiscount * (decimal)(1.0 - AgencyDiscount));
		}
		


		public void UpdateWithRecalculation()
		{
			UpdateWithRecalculation(null);
		}
		public void UpdateWithRecalculation(Transaction transaction)
		{
			if (BannerPosition == 0)
			{
				RecalculatePriceBeforeAgencyDiscount();
			}
			else
			{
				RecalculatePriceBeforeDiscount();
			}
			Update(transaction);
		}
		#endregion
		public int GetCampaignCredits()
		{
			try
			{
				return Convert.ToInt32(Math.Ceiling(Banner.GetCampaignCreditsPerImpression((Banner.Positions)this.BannerPosition) * this.ImpressionQuantity));
			}
			catch
			{
				return 0;
			}
		}

	}
	#endregion

}
