/*
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


namespace SpottedScript.MixmagGreatest.Home
{
	[GlobalMethods]
	public static class PageImplementation
	{
		#region Page functions

		#region FacebookReady
		[PreserveCase]
		public static void FacebookReady()
		{
			Controller.Instance.FacebookReady();
		}
		#endregion

		#endregion
	}
	public class Controller
	{
		#region Initialisation
		#region Controller
		public View View;
		public static Controller Instance;
		public Server Server;
		long PageIdToLike;

		bool DoneControllerInit = false;
		public Controller(View v)
		{
			Instance = this;
			this.View = v;
			this.Server = v.server;

			if (Misc.BrowserIsIE)
				JQueryAPI.JQuery(Document.Body).ready(new Action(initialise));
			else
				initialise();
		}
		#endregion

		#region initialise - safe to affect the dom in here
		void initialise()
		{
			DoneControllerInit = true;
			FacebookReady();

			PageIdToLike = long.ParseInvariant(View.PageIdToLike.Value);

			DomEvent.AddHandler(View.Button, "click", new DomEventHandler(ButtonClick));
			DomEvent.AddHandler(View.Confirm_YesButton, "click", new DomEventHandler(confirmYesButtonClick));
			DomEvent.AddHandler(View.Confirm_NoButton, "click", new DomEventHandler(confirmNoButtonClick));
		}
		#endregion

		#region FacebookReady
		public void FacebookReady()
		{
			if (DoneControllerInit && (bool)Script.Eval("DoneFbAsyncInit"))
			{
				#region updateCurrentFacebookLoginStatus on auth.statusChange
				FB.Event.subscribe(
					"auth.statusChange",
					new Response(
						delegate(Dictionary statusResponse)
						{
							updateCurrentFacebookLoginStatus(statusResponse);
						}
					)
				);
				#endregion

				#region getLoginStatus
				FB.getLoginStatus(
					new Response(
						delegate(Dictionary statusResponse)
						{
							updateCurrentFacebookLoginStatus(statusResponse);

							InitialFacebookLoggedIn = CurrentFacebookLoggedIn;
							InitialFacebookUID = CurrentFacebookUID;
						}
					)
				);
				#endregion

				#region subscribe to edge.create
				FB.Event.subscribe(
					"edge.create",
					new Response(
						delegate(Dictionary edgeCreateResponse)
						{
							edgeCreate();
						}
					)
				);
				#endregion

				initialiseForm();
			}
		}
		#endregion

		#region initialiseForm
		void initialiseForm()
		{
			//changePanel(View.Panel1);
		}
		#endregion

		#region ButtonClick
		void ButtonClick(DomEvent e)
		{
			debug("ButtonClick");

			e.PreventDefault();



		}

		#endregion
		#endregion

		#region confirmFacebookAccount
		bool FacebookAccountConfirmationStepDone = false;
		void confirmFacebookAccount()
		{
			debug("confirmFacebookAccount");
			if (InitialFacebookLoggedIn && !FacebookAccountConfirmationStepDone)
			{
				int thisAsyncOperation1 = RegisterStartAsync("Loading...");
				FB.api(
					"/me",
					new Response(
						delegate(Dictionary meResponse)
						{
							if (RegisterEndAsync(thisAsyncOperation1))
								return;

							View.Confirm_Link.InnerHTML = U.get(meResponse, "name").ToString();
							View.Confirm_Link.Href = U.get(meResponse, "link").ToString();
							View.Confirm_Img.Src = "http://graph.facebook.com/" + U.get(meResponse, "id").ToString() + "/picture";
							changePanel(View.ConfirmPanel);
						}
					)
				);


			}
			else
			{
				showLikePanelIfNeeded();
			}
		}
		#endregion
		#region confirmYesButtonClick
		void confirmYesButtonClick(DomEvent e)
		{
			e.PreventDefault();

			FacebookAccountConfirmationStepDone = true;
			showLikePanelIfNeeded();

		}
		#endregion
		#region voteConfirmNoButtonClick
		void voteConfirmNoButtonClick(DomEvent e)
		{
			e.PreventDefault();

			FacebookAccountConfirmationStepDone = true;
			int thisAsyncOperation = RegisterStartAsync("Logging out...");
			FB.logout(
				new Response(
					delegate(Dictionary logoutResponse)
					{
						if (RegisterEndAsync(thisAsyncOperation))
							return;

						//changePanel(View.Panel1);
					}
				)
			);

		}
		#endregion

		#region showLikePanelIfNeeded
		void showLikePanelIfNeeded()
		{
			debug("showLikePanelIfNeeded");
			int thisAsyncOperation1 = RegisterStartAsync("Loading...");
			FB.api(
				F.d("method", "fql.query", "query", "SELECT type, created_time FROM page_fan WHERE page_id=\"" + PageIdToLike.ToString() + "\" AND uid=\"" + CurrentFacebookUID + "\""),
				new Response(
					delegate(Dictionary likeFqlResponse)
					{
						if (RegisterEndAsync(thisAsyncOperation1))
							return;

						if (U.exists(likeFqlResponse, "/value/type"))
						{
							voteNow();
						}
						else
						{
							//changePanel(View.LikePanel);
						}
					}
				)
			);
		}
		#endregion

		#region edgeCreate
		void edgeCreate()
		{
			voteNow();
		}
		#endregion

		#region voteNow
		void voteNow()
		{
			debug("voteNow");
			int thisAsyncOperation1 = RegisterStartAsync("Voting...");
			//Server.VoteNow(
			//    EntryK,
			//    CompK,
			//    ImageUrl,
			//    new Response(
			//        delegate(Dictionary response)
			//        {
			//            if (RegisterEndAsync(thisAsyncOperation1))
			//                return;

			//            if (U.isTrue(response, "Exception"))
			//            {
			//                showError("Looks like we had a problem...");
			//            }
			//            else if (!U.isTrue(response, "Done"))
			//            {
			//                showError(U.get(response, "Message").ToString());
			//            }
			//            else
			//            {
			//                changePanel(View.Vote2Panel);
			//                View.ArmaniTextFieldPanel.Style.Display = CompK == 2 ? "" : "none";
			//            }
			//        }
			//    )
			//);

		}
		#endregion

		//#region armaniSaveQuestionClick
		//void armaniSaveQuestionClick(DomEvent e)
		//{
		//    e.PreventDefault();

		//    int thisAsyncOperation1 = RegisterStartAsync("Saving...");

		//    Server.SaveQuestion(
		//        EntryK,
		//        CompK,
		//        ImageUrl,
		//        View.ArmaniTextField.Value,
		//        new Response(
		//            delegate(Dictionary response)
		//            {
		//                if (RegisterEndAsync(thisAsyncOperation1))
		//                    return;

		//                if (U.isTrue(response, "Exception"))
		//                {
		//                    showError("Looks like we had a problem...");
		//                }
		//                else if (!U.isTrue(response, "Done"))
		//                {
		//                    showError(U.get(response, "Message").ToString());
		//                }
		//                else
		//                {
		//                    changePanel(View.Vote2Panel);
		//                    View.ArmaniSavedLabel.Style.Display = "";
		//                }
		//            }
		//        )
		//    );
		//}
		//#endregion

		#region Utilities

		#region changePanel
		void changePanel(DivElement panel)
		{
			View.Panel1.Style.Display = panel == View.Panel1 ? "" : "none";
			View.ConfirmPanel.Style.Display = panel == View.ConfirmPanel ? "" : "none";
			View.LikePanel.Style.Display = panel == View.LikePanel ? "" : "none";
			View.Panel2.Style.Display = panel == View.Panel2 ? "" : "none";
		}
		#endregion

		#region updateCurrentFacebookLoginStatus
		bool InitialFacebookLoggedIn = false;
		bool CurrentFacebookLoggedIn = false;
		bool CurrentFacebookConnected = false;
		string CurrentFacebookUID = "0";
		string InitialFacebookUID = "0";
		Dictionary CurrentFacebookSession = null;

		void updateCurrentFacebookLoginStatus(Dictionary statusResponse)
		{
			CurrentFacebookConnected = U.exists(statusResponse, "status") && U.get(statusResponse, "status").ToString() == "connected";
			CurrentFacebookLoggedIn = U.exists(statusResponse, "status") && U.get(statusResponse, "status").ToString() != "unknown";
			CurrentFacebookUID = CurrentFacebookConnected ? U.get(statusResponse, "session/uid").ToString() : "0";
			CurrentFacebookSession = CurrentFacebookConnected ? (Dictionary)U.get(statusResponse, "session") : null;
		}
		#endregion

		#region debug
		public void debug(string txt)
		{
			View.DebugOutput.InnerHTML = txt + "\n" + View.DebugOutput.InnerHTML;
		}
		#endregion

		#region show / hide loading label
		bool AsyncInProgress = false;
		int AsyncOperationCounter = 0;
		Dictionary CancelledAsyncOperations = new Dictionary();
		int RegisterStartAsync(string text)
		{
			return RegisterStartAsyncGeneric(text, true, true);
		}
		int RegisterStartAsyncGeneric(string text, bool setTimer, bool showLoadingLabel)
		{
			AsyncInProgress = true;
			AsyncOperationCounter++;
			int thisAsyncOperationCounter = AsyncOperationCounter;

			if (showLoadingLabel)
			{
				if (text.Length > 0)
					View.LoadingLabel.InnerHTML = "<p>" + text + "<p>";
				else
					View.LoadingLabel.InnerHTML = "<p>Loading...<p>";

				View.LoadingLabel.Style.Display = "";
			}

			if (setTimer)
			{
				Window.SetTimeout(
					new Callback(
						delegate()
						{
							if (AsyncInProgress && thisAsyncOperationCounter == AsyncOperationCounter)
							{
								//... still going
								View.LoadingLabel.InnerHTML = "<p>This seems to be taking a long time... <button id=\"LoadingLabel_CancelLink\">Cancel</button></p>";

								DomEvent.AddHandler(
									Document.GetElementById("LoadingLabel_CancelLink"),
									"click",
									new DomEventHandler(
										delegate(DomEvent e)
										{
											e.PreventDefault();

											CancelledAsyncOperations[thisAsyncOperationCounter.ToString()] = true;

											AsyncInProgress = false;

											if (View.LoadingLabel != null)
												View.LoadingLabel.Style.Display = "none";

											initialiseForm(); //TODO
										}
									)
								);
							}
						}
					),
					5000);
			}
			return thisAsyncOperationCounter;

		}
		bool RegisterEndAsync(int asyncOperationCounter)
		{
			if (CancelledAsyncOperations[asyncOperationCounter.ToString()] != null && (bool)CancelledAsyncOperations[asyncOperationCounter.ToString()])
				return true;

			AsyncInProgress = false;

			if (View.LoadingLabel != null)
				View.LoadingLabel.Style.Display = "none";

			return false;

		}
		#endregion

		#region showError
		void showError(string message)
		{
			Script.Alert(message);
			changePanel(View.Panel1);
		}
		#endregion

		#endregion

	}
}
*/
