using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bobs
{
	/// This class is automatically-generated from the database. The contents 
	/// should be copied into the correct Bob class and modified to suit. You'll 
	/// probably have to change some int types to enum's etc.

	#region UsrDonationIcon
	/// <summary>
	/// Usrs having bought DonationIcons
	/// </summary>
	[Serializable]
	public partial class UsrDonationIcon : IBuyable, IHasObjectType
	{

		#region Simple members
		/// <summary>
		/// K
		/// </summary>
		public override int K
		{
			get { return (int)this[UsrDonationIcon.Columns.K] as int? ?? 0; }
			set { this[UsrDonationIcon.Columns.K] = value; }
		}
		/// <summary>
		/// UsrK who bought icon
		/// </summary>
		public override int UsrK
		{
			get { return (int)this[UsrDonationIcon.Columns.UsrK]; }
			set { this[UsrDonationIcon.Columns.UsrK] = value; }
		}
		/// <summary>
		/// DonationIcon bought
		/// </summary>
		public override int DonationIconK
		{
			get { return (int)this[UsrDonationIcon.Columns.DonationIconK]; }
			set { this[UsrDonationIcon.Columns.DonationIconK] = value; }
		}
		/// <summary>
		/// DateTime bought
		/// </summary>
		public override DateTime BuyDateTime
		{
			get { return (DateTime)this[UsrDonationIcon.Columns.BuyDateTime]; }
			set { this[UsrDonationIcon.Columns.BuyDateTime] = value; }
		}
		/// <summary>
		/// Enabled - if payment processed correctly
		/// </summary>
		public override bool Enabled
		{
			get { return (bool)this[UsrDonationIcon.Columns.Enabled]; }
			set { this[UsrDonationIcon.Columns.Enabled] = value; }
		}
		#endregion

		#region IHasObjectType Members

		public Model.Entities.ObjectType ObjectType
		{
			get { return Model.Entities.ObjectType.UsrDonationIcon; }
		}

		#endregion

		#region DonationIcon
		private DonationIcon donationIcon;
		public DonationIcon DonationIcon
		{
			get { return donationIcon ?? (donationIcon = new DonationIcon(this.DonationIconK)); }
		}
		#endregion

		#region IBuyable Members

		public bool IsLocked
		{
			get
			{
				//Query iBuyableLockDateTimeQuery = new Query(
				//    new And(new Q(UsrDonationIcon.Columns.K, this.K),
				//          new Q(UsrDonationIcon.Columns.BuyDateTime, QueryOperator.GreaterThanOrEqualTo, DateTime.Now.AddSeconds(-1 * Vars.IBUYABLE_LOCK_SECONDS))));
				//iBuyableLockDateTimeQuery.Columns = new ColumnSet(UsrDonationIcon.Columns.BuyDateTime);

				//UsrDonationIconSet lockedBuyableSet = new UsrDonationIconSet(iBuyableLockDateTimeQuery);
				//if (lockedBuyableSet.Count > 0)
				//{
				//    this.BuyableLockDateTime = lockedBuyableSet[0].BuyableLockDateTime;
				//    return true;
				//}
				//else
					return false;
			}
		}

		public DateTime BuyableLockDateTime
		{
			get { return this.BuyDateTime; }
			set { this.BuyDateTime = value; }
		}

		public bool IsReadyForProcessing(InvoiceItem.Types invoiceItemType, decimal price, decimal total)
		{
			if (VerifyPrice(invoiceItemType, price, total))
			{
				if (invoiceItemType.Equals(InvoiceItem.Types.UsrDonate) || invoiceItemType.Equals(InvoiceItem.Types.CharityDonation))
					return true;
				else
					throw new Exception("invalid invoice item type: " + Utilities.CamelCaseToString(invoiceItemType.ToString()));
			}
			else
				throw new DsiUserFriendlyException("price wrong!");
		}

		public bool Process(InvoiceItem.Types invoiceItemType, decimal price, decimal total)
		{
			if (VerifyPrice(invoiceItemType, price, total))
			{
				if (invoiceItemType.Equals(InvoiceItem.Types.UsrDonate) || invoiceItemType.Equals(InvoiceItem.Types.CharityDonation))
				{
					this.ProcessDonation();
				}
				else
					throw new Exception("invalid invoice item type: " + Utilities.CamelCaseToString(invoiceItemType.ToString()));
			}
			else
			{
				throw new Exception("price wrong!");
			}

			return IsProcessed(invoiceItemType);
		}

		public bool Unprocess(InvoiceItem.Types invoiceItemType)
		{
			if (invoiceItemType.Equals(InvoiceItem.Types.UsrDonate) || invoiceItemType.Equals(InvoiceItem.Types.CharityDonation))
			{
				this.UnprocessDonation();
			}
			else
				throw new Exception("invalid invoice item type: " + Utilities.CamelCaseToString(invoiceItemType.ToString()));

			return true;
		}

		public bool IsProcessed(InvoiceItem.Types invoiceItemType)
		{
			if (invoiceItemType.Equals(InvoiceItem.Types.UsrDonate) || invoiceItemType.Equals(InvoiceItem.Types.CharityDonation))
				return this.Enabled;
			else
				throw new Exception("invalid invoice item type: " + Utilities.CamelCaseToString(invoiceItemType.ToString()));
		}

		public bool VerifyPrice(InvoiceItem.Types invoiceItemType, decimal price, decimal total)
		{
			if (invoiceItemType.Equals(InvoiceItem.Types.UsrDonate) || invoiceItemType.Equals(InvoiceItem.Types.CharityDonation))
				return total == this.DonationIcon.Price;
			else
				throw new Exception("invalid invoice item type: " + Utilities.CamelCaseToString(invoiceItemType.ToString()));
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


		private void ProcessDonation()
		{
			if (!this.Enabled)
			{
				this.Enabled = true;
				this.BuyDateTime = DateTime.Now;
				this.Update();
			}

			var u = new Usr(this.UsrK);
			u.RolloverDonationIconK = this.DonationIconK;
			u.Update();
		}

		private void UnprocessDonation()
		{
			this.Enabled = false;
			this.BuyDateTime = DateTime.Now;
			this.Update();
		}

		#endregion

		#region GetMostRecentDonators
		public static UsrSet GetMostRecentDonators(int top)
		{
			var q = new Query
			{
				Columns = Usr.LinkColumns,
				QueryCondition = new Q(UsrDonationIcon.Columns.Enabled, true),
				OrderBy = new OrderBy(UsrDonationIcon.Columns.BuyDateTime, OrderBy.OrderDirection.Descending),
				TableElement = new Join(UsrDonationIcon.Columns.UsrK, Usr.Columns.K),
				TopRecords = top
			};
			return new UsrSet(q);
		}
		#endregion

	}
	#endregion

}
