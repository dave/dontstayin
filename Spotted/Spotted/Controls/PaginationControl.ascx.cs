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

namespace Spotted.Controls
{
	public partial class PaginationControl : System.Web.UI.UserControl
	{
		public int CurrentPage
		{
			get
			{
				int? currentPage = this.ViewState[this.ClientID + "_CurrentPage"] as int?;
				return currentPage.HasValue && currentPage.Value > 0? currentPage.Value : 1;
			}
			set
			{
                this.ViewState[this.ClientID + "_CurrentPage"] = value;
			}
		}
		public int PageCount
		{
			get
			{
                int? pageCount = this.ViewState[this.ClientID + "_PageCount"] as int?;
                return pageCount.HasValue && pageCount.Value > 0 ? pageCount.Value : 1;
			}
			set
			{
                this.ViewState[this.ClientID + "_PageCount"] = value;
			}
		} 

		protected override void OnPreRender(EventArgs e)
		{
			uiPrevPage.Enabled = CurrentPage > 1;
			uiNextPage.Enabled = CurrentPage < PageCount;
			this.uiCurrentPage.Text = CurrentPage.ToString();
			this.uiPageCount.Text = PageCount.ToString();
			base.OnPreRender(e);
		}
		protected void uiPrevPage_Click(object sender, EventArgs e)
		{
			CurrentPage -= 1;
		}

		protected void uiNextPage_Click(object sender, EventArgs e)
		{
			CurrentPage += 1;
		}

	
	}
}
