using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Bobs;

namespace Spotted.Pages
{
	public partial class CommentEdit : DsiUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (canEdit)
			{
				if (!Page.IsPostBack)
				{
					CommentEditHtml.LoadHtml(CurrentComment.Text);
					if (CurrentComment.K == CurrentComment.Thread.FirstComment.K)
					{
						SubjectPanel.Visible = true;
						ThreadSubjectEditBox.Text = CurrentComment.Thread.Subject;
					}
					else
					{
						SubjectPanel.Visible = false;
					}
				}
			}
			else
				throw new DsiUserFriendlyException("You didn't post this comment!");

		}

		#region CurrentComment
		public Comment CurrentComment
		{
			get
			{
				if (currentComment == null)
					currentComment = new Comment(ContainerPage.Url["k"]);
				return currentComment;
			}
			set
			{
				currentComment = value;
			}
		}
		Comment currentComment;
		#endregion

		bool canEdit
		{
			get
			{
				return Usr.Current != null && (CurrentComment.UsrK == Usr.Current.K || Usr.Current.IsAdmin);
			}
		}

		#region CommentEditSaveClick
		protected void CommentEditSaveClick(object o, EventArgs e)
		{
			if (canEdit)
			{

				if (CommentEditHtml.ValidationPropertyValue.Trim().Length == 0)
					throw new DsiUserFriendlyException("No text in comment!");

				CurrentComment.Text = CommentEditHtml.GetHtml();
				if (CurrentComment.UsrK == Usr.Current.K)
				{
					CurrentComment.IsEdited = true;
					CurrentComment.EditDateTime = DateTime.Now;
				}
				if (CurrentComment.K == CurrentComment.Thread.FirstComment.K)
				{
					string newSubject = Cambro.Web.Helpers.Strip(ThreadSubjectEditBox.Text, true, true, true, true);
					if (newSubject.Length == 0)
						throw new DsiUserFriendlyException("No text in subject!");

					CurrentComment.Thread.Subject = newSubject;
					CurrentComment.Thread.Update();
				}
				CurrentComment.Update();

				Response.Redirect(CurrentComment.UrlRefresher());
			}
			else
				throw new DsiUserFriendlyException("You didn't post this comment!");
		}
		#endregion

	}
}
