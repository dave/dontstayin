using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Spotted.Controls
{
	public partial class SpottersChecklist : System.Web.UI.UserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			this.DataBind();
		}
		public bool AllTicked
		{
			get
			{
				return (
					Checkbox2.Checked &&
					Checkbox3.Checked &&
					Checkbox6.Checked &&
					Checkbox9.Checked);
			}
		}
	}
}
