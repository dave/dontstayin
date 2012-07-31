using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Cambro.Misc;
using Cambro.Web;

namespace Bobs
{
    #region BankExport
    /// <summary>
    /// Exports to bank for transferring funds
    /// </summary>
    [Serializable]
	public partial class BankExport : IBobAsHTML
    {
        #region Simple members
        /// <summary>
        /// Primary Key
        /// </summary>
        public override int K
        {
            get { return this[BankExport.Columns.K] as int? ?? 0; }
            set { this[BankExport.Columns.K] = value; }
        }
        /// <summary>
        /// Date when it was added
        /// </summary>
        public override DateTime AddedDateTime
        {
            get { return (DateTime)this[BankExport.Columns.AddedDateTime]; }
            set { this[BankExport.Columns.AddedDateTime] = value; }
        }
        /// <summary>
        /// Date when it was output was exported
        /// </summary>
        public override DateTime OutputDateTime
        {
            get { return (DateTime)this[BankExport.Columns.OutputDateTime]; }
            set { this[BankExport.Columns.OutputDateTime] = value; }
        }
        /// <summary>
        /// Date when it will be processed
        /// </summary>
        public override DateTime ProcessingDateTime
        {
            get { return (DateTime)this[BankExport.Columns.ProcessingDateTime]; }
            set { this[BankExport.Columns.ProcessingDateTime] = value; }
        }
        /// <summary>
        /// Transfer key reference
        /// </summary>
        public override int TransferK
        {
            get { return (int)this[BankExport.Columns.TransferK]; }
            set { this[BankExport.Columns.TransferK] = value; }
        }
        /// <summary>
        /// Promoter key reference
        /// </summary>
        public override int PromoterK
        {
            get { return (int)this[BankExport.Columns.PromoterK]; }
            set { this[BankExport.Columns.PromoterK] = value; }
        }
        /// <summary>
        /// Type enum: BACS = 1, Internal = 2
        /// </summary>
        public override Types Type
        {
            get { return (Types)this[BankExport.Columns.Type]; }
            set { this[BankExport.Columns.Type] = value; }
        }
        /// <summary>
        /// Amount of transaction
        /// </summary>
        public override decimal Amount
        {
			get { return (decimal)this[BankExport.Columns.Amount]; }
            set { this[BankExport.Columns.Amount] = value; }
        }
        /// <summary>
        /// Unique batch reference #
        /// </summary>
        public override string BatchRef
        {
            get { return (string)this[BankExport.Columns.BatchRef]; }
            set { this[BankExport.Columns.BatchRef] = value; }
        }
        /// <summary>
        /// Status enum: Added, AwaitingConfirmation, Successful, Failed, Cancelled.
        /// </summary>
        public override Statuses Status
        {
            get { return (Statuses)this[BankExport.Columns.Status]; }
            set { this[BankExport.Columns.Status] = value; }
        }
        /// <summary>
        /// Beneficiary's bank name
        /// </summary>
        public override string BankName
        {
            get { return (string)this[BankExport.Columns.BankName]; }
            set { this[BankExport.Columns.BankName] = value; }
        }
        /// <summary>
        /// Beneficiary's bank account sort code
        /// </summary>
        /// <summary>
        /// Beneficiary's bank account sort code
        /// </summary>
        public override string BankAccountSortCode
        {
            get { return (string)this[BankExport.Columns.BankAccountSortCode]; }
            set { this[BankExport.Columns.BankAccountSortCode] = value.Replace(" ", "").Replace("-", ""); }
        }
        /// <summary>
        /// Beneficiary's bank account number
        /// </summary>
        public override string BankAccountNumber
        {
            get { return (string)this[BankExport.Columns.BankAccountNumber]; }
            set { this[BankExport.Columns.BankAccountNumber] = value.Replace(" ", "").Replace("-", ""); }
        }
        /// <summary>
        /// Details of bank export for reference
        /// </summary>
        public override string Details
        {
            get { return (string)this[BankExport.Columns.Details]; }
            set { this[BankExport.Columns.Details] = value; }
        }
        /// <summary>
        /// Date of transaction that it is referencing
        /// </summary>
        public override DateTime ReferenceDateTime
        {
            get { return (DateTime)this[BankExport.Columns.ReferenceDateTime]; }
            set { this[BankExport.Columns.ReferenceDateTime] = value; }
        }
        #endregion

        #region Constants
        public const string CURRENT_ACCOUNT_TO_CLIENT_ACCOUNT_ABBREVIATION = "CUR2CL";
        public const string CLIENT_ACCOUNT_TO_CURRENT_ACCOUNT_ABBREVIATION = "CL2CUR";
        public const string CLIENT_ACCOUNT_TO_BACS_ABBREVIATION = "CL2BACS";
        public const string CURRENT_ACCOUNT_TO_BACS_ABBREVIATION = "CUR2BACS";
        public const string CLIENT_ACCOUNT_TO_INTERNAL_BACS_ABBREVIATION = "CL2PR";
        public const string CURRENT_ACCOUNT_TO_INTERNAL_BACS_ABBREVIATION = "CUR2PR";
        #endregion

        #region Enums

        #endregion

        #region Enums To ListItem[]
        public static ListItem[] TypesAsListItemArray()
        {
            List<ListItem> ListItems = new List<ListItem>();
            ListItems.Add(new ListItem(Utilities.CamelCaseToString(Types.ExternalBACSRefundToPromoter.ToString()), Convert.ToInt32(Types.ExternalBACSRefundToPromoter).ToString()));
            ListItems.Add(new ListItem(Utilities.CamelCaseToString(Types.ExternalBACSTicketFundsToPromoter.ToString()), Convert.ToInt32(Types.ExternalBACSTicketFundsToPromoter).ToString()));
            ListItems.Add(new ListItem(Utilities.CamelCaseToString(Types.InternalTransferRefundToPromoter.ToString()), Convert.ToInt32(Types.InternalTransferRefundToPromoter).ToString()));
            ListItems.Add(new ListItem(Utilities.CamelCaseToString(Types.InternalTransferNonTicketFunds.ToString()), Convert.ToInt32(Types.InternalTransferNonTicketFunds).ToString()));
            ListItems.Add(new ListItem(Utilities.CamelCaseToString(Types.InternalTransferTicketFundsToPromoter.ToString()), Convert.ToInt32(Types.InternalTransferTicketFundsToPromoter).ToString()));
            ListItems.Add(new ListItem(Utilities.CamelCaseToString(Types.InternalTransferTicketFundsUsed.ToString()), Convert.ToInt32(Types.InternalTransferTicketFundsUsed).ToString()));

            return ListItems.ToArray();
        }

