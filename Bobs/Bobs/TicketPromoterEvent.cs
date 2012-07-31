using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Bobs;
using Bobs.DataHolders;
using Common;

namespace Bobs
{
	/// This class is automatically-generated from the database. The contents 
	/// should be copied into the correct Bob class and modified to suit. You'll 
	/// probably have to change some int types to enum's etc.

	#region TicketPromoterEvent
	/// <summary>
	/// TicketPromoter to Event relational table
	/// </summary>
	[Serializable] 
	public partial class TicketPromoterEvent : IBobReport, IBobAsHTML, IPage, IReadableReference, ILinkable
	{
		
		#region Simple members
		/// <summary>
		/// The promoter for the tickets
		/// </summary>
		public override int PromoterK
		{
			get { return (int)this[TicketPromoterEvent.Columns.PromoterK]; }
			set { this[TicketPromoterEvent.Columns.PromoterK] = value; }
		}
		/// <summary>
		/// The event for the tickets
		/// </summary>
		public override int EventK
		{
			get { return (int)this[TicketPromoterEvent.Columns.EventK]; }
			set { this[TicketPromoterEvent.Columns.EventK] = value; }
		}
		/// <summary>
		/// Total number of tickets available
		/// </summary>
		public override int TotalTickets
		{
			get { return (int)this[TicketPromoterEvent.Columns.TotalTickets]; }
			set { this[TicketPromoterEvent.Columns.TotalTickets] = value; }
		}
		/// <summary>
		/// Total number of tickets sold
		/// </summary>
		public override int SoldTickets
		{
			get { return (int)this[TicketPromoterEvent.Columns.SoldTickets]; }
			set { this[TicketPromoterEvent.Columns.SoldTickets] = value; }
		}
		/// <summary>
		/// Total amount of money from sold tickets - REMEMBER THIS IS INCORRECT UNITL FUNDSRELEASED=TRUE
		/// </summary>
		public override decimal TotalFunds
		{
			get { return (decimal)this[TicketPromoterEvent.Columns.TotalFunds]; }
			set { this[TicketPromoterEvent.Columns.TotalFunds] = value; }
		}
		/// <summary>
		/// Have the funds been locked manually
		/// </summary>
		public override bool FundsLockManual
		{
			get { return (bool)this[TicketPromoterEvent.Columns.FundsLockManual]; }
			set { this[TicketPromoterEvent.Columns.FundsLockManual] = value; }
		}
		/// <summary>
		/// The user who locked the funds manually
		/// </summary>
		public override int FundsLockManualUsrK
		{
			get { return (int)this[TicketPromoterEvent.Columns.FundsLockManualUsrK]; }
			set { this[TicketPromoterEvent.Columns.FundsLockManualUsrK] = value; }
		}
		/// <summary>
		/// Timestamp for manual funds lock
		/// </summary>
		public override DateTime FundsLockManualDateTime
		{
			get { return (DateTime)this[TicketPromoterEvent.Columns.FundsLockManualDateTime]; }
			set { this[TicketPromoterEvent.Columns.FundsLockManualDateTime] = value; }
		}
		/// <summary>
		/// Note for manual funds lock
		/// </summary>
		public override string FundsLockManualNote
		{
			get { return (string)this[TicketPromoterEvent.Columns.FundsLockManualNote]; }
			set { this[TicketPromoterEvent.Columns.FundsLockManualNote] = value; }
		}
		/// <summary>
		/// Are funds locked due to duplicate IP fraud
		/// </summary>
		public override bool FundsLockFraudIpDuplicate
		{
			get { return (bool)this[TicketPromoterEvent.Columns.FundsLockFraudIpDuplicate]; }
			set { this[TicketPromoterEvent.Columns.FundsLockFraudIpDuplicate] = value; }
		}
		/// <summary>
		/// Country origin of duplicate IP fraud
		/// </summary>
		public override int FundsLockFraudIpCountry
		{
			get { return (int)this[TicketPromoterEvent.Columns.FundsLockFraudIpCountry]; }
			set { this[TicketPromoterEvent.Columns.FundsLockFraudIpCountry] = value; }
		}
		/// <summary>
		/// Are funds locked due to GUID fraud
		/// </summary>
		public override bool FundsLockFraudGuid
		{
			get { return (bool)this[TicketPromoterEvent.Columns.FundsLockFraudGuid]; }
			set { this[TicketPromoterEvent.Columns.FundsLockFraudGuid] = value; }
		}
		/// <summary>
		/// Are funds locked due to users negative responses
		/// </summary>
		public override bool FundsLockUsrResponses
		{
			get { return (bool)this[TicketPromoterEvent.Columns.FundsLockUsrResponses]; }
			set { this[TicketPromoterEvent.Columns.FundsLockUsrResponses] = value; }
		}
		/// <summary>
		/// Text explaining any locks, readable by admins and used when making unlock decisions
		/// </summary>
		public override string FundsLockText
		{
			get { return (string)this[TicketPromoterEvent.Columns.FundsLockText]; }
			set { this[TicketPromoterEvent.Columns.FundsLockText] = value; }
		}
		/// <summary>
		/// Is funds lock overridden
		/// </summary>
		public override bool FundsLockOverride
		{
			get { return (bool)this[TicketPromoterEvent.Columns.FundsLockOverride]; }
			set { this[TicketPromoterEvent.Columns.FundsLockOverride] = value; }
		}
		/// <summary>
		/// The user who overrode the funds lock
		/// </summary>
		public override int FundsLockOverrideUsrK
		{
			get { return (int)this[TicketPromoterEvent.Columns.FundsLockOverrideUsrK]; }
			set { this[TicketPromoterEvent.Columns.FundsLockOverrideUsrK] = value; }
		}
		/// <summary>
		/// Timestamp for funds lock override
		/// </summary>
		public override DateTime FundsLockOverrideDateTime
		{
			get { return (DateTime)this[TicketPromoterEvent.Columns.FundsLockOverrideDateTime]; }
			set { this[TicketPromoterEvent.Columns.FundsLockOverrideDateTime] = value; }
		}
		/// <summary>
		/// Note explaining why funds lock has been overridden
		/// </summary>
		public override string FundsLockOverrideNote
		{
			get { return (string)this[TicketPromoterEvent.Columns.FundsLockOverrideNote]; }
			set { this[TicketPromoterEvent.Columns.FundsLockOverrideNote] = value; }
		}
		/// <summary>
		/// Have funds been released to promoter
		/// </summary>
		public override bool FundsReleased
		{
			get { return (bool)this[TicketPromoterEvent.Columns.FundsReleased]; }
			set { this[TicketPromoterEvent.Columns.FundsReleased] = value; }
		}
		/// <summary>
		/// Transfer reference for funds to promoter
		/// </summary>
		public override int FundsTransferK
		{
			get { return (int)this[TicketPromoterEvent.Columns.FundsTransferK]; }
			set { this[TicketPromoterEvent.Columns.FundsTransferK] = value; }
		}
		/// <summary>
		/// Total number of tickets cancelled
		/// </summary>
		public override int CancelledTickets
		{
			get { return (int)this[TicketPromoterEvent.Columns.CancelledTickets]; }
			set { this[TicketPromoterEvent.Columns.CancelledTickets] = value; }
		}
		/// <summary>
		/// Lock when the total funds dont match the ticket run funds
		/// </summary>
		public override bool FundsLockTotalFundsDontMatch
		{
			get { return (bool)this[TicketPromoterEvent.Columns.FundsLockTotalFundsDontMatch]; }
			set { this[TicketPromoterEvent.Columns.FundsLockTotalFundsDontMatch] = value; }
		}
		/// <summary>
		/// Total amount of VAT from ticket invoices
		/// </summary>
		public override decimal TotalVat
		{
			get { return (decimal)this[TicketPromoterEvent.Columns.TotalVat]; }
			set { this[TicketPromoterEvent.Columns.TotalVat] = value; }
		}
        /// <summary>
        /// Total amount of booking fees
        /// </summary>
		public override decimal TotalBookingFees
        {
			get { return (decimal)this[TicketPromoterEvent.Columns.TotalBookingFees]; }
            set { this[TicketPromoterEvent.Columns.TotalBookingFees] = value; }
        }
		/// <summary>
		/// Contact email address for users to contact regarding ticket sales
		/// </summary>
		public override string ContactEmail
		{
			get { return (string)this[TicketPromoterEvent.Columns.ContactEmail]; }
			set { this[TicketPromoterEvent.Columns.ContactEmail] = value; }
		}
		#endregion

