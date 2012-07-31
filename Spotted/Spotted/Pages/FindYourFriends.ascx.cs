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
	public partial class FindYourFriends : DsiUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn();

			// https:// for contact importer
			ContainerPage.SslPage = true;
			ContainerPage.SetPageTitle("Find your friends");

			Cambro.Web.Helpers.TieButton(uiUserName, uiUserNameButton);
			Cambro.Web.Helpers.TieButton(uiSpotterCode, uiSpotterCodeButton);
			Cambro.Web.Helpers.TieButton(uiLastName, uiRealNameButton);

			this.uiUserNameBuddyControl.SetUpQuery += new EventHandler(uiUserNameBuddyControl_SetUpQuery);
			this.uiRealNameBuddyControl.SetUpQuery += new EventHandler(uiRealNameBuddyControl_SetUpQuery);
		}

		protected void LookUpSpotter(object sender, EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn();
			int spotterCode;
			if (int.TryParse(uiSpotterCode.Text, out spotterCode))
			{
				this.uiSpotterBuddyControl.Query.QueryCondition = new And(
					new Q(Usr.Columns.K, spotterCode),
					new Q(Usr.Columns.IsSpotter, true));
				this.uiSpotterBuddyControl.DataBind();
				this.uiSpotterBuddyControl.Visible = true;
				this.uiInvalidSpottedCode.Visible = false;
			}
			else
			{
				// just display default no results 
				this.uiInvalidSpottedCode.Visible = true;
			}
		}

		protected void LookUpUserName(object sender, EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn();
			uiUserNameBuddyControl_SetUpQuery(null, EventArgs.Empty);
			this.uiUserNameBuddyControl.PageIndex = 0;
			this.uiUserNameBuddyControl.DataBind();
		}

		void uiUserNameBuddyControl_SetUpQuery(object sender, EventArgs e)
		{
			this.uiUserNameBuddyControl.Query.QueryCondition =
				new Q(Usr.Columns.NickName, QueryOperator.TextContains, this.uiUserName.Text);
			// order by those that begin with text first, then those with photo first
			this.uiUserNameBuddyControl.Query.OrderBy = new OrderBy(
				"CASE WHEN [Usr].[Nickname] LIKE '" + this.uiUserName.Text + "%' THEN 0 ELSE 1 END ASC, CASE WHEN [Usr].[PicPhotoK] > 0 THEN 0 ELSE 1 END ASC");
		}

		protected void LookUpRealName(object sender, EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn();
			uiRealNameBuddyControl_SetUpQuery(null, EventArgs.Empty);
			this.uiRealNameBuddyControl.PageIndex = 0;
			this.uiRealNameBuddyControl.DataBind();
			this.uiRealNameBuddyControl.Visible = true;
		}

		void uiRealNameBuddyControl_SetUpQuery(object sender, EventArgs e)
		{
			this.uiRealNameBuddyControl.Query.QueryCondition = new And(
					new Q(Usr.Columns.FirstName, uiFirstName.Text),
					new Q(Usr.Columns.LastName, uiLastName.Text));
			// order by those with photo first
			this.uiRealNameBuddyControl.Query.OrderBy = new OrderBy("CASE WHEN [Usr].[PicPhotoK] > 0 THEN 0 ELSE 1 END ASC");
		}
	}
}
