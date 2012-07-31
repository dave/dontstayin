using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs;

namespace Spotted.Pages
{
	public partial class Faq : DsiUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{

			FaqPh.Controls.Clear();
			Comment c = new Comment(20717915);

			FaqPh.Controls.Add(new LiteralControl(c.Text));
		}
	}
}
