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
using Bobs;

namespace Spotted.Pages.Usrs
{
	public partial class BuddyRequests : UsrUserControl
	{
		#region UsrKs
		private List<int> usrKs = new List<int>();
		protected string UsrKsList
		{
			get { return usrKs.ConvertAll<string>(i => i.ToString()).Join(","); }
		}
		protected int UsrKsCount
		{
			get { return usrKs.Count; }
		}
		#endregion

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				Usr.KickUserIfNotLoggedIn("You must be logged in to view buddies");

				if (ThisUsr.K != Usr.Current.K)
				{
					throw new DsiUserFriendlyException("You can only view your own Buddy Requests!");
				}
				ContainerPage.SetPageTitle("My Buddy Requests");

				BindDataGrid();
			}

			ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "DbButtonInit", "DbButtonInit(" + Bobs.Vars.LanguageString + ");", true);
		}
		public void ChangePage(object o, GridViewPageEventArgs e)
		{
			uiBuddiesRequested.PageIndex = e.NewPageIndex;
			BindDataGrid();
		}

		private void BindDataGrid()
		{
			List<Usr> usrs = Usr.Current.UsrsWhoHavePendingBuddyRequestsForMe;
			if (usrs.Count > 0)
			{
				uiBuddiesRequested.DataSource = usrs;
				uiBuddiesRequested.DataBind();
				SetPanelVisibility(uiBuddyRequestsPanel);

				if (usrs.Count > 1)
				{
					uiMultiButtonsPanel.Visible = true;
				}
				else
				{
					uiMultiButtonsPanel.Visible = false;
				}
			}
			else
			{
				SetPanelVisibility(uiNoBuddyRequestsPanel);
			}
		}
		private void SetPanelVisibility(Panel p)
		{
			uiBuddyRequestsPanel.Visible = p == uiBuddyRequestsPanel;
			uiNoBuddyRequestsPanel.Visible = p == uiNoBuddyRequestsPanel;
		}

		public void Refresh(object sender, EventArgs e)
		{
			BindDataGrid();
		}

		protected void uiBuddiesRequestedRowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				Usr usr = (Usr)e.Row.DataItem;
				usrKs.Add(usr.K);
				Literal l = (Literal)e.Row.FindControl("uiDbButtonScripts");
				l.Text = string.Format(@"
<p><script>
DbButton(
	""/gfx/icon-star-26-up.png"",
	""/gfx/icon-star-26-dn.png"",
	""{0} has been added to your buddy list"",
	""{0} is not on your buddy list"",
	""Remove from my buddy list"",
	""Add to my buddy list"",
	"""",
	""cursor:pointer;margin-right:3px;"",
	""absmiddle"",
	26,21,
	""Buddy"",
	""{1}"",
	""0"",
	""BuddyButton{2}"",
	""BuddyButtonReturn{2}"",
	"""",
	"""");
function BuddyButtonReturn{2}(id,oldState,newState)
{{
	DbButtonSetState(""BuddyInviteButton{2}"",newState);
	DbButtonSetState(""BuddyDenyButton{2}"",false);
}}</script></p>
<p><script>DbButton(
	""/gfx/icon-inbox-up.png"",
	""/gfx/icon-inbox-dn.png"",
	""{0} can invite you to chat topics"",
	""You have stopped {0} inviting you to chat topics"",
	""Stop inviting me in bulk to chat topics"",
	""Allow to invite me in bulk to chat topics"",
	"""",
	""cursor:pointer;margin-right:3px;"",
	""absmiddle"",
	26,21,
	""BuddyChatInvite"",
	""{1}"",
	""0"",
	""BuddyInviteButton{2}"",
	""BuddyInviteButtonReturn{2}"",
	"""",
	"""");
function BuddyInviteButtonReturn{2}(id,oldState,newState)
{{
	if (newState)
		DbButtonSetState(""BuddyButton{2}"",true);
	DbButtonSetState(""BuddyDenyButton{2}"",false);
}}</script></p>
<p><script>DbButton(
	""/gfx/icon-cross-up.png"",
	""/gfx/icon-cross-dn.png"",
	"""",
	"""",
	""Leave buddy request in this list for later"",
	""Deny and remove buddy request from this list"",
	"""",
	""cursor:pointer;margin-right:3px;"",
	""absmiddle"",
	26,21,
	""BuddyDeny"",
	""{1}"",
	""0"",
	""BuddyDenyButton{2}"",
	""BuddyDenyButtonReturn{2}"",
	"""",
	"""");
function BuddyDenyButtonReturn{2}(id,oldState,newState)
{{
	if (newState)
	{{
		DbButtonSetState(""BuddyButton{2}"",false);
		DbButtonSetState(""BuddyInviteButton{2}"",false);
	}}
}}</script></p>", usr.NickName, usr.K.ToString(), e.Row.RowIndex.ToString());
			}
		}

	}
}