        public static ListItem[] StatusesAsListItemArray()
        {
            List<ListItem> ListItems = new List<ListItem>();
            ListItems.Add(new ListItem(Statuses.Added.ToString(), Convert.ToInt32(Statuses.Added).ToString()));
            ListItems.Add(new ListItem(Utilities.CamelCaseToString(Statuses.AwaitingConfirmation.ToString()), Convert.ToInt32(Statuses.AwaitingConfirmation).ToString()));
            ListItems.Add(new ListItem(Statuses.Cancelled.ToString(), Convert.ToInt32(Statuses.Cancelled).ToString()));
            ListItems.Add(new ListItem(Statuses.Failed.ToString(), Convert.ToInt32(Statuses.Failed).ToString()));
            ListItems.Add(new ListItem(Statuses.Successful.ToString(), Convert.ToInt32(Statuses.Successful).ToString()));

            return ListItems.ToArray();
        }
        #endregion

        #region Properties
        public string BeneficiaryName
        {
            get
            {
                if ((Type == Types.ExternalBACSRefundToPromoter || Type == Types.ExternalBACSTicketFundsToPromoter || Type == Types.InternalTransferTicketFundsToPromoter || Type == Types.InternalTransferRefundToPromoter) && Promoter != null)
                    return Promoter.Name;
                //else if (this.BankAccountNumber == Vars.DSI_CLIENT_BANK_ACCOUNT_NUMBER && this.BankAccountSortCode == Vars.DSI_CLIENT_BANK_SORT_CODE)
                //    return "Client Account";
                else if (this.BankAccountNumber == Vars.DSI_BANK_ACCOUNT_NUMBER && this.BankAccountSortCode == Vars.DSI_BANK_SORT_CODE)
                    return "Current Account";
                else 
                    return "";
            }
        }

        public string BeneficiaryLink
        {
            get
            {
                if ((Type == Types.ExternalBACSRefundToPromoter || Type == Types.ExternalBACSTicketFundsToPromoter || Type == Types.InternalTransferTicketFundsToPromoter || Type == Types.InternalTransferRefundToPromoter) && Promoter != null)
                    return Promoter.LinkNewWindow();
                //else if (this.BankAccountNumber == Vars.DSI_CLIENT_BANK_ACCOUNT_NUMBER && this.BankAccountSortCode == Vars.DSI_CLIENT_BANK_SORT_CODE)
                //    return "Client Account";
                else if (this.BankAccountNumber == Vars.DSI_BANK_ACCOUNT_NUMBER && this.BankAccountSortCode == Vars.DSI_BANK_SORT_CODE)
                    return "Current Account";
                else
                    return "";
            }
        }

        public string PayeeName
        {
            get
            {
                if (Type == Types.InternalTransferTicketFundsUsed && this.Promoter != null)
                    return Promoter.Name;
                else if (Type == Types.ExternalBACSRefundToPromoter || Type == Types.InternalTransferRefundToPromoter)
                    return "Current Account";
                else
                    return "";
            }
        }

        public string PayeeLink
        {
            get
            {
                if (Type == Types.InternalTransferTicketFundsUsed && this.Promoter != null)
                    return Promoter.LinkNewWindow();
                else if (Type == Types.ExternalBACSRefundToPromoter || Type == Types.InternalTransferRefundToPromoter)
                    return "Current Account";
                else
                    return "";
            }
        }

        // Max length = 16 for internal and 18 for BACS
        public string PaymentRef
        {
            get
            {
                string paymentRef = "";
                if (this.K > 0)
                    paymentRef += this.K.ToString() + "-";
                if (this.Type == Types.InternalTransferNonTicketFunds)
                {
                    if (this.BankAccountNumber == Vars.DSI_BANK_ACCOUNT_NUMBER && this.BankAccountSortCode == Vars.DSI_BANK_SORT_CODE)
                        paymentRef += "PA";
                    else
                        paymentRef += "RE";
                    paymentRef += (this.ReferenceDateTime > DateTime.MinValue ? "-" + this.ReferenceDateTime.ToString("yyMMdd") : "");
                }
                else if (this.Type == Types.InternalTransferTicketFundsUsed)
                    paymentRef += "TIXFUNDS";
                else if (this.Type == Types.InternalTransferTicketFundsToPromoter || this.Type == Types.ExternalBACSTicketFundsToPromoter)
                    paymentRef += "TIXPAY";
                else if (this.Type == Types.ExternalBACSRefundToPromoter || this.Type == Types.InternalTransferRefundToPromoter)
                    paymentRef += "REFUND";

                return paymentRef;
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
                if (value == null)
                    this.PromoterK = 0;
                else
                    this.PromoterK = promoter.K;
            }
        }
        private Promoter promoter;
        #endregion

        #region Transfer
        public Transfer Transfer
        {
            get
            {
                if (transfer == null && TransferK > 0)
                    transfer = new Transfer(TransferK);
                return transfer;
            }
            set
            {
                transfer = value;
                if (value == null)
                    this.TransferK = 0;
                else
                    this.TransferK = transfer.K;
            }
        }
        private Transfer transfer;
        #endregion
        #endregion

        #region Methods
		#region IBobAsHTML methods
		public string AsHTML()
        {
            string lineReturn = Vars.HTML_LINE_RETURN;
            StringBuilder sb = new StringBuilder();

            sb.Append(lineReturn);
            sb.Append(lineReturn);
            sb.Append("<u>BankExport details</u>");
            sb.Append(lineReturn);
            sb.Append("Payment ref: ");
            sb.Append(this.PaymentRef);
            sb.Append(lineReturn);
            sb.Append("Amount: ");
            sb.Append(Utilities.MoneyToHTML(this.Amount));
            sb.Append(lineReturn);
            if (this.BatchRef.Length > 0)
            {
                sb.Append("Batch ref: ");
                sb.Append(this.BatchRef);
                sb.Append(lineReturn);
            }
            sb.Append("Status: ");
            sb.Append(Utilities.CamelCaseToString(this.Status.ToString()));
            sb.Append(lineReturn);
            sb.Append("Type: ");
            sb.Append(Utilities.CamelCaseToString(this.Type.ToString()));
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
            if (this.Transfer != null)
            {
                sb.Append(this.Transfer.TypeAndK);
                sb.Append(lineReturn);
            }
            sb.Append("Bank name: ");
            sb.Append(this.BankName.ToString());
            sb.Append(lineReturn);
            sb.Append("Bank sort code: ");
            sb.Append(this.BankAccountSortCode.ToString());
            sb.Append(lineReturn);
            sb.Append("Bank account #: ");
            sb.Append(this.BankAccountNumber.ToString());
            sb.Append(lineReturn);
            sb.Append("Added: ");
            sb.Append(this.AddedDateTime.ToString("ddd dd/MM/yyyy HH:mm"));
            sb.Append(lineReturn);
            if (this.OutputDateTime > DateTime.MinValue)
            {
                sb.Append("Output: ");
                sb.Append(this.OutputDateTime.ToString());
                sb.Append(lineReturn);
            }
            if (this.ProcessingDateTime > DateTime.MinValue)
            {
                sb.Append("Processed: ");
                sb.Append(this.ProcessingDateTime.ToString());
                sb.Append(lineReturn);
            }
            if (this.ReferenceDateTime > DateTime.MinValue)
            {
                sb.Append("Reference date: ");
                sb.Append(this.ReferenceDateTime.ToString());
                sb.Append(lineReturn);
            }
            return sb.ToString();
		}
		#endregion

