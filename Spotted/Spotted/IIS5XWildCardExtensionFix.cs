using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Spotted
{
	public class IIS5XWildCardExtensionFix : IHttpModule
	{
		public IIS5XWildCardExtensionFix() { }

		public void Dispose() { }

		public void Init(HttpApplication context)
		{
			context.BeginRequest += new EventHandler(OnBeginRequest);
		}

		private void OnBeginRequest(object sender, EventArgs e)
		{
			HttpApplication app = sender as HttpApplication;
			HttpContext context = app.Context;
			string path = context.Request.Path;
			int asmx = path.IndexOf(".asmx/", StringComparison.OrdinalIgnoreCase);
			if (asmx >= 0)
				context.RewritePath(
					path.Substring(0, asmx + 5),
					path.Substring(asmx + 5),
					context.Request.QueryString.ToString());
		}

	}
}
