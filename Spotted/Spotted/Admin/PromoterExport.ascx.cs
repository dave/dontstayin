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
using System.IO;
using System.Xml;

namespace Spotted.Admin
{
	public partial class PromoterExport : AdminUserControl
	{
		protected void Process_Click(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotAdmin("Must be admin");

			Cambro.Web.Helpers.WriteAlertHeader();
			Promoter.CreatePromoterXml();
			Cambro.Web.Helpers.WriteAlertFooter("/admin/promoterexport");



		}

		
	}
}