		public void UpdateStatus(Statuses status)
        {
            if (this.Status != status)
            {
                this.Status = status;

                switch (status)
                {
                    case Statuses.Added: 
                        //this.AddedDateTime = DateTime.Now;
                        this.OutputDateTime = DateTime.MinValue;
                        this.ProcessingDateTime = DateTime.MinValue;
                        this.BatchRef = "";
                        if (this.Transfer != null && this.Transfer.Method == Transfer.Methods.BankTransfer && this.Transfer.Type == Transfer.TransferTypes.Refund)
                        {
                            this.Transfer.UpdateStatus(Transfer.StatusEnum.Pending);
                        }
                        break;

                    case Statuses.AwaitingConfirmation:
                        this.OutputDateTime = DateTime.Now;
                        this.ProcessingDateTime = DateTime.MinValue;
                        if (this.Transfer != null && this.Transfer.Method == Transfer.Methods.BankTransfer && this.Transfer.Type == Transfer.TransferTypes.Refund)
                        {
                            this.Transfer.UpdateStatus(Transfer.StatusEnum.Pending);
                        }
                        break;

                    
                    case Statuses.Successful:
                        this.ProcessingDateTime = DateTime.Now;
                        if (this.Transfer != null && this.Transfer.Method == Transfer.Methods.BankTransfer && this.Transfer.Type == Transfer.TransferTypes.Refund)
                        {
                            this.Transfer.UpdateStatus(Transfer.StatusEnum.Success);
                        }
                        break;

                    case Statuses.Cancelled:
                        this.ProcessingDateTime = DateTime.Now;
                        if (this.Transfer != null && this.Transfer.Method == Transfer.Methods.BankTransfer && this.Transfer.Type == Transfer.TransferTypes.Refund)
                        {
                            this.Transfer.UpdateStatus(Transfer.StatusEnum.Cancelled);
                        }
                        break;

                    case Statuses.Failed:
                        this.ProcessingDateTime = DateTime.Now;
                        if (this.Transfer != null && this.Transfer.Method == Transfer.Methods.BankTransfer && this.Transfer.Type == Transfer.TransferTypes.Refund)
                        {
                            this.Transfer.UpdateStatus(Transfer.StatusEnum.Failed);
                        }
                        break;

                    default:
                        break;
                }
            }

            this.Update();
        }

        public string LloydsLinkBeneficiaryToXML()
        {
            //<BeneficiaryItem AccountNumber="91590561" SortCode="400516" Amount="5.00" AccountTypeIndicator="" Name="BACS TEST P1" BankName="LLOYDSTSB BANK PLC" PaymentReferenceNumber="BACS TEST PAY1" />
            StringBuilder sb = new StringBuilder();
            sb.Append("<BeneficiaryItem AccountNumber=\"");
            sb.Append(this.BankAccountNumber);
            sb.Append("\" SortCode=\"");
            sb.Append(this.BankAccountSortCode);
            sb.Append("\" Amount=\"");
            sb.Append(this.Amount.ToString("0.00"));
            sb.Append("\" AccountTypeIndicator=\"\" Name=\"");
			sb.Append(Cambro.Misc.Utility.Snip(this.BeneficiaryName.Replace("&", "").Replace("'", "").Replace("(", "").Replace(")", ""), 18));
            sb.Append("\" BankName=\"");
            sb.Append(Cambro.Misc.Utility.Snip(this.BankName, 35));
            
            sb.Append("\" PaymentReferenceNumber=\"");
            sb.Append(this.PaymentRef);
            sb.Append("\" />");

            return sb.ToString();
        }

        public static string GetNextBatchRef()
        {
            // Get last bacth ref for today
            // increment the batch #
            char batchNumber = 'A';
            string batchRef = "";

            Query getLastBatchRefQuery = new Query(new Q(BankExport.Columns.Status, BankExport.Statuses.Successful));
            getLastBatchRefQuery.Columns = new ColumnSet();
            getLastBatchRefQuery.ExtraSelectElements.Add("LastBatchRef", "MAX([BankExport].[BatchRef])");

            BankExportSet bankExports = new BankExportSet(getLastBatchRefQuery);
            if (bankExports.Count > 0 && bankExports[0].ExtraSelectElements["LastBatchRef"] != DBNull.Value)
                batchRef = bankExports[0].ExtraSelectElements["LastBatchRef"].ToString();

            if(batchRef.IndexOf(DateTime.Today.ToString("yyyyMMdd")) >= 0)
            {
                batchNumber = batchRef[8];
                batchNumber++;
            }

            return DateTime.Today.ToString("yyyyMMdd") + batchNumber.ToString();
        }

        public static DateTime GetDateOfLastNonTicketFundsBankExport()
        {
            // Get last bacth ref for today
            // increment the batch #
            DateTime lastNonTicketFunds = new DateTime(2007, 8, 13);

            Query getLastNonTicketFundsQuery = new Query(new And(new Q(BankExport.Columns.Type, BankExport.Types.InternalTransferNonTicketFunds)));
            getLastNonTicketFundsQuery.Columns = new ColumnSet();
            getLastNonTicketFundsQuery.ExtraSelectElements.Add("LastNonTicketFunds", "MAX([BankExport].[AddedDateTime])");

            BankExportSet bankExports = new BankExportSet(getLastNonTicketFundsQuery);
            if (bankExports.Count > 0 && bankExports[0].ExtraSelectElements["LastNonTicketFunds"] != DBNull.Value)
                lastNonTicketFunds = Convert.ToDateTime(bankExports[0].ExtraSelectElements["LastNonTicketFunds"]);

            return new DateTime(lastNonTicketFunds.Year, lastNonTicketFunds.Month, lastNonTicketFunds.Day);
        }

        //public static void GenerateBankExportsForBookingFeesToDate()
        //{
        //    DateTime startDateRange = Utilities.CardnetDelay(GetDateOfLastNonTicketFundsBankExport()).AddDays(1);
        //    DateTime endDateRange = Utilities.CardnetDelay().AddDays(1);
            
