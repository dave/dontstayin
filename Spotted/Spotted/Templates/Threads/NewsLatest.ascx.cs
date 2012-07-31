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
	public partial class NewsLatest : System.Web.UI.UserControl
	{
		protected HtmlImage Icon;
		protected Label SubjectLabel;
		protected Label LastPostLabel, TotalCommentsLabel, PostTimeLabel, CommentPluralLabel;
		protected HtmlAnchor ThreadAnchor, AuthorAnchor;

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
					Thread.Columns.DateTime,
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
				//ThreadAnchor1.HRef=CurrentThread.UrlDiscussion();
				PostTimeLabel.Text = Cambro.Misc.Utility.FriendlyTime(CurrentThread.DateTime, false);
				TotalCommentsLabel.Text = ((int)(CurrentThread.TotalComments - 1)).ToString();
				if ((CurrentThread.TotalComments - 1) == 1)
					CommentPluralLabel.Text = "";
				else
					CommentPluralLabel.Text = "s";

				AuthorAnchor.HRef = CurrentThread.Usr.Url();
				AuthorAnchor.InnerText = CurrentThread.Usr.NickName;
				CurrentThread.Usr.MakeRollover(AuthorAnchor);
			}

		}
		public void Page_Init(object o, System.EventArgs e)
		{
			//Strange - CurrentThread is always null if we don't access it in the Init!
			int i = CurrentThread.K;
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


	}
}
