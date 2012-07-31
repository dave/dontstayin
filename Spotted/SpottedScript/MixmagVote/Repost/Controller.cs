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

namespace SpottedScript.MixmagVote.Repost
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
		int EntryK;
		public View View;
		public static Controller Instance;
		public Server Server;
		#region Controller(View v)
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
			DomEvent.AddHandler(View.Repost1Button, "click", new DomEventHandler(repost1ButtonClick));

			DomEvent.AddHandler(View.RepostConfirm_YesButton, "click", new DomEventHandler(repostConfirmYesButtonClick));
			DomEvent.AddHandler(View.RepostConfirm_NoButton, "click", new DomEventHandler(repostConfirmNoButtonClick));

			EntryK = int.ParseInvariant(View.EntryK.Value);
		
			DoneControllerInit = true;
			FacebookReady();

		}
		#endregion
		#region FacebookReady
		public void FacebookReady()
		{
			debug("FacebookReady start: DoneControllerInit = " + DoneControllerInit.ToString() + ", DoneFbAsyncInit = " + ((bool)Script.Eval("DoneFbAsyncInit")).ToString());

			if (DoneControllerInit && (bool)Script.Eval("DoneFbAsyncInit"))
			{
				debug("FacebookReady running");

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

							debug(U.toString(statusResponse));

							InitialFacebookLoggedIn = CurrentFacebookLoggedIn;
							InitialFacebookUID = CurrentFacebookUID;
								
						}
					)
				);
				#endregion

				initialiseForm();
			}
		}
		bool DoneControllerInit = false;
		#endregion
		#region initialiseForm
		void initialiseForm()
		{
			changePanel(View.Repost1Panel);
		}
		#endregion
		#endregion


		#region repost1ButtonClick
		public void repost1ButtonClick(DomEvent e)
		{
			e.PreventDefault();

			repost1ButtonClickInternal();

		}
		void repost1ButtonClickInternal()
		{
			debug("entry1ButtonClickInternal: CurrentFacebookLoggedIn = " + CurrentFacebookLoggedIn.ToString() + ", CurrentFacebookConnected = " + CurrentFacebookConnected.ToString());
			if (CurrentFacebookLoggedIn && CurrentFacebookConnected)
			{
				confirmFacebookAccount();
			}
			else
			{
				#region login
				int asyncOperation = RegisterStartAsyncGeneric("Connecting...", false, false); // Don't set the timer to show the cencel button.
				FB.login(
					new Response(
						delegate(Dictionary loginResponse)
						{
							if (RegisterEndAsync(asyncOperation))
								return;

							if (CurrentFacebookConnected)
							{
								confirmFacebookAccount();
							}
							else
							{
								showError("Looks like Facebook had trouble getting you connected.");
							}
						}
					),
					//F.d("perms", "email,publish_stream,offline_access,user_birthday,user_hometown,user_location,create_event,rsvp_event,user_events")
					//F.d("perms", "email,publish_stream,offline_access,user_birthday,user_hometown,user_location,rsvp_event")
					//F.d("perms", "email,publish_stream,offline_access,user_birthday,user_hometown,user_location")
					F.d("perms", "email,publish_stream")
					//events: create_event,rsvp_event,user_events
					//new: user_address,user_mobile_phone
				);
				//Check out: http://developers.facebook.com/docs/authentication/permissions
				#endregion
			}

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
		#endregion

		#region confirmFacebookAccount
		void confirmFacebookAccount()
		{

			debug("InitialFacebookLoggedIn = " + InitialFacebookLoggedIn.ToString());
			debug("FacebookAccountConfirmationStepDone = " + FacebookAccountConfirmationStepDone.ToString());

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

							View.RepostConfirm_Link.InnerHTML = U.get(meResponse, "name").ToString();
							View.RepostConfirm_Link.Href = U.get(meResponse, "link").ToString();
							View.RepostConfirm_Img.Src = "http://graph.facebook.com/" + U.get(meResponse, "id").ToString() + "/picture";
							changePanel(View.RepostConfirmPanel);
						}
					)
				);

				
			}
			else
			{
				repost();
			}
		}
		#endregion
		#region repostConfirmYesButtonClick
		void repostConfirmYesButtonClick(DomEvent e)
		{
			e.PreventDefault();

			FacebookAccountConfirmationStepDone = true;
			repost();

		}
		#endregion
		#region repostConfirmNoButtonClick
		void repostConfirmNoButtonClick(DomEvent e)
		{

			e.PreventDefault();


			debug("entryConfirmNoButtonClick");


			FacebookAccountConfirmationStepDone = true;
			int thisAsyncOperation = RegisterStartAsync("Logging out...");
			FB.logout(
				new Response(
					delegate(Dictionary logoutResponse)
					{
						if (RegisterEndAsync(thisAsyncOperation))
							return;

						changePanel(View.Repost1Panel);
					}
				)
			);

		}
		#endregion

		#region repost
		void repost()
		{
			int thisAsyncOperation1 = RegisterStartAsync("Posting your message...");
			Server.RepostNow(
				EntryK,
				View.Repost1FacebookMessageTextbox.Value,
				new Response(
					delegate(Dictionary response)
					{
						if (RegisterEndAsync(thisAsyncOperation1))
							return;

						if (U.isTrue(response, "Exception") || !U.isTrue(response, "Done"))
						{
							showError("Looks like we had a problem...");
						}
						else
						{
							debug(U.toString(response));
							changePanel(View.Repost2Panel);
						}
					}
				)
			);
		}
		#endregion


		
		
		#region Utilities

		#region changePanel
		void changePanel(DivElement panel)
		{
			View.Repost1Panel.Style.Display = panel == View.Repost1Panel ? "" : "none";
			View.RepostConfirmPanel.Style.Display = panel == View.RepostConfirmPanel ? "" : "none";
			View.Repost2Panel.Style.Display = panel == View.Repost2Panel ? "" : "none";
		}
		#endregion

		#region showError
		void showError(string message)
		{
			Script.Alert(message);
			changePanel(View.Repost1Panel);
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
		#region updateCurrentFacebookLoginStatus
		bool InitialFacebookLoggedIn = false;
		bool CurrentFacebookLoggedIn = false;
		bool CurrentFacebookConnected = false;
		string CurrentFacebookUID = "0";
		string InitialFacebookUID = "0";
		Dictionary CurrentFacebookSession = null;
		bool FacebookAccountConfirmationStepDone = false;
		void updateCurrentFacebookLoginStatus(Dictionary statusResponse)
		{
			debug("START updateCurrentFacebookLoginStatus CurrentFacebookConnected = " + CurrentFacebookConnected.ToString() + ", CurrentFacebookLoggedIn = " + CurrentFacebookLoggedIn.ToString());

			CurrentFacebookConnected = U.exists(statusResponse, "status") && U.get(statusResponse, "status").ToString() == "connected";
			CurrentFacebookLoggedIn = U.exists(statusResponse, "status") && U.get(statusResponse, "status").ToString() != "unknown";

			CurrentFacebookUID = CurrentFacebookConnected ? U.get(statusResponse, "session/uid").ToString() : "0";
			CurrentFacebookSession = CurrentFacebookConnected ? (Dictionary)U.get(statusResponse, "session") : null;

			debug("DONE updateCurrentFacebookLoginStatus CurrentFacebookConnected = " + CurrentFacebookConnected.ToString() + ", CurrentFacebookLoggedIn = " + CurrentFacebookLoggedIn.ToString());

		}
		#endregion
		#endregion

	}
}
