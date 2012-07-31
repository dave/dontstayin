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
	public partial class ReviewLatest : System.Web.UI.UserControl
	{
		protected Label SubjectLabel;
		protected Label LastPostLabel, TotalCommentsLabel, CommentPluralLabel, EventLabel;
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
					Thread.Columns.UsrK,
					Thread.Columns.DateTime,
					Thread.Columns.TotalComments,
					Thread.Columns.Private,
					Thread.Columns.ParentObjectK
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
				ThreadAnchor.InnerText = CurrentThread.Subject;
				ThreadAnchor.HRef = CurrentThread.UrlDiscussion();
				TotalCommentsLabel.Text = ((int)(CurrentThread.TotalComments - 1)).ToString();
				if ((CurrentThread.TotalComments - 1) == 1)
					CommentPluralLabel.Text = "";
				else
					CommentPluralLabel.Text = "s";

				object parent = null;

				if (NamingContainer.NamingContainer.NamingContainer.NamingContainer is Spotted.Controls.Latest)
					parent = ((Spotted.Controls.Latest)(NamingContainer.NamingContainer.NamingContainer.NamingContainer)).Parent;

				if (parent == null || parent is Country || parent is Brand || parent is Group)
					EventLabel.Text = " - <a href=\"" + CurrentThread.Event.Url() + "\">" + CurrentThread.Event.Name + "</a> @ <a href=\"" + CurrentThread.Event.Venue.Url() + "\">" + CurrentThread.Event.Venue.Name + "</a> in <a href=\"" + CurrentThread.Event.Venue.Place.Url() + "\">" + CurrentThread.Event.Venue.Place.Name + "</a>, " + CurrentThread.Event.FriendlyDate(false);
				else if (parent is Place)
					EventLabel.Text = " - <a href=\"" + CurrentThread.Event.Url() + "\">" + CurrentThread.Event.Name + "</a> @ <a href=\"" + CurrentThread.Event.Venue.Url() + "\">" + CurrentThread.Event.Venue.Name + "</a>, " + CurrentThread.Event.FriendlyDate(false);
				else if (parent is Venue)
					EventLabel.Text = " - <a href=\"" + CurrentThread.Event.Url() + "\">" + CurrentThread.Event.Name + "</a>, " + CurrentThread.Event.FriendlyDate(false);
				else if (parent is Event)
					EventLabel.Text = "";

				AuthorAnchor.HRef = CurrentThread.Usr.Url();
				AuthorAnchor.InnerText = CurrentThread.Usr.NickName;
				CurrentThread.Usr.MakeRollover(AuthorAnchor);

				//CurrentThread.ParentEvent.MakeRollover(ThreadAnchor);

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
