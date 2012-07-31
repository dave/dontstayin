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

namespace Spotted.Support
{
	public partial class Style : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Response.ContentType = "text/css";
		}


		protected string adjustLinks(string element, string normalBackground, string hoverBackground)
		{

			return @"
" + element + @" a:link,
" + element + @" a:visited       { background-color:" + (normalBackground == "transparent" ? "transparent" : ("#" + normalBackground)) + @"; }
" + element + @" a:hover         { background-color:" + (hoverBackground == "transparent" ? "transparent" : ("#" + hoverBackground)) + @"; }

" + element + @" .Rollover       { background-color:transparent; }
" + element + @" .Rollover:hover { background-color:" + (hoverBackground == "transparent" ? "transparent" : ("#" + hoverBackground)) + @"; }

";

		}
	}
}
