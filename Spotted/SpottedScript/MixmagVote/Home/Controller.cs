using System;
using ScriptSharpLibrary;
using Sys;
using Utils;
using Sys.UI;
using SpottedScript.Controls.Picker;
using FacebookAPI;
using ImportedUtilities;
using JQ;
using System.DHTML;

namespace SpottedScript.MixmagVote.Home
{
	public class Controller
	{
		public View View;
		public static Controller Instance;
		public Controller(View v)
		{
			Instance = this;
			this.View = v;

			if (Misc.BrowserIsIE)
				JQueryAPI.JQuery(Document.Body).ready(new Action(initialise));
			else
				initialise();
		}

		#region initialise - safe to affect the dom in here
		void initialise()
		{
		}
		#endregion

		bool CurrentFacebookLoggedIn = false;
		bool CurrentFacebookConnected = false;
		string CurrentFacebookUID = "0";
		Dictionary CurrentFacebookSession = null;

		void updateCurrentFacebookLoginStatus(Dictionary statusResponse)
		{
			CurrentFacebookConnected = U.exists(statusResponse, "status") && U.get(statusResponse, "status").ToString() == "connected";
			CurrentFacebookLoggedIn = U.exists(statusResponse, "status") && U.get(statusResponse, "status").ToString() != "unknown";

			CurrentFacebookUID = CurrentFacebookConnected ? U.get(statusResponse, "session/uid").ToString() : "0";
			CurrentFacebookSession = CurrentFacebookConnected ? (Dictionary)U.get(statusResponse, "session") : null;

			debug(CurrentFacebookUID);
		}

		
		public void debug(string txt)
		{
			//View.Output.InnerHTML = txt + "\n" + View.Output.InnerHTML;
		}

	}
}
