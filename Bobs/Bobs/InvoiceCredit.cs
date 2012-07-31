using System;
using System.Collections.Generic;
using System.Text;
using Bobs.DataHolders;

namespace Bobs
{
    #region InvoiceCredit
    /// <summary>
    /// Invoice to Credit relational table
    /// </summary>
    [Serializable] 
	public partial class InvoiceCredit 
    {

        #region Simple members
        /// <summary>
        /// Link to the Invoice table
        /// </summary>
        public override int InvoiceK
        {
            get { return (int)this[InvoiceCredit.Columns.InvoiceK]; }
            set { this[InvoiceCredit.Columns.InvoiceK] = value; }
        }
        /// <summary>
        /// Link to the Invoice table, referring to a credit
        /// </summary>
        public override int CreditInvoiceK
        {
            get { return (int)this[InvoiceCredit.Columns.CreditInvoiceK]; }
            set { this[InvoiceCredit.Columns.CreditInvoiceK] = value; }
        }
        /// <summary>
        /// +ve for DSI receiving money, -ve for DSI paying out money
        /// </summary>
		public override decimal Amount
        {
			get { return (decimal)this[InvoiceCredit.Columns.Amount]; }
            set { this[InvoiceCredit.Columns.Amount] = value; }
        }
        #endregion

        #region Invoice
        public Invoice Invoice
        {
            get
            {
                if (invoice == null && InvoiceK > 0)
                    invoice = new Invoice(InvoiceK);
                return invoice;
            }
            set
            {
                invoice = value;
            }
        }
        private Invoice invoice;
        #endregion

    }
    #endregion

    #region InvoiceCreditDataHolder
	/// <summary>
	/// InvoiceCreditDataHolder is a stub for InvoiceCredit that can be stored in the page ViewState
	/// </summary>
    [Serializable]
    public class InvoiceCreditDataHolder : BobDataHolder
    {
        #region Variables
        private int creditK = 0;
        private int invoiceK = 0;
		private decimal amount = 0.0m;
        private DateTime createdDateTime = DateTime.Now;
        #endregion

        #region Properties
        /// <summary>
        /// The primary key
        /// </summary>
        public int CreditK
        {
            get { return this.creditK; }
            set
            {
                this.State = DataHolderState.Modified;
                this.creditK = value;
            }
        }
        /// <summary>
        /// The invoice
        /// </summary>
        public int InvoiceK
        {
            get { return this.invoiceK; }
            set
            {
				this.State = DataHolderState.Modified;
                this.invoiceK = value;
            }
        }
        /// <summary>
        /// Credit applied to this Invoice
        /// </summary>
		public decimal Amount
        {
            get { return this.amount; }
            set
            {
				this.State = DataHolderState.Modified;
                this.amount = value;
            }
        }
        /// <summary>
        /// Date/time the invoice was created
        /// </summary>
        public DateTime CreatedDateTime
        {
            get { return this.createdDateTime; }
            set
            {
				this.State = DataHolderState.Modified;
                this.createdDateTime = value;
            }
        }

        #endregion

        #region Constructors
        public InvoiceCreditDataHolder()
        {
        }

        public InvoiceCreditDataHolder(InvoiceCredit invoiceCredit)
        {
            this.invoiceK = invoiceCredit.InvoiceK;
            this.creditK = invoiceCredit.CreditInvoiceK;
            this.amount = invoiceCredit.Amount;
            Invoice credit = new Invoice(invoiceCredit.CreditInvoiceK);
            this.createdDateTime = credit.CreatedDateTime;
        }
        #endregion

        #region Export To InvoiceCredit
        public InvoiceCredit ExportToInvoiceCredit()
        {
            InvoiceCredit invoiceCredit = new InvoiceCredit();
            invoiceCredit.CreditInvoiceK = this.creditK;
			try
			{
				invoiceCredit = new InvoiceCredit(this.invoiceK, this.creditK);
			}
			catch (Exception)
			{
				// if it doesnt already exist in the database, then it is new.
				this.State = DataHolderState.Added;
				invoiceCredit.InvoiceK = this.invoiceK;
				invoiceCredit.CreditInvoiceK = this.creditK;
			}         
            invoiceCredit.Amount = this.amount;

            return invoiceCredit;
        }
        #endregion

        #region UpdateInsertDelete
        public InvoiceCredit UpdateInsertDelete()
        {
            InvoiceCredit invoiceCredit = ExportToInvoiceCredit();
			if (invoiceCredit.InvoiceK <= 0)
			{
				throw new Exception("Invalid InvoiceK #" + invoiceCredit.InvoiceK.ToString());
			}
			else if (invoiceCredit.CreditInvoiceK <= 0)
			{
				throw new Exception("Invalid CreditInvoiceK #" + invoiceCredit.CreditInvoiceK.ToString());
			}
			else
			{
				if (this.State == DataHolderState.Deleted)
					invoiceCredit.Delete();
				else
					invoiceCredit.Update();
			}
            return invoiceCredit;
        }
        #endregion
    }


    #endregion
}
