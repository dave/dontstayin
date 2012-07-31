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

namespace Spotted.Admin
{
	public partial class TicketSearch : AdminUserControl
	{
		#region Page Load
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{
				if (this.ContainerPage.Url["ticketrunk"].Exists && this.ContainerPage.Url["ticketrunk"].IsInt)
				{
					TicketRunKTextBox.Text = this.ContainerPage.Url["ticketrunk"].Value;
				}
				else if (this.ContainerPage.Url["promoterk"].Exists && this.ContainerPage.Url["promoterk"].IsInt)
				{
					this.uiPromotersAutoComplete.Value = this.ContainerPage.Url["promoterk"].Value;
					this.uiPromotersAutoComplete.Text = new Promoter(Convert.ToInt32(this.ContainerPage.Url["promoterk"].Value)).Name;
				}

				SetupStatusDropDownList();
				SetupFeedbackDropDownList();

				GetSearchResults();
			}
		}
		#endregion

		#region Methods
		#region Setup DropDownLists
		private void SetupStatusDropDownList()
		{
			StatusDropDownList.Items.Clear();
			StatusDropDownList.Items.Add(new ListItem("", ""));
			StatusDropDownList.Items.Add(new ListItem("Valid", "false"));
			StatusDropDownList.Items.Add(new ListItem("Cancelled", "true"));
		}

		private void SetupFeedbackDropDownList()
		{
			FeedbackDropDownList.Items.Clear();
			FeedbackDropDownList.Items.Add(new ListItem("", ""));
			FeedbackDropDownList.Items.AddRange(Ticket.FeedbackAsListItemArray());
		}
		#endregion

		

		private void GetSearchResults()
		{
			// Query the database based on the search parameters
			// Set the returned results to be the data source of the GridView
			Query q = new Query();
			List<Q> QueryConditionList = new List<Q>();

			q.OrderBy = new OrderBy(Ticket.Columns.K, OrderBy.OrderDirection.Descending);
			QueryConditionList.Add(Ticket.SoldTicketsQ);
			if (this.TicketRunKTextBox.Text.Trim().Length > 0)
				QueryConditionList.Add(new Q(Ticket.Columns.TicketRunK, Convert.ToInt32(TicketRunKTextBox.Text)));
			if (this.FirstNameTextBox.Text.Trim().Length > 0)
			{
                QueryConditionList.Add(new Q(Ticket.Columns.FirstName, QueryOperator.TextStartsWith, this.FirstNameTextBox.Text.Trim()));
				//q.TableElement = new Join(Ticket.Columns.BuyerUsrK, Usr.Columns.K);
			}
			if (this.LastNameTextBox.Text.Trim().Length > 0)
			{
				QueryConditionList.Add(new Q(Ticket.Columns.LastName, QueryOperator.TextStartsWith, this.LastNameTextBox.Text.Trim()));
				//q.TableElement = new Join(Ticket.Columns.BuyerUsrK, Usr.Columns.K);
			}
			if (this.uiUsersAutoComplete.Value != null && !this.uiUsersAutoComplete.Value.Equals(""))
				QueryConditionList.Add(new Q(Ticket.Columns.BuyerUsrK, Convert.ToInt32(this.uiUsersAutoComplete.Value)));
			if (this.uiPromotersAutoComplete.Value != null && !uiPromotersAutoComplete.Value.Equals(""))
			{
				QueryConditionList.Add(new Q(TicketRun.Columns.PromoterK, Convert.ToInt32(uiPromotersAutoComplete.Value)));
				if(q.TableElement != null && q.TableElement is Join)
					q.TableElement = new Join(q.TableElement, new TableElement(TablesEnum.TicketRun), QueryJoinType.Inner, Ticket.Columns.TicketRunK, TicketRun.Columns.K);
				else
					q.TableElement = new Join(Ticket.Columns.TicketRunK, TicketRun.Columns.K);
			}
			
			if(this.CardDigitsTextBox.Text.Trim().Length > 0)
				QueryConditionList.Add(new Q(Ticket.Columns.CardNumberEnd, this.CardDigitsTextBox.Text.Trim()));
			if(this.PostCodeTextBox.Text.Trim().Length > 0)
				QueryConditionList.Add(new Q(Ticket.Columns.AddressPostcode, QueryOperator.TextStartsWith, this.PostCodeTextBox.Text.Trim()));
			if (this.FeedbackDropDownList.SelectedValue != "")
			{
				if (this.FeedbackDropDownList.SelectedValue == Ticket.FeedbackEnum.None.ToString())
				{
					QueryConditionList.Add(new Or(new Q(Ticket.Columns.Feedback, QueryOperator.IsNull, null),
												  new Q(Ticket.Columns.Feedback, Convert.ToInt32(FeedbackDropDownList.SelectedValue))));
				}
				else
					QueryConditionList.Add(new Q(Ticket.Columns.Feedback, Convert.ToInt32(FeedbackDropDownList.SelectedValue)));
			}

			if (this.StatusDropDownList.SelectedValue != "")
			{
				if (this.StatusDropDownList.SelectedValue == "0")
				{
					QueryConditionList.Add(new Q(Ticket.Columns.Cancelled, QueryOperator.IsNull, null));
				}
				QueryConditionList.Add(new Q(Ticket.Columns.Cancelled, Convert.ToBoolean(StatusDropDownList.SelectedValue)));
			}

			if(QueryConditionList.Count > 0)
				q.QueryCondition = new And(QueryConditionList.ToArray());

			TicketSet searchResultTickets = new TicketSet(q);

			SearchResultsTicketsGridView.AllowPaging = (searchResultTickets.Count > SearchResultsTicketsGridView.PageSize);
			SearchResultsTicketsGridView.DataSource = searchResultTickets;
			SearchResultsTicketsGridView.Visible = true;
			SearchResultsTicketsGridView.DataBind();

            ErrorLabel.Visible = false;
            ErrorLabel.Text = "Details missing on tickets, please fix:<ul>";
            searchResultTickets.Reset();
            foreach (Ticket ticket in searchResultTickets)
            {
                // Check for details missing for tickets created with the new ticket system
                if (ticket.BuyDateTime > Vars.TICKETS_NEW_SYSTEM_START_DATE && (ticket.LastName.Trim().Length == 0 || ticket.FirstName.Trim().Length == 0 || ticket.CardNumberEnd.Trim().Length == 0 || ticket.InvoiceItemK == 0))
                {
                    ErrorLabel.Visible = true;
                    ErrorLabel.Text += "<li>Ticket #" + ticket.K.ToString() + "</li>";
                }
            }
			// Check for tickets potentially paid for, but not enabled. Tickets up to K = 4200 have been verified.
			Query notEnabledTicketsQuery = new Query(new And(new Q(Ticket.Columns.Enabled, 0),
															 new Q(Ticket.Columns.InvoiceItemK, QueryOperator.GreaterThan, 0),
															 new Q(Ticket.Columns.K, QueryOperator.GreaterThan, 4200)));
			notEnabledTicketsQuery.Columns = new ColumnSet(Ticket.Columns.K);
			TicketSet notEnabledTickets = new TicketSet(notEnabledTicketsQuery);
			foreach (Ticket ticket in notEnabledTickets)
			{
				ErrorLabel.Visible = true;
				ErrorLabel.Text += "<li>Ticket #" + ticket.K.ToString() + " is not enabled</li>";
			}
            ErrorLabel.Text += "</ul>";

			if (searchResultTickets == null || searchResultTickets.Count == 0)
			{
				SearchResultsMessageLabel.Text = "* Zero results for your search.  Please verify your search criteria.";
				SearchResultsMessageLabel.Visible = true;
			}
		}
		#endregion

