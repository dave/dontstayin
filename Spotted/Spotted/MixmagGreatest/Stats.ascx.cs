using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs;

namespace Spotted.MixmagGreatest
{
	public partial class Stats : MixmagGreatestUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Query q = new Query();
			q.ReturnCountOnly = true;
			MixmagGreatestVoteSet vs = new MixmagGreatestVoteSet(q);

			Response.Write(vs.Count);
		}
	}
}