        //    Query untransferedBookingFeesQuery = new Query(new And(new Q(InvoiceItem.Columns.Type, InvoiceItem.Types.EventTicketsBookingFee),
        //                                                           new Q(InvoiceItem.Columns.RevenueStartDate, QueryOperator.GreaterThanOrEqualTo, startDateRange),
        //                                                           new Q(InvoiceItem.Columns.RevenueStartDate, QueryOperator.LessThan, endDateRange)));
        //    untransferedBookingFeesQuery.ExtraSelectElements.Add("SumBookingFeeAmounts", "SUM([InvoiceItem].[Total])");
        //    untransferedBookingFeesQuery.ExtraSelectElements.Add("TheDate", "dateadd(day, 0, datediff(day, 0, [InvoiceItem].[RevenueStartDate]))");
        //    untransferedBookingFeesQuery.GroupBy = new GroupBy("dateadd(day, 0, datediff(day, 0, [InvoiceItem].[RevenueStartDate]))");
        //    untransferedBookingFeesQuery.Columns = new ColumnSet();

        //    InvoiceItemSet untransferedBookingFeeItems = new InvoiceItemSet(untransferedBookingFeesQuery);
        //    foreach (InvoiceItem ii in untransferedBookingFeeItems)
        //    {
        //        GenerateBankExportForBookingFees(ii);
        //    }
        //}


		#region I this this sets up the transfers between the current and client accounts. I've disabled it.

		//public static void GenerateBankExportsForAllNonTicketFundsToDate()
		//{
		//    DateTime startDateRange = Utilities.CardnetDelay(GetDateOfLastNonTicketFundsBankExport()).AddDays(1);
		//    DateTime endDateRange = Utilities.CardnetDelay().AddDays(1);

		//    GenerateBankExportsForNonTicketFunds(startDateRange, endDateRange, true);
		//    GenerateBankExportsForNonTicketFunds(startDateRange, endDateRange, false);            
		//}

		//private static void GenerateBankExportsForNonTicketFunds(DateTime startDateRange, DateTime endDateRange, bool positiveCashFlow)
		//{
		//    startDateRange = Utilities.GetStartOfDay(startDateRange);
		//    endDateRange = Utilities.GetStartOfDay(endDateRange);

		//    Query nonTicketCardMoneyQuery = new Query(new And(new Q(Transfer.Columns.Method, Transfer.Methods.Card),
		//                                                      new Q(Transfer.Columns.Status, Transfer.StatusEnum.Success),
		//                                                      new Q(Transfer.Columns.DateTimeComplete, QueryOperator.GreaterThanOrEqualTo, startDateRange),
		//                                                      new Q(Transfer.Columns.DateTimeComplete, QueryOperator.LessThan, endDateRange)));
            
		//    nonTicketCardMoneyQuery.ExtraSelectElements.Add("SumAmounts", "SUM([Transfer].[Amount])");
		//    nonTicketCardMoneyQuery.ExtraSelectElements.Add("TheDate", "dateadd(day, 0, datediff(day, 0, [Transfer].[DateTimeComplete]))");
		//    nonTicketCardMoneyQuery.OrderBy = new OrderBy("dateadd(day, 0, datediff(day, 0, [Transfer].[DateTimeComplete]))");
		//    nonTicketCardMoneyQuery.GroupBy = new GroupBy("dateadd(day, 0, datediff(day, 0, [Transfer].[DateTimeComplete]))");
		//    nonTicketCardMoneyQuery.Columns = new ColumnSet();


		//    Query ticketCardMoneyQuery = new Query(new And(new Q(InvoiceItem.Columns.Type, InvoiceItem.Types.EventTickets),
		//                                                   new Q(InvoiceItem.Columns.RevenueStartDate, QueryOperator.GreaterThanOrEqualTo, startDateRange),
		//                                                   new Q(InvoiceItem.Columns.RevenueStartDate, QueryOperator.LessThan, endDateRange)));
		//    ticketCardMoneyQuery.ExtraSelectElements.Add("SumAmounts", "SUM([InvoiceItem].[Total])");
		//    ticketCardMoneyQuery.ExtraSelectElements.Add("TheDate", "dateadd(day, 0, datediff(day, 0, [InvoiceItem].[RevenueStartDate]))");
		//    ticketCardMoneyQuery.OrderBy = new OrderBy("dateadd(day, 0, datediff(day, 0, [InvoiceItem].[RevenueStartDate]))");
		//    ticketCardMoneyQuery.GroupBy = new GroupBy("dateadd(day, 0, datediff(day, 0, [InvoiceItem].[RevenueStartDate]))");
		//    ticketCardMoneyQuery.Columns = new ColumnSet();

		//    if(positiveCashFlow)
		//    {
		//        nonTicketCardMoneyQuery.QueryCondition = new And(nonTicketCardMoneyQuery.QueryCondition,
		//                                                         new Q(Transfer.Columns.Amount, QueryOperator.GreaterThan, 0));
		//        ticketCardMoneyQuery.QueryCondition = new And(ticketCardMoneyQuery.QueryCondition,
		//                                                      new Q(InvoiceItem.Columns.Total, QueryOperator.GreaterThan, 0));
		//    }
		//    else
		//    {
		//        nonTicketCardMoneyQuery.QueryCondition = new And(nonTicketCardMoneyQuery.QueryCondition,
		//                                                         new Q(Transfer.Columns.Amount, QueryOperator.LessThan, 0));
		//        ticketCardMoneyQuery.QueryCondition = new And(ticketCardMoneyQuery.QueryCondition,
		//                                                      new Q(InvoiceItem.Columns.Total, QueryOperator.LessThan, 0));
		//    }

		//    TransferSet nonTicketCardMoneyTransfers = new TransferSet(nonTicketCardMoneyQuery);
		//    InvoiceItemSet ticketCardMoneyItems = new InvoiceItemSet(ticketCardMoneyQuery);

		//    Hashtable hash = new Hashtable();
		//    foreach (Transfer t in nonTicketCardMoneyTransfers)
		//    {
		//        hash.Add(Convert.ToDateTime(t.ExtraSelectElements["TheDate"]).ToString("dd-MM-yyyy"), t.ExtraSelectElements["SumAmounts"]);
		//    }

		//    foreach (InvoiceItem ii in ticketCardMoneyItems)
		//    {
		//        if (hash.ContainsKey(Convert.ToDateTime(ii.ExtraSelectElements["TheDate"]).ToString("dd-MM-yyyy")))
		//            hash[Convert.ToDateTime(ii.ExtraSelectElements["TheDate"]).ToString("dd-MM-yyyy")] = ((double)(Convert.ToDouble(hash[Convert.ToDateTime(ii.ExtraSelectElements["TheDate"]).ToString("dd-MM-yyyy")]) - Convert.ToDouble(ii.ExtraSelectElements["SumAmounts"]))).ToString();
		//        else
		//            hash.Add(Convert.ToDateTime(ii.ExtraSelectElements["TheDate"]).ToString("dd-MM-yyyy"), ((double)(-1 * Convert.ToDouble(ii.ExtraSelectElements["SumAmounts"]))).ToString());
		//    }

