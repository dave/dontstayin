using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs;

namespace Spotted.Pages
{
	public partial class Mixmag : DsiUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			//MixmagPh.Controls.Clear();
			//MixmagPh.Controls.Add(new LiteralControl(Common.Settings.MixmagPageHtml));


			MixmagPh.Controls.Clear();
			Comment c = new Comment(Vars.DevEnv ? 20690623 : 20717954);

			MixmagPh.Controls.Add(new LiteralControl(c.Text));
		}
	}
}
