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
using JsonAPI;
//using JsonApi;

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
		bool LikedPage;
		string FacebookSource;
		int MixmagGreatestDjK;
		bool SafariKludge = false;

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
			PageIdToLike = long.ParseInvariant(View.PageIdToLike.Value);
			LikedPage = bool.Parse(View.LikedPage.Value);
			MixmagGreatestDjK = int.ParseInvariant(View.MixmagGreatestDjK.Value);
			FacebookSource = View.FacebookSource.Value;
			SafariKludge = bool.Parse(View.SafariKludge.Value);

			DomEvent.AddHandler(View.VoteButton, "click", new DomEventHandler(voteButtonClick));
			DomEvent.AddHandler(View.Confirm_YesButton, "click", new DomEventHandler(confirmYesButtonClick));
			DomEvent.AddHandler(View.Confirm_NoButton, "click", new DomEventHandler(confirmNoButtonClick));
			DomEvent.AddHandler(View.LoggedOutButton, "click", new DomEventHandler(loggedOutButtonClick));
			//DomEvent.AddHandler(View.VoteFollowSkipLink, "click", new DomEventHandler(voteFollowSkipLinkClick));
			//DomEvent.AddHandler(View.VoteTweetSkipLink, "click", new DomEventHandler(voteTweetSkipLinkClick));
			//DomEvent.AddHandler(View.VoteTwitterSkipButton, "click", new DomEventHandler(voteTwitterSkipButtonClick));

			DoneControllerInit = true; //Leave this at the end of initialise()
			FacebookReady();           //Leave this at the end of initialise()
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
			if (MixmagGreatestDjK == 0)
			{
				changePanel(View.NominationsPanel);
			}
			else
			{
				//View.VoteFollowPrompt.InnerHTML = LikedPage ? "Step 1 - click the Follow button:" : "Step 2 - click the Follow button:";
				View.VoteButtonPrompt.InnerHTML = LikedPage ? "Step 1 - click the Vote button:" : "Step 2 - click the Vote button:";
				//View.VoteTweetPrompt.InnerHTML = LikedPage ? "Step 3 - Tweet about your vote:" : "Step 4 - Tweet about your vote:";

				changePanel(View.VotePanel);
			}
		}
		#endregion


		#region voteButtonClick
		void voteButtonClick(DomEvent e)
		{
			debug("ButtonClick");

			e.PreventDefault();

			if (CurrentFacebookLoggedIn && CurrentFacebookConnected)
			{
				confirmFacebookAccount();
			}
			else
			{
				doLogin();
			}
			
		}
		#endregion

		void doLogin()
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
				F.d("scope", "publish_stream")
				//events: create_event,rsvp_event,user_events
				//new: user_address,user_mobile_phone
			);
			//Check out: http://developers.facebook.com/docs/authentication/permissions
			#endregion
		}

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

							View.VoteLikeHolder.Style.Display = "none";
							//View.VoteFollowHolder.Style.Display = "none";
							View.VoteButtonHolder.Style.Display = "none";
							View.VoteConfirmHolder.Style.Display = "";
							//View.VoteTweetHolder.Style.Display = "none";
							View.VoteLoggedOutHolder.Style.Display = "none";
						}
					)
				);
			}
			else
			{
				voteNow();
			}
		}
		#endregion
		#region confirmYesButtonClick
		void confirmYesButtonClick(DomEvent e)
		{
			e.PreventDefault();

			FacebookAccountConfirmationStepDone = true;

			voteNow();


		}
		#endregion
		#region confirmNoButtonClick
		void confirmNoButtonClick(DomEvent e)
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

						View.VoteLikeHolder.Style.Display = "none";
						//View.VoteFollowHolder.Style.Display = "none";
						View.VoteButtonHolder.Style.Display = "none";
						View.VoteConfirmHolder.Style.Display = "none";
						//View.VoteTweetHolder.Style.Display = "none";
						View.VoteLoggedOutHolder.Style.Display = "";
					}
				)
			);

		}
		#endregion
		#region loggedOutButtonClick
		void loggedOutButtonClick(DomEvent e)
		{
			e.PreventDefault();

			doLogin();
		}
		#endregion

		//#region showLikePanelIfNeeded
		//void showLikePanelIfNeeded()
		//{
		//    debug("showLikePanelIfNeeded");
		//    int thisAsyncOperation1 = RegisterStartAsync("Loading...");
		//    FB.api(
		//        F.d("method", "fql.query", "query", "SELECT type, created_time FROM page_fan WHERE page_id=\"" + PageIdToLike.ToString() + "\" AND uid=\"" + CurrentFacebookUID + "\""),
		//        new Response(
		//            delegate(Dictionary likeFqlResponse)
		//            {
		//                if (RegisterEndAsync(thisAsyncOperation1))
		//                    return;

		//                if (U.exists(likeFqlResponse, "/value/type"))
		//                {
		//                    voteNow();
		//                }
		//                else
		//                {
		//                    //changePanel(View.LikePanel);
		//                }
		//            }
		//        )
		//    );
		//}
		//#endregion

		#region edgeCreate
		void edgeCreate()
		{
			LikedPage = true;

			View.NominationsHolder.Style.Display = "";
			View.NominationsLikeButtonHolder.Style.Display = "none";

			View.VoteLikeHolder.Style.Display = "none";
			//View.VoteFollowHolder.Style.Display = "";
			View.VoteButtonHolder.Style.Display = "";
			View.VoteConfirmHolder.Style.Display = "none";
			//View.VoteTweetHolder.Style.Display = "none";
			View.VoteLoggedOutHolder.Style.Display = "none";

		}
		#endregion

		//#region voteFollowSkipLinkClick
		//void voteFollowSkipLinkClick(DomEvent e)
		//{
		//    e.PreventDefault();

		//    View.VoteLikeHolder.Style.Display = "none";
		//    View.VoteFollowHolder.Style.Display = "none";
		//    View.VoteButtonHolder.Style.Display = "";
		//    View.VoteConfirmHolder.Style.Display = "none";
		//    View.VoteTweetHolder.Style.Display = "none";
		//    View.VoteLoggedOutHolder.Style.Display = "none";
		//}
		//#endregion

		//#region voteFollowSkipLinkClick
		//void voteTweetSkipLinkClick(DomEvent e)
		//{
		//    e.PreventDefault();

		//    FB.Canvas.scrollTo(0, 0);
		//    changePanel(View.VoteCompletePanel);

		////	View.VoteLikeHolder.Style.Display = "none";
		////	View.VoteFollowHolder.Style.Display = "none";
		////	View.VoteButtonHolder.Style.Display = "";
		////	View.VoteConfirmHolder.Style.Display = "none";
		////	View.VoteTweetHolder.Style.Display = "none";
		////	View.VoteLoggedOutHolder.Style.Display = "none";
		//}
		//#endregion

		//#region voteFollowSkipLinkClick
		//void voteTwitterSkipButtonClick(DomEvent e)
		//{
		//    e.PreventDefault();

		//    bool isOnFollowPanel = View.VoteFollowHolder.Style.Display == "";
		//    bool isOnTweetPanel = View.VoteTweetHolder.Style.Display == "";

		//    if (isOnFollowPanel)
		//    {
		//        View.VoteLikeHolder.Style.Display = "none";
		//        View.VoteFollowHolder.Style.Display = "none";
		//        View.VoteButtonHolder.Style.Display = "";
		//        View.VoteConfirmHolder.Style.Display = "none";
		//        View.VoteTweetHolder.Style.Display = "none";
		//        View.VoteLoggedOutHolder.Style.Display = "none";
		//    }
		//    else if (isOnTweetPanel)
		//    {
		//        FB.Canvas.scrollTo(0, 0);
		//        changePanel(View.VoteCompletePanel);
		//    }
		//}
		//#endregion

		#region voteNow
		void voteNow()
		{
			int thisAsyncOperation1 = RegisterStartAsync("Voting...");
			Server.VoteNow(
				MixmagGreatestDjK,
				FacebookSource,
				View.VoteFacebookUpdateCheckbox1.Checked,
				new Response(
					delegate(Dictionary response)
					{
						if (RegisterEndAsync(thisAsyncOperation1))
							return;

						if (U.isTrue(response, "Exception"))
						{
							showError("Looks like we had a problem...");
						}
						else if (!U.isTrue(response, "Done"))
						{
							showError(U.get(response, "Message").ToString());
						}
						else
						{
							FB.Canvas.scrollTo(0, 0);
							changePanel(View.VoteCompletePanel);
							//View.VoteLikeHolder.Style.Display = "none";
							//View.VoteFollowHolder.Style.Display = "none";
							//View.VoteButtonHolder.Style.Display = "none";
							//View.VoteConfirmHolder.Style.Display = "none";
							//View.VoteTweetHolder.Style.Display = "";
							//View.VoteLoggedOutHolder.Style.Display = "none";
						}
					}
				)
			);
			
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
		void changePanel(DOMElement panel)
		{
			View.NominationsPanel.Style.Display = panel == View.NominationsPanel ? "" : "none";
			View.VotePanel.Style.Display = panel == View.VotePanel ? "" : "none";
			//View.ConfirmPanel.Style.Display = panel == View.ConfirmPanel ? "" : "none";
			//View.LoggedOutPanel.Style.Display = panel == View.LoggedOutPanel ? "" : "none";
			View.VoteCompletePanel.Style.Display = panel == View.VoteCompletePanel ? "" : "none";
			
		}
		#endregion

		#region updateCurrentFacebookLoginStatus
		bool InitialFacebookLoggedIn = false;
		bool CurrentFacebookLoggedIn = false;
		bool CurrentFacebookConnected = false;
		string CurrentFacebookUID = "0";
		string InitialFacebookUID = "0";
		Dictionary CurrentFacebookAuthResponse = null;

		void updateCurrentFacebookLoginStatus(Dictionary statusResponse)
		{
			CurrentFacebookConnected = U.exists(statusResponse, "status") && U.get(statusResponse, "status").ToString() == "connected";
			CurrentFacebookLoggedIn = U.exists(statusResponse, "status") && U.get(statusResponse, "status").ToString() != "unknown";
			CurrentFacebookUID = CurrentFacebookConnected ? U.get(statusResponse, "authResponse/userID").ToString() : "0";
			CurrentFacebookAuthResponse = CurrentFacebookConnected ? (Dictionary)U.get(statusResponse, "authResponse") : null;


			if (SafariKludge)
			{
				if (U.exists(statusResponse, "authResponse"))
				{
					int thisAsyncOperation1 = RegisterStartAsync("Loading...");
					Server.SetCookie(
						//JSON.stringify(U.get(statusResponse, "authResponse")),
						U.get(statusResponse, "authResponse/signedRequest").ToString(),
						new Response(
							delegate(Dictionary response)
							{
								if (RegisterEndAsync(thisAsyncOperation1))
									return;

								if (U.isTrue(response, "Exception"))
								{
									showError("Looks like we had a problem...");
								}
								else if (!U.isTrue(response, "Done"))
								{
									showError(U.get(response, "Message").ToString());
								}
								else
								{
									//Script.Alert("Done");
								}
							}
						)
					);
				}
			}


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
			initialiseForm();
		}
		#endregion

		#endregion

	}
}
