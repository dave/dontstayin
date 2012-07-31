using System;
using System.Collections.Generic;
using System.Text;
using Bobs.DataHolders;

namespace Bobs
{
    #region InvoiceTransfer
    /// <summary>
    /// Invoice to Transfer relational table
    /// </summary>
    [Serializable] 
	public partial class InvoiceTransfer 
    {

        #region Simple members
        /// <summary>
        /// Link to the Invoice table
        /// </summary>
        public override int InvoiceK
        {
            get { return (int)this[InvoiceTransfer.Columns.InvoiceK]; }
            set { this[InvoiceTransfer.Columns.InvoiceK] = value; }
        }
        /// <summary>
        /// Link to the Transfer table
        /// </summary>
        public override int TransferK
        {
            get { return (int)this[InvoiceTransfer.Columns.TransferK]; }
            set { this[InvoiceTransfer.Columns.TransferK] = value; }
        }
        /// <summary>
        /// +ve for DSI receiving money, -ve for DSI paying out money
        /// </summary>
		public override decimal Amount
        {
			get { return (decimal)this[InvoiceTransfer.Columns.Amount]; }
            set { this[InvoiceTransfer.Columns.Amount] = value; }
        }
        #endregion

    }
    #endregion


    #region InvoiceTransferDataHolder
	/// <summary>
	/// InvoiceTransferDataHolder is a stub for InvoiceTransfer that can be stored in the page ViewState
	/// </summary>
    [Serializable]
    public class InvoiceTransferDataHolder : BobDataHolder
    {
        #region Variables
        private int transferK = 0;
        private int invoiceK = 0;
        private Transfer.TransferTypes type = Transfer.TransferTypes.Payment;
        private Transfer.Methods method = Transfer.Methods.Card;
        private Transfer.StatusEnum status = Transfer.StatusEnum.Pending;
        private int usrK = 0;
		private decimal amount = 0.0m;
        #endregion

        #region Properties
        /// <summary>
        /// The primary key
        /// </summary>
        public int TransferK
        {
            get { return this.transferK; }
            set
            {
                this.State = DataHolderState.Modified;
                this.transferK = value;
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
        /// The type of the TransferTypes
        /// </summary>
        public Transfer.TransferTypes Type
        {
            get { return this.type; }
            set
            {
                this.State = DataHolderState.Modified;
                this.type = value;
            }
        }
        /// <summary>
        /// The type of the Methods
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
        /// The type of the Statuses
        /// </summary>
        public Transfer.StatusEnum Status
        {
            get
            {   return this.status; }
            set
            {
                this.State = DataHolderState.Modified;
                this.status = value;
            }
        }
        /// <summary>
        /// The UsrK
        /// </summary>
        public int UsrK
        {
            get { return this.usrK; }
            set
            {
                this.State = DataHolderState.Modified;
                this.usrK = value;
            }
        }
        /// <summary>
        /// The UsrName
        /// </summary>
        public string UsrName
        {
            get
            {
                Usr usr = new Usr();
                try
                {
                    usr = new Usr(this.usrK);
                }
                catch(Exception)
                {
                    usr = new Usr();
                }
                return usr.Name;
            }
        }
        /// <summary>
        /// Amount applied from the Transfer to this Invoice
        /// </summary>
		public decimal Amount
        {
            get { return this.amount; }
            set
            {
                this.State = DataHolderState.Modified;
                this.amount = value; }
        }

		public string LinkAdminTransfer
		{
			get
			{
				return Utilities.Link(UrlInfo.PageUrl(UrlInfo.PageTypes.Admin, "transferscreen", new string[] { "K", this.TransferK.ToString() }), Utilities.CamelCaseToString(this.Type.ToString()) + " #" + this.TransferK.ToString());
			}
		}
        #endregion

        #region Constructors
        public InvoiceTransferDataHolder()
        {
        }

        public InvoiceTransferDataHolder(InvoiceTransfer invoiceTransfer)
        {
            this.invoiceK = invoiceTransfer.InvoiceK;
            this.transferK = invoiceTransfer.TransferK;
            this.amount = invoiceTransfer.Amount;
            Transfer transfer = new Transfer(invoiceTransfer.TransferK);
            this.type = transfer.Type;
            this.method = transfer.Method;
            this.status = transfer.Status;
            this.usrK = transfer.UsrK;
        }
        #endregion

        #region Export To InvoiceTransfer
        public InvoiceTransfer ExportToInvoiceTransfer()
        {
            InvoiceTransfer invoiceTransfer = new InvoiceTransfer();
            
			try
			{
				invoiceTransfer = new InvoiceTransfer(this.invoiceK, this.transferK);
			}
			catch (Exception)
			{
				// if it doesnt already exist in the database, then it is new.
				this.State = DataHolderState.Added;
				invoiceTransfer.InvoiceK = this.invoiceK;
				invoiceTransfer.TransferK = this.transferK;
			}                
            invoiceTransfer.Amount = this.amount;

            return invoiceTransfer;
        }
        #endregion

        #region UpdateInsertDelete
        public InvoiceTransfer UpdateInsertDelete()
        {
            InvoiceTransfer invoiceTransfer = ExportToInvoiceTransfer();
			if (invoiceTransfer.InvoiceK <= 0)
			{
				throw new Exception("Invalid InvoiceK #" + invoiceTransfer.InvoiceK.ToString());
			}
			else if(invoiceTransfer.TransferK <= 0)
			{
				throw new Exception("Invalid TransferK #" + invoiceTransfer.TransferK.ToString());
			}
			else
			{
				if (this.State == DataHolderState.Deleted)
					invoiceTransfer.Delete();
				else
					invoiceTransfer.Update();
			}
			
            return invoiceTransfer;
        }
        #endregion
    }


    #endregion
}
