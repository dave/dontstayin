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
	public partial class HotTopicLatest : System.Web.UI.UserControl
	{
		protected HtmlImage Icon;
		protected Label SubjectLabel;
		protected Label LastPostLabel, TotalCommentsLabel;
		protected HtmlAnchor ThreadAnchor, ThreadAnchor1;
		protected Label CommentPluralLabel;
		protected HtmlAnchor AuthorAnchor;

		public static ColumnSet Columns
		{
			get
			{
				return new ColumnSet(
					Thread.Columns.K,
					Thread.Columns.UrlFragment,
					Thread.Columns.ParentObjectType,
					Thread.Columns.Subject,
					Thread.Columns.EventK,
					Thread.Columns.VenueK,
					Thread.Columns.PlaceK,
					Thread.Columns.ArticleK,
					Thread.Columns.PhotoK,
					Thread.Columns.BrandK,
					Thread.Columns.GroupK,
					Thread.Columns.UsrK,
					Thread.Columns.Private,
					Thread.Columns.LastPost,
					Thread.Columns.TotalComments
					);
			}
		}
		public static TableElement PerformJoins(TableElement tIn)
		{
			if (tIn == null)
				tIn = new TableElement(TablesEnum.Thread);
			TableElement t = tIn;
			return t;
		}


		private void Page_Load(object sender, System.EventArgs e)
		{
			if (CurrentThread != null)
			{

				SubjectLabel.Text = HttpUtility.HtmlEncode(CurrentThread.Subject);
				ThreadAnchor.HRef = CurrentThread.UrlDiscussion();
				TotalCommentsLabel.Text = ((int)(CurrentThread.TotalComments - 1)).ToString();
				if ((CurrentThread.TotalComments - 1) == 1)
					CommentPluralLabel.Text = "";
				else
					CommentPluralLabel.Text = "s";

				AuthorAnchor.HRef = CurrentThread.Usr.Url();
				AuthorAnchor.InnerText = CurrentThread.Usr.NickName;
				CurrentThread.Usr.MakeRollover(AuthorAnchor);


				LastPostLabel.Text = Cambro.Misc.Utility.FriendlyTime(CurrentThread.LastPost);
				TotalCommentsLabel.Text = CurrentThread.TotalComments.ToString();
			}
		}
		string AltIcon
		{
			get
			{
				return CurrentThread.Usr.AnyPicPath;
			}
		}

		protected Thread CurrentThread
		{
			get
			{
				if (currentThread == null)
					currentThread = ((Thread)((DataListItem)NamingContainer).DataItem);
				return currentThread;
			}
		}
		Thread currentThread;

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
