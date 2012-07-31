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

namespace Spotted.Controls.Admin
{
	public partial class BannerDataGrid : System.Web.UI.UserControl
	{
		protected Panel NoBanners;
		#region Banners
		public BannerSet Banners
		{
			get
			{
				return banners;
			}
			set
			{
				banners = value;
			}
		}
		private BannerSet banners;
		#endregion

		protected DataGrid BannersDataGrid;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (Banners!=null && Banners.Count > 0)
			{
				BannersDataGrid.DataSource = Banners;
				BannersDataGrid.DataBind();
				NoBanners.Visible = false;
			}
			else
				NoBanners.Visible = true;
		}
	}
}
