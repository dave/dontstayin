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
	public partial class BannerCancel : PromoterUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			ContainerPage.SetPageTitle("Banner - cancel existing campaign");

			Usr.KickUserIfNotLoggedIn();

			if (!Usr.Current.IsPromoter && !Usr.Current.IsAdmin)
			{
				throw new Exception("You must be a promoter to view this page");
			}
		}
	}
}
