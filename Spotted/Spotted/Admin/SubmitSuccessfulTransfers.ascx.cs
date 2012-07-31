using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Bobs;
using Local;
using Spotted;

namespace Spotted.Admin
{
    public partial class SubmitSuccessfulTransfers : AdminUserControl
    {
        private List<TransferDataHolder> transferDataHolderList = new List<TransferDataHolder>();
		private bool completed = false;
		private List<int> completedTransferKList = new List<int>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                TransfersBindData();
            }
        }

        #region Properties
        public List<TransferDataHolder> TransferDataHolderList
        {
            get
            {
                return transferDataHolderList;
            }
            set
            {
                transferDataHolderList = value;
            }
        }
		public bool Completed
		{
			get { return this.completed; }
			set { this.completed = value; }
		}
		public List<int> CompletedTransferKList
		{
			get { return this.completedTransferKList; }
			set { this.completedTransferKList = value; }
		}

        #endregion

		#region SaveViewState()
		protected override object SaveViewState()
		{
			this.ViewState["Completed"] = Completed;
			this.ViewState["TransferDataHolderList"] = TransferDataHolderList;
			this.ViewState["CompletedTransferKList"] = CompletedTransferKList;
			return base.SaveViewState();
		}
		#endregion
		#region LoadViewState()
		protected override void LoadViewState(object savedState)
		{
			base.LoadViewState(savedState);
			if (this.ViewState["Completed"] != null) Completed = (bool)this.ViewState["Completed"];
			if (this.ViewState["TransferDataHolderList"] != null) TransferDataHolderList = (List<TransferDataHolder>)ViewState["TransferDataHolderList"];
			if (this.ViewState["CompletedTransferKList"] != null) CompletedTransferKList = (List<int>)ViewState["CompletedTransferKList"];
		}
		#endregion

        private void TransfersBindData()
        {
            // Invoice Item GridView loading
            if (TransferDataHolderList.Count == 0)
                TransferDataHolderList.Add(null);
            SuccessfulTransferGridView.DataSource = TransferDataHolderList;
            SuccessfulTransferGridView.DataBind();
        }

        #region TrasnferGridView Event Handlers
        protected void SuccessfulTransferGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToUpper().Equals("ADD"))
            {
                // add new row from footer to db
                TransferDataHolder NewTransferDataHolder = new TransferDataHolder();

                GridViewRow row = SuccessfulTransferGridView.FooterRow;

                NewTransferDataHolder.Amount = Utilities.ConvertMoneyStringToDecimal(((TextBox)row.FindControl("NewAmountTextBox")).Text);
                NewTransferDataHolder.K = Convert.ToInt32(((TextBox)row.FindControl("NewTransferKTextBox")).Text);
				NewTransferDataHolder.ReferenceNumber = ((TextBox)row.FindControl("NewReferenceNumberTextBox")).Text;

                TransferDataHolderList.Add(NewTransferDataHolder);

                ViewState["TransferDataHolderList"] = TransferDataHolderList;

                TransfersBindData();
            }
        }

        protected void SuccessfulTransferGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            this.SuccessfulTransferGridView.EditIndex = -1;
            this.SuccessfulTransferGridView.ShowFooter = true;

            if (TransferDataHolderList.Count >= e.RowIndex)
            {
                // Delete row from List
                TransferDataHolderList.RemoveAt(e.RowIndex);
            }
            TransfersBindData();
        }

        protected void SuccessfulTransferGridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            this.SuccessfulTransferGridView.EditIndex = -1;
            this.SuccessfulTransferGridView.ShowFooter = true;

            GridViewRow row = SuccessfulTransferGridView.Rows[e.RowIndex];
            // Note: this works only when paging is turned off
            TransferDataHolder editTransferDataHolder = TransferDataHolderList[e.RowIndex];

            editTransferDataHolder.K = Convert.ToInt32(((TextBox)row.FindControl("EditTransferKTextBox")).Text);
            editTransferDataHolder.Amount = Utilities.ConvertMoneyStringToDecimal(((TextBox)row.FindControl("EditAmountTextBox")).Text);
			editTransferDataHolder.ReferenceNumber = ((TextBox)row.FindControl("EditReferenceNumberTextBox")).Text;

            TransferDataHolderList[e.RowIndex] = editTransferDataHolder;

            ViewState["TransferDataHolderList"] = TransferDataHolderList;
            
            TransfersBindData();
        }

        protected void SuccessfulTransferGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.SuccessfulTransferGridView.ShowFooter = false;
            SuccessfulTransferGridView.EditIndex = e.NewEditIndex;

            TransfersBindData();
        }

        protected void SuccessfulTransferGridView_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // For Empty DataSource, we've created one null record so that the header and footers are displayed.  We then hide this row
                if (TransferDataHolderList.Count <= e.Row.RowIndex)
                    e.Row.Visible = false;

                if (TransferDataHolderList.Count > e.Row.RowIndex && TransferDataHolderList[e.Row.RowIndex] == null)
                {
                    e.Row.Visible = false;
                    TransferDataHolderList.RemoveAt(e.Row.RowIndex);
                }

				//if (this.Completed == true)
				//{
				//    GridViewRow row = SuccessfulTransferGridView.Rows[e.Row.RowIndex];
				//    if( TransferDataHolderList[e.Row.RowIndex].Status.Equals(Transfer.Statuses.Success))
				//        row.FindControl("SuccessImage").Visible = true;
				//    else
				//        row.FindControl("FailedImage").Visible = true;
				//}
            }
        }

        protected void SuccessfulTransferGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
				if (e.Row.RowIndex != SuccessfulTransferGridView.EditIndex)
				{
					LinkButton l = (LinkButton)e.Row.FindControl("DeleteLinkButton");
					l.Attributes.Add("onclick", "javascript:return " +
					"confirm('Are you sure you want to delete this Invoice Item?')");
				}
            }
        }

        protected void SuccessfulTransferGridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.SuccessfulTransferGridView.ShowFooter = true;
            SuccessfulTransferGridView.EditIndex = -1;

            TransfersBindData();
        }
        #endregion

        protected void SaveButton_Click(object sender, EventArgs e)
        {
			Page.Validate("");
			if (Page.IsValid)
			{
				this.ErrorLabel.Visible = false;
				this.ErrorLabel.Text = "";
				
				for (int i=0; i< this.TransferDataHolderList.Count; i++)
				{
					try
					{
						if (!this.CompletedTransferKList.Contains(TransferDataHolderList[i].K))
						{
							Transfer transfer = new Transfer(TransferDataHolderList[i].K);
							if (transfer.Amount != TransferDataHolderList[i].Amount)
							{
								this.ErrorLabel.Text += "ERROR: Transfer #" + transfer.K.ToString() + " has amount " + transfer.Amount.ToString("c")
														+ "\n. You've entered " + TransferDataHolderList[i].Amount.ToString("c") + ". Please resolve.\n";
								this.ErrorLabel.Visible = true;
							}
							else if (!transfer.Status.Equals(Transfer.StatusEnum.Pending))
							{
								this.ErrorLabel.Text += "ERROR: Transfer #" + transfer.K.ToString() + " has status " + transfer.Status.ToString()
														+ "\n. Only Pending transfers allowed to be made successful.\n";
								this.ErrorLabel.Visible = true;
							}
							else
							{
								transfer.Status = Transfer.StatusEnum.Success;
								transfer.DateTimeComplete = DateTime.Now;
								transfer = TransferDataHolderList[i].SetReferenceNumber(transfer);	
								transfer.Update();
								transfer.UpdateAffectedInvoices();
								Utilities.EmailTransfer(transfer, false, true);
								TransferDataHolderList[i] = new TransferDataHolder(transfer);
								this.CompletedTransferKList.Add(transfer.K);
							}
						}
						else
						{
							this.ErrorLabel.Text += "ERROR: Transfer #" + TransferDataHolderList[i].K.ToString() + " has already been successfully updated.\n";
							this.ErrorLabel.Visible = true;
						}
					}
					catch (Exception ex)
					{
						// Display error message
						this.ErrorLabel.Text += ex.Message + "\n";
						this.ErrorLabel.Visible = true;
					}
					
				}
				this.Completed = true;
				ShowResults();
			}
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(Spotted.Admin.AdminMainAccounting.Uri);
        }

		private void ShowResults()
		{
			if (Completed == true)
			{
				this.SuccessfulTransferGridView.Columns[0].Visible = true;
				foreach (GridViewRow row in this.SuccessfulTransferGridView.Rows)
				{
					if (row.RowType.Equals(DataControlRowType.DataRow))
					{
						if (TransferDataHolderList[row.RowIndex].Status.Equals(Transfer.StatusEnum.Success))
							row.FindControl("SuccessImage").Visible = true;
						else
							row.FindControl("FailedImage").Visible = true;
					}
				}

			}
		}
    }
}
