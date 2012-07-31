using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Bobs;

namespace SpottedLibrary
{
	public interface IView
	{
		event EventHandler Init;
		event EventHandler Load;
		bool IsPostBack { get; }
		void DataBind();
		bool Visible { set; }
		IDictionary ViewState { get; }
		ICutDownUrlInfo CutDownUrl { get; }
	}

}
