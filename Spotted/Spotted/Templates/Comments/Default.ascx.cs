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
using Spotted.Controls;

namespace Spotted.Templates.Comments
{
	public partial class Default : System.Web.UI.UserControl
	{

		protected string CommentNumber
		{
			get
			{
				if (CurrentComment.K == 1000000)
					return "<b><a href=\"/chat/k-198102\">Comment " + CurrentComment.K.ToString("#,##0") + " WINNER!!!</a></b><br>";
				else if (Vars.DevEnv || (CurrentComment.K > 990000 && CurrentComment.K < 1010000))
					return "<b><a href=\"/chat/k-198102\">Comment " + CurrentComment.K.ToString("#,##0") + "</a></b><br>";
				else
					return "";
			}
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			DeleteButtonSpan.Visible = Usr.Current != null && Usr.Current.CanDelete(CurrentComment, CurrentThread, CurrentGroupUsr);

			if (Usr.Current != null)
			{
				if (CurrentGroupUsr != null && CurrentGroupUsr.Moderator)
					DeleteButton.Attributes["onclick"] = "return confirm('You are using your group moderator power to delete this comment.\\n\\nAre you sure?');";
				else if (CurrentComment.UsrK != Usr.Current.K)
					DeleteButton.Attributes["onclick"] = "return confirm('You are using your moderator power to delete this comment.\\n\\nAre you sure?');";
				else
					DeleteButton.Attributes["onclick"] = "return confirm('Are you sure?');";
			}

			this.ID = "c" + CurrentComment.K.ToString();
			if (this.NamingContainer is DataListItem)
				((DataListItem)this.NamingContainer).ID = "dlc" + CurrentComment.K.ToString();
			else if (this.NamingContainer is DataGridItem)
				((DataGridItem)this.NamingContainer).ID = "dgc" + CurrentComment.K.ToString();

			PicImg.Visible = true;
			PicImg.Src = CurrentComment.Usr.AnyPicPath;

			//if (CurrentComment.IsNew)
			//	LeftCell.Style["border-right"] = "1px solid black";

			CommentEditSpan.Visible = Usr.Current != null && (CurrentComment.UsrK == Usr.Current.K || Usr.Current.IsAdmin);
			CommentEditAnchor.HRef = "/pages/commentedit/k-" + CurrentComment.K;

			

		}

		#region CurrentThread
		public Thread CurrentThread
		{
			get
			{
				if (currentThread == null)
				{
					currentThread = ((ICommentsPage)this.NamingContainer.NamingContainer.NamingContainer).CurrentThread;
					
				}
				return currentThread;
			}
			set
			{
				currentThread = value;
			}
		}
		private Thread currentThread;
		#endregion
		#region CurrentGroupUsr
		public GroupUsr CurrentGroupUsr
		{
			get
			{
				if (!currentGroupUsrDone)
				{
					currentGroupUsrDone = true;
					//currentGroupUsr = ((ICommentsPage)this.NamingContainer.NamingContainer.NamingContainer).CurrentThreadGroupUsr;

					if (Usr.Current != null && CurrentThread != null && CurrentThread.GroupK > 0 && CurrentThread.Group != null)
					{
						currentGroupUsr = CurrentThread.Group.GetGroupUsr(Usr.Current);
					}
				}
				return currentGroupUsr;
			}
			set
			{
				currentGroupUsrDone = true;
				currentGroupUsr = value;
			}
		}
		private bool currentGroupUsrDone = false;
		private GroupUsr currentGroupUsr;
		#endregion

		#region DeleteCommand
		protected LinkButton DeleteButton;
		public void DeleteCommand(object o, CommandEventArgs e)
		{
			Usr.KickUserIfNotLoggedIn("Must be logged in to delete a post");
			if (e.CommandName.Equals("Delete"))
			{
				Comment CommentToDelete = new Comment(int.Parse(e.CommandArgument.ToString()));
				if (CurrentComment.ThreadK == CommentToDelete.ThreadK)
				{
					if (Usr.Current.CanDelete(CommentToDelete, CurrentThread, CurrentGroupUsr))
					{
						string redirect = CommentToDelete.Thread.UrlDiscussion();
						if (CommentToDelete.Thread.TotalComments == 1)
						{
							redirect = "/chat";
						}
						CommentToDelete.RegisterDelete(Usr.Current);
						CommentToDelete.DeleteAll(null);
						Response.Redirect(redirect);
					}
					else
					{
						throw new Exception("Can't delete");
					}
				}
			}
		}
		#endregion

		#region LolClick
		public void LolClick(object o, System.EventArgs e)
		{
			Lol.CreateLol(CurrentComment);
			((Spotted.Master.DsiPage)Page).AnchorSkip("CommentK-" + CurrentComment.K.ToString());
		}
		#endregion

		#region Page_PreRender
		public void Page_PreRender(object o, System.EventArgs e)
		{
			LolDownLevelSpan.Visible = Usr.Current != null && !Usr.Current.IsSkeleton;
			bool me;
			int meK = 0;
			if (Usr.Current != null)
				meK = Usr.Current.K;
			string lolList = CurrentComment.LolUsrListHtml(out me, meK);
			if (me)
				LolDownLevelSpan.Visible = false;
			LolHtmlPh.Controls.Add(new LiteralControl(lolList));
		}
		#endregion

		#region CurrentComment
		public Comment CurrentComment
		{
			get
			{
				if (currentComment == null)
				{
					if (NamingContainer is DataGridItem)
						currentComment = ((Comment)((DataGridItem)NamingContainer).DataItem);
					else
						currentComment = ((Comment)((DataListItem)NamingContainer).DataItem);
				}
				return currentComment;
			}
			set
			{
				currentComment = value;
			}
		}
		Comment currentComment;
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
		}
		#endregion
	}
}
