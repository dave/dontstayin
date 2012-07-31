using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Bobs;
using System.Text;
using SpottedLibrary.Controls.ThreadControl;
using Common;
using Spotted.Templates.Comments;
using System.Collections.Generic;

namespace Spotted.Controls
{
	[ClientScript]
	public partial class ThreadControl : EnhancedUserControl, IThreadControl, ICommentsPage, IIncludesJs
	{
		ThreadControlService service;
		public ThreadControl()
		{

			service = new ThreadControlService();
			this.Load += new System.EventHandler(this.ThreadComments_Load);
			this.Load += new System.EventHandler(this.ForumAddThread_Load);
		}
		
		public void IncludeJsInternal() { IncludeJs(this.Page); }
		public static void IncludeJs(Page page)
		{
			Spotted.Controls.CommentsDisplay.IncludeJs(page);

			ScriptSharp.RegisterInclude(page, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		}

		public Spotted.Master.DsiPage ContainerPage
		{
			get { return (Spotted.Master.DsiPage)Page; }
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				this.Visible = HasParentObject;
			}

			

			ScriptManager.RegisterClientScriptInclude(this, typeof(Page), "ChatJs", "/misc/chat.js?a=2");
			ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "DbButtonInit", "DbButtonInit(" + Bobs.Vars.LanguageString + ");", true);
		}

		bool HasParentObject
		{
			get
			{
				return ParentObjectK != null;
			}
		}

		public void Page_PreRender(object o, System.EventArgs e)
		{
			((GenericPage)Page).ViewStatePublic["CommentDuplicateGuid"] = Guid.NewGuid();
		}

		public string ParentObjectTypeString(bool Capital)
		{
			if (!HasParentObject)
				return "";

			if (Capital)
				return ((IBobType)ParentObject).TypeName;
			else
				return ((IBobType)ParentObject).TypeName.ToLower();
		}


		#region ThreadComments_Load
		public void ThreadComments_Load(object o, System.EventArgs e)
		{
			if ((ThreadK ?? 0) == 0)
			{
				this.uiCommentsDisplay.HideCommentsPanel();
				this.uiCommentsDisplay.HideInitialCommentPanel();
				ThreadFavouriteButtonP.Visible = false;
			}
			else
			{
				BindComments();
			}
		}

		#region BindComments()
		public void BindComments()
		{
			if (!CurrentThread.CheckPermissionRead(Usr.Current))
				return;

			if (CurrentThread.TotalComments == 0)
			{
				this.uiCommentsDisplay.HideCommentsPanel();
				ThreadFavouriteButtonP.Visible = false;
			}
			else
			{
				ThreadFavouriteButtonP.Visible = true;
				// AddThreadInviteDrop.ThreadK = ThreadK.Value;

				this.uiCommentsDisplay.CurrentThread = CurrentThread;
				this.uiCommentsDisplay.CurrentThreadUsr = CurrentThreadUsr;
				this.uiCommentsDisplay.ParentObject = ParentObject;
				
				this.uiCommentsDisplay.CurrentThread = CurrentThread;
				this.uiCommentsDisplay.CurrentThreadUsr = CurrentThreadUsr;
				this.uiCommentsDisplay.ParentObject = ParentObject;

				this.DataBind();
			}

		}
		#endregion

		#endregion

