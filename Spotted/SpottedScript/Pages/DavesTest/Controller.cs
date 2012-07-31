using System;
using ScriptSharpLibrary;
using Sys;
using Utils;
using Sys.UI;
using JQ;
using System.DHTML;

namespace SpottedScript.Pages.DavesTest
{
	public class Controller
	{
		public View view;
		public Controller(View v)
		{
			this.view = v;

			if (Misc.BrowserIsIE)
				JQueryAPI.JQuery(Document.Body).ready(new Action(initialise));
			else
				initialise();
		}
		void initialise()
		{
			
		}
	}
}
