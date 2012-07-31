using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Bobs;

namespace Spotted.Controls
{
	[ClientScript]
	public partial class AddThread : EnhancedUserControl
	{
		public AddThread()
		{
		}

		private void Page_Init(object sender, System.EventArgs e)
		{
			int desiredWidth = 600;
			AddThreadSubjectTextBox.Style["width"] = (desiredWidth - (Vars.IE ? 4 : 2)).ToString() + "px";
			CommentHtml.Width = desiredWidth;
		}
		private void Page_Load(object sender, System.EventArgs e)
		{
			ScriptManager.RegisterClientScriptInclude(this, typeof(Page), "ChatJs", "/misc/chat.js?a=2");
		}

		#region ForumAddThread
		#region Controls
		public TextBox
			AddThreadSubjectTextBox;
		public Html
			CommentHtml;

		#region Advanced controls
		public RadioButton
			AddThreadPublicRadioButton,
			AddThreadPrivateRadioButton,
			AddThreadGroupRadioButton;
		public CheckBox
			AddThreadAdvancedCheckBox,
			AddThreadGroupPrivateCheckBox,
			AddThreadEventCheckBox,
			AddThreadSealedCheckBox,
			AddThreadNewsCheckBox,
			AddThreadInviteCheckBox;
		#endregion

