using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JsWebControls
{
	[ToolboxData("<{0}:HelloWorldControl runat=server></{0}:HelloWorldControl>")]
	public class HelloWorldControl : WebControl
	{
		
		public HelloWorldControl()
			: base(HtmlTextWriterTag.Span)
		{
			this.CssClass = "HelloWorld";
		}
		protected override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);
			writer.Write("<script>new ScriptSharpLibrary.HelloWorld($get('" + ClientID + "'));</script>");
		}
		
	}
}