		#region ForumAddThread
		#region ThreadFavouriteButton
		protected string ThreadFavouriteButtonThreadK
		{
			get
			{
				if ((ThreadK ?? 0) == 0)
					return "0";
				else
					return ThreadK.ToString();
			}
		}
		protected string ThreadFavouriteButtonState
		{
			get
			{
				if ((ThreadK ?? 0) == 0)
					return "0";
				else
				{
					return (CurrentThreadUsr != null && CurrentThreadUsr.Favourite) ? "1" : "0";
				}
			}
		}
		#endregion
		#region ThreadWatchButton
		protected string ThreadWatchButtonArgs
		{
			get
			{
				if (!HasParentObject)
					return "";
				else
					return ((int)ParentObjectType).ToString() + "," + ParentObjectK.ToString();
			}
		}
		protected string ThreadWatchButtonState
		{
			get
			{
				if (!HasParentObject)
					return "0";

				if (Usr.Current == null)
					return "0";

				bool found = false;

				if ((ThreadK ?? 0) == 0)
				{
					found = CommentAlert.IsEnabled(Usr.Current.K, ParentObjectK.Value, ParentObjectType.Value);
				}
				else
				{
					found = CommentAlert.IsEnabled(Usr.Current.K, ParentObjectK.Value, ParentObjectType.Value);

					if (CommentAlert.IsEnabled(Usr.Current.K, CurrentThread.K, Model.Entities.ObjectType.Thread))
						found = true;
				}
				return found ? "1" : "0";
			}
		}
		#endregion
		#region ForumAddThread_Load
		public void ForumAddThread_Load(object o, System.EventArgs eventargs)
		{


			#region Init AddComment/AddThread post boxes for users not logged in etc.
			//if (Usr.Current == null)
			//{
			//    CommentLoginPanel.Visible = true;
			//    CommentEmailVerifyPanel.Visible = false;
			//    CommentGroupMemberPanel.Visible = false;
			//    CommentHtml.Enabled = false;
			//    CommentHtml.Text = "You can't post until you are logged in!";

			//    AddThreadAdvancedPanel.Visible = false;
			//    AddThreadAdvancedCheckBox.Visible = false;

			//    return;
			//}
			//else if (!Usr.Current.IsEmailVerified)
			//{
			//    CommentLoginPanel.Visible = false;
			//    CommentEmailVerifyPanel.Visible = true;
			//    CommentGroupMemberPanel.Visible = false;
			//    CommentHtml.Enabled = false;
			//    CommentHtml.Text = "You can't post until your email address has been verified!";

			//    AddThreadAdvancedPanel.Visible = false;
			//    AddThreadAdvancedCheckBox.Visible = false;

			//    return;
			//}
			//else 
				if (CurrentThread != null && CurrentThread.GroupK > 0)
			{
				GroupUsr gu = CurrentThread.Group.GetGroupUsr(Usr.Current);
				if (!CurrentThread.Group.IsMember(gu))
				{
					CommentLoginPanel.Visible = false;
					CommentEmailVerifyPanel.Visible = false;
					CommentGroupMemberPanel.Visible = true;
					CommentGroupMemberAnchor.HRef = CurrentThread.Group.UrlApp("join", "type", ((int)ParentObjectType).ToString(), "k", ParentObjectK.ToString());

					CommentHtml.Enabled = false;
					CommentHtml.Text = "You can't post until you're a member of this group!";

					AddThreadAdvancedPanel.Visible = false;
					AddThreadAdvancedCheckBox.Visible = false;
					return;

				}
				else
				{
					CommentLoginPanel.Visible = false;
					CommentEmailVerifyPanel.Visible = false;
					CommentGroupMemberPanel.Visible = false;
					CommentHtml.Enabled = true;
				}
			}
			else
			{
				CommentLoginPanel.Visible = false;
				CommentEmailVerifyPanel.Visible = false;
				CommentGroupMemberPanel.Visible = false;
				CommentHtml.Enabled = true;
			}
			#endregion

			
			SetUpAddThreadAdvancedPanel(false);

		}
		void SetUpAddThreadAdvancedPanel(bool Reset)
		{
			if (Usr.Current == null)
			{
				AddThreadAdvancedCheckBoxP.Style["display"] = "none";
				AddThreadAdvancedPanel.Style["display"] = "none";
				return;
			}

			if (!HasParentObject)
				return;

			#region Add thread advanced tick-boxes

		//	AddThreadAdvancedPanel.Visible = AddThreadAdvancedCheckBox.Checked;
			//AddThreadAdvancedPanel.Style["display"] = AddThreadAdvancedCheckBox.Checked ? null : "none";
			//AddThreadAdvancedCheckBox.Attributes["onclick"] = "document.getElementById('" + AddThreadAdvancedPanel.ClientID + "').style.display=this.checked?'':'none';";

			//PublicRadioButton
			AddThreadPublicRadioButtonSpan.Style["display"] = (ThreadK ?? 0) == 0 ? "none" : null;
			if (ThreadK > 0 && (!Page.IsPostBack || Reset))
			{
				AddThreadPublicRadioButton.Checked = true;
				AddThreadNewPublicRadioButton.Checked = false;
			}
			AddThreadPublicRadioButton.Attributes["onclick"] = "PaintThreadControl();";

			//NewPublicRadioButton
			if ((ThreadK ?? 0) == 0)
				AddThreadNewPublicRadioButton.Text = "Public topic <small>- post a normal public topic</small><br>";
			else
				AddThreadNewPublicRadioButton.Text = "New public topic <small>- start a new public topic about this " + ParentObjectTypeString(false) + "</small><br>";

			if ((ThreadK ?? 0) == 0 && (!Page.IsPostBack || Reset))
			{
				AddThreadPublicRadioButton.Checked = false;
				AddThreadNewPublicRadioButton.Checked = true;
			}

			AddThreadNewPublicRadioButton.Attributes["onclick"] = "PaintThreadControl();";

			//PrivateRadioButton
			if ((ThreadK ?? 0) == 0)
				AddThreadPrivateRadioButton.Text = "Private topic <small>- send a private message about this " + ParentObjectTypeString(false) + "</small><br>";
			else
				AddThreadPrivateRadioButton.Text = "New private topic <small>- send a private message about this " + ParentObjectTypeString(false) + "</small><br>";
			AddThreadPrivateRadioButton.Attributes["onclick"] = "PaintThreadControl();";

			//GroupRadioButton
			if ((ThreadK ?? 0) == 0)
				AddThreadGroupRadioButton.Text = "Start this topic in a group: ";
			else
				AddThreadGroupRadioButton.Text = "New topic in a group: ";
			AddThreadGroupRadioButton.Attributes["onclick"] = "PaintThreadControl();";
			AddThreadGroupDropDown.Enabled = AddThreadGroupRadioButton.Checked;

			//GroupPrivateCheckBox
			Cambro.Web.Helpers.ChangeState(AddThreadGroupPrivateCheckBoxSpan, AddThreadGroupPrivateCheckBox, AddThreadGroupRadioButton.Checked);
			AddThreadGroupPrivateCheckBox.Attributes["onclick"] = "changedGroupPrivate = false;";

			//NewsCheckBox
			AddThreadNewsCheckBox.Attributes["onclick"] = "changedNews = false;";
			Cambro.Web.Helpers.ChangeState(AddThreadNewsCheckBoxSpan, AddThreadNewsCheckBox, AddThreadGroupRadioButton.Checked);

			//SealedCheckBox
			AddThreadSealedCheckBox.Attributes["onclick"] = "changedSealed = false;";
			Cambro.Web.Helpers.ChangeState(AddThreadSealedCheckBoxSpan, AddThreadSealedCheckBox, AddThreadPrivateRadioButton.Checked);

			//InviteCheckBox
			if (AddThreadPrivateRadioButton.Checked)
				AddThreadInviteCheckBox.Checked = true;
			AddThreadInviteCheckBox.Attributes["onclick"] = "changedInvite = false;InvitePanel.style.display=this.checked?'':'none';";
			Cambro.Web.Helpers.ChangeState(AddThreadInviteCheckBoxSpan, AddThreadInviteCheckBox, !AddThreadPrivateRadioButton.Checked);

			AddThreadInvitePanel.Style["display"] = (AddThreadInviteCheckBox.Checked || AddThreadPrivateRadioButton.Checked) ? null : "none";

			#endregion
			#region Send to group drop-down
			if (!Page.IsPostBack)
			{
				GroupSet gs = service.GetUsrsGroups(Usr.Current.K);
				if (gs.Count > 0)
				{
					AddThreadGroupDropDown.DataTextField = "Name";
					AddThreadGroupDropDown.DataValueField = "K";
					AddThreadGroupDropDown.DataSource = gs;
					AddThreadGroupDropDown.DataBind();
				}
				else
				{
					AddThreadGroupRadioButtonSpan.Visible = false;
					AddThreadGroupPrivateCheckBoxSpan.Visible = false;
				}
			}
			#endregion
		}

