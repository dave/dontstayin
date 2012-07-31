using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs.DataHolders;
using Common;

namespace Bobs
{

    #region Ticket
    /// <summary>
    /// An individual ticket sold
    /// </summary>
    [Serializable]
	public partial class Ticket : IBuyable, IBobReport, IBobAsHTML, IHasAddress
    {

        #region Simple members
        /// <summary>
        /// Key
        /// </summary>
        public override int K
        {
			get { return this[Ticket.Columns.K] as int? ?? 0; }
            set { this[Ticket.Columns.K] = value; }
        }
        /// <summary>
        /// Ticket run link
        /// </summary>
        public override int TicketRunK
        {
            get { return (int)this[Ticket.Columns.TicketRunK]; }
            set { this[Ticket.Columns.TicketRunK] = value; }
        }
        /// <summary>
        /// Link to the event table
        /// </summary>
        public override int EventK
        {
            get { return (int)this[Ticket.Columns.EventK]; }
            set { this[Ticket.Columns.EventK] = value; }
        }
        /// <summary>
        /// The user that bought the ticket
        /// </summary>
        public override int BuyerUsrK
        {
            get { return (int)this[Ticket.Columns.BuyerUsrK]; }
            set { this[Ticket.Columns.BuyerUsrK] = value; }
        }
        /// <summary>
        /// Tickets that have been successfully processed should set Enabled = true. Tickets that are not enable
        /// </summary>
        public override bool Enabled
        {
            get { return (bool)this[Ticket.Columns.Enabled]; }
            set { this[Ticket.Columns.Enabled] = value; }
        }
        /// <summary>
        /// If the ticket has been cancelled
        /// </summary>
        public override bool Cancelled
        {
            get { return (bool)this[Ticket.Columns.Cancelled]; }
            set { this[Ticket.Columns.Cancelled] = value; }
        }
        /// <summary>
        /// Date time of the original purchase
        /// </summary>
        public override DateTime BuyDateTime
        {
            get { return (DateTime)this[Ticket.Columns.BuyDateTime]; }
            set { this[Ticket.Columns.BuyDateTime] = value; }
        }
        /// <summary>
        /// Address - Street
        /// </summary>
        public override string AddressStreet
        {
            get { return (string)this[Ticket.Columns.AddressStreet]; }
            set { this[Ticket.Columns.AddressStreet] = value; }
        }
        /// <summary>
        /// Address - Area
        /// </summary>
        public override string AddressArea
        {
            get { return (string)this[Ticket.Columns.AddressArea]; }
            set { this[Ticket.Columns.AddressArea] = value; }
        }
        /// <summary>
        /// Address - Place
        /// </summary>
        public override string AddressTown
        {
            get { return (string)this[Ticket.Columns.AddressTown]; }
            set { this[Ticket.Columns.AddressTown] = value; }
        }
        /// <summary>
        /// Address - County
        /// </summary>
        public override string AddressCounty
        {
            get { return (string)this[Ticket.Columns.AddressCounty]; }
            set { this[Ticket.Columns.AddressCounty] = value; }
        }
        /// <summary>
        /// Address - Postcode
        /// </summary>
        public override string AddressPostcode
        {
            get { return (string)this[Ticket.Columns.AddressPostcode]; }
            set { this[Ticket.Columns.AddressPostcode] = value; }
        }
        /// <summary>
        /// Address - Country (link to Country table)
        /// </summary>
        public override int AddressCountryK
        {
            get { return (int)this[Ticket.Columns.AddressCountryK]; }
            set { this[Ticket.Columns.AddressCountryK] = value; }
        }
        /// <summary>
        /// Full mobile number including country code (e.g. 447971597702)
        /// </summary>
        public override string Mobile
        {
            get { return (string)this[Ticket.Columns.Mobile]; }
            set { this[Ticket.Columns.Mobile] = value; }
        }
        /// <summary>
        /// Country code of mobile number (e.g. 44)
        /// </summary>
        public override string MobileCountryCode
        {
            get { return (string)this[Ticket.Columns.MobileCountryCode]; }
            set { this[Ticket.Columns.MobileCountryCode] = value; }
        }
        /// <summary>
        /// Mobile number excluding country code and leading zero (e.g. 7971597702)
        /// </summary>
        public override string MobileNumber
        {
            get { return (string)this[Ticket.Columns.MobileNumber]; }
            set { this[Ticket.Columns.MobileNumber] = value; }
        }
        /// <summary>
        /// First name, verified by credit card
        /// </summary>
        public override string FirstName
        {
            get { return (string)this[Ticket.Columns.FirstName]; }
            set { this[Ticket.Columns.FirstName] = value; }
        }
        /// <summary>
        /// Last name, verified by credit card
        /// </summary>
        public override string LastName
        {
            get { return (string)this[Ticket.Columns.LastName]; }
            set { this[Ticket.Columns.LastName] = value; }
        }
        /// <summary>
        /// Cryptographic hash of the card number
        /// </summary>
        public override Guid CardNumberHash
        {
            get { return Cambro.Misc.Db.GuidConvertor(this[Ticket.Columns.CardNumberHash]); }
            set { this[Ticket.Columns.CardNumberHash] = new System.Data.SqlTypes.SqlGuid(value); }
        }
        /// <summary>
        /// Last 6 digits of the card number used to order
        /// </summary>
        public override string CardNumberEnd
        {
            get { return (string)this[Ticket.Columns.CardNumberEnd]; }
            set { this[Ticket.Columns.CardNumberEnd] = value; }
        }
        /// <summary>
        /// Number of digits on the card used to order
        /// </summary>
        public override int CardNumberDigits
        {
            get { return (int)this[Ticket.Columns.CardNumberDigits]; }
            set { this[Ticket.Columns.CardNumberDigits] = value; }
        }
        /// <summary>
        /// Quantity of tickets (for multiple-entrance tickets)
        /// </summary>
        public override int Quantity
        {
            get { return (int)this[Ticket.Columns.Quantity]; }
            set { this[Ticket.Columns.Quantity] = value; }
        }
        /// <summary>
        /// Custom data specific to this ticket run (as a string)
        /// </summary>
        public override string CustomData
        {
            get { return (string)this[Ticket.Columns.CustomData]; }
            set { this[Ticket.Columns.CustomData] = value; }
        }
        /// <summary>
        /// Custom data specific to this ticket run (as xml)
        /// </summary>
        public override string CustomXml
        {
            get { return (string)this[Ticket.Columns.CustomXml]; }
            set { this[Ticket.Columns.CustomXml] = value; }
        }
        /// <summary>
        /// Link to the invoice item table - e.g. booking reference
        /// </summary>
        public override int InvoiceItemK
        {
            get { return (int)this[Ticket.Columns.InvoiceItemK]; }
            set { this[Ticket.Columns.InvoiceItemK] = value; }
        }
        /// <summary>
        /// Guid from the browser cookie
        /// </summary>
        public override Guid BrowserGuid
        {
            get { return Cambro.Misc.Db.GuidConvertor(this[Ticket.Columns.BrowserGuid]); }
            set { this[Ticket.Columns.BrowserGuid] = new System.Data.SqlTypes.SqlGuid(value); }
        }
        /// <summary>
        /// Price in pounds
        /// </summary>
        public override decimal Price
        {
			get { return (decimal)this[Ticket.Columns.Price]; }
            set { this[Ticket.Columns.Price] = value; }
        }
        /// <summary>
        /// Our booking fee
        /// </summary>
		public override decimal BookingFee
        {
			get { return (decimal)this[Ticket.Columns.BookingFee]; }
            set { this[Ticket.Columns.BookingFee] = value; }
        }
        /// <summary>
        /// Buyer's IpAddress
        /// </summary>
        public override string IpAddress
        {
            get { return (string)this[Ticket.Columns.IpAddress]; }
            set { this[Ticket.Columns.IpAddress] = value; }
        }
        /// <summary>
        /// Post event feedback enum: None=0, Good=1, Bad=2
        /// </summary>
        public override FeedbackEnum Feedback
        {
            get { return (FeedbackEnum)this[Ticket.Columns.Feedback]; }
            set { this[Ticket.Columns.Feedback] = value; }
        }
        /// <summary>
        /// Post event feedback text for negative comments
        /// </summary>
        public override string FeedbackNote
        {
            get { return (string)this[Ticket.Columns.FeedbackNote]; }
            set { this[Ticket.Columns.FeedbackNote] = value; }
        }
        /// <summary>
        /// Date time til when a pending ticket is reserved until
        /// </summary>
        public override DateTime ReserveDateTime
        {
            get { return (DateTime)this[Ticket.Columns.ReserveDateTime]; }
            set { this[Ticket.Columns.ReserveDateTime] = value; }
        }
		/// <summary>
		/// Random code generated for the ticket
		/// </summary>
		public override string Code
		{
			get { return (string)this[Ticket.Columns.Code]; }
			set { this[Ticket.Columns.Code] = value; }
		}
		/// <summary>
		/// Domain from which the request originated
		/// </summary>
		public override int DomainK
		{
			get { return (int)this[Ticket.Columns.DomainK]; }
			set { this[Ticket.Columns.DomainK] = value; }
		}
		/// <summary>
		/// Was this ticket cancelled / refunded before the promoter funds release event?
		/// </summary>
		public override bool CancelledBeforeFundsRelease
		{
			get { return (bool)this[Ticket.Columns.CancelledBeforeFundsRelease]; }
			set { this[Ticket.Columns.CancelledBeforeFundsRelease] = value; }
		}
		/// <summary>
		/// Date / time that the ticket was cancelled / refunded
		/// </summary>
		public override DateTime CancelledDateTime
		{
			get { return (DateTime)this[Ticket.Columns.CancelledDateTime]; }
			set { this[Ticket.Columns.CancelledDateTime] = value; }
		}
		/// <summary>
		/// CV2 Security code on the back on the credit card
		/// </summary>
		public override string CardCV2
		{
			get { return (string)this[Ticket.Columns.CardCV2]; }
			set { this[Ticket.Columns.CardCV2] = value; }
		}
		/// <summary>
		/// Has the promoter proven that the card was checked?
		/// </summary>
		public override bool CardCheckedByPromoter
		{
			get { return (bool)this[Ticket.Columns.CardCheckedByPromoter]; }
			set { this[Ticket.Columns.CardCheckedByPromoter] = value; }
		}
		/// <summary>
		/// How many times the promoter has attempted to confirm card details
		/// </summary>
		public override int CardCheckAttempts
		{
			get { return (int)this[Ticket.Columns.CardCheckAttempts]; }
			set { this[Ticket.Columns.CardCheckAttempts] = value; }
		}
		/// <summary>
		/// the name used on the card used to buy the tickets
		/// </summary>
		public override string AddressName
		{
			get { return (string)this[Ticket.Columns.AddressName]; }
			set { this[Ticket.Columns.AddressName] = value; }
		}
		/// <summary>
		/// Is this ticket suspected fraud?
		/// </summary>
		public override bool? IsFraud
		{
			get { return (bool?)this[Ticket.Columns.IsFraud]; }
			set { this[Ticket.Columns.IsFraud] = value; }
		}

		
        #endregion