		//    while (startDateRange < endDateRange)
		//    {
		//        if (hash.ContainsKey(startDateRange.ToString("dd-MM-yyyy")))
		//        {
		//            BankExport be = new BankExport();
		//            be.Amount = Math.Round(Convert.ToDecimal(hash[startDateRange.ToString("dd-MM-yyyy")]), 2);
		//            if (be.Amount != 0)
		//            {
		//                be.ReferenceDateTime = startDateRange;
		//                StringBuilder sb = new StringBuilder();
		//                sb.Append("Client account to current account: ");
		//                sb.Append(be.Amount.ToString("c"));
		//                sb.Append(" from ");
		//                sb.Append(be.ReferenceDateTime.ToString("ddd dd/MM/yyyy"));
		//                be.Details = sb.ToString();

		//                be.AddedDateTime = DateTime.Now;
		//                if (be.Amount > 0)
		//                {
		//                    be.BankAccountNumber = Vars.DSI_BANK_ACCOUNT_NUMBER;
		//                    be.BankAccountSortCode = Vars.DSI_BANK_SORT_CODE;
		//                    be.BankName = Vars.DSI_BANK_NAME;
		//                }
		//                else
		//                {
		//                    be.BankAccountNumber = Vars.DSI_CLIENT_BANK_ACCOUNT_NUMBER;
		//                    be.BankAccountSortCode = Vars.DSI_CLIENT_BANK_SORT_CODE;
		//                    be.BankName = Vars.DSI_CLIENT_BANK_NAME;
		//                }
		//                be.Amount = Math.Abs(be.Amount);

		//                be.Status = Statuses.Added;

		//                be.Type = Types.InternalTransferNonTicketFunds;

		//                be.Update();

		//                be.Validate();
		//            }
		//        }
		//        startDateRange = startDateRange.AddDays(1);
		//    }
		//}
		

		//private static void GenerateBankExportForBookingFees(InvoiceItem invoiceItem)
		//{
		//    BankExport be = new BankExport();

		//    be.Amount = Math.Round(Convert.ToDecimal(invoiceItem.ExtraSelectElements["SumBookingFeeAmounts"]), 2);
		//    if (be.Amount != 0)
		//    {
		//        be.ReferenceDateTime = (DateTime)invoiceItem.ExtraSelectElements["TheDate"];
		//        StringBuilder sb = new StringBuilder();
		//        sb.Append("Ticket booking fees of ");
		//        sb.Append(be.Amount.ToString("c"));
		//        sb.Append(" from ");
		//        sb.Append(be.ReferenceDateTime.ToString("ddd dd/MM/yyyy"));
		//        be.Details = sb.ToString();

		//        be.AddedDateTime = DateTime.Now;
		//        if (be.Amount > 0)
		//        {
		//            be.BankAccountNumber = Vars.DSI_BANK_ACCOUNT_NUMBER;
		//            be.BankAccountSortCode = Vars.DSI_BANK_SORT_CODE;
		//            be.BankName = Vars.DSI_BANK_NAME;
		//        }
		//        else
		//        {
		//            be.BankAccountNumber = Vars.DSI_CLIENT_BANK_ACCOUNT_NUMBER;
		//            be.BankAccountSortCode = Vars.DSI_CLIENT_BANK_SORT_CODE;
		//            be.BankName = Vars.DSI_CLIENT_BANK_NAME;
		//        }
		//        be.Amount = Math.Abs(be.Amount);

		//        be.Status = Statuses.Added;
 
		//        be.Type = Types.InternalTransferNonTicketFunds;

		//        be.Update();

		//        be.Validate();
		//    }
		//}
		#endregion

		public static void GenerateBankExportForRefundTransfer(Transfer refundTransfer)
        {
            BankExport be = new BankExport();
            be.AddedDateTime = DateTime.Now;
            be.Amount = Math.Abs(refundTransfer.Amount);
            be.BankName = Cambro.Misc.Utility.Snip(refundTransfer.BankName, 35);
            be.BankAccountNumber = refundTransfer.BankAccountNumber;
            be.BankAccountSortCode = refundTransfer.BankSortCode;
            be.PromoterK = refundTransfer.PromoterK;
            be.Status = BankExport.Statuses.Added;

			if (refundTransfer.TransferRefunded != null && refundTransfer.TransferRefunded.Method == Transfer.Methods.TicketSales)
            {
				//if (refundTransfer.BankName.ToUpper().Contains(Vars.DSI_BANK_NAME.ToUpper()))
				//    be.Type = BankExport.Types.InternalTransferTicketFundsToPromoter;
				//else
                    be.Type = BankExport.Types.ExternalBACSTicketFundsToPromoter;
            }
            else
            {
				//if (refundTransfer.BankName.ToUpper().Contains(Vars.DSI_BANK_NAME.ToUpper()))
				//    be.Type = BankExport.Types.InternalTransferRefundToPromoter;
				//else
                    be.Type = Types.ExternalBACSRefundToPromoter;
            }
            be.TransferK = refundTransfer.K;

            StringBuilder sb = new StringBuilder();
            sb.Append("Promoter ");
            sb.Append(refundTransfer.Promoter.Name);
            sb.Append(" requested a refund of ");
            sb.Append(refundTransfer.Amount.ToString("c"));
            sb.Append(".");
            sb.Append(refundTransfer.Promoter.AsHTML());
            sb.Append(refundTransfer.AsHTML());
            be.Details = sb.ToString();

            be.Update();

            be.Validate();
            refundTransfer.BankTransferReference = be.PaymentRef;
            refundTransfer.Update();
        }

