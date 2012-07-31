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

namespace Spotted.Templates.Articles
{
	public partial class LatestNew : System.Web.UI.UserControl
	{
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
					Article.Columns.EnabledDateTime,
					Article.Columns.IsMixmagNews,
					Article.Columns.IsExtendedDisplay,
					Article.Columns.ShowAboveFoldOnFrontPage
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

				if (CurrentArticle.IsExtendedDisplay)
				{

					Templates.Articles.ParaTemplate para = (Templates.Articles.ParaTemplate)this.LoadControl("/Templates/Articles/ParaTemplate.ascx");
					para.OverridePara = CurrentArticle.FirstPara;
					para.ForceLinksToArticle = true;
					ParaPh.Controls.Add(para);
				}
				else
				{
					SummaryDisplayDiv.Visible = true;
				}

				//TitleLabel.Text = HttpUtility.HtmlEncode(CurrentArticle.Title);
				//ArticleAnchor.HRef = CurrentArticle.Url();
				//CurrentArticle.MakeRollover(ArticleAnchor);
				//EnabledTimeLabel.Text=Cambro.Misc.Utility.FriendlyTime(CurrentArticle.EnabledDateTime,false);

				ChatDetailsDiv.Visible = NamingContainer is Pages.Chat;
				NormalDetailsDiv.Visible = !(NamingContainer is Pages.Chat);
				if (NamingContainer is Pages.Chat)
				{
					AuthorAnchor1.HRef = CurrentArticle.Owner.Url();
					AuthorAnchor1.InnerText = CurrentArticle.Owner.NickName;
					CurrentArticle.Owner.MakeRollover(AuthorAnchor1);
				}

				if (!(NamingContainer is Pages.Chat))
				{
					AuthorAnchor.HRef = CurrentArticle.Owner.Url();
					AuthorAnchor.InnerText = CurrentArticle.Owner.NickName;
					CurrentArticle.Owner.MakeRollover(AuthorAnchor);
				}

				ArticlesArchiveDiv.Visible = false;
				MixmagArchiveDiv.Visible = false;
				MixmagP.Visible = false;
				if (this.NamingContainer.NamingContainer.NamingContainer is Controls.LatestContent)
				{
					if (CurrentArticle.IsMixmagNews)
					{
						MixmagP.Visible = true;
						MixmagArchiveDiv.Visible = true;
						MixmagArchiveAnchor.HRef = Archive.GetUrl(DateTime.Now.Year, DateTime.Now.Month, 0, Model.Entities.ArchiveObjectType.Article, new string[]{"mixmag", ""}, "");
					}
					else
					{
						Controls.LatestContent parent = (Spotted.Controls.LatestContent)this.NamingContainer.NamingContainer.NamingContainer;
						ArticlesArchiveDiv.Visible = parent.ArticlesArchiveVisible;
						ArticlesArchiveAnchor.HRef = parent.ArticlesArchiveUrl;
					}
				}

			}

		}
		public void Page_Init(object o, System.EventArgs e)
		{
			//Strange - CurrentThread is always null if we don't access it in the Init!
			int i = CurrentArticle.K;
		}


		public Article CurrentArticle
		{
			get
			{
				if (NamingContainer is Pages.Chat)
				{
					return ((Pages.Chat)NamingContainer).CurrentThread.ParentArticle;
				}

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