		#region Constants
		public const float MAX_PERCENT_FOR_DUPLICATE_IP = 1;
		public const int MAX_NUMBER_FOR_DUPLICATE_IP = 20;
		public const float MAX_PERCENT_FOR_OUTSIDE_UK_IP = 1;
		public const int MAX_NUMBER_FOR_OUTSIDE_UK_IP = 20;
		public const float MAX_PERCENT_PER_BROWSER_GUID = 1;
		public const int MAX_NUMBER_PER_BROWSER_GUID = 20;
		public const float MAX_PERCENT_FOR_NEGATIVE_USR_RESPONSES = 10;
		public const float MIN_PERCENT_FOR_USR_RESPONSES = 10;
		public const int MIN_NUMBER_FOR_USR_RESPONSES = 5;
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

		#region IBobAsHTML methods
		public string AsHTML()
		{
			//string lineReturn = Vars.HTML_LINE_RETURN;
			StringBuilder sb = new StringBuilder();

			if(this.Promoter != null)
				sb.Append(this.Promoter.AsHTML());
			if(this.Event != null)
				sb.Append(this.Event.AsHTML());

			return sb.ToString();
		}
		#endregion

		#region Properties
		#region Event
		public Event Event
		{
			get
			{
				if (_Event == null && EventK > 0)
					_Event = new Event(EventK, this, TicketPromoterEvent.Columns.EventK);
				return _Event;
			}
			set
			{
				_Event = value;
				if (_Event != null)
					this.EventK = _Event.K;
				else
					this.EventK = 0;
			}
		}
		Event _Event;
		#endregion

		#region Promoter
		public Promoter Promoter
		{
			get
			{
				if (promoter == null)
					promoter = new Promoter(PromoterK, this, TicketPromoterEvent.Columns.PromoterK);
				return promoter;
			}
			set
			{
				promoter = value;
				if (promoter != null)
					this.PromoterK = promoter.K;
				else
					this.PromoterK = 0;
			}
		}
		Promoter promoter;
		#endregion

		#region FundsLockManualUsr
		public Usr FundsLockManualUsr
		{
			get
			{
				if (fundsLockManualUsr == null)
					fundsLockManualUsr = new Usr(FundsLockManualUsrK, this, TicketPromoterEvent.Columns.FundsLockManualUsrK);
				return fundsLockManualUsr;
			}
			set
			{
				fundsLockManualUsr = value;
				if (fundsLockManualUsr != null)
					this.FundsLockManualUsrK = fundsLockManualUsr.K;
				else
					this.FundsLockManualUsrK = 0;
			}
		}
		Usr fundsLockManualUsr;
		#endregion

		#region FundsLockOverrideUsr
		public Usr FundsLockOverrideUsr
		{
			get
			{
				if (fundsLockOverrideUsr == null)
					fundsLockOverrideUsr = new Usr(FundsLockOverrideUsrK, this, TicketPromoterEvent.Columns.FundsLockOverrideUsrK);
				return fundsLockOverrideUsr;
			}
			set
			{
				fundsLockOverrideUsr = value;
				if (fundsLockOverrideUsr != null)
					this.FundsLockOverrideUsrK = fundsLockOverrideUsr.K;
				else
					this.FundsLockOverrideUsrK = 0;
			}
		}
		Usr fundsLockOverrideUsr;
		#endregion

		#region FundsTransfer
		public Transfer FundsTransfer
		{
			get
			{
				if (fundsTransfer == null && FundsTransferK > 0)
					fundsTransfer = new Transfer(FundsTransferK, this, TicketPromoterEvent.Columns.FundsTransferK);
				return fundsTransfer;
			}
			set
			{
				fundsTransfer = value;
				if (fundsTransfer != null)
					this.FundsTransferK = fundsTransfer.K;
				else
					this.FundsTransferK = 0;
			}
		}
		Transfer fundsTransfer;
		#endregion

		#region TicketRuns
		public TicketRunSet TicketRuns
		{
			get
			{
				if (promoterEventTicketRuns == null)
				{
					Query q = new Query();
					q.NoLock = true;
					q.QueryCondition = new And(new Q(TicketRun.Columns.EventK, this.EventK),
											   new Q(TicketRun.Columns.PromoterK, this.PromoterK));
					q.OrderBy = new OrderBy(new OrderBy(TicketRun.Columns.ListOrder), new OrderBy(TicketRun.Columns.Price));
					promoterEventTicketRuns = new TicketRunSet(q);

					CalculateTicketsAndFunds();
				}
				return promoterEventTicketRuns;
			}
		}
		private TicketRunSet promoterEventTicketRuns;
		#endregion

		#region PromoterEventTicketsSold
		public TicketSet TicketsSold
		{
			get
			{
				if (promoterEventTicketsSold == null)
				{
					//promoterEventTicketsSold = this.Event.TicketsSold(this.PromoterK);

					Query ticketQuery = new Query(new And(new Q(Ticket.Columns.EventK, this.EventK),
												  new Q(TicketRun.Columns.PromoterK, this.PromoterK),
												  Ticket.SoldTicketsQ));
					ticketQuery.OrderBy = new OrderBy(Ticket.Columns.K);
					ticketQuery.TableElement = new Join(Ticket.Columns.TicketRunK, TicketRun.Columns.K);

					promoterEventTicketsSold = new TicketSet(ticketQuery);

					

					//CalculateTicketsAndFunds();
				}
				return promoterEventTicketsSold;
			}
		}
		private TicketSet promoterEventTicketsSold;
		#endregion