		#region IBobAsHTML methods
		public string AsHTML()
		{
			string lineReturn = Vars.HTML_LINE_RETURN;
			StringBuilder sb = new StringBuilder();

			sb.Append(lineReturn);
			sb.Append(lineReturn);
			sb.Append("<u>Ticket details</u>");
			sb.Append(lineReturn);
			sb.Append("K: ");
			sb.Append(this.K.ToString());
			sb.Append(lineReturn);
			if (this.BuyerUsr != null)
			{
				sb.Append("Buyer usr: ");
				sb.Append(this.BuyerUsr.NickName);
				sb.Append(" (K: ");
				sb.Append(this.BuyerUsrK.ToString());
				sb.Append(")");
				sb.Append(lineReturn);
			}
			if (this.Event != null)
			{
				sb.Append("Event: ");
				sb.Append(this.Event.FriendlyName);
				sb.Append(" (K: ");
				sb.Append(this.EventK.ToString());
				sb.Append(")");
				sb.Append(lineReturn);
			}
			if (this.TicketRun != null)
			{
				sb.Append("Ticket run: ");
				sb.Append(this.TicketRun.PriceBrandName);
				sb.Append(" (K: ");
				sb.Append(this.TicketRunK.ToString());
				sb.Append(")");
				sb.Append(lineReturn);
			}
			sb.Append("First name: ");
			sb.Append(this.FirstName.ToString());
			sb.Append(lineReturn);
			sb.Append("Last name: ");
			sb.Append(this.LastName.ToString());
			sb.Append(lineReturn);
			sb.Append("Buy date: ");
			sb.Append(this.BuyDateTime.ToString("ddd dd/MM/yy HH:mm:ss"));
			sb.Append(lineReturn);
			sb.Append("Price: ");
			sb.Append(this.Price.ToString("c"));
			sb.Append(lineReturn);
			sb.Append("Booking fee: ");
			sb.Append(this.BookingFee.ToString("c"));
			sb.Append(lineReturn);
			sb.Append("Quantity: ");
			sb.Append(this.Quantity.ToString());
			sb.Append(lineReturn);
			sb.Append("InvoiceItemK: ");
			sb.Append(this.InvoiceItemK.ToString());
			sb.Append(lineReturn);
			if (this.Code.Length > 0)
			{
				sb.Append("Code: ");
				sb.Append(this.Code.ToString());
				sb.Append(lineReturn);
			}
			sb.Append("Card number end: ");
			sb.Append(this.CardNumberEnd.ToString());
			sb.Append(lineReturn);
			sb.Append("Card number hash: ");
			sb.Append(this.CardNumberHash.ToString());
			sb.Append(lineReturn);
			if (this.Cancelled)
			{
				sb.Append("<b>CANCELLED</b>");
				sb.Append(lineReturn);
			}
			
			return sb.ToString();
		}
		#endregion

		#region UpdateTicketsStatusOfEvents()
		public static void UpdateTicketsStatusOfEvents()
		{
			Transaction t = new Transaction();
			try
			{
			   Update clear = new Update();
			   clear.Changes.Add(new Assign(Event.Columns.IsTicketsAvailable, false));
			   clear.Table = TablesEnum.Event;
			   clear.Where = new Q(Event.Columns.IsTicketsAvailable, true);
			   Console.WriteLine("Cleared {0} row(s)", clear.Run(t));

			   Update up = new Update();
			   up.Table = TablesEnum.Event;
			   up.Changes.Add(new Assign(Event.Columns.IsTicketsAvailable, true));
			   up.Where = new Q(true);
			   up.From = new Join(
				   new TableElement(TablesEnum.Event),
				   new TableElement(TablesEnum.TicketRun),
				   QueryJoinType.Inner,
				   new And(
					   new Q(TicketRun.Columns.EndDateTime, QueryOperator.GreaterThan, DateTime.Now),
					   new Q(TicketRun.Columns.StartDateTime, QueryOperator.LessThanOrEqualTo, DateTime.Now),
					   new Q(TicketRun.Columns.SoldTickets, QueryOperator.LessThan, TicketRun.Columns.MaxTickets, true),
					   new Q(TicketRun.Columns.Paused, false),
					   new Q(Event.Columns.K, TicketRun.Columns.EventK, true)
				   )
			   );
			   Console.WriteLine("Updated {0} row(s)", up.Run(t));

			   t.Commit();
			}
			catch (Exception ex)
			{
			   t.Rollback();
			   Utilities.AdminEmailAlert("Exception updating TicketsAvailable flag on all events", "Exception updating TicketsAvailable flag on all events", ex);
			}
			finally
			{
			   t.Close();
			}

			Update clearHilight = new Update();
			clearHilight.Changes.Add(new Assign(Event.Columns.HasHilight, false));
			clearHilight.Table = TablesEnum.Event;
			clearHilight.Where = new And(
									new Q(Event.Columns.IsTicketsAvailable, false),
									new Q(Event.Columns.Donated, false),
									new Q(Event.Columns.HasHilight, true));
			Console.WriteLine("Cleared hilight on {0} row(s)", clearHilight.Run());


			Update setHilight = new Update();
			setHilight.Changes.Add(new Assign(Event.Columns.HasHilight, true));
			setHilight.Table = TablesEnum.Event;
			setHilight.Where = new And(
									new Or(new Q(Event.Columns.IsTicketsAvailable, true), new Q(Event.Columns.Donated, true)),
									new Q(Event.Columns.HasHilight, false));
			Console.WriteLine("Set hilight on {0} row(s)", setHilight.Run());

		}
		#endregion
		#region UpdateTicketHeatOfEvents()
		public static void UpdateTicketHeatOfEvents()
		{
			//UPDATE [Event] set [Event].[TicketHeat] = sumBookingFee FROM 
			//(SELECT [Ticket].[EventK], SUM([Ticket].[BookingFee]) AS sumBookingFee FROM [Ticket] WHERE [Ticket].[BuyDateTime]>DATEADD(day, -2, GETDATE()) AND [Ticket].[Enabled] = 1 GROUP BY [Ticket].[EventK]) t1 
			//INNER JOIN [Event] on t1.[EventK] = [Event].[K]

			Transaction t = new Transaction();
			try
			{
				Update clear = new Update();
				clear.Changes.Add(new Assign(Event.Columns.TicketHeat, 0.0));
				clear.Table = TablesEnum.Event;
				clear.Where = new Q(Event.Columns.TicketHeat, QueryOperator.GreaterThan, 0.0);
				Console.WriteLine("Cleared {0} row(s)", clear.Run(t));

				Update up = new Update();
				up.Table = TablesEnum.Event;
				up.Changes.Add(new Assign.Override(Event.Columns.TicketHeat, "sumBookingFee"));
				up.Where = new Q(true);
				up.From = new Join(
					new Join.StringOverride(
						"(SELECT [Ticket].[EventK], SUM([Ticket].[BookingFee]) AS sumBookingFee FROM [Ticket] WHERE [Ticket].[BuyDateTime]>DATEADD(day, -7, GETDATE()) AND [Ticket].[Enabled] = 1 GROUP BY [Ticket].[EventK]) t1"),
					new TableElement(TablesEnum.Event),
					QueryJoinType.Inner,
					new StringQueryCondition("t1.[EventK] = [Event].[K]"));
				
				Console.WriteLine("Updated {0} row(s)", up.Run(t));

				t.Commit();
			}
			catch (Exception ex)
			{
				t.Rollback();
				Utilities.AdminEmailAlert("Exception updating TicketHeat flag on all events", "Exception updating TicketHeat flag on all events", ex);
			}
			finally
			{
				t.Close();
			}

			try
			{
				Event ev1 = new Event(159336);
				ev1.TicketHeat = 1001;
				ev1.Update();
			}
			catch(Exception ex)
			{
				Utilities.AdminEmailAlert("Exception updating TicketHeat flag on CreamFields 159336", "Exception updating TicketHeat flag on CreamFields 159336", ex);
			}

			try
			{
				Event ev2 = new Event(166002);
				ev2.TicketHeat = 1000;
				ev2.Update();
			}
			catch (Exception ex)
			{
				Utilities.AdminEmailAlert("Exception updating TicketHeat flag on CreamFields 166002", "Exception updating TicketHeat flag on CreamFields 166002", ex);
			}

			

			
			
		}
		#endregion

