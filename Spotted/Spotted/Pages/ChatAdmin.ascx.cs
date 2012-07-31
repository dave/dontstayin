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
using System.Data.SqlClient;

namespace Spotted.Pages
{
	public partial class ChatAdmin : DsiUserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn();
			if (!Page.IsPostBack)
			{
				if (Mode.Equals(Modes.None))
					ChangePanel(PanelOptions);
			}
		}

		#region PanelOptions
		protected Panel AdminPanel;
		private void PanelOptions_Load(object sender, System.EventArgs e)
		{
			if (Mode.Equals(Modes.None))
			{
				ThreadSubjectAnchor.HRef = CurrentThread.Url();
				ThreadSubjectAnchor.InnerText = CurrentThread.Subject;
				CurrentThread.MakeRollover(ThreadSubjectAnchor);

				if (CurrentThread.ParentObjectType.Equals(Model.Entities.ObjectType.None) || CurrentThread.ParentObjectType.Equals(Model.Entities.ObjectType.Group))
				{
					ThreadForumAnchor.InnerHtml = "<small>none</small>";
				}
				else
				{
					ThreadForumAnchor.HRef = ((IPage)CurrentThread.ParentForumObject).Url();
					ThreadForumAnchor.InnerText = ((IReadableReference)CurrentThread.ParentForumObject).ReadableReference;
				}

				if (CurrentThread.GroupK > 0)
				{
					ThreadGroupAnchor.HRef = CurrentThread.Group.Url();
					ThreadGroupAnchor.InnerText = CurrentThread.Group.FriendlyName;
					PrivateCheckBox.Text = "Group private <small> - only people who are members of the group can read the topic</small>";
				}
				else
					ThreadGroupAnchor.InnerHtml = "<small>none</small>";



				if (!Page.IsPostBack)
				{
					UpdateOptionsControls();
				}

				if (!CurrentUsrCanClose)
					ClosedSpan.Visible = false;

				AdminPanel.Visible = Usr.Current != null && Usr.Current.IsAdmin;
				if (Usr.Current != null && Usr.Current.IsAdmin)
				{
					AdminPanel.Visible =
						!CurrentThread.Private &&
						!CurrentThread.PrivateGroup &&
						!CurrentThread.GroupPrivate &&
						CurrentThread.ParentObjectType == Model.Entities.ObjectType.Article &&
						((IHasPrimaryThread)(CurrentThread.Parent)).ThreadK != CurrentThread.K;
				}
			}
		}
		#region UpdateOptionsControls()
		void UpdateOptionsControls()
		{
			SealedCheckBox.Checked = CurrentThread.Sealed;
			ClosedCheckBox.Checked = CurrentThread.Closed;

			NewsCheckBox.Checked = CurrentThread.NewsStatus.Equals(Thread.NewsStatusEnum.Recommended) || CurrentThread.IsNews;
			
			if (CurrentThread.GroupK > 0)
				PrivateCheckBox.Checked = CurrentThread.GroupPrivate;
			else
				PrivateCheckBox.Checked = CurrentThread.Private;

			#region showPrivate
			bool showPrivate = true;

			if (CurrentThread.ParentObjectType.Equals(Model.Entities.ObjectType.Photo))
				showPrivate = CurrentThread.GroupK > 0;

			if (CurrentThread.IsReview)
				showPrivate = false;

			if (CurrentThread.NewsStatus.Equals(Thread.NewsStatusEnum.Recommended)
				|| CurrentThread.NewsStatus.Equals(Thread.NewsStatusEnum.Done))
				showPrivate = false;
			#endregion

			#region showSealed
			bool showSealed = CurrentThread.Private;
			#endregion

			#region showNews
			bool showNews = true;

			if (CurrentThread.Private)
				showNews = false;

			if (CurrentThread.IsReview)
				showNews = false;

			if (CurrentThread.NewsStatus.Equals(Thread.NewsStatusEnum.Recommended))
			{
				if (Usr.Current.K != CurrentThread.NewsUsrK && !CurrentUsrIsNewsMod)
				{
					showNews = false;
				}
			}
			else if (CurrentThread.NewsStatus.Equals(Thread.NewsStatusEnum.Done))
			{
				if (!CurrentUsrIsNewsMod)
				{
					showNews = false;
				}
			}
			#endregion

			Cambro.Web.Helpers.ChangeState(NewsSpan, NewsCheckBox, showNews);
			
			Cambro.Web.Helpers.ChangeState(SealedSpan, SealedCheckBox, showSealed);
			Cambro.Web.Helpers.ChangeState(PrivateSpan, PrivateCheckBox, showPrivate);

			ChangeForumPanel.Visible = !(CurrentThread.ParentObjectType.Equals(Model.Entities.ObjectType.Photo) || CurrentThread.IsReview);


		}
		#endregion
		#region UpdateOptions_Click
		public void UpdateOptions_Click(object o, System.EventArgs e)
		{

			#region showPrivate
			bool showPrivate = true;

			if (CurrentThread.ParentObjectType.Equals(Model.Entities.ObjectType.Photo))
				showPrivate = CurrentThread.GroupK > 0;

			if (CurrentThread.IsReview)
				showPrivate = false;

			if (CurrentThread.NewsStatus.Equals(Thread.NewsStatusEnum.Recommended)
				|| CurrentThread.NewsStatus.Equals(Thread.NewsStatusEnum.Done))
				showPrivate = false;
			#endregion

			#region showSealed
			bool showSealed = CurrentThread.Private;
			#endregion

			#region showNews
			bool showNews = true;

			if (CurrentThread.Private)
				showNews = false;

			if (CurrentThread.IsReview)
				showNews = false;

			if (CurrentThread.NewsStatus.Equals(Thread.NewsStatusEnum.Recommended))
			{
				if (Usr.Current.K != CurrentThread.NewsUsrK && !CurrentUsrIsNewsMod)
				{
					showNews = false;
				}
			}
			else if (CurrentThread.NewsStatus.Equals(Thread.NewsStatusEnum.Done))
			{
				if (!CurrentUsrIsNewsMod)
				{
					showNews = false;
				}
			}
			#endregion

			if (CurrentUsrCanClose)
			{
				CurrentThread.Closed = ClosedCheckBox.Checked;
			}

			if (showPrivate)
			{
				if (CurrentThread.GroupK > 0)
				{
					CurrentThread.GroupPrivate = PrivateCheckBox.Checked;
				}
				else
				{
					CurrentThread.Private = PrivateCheckBox.Checked;
					if (PrivateCheckBox.Checked)
					{
						#region reset news status
						CurrentThread.IsNews = false;
						CurrentThread.NewsLevel = 0;
						CurrentThread.NewsModeratedByUsrK = 0;
						CurrentThread.NewsModerationDateTime = DateTime.MinValue;
						CurrentThread.NewsModeratorUsrK = 0;
						CurrentThread.NewsStatus = Thread.NewsStatusEnum.None;
						CurrentThread.NewsUsrK = 0;
						#endregion
					}
				}
			}

			if (showSealed)
				CurrentThread.Sealed = SealedCheckBox.Checked;

			if (showNews)
			{
				if (NewsCheckBox.Checked)
				{
					if (CurrentThread.NewsStatus.Equals(Thread.NewsStatusEnum.None))
					{
						#region Enabling fresh news
						if (CurrentThread.GroupK > 0)
						{
							CurrentThread.IsNews = false;
							CurrentThread.NewsStatus = Thread.NewsStatusEnum.Recommended;
							CurrentThread.NewsLevel = 10;
							CurrentThread.NewsUsrK = Usr.Current.K;
							CurrentThread.SendGroupNewsModNewNewsAlerts();
						}
						else
						{
							CurrentThread.IsNews = false;
							CurrentThread.NewsStatus = Thread.NewsStatusEnum.Recommended;
							CurrentThread.NewsModeratorUsrK = Usr.GetNewsModeratorUsrK();
							CurrentThread.NewsLevel = 0;
							CurrentThread.NewsUsrK = Usr.Current.K;
						}
						#endregion
					}
					else if (CurrentThread.NewsStatus.Equals(Thread.NewsStatusEnum.Done))
					{
						#region Enabling already moderated news
						if (CurrentThread.GroupK > 0)
						{
							if (Usr.Current.CanGroupNewsAdmin(CurrentGroupUsr))
							{
								if (!CurrentThread.IsNews)
								{
									CurrentThread.IsNews = false;
									CurrentThread.NewsStatus = Thread.NewsStatusEnum.Recommended;
									CurrentThread.NewsLevel = 10;
									CurrentThread.NewsUsrK = Usr.Current.K;
									CurrentThread.SendGroupNewsModNewNewsAlerts();
								}
							}
						}
						else
						{
							if (Usr.Current.CanNewsModerator())
							{
								if (!CurrentThread.IsNews)
								{
									CurrentThread.IsNews = false;
									CurrentThread.NewsStatus = Thread.NewsStatusEnum.Recommended;
									CurrentThread.NewsModeratorUsrK = Usr.GetNewsModeratorUsrK();
									CurrentThread.NewsLevel = 0;
									CurrentThread.NewsUsrK = Usr.Current.K;
								}
							}
						}
						#endregion
					}
				}
				else
				{
					if (CurrentThread.NewsStatus.Equals(Thread.NewsStatusEnum.None))
					{
						// Disabling fresh news - do nothing!
					}
					else if (CurrentThread.NewsStatus.Equals(Thread.NewsStatusEnum.Recommended))
					{
						#region Disabling recommended news
						if (Usr.Current.K == CurrentThread.NewsUsrK)
						{
							#region withdraw news
							CurrentThread.IsNews = false;
							CurrentThread.NewsLevel = 0;
							CurrentThread.NewsModeratedByUsrK = 0;
							CurrentThread.NewsModerationDateTime = DateTime.MinValue;
							CurrentThread.NewsModeratorUsrK = 0;
							CurrentThread.NewsStatus = Thread.NewsStatusEnum.None;
							#endregion
						}
						else if (CurrentUsrIsNewsMod)
						{
							CurrentThread.DisableNews();
						}
						#endregion
					}
					else if (CurrentThread.NewsStatus.Equals(Thread.NewsStatusEnum.Done))
					{
						#region Disabling already moderated news
						if (CurrentUsrIsNewsMod)
						{
							if (CurrentThread.IsNews)
							{
								#region reset news status
								CurrentThread.IsNews = false;
								CurrentThread.NewsLevel = 0;
								CurrentThread.NewsModeratedByUsrK = Usr.Current.K;
								CurrentThread.NewsModerationDateTime = DateTime.Now;
								CurrentThread.NewsStatus = Thread.NewsStatusEnum.Done;
								#endregion
							}
						}
						#endregion
					}
				}
			}

			CurrentThread.Update();

			UpdateOptionsControls();
		}
		#endregion
		#region ChangeForumPanel
		public void ChangeForum_Click(object o, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				if (CurrentThread.ParentObjectType.Equals(Model.Entities.ObjectType.Photo))
				{
					throw new Exception("Can't change forum for a photo thread!");
				}

				if (CurrentThread.IsReview)
				{
					throw new Exception("Can't change forum for a review!");
				}

				IDiscussable previousParentForum = ((IDiscussable)CurrentThread.ParentForumObject);
				Model.Entities.ObjectType previousParentType = CurrentThread.ParentObjectType;
				IDiscussable previousParent = CurrentThread.ParentDiscussable;
				int previousParentK = CurrentThread.ParentObjectK;

				if (previousParentType.Equals(Model.Entities.ObjectType.Group))
				{
					previousParentType = Model.Entities.ObjectType.None;
					previousParentK = 0;
				}

				Model.Entities.ObjectType newParentType;
				if (ScopeEvent.Checked)
					newParentType = Model.Entities.ObjectType.Event;
				else if (ScopeVenue.Checked)
					newParentType = Model.Entities.ObjectType.Venue;
				else if (ScopePlace.Checked)
					newParentType = Model.Entities.ObjectType.Place;
				else if (ScopeCountry.Checked)
					newParentType = Model.Entities.ObjectType.Country;
				else
					newParentType = Model.Entities.ObjectType.None;

				IBob newParent = null;
				int newParentK = 0;
				if (!newParentType.Equals(Model.Entities.ObjectType.None))
				{
					newParentK = int.Parse(this.uiObjectMultiComplete.Value);

					//ensure the new parent exists
					newParent = Bob.Get(newParentType, newParentK);
					if (newParent == null)
						throw new Exception("Can't find new parent!");
				}

				//ensure new parent is different
				if (previousParentType.Equals(newParentType) && newParentK == previousParentK)
					return;

				if (newParentType.Equals(Model.Entities.ObjectType.None) && CurrentThread.GroupK > 0)
				{
					CurrentThread.ParentObjectType = Model.Entities.ObjectType.Group;
					CurrentThread.ParentObjectK = CurrentThread.GroupK;
				}
				else
				{
					CurrentThread.ParentObjectType = newParentType;
					CurrentThread.ParentObjectK = newParentK;
				}
				CurrentThread.ParentDiscussable = null;

				CurrentThread.UpdateAncestorLinksNoUpdate();
				CurrentThread.Update();

				if (previousParent is IHasPrimaryThread)
					((IHasPrimaryThread)previousParent).UpdateSingleThread();

				if (newParent is IHasPrimaryThread)
					((IHasPrimaryThread)newParent).UpdateSingleThread();

				if (previousParentForum != null && !(previousParentForum is Group))
					previousParentForum.UpdateTotalComments(null);

				IDiscussable newParentForum = ((IDiscussable)CurrentThread.ParentForumObject);
				if (newParentForum != null && !(newParentForum is Group))
					newParentForum.UpdateTotalComments(null);

				Response.Redirect(CurrentThread.Url());
			}
		}
		public void ScopeVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = ScopeEvent.Checked || ScopeVenue.Checked || ScopePlace.Checked || ScopeCountry.Checked || ScopeGeneral.Checked;
		}
		public void DbComboVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = this.uiObjectMultiComplete.Value != "" || ScopeGeneral.Checked;
		}
		#endregion

		protected void ChangeToPrimary(object o, EventArgs e)
		{
			if (Usr.Current != null && Usr.Current.IsAdmin)
			{
				Article parent = CurrentThread.ParentArticle;
				parent.ThreadK = CurrentThread.K;
				parent.Update();

				parent.UpdateTotalComments(null);

				Response.Redirect(parent.Url());
			}
		}
		#endregion



		#region CurrentUsrIsNewsMod
		public bool CurrentUsrIsNewsMod
		{
			get
			{
				if (CurrentThread.GroupK == 0 && Usr.Current.CanNewsModerator())
					return true;
				else if (CurrentThread.GroupK > 0 && Usr.Current.CanGroupNewsAdmin(CurrentGroupUsr))
					return true;

				return false;
			}
		}
		#endregion
		#region CurrentUsrCanClose
		public bool CurrentUsrCanClose
		{
			get
			{
				if (Usr.Current.IsAdmin)
					return true;
				else if (CurrentThread.GroupK > 0 && Usr.Current.CanGroupModerator(CurrentGroupUsr))
					return true;

				return false;
			}
		}
		#endregion
		#region CurrentThread
		public Thread CurrentThread
		{
			get
			{
				if (currentThread == null && ThreadK > 0)
					currentThread = new Thread(ThreadK);
				return currentThread;
			}
			set
			{
				currentThread = value;
			}
		}
		private Thread currentThread;
		#endregion
		#region ThreadK
		public int ThreadK
		{
			get
			{
				return ContainerPage.Url["k"];
			}
		}
		#endregion
		#region CurrentGroupUsr
		public GroupUsr CurrentGroupUsr
		{
			get
			{
				if (!currentGroupUsrDone && CurrentThread.GroupK > 0)
				{
					currentGroupUsr = CurrentThread.Group.GetGroupUsr(Usr.Current);
					currentGroupUsrDone = true;
				}
				return currentGroupUsr;
			}
			set
			{
				currentGroupUsr = value;
			}
		}
		bool currentGroupUsrDone = false;
		GroupUsr currentGroupUsr;
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
			PanelOptions.Visible = p.Equals(PanelOptions);
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
			this.Load += new System.EventHandler(this.PanelOptions_Load);
		}
		#endregion
	}
}