		public static void GenerateBankExportForTicketFundsUsed(Transfer ticketFundsTransfer, decimal amount, Invoice invoiceAppliedTo)
        {            
			//BankExport be = new BankExport();
            
			//be.Amount = Math.Round(amount, 2);
			//if (be.Amount != 0)
			//{
			//    be.AddedDateTime = DateTime.Now;
                
			//    be.BankAccountNumber = Vars.DSI_BANK_ACCOUNT_NUMBER;
			//    be.BankAccountSortCode = Vars.DSI_BANK_SORT_CODE;
			//    be.BankName = Vars.DSI_BANK_NAME;
                
			//    be.Amount = Math.Abs(be.Amount);

			//    be.PromoterK = ticketFundsTransfer.PromoterK;
			//    be.TransferK = ticketFundsTransfer.K;

			//    be.Status = Statuses.Added;
			//    be.Type = Types.InternalTransferTicketFundsUsed;

			//    StringBuilder sb = new StringBuilder();
			//    sb.Append("Promoter ");
			//    sb.Append(ticketFundsTransfer.Promoter.Name);
			//    sb.Append(" used ticket funds transfer #");
			//    sb.Append(ticketFundsTransfer.K.ToString());
			//    sb.Append(" and applied ");
			//    sb.Append(amount.ToString("c"));
			//    sb.Append(" to invoice #");
			//    sb.Append(invoiceAppliedTo.K.ToString());
			//    //sb.Append(ticketFundsTransfer.AmountRemaining().ToString("c"));
			//    //sb.Append(" remaining.<br>Invoice is ");
			//    //if(!invoiceAppliedTo.Paid)
			//    //  sb.Append("not");
			//    //sb.Append(" fully paid.<br><br>");
			//    sb.Append(ticketFundsTransfer.Promoter.AsHTML());
			//    if (ticketFundsTransfer.TicketPromoterEvent != null && ticketFundsTransfer.TicketPromoterEvent.Event != null)
			//    {
			//        sb.Append(ticketFundsTransfer.TicketPromoterEvent.Event.AsHTML());
			//    }
			//    sb.Append(ticketFundsTransfer.AsHTML());
			//    be.Details = sb.ToString();

			//    be.Update();

			//    be.Validate();
			//    ticketFundsTransfer.BankTransferReference = be.PaymentRef;
			//    ticketFundsTransfer.Update();
			//}
        }

		public static string BarclaysExport(BankExportSet bankExports)
		{
			StringBuilder sb = new StringBuilder();

			if (bankExports.Count > 0)
			{
				string batchRef = GetNextBatchRef();

				foreach (BankExport be in bankExports)
				{
					if (be.Type == Types.ExternalBACSTicketFundsToPromoter || be.Type == Types.ExternalBACSRefundToPromoter)
					{

						be.Validate();
						be.BatchRef = batchRef;
						be.OutputDateTime = DateTime.Now;
						be.UpdateStatus(Statuses.AwaitingConfirmation);


						sb.Append("\"" + be.BankAccountSortCode + "\",");
						sb.Append("\"" + be.BeneficiaryName.StripAllNonAlphaNumeric().Truncate(18) + "\",");
						sb.Append("\"" + be.BankAccountNumber + "\",");
						sb.Append("\"" + be.Amount.ToString("0.00") + "\",");
						sb.Append("\"" + be.PaymentRef + "\",");
						sb.Append("\"99\"\r\n");
					}
					else
						throw new Exception("Invalid BankExport type!!!");
				}
			}

			return sb.ToString();
		}

		#region Lloyds XML stuff - removed
		//public static string LloydsLinkExportToXML(BankExportSet bankExports)
		//{
		//    StringBuilder sb = new StringBuilder();

		//    if (bankExports.Count > 0)
		//    {
		//        List<BankExport> internalClientToCurrentTransfers = new List<BankExport>();
		//        List<BankExport> internalCurrentToClientTransfers = new List<BankExport>();
		//        List<BankExport> internalTransferFromClientAccount = new List<BankExport>();
		//        List<BankExport> internalTransferFromCurrentAccount = new List<BankExport>();
		//        List<BankExport> externalBACSFromClientAccount = new List<BankExport>();
		//        List<BankExport> externalBACSFromCurrentAccount = new List<BankExport>();

		//        // BatchRef Max length = 16 for internal, and 18 for BACS    
		//        string batchRef = GetNextBatchRef();
		//        string internalClientToCurrentTransfersBatchRef = batchRef + "-" + CLIENT_ACCOUNT_TO_CURRENT_ACCOUNT_ABBREVIATION;
		//        string internalCurrentToClientTransfersBatchRef = batchRef + "-" + CURRENT_ACCOUNT_TO_CLIENT_ACCOUNT_ABBREVIATION;
		//        string internalTransferFromClientAccountBatchRef = batchRef + "-" + CLIENT_ACCOUNT_TO_INTERNAL_BACS_ABBREVIATION;
		//        string internalTransferFromCurrentAccountBatchRef = batchRef + "-" + CURRENT_ACCOUNT_TO_INTERNAL_BACS_ABBREVIATION;
		//        string externalBACSFromClientAccountBatchRef = batchRef + "-" + CLIENT_ACCOUNT_TO_BACS_ABBREVIATION;
		//        string externalBACSFromCurrentAccountBatchRef = batchRef + "-" + CURRENT_ACCOUNT_TO_BACS_ABBREVIATION;

		//        foreach (BankExport be in bankExports)
		//        {
		//            if (be.Type == Types.ExternalBACSTicketFundsToPromoter)
		//                externalBACSFromClientAccount.Add(be);
		//            else if (be.Type == Types.ExternalBACSRefundToPromoter)
		//                externalBACSFromCurrentAccount.Add(be);
		//            else if(be.Type == Types.InternalTransferRefundToPromoter)
		//                internalTransferFromCurrentAccount.Add(be);
		//            else if(be.Type == Types.InternalTransferTicketFundsToPromoter)
		//                internalTransferFromClientAccount.Add(be);
		//            else
		//            {
		//                //if (be.BankAccountSortCode == Vars.DSI_BANK_SORT_CODE && be.BankAccountNumber == Vars.DSI_BANK_ACCOUNT_NUMBER) 
		//                //    internalClientToCurrentTransfers.Add(be);
		//                //else if (be.BankAccountSortCode == Vars.DSI_CLIENT_BANK_SORT_CODE && be.BankAccountNumber == Vars.DSI_CLIENT_BANK_ACCOUNT_NUMBER) 
		//                //    internalCurrentToClientTransfers.Add(be);
		//            }
		//        }

