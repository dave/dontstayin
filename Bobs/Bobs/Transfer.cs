using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs.DataHolders;
using Bobs;
using Common;

namespace Bobs
{
    #region Transfer
    /// <summary>
    /// Customer pays us using a credit card / card is refunded / customer transfers money into our bank acc
    /// </summary>
    [Serializable]
	public partial class Transfer : IBobReport, ILinkable, IBobAsHTML, ILinkableAdmin, IReadableReference
    {
        #region Simple members
        /// <summary>
        /// Primary key
        /// </summary>
        public override int K
        {
            get { return this[Transfer.Columns.K] as int? ?? 0; }
            set { this[Transfer.Columns.K] = value; }
        }
        /// <summary>
        /// Payment, Refund
        /// </summary>
        public override TransferTypes Type
        {
            get { return (TransferTypes)this[Transfer.Columns.Type]; }
            set { this[Transfer.Columns.Type] = value; }
        }
        /// <summary>
        /// Pending, Success, Cancelled
        /// </summary>
        public override StatusEnum Status
        {
            get { return (StatusEnum)this[Transfer.Columns.Status]; }
            set { this[Transfer.Columns.Status] = value; }
        }
        /// <summary>
        /// Card, Bank Transfer, Cheque, Cash
        /// </summary>
        public override Methods Method
        {
            get { return (Methods)this[Transfer.Columns.Method]; }
            set { this[Transfer.Columns.Method] = value; }
        }
        /// <summary>
        /// Link to the relevant user
        /// </summary>
        public override int UsrK
        {
            get { return (int)this[Transfer.Columns.UsrK]; }
            set { this[Transfer.Columns.UsrK] = value; }
        }
        /// <summary>
        /// Link to the promoter (if this is a promoter transfer)
        /// </summary>
        public override int PromoterK
        {
            get { return (int)this[Transfer.Columns.PromoterK]; }
            set { this[Transfer.Columns.PromoterK] = value; }
        }
        /// <summary>
        /// Link to the user that initiated this transfer (e.g. the admin user if it's a refund!)
        /// </summary>
        public override int ActionUsrK
        {
            get { return (int)this[Transfer.Columns.ActionUsrK]; }
            set { this[Transfer.Columns.ActionUsrK] = value; }
        }
        /// <summary>
        /// +ve for DSI receiving money, -ve for DSI paying out money
        /// </summary>
		public override decimal Amount
        {
			get { return (decimal)this[Transfer.Columns.Amount]; }
            set { this[Transfer.Columns.Amount] = value; }
        }
        /// <summary>
        /// Date / time the transfer was initiated / received
        /// </summary>
        public override DateTime DateTime
        {
            get { return (DateTime)this[Transfer.Columns.DateTime]; }
            set { this[Transfer.Columns.DateTime] = value; }
        }
        /// <summary>
        /// Date / time the transfer was created
        /// </summary>
        public override DateTime DateTimeCreated
        {
            get { return (DateTime)this[Transfer.Columns.DateTimeCreated]; }
            set { this[Transfer.Columns.DateTimeCreated] = value; }
        }
        /// <summary>
        /// Date / time the transfer was completed
        /// </summary>
        public override DateTime DateTimeComplete
        {
            get { return (DateTime)this[Transfer.Columns.DateTimeComplete]; }
            set { this[Transfer.Columns.DateTimeComplete] = value; }
        }
        /// <summary>
        /// IP address of the client machine
        /// </summary>
        public override string ClientHost
        {
            get { return (string)this[Transfer.Columns.ClientHost]; }
            set { this[Transfer.Columns.ClientHost] = value; }
        }
        /// <summary>
        /// for card payment - the billing name
        /// </summary>
        public override string CardName
        {
            get { return (string)this[Transfer.Columns.CardName]; }
            set { this[Transfer.Columns.CardName] = value; }
        }
        /// <summary>
        /// for card payment - the billing address (line 1)
        /// </summary>
        public override string CardAddress1
        {
            get { return (string)this[Transfer.Columns.CardAddress1]; }
            set { this[Transfer.Columns.CardAddress1] = value; }
        }
        /// <summary>
        /// for card payment - the billing postcode
        /// </summary>
        public override string CardPostcode
        {
            get { return (string)this[Transfer.Columns.CardPostcode]; }
            set { this[Transfer.Columns.CardPostcode] = value; }
        }
        /// <summary>
        /// This transfer used card details from an earlier transfer (saved card details or refund)
        /// </summary>
        public override int CardSavedTransferK
        {
            get { return (int)this[Transfer.Columns.CardSavedTransferK]; }
            set { this[Transfer.Columns.CardSavedTransferK] = value; }
        }
        /// <summary>
        /// Cryptographic hash of the card number
        /// </summary>
        public override Guid CardNumberHash
        {
            get { return Cambro.Misc.Db.GuidConvertor(this[Transfer.Columns.CardNumberHash]); }
            set { this[Transfer.Columns.CardNumberHash] = new System.Data.SqlTypes.SqlGuid(value); }
        }
        /// <summary>
        /// Last 6 digits of the card number
        /// </summary>
        public override string CardNumberEnd
        {
            get { return (string)this[Transfer.Columns.CardNumberEnd]; }
            set { this[Transfer.Columns.CardNumberEnd] = value; }
        }
        /// <summary>
        /// Card issuer deduced from card number (e.g. Visa, Mastercard etc.)
        /// </summary>
		public override BinRange.Types CardType
        {
			get { return (BinRange.Types)this[Transfer.Columns.CardType]; }
            set { this[Transfer.Columns.CardType] = value; }
        }
        /// <summary>
        /// Card start date
        /// </summary>
        public override DateTime CardStart
        {
            get { return (DateTime)this[Transfer.Columns.CardStart]; }
            set { this[Transfer.Columns.CardStart] = value; }
        }
        /// <summary>
        /// Card expiry date
        /// </summary>
        public override DateTime CardExpires
        {
            get { return (DateTime)this[Transfer.Columns.CardExpires]; }
            set { this[Transfer.Columns.CardExpires] = value; }
        }
        /// <summary>
        /// Issue number
        /// </summary>
        public override int CardIssue
        {
            get { return (int)this[Transfer.Columns.CardIssue]; }
            set { this[Transfer.Columns.CardIssue] = value; }
        }
        /// <summary>
        /// Card CV2 number
        /// </summary>
        public override string CardCV2
        {
            get { return (string)this[Transfer.Columns.CardCV2]; }
            set { this[Transfer.Columns.CardCV2] = value; }
        }
        /// <summary>
        /// Is the card saved for further use?
        /// </summary>
        public override bool CardSaved
        {
            get { return (bool)this[Transfer.Columns.CardSaved]; }
            set { this[Transfer.Columns.CardSaved] = value; }
        }
        /// <summary>
        /// The account name - e.g. Uprising Clubs Limited
        /// </summary>
        public override string BankAccountName
        {
            get { return (string)this[Transfer.Columns.BankAccountName]; }
            set { this[Transfer.Columns.BankAccountName] = value.Trim(); }
        }
        /// <summary>
        /// The bank name - e.g. Lloyds
        /// </summary>
        public override string BankName
        {
            get { return (string)this[Transfer.Columns.BankName]; }
            set { this[Transfer.Columns.BankName] = value.Trim(); }
        }
        /// <summary>
        /// Sort code
        /// </summary>
        public override string BankSortCode
        {
            get { return (string)this[Transfer.Columns.BankSortCode]; }
            set { this[Transfer.Columns.BankSortCode] = value.Replace(" ", "").Replace("-", ""); }
        }
        /// <summary>
        /// Account number
        /// </summary>
        public override string BankAccountNumber
        {
            get { return (string)this[Transfer.Columns.BankAccountNumber]; }
            set { this[Transfer.Columns.BankAccountNumber] = value.Replace(" ", "").Replace("-", ""); }
        }
        /// <summary>
        /// The reference/comment added to the transfer
        /// </summary>
        public override string BankTransferReference
        {
            get { return (string)this[Transfer.Columns.BankTransferReference]; }
            set { this[Transfer.Columns.BankTransferReference] = value; }
        }
        /// <summary>
        /// Only when Status=Success.The bank's authorisation code for your information only, do not show to cus
        /// </summary>
        public override string CardResponseAuthCode
        {
            get { return (string)this[Transfer.Columns.CardResponseAuthCode]; }
            set { this[Transfer.Columns.CardResponseAuthCode] = value; }
        }
        /// <summary>
        /// The Apacs approved text that is supplied as a result of the CV2 and AVS anti-Fraud checks. There are
        /// </summary>
        public override string CardResponseCv2Avs
        {
            get { return (string)this[Transfer.Columns.CardResponseCv2Avs]; }
            set { this[Transfer.Columns.CardResponseCv2Avs] = value; }
        }
        /// <summary>
        /// Only when Status=Failed. The bank's failure message for your information only, do not show to custom
        /// </summary>
        public override string CardResponseMessage
        {
            get { return (string)this[Transfer.Columns.CardResponseMessage]; }
            set { this[Transfer.Columns.CardResponseMessage] = value; }
        }
        /// <summary>
        /// Only when Status=Failed and CardResponseCode='N'. The bank's failure code for your information only,
        /// </summary>
        public override string CardResponseRespCode
        {
            get { return (string)this[Transfer.Columns.CardResponseRespCode]; }
            set { this[Transfer.Columns.CardResponseRespCode] = value; }
        }
        /// <summary>
        /// The code field is a short code giving extensive details of failure states. It is of particular use t
        /// </summary>
        public override string CardResponseCode
        {
            get { return (string)this[Transfer.Columns.CardResponseCode]; }
            set { this[Transfer.Columns.CardResponseCode] = value; }
        }
		/// <summary>
		/// Flag to mark results from CV2 Fraud check
		/// </summary>
		public override bool CardResponseIsCv2Match
		{
			get { return (bool)this[Transfer.Columns.CardResponseIsCv2Match]; }
			set { this[Transfer.Columns.CardResponseIsCv2Match] = value; }
		}
		/// <summary>
		/// Flag to mark results from Post Code Fraud check
		/// </summary>
		public override bool CardResponseIsPostCodeMatch
		{
			get { return (bool)this[Transfer.Columns.CardResponseIsPostCodeMatch]; }
			set { this[Transfer.Columns.CardResponseIsPostCodeMatch] = value; }
		}
		/// <summary>
		/// Flag to mark results from Address Fraud check
		/// </summary>
		public override bool CardResponseIsAddressMatch
		{
			get { return (bool)this[Transfer.Columns.CardResponseIsAddressMatch]; }
			set { this[Transfer.Columns.CardResponseIsAddressMatch] = value; }
		}
		/// <summary>
		/// Flag to mark if fraud check was enforced
		/// </summary>
		public override bool CardResponseIsDataChecked
		{
			get { return (bool)this[Transfer.Columns.CardResponseIsDataChecked]; }
			set { this[Transfer.Columns.CardResponseIsDataChecked] = value; }
		}
		/// <summary>
		/// Additional Notes
		/// </summary>
		public override string Notes
		{
			get { return (string)this[Transfer.Columns.Notes]; }
			set { this[Transfer.Columns.Notes] = value; }
		}
		/// <summary>
		/// This flag is to be set when the sum of InvoiceTransfers amounts = Transfer.Amount.  It will facilita
		/// </summary>
		public override bool IsFullyApplied
		{
			get { return (bool)this[Transfer.Columns.IsFullyApplied]; }
			set { this[Transfer.Columns.IsFullyApplied] = value; }
		}
		/// <summary>
		/// The guid of the transfer.  Allows unique identifier to be assigned prior to saving to the db
		/// </summary>
		public override Guid Guid
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[Transfer.Columns.Guid]); }
			set { this[Transfer.Columns.Guid] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// Transfer K of transfer that this has refunded
		/// </summary>
		public override int TransferRefundedK
		{
			get { return (int)this[Transfer.Columns.TransferRefundedK]; }
			set { this[Transfer.Columns.TransferRefundedK] = value; }
		}
		/// <summary>
		/// Not Refunded, Partial Refund, Full Refund
		/// </summary>
		public override RefundStatuses RefundStatus
		{
			get { return (RefundStatuses)this[Transfer.Columns.RefundStatus]; }
			set { this[Transfer.Columns.RefundStatus] = value; }
		}
		/// <summary>
		/// Guid to catch duplicate on save
		/// </summary>
		public override Guid DuplicateGuid
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[Transfer.Columns.DuplicateGuid]); }
			set { this[Transfer.Columns.DuplicateGuid] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// The cheque reference number
		/// </summary>
		public override string ChequeReferenceNumber
		{
			get { return (string)this[Transfer.Columns.ChequeReferenceNumber]; }
			set { this[Transfer.Columns.ChequeReferenceNumber] = value; }
		}
		/// <summary>
		/// Number of digits in the card number
		/// </summary>
		public override int CardDigits
		{
			get { return (int)this[Transfer.Columns.CardDigits]; }
			set { this[Transfer.Columns.CardDigits] = value; }
		}
        /// <summary>
        /// Which DSI bank account was used in this transfer. DSI Current account = 1, DSI Client account = 2
        /// </summary>
        public override DSIBankAccounts DSIBankAccount
        {
            get { return (DSIBankAccounts)this[Transfer.Columns.DSIBankAccount]; }
            set { this[Transfer.Columns.DSIBankAccount] = value; }
        }
		/// <summary>
		/// Part of address card is registered to
		/// </summary>
		public override string CardAddressArea
		{
			get { return (string)this[Transfer.Columns.CardAddressArea]; }
			set { this[Transfer.Columns.CardAddressArea] = value; }
		}
		/// <summary>
		/// Part of address card is registered to
		/// </summary>
		public override string CardAddressTown
		{
			get { return (string)this[Transfer.Columns.CardAddressTown]; }
			set { this[Transfer.Columns.CardAddressTown] = value; }
		}
		/// <summary>
		/// Part of address card is registered to
		/// </summary>
		public override string CardAddressCounty
		{
			get { return (string)this[Transfer.Columns.CardAddressCounty]; }
			set { this[Transfer.Columns.CardAddressCounty] = value; }
		}
		/// <summary>
		/// Part of address card is registered to
		/// </summary>
		public override int CardAddressCountryK
		{
			get { return (int)this[Transfer.Columns.CardAddressCountryK]; }
			set { this[Transfer.Columns.CardAddressCountryK] = value; }
		}
		/// <summary>
		/// Which company did the transfer go to / come from?
		/// </summary>
		public override CompanyEnum Company
		{
			get { return (CompanyEnum)this[Transfer.Columns.Company]; }
			set { this[Transfer.Columns.Company] = value; }
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
            sb.Append("<u>Transfer details</u>");
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
            if (DateTimeComplete >= DateTimeCreated)
            {
                sb.Append("Date completed: ");
				sb.Append(this.DateTimeComplete.ToString("ddd dd/MM/yyyy HH:mm:ss"));
            }
            else
            {
                sb.Append("Date created: ");
				sb.Append(this.DateTimeCreated.ToString("ddd dd/MM/yyyy HH:mm:ss"));
            }
            sb.Append(lineReturn);
            sb.Append("Amount: ");
            sb.Append(Utilities.MoneyToHTML(this.Amount));
            sb.Append(lineReturn);
            sb.Append("Status: ");
            sb.Append(this.Status.ToString());
            sb.Append(lineReturn);
			sb.Append("Company: ");
			sb.Append(this.Company.ToString());
			sb.Append(lineReturn);
            sb.Append("Method: ");
            sb.Append(this.Method.ToString());
            sb.Append(lineReturn);
            if (this.Method == Methods.BankTransfer)
            {
                if (this.BankName != "")
                {
                    sb.Append("Bank name: ");
                    sb.Append(this.BankName);
					sb.Append(lineReturn);
                }
                if (this.BankAccountName != "")
                {
                    sb.Append("Bank account name: ");
                    sb.Append(this.BankAccountName);
					sb.Append(lineReturn);
                }
                if (this.BankSortCode != "")
                {
                    sb.Append("Bank sort code: ");
                    sb.Append(this.BankSortCode);
					sb.Append(lineReturn);
                }
                if (this.BankAccountNumber != "")
                {
                    sb.Append("Bank account #: ");
                    sb.Append(this.BankAccountNumber);
					sb.Append(lineReturn);
                }
            }
            else if (this.Method == Methods.Card)
            {
				sb.Append("Card #: ");
				sb.Append(this.CardNumberEnd);
				sb.Append(lineReturn);

				sb.Append("Card hash: ");
				sb.Append(this.CardNumberHash);
				sb.Append(lineReturn);

                sb.Append("Card CV2: ");
                sb.Append(this.CardCV2);
				sb.Append(lineReturn);
               
                sb.Append("Card type: ");
                sb.Append(this.CardType.ToString());
				sb.Append(lineReturn);

				sb.Append("Address:");
				sb.Append(lineReturn);
				sb.Append(this.CardAddress1);
				sb.Append(lineReturn);
				sb.Append(this.CardPostcode);
				sb.Append(lineReturn);
            }
            else if (this.Method == Methods.Cheque)
            {
                sb.Append("Cheque #: ");
                sb.Append(this.ChequeReferenceNumber);
				sb.Append(lineReturn);
            }
            else if (this.Method == Methods.TicketSales && this.TicketPromoterEvent != null && this.TicketPromoterEvent.Event != null)
            {
                sb.Append("Event: ");
                sb.Append(this.TicketPromoterEvent.Event.FriendlyName);
				sb.Append(" (K: ");
				sb.Append(this.TicketPromoterEvent.EventK);
				sb.Append(")");
				sb.Append(lineReturn);
            }

            return sb.ToString();
		}
		#endregion

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
		#region RefundTransfers
		public TransferSet RefundTransfers
		{
			get
			{
				if (refundTransfers == null)
				{
					Query refundTransferQuery = new Query(new And(new Q(Transfer.Columns.TransferRefundedK, this.K),
														  new Or(new Q(Transfer.Columns.Status, Transfer.StatusEnum.Pending),
																 new Q(Transfer.Columns.Status, Transfer.StatusEnum.Success))));
					refundTransfers = new TransferSet(refundTransferQuery);
				}
				return refundTransfers;
			}
		}
		private TransferSet refundTransfers;
		#endregion
        #region TransferRefunded
        public Transfer TransferRefunded
        {
            get
            {
                if (transferRefunded == null && TransferRefundedK > 0)
                {
                    transferRefunded = new Transfer(TransferRefundedK);
                }
                return transferRefunded;
            }
        }
        private Transfer transferRefunded;
        #endregion
        #region TicketPromoterEvent
        public TicketPromoterEvent TicketPromoterEvent
        {
            get
            {
                if (this.Method == Methods.TicketSales && ticketPromoterEvent == null)
                {
                    TicketPromoterEventSet tpeSet = new TicketPromoterEventSet(new Query(new Q(TicketPromoterEvent.Columns.FundsTransferK, this.K)));
                    if (tpeSet.Count > 0)
                    {
                        ticketPromoterEvent = tpeSet[0];
                    }
                }

                return ticketPromoterEvent;
            }
        }
        private TicketPromoterEvent ticketPromoterEvent;
        #endregion
        #endregion

        #region Enums
        #region Statuses
        #endregion

        #region TransferTypes
        #endregion

        #region Methods
        #endregion

		#region RefundStatuses
		#endregion

		#region FraudCheckEnum
		#endregion

        #region DSIBankAccount
        #endregion
        #endregion

        #region Enums To ListItem[]
        public static ListItem[] TypesAsListItemArray()
		{
            //ListItem[] ListItems = new ListItem[2];
            //ListItems[0] = new ListItem(TransferTypes.Payment.ToString(), Convert.ToInt32(TransferTypes.Payment).ToString());
            //ListItems[1] = new ListItem(TransferTypes.Refund.ToString(), Convert.ToInt32(TransferTypes.Refund).ToString());

            //// default Type is Payment
            //ListItems[0].Selected = true;

            //return ListItems;

            return Utilities.EnumToListItemArray(typeof(TransferTypes));
		}

		public static ListItem[] MethodsAsListItemArray()
		{
            //ListItem[] ListItems = new ListItem[5];
            //ListItems[0] = new ListItem(Utilities.CamelCaseToString(Methods.BankTransfer.ToString()), Convert.ToInt32(Methods.BankTransfer).ToString());
            //ListItems[1] = new ListItem(Methods.Card.ToString(), Convert.ToInt32(Methods.Card).ToString());
            //ListItems[2] = new ListItem(Methods.Cash.ToString(), Convert.ToInt32(Methods.Cash).ToString());
            //ListItems[3] = new ListItem(Methods.Cheque.ToString(), Convert.ToInt32(Methods.Cheque).ToString());
            //ListItems[4] = new ListItem(Utilities.CamelCaseToString(Methods.TicketSales.ToString()), Convert.ToInt32(Methods.TicketSales).ToString());

            //// default Method is Card
            //ListItems[1].Selected = true;

            //return ListItems;

            return Utilities.EnumToListItemArray(typeof(Methods));
		}

		public static ListItem[] StatusesAsListItemArray()
		{
            //ListItem[] ListItems = new ListItem[4];
            //ListItems[0] = new ListItem(StatusEnum.Pending.ToString(), Convert.ToInt32(StatusEnum.Pending).ToString());
            //ListItems[1] = new ListItem(StatusEnum.Success.ToString(), Convert.ToInt32(StatusEnum.Success).ToString());
            //ListItems[2] = new ListItem(StatusEnum.Cancelled.ToString(), Convert.ToInt32(StatusEnum.Cancelled).ToString());
            //ListItems[3] = new ListItem(StatusEnum.Failed.ToString(), Convert.ToInt32(StatusEnum.Failed).ToString());

            //return ListItems;

            return Utilities.EnumToListItemArray(typeof(StatusEnum));
		}

        //public static ListItem[] DSIBankAccountsAsListItemArray()
        //{
        //    List<ListItem> ListItems = new List<ListItem>();
        //    ListItems.Add(new ListItem(DSIBankAccounts.Client.ToString(), Convert.ToInt32(DSIBankAccounts.Client).ToString()));
        //    ListItems.Add(new ListItem(DSIBankAccounts.Current.ToString(), Convert.ToInt32(DSIBankAccounts.Current).ToString()));

        //    return ListItems.ToArray();
        //}

		#endregion

		#region CardTypes
		public void SetCardType(string cardNumber)
		{
			#region Get cardType from BinRange database
			int bin = 0;
			if(cardNumber.Length >= 6)
				bin = Convert.ToInt32(cardNumber.Substring(0, 6));
			Query q = new Query();
			q.QueryCondition = new And(new Q(BinRange.Columns.Low, QueryOperator.LessThanOrEqualTo, bin), new Q(BinRange.Columns.High, QueryOperator.GreaterThanOrEqualTo, bin));
			q.NoLock = true;
			q.OrderBy = new OrderBy(BinRange.Columns.Order, OrderBy.OrderDirection.Descending);
			BinRangeSet brs = new BinRangeSet(q);
			if (brs.Count == 0)
			{
				Mailer sm = new Mailer();
				sm.Body = "<p>Can't find BIN in BinRange! - Transfer=" + this.K.ToString() + ", PromoterK=" + this.PromoterK.ToString() + ", CardEnd=" + this.CardNumberEnd + "</p>";
				sm.TemplateType = Mailer.TemplateTypes.AdminNote;
				sm.Subject = "Can't find BIN in BinRange!";
				sm.To = "d.brophy@dontstayin.com, neil@dontstayin.com";
				sm.Send();

				throw new DsiUserFriendlyException("Sorry, we can't identify your card. Please ring us on 0207 835 5599 and we will process your payment manually.");
			}
			else
			{
				this.CardType = brs[0].Type;

				if (brs[0].Type.Equals(BinRange.Types.JCB))
					throw new DsiUserFriendlyException("Sorry, we don't accept JCB cards.");
			}
			#endregion
		}
        #endregion

		#region Properties
		public string CardNumber
		{
			get
			{
				if (cardNumber.Length == 0 && this.CardNumberEnd.Length > 0)
				{	
					cardNumber = Utilities.MaskedCardNumber(this.CardDigits, this.CardNumberEnd);
				}
				return cardNumber;
			}
		}
		private string cardNumber = "";

		public string TypeAndK
		{
			get
			{
				return this.TypeToString + " #" + this.K.ToString();
			}
		}

        public string TypeToString
        {
            get
            {
                if (this.Method == Methods.TicketSales)
                    return Utilities.CamelCaseToString(Methods.TicketSales.ToString());
                else if(this.TransferRefunded != null && this.TransferRefunded.Method == Methods.TicketSales)
                    return Utilities.CamelCaseToString(Methods.TicketSales.ToString()) + " Payment";
                else
                    return Utilities.CamelCaseToString(this.Type.ToString());
            }
        }
		public string ReadableReference
		{
			get
			{
				return TypeAndK;
			}
		}

		#endregion

		#region StoreCardEndAndHash
		public void StoreCardEndAndHashAndCardType(string cardNumber)
		{
			cardNumber = cardNumber.Trim().Replace("*", "").Replace(" ", "");

			this.CardNumberEnd = cardNumber.Substring(cardNumber.Length - Utilities.DISPLAY_CARD_LAST_NUMBER_OF_DIGITS);

			if (!cardNumber.Equals(this.CardNumberEnd) && cardNumber.Length > 0)
			{
				this.CardDigits = cardNumber.Length;
				this.CardNumberHash = Cambro.Misc.Utility.Hash(cardNumber);
				SetCardType(cardNumber);
			}
		}
		#endregion

		#region IsUsrAllowedAccess
		public bool IsUsrAllowedAccess(Usr usr)
		{
			return usr.IsAdmin || ((this.PromoterK > 0 && usr.IsPromoter && usr.IsPromoterK(this.PromoterK)) || (this.PromoterK == 0 && this.UsrK > 0 && usr.K == this.UsrK));
		}
		#endregion

		#region DoesDuplicateGuidExistInDb
		public static bool DoesDuplicateGuidExistInDb(Guid checkGuid)
		{
			//select duplicate transfers
			Query duplicateTransferQuery = new Query(new Q(Transfer.Columns.DuplicateGuid, checkGuid));
			duplicateTransferQuery.ReturnCountOnly = true;

			return new TransferSet(duplicateTransferQuery).Count > 0;
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

        //#region SetDSIBankAccount
        //public void SetDSIBankAccount()
        //{
        //    if (this.Method == Methods.TicketFunds || this.PromoterK == 0 || (this.TransferRefunded != null && this.TransferRefunded.Method == Methods.TicketFunds))
        //        this.DSIBankAccount = DSIBankAccounts.Client;
        //    else
        //        this.DSIBankAccount = DSIBankAccounts.Current;
        //}
        //#endregion

        #region Amounts Remaining / Applied / Refunded
        public decimal AmountRemaining()
		{
			return Math.Round(this.Amount - AmountApplied() - AmountRefunded(),2);
		}

		public decimal AmountApplied()
		{
			decimal amountApplied = 0;
			Query invoiceTransferQuery = new Query(new Q(InvoiceTransfer.Columns.TransferK, this.K));
			invoiceTransferQuery.Columns = new ColumnSet(InvoiceTransfer.Columns.Amount);
			InvoiceTransferSet invoiceTransferSet = new InvoiceTransferSet(invoiceTransferQuery);
			foreach (InvoiceTransfer invoiceTransfer in invoiceTransferSet)
				amountApplied += invoiceTransfer.Amount;

			return Math.Round(amountApplied, 2);
		}

		public decimal AmountRefunded()
		{
			decimal amountRefunded = 0;
			TransferSet refundTransferSet;

			if (refundTransfers != null)
			{
				refundTransferSet = refundTransfers;
			}
			else
			{
				Query refundTransferQuery = new Query(new And(new Q(Transfer.Columns.TransferRefundedK, this.K),
															  new Or(new Q(Transfer.Columns.Status, Transfer.StatusEnum.Pending),
																	 new Q(Transfer.Columns.Status, Transfer.StatusEnum.Success))));
				refundTransferQuery.Columns = new ColumnSet(Transfer.Columns.Amount);
				refundTransferSet = new TransferSet(refundTransferQuery);
			}

			foreach (Transfer refundTransfer in refundTransferSet)
				amountRefunded += Math.Abs(refundTransfer.Amount);

			return Math.Round(amountRefunded, 2);
		}
		#endregion

		#region UpdateAndResolveOverapplied
		public void UpdateAndResolveOverapplied()
		{
			decimal amountRemaining = AmountRemaining();
			if (Math.Round(amountRemaining,2) < 0)
			{
				Query invoiceTransfersAppliedQuery = new Query(new And(new Q(InvoiceTransfer.Columns.TransferK, this.K),
															           new Q(Invoice.Columns.Type, Invoice.Types.Invoice)));
				invoiceTransfersAppliedQuery.TableElement = new Join(InvoiceTransfer.Columns.InvoiceK, Invoice.Columns.K);
				invoiceTransfersAppliedQuery.OrderBy = new OrderBy(Invoice.Columns.DueDateTime, OrderBy.OrderDirection.Ascending);
				InvoiceTransferSet invoiceTransferSet = new InvoiceTransferSet(invoiceTransfersAppliedQuery);

				string transferNote = "Transfer overapplied by " + ((double)(-1 * amountRemaining)).ToString("c") + ".";

				decimal amountToUnApply = 0;
				for (int i = invoiceTransferSet.Count - 1; i >= 0; i--)
				{
					if (Math.Round(amountRemaining,2) < 0)
					{
						Invoice invoice = new Invoice(invoiceTransferSet[i].InvoiceK);
						if (invoiceTransferSet[i].Amount <= -1 * amountRemaining)
						{
							amountToUnApply = invoiceTransferSet[i].Amount;
							invoiceTransferSet[i].Delete();
						}
						else
						{
							amountToUnApply = -1 * amountRemaining;
							invoiceTransferSet[i].Amount -= amountToUnApply;
							invoiceTransferSet[i].Amount = Math.Round(invoiceTransferSet[i].Amount, 2);
							invoiceTransferSet[i].Update();
						}
						transferNote += string.Format("\nAutomatically unapplied {0} from invoice #{1}.", amountToUnApply.ToString("c"), invoice.K);
						amountRemaining += amountToUnApply;
                        //if (this.Method == Methods.TicketSales)
                        //    BankExport.GenerateBankExportForTicketFundsUsed(this, -1 * amountToUnApply, new Invoice(invoiceTransferSet[i].InvoiceK));

						invoice.AddNote(string.Format("Transfer #{0} was overapplied. {1} has been unapplied from transfer #{0}.", this.K, amountToUnApply.ToString("c")), "System");
						invoice.UpdateAndSetPaidStatus();
					}
				}
				this.AddNote(transferNote, "System");

				if (Math.Round(amountRemaining, 2) == 0)
					this.IsFullyApplied = true;
				else
					this.IsFullyApplied = false;
				this.Update();
			}
		}

		#endregion

		#region SetAndUpdateIsFullyApplied
		public void SetIsFullyApplied()
		{
			if (this.Status.Equals(Transfer.StatusEnum.Success) || this.Status.Equals(Transfer.StatusEnum.Pending))
			{
				decimal amountRemaining = this.AmountRemaining();
				if (this.IsFullyApplied == false && Math.Round(amountRemaining, 2) == 0)
				{
					this.IsFullyApplied = true;
				}
				else if (this.IsFullyApplied == true && Math.Round(amountRemaining, 2) != 0)
				{
					this.IsFullyApplied = false;
				}
			}
		}
		#endregion

		#region CopyThisTransfer
		public Transfer CopyThisTransfer()
		{
			Transfer copyToTransfer = new Transfer();
			// Copy all details from this transfer to the new copyToTransfer
			copyToTransfer.Guid = Guid.NewGuid();
			copyToTransfer.ActionUsrK = this.ActionUsrK;
			copyToTransfer.Amount = this.Amount;
			copyToTransfer.ClientHost = this.ClientHost;
			copyToTransfer.DateTimeCreated = DateTime.Now;
            //copyToTransfer.DSIBankAccount = this.DSIBankAccount;
			copyToTransfer.Method = this.Method;
			if (this.Method.Equals(Methods.Card))
			{
				copyToTransfer.CardAddress1 = this.CardAddress1;
				copyToTransfer.CardAddressArea = this.CardAddressArea;
				copyToTransfer.CardAddressTown = this.CardAddressTown;
				copyToTransfer.CardAddressCounty = this.CardAddressCounty;
				copyToTransfer.CardAddressCountryK = this.CardAddressCountryK;
				copyToTransfer.CardCV2 = this.CardCV2;
				copyToTransfer.CardExpires = this.CardExpires;
				copyToTransfer.CardIssue = this.CardIssue;
				copyToTransfer.CardName = this.CardName;
				copyToTransfer.CardDigits = this.CardDigits;
				copyToTransfer.CardNumberEnd = this.CardNumberEnd;
				copyToTransfer.CardNumberHash = this.CardNumberHash;
				copyToTransfer.CardPostcode = this.CardPostcode;
				copyToTransfer.CardStart = this.CardStart;
				copyToTransfer.CardType = this.CardType;
			}
			else if (this.Method.Equals(Methods.BankTransfer))
			{
				copyToTransfer.BankAccountName = this.BankAccountName;
				copyToTransfer.BankAccountNumber = this.BankAccountNumber;
				copyToTransfer.BankName = this.BankName;
				copyToTransfer.BankSortCode = this.BankSortCode;
				copyToTransfer.BankTransferReference = this.BankTransferReference;
			}
			copyToTransfer.Company = this.Company;

			copyToTransfer.Status = this.Status;
			copyToTransfer.PromoterK = this.PromoterK;
			copyToTransfer.Status = this.Status;
			copyToTransfer.Type = this.Type;
			copyToTransfer.UsrK = this.UsrK;

			return copyToTransfer;
		}

		#endregion

		#region GetBankDetailsFromPromoterAccount
		public void GetBankDetailsFromPromoterAccount()
        {
            if ((this.BankSortCode.Trim() == "" || this.BankAccountNumber.Trim() == "") && this.Promoter != null)
            {
                this.BankAccountName = this.Promoter.BankAccountName;
                this.BankAccountNumber = this.Promoter.BankAccountNumber;
                this.BankName = this.Promoter.BankName;
                this.BankSortCode = this.Promoter.BankAccountSortCode;
            }
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
			else if(overrideExisting || this.UsrK == 0)
			{
				this.UsrK = CurrentUsr.K;
			}
			if (overrideExisting || this.ActionUsrK == 0)
				this.ActionUsrK = CurrentUsr.K;
		}
		#endregion

		#region RefundThisTransfer
		public Transfer RefundThisTransfer()
		{
			return RefundThisTransfer(this.Amount);
		}

		public Transfer RefundThisTransfer(decimal refundAmount)
		{
         	if (this.Type.Equals(Transfer.TransferTypes.Refund))
				throw new Exception("Cannot refund this transfer because it is a refund.");
            refundAmount = Math.Round(Math.Abs(refundAmount), 2);
			if(refundAmount == 0)
				throw new Exception("Cannot refund �0.00.");
            //double amountRemaining = this.AmountRemaining();
            if (this.Amount < refundAmount)
                throw new Exception("Cannot refund " + refundAmount.ToString("c") + " because this transfer amount is only " + this.Amount.ToString("c") + ".");

			Transfer refundTransfer = this.CopyThisTransfer();

            if (refundTransfer.Method == Methods.TicketSales)
            {
                refundTransfer.Method = Methods.BankTransfer;
            }
            if (refundTransfer.PromoterK > 0)
            {
                refundTransfer.GetBankDetailsFromPromoterAccount();
            }
			// Copy all details from this transfer to the new refundTransfer
			refundTransfer.Guid = Guid.NewGuid();
			refundTransfer.Amount = -1 * refundAmount;
			refundTransfer.Status = Transfer.StatusEnum.Pending;
			refundTransfer.Type = TransferTypes.Refund;
			refundTransfer.TransferRefundedK = this.K;
            //refundTransfer.DSIBankAccount = this.DSIBankAccount;
			if (Math.Abs(refundTransfer.Amount) < Math.Abs(this.Amount))
				refundTransfer.RefundStatus = Transfer.RefundStatuses.PartialRefund;
			else
				refundTransfer.RefundStatus = Transfer.RefundStatuses.FullRefund;

			refundTransfer.AddNote("Refund for transfer #" + this.K.ToString(), "System");

			return refundTransfer;
		}

        public Transfer RefundViaBACS()
        {
            return RefundViaBACS(this.AmountRemaining());
        }

		public Transfer RefundViaBACS(decimal amount)
        {
            return RefundViaBACS(this.RefundThisTransfer(amount));
        }

        public static Transfer RefundViaBACS(Transfer refundTransfer)
        {
            refundTransfer.Method = Methods.BankTransfer;
            refundTransfer.GetBankDetailsFromPromoterAccount();
            
            try
            {
                if (refundTransfer.BankSortCode.Trim().Length > 0 && refundTransfer.BankAccountNumber.Trim().Length > 0)
                {
                    refundTransfer.Update();

                    BankExport.GenerateBankExportForRefundTransfer(refundTransfer);                    
                }
                else
                    throw new DsiUserFriendlyException("Insufficient bank details to refund via BACS." + refundTransfer.AsHTML());
            }
            catch (Exception ex)
            {
				Utilities.AdminEmailAlert("Unable to refund via BACS because of insufficient bank details.", "BACS refund failed", ex, refundTransfer);
				throw ex;
            }

            return refundTransfer;
        }
		//public Transfer RefundThisTransferAndAutoCreateCredits(Transfer.Statuses status)
		//{
		//    Transfer refundTransfer = this.RefundThisTransfer(Guid.NewGuid(), status);

		//    InvoiceTransferSet invoiceTransferSet = new InvoiceTransferSet(new Query(new Q(InvoiceTransfer.Columns.TransferK, this.K)));
		//    foreach (InvoiceTransfer invoiceTransfer in invoiceTransferSet)
		//    {
		//        InvoiceTransfer refundInvoiceTransfer = new InvoiceTransfer();
		//        refundInvoiceTransfer.InvoiceK = invoiceTransfer.InvoiceK;
		//        refundInvoiceTransfer.Amount = -1 * invoiceTransfer.Amount;
		//        refundInvoiceTransfer.TransferK = refundTransfer.K;

		//        refundInvoiceTransfer.Update();
		//    }
		//    // Update all invoices affected by this
		//    refundTransfer.UpdateAffectedInvoices();

		//    return refundTransfer;
		//}

		#endregion

		#region ReferenceNumberToHtml
		public string ReferenceNumberToHtml()
		{
			string refNbr = "&nbsp;";

			if (this.Method.Equals(Transfer.Methods.Card))
			{
				refNbr = this.CardNumberEnd.Length > 0 ? Utilities.MaskedCardNumber(this.CardDigits, this.CardNumberEnd) : "&nbsp;";
			}
			else if (this.Method.Equals(Transfer.Methods.Cheque))
			{
				refNbr = this.ChequeReferenceNumber.Length > 0 ? this.ChequeReferenceNumber : "&nbsp;";
			}
			else if (this.Method.Equals(Transfer.Methods.BankTransfer))
			{
				refNbr = this.BankTransferReference.Length > 0 ? this.BankTransferReference : "&nbsp;";
			}

			return refNbr;
		}
		#endregion

		#region GenerateReportInHTML
		public StringBuilder GenerateReportStringBuilder(bool linksEnabled)
        {
            StringBuilder sb = new StringBuilder();

			sb.Append(@"<form id='form1' runat='server'><div style='font-family:Verdana;'><table width='100%' border='0' cellspacing='0' cellpadding='0' height='100%'><tr><td valign='top'>
                        <table width='100%'>");
			sb.Append(Utilities.GenerateHTMLHeaderRowString(this.Type.Equals(TransferTypes.Payment) ? "RECEIPT" : "REMITTANCE&nbsp;ADVICE"));
			sb.Append(@"<tr>
                                <td align='left' valign='top' style='padding-left:48px;'>");
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

			sb.Append(@"</td><td width='350'></td><td valign='top' width='100'>Transfer&nbsp;No.<br><br>Acc&nbsp;No.<br><br>Date</td>
							<td align='right' valign='top' width='125'>");

			sb.Append("TRN");
			sb.Append(this.K.ToString());
			sb.Append("<br><br>");
			if (this.Promoter != null)
				sb.Append(this.PromoterK.ToString());
			else if (this.Usr != null)
				sb.Append(this.UsrK.ToString());
			else
				sb.Append("&nbsp;");
			sb.Append("<br><br>");
			sb.Append(this.DateTimeCreated.ToString("dd/MM/yy"));
			sb.Append("</td></tr>");

			//if (this.DateTimeComplete != null && this.DateTimeComplete > DateTime.MinValue)
			//{
			//    sb.Append(@"<br><br><b>Date Completed</b><br>" + this.DateTimeComplete.ToShortDateString());
			//}

            sb.Append(@"</table><br><br>
                        <table width='100%' cellspacing='0' cellpadding='3' class='BorderBlack Top Bottom Right'>
                            <tr>
                                <td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='250'><b>Transfer</b></td>
                                <td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='85'><b>Method</b></td>
                                <td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='160'><b>Method&nbsp;Ref#</b></td>
                                <td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='90'><b>Status</b></td>
                                <td style='vertical-align:bottom;' class='BorderBlack Bottom Left' align='left' width='85'><b>Amount</b></td>
                            </tr>");

			sb.Append(@"<tr><td class='BorderBlack Left'>" + this.TypeToString + @"</td>" +
						   "<td class='BorderBlack Left'>" + Utilities.CamelCaseToString(this.Method.ToString()).Replace(" ", "&nbsp;") + @"</td>
						   <td class='BorderBlack Left'>");
			sb.Append(this.ReferenceNumberToHtml());
			sb.Append(@"</td>
						 	 <td class='BorderBlack Left'>" + this.Status.ToString() + @"</td>
						     <td class='BorderBlack Left' align='right'>" + Utilities.MoneyToHTML(this.Amount) + @"</td></tr></table><br><br>");

            
            if (InvoiceTransfers.Count > 0)
			{	// Rename "Date" to "Tax Date", as per Dave's request 7/2/07
				sb.Append(@"<table width='100%' cellspacing='0' cellpadding='3' class='BorderBlack Top Bottom Right'>
                            <tr>
								<td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='305'><b>Items&nbsp;Applied&nbsp;To</b></td>
                                <td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='65'><b>Tax&nbsp;Date</b></td>
								<td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='150'><b>Total Invoice Amount</b></td>
                                <td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='150'><b>Amount Applied To Invoice</b></td>
                            </tr>");

                foreach (InvoiceTransfer invoiceTransfer in InvoiceTransfers)
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
                }

                sb.Append(@"</table><br>");
            }

			Query refundTransferQuery = new Query();
			if(this.Type.Equals(TransferTypes.Payment))
				refundTransferQuery.QueryCondition = new And(new Q(Transfer.Columns.TransferRefundedK, this.K),
															 new Or(new Q(Transfer.Columns.Status, Transfer.StatusEnum.Pending),
																	new Q(Transfer.Columns.Status, Transfer.StatusEnum.Success)));
			else
				refundTransferQuery.QueryCondition = new And(new Q(Transfer.Columns.K, this.TransferRefundedK),
															 new Or(new Q(Transfer.Columns.Status, Transfer.StatusEnum.Pending),
																	new Q(Transfer.Columns.Status, Transfer.StatusEnum.Success)));

			TransferSet refundTransferSet = new TransferSet(refundTransferQuery);

			if (refundTransferSet.Count > 0)
			{
				sb.Append(@"<br><table width='100%' cellspacing='0' cellpadding='3' class='BorderBlack Top Bottom Right'>
                            <tr>
								<td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='250'><b>");

                if (this.Type.Equals(TransferTypes.Payment))
                {
                    if (this.Method == Methods.TicketSales)
                        sb.Append(Utilities.CamelCaseToString(Methods.TicketSales.ToString()) + " Release");
                    else
                        sb.Append(TransferTypes.Refund.ToString());
                }
                else if (this.Type.Equals(TransferTypes.Refund))
                    sb.Append(TransferTypes.Payment.ToString());

				sb.Append(@"</b></td>
                                <td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='85'><b>Method</b></td>
                                <td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='160'><b>Method&nbsp;Ref#</b></td>
                                <td style='vertical-align:bottom;' class='BorderBlack Bottom Left' width='90'><b>Status</b></td>
                                <td style='vertical-align:bottom;' class='BorderBlack Bottom Left' align='left' width='85'><b>Amount</b></td>
                            </tr>");

				foreach (Transfer refundTransfer in refundTransferSet)
				{
					sb.Append(@"<tr>
                                <td class='BorderBlack Left'>");
                    if (linksEnabled)
                    {
                        if (refundTransfer.TicketPromoterEvent != null)
                            sb.Append(Utilities.Link(refundTransfer.TicketPromoterEvent.UrlReport(), refundTransfer.TypeAndK));
                        else
                            sb.Append(refundTransfer.Link());
                    }
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
				}

				sb.Append(@"</table><br>");
			}

			// DSI Registration Footer
			sb.Append(Utilities.GenerateHTMLFooterRowString());

			sb.Append(@"</table></div></form>");

            return sb;
        }
        #endregion

		#region JoinedInvoiceTransfer
		public InvoiceTransfer JoinedInvoiceTransfer
		{
			get
			{
				if (joinedInvoiceTransfer == null)
				{
					joinedInvoiceTransfer = new InvoiceTransfer(this, Transfer.Columns.K);
				}
				return joinedInvoiceTransfer;
			}
			set
			{
				joinedInvoiceTransfer = value;
			}
		}
		private InvoiceTransfer joinedInvoiceTransfer;
		#endregion

        #region InvoiceTransfers
        public InvoiceTransferSet InvoiceTransfers
        {
            get
            {
                if (invoiceTransfers == null)
                {
                    Query InvoiceTransferQuery = new Query(new Q(InvoiceTransfer.Columns.TransferK, this.K));
                    invoiceTransfers = new InvoiceTransferSet(InvoiceTransferQuery);
                }
                return invoiceTransfers;
            }
            set
            {
                invoiceTransfers = null;
            }
        }
        private InvoiceTransferSet invoiceTransfers;

        #endregion

        #region InvoicesAppliedTo
        public InvoiceSet InvoicesAppliedTo
        {
            get
            {
                if (invoicesAppliedTo == null)
                {
                    Query invoiceTransferQuery = new Query(new Q(InvoiceTransfer.Columns.TransferK, this.K));

                    invoiceTransferQuery.TableElement = new Join(InvoiceTransfer.Columns.InvoiceK, Invoice.Columns.K, QueryJoinType.Inner);
                    invoicesAppliedTo = new InvoiceSet(invoiceTransferQuery);
                }
                return invoicesAppliedTo;
            }
            set
            {
                invoicesAppliedTo = null;
            }
        }
        private InvoiceSet invoicesAppliedTo;
        #endregion

        #region UpdateStatus
        public void UpdateStatus(StatusEnum status)
        {
            if (this.Status != status)
            {
                this.AddNote("Status changed from " + this.Status.ToString() + " to " + status.ToString(), "System");
                this.Status = status;
                if (status == StatusEnum.Success)
                    DateTimeComplete = DateTime.Now;
                else if (status == StatusEnum.Pending)
                    DateTimeComplete = DateTime.MinValue;
                this.Update();
                this.UpdateAffectedInvoices();
            }
        }
        #endregion

        #region UpdateAffectedInvoices
        /// <summary>
		/// UpdateAffectedInvoices finds all invoices that this transfer is applied to, then compares the invoice total against all successful transfers and credits and updates the invoice paid status
		/// </summary>
		public void UpdateAffectedInvoices()
		{
            InvoicesAppliedTo = null;

			// Update Invoices that are affected by this transfer
			foreach (Invoice invoice in InvoicesAppliedTo)
			{
				invoice.UpdateAndSetPaidStatus();
			}
			bool prevIsFullyApplied = this.IsFullyApplied;
			SetIsFullyApplied();
			if(prevIsFullyApplied != this.IsFullyApplied)
				this.Update();
		}

		#endregion

		#region Urls
		public string UrlAdmin(params string[] par)
		{
			string[] fullParams = Cambro.Misc.Utility.JoinStringArrays(new string[] { "K", this.K.ToString() }, par);
			return UrlInfo.PageUrl(UrlInfo.PageTypes.Admin, "transferscreen", fullParams);

			//string[] par = new string[]{"K", this.K.ToString()};
			//return UrlAdmin(par);

			
		}
		public string UrlAdminRefundMe(decimal amount)
		{
			string[] par = new string[] { "TransferRefundK", this.K.ToString(), "RefundAmount", ((double)(amount * 100)).ToString() };
			return UrlInfo.PageUrl(UrlInfo.PageTypes.Admin, "transferscreen", par);
		}
		public string UrlAdminTransferRefunded(params string[] par)
		{
			string[] fullParams = Cambro.Misc.Utility.JoinStringArrays(new string[] { "K", this.TransferRefundedK.ToString() }, par);
			return UrlInfo.PageUrl(UrlInfo.PageTypes.Admin, "transferscreen", fullParams);

			//string[] par = new string[] { "K", this.TransferRefundedK.ToString() };
			//return UrlAdmin(par);
		}
		public string UrlAdminRetryFailedTransfer(params string[] par)
		{
			string[] fullParams = Cambro.Misc.Utility.JoinStringArrays(new string[] { "FailedTransferK", this.K.ToString() }, par);
			return UrlInfo.PageUrl(UrlInfo.PageTypes.Admin, "transferscreen", fullParams);

			//string[] par = new string[] { "FailedTransferK", this.K.ToString() };
			//return UrlAdmin(par);
		}
		public string UrlAdminRetryFailedTransfer(int invoiceK)
		{
			if (invoiceK > 0)
			{
				string[] par = new string[] { "FailedTransferK", this.K.ToString(), "InvoiceK", invoiceK.ToString() };
				return UrlInfo.PageUrl(UrlInfo.PageTypes.Admin, "transferscreen", par);

				//return UrlAdmin(par);
			}
			else
				return UrlAdminRetryFailedTransfer();
		}
		public static string UrlAdminNewTransfer(params string[] par)
		{
			return UrlInfo.PageUrl(UrlInfo.PageTypes.Admin, "transferscreen", par);
		}
		public string Url(params string[] par)
		{
			return UrlReport(par);
		}
		public string UrlReport(params string[] par)
		{
			string[] fullParams = Cambro.Misc.Utility.JoinStringArrays(new string[] { "K", this.K.ToString(), "type", "transfer" }, par);
			return UrlInfo.PageUrl(UrlInfo.PageTypes.Blank, "reportgenerator", fullParams);
		}
		#endregion

		#region PurchaseCampaignCreditsWithRemainingFunds
		public CampaignCredit PurchaseCampaignCreditsWithRemainingFunds(Usr actionUsr, double discountForCredits)
		{
			decimal total = this.AmountRemaining();
			if (actionUsr.IsAdmin && total > 0)
			{
				DateTime now = Time.Now;

				InvoiceDataHolder idh = new InvoiceDataHolder()
				{
					CreatedDateTime = now,
					DueDateTime = now,
					DuplicateGuid = new Guid(),
					PromoterK = this.PromoterK,
					SalesUsrK = this.Promoter.SalesUsrK,
					TaxDateTime = now,
					Type = Invoice.Types.Invoice,
					VatCode = Invoice.VATCodes.T1
				};
				InvoiceItemDataHolder iidh = new InvoiceItemDataHolder()
				{
					//BuyableObjectK = campaingCredit.K,
					BuyableObjectType = Model.Entities.ObjectType.CampaignCredit,
					//Description = campaingCredit.Description,
					Discount = discountForCredits,
					RevenueStartDate = now,
					RevenueEndDate = now,
					//ShortDescription = campaingCredit.Description,
					Type = InvoiceItem.Types.CampaignCredits,
					VatCode = InvoiceItem.VATCodes.T1
				};
				iidh.SetTotal(total);
				int credits = CampaignCredit.CalculateTotalCreditsForMoney(iidh.Price, discountForCredits, this.Promoter);
				if (credits > 0)
				{
					iidh.Discount = 1 - (double)Math.Round(iidh.Price / credits, 4);
					iidh.SetTotal(total);
					string description = credits.ToString() + " credits";
					iidh.Description = description;
					iidh.ShortDescription = description;
					idh.InvoiceItemDataHolderList.Add(iidh);
					Invoice invoice = idh.UpdateInsertDelete();
					invoice.SetUsrAndActionUsr(actionUsr);
					invoice.AssignSalesUsrAndAmount();
					invoice.ApplyTransfersToThisInvoice(this);
					invoice.UpdateAndSetPaidStatus();

					CampaignCredit campaingCredit = new CampaignCredit()
					{
						ActionDateTime = now,
						ActionUsrK = actionUsr.K,
						BuyableObjectK = invoice.K,
						BuyableObjectType = Model.Entities.ObjectType.Invoice,
						Credits = credits,
						Description = description,
						DisplayOrder = 0,
						Enabled = true,
						FixedDiscount = iidh.Discount,
						InvoiceItemType = InvoiceItem.Types.CampaignCredits,
						IsPriceFixed = true,
						PromoterK = this.PromoterK
					};
					campaingCredit.SetUsrAndActionUsr(actionUsr);
					campaingCredit.UpdateWithRecalculateBalance();

					invoice.Items[0].BuyableObjectK = campaingCredit.K;
					invoice.Items[0].Update();

					return campaingCredit;
				}
				else
					return null;
			}
			else
				return null;
		}
		#endregion

	}

    #region TransferDataHolder
	/// <summary>
	/// TransferDataHolder is a stub for Transfer that can be stored in the page ViewState
	/// </summary>
    [Serializable]
    public class TransferDataHolder : BobDataHolder
    {
        #region Variables
        private int k = 0;
		private decimal amount = 0.0m;
        private Transfer.StatusEnum status = Transfer.StatusEnum.Pending;
        private Transfer.Methods method = Transfer.Methods.Card;
        private DateTime dateTimeComplete = DateTime.MinValue;
		private string referenceNumber = "";

        #endregion

        #region Properties
        /// <summary>
        /// The primary key
        /// </summary>
        public int K
        {
            get { return this.k; }
            set { this.k = value; }
        }
        /// <summary>
        /// Total Amount
        /// </summary>
		public decimal Amount
        {
            get { return this.amount; }
            set
            {
                this.State = DataHolderState.Modified;
                this.amount = Math.Round(value,2);
            }
        }
        /// <summary>
        /// Status
        /// </summary>
        public Transfer.StatusEnum Status
        {

            get { return this.status; }
            set
            {
                this.State = DataHolderState.Modified;
                this.status = value;
            }
        }
        /// <summary>
        /// Method
        /// </summary>
        public Transfer.Methods Method
        {

            get { return this.method; }
            set
            {
                this.State = DataHolderState.Modified;
                this.method = value;
            }
        }
        /// <summary>
        /// Date Transfer was completed
        /// </summary>
        public DateTime DateTimeComplete
        {
            get { return this.dateTimeComplete; }
            set
            {
                this.State = DataHolderState.Modified;
                this.dateTimeComplete = value;
            }
        }
		/// <summary>
		/// Reference Number (Card Number or 
		/// </summary>
		public string ReferenceNumber
		{
			get { return this.referenceNumber; }
			set
			{
				this.State = DataHolderState.Modified;
				this.referenceNumber = value;
			}
		}
        #endregion

        #region Constructors
        public TransferDataHolder()
        {
        }

        public TransferDataHolder(Transfer transfer)
        {
            this.k = transfer.K;
            this.amount = Math.Round(transfer.Amount,2);
            this.status = transfer.Status;
            this.dateTimeComplete = transfer.DateTimeComplete;
            this.method = transfer.Method;
			if (transfer.Method.Equals(Transfer.Methods.Card))
				this.referenceNumber = transfer.CardNumberEnd;
			else if (transfer.Method.Equals(Transfer.Methods.BankTransfer))
				this.referenceNumber = transfer.BankTransferReference;
			else if (transfer.Method.Equals(Transfer.Methods.Cheque))
			    this.referenceNumber = transfer.ChequeReferenceNumber;
			else
				this.referenceNumber = "";
        }
        #endregion

        #region Export To Transfer
        public Transfer ExportToTransfer()
        {
            Transfer transfer = new Transfer();
            transfer.K = this.k;
            if (this.k > 0)
                transfer = new Transfer(k);
            else
                // if it doesnt already exist in the database, then it is new.
                this.State = DataHolderState.Added;
            transfer.Amount = Math.Round(this.amount,2);
            transfer.Status = this.status;
            transfer.Method = this.method;
            transfer.DateTimeComplete = this.dateTimeComplete;

			transfer = this.SetReferenceNumber(transfer);			

            return transfer;
        }

		public Transfer SetReferenceNumber(Transfer transfer)
		{
			if (transfer.Method.Equals(Transfer.Methods.Card))
				transfer.CardNumberEnd = this.ReferenceNumber;
			else if (transfer.Method.Equals(Transfer.Methods.BankTransfer))
				transfer.BankTransferReference = this.ReferenceNumber;
			else if (transfer.Method.Equals(Transfer.Methods.Cheque))
				transfer.ChequeReferenceNumber = this.ReferenceNumber;

			return transfer;
		}
        #endregion

        #region UpdateInsertDelete
        public Transfer UpdateInsertDelete()
        {
            Transfer transfer = ExportToTransfer();
			if (this.State == DataHolderState.Deleted)
			{
				if(this.K > 0)
					transfer.Delete();
			}
			else
			{
				if (transfer.DateTimeCreated.Equals(DateTime.MinValue))
					transfer.DateTimeCreated = DateTime.Now;
				transfer.Update();
				this.k = transfer.K;
				transfer.UpdateAffectedInvoices();
				this.State = DataHolderState.Unchanged;
			}
            return transfer;
        }
        #endregion
    }

    #endregion
    
#endregion
}
