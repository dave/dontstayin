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
using Bobs;

namespace Spotted.Pages.Promoters
{
	public partial class Unconfirmed : PromoterUserControl
	{
		#region Page_Init
		protected override void Page_Init(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn();
		}
		#endregion

		#region Page_Load
		protected void Page_Load(object sender, System.EventArgs e)
		{
		}
		#endregion
	}
}
