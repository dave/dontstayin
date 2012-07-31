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
	public partial class EditPage : System.Web.UI.UserControl
	{
		protected Label TitleLabel;
		protected Repeater ParaRepeater;
		private void Page_Load(object sender, System.EventArgs e)
		{
			ParaRepeater.ItemTemplate = this.LoadTemplate("/Templates/Articles/EditPara.ascx");
			ParaRepeater.DataSource = CurrentArticle.GetParaInPage(CurrentPage);
			ParaRepeater.DataBind();
			TitleLabel.Text = CurrentArticle.Name;
		}
		public void Page_Init(object o, System.EventArgs e)
		{
			int i = CurrentPage;
		}
		#region CurrentArticle
		protected Article CurrentArticle
		{
			get
			{
				return ((Pages.MyArticles)(NamingContainer.NamingContainer.NamingContainer)).CurrentArticle;
			}
		}
		#endregion
		#region CurrentPage
		protected int CurrentPage
		{
			get
			{
				if (currentPage == 0)
					currentPage = ((int)((RepeaterItem)NamingContainer).DataItem);
				return currentPage;
			}
		}
		int currentPage = 0;
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
