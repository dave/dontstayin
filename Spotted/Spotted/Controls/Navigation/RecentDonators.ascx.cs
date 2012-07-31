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

namespace Spotted.Controls.Navigation
{
	public partial class RecentDonators : System.Web.UI.UserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			uiRepeater.ItemTemplate = this.LoadTemplate("/Templates/Usrs/RecentDonator.ascx");
			uiRepeater.DataSource = UsrDonationIcon.GetMostRecentDonators(10);
			uiRepeater.DataBind();
		}
	}
}