		#region IsFundsLock
		public bool IsFundsLock
		{
			get
			{
				return 
					this.Event.DateTime >= DateTime.Today || 
					(
						Math.Round(GetTotalFunds(), 2) != 0  && 
						(
							!this.Promoter.EnableTickets ||
							(
								!FundsLockOverride && 
								(FundsLockFraudGuid || FundsLockFraudIpCountry > 0 || FundsLockFraudIpDuplicate || FundsLockManual || FundsLockUsrResponses || FundsLockTotalFundsDontMatch)
							)
						)
					);
			}
		}

		#endregion

        #region TypeAndK
        public string TypeAndK
        {
            get
            {
                return Utilities.CamelCaseToString(Transfer.Methods.TicketSales.ToString()) + " #" + this.FundsTransferK.ToString();
            }
        }
        #endregion

		#region GrossFunds
		public decimal GrossFunds
		{
			get
			{
				if (grossFunds == -1)
				{
					grossFunds = 0;
					foreach (TicketRun tr in this.TicketRuns)
					{
						foreach (Ticket t in tr.Tickets)
						{
							grossFunds += t.Price;
						}
					}

					grossFunds = Math.Round(grossFunds, 2);
				}
				return grossFunds;
			}
		}
		private decimal grossFunds = -1;
		#endregion

		#region GrossVat
		public decimal GrossVat
		{
			get
			{
				if (grossVat == -1)
				{
					grossVat = 0;
					foreach (TicketRun tr in this.TicketRuns)
					{
						foreach (Ticket t in tr.Tickets)
						{
							grossVat += t.InvoiceItem.Vat;
						}
					}
					grossVat = Math.Round(grossVat, 2);
				}
				return grossVat;
			}
		}
		private decimal grossVat = -1;
		#endregion

		#region GrossTotal
		public decimal GrossTotal
		{
			get
			{
				return Math.Round(GrossFunds + GrossVat, 2);
			}
		}
		#endregion

		#region GrossBookingFees
		public decimal GrossBookingFees
		{
			get
			{
				if (grossBookingFees == -1)
				{
					grossBookingFees = 0;
					foreach (TicketRun tr in this.TicketRuns)
					{
						foreach (Ticket t in tr.Tickets)
						{
							grossBookingFees += t.BookingFee;
						}
					}
					grossBookingFees = Math.Round(grossBookingFees, 2);
				}
				return grossBookingFees;
			}
		}
		private decimal grossBookingFees = -1;
		#endregion

		#region TotalFundsReleased
		public decimal TotalFundsReleased
		{
			get
			{
				if (totalFundsReleased == -1)
				{
					totalFundsReleased = 0;
					foreach (TicketRun tr in this.TicketRuns)
					{
						foreach (Ticket t in tr.Tickets)
						{
							if(!t.CancelledBeforeFundsRelease)
								totalFundsReleased += t.Price;
						}
					}

					totalFundsReleased = Math.Round(totalFundsReleased, 2);
				}
				return totalFundsReleased;
			}
		}
		private decimal totalFundsReleased = -1;
		#endregion

		#region TotalVatReleased
		public decimal TotalVatReleased
		{
			get
			{
				if (totalVatReleased == -1)
				{
					totalVatReleased = 0;
					foreach (TicketRun tr in this.TicketRuns)
					{
						foreach (Ticket t in tr.Tickets)
						{
							if (!t.CancelledBeforeFundsRelease)
								totalVatReleased += t.InvoiceItem.Vat;
						}
					}

					totalVatReleased = Math.Round(totalVatReleased, 2);
				}
				return totalVatReleased;
			}
		}
		private decimal totalVatReleased = -1;
		#endregion

		#region TotalReleased
		public decimal TotalReleased
		{
			get
			{
				return Math.Round(TotalFundsReleased + TotalVatReleased, 2);
			}
		}
		#endregion

		#region TotalBookingFeesReleased
		public decimal TotalBookingFeesReleased
		{
			get
			{
				if (totalBookingFeesReleased == -1)
				{
					totalBookingFeesReleased = 0;
					foreach (TicketRun tr in this.TicketRuns)
					{
						foreach (Ticket t in tr.Tickets)
						{
							if (!t.CancelledBeforeFundsRelease)
							{
								totalBookingFeesReleased += t.BookingFee;
							}
							else if (t.Invoice.CreditsApplied.Count > 0)
							{
								decimal refundedBookingFees = 0;
								foreach (InvoiceItem ii in t.Invoice.CreditsApplied[0].Items)
								{
									if (ii.Type == InvoiceItem.Types.EventTicketsBookingFee)
										refundedBookingFees += Math.Abs(ii.Total);
								}
								refundedBookingFees = Math.Round(refundedBookingFees, 2);
								if (refundedBookingFees > t.BookingFee)
									throw new Exception("Error calculating refunded booking fees");
								else
									totalBookingFeesReleased += t.BookingFee - refundedBookingFees;
							}
						}
					}

					totalBookingFeesReleased = Math.Round(totalBookingFeesReleased, 2);
				}
				return totalBookingFeesReleased;
			}
		}
		private decimal totalBookingFeesReleased = -1;
		#endregion

		#region TotalCancelledTicketsBeforeRelease
		public int TotalCancelledTicketsBeforeRelease
		{
			get
			{
				if (totalCancelledTicketsBeforeRelease == -1)
				{
					totalCancelledTicketsBeforeRelease = 0;
					foreach (TicketRun tr in this.TicketRuns)
					{
						foreach (Ticket t in tr.Tickets)
						{
							if (t.CancelledBeforeFundsRelease)
								totalCancelledTicketsBeforeRelease += t.Quantity;
						}
					}
				}
				return totalCancelledTicketsBeforeRelease;
			}
		}
		private int totalCancelledTicketsBeforeRelease = -1;
		#endregion

		#region Query Conditions
		public static Q AwaitingFundsReleaseQ
		{
			get
			{
				return new Or(new Q(TicketPromoterEvent.Columns.FundsReleased, false),
							  new Q(TicketPromoterEvent.Columns.FundsReleased, QueryOperator.IsNull, null));
			}
		}

		public static Q FundsReleasedQ
		{
			get
			{
				return new Q(TicketPromoterEvent.Columns.FundsReleased, true);
			}
		}

		public static Q LockedFundsQ
		{
			get
			{
				return new And(new Q(TicketPromoterEvent.Columns.FundsLockOverride, false),
							   new Or(new Q(TicketPromoterEvent.Columns.FundsLockFraudGuid, true),
									  new Q(TicketPromoterEvent.Columns.FundsLockFraudIpCountry, QueryOperator.GreaterThan, 0),
									  new Q(TicketPromoterEvent.Columns.FundsLockFraudIpDuplicate, true),
									  new Q(TicketPromoterEvent.Columns.FundsLockManual, true),
									  new Q(TicketPromoterEvent.Columns.FundsLockUsrResponses, true)));
			}
		}
		#endregion
		#endregion

