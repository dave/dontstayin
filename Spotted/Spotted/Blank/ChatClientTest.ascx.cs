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

namespace Spotted.Blank
{
	public partial class ChatClientTest : BlankUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			ScriptManager.RegisterStartupScript(this, this.GetType(), "Tip", "mig_hand();", true);
#if DEBUG
			ScriptManager.GetCurrent(Page).Scripts.Add(new ScriptReference("/misc/spottedscript.debug.js"));
#else
			ScriptManager.GetCurrent(Page).Scripts.Add(new ScriptReference("/misc/spottedscript.js"));
#endif

		}
	}
}