		#endregion
		#region ForumAddThread_PreRender
		public void ForumAddThread_PreRender(object o, System.EventArgs e)
		{
			ContainerPage.ViewStatePublic["CommentDuplicateGuid"] = Guid.NewGuid();
		}
		#endregion
		#region ForumAddThread_Load
		public void ForumAddThread_Load(object o, System.EventArgs eventargs)
		{
			#region Init AddComment/AddThread post boxes for users not logged in etc.
			//if (Usr.Current == null)
			//{
			//    AddThreadNotGroupMemberPanel.Style["display"] = "none";
			//    AddThreadLoginPanel.Style["display"] = "";
			//    AddThreadEmailVerifyPanel.Style["display"] = "none";
			//    CommentHtml.Enabled = false;
			//    AddThreadSubjectTextBox.Enabled = false;
			//    AddThreadSubjectTextBox.Text = "You can't post until you are logged in!";
			//    CommentHtml.Text = "You can't post until you are logged in!";
				
			//    AddThreadAdvancedCheckBox.Style["display"] = "none";


			//    return;
			//}
			//else if (!Usr.Current.IsEmailVerified)
			//{
			//    AddThreadNotGroupMemberPanel.Style["display"] = "none";
			//    AddThreadLoginPanel.Style["display"] = "none";
			//    AddThreadEmailVerifyPanel.Style["display"] = "";
			//    CommentHtml.Enabled = false;
			//    AddThreadSubjectTextBox.Enabled = false;
			//    AddThreadSubjectTextBox.Text = "You can't post until your email address has been verified!";
			//    CommentHtml.Text = "You can't post until your email address has been verified!";
				
			//    AddThreadAdvancedCheckBox.Style["display"] = "none";

			//    return;
			//}
			//else 
			if (
				ThreadParentType.Equals(Model.Entities.ObjectType.Group) &&
				(Usr.Current == null || !CurrentForumCheckPermissionRead || !CurrentForumCheckPermissionPost)
				)
			{
				AddThreadNotGroupMemberPanel.Style["display"] = "";
				AddThreadLoginPanel.Style["display"] = "none";
				AddThreadEmailVerifyPanel.Style["display"] = "none";
				CommentHtml.Enabled = false;
				AddThreadSubjectTextBox.Enabled = false;
				AddThreadSubjectTextBox.Text = "You can't post until you're a member of this group!";
				CommentHtml.Text = "You can't post until you're a member of this group!";
				
				AddThreadAdvancedCheckBox.Style["display"] = "none";

				AddThreadNotGroupMemberGroupPageAnchor.HRef = CurrentGroup.UrlApp("join", "type", ((int)ThreadParentType).ToString(), "k", ObjectK.ToString());

				return;
			}
			#endregion

			AddThreadNotGroupMemberPanel.Style["display"] = "none";
			AddThreadLoginPanel.Style["display"] = "none";
			AddThreadEmailVerifyPanel.Style["display"] = "none";
			CommentHtml.Enabled = true;
			AddThreadSubjectTextBox.Enabled = true;

			if (Usr.Current == null)
				AddThreadAdvancedCheckBox.Style["display"] = "none";

			if (Usr.Current != null)
			{
				#region Send to group drop-down
				if (!Page.IsPostBack)
				{
					Query q = new Query();
					if (ContainerPage.Url.HasPromoterObjectFilter)
					{
						#region Promoter options
						q.TableElement = new Join(
							new TableElement(TablesEnum.Group),
							new TableElement(TablesEnum.Brand),
							QueryJoinType.Inner,
							new And(
								new Q(Group.Columns.BrandK, Brand.Columns.K, true),
								new Q(Brand.Columns.PromoterK, ContainerPage.Url.ObjectFilterPromoter.K),
								new Q(Brand.Columns.PromoterStatus, Brand.PromoterStatusEnum.Confirmed)
							)
						);
						if (ThreadParentType.Equals(Model.Entities.ObjectType.Event))
						{
							//Find a brand that is promoted by this promoter, and attached to this event...
							Query qEv = new Query();
							qEv.TableElement = new Join(
								new TableElement(TablesEnum.Brand),
								new TableElement(TablesEnum.EventBrand),
								QueryJoinType.Inner,
								new And(
									new Q(Brand.Columns.K, EventBrand.Columns.BrandK, true),
									new Q(EventBrand.Columns.EventK, ObjectK),
									new Q(Brand.Columns.PromoterK, ContainerPage.Url.ObjectFilterPromoter.K),
									new Q(Brand.Columns.PromoterStatus, Brand.PromoterStatusEnum.Confirmed)
								)
							);
							qEv.ReturnCountOnly = true;
							BrandSet bs = new BrandSet(qEv);
							if (bs.Count > 0)
							{
								q.TableElement = new Join(
									q.TableElement,
									new TableElement(TablesEnum.EventBrand),
									QueryJoinType.Inner,
									new And(
										new Q(Brand.Columns.K, EventBrand.Columns.BrandK, true),
										new Q(EventBrand.Columns.EventK, ObjectK)
									)
								);
							}
						}
						q.OrderBy = new OrderBy(Group.Columns.TotalMembers, OrderBy.OrderDirection.Descending);
						#endregion
					}
					else
					{
						#region User options
						q.QueryCondition = new And(
							new Q(GroupUsr.Columns.UsrK, Usr.Current.K),
							new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.Member)
							);
						q.OrderBy = new OrderBy(
							new OrderBy(GroupUsr.Columns.Favourite, OrderBy.OrderDirection.Descending),
							new OrderBy(Group.Columns.Name));
						q.TableElement = Group.UsrMemberJoin;
						#endregion
					}
					q.Columns = new ColumnSet(
						Group.Columns.Name,
						Group.Columns.K,
						Group.Columns.BrandK
					);
					GroupSet gs = new GroupSet(q);
					if (gs.Count > 0)
					{
						AddThreadGroupDropDown.DataTextField = "FriendlyName";
						AddThreadGroupDropDown.DataValueField = "K";
						AddThreadGroupDropDown.DataSource = gs;
						AddThreadGroupDropDown.DataBind();
					}
					else
					{
						AddThreadGroupRadioButtonSpan.Style["display"] = "none";
						AddThreadGroupPrivateCheckBoxSpan.Style["display"] = "none";
					}
				}
				#endregion

				if (ThreadParentType.Equals(Model.Entities.ObjectType.Group))
				{
					#region Add thread advanced tick-boxes
					AddThreadAdvancedPanel.Style["display"] = AddThreadAdvancedCheckBox.Checked ? null : "none";
					//				AddThreadAdvancedCheckBox.Attributes["onclick"] = "document.getElementById('" + AddThreadAdvancedPanel.ClientID + "').style.display=this.checked?'':'none';";

					Cambro.Web.Helpers.ChangeState(AddThreadPublicRadioButtonSpan, AddThreadPublicRadioButton, false);
					Cambro.Web.Helpers.ChangeState(AddThreadPrivateRadioButtonSpan, AddThreadPrivateRadioButton, false);

					AddThreadPublicRadioButton.Checked = false;
					AddThreadGroupRadioButton.Checked = true;
					if (AddThreadGroupDropDown.Items.FindByValue(ObjectK.ToString()) != null)
						AddThreadGroupDropDown.SelectedValue = ObjectK.ToString();

					AddThreadGroupDropDown.Enabled = false;

					AddThreadEventCheckBox.Attributes["onclick"] = "EventDropDown.disabled=!this.checked;";
					AddThreadEventDropDown.Enabled = AddThreadEventCheckBox.Checked;
					AddThreadEventDropDown.Attributes["onchange"] = "if(this[this.selectedIndex].value=='0'){this.selectedIndex=this.selectedIndex-1;}";

					Cambro.Web.Helpers.ChangeState(AddThreadSealedCheckBoxSpan, AddThreadSealedCheckBox, false);

					AddThreadInvitePanel.Style["display"] = AddThreadInviteCheckBox.Checked ? null : "none";
					AddThreadInviteCheckBox.Attributes["onclick"] = "InvitePanel.style.display=this.checked?'':'none';";

					#endregion
					#region Event drop-down
					if (!Page.IsPostBack)
					{
						#region Find 10 future events
						Query qFuture = new Query();
						qFuture.Columns = new ColumnSet(
							Event.FriendlyLinkColumns,
							Event.Columns.TotalComments);
						qFuture.TableElement = Event.JoinTo(Event.CountryAllJoin, CurrentGroup);
						qFuture.TopRecords = 10;
						qFuture.QueryCondition = Event.FutureEventsQueryCondition;
						qFuture.OrderBy = Event.FutureEventOrder;
						EventSet esFuture = new EventSet(qFuture);
						#endregion
						#region Find 10 past events
						Query qPast = new Query();
						qPast.Columns = new ColumnSet(
							Event.FriendlyLinkColumns,
							Event.Columns.TotalComments);
						qPast.TableElement = Event.JoinTo(Event.CountryAllJoin, CurrentGroup);
						qPast.TopRecords = 10;
						qPast.QueryCondition = Event.PreviousEventsQueryCondition;
						qPast.OrderBy = Event.PastEventOrder;
						EventSet esPast = new EventSet(qPast);
						#endregion
						#region Add selected events
						if (esFuture.Count > 0 || esPast.Count > 0)
						{
							foreach (Event e in esFuture)
							{
								AddThreadEventDropDown.Items.Insert(0, new ListItem(e.FriendlyName, e.K.ToString()));
							}
							if (esFuture.Count > 0 && esPast.Count > 0)
							{
								AddThreadEventDropDown.Items.Add(new ListItem(" ", "0"));
							}
							foreach (Event e in esPast)
							{
								AddThreadEventDropDown.Items.Add(new ListItem(e.FriendlyName, e.K.ToString()));
							}
						}
						else
						{
							AddThreadEventCheckBoxSpan.Style["display"] = "none";
						}
						#endregion
					}
					#endregion
				}
				else
				{
					#region Add thread advanced tick-boxes


					AddThreadAdvancedPanel.Style["display"] = AddThreadAdvancedCheckBox.Checked ? null : "none";
					//AddThreadAdvancedCheckBox.Attributes["onclick"] = "document.getElementById('" + AddThreadAdvancedPanel.ClientID + "').style.display=this.checked?'':'none';";

					AddThreadPublicRadioButton.Attributes["onclick"] = "PaintAddThread();";

					AddThreadPrivateRadioButton.Attributes["onclick"] = "PaintAddThread();";

					AddThreadGroupRadioButton.Attributes["onclick"] = "PaintAddThread();";
					AddThreadGroupDropDown.Enabled = AddThreadGroupRadioButton.Checked;

					Cambro.Web.Helpers.ChangeState(AddThreadGroupPrivateCheckBoxSpan, AddThreadGroupPrivateCheckBox, AddThreadGroupRadioButton.Checked);
					AddThreadGroupPrivateCheckBox.Attributes["onclick"] = "changedGroupPrivate = false;";

					AddThreadEventCheckBoxSpan.Style["display"] = "none";

					AddThreadNewsCheckBox.Attributes["onclick"] = "changedNews = false;";
					Cambro.Web.Helpers.ChangeState(AddThreadNewsCheckBoxSpan, AddThreadNewsCheckBox, AddThreadGroupRadioButton.Checked);

					AddThreadSealedCheckBox.Attributes["onclick"] = "changedSealed = false;";
					Cambro.Web.Helpers.ChangeState(AddThreadSealedCheckBoxSpan, AddThreadSealedCheckBox, AddThreadPrivateRadioButton.Checked);

					if (AddThreadPrivateRadioButton.Checked)
						AddThreadInviteCheckBox.Checked = true;
					AddThreadInviteCheckBox.Attributes["onclick"] = "changedInvite = false;InvitePanel.style.display=this.checked?'':'none';";
					Cambro.Web.Helpers.ChangeState(AddThreadInviteCheckBoxSpan, AddThreadInviteCheckBox, !AddThreadPrivateRadioButton.Checked);

					AddThreadInvitePanel.Style["display"] = (AddThreadInviteCheckBox.Checked || AddThreadPrivateRadioButton.Checked) ? null : "none";


					#endregion
				}
			}
		}