		#region Methods
        #region IsUsrAllowedAccess
        public bool IsUsrAllowedAccess(Usr usr)
        {
            return this.Promoter.IsUsrAllowedAccess(usr);
        }
        #endregion

    	public Transfer CreateFundsReleaseTransfer()
		{
			Transfer releaseFundsTransfer = new Transfer();
			if (Usr.Current != null)
			{
				releaseFundsTransfer.ActionUsrK = Usr.Current.K;
			}
			releaseFundsTransfer.AddNote("Funds released from Event K=" + this.EventK, "System");
			releaseFundsTransfer.Amount = this.TotalFunds;
			releaseFundsTransfer.DateTimeCreated = DateTime.Now;
			releaseFundsTransfer.DateTimeComplete = DateTime.Now;
            //releaseFundsTransfer.DSIBankAccount = Transfer.DSIBankAccounts.Client;
			releaseFundsTransfer.DuplicateGuid = Guid.NewGuid();
			releaseFundsTransfer.Method = Transfer.Methods.TicketSales;
			releaseFundsTransfer.Company = Model.Entities.Transfer.CompanyEnum.DH;
			releaseFundsTransfer.PromoterK = this.PromoterK;
			releaseFundsTransfer.Status = Transfer.StatusEnum.Success;
			releaseFundsTransfer.Type = Transfer.TransferTypes.Payment;
			releaseFundsTransfer.UsrK = this.Promoter.PrimaryUsrK;

			return releaseFundsTransfer;
		}

		public void RunFundsLockChecks()
		{
			this.FundsLockText = "";
			IsFundsLockFraudIpDuplicate();
			IsFundsLockFraudIpCountry();
			IsFundsLockFraudGuid();
			IsFundsLockFraudUsrResponses();
			IsFundsLockManual();
			IsFundsLockOverride();
			DoesTicketTotalMatchTicketRunTotalFunds();
		}

		public void CalculateTicketsAndFunds()
		{
			CalculateTickets();
			CalculateTotalFundsAndVat();
		}

		public void CalculateTickets()
		{
			if (this.TicketRuns != null)
			{
				this.TotalTickets = 0;

				foreach (TicketRun ticketRun in this.TicketRuns)
				{
					this.TotalTickets += ticketRun.MaxTickets;
				}
			}
		}

		public void CalculateTotalFundsAndVat()
		{
			this.promoterEventTicketsSold = null;
			if (this.TicketsSold != null)
			{
				this.SoldTickets = 0;
				this.TotalFunds = 0;
                this.TotalBookingFees = 0;
				this.TotalVat = 0;
				this.CancelledTickets = 0;

				foreach (Ticket ticket in this.TicketsSold)
				{
					this.SoldTickets += ticket.Quantity;

					if (!ticket.Cancelled)
					{
						this.TotalFunds += ticket.Price;
						this.TotalBookingFees += ticket.BookingFee;

						if (ticket.InvoiceItem != null)
							this.TotalVat += ticket.InvoiceItem.Vat;
					}
					else
					{
						this.CancelledTickets += ticket.Quantity;
						if (ticket.Invoice.CreditsApplied.Count > 0)
						{
							decimal refundedBookingFees = 0;
							foreach (InvoiceItem ii in ticket.Invoice.CreditsApplied[0].Items)
							{
								if (ii.Type == InvoiceItem.Types.EventTicketsBookingFee)
									refundedBookingFees += Math.Abs(ii.Total);
							}
							refundedBookingFees = Math.Round(refundedBookingFees, 2);
							if (refundedBookingFees > ticket.BookingFee)
								throw new Exception("Error calculating refunded booking fees");
							else
								this.TotalBookingFees += ticket.BookingFee - refundedBookingFees;

						}
					}
				}
				
				this.TotalFunds = Math.Round(this.TotalFunds, 2);
                this.TotalBookingFees = Math.Round(this.TotalBookingFees, 2);
				this.TotalVat = Math.Round(this.TotalVat, 2);
			}
		}

		public decimal GetTotalFunds()
		{
			return GetTotalFundsAtDate(DateTime.MaxValue);
		}


		public decimal GetTotalFundsAtDate(DateTime date)
		{
			this.promoterEventTicketsSold = null;
			if (this.TicketsSold != null)
			{
				decimal totalFunds = 0;

				foreach (Ticket ticket in this.TicketsSold)
				{
					bool cancelledBeforeDate = ticket.Cancelled && ticket.CancelledDateTime < date;
					if (!cancelledBeforeDate && ticket.BuyDateTime < date)
					{
						totalFunds += ticket.Price;
					}
				}

				return totalFunds;
			}
			else
				return 0;
		}

		public bool IsFundsLockFraudIpDuplicate()
		{
			Dictionary<string, int> IPAddressDictionary = new Dictionary<string, int>();
			int numberOfTicketsSold = 0;
			this.FundsLockFraudIpDuplicate = false;

			if (TicketsSold.Count > 0)
			{
				string ipKey = "";
				foreach (Ticket ticket in TicketsSold)
				{
					if (ticket.IpAddress.Length > 3)
					{
						ipKey = ticket.IpAddress.Substring(0, ticket.IpAddress.LastIndexOf("."));
						if (!IPAddressDictionary.ContainsKey(ipKey))
							IPAddressDictionary.Add(ipKey, ticket.Quantity);
						else
							IPAddressDictionary[ipKey] += ticket.Quantity;
						numberOfTicketsSold += ticket.Quantity;
					}
				}

				if (IPAddressDictionary.Count > 0)
				{
					string mostUsedIpRange = GetKeyOfMaxValue(IPAddressDictionary);
					int mostUsedIpRangeTickets = IPAddressDictionary[mostUsedIpRange ];

					if (mostUsedIpRangeTickets > MAX_NUMBER_FOR_DUPLICATE_IP && mostUsedIpRangeTickets * 100 > numberOfTicketsSold * MAX_PERCENT_FOR_DUPLICATE_IP)
					{
						// Lock
						this.FundsLockFraudIpDuplicate = true;
						this.AddFundsLockText("Duplicate IP range: " + mostUsedIpRange + " bought " + mostUsedIpRangeTickets.ToString() + " tickets.");
					}
				}
			}
			return this.FundsLockFraudIpDuplicate;
		}

