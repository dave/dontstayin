using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs;

namespace Spotted.MixmagVote
{
	public partial class Stop : MixmagVoteUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string email = Request.QueryString["email"];
			Update u = new Update();
			u.Changes.Add(new Assign(MixmagEntry.Columns.SendDailyEmails, false));
			u.Where = new Q(MixmagEntry.Columns.Email, email);
			u.Table = TablesEnum.MixmagEntry;
			u.Run();

			

		}
	}
}
