using System;
using System.Collections.Generic;
using System.Text;
using Bobs;

namespace SpottedLibrary.Admin.ReportABug
{
	public interface IReportABugView : IView
	{
		event EventHandler SubmitButtonClicked;
		
		string Title { get; }
		string Description { get; }
		Usr CurrentUsr { get; }
		string Referrer { get; }
		string Url { get; set; }
		bool FormIsVisible { set; }
		bool SuccessMessageIsVisible { set; }

	}
}
