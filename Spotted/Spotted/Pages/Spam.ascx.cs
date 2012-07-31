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
using Common;

namespace Spotted.Pages
{
	public partial class Spam : DsiUserControl
	{
		//protected string JavascriptConfirmArchive = "return confirm('Are you sure you want to remove these topics from your inbox?\nYou can still view them by click \\\'Watching\\\'.')";
		//protected string JavascriptConfirmIgnore = "return confirm('Are you sure you want to ignore these topics in your inbox?\nThey will be removed from your \\\'Inbox\\\' and \\\'Watching\\\'.')";

		private const string PROCESSING_HTML = "<nobr><small><b>Processing</b></small> <img src=\"/gfx/animated-processing.gif\" border=\"0\" height=\"16\" width=\"16\" align=\"absmiddle\" /></nobr>";
		private const string REMOVE_FROM_INBOX = "Remove all from inbox";
		private const string SMART_DELETE = "Smart delete*";
		private const string IGNORE_ALL = "Ignore all";
		private const string EXIT_GROUP = "Exit group";
		private const string STOP_WATCHING = "Stop watching";


		//int preventInfiniteLoopCounter = 0;
		protected void Page_Load(object sender, EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn("You must be logged in to view this page");
			if (!this.IsPostBack)
			{
				BindPanels();
			}
		}

		private void BindPanels()
		{
			BindInvitePanel();
			BindEventWatchPanel();
			BindVenueWatchPanel();
			BindBrandWatchPanel();
			BindGroupWatchPanel();
			BindGroupNewsPanel();
			BindWatchPhotoPanel();
			BindWatchArticlePanel();
			BindWatchOtherThreadPanel();

			InboxSpamPanel.Visible = InvitePanel.Visible || EventWatchPanel.Visible || VenueWatchPanel.Visible || BrandWatchPanel.Visible || GroupWatchPanel.Visible || GroupNewsPanel.Visible || WatchArticlePanel.Visible || WatchPhotoPanel.Visible || WatchOtherThreadPanel.Visible;
			NoInboxSpamPanel.Visible = !InboxSpamPanel.Visible;
		}

		#region Status Message
		public void SuccessfulStatusMessage()
		{
			//StatusMessage("Spam removal successful!");
			//StatusMessage("Spam removal processing . . .");
		}
		public void SuccessfulStatusMessage(IBobType bobType)
		{
			//SuccessfulStatusMessage(bobType, false, false);
		}
		public void SuccessfulStatusMessage(IBobType bobType, bool isIgnoreAll)
		{
			//SuccessfulStatusMessage(bobType, isIgnoreAll, false);
		}
		public void SuccessfulStatusMessage(IBobType bobType, bool isIgnoreAll, bool isNews)
		{
			//StatusMessage("You have " + (isIgnoreAll ? "ignored" : "removed") + " all inbox " + (isNews ? "news " : "") + "spam from " + bobType.TypeName.ToLower() + ": " + bobType.Name);
		}
		public void ErrorStatusMessage()
		{
			ErrorStatusMessage(new Exception());
		}
		public void ErrorStatusMessage(Exception ex)
		{
			if (!errorMessageFlag)
			{
				string message = "An error occurred. ";
				if (ex is DsiUserFriendlyException)
				{
					message += ex.Message + " ";
				}

				StatusMessage(message + "Please refresh the page and try again. If the problem persists, contact an administrator.");
				errorMessageFlag = true;
			}
		}
		public void StatusMessage(string message)
		{
			//StatusPlaceHolder.Controls.Clear();
			StatusPlaceHolder.Controls.Add(new LiteralControl("<script language=\"javascript\">alert('"+message.Replace("'","\'")+"');</script>"));
		}
		private bool errorMessageFlag = false;
		#endregion

		#region InvitePanel
		protected void BindInvitePanel()
		{
			try
			{
				Query q = new Query();
				q.GroupBy = new GroupBy(new GroupBy(ThreadUsr.Columns.InvitingUsrK), new GroupBy(ThreadUsr.Columns.StatusChangeObjectType), new GroupBy(ThreadUsr.Columns.StatusChangeObjectK), new GroupBy(ThreadUsr.Columns.Status));
				q.ExtraSelectElements.Add("count", "COUNT(*)");
				q.QueryCondition = new And(new Q(ThreadUsr.Columns.UsrK, Usr.Current.K),
										   new Q(ThreadUsr.Columns.InvitingUsrK, QueryOperator.NotEqualTo, Usr.Current.K),
										   new Q(ThreadUsr.Columns.InvitingUsrK, QueryOperator.NotEqualTo, 0),
										   new Q(ThreadUsr.Columns.Status, ThreadUsr.StatusEnum.NewInvite));

				q.TableElement = new Join(new Join(ThreadUsr.Columns.ThreadK, Thread.Columns.K),
										  new TableElement(TablesEnum.GroupUsr),
										  QueryJoinType.Left,
										  new And(new Q(Thread.Columns.GroupK, GroupUsr.Columns.GroupK, true),
												  new Q(GroupUsr.Columns.UsrK, Usr.Current.K),
												  new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.Member)));

				q.QueryCondition = new And(q.QueryCondition,
										   new Or(new Q(Thread.Columns.GroupPrivate, false),
												  new Q(GroupUsr.Columns.UsrK, Usr.Current.K)),
										   new Or(new Q(Thread.Columns.PrivateGroup, false),
												  new Q(GroupUsr.Columns.UsrK, Usr.Current.K)));

				q.TopRecords = 100;
				q.OrderBy = new OrderBy("COUNT(*) DESC");
				q.Columns = new ColumnSet(ThreadUsr.Columns.InvitingUsrK, ThreadUsr.Columns.StatusChangeObjectType, ThreadUsr.Columns.StatusChangeObjectK, ThreadUsr.Columns.Status);
				ThreadUsrSet tus = new ThreadUsrSet(q);

				CheckForNullStatusChangeObjectAndBind(InviteGridView, InvitePanel, tus);
			}
			catch (Exception ex)
			{
				if (Vars.DevEnv)
					ErrorStatusMessage(ex);
			}
		}
		#region InviteGridView Event Handlers
		protected void InviteGridView_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				DropDownList OptionsDropDownList = (DropDownList)e.Row.FindControl("OptionsDropDownList");
				DropDownList TopicOptionsDropDownList = (DropDownList)e.Row.FindControl("TopicOptionsDropDownList");
				Label InviteNotBuddyLabel = (Label)e.Row.FindControl("InviteNotBuddyLabel");
				ThreadUsr threadUsr = (Bobs.ThreadUsr)e.Row.DataItem;

