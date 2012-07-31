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
using Bobs;

namespace Spotted.Pages
{
	public partial class HtmlTest : DsiUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Usr.Current == null || !Usr.Current.IsAdmin)
			{
				throw new DsiUserFriendlyException("Access denied.");
			}
			this.Button1.Click += new EventHandler(Button1_Click);
		}

		void Button1_Click(object sender, EventArgs e)
		{
			var q = new Query(new Q(Bobs.Usr.Columns.K, this.MultiBuddyChooser1.SelectedUsrKs.ToArray()));
			GridView1.DataSource = from u in new UsrSet(q) select new { u.K, u.NickName, u.Email };
			GridView1.DataBind();
		}
	}
}