		/// <summary>
		/// Check if IP for country of origin. If more than MAX % and MAX # is from outside UK, then set FundsLock = true;
		/// </summary>
		/// <returns></returns>
		public bool IsFundsLockFraudIpCountry()
		{
			Dictionary<string, int> NonUKCountryKDictionary = new Dictionary<string, int>();
			int outOfUKTicketCount = 0;
			int numberOfTicketsSold = 0;
			this.FundsLockFraudIpCountry = 0;

			if (TicketsSold.Count > 0)
			{
				foreach (Ticket ticket in TicketsSold)
				{
					if (ticket.IpAddress.Length > 0)
					{
						IpCountry ipAddressCountry = IpCountry.Lookup(ticket.IpAddress);
						numberOfTicketsSold += ticket.Quantity;

						// UK CountryK = 224
						if (ipAddressCountry != null && ipAddressCountry.CountryK != 224)
						{
							if (NonUKCountryKDictionary.ContainsKey(ipAddressCountry.CountryK.ToString()))
								NonUKCountryKDictionary[ipAddressCountry.CountryK.ToString()] += ticket.Quantity;
							else
								NonUKCountryKDictionary.Add(ipAddressCountry.CountryK.ToString(), ticket.Quantity);

							outOfUKTicketCount += ticket.Quantity;
						}
					}
				}
				if (NonUKCountryKDictionary.Count > 0)
				{
					string mostUsedNonUKIpCountry = GetKeyOfMaxValue(NonUKCountryKDictionary);
					int mostUsedNonUKIpCountryTickets = NonUKCountryKDictionary[mostUsedNonUKIpCountry];

					if (outOfUKTicketCount > MAX_NUMBER_FOR_OUTSIDE_UK_IP && outOfUKTicketCount * 100 > numberOfTicketsSold * MAX_PERCENT_FOR_OUTSIDE_UK_IP)
					{
						// Lock
						this.FundsLockFraudIpCountry = Convert.ToInt32(mostUsedNonUKIpCountry);
						this.AddFundsLockText("Outside of UK IP range: bought " + outOfUKTicketCount.ToString() + " tickets. With " + mostUsedNonUKIpCountryTickets.ToString() + " tickets purchased from CountryK=" + mostUsedNonUKIpCountry);
					}
				}
			}
			return this.FundsLockFraudIpCountry > 0;
		}

		public bool IsFundsLockFraudGuid()
		{
			Dictionary<string, int> browserGuidDictionary = new Dictionary<string, int>();
			
			int numberOfTicketsSold = 0;
			this.FundsLockFraudGuid = false;

			if (TicketsSold.Count > 0)
			{
				foreach (Ticket ticket in TicketsSold)
				{
					if (browserGuidDictionary.ContainsKey(ticket.BrowserGuid.ToString()))
						browserGuidDictionary[ticket.BrowserGuid.ToString()] += ticket.Quantity;
					else
						browserGuidDictionary.Add(ticket.BrowserGuid.ToString(), ticket.Quantity);
					numberOfTicketsSold += ticket.Quantity;
				}

				if (browserGuidDictionary.Count > 0)
				{
					string mostUsedGuid = GetKeyOfMaxValue(browserGuidDictionary);
					int mostUsedGuidTickets = browserGuidDictionary[mostUsedGuid];

					if (mostUsedGuidTickets > MAX_NUMBER_PER_BROWSER_GUID && mostUsedGuidTickets * 100 > numberOfTicketsSold * MAX_PERCENT_PER_BROWSER_GUID)
					{
						// Lock
						this.FundsLockFraudGuid = true;
						this.AddFundsLockText("Duplicate GUID: " + mostUsedGuid + " bought " + mostUsedGuidTickets.ToString() + " tickets.");
					}
				}
			}
			return this.FundsLockFraudGuid;
		}

		public bool IsFundsLockFraudUsrResponses()
		{
			int negativeResponseTicketCount = 0;
			int positiveResponseTicketCount = 0;
			int numberOfTicketsSold = 0;
			this.FundsLockUsrResponses = false;

			TicketSet promoterTickets = this.Event.TicketsSold(this.PromoterK);

			if (promoterTickets.Count > 0)
			{
				foreach (Ticket ticket in promoterTickets)
				{
					numberOfTicketsSold += ticket.Quantity;

					if (ticket.Feedback == Ticket.FeedbackEnum.Good)
						positiveResponseTicketCount += ticket.Quantity;
					else if (ticket.Feedback == Ticket.FeedbackEnum.Bad)
						negativeResponseTicketCount += ticket.Quantity;
				}

				int totalResponseCount = negativeResponseTicketCount + positiveResponseTicketCount;

				if (totalResponseCount * 100 < this.Event.TicketsSoldTotal * MIN_PERCENT_FOR_USR_RESPONSES || totalResponseCount < MIN_NUMBER_FOR_USR_RESPONSES)
				{
					// Lock
					this.FundsLockUsrResponses = true;
                    this.AddFundsLockText("Not enough user ticket responses: " + positiveResponseTicketCount.ToString() + " positive and " + negativeResponseTicketCount.ToString() + " negative ticket responses for " + numberOfTicketsSold.ToString() + " ticket" + (numberOfTicketsSold > 1 ? "s" : "") + ".");
				}
				else if (negativeResponseTicketCount * 100 > totalResponseCount * MAX_PERCENT_FOR_NEGATIVE_USR_RESPONSES)
				{
					// Lock
					this.FundsLockUsrResponses = true;
                    this.AddFundsLockText("Negative user ticket responses: " + negativeResponseTicketCount.ToString() + " negative ticket responses out of " + totalResponseCount.ToString() + " total ticket responses out of " + numberOfTicketsSold.ToString() + " ticket" + (numberOfTicketsSold > 1 ? "s" : "") + ".");
				}
			}
			return this.FundsLockUsrResponses;
		}

		public bool IsFundsLockManual()
		{
			if(this.FundsLockManual)
				this.AddFundsLockText(this.FundsLockManualDateTime.ToString("dd/MM/yy HH:mm") + " Manual funds lock by " + this.FundsLockManualUsr.NickName + ": " + this.FundsLockManualNote);
			
			return this.FundsLockManual;
		}

		public bool IsFundsLockOverride()
		{
			if(this.FundsLockOverride)
				this.AddFundsLockText(this.FundsLockOverrideDateTime.ToString("dd/MM/yy HH:mm") + " Override funds lock by " + this.FundsLockOverrideUsr.NickName + ": " + this.FundsLockOverrideNote);
			
			return this.FundsLockOverride;
		}

		public bool DoesTicketTotalMatchTicketRunTotalFunds()
		{
			decimal ticketRunTotalFunds = 0;
			foreach (TicketRun ticketRun in this.TicketRuns)
			{
				ticketRunTotalFunds += ticketRun.ValidTicketQuantity * ticketRun.Price;
			}

			this.FundsLockTotalFundsDontMatch = Math.Round(ticketRunTotalFunds, 2) != Math.Round(this.GetTotalFunds(), 2);

			if(this.FundsLockTotalFundsDontMatch)
				this.AddFundsLockText("Total funds do not match total ticket run funds");

			return this.FundsLockTotalFundsDontMatch;
		}

