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

namespace Spotted.Templates.Articles
{
	public partial class Latest : System.Web.UI.UserControl
	{
		protected Label TitleLabel;
		protected Label TotalCommentsLabel, CommentPluralLabel;
		protected HtmlAnchor ArticleAnchor, AuthorAnchor;

		public static ColumnSet Columns
		{
			get
			{
				return new ColumnSet(
					Article.Columns.Pic,
					Article.Columns.K,
					Article.Columns.Title,
					Article.Columns.UrlFragment,
					Article.Columns.ParentObjectK,
					Article.Columns.ParentObjectType,
					Article.Columns.ThreadK,
					Article.Columns.Summary,
					Article.Columns.OwnerUsrK,
					Article.Columns.TotalComments,
					Article.Columns.Views,
					Article.Columns.EnabledDateTime
				);
			}
		}
		public static TableElement PerformJoins(TableElement tIn)
		{
			if (tIn == null)
				tIn = new TableElement(TablesEnum.Article);
			
			TableElement t = tIn;

			return t;
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (CurrentArticle != null)
			{
				TitleLabel.Text = HttpUtility.HtmlEncode(CurrentArticle.Title);
				ArticleAnchor.HRef = CurrentArticle.Url();
				//CurrentArticle.MakeRollover(ArticleAnchor);
				//EnabledTimeLabel.Text=Cambro.Misc.Utility.FriendlyTime(CurrentArticle.EnabledDateTime,false);
				TotalCommentsLabel.Text = ((int)(CurrentArticle.TotalComments)).ToString();
				if (CurrentArticle.TotalComments == 1)
					CommentPluralLabel.Text = "";
				else
					CommentPluralLabel.Text = "s";
				AuthorAnchor.HRef = CurrentArticle.Owner.Url();
				AuthorAnchor.InnerText = CurrentArticle.Owner.NickName;
				CurrentArticle.Owner.MakeRollover(AuthorAnchor);

			}

		}
		public void Page_Init(object o, System.EventArgs e)
		{
			//Strange - CurrentThread is always null if we don't access it in the Init!
			int i = CurrentArticle.K;
		}


		protected Article CurrentArticle
		{
			get
			{
				if (currentArticle == null)
					currentArticle = ((Article)((DataListItem)NamingContainer).DataItem);
				return currentArticle;
			}
		}
		Article currentArticle;

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
