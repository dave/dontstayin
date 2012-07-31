using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs;

namespace MixmagGreatest
{
	public partial class Stats1 : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

			
			Dg.DataSource = Cambro.Misc.Db.Dr(@"
SELECT COUNT(*) FROM Usr where IsDj=1
");
			Dg.DataBind();



		}
	}
}
