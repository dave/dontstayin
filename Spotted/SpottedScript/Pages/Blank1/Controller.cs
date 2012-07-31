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

namespace SpottedScript.Pages.Blank1
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
			DomEvent.AddHandler(View.Button1, "click", new DomEventHandler(button1Click));
			DomEvent.AddHandler(View.Button2, "click", new DomEventHandler(button2Click));
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

		public void button1Click(DomEvent e)
		{
			e.PreventDefault();
			debug("click!");

			FB.getLoginStatus(
				new Response(
					delegate(Dictionary statusResponse)
					{
						updateCurrentFacebookLoginStatus(statusResponse);
					}
				)
			);

			FB.Event.subscribe(
				"edge.create",
				new Response(
					delegate(Dictionary edgeCreateResponse)
					{
						debug("edge.create");
					}
				)
			);

		}

		public void button2Click(DomEvent e)
		{
			e.PreventDefault();

			FB.api(
				F.d("method", "fql.query", "query", "SELECT type, created_time FROM page_fan WHERE page_id=\"18658586341\" AND uid=\"" + CurrentFacebookUID + "\""),
				new Response(
					delegate(Dictionary likeFqlResponse)
					{

						debug(U.toString(likeFqlResponse));

						//if (U.exists(likeFqlResponse, "/value/type"))
						//{
						//    JQueryAPI.JQuery(View.ConnectDialog).dialog("close");
						//}
						//else
						//{
						//    changePanel("View.Connect_LikeButtonPanel");
						//    FB.Event.subscribe(
						//        "edge.create",
						//        new Response(
						//            delegate(Dictionary edgeCreateResponse)
						//            {
						//                JQueryAPI.JQuery(View.ConnectDialog).dialog("close");
						//            }
						//        )
						//    );
						//}
					}
				)
			);

			
		}

		public void debug(string txt)
		{
			View.Output.InnerHTML = txt + "\n" + View.Output.InnerHTML;
		}
	}
}
