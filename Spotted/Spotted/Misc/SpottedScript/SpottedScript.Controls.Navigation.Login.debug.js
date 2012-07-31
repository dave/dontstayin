Type.registerNamespace('SpottedScript.Controls.Navigation.Login');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.Navigation.Login.PageImplementation
function WhenLoggedIn(onLogin) {
    /// <param name="onLogin" type="ScriptSharpLibrary.Action">
    /// </param>
    /// <returns type="Boolean"></returns>
    var action = Function.createDelegate(null, function() {
        if (SpottedScript.Controls.Navigation.Login.Controller.instance.currentIsLoggedIn) {
            onLogin();
        }
        else {
            SpottedScript.Controls.Navigation.Login.Controller.instance.show(Function.createDelegate(null, function(loggedIn, stateChanged) {
                if (loggedIn) {
                    onLogin();
                }
            }));
        }
    });
    if (SpottedScript.Controls.Navigation.Login.Controller.instance.initialised) {
        action();
    }
    else {
        SpottedScript.Controls.Navigation.Login.Controller.instance.whenInitialisedAction = action;
    }
    return false;
}
function WhenLoggedInButton(element) {
    /// <param name="element" type="Object" domElement="true">
    /// </param>
    /// <returns type="Boolean"></returns>
    return WhenLoggedInButtonValidator(element, '');
}
function WhenLoggedInButtonValidator(element, validators) {
    /// <param name="element" type="Object" domElement="true">
    /// </param>
    /// <param name="validators" type="String">
    /// </param>
    /// <returns type="Boolean"></returns>
    WhenLoggedIn(Function.createDelegate(null, function() {
        eval('WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions(\"' + element.id.replace(new RegExp('_', 'g'), '$') + '\", \"\", true, \"' + validators + '\", \"\", false, true));');
    }));
    return false;
}
function WhenLoggedInButtonNoValidation(element) {
    /// <param name="element" type="Object" domElement="true">
    /// </param>
    /// <returns type="Boolean"></returns>
    WhenLoggedIn(Function.createDelegate(null, function() {
        eval('__doPostBack(\"' + element.id.replace(new RegExp('_', 'g'), '$') + '\",\'\');');
    }));
    return false;
}
function WhenLoggedInHtmlButton(element) {
    /// <param name="element" type="Object" domElement="true">
    /// </param>
    /// <returns type="Boolean"></returns>
    WhenLoggedIn(Function.createDelegate(null, function() {
        eval('__doPostBack(\'' + element.id.replace(new RegExp('_', 'g'), '$') + '\',\'\');');
    }));
    return false;
}
function WhenLoggedInRadio(element) {
    /// <summary>
    /// Remember this should be followed by "return true;", so that the radio button doesn't reset to the previous value.
    /// </summary>
    /// <param name="element" type="Object" domElement="true">
    /// </param>
    /// <returns type="Boolean"></returns>
    WhenLoggedIn(Function.createDelegate(null, function() {
        eval('setTimeout(\'__doPostBack(\\\'' + element.id.replace(new RegExp('_', 'g'), '$') + '\\\',\\\'\\\')\', 0);');
    }));
    return true;
}
function WhenLoggedInAnchor(anchor) {
    /// <param name="anchor" type="Object" domElement="true">
    /// </param>
    /// <returns type="Boolean"></returns>
    LogInTransfer(anchor.href);
    return false;
}
function ConnectButtonClick() {
    var action = Function.createDelegate(null, function() {
        SpottedScript.Controls.Navigation.Login.Controller.instance.show(Function.createDelegate(null, function(loggedIn, stateChanged) {
            if (stateChanged) {
                if (loggedIn) {
                    window.location.href = window.location.href;
                }
                else {
                    window.location.href = '/';
                }
            }
        }));
    });
    if (SpottedScript.Controls.Navigation.Login.Controller.instance.initialised) {
        action();
    }
    else {
        SpottedScript.Controls.Navigation.Login.Controller.instance.whenInitialisedAction = action;
    }
}
function LogInTransfer(url) {
    /// <param name="url" type="String">
    /// </param>
    var action = Function.createDelegate(null, function() {
        if (SpottedScript.Controls.Navigation.Login.Controller.instance.currentIsLoggedIn) {
            window.location.href = url;
        }
        else {
            SpottedScript.Controls.Navigation.Login.Controller.instance.show(Function.createDelegate(null, function(loggedIn, stateChanged) {
                if (loggedIn) {
                    window.location.href = url;
                }
            }));
        }
    });
    if (SpottedScript.Controls.Navigation.Login.Controller.instance.initialised) {
        action();
    }
    else {
        SpottedScript.Controls.Navigation.Login.Controller.instance.whenInitialisedAction = action;
    }
}
function IsLoggedIn() {
    /// <returns type="Boolean"></returns>
    return SpottedScript.Controls.Navigation.Login.Controller.instance.currentIsLoggedIn;
}
function LoginDebug(text) {
    /// <param name="text" type="String">
    /// </param>
    SpottedScript.Controls.Navigation.Login.Controller.instance.debug(text);
}
function LoginFacebookReady() {
    SpottedScript.Controls.Navigation.Login.Controller.instance.facebookReady('LoginFacebookReady');
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.Navigation.Login.Controller
SpottedScript.Controls.Navigation.Login.Controller = function SpottedScript_Controls_Navigation_Login_Controller(view) {
    /// <param name="view" type="SpottedScript.Controls.Navigation.Login.View">
    /// </param>
    /// <field name="_view" type="SpottedScript.Controls.Navigation.Login.View">
    /// </field>
    /// <field name="_server" type="SpottedScript.Controls.Navigation.Login.Server">
    /// </field>
    /// <field name="instance" type="SpottedScript.Controls.Navigation.Login.Controller" static="true">
    /// </field>
    /// <field name="autoLogin" type="Boolean">
    /// </field>
    /// <field name="autoLogout" type="Boolean">
    /// </field>
    /// <field name="autoLoginUsrK" type="Number" integer="true">
    /// </field>
    /// <field name="autoLoginUsrLoginString" type="String">
    /// </field>
    /// <field name="autoLoginRedirectUrl" type="String">
    /// </field>
    /// <field name="autoLoginLogOutFirst" type="Boolean">
    /// </field>
    /// <field name="autoLoginUsrEmail" type="String">
    /// </field>
    /// <field name="autoLoginUsrIsSkeleton" type="Boolean">
    /// </field>
    /// <field name="autoLoginUsrIsEnhancedSecurity" type="Boolean">
    /// </field>
    /// <field name="autoLoginUsrIsFacebookNotConfirmed" type="Boolean">
    /// </field>
    /// <field name="autoLoginUsrNeedsCaptcha" type="Boolean">
    /// </field>
    /// <field name="autoLoginUsrCaptchaEncrypted" type="String">
    /// </field>
    /// <field name="autoLoginUsrHomePlaceK" type="Number" integer="true">
    /// </field>
    /// <field name="autoLoginUsrHomePlaceName" type="String">
    /// </field>
    /// <field name="autoLoginUsrHomeCountryK" type="Number" integer="true">
    /// </field>
    /// <field name="autoLoginUsrHomeCountryName" type="String">
    /// </field>
    /// <field name="autoLoginUsrHomeGoodMatch" type="Boolean">
    /// </field>
    /// <field name="autoLoginUsrFavouriteMusicK" type="Number" integer="true">
    /// </field>
    /// <field name="autoLoginUsrSendSpottedEmails" type="Boolean">
    /// </field>
    /// <field name="autoLoginUsrSendEflyers" type="Boolean">
    /// </field>
    /// <field name="_autoLoginNickname" type="String">
    /// </field>
    /// <field name="_autoLoginLink" type="String">
    /// </field>
    /// <field name="_autoLoginEmail" type="String">
    /// </field>
    /// <field name="_autoLoginStringMatch" type="Boolean">
    /// </field>
    /// <field name="_initialIsLoggedIn" type="Boolean">
    /// </field>
    /// <field name="initialised" type="Boolean">
    /// </field>
    /// <field name="whenInitialisedAction" type="ScriptSharpLibrary.Action">
    /// </field>
    /// <field name="_initialFacebookLoggedIn" type="Boolean">
    /// </field>
    /// <field name="_initialFacebookConnected" type="Boolean">
    /// </field>
    /// <field name="_initialFacebookUID" type="String">
    /// </field>
    /// <field name="_initialFacebookAuthResponse" type="Object">
    /// </field>
    /// <field name="_initialAuthCookieHasError" type="Boolean">
    /// </field>
    /// <field name="_openDialogIsLoggedIn" type="Boolean">
    /// </field>
    /// <field name="currentIsLoggedIn" type="Boolean">
    /// </field>
    /// <field name="currentIsLoggedInWithFacebook" type="Boolean">
    /// </field>
    /// <field name="_currentFacebookLoggedIn" type="Boolean">
    /// </field>
    /// <field name="_currentFacebookConnected" type="Boolean">
    /// </field>
    /// <field name="_currentFacebookUID" type="String">
    /// </field>
    /// <field name="_currentFacebookAuthResponse" type="Object">
    /// </field>
    /// <field name="_currentAuthCookieHasError" type="Boolean">
    /// </field>
    /// <field name="_currentAuthUsrK" type="String">
    /// </field>
    /// <field name="_currentAuthUsrFacebookUID" type="String">
    /// </field>
    /// <field name="_currentAuthUsrNickName" type="String">
    /// </field>
    /// <field name="_currentAuthUsrLink" type="String">
    /// </field>
    /// <field name="_currentAuthUsrEmail" type="String">
    /// </field>
    /// <field name="_currentAuthUsrHasNullPassword" type="Boolean">
    /// </field>
    /// <field name="_exitEvent" type="ScriptSharpLibrary.ActionBoolBool">
    /// </field>
    /// <field name="_doneControllerInit" type="Boolean">
    /// </field>
    /// <field name="_facebookAccountNeedsConfirmationBecauseInitiallyFacebookLoggedIn" type="Boolean">
    /// </field>
    /// <field name="_facebookAccountNeedsConfirmationBecauseInitiallyFacebookConnectedAndSiteLoggedOut" type="Boolean">
    /// </field>
    /// <field name="_facebookAccountConfirmationStepDone" type="Boolean">
    /// </field>
    /// <field name="_noFacebookSignUp2PanelLoginUsrK" type="Number" integer="true">
    /// </field>
    /// <field name="_noFacebookSignUp1PanelSource" type="String">
    /// </field>
    /// <field name="_noFacebookSignUp2PanelSource" type="String">
    /// </field>
    /// <field name="_detailsShowSource" type="String">
    /// </field>
    /// <field name="_captchaPanelSource" type="String">
    /// </field>
    /// <field name="_detailsDefaultCountryK" type="Number" integer="true">
    /// </field>
    /// <field name="_detailsDefaultPlaceK" type="Number" integer="true">
    /// </field>
    /// <field name="_detailsDefaultPlaceGoodMatch" type="Boolean">
    /// </field>
    /// <field name="_detailsPlaceDropDownJ" type="JQ.JQueryObject">
    /// </field>
    /// <field name="_detailsCountryDropDownJ" type="JQ.JQueryObject">
    /// </field>
    /// <field name="_detailsCountryDropDownPopulated" type="Boolean">
    /// </field>
    /// <field name="_detailsPlaceDropDownPopulated" type="Boolean">
    /// </field>
    /// <field name="_detailsPlaceDropDownPopulatedCountryK" type="Number" integer="true">
    /// </field>
    /// <field name="_detailsCountrySelectedK" type="Number" integer="true">
    /// </field>
    /// <field name="_detailsPlaceSelectedK" type="Number" integer="true">
    /// </field>
    /// <field name="_detailsPlacePreviouslySelectedIndex" type="Number" integer="true">
    /// </field>
    /// <field name="_detailsCountryPreviouslySelectedIndex" type="Number" integer="true">
    /// </field>
    /// <field name="_detailsPlaceDropDownVisible" type="Boolean">
    /// </field>
    /// <field name="_facebookEmailMatch" type="Boolean">
    /// </field>
    /// <field name="_facebookEmailMatchToCurrentUser" type="Boolean">
    /// </field>
    /// <field name="_facebookEmailMatchEnhancedSecurity" type="Boolean">
    /// </field>
    /// <field name="_facebookEmailMatchNickName" type="String">
    /// </field>
    /// <field name="_facebookEmailMatchEmail" type="String">
    /// </field>
    /// <field name="_closeOnLoggedIn" type="Boolean">
    /// </field>
    /// <field name="_connectButtonClickAsyncOperation" type="Number" integer="true">
    /// </field>
    /// <field name="_noFacebookLoginPanelSource" type="String">
    /// </field>
    /// <field name="_previousNicknameTest" type="String">
    /// </field>
    /// <field name="_noFacebookSignup2NicknameHasEntry" type="Boolean">
    /// </field>
    /// <field name="_captchaPassthrough" type="String">
    /// </field>
    /// <field name="_detectAutoLoginProblemNewLink" type="Boolean">
    /// </field>
    /// <field name="_asyncInProgress" type="Boolean">
    /// </field>
    /// <field name="_asyncOperationCounter" type="Number" integer="true">
    /// </field>
    /// <field name="_cancelledAsyncOperations" type="Object">
    /// </field>
    /// <field name="_clientIdRegex" type="RegExp">
    /// </field>
    this._cancelledAsyncOperations = {};
    this._clientIdRegex = new RegExp('{ClientID}', 'g');
    SpottedScript.Controls.Navigation.Login.Controller.instance = this;
    this._view = view;
    this._server = view.server;
    if (SpottedScript.Misc.get_browserIsIE()) {
        jQuery(document.body).ready(Function.createDelegate(this, this._initialise));
    }
    else {
        this._initialise();
    }
}
SpottedScript.Controls.Navigation.Login.Controller._readCookie = function SpottedScript_Controls_Navigation_Login_Controller$_readCookie(name) {
    /// <param name="name" type="String">
    /// </param>
    /// <returns type="String"></returns>
    var nameEQ = name + '=';
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        c = c.trim();
        if (c.indexOf(nameEQ) === 0) {
            return c.substring(nameEQ.length, c.length);
        }
    }
    return '';
}
SpottedScript.Controls.Navigation.Login.Controller.prototype = {
    _view: null,
    _server: null,
    autoLogin: false,
    autoLogout: false,
    autoLoginUsrK: 0,
    autoLoginUsrLoginString: '',
    autoLoginRedirectUrl: '',
    autoLoginLogOutFirst: false,
    autoLoginUsrEmail: '',
    autoLoginUsrIsSkeleton: false,
    autoLoginUsrIsEnhancedSecurity: false,
    autoLoginUsrIsFacebookNotConfirmed: false,
    autoLoginUsrNeedsCaptcha: false,
    autoLoginUsrCaptchaEncrypted: '',
    autoLoginUsrHomePlaceK: 0,
    autoLoginUsrHomePlaceName: '',
    autoLoginUsrHomeCountryK: 0,
    autoLoginUsrHomeCountryName: '',
    autoLoginUsrHomeGoodMatch: false,
    autoLoginUsrFavouriteMusicK: 0,
    autoLoginUsrSendSpottedEmails: false,
    autoLoginUsrSendEflyers: false,
    _autoLoginNickname: '',
    _autoLoginLink: '',
    _autoLoginEmail: '',
    _autoLoginStringMatch: false,
    _initialIsLoggedIn: false,
    initialised: false,
    whenInitialisedAction: null,
    _initialFacebookLoggedIn: false,
    _initialFacebookConnected: false,
    _initialFacebookUID: '0',
    _initialFacebookAuthResponse: null,
    _initialAuthCookieHasError: false,
    _openDialogIsLoggedIn: false,
    currentIsLoggedIn: false,
    currentIsLoggedInWithFacebook: false,
    _currentFacebookLoggedIn: false,
    _currentFacebookConnected: false,
    _currentFacebookUID: '0',
    _currentFacebookAuthResponse: null,
    _currentAuthCookieHasError: false,
    _currentAuthUsrK: '0',
    _currentAuthUsrFacebookUID: '0',
    _currentAuthUsrNickName: '',
    _currentAuthUsrLink: '',
    _currentAuthUsrEmail: '',
    _currentAuthUsrHasNullPassword: false,
    _exitEvent: null,
    _doneControllerInit: false,
    _facebookAccountNeedsConfirmationBecauseInitiallyFacebookLoggedIn: false,
    _facebookAccountNeedsConfirmationBecauseInitiallyFacebookConnectedAndSiteLoggedOut: false,
    _facebookAccountConfirmationStepDone: false,
    _noFacebookSignUp2PanelLoginUsrK: 0,
    _noFacebookSignUp1PanelSource: '',
    _noFacebookSignUp2PanelSource: '',
    _detailsShowSource: '',
    _captchaPanelSource: '',
    _detailsDefaultCountryK: 0,
    _detailsDefaultPlaceK: 0,
    _detailsDefaultPlaceGoodMatch: false,
    _detailsPlaceDropDownJ: null,
    _detailsCountryDropDownJ: null,
    _detailsCountryDropDownPopulated: false,
    _detailsPlaceDropDownPopulated: false,
    _detailsPlaceDropDownPopulatedCountryK: 0,
    _detailsCountrySelectedK: 0,
    _detailsPlaceSelectedK: 0,
    _detailsPlacePreviouslySelectedIndex: 0,
    _detailsCountryPreviouslySelectedIndex: 0,
    _detailsPlaceDropDownVisible: false,
    _initialise: function SpottedScript_Controls_Navigation_Login_Controller$_initialise() {
        jQuery(this._view.get_connectDialog()).dialog(F.d('autoOpen', false, 'width', 505, 'height', 280, 'modal', true, 'resizable', false, 'zIndex', 990, 'draggable', false, 'closeOnEscape', false, 'open', Function.createDelegate(this, function(ev, ui) {
            jQuery('.ui-dialog-titlebar-close').hide();
        })));
        this._view.get_connectDialog().style.display = '';
        this._doneControllerInit = true;
        this.facebookReady('initialise');
    },
    facebookReady: function SpottedScript_Controls_Navigation_Login_Controller$facebookReady(from) {
        /// <param name="from" type="String">
        /// </param>
        if (this._doneControllerInit && eval('DoneFbAsyncInit')) {
            this._initialAuthCookieHasError = this._getBoolFromBasePage('AuthCookieHasError');
            this._currentAuthCookieHasError = this._initialAuthCookieHasError;
            this.updateAuthDetailsFromDsiCookie();
            FB.Event.subscribe('auth.statusChange', Function.createDelegate(this, function(statusResponse) {
                this._updateCurrentFacebookLoginStatus(statusResponse);
            }));
            FB.getLoginStatus(Function.createDelegate(this, function(statusResponse) {
                this._updateCurrentFacebookLoginStatus(statusResponse);
                this._initialIsLoggedIn = this.currentIsLoggedIn;
                this._initialFacebookLoggedIn = this._currentFacebookLoggedIn;
                this._initialFacebookConnected = this._currentFacebookConnected;
                this._initialFacebookUID = this._currentFacebookUID;
                this._initialFacebookAuthResponse = this._currentFacebookAuthResponse;
                this.initialised = true;
                this.autoLogout = this._getBoolFromPage('AutoLogout_Value');
                this.autoLogin = this._getBoolFromPage('AutoLogin_Value');
                this.autoLoginRedirectUrl = this._getStringFromPage('AutoLogin_RedirectUrl');
                this.autoLoginUsrK = this._getIntFromPage('AutoLogin_UsrK');
                this.autoLoginUsrLoginString = this._getStringFromPage('AutoLogin_String');
                this.autoLoginLogOutFirst = this._getBoolFromPage('AutoLogin_LogOutFirst');
                this.autoLoginUsrEmail = this._getStringFromPage('AutoLogin_UsrEmail');
                this.autoLoginUsrIsSkeleton = this._getBoolFromPage('AutoLogin_UsrIsSkeleton');
                this.autoLoginUsrIsEnhancedSecurity = this._getBoolFromPage('AutoLogin_UsrIsEnhancedSecurity');
                this.autoLoginUsrIsFacebookNotConfirmed = this._getBoolFromPage('AutoLogin_UsrIsFacebookNotConfirmed');
                this.autoLoginUsrNeedsCaptcha = this._getBoolFromPage('AutoLogin_UsrNeedsCaptcha');
                this.autoLoginUsrCaptchaEncrypted = this._getStringFromPage('AutoLogin_UsrCaptchaEncrypted');
                this.autoLoginUsrHomePlaceK = this._getIntFromPage('AutoLogin_HomePlaceK');
                this.autoLoginUsrHomePlaceName = this._getStringFromPage('AutoLogin_HomePlaceName');
                this.autoLoginUsrHomeCountryK = this._getIntFromPage('AutoLogin_HomeCountryK');
                this.autoLoginUsrHomeCountryName = this._getStringFromPage('AutoLogin_HomeCountryName');
                this.autoLoginUsrHomeGoodMatch = this._getBoolFromPage('AutoLogin_HomeGoodMatch');
                this.autoLoginUsrFavouriteMusicK = this._getIntFromPage('AutoLogin_FavouriteMusicK');
                this.autoLoginUsrSendSpottedEmails = this._getBoolFromPage('AutoLogin_SendSpottedEmails');
                this.autoLoginUsrSendEflyers = this._getBoolFromPage('AutoLogin_SendEflyers');
                if (this._initialFacebookLoggedIn) {
                    this._facebookAccountNeedsConfirmationBecauseInitiallyFacebookLoggedIn = true;
                }
                if (this._initialFacebookConnected && !this._initialIsLoggedIn) {
                    this._facebookAccountNeedsConfirmationBecauseInitiallyFacebookConnectedAndSiteLoggedOut = true;
                }
                if (this.autoLogout) {
                    this._logoutNow(true, Function.createDelegate(this, function() {
                        this._redirectToHomePage();
                    }), false);
                }
                else if (this.autoLogin) {
                    if (this.currentIsLoggedIn && this._currentAuthUsrK === this.autoLoginUsrK.toString() && !this.autoLoginLogOutFirst) {
                        window.location.href = this.autoLoginRedirectUrl;
                    }
                    else if (this.currentIsLoggedIn) {
                        this.logOutAndDoAction(Function.createDelegate(this, function() {
                            this._autoLoginShowPanel();
                        }), false);
                    }
                    else {
                        this._removeAuthCookie();
                        this._autoLoginShowPanel();
                    }
                }
                else if (this.whenInitialisedAction != null) {
                    this.whenInitialisedAction();
                }
            }));
        }
    },
    _autoLoginShowPanel: function SpottedScript_Controls_Navigation_Login_Controller$_autoLoginShowPanel() {
        SpottedScript.Controls.Navigation.Login.Controller.instance.show(Function.createDelegate(this, function(loggedIn, stateChanged) {
            if (loggedIn) {
                window.location.href = this.autoLoginRedirectUrl;
            }
            else {
                window.location.href = '/';
            }
        }));
    },
    _redirectToHomePage: function SpottedScript_Controls_Navigation_Login_Controller$_redirectToHomePage() {
        window.location.href = '/';
    },
    show: function SpottedScript_Controls_Navigation_Login_Controller$show(onExit) {
        /// <param name="onExit" type="ScriptSharpLibrary.ActionBoolBool">
        /// </param>
        this._exitEvent = onExit;
        this._openDialogIsLoggedIn = this.currentIsLoggedIn;
        this._changePanel('View.Connect_LoadingPanel');
        this._initialiseForm();
        jQuery(this._view.get_connectDialog()).dialog('open');
        jQuery(this._view.get_connectDialog()).dialog(F.d('close', Function.createDelegate(this, function() {
            this._exitEvent(this.currentIsLoggedIn, this.currentIsLoggedIn !== this._openDialogIsLoggedIn);
        })));
    },
    _initialiseForm: function SpottedScript_Controls_Navigation_Login_Controller$_initialiseForm() {
        if (this.currentIsLoggedIn) {
            this._currentAuthUsrNickName = this._getStringFromBasePage('UsrNickname');
            this._currentAuthUsrLink = this._getStringFromBasePage('UsrLink');
            jQuery(this._view.get_connectDialog()).dialog('open');
            this._showLoggedInPanel((this._currentAuthUsrNickName.length > 0) ? this._currentAuthUsrLink : '???');
        }
        else {
            if (this._currentFacebookConnected) {
                this._closeOnLoggedIn = false;
            }
            jQuery(this._view.get_connectDialog()).dialog('open');
            this._changePanel('View.Connect_LoggedOutPanel');
        }
    },
    _facebookEmailMatch: false,
    _facebookEmailMatchToCurrentUser: false,
    _facebookEmailMatchEnhancedSecurity: false,
    _facebookEmailMatchNickName: '',
    _facebookEmailMatchEmail: '',
    _closeOnLoggedIn: false,
    _configureFormConnected: function SpottedScript_Controls_Navigation_Login_Controller$_configureFormConnected() {
        var thisAsyncOperation = this._registerStartAsync('Connecting...');
        this._server.getUserByFacebookUID(this.autoLoginUsrK, this.autoLoginUsrLoginString, Function.createDelegate(this, function(response) {
            if (this._registerEndAsync(thisAsyncOperation)) {
                return;
            }
            if (ImportedUtilities.U.isTrue(response, 'Exception')) {
                jQuery(this._view.get_connectDialog()).dialog('open');
                this._showError(1, 'Internal server error');
            }
            else {
                this._facebookEmailMatch = ImportedUtilities.U.isTrue(response, 'FacebookEmailMatch');
                this._facebookEmailMatchToCurrentUser = ImportedUtilities.U.isTrue(response, 'FacebookEmailMatchToCurrentUser');
                this._facebookEmailMatchEnhancedSecurity = ImportedUtilities.U.isTrue(response, 'EnhancedSecurity');
                var facebookUidMatch = ImportedUtilities.U.isTrue(response, 'FacebookUIDMatch');
                var autoLoginMatch = ImportedUtilities.U.isTrue(response, 'FacebookAutoLoginUsrMatch');
                this._autoLoginNickname = (ImportedUtilities.U.exists(response, 'AutoLoginUsr/NickName')) ? ImportedUtilities.U.get(response, 'AutoLoginUsr/NickName').toString() : '';
                this._autoLoginLink = (ImportedUtilities.U.exists(response, 'AutoLoginUsr/Link')) ? ImportedUtilities.U.get(response, 'AutoLoginUsr/Link').toString() : '';
                this._autoLoginEmail = (ImportedUtilities.U.exists(response, 'AutoLoginUsr/Email')) ? ImportedUtilities.U.get(response, 'AutoLoginUsr/Email').toString() : '';
                this._autoLoginStringMatch = (ImportedUtilities.U.exists(response, 'AutoLoginUsr/LoginStringMatch')) ? ImportedUtilities.U.get(response, 'AutoLoginUsr/LoginStringMatch') : false;
                if (this._facebookAccountNeedsConfirmationBecauseInitiallyFacebookConnectedAndSiteLoggedOut) {
                    this._showConfirmFacebookPanel();
                    return;
                }
                else if (facebookUidMatch) {
                    this._setAuthCookie(ImportedUtilities.U.get(response, 'AuthCookie'), ImportedUtilities.U.get(response, 'AuthUsr'));
                    if (this._closeOnLoggedIn) {
                        this._detectAutoLoginProblem(false);
                    }
                    else {
                        jQuery(this._view.get_connectDialog()).dialog('open');
                        this._showLoggedInPanel((this._currentAuthUsrNickName.length > 0) ? this._currentAuthUsrLink : '???');
                    }
                }
                else if (this._facebookAccountNeedsConfirmationBecauseInitiallyFacebookLoggedIn) {
                    this._showConfirmFacebookPanel();
                    return;
                }
                else if (autoLoginMatch) {
                    this.autoLinkByAutoLoginUsr(false);
                }
                else if (this._facebookEmailMatchToCurrentUser) {
                    jQuery(this._view.get_connectDialog()).dialog('open');
                    this._ensurePanelGenerated('View.Connect_NewAccount_EmailMatchPanel');
                    this._facebookEmailMatchNickName = ImportedUtilities.U.get(response, 'EmailMatchUsr/NickName').toString();
                    this._facebookEmailMatchEmail = ImportedUtilities.U.get(response, 'EmailMatchUsr/Email').toString();
                    if (ImportedUtilities.U.get(response, 'EmailMatchUsr/NickName').toString().length > 0) {
                        this._view.get_connect_NewAccount_EmailMatch_UserLink1().innerHTML = 'Link to ' + ImportedUtilities.U.get(response, 'EmailMatchUsr/Link').toString() + ':';
                    }
                    else {
                        this._view.get_connect_NewAccount_EmailMatch_UserLink1().innerHTML = 'Link to ' + this._facebookEmailMatchEmail + ':';
                    }
                    this._view.get_connect_NewAccount_EmailMatch_BackButton().style.display = (this._facebookAccountConfirmationStepDone) ? '' : 'none';
                    this._changePanel('View.Connect_NewAccount_EmailMatchPanel');
                }
                else {
                    jQuery(this._view.get_connectDialog()).dialog('open');
                    this._ensurePanelGenerated('View.Connect_NewAccount_NoEmailMatchPanel');
                    this._view.get_connect_NewAccount_NoEmailMatch_BackButton().style.display = (this._facebookAccountConfirmationStepDone) ? '' : 'none';
                    this._changePanel('View.Connect_NewAccount_NoEmailMatchPanel');
                }
            }
        }));
    },
    _add_View_Connect_LoggedOutPanel: function SpottedScript_Controls_Navigation_Login_Controller$_add_View_Connect_LoggedOutPanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = '\r\n<div id=\"{ClientID}Connect_LoggedOutPanel\" class=\"LoginPanel\" style=\"display:none;\">\r\n\r\n\t<div style=\"position:relative;\" class=\"LoginPanelInner ClearAfter\">\r\n\t\t<div style=\"width:240px; float:left;\" class=\"ColumnWithNoParaAbove\">\r\n\t\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\t\tUse Facebook\r\n\t\t\t</p>\r\n\t\t\t<p>\r\n\t\t\t\tThe easiest way to log in to Don\'t Stay In:\r\n\t\t\t</p>\r\n\t\t\t<p>\r\n\t\t\t\t<button id=\"{ClientID}Connect_LoggedOut_ConnectButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\">\r\n\t\t\t\t\t<img src=\"/gfx/facebook-small-16.png\" width=\"16\" height=\"16\" border=\"0\" align=\"top\" />\r\n\t\t\t\t\t<span style=\"height:16px;\">Connect with Facebook</span>\r\n\t\t\t\t</button>\r\n\t\t\t</p>\r\n\t\t\t<ul style=\"margin-top:10px;\">\r\n\t\t\t\t<li>\r\n\t\t\t\t\tEasy: just three clicks to sign up\r\n\t\t\t\t</li>\r\n\t\t\t\t<li>\r\n\t\t\t\t\tSimple: use your Facebook password\r\n\t\t\t\t</li>\r\n\t\t\t</ul>\r\n\t\t</div>\r\n\t\t<div style=\"left:220px; width:10px; height:173px; float:left; border-left:dotted 2px #cccccc;\"> </div>\r\n\t\t<div style=\"left:210px; width:220px; float:left;\" class=\"ColumnWithNoParaAbove\">\r\n\t\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\t\tConnect manually\r\n\t\t\t</p>\r\n\t\t\t<p>\r\n\t\t\t\tIf you don\'t have Facebook access, you can log in manually:\r\n\t\t\t</p>\r\n\t\t\t<p>\r\n\t\t\t\t<button id=\"{ClientID}Connect_LoggedOut_NoFacebookButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\">\r\n\t\t\t\t\t<img src=\"/gfx/dsi-tiny-16.png\" width=\"16\" height=\"16\" border=\"0\" align=\"top\" />\r\n\t\t\t\t\t<span style=\"height:16px;\">Connect manually</span>\r\n\t\t\t\t</button>\r\n\t\t\t</p>\r\n\t\t\t<ul style=\"margin-top:10px;\">\r\n\t\t\t\t<li>\r\n\t\t\t\t\tWorks even if Facebook is blocked\r\n\t\t\t\t</li>\r\n\t\t\t</ul>\r\n\t\t</div>\r\n\t</div>\r\n\t<p>\r\n\t\t<button id=\"{ClientID}Connect_LoggedOut_CancelButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Cancel</button>\r\n\t</p>\r\n</div>\r\n';
        this._addChild(s);
        $addHandler(this._view.get_connect_LoggedOut_ConnectButton(), 'click', Function.createDelegate(this, this._connectButtonClick));
        $addHandler(this._view.get_connect_LoggedOut_CancelButton(), 'click', Function.createDelegate(this, this._cancelButtonClick));
        $addHandler(this._view.get_connect_LoggedOut_NoFacebookButton(), 'click', Function.createDelegate(this, this._noFacebookButtonClick));
        return this._view.get_connect_LoggedOutPanel();
    },
    _connectButtonClickAsyncOperation: 0,
    _connectButtonClick: function SpottedScript_Controls_Navigation_Login_Controller$_connectButtonClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._connectButtonClickInternal();
    },
    _connectButtonClickInternal: function SpottedScript_Controls_Navigation_Login_Controller$_connectButtonClickInternal() {
        this._closeOnLoggedIn = true;
        if (this._currentFacebookConnected) {
            this._configureFormConnected();
        }
        else {
            this._changePanel('View.Connect_ConnectingPanel');
            this._connectButtonClickAsyncOperation = this._registerStartAsyncGeneric('Connecting...', false, false);
            FB.login(Function.createDelegate(this, function(loginResponse) {
                if (this._registerEndAsync(this._connectButtonClickAsyncOperation)) {
                    return;
                }
                if (this._currentFacebookConnected) {
                    this._changePanel('View.Connect_LoadingPanel');
                    this._configureFormConnected();
                }
                else {
                    this._showError(6, 'Looks like Facebook had trouble getting you connected.');
                }
            }), F.d('scope', 'email,publish_stream,offline_access,user_birthday,user_hometown,user_location'));
        }
    },
    _noFacebookButtonClick: function SpottedScript_Controls_Navigation_Login_Controller$_noFacebookButtonClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        if (this.autoLogin) {
            if (this.autoLoginUsrIsEnhancedSecurity) {
                this._showNoFacebookLoginPanel('LoggedOutPanel', this.autoLoginUsrEmail);
            }
            else if (this.autoLoginUsrIsSkeleton) {
                this._showNoFacebookSignUp1Panel('AutoLoginNoFacebookSkeleton');
            }
            else if (this.autoLoginUsrIsFacebookNotConfirmed) {
                var details = {};
                var home = {};
                home['GoodMatch'] = this.autoLoginUsrHomeGoodMatch;
                if (this.autoLoginUsrHomeGoodMatch) {
                    home['PlaceK'] = this.autoLoginUsrHomePlaceK;
                    home['PlaceName'] = this.autoLoginUsrHomePlaceName;
                    home['CountryK'] = this.autoLoginUsrHomeCountryK;
                    home['CountryName'] = this.autoLoginUsrHomeCountryName;
                }
                else {
                    home['PlaceK'] = 0;
                    home['PlaceName'] = '';
                    home['CountryName'] = '';
                    var countryKFromIp = 224;
                    try {
                        countryKFromIp = Number.parseInvariant(this._getStringFromBasePage('CountryKFromIp'));
                    }
                    catch ($e1) {
                    }
                    home['CountryK'] = countryKFromIp;
                }
                details['HomePlace'] = home;
                details['FavouriteMusicK'] = this.autoLoginUsrFavouriteMusicK;
                details['SendSpottedEmails'] = this.autoLoginUsrSendSpottedEmails;
                details['SendEflyers'] = this.autoLoginUsrSendEflyers;
                this._showDetailsPanel('AutoLoginNoFacebookFacebookNotConfirmed', details);
            }
            else if (this.autoLoginUsrNeedsCaptcha) {
                var details = {};
                details['CaptchaEncrypted'] = this.autoLoginUsrCaptchaEncrypted;
                this._showCaptchaPanel('AutoLoginNoFacebookNeedsCaptcha', details);
            }
            else {
                this._changePanel('View.Connect_LoggedOut_NoFacebook_ChoosePanel');
            }
        }
        else {
            this._changePanel('View.Connect_LoggedOut_NoFacebook_ChoosePanel');
        }
    },
    _add_View_Connect_LoggedOut_NoFacebook_ChoosePanel: function SpottedScript_Controls_Navigation_Login_Controller$_add_View_Connect_LoggedOut_NoFacebook_ChoosePanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = '\r\n<div id=\"{ClientID}Connect_LoggedOut_NoFacebook_ChoosePanel\" class=\"LoginPanel\" style=\"display:none;\">\r\n\t<div style=\"position:relative;\" class=\"LoginPanelInner ClearAfter\">\r\n\t\t<p>\r\n\t\t\tIf you can\'t use Facebook to log in, you can log in with your Don\'t Stay In details.\r\n\t\t</p>\r\n\t\t<div style=\"width:240px; float:left;\">\r\n\t\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\t\tLog in\r\n\t\t\t</p>\r\n\t\t\t<p>\r\n\t\t\t\tIf you already have an account:\r\n\t\t\t</p>\r\n\t\t\t<p>\r\n\t\t\t\t<button id=\"{ClientID}Connect_LoggedOut_NoFacebook_Choose_LoginButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\">Log in</button>\r\n\t\t\t</p>\r\n\t\t</div>\r\n\t\t<div style=\"left:220px; width:10px; height:150px; float:left; border-left:dotted 2px #cccccc;\"> </div>\r\n\t\t<div style=\"left:210px; width:220px; float:left;\">\r\n\t\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\t\tSign up\r\n\t\t\t</p>\r\n\t\t\t<p>\r\n\t\t\t\tIf you\'ve not used Don\'t Stay In:\r\n\t\t\t</p>\r\n\t\t\t<p>\r\n\t\t\t\t<button id=\"{ClientID}Connect_LoggedOut_NoFacebook_Choose_SignupButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\">Sign up</button>\r\n\t\t\t</p>\r\n\t\t</div>\r\n\t</div>\r\n\t<p>\r\n\t\t<button id=\"{ClientID}Connect_LoggedOut_NoFacebook_Choose_BackButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:left;\">Back</button>\r\n\r\n\t\t<button id=\"{ClientID}Connect_LoggedOut_NoFacebook_Choose_CancelButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Cancel</button>\r\n\t</p>\r\n</div>\r\n';
        this._addChild(s);
        $addHandler(this._view.get_connect_LoggedOut_NoFacebook_Choose_LoginButton(), 'click', Function.createDelegate(this, function(e) {
            this._showNoFacebookLoginPanel('ChoosePanel', '');
        }));
        $addHandler(this._view.get_connect_LoggedOut_NoFacebook_Choose_SignupButton(), 'click', Function.createDelegate(this, function(e) {
            this._showNoFacebookSignUp1Panel('NoFacebookNewAccount');
        }));
        $addHandler(this._view.get_connect_LoggedOut_NoFacebook_Choose_BackButton(), 'click', Function.createDelegate(this, function(e) {
            this._changePanelOnClick('View.Connect_LoggedOutPanel', e);
        }));
        $addHandler(this._view.get_connect_LoggedOut_NoFacebook_Choose_CancelButton(), 'click', Function.createDelegate(this, this._cancelButtonClick));
        return this._view.get_connect_LoggedOut_NoFacebook_ChoosePanel();
    },
    _add_View_Connect_LoggedOut_NoFacebook_LoginPanel: function SpottedScript_Controls_Navigation_Login_Controller$_add_View_Connect_LoggedOut_NoFacebook_LoginPanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = '\r\n<div id=\"{ClientID}Connect_LoggedOut_NoFacebook_LoginPanel\" class=\"LoginPanel\" style=\"display:none;\">\r\n\t<div style=\"position:relative;\" class=\"LoginPanelInner ClearAfter\">\r\n\t\t<div style=\"width:220px; float:left;\" class=\"ColumnWithNoParaAbove\">\r\n\t\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\t\tLog in\r\n\t\t\t</p>\r\n\t\t\t<p style=\"margin-bottom:-5px;\">\r\n\t\t\t\tNickname or email:\r\n\t\t\t</p>\r\n\t\t\t<p style=\"position:relative; height:25px; line-height:25px;\">\r\n\t\t\t\t<input id=\"{ClientID}Connect_LoggedOut_NoFacebook_Login_UsernameTextbox\" type=\"text\" class=\"xui-state-default ui-corner-all\" style=\"padding-left:5px; height:20px; width:150px;\" />\r\n\t\t\t</p>\r\n\t\t\t<p style=\"margin-bottom:-5px; margin-top:0px;\">\r\n\t\t\t\tPassword:\r\n\t\t\t</p>\r\n\t\t\t<p style=\"position:relative; height:25px; line-height:25px;\">\r\n\t\t\t\t<input id=\"{ClientID}Connect_LoggedOut_NoFacebook_Login_PasswordTextbox\" type=\"password\" class=\"xui-state-default ui-corner-all\" style=\"padding-left:5px; height:20px; width:150px; border:1px solid #cccccc;\" />\r\n\t\t\t</p>\r\n\t\t\t<p>\r\n\t\t\t\t<button id=\"{ClientID}Connect_LoggedOut_NoFacebook_Login_LoginButton\" style=\"float:left;\" class=\"ui-state-default ui-corner-all Pointer BigButton\">Log in</button>\r\n\t\t\t\t<p id=\"{ClientID}Connect_LoggedOut_NoFacebook_Login_Error\" class=\"ForegroundAttentionRed BackgroundWhite\" style=\"display:none; position:absolute; left:65px; float:left; width:400px; height:50px; font-weight:bold; line-height:15px;\">\r\n\t\t\t\t\tCan\'t find those details. Remember to enter your password from Don\'t Stay In, not your Facebook password.\r\n\t\t\t\t</p>\r\n\t\t\t</p>\r\n\t\t</div>\r\n\t\t<div style=\"left:220px; width:10px; height:173px; float:left; border-left:dotted 2px #cccccc;\"> </div>\r\n\t\t<div style=\"left:230px; width:220px; float:left;\" class=\"ColumnWithNoParaAbove\">\r\n\t\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\t\tNo password?\r\n\t\t\t</p>\r\n\t\t\t<p>\r\n\t\t\t\tIf you signed up using Facebook you may not have a password.\r\n\t\t\t</p>\r\n\t\t\t<p>\r\n\t\t\t\t<button id=\"{ClientID}Connect_LoggedOut_NoFacebook_Login_NoPasswordButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\">Get a password</button>\r\n\t\t\t</p>\r\n\t\t</div>\r\n\t</div>\r\n\t<p>\r\n\t\t<button id=\"{ClientID}Connect_LoggedOut_NoFacebook_Login_BackButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:left;\">Back</button>\r\n\r\n\t\t<button id=\"{ClientID}Connect_LoggedOut_NoFacebook_Login_CancelButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Cancel</button>\r\n\t\t<button id=\"{ClientID}Connect_LoggedOut_NoFacebook_Login_ForgottonPasswordButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Forgot your password?</button>\r\n\t</p>\r\n</div>';
        this._addChild(s);
        $addHandler(this._view.get_connect_LoggedOut_NoFacebook_Login_LoginButton(), 'click', Function.createDelegate(this, this._noFacebookLoginButtonClick));
        $addHandler(this._view.get_connect_LoggedOut_NoFacebook_Login_BackButton(), 'click', Function.createDelegate(this, this._noFacebookLoginBackClick));
        $addHandler(this._view.get_connect_LoggedOut_NoFacebook_Login_CancelButton(), 'click', Function.createDelegate(this, this._cancelButtonClick));
        $addHandler(this._view.get_connect_LoggedOut_NoFacebook_Login_NoPasswordButton(), 'click', Function.createDelegate(this, function(e) {
            this._ensurePanelGenerated('View.Connect_LoggedOut_NoFacebook_PasswordResetPanel');
            this._view.get_connect_LoggedOut_NoFacebook_PasswordReset_Title().innerHTML = 'No password?';
            this._changePanelOnClick('View.Connect_LoggedOut_NoFacebook_PasswordResetPanel', e);
        }));
        $addHandler(this._view.get_connect_LoggedOut_NoFacebook_Login_ForgottonPasswordButton(), 'click', Function.createDelegate(this, function(e) {
            this._ensurePanelGenerated('View.Connect_LoggedOut_NoFacebook_PasswordResetPanel');
            this._view.get_connect_LoggedOut_NoFacebook_PasswordReset_Title().innerHTML = 'Forgot your password?';
            this._changePanelOnClick('View.Connect_LoggedOut_NoFacebook_PasswordResetPanel', e);
        }));
        this._defaultButton(this._view.get_connect_LoggedOut_NoFacebook_Login_UsernameTextbox(), this._view.get_connect_LoggedOut_NoFacebook_Login_LoginButton());
        this._defaultButton(this._view.get_connect_LoggedOut_NoFacebook_Login_PasswordTextbox(), this._view.get_connect_LoggedOut_NoFacebook_Login_LoginButton());
        return this._view.get_connect_LoggedOut_NoFacebook_LoginPanel();
    },
    _noFacebookLoginBackClick: function SpottedScript_Controls_Navigation_Login_Controller$_noFacebookLoginBackClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        if (this._noFacebookLoginPanelSource === 'ChoosePanel') {
            this._changePanel('View.Connect_LoggedOut_NoFacebook_ChoosePanel');
        }
        else if (this._noFacebookLoginPanelSource === 'LoggedOutPanel') {
            this._changePanel('View.Connect_LoggedOutPanel');
        }
    },
    _noFacebookLoginButtonClick: function SpottedScript_Controls_Navigation_Login_Controller$_noFacebookLoginButtonClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._noFacebookLogin('NoFacebookLogin', false, false, false);
    },
    _noFacebookLogin: function SpottedScript_Controls_Navigation_Login_Controller$_noFacebookLogin(source, sendDetailsPanelData, sendNoFacebookSignupPanelData, autoLogin) {
        /// <param name="source" type="String">
        /// </param>
        /// <param name="sendDetailsPanelData" type="Boolean">
        /// </param>
        /// <param name="sendNoFacebookSignupPanelData" type="Boolean">
        /// </param>
        /// <param name="autoLogin" type="Boolean">
        /// </param>
        var thisAsyncOperation = this._registerStartAsync('Logging in...');
        this._server.noFacebookLogin((autoLogin) ? '' : this._view.get_connect_LoggedOut_NoFacebook_Login_UsernameTextbox().value, (autoLogin) ? '' : this._view.get_connect_LoggedOut_NoFacebook_Login_PasswordTextbox().value, this._getCaptchaData(), (sendNoFacebookSignupPanelData) ? this._getNoFacebookSignupPanelData() : null, (sendDetailsPanelData) ? this._getDetailsPanelData() : null, autoLogin, ((autoLogin) ? this.autoLoginUsrK : 0), ((autoLogin) ? this.autoLoginUsrLoginString : ''), Function.createDelegate(this, function(response) {
            if (this._registerEndAsync(thisAsyncOperation)) {
                return;
            }
            if (ImportedUtilities.U.isTrue(response, 'Exception')) {
                this._showError(4, 'Internal server error.');
            }
            else {
                this._ensurePanelGenerated('View.Connect_LoggedOut_NoFacebook_LoginPanel');
                this._view.get_connect_LoggedOut_NoFacebook_Login_Error().style.display = 'none';
                if (ImportedUtilities.U.isTrue(response, 'Error')) {
                    if (ImportedUtilities.U.isTrue(response, 'HasNullPassword')) {
                        this._changePanel('View.Connect_LoggedOut_NoFacebook_LoginNoPasswordPanel');
                    }
                    else {
                        this._view.get_connect_LoggedOut_NoFacebook_Login_Error().style.display = '';
                    }
                }
                else {
                    if (ImportedUtilities.U.isTrue(response, 'IsSkeleton')) {
                        this._showNoFacebookSignUp2Panel('NoFacebookLoginSkeleton', ImportedUtilities.U.get(response, 'Details'));
                    }
                    else if (ImportedUtilities.U.isTrue(response, 'NeedsConfirmation')) {
                        this._showDetailsPanel('NoFacebookLoginFacebookNotConfirmed', ImportedUtilities.U.get(response, 'Details'));
                    }
                    else if (ImportedUtilities.U.isTrue(response, 'NeedsCaptcha')) {
                        this._showCaptchaPanel(source, response);
                    }
                    else {
                        this._setAuthCookie(ImportedUtilities.U.get(response, 'AuthCookie'), ImportedUtilities.U.get(response, 'AuthUsr'));
                        jQuery(this._view.get_connectDialog()).dialog('close');
                    }
                }
            }
        }));
    },
    _noFacebookLoginPanelSource: '',
    _showNoFacebookLoginPanel: function SpottedScript_Controls_Navigation_Login_Controller$_showNoFacebookLoginPanel(noFacebookLoginPanelSource, emailPreset) {
        /// <param name="noFacebookLoginPanelSource" type="String">
        /// </param>
        /// <param name="emailPreset" type="String">
        /// </param>
        this._noFacebookLoginPanelSource = noFacebookLoginPanelSource;
        this._ensurePanelGenerated('View.Connect_LoggedOut_NoFacebook_LoginPanel');
        this._view.get_connect_LoggedOut_NoFacebook_Login_UsernameTextbox().value = emailPreset;
        this._view.get_connect_LoggedOut_NoFacebook_Login_PasswordTextbox().value = '';
        this._changePanel('View.Connect_LoggedOut_NoFacebook_LoginPanel');
    },
    _add_View_Connect_LoggedOut_NoFacebook_LoginNoPasswordPanel: function SpottedScript_Controls_Navigation_Login_Controller$_add_View_Connect_LoggedOut_NoFacebook_LoginNoPasswordPanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = '\r\n<div id=\"{ClientID}Connect_LoggedOut_NoFacebook_LoginNoPasswordPanel\" class=\"LoginPanel\" style=\"display:none;\">\r\n\t<div class=\"LoginPanelInner\">\r\n\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\tOops!\r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\tYour account doesn\'t have a password. You probably created it using our Facebook connect feature. We\'ve \r\n\t\t\tsent you an email with a password reset link - with this you\'ll be able to create a password.\r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\t<button id=\"{ClientID}Connect_LoggedOut_NoFacebook_LoginNoPassword_TryAgainButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\">Try again</button>\r\n\t\t</p>\r\n\t</div>\r\n\t<p>\r\n\t\t<button id=\"{ClientID}Connect_LoggedOut_NoFacebook_LoginNoPassword_CancelButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Cancel</button>\r\n\t</p>\r\n</div>\r\n';
        this._addChild(s);
        $addHandler(this._view.get_connect_LoggedOut_NoFacebook_LoginNoPassword_CancelButton(), 'click', Function.createDelegate(this, this._cancelButtonClick));
        $addHandler(this._view.get_connect_LoggedOut_NoFacebook_LoginNoPassword_TryAgainButton(), 'click', Function.createDelegate(this, function(e) {
            this._changePanelOnClick('View.Connect_LoggedOut_NoFacebook_LoginPanel', e);
        }));
        return this._view.get_connect_LoggedOut_NoFacebook_LoginNoPasswordPanel();
    },
    _add_View_Connect_LoggedOut_NoFacebook_PasswordResetPanel: function SpottedScript_Controls_Navigation_Login_Controller$_add_View_Connect_LoggedOut_NoFacebook_PasswordResetPanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = '\r\n<div id=\"{ClientID}Connect_LoggedOut_NoFacebook_PasswordResetPanel\" class=\"LoginPanel\" style=\"display:none;\">\r\n\t<div class=\"LoginPanelInner\">\r\n\t\t<p id=\"{ClientID}Connect_LoggedOut_NoFacebook_PasswordReset_Title\" class=\"LoginPanelTitle\">\r\n\t\t\tDon\'t have a password?\r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\tEnter your Don\'t Stay In username or email below and we\'ll send you a password reset link by email:\r\n\t\t</p>\r\n\t\t<p style=\"position:relative; height:25px; line-height:25px;\">\r\n\t\t\tNickname or email:\r\n\t\t\t<input id=\"{ClientID}Connect_LoggedOut_NoFacebook_PasswordReset_UsernameTextbox\" type=\"text\" class=\"xui-state-default ui-corner-all\" style=\"padding-left:5px; height:20px; left:140px; top:0px; position:absolute; width:150px;\" />\r\n\t\t</p>\r\n\t\t<p style=\"position:relative; height:25px; line-height:25px;\">\r\n\t\t\t<button id=\"{ClientID}Connect_LoggedOut_NoFacebook_PasswordReset_SendLinkButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\" style=\"left:140px; top:0px; position:absolute; float:left;\">Send password reset link</button>\r\n\t\t</p>\r\n\t\t<p style=\"position:relative;\">\r\n\t\t\t<span id=\"{ClientID}Connect_LoggedOut_NoFacebook_PasswordReset_MessageLabel\" class=\"ForegroundAttentionBlue\" style=\"left:140px; top:0px; position:absolute; float:left; font-weight:bold;\"></span>\r\n\t\t\t<span id=\"{ClientID}Connect_LoggedOut_NoFacebook_PasswordReset_ErrorLabel\" class=\"ForegroundAttentionRed\" style=\"left:140px; top:0px; position:absolute; float:left; font-weight:bold;\"></span>\r\n\t\t</p>\r\n\t</div>\r\n\t<p>\r\n\t\t<button id=\"{ClientID}Connect_LoggedOut_NoFacebook_PasswordReset_BackButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:left;\">Back</button>\r\n\t\t<button id=\"{ClientID}Connect_LoggedOut_NoFacebook_PasswordReset_CancelButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Cancel</button>\r\n\t</p>\r\n</div>\r\n';
        this._addChild(s);
        $addHandler(this._view.get_connect_LoggedOut_NoFacebook_PasswordReset_BackButton(), 'click', Function.createDelegate(this, function(e) {
            this._changePanelOnClick('View.Connect_LoggedOut_NoFacebook_LoginPanel', e);
        }));
        $addHandler(this._view.get_connect_LoggedOut_NoFacebook_PasswordReset_CancelButton(), 'click', Function.createDelegate(this, this._cancelButtonClick));
        $addHandler(this._view.get_connect_LoggedOut_NoFacebook_PasswordReset_SendLinkButton(), 'click', Function.createDelegate(this, this._noFacebookNoPasswordSendLinkClick));
        this._defaultButton(this._view.get_connect_LoggedOut_NoFacebook_PasswordReset_UsernameTextbox(), this._view.get_connect_LoggedOut_NoFacebook_PasswordReset_SendLinkButton());
        return this._view.get_connect_LoggedOut_NoFacebook_PasswordResetPanel();
    },
    _noFacebookNoPasswordSendLinkClick: function SpottedScript_Controls_Navigation_Login_Controller$_noFacebookNoPasswordSendLinkClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._view.get_connect_LoggedOut_NoFacebook_PasswordReset_ErrorLabel().style.display = 'none';
        this._view.get_connect_LoggedOut_NoFacebook_PasswordReset_MessageLabel().style.display = 'none';
        if (this._view.get_connect_LoggedOut_NoFacebook_PasswordReset_UsernameTextbox().value.trim().length === 0) {
            this._view.get_connect_LoggedOut_NoFacebook_PasswordReset_ErrorLabel().style.display = '';
            this._view.get_connect_LoggedOut_NoFacebook_PasswordReset_ErrorLabel().innerHTML = 'Please enter your email or nickname.';
            return;
        }
        var thisAsyncOperation = this._registerStartAsync('Sending password reset link...');
        this._server.sendPassword(this._view.get_connect_LoggedOut_NoFacebook_PasswordReset_UsernameTextbox().value, Function.createDelegate(this, function(response) {
            if (this._registerEndAsync(thisAsyncOperation)) {
                return;
            }
            if (ImportedUtilities.U.isTrue(response, 'Exception')) {
                this._showError(12, 'Internal server error.');
            }
            else {
                if (ImportedUtilities.U.isTrue(response, 'Done')) {
                    this._view.get_connect_LoggedOut_NoFacebook_PasswordReset_MessageLabel().style.display = '';
                    this._view.get_connect_LoggedOut_NoFacebook_PasswordReset_MessageLabel().innerHTML = 'We have sent you a password reset email.';
                }
                else {
                    this._view.get_connect_LoggedOut_NoFacebook_PasswordReset_ErrorLabel().style.display = '';
                    this._view.get_connect_LoggedOut_NoFacebook_PasswordReset_ErrorLabel().innerHTML = 'Can\'t find that username or email.';
                }
            }
        }));
    },
    _add_View_Connect_LoggedOut_NoFacebook_SignUp1Panel: function SpottedScript_Controls_Navigation_Login_Controller$_add_View_Connect_LoggedOut_NoFacebook_SignUp1Panel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = '\r\n<div id=\"{ClientID}Connect_LoggedOut_NoFacebook_SignUp1Panel\" class=\"LoginPanel\" style=\"display:none;\">\r\n\t<div class=\"LoginPanelInner\">\r\n\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\tEnter your details...\r\n\t\t</p>\r\n\t\t<p id=\"{ClientID}Connect_LoggedOut_NoFacebook_SignUp1_EmailPara\" style=\"position:relative; height:25px; line-height:25px;\">\r\n\t\t\tEmail:\r\n\t\t\t<input id=\"{ClientID}Connect_LoggedOut_NoFacebook_SignUp1_EmailTextbox\" type=\"text\" style=\"padding-left:5px; height:20px; left:140px; top:0px; position:absolute; width:210px; height:25px; line-height:25px;\" />\r\n\t\t</p>\r\n\t\t<p style=\"position:relative; height:25px; line-height:25px;\">\r\n\t\t\tChoose a password:\r\n\t\t\t<input id=\"{ClientID}Connect_LoggedOut_NoFacebook_SignUp1_Password1Textbox\" type=\"password\" style=\"padding-left:5px; height:20px; left:140px; top:0px; position:absolute; width:210px; height:25px; line-height:25px; border:1px solid #cccccc;\" />\r\n\t\t</p>\r\n\t\t<p style=\"position:relative; height:25px; line-height:25px;\">\r\n\t\t\tConfirm your password:\r\n\t\t\t<input id=\"{ClientID}Connect_LoggedOut_NoFacebook_SignUp1_Password2Textbox\" type=\"password\" style=\"padding-left:5px; height:20px; left:140px; top:0px; position:absolute; width:210px; height:25px; line-height:25px; border:1px solid #cccccc;\" />\r\n\t\t</p>\r\n\t\t<p style=\"position:relative; height:25px; line-height:25px;\">\r\n\t\t\t<button id=\"{ClientID}Connect_LoggedOut_NoFacebook_SignUp1_SaveButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\" style=\"left:140px; top:0px; position:absolute; width:50px; \">Save</button>\r\n\t\t\t<span id=\"{ClientID}Connect_LoggedOut_NoFacebook_SignUp1_ErrorLabel\" class=\"ForegroundAttentionRed\" style=\"left:200px; position:absolute; font-weight:bold; top:7px;\"></span>\r\n\t\t</p>\r\n\t</div>\r\n\t<p style=\"position:relative;\">\r\n\t\t<button id=\"{ClientID}Connect_LoggedOut_NoFacebook_SignUp1_BackButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:left; position:absolute; left:0px;\">Back</button>\r\n\r\n\t\t<button id=\"{ClientID}Connect_LoggedOut_NoFacebook_SignUp1_CancelButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Cancel</button>\r\n\t</p>\r\n</div>\r\n';
        this._addChild(s);
        $addHandler(this._view.get_connect_LoggedOut_NoFacebook_SignUp1_CancelButton(), 'click', Function.createDelegate(this, this._cancelButtonClick));
        $addHandler(this._view.get_connect_LoggedOut_NoFacebook_SignUp1_BackButton(), 'click', Function.createDelegate(this, function(e) {
            this._changePanelOnClick('View.Connect_LoggedOut_NoFacebook_ChoosePanel', e);
        }));
        $addHandler(this._view.get_connect_LoggedOut_NoFacebook_SignUp1_SaveButton(), 'click', Function.createDelegate(this, this._noFacebookSignup1SaveClick));
        this._defaultButton(this._view.get_connect_LoggedOut_NoFacebook_SignUp1_EmailTextbox(), this._view.get_connect_LoggedOut_NoFacebook_SignUp1_SaveButton());
        this._defaultButton(this._view.get_connect_LoggedOut_NoFacebook_SignUp1_Password1Textbox(), this._view.get_connect_LoggedOut_NoFacebook_SignUp1_SaveButton());
        this._defaultButton(this._view.get_connect_LoggedOut_NoFacebook_SignUp1_Password2Textbox(), this._view.get_connect_LoggedOut_NoFacebook_SignUp1_SaveButton());
        return this._view.get_connect_LoggedOut_NoFacebook_SignUp1Panel();
    },
    _noFacebookSignup1SaveClick: function SpottedScript_Controls_Navigation_Login_Controller$_noFacebookSignup1SaveClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._view.get_connect_LoggedOut_NoFacebook_SignUp1_ErrorLabel().innerHTML = '';
        if (this._noFacebookSignUp1PanelSource === 'NoFacebookNewAccount') {
            if (this._view.get_connect_LoggedOut_NoFacebook_SignUp1_EmailTextbox().value.length === 0) {
                this._view.get_connect_LoggedOut_NoFacebook_SignUp1_ErrorLabel().innerHTML = 'Enter an email';
                return;
            }
            var emailRegex = new RegExp('^[^\\@\\s]+\\@[A-Za-z0-9\\-]{1}[.A-Za-z0-9\\-]+\\.[.A-Za-z0-9\\-]*[A-Za-z0-9]$', 'g');
            if (!emailRegex.test(this._view.get_connect_LoggedOut_NoFacebook_SignUp1_EmailTextbox().value)) {
                this._view.get_connect_LoggedOut_NoFacebook_SignUp1_ErrorLabel().innerHTML = 'Check your email';
                return;
            }
        }
        if (this._view.get_connect_LoggedOut_NoFacebook_SignUp1_Password1Textbox().value.length === 0) {
            this._view.get_connect_LoggedOut_NoFacebook_SignUp1_ErrorLabel().innerHTML = 'Enter a password';
            return;
        }
        if (this._view.get_connect_LoggedOut_NoFacebook_SignUp1_Password1Textbox().value.length < 4) {
            this._view.get_connect_LoggedOut_NoFacebook_SignUp1_ErrorLabel().innerHTML = 'Password is too short';
            return;
        }
        if (this._view.get_connect_LoggedOut_NoFacebook_SignUp1_Password1Textbox().value !== this._view.get_connect_LoggedOut_NoFacebook_SignUp1_Password2Textbox().value) {
            this._view.get_connect_LoggedOut_NoFacebook_SignUp1_ErrorLabel().innerHTML = 'Passwords don\'t match';
            return;
        }
        if (this._noFacebookSignUp1PanelSource === 'NoFacebookNewAccount') {
            var thisAsyncOperation = this._registerStartAsync('Checking email...');
            this._server.checkEmail(this._view.get_connect_LoggedOut_NoFacebook_SignUp1_EmailTextbox().value, Function.createDelegate(this, function(response) {
                if (this._registerEndAsync(thisAsyncOperation)) {
                    return;
                }
                if (ImportedUtilities.U.isTrue(response, 'Exception')) {
                    this._showError(7, 'Internal server error');
                }
                else {
                    var emailFound = ImportedUtilities.U.get(response, 'Found');
                    if (emailFound) {
                        var thisAsyncOperationSendPassword = this._registerStartAsync('Checking email...');
                        this._server.sendPassword(this._view.get_connect_LoggedOut_NoFacebook_SignUp1_EmailTextbox().value, Function.createDelegate(this, function(responseSendPassword) {
                            if (this._registerEndAsync(thisAsyncOperationSendPassword)) {
                                return;
                            }
                            if (ImportedUtilities.U.isTrue(responseSendPassword, 'Exception')) {
                                this._showError(12, 'Internal server error.');
                            }
                            else {
                                if (ImportedUtilities.U.isTrue(responseSendPassword, 'Done')) {
                                    this._view.get_connect_LoggedOut_NoFacebook_SignUp1_ErrorLabel().innerHTML = 'This email is already in our database. We\'ve sent you a password reset email.';
                                }
                                else {
                                    this._view.get_connect_LoggedOut_NoFacebook_SignUp1_ErrorLabel().innerHTML = 'This email is already in our database.';
                                }
                            }
                        }));
                    }
                    else {
                        this._showNoFacebookSignUp2Panel('NoFacebookNewAccount', null);
                    }
                }
            }));
        }
        else if (this._noFacebookSignUp1PanelSource === 'AutoLoginNoFacebookSkeleton') {
            var details = {};
            details['UsrK'] = this.autoLoginUsrK;
            details['Nickname'] = this._autoLoginNickname;
            this._showNoFacebookSignUp2Panel('AutoLoginNoFacebookSkeleton', details);
        }
    },
    _noFacebookSignup1BackClick: function SpottedScript_Controls_Navigation_Login_Controller$_noFacebookSignup1BackClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        if (this._noFacebookSignUp1PanelSource === 'NoFacebookNewAccount') {
            this._changePanel('View.Connect_LoggedOut_NoFacebook_ChoosePanel');
        }
        else if (this._noFacebookSignUp1PanelSource === 'AutoLoginNoFacebookSkeleton') {
            this._changePanel('View.Connect_LoggedOutPanel');
        }
    },
    _showNoFacebookSignUp1Panel: function SpottedScript_Controls_Navigation_Login_Controller$_showNoFacebookSignUp1Panel(source) {
        /// <param name="source" type="String">
        /// </param>
        this._noFacebookSignUp1PanelSource = source;
        this._ensurePanelGenerated('View.Connect_LoggedOut_NoFacebook_SignUp1Panel');
        this._view.get_connect_LoggedOut_NoFacebook_SignUp1_EmailPara().style.display = '';
        if (this._noFacebookSignUp1PanelSource === 'AutoLoginNoFacebookSkeleton') {
            this._view.get_connect_LoggedOut_NoFacebook_SignUp1_EmailPara().style.display = 'none';
        }
        this._changePanel('View.Connect_LoggedOut_NoFacebook_SignUp1Panel');
    },
    _add_View_Connect_LoggedOut_NoFacebook_SignUp2Panel: function SpottedScript_Controls_Navigation_Login_Controller$_add_View_Connect_LoggedOut_NoFacebook_SignUp2Panel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = '\r\n<div id=\"{ClientID}Connect_LoggedOut_NoFacebook_SignUp2Panel\" class=\"LoginPanel\" style=\"display:none;\">\r\n\t<div class=\"LoginPanelInner\">\r\n\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\tEnter your details...\r\n\t\t</p>\r\n\t\t<p style=\"position:relative; height:25px; line-height:25px;\">\r\n\t\t\tReal name:\r\n\t\t\t<input id=\"{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextbox\" type=\"text\" style=\"padding-left:5px; height:20px; left:140px; top:0px; position:absolute; width:100px; height:25px; line-height:25px;\" />\r\n\t\t\t<input id=\"{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_LastNameTextbox\" type=\"text\" style=\"padding-left:5px; height:20px; left:250px; top:0px; position:absolute; width:100px; height:25px; line-height:25px;\" />\r\n\t\t</p>\r\n\t\t<p style=\"position:relative; height:25px; line-height:25px;\">\r\n\t\t\tNickname:\r\n\t\t\t<input id=\"{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox\" type=\"text\" style=\"padding-left:5px; height:20px; left:140px; top:0px; position:absolute; width:210px; height:25px; line-height:25px;\" />\r\n\t\t</p>\r\n\t\t<p style=\"position:relative; height:25px; line-height:25px;\">\r\n\t\t\tDate of birth:\r\n\t\t\t<span style=\"left:140px; top:0px; position:absolute;\">\r\n\t\t\t\t<select id=\"{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDown\" class=\"xui-state-default ui-corner-all\" style=\"padding-left:5px; height:25px; line-height:25px;\"></select>\r\n\t\t\t\t<select id=\"{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDown\" class=\"xui-state-default ui-corner-all\" style=\"padding-left:5px; height:25px; line-height:25px;\"></select>\r\n\t\t\t\t<select id=\"{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDown\" class=\"xui-state-default ui-corner-all\" style=\"padding-left:5px; height:25px; line-height:25px;\"></select>\r\n\t\t\t</span>\r\n\t\t</p>\r\n\t\t<p style=\"position:relative; height:25px; line-height:25px;\">\r\n\t\t\tSex:\r\n\t\t\t<span style=\"left:140px; top:0px; position:absolute;\">\r\n\t\t\t\t<input id=\"{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_SexMaleRadio\" type=\"radio\" name=\"{ClientID}Connect_LoggedOut_NoFacebook_SignUp1_Sex\" />\r\n\t\t\t\t<label for=\"{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_SexMaleRadio\"> Male</label>\r\n\t\t\t\t<input id=\"{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_SexFemaleRadio\" type=\"radio\" name=\"{ClientID}Connect_LoggedOut_NoFacebook_SignUp1_Sex\" />\r\n\t\t\t\t<label for=\"{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_SexFemaleRadio\"> Female</label>\r\n\t\t\t</span>\r\n\t\t</p>\r\n\t\t<p style=\"position:relative; height:25px; line-height:25px;\">\r\n\t\t\t<button id=\"{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_SaveButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\" style=\"left:140px; top:0px; position:absolute; width:50px;\">Save</button>\r\n\t\t\t<span id=\"{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel\" class=\"ForegroundAttentionRed\" style=\"left:200px; position:absolute; font-weight:bold; top:7px;\"></span>\r\n\t\t</p>\r\n\t</div>\r\n\t<p style=\"position:relative;\">\r\n\t\t<button id=\"{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_BackButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:left; position:absolute; left:0px;\">Back</button>\r\n\r\n\t\t<button id=\"{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_CancelButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Cancel</button>\r\n\t</p>\r\n</div>\r\n';
        this._addChild(s);
        $addHandler(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextbox(), 'keyup', Function.createDelegate(this, this._noFacebookSignup2NameKeyUp));
        $addHandler(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_LastNameTextbox(), 'keyup', Function.createDelegate(this, this._noFacebookSignup2NameKeyUp));
        $addHandler(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox(), 'keyup', Function.createDelegate(this, this._noFacebookSignup2NicknameKeyUp));
        $addHandler(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_CancelButton(), 'click', Function.createDelegate(this, this._cancelButtonClick));
        $addHandler(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_BackButton(), 'click', Function.createDelegate(this, this._noFacebookSignup2BackClick));
        $addHandler(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_SaveButton(), 'click', Function.createDelegate(this, this._noFacebookSignup2SaveClick));
        if (this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDown().options.length === 0) {
            this._addOption('-1', '', this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDown());
            for (var i = 1; i <= 31; i++) {
                this._addOption(i.toString(), i.toString(), this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDown());
            }
        }
        if (this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDown().options.length === 0) {
            this._addOption('-1', '', this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDown());
            var months = [ 'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec' ];
            for (var i = 0; i < months.length; i++) {
                this._addOption(i.toString(), months[i], this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDown());
            }
        }
        if (this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDown().options.length === 0) {
            this._addOption('-1', '', this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDown());
            var t = new Date();
            var year = t.getFullYear();
            for (var i = year; i > year - 100; i--) {
                this._addOption(i.toString(), i.toString(), this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDown());
            }
        }
        this._defaultButton(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextbox(), this._view.get_connect_LoggedOut_NoFacebook_SignUp2_SaveButton());
        this._defaultButton(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_LastNameTextbox(), this._view.get_connect_LoggedOut_NoFacebook_SignUp2_SaveButton());
        this._defaultButton(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox(), this._view.get_connect_LoggedOut_NoFacebook_SignUp2_SaveButton());
        return this._view.get_connect_LoggedOut_NoFacebook_SignUp2Panel();
    },
    _noFacebookSignup2BackClick: function SpottedScript_Controls_Navigation_Login_Controller$_noFacebookSignup2BackClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        if (this._noFacebookSignUp2PanelSource === 'NoFacebookNewAccount') {
            this._changePanel('View.Connect_LoggedOut_NoFacebook_SignUp1Panel');
        }
        else if (this._noFacebookSignUp2PanelSource === 'NoFacebookLoginSkeleton') {
            this._changePanel('View.Connect_LoggedOut_NoFacebook_LoginPanel');
        }
        else if (this._noFacebookSignUp2PanelSource === 'AutoLoginNoFacebookSkeleton') {
            this._changePanel('View.Connect_LoggedOut_NoFacebook_SignUp1Panel');
        }
    },
    _previousNicknameTest: '',
    _noFacebookSignup2NameKeyUp: function SpottedScript_Controls_Navigation_Login_Controller$_noFacebookSignup2NameKeyUp(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        if ((!this._noFacebookSignup2NicknameHasEntry || this._view.get_connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox().value === '') && this._view.get_connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextbox().value.length > 0 && this._view.get_connect_LoggedOut_NoFacebook_SignUp2_LastNameTextbox().value.length > 0) {
            var nickTest = this._view.get_connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextbox().value + '-' + this._view.get_connect_LoggedOut_NoFacebook_SignUp2_LastNameTextbox().value;
            if (this._previousNicknameTest !== nickTest) {
                this._previousNicknameTest = nickTest;
                var thisAsyncOperation = this._registerStartAsync('Suggesting nickname...');
                this._server.getUniqueNickName(nickTest, this._noFacebookSignUp2PanelLoginUsrK, Function.createDelegate(this, function(response) {
                    if (this._registerEndAsync(thisAsyncOperation)) {
                        return;
                    }
                    if (ImportedUtilities.U.isTrue(response, 'Exception')) {
                    }
                    else {
                        var newNickname = ImportedUtilities.U.get(response, 'Nickname').toString();
                        this._view.get_connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox().value = newNickname;
                    }
                }));
            }
        }
    },
    _noFacebookSignup2NicknameHasEntry: false,
    _noFacebookSignup2NicknameKeyUp: function SpottedScript_Controls_Navigation_Login_Controller$_noFacebookSignup2NicknameKeyUp(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        this._noFacebookSignup2NicknameHasEntry = true;
    },
    _noFacebookSignup2SaveClick: function SpottedScript_Controls_Navigation_Login_Controller$_noFacebookSignup2SaveClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._view.get_connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel().innerHTML = '';
        if (this._view.get_connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextbox().value.length === 0) {
            this._view.get_connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel().innerHTML = 'Enter your first name';
            return;
        }
        if (this._view.get_connect_LoggedOut_NoFacebook_SignUp2_LastNameTextbox().value.length === 0) {
            this._view.get_connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel().innerHTML = 'Enter your last name';
            return;
        }
        if (this._view.get_connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextbox().value.length > 20) {
            this._view.get_connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel().innerHTML = 'First name is too long';
            return;
        }
        if (this._view.get_connect_LoggedOut_NoFacebook_SignUp2_LastNameTextbox().value.length > 20) {
            this._view.get_connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel().innerHTML = 'Last name is too long';
            return;
        }
        if (this._view.get_connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox().value.length === 0) {
            this._view.get_connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel().innerHTML = 'Enter a nickname';
            return;
        }
        if (this._view.get_connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox().value.length < 2) {
            this._view.get_connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel().innerHTML = 'Nickname must be longer';
            return;
        }
        if (this._view.get_connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox().value.length > 20) {
            this._view.get_connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel().innerHTML = 'Nickname is too long';
            return;
        }
        if (Number.parseInvariant(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDown().value) < 0 || Number.parseInvariant(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDown().value) < 0 || Number.parseInvariant(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDown().value) < 0) {
            this._view.get_connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel().innerHTML = 'Enter your date of birth';
            return;
        }
        var d = new Date();
        d.setFullYear(Number.parseInvariant(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDown().value));
        d.setMonth(Number.parseInvariant(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDown().value));
        d.setDate(Number.parseInvariant(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDown().value));
        if (d.getFullYear() !== Number.parseInvariant(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDown().value) || d.getMonth() !== Number.parseInvariant(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDown().value) || d.getDate() !== Number.parseInvariant(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDown().value)) {
            this._view.get_connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel().innerHTML = 'Check your date of birth';
            return;
        }
        if (!this._view.get_connect_LoggedOut_NoFacebook_SignUp2_SexMaleRadio().checked && !this._view.get_connect_LoggedOut_NoFacebook_SignUp2_SexFemaleRadio().checked) {
            this._view.get_connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel().innerHTML = 'Enter your sex';
            return;
        }
        var thisAsyncOperation = this._registerStartAsync('Checking nickname...');
        this._server.getUniqueNickName(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox().value, this._noFacebookSignUp2PanelLoginUsrK, Function.createDelegate(this, function(response) {
            if (this._registerEndAsync(thisAsyncOperation)) {
                return;
            }
            if (ImportedUtilities.U.isTrue(response, 'Exception')) {
                this._showError(7, 'Internal server error');
            }
            else {
                var newNickname = ImportedUtilities.U.get(response, 'Nickname').toString();
                if (this._view.get_connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox().value !== newNickname) {
                    this._view.get_connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox().value = newNickname;
                    this._view.get_connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel().innerHTML = 'We changed your nickname. Is this OK?';
                    return;
                }
                else {
                    this._noFacebookSignup2SaveDone();
                }
            }
        }));
    },
    _noFacebookSignup2SaveDone: function SpottedScript_Controls_Navigation_Login_Controller$_noFacebookSignup2SaveDone() {
        this._showDetailsPanel(this._noFacebookSignUp2PanelSource, null);
    },
    _getNoFacebookSignupPanelData: function SpottedScript_Controls_Navigation_Login_Controller$_getNoFacebookSignupPanelData() {
        /// <returns type="Object"></returns>
        var ret = {};
        ret['Email'] = this._view.get_connect_LoggedOut_NoFacebook_SignUp1_EmailTextbox().value;
        ret['Password'] = this._view.get_connect_LoggedOut_NoFacebook_SignUp1_Password1Textbox().value;
        ret['FirstName'] = this._view.get_connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextbox().value;
        ret['LastName'] = this._view.get_connect_LoggedOut_NoFacebook_SignUp2_LastNameTextbox().value;
        ret['Nickname'] = this._view.get_connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox().value;
        ret['DateOfBirthYear'] = Number.parseInvariant(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDown().value);
        ret['DateOfBirthMonth'] = Number.parseInvariant(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDown().value);
        ret['DateOfBirthDay'] = Number.parseInvariant(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDown().value);
        ret['SexMale'] = this._view.get_connect_LoggedOut_NoFacebook_SignUp2_SexMaleRadio().checked;
        return ret;
    },
    _noFacebookNewAccount: function SpottedScript_Controls_Navigation_Login_Controller$_noFacebookNewAccount() {
        var thisAsyncOperation = this._registerStartAsync('Creating account...');
        this._server.noFacebookNewAccount(this._getNoFacebookSignupPanelData(), this._getDetailsPanelData(), this._getCaptchaData(), Function.createDelegate(this, function(response) {
            if (this._registerEndAsync(thisAsyncOperation)) {
                return;
            }
            if (ImportedUtilities.U.isTrue(response, 'Exception')) {
                this._showError(8, 'Internal server error (' + ImportedUtilities.U.get(response, 'Message').toString() + ').');
            }
            else {
                if (ImportedUtilities.U.isTrue(response, 'NeedsCaptcha')) {
                    this._showCaptchaPanel('NoFacebookNewAccount', response);
                }
                else {
                    this._setAuthCookie(ImportedUtilities.U.get(response, 'AuthCookie'), ImportedUtilities.U.get(response, 'AuthUsr'));
                    this._showLikeButtonPanel();
                }
            }
        }));
    },
    _showNoFacebookSignUp2Panel: function SpottedScript_Controls_Navigation_Login_Controller$_showNoFacebookSignUp2Panel(source, details) {
        /// <param name="source" type="String">
        /// </param>
        /// <param name="details" type="Object">
        /// </param>
        this._noFacebookSignUp2PanelSource = source;
        this._ensurePanelGenerated('View.Connect_LoggedOut_NoFacebook_SignUp2Panel');
        this._noFacebookSignUp2PanelLoginUsrK = 0;
        if (details != null) {
            this._noFacebookSignUp2PanelLoginUsrK = ImportedUtilities.U.get(details, 'UsrK');
            var nickname = ImportedUtilities.U.get(details, 'Nickname').toString();
            if (nickname.length > 0) {
                this._noFacebookSignup2NicknameHasEntry = true;
                this._view.get_connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox().value = nickname;
            }
        }
        this._changePanel('View.Connect_LoggedOut_NoFacebook_SignUp2Panel');
    },
    _add_View_Connect_ConnectingPanel: function SpottedScript_Controls_Navigation_Login_Controller$_add_View_Connect_ConnectingPanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = '\r\n<div id=\"{ClientID}Connect_ConnectingPanel\" class=\"LoginPanel\" style=\"display:none;\">\r\n\t<div class=\"LoginPanelInner\">\r\n\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\tConnecting...\r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\tYou should see a Facebook pop-up. Please log in using your Facebook account. \r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\tIf you don\'t see the pop-up, click the button below:\r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\t<button id=\"{ClientID}Connect_Connecting_PopupRetry\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:left;\">Re-open popup</button>\r\n\t\t</p>\r\n\t</div>\r\n\t<p>\r\n\t\t<button id=\"{ClientID}Connect_Connecting_BackButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:left;\">Back</button>\r\n\t\t<button id=\"{ClientID}Connect_Connecting_CancelButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Cancel</button>\r\n\t</p>\r\n</div>\r\n';
        this._addChild(s);
        $addHandler(this._view.get_connect_Connecting_PopupRetry(), 'click', Function.createDelegate(this, this._connectingRetryPopupClick));
        $addHandler(this._view.get_connect_Connecting_CancelButton(), 'click', Function.createDelegate(this, this._connectingCancelButtonClick));
        $addHandler(this._view.get_connect_Connecting_BackButton(), 'click', Function.createDelegate(this, this._connectingBackButtonClick));
        return this._view.get_connect_ConnectingPanel();
    },
    _connectingCancelButtonClick: function SpottedScript_Controls_Navigation_Login_Controller$_connectingCancelButtonClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        this._cancelledAsyncOperations[this._connectButtonClickAsyncOperation.toString()] = true;
        this._asyncInProgress = false;
        jQuery(this._view.get_connectDialog()).dialog('close');
    },
    _connectingRetryPopupClick: function SpottedScript_Controls_Navigation_Login_Controller$_connectingRetryPopupClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        this._cancelledAsyncOperations[this._connectButtonClickAsyncOperation.toString()] = true;
        this._asyncInProgress = false;
        this._connectButtonClickInternal();
    },
    _connectingBackButtonClick: function SpottedScript_Controls_Navigation_Login_Controller$_connectingBackButtonClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        this._cancelledAsyncOperations[this._connectButtonClickAsyncOperation.toString()] = true;
        this._asyncInProgress = false;
        this._changePanel('View.Connect_LoggedOutPanel');
    },
    _add_View_Connect_NewAccount_NoEmailMatchPanel: function SpottedScript_Controls_Navigation_Login_Controller$_add_View_Connect_NewAccount_NoEmailMatchPanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = '\r\n<div id=\"{ClientID}Connect_NewAccount_NoEmailMatchPanel\" class=\"LoginPanel\" style=\"display:none;\">\r\n\t<div style=\"position:relative;\" class=\"LoginPanelInner ClearAfter\">\r\n\t\t<p>\r\n\t\t\tWe\'ve not seen this Facebook account before...\r\n\t\t</p>\r\n\t\t<div style=\"width:220px; float:left;\">\r\n\t\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\t\tNew user\r\n\t\t\t</p>\r\n\t\t\t<p>\r\n\t\t\t\tIf you\'ve never used Don\'t Stay In before:\r\n\t\t\t</p>\r\n\t\t\t<p>\r\n\t\t\t\t<button id=\"{ClientID}Connect_NewAccount_NoEmailMatch_NewAccountButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\">I\'m new to Don\'t Stay In</button>\r\n\t\t\t</p>\r\n\t\t</div>\r\n\t\t<div style=\"left:220px; width:10px; height:150px; float:left; border-left:dotted 2px #cccccc;\"> </div>\r\n\t\t<div style=\"left:230px; width:220px; float:left;\">\r\n\t\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\t\tExisting user\r\n\t\t\t</p>\r\n\t\t\t<p>\r\n\t\t\t\tIf you\'ve logged in to Don\'t Stay In before:\r\n\t\t\t</p>\r\n\t\t\t<p>\r\n\t\t\t\t<button id=\"{ClientID}Connect_NewAccount_NoEmailMatch_ChooseAccountButton\"  class=\"ui-state-default ui-corner-all Pointer BigButton\">Link to my Don\'t Stay In...</button>\r\n\t\t\t</p>\r\n\t\t</div>\r\n\t</div>\r\n\t<p>\r\n\t\t<button id=\"{ClientID}Connect_NewAccount_NoEmailMatch_BackButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:left;\">Back</button>\r\n\r\n\t\t<button id=\"{ClientID}Connect_NewAccount_NoEmailMatch_CancelButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Cancel</button>\r\n\t\t<button id=\"{ClientID}Connect_NewAccount_NoEmailMatch_FacebookLogoutButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Log out of Facebook</button>\r\n\t</p>\r\n</div>\r\n';
        this._addChild(s);
        $addHandler(this._view.get_connect_NewAccount_NoEmailMatch_CancelButton(), 'click', Function.createDelegate(this, this._cancelButtonClick));
        $addHandler(this._view.get_connect_NewAccount_NoEmailMatch_FacebookLogoutButton(), 'click', Function.createDelegate(this, this._logoutOfFacebookButtonClick));
        $addHandler(this._view.get_connect_NewAccount_NoEmailMatch_BackButton(), 'click', Function.createDelegate(this, function(e) {
            this._changePanelOnClick('View.Connect_NewAccount_ConfirmFacebookPanel', e);
        }));
        $addHandler(this._view.get_connect_NewAccount_NoEmailMatch_ChooseAccountButton(), 'click', Function.createDelegate(this, function(e) {
            this._changePanelOnClick('View.Connect_NewAccount_ChooseAccountPanel', e);
        }));
        $addHandler(this._view.get_connect_NewAccount_NoEmailMatch_NewAccountButton(), 'click', Function.createDelegate(this, this._newAccountButtonClick));
        return this._view.get_connect_NewAccount_NoEmailMatchPanel();
    },
    _newAccountButtonClick: function SpottedScript_Controls_Navigation_Login_Controller$_newAccountButtonClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        if (this._facebookEmailMatch) {
            this._autoConnectClickInternal();
        }
        else {
            this._newAccountButtonClickInternal();
        }
    },
    _newAccountButtonClickInternal: function SpottedScript_Controls_Navigation_Login_Controller$_newAccountButtonClickInternal() {
        var thisAsyncOperation = this._registerStartAsync('Creating new account...');
        this._server.getHomePlaceFromFacebook(Function.createDelegate(this, function(response) {
            if (this._registerEndAsync(thisAsyncOperation)) {
                return;
            }
            if (ImportedUtilities.U.isTrue(response, 'Exception')) {
                this._showError(7, 'Internal server error');
            }
            else {
                this._showDetailsPanel('NewAccount', response);
            }
        }));
    },
    _newAccount: function SpottedScript_Controls_Navigation_Login_Controller$_newAccount() {
        var thisAsyncOperation = this._registerStartAsync('Creating new account...');
        this._server.createNewAccount(this._getDetailsPanelData(), Function.createDelegate(this, function(response) {
            if (this._registerEndAsync(thisAsyncOperation)) {
                return;
            }
            if (ImportedUtilities.U.isTrue(response, 'Exception')) {
                this._showError(7, 'Internal server error');
            }
            else {
                if (ImportedUtilities.U.isTrue(response, 'NeedsConfirmation')) {
                    this._showDetailsPanel('NewAccount', ImportedUtilities.U.get(response, 'Details'));
                }
                else {
                    this._setAuthCookie(ImportedUtilities.U.get(response, 'AuthCookie'), ImportedUtilities.U.get(response, 'AuthUsr'));
                    this._detectAutoLoginProblem(true);
                }
            }
        }));
    },
    _add_View_Connect_NewAccount_EmailMatchPanel: function SpottedScript_Controls_Navigation_Login_Controller$_add_View_Connect_NewAccount_EmailMatchPanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = '\r\n<div id=\"{ClientID}Connect_NewAccount_EmailMatchPanel\" class=\"LoginPanel\" style=\"display:none;\">\r\n\t<div style=\"position:relative;\" class=\"LoginPanelInner ClearAfter\">\r\n\t\t<p>\r\n\t\t\tThere\'s a Don\'t Stay In account with your Facebook email...\r\n\t\t</p>\r\n\t\t<div style=\"width:220px; float:left;\">\r\n\t\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\t\tLink my accounts\r\n\t\t\t</p>\r\n\t\t\t<p>\r\n\t\t\t\t<span id=\"{ClientID}Connect_NewAccount_EmailMatch_UserLink1\"></span>\r\n\t\t\t</p>\r\n\t\t\t<p>\r\n\t\t\t\t<button id=\"{ClientID}Connect_NewAccount_EmailMatch_AutoConnectButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\">Link to this account</button>\r\n\t\t\t</p>\r\n\t\t</div>\r\n\t\t<div style=\"left:220px; width:10px; height:150px; float:left; border-left:dotted 2px #cccccc;\"> </div>\r\n\t\t<div style=\"left:230px; width:220px; float:left;\">\r\n\t\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\t\tOther options\r\n\t\t\t</p>\r\n\t\t\t<p>\r\n\t\t\t\tIf this isn\'t the right account:\r\n\t\t\t</p>\r\n\t\t\t<p>\r\n\t\t\t\t<button id=\"{ClientID}Connect_NewAccount_EmailMatch_ChooseAccountButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\">Choose another account</button>\r\n\t\t\t</p>\r\n\t\t</div>\r\n\t</div>\r\n\t<p>\r\n\t\t<button id=\"{ClientID}Connect_NewAccount_EmailMatch_BackButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:left;\">Back</button>\r\n\r\n\t\t<button id=\"{ClientID}Connect_NewAccount_EmailMatch_CancelButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Cancel</button>\r\n\t\t<button id=\"{ClientID}Connect_NewAccount_EmailMatch_FacebookLogoutButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Log out of Facebook</button>\r\n\t</p>\r\n</div>\r\n';
        this._addChild(s);
        $addHandler(this._view.get_connect_NewAccount_EmailMatch_CancelButton(), 'click', Function.createDelegate(this, this._cancelButtonClick));
        $addHandler(this._view.get_connect_NewAccount_EmailMatch_FacebookLogoutButton(), 'click', Function.createDelegate(this, this._logoutOfFacebookButtonClick));
        $addHandler(this._view.get_connect_NewAccount_EmailMatch_BackButton(), 'click', Function.createDelegate(this, function(e) {
            this._changePanelOnClick('View.Connect_NewAccount_ConfirmFacebookPanel', e);
        }));
        $addHandler(this._view.get_connect_NewAccount_EmailMatch_ChooseAccountButton(), 'click', Function.createDelegate(this, function(e) {
            this._changePanelOnClick('View.Connect_NewAccount_ChooseAccountPanel', e);
        }));
        $addHandler(this._view.get_connect_NewAccount_EmailMatch_AutoConnectButton(), 'click', Function.createDelegate(this, this._autoConnectClick));
        return this._view.get_connect_NewAccount_EmailMatchPanel();
    },
    _autoConnectClick: function SpottedScript_Controls_Navigation_Login_Controller$_autoConnectClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._autoConnectClickInternal();
    },
    _autoConnectClickInternal: function SpottedScript_Controls_Navigation_Login_Controller$_autoConnectClickInternal() {
        if (this._facebookEmailMatchEnhancedSecurity) {
            this._ensurePanelGenerated('View.Connect_NewAccount_ChooseAccountPanel');
            this._view.get_connect_NewAccount_ChooseAccount_UsernameTextbox().value = this._facebookEmailMatchNickName;
            this._changePanel('View.Connect_NewAccount_ChooseAccountPanel');
        }
        else {
            this._autoConnect(false);
        }
    },
    _autoConnect: function SpottedScript_Controls_Navigation_Login_Controller$_autoConnect(sendDetailsPanelData) {
        /// <param name="sendDetailsPanelData" type="Boolean">
        /// </param>
        var thisAsyncOperation = this._registerStartAsync('Linking accounts...');
        this._server.autoLinkByEmail((sendDetailsPanelData) ? this._getDetailsPanelData() : null, Function.createDelegate(this, function(response) {
            if (this._registerEndAsync(thisAsyncOperation)) {
                return;
            }
            if (ImportedUtilities.U.isTrue(response, 'Exception')) {
                this._showError(2, 'Internal server error');
            }
            else {
                if (ImportedUtilities.U.isTrue(response, 'FacebookEmailMatch')) {
                    if (ImportedUtilities.U.isTrue(response, 'NeedsConfirmation')) {
                        this._showDetailsPanel('AutoConnect', ImportedUtilities.U.get(response, 'Details'));
                    }
                    else {
                        this._setAuthCookie(ImportedUtilities.U.get(response, 'AuthCookie'), ImportedUtilities.U.get(response, 'AuthUsr'));
                        this._detectAutoLoginProblem(true);
                    }
                }
                else {
                    this._showError(3, 'Can\'t find account by email.');
                }
            }
        }));
    },
    _add_View_Connect_NewAccount_ChooseAccountPanel: function SpottedScript_Controls_Navigation_Login_Controller$_add_View_Connect_NewAccount_ChooseAccountPanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = '\r\n<div id=\"{ClientID}Connect_NewAccount_ChooseAccountPanel\" class=\"LoginPanel\" style=\"display:none;\">\r\n\t<div class=\"LoginPanelInner\">\r\n\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\tDon\'t Stay In details\r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\tEnter your Don\'t Stay In details below to link with your Facebook:\r\n\t\t</p>\r\n\t\t<p style=\"position:relative; height:25px; line-height:25px;\">\r\n\t\t\tNickname or email:\r\n\t\t\t<input id=\"{ClientID}Connect_NewAccount_ChooseAccount_UsernameTextbox\" type=\"text\" class=\"xui-state-default ui-corner-all\" style=\"padding-left:5px; height:20px; left:140px; top:0px; position:absolute; width:150px;\" />\r\n\t\t</p>\r\n\t\t<p style=\"position:relative; height:25px; line-height:25px;\">\r\n\t\t\tPassword:\r\n\t\t\t<input id=\"{ClientID}Connect_NewAccount_ChooseAccount_PasswordTextbox\" type=\"password\" class=\"xui-state-default ui-corner-all\" style=\"padding-left:5px; height:20px; left:140px; top:0px; position:absolute; width:150px; border:1px solid #cccccc;\" />\r\n\t\t</p>\r\n\t\t<p style=\"position:relative; height:30px; line-height:30px;\">\r\n\t\t\t<button id=\"{ClientID}Connect_NewAccount_ChooseAccount_LinkAccountButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\" style=\"left:140px; top:0px; position:absolute; float:left;\">Link my account</button>\r\n\t\t</p>\r\n\t\t<p style=\"position:relative;\">\r\n\t\t\t<span id=\"{ClientID}Connect_NewAccount_ChooseAccount_ErrorLabel\" class=\"ForegroundAttentionRed\" style=\"left:140px; top:0px; position:absolute; float:left; font-weight:bold;\"></span>\r\n\t\t</p>\r\n\t</div>\r\n\t<p>\r\n\t\t<button id=\"{ClientID}Connect_NewAccount_ChooseAccount_BackButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:left;\">Back</button>\r\n\r\n\t\t<button id=\"{ClientID}Connect_NewAccount_ChooseAccount_CancelButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Cancel</button>\r\n\t\t<button id=\"{ClientID}Connect_NewAccount_ChooseAccount_FacebookLogoutButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Log out of Facebook</button>\r\n\t\t<button id=\"{ClientID}Connect_NewAccount_ChooseAccount_ForgottonPasswordButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Forgot your password?</button>\r\n\t</p>\r\n</div>\r\n';
        this._addChild(s);
        $addHandler(this._view.get_connect_NewAccount_ChooseAccount_BackButton(), 'click', Function.createDelegate(this, this._chooseAccountBackButtonClick));
        $addHandler(this._view.get_connect_NewAccount_ChooseAccount_CancelButton(), 'click', Function.createDelegate(this, this._cancelButtonClick));
        $addHandler(this._view.get_connect_NewAccount_ChooseAccount_FacebookLogoutButton(), 'click', Function.createDelegate(this, this._logoutOfFacebookButtonClick));
        $addHandler(this._view.get_connect_NewAccount_ChooseAccount_LinkAccountButton(), 'click', Function.createDelegate(this, this._linkAccountClick));
        $addHandler(this._view.get_connect_NewAccount_ChooseAccount_ForgottonPasswordButton(), 'click', Function.createDelegate(this, function(e) {
            this._changePanelOnClick('View.Connect_NewAccount_ForgotPasswordPanel', e);
        }));
        this._defaultButton(this._view.get_connect_NewAccount_ChooseAccount_UsernameTextbox(), this._view.get_connect_NewAccount_ChooseAccount_LinkAccountButton());
        this._defaultButton(this._view.get_connect_NewAccount_ChooseAccount_PasswordTextbox(), this._view.get_connect_NewAccount_ChooseAccount_LinkAccountButton());
        return this._view.get_connect_NewAccount_ChooseAccountPanel();
    },
    _linkAccountClick: function SpottedScript_Controls_Navigation_Login_Controller$_linkAccountClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._linkAccount(false);
    },
    _linkAccount: function SpottedScript_Controls_Navigation_Login_Controller$_linkAccount(sendDetailsPanelData) {
        /// <param name="sendDetailsPanelData" type="Boolean">
        /// </param>
        var thisAsyncOperation = this._registerStartAsync('Linking accounts...');
        this._server.linkAccounts(this._view.get_connect_NewAccount_ChooseAccount_UsernameTextbox().value, this._view.get_connect_NewAccount_ChooseAccount_PasswordTextbox().value, (sendDetailsPanelData) ? this._getDetailsPanelData() : null, Function.createDelegate(this, function(response) {
            if (this._registerEndAsync(thisAsyncOperation)) {
                return;
            }
            if (ImportedUtilities.U.isTrue(response, 'Exception')) {
                this._showError(4, 'Internal server error.');
            }
            else {
                this._view.get_connect_NewAccount_ChooseAccount_ErrorLabel().style.display = 'none';
                if (ImportedUtilities.U.isTrue(response, 'Error')) {
                    this._view.get_connect_NewAccount_ChooseAccount_ErrorLabel().style.display = '';
                    this._view.get_connect_NewAccount_ChooseAccount_ErrorLabel().innerHTML = 'Can\'t find a user with those details.';
                }
                else {
                    if (ImportedUtilities.U.isTrue(response, 'NeedsConfirmation')) {
                        this._showDetailsPanel('LinkAccount', ImportedUtilities.U.get(response, 'Details'));
                    }
                    else {
                        this._setAuthCookie(ImportedUtilities.U.get(response, 'AuthCookie'), ImportedUtilities.U.get(response, 'AuthUsr'));
                        this._detectAutoLoginProblem(true);
                    }
                }
            }
        }));
    },
    _chooseAccountBackButtonClick: function SpottedScript_Controls_Navigation_Login_Controller$_chooseAccountBackButtonClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._changePanel((this._facebookEmailMatchToCurrentUser) ? 'View.Connect_NewAccount_EmailMatchPanel' : 'View.Connect_NewAccount_NoEmailMatchPanel');
    },
    _add_View_Connect_NewAccount_ForgotPasswordPanel: function SpottedScript_Controls_Navigation_Login_Controller$_add_View_Connect_NewAccount_ForgotPasswordPanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = '\r\n<div id=\"{ClientID}Connect_NewAccount_ForgotPasswordPanel\" class=\"LoginPanel\" style=\"display:none;\">\r\n\t<div class=\"LoginPanelInner\">\r\n\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\tForgot your password?\r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\tEnter your Don\'t Stay In username or email below and we\'ll send you a password reset link by email:\r\n\t\t</p>\r\n\t\t<p style=\"position:relative; height:25px; line-height:25px;\">\r\n\t\t\tNickname or email:\r\n\t\t\t<input id=\"{ClientID}Connect_NewAccount_ForgotPassword_UsernameTextbox\" type=\"text\" class=\"xui-state-default ui-corner-all\" style=\"padding-left:5px; height:20px; left:140px; top:0px; position:absolute; width:150px;\" />\r\n\t\t</p>\r\n\t\t<p style=\"position:relative; height:30px; line-height:30px;\">\r\n\t\t\t<button id=\"{ClientID}Connect_NewAccount_ForgotPassword_SendLinkButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\" style=\"left:140px; top:0px; position:absolute; float:left;\">Send password reset link</button>\r\n\t\t</p>\r\n\t\t<p style=\"position:relative;\">\r\n\t\t\t<span id=\"{ClientID}Connect_NewAccount_ForgotPassword_MessageLabel\" class=\"ForegroundAttentionBlue\" style=\"left:140px; top:0px; position:absolute; float:left; font-weight:bold;\"></span>\r\n\t\t\t<span id=\"{ClientID}Connect_NewAccount_ForgotPassword_ErrorLabel\" class=\"ForegroundAttentionRed\" style=\"left:140px; top:0px; position:absolute; float:left; font-weight:bold;\"></span>\r\n\t\t</p>\r\n\t</div>\r\n\t<p>\r\n\t\t<button id=\"{ClientID}Connect_NewAccount_ForgotPassword_BackButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:left;\">Back</button>\r\n\r\n\t\t<button id=\"{ClientID}Connect_NewAccount_ForgotPassword_CancelButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Cancel</button>\r\n\t\t<button id=\"{ClientID}Connect_NewAccount_ForgotPassword_FacebookLogoutButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Log out of Facebook</button>\r\n\t</p>\r\n</div>\r\n';
        this._addChild(s);
        $addHandler(this._view.get_connect_NewAccount_ForgotPassword_BackButton(), 'click', Function.createDelegate(this, function(e) {
            this._changePanelOnClick('View.Connect_NewAccount_ChooseAccountPanel', e);
        }));
        $addHandler(this._view.get_connect_NewAccount_ForgotPassword_CancelButton(), 'click', Function.createDelegate(this, this._cancelButtonClick));
        $addHandler(this._view.get_connect_NewAccount_ForgotPassword_FacebookLogoutButton(), 'click', Function.createDelegate(this, this._logoutOfFacebookButtonClick));
        $addHandler(this._view.get_connect_NewAccount_ForgotPassword_SendLinkButton(), 'click', Function.createDelegate(this, this._forgotPasswordSendLinkClick));
        this._defaultButton(this._view.get_connect_NewAccount_ForgotPassword_UsernameTextbox(), this._view.get_connect_NewAccount_ForgotPassword_SendLinkButton());
        return this._view.get_connect_NewAccount_ForgotPasswordPanel();
    },
    _forgotPasswordSendLinkClick: function SpottedScript_Controls_Navigation_Login_Controller$_forgotPasswordSendLinkClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._view.get_connect_NewAccount_ForgotPassword_ErrorLabel().style.display = 'none';
        this._view.get_connect_NewAccount_ForgotPassword_MessageLabel().style.display = 'none';
        if (this._view.get_connect_NewAccount_ForgotPassword_UsernameTextbox().value.trim().length === 0) {
            this._view.get_connect_NewAccount_ForgotPassword_ErrorLabel().style.display = '';
            this._view.get_connect_NewAccount_ForgotPassword_ErrorLabel().innerHTML = 'Please enter your email or nickname.';
            return;
        }
        var thisAsyncOperation = this._registerStartAsync('Sending password reset link...');
        this._server.sendPassword(this._view.get_connect_NewAccount_ForgotPassword_UsernameTextbox().value, Function.createDelegate(this, function(response) {
            if (this._registerEndAsync(thisAsyncOperation)) {
                return;
            }
            if (ImportedUtilities.U.isTrue(response, 'Exception')) {
                this._showError(12, 'Internal server error.');
            }
            else {
                if (ImportedUtilities.U.isTrue(response, 'Done')) {
                    this._view.get_connect_NewAccount_ForgotPassword_MessageLabel().style.display = '';
                    this._view.get_connect_NewAccount_ForgotPassword_MessageLabel().innerHTML = 'We have sent you a password reset email.';
                }
                else {
                    this._view.get_connect_NewAccount_ForgotPassword_ErrorLabel().style.display = '';
                    this._view.get_connect_NewAccount_ForgotPassword_ErrorLabel().innerHTML = 'Can\'t find that username or email.';
                }
            }
        }));
    },
    _add_View_Connect_NewAccount_ConfirmFacebookPanel: function SpottedScript_Controls_Navigation_Login_Controller$_add_View_Connect_NewAccount_ConfirmFacebookPanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = '\r\n<div id=\"{ClientID}Connect_NewAccount_ConfirmFacebookPanel\" class=\"LoginPanel\" style=\"display:none;\">\r\n\t<div class=\"LoginPanelInner\">\r\n\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\tIs this you?\r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\tWe need to confirm we\'ve got the right Facebook account...\r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\t<img src=\"\" width=\"50\" height=\"50\" border=\"0\" id=\"{ClientID}Connect_NewAccount_ConfirmFacebook_Image\" align=\"absmiddle\" />\r\n\t\t\t<a href=\"\" id=\"{ClientID}Connect_NewAccount_ConfirmFacebook_Link\" target=\"_blank\"></a>\r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\t<button id=\"{ClientID}Connect_NewAccount_ConfirmFacebook_YesButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\">Yes, this is me</button>\r\n\t\t\t<button id=\"{ClientID}Connect_NewAccount_ConfirmFacebook_NoButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\">Nope, not me</button>\r\n\t\t</p>\r\n<p>\r\n</p>\r\n\t</div>\r\n\t<p>\r\n\t\t<button id=\"{ClientID}Connect_NewAccount_ConfirmFacebook_BackButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:left;\">Back</button>\r\n\t\t<button id=\"{ClientID}Connect_NewAccount_ConfirmFacebook_CancelButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Cancel</button>\r\n\t</p>\r\n</div>\r\n';
        this._addChild(s);
        $addHandler(this._view.get_connect_NewAccount_ConfirmFacebook_BackButton(), 'click', Function.createDelegate(this, function(e) {
            this._changePanelOnClick('View.Connect_LoggedOutPanel', e);
        }));
        $addHandler(this._view.get_connect_NewAccount_ConfirmFacebook_YesButton(), 'click', Function.createDelegate(this, this._confirmFacebookAccountYesClick));
        $addHandler(this._view.get_connect_NewAccount_ConfirmFacebook_NoButton(), 'click', Function.createDelegate(this, this._confirmFacebookAccountNoClick));
        $addHandler(this._view.get_connect_NewAccount_ConfirmFacebook_CancelButton(), 'click', Function.createDelegate(this, this._cancelButtonClick));
        return this._view.get_connect_NewAccount_ConfirmFacebookPanel();
    },
    _showConfirmFacebookPanel: function SpottedScript_Controls_Navigation_Login_Controller$_showConfirmFacebookPanel() {
        var thisAsyncOperation1 = this._registerStartAsync('Loading...');
        FB.api('/me', Function.createDelegate(this, function(meResponse) {
            if (this._registerEndAsync(thisAsyncOperation1)) {
                return;
            }
            jQuery(this._view.get_connectDialog()).dialog('open');
            this._ensurePanelGenerated('View.Connect_NewAccount_ConfirmFacebookPanel');
            this._view.get_connect_NewAccount_ConfirmFacebook_Link().innerHTML = ImportedUtilities.U.get(meResponse, 'name').toString();
            this._view.get_connect_NewAccount_ConfirmFacebook_Link().href = ImportedUtilities.U.get(meResponse, 'link').toString();
            this._view.get_connect_NewAccount_ConfirmFacebook_Image().src = 'http://graph.facebook.com/' + ImportedUtilities.U.get(meResponse, 'id').toString() + '/picture';
            this._changePanel('View.Connect_NewAccount_ConfirmFacebookPanel');
        }));
    },
    _confirmFacebookAccountYesClick: function SpottedScript_Controls_Navigation_Login_Controller$_confirmFacebookAccountYesClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._facebookAccountNeedsConfirmationBecauseInitiallyFacebookLoggedIn = false;
        this._facebookAccountNeedsConfirmationBecauseInitiallyFacebookConnectedAndSiteLoggedOut = false;
        this._facebookAccountConfirmationStepDone = true;
        this._configureFormConnected();
    },
    _confirmFacebookAccountNoClick: function SpottedScript_Controls_Navigation_Login_Controller$_confirmFacebookAccountNoClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._facebookAccountConfirmationStepDone = true;
        this._logoutNow(false, null, true);
    },
    _add_View_Connect_DetailsPanel: function SpottedScript_Controls_Navigation_Login_Controller$_add_View_Connect_DetailsPanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = '\r\n<div id=\"{ClientID}Connect_DetailsPanel\" class=\"LoginPanel\" style=\"display:none;\">\r\n\t<div class=\"LoginPanelInner\">\r\n\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\tJust to confirm...\r\n\t\t</p>\r\n\t\t<p style=\"position:relative; height:25px; line-height:25px;\">\r\n\t\t\tFavourite music:\r\n\t\t\t<select id=\"{ClientID}Connect_Details_MusicDropDown\" class=\"xui-state-default ui-corner-all\" style=\"padding-left:5px; height:20px; left:140px; top:0px; position:absolute; width:300px;  height:25px; line-height:25px;\">\r\n\t\t\t\t<option value=\"1\">I like all music</option>\r\n\t\t\t\t<option value=\"42\">Commercial (pop, chart dance, club classics etc...)</option>\r\n\t\t\t\t<option value=\"4\">House (funky house, soulful house, deep house etc...)</option>\r\n\t\t\t\t<option value=\"10\">Hard Dance (hard house, trance, hardcore etc...)</option>\r\n\t\t\t\t<option value=\"15\">Alternative Dance (breaks, electro, big beat etc...)</option>\r\n\t\t\t\t<option value=\"20\">Techno (electro techno, detroit techno etc...)</option>\r\n\t\t\t\t<option value=\"24\">Drum and Bass (drum\'n\'bass, jungle etc.)</option>\r\n\t\t\t\t<option value=\"28\">Urban (hip-hop, R&amp;B, garage etc...)</option>\r\n\t\t\t\t<option value=\"65\">Alternative Electronic (industrial, ebm, powernoise etc.)</option>\r\n\t\t\t\t<option value=\"46\">Retro (disco, soul, jazz, funk etc...)</option>\r\n\t\t\t\t<option value=\"35\">Chillout / Leftfield</option>\r\n\t\t\t\t<option value=\"36\">Rock (indie, rock, metal etc...)</option>\r\n\t\t\t</select>\r\n\t\t</p>\r\n\t\t<p style=\"position:relative; height:25px; line-height:25px;\">\r\n\t\t\tNearest town: \r\n\t\t\t<select id=\"{ClientID}Connect_Details_CountryDropDown\" class=\"ui-corner-all\" style=\"padding-left:5px; height:20px; left:140px; top:0px; position:absolute; width:100px; height:25px; line-height:25px; display:none;\">\r\n\t\t\t\t<option value=\"0\">Countries</option>\r\n\t\t\t</select>\r\n\t\t\t<select id=\"{ClientID}Connect_Details_PlaceDropDown\" class=\"ui-corner-all\" style=\"padding-left:5px; height:20px; left:250px; top:0px; position:absolute; width:190px; height:25px; line-height:25px; display:none;\">\r\n\t\t\t\t<option value=\"0\">Towns</option>\r\n\t\t\t</select>\r\n\t\t\t<span id=\"{ClientID}Connect_Details_PlaceDefaultOuterSpan\" style=\"height:25px; line-height:25px; left:140px; top:0px; position:absolute; width:300px;\">\r\n\t\t\t\t<span id=\"{ClientID}Connect_Details_PlaceDefaultSpan\"></span>\r\n\t\t\t\t<a id=\"{ClientID}Connect_Details_PlaceChangeLink\" href=\"\">change</a>\r\n\t\t\t</span>\r\n\t\t</p>\r\n\t\t<div style=\"position:relative;\">\r\n\t\t\t<p id=\"{ClientID}Connect_Details_FacebookInfoPanel\" class=\"ui-state-highlight ui-corner-all\" style=\"position:absolute; top:-10px; padding:5px; left:300px; width:150px; display:none;\">\r\n\t\t\t\tWe\'ll update your Facebook wall when you create stuff.\r\n\t\t\t</p>\r\n\t\t\t<p id=\"{ClientID}Connect_Details_WeeklyEmailInfoPanel\" class=\"ui-state-highlight ui-corner-all\" style=\"position:absolute; top:-10px; padding:5px; left:300px; width:150px; display:none;\">\r\n\t\t\t\tWe\'ll send you a weekly summary of events in your area playing your favourite music.\r\n\t\t\t</p>\r\n\t\t\t<p id=\"{ClientID}Connect_Details_PartyInvitesInfoPanel\" class=\"ui-state-highlight ui-corner-all\" style=\"position:absolute; top:6px; padding:5px; left:300px; width:150px; display:none;\">\r\n\t\t\t\tWe\'ll send you guestlist offers, e-flyers and party invites.\r\n\t\t\t</p>\r\n\t\t</div>\r\n\t\t<p style=\"margin-left:140px; margin-top:0px; margin-bottom:2px; \">\r\n\t\t\t<input id=\"{ClientID}Connect_Details_FacebookCheck\" type=\"checkbox\" checked=\"checked\" />\r\n\t\t\t<label for=\"{ClientID}Connect_Details_FacebookCheck\"> Facebook updates</label>\r\n\t\t\t<a id=\"{ClientID}Connect_Details_FacebookInfoAnchor\" href=\"\">info</a>\r\n\t\t</p>\r\n\t\t<p style=\"margin-left:140px; margin-top:0px; margin-bottom:2px;\">\r\n\t\t\t<input id=\"{ClientID}Connect_Details_WeeklyEmailCheck\" type=\"checkbox\" checked=\"checked\" />\r\n\t\t\t<label for=\"{ClientID}Connect_Details_WeeklyEmailCheck\"> Weekly email</label>\r\n\t\t\t<a id=\"{ClientID}Connect_Details_WeeklyEmailInfoAnchor\" href=\"\">info</a>\r\n\t\t</p>\r\n\t\t<p style=\"margin-left:140px; margin-top:0px;\">\r\n\t\t\t<input id=\"{ClientID}Connect_Details_PartyInvitesCheck\" type=\"checkbox\" checked=\"checked\" />\r\n\t\t\t<label for=\"{ClientID}Connect_Details_PartyInvitesCheck\"> Party invites</label>\r\n\t\t\t<a id=\"{ClientID}Connect_Details_PartyInvitesInfoAnchor\" href=\"\">info</a>\r\n\t\t</p>\r\n\t\t<p style=\"margin-left:140px; position:relative;\">\r\n\t\t\t<button id=\"{ClientID}Connect_Details_SaveButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\" style=\"float:left;\">Save</button>\r\n\t\t\t<p id=\"{ClientID}Connect_Details_PlaceErrorSpan\" class=\"ForegroundAttentionRed\" style=\"font-weight:bold; display:none; padding-top:7px;\">&nbsp;Please select a town.</p>\r\n\t\t</p>\r\n\t</div>\r\n\t<p style=\"position:relative;\">\r\n\t\t<button id=\"{ClientID}Connect_Details_BackButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:left; position:absolute; left:0px;\">Back</button>\r\n\r\n\t\t<button id=\"{ClientID}Connect_Details_CancelButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Cancel</button>\r\n\t</p>\r\n</div>\r\n';
        this._addChild(s);
        $addHandler(this._view.get_connect_Details_CountryDropDown(), 'change', Function.createDelegate(this, this._detailsCountryDropDownChange));
        $addHandler(this._view.get_connect_Details_PlaceDropDown(), 'change', Function.createDelegate(this, this._detailsPlaceDropDownChange));
        $addHandler(this._view.get_connect_Details_PlaceChangeLink(), 'click', Function.createDelegate(this, this._detailsPlaceChangeLinkClick));
        $addHandler(this._view.get_connect_Details_SaveButton(), 'click', Function.createDelegate(this, this._detailsPanelSaveClick));
        $addHandler(this._view.get_connect_Details_CancelButton(), 'click', Function.createDelegate(this, this._cancelButtonClick));
        $addHandler(this._view.get_connect_Details_BackButton(), 'click', Function.createDelegate(this, this._detailsPanelBackClick));
        $addHandler(this._view.get_connect_Details_FacebookInfoAnchor(), 'click', Function.createDelegate(this, this._detailsFacebookInfoAnchorClick));
        $addHandler(this._view.get_connect_Details_WeeklyEmailInfoAnchor(), 'click', Function.createDelegate(this, this._detailsWeeklyEmailInfoAnchorClick));
        $addHandler(this._view.get_connect_Details_PartyInvitesInfoAnchor(), 'click', Function.createDelegate(this, this._detailsPartyInvitesInfoAnchorClick));
        this._detailsCountryDropDownJ = jQuery(this._view.get_connect_Details_CountryDropDown());
        this._detailsPlaceDropDownJ = jQuery(this._view.get_connect_Details_PlaceDropDown());
        return this._view.get_connect_DetailsPanel();
    },
    _showDetailsPanel: function SpottedScript_Controls_Navigation_Login_Controller$_showDetailsPanel(source, details) {
        /// <param name="source" type="String">
        /// </param>
        /// <param name="details" type="Object">
        /// </param>
        this._detailsShowSource = source;
        this._ensurePanelGenerated('View.Connect_DetailsPanel');
        var homePlace = null;
        if (details != null) {
            homePlace = ImportedUtilities.U.get(details, 'HomePlace');
            this._detailsDefaultPlaceK = ImportedUtilities.U.get(homePlace, 'PlaceK');
            this._detailsDefaultCountryK = ImportedUtilities.U.get(homePlace, 'CountryK');
            this._detailsDefaultPlaceGoodMatch = ImportedUtilities.U.get(homePlace, 'GoodMatch');
            if (ImportedUtilities.U.exists(details, 'FavouriteMusicK')) {
                this._view.get_connect_Details_MusicDropDown().value = ImportedUtilities.U.get(details, 'FavouriteMusicK');
            }
            if (ImportedUtilities.U.exists(details, 'SendSpottedEmails')) {
                this._view.get_connect_Details_WeeklyEmailCheck().checked = ImportedUtilities.U.isTrue(details, 'SendSpottedEmails');
            }
            if (ImportedUtilities.U.exists(details, 'SendEflyers')) {
                this._view.get_connect_Details_PartyInvitesCheck().checked = ImportedUtilities.U.isTrue(details, 'SendEflyers');
            }
        }
        else {
            var countryKFromIp = 224;
            try {
                countryKFromIp = Number.parseInvariant(this._getStringFromBasePage('CountryKFromIp'));
            }
            catch ($e1) {
            }
            this._detailsDefaultCountryK = countryKFromIp;
            this._detailsDefaultPlaceGoodMatch = false;
        }
        this._view.get_connect_Details_BackButton().style.display = (source === 'AutoLinkByAutoLoginUsr') ? 'none' : '';
        this._view.get_connect_Details_PlaceErrorSpan().style.display = 'none';
        this._view.get_connect_Details_FacebookInfoPanel().style.display = 'none';
        this._view.get_connect_Details_WeeklyEmailInfoPanel().style.display = 'none';
        this._view.get_connect_Details_PartyInvitesInfoPanel().style.display = 'none';
        if (this._detailsDefaultPlaceGoodMatch) {
            this._view.get_connect_Details_PlaceDefaultOuterSpan().style.display = '';
            this._view.get_connect_Details_CountryDropDown().style.display = 'none';
            this._view.get_connect_Details_PlaceDropDown().style.display = 'none';
            this._view.get_connect_Details_PlaceDefaultSpan().innerHTML = ImportedUtilities.U.get(homePlace, 'PlaceName') + ', ' + ImportedUtilities.U.get(homePlace, 'CountryName');
            this._changePanel('View.Connect_DetailsPanel');
        }
        else {
            this._detailsDropDownsUpdate(true);
        }
    },
    _detailsCountryDropDownChange: function SpottedScript_Controls_Navigation_Login_Controller$_detailsCountryDropDownChange(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        var k = this._getK(this._view.get_connect_Details_CountryDropDown());
        if (k > 0) {
            this._detailsCountrySelectedK = k;
            this._detailsCountryPreviouslySelectedIndex = this._getIndex(this._view.get_connect_Details_CountryDropDown());
        }
        else {
            this._setIndex(this._view.get_connect_Details_CountryDropDown(), this._detailsCountryPreviouslySelectedIndex);
        }
        this._detailsDropDownsUpdate(false);
    },
    _detailsPlaceDropDownChange: function SpottedScript_Controls_Navigation_Login_Controller$_detailsPlaceDropDownChange(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        var k = this._getK(this._view.get_connect_Details_PlaceDropDown());
        if (k > 0) {
            this._detailsPlaceSelectedK = k;
            this._detailsPlacePreviouslySelectedIndex = this._getIndex(this._view.get_connect_Details_PlaceDropDown());
        }
        else {
            this._setIndex(this._view.get_connect_Details_PlaceDropDown(), this._detailsPlacePreviouslySelectedIndex);
        }
    },
    _detailsPlaceChangeLinkClick: function SpottedScript_Controls_Navigation_Login_Controller$_detailsPlaceChangeLinkClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._detailsDropDownsUpdate(true);
    },
    _detailsDropDownsUpdate: function SpottedScript_Controls_Navigation_Login_Controller$_detailsDropDownsUpdate(resetSelection) {
        /// <param name="resetSelection" type="Boolean">
        /// </param>
        var firingAsync = false;
        if (!this._detailsCountryDropDownPopulated) {
            firingAsync = true;
            this._detailsCountryDropDownJ.ajaxAddOption('/support/getcached.aspx?type=country', null, false, Function.createDelegate(this, function() {
                this._detailsCountryDropDownPopulated = true;
                this._setK(this._view.get_connect_Details_CountryDropDown(), this._detailsDefaultCountryK);
                this._detailsCountrySelectedK = this._detailsDefaultCountryK;
                this._detailsCountryPreviouslySelectedIndex = this._getIndex(this._view.get_connect_Details_CountryDropDown());
                if (this._detailsPlaceDropDownPopulated && this._detailsCountrySelectedK === this._detailsPlaceDropDownPopulatedCountryK) {
                    this._showPlaceDropDownsAndDetailsPanel();
                }
            }), null);
        }
        if (resetSelection || !this._detailsPlaceDropDownPopulated || this._detailsCountrySelectedK !== this._detailsPlaceDropDownPopulatedCountryK) {
            if (resetSelection) {
                this._setK(this._view.get_connect_Details_CountryDropDown(), this._detailsDefaultCountryK);
            }
            firingAsync = true;
            var countryK = (resetSelection) ? this._detailsDefaultCountryK : (this._detailsCountrySelectedK > 0) ? this._detailsCountrySelectedK : this._detailsDefaultCountryK;
            var thisAsyncOperation = this._registerStartAsync('Loading towns...');
            this._detailsPlaceDropDownJ.ajaxAddOption('/support/getcached.aspx?type=place&countryk=' + countryK + '&return=k', null, false, Function.createDelegate(this, function() {
                if (this._registerEndAsync(thisAsyncOperation)) {
                    return;
                }
                this._detailsPlaceDropDownPopulated = true;
                this._detailsPlaceDropDownPopulatedCountryK = countryK;
                if (countryK === this._detailsDefaultCountryK) {
                    this._setK(this._view.get_connect_Details_PlaceDropDown(), this._detailsDefaultPlaceK);
                    this._detailsPlaceSelectedK = this._detailsDefaultPlaceK;
                    this._detailsPlacePreviouslySelectedIndex = this._getIndex(this._view.get_connect_Details_PlaceDropDown());
                }
                else {
                    this._detailsPlaceSelectedK = 0;
                    this._detailsPlacePreviouslySelectedIndex = 0;
                }
                if (this._detailsCountryDropDownPopulated) {
                    this._showPlaceDropDownsAndDetailsPanel();
                }
            }), null);
        }
        if (!firingAsync) {
            this._showPlaceDropDownsAndDetailsPanel();
        }
    },
    _showPlaceDropDownsAndDetailsPanel: function SpottedScript_Controls_Navigation_Login_Controller$_showPlaceDropDownsAndDetailsPanel() {
        this._detailsPlaceDropDownVisible = true;
        this._view.get_connect_Details_PlaceDefaultOuterSpan().style.display = 'none';
        this._view.get_connect_Details_CountryDropDown().style.display = '';
        this._view.get_connect_Details_PlaceDropDown().style.display = '';
        this._changePanel('View.Connect_DetailsPanel');
    },
    _getK: function SpottedScript_Controls_Navigation_Login_Controller$_getK(sel) {
        /// <param name="sel" type="Object" domElement="true">
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        try {
            var value = (sel.options[sel.selectedIndex]).value;
            value = value.substr(5, value.length - 5);
            return Number.parseInvariant(value);
        }
        catch ($e1) {
            return 0;
        }
    },
    _setK: function SpottedScript_Controls_Navigation_Login_Controller$_setK(sel, value) {
        /// <param name="sel" type="Object" domElement="true">
        /// </param>
        /// <param name="value" type="Number" integer="true">
        /// </param>
        /// <returns type="Boolean"></returns>
        for (var i = 0; i < sel.options.length; i++) {
            var op = sel.options[i];
            if (op.value.substr(5, op.value.length - 5).toLowerCase() === value.toString().toLowerCase()) {
                op.selected = true;
                return true;
            }
        }
        return false;
    },
    _getIndex: function SpottedScript_Controls_Navigation_Login_Controller$_getIndex(sel) {
        /// <param name="sel" type="Object" domElement="true">
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        return sel.selectedIndex;
    },
    _setIndex: function SpottedScript_Controls_Navigation_Login_Controller$_setIndex(sel, index) {
        /// <param name="sel" type="Object" domElement="true">
        /// </param>
        /// <param name="index" type="Number" integer="true">
        /// </param>
        try {
            var op = (index > sel.options.length - 1) ? sel.options[sel.options.length - 1] : sel.options[index];
            op.selected = true;
        }
        catch ($e1) {
        }
    },
    _detailsPanelBackClick: function SpottedScript_Controls_Navigation_Login_Controller$_detailsPanelBackClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        if (this._detailsShowSource === 'AutoLinkByAutoLoginUsr') {
        }
        else if (this._detailsShowSource === 'AutoConnect') {
            this._changePanel('View.Connect_NewAccount_EmailMatchPanel');
        }
        else if (this._detailsShowSource === 'LinkAccount') {
            this._changePanel('View.Connect_NewAccount_ChooseAccountPanel');
        }
        else if (this._detailsShowSource === 'NewAccount') {
            this._changePanel('View.Connect_NewAccount_NoEmailMatchPanel');
        }
        else if (this._detailsShowSource === 'SwitchAccounts') {
            this._changePanel('View.Connect_AutoLoginMismatchPanel');
        }
        else if (this._detailsShowSource === 'NoFacebookNewAccount') {
            this._changePanel('View.Connect_LoggedOut_NoFacebook_SignUp2Panel');
        }
        else if (this._detailsShowSource === 'NoFacebookLoginFacebookNotConfirmed') {
            this._changePanel('View.Connect_LoggedOut_NoFacebook_LoginPanel');
        }
        else if (this._detailsShowSource === 'NoFacebookLoginSkeleton' || this._detailsShowSource === 'AutoLoginNoFacebookSkeleton') {
            this._changePanel('View.Connect_LoggedOut_NoFacebook_SignUp2Panel');
        }
        else if (this._detailsShowSource === 'AutoLoginNoFacebookFacebookNotConfirmed') {
            this._changePanel('View.Connect_LoggedOutPanel');
        }
    },
    _detailsPanelSaveClick: function SpottedScript_Controls_Navigation_Login_Controller$_detailsPanelSaveClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._view.get_connect_Details_PlaceErrorSpan().style.display = 'none';
        this._view.get_connect_Details_FacebookInfoPanel().style.display = 'none';
        this._view.get_connect_Details_WeeklyEmailInfoPanel().style.display = 'none';
        this._view.get_connect_Details_PartyInvitesInfoPanel().style.display = 'none';
        if (this._detailsPlaceDropDownVisible && this._detailsPlaceSelectedK === 0) {
            this._view.get_connect_Details_PlaceErrorSpan().style.display = '';
            return;
        }
        if (this._detailsShowSource === 'AutoLinkByAutoLoginUsr') {
            this.autoLinkByAutoLoginUsr(true);
        }
        else if (this._detailsShowSource === 'AutoConnect') {
            this._autoConnect(true);
        }
        else if (this._detailsShowSource === 'LinkAccount') {
            this._linkAccount(true);
        }
        else if (this._detailsShowSource === 'NewAccount') {
            this._newAccount();
        }
        else if (this._detailsShowSource === 'SwitchAccounts') {
            this._switchAccounts(true);
        }
        else if (this._detailsShowSource === 'NoFacebookNewAccount') {
            this._noFacebookNewAccount();
        }
        else if (this._detailsShowSource === 'NoFacebookLoginFacebookNotConfirmed') {
            this._noFacebookLogin('NoFacebookLoginFacebookNotConfirmed', true, false, false);
        }
        else if (this._detailsShowSource === 'NoFacebookLoginSkeleton') {
            this._noFacebookLogin('NoFacebookLoginSkeleton', true, true, false);
        }
        else if (this._detailsShowSource === 'AutoLoginNoFacebookSkeleton') {
            this._noFacebookLogin('AutoLoginNoFacebookSkeleton', true, true, true);
        }
        else if (this._detailsShowSource === 'AutoLoginNoFacebookFacebookNotConfirmed') {
            this._noFacebookLogin('AutoLoginNoFacebookFacebookNotConfirmed', true, false, true);
        }
    },
    _getDetailsPanelData: function SpottedScript_Controls_Navigation_Login_Controller$_getDetailsPanelData() {
        /// <returns type="Object"></returns>
        var detailsPanelData = {};
        detailsPanelData['MusicTypeK'] = Number.parseInvariant((this._view.get_connect_Details_MusicDropDown().options[this._view.get_connect_Details_MusicDropDown().selectedIndex]).value);
        detailsPanelData['PlaceK'] = (this._detailsPlaceDropDownVisible) ? this._detailsPlaceSelectedK : this._detailsDefaultPlaceK;
        detailsPanelData['Facebook'] = this._view.get_connect_Details_FacebookCheck().checked;
        detailsPanelData['WeeklyEmail'] = this._view.get_connect_Details_WeeklyEmailCheck().checked;
        detailsPanelData['PartyInvites'] = this._view.get_connect_Details_PartyInvitesCheck().checked;
        return detailsPanelData;
    },
    _detailsFacebookInfoAnchorClick: function SpottedScript_Controls_Navigation_Login_Controller$_detailsFacebookInfoAnchorClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._view.get_connect_Details_FacebookInfoPanel().style.display = '';
        this._view.get_connect_Details_WeeklyEmailInfoPanel().style.display = 'none';
        this._view.get_connect_Details_PartyInvitesInfoPanel().style.display = 'none';
    },
    _detailsWeeklyEmailInfoAnchorClick: function SpottedScript_Controls_Navigation_Login_Controller$_detailsWeeklyEmailInfoAnchorClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._view.get_connect_Details_FacebookInfoPanel().style.display = 'none';
        this._view.get_connect_Details_WeeklyEmailInfoPanel().style.display = '';
        this._view.get_connect_Details_PartyInvitesInfoPanel().style.display = 'none';
    },
    _detailsPartyInvitesInfoAnchorClick: function SpottedScript_Controls_Navigation_Login_Controller$_detailsPartyInvitesInfoAnchorClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._view.get_connect_Details_FacebookInfoPanel().style.display = 'none';
        this._view.get_connect_Details_WeeklyEmailInfoPanel().style.display = 'none';
        this._view.get_connect_Details_PartyInvitesInfoPanel().style.display = '';
    },
    autoLinkByAutoLoginUsr: function SpottedScript_Controls_Navigation_Login_Controller$autoLinkByAutoLoginUsr(sendDetailsPanelData) {
        /// <param name="sendDetailsPanelData" type="Boolean">
        /// </param>
        var thisAsyncOperation1 = this._registerStartAsync('Connecting...');
        this._server.autoLinkByAutoLoginUsr(this.autoLoginUsrK, this.autoLoginUsrLoginString, (sendDetailsPanelData) ? this._getDetailsPanelData() : null, Function.createDelegate(this, function(response) {
            if (this._registerEndAsync(thisAsyncOperation1)) {
                return;
            }
            if (ImportedUtilities.U.isTrue(response, 'Exception')) {
                this._showError(9, 'Internal server error');
            }
            else {
                if (ImportedUtilities.U.isTrue(response, 'FacebookAutoLoginUsrMatch')) {
                    if (ImportedUtilities.U.isTrue(response, 'NeedsConfirmation')) {
                        this._showDetailsPanel('AutoLinkByAutoLoginUsr', ImportedUtilities.U.get(response, 'Details'));
                    }
                    else {
                        this._setAuthCookie(ImportedUtilities.U.get(response, 'AuthCookie'), ImportedUtilities.U.get(response, 'AuthUsr'));
                        this._detectAutoLoginProblem(true);
                    }
                }
                else {
                    this._showError(10, 'Internal server error');
                }
            }
        }));
    },
    _add_View_Connect_CaptchaPanel: function SpottedScript_Controls_Navigation_Login_Controller$_add_View_Connect_CaptchaPanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = '\r\n<div id=\"{ClientID}Connect_CaptchaPanel\" class=\"LoginPanel\" style=\"display:none;\">\r\n\t<div class=\"LoginPanelInner\">\r\n\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\tOne more thing...\r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\tTo help fight spam, we need you to comfirm you\'re a nice, fleshy human. You should see five upper-case letters in the box below. Enter them to continue:\r\n\t\t</p>\r\n\t\t<div style=\"position:relative;\">\r\n\t\t\t<p style=\"position:absolute;\">\r\n\t\t\t\t<img id=\"{ClientID}Connect_Captcha_Img\" src=\"\" width=\"150\" height=\"50\" style=\"text-align:top;\" />\r\n\t\t\t</p>\r\n\t\t\t<div style=\"margin-left:160px; position:absolute;\">\r\n\t\t\t\t<p>\r\n\t\t\t\t\t<input id=\"{ClientID}Connect_Captcha_Textbox\" type=\"text\" class=\"xui-state-default ui-corner-all\" style=\"padding-left:5px; height:20px; width:50px;\" />\r\n\t\t\t\t</p>\r\n\t\t\t\t<p>\r\n\t\t\t\t\t<button id=\"{ClientID}Connect_Captcha_SaveButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\">Save</button><span id=\"{ClientID}Connect_Captcha_Error\" class=\"ForegroundAttentionRed\" style=\"font-weight:bold; display:none;\">&nbsp;Try again</span>\r\n\t\t\t\t</p>\r\n\t\t\t\t\r\n\t\t\t</div>\r\n\t\t</div>\r\n\t</div>\r\n\t<p style=\"position:relative;\">\r\n\t\t<button id=\"{ClientID}Connect_Captcha_BackButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:left; position:absolute; left:0px;\">Back</button>\r\n\r\n\t\t<button id=\"{ClientID}Connect_Captcha_CancelButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Cancel</button>\r\n\t</p>\r\n</div>\r\n';
        this._addChild(s);
        $addHandler(this._view.get_connect_Captcha_SaveButton(), 'click', Function.createDelegate(this, this._captchaPanelSaveClick));
        $addHandler(this._view.get_connect_Captcha_CancelButton(), 'click', Function.createDelegate(this, this._cancelButtonClick));
        $addHandler(this._view.get_connect_Captcha_BackButton(), 'click', Function.createDelegate(this, this._captchaPanelBackClick));
        this._defaultButton(this._view.get_connect_Captcha_Textbox(), this._view.get_connect_Captcha_SaveButton());
        return this._view.get_connect_CaptchaPanel();
    },
    _captchaPassthrough: '',
    _showCaptchaPanel: function SpottedScript_Controls_Navigation_Login_Controller$_showCaptchaPanel(captchaPanelSource, details) {
        /// <param name="captchaPanelSource" type="String">
        /// </param>
        /// <param name="details" type="Object">
        /// </param>
        this._captchaPanelSource = captchaPanelSource;
        this._ensurePanelGenerated('View.Connect_CaptchaPanel');
        this._view.get_connect_Captcha_Error().style.display = (ImportedUtilities.U.exists(details, 'CaptchaFailed')) ? '' : 'none';
        this._captchaPassthrough = ImportedUtilities.U.get(details, 'CaptchaEncrypted').toString();
        this._view.get_connect_Captcha_Img().src = '/support/hipimagenew.aspx?a=' + this._captchaPassthrough;
        this._changePanel('View.Connect_CaptchaPanel');
    },
    _captchaPanelSaveClick: function SpottedScript_Controls_Navigation_Login_Controller$_captchaPanelSaveClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._view.get_connect_Captcha_Error().style.display = 'none';
        if (this._view.get_connect_Captcha_Textbox().value.length !== 5) {
            this._view.get_connect_Captcha_Error().style.display = '';
            return;
        }
        if (this._captchaPanelSource === 'NoFacebookNewAccount') {
            this._noFacebookNewAccount();
        }
        else if (this._captchaPanelSource === 'NoFacebookLoginSkeleton') {
            this._noFacebookLogin('NoFacebookLoginSkeleton', true, true, false);
        }
        else if (this._captchaPanelSource === 'NoFacebookLoginFacebookNotConfirmed') {
            this._noFacebookLogin('NoFacebookLoginFacebookNotConfirmed', true, false, false);
        }
        else if (this._captchaPanelSource === 'NoFacebookLogin') {
            this._noFacebookLogin('NoFacebookLogin', false, false, false);
        }
        else if (this._captchaPanelSource === 'AutoLoginNoFacebookSkeleton') {
            this._noFacebookLogin('AutoLoginNoFacebookSkeleton', true, true, true);
        }
        else if (this._captchaPanelSource === 'AutoLoginNoFacebookFacebookNotConfirmed') {
            this._noFacebookLogin('AutoLoginNoFacebookFacebookNotConfirmed', true, false, true);
        }
        else if (this._captchaPanelSource === 'AutoLoginNoFacebookNeedsCaptcha') {
            this._noFacebookLogin('AutoLoginNoFacebookNeedsCaptcha', false, false, true);
        }
    },
    _captchaPanelBackClick: function SpottedScript_Controls_Navigation_Login_Controller$_captchaPanelBackClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        if (this._captchaPanelSource === 'NoFacebookNewAccount') {
            this._changePanel('View.Connect_DetailsPanel');
        }
        else if (this._captchaPanelSource === 'NoFacebookLoginSkeleton') {
            this._changePanel('View.Connect_DetailsPanel');
        }
        else if (this._captchaPanelSource === 'NoFacebookLoginFacebookNotConfirmed') {
            this._changePanel('View.Connect_DetailsPanel');
        }
        else if (this._captchaPanelSource === 'NoFacebookLogin') {
            this._changePanel('View.Connect_LoggedOut_NoFacebook_LoginPanel');
        }
        else if (this._captchaPanelSource === 'AutoLoginNoFacebookSkeleton') {
            this._changePanel('View.Connect_DetailsPanel');
        }
        else if (this._captchaPanelSource === 'AutoLoginNoFacebookFacebookNotConfirmed') {
            this._changePanel('View.Connect_DetailsPanel');
        }
        else if (this._captchaPanelSource === 'AutoLoginNoFacebookNeedsCaptcha') {
            this._changePanel('View.Connect_LoggedOutPanel');
        }
    },
    _getCaptchaData: function SpottedScript_Controls_Navigation_Login_Controller$_getCaptchaData() {
        /// <returns type="Object"></returns>
        this._ensurePanelGenerated('View.Connect_CaptchaPanel');
        var ret = {};
        ret['Entered'] = this._view.get_connect_Captcha_Textbox().value;
        ret['Passthrough'] = this._captchaPassthrough;
        return ret;
    },
    _add_View_Connect_LoggedInPanel: function SpottedScript_Controls_Navigation_Login_Controller$_add_View_Connect_LoggedInPanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = '\r\n<div id=\"{ClientID}Connect_LoggedInPanel\" class=\"LoginPanel\" style=\"display:none;\">\r\n\t<div class=\"LoginPanelInner\">\r\n\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\tLogged in\r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\tYou\'re logged in<span id=\"{ClientID}Connect_LoggedIn_LoggedInUsrLink\"></span>.\r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\t<button id=\"{ClientID}Connect_LoggedIn_CloseButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\">Close</button>\r\n\t\t\t<button id=\"{ClientID}Connect_LoggedIn_LogoutButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\">Log out</button>\r\n\t\t</p>\r\n\t\t<p id=\"{ClientID}Connect_LoggedIn_DisconnectLinkOuter\" style=\"display:none;\">\r\n\t\t\tTo permanently disconnect your Facebook account, <a id=\"{ClientID}Connect_LoggedIn_DisconnectButtonShowLink\" href=\"\">click here</a>.\r\n\t\t</p>\r\n\t\t<p id=\"{ClientID}Connect_LoggedIn_DisconnectButtonOuter\" style=\"display:none;\">\r\n\t\t\t<button id=\"{ClientID}Connect_LoggedIn_DisconnectButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:left;\">Disconnect</button>\r\n\t\t</p>\r\n\t</div>\r\n\t<p>\r\n\t\t<button id=\"{ClientID}Connect_LoggedIn_CancelButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Cancel</button>\r\n\t</p>\r\n</div>\r\n';
        this._addChild(s);
        $addHandler(this._view.get_connect_LoggedIn_DisconnectButton(), 'click', Function.createDelegate(this, this._disconnectButtonClick));
        $addHandler(this._view.get_connect_LoggedIn_LogoutButton(), 'click', Function.createDelegate(this, this._logoutButtonClick));
        $addHandler(this._view.get_connect_LoggedIn_CloseButton(), 'click', Function.createDelegate(this, this._cancelButtonClick));
        $addHandler(this._view.get_connect_LoggedIn_CancelButton(), 'click', Function.createDelegate(this, this._cancelButtonClick));
        $addHandler(this._view.get_connect_LoggedIn_DisconnectButtonShowLink(), 'click', Function.createDelegate(this, this._disconnectButtonShowClick));
        return this._view.get_connect_LoggedInPanel();
    },
    _showLoggedInPanel: function SpottedScript_Controls_Navigation_Login_Controller$_showLoggedInPanel(link) {
        /// <param name="link" type="String">
        /// </param>
        this._ensurePanelGenerated('View.Connect_LoggedInPanel');
        this._view.get_connect_LoggedIn_DisconnectLinkOuter().style.display = (this.currentIsLoggedInWithFacebook) ? '' : 'none';
        if (link === '???') {
            this._view.get_connect_LoggedIn_LoggedInUsrLink().innerHTML = '';
        }
        else if (link.length > 0) {
            this._view.get_connect_LoggedIn_LoggedInUsrLink().innerHTML = ' as ' + link;
        }
        this._changePanel('View.Connect_LoggedInPanel');
    },
    _disconnectButtonShowClick: function SpottedScript_Controls_Navigation_Login_Controller$_disconnectButtonShowClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._view.get_connect_LoggedIn_DisconnectButtonOuter().style.display = '';
    },
    _disconnectButtonClick: function SpottedScript_Controls_Navigation_Login_Controller$_disconnectButtonClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        if (!this._currentFacebookConnected) {
            this._removeAuthCookie();
            this._showError(5, 'You tried to disconnect, but you\'re not connected.');
        }
        else {
            if (this._currentAuthUsrHasNullPassword) {
                this._ensurePanelGenerated('View.Connect_CreatePasswordPanel');
                this._view.get_connect_CreatePassword_ErrorSpan().innerHTML = '';
                this._changePanel('View.Connect_CreatePasswordPanel');
            }
            else {
                this._disconnectInner('');
            }
        }
    },
    _add_View_Connect_CreatePasswordPanel: function SpottedScript_Controls_Navigation_Login_Controller$_add_View_Connect_CreatePasswordPanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = '\r\n<div id=\"{ClientID}Connect_CreatePasswordPanel\" class=\"LoginPanel\" style=\"display:none;\">\r\n\t<div class=\"LoginPanelInner\">\r\n\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\tCreate a password\r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\tYou\'ll need this password if you ever want to reconnect your Facebook to this account:\r\n\t\t</p>\r\n\t\t<p style=\"position:relative; height:25px; line-height:25px;\">\r\n\t\t\tPassword:\r\n\t\t\t<input id=\"{ClientID}Connect_CreatePassword_Password1Textbox\" type=\"password\" class=\"xui-state-default ui-corner-all\" style=\"padding-left:5px; height:20px; left:140px; top:0px; position:absolute; width:150px; border:1px solid #cccccc;\" />\r\n\t\t</p>\r\n\t\t<p style=\"position:relative; height:25px; line-height:25px;\">\r\n\t\t\tPassword (confirm):\r\n\t\t\t<input id=\"{ClientID}Connect_CreatePassword_Password2Textbox\" type=\"password\" class=\"xui-state-default ui-corner-all\" style=\"padding-left:5px; height:20px; left:140px; top:0px; position:absolute; width:150px; border:1px solid #cccccc;\" />\r\n\t\t</p>\r\n\t\t<p style=\"position:relative; height:30px; line-height:30px;\">\r\n\t\t\t<button id=\"{ClientID}Connect_CreatePassword_DisconnectButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\" style=\"left:140px; top:0px; position:absolute;\">Disconnect</button>\r\n\t\t</p>\r\n\t\t<p style=\"position:relative;\">\r\n\t\t\t<span id=\"{ClientID}Connect_CreatePassword_ErrorSpan\" class=\"ForegroundAttentionRed\" style=\"font-weight:bold; left:140px; top:0px; position:absolute;\"></span>\r\n\t\t</p>\r\n\r\n\t</div>\r\n\t<p>\r\n\t\t<button id=\"{ClientID}Connect_CreatePassword_BackButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:left;\">Back</button>\r\n\r\n\t\t<button id=\"{ClientID}Connect_CreatePassword_CancelButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Cancel</button>\r\n\t</p>\r\n</div>';
        this._addChild(s);
        $addHandler(this._view.get_connect_CreatePassword_DisconnectButton(), 'click', Function.createDelegate(this, this._createPasswordDisconnectButtonClick));
        $addHandler(this._view.get_connect_CreatePassword_CancelButton(), 'click', Function.createDelegate(this, this._cancelButtonClick));
        $addHandler(this._view.get_connect_CreatePassword_BackButton(), 'click', Function.createDelegate(this, this._createPasswordBackButtonClick));
        this._defaultButton(this._view.get_connect_CreatePassword_Password1Textbox(), this._view.get_connect_CreatePassword_DisconnectButton());
        this._defaultButton(this._view.get_connect_CreatePassword_Password2Textbox(), this._view.get_connect_CreatePassword_DisconnectButton());
        return this._view.get_connect_CreatePasswordPanel();
    },
    _createPasswordBackButtonClick: function SpottedScript_Controls_Navigation_Login_Controller$_createPasswordBackButtonClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._showLoggedInPanel('');
    },
    _createPasswordDisconnectButtonClick: function SpottedScript_Controls_Navigation_Login_Controller$_createPasswordDisconnectButtonClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        if (this._view.get_connect_CreatePassword_Password1Textbox().value.trim() !== this._view.get_connect_CreatePassword_Password2Textbox().value.trim()) {
            this._view.get_connect_CreatePassword_ErrorSpan().innerHTML = 'Passwords don\'t match.';
            return;
        }
        if (this._view.get_connect_CreatePassword_Password1Textbox().value.trim().length < 4) {
            this._view.get_connect_CreatePassword_ErrorSpan().innerHTML = 'Please enter at least four characters.';
            return;
        }
        this._disconnectInner(this._view.get_connect_CreatePassword_Password1Textbox().value.trim());
    },
    _disconnectInner: function SpottedScript_Controls_Navigation_Login_Controller$_disconnectInner(password) {
        /// <param name="password" type="String">
        /// </param>
        var thisAsyncOperation = this._registerStartAsync('Disconnecting...');
        this._server.unlinkAccount(password, Function.createDelegate(this, function(response) {
            if (this._registerEndAsync(thisAsyncOperation)) {
                return;
            }
            if (ImportedUtilities.U.isTrue(response, 'Exception')) {
                this._showError(10, 'Internal server error');
            }
            else {
                if (ImportedUtilities.U.isTrue(response, 'DoneUnlink')) {
                    var thisAsyncOperation1 = this._registerStartAsync('Disconnecting...');
                    FB.api(F.d('method', 'Auth.revokeAuthorization'), Function.createDelegate(this, function(revokeResponse) {
                        if (this._registerEndAsync(thisAsyncOperation1)) {
                            return;
                        }
                        this._removeAuthCookie();
                        jQuery(this._view.get_connectDialog()).dialog('close');
                    }));
                }
                else {
                    this._showError(11, 'Internal server error');
                }
            }
        }));
    },
    _add_View_Connect_AutoLoginMismatchPanel: function SpottedScript_Controls_Navigation_Login_Controller$_add_View_Connect_AutoLoginMismatchPanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = '\r\n<div id=\"{ClientID}Connect_AutoLoginMismatchPanel\" class=\"LoginPanel\" style=\"display:none;\">\r\n\t<div class=\"LoginPanelInner\">\r\n\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\tMight be a problem...\r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\t<span id=\"{ClientID}Connect_AutoLoginMismatch_AutoLoginUsrLink\"></span>\r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\t<button id=\"{ClientID}Connect_AutoLoginMismatch_RetryButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\">Retry login</button>\r\n\t\t\t<button id=\"{ClientID}Connect_AutoLoginMismatch_ContinueButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\">Continue to the page</button>\r\n\t\t</p>\r\n\t\t<p id=\"{ClientID}Connect_AutoLoginMismatch_SwitchAccountsPara\">\r\n\t\t\tTo switch your Facebook to <span id=\"{ClientID}Connect_AutoLoginMismatch_AutoLoginUsrLink2\"></span>, <a id=\"{ClientID}Connect_AutoLoginMismatch_SwitchAccountsShowLink\" href=\"\">click here</a>.\r\n\t\t</p>\r\n\t\t<p id=\"{ClientID}Connect_AutoLoginMismatch_SwitchAccountsOuter\" style=\"display:none;\">\r\n\t\t\t<button id=\"{ClientID}Connect_AutoLoginMismatch_SwitchButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:left;\">Switch accounts</button>\r\n\t\t</p>\r\n\t</div>\r\n\t<p>\r\n\t\t<button id=\"{ClientID}Connect_AutoLoginMismatch_CancelButton\"  class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Cancel</button>\t\t\r\n\t</p>\r\n</div>';
        this._addChild(s);
        $addHandler(this._view.get_connect_AutoLoginMismatch_RetryButton(), 'click', Function.createDelegate(this, this._detectAutoLoginRetryClick));
        $addHandler(this._view.get_connect_AutoLoginMismatch_ContinueButton(), 'click', Function.createDelegate(this, this._detectAutoLoginContinueClick));
        $addHandler(this._view.get_connect_AutoLoginMismatch_CancelButton(), 'click', Function.createDelegate(this, this._detectAutoLoginRetryClick));
        $addHandler(this._view.get_connect_AutoLoginMismatch_SwitchButton(), 'click', Function.createDelegate(this, this._detectAutoLoginSwitchClick));
        $addHandler(this._view.get_connect_AutoLoginMismatch_SwitchAccountsShowLink(), 'click', Function.createDelegate(this, this._detectAutoLoginSwitchShowLinkClick));
        return this._view.get_connect_AutoLoginMismatchPanel();
    },
    _detectAutoLoginProblemNewLink: false,
    _detectAutoLoginProblem: function SpottedScript_Controls_Navigation_Login_Controller$_detectAutoLoginProblem(newLink) {
        /// <param name="newLink" type="Boolean">
        /// </param>
        this._detectAutoLoginProblemNewLink = newLink;
        if (this.autoLogin && this.autoLoginUsrK > 0 && this.currentIsLoggedIn && this.autoLoginUsrK.toString() !== this._currentAuthUsrK) {
            this._ensurePanelGenerated('View.Connect_AutoLoginMismatchPanel');
            if (this._autoLoginNickname.length > 0) {
                this._view.get_connect_AutoLoginMismatch_AutoLoginUsrLink().innerHTML = 'You logged in as ' + this._currentAuthUsrLink + ', but the link you clicked was sent to ' + this._autoLoginLink + '. This might cause an error if you clicked on a private link.';
            }
            else if (this._autoLoginEmail.length > 0) {
                this._view.get_connect_AutoLoginMismatch_AutoLoginUsrLink().innerHTML = 'You logged in as ' + this._currentAuthUsrLink + ' (' + this._currentAuthUsrEmail + '), but the link you clicked was sent to a different account (' + this._autoLoginEmail + '). This might cause an error if you clicked on a private link.';
            }
            else {
                this._view.get_connect_AutoLoginMismatch_AutoLoginUsrLink().innerHTML = 'You logged in as ' + this._currentAuthUsrLink + ', but the link you clicked was sent to a different account. This might cause an error if you clicked on a private link.';
            }
            this._view.get_connect_AutoLoginMismatch_AutoLoginUsrLink2().innerHTML = (this._autoLoginNickname.length > 0) ? this._autoLoginLink : this._autoLoginEmail;
            this._view.get_connect_AutoLoginMismatch_SwitchAccountsPara().style.display = (this._autoLoginStringMatch) ? '' : 'none';
            this._changePanel('View.Connect_AutoLoginMismatchPanel');
        }
        else {
            this._detectAutoLoginProblemNext();
        }
    },
    _detectAutoLoginContinueClick: function SpottedScript_Controls_Navigation_Login_Controller$_detectAutoLoginContinueClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._detectAutoLoginProblemNext();
    },
    _detectAutoLoginRetryClick: function SpottedScript_Controls_Navigation_Login_Controller$_detectAutoLoginRetryClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._logoutNow(false, null, true);
    },
    _detectAutoLoginSwitchShowLinkClick: function SpottedScript_Controls_Navigation_Login_Controller$_detectAutoLoginSwitchShowLinkClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._view.get_connect_AutoLoginMismatch_SwitchAccountsOuter().style.display = '';
    },
    _detectAutoLoginSwitchClick: function SpottedScript_Controls_Navigation_Login_Controller$_detectAutoLoginSwitchClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._switchAccounts(false);
    },
    _switchAccounts: function SpottedScript_Controls_Navigation_Login_Controller$_switchAccounts(sendDetailsPanelData) {
        /// <param name="sendDetailsPanelData" type="Boolean">
        /// </param>
        var thisAsyncOperation = this._registerStartAsync('Switching accounts...');
        this._server.switchAccounts(this._currentFacebookUID, this.autoLoginUsrK, this.autoLoginUsrLoginString, (sendDetailsPanelData) ? this._getDetailsPanelData() : null, Function.createDelegate(this, function(response) {
            if (this._registerEndAsync(thisAsyncOperation)) {
                return;
            }
            if (ImportedUtilities.U.isTrue(response, 'Exception')) {
                this._showError(8, 'Internal server error.');
            }
            else {
                if (ImportedUtilities.U.isTrue(response, 'NeedsConfirmation')) {
                    this._showDetailsPanel('SwitchAccounts', ImportedUtilities.U.get(response, 'Details'));
                }
                else {
                    this._setAuthCookie(ImportedUtilities.U.get(response, 'AuthCookie'), ImportedUtilities.U.get(response, 'AuthUsr'));
                    this._showLikeButtonPanel();
                }
            }
        }));
    },
    _detectAutoLoginProblemNext: function SpottedScript_Controls_Navigation_Login_Controller$_detectAutoLoginProblemNext() {
        if (this._detectAutoLoginProblemNewLink) {
            this._showLikeButtonPanel();
        }
        else {
            jQuery(this._view.get_connectDialog()).dialog('close');
        }
    },
    _add_View_Connect_LikeButtonPanel: function SpottedScript_Controls_Navigation_Login_Controller$_add_View_Connect_LikeButtonPanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = '\r\n<div id=\"{ClientID}Connect_LikeButtonPanel\" class=\"LoginPanel\" style=\"display:none;\">\r\n\t<div class=\"LoginPanelInner\">\r\n\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\tClick the Like button...\r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\t... if you\'d like to be kept up to date by Facebook:\r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\t<fb:like href=\"http://www.facebook.com/dontstayin\" layout=\"box_count\" font=\"verdana\" width=\"200px\"></fb:like>\r\n\t\t</p>\r\n\t</div>\r\n\t<p>\r\n\t\t<button id=\"{ClientID}Connect_LikeButton_CancelButton\"  class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">I\'d rather not</button>\r\n\t</p>\r\n</div>\r\n';
        this._addChild(s);
        $addHandler(this._view.get_connect_LikeButton_CancelButton(), 'click', Function.createDelegate(this, this._cancelButtonClick));
        FB.XFBML.parse(this._view.get_connect_LikeButtonPanel());
        return this._view.get_connect_LikeButtonPanel();
    },
    _showLikeButtonPanel: function SpottedScript_Controls_Navigation_Login_Controller$_showLikeButtonPanel() {
        var thisAsyncOperation = this._registerStartAsync('Loading...');
        FB.api(F.d('method', 'fql.query', 'query', 'SELECT type, created_time FROM page_fan WHERE page_id=\"95813938222\" AND uid=\"' + this._currentFacebookUID + '\"'), Function.createDelegate(this, function(likeFqlResponse) {
            if (this._registerEndAsync(thisAsyncOperation)) {
                return;
            }
            if (ImportedUtilities.U.exists(likeFqlResponse, '/value/type')) {
                jQuery(this._view.get_connectDialog()).dialog('close');
            }
            else {
                this._changePanel('View.Connect_LikeButtonPanel');
                FB.Event.subscribe('edge.create', Function.createDelegate(this, function(edgeCreateResponse) {
                    jQuery(this._view.get_connectDialog()).dialog('close');
                }));
            }
        }));
    },
    _add_View_Connect_LoadingPanel: function SpottedScript_Controls_Navigation_Login_Controller$_add_View_Connect_LoadingPanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = '\r\n<div id=\"{ClientID}Connect_LoadingPanel\" class=\"LoginPanel\" style=\"display:none;\">\r\n\t<div class=\"LoginPanelInner\">\r\n\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\tLoading...\r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\tWe\'re waiting for Facebook to connect...\r\n\t\t</p>\r\n\t</div>\r\n\t<p>\r\n\t\t<button id=\"{ClientID}Connect_Loading_CancelButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Cancel</button>\r\n\t</p>\r\n</div>\r\n';
        this._addChild(s);
        $addHandler(this._view.get_connect_Loading_CancelButton(), 'click', Function.createDelegate(this, this._cancelButtonClick));
        return this._view.get_connect_LoadingPanel();
    },
    _add_View_Connect_ErrorPanel: function SpottedScript_Controls_Navigation_Login_Controller$_add_View_Connect_ErrorPanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = '\r\n<div id=\"{ClientID}Connect_ErrorPanel\" class=\"LoginPanel\" style=\"display:none;\">\r\n\t<div class=\"LoginPanelInner\">\r\n\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\tOops!\r\n\t\t</p>\r\n\t\t<p id=\"{ClientID}Connect_Error_ErrorDescription\" />\r\n\t\t<p>\r\n\t\t\t<button id=\"{ClientID}Connect_Error_TryAgainButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\">Try again</button>\r\n\t\t</p>\r\n\t</div>\r\n\t<p>\r\n\t\t<button id=\"{ClientID}Connect_Error_CancelButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Cancel</button>\r\n\t</p>\r\n</div>\r\n';
        this._addChild(s);
        $addHandler(this._view.get_connect_Error_CancelButton(), 'click', Function.createDelegate(this, this._cancelButtonClick));
        $addHandler(this._view.get_connect_Error_TryAgainButton(), 'click', Function.createDelegate(this, this._errorTryAgainClick));
        return this._view.get_connect_ErrorPanel();
    },
    _showError: function SpottedScript_Controls_Navigation_Login_Controller$_showError(id, description) {
        /// <param name="id" type="Number" integer="true">
        /// </param>
        /// <param name="description" type="String">
        /// </param>
        this._changePanel('View.Connect_ErrorPanel');
        this._view.get_connect_Error_ErrorDescription().innerHTML = description;
    },
    _errorTryAgainClick: function SpottedScript_Controls_Navigation_Login_Controller$_errorTryAgainClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._initialiseForm();
    },
    _add_View_Connect_LoadingLabel: function SpottedScript_Controls_Navigation_Login_Controller$_add_View_Connect_LoadingLabel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = '\r\n<div id=\"{ClientID}Connect_LoadingLabel\" class=\"ui-state-highlight ui-corner-all BigButton\" style=\"position:absolute; right:2px; top:36px; display:none; z-index:995;\">\r\n\t<p>\r\n\t\tLoading...\r\n\t</p>\r\n</div>\r\n';
        this._addChild(s);
        return this._view.get_connect_LoadingLabel();
    },
    _asyncInProgress: false,
    _asyncOperationCounter: 0,
    _registerStartAsync: function SpottedScript_Controls_Navigation_Login_Controller$_registerStartAsync(text) {
        /// <param name="text" type="String">
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        return this._registerStartAsyncGeneric(text, true, true);
    },
    _registerStartAsyncGeneric: function SpottedScript_Controls_Navigation_Login_Controller$_registerStartAsyncGeneric(text, setTimer, showLoadingLabel) {
        /// <param name="text" type="String">
        /// </param>
        /// <param name="setTimer" type="Boolean">
        /// </param>
        /// <param name="showLoadingLabel" type="Boolean">
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        this._asyncInProgress = true;
        this._asyncOperationCounter++;
        var thisAsyncOperationCounter = this._asyncOperationCounter;
        if (showLoadingLabel) {
            this._ensurePanelGenerated('View.Connect_LoadingLabel');
            if (text.length > 0) {
                this._view.get_connect_LoadingLabel().innerHTML = '<p>' + text + '<p>';
            }
            else {
                this._view.get_connect_LoadingLabel().innerHTML = '<p>Loading...<p>';
            }
            this._view.get_connect_LoadingLabel().style.display = '';
        }
        if (setTimer) {
            window.setTimeout(Function.createDelegate(this, function() {
                if (this._asyncInProgress && thisAsyncOperationCounter === this._asyncOperationCounter) {
                    this._view.get_connect_LoadingLabel().innerHTML = '<p style=\"margin-top:3px;padding-top:0px;\">This seems to be taking a long time... <button id=\"Connect_LoadingLabel_CancelLink\">Cancel</button></p>';
                    $addHandler(document.getElementById('Connect_LoadingLabel_CancelLink'), 'click', Function.createDelegate(this, function(e) {
                        e.preventDefault();
                        this._cancelledAsyncOperations[thisAsyncOperationCounter.toString()] = true;
                        this._asyncInProgress = false;
                        if (this._view.get_connect_LoadingLabel() != null) {
                            this._view.get_connect_LoadingLabel().style.display = 'none';
                        }
                        this._initialiseForm();
                    }));
                }
            }), 5000);
        }
        return thisAsyncOperationCounter;
    },
    _registerEndAsync: function SpottedScript_Controls_Navigation_Login_Controller$_registerEndAsync(asyncOperationCounter) {
        /// <param name="asyncOperationCounter" type="Number" integer="true">
        /// </param>
        /// <returns type="Boolean"></returns>
        if (this._cancelledAsyncOperations[asyncOperationCounter.toString()] != null && this._cancelledAsyncOperations[asyncOperationCounter.toString()]) {
            return true;
        }
        this._asyncInProgress = false;
        if (this._view.get_connect_LoadingLabel() != null) {
            this._view.get_connect_LoadingLabel().style.display = 'none';
        }
        return false;
    },
    _add_View_Connect_DebugPanel: function SpottedScript_Controls_Navigation_Login_Controller$_add_View_Connect_DebugPanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = '\r\n<p id=\"{ClientID}Connect_DebugPanel\" style=\"display:none;\">\r\n\t<textarea id=\"{ClientID}Connect_Debug_Output\" cols=\"75\" rows=\"10\"></textarea><br />\r\n\t<button id=\"{ClientID}Connect_Debug_LogoutButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\">Logout</button>\r\n\t<button id=\"{ClientID}Connect_Debug_DisconnectButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\">Disconnect</button>\r\n\t<button id=\"{ClientID}Connect_Debug_AuthButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\">Auth</button>\r\n</p>\r\n';
        this._addChild(s);
        $addHandler(this._view.get_connect_Debug_LogoutButton(), 'click', Function.createDelegate(this, this._logoutButtonClick));
        $addHandler(this._view.get_connect_Debug_DisconnectButton(), 'click', Function.createDelegate(this, this._disconnectDebug));
        return this._view.get_connect_DebugPanel();
    },
    _disconnectDebug: function SpottedScript_Controls_Navigation_Login_Controller$_disconnectDebug(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        var thisAsyncOperation = this._registerStartAsync('Disconnecting...');
        FB.api(F.d('method', 'Auth.revokeAuthorization'), Function.createDelegate(this, function(revokeResponse) {
            if (this._registerEndAsync(thisAsyncOperation)) {
                return;
            }
            this._removeAuthCookie();
            jQuery(this._view.get_connectDialog()).dialog('close');
        }));
    },
    debug: function SpottedScript_Controls_Navigation_Login_Controller$debug(txt) {
        /// <param name="txt" type="String">
        /// </param>
        jQuery(this._view.get_connectDialog()).dialog('open');
        this._ensurePanelGenerated('View.Connect_DebugPanel');
        this._view.get_connect_DebugPanel().style.display = '';
        this._view.get_connect_Debug_Output().innerHTML = txt + '\n' + this._view.get_connect_Debug_Output().innerHTML;
    },
    _setAuthCookie: function SpottedScript_Controls_Navigation_Login_Controller$_setAuthCookie(authCookie, authUsr) {
        /// <param name="authCookie" type="Object">
        /// </param>
        /// <param name="authUsr" type="Object">
        /// </param>
        var name = ImportedUtilities.U.get(authCookie, 'Name').toString();
        var value = ImportedUtilities.U.get(authCookie, 'Value').toString();
        var expires = (ImportedUtilities.U.get(authCookie, 'Expires')).toUTCString();
        var path = ImportedUtilities.U.get(authCookie, 'Path').toString();
        document.cookie = 'SpottedAuthFix=' + escape(value) + '; ' + 'expires=' + expires + '; path=' + path;
        try {
            var valueParts = value.split('-');
            var usrK = valueParts[1];
            var facebookUID = valueParts[2];
            this._currentAuthUsrK = usrK;
            this._currentAuthUsrFacebookUID = facebookUID;
            this.updateIsLoggedIn();
        }
        catch ($e1) {
        }
        try {
            this._currentAuthUsrNickName = ImportedUtilities.U.get(authUsr, 'NickName').toString();
            this._currentAuthUsrLink = ImportedUtilities.U.get(authUsr, 'Link').toString();
            this._currentAuthUsrEmail = ImportedUtilities.U.get(authUsr, 'Email').toString();
            this._currentAuthUsrHasNullPassword = ImportedUtilities.U.get(authUsr, 'HasNullPassword');
        }
        catch ($e2) {
        }
        this._currentAuthCookieHasError = false;
    },
    _removeAuthCookie: function SpottedScript_Controls_Navigation_Login_Controller$_removeAuthCookie() {
        this._currentAuthUsrK = '0';
        this._currentAuthUsrFacebookUID = '0';
        this.updateIsLoggedIn();
        document.cookie = 'SpottedAuthFix=1; expires=Fri, 27 Jul 2001 02:47:11 UTC; path=/;';
    },
    _updateCurrentFacebookLoginStatus: function SpottedScript_Controls_Navigation_Login_Controller$_updateCurrentFacebookLoginStatus(statusResponse) {
        /// <param name="statusResponse" type="Object">
        /// </param>
        this._currentFacebookConnected = ImportedUtilities.U.exists(statusResponse, 'status') && ImportedUtilities.U.get(statusResponse, 'status').toString() === 'connected';
        this._currentFacebookLoggedIn = ImportedUtilities.U.exists(statusResponse, 'status') && ImportedUtilities.U.get(statusResponse, 'status').toString() !== 'unknown';
        this._currentFacebookUID = (this._currentFacebookConnected) ? ImportedUtilities.U.get(statusResponse, 'authResponse/userID').toString() : '0';
        this._currentFacebookAuthResponse = (this._currentFacebookConnected) ? ImportedUtilities.U.get(statusResponse, 'authResponse') : null;
        this.updateIsLoggedIn();
    },
    updateAuthDetailsFromDsiCookie: function SpottedScript_Controls_Navigation_Login_Controller$updateAuthDetailsFromDsiCookie() {
        var s = SpottedScript.Controls.Navigation.Login.Controller._readCookie('SpottedAuthFix');
        if (!this._currentAuthCookieHasError && s !== '' && s !== '1') {
            var valueParts = s.split('-');
            var usrK = valueParts[1];
            var facebookUID = valueParts[2];
            this._currentAuthUsrK = usrK;
            this._currentAuthUsrFacebookUID = facebookUID;
        }
        else {
            this._currentAuthUsrK = '0';
            this._currentAuthUsrFacebookUID = '0';
        }
        this.updateIsLoggedIn();
    },
    updateIsLoggedIn: function SpottedScript_Controls_Navigation_Login_Controller$updateIsLoggedIn() {
        /// <returns type="Boolean"></returns>
        this.currentIsLoggedIn = this._currentAuthUsrK !== '0';
        this.currentIsLoggedInWithFacebook = this._currentFacebookUID !== '0' && this._currentFacebookUID === this._currentAuthUsrFacebookUID;
        return this.currentIsLoggedIn;
    },
    _cancelButtonClick: function SpottedScript_Controls_Navigation_Login_Controller$_cancelButtonClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        jQuery(this._view.get_connectDialog()).dialog('close');
    },
    _logoutButtonClick: function SpottedScript_Controls_Navigation_Login_Controller$_logoutButtonClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._logoutNow(true, null, false);
    },
    _logoutOfFacebookButtonClick: function SpottedScript_Controls_Navigation_Login_Controller$_logoutOfFacebookButtonClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._logoutNow(true, null, true);
    },
    logOutAndDoAction: function SpottedScript_Controls_Navigation_Login_Controller$logOutAndDoAction(onLogout, forceFacebookLogout) {
        /// <param name="onLogout" type="ScriptSharpLibrary.Action">
        /// </param>
        /// <param name="forceFacebookLogout" type="Boolean">
        /// </param>
        this._logoutNow(true, onLogout, forceFacebookLogout);
    },
    _logoutNow: function SpottedScript_Controls_Navigation_Login_Controller$_logoutNow(closeConnectDialog, onLogout, forceFacebookLogout) {
        /// <param name="closeConnectDialog" type="Boolean">
        /// </param>
        /// <param name="onLogout" type="ScriptSharpLibrary.Action">
        /// </param>
        /// <param name="forceFacebookLogout" type="Boolean">
        /// </param>
        this._facebookAccountNeedsConfirmationBecauseInitiallyFacebookLoggedIn = false;
        this._facebookAccountNeedsConfirmationBecauseInitiallyFacebookConnectedAndSiteLoggedOut = false;
        if (this.currentIsLoggedIn || (this._currentFacebookConnected && forceFacebookLogout)) {
            if (this.currentIsLoggedInWithFacebook || (this._currentFacebookConnected && forceFacebookLogout)) {
                var thisAsyncOperation = this._registerStartAsync('Logging out...');
                FB.logout(Function.createDelegate(this, function(logoutResponse) {
                    if (this._registerEndAsync(thisAsyncOperation)) {
                        return;
                    }
                    this._logoutInternal(closeConnectDialog, onLogout);
                }));
            }
            else {
                this._logoutInternal(closeConnectDialog, onLogout);
            }
        }
        else {
            this._logoutInternal(closeConnectDialog, onLogout);
        }
    },
    _logoutInternal: function SpottedScript_Controls_Navigation_Login_Controller$_logoutInternal(closeConnectDialog, onLogout) {
        /// <param name="closeConnectDialog" type="Boolean">
        /// </param>
        /// <param name="onLogout" type="ScriptSharpLibrary.Action">
        /// </param>
        this._removeAuthCookie();
        if (closeConnectDialog) {
            jQuery(this._view.get_connectDialog()).dialog('close');
        }
        else {
            this._initialiseForm();
        }
        if (onLogout != null) {
            onLogout();
        }
    },
    _changePanelOnClick: function SpottedScript_Controls_Navigation_Login_Controller$_changePanelOnClick(panelString, e) {
        /// <param name="panelString" type="String">
        /// </param>
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._changePanel(panelString);
    },
    _changePanel: function SpottedScript_Controls_Navigation_Login_Controller$_changePanel(panelString) {
        /// <param name="panelString" type="String">
        /// </param>
        var panel = this._getPanel(panelString);
        if (panel == null) {
            panel = this._ensurePanelGenerated(panelString);
        }
        if (panel == null) {
            alert('panel is null!');
        }
        if (this._view.get_connect_ConnectingPanel() != null) {
            this._view.get_connect_ConnectingPanel().style.display = (panel.id === this._view.get_connect_ConnectingPanel().id) ? '' : 'none';
        }
        if (this._view.get_connect_LoadingPanel() != null) {
            this._view.get_connect_LoadingPanel().style.display = (panel.id === this._view.get_connect_LoadingPanel().id) ? '' : 'none';
        }
        if (this._view.get_connect_ErrorPanel() != null) {
            this._view.get_connect_ErrorPanel().style.display = (panel.id === this._view.get_connect_ErrorPanel().id) ? '' : 'none';
        }
        if (this._view.get_connect_LoggedOutPanel() != null) {
            this._view.get_connect_LoggedOutPanel().style.display = (panel.id === this._view.get_connect_LoggedOutPanel().id) ? '' : 'none';
        }
        if (this._view.get_connect_LoggedOut_NoFacebook_ChoosePanel() != null) {
            this._view.get_connect_LoggedOut_NoFacebook_ChoosePanel().style.display = (panel.id === this._view.get_connect_LoggedOut_NoFacebook_ChoosePanel().id) ? '' : 'none';
        }
        if (this._view.get_connect_LoggedOut_NoFacebook_LoginPanel() != null) {
            this._view.get_connect_LoggedOut_NoFacebook_LoginPanel().style.display = (panel.id === this._view.get_connect_LoggedOut_NoFacebook_LoginPanel().id) ? '' : 'none';
        }
        if (this._view.get_connect_LoggedOut_NoFacebook_LoginNoPasswordPanel() != null) {
            this._view.get_connect_LoggedOut_NoFacebook_LoginNoPasswordPanel().style.display = (panel.id === this._view.get_connect_LoggedOut_NoFacebook_LoginNoPasswordPanel().id) ? '' : 'none';
        }
        if (this._view.get_connect_LoggedOut_NoFacebook_PasswordResetPanel() != null) {
            this._view.get_connect_LoggedOut_NoFacebook_PasswordResetPanel().style.display = (panel.id === this._view.get_connect_LoggedOut_NoFacebook_PasswordResetPanel().id) ? '' : 'none';
        }
        if (this._view.get_connect_LoggedOut_NoFacebook_SignUp1Panel() != null) {
            this._view.get_connect_LoggedOut_NoFacebook_SignUp1Panel().style.display = (panel.id === this._view.get_connect_LoggedOut_NoFacebook_SignUp1Panel().id) ? '' : 'none';
        }
        if (this._view.get_connect_LoggedOut_NoFacebook_SignUp2Panel() != null) {
            this._view.get_connect_LoggedOut_NoFacebook_SignUp2Panel().style.display = (panel.id === this._view.get_connect_LoggedOut_NoFacebook_SignUp2Panel().id) ? '' : 'none';
        }
        if (this._view.get_connect_NewAccount_ConfirmFacebookPanel() != null) {
            this._view.get_connect_NewAccount_ConfirmFacebookPanel().style.display = (panel.id === this._view.get_connect_NewAccount_ConfirmFacebookPanel().id) ? '' : 'none';
        }
        if (this._view.get_connect_NewAccount_NoEmailMatchPanel() != null) {
            this._view.get_connect_NewAccount_NoEmailMatchPanel().style.display = (panel.id === this._view.get_connect_NewAccount_NoEmailMatchPanel().id) ? '' : 'none';
        }
        if (this._view.get_connect_NewAccount_EmailMatchPanel() != null) {
            this._view.get_connect_NewAccount_EmailMatchPanel().style.display = (panel.id === this._view.get_connect_NewAccount_EmailMatchPanel().id) ? '' : 'none';
        }
        if (this._view.get_connect_NewAccount_ChooseAccountPanel() != null) {
            this._view.get_connect_NewAccount_ChooseAccountPanel().style.display = (panel.id === this._view.get_connect_NewAccount_ChooseAccountPanel().id) ? '' : 'none';
        }
        if (this._view.get_connect_NewAccount_ForgotPasswordPanel() != null) {
            this._view.get_connect_NewAccount_ForgotPasswordPanel().style.display = (panel.id === this._view.get_connect_NewAccount_ForgotPasswordPanel().id) ? '' : 'none';
        }
        if (this._view.get_connect_DetailsPanel() != null) {
            this._view.get_connect_DetailsPanel().style.display = (panel.id === this._view.get_connect_DetailsPanel().id) ? '' : 'none';
        }
        if (this._view.get_connect_CaptchaPanel() != null) {
            this._view.get_connect_CaptchaPanel().style.display = (panel.id === this._view.get_connect_CaptchaPanel().id) ? '' : 'none';
        }
        if (this._view.get_connect_LoggedInPanel() != null) {
            this._view.get_connect_LoggedInPanel().style.display = (panel.id === this._view.get_connect_LoggedInPanel().id) ? '' : 'none';
        }
        if (this._view.get_connect_LikeButtonPanel() != null) {
            this._view.get_connect_LikeButtonPanel().style.display = (panel.id === this._view.get_connect_LikeButtonPanel().id) ? '' : 'none';
        }
        if (this._view.get_connect_AutoLoginMismatchPanel() != null) {
            this._view.get_connect_AutoLoginMismatchPanel().style.display = (panel.id === this._view.get_connect_AutoLoginMismatchPanel().id) ? '' : 'none';
        }
        if (this._view.get_connect_CreatePasswordPanel() != null) {
            this._view.get_connect_CreatePasswordPanel().style.display = (panel.id === this._view.get_connect_CreatePasswordPanel().id) ? '' : 'none';
        }
    },
    _getPanel: function SpottedScript_Controls_Navigation_Login_Controller$_getPanel(panelString) {
        /// <param name="panelString" type="String">
        /// </param>
        /// <returns type="Object" domElement="true"></returns>
        if (panelString === 'View.Connect_LoadingLabel') {
            return this._view.get_connect_LoadingLabel();
        }
        else if (panelString === 'View.Connect_ConnectingPanel') {
            return this._view.get_connect_ConnectingPanel();
        }
        else if (panelString === 'View.Connect_LoadingPanel') {
            return this._view.get_connect_LoadingPanel();
        }
        else if (panelString === 'View.Connect_ErrorPanel') {
            return this._view.get_connect_ErrorPanel();
        }
        else if (panelString === 'View.Connect_LoggedOutPanel') {
            return this._view.get_connect_LoggedOutPanel();
        }
        else if (panelString === 'View.Connect_LoggedOut_NoFacebook_LoginPanel') {
            return this._view.get_connect_LoggedOut_NoFacebook_LoginPanel();
        }
        else if (panelString === 'View.Connect_LoggedOut_NoFacebook_LoginNoPasswordPanel') {
            return this._view.get_connect_LoggedOut_NoFacebook_LoginNoPasswordPanel();
        }
        else if (panelString === 'View.Connect_LoggedOut_NoFacebook_PasswordResetPanel') {
            return this._view.get_connect_LoggedOut_NoFacebook_PasswordResetPanel();
        }
        else if (panelString === 'View.Connect_LoggedOut_NoFacebook_ChoosePanel') {
            return this._view.get_connect_LoggedOut_NoFacebook_ChoosePanel();
        }
        else if (panelString === 'View.Connect_LoggedOut_NoFacebook_SignUp1Panel') {
            return this._view.get_connect_LoggedOut_NoFacebook_SignUp1Panel();
        }
        else if (panelString === 'View.Connect_LoggedOut_NoFacebook_SignUp2Panel') {
            return this._view.get_connect_LoggedOut_NoFacebook_SignUp2Panel();
        }
        else if (panelString === 'View.Connect_NewAccount_ConfirmFacebookPanel') {
            return this._view.get_connect_NewAccount_ConfirmFacebookPanel();
        }
        else if (panelString === 'View.Connect_NewAccount_NoEmailMatchPanel') {
            return this._view.get_connect_NewAccount_NoEmailMatchPanel();
        }
        else if (panelString === 'View.Connect_NewAccount_EmailMatchPanel') {
            return this._view.get_connect_NewAccount_EmailMatchPanel();
        }
        else if (panelString === 'View.Connect_NewAccount_ChooseAccountPanel') {
            return this._view.get_connect_NewAccount_ChooseAccountPanel();
        }
        else if (panelString === 'View.Connect_NewAccount_ForgotPasswordPanel') {
            return this._view.get_connect_NewAccount_ForgotPasswordPanel();
        }
        else if (panelString === 'View.Connect_DetailsPanel') {
            return this._view.get_connect_DetailsPanel();
        }
        else if (panelString === 'View.Connect_CaptchaPanel') {
            return this._view.get_connect_CaptchaPanel();
        }
        else if (panelString === 'View.Connect_LoggedInPanel') {
            return this._view.get_connect_LoggedInPanel();
        }
        else if (panelString === 'View.Connect_LikeButtonPanel') {
            return this._view.get_connect_LikeButtonPanel();
        }
        else if (panelString === 'View.Connect_DebugPanel') {
            return this._view.get_connect_DebugPanel();
        }
        else if (panelString === 'View.Connect_AutoLoginMismatchPanel') {
            return this._view.get_connect_AutoLoginMismatchPanel();
        }
        else if (panelString === 'View.Connect_CreatePasswordPanel') {
            return this._view.get_connect_CreatePasswordPanel();
        }
        else {
            return null;
        }
    },
    _ensurePanelGenerated: function SpottedScript_Controls_Navigation_Login_Controller$_ensurePanelGenerated(panelString) {
        /// <param name="panelString" type="String">
        /// </param>
        /// <returns type="Object" domElement="true"></returns>
        var panel = this._getPanel(panelString);
        if (panel != null) {
            return panel;
        }
        if (panelString === 'View.Connect_LoadingLabel') {
            panel = this._add_View_Connect_LoadingLabel();
        }
        else if (panelString === 'View.Connect_ConnectingPanel') {
            panel = this._add_View_Connect_ConnectingPanel();
        }
        else if (panelString === 'View.Connect_LoadingPanel') {
            panel = this._add_View_Connect_LoadingPanel();
        }
        else if (panelString === 'View.Connect_ErrorPanel') {
            panel = this._add_View_Connect_ErrorPanel();
        }
        else if (panelString === 'View.Connect_LoggedOutPanel') {
            panel = this._add_View_Connect_LoggedOutPanel();
        }
        else if (panelString === 'View.Connect_LoggedOut_NoFacebook_ChoosePanel') {
            panel = this._add_View_Connect_LoggedOut_NoFacebook_ChoosePanel();
        }
        else if (panelString === 'View.Connect_LoggedOut_NoFacebook_LoginPanel') {
            panel = this._add_View_Connect_LoggedOut_NoFacebook_LoginPanel();
        }
        else if (panelString === 'View.Connect_LoggedOut_NoFacebook_LoginNoPasswordPanel') {
            panel = this._add_View_Connect_LoggedOut_NoFacebook_LoginNoPasswordPanel();
        }
        else if (panelString === 'View.Connect_LoggedOut_NoFacebook_PasswordResetPanel') {
            panel = this._add_View_Connect_LoggedOut_NoFacebook_PasswordResetPanel();
        }
        else if (panelString === 'View.Connect_LoggedOut_NoFacebook_SignUp1Panel') {
            panel = this._add_View_Connect_LoggedOut_NoFacebook_SignUp1Panel();
        }
        else if (panelString === 'View.Connect_LoggedOut_NoFacebook_SignUp2Panel') {
            panel = this._add_View_Connect_LoggedOut_NoFacebook_SignUp2Panel();
        }
        else if (panelString === 'View.Connect_NewAccount_ConfirmFacebookPanel') {
            panel = this._add_View_Connect_NewAccount_ConfirmFacebookPanel();
        }
        else if (panelString === 'View.Connect_NewAccount_NoEmailMatchPanel') {
            panel = this._add_View_Connect_NewAccount_NoEmailMatchPanel();
        }
        else if (panelString === 'View.Connect_NewAccount_EmailMatchPanel') {
            panel = this._add_View_Connect_NewAccount_EmailMatchPanel();
        }
        else if (panelString === 'View.Connect_NewAccount_ChooseAccountPanel') {
            panel = this._add_View_Connect_NewAccount_ChooseAccountPanel();
        }
        else if (panelString === 'View.Connect_DetailsPanel') {
            panel = this._add_View_Connect_DetailsPanel();
        }
        else if (panelString === 'View.Connect_CaptchaPanel') {
            panel = this._add_View_Connect_CaptchaPanel();
        }
        else if (panelString === 'View.Connect_NewAccount_ForgotPasswordPanel') {
            panel = this._add_View_Connect_NewAccount_ForgotPasswordPanel();
        }
        else if (panelString === 'View.Connect_LoggedInPanel') {
            panel = this._add_View_Connect_LoggedInPanel();
        }
        else if (panelString === 'View.Connect_LikeButtonPanel') {
            panel = this._add_View_Connect_LikeButtonPanel();
        }
        else if (panelString === 'View.Connect_DebugPanel') {
            panel = this._add_View_Connect_DebugPanel();
        }
        else if (panelString === 'View.Connect_AutoLoginMismatchPanel') {
            panel = this._add_View_Connect_AutoLoginMismatchPanel();
        }
        else if (panelString === 'View.Connect_CreatePasswordPanel') {
            panel = this._add_View_Connect_CreatePasswordPanel();
        }
        return panel;
    },
    _replaceClientId: function SpottedScript_Controls_Navigation_Login_Controller$_replaceClientId(s) {
        /// <param name="s" type="String">
        /// </param>
        /// <returns type="String"></returns>
        return s.replace(this._clientIdRegex, this._view.clientId + '_');
    },
    _addChild: function SpottedScript_Controls_Navigation_Login_Controller$_addChild(s) {
        /// <param name="s" type="String">
        /// </param>
        var el = document.createElement('div');
        el.innerHTML = this._replaceClientId(s);
        this._view.get_connectDialog().appendChild(el);
    },
    _defaultButton: function SpottedScript_Controls_Navigation_Login_Controller$_defaultButton(textBox, button) {
        /// <param name="textBox" type="Object" domElement="true">
        /// </param>
        /// <param name="button" type="Object" domElement="true">
        /// </param>
        $addHandler(textBox, 'keyup', Function.createDelegate(this, function(e) {
            if (e.keyCode === 13) {
                if (this._asyncInProgress) {
                    return;
                }
                button.click();
            }
        }));
    },
    _getStringFromBasePage: function SpottedScript_Controls_Navigation_Login_Controller$_getStringFromBasePage(id) {
        /// <param name="id" type="String">
        /// </param>
        /// <returns type="String"></returns>
        var s = '';
        try {
            s = (document.getElementById(id)).value;
        }
        catch ($e1) {
        }
        return s;
    },
    _getStringFromPage: function SpottedScript_Controls_Navigation_Login_Controller$_getStringFromPage(id) {
        /// <param name="id" type="String">
        /// </param>
        /// <returns type="String"></returns>
        return this._getStringFromBasePage('Content_' + id);
    },
    _getIntFromPage: function SpottedScript_Controls_Navigation_Login_Controller$_getIntFromPage(id) {
        /// <param name="id" type="String">
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        var i = 0;
        try {
            i = Number.parseInvariant((document.getElementById('Content_' + id)).value);
        }
        catch ($e1) {
        }
        return i;
    },
    _getBoolFromPage: function SpottedScript_Controls_Navigation_Login_Controller$_getBoolFromPage(id) {
        /// <param name="id" type="String">
        /// </param>
        /// <returns type="Boolean"></returns>
        return this._getBoolFromBasePage('Content_' + id);
    },
    _getBoolFromBasePage: function SpottedScript_Controls_Navigation_Login_Controller$_getBoolFromBasePage(id) {
        /// <param name="id" type="String">
        /// </param>
        /// <returns type="Boolean"></returns>
        var b = false;
        try {
            b = Boolean.parse((document.getElementById(id)).value.toLowerCase());
        }
        catch ($e1) {
        }
        return b;
    },
    _addOption: function SpottedScript_Controls_Navigation_Login_Controller$_addOption(value, text, parent) {
        /// <param name="value" type="String">
        /// </param>
        /// <param name="text" type="String">
        /// </param>
        /// <param name="parent" type="Object" domElement="true">
        /// </param>
        var oe = document.createElement('OPTION');
        oe.value = value;
        oe.text = text;
        try {
            parent.add(oe, null);
        }
        catch ($e1) {
            parent.add(oe);
        }
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.Navigation.Login.Server
SpottedScript.Controls.Navigation.Login.Server = function SpottedScript_Controls_Navigation_Login_Server() {
}
SpottedScript.Controls.Navigation.Login.Server.prototype = {
    checkEmail: function SpottedScript_Controls_Navigation_Login_Server$checkEmail(email, response) {
        /// <param name="email" type="String">
        /// </param>
        /// <param name="response" type="ScriptSharpLibrary.Response">
        /// </param>
        var paramArr = [ email ];
        var req = eval('PageMethods.ClientRequest');
        if (req != null) {
            try {
                req('Spotted.Controls.Navigation.Login, Spotted, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 'CheckEmail', paramArr, response, response);
            }
            catch (e) {
                var d = {};
                d['Exception'] = true;
                d['ExceptionType'] = 'ClientException';
                d['Message'] = e.message;
                d['StackTrace'] = '';
                response(d);
            }
        }
    },
    noFacebookNewAccount: function SpottedScript_Controls_Navigation_Login_Server$noFacebookNewAccount(noFacebookSignupPanelData, detailsPanelData, captchaData, response) {
        /// <param name="noFacebookSignupPanelData" type="Object">
        /// </param>
        /// <param name="detailsPanelData" type="Object">
        /// </param>
        /// <param name="captchaData" type="Object">
        /// </param>
        /// <param name="response" type="ScriptSharpLibrary.Response">
        /// </param>
        var paramArr = [ noFacebookSignupPanelData, detailsPanelData, captchaData ];
        var req = eval('PageMethods.ClientRequest');
        if (req != null) {
            try {
                req('Spotted.Controls.Navigation.Login, Spotted, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 'NoFacebookNewAccount', paramArr, response, response);
            }
            catch (e) {
                var d = {};
                d['Exception'] = true;
                d['ExceptionType'] = 'ClientException';
                d['Message'] = e.message;
                d['StackTrace'] = '';
                response(d);
            }
        }
    },
    getUniqueNickName: function SpottedScript_Controls_Navigation_Login_Server$getUniqueNickName(nickname, usrK, response) {
        /// <param name="nickname" type="String">
        /// </param>
        /// <param name="usrK" type="Number" integer="true">
        /// </param>
        /// <param name="response" type="ScriptSharpLibrary.Response">
        /// </param>
        var paramArr = [ nickname, usrK ];
        var req = eval('PageMethods.ClientRequest');
        if (req != null) {
            try {
                req('Spotted.Controls.Navigation.Login, Spotted, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 'GetUniqueNickName', paramArr, response, response);
            }
            catch (e) {
                var d = {};
                d['Exception'] = true;
                d['ExceptionType'] = 'ClientException';
                d['Message'] = e.message;
                d['StackTrace'] = '';
                response(d);
            }
        }
    },
    noFacebookLogin: function SpottedScript_Controls_Navigation_Login_Server$noFacebookLogin(nickNameOrEmail, password, captchaData, noFacebookSignupPanelData, detailsPanelData, autoLogin, autoLoginUsrK, autoLoginString, response) {
        /// <param name="nickNameOrEmail" type="String">
        /// </param>
        /// <param name="password" type="String">
        /// </param>
        /// <param name="captchaData" type="Object">
        /// </param>
        /// <param name="noFacebookSignupPanelData" type="Object">
        /// </param>
        /// <param name="detailsPanelData" type="Object">
        /// </param>
        /// <param name="autoLogin" type="Boolean">
        /// </param>
        /// <param name="autoLoginUsrK" type="Number" integer="true">
        /// </param>
        /// <param name="autoLoginString" type="String">
        /// </param>
        /// <param name="response" type="ScriptSharpLibrary.Response">
        /// </param>
        var paramArr = [ nickNameOrEmail, password, captchaData, noFacebookSignupPanelData, detailsPanelData, autoLogin, autoLoginUsrK, autoLoginString ];
        var req = eval('PageMethods.ClientRequest');
        if (req != null) {
            try {
                req('Spotted.Controls.Navigation.Login, Spotted, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 'NoFacebookLogin', paramArr, response, response);
            }
            catch (e) {
                var d = {};
                d['Exception'] = true;
                d['ExceptionType'] = 'ClientException';
                d['Message'] = e.message;
                d['StackTrace'] = '';
                response(d);
            }
        }
    },
    sendPassword: function SpottedScript_Controls_Navigation_Login_Server$sendPassword(emailOrNickname, response) {
        /// <param name="emailOrNickname" type="String">
        /// </param>
        /// <param name="response" type="ScriptSharpLibrary.Response">
        /// </param>
        var paramArr = [ emailOrNickname ];
        var req = eval('PageMethods.ClientRequest');
        if (req != null) {
            try {
                req('Spotted.Controls.Navigation.Login, Spotted, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 'SendPassword', paramArr, response, response);
            }
            catch (e) {
                var d = {};
                d['Exception'] = true;
                d['ExceptionType'] = 'ClientException';
                d['Message'] = e.message;
                d['StackTrace'] = '';
                response(d);
            }
        }
    },
    switchAccounts: function SpottedScript_Controls_Navigation_Login_Server$switchAccounts(currentUIDFromFacebook, autoLoginUsrK, autoLoginUsrLoginString, detailsPanelData, response) {
        /// <param name="currentUIDFromFacebook" type="String">
        /// </param>
        /// <param name="autoLoginUsrK" type="Number" integer="true">
        /// </param>
        /// <param name="autoLoginUsrLoginString" type="String">
        /// </param>
        /// <param name="detailsPanelData" type="Object">
        /// </param>
        /// <param name="response" type="ScriptSharpLibrary.Response">
        /// </param>
        var paramArr = [ currentUIDFromFacebook, autoLoginUsrK, autoLoginUsrLoginString, detailsPanelData ];
        var req = eval('PageMethods.ClientRequest');
        if (req != null) {
            try {
                req('Spotted.Controls.Navigation.Login, Spotted, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 'SwitchAccounts', paramArr, response, response);
            }
            catch (e) {
                var d = {};
                d['Exception'] = true;
                d['ExceptionType'] = 'ClientException';
                d['Message'] = e.message;
                d['StackTrace'] = '';
                response(d);
            }
        }
    },
    createNewAccount: function SpottedScript_Controls_Navigation_Login_Server$createNewAccount(detailsPanelData, response) {
        /// <param name="detailsPanelData" type="Object">
        /// </param>
        /// <param name="response" type="ScriptSharpLibrary.Response">
        /// </param>
        var paramArr = [ detailsPanelData ];
        var req = eval('PageMethods.ClientRequest');
        if (req != null) {
            try {
                req('Spotted.Controls.Navigation.Login, Spotted, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 'CreateNewAccount', paramArr, response, response);
            }
            catch (e) {
                var d = {};
                d['Exception'] = true;
                d['ExceptionType'] = 'ClientException';
                d['Message'] = e.message;
                d['StackTrace'] = '';
                response(d);
            }
        }
    },
    getHomePlaceFromFacebook: function SpottedScript_Controls_Navigation_Login_Server$getHomePlaceFromFacebook(response) {
        /// <param name="response" type="ScriptSharpLibrary.Response">
        /// </param>
        var paramArr = [];
        var req = eval('PageMethods.ClientRequest');
        if (req != null) {
            try {
                req('Spotted.Controls.Navigation.Login, Spotted, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 'GetHomePlaceFromFacebook', paramArr, response, response);
            }
            catch (e) {
                var d = {};
                d['Exception'] = true;
                d['ExceptionType'] = 'ClientException';
                d['Message'] = e.message;
                d['StackTrace'] = '';
                response(d);
            }
        }
    },
    getUserByFacebookUID: function SpottedScript_Controls_Navigation_Login_Server$getUserByFacebookUID(autoLoginUsrK, autoLoginUsrLoginString, response) {
        /// <param name="autoLoginUsrK" type="Number" integer="true">
        /// </param>
        /// <param name="autoLoginUsrLoginString" type="String">
        /// </param>
        /// <param name="response" type="ScriptSharpLibrary.Response">
        /// </param>
        var paramArr = [ autoLoginUsrK, autoLoginUsrLoginString ];
        var req = eval('PageMethods.ClientRequest');
        if (req != null) {
            try {
                req('Spotted.Controls.Navigation.Login, Spotted, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 'GetUserByFacebookUID', paramArr, response, response);
            }
            catch (e) {
                var d = {};
                d['Exception'] = true;
                d['ExceptionType'] = 'ClientException';
                d['Message'] = e.message;
                d['StackTrace'] = '';
                response(d);
            }
        }
    },
    autoLinkByAutoLoginUsr: function SpottedScript_Controls_Navigation_Login_Server$autoLinkByAutoLoginUsr(autoLoginUsrK, autoLoginUsrLoginString, detailsPanelData, response) {
        /// <param name="autoLoginUsrK" type="Number" integer="true">
        /// </param>
        /// <param name="autoLoginUsrLoginString" type="String">
        /// </param>
        /// <param name="detailsPanelData" type="Object">
        /// </param>
        /// <param name="response" type="ScriptSharpLibrary.Response">
        /// </param>
        var paramArr = [ autoLoginUsrK, autoLoginUsrLoginString, detailsPanelData ];
        var req = eval('PageMethods.ClientRequest');
        if (req != null) {
            try {
                req('Spotted.Controls.Navigation.Login, Spotted, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 'AutoLinkByAutoLoginUsr', paramArr, response, response);
            }
            catch (e) {
                var d = {};
                d['Exception'] = true;
                d['ExceptionType'] = 'ClientException';
                d['Message'] = e.message;
                d['StackTrace'] = '';
                response(d);
            }
        }
    },
    autoLinkByEmail: function SpottedScript_Controls_Navigation_Login_Server$autoLinkByEmail(detailsPanelData, response) {
        /// <param name="detailsPanelData" type="Object">
        /// </param>
        /// <param name="response" type="ScriptSharpLibrary.Response">
        /// </param>
        var paramArr = [ detailsPanelData ];
        var req = eval('PageMethods.ClientRequest');
        if (req != null) {
            try {
                req('Spotted.Controls.Navigation.Login, Spotted, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 'AutoLinkByEmail', paramArr, response, response);
            }
            catch (e) {
                var d = {};
                d['Exception'] = true;
                d['ExceptionType'] = 'ClientException';
                d['Message'] = e.message;
                d['StackTrace'] = '';
                response(d);
            }
        }
    },
    linkAccounts: function SpottedScript_Controls_Navigation_Login_Server$linkAccounts(nickNameOrEmail, password, detailsPanelData, response) {
        /// <param name="nickNameOrEmail" type="String">
        /// </param>
        /// <param name="password" type="String">
        /// </param>
        /// <param name="detailsPanelData" type="Object">
        /// </param>
        /// <param name="response" type="ScriptSharpLibrary.Response">
        /// </param>
        var paramArr = [ nickNameOrEmail, password, detailsPanelData ];
        var req = eval('PageMethods.ClientRequest');
        if (req != null) {
            try {
                req('Spotted.Controls.Navigation.Login, Spotted, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 'LinkAccounts', paramArr, response, response);
            }
            catch (e) {
                var d = {};
                d['Exception'] = true;
                d['ExceptionType'] = 'ClientException';
                d['Message'] = e.message;
                d['StackTrace'] = '';
                response(d);
            }
        }
    },
    unlinkAccount: function SpottedScript_Controls_Navigation_Login_Server$unlinkAccount(password, response) {
        /// <param name="password" type="String">
        /// </param>
        /// <param name="response" type="ScriptSharpLibrary.Response">
        /// </param>
        var paramArr = [ password ];
        var req = eval('PageMethods.ClientRequest');
        if (req != null) {
            try {
                req('Spotted.Controls.Navigation.Login, Spotted, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 'UnlinkAccount', paramArr, response, response);
            }
            catch (e) {
                var d = {};
                d['Exception'] = true;
                d['ExceptionType'] = 'ClientException';
                d['Message'] = e.message;
                d['StackTrace'] = '';
                response(d);
            }
        }
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.Navigation.Login.View
SpottedScript.Controls.Navigation.Login.View = function SpottedScript_Controls_Navigation_Login_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    /// <field name="server" type="SpottedScript.Controls.Navigation.Login.Server">
    /// </field>
    this.clientId = clientId;
    this.server = new SpottedScript.Controls.Navigation.Login.Server();
}
SpottedScript.Controls.Navigation.Login.View.prototype = {
    clientId: null,
    server: null,
    get_toggleAdminLinkButton: function SpottedScript_Controls_Navigation_Login_View$get_toggleAdminLinkButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ToggleAdminLinkButton');
    },
    get_connectDialog: function SpottedScript_Controls_Navigation_Login_View$get_connectDialog() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ConnectDialog');
    },
    get_connect_Inner: function SpottedScript_Controls_Navigation_Login_View$get_connect_Inner() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Inner');
    },
    get_connect_Note: function SpottedScript_Controls_Navigation_Login_View$get_connect_Note() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Note');
    },
    get_connect_LoadingLabel: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoadingLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoadingLabel');
    },
    get_connect_LoadingPanel: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoadingPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoadingPanel');
    },
    get_connect_Loading_CancelButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_Loading_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Loading_CancelButton');
    },
    get_connect_ConnectingPanel: function SpottedScript_Controls_Navigation_Login_View$get_connect_ConnectingPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_ConnectingPanel');
    },
    get_connect_Connecting_PopupRetry: function SpottedScript_Controls_Navigation_Login_View$get_connect_Connecting_PopupRetry() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Connecting_PopupRetry');
    },
    get_connect_Connecting_BackButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_Connecting_BackButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Connecting_BackButton');
    },
    get_connect_Connecting_CancelButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_Connecting_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Connecting_CancelButton');
    },
    get_connect_ErrorPanel: function SpottedScript_Controls_Navigation_Login_View$get_connect_ErrorPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_ErrorPanel');
    },
    get_connect_Error_ErrorDescription: function SpottedScript_Controls_Navigation_Login_View$get_connect_Error_ErrorDescription() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Error_ErrorDescription');
    },
    get_connect_Error_TryAgainButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_Error_TryAgainButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Error_TryAgainButton');
    },
    get_connect_Error_CancelButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_Error_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Error_CancelButton');
    },
    get_connect_LoggedOutPanel: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOutPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOutPanel');
    },
    get_connect_LoggedOut_ConnectButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_ConnectButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_ConnectButton');
    },
    get_connect_LoggedOut_CancelButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_CancelButton');
    },
    get_connect_LoggedOut_NoFacebookButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebookButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebookButton');
    },
    get_connect_LoggedOut_NoFacebook_ChoosePanel: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_ChoosePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_ChoosePanel');
    },
    get_connect_LoggedOut_NoFacebook_Choose_LoginButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_Choose_LoginButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_Choose_LoginButton');
    },
    get_connect_LoggedOut_NoFacebook_Choose_SignupButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_Choose_SignupButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_Choose_SignupButton');
    },
    get_connect_LoggedOut_NoFacebook_Choose_BackButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_Choose_BackButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_Choose_BackButton');
    },
    get_connect_LoggedOut_NoFacebook_Choose_CancelButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_Choose_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_Choose_CancelButton');
    },
    get_connect_LoggedOut_NoFacebook_LoginPanel: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_LoginPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_LoginPanel');
    },
    get_connect_LoggedOut_NoFacebook_Login_UsernameTextbox: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_Login_UsernameTextbox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_Login_UsernameTextbox');
    },
    get_connect_LoggedOut_NoFacebook_Login_PasswordTextbox: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_Login_PasswordTextbox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_Login_PasswordTextbox');
    },
    get_connect_LoggedOut_NoFacebook_Login_Error: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_Login_Error() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_Login_Error');
    },
    get_connect_LoggedOut_NoFacebook_Login_LoginButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_Login_LoginButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_Login_LoginButton');
    },
    get_connect_LoggedOut_NoFacebook_Login_NoPasswordButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_Login_NoPasswordButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_Login_NoPasswordButton');
    },
    get_connect_LoggedOut_NoFacebook_Login_BackButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_Login_BackButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_Login_BackButton');
    },
    get_connect_LoggedOut_NoFacebook_Login_CancelButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_Login_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_Login_CancelButton');
    },
    get_connect_LoggedOut_NoFacebook_Login_ForgottonPasswordButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_Login_ForgottonPasswordButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_Login_ForgottonPasswordButton');
    },
    get_connect_LoggedOut_NoFacebook_LoginNoPasswordPanel: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_LoginNoPasswordPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_LoginNoPasswordPanel');
    },
    get_connect_LoggedOut_NoFacebook_LoginNoPassword_TryAgainButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_LoginNoPassword_TryAgainButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_LoginNoPassword_TryAgainButton');
    },
    get_connect_LoggedOut_NoFacebook_LoginNoPassword_CancelButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_LoginNoPassword_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_LoginNoPassword_CancelButton');
    },
    get_connect_LoggedOut_NoFacebook_PasswordResetPanel: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_PasswordResetPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_PasswordResetPanel');
    },
    get_connect_LoggedOut_NoFacebook_PasswordReset_Title: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_PasswordReset_Title() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_PasswordReset_Title');
    },
    get_connect_LoggedOut_NoFacebook_PasswordReset_UsernameTextbox: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_PasswordReset_UsernameTextbox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_PasswordReset_UsernameTextbox');
    },
    get_connect_LoggedOut_NoFacebook_PasswordReset_SendLinkButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_PasswordReset_SendLinkButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_PasswordReset_SendLinkButton');
    },
    get_connect_LoggedOut_NoFacebook_PasswordReset_MessageLabel: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_PasswordReset_MessageLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_PasswordReset_MessageLabel');
    },
    get_connect_LoggedOut_NoFacebook_PasswordReset_ErrorLabel: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_PasswordReset_ErrorLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_PasswordReset_ErrorLabel');
    },
    get_connect_LoggedOut_NoFacebook_PasswordReset_BackButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_PasswordReset_BackButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_PasswordReset_BackButton');
    },
    get_connect_LoggedOut_NoFacebook_PasswordReset_CancelButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_PasswordReset_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_PasswordReset_CancelButton');
    },
    get_connect_LoggedOut_NoFacebook_SignUp1Panel: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_SignUp1Panel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp1Panel');
    },
    get_connect_LoggedOut_NoFacebook_SignUp1_EmailPara: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_SignUp1_EmailPara() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp1_EmailPara');
    },
    get_connect_LoggedOut_NoFacebook_SignUp1_EmailTextbox: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_SignUp1_EmailTextbox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp1_EmailTextbox');
    },
    get_connect_LoggedOut_NoFacebook_SignUp1_Password1Textbox: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_SignUp1_Password1Textbox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp1_Password1Textbox');
    },
    get_connect_LoggedOut_NoFacebook_SignUp1_Password2Textbox: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_SignUp1_Password2Textbox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp1_Password2Textbox');
    },
    get_connect_LoggedOut_NoFacebook_SignUp1_SaveButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_SignUp1_SaveButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp1_SaveButton');
    },
    get_connect_LoggedOut_NoFacebook_SignUp1_ErrorLabel: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_SignUp1_ErrorLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp1_ErrorLabel');
    },
    get_connect_LoggedOut_NoFacebook_SignUp1_BackButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_SignUp1_BackButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp1_BackButton');
    },
    get_connect_LoggedOut_NoFacebook_SignUp1_CancelButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_SignUp1_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp1_CancelButton');
    },
    get_connect_LoggedOut_NoFacebook_SignUp2Panel: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_SignUp2Panel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp2Panel');
    },
    get_connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextbox: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextbox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextbox');
    },
    get_connect_LoggedOut_NoFacebook_SignUp2_LastNameTextbox: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_SignUp2_LastNameTextbox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp2_LastNameTextbox');
    },
    get_connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox');
    },
    get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDown: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDown');
    },
    get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDown: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDown');
    },
    get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDown: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDown');
    },
    get_connect_LoggedOut_NoFacebook_SignUp2_SexMaleRadio: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_SignUp2_SexMaleRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp2_SexMaleRadio');
    },
    get_connect_LoggedOut_NoFacebook_SignUp2_SexFemaleRadio: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_SignUp2_SexFemaleRadio() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp2_SexFemaleRadio');
    },
    get_connect_LoggedOut_NoFacebook_SignUp2_SaveButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_SignUp2_SaveButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp2_SaveButton');
    },
    get_connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel');
    },
    get_connect_LoggedOut_NoFacebook_SignUp2_BackButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_SignUp2_BackButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp2_BackButton');
    },
    get_connect_LoggedOut_NoFacebook_SignUp2_CancelButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedOut_NoFacebook_SignUp2_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp2_CancelButton');
    },
    get_connect_NewAccount_ConfirmFacebookPanel: function SpottedScript_Controls_Navigation_Login_View$get_connect_NewAccount_ConfirmFacebookPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_NewAccount_ConfirmFacebookPanel');
    },
    get_connect_NewAccount_ConfirmFacebook_Image: function SpottedScript_Controls_Navigation_Login_View$get_connect_NewAccount_ConfirmFacebook_Image() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_NewAccount_ConfirmFacebook_Image');
    },
    get_connect_NewAccount_ConfirmFacebook_Link: function SpottedScript_Controls_Navigation_Login_View$get_connect_NewAccount_ConfirmFacebook_Link() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_NewAccount_ConfirmFacebook_Link');
    },
    get_connect_NewAccount_ConfirmFacebook_YesButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_NewAccount_ConfirmFacebook_YesButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_NewAccount_ConfirmFacebook_YesButton');
    },
    get_connect_NewAccount_ConfirmFacebook_NoButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_NewAccount_ConfirmFacebook_NoButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_NewAccount_ConfirmFacebook_NoButton');
    },
    get_connect_NewAccount_ConfirmFacebook_BackButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_NewAccount_ConfirmFacebook_BackButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_NewAccount_ConfirmFacebook_BackButton');
    },
    get_connect_NewAccount_ConfirmFacebook_CancelButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_NewAccount_ConfirmFacebook_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_NewAccount_ConfirmFacebook_CancelButton');
    },
    get_connect_NewAccount_NoEmailMatchPanel: function SpottedScript_Controls_Navigation_Login_View$get_connect_NewAccount_NoEmailMatchPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_NewAccount_NoEmailMatchPanel');
    },
    get_connect_NewAccount_NoEmailMatch_NewAccountButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_NewAccount_NoEmailMatch_NewAccountButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_NewAccount_NoEmailMatch_NewAccountButton');
    },
    get_connect_NewAccount_NoEmailMatch_ChooseAccountButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_NewAccount_NoEmailMatch_ChooseAccountButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_NewAccount_NoEmailMatch_ChooseAccountButton');
    },
    get_connect_NewAccount_NoEmailMatch_CancelButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_NewAccount_NoEmailMatch_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_NewAccount_NoEmailMatch_CancelButton');
    },
    get_connect_NewAccount_NoEmailMatch_FacebookLogoutButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_NewAccount_NoEmailMatch_FacebookLogoutButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_NewAccount_NoEmailMatch_FacebookLogoutButton');
    },
    get_connect_NewAccount_NoEmailMatch_BackButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_NewAccount_NoEmailMatch_BackButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_NewAccount_NoEmailMatch_BackButton');
    },
    get_connect_NewAccount_EmailMatchPanel: function SpottedScript_Controls_Navigation_Login_View$get_connect_NewAccount_EmailMatchPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_NewAccount_EmailMatchPanel');
    },
    get_connect_NewAccount_EmailMatch_UserLink1: function SpottedScript_Controls_Navigation_Login_View$get_connect_NewAccount_EmailMatch_UserLink1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_NewAccount_EmailMatch_UserLink1');
    },
    get_connect_NewAccount_EmailMatch_AutoConnectButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_NewAccount_EmailMatch_AutoConnectButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_NewAccount_EmailMatch_AutoConnectButton');
    },
    get_connect_NewAccount_EmailMatch_ChooseAccountButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_NewAccount_EmailMatch_ChooseAccountButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_NewAccount_EmailMatch_ChooseAccountButton');
    },
    get_connect_NewAccount_EmailMatch_CancelButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_NewAccount_EmailMatch_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_NewAccount_EmailMatch_CancelButton');
    },
    get_connect_NewAccount_EmailMatch_FacebookLogoutButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_NewAccount_EmailMatch_FacebookLogoutButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_NewAccount_EmailMatch_FacebookLogoutButton');
    },
    get_connect_NewAccount_EmailMatch_BackButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_NewAccount_EmailMatch_BackButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_NewAccount_EmailMatch_BackButton');
    },
    get_connect_NewAccount_ChooseAccountPanel: function SpottedScript_Controls_Navigation_Login_View$get_connect_NewAccount_ChooseAccountPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_NewAccount_ChooseAccountPanel');
    },
    get_connect_NewAccount_ChooseAccount_UsernameTextbox: function SpottedScript_Controls_Navigation_Login_View$get_connect_NewAccount_ChooseAccount_UsernameTextbox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_NewAccount_ChooseAccount_UsernameTextbox');
    },
    get_connect_NewAccount_ChooseAccount_PasswordTextbox: function SpottedScript_Controls_Navigation_Login_View$get_connect_NewAccount_ChooseAccount_PasswordTextbox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_NewAccount_ChooseAccount_PasswordTextbox');
    },
    get_connect_NewAccount_ChooseAccount_LinkAccountButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_NewAccount_ChooseAccount_LinkAccountButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_NewAccount_ChooseAccount_LinkAccountButton');
    },
    get_connect_NewAccount_ChooseAccount_ErrorLabel: function SpottedScript_Controls_Navigation_Login_View$get_connect_NewAccount_ChooseAccount_ErrorLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_NewAccount_ChooseAccount_ErrorLabel');
    },
    get_connect_NewAccount_ChooseAccount_CancelButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_NewAccount_ChooseAccount_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_NewAccount_ChooseAccount_CancelButton');
    },
    get_connect_NewAccount_ChooseAccount_FacebookLogoutButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_NewAccount_ChooseAccount_FacebookLogoutButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_NewAccount_ChooseAccount_FacebookLogoutButton');
    },
    get_connect_NewAccount_ChooseAccount_BackButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_NewAccount_ChooseAccount_BackButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_NewAccount_ChooseAccount_BackButton');
    },
    get_connect_NewAccount_ChooseAccount_ForgottonPasswordButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_NewAccount_ChooseAccount_ForgottonPasswordButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_NewAccount_ChooseAccount_ForgottonPasswordButton');
    },
    get_connect_NewAccount_ForgotPasswordPanel: function SpottedScript_Controls_Navigation_Login_View$get_connect_NewAccount_ForgotPasswordPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_NewAccount_ForgotPasswordPanel');
    },
    get_connect_NewAccount_ForgotPassword_UsernameTextbox: function SpottedScript_Controls_Navigation_Login_View$get_connect_NewAccount_ForgotPassword_UsernameTextbox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_NewAccount_ForgotPassword_UsernameTextbox');
    },
    get_connect_NewAccount_ForgotPassword_SendLinkButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_NewAccount_ForgotPassword_SendLinkButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_NewAccount_ForgotPassword_SendLinkButton');
    },
    get_connect_NewAccount_ForgotPassword_MessageLabel: function SpottedScript_Controls_Navigation_Login_View$get_connect_NewAccount_ForgotPassword_MessageLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_NewAccount_ForgotPassword_MessageLabel');
    },
    get_connect_NewAccount_ForgotPassword_ErrorLabel: function SpottedScript_Controls_Navigation_Login_View$get_connect_NewAccount_ForgotPassword_ErrorLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_NewAccount_ForgotPassword_ErrorLabel');
    },
    get_connect_NewAccount_ForgotPassword_CancelButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_NewAccount_ForgotPassword_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_NewAccount_ForgotPassword_CancelButton');
    },
    get_connect_NewAccount_ForgotPassword_FacebookLogoutButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_NewAccount_ForgotPassword_FacebookLogoutButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_NewAccount_ForgotPassword_FacebookLogoutButton');
    },
    get_connect_NewAccount_ForgotPassword_BackButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_NewAccount_ForgotPassword_BackButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_NewAccount_ForgotPassword_BackButton');
    },
    get_connect_LoggedInPanel: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedInPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedInPanel');
    },
    get_connect_LoggedIn_LoggedInUsrLink: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedIn_LoggedInUsrLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedIn_LoggedInUsrLink');
    },
    get_connect_LoggedIn_CloseButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedIn_CloseButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedIn_CloseButton');
    },
    get_connect_LoggedIn_LogoutButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedIn_LogoutButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedIn_LogoutButton');
    },
    get_connect_LoggedIn_DisconnectLinkOuter: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedIn_DisconnectLinkOuter() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedIn_DisconnectLinkOuter');
    },
    get_connect_LoggedIn_DisconnectButtonShowLink: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedIn_DisconnectButtonShowLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedIn_DisconnectButtonShowLink');
    },
    get_connect_LoggedIn_DisconnectButtonOuter: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedIn_DisconnectButtonOuter() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedIn_DisconnectButtonOuter');
    },
    get_connect_LoggedIn_DisconnectButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedIn_DisconnectButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedIn_DisconnectButton');
    },
    get_connect_LoggedIn_CancelButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_LoggedIn_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LoggedIn_CancelButton');
    },
    get_connect_AutoLoginMismatchPanel: function SpottedScript_Controls_Navigation_Login_View$get_connect_AutoLoginMismatchPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_AutoLoginMismatchPanel');
    },
    get_connect_AutoLoginMismatch_AutoLoginUsrLink: function SpottedScript_Controls_Navigation_Login_View$get_connect_AutoLoginMismatch_AutoLoginUsrLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_AutoLoginMismatch_AutoLoginUsrLink');
    },
    get_connect_AutoLoginMismatch_RetryButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_AutoLoginMismatch_RetryButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_AutoLoginMismatch_RetryButton');
    },
    get_connect_AutoLoginMismatch_ContinueButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_AutoLoginMismatch_ContinueButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_AutoLoginMismatch_ContinueButton');
    },
    get_connect_AutoLoginMismatch_SwitchAccountsPara: function SpottedScript_Controls_Navigation_Login_View$get_connect_AutoLoginMismatch_SwitchAccountsPara() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_AutoLoginMismatch_SwitchAccountsPara');
    },
    get_connect_AutoLoginMismatch_AutoLoginUsrLink2: function SpottedScript_Controls_Navigation_Login_View$get_connect_AutoLoginMismatch_AutoLoginUsrLink2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_AutoLoginMismatch_AutoLoginUsrLink2');
    },
    get_connect_AutoLoginMismatch_SwitchAccountsShowLink: function SpottedScript_Controls_Navigation_Login_View$get_connect_AutoLoginMismatch_SwitchAccountsShowLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_AutoLoginMismatch_SwitchAccountsShowLink');
    },
    get_connect_AutoLoginMismatch_SwitchAccountsOuter: function SpottedScript_Controls_Navigation_Login_View$get_connect_AutoLoginMismatch_SwitchAccountsOuter() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_AutoLoginMismatch_SwitchAccountsOuter');
    },
    get_connect_AutoLoginMismatch_SwitchButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_AutoLoginMismatch_SwitchButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_AutoLoginMismatch_SwitchButton');
    },
    get_connect_AutoLoginMismatch_CancelButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_AutoLoginMismatch_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_AutoLoginMismatch_CancelButton');
    },
    get_connect_CreatePasswordPanel: function SpottedScript_Controls_Navigation_Login_View$get_connect_CreatePasswordPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_CreatePasswordPanel');
    },
    get_connect_CreatePassword_Password1Textbox: function SpottedScript_Controls_Navigation_Login_View$get_connect_CreatePassword_Password1Textbox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_CreatePassword_Password1Textbox');
    },
    get_connect_CreatePassword_Password2Textbox: function SpottedScript_Controls_Navigation_Login_View$get_connect_CreatePassword_Password2Textbox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_CreatePassword_Password2Textbox');
    },
    get_connect_CreatePassword_DisconnectButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_CreatePassword_DisconnectButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_CreatePassword_DisconnectButton');
    },
    get_connect_CreatePassword_ErrorSpan: function SpottedScript_Controls_Navigation_Login_View$get_connect_CreatePassword_ErrorSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_CreatePassword_ErrorSpan');
    },
    get_connect_CreatePassword_CancelButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_CreatePassword_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_CreatePassword_CancelButton');
    },
    get_connect_CreatePassword_BackButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_CreatePassword_BackButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_CreatePassword_BackButton');
    },
    get_connect_LikeButtonPanel: function SpottedScript_Controls_Navigation_Login_View$get_connect_LikeButtonPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LikeButtonPanel');
    },
    get_connect_LikeButton_CancelButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_LikeButton_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_LikeButton_CancelButton');
    },
    get_connect_DetailsPanel: function SpottedScript_Controls_Navigation_Login_View$get_connect_DetailsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_DetailsPanel');
    },
    get_connect_Details_MusicDropDown: function SpottedScript_Controls_Navigation_Login_View$get_connect_Details_MusicDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Details_MusicDropDown');
    },
    get_connect_Details_CountryDropDown: function SpottedScript_Controls_Navigation_Login_View$get_connect_Details_CountryDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Details_CountryDropDown');
    },
    get_connect_Details_PlaceDropDown: function SpottedScript_Controls_Navigation_Login_View$get_connect_Details_PlaceDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Details_PlaceDropDown');
    },
    get_connect_Details_PlaceDefaultOuterSpan: function SpottedScript_Controls_Navigation_Login_View$get_connect_Details_PlaceDefaultOuterSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Details_PlaceDefaultOuterSpan');
    },
    get_connect_Details_PlaceDefaultSpan: function SpottedScript_Controls_Navigation_Login_View$get_connect_Details_PlaceDefaultSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Details_PlaceDefaultSpan');
    },
    get_connect_Details_PlaceChangeLink: function SpottedScript_Controls_Navigation_Login_View$get_connect_Details_PlaceChangeLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Details_PlaceChangeLink');
    },
    get_connect_Details_FacebookInfoPanel: function SpottedScript_Controls_Navigation_Login_View$get_connect_Details_FacebookInfoPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Details_FacebookInfoPanel');
    },
    get_connect_Details_WeeklyEmailInfoPanel: function SpottedScript_Controls_Navigation_Login_View$get_connect_Details_WeeklyEmailInfoPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Details_WeeklyEmailInfoPanel');
    },
    get_connect_Details_PartyInvitesInfoPanel: function SpottedScript_Controls_Navigation_Login_View$get_connect_Details_PartyInvitesInfoPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Details_PartyInvitesInfoPanel');
    },
    get_connect_Details_FacebookCheck: function SpottedScript_Controls_Navigation_Login_View$get_connect_Details_FacebookCheck() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Details_FacebookCheck');
    },
    get_connect_Details_FacebookCheckLabel: function SpottedScript_Controls_Navigation_Login_View$get_connect_Details_FacebookCheckLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Details_FacebookCheckLabel');
    },
    get_connect_Details_FacebookInfoAnchor: function SpottedScript_Controls_Navigation_Login_View$get_connect_Details_FacebookInfoAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Details_FacebookInfoAnchor');
    },
    get_connect_Details_WeeklyEmailCheck: function SpottedScript_Controls_Navigation_Login_View$get_connect_Details_WeeklyEmailCheck() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Details_WeeklyEmailCheck');
    },
    get_connect_Details_WeeklyEmailCheckLabel: function SpottedScript_Controls_Navigation_Login_View$get_connect_Details_WeeklyEmailCheckLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Details_WeeklyEmailCheckLabel');
    },
    get_connect_Details_WeeklyEmailInfoAnchor: function SpottedScript_Controls_Navigation_Login_View$get_connect_Details_WeeklyEmailInfoAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Details_WeeklyEmailInfoAnchor');
    },
    get_connect_Details_PartyInvitesCheck: function SpottedScript_Controls_Navigation_Login_View$get_connect_Details_PartyInvitesCheck() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Details_PartyInvitesCheck');
    },
    get_connect_Details_PartyInvitesCheckLabel: function SpottedScript_Controls_Navigation_Login_View$get_connect_Details_PartyInvitesCheckLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Details_PartyInvitesCheckLabel');
    },
    get_connect_Details_PartyInvitesInfoAnchor: function SpottedScript_Controls_Navigation_Login_View$get_connect_Details_PartyInvitesInfoAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Details_PartyInvitesInfoAnchor');
    },
    get_connect_Details_CancelButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_Details_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Details_CancelButton');
    },
    get_connect_Details_BackButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_Details_BackButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Details_BackButton');
    },
    get_connect_Details_SaveButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_Details_SaveButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Details_SaveButton');
    },
    get_connect_Details_PlaceErrorSpan: function SpottedScript_Controls_Navigation_Login_View$get_connect_Details_PlaceErrorSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Details_PlaceErrorSpan');
    },
    get_connect_CaptchaPanel: function SpottedScript_Controls_Navigation_Login_View$get_connect_CaptchaPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_CaptchaPanel');
    },
    get_connect_Captcha_Img: function SpottedScript_Controls_Navigation_Login_View$get_connect_Captcha_Img() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Captcha_Img');
    },
    get_connect_Captcha_Textbox: function SpottedScript_Controls_Navigation_Login_View$get_connect_Captcha_Textbox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Captcha_Textbox');
    },
    get_connect_Captcha_SaveButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_Captcha_SaveButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Captcha_SaveButton');
    },
    get_connect_Captcha_Error: function SpottedScript_Controls_Navigation_Login_View$get_connect_Captcha_Error() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Captcha_Error');
    },
    get_connect_Captcha_BackButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_Captcha_BackButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Captcha_BackButton');
    },
    get_connect_Captcha_CancelButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_Captcha_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Captcha_CancelButton');
    },
    get_connect_DebugPanel: function SpottedScript_Controls_Navigation_Login_View$get_connect_DebugPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_DebugPanel');
    },
    get_connect_Debug_Output: function SpottedScript_Controls_Navigation_Login_View$get_connect_Debug_Output() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Debug_Output');
    },
    get_connect_Debug_LogoutButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_Debug_LogoutButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Debug_LogoutButton');
    },
    get_connect_Debug_DisconnectButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_Debug_DisconnectButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Debug_DisconnectButton');
    },
    get_connect_Debug_AuthButton: function SpottedScript_Controls_Navigation_Login_View$get_connect_Debug_AuthButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Connect_Debug_AuthButton');
    }
}
SpottedScript.Controls.Navigation.Login.Controller.registerClass('SpottedScript.Controls.Navigation.Login.Controller');
SpottedScript.Controls.Navigation.Login.Server.registerClass('SpottedScript.Controls.Navigation.Login.Server');
SpottedScript.Controls.Navigation.Login.View.registerClass('SpottedScript.Controls.Navigation.Login.View');
SpottedScript.Controls.Navigation.Login.Controller.instance = null;
