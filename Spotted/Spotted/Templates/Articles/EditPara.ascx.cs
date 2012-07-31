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
	public partial class EditPara : System.Web.UI.UserControl
	{
		protected Button DeleteButton;
		protected PlaceHolder ParaPh;
		private void Page_Load(object sender, System.EventArgs e)
		{
			Templates.Articles.ParaTemplate para = (Templates.Articles.ParaTemplate)this.LoadControl("/Templates/Articles/ParaTemplate.ascx");
			para.OverridePara = CurrentPara;
			para.PrivateMode = true;
			ParaPh.Controls.Add(para);
			DeleteButton.Attributes["onclick"] = "return confirm('Are you sure?');";
		}
		public void Page_Init(object o, System.EventArgs e)
		{
			Para c = CurrentPara;
		}
		public void EditClick(object o, System.EventArgs e)
		{
			Response.Redirect("/pages/myarticles/mode-para/k-" + CurrentPara.K.ToString());
		}
		public void PhotoClick(object o, System.EventArgs e)
		{
			Response.Redirect("/pages/myarticles/mode-paraphoto/k-" + CurrentPara.K.ToString());
		}
		public void UpClick(object o, System.EventArgs e)
		{
			//Find the two paras in this page that have lower orders...
			Query qLower = new Query();
			qLower.NoLock = false;
			qLower.QueryCondition = new And(
				new Q(Para.Columns.ArticleK, CurrentPara.ArticleK),
				new Q(Para.Columns.Page, CurrentPara.Page),
				new Q(Para.Columns.Order, QueryOperator.LessThan, CurrentPara.Order)
				);
			qLower.TopRecords = 2;
			qLower.OrderBy = new OrderBy(Para.Columns.Order, OrderBy.OrderDirection.Descending);
			ParaSet ps = new ParaSet(qLower);
			if (ps.Count == 2)
			{
				//put this para in between
				CurrentPara.Order = (ps[0].Order + ps[1].Order) / 2.0;
				CurrentPara.Update();
				CurrentPara.Article.ReOrder(CurrentPara.Page);
			}
			else if (ps.Count == 1)
			{
				CurrentPara.Order = ps[0].Order - 1.0;
				CurrentPara.Update();
				CurrentPara.Article.ReOrder(CurrentPara.Page);
			}
			else
			{
				if (CurrentPara.Page > 1)
				{
					CurrentPara.Page = CurrentPara.Page - 1;
					//find the greatest order in the previous page, and put this para after it.
					Query qPrev = new Query();
					qPrev.NoLock = false;
					qPrev.QueryCondition = new And(
						new Q(Para.Columns.ArticleK, CurrentPara.ArticleK),
						new Q(Para.Columns.Page, CurrentPara.Page)
						);
					qPrev.TopRecords = 1;
					qPrev.OrderBy = new OrderBy(Para.Columns.Order, OrderBy.OrderDirection.Descending);
					ParaSet psPrev = new ParaSet(qPrev);
					if (psPrev.Count == 1)
						CurrentPara.Order = psPrev[0].Order + 1.0;
					else
						CurrentPara.Order = 1.0;
					CurrentPara.Update();
					CurrentPara.Article.ReOrder(CurrentPara.Page + 1);
				}
			}
			MyArticlesPage.BindBodyPageRepeater();
			MyArticlesPage.AnchorSkip("ArticlePage" + CurrentPara.Page);
		}
		public void DownClick(object o, System.EventArgs e)
		{
			//Find the two paras in this page that have higher orders...
			Query qHigher = new Query();
			qHigher.NoLock = false;
			qHigher.QueryCondition = new And(
				new Q(Para.Columns.ArticleK, CurrentPara.ArticleK),
				new Q(Para.Columns.Page, CurrentPara.Page),
				new Q(Para.Columns.Order, QueryOperator.GreaterThan, CurrentPara.Order)
				);
			qHigher.TopRecords = 2;
			qHigher.OrderBy = new OrderBy(Para.Columns.Order, OrderBy.OrderDirection.Ascending);
			ParaSet ps = new ParaSet(qHigher);
			if (ps.Count == 2)
			{
				//put this para in between
				CurrentPara.Order = (ps[0].Order + ps[1].Order) / 2.0;
				CurrentPara.Update();
				CurrentPara.Article.ReOrder(CurrentPara.Page);
			}
			else if (ps.Count == 1)
			{
				CurrentPara.Order = ps[0].Order + 1.0;
				CurrentPara.Update();
				CurrentPara.Article.ReOrder(CurrentPara.Page);
			}
			else
			{
				if (CurrentPara.Page < CurrentPara.Article.LastPage || (CurrentPara.Page == CurrentPara.Article.LastPage && CurrentPara.Article.GetParaInPage(CurrentPara.Article.LastPage).Count > 1))
				{
					CurrentPara.Page = CurrentPara.Page + 1;
					//find the lowest order in the next page, and put this para before it.
					Query qNext = new Query();
					qNext.NoLock = false;
					qNext.QueryCondition = new And(
						new Q(Para.Columns.ArticleK, CurrentPara.ArticleK),
						new Q(Para.Columns.Page, CurrentPara.Page)
						);
					qNext.TopRecords = 1;
					qNext.OrderBy = new OrderBy(Para.Columns.Order, OrderBy.OrderDirection.Ascending);
					ParaSet psNext = new ParaSet(qNext);
					if (psNext.Count == 1)
						CurrentPara.Order = psNext[0].Order - 1.0;
					else
						CurrentPara.Order = 1.0;
					CurrentPara.Update();
					CurrentPara.Article.ReOrder(CurrentPara.Page);
				}
			}
			MyArticlesPage.BindBodyPageRepeater();
			MyArticlesPage.AnchorSkip("ArticlePage" + CurrentPara.Page);
		}

		public void DeleteClick(object o, System.EventArgs e)
		{
			int page = CurrentPara.Page;
			Article article = CurrentPara.Article;

			Delete.DeleteAll(CurrentPara);
			article.ReOrder(page);
			MyArticlesPage.BindBodyPageRepeater();
			MyArticlesPage.AnchorSkip("ArticlePage" + page.ToString());
		}
		public void NewClick(object o, System.EventArgs e)
		{
			Response.Redirect("/pages/myarticles/mode-para/k-" + CurrentPara.K.ToString() + "/new-1");
		}

		protected Para CurrentPara
		{
			get
			{
				if (currentPara == null)
					currentPara = ((Para)((RepeaterItem)NamingContainer).DataItem);
				return currentPara;
			}
		}
		Para currentPara;
		public Pages.MyArticles MyArticlesPage
		{
			get
			{
				if (myArticlesPage == null)
					myArticlesPage = (Pages.MyArticles)(this.NamingContainer.NamingContainer.NamingContainer.NamingContainer.NamingContainer.NamingContainer);
				return myArticlesPage;
			}
		}
		Pages.MyArticles myArticlesPage;


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
