using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs;

namespace MixmagGreatest
{
	public partial class Stats : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

			if (Request.QueryString["password"] != "mixmag15548")
				throw new Exception("wrong password");

			Dg1.DataSource = Cambro.Misc.Db.Dr(@"
SELECT COUNT(*) AS Votes FROM MixmagGreatestVote
");
			Dg1.DataBind();

			Dg.DataSource = Cambro.Misc.Db.Dr(@"
SELECT Name, (SELECT COUNT(*) FROM MixmagGreatestVote WHERE MixmagGreatestDjK=K) AS Votes FROM MixmagGreatestDj ORDER BY K
");
			Dg.DataBind();

			Dg2.DataSource = Cambro.Misc.Db.Dr(@"
SELECT Name, (SELECT COUNT(*) FROM MixmagGreatestVote WHERE MixmagGreatestDjK=K) AS Votes FROM MixmagGreatestDj ORDER BY (SELECT COUNT(*) FROM MixmagGreatestVote WHERE MixmagGreatestDjK=K) DESC
");
			Dg2.DataBind();


		}
	}
}
