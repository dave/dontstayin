using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Spotted.Pages
{
	public partial class TagTest : DsiUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Bobs.Usr.KickUserIfNotAdmin("");
			ContainerPage.UseLeftHandSideForContent = true;
		}
		protected void Go(object sender, EventArgs e)
		{
			TagOut.InnerHtml = TagIn.Text;
		}
		protected void Clear(object sender, EventArgs e)
		{
			TagOut.InnerHtml = "<p>(empty)</p>";
		}
	}
}