		#region Enums
        public static ListItem[] FeedbackAsListItemArray()
        {
            List<ListItem> ListItems = new List<ListItem>();
            ListItems.Add(new ListItem(FeedbackEnum.None.ToString(), Convert.ToInt32(FeedbackEnum.None).ToString()));
            ListItems.Add(new ListItem(FeedbackEnum.Good.ToString(), Convert.ToInt32(FeedbackEnum.Good).ToString()));
            ListItems.Add(new ListItem(FeedbackEnum.Bad.ToString(), Convert.ToInt32(FeedbackEnum.Bad).ToString()));

            return ListItems.ToArray();
        }
        #endregion

        #region Constants + Readonly
        public const string ETICKET_CARD_REMINDER_SINGULAR = "Ticket collection at the event. When you arrive at the event, you must show the credit/debit card used to buy the ticket";
        public static readonly string ETICKET_CARD_REMINDER_PLURAL = ETICKET_CARD_REMINDER_SINGULAR + "s";
		public const string ETICKET_CODE_REMINDER_SINGULAR = "You must also show your ticket code";
		public static readonly string ETICKET_CODE_REMINDER_PLURAL = ETICKET_CODE_REMINDER_SINGULAR + "s";

        public const string ETICKET_CARD_REMINDER_NOT_LET_YOU_IN = "The venue/promoter will NOT let you in without the card you used to pay.";
        public static readonly string ETICKET_REMINDER_HTML = "<b>" + ETICKET_CARD_REMINDER_PLURAL + "</b>.<br>" + ETICKET_CARD_REMINDER_NOT_LET_YOU_IN;
		public const decimal DEFAULT_REFUND_CHARGE = 1m;
        #endregion

        #region Properties
        #region CurrentTicketsQ
        public static Q CurrentTicketsQ
        {
            get
            {
                return new And(new Q(Ticket.Columns.Quantity, QueryOperator.GreaterThan, 0),
                               new Or(new Q(Ticket.Columns.Enabled, true),
                                      new Q(Ticket.Columns.ReserveDateTime, QueryOperator.GreaterThanOrEqualTo, DateTime.Now.AddSeconds(-1 * Vars.TICKETS_RESERVE_SECONDS))));
                //new Or(new Q(Ticket.Columns.Cancelled, 0),
                //       new Q(Ticket.Columns.Cancelled, QueryOperator.IsNull, null)));
            }
        }
        #endregion
        #region SoldTicketsQ
        public static Q SoldTicketsQ
        {
            get
            {
                return new And(new Q(Ticket.Columns.Enabled, true),
                               new Q(Ticket.Columns.Quantity, QueryOperator.GreaterThan, 0));
            }
        }
        #endregion
        #region ValidTicketsQ
        public static Q ValidTicketsQ
        {
            get
            {
                return new And(new Q(Ticket.Columns.Enabled, true),
                               new Q(Ticket.Columns.Quantity, QueryOperator.GreaterThan, 0),
                               new Or(new Q(Ticket.Columns.Cancelled, 0),
                                      new Q(Ticket.Columns.Cancelled, QueryOperator.IsNull, null)));
            }
        }
        #endregion
        #region CancelledTicketsQ
        public static Q CancelledTicketsQ
        {
            get
            {
                return new And(new Q(Ticket.Columns.Enabled, true),
                               new Q(Ticket.Columns.Quantity, QueryOperator.GreaterThan, 0),
                               new Q(Ticket.Columns.Cancelled, 1));
            }
        }
        #endregion
		#region CancelledTicketsBeforeFundsReleaseQ
		public static Q CancelledTicketsBeforeFundsReleaseQ
		{
			get
			{
				return new And(new Q(Ticket.Columns.Enabled, true),
							   new Q(Ticket.Columns.Quantity, QueryOperator.GreaterThan, 0),
							   new Q(Ticket.Columns.CancelledBeforeFundsRelease, 1));
			}
		}
		#endregion

        #region AddressInHtml
        public string AddressInHtml
        {
            get
            {
                string addressInHtml = "";
                addressInHtml += AddressStreet.Length > 0 ? AddressStreet + "<br>" : "";
                addressInHtml += AddressTown.Length > 0 ? AddressTown + "<br>" : "";
                addressInHtml += AddressArea.Length > 0 ? AddressArea + "<br>" : "";
                addressInHtml += AddressCounty.Length > 0 ? AddressCounty + "<br>" : "";
                addressInHtml += AddressPostcode.Length > 0 ? AddressPostcode + "<br>" : "";
                try
                {
                    addressInHtml += AddressCountryK > 0 ? new Country(AddressCountryK).Name + "<br>" : "";
                }
                catch
                { }

                if (addressInHtml.Length > 4)
                    addressInHtml = addressInHtml.Substring(0, addressInHtml.Length - 4);

                return addressInHtml;
            }
        }
        #endregion

        #region Description
        public string Description
        {
            get
            {
                return this.Quantity.ToString() + " x " + this.TicketRun.PriceEventAndName;
            }
        }

        #endregion

        #region ShortDescription
        public string ShortDescription
        {
            get
            {
                return this.Quantity.ToString() + " x " + (this.TicketRun.Name.Length > 0 ? this.TicketRun.Name : "Ticket");
            }
        }

        #endregion

		#region QuantityPriceName
		public string QuantityPriceName
		{
			get
			{
				return this.Quantity.ToString() + " x " + this.TicketRun.PriceBrandName;
			}
		}
		#endregion

