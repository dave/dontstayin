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

namespace Spotted.Pages.Usrs
{
	public partial class BuddyRequestsIveSent : UsrUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				Usr.KickUserIfNotLoggedIn("You must be logged in to view buddies");

				if (ThisUsr.K != Usr.Current.K)
				{
					throw new DsiUserFriendlyException("You can only view your own Buddy Requests!");
				}
				ContainerPage.SetPageTitle("Buddy Requests I've Sent");

				int buddiesPendingCount = Usr.Current.BuddiesPendingCount;

				if (buddiesPendingCount == 0)
					usrBrowser.DescriptionText = "You have no outstanding buddy requests."; // View all of <a href=\"" + Usr.Current.UrlFavouritePhotos() + "\">my buddies</a>, or back to <a href=\"" + Usr.Current.Url() + "\">my profile</a>.";
				else
					usrBrowser.DescriptionText = "You are still awaiting buddy confirmation from <b>" + (buddiesPendingCount == 1 ? "one</b> person:" : buddiesPendingCount + "</b> people:");

				usrBrowser.TotalSetCount = buddiesPendingCount;
				usrBrowser.BrowsingObject = Usr.Current;
				usrBrowser.BaseQ = Usr.Current.BuddiesPendingQ;
				usrBrowser.HeaderText = "Buddy requests I've sent";
				usrBrowser.JoinToUsrTable = Usr.BuddiesPendingQJoin;
				usrBrowser.JoinToBuddyTable = true;
			}
		}
	}
}
