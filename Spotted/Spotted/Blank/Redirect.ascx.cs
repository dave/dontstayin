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

namespace Spotted.Blank
{
	public partial class Redirect : BlankUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			int domainK;
			bool gotDomainK = int.TryParse(Request.Params["domainK"], out domainK);
			if (gotDomainK)
			{
				if (Visit.HasCurrent)
				{
					Visit.Current.DomainK = domainK;
					Visit.Current.Update();
				}
			}

			string redirectUrl = Request.Params["redirectUrl"];
			if (redirectUrl != null)
			{
				Response.Redirect(redirectUrl);
			}

		}
	}
}
