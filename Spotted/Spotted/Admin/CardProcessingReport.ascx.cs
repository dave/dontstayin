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
using Spotted.Controls;
using Spotted.Master;

namespace Spotted.Admin
{
    public partial class CardProcessingReport : AdminUserControl
    {
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                DefaultDates();
                GetResults();
            }
        }
        #endregion

        #region Methods
        private void DefaultDates()
        {
            this.FromDateCal.Date = Utilities.GetStartOfMonth(DateTime.Today.AddMonths(-1));
            this.ToDateCal.Date = Utilities.GetStartOfDay(Utilities.GetEndOfMonth(DateTime.Today.AddMonths(-1)));
        }

        private void GetResults()
        {
            Query searchCardTransferQuery = new Query();

            List<Q> QueryConditionList = new List<Q>();

            searchCardTransferQuery.OrderBy = new OrderBy("dateadd(day, 0, datediff(day, 0, [Transfer].[DateTimeComplete]))");
            searchCardTransferQuery.GroupBy = new GroupBy("dateadd(day, 0, datediff(day, 0, [Transfer].[DateTimeComplete]))");
            searchCardTransferQuery.ExtraSelectElements.Add("SumAmount", "SUM([Transfer].[Amount])");
            searchCardTransferQuery.ExtraSelectElements.Add("CountTransfers", "COUNT([Transfer].[K])");
            searchCardTransferQuery.ExtraSelectElements.Add("Date", "dateadd(day, 0, datediff(day, 0, [Transfer].[DateTimeComplete]))");
            searchCardTransferQuery.Columns = new ColumnSet();

            QueryConditionList.Add(new Q(Bobs.Transfer.Columns.Method, Bobs.Transfer.Methods.Card));
            QueryConditionList.Add(new Q(Bobs.Transfer.Columns.Status, Bobs.Transfer.StatusEnum.Success));

            if (this.FromDateCal.Date != DateTime.MinValue)
                QueryConditionList.Add(new Q(Bobs.Transfer.Columns.DateTimeComplete, QueryOperator.GreaterThanOrEqualTo, this.FromDateCal.Date));
            if (this.ToDateCal.Date != DateTime.MinValue)
                QueryConditionList.Add(new Q(Bobs.Transfer.Columns.DateTimeComplete, QueryOperator.LessThan, this.ToDateCal.Date.AddDays(1)));

            searchCardTransferQuery.QueryCondition = new And(QueryConditionList.ToArray());

            TransferSet searchCardTransfers = new TransferSet(searchCardTransferQuery);

            this.CardnetAccountGridView.DataSource = searchCardTransfers;
            this.CardnetAccountGridView.DataBind();
            
            SumAmounts(searchCardTransfers);
        }

        private void SumAmounts(TransferSet searchCardTransfers)
        {
            decimal sum = 0;
            int count = 0;
            searchCardTransfers.Reset();
            foreach (Transfer t in searchCardTransfers)
            {
                count += (int)t.ExtraSelectElements["CountTransfers"];
                sum += (decimal)t.ExtraSelectElements["SumAmount"];
            }
            SumAccountLabel.Text = sum.ToString("c");
            SumTransferCountLabel.Text = count.ToString();                        
        }

        public string LinkAdmin(Transfer transfer)
        {
            return Utilities.Link(UrlInfo.PageUrl(UrlInfo.PageTypes.Admin, "adminmainaccounting", "SearchType", Convert.ToInt32(AdminMainAccounting.SearchType.Transfer).ToString(),
                                    "FromDate", ((DateTime)transfer.ExtraSelectElements["Date"]).ToString("dd_MM_yy"), "ToDate", ((DateTime)transfer.ExtraSelectElements["Date"]).ToString("dd_MM_yy"),
                                    "Method", Convert.ToInt32(Bobs.Transfer.Methods.Card).ToString(),
                                    "Status", Convert.ToInt32(Bobs.Transfer.StatusEnum.Success).ToString()),
                                    ((DateTime)transfer.ExtraSelectElements["Date"]).ToString("ddd dd/MM/yy"));
        }
        #endregion

        #region Page Event Handlers
        protected void SearchButton_Click(object sender, EventArgs e)
        {
            CardnetAccountGridView.PageIndex = 0;
            GetResults();
        }
        #endregion

        #region Search Grid View Event Handlers
        protected void CardnetAccountGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Cancel the paging operation if the user attempts to navigate
            // to another page while the GridView control is in edit mode. 
            if (this.CardnetAccountGridView.EditIndex != -1)
            {
                // Use the Cancel property to cancel the paging operation.
                e.Cancel = true;
            }
            else
            {
                CardnetAccountGridView.PageIndex = e.NewPageIndex;
                GetResults();
                if (CardnetAccountGridView.PageIndex > CardnetAccountGridView.PageCount)
                    CardnetAccountGridView.PageIndex = 1;
            }
        }

        #endregion
    }
}
