using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Spotted.Blank
{
	public partial class Flush : BlankUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Bobs.Vars.DevEnv)
				throw new Exception("Dev env only");
			Bobs.Vars.FlushAllMemcached();
			Lab1.Text = "Flushed.";
		}
	}
}
