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

namespace Spotted.Pages
{
	public partial class MyGroups : DsiUserControl
	{
		protected Controls.AddThread AddThread;
		protected HtmlGenericControl
			AddThreadLinkP;
		protected HtmlInputHidden
			AddThreadStatusHidden;
		protected Panel
			AddThreadPanel;

		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn("You must be logged in to view this page");
			ContainerPage.SetPageTitle("My groups");

			AddThread.AddThreadAdvancedCheckBox.Checked = true;
			AddThread.AddThreadGroupRadioButton.Checked = true;

			if (AddThreadStatusHidden.Value.Equals("1"))
			{
				AddThreadPanel.Style["display"] = "";
				AddThreadLinkP.Style["display"] = "none";
			}
			else
			{
				AddThreadPanel.Style["display"] = "none";
				AddThreadLinkP.Style["display"] = "";
			}

			ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "DbButtonInit", "DbButtonInit(" + Bobs.Vars.LanguageString + ");", true);

		}

		#region GroupsPanel
		protected Panel GroupsPanel;

		#region GroupsList
		protected DataGrid GroupsDataGrid;
		protected Panel PanelGroupsList, PanelNoGroups;
		private void GroupsList_Load(object sender, System.EventArgs e)
		{
			BindGroups();
		}
		void BindGroups()
		{
			Query q = new Query();
			q.QueryCondition = new And(
				new Q(GroupUsr.Columns.UsrK, Usr.Current.K),
				new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.Member)
			);
			q.Columns = new ColumnSet(
				GroupUsr.Columns.GroupK,
				GroupUsr.Columns.Status,
				GroupUsr.Columns.Moderator,
				//GroupUsr.Columns.HideWhenRead,
				GroupUsr.Columns.Favourite,
				new JoinedColumnSet(
					GroupUsr.Columns.GroupK,
					Group.Columns.Name,
					Group.Columns.BrandK,
					Group.Columns.TotalMembers,
					Group.Columns.TotalComments,
					Group.Columns.LastPost,
					Group.Columns.UrlName),
				new JoinedColumnSet(
					GroupUsr.Columns.GroupK,
					CommentAlert.Columns.UsrK,
					CommentAlert.Columns.ParentObjectType,
					CommentAlert.Columns.ParentObjectK)
			);
			q.TableElement = GroupUsr.GroupJoin;
			q.TableElement = new Join(
				q.TableElement,
				new TableElement(new Column(GroupUsr.Columns.GroupK, CommentAlert.Columns.ParentObjectK)),
				QueryJoinType.Left,
				new And(
					new Q(GroupUsr.Columns.GroupK, new Column(GroupUsr.Columns.GroupK, CommentAlert.Columns.ParentObjectK), true),
					new Q(new Column(GroupUsr.Columns.GroupK, CommentAlert.Columns.ParentObjectType), Model.Entities.ObjectType.Group),
					new Q(new Column(GroupUsr.Columns.GroupK, CommentAlert.Columns.UsrK), Usr.Current.K)
				)
			);
			q.OrderBy = new OrderBy(
				new OrderBy(GroupUsr.Columns.Favourite, OrderBy.OrderDirection.Descending),
				new OrderBy(GroupUsr.Columns.Owner, OrderBy.OrderDirection.Descending),
				new OrderBy(GroupUsr.Columns.Moderator, OrderBy.OrderDirection.Descending),
				new OrderBy(new Column(GroupUsr.Columns.GroupK, Group.Columns.Name))
			);
			GroupUsrSet vs = new GroupUsrSet(q);
			PanelGroupsList.Visible = vs.Count > 0;
			PanelNoGroups.Visible = vs.Count == 0;
			if (vs.Count > 0)
			{
				GroupsDataGrid.AllowPaging = (vs.Count > GroupsDataGrid.PageSize);
				GroupsDataGrid.DataSource = vs;
				GroupsDataGrid.DataBind();
			}
		}
		public void GroupsDataGridChangePage(object o, DataGridPageChangedEventArgs e)
		{
			GroupsDataGrid.CurrentPageIndex = e.NewPageIndex;
			BindGroups();
		}
		#endregion

		#region PanelInvites
		protected Panel PanelInvites;
		protected DataGrid InvitesDataGrid;
		private void PanelInvites_Load(object sender, System.EventArgs e)
		{
			BindInvites();
		}
		void BindInvites()
		{
			Query q = new Query();
			q.QueryCondition = new And(
				new Q(GroupUsr.Columns.UsrK, Usr.Current.K),
				new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.Invite)
			);
			q.Columns = new ColumnSet(
				GroupUsr.Columns.GroupK,
				GroupUsr.Columns.Status,
				GroupUsr.Columns.StatusChangeDateTime,
				GroupUsr.Columns.InviteMessage,
				GroupUsr.Columns.InviteUsrK,
				new JoinedColumnSet(
					GroupUsr.Columns.InviteUsrK,
					Usr.LinkColumns
				),
				new JoinedColumnSet(
					GroupUsr.Columns.GroupK,
					Group.Columns.Name,
					Group.Columns.TotalMembers,
					Group.Columns.TotalComments,
					Group.Columns.LastPost,
					Group.Columns.UrlName
				)
			);
			q.TableElement = GroupUsr.GroupAndInvitingUsrJoin;
			q.OrderBy = new OrderBy(GroupUsr.Columns.StatusChangeDateTime, OrderBy.OrderDirection.Descending);
			GroupUsrSet gus = new GroupUsrSet(q);
			PanelInvites.Visible = gus.Count > 0;
			if (gus.Count > 0)
			{
				InvitesDataGrid.AllowPaging = (gus.Count > InvitesDataGrid.PageSize);
				InvitesDataGrid.DataSource = gus;
				InvitesDataGrid.DataBind();
			}
		}
		public void InvitesDataGridChangePage(object o, DataGridPageChangedEventArgs e)
		{
			InvitesDataGrid.CurrentPageIndex = e.NewPageIndex;
			BindInvites();
		}

		protected void InvitesDataGridItemCommand(object sender, DataGridCommandEventArgs e)
		{
			if (e.CommandName.Equals("reject"))
			{
				Group g = new Group(int.Parse(e.CommandArgument.ToString()));
				GroupUsr gu = new GroupUsr(Usr.Current.K, g.K);

				if (gu.Status.Equals(GroupUsr.StatusEnum.Invite))
				{
					g.InviteReject(Usr.Current, gu);
				}
				BindInvites();
				//ContainerPage.AnchorSkip("PanelInvites");
			}
		}
		protected void InvitesDataGridItemDataBound(object o, DataGridItemEventArgs e)
		{
			if (e.Item.DataItem is GroupUsr)
			{
				GroupUsr gu = (GroupUsr)e.Item.DataItem;
				e.Item.ID = "GroupK" + gu.GroupK.ToString();
			}
		}
		#endregion

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
			this.Load += new System.EventHandler(this.PanelInvites_Load);
			this.Load += new System.EventHandler(this.GroupsList_Load);
		}
		#endregion
	}
}
