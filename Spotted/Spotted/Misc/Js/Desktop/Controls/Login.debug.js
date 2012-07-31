//! Login.debug.js
//

(function($) {

Type.registerNamespace('Js.Controls.Login');

////////////////////////////////////////////////////////////////////////////////
// Js.Controls.Login.PageImplementation

window.WhenLoggedIn = function Js_Controls_Login_PageImplementation$WhenLoggedIn(onLogin) {
    /// <param name="onLogin" type="Function">
    /// </param>
    /// <returns type="Boolean"></returns>
    var action = function() {
        if (Js.Controls.Login.Controller.instance.currentIsLoggedIn) {
            onLogin();
        }
        else {
            Js.Controls.Login.Controller.instance.show(function(loggedIn, stateChanged) {
                if (loggedIn) {
                    onLogin();
                }
            });
        }
    };
    if (Js.Controls.Login.Controller.instance.initialised) {
        action();
    }
    else {
        Js.Controls.Login.Controller.instance.whenInitialisedAction = action;
    }
    return false;
}
window.WhenLoggedInButton = function Js_Controls_Login_PageImplementation$WhenLoggedInButton(element) {
    /// <param name="element" type="Object" domElement="true">
    /// </param>
    /// <returns type="Boolean"></returns>
    return WhenLoggedInButtonValidator(element, '');
}
window.WhenLoggedInButtonValidator = function Js_Controls_Login_PageImplementation$WhenLoggedInButtonValidator(element, validators) {
    /// <param name="element" type="Object" domElement="true">
    /// </param>
    /// <param name="validators" type="String">
    /// </param>
    /// <returns type="Boolean"></returns>
    WhenLoggedIn(function() {
        eval('WebForm_DoPostBackWithOptions(new WebForm_PostBackOptions("' + element.id.replaceAll('_', '$') + '", "", true, "' + validators + '", "", false, true));');
    });
    return false;
}
window.WhenLoggedInButtonNoValidation = function Js_Controls_Login_PageImplementation$WhenLoggedInButtonNoValidation(element) {
    /// <param name="element" type="Object" domElement="true">
    /// </param>
    /// <returns type="Boolean"></returns>
    WhenLoggedIn(function() {
        eval('__doPostBack("' + element.id.replaceAll('_', '$') + "\",'');");
    });
    return false;
}
window.WhenLoggedInHtmlButton = function Js_Controls_Login_PageImplementation$WhenLoggedInHtmlButton(element) {
    /// <param name="element" type="Object" domElement="true">
    /// </param>
    /// <returns type="Boolean"></returns>
    WhenLoggedIn(function() {
        eval("__doPostBack('" + element.id.replaceAll('_', '$') + "','');");
    });
    return false;
}
window.WhenLoggedInRadio = function Js_Controls_Login_PageImplementation$WhenLoggedInRadio(element) {
    /// <summary>
    /// Remember this should be followed by "return true;", so that the radio button doesn't reset to the previous value.
    /// </summary>
    /// <param name="element" type="Object" domElement="true">
    /// </param>
    /// <returns type="Boolean"></returns>
    WhenLoggedIn(function() {
        eval("setTimeout('__doPostBack(\\'" + element.id.replaceAll('_', '$') + "\\',\\'\\')', 0);");
    });
    return true;
}
window.WhenLoggedInAnchor = function Js_Controls_Login_PageImplementation$WhenLoggedInAnchor(anchor) {
    /// <param name="anchor" type="Object" domElement="true">
    /// </param>
    /// <returns type="Boolean"></returns>
    LogInTransfer(anchor.href);
    return false;
}
window.ConnectButtonClick = function Js_Controls_Login_PageImplementation$ConnectButtonClick() {
    var action = function() {
        Js.Controls.Login.Controller.instance.show(function(loggedIn, stateChanged) {
            if (stateChanged) {
                if (loggedIn) {
                    window.location.href = window.location.href;
                }
                else {
                    window.location.href = '/';
                }
            }
        });
    };
    if (Js.Controls.Login.Controller.instance.initialised) {
        action();
    }
    else {
        Js.Controls.Login.Controller.instance.whenInitialisedAction = action;
    }
}
window.LogInTransfer = function Js_Controls_Login_PageImplementation$LogInTransfer(url) {
    /// <param name="url" type="String">
    /// </param>
    var action = function() {
        if (Js.Controls.Login.Controller.instance.currentIsLoggedIn) {
            window.location.href = url;
        }
        else {
            Js.Controls.Login.Controller.instance.show(function(loggedIn, stateChanged) {
                if (loggedIn) {
                    window.location.href = url;
                }
            });
        }
    };
    if (Js.Controls.Login.Controller.instance.initialised) {
        action();
    }
    else {
        Js.Controls.Login.Controller.instance.whenInitialisedAction = action;
    }
}
window.IsLoggedIn = function Js_Controls_Login_PageImplementation$IsLoggedIn() {
    /// <returns type="Boolean"></returns>
    return Js.Controls.Login.Controller.instance.currentIsLoggedIn;
}
window.LoginDebug = function Js_Controls_Login_PageImplementation$LoginDebug(text) {
    /// <param name="text" type="String">
    /// </param>
    Js.Controls.Login.Controller.instance.debug(text);
}
window.LoginFacebookReady = function Js_Controls_Login_PageImplementation$LoginFacebookReady() {
    Js.Controls.Login.Controller.instance.facebookReady('LoginFacebookReady');
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.Login.Controller

Js.Controls.Login.Controller = function Js_Controls_Login_Controller(view) {
    /// <param name="view" type="Js.Controls.Login.View">
    /// </param>
    /// <field name="_view" type="Js.Controls.Login.View">
    /// </field>
    /// <field name="_server" type="Js.Controls.Login.Server">
    /// </field>
    /// <field name="instance" type="Js.Controls.Login.Controller" static="true">
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
    /// <field name="whenInitialisedAction" type="Function">
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
    /// <field name="_exitEvent" type="Js.Library.Function">
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
    /// <field name="_detailsPlaceDropDownJ" type="$">
    /// </field>
    /// <field name="_detailsCountryDropDownJ" type="$">
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
    this._cancelledAsyncOperations = {};
    Js.Controls.Login.Controller.instance = this;
    this._view = view;
    this._server = view.server;
    if (Js.Library.Misc.get_browserIsIE()) {
        $(ss.Delegate.create(this, this._initialise));
    }
    else {
        this._initialise();
    }
}
Js.Controls.Login.Controller._readCookie = function Js_Controls_Login_Controller$_readCookie(name) {
    /// <param name="name" type="String">
    /// </param>
    /// <returns type="String"></returns>
    var nameEQ = name + '=';
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        c = c.trim();
        if (!c.indexOf(nameEQ)) {
            return c.substring(nameEQ.length, c.length);
        }
    }
    return '';
}
Js.Controls.Login.Controller.prototype = {
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
    
    _initialise: function Js_Controls_Login_Controller$_initialise() {
        $(this._view.get_connectDialog()).dialog(F.d('autoOpen', false, 'width', 505, 'height', 280, 'modal', true, 'resizable', false, 'zIndex', 990, 'draggable', false, 'closeOnEscape', false, 'open', function(ev, ui) {
            $('.ui-dialog-titlebar-close').hide();
        }));
        this._view.get_connectDialog().style.display = '';
        this._doneControllerInit = true;
        this.facebookReady('initialise');
    },
    
    facebookReady: function Js_Controls_Login_Controller$facebookReady(from) {
        /// <param name="from" type="String">
        /// </param>
        if (this._doneControllerInit && eval('DoneFbAsyncInit')) {
            this._initialAuthCookieHasError = this._getBoolFromBasePage('AuthCookieHasError');
            this._currentAuthCookieHasError = this._initialAuthCookieHasError;
            this.updateAuthDetailsFromDsiCookie();
            FB.Event.subscribe('auth.statusChange', ss.Delegate.create(this, function(statusResponse) {
                this._updateCurrentFacebookLoginStatus(statusResponse);
            }));
            FB.getLoginStatus(ss.Delegate.create(this, function(statusResponse) {
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
                    this._logoutNow(true, ss.Delegate.create(this, function() {
                        this._redirectToHomePage();
                    }), false);
                }
                else if (this.autoLogin) {
                    if (this.currentIsLoggedIn && this._currentAuthUsrK === this.autoLoginUsrK.toString() && !this.autoLoginLogOutFirst) {
                        window.location.href = this.autoLoginRedirectUrl;
                    }
                    else if (this.currentIsLoggedIn) {
                        this.logOutAndDoAction(ss.Delegate.create(this, function() {
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
    
    _autoLoginShowPanel: function Js_Controls_Login_Controller$_autoLoginShowPanel() {
        Js.Controls.Login.Controller.instance.show(ss.Delegate.create(this, function(loggedIn, stateChanged) {
            if (loggedIn) {
                window.location.href = this.autoLoginRedirectUrl;
            }
            else {
                window.location.href = '/';
            }
        }));
    },
    
    _redirectToHomePage: function Js_Controls_Login_Controller$_redirectToHomePage() {
        window.location.href = '/';
    },
    
    show: function Js_Controls_Login_Controller$show(onExit) {
        /// <param name="onExit" type="Js.Library.Function">
        /// </param>
        this._exitEvent = onExit;
        this._openDialogIsLoggedIn = this.currentIsLoggedIn;
        this._changePanel('View.Connect_LoadingPanel');
        this._initialiseForm();
        $(this._view.get_connectDialog()).dialog('open');
        $(this._view.get_connectDialog()).dialog(F.d('close', ss.Delegate.create(this, function() {
            this._exitEvent(this.currentIsLoggedIn, this.currentIsLoggedIn !== this._openDialogIsLoggedIn);
        })));
    },
    
    _initialiseForm: function Js_Controls_Login_Controller$_initialiseForm() {
        if (this.currentIsLoggedIn) {
            this._currentAuthUsrNickName = this._getStringFromBasePage('UsrNickname');
            this._currentAuthUsrLink = this._getStringFromBasePage('UsrLink');
            $(this._view.get_connectDialog()).dialog('open');
            this._showLoggedInPanel((this._currentAuthUsrNickName.length > 0) ? this._currentAuthUsrLink : '???');
        }
        else {
            if (this._currentFacebookConnected) {
                this._closeOnLoggedIn = false;
            }
            $(this._view.get_connectDialog()).dialog('open');
            this._changePanel('View.Connect_LoggedOutPanel');
        }
    },
    
    _facebookEmailMatch: false,
    _facebookEmailMatchToCurrentUser: false,
    _facebookEmailMatchEnhancedSecurity: false,
    _facebookEmailMatchNickName: '',
    _facebookEmailMatchEmail: '',
    _closeOnLoggedIn: false,
    
    _configureFormConnected: function Js_Controls_Login_Controller$_configureFormConnected() {
        var thisAsyncOperation = this._registerStartAsync('Connecting...');
        this._server.getUserByFacebookUID(this.autoLoginUsrK, this.autoLoginUsrLoginString, ss.Delegate.create(this, function(response) {
            if (this._registerEndAsync(thisAsyncOperation)) {
                return;
            }
            if (Js.Library.U.isTrue(response, 'Exception')) {
                $(this._view.get_connectDialog()).dialog('open');
                this._showError(1, 'Internal server error');
            }
            else {
                this._facebookEmailMatch = Js.Library.U.isTrue(response, 'FacebookEmailMatch');
                this._facebookEmailMatchToCurrentUser = Js.Library.U.isTrue(response, 'FacebookEmailMatchToCurrentUser');
                this._facebookEmailMatchEnhancedSecurity = Js.Library.U.isTrue(response, 'EnhancedSecurity');
                var facebookUidMatch = Js.Library.U.isTrue(response, 'FacebookUIDMatch');
                var autoLoginMatch = Js.Library.U.isTrue(response, 'FacebookAutoLoginUsrMatch');
                this._autoLoginNickname = (Js.Library.U.exists(response, 'AutoLoginUsr/NickName')) ? Js.Library.U.get(response, 'AutoLoginUsr/NickName').toString() : '';
                this._autoLoginLink = (Js.Library.U.exists(response, 'AutoLoginUsr/Link')) ? Js.Library.U.get(response, 'AutoLoginUsr/Link').toString() : '';
                this._autoLoginEmail = (Js.Library.U.exists(response, 'AutoLoginUsr/Email')) ? Js.Library.U.get(response, 'AutoLoginUsr/Email').toString() : '';
                this._autoLoginStringMatch = (Js.Library.U.exists(response, 'AutoLoginUsr/LoginStringMatch')) ? Js.Library.U.get(response, 'AutoLoginUsr/LoginStringMatch') : false;
                if (this._facebookAccountNeedsConfirmationBecauseInitiallyFacebookConnectedAndSiteLoggedOut) {
                    this._showConfirmFacebookPanel();
                    return;
                }
                else if (facebookUidMatch) {
                    this._setAuthCookie(Js.Library.U.get(response, 'AuthCookie'), Js.Library.U.get(response, 'AuthUsr'));
                    if (this._closeOnLoggedIn) {
                        this._detectAutoLoginProblem(false);
                    }
                    else {
                        $(this._view.get_connectDialog()).dialog('open');
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
                    $(this._view.get_connectDialog()).dialog('open');
                    this._ensurePanelGenerated('View.Connect_NewAccount_EmailMatchPanel');
                    this._facebookEmailMatchNickName = Js.Library.U.get(response, 'EmailMatchUsr/NickName').toString();
                    this._facebookEmailMatchEmail = Js.Library.U.get(response, 'EmailMatchUsr/Email').toString();
                    if (Js.Library.U.get(response, 'EmailMatchUsr/NickName').toString().length > 0) {
                        this._view.get_connect_NewAccount_EmailMatch_UserLink1().innerHTML = 'Link to ' + Js.Library.U.get(response, 'EmailMatchUsr/Link').toString() + ':';
                    }
                    else {
                        this._view.get_connect_NewAccount_EmailMatch_UserLink1().innerHTML = 'Link to ' + this._facebookEmailMatchEmail + ':';
                    }
                    this._view.get_connect_NewAccount_EmailMatch_BackButton().style.display = (this._facebookAccountConfirmationStepDone) ? '' : 'none';
                    this._changePanel('View.Connect_NewAccount_EmailMatchPanel');
                }
                else {
                    $(this._view.get_connectDialog()).dialog('open');
                    this._ensurePanelGenerated('View.Connect_NewAccount_NoEmailMatchPanel');
                    this._view.get_connect_NewAccount_NoEmailMatch_BackButton().style.display = (this._facebookAccountConfirmationStepDone) ? '' : 'none';
                    this._changePanel('View.Connect_NewAccount_NoEmailMatchPanel');
                }
            }
        }));
    },
    
    _add_View_Connect_LoggedOutPanel: function Js_Controls_Login_Controller$_add_View_Connect_LoggedOutPanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = "\r\n<div id=\"{ClientID}Connect_LoggedOutPanel\" class=\"LoginPanel\" style=\"display:none;\">\r\n\r\n\t<div style=\"position:relative;\" class=\"LoginPanelInner ClearAfter\">\r\n\t\t<div style=\"width:240px; float:left;\" class=\"ColumnWithNoParaAbove\">\r\n\t\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\t\tUse Facebook\r\n\t\t\t</p>\r\n\t\t\t<p>\r\n\t\t\t\tThe easiest way to log in to Don't Stay In:\r\n\t\t\t</p>\r\n\t\t\t<p>\r\n\t\t\t\t<button id=\"{ClientID}Connect_LoggedOut_ConnectButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\">\r\n\t\t\t\t\t<img src=\"/gfx/facebook-small-16.png\" width=\"16\" height=\"16\" border=\"0\" align=\"top\" />\r\n\t\t\t\t\t<span style=\"height:16px;\">Connect with Facebook</span>\r\n\t\t\t\t</button>\r\n\t\t\t</p>\r\n\t\t\t<ul style=\"margin-top:10px;\">\r\n\t\t\t\t<li>\r\n\t\t\t\t\tEasy: just three clicks to sign up\r\n\t\t\t\t</li>\r\n\t\t\t\t<li>\r\n\t\t\t\t\tSimple: use your Facebook password\r\n\t\t\t\t</li>\r\n\t\t\t</ul>\r\n\t\t</div>\r\n\t\t<div style=\"left:220px; width:10px; height:173px; float:left; border-left:dotted 2px #cccccc;\"> </div>\r\n\t\t<div style=\"left:210px; width:220px; float:left;\" class=\"ColumnWithNoParaAbove\">\r\n\t\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\t\tConnect manually\r\n\t\t\t</p>\r\n\t\t\t<p>\r\n\t\t\t\tIf you don't have Facebook access, you can log in manually:\r\n\t\t\t</p>\r\n\t\t\t<p>\r\n\t\t\t\t<button id=\"{ClientID}Connect_LoggedOut_NoFacebookButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\">\r\n\t\t\t\t\t<img src=\"/gfx/dsi-tiny-16.png\" width=\"16\" height=\"16\" border=\"0\" align=\"top\" />\r\n\t\t\t\t\t<span style=\"height:16px;\">Connect manually</span>\r\n\t\t\t\t</button>\r\n\t\t\t</p>\r\n\t\t\t<ul style=\"margin-top:10px;\">\r\n\t\t\t\t<li>\r\n\t\t\t\t\tWorks even if Facebook is blocked\r\n\t\t\t\t</li>\r\n\t\t\t</ul>\r\n\t\t</div>\r\n\t</div>\r\n\t<p>\r\n\t\t<button id=\"{ClientID}Connect_LoggedOut_CancelButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Cancel</button>\r\n\t</p>\r\n</div>\r\n";
        this._addChild(s);
        $(this._view.get_connect_LoggedOut_ConnectButton()).click(ss.Delegate.create(this, this._connectButtonClick));
        $(this._view.get_connect_LoggedOut_CancelButton()).click(ss.Delegate.create(this, this._cancelButtonClick));
        $(this._view.get_connect_LoggedOut_NoFacebookButton()).click(ss.Delegate.create(this, this._noFacebookButtonClick));
        return this._view.get_connect_LoggedOutPanel();
    },
    
    _connectButtonClickAsyncOperation: 0,
    
    _connectButtonClick: function Js_Controls_Login_Controller$_connectButtonClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._connectButtonClickInternal();
    },
    
    _connectButtonClickInternal: function Js_Controls_Login_Controller$_connectButtonClickInternal() {
        this._closeOnLoggedIn = true;
        if (this._currentFacebookConnected) {
            this._configureFormConnected();
        }
        else {
            this._changePanel('View.Connect_ConnectingPanel');
            this._connectButtonClickAsyncOperation = this._registerStartAsyncGeneric('Connecting...', false, false);
            FB.login(ss.Delegate.create(this, function(loginResponse) {
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
    
    _noFacebookButtonClick: function Js_Controls_Login_Controller$_noFacebookButtonClick(e) {
        /// <param name="e" type="jQueryEvent">
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
                        countryKFromIp = parseInt(this._getStringFromBasePage('CountryKFromIp'));
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
    
    _add_View_Connect_LoggedOut_NoFacebook_ChoosePanel: function Js_Controls_Login_Controller$_add_View_Connect_LoggedOut_NoFacebook_ChoosePanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = "\r\n<div id=\"{ClientID}Connect_LoggedOut_NoFacebook_ChoosePanel\" class=\"LoginPanel\" style=\"display:none;\">\r\n\t<div style=\"position:relative;\" class=\"LoginPanelInner ClearAfter\">\r\n\t\t<p>\r\n\t\t\tIf you can't use Facebook to log in, you can log in with your Don't Stay In details.\r\n\t\t</p>\r\n\t\t<div style=\"width:240px; float:left;\">\r\n\t\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\t\tLog in\r\n\t\t\t</p>\r\n\t\t\t<p>\r\n\t\t\t\tIf you already have an account:\r\n\t\t\t</p>\r\n\t\t\t<p>\r\n\t\t\t\t<button id=\"{ClientID}Connect_LoggedOut_NoFacebook_Choose_LoginButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\">Log in</button>\r\n\t\t\t</p>\r\n\t\t</div>\r\n\t\t<div style=\"left:220px; width:10px; height:150px; float:left; border-left:dotted 2px #cccccc;\"> </div>\r\n\t\t<div style=\"left:210px; width:220px; float:left;\">\r\n\t\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\t\tSign up\r\n\t\t\t</p>\r\n\t\t\t<p>\r\n\t\t\t\tIf you've not used Don't Stay In:\r\n\t\t\t</p>\r\n\t\t\t<p>\r\n\t\t\t\t<button id=\"{ClientID}Connect_LoggedOut_NoFacebook_Choose_SignupButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\">Sign up</button>\r\n\t\t\t</p>\r\n\t\t</div>\r\n\t</div>\r\n\t<p>\r\n\t\t<button id=\"{ClientID}Connect_LoggedOut_NoFacebook_Choose_BackButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:left;\">Back</button>\r\n\r\n\t\t<button id=\"{ClientID}Connect_LoggedOut_NoFacebook_Choose_CancelButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Cancel</button>\r\n\t</p>\r\n</div>\r\n";
        this._addChild(s);
        $(this._view.get_connect_LoggedOut_NoFacebook_Choose_LoginButton()).click(ss.Delegate.create(this, function(e) {
            this._showNoFacebookLoginPanel('ChoosePanel', '');
        }));
        $(this._view.get_connect_LoggedOut_NoFacebook_Choose_SignupButton()).click(ss.Delegate.create(this, function(e) {
            this._showNoFacebookSignUp1Panel('NoFacebookNewAccount');
        }));
        $(this._view.get_connect_LoggedOut_NoFacebook_Choose_BackButton()).click(ss.Delegate.create(this, function(e) {
            this._changePanelOnClick('View.Connect_LoggedOutPanel', e);
        }));
        $(this._view.get_connect_LoggedOut_NoFacebook_Choose_CancelButton()).click(ss.Delegate.create(this, this._cancelButtonClick));
        return this._view.get_connect_LoggedOut_NoFacebook_ChoosePanel();
    },
    
    _add_View_Connect_LoggedOut_NoFacebook_LoginPanel: function Js_Controls_Login_Controller$_add_View_Connect_LoggedOut_NoFacebook_LoginPanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = "\r\n<div id=\"{ClientID}Connect_LoggedOut_NoFacebook_LoginPanel\" class=\"LoginPanel\" style=\"display:none;\">\r\n\t<div style=\"position:relative;\" class=\"LoginPanelInner ClearAfter\">\r\n\t\t<div style=\"width:220px; float:left;\" class=\"ColumnWithNoParaAbove\">\r\n\t\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\t\tLog in\r\n\t\t\t</p>\r\n\t\t\t<p style=\"margin-bottom:-5px;\">\r\n\t\t\t\tNickname or email:\r\n\t\t\t</p>\r\n\t\t\t<p style=\"position:relative; height:25px; line-height:25px;\">\r\n\t\t\t\t<input id=\"{ClientID}Connect_LoggedOut_NoFacebook_Login_UsernameTextbox\" type=\"text\" class=\"xui-state-default ui-corner-all\" style=\"padding-left:5px; height:20px; width:150px;\" />\r\n\t\t\t</p>\r\n\t\t\t<p style=\"margin-bottom:-5px; margin-top:0px;\">\r\n\t\t\t\tPassword:\r\n\t\t\t</p>\r\n\t\t\t<p style=\"position:relative; height:25px; line-height:25px;\">\r\n\t\t\t\t<input id=\"{ClientID}Connect_LoggedOut_NoFacebook_Login_PasswordTextbox\" type=\"password\" class=\"xui-state-default ui-corner-all\" style=\"padding-left:5px; height:20px; width:150px; border:1px solid #cccccc;\" />\r\n\t\t\t</p>\r\n\t\t\t<p>\r\n\t\t\t\t<button id=\"{ClientID}Connect_LoggedOut_NoFacebook_Login_LoginButton\" style=\"float:left;\" class=\"ui-state-default ui-corner-all Pointer BigButton\">Log in</button>\r\n\t\t\t\t<p id=\"{ClientID}Connect_LoggedOut_NoFacebook_Login_Error\" class=\"ForegroundAttentionRed BackgroundWhite\" style=\"display:none; position:absolute; left:65px; float:left; width:400px; height:50px; font-weight:bold; line-height:15px;\">\r\n\t\t\t\t\tCan't find those details. Remember to enter your password from Don't Stay In, not your Facebook password.\r\n\t\t\t\t</p>\r\n\t\t\t</p>\r\n\t\t</div>\r\n\t\t<div style=\"left:220px; width:10px; height:173px; float:left; border-left:dotted 2px #cccccc;\"> </div>\r\n\t\t<div style=\"left:230px; width:220px; float:left;\" class=\"ColumnWithNoParaAbove\">\r\n\t\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\t\tNo password?\r\n\t\t\t</p>\r\n\t\t\t<p>\r\n\t\t\t\tIf you signed up using Facebook you may not have a password.\r\n\t\t\t</p>\r\n\t\t\t<p>\r\n\t\t\t\t<button id=\"{ClientID}Connect_LoggedOut_NoFacebook_Login_NoPasswordButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\">Get a password</button>\r\n\t\t\t</p>\r\n\t\t</div>\r\n\t</div>\r\n\t<p>\r\n\t\t<button id=\"{ClientID}Connect_LoggedOut_NoFacebook_Login_BackButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:left;\">Back</button>\r\n\r\n\t\t<button id=\"{ClientID}Connect_LoggedOut_NoFacebook_Login_CancelButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Cancel</button>\r\n\t\t<button id=\"{ClientID}Connect_LoggedOut_NoFacebook_Login_ForgottonPasswordButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Forgot your password?</button>\r\n\t</p>\r\n</div>";
        this._addChild(s);
        $(this._view.get_connect_LoggedOut_NoFacebook_Login_LoginButton()).click(ss.Delegate.create(this, this._noFacebookLoginButtonClick));
        $(this._view.get_connect_LoggedOut_NoFacebook_Login_BackButton()).click(ss.Delegate.create(this, this._noFacebookLoginBackClick));
        $(this._view.get_connect_LoggedOut_NoFacebook_Login_CancelButton()).click(ss.Delegate.create(this, this._cancelButtonClick));
        $(this._view.get_connect_LoggedOut_NoFacebook_Login_NoPasswordButton()).click(ss.Delegate.create(this, function(e) {
            this._ensurePanelGenerated('View.Connect_LoggedOut_NoFacebook_PasswordResetPanel');
            this._view.get_connect_LoggedOut_NoFacebook_PasswordReset_Title().innerHTML = 'No password?';
            this._changePanelOnClick('View.Connect_LoggedOut_NoFacebook_PasswordResetPanel', e);
        }));
        $(this._view.get_connect_LoggedOut_NoFacebook_Login_ForgottonPasswordButton()).click(ss.Delegate.create(this, function(e) {
            this._ensurePanelGenerated('View.Connect_LoggedOut_NoFacebook_PasswordResetPanel');
            this._view.get_connect_LoggedOut_NoFacebook_PasswordReset_Title().innerHTML = 'Forgot your password?';
            this._changePanelOnClick('View.Connect_LoggedOut_NoFacebook_PasswordResetPanel', e);
        }));
        this._defaultButton(this._view.get_connect_LoggedOut_NoFacebook_Login_UsernameTextbox(), this._view.get_connect_LoggedOut_NoFacebook_Login_LoginButton());
        this._defaultButton(this._view.get_connect_LoggedOut_NoFacebook_Login_PasswordTextbox(), this._view.get_connect_LoggedOut_NoFacebook_Login_LoginButton());
        return this._view.get_connect_LoggedOut_NoFacebook_LoginPanel();
    },
    
    _noFacebookLoginBackClick: function Js_Controls_Login_Controller$_noFacebookLoginBackClick(e) {
        /// <param name="e" type="jQueryEvent">
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
    
    _noFacebookLoginButtonClick: function Js_Controls_Login_Controller$_noFacebookLoginButtonClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._noFacebookLogin('NoFacebookLogin', false, false, false);
    },
    
    _noFacebookLogin: function Js_Controls_Login_Controller$_noFacebookLogin(source, sendDetailsPanelData, sendNoFacebookSignupPanelData, autoLogin) {
        /// <param name="source" type="String">
        /// </param>
        /// <param name="sendDetailsPanelData" type="Boolean">
        /// </param>
        /// <param name="sendNoFacebookSignupPanelData" type="Boolean">
        /// </param>
        /// <param name="autoLogin" type="Boolean">
        /// </param>
        var thisAsyncOperation = this._registerStartAsync('Logging in...');
        this._server.noFacebookLogin((autoLogin) ? '' : this._view.get_connect_LoggedOut_NoFacebook_Login_UsernameTextbox().value, (autoLogin) ? '' : this._view.get_connect_LoggedOut_NoFacebook_Login_PasswordTextbox().value, this._getCaptchaData(), (sendNoFacebookSignupPanelData) ? this._getNoFacebookSignupPanelData() : null, (sendDetailsPanelData) ? this._getDetailsPanelData() : null, autoLogin, ((autoLogin) ? this.autoLoginUsrK : 0), ((autoLogin) ? this.autoLoginUsrLoginString : ''), ss.Delegate.create(this, function(response) {
            if (this._registerEndAsync(thisAsyncOperation)) {
                return;
            }
            if (Js.Library.U.isTrue(response, 'Exception')) {
                this._showError(4, 'Internal server error.');
            }
            else {
                this._ensurePanelGenerated('View.Connect_LoggedOut_NoFacebook_LoginPanel');
                this._view.get_connect_LoggedOut_NoFacebook_Login_Error().style.display = 'none';
                if (Js.Library.U.isTrue(response, 'Error')) {
                    if (Js.Library.U.isTrue(response, 'HasNullPassword')) {
                        this._changePanel('View.Connect_LoggedOut_NoFacebook_LoginNoPasswordPanel');
                    }
                    else {
                        this._view.get_connect_LoggedOut_NoFacebook_Login_Error().style.display = '';
                    }
                }
                else {
                    if (Js.Library.U.isTrue(response, 'IsSkeleton')) {
                        this._showNoFacebookSignUp2Panel('NoFacebookLoginSkeleton', Js.Library.U.get(response, 'Details'));
                    }
                    else if (Js.Library.U.isTrue(response, 'NeedsConfirmation')) {
                        this._showDetailsPanel('NoFacebookLoginFacebookNotConfirmed', Js.Library.U.get(response, 'Details'));
                    }
                    else if (Js.Library.U.isTrue(response, 'NeedsCaptcha')) {
                        this._showCaptchaPanel(source, response);
                    }
                    else {
                        this._setAuthCookie(Js.Library.U.get(response, 'AuthCookie'), Js.Library.U.get(response, 'AuthUsr'));
                        $(this._view.get_connectDialog()).dialog('close');
                    }
                }
            }
        }));
    },
    
    _noFacebookLoginPanelSource: '',
    
    _showNoFacebookLoginPanel: function Js_Controls_Login_Controller$_showNoFacebookLoginPanel(noFacebookLoginPanelSource, emailPreset) {
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
    
    _add_View_Connect_LoggedOut_NoFacebook_LoginNoPasswordPanel: function Js_Controls_Login_Controller$_add_View_Connect_LoggedOut_NoFacebook_LoginNoPasswordPanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = "\r\n<div id=\"{ClientID}Connect_LoggedOut_NoFacebook_LoginNoPasswordPanel\" class=\"LoginPanel\" style=\"display:none;\">\r\n\t<div class=\"LoginPanelInner\">\r\n\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\tOops!\r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\tYour account doesn't have a password. You probably created it using our Facebook connect feature. We've \r\n\t\t\tsent you an email with a password reset link - with this you'll be able to create a password.\r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\t<button id=\"{ClientID}Connect_LoggedOut_NoFacebook_LoginNoPassword_TryAgainButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\">Try again</button>\r\n\t\t</p>\r\n\t</div>\r\n\t<p>\r\n\t\t<button id=\"{ClientID}Connect_LoggedOut_NoFacebook_LoginNoPassword_CancelButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Cancel</button>\r\n\t</p>\r\n</div>\r\n";
        this._addChild(s);
        $(this._view.get_connect_LoggedOut_NoFacebook_LoginNoPassword_CancelButton()).click(ss.Delegate.create(this, this._cancelButtonClick));
        $(this._view.get_connect_LoggedOut_NoFacebook_LoginNoPassword_TryAgainButton()).click(ss.Delegate.create(this, function(e) {
            this._changePanelOnClick('View.Connect_LoggedOut_NoFacebook_LoginPanel', e);
        }));
        return this._view.get_connect_LoggedOut_NoFacebook_LoginNoPasswordPanel();
    },
    
    _add_View_Connect_LoggedOut_NoFacebook_PasswordResetPanel: function Js_Controls_Login_Controller$_add_View_Connect_LoggedOut_NoFacebook_PasswordResetPanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = "\r\n<div id=\"{ClientID}Connect_LoggedOut_NoFacebook_PasswordResetPanel\" class=\"LoginPanel\" style=\"display:none;\">\r\n\t<div class=\"LoginPanelInner\">\r\n\t\t<p id=\"{ClientID}Connect_LoggedOut_NoFacebook_PasswordReset_Title\" class=\"LoginPanelTitle\">\r\n\t\t\tDon't have a password?\r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\tEnter your Don't Stay In username or email below and we'll send you a password reset link by email:\r\n\t\t</p>\r\n\t\t<p style=\"position:relative; height:25px; line-height:25px;\">\r\n\t\t\tNickname or email:\r\n\t\t\t<input id=\"{ClientID}Connect_LoggedOut_NoFacebook_PasswordReset_UsernameTextbox\" type=\"text\" class=\"xui-state-default ui-corner-all\" style=\"padding-left:5px; height:20px; left:140px; top:0px; position:absolute; width:150px;\" />\r\n\t\t</p>\r\n\t\t<p style=\"position:relative; height:25px; line-height:25px;\">\r\n\t\t\t<button id=\"{ClientID}Connect_LoggedOut_NoFacebook_PasswordReset_SendLinkButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\" style=\"left:140px; top:0px; position:absolute; float:left;\">Send password reset link</button>\r\n\t\t</p>\r\n\t\t<p style=\"position:relative;\">\r\n\t\t\t<span id=\"{ClientID}Connect_LoggedOut_NoFacebook_PasswordReset_MessageLabel\" class=\"ForegroundAttentionBlue\" style=\"left:140px; top:0px; position:absolute; float:left; font-weight:bold;\"></span>\r\n\t\t\t<span id=\"{ClientID}Connect_LoggedOut_NoFacebook_PasswordReset_ErrorLabel\" class=\"ForegroundAttentionRed\" style=\"left:140px; top:0px; position:absolute; float:left; font-weight:bold;\"></span>\r\n\t\t</p>\r\n\t</div>\r\n\t<p>\r\n\t\t<button id=\"{ClientID}Connect_LoggedOut_NoFacebook_PasswordReset_BackButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:left;\">Back</button>\r\n\t\t<button id=\"{ClientID}Connect_LoggedOut_NoFacebook_PasswordReset_CancelButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Cancel</button>\r\n\t</p>\r\n</div>\r\n";
        this._addChild(s);
        $(this._view.get_connect_LoggedOut_NoFacebook_PasswordReset_BackButton()).click(ss.Delegate.create(this, function(e) {
            this._changePanelOnClick('View.Connect_LoggedOut_NoFacebook_LoginPanel', e);
        }));
        $(this._view.get_connect_LoggedOut_NoFacebook_PasswordReset_CancelButton()).click(ss.Delegate.create(this, this._cancelButtonClick));
        $(this._view.get_connect_LoggedOut_NoFacebook_PasswordReset_SendLinkButton()).click(ss.Delegate.create(this, this._noFacebookNoPasswordSendLinkClick));
        this._defaultButton(this._view.get_connect_LoggedOut_NoFacebook_PasswordReset_UsernameTextbox(), this._view.get_connect_LoggedOut_NoFacebook_PasswordReset_SendLinkButton());
        return this._view.get_connect_LoggedOut_NoFacebook_PasswordResetPanel();
    },
    
    _noFacebookNoPasswordSendLinkClick: function Js_Controls_Login_Controller$_noFacebookNoPasswordSendLinkClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._view.get_connect_LoggedOut_NoFacebook_PasswordReset_ErrorLabel().style.display = 'none';
        this._view.get_connect_LoggedOut_NoFacebook_PasswordReset_MessageLabel().style.display = 'none';
        if (!this._view.get_connect_LoggedOut_NoFacebook_PasswordReset_UsernameTextbox().value.trim().length) {
            this._view.get_connect_LoggedOut_NoFacebook_PasswordReset_ErrorLabel().style.display = '';
            this._view.get_connect_LoggedOut_NoFacebook_PasswordReset_ErrorLabel().innerHTML = 'Please enter your email or nickname.';
            return;
        }
        var thisAsyncOperation = this._registerStartAsync('Sending password reset link...');
        this._server.sendPassword(this._view.get_connect_LoggedOut_NoFacebook_PasswordReset_UsernameTextbox().value, ss.Delegate.create(this, function(response) {
            if (this._registerEndAsync(thisAsyncOperation)) {
                return;
            }
            if (Js.Library.U.isTrue(response, 'Exception')) {
                this._showError(12, 'Internal server error.');
            }
            else {
                if (Js.Library.U.isTrue(response, 'Done')) {
                    this._view.get_connect_LoggedOut_NoFacebook_PasswordReset_MessageLabel().style.display = '';
                    this._view.get_connect_LoggedOut_NoFacebook_PasswordReset_MessageLabel().innerHTML = 'We have sent you a password reset email.';
                }
                else {
                    this._view.get_connect_LoggedOut_NoFacebook_PasswordReset_ErrorLabel().style.display = '';
                    this._view.get_connect_LoggedOut_NoFacebook_PasswordReset_ErrorLabel().innerHTML = "Can't find that username or email.";
                }
            }
        }));
    },
    
    _add_View_Connect_LoggedOut_NoFacebook_SignUp1Panel: function Js_Controls_Login_Controller$_add_View_Connect_LoggedOut_NoFacebook_SignUp1Panel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = '\r\n<div id="{ClientID}Connect_LoggedOut_NoFacebook_SignUp1Panel" class="LoginPanel" style="display:none;">\r\n\t<div class="LoginPanelInner">\r\n\t\t<p class="LoginPanelTitle">\r\n\t\t\tEnter your details...\r\n\t\t</p>\r\n\t\t<p id="{ClientID}Connect_LoggedOut_NoFacebook_SignUp1_EmailPara" style="position:relative; height:25px; line-height:25px;">\r\n\t\t\tEmail:\r\n\t\t\t<input id="{ClientID}Connect_LoggedOut_NoFacebook_SignUp1_EmailTextbox" type="text" style="padding-left:5px; height:20px; left:140px; top:0px; position:absolute; width:210px; height:25px; line-height:25px;" />\r\n\t\t</p>\r\n\t\t<p style="position:relative; height:25px; line-height:25px;">\r\n\t\t\tChoose a password:\r\n\t\t\t<input id="{ClientID}Connect_LoggedOut_NoFacebook_SignUp1_Password1Textbox" type="password" style="padding-left:5px; height:20px; left:140px; top:0px; position:absolute; width:210px; height:25px; line-height:25px; border:1px solid #cccccc;" />\r\n\t\t</p>\r\n\t\t<p style="position:relative; height:25px; line-height:25px;">\r\n\t\t\tConfirm your password:\r\n\t\t\t<input id="{ClientID}Connect_LoggedOut_NoFacebook_SignUp1_Password2Textbox" type="password" style="padding-left:5px; height:20px; left:140px; top:0px; position:absolute; width:210px; height:25px; line-height:25px; border:1px solid #cccccc;" />\r\n\t\t</p>\r\n\t\t<p style="position:relative; height:25px; line-height:25px;">\r\n\t\t\t<button id="{ClientID}Connect_LoggedOut_NoFacebook_SignUp1_SaveButton" class="ui-state-default ui-corner-all Pointer BigButton" style="left:140px; top:0px; position:absolute; width:50px; ">Save</button>\r\n\t\t\t<span id="{ClientID}Connect_LoggedOut_NoFacebook_SignUp1_ErrorLabel" class="ForegroundAttentionRed" style="left:200px; position:absolute; font-weight:bold; top:7px;"></span>\r\n\t\t</p>\r\n\t</div>\r\n\t<p style="position:relative;">\r\n\t\t<button id="{ClientID}Connect_LoggedOut_NoFacebook_SignUp1_BackButton" class="ui-state-default ui-corner-all Pointer SmallButton" style="float:left; position:absolute; left:0px;">Back</button>\r\n\r\n\t\t<button id="{ClientID}Connect_LoggedOut_NoFacebook_SignUp1_CancelButton" class="ui-state-default ui-corner-all Pointer SmallButton" style="float:right;">Cancel</button>\r\n\t</p>\r\n</div>\r\n';
        this._addChild(s);
        $(this._view.get_connect_LoggedOut_NoFacebook_SignUp1_CancelButton()).click(ss.Delegate.create(this, this._cancelButtonClick));
        $(this._view.get_connect_LoggedOut_NoFacebook_SignUp1_BackButton()).click(ss.Delegate.create(this, function(e) {
            this._changePanelOnClick('View.Connect_LoggedOut_NoFacebook_ChoosePanel', e);
        }));
        $(this._view.get_connect_LoggedOut_NoFacebook_SignUp1_SaveButton()).click(ss.Delegate.create(this, this._noFacebookSignup1SaveClick));
        this._defaultButton(this._view.get_connect_LoggedOut_NoFacebook_SignUp1_EmailTextbox(), this._view.get_connect_LoggedOut_NoFacebook_SignUp1_SaveButton());
        this._defaultButton(this._view.get_connect_LoggedOut_NoFacebook_SignUp1_Password1Textbox(), this._view.get_connect_LoggedOut_NoFacebook_SignUp1_SaveButton());
        this._defaultButton(this._view.get_connect_LoggedOut_NoFacebook_SignUp1_Password2Textbox(), this._view.get_connect_LoggedOut_NoFacebook_SignUp1_SaveButton());
        return this._view.get_connect_LoggedOut_NoFacebook_SignUp1Panel();
    },
    
    _noFacebookSignup1SaveClick: function Js_Controls_Login_Controller$_noFacebookSignup1SaveClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._view.get_connect_LoggedOut_NoFacebook_SignUp1_ErrorLabel().innerHTML = '';
        if (this._noFacebookSignUp1PanelSource === 'NoFacebookNewAccount') {
            if (!this._view.get_connect_LoggedOut_NoFacebook_SignUp1_EmailTextbox().value.length) {
                this._view.get_connect_LoggedOut_NoFacebook_SignUp1_ErrorLabel().innerHTML = 'Enter an email';
                return;
            }
            var emailRegex = new RegExp('^[^\\@\\s]+\\@[A-Za-z0-9\\-]{1}[.A-Za-z0-9\\-]+\\.[.A-Za-z0-9\\-]*[A-Za-z0-9]$', 'g');
            if (!emailRegex.test(this._view.get_connect_LoggedOut_NoFacebook_SignUp1_EmailTextbox().value)) {
                this._view.get_connect_LoggedOut_NoFacebook_SignUp1_ErrorLabel().innerHTML = 'Check your email';
                return;
            }
        }
        if (!this._view.get_connect_LoggedOut_NoFacebook_SignUp1_Password1Textbox().value.length) {
            this._view.get_connect_LoggedOut_NoFacebook_SignUp1_ErrorLabel().innerHTML = 'Enter a password';
            return;
        }
        if (this._view.get_connect_LoggedOut_NoFacebook_SignUp1_Password1Textbox().value.length < 4) {
            this._view.get_connect_LoggedOut_NoFacebook_SignUp1_ErrorLabel().innerHTML = 'Password is too short';
            return;
        }
        if (this._view.get_connect_LoggedOut_NoFacebook_SignUp1_Password1Textbox().value !== this._view.get_connect_LoggedOut_NoFacebook_SignUp1_Password2Textbox().value) {
            this._view.get_connect_LoggedOut_NoFacebook_SignUp1_ErrorLabel().innerHTML = "Passwords don't match";
            return;
        }
        if (this._noFacebookSignUp1PanelSource === 'NoFacebookNewAccount') {
            var thisAsyncOperation = this._registerStartAsync('Checking email...');
            this._server.checkEmail(this._view.get_connect_LoggedOut_NoFacebook_SignUp1_EmailTextbox().value, ss.Delegate.create(this, function(response) {
                if (this._registerEndAsync(thisAsyncOperation)) {
                    return;
                }
                if (Js.Library.U.isTrue(response, 'Exception')) {
                    this._showError(7, 'Internal server error');
                }
                else {
                    var emailFound = Js.Library.U.get(response, 'Found');
                    if (emailFound) {
                        var thisAsyncOperationSendPassword = this._registerStartAsync('Checking email...');
                        this._server.sendPassword(this._view.get_connect_LoggedOut_NoFacebook_SignUp1_EmailTextbox().value, ss.Delegate.create(this, function(responseSendPassword) {
                            if (this._registerEndAsync(thisAsyncOperationSendPassword)) {
                                return;
                            }
                            if (Js.Library.U.isTrue(responseSendPassword, 'Exception')) {
                                this._showError(12, 'Internal server error.');
                            }
                            else {
                                if (Js.Library.U.isTrue(responseSendPassword, 'Done')) {
                                    this._view.get_connect_LoggedOut_NoFacebook_SignUp1_ErrorLabel().innerHTML = "This email is already in our database. We've sent you a password reset email.";
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
    
    _noFacebookSignup1BackClick: function Js_Controls_Login_Controller$_noFacebookSignup1BackClick(e) {
        /// <param name="e" type="jQueryEvent">
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
    
    _showNoFacebookSignUp1Panel: function Js_Controls_Login_Controller$_showNoFacebookSignUp1Panel(source) {
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
    
    _add_View_Connect_LoggedOut_NoFacebook_SignUp2Panel: function Js_Controls_Login_Controller$_add_View_Connect_LoggedOut_NoFacebook_SignUp2Panel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = '\r\n<div id="{ClientID}Connect_LoggedOut_NoFacebook_SignUp2Panel" class="LoginPanel" style="display:none;">\r\n\t<div class="LoginPanelInner">\r\n\t\t<p class="LoginPanelTitle">\r\n\t\t\tEnter your details...\r\n\t\t</p>\r\n\t\t<p style="position:relative; height:25px; line-height:25px;">\r\n\t\t\tReal name:\r\n\t\t\t<input id="{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextbox" type="text" style="padding-left:5px; height:20px; left:140px; top:0px; position:absolute; width:100px; height:25px; line-height:25px;" />\r\n\t\t\t<input id="{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_LastNameTextbox" type="text" style="padding-left:5px; height:20px; left:250px; top:0px; position:absolute; width:100px; height:25px; line-height:25px;" />\r\n\t\t</p>\r\n\t\t<p style="position:relative; height:25px; line-height:25px;">\r\n\t\t\tNickname:\r\n\t\t\t<input id="{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox" type="text" style="padding-left:5px; height:20px; left:140px; top:0px; position:absolute; width:210px; height:25px; line-height:25px;" />\r\n\t\t</p>\r\n\t\t<p style="position:relative; height:25px; line-height:25px;">\r\n\t\t\tDate of birth:\r\n\t\t\t<span style="left:140px; top:0px; position:absolute;">\r\n\t\t\t\t<select id="{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDown" class="xui-state-default ui-corner-all" style="padding-left:5px; height:25px; line-height:25px;"></select>\r\n\t\t\t\t<select id="{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDown" class="xui-state-default ui-corner-all" style="padding-left:5px; height:25px; line-height:25px;"></select>\r\n\t\t\t\t<select id="{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDown" class="xui-state-default ui-corner-all" style="padding-left:5px; height:25px; line-height:25px;"></select>\r\n\t\t\t</span>\r\n\t\t</p>\r\n\t\t<p style="position:relative; height:25px; line-height:25px;">\r\n\t\t\tSex:\r\n\t\t\t<span style="left:140px; top:0px; position:absolute;">\r\n\t\t\t\t<input id="{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_SexMaleRadio" type="radio" name="{ClientID}Connect_LoggedOut_NoFacebook_SignUp1_Sex" />\r\n\t\t\t\t<label for="{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_SexMaleRadio"> Male</label>\r\n\t\t\t\t<input id="{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_SexFemaleRadio" type="radio" name="{ClientID}Connect_LoggedOut_NoFacebook_SignUp1_Sex" />\r\n\t\t\t\t<label for="{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_SexFemaleRadio"> Female</label>\r\n\t\t\t</span>\r\n\t\t</p>\r\n\t\t<p style="position:relative; height:25px; line-height:25px;">\r\n\t\t\t<button id="{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_SaveButton" class="ui-state-default ui-corner-all Pointer BigButton" style="left:140px; top:0px; position:absolute; width:50px;">Save</button>\r\n\t\t\t<span id="{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel" class="ForegroundAttentionRed" style="left:200px; position:absolute; font-weight:bold; top:7px;"></span>\r\n\t\t</p>\r\n\t</div>\r\n\t<p style="position:relative;">\r\n\t\t<button id="{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_BackButton" class="ui-state-default ui-corner-all Pointer SmallButton" style="float:left; position:absolute; left:0px;">Back</button>\r\n\r\n\t\t<button id="{ClientID}Connect_LoggedOut_NoFacebook_SignUp2_CancelButton" class="ui-state-default ui-corner-all Pointer SmallButton" style="float:right;">Cancel</button>\r\n\t</p>\r\n</div>\r\n';
        this._addChild(s);
        $(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextbox()).keyup(ss.Delegate.create(this, this._noFacebookSignup2NameKeyUp));
        $(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_LastNameTextbox()).keyup(ss.Delegate.create(this, this._noFacebookSignup2NameKeyUp));
        $(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox()).keyup(ss.Delegate.create(this, this._noFacebookSignup2NicknameKeyUp));
        $(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_CancelButton()).click(ss.Delegate.create(this, this._cancelButtonClick));
        $(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_BackButton()).click(ss.Delegate.create(this, this._noFacebookSignup2BackClick));
        $(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_SaveButton()).click(ss.Delegate.create(this, this._noFacebookSignup2SaveClick));
        if (!this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDown().options.length) {
            this._addOption('-1', '', this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDown());
            for (var i = 1; i <= 31; i++) {
                this._addOption(i.toString(), i.toString(), this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDown());
            }
        }
        if (!this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDown().options.length) {
            this._addOption('-1', '', this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDown());
            var months = [ 'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec' ];
            for (var i = 0; i < months.length; i++) {
                this._addOption(i.toString(), months[i], this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDown());
            }
        }
        if (!this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDown().options.length) {
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
    
    _noFacebookSignup2BackClick: function Js_Controls_Login_Controller$_noFacebookSignup2BackClick(e) {
        /// <param name="e" type="jQueryEvent">
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
    
    _noFacebookSignup2NameKeyUp: function Js_Controls_Login_Controller$_noFacebookSignup2NameKeyUp(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        if ((!this._noFacebookSignup2NicknameHasEntry || !this._view.get_connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox().value) && this._view.get_connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextbox().value.length > 0 && this._view.get_connect_LoggedOut_NoFacebook_SignUp2_LastNameTextbox().value.length > 0) {
            var nickTest = this._view.get_connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextbox().value + '-' + this._view.get_connect_LoggedOut_NoFacebook_SignUp2_LastNameTextbox().value;
            if (this._previousNicknameTest !== nickTest) {
                this._previousNicknameTest = nickTest;
                var thisAsyncOperation = this._registerStartAsync('Suggesting nickname...');
                this._server.getUniqueNickName(nickTest, this._noFacebookSignUp2PanelLoginUsrK, ss.Delegate.create(this, function(response) {
                    if (this._registerEndAsync(thisAsyncOperation)) {
                        return;
                    }
                    if (Js.Library.U.isTrue(response, 'Exception')) {
                    }
                    else {
                        var newNickname = Js.Library.U.get(response, 'Nickname').toString();
                        this._view.get_connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox().value = newNickname;
                    }
                }));
            }
        }
    },
    
    _noFacebookSignup2NicknameHasEntry: false,
    
    _noFacebookSignup2NicknameKeyUp: function Js_Controls_Login_Controller$_noFacebookSignup2NicknameKeyUp(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        this._noFacebookSignup2NicknameHasEntry = true;
    },
    
    _noFacebookSignup2SaveClick: function Js_Controls_Login_Controller$_noFacebookSignup2SaveClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._view.get_connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel().innerHTML = '';
        if (!this._view.get_connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextbox().value.length) {
            this._view.get_connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel().innerHTML = 'Enter your first name';
            return;
        }
        if (!this._view.get_connect_LoggedOut_NoFacebook_SignUp2_LastNameTextbox().value.length) {
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
        if (!this._view.get_connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox().value.length) {
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
        if (parseInt(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDown().value) < 0 || parseInt(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDown().value) < 0 || parseInt(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDown().value) < 0) {
            this._view.get_connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel().innerHTML = 'Enter your date of birth';
            return;
        }
        var d = new Date();
        d.setFullYear(parseInt(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDown().value));
        d.setMonth(parseInt(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDown().value));
        d.setDate(parseInt(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDown().value));
        if (d.getFullYear() !== parseInt(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDown().value) || d.getMonth() !== parseInt(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDown().value) || d.getDate() !== parseInt(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDown().value)) {
            this._view.get_connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel().innerHTML = 'Check your date of birth';
            return;
        }
        if (!this._view.get_connect_LoggedOut_NoFacebook_SignUp2_SexMaleRadio().checked && !this._view.get_connect_LoggedOut_NoFacebook_SignUp2_SexFemaleRadio().checked) {
            this._view.get_connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel().innerHTML = 'Enter your sex';
            return;
        }
        var thisAsyncOperation = this._registerStartAsync('Checking nickname...');
        this._server.getUniqueNickName(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox().value, this._noFacebookSignUp2PanelLoginUsrK, ss.Delegate.create(this, function(response) {
            if (this._registerEndAsync(thisAsyncOperation)) {
                return;
            }
            if (Js.Library.U.isTrue(response, 'Exception')) {
                this._showError(7, 'Internal server error');
            }
            else {
                var newNickname = Js.Library.U.get(response, 'Nickname').toString();
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
    
    _noFacebookSignup2SaveDone: function Js_Controls_Login_Controller$_noFacebookSignup2SaveDone() {
        this._showDetailsPanel(this._noFacebookSignUp2PanelSource, null);
    },
    
    _getNoFacebookSignupPanelData: function Js_Controls_Login_Controller$_getNoFacebookSignupPanelData() {
        /// <returns type="Object"></returns>
        var ret = {};
        ret['Email'] = this._view.get_connect_LoggedOut_NoFacebook_SignUp1_EmailTextbox().value;
        ret['Password'] = this._view.get_connect_LoggedOut_NoFacebook_SignUp1_Password1Textbox().value;
        ret['FirstName'] = this._view.get_connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextbox().value;
        ret['LastName'] = this._view.get_connect_LoggedOut_NoFacebook_SignUp2_LastNameTextbox().value;
        ret['Nickname'] = this._view.get_connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox().value;
        ret['DateOfBirthYear'] = parseInt(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDown().value);
        ret['DateOfBirthMonth'] = parseInt(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDown().value);
        ret['DateOfBirthDay'] = parseInt(this._view.get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDown().value);
        ret['SexMale'] = this._view.get_connect_LoggedOut_NoFacebook_SignUp2_SexMaleRadio().checked;
        return ret;
    },
    
    _noFacebookNewAccount: function Js_Controls_Login_Controller$_noFacebookNewAccount() {
        var thisAsyncOperation = this._registerStartAsync('Creating account...');
        this._server.noFacebookNewAccount(this._getNoFacebookSignupPanelData(), this._getDetailsPanelData(), this._getCaptchaData(), ss.Delegate.create(this, function(response) {
            if (this._registerEndAsync(thisAsyncOperation)) {
                return;
            }
            if (Js.Library.U.isTrue(response, 'Exception')) {
                this._showError(8, 'Internal server error (' + Js.Library.U.get(response, 'Message').toString() + ').');
            }
            else {
                if (Js.Library.U.isTrue(response, 'NeedsCaptcha')) {
                    this._showCaptchaPanel('NoFacebookNewAccount', response);
                }
                else {
                    this._setAuthCookie(Js.Library.U.get(response, 'AuthCookie'), Js.Library.U.get(response, 'AuthUsr'));
                    this._showLikeButtonPanel();
                }
            }
        }));
    },
    
    _showNoFacebookSignUp2Panel: function Js_Controls_Login_Controller$_showNoFacebookSignUp2Panel(source, details) {
        /// <param name="source" type="String">
        /// </param>
        /// <param name="details" type="Object">
        /// </param>
        this._noFacebookSignUp2PanelSource = source;
        this._ensurePanelGenerated('View.Connect_LoggedOut_NoFacebook_SignUp2Panel');
        this._noFacebookSignUp2PanelLoginUsrK = 0;
        if (details != null) {
            this._noFacebookSignUp2PanelLoginUsrK = Js.Library.U.get(details, 'UsrK');
            var nickname = Js.Library.U.get(details, 'Nickname').toString();
            if (nickname.length > 0) {
                this._noFacebookSignup2NicknameHasEntry = true;
                this._view.get_connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox().value = nickname;
            }
        }
        this._changePanel('View.Connect_LoggedOut_NoFacebook_SignUp2Panel');
    },
    
    _add_View_Connect_ConnectingPanel: function Js_Controls_Login_Controller$_add_View_Connect_ConnectingPanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = "\r\n<div id=\"{ClientID}Connect_ConnectingPanel\" class=\"LoginPanel\" style=\"display:none;\">\r\n\t<div class=\"LoginPanelInner\">\r\n\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\tConnecting...\r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\tYou should see a Facebook pop-up. Please log in using your Facebook account. \r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\tIf you don't see the pop-up, click the button below:\r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\t<button id=\"{ClientID}Connect_Connecting_PopupRetry\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:left;\">Re-open popup</button>\r\n\t\t</p>\r\n\t</div>\r\n\t<p>\r\n\t\t<button id=\"{ClientID}Connect_Connecting_BackButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:left;\">Back</button>\r\n\t\t<button id=\"{ClientID}Connect_Connecting_CancelButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Cancel</button>\r\n\t</p>\r\n</div>\r\n";
        this._addChild(s);
        $(this._view.get_connect_Connecting_PopupRetry()).click(ss.Delegate.create(this, this._connectingRetryPopupClick));
        $(this._view.get_connect_Connecting_CancelButton()).click(ss.Delegate.create(this, this._connectingCancelButtonClick));
        $(this._view.get_connect_Connecting_BackButton()).click(ss.Delegate.create(this, this._connectingBackButtonClick));
        return this._view.get_connect_ConnectingPanel();
    },
    
    _connectingCancelButtonClick: function Js_Controls_Login_Controller$_connectingCancelButtonClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        this._cancelledAsyncOperations[this._connectButtonClickAsyncOperation.toString()] = true;
        this._asyncInProgress = false;
        $(this._view.get_connectDialog()).dialog('close');
    },
    
    _connectingRetryPopupClick: function Js_Controls_Login_Controller$_connectingRetryPopupClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        this._cancelledAsyncOperations[this._connectButtonClickAsyncOperation.toString()] = true;
        this._asyncInProgress = false;
        this._connectButtonClickInternal();
    },
    
    _connectingBackButtonClick: function Js_Controls_Login_Controller$_connectingBackButtonClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        this._cancelledAsyncOperations[this._connectButtonClickAsyncOperation.toString()] = true;
        this._asyncInProgress = false;
        this._changePanel('View.Connect_LoggedOutPanel');
    },
    
    _add_View_Connect_NewAccount_NoEmailMatchPanel: function Js_Controls_Login_Controller$_add_View_Connect_NewAccount_NoEmailMatchPanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = "\r\n<div id=\"{ClientID}Connect_NewAccount_NoEmailMatchPanel\" class=\"LoginPanel\" style=\"display:none;\">\r\n\t<div style=\"position:relative;\" class=\"LoginPanelInner ClearAfter\">\r\n\t\t<p>\r\n\t\t\tWe've not seen this Facebook account before...\r\n\t\t</p>\r\n\t\t<div style=\"width:220px; float:left;\">\r\n\t\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\t\tNew user\r\n\t\t\t</p>\r\n\t\t\t<p>\r\n\t\t\t\tIf you've never used Don't Stay In before:\r\n\t\t\t</p>\r\n\t\t\t<p>\r\n\t\t\t\t<button id=\"{ClientID}Connect_NewAccount_NoEmailMatch_NewAccountButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\">I'm new to Don't Stay In</button>\r\n\t\t\t</p>\r\n\t\t</div>\r\n\t\t<div style=\"left:220px; width:10px; height:150px; float:left; border-left:dotted 2px #cccccc;\"> </div>\r\n\t\t<div style=\"left:230px; width:220px; float:left;\">\r\n\t\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\t\tExisting user\r\n\t\t\t</p>\r\n\t\t\t<p>\r\n\t\t\t\tIf you've logged in to Don't Stay In before:\r\n\t\t\t</p>\r\n\t\t\t<p>\r\n\t\t\t\t<button id=\"{ClientID}Connect_NewAccount_NoEmailMatch_ChooseAccountButton\"  class=\"ui-state-default ui-corner-all Pointer BigButton\">Link to my Don't Stay In...</button>\r\n\t\t\t</p>\r\n\t\t</div>\r\n\t</div>\r\n\t<p>\r\n\t\t<button id=\"{ClientID}Connect_NewAccount_NoEmailMatch_BackButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:left;\">Back</button>\r\n\r\n\t\t<button id=\"{ClientID}Connect_NewAccount_NoEmailMatch_CancelButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Cancel</button>\r\n\t\t<button id=\"{ClientID}Connect_NewAccount_NoEmailMatch_FacebookLogoutButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Log out of Facebook</button>\r\n\t</p>\r\n</div>\r\n";
        this._addChild(s);
        $(this._view.get_connect_NewAccount_NoEmailMatch_CancelButton()).click(ss.Delegate.create(this, this._cancelButtonClick));
        $(this._view.get_connect_NewAccount_NoEmailMatch_FacebookLogoutButton()).click(ss.Delegate.create(this, this._logoutOfFacebookButtonClick));
        $(this._view.get_connect_NewAccount_NoEmailMatch_BackButton()).click(ss.Delegate.create(this, function(e) {
            this._changePanelOnClick('View.Connect_NewAccount_ConfirmFacebookPanel', e);
        }));
        $(this._view.get_connect_NewAccount_NoEmailMatch_ChooseAccountButton()).click(ss.Delegate.create(this, function(e) {
            this._changePanelOnClick('View.Connect_NewAccount_ChooseAccountPanel', e);
        }));
        $(this._view.get_connect_NewAccount_NoEmailMatch_NewAccountButton()).click(ss.Delegate.create(this, this._newAccountButtonClick));
        return this._view.get_connect_NewAccount_NoEmailMatchPanel();
    },
    
    _newAccountButtonClick: function Js_Controls_Login_Controller$_newAccountButtonClick(e) {
        /// <param name="e" type="jQueryEvent">
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
    
    _newAccountButtonClickInternal: function Js_Controls_Login_Controller$_newAccountButtonClickInternal() {
        var thisAsyncOperation = this._registerStartAsync('Creating new account...');
        this._server.getHomePlaceFromFacebook(ss.Delegate.create(this, function(response) {
            if (this._registerEndAsync(thisAsyncOperation)) {
                return;
            }
            if (Js.Library.U.isTrue(response, 'Exception')) {
                this._showError(7, 'Internal server error');
            }
            else {
                this._showDetailsPanel('NewAccount', response);
            }
        }));
    },
    
    _newAccount: function Js_Controls_Login_Controller$_newAccount() {
        var thisAsyncOperation = this._registerStartAsync('Creating new account...');
        this._server.createNewAccount(this._getDetailsPanelData(), ss.Delegate.create(this, function(response) {
            if (this._registerEndAsync(thisAsyncOperation)) {
                return;
            }
            if (Js.Library.U.isTrue(response, 'Exception')) {
                this._showError(7, 'Internal server error');
            }
            else {
                if (Js.Library.U.isTrue(response, 'NeedsConfirmation')) {
                    this._showDetailsPanel('NewAccount', Js.Library.U.get(response, 'Details'));
                }
                else {
                    this._setAuthCookie(Js.Library.U.get(response, 'AuthCookie'), Js.Library.U.get(response, 'AuthUsr'));
                    this._detectAutoLoginProblem(true);
                }
            }
        }));
    },
    
    _add_View_Connect_NewAccount_EmailMatchPanel: function Js_Controls_Login_Controller$_add_View_Connect_NewAccount_EmailMatchPanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = "\r\n<div id=\"{ClientID}Connect_NewAccount_EmailMatchPanel\" class=\"LoginPanel\" style=\"display:none;\">\r\n\t<div style=\"position:relative;\" class=\"LoginPanelInner ClearAfter\">\r\n\t\t<p>\r\n\t\t\tThere's a Don't Stay In account with your Facebook email...\r\n\t\t</p>\r\n\t\t<div style=\"width:220px; float:left;\">\r\n\t\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\t\tLink my accounts\r\n\t\t\t</p>\r\n\t\t\t<p>\r\n\t\t\t\t<span id=\"{ClientID}Connect_NewAccount_EmailMatch_UserLink1\"></span>\r\n\t\t\t</p>\r\n\t\t\t<p>\r\n\t\t\t\t<button id=\"{ClientID}Connect_NewAccount_EmailMatch_AutoConnectButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\">Link to this account</button>\r\n\t\t\t</p>\r\n\t\t</div>\r\n\t\t<div style=\"left:220px; width:10px; height:150px; float:left; border-left:dotted 2px #cccccc;\"> </div>\r\n\t\t<div style=\"left:230px; width:220px; float:left;\">\r\n\t\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\t\tOther options\r\n\t\t\t</p>\r\n\t\t\t<p>\r\n\t\t\t\tIf this isn't the right account:\r\n\t\t\t</p>\r\n\t\t\t<p>\r\n\t\t\t\t<button id=\"{ClientID}Connect_NewAccount_EmailMatch_ChooseAccountButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\">Choose another account</button>\r\n\t\t\t</p>\r\n\t\t</div>\r\n\t</div>\r\n\t<p>\r\n\t\t<button id=\"{ClientID}Connect_NewAccount_EmailMatch_BackButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:left;\">Back</button>\r\n\r\n\t\t<button id=\"{ClientID}Connect_NewAccount_EmailMatch_CancelButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Cancel</button>\r\n\t\t<button id=\"{ClientID}Connect_NewAccount_EmailMatch_FacebookLogoutButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Log out of Facebook</button>\r\n\t</p>\r\n</div>\r\n";
        this._addChild(s);
        $(this._view.get_connect_NewAccount_EmailMatch_CancelButton()).click(ss.Delegate.create(this, this._cancelButtonClick));
        $(this._view.get_connect_NewAccount_EmailMatch_FacebookLogoutButton()).click(ss.Delegate.create(this, this._logoutOfFacebookButtonClick));
        $(this._view.get_connect_NewAccount_EmailMatch_BackButton()).click(ss.Delegate.create(this, function(e) {
            this._changePanelOnClick('View.Connect_NewAccount_ConfirmFacebookPanel', e);
        }));
        $(this._view.get_connect_NewAccount_EmailMatch_ChooseAccountButton()).click(ss.Delegate.create(this, function(e) {
            this._changePanelOnClick('View.Connect_NewAccount_ChooseAccountPanel', e);
        }));
        $(this._view.get_connect_NewAccount_EmailMatch_AutoConnectButton()).click(ss.Delegate.create(this, this._autoConnectClick));
        return this._view.get_connect_NewAccount_EmailMatchPanel();
    },
    
    _autoConnectClick: function Js_Controls_Login_Controller$_autoConnectClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._autoConnectClickInternal();
    },
    
    _autoConnectClickInternal: function Js_Controls_Login_Controller$_autoConnectClickInternal() {
        if (this._facebookEmailMatchEnhancedSecurity) {
            this._ensurePanelGenerated('View.Connect_NewAccount_ChooseAccountPanel');
            this._view.get_connect_NewAccount_ChooseAccount_UsernameTextbox().value = this._facebookEmailMatchNickName;
            this._changePanel('View.Connect_NewAccount_ChooseAccountPanel');
        }
        else {
            this._autoConnect(false);
        }
    },
    
    _autoConnect: function Js_Controls_Login_Controller$_autoConnect(sendDetailsPanelData) {
        /// <param name="sendDetailsPanelData" type="Boolean">
        /// </param>
        var thisAsyncOperation = this._registerStartAsync('Linking accounts...');
        this._server.autoLinkByEmail((sendDetailsPanelData) ? this._getDetailsPanelData() : null, ss.Delegate.create(this, function(response) {
            if (this._registerEndAsync(thisAsyncOperation)) {
                return;
            }
            if (Js.Library.U.isTrue(response, 'Exception')) {
                this._showError(2, 'Internal server error');
            }
            else {
                if (Js.Library.U.isTrue(response, 'FacebookEmailMatch')) {
                    if (Js.Library.U.isTrue(response, 'NeedsConfirmation')) {
                        this._showDetailsPanel('AutoConnect', Js.Library.U.get(response, 'Details'));
                    }
                    else {
                        this._setAuthCookie(Js.Library.U.get(response, 'AuthCookie'), Js.Library.U.get(response, 'AuthUsr'));
                        this._detectAutoLoginProblem(true);
                    }
                }
                else {
                    this._showError(3, "Can't find account by email.");
                }
            }
        }));
    },
    
    _add_View_Connect_NewAccount_ChooseAccountPanel: function Js_Controls_Login_Controller$_add_View_Connect_NewAccount_ChooseAccountPanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = "\r\n<div id=\"{ClientID}Connect_NewAccount_ChooseAccountPanel\" class=\"LoginPanel\" style=\"display:none;\">\r\n\t<div class=\"LoginPanelInner\">\r\n\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\tDon't Stay In details\r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\tEnter your Don't Stay In details below to link with your Facebook:\r\n\t\t</p>\r\n\t\t<p style=\"position:relative; height:25px; line-height:25px;\">\r\n\t\t\tNickname or email:\r\n\t\t\t<input id=\"{ClientID}Connect_NewAccount_ChooseAccount_UsernameTextbox\" type=\"text\" class=\"xui-state-default ui-corner-all\" style=\"padding-left:5px; height:20px; left:140px; top:0px; position:absolute; width:150px;\" />\r\n\t\t</p>\r\n\t\t<p style=\"position:relative; height:25px; line-height:25px;\">\r\n\t\t\tPassword:\r\n\t\t\t<input id=\"{ClientID}Connect_NewAccount_ChooseAccount_PasswordTextbox\" type=\"password\" class=\"xui-state-default ui-corner-all\" style=\"padding-left:5px; height:20px; left:140px; top:0px; position:absolute; width:150px; border:1px solid #cccccc;\" />\r\n\t\t</p>\r\n\t\t<p style=\"position:relative; height:30px; line-height:30px;\">\r\n\t\t\t<button id=\"{ClientID}Connect_NewAccount_ChooseAccount_LinkAccountButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\" style=\"left:140px; top:0px; position:absolute; float:left;\">Link my account</button>\r\n\t\t</p>\r\n\t\t<p style=\"position:relative;\">\r\n\t\t\t<span id=\"{ClientID}Connect_NewAccount_ChooseAccount_ErrorLabel\" class=\"ForegroundAttentionRed\" style=\"left:140px; top:0px; position:absolute; float:left; font-weight:bold;\"></span>\r\n\t\t</p>\r\n\t</div>\r\n\t<p>\r\n\t\t<button id=\"{ClientID}Connect_NewAccount_ChooseAccount_BackButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:left;\">Back</button>\r\n\r\n\t\t<button id=\"{ClientID}Connect_NewAccount_ChooseAccount_CancelButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Cancel</button>\r\n\t\t<button id=\"{ClientID}Connect_NewAccount_ChooseAccount_FacebookLogoutButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Log out of Facebook</button>\r\n\t\t<button id=\"{ClientID}Connect_NewAccount_ChooseAccount_ForgottonPasswordButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Forgot your password?</button>\r\n\t</p>\r\n</div>\r\n";
        this._addChild(s);
        $(this._view.get_connect_NewAccount_ChooseAccount_BackButton()).click(ss.Delegate.create(this, this._chooseAccountBackButtonClick));
        $(this._view.get_connect_NewAccount_ChooseAccount_CancelButton()).click(ss.Delegate.create(this, this._cancelButtonClick));
        $(this._view.get_connect_NewAccount_ChooseAccount_FacebookLogoutButton()).click(ss.Delegate.create(this, this._logoutOfFacebookButtonClick));
        $(this._view.get_connect_NewAccount_ChooseAccount_LinkAccountButton()).click(ss.Delegate.create(this, this._linkAccountClick));
        $(this._view.get_connect_NewAccount_ChooseAccount_ForgottonPasswordButton()).click(ss.Delegate.create(this, function(e) {
            this._changePanelOnClick('View.Connect_NewAccount_ForgotPasswordPanel', e);
        }));
        this._defaultButton(this._view.get_connect_NewAccount_ChooseAccount_UsernameTextbox(), this._view.get_connect_NewAccount_ChooseAccount_LinkAccountButton());
        this._defaultButton(this._view.get_connect_NewAccount_ChooseAccount_PasswordTextbox(), this._view.get_connect_NewAccount_ChooseAccount_LinkAccountButton());
        return this._view.get_connect_NewAccount_ChooseAccountPanel();
    },
    
    _linkAccountClick: function Js_Controls_Login_Controller$_linkAccountClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._linkAccount(false);
    },
    
    _linkAccount: function Js_Controls_Login_Controller$_linkAccount(sendDetailsPanelData) {
        /// <param name="sendDetailsPanelData" type="Boolean">
        /// </param>
        var thisAsyncOperation = this._registerStartAsync('Linking accounts...');
        this._server.linkAccounts(this._view.get_connect_NewAccount_ChooseAccount_UsernameTextbox().value, this._view.get_connect_NewAccount_ChooseAccount_PasswordTextbox().value, (sendDetailsPanelData) ? this._getDetailsPanelData() : null, ss.Delegate.create(this, function(response) {
            if (this._registerEndAsync(thisAsyncOperation)) {
                return;
            }
            if (Js.Library.U.isTrue(response, 'Exception')) {
                this._showError(4, 'Internal server error.');
            }
            else {
                this._view.get_connect_NewAccount_ChooseAccount_ErrorLabel().style.display = 'none';
                if (Js.Library.U.isTrue(response, 'Error')) {
                    this._view.get_connect_NewAccount_ChooseAccount_ErrorLabel().style.display = '';
                    this._view.get_connect_NewAccount_ChooseAccount_ErrorLabel().innerHTML = "Can't find a user with those details.";
                }
                else {
                    if (Js.Library.U.isTrue(response, 'NeedsConfirmation')) {
                        this._showDetailsPanel('LinkAccount', Js.Library.U.get(response, 'Details'));
                    }
                    else {
                        this._setAuthCookie(Js.Library.U.get(response, 'AuthCookie'), Js.Library.U.get(response, 'AuthUsr'));
                        this._detectAutoLoginProblem(true);
                    }
                }
            }
        }));
    },
    
    _chooseAccountBackButtonClick: function Js_Controls_Login_Controller$_chooseAccountBackButtonClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._changePanel((this._facebookEmailMatchToCurrentUser) ? 'View.Connect_NewAccount_EmailMatchPanel' : 'View.Connect_NewAccount_NoEmailMatchPanel');
    },
    
    _add_View_Connect_NewAccount_ForgotPasswordPanel: function Js_Controls_Login_Controller$_add_View_Connect_NewAccount_ForgotPasswordPanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = "\r\n<div id=\"{ClientID}Connect_NewAccount_ForgotPasswordPanel\" class=\"LoginPanel\" style=\"display:none;\">\r\n\t<div class=\"LoginPanelInner\">\r\n\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\tForgot your password?\r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\tEnter your Don't Stay In username or email below and we'll send you a password reset link by email:\r\n\t\t</p>\r\n\t\t<p style=\"position:relative; height:25px; line-height:25px;\">\r\n\t\t\tNickname or email:\r\n\t\t\t<input id=\"{ClientID}Connect_NewAccount_ForgotPassword_UsernameTextbox\" type=\"text\" class=\"xui-state-default ui-corner-all\" style=\"padding-left:5px; height:20px; left:140px; top:0px; position:absolute; width:150px;\" />\r\n\t\t</p>\r\n\t\t<p style=\"position:relative; height:30px; line-height:30px;\">\r\n\t\t\t<button id=\"{ClientID}Connect_NewAccount_ForgotPassword_SendLinkButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\" style=\"left:140px; top:0px; position:absolute; float:left;\">Send password reset link</button>\r\n\t\t</p>\r\n\t\t<p style=\"position:relative;\">\r\n\t\t\t<span id=\"{ClientID}Connect_NewAccount_ForgotPassword_MessageLabel\" class=\"ForegroundAttentionBlue\" style=\"left:140px; top:0px; position:absolute; float:left; font-weight:bold;\"></span>\r\n\t\t\t<span id=\"{ClientID}Connect_NewAccount_ForgotPassword_ErrorLabel\" class=\"ForegroundAttentionRed\" style=\"left:140px; top:0px; position:absolute; float:left; font-weight:bold;\"></span>\r\n\t\t</p>\r\n\t</div>\r\n\t<p>\r\n\t\t<button id=\"{ClientID}Connect_NewAccount_ForgotPassword_BackButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:left;\">Back</button>\r\n\r\n\t\t<button id=\"{ClientID}Connect_NewAccount_ForgotPassword_CancelButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Cancel</button>\r\n\t\t<button id=\"{ClientID}Connect_NewAccount_ForgotPassword_FacebookLogoutButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Log out of Facebook</button>\r\n\t</p>\r\n</div>\r\n";
        this._addChild(s);
        $(this._view.get_connect_NewAccount_ForgotPassword_BackButton()).click(ss.Delegate.create(this, function(e) {
            this._changePanelOnClick('View.Connect_NewAccount_ChooseAccountPanel', e);
        }));
        $(this._view.get_connect_NewAccount_ForgotPassword_CancelButton()).click(ss.Delegate.create(this, this._cancelButtonClick));
        $(this._view.get_connect_NewAccount_ForgotPassword_FacebookLogoutButton()).click(ss.Delegate.create(this, this._logoutOfFacebookButtonClick));
        $(this._view.get_connect_NewAccount_ForgotPassword_SendLinkButton()).click(ss.Delegate.create(this, this._forgotPasswordSendLinkClick));
        this._defaultButton(this._view.get_connect_NewAccount_ForgotPassword_UsernameTextbox(), this._view.get_connect_NewAccount_ForgotPassword_SendLinkButton());
        return this._view.get_connect_NewAccount_ForgotPasswordPanel();
    },
    
    _forgotPasswordSendLinkClick: function Js_Controls_Login_Controller$_forgotPasswordSendLinkClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._view.get_connect_NewAccount_ForgotPassword_ErrorLabel().style.display = 'none';
        this._view.get_connect_NewAccount_ForgotPassword_MessageLabel().style.display = 'none';
        if (!this._view.get_connect_NewAccount_ForgotPassword_UsernameTextbox().value.trim().length) {
            this._view.get_connect_NewAccount_ForgotPassword_ErrorLabel().style.display = '';
            this._view.get_connect_NewAccount_ForgotPassword_ErrorLabel().innerHTML = 'Please enter your email or nickname.';
            return;
        }
        var thisAsyncOperation = this._registerStartAsync('Sending password reset link...');
        this._server.sendPassword(this._view.get_connect_NewAccount_ForgotPassword_UsernameTextbox().value, ss.Delegate.create(this, function(response) {
            if (this._registerEndAsync(thisAsyncOperation)) {
                return;
            }
            if (Js.Library.U.isTrue(response, 'Exception')) {
                this._showError(12, 'Internal server error.');
            }
            else {
                if (Js.Library.U.isTrue(response, 'Done')) {
                    this._view.get_connect_NewAccount_ForgotPassword_MessageLabel().style.display = '';
                    this._view.get_connect_NewAccount_ForgotPassword_MessageLabel().innerHTML = 'We have sent you a password reset email.';
                }
                else {
                    this._view.get_connect_NewAccount_ForgotPassword_ErrorLabel().style.display = '';
                    this._view.get_connect_NewAccount_ForgotPassword_ErrorLabel().innerHTML = "Can't find that username or email.";
                }
            }
        }));
    },
    
    _add_View_Connect_NewAccount_ConfirmFacebookPanel: function Js_Controls_Login_Controller$_add_View_Connect_NewAccount_ConfirmFacebookPanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = "\r\n<div id=\"{ClientID}Connect_NewAccount_ConfirmFacebookPanel\" class=\"LoginPanel\" style=\"display:none;\">\r\n\t<div class=\"LoginPanelInner\">\r\n\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\tIs this you?\r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\tWe need to confirm we've got the right Facebook account...\r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\t<img src=\"\" width=\"50\" height=\"50\" border=\"0\" id=\"{ClientID}Connect_NewAccount_ConfirmFacebook_Image\" align=\"absmiddle\" />\r\n\t\t\t<a href=\"\" id=\"{ClientID}Connect_NewAccount_ConfirmFacebook_Link\" target=\"_blank\"></a>\r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\t<button id=\"{ClientID}Connect_NewAccount_ConfirmFacebook_YesButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\">Yes, this is me</button>\r\n\t\t\t<button id=\"{ClientID}Connect_NewAccount_ConfirmFacebook_NoButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\">Nope, not me</button>\r\n\t\t</p>\r\n<p>\r\n</p>\r\n\t</div>\r\n\t<p>\r\n\t\t<button id=\"{ClientID}Connect_NewAccount_ConfirmFacebook_BackButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:left;\">Back</button>\r\n\t\t<button id=\"{ClientID}Connect_NewAccount_ConfirmFacebook_CancelButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Cancel</button>\r\n\t</p>\r\n</div>\r\n";
        this._addChild(s);
        $(this._view.get_connect_NewAccount_ConfirmFacebook_BackButton()).click(ss.Delegate.create(this, function(e) {
            this._changePanelOnClick('View.Connect_LoggedOutPanel', e);
        }));
        $(this._view.get_connect_NewAccount_ConfirmFacebook_YesButton()).click(ss.Delegate.create(this, this._confirmFacebookAccountYesClick));
        $(this._view.get_connect_NewAccount_ConfirmFacebook_NoButton()).click(ss.Delegate.create(this, this._confirmFacebookAccountNoClick));
        $(this._view.get_connect_NewAccount_ConfirmFacebook_CancelButton()).click(ss.Delegate.create(this, this._cancelButtonClick));
        return this._view.get_connect_NewAccount_ConfirmFacebookPanel();
    },
    
    _showConfirmFacebookPanel: function Js_Controls_Login_Controller$_showConfirmFacebookPanel() {
        var thisAsyncOperation1 = this._registerStartAsync('Loading...');
        FB.api('/me', ss.Delegate.create(this, function(meResponse) {
            if (this._registerEndAsync(thisAsyncOperation1)) {
                return;
            }
            $(this._view.get_connectDialog()).dialog('open');
            this._ensurePanelGenerated('View.Connect_NewAccount_ConfirmFacebookPanel');
            this._view.get_connect_NewAccount_ConfirmFacebook_Link().innerHTML = Js.Library.U.get(meResponse, 'name').toString();
            this._view.get_connect_NewAccount_ConfirmFacebook_Link().href = Js.Library.U.get(meResponse, 'link').toString();
            this._view.get_connect_NewAccount_ConfirmFacebook_Image().src = 'http://graph.facebook.com/' + Js.Library.U.get(meResponse, 'id').toString() + '/picture';
            this._changePanel('View.Connect_NewAccount_ConfirmFacebookPanel');
        }));
    },
    
    _confirmFacebookAccountYesClick: function Js_Controls_Login_Controller$_confirmFacebookAccountYesClick(e) {
        /// <param name="e" type="jQueryEvent">
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
    
    _confirmFacebookAccountNoClick: function Js_Controls_Login_Controller$_confirmFacebookAccountNoClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._facebookAccountConfirmationStepDone = true;
        this._logoutNow(false, null, true);
    },
    
    _add_View_Connect_DetailsPanel: function Js_Controls_Login_Controller$_add_View_Connect_DetailsPanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = "\r\n<div id=\"{ClientID}Connect_DetailsPanel\" class=\"LoginPanel\" style=\"display:none;\">\r\n\t<div class=\"LoginPanelInner\">\r\n\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\tJust to confirm...\r\n\t\t</p>\r\n\t\t<p style=\"position:relative; height:25px; line-height:25px;\">\r\n\t\t\tFavourite music:\r\n\t\t\t<select id=\"{ClientID}Connect_Details_MusicDropDown\" class=\"xui-state-default ui-corner-all\" style=\"padding-left:5px; height:20px; left:140px; top:0px; position:absolute; width:300px;  height:25px; line-height:25px;\">\r\n\t\t\t\t<option value=\"1\">I like all music</option>\r\n\t\t\t\t<option value=\"42\">Commercial (pop, chart dance, club classics etc...)</option>\r\n\t\t\t\t<option value=\"4\">House (funky house, soulful house, deep house etc...)</option>\r\n\t\t\t\t<option value=\"10\">Hard Dance (hard house, trance, hardcore etc...)</option>\r\n\t\t\t\t<option value=\"15\">Alternative Dance (breaks, electro, big beat etc...)</option>\r\n\t\t\t\t<option value=\"20\">Techno (electro techno, detroit techno etc...)</option>\r\n\t\t\t\t<option value=\"24\">Drum and Bass (drum'n'bass, jungle etc.)</option>\r\n\t\t\t\t<option value=\"28\">Urban (hip-hop, R&amp;B, garage etc...)</option>\r\n\t\t\t\t<option value=\"65\">Alternative Electronic (industrial, ebm, powernoise etc.)</option>\r\n\t\t\t\t<option value=\"46\">Retro (disco, soul, jazz, funk etc...)</option>\r\n\t\t\t\t<option value=\"35\">Chillout / Leftfield</option>\r\n\t\t\t\t<option value=\"36\">Rock (indie, rock, metal etc...)</option>\r\n\t\t\t</select>\r\n\t\t</p>\r\n\t\t<p style=\"position:relative; height:25px; line-height:25px;\">\r\n\t\t\tNearest town: \r\n\t\t\t<select id=\"{ClientID}Connect_Details_CountryDropDown\" class=\"ui-corner-all\" style=\"padding-left:5px; height:20px; left:140px; top:0px; position:absolute; width:100px; height:25px; line-height:25px; display:none;\">\r\n\t\t\t\t<option value=\"0\">Countries</option>\r\n\t\t\t</select>\r\n\t\t\t<select id=\"{ClientID}Connect_Details_PlaceDropDown\" class=\"ui-corner-all\" style=\"padding-left:5px; height:20px; left:250px; top:0px; position:absolute; width:190px; height:25px; line-height:25px; display:none;\">\r\n\t\t\t\t<option value=\"0\">Towns</option>\r\n\t\t\t</select>\r\n\t\t\t<span id=\"{ClientID}Connect_Details_PlaceDefaultOuterSpan\" style=\"height:25px; line-height:25px; left:140px; top:0px; position:absolute; width:300px;\">\r\n\t\t\t\t<span id=\"{ClientID}Connect_Details_PlaceDefaultSpan\"></span>\r\n\t\t\t\t<a id=\"{ClientID}Connect_Details_PlaceChangeLink\" href=\"\">change</a>\r\n\t\t\t</span>\r\n\t\t</p>\r\n\t\t<div style=\"position:relative;\">\r\n\t\t\t<p id=\"{ClientID}Connect_Details_FacebookInfoPanel\" class=\"ui-state-highlight ui-corner-all\" style=\"position:absolute; top:-10px; padding:5px; left:300px; width:150px; display:none;\">\r\n\t\t\t\tWe'll update your Facebook wall when you create stuff.\r\n\t\t\t</p>\r\n\t\t\t<p id=\"{ClientID}Connect_Details_WeeklyEmailInfoPanel\" class=\"ui-state-highlight ui-corner-all\" style=\"position:absolute; top:-10px; padding:5px; left:300px; width:150px; display:none;\">\r\n\t\t\t\tWe'll send you a weekly summary of events in your area playing your favourite music.\r\n\t\t\t</p>\r\n\t\t\t<p id=\"{ClientID}Connect_Details_PartyInvitesInfoPanel\" class=\"ui-state-highlight ui-corner-all\" style=\"position:absolute; top:6px; padding:5px; left:300px; width:150px; display:none;\">\r\n\t\t\t\tWe'll send you guestlist offers, e-flyers and party invites.\r\n\t\t\t</p>\r\n\t\t</div>\r\n\t\t<p style=\"margin-left:140px; margin-top:0px; margin-bottom:2px; \">\r\n\t\t\t<input id=\"{ClientID}Connect_Details_FacebookCheck\" type=\"checkbox\" checked=\"checked\" />\r\n\t\t\t<label for=\"{ClientID}Connect_Details_FacebookCheck\"> Facebook updates</label>\r\n\t\t\t<a id=\"{ClientID}Connect_Details_FacebookInfoAnchor\" href=\"\">info</a>\r\n\t\t</p>\r\n\t\t<p style=\"margin-left:140px; margin-top:0px; margin-bottom:2px;\">\r\n\t\t\t<input id=\"{ClientID}Connect_Details_WeeklyEmailCheck\" type=\"checkbox\" checked=\"checked\" />\r\n\t\t\t<label for=\"{ClientID}Connect_Details_WeeklyEmailCheck\"> Weekly email</label>\r\n\t\t\t<a id=\"{ClientID}Connect_Details_WeeklyEmailInfoAnchor\" href=\"\">info</a>\r\n\t\t</p>\r\n\t\t<p style=\"margin-left:140px; margin-top:0px;\">\r\n\t\t\t<input id=\"{ClientID}Connect_Details_PartyInvitesCheck\" type=\"checkbox\" checked=\"checked\" />\r\n\t\t\t<label for=\"{ClientID}Connect_Details_PartyInvitesCheck\"> Party invites</label>\r\n\t\t\t<a id=\"{ClientID}Connect_Details_PartyInvitesInfoAnchor\" href=\"\">info</a>\r\n\t\t</p>\r\n\t\t<p style=\"margin-left:140px; position:relative;\">\r\n\t\t\t<button id=\"{ClientID}Connect_Details_SaveButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\" style=\"float:left;\">Save</button>\r\n\t\t\t<p id=\"{ClientID}Connect_Details_PlaceErrorSpan\" class=\"ForegroundAttentionRed\" style=\"font-weight:bold; display:none; padding-top:7px;\">&nbsp;Please select a town.</p>\r\n\t\t</p>\r\n\t</div>\r\n\t<p style=\"position:relative;\">\r\n\t\t<button id=\"{ClientID}Connect_Details_BackButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:left; position:absolute; left:0px;\">Back</button>\r\n\r\n\t\t<button id=\"{ClientID}Connect_Details_CancelButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Cancel</button>\r\n\t</p>\r\n</div>\r\n";
        this._addChild(s);
        $(this._view.get_connect_Details_CountryDropDown()).change(ss.Delegate.create(this, this._detailsCountryDropDownChange));
        $(this._view.get_connect_Details_PlaceDropDown()).change(ss.Delegate.create(this, this._detailsPlaceDropDownChange));
        $(this._view.get_connect_Details_PlaceChangeLink()).click(ss.Delegate.create(this, this._detailsPlaceChangeLinkClick));
        $(this._view.get_connect_Details_SaveButton()).click(ss.Delegate.create(this, this._detailsPanelSaveClick));
        $(this._view.get_connect_Details_CancelButton()).click(ss.Delegate.create(this, this._cancelButtonClick));
        $(this._view.get_connect_Details_BackButton()).click(ss.Delegate.create(this, this._detailsPanelBackClick));
        $(this._view.get_connect_Details_FacebookInfoAnchor()).click(ss.Delegate.create(this, this._detailsFacebookInfoAnchorClick));
        $(this._view.get_connect_Details_WeeklyEmailInfoAnchor()).click(ss.Delegate.create(this, this._detailsWeeklyEmailInfoAnchorClick));
        $(this._view.get_connect_Details_PartyInvitesInfoAnchor()).click(ss.Delegate.create(this, this._detailsPartyInvitesInfoAnchorClick));
        this._detailsCountryDropDownJ = $(this._view.get_connect_Details_CountryDropDown());
        this._detailsPlaceDropDownJ = $(this._view.get_connect_Details_PlaceDropDown());
        return this._view.get_connect_DetailsPanel();
    },
    
    _showDetailsPanel: function Js_Controls_Login_Controller$_showDetailsPanel(source, details) {
        /// <param name="source" type="String">
        /// </param>
        /// <param name="details" type="Object">
        /// </param>
        this._detailsShowSource = source;
        this._ensurePanelGenerated('View.Connect_DetailsPanel');
        var homePlace = null;
        if (details != null) {
            homePlace = Js.Library.U.get(details, 'HomePlace');
            this._detailsDefaultPlaceK = Js.Library.U.get(homePlace, 'PlaceK');
            this._detailsDefaultCountryK = Js.Library.U.get(homePlace, 'CountryK');
            this._detailsDefaultPlaceGoodMatch = Js.Library.U.get(homePlace, 'GoodMatch');
            if (Js.Library.U.exists(details, 'FavouriteMusicK')) {
                this._view.get_connect_Details_MusicDropDown().value = Js.Library.U.get(details, 'FavouriteMusicK');
            }
            if (Js.Library.U.exists(details, 'SendSpottedEmails')) {
                this._view.get_connect_Details_WeeklyEmailCheck().checked = Js.Library.U.isTrue(details, 'SendSpottedEmails');
            }
            if (Js.Library.U.exists(details, 'SendEflyers')) {
                this._view.get_connect_Details_PartyInvitesCheck().checked = Js.Library.U.isTrue(details, 'SendEflyers');
            }
        }
        else {
            var countryKFromIp = 224;
            try {
                countryKFromIp = parseInt(this._getStringFromBasePage('CountryKFromIp'));
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
            this._view.get_connect_Details_PlaceDefaultSpan().innerHTML = Js.Library.U.get(homePlace, 'PlaceName') + ', ' + Js.Library.U.get(homePlace, 'CountryName');
            this._changePanel('View.Connect_DetailsPanel');
        }
        else {
            this._detailsDropDownsUpdate(true);
        }
    },
    
    _detailsCountryDropDownChange: function Js_Controls_Login_Controller$_detailsCountryDropDownChange(e) {
        /// <param name="e" type="jQueryEvent">
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
    
    _detailsPlaceDropDownChange: function Js_Controls_Login_Controller$_detailsPlaceDropDownChange(e) {
        /// <param name="e" type="jQueryEvent">
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
    
    _detailsPlaceChangeLinkClick: function Js_Controls_Login_Controller$_detailsPlaceChangeLinkClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._detailsDropDownsUpdate(true);
    },
    
    _detailsDropDownsUpdate: function Js_Controls_Login_Controller$_detailsDropDownsUpdate(resetSelection) {
        /// <param name="resetSelection" type="Boolean">
        /// </param>
        var firingAsync = false;
        if (!this._detailsCountryDropDownPopulated) {
            firingAsync = true;
            this._detailsCountryDropDownJ.ajaxAddOption('/support/getcached.aspx?type=country', null, false, ss.Delegate.create(this, function(args) {
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
            this._detailsPlaceDropDownJ.ajaxAddOption('/support/getcached.aspx?type=place&countryk=' + countryK + '&return=k', null, false, ss.Delegate.create(this, function(args) {
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
    
    _showPlaceDropDownsAndDetailsPanel: function Js_Controls_Login_Controller$_showPlaceDropDownsAndDetailsPanel() {
        this._detailsPlaceDropDownVisible = true;
        this._view.get_connect_Details_PlaceDefaultOuterSpan().style.display = 'none';
        this._view.get_connect_Details_CountryDropDown().style.display = '';
        this._view.get_connect_Details_PlaceDropDown().style.display = '';
        this._changePanel('View.Connect_DetailsPanel');
    },
    
    _getK: function Js_Controls_Login_Controller$_getK(sel) {
        /// <param name="sel" type="Object" domElement="true">
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        try {
            var value = (sel.options[sel.selectedIndex]).value;
            value = value.substr(5, value.length - 5);
            return parseInt(value);
        }
        catch ($e1) {
            return 0;
        }
    },
    
    _setK: function Js_Controls_Login_Controller$_setK(sel, value) {
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
    
    _getIndex: function Js_Controls_Login_Controller$_getIndex(sel) {
        /// <param name="sel" type="Object" domElement="true">
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        return sel.selectedIndex;
    },
    
    _setIndex: function Js_Controls_Login_Controller$_setIndex(sel, index) {
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
    
    _detailsPanelBackClick: function Js_Controls_Login_Controller$_detailsPanelBackClick(e) {
        /// <param name="e" type="jQueryEvent">
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
    
    _detailsPanelSaveClick: function Js_Controls_Login_Controller$_detailsPanelSaveClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._view.get_connect_Details_PlaceErrorSpan().style.display = 'none';
        this._view.get_connect_Details_FacebookInfoPanel().style.display = 'none';
        this._view.get_connect_Details_WeeklyEmailInfoPanel().style.display = 'none';
        this._view.get_connect_Details_PartyInvitesInfoPanel().style.display = 'none';
        if (this._detailsPlaceDropDownVisible && !this._detailsPlaceSelectedK) {
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
    
    _getDetailsPanelData: function Js_Controls_Login_Controller$_getDetailsPanelData() {
        /// <returns type="Object"></returns>
        var detailsPanelData = {};
        detailsPanelData['MusicTypeK'] = parseInt((this._view.get_connect_Details_MusicDropDown().options[this._view.get_connect_Details_MusicDropDown().selectedIndex]).value);
        detailsPanelData['PlaceK'] = (this._detailsPlaceDropDownVisible) ? this._detailsPlaceSelectedK : this._detailsDefaultPlaceK;
        detailsPanelData['Facebook'] = this._view.get_connect_Details_FacebookCheck().checked;
        detailsPanelData['WeeklyEmail'] = this._view.get_connect_Details_WeeklyEmailCheck().checked;
        detailsPanelData['PartyInvites'] = this._view.get_connect_Details_PartyInvitesCheck().checked;
        return detailsPanelData;
    },
    
    _detailsFacebookInfoAnchorClick: function Js_Controls_Login_Controller$_detailsFacebookInfoAnchorClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._view.get_connect_Details_FacebookInfoPanel().style.display = '';
        this._view.get_connect_Details_WeeklyEmailInfoPanel().style.display = 'none';
        this._view.get_connect_Details_PartyInvitesInfoPanel().style.display = 'none';
    },
    
    _detailsWeeklyEmailInfoAnchorClick: function Js_Controls_Login_Controller$_detailsWeeklyEmailInfoAnchorClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._view.get_connect_Details_FacebookInfoPanel().style.display = 'none';
        this._view.get_connect_Details_WeeklyEmailInfoPanel().style.display = '';
        this._view.get_connect_Details_PartyInvitesInfoPanel().style.display = 'none';
    },
    
    _detailsPartyInvitesInfoAnchorClick: function Js_Controls_Login_Controller$_detailsPartyInvitesInfoAnchorClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._view.get_connect_Details_FacebookInfoPanel().style.display = 'none';
        this._view.get_connect_Details_WeeklyEmailInfoPanel().style.display = 'none';
        this._view.get_connect_Details_PartyInvitesInfoPanel().style.display = '';
    },
    
    autoLinkByAutoLoginUsr: function Js_Controls_Login_Controller$autoLinkByAutoLoginUsr(sendDetailsPanelData) {
        /// <param name="sendDetailsPanelData" type="Boolean">
        /// </param>
        var thisAsyncOperation1 = this._registerStartAsync('Connecting...');
        this._server.autoLinkByAutoLoginUsr(this.autoLoginUsrK, this.autoLoginUsrLoginString, (sendDetailsPanelData) ? this._getDetailsPanelData() : null, ss.Delegate.create(this, function(response) {
            if (this._registerEndAsync(thisAsyncOperation1)) {
                return;
            }
            if (Js.Library.U.isTrue(response, 'Exception')) {
                this._showError(9, 'Internal server error');
            }
            else {
                if (Js.Library.U.isTrue(response, 'FacebookAutoLoginUsrMatch')) {
                    if (Js.Library.U.isTrue(response, 'NeedsConfirmation')) {
                        this._showDetailsPanel('AutoLinkByAutoLoginUsr', Js.Library.U.get(response, 'Details'));
                    }
                    else {
                        this._setAuthCookie(Js.Library.U.get(response, 'AuthCookie'), Js.Library.U.get(response, 'AuthUsr'));
                        this._detectAutoLoginProblem(true);
                    }
                }
                else {
                    this._showError(10, 'Internal server error');
                }
            }
        }));
    },
    
    _add_View_Connect_CaptchaPanel: function Js_Controls_Login_Controller$_add_View_Connect_CaptchaPanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = "\r\n<div id=\"{ClientID}Connect_CaptchaPanel\" class=\"LoginPanel\" style=\"display:none;\">\r\n\t<div class=\"LoginPanelInner\">\r\n\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\tOne more thing...\r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\tTo help fight spam, we need you to comfirm you're a nice, fleshy human. You should see five upper-case letters in the box below. Enter them to continue:\r\n\t\t</p>\r\n\t\t<div style=\"position:relative;\">\r\n\t\t\t<p style=\"position:absolute;\">\r\n\t\t\t\t<img id=\"{ClientID}Connect_Captcha_Img\" src=\"\" width=\"150\" height=\"50\" style=\"text-align:top;\" />\r\n\t\t\t</p>\r\n\t\t\t<div style=\"margin-left:160px; position:absolute;\">\r\n\t\t\t\t<p>\r\n\t\t\t\t\t<input id=\"{ClientID}Connect_Captcha_Textbox\" type=\"text\" class=\"xui-state-default ui-corner-all\" style=\"padding-left:5px; height:20px; width:50px;\" />\r\n\t\t\t\t</p>\r\n\t\t\t\t<p>\r\n\t\t\t\t\t<button id=\"{ClientID}Connect_Captcha_SaveButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\">Save</button><span id=\"{ClientID}Connect_Captcha_Error\" class=\"ForegroundAttentionRed\" style=\"font-weight:bold; display:none;\">&nbsp;Try again</span>\r\n\t\t\t\t</p>\r\n\t\t\t\t\r\n\t\t\t</div>\r\n\t\t</div>\r\n\t</div>\r\n\t<p style=\"position:relative;\">\r\n\t\t<button id=\"{ClientID}Connect_Captcha_BackButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:left; position:absolute; left:0px;\">Back</button>\r\n\r\n\t\t<button id=\"{ClientID}Connect_Captcha_CancelButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Cancel</button>\r\n\t</p>\r\n</div>\r\n";
        this._addChild(s);
        $(this._view.get_connect_Captcha_SaveButton()).click(ss.Delegate.create(this, this._captchaPanelSaveClick));
        $(this._view.get_connect_Captcha_CancelButton()).click(ss.Delegate.create(this, this._cancelButtonClick));
        $(this._view.get_connect_Captcha_BackButton()).click(ss.Delegate.create(this, this._captchaPanelBackClick));
        this._defaultButton(this._view.get_connect_Captcha_Textbox(), this._view.get_connect_Captcha_SaveButton());
        return this._view.get_connect_CaptchaPanel();
    },
    
    _captchaPassthrough: '',
    
    _showCaptchaPanel: function Js_Controls_Login_Controller$_showCaptchaPanel(captchaPanelSource, details) {
        /// <param name="captchaPanelSource" type="String">
        /// </param>
        /// <param name="details" type="Object">
        /// </param>
        this._captchaPanelSource = captchaPanelSource;
        this._ensurePanelGenerated('View.Connect_CaptchaPanel');
        this._view.get_connect_Captcha_Error().style.display = (Js.Library.U.exists(details, 'CaptchaFailed')) ? '' : 'none';
        this._captchaPassthrough = Js.Library.U.get(details, 'CaptchaEncrypted').toString();
        this._view.get_connect_Captcha_Img().src = '/support/hipimagenew.aspx?a=' + this._captchaPassthrough;
        this._changePanel('View.Connect_CaptchaPanel');
    },
    
    _captchaPanelSaveClick: function Js_Controls_Login_Controller$_captchaPanelSaveClick(e) {
        /// <param name="e" type="jQueryEvent">
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
    
    _captchaPanelBackClick: function Js_Controls_Login_Controller$_captchaPanelBackClick(e) {
        /// <param name="e" type="jQueryEvent">
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
    
    _getCaptchaData: function Js_Controls_Login_Controller$_getCaptchaData() {
        /// <returns type="Object"></returns>
        this._ensurePanelGenerated('View.Connect_CaptchaPanel');
        var ret = {};
        ret['Entered'] = this._view.get_connect_Captcha_Textbox().value;
        ret['Passthrough'] = this._captchaPassthrough;
        return ret;
    },
    
    _add_View_Connect_LoggedInPanel: function Js_Controls_Login_Controller$_add_View_Connect_LoggedInPanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = "\r\n<div id=\"{ClientID}Connect_LoggedInPanel\" class=\"LoginPanel\" style=\"display:none;\">\r\n\t<div class=\"LoginPanelInner\">\r\n\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\tLogged in\r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\tYou're logged in<span id=\"{ClientID}Connect_LoggedIn_LoggedInUsrLink\"></span>.\r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\t<button id=\"{ClientID}Connect_LoggedIn_CloseButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\">Close</button>\r\n\t\t\t<button id=\"{ClientID}Connect_LoggedIn_LogoutButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\">Log out</button>\r\n\t\t</p>\r\n\t\t<p id=\"{ClientID}Connect_LoggedIn_DisconnectLinkOuter\" style=\"display:none;\">\r\n\t\t\tTo permanently disconnect your Facebook account, <a id=\"{ClientID}Connect_LoggedIn_DisconnectButtonShowLink\" href=\"\">click here</a>.\r\n\t\t</p>\r\n\t\t<p id=\"{ClientID}Connect_LoggedIn_DisconnectButtonOuter\" style=\"display:none;\">\r\n\t\t\t<button id=\"{ClientID}Connect_LoggedIn_DisconnectButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:left;\">Disconnect</button>\r\n\t\t</p>\r\n\t</div>\r\n\t<p>\r\n\t\t<button id=\"{ClientID}Connect_LoggedIn_CancelButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Cancel</button>\r\n\t</p>\r\n</div>\r\n";
        this._addChild(s);
        $(this._view.get_connect_LoggedIn_DisconnectButton()).click(ss.Delegate.create(this, this._disconnectButtonClick));
        $(this._view.get_connect_LoggedIn_LogoutButton()).click(ss.Delegate.create(this, this._logoutButtonClick));
        $(this._view.get_connect_LoggedIn_CloseButton()).click(ss.Delegate.create(this, this._cancelButtonClick));
        $(this._view.get_connect_LoggedIn_CancelButton()).click(ss.Delegate.create(this, this._cancelButtonClick));
        $(this._view.get_connect_LoggedIn_DisconnectButtonShowLink()).click(ss.Delegate.create(this, this._disconnectButtonShowClick));
        return this._view.get_connect_LoggedInPanel();
    },
    
    _showLoggedInPanel: function Js_Controls_Login_Controller$_showLoggedInPanel(link) {
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
    
    _disconnectButtonShowClick: function Js_Controls_Login_Controller$_disconnectButtonShowClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._view.get_connect_LoggedIn_DisconnectButtonOuter().style.display = '';
    },
    
    _disconnectButtonClick: function Js_Controls_Login_Controller$_disconnectButtonClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        if (!this._currentFacebookConnected) {
            this._removeAuthCookie();
            this._showError(5, "You tried to disconnect, but you're not connected.");
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
    
    _add_View_Connect_CreatePasswordPanel: function Js_Controls_Login_Controller$_add_View_Connect_CreatePasswordPanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = "\r\n<div id=\"{ClientID}Connect_CreatePasswordPanel\" class=\"LoginPanel\" style=\"display:none;\">\r\n\t<div class=\"LoginPanelInner\">\r\n\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\tCreate a password\r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\tYou'll need this password if you ever want to reconnect your Facebook to this account:\r\n\t\t</p>\r\n\t\t<p style=\"position:relative; height:25px; line-height:25px;\">\r\n\t\t\tPassword:\r\n\t\t\t<input id=\"{ClientID}Connect_CreatePassword_Password1Textbox\" type=\"password\" class=\"xui-state-default ui-corner-all\" style=\"padding-left:5px; height:20px; left:140px; top:0px; position:absolute; width:150px; border:1px solid #cccccc;\" />\r\n\t\t</p>\r\n\t\t<p style=\"position:relative; height:25px; line-height:25px;\">\r\n\t\t\tPassword (confirm):\r\n\t\t\t<input id=\"{ClientID}Connect_CreatePassword_Password2Textbox\" type=\"password\" class=\"xui-state-default ui-corner-all\" style=\"padding-left:5px; height:20px; left:140px; top:0px; position:absolute; width:150px; border:1px solid #cccccc;\" />\r\n\t\t</p>\r\n\t\t<p style=\"position:relative; height:30px; line-height:30px;\">\r\n\t\t\t<button id=\"{ClientID}Connect_CreatePassword_DisconnectButton\" class=\"ui-state-default ui-corner-all Pointer BigButton\" style=\"left:140px; top:0px; position:absolute;\">Disconnect</button>\r\n\t\t</p>\r\n\t\t<p style=\"position:relative;\">\r\n\t\t\t<span id=\"{ClientID}Connect_CreatePassword_ErrorSpan\" class=\"ForegroundAttentionRed\" style=\"font-weight:bold; left:140px; top:0px; position:absolute;\"></span>\r\n\t\t</p>\r\n\r\n\t</div>\r\n\t<p>\r\n\t\t<button id=\"{ClientID}Connect_CreatePassword_BackButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:left;\">Back</button>\r\n\r\n\t\t<button id=\"{ClientID}Connect_CreatePassword_CancelButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Cancel</button>\r\n\t</p>\r\n</div>";
        this._addChild(s);
        $(this._view.get_connect_CreatePassword_DisconnectButton()).click(ss.Delegate.create(this, this._createPasswordDisconnectButtonClick));
        $(this._view.get_connect_CreatePassword_CancelButton()).click(ss.Delegate.create(this, this._cancelButtonClick));
        $(this._view.get_connect_CreatePassword_BackButton()).click(ss.Delegate.create(this, this._createPasswordBackButtonClick));
        this._defaultButton(this._view.get_connect_CreatePassword_Password1Textbox(), this._view.get_connect_CreatePassword_DisconnectButton());
        this._defaultButton(this._view.get_connect_CreatePassword_Password2Textbox(), this._view.get_connect_CreatePassword_DisconnectButton());
        return this._view.get_connect_CreatePasswordPanel();
    },
    
    _createPasswordBackButtonClick: function Js_Controls_Login_Controller$_createPasswordBackButtonClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._showLoggedInPanel('');
    },
    
    _createPasswordDisconnectButtonClick: function Js_Controls_Login_Controller$_createPasswordDisconnectButtonClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        if (this._view.get_connect_CreatePassword_Password1Textbox().value.trim() !== this._view.get_connect_CreatePassword_Password2Textbox().value.trim()) {
            this._view.get_connect_CreatePassword_ErrorSpan().innerHTML = "Passwords don't match.";
            return;
        }
        if (this._view.get_connect_CreatePassword_Password1Textbox().value.trim().length < 4) {
            this._view.get_connect_CreatePassword_ErrorSpan().innerHTML = 'Please enter at least four characters.';
            return;
        }
        this._disconnectInner(this._view.get_connect_CreatePassword_Password1Textbox().value.trim());
    },
    
    _disconnectInner: function Js_Controls_Login_Controller$_disconnectInner(password) {
        /// <param name="password" type="String">
        /// </param>
        var thisAsyncOperation = this._registerStartAsync('Disconnecting...');
        this._server.unlinkAccount(password, ss.Delegate.create(this, function(response) {
            if (this._registerEndAsync(thisAsyncOperation)) {
                return;
            }
            if (Js.Library.U.isTrue(response, 'Exception')) {
                this._showError(10, 'Internal server error');
            }
            else {
                if (Js.Library.U.isTrue(response, 'DoneUnlink')) {
                    var thisAsyncOperation1 = this._registerStartAsync('Disconnecting...');
                    FB.api(F.d('method', 'Auth.revokeAuthorization'), ss.Delegate.create(this, function(revokeResponse) {
                        if (this._registerEndAsync(thisAsyncOperation1)) {
                            return;
                        }
                        this._removeAuthCookie();
                        $(this._view.get_connectDialog()).dialog('close');
                    }));
                }
                else {
                    this._showError(11, 'Internal server error');
                }
            }
        }));
    },
    
    _add_View_Connect_AutoLoginMismatchPanel: function Js_Controls_Login_Controller$_add_View_Connect_AutoLoginMismatchPanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = '\r\n<div id="{ClientID}Connect_AutoLoginMismatchPanel" class="LoginPanel" style="display:none;">\r\n\t<div class="LoginPanelInner">\r\n\t\t<p class="LoginPanelTitle">\r\n\t\t\tMight be a problem...\r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\t<span id="{ClientID}Connect_AutoLoginMismatch_AutoLoginUsrLink"></span>\r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\t<button id="{ClientID}Connect_AutoLoginMismatch_RetryButton" class="ui-state-default ui-corner-all Pointer BigButton">Retry login</button>\r\n\t\t\t<button id="{ClientID}Connect_AutoLoginMismatch_ContinueButton" class="ui-state-default ui-corner-all Pointer BigButton">Continue to the page</button>\r\n\t\t</p>\r\n\t\t<p id="{ClientID}Connect_AutoLoginMismatch_SwitchAccountsPara">\r\n\t\t\tTo switch your Facebook to <span id="{ClientID}Connect_AutoLoginMismatch_AutoLoginUsrLink2"></span>, <a id="{ClientID}Connect_AutoLoginMismatch_SwitchAccountsShowLink" href="">click here</a>.\r\n\t\t</p>\r\n\t\t<p id="{ClientID}Connect_AutoLoginMismatch_SwitchAccountsOuter" style="display:none;">\r\n\t\t\t<button id="{ClientID}Connect_AutoLoginMismatch_SwitchButton" class="ui-state-default ui-corner-all Pointer SmallButton" style="float:left;">Switch accounts</button>\r\n\t\t</p>\r\n\t</div>\r\n\t<p>\r\n\t\t<button id="{ClientID}Connect_AutoLoginMismatch_CancelButton"  class="ui-state-default ui-corner-all Pointer SmallButton" style="float:right;">Cancel</button>\t\t\r\n\t</p>\r\n</div>';
        this._addChild(s);
        $(this._view.get_connect_AutoLoginMismatch_RetryButton()).click(ss.Delegate.create(this, this._detectAutoLoginRetryClick));
        $(this._view.get_connect_AutoLoginMismatch_ContinueButton()).click(ss.Delegate.create(this, this._detectAutoLoginContinueClick));
        $(this._view.get_connect_AutoLoginMismatch_CancelButton()).click(ss.Delegate.create(this, this._detectAutoLoginRetryClick));
        $(this._view.get_connect_AutoLoginMismatch_SwitchButton()).click(ss.Delegate.create(this, this._detectAutoLoginSwitchClick));
        $(this._view.get_connect_AutoLoginMismatch_SwitchAccountsShowLink()).click(ss.Delegate.create(this, this._detectAutoLoginSwitchShowLinkClick));
        return this._view.get_connect_AutoLoginMismatchPanel();
    },
    
    _detectAutoLoginProblemNewLink: false,
    
    _detectAutoLoginProblem: function Js_Controls_Login_Controller$_detectAutoLoginProblem(newLink) {
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
    
    _detectAutoLoginContinueClick: function Js_Controls_Login_Controller$_detectAutoLoginContinueClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._detectAutoLoginProblemNext();
    },
    
    _detectAutoLoginRetryClick: function Js_Controls_Login_Controller$_detectAutoLoginRetryClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._logoutNow(false, null, true);
    },
    
    _detectAutoLoginSwitchShowLinkClick: function Js_Controls_Login_Controller$_detectAutoLoginSwitchShowLinkClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._view.get_connect_AutoLoginMismatch_SwitchAccountsOuter().style.display = '';
    },
    
    _detectAutoLoginSwitchClick: function Js_Controls_Login_Controller$_detectAutoLoginSwitchClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._switchAccounts(false);
    },
    
    _switchAccounts: function Js_Controls_Login_Controller$_switchAccounts(sendDetailsPanelData) {
        /// <param name="sendDetailsPanelData" type="Boolean">
        /// </param>
        var thisAsyncOperation = this._registerStartAsync('Switching accounts...');
        this._server.switchAccounts(this._currentFacebookUID, this.autoLoginUsrK, this.autoLoginUsrLoginString, (sendDetailsPanelData) ? this._getDetailsPanelData() : null, ss.Delegate.create(this, function(response) {
            if (this._registerEndAsync(thisAsyncOperation)) {
                return;
            }
            if (Js.Library.U.isTrue(response, 'Exception')) {
                this._showError(8, 'Internal server error.');
            }
            else {
                if (Js.Library.U.isTrue(response, 'NeedsConfirmation')) {
                    this._showDetailsPanel('SwitchAccounts', Js.Library.U.get(response, 'Details'));
                }
                else {
                    this._setAuthCookie(Js.Library.U.get(response, 'AuthCookie'), Js.Library.U.get(response, 'AuthUsr'));
                    this._showLikeButtonPanel();
                }
            }
        }));
    },
    
    _detectAutoLoginProblemNext: function Js_Controls_Login_Controller$_detectAutoLoginProblemNext() {
        if (this._detectAutoLoginProblemNewLink) {
            this._showLikeButtonPanel();
        }
        else {
            $(this._view.get_connectDialog()).dialog('close');
        }
    },
    
    _add_View_Connect_LikeButtonPanel: function Js_Controls_Login_Controller$_add_View_Connect_LikeButtonPanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = "\r\n<div id=\"{ClientID}Connect_LikeButtonPanel\" class=\"LoginPanel\" style=\"display:none;\">\r\n\t<div class=\"LoginPanelInner\">\r\n\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\tClick the Like button...\r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\t... if you'd like to be kept up to date by Facebook:\r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\t<fb:like href=\"http://www.facebook.com/dontstayin\" layout=\"box_count\" font=\"verdana\" width=\"200px\"></fb:like>\r\n\t\t</p>\r\n\t</div>\r\n\t<p>\r\n\t\t<button id=\"{ClientID}Connect_LikeButton_CancelButton\"  class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">I'd rather not</button>\r\n\t</p>\r\n</div>\r\n";
        this._addChild(s);
        $(this._view.get_connect_LikeButton_CancelButton()).click(ss.Delegate.create(this, this._cancelButtonClick));
        FB.XFBML.parse(this._view.get_connect_LikeButtonPanel());
        return this._view.get_connect_LikeButtonPanel();
    },
    
    _showLikeButtonPanel: function Js_Controls_Login_Controller$_showLikeButtonPanel() {
        var thisAsyncOperation = this._registerStartAsync('Loading...');
        FB.api(F.d('method', 'fql.query', 'query', 'SELECT type, created_time FROM page_fan WHERE page_id="95813938222" AND uid="' + this._currentFacebookUID + '"'), ss.Delegate.create(this, function(likeFqlResponse) {
            if (this._registerEndAsync(thisAsyncOperation)) {
                return;
            }
            if (Js.Library.U.exists(likeFqlResponse, '/value/type')) {
                $(this._view.get_connectDialog()).dialog('close');
            }
            else {
                this._changePanel('View.Connect_LikeButtonPanel');
                FB.Event.subscribe('edge.create', ss.Delegate.create(this, function(edgeCreateResponse) {
                    $(this._view.get_connectDialog()).dialog('close');
                }));
            }
        }));
    },
    
    _add_View_Connect_LoadingPanel: function Js_Controls_Login_Controller$_add_View_Connect_LoadingPanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = "\r\n<div id=\"{ClientID}Connect_LoadingPanel\" class=\"LoginPanel\" style=\"display:none;\">\r\n\t<div class=\"LoginPanelInner\">\r\n\t\t<p class=\"LoginPanelTitle\">\r\n\t\t\tLoading...\r\n\t\t</p>\r\n\t\t<p>\r\n\t\t\tWe're waiting for Facebook to connect...\r\n\t\t</p>\r\n\t</div>\r\n\t<p>\r\n\t\t<button id=\"{ClientID}Connect_Loading_CancelButton\" class=\"ui-state-default ui-corner-all Pointer SmallButton\" style=\"float:right;\">Cancel</button>\r\n\t</p>\r\n</div>\r\n";
        this._addChild(s);
        $(this._view.get_connect_Loading_CancelButton()).click(ss.Delegate.create(this, this._cancelButtonClick));
        return this._view.get_connect_LoadingPanel();
    },
    
    _add_View_Connect_ErrorPanel: function Js_Controls_Login_Controller$_add_View_Connect_ErrorPanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = '\r\n<div id="{ClientID}Connect_ErrorPanel" class="LoginPanel" style="display:none;">\r\n\t<div class="LoginPanelInner">\r\n\t\t<p class="LoginPanelTitle">\r\n\t\t\tOops!\r\n\t\t</p>\r\n\t\t<p id="{ClientID}Connect_Error_ErrorDescription" />\r\n\t\t<p>\r\n\t\t\t<button id="{ClientID}Connect_Error_TryAgainButton" class="ui-state-default ui-corner-all Pointer BigButton">Try again</button>\r\n\t\t</p>\r\n\t</div>\r\n\t<p>\r\n\t\t<button id="{ClientID}Connect_Error_CancelButton" class="ui-state-default ui-corner-all Pointer SmallButton" style="float:right;">Cancel</button>\r\n\t</p>\r\n</div>\r\n';
        this._addChild(s);
        $(this._view.get_connect_Error_CancelButton()).click(ss.Delegate.create(this, this._cancelButtonClick));
        $(this._view.get_connect_Error_TryAgainButton()).click(ss.Delegate.create(this, this._errorTryAgainClick));
        return this._view.get_connect_ErrorPanel();
    },
    
    _showError: function Js_Controls_Login_Controller$_showError(id, description) {
        /// <param name="id" type="Number" integer="true">
        /// </param>
        /// <param name="description" type="String">
        /// </param>
        this._changePanel('View.Connect_ErrorPanel');
        this._view.get_connect_Error_ErrorDescription().innerHTML = description;
    },
    
    _errorTryAgainClick: function Js_Controls_Login_Controller$_errorTryAgainClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._initialiseForm();
    },
    
    _add_View_Connect_LoadingLabel: function Js_Controls_Login_Controller$_add_View_Connect_LoadingLabel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = '\r\n<div id="{ClientID}Connect_LoadingLabel" class="ui-state-highlight ui-corner-all BigButton" style="position:absolute; right:2px; top:36px; display:none; z-index:995;">\r\n\t<p>\r\n\t\tLoading...\r\n\t</p>\r\n</div>\r\n';
        this._addChild(s);
        return this._view.get_connect_LoadingLabel();
    },
    
    _asyncInProgress: false,
    _asyncOperationCounter: 0,
    
    _registerStartAsync: function Js_Controls_Login_Controller$_registerStartAsync(text) {
        /// <param name="text" type="String">
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        return this._registerStartAsyncGeneric(text, true, true);
    },
    
    _registerStartAsyncGeneric: function Js_Controls_Login_Controller$_registerStartAsyncGeneric(text, setTimer, showLoadingLabel) {
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
            window.setTimeout(ss.Delegate.create(this, function(o) {
                if (this._asyncInProgress && thisAsyncOperationCounter === this._asyncOperationCounter) {
                    this._view.get_connect_LoadingLabel().innerHTML = '<p style="margin-top:3px;padding-top:0px;">This seems to be taking a long time... <button id="Connect_LoadingLabel_CancelLink">Cancel</button></p>';
                    $(document.getElementById('Connect_LoadingLabel_CancelLink')).click(ss.Delegate.create(this, function(e) {
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
    
    _registerEndAsync: function Js_Controls_Login_Controller$_registerEndAsync(asyncOperationCounter) {
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
    
    _add_View_Connect_DebugPanel: function Js_Controls_Login_Controller$_add_View_Connect_DebugPanel() {
        /// <returns type="Object" domElement="true"></returns>
        var s = '\r\n<p id="{ClientID}Connect_DebugPanel" style="display:none;">\r\n\t<textarea id="{ClientID}Connect_Debug_Output" cols="75" rows="10"></textarea><br />\r\n\t<button id="{ClientID}Connect_Debug_LogoutButton" class="ui-state-default ui-corner-all Pointer SmallButton">Logout</button>\r\n\t<button id="{ClientID}Connect_Debug_DisconnectButton" class="ui-state-default ui-corner-all Pointer SmallButton">Disconnect</button>\r\n\t<button id="{ClientID}Connect_Debug_AuthButton" class="ui-state-default ui-corner-all Pointer SmallButton">Auth</button>\r\n</p>\r\n';
        this._addChild(s);
        $(this._view.get_connect_Debug_LogoutButton()).click(ss.Delegate.create(this, this._logoutButtonClick));
        $(this._view.get_connect_Debug_DisconnectButton()).click(ss.Delegate.create(this, this._disconnectDebug));
        return this._view.get_connect_DebugPanel();
    },
    
    _disconnectDebug: function Js_Controls_Login_Controller$_disconnectDebug(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        var thisAsyncOperation = this._registerStartAsync('Disconnecting...');
        FB.api(F.d('method', 'Auth.revokeAuthorization'), ss.Delegate.create(this, function(revokeResponse) {
            if (this._registerEndAsync(thisAsyncOperation)) {
                return;
            }
            this._removeAuthCookie();
            $(this._view.get_connectDialog()).dialog('close');
        }));
    },
    
    debug: function Js_Controls_Login_Controller$debug(txt) {
        /// <param name="txt" type="String">
        /// </param>
        $(this._view.get_connectDialog()).dialog('open');
        this._ensurePanelGenerated('View.Connect_DebugPanel');
        this._view.get_connect_DebugPanel().style.display = '';
        this._view.get_connect_Debug_Output().innerHTML = txt + '\n' + this._view.get_connect_Debug_Output().innerHTML;
    },
    
    _setAuthCookie: function Js_Controls_Login_Controller$_setAuthCookie(authCookie, authUsr) {
        /// <param name="authCookie" type="Object">
        /// </param>
        /// <param name="authUsr" type="Object">
        /// </param>
        var name = Js.Library.U.get(authCookie, 'Name').toString();
        var value = Js.Library.U.get(authCookie, 'Value').toString();
        var expires = (Js.Library.U.get(authCookie, 'Expires')).toUTCString();
        var path = Js.Library.U.get(authCookie, 'Path').toString();
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
            this._currentAuthUsrNickName = Js.Library.U.get(authUsr, 'NickName').toString();
            this._currentAuthUsrLink = Js.Library.U.get(authUsr, 'Link').toString();
            this._currentAuthUsrEmail = Js.Library.U.get(authUsr, 'Email').toString();
            this._currentAuthUsrHasNullPassword = Js.Library.U.get(authUsr, 'HasNullPassword');
        }
        catch ($e2) {
        }
        this._currentAuthCookieHasError = false;
    },
    
    _removeAuthCookie: function Js_Controls_Login_Controller$_removeAuthCookie() {
        this._currentAuthUsrK = '0';
        this._currentAuthUsrFacebookUID = '0';
        this.updateIsLoggedIn();
        document.cookie = 'SpottedAuthFix=1; expires=Fri, 27 Jul 2001 02:47:11 UTC; path=/;';
    },
    
    _updateCurrentFacebookLoginStatus: function Js_Controls_Login_Controller$_updateCurrentFacebookLoginStatus(statusResponse) {
        /// <param name="statusResponse" type="Object">
        /// </param>
        this._currentFacebookConnected = Js.Library.U.exists(statusResponse, 'status') && Js.Library.U.get(statusResponse, 'status').toString() === 'connected';
        this._currentFacebookLoggedIn = Js.Library.U.exists(statusResponse, 'status') && Js.Library.U.get(statusResponse, 'status').toString() !== 'unknown';
        this._currentFacebookUID = (this._currentFacebookConnected) ? Js.Library.U.get(statusResponse, 'authResponse/userID').toString() : '0';
        this._currentFacebookAuthResponse = (this._currentFacebookConnected) ? Js.Library.U.get(statusResponse, 'authResponse') : null;
        this.updateIsLoggedIn();
    },
    
    updateAuthDetailsFromDsiCookie: function Js_Controls_Login_Controller$updateAuthDetailsFromDsiCookie() {
        var s = Js.Controls.Login.Controller._readCookie('SpottedAuthFix');
        if (!this._currentAuthCookieHasError && !!s && s !== '1') {
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
    
    updateIsLoggedIn: function Js_Controls_Login_Controller$updateIsLoggedIn() {
        /// <returns type="Boolean"></returns>
        this.currentIsLoggedIn = this._currentAuthUsrK !== '0';
        this.currentIsLoggedInWithFacebook = this._currentFacebookUID !== '0' && this._currentFacebookUID === this._currentAuthUsrFacebookUID;
        return this.currentIsLoggedIn;
    },
    
    _cancelButtonClick: function Js_Controls_Login_Controller$_cancelButtonClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        $(this._view.get_connectDialog()).dialog('close');
    },
    
    _logoutButtonClick: function Js_Controls_Login_Controller$_logoutButtonClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._logoutNow(true, null, false);
    },
    
    _logoutOfFacebookButtonClick: function Js_Controls_Login_Controller$_logoutOfFacebookButtonClick(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._logoutNow(true, null, true);
    },
    
    logOutAndDoAction: function Js_Controls_Login_Controller$logOutAndDoAction(onLogout, forceFacebookLogout) {
        /// <param name="onLogout" type="Function">
        /// </param>
        /// <param name="forceFacebookLogout" type="Boolean">
        /// </param>
        this._logoutNow(true, onLogout, forceFacebookLogout);
    },
    
    _logoutNow: function Js_Controls_Login_Controller$_logoutNow(closeConnectDialog, onLogout, forceFacebookLogout) {
        /// <param name="closeConnectDialog" type="Boolean">
        /// </param>
        /// <param name="onLogout" type="Function">
        /// </param>
        /// <param name="forceFacebookLogout" type="Boolean">
        /// </param>
        this._facebookAccountNeedsConfirmationBecauseInitiallyFacebookLoggedIn = false;
        this._facebookAccountNeedsConfirmationBecauseInitiallyFacebookConnectedAndSiteLoggedOut = false;
        if (this.currentIsLoggedIn || (this._currentFacebookConnected && forceFacebookLogout)) {
            if (this.currentIsLoggedInWithFacebook || (this._currentFacebookConnected && forceFacebookLogout)) {
                var thisAsyncOperation = this._registerStartAsync('Logging out...');
                FB.logout(ss.Delegate.create(this, function(logoutResponse) {
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
    
    _logoutInternal: function Js_Controls_Login_Controller$_logoutInternal(closeConnectDialog, onLogout) {
        /// <param name="closeConnectDialog" type="Boolean">
        /// </param>
        /// <param name="onLogout" type="Function">
        /// </param>
        this._removeAuthCookie();
        if (closeConnectDialog) {
            $(this._view.get_connectDialog()).dialog('close');
        }
        else {
            this._initialiseForm();
        }
        if (onLogout != null) {
            onLogout();
        }
    },
    
    _changePanelOnClick: function Js_Controls_Login_Controller$_changePanelOnClick(panelString, e) {
        /// <param name="panelString" type="String">
        /// </param>
        /// <param name="e" type="jQueryEvent">
        /// </param>
        e.preventDefault();
        if (this._asyncInProgress) {
            return;
        }
        this._changePanel(panelString);
    },
    
    _changePanel: function Js_Controls_Login_Controller$_changePanel(panelString) {
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
    
    _getPanel: function Js_Controls_Login_Controller$_getPanel(panelString) {
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
    
    _ensurePanelGenerated: function Js_Controls_Login_Controller$_ensurePanelGenerated(panelString) {
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
    
    _replaceClientId: function Js_Controls_Login_Controller$_replaceClientId(s) {
        /// <param name="s" type="String">
        /// </param>
        /// <returns type="String"></returns>
        return s.replaceAll('{ClientID}', this._view.clientId + '_');
    },
    
    _addChild: function Js_Controls_Login_Controller$_addChild(s) {
        /// <param name="s" type="String">
        /// </param>
        var el = document.createElement('div');
        el.innerHTML = this._replaceClientId(s);
        this._view.get_connectDialog().appendChild(el);
    },
    
    _defaultButton: function Js_Controls_Login_Controller$_defaultButton(textBox, button) {
        /// <param name="textBox" type="Object" domElement="true">
        /// </param>
        /// <param name="button" type="Object" domElement="true">
        /// </param>
        $(textBox).keyup(ss.Delegate.create(this, function(e) {
            if (e.which === 13) {
                if (this._asyncInProgress) {
                    return;
                }
                button.click();
            }
        }));
    },
    
    _getStringFromBasePage: function Js_Controls_Login_Controller$_getStringFromBasePage(id) {
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
    
    _getStringFromPage: function Js_Controls_Login_Controller$_getStringFromPage(id) {
        /// <param name="id" type="String">
        /// </param>
        /// <returns type="String"></returns>
        return this._getStringFromBasePage('Content_' + id);
    },
    
    _getIntFromPage: function Js_Controls_Login_Controller$_getIntFromPage(id) {
        /// <param name="id" type="String">
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        var i = 0;
        try {
            i = parseInt((document.getElementById('Content_' + id)).value);
        }
        catch ($e1) {
        }
        return i;
    },
    
    _getBoolFromPage: function Js_Controls_Login_Controller$_getBoolFromPage(id) {
        /// <param name="id" type="String">
        /// </param>
        /// <returns type="Boolean"></returns>
        return this._getBoolFromBasePage('Content_' + id);
    },
    
    _getBoolFromBasePage: function Js_Controls_Login_Controller$_getBoolFromBasePage(id) {
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
    
    _addOption: function Js_Controls_Login_Controller$_addOption(value, text, parent) {
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
// Js.Controls.Login.Server

Js.Controls.Login.Server = function Js_Controls_Login_Server() {
}
Js.Controls.Login.Server.prototype = {
    
    checkEmail: function Js_Controls_Login_Server$checkEmail(email, response) {
        /// <param name="email" type="String">
        /// </param>
        /// <param name="response" type="Js.Library.Function">
        /// </param>
        var paramArr = [ email ];
        var req = eval('PageMethods.ClientRequest');
        if (req != null) {
            try {
                req('Spotted.Controls.Login, Spotted, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 'CheckEmail', paramArr, response, response);
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
    
    noFacebookNewAccount: function Js_Controls_Login_Server$noFacebookNewAccount(noFacebookSignupPanelData, detailsPanelData, captchaData, response) {
        /// <param name="noFacebookSignupPanelData" type="Object">
        /// </param>
        /// <param name="detailsPanelData" type="Object">
        /// </param>
        /// <param name="captchaData" type="Object">
        /// </param>
        /// <param name="response" type="Js.Library.Function">
        /// </param>
        var paramArr = [ noFacebookSignupPanelData, detailsPanelData, captchaData ];
        var req = eval('PageMethods.ClientRequest');
        if (req != null) {
            try {
                req('Spotted.Controls.Login, Spotted, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 'NoFacebookNewAccount', paramArr, response, response);
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
    
    getUniqueNickName: function Js_Controls_Login_Server$getUniqueNickName(nickname, usrK, response) {
        /// <param name="nickname" type="String">
        /// </param>
        /// <param name="usrK" type="Number" integer="true">
        /// </param>
        /// <param name="response" type="Js.Library.Function">
        /// </param>
        var paramArr = [ nickname, usrK ];
        var req = eval('PageMethods.ClientRequest');
        if (req != null) {
            try {
                req('Spotted.Controls.Login, Spotted, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 'GetUniqueNickName', paramArr, response, response);
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
    
    noFacebookLogin: function Js_Controls_Login_Server$noFacebookLogin(nickNameOrEmail, password, captchaData, noFacebookSignupPanelData, detailsPanelData, autoLogin, autoLoginUsrK, autoLoginString, response) {
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
        /// <param name="response" type="Js.Library.Function">
        /// </param>
        var paramArr = [ nickNameOrEmail, password, captchaData, noFacebookSignupPanelData, detailsPanelData, autoLogin, autoLoginUsrK, autoLoginString ];
        var req = eval('PageMethods.ClientRequest');
        if (req != null) {
            try {
                req('Spotted.Controls.Login, Spotted, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 'NoFacebookLogin', paramArr, response, response);
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
    
    sendPassword: function Js_Controls_Login_Server$sendPassword(emailOrNickname, response) {
        /// <param name="emailOrNickname" type="String">
        /// </param>
        /// <param name="response" type="Js.Library.Function">
        /// </param>
        var paramArr = [ emailOrNickname ];
        var req = eval('PageMethods.ClientRequest');
        if (req != null) {
            try {
                req('Spotted.Controls.Login, Spotted, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 'SendPassword', paramArr, response, response);
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
    
    switchAccounts: function Js_Controls_Login_Server$switchAccounts(currentUIDFromFacebook, autoLoginUsrK, autoLoginUsrLoginString, detailsPanelData, response) {
        /// <param name="currentUIDFromFacebook" type="String">
        /// </param>
        /// <param name="autoLoginUsrK" type="Number" integer="true">
        /// </param>
        /// <param name="autoLoginUsrLoginString" type="String">
        /// </param>
        /// <param name="detailsPanelData" type="Object">
        /// </param>
        /// <param name="response" type="Js.Library.Function">
        /// </param>
        var paramArr = [ currentUIDFromFacebook, autoLoginUsrK, autoLoginUsrLoginString, detailsPanelData ];
        var req = eval('PageMethods.ClientRequest');
        if (req != null) {
            try {
                req('Spotted.Controls.Login, Spotted, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 'SwitchAccounts', paramArr, response, response);
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
    
    createNewAccount: function Js_Controls_Login_Server$createNewAccount(detailsPanelData, response) {
        /// <param name="detailsPanelData" type="Object">
        /// </param>
        /// <param name="response" type="Js.Library.Function">
        /// </param>
        var paramArr = [ detailsPanelData ];
        var req = eval('PageMethods.ClientRequest');
        if (req != null) {
            try {
                req('Spotted.Controls.Login, Spotted, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 'CreateNewAccount', paramArr, response, response);
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
    
    getHomePlaceFromFacebook: function Js_Controls_Login_Server$getHomePlaceFromFacebook(response) {
        /// <param name="response" type="Js.Library.Function">
        /// </param>
        var paramArr = [];
        var req = eval('PageMethods.ClientRequest');
        if (req != null) {
            try {
                req('Spotted.Controls.Login, Spotted, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 'GetHomePlaceFromFacebook', paramArr, response, response);
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
    
    getUserByFacebookUID: function Js_Controls_Login_Server$getUserByFacebookUID(autoLoginUsrK, autoLoginUsrLoginString, response) {
        /// <param name="autoLoginUsrK" type="Number" integer="true">
        /// </param>
        /// <param name="autoLoginUsrLoginString" type="String">
        /// </param>
        /// <param name="response" type="Js.Library.Function">
        /// </param>
        var paramArr = [ autoLoginUsrK, autoLoginUsrLoginString ];
        var req = eval('PageMethods.ClientRequest');
        if (req != null) {
            try {
                req('Spotted.Controls.Login, Spotted, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 'GetUserByFacebookUID', paramArr, response, response);
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
    
    autoLinkByAutoLoginUsr: function Js_Controls_Login_Server$autoLinkByAutoLoginUsr(autoLoginUsrK, autoLoginUsrLoginString, detailsPanelData, response) {
        /// <param name="autoLoginUsrK" type="Number" integer="true">
        /// </param>
        /// <param name="autoLoginUsrLoginString" type="String">
        /// </param>
        /// <param name="detailsPanelData" type="Object">
        /// </param>
        /// <param name="response" type="Js.Library.Function">
        /// </param>
        var paramArr = [ autoLoginUsrK, autoLoginUsrLoginString, detailsPanelData ];
        var req = eval('PageMethods.ClientRequest');
        if (req != null) {
            try {
                req('Spotted.Controls.Login, Spotted, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 'AutoLinkByAutoLoginUsr', paramArr, response, response);
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
    
    autoLinkByEmail: function Js_Controls_Login_Server$autoLinkByEmail(detailsPanelData, response) {
        /// <param name="detailsPanelData" type="Object">
        /// </param>
        /// <param name="response" type="Js.Library.Function">
        /// </param>
        var paramArr = [ detailsPanelData ];
        var req = eval('PageMethods.ClientRequest');
        if (req != null) {
            try {
                req('Spotted.Controls.Login, Spotted, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 'AutoLinkByEmail', paramArr, response, response);
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
    
    linkAccounts: function Js_Controls_Login_Server$linkAccounts(nickNameOrEmail, password, detailsPanelData, response) {
        /// <param name="nickNameOrEmail" type="String">
        /// </param>
        /// <param name="password" type="String">
        /// </param>
        /// <param name="detailsPanelData" type="Object">
        /// </param>
        /// <param name="response" type="Js.Library.Function">
        /// </param>
        var paramArr = [ nickNameOrEmail, password, detailsPanelData ];
        var req = eval('PageMethods.ClientRequest');
        if (req != null) {
            try {
                req('Spotted.Controls.Login, Spotted, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 'LinkAccounts', paramArr, response, response);
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
    
    unlinkAccount: function Js_Controls_Login_Server$unlinkAccount(password, response) {
        /// <param name="password" type="String">
        /// </param>
        /// <param name="response" type="Js.Library.Function">
        /// </param>
        var paramArr = [ password ];
        var req = eval('PageMethods.ClientRequest');
        if (req != null) {
            try {
                req('Spotted.Controls.Login, Spotted, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 'UnlinkAccount', paramArr, response, response);
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
// Js.Controls.Login.View

Js.Controls.Login.View = function Js_Controls_Login_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    /// <field name="server" type="Js.Controls.Login.Server">
    /// </field>
    /// <field name="_ConnectDialog" type="Object" domElement="true">
    /// </field>
    /// <field name="_ConnectDialogJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Inner" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_InnerJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Note" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_NoteJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoadingLabel" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoadingLabelJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoadingPanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoadingPanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Loading_CancelButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_Loading_CancelButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_ConnectingPanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_ConnectingPanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Connecting_PopupRetry" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_Connecting_PopupRetryJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Connecting_BackButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_Connecting_BackButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Connecting_CancelButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_Connecting_CancelButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_ErrorPanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_ErrorPanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Error_ErrorDescription" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_Error_ErrorDescriptionJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Error_TryAgainButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_Error_TryAgainButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Error_CancelButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_Error_CancelButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOutPanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOutPanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_ConnectButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_ConnectButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_CancelButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_CancelButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebookButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebookButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_ChoosePanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_ChoosePanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_Choose_LoginButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_Choose_LoginButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_Choose_SignupButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_Choose_SignupButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_Choose_BackButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_Choose_BackButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_Choose_CancelButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_Choose_CancelButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_LoginPanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_LoginPanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_Login_UsernameTextbox" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_Login_UsernameTextboxJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_Login_PasswordTextbox" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_Login_PasswordTextboxJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_Login_Error" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_Login_ErrorJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_Login_LoginButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_Login_LoginButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_Login_NoPasswordButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_Login_NoPasswordButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_Login_BackButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_Login_BackButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_Login_CancelButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_Login_CancelButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_Login_ForgottonPasswordButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_Login_ForgottonPasswordButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_LoginNoPasswordPanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_LoginNoPasswordPanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_LoginNoPassword_TryAgainButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_LoginNoPassword_TryAgainButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_LoginNoPassword_CancelButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_LoginNoPassword_CancelButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_PasswordResetPanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_PasswordResetPanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_PasswordReset_Title" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_PasswordReset_TitleJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_PasswordReset_UsernameTextbox" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_PasswordReset_UsernameTextboxJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_PasswordReset_SendLinkButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_PasswordReset_SendLinkButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_PasswordReset_MessageLabel" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_PasswordReset_MessageLabelJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_PasswordReset_ErrorLabel" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_PasswordReset_ErrorLabelJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_PasswordReset_BackButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_PasswordReset_BackButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_PasswordReset_CancelButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_PasswordReset_CancelButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp1Panel" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp1PanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp1_EmailPara" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp1_EmailParaJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp1_EmailTextbox" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp1_EmailTextboxJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp1_Password1Textbox" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp1_Password1TextboxJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp1_Password2Textbox" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp1_Password2TextboxJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp1_SaveButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp1_SaveButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp1_ErrorLabel" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp1_ErrorLabelJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp1_BackButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp1_BackButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp1_CancelButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp1_CancelButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp2Panel" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp2PanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextbox" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextboxJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp2_LastNameTextbox" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp2_LastNameTextboxJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp2_NicknameTextboxJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDown" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDownJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDown" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDownJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDown" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDownJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp2_SexMaleRadio" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp2_SexMaleRadioJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp2_SexFemaleRadio" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp2_SexFemaleRadioJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp2_SaveButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp2_SaveButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp2_ErrorLabelJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp2_BackButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp2_BackButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp2_CancelButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedOut_NoFacebook_SignUp2_CancelButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_NewAccount_ConfirmFacebookPanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_NewAccount_ConfirmFacebookPanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_NewAccount_ConfirmFacebook_Image" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_NewAccount_ConfirmFacebook_ImageJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_NewAccount_ConfirmFacebook_Link" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_NewAccount_ConfirmFacebook_LinkJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_NewAccount_ConfirmFacebook_YesButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_NewAccount_ConfirmFacebook_YesButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_NewAccount_ConfirmFacebook_NoButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_NewAccount_ConfirmFacebook_NoButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_NewAccount_ConfirmFacebook_BackButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_NewAccount_ConfirmFacebook_BackButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_NewAccount_ConfirmFacebook_CancelButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_NewAccount_ConfirmFacebook_CancelButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_NewAccount_NoEmailMatchPanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_NewAccount_NoEmailMatchPanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_NewAccount_NoEmailMatch_NewAccountButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_NewAccount_NoEmailMatch_NewAccountButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_NewAccount_NoEmailMatch_ChooseAccountButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_NewAccount_NoEmailMatch_ChooseAccountButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_NewAccount_NoEmailMatch_CancelButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_NewAccount_NoEmailMatch_CancelButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_NewAccount_NoEmailMatch_FacebookLogoutButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_NewAccount_NoEmailMatch_FacebookLogoutButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_NewAccount_NoEmailMatch_BackButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_NewAccount_NoEmailMatch_BackButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_NewAccount_EmailMatchPanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_NewAccount_EmailMatchPanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_NewAccount_EmailMatch_UserLink1" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_NewAccount_EmailMatch_UserLink1J" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_NewAccount_EmailMatch_AutoConnectButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_NewAccount_EmailMatch_AutoConnectButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_NewAccount_EmailMatch_ChooseAccountButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_NewAccount_EmailMatch_ChooseAccountButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_NewAccount_EmailMatch_CancelButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_NewAccount_EmailMatch_CancelButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_NewAccount_EmailMatch_FacebookLogoutButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_NewAccount_EmailMatch_FacebookLogoutButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_NewAccount_EmailMatch_BackButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_NewAccount_EmailMatch_BackButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_NewAccount_ChooseAccountPanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_NewAccount_ChooseAccountPanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_NewAccount_ChooseAccount_UsernameTextbox" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_NewAccount_ChooseAccount_UsernameTextboxJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_NewAccount_ChooseAccount_PasswordTextbox" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_NewAccount_ChooseAccount_PasswordTextboxJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_NewAccount_ChooseAccount_LinkAccountButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_NewAccount_ChooseAccount_LinkAccountButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_NewAccount_ChooseAccount_ErrorLabel" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_NewAccount_ChooseAccount_ErrorLabelJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_NewAccount_ChooseAccount_CancelButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_NewAccount_ChooseAccount_CancelButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_NewAccount_ChooseAccount_FacebookLogoutButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_NewAccount_ChooseAccount_FacebookLogoutButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_NewAccount_ChooseAccount_BackButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_NewAccount_ChooseAccount_BackButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_NewAccount_ChooseAccount_ForgottonPasswordButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_NewAccount_ChooseAccount_ForgottonPasswordButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_NewAccount_ForgotPasswordPanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_NewAccount_ForgotPasswordPanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_NewAccount_ForgotPassword_UsernameTextbox" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_NewAccount_ForgotPassword_UsernameTextboxJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_NewAccount_ForgotPassword_SendLinkButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_NewAccount_ForgotPassword_SendLinkButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_NewAccount_ForgotPassword_MessageLabel" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_NewAccount_ForgotPassword_MessageLabelJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_NewAccount_ForgotPassword_ErrorLabel" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_NewAccount_ForgotPassword_ErrorLabelJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_NewAccount_ForgotPassword_CancelButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_NewAccount_ForgotPassword_CancelButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_NewAccount_ForgotPassword_FacebookLogoutButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_NewAccount_ForgotPassword_FacebookLogoutButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_NewAccount_ForgotPassword_BackButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_NewAccount_ForgotPassword_BackButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedInPanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedInPanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedIn_LoggedInUsrLink" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedIn_LoggedInUsrLinkJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedIn_CloseButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedIn_CloseButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedIn_LogoutButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedIn_LogoutButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedIn_DisconnectLinkOuter" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedIn_DisconnectLinkOuterJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedIn_DisconnectButtonShowLink" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedIn_DisconnectButtonShowLinkJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedIn_DisconnectButtonOuter" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedIn_DisconnectButtonOuterJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedIn_DisconnectButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedIn_DisconnectButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LoggedIn_CancelButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LoggedIn_CancelButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_AutoLoginMismatchPanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_AutoLoginMismatchPanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_AutoLoginMismatch_AutoLoginUsrLink" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_AutoLoginMismatch_AutoLoginUsrLinkJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_AutoLoginMismatch_RetryButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_AutoLoginMismatch_RetryButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_AutoLoginMismatch_ContinueButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_AutoLoginMismatch_ContinueButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_AutoLoginMismatch_SwitchAccountsPara" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_AutoLoginMismatch_SwitchAccountsParaJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_AutoLoginMismatch_AutoLoginUsrLink2" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_AutoLoginMismatch_AutoLoginUsrLink2J" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_AutoLoginMismatch_SwitchAccountsShowLink" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_AutoLoginMismatch_SwitchAccountsShowLinkJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_AutoLoginMismatch_SwitchAccountsOuter" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_AutoLoginMismatch_SwitchAccountsOuterJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_AutoLoginMismatch_SwitchButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_AutoLoginMismatch_SwitchButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_AutoLoginMismatch_CancelButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_AutoLoginMismatch_CancelButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_CreatePasswordPanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_CreatePasswordPanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_CreatePassword_Password1Textbox" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_CreatePassword_Password1TextboxJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_CreatePassword_Password2Textbox" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_CreatePassword_Password2TextboxJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_CreatePassword_DisconnectButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_CreatePassword_DisconnectButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_CreatePassword_ErrorSpan" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_CreatePassword_ErrorSpanJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_CreatePassword_CancelButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_CreatePassword_CancelButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_CreatePassword_BackButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_CreatePassword_BackButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LikeButtonPanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LikeButtonPanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_LikeButton_CancelButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_LikeButton_CancelButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_DetailsPanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_DetailsPanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Details_MusicDropDown" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_Details_MusicDropDownJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Details_CountryDropDown" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_Details_CountryDropDownJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Details_PlaceDropDown" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_Details_PlaceDropDownJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Details_PlaceDefaultOuterSpan" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_Details_PlaceDefaultOuterSpanJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Details_PlaceDefaultSpan" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_Details_PlaceDefaultSpanJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Details_PlaceChangeLink" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_Details_PlaceChangeLinkJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Details_FacebookInfoPanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_Details_FacebookInfoPanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Details_WeeklyEmailInfoPanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_Details_WeeklyEmailInfoPanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Details_PartyInvitesInfoPanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_Details_PartyInvitesInfoPanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Details_FacebookCheck" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_Details_FacebookCheckJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Details_FacebookCheckLabel" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_Details_FacebookCheckLabelJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Details_FacebookInfoAnchor" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_Details_FacebookInfoAnchorJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Details_WeeklyEmailCheck" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_Details_WeeklyEmailCheckJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Details_WeeklyEmailCheckLabel" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_Details_WeeklyEmailCheckLabelJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Details_WeeklyEmailInfoAnchor" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_Details_WeeklyEmailInfoAnchorJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Details_PartyInvitesCheck" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_Details_PartyInvitesCheckJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Details_PartyInvitesCheckLabel" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_Details_PartyInvitesCheckLabelJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Details_PartyInvitesInfoAnchor" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_Details_PartyInvitesInfoAnchorJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Details_CancelButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_Details_CancelButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Details_BackButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_Details_BackButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Details_SaveButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_Details_SaveButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Details_PlaceErrorSpan" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_Details_PlaceErrorSpanJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_CaptchaPanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_CaptchaPanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Captcha_Img" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_Captcha_ImgJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Captcha_Textbox" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_Captcha_TextboxJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Captcha_SaveButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_Captcha_SaveButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Captcha_Error" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_Captcha_ErrorJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Captcha_BackButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_Captcha_BackButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Captcha_CancelButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_Captcha_CancelButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_DebugPanel" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_DebugPanelJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Debug_Output" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_Debug_OutputJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Debug_LogoutButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_Debug_LogoutButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Debug_DisconnectButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_Debug_DisconnectButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_Connect_Debug_AuthButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_Connect_Debug_AuthButtonJ" type="jQueryObject">
    /// </field>
    /// <field name="_ToggleAdminLinkButton" type="Object" domElement="true">
    /// </field>
    /// <field name="_ToggleAdminLinkButtonJ" type="jQueryObject">
    /// </field>
    this.clientId = clientId;
    this.server = new Js.Controls.Login.Server();
}
Js.Controls.Login.View.prototype = {
    clientId: null,
    server: null,
    
    get_connectDialog: function Js_Controls_Login_View$get_connectDialog() {
        /// <value type="Object" domElement="true"></value>
        if (this._ConnectDialog == null) {
            this._ConnectDialog = document.getElementById(this.clientId + '_ConnectDialog');
        }
        return this._ConnectDialog;
    },
    
    _ConnectDialog: null,
    
    get_connectDialogJ: function Js_Controls_Login_View$get_connectDialogJ() {
        /// <value type="jQueryObject"></value>
        if (this._ConnectDialogJ == null) {
            this._ConnectDialogJ = $('#' + this.clientId + '_ConnectDialog');
        }
        return this._ConnectDialogJ;
    },
    
    _ConnectDialogJ: null,
    
    get_connect_Inner: function Js_Controls_Login_View$get_connect_Inner() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Inner == null) {
            this._Connect_Inner = document.getElementById(this.clientId + '_Connect_Inner');
        }
        return this._Connect_Inner;
    },
    
    _Connect_Inner: null,
    
    get_connect_InnerJ: function Js_Controls_Login_View$get_connect_InnerJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_InnerJ == null) {
            this._Connect_InnerJ = $('#' + this.clientId + '_Connect_Inner');
        }
        return this._Connect_InnerJ;
    },
    
    _Connect_InnerJ: null,
    
    get_connect_Note: function Js_Controls_Login_View$get_connect_Note() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Note == null) {
            this._Connect_Note = document.getElementById(this.clientId + '_Connect_Note');
        }
        return this._Connect_Note;
    },
    
    _Connect_Note: null,
    
    get_connect_NoteJ: function Js_Controls_Login_View$get_connect_NoteJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_NoteJ == null) {
            this._Connect_NoteJ = $('#' + this.clientId + '_Connect_Note');
        }
        return this._Connect_NoteJ;
    },
    
    _Connect_NoteJ: null,
    
    get_connect_LoadingLabel: function Js_Controls_Login_View$get_connect_LoadingLabel() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoadingLabel == null) {
            this._Connect_LoadingLabel = document.getElementById(this.clientId + '_Connect_LoadingLabel');
        }
        return this._Connect_LoadingLabel;
    },
    
    _Connect_LoadingLabel: null,
    
    get_connect_LoadingLabelJ: function Js_Controls_Login_View$get_connect_LoadingLabelJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoadingLabelJ == null) {
            this._Connect_LoadingLabelJ = $('#' + this.clientId + '_Connect_LoadingLabel');
        }
        return this._Connect_LoadingLabelJ;
    },
    
    _Connect_LoadingLabelJ: null,
    
    get_connect_LoadingPanel: function Js_Controls_Login_View$get_connect_LoadingPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoadingPanel == null) {
            this._Connect_LoadingPanel = document.getElementById(this.clientId + '_Connect_LoadingPanel');
        }
        return this._Connect_LoadingPanel;
    },
    
    _Connect_LoadingPanel: null,
    
    get_connect_LoadingPanelJ: function Js_Controls_Login_View$get_connect_LoadingPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoadingPanelJ == null) {
            this._Connect_LoadingPanelJ = $('#' + this.clientId + '_Connect_LoadingPanel');
        }
        return this._Connect_LoadingPanelJ;
    },
    
    _Connect_LoadingPanelJ: null,
    
    get_connect_Loading_CancelButton: function Js_Controls_Login_View$get_connect_Loading_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Loading_CancelButton == null) {
            this._Connect_Loading_CancelButton = document.getElementById(this.clientId + '_Connect_Loading_CancelButton');
        }
        return this._Connect_Loading_CancelButton;
    },
    
    _Connect_Loading_CancelButton: null,
    
    get_connect_Loading_CancelButtonJ: function Js_Controls_Login_View$get_connect_Loading_CancelButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_Loading_CancelButtonJ == null) {
            this._Connect_Loading_CancelButtonJ = $('#' + this.clientId + '_Connect_Loading_CancelButton');
        }
        return this._Connect_Loading_CancelButtonJ;
    },
    
    _Connect_Loading_CancelButtonJ: null,
    
    get_connect_ConnectingPanel: function Js_Controls_Login_View$get_connect_ConnectingPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_ConnectingPanel == null) {
            this._Connect_ConnectingPanel = document.getElementById(this.clientId + '_Connect_ConnectingPanel');
        }
        return this._Connect_ConnectingPanel;
    },
    
    _Connect_ConnectingPanel: null,
    
    get_connect_ConnectingPanelJ: function Js_Controls_Login_View$get_connect_ConnectingPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_ConnectingPanelJ == null) {
            this._Connect_ConnectingPanelJ = $('#' + this.clientId + '_Connect_ConnectingPanel');
        }
        return this._Connect_ConnectingPanelJ;
    },
    
    _Connect_ConnectingPanelJ: null,
    
    get_connect_Connecting_PopupRetry: function Js_Controls_Login_View$get_connect_Connecting_PopupRetry() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Connecting_PopupRetry == null) {
            this._Connect_Connecting_PopupRetry = document.getElementById(this.clientId + '_Connect_Connecting_PopupRetry');
        }
        return this._Connect_Connecting_PopupRetry;
    },
    
    _Connect_Connecting_PopupRetry: null,
    
    get_connect_Connecting_PopupRetryJ: function Js_Controls_Login_View$get_connect_Connecting_PopupRetryJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_Connecting_PopupRetryJ == null) {
            this._Connect_Connecting_PopupRetryJ = $('#' + this.clientId + '_Connect_Connecting_PopupRetry');
        }
        return this._Connect_Connecting_PopupRetryJ;
    },
    
    _Connect_Connecting_PopupRetryJ: null,
    
    get_connect_Connecting_BackButton: function Js_Controls_Login_View$get_connect_Connecting_BackButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Connecting_BackButton == null) {
            this._Connect_Connecting_BackButton = document.getElementById(this.clientId + '_Connect_Connecting_BackButton');
        }
        return this._Connect_Connecting_BackButton;
    },
    
    _Connect_Connecting_BackButton: null,
    
    get_connect_Connecting_BackButtonJ: function Js_Controls_Login_View$get_connect_Connecting_BackButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_Connecting_BackButtonJ == null) {
            this._Connect_Connecting_BackButtonJ = $('#' + this.clientId + '_Connect_Connecting_BackButton');
        }
        return this._Connect_Connecting_BackButtonJ;
    },
    
    _Connect_Connecting_BackButtonJ: null,
    
    get_connect_Connecting_CancelButton: function Js_Controls_Login_View$get_connect_Connecting_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Connecting_CancelButton == null) {
            this._Connect_Connecting_CancelButton = document.getElementById(this.clientId + '_Connect_Connecting_CancelButton');
        }
        return this._Connect_Connecting_CancelButton;
    },
    
    _Connect_Connecting_CancelButton: null,
    
    get_connect_Connecting_CancelButtonJ: function Js_Controls_Login_View$get_connect_Connecting_CancelButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_Connecting_CancelButtonJ == null) {
            this._Connect_Connecting_CancelButtonJ = $('#' + this.clientId + '_Connect_Connecting_CancelButton');
        }
        return this._Connect_Connecting_CancelButtonJ;
    },
    
    _Connect_Connecting_CancelButtonJ: null,
    
    get_connect_ErrorPanel: function Js_Controls_Login_View$get_connect_ErrorPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_ErrorPanel == null) {
            this._Connect_ErrorPanel = document.getElementById(this.clientId + '_Connect_ErrorPanel');
        }
        return this._Connect_ErrorPanel;
    },
    
    _Connect_ErrorPanel: null,
    
    get_connect_ErrorPanelJ: function Js_Controls_Login_View$get_connect_ErrorPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_ErrorPanelJ == null) {
            this._Connect_ErrorPanelJ = $('#' + this.clientId + '_Connect_ErrorPanel');
        }
        return this._Connect_ErrorPanelJ;
    },
    
    _Connect_ErrorPanelJ: null,
    
    get_connect_Error_ErrorDescription: function Js_Controls_Login_View$get_connect_Error_ErrorDescription() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Error_ErrorDescription == null) {
            this._Connect_Error_ErrorDescription = document.getElementById(this.clientId + '_Connect_Error_ErrorDescription');
        }
        return this._Connect_Error_ErrorDescription;
    },
    
    _Connect_Error_ErrorDescription: null,
    
    get_connect_Error_ErrorDescriptionJ: function Js_Controls_Login_View$get_connect_Error_ErrorDescriptionJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_Error_ErrorDescriptionJ == null) {
            this._Connect_Error_ErrorDescriptionJ = $('#' + this.clientId + '_Connect_Error_ErrorDescription');
        }
        return this._Connect_Error_ErrorDescriptionJ;
    },
    
    _Connect_Error_ErrorDescriptionJ: null,
    
    get_connect_Error_TryAgainButton: function Js_Controls_Login_View$get_connect_Error_TryAgainButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Error_TryAgainButton == null) {
            this._Connect_Error_TryAgainButton = document.getElementById(this.clientId + '_Connect_Error_TryAgainButton');
        }
        return this._Connect_Error_TryAgainButton;
    },
    
    _Connect_Error_TryAgainButton: null,
    
    get_connect_Error_TryAgainButtonJ: function Js_Controls_Login_View$get_connect_Error_TryAgainButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_Error_TryAgainButtonJ == null) {
            this._Connect_Error_TryAgainButtonJ = $('#' + this.clientId + '_Connect_Error_TryAgainButton');
        }
        return this._Connect_Error_TryAgainButtonJ;
    },
    
    _Connect_Error_TryAgainButtonJ: null,
    
    get_connect_Error_CancelButton: function Js_Controls_Login_View$get_connect_Error_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Error_CancelButton == null) {
            this._Connect_Error_CancelButton = document.getElementById(this.clientId + '_Connect_Error_CancelButton');
        }
        return this._Connect_Error_CancelButton;
    },
    
    _Connect_Error_CancelButton: null,
    
    get_connect_Error_CancelButtonJ: function Js_Controls_Login_View$get_connect_Error_CancelButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_Error_CancelButtonJ == null) {
            this._Connect_Error_CancelButtonJ = $('#' + this.clientId + '_Connect_Error_CancelButton');
        }
        return this._Connect_Error_CancelButtonJ;
    },
    
    _Connect_Error_CancelButtonJ: null,
    
    get_connect_LoggedOutPanel: function Js_Controls_Login_View$get_connect_LoggedOutPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOutPanel == null) {
            this._Connect_LoggedOutPanel = document.getElementById(this.clientId + '_Connect_LoggedOutPanel');
        }
        return this._Connect_LoggedOutPanel;
    },
    
    _Connect_LoggedOutPanel: null,
    
    get_connect_LoggedOutPanelJ: function Js_Controls_Login_View$get_connect_LoggedOutPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOutPanelJ == null) {
            this._Connect_LoggedOutPanelJ = $('#' + this.clientId + '_Connect_LoggedOutPanel');
        }
        return this._Connect_LoggedOutPanelJ;
    },
    
    _Connect_LoggedOutPanelJ: null,
    
    get_connect_LoggedOut_ConnectButton: function Js_Controls_Login_View$get_connect_LoggedOut_ConnectButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_ConnectButton == null) {
            this._Connect_LoggedOut_ConnectButton = document.getElementById(this.clientId + '_Connect_LoggedOut_ConnectButton');
        }
        return this._Connect_LoggedOut_ConnectButton;
    },
    
    _Connect_LoggedOut_ConnectButton: null,
    
    get_connect_LoggedOut_ConnectButtonJ: function Js_Controls_Login_View$get_connect_LoggedOut_ConnectButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_ConnectButtonJ == null) {
            this._Connect_LoggedOut_ConnectButtonJ = $('#' + this.clientId + '_Connect_LoggedOut_ConnectButton');
        }
        return this._Connect_LoggedOut_ConnectButtonJ;
    },
    
    _Connect_LoggedOut_ConnectButtonJ: null,
    
    get_connect_LoggedOut_CancelButton: function Js_Controls_Login_View$get_connect_LoggedOut_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_CancelButton == null) {
            this._Connect_LoggedOut_CancelButton = document.getElementById(this.clientId + '_Connect_LoggedOut_CancelButton');
        }
        return this._Connect_LoggedOut_CancelButton;
    },
    
    _Connect_LoggedOut_CancelButton: null,
    
    get_connect_LoggedOut_CancelButtonJ: function Js_Controls_Login_View$get_connect_LoggedOut_CancelButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_CancelButtonJ == null) {
            this._Connect_LoggedOut_CancelButtonJ = $('#' + this.clientId + '_Connect_LoggedOut_CancelButton');
        }
        return this._Connect_LoggedOut_CancelButtonJ;
    },
    
    _Connect_LoggedOut_CancelButtonJ: null,
    
    get_connect_LoggedOut_NoFacebookButton: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebookButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebookButton == null) {
            this._Connect_LoggedOut_NoFacebookButton = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebookButton');
        }
        return this._Connect_LoggedOut_NoFacebookButton;
    },
    
    _Connect_LoggedOut_NoFacebookButton: null,
    
    get_connect_LoggedOut_NoFacebookButtonJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebookButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebookButtonJ == null) {
            this._Connect_LoggedOut_NoFacebookButtonJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebookButton');
        }
        return this._Connect_LoggedOut_NoFacebookButtonJ;
    },
    
    _Connect_LoggedOut_NoFacebookButtonJ: null,
    
    get_connect_LoggedOut_NoFacebook_ChoosePanel: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_ChoosePanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_ChoosePanel == null) {
            this._Connect_LoggedOut_NoFacebook_ChoosePanel = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_ChoosePanel');
        }
        return this._Connect_LoggedOut_NoFacebook_ChoosePanel;
    },
    
    _Connect_LoggedOut_NoFacebook_ChoosePanel: null,
    
    get_connect_LoggedOut_NoFacebook_ChoosePanelJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_ChoosePanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_ChoosePanelJ == null) {
            this._Connect_LoggedOut_NoFacebook_ChoosePanelJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_ChoosePanel');
        }
        return this._Connect_LoggedOut_NoFacebook_ChoosePanelJ;
    },
    
    _Connect_LoggedOut_NoFacebook_ChoosePanelJ: null,
    
    get_connect_LoggedOut_NoFacebook_Choose_LoginButton: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_Choose_LoginButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_Choose_LoginButton == null) {
            this._Connect_LoggedOut_NoFacebook_Choose_LoginButton = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_Choose_LoginButton');
        }
        return this._Connect_LoggedOut_NoFacebook_Choose_LoginButton;
    },
    
    _Connect_LoggedOut_NoFacebook_Choose_LoginButton: null,
    
    get_connect_LoggedOut_NoFacebook_Choose_LoginButtonJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_Choose_LoginButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_Choose_LoginButtonJ == null) {
            this._Connect_LoggedOut_NoFacebook_Choose_LoginButtonJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_Choose_LoginButton');
        }
        return this._Connect_LoggedOut_NoFacebook_Choose_LoginButtonJ;
    },
    
    _Connect_LoggedOut_NoFacebook_Choose_LoginButtonJ: null,
    
    get_connect_LoggedOut_NoFacebook_Choose_SignupButton: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_Choose_SignupButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_Choose_SignupButton == null) {
            this._Connect_LoggedOut_NoFacebook_Choose_SignupButton = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_Choose_SignupButton');
        }
        return this._Connect_LoggedOut_NoFacebook_Choose_SignupButton;
    },
    
    _Connect_LoggedOut_NoFacebook_Choose_SignupButton: null,
    
    get_connect_LoggedOut_NoFacebook_Choose_SignupButtonJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_Choose_SignupButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_Choose_SignupButtonJ == null) {
            this._Connect_LoggedOut_NoFacebook_Choose_SignupButtonJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_Choose_SignupButton');
        }
        return this._Connect_LoggedOut_NoFacebook_Choose_SignupButtonJ;
    },
    
    _Connect_LoggedOut_NoFacebook_Choose_SignupButtonJ: null,
    
    get_connect_LoggedOut_NoFacebook_Choose_BackButton: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_Choose_BackButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_Choose_BackButton == null) {
            this._Connect_LoggedOut_NoFacebook_Choose_BackButton = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_Choose_BackButton');
        }
        return this._Connect_LoggedOut_NoFacebook_Choose_BackButton;
    },
    
    _Connect_LoggedOut_NoFacebook_Choose_BackButton: null,
    
    get_connect_LoggedOut_NoFacebook_Choose_BackButtonJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_Choose_BackButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_Choose_BackButtonJ == null) {
            this._Connect_LoggedOut_NoFacebook_Choose_BackButtonJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_Choose_BackButton');
        }
        return this._Connect_LoggedOut_NoFacebook_Choose_BackButtonJ;
    },
    
    _Connect_LoggedOut_NoFacebook_Choose_BackButtonJ: null,
    
    get_connect_LoggedOut_NoFacebook_Choose_CancelButton: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_Choose_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_Choose_CancelButton == null) {
            this._Connect_LoggedOut_NoFacebook_Choose_CancelButton = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_Choose_CancelButton');
        }
        return this._Connect_LoggedOut_NoFacebook_Choose_CancelButton;
    },
    
    _Connect_LoggedOut_NoFacebook_Choose_CancelButton: null,
    
    get_connect_LoggedOut_NoFacebook_Choose_CancelButtonJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_Choose_CancelButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_Choose_CancelButtonJ == null) {
            this._Connect_LoggedOut_NoFacebook_Choose_CancelButtonJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_Choose_CancelButton');
        }
        return this._Connect_LoggedOut_NoFacebook_Choose_CancelButtonJ;
    },
    
    _Connect_LoggedOut_NoFacebook_Choose_CancelButtonJ: null,
    
    get_connect_LoggedOut_NoFacebook_LoginPanel: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_LoginPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_LoginPanel == null) {
            this._Connect_LoggedOut_NoFacebook_LoginPanel = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_LoginPanel');
        }
        return this._Connect_LoggedOut_NoFacebook_LoginPanel;
    },
    
    _Connect_LoggedOut_NoFacebook_LoginPanel: null,
    
    get_connect_LoggedOut_NoFacebook_LoginPanelJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_LoginPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_LoginPanelJ == null) {
            this._Connect_LoggedOut_NoFacebook_LoginPanelJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_LoginPanel');
        }
        return this._Connect_LoggedOut_NoFacebook_LoginPanelJ;
    },
    
    _Connect_LoggedOut_NoFacebook_LoginPanelJ: null,
    
    get_connect_LoggedOut_NoFacebook_Login_UsernameTextbox: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_Login_UsernameTextbox() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_Login_UsernameTextbox == null) {
            this._Connect_LoggedOut_NoFacebook_Login_UsernameTextbox = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_Login_UsernameTextbox');
        }
        return this._Connect_LoggedOut_NoFacebook_Login_UsernameTextbox;
    },
    
    _Connect_LoggedOut_NoFacebook_Login_UsernameTextbox: null,
    
    get_connect_LoggedOut_NoFacebook_Login_UsernameTextboxJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_Login_UsernameTextboxJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_Login_UsernameTextboxJ == null) {
            this._Connect_LoggedOut_NoFacebook_Login_UsernameTextboxJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_Login_UsernameTextbox');
        }
        return this._Connect_LoggedOut_NoFacebook_Login_UsernameTextboxJ;
    },
    
    _Connect_LoggedOut_NoFacebook_Login_UsernameTextboxJ: null,
    
    get_connect_LoggedOut_NoFacebook_Login_PasswordTextbox: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_Login_PasswordTextbox() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_Login_PasswordTextbox == null) {
            this._Connect_LoggedOut_NoFacebook_Login_PasswordTextbox = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_Login_PasswordTextbox');
        }
        return this._Connect_LoggedOut_NoFacebook_Login_PasswordTextbox;
    },
    
    _Connect_LoggedOut_NoFacebook_Login_PasswordTextbox: null,
    
    get_connect_LoggedOut_NoFacebook_Login_PasswordTextboxJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_Login_PasswordTextboxJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_Login_PasswordTextboxJ == null) {
            this._Connect_LoggedOut_NoFacebook_Login_PasswordTextboxJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_Login_PasswordTextbox');
        }
        return this._Connect_LoggedOut_NoFacebook_Login_PasswordTextboxJ;
    },
    
    _Connect_LoggedOut_NoFacebook_Login_PasswordTextboxJ: null,
    
    get_connect_LoggedOut_NoFacebook_Login_Error: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_Login_Error() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_Login_Error == null) {
            this._Connect_LoggedOut_NoFacebook_Login_Error = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_Login_Error');
        }
        return this._Connect_LoggedOut_NoFacebook_Login_Error;
    },
    
    _Connect_LoggedOut_NoFacebook_Login_Error: null,
    
    get_connect_LoggedOut_NoFacebook_Login_ErrorJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_Login_ErrorJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_Login_ErrorJ == null) {
            this._Connect_LoggedOut_NoFacebook_Login_ErrorJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_Login_Error');
        }
        return this._Connect_LoggedOut_NoFacebook_Login_ErrorJ;
    },
    
    _Connect_LoggedOut_NoFacebook_Login_ErrorJ: null,
    
    get_connect_LoggedOut_NoFacebook_Login_LoginButton: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_Login_LoginButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_Login_LoginButton == null) {
            this._Connect_LoggedOut_NoFacebook_Login_LoginButton = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_Login_LoginButton');
        }
        return this._Connect_LoggedOut_NoFacebook_Login_LoginButton;
    },
    
    _Connect_LoggedOut_NoFacebook_Login_LoginButton: null,
    
    get_connect_LoggedOut_NoFacebook_Login_LoginButtonJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_Login_LoginButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_Login_LoginButtonJ == null) {
            this._Connect_LoggedOut_NoFacebook_Login_LoginButtonJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_Login_LoginButton');
        }
        return this._Connect_LoggedOut_NoFacebook_Login_LoginButtonJ;
    },
    
    _Connect_LoggedOut_NoFacebook_Login_LoginButtonJ: null,
    
    get_connect_LoggedOut_NoFacebook_Login_NoPasswordButton: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_Login_NoPasswordButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_Login_NoPasswordButton == null) {
            this._Connect_LoggedOut_NoFacebook_Login_NoPasswordButton = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_Login_NoPasswordButton');
        }
        return this._Connect_LoggedOut_NoFacebook_Login_NoPasswordButton;
    },
    
    _Connect_LoggedOut_NoFacebook_Login_NoPasswordButton: null,
    
    get_connect_LoggedOut_NoFacebook_Login_NoPasswordButtonJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_Login_NoPasswordButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_Login_NoPasswordButtonJ == null) {
            this._Connect_LoggedOut_NoFacebook_Login_NoPasswordButtonJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_Login_NoPasswordButton');
        }
        return this._Connect_LoggedOut_NoFacebook_Login_NoPasswordButtonJ;
    },
    
    _Connect_LoggedOut_NoFacebook_Login_NoPasswordButtonJ: null,
    
    get_connect_LoggedOut_NoFacebook_Login_BackButton: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_Login_BackButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_Login_BackButton == null) {
            this._Connect_LoggedOut_NoFacebook_Login_BackButton = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_Login_BackButton');
        }
        return this._Connect_LoggedOut_NoFacebook_Login_BackButton;
    },
    
    _Connect_LoggedOut_NoFacebook_Login_BackButton: null,
    
    get_connect_LoggedOut_NoFacebook_Login_BackButtonJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_Login_BackButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_Login_BackButtonJ == null) {
            this._Connect_LoggedOut_NoFacebook_Login_BackButtonJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_Login_BackButton');
        }
        return this._Connect_LoggedOut_NoFacebook_Login_BackButtonJ;
    },
    
    _Connect_LoggedOut_NoFacebook_Login_BackButtonJ: null,
    
    get_connect_LoggedOut_NoFacebook_Login_CancelButton: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_Login_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_Login_CancelButton == null) {
            this._Connect_LoggedOut_NoFacebook_Login_CancelButton = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_Login_CancelButton');
        }
        return this._Connect_LoggedOut_NoFacebook_Login_CancelButton;
    },
    
    _Connect_LoggedOut_NoFacebook_Login_CancelButton: null,
    
    get_connect_LoggedOut_NoFacebook_Login_CancelButtonJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_Login_CancelButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_Login_CancelButtonJ == null) {
            this._Connect_LoggedOut_NoFacebook_Login_CancelButtonJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_Login_CancelButton');
        }
        return this._Connect_LoggedOut_NoFacebook_Login_CancelButtonJ;
    },
    
    _Connect_LoggedOut_NoFacebook_Login_CancelButtonJ: null,
    
    get_connect_LoggedOut_NoFacebook_Login_ForgottonPasswordButton: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_Login_ForgottonPasswordButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_Login_ForgottonPasswordButton == null) {
            this._Connect_LoggedOut_NoFacebook_Login_ForgottonPasswordButton = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_Login_ForgottonPasswordButton');
        }
        return this._Connect_LoggedOut_NoFacebook_Login_ForgottonPasswordButton;
    },
    
    _Connect_LoggedOut_NoFacebook_Login_ForgottonPasswordButton: null,
    
    get_connect_LoggedOut_NoFacebook_Login_ForgottonPasswordButtonJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_Login_ForgottonPasswordButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_Login_ForgottonPasswordButtonJ == null) {
            this._Connect_LoggedOut_NoFacebook_Login_ForgottonPasswordButtonJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_Login_ForgottonPasswordButton');
        }
        return this._Connect_LoggedOut_NoFacebook_Login_ForgottonPasswordButtonJ;
    },
    
    _Connect_LoggedOut_NoFacebook_Login_ForgottonPasswordButtonJ: null,
    
    get_connect_LoggedOut_NoFacebook_LoginNoPasswordPanel: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_LoginNoPasswordPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_LoginNoPasswordPanel == null) {
            this._Connect_LoggedOut_NoFacebook_LoginNoPasswordPanel = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_LoginNoPasswordPanel');
        }
        return this._Connect_LoggedOut_NoFacebook_LoginNoPasswordPanel;
    },
    
    _Connect_LoggedOut_NoFacebook_LoginNoPasswordPanel: null,
    
    get_connect_LoggedOut_NoFacebook_LoginNoPasswordPanelJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_LoginNoPasswordPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_LoginNoPasswordPanelJ == null) {
            this._Connect_LoggedOut_NoFacebook_LoginNoPasswordPanelJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_LoginNoPasswordPanel');
        }
        return this._Connect_LoggedOut_NoFacebook_LoginNoPasswordPanelJ;
    },
    
    _Connect_LoggedOut_NoFacebook_LoginNoPasswordPanelJ: null,
    
    get_connect_LoggedOut_NoFacebook_LoginNoPassword_TryAgainButton: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_LoginNoPassword_TryAgainButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_LoginNoPassword_TryAgainButton == null) {
            this._Connect_LoggedOut_NoFacebook_LoginNoPassword_TryAgainButton = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_LoginNoPassword_TryAgainButton');
        }
        return this._Connect_LoggedOut_NoFacebook_LoginNoPassword_TryAgainButton;
    },
    
    _Connect_LoggedOut_NoFacebook_LoginNoPassword_TryAgainButton: null,
    
    get_connect_LoggedOut_NoFacebook_LoginNoPassword_TryAgainButtonJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_LoginNoPassword_TryAgainButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_LoginNoPassword_TryAgainButtonJ == null) {
            this._Connect_LoggedOut_NoFacebook_LoginNoPassword_TryAgainButtonJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_LoginNoPassword_TryAgainButton');
        }
        return this._Connect_LoggedOut_NoFacebook_LoginNoPassword_TryAgainButtonJ;
    },
    
    _Connect_LoggedOut_NoFacebook_LoginNoPassword_TryAgainButtonJ: null,
    
    get_connect_LoggedOut_NoFacebook_LoginNoPassword_CancelButton: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_LoginNoPassword_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_LoginNoPassword_CancelButton == null) {
            this._Connect_LoggedOut_NoFacebook_LoginNoPassword_CancelButton = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_LoginNoPassword_CancelButton');
        }
        return this._Connect_LoggedOut_NoFacebook_LoginNoPassword_CancelButton;
    },
    
    _Connect_LoggedOut_NoFacebook_LoginNoPassword_CancelButton: null,
    
    get_connect_LoggedOut_NoFacebook_LoginNoPassword_CancelButtonJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_LoginNoPassword_CancelButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_LoginNoPassword_CancelButtonJ == null) {
            this._Connect_LoggedOut_NoFacebook_LoginNoPassword_CancelButtonJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_LoginNoPassword_CancelButton');
        }
        return this._Connect_LoggedOut_NoFacebook_LoginNoPassword_CancelButtonJ;
    },
    
    _Connect_LoggedOut_NoFacebook_LoginNoPassword_CancelButtonJ: null,
    
    get_connect_LoggedOut_NoFacebook_PasswordResetPanel: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_PasswordResetPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_PasswordResetPanel == null) {
            this._Connect_LoggedOut_NoFacebook_PasswordResetPanel = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_PasswordResetPanel');
        }
        return this._Connect_LoggedOut_NoFacebook_PasswordResetPanel;
    },
    
    _Connect_LoggedOut_NoFacebook_PasswordResetPanel: null,
    
    get_connect_LoggedOut_NoFacebook_PasswordResetPanelJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_PasswordResetPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_PasswordResetPanelJ == null) {
            this._Connect_LoggedOut_NoFacebook_PasswordResetPanelJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_PasswordResetPanel');
        }
        return this._Connect_LoggedOut_NoFacebook_PasswordResetPanelJ;
    },
    
    _Connect_LoggedOut_NoFacebook_PasswordResetPanelJ: null,
    
    get_connect_LoggedOut_NoFacebook_PasswordReset_Title: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_PasswordReset_Title() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_PasswordReset_Title == null) {
            this._Connect_LoggedOut_NoFacebook_PasswordReset_Title = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_PasswordReset_Title');
        }
        return this._Connect_LoggedOut_NoFacebook_PasswordReset_Title;
    },
    
    _Connect_LoggedOut_NoFacebook_PasswordReset_Title: null,
    
    get_connect_LoggedOut_NoFacebook_PasswordReset_TitleJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_PasswordReset_TitleJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_PasswordReset_TitleJ == null) {
            this._Connect_LoggedOut_NoFacebook_PasswordReset_TitleJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_PasswordReset_Title');
        }
        return this._Connect_LoggedOut_NoFacebook_PasswordReset_TitleJ;
    },
    
    _Connect_LoggedOut_NoFacebook_PasswordReset_TitleJ: null,
    
    get_connect_LoggedOut_NoFacebook_PasswordReset_UsernameTextbox: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_PasswordReset_UsernameTextbox() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_PasswordReset_UsernameTextbox == null) {
            this._Connect_LoggedOut_NoFacebook_PasswordReset_UsernameTextbox = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_PasswordReset_UsernameTextbox');
        }
        return this._Connect_LoggedOut_NoFacebook_PasswordReset_UsernameTextbox;
    },
    
    _Connect_LoggedOut_NoFacebook_PasswordReset_UsernameTextbox: null,
    
    get_connect_LoggedOut_NoFacebook_PasswordReset_UsernameTextboxJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_PasswordReset_UsernameTextboxJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_PasswordReset_UsernameTextboxJ == null) {
            this._Connect_LoggedOut_NoFacebook_PasswordReset_UsernameTextboxJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_PasswordReset_UsernameTextbox');
        }
        return this._Connect_LoggedOut_NoFacebook_PasswordReset_UsernameTextboxJ;
    },
    
    _Connect_LoggedOut_NoFacebook_PasswordReset_UsernameTextboxJ: null,
    
    get_connect_LoggedOut_NoFacebook_PasswordReset_SendLinkButton: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_PasswordReset_SendLinkButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_PasswordReset_SendLinkButton == null) {
            this._Connect_LoggedOut_NoFacebook_PasswordReset_SendLinkButton = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_PasswordReset_SendLinkButton');
        }
        return this._Connect_LoggedOut_NoFacebook_PasswordReset_SendLinkButton;
    },
    
    _Connect_LoggedOut_NoFacebook_PasswordReset_SendLinkButton: null,
    
    get_connect_LoggedOut_NoFacebook_PasswordReset_SendLinkButtonJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_PasswordReset_SendLinkButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_PasswordReset_SendLinkButtonJ == null) {
            this._Connect_LoggedOut_NoFacebook_PasswordReset_SendLinkButtonJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_PasswordReset_SendLinkButton');
        }
        return this._Connect_LoggedOut_NoFacebook_PasswordReset_SendLinkButtonJ;
    },
    
    _Connect_LoggedOut_NoFacebook_PasswordReset_SendLinkButtonJ: null,
    
    get_connect_LoggedOut_NoFacebook_PasswordReset_MessageLabel: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_PasswordReset_MessageLabel() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_PasswordReset_MessageLabel == null) {
            this._Connect_LoggedOut_NoFacebook_PasswordReset_MessageLabel = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_PasswordReset_MessageLabel');
        }
        return this._Connect_LoggedOut_NoFacebook_PasswordReset_MessageLabel;
    },
    
    _Connect_LoggedOut_NoFacebook_PasswordReset_MessageLabel: null,
    
    get_connect_LoggedOut_NoFacebook_PasswordReset_MessageLabelJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_PasswordReset_MessageLabelJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_PasswordReset_MessageLabelJ == null) {
            this._Connect_LoggedOut_NoFacebook_PasswordReset_MessageLabelJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_PasswordReset_MessageLabel');
        }
        return this._Connect_LoggedOut_NoFacebook_PasswordReset_MessageLabelJ;
    },
    
    _Connect_LoggedOut_NoFacebook_PasswordReset_MessageLabelJ: null,
    
    get_connect_LoggedOut_NoFacebook_PasswordReset_ErrorLabel: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_PasswordReset_ErrorLabel() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_PasswordReset_ErrorLabel == null) {
            this._Connect_LoggedOut_NoFacebook_PasswordReset_ErrorLabel = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_PasswordReset_ErrorLabel');
        }
        return this._Connect_LoggedOut_NoFacebook_PasswordReset_ErrorLabel;
    },
    
    _Connect_LoggedOut_NoFacebook_PasswordReset_ErrorLabel: null,
    
    get_connect_LoggedOut_NoFacebook_PasswordReset_ErrorLabelJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_PasswordReset_ErrorLabelJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_PasswordReset_ErrorLabelJ == null) {
            this._Connect_LoggedOut_NoFacebook_PasswordReset_ErrorLabelJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_PasswordReset_ErrorLabel');
        }
        return this._Connect_LoggedOut_NoFacebook_PasswordReset_ErrorLabelJ;
    },
    
    _Connect_LoggedOut_NoFacebook_PasswordReset_ErrorLabelJ: null,
    
    get_connect_LoggedOut_NoFacebook_PasswordReset_BackButton: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_PasswordReset_BackButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_PasswordReset_BackButton == null) {
            this._Connect_LoggedOut_NoFacebook_PasswordReset_BackButton = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_PasswordReset_BackButton');
        }
        return this._Connect_LoggedOut_NoFacebook_PasswordReset_BackButton;
    },
    
    _Connect_LoggedOut_NoFacebook_PasswordReset_BackButton: null,
    
    get_connect_LoggedOut_NoFacebook_PasswordReset_BackButtonJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_PasswordReset_BackButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_PasswordReset_BackButtonJ == null) {
            this._Connect_LoggedOut_NoFacebook_PasswordReset_BackButtonJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_PasswordReset_BackButton');
        }
        return this._Connect_LoggedOut_NoFacebook_PasswordReset_BackButtonJ;
    },
    
    _Connect_LoggedOut_NoFacebook_PasswordReset_BackButtonJ: null,
    
    get_connect_LoggedOut_NoFacebook_PasswordReset_CancelButton: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_PasswordReset_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_PasswordReset_CancelButton == null) {
            this._Connect_LoggedOut_NoFacebook_PasswordReset_CancelButton = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_PasswordReset_CancelButton');
        }
        return this._Connect_LoggedOut_NoFacebook_PasswordReset_CancelButton;
    },
    
    _Connect_LoggedOut_NoFacebook_PasswordReset_CancelButton: null,
    
    get_connect_LoggedOut_NoFacebook_PasswordReset_CancelButtonJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_PasswordReset_CancelButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_PasswordReset_CancelButtonJ == null) {
            this._Connect_LoggedOut_NoFacebook_PasswordReset_CancelButtonJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_PasswordReset_CancelButton');
        }
        return this._Connect_LoggedOut_NoFacebook_PasswordReset_CancelButtonJ;
    },
    
    _Connect_LoggedOut_NoFacebook_PasswordReset_CancelButtonJ: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp1Panel: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp1Panel() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp1Panel == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp1Panel = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp1Panel');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp1Panel;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp1Panel: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp1PanelJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp1PanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp1PanelJ == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp1PanelJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp1Panel');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp1PanelJ;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp1PanelJ: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp1_EmailPara: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp1_EmailPara() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp1_EmailPara == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp1_EmailPara = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp1_EmailPara');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp1_EmailPara;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp1_EmailPara: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp1_EmailParaJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp1_EmailParaJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp1_EmailParaJ == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp1_EmailParaJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp1_EmailPara');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp1_EmailParaJ;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp1_EmailParaJ: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp1_EmailTextbox: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp1_EmailTextbox() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp1_EmailTextbox == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp1_EmailTextbox = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp1_EmailTextbox');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp1_EmailTextbox;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp1_EmailTextbox: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp1_EmailTextboxJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp1_EmailTextboxJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp1_EmailTextboxJ == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp1_EmailTextboxJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp1_EmailTextbox');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp1_EmailTextboxJ;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp1_EmailTextboxJ: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp1_Password1Textbox: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp1_Password1Textbox() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp1_Password1Textbox == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp1_Password1Textbox = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp1_Password1Textbox');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp1_Password1Textbox;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp1_Password1Textbox: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp1_Password1TextboxJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp1_Password1TextboxJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp1_Password1TextboxJ == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp1_Password1TextboxJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp1_Password1Textbox');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp1_Password1TextboxJ;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp1_Password1TextboxJ: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp1_Password2Textbox: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp1_Password2Textbox() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp1_Password2Textbox == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp1_Password2Textbox = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp1_Password2Textbox');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp1_Password2Textbox;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp1_Password2Textbox: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp1_Password2TextboxJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp1_Password2TextboxJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp1_Password2TextboxJ == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp1_Password2TextboxJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp1_Password2Textbox');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp1_Password2TextboxJ;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp1_Password2TextboxJ: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp1_SaveButton: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp1_SaveButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp1_SaveButton == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp1_SaveButton = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp1_SaveButton');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp1_SaveButton;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp1_SaveButton: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp1_SaveButtonJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp1_SaveButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp1_SaveButtonJ == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp1_SaveButtonJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp1_SaveButton');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp1_SaveButtonJ;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp1_SaveButtonJ: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp1_ErrorLabel: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp1_ErrorLabel() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp1_ErrorLabel == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp1_ErrorLabel = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp1_ErrorLabel');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp1_ErrorLabel;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp1_ErrorLabel: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp1_ErrorLabelJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp1_ErrorLabelJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp1_ErrorLabelJ == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp1_ErrorLabelJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp1_ErrorLabel');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp1_ErrorLabelJ;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp1_ErrorLabelJ: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp1_BackButton: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp1_BackButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp1_BackButton == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp1_BackButton = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp1_BackButton');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp1_BackButton;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp1_BackButton: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp1_BackButtonJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp1_BackButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp1_BackButtonJ == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp1_BackButtonJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp1_BackButton');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp1_BackButtonJ;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp1_BackButtonJ: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp1_CancelButton: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp1_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp1_CancelButton == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp1_CancelButton = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp1_CancelButton');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp1_CancelButton;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp1_CancelButton: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp1_CancelButtonJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp1_CancelButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp1_CancelButtonJ == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp1_CancelButtonJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp1_CancelButton');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp1_CancelButtonJ;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp1_CancelButtonJ: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp2Panel: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp2Panel() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp2Panel == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp2Panel = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp2Panel');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp2Panel;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp2Panel: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp2PanelJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp2PanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp2PanelJ == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp2PanelJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp2Panel');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp2PanelJ;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp2PanelJ: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextbox: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextbox() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextbox == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextbox = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextbox');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextbox;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextbox: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextboxJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextboxJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextboxJ == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextboxJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextbox');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextboxJ;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp2_FirstNameTextboxJ: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp2_LastNameTextbox: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp2_LastNameTextbox() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp2_LastNameTextbox == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp2_LastNameTextbox = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp2_LastNameTextbox');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp2_LastNameTextbox;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp2_LastNameTextbox: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp2_LastNameTextboxJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp2_LastNameTextboxJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp2_LastNameTextboxJ == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp2_LastNameTextboxJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp2_LastNameTextbox');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp2_LastNameTextboxJ;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp2_LastNameTextboxJ: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp2_NicknameTextboxJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp2_NicknameTextboxJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp2_NicknameTextboxJ == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp2_NicknameTextboxJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp2_NicknameTextbox');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp2_NicknameTextboxJ;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp2_NicknameTextboxJ: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDown: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDown() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDown == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDown = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDown');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDown;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDown: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDownJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDownJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDownJ == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDownJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDown');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDownJ;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthDayDropDownJ: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDown: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDown() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDown == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDown = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDown');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDown;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDown: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDownJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDownJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDownJ == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDownJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDown');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDownJ;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthMonthDropDownJ: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDown: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDown() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDown == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDown = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDown');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDown;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDown: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDownJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDownJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDownJ == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDownJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDown');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDownJ;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp2_DateOfBirthYearDropDownJ: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp2_SexMaleRadio: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp2_SexMaleRadio() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp2_SexMaleRadio == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp2_SexMaleRadio = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp2_SexMaleRadio');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp2_SexMaleRadio;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp2_SexMaleRadio: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp2_SexMaleRadioJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp2_SexMaleRadioJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp2_SexMaleRadioJ == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp2_SexMaleRadioJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp2_SexMaleRadio');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp2_SexMaleRadioJ;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp2_SexMaleRadioJ: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp2_SexFemaleRadio: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp2_SexFemaleRadio() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp2_SexFemaleRadio == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp2_SexFemaleRadio = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp2_SexFemaleRadio');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp2_SexFemaleRadio;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp2_SexFemaleRadio: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp2_SexFemaleRadioJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp2_SexFemaleRadioJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp2_SexFemaleRadioJ == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp2_SexFemaleRadioJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp2_SexFemaleRadio');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp2_SexFemaleRadioJ;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp2_SexFemaleRadioJ: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp2_SaveButton: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp2_SaveButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp2_SaveButton == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp2_SaveButton = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp2_SaveButton');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp2_SaveButton;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp2_SaveButton: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp2_SaveButtonJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp2_SaveButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp2_SaveButtonJ == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp2_SaveButtonJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp2_SaveButton');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp2_SaveButtonJ;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp2_SaveButtonJ: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp2_ErrorLabelJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp2_ErrorLabelJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp2_ErrorLabelJ == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp2_ErrorLabelJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp2_ErrorLabel');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp2_ErrorLabelJ;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp2_ErrorLabelJ: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp2_BackButton: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp2_BackButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp2_BackButton == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp2_BackButton = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp2_BackButton');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp2_BackButton;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp2_BackButton: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp2_BackButtonJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp2_BackButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp2_BackButtonJ == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp2_BackButtonJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp2_BackButton');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp2_BackButtonJ;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp2_BackButtonJ: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp2_CancelButton: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp2_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp2_CancelButton == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp2_CancelButton = document.getElementById(this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp2_CancelButton');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp2_CancelButton;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp2_CancelButton: null,
    
    get_connect_LoggedOut_NoFacebook_SignUp2_CancelButtonJ: function Js_Controls_Login_View$get_connect_LoggedOut_NoFacebook_SignUp2_CancelButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedOut_NoFacebook_SignUp2_CancelButtonJ == null) {
            this._Connect_LoggedOut_NoFacebook_SignUp2_CancelButtonJ = $('#' + this.clientId + '_Connect_LoggedOut_NoFacebook_SignUp2_CancelButton');
        }
        return this._Connect_LoggedOut_NoFacebook_SignUp2_CancelButtonJ;
    },
    
    _Connect_LoggedOut_NoFacebook_SignUp2_CancelButtonJ: null,
    
    get_connect_NewAccount_ConfirmFacebookPanel: function Js_Controls_Login_View$get_connect_NewAccount_ConfirmFacebookPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_NewAccount_ConfirmFacebookPanel == null) {
            this._Connect_NewAccount_ConfirmFacebookPanel = document.getElementById(this.clientId + '_Connect_NewAccount_ConfirmFacebookPanel');
        }
        return this._Connect_NewAccount_ConfirmFacebookPanel;
    },
    
    _Connect_NewAccount_ConfirmFacebookPanel: null,
    
    get_connect_NewAccount_ConfirmFacebookPanelJ: function Js_Controls_Login_View$get_connect_NewAccount_ConfirmFacebookPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_NewAccount_ConfirmFacebookPanelJ == null) {
            this._Connect_NewAccount_ConfirmFacebookPanelJ = $('#' + this.clientId + '_Connect_NewAccount_ConfirmFacebookPanel');
        }
        return this._Connect_NewAccount_ConfirmFacebookPanelJ;
    },
    
    _Connect_NewAccount_ConfirmFacebookPanelJ: null,
    
    get_connect_NewAccount_ConfirmFacebook_Image: function Js_Controls_Login_View$get_connect_NewAccount_ConfirmFacebook_Image() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_NewAccount_ConfirmFacebook_Image == null) {
            this._Connect_NewAccount_ConfirmFacebook_Image = document.getElementById(this.clientId + '_Connect_NewAccount_ConfirmFacebook_Image');
        }
        return this._Connect_NewAccount_ConfirmFacebook_Image;
    },
    
    _Connect_NewAccount_ConfirmFacebook_Image: null,
    
    get_connect_NewAccount_ConfirmFacebook_ImageJ: function Js_Controls_Login_View$get_connect_NewAccount_ConfirmFacebook_ImageJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_NewAccount_ConfirmFacebook_ImageJ == null) {
            this._Connect_NewAccount_ConfirmFacebook_ImageJ = $('#' + this.clientId + '_Connect_NewAccount_ConfirmFacebook_Image');
        }
        return this._Connect_NewAccount_ConfirmFacebook_ImageJ;
    },
    
    _Connect_NewAccount_ConfirmFacebook_ImageJ: null,
    
    get_connect_NewAccount_ConfirmFacebook_Link: function Js_Controls_Login_View$get_connect_NewAccount_ConfirmFacebook_Link() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_NewAccount_ConfirmFacebook_Link == null) {
            this._Connect_NewAccount_ConfirmFacebook_Link = document.getElementById(this.clientId + '_Connect_NewAccount_ConfirmFacebook_Link');
        }
        return this._Connect_NewAccount_ConfirmFacebook_Link;
    },
    
    _Connect_NewAccount_ConfirmFacebook_Link: null,
    
    get_connect_NewAccount_ConfirmFacebook_LinkJ: function Js_Controls_Login_View$get_connect_NewAccount_ConfirmFacebook_LinkJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_NewAccount_ConfirmFacebook_LinkJ == null) {
            this._Connect_NewAccount_ConfirmFacebook_LinkJ = $('#' + this.clientId + '_Connect_NewAccount_ConfirmFacebook_Link');
        }
        return this._Connect_NewAccount_ConfirmFacebook_LinkJ;
    },
    
    _Connect_NewAccount_ConfirmFacebook_LinkJ: null,
    
    get_connect_NewAccount_ConfirmFacebook_YesButton: function Js_Controls_Login_View$get_connect_NewAccount_ConfirmFacebook_YesButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_NewAccount_ConfirmFacebook_YesButton == null) {
            this._Connect_NewAccount_ConfirmFacebook_YesButton = document.getElementById(this.clientId + '_Connect_NewAccount_ConfirmFacebook_YesButton');
        }
        return this._Connect_NewAccount_ConfirmFacebook_YesButton;
    },
    
    _Connect_NewAccount_ConfirmFacebook_YesButton: null,
    
    get_connect_NewAccount_ConfirmFacebook_YesButtonJ: function Js_Controls_Login_View$get_connect_NewAccount_ConfirmFacebook_YesButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_NewAccount_ConfirmFacebook_YesButtonJ == null) {
            this._Connect_NewAccount_ConfirmFacebook_YesButtonJ = $('#' + this.clientId + '_Connect_NewAccount_ConfirmFacebook_YesButton');
        }
        return this._Connect_NewAccount_ConfirmFacebook_YesButtonJ;
    },
    
    _Connect_NewAccount_ConfirmFacebook_YesButtonJ: null,
    
    get_connect_NewAccount_ConfirmFacebook_NoButton: function Js_Controls_Login_View$get_connect_NewAccount_ConfirmFacebook_NoButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_NewAccount_ConfirmFacebook_NoButton == null) {
            this._Connect_NewAccount_ConfirmFacebook_NoButton = document.getElementById(this.clientId + '_Connect_NewAccount_ConfirmFacebook_NoButton');
        }
        return this._Connect_NewAccount_ConfirmFacebook_NoButton;
    },
    
    _Connect_NewAccount_ConfirmFacebook_NoButton: null,
    
    get_connect_NewAccount_ConfirmFacebook_NoButtonJ: function Js_Controls_Login_View$get_connect_NewAccount_ConfirmFacebook_NoButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_NewAccount_ConfirmFacebook_NoButtonJ == null) {
            this._Connect_NewAccount_ConfirmFacebook_NoButtonJ = $('#' + this.clientId + '_Connect_NewAccount_ConfirmFacebook_NoButton');
        }
        return this._Connect_NewAccount_ConfirmFacebook_NoButtonJ;
    },
    
    _Connect_NewAccount_ConfirmFacebook_NoButtonJ: null,
    
    get_connect_NewAccount_ConfirmFacebook_BackButton: function Js_Controls_Login_View$get_connect_NewAccount_ConfirmFacebook_BackButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_NewAccount_ConfirmFacebook_BackButton == null) {
            this._Connect_NewAccount_ConfirmFacebook_BackButton = document.getElementById(this.clientId + '_Connect_NewAccount_ConfirmFacebook_BackButton');
        }
        return this._Connect_NewAccount_ConfirmFacebook_BackButton;
    },
    
    _Connect_NewAccount_ConfirmFacebook_BackButton: null,
    
    get_connect_NewAccount_ConfirmFacebook_BackButtonJ: function Js_Controls_Login_View$get_connect_NewAccount_ConfirmFacebook_BackButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_NewAccount_ConfirmFacebook_BackButtonJ == null) {
            this._Connect_NewAccount_ConfirmFacebook_BackButtonJ = $('#' + this.clientId + '_Connect_NewAccount_ConfirmFacebook_BackButton');
        }
        return this._Connect_NewAccount_ConfirmFacebook_BackButtonJ;
    },
    
    _Connect_NewAccount_ConfirmFacebook_BackButtonJ: null,
    
    get_connect_NewAccount_ConfirmFacebook_CancelButton: function Js_Controls_Login_View$get_connect_NewAccount_ConfirmFacebook_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_NewAccount_ConfirmFacebook_CancelButton == null) {
            this._Connect_NewAccount_ConfirmFacebook_CancelButton = document.getElementById(this.clientId + '_Connect_NewAccount_ConfirmFacebook_CancelButton');
        }
        return this._Connect_NewAccount_ConfirmFacebook_CancelButton;
    },
    
    _Connect_NewAccount_ConfirmFacebook_CancelButton: null,
    
    get_connect_NewAccount_ConfirmFacebook_CancelButtonJ: function Js_Controls_Login_View$get_connect_NewAccount_ConfirmFacebook_CancelButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_NewAccount_ConfirmFacebook_CancelButtonJ == null) {
            this._Connect_NewAccount_ConfirmFacebook_CancelButtonJ = $('#' + this.clientId + '_Connect_NewAccount_ConfirmFacebook_CancelButton');
        }
        return this._Connect_NewAccount_ConfirmFacebook_CancelButtonJ;
    },
    
    _Connect_NewAccount_ConfirmFacebook_CancelButtonJ: null,
    
    get_connect_NewAccount_NoEmailMatchPanel: function Js_Controls_Login_View$get_connect_NewAccount_NoEmailMatchPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_NewAccount_NoEmailMatchPanel == null) {
            this._Connect_NewAccount_NoEmailMatchPanel = document.getElementById(this.clientId + '_Connect_NewAccount_NoEmailMatchPanel');
        }
        return this._Connect_NewAccount_NoEmailMatchPanel;
    },
    
    _Connect_NewAccount_NoEmailMatchPanel: null,
    
    get_connect_NewAccount_NoEmailMatchPanelJ: function Js_Controls_Login_View$get_connect_NewAccount_NoEmailMatchPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_NewAccount_NoEmailMatchPanelJ == null) {
            this._Connect_NewAccount_NoEmailMatchPanelJ = $('#' + this.clientId + '_Connect_NewAccount_NoEmailMatchPanel');
        }
        return this._Connect_NewAccount_NoEmailMatchPanelJ;
    },
    
    _Connect_NewAccount_NoEmailMatchPanelJ: null,
    
    get_connect_NewAccount_NoEmailMatch_NewAccountButton: function Js_Controls_Login_View$get_connect_NewAccount_NoEmailMatch_NewAccountButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_NewAccount_NoEmailMatch_NewAccountButton == null) {
            this._Connect_NewAccount_NoEmailMatch_NewAccountButton = document.getElementById(this.clientId + '_Connect_NewAccount_NoEmailMatch_NewAccountButton');
        }
        return this._Connect_NewAccount_NoEmailMatch_NewAccountButton;
    },
    
    _Connect_NewAccount_NoEmailMatch_NewAccountButton: null,
    
    get_connect_NewAccount_NoEmailMatch_NewAccountButtonJ: function Js_Controls_Login_View$get_connect_NewAccount_NoEmailMatch_NewAccountButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_NewAccount_NoEmailMatch_NewAccountButtonJ == null) {
            this._Connect_NewAccount_NoEmailMatch_NewAccountButtonJ = $('#' + this.clientId + '_Connect_NewAccount_NoEmailMatch_NewAccountButton');
        }
        return this._Connect_NewAccount_NoEmailMatch_NewAccountButtonJ;
    },
    
    _Connect_NewAccount_NoEmailMatch_NewAccountButtonJ: null,
    
    get_connect_NewAccount_NoEmailMatch_ChooseAccountButton: function Js_Controls_Login_View$get_connect_NewAccount_NoEmailMatch_ChooseAccountButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_NewAccount_NoEmailMatch_ChooseAccountButton == null) {
            this._Connect_NewAccount_NoEmailMatch_ChooseAccountButton = document.getElementById(this.clientId + '_Connect_NewAccount_NoEmailMatch_ChooseAccountButton');
        }
        return this._Connect_NewAccount_NoEmailMatch_ChooseAccountButton;
    },
    
    _Connect_NewAccount_NoEmailMatch_ChooseAccountButton: null,
    
    get_connect_NewAccount_NoEmailMatch_ChooseAccountButtonJ: function Js_Controls_Login_View$get_connect_NewAccount_NoEmailMatch_ChooseAccountButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_NewAccount_NoEmailMatch_ChooseAccountButtonJ == null) {
            this._Connect_NewAccount_NoEmailMatch_ChooseAccountButtonJ = $('#' + this.clientId + '_Connect_NewAccount_NoEmailMatch_ChooseAccountButton');
        }
        return this._Connect_NewAccount_NoEmailMatch_ChooseAccountButtonJ;
    },
    
    _Connect_NewAccount_NoEmailMatch_ChooseAccountButtonJ: null,
    
    get_connect_NewAccount_NoEmailMatch_CancelButton: function Js_Controls_Login_View$get_connect_NewAccount_NoEmailMatch_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_NewAccount_NoEmailMatch_CancelButton == null) {
            this._Connect_NewAccount_NoEmailMatch_CancelButton = document.getElementById(this.clientId + '_Connect_NewAccount_NoEmailMatch_CancelButton');
        }
        return this._Connect_NewAccount_NoEmailMatch_CancelButton;
    },
    
    _Connect_NewAccount_NoEmailMatch_CancelButton: null,
    
    get_connect_NewAccount_NoEmailMatch_CancelButtonJ: function Js_Controls_Login_View$get_connect_NewAccount_NoEmailMatch_CancelButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_NewAccount_NoEmailMatch_CancelButtonJ == null) {
            this._Connect_NewAccount_NoEmailMatch_CancelButtonJ = $('#' + this.clientId + '_Connect_NewAccount_NoEmailMatch_CancelButton');
        }
        return this._Connect_NewAccount_NoEmailMatch_CancelButtonJ;
    },
    
    _Connect_NewAccount_NoEmailMatch_CancelButtonJ: null,
    
    get_connect_NewAccount_NoEmailMatch_FacebookLogoutButton: function Js_Controls_Login_View$get_connect_NewAccount_NoEmailMatch_FacebookLogoutButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_NewAccount_NoEmailMatch_FacebookLogoutButton == null) {
            this._Connect_NewAccount_NoEmailMatch_FacebookLogoutButton = document.getElementById(this.clientId + '_Connect_NewAccount_NoEmailMatch_FacebookLogoutButton');
        }
        return this._Connect_NewAccount_NoEmailMatch_FacebookLogoutButton;
    },
    
    _Connect_NewAccount_NoEmailMatch_FacebookLogoutButton: null,
    
    get_connect_NewAccount_NoEmailMatch_FacebookLogoutButtonJ: function Js_Controls_Login_View$get_connect_NewAccount_NoEmailMatch_FacebookLogoutButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_NewAccount_NoEmailMatch_FacebookLogoutButtonJ == null) {
            this._Connect_NewAccount_NoEmailMatch_FacebookLogoutButtonJ = $('#' + this.clientId + '_Connect_NewAccount_NoEmailMatch_FacebookLogoutButton');
        }
        return this._Connect_NewAccount_NoEmailMatch_FacebookLogoutButtonJ;
    },
    
    _Connect_NewAccount_NoEmailMatch_FacebookLogoutButtonJ: null,
    
    get_connect_NewAccount_NoEmailMatch_BackButton: function Js_Controls_Login_View$get_connect_NewAccount_NoEmailMatch_BackButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_NewAccount_NoEmailMatch_BackButton == null) {
            this._Connect_NewAccount_NoEmailMatch_BackButton = document.getElementById(this.clientId + '_Connect_NewAccount_NoEmailMatch_BackButton');
        }
        return this._Connect_NewAccount_NoEmailMatch_BackButton;
    },
    
    _Connect_NewAccount_NoEmailMatch_BackButton: null,
    
    get_connect_NewAccount_NoEmailMatch_BackButtonJ: function Js_Controls_Login_View$get_connect_NewAccount_NoEmailMatch_BackButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_NewAccount_NoEmailMatch_BackButtonJ == null) {
            this._Connect_NewAccount_NoEmailMatch_BackButtonJ = $('#' + this.clientId + '_Connect_NewAccount_NoEmailMatch_BackButton');
        }
        return this._Connect_NewAccount_NoEmailMatch_BackButtonJ;
    },
    
    _Connect_NewAccount_NoEmailMatch_BackButtonJ: null,
    
    get_connect_NewAccount_EmailMatchPanel: function Js_Controls_Login_View$get_connect_NewAccount_EmailMatchPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_NewAccount_EmailMatchPanel == null) {
            this._Connect_NewAccount_EmailMatchPanel = document.getElementById(this.clientId + '_Connect_NewAccount_EmailMatchPanel');
        }
        return this._Connect_NewAccount_EmailMatchPanel;
    },
    
    _Connect_NewAccount_EmailMatchPanel: null,
    
    get_connect_NewAccount_EmailMatchPanelJ: function Js_Controls_Login_View$get_connect_NewAccount_EmailMatchPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_NewAccount_EmailMatchPanelJ == null) {
            this._Connect_NewAccount_EmailMatchPanelJ = $('#' + this.clientId + '_Connect_NewAccount_EmailMatchPanel');
        }
        return this._Connect_NewAccount_EmailMatchPanelJ;
    },
    
    _Connect_NewAccount_EmailMatchPanelJ: null,
    
    get_connect_NewAccount_EmailMatch_UserLink1: function Js_Controls_Login_View$get_connect_NewAccount_EmailMatch_UserLink1() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_NewAccount_EmailMatch_UserLink1 == null) {
            this._Connect_NewAccount_EmailMatch_UserLink1 = document.getElementById(this.clientId + '_Connect_NewAccount_EmailMatch_UserLink1');
        }
        return this._Connect_NewAccount_EmailMatch_UserLink1;
    },
    
    _Connect_NewAccount_EmailMatch_UserLink1: null,
    
    get_connect_NewAccount_EmailMatch_UserLink1J: function Js_Controls_Login_View$get_connect_NewAccount_EmailMatch_UserLink1J() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_NewAccount_EmailMatch_UserLink1J == null) {
            this._Connect_NewAccount_EmailMatch_UserLink1J = $('#' + this.clientId + '_Connect_NewAccount_EmailMatch_UserLink1');
        }
        return this._Connect_NewAccount_EmailMatch_UserLink1J;
    },
    
    _Connect_NewAccount_EmailMatch_UserLink1J: null,
    
    get_connect_NewAccount_EmailMatch_AutoConnectButton: function Js_Controls_Login_View$get_connect_NewAccount_EmailMatch_AutoConnectButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_NewAccount_EmailMatch_AutoConnectButton == null) {
            this._Connect_NewAccount_EmailMatch_AutoConnectButton = document.getElementById(this.clientId + '_Connect_NewAccount_EmailMatch_AutoConnectButton');
        }
        return this._Connect_NewAccount_EmailMatch_AutoConnectButton;
    },
    
    _Connect_NewAccount_EmailMatch_AutoConnectButton: null,
    
    get_connect_NewAccount_EmailMatch_AutoConnectButtonJ: function Js_Controls_Login_View$get_connect_NewAccount_EmailMatch_AutoConnectButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_NewAccount_EmailMatch_AutoConnectButtonJ == null) {
            this._Connect_NewAccount_EmailMatch_AutoConnectButtonJ = $('#' + this.clientId + '_Connect_NewAccount_EmailMatch_AutoConnectButton');
        }
        return this._Connect_NewAccount_EmailMatch_AutoConnectButtonJ;
    },
    
    _Connect_NewAccount_EmailMatch_AutoConnectButtonJ: null,
    
    get_connect_NewAccount_EmailMatch_ChooseAccountButton: function Js_Controls_Login_View$get_connect_NewAccount_EmailMatch_ChooseAccountButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_NewAccount_EmailMatch_ChooseAccountButton == null) {
            this._Connect_NewAccount_EmailMatch_ChooseAccountButton = document.getElementById(this.clientId + '_Connect_NewAccount_EmailMatch_ChooseAccountButton');
        }
        return this._Connect_NewAccount_EmailMatch_ChooseAccountButton;
    },
    
    _Connect_NewAccount_EmailMatch_ChooseAccountButton: null,
    
    get_connect_NewAccount_EmailMatch_ChooseAccountButtonJ: function Js_Controls_Login_View$get_connect_NewAccount_EmailMatch_ChooseAccountButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_NewAccount_EmailMatch_ChooseAccountButtonJ == null) {
            this._Connect_NewAccount_EmailMatch_ChooseAccountButtonJ = $('#' + this.clientId + '_Connect_NewAccount_EmailMatch_ChooseAccountButton');
        }
        return this._Connect_NewAccount_EmailMatch_ChooseAccountButtonJ;
    },
    
    _Connect_NewAccount_EmailMatch_ChooseAccountButtonJ: null,
    
    get_connect_NewAccount_EmailMatch_CancelButton: function Js_Controls_Login_View$get_connect_NewAccount_EmailMatch_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_NewAccount_EmailMatch_CancelButton == null) {
            this._Connect_NewAccount_EmailMatch_CancelButton = document.getElementById(this.clientId + '_Connect_NewAccount_EmailMatch_CancelButton');
        }
        return this._Connect_NewAccount_EmailMatch_CancelButton;
    },
    
    _Connect_NewAccount_EmailMatch_CancelButton: null,
    
    get_connect_NewAccount_EmailMatch_CancelButtonJ: function Js_Controls_Login_View$get_connect_NewAccount_EmailMatch_CancelButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_NewAccount_EmailMatch_CancelButtonJ == null) {
            this._Connect_NewAccount_EmailMatch_CancelButtonJ = $('#' + this.clientId + '_Connect_NewAccount_EmailMatch_CancelButton');
        }
        return this._Connect_NewAccount_EmailMatch_CancelButtonJ;
    },
    
    _Connect_NewAccount_EmailMatch_CancelButtonJ: null,
    
    get_connect_NewAccount_EmailMatch_FacebookLogoutButton: function Js_Controls_Login_View$get_connect_NewAccount_EmailMatch_FacebookLogoutButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_NewAccount_EmailMatch_FacebookLogoutButton == null) {
            this._Connect_NewAccount_EmailMatch_FacebookLogoutButton = document.getElementById(this.clientId + '_Connect_NewAccount_EmailMatch_FacebookLogoutButton');
        }
        return this._Connect_NewAccount_EmailMatch_FacebookLogoutButton;
    },
    
    _Connect_NewAccount_EmailMatch_FacebookLogoutButton: null,
    
    get_connect_NewAccount_EmailMatch_FacebookLogoutButtonJ: function Js_Controls_Login_View$get_connect_NewAccount_EmailMatch_FacebookLogoutButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_NewAccount_EmailMatch_FacebookLogoutButtonJ == null) {
            this._Connect_NewAccount_EmailMatch_FacebookLogoutButtonJ = $('#' + this.clientId + '_Connect_NewAccount_EmailMatch_FacebookLogoutButton');
        }
        return this._Connect_NewAccount_EmailMatch_FacebookLogoutButtonJ;
    },
    
    _Connect_NewAccount_EmailMatch_FacebookLogoutButtonJ: null,
    
    get_connect_NewAccount_EmailMatch_BackButton: function Js_Controls_Login_View$get_connect_NewAccount_EmailMatch_BackButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_NewAccount_EmailMatch_BackButton == null) {
            this._Connect_NewAccount_EmailMatch_BackButton = document.getElementById(this.clientId + '_Connect_NewAccount_EmailMatch_BackButton');
        }
        return this._Connect_NewAccount_EmailMatch_BackButton;
    },
    
    _Connect_NewAccount_EmailMatch_BackButton: null,
    
    get_connect_NewAccount_EmailMatch_BackButtonJ: function Js_Controls_Login_View$get_connect_NewAccount_EmailMatch_BackButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_NewAccount_EmailMatch_BackButtonJ == null) {
            this._Connect_NewAccount_EmailMatch_BackButtonJ = $('#' + this.clientId + '_Connect_NewAccount_EmailMatch_BackButton');
        }
        return this._Connect_NewAccount_EmailMatch_BackButtonJ;
    },
    
    _Connect_NewAccount_EmailMatch_BackButtonJ: null,
    
    get_connect_NewAccount_ChooseAccountPanel: function Js_Controls_Login_View$get_connect_NewAccount_ChooseAccountPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_NewAccount_ChooseAccountPanel == null) {
            this._Connect_NewAccount_ChooseAccountPanel = document.getElementById(this.clientId + '_Connect_NewAccount_ChooseAccountPanel');
        }
        return this._Connect_NewAccount_ChooseAccountPanel;
    },
    
    _Connect_NewAccount_ChooseAccountPanel: null,
    
    get_connect_NewAccount_ChooseAccountPanelJ: function Js_Controls_Login_View$get_connect_NewAccount_ChooseAccountPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_NewAccount_ChooseAccountPanelJ == null) {
            this._Connect_NewAccount_ChooseAccountPanelJ = $('#' + this.clientId + '_Connect_NewAccount_ChooseAccountPanel');
        }
        return this._Connect_NewAccount_ChooseAccountPanelJ;
    },
    
    _Connect_NewAccount_ChooseAccountPanelJ: null,
    
    get_connect_NewAccount_ChooseAccount_UsernameTextbox: function Js_Controls_Login_View$get_connect_NewAccount_ChooseAccount_UsernameTextbox() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_NewAccount_ChooseAccount_UsernameTextbox == null) {
            this._Connect_NewAccount_ChooseAccount_UsernameTextbox = document.getElementById(this.clientId + '_Connect_NewAccount_ChooseAccount_UsernameTextbox');
        }
        return this._Connect_NewAccount_ChooseAccount_UsernameTextbox;
    },
    
    _Connect_NewAccount_ChooseAccount_UsernameTextbox: null,
    
    get_connect_NewAccount_ChooseAccount_UsernameTextboxJ: function Js_Controls_Login_View$get_connect_NewAccount_ChooseAccount_UsernameTextboxJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_NewAccount_ChooseAccount_UsernameTextboxJ == null) {
            this._Connect_NewAccount_ChooseAccount_UsernameTextboxJ = $('#' + this.clientId + '_Connect_NewAccount_ChooseAccount_UsernameTextbox');
        }
        return this._Connect_NewAccount_ChooseAccount_UsernameTextboxJ;
    },
    
    _Connect_NewAccount_ChooseAccount_UsernameTextboxJ: null,
    
    get_connect_NewAccount_ChooseAccount_PasswordTextbox: function Js_Controls_Login_View$get_connect_NewAccount_ChooseAccount_PasswordTextbox() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_NewAccount_ChooseAccount_PasswordTextbox == null) {
            this._Connect_NewAccount_ChooseAccount_PasswordTextbox = document.getElementById(this.clientId + '_Connect_NewAccount_ChooseAccount_PasswordTextbox');
        }
        return this._Connect_NewAccount_ChooseAccount_PasswordTextbox;
    },
    
    _Connect_NewAccount_ChooseAccount_PasswordTextbox: null,
    
    get_connect_NewAccount_ChooseAccount_PasswordTextboxJ: function Js_Controls_Login_View$get_connect_NewAccount_ChooseAccount_PasswordTextboxJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_NewAccount_ChooseAccount_PasswordTextboxJ == null) {
            this._Connect_NewAccount_ChooseAccount_PasswordTextboxJ = $('#' + this.clientId + '_Connect_NewAccount_ChooseAccount_PasswordTextbox');
        }
        return this._Connect_NewAccount_ChooseAccount_PasswordTextboxJ;
    },
    
    _Connect_NewAccount_ChooseAccount_PasswordTextboxJ: null,
    
    get_connect_NewAccount_ChooseAccount_LinkAccountButton: function Js_Controls_Login_View$get_connect_NewAccount_ChooseAccount_LinkAccountButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_NewAccount_ChooseAccount_LinkAccountButton == null) {
            this._Connect_NewAccount_ChooseAccount_LinkAccountButton = document.getElementById(this.clientId + '_Connect_NewAccount_ChooseAccount_LinkAccountButton');
        }
        return this._Connect_NewAccount_ChooseAccount_LinkAccountButton;
    },
    
    _Connect_NewAccount_ChooseAccount_LinkAccountButton: null,
    
    get_connect_NewAccount_ChooseAccount_LinkAccountButtonJ: function Js_Controls_Login_View$get_connect_NewAccount_ChooseAccount_LinkAccountButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_NewAccount_ChooseAccount_LinkAccountButtonJ == null) {
            this._Connect_NewAccount_ChooseAccount_LinkAccountButtonJ = $('#' + this.clientId + '_Connect_NewAccount_ChooseAccount_LinkAccountButton');
        }
        return this._Connect_NewAccount_ChooseAccount_LinkAccountButtonJ;
    },
    
    _Connect_NewAccount_ChooseAccount_LinkAccountButtonJ: null,
    
    get_connect_NewAccount_ChooseAccount_ErrorLabel: function Js_Controls_Login_View$get_connect_NewAccount_ChooseAccount_ErrorLabel() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_NewAccount_ChooseAccount_ErrorLabel == null) {
            this._Connect_NewAccount_ChooseAccount_ErrorLabel = document.getElementById(this.clientId + '_Connect_NewAccount_ChooseAccount_ErrorLabel');
        }
        return this._Connect_NewAccount_ChooseAccount_ErrorLabel;
    },
    
    _Connect_NewAccount_ChooseAccount_ErrorLabel: null,
    
    get_connect_NewAccount_ChooseAccount_ErrorLabelJ: function Js_Controls_Login_View$get_connect_NewAccount_ChooseAccount_ErrorLabelJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_NewAccount_ChooseAccount_ErrorLabelJ == null) {
            this._Connect_NewAccount_ChooseAccount_ErrorLabelJ = $('#' + this.clientId + '_Connect_NewAccount_ChooseAccount_ErrorLabel');
        }
        return this._Connect_NewAccount_ChooseAccount_ErrorLabelJ;
    },
    
    _Connect_NewAccount_ChooseAccount_ErrorLabelJ: null,
    
    get_connect_NewAccount_ChooseAccount_CancelButton: function Js_Controls_Login_View$get_connect_NewAccount_ChooseAccount_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_NewAccount_ChooseAccount_CancelButton == null) {
            this._Connect_NewAccount_ChooseAccount_CancelButton = document.getElementById(this.clientId + '_Connect_NewAccount_ChooseAccount_CancelButton');
        }
        return this._Connect_NewAccount_ChooseAccount_CancelButton;
    },
    
    _Connect_NewAccount_ChooseAccount_CancelButton: null,
    
    get_connect_NewAccount_ChooseAccount_CancelButtonJ: function Js_Controls_Login_View$get_connect_NewAccount_ChooseAccount_CancelButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_NewAccount_ChooseAccount_CancelButtonJ == null) {
            this._Connect_NewAccount_ChooseAccount_CancelButtonJ = $('#' + this.clientId + '_Connect_NewAccount_ChooseAccount_CancelButton');
        }
        return this._Connect_NewAccount_ChooseAccount_CancelButtonJ;
    },
    
    _Connect_NewAccount_ChooseAccount_CancelButtonJ: null,
    
    get_connect_NewAccount_ChooseAccount_FacebookLogoutButton: function Js_Controls_Login_View$get_connect_NewAccount_ChooseAccount_FacebookLogoutButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_NewAccount_ChooseAccount_FacebookLogoutButton == null) {
            this._Connect_NewAccount_ChooseAccount_FacebookLogoutButton = document.getElementById(this.clientId + '_Connect_NewAccount_ChooseAccount_FacebookLogoutButton');
        }
        return this._Connect_NewAccount_ChooseAccount_FacebookLogoutButton;
    },
    
    _Connect_NewAccount_ChooseAccount_FacebookLogoutButton: null,
    
    get_connect_NewAccount_ChooseAccount_FacebookLogoutButtonJ: function Js_Controls_Login_View$get_connect_NewAccount_ChooseAccount_FacebookLogoutButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_NewAccount_ChooseAccount_FacebookLogoutButtonJ == null) {
            this._Connect_NewAccount_ChooseAccount_FacebookLogoutButtonJ = $('#' + this.clientId + '_Connect_NewAccount_ChooseAccount_FacebookLogoutButton');
        }
        return this._Connect_NewAccount_ChooseAccount_FacebookLogoutButtonJ;
    },
    
    _Connect_NewAccount_ChooseAccount_FacebookLogoutButtonJ: null,
    
    get_connect_NewAccount_ChooseAccount_BackButton: function Js_Controls_Login_View$get_connect_NewAccount_ChooseAccount_BackButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_NewAccount_ChooseAccount_BackButton == null) {
            this._Connect_NewAccount_ChooseAccount_BackButton = document.getElementById(this.clientId + '_Connect_NewAccount_ChooseAccount_BackButton');
        }
        return this._Connect_NewAccount_ChooseAccount_BackButton;
    },
    
    _Connect_NewAccount_ChooseAccount_BackButton: null,
    
    get_connect_NewAccount_ChooseAccount_BackButtonJ: function Js_Controls_Login_View$get_connect_NewAccount_ChooseAccount_BackButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_NewAccount_ChooseAccount_BackButtonJ == null) {
            this._Connect_NewAccount_ChooseAccount_BackButtonJ = $('#' + this.clientId + '_Connect_NewAccount_ChooseAccount_BackButton');
        }
        return this._Connect_NewAccount_ChooseAccount_BackButtonJ;
    },
    
    _Connect_NewAccount_ChooseAccount_BackButtonJ: null,
    
    get_connect_NewAccount_ChooseAccount_ForgottonPasswordButton: function Js_Controls_Login_View$get_connect_NewAccount_ChooseAccount_ForgottonPasswordButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_NewAccount_ChooseAccount_ForgottonPasswordButton == null) {
            this._Connect_NewAccount_ChooseAccount_ForgottonPasswordButton = document.getElementById(this.clientId + '_Connect_NewAccount_ChooseAccount_ForgottonPasswordButton');
        }
        return this._Connect_NewAccount_ChooseAccount_ForgottonPasswordButton;
    },
    
    _Connect_NewAccount_ChooseAccount_ForgottonPasswordButton: null,
    
    get_connect_NewAccount_ChooseAccount_ForgottonPasswordButtonJ: function Js_Controls_Login_View$get_connect_NewAccount_ChooseAccount_ForgottonPasswordButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_NewAccount_ChooseAccount_ForgottonPasswordButtonJ == null) {
            this._Connect_NewAccount_ChooseAccount_ForgottonPasswordButtonJ = $('#' + this.clientId + '_Connect_NewAccount_ChooseAccount_ForgottonPasswordButton');
        }
        return this._Connect_NewAccount_ChooseAccount_ForgottonPasswordButtonJ;
    },
    
    _Connect_NewAccount_ChooseAccount_ForgottonPasswordButtonJ: null,
    
    get_connect_NewAccount_ForgotPasswordPanel: function Js_Controls_Login_View$get_connect_NewAccount_ForgotPasswordPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_NewAccount_ForgotPasswordPanel == null) {
            this._Connect_NewAccount_ForgotPasswordPanel = document.getElementById(this.clientId + '_Connect_NewAccount_ForgotPasswordPanel');
        }
        return this._Connect_NewAccount_ForgotPasswordPanel;
    },
    
    _Connect_NewAccount_ForgotPasswordPanel: null,
    
    get_connect_NewAccount_ForgotPasswordPanelJ: function Js_Controls_Login_View$get_connect_NewAccount_ForgotPasswordPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_NewAccount_ForgotPasswordPanelJ == null) {
            this._Connect_NewAccount_ForgotPasswordPanelJ = $('#' + this.clientId + '_Connect_NewAccount_ForgotPasswordPanel');
        }
        return this._Connect_NewAccount_ForgotPasswordPanelJ;
    },
    
    _Connect_NewAccount_ForgotPasswordPanelJ: null,
    
    get_connect_NewAccount_ForgotPassword_UsernameTextbox: function Js_Controls_Login_View$get_connect_NewAccount_ForgotPassword_UsernameTextbox() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_NewAccount_ForgotPassword_UsernameTextbox == null) {
            this._Connect_NewAccount_ForgotPassword_UsernameTextbox = document.getElementById(this.clientId + '_Connect_NewAccount_ForgotPassword_UsernameTextbox');
        }
        return this._Connect_NewAccount_ForgotPassword_UsernameTextbox;
    },
    
    _Connect_NewAccount_ForgotPassword_UsernameTextbox: null,
    
    get_connect_NewAccount_ForgotPassword_UsernameTextboxJ: function Js_Controls_Login_View$get_connect_NewAccount_ForgotPassword_UsernameTextboxJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_NewAccount_ForgotPassword_UsernameTextboxJ == null) {
            this._Connect_NewAccount_ForgotPassword_UsernameTextboxJ = $('#' + this.clientId + '_Connect_NewAccount_ForgotPassword_UsernameTextbox');
        }
        return this._Connect_NewAccount_ForgotPassword_UsernameTextboxJ;
    },
    
    _Connect_NewAccount_ForgotPassword_UsernameTextboxJ: null,
    
    get_connect_NewAccount_ForgotPassword_SendLinkButton: function Js_Controls_Login_View$get_connect_NewAccount_ForgotPassword_SendLinkButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_NewAccount_ForgotPassword_SendLinkButton == null) {
            this._Connect_NewAccount_ForgotPassword_SendLinkButton = document.getElementById(this.clientId + '_Connect_NewAccount_ForgotPassword_SendLinkButton');
        }
        return this._Connect_NewAccount_ForgotPassword_SendLinkButton;
    },
    
    _Connect_NewAccount_ForgotPassword_SendLinkButton: null,
    
    get_connect_NewAccount_ForgotPassword_SendLinkButtonJ: function Js_Controls_Login_View$get_connect_NewAccount_ForgotPassword_SendLinkButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_NewAccount_ForgotPassword_SendLinkButtonJ == null) {
            this._Connect_NewAccount_ForgotPassword_SendLinkButtonJ = $('#' + this.clientId + '_Connect_NewAccount_ForgotPassword_SendLinkButton');
        }
        return this._Connect_NewAccount_ForgotPassword_SendLinkButtonJ;
    },
    
    _Connect_NewAccount_ForgotPassword_SendLinkButtonJ: null,
    
    get_connect_NewAccount_ForgotPassword_MessageLabel: function Js_Controls_Login_View$get_connect_NewAccount_ForgotPassword_MessageLabel() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_NewAccount_ForgotPassword_MessageLabel == null) {
            this._Connect_NewAccount_ForgotPassword_MessageLabel = document.getElementById(this.clientId + '_Connect_NewAccount_ForgotPassword_MessageLabel');
        }
        return this._Connect_NewAccount_ForgotPassword_MessageLabel;
    },
    
    _Connect_NewAccount_ForgotPassword_MessageLabel: null,
    
    get_connect_NewAccount_ForgotPassword_MessageLabelJ: function Js_Controls_Login_View$get_connect_NewAccount_ForgotPassword_MessageLabelJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_NewAccount_ForgotPassword_MessageLabelJ == null) {
            this._Connect_NewAccount_ForgotPassword_MessageLabelJ = $('#' + this.clientId + '_Connect_NewAccount_ForgotPassword_MessageLabel');
        }
        return this._Connect_NewAccount_ForgotPassword_MessageLabelJ;
    },
    
    _Connect_NewAccount_ForgotPassword_MessageLabelJ: null,
    
    get_connect_NewAccount_ForgotPassword_ErrorLabel: function Js_Controls_Login_View$get_connect_NewAccount_ForgotPassword_ErrorLabel() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_NewAccount_ForgotPassword_ErrorLabel == null) {
            this._Connect_NewAccount_ForgotPassword_ErrorLabel = document.getElementById(this.clientId + '_Connect_NewAccount_ForgotPassword_ErrorLabel');
        }
        return this._Connect_NewAccount_ForgotPassword_ErrorLabel;
    },
    
    _Connect_NewAccount_ForgotPassword_ErrorLabel: null,
    
    get_connect_NewAccount_ForgotPassword_ErrorLabelJ: function Js_Controls_Login_View$get_connect_NewAccount_ForgotPassword_ErrorLabelJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_NewAccount_ForgotPassword_ErrorLabelJ == null) {
            this._Connect_NewAccount_ForgotPassword_ErrorLabelJ = $('#' + this.clientId + '_Connect_NewAccount_ForgotPassword_ErrorLabel');
        }
        return this._Connect_NewAccount_ForgotPassword_ErrorLabelJ;
    },
    
    _Connect_NewAccount_ForgotPassword_ErrorLabelJ: null,
    
    get_connect_NewAccount_ForgotPassword_CancelButton: function Js_Controls_Login_View$get_connect_NewAccount_ForgotPassword_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_NewAccount_ForgotPassword_CancelButton == null) {
            this._Connect_NewAccount_ForgotPassword_CancelButton = document.getElementById(this.clientId + '_Connect_NewAccount_ForgotPassword_CancelButton');
        }
        return this._Connect_NewAccount_ForgotPassword_CancelButton;
    },
    
    _Connect_NewAccount_ForgotPassword_CancelButton: null,
    
    get_connect_NewAccount_ForgotPassword_CancelButtonJ: function Js_Controls_Login_View$get_connect_NewAccount_ForgotPassword_CancelButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_NewAccount_ForgotPassword_CancelButtonJ == null) {
            this._Connect_NewAccount_ForgotPassword_CancelButtonJ = $('#' + this.clientId + '_Connect_NewAccount_ForgotPassword_CancelButton');
        }
        return this._Connect_NewAccount_ForgotPassword_CancelButtonJ;
    },
    
    _Connect_NewAccount_ForgotPassword_CancelButtonJ: null,
    
    get_connect_NewAccount_ForgotPassword_FacebookLogoutButton: function Js_Controls_Login_View$get_connect_NewAccount_ForgotPassword_FacebookLogoutButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_NewAccount_ForgotPassword_FacebookLogoutButton == null) {
            this._Connect_NewAccount_ForgotPassword_FacebookLogoutButton = document.getElementById(this.clientId + '_Connect_NewAccount_ForgotPassword_FacebookLogoutButton');
        }
        return this._Connect_NewAccount_ForgotPassword_FacebookLogoutButton;
    },
    
    _Connect_NewAccount_ForgotPassword_FacebookLogoutButton: null,
    
    get_connect_NewAccount_ForgotPassword_FacebookLogoutButtonJ: function Js_Controls_Login_View$get_connect_NewAccount_ForgotPassword_FacebookLogoutButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_NewAccount_ForgotPassword_FacebookLogoutButtonJ == null) {
            this._Connect_NewAccount_ForgotPassword_FacebookLogoutButtonJ = $('#' + this.clientId + '_Connect_NewAccount_ForgotPassword_FacebookLogoutButton');
        }
        return this._Connect_NewAccount_ForgotPassword_FacebookLogoutButtonJ;
    },
    
    _Connect_NewAccount_ForgotPassword_FacebookLogoutButtonJ: null,
    
    get_connect_NewAccount_ForgotPassword_BackButton: function Js_Controls_Login_View$get_connect_NewAccount_ForgotPassword_BackButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_NewAccount_ForgotPassword_BackButton == null) {
            this._Connect_NewAccount_ForgotPassword_BackButton = document.getElementById(this.clientId + '_Connect_NewAccount_ForgotPassword_BackButton');
        }
        return this._Connect_NewAccount_ForgotPassword_BackButton;
    },
    
    _Connect_NewAccount_ForgotPassword_BackButton: null,
    
    get_connect_NewAccount_ForgotPassword_BackButtonJ: function Js_Controls_Login_View$get_connect_NewAccount_ForgotPassword_BackButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_NewAccount_ForgotPassword_BackButtonJ == null) {
            this._Connect_NewAccount_ForgotPassword_BackButtonJ = $('#' + this.clientId + '_Connect_NewAccount_ForgotPassword_BackButton');
        }
        return this._Connect_NewAccount_ForgotPassword_BackButtonJ;
    },
    
    _Connect_NewAccount_ForgotPassword_BackButtonJ: null,
    
    get_connect_LoggedInPanel: function Js_Controls_Login_View$get_connect_LoggedInPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedInPanel == null) {
            this._Connect_LoggedInPanel = document.getElementById(this.clientId + '_Connect_LoggedInPanel');
        }
        return this._Connect_LoggedInPanel;
    },
    
    _Connect_LoggedInPanel: null,
    
    get_connect_LoggedInPanelJ: function Js_Controls_Login_View$get_connect_LoggedInPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedInPanelJ == null) {
            this._Connect_LoggedInPanelJ = $('#' + this.clientId + '_Connect_LoggedInPanel');
        }
        return this._Connect_LoggedInPanelJ;
    },
    
    _Connect_LoggedInPanelJ: null,
    
    get_connect_LoggedIn_LoggedInUsrLink: function Js_Controls_Login_View$get_connect_LoggedIn_LoggedInUsrLink() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedIn_LoggedInUsrLink == null) {
            this._Connect_LoggedIn_LoggedInUsrLink = document.getElementById(this.clientId + '_Connect_LoggedIn_LoggedInUsrLink');
        }
        return this._Connect_LoggedIn_LoggedInUsrLink;
    },
    
    _Connect_LoggedIn_LoggedInUsrLink: null,
    
    get_connect_LoggedIn_LoggedInUsrLinkJ: function Js_Controls_Login_View$get_connect_LoggedIn_LoggedInUsrLinkJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedIn_LoggedInUsrLinkJ == null) {
            this._Connect_LoggedIn_LoggedInUsrLinkJ = $('#' + this.clientId + '_Connect_LoggedIn_LoggedInUsrLink');
        }
        return this._Connect_LoggedIn_LoggedInUsrLinkJ;
    },
    
    _Connect_LoggedIn_LoggedInUsrLinkJ: null,
    
    get_connect_LoggedIn_CloseButton: function Js_Controls_Login_View$get_connect_LoggedIn_CloseButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedIn_CloseButton == null) {
            this._Connect_LoggedIn_CloseButton = document.getElementById(this.clientId + '_Connect_LoggedIn_CloseButton');
        }
        return this._Connect_LoggedIn_CloseButton;
    },
    
    _Connect_LoggedIn_CloseButton: null,
    
    get_connect_LoggedIn_CloseButtonJ: function Js_Controls_Login_View$get_connect_LoggedIn_CloseButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedIn_CloseButtonJ == null) {
            this._Connect_LoggedIn_CloseButtonJ = $('#' + this.clientId + '_Connect_LoggedIn_CloseButton');
        }
        return this._Connect_LoggedIn_CloseButtonJ;
    },
    
    _Connect_LoggedIn_CloseButtonJ: null,
    
    get_connect_LoggedIn_LogoutButton: function Js_Controls_Login_View$get_connect_LoggedIn_LogoutButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedIn_LogoutButton == null) {
            this._Connect_LoggedIn_LogoutButton = document.getElementById(this.clientId + '_Connect_LoggedIn_LogoutButton');
        }
        return this._Connect_LoggedIn_LogoutButton;
    },
    
    _Connect_LoggedIn_LogoutButton: null,
    
    get_connect_LoggedIn_LogoutButtonJ: function Js_Controls_Login_View$get_connect_LoggedIn_LogoutButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedIn_LogoutButtonJ == null) {
            this._Connect_LoggedIn_LogoutButtonJ = $('#' + this.clientId + '_Connect_LoggedIn_LogoutButton');
        }
        return this._Connect_LoggedIn_LogoutButtonJ;
    },
    
    _Connect_LoggedIn_LogoutButtonJ: null,
    
    get_connect_LoggedIn_DisconnectLinkOuter: function Js_Controls_Login_View$get_connect_LoggedIn_DisconnectLinkOuter() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedIn_DisconnectLinkOuter == null) {
            this._Connect_LoggedIn_DisconnectLinkOuter = document.getElementById(this.clientId + '_Connect_LoggedIn_DisconnectLinkOuter');
        }
        return this._Connect_LoggedIn_DisconnectLinkOuter;
    },
    
    _Connect_LoggedIn_DisconnectLinkOuter: null,
    
    get_connect_LoggedIn_DisconnectLinkOuterJ: function Js_Controls_Login_View$get_connect_LoggedIn_DisconnectLinkOuterJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedIn_DisconnectLinkOuterJ == null) {
            this._Connect_LoggedIn_DisconnectLinkOuterJ = $('#' + this.clientId + '_Connect_LoggedIn_DisconnectLinkOuter');
        }
        return this._Connect_LoggedIn_DisconnectLinkOuterJ;
    },
    
    _Connect_LoggedIn_DisconnectLinkOuterJ: null,
    
    get_connect_LoggedIn_DisconnectButtonShowLink: function Js_Controls_Login_View$get_connect_LoggedIn_DisconnectButtonShowLink() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedIn_DisconnectButtonShowLink == null) {
            this._Connect_LoggedIn_DisconnectButtonShowLink = document.getElementById(this.clientId + '_Connect_LoggedIn_DisconnectButtonShowLink');
        }
        return this._Connect_LoggedIn_DisconnectButtonShowLink;
    },
    
    _Connect_LoggedIn_DisconnectButtonShowLink: null,
    
    get_connect_LoggedIn_DisconnectButtonShowLinkJ: function Js_Controls_Login_View$get_connect_LoggedIn_DisconnectButtonShowLinkJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedIn_DisconnectButtonShowLinkJ == null) {
            this._Connect_LoggedIn_DisconnectButtonShowLinkJ = $('#' + this.clientId + '_Connect_LoggedIn_DisconnectButtonShowLink');
        }
        return this._Connect_LoggedIn_DisconnectButtonShowLinkJ;
    },
    
    _Connect_LoggedIn_DisconnectButtonShowLinkJ: null,
    
    get_connect_LoggedIn_DisconnectButtonOuter: function Js_Controls_Login_View$get_connect_LoggedIn_DisconnectButtonOuter() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedIn_DisconnectButtonOuter == null) {
            this._Connect_LoggedIn_DisconnectButtonOuter = document.getElementById(this.clientId + '_Connect_LoggedIn_DisconnectButtonOuter');
        }
        return this._Connect_LoggedIn_DisconnectButtonOuter;
    },
    
    _Connect_LoggedIn_DisconnectButtonOuter: null,
    
    get_connect_LoggedIn_DisconnectButtonOuterJ: function Js_Controls_Login_View$get_connect_LoggedIn_DisconnectButtonOuterJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedIn_DisconnectButtonOuterJ == null) {
            this._Connect_LoggedIn_DisconnectButtonOuterJ = $('#' + this.clientId + '_Connect_LoggedIn_DisconnectButtonOuter');
        }
        return this._Connect_LoggedIn_DisconnectButtonOuterJ;
    },
    
    _Connect_LoggedIn_DisconnectButtonOuterJ: null,
    
    get_connect_LoggedIn_DisconnectButton: function Js_Controls_Login_View$get_connect_LoggedIn_DisconnectButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedIn_DisconnectButton == null) {
            this._Connect_LoggedIn_DisconnectButton = document.getElementById(this.clientId + '_Connect_LoggedIn_DisconnectButton');
        }
        return this._Connect_LoggedIn_DisconnectButton;
    },
    
    _Connect_LoggedIn_DisconnectButton: null,
    
    get_connect_LoggedIn_DisconnectButtonJ: function Js_Controls_Login_View$get_connect_LoggedIn_DisconnectButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedIn_DisconnectButtonJ == null) {
            this._Connect_LoggedIn_DisconnectButtonJ = $('#' + this.clientId + '_Connect_LoggedIn_DisconnectButton');
        }
        return this._Connect_LoggedIn_DisconnectButtonJ;
    },
    
    _Connect_LoggedIn_DisconnectButtonJ: null,
    
    get_connect_LoggedIn_CancelButton: function Js_Controls_Login_View$get_connect_LoggedIn_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LoggedIn_CancelButton == null) {
            this._Connect_LoggedIn_CancelButton = document.getElementById(this.clientId + '_Connect_LoggedIn_CancelButton');
        }
        return this._Connect_LoggedIn_CancelButton;
    },
    
    _Connect_LoggedIn_CancelButton: null,
    
    get_connect_LoggedIn_CancelButtonJ: function Js_Controls_Login_View$get_connect_LoggedIn_CancelButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LoggedIn_CancelButtonJ == null) {
            this._Connect_LoggedIn_CancelButtonJ = $('#' + this.clientId + '_Connect_LoggedIn_CancelButton');
        }
        return this._Connect_LoggedIn_CancelButtonJ;
    },
    
    _Connect_LoggedIn_CancelButtonJ: null,
    
    get_connect_AutoLoginMismatchPanel: function Js_Controls_Login_View$get_connect_AutoLoginMismatchPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_AutoLoginMismatchPanel == null) {
            this._Connect_AutoLoginMismatchPanel = document.getElementById(this.clientId + '_Connect_AutoLoginMismatchPanel');
        }
        return this._Connect_AutoLoginMismatchPanel;
    },
    
    _Connect_AutoLoginMismatchPanel: null,
    
    get_connect_AutoLoginMismatchPanelJ: function Js_Controls_Login_View$get_connect_AutoLoginMismatchPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_AutoLoginMismatchPanelJ == null) {
            this._Connect_AutoLoginMismatchPanelJ = $('#' + this.clientId + '_Connect_AutoLoginMismatchPanel');
        }
        return this._Connect_AutoLoginMismatchPanelJ;
    },
    
    _Connect_AutoLoginMismatchPanelJ: null,
    
    get_connect_AutoLoginMismatch_AutoLoginUsrLink: function Js_Controls_Login_View$get_connect_AutoLoginMismatch_AutoLoginUsrLink() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_AutoLoginMismatch_AutoLoginUsrLink == null) {
            this._Connect_AutoLoginMismatch_AutoLoginUsrLink = document.getElementById(this.clientId + '_Connect_AutoLoginMismatch_AutoLoginUsrLink');
        }
        return this._Connect_AutoLoginMismatch_AutoLoginUsrLink;
    },
    
    _Connect_AutoLoginMismatch_AutoLoginUsrLink: null,
    
    get_connect_AutoLoginMismatch_AutoLoginUsrLinkJ: function Js_Controls_Login_View$get_connect_AutoLoginMismatch_AutoLoginUsrLinkJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_AutoLoginMismatch_AutoLoginUsrLinkJ == null) {
            this._Connect_AutoLoginMismatch_AutoLoginUsrLinkJ = $('#' + this.clientId + '_Connect_AutoLoginMismatch_AutoLoginUsrLink');
        }
        return this._Connect_AutoLoginMismatch_AutoLoginUsrLinkJ;
    },
    
    _Connect_AutoLoginMismatch_AutoLoginUsrLinkJ: null,
    
    get_connect_AutoLoginMismatch_RetryButton: function Js_Controls_Login_View$get_connect_AutoLoginMismatch_RetryButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_AutoLoginMismatch_RetryButton == null) {
            this._Connect_AutoLoginMismatch_RetryButton = document.getElementById(this.clientId + '_Connect_AutoLoginMismatch_RetryButton');
        }
        return this._Connect_AutoLoginMismatch_RetryButton;
    },
    
    _Connect_AutoLoginMismatch_RetryButton: null,
    
    get_connect_AutoLoginMismatch_RetryButtonJ: function Js_Controls_Login_View$get_connect_AutoLoginMismatch_RetryButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_AutoLoginMismatch_RetryButtonJ == null) {
            this._Connect_AutoLoginMismatch_RetryButtonJ = $('#' + this.clientId + '_Connect_AutoLoginMismatch_RetryButton');
        }
        return this._Connect_AutoLoginMismatch_RetryButtonJ;
    },
    
    _Connect_AutoLoginMismatch_RetryButtonJ: null,
    
    get_connect_AutoLoginMismatch_ContinueButton: function Js_Controls_Login_View$get_connect_AutoLoginMismatch_ContinueButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_AutoLoginMismatch_ContinueButton == null) {
            this._Connect_AutoLoginMismatch_ContinueButton = document.getElementById(this.clientId + '_Connect_AutoLoginMismatch_ContinueButton');
        }
        return this._Connect_AutoLoginMismatch_ContinueButton;
    },
    
    _Connect_AutoLoginMismatch_ContinueButton: null,
    
    get_connect_AutoLoginMismatch_ContinueButtonJ: function Js_Controls_Login_View$get_connect_AutoLoginMismatch_ContinueButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_AutoLoginMismatch_ContinueButtonJ == null) {
            this._Connect_AutoLoginMismatch_ContinueButtonJ = $('#' + this.clientId + '_Connect_AutoLoginMismatch_ContinueButton');
        }
        return this._Connect_AutoLoginMismatch_ContinueButtonJ;
    },
    
    _Connect_AutoLoginMismatch_ContinueButtonJ: null,
    
    get_connect_AutoLoginMismatch_SwitchAccountsPara: function Js_Controls_Login_View$get_connect_AutoLoginMismatch_SwitchAccountsPara() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_AutoLoginMismatch_SwitchAccountsPara == null) {
            this._Connect_AutoLoginMismatch_SwitchAccountsPara = document.getElementById(this.clientId + '_Connect_AutoLoginMismatch_SwitchAccountsPara');
        }
        return this._Connect_AutoLoginMismatch_SwitchAccountsPara;
    },
    
    _Connect_AutoLoginMismatch_SwitchAccountsPara: null,
    
    get_connect_AutoLoginMismatch_SwitchAccountsParaJ: function Js_Controls_Login_View$get_connect_AutoLoginMismatch_SwitchAccountsParaJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_AutoLoginMismatch_SwitchAccountsParaJ == null) {
            this._Connect_AutoLoginMismatch_SwitchAccountsParaJ = $('#' + this.clientId + '_Connect_AutoLoginMismatch_SwitchAccountsPara');
        }
        return this._Connect_AutoLoginMismatch_SwitchAccountsParaJ;
    },
    
    _Connect_AutoLoginMismatch_SwitchAccountsParaJ: null,
    
    get_connect_AutoLoginMismatch_AutoLoginUsrLink2: function Js_Controls_Login_View$get_connect_AutoLoginMismatch_AutoLoginUsrLink2() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_AutoLoginMismatch_AutoLoginUsrLink2 == null) {
            this._Connect_AutoLoginMismatch_AutoLoginUsrLink2 = document.getElementById(this.clientId + '_Connect_AutoLoginMismatch_AutoLoginUsrLink2');
        }
        return this._Connect_AutoLoginMismatch_AutoLoginUsrLink2;
    },
    
    _Connect_AutoLoginMismatch_AutoLoginUsrLink2: null,
    
    get_connect_AutoLoginMismatch_AutoLoginUsrLink2J: function Js_Controls_Login_View$get_connect_AutoLoginMismatch_AutoLoginUsrLink2J() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_AutoLoginMismatch_AutoLoginUsrLink2J == null) {
            this._Connect_AutoLoginMismatch_AutoLoginUsrLink2J = $('#' + this.clientId + '_Connect_AutoLoginMismatch_AutoLoginUsrLink2');
        }
        return this._Connect_AutoLoginMismatch_AutoLoginUsrLink2J;
    },
    
    _Connect_AutoLoginMismatch_AutoLoginUsrLink2J: null,
    
    get_connect_AutoLoginMismatch_SwitchAccountsShowLink: function Js_Controls_Login_View$get_connect_AutoLoginMismatch_SwitchAccountsShowLink() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_AutoLoginMismatch_SwitchAccountsShowLink == null) {
            this._Connect_AutoLoginMismatch_SwitchAccountsShowLink = document.getElementById(this.clientId + '_Connect_AutoLoginMismatch_SwitchAccountsShowLink');
        }
        return this._Connect_AutoLoginMismatch_SwitchAccountsShowLink;
    },
    
    _Connect_AutoLoginMismatch_SwitchAccountsShowLink: null,
    
    get_connect_AutoLoginMismatch_SwitchAccountsShowLinkJ: function Js_Controls_Login_View$get_connect_AutoLoginMismatch_SwitchAccountsShowLinkJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_AutoLoginMismatch_SwitchAccountsShowLinkJ == null) {
            this._Connect_AutoLoginMismatch_SwitchAccountsShowLinkJ = $('#' + this.clientId + '_Connect_AutoLoginMismatch_SwitchAccountsShowLink');
        }
        return this._Connect_AutoLoginMismatch_SwitchAccountsShowLinkJ;
    },
    
    _Connect_AutoLoginMismatch_SwitchAccountsShowLinkJ: null,
    
    get_connect_AutoLoginMismatch_SwitchAccountsOuter: function Js_Controls_Login_View$get_connect_AutoLoginMismatch_SwitchAccountsOuter() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_AutoLoginMismatch_SwitchAccountsOuter == null) {
            this._Connect_AutoLoginMismatch_SwitchAccountsOuter = document.getElementById(this.clientId + '_Connect_AutoLoginMismatch_SwitchAccountsOuter');
        }
        return this._Connect_AutoLoginMismatch_SwitchAccountsOuter;
    },
    
    _Connect_AutoLoginMismatch_SwitchAccountsOuter: null,
    
    get_connect_AutoLoginMismatch_SwitchAccountsOuterJ: function Js_Controls_Login_View$get_connect_AutoLoginMismatch_SwitchAccountsOuterJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_AutoLoginMismatch_SwitchAccountsOuterJ == null) {
            this._Connect_AutoLoginMismatch_SwitchAccountsOuterJ = $('#' + this.clientId + '_Connect_AutoLoginMismatch_SwitchAccountsOuter');
        }
        return this._Connect_AutoLoginMismatch_SwitchAccountsOuterJ;
    },
    
    _Connect_AutoLoginMismatch_SwitchAccountsOuterJ: null,
    
    get_connect_AutoLoginMismatch_SwitchButton: function Js_Controls_Login_View$get_connect_AutoLoginMismatch_SwitchButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_AutoLoginMismatch_SwitchButton == null) {
            this._Connect_AutoLoginMismatch_SwitchButton = document.getElementById(this.clientId + '_Connect_AutoLoginMismatch_SwitchButton');
        }
        return this._Connect_AutoLoginMismatch_SwitchButton;
    },
    
    _Connect_AutoLoginMismatch_SwitchButton: null,
    
    get_connect_AutoLoginMismatch_SwitchButtonJ: function Js_Controls_Login_View$get_connect_AutoLoginMismatch_SwitchButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_AutoLoginMismatch_SwitchButtonJ == null) {
            this._Connect_AutoLoginMismatch_SwitchButtonJ = $('#' + this.clientId + '_Connect_AutoLoginMismatch_SwitchButton');
        }
        return this._Connect_AutoLoginMismatch_SwitchButtonJ;
    },
    
    _Connect_AutoLoginMismatch_SwitchButtonJ: null,
    
    get_connect_AutoLoginMismatch_CancelButton: function Js_Controls_Login_View$get_connect_AutoLoginMismatch_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_AutoLoginMismatch_CancelButton == null) {
            this._Connect_AutoLoginMismatch_CancelButton = document.getElementById(this.clientId + '_Connect_AutoLoginMismatch_CancelButton');
        }
        return this._Connect_AutoLoginMismatch_CancelButton;
    },
    
    _Connect_AutoLoginMismatch_CancelButton: null,
    
    get_connect_AutoLoginMismatch_CancelButtonJ: function Js_Controls_Login_View$get_connect_AutoLoginMismatch_CancelButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_AutoLoginMismatch_CancelButtonJ == null) {
            this._Connect_AutoLoginMismatch_CancelButtonJ = $('#' + this.clientId + '_Connect_AutoLoginMismatch_CancelButton');
        }
        return this._Connect_AutoLoginMismatch_CancelButtonJ;
    },
    
    _Connect_AutoLoginMismatch_CancelButtonJ: null,
    
    get_connect_CreatePasswordPanel: function Js_Controls_Login_View$get_connect_CreatePasswordPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_CreatePasswordPanel == null) {
            this._Connect_CreatePasswordPanel = document.getElementById(this.clientId + '_Connect_CreatePasswordPanel');
        }
        return this._Connect_CreatePasswordPanel;
    },
    
    _Connect_CreatePasswordPanel: null,
    
    get_connect_CreatePasswordPanelJ: function Js_Controls_Login_View$get_connect_CreatePasswordPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_CreatePasswordPanelJ == null) {
            this._Connect_CreatePasswordPanelJ = $('#' + this.clientId + '_Connect_CreatePasswordPanel');
        }
        return this._Connect_CreatePasswordPanelJ;
    },
    
    _Connect_CreatePasswordPanelJ: null,
    
    get_connect_CreatePassword_Password1Textbox: function Js_Controls_Login_View$get_connect_CreatePassword_Password1Textbox() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_CreatePassword_Password1Textbox == null) {
            this._Connect_CreatePassword_Password1Textbox = document.getElementById(this.clientId + '_Connect_CreatePassword_Password1Textbox');
        }
        return this._Connect_CreatePassword_Password1Textbox;
    },
    
    _Connect_CreatePassword_Password1Textbox: null,
    
    get_connect_CreatePassword_Password1TextboxJ: function Js_Controls_Login_View$get_connect_CreatePassword_Password1TextboxJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_CreatePassword_Password1TextboxJ == null) {
            this._Connect_CreatePassword_Password1TextboxJ = $('#' + this.clientId + '_Connect_CreatePassword_Password1Textbox');
        }
        return this._Connect_CreatePassword_Password1TextboxJ;
    },
    
    _Connect_CreatePassword_Password1TextboxJ: null,
    
    get_connect_CreatePassword_Password2Textbox: function Js_Controls_Login_View$get_connect_CreatePassword_Password2Textbox() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_CreatePassword_Password2Textbox == null) {
            this._Connect_CreatePassword_Password2Textbox = document.getElementById(this.clientId + '_Connect_CreatePassword_Password2Textbox');
        }
        return this._Connect_CreatePassword_Password2Textbox;
    },
    
    _Connect_CreatePassword_Password2Textbox: null,
    
    get_connect_CreatePassword_Password2TextboxJ: function Js_Controls_Login_View$get_connect_CreatePassword_Password2TextboxJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_CreatePassword_Password2TextboxJ == null) {
            this._Connect_CreatePassword_Password2TextboxJ = $('#' + this.clientId + '_Connect_CreatePassword_Password2Textbox');
        }
        return this._Connect_CreatePassword_Password2TextboxJ;
    },
    
    _Connect_CreatePassword_Password2TextboxJ: null,
    
    get_connect_CreatePassword_DisconnectButton: function Js_Controls_Login_View$get_connect_CreatePassword_DisconnectButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_CreatePassword_DisconnectButton == null) {
            this._Connect_CreatePassword_DisconnectButton = document.getElementById(this.clientId + '_Connect_CreatePassword_DisconnectButton');
        }
        return this._Connect_CreatePassword_DisconnectButton;
    },
    
    _Connect_CreatePassword_DisconnectButton: null,
    
    get_connect_CreatePassword_DisconnectButtonJ: function Js_Controls_Login_View$get_connect_CreatePassword_DisconnectButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_CreatePassword_DisconnectButtonJ == null) {
            this._Connect_CreatePassword_DisconnectButtonJ = $('#' + this.clientId + '_Connect_CreatePassword_DisconnectButton');
        }
        return this._Connect_CreatePassword_DisconnectButtonJ;
    },
    
    _Connect_CreatePassword_DisconnectButtonJ: null,
    
    get_connect_CreatePassword_ErrorSpan: function Js_Controls_Login_View$get_connect_CreatePassword_ErrorSpan() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_CreatePassword_ErrorSpan == null) {
            this._Connect_CreatePassword_ErrorSpan = document.getElementById(this.clientId + '_Connect_CreatePassword_ErrorSpan');
        }
        return this._Connect_CreatePassword_ErrorSpan;
    },
    
    _Connect_CreatePassword_ErrorSpan: null,
    
    get_connect_CreatePassword_ErrorSpanJ: function Js_Controls_Login_View$get_connect_CreatePassword_ErrorSpanJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_CreatePassword_ErrorSpanJ == null) {
            this._Connect_CreatePassword_ErrorSpanJ = $('#' + this.clientId + '_Connect_CreatePassword_ErrorSpan');
        }
        return this._Connect_CreatePassword_ErrorSpanJ;
    },
    
    _Connect_CreatePassword_ErrorSpanJ: null,
    
    get_connect_CreatePassword_CancelButton: function Js_Controls_Login_View$get_connect_CreatePassword_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_CreatePassword_CancelButton == null) {
            this._Connect_CreatePassword_CancelButton = document.getElementById(this.clientId + '_Connect_CreatePassword_CancelButton');
        }
        return this._Connect_CreatePassword_CancelButton;
    },
    
    _Connect_CreatePassword_CancelButton: null,
    
    get_connect_CreatePassword_CancelButtonJ: function Js_Controls_Login_View$get_connect_CreatePassword_CancelButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_CreatePassword_CancelButtonJ == null) {
            this._Connect_CreatePassword_CancelButtonJ = $('#' + this.clientId + '_Connect_CreatePassword_CancelButton');
        }
        return this._Connect_CreatePassword_CancelButtonJ;
    },
    
    _Connect_CreatePassword_CancelButtonJ: null,
    
    get_connect_CreatePassword_BackButton: function Js_Controls_Login_View$get_connect_CreatePassword_BackButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_CreatePassword_BackButton == null) {
            this._Connect_CreatePassword_BackButton = document.getElementById(this.clientId + '_Connect_CreatePassword_BackButton');
        }
        return this._Connect_CreatePassword_BackButton;
    },
    
    _Connect_CreatePassword_BackButton: null,
    
    get_connect_CreatePassword_BackButtonJ: function Js_Controls_Login_View$get_connect_CreatePassword_BackButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_CreatePassword_BackButtonJ == null) {
            this._Connect_CreatePassword_BackButtonJ = $('#' + this.clientId + '_Connect_CreatePassword_BackButton');
        }
        return this._Connect_CreatePassword_BackButtonJ;
    },
    
    _Connect_CreatePassword_BackButtonJ: null,
    
    get_connect_LikeButtonPanel: function Js_Controls_Login_View$get_connect_LikeButtonPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LikeButtonPanel == null) {
            this._Connect_LikeButtonPanel = document.getElementById(this.clientId + '_Connect_LikeButtonPanel');
        }
        return this._Connect_LikeButtonPanel;
    },
    
    _Connect_LikeButtonPanel: null,
    
    get_connect_LikeButtonPanelJ: function Js_Controls_Login_View$get_connect_LikeButtonPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LikeButtonPanelJ == null) {
            this._Connect_LikeButtonPanelJ = $('#' + this.clientId + '_Connect_LikeButtonPanel');
        }
        return this._Connect_LikeButtonPanelJ;
    },
    
    _Connect_LikeButtonPanelJ: null,
    
    get_connect_LikeButton_CancelButton: function Js_Controls_Login_View$get_connect_LikeButton_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_LikeButton_CancelButton == null) {
            this._Connect_LikeButton_CancelButton = document.getElementById(this.clientId + '_Connect_LikeButton_CancelButton');
        }
        return this._Connect_LikeButton_CancelButton;
    },
    
    _Connect_LikeButton_CancelButton: null,
    
    get_connect_LikeButton_CancelButtonJ: function Js_Controls_Login_View$get_connect_LikeButton_CancelButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_LikeButton_CancelButtonJ == null) {
            this._Connect_LikeButton_CancelButtonJ = $('#' + this.clientId + '_Connect_LikeButton_CancelButton');
        }
        return this._Connect_LikeButton_CancelButtonJ;
    },
    
    _Connect_LikeButton_CancelButtonJ: null,
    
    get_connect_DetailsPanel: function Js_Controls_Login_View$get_connect_DetailsPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_DetailsPanel == null) {
            this._Connect_DetailsPanel = document.getElementById(this.clientId + '_Connect_DetailsPanel');
        }
        return this._Connect_DetailsPanel;
    },
    
    _Connect_DetailsPanel: null,
    
    get_connect_DetailsPanelJ: function Js_Controls_Login_View$get_connect_DetailsPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_DetailsPanelJ == null) {
            this._Connect_DetailsPanelJ = $('#' + this.clientId + '_Connect_DetailsPanel');
        }
        return this._Connect_DetailsPanelJ;
    },
    
    _Connect_DetailsPanelJ: null,
    
    get_connect_Details_MusicDropDown: function Js_Controls_Login_View$get_connect_Details_MusicDropDown() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Details_MusicDropDown == null) {
            this._Connect_Details_MusicDropDown = document.getElementById(this.clientId + '_Connect_Details_MusicDropDown');
        }
        return this._Connect_Details_MusicDropDown;
    },
    
    _Connect_Details_MusicDropDown: null,
    
    get_connect_Details_MusicDropDownJ: function Js_Controls_Login_View$get_connect_Details_MusicDropDownJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_Details_MusicDropDownJ == null) {
            this._Connect_Details_MusicDropDownJ = $('#' + this.clientId + '_Connect_Details_MusicDropDown');
        }
        return this._Connect_Details_MusicDropDownJ;
    },
    
    _Connect_Details_MusicDropDownJ: null,
    
    get_connect_Details_CountryDropDown: function Js_Controls_Login_View$get_connect_Details_CountryDropDown() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Details_CountryDropDown == null) {
            this._Connect_Details_CountryDropDown = document.getElementById(this.clientId + '_Connect_Details_CountryDropDown');
        }
        return this._Connect_Details_CountryDropDown;
    },
    
    _Connect_Details_CountryDropDown: null,
    
    get_connect_Details_CountryDropDownJ: function Js_Controls_Login_View$get_connect_Details_CountryDropDownJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_Details_CountryDropDownJ == null) {
            this._Connect_Details_CountryDropDownJ = $('#' + this.clientId + '_Connect_Details_CountryDropDown');
        }
        return this._Connect_Details_CountryDropDownJ;
    },
    
    _Connect_Details_CountryDropDownJ: null,
    
    get_connect_Details_PlaceDropDown: function Js_Controls_Login_View$get_connect_Details_PlaceDropDown() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Details_PlaceDropDown == null) {
            this._Connect_Details_PlaceDropDown = document.getElementById(this.clientId + '_Connect_Details_PlaceDropDown');
        }
        return this._Connect_Details_PlaceDropDown;
    },
    
    _Connect_Details_PlaceDropDown: null,
    
    get_connect_Details_PlaceDropDownJ: function Js_Controls_Login_View$get_connect_Details_PlaceDropDownJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_Details_PlaceDropDownJ == null) {
            this._Connect_Details_PlaceDropDownJ = $('#' + this.clientId + '_Connect_Details_PlaceDropDown');
        }
        return this._Connect_Details_PlaceDropDownJ;
    },
    
    _Connect_Details_PlaceDropDownJ: null,
    
    get_connect_Details_PlaceDefaultOuterSpan: function Js_Controls_Login_View$get_connect_Details_PlaceDefaultOuterSpan() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Details_PlaceDefaultOuterSpan == null) {
            this._Connect_Details_PlaceDefaultOuterSpan = document.getElementById(this.clientId + '_Connect_Details_PlaceDefaultOuterSpan');
        }
        return this._Connect_Details_PlaceDefaultOuterSpan;
    },
    
    _Connect_Details_PlaceDefaultOuterSpan: null,
    
    get_connect_Details_PlaceDefaultOuterSpanJ: function Js_Controls_Login_View$get_connect_Details_PlaceDefaultOuterSpanJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_Details_PlaceDefaultOuterSpanJ == null) {
            this._Connect_Details_PlaceDefaultOuterSpanJ = $('#' + this.clientId + '_Connect_Details_PlaceDefaultOuterSpan');
        }
        return this._Connect_Details_PlaceDefaultOuterSpanJ;
    },
    
    _Connect_Details_PlaceDefaultOuterSpanJ: null,
    
    get_connect_Details_PlaceDefaultSpan: function Js_Controls_Login_View$get_connect_Details_PlaceDefaultSpan() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Details_PlaceDefaultSpan == null) {
            this._Connect_Details_PlaceDefaultSpan = document.getElementById(this.clientId + '_Connect_Details_PlaceDefaultSpan');
        }
        return this._Connect_Details_PlaceDefaultSpan;
    },
    
    _Connect_Details_PlaceDefaultSpan: null,
    
    get_connect_Details_PlaceDefaultSpanJ: function Js_Controls_Login_View$get_connect_Details_PlaceDefaultSpanJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_Details_PlaceDefaultSpanJ == null) {
            this._Connect_Details_PlaceDefaultSpanJ = $('#' + this.clientId + '_Connect_Details_PlaceDefaultSpan');
        }
        return this._Connect_Details_PlaceDefaultSpanJ;
    },
    
    _Connect_Details_PlaceDefaultSpanJ: null,
    
    get_connect_Details_PlaceChangeLink: function Js_Controls_Login_View$get_connect_Details_PlaceChangeLink() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Details_PlaceChangeLink == null) {
            this._Connect_Details_PlaceChangeLink = document.getElementById(this.clientId + '_Connect_Details_PlaceChangeLink');
        }
        return this._Connect_Details_PlaceChangeLink;
    },
    
    _Connect_Details_PlaceChangeLink: null,
    
    get_connect_Details_PlaceChangeLinkJ: function Js_Controls_Login_View$get_connect_Details_PlaceChangeLinkJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_Details_PlaceChangeLinkJ == null) {
            this._Connect_Details_PlaceChangeLinkJ = $('#' + this.clientId + '_Connect_Details_PlaceChangeLink');
        }
        return this._Connect_Details_PlaceChangeLinkJ;
    },
    
    _Connect_Details_PlaceChangeLinkJ: null,
    
    get_connect_Details_FacebookInfoPanel: function Js_Controls_Login_View$get_connect_Details_FacebookInfoPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Details_FacebookInfoPanel == null) {
            this._Connect_Details_FacebookInfoPanel = document.getElementById(this.clientId + '_Connect_Details_FacebookInfoPanel');
        }
        return this._Connect_Details_FacebookInfoPanel;
    },
    
    _Connect_Details_FacebookInfoPanel: null,
    
    get_connect_Details_FacebookInfoPanelJ: function Js_Controls_Login_View$get_connect_Details_FacebookInfoPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_Details_FacebookInfoPanelJ == null) {
            this._Connect_Details_FacebookInfoPanelJ = $('#' + this.clientId + '_Connect_Details_FacebookInfoPanel');
        }
        return this._Connect_Details_FacebookInfoPanelJ;
    },
    
    _Connect_Details_FacebookInfoPanelJ: null,
    
    get_connect_Details_WeeklyEmailInfoPanel: function Js_Controls_Login_View$get_connect_Details_WeeklyEmailInfoPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Details_WeeklyEmailInfoPanel == null) {
            this._Connect_Details_WeeklyEmailInfoPanel = document.getElementById(this.clientId + '_Connect_Details_WeeklyEmailInfoPanel');
        }
        return this._Connect_Details_WeeklyEmailInfoPanel;
    },
    
    _Connect_Details_WeeklyEmailInfoPanel: null,
    
    get_connect_Details_WeeklyEmailInfoPanelJ: function Js_Controls_Login_View$get_connect_Details_WeeklyEmailInfoPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_Details_WeeklyEmailInfoPanelJ == null) {
            this._Connect_Details_WeeklyEmailInfoPanelJ = $('#' + this.clientId + '_Connect_Details_WeeklyEmailInfoPanel');
        }
        return this._Connect_Details_WeeklyEmailInfoPanelJ;
    },
    
    _Connect_Details_WeeklyEmailInfoPanelJ: null,
    
    get_connect_Details_PartyInvitesInfoPanel: function Js_Controls_Login_View$get_connect_Details_PartyInvitesInfoPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Details_PartyInvitesInfoPanel == null) {
            this._Connect_Details_PartyInvitesInfoPanel = document.getElementById(this.clientId + '_Connect_Details_PartyInvitesInfoPanel');
        }
        return this._Connect_Details_PartyInvitesInfoPanel;
    },
    
    _Connect_Details_PartyInvitesInfoPanel: null,
    
    get_connect_Details_PartyInvitesInfoPanelJ: function Js_Controls_Login_View$get_connect_Details_PartyInvitesInfoPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_Details_PartyInvitesInfoPanelJ == null) {
            this._Connect_Details_PartyInvitesInfoPanelJ = $('#' + this.clientId + '_Connect_Details_PartyInvitesInfoPanel');
        }
        return this._Connect_Details_PartyInvitesInfoPanelJ;
    },
    
    _Connect_Details_PartyInvitesInfoPanelJ: null,
    
    get_connect_Details_FacebookCheck: function Js_Controls_Login_View$get_connect_Details_FacebookCheck() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Details_FacebookCheck == null) {
            this._Connect_Details_FacebookCheck = document.getElementById(this.clientId + '_Connect_Details_FacebookCheck');
        }
        return this._Connect_Details_FacebookCheck;
    },
    
    _Connect_Details_FacebookCheck: null,
    
    get_connect_Details_FacebookCheckJ: function Js_Controls_Login_View$get_connect_Details_FacebookCheckJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_Details_FacebookCheckJ == null) {
            this._Connect_Details_FacebookCheckJ = $('#' + this.clientId + '_Connect_Details_FacebookCheck');
        }
        return this._Connect_Details_FacebookCheckJ;
    },
    
    _Connect_Details_FacebookCheckJ: null,
    
    get_connect_Details_FacebookCheckLabel: function Js_Controls_Login_View$get_connect_Details_FacebookCheckLabel() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Details_FacebookCheckLabel == null) {
            this._Connect_Details_FacebookCheckLabel = document.getElementById(this.clientId + '_Connect_Details_FacebookCheckLabel');
        }
        return this._Connect_Details_FacebookCheckLabel;
    },
    
    _Connect_Details_FacebookCheckLabel: null,
    
    get_connect_Details_FacebookCheckLabelJ: function Js_Controls_Login_View$get_connect_Details_FacebookCheckLabelJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_Details_FacebookCheckLabelJ == null) {
            this._Connect_Details_FacebookCheckLabelJ = $('#' + this.clientId + '_Connect_Details_FacebookCheckLabel');
        }
        return this._Connect_Details_FacebookCheckLabelJ;
    },
    
    _Connect_Details_FacebookCheckLabelJ: null,
    
    get_connect_Details_FacebookInfoAnchor: function Js_Controls_Login_View$get_connect_Details_FacebookInfoAnchor() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Details_FacebookInfoAnchor == null) {
            this._Connect_Details_FacebookInfoAnchor = document.getElementById(this.clientId + '_Connect_Details_FacebookInfoAnchor');
        }
        return this._Connect_Details_FacebookInfoAnchor;
    },
    
    _Connect_Details_FacebookInfoAnchor: null,
    
    get_connect_Details_FacebookInfoAnchorJ: function Js_Controls_Login_View$get_connect_Details_FacebookInfoAnchorJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_Details_FacebookInfoAnchorJ == null) {
            this._Connect_Details_FacebookInfoAnchorJ = $('#' + this.clientId + '_Connect_Details_FacebookInfoAnchor');
        }
        return this._Connect_Details_FacebookInfoAnchorJ;
    },
    
    _Connect_Details_FacebookInfoAnchorJ: null,
    
    get_connect_Details_WeeklyEmailCheck: function Js_Controls_Login_View$get_connect_Details_WeeklyEmailCheck() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Details_WeeklyEmailCheck == null) {
            this._Connect_Details_WeeklyEmailCheck = document.getElementById(this.clientId + '_Connect_Details_WeeklyEmailCheck');
        }
        return this._Connect_Details_WeeklyEmailCheck;
    },
    
    _Connect_Details_WeeklyEmailCheck: null,
    
    get_connect_Details_WeeklyEmailCheckJ: function Js_Controls_Login_View$get_connect_Details_WeeklyEmailCheckJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_Details_WeeklyEmailCheckJ == null) {
            this._Connect_Details_WeeklyEmailCheckJ = $('#' + this.clientId + '_Connect_Details_WeeklyEmailCheck');
        }
        return this._Connect_Details_WeeklyEmailCheckJ;
    },
    
    _Connect_Details_WeeklyEmailCheckJ: null,
    
    get_connect_Details_WeeklyEmailCheckLabel: function Js_Controls_Login_View$get_connect_Details_WeeklyEmailCheckLabel() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Details_WeeklyEmailCheckLabel == null) {
            this._Connect_Details_WeeklyEmailCheckLabel = document.getElementById(this.clientId + '_Connect_Details_WeeklyEmailCheckLabel');
        }
        return this._Connect_Details_WeeklyEmailCheckLabel;
    },
    
    _Connect_Details_WeeklyEmailCheckLabel: null,
    
    get_connect_Details_WeeklyEmailCheckLabelJ: function Js_Controls_Login_View$get_connect_Details_WeeklyEmailCheckLabelJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_Details_WeeklyEmailCheckLabelJ == null) {
            this._Connect_Details_WeeklyEmailCheckLabelJ = $('#' + this.clientId + '_Connect_Details_WeeklyEmailCheckLabel');
        }
        return this._Connect_Details_WeeklyEmailCheckLabelJ;
    },
    
    _Connect_Details_WeeklyEmailCheckLabelJ: null,
    
    get_connect_Details_WeeklyEmailInfoAnchor: function Js_Controls_Login_View$get_connect_Details_WeeklyEmailInfoAnchor() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Details_WeeklyEmailInfoAnchor == null) {
            this._Connect_Details_WeeklyEmailInfoAnchor = document.getElementById(this.clientId + '_Connect_Details_WeeklyEmailInfoAnchor');
        }
        return this._Connect_Details_WeeklyEmailInfoAnchor;
    },
    
    _Connect_Details_WeeklyEmailInfoAnchor: null,
    
    get_connect_Details_WeeklyEmailInfoAnchorJ: function Js_Controls_Login_View$get_connect_Details_WeeklyEmailInfoAnchorJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_Details_WeeklyEmailInfoAnchorJ == null) {
            this._Connect_Details_WeeklyEmailInfoAnchorJ = $('#' + this.clientId + '_Connect_Details_WeeklyEmailInfoAnchor');
        }
        return this._Connect_Details_WeeklyEmailInfoAnchorJ;
    },
    
    _Connect_Details_WeeklyEmailInfoAnchorJ: null,
    
    get_connect_Details_PartyInvitesCheck: function Js_Controls_Login_View$get_connect_Details_PartyInvitesCheck() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Details_PartyInvitesCheck == null) {
            this._Connect_Details_PartyInvitesCheck = document.getElementById(this.clientId + '_Connect_Details_PartyInvitesCheck');
        }
        return this._Connect_Details_PartyInvitesCheck;
    },
    
    _Connect_Details_PartyInvitesCheck: null,
    
    get_connect_Details_PartyInvitesCheckJ: function Js_Controls_Login_View$get_connect_Details_PartyInvitesCheckJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_Details_PartyInvitesCheckJ == null) {
            this._Connect_Details_PartyInvitesCheckJ = $('#' + this.clientId + '_Connect_Details_PartyInvitesCheck');
        }
        return this._Connect_Details_PartyInvitesCheckJ;
    },
    
    _Connect_Details_PartyInvitesCheckJ: null,
    
    get_connect_Details_PartyInvitesCheckLabel: function Js_Controls_Login_View$get_connect_Details_PartyInvitesCheckLabel() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Details_PartyInvitesCheckLabel == null) {
            this._Connect_Details_PartyInvitesCheckLabel = document.getElementById(this.clientId + '_Connect_Details_PartyInvitesCheckLabel');
        }
        return this._Connect_Details_PartyInvitesCheckLabel;
    },
    
    _Connect_Details_PartyInvitesCheckLabel: null,
    
    get_connect_Details_PartyInvitesCheckLabelJ: function Js_Controls_Login_View$get_connect_Details_PartyInvitesCheckLabelJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_Details_PartyInvitesCheckLabelJ == null) {
            this._Connect_Details_PartyInvitesCheckLabelJ = $('#' + this.clientId + '_Connect_Details_PartyInvitesCheckLabel');
        }
        return this._Connect_Details_PartyInvitesCheckLabelJ;
    },
    
    _Connect_Details_PartyInvitesCheckLabelJ: null,
    
    get_connect_Details_PartyInvitesInfoAnchor: function Js_Controls_Login_View$get_connect_Details_PartyInvitesInfoAnchor() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Details_PartyInvitesInfoAnchor == null) {
            this._Connect_Details_PartyInvitesInfoAnchor = document.getElementById(this.clientId + '_Connect_Details_PartyInvitesInfoAnchor');
        }
        return this._Connect_Details_PartyInvitesInfoAnchor;
    },
    
    _Connect_Details_PartyInvitesInfoAnchor: null,
    
    get_connect_Details_PartyInvitesInfoAnchorJ: function Js_Controls_Login_View$get_connect_Details_PartyInvitesInfoAnchorJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_Details_PartyInvitesInfoAnchorJ == null) {
            this._Connect_Details_PartyInvitesInfoAnchorJ = $('#' + this.clientId + '_Connect_Details_PartyInvitesInfoAnchor');
        }
        return this._Connect_Details_PartyInvitesInfoAnchorJ;
    },
    
    _Connect_Details_PartyInvitesInfoAnchorJ: null,
    
    get_connect_Details_CancelButton: function Js_Controls_Login_View$get_connect_Details_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Details_CancelButton == null) {
            this._Connect_Details_CancelButton = document.getElementById(this.clientId + '_Connect_Details_CancelButton');
        }
        return this._Connect_Details_CancelButton;
    },
    
    _Connect_Details_CancelButton: null,
    
    get_connect_Details_CancelButtonJ: function Js_Controls_Login_View$get_connect_Details_CancelButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_Details_CancelButtonJ == null) {
            this._Connect_Details_CancelButtonJ = $('#' + this.clientId + '_Connect_Details_CancelButton');
        }
        return this._Connect_Details_CancelButtonJ;
    },
    
    _Connect_Details_CancelButtonJ: null,
    
    get_connect_Details_BackButton: function Js_Controls_Login_View$get_connect_Details_BackButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Details_BackButton == null) {
            this._Connect_Details_BackButton = document.getElementById(this.clientId + '_Connect_Details_BackButton');
        }
        return this._Connect_Details_BackButton;
    },
    
    _Connect_Details_BackButton: null,
    
    get_connect_Details_BackButtonJ: function Js_Controls_Login_View$get_connect_Details_BackButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_Details_BackButtonJ == null) {
            this._Connect_Details_BackButtonJ = $('#' + this.clientId + '_Connect_Details_BackButton');
        }
        return this._Connect_Details_BackButtonJ;
    },
    
    _Connect_Details_BackButtonJ: null,
    
    get_connect_Details_SaveButton: function Js_Controls_Login_View$get_connect_Details_SaveButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Details_SaveButton == null) {
            this._Connect_Details_SaveButton = document.getElementById(this.clientId + '_Connect_Details_SaveButton');
        }
        return this._Connect_Details_SaveButton;
    },
    
    _Connect_Details_SaveButton: null,
    
    get_connect_Details_SaveButtonJ: function Js_Controls_Login_View$get_connect_Details_SaveButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_Details_SaveButtonJ == null) {
            this._Connect_Details_SaveButtonJ = $('#' + this.clientId + '_Connect_Details_SaveButton');
        }
        return this._Connect_Details_SaveButtonJ;
    },
    
    _Connect_Details_SaveButtonJ: null,
    
    get_connect_Details_PlaceErrorSpan: function Js_Controls_Login_View$get_connect_Details_PlaceErrorSpan() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Details_PlaceErrorSpan == null) {
            this._Connect_Details_PlaceErrorSpan = document.getElementById(this.clientId + '_Connect_Details_PlaceErrorSpan');
        }
        return this._Connect_Details_PlaceErrorSpan;
    },
    
    _Connect_Details_PlaceErrorSpan: null,
    
    get_connect_Details_PlaceErrorSpanJ: function Js_Controls_Login_View$get_connect_Details_PlaceErrorSpanJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_Details_PlaceErrorSpanJ == null) {
            this._Connect_Details_PlaceErrorSpanJ = $('#' + this.clientId + '_Connect_Details_PlaceErrorSpan');
        }
        return this._Connect_Details_PlaceErrorSpanJ;
    },
    
    _Connect_Details_PlaceErrorSpanJ: null,
    
    get_connect_CaptchaPanel: function Js_Controls_Login_View$get_connect_CaptchaPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_CaptchaPanel == null) {
            this._Connect_CaptchaPanel = document.getElementById(this.clientId + '_Connect_CaptchaPanel');
        }
        return this._Connect_CaptchaPanel;
    },
    
    _Connect_CaptchaPanel: null,
    
    get_connect_CaptchaPanelJ: function Js_Controls_Login_View$get_connect_CaptchaPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_CaptchaPanelJ == null) {
            this._Connect_CaptchaPanelJ = $('#' + this.clientId + '_Connect_CaptchaPanel');
        }
        return this._Connect_CaptchaPanelJ;
    },
    
    _Connect_CaptchaPanelJ: null,
    
    get_connect_Captcha_Img: function Js_Controls_Login_View$get_connect_Captcha_Img() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Captcha_Img == null) {
            this._Connect_Captcha_Img = document.getElementById(this.clientId + '_Connect_Captcha_Img');
        }
        return this._Connect_Captcha_Img;
    },
    
    _Connect_Captcha_Img: null,
    
    get_connect_Captcha_ImgJ: function Js_Controls_Login_View$get_connect_Captcha_ImgJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_Captcha_ImgJ == null) {
            this._Connect_Captcha_ImgJ = $('#' + this.clientId + '_Connect_Captcha_Img');
        }
        return this._Connect_Captcha_ImgJ;
    },
    
    _Connect_Captcha_ImgJ: null,
    
    get_connect_Captcha_Textbox: function Js_Controls_Login_View$get_connect_Captcha_Textbox() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Captcha_Textbox == null) {
            this._Connect_Captcha_Textbox = document.getElementById(this.clientId + '_Connect_Captcha_Textbox');
        }
        return this._Connect_Captcha_Textbox;
    },
    
    _Connect_Captcha_Textbox: null,
    
    get_connect_Captcha_TextboxJ: function Js_Controls_Login_View$get_connect_Captcha_TextboxJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_Captcha_TextboxJ == null) {
            this._Connect_Captcha_TextboxJ = $('#' + this.clientId + '_Connect_Captcha_Textbox');
        }
        return this._Connect_Captcha_TextboxJ;
    },
    
    _Connect_Captcha_TextboxJ: null,
    
    get_connect_Captcha_SaveButton: function Js_Controls_Login_View$get_connect_Captcha_SaveButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Captcha_SaveButton == null) {
            this._Connect_Captcha_SaveButton = document.getElementById(this.clientId + '_Connect_Captcha_SaveButton');
        }
        return this._Connect_Captcha_SaveButton;
    },
    
    _Connect_Captcha_SaveButton: null,
    
    get_connect_Captcha_SaveButtonJ: function Js_Controls_Login_View$get_connect_Captcha_SaveButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_Captcha_SaveButtonJ == null) {
            this._Connect_Captcha_SaveButtonJ = $('#' + this.clientId + '_Connect_Captcha_SaveButton');
        }
        return this._Connect_Captcha_SaveButtonJ;
    },
    
    _Connect_Captcha_SaveButtonJ: null,
    
    get_connect_Captcha_Error: function Js_Controls_Login_View$get_connect_Captcha_Error() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Captcha_Error == null) {
            this._Connect_Captcha_Error = document.getElementById(this.clientId + '_Connect_Captcha_Error');
        }
        return this._Connect_Captcha_Error;
    },
    
    _Connect_Captcha_Error: null,
    
    get_connect_Captcha_ErrorJ: function Js_Controls_Login_View$get_connect_Captcha_ErrorJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_Captcha_ErrorJ == null) {
            this._Connect_Captcha_ErrorJ = $('#' + this.clientId + '_Connect_Captcha_Error');
        }
        return this._Connect_Captcha_ErrorJ;
    },
    
    _Connect_Captcha_ErrorJ: null,
    
    get_connect_Captcha_BackButton: function Js_Controls_Login_View$get_connect_Captcha_BackButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Captcha_BackButton == null) {
            this._Connect_Captcha_BackButton = document.getElementById(this.clientId + '_Connect_Captcha_BackButton');
        }
        return this._Connect_Captcha_BackButton;
    },
    
    _Connect_Captcha_BackButton: null,
    
    get_connect_Captcha_BackButtonJ: function Js_Controls_Login_View$get_connect_Captcha_BackButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_Captcha_BackButtonJ == null) {
            this._Connect_Captcha_BackButtonJ = $('#' + this.clientId + '_Connect_Captcha_BackButton');
        }
        return this._Connect_Captcha_BackButtonJ;
    },
    
    _Connect_Captcha_BackButtonJ: null,
    
    get_connect_Captcha_CancelButton: function Js_Controls_Login_View$get_connect_Captcha_CancelButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Captcha_CancelButton == null) {
            this._Connect_Captcha_CancelButton = document.getElementById(this.clientId + '_Connect_Captcha_CancelButton');
        }
        return this._Connect_Captcha_CancelButton;
    },
    
    _Connect_Captcha_CancelButton: null,
    
    get_connect_Captcha_CancelButtonJ: function Js_Controls_Login_View$get_connect_Captcha_CancelButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_Captcha_CancelButtonJ == null) {
            this._Connect_Captcha_CancelButtonJ = $('#' + this.clientId + '_Connect_Captcha_CancelButton');
        }
        return this._Connect_Captcha_CancelButtonJ;
    },
    
    _Connect_Captcha_CancelButtonJ: null,
    
    get_connect_DebugPanel: function Js_Controls_Login_View$get_connect_DebugPanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_DebugPanel == null) {
            this._Connect_DebugPanel = document.getElementById(this.clientId + '_Connect_DebugPanel');
        }
        return this._Connect_DebugPanel;
    },
    
    _Connect_DebugPanel: null,
    
    get_connect_DebugPanelJ: function Js_Controls_Login_View$get_connect_DebugPanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_DebugPanelJ == null) {
            this._Connect_DebugPanelJ = $('#' + this.clientId + '_Connect_DebugPanel');
        }
        return this._Connect_DebugPanelJ;
    },
    
    _Connect_DebugPanelJ: null,
    
    get_connect_Debug_Output: function Js_Controls_Login_View$get_connect_Debug_Output() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Debug_Output == null) {
            this._Connect_Debug_Output = document.getElementById(this.clientId + '_Connect_Debug_Output');
        }
        return this._Connect_Debug_Output;
    },
    
    _Connect_Debug_Output: null,
    
    get_connect_Debug_OutputJ: function Js_Controls_Login_View$get_connect_Debug_OutputJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_Debug_OutputJ == null) {
            this._Connect_Debug_OutputJ = $('#' + this.clientId + '_Connect_Debug_Output');
        }
        return this._Connect_Debug_OutputJ;
    },
    
    _Connect_Debug_OutputJ: null,
    
    get_connect_Debug_LogoutButton: function Js_Controls_Login_View$get_connect_Debug_LogoutButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Debug_LogoutButton == null) {
            this._Connect_Debug_LogoutButton = document.getElementById(this.clientId + '_Connect_Debug_LogoutButton');
        }
        return this._Connect_Debug_LogoutButton;
    },
    
    _Connect_Debug_LogoutButton: null,
    
    get_connect_Debug_LogoutButtonJ: function Js_Controls_Login_View$get_connect_Debug_LogoutButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_Debug_LogoutButtonJ == null) {
            this._Connect_Debug_LogoutButtonJ = $('#' + this.clientId + '_Connect_Debug_LogoutButton');
        }
        return this._Connect_Debug_LogoutButtonJ;
    },
    
    _Connect_Debug_LogoutButtonJ: null,
    
    get_connect_Debug_DisconnectButton: function Js_Controls_Login_View$get_connect_Debug_DisconnectButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Debug_DisconnectButton == null) {
            this._Connect_Debug_DisconnectButton = document.getElementById(this.clientId + '_Connect_Debug_DisconnectButton');
        }
        return this._Connect_Debug_DisconnectButton;
    },
    
    _Connect_Debug_DisconnectButton: null,
    
    get_connect_Debug_DisconnectButtonJ: function Js_Controls_Login_View$get_connect_Debug_DisconnectButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_Debug_DisconnectButtonJ == null) {
            this._Connect_Debug_DisconnectButtonJ = $('#' + this.clientId + '_Connect_Debug_DisconnectButton');
        }
        return this._Connect_Debug_DisconnectButtonJ;
    },
    
    _Connect_Debug_DisconnectButtonJ: null,
    
    get_connect_Debug_AuthButton: function Js_Controls_Login_View$get_connect_Debug_AuthButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._Connect_Debug_AuthButton == null) {
            this._Connect_Debug_AuthButton = document.getElementById(this.clientId + '_Connect_Debug_AuthButton');
        }
        return this._Connect_Debug_AuthButton;
    },
    
    _Connect_Debug_AuthButton: null,
    
    get_connect_Debug_AuthButtonJ: function Js_Controls_Login_View$get_connect_Debug_AuthButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._Connect_Debug_AuthButtonJ == null) {
            this._Connect_Debug_AuthButtonJ = $('#' + this.clientId + '_Connect_Debug_AuthButton');
        }
        return this._Connect_Debug_AuthButtonJ;
    },
    
    _Connect_Debug_AuthButtonJ: null,
    
    get_toggleAdminLinkButton: function Js_Controls_Login_View$get_toggleAdminLinkButton() {
        /// <value type="Object" domElement="true"></value>
        if (this._ToggleAdminLinkButton == null) {
            this._ToggleAdminLinkButton = document.getElementById(this.clientId + '_ToggleAdminLinkButton');
        }
        return this._ToggleAdminLinkButton;
    },
    
    _ToggleAdminLinkButton: null,
    
    get_toggleAdminLinkButtonJ: function Js_Controls_Login_View$get_toggleAdminLinkButtonJ() {
        /// <value type="jQueryObject"></value>
        if (this._ToggleAdminLinkButtonJ == null) {
            this._ToggleAdminLinkButtonJ = $('#' + this.clientId + '_ToggleAdminLinkButton');
        }
        return this._ToggleAdminLinkButtonJ;
    },
    
    _ToggleAdminLinkButtonJ: null
}


Js.Controls.Login.Controller.registerClass('Js.Controls.Login.Controller');
Js.Controls.Login.Server.registerClass('Js.Controls.Login.Server');
Js.Controls.Login.View.registerClass('Js.Controls.Login.View');
Js.Controls.Login.Controller.instance = null;
})(jQuery);

//! This script was generated using Script# v0.7.4.0
