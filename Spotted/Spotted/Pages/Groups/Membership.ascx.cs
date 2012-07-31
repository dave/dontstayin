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

namespace Spotted.Pages.Groups
{
	public partial class Membership : DsiUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			//Included for legacy URL compatibility - [group]/membership/join
			Response.Redirect(ContainerPage.Url.ObjectFilterGroup.UrlApp("join"));
		}
	}
}
