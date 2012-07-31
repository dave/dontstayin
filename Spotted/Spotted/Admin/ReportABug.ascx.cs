using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using SpottedLibrary.Admin.ReportABug;
//http://blogs.vertigosoftware.com/teamsystem/archive/2006/07/13/Programmatically_adding_new_Work_Items_to_a_Team_System_Project.aspx
namespace Spotted.Admin
{
	public partial class ReportABug : AdminUserControl, IReportABugView
	{
		ReportABugController controller;
		public ReportABug()
		{
			this.controller = new ReportABugController(this);
		}
	 

		protected void uiSubmit_Click(object sender, EventArgs e)
		{
			if (this.SubmitButtonClicked != null)
			{
				this.SubmitButtonClicked(this, new EventArgs());
			}
		}





		public event EventHandler SubmitButtonClicked;

		public string Title
		{
			get { return uiTitle.Text; }
		}

		public string Description
		{
			get { return uiDescription.Text; }
		}


		#region IReportABugView Members


		public Bobs.Usr CurrentUsr
		{
			get { return Bobs.Usr.Current; ; }
		}

		#endregion

		#region IReportABugView Members


		public string Referrer
		{
			get { return Request.UrlReferrer.ToString(); }
		}

		public string Url
		{
			get { return uiUrl.Text; }
			set { uiUrl.Text = value; }
		}

		#endregion

		#region IReportABugView Members


		public bool FormIsVisible
		{
			set { this.uiBugFormPanel.Visible = value; }
		}

		public bool SuccessMessageIsVisible
		{
			set { this.uiSuccessPanel.Visible = value; }
		}

		#endregion
	}
}