		#endregion
		#region AddThreadAdvancedCheckBox_CheckChanged
		protected void AddThreadAdvancedCheckBox_CheckChanged(object sender, EventArgs eventArgs)
		{
			AddThreadAdvancedPanel.Visible = AddThreadAdvancedCheckBox.Checked;

		}
		#endregion
		#endregion

		#region ThreadK and CurrentThread
		public int? ThreadK
		{
			get
			{
				int threadK;
				if (int.TryParse(uiThreadK.Value, out threadK))
					return threadK;
				else
					return null;
			}
			set
			{
				CurrentThread = null;
				uiThreadK.Value = value.HasValue ? value.Value.ToString() : "";
			}
		}

		public Thread CurrentThread
		{
			get
			{
				try
				{
					if ((ThreadK ?? 0) == 0)
						return null;
				}
				catch
				{
					return null;
				}
				if (currentThread == null)
					currentThread = new Thread(ThreadK.Value);
				return currentThread;
			}
			set
			{
				currentThread = value;
			}
		}
		Thread currentThread;

		#endregion

		#region CurrentThreadGroupUsr
		public GroupUsr CurrentThreadGroupUsr
		{
			get
			{
				if (!currentThreadGroupUsrDone && CurrentThread != null && CurrentThread.GroupK > 0)
				{
					currentThreadGroupUsr = CurrentThread.Group.GetGroupUsr(Usr.Current);
					currentThreadGroupUsrDone = true;
				}
				return currentThreadGroupUsr;
			}
			set
			{
				currentThreadGroupUsr = value;
			}
		}
		bool currentThreadGroupUsrDone = false;
		GroupUsr currentThreadGroupUsr;
		#endregion

