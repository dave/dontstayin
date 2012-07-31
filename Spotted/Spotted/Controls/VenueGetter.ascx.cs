using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Spotted.Controls
{
	public partial class VenueGetter : EnhancedUserControl
	{
		public Unit Width
		{
			get { return this.uiAuto.Width; }
			set
			{
				this.uiOuterPanel.Width = value;
			}
		}
	}
}
