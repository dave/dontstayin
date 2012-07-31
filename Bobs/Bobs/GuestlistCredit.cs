using System;
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

namespace Bobs
{
		
	#region GuestlistCredit
	/// <summary>
	/// Request for guestlist credits - created when a promoter clicks a paypal link to buy guestlist credits
	/// </summary>
	[Serializable] 
	public partial class GuestlistCredit : IBuyable
	{

		#region simple members
		/// <summary>
		/// The primary key
		/// </summary>
		public override int K
		{
			get { return this[GuestlistCredit.Columns.K] as int? ?? 0; }
			set { this[GuestlistCredit.Columns.K] = value; }
		}
		/// <summary>
		/// Link to the promoter table
		/// </summary>
		public override int PromoterK
		{
			get { return (int)this[GuestlistCredit.Columns.PromoterK]; }
			set { promoter = null; this[GuestlistCredit.Columns.PromoterK] = value; }
		}
		/// <summary>
		/// DateTime the credit request was created
		/// </summary>
		public override DateTime DateTimeCreated
		{
			get { return (DateTime)this[GuestlistCredit.Columns.DateTimeCreated]; }
			set { this[GuestlistCredit.Columns.DateTimeCreated] = value; }
		}
		/// <summary>
		/// Number of credits bought
		/// </summary>
		public override int Credits
		{
			get { return (int)this[GuestlistCredit.Columns.Credits]; }
			set { this[GuestlistCredit.Columns.Credits] = value; }
		}
		/// <summary>
		/// Total price charged
		/// </summary>
		public override decimal TotalPrice
		{
			get { return (decimal)this[GuestlistCredit.Columns.TotalPrice]; }
			set { this[GuestlistCredit.Columns.TotalPrice] = value; }
		}
		/// <summary>
		/// Has the confirmation been received from paypal?
		/// </summary>
		public override bool Done
		{
			get { return (bool)this[GuestlistCredit.Columns.Done]; }
			set { this[GuestlistCredit.Columns.Done] = value; }
		}
		/// <summary>
		/// Has the confirmation been received from paypal?
		/// </summary>
		public override DateTime DateTimeDone
		{
			get { return (DateTime)this[GuestlistCredit.Columns.DateTimeDone]; }
			set { this[GuestlistCredit.Columns.DateTimeDone] = value; }
		}
		/// <summary>
		/// Time stamp to record when someone is trying to purchase an IBuyable item that is linked to this Bob.
		/// </summary>
		public override DateTime BuyableLockDateTime
		{
			get { return (DateTime)this[GuestlistCredit.Columns.BuyableLockDateTime]; }
			set { this[GuestlistCredit.Columns.BuyableLockDateTime] = value; }
		}
		#endregion

		//public void ReceivePayment()
		//{
		//    if (!this.Done)
		//    {
		//        this.Done=true;
		//        this.DateTimeDone=DateTime.Now;
		//        this.Promoter.GuestlistCredit+=this.Credits;
		//        this.Update();
		//        this.Promoter.Update();
		//    }
		//}

		#region IBuyable Methods + Properties
		/// <summary>
		/// Queries database to retrieve the latest BuyableLockDateTime. Only returns if there is a lock within the Vars.IBUYABLE_LOCK_SECONDS
		/// </summary>
		public bool IsLocked
		{
			get
			{
                if (K == 0)
                    return false;

				Query iBuyableLockDateTimeQuery = new Query(new And(new Q(GuestlistCredit.Columns.K, this.K),
																	new Q(GuestlistCredit.Columns.BuyableLockDateTime, QueryOperator.GreaterThanOrEqualTo, DateTime.Now.AddSeconds(-1 * Vars.IBUYABLE_LOCK_SECONDS))));
				iBuyableLockDateTimeQuery.Columns = new ColumnSet(GuestlistCredit.Columns.BuyableLockDateTime);

				GuestlistCreditSet lockedBuyableSet = new GuestlistCreditSet(iBuyableLockDateTimeQuery);
				if (lockedBuyableSet.Count > 0)
				{
					this.BuyableLockDateTime = lockedBuyableSet[0].BuyableLockDateTime;
					return true;
				}
				else
					return false;
			}
		}

