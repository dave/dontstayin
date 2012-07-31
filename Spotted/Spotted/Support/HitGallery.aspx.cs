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

namespace Spotted.Support
{
	public partial class HitGallery : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			Gallery g = new Gallery(int.Parse(Request.QueryString["K"]));
			int photos = int.Parse(Request.QueryString["P"]);
			g.SetGalleryUsr(photos);
		}
	}
}
