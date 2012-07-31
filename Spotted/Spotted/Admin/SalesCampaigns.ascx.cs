using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Bobs;

namespace Spotted.Admin
{
	public partial class SalesCampaigns : AdminUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Query q = new Query();
			SalesCampaignSet scs = new SalesCampaignSet(q);
			CampaignsDataGrid.DataSource = scs;
			CampaignsDataGrid.DataBind();


		}
		#region Add
		protected void Add(object sender, EventArgs eventArgs)
		{
			SalesCampaign c = new SalesCampaign();
			c.UsrK = Usr.Current.K;
			c.Name = AddName.Text;
			c.Description = AddDescription.Text;
			c.DateStart = AddStartDate.Date;
			c.DateEnd = AddEndDate.Date;
			c.Update();
			Response.Redirect("/admin/salescampaigns");
		}
		#endregion
	}
}