		//        sb.Append("<PaymentList>");
		//        foreach (BankExport internalClientToCurrent in internalClientToCurrentTransfers)
		//        {
		//            sb.Append(LloydsLinkPaymentToXML(internalClientToCurrent, "Internal_Transfer_GBP_Payment", internalClientToCurrentTransfersBatchRef, Vars.DSI_CLIENT_BANK_SORT_CODE, Vars.DSI_CLIENT_BANK_ACCOUNT_NUMBER, 0));
		//        }
		//        foreach (BankExport internalCurrentToClient in internalCurrentToClientTransfers)
		//        {
		//            sb.Append(LloydsLinkPaymentToXML(internalCurrentToClient, "Internal_Transfer_GBP_Payment", internalCurrentToClientTransfersBatchRef, Vars.DSI_BANK_SORT_CODE, Vars.DSI_BANK_ACCOUNT_NUMBER, 0));
		//        }
		//        foreach (BankExport internalTransferFromCurrent in internalTransferFromCurrentAccount)
		//        {
		//            sb.Append(LloydsLinkPaymentToXML(internalTransferFromCurrent, "Internal_Transfer_GBP_Payment", internalTransferFromCurrentAccountBatchRef, Vars.DSI_BANK_SORT_CODE, Vars.DSI_BANK_ACCOUNT_NUMBER, 0));
		//        }
		//        foreach (BankExport internalTransferFromClient in internalTransferFromClientAccount)
		//        {
		//            sb.Append(LloydsLinkPaymentToXML(internalTransferFromClient, "Internal_Transfer_GBP_Payment", internalTransferFromClientAccountBatchRef, Vars.DSI_CLIENT_BANK_SORT_CODE, Vars.DSI_CLIENT_BANK_ACCOUNT_NUMBER, 0));
		//        }
		//        sb.Append(LloydsLinkPaymentToXML(externalBACSFromClientAccount, "BACS_Payment", externalBACSFromClientAccountBatchRef, Vars.DSI_CLIENT_BANK_SORT_CODE, Vars.DSI_CLIENT_BANK_ACCOUNT_NUMBER, 3));
		//        sb.Append(LloydsLinkPaymentToXML(externalBACSFromCurrentAccount, "BACS_Payment", externalBACSFromCurrentAccountBatchRef, Vars.DSI_BANK_SORT_CODE, Vars.DSI_BANK_ACCOUNT_NUMBER, 3));
		//        sb.Append("</PaymentList>");
		//    }

		//    return sb.ToString();            
		//}
		//private static string LloydsLinkPaymentToXML(BankExport bankExport, string paymentType, string batchRef, string debitSortCode, string debitAccountNbr, int addBusinessDays)
		//{
		//    List<BankExport> bankExports = new List<BankExport>();
		//    bankExports.Add(bankExport);

		//    return LloydsLinkPaymentToXML(bankExports, paymentType, batchRef, debitSortCode, debitAccountNbr, addBusinessDays);
		//}
		//private static string LloydsLinkPaymentToXML(List<BankExport> bankExports, string paymentType, string batchRef, string debitSortCode, string debitAccountNbr, int addBusinessDays)
		//{
		//    StringBuilder sb = new StringBuilder();

		//    if (bankExports.Count > 0)
		//    {
		//        sb.Append("<Payment>");
		//        decimal paymentAmount = 0;
		//        DateTime processingDate = DateTime.Now > DateTime.Today.AddHours(16).AddMinutes(59) ? Utilities.AddBusinessDays(DateTime.Now, addBusinessDays + 1) : Utilities.AddBusinessDays(DateTime.Now, addBusinessDays);
		//        foreach (BankExport be in bankExports)
		//        {
		//            paymentAmount += be.Amount;
		//        }

		//        sb.Append("<PaymentTypeField>" + paymentType + "</PaymentTypeField>");
		//        sb.Append("<DebitSortCodeField>" + debitSortCode + "</DebitSortCodeField>");
		//        sb.Append("<DebitAccountNumberField>" + debitAccountNbr + "</DebitAccountNumberField>");
                
		//        if (paymentType == "Internal_Transfer_GBP_Payment")
		//            sb.Append("<DebitAccountCurrencyField>GBP</DebitAccountCurrencyField>");

		//        sb.Append("<PaymentCurrencyField>GBP</PaymentCurrencyField>");
		//        sb.Append("<PaymentAmountField>" + Math.Round(paymentAmount, 2).ToString("c").Replace("£", "") + "</PaymentAmountField>");
		//        sb.Append("<PaymentReferenceNumberField>" + batchRef + "</PaymentReferenceNumberField>");
		//        sb.Append("<ValueDateField>" + processingDate.ToString("dd-MMM-yyyy") + "</ValueDateField>");

		//        sb.Append("<BeneficiaryList>");

		//        foreach (BankExport be in bankExports)
		//        {
		//            be.Validate();
		//            be.BatchRef = batchRef;
		//            be.OutputDateTime = DateTime.Now;
		//            be.UpdateStatus(Statuses.AwaitingConfirmation);
		//            sb.Append(be.LloydsLinkBeneficiaryToXML());
		//        }

		//        sb.Append("</BeneficiaryList></Payment>").Replace("&", "");
		//    }

		//    return sb.ToString();
		//}
		#endregion

		public void Validate()
        {
            try
            {
                if (this.Type == Types.InternalTransferNonTicketFunds)
                    this.ValidateNonTicketFunds();
                else
                    this.ValidateTransfer();
            }
            catch (Exception ex)
            {
                Utilities.AdminEmailAlert("Could not validate bank export. BankExport has been cancelled." + this.AsHTML(), "Could not validate bank export.", ex, this);
                this.UpdateStatus(Statuses.Cancelled);
            }
        }

        private void ValidateTransfer()
        {
            if (this.Transfer != null)
            {
                if (this.Type == Types.ExternalBACSRefundToPromoter || this.Type == Types.ExternalBACSTicketFundsToPromoter || this.Type == Types.InternalTransferTicketFundsToPromoter)
                {
                    if (this.Transfer.Type != Transfer.TransferTypes.Refund)
                        throw new DsiUserFriendlyException("Validation error on " + this.PaymentRef + ". " + this.Transfer.TypeAndK + " is not a refund.");
                    if (this.Transfer.Method != Transfer.Methods.BankTransfer)
                        throw new DsiUserFriendlyException("Validation error on " + this.PaymentRef + ". " + this.Transfer.TypeAndK + " is not a bank transfer.");
                }
                if (this.Type == Types.ExternalBACSTicketFundsToPromoter || this.Type == Types.InternalTransferTicketFundsToPromoter)
                {
                    if (this.Transfer.TransferRefunded == null || this.Transfer.TransferRefunded.Method != Transfer.Methods.TicketSales)
                        throw new DsiUserFriendlyException("Validation error on " + this.PaymentRef + ". " + this.Transfer.TypeAndK + " is not paying out for ticket funds.");
                }

                // Sum all bank exports for this transfer and ensure that it doesnt exceed the amount of the transfer.
                Query bookingFeeBankExportQuery = new Query(new And(new Q(BankExport.Columns.TransferK, this.Transfer.K),
                                                                    new Q(BankExport.Columns.Status, QueryOperator.NotEqualTo, BankExport.Statuses.Cancelled)));
                BankExportSet bankExports = new BankExportSet(bookingFeeBankExportQuery);
                decimal amount = 0;
                if (this.Type == Types.InternalTransferTicketFundsUsed)
                {
                    foreach (BankExport be in bankExports)
                    {
                        if (be.BankAccountNumber == Vars.DSI_BANK_ACCOUNT_NUMBER && be.BankAccountSortCode == Vars.DSI_BANK_SORT_CODE)
                            amount += be.Amount;
                        else
                            amount -= be.Amount;
                    }
                    amount = Math.Round(amount, 2);
                    if (amount < 0 || amount > this.Transfer.Amount)
                        throw new DsiUserFriendlyException("Validation error on " + this.PaymentRef + ". BankExport summation does not match amount for " + this.Transfer.TypeAndK);
                }
                else
                {
                    foreach (BankExport be in bankExports)
                    {
                        amount -= be.Amount;
                    }
                    amount = Math.Round(amount, 2);
                    if (amount > 0 || amount < this.Transfer.Amount)
                        throw new DsiUserFriendlyException("Validation error on " + this.PaymentRef + ". BankExport summation does not match amount for " + this.Transfer.TypeAndK);
                }
            }
            else
            {
                throw new DsiUserFriendlyException("Validation error on " + this.PaymentRef + ". Cannot validate a transfer that is null.");
            }
        }
        
