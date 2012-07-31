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

namespace Spotted.Controls
{
	public partial class ExDirectoryPrivacyOption : System.Web.UI.UserControl
	{
		public ExDirectoryPrivacyOption()
		{
			CloseDiv = true;
		}
		public bool CloseDiv { get; set; }
		public bool Checked
		{
			get { return this.uiOptions.SelectedValue == "1"; }
			set { this.uiOptions.SelectedValue = value ? "1" : "0"; }
		}
	}
}