        #region Event
        public Event Event
        {
            get
            {
                if (_Event == null && EventK > 0)
                    _Event = new Event(EventK, this, Ticket.Columns.EventK);
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
        #region TicketRun
        public TicketRun TicketRun
        {
            get
            {
                if (ticketRun == null && TicketRunK > 0)
                    ticketRun = new TicketRun(TicketRunK, this, Ticket.Columns.TicketRunK);
                return ticketRun;
            }
            set
            {
                ticketRun = value;
                if (ticketRun != null)
                    this.TicketRunK = ticketRun.K;
                else
                    this.TicketRunK = 0;
            }
        }
        TicketRun ticketRun;
        #endregion

        #region BuyerUsr
        public Usr BuyerUsr
        {
            get
            {
                if (buyerUsr == null && BuyerUsrK > 0)
                {
                    buyerUsr = new Usr(BuyerUsrK);
                }
                return buyerUsr;
            }
            set
            {
                buyerUsr = value;
                if (buyerUsr != null)
                    this.BuyerUsrK = buyerUsr.K;
                else
                    this.BuyerUsrK = 0;
            }
        }
        Usr buyerUsr;
        #endregion

        #region Invoice
        public Invoice Invoice
        {
            get
            {
                if (invoice == null && InvoiceItemK > 0)
                {
                    Query ticketInvoiceQuery = new Query(new Q(InvoiceItem.Columns.K, this.InvoiceItemK));
                    ticketInvoiceQuery.TableElement = new Join(Bobs.Invoice.Columns.K, InvoiceItem.Columns.InvoiceK);
                    InvoiceSet invoices = new InvoiceSet(ticketInvoiceQuery);
                    if (invoices.Count > 0)
                        invoice = invoices[0];
                }
                return invoice;
            }
            set
            {
                invoice = null;
            }
        }
        Invoice invoice;
        #endregion

        #region InvoiceItem
        public InvoiceItem InvoiceItem
        {
            get
            {
                if (invoiceItem == null && InvoiceItemK > 0)
                {
                    invoiceItem = new InvoiceItem(InvoiceItemK);
                }
                return invoiceItem;
            }
            set
            {
                invoiceItem = value;
                if (invoiceItem != null)
                    this.InvoiceItemK = invoiceItem.K;
                else
                    this.InvoiceItemK = 0;
            }
        }
        InvoiceItem invoiceItem;
        #endregion
        #endregion

        #region Methods
        #region IsUsrAllowedAccess
        public bool IsUsrAllowedAccess(Usr usr)
        {
            return usr.IsAdmin || (this.BuyerUsrK > 0 && this.BuyerUsrK == usr.K);
        }
        #endregion

        #region VerifyTicketPurchase
        public bool VerifyTicketPurchase()
        {
			List<IBobAsHTML> bobsAsHTML = new List<IBobAsHTML>();

			if ((this.Enabled || this.Cancelled) && (this.Invoice == null || this.CardNumberEnd.Length == 0 || this.FirstName.Length == 0 || this.LastName.Length == 0))
			{
				string oldTicketHTML = this.AsHTML();
				string ticketFromDataBaseHTML = "Unable to retrieve ticket from database.";
				try
				{
					// Get fresh data from database and compare to MemCached Ticket
					TicketSet ticketsFromDatabase = new TicketSet(new Query(new Q(Ticket.Columns.K, this.K)));
					
					if (ticketsFromDatabase.Count == 1)
					{
						ticketFromDataBaseHTML = ticketsFromDatabase[0].AsHTML();
						this.InvoiceItemK = ticketsFromDatabase[0].InvoiceItemK;
						this.FirstName = ticketsFromDatabase[0].FirstName;
						this.LastName = ticketsFromDatabase[0].LastName;
						this.CardNumberEnd = ticketsFromDatabase[0].CardNumberEnd;
						this.CardCV2 = ticketsFromDatabase[0].CardCV2;
						this.Update();
						//if (this.InvoiceItemK != ticketsFromDatabase[0].InvoiceItemK || this.Enabled != ticketsFromDatabase[0].Enabled || this.Cancelled != ticketsFromDatabase[0].Cancelled || Math.Round(this.Price, 2) != Math.Round(ticketsFromDatabase[0].Price, 2))
						//{	
						//    bobsAsHTML.Add(this);
						//    bobsAsHTML.Add(ticketsFromDatabase[0]);
						//    Utilities.AdminEmailAlert("<p>MemCache and database do not match for ticket.</p><p>TicketK= " + this.K.ToString() + ", InvoiceItemK= " + this.InvoiceItemK.ToString() + "</p>", "Error with MemCache", new DSIUserFriendlyException("Error with MemCache"), bobsAsHTML);
						//}
					}

					if (this.InvoiceItemK == 0 || this.CardNumberEnd.Length == 0 || this.FirstName.Length == 0 || this.LastName.Length == 0)
					{
						bobsAsHTML.Clear();
						Query ticketInvoiceQuery = new Query(new And(new Q(Invoice.Columns.UsrK, this.BuyerUsrK),
																	 //new Q(InvoiceItem.Columns.Total, this.Price),
																	 new Q(InvoiceItem.Columns.BuyableObjectType, Convert.ToInt32(Model.Entities.ObjectType.Ticket)),
																	 new Q(InvoiceItem.Columns.BuyableObjectK, this.K),
																	 new Q(Invoice.Columns.Paid, 1),
																	 new Q(Invoice.Columns.PaidDateTime, QueryOperator.GreaterThanOrEqualTo, this.BuyDateTime.AddMinutes(-8)),
																	 new Q(Invoice.Columns.PaidDateTime, QueryOperator.LessThanOrEqualTo, this.BuyDateTime.AddMinutes(8))));
						ticketInvoiceQuery.TableElement = new Join(Invoice.Columns.K, InvoiceItem.Columns.InvoiceK);
						InvoiceSet ticketInvoice = new InvoiceSet(ticketInvoiceQuery);

						if (ticketInvoice.Count == 1 && ticketInvoice[0].Items.Count > 0)
						{
							if (this.InvoiceItemK == 0)
							{
								foreach (InvoiceItem ii in ticketInvoice[0].Items)
								{
									if (ii.Type == InvoiceItem.Types.EventTickets && ii.KeyData == this.K && ii.BuyableObjectType == Model.Entities.ObjectType.Ticket && ii.BuyableObjectK == this.K && Math.Round(ii.Total, 2) == Math.Round(this.Price))
										this.InvoiceItemK = ii.K;
								}
							}
							bobsAsHTML.Add(this);
							bobsAsHTML.Add(ticketInvoice[0]);

							if (ticketInvoice[0].SuccessfulAppliedTransfers.Count == 1)
							{
								bobsAsHTML.Add(ticketInvoice[0].SuccessfulAppliedTransfers[0]);

								if (this.CardNumberEnd.Length == 0)
								{
									this.CardNumberEnd = ticketInvoice[0].SuccessfulAppliedTransfers[0].CardNumberEnd;
									this.CardNumberDigits = ticketInvoice[0].SuccessfulAppliedTransfers[0].CardDigits;
									this.CardCV2 = ticketInvoice[0].SuccessfulAppliedTransfers[0].CardCV2;
								}
								if (this.AddressPostcode.Length == 0)
								{
									this.AddressPostcode = ticketInvoice[0].SuccessfulAppliedTransfers[0].CardPostcode;
								}
								if (this.AddressStreet.Length == 0)
								{
									this.AddressStreet = ticketInvoice[0].SuccessfulAppliedTransfers[0].CardAddress1;
								}
								if (this.FirstName != Cambro.Misc.Utility.Snip(Utilities.GetFirstName(ticketInvoice[0].SuccessfulAppliedTransfers[0].CardName), 100))
								{
									this.FirstName = Cambro.Misc.Utility.Snip(Utilities.GetFirstName(ticketInvoice[0].SuccessfulAppliedTransfers[0].CardName), 100);
								}
								if (this.LastName != Cambro.Misc.Utility.Snip(Utilities.GetLastName(ticketInvoice[0].SuccessfulAppliedTransfers[0].CardName), 100))
								{
									this.LastName = Cambro.Misc.Utility.Snip(Utilities.GetLastName(ticketInvoice[0].SuccessfulAppliedTransfers[0].CardName), 100);
								}
								this.Update();
							}

							Utilities.AdminEmailAlert("<p>Ticket and invoice did not match, but have been auto fixed.</p><p>Please verify manually.</p><p>TicketK= " + this.K.ToString() + ", InvoiceItemK= " + this.InvoiceItemK.ToString() + "</p>" + oldTicketHTML + ticketFromDataBaseHTML,
													  "Ticket auto fixed #" + this.K.ToString(), new DsiUserFriendlyException("Ticket auto fixed"), bobsAsHTML, new string[] { Vars.EMAIL_ADDRESS_TIMI });
						}
						else
						{
							Utilities.AdminEmailAlert("<p>Ticket and invoice did not match, and have not been fixed. Unable to find invoice from database</p><p>Please verify manually.</p><p>TicketK= " + this.K.ToString() + ", InvoiceItemK= " + this.InvoiceItemK.ToString() + "</p>" + oldTicketHTML + ticketFromDataBaseHTML,
													  "Error not fixed with ticket #" + this.K.ToString(), new DsiUserFriendlyException("Ticket not fixed"), bobsAsHTML, new string[] { Vars.EMAIL_ADDRESS_TIMI });
						}
					}
				}
				catch { }
				if (this.InvoiceItemK == 0 || this.CardNumberEnd.Length == 0 || this.FirstName.Length == 0 || this.LastName.Length == 0)
				{
					Utilities.AdminEmailAlert("<p>Ticket information missing.</p><p>Please verify manually.</p><p>TicketK= " + this.K.ToString() + ", InvoiceItemK= " + this.InvoiceItemK.ToString() + "</p>",
											  "Error with Ticket #" + this.K.ToString(), new DsiUserFriendlyException("Error with ticket"), new List<IBobAsHTML>(){this}, new string[] { Vars.EMAIL_ADDRESS_TIMI });
				}
			}
			if ((this.Enabled || this.Cancelled) && this.Invoice != null && !this.Invoice.Paid)
			{
				bobsAsHTML.Clear();
				bobsAsHTML.Add(this);
				bobsAsHTML.Add(this.Invoice);

				Utilities.AdminEmailAlert("<p>Error occurred in VerifyTicketPurchase. Invoice was not fully paid for Ticket #" + this.K.ToString() + "</p>", 
										  "Error occurred in VerifyTicketPurchase. Invoice was not fully paid for Ticket #" + this.K.ToString(), new Exception(), bobsAsHTML);
			}

            if (!this.Enabled)
                throw new DsiUserFriendlyException("Ticket not valid.");

            if (this.Cancelled)
                throw new DsiUserFriendlyException("Ticket has been cancelled.");

            return true;
        }
        #endregion

        public void SetBrowserGuidToDsiCookieGuid()
        {
            this.BrowserGuid = Visit.HasCurrent ? Visit.Current.Guid : Guid.Empty;
        }

        public void SetIpAddressAndCountry()
        {
			if(Visit.HasCurrent)
			{
				this.IpAddress = Visit.Current.IpAddress;
				try
				{
					if(!Vars.DevEnv)
						this.AddressCountryK = IpCountry.Lookup(this.IpAddress).CountryK;
				}
				catch (Exception ex)
				{
					//Utilities.AdminEmailAlert("Exception in IpCountry.Lookup().CountryK for IpAddress: " + this.IpAddress, "Exception in IpCountry.Lookup", ex);
				}
			}
			else
				this.IpAddress = "0.0.0.0";
        }

        private void AdminEmailAlertWrapper(string subject)
        {
            Utilities.AdminEmailAlert("<p>" + subject + "</p><p>EventK=" + this.EventK.ToString() + "<br>TicketRunK=" + this.TicketRunK.ToString()
                                    + "<br>BuyerUsrK=" + this.BuyerUsrK.ToString() + "<br>Quantity=" + this.Quantity.ToString() + "<br>Card Number End=" + this.CardNumberEnd
                                    + "<br>Name=" + this.FirstName + " " + this.LastName + "</p>", subject, new Exception(), this);
        }

        public static int GetQuantity(TicketSet tickets)
        {
            int quantity = 0;
            foreach (Ticket ticket in tickets)
            {
                quantity += ticket.Quantity;
            }
            return quantity;
        }

        public void Reserve()
        {
            this.ReserveDateTime = DateTime.Now;
            this.Update();
        }
        public void Unreserve()
        {
            this.ReserveDateTime = DateTime.Now.AddSeconds(-1 * Vars.TICKETS_RESERVE_SECONDS - 10); ;
            this.Update();
        }

		public void SetRandomCode()
		{
			this.Code = Common.ThreadSafeRandom.Next(1000, 9999).ToString("0000");
		}
        #endregion

        #region IBuyable Methods + Properties
        public DateTime BuyableLockDateTime
        {
            get
            {
                return this.BuyDateTime;
            }
            set
            {
                this.BuyDateTime = value;
            }
        }

        /// <summary>
        /// Queries database to retrieve the latest BuyableLockDateTime. Only returns if there is a lock within the Vars.IBUYABLE_LOCK_SECONDS
        /// NOTE: For tickets this applies to check if the number of tickets that are in the process of being paid for will prevent the current ticket purchase from going through.
        /// </summary>
        public bool IsLocked
        {
            get
            {
                if (K == 0)
                    return false;

                Query iBuyableLockDateTimeQuery = new Query(new And(new Q(Ticket.Columns.K, this.K),
                                                                    new And(new Q(Ticket.Columns.BuyDateTime, QueryOperator.GreaterThanOrEqualTo, DateTime.Now.AddSeconds(-1 * Vars.IBUYABLE_LOCK_SECONDS)),
                                                                            new Q(Ticket.Columns.Enabled, false))));
                iBuyableLockDateTimeQuery.Columns = new ColumnSet(Ticket.Columns.BuyDateTime, Ticket.Columns.Enabled);

                TicketSet lockedBuyableSet = new TicketSet(iBuyableLockDateTimeQuery);
                if (lockedBuyableSet.Count > 0)
                {
                    this.BuyableLockDateTime = lockedBuyableSet[0].BuyDateTime;
                    this.Enabled = lockedBuyableSet[0].Enabled;
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
        /// <param name="total">InvoiceItem.Total</param>
        /// <returns></returns>
		public bool VerifyPrice(InvoiceItem.Types invoiceItemType, decimal price, decimal total)
        {
            if (invoiceItemType.Equals(InvoiceItem.Types.EventTickets))
            {
                return Math.Round(total, 2) == Math.Round(this.TicketRun.Price * this.Quantity, 2);
            }
            else if (invoiceItemType.Equals(InvoiceItem.Types.EventTicketsBookingFee))
            {
                return Math.Round(total, 2) == Math.Round(this.TicketRun.BookingFee * this.Quantity, 2);
            }
            else
                throw new Exception("invalid invoice item type: " + Utilities.CamelCaseToString(invoiceItemType.ToString()));
        }

        /// <summary>
        /// Checks if the IBuyable Bob is ready to be processed. This is used as a pre-purchasing check.
        /// Verifies if the Ticket Run is still running
        /// Verifies if the Ticket Run has enough tickets remaining
        /// Verifies if the Ticket price and booking fee havent been changed
        /// Verifies that the usr does not exceed the max tickets per usr
        /// Verifies that the usr's card does not exceed the max tickets per card
        /// </summary>
        /// <param name="invoiceItemType">InvoiceItem.Type</param>
        /// <param name="price">InvoiceItem.Price</param>
        /// <returns></returns>
		public bool IsReadyForProcessing(InvoiceItem.Types invoiceItemType, decimal price, decimal total)
        {
            if (invoiceItemType.Equals(InvoiceItem.Types.EventTickets))
            {
                if (this.Enabled)
                    throw new DsiUserFriendlyException("This ticket has already been purchased.");

                if (this.Cancelled)
                    throw new DsiUserFriendlyException("This ticket has already been cancelled.");

                this.TicketRun = new TicketRun(this.TicketRunK);

                if (!this.TicketRun.Status.Equals(Bobs.TicketRun.TicketRunStatus.Running))
                {
                    throw new DsiUserFriendlyException("This ticket run is not currently selling tickets. Status: " + Utilities.CamelCaseToString(this.TicketRun.Status.ToString()));
                }

                if (!VerifyPrice(invoiceItemType, price, total))
                {
                    throw new DsiUserFriendlyException("Price wrong! Please restart and try again.");
                }

                // Update the BuyableLockDateTime, as the time between the Ticket saved and the user paying for it is not determined. This will re-lock the ticket for a further period.
                this.Reserve();
                

                // As this Ticket has been entered in the database and will be counted when other people are trying to purchase tickets it is calculated in the CurrentNumberOfTicketsSold, so we need to remove Ticket.Quantity.
                int currentNumberOfTicketsSold = this.TicketRun.CurrentNumberOfTicketsSold - this.Quantity;
                int ticketsRemaining = this.TicketRun.MaxTickets - currentNumberOfTicketsSold;
                if (ticketsRemaining < this.Quantity)
                {
                    if (ticketsRemaining <= 0)
                    {
                        if (this.TicketRun.SoldTickets >= this.TicketRun.MaxTickets)
                        {
                            if (this.TicketRun.SoldTickets > this.TicketRun.MaxTickets)
                                this.AdminEmailAlertWrapper("Exception in Ticket.IsReadyForProcessing(): TicketRunK= " + this.TicketRunK.ToString() + ", SoldTickets= " + this.TicketRun.SoldTickets.ToString() + ", MaxTickets= " + this.TicketRun.MaxTickets.ToString());

                            throw new DsiUserFriendlyException("No tickets left for this ticket run.");
                        }

                        throw new DsiUserFriendlyException("No tickets currently available for this ticket run. " + Vars.TICKETS_PLEASE_TRY_AGAIN_IN);
                    }
                    else
                        throw new DsiUserFriendlyException("Only " + ticketsRemaining.ToString() + " ticket" + (ticketsRemaining > 1 ? "s" : "") + " currently available for this ticket run.");
                }

                // As this Ticket has been entered in the database and will be counted if usr is trying to purchase tickets in another window, so it is calculated in the CurrentTicketsSoldForUsr, so we need to remove Ticket.Quantity.
                int usrEventTickets = this.Event.TicketsSoldForUsr(this.BuyerUsrK) + this.Event.TicketsAwaitingPaymentForUsrTotal(this.BuyerUsrK) - this.Quantity;
                if (usrEventTickets >= Vars.TICKETS_MAX_PER_USR)
                {
                    if (usrEventTickets > Vars.TICKETS_MAX_PER_USR)
                    {
                        this.AdminEmailAlertWrapper("Exception in Ticket.IsReadyForProcessing(): Usr trying to exceed max tickets per user. Usr event tickets= " + this.Event.TicketsSoldForUsr(this.BuyerUsrK).ToString() + ", usr tickets awaiting payment= " + this.Event.TicketsAwaitingPaymentForUsrTotal(this.BuyerUsrK).ToString());
                    }
                    throw new DsiUserFriendlyException("You have reached the ticket limit for this event. You cannot buy anymore tickets for this event.");
                }
                else if (usrEventTickets + this.Quantity > Vars.TICKETS_MAX_PER_USR)
                {
                    throw new DsiUserFriendlyException("This will exceed your ticket limit for this event. You can only purchase " + ((int)(Vars.TICKETS_MAX_PER_USR - usrEventTickets)).ToString() + " more ticket" + (Vars.TICKETS_MAX_PER_USR - usrEventTickets > 1 ? "s" : "") + " for this event.");
                }

                // As this Ticket has been entered in the database and will be counted if usr is trying to purchase tickets in another window, so it is calculated in the CurrentTicketsSoldForUsr, so we need to remove Ticket.Quantity.
                int cardEventTickets = this.Event.TicketsSoldForCard(this.CardNumberHash) + this.Event.TicketsAwaitingPaymentForCardTotal(this.CardNumberHash) - this.Quantity;
                if (cardEventTickets >= Vars.TICKETS_MAX_PER_CARD)
                {
                    if (cardEventTickets > Vars.TICKETS_MAX_PER_CARD)
                        this.AdminEmailAlertWrapper("Exception in Ticket.IsReadyForProcessing(): Usr trying to exceed max tickets per card. Card event tickets= " + this.Event.TicketsSoldForCard(this.CardNumberHash).ToString() + ", card tickets awaiting payment= " + this.Event.TicketsAwaitingPaymentForCardTotal(this.CardNumberHash).ToString());

                    if (this.Event.TicketsSoldForCard(this.CardNumberHash) >= Vars.TICKETS_MAX_PER_CARD)
                        throw new DsiUserFriendlyException("You have reached the ticket limit for this event. You cannot buy anymore tickets for this event with this card.");
                    else
                        throw new DsiUserFriendlyException("You cannot buy anymore tickets for this event right now with this card. " + Vars.TICKETS_PLEASE_TRY_AGAIN_IN);
                }
                else if (cardEventTickets + this.Quantity > Vars.TICKETS_MAX_PER_CARD)
                {
                    throw new DsiUserFriendlyException("This will exceed your ticket limit for this event. You can only purchase " + ((int)(Vars.TICKETS_MAX_PER_CARD - cardEventTickets)).ToString() + " more ticket" + (Vars.TICKETS_MAX_PER_CARD - cardEventTickets > 1 ? "s" : "") + " for this event with this card.");
                }

                return true;
            }
            else if (invoiceItemType.Equals(InvoiceItem.Types.EventTicketsBookingFee))
            {
                if (!VerifyPrice(invoiceItemType, price, total))
                {
                    throw new DsiUserFriendlyException("Booking fee wrong! Please restart and try again.");
                }
                return true;
            }
            else
                throw new Exception("invalid invoice item type: " + Utilities.CamelCaseToString(invoiceItemType.ToString()));
        }

        /// <summary>
        /// Processes the IBuyable Bob. For banners, it verifies that the banner IsReadyForProcessing. If yes, then it sets banner status to Booked, stores the price, and updates the banner.
        /// </summary>
        /// <param name="invoiceItemType">InvoiceItem.Type</param>
        /// <param name="price">InvoiceItem.Price</param>
        /// <returns></returns>
		public bool Process(InvoiceItem.Types invoiceItemType, decimal price, decimal total)
        {
            if (invoiceItemType.Equals(InvoiceItem.Types.EventTickets))
            {
                if (IsReadyForProcessing(invoiceItemType, price, total))
                {
                    try
                    {
                        this.Enabled = true;
                        this.BuyDateTime = DateTime.Now;
						try
						{
							if (this.TicketRun.Promoter.AddRandomCodeToTickets)
								this.SetRandomCode();
						}
						catch { }
                        this.Update();
                    }
                    catch {}

                    this.TicketRun.CalculateSoldTicketsAndUpdate();

					try
					{
						if (this.BuyerUsr.FacebookConnected && this.BuyerUsr.FacebookStoryBuyTicket)
						{
							FacebookPost.CreateBuyTicket(this.BuyerUsr, this.Event);
						}
					}
					catch { }
                }
            }
            else if (invoiceItemType.Equals(InvoiceItem.Types.EventTicketsBookingFee))
            {
                IsReadyForProcessing(invoiceItemType, price, total);
            }

            return IsProcessed(invoiceItemType);
        }

	 

        /// <summary>
        /// Unprocesses the IBuyable Bob. For banners, it sets the event donation off, and updates the event.
        /// </summary>
        /// <param name="invoiceItemType">InvoiceItem.Type</param>
        /// <returns></returns>
        public bool Unprocess(InvoiceItem.Types invoiceItemType)
        {
            if (invoiceItemType.Equals(InvoiceItem.Types.EventTickets))
            {
                this.Unlock();
                this.TicketRun.CalculateSoldTicketsAndUpdate();

                return !IsProcessed(invoiceItemType);
            }
            else
                throw new Exception("invalid invoice item type: " + Utilities.CamelCaseToString(invoiceItemType.ToString()));
        }

        /// <summary>
        /// Verifies if the IBuyable Bob has already been processed successfully.
        /// </summary>
        /// <param name="invoiceItemType">InvoiceItem.Type</param>
        /// <returns></returns>
        public bool IsProcessed(InvoiceItem.Types invoiceItemType)
        {
            if (invoiceItemType.Equals(InvoiceItem.Types.EventTickets))
            {
                return this.Enabled && this.K > 0 && this.Quantity > 0;
            }
            else if (invoiceItemType.Equals(InvoiceItem.Types.EventTicketsBookingFee))
            {
                return this.K > 0;
            }
            else
                throw new Exception("invalid invoice item type: " + Utilities.CamelCaseToString(invoiceItemType.ToString()));
        }

        /// <summary>
        /// Sets the IBuyable Bob field BuyableLockDateTime to DateTime.Now and updates the Bob
        /// </summary>
        public void Lock()
        {
            if (!this.IsProcessed(InvoiceItem.Types.EventTickets))
            {
                this.ReserveDateTime = DateTime.Now;
                this.BuyableLockDateTime = DateTime.Now;
                this.Update();
            }
        }

        /// <summary>
        /// Unlocks the IBuyable Ticket record
        /// </summary>
        public void Unlock()
        {
            this.BuyableLockDateTime = DateTime.Now.AddSeconds(-1 * Vars.IBUYABLE_LOCK_SECONDS - 10);
            this.ReserveDateTime = DateTime.Now.AddSeconds(-1 * Vars.TICKETS_RESERVE_SECONDS - 10); ; ;
            this.Enabled = false;
            this.Update();
        }

        #endregion

        #region Link + Url

		public string UrlReport(params string[] par)
        {
			string[] fullParams = Cambro.Misc.Utility.JoinStringArrays(new string[] { "K", this.K.ToString(), "type", "ticket" }, par);
            return UrlInfo.PageUrl(UrlInfo.PageTypes.Blank, "reportgenerator", fullParams);
        }

		public string UrlAdmin(params string[] par)
        {
			string[] fullParams = Cambro.Misc.Utility.JoinStringArrays(new string[] { "TicketK", this.K.ToString() }, par);
			return UrlInfo.PageUrl(UrlInfo.PageTypes.Admin, "ticketdetails", fullParams);
        }
        #endregion

        #region Refund
        public void Refund(Usr actionUsr)
        {
            Ticket.Refund(actionUsr, this);
        }
        public void Refund(Usr actionUsr, bool refundIncludeBookingFee, decimal chargeToPromoter)
        {
            Ticket.Refund(actionUsr, this, refundIncludeBookingFee, chargeToPromoter);
        }
        public static void Refund(Usr actionUsr, Ticket ticket)
        {
            Refund(actionUsr, ticket, false, DEFAULT_REFUND_CHARGE);
        }
        public static void Refund(Usr actionUsr, Ticket ticket, bool refundIncludeBookingFee, decimal chargeToPromoter)
        {
            if (ticket != null)
            {
                List<Ticket> ticketList = new List<Ticket>();
                ticketList.Add(ticket);

                Refund(actionUsr, ticketList, refundIncludeBookingFee, chargeToPromoter);
            }
        }
        public static void Refund(Usr actionUsr, TicketSet tickets)
        {
            Refund(actionUsr, tickets, false, DEFAULT_REFUND_CHARGE);
        }

		public static void Refund(Usr actionUsr, TicketSet tickets, bool refundIncludeBookingFee, decimal chargeToPromoter)
        {
            if (tickets != null)
            {
                List<Ticket> ticketList = new List<Ticket>();
                tickets.Reset();
                foreach (Ticket ticket in tickets)
                    ticketList.Add(ticket);

                Refund(actionUsr, ticketList, refundIncludeBookingFee, chargeToPromoter);
            }
        }
        public static void Refund(Usr actionUsr, List<Ticket> tickets)
        {
            Refund(actionUsr, tickets, false, DEFAULT_REFUND_CHARGE);
        }

		public static void Refund(Usr actionUsr, List<Ticket> tickets, bool refundIncludeBookingFee, decimal chargeToPromoter)
        {
            try
            {
                if (tickets.Count > 0 && actionUsr.IsSuperAdmin)
                {
                    chargeToPromoter = Math.Abs(chargeToPromoter);

                    List<int> ticketRunKs = new List<int>();

                    int promoterK = tickets[0].TicketRun.PromoterK;
                    int eventK = tickets[0].TicketRun.EventK;
                    foreach (Ticket ticket in tickets)
                    {
                        if (promoterK != ticket.TicketRun.PromoterK)
                            throw new Exception("Cannot automate refund for tickets belonging to more than 1 promoter.");
                        if (eventK != ticket.TicketRun.EventK)
                            throw new Exception("Cannot automate refund for tickets belonging to more than 1 event.");
                    }
                    TicketPromoterEvent ticketPromoterEvent = new TicketPromoterEvent(promoterK, eventK);

                    bool chargePromoterTicketPrice = ticketPromoterEvent.FundsReleased && ticketPromoterEvent.FundsTransfer != null && ticketPromoterEvent.FundsTransfer.Status == Transfer.StatusEnum.Success;
					bool areFundsAlreadyReleased = chargePromoterTicketPrice;
					decimal sumTicketPrice = 0;

                    string failedTicketKs = "";
                    List<Exception> failedRefundExceptions = new List<Exception>();
                    int successfulRefundTransferCount = 0;
                    int successfulRefundQuantity = 0;

                    foreach (Ticket ticket in tickets)
                    {
                        try
                        {
							RefundTicket(actionUsr, ticket, refundIncludeBookingFee, areFundsAlreadyReleased);

                            if (ticket.Cancelled)
                            {
                                if (!ticketRunKs.Contains(ticket.TicketRunK))
                                    ticketRunKs.Add(ticket.TicketRunK);

                                successfulRefundTransferCount++;
                                successfulRefundQuantity += ticket.Quantity;

                                if (chargePromoterTicketPrice)
                                    sumTicketPrice += ticket.Price;
                            }
                        }
                        catch (Exception ex)
                        {
                            failedRefundExceptions.Add(ex);
                            failedTicketKs += ticket.K.ToString() + ", ";
                        }
                    }

                    if (successfulRefundTransferCount > 0 && (chargePromoterTicketPrice || chargeToPromoter > 0))
                    {
                        // create invoice with tickets.count * chargeToPromoter invoice item for refund.
                        DateTime now = DateTime.Now;

                        InvoiceDataHolder refundChargeInvoiceDH = new InvoiceDataHolder();
                        refundChargeInvoiceDH.ActionUsrK = Usr.Current.K;
                        refundChargeInvoiceDH.CreatedDateTime = now;
                        refundChargeInvoiceDH.DueDateTime = now.AddDays(tickets[0].TicketRun.Promoter.InvoiceDueDaysEffective > Vars.InvoiceDueDaysDefault ? tickets[0].TicketRun.Promoter.InvoiceDueDaysEffective : Vars.InvoiceDueDaysDefault);
                        refundChargeInvoiceDH.DuplicateGuid = Guid.NewGuid();
                        refundChargeInvoiceDH.PromoterK = tickets[0].TicketRun.PromoterK;
                        refundChargeInvoiceDH.TaxDateTime = now;
                        refundChargeInvoiceDH.Type = Invoice.Types.Invoice;
                        if (tickets[0].TicketRun.Promoter.PrimaryUsrK != 0)
                            refundChargeInvoiceDH.UsrK = tickets[0].TicketRun.Promoter.PrimaryUsrK;
                        else
                            refundChargeInvoiceDH.UsrK = Usr.Current.K;
                        refundChargeInvoiceDH.VatCode = Invoice.VATCodes.T1;

                        if (chargePromoterTicketPrice)
                        {
                            InvoiceItemDataHolder iidhPrice = new InvoiceItemDataHolder();
                            iidhPrice.RevenueStartDate = now;
                            iidhPrice.RevenueEndDate = now;
                            iidhPrice.Description = "Ticket price refund charge for " + successfulRefundQuantity.ToString() + " ticket" + (successfulRefundQuantity > 1 ? "s" : "");
                            iidhPrice.ShortDescription = "Ticket price refund charge";
                            iidhPrice.Type = InvoiceItem.Types.Misc;
                            iidhPrice.VatCode = InvoiceItem.VATCodes.T9;
                            iidhPrice.SetTotal(Math.Round(sumTicketPrice, 2));

                            refundChargeInvoiceDH.InvoiceItemDataHolderList.Add(iidhPrice);
                        }

                        InvoiceItemDataHolder iidh = new InvoiceItemDataHolder();
                        iidh.RevenueStartDate = now;
                        iidh.RevenueEndDate = now;
                        iidh.Description = "Ticket refund charge for " + successfulRefundTransferCount.ToString() + " ticket" + (successfulRefundTransferCount > 1 ? "s" : "") + " transfers";
                        iidh.ShortDescription = "Ticket refund charge";
                        iidh.Type = InvoiceItem.Types.Misc;
                        iidh.VatCode = InvoiceItem.VATCodes.T1;
                        iidh.SetTotal(Math.Round(successfulRefundTransferCount * chargeToPromoter, 2));

                        refundChargeInvoiceDH.InvoiceItemDataHolderList.Add(iidh);

                        Invoice refundChargeInovice = refundChargeInvoiceDH.UpdateInsertDelete();

                        foreach (int ticketRunK in ticketRunKs)
                        {
                            new TicketRun(ticketRunK).CalculateSoldTicketsAndUpdate();
                        }

                        refundChargeInovice.UpdateAndAutoApplySuccessfulTransfersWithAvailableMoney();

                        Utilities.EmailInvoice(refundChargeInovice, true);

						if (areFundsAlreadyReleased)
							ticketPromoterEvent.CalculateTotalFundsAndVat();
                    }

                    failedTicketKs = failedTicketKs.Trim();

                    if (failedTicketKs.Length > 0)
                    {
                        string exceptionMessages = "";
                        foreach (Exception ex in failedRefundExceptions)
                            exceptionMessages += ex.Message + "\n\n";

                        failedTicketKs = failedTicketKs.Substring(0, failedTicketKs.Length - 1);

                        throw new Exception("Failed to refund the following tickets #" + failedTicketKs + ". Exception messages: " + exceptionMessages);
                    }
                }
            }
            catch (Exception ex)
            {
				Utilities.AdminEmailAlert("Exception in Ticket.Refund(List<Ticket> tickets)", "Exception in Ticket.Refund(List<Ticket> tickets)", ex, tickets.ConvertAll(ticket => (IBobAsHTML)ticket));
                throw ex;
            }
        }

		private bool IsAllowedToRefund(Usr actionUsr, decimal refundAmount)
        {
            if (actionUsr == null)
				throw new DsiUserFriendlyException("Ticket refunding must be performed by a logged-in site user.");

            if (!actionUsr.IsSuperAdmin)
				throw new DsiUserFriendlyException("You do not have permission to refund tickets.");

            if (this.Invoice.AmountAllowedToCredit < refundAmount)
				throw new DsiUserFriendlyException("You cannot automate a refund for a ticket for " + refundAmount.ToString("c") + " where the invoice amount allowed to credit = " + this.Invoice.AmountAllowedToCredit.ToString("c"));

            if (this.Cancelled)
				throw new DsiUserFriendlyException("This ticket has already been refunded.");

            return true;
        }

		//REMOVED BY DAVEB 10/01
		//private static void RefundTicket(Usr actionUsr, Ticket ticket)
		//{
		//    RefundTicket(actionUsr, ticket, false);
		//}

		private static void RefundTicket(Usr actionUsr, Ticket ticket, bool refundIncludeBookingFee, bool areFundsAlreadyReleased)
        {
            try
            {
                if (ticket.IsAllowedToRefund(actionUsr, Math.Round(ticket.Price + (refundIncludeBookingFee ? ticket.BookingFee : 0), 2)))
                {
                    InvoiceDataHolder creditDH = ticket.Invoice.CreateCredit();
                    creditDH.ActionUsrK = actionUsr.K;
                    // Remove all invoice items, except that of tickets
                    for (int i = creditDH.InvoiceItemDataHolderList.Count - 1; i >= 0; i--)
                    {
                        // Remove only the ticket invoice item (and booking fee if refundIncludeBookingFee)
                        if (creditDH.InvoiceItemDataHolderList[i].BuyableObjectK != ticket.K || creditDH.InvoiceItemDataHolderList[i].BuyableObjectType != Model.Entities.ObjectType.Ticket
                            || (creditDH.InvoiceItemDataHolderList[i].Type != InvoiceItem.Types.EventTickets
                                && (creditDH.InvoiceItemDataHolderList[i].Type != InvoiceItem.Types.EventTicketsBookingFee || !refundIncludeBookingFee)))
                        {
                            creditDH.InvoiceItemDataHolderList.RemoveAt(i);
                        }
                    }

                    if (ticket.Invoice.AmountAllowedToCredit < Math.Abs(creditDH.Total))
                        throw new Exception("Cannot credit more than " + ticket.Invoice.AmountAllowedToCredit.ToString("c") + " for Invoice #" + ticket.Invoice.K.ToString());

					decimal refundAmountRemaining = Math.Abs(creditDH.Total);
                    SecPay secPay = new SecPay();

                    // Refund that amount from transfers
                    foreach (InvoiceTransfer invoiceTransfer in ticket.Invoice.SuccessfulInvoiceTransfers)
                    {
                        try
                        {
                            if (refundAmountRemaining <= 0)
                                break;

							decimal refundAmount = Math.Round(invoiceTransfer.Amount, 2) < Math.Round(refundAmountRemaining, 2) ? Math.Round(invoiceTransfer.Amount, 2) : Math.Round(refundAmountRemaining, 2);

                            // refund refundAmount from that transfer, the subtract that from refundAmountRemaining
                            Transfer transferToRefund = new Transfer(invoiceTransfer.TransferK);

                            secPay.MakeRefund(transferToRefund, Guid.NewGuid(), Usr.Current.K, refundAmount);
                            if (secPay.Transfer.Status == Transfer.StatusEnum.Success)
                            {
                                refundAmountRemaining = Math.Round(refundAmountRemaining - refundAmount, 2);
                                transferToRefund.UpdateAndResolveOverapplied();
                                Utilities.EmailTransfer(secPay.Transfer, true, false);
                            }
                            else
                                throw new Exception("SecPay refund #");// + secPay.Transfer.K.ToString() + " failed, by user: " + Usr.Current.NickName + ", for ticket #" + ticket.K.ToString());
                        }
                        catch (Exception ex)
                        {
                            Utilities.AdminEmailAlert("Exception in Ticket.RefundTicket(Ticket ticket)", "Exception in Ticket.RefundTicket(Ticket ticket)", ex, ticket);
                        }
                    }

                    if (Math.Round(refundAmountRemaining, 2) <= 0)
                    {
                        creditDH.DueDateTime = Time.Now;
                        Invoice credit = creditDH.UpdateInsertDelete();
                        // Get latest invoice from DB
                        ticket.Invoice = null;
                        ticket.Invoice.ApplyCreditToThisInvoice(credit);

                        if (!ticket.Invoice.Paid)
                            throw new Exception("Invoice #" + ticket.Invoice.K.ToString() + " was not refunded completely, please verify manually.");

                        ticket.Cancelled = true;
						ticket.CancelledBeforeFundsRelease = !areFundsAlreadyReleased;
						ticket.CancelledDateTime = Time.Now;
                        ticket.Update();
						if (ticket.BuyerUsr != null)
						{
							ticket.BuyerUsr.SetPrefsNextTicketFeedbackDate();
						}
                        Utilities.EmailTicket(ticket);
                    }
                }
            }
            catch (Exception ex)
            {
                Utilities.AdminEmailAlert("Exception in Ticket.RefundTicket(Ticket ticket)", "Exception in Ticket.RefundTicket(Ticket ticket)", ex, ticket);
                throw ex;
            }
        }
        #endregion

        #region GenerateReport
		public StringBuilder GenerateReportStringBuilder(bool linksEnabled)
		{
			StringBuilder sb = new StringBuilder();

			sb.Append(@"<form id='form1' runat='server'><div style='font-family:Verdana;'><table width='100%' border='0' cellspacing='0' cellpadding='0' height='100%'><tr><td valign='top'>
						<table width='100%'>");

			sb.Append(Utilities.GenerateHTMLHeaderRowString("TICKET&nbsp;RECEIPT", false));
			sb.Append(@"<tr>
								<td colspan=1 align='left' valign='top' width='450'>");
			if (this.BuyerUsr != null && this.BuyerUsr.FullName.Length > 0)
			{
				sb.Append(this.BuyerUsr.FullName);
				sb.Append("<br>");
				sb.Append(this.BuyerUsr.AddressHtml());
			}

			// Addition of Created and renaming of "Date" to "Tax Date", as per Dave's request 7/2/07
			sb.Append(@"</td><td width='350'></td><td valign='top' width='100'>Ticket&nbsp;No.<br><br>Purchase&nbsp;Date</td><td align='right' valign='top' width='125'>");
			sb.Append(this.K.ToString());
			sb.Append("<br><br><nobr>" + this.BuyDateTime.ToString("dd/MM/yy"));
			sb.Append(@"</nobr></td></tr>
						</table><br><br>");

			if (this.Cancelled)
			{
				sb.Append("<p><font color='Red' size='+2'><b>TICKET&nbsp;CANCELLED</b></font></p>");
			}

			sb.Append(@"<table cellspacing='0' cellpadding='3'>
							<tr>
								<td style='vertical-align:top;'>Event:</td>
								<td style='vertical-align:top;'>" + this.Event.FriendlyName + @"</td>
							</tr>
							<tr>
								<td style='vertical-align:top;'>Tickets:</td>
								<td style='vertical-align:top;'>" + this.Quantity.ToString() + " x " + Utilities.MoneyToHTML(this.TicketRun.Price) + " + booking fee (" + Utilities.MoneyToHTML(this.BookingFee) + @")</td>
							</tr>");
			if (this.TicketRun.Name.Length > 0)
			{
				sb.Append(@"<tr>
								<td style='vertical-align:top;'>Ticket&nbsp;type:</td>
								<td style='vertical-align:top;'>" + this.TicketRun.Name + @"</td>
							</tr>");
			}

			if (this.TicketRun.Description.Length > 0)
			{
				sb.Append(@"<tr>
								<td style='vertical-align:top;'>Description:</td>
								<td style='vertical-align:top;'>" + this.TicketRun.Description + @"</td>
							</tr>");
			}
			sb.Append(@"	<tr>
								<td style='vertical-align:top;'>Card used:</td>
								<td style='vertical-align:top;'>" + Utilities.MaskedCardNumber(this.CardNumberDigits, this.CardNumberEnd) + @"</td>
							</tr>
						</table><br><br><p>");
			if (!this.Cancelled)
			{
				sb.Append("<b>");
				sb.Append(this.Quantity > 0 ? Ticket.ETICKET_CARD_REMINDER_PLURAL : Ticket.ETICKET_CARD_REMINDER_SINGULAR);
				sb.Append("</b><br>");
				if (this.Code.Length > 0)
				{
					sb.Append("<b>");
					sb.Append(Ticket.ETICKET_CODE_REMINDER_SINGULAR);
					sb.Append("</b><br>");
				}
				sb.Append(Ticket.ETICKET_CARD_REMINDER_NOT_LET_YOU_IN);
				sb.Append("<br>This receipt alone will not be sufficient.");
			}
			sb.Append("</p>");
			sb.Append(Utilities.GenerateHTMLFooterRowString(false));

			sb.Append("</table></div></form>");

			return sb;
		}
        #endregion

		public bool ConfirmCv2(string cv2)
		{
			if (this.HasExceededCardCheckAttempts)
			{
				throw new Exception("Ticket has exceeded card check attempts!");
			}
			this.CardCheckAttempts++;
			if (this.CardCV2 == cv2)
			{
				this.CardCheckedByPromoter = true;
			}
			this.Update();
			return this.CardCheckedByPromoter;
		}
		public string[] AddressParts
		{
			get
			{
				List<string> parts = new List<string>();
				parts.Add(AddressName);
				parts.Add(AddressStreet);
				if (AddressArea.Length > 0) parts.Add(AddressArea);
				parts.Add(AddressTown);
				if(AddressCounty.Length > 0) parts.Add(AddressCounty);
				if (AddressCountryK > 0) parts.Add(new Country(AddressCountryK).Name);
				parts.Add(AddressPostcode);
				return parts.ToArray();
			}
		}
		public string SingleLineAddress
		{
			get
			{
				List<string> parts = new List<string>();

				return String.Join(", ", AddressParts);
			}
		}
		public bool HasExceededCardCheckAttempts
		{
			get { return this.CardCheckAttempts >= 3; }
		}

		#region Country
		/// <summary>
		/// This is the Country linked to by CountryK
		/// </summary>
		public Country AddressCountry
		{
			get
			{
				if (addressCountry == null && AddressCountryK > 0)
				{
					try
					{
						addressCountry = new Country(AddressCountryK);
					}
					catch { }
				}
				return addressCountry;
			}
		}
		private Country addressCountry;
		#endregion

	}
    #endregion

	#region TicketForPrinting
	public class TicketForPrinting : Ticket, IBobReport
	{ 
		public TicketForPrinting(int k) : base(k)
		{
		}

		public string BuyerName
		{
			get
			{
				string name = "";
				if (!string.IsNullOrEmpty(FirstName))
				{
					name = FirstName;
					if (!string.IsNullOrEmpty(this.LastName))
						name += " ";
				}
				if (!string.IsNullOrEmpty(this.LastName))
					name += LastName;

				return name;
			}
		}

		StringBuilder IBobReport.GenerateReportStringBuilder(bool linksEnabled)
		{
			StringBuilder sb = new StringBuilder();

			sb.Append(@"<form id='form1' runat='server'><div style='font-family:Verdana;'><table width='100%' border='0' cellspacing='0' cellpadding='0' height='100%' style='padding-left:100px; padding-right:100px; padding-top:50px'><tr><td valign='top'>
						<table width='100%'>");

			sb.Append(Utilities.GenerateHTMLHeaderRowString("TICKET&nbsp;RECEIPT", false));
			sb.Append(@"<tr>
								<td colspan=1 align='left' valign='top' width='450'>");
			string buyerName = this.BuyerName;
			if (!string.IsNullOrEmpty(buyerName))
			{
				sb.Append(buyerName);
				sb.Append("<br>");
				sb.Append(this.AddressHtml());
			}

			// Addition of Created and renaming of "Date" to "Tax Date", as per Dave's request 7/2/07
			sb.Append(@"</td><td width='350'></td><td valign='top' width='100'>Ticket&nbsp;No.<br><br>Purchase&nbsp;Date</td><td align='right' valign='top' width='125'>");
			sb.Append(this.K.ToString());
			sb.Append("<br><br><nobr>" + this.BuyDateTime.ToString("dd/MM/yy"));
			sb.Append(@"</nobr></td></tr>
						</table><br><br>");

			if (this.Cancelled)
			{
				sb.Append("<p><font color='Red' size='+2'><b>TICKET&nbsp;CANCELLED</b></font></p>");
			}

			sb.Append(@"<table cellspacing='0' cellpadding='3'>
							<tr>
								<td colspan='2'>&nbsp;</td>
							</tr>
							<tr>
								<td style='vertical-align:top;'>Event:</td>
								<td style='vertical-align:top;'>" + this.Event.FriendlyName + @"</td>
							</tr>
							<tr>
								<td style='vertical-align:top;'>Tickets:</td>
								<td style='vertical-align:top;'>" + this.Quantity.ToString() + " x " + Utilities.MoneyToHTML(this.TicketRun.Price) + " + booking fee (" + Utilities.MoneyToHTML(this.BookingFee) + @")</td>
							</tr>");
			if (this.TicketRun.Name.Length > 0)
			{
				sb.Append(@"<tr>
								<td style='vertical-align:top;'>Ticket&nbsp;type:</td>
								<td style='vertical-align:top;'>" + this.TicketRun.Name + @"</td>
							</tr>");
			}

			if (this.TicketRun.Description.Length > 0)
			{
				sb.Append(@"<tr>
								<td style='vertical-align:top;'>Description:</td>
								<td style='vertical-align:top;'>" + this.TicketRun.Description + @"</td>
							</tr>");
			}
			sb.Append(@"	<tr>
								<td style='vertical-align:top;'>Card used:</td>
								<td style='vertical-align:top;'>" + Utilities.MaskedCardNumber(this.CardNumberDigits, this.CardNumberEnd) + @"</td>
							</tr>
						</table><br><br><p>");
			if (!this.Cancelled)
			{
				sb.Append("Please find your ticket(s) enclosed. Keep them safe as we cannot issue replacements.");
			}
			sb.Append("</p>");
			sb.Append("<p>Thanks for buying your ticket from DontStayIn.com! We hope you enjoy the event.</p>");
			sb.Append("<p>Lots of love,</p>");
			sb.Append("<p>The DSI team</p>");
			sb.Append(Utilities.GenerateHTMLFooterRowString(false));

			sb.Append("</table></div></form>");

			return sb;
		}
	}
	#endregion

}
