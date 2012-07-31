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
using System.ComponentModel;

namespace Spotted.Controls
{
	public partial class Video : System.Web.UI.UserControl
	{
		public int Width { get; set; }
		public int Height { get; set; }
		public string VideoUrl{get;set;}
		public string JpgUrl { get; set; }
		public string LinkUrl { get; set; }
		public bool AutoStart { get; set; }

	
	}
}
