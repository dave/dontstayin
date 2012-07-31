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


namespace SpottedScript.MixmagVote.Vote
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
		int EntryK;
		int CompK;
		string ImageUrl;

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
			EntryK = int.ParseInvariant(View.EntryK.Value);
			CompK = int.ParseInvariant(View.CompK.Value);
			ImageUrl = View.ImageUrl.Value;

			DomEvent.AddHandler(View.Vote1VoteButton, "click", new DomEventHandler(vote1VoteButtonClick));
			DomEvent.AddHandler(View.VoteConfirm_YesButton, "click", new DomEventHandler(voteConfirmYesButtonClick));
			DomEvent.AddHandler(View.VoteConfirm_NoButton, "click", new DomEventHandler(voteConfirmNoButtonClick));
			if (CompK == 2)
				DomEvent.AddHandler(View.ArmaniSubmitButton, "click", new DomEventHandler(armaniSaveQuestionClick));
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
			changePanel(View.Vote1Panel);
		}
		#endregion

		#region vote1VoteButtonClick
		void vote1VoteButtonClick(DomEvent e)
		{
			debug("vote1VoteButtonClick");

			e.PreventDefault();

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
					F.d("scope", "email,publish_stream")
				);
				//Check out: http://developers.facebook.com/docs/authentication/permissions
				#endregion
			}


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

							View.VoteConfirm_Link.InnerHTML = U.get(meResponse, "name").ToString();
							View.VoteConfirm_Link.Href = U.get(meResponse, "link").ToString();
							View.VoteConfirm_Img.Src = "http://graph.facebook.com/" + U.get(meResponse, "id").ToString() + "/picture";
							changePanel(View.VoteConfirmPanel);
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
		#region voteConfirmYesButtonClick
		void voteConfirmYesButtonClick(DomEvent e)
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

						changePanel(View.Vote1Panel);
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
							changePanel(View.VoteLikePanel);
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
			Server.VoteNow(
				EntryK,
				CompK,
				ImageUrl,
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
							changePanel(View.Vote2Panel);
							View.ArmaniTextFieldPanel.Style.Display = CompK == 2 ? "" : "none";
						}
					}
				)
			);
			
		}
		#endregion

		#region armaniSaveQuestionClick
		void armaniSaveQuestionClick(DomEvent e)
		{
			e.PreventDefault();

			int thisAsyncOperation1 = RegisterStartAsync("Saving...");

			Server.SaveQuestion(
				EntryK,
				CompK,
				ImageUrl,
				View.ArmaniTextField.Value,
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
							changePanel(View.Vote2Panel);
							View.ArmaniSavedLabel.Style.Display = "";
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
			View.Vote1Panel.Style.Display = panel == View.Vote1Panel ? "" : "none";
			View.VoteConfirmPanel.Style.Display = panel == View.VoteConfirmPanel ? "" : "none";
			View.VoteLikePanel.Style.Display = panel == View.VoteLikePanel ? "" : "none";
			View.Vote2Panel.Style.Display = panel == View.Vote2Panel ? "" : "none";
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
			changePanel(View.Vote1Panel);
		}
		#endregion

		#endregion

	}
}