		public string GetKeyOfMaxValue(Dictionary<string, int> dictionary)
		{
			int maxValue = Int32.MinValue;
			string keyOfMaxValue = "";
			
			foreach(string key in dictionary.Keys)
			{
				if (maxValue < dictionary[key])
				{
					maxValue = dictionary[key];
					keyOfMaxValue = key;
				}
			}

			return keyOfMaxValue;
		}

		public void AddFundsLockText(string fundsLockText)
		{
			if (this.FundsLockText.Length > 1)
				this.FundsLockText += "<br>";
			this.FundsLockText += fundsLockText;
		}

		#region EvaluateRecentTicketEventsAndReleaseFunds
		public static void EvaluateRecentTicketEventsAndReleaseFunds()
		{
			try
			{
				CreateNewTicketPromoterEventsWhenNeeded();

				TicketPromoterEventSet ticketPromoterEvents = TicketPromoterEvent.GetUnreleasedFundsTicketPromoterEventsForPastEvents();

				CalculateTicketsAndRunFundLocksChecksAndReleaseFunds(ticketPromoterEvents);
			}
			catch (Exception ex)
			{
				Utilities.AdminEmailAlert("Exception occurred in TicketPromoterEvent.EvaluateRecentTicketEventsAndReleaseFunds()", 
											"Exception occurred in TicketPromoterEvent.EvaluateRecentTicketEventsAndReleaseFunds()", ex);
			}
		}

		public void ReleaseFunds()
		{
			try
			{
				if (!this.IsFundsLock && !this.FundsReleased && (this.FundsTransferK == 0 || Math.Round(this.GetTotalFunds(), 2) == 0))
				{
                    this.FundsReleased = true;
                    this.Update();

                    if (Math.Round(this.GetTotalFunds(), 2) > 0)
                    {
                        Transfer releaseFundsTransfer = this.CreateFundsReleaseTransfer();

                        if (Math.Round(releaseFundsTransfer.Amount, 2) == 0)
                            releaseFundsTransfer.IsFullyApplied = true;

                        releaseFundsTransfer.Update();

                        this.FundsTransfer = releaseFundsTransfer;
                        this.Update();

                        this.Promoter.ApplyAvailableMoneyToUnpaidInvoices();
                    }
					Utilities.EmailTicketPromoterEvent(this);
				}
			}
			catch (Exception ex)
			{
				Utilities.AdminEmailAlert("Exception occurred in TicketPromoterEvent.ReleaseFunds(): Promoter K=" + this.PromoterK + ", Event K=" + this.EventK, 
											"Exception occurred in TicketPromoterEvent.ReleaseFunds(): Promoter K=" + this.PromoterK + ", Event K=" + this.EventK, ex);
			}
		}

		public static TicketPromoterEventSet GetUnreleasedFundsTicketPromoterEventsForPastEvents()
		{
			try
			{
				Query unreleasedFundsTicketPromoterEventsQuery = new Query(new And(new Or(new Q(TicketPromoterEvent.Columns.FundsReleased, QueryOperator.IsNull, null),
																						  new Q(TicketPromoterEvent.Columns.FundsReleased, false)),
																				   new Q(Event.Columns.DateTime, QueryOperator.LessThan, DateTime.Today)));
				unreleasedFundsTicketPromoterEventsQuery.TableElement = new Join(TicketPromoterEvent.Columns.EventK, Event.Columns.K);
				return new TicketPromoterEventSet(unreleasedFundsTicketPromoterEventsQuery);
			}
			catch (Exception ex)
			{
				Utilities.AdminEmailAlert("Exception in TicketPromoterEvent.GetUnreleasedFundsTicketPromoterEventsForPastEvents()", "Exception in TicketPromoterEvent.GetUnreleasedFundsTicketPromoterEventsForPastEvents()", ex);
			}

			return null;
		}