		#region CurrentThreadUsr
		public ThreadUsr CurrentThreadUsr
		{
			get
			{
				if (!doneThreadUsr && currentThreadUsr == null && CurrentThread != null && Usr.Current != null)
				{
					if (CurrentThread.CheckPermissionRead(Usr.Current))
					{
						try
						{
							doneThreadUsr = true;
							currentThreadUsr = new ThreadUsr(CurrentThread.K, Usr.Current.K);
						}
						catch
						{
						}
					}
				}
				return currentThreadUsr;
			}
			set
			{
				currentThreadUsr = value;
				doneThreadUsr = false;
			}
		}
		private ThreadUsr currentThreadUsr;
		private bool doneThreadUsr = false;
		#endregion

		#region ParentObjectType, ParentObjectK
		public Model.Entities.ObjectType? ParentObjectType
		{
			get
			{
				int parentObjectType;
				if (int.TryParse(uiParentObjectType.Value, out parentObjectType)) return (Model.Entities.ObjectType)parentObjectType;
				else return null;
			}
			set
			{
				ParentObject = null;
				uiParentObjectType.Value = value.HasValue ? ((int)value.Value).ToString() : "";
			}
		}
		public int? ParentObjectK
		{
			get
			{
				int parentObjectK;
				if (int.TryParse(uiParentObjectK.Value, out parentObjectK)) return parentObjectK;
				else return null;
			}
			set
			{
				ParentObject = null;
				uiParentObjectK.Value = value.HasValue ? value.Value.ToString() : "";
				this.Visible = value != null;
			}
		}
		#region ParentObject
		public IDiscussable ParentObject
		{
			get
			{
				if (parentObject == null && HasParentObject)
				{
					if (ContainerPage.Url.ObjectFilterType.Equals(ParentObjectType) && ContainerPage.Url.ObjectFilterK == ParentObjectK)
						return (IDiscussable)ContainerPage.Url.ObjectFilterBob;
					else
						parentObject = (IDiscussable)Bob.Get(ParentObjectType.Value, ParentObjectK.Value);
				}
				return parentObject;
			}
			set
			{
				parentObject = value;
			}
		}
		IDiscussable parentObject;
		#endregion
		#endregion