        private void ValidateNonTicketFunds()
        {
            // Validate the summation is correct
            Query nonTicketCardMoneyQuery = new Query(new And(new Q(Transfer.Columns.Method, Transfer.Methods.Card),
                                                              new Q(Transfer.Columns.Status, Transfer.StatusEnum.Success),
                                                              new Q(Transfer.Columns.DateTimeComplete, QueryOperator.GreaterThanOrEqualTo, Utilities.GetStartOfDay(this.ReferenceDateTime)),
                                                              new Q(Transfer.Columns.DateTimeComplete, QueryOperator.LessThan, Utilities.GetStartOfDay(this.ReferenceDateTime).AddDays(1))));

            nonTicketCardMoneyQuery.ExtraSelectElements.Add("SumAmounts", "SUM([Transfer].[Amount])");
            nonTicketCardMoneyQuery.ExtraSelectElements.Add("TheDate", "dateadd(day, 0, datediff(day, 0, [Transfer].[DateTimeComplete]))");
            nonTicketCardMoneyQuery.GroupBy = new GroupBy("dateadd(day, 0, datediff(day, 0, [Transfer].[DateTimeComplete]))");
            nonTicketCardMoneyQuery.Columns = new ColumnSet();


            Query ticketCardMoneyQuery = new Query(new And(new Q(InvoiceItem.Columns.Type, InvoiceItem.Types.EventTickets),
                                                           new Q(InvoiceItem.Columns.RevenueStartDate, QueryOperator.GreaterThanOrEqualTo, Utilities.GetStartOfDay(this.ReferenceDateTime)),
                                                           new Q(InvoiceItem.Columns.RevenueStartDate, QueryOperator.LessThan, Utilities.GetStartOfDay(this.ReferenceDateTime).AddDays(1))));
            ticketCardMoneyQuery.ExtraSelectElements.Add("SumAmounts", "SUM([InvoiceItem].[Total])");
            ticketCardMoneyQuery.ExtraSelectElements.Add("TheDate", "dateadd(day, 0, datediff(day, 0, [InvoiceItem].[RevenueStartDate]))");
            ticketCardMoneyQuery.GroupBy = new GroupBy("dateadd(day, 0, datediff(day, 0, [InvoiceItem].[RevenueStartDate]))");
            ticketCardMoneyQuery.Columns = new ColumnSet();

            int signMultiplier = 1;
            if (this.BankAccountNumber == Vars.DSI_BANK_ACCOUNT_NUMBER && this.BankAccountSortCode == Vars.DSI_BANK_SORT_CODE)
            {
                nonTicketCardMoneyQuery.QueryCondition = new And(nonTicketCardMoneyQuery.QueryCondition,
                                                                 new Q(Transfer.Columns.Amount, QueryOperator.GreaterThan, 0));
                ticketCardMoneyQuery.QueryCondition = new And(ticketCardMoneyQuery.QueryCondition,
                                                              new Q(InvoiceItem.Columns.Total, QueryOperator.GreaterThan, 0));
            }
            else
            {
                signMultiplier = -1;

                nonTicketCardMoneyQuery.QueryCondition = new And(nonTicketCardMoneyQuery.QueryCondition,
                                                                 new Q(Transfer.Columns.Amount, QueryOperator.LessThan, 0));
                ticketCardMoneyQuery.QueryCondition = new And(ticketCardMoneyQuery.QueryCondition,
                                                              new Q(InvoiceItem.Columns.Total, QueryOperator.LessThan, 0));
            }

            TransferSet nonTicketCardMoneyTransfers = new TransferSet(nonTicketCardMoneyQuery);
            InvoiceItemSet ticketCardMoneyItems = new InvoiceItemSet(ticketCardMoneyQuery);

			decimal sumAmounts = 0;
            if (nonTicketCardMoneyTransfers.Count > 0 && nonTicketCardMoneyTransfers[0].ExtraSelectElements["SumAmounts"] != DBNull.Value)
                sumAmounts = Convert.ToDecimal(nonTicketCardMoneyTransfers[0].ExtraSelectElements["SumAmounts"]);
            if (ticketCardMoneyItems.Count > 0 && ticketCardMoneyItems[0].ExtraSelectElements["SumAmounts"] != DBNull.Value)
				sumAmounts -= Convert.ToDecimal(ticketCardMoneyItems[0].ExtraSelectElements["SumAmounts"]);

            if (Math.Round(sumAmounts, 2) != Math.Round(signMultiplier * this.Amount, 2))
                throw new DsiUserFriendlyException("Validation error on " + this.PaymentRef + ". Non ticket funds do not add up.");


            // Validate that no other Bank Export covers non ticket funds for that day
            Query nonTicketFundsBankExportQuery = new Query(new And(new Q(BankExport.Columns.Type, BankExport.Types.InternalTransferNonTicketFunds),
                                                                    new Q(BankExport.Columns.BankAccountNumber, this.BankAccountNumber),
                                                                    new Q(BankExport.Columns.BankAccountSortCode, this.BankAccountSortCode),
                                                                    new Q(BankExport.Columns.ReferenceDateTime, QueryOperator.GreaterThanOrEqualTo, Utilities.GetStartOfDay(this.ReferenceDateTime)),
                                                                    new Q(BankExport.Columns.ReferenceDateTime, QueryOperator.LessThan, Utilities.GetStartOfDay(this.ReferenceDateTime).AddDays(1))));
            BankExportSet bankExports = new BankExportSet(nonTicketFundsBankExportQuery);
            if (!((this.K == 0 && bankExports.Count == 0) || (this.K > 0 && bankExports.Count == 1)))
                throw new DsiUserFriendlyException("Validation error on " + this.PaymentRef + ". Conflict with bank export records for non ticket funds for " + this.ReferenceDateTime.ToString("ddd dd/MM/yyyy"));
        }
        #endregion
    }
    #endregion
}