		public string IsGroupMode
		{
			get
			{
				if (!CurrentForumCheckPermissionRead)
					return "false";

				if (ThreadParentType.Equals(Model.Entities.ObjectType.Group))
					return "true";
				else
					return "false";


			}
		}
		#endregion
		#region AddThreadPost_Click
		public void AddThreadPost_Click(object o, System.EventArgs e)
		{
			if (!CurrentForumCheckPermissionPost)
				return;

			if (Page.IsValid)
			{
				Usr.KickUserIfNotLoggedIn();

				Thread.Maker m = new Thread.Maker();
				m.DuplicateGuid = ContainerPage.ViewStatePublic["CommentDuplicateGuid"];
				m.Subject = AddThreadSubjectTextBox.Text;
				m.Body = CommentHtml.GetHtml();

				#region Work out the parent / group
				if (ThreadParentType.Equals(Model.Entities.ObjectType.Group) &&
					AddThreadAdvancedCheckBox.Checked &&
					AddThreadEventCheckBox.Checked)
				{
					Event ev = new Event(int.Parse(AddThreadEventDropDown.SelectedValue));
					m.ParentType = Model.Entities.ObjectType.Event;
					m.ParentK = ev.K;
					m.GroupK = ObjectK;
				}
				else if (
					!ThreadParentType.Equals(Model.Entities.ObjectType.Group) &&
					AddThreadAdvancedCheckBox.Checked &&
					AddThreadGroupRadioButton.Checked)
				{
					Group g = new Group(int.Parse(AddThreadGroupDropDown.SelectedValue));
					m.ParentType = ThreadParentType;
					m.ParentK = ObjectK;
					m.GroupK = g.K;
				}
				else
				{
					m.ParentType = ThreadParentType;
					m.ParentK = ObjectK;
					if (ThreadParentType.Equals(Model.Entities.ObjectType.Group))
						m.GroupK = ObjectK;
				}
				#endregion

				if (AddThreadAdvancedCheckBox.Checked &&
					AddThreadNewsCheckBox.Checked)
					m.News = true;

				if (AddThreadAdvancedCheckBox.Checked
					&& AddThreadPrivateRadioButton.Checked
					&& AddThreadSealedCheckBox.Checked)
					m.Sealed = true;

				if (AddThreadAdvancedCheckBox.Checked
					&& AddThreadPrivateRadioButton.Checked)
					m.Private = true;

				if (AddThreadAdvancedCheckBox.Checked
					&& (ThreadParentType.Equals(Model.Entities.ObjectType.Group) || AddThreadGroupRadioButton.Checked)
					&& AddThreadGroupPrivateCheckBox.Checked)
					m.Private = true;

				if (AddThreadAdvancedCheckBox.Checked &&
					(AddThreadInviteCheckBox.Checked || AddThreadPrivateRadioButton.Checked))
				{

					m.InviteKs = new List<int>(this.uiMultiBuddyChooser.SelectedUsrKs);
				}

				m.PostingUsr = Usr.Current;
				Thread.MakerReturn r = m.Post();
				if (!r.Success && !r.Duplicate)
					throw new Exception(r.MessageHtml);

				Response.Redirect(r.Thread.Url());

			}
			else
			{
				ContainerPage.AnchorSkip("NewThread");
			}
		}
		#endregion
		#region AppendEventLink
		/// <summary>
		/// This draws event links for brand public chat
		/// </summary>
		void AppendEventLink(StringBuilder sb, bool first, Event e)
		{
			if (!first)
				sb.Append("<br>");
			sb.Append("<a href=\"");
			sb.Append(e.UrlDiscussion());
			sb.Append("\">");
			sb.Append(e.Name);
			sb.Append("</a> <small>@ ");
			sb.Append(e.Venue.Name);
			sb.Append(" in ");
			sb.Append(e.Venue.Place.Name);
			sb.Append(", ");
			sb.Append(e.FriendlyDate(false));
			sb.Append(" - ");
			sb.Append(e.TotalComments.ToString("#,##0"));
			sb.Append(" comment");
			if (e.TotalComments != 1)
				sb.Append("s");
			sb.Append("</small>");

		}
		#endregion
		#endregion