		#region Page Event Handlers
		protected void SearchButton_Click(object sender, EventArgs e)
		{
			SearchResultsTicketsGridView.PageIndex = 0;
			// Clear the error message.
			SearchResultsMessageLabel.Text = "";
			SearchResultsMessageLabel.Visible = false;

			GetSearchResults();
		}
		protected void ClearButton_Click(object sender, EventArgs e)
		{
			//this.SearchResultsTicketsGridView.PageIndex = 0;
			this.uiUsersAutoComplete.Value = "";
			this.uiUsersAutoComplete.Text = "";
			this.uiPromotersAutoComplete.Value = "";
			this.uiPromotersAutoComplete.Text = "";
			this.TicketRunKTextBox.Text = "";
			this.StatusDropDownList.SelectedIndex = 0;
			this.CardDigitsTextBox.Text = "";
			this.FeedbackDropDownList.SelectedIndex = 0;
			this.FirstNameTextBox.Text = "";
			this.LastNameTextBox.Text = "";
			this.PostCodeTextBox.Text = "";

			SearchResultsMessageLabel.Visible = false;
		}
		
		#endregion

		#region SearchResultsTicketsGridView Event Handlers
		protected void SearchResultsTicketsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			// Cancel the paging operation if the user attempts to navigate
			// to another page while the GridView control is in edit mode. 
			if (SearchResultsTicketsGridView.EditIndex != -1)
			{
				// Use the Cancel property to cancel the paging operation.
				e.Cancel = true;

				// Display an error message.
				int newPageNumber = e.NewPageIndex + 1;
				SearchResultsMessageLabel.Text = "* Please update the record before moving to page " + newPageNumber.ToString() + ".";
				SearchResultsMessageLabel.Visible = true;
			}
			else
			{
				SearchResultsTicketsGridView.PageIndex = e.NewPageIndex;
				GetSearchResults();
				if (SearchResultsTicketsGridView.PageIndex > SearchResultsTicketsGridView.PageCount)
					SearchResultsTicketsGridView.PageIndex = 1;

				// Clear the error message.
				SearchResultsMessageLabel.Text = "";
				SearchResultsMessageLabel.Visible = false;
			}
		}
		#endregion
	}
}