		/// <summary>
		/// Checks the price entered against the calculated price.  This checks if the figures have been adjusted during the payment processing.
		/// </summary>
		/// <param name="invoiceItemType">InvoiceItem.Type</param>
		/// <param name="price">InvoiceItem.Price</param>
		/// <returns></returns>
		public bool VerifyPrice(InvoiceItem.Types invoiceItemType, decimal price, decimal total)
		{
			if (invoiceItemType.Equals(InvoiceItem.Types.GuestlistCredit))
				return Math.Round(price, 2) == Math.Round(this.Credits * this.Promoter.GuestlistCharge, 2);
			else
				throw new Exception("invalid invoice item type: " + Utilities.CamelCaseToString(invoiceItemType.ToString()));
		}

		/// <summary>
		/// Checks if the IBuyable Bob is ready to be processed. This is used as a pre-purchasing check.
		/// </summary>
		/// <param name="invoiceItemType">InvoiceItem.Type</param>
		/// <param name="price">InvoiceItem.Price</param>
		/// <returns></returns>
		public bool IsReadyForProcessing(InvoiceItem.Types invoiceItemType, decimal price, decimal total)
		{
			if (invoiceItemType.Equals(InvoiceItem.Types.GuestlistCredit))
			{
				if (!this.Done)
				{
					if (VerifyPrice(invoiceItemType, price, total))
						return true;
					else
						throw new DsiUserFriendlyException("price wrong!");
				}
			}
			else
				throw new Exception("invalid invoice item type: " + Utilities.CamelCaseToString(invoiceItemType.ToString()));

			return this.Done;
		}

		/// <summary>
		/// Processes the IBuyable Bob. For guestlist credits, it verifies that the guestlist credit IsReadyForProcessing. If yes, then add credits to the promoter account, sets guestlist credit as done, and updates the promoter and the guestlist credit.
		/// </summary>
		/// <param name="invoiceItemType">InvoiceItem.Type</param>
		/// <param name="price">InvoiceItem.Price</param>
		/// <returns></returns>
		public bool Process(InvoiceItem.Types invoiceItemType, decimal price, decimal total)
		{
			if (IsReadyForProcessing(invoiceItemType, price, total))
			{
				this.Done = true;
				this.DateTimeDone = DateTime.Now;
				this.Promoter.GuestlistCredit += this.Credits;
				this.Update();
				this.Promoter.Update();
			}
			
			return IsProcessed(invoiceItemType);
		}

		/// <summary>
		/// Unprocesses the IBuyable Bob. For guestlist credit, it removes the guestlist credit amount from the promoter, sets the guestlist credit to not done, and updates the promoter and the guest list credit.
		/// </summary>
		/// <param name="invoiceItemType">InvoiceItem.Type</param>
		/// <returns></returns>
		public bool Unprocess(InvoiceItem.Types invoiceItemType)
		{
			if (invoiceItemType.Equals(InvoiceItem.Types.GuestlistCredit))
			{
				if (this.Done)
				{
					this.Done = false;
					this.DateTimeDone = DateTime.MinValue;
					this.Promoter.GuestlistCredit -= this.Credits;
					this.Update();
					this.Promoter.Update();
				}
			}
			else
				throw new Exception("invalid invoice item type: " + Utilities.CamelCaseToString(invoiceItemType.ToString()));

			return !IsProcessed(invoiceItemType);
		}

		/// <summary>
		/// Verifies if the IBuyable Bob has already been processed successfully.
		/// </summary>
		/// <param name="invoiceItemType">InvoiceItem.Type</param>
		/// <returns></returns>
		public bool IsProcessed(InvoiceItem.Types invoiceItemType)
		{
			if (invoiceItemType.Equals(InvoiceItem.Types.GuestlistCredit))
				return this.Done;
			else
				throw new Exception("invalid invoice item type: " + Utilities.CamelCaseToString(invoiceItemType.ToString()));
		}

		/// <summary>
		/// Sets the IBuyable Bob field BuyableLockDateTime to DateTime.Now and updates the Bob
		/// </summary>
		public void Lock()
		{
			this.BuyableLockDateTime = DateTime.Now;
			this.Update();
		}

		/// <summary>
		/// Sets the IBuyable Bob field BuyableLockDateTime to DateTime.MinValue and updates the Bob
		/// </summary>
		public void Unlock()
		{
			this.BuyableLockDateTime = DateTime.MinValue;
			this.Update();
		}
		#endregion

		#region Links to Bobs
		#region Promoter
		public Promoter Promoter
		{
			get
			{
				if (promoter==null && PromoterK>0)
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

	}
	#endregion
	
}