		#region ForceParentType
		public Model.Entities.ObjectType ForceParentType
		{
			get
			{
				return forceParentType;
			}
			set
			{
				forceParent = null;
				HasForceParent = true;
				forceParentType = value;
			}
		}
		private Model.Entities.ObjectType forceParentType;
		#endregion
		#region HasForceParent
		protected bool HasForceParent { get; set; }
		#endregion
		#region ForceParentK
		public int ForceParentK
		{
			get
			{
				return forceParentK;
			}
			set
			{
				forceParent = null;
				HasForceParent = true;
				forceParentK = value;
			}
		}
		private int forceParentK;
		#endregion
		#region ForceParent
		public IBob ForceParent
		{
			get
			{
				if (forceParent == null && HasForceParent)
					forceParent = Bob.Get(ForceParentType, ForceParentK);
				return forceParent;
			}
			set
			{
				forceParent = value;
			}
		}
		private IBob forceParent;
		#endregion

		#region ThreadParentType
		Model.Entities.ObjectType ThreadParentType
		{
			get
			{
				if (HasForceParent)
					return ForceParentType;
				else if (ContainerPage.Url.HasGroupLogicalFilter)
					return Model.Entities.ObjectType.Group;
				else if (ContainerPage.Url.HasObjectFilter && ContainerPage.Url.ObjectFilterBob is IDiscussable)
					return ContainerPage.Url.ObjectFilterType;
				else
					return Model.Entities.ObjectType.None;
			}
		}
		#endregion
		#region ObjectK
		int ObjectK
		{
			get
			{
				if (HasForceParent)
					return ForceParentK;
				else if (ContainerPage.Url.HasGroupLogicalFilter)
					return ContainerPage.Url.LogicalFilterGroupK;
				else if (ContainerPage.Url.HasObjectFilter && ContainerPage.Url.ObjectFilterBob is IDiscussable)
					return ContainerPage.Url.ObjectFilterK;
				else
					return 0;
			}
		}
		#endregion
		#region CurrentGroup
		Group CurrentGroup
		{
			get
			{
				if (!doneCurrentGroup)
				{
					if (ThreadParentType.Equals(Model.Entities.ObjectType.Group))
					{
						currentGroup = new Group(ObjectK);
					}
					doneCurrentGroup = true;
				}
				return currentGroup;
			}
		}
		bool doneCurrentGroup = false;
		Group currentGroup;
		#endregion
		#region CurrentGroupUsr
		GroupUsr CurrentGroupUsr
		{
			get
			{
				if (!doneCurrentGroupUsr)
				{
					if (ThreadParentType.Equals(Model.Entities.ObjectType.Group))
					{
						currentGroupUsr = CurrentGroup.GetGroupUsr(Usr.Current);
					}
					doneCurrentGroupUsr = true;
				}
				return currentGroupUsr;
			}
		}
		bool doneCurrentGroupUsr = false;
		GroupUsr currentGroupUsr;
		#endregion

