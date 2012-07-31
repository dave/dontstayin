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
using Cambro.Web.DbCombo;
using Bobs;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using Spotted.Controls;
namespace Spotted.Pages.Groups
{
	public partial class Admin : DsiUserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn();

			if (!Usr.Current.IsAdmin && (CurrentGroupUsr == null || !CurrentGroupUsr.IsMember))
				throw new DsiUserFriendlyException("You're not a member of this group!");
			
			if (!Page.IsPostBack)
			{
				if (Mode.Equals(Modes.Options))
					ChangePanel(PanelOptions);
			}
			ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "DbButtonInit", "DbButtonInit(" + Bobs.Vars.LanguageString + ");", true);
			this.PanelOptions_Load(sender, e);
			this.BuddiesPanel_Load(sender, e);
			this.MemberPanel_Load(sender, e);
			this.ModeratorPanel_Load(sender, e);
			this.NewsAdminPanel_Load(sender, e);
			this.MemberAdminPanel_Load(sender, e);
			this.OwnerPanel_Load(sender, e);
			this.EmailPanel_Load(sender, e);
		}

		#region OptionsPanel
		private void PanelOptions_Load(object sender, System.EventArgs e)
		{
			OptionsGroupAnchor.HRef = CurrentGroup.Url();
			OptionsGroupAnchor.InnerText = CurrentGroup.FriendlyName;

			ArrayList sb = new ArrayList();

			if (HasOptions)
			{
				if (Mode.Equals(Modes.MemberOptions))
					sb.Add("<b>member</b>");
				else
					sb.Add("<a href=\"" + CurrentGroup.UrlApp("admin", "mode", "options") + "\">member</a>");
			}

			if (HasModerator)
			{
				if (Mode.Equals(Modes.ModeratorOptions))
					sb.Add("<b>moderator</b>");
				else
					sb.Add("<a href=\"" + CurrentGroup.UrlApp("admin", "mode", "moderator") + "\">moderator</a>");

				if (HasNewsAdmin)
				{
					if (Mode.Equals(Modes.NewsAdmin))
						sb.Add("<b>news</b>");
					else
						sb.Add("<a href=\"" + CurrentGroup.UrlApp("admin", "mode", "news") + "\">news</a>");
				}

				if (HasMemberAdmin)
				{
					if (Mode.Equals(Modes.MemberAdmin))
						sb.Add("<b>membership</b>");
					else
						sb.Add("<a href=\"" + CurrentGroup.UrlApp("admin", "mode", "membership") + "\">membership</a>");
				}

				if (HasOwner)
				{
					if (Mode.Equals(Modes.OwnerOptions))
						sb.Add("<b>owner</b>");
					else
						sb.Add("<a href=\"" + CurrentGroup.UrlApp("admin", "mode", "owner") + "\">owner</a>");
				}

			}

			if (sb.Count > 0)
			{
				OptionsMenuP.InnerHtml = "";

				foreach (string s in sb)
				{
					OptionsMenuP.InnerHtml += (OptionsMenuP.InnerHtml.Length > 0 ? " | " : "") + s;
				}
				OptionsMenuP.InnerHtml = "Options: " + OptionsMenuP.InnerHtml;
			}
			else
				OptionsMenuP.Visible = false;


			if (CurrentGroup.CanMember(Usr.Current, CurrentGroupUsr))
			{
				OptionsInviteP.InnerHtml = "Invite: ";

				if (Usr.Current.HasBuddiesFull)
				{
					if (Mode.Equals(Modes.Buddies))
						OptionsInviteP.InnerHtml += "<b>my buddies</b> | ";
					else
						OptionsInviteP.InnerHtml += "<a href=\"" + CurrentGroup.UrlApp("admin", "mode", "buddies") + "\">my buddies</a> | ";
				}

				if (Mode.Equals(Modes.Email))
					OptionsInviteP.InnerHtml += "<b>by email</b>";
				else
					OptionsInviteP.InnerHtml += "<a href=\"" + CurrentGroup.UrlApp("admin", "mode", "email") + "\">by email</a>";
			}
			else
				OptionsInviteP.Visible = false;

			if (Usr.Current.CanGroupOwner(CurrentGroupUsr))
			{
				EditOptionsP.InnerHtml = "Edit group: ";
				if (CurrentGroup.BrandK == 0)
					EditOptionsP.InnerHtml += "<a href=\"" + CurrentGroup.UrlApp("edit", "mode", "theme") + "\">theme</a> | ";
				EditOptionsP.InnerHtml += "<a href=\"" + CurrentGroup.UrlApp("edit", "mode", "location") + "\">location</a> | ";
				EditOptionsP.InnerHtml += "<a href=\"" + CurrentGroup.UrlApp("edit", "mode", "musictype") + "\">music type</a> | ";
				EditOptionsP.InnerHtml += "<a href=\"" + CurrentGroup.UrlApp("edit", "mode", "details") + "\">details</a> | ";
				EditOptionsP.InnerHtml += "<a href=\"" + CurrentGroup.UrlApp("edit", "mode", "membership") + "\">membership</a> | ";
				if (CurrentGroup.BrandK == 0)
					EditOptionsP.InnerHtml += "<a href=\"" + CurrentGroup.UrlApp("edit", "mode", "private") + "\">privacy</a> | ";
				EditOptionsP.InnerHtml += "<a href=\"" + CurrentGroup.UrlApp("edit", "mode", "pic") + "\">pic</a>";
			}
			else
				EditOptionsP.Visible = false;

		}
		#endregion
		#region BuddiesPanel
		public void BuddiesPanel_Load(object o, System.EventArgs e)
		{
			BuddiesPanel.Visible = Mode.Equals(Modes.Buddies);
			if (Mode.Equals(Modes.Buddies))
			{
				
			}
		}
		public void Buddies_Click(object o, System.EventArgs e)
		{
			if (Mode.Equals(Modes.Buddies))
			{
				if (Page.IsValid)
				{
					
					Query q = new Query(new Q(Usr.Columns.K, uiMultiBuddyChooser.SelectedUsrKs.ToArray()));
					


					q.Columns = new ColumnSet(Usr.EmailColumns, Usr.LinkColumns, Usr.Columns.AddedByUsrK, Usr.Columns.AddedByGroupK);
					UsrSet us = new UsrSet(q);
					int successCount = 0;
					foreach (Usr u in us)
					{
						try
						{
							GroupUsr gu = CurrentGroup.GetGroupUsr(u);
							Return r = CurrentGroup.Invite(u, gu, Usr.Current, CurrentGroupUsr, BuddiesIntroTextBox.Text, false);

							if (r.Success)
							{
								successCount++;
								BuddiesOutputP.InnerHtml += @"<img src=""/gfx/icon-tick-up.png"" border=""0"" height=""21"" width=""26"" align=""absmiddle"" style=""margin-right:3px;"">";
								BuddiesOutputP.InnerHtml += r.MessageHtml;
							}
							else
							{
								BuddiesOutputP.InnerHtml += @"<img src=""/gfx/icon-cross-up.png"" border=""0"" height=""21"" width=""26"" align=""absmiddle"" style=""margin-right:3px;"">";
								BuddiesOutputP.InnerHtml += r.MessageHtml;
							}

						}
						catch
						{
							BuddiesOutputP.InnerHtml += @"<img src=""/gfx/icon-cross-up.png"" border=""0"" height=""21"" width=""26"" align=""absmiddle"" style=""margin-right:3px;"">Exception";
						}
						BuddiesOutputP.InnerHtml += "<br>";
					}
					if (successCount > 0)
						this.uiMultiBuddyChooser.Clear();

				}
			}
		}
		public void BuddiesIntro_Val(object o, ServerValidateEventArgs e)
		{
			string s = Cambro.Web.Helpers.Strip(BuddiesIntroTextBox.Text, true, true, false, true);
			BuddiesIntroTextBox.Text = s;
			e.IsValid = s.Length > 10 && s.Length < 500;
		}

		#endregion
		#region EmailPanel
		public void EmailPanel_Load(object o, System.EventArgs e)
		{
			EmailPanel.Visible = Mode.Equals(Modes.Email);
			if (Mode.Equals(Modes.Email))
			{

			}
		}
		public void Email_Click(object o, System.EventArgs e)
		{
			if (Mode.Equals(Modes.Email))
			{
				if (Page.IsValid)
				{
					Cambro.Web.Helpers.WriteAlertHeader();
					Cambro.Web.Helpers.WriteAlert("Starting bulk invite...", true);

					Cambro.Web.Helpers.WriteAlert("Parsing email addresses...", true);
					ArrayList matchedEmails = new ArrayList();
					string[] arr = System.Text.RegularExpressions.Regex.Replace(EmailTextBox.Text, @"[\s,]+", @" ").Split(' ');
					foreach (string c in arr)
					{
						if (System.Text.RegularExpressions.Regex.Match(c, Cambro.Misc.RegEx.Email).Success)
						{
							if (!matchedEmails.Contains(c.ToLower().Trim()))
								matchedEmails.Add(c.ToLower().Trim());
						}
					}
					Cambro.Web.Helpers.WriteAlert("Done parsing email addresses - found " + matchedEmails.Count + " emails...");
					int total = 0;
					int fail = 0;
					int done = 0;
					Cambro.Web.Helpers.WriteAlert("Sending invites...", true);
					int doneLevel = Cambro.Web.Helpers.WriteAlert("Done: 0", true);
					int failLevel = Cambro.Web.Helpers.WriteAlert("Failed: 0", true);
					Cambro.Web.Helpers.WriteAlert("...", true);
					foreach (string c in matchedEmails)
					{
						try
						{
							Usr u = Usr.GetOrCreateSkeletonUser(Usr.Current, c, "", CurrentGroup, EmailIntroTextBox.Text);
							GroupUsr gu = CurrentGroup.GetGroupUsr(u);
							Return r = CurrentGroup.Invite(u, gu, Usr.Current, CurrentGroupUsr, EmailIntroTextBox.Text, true);

							string matchNick = "";
							if (u.NickName.Length > 0)
								matchNick = " (" + u.NickName + ")";

							if (r.Success)
							{
								done++;
								Cambro.Web.Helpers.WriteAlert("Done: " + done, doneLevel);
								Cambro.Web.Helpers.WriteAlert(Cambro.Web.Helpers.StripHtml(r.MessageHtml));
								//EmailOutputP.InnerHtml += @"<img src=""/gfx/icon-tick-up.png"" border=""0"" height=""21"" width=""26"" align=""absmiddle"" style=""margin-right:3px;"">";
								//EmailOutputP.InnerHtml += r.MessageHtml;
							}
							else
							{
								fail++;
								Cambro.Web.Helpers.WriteAlert("Failed: " + fail, failLevel);
								Cambro.Web.Helpers.WriteAlert(Cambro.Web.Helpers.StripHtml(r.MessageHtml));
								//EmailOutputP.InnerHtml += @"<img src=""/gfx/icon-cross-up.png"" border=""0"" height=""21"" width=""26"" align=""absmiddle"" style=""margin-right:3px;"">";
								//EmailOutputP.InnerHtml += r.MessageHtml;
							}
						}
						catch
						{
							fail++;
							Cambro.Web.Helpers.WriteAlert("Failed: " + fail, failLevel);
							Cambro.Web.Helpers.WriteAlert(c + " failed: Exception");
							//EmailOutputP.InnerHtml += @"<img src=""/gfx/icon-cross-up.png"" border=""0"" height=""21"" width=""26"" align=""absmiddle"" style=""margin-right:3px;"">Exception";
						}
						
						//EmailOutputP.InnerHtml += "<br>";
						total++;
					}
					Cambro.Web.Helpers.WriteAlert("Done sending invites.", true);
					Cambro.Web.Helpers.WriteAlertFooter(CurrentGroup.UrlApp("admin","mode","email"));

				//	if (done > 0)
				//		EmailTextBox.Text = "";
				}
			}
		}
		public void EmailIntro_Val(object o, ServerValidateEventArgs e)
		{
			string s = Cambro.Web.Helpers.Strip(EmailIntroTextBox.Text, true, true, false, true);
			EmailIntroTextBox.Text = s;
			e.IsValid = s.Length > 10 && s.Length < 500;
		}

		#endregion
		#region MemberPanel
		private void MemberPanel_Load(object sender, System.EventArgs e)
		{
			MemberPanel.Visible = HasOptions && ShowOptions;
			if (HasOptions && ShowOptions)
			{
				if (!Page.IsPostBack)
				{
					MemberFavouriteCheckBox.Checked = CurrentGroupUsr != null && CurrentGroupUsr.Favourite;
				}
			}
		}
		protected void MemberFavouriteCheckBox_Change(object sender, System.EventArgs e)
		{
			if (HasOptions && CurrentGroupUsr.Favourite != MemberFavouriteCheckBox.Checked)
			{
				CurrentGroupUsr.Favourite = MemberFavouriteCheckBox.Checked;
				CurrentGroupUsr.Update();
			}
		}

		#endregion
		#region ModeratorPanel
		private void ModeratorPanel_Load(object sender, System.EventArgs e)
		{
			ModeratorRecommendedPanel.Visible = CurrentGroup.BrandK == 0;
			ModeratorPanel.Visible = ShowModerator;
		}

		public void ModeratorRecommendedAdd_Click(object o, System.EventArgs e)
		{
			if (ShowModerator)
			{
				if (CurrentGroup.BrandK > 0)
					throw new Exception("Not brand groups!!!");
				try
				{
					Event ev = this.uiEventPicker.Event;

					try
					{
						GroupEvent ge = new GroupEvent(CurrentGroup.K, ev.K);
					}
					catch
					{
						GroupEvent ge = new GroupEvent();
						ge.EventK = ev.K;
						ge.GroupK = CurrentGroup.K;
						ge.Update();
					}
				}
				catch
				{
					ModeratorRecommendedAddErrorP.Visible = true;
					return;
				}
				ModeratorRecommendedAddDoneP.Visible = true;
			}
		}
		protected Picker uiEventPicker, uiRemoveEventPicker;
		public void ModeratorRecommendedRemove_Click(object o, System.EventArgs e)
		{
			if (ShowModerator)
			{
				if (CurrentGroup.BrandK > 0)
					throw new Exception("Not brand groups!!!");
				try
				{
					Event ev = this.uiRemoveEventPicker.Event;
					try
					{
						GroupEvent ge = new GroupEvent(CurrentGroup.K, ev.K);
						ge.Delete();
					}
					catch
					{

					}
				}
				catch
				{
					ModeratorRecommendedRemoveErrorP.Visible = true;
					return;
				}
				ModeratorRecommendedRemoveDoneP.Visible = true;
			}
		}

	 

		#endregion
		#region NewsAdminPanel
		private void NewsAdminPanel_Load(object sender, System.EventArgs e)
		{
			NewsAdminPanel.Visible = ShowNewsAdmin;
			if (ShowNewsAdmin)
			{
				NewsDisableAllLinkButton.Attributes["onclick"] = "return confirm('Are you sure?');";
				BindNewsDataGrid();
				if (NewsThreadK > 0 && NewsThread.NewsStatus.Equals(Thread.NewsStatusEnum.Recommended))
				{
					ThreadSet ts = new ThreadSet(new Query(new Q(Thread.Columns.K, NewsThreadK)));
					NewsThreadRepeater.ItemTemplate = this.LoadTemplate("/Templates/Threads/GroupNewsAdmin.ascx");
					NewsThreadRepeater.DataSource = ts;
					NewsThreadRepeater.DataBind();
				}
				else
					NewsThreadPanel.Visible = false;
			}
		}
		public void NewsDisableAll(object o, System.EventArgs e)
		{
			if (ShowNewsAdmin)
			{
				Update u = new Update();

				u.Changes.Add(new Assign(Thread.Columns.IsNews, false));
				u.Changes.Add(new Assign(Thread.Columns.NewsLevel, 0));
				u.Changes.Add(new Assign(Thread.Columns.NewsModeratedByUsrK, Usr.Current.K));
				u.Changes.Add(new Assign(Thread.Columns.NewsModerationDateTime, DateTime.Now));
				u.Changes.Add(new Assign(Thread.Columns.NewsStatus, Thread.NewsStatusEnum.Done));

				u.Where = new And(
					new Q(Thread.Columns.NewsStatus, Thread.NewsStatusEnum.Recommended),
					new Q(Thread.Columns.GroupK, CurrentGroup.K));
				u.Table = TablesEnum.Thread;
				u.Run();

                // Set NewsThreadsPage = 1, fixes bug when paged to any other page and disabling all news items. Neil Sankey on 8/8/07
                NewsThreadsPage = 1;
				BindNewsDataGrid();
				NewsThreadPanel.Visible = false;
			}
		}
		#region NewsThread
		public Thread NewsThread
		{
			get
			{
				if (newsThread == null && NewsThreadK > 0)
					newsThread = new Thread(NewsThreadK);
				return newsThread;
			}
			set
			{
				newsThread = value;
			}
		}
		private Thread newsThread;
		#endregion
		#region NewsThreadK
		public int NewsThreadK
		{
			get
			{
				return ContainerPage.Url["ThreadK"];
			}
		}
		#endregion
		public void BindNewsDataGrid()
		{
			int perPage = 10;

			Query q = new Query();

			q.Paging.RecordsPerPage = perPage;
			q.Paging.RequestedPage = NewsThreadsPage;

			q.Columns = new ColumnSet(
				Thread.Columns.K,
				Thread.Columns.Private,
				Thread.Columns.GroupPrivate,
				Thread.Columns.PrivateGroup,
				Thread.Columns.Subject,
				Thread.Columns.LastPost,
				Thread.Columns.TotalComments,
				Thread.Columns.TotalParticipants,
				Thread.Columns.TotalWatching,
				Thread.Columns.IsNews,
				Thread.Columns.IsReview,
				Thread.Columns.ParentObjectType,
				Thread.Columns.ParentObjectK,
				Thread.Columns.GroupK,
				Thread.Columns.UsrK,
				new JoinedColumnSet(Thread.Columns.UsrK, Usr.LinkColumns),
				Thread.Columns.LastPostUsrK,
				new JoinedColumnSet(Thread.Columns.LastPostUsrK, Usr.LinkColumns)
				);

			q.OrderBy = new OrderBy(Thread.Columns.DateTime);

			q.TableElement = new TableElement(TablesEnum.Thread);

			q.TableElement = new Bobs.Join(
				q.TableElement,
				new TableElement(new Column(Thread.Columns.UsrK, Usr.Columns.K)),
				QueryJoinType.Left,
				Thread.Columns.UsrK,
				new Column(Thread.Columns.UsrK, Usr.Columns.K));

			q.TableElement = new Bobs.Join(
				q.TableElement,
				new TableElement(new Column(Thread.Columns.LastPostUsrK, Usr.Columns.K)),
				QueryJoinType.Left,
				Thread.Columns.LastPostUsrK,
				new Column(Thread.Columns.LastPostUsrK, Usr.Columns.K));

			q.QueryCondition = new And(new Q(Thread.Columns.NewsStatus, Thread.NewsStatusEnum.Recommended), new Q(Thread.Columns.GroupK, CurrentGroup.K));

			ThreadSet ts = new ThreadSet(q);

			if (ts.Count > 0)
			{
				NewsThreadsPage = ts.Paging.ReturnedPage;


				NewsDataGrid.DataSource = ts;
				NewsDataGrid.DataBind();


				Query qTot = new Query();
				qTot.QueryCondition = new And(new Q(Thread.Columns.NewsStatus, Thread.NewsStatusEnum.Recommended), new Q(Thread.Columns.GroupK, CurrentGroup.K));
				qTot.ReturnCountOnly = true;
				ThreadSet tCount = new ThreadSet(qTot);


				int endLinks = 4;
				int midLinks = 4;
				PageLinkWriter p = new PageLinkWriter();
				p.SetLastPage(perPage, tCount.Count);
				p.CurrentPageForLinks = NewsThreadsPage;
				p.Zones.Add(new PageLinkWriter.Zone(1, endLinks));
				p.Zones.Add(new PageLinkWriter.Zone(p.LastPage - endLinks + 1, p.LastPage));
				p.Zones.Add(new PageLinkWriter.Zone(NewsThreadsPage - midLinks, NewsThreadsPage + midLinks));
				StringBuilder sb = new StringBuilder();
				sb.Append("Pages: ");
				p.Build(new PageLinkWriter.LinkWriter(NewsPageLinkWriter), new PageLinkWriter.SeperatorWriter(NewsPageSeperatorWriter), sb);
				NewsPageP.Controls.Clear();
				if (p.LastPage > 1)
					NewsPageP.Controls.Add(new LiteralControl(sb.ToString()));
				else
					NewsPageP.Visible = false;
			}
			else
			{
				NewsDataGrid.Visible = false;
				NewsPageP.InnerHtml = "<small>No news suggestions</small>";
			}
		}
		#region NewsThreadsPage
		int NewsThreadsPage
		{
			get
			{
				if (newsThreadsPage == -1)
				{
					if (ContainerPage.Url["NewsPage"].IsInt)
						return ContainerPage.Url["NewsPage"];
					else
						return 1;
				}
				else
					return newsThreadsPage;
			}
			set
			{
				newsThreadsPage = value;
			}
		}
		int newsThreadsPage = -1;
		#endregion

		#region NewsPageSeperatorWriter
		public void NewsPageSeperatorWriter(int PreviousPage, int NextPage, StringBuilder Builder)
		{
			Builder.Append(" ... ");
		}
		#endregion
		#region NewsPageLinkWriter
		public void NewsPageLinkWriter(int Page, int CurrentPage, StringBuilder Builder)
		{
			if (CurrentPage == Page)
			{
				Builder.Append("<span class=\"CurrentPage\">");
			}
			else
			{
				Builder.Append("<a href=\"");
				if (Page > 1)
					Builder.Append(ContainerPage.Url.CurrentUrl("newspage", Page) + "#NewsDataGrid");
				else
					Builder.Append(ContainerPage.Url.CurrentUrl("newspage", null) + "#NewsDataGrid");
				Builder.Append("\">");
			}
			Builder.Append(Page.ToString());
			if (CurrentPage == Page)
			{
				Builder.Append("</span>");
			}
			else
			{
				Builder.Append("</a>");
			}
			Builder.Append(" ");
		}
		#endregion

		public void NewsDataGridChangePage(object o, DataGridPageChangedEventArgs e)
		{
			if (HasMemberAdmin)
			{
				NewsDataGrid.CurrentPageIndex = e.NewPageIndex;
				BindNewsDataGrid();
				ContainerPage.AnchorSkip("NewsDataGrid");
			}
		}
		#endregion
		#region MemberAdminPanel
		private void MemberAdminPanel_Load(object sender, System.EventArgs e)
		{
			MemberAdminPanel.Visible = ShowMemberAdmin;
			if (ShowMemberAdmin)
			{
				BindMemberAdminOptionsDataGrid();
				BindRequestsDataGrid();
				if (!Page.IsPostBack)
				{
					MemberAdminNewUserEmailsCheckBox.Checked = CurrentGroupUsr != null && CurrentGroupUsr.MemberAdminNewUserEmails;
				}
			}
		}
		public void MemberAdminNewUserEmailsCheckBox_Change(object o, System.EventArgs e)
		{
			if (CurrentGroupUsr != null && CurrentGroupUsr.MemberAdmin)
			{
				CurrentGroupUsr.MemberAdminNewUserEmails = MemberAdminNewUserEmailsCheckBox.Checked;
				CurrentGroupUsr.Update();
				MemberAdminNewUserEmailsCheckBoxStatusPanel.Visible = true;
			}
			else
			{
				throw new DsiUserFriendlyException("You can't set this without being a member of the group!");
			}

		}
		#region Invite
		protected void MemberInvite(object sender, System.EventArgs e)
		{
			if (HasMemberAdmin)
			{
				if (this.uiMemberAdminInviteAutoComplete.Value.Length > 0)
				{
					int usrK = int.Parse(this.uiMemberAdminInviteAutoComplete.Value);
					Usr targetUsr = new Usr(usrK);
					GroupUsr targetGroupUsr = CurrentGroup.GetGroupUsr(targetUsr);
					Return r = CurrentGroup.Invite(targetUsr, targetGroupUsr, Usr.Current, CurrentGroupUsr, "", false);
					MemberInviteLabel.Text = (r.Success ? "Success: " : "Error: ") + r.MessageHtml;
				}
				else
					MemberInviteLabel.Text = "Please choose someone from the drop-down above!";
			}
		}
		#endregion
		#region Requests
		#region MemberAdminRequestsUpdate_Click
		protected void MemberAdminRequestsUpdate_Click(object sender, System.EventArgs e)
		{
			if (HasMemberAdmin)
			{
				Query q = new Query();
				q.NoLock = true;
				q.QueryCondition = new And(
					new Q(GroupUsr.Columns.GroupK, CurrentGroup.K),
					new Or(
						new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.Request),
						new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.Recommend)
					)
				);
				GroupUsrSet gus = new GroupUsrSet(q);

				MemberAdminRequestsUpdateResultsP.InnerHtml = "";
				bool doneOne = false;
				foreach (GroupUsr gu in gus)
				{
					try
					{
						CheckBox accCb = (CheckBox)Cambro.Web.Helpers.SearchControl(MemberAdminRequestsDataGrid, "ReqAccUsrK" + gu.UsrK.ToString());
						CheckBox rejCb = (CheckBox)Cambro.Web.Helpers.SearchControl(MemberAdminRequestsDataGrid, "ReqRejUsrK" + gu.UsrK.ToString());
						if (accCb != null && rejCb != null)
						{
							if (accCb.Checked != rejCb.Checked)
							{
								if (accCb.Checked)
								{
									doneOne = true;
									Return r = CurrentGroup.Invite(gu.Usr, gu, Usr.Current, CurrentGroupUsr, "", false);
									MemberAdminRequestsUpdateResultsP.InnerHtml += (MemberAdminRequestsUpdateResultsP.InnerHtml.Length > 0 ? "<br>" : "") + "Accepting " + gu.Usr.Link() + " - " + (r.Success ? "Done." : "Error: " + r.MessageHtml);
								}
								else if (rejCb.Checked)
								{
									doneOne = true;
									Return r = CurrentGroup.Reject(gu.Usr, gu, Usr.Current, CurrentGroupUsr);
									MemberAdminRequestsUpdateResultsP.InnerHtml += (MemberAdminRequestsUpdateResultsP.InnerHtml.Length > 0 ? "<br>" : "") + "Rejecting " + gu.Usr.Link() + " - " + (r.Success ? "Done." : "Error: " + r.MessageHtml);
								}
							}
						}
					}
					catch { }
				}
				if (doneOne)
				{
					CurrentGroup.UpdateTotalMembers();
					MemberAdminRequestsDataGrid.CurrentPageIndex = 0;
					BindRequestsDataGrid();
					ContainerPage.AnchorSkip("MemberAdminRequests");
				}
			}
		}
		#endregion
		#region BindRequestsDataGrid
		void BindRequestsDataGrid()
		{
			if (HasMemberAdmin)
			{
				Query q = new Query();
				q.QueryCondition = new And(
					new Q(GroupUsr.Columns.GroupK, CurrentGroup.K),
					new Or(
						new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.Request),
						new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.Recommend)
					)
				);
				q.Columns = new ColumnSet(
					GroupUsr.Columns.Status,
					GroupUsr.Columns.Moderator,
					GroupUsr.Columns.NewsAdmin,
					GroupUsr.Columns.MemberAdmin,
					GroupUsr.Columns.Owner,
					GroupUsr.Columns.StatusChangeDateTime,
					GroupUsr.Columns.UsrK,
					GroupUsr.Columns.InviteUsrK,
					new JoinedColumnSet(
						GroupUsr.Columns.UsrK,
						Usr.LinkColumns
					),
					GroupUsr.Columns.StatusChangeUsrK,
					new JoinedColumnSet(
						GroupUsr.Columns.StatusChangeUsrK,
						Usr.LinkColumns
					),
					new JoinedColumnSet(
						GroupUsr.Columns.InviteUsrK,
						Usr.LinkColumns
					)
				);
				q.TableElement = GroupUsr.UsrAndStatusChangeUsrJoin;
				q.OrderBy = new OrderBy(GroupUsr.Columns.StatusChangeDateTime, OrderBy.OrderDirection.Descending);
				GroupUsrSet gus = new GroupUsrSet(q);
				MemberAdminRequestsDataGridPanel.Visible = gus.Count > 0;
				MemberAdminRequestsNoResultsP.Visible = gus.Count == 0;
				if (gus.Count > 0)
				{
					MemberAdminRequestsDataGrid.AllowPaging = (gus.Count > MemberAdminRequestsDataGrid.PageSize);
					MemberAdminRequestsDataGrid.DataSource = gus;
					MemberAdminRequestsDataGrid.DataBind();
				}
			}
		}
		public void MemberAdminRequestsDataGridChangePage(object o, DataGridPageChangedEventArgs e)
		{
			if (HasMemberAdmin)
			{
				MemberAdminRequestsDataGrid.CurrentPageIndex = e.NewPageIndex;
				BindRequestsDataGrid();
				ContainerPage.AnchorSkip("MemberAdminRequests");
			}
		}
		#endregion
		#region DataBinders
		public void MemberAdminRequestsDataGridCheckBoxDataBindAccept(object sender, System.EventArgs e)
		{
			CheckBox cb = (CheckBox)sender;
			GroupUsr gu = (GroupUsr)((DataGridItem)((CheckBox)sender).NamingContainer).DataItem;
			cb.ID = "ReqAccUsrK" + gu.UsrK.ToString();
			#region on tick, un-tick the other box
			string acceptId = cb.NamingContainer.ClientID + "_" + "ReqAccUsrK" + gu.UsrK.ToString();
			string rejectId = cb.NamingContainer.ClientID + "_" + "ReqRejUsrK" + gu.UsrK.ToString();
			cb.Attributes["onclick"] = "if (document.getElementById('" + rejectId + "').checked){document.getElementById('" + rejectId + "').checked=false;};";
			#endregion
		}
		public void MemberAdminRequestsDataGridCheckBoxDataBindReject(object sender, System.EventArgs e)
		{
			CheckBox cb = (CheckBox)sender;
			GroupUsr gu = (GroupUsr)((DataGridItem)((CheckBox)sender).NamingContainer).DataItem;
			cb.ID = "ReqRejUsrK" + gu.UsrK.ToString();
			#region on tick, un-tick the other box
			string acceptId = cb.NamingContainer.ClientID + "_" + "ReqAccUsrK" + gu.UsrK.ToString();
			string rejectId = cb.NamingContainer.ClientID + "_" + "ReqRejUsrK" + gu.UsrK.ToString();
			cb.Attributes["onclick"] = "if (document.getElementById('" + acceptId + "').checked){document.getElementById('" + acceptId + "').checked=false;};";
			#endregion
		}
		#endregion
		#endregion
		#region Options
		void BindMemberAdminOptionsDataGrid()
		{
			if (HasMemberAdmin)
			{
				#region Status level Q
				ArrayList alStatusLevels = new ArrayList();
				if (MemberAdminOptionsMembersCheckBox.Checked)
				{
					alStatusLevels.Add(new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.Member));
				}
				if (MemberAdminOptionsRequestCheckBox.Checked)
				{
					alStatusLevels.Add(new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.Request));
					alStatusLevels.Add(new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.Recommend));
				}
				if (MemberAdminOptionsInvitedCheckBox.Checked)
				{
					alStatusLevels.Add(new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.Invite));
					alStatusLevels.Add(new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.InviteRejected));
				}
				if (MemberAdminOptionsRejectedCheckBox.Checked)
				{
					alStatusLevels.Add(new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.RecommendRejected));
					alStatusLevels.Add(new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.RequestRejected));
				}
				if (MemberAdminOptionsExitedCheckBox.Checked)
				{
					alStatusLevels.Add(new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.Exited));
				}
				if (MemberAdminOptionsBarredCheckBox.Checked)
				{
					alStatusLevels.Add(new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.Barred));
				}
				Q qStatusLevels = new Q(false);
				MemberAdminOptionsSearchNoneSelected.Visible = alStatusLevels.Count == 0 && MemberAdminOptionsSearchClicked;
				if (alStatusLevels.Count > 0)
				{
					qStatusLevels = new Or((Q[])alStatusLevels.ToArray(typeof(Q)));
				}
				#endregion
				#region Nickname Q
				Q qNickName = new Q(true);
				if (MemberAdminOptionsSearchTextBox.Text.Trim().Length > 0)
				{
					qNickName = new Q(new Column(GroupUsr.Columns.UsrK, Usr.Columns.NickName), QueryOperator.TextContains, MemberAdminOptionsSearchTextBox.Text.Trim());
				}
				#endregion

				Query q = new Query();
				q.QueryCondition = new And(
					new Q(GroupUsr.Columns.GroupK, CurrentGroup.K),
					qStatusLevels,
					qNickName
				);
				q.Columns = new ColumnSet(
					GroupUsr.Columns.UsrK,
					GroupUsr.Columns.Status,
					GroupUsr.Columns.Moderator,
					GroupUsr.Columns.NewsAdmin,
					GroupUsr.Columns.MemberAdmin,
					GroupUsr.Columns.Owner,
					GroupUsr.Columns.StatusChangeDateTime,
					GroupUsr.Columns.StatusChangeUsrK,
					GroupUsr.Columns.InviteUsrK,
					new JoinedColumnSet(
						GroupUsr.Columns.UsrK,
						Usr.LinkColumns
					),
					new JoinedColumnSet(
						GroupUsr.Columns.StatusChangeUsrK,
						Usr.LinkColumns
					),
					new JoinedColumnSet(
						GroupUsr.Columns.InviteUsrK,
						Usr.LinkColumns
					)
				);
				q.TableElement = GroupUsr.UsrAndStatusChangeUsrJoin;
				q.OrderBy = new OrderBy(new OrderBy(GroupUsr.Columns.StatusChangeDateTime, OrderBy.OrderDirection.Descending), new OrderBy(new Column(GroupUsr.Columns.UsrK, Usr.Columns.NickName)));
				GroupUsrSet gus = new GroupUsrSet(q);

				MemberAdminOptionsSearchNoResults.Visible = gus.Count == 0 && alStatusLevels.Count > 0;
				MemberAdminOptionsDataGridP.Visible = gus.Count > 0;
				if (gus.Count > 0)
				{
					MemberAdminOptionsDataGrid.AllowPaging = (gus.Count > MemberAdminOptionsDataGrid.PageSize);
					MemberAdminOptionsDataGrid.DataSource = gus;
					MemberAdminOptionsDataGrid.DataBind();
				}
			}
		}
		protected void MemberAdminOptionsSearch_Click(object sender, System.EventArgs e)
		{
			if (HasMemberAdmin)
			{
				MemberAdminOptionsSearchClicked = true;
				MemberAdminOptionsDataGrid.CurrentPageIndex = 0;
				BindMemberAdminOptionsDataGrid();
				ContainerPage.AnchorSkip("MemberOptions");
			}
		}
		bool MemberAdminOptionsSearchClicked
		{
			get
			{
				if (this.ViewState["MemberAdminOptionsSearchClicked"] == null)
					return false;
				else
					return (bool)this.ViewState["MemberAdminOptionsSearchClicked"];
			}
			set
			{
				this.ViewState["MemberAdminOptionsSearchClicked"] = value;
			}
		}
		public void MemberAdminOptionsDataGridChangePage(object o, DataGridPageChangedEventArgs e)
		{
			if (HasMemberAdmin)
			{
				MemberAdminOptionsDataGrid.CurrentPageIndex = e.NewPageIndex;
				BindMemberAdminOptionsDataGrid();
				ContainerPage.AnchorSkip("MemberOptions");
			}
		}
		protected void MemberAdminOptionsDataGridItemCommand(object sender, DataGridCommandEventArgs e)
		{
			if (HasMemberAdmin)
			{
				if (
					e.CommandName.Equals("accept") ||
					e.CommandName.Equals("invite") ||
					e.CommandName.Equals("reject") ||
					e.CommandName.Equals("bar") ||
					e.CommandName.Equals("unbar"))
				{
					GroupUsr gu = new GroupUsr(int.Parse(e.CommandArgument.ToString()), CurrentGroup.K);

					if (e.CommandName.Equals("accept"))
					{
						if (gu.MemberAdminPermission.Accept)
						{
							CurrentGroup.Accept(gu.Usr, gu, Usr.Current, CurrentGroupUsr);
						}
					}
					else if (e.CommandName.Equals("invite"))
					{
						if (gu.MemberAdminPermission.Invite)
						{
							CurrentGroup.Invite(
								gu.Usr,
								gu,
								Usr.Current,
								CurrentGroupUsr,
								"",
								false);
						}
					}
					else if (e.CommandName.Equals("reject"))
					{
						if (gu.MemberAdminPermission.Reject)
						{
							CurrentGroup.Reject(gu.Usr, gu, Usr.Current, CurrentGroupUsr);
						}
					}
					else if (e.CommandName.Equals("bar"))
					{
						if (gu.MemberAdminPermission.Bar)
						{
							CurrentGroup.Bar(gu.Usr, gu, Usr.Current, CurrentGroupUsr);
						}
					}
					else if (e.CommandName.Equals("unbar"))
					{
						if (gu.MemberAdminPermission.UnBar)
						{
							CurrentGroup.UnBar(gu.Usr, gu, Usr.Current, CurrentGroupUsr);
						}
					}
					BindMemberAdminOptionsDataGrid();
					ContainerPage.AnchorSkip("MemberOptions");
				}
			}
		}
		protected void MemberAdminOptionsDataGridItemDataBound(object o, DataGridItemEventArgs e)
		{
			if (e.Item.DataItem is GroupUsr)
			{
				GroupUsr gu = (GroupUsr)e.Item.DataItem;
				e.Item.ID = "UsrK" + gu.UsrK.ToString();
				gu.AddMemberAdminOptions(e.Item.Cells[e.Item.Cells.Count - 1]);
			}
		}
		#endregion
		#endregion
		#region OwnerPanel
		private void OwnerPanel_Load(object sender, System.EventArgs e)
		{
			this.uiOwnerModeratorsAutoComplete.Parameters.Add("groupK", CurrentGroup.K.ToString());
			OwnerPanel.Visible = ShowOwner;
			if (ShowOwner)
			{
				BindModeratorsDataGrid();

			}
		}
		#region OwnerModeratorsAdd
		protected void OwnerModeratorsAdd(object sender, System.EventArgs e)
		{
			if (HasOwner)
			{
				if (this.uiOwnerModeratorsAutoComplete.Value.Length > 0)
				{
					int usrK = int.Parse(this.uiOwnerModeratorsAutoComplete.Value);
					Usr u = new Usr(usrK);
					GroupUsr gu = CurrentGroup.GetGroupUsr(u);
					if (gu != null && gu.IsMember && !gu.Moderator)
					{
						gu.ChangeModeratorPermission(true, false, false, false);
						CurrentGroup.UpdateTotalMembers();
						BindModeratorsDataGrid();
					}
				}
			}
		}
		#endregion
		#region BindModeratorsDataGrid
		void BindModeratorsDataGrid()
		{
			if (HasOwner)
			{
				Query q = new Query();
				q.QueryCondition = new And(
					new Q(GroupUsr.Columns.GroupK, CurrentGroup.K),
					new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.Member),
					new Q(GroupUsr.Columns.Moderator, true)
				);
				q.Columns = new ColumnSet(
					GroupUsr.Columns.UsrK,
					GroupUsr.Columns.Status,
					GroupUsr.Columns.Moderator,
					GroupUsr.Columns.NewsAdmin,
					GroupUsr.Columns.MemberAdmin,
					GroupUsr.Columns.Owner,
					new JoinedColumnSet(
						GroupUsr.Columns.UsrK,
						Usr.LinkColumns
					)
				);
				q.TableElement = GroupUsr.UsrJoin;
				q.OrderBy = new OrderBy(new Column(GroupUsr.Columns.UsrK, Usr.Columns.NickName));
				GroupUsrSet gus = new GroupUsrSet(q);
				if (gus.Count > 0)
				{
					OwnerModeratorsDataGrid.AllowPaging = (gus.Count > OwnerModeratorsDataGrid.PageSize);
					OwnerModeratorsDataGrid.DataSource = gus;
					OwnerModeratorsDataGrid.DataBind();
				}
			}
		}
		#endregion
		#region OwnerModeratorsDataGridChangePage
		public void OwnerModeratorsDataGridChangePage(object o, DataGridPageChangedEventArgs e)
		{
			if (HasOwner)
			{
				OwnerModeratorsDataGrid.CurrentPageIndex = e.NewPageIndex;
				BindModeratorsDataGrid();
			}
		}
		#endregion
		#region CheckBox databinders
		public void OwnerModeratorsDataGridCheckBoxDataBindMod(object sender, System.EventArgs e)
		{
			CheckBox cb = (CheckBox)sender;
			GroupUsr gu = (GroupUsr)((DataGridItem)((CheckBox)sender).NamingContainer).DataItem;
			cb.ID = "ModUsrK" + gu.UsrK.ToString();
			if (!Usr.Current.IsAdmin && Usr.Current.K == gu.UsrK)
				cb.Enabled = false;
			else
			{
				#region on un-tick, un-tick all the other 3 boxes
				string modId = cb.NamingContainer.ClientID + "_" + "ModUsrK" + gu.UsrK.ToString();
				string newsId = cb.NamingContainer.ClientID + "_" + "NewsUsrK" + gu.UsrK.ToString();
				string memberId = cb.NamingContainer.ClientID + "_" + "MemberUsrK" + gu.UsrK.ToString();
				string ownerId = cb.NamingContainer.ClientID + "_" + "OwnerUsrK" + gu.UsrK.ToString();
				cb.Attributes["onclick"] = "if (!document.getElementById('" + modId + "').checked){document.getElementById('" + newsId + "').checked=false;document.getElementById('" + memberId + "').checked=false;document.getElementById('" + ownerId + "').checked=false;};";
				#endregion
			}
			cb.Checked = (gu.Moderator || gu.NewsAdmin || gu.MemberAdmin || gu.Owner);
		}
		public void OwnerModeratorsDataGridCheckBoxDataBindNews(object sender, System.EventArgs e)
		{
			CheckBox cb = (CheckBox)sender;
			GroupUsr gu = (GroupUsr)((DataGridItem)((CheckBox)sender).NamingContainer).DataItem;
			cb.ID = "NewsUsrK" + gu.UsrK.ToString();
			if (!Usr.Current.IsAdmin && Usr.Current.K == gu.UsrK)
				cb.Enabled = false;
			else
			{
				#region on un-tick, un-tick owner. On tick, tick mod
				string modId = cb.NamingContainer.ClientID + "_" + "ModUsrK" + gu.UsrK.ToString();
				string newsId = cb.NamingContainer.ClientID + "_" + "NewsUsrK" + gu.UsrK.ToString();
				string memberId = cb.NamingContainer.ClientID + "_" + "MemberUsrK" + gu.UsrK.ToString();
				string ownerId = cb.NamingContainer.ClientID + "_" + "OwnerUsrK" + gu.UsrK.ToString();
				cb.Attributes["onclick"] = "if (document.getElementById('" + newsId + "').checked){document.getElementById('" + modId + "').checked=true;}else{document.getElementById('" + ownerId + "').checked=false;};";
				#endregion
			}
			cb.Checked = (gu.NewsAdmin || gu.Owner);
		}
		public void OwnerModeratorsDataGridCheckBoxDataBindMember(object sender, System.EventArgs e)
		{
			CheckBox cb = (CheckBox)sender;
			GroupUsr gu = (GroupUsr)((DataGridItem)((CheckBox)sender).NamingContainer).DataItem;
			cb.ID = "MemberUsrK" + gu.UsrK.ToString();
			if (!Usr.Current.IsAdmin && Usr.Current.K == gu.UsrK)
				cb.Enabled = false;
			else
			{
				#region on un-tick, un-tick owner. On tick, tick mod
				string modId = cb.NamingContainer.ClientID + "_" + "ModUsrK" + gu.UsrK.ToString();
				string newsId = cb.NamingContainer.ClientID + "_" + "NewsUsrK" + gu.UsrK.ToString();
				string memberId = cb.NamingContainer.ClientID + "_" + "MemberUsrK" + gu.UsrK.ToString();
				string ownerId = cb.NamingContainer.ClientID + "_" + "OwnerUsrK" + gu.UsrK.ToString();
				cb.Attributes["onclick"] = "if (document.getElementById('" + memberId + "').checked){document.getElementById('" + modId + "').checked=true;}else{document.getElementById('" + ownerId + "').checked=false;};";
				#endregion
			}
			cb.Checked = (gu.MemberAdmin || gu.Owner);
		}
		public void OwnerModeratorsDataGridCheckBoxDataBindOwner(object sender, System.EventArgs e)
		{
			CheckBox cb = (CheckBox)sender;
			GroupUsr gu = (GroupUsr)((DataGridItem)((CheckBox)sender).NamingContainer).DataItem;
			cb.ID = "OwnerUsrK" + gu.UsrK.ToString();
			if (!Usr.Current.IsAdmin && Usr.Current.K == gu.UsrK)
				cb.Enabled = false;
			else
			{
				#region on tick, tick all the other 3 boxes
				string modId = cb.NamingContainer.ClientID + "_" + "ModUsrK" + gu.UsrK.ToString();
				string newsId = cb.NamingContainer.ClientID + "_" + "NewsUsrK" + gu.UsrK.ToString();
				string memberId = cb.NamingContainer.ClientID + "_" + "MemberUsrK" + gu.UsrK.ToString();
				string ownerId = cb.NamingContainer.ClientID + "_" + "OwnerUsrK" + gu.UsrK.ToString();
				cb.Attributes["onclick"] = "if (document.getElementById('" + ownerId + "').checked){document.getElementById('" + modId + "').checked=true;document.getElementById('" + newsId + "').checked=true;document.getElementById('" + memberId + "').checked=true;};";
				#endregion
			}
			cb.Checked = (gu.Owner);
		}
		#endregion
		#region OwnerModeratorsUpdate
		protected void OwnerModeratorsUpdate(object sender, System.EventArgs e)
		{
			if (HasOwner)
			{
				Query q = new Query();
				q.NoLock = true;
				q.QueryCondition = CurrentGroup.ModeratorQ;
				GroupUsrSet gus = new GroupUsrSet(q);

				#region check for at least one owner!!!
				bool gotOwner = false;
				foreach (GroupUsr gu in gus)
				{
					CheckBox ownerCb = (CheckBox)Cambro.Web.Helpers.SearchControl(OwnerModeratorsDataGrid, "OwnerUsrK" + gu.UsrK.ToString());
					if (ownerCb != null)
					{
						if (ownerCb.Checked)
						{
							gotOwner = true;
						}
					}
					else
					{
						if (gu.Owner)
						{
							gotOwner = true;
						}
					}
				}
				#endregion
				if (!gotOwner)
					OnwerModeratorError.Visible = true;
				else
				{
					bool removedMod = false;
					foreach (GroupUsr gu in gus)
					{
						try
						{
							if (Usr.Current.IsAdmin || gu.UsrK != Usr.Current.K)
							{
								CheckBox modCb = (CheckBox)Cambro.Web.Helpers.SearchControl(OwnerModeratorsDataGrid, "ModUsrK" + gu.UsrK.ToString());
								CheckBox newsCb = (CheckBox)Cambro.Web.Helpers.SearchControl(OwnerModeratorsDataGrid, "NewsUsrK" + gu.UsrK.ToString());
								CheckBox memberCb = (CheckBox)Cambro.Web.Helpers.SearchControl(OwnerModeratorsDataGrid, "MemberUsrK" + gu.UsrK.ToString());
								CheckBox ownerCb = (CheckBox)Cambro.Web.Helpers.SearchControl(OwnerModeratorsDataGrid, "OwnerUsrK" + gu.UsrK.ToString());
								if (modCb != null && newsCb != null && memberCb != null && ownerCb != null)
								{
									bool prevOwner = gu.Owner;
									bool prevMemberAdmin = gu.MemberAdmin;
									bool prevNewsAdmin = gu.NewsAdmin;
									bool prevModerator = gu.Moderator;
									if (ownerCb.Checked)
									{
										gu.ChangeModeratorPermission(true, true, true, true);
									}
									else if (memberCb.Checked || newsCb.Checked)
									{
										gu.ChangeModeratorPermission(true, newsCb.Checked, memberCb.Checked, false);
									}
									else if (modCb.Checked)
									{
										gu.ChangeModeratorPermission(true, false, false, false);
									}
									else
									{
										removedMod = true;
										gu.ChangeModeratorPermission(false, false, false, false);
									}
								}
							}
						}
						catch { }
					}

					CurrentGroup.UpdateTotalMembers();
					if (removedMod)
						BindModeratorsDataGrid();

					OnwerModeratorDone.Visible = true;
				}

			}
		}
		#endregion
		#endregion

		#region Permissions
		#region Moderator
		public bool HasModerator
		{
			get
			{
				return Usr.Current.CanGroupModerator(CurrentGroupUsr);
			}
		}
		public bool ShowModerator
		{
			get
			{
				return HasModerator && Mode.Equals(Modes.ModeratorOptions);
			}
		}
		#endregion
		#region NewsAdmin
		public bool HasNewsAdmin
		{
			get
			{
				return Usr.Current.CanGroupNewsAdmin(CurrentGroupUsr);
			}
		}
		public bool ShowNewsAdmin
		{
			get
			{
				return HasNewsAdmin && Mode.Equals(Modes.NewsAdmin);
			}
		}
		#endregion
		#region MemberAdmin
		public bool HasMemberAdmin
		{
			get
			{
				return Usr.Current.CanGroupMemberAdmin(CurrentGroupUsr);
			}
		}
		public bool ShowMemberAdmin
		{
			get
			{
				return HasMemberAdmin && Mode.Equals(Modes.MemberAdmin);
			}
		}
		#endregion
		#region Owner
		public bool HasOwner
		{
			get
			{
				return Usr.Current.CanGroupOwner(CurrentGroupUsr);
			}
		}
		public bool ShowOwner
		{
			get
			{
				return HasOwner && Mode.Equals(Modes.OwnerOptions);
			}
		}
		#endregion
		#region HasOptions
		public bool HasOptions
		{
			get
			{
				return (CurrentGroupUsr != null && CurrentGroupUsr.IsMember);
			}
		}
		#endregion
		#region ShowOptions
		public bool ShowOptions
		{
			get
			{
				return (Mode.Equals(Modes.MemberOptions));
			}
		}
		#endregion
		#endregion

		#region CurrentGroupUsr
		public GroupUsr CurrentGroupUsr
		{
			get
			{
				if (!gotGroupUsr)
				{
					gotGroupUsr = true;
					currentGroupUsr = CurrentGroup.GetGroupUsr(Usr.Current);
				}
				return currentGroupUsr;
			}
		}
		GroupUsr currentGroupUsr;
		bool gotGroupUsr = false;
		#endregion

		#region CurrentGroup
		public Group CurrentGroup
		{
			get
			{
				return ContainerPage.Url.ObjectFilterGroup;
			}
		}
		#endregion

		#region PageMode
		Modes Mode
		{
			get
			{
				if (ContainerPage.Url["mode"].Equals("options"))
					return Modes.MemberOptions;
				else if (ContainerPage.Url["mode"].Equals("moderator"))
					return Modes.ModeratorOptions;
				else if (ContainerPage.Url["mode"].Equals("news"))
					return Modes.NewsAdmin;
				else if (ContainerPage.Url["mode"].Equals("membership"))
					return Modes.MemberAdmin;
				else if (ContainerPage.Url["mode"].Equals("owner"))
					return Modes.OwnerOptions;
				else if (ContainerPage.Url["mode"].Equals("buddies"))
					return Modes.Buddies;
				else if (ContainerPage.Url["mode"].Equals("email"))
					return Modes.Email;
				else
					return Modes.Options;
			}
		}
		public enum Modes
		{
			Options,
			OwnerOptions,
			MemberAdmin,
			NewsAdmin,
			ModeratorOptions,
			MemberOptions,
			Buddies,
			Email
		}
		#endregion

		#region ChangePanel
		void ChangePanel(Panel p)
		{
			PanelOptions.Visible = p.Equals(PanelOptions);
		}
		#endregion
	}
}
