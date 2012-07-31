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
	public partial class Invite : UsrUserControl
	{
		protected override void Page_Init(object sender, System.EventArgs e)
		{
			base.Page_Init(sender, e);
		}
		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn();
			if (!Page.IsPostBack)
			{
				if (Mode.Equals(Modes.None))
					ChangePanel(PanelInvite);
			}
		}

		#region PanelInvite
		protected Panel PanelInvite;
		protected Spotted.CustomControls.h1 Header;
		protected HtmlAnchor InviteUsrAnchor;
		protected DropDownList GroupDropDown;
		protected Panel InviteErrorPanel, InviteSentPanel;
		protected HtmlGenericControl InviteErrorMessage, InviteSentMessage;
		protected TextBox InviteMessage;
		private void PanelInvite_Load(object sender, System.EventArgs e)
		{
			if (Mode.Equals(Modes.None))
			{
				ContainerPage.SetPageTitle(String.Format("Invite {0} to a group...", ThisUsr.NickName));
				InviteUsrAnchor.HRef = ThisUsr.Url();
				InviteUsrAnchor.InnerText = ThisUsr.NickName;
				ThisUsr.MakeRollover(InviteUsrAnchor);
				Header.InnerText = String.Format("Invite {0} to a group...", ThisUsr.NickName);

				if (!Page.IsPostBack)
				{
					Query q = new Query();
					q.Columns = new ColumnSet(Group.Columns.Name, Group.Columns.K);
					q.QueryCondition = Usr.Current.GroupMemberQ;
					q.TableElement = Group.UsrMemberJoin;
					q.OrderBy = new OrderBy(Group.Columns.Name);
					GroupSet gs = new GroupSet(q);
					if (gs.Count == 0)
					{
						ChangePanel(PanelNoGroups);
					}
					else
					{
						GroupDropDown.DataSource = gs;
						GroupDropDown.DataTextField = "Name";
						GroupDropDown.DataValueField = "K";
						GroupDropDown.DataBind();
					}
				}
			}
		}
		protected void Invite_Click(object sender, System.EventArgs e)
		{
			if (Mode.Equals(Modes.None))
			{
				if (Page.IsValid)
				{
					Usr.IncrementSpamBotDefeaterCounter(Usr.SpamBotDefeaterCounter.GroupInvites, Usr.Current.K);

					Group g = new Group(int.Parse(GroupDropDown.SelectedValue));
					GroupUsr guThisUsr = g.GetGroupUsr(ThisUsr);
					GroupUsr guCurrentUsr = g.GetGroupUsr(Usr.Current);

					Return r = g.Invite(ThisUsr, guThisUsr, Usr.Current, guCurrentUsr, InviteMessage.Text, false);
					InviteErrorPanel.Visible = !r.Success;
					InviteSentPanel.Visible = r.Success;
					InviteErrorMessage.InnerHtml = "";
					InviteSentMessage.InnerHtml = "";
					if (r.Success)
						InviteSentMessage.InnerHtml = r.MessageHtml;
					else
						InviteErrorMessage.InnerHtml = r.MessageHtml;
				}
			}
		}
		#endregion

		#region PanelNoGroups
		protected Panel PanelNoGroups;
		#endregion

		#region PageMode
		Modes Mode
		{
			get
			{
				if (ContainerPage.Url[0].Equals("xxx"))
					return Modes.XXX;
				else
					return Modes.None;
			}
		}
		public enum Modes
		{
			None,
			XXX
		}
		#endregion

		#region ChangePanel
		void ChangePanel(Panel p)
		{
			PanelInvite.Visible = p.Equals(PanelInvite);
			PanelNoGroups.Visible = p.Equals(PanelNoGroups);
		}
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.PanelInvite_Load);

		}
		#endregion
	}
}
