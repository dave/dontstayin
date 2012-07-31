using System;
using System.Collections.Generic;
using System.Text;
//using Microsoft.TeamFoundation.WorkItemTracking.Client;

namespace SpottedLibrary.Admin.ReportABug
{
	public class ReportABugController
	{
		IReportABugView view;
		ReportABugService service;
		public ReportABugController(IReportABugView view)
		{
			this.view = view;
			this.service = new ReportABugService();
			this.view.SubmitButtonClicked += new EventHandler(view_SubmitButtonClicked);
			this.view.Load += new EventHandler(view_Load);
		}

		void view_Load(object sender, EventArgs e)
		{
			this.view.Url = this.view.Referrer;
		}

		void view_SubmitButtonClicked(object sender, EventArgs e)
		{
			service.AddNewWorkItem(
				Common.Properties.TfsServerName, 
				Common.Properties.TfsBranchName, "bug", this.view.Title, this.view.Description, 
				new KeyValuePair<string, string>("Reporting usr", this.view.CurrentUsr.NickName)
			);
			view.FormIsVisible = false;
			view.SuccessMessageIsVisible = true;
		}
	}
}