				if (OptionsDropDownList != null)
				{
					OptionsDropDownList.Items.Clear();
					OptionsDropDownList.Items.Add(new ListItem("Leave as buddy", ""));
					if (threadUsr.InvitingUsr.CanInvite(Usr.Current.K))
						OptionsDropDownList.Items.Add(new ListItem("Stop bulk invite", "Stop bulk invite"));
					try
					{
						Buddy buddy = new Buddy(Usr.Current.K, threadUsr.InvitingUsrK);
						OptionsDropDownList.Items.Add(new ListItem("Un-buddy", "Un-buddy"));
					}
					catch
					{
						InviteNotBuddyLabel.Visible = true;
						OptionsDropDownList.Visible = false;
					}
				}

				SetupTopicOptionsDropDownList(TopicOptionsDropDownList);
			}

			GridViewRowDataBound(sender, e);
		}
		#endregion
		#endregion
		#region VenueWatchPanel
		protected void BindVenueWatchPanel()
		{
			ThreadUsrSet tus = GetCountThreadUsrs(Usr.Current, ThreadUsr.StatusEnum.NewWatchedForumAlert, Model.Entities.ObjectType.Venue);
			CheckForNullStatusChangeObjectAndBind(VenueWatchGridView, VenueWatchPanel, tus);//, new BindPanel(BindVenueWatchPanel));
			
		}
		#region VenueWatchGridView Event Handlers
		protected void VenueWatchGridView_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				SetupGridViewDropDownLists(e.Row);
			}

			GridViewRowDataBound(sender, e);
		}
		#endregion
		#endregion
		#region BrandWatchPanel
		protected void BindBrandWatchPanel()
		{
			ThreadUsrSet tus = GetCountThreadUsrs(Usr.Current, ThreadUsr.StatusEnum.NewWatchedForumAlert, Model.Entities.ObjectType.Brand);
			CheckForNullStatusChangeObjectAndBind(BrandWatchGridView, BrandWatchPanel, tus);//, new BindPanel(BindBrandWatchPanel));
			
		}
		#region BrandWatchGridView Event Handlers
		protected void BrandWatchGridView_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				SetupGridViewDropDownLists(e.Row);
			}

			GridViewRowDataBound(sender, e);
		}
		#endregion
		#endregion
		#region EventWatchPanel
		protected void BindEventWatchPanel()
		{
			ThreadUsrSet tus = GetCountThreadUsrs(Usr.Current, ThreadUsr.StatusEnum.NewWatchedForumAlert, Model.Entities.ObjectType.Event);
			CheckForNullStatusChangeObjectAndBind(EventWatchGridView, EventWatchPanel, tus);//, new BindPanel(BindEventWatchPanel));

		}
		#region EventWatchGridViewEvent Handlers
		protected void EventWatchGridView_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				SetupGridViewDropDownLists(e.Row);
			}
			GridViewRowDataBound(sender, e);
		}
		#endregion
		#endregion
		#region GroupWatchPanel
		protected void BindGroupWatchPanel()
		{
			ThreadUsrSet tus = GetCountThreadUsrs(Usr.Current, ThreadUsr.StatusEnum.NewWatchedForumAlert, Model.Entities.ObjectType.Group);
			CheckForNullStatusChangeObjectAndBind(GroupWatchGridView, GroupWatchPanel, tus);//, new BindPanel(BindGroupWatchPanel));
		}
		#region GroupWatchGridViewEvent Handlers
		protected void GroupWatchGridView_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				SetupGridViewDropDownLists(e.Row);
			}
			GridViewRowDataBound(sender, e);
		}
		#endregion
		#endregion
		#region GroupNewsPanel
		protected void BindGroupNewsPanel()
		{
			ThreadUsrSet tus = GetCountThreadUsrs(Usr.Current, ThreadUsr.StatusEnum.NewGroupNewsAlert, Model.Entities.ObjectType.Group);
			CheckForNullStatusChangeObjectAndBind(GroupNewsGridView, GroupNewsPanel, tus);//, new BindPanel(BindGroupNewsPanel));
		}
		protected void GroupNewsGridView_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				SetupGridViewDropDownLists(e.Row, true);
			}
			GridViewRowDataBound(sender, e);
		}
		#endregion

		#region WatchPhotoPanel
		protected void BindWatchPhotoPanel()
		{
			ThreadUsrSet tus = GetCountThreadUsrs(Usr.Current, ThreadUsr.StatusEnum.NewWatchedForumAlert, Model.Entities.ObjectType.Photo, false, 1);
			int count = 0;
			if(tus.Count == 1 && tus[0].ExtraSelectElements["count"] != DBNull.Value)
				count = Convert.ToInt32(tus[0].ExtraSelectElements["count"]);

			WatchPhotoPanel.Visible = count > 0;
			if (count > 0)
			{
				NumberOfTopicsFromWatchingPhotosLabel.Text = Utilities.Link(UrlInfo.PageUrl("inbox", "statuschangeobjecttype", Convert.ToInt32(Model.Entities.ObjectType.Photo).ToString()), count.ToString() + " topic" + (count > 1 ? "s" : ""));

				// Get job status from Memcached
				object cachedJobStatus = global::Caching.Instances.Main.Get(new global::Caching.CacheKey(global::Caching.CacheKeyPrefix.UpdateThreadUsrJobStatus, "UsrK", Usr.Current.K.ToString(), "StatusChangeObjectType",
																			  tus[0].StatusChangeObjectType.ToString(), "StatusChangeObjectK", null).ToString());
				if (cachedJobStatus != null && (Bobs.JobProcessor.Job.JobStatus.Queued == (Bobs.JobProcessor.Job.JobStatus)cachedJobStatus || Bobs.JobProcessor.Job.JobStatus.Running == (Bobs.JobProcessor.Job.JobStatus)cachedJobStatus))
				{
					WatchPhotoTopicOptionsPanel.Visible = false;
					WatchPhotoTopicOptionsLabel.Visible = true;
					WatchPhotoTopicOptionsLabel.Text = PROCESSING_HTML;
				}
			}
			SetupTopicOptionsDropDownList(PhotoTopicOptionsDropDownList);
		}

		protected void PhotoTopicOptionsUpdateButton_Click(object o, EventArgs e)
		{
			try
			{
				ProcessTopicOptions(false, null, Model.Entities.ObjectType.Photo, PhotoTopicOptionsDropDownList);
				BindWatchPhotoPanel();
				((Spotted.Master.DsiPage)Page).AnchorSkip("WatchPhotoPanelAnchor");
			}
			catch (Exception ex)
			{
				ErrorStatusMessage(ex);
			}
		}

		//protected void RemoveAllPhotoLinkButton_Click(object sender, EventArgs eventArgs)
		//{
		//    try
		//    {
		//        Usr.Current.UpdateThreadUsrs(ThreadUsr.StatusEnum.Archived, ThreadUsr.StatusEnum.NewWatchedForumAlert, Model.Entities.ObjectType.Photo);
		//        BindWatchPhotoPanel();
		//        ((Spotted.Master.DsiPage)Page).AnchorSkip("WatchPhotoPanelAnchor");
		//    }
		//    catch (Exception ex)
		//    {
		//        ErrorStatusMessage(ex);
		//    }
		//}

		//protected void IgnoreAllPhotoLinkButton_Click(object sender, EventArgs eventArgs)
		//{
		//    try
		//    {
		//        Usr.Current.UpdateThreadUsrs(ThreadUsr.StatusEnum.Ignore, ThreadUsr.StatusEnum.NewWatchedForumAlert, Model.Entities.ObjectType.Photo);
		//        BindWatchPhotoPanel();
		//        ((Spotted.Master.DsiPage)Page).AnchorSkip("WatchPhotoPanelAnchor");
		//    }
		//    catch (Exception ex)
		//    {
		//        ErrorStatusMessage(ex);
		//    }
		//}
		#endregion

		#region WatchArticlePanel
		protected void BindWatchArticlePanel()
		{
			ThreadUsrSet tus = GetCountThreadUsrs(Usr.Current, ThreadUsr.StatusEnum.NewWatchedForumAlert, Model.Entities.ObjectType.Article, false, 1);
			int count = 0;
			if (tus.Count == 1 && tus[0].ExtraSelectElements["count"] != DBNull.Value)
				count = Convert.ToInt32(tus[0].ExtraSelectElements["count"]);

			WatchArticlePanel.Visible = count > 0;
			if (count > 0)
			{
				NumberOfTopicsFromWatchingArticlesLabel.Text = Utilities.Link(UrlInfo.PageUrl("inbox", "statuschangeobjecttype", Convert.ToInt32(Model.Entities.ObjectType.Article).ToString()), count.ToString() + " topic" + (count > 1 ? "s" : ""));

				// Get job status from Memcached
				object cachedJobStatus = global::Caching.Instances.Main.Get(new global::Caching.CacheKey(global::Caching.CacheKeyPrefix.UpdateThreadUsrJobStatus, "UsrK", Usr.Current.K.ToString(), "StatusChangeObjectType",
																			  tus[0].StatusChangeObjectType.ToString(), "StatusChangeObjectK", null).ToString());
				if (cachedJobStatus != null && (Bobs.JobProcessor.Job.JobStatus.Queued == (Bobs.JobProcessor.Job.JobStatus)cachedJobStatus || Bobs.JobProcessor.Job.JobStatus.Running == (Bobs.JobProcessor.Job.JobStatus)cachedJobStatus))
				{
					WatchArticleTopicOptionsPanel.Visible = false;
					WatchArticleTopicOptionsLabel.Visible = true;
					WatchArticleTopicOptionsLabel.Text = PROCESSING_HTML;
				}
			}
			SetupTopicOptionsDropDownList(ArticleTopicOptionsDropDownList);
		}
		protected void ArticleTopicOptionsUpdateButton_Click(object o, EventArgs e)
		{
			try
			{
				ProcessTopicOptions(false, null, Model.Entities.ObjectType.Article, ArticleTopicOptionsDropDownList);
				BindWatchArticlePanel();
				((Spotted.Master.DsiPage)Page).AnchorSkip("WatchArticlePanelAnchor");
			}
			catch (Exception ex)
			{
				ErrorStatusMessage(ex);
			}
		}

		//protected void RemoveAllArticleLinkButton_Click(object sender, EventArgs eventArgs)
		//{
		//    try
		//    {
		//        Usr.Current.UpdateThreadUsrs(ThreadUsr.StatusEnum.Archived, ThreadUsr.StatusEnum.NewWatchedForumAlert, Model.Entities.ObjectType.Article);
		//        BindWatchArticlePanel();
		//        ((Spotted.Master.DsiPage)Page).AnchorSkip("WatchArticlePanelAnchor");
		//    }
		//    catch (Exception ex)
		//    {
		//        ErrorStatusMessage(ex);
		//    }
		//}

		//protected void IgnoreAllArticleLinkButton_Click(object sender, EventArgs eventArgs)
		//{
		//    try
		//    {
		//        Usr.Current.UpdateThreadUsrs(ThreadUsr.StatusEnum.Ignore, ThreadUsr.StatusEnum.NewWatchedForumAlert, Model.Entities.ObjectType.Article);
		//        BindWatchArticlePanel();
		//        ((Spotted.Master.DsiPage)Page).AnchorSkip("WatchArticlePanelAnchor");
		//    }
		//    catch (Exception ex)
		//    {
		//        ErrorStatusMessage(ex);
		//    }
		//}
		#endregion

		#region WatchOtherThreadPanel
		protected void BindWatchOtherThreadPanel()
		{
			Query q = new Query();
			q.ExtraSelectElements.Add("count", "COUNT(*)");
			q.QueryCondition = new And(new Q(ThreadUsr.Columns.UsrK, Usr.Current.K),
									   new Q(ThreadUsr.Columns.Status, ThreadUsr.StatusEnum.UnArchived));
			q.Columns = new ColumnSet();

			ThreadUsrSet tus = new ThreadUsrSet(q);
			int count = 0;
			if (tus.Count == 1 && tus[0].ExtraSelectElements["count"] != DBNull.Value)
				count = Convert.ToInt32(tus[0].ExtraSelectElements["count"]);

			WatchOtherThreadPanel.Visible = count > 0;
			if (count > 0)
			{
				NumberOfTopicsFromWatchingOtherThreadLabel.Text = Utilities.Link(UrlInfo.PageUrl("inbox", "statusenum", Convert.ToInt32(ThreadUsr.StatusEnum.UnArchived).ToString()), count.ToString() + " other topic" + (count > 1 ? "s" : ""));

				// Get job status from Memcached
				object cachedJobStatus = global::Caching.Instances.Main.Get(new global::Caching.CacheKey(global::Caching.CacheKeyPrefix.UpdateThreadUsrJobStatus, "UsrK", Usr.Current.K.ToString(), "StatusChangeObjectType",
																			  null, "StatusChangeObjectK", null).ToString());
				if (cachedJobStatus != null && (Bobs.JobProcessor.Job.JobStatus.Queued == (Bobs.JobProcessor.Job.JobStatus)cachedJobStatus || Bobs.JobProcessor.Job.JobStatus.Running == (Bobs.JobProcessor.Job.JobStatus)cachedJobStatus))
				{
					WatchOtherTopicOptionsPanel.Visible = false;
					WatchOtherTopicOptionsLabel.Visible = true;
					WatchOtherTopicOptionsLabel.Text = PROCESSING_HTML;
				}
			}

			SetupTopicOptionsDropDownList(OtherTopicOptionsDropDownList);
		}

		protected void OtherTopicOptionsUpdateButton_Click(object o, EventArgs e)
		{
			try
			{
				if(OtherTopicOptionsDropDownList.SelectedValue == REMOVE_FROM_INBOX)
					Usr.Current.UpdateThreadUsrs(ThreadUsr.StatusEnum.Archived, ThreadUsr.StatusEnum.UnArchived);
				else if (OtherTopicOptionsDropDownList.SelectedValue == SMART_DELETE)
					Usr.Current.SmartDelete(ThreadUsr.StatusEnum.UnArchived, null);
				else if (OtherTopicOptionsDropDownList.SelectedValue == IGNORE_ALL)
					Usr.Current.UpdateThreadUsrs(ThreadUsr.StatusEnum.Ignore, ThreadUsr.StatusEnum.UnArchived);

				BindWatchOtherThreadPanel();
				((Spotted.Master.DsiPage)Page).AnchorSkip("WatchOtherThreadPanelAnchor");
			}
			catch (Exception ex)
			{
				ErrorStatusMessage(ex);
			}
		}
		//protected void RemoveAllOtherInboxLinkButton_Click(object sender, EventArgs eventArgs)
		//{
		//    try
		//    {
		//        Usr.Current.UpdateThreadUsrs(ThreadUsr.StatusEnum.Archived, ThreadUsr.StatusEnum.UnArchived);
		//        BindWatchOtherThreadPanel();
		//        ((Spotted.Master.DsiPage)Page).AnchorSkip("WatchOtherThreadPanelAnchor");
		//    }
		//    catch (Exception ex)
		//    {
		//        ErrorStatusMessage(ex);
		//    }
		//}

		//protected void IgnoreAllOtherInboxLinkButton_Click(object sender, EventArgs eventArgs)
		//{
		//    try
		//    {
		//        Usr.Current.UpdateThreadUsrs(ThreadUsr.StatusEnum.Ignore, ThreadUsr.StatusEnum.UnArchived);
		//        BindWatchOtherThreadPanel();
		//        ((Spotted.Master.DsiPage)Page).AnchorSkip("WatchOtherThreadPanelAnchor");
		//    }
		//    catch (Exception ex)
		//    {
		//        ErrorStatusMessage(ex);
		//    }
		//}

		#endregion

		#region GetCountThreadUsrs
		private ThreadUsrSet GetCountThreadUsrs(Usr usr, ThreadUsr.StatusEnum status, Model.Entities.ObjectType statusChangeObjectType)
		{
			return GetCountThreadUsrs(usr, status, statusChangeObjectType, true, 100);
		}
		private ThreadUsrSet GetCountThreadUsrs(Usr usr, ThreadUsr.StatusEnum status, Model.Entities.ObjectType statusChangeObjectType, bool groupByStatusChangeObjectK, int returnTopRecords)
		{
			Query q = new Query();

			try
			{
				if (groupByStatusChangeObjectK)
				{
					q.GroupBy = new GroupBy(new GroupBy(ThreadUsr.Columns.StatusChangeObjectType), new GroupBy(ThreadUsr.Columns.StatusChangeObjectK), new GroupBy(ThreadUsr.Columns.Status));
					q.TopRecords = returnTopRecords;
					q.OrderBy = new OrderBy("COUNT(*) DESC");
					q.Columns = new ColumnSet(ThreadUsr.Columns.StatusChangeObjectType, ThreadUsr.Columns.StatusChangeObjectK, ThreadUsr.Columns.Status);
				}
				else
				{
					q.GroupBy = new GroupBy(new GroupBy(ThreadUsr.Columns.StatusChangeObjectType), new GroupBy(ThreadUsr.Columns.Status));
					q.Columns = new ColumnSet(ThreadUsr.Columns.StatusChangeObjectType, ThreadUsr.Columns.Status);
				}
				q.ExtraSelectElements.Add("count", "COUNT(*)");
				q.TableElement = new Join(ThreadUsr.Columns.ThreadK, Thread.Columns.K);
				q.QueryCondition = new And(new Q(ThreadUsr.Columns.UsrK, usr.K),
										   new Q(ThreadUsr.Columns.Status, status),
										   new Q(ThreadUsr.Columns.StatusChangeObjectType, statusChangeObjectType));
				if (status == ThreadUsr.StatusEnum.NewGroupNewsAlert)
				{
					q.QueryCondition = new And(q.QueryCondition,
											   new Q(Thread.Columns.IsNews, true));
				}

				if (statusChangeObjectType == Model.Entities.ObjectType.Group)
				{
					q.TableElement = new Join(q.TableElement,
										  new TableElement(TablesEnum.GroupUsr),
										  QueryJoinType.Left,
										  new And(new Q(Thread.Columns.GroupK, GroupUsr.Columns.GroupK, true),
												  new Q(GroupUsr.Columns.UsrK, Usr.Current.K),
												  new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.Member)));

					q.QueryCondition = new And(q.QueryCondition,
											   new Or(new Q(Thread.Columns.GroupPrivate, false),
													  new Q(GroupUsr.Columns.UsrK, Usr.Current.K)),
											   new Or(new Q(Thread.Columns.PrivateGroup, false),
													  new Q(GroupUsr.Columns.UsrK, Usr.Current.K)));
				}

				return new ThreadUsrSet(q);
			}
			catch (Exception ex)
			{
				if (Vars.DevEnv)
					ErrorStatusMessage(ex);

				return null;
			}
		}
		#endregion

		//public delegate void BindPanel();
		private void BindGridViewAndPanel(GridView gridView)
		{
			if(InviteGridView == gridView)
				BindInvitePanel();
			else if(VenueWatchGridView == gridView)
				BindVenueWatchPanel();
			else if(GroupNewsGridView == gridView)
				BindGroupNewsPanel();
			else if(GroupWatchGridView == gridView)
				BindGroupWatchPanel();
			else if(EventWatchGridView == gridView)
				BindEventWatchPanel();
			else if(BrandWatchGridView == gridView)
				BindBrandWatchPanel();

			InboxSpamPanel.Visible = InvitePanel.Visible || EventWatchPanel.Visible || VenueWatchPanel.Visible || BrandWatchPanel.Visible || GroupWatchPanel.Visible || GroupNewsPanel.Visible || WatchArticlePanel.Visible || WatchPhotoPanel.Visible || WatchOtherThreadPanel.Visible;
			NoInboxSpamPanel.Visible = !InboxSpamPanel.Visible;
		}

		protected void GridViewRowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				try
				{
					ThreadUsr tu = (Bobs.ThreadUsr)e.Row.DataItem;

					// Get job status from Memcached
					
					object cachedJobStatus = global::Caching.Instances.Main.Get(new global::Caching.CacheKey(global::Caching.CacheKeyPrefix.UpdateThreadUsrJobStatus, "UsrK", Usr.Current.K.ToString(), "StatusChangeObjectType",
																				  tu.StatusChangeObjectType.ToString(), "StatusChangeObjectK", tu.StatusChangeObjectK.ToString()).ToString());

					if (sender == GroupNewsGridView)
					{
						cachedJobStatus = global::Caching.Instances.Main.Get(new global::Caching.CacheKey(global::Caching.CacheKeyPrefix.UpdateThreadUsrJobStatus, "UsrK", Usr.Current.K.ToString(), "StatusChangeObjectType",
																				  tu.StatusChangeObjectType.ToString(), "StatusChangeObjectK", tu.StatusChangeObjectK.ToString(), "News").ToString());
					}
					if (cachedJobStatus != null && (Bobs.JobProcessor.Job.JobStatus.Queued == (Bobs.JobProcessor.Job.JobStatus)cachedJobStatus || Bobs.JobProcessor.Job.JobStatus.Running == (Bobs.JobProcessor.Job.JobStatus)cachedJobStatus))
					{
						DropDownList TopicOptionsDropDownList = (DropDownList)e.Row.FindControl("TopicOptionsDropDownList");
						Label TopicOptionsLabel = (Label)e.Row.FindControl("TopicOptionsLabel");

						TopicOptionsDropDownList.Visible = false;
						TopicOptionsLabel.Visible = true;
						TopicOptionsLabel.Text = PROCESSING_HTML;
					}
									
				}
				catch { }
			}
		}

		protected void RefreshButton_Click(object o, EventArgs e)
		{
			BindPanels();

			if (((HtmlButton)o).Parent.Parent.NamingContainer is GridView)
				AnchorSkipSpamPage((GridView)((HtmlButton)o).Parent.Parent.NamingContainer);
			else
				((Spotted.Master.DsiPage)Page).AnchorSkip("AfterGridViewsAnchor");
		}
		
		protected void OptionsUpdateButton_Click(object o, EventArgs e)
		{
			bool isNews = false;
			IBob statusChangeObject = null;
			bool fail = false;
			GridView gridView = null;
			try
			{
				HtmlButton OptionsUpdateButton = (HtmlButton)o;
				GridViewRow gridRow = (GridViewRow)OptionsUpdateButton.Parent.Parent;
				gridView = (GridView)gridRow.Parent.Parent;

				foreach (GridViewRow gvr in gridView.Rows)
				{
					if (gvr.RowType == DataControlRowType.DataRow)
					{
						int statusChangeObjectK = Convert.ToInt32(((TextBox)gvr.FindControl("StatusChangeObjectKTextBox")).Text);
						Model.Entities.ObjectType statusChangeObjectType = (Model.Entities.ObjectType)Convert.ToInt32(((TextBox)gvr.FindControl("StatusChangeObjectTypeTextBox")).Text);
						statusChangeObject = Bob.Get(statusChangeObjectType, statusChangeObjectK);
						DropDownList OptionsDropDownList = (DropDownList)gvr.FindControl("OptionsDropDownList");
						DropDownList TopicOptionsDropDownList = (DropDownList)gvr.FindControl("TopicOptionsDropDownList");

						try
						{
							isNews = Convert.ToBoolean(((TextBox)gvr.FindControl("StatusChangeObjectIsNewsTextBox")).Text);
						}
						catch { }

						try
						{
							ProcessOptions(statusChangeObjectK, statusChangeObjectType, OptionsDropDownList);
						}
						catch
						{
							fail = true;
						}
						try
						{
							ProcessTopicOptions(isNews, statusChangeObjectK, statusChangeObjectType, TopicOptionsDropDownList);
						}
						catch (Exception ex)
						{
							//ErrorStatusMessage(ex);
							fail = true;
						}
					}
				}
			}
			catch (Exception ex)
			{
				fail = true;
//				ErrorStatusMessage(ex);
			}

			if (fail)
				ErrorStatusMessage();
			else
				SuccessfulStatusMessage();
			BindGridViewAndPanel(gridView);
			AnchorSkipSpamPage((IBobType)statusChangeObject, isNews);
		}

		private void ProcessTopicOptions(bool isNews, int? statusChangeObjectK, Model.Entities.ObjectType? statusChangeObjectType, DropDownList TopicOptionsDropDownList)
		{
			ThreadUsr.StatusEnum threadUsrStatus = ThreadUsr.StatusEnum.NewWatchedForumAlert;
			if (statusChangeObjectType == Model.Entities.ObjectType.Usr)
				threadUsrStatus = ThreadUsr.StatusEnum.NewInvite;
			else if(isNews)
				threadUsrStatus = ThreadUsr.StatusEnum.NewGroupNewsAlert;

			if (TopicOptionsDropDownList.Visible && TopicOptionsDropDownList.SelectedValue != "")
			{
				if (TopicOptionsDropDownList.SelectedValue == REMOVE_FROM_INBOX)
				{
					Usr.Current.UpdateThreadUsrs(ThreadUsr.StatusEnum.Archived, threadUsrStatus, statusChangeObjectType, statusChangeObjectK);
				}
				else if (TopicOptionsDropDownList.SelectedValue == IGNORE_ALL)
				{
					Usr.Current.UpdateThreadUsrs(ThreadUsr.StatusEnum.Ignore, threadUsrStatus, statusChangeObjectType, statusChangeObjectK);
				}
				else if (TopicOptionsDropDownList.SelectedValue == SMART_DELETE)
				{
					Usr.Current.SmartDelete(threadUsrStatus, statusChangeObjectType, statusChangeObjectK);
				}
			}
		}

		private void ProcessOptions(int? statusChangeObjectK, Model.Entities.ObjectType? statusChangeObjectType, DropDownList OptionsDropDownList)
		{
			if (OptionsDropDownList.Visible && OptionsDropDownList.SelectedValue != "" && statusChangeObjectType != null)
			{
				if (statusChangeObjectType.Value == Model.Entities.ObjectType.Usr)
				{
					if (statusChangeObjectK == null || statusChangeObjectK.Value == 0 || statusChangeObjectK.Value == Usr.Current.K)
						throw new DsiUserFriendlyException("Invalid buddy.");

					if (OptionsDropDownList.SelectedValue == "Un-buddy")
					{
						Usr.Current.RemoveBuddy(statusChangeObjectK.Value);
					}
					else if (OptionsDropDownList.SelectedValue == "Stop bulk invite")
					{
						Usr.Current.SetBuddyInvite(new Usr(statusChangeObjectK.Value), false, Usr.AddBuddySource.SpamPage, null);
						//if (Usr.Current.HasBuddy(statusChangeObjectK.Value))
						//{
						//    Buddy b = new Buddy(Usr.Current.K, statusChangeObjectK.Value);
						//    b.CanInvite = false;
						//    b.Update();
						//}
					}
				}

				else
				{
					if (OptionsDropDownList.SelectedValue == STOP_WATCHING)
					{
						CommentAlert.Disable(Usr.Current, statusChangeObjectK.Value, statusChangeObjectType.Value);
						//StatusMessage("You have stopped watching " + ((IBobType)statusChangeObject).TypeName.ToLower() + ": " + ((IBobType)statusChangeObject).Name);
					}
					else if (OptionsDropDownList.SelectedValue == EXIT_GROUP) 
					{
						var bob = Bob.Get(statusChangeObjectType.Value, statusChangeObjectK.Value);

						if(statusChangeObjectType.Value == Model.Entities.ObjectType.Group)
							((Group)bob).Exit(Usr.Current);
						else if (statusChangeObjectType.Value == Model.Entities.ObjectType.Brand)
							((Brand)bob).Group.Exit(Usr.Current);
						
						//StatusMessage("You have exited " + ((IBobType)statusChangeObject).TypeName.ToLower() + ": " + ((IBobType)statusChangeObject).Name);
					}
				}
			}
		}

		#region AnchorSkip
		private void AnchorSkipSpamPage(IBobType iBobType)
		{
			AnchorSkipSpamPage(iBobType, false);
		}
		private void AnchorSkipSpamPage(IBobType iBobType, bool isNews)
		{
			((Spotted.Master.DsiPage)Page).AnchorSkip(iBobType.TypeName + (isNews ? "News" : "Watch") + "PanelAnchor");
		}
		private void AnchorSkipSpamPage(GridView gridView)
		{
			if (InviteGridView == gridView)
				((Spotted.Master.DsiPage)Page).AnchorSkip(Model.Entities.ObjectType.Usr.ToString() + "WatchPanelAnchor");
			else if (VenueWatchGridView == gridView)
				((Spotted.Master.DsiPage)Page).AnchorSkip(Model.Entities.ObjectType.Venue.ToString() + "WatchPanelAnchor");
			else if (GroupNewsGridView == gridView)
				((Spotted.Master.DsiPage)Page).AnchorSkip(Model.Entities.ObjectType.Group.ToString() + "NewsPanelAnchor");
			else if (GroupWatchGridView == gridView)
				((Spotted.Master.DsiPage)Page).AnchorSkip(Model.Entities.ObjectType.Group.ToString() + "WatchPanelAnchor");
			else if (EventWatchGridView == gridView)
				((Spotted.Master.DsiPage)Page).AnchorSkip(Model.Entities.ObjectType.Event.ToString() + "WatchPanelAnchor");
			else if (BrandWatchGridView == gridView)
				((Spotted.Master.DsiPage)Page).AnchorSkip(Model.Entities.ObjectType.Brand.ToString() + "WatchPanelAnchor");
		}
		#endregion

		protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			GridView gridView = (GridView)sender;
			if (gridView.EditIndex != -1)
			{
				// Use the Cancel property to cancel the paging operation.
				e.Cancel = true;
			}
			else
			{
				gridView.PageIndex = e.NewPageIndex;
				BindGridViewAndPanel(gridView);
				if (gridView.PageIndex > gridView.PageCount)
					gridView.PageIndex = 1;
				AnchorSkipSpamPage(gridView);
			}
		}
		#region Setup DropDownLists
		private void SetupGridViewDropDownLists(GridViewRow gvr)
		{
			SetupGridViewDropDownLists(gvr, false);
		}

		private void SetupGridViewDropDownLists(GridViewRow gvr, bool isNews)
		{
			ThreadUsr threadUsr = (Bobs.ThreadUsr)gvr.DataItem;

			DropDownList OptionsDropDownList = (DropDownList)gvr.FindControl("OptionsDropDownList");
			DropDownList TopicOptionsDropDownList = (DropDownList)gvr.FindControl("TopicOptionsDropDownList");
			Label NotWatchingLabel = (Label)gvr.FindControl("NotWatchingLabel");

			SetupWatchingDropDownList((IBobType)threadUsr.StatusChangeObject, OptionsDropDownList, NotWatchingLabel, isNews);
			SetupTopicOptionsDropDownList(TopicOptionsDropDownList);
		}

		private void SetupWatchingDropDownList(IBobType bobType, DropDownList ddl, Label notWatchingLabel, bool isNews)
		{
			if (ddl != null)
			{
				ddl.Items.Clear();
				ddl.Items.Add(new ListItem("No change", ""));

				if (bobType.ObjectType == Model.Entities.ObjectType.Group)
				{
					if (((Group)bobType).IsMember(Usr.Current))
					{
						if (!isNews && CommentAlert.IsEnabled(Usr.Current.K, bobType.K, bobType.ObjectType))
							ddl.Items.Add(new ListItem(STOP_WATCHING, STOP_WATCHING));

						ddl.Items.Add(new ListItem(EXIT_GROUP, EXIT_GROUP));
					}
					else
					{
						notWatchingLabel.Visible = true;
						notWatchingLabel.Text = "<small>Not member of " + bobType.TypeName.ToLower() + "</small>";
						ddl.Visible = false;
					}
				}
				else
				{
					if (CommentAlert.IsEnabled(Usr.Current.K, bobType.K, bobType.ObjectType))
						ddl.Items.Add(new ListItem(STOP_WATCHING, STOP_WATCHING));
					else
					{
						notWatchingLabel.Visible = true;
						notWatchingLabel.Text = "<small>Not watching " + bobType.TypeName.ToLower() + "</small>";
						ddl.Visible = false;
					}
				}
			}
		}

		private void SetupTopicOptionsDropDownList(DropDownList ddl)
		{
			if (ddl != null)
			{
				ddl.Items.Clear();
				ddl.Items.Add(new ListItem("Leave in inbox", ""));
				ddl.Items.Add(new ListItem(REMOVE_FROM_INBOX, REMOVE_FROM_INBOX));
				ddl.Items.Add(new ListItem(SMART_DELETE, SMART_DELETE));
				ddl.Items.Add(new ListItem(IGNORE_ALL, IGNORE_ALL));
			}
		}
		#endregion

		private void CheckForNullStatusChangeObjectAndBind(GridView gridView, Panel panel, ThreadUsrSet tus)
		{
			List<ThreadUsr> threadUsrs = new List<ThreadUsr>();
			try
			{
				tus.Reset();

				//global::Cache.Instances.Main.Store(new Cache.CacheKey(global::Cache.CacheKeyPrefix.SpamQueryResults, "Panel", panel.ClientID), tus);
				if (tus.Count == 0)
				{
					panel.Visible = false;
				}
				else
				{
					foreach (ThreadUsr tu in tus)
					{
						if (tu.StatusChangeObject != null)
						{
							//object cachedJobStatus = global::Cache.Instances.Main.Get(new global::Cache.CacheKey(global::Cache.CacheKeyPrefix.UpdateThreadUsrJobStatus, "UsrK", Usr.Current.K.ToString(), "StatusChangeObjectType",
							//                                                      tu.StatusChangeObjectType.ToString(), "StatusChangeObjectK", tu.StatusChangeObjectK.ToString()).ToString());
							//if (cachedJobStatus == null || Bobs.JobProcessor.Job.JobStatus.Failed == (Bobs.JobProcessor.Job.JobStatus)cachedJobStatus)
								threadUsrs.Add(tu);
							//else
							//    cachedJobStatus = cachedJobStatus;
						}
					}
					if (threadUsrs.Count > 0)
					{
						panel.Visible = true;
						if ((gridView.PageIndex + 1) * gridView.PageCount > threadUsrs.Count)
							gridView.PageIndex = Convert.ToInt32(Math.Floor((double)(threadUsrs.Count / gridView.PageCount)));
						gridView.DataSource = threadUsrs;
						gridView.DataBind();
					}
					else
					{
						panel.Visible = false;
					}
				}
			}
			catch (Exception ex)
			{
				if(Vars.DevEnv)
					ErrorStatusMessage(ex);
			}
		}
	}
}