		#region CurrentForumCheckPermissionRead
		bool CurrentForumCheckPermissionRead
		{
			get
			{
				if (!currentForumCheckPermissionReadSet)
				{
					if (ThreadParentType.Equals(Model.Entities.ObjectType.Group))
						currentForumCheckPermissionRead = CurrentGroup.CanRead(Usr.Current, CurrentGroupUsr);
					else
						currentForumCheckPermissionRead = true;

					currentForumCheckPermissionReadSet = true;
				}
				return currentForumCheckPermissionRead;
			}
		}
		bool currentForumCheckPermissionRead;
		bool currentForumCheckPermissionReadSet = false;
		#endregion
		#region CurrentForumCheckPermissionPost
		bool CurrentForumCheckPermissionPost
		{
			get
			{
				if (!currentForumCheckPermissionPostSet)
				{
					if (ThreadParentType.Equals(Model.Entities.ObjectType.Group))
						currentForumCheckPermissionPost = CurrentGroup.IsMember(CurrentGroupUsr);
					else
						currentForumCheckPermissionPost = Usr.Current != null;

					currentForumCheckPermissionPostSet = true;
				}
				return currentForumCheckPermissionPost;
			}
		}
		bool currentForumCheckPermissionPost;
		bool currentForumCheckPermissionPostSet = false;
		#endregion

		#region ContainerPage
		public Spotted.Master.DsiPage ContainerPage
		{
			get
			{
				return (Spotted.Master.DsiPage)Page;
			}
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
			this.PreRender += new System.EventHandler(this.ForumAddThread_PreRender);
			this.Load += new System.EventHandler(this.ForumAddThread_Load);
		}
		#endregion
	}
}
