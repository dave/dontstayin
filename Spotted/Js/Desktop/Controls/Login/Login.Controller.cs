//using System;
//using Sys.UI;
//using JQ;
//using ScriptSharpLibrary;
//using System.DHTML;
//using FacebookAPI;
//using ImportedUtilities;

using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;
using Js.jQueryUIAPI;
using Js.jQuerySelectBoxesAPI;
using System.Runtime.CompilerServices;

#if MOBILE
namespace Js.Mobile.Controls.Login
#else
namespace Js.Controls.Login
#endif
{

	[GlobalMethods]
	public static class PageImplementation
	{
		#region Page functions

		#region WhenLoggedIn
		[PreserveCase]
		public static bool WhenLoggedIn(Action onLogin)
		{
			Action action = new Action(
				delegate()
				{
					if (Controller.Instance.CurrentIsLoggedIn)
					{
						onLogin();
					}
					else
					{
						Controller.Instance.Show(
							new ActionBoolBool(
								delegate(bool loggedIn, bool stateChanged)
								{
									if (loggedIn)
										onLogin();
								}
							)
						);
					}
				}
			);

			if (Controller.Instance.Initialised)
				action();
			else
				Controller.Instance.WhenInitialisedAction = action;

			return false;
		}
		#endregion

		#region WhenLoggedInButton
		[PreserveCase]
		public static bool WhenLoggedInButton(InputElement element)
		{
			return WhenLoggedInButtonValidator(element, "");
		}
		#endregion

		#region WhenLoggedInButtonValidator
		[PreserveCase]
		public static bool WhenLoggedInButtonValidator(InputElement element, string validators)
		{
			PageImplementation.WhenLoggedIn(
				new Action(
					delegate()
					{
						Script.Eval("WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions(\"" + element.ID.Replace("_", "$") + "\", \"\", true, \"" + validators + "\", \"\", false, true));");
					}
				)
			);
			return false;
		}
		#endregion

		#region WhenLoggedInButtonNoValidation
		[PreserveCase]
		public static bool WhenLoggedInButtonNoValidation(InputElement element)
		{
			PageImplementation.WhenLoggedIn(
				new Action(
					delegate()
					{
						Script.Eval("__doPostBack(\"" + element.ID.Replace("_", "$") + "\",'');");
					}
				)
			);
			return false;
		}
		#endregion

		#region WhenLoggedInHtmlButton
		[PreserveCase]
		public static bool WhenLoggedInHtmlButton(InputElement element)
		{
			PageImplementation.WhenLoggedIn(
				new Action(
					delegate()
					{
						Script.Eval("__doPostBack('" + element.ID.Replace("_", "$") + "','');");
					}
				)
			);
			return false;
		}
		#endregion

		#region WhenLoggedInRadio
		/// <summary>
		/// Remember this should be followed by "return true;", so that the radio button doesn't reset to the previous value.
		/// </summary>
		/// <param name="element"></param>
		[PreserveCase]
		public static bool WhenLoggedInRadio(InputElement element)
		{
			PageImplementation.WhenLoggedIn(
				new Action(
					delegate()
					{
						Script.Eval("setTimeout('__doPostBack(\\'" + element.ID.Replace("_", "$") + "\\',\\'\\')', 0);");
					}
				)
			);
			return true;
		}
		#endregion

		#region WhenLoggedInAnchor
		[PreserveCase]
		public static bool WhenLoggedInAnchor(AnchorElement anchor)
		{
			PageImplementation.LogInTransfer(anchor.Href);
			return false;
		}
		#endregion

		#region ConnectButtonClick
		[PreserveCase]
		public static void ConnectButtonClick()
		{
			
			Action action = new Action(
				delegate()
				{
					Controller.Instance.Show(
						new ActionBoolBool(
							delegate(bool loggedIn, bool stateChanged)
							{
								if (stateChanged)
								{
									if (loggedIn)
										Window.Location.Href = Window.Location.Href;
									else
										Window.Location.Href = "/";
								}
							}
						)
					);
				}
			);

			if (Controller.Instance.Initialised)
				action();
			else
				Controller.Instance.WhenInitialisedAction = action;

		}
		#endregion

		#region LogInTransfer
		[PreserveCase]
		public static void LogInTransfer(string url)
		{
			Action action = new Action(
				delegate()
				{
					if (Controller.Instance.CurrentIsLoggedIn)
					{
						Window.Location.Href = url;
					}
					else
					{
						Controller.Instance.Show(
							new ActionBoolBool(
								delegate(bool loggedIn, bool stateChanged)
								{
									if (loggedIn)
										Window.Location.Href = url;
								}
							)
						);
					}
				}
			);

			if (Controller.Instance.Initialised)
				action();
			else
				Controller.Instance.WhenInitialisedAction = action;

		}
		#endregion

		#region IsLoggedIn
		[PreserveCase]
		public static bool IsLoggedIn()
		{
			return Controller.Instance.CurrentIsLoggedIn;
		}
		#endregion

		#region LoginDebug
		[PreserveCase]
		public static void LoginDebug(string text)
		{
			Controller.Instance.debug(text);
		}
		#endregion

		#region LoginFacebookReady
		[PreserveCase]
		public static void LoginFacebookReady()
		{
			Controller.Instance.FacebookReady("LoginFacebookReady");
		}
		#endregion

		#endregion
	}
	public class Controller
	{


		#region Variables
		View View;
		Server Server;
		public static Controller Instance;
		public bool AutoLogin = false;
		public bool AutoLogout = false;
		public int AutoLoginUsrK = 0;
		public string AutoLoginUsrLoginString = "";
		public string AutoLoginRedirectUrl = "";
		public bool AutoLoginLogOutFirst = false;
		public string AutoLoginUsrEmail = "";
		public bool AutoLoginUsrIsSkeleton = false;
		public bool AutoLoginUsrIsEnhancedSecurity = false;
		public bool AutoLoginUsrIsFacebookNotConfirmed = false;
		public bool AutoLoginUsrNeedsCaptcha = false;
		public string AutoLoginUsrCaptchaEncrypted = "";

		public int AutoLoginUsrHomePlaceK = 0;
		public string AutoLoginUsrHomePlaceName = "";
		public int AutoLoginUsrHomeCountryK = 0;
		public string AutoLoginUsrHomeCountryName = "";
		public bool AutoLoginUsrHomeGoodMatch = false;
		public int AutoLoginUsrFavouriteMusicK = 0;
		public bool AutoLoginUsrSendSpottedEmails = false;
		public bool AutoLoginUsrSendEflyers = false;


		string AutoLoginNickname = "";
		string AutoLoginLink = "";
		string AutoLoginEmail = "";
		bool AutoLoginStringMatch = false;

		bool InitialIsLoggedIn;
		public bool Initialised = false;
		public Action WhenInitialisedAction = null;
		bool InitialFacebookLoggedIn;
		bool InitialFacebookConnected;
		string InitialFacebookUID = "0";
		Dictionary<object, object> InitialFacebookAuthResponse = null;
		bool InitialAuthCookieHasError = false;

		bool OpenDialogIsLoggedIn = false;

		public bool CurrentIsLoggedIn = false;
		public bool CurrentIsLoggedInWithFacebook = false;
		bool CurrentFacebookLoggedIn = false;
		bool CurrentFacebookConnected = false;
		string CurrentFacebookUID = "0";
		Dictionary<object, object> CurrentFacebookAuthResponse = null;
		bool CurrentAuthCookieHasError = false;

		string CurrentAuthUsrK = "0";
		string CurrentAuthUsrFacebookUID = "0";
		string CurrentAuthUsrNickName = "";
		string CurrentAuthUsrLink = "";
		string CurrentAuthUsrEmail = "";
		bool CurrentAuthUsrHasNullPassword = false;

		ActionBoolBool ExitEvent;

		bool DoneControllerInit = false;

		bool FacebookAccountNeedsConfirmationBecauseInitiallyFacebookLoggedIn = false;
		bool FacebookAccountNeedsConfirmationBecauseInitiallyFacebookConnectedAndSiteLoggedOut = false;
		bool FacebookAccountConfirmationStepDone = false;

		int NoFacebookSignUp2PanelLoginUsrK = 0;
		string NoFacebookSignUp1PanelSource = "";
		string NoFacebookSignUp2PanelSource = "";
		string DetailsShowSource = "";
		string CaptchaPanelSource = "";
		int DetailsDefaultCountryK;
		int DetailsDefaultPlaceK;
		bool DetailsDefaultPlaceGoodMatch;
		jQuerySelectBoxesObject DetailsPlaceDropDownJ;
		jQuerySelectBoxesObject DetailsCountryDropDownJ;
		bool DetailsCountryDropDownPopulated = false;
		bool DetailsPlaceDropDownPopulated = false;
		int DetailsPlaceDropDownPopulatedCountryK;
		int DetailsCountrySelectedK = 0;
		int DetailsPlaceSelectedK = 0;
		int DetailsPlacePreviouslySelectedIndex;
		int DetailsCountryPreviouslySelectedIndex;
		bool DetailsPlaceDropDownVisible = false;
		#endregion

		#region initialiser - don't affect the dom in here or ie will spaz out
		public Controller(View view)
		{
			Instance = this;
			this.View = view;
			this.Server = view.server;

			if (Misc.BrowserIsIE)
				jQuery.OnDocumentReady(new Action(initialise));
			else
				initialise();
		}
		#endregion
		#region initialise - safe to affect the dom in here
		void initialise()
		{
			jQueryUI.FromObject(View.ConnectDialog).Dialog(
				F.d(
					"autoOpen", false,
					"width", 505,
					"height", 280,
					"modal", true,
					"resizable", false,
					"zIndex", 990,
					"draggable", false,
					"closeOnEscape", false,
					"open", new ActionObjectObject(delegate(object ev, object ui) { jQuery.Select(".ui-dialog-titlebar-close").Hide(); })
				)
			);
			View.ConnectDialog.Style.Display = "";

			DoneControllerInit = true;
			FacebookReady("initialise");
		}
		#endregion
		#region FacebookReady
		public void FacebookReady(string from)
		{
			if (DoneControllerInit && (bool)Script.Eval("DoneFbAsyncInit"))
			{
				#region Auth details stuff
				
				InitialAuthCookieHasError = getBoolFromBasePage("AuthCookieHasError");
				CurrentAuthCookieHasError = InitialAuthCookieHasError;

				updateAuthDetailsFromDsiCookie();

				#region updateCurrentFacebookLoginStatus on auth.statusChange
				FB.Event.subscribe(
					"auth.statusChange",
					new Response(
						delegate(Dictionary<object, object> statusResponse)
						{
							updateCurrentFacebookLoginStatus(statusResponse);
						}
					)
				);
				#endregion

				FB.getLoginStatus(
					new Response(
						delegate(Dictionary<object, object> statusResponse)
						{
							updateCurrentFacebookLoginStatus(statusResponse);

							InitialIsLoggedIn = CurrentIsLoggedIn;
							InitialFacebookLoggedIn = CurrentFacebookLoggedIn;
							InitialFacebookConnected = CurrentFacebookConnected;
							InitialFacebookUID = CurrentFacebookUID;
							InitialFacebookAuthResponse = CurrentFacebookAuthResponse;
							Initialised = true;

							AutoLogout = getBoolFromPage("AutoLogout_Value");
							AutoLogin = getBoolFromPage("AutoLogin_Value");
							AutoLoginRedirectUrl = getStringFromPage("AutoLogin_RedirectUrl");
							AutoLoginUsrK = getIntFromPage("AutoLogin_UsrK");
							AutoLoginUsrLoginString = getStringFromPage("AutoLogin_String");
							AutoLoginLogOutFirst = getBoolFromPage("AutoLogin_LogOutFirst");
							AutoLoginUsrEmail = getStringFromPage("AutoLogin_UsrEmail");
							AutoLoginUsrIsSkeleton = getBoolFromPage("AutoLogin_UsrIsSkeleton");
							AutoLoginUsrIsEnhancedSecurity = getBoolFromPage("AutoLogin_UsrIsEnhancedSecurity");
							AutoLoginUsrIsFacebookNotConfirmed = getBoolFromPage("AutoLogin_UsrIsFacebookNotConfirmed");
							AutoLoginUsrNeedsCaptcha = getBoolFromPage("AutoLogin_UsrNeedsCaptcha");
							AutoLoginUsrCaptchaEncrypted = getStringFromPage("AutoLogin_UsrCaptchaEncrypted");

							AutoLoginUsrHomePlaceK = getIntFromPage("AutoLogin_HomePlaceK");
							AutoLoginUsrHomePlaceName = getStringFromPage("AutoLogin_HomePlaceName");
							AutoLoginUsrHomeCountryK = getIntFromPage("AutoLogin_HomeCountryK");
							AutoLoginUsrHomeCountryName = getStringFromPage("AutoLogin_HomeCountryName");
							AutoLoginUsrHomeGoodMatch = getBoolFromPage("AutoLogin_HomeGoodMatch");
							AutoLoginUsrFavouriteMusicK = getIntFromPage("AutoLogin_FavouriteMusicK");
							AutoLoginUsrSendSpottedEmails = getBoolFromPage("AutoLogin_SendSpottedEmails");
							AutoLoginUsrSendEflyers = getBoolFromPage("AutoLogin_SendEflyers");

							if (InitialFacebookLoggedIn)
								FacebookAccountNeedsConfirmationBecauseInitiallyFacebookLoggedIn = true; //This was in some of the conditions below, but I think it should be global.

							if (InitialFacebookConnected && !InitialIsLoggedIn)
								FacebookAccountNeedsConfirmationBecauseInitiallyFacebookConnectedAndSiteLoggedOut = true;

							if (AutoLogout)
							{
								#region logoutNow
								logoutNow(
									true,
									new Action(
										delegate()
										{
											redirectToHomePage(); //oddity - js error if this is in here...
										}
									),
									false
								);
								#endregion
							}
							else if (AutoLogin)
							{
								if (CurrentIsLoggedIn && CurrentAuthUsrK == AutoLoginUsrK.ToString() && !AutoLoginLogOutFirst)
								{
									#region We're logged in with the correct DSI user - just redirect to the correct page.
									//This should have already been done by the server, but maybe not.
									Window.Location.Href = AutoLoginRedirectUrl;
									#endregion
								}
								else if (CurrentIsLoggedIn)
								{
									#region If we're logged in as the wrong user (or we're on the 'logout first' version of the autologin page), log out before opening the panel
									LogOutAndDoAction(
										new Action(
											delegate()
											{
												autoLoginShowPanel();
											}
										),
										false
									);
									#endregion
								}
								else
								{
									#region If we're not logged in, remove the DSI auth cookie and show the panel.
									removeAuthCookie();
									autoLoginShowPanel();
									#endregion
								}
							}
							//else if (CurrentFacebookUID != "0" && CurrentAuthUsrFacebookUID != "0" && CurrentFacebookUID != CurrentAuthUsrFacebookUID)
							//{
							//    #region Handle inconsistant auth state...
							//    FacebookAccountNeedsConfirmationBecauseInconsistantAuth = true;
							//    string previousUsrKFromDsiCookie = CurrentAuthUsrK;
							//    removeAuthCookie();
							//    Show(
							//        new ActionBoolBool(
							//            delegate(bool loggedIn, bool stateChanged)
							//            {
							//                if (loggedIn)
							//                {
							//                    if (previousUsrKFromDsiCookie != CurrentAuthUsrK)
							//                        Window.Location.Href = Window.Location.Href;
							//                }
							//                else
							//                    Window.Location.Href = "/";
							//            }
							//        )
							//    );
							//    #endregion
							//}
							else if (WhenInitialisedAction != null)
							{
								WhenInitialisedAction();
							}

						}
					)
				);
				#endregion
			}
		}
		#region autoLoginShowPanel
		void autoLoginShowPanel()
		{
			Controller.Instance.Show(
				new ActionBoolBool(
					delegate(bool loggedIn, bool stateChanged)
					{
						if (loggedIn)
							Window.Location.Href = AutoLoginRedirectUrl;
						else
							Window.Location.Href = "/";
					}
				)
			);
		}
		#endregion
		#region redirectToHomePage
		void redirectToHomePage()
		{
			Window.Location.Href = "/"; // for some reason there was a js error when this was inline code.
		}
		#endregion
		#endregion

		#region Show
		public void Show(ActionBoolBool onExit)
		{
			ExitEvent = onExit;
			OpenDialogIsLoggedIn = CurrentIsLoggedIn;

			changePanel("View.Connect_LoadingPanel");

			initialiseForm();

			jQueryUI.FromObject(View.ConnectDialog).Dialog("open");

			jQueryUI.FromObject(View.ConnectDialog).Dialog(
				F.d("close",
					new Action(
						delegate()
						{
							ExitEvent(
								CurrentIsLoggedIn,
								CurrentIsLoggedIn != OpenDialogIsLoggedIn
							);
						}
					)
				)
			);
			
		}
		#endregion
		#region initialiseForm
		void initialiseForm()
		{
			if (CurrentIsLoggedIn)
			{
				CurrentAuthUsrNickName = getStringFromBasePage("UsrNickname");
				CurrentAuthUsrLink = getStringFromBasePage("UsrLink");

				jQueryUI.FromObject(View.ConnectDialog).Dialog("open");

				showLoggedInPanel(CurrentAuthUsrNickName.Length > 0 ? CurrentAuthUsrLink : "???");
				
			}
			//else if (CurrentFacebookConnected)
			//{
			//    changePanel("View.Connect_LoadingPanel");
			//    CloseOnLoggedIn = false;
			//    configureFormConnected();
			//}
			else
			{
				if (CurrentFacebookConnected)
				{
					CloseOnLoggedIn = false; //from above???
				}
				jQueryUI.FromObject(View.ConnectDialog).Dialog("open");
				changePanel("View.Connect_LoggedOutPanel");
			}
		}
		#endregion
		#region configureFormConnected
		bool FacebookEmailMatch = false;
		bool FacebookEmailMatchToCurrentUser = false;
		bool FacebookEmailMatchEnhancedSecurity = false;
		string FacebookEmailMatchNickName = "";
		string FacebookEmailMatchEmail = "";
		bool CloseOnLoggedIn = false;
		void configureFormConnected()
		{
			#region Server.GetUserByFacebookUID
			int thisAsyncOperation = RegisterStartAsync("Connecting...");
			Server.GetUserByFacebookUID(
				AutoLoginUsrK,
				AutoLoginUsrLoginString,
				new Response(
					delegate(Dictionary<object, object> response)
					{
						if (RegisterEndAsync(thisAsyncOperation))
							return;

						if (U.isTrue(response, "Exception"))
						{
							jQueryUI.FromObject(View.ConnectDialog).Dialog("open");
							showError(1, "Internal server error");
						}
						else
						{

							FacebookEmailMatch = U.isTrue(response, "FacebookEmailMatch");
							FacebookEmailMatchToCurrentUser = U.isTrue(response, "FacebookEmailMatchToCurrentUser");
							FacebookEmailMatchEnhancedSecurity = U.isTrue(response, "EnhancedSecurity");
							bool facebookUidMatch = U.isTrue(response, "FacebookUIDMatch");
							bool autoLoginMatch = U.isTrue(response, "FacebookAutoLoginUsrMatch");

							AutoLoginNickname = U.exists(response, "AutoLoginUsr/NickName") ? U.get(response, "AutoLoginUsr/NickName").ToString() : "";
							AutoLoginLink = U.exists(response, "AutoLoginUsr/Link") ? U.get(response, "AutoLoginUsr/Link").ToString() : "";
							AutoLoginEmail = U.exists(response, "AutoLoginUsr/Email") ? U.get(response, "AutoLoginUsr/Email").ToString() : "";
							AutoLoginStringMatch = U.exists(response, "AutoLoginUsr/LoginStringMatch") ? (bool)U.get(response, "AutoLoginUsr/LoginStringMatch") : false;

							if (FacebookAccountNeedsConfirmationBecauseInitiallyFacebookConnectedAndSiteLoggedOut)
							{
								showConfirmFacebookPanel();
								return;
							}
							else if (facebookUidMatch)
							{
								setAuthCookie((Dictionary<object, object>)U.get(response, "AuthCookie"), (Dictionary<object, object>)U.get(response, "AuthUsr"));

								if (CloseOnLoggedIn) //in fact i'm going to!
								{
									detectAutoLoginProblem(false);
								}
								else
								{
									jQueryUI.FromObject(View.ConnectDialog).Dialog("open");
									showLoggedInPanel(CurrentAuthUsrNickName.Length > 0 ? CurrentAuthUsrLink : "???");
								}
							}
							else if (FacebookAccountNeedsConfirmationBecauseInitiallyFacebookLoggedIn)
							{
								showConfirmFacebookPanel();
								return;
							}
							else if (autoLoginMatch)
							{
								#region AutoLinkByAutoLoginUsr
								autoLinkByAutoLoginUsr(false);
								#endregion
							}
							else if (FacebookEmailMatchToCurrentUser)
							{
								jQueryUI.FromObject(View.ConnectDialog).Dialog("open");
								ensurePanelGenerated("View.Connect_NewAccount_EmailMatchPanel");
								FacebookEmailMatchNickName = U.get(response, "EmailMatchUsr/NickName").ToString();
								FacebookEmailMatchEmail = U.get(response, "EmailMatchUsr/Email").ToString();
								#region set up email match panel
								if (U.get(response, "EmailMatchUsr/NickName").ToString().Length > 0)
								{
									View.Connect_NewAccount_EmailMatch_UserLink1.InnerHTML = "Link to " + U.get(response, "EmailMatchUsr/Link").ToString() + ":";
								}
								else
								{
									View.Connect_NewAccount_EmailMatch_UserLink1.InnerHTML = "Link to " + FacebookEmailMatchEmail + ":";
								}
								#endregion
								View.Connect_NewAccount_EmailMatch_BackButton.Style.Display = FacebookAccountConfirmationStepDone ? "" : "none";
								changePanel("View.Connect_NewAccount_EmailMatchPanel");
							}
							else
							{
								jQueryUI.FromObject(View.ConnectDialog).Dialog("open");
								ensurePanelGenerated("View.Connect_NewAccount_NoEmailMatchPanel");
								View.Connect_NewAccount_NoEmailMatch_BackButton.Style.Display = FacebookAccountConfirmationStepDone ? "" : "none";
								changePanel("View.Connect_NewAccount_NoEmailMatchPanel");
							}
						}
					}
				)
			);
			#endregion
		}
		#endregion
		
		#region Panels
		#region Connect_LoggedOutPanel ****************************************************************
		#region add_View_Connect_LoggedOutPanel
		Element add_View_Connect_LoggedOutPanel()
		{
			#region LoggedOutPanel html
			string s = @"
<div id=""{ClientID}Connect_LoggedOutPanel"" class=""LoginPanel"" style=""display:none;"">

	<div style=""position:relative;"" class=""LoginPanelInner ClearAfter"">
		<div style=""width:240px; float:left;"" class=""ColumnWithNoParaAbove"">
			<p class=""LoginPanelTitle"">
				Use Facebook
			</p>
			<p>
				The easiest way to log in to Don't Stay In:
			</p>
			<p>
				<button id=""{ClientID}Connect_LoggedOut_ConnectButton"" class=""ui-state-default ui-corner-all Pointer BigButton"">
					<img src=""/gfx/facebook-small-16.png"" width=""16"" height=""16"" border=""0"" align=""top"" />
					<span style=""height:16px;"">Connect with Facebook</span>
				</button>
			</p>
			<ul style=""margin-top:10px;"">
				<li>
					Easy: just three clicks to sign up
				</li>
				<li>
					Simple: use your Facebook password
				</li>
			</ul>
		</div>
		<div style=""left:220px; width:10px; height:173px; float:left; border-left:dotted 2px #cccccc;""> </div>
		<div style=""left:210px; width:220px; float:left;"" class=""ColumnWithNoParaAbove"">
			<p class=""LoginPanelTitle"">
				Connect manually
			</p>
			<p>
				If you don't have Facebook access, you can log in manually:
			</p>
			<p>
				<button id=""{ClientID}Connect_LoggedOut_NoFacebookButton"" class=""ui-state-default ui-corner-all Pointer BigButton"">
					<img src=""/gfx/dsi-tiny-16.png"" width=""16"" height=""16"" border=""0"" align=""top"" />
					<span style=""height:16px;"">Connect manually</span>
				</button>
			</p>
			<ul style=""margin-top:10px;"">
				<li>
					Works even if Facebook is blocked
				</li>
			</ul>
		</div>
	</div>
	<p>
		<button id=""{ClientID}Connect_LoggedOut_CancelButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:right;"">Cancel</button>
	</p>
</div>
";
			addChild(s);
			#endregion
			jQuery.FromElement(View.Connect_LoggedOut_ConnectButton).Click(new jQueryEventHandler(connectButtonClick));
			jQuery.FromElement(View.Connect_LoggedOut_CancelButton).Click(new jQueryEventHandler(cancelButtonClick));
			jQuery.FromElement(View.Connect_LoggedOut_NoFacebookButton).Click(new jQueryEventHandler(noFacebookButtonClick));

			return View.Connect_LoggedOutPanel;
		}
		#endregion
		#region connectButtonClick
		int ConnectButtonClickAsyncOperation;
		void connectButtonClick(jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;

			connectButtonClickInternal();
		}
		#endregion
		#region connectButtonClickInternal
		void connectButtonClickInternal()
		{
			CloseOnLoggedIn = true;


			if (CurrentFacebookConnected)
			{
				#region already logged in
				configureFormConnected();
				#endregion

			}
			else
			{
				changePanel("View.Connect_ConnectingPanel");

				#region login
				ConnectButtonClickAsyncOperation = RegisterStartAsyncGeneric("Connecting...", false, false); // Don't set the timer to show the cencel button.
				FB.login(
					new Response(
						delegate(Dictionary<object, object> loginResponse)
						{
							if (RegisterEndAsync(ConnectButtonClickAsyncOperation))
								return;

							if (CurrentFacebookConnected)
							{
								changePanel("View.Connect_LoadingPanel");
								configureFormConnected();
							}
							else
							{
								showError(6, "Looks like Facebook had trouble getting you connected.");
							}
						}
					),
					//F.d("perms", "email,publish_stream,offline_access,user_birthday,user_hometown,user_location,create_event,rsvp_event,user_events")
					//F.d("perms", "email,publish_stream,offline_access,user_birthday,user_hometown,user_location,rsvp_event")
					F.d("scope", "email,publish_stream,offline_access,user_birthday,user_hometown,user_location")
					//events: create_event,rsvp_event,user_events
					//new: user_address,user_mobile_phone
				);
				//Check out: http://developers.facebook.com/docs/authentication/permissions
				#endregion
			}
		}
		#endregion
		#region noFacebookButtonClick
		void noFacebookButtonClick(jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;

			if (AutoLogin)
			{
				if (AutoLoginUsrIsEnhancedSecurity)
				{
					showNoFacebookLoginPanel("LoggedOutPanel", AutoLoginUsrEmail);
				}
				else if (AutoLoginUsrIsSkeleton)
				{
					showNoFacebookSignUp1Panel("AutoLoginNoFacebookSkeleton");
				}
				else if (AutoLoginUsrIsFacebookNotConfirmed)
				{
					Dictionary<object, object> details = new Dictionary<object, object>();

					#region home
					Dictionary<object, object> home = new Dictionary<object, object>();
					home["GoodMatch"] = AutoLoginUsrHomeGoodMatch;
					if (AutoLoginUsrHomeGoodMatch)
					{
						home["PlaceK"] = AutoLoginUsrHomePlaceK;
						home["PlaceName"] = AutoLoginUsrHomePlaceName;
						home["CountryK"] = AutoLoginUsrHomeCountryK;
						home["CountryName"] = AutoLoginUsrHomeCountryName;
					}
					else
					{
						home["PlaceK"] = 0;
						home["PlaceName"] = "";
						home["CountryName"] = "";

						int countryKFromIp = 224;
						try
						{
							countryKFromIp = int.Parse(getStringFromBasePage("CountryKFromIp"));
						}
						catch { }
						home["CountryK"] = countryKFromIp;
					}
					#endregion
					details["HomePlace"] = home;
					details["FavouriteMusicK"] = AutoLoginUsrFavouriteMusicK;
					details["SendSpottedEmails"] = AutoLoginUsrSendSpottedEmails;
					details["SendEflyers"] = AutoLoginUsrSendEflyers;
					showDetailsPanel("AutoLoginNoFacebookFacebookNotConfirmed", details);
				}
				else if (AutoLoginUsrNeedsCaptcha)
				{
					Dictionary<object, object> details = new Dictionary<object, object>();
					details["CaptchaEncrypted"] = AutoLoginUsrCaptchaEncrypted;
					showCaptchaPanel("AutoLoginNoFacebookNeedsCaptcha", details);
				}
				else
					changePanel("View.Connect_LoggedOut_NoFacebook_ChoosePanel");
			}
			else
			{
				changePanel("View.Connect_LoggedOut_NoFacebook_ChoosePanel");
			}

		}
		#endregion
		#endregion

		#region Connect_LoggedOut_NoFacebook XXXXXXXXXXXXXXXXXXXXXXXXXXXXX
		#region Connect_LoggedOut_NoFacebook_ChoosePanel **********************************************
		#region add_View_Connect_LoggedOut_NoFacebook_ChoosePanel
		Element add_View_Connect_LoggedOut_NoFacebook_ChoosePanel()
		{
			#region LoggedOut_NoFacebook_ChoosePanel html
			string s = @"
<div id=""{ClientID}Connect_LoggedOut_NoFacebook_ChoosePanel"" class=""LoginPanel"" style=""display:none;"">
	<div style=""position:relative;"" class=""LoginPanelInner ClearAfter"">
		<p>
			If you can't use Facebook to log in, you can log in with your Don't Stay In details.
		</p>
		<div style=""width:240px; float:left;"">
			<p class=""LoginPanelTitle"">
				Log in
			</p>
			<p>
				If you already have an account:
			</p>
			<p>
				<button id=""{ClientID}Connect_LoggedOut_NoFacebook_Choose_LoginButton"" class=""ui-state-default ui-corner-all Pointer BigButton"">Log in</button>
			</p>
		</div>
		<div style=""left:220px; width:10px; height:150px; float:left; border-left:dotted 2px #cccccc;""> </div>
		<div style=""left:210px; width:220px; float:left;"">
			<p class=""LoginPanelTitle"">
				Sign up
			</p>
			<p>
				If you've not used Don't Stay In:
			</p>
			<p>
				<button id=""{ClientID}Connect_LoggedOut_NoFacebook_Choose_SignupButton"" class=""ui-state-default ui-corner-all Pointer BigButton"">Sign up</button>
			</p>
		</div>
	</div>
	<p>
		<button id=""{ClientID}Connect_LoggedOut_NoFacebook_Choose_BackButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:left;"">Back</button>

		<button id=""{ClientID}Connect_LoggedOut_NoFacebook_Choose_CancelButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:right;"">Cancel</button>
	</p>
</div>
";
			addChild(s);
			#endregion
			jQuery.FromElement(View.Connect_LoggedOut_NoFacebook_Choose_LoginButton).Click(delegate(jQueryEvent e) { showNoFacebookLoginPanel("ChoosePanel", ""); });
			jQuery.FromElement(View.Connect_LoggedOut_NoFacebook_Choose_SignupButton).Click(delegate(jQueryEvent e) { showNoFacebookSignUp1Panel("NoFacebookNewAccount"); });
			jQuery.FromElement(View.Connect_LoggedOut_NoFacebook_Choose_BackButton).Click(delegate(jQueryEvent e) { changePanelOnClick("View.Connect_LoggedOutPanel", e); });
			jQuery.FromElement(View.Connect_LoggedOut_NoFacebook_Choose_CancelButton).Click(new jQueryEventHandler(cancelButtonClick));
			return View.Connect_LoggedOut_NoFacebook_ChoosePanel;
		}
		#endregion
		#endregion

		#region Connect_LoggedOut_NoFacebook_LoginPanel ***********************************************
		#region add_View_Connect_LoggedOut_NoFacebook_LoginPanel
		Element add_View_Connect_LoggedOut_NoFacebook_LoginPanel()
		{
			#region LoggedOut_NoFacebook_LoginPanel html
			string s = @"
<div id=""{ClientID}Connect_LoggedOut_NoFacebook_LoginPanel"" class=""LoginPanel"" style=""display:none;"">
	<div style=""position:relative;"" class=""LoginPanelInner ClearAfter"">
		<div style=""width:220px; float:left;"" class=""ColumnWithNoParaAbove"">
			<p class=""LoginPanelTitle"">
				Log in
			</p>
			<p style=""margin-bottom:-5px;"">
				Nickname or email:
			</p>
			<p style=""position:relative; height:25px; line-height:25px;"">
				<input id=""{ClientID}Connect_LoggedOut_NoFacebook_Login_UsernameTextbox"" type=""text"" class=""xui-state-default ui-corner-all"" style=""padding-left:5px; height:20px; width:150px;"" />
			</p>
			<p style=""margin-bottom:-5px; margin-top:0px;"">
				Password:
			</p>
			<p style=""position:relative; height:25px; line-height:25px;"">
				<input id=""{ClientID}Connect_LoggedOut_NoFacebook_Login_PasswordTextbox"" type=""password"" class=""xui-state-default ui-corner-all"" style=""padding-left:5px; height:20px; width:150px; border:1px solid #cccccc;"" />
			</p>
			<p>
				<button id=""{ClientID}Connect_LoggedOut_NoFacebook_Login_LoginButton"" style=""float:left;"" class=""ui-state-default ui-corner-all Pointer BigButton"">Log in</button>
				<p id=""{ClientID}Connect_LoggedOut_NoFacebook_Login_Error"" class=""ForegroundAttentionRed BackgroundWhite"" style=""display:none; position:absolute; left:65px; float:left; width:400px; height:50px; font-weight:bold; line-height:15px;"">
					Can't find those details. Remember to enter your password from Don't Stay In, not your Facebook password.
				</p>
			</p>
		</div>
		<div style=""left:220px; width:10px; height:173px; float:left; border-left:dotted 2px #cccccc;""> </div>
		<div style=""left:230px; width:220px; float:left;"" class=""ColumnWithNoParaAbove"">
			<p class=""LoginPanelTitle"">
				No password?
			</p>
			<p>
				If you signed up using Facebook you may not have a password.
			</p>
			<p>
				<button id=""{ClientID}Connect_LoggedOut_NoFacebook_Login_NoPasswordButton"" class=""ui-state-default ui-corner-all Pointer BigButton"">Get a password</button>
			</p>
		</div>
	</div>
	<p>
		<button id=""{ClientID}Connect_LoggedOut_NoFacebook_Login_BackButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:left;"">Back</button>

		<button id=""{ClientID}Connect_LoggedOut_NoFacebook_Login_CancelButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:right;"">Cancel</button>
		<button id=""{ClientID}Connect_LoggedOut_NoFacebook_Login_ForgottonPasswordButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:right;"">Forgot your password?</button>
	</p>
</div>";
			addChild(s);
			#endregion
			jQuery.FromElement(View.Connect_LoggedOut_NoFacebook_Login_LoginButton).Click(new jQueryEventHandler(noFacebookLoginButtonClick));
			jQuery.FromElement(View.Connect_LoggedOut_NoFacebook_Login_BackButton).Click(new jQueryEventHandler(noFacebookLoginBackClick));
			jQuery.FromElement(View.Connect_LoggedOut_NoFacebook_Login_CancelButton).Click(new jQueryEventHandler(cancelButtonClick));
			jQuery.FromElement(View.Connect_LoggedOut_NoFacebook_Login_NoPasswordButton).Click(delegate(jQueryEvent e) { ensurePanelGenerated("View.Connect_LoggedOut_NoFacebook_PasswordResetPanel"); View.Connect_LoggedOut_NoFacebook_PasswordReset_Title.InnerHTML = "No password?"; changePanelOnClick("View.Connect_LoggedOut_NoFacebook_PasswordResetPanel", e); });
			jQuery.FromElement(View.Connect_LoggedOut_NoFacebook_Login_ForgottonPasswordButton).Click(delegate(jQueryEvent e) { ensurePanelGenerated("View.Connect_LoggedOut_NoFacebook_PasswordResetPanel"); View.Connect_LoggedOut_NoFacebook_PasswordReset_Title.InnerHTML = "Forgot your password?"; changePanelOnClick("View.Connect_LoggedOut_NoFacebook_PasswordResetPanel", e); });

			defaultButton(View.Connect_LoggedOut_NoFacebook_Login_UsernameTextbox, View.Connect_LoggedOut_NoFacebook_Login_LoginButton);
			defaultButton(View.Connect_LoggedOut_NoFacebook_Login_PasswordTextbox, View.Connect_LoggedOut_NoFacebook_Login_LoginButton);

			return View.Connect_LoggedOut_NoFacebook_LoginPanel;
		}
		#endregion
		#region noFacebookLoginBackClick
		void noFacebookLoginBackClick(jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;

			if (NoFacebookLoginPanelSource == "ChoosePanel")
				changePanel("View.Connect_LoggedOut_NoFacebook_ChoosePanel");
			else if (NoFacebookLoginPanelSource == "LoggedOutPanel")
				changePanel("View.Connect_LoggedOutPanel");

		}
		#endregion
		#region noFacebookLoginButtonClick
		void noFacebookLoginButtonClick(jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;

			noFacebookLogin("NoFacebookLogin", false, false, false);
		}
		#endregion
		#region noFacebookLogin
		void noFacebookLogin(string source, bool sendDetailsPanelData, bool sendNoFacebookSignupPanelData, bool autoLogin)
		{
			int thisAsyncOperation = RegisterStartAsync("Logging in...");
			Server.NoFacebookLogin(
				autoLogin ? "" : View.Connect_LoggedOut_NoFacebook_Login_UsernameTextbox.Value,
				autoLogin ? "" : View.Connect_LoggedOut_NoFacebook_Login_PasswordTextbox.Value,
				getCaptchaData(),
				sendNoFacebookSignupPanelData ? getNoFacebookSignupPanelData() : null,
				sendDetailsPanelData ? getDetailsPanelData() : null,
				autoLogin,
				(autoLogin ? AutoLoginUsrK : 0),
				(autoLogin ? AutoLoginUsrLoginString : ""),
				new Response(
					delegate(Dictionary<object, object> response)
					{
						if (RegisterEndAsync(thisAsyncOperation))
							return;

						if (U.isTrue(response, "Exception"))
						{
							showError(4, "Internal server error.");
						}
						else
						{
							ensurePanelGenerated("View.Connect_LoggedOut_NoFacebook_LoginPanel");
							View.Connect_LoggedOut_NoFacebook_Login_Error.Style.Display = "none";
							if (U.isTrue(response, "Error"))
							{
								if (U.isTrue(response, "HasNullPassword"))
								{
									changePanel("View.Connect_LoggedOut_NoFacebook_LoginNoPasswordPanel");
								}
								else
								{
									View.Connect_LoggedOut_NoFacebook_Login_Error.Style.Display = "";
								}
							}
							else
							{
								if (U.isTrue(response, "IsSkeleton"))
								{
									showNoFacebookSignUp2Panel("NoFacebookLoginSkeleton", (Dictionary<object, object>)U.get(response, "Details"));
								}
								else if (U.isTrue(response, "NeedsConfirmation"))
								{
									showDetailsPanel("NoFacebookLoginFacebookNotConfirmed", (Dictionary<object, object>)U.get(response, "Details"));
								}
								else if (U.isTrue(response, "NeedsCaptcha"))
								{
									showCaptchaPanel(source, response);
								}
								else
								{
									setAuthCookie((Dictionary<object, object>)U.get(response, "AuthCookie"), (Dictionary<object, object>)U.get(response, "AuthUsr"));
									jQueryUI.FromObject(View.ConnectDialog).Dialog("close");
								}
							}
						}
					}
				)
			);
		}
		#endregion
		#region showNoFacebookLoginPanel
		string NoFacebookLoginPanelSource = "";
		void showNoFacebookLoginPanel(string noFacebookLoginPanelSource, string emailPreset)
		{
			NoFacebookLoginPanelSource = noFacebookLoginPanelSource;
			ensurePanelGenerated("View.Connect_LoggedOut_NoFacebook_LoginPanel");
			View.Connect_LoggedOut_NoFacebook_Login_UsernameTextbox.Value = emailPreset;
			View.Connect_LoggedOut_NoFacebook_Login_PasswordTextbox.Value = "";
			changePanel("View.Connect_LoggedOut_NoFacebook_LoginPanel");
		}
		#endregion
		#endregion

		#region Connect_LoggedOut_NoFacebook_LoginNoPasswordPanel *************************************
		#region add_View_Connect_LoggedOut_NoFacebook_LoginNoPasswordPanel
		Element add_View_Connect_LoggedOut_NoFacebook_LoginNoPasswordPanel()
		{
			#region LoggedOut_NoFacebook_LoginNoPasswordPanel html
			string s = @"
<div id=""{ClientID}Connect_LoggedOut_NoFacebook_LoginNoPasswordPanel"" class=""LoginPanel"" style=""display:none;"">
	<div class=""LoginPanelInner"">
		<p class=""LoginPanelTitle"">
			Oops!
		</p>
		<p>
			Your account doesn't have a password. You probably created it using our Facebook connect feature. We've 
			sent you an email with a password reset link - with this you'll be able to create a password.
		</p>
		<p>
			<button id=""{ClientID}Connect_LoggedOut_NoFacebook_LoginNoPassword_TryAgainButton"" class=""ui-state-default ui-corner-all Pointer BigButton"">Try again</button>
		</p>
	</div>
	<p>
		<button id=""{ClientID}Connect_LoggedOut_NoFacebook_LoginNoPassword_CancelButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:right;"">Cancel</button>
	</p>
</div>
";
			addChild(s);
			#endregion
			jQuery.FromElement(View.Connect_LoggedOut_NoFacebook_LoginNoPassword_CancelButton).Click(new jQueryEventHandler(cancelButtonClick));
			jQuery.FromElement(View.Connect_LoggedOut_NoFacebook_LoginNoPassword_TryAgainButton).Click(delegate(jQueryEvent e) { changePanelOnClick("View.Connect_LoggedOut_NoFacebook_LoginPanel", e); });
			return View.Connect_LoggedOut_NoFacebook_LoginNoPasswordPanel;
		}
		#endregion
		#endregion

		#region Connect_LoggedOut_NoFacebook_PasswordResetPanel ***************************************
		#region add_View_Connect_LoggedOut_NoFacebook_PasswordResetPanel
		Element add_View_Connect_LoggedOut_NoFacebook_PasswordResetPanel()
		{
			#region LoggedOut_NoFacebook_PasswordResetPanel
			string s = @"
<div id=""{ClientID}Connect_LoggedOut_NoFacebook_PasswordResetPanel"" class=""LoginPanel"" style=""display:none;"">
	<div class=""LoginPanelInner"">
		<p id=""{ClientID}Connect_LoggedOut_NoFacebook_PasswordReset_Title"" class=""LoginPanelTitle"">
			Don't have a password?
		</p>
		<p>
			Enter your Don't Stay In username or email below and we'll send you a password reset link by email:
		</p>
		<p style=""position:relative; height:25px; line-height:25px;"">
			Nickname or email:
			<input id=""{ClientID}Connect_LoggedOut_NoFacebook_PasswordReset_UsernameTextbox"" type=""text"" class=""xui-state-default ui-corner-all"" style=""padding-left:5px; height:20px; left:140px; top:0px; position:absolute; width:150px;"" />
		</p>
		<p style=""position:relative; height:25px; line-height:25px;"">
			<button id=""{ClientID}Connect_LoggedOut_NoFacebook_PasswordReset_SendLinkButton"" class=""ui-state-default ui-corner-all Pointer BigButton"" style=""left:140px; top:0px; position:absolute; float:left;"">Send password reset link</button>
		</p>
		<p style=""position:relative;"">
			<span id=""{ClientID}Connect_LoggedOut_NoFacebook_PasswordReset_MessageLabel"" class=""ForegroundAttentionBlue"" style=""left:140px; top:0px; position:absolute; float:left; font-weight:bold;""></span>
			<span id=""{ClientID}Connect_LoggedOut_NoFacebook_PasswordReset_ErrorLabel"" class=""ForegroundAttentionRed"" style=""left:140px; top:0px; position:absolute; float:left; font-weight:bold;""></span>
		</p>
	</div>
	<p>
		<button id=""{ClientID}Connect_LoggedOut_NoFacebook_PasswordReset_BackButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:left;"">Back</button>
		<button id=""{ClientID}Connect_LoggedOut_NoFacebook_PasswordReset_CancelButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:right;"">Cancel</button>
	</p>
</div>
";
			addChild(s);
			#endregion
			jQuery.FromElement(View.Connect_LoggedOut_NoFacebook_PasswordReset_BackButton).Click(delegate(jQueryEvent e) { changePanelOnClick("View.Connect_LoggedOut_NoFacebook_LoginPanel", e); });
			jQuery.FromElement(View.Connect_LoggedOut_NoFacebook_PasswordReset_CancelButton).Click(new jQueryEventHandler(cancelButtonClick));
			jQuery.FromElement(View.Connect_LoggedOut_NoFacebook_PasswordReset_SendLinkButton).Click(new jQueryEventHandler(noFacebookNoPasswordSendLinkClick));

			defaultButton(View.Connect_LoggedOut_NoFacebook_PasswordReset_UsernameTextbox, View.Connect_LoggedOut_NoFacebook_PasswordReset_SendLinkButton);

			return View.Connect_LoggedOut_NoFacebook_PasswordResetPanel;
		}
		#endregion
		#region noFacebookNoPasswordSendLinkClick
		void noFacebookNoPasswordSendLinkClick(jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;

			View.Connect_LoggedOut_NoFacebook_PasswordReset_ErrorLabel.Style.Display = "none";
			View.Connect_LoggedOut_NoFacebook_PasswordReset_MessageLabel.Style.Display = "none";

			if (View.Connect_LoggedOut_NoFacebook_PasswordReset_UsernameTextbox.Value.Trim().Length == 0)
			{
				View.Connect_LoggedOut_NoFacebook_PasswordReset_ErrorLabel.Style.Display = "";
				View.Connect_LoggedOut_NoFacebook_PasswordReset_ErrorLabel.InnerHTML = "Please enter your email or nickname.";
				return;
			}

			#region Send password reset link
			int thisAsyncOperation = RegisterStartAsync("Sending password reset link...");
			Server.SendPassword(
				View.Connect_LoggedOut_NoFacebook_PasswordReset_UsernameTextbox.Value,
				new Response(
					delegate(Dictionary<object, object> response)
					{
						if (RegisterEndAsync(thisAsyncOperation))
							return;

						if (U.isTrue(response, "Exception"))
						{
							showError(12, "Internal server error.");
						}
						else
						{
							if (U.isTrue(response, "Done"))
							{
								View.Connect_LoggedOut_NoFacebook_PasswordReset_MessageLabel.Style.Display = "";
								View.Connect_LoggedOut_NoFacebook_PasswordReset_MessageLabel.InnerHTML = "We have sent you a password reset email.";
							}
							else
							{
								View.Connect_LoggedOut_NoFacebook_PasswordReset_ErrorLabel.Style.Display = "";
								View.Connect_LoggedOut_NoFacebook_PasswordReset_ErrorLabel.InnerHTML = "Can't find that username or email.";
							}
						}
					}
				)
			);
			#endregion

		}
		#endregion
		#endregion

		#region Connect_LoggedOut_NoFacebook_SignUp1Panel *********************************************
		#region add_View_Connect_LoggedOut_NoFacebook_SignUp1Panel
		Element add_View_Connect_LoggedOut_NoFacebook_SignUp1Panel()
		{
			#region LoggedOut_NoFacebook_SignUp1Panel
			string s = @"
<div id=""{ClientID}Connect_LoggedOut_NoFacebook_SignUp1Panel"" class=""LoginPanel"" style=""display:none;"">
	<div class=""LoginPanelInner"">
		<p class=""LoginPanelTitle"">
			Enter your details...
		</p>
		<p id=""{ClientID}Connect_LoggedOut_NoFacebook_SignUp1_EmailPara"" style=""position:relative; height:25px; line-height:25px;"">
			Email:
			<input id=""{ClientID}Connect_LoggedOut_NoFacebook_SignUp1_EmailTextbox"" type=""text"" style=""padding-left:5px; height:20px; left:140px; top:0px; position:absolute; width:210px; height:25px; line-height:25px;"" />
		</p>
		<p style=""position:relative; height:25px; line-height:25px;"">
			Choose a password:
			<input id=""{ClientID}Connect_LoggedOut_NoFacebook_SignUp1_Password1Textbox"" type=""password"" style=""padding-left:5px; height:20px; left:140px; top:0px; position:absolute; width:210px; height:25px; line-height:25px; border:1px solid #cccccc;"" />
		</p>
		<p style=""position:relative; height:25px; line-height:25px;"">
			Confirm your password:
			<input id=""{ClientID}Connect_LoggedOut_NoFacebook_SignUp1_Password2Textbox"" type=""password"" style=""padding-left:5px; height:20px; left:140px; top:0px; position:absolute; width:210px; height:25px; line-height:25px; border:1px solid #cccccc;"" />
		</p>
		<p style=""position:relative; height:25px; line-height:25px;"">
			<button id=""{ClientID}Connect_LoggedOut_NoFacebook_SignUp1_SaveButton"" class=""ui-state-default ui-corner-all Pointer BigButton"" style=""left:140px; top:0px; position:absolute; width:50px; "">Save</button>
			<span id=""{ClientID}Connect_LoggedOut_NoFacebook_SignUp1_ErrorLabel"" class=""ForegroundAttentionRed"" style=""left:200px; position:absolute; font-weight:bold; top:7px;""></span>
		</p>
	</div>
	<p style=""position:relative;"">
		<button id=""{ClientID}Connect_LoggedOut_NoFacebook_SignUp1_BackButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:left; position:absolute; left:0px;"">Back</button>

		<button id=""{ClientID}Connect_LoggedOut_NoFacebook_SignUp1_CancelButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:right;"">Cancel</button>
	</p>
</div>
";
			addChild(s);
			#endregion

			jQuery.FromElement(View.Connect_LoggedOut_NoFacebook_SignUp1_CancelButton).Click(new jQueryEventHandler(cancelButtonClick));
			jQuery.FromElement(View.Connect_LoggedOut_NoFacebook_SignUp1_BackButton).Click(delegate(jQueryEvent e) { changePanelOnClick("View.Connect_LoggedOut_NoFacebook_ChoosePanel", e); });
			jQuery.FromElement(View.Connect_LoggedOut_NoFacebook_SignUp1_SaveButton).Click(new jQueryEventHandler(noFacebookSignup1SaveClick));

			defaultButton(View.Connect_LoggedOut_NoFacebook_SignUp1_EmailTextbox, View.Connect_LoggedOut_NoFacebook_SignUp1_SaveButton);
			defaultButton(View.Connect_LoggedOut_NoFacebook_SignUp1_Password1Textbox, View.Connect_LoggedOut_NoFacebook_SignUp1_SaveButton);
			defaultButton(View.Connect_LoggedOut_NoFacebook_SignUp1_Password2Textbox, View.Connect_LoggedOut_NoFacebook_SignUp1_SaveButton);

			return View.Connect_LoggedOut_NoFacebook_SignUp1Panel;
		}
		#endregion
		#region noFacebookSignup1SaveClick
		void noFacebookSignup1SaveClick(jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;

			View.Connect_LoggedOut_NoFacebook_SignUp1_ErrorLabel.InnerHTML = "";

			#region Email
			if (NoFacebookSignUp1PanelSource == "NoFacebookNewAccount")
			{
				if (View.Connect_LoggedOut_NoFacebook_SignUp1_EmailTextbox.Value.Length == 0)
				{
					View.Connect_LoggedOut_NoFacebook_SignUp1_ErrorLabel.InnerHTML = "Enter an email";
					return;
				}

				RegularExpression emailRegex = new RegularExpression(@"^[^\@\s]+\@[A-Za-z0-9\-]{1}[.A-Za-z0-9\-]+\.[.A-Za-z0-9\-]*[A-Za-z0-9]$", "g");
				if (!emailRegex.Test(View.Connect_LoggedOut_NoFacebook_SignUp1_EmailTextbox.Value))
				{
					View.Connect_LoggedOut_NoFacebook_SignUp1_ErrorLabel.InnerHTML = "Check your email";
					return;
				}
			}
			#endregion

			#region Password
			if (View.Connect_LoggedOut_NoFacebook_SignUp1_Password1Textbox.Value.Length == 0)
			{
				View.Connect_LoggedOut_NoFacebook_SignUp1_ErrorLabel.InnerHTML = "Enter a password";
				return;
			}

			if (View.Connect_LoggedOut_NoFacebook_SignUp1_Password1Textbox.Value.Length < 4)
			{
				View.Connect_LoggedOut_NoFacebook_SignUp1_ErrorLabel.InnerHTML = "Password is too short";
				return;
			}

			if (View.Connect_LoggedOut_NoFacebook_SignUp1_Password1Textbox.Value != View.Connect_LoggedOut_NoFacebook_SignUp1_Password2Textbox.Value)
			{
				View.Connect_LoggedOut_NoFacebook_SignUp1_ErrorLabel.InnerHTML = "Passwords don't match";
				return;
			}
			#endregion

			
			if (NoFacebookSignUp1PanelSource == "NoFacebookNewAccount")
			{
				#region Check email for duplicates
				int thisAsyncOperation = RegisterStartAsync("Checking email...");
				Server.CheckEmail(
					View.Connect_LoggedOut_NoFacebook_SignUp1_EmailTextbox.Value,
					new Response(
						delegate(Dictionary<object, object> response)
						{
							if (RegisterEndAsync(thisAsyncOperation))
								return;

							if (U.isTrue(response, "Exception"))
							{
								showError(7, "Internal server error");
							}
							else
							{
								bool emailFound = (bool)U.get(response, "Found");
								if (emailFound)
								{
									#region Send password
									int thisAsyncOperationSendPassword = RegisterStartAsync("Checking email...");
									Server.SendPassword(
										View.Connect_LoggedOut_NoFacebook_SignUp1_EmailTextbox.Value,
										new Response(
											delegate(Dictionary<object, object> responseSendPassword)
											{
												if (RegisterEndAsync(thisAsyncOperationSendPassword))
													return;

												if (U.isTrue(responseSendPassword, "Exception"))
												{
													showError(12, "Internal server error.");
												}
												else
												{
													if (U.isTrue(responseSendPassword, "Done"))
													{
														View.Connect_LoggedOut_NoFacebook_SignUp1_ErrorLabel.InnerHTML = "This email is already in our database. We've sent you a password reset email.";
													}
													else
													{
														View.Connect_LoggedOut_NoFacebook_SignUp1_ErrorLabel.InnerHTML = "This email is already in our database.";
													}
												}
											}
										)
									);
									#endregion
								}
								else
								{
									showNoFacebookSignUp2Panel("NoFacebookNewAccount", null);
								}
							}
						}
					)
				);
				#endregion
			}
			else if (NoFacebookSignUp1PanelSource == "AutoLoginNoFacebookSkeleton")
			{
				Dictionary<object, object> details = new Dictionary<object, object>();
				details["UsrK"] = AutoLoginUsrK;
				details["Nickname"] = AutoLoginNickname;
				showNoFacebookSignUp2Panel("AutoLoginNoFacebookSkeleton", details);
			}

		}
		#endregion
		#region noFacebookSignup1BackClick
		void noFacebookSignup1BackClick(jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;

			if (NoFacebookSignUp1PanelSource == "NoFacebookNewAccount")
				changePanel("View.Connect_LoggedOut_NoFacebook_ChoosePanel");
			else if (NoFacebookSignUp1PanelSource == "AutoLoginNoFacebookSkeleton")
				changePanel("View.Connect_LoggedOutPanel");

		}
		#endregion
		#region showNoFacebookSignUp1Panel
		void showNoFacebookSignUp1Panel(string source)//, string email)
		{
			NoFacebookSignUp1PanelSource = source;
			ensurePanelGenerated("View.Connect_LoggedOut_NoFacebook_SignUp1Panel");

			View.Connect_LoggedOut_NoFacebook_SignUp1_EmailPara.Style.Display = "";

			if (NoFacebookSignUp1PanelSource == "AutoLoginNoFacebookSkeleton")
				View.Connect_LoggedOut_NoFacebook_SignUp1_EmailPara.Style.Display = "none";

			changePanel("View.Connect_LoggedOut_NoFacebook_SignUp1Panel");
		}
		#endregion
		#endregion

		#region Connect_LoggedOut_NoFacebook_SignUp2Panel *********************************************
		#region add_View_Connect_LoggedOut_NoFacebook_SignUp2Panel
		Element add_View_Connect_LoggedOut_NoFacebook_SignUp2Panel()
		{
			#region LoggedOut_NoFacebook_SignUp2Panel
			string s = @"
<div id=""{ClientID}Connect_LoggedOut_NoFacebook_SignUp2Panel"" class=""LoginPanel"" style=""display:none;"">
	<div class=""LoginPanelInner"">
		<p class=""LoginPanelTitle"">
			Enter your details...
		</p>
		<p style=""position:relative; height:25px; line-height:25px;"">
			Real name:
			<input id=""{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextbox"" type=""text"" style=""padding-left:5px; height:20px; left:140px; top:0px; position:absolute; width:100px; height:25px; line-height:25px;"" />
			<input id=""{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_LastNameTextbox"" type=""text"" style=""padding-left:5px; height:20px; left:250px; top:0px; position:absolute; width:100px; height:25px; line-height:25px;"" />
		</p>
		<p style=""position:relative; height:25px; line-height:25px;"">
			Nickname:
			<input id=""{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox"" type=""text"" style=""padding-left:5px; height:20px; left:140px; top:0px; position:absolute; width:210px; height:25px; line-height:25px;"" />
		</p>
		<p style=""position:relative; height:25px; line-height:25px;"">
			Date of birth:
			<span style=""left:140px; top:0px; position:absolute;"">
				<select id=""{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDown"" class=""xui-state-default ui-corner-all"" style=""padding-left:5px; height:25px; line-height:25px;""></select>
				<select id=""{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDown"" class=""xui-state-default ui-corner-all"" style=""padding-left:5px; height:25px; line-height:25px;""></select>
				<select id=""{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDown"" class=""xui-state-default ui-corner-all"" style=""padding-left:5px; height:25px; line-height:25px;""></select>
			</span>
		</p>
		<p style=""position:relative; height:25px; line-height:25px;"">
			Sex:
			<span style=""left:140px; top:0px; position:absolute;"">
				<input id=""{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_SexMaleRadio"" type=""radio"" name=""{ClientID}Connect_LoggedOut_NoFacebook_SignUp1_Sex"" />
				<label for=""{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_SexMaleRadio""> Male</label>
				<input id=""{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_SexFemaleRadio"" type=""radio"" name=""{ClientID}Connect_LoggedOut_NoFacebook_SignUp1_Sex"" />
				<label for=""{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_SexFemaleRadio""> Female</label>
			</span>
		</p>
		<p style=""position:relative; height:25px; line-height:25px;"">
			<button id=""{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_SaveButton"" class=""ui-state-default ui-corner-all Pointer BigButton"" style=""left:140px; top:0px; position:absolute; width:50px;"">Save</button>
			<span id=""{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel"" class=""ForegroundAttentionRed"" style=""left:200px; position:absolute; font-weight:bold; top:7px;""></span>
		</p>
	</div>
	<p style=""position:relative;"">
		<button id=""{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_BackButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:left; position:absolute; left:0px;"">Back</button>

		<button id=""{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_CancelButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:right;"">Cancel</button>
	</p>
</div>
";
			addChild(s);
			#endregion

			jQuery.FromElement(View.Connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextbox).Keyup(new jQueryEventHandler(noFacebookSignup2NameKeyUp));
			jQuery.FromElement(View.Connect_LoggedOut_NoFacebook_SignUp2_LastNameTextbox).Keyup(new jQueryEventHandler(noFacebookSignup2NameKeyUp));
			jQuery.FromElement(View.Connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox).Keyup(new jQueryEventHandler(noFacebookSignup2NicknameKeyUp));
			jQuery.FromElement(View.Connect_LoggedOut_NoFacebook_SignUp2_CancelButton).Click(new jQueryEventHandler(cancelButtonClick));
			jQuery.FromElement(View.Connect_LoggedOut_NoFacebook_SignUp2_BackButton).Click(new jQueryEventHandler(noFacebookSignup2BackClick));
			jQuery.FromElement(View.Connect_LoggedOut_NoFacebook_SignUp2_SaveButton).Click(new jQueryEventHandler(noFacebookSignup2SaveClick));

			#region add date dropdown options
			if (View.Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDown.Options.Length == 0)
			{
				addOption("-1", "", View.Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDown);
				for (int i = 1; i <= 31; i++)
					addOption(i.ToString(), i.ToString(), View.Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDown);
			}

			if (View.Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDown.Options.Length == 0)
			{
				addOption("-1", "", View.Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDown);
				string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
				for (int i = 0; i < months.Length; i++)
					addOption(i.ToString(), months[i], View.Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDown);
			}

			if (View.Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDown.Options.Length == 0)
			{
				addOption("-1", "", View.Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDown);
				Date t = new Date();
				int year = t.GetFullYear();
				for (int i = year; i > year - 100; i--)
					addOption(i.ToString(), i.ToString(), View.Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDown);
			}
			#endregion

			defaultButton(View.Connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextbox, View.Connect_LoggedOut_NoFacebook_SignUp2_SaveButton);
			defaultButton(View.Connect_LoggedOut_NoFacebook_SignUp2_LastNameTextbox, View.Connect_LoggedOut_NoFacebook_SignUp2_SaveButton);
			defaultButton(View.Connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox, View.Connect_LoggedOut_NoFacebook_SignUp2_SaveButton);

			return View.Connect_LoggedOut_NoFacebook_SignUp2Panel;
		}
		#endregion
		#region noFacebookSignup2BackClick
		void noFacebookSignup2BackClick(jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;

			if (NoFacebookSignUp2PanelSource == "NoFacebookNewAccount")
				changePanel("View.Connect_LoggedOut_NoFacebook_SignUp1Panel");
			else if (NoFacebookSignUp2PanelSource == "NoFacebookLoginSkeleton")
				changePanel("View.Connect_LoggedOut_NoFacebook_LoginPanel");
			else if (NoFacebookSignUp2PanelSource == "AutoLoginNoFacebookSkeleton")
				changePanel("View.Connect_LoggedOut_NoFacebook_SignUp1Panel");
			

		}
		#endregion
		#region noFacebookSignup2NameKeyUp
		string previousNicknameTest = "";
		void noFacebookSignup2NameKeyUp(jQueryEvent e)
		{
			if (
				(!noFacebookSignup2NicknameHasEntry || View.Connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox.Value == "")
				&& View.Connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextbox.Value.Length > 0
				&& View.Connect_LoggedOut_NoFacebook_SignUp2_LastNameTextbox.Value.Length > 0
				)
			{
				string nickTest = View.Connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextbox.Value + "-" + View.Connect_LoggedOut_NoFacebook_SignUp2_LastNameTextbox.Value;

				if (previousNicknameTest != nickTest)
				{
					previousNicknameTest = nickTest;
					int thisAsyncOperation = RegisterStartAsync("Suggesting nickname...");
					Server.GetUniqueNickName(
						nickTest,
						NoFacebookSignUp2PanelLoginUsrK,
						new Response(
							delegate(Dictionary<object, object> response)
							{
								if (RegisterEndAsync(thisAsyncOperation))
									return;

								if (U.isTrue(response, "Exception"))
								{
									//showError(7, "Internal server error");
								}
								else
								{
									string newNickname = U.get(response, "Nickname").ToString();
									View.Connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox.Value = newNickname;
								}
							}
						)
					);
				}

			}
		}
		#endregion
		#region noFacebookSignup2NicknameKeyUp
		bool noFacebookSignup2NicknameHasEntry = false;
		void noFacebookSignup2NicknameKeyUp(jQueryEvent e)
		{
			noFacebookSignup2NicknameHasEntry = true;
		}
		#endregion
		#region noFacebookSignup2SaveClick
		void noFacebookSignup2SaveClick(jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;

			View.Connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel.InnerHTML = "";

			#region Check name
			if (View.Connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextbox.Value.Length == 0)
			{
				View.Connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel.InnerHTML = "Enter your first name";
				return;
			}

			if (View.Connect_LoggedOut_NoFacebook_SignUp2_LastNameTextbox.Value.Length == 0)
			{
				View.Connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel.InnerHTML = "Enter your last name";
				return;
			}

			if (View.Connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextbox.Value.Length > 20)
			{
				View.Connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel.InnerHTML = "First name is too long";
				return;
			}

			if (View.Connect_LoggedOut_NoFacebook_SignUp2_LastNameTextbox.Value.Length > 20)
			{
				View.Connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel.InnerHTML = "Last name is too long";
				return;
			}
			#endregion

			#region Check nickname
			if (View.Connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox.Value.Length == 0)
			{
				View.Connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel.InnerHTML = "Enter a nickname";
				return;
			}

			if (View.Connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox.Value.Length < 2)
			{
				View.Connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel.InnerHTML = "Nickname must be longer";
				return;
			}

			if (View.Connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox.Value.Length > 20)
			{
				View.Connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel.InnerHTML = "Nickname is too long";
				return;
			}
			#endregion

			#region Check date of birth
			if (int.Parse(View.Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDown.Value) < 0 ||
				int.Parse(View.Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDown.Value) < 0 ||
				int.Parse(View.Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDown.Value) < 0)
			{
				View.Connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel.InnerHTML = "Enter your date of birth";
				return;
			}

			Date d = new Date();
			d.SetFullYear(int.Parse(View.Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDown.Value));
			d.SetMonth(int.Parse(View.Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDown.Value));
			d.SetDate(int.Parse(View.Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDown.Value));

			if (d.GetFullYear() != int.Parse(View.Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDown.Value) ||
				d.GetMonth() != int.Parse(View.Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDown.Value) ||
				d.GetDate() != int.Parse(View.Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDown.Value))
			{
				View.Connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel.InnerHTML = "Check your date of birth";
				return;
			}
			#endregion

			#region Check sex
			if (!View.Connect_LoggedOut_NoFacebook_SignUp2_SexMaleRadio.Checked && !View.Connect_LoggedOut_NoFacebook_SignUp2_SexFemaleRadio.Checked)
			{
				View.Connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel.InnerHTML = "Enter your sex";
				return;
			}
			#endregion

			#region Check nickname
			int thisAsyncOperation = RegisterStartAsync("Checking nickname...");
			Server.GetUniqueNickName(
				View.Connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox.Value,
				NoFacebookSignUp2PanelLoginUsrK,
				new Response(
					delegate(Dictionary<object, object> response)
					{
						if (RegisterEndAsync(thisAsyncOperation))
							return;

						if (U.isTrue(response, "Exception"))
						{
							showError(7, "Internal server error");
						}
						else
						{
							string newNickname = U.get(response, "Nickname").ToString();
							if (View.Connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox.Value != newNickname)
							{
								View.Connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox.Value = newNickname;
								View.Connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel.InnerHTML = "We changed your nickname. Is this OK?";
								return;
							}
							else
							{
								noFacebookSignup2SaveDone();
							}
						}
					}
				)
			);
			#endregion
			
		}
		#endregion
		#region noFacebookSignup2SaveDone
		void noFacebookSignup2SaveDone()
		{
			showDetailsPanel(NoFacebookSignUp2PanelSource, null);
		}
		#endregion
		#region getNoFacebookSignupPanelData
		Dictionary<object, object> getNoFacebookSignupPanelData()
		{
			Dictionary<object, object> ret = new Dictionary<object, object>();

			ret["Email"] = View.Connect_LoggedOut_NoFacebook_SignUp1_EmailTextbox.Value;
			ret["Password"] = View.Connect_LoggedOut_NoFacebook_SignUp1_Password1Textbox.Value;
			ret["FirstName"] = View.Connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextbox.Value;
			ret["LastName"] = View.Connect_LoggedOut_NoFacebook_SignUp2_LastNameTextbox.Value;
			ret["Nickname"] = View.Connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox.Value;
			ret["DateOfBirthYear"] = int.Parse(View.Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDown.Value);
			ret["DateOfBirthMonth"] = int.Parse(View.Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDown.Value);
			ret["DateOfBirthDay"] = int.Parse(View.Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDown.Value);
			ret["SexMale"] = View.Connect_LoggedOut_NoFacebook_SignUp2_SexMaleRadio.Checked;

			return ret;
		}
		#endregion
		#region noFacebookNewAccount
		void noFacebookNewAccount()
		{
			int thisAsyncOperation = RegisterStartAsync("Creating account...");
			Server.NoFacebookNewAccount(
				getNoFacebookSignupPanelData(),
				getDetailsPanelData(),
				getCaptchaData(),
				new Response(
					delegate(Dictionary<object, object> response)
					{
						if (RegisterEndAsync(thisAsyncOperation))
							return;

						if (U.isTrue(response, "Exception"))
						{
							showError(8, "Internal server error (" + U.get(response, "Message").ToString() + ").");
						}
						else
						{
							if (U.isTrue(response, "NeedsCaptcha"))
							{
								showCaptchaPanel("NoFacebookNewAccount", response);
							}
							else
							{
								setAuthCookie((Dictionary<object, object>)U.get(response, "AuthCookie"), (Dictionary<object, object>)U.get(response, "AuthUsr"));
								showLikeButtonPanel();
							}
						}
					}
				)
			);
		}
		#endregion
		#region showNoFacebookSignUp2Panel
		void showNoFacebookSignUp2Panel(string source, Dictionary<object, object> details)
		{
			NoFacebookSignUp2PanelSource = source;
			ensurePanelGenerated("View.Connect_LoggedOut_NoFacebook_SignUp2Panel");
			NoFacebookSignUp2PanelLoginUsrK = 0;
			if (details != null)
			{
				NoFacebookSignUp2PanelLoginUsrK = (int)U.get(details, "UsrK");
				string nickname = U.get(details, "Nickname").ToString();
				if (nickname.Length > 0)
				{
					noFacebookSignup2NicknameHasEntry = true;
					View.Connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox.Value = nickname;
				}
			}
			changePanel("View.Connect_LoggedOut_NoFacebook_SignUp2Panel");
		}
		#endregion
		#endregion
		#endregion

		#region Connect_ConnectingPanel ***************************************************************
		Element add_View_Connect_ConnectingPanel()
		{
			#region ConnectingPanel html
			string s = @"
<div id=""{ClientID}Connect_ConnectingPanel"" class=""LoginPanel"" style=""display:none;"">
	<div class=""LoginPanelInner"">
		<p class=""LoginPanelTitle"">
			Connecting...
		</p>
		<p>
			You should see a Facebook pop-up. Please log in using your Facebook account. 
		</p>
		<p>
			If you don't see the pop-up, click the button below:
		</p>
		<p>
			<button id=""{ClientID}Connect_Connecting_PopupRetry"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:left;"">Re-open popup</button>
		</p>
	</div>
	<p>
		<button id=""{ClientID}Connect_Connecting_BackButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:left;"">Back</button>
		<button id=""{ClientID}Connect_Connecting_CancelButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:right;"">Cancel</button>
	</p>
</div>
";
			addChild(s);
			#endregion
			jQuery.FromElement(View.Connect_Connecting_PopupRetry).Click(new jQueryEventHandler(connectingRetryPopupClick));
			jQuery.FromElement(View.Connect_Connecting_CancelButton).Click(new jQueryEventHandler(connectingCancelButtonClick));
			jQuery.FromElement(View.Connect_Connecting_BackButton).Click(new jQueryEventHandler(connectingBackButtonClick));
			return View.Connect_ConnectingPanel;
		}
		#region connectingCancelButtonClick
		void connectingCancelButtonClick(jQueryEvent e)
		{
			e.PreventDefault();

			CancelledAsyncOperations[ConnectButtonClickAsyncOperation.ToString()] = true;

			AsyncInProgress = false;

			jQueryUI.FromObject(View.ConnectDialog).Dialog("close");

		}
		#endregion
		#region connectingRetryPopupClick
		void connectingRetryPopupClick(jQueryEvent e)
		{
			e.PreventDefault();

			CancelledAsyncOperations[ConnectButtonClickAsyncOperation.ToString()] = true;

			AsyncInProgress = false;

			connectButtonClickInternal();

		}
		#endregion
		#region connectingBackButtonClick
		void connectingBackButtonClick(jQueryEvent e)
		{
			e.PreventDefault();

			CancelledAsyncOperations[ConnectButtonClickAsyncOperation.ToString()] = true;

			AsyncInProgress = false;

			changePanel("View.Connect_LoggedOutPanel");

		}
		#endregion
		#endregion

		#region Connect_LoggedIn_NewAccount XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

		#region Connect_NewAccount_NoEmailMatchPanel **************************************************
		#region add_View_Connect_NewAccount_NoEmailMatchPanel
		Element add_View_Connect_NewAccount_NoEmailMatchPanel()
		{
			#region NewAccount_NoEmailMatchPanel html
			string s = @"
<div id=""{ClientID}Connect_NewAccount_NoEmailMatchPanel"" class=""LoginPanel"" style=""display:none;"">
	<div style=""position:relative;"" class=""LoginPanelInner ClearAfter"">
		<p>
			We've not seen this Facebook account before...
		</p>
		<div style=""width:220px; float:left;"">
			<p class=""LoginPanelTitle"">
				New user
			</p>
			<p>
				If you've never used Don't Stay In before:
			</p>
			<p>
				<button id=""{ClientID}Connect_NewAccount_NoEmailMatch_NewAccountButton"" class=""ui-state-default ui-corner-all Pointer BigButton"">I'm new to Don't Stay In</button>
			</p>
		</div>
		<div style=""left:220px; width:10px; height:150px; float:left; border-left:dotted 2px #cccccc;""> </div>
		<div style=""left:230px; width:220px; float:left;"">
			<p class=""LoginPanelTitle"">
				Existing user
			</p>
			<p>
				If you've logged in to Don't Stay In before:
			</p>
			<p>
				<button id=""{ClientID}Connect_NewAccount_NoEmailMatch_ChooseAccountButton""  class=""ui-state-default ui-corner-all Pointer BigButton"">Link to my Don't Stay In...</button>
			</p>
		</div>
	</div>
	<p>
		<button id=""{ClientID}Connect_NewAccount_NoEmailMatch_BackButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:left;"">Back</button>

		<button id=""{ClientID}Connect_NewAccount_NoEmailMatch_CancelButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:right;"">Cancel</button>
		<button id=""{ClientID}Connect_NewAccount_NoEmailMatch_FacebookLogoutButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:right;"">Log out of Facebook</button>
	</p>
</div>
";
			addChild(s);
			#endregion
			jQuery.FromElement(View.Connect_NewAccount_NoEmailMatch_CancelButton).Click(new jQueryEventHandler(cancelButtonClick));
			jQuery.FromElement(View.Connect_NewAccount_NoEmailMatch_FacebookLogoutButton).Click(new jQueryEventHandler(logoutOfFacebookButtonClick));
			jQuery.FromElement(View.Connect_NewAccount_NoEmailMatch_BackButton).Click(delegate(jQueryEvent e) { changePanelOnClick("View.Connect_NewAccount_ConfirmFacebookPanel", e); });
			jQuery.FromElement(View.Connect_NewAccount_NoEmailMatch_ChooseAccountButton).Click(delegate(jQueryEvent e) { changePanelOnClick("View.Connect_NewAccount_ChooseAccountPanel", e); });
			jQuery.FromElement(View.Connect_NewAccount_NoEmailMatch_NewAccountButton).Click(new jQueryEventHandler(newAccountButtonClick));
			return View.Connect_NewAccount_NoEmailMatchPanel;
		}
		#endregion
		#region newAccountButtonClick
		void newAccountButtonClick(jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;

			if (FacebookEmailMatch)
			{
				autoConnectClickInternal();
			}
			else
			{
				newAccountButtonClickInternal();
			}
		}
		void newAccountButtonClickInternal()
		{
			int thisAsyncOperation = RegisterStartAsync("Creating new account...");
			Server.GetHomePlaceFromFacebook(
				new Response(
					delegate(Dictionary<object, object> response)
					{
						if (RegisterEndAsync(thisAsyncOperation))
							return;

						if (U.isTrue(response, "Exception"))
						{
							showError(7, "Internal server error");
						}
						else
						{
							showDetailsPanel("NewAccount", response);
						}
					}
				)
			);
		}
		#endregion
		#region newAccount
		void newAccount()
		{

			int thisAsyncOperation = RegisterStartAsync("Creating new account...");
			Server.CreateNewAccount(
				getDetailsPanelData(),
				new Response(
					delegate(Dictionary<object, object> response)
					{
						if (RegisterEndAsync(thisAsyncOperation))
							return;

						if (U.isTrue(response, "Exception"))
						{
							showError(7, "Internal server error");
						}
						else
						{
							if (U.isTrue(response, "NeedsConfirmation"))
							{
								showDetailsPanel("NewAccount", (Dictionary<object, object>)U.get(response, "Details"));
							}
							else
							{
								setAuthCookie((Dictionary<object, object>)U.get(response, "AuthCookie"), (Dictionary<object, object>)U.get(response, "AuthUsr"));
								detectAutoLoginProblem(true);
							}
						}
					}
				)
			);
		}
		#endregion
		#endregion

		#region Connect_NewAccount_EmailMatchPanel ****************************************************
		#region add_View_Connect_NewAccount_EmailMatchPanel
		Element add_View_Connect_NewAccount_EmailMatchPanel()
		{
			#region NewAccount_EmailMatchPanel html
			string s = @"
<div id=""{ClientID}Connect_NewAccount_EmailMatchPanel"" class=""LoginPanel"" style=""display:none;"">
	<div style=""position:relative;"" class=""LoginPanelInner ClearAfter"">
		<p>
			There's a Don't Stay In account with your Facebook email...
		</p>
		<div style=""width:220px; float:left;"">
			<p class=""LoginPanelTitle"">
				Link my accounts
			</p>
			<p>
				<span id=""{ClientID}Connect_NewAccount_EmailMatch_UserLink1""></span>
			</p>
			<p>
				<button id=""{ClientID}Connect_NewAccount_EmailMatch_AutoConnectButton"" class=""ui-state-default ui-corner-all Pointer BigButton"">Link to this account</button>
			</p>
		</div>
		<div style=""left:220px; width:10px; height:150px; float:left; border-left:dotted 2px #cccccc;""> </div>
		<div style=""left:230px; width:220px; float:left;"">
			<p class=""LoginPanelTitle"">
				Other options
			</p>
			<p>
				If this isn't the right account:
			</p>
			<p>
				<button id=""{ClientID}Connect_NewAccount_EmailMatch_ChooseAccountButton"" class=""ui-state-default ui-corner-all Pointer BigButton"">Choose another account</button>
			</p>
		</div>
	</div>
	<p>
		<button id=""{ClientID}Connect_NewAccount_EmailMatch_BackButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:left;"">Back</button>

		<button id=""{ClientID}Connect_NewAccount_EmailMatch_CancelButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:right;"">Cancel</button>
		<button id=""{ClientID}Connect_NewAccount_EmailMatch_FacebookLogoutButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:right;"">Log out of Facebook</button>
	</p>
</div>
";
			addChild(s);
			#endregion
			jQuery.FromElement(View.Connect_NewAccount_EmailMatch_CancelButton).Click(new jQueryEventHandler(cancelButtonClick));
			jQuery.FromElement(View.Connect_NewAccount_EmailMatch_FacebookLogoutButton).Click(new jQueryEventHandler(logoutOfFacebookButtonClick));
			jQuery.FromElement(View.Connect_NewAccount_EmailMatch_BackButton).Click(delegate(jQueryEvent e) { changePanelOnClick("View.Connect_NewAccount_ConfirmFacebookPanel", e); });
			jQuery.FromElement(View.Connect_NewAccount_EmailMatch_ChooseAccountButton).Click(delegate(jQueryEvent e) { changePanelOnClick("View.Connect_NewAccount_ChooseAccountPanel", e); });
			jQuery.FromElement(View.Connect_NewAccount_EmailMatch_AutoConnectButton).Click(new jQueryEventHandler(autoConnectClick));
			return View.Connect_NewAccount_EmailMatchPanel;
		}
		#endregion
		#region autoConnectClick
		void autoConnectClick(jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;

			autoConnectClickInternal();
		}
		void autoConnectClickInternal()
		{

			if (FacebookEmailMatchEnhancedSecurity)
			{
				ensurePanelGenerated("View.Connect_NewAccount_ChooseAccountPanel");
				View.Connect_NewAccount_ChooseAccount_UsernameTextbox.Value = FacebookEmailMatchNickName;
				changePanel("View.Connect_NewAccount_ChooseAccountPanel");
			}
			else
				autoConnect(false);
		}
		#endregion
		#region autoConnect
		void autoConnect(bool sendDetailsPanelData)
		{
			int thisAsyncOperation = RegisterStartAsync("Linking accounts...");
			Server.AutoLinkByEmail(
				sendDetailsPanelData ? getDetailsPanelData() : null,
				new Response(
					delegate(Dictionary<object, object> response)
					{
						if (RegisterEndAsync(thisAsyncOperation))
							return;

						if (U.isTrue(response, "Exception"))
						{
							showError(2, "Internal server error");
						}
						else
						{
							if (U.isTrue(response, "FacebookEmailMatch"))
							{
								if (U.isTrue(response, "NeedsConfirmation"))
								{
									showDetailsPanel("AutoConnect", (Dictionary<object, object>)U.get(response, "Details"));
								}
								else
								{
									setAuthCookie((Dictionary<object, object>)U.get(response, "AuthCookie"), (Dictionary<object, object>)U.get(response, "AuthUsr"));
									detectAutoLoginProblem(true);
								}
							}
							else
							{
								showError(3, "Can't find account by email.");
							}
						}
					}
				)
			);
		}
		#endregion
		#endregion

		#region Connect_NewAccount_ChooseAccountPanel *************************************************
		#region add_View_Connect_NewAccount_ChooseAccountPanel
		Element add_View_Connect_NewAccount_ChooseAccountPanel()
		{
			#region NewAccount_ChooseAccountPanel
			string s = @"
<div id=""{ClientID}Connect_NewAccount_ChooseAccountPanel"" class=""LoginPanel"" style=""display:none;"">
	<div class=""LoginPanelInner"">
		<p class=""LoginPanelTitle"">
			Don't Stay In details
		</p>
		<p>
			Enter your Don't Stay In details below to link with your Facebook:
		</p>
		<p style=""position:relative; height:25px; line-height:25px;"">
			Nickname or email:
			<input id=""{ClientID}Connect_NewAccount_ChooseAccount_UsernameTextbox"" type=""text"" class=""xui-state-default ui-corner-all"" style=""padding-left:5px; height:20px; left:140px; top:0px; position:absolute; width:150px;"" />
		</p>
		<p style=""position:relative; height:25px; line-height:25px;"">
			Password:
			<input id=""{ClientID}Connect_NewAccount_ChooseAccount_PasswordTextbox"" type=""password"" class=""xui-state-default ui-corner-all"" style=""padding-left:5px; height:20px; left:140px; top:0px; position:absolute; width:150px; border:1px solid #cccccc;"" />
		</p>
		<p style=""position:relative; height:30px; line-height:30px;"">
			<button id=""{ClientID}Connect_NewAccount_ChooseAccount_LinkAccountButton"" class=""ui-state-default ui-corner-all Pointer BigButton"" style=""left:140px; top:0px; position:absolute; float:left;"">Link my account</button>
		</p>
		<p style=""position:relative;"">
			<span id=""{ClientID}Connect_NewAccount_ChooseAccount_ErrorLabel"" class=""ForegroundAttentionRed"" style=""left:140px; top:0px; position:absolute; float:left; font-weight:bold;""></span>
		</p>
	</div>
	<p>
		<button id=""{ClientID}Connect_NewAccount_ChooseAccount_BackButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:left;"">Back</button>

		<button id=""{ClientID}Connect_NewAccount_ChooseAccount_CancelButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:right;"">Cancel</button>
		<button id=""{ClientID}Connect_NewAccount_ChooseAccount_FacebookLogoutButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:right;"">Log out of Facebook</button>
		<button id=""{ClientID}Connect_NewAccount_ChooseAccount_ForgottonPasswordButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:right;"">Forgot your password?</button>
	</p>
</div>
";
			addChild(s);
			#endregion
			jQuery.FromElement(View.Connect_NewAccount_ChooseAccount_BackButton).Click(new jQueryEventHandler(chooseAccountBackButtonClick));
			jQuery.FromElement(View.Connect_NewAccount_ChooseAccount_CancelButton).Click(new jQueryEventHandler(cancelButtonClick));
			jQuery.FromElement(View.Connect_NewAccount_ChooseAccount_FacebookLogoutButton).Click(new jQueryEventHandler(logoutOfFacebookButtonClick));
			jQuery.FromElement(View.Connect_NewAccount_ChooseAccount_LinkAccountButton).Click(new jQueryEventHandler(linkAccountClick));
			jQuery.FromElement(View.Connect_NewAccount_ChooseAccount_ForgottonPasswordButton).Click(delegate(jQueryEvent e) { changePanelOnClick("View.Connect_NewAccount_ForgotPasswordPanel", e); });

			defaultButton(View.Connect_NewAccount_ChooseAccount_UsernameTextbox, View.Connect_NewAccount_ChooseAccount_LinkAccountButton);
			defaultButton(View.Connect_NewAccount_ChooseAccount_PasswordTextbox, View.Connect_NewAccount_ChooseAccount_LinkAccountButton);

			return View.Connect_NewAccount_ChooseAccountPanel;
		}
		#endregion
		#region linkAccountClick
		void linkAccountClick(jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;

			linkAccount(false);
		}
		#endregion
		#region linkAccount
		void linkAccount(bool sendDetailsPanelData)
		{
			int thisAsyncOperation = RegisterStartAsync("Linking accounts...");
			Server.LinkAccounts(
				View.Connect_NewAccount_ChooseAccount_UsernameTextbox.Value,
				View.Connect_NewAccount_ChooseAccount_PasswordTextbox.Value,
				sendDetailsPanelData ? getDetailsPanelData() : null,
				new Response(
					delegate(Dictionary<object, object> response)
					{
						if (RegisterEndAsync(thisAsyncOperation))
							return;

						if (U.isTrue(response, "Exception"))
						{
							showError(4, "Internal server error.");
						}
						else
						{
							View.Connect_NewAccount_ChooseAccount_ErrorLabel.Style.Display = "none";
							if (U.isTrue(response, "Error"))
							{
								View.Connect_NewAccount_ChooseAccount_ErrorLabel.Style.Display = "";
								View.Connect_NewAccount_ChooseAccount_ErrorLabel.InnerHTML = "Can't find a user with those details.";
							}
							else
							{
								if (U.isTrue(response, "NeedsConfirmation"))
								{
									showDetailsPanel("LinkAccount", (Dictionary<object, object>)U.get(response, "Details"));
								}
								else
								{
									setAuthCookie((Dictionary<object, object>)U.get(response, "AuthCookie"), (Dictionary<object, object>)U.get(response, "AuthUsr"));
									detectAutoLoginProblem(true);
								}
							}
						}
					}
				)
			);
		}
		#endregion
		#region chooseAccountBackButtonClick
		void chooseAccountBackButtonClick(jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;

			changePanel(
				FacebookEmailMatchToCurrentUser ? "View.Connect_NewAccount_EmailMatchPanel"
				: "View.Connect_NewAccount_NoEmailMatchPanel");

		}
		#endregion
		#endregion

		#region Connect_NewAccount_ForgotPasswordPanel ************************************************
		#region add_View_Connect_NewAccount_ForgotPasswordPanel
		Element add_View_Connect_NewAccount_ForgotPasswordPanel()
		{
			#region NewAccount_ForgotPasswordPanel
			string s = @"
<div id=""{ClientID}Connect_NewAccount_ForgotPasswordPanel"" class=""LoginPanel"" style=""display:none;"">
	<div class=""LoginPanelInner"">
		<p class=""LoginPanelTitle"">
			Forgot your password?
		</p>
		<p>
			Enter your Don't Stay In username or email below and we'll send you a password reset link by email:
		</p>
		<p style=""position:relative; height:25px; line-height:25px;"">
			Nickname or email:
			<input id=""{ClientID}Connect_NewAccount_ForgotPassword_UsernameTextbox"" type=""text"" class=""xui-state-default ui-corner-all"" style=""padding-left:5px; height:20px; left:140px; top:0px; position:absolute; width:150px;"" />
		</p>
		<p style=""position:relative; height:30px; line-height:30px;"">
			<button id=""{ClientID}Connect_NewAccount_ForgotPassword_SendLinkButton"" class=""ui-state-default ui-corner-all Pointer BigButton"" style=""left:140px; top:0px; position:absolute; float:left;"">Send password reset link</button>
		</p>
		<p style=""position:relative;"">
			<span id=""{ClientID}Connect_NewAccount_ForgotPassword_MessageLabel"" class=""ForegroundAttentionBlue"" style=""left:140px; top:0px; position:absolute; float:left; font-weight:bold;""></span>
			<span id=""{ClientID}Connect_NewAccount_ForgotPassword_ErrorLabel"" class=""ForegroundAttentionRed"" style=""left:140px; top:0px; position:absolute; float:left; font-weight:bold;""></span>
		</p>
	</div>
	<p>
		<button id=""{ClientID}Connect_NewAccount_ForgotPassword_BackButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:left;"">Back</button>

		<button id=""{ClientID}Connect_NewAccount_ForgotPassword_CancelButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:right;"">Cancel</button>
		<button id=""{ClientID}Connect_NewAccount_ForgotPassword_FacebookLogoutButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:right;"">Log out of Facebook</button>
	</p>
</div>
";
			addChild(s);
			#endregion
			jQuery.FromElement(View.Connect_NewAccount_ForgotPassword_BackButton).Click(delegate(jQueryEvent e) { changePanelOnClick("View.Connect_NewAccount_ChooseAccountPanel", e); });
			jQuery.FromElement(View.Connect_NewAccount_ForgotPassword_CancelButton).Click(new jQueryEventHandler(cancelButtonClick));
			jQuery.FromElement(View.Connect_NewAccount_ForgotPassword_FacebookLogoutButton).Click(new jQueryEventHandler(logoutOfFacebookButtonClick));
			jQuery.FromElement(View.Connect_NewAccount_ForgotPassword_SendLinkButton).Click(new jQueryEventHandler(forgotPasswordSendLinkClick));

			defaultButton(View.Connect_NewAccount_ForgotPassword_UsernameTextbox, View.Connect_NewAccount_ForgotPassword_SendLinkButton);

			return View.Connect_NewAccount_ForgotPasswordPanel;
		}
		#endregion
		#region forgotPasswordSendLinkClick
		void forgotPasswordSendLinkClick(jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;

			View.Connect_NewAccount_ForgotPassword_ErrorLabel.Style.Display = "none";
			View.Connect_NewAccount_ForgotPassword_MessageLabel.Style.Display = "none";

			if (View.Connect_NewAccount_ForgotPassword_UsernameTextbox.Value.Trim().Length == 0)
			{
				View.Connect_NewAccount_ForgotPassword_ErrorLabel.Style.Display = "";
				View.Connect_NewAccount_ForgotPassword_ErrorLabel.InnerHTML = "Please enter your email or nickname.";
				return;
			}

			int thisAsyncOperation = RegisterStartAsync("Sending password reset link...");
			Server.SendPassword(
				View.Connect_NewAccount_ForgotPassword_UsernameTextbox.Value,
				new Response(
					delegate(Dictionary<object, object> response)
					{
						if (RegisterEndAsync(thisAsyncOperation))
							return;

						if (U.isTrue(response, "Exception"))
						{
							showError(12, "Internal server error.");
						}
						else
						{
							if (U.isTrue(response, "Done"))
							{
								View.Connect_NewAccount_ForgotPassword_MessageLabel.Style.Display = "";
								View.Connect_NewAccount_ForgotPassword_MessageLabel.InnerHTML = "We have sent you a password reset email.";
							}
							else
							{
								View.Connect_NewAccount_ForgotPassword_ErrorLabel.Style.Display = "";
								View.Connect_NewAccount_ForgotPassword_ErrorLabel.InnerHTML = "Can't find that username or email.";
							}
						}
					}
				)
			);

		}
		#endregion
		#endregion

		#region Connect_NewAccount_ConfirmFacebookPanel ***********************************************
		#region add_View_Connect_NewAccount_ConfirmFacebookPanel
		Element add_View_Connect_NewAccount_ConfirmFacebookPanel()
		{
			#region NewAccount_ConfirmFacebookPanel html
			string s = @"
<div id=""{ClientID}Connect_NewAccount_ConfirmFacebookPanel"" class=""LoginPanel"" style=""display:none;"">
	<div class=""LoginPanelInner"">
		<p class=""LoginPanelTitle"">
			Is this you?
		</p>
		<p>
			We need to confirm we've got the right Facebook account...
		</p>
		<p>
			<img src="""" width=""50"" height=""50"" border=""0"" id=""{ClientID}Connect_NewAccount_ConfirmFacebook_Image"" align=""absmiddle"" />
			<a href="""" id=""{ClientID}Connect_NewAccount_ConfirmFacebook_Link"" target=""_blank""></a>
		</p>
		<p>
			<button id=""{ClientID}Connect_NewAccount_ConfirmFacebook_YesButton"" class=""ui-state-default ui-corner-all Pointer BigButton"">Yes, this is me</button>
			<button id=""{ClientID}Connect_NewAccount_ConfirmFacebook_NoButton"" class=""ui-state-default ui-corner-all Pointer BigButton"">Nope, not me</button>
		</p>
<p>
</p>
	</div>
	<p>
		<button id=""{ClientID}Connect_NewAccount_ConfirmFacebook_BackButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:left;"">Back</button>
		<button id=""{ClientID}Connect_NewAccount_ConfirmFacebook_CancelButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:right;"">Cancel</button>
	</p>
</div>
";
			addChild(s);
			#endregion
			jQuery.FromElement(View.Connect_NewAccount_ConfirmFacebook_BackButton).Click(delegate(jQueryEvent e) { changePanelOnClick("View.Connect_LoggedOutPanel", e); });
			jQuery.FromElement(View.Connect_NewAccount_ConfirmFacebook_YesButton).Click(new jQueryEventHandler(confirmFacebookAccountYesClick));
			jQuery.FromElement(View.Connect_NewAccount_ConfirmFacebook_NoButton).Click(new jQueryEventHandler(confirmFacebookAccountNoClick));
			jQuery.FromElement(View.Connect_NewAccount_ConfirmFacebook_CancelButton).Click(new jQueryEventHandler(cancelButtonClick));
			return View.Connect_NewAccount_ConfirmFacebookPanel;
		}
		#endregion
		#region showConfirmFacebookPanel
		void showConfirmFacebookPanel()
		{
			int thisAsyncOperation1 = RegisterStartAsync("Loading...");
			FB.api(
				"/me",
				new Response(
					delegate(Dictionary<object, object> meResponse)
					{
						if (RegisterEndAsync(thisAsyncOperation1))
							return;

						jQueryUI.FromObject(View.ConnectDialog).Dialog("open");
						ensurePanelGenerated("View.Connect_NewAccount_ConfirmFacebookPanel");
						View.Connect_NewAccount_ConfirmFacebook_Link.InnerHTML = U.get(meResponse, "name").ToString();
						View.Connect_NewAccount_ConfirmFacebook_Link.Href = U.get(meResponse, "link").ToString();
						View.Connect_NewAccount_ConfirmFacebook_Image.Src = "http://graph.facebook.com/" + U.get(meResponse, "id").ToString() + "/picture";
						changePanel("View.Connect_NewAccount_ConfirmFacebookPanel");
					}
				)
			);
		}
		#endregion
		#region confirmFacebookAccountYesClick
		void confirmFacebookAccountYesClick(jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;

			FacebookAccountNeedsConfirmationBecauseInitiallyFacebookLoggedIn = false;
			FacebookAccountNeedsConfirmationBecauseInitiallyFacebookConnectedAndSiteLoggedOut = false;
			FacebookAccountConfirmationStepDone = true;

			configureFormConnected();
		}
		#endregion
		#region confirmFacebookAccountNoClick
		void confirmFacebookAccountNoClick(jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;

			FacebookAccountConfirmationStepDone = true;

			logoutNow(false, null, true);


		}
		#endregion
		#endregion

		#endregion

		#region Connect_DetailsPanel ******************************************************************
		#region add_View_Connect_DetailsPanel
		Element add_View_Connect_DetailsPanel()
		{
			#region DetailsPanel
			string s = @"
<div id=""{ClientID}Connect_DetailsPanel"" class=""LoginPanel"" style=""display:none;"">
	<div class=""LoginPanelInner"">
		<p class=""LoginPanelTitle"">
			Just to confirm...
		</p>
		<p style=""position:relative; height:25px; line-height:25px;"">
			Favourite music:
			<select id=""{ClientID}Connect_Details_MusicDropDown"" class=""xui-state-default ui-corner-all"" style=""padding-left:5px; height:20px; left:140px; top:0px; position:absolute; width:300px;  height:25px; line-height:25px;"">
				<option value=""1"">I like all music</option>
				<option value=""42"">Commercial (pop, chart dance, club classics etc...)</option>
				<option value=""4"">House (funky house, soulful house, deep house etc...)</option>
				<option value=""10"">Hard Dance (hard house, trance, hardcore etc...)</option>
				<option value=""15"">Alternative Dance (breaks, electro, big beat etc...)</option>
				<option value=""20"">Techno (electro techno, detroit techno etc...)</option>
				<option value=""24"">Drum and Bass (drum'n'bass, jungle etc.)</option>
				<option value=""28"">Urban (hip-hop, R&amp;B, garage etc...)</option>
				<option value=""65"">Alternative Electronic (industrial, ebm, powernoise etc.)</option>
				<option value=""46"">Retro (disco, soul, jazz, funk etc...)</option>
				<option value=""35"">Chillout / Leftfield</option>
				<option value=""36"">Rock (indie, rock, metal etc...)</option>
			</select>
		</p>
		<p style=""position:relative; height:25px; line-height:25px;"">
			Nearest town: 
			<select id=""{ClientID}Connect_Details_CountryDropDown"" class=""ui-corner-all"" style=""padding-left:5px; height:20px; left:140px; top:0px; position:absolute; width:100px; height:25px; line-height:25px; display:none;"">
				<option value=""0"">Countries</option>
			</select>
			<select id=""{ClientID}Connect_Details_PlaceDropDown"" class=""ui-corner-all"" style=""padding-left:5px; height:20px; left:250px; top:0px; position:absolute; width:190px; height:25px; line-height:25px; display:none;"">
				<option value=""0"">Towns</option>
			</select>
			<span id=""{ClientID}Connect_Details_PlaceDefaultOuterSpan"" style=""height:25px; line-height:25px; left:140px; top:0px; position:absolute; width:300px;"">
				<span id=""{ClientID}Connect_Details_PlaceDefaultSpan""></span>
				<a id=""{ClientID}Connect_Details_PlaceChangeLink"" href="""">change</a>
			</span>
		</p>
		<div style=""position:relative;"">
			<p id=""{ClientID}Connect_Details_FacebookInfoPanel"" class=""ui-state-highlight ui-corner-all"" style=""position:absolute; top:-10px; padding:5px; left:300px; width:150px; display:none;"">
				We'll update your Facebook wall when you create stuff.
			</p>
			<p id=""{ClientID}Connect_Details_WeeklyEmailInfoPanel"" class=""ui-state-highlight ui-corner-all"" style=""position:absolute; top:-10px; padding:5px; left:300px; width:150px; display:none;"">
				We'll send you a weekly summary of events in your area playing your favourite music.
			</p>
			<p id=""{ClientID}Connect_Details_PartyInvitesInfoPanel"" class=""ui-state-highlight ui-corner-all"" style=""position:absolute; top:6px; padding:5px; left:300px; width:150px; display:none;"">
				We'll send you guestlist offers, e-flyers and party invites.
			</p>
		</div>
		<p style=""margin-left:140px; margin-top:0px; margin-bottom:2px; "">
			<input id=""{ClientID}Connect_Details_FacebookCheck"" type=""checkbox"" checked=""checked"" />
			<label for=""{ClientID}Connect_Details_FacebookCheck""> Facebook updates</label>
			<a id=""{ClientID}Connect_Details_FacebookInfoAnchor"" href="""">info</a>
		</p>
		<p style=""margin-left:140px; margin-top:0px; margin-bottom:2px;"">
			<input id=""{ClientID}Connect_Details_WeeklyEmailCheck"" type=""checkbox"" checked=""checked"" />
			<label for=""{ClientID}Connect_Details_WeeklyEmailCheck""> Weekly email</label>
			<a id=""{ClientID}Connect_Details_WeeklyEmailInfoAnchor"" href="""">info</a>
		</p>
		<p style=""margin-left:140px; margin-top:0px;"">
			<input id=""{ClientID}Connect_Details_PartyInvitesCheck"" type=""checkbox"" checked=""checked"" />
			<label for=""{ClientID}Connect_Details_PartyInvitesCheck""> Party invites</label>
			<a id=""{ClientID}Connect_Details_PartyInvitesInfoAnchor"" href="""">info</a>
		</p>
		<p style=""margin-left:140px; position:relative;"">
			<button id=""{ClientID}Connect_Details_SaveButton"" class=""ui-state-default ui-corner-all Pointer BigButton"" style=""float:left;"">Save</button>
			<p id=""{ClientID}Connect_Details_PlaceErrorSpan"" class=""ForegroundAttentionRed"" style=""font-weight:bold; display:none; padding-top:7px;"">&nbsp;Please select a town.</p>
		</p>
	</div>
	<p style=""position:relative;"">
		<button id=""{ClientID}Connect_Details_BackButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:left; position:absolute; left:0px;"">Back</button>

		<button id=""{ClientID}Connect_Details_CancelButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:right;"">Cancel</button>
	</p>
</div>
";
			addChild(s);
			#endregion

			jQuery.FromElement(View.Connect_Details_CountryDropDown).Change(new jQueryEventHandler(detailsCountryDropDownChange));
			jQuery.FromElement(View.Connect_Details_PlaceDropDown).Change(new jQueryEventHandler(detailsPlaceDropDownChange));
			jQuery.FromElement(View.Connect_Details_PlaceChangeLink).Click(new jQueryEventHandler(detailsPlaceChangeLinkClick));
			jQuery.FromElement(View.Connect_Details_SaveButton).Click(new jQueryEventHandler(detailsPanelSaveClick));

			jQuery.FromElement(View.Connect_Details_CancelButton).Click(new jQueryEventHandler(cancelButtonClick));
			jQuery.FromElement(View.Connect_Details_BackButton).Click(new jQueryEventHandler(detailsPanelBackClick));

			jQuery.FromElement(View.Connect_Details_FacebookInfoAnchor).Click(new jQueryEventHandler(detailsFacebookInfoAnchorClick));
			jQuery.FromElement(View.Connect_Details_WeeklyEmailInfoAnchor).Click(new jQueryEventHandler(detailsWeeklyEmailInfoAnchorClick));
			jQuery.FromElement(View.Connect_Details_PartyInvitesInfoAnchor).Click(new jQueryEventHandler(detailsPartyInvitesInfoAnchorClick));

			
			DetailsCountryDropDownJ = jQuerySelectBoxes.FromObject(View.Connect_Details_CountryDropDown);
			DetailsPlaceDropDownJ = jQuerySelectBoxes.FromObject(View.Connect_Details_PlaceDropDown);

			return View.Connect_DetailsPanel;
		}
		#endregion
		#region showDetailsPanel
		void showDetailsPanel(string source, Dictionary<object, object> details)
		{
			//debug(source);
			//debug(U.toString(details));
			DetailsShowSource = source;
			ensurePanelGenerated("View.Connect_DetailsPanel");

			Dictionary<object, object> homePlace = null;
			if (details != null)
			{
				homePlace = (Dictionary<object, object>)U.get(details, "HomePlace");
				DetailsDefaultPlaceK = (int)U.get(homePlace, "PlaceK");
				DetailsDefaultCountryK = (int)U.get(homePlace, "CountryK");
				DetailsDefaultPlaceGoodMatch = (bool)U.get(homePlace, "GoodMatch");

				if (U.exists(details, "FavouriteMusicK"))
				{
					View.Connect_Details_MusicDropDown.Value = (string)U.get(details, "FavouriteMusicK");
				}
				if (U.exists(details, "SendSpottedEmails"))
				{
					View.Connect_Details_WeeklyEmailCheck.Checked = U.isTrue(details, "SendSpottedEmails");
				}
				if (U.exists(details, "SendEflyers"))
				{
					View.Connect_Details_PartyInvitesCheck.Checked = U.isTrue(details, "SendEflyers");
				}
			}
			else
			{
				int countryKFromIp = 224;
				try
				{
					countryKFromIp = int.Parse(getStringFromBasePage("CountryKFromIp"));
				}
				catch { }
				DetailsDefaultCountryK = countryKFromIp;
				DetailsDefaultPlaceGoodMatch = false;
			}
			View.Connect_Details_BackButton.Style.Display = source == "AutoLinkByAutoLoginUsr" ? "none" : "";

			View.Connect_Details_PlaceErrorSpan.Style.Display = "none";
			View.Connect_Details_FacebookInfoPanel.Style.Display = "none";
			View.Connect_Details_WeeklyEmailInfoPanel.Style.Display = "none";
			View.Connect_Details_PartyInvitesInfoPanel.Style.Display = "none";

			if (DetailsDefaultPlaceGoodMatch)
			{
				View.Connect_Details_PlaceDefaultOuterSpan.Style.Display = "";
				View.Connect_Details_CountryDropDown.Style.Display = "none";
				View.Connect_Details_PlaceDropDown.Style.Display = "none";
				View.Connect_Details_PlaceDefaultSpan.InnerHTML = (string)U.get(homePlace, "PlaceName") + ", " + (string)U.get(homePlace, "CountryName");

				changePanel("View.Connect_DetailsPanel");
			}
			else
			{
				detailsDropDownsUpdate(true);
			}
		}
		#endregion
		#region place drop downs
		#region detailsCountryDropDownChange
		void detailsCountryDropDownChange(jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;

			int k = getK(View.Connect_Details_CountryDropDown);
			if (k > 0)
			{
				DetailsCountrySelectedK = k;
				DetailsCountryPreviouslySelectedIndex = getIndex(View.Connect_Details_CountryDropDown);
			}
			else
			{
				setIndex(View.Connect_Details_CountryDropDown, DetailsCountryPreviouslySelectedIndex);
			}

			detailsDropDownsUpdate(false);
		}
		#endregion
		#region detailsPlaceDropDownChange
		void detailsPlaceDropDownChange(jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;

			int k = getK(View.Connect_Details_PlaceDropDown);
			if (k > 0)
			{
				DetailsPlaceSelectedK = k;
				DetailsPlacePreviouslySelectedIndex = getIndex(View.Connect_Details_PlaceDropDown);
			}
			else
			{
				setIndex(View.Connect_Details_PlaceDropDown, DetailsPlacePreviouslySelectedIndex);
			}
		}
		#endregion
		#region detailsPlaceChangeLinkClick
		void detailsPlaceChangeLinkClick(jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;

			detailsDropDownsUpdate(true);
		}
		#endregion
		#region detailsDropDownsUpdate
		void detailsDropDownsUpdate(bool resetSelection)
		{
			bool firingAsync = false;
			if (!DetailsCountryDropDownPopulated)
			{
				#region fill country dropdown with data
				firingAsync = true;
				DetailsCountryDropDownJ.AjaxAddOption(
					"/support/getcached.aspx?type=country",
					null,
					false,
					new ActionObject(
						delegate(object args)
						{

							DetailsCountryDropDownPopulated = true;
							setK(View.Connect_Details_CountryDropDown, DetailsDefaultCountryK);
							DetailsCountrySelectedK = DetailsDefaultCountryK;
							DetailsCountryPreviouslySelectedIndex = getIndex(View.Connect_Details_CountryDropDown);

							if (DetailsPlaceDropDownPopulated && DetailsCountrySelectedK == DetailsPlaceDropDownPopulatedCountryK)
							{
								showPlaceDropDownsAndDetailsPanel();
							}
						}
					),
					null
				);
				#endregion
			}
			if (resetSelection || !DetailsPlaceDropDownPopulated || DetailsCountrySelectedK != DetailsPlaceDropDownPopulatedCountryK)
			{
				if (resetSelection)
					setK(View.Connect_Details_CountryDropDown, DetailsDefaultCountryK);

				#region fill place dropdown with data
				firingAsync = true;
				int countryK = resetSelection ? DetailsDefaultCountryK : DetailsCountrySelectedK > 0 ? DetailsCountrySelectedK : DetailsDefaultCountryK;
				int thisAsyncOperation = RegisterStartAsync("Loading towns...");
				DetailsPlaceDropDownJ.AjaxAddOption(
					"/support/getcached.aspx?type=place&countryk=" + countryK + "&return=k",
					null,
					false,
					new ActionObject(
						delegate(object args)
						{
							if (RegisterEndAsync(thisAsyncOperation))
								return;

							DetailsPlaceDropDownPopulated = true;
							DetailsPlaceDropDownPopulatedCountryK = countryK;
							if (countryK == DetailsDefaultCountryK)
							{
								setK(View.Connect_Details_PlaceDropDown, DetailsDefaultPlaceK);
								DetailsPlaceSelectedK = DetailsDefaultPlaceK;
								DetailsPlacePreviouslySelectedIndex = getIndex(View.Connect_Details_PlaceDropDown);
							}
							else
							{
								DetailsPlaceSelectedK = 0;
								DetailsPlacePreviouslySelectedIndex = 0;
							}

							if (DetailsCountryDropDownPopulated)
							{
								showPlaceDropDownsAndDetailsPanel();
							}
						}
					),
					null);
				#endregion
			}
			if (!firingAsync)
			{
				showPlaceDropDownsAndDetailsPanel();
			}
		}
		void showPlaceDropDownsAndDetailsPanel()
		{
			DetailsPlaceDropDownVisible = true;
			View.Connect_Details_PlaceDefaultOuterSpan.Style.Display = "none";
			View.Connect_Details_CountryDropDown.Style.Display = "";
			View.Connect_Details_PlaceDropDown.Style.Display = "";
			changePanel("View.Connect_DetailsPanel");
		}
		#endregion
		#region select box helper functions
		#region getK
		int getK(SelectElement sel)
		{
			try
			{
				string value = ((OptionElement)sel.Options[sel.SelectedIndex]).Value;
				value = value.Substr(5, value.Length - 5);
				return int.Parse(value);
			}
			catch
			{
				return 0;
			}
		}
		#endregion
		#region setK
		bool setK(SelectElement sel, int value)
		{
			for (int i = 0; i < sel.Options.Length; i++)
			{
				OptionElement op = (OptionElement)sel.Options[i];
				if (op.Value.Substr(5, op.Value.Length - 5).ToLowerCase() == value.ToString().ToLowerCase())
				{
					op.Selected = true;
					return true;
				}
			}
			return false;
		}
		#endregion
		#region setIndex
		int getIndex(SelectElement sel)
		{
			return sel.SelectedIndex;
		}
		#endregion
		#region setIndex
		void setIndex(SelectElement sel, int index)
		{
			try
			{
				OptionElement op = index > sel.Options.Length - 1 ? (OptionElement)sel.Options[sel.Options.Length - 1] : (OptionElement)sel.Options[index];
				op.Selected = true;
			}
			catch { }
		}
		#endregion
		#endregion
		#endregion
		#region detailsPanelBackClick
		void detailsPanelBackClick(jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;

			if (DetailsShowSource == "AutoLinkByAutoLoginUsr")
			{
				//back button won't be displayed in this mode
			}
			else if (DetailsShowSource == "AutoConnect")
			{
				changePanel("View.Connect_NewAccount_EmailMatchPanel");
			}
			else if (DetailsShowSource == "LinkAccount")
			{
				changePanel("View.Connect_NewAccount_ChooseAccountPanel");
			}
			else if (DetailsShowSource == "NewAccount")
			{
				changePanel("View.Connect_NewAccount_NoEmailMatchPanel");
			}
			else if (DetailsShowSource == "SwitchAccounts")
			{
				changePanel("View.Connect_AutoLoginMismatchPanel");
			}
			else if (DetailsShowSource == "NoFacebookNewAccount")
			{
				changePanel("View.Connect_LoggedOut_NoFacebook_SignUp2Panel");
			}
			else if (DetailsShowSource == "NoFacebookLoginFacebookNotConfirmed")
			{
				changePanel("View.Connect_LoggedOut_NoFacebook_LoginPanel");
			}
			else if (DetailsShowSource == "NoFacebookLoginSkeleton" || DetailsShowSource == "AutoLoginNoFacebookSkeleton")
			{
				changePanel("View.Connect_LoggedOut_NoFacebook_SignUp2Panel");
			}
			else if (DetailsShowSource == "AutoLoginNoFacebookFacebookNotConfirmed")
			{
				changePanel("View.Connect_LoggedOutPanel");
			}
		}
		#endregion
		#region detailsPanelSaveClick
		void detailsPanelSaveClick(jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;

			View.Connect_Details_PlaceErrorSpan.Style.Display = "none";
			View.Connect_Details_FacebookInfoPanel.Style.Display = "none";
			View.Connect_Details_WeeklyEmailInfoPanel.Style.Display = "none";
			View.Connect_Details_PartyInvitesInfoPanel.Style.Display = "none";

			if (DetailsPlaceDropDownVisible && DetailsPlaceSelectedK == 0)
			{
				View.Connect_Details_PlaceErrorSpan.Style.Display = "";
				return;
			}

			if (DetailsShowSource == "AutoLinkByAutoLoginUsr")
			{
				autoLinkByAutoLoginUsr(true);
			}
			else if (DetailsShowSource == "AutoConnect")
			{
				autoConnect(true);
			}
			else if (DetailsShowSource == "LinkAccount")
			{
				linkAccount(true);
			}
			else if (DetailsShowSource == "NewAccount")
			{
				newAccount();
			}
			else if (DetailsShowSource == "SwitchAccounts")
			{
				switchAccounts(true);
			}
			else if (DetailsShowSource == "NoFacebookNewAccount")
			{
				noFacebookNewAccount();
			}
			else if (DetailsShowSource == "NoFacebookLoginFacebookNotConfirmed")
			{
				noFacebookLogin("NoFacebookLoginFacebookNotConfirmed", true, false, false);
			}
			else if (DetailsShowSource == "NoFacebookLoginSkeleton")
			{
				noFacebookLogin("NoFacebookLoginSkeleton", true, true, false);
			}
			else if (DetailsShowSource == "AutoLoginNoFacebookSkeleton")
			{
				noFacebookLogin("AutoLoginNoFacebookSkeleton", true, true, true);
			}
			else if (DetailsShowSource == "AutoLoginNoFacebookFacebookNotConfirmed")
			{
				noFacebookLogin("AutoLoginNoFacebookFacebookNotConfirmed", true, false, true);
			}
		}
		#endregion
		#region getDetailsPanelData
		Dictionary<object, object> getDetailsPanelData()
		{
			Dictionary<object, object> detailsPanelData = new Dictionary<object, object>();

			detailsPanelData["MusicTypeK"] = int.Parse(((OptionElement)View.Connect_Details_MusicDropDown.Options[View.Connect_Details_MusicDropDown.SelectedIndex]).Value);
			detailsPanelData["PlaceK"] = DetailsPlaceDropDownVisible ? DetailsPlaceSelectedK : DetailsDefaultPlaceK;
			detailsPanelData["Facebook"] = View.Connect_Details_FacebookCheck.Checked;
			detailsPanelData["WeeklyEmail"] = View.Connect_Details_WeeklyEmailCheck.Checked;
			detailsPanelData["PartyInvites"] = View.Connect_Details_PartyInvitesCheck.Checked;

			return detailsPanelData;
		}
		#endregion
		#region detailsInfoAnchors
		void detailsFacebookInfoAnchorClick(jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;

			View.Connect_Details_FacebookInfoPanel.Style.Display = "";
			View.Connect_Details_WeeklyEmailInfoPanel.Style.Display = "none";
			View.Connect_Details_PartyInvitesInfoPanel.Style.Display = "none";
		}
		void detailsWeeklyEmailInfoAnchorClick(jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;

			View.Connect_Details_FacebookInfoPanel.Style.Display = "none";
			View.Connect_Details_WeeklyEmailInfoPanel.Style.Display = "";
			View.Connect_Details_PartyInvitesInfoPanel.Style.Display = "none";
		}
		void detailsPartyInvitesInfoAnchorClick(jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;

			View.Connect_Details_FacebookInfoPanel.Style.Display = "none";
			View.Connect_Details_WeeklyEmailInfoPanel.Style.Display = "none";
			View.Connect_Details_PartyInvitesInfoPanel.Style.Display = "";
		}
		#endregion
		#region autoLinkByAutoLoginUsr
		public void autoLinkByAutoLoginUsr(bool sendDetailsPanelData)
		{
			int thisAsyncOperation1 = RegisterStartAsync("Connecting...");
			Server.AutoLinkByAutoLoginUsr(
				AutoLoginUsrK,
				AutoLoginUsrLoginString,
				sendDetailsPanelData ? getDetailsPanelData() : null,
				new Response(
					delegate(Dictionary<object, object> response)
					{
						if (RegisterEndAsync(thisAsyncOperation1))
							return;

						if (U.isTrue(response, "Exception"))
						{
							showError(9, "Internal server error");
						}
						else
						{
							if (U.isTrue(response, "FacebookAutoLoginUsrMatch"))
							{
								if (U.isTrue(response, "NeedsConfirmation"))
								{
									showDetailsPanel("AutoLinkByAutoLoginUsr", (Dictionary<object, object>)U.get(response, "Details"));
								}
								else
								{
									setAuthCookie((Dictionary<object, object>)U.get(response, "AuthCookie"), (Dictionary<object, object>)U.get(response, "AuthUsr"));
									detectAutoLoginProblem(true);
								}
							}
							else
								showError(10, "Internal server error");
						}
					}
				)
			);
		}
		#endregion
		#endregion

		#region Connect_CaptchaPanel ******************************************************************
		#region add_View_Connect_CaptchaPanel
		Element add_View_Connect_CaptchaPanel()
		{
			#region CaptchaPanel
			string s = @"
<div id=""{ClientID}Connect_CaptchaPanel"" class=""LoginPanel"" style=""display:none;"">
	<div class=""LoginPanelInner"">
		<p class=""LoginPanelTitle"">
			One more thing...
		</p>
		<p>
			To help fight spam, we need you to comfirm you're a nice, fleshy human. You should see five upper-case letters in the box below. Enter them to continue:
		</p>
		<div style=""position:relative;"">
			<p style=""position:absolute;"">
				<img id=""{ClientID}Connect_Captcha_Img"" src="""" width=""150"" height=""50"" style=""text-align:top;"" />
			</p>
			<div style=""margin-left:160px; position:absolute;"">
				<p>
					<input id=""{ClientID}Connect_Captcha_Textbox"" type=""text"" class=""xui-state-default ui-corner-all"" style=""padding-left:5px; height:20px; width:50px;"" />
				</p>
				<p>
					<button id=""{ClientID}Connect_Captcha_SaveButton"" class=""ui-state-default ui-corner-all Pointer BigButton"">Save</button><span id=""{ClientID}Connect_Captcha_Error"" class=""ForegroundAttentionRed"" style=""font-weight:bold; display:none;"">&nbsp;Try again</span>
				</p>
				
			</div>
		</div>
	</div>
	<p style=""position:relative;"">
		<button id=""{ClientID}Connect_Captcha_BackButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:left; position:absolute; left:0px;"">Back</button>

		<button id=""{ClientID}Connect_Captcha_CancelButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:right;"">Cancel</button>
	</p>
</div>
";
			addChild(s);
			#endregion

			jQuery.FromElement(View.Connect_Captcha_SaveButton).Click(new jQueryEventHandler(captchaPanelSaveClick));
			jQuery.FromElement(View.Connect_Captcha_CancelButton).Click(new jQueryEventHandler(cancelButtonClick));
			jQuery.FromElement(View.Connect_Captcha_BackButton).Click(new jQueryEventHandler(captchaPanelBackClick));

			defaultButton(View.Connect_Captcha_Textbox, View.Connect_Captcha_SaveButton);

			return View.Connect_CaptchaPanel;
		}
		#endregion
		#region showCaptchaPanel
		string CaptchaPassthrough = "";
		void showCaptchaPanel(string captchaPanelSource, Dictionary<object, object> details)
		{
			CaptchaPanelSource = captchaPanelSource;
			ensurePanelGenerated("View.Connect_CaptchaPanel");

			View.Connect_Captcha_Error.Style.Display = U.exists(details, "CaptchaFailed") ? "" : "none";

			CaptchaPassthrough = U.get(details, "CaptchaEncrypted").ToString();
			View.Connect_Captcha_Img.Src = "/support/hipimagenew.aspx?a=" + CaptchaPassthrough;
			changePanel("View.Connect_CaptchaPanel");
		}
		#endregion
		#region captchaPanelSaveClick
		void captchaPanelSaveClick(jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;

			View.Connect_Captcha_Error.Style.Display = "none";
			if (View.Connect_Captcha_Textbox.Value.Length != 5)
			{
				View.Connect_Captcha_Error.Style.Display = "";
				return;
			}

			if (CaptchaPanelSource == "NoFacebookNewAccount")
			{
				noFacebookNewAccount();
			}
			else if (CaptchaPanelSource == "NoFacebookLoginSkeleton")
			{
				noFacebookLogin("NoFacebookLoginSkeleton", true, true, false);
			}
			else if (CaptchaPanelSource == "NoFacebookLoginFacebookNotConfirmed")
			{
				noFacebookLogin("NoFacebookLoginFacebookNotConfirmed", true, false, false);
			}
			else if (CaptchaPanelSource == "NoFacebookLogin")
			{
				noFacebookLogin("NoFacebookLogin", false, false, false);
			}
			else if (CaptchaPanelSource == "AutoLoginNoFacebookSkeleton")
			{
				noFacebookLogin("AutoLoginNoFacebookSkeleton", true, true, true);
			}
			else if (CaptchaPanelSource == "AutoLoginNoFacebookFacebookNotConfirmed")
			{
				noFacebookLogin("AutoLoginNoFacebookFacebookNotConfirmed", true, false, true);
			}
			else if (CaptchaPanelSource == "AutoLoginNoFacebookNeedsCaptcha")
			{
				noFacebookLogin("AutoLoginNoFacebookNeedsCaptcha", false, false, true);
			}

		}
		#endregion
		#region captchaPanelBackClick
		void captchaPanelBackClick(jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;


			if (CaptchaPanelSource == "NoFacebookNewAccount")
			{
				changePanel("View.Connect_DetailsPanel");
			}
			else if (CaptchaPanelSource == "NoFacebookLoginSkeleton")
			{
				changePanel("View.Connect_DetailsPanel");
			}
			else if (CaptchaPanelSource == "NoFacebookLoginFacebookNotConfirmed")
			{
				changePanel("View.Connect_DetailsPanel");
			}
			else if (CaptchaPanelSource == "NoFacebookLogin")
			{
				changePanel("View.Connect_LoggedOut_NoFacebook_LoginPanel");
			}
			else if (CaptchaPanelSource == "AutoLoginNoFacebookSkeleton")
			{
				changePanel("View.Connect_DetailsPanel");
			}
			else if (CaptchaPanelSource == "AutoLoginNoFacebookFacebookNotConfirmed")
			{
				changePanel("View.Connect_DetailsPanel");
			}
			else if (CaptchaPanelSource == "AutoLoginNoFacebookNeedsCaptcha")
			{
				changePanel("View.Connect_LoggedOutPanel");
			}


		}
		#endregion
		#region getCaptchaData
		Dictionary<object, object> getCaptchaData()
		{
			ensurePanelGenerated("View.Connect_CaptchaPanel");
			Dictionary<object, object> ret = new Dictionary<object, object>();
			ret["Entered"] = View.Connect_Captcha_Textbox.Value;
			ret["Passthrough"] = CaptchaPassthrough;
			return ret;
		}
		#endregion
		#endregion

		#region Connect_LoggedInPanel *****************************************************************
		#region add_View_Connect_LoggedInPanel
		Element add_View_Connect_LoggedInPanel()
		{
			#region LoggedInPanel
			string s = @"
<div id=""{ClientID}Connect_LoggedInPanel"" class=""LoginPanel"" style=""display:none;"">
	<div class=""LoginPanelInner"">
		<p class=""LoginPanelTitle"">
			Logged in
		</p>
		<p>
			You're logged in<span id=""{ClientID}Connect_LoggedIn_LoggedInUsrLink""></span>.
		</p>
		<p>
			<button id=""{ClientID}Connect_LoggedIn_CloseButton"" class=""ui-state-default ui-corner-all Pointer BigButton"">Close</button>
			<button id=""{ClientID}Connect_LoggedIn_LogoutButton"" class=""ui-state-default ui-corner-all Pointer BigButton"">Log out</button>
		</p>
		<p id=""{ClientID}Connect_LoggedIn_DisconnectLinkOuter"" style=""display:none;"">
			To permanently disconnect your Facebook account, <a id=""{ClientID}Connect_LoggedIn_DisconnectButtonShowLink"" href="""">click here</a>.
		</p>
		<p id=""{ClientID}Connect_LoggedIn_DisconnectButtonOuter"" style=""display:none;"">
			<button id=""{ClientID}Connect_LoggedIn_DisconnectButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:left;"">Disconnect</button>
		</p>
	</div>
	<p>
		<button id=""{ClientID}Connect_LoggedIn_CancelButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:right;"">Cancel</button>
	</p>
</div>
";
			addChild(s);
			#endregion
			jQuery.FromElement(View.Connect_LoggedIn_DisconnectButton).Click(new jQueryEventHandler(disconnectButtonClick));
			jQuery.FromElement(View.Connect_LoggedIn_LogoutButton).Click(new jQueryEventHandler(logoutButtonClick));
			jQuery.FromElement(View.Connect_LoggedIn_CloseButton).Click(new jQueryEventHandler(cancelButtonClick));
			jQuery.FromElement(View.Connect_LoggedIn_CancelButton).Click(new jQueryEventHandler(cancelButtonClick));
			jQuery.FromElement(View.Connect_LoggedIn_DisconnectButtonShowLink).Click(new jQueryEventHandler(disconnectButtonShowClick));
			return View.Connect_LoggedInPanel;
		}
		#endregion
		#region showLoggedInPanel
		void showLoggedInPanel(string link)
		{
			ensurePanelGenerated("View.Connect_LoggedInPanel");

			View.Connect_LoggedIn_DisconnectLinkOuter.Style.Display = CurrentIsLoggedInWithFacebook ? "" : "none";

			if (link == "???")
				View.Connect_LoggedIn_LoggedInUsrLink.InnerHTML = "";
			else if (link.Length > 0)
				View.Connect_LoggedIn_LoggedInUsrLink.InnerHTML = " as " + link;

			changePanel("View.Connect_LoggedInPanel");
		}
		#endregion
		#region disconnectButtonShowClick
		void disconnectButtonShowClick(jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;

			View.Connect_LoggedIn_DisconnectButtonOuter.Style.Display = "";
		}
		#endregion
		#region disconnectButtonClick
		void disconnectButtonClick(jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;

			if (!CurrentFacebookConnected)
			{
				#region not logged in
				removeAuthCookie();
				showError(5, "You tried to disconnect, but you're not connected.");
				#endregion
			}
			else
			{
				#region revoke authorisation
				if (CurrentAuthUsrHasNullPassword)
				{
					ensurePanelGenerated("View.Connect_CreatePasswordPanel");
					View.Connect_CreatePassword_ErrorSpan.InnerHTML = "";
					changePanel("View.Connect_CreatePasswordPanel");
				}
				else
				{
					disconnectInner("");
				}
				#endregion
			}

		}
		#endregion
		#endregion

		#region Connect_CreatePasswordPanel ***********************************************************
		#region add_View_Connect_CreatePasswordPanel
		Element add_View_Connect_CreatePasswordPanel()
		{
			#region CreatePasswordPanel html
			string s = @"
<div id=""{ClientID}Connect_CreatePasswordPanel"" class=""LoginPanel"" style=""display:none;"">
	<div class=""LoginPanelInner"">
		<p class=""LoginPanelTitle"">
			Create a password
		</p>
		<p>
			You'll need this password if you ever want to reconnect your Facebook to this account:
		</p>
		<p style=""position:relative; height:25px; line-height:25px;"">
			Password:
			<input id=""{ClientID}Connect_CreatePassword_Password1Textbox"" type=""password"" class=""xui-state-default ui-corner-all"" style=""padding-left:5px; height:20px; left:140px; top:0px; position:absolute; width:150px; border:1px solid #cccccc;"" />
		</p>
		<p style=""position:relative; height:25px; line-height:25px;"">
			Password (confirm):
			<input id=""{ClientID}Connect_CreatePassword_Password2Textbox"" type=""password"" class=""xui-state-default ui-corner-all"" style=""padding-left:5px; height:20px; left:140px; top:0px; position:absolute; width:150px; border:1px solid #cccccc;"" />
		</p>
		<p style=""position:relative; height:30px; line-height:30px;"">
			<button id=""{ClientID}Connect_CreatePassword_DisconnectButton"" class=""ui-state-default ui-corner-all Pointer BigButton"" style=""left:140px; top:0px; position:absolute;"">Disconnect</button>
		</p>
		<p style=""position:relative;"">
			<span id=""{ClientID}Connect_CreatePassword_ErrorSpan"" class=""ForegroundAttentionRed"" style=""font-weight:bold; left:140px; top:0px; position:absolute;""></span>
		</p>

	</div>
	<p>
		<button id=""{ClientID}Connect_CreatePassword_BackButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:left;"">Back</button>

		<button id=""{ClientID}Connect_CreatePassword_CancelButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:right;"">Cancel</button>
	</p>
</div>";
			addChild(s);
			#endregion
			jQuery.FromElement(View.Connect_CreatePassword_DisconnectButton).Click(new jQueryEventHandler(createPasswordDisconnectButtonClick));
			jQuery.FromElement(View.Connect_CreatePassword_CancelButton).Click(new jQueryEventHandler(cancelButtonClick));
			jQuery.FromElement(View.Connect_CreatePassword_BackButton).Click(new jQueryEventHandler(createPasswordBackButtonClick));

			defaultButton(View.Connect_CreatePassword_Password1Textbox, View.Connect_CreatePassword_DisconnectButton);
			defaultButton(View.Connect_CreatePassword_Password2Textbox, View.Connect_CreatePassword_DisconnectButton);

			return View.Connect_CreatePasswordPanel;
		}
		#endregion
		#region createPasswordBackButtonClick
		void createPasswordBackButtonClick(jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;

			showLoggedInPanel("");
		}
		#endregion
		#region createPasswordDisconnectButtonClick
		void createPasswordDisconnectButtonClick(jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;

			if (View.Connect_CreatePassword_Password1Textbox.Value.Trim() != View.Connect_CreatePassword_Password2Textbox.Value.Trim())
			{
				View.Connect_CreatePassword_ErrorSpan.InnerHTML = "Passwords don't match.";
				return;
			}

			if (View.Connect_CreatePassword_Password1Textbox.Value.Trim().Length < 4)
			{
				View.Connect_CreatePassword_ErrorSpan.InnerHTML = "Please enter at least four characters.";
				return;
			}

			disconnectInner(View.Connect_CreatePassword_Password1Textbox.Value.Trim());

		}
		#endregion
		#region disconnectInner
		void disconnectInner(string password)
		{
			int thisAsyncOperation = RegisterStartAsync("Disconnecting...");
			Server.UnlinkAccount(
				password,
				new Response(
					delegate(Dictionary<object, object> response)
					{
						if (RegisterEndAsync(thisAsyncOperation))
							return;

						if (U.isTrue(response, "Exception"))
						{
							showError(10, "Internal server error");
						}
						else
						{
							if (U.isTrue(response, "DoneUnlink"))
							{
								int thisAsyncOperation1 = RegisterStartAsync("Disconnecting...");
								FB.api(
									F.d("method", "Auth.revokeAuthorization"),
									new Response(
										delegate(Dictionary<object, object> revokeResponse)
										{
											if (RegisterEndAsync(thisAsyncOperation1))
												return;

											removeAuthCookie();
											jQueryUI.FromObject(View.ConnectDialog).Dialog("close");
										}
									)
								);
							}
							else
							{
								showError(11, "Internal server error");
							}
						}
					}
				)
			);
		}
		#endregion
		#endregion

		#region Connect_AutoLoginMismatchPanel ********************************************************
		#region add_View_Connect_AutoLoginMismatchPanel
		Element add_View_Connect_AutoLoginMismatchPanel()
		{
			#region AutoLoginMismatchPanel html
			string s = @"
<div id=""{ClientID}Connect_AutoLoginMismatchPanel"" class=""LoginPanel"" style=""display:none;"">
	<div class=""LoginPanelInner"">
		<p class=""LoginPanelTitle"">
			Might be a problem...
		</p>
		<p>
			<span id=""{ClientID}Connect_AutoLoginMismatch_AutoLoginUsrLink""></span>
		</p>
		<p>
			<button id=""{ClientID}Connect_AutoLoginMismatch_RetryButton"" class=""ui-state-default ui-corner-all Pointer BigButton"">Retry login</button>
			<button id=""{ClientID}Connect_AutoLoginMismatch_ContinueButton"" class=""ui-state-default ui-corner-all Pointer BigButton"">Continue to the page</button>
		</p>
		<p id=""{ClientID}Connect_AutoLoginMismatch_SwitchAccountsPara"">
			To switch your Facebook to <span id=""{ClientID}Connect_AutoLoginMismatch_AutoLoginUsrLink2""></span>, <a id=""{ClientID}Connect_AutoLoginMismatch_SwitchAccountsShowLink"" href="""">click here</a>.
		</p>
		<p id=""{ClientID}Connect_AutoLoginMismatch_SwitchAccountsOuter"" style=""display:none;"">
			<button id=""{ClientID}Connect_AutoLoginMismatch_SwitchButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:left;"">Switch accounts</button>
		</p>
	</div>
	<p>
		<button id=""{ClientID}Connect_AutoLoginMismatch_CancelButton""  class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:right;"">Cancel</button>		
	</p>
</div>";
			addChild(s);
			#endregion
			jQuery.FromElement(View.Connect_AutoLoginMismatch_RetryButton).Click(new jQueryEventHandler(detectAutoLoginRetryClick));
			jQuery.FromElement(View.Connect_AutoLoginMismatch_ContinueButton).Click(new jQueryEventHandler(detectAutoLoginContinueClick));
			jQuery.FromElement(View.Connect_AutoLoginMismatch_CancelButton).Click(new jQueryEventHandler(detectAutoLoginRetryClick));
			jQuery.FromElement(View.Connect_AutoLoginMismatch_SwitchButton).Click(new jQueryEventHandler(detectAutoLoginSwitchClick));
			jQuery.FromElement(View.Connect_AutoLoginMismatch_SwitchAccountsShowLink).Click(new jQueryEventHandler(detectAutoLoginSwitchShowLinkClick));
			return View.Connect_AutoLoginMismatchPanel;
		}
		#endregion
		#region detectAutoLoginProblem
		bool detectAutoLoginProblemNewLink = false;
		void detectAutoLoginProblem(bool newLink)
		{
			detectAutoLoginProblemNewLink = newLink;
			if (AutoLogin && AutoLoginUsrK > 0 && CurrentIsLoggedIn && AutoLoginUsrK.ToString() != CurrentAuthUsrK)
			{
				ensurePanelGenerated("View.Connect_AutoLoginMismatchPanel");

				if (AutoLoginNickname.Length > 0)
					View.Connect_AutoLoginMismatch_AutoLoginUsrLink.InnerHTML = "You logged in as " + CurrentAuthUsrLink + ", but the link you clicked was sent to " + AutoLoginLink + ". This might cause an error if you clicked on a private link.";
				else if (AutoLoginEmail.Length > 0)
					View.Connect_AutoLoginMismatch_AutoLoginUsrLink.InnerHTML = "You logged in as " + CurrentAuthUsrLink + " (" + CurrentAuthUsrEmail + "), but the link you clicked was sent to a different account (" + AutoLoginEmail + "). This might cause an error if you clicked on a private link.";
				else
					View.Connect_AutoLoginMismatch_AutoLoginUsrLink.InnerHTML = "You logged in as " + CurrentAuthUsrLink + ", but the link you clicked was sent to a different account. This might cause an error if you clicked on a private link.";

				View.Connect_AutoLoginMismatch_AutoLoginUsrLink2.InnerHTML = AutoLoginNickname.Length > 0 ? AutoLoginLink : AutoLoginEmail;
				View.Connect_AutoLoginMismatch_SwitchAccountsPara.Style.Display = AutoLoginStringMatch ? "" : "none";

				changePanel("View.Connect_AutoLoginMismatchPanel");

			}
			else
			{
				detectAutoLoginProblemNext();
			}
		}
		#endregion
		#region detectAutoLoginContinueClick
		void detectAutoLoginContinueClick(jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;

			detectAutoLoginProblemNext();
		}
		#endregion
		#region detectAutoLoginRetryClick
		void detectAutoLoginRetryClick(jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;

			logoutNow(false, null, true);
		}
		#endregion
		#region detectAutoLoginSwitchShowLinkClick
		void detectAutoLoginSwitchShowLinkClick(jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;

			View.Connect_AutoLoginMismatch_SwitchAccountsOuter.Style.Display = "";

		}
		#endregion
		#region detectAutoLoginSwitchClick
		void detectAutoLoginSwitchClick(jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;

			switchAccounts(false);

		}
		#endregion
		#region switchAccounts
		void switchAccounts(bool sendDetailsPanelData)
		{
			int thisAsyncOperation = RegisterStartAsync("Switching accounts...");
			Server.SwitchAccounts(
				CurrentFacebookUID,
				AutoLoginUsrK,
				AutoLoginUsrLoginString,
				sendDetailsPanelData ? getDetailsPanelData() : null,
				new Response(
					delegate(Dictionary<object, object> response)
					{
						if (RegisterEndAsync(thisAsyncOperation))
							return;

						if (U.isTrue(response, "Exception"))
						{
							showError(8, "Internal server error.");
						}
						else
						{
							if (U.isTrue(response, "NeedsConfirmation"))
							{
								showDetailsPanel("SwitchAccounts", (Dictionary<object, object>)U.get(response, "Details"));
							}
							else
							{
								setAuthCookie((Dictionary<object, object>)U.get(response, "AuthCookie"), (Dictionary<object, object>)U.get(response, "AuthUsr"));
								showLikeButtonPanel();
							}

						}
					}
				)
			);
		}
		#endregion
		#region detectAutoLoginProblemNext
		void detectAutoLoginProblemNext()
		{
			if (detectAutoLoginProblemNewLink)
				showLikeButtonPanel();
			else
				jQueryUI.FromObject(View.ConnectDialog).Dialog("close");
		}
		#endregion
		#endregion

		#region Connect_LikeButtonPanel ***************************************************************
		#region add_View_Connect_LikeButtonPanel
		Element add_View_Connect_LikeButtonPanel()
		{
			#region LikeButtonPanel html
			string s = @"
<div id=""{ClientID}Connect_LikeButtonPanel"" class=""LoginPanel"" style=""display:none;"">
	<div class=""LoginPanelInner"">
		<p class=""LoginPanelTitle"">
			Click the Like button...
		</p>
		<p>
			... if you'd like to be kept up to date by Facebook:
		</p>
		<p>
			<fb:like href=""http://www.facebook.com/dontstayin"" layout=""box_count"" font=""verdana"" width=""200px""></fb:like>
		</p>
	</div>
	<p>
		<button id=""{ClientID}Connect_LikeButton_CancelButton""  class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:right;"">I'd rather not</button>
	</p>
</div>
";
			addChild(s);
			#endregion
			jQuery.FromElement(View.Connect_LikeButton_CancelButton).Click(new jQueryEventHandler(cancelButtonClick));
			FB.XFBML.parse(View.Connect_LikeButtonPanel);
			return View.Connect_LikeButtonPanel;
		}
		#endregion
		#region showLikeButtonPanel
		void showLikeButtonPanel()
		{
			int thisAsyncOperation = RegisterStartAsync("Loading...");
			FB.api(
				F.d("method", "fql.query", "query", "SELECT type, created_time FROM page_fan WHERE page_id=\"95813938222\" AND uid=\"" + CurrentFacebookUID + "\""),
				new Response(
					delegate(Dictionary<object, object> likeFqlResponse)
					{
						if (RegisterEndAsync(thisAsyncOperation))
							return;

						if (U.exists(likeFqlResponse, "/value/type"))
						{
							jQueryUI.FromObject(View.ConnectDialog).Dialog("close");
						}
						else
						{
							changePanel("View.Connect_LikeButtonPanel");
							FB.Event.subscribe(
								"edge.create",
								new Response(
									delegate(Dictionary<object, object> edgeCreateResponse)
									{
										jQueryUI.FromObject(View.ConnectDialog).Dialog("close");
									}
								)
							);
						}
					}
				)
			);

		}
		#endregion
		#endregion

		#region Connect_LoadingPanel ******************************************************************
		#region add_View_Connect_LoadingPanel
		Element add_View_Connect_LoadingPanel()
		{
			#region LoadingPanel html
			string s = @"
<div id=""{ClientID}Connect_LoadingPanel"" class=""LoginPanel"" style=""display:none;"">
	<div class=""LoginPanelInner"">
		<p class=""LoginPanelTitle"">
			Loading...
		</p>
		<p>
			We're waiting for Facebook to connect...
		</p>
	</div>
	<p>
		<button id=""{ClientID}Connect_Loading_CancelButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:right;"">Cancel</button>
	</p>
</div>
";
			addChild(s);
			#endregion
			jQuery.FromElement(View.Connect_Loading_CancelButton).Click(new jQueryEventHandler(cancelButtonClick));
			return View.Connect_LoadingPanel;
		}
		#endregion
		#endregion

		#region Connect_ErrorPanel ********************************************************************
		#region add_View_Connect_ErrorPanel()
		Element add_View_Connect_ErrorPanel()
		{
			#region ErrorPanel html
			string s = @"
<div id=""{ClientID}Connect_ErrorPanel"" class=""LoginPanel"" style=""display:none;"">
	<div class=""LoginPanelInner"">
		<p class=""LoginPanelTitle"">
			Oops!
		</p>
		<p id=""{ClientID}Connect_Error_ErrorDescription"" />
		<p>
			<button id=""{ClientID}Connect_Error_TryAgainButton"" class=""ui-state-default ui-corner-all Pointer BigButton"">Try again</button>
		</p>
	</div>
	<p>
		<button id=""{ClientID}Connect_Error_CancelButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"" style=""float:right;"">Cancel</button>
	</p>
</div>
";
			addChild(s);
			#endregion
			jQuery.FromElement(View.Connect_Error_CancelButton).Click(new jQueryEventHandler(cancelButtonClick));
			jQuery.FromElement(View.Connect_Error_TryAgainButton).Click(new jQueryEventHandler(errorTryAgainClick));
			return View.Connect_ErrorPanel;
		}
		#endregion
		#region showError
		void showError(int id, string description)
		{
			changePanel("View.Connect_ErrorPanel");
			View.Connect_Error_ErrorDescription.InnerHTML = description;
		}
		#endregion
		#region errorTryAgainClick
		void errorTryAgainClick(jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;

			initialiseForm();
		}
		#endregion
		#endregion

		#region Connect_LoadingLabel ******************************************************************
		#region add_View_Connect_LoadingLabel
		Element add_View_Connect_LoadingLabel()
		{
			#region LoadingLabel html
			string s = @"
<div id=""{ClientID}Connect_LoadingLabel"" class=""ui-state-highlight ui-corner-all BigButton"" style=""position:absolute; right:2px; top:36px; display:none; z-index:995;"">
	<p>
		Loading...
	</p>
</div>
";
			addChild(s);
			#endregion
			return View.Connect_LoadingLabel;
		}
		#endregion
		#region show / hide loading label
		bool AsyncInProgress = false;
		int AsyncOperationCounter = 0;
		Dictionary<object, object> CancelledAsyncOperations = new Dictionary<object, object>();
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
				ensurePanelGenerated("View.Connect_LoadingLabel");

				if (text.Length > 0)
					View.Connect_LoadingLabel.InnerHTML = "<p>" + text + "<p>";
				else
					View.Connect_LoadingLabel.InnerHTML = "<p>Loading...<p>";

				View.Connect_LoadingLabel.Style.Display = "";
			}

			if (setTimer)
			{
				Window.SetTimeout(
					new Callback(
						delegate(object o)
						{
							if (AsyncInProgress && thisAsyncOperationCounter == AsyncOperationCounter)
							{
								//... still going
								View.Connect_LoadingLabel.InnerHTML = "<p style=\"margin-top:3px;padding-top:0px;\">This seems to be taking a long time... <button id=\"Connect_LoadingLabel_CancelLink\">Cancel</button></p>";

								jQuery.FromElement(Document.GetElementById("Connect_LoadingLabel_CancelLink")).Click(
									new jQueryEventHandler(
										delegate(jQueryEvent e)
										{
											e.PreventDefault();

											CancelledAsyncOperations[thisAsyncOperationCounter.ToString()] = true;

											AsyncInProgress = false;

											if (View.Connect_LoadingLabel != null)
												View.Connect_LoadingLabel.Style.Display = "none";

											initialiseForm();
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

			if (View.Connect_LoadingLabel != null)
				View.Connect_LoadingLabel.Style.Display = "none";

			return false;

		}
		#endregion
		#endregion

		#region Connect_DebugPanel ********************************************************************
		#region add_View_Connect_DebugPanel
		Element add_View_Connect_DebugPanel()
		{
			#region DebugPanel html
			string s = @"
<p id=""{ClientID}Connect_DebugPanel"" style=""display:none;"">
	<textarea id=""{ClientID}Connect_Debug_Output"" cols=""75"" rows=""10""></textarea><br />
	<button id=""{ClientID}Connect_Debug_LogoutButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"">Logout</button>
	<button id=""{ClientID}Connect_Debug_DisconnectButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"">Disconnect</button>
	<button id=""{ClientID}Connect_Debug_AuthButton"" class=""ui-state-default ui-corner-all Pointer SmallButton"">Auth</button>
</p>
";
			addChild(s);
			//DOMElement el = Document.CreateElement("div");
			//el.InnerHTML = replaceClientId(s);
			//Document.GetElementById("ContentDiv").AppendChild(el);
			#endregion
			jQuery.FromElement(View.Connect_Debug_LogoutButton).Click(new jQueryEventHandler(logoutButtonClick));
			jQuery.FromElement(View.Connect_Debug_DisconnectButton).Click(new jQueryEventHandler(disconnectDebug));
			return View.Connect_DebugPanel;
		}
		#endregion
		#region disconnectDebug
		void disconnectDebug(jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;

			int thisAsyncOperation = RegisterStartAsync("Disconnecting...");
			FB.api(
				F.d("method", "Auth.revokeAuthorization"),
				new Response(
					delegate(Dictionary<object, object> revokeResponse)
					{
						if (RegisterEndAsync(thisAsyncOperation))
							return;

						removeAuthCookie();
						jQueryUI.FromObject(View.ConnectDialog).Dialog("close");
					}
				)
			);
		}
		#endregion
		#region debug
		public void debug(string txt)
		{
			//return;

			jQueryUI.FromObject(View.ConnectDialog).Dialog("open");

			ensurePanelGenerated("View.Connect_DebugPanel");

			View.Connect_DebugPanel.Style.Display = "";
			View.Connect_Debug_Output.InnerHTML = txt + "\n" + View.Connect_Debug_Output.InnerHTML;
		}
		#endregion
		#endregion
		#endregion

		#region Auth functions
		#region setAuthCookie
		void setAuthCookie(Dictionary<object, object> authCookie, Dictionary<object, object> authUsr)
		{
			string name = U.get(authCookie, "Name").ToString();
			string value = U.get(authCookie, "Value").ToString();
			string expires = ((Date)U.get(authCookie, "Expires")).ToUTCString();
			string path = U.get(authCookie, "Path").ToString();

			Document.Cookie = "SpottedAuthFix=" + value.Escape() + "; " + "expires=" + expires + "; path=" + path;

			try
			{
				string[] valueParts = value.Split('-');
				string usrK = valueParts[1];
				string facebookUID = valueParts[2];

				CurrentAuthUsrK = usrK;
				CurrentAuthUsrFacebookUID = facebookUID;
				updateIsLoggedIn();
			}
			catch { }

			try
			{
				CurrentAuthUsrNickName = U.get(authUsr, "NickName").ToString();
				CurrentAuthUsrLink = U.get(authUsr, "Link").ToString();
				CurrentAuthUsrEmail = U.get(authUsr, "Email").ToString();
				CurrentAuthUsrHasNullPassword = (bool)U.get(authUsr, "HasNullPassword");
			}
			catch { }

			CurrentAuthCookieHasError = false;


		}
		#endregion
		#region removeAuthCookie
		void removeAuthCookie()
		{
			CurrentAuthUsrK = "0";
			CurrentAuthUsrFacebookUID = "0";
			updateIsLoggedIn();

			Document.Cookie = "SpottedAuthFix=1; expires=Fri, 27 Jul 2001 02:47:11 UTC; path=/;"; //Set expires to a time in the past removes the cookie
		}
		#endregion
		#region updateCurrentFacebookLoginStatus
		void updateCurrentFacebookLoginStatus(Dictionary<object, object> statusResponse)
		{
			CurrentFacebookConnected = U.exists(statusResponse, "status") && U.get(statusResponse, "status").ToString() == "connected";
			CurrentFacebookLoggedIn = U.exists(statusResponse, "status") && U.get(statusResponse, "status").ToString() != "unknown";

			CurrentFacebookUID = CurrentFacebookConnected ? U.get(statusResponse, "authResponse/userID").ToString() : "0";
			CurrentFacebookAuthResponse = CurrentFacebookConnected ? (Dictionary<object, object>)U.get(statusResponse, "authResponse") : null;

			updateIsLoggedIn();
		}
		#endregion
		#region updateAuthDetailsFromDsiCookie
		public void updateAuthDetailsFromDsiCookie()
		{
			string s = readCookie("SpottedAuthFix");
			if (!CurrentAuthCookieHasError && s != "" && s != "1")
			{
				string[] valueParts = s.Split('-');
				string usrK = valueParts[1];
				string facebookUID = valueParts[2];

				CurrentAuthUsrK = usrK;
				CurrentAuthUsrFacebookUID = facebookUID;
			}
			else
			{
				CurrentAuthUsrK = "0";
				CurrentAuthUsrFacebookUID = "0";
			}
			updateIsLoggedIn();
		}
		#endregion
		#region updateIsLoggedIn
		public bool updateIsLoggedIn()
		{
			CurrentIsLoggedIn = CurrentAuthUsrK != "0";
			CurrentIsLoggedInWithFacebook = CurrentFacebookUID != "0" && CurrentFacebookUID == CurrentAuthUsrFacebookUID;
			return CurrentIsLoggedIn;
		}
		#endregion
		#region readCookie
		static string readCookie(string name)
		{
			string nameEQ = name + "=";
			string[] ca = Document.Cookie.Split(';');
			for (int i = 0; i < ca.Length; i++)
			{
				string c = ca[i];
				c = c.Trim();

				if (c.IndexOf(nameEQ) == 0)
					return c.Substring(nameEQ.Length, c.Length);
			}
			return "";
		}
		#endregion
		#endregion

		#region General functions
		#region cancelButtonClick
		void cancelButtonClick(jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;

			jQueryUI.FromObject(View.ConnectDialog).Dialog("close");
		}
		#endregion
		#region logoutButtonClick
		void logoutButtonClick(jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;

			logoutNow(true, null, false);
		}
		#endregion
		#region logoutOfFacebookButtonClick
		void logoutOfFacebookButtonClick(jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;

			logoutNow(true, null, true);
		}
		#endregion
		#region LogOutAndDoAction
		public void LogOutAndDoAction(Action onLogout, bool forceFacebookLogout)
		{
			logoutNow(true, onLogout, forceFacebookLogout);
		}
		#endregion
		#region LogoutNow
		void logoutNow(bool closeConnectDialog, Action onLogout, bool forceFacebookLogout)
		{
			FacebookAccountNeedsConfirmationBecauseInitiallyFacebookLoggedIn = false;
			FacebookAccountNeedsConfirmationBecauseInitiallyFacebookConnectedAndSiteLoggedOut = false;
			if (CurrentIsLoggedIn || (CurrentFacebookConnected && forceFacebookLogout))
			{
				#region logout
				if (CurrentIsLoggedInWithFacebook || (CurrentFacebookConnected && forceFacebookLogout))
				{
					int thisAsyncOperation = RegisterStartAsync("Logging out...");
					FB.logout(
						new Response(
							delegate(Dictionary<object, object> logoutResponse)
							{
								if (RegisterEndAsync(thisAsyncOperation))
									return;

								logoutInternal(closeConnectDialog, onLogout);
							}
						)
					);
				}
				else
					logoutInternal(closeConnectDialog, onLogout);
				#endregion
			}
			else
			{
				#region already logged out
				logoutInternal(closeConnectDialog, onLogout);
				#endregion
			}
		}

		void logoutInternal(bool closeConnectDialog, Action onLogout)
		{
			removeAuthCookie();
			if (closeConnectDialog)
				jQueryUI.FromObject(View.ConnectDialog).Dialog("close");
			else
				initialiseForm();

			if (onLogout != null)
				onLogout();
		}
		#endregion
		#endregion

		#region changePanel
		void changePanelOnClick(string panelString, jQueryEvent e)
		{
			e.PreventDefault();

			if (AsyncInProgress)
				return;

			changePanel(panelString);

		}
		void changePanel(string panelString)
		{
			Element panel = getPanel(panelString);
			
			if (panel == null)
				panel = ensurePanelGenerated(panelString);
			
			if (panel == null)
				Script.Alert("panel is null!");
			
			if (View.Connect_ConnectingPanel != null) View.Connect_ConnectingPanel.Style.Display = panel.ID == View.Connect_ConnectingPanel.ID ? "" : "none";
			if (View.Connect_LoadingPanel != null) View.Connect_LoadingPanel.Style.Display = panel.ID == View.Connect_LoadingPanel.ID ? "" : "none";
			if (View.Connect_ErrorPanel != null) View.Connect_ErrorPanel.Style.Display = panel.ID == View.Connect_ErrorPanel.ID ? "" : "none";
			if (View.Connect_LoggedOutPanel != null) View.Connect_LoggedOutPanel.Style.Display = panel.ID == View.Connect_LoggedOutPanel.ID ? "" : "none";
			if (View.Connect_LoggedOut_NoFacebook_ChoosePanel != null) View.Connect_LoggedOut_NoFacebook_ChoosePanel.Style.Display = panel.ID == View.Connect_LoggedOut_NoFacebook_ChoosePanel.ID ? "" : "none";
			if (View.Connect_LoggedOut_NoFacebook_LoginPanel != null) View.Connect_LoggedOut_NoFacebook_LoginPanel.Style.Display = panel.ID == View.Connect_LoggedOut_NoFacebook_LoginPanel.ID ? "" : "none";
			if (View.Connect_LoggedOut_NoFacebook_LoginNoPasswordPanel != null) View.Connect_LoggedOut_NoFacebook_LoginNoPasswordPanel.Style.Display = panel.ID == View.Connect_LoggedOut_NoFacebook_LoginNoPasswordPanel.ID ? "" : "none";
			if (View.Connect_LoggedOut_NoFacebook_PasswordResetPanel != null) View.Connect_LoggedOut_NoFacebook_PasswordResetPanel.Style.Display = panel.ID == View.Connect_LoggedOut_NoFacebook_PasswordResetPanel.ID ? "" : "none";
			if (View.Connect_LoggedOut_NoFacebook_SignUp1Panel != null) View.Connect_LoggedOut_NoFacebook_SignUp1Panel.Style.Display = panel.ID == View.Connect_LoggedOut_NoFacebook_SignUp1Panel.ID ? "" : "none";
			if (View.Connect_LoggedOut_NoFacebook_SignUp2Panel != null) View.Connect_LoggedOut_NoFacebook_SignUp2Panel.Style.Display = panel.ID == View.Connect_LoggedOut_NoFacebook_SignUp2Panel.ID ? "" : "none";
			if (View.Connect_NewAccount_ConfirmFacebookPanel != null) View.Connect_NewAccount_ConfirmFacebookPanel.Style.Display = panel.ID == View.Connect_NewAccount_ConfirmFacebookPanel.ID ? "" : "none";
			if (View.Connect_NewAccount_NoEmailMatchPanel != null) View.Connect_NewAccount_NoEmailMatchPanel.Style.Display = panel.ID == View.Connect_NewAccount_NoEmailMatchPanel.ID ? "" : "none";
			if (View.Connect_NewAccount_EmailMatchPanel != null) View.Connect_NewAccount_EmailMatchPanel.Style.Display = panel.ID == View.Connect_NewAccount_EmailMatchPanel.ID ? "" : "none";
			if (View.Connect_NewAccount_ChooseAccountPanel != null) View.Connect_NewAccount_ChooseAccountPanel.Style.Display = panel.ID == View.Connect_NewAccount_ChooseAccountPanel.ID ? "" : "none";
			if (View.Connect_NewAccount_ForgotPasswordPanel != null) View.Connect_NewAccount_ForgotPasswordPanel.Style.Display = panel.ID == View.Connect_NewAccount_ForgotPasswordPanel.ID ? "" : "none";
			if (View.Connect_DetailsPanel != null) View.Connect_DetailsPanel.Style.Display = panel.ID == View.Connect_DetailsPanel.ID ? "" : "none";
			if (View.Connect_CaptchaPanel != null) View.Connect_CaptchaPanel.Style.Display = panel.ID == View.Connect_CaptchaPanel.ID ? "" : "none";
			if (View.Connect_LoggedInPanel != null) View.Connect_LoggedInPanel.Style.Display = panel.ID == View.Connect_LoggedInPanel.ID ? "" : "none";
			if (View.Connect_LikeButtonPanel != null) View.Connect_LikeButtonPanel.Style.Display = panel.ID == View.Connect_LikeButtonPanel.ID ? "" : "none";
			if (View.Connect_AutoLoginMismatchPanel != null) View.Connect_AutoLoginMismatchPanel.Style.Display = panel.ID == View.Connect_AutoLoginMismatchPanel.ID ? "" : "none";
			if (View.Connect_CreatePasswordPanel != null) View.Connect_CreatePasswordPanel.Style.Display = panel.ID == View.Connect_CreatePasswordPanel.ID ? "" : "none";
			
		}
		#endregion
		#region getPanel
		Element getPanel(string panelString)
		{
			if (panelString == "View.Connect_LoadingLabel") return View.Connect_LoadingLabel;
			else if (panelString == "View.Connect_ConnectingPanel") return View.Connect_ConnectingPanel;
			else if (panelString == "View.Connect_LoadingPanel") return View.Connect_LoadingPanel;
			else if (panelString == "View.Connect_ErrorPanel") return View.Connect_ErrorPanel;
			else if (panelString == "View.Connect_LoggedOutPanel") return View.Connect_LoggedOutPanel;
			else if (panelString == "View.Connect_LoggedOut_NoFacebook_LoginPanel") return View.Connect_LoggedOut_NoFacebook_LoginPanel;
			else if (panelString == "View.Connect_LoggedOut_NoFacebook_LoginNoPasswordPanel") return View.Connect_LoggedOut_NoFacebook_LoginNoPasswordPanel;
			else if (panelString == "View.Connect_LoggedOut_NoFacebook_PasswordResetPanel") return View.Connect_LoggedOut_NoFacebook_PasswordResetPanel;
			else if (panelString == "View.Connect_LoggedOut_NoFacebook_ChoosePanel") return View.Connect_LoggedOut_NoFacebook_ChoosePanel;
			else if (panelString == "View.Connect_LoggedOut_NoFacebook_SignUp1Panel") return View.Connect_LoggedOut_NoFacebook_SignUp1Panel;
			else if (panelString == "View.Connect_LoggedOut_NoFacebook_SignUp2Panel") return View.Connect_LoggedOut_NoFacebook_SignUp2Panel;
			else if (panelString == "View.Connect_NewAccount_ConfirmFacebookPanel") return View.Connect_NewAccount_ConfirmFacebookPanel;
			else if (panelString == "View.Connect_NewAccount_NoEmailMatchPanel") return View.Connect_NewAccount_NoEmailMatchPanel;
			else if (panelString == "View.Connect_NewAccount_EmailMatchPanel") return View.Connect_NewAccount_EmailMatchPanel;
			else if (panelString == "View.Connect_NewAccount_ChooseAccountPanel") return View.Connect_NewAccount_ChooseAccountPanel;
			else if (panelString == "View.Connect_NewAccount_ForgotPasswordPanel") return View.Connect_NewAccount_ForgotPasswordPanel;
			else if (panelString == "View.Connect_DetailsPanel") return View.Connect_DetailsPanel;
			else if (panelString == "View.Connect_CaptchaPanel") return View.Connect_CaptchaPanel;
			else if (panelString == "View.Connect_LoggedInPanel") return View.Connect_LoggedInPanel;
			else if (panelString == "View.Connect_LikeButtonPanel") return View.Connect_LikeButtonPanel;
			else if (panelString == "View.Connect_DebugPanel") return View.Connect_DebugPanel;
			else if (panelString == "View.Connect_AutoLoginMismatchPanel") return View.Connect_AutoLoginMismatchPanel;
			else if (panelString == "View.Connect_CreatePasswordPanel") return View.Connect_CreatePasswordPanel;
			else return null;
		}
		#endregion
		#region ensurePanelGenerated
		Element ensurePanelGenerated(string panelString)
		{
			Element panel = getPanel(panelString);

			if (panel != null)
				return panel;

			if (panelString == "View.Connect_LoadingLabel") panel = add_View_Connect_LoadingLabel();
			else if (panelString == "View.Connect_ConnectingPanel") panel = add_View_Connect_ConnectingPanel();
			else if (panelString == "View.Connect_LoadingPanel") panel = add_View_Connect_LoadingPanel();
			else if (panelString == "View.Connect_ErrorPanel") panel = add_View_Connect_ErrorPanel();
			else if (panelString == "View.Connect_LoggedOutPanel") panel = add_View_Connect_LoggedOutPanel();
			else if (panelString == "View.Connect_LoggedOut_NoFacebook_ChoosePanel") panel = add_View_Connect_LoggedOut_NoFacebook_ChoosePanel();
			else if (panelString == "View.Connect_LoggedOut_NoFacebook_LoginPanel") panel = add_View_Connect_LoggedOut_NoFacebook_LoginPanel();
			else if (panelString == "View.Connect_LoggedOut_NoFacebook_LoginNoPasswordPanel") panel = add_View_Connect_LoggedOut_NoFacebook_LoginNoPasswordPanel();
			else if (panelString == "View.Connect_LoggedOut_NoFacebook_PasswordResetPanel") panel = add_View_Connect_LoggedOut_NoFacebook_PasswordResetPanel();
			else if (panelString == "View.Connect_LoggedOut_NoFacebook_SignUp1Panel") panel = add_View_Connect_LoggedOut_NoFacebook_SignUp1Panel();
			else if (panelString == "View.Connect_LoggedOut_NoFacebook_SignUp2Panel") panel = add_View_Connect_LoggedOut_NoFacebook_SignUp2Panel();
			else if (panelString == "View.Connect_NewAccount_ConfirmFacebookPanel") panel = add_View_Connect_NewAccount_ConfirmFacebookPanel();
			else if (panelString == "View.Connect_NewAccount_NoEmailMatchPanel") panel = add_View_Connect_NewAccount_NoEmailMatchPanel();
			else if (panelString == "View.Connect_NewAccount_EmailMatchPanel") panel = add_View_Connect_NewAccount_EmailMatchPanel();
			else if (panelString == "View.Connect_NewAccount_ChooseAccountPanel") panel = add_View_Connect_NewAccount_ChooseAccountPanel();
			else if (panelString == "View.Connect_DetailsPanel") panel = add_View_Connect_DetailsPanel();
			else if (panelString == "View.Connect_CaptchaPanel") panel = add_View_Connect_CaptchaPanel();
			else if (panelString == "View.Connect_NewAccount_ForgotPasswordPanel") panel = add_View_Connect_NewAccount_ForgotPasswordPanel();
			else if (panelString == "View.Connect_LoggedInPanel") panel = add_View_Connect_LoggedInPanel();
			else if (panelString == "View.Connect_LikeButtonPanel") panel = add_View_Connect_LikeButtonPanel();
			else if (panelString == "View.Connect_DebugPanel") panel = add_View_Connect_DebugPanel();
			else if (panelString == "View.Connect_AutoLoginMismatchPanel") panel = add_View_Connect_AutoLoginMismatchPanel();
			else if (panelString == "View.Connect_CreatePasswordPanel") panel = add_View_Connect_CreatePasswordPanel();

			return panel;
		}
		//RegularExpression clientIdRegex = new RegularExpression(@"{ClientID}", "g");
		string replaceClientId(string s)
		{
			return s.Replace("{ClientID}", View.clientId + "_");
		}
		void addChild(string s)
		{
			Element el = Document.CreateElement("div");
			el.InnerHTML = replaceClientId(s);
			View.ConnectDialog.AppendChild(el);
		}
		#endregion

		#region Helpers
		#region defaultButton
		void defaultButton(InputElement textBox, InputElement button)
		{
			jQuery.FromElement(textBox).Keyup(delegate(jQueryEvent e)
			{
				if (e.Which == 13)
				{
					if (AsyncInProgress)
						return;

					button.Click();

				}
			});
		}
		#endregion
		#region getStringFromBasePage
		string getStringFromBasePage(string id)
		{
			string s = "";
			try
			{
				s = ((InputElement)Document.GetElementById(id)).Value;
			}
			catch { }
			return s;
		}
		#endregion
		#region getStringFromPage
		string getStringFromPage(string id)
		{
			return getStringFromBasePage("Content_" + id);
		}
		#endregion
		#region getIntFromPage
		int getIntFromPage(string id)
		{
			int i = 0;
			try
			{
				i = int.Parse(((InputElement)Document.GetElementById("Content_" + id)).Value);
			}
			catch { }
			return i;
		}
		#endregion
		#region getBoolFromPage
		bool getBoolFromPage(string id)
		{
			return getBoolFromBasePage("Content_" + id);
		}
		#endregion
		#region getBoolFromBasePage
		bool getBoolFromBasePage(string id)
		{
			bool b = false;
			try
			{
				b = bool.Parse(((InputElement)Document.GetElementById(id)).Value.ToLowerCase());
			}
			catch { }
			return b;
		}
		#endregion
		#region addOption
		void addOption(string value, string text, SelectElement parent)
		{
			OptionElement oe = (OptionElement)Document.CreateElement("OPTION");
			oe.Value = value;
			oe.Text = text;
			try
			{
				parent.Add(oe, null);
			}
			catch
			{
				parent.Add(oe);
			}

		}
		#endregion
		#endregion

	}
	
}