		#region CommentPost
		public void CommentPost(object o, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				Guid duplicateGuid = (Guid)((GenericPage)Page).ViewStatePublic["CommentDuplicateGuid"];
				string commentHtml = CommentHtml.GetHtml();

				List<int> invitedUsrKs = new List<int>();

				if (AddThreadAdvancedCheckBox.Checked && AddThreadInviteCheckBox.Checked)
				{
					invitedUsrKs = this.uiMultiBuddyChooser.SelectedUsrKs.ToList();
				}

				if (!AddThreadAdvancedCheckBox.Checked || AddThreadPublicRadioButton.Checked || AddThreadNewPublicRadioButton.Checked)
				{
					if ((ThreadK ?? 0) == 0)
					{
						#region Start a public thread

						Thread.MakerReturn r = Thread.CreatePublicThread(ParentObject, duplicateGuid, commentHtml,
							AddThreadAdvancedCheckBox.Checked && AddThreadNewsCheckBox.Checked, invitedUsrKs, false);


						if (r.Success)
						{
							this.ThreadK = r.Thread.K;
						}
						else if (r.Duplicate)
							this.ThreadK = r.Thread.K;
						else
							return;

						CommentHtml.Clear();
						CurrentThread = null;
						BindComments();
						SetUpAddThreadAdvancedPanel(true);
						((Spotted.Master.DsiPage)Page).AnchorSkip("Comments");
						#endregion
					}
					else if (AddThreadAdvancedCheckBox.Checked && AddThreadNewPublicRadioButton.Checked)
					{
						#region Start a NEW public thread
						Thread.MakerReturn r = Thread.CreateNewPublicThread(ParentObject, duplicateGuid, commentHtml, AddThreadNewsCheckBox.Checked,
							invitedUsrKs);

						if (r.Success || r.Duplicate)
							Response.Redirect(r.Thread.Url());
						else
							throw new Exception(r.MessageHtml);
						#endregion
					}
					else
					{
						#region Reply to this thread
						Comment.MakerReturn r = Thread.CreateReply(CurrentThread, duplicateGuid, commentHtml, invitedUsrKs);

						if (r.Success)
						{
							if (ParentObject != null)
							{
								ParentObject.UpdateTotalComments(null);
							}
						}
						else if (!r.Duplicate)
							return;

						CommentHtml.Clear();
						CurrentThread = null;
						if (r.Comment != null && r.Comment.Page != CurrentPage)
						{
							CurrentPage = r.Comment.Page;
						}
						BindComments();
						if (r.Comment != null)
							((Spotted.Master.DsiPage)Page).AnchorSkip("CommentK-" + r.Comment.K.ToString());
						else
							((Spotted.Master.DsiPage)Page).AnchorSkip("Comments");
						#endregion
					}

				}
				else if (AddThreadAdvancedCheckBox.Checked && AddThreadPrivateRadioButton.Checked)
				{
					#region Add a private thread
					
					Thread.MakerReturn r = Thread.CreatePrivateThread(ParentObject, duplicateGuid, commentHtml, invitedUsrKs, AddThreadSealedCheckBox.Checked);

					if (r.Success || r.Duplicate)
						Response.Redirect(r.Thread.Url());
					else
						throw new Exception(r.MessageHtml);
					#endregion
				}
				else if (AddThreadAdvancedCheckBox.Checked && AddThreadGroupRadioButton.Checked)
				{
					#region Add a new thread in a group
				
					int groupK = int.Parse(AddThreadGroupDropDown.SelectedValue);
					Thread.MakerReturn r = Thread.CreateNewThreadInGroup(groupK,
						ParentObject, duplicateGuid, commentHtml, AddThreadNewsCheckBox.Checked, invitedUsrKs, AddThreadGroupPrivateCheckBox.Checked);

					if (r.Success || r.Duplicate)
						Response.Redirect(r.Thread.Url());
					else
						throw new Exception(r.MessageHtml);
					#endregion
				}
			}
			else
			{
				((Spotted.Master.DsiPage)Page).AnchorSkip("PostComment");
			}
		}
		#endregion

		#region IThreadControl Members

		public int CurrentPage
		{
			get {return this.uiCommentsDisplay.CurrentPage; }
			set { this.uiCommentsDisplay.CurrentPage = value; }
		}

		public void Initialise()
		{
			ThreadComments_Load(null, null);
		}

		#endregion
	}
}
