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
using Common;
using Spotted.Controls;
using SpottedLibrary.Controls.ThreadControl;
using SpottedLibrary.Pages.TagSearch;
using SpottedLibrary.Controls.SearchBoxControl;

namespace Spotted.Pages.Articles
{
	[ClientScript]
	public partial class Home : DsiUserControl
	{
		public Home() 
		{
			this.Init += new EventHandler(Comments_Init);
		}
		public void Page_Init(object o, System.EventArgs e)
		{
			if (CurrentArticle != null)
			{
				((HomeContent)HomeContent).CurrentArticle = CurrentArticle;

				CurrentArticle.AddRelevant(ContainerPage);

				this.uiThreadK.Value = CurrentArticle.ThreadK.HasValue ? CurrentArticle.ThreadK.Value.ToString() : "0";
				this.uiArticleK.Value = CurrentArticle.K.ToString();
				this.uiPageNumber.Value = PageIndex.ToString();
			}
		}


		#region Comments
		
		#region Comments_Init
		protected void Comments_Init(object sender, EventArgs eventArgs)
		{
			ThreadControl.ParentObjectType = Model.Entities.ObjectType.Article;
			ThreadControl.ParentObjectK = CurrentArticle.K;

			ThreadControl.ThreadK = CurrentArticle.ThreadK;
			//ThreadControl.CommentPage = CommentsPage;
			int currentPage = Url["c"].ValueInt;
			if (currentPage > 0) ThreadControl.CurrentPage = currentPage;
			
			//ThreadControl.ThreadCreated += new EventHandler<ThreadCreatedArgs>(ThreadControl_ThreadCreated);

			this.LatestChat.Discussable = CurrentArticle;
		}

	


		#endregion

		//#region CommentsPage
		//int CommentsPage
		//{
		//    get
		//    {
		//        if (ContainerPage.Url["c"].IsInt)
		//            return ContainerPage.Url["c"];
		//        else
		//            return 1;
		//    }
		//}
		//#endregion

		//#region Thread Events - CreatedThread, AddComment
		//void ThreadControl_ThreadCreated(object sender,  ThreadCreatedArgs e)
		//{
		//    if (!e.Private && e.GroupK == 0)
		//    {
		//        CurrentArticle.ThreadK = e.NewThread.K;
		//        CurrentArticle.Update();
		//        CurrentArticle.UpdateTotalComments(null);
		//    }
		//    else
		//    {
		//        Response.Redirect(e.NewThread.Url());
		//    }
		//}
		//#endregion

		#endregion


		private void Page_Load(object sender, System.EventArgs e)
		{
			SetPageTitle(CurrentArticle.Title);
			
			if (Usr.Current != null && Usr.Current.IsAdmin)
			{
				ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p><a href=\"http://old.dontstayin.com/login-" + Usr.Current.K + "- " + Usr.Current.LoginString + "/admin/article?ID=" + CurrentArticle.K + "\">Edit article (Admin)</a></p>"));
				ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p><a href=\"/pages/myarticles/mode-edit/k-" + CurrentArticle.K + "\">Edit article (My articles)</a></p>"));

				ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p><a href=\"/admin/addpic?Type=Article&K=" + CurrentArticle.K + "\">Add pic to this article</a></p>"));
				ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p><a onclick=\"return confirm('This will delete ALL attached objects.\\nARE YOU SURE?');\" href=\"/admin/multidelete?ObjectType=Article&ObjectK=" + CurrentArticle.K + "\">Delete this article</a></p>"));
			}

		}

		#region CurrentArticle
		public Article CurrentArticle
		{
			get
			{
				return ContainerPage.Url.ObjectFilterArticle;
			}
		}
		#endregion

		#region PageIndex
		public int PageIndex
		{
			get
			{
				if (ContainerPage.Url["P"].IsInt)
					return ContainerPage.Url["P"];
				else
					return 1;
			}
		}
		#endregion

 
	}
}
