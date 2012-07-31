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
	public partial class Banners : PromoterUserControl
	{
		#region Page_Init
		protected override void Page_Init(object sender, System.EventArgs e)
		{
			base.Page_Init(sender, e);
		}
		#endregion

		#region PanelBannerList
		protected Panel PanelBannerList;
		protected Spotted.CustomControls.h1 BannerListHeader;
		protected HtmlAnchor BannerListAddLink;
		protected DataGrid BannerListDataGrid;
		public void Page_Load(object o, System.EventArgs e)
		{
			//Usr.KickUserIfNotEmailVerified();
			//if (!Usr.Current.IsPromoter && !Usr.Current.IsAdmin)
			//{
			//    throw new Exception("You must be a promoter to view this page");
			//}
			//if (CurrentPromoter!=null && !CurrentPromoter.IsUsrAllowedAccess(Usr.Current))
			//{
			//    throw new Exception(Vars.CANT_VIEW_DETAILS);
			//}
			
			ContainerPage.SetPageTitle("Promoter administration");


			BannerListHeader.InnerText = BannerListHeader.InnerText.Replace("???", CurrentPromoter.Name);
            //BannerListAddLink.HRef = CurrentPromoter.UrlApp("banneredit", "mode", "add");
            //BannerListAddLink1.HRef = CurrentPromoter.UrlApp("banneredit", "mode", "add");
			BannerListBind();

            if (!Page.IsPostBack)
            {
                FolderListBind();
                SelectBannerFolderFromUrl();               
            }
		}
		void FolderListBind()
		{
			Query q = new Query();
			q.QueryCondition = new Q(BannerFolder.Columns.PromoterK, CurrentPromoter.K);
			q.OrderBy = new OrderBy(BannerFolder.Columns.DateTimeCreated, OrderBy.OrderDirection.Descending);
			BannerFolderSet bfs = new BannerFolderSet(q);
			FolderDropDown.DataSource = bfs;
			FolderDropDown.DataTextField = "Name";
			FolderDropDown.DataValueField = "K";
			FolderDropDown.DataBind();
			FolderDropDown.Items.Insert(0, new ListItem("Choose a folder...", "0"));

		}

		#region Folder_Change
		protected void Folder_Change(object sender, EventArgs eventArgs)
		{
			BannerListBind();
		}
		#endregion

        private void SelectBannerFolderFromUrl()
        {
            if (ContainerPage.Url["BannerFolderK"].IsInt)
            {
                try
                {
                    FolderDropDown.SelectedValue = ContainerPage.Url["BannerFolderK"].Value;
                    BannerListBind();
                }
                catch { }
            }
        }

		int FolderK
		{
			get
			{
				try
				{
					return Convert.ToInt32(FolderDropDown.SelectedValue);
				}
				catch
				{
					return 0;
				}
			}
		}
		void BannerListBind()
		{
			Q folderQ = FolderK > 0 ? new Q(Banner.Columns.BannerFolderK, FolderK) : new Q(true);

			Query q = new Query();

			q.QueryCondition = new And(
				new Q(Banner.Columns.PromoterK, CurrentPromoter.K),
				folderQ);
			q.OrderBy = new OrderBy(Banner.Columns.LastDay, OrderBy.OrderDirection.Descending);
			q.NoLock = true;
			BannerSet bs = new BannerSet(q);
			BannerListDataGrid.AllowPaging = bs.Count > BannerListDataGrid.PageSize;
			BannerListDataGrid.DataSource = bs;
			BannerListDataGrid.DataBind();
		}
		public void BannerListDataGridChangePage(object o, DataGridPageChangedEventArgs e)
		{
			BannerListDataGrid.CurrentPageIndex = e.NewPageIndex;
			BannerListBind();
		}
		#endregion

	}
}
