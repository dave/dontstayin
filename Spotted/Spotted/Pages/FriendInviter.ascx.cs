using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Spotted.Pages
{
	public partial class FriendInviter : DsiUserControl
	{
		protected void Page_Load(object o, EventArgs e)
		{
			Bobs.Usr.KickUserIfNotLoggedIn();

			if (ContainerPage != null)
			{
				ContainerPage.SslPage = true;
				ContainerPage.SetPageTitle("Invite your friends");
			}

			uiBuddyImporter.OnBegin += new EventHandler(uiBuddyImporter_OnBegin);
			uiBuddyImporter.OnDone += new EventHandler(uiBuddyImporter_OnDone);
		}

		void uiBuddyImporter_OnBegin(object sender, EventArgs e)
		{
			this.uiIntroPanel.Visible = false;
		}

		void uiBuddyImporter_OnDone(object sender, EventArgs e)
		{
			this.uiSuccessPanel.Visible = true;
		}

	}
}
