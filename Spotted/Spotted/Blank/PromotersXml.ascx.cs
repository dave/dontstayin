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
	public partial class PromotersXml : BlankUserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotAdmin("Must be admin");

			string fileSystemPath = "";
			if (Vars.DevEnv)
				fileSystemPath = @"C:\inetpub\DontStayIn\PromoterXmlTmp\promoters.mxi";
			else
				fileSystemPath = @"\\" + Vars.ExtraIp + @"\d$\DontStayIn\Live\DontStayInTemp\promoters.mxi";



			Response.ContentType = "text/xml";
			Response.Clear();
			Response.WriteFile(fileSystemPath);
			Response.End();

		}
	}
}
