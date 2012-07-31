using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Bobs;
using Bobs.DataHolders;

namespace Spotted.Pages.Promoters
{
	public partial class CampaignCreditHistory : PromoterUserControl
    {
        protected override void OnPreRender(EventArgs e)
		{
            Response.CacheControl = "no-cache";
            Response.AddHeader("Pragma", "no-cache");
            Response.Expires = -1;

            base.OnPreRender(e);
			ContainerPage.SetPageTitle("Promoter administration");

            if (!IsPostBack)
            {
                int campaignCreditRecordCount = 0;
                Query query = new Query(new And(new Q(CampaignCredit.Columns.PromoterK, CurrentPromoter.K), new Q(CampaignCredit.Columns.Enabled, true)));
                query.Columns = new ColumnSet();
                query.ExtraSelectElements.Add("CountRecords", "COUNT([" + Tables.GetTableName(TablesEnum.CampaignCredit) + "].[" + CampaignCredit.GetColumnName(CampaignCredit.Columns.K) + "])");
                CampaignCreditSet ccs = new CampaignCreditSet(query);
                if (ccs.Count > 0 && ccs[0].ExtraSelectElements["CountRecords"] != DBNull.Value)
                    campaignCreditRecordCount = Convert.ToInt32(ccs[0].ExtraSelectElements["CountRecords"]);

                uiPaginationControl.PageCount = Convert.ToInt32(Math.Ceiling((double)campaignCreditRecordCount / 20d));
            }

            Query campaignCreditQuery = new Query(new And(new Q(CampaignCredit.Columns.PromoterK, CurrentPromoter.K), new Q(CampaignCredit.Columns.Enabled, true)));
            //campaignCreditQuery.OrderBy = new OrderBy(new OrderBy(CampaignCredit.Columns.ActionDateTime, OrderBy.OrderDirection.Descending), new OrderBy(CampaignCredit.Columns.DisplayOrder, OrderBy.OrderDirection.Descending));
			//campaignCreditQuery.OrderBy = new OrderBy(CampaignCredit.Columns.K, OrderBy.OrderDirection.Descending);
			campaignCreditQuery.OrderBy = new OrderBy("DATEADD(minute, [CampaignCredit].[DisplayOrder], [CampaignCredit].[ActionDateTime]) desc");
            campaignCreditQuery.Paging.RecordsPerPage = 20;
            campaignCreditQuery.Paging.RequestedPage = uiPaginationControl.CurrentPage;
            campaignCreditQuery.TopRecords = (uiPaginationControl.CurrentPage * 20) + 1;
            CampaignCreditSet campaignCredits = new CampaignCreditSet(campaignCreditQuery);

            //this.CurrentCampaignCreditBalance.Text = campaignCredits.Count > 0 ? campaignCredits[0].BalanceToDate.ToString() : "0";

            this.CampaignCreditHistoryGridView.DataSource = campaignCredits;
            this.CampaignCreditHistoryGridView.DataBind();

        }

        //#region Page Events
        //protected void BuyCampaignCreditsButton_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect(UrlInfo.MakeUrl(CurrentPromoter.UrlFilterPart,"CampaignCredits",null));
        //}
        //#endregion
    }
}