		public static void CreateNewTicketPromoterEventsWhenNeeded()
		{
			try
			{
                // Testing started in May 2007, so include all events after that date. We do not want to include past ticket runs, SW4 2006.
				Query ticketPromoterEventNotCreatedQuery = new Query(new And(new Q(TicketPromoterEvent.Columns.EventK, QueryOperator.IsNull, null),
																			 new Q(Event.Columns.DateTime, QueryOperator.LessThan, DateTime.Today),
                                                                             new Q(Event.Columns.DateTime, QueryOperator.GreaterThanOrEqualTo, Vars.TICKETS_NEW_SYSTEM_START_DATE)));

				ticketPromoterEventNotCreatedQuery.TableElement = new Join(new Join(TicketRun.Columns.EventK, Event.Columns.K), new TableElement(TablesEnum.TicketPromoterEvent), QueryJoinType.Left,
																				 new And(new Q(TicketPromoterEvent.Columns.EventK, Event.Columns.K, true),
																						 new Q(TicketPromoterEvent.Columns.PromoterK, TicketRun.Columns.PromoterK, true)));
				ticketPromoterEventNotCreatedQuery.Columns = new ColumnSet(TicketRun.Columns.PromoterK, TicketRun.Columns.EventK);
				ticketPromoterEventNotCreatedQuery.GroupBy = new GroupBy(new GroupBy(TicketRun.Columns.PromoterK), new GroupBy(TicketRun.Columns.EventK));

				TicketRunSet ticketRunsWithoutTicketPromoterEvents = new TicketRunSet(ticketPromoterEventNotCreatedQuery);
				foreach (TicketRun ticketRunWithoutTicketPromoterEvent in ticketRunsWithoutTicketPromoterEvents)
				{
					TicketPromoterEvent ticketPromoterEvent = new TicketPromoterEvent();
					try
					{
						ticketPromoterEvent = new TicketPromoterEvent(ticketRunWithoutTicketPromoterEvent.PromoterK, ticketRunWithoutTicketPromoterEvent.EventK);
					}
					catch
					{
						ticketPromoterEvent.PromoterK = ticketRunWithoutTicketPromoterEvent.PromoterK;
						ticketPromoterEvent.EventK = ticketRunWithoutTicketPromoterEvent.EventK;
						ticketPromoterEvent.FundsLockText = "";
						ticketPromoterEvent.FundsReleased = false;
						ticketPromoterEvent.Update();

						if (!ticketPromoterEvent.Promoter.EnableTickets)
						{
							Utilities.EmailPromoterReminderToSubmitTicketApplicationForm(ticketPromoterEvent);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Utilities.AdminEmailAlert("Exception in TicketPromoterEvent.CreateTicketPromoterEventsWhenNeeded()", "Exception in TicketPromoterEvent.CreateTicketPromoterEventsWhenNeeded()", ex);
			}
		}

		public static void CalculateTicketsAndRunFundLocksChecksAndReleaseFunds(IEnumerable<TicketPromoterEvent> ticketPromoterEvents)
		{
			foreach (TicketPromoterEvent ticketPromoterEvent in ticketPromoterEvents)
			{
				if (!ticketPromoterEvent.FundsReleased && (ticketPromoterEvent.FundsTransferK == 0 || Math.Round(ticketPromoterEvent.GetTotalFunds(), 2) == 0))
				{
					ticketPromoterEvent.CalculateTicketsAndFunds();
					ticketPromoterEvent.RunFundsLockChecks();

					if (!ticketPromoterEvent.IsFundsLock)
					{
						ticketPromoterEvent.ReleaseFunds();
					}

					ticketPromoterEvent.Update();
				}
			}
		}
		#endregion

        #region Links

		public string ReadableReference
		{
			get 
			{
				return "Ticket funds invoice"; 
			}
		}

		public string Url(params string[] par)
		{
			return UrlReport(par);
		}

        public string UrlReport(params string[] par)
        {
			string[] fullParams = Cambro.Misc.Utility.JoinStringArrays(new string[] { "PK", this.PromoterK.ToString(), "K", this.EventK.ToString(), "type", "ticketfundsinvoice" }, par);
			return UrlInfo.PageUrl(UrlInfo.PageTypes.Blank, "reportgenerator", fullParams);
		}

        #endregion

        #region GenerateReportInHTML
        public StringBuilder GenerateReportStringBuilder(bool linksEnabled)
        {
            StringBuilder sb = new StringBuilder();
            string lineReturn = Vars.HTML_LINE_RETURN;

            if (this.FundsReleased && this.Promoter != null && this.Event != null && this.FundsTransfer != null)
            {
                this.CalculateTotalFundsAndVat();

                sb.Append(@"<form id='form1' runat='server'><div style='font-family:Verdana;'><table width='100%' border='0' cellspacing='0' cellpadding='0' height='100%'><tr><td valign='top'>
						<table width='100%'>");

                sb.Append(@"<tr>
                            <td valign='bottom'>" + Vars.DSI_POSTAL_DETAILS_HTML + "</td>");
                sb.Append(@"    <td align='right'>");
                sb.Append(@"    <table>
                                <tr>
                                    <td><b>");
                sb.Append(lineReturn);
                if (linksEnabled)
                    sb.Append(this.Promoter.Link());
                else
                    sb.Append(this.Promoter.Name);
                sb.Append("</b>");
                sb.Append(lineReturn);
                sb.Append(lineReturn);
                sb.Append(this.Promoter.AddressHtml);
                sb.Append(lineReturn);
                sb.Append(lineReturn);
                sb.Append("VAT #: ");
                if (this.Promoter.VatStatus == Promoter.VatStatusEnum.Registered)
                    sb.Append(this.Promoter.VatNumber);
                else if (this.Promoter.VatStatus == Promoter.VatStatusEnum.NotRegistered)
                    sb.Append("Not registered");
                else
//					sb.Append("UNKNOWN");
                    throw new DsiUserFriendlyException("VAT Status is unknown.");
                sb.Append(lineReturn);
                sb.Append(@"                <b>INVOICE</b>
                                    </td>
                                </tr>");
                sb.Append(@"        <tr>
                                    <td>Invoice&nbsp;date:</td>");
                sb.Append("             <td>" + this.FundsTransfer.DateTimeCreated.ToString("dd/MM/yyyy") + @"</td>
                                </tr>");
                sb.Append(@"        <tr>
                                    <td>Invoice&nbsp;number:</td>");
                sb.Append("             <td>" + this.FundsTransferK.ToString() + @"</td>
                                </tr>");
                sb.Append(@"        <tr>
                                    <td>Your&nbsp;account&nbsp;number:</td>");
                sb.Append("             <td>" + this.PromoterK.ToString() + @"</td>
                                </tr>
                            </table>");
                sb.Append(@" </td>
                        </tr>
                       </table>");
                sb.Append(lineReturn);
                sb.Append(lineReturn);
                sb.Append(lineReturn);

                sb.Append(lineReturn);
                sb.Append("<b>EVENT: ");
                if (linksEnabled)
                    sb.Append(this.Event.LinkFriendlyName);
                else
                    sb.Append(this.Event.FriendlyName);
                sb.Append("</b>");
                sb.Append(lineReturn);
                sb.Append(lineReturn);
                sb.Append(lineReturn);
                sb.Append(@"<table width='100%' cellspacing='0' cellpadding='3'  class='BorderBlack Top'>
							<tr>
								<td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='85'><b>Ticket&nbsp;run&nbsp;#</b></td>
                                <td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='365'><b>Details</b></td>
								<td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='40'><b>Sold</b></td>
								<td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='60'><b>Price</b></td>
								<td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='55'><b>VAT</b></td>
								<td style='vertical-align:bottom;' class='BorderBlack Bottom Left Right' width='65'><b>Total</b></td>
							</tr>");

				decimal totalPrice = 0;
				decimal totalVat = 0;
				decimal totalFunds = 0;
                int totalQuantity = 0;

                foreach (TicketRun tr in this.TicketRuns)
                {
                    sb.Append(@"<tr>
								<td class='BorderBlack Left'>" + tr.K.ToString() + @"</td>");
					sb.Append(@"    <td class='BorderBlack Left'>");
                    if (linksEnabled)
                        sb.Append(tr.LinkPriceBrandName);
                    else
                        sb.Append(tr.PriceBrandName);
                    sb.Append(@"    </td>");

					decimal price = 0;
					decimal vat = 0;
					decimal total = 0;
                    int quantity = 0;

                    foreach (Ticket ticket in tr.Tickets)
                    {
                        if (!ticket.CancelledBeforeFundsRelease)
                        {
                            quantity += ticket.Quantity;
                            price += ticket.InvoiceItem.Price;
                            vat += ticket.InvoiceItem.Vat;
                            total += ticket.InvoiceItem.Total;
                        }
                    }
                    if (quantity != tr.SoldTickets - tr.CancelledTicketBeforeFundReleaseQuantity)
                        throw new DsiUserFriendlyException("Error in calculating number of tickets sold. Please contact an administrator.");

					sb.Append(@"    <td class='BorderBlack Left' align='right'>" + quantity.ToString() + @"</td>");
					sb.Append(@"    <td class='BorderBlack Left' align='right'>" + Utilities.MoneyToHTML(price) + @"</td>");
					sb.Append(@"    <td class='BorderBlack Left' align='right'>" + Utilities.MoneyToHTML(vat) + @"</td>");
					sb.Append(@"    <td class='BorderBlack Left Right' align='right'>" + Utilities.MoneyToHTML(total) + @"</td>
                            </tr>");

                    totalQuantity += quantity;
                    totalPrice += price;
                    totalVat += vat;
                    totalFunds += total;
                }

				if (Math.Round(totalPrice, 2) != Math.Round(this.TotalFundsReleased - this.TotalVatReleased, 2) || Math.Round(totalVat, 2) != Math.Round(this.TotalVatReleased, 2) || Math.Round(totalFunds, 2) != Math.Round(this.TotalFundsReleased, 2) || totalQuantity != this.SoldTickets - this.TotalCancelledTicketsBeforeRelease)
                    throw new DsiUserFriendlyException("Error in calculating tickets funds. Please contact an administrator.");

				sb.Append(@"<tr style='border-top-width: 2px;' class='BorderBlack'>
								<td style='border-top-width: 2px;' class='BorderBlack' colspan='2'>&nbsp;</td>");
                sb.Append(@"    <td style='border-top-width: 2px; border-bottom-width: 2px;' class='BorderBlack Left' align='right'>" + totalQuantity.ToString() + @"</td>");
				sb.Append(@"    <td style='border-top-width: 2px; border-bottom-width: 2px;' class='BorderBlack Left' align='right'>" + Utilities.MoneyToHTML(this.TotalFundsReleased - this.TotalVatReleased) + @"</td>");
				sb.Append(@"    <td style='border-top-width: 2px; border-bottom-width: 2px;' class='BorderBlack Left' align='right'>" + Utilities.MoneyToHTML(this.TotalVatReleased) + @"</td>");
				sb.Append(@"    <td style='border-top-width: 2px; border-bottom-width: 2px;' class='BorderBlack Left Right' align='right'>" + Utilities.MoneyToHTML(this.TotalFundsReleased) + @"</td>
                            </tr>");

                if (this.Promoter.VatStatus == Promoter.VatStatusEnum.Registered)
                    sb.Append("<tr><td colspan='6' align='center'>THE VAT SHOWN IS YOUR OUTPUT TAX DUE TO HM REVENUE & CUSTOMS</td></tr>");
                        
                sb.Append("</table>");

				decimal appliedAmount = 0;
                if (this.FundsTransfer.InvoiceTransfers.Count > 0)
                {
                    sb.Append(lineReturn);
                    sb.Append(lineReturn);
                    sb.Append(lineReturn);

                    sb.Append(@"<table width='100%' cellspacing='0' cellpadding='3' class='BorderBlack Top Bottom Right'>
                            <tr>
								<td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='305'><b>Items&nbsp;Applied&nbsp;To</b></td>
                                <td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='65'><b>Tax&nbsp;Date</b></td>
								<td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='150'><b>Total Invoice Amount</b></td>
                                <td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='150'><b>Amount Applied To Invoice</b></td>
                            </tr>");

                    foreach (InvoiceTransfer invoiceTransfer in this.FundsTransfer.InvoiceTransfers)
                    {
                        Invoice invoice = new Invoice(invoiceTransfer.InvoiceK);
                        sb.Append(@"<tr>
                                <td class='BorderBlack Left'>");
                        if (linksEnabled)
                        {
                            sb.Append(invoice.Link());
                        }
                        else
                            sb.Append(invoice.TypeAndK);

                        // Replacing CreatedDateTime with TaxDateTime, as per Gee's request for OASIS v1.5
                        sb.Append(@"</td>
                                <td class='BorderBlack Left'>" + invoice.TaxDateTime.ToString("dd/MM/yy") + @"</td>
                                <td class='BorderBlack Left' align='right'>" + Utilities.MoneyToHTML(invoice.Total) + @"</td>
                                <td class='BorderBlack Left' align='right'>" + Utilities.MoneyToHTML(invoiceTransfer.Amount) + @"</td>
                            </tr>");
                        appliedAmount += Math.Abs(invoiceTransfer.Amount);
                    }

                    sb.Append(@"</table>");
                }

				decimal refundedAmount = 0;
                if (this.FundsTransfer.RefundTransfers.Count > 0)
                {
                    sb.Append(lineReturn);
                    sb.Append(lineReturn);
                    sb.Append(lineReturn);

					sb.Append(@"<table width='100%' cellspacing='0' cellpadding='3' class='BorderBlack Top Bottom Right'>
                        <tr>
							<td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='250'><b>" + this.FundsTransfer.RefundTransfers[0].TypeToString);

                    sb.Append(@"</b></td>
                            <td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='85'><b>Method</b></td>
                            <td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='160'><b>Method&nbsp;Ref#</b></td>
                            <td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='90'><b>Status</b></td>
                            <td style='vertical-align:bottom;' class='BorderBlack Bottom Left' align='left' width='85'><b>Amount</b></td>
                        </tr>");

                    foreach (Transfer refundTransfer in this.FundsTransfer.RefundTransfers)
                    {
                        sb.Append(@"<tr>
                            <td class='BorderBlack Left'>");
                        if (linksEnabled)
                            sb.Append(refundTransfer.Link());
                        else
                            sb.Append(refundTransfer.TypeAndK);

                        sb.Append(@"</td>
                            <td class='BorderBlack Left'>" + Utilities.CamelCaseToString(refundTransfer.Method.ToString()) + @"</td>" +
							   "<td class='BorderBlack Left'>");
                        sb.Append(refundTransfer.ReferenceNumberToHtml());
                        sb.Append(@"</td>
								<td class='BorderBlack Left'>" + refundTransfer.Status.ToString()
                                    + @"</td>
								<td class='BorderBlack Left' align='right'>" + Utilities.MoneyToHTML(refundTransfer.Amount) + @"</td></tr>");
                        refundedAmount += Math.Abs(refundTransfer.Amount);
                    }

                    sb.Append(@"</table><br>");
                }

                // TicketPromoterEvent Summary
                sb.Append(@"<br><table width='250'>
							<tr>
								<td colspan=2><b>Summary</b></td>
							</tr>
							<tr>
								<td width='135'>Ticket&nbsp;Sales&nbsp;Total:</td>
								<td width='115' align='right'>" + Utilities.MoneyToHTML(totalFunds) + @"</td>
							</tr>
							<tr>
								<td width='135'>Payment&nbsp;Total:</td>
								<td width='115' align='right'>" + Utilities.MoneyToHTML(appliedAmount + refundedAmount) + @"</td>
							</tr>
							<tr>
								<td width='135'><b>Outstanding:</b></td>                                
								<td width='115' align='right'><b>" + Utilities.MoneyToHTML(totalFunds - appliedAmount - refundedAmount) + @"</b></td>
							</tr></table>");

                sb.Append("</td></tr>");

                //                sb.Append(@"<tr><td valign='bottom'><hr></td></tr>");

                sb.Append(@"</table></div></form>");
            }

            return sb;
        }
        #endregion

		#region ConvertRemainingFundsToCampaignCredits
		public CampaignCredit ConvertRemainingFundsToCampaignCredits(Usr actionUsr)
		{
			if (actionUsr.IsAdmin && this.FundsReleased && this.FundsTransfer != null)
			{
				return this.FundsTransfer.PurchaseCampaignCreditsWithRemainingFunds(actionUsr, 0.5);
			}
			else
				return null;
		}
		#endregion
		#endregion
	}
	#endregion
}
