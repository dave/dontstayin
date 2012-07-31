using System;
using System.Data;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Microsoft.JScript;
using Spotted;

namespace Spotted.Controls.ClientSideRepeater
{
	[ClientScript]
	public abstract class Template : EnhancedUserControl
	{

		public Template()
		{
		}

		public string TemplateText { get; set; }
		protected override void Render(HtmlTextWriter writer)
		{
			writer.WriteLine("<DIV id='" + this.ClientID + "' style='display:none'>");
			base.Render(writer);
			writer.WriteEndTag("DIV");
		}
		protected override void RenderChildren(HtmlTextWriter writer)
		{
			using (StringWriter sw = new StringWriter())
			using (HtmlTextWriter tw = new HtmlTextWriter(sw))
			{
				base.RenderChildren(tw);
				this.TemplateText = sw.ToString();
				writer.Write(GlobalObject.escape(this.TemplateText));
			}
		}
	}
}
