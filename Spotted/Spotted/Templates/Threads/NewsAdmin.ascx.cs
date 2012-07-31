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

namespace Spotted.Templates.Threads
{
	public partial class NewsAdmin : System.Web.UI.UserControl
	{
		protected HtmlImage PicImg;
		protected HtmlAnchor AdminEditLink, AdminDeleteLink;
		protected HtmlImage SpacerImg;
		protected HtmlGenericControl SpacerBr, IconSpacerBr;
		protected HtmlTableCell TextCell, LeftCell, EditCell;
		protected TextBox CommentEditTextBox;
		protected Panel SubjectPanel;
		protected TextBox ThreadSubjectEditBox;
		protected HtmlAnchor ThreadLink, ForumLink;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Usr.Current.CanNewsModerator())
			{
				throw new Exception("Can't news admin!!!");
			}


			this.ID = "c" + CurrentComment.K.ToString();
			((RepeaterItem)this.NamingContainer).ID = "dlc" + CurrentComment.K.ToString();

			PicImg.Visible = true;
			PicImg.Src = CurrentComment.Usr.AnyPicPath;

			if (CurrentThread.PhotoK > 0)
			{
				PhotoP.InnerHtml = "<img src=\"" + CurrentThread.Photo.WebPath + "\" width=\"" + CurrentThread.Photo.WebWidth.ToString() + "\" height=\"" + CurrentThread.Photo.WebHeight + "\" class=\"BorderBlack All\">";
			}
			else
				PhotoP.Visible=false;

			AdminEditLink.Visible = (Usr.Current != null && Usr.Current.IsAdmin);
			AdminDeleteLink.Visible = (Usr.Current != null && Usr.Current.IsAdmin);

			if (CurrentThread.ParentObjectType.Equals(Model.Entities.ObjectType.None))
			{
				ForumLink.HRef = "/chat";
				ForumLink.InnerText = "General topic";
			}
			else
			{
				ForumLink.InnerText = ((IBobType)CurrentThread.ParentForumObject).TypeName + ": " + ((IReadableReference)CurrentThread.ParentForumObject).ReadableReference;
				ForumLink.HRef = ((IDiscussable)CurrentThread.ParentForumObject).UrlDiscussion();
			}
			ThreadLink.HRef = CurrentThread.Url();


		}

		public void Page_PreRender(object o, System.EventArgs e)
		{
		}

		protected void CommentEditClick(object o, EventArgs e)
		{
			if (!Usr.Current.CanNewsModerator())
			{
				throw new Exception("Can't news admin!!!");
			}

			CurrentComment = new Comment(CurrentComment.K);
			EditCell.Visible = true;
			TextCell.Visible = false;
			CommentEditTextBox.Text = CurrentComment.Text;
			if (CurrentComment.K == CurrentComment.Thread.FirstComment.K)
			{
				SubjectPanel.Visible = true;
				ThreadSubjectEditBox.Text = CurrentComment.Thread.Subject;
			}
			else
			{
				SubjectPanel.Visible = false;
			}
			((Spotted.Master.DsiPage)Page).AnchorSkip("CommentK-" + CurrentComment.K.ToString());

		}
		protected void CommentEditSaveClick(object o, EventArgs e)
		{
			if (!Usr.Current.CanNewsModerator())
			{
				throw new Exception("Can't news admin!!!");
			}
			CurrentComment = new Comment(CurrentComment.K);
			if (CommentEditTextBox.Text.Trim().Length == 0)
				throw new DsiUserFriendlyException("No text in comment!");
			CurrentComment.Text = Cambro.Web.Helpers.CleanHtml(CommentEditTextBox.Text);
			if (CurrentComment.K == CurrentComment.Thread.FirstComment.K)
			{
				if (Cambro.Web.Helpers.StripHtml(ThreadSubjectEditBox.Text).Trim().Length == 0)
					throw new DsiUserFriendlyException("No text in subject!");
				CurrentComment.Thread.Subject = Cambro.Web.Helpers.StripHtml(ThreadSubjectEditBox.Text);
				CurrentComment.Thread.Update();
			}
			CurrentComment.Update();
			((Spotted.Master.DsiPage)Page).AnchorSkip("CommentK-" + CurrentComment.K.ToString());

		}

		protected Comment CurrentComment
		{
			get
			{
				return CurrentThread.FirstComment;
			}
			set
			{
				CurrentThread.FirstComment = value;
			}
		}

		protected Thread CurrentThread
		{
			get
			{
				if (currentThread == null)
				{
					currentThread = ((Thread)((RepeaterItem)NamingContainer).DataItem);
				}
				return currentThread;
			}
			set
			{
				currentThread = value;
			}
		}
		Thread currentThread;
	}
}
