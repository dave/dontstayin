using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Spotted.Controls
{
	public partial class EventGetter : EnhancedUserControl
	{
		public int Width
		{
			get { return int.Parse(uiEventDisplayDiv.Style["width"]); }
			set { this.uiEventDisplayDiv.Style["width"] = value.ToString(); }
		}
	}
}
