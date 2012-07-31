using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs;
using SpottedLibrary.Controls.LatestChat;
using System.Web.UI.HtmlControls;

namespace Spotted.Controls
{
	[ClientScript]
	public partial class LatestChat : EnhancedUserControl, ILatestChat, IIncludesJs
	{
		public LatestChat()
		{
		}

		public void IncludeJsInternal() { IncludeJs(this.Page); }
		public static void IncludeJs(Page page)
		{
			ScriptSharp.RegisterInclude(page, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		}

		public bool HotTopicsOnly { get; set; }

		public string DbButtonPrefix = "a";

		Spotted.Master.DsiPage ContainerPage
		{
			get
			{
				return (Spotted.Master.DsiPage)Page;
			}
		}
		protected void Page_PreRender(object sender, EventArgs e)
		{
		
			if (CurrentForumCheckPermissionRead)
			{
				ThreadsNoPermissionPanel.Visible = false;
				BindThreads();
			}
			else
			{
				ThreadsNoPermissionPanel.Visible = true;
				ThreadsPanel.Visible = false;
				ThreadsNoPermissionJoinAnchor.HRef = CurrentGroup.UrlApp("join");
			}

			this.uiThreadsCount.Value = ThreadsCount.ToString();
			this.uiHasGroupObjectFilter.Value = ((Spotted.Master.DsiPage)Page).Url.HasGroupObjectFilter.ToString();

			this.Header.Visible = ShowHolder;
			if (ShowHolder)
				this.InnerHolder.Attributes["class"] = "ContentBorder";
		}

		public HtmlContainerControl ExternalHeader;
		public Panel ExternalHolder;

		#region HasContent
		public bool HasContent
		{
			get { return ThreadsPanel.Visible; }
		}
		#endregion

		#region CurrentForumCheckPermissionRead
		public bool CurrentForumCheckPermissionRead
		{
			get
			{
				if (!currentForumCheckPermissionReadSet)
				{
					if (Discussable != null && Discussable.ObjectType == Model.Entities.ObjectType.Group)
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
		#region CurrentGroup
		Group CurrentGroup
		{
			get
			{
				if (!doneCurrentGroup)
				{
					if (Discussable.ObjectType == Model.Entities.ObjectType.Group)
					{
						currentGroup = (Group)Discussable;
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
					if (CurrentGroup != null)
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
		#endregion

		#region Discussable
		IDiscussable discussable;
		public IDiscussable Discussable
		{
			set
			{
				if (value != null)
				{
					uiObjectK.Value = value.K.ToString();
					uiObjectType.Value = ((int)value.ObjectType).ToString();
				}

				threads = null;
				discussable = value;
			}
			get
			{
				if (discussable == null)
				{
					try
					{
						if (uiObjectType.Value != "" && uiObjectK.Value != "")
						{
							discussable = Bob.Get((Model.Entities.ObjectType)int.Parse(uiObjectType.Value), int.Parse(uiObjectK.Value)) as IDiscussable;
						}
					}
					catch { }
				}
				return discussable;
			}
		}
		#endregion

		public int Items { get; set; }
		public bool ShowHolder { get; set; }

		#region ThreadsPanel
		#region ThreadsCount
		int ThreadsCount
		{
			get
			{
				return (int)(Items * 1.5);
			}
		}
		#endregion
		#region BindThreads()
		void BindThreads()
		{
			if (Threads.Count > 0)
			{
				ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "DbButtonInit", "DbButtonInit(" + Bobs.Vars.LanguageString + ");", true);
			}
			else
			{
				ThreadsPanel.Style["display"] = "none";
			}

			BrandChatControlsP.Visible = Discussable != null && (Discussable.UsedDiscussable.ObjectType == Model.Entities.ObjectType.Brand && ((Brand)Discussable.UsedDiscussable).TotalComments > 0 && ((Brand)Discussable.UsedDiscussable).Group.TotalComments > 0);
			if (Discussable != null && (Discussable.UsedDiscussable.ObjectType == Model.Entities.ObjectType.Brand))
			{
				if (((Brand)Discussable.UsedDiscussable).BrandPageShowEventChat)
				{
					ShowGroupChatLinkButton.Visible = true;
					ShowGroupChatEnabled.Visible = false;

					ShowBrandChatLinkButton.Visible = false;
					ShowBrandChatEnabled.Visible = true;
				}
				else
				{
					ShowGroupChatLinkButton.Visible = false;
					ShowGroupChatEnabled.Visible = true;

					ShowBrandChatLinkButton.Visible = true;
					ShowBrandChatEnabled.Visible = false;
				}
			}


			if (Discussable != null && (Discussable.ObjectType == Model.Entities.ObjectType.Photo))
			{
				if (Header != null)
				{
					Header.Visible = true;
					Header.InnerText = "More topics about this photo";
				}
				if (ExternalHeader != null)
				{
					ExternalHeader.Visible = true;
					ExternalHeader.InnerText = "More topics about this photo";
				}
				CommentsFooter.Visible = false;
			}
			else if (Discussable != null && (Discussable.ObjectType == Model.Entities.ObjectType.Article))
			{
				if (Header != null)
				{
					Header.Visible = true;
					Header.InnerText = "More topics about this article";
				}
				if (ExternalHeader != null)
				{
					ExternalHeader.Visible = true;
					ExternalHeader.InnerText = "More topics about this article";
				}
				CommentsFooter.Visible = false;
			}

			if (Threads.Count != 0)
			{
				ThreadsDataGrid.DataSource = Threads;
				ThreadsDataGrid.DataBind();

				if (Discussable == null)
				{
					if (Threads.Count == ThreadsCount)
					{
						MoreThreadsAnchor.HRef = "/chat";
					}
				}
				else
				{
					IDiscussable discuss = null;

					if (Discussable.UsedDiscussable.ObjectType == Model.Entities.ObjectType.Brand)
					{
						if (((Brand)Discussable.UsedDiscussable).BrandPageShowEventChat)
							discuss = Discussable.UsedDiscussable;
						else
							discuss = (IDiscussable)((Brand)Discussable.UsedDiscussable).Group;
					}
					else
					{
						discuss = Discussable.UsedDiscussable;
					}

					if (Threads.Count == ThreadsCount)
					{
						MoreThreadsAnchor.HRef = discuss.UrlDiscussion();
						if (discuss.TotalComments > 0)
							MoreThreadsCountLabel.Text = " - " + discuss.TotalComments.ToString("#,##0") + " comment" + (discuss.TotalComments == 1 ? "" : "s");
					}
				}

				if (Threads.Count != ThreadsCount || HotTopicsOnly)
					CommentsFooter.Visible = false;

			}
			else
			{
				if (Discussable != null && Discussable.OnlyShowThreads && Holder != null)
					this.Holder.Style["display"] = "none";

				if (Discussable != null && Discussable.OnlyShowThreads && ExternalHolder != null)
					this.ExternalHolder.Visible = false;
			}
		}
		#endregion
		#region Threads
		ThreadSet Threads
		{
			get
			{
				if (threads == null)
				{
					threads = Thread.GetThreadsByIDiscussable(Discussable, ThreadsCount, HotTopicsOnly);
				}
				return threads;
			}
			set
			{
				threads = value;
			}
		}
		ThreadSet threads;
		#endregion

		#region ShowGroupChat_Click
		protected void ShowGroupChat_Click(object sender, EventArgs eventArgs)
		{
			Prefs.Current["BrandChat"] = "Group";
			//BindThreads();
			ContainerPage.AnchorSkip("LatestBoxAnchor");
		}
		#endregion
		#region ShowBrandChat_Click
		protected void ShowBrandChat_Click(object sender, EventArgs eventArgs)
		{
			Prefs.Current["BrandChat"] = "Brand";
			//BindThreads();
			ContainerPage.AnchorSkip("LatestBoxAnchor");
		}
		#endregion
		#endregion
	}
}
