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
using Bobs;

namespace Spotted.Pages
{
	[ClientScript]
	public partial class FindYourPhoto : DsiUserControl
	{

		public FindYourPhoto()
		{
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			ContainerPage.SetPageTitle("Find your photo");
		}

		


	}
}
