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
	public partial class Buddies : UsrUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				Usr.KickUserIfNotLoggedIn("You must be logged in to view buddies");
				int buddyCount = ThisUsr.BuddiesFullCount;

				if (ThisUsr.K == Usr.Current.K)
				{
					usrBrowser.HeaderText = "My buddies";
					switch (buddyCount)
					{
						case 0:
							usrBrowser.DescriptionText = "You have no buddies yet! Try <a href=\"/pages/friendinviter\">inviting your friends</a>, or check your <a href=\"" + Usr.Current.UrlBuddyRequestsIveSent() + "\">pending buddy requests</a>.";
							break;
						case 1:
							usrBrowser.DescriptionText = "You currently have <b>one</b> buddy:";
							break;
						default:
							usrBrowser.DescriptionText = "You currently have <b>" + buddyCount + "</b> buddies:";
							break;
					}
				}
				else
				{
					usrBrowser.HeaderText = ThisUsr.NickName + "'s buddies";
					switch (buddyCount)
					{
						case 0:
							usrBrowser.DescriptionText = "<b>" + ThisUsr.NickName + "</b> has no buddies yet!";
							break;
						case 1:
							usrBrowser.DescriptionText = "<b>" + ThisUsr.NickName + "</b> has <b>one</b> buddy:";
							break;
						default:
							usrBrowser.DescriptionText = "<b>" + ThisUsr.NickName + "</b> has <b>" + buddyCount + "</b> buddies:";
							break;
					}

				}
				ContainerPage.SetPageTitle(usrBrowser.HeaderText);

				usrBrowser.BrowsingObject = ThisUsr;
				usrBrowser.BaseQ = ThisUsr.BuddiesFullQ;
				usrBrowser.TotalSetCount = buddyCount;
				usrBrowser.JoinToUsrTable = new Join(Buddy.Columns.BuddyUsrK, Usr.Columns.K);
				//usrBrowser.OrderByNewest = new OrderBy(Usr.Columns.DateTimeSignUp);
				usrBrowser.NoRecordsDisplayedText = "No buddies here!";
				usrBrowser.ShowRealNames = ThisUsr.K == Usr.Current.K;
				usrBrowser.AlwaysShowNicknames = ThisUsr.K != Usr.Current.K;
			}
		}
	}
}
