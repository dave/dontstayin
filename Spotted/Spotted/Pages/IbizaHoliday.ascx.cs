using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs;

namespace Spotted.Pages
{
	public partial class IbizaHoliday : DsiUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Ph.Controls.Clear();
			Comment c = new Comment(21230296);

			Ph.Controls.Add(new LiteralControl(c.Text));
		}
	}
}
