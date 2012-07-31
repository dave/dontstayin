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
namespace Spotted.Pages.Promoters
{
	public partial class BannerFolders : PromoterUserControl
	{
		protected override void OnPreRender(EventArgs e)
		{
            base.OnPreRender(e);
			ContainerPage.SetPageTitle("Promoter administration");
			BannerListHeader.InnerText = BannerListHeader.InnerText.Replace("???", CurrentPromoter.Name);

            if (!IsPostBack)
            {
                int bannerFolderRecordCount = 0;
                Query query = new Query(new Q(BannerFolder.Columns.PromoterK, CurrentPromoter.K));
                query.Columns = new ColumnSet();
                query.ExtraSelectElements.Add("CountRecords", "COUNT([" + Tables.GetTableName(TablesEnum.BannerFolder) + "].[" + BannerFolder.GetColumnName(BannerFolder.Columns.K) + "])");
                BannerFolderSet bfs = new BannerFolderSet(query);
                if (bfs.Count > 0 && bfs[0].ExtraSelectElements["CountRecords"] != DBNull.Value)
                    bannerFolderRecordCount = Convert.ToInt32(bfs[0].ExtraSelectElements["CountRecords"]);

                uiPaginationControl.PageCount = Convert.ToInt32(Math.Ceiling((double)bannerFolderRecordCount / 20d));
            }
			//PagedDataSource pagedBannerFolders = new PagedDataSource();

            Query bannerFoldersQuery = new Query(new Q(BannerFolder.Columns.PromoterK, CurrentPromoter.K));
            bannerFoldersQuery.OrderBy = new OrderBy(BannerFolder.Columns.K, OrderBy.OrderDirection.Descending);
            bannerFoldersQuery.Paging.RecordsPerPage = 20;
            bannerFoldersQuery.Paging.RequestedPage = uiPaginationControl.CurrentPage;
            bannerFoldersQuery.TopRecords = (uiPaginationControl.CurrentPage * 20) + 1;
            BannerFolderSet bannerFolders = new BannerFolderSet(bannerFoldersQuery);

            //pagedBannerFolders.DataSource = CurrentPromoter.BannerFolders;
            //pagedBannerFolders.PageSize = 20;
            //pagedBannerFolders.AllowPaging = true;
            //pagedBannerFolders.CurrentPageIndex = uiPaginationControl.CurrentPage - 1;

            this.uiBannerFolderRepeater.DataSource = bannerFolders;
			this.uiBannerFolderRepeater.DataBind();
		}
	}
}
