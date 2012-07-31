using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Spotted.CustomControls
{
	[ToolboxData("<{0}:InlineScript runat=server></{0}:InlineScript>")]
	public class InlineScript : Control
	{
		public enum TypeEnum
		{
			StartOfPage,
			EndOfPage
		}
		public TypeEnum Type { get; set; }
		protected override void Render(HtmlTextWriter writer)
		{
			ScriptManager sm = ScriptManager.GetCurrent(Page); 
			if (sm != null && sm.IsInAsyncPostBack)
			{
				StringBuilder sb = new StringBuilder();
				base.Render(new HtmlTextWriter(new StringWriter(sb)));
				string script = sb.ToString();
				if (Type == TypeEnum.StartOfPage)
					ScriptManager.RegisterClientScriptBlock(this, typeof(InlineScript), UniqueID, script, false);
				else
					ScriptManager.RegisterStartupScript(this, typeof(InlineScript), UniqueID, script, false);
			}
			else
			{
				base.Render(writer);
			}
		}
	}
}
