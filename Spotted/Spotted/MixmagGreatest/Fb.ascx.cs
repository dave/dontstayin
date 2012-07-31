using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs;

namespace Spotted.MixmagGreatest
{
	public partial class Fb : MixmagGreatestUserControl
	{
		public MixmagGreatestDj Current
		{
			get
			{
				if (current == null)
					current = new MixmagGreatestDj(ContainerPage.Url.MixmagGreatestDjK);
				return current;
			}
		}
		MixmagGreatestDj current;

		protected void Page_Load(object sender, EventArgs e)
		{
			FacebookComments.Attributes["data-href"] = "mixmag-greatest.com/fb/" + Current.UrlName;
		}
	}
}
