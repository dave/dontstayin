using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Spotted.Pages
{
	public partial class SearchResults : DsiUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.ContainerPage.HideBottomVideoBox = true;
			this.ContainerPage.UseLeftHandSideForContent = true;
			//this.ContainerPage.Menu.uiSiteSearchBox.uiAuto.Text = Request.QueryString["q"];
			
			
		}
	}
}
