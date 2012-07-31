Type.registerNamespace('SpottedScript.MixmagGreatest.Home');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.MixmagGreatest.Home.PageImplementation
function FacebookReady() {
    SpottedScript.MixmagGreatest.Home.Controller.instance.facebookReady();
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.MixmagGreatest.Home.Controller
SpottedScript.MixmagGreatest.Home.Controller = function SpottedScript_MixmagGreatest_Home_Controller(v) {
    /// <param name="v" type="SpottedScript.MixmagGreatest.Home.View">
    /// </param>
    /// <field name="view" type="SpottedScript.MixmagGreatest.Home.View">
    /// </field>
    /// <field name="instance" type="SpottedScript.MixmagGreatest.Home.Controller" static="true">
    /// </field>
    /// <field name="server" type="SpottedScript.MixmagGreatest.Home.Server">
    /// </field>
    /// <field name="_pageIdToLike" type="Number" integer="true">
    /// </field>
    /// <field name="_likedPage" type="Boolean">
    /// </field>
    /// <field name="_facebookSource" type="String">
    /// </field>
    /// <field name="_mixmagGreatestDjK" type="Number" integer="true">
    /// </field>
    /// <field name="_safariKludge" type="Boolean">
    /// </field>
    /// <field name="_doneControllerInit" type="Boolean">
    /// </field>
    /// <field name="_facebookAccountConfirmationStepDone" type="Boolean">
    /// </field>
    /// <field name="_initialFacebookLoggedIn" type="Boolean">
    /// </field>
    /// <field name="_currentFacebookLoggedIn" type="Boolean">
    /// </field>
    /// <field name="_currentFacebookConnected" type="Boolean">
    /// </field>
    /// <field name="_currentFacebookUID" type="String">
    /// </field>
    /// <field name="_initialFacebookUID" type="String">
    /// </field>
    /// <field name="_currentFacebookAuthResponse" type="Object">
    /// </field>
    /// <field name="_asyncInProgress" type="Boolean">
    /// </field>
    /// <field name="_asyncOperationCounter" type="Number" integer="true">
    /// </field>
    /// <field name="_cancelledAsyncOperations" type="Object">
    /// </field>
    this._cancelledAsyncOperations = {};
    SpottedScript.MixmagGreatest.Home.Controller.instance = this;
    this.view = v;
    this.server = v.server;
    if (SpottedScript.Misc.get_browserIsIE()) {
        jQuery(document.body).ready(Function.createDelegate(this, this._initialise));
    }
    else {
        this._initialise();
    }
}
SpottedScript.MixmagGreatest.Home.Controller.prototype = {
    view: null,
    server: null,
    _pageIdToLike: 0,
    _likedPage: false,
    _facebookSource: null,
    _mixmagGreatestDjK: 0,
    _safariKludge: false,
    _doneControllerInit: false,
    _initialise: function SpottedScript_MixmagGreatest_Home_Controller$_initialise() {
        this._pageIdToLike = Number.parseInvariant(this.view.get_pageIdToLike().value);
        this._likedPage = Boolean.parse(this.view.get_likedPage().value);
        this._mixmagGreatestDjK = Number.parseInvariant(this.view.get_mixmagGreatestDjK().value);
        this._facebookSource = this.view.get_facebookSource().value;
        this._safariKludge = Boolean.parse(this.view.get_safariKludge().value);
        $addHandler(this.view.get_voteButton(), 'click', Function.createDelegate(this, this._voteButtonClick));
        $addHandler(this.view.get_confirm_YesButton(), 'click', Function.createDelegate(this, this._confirmYesButtonClick));
        $addHandler(this.view.get_confirm_NoButton(), 'click', Function.createDelegate(this, this._confirmNoButtonClick));
        $addHandler(this.view.get_loggedOutButton(), 'click', Function.createDelegate(this, this._loggedOutButtonClick));
        this._doneControllerInit = true;
        this.facebookReady();
    },
    facebookReady: function SpottedScript_MixmagGreatest_Home_Controller$facebookReady() {
        if (this._doneControllerInit && eval('DoneFbAsyncInit')) {
            FB.Event.subscribe('auth.statusChange', Function.createDelegate(this, function(statusResponse) {
                this._updateCurrentFacebookLoginStatus(statusResponse);
            }));
            FB.getLoginStatus(Function.createDelegate(this, function(statusResponse) {
                this._updateCurrentFacebookLoginStatus(statusResponse);
                this._initialFacebookLoggedIn = this._currentFacebookLoggedIn;
                this._initialFacebookUID = this._currentFacebookUID;
            }));
            FB.Event.subscribe('edge.create', Function.createDelegate(this, function(edgeCreateResponse) {
                this._edgeCreate();
            }));
            this._initialiseForm();
        }
    },
    _initialiseForm: function SpottedScript_MixmagGreatest_Home_Controller$_initialiseForm() {
        if (this._mixmagGreatestDjK === 0) {
            this._changePanel(this.view.get_nominationsPanel());
        }
        else {
            this.view.get_voteButtonPrompt().innerHTML = (this._likedPage) ? 'Step 1 - click the Vote button:' : 'Step 2 - click the Vote button:';
            this._changePanel(this.view.get_votePanel());
        }
    },
    _voteButtonClick: function SpottedScript_MixmagGreatest_Home_Controller$_voteButtonClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        this.debug('ButtonClick');
        e.preventDefault();
        if (this._currentFacebookLoggedIn && this._currentFacebookConnected) {
            this._confirmFacebookAccount();
        }
        else {
            this._doLogin();
        }
    },
    _doLogin: function SpottedScript_MixmagGreatest_Home_Controller$_doLogin() {
        var asyncOperation = this._registerStartAsyncGeneric('Connecting...', false, false);
        FB.login(Function.createDelegate(this, function(loginResponse) {
            if (this._registerEndAsync(asyncOperation)) {
                return;
            }
            if (this._currentFacebookConnected) {
                this._confirmFacebookAccount();
            }
            else {
                this._showError('Looks like Facebook had trouble getting you connected.');
            }
        }), F.d('scope', 'publish_stream'));
    },
    _facebookAccountConfirmationStepDone: false,
    _confirmFacebookAccount: function SpottedScript_MixmagGreatest_Home_Controller$_confirmFacebookAccount() {
        this.debug('confirmFacebookAccount');
        if (this._initialFacebookLoggedIn && !this._facebookAccountConfirmationStepDone) {
            var thisAsyncOperation1 = this._registerStartAsync('Loading...');
            FB.api('/me', Function.createDelegate(this, function(meResponse) {
                if (this._registerEndAsync(thisAsyncOperation1)) {
                    return;
                }
                this.view.get_confirm_Link().innerHTML = ImportedUtilities.U.get(meResponse, 'name').toString();
                this.view.get_confirm_Link().href = ImportedUtilities.U.get(meResponse, 'link').toString();
                this.view.get_confirm_Img().src = 'http://graph.facebook.com/' + ImportedUtilities.U.get(meResponse, 'id').toString() + '/picture';
                this.view.get_voteLikeHolder().style.display = 'none';
                this.view.get_voteButtonHolder().style.display = 'none';
                this.view.get_voteConfirmHolder().style.display = '';
                this.view.get_voteLoggedOutHolder().style.display = 'none';
            }));
        }
        else {
            this._voteNow();
        }
    },
    _confirmYesButtonClick: function SpottedScript_MixmagGreatest_Home_Controller$_confirmYesButtonClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        this._facebookAccountConfirmationStepDone = true;
        this._voteNow();
    },
    _confirmNoButtonClick: function SpottedScript_MixmagGreatest_Home_Controller$_confirmNoButtonClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        this._facebookAccountConfirmationStepDone = true;
        var thisAsyncOperation = this._registerStartAsync('Logging out...');
        FB.logout(Function.createDelegate(this, function(logoutResponse) {
            if (this._registerEndAsync(thisAsyncOperation)) {
                return;
            }
            this.view.get_voteLikeHolder().style.display = 'none';
            this.view.get_voteButtonHolder().style.display = 'none';
            this.view.get_voteConfirmHolder().style.display = 'none';
            this.view.get_voteLoggedOutHolder().style.display = '';
        }));
    },
    _loggedOutButtonClick: function SpottedScript_MixmagGreatest_Home_Controller$_loggedOutButtonClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        this._doLogin();
    },
    _edgeCreate: function SpottedScript_MixmagGreatest_Home_Controller$_edgeCreate() {
        this._likedPage = true;
        this.view.get_nominationsHolder().style.display = '';
        this.view.get_nominationsLikeButtonHolder().style.display = 'none';
        this.view.get_voteLikeHolder().style.display = 'none';
        this.view.get_voteButtonHolder().style.display = '';
        this.view.get_voteConfirmHolder().style.display = 'none';
        this.view.get_voteLoggedOutHolder().style.display = 'none';
    },
    _voteNow: function SpottedScript_MixmagGreatest_Home_Controller$_voteNow() {
        var thisAsyncOperation1 = this._registerStartAsync('Voting...');
        this.server.voteNow(this._mixmagGreatestDjK, this._facebookSource, this.view.get_voteFacebookUpdateCheckbox1().checked, Function.createDelegate(this, function(response) {
            if (this._registerEndAsync(thisAsyncOperation1)) {
                return;
            }
            if (ImportedUtilities.U.isTrue(response, 'Exception')) {
                this._showError('Looks like we had a problem...');
            }
            else if (!ImportedUtilities.U.isTrue(response, 'Done')) {
                this._showError(ImportedUtilities.U.get(response, 'Message').toString());
            }
            else {
                FB.Canvas.scrollTo(0, 0);
                this._changePanel(this.view.get_voteCompletePanel());
            }
        }));
    },
    _changePanel: function SpottedScript_MixmagGreatest_Home_Controller$_changePanel(panel) {
        /// <param name="panel" type="Object" domElement="true">
        /// </param>
        this.view.get_nominationsPanel().style.display = (panel === this.view.get_nominationsPanel()) ? '' : 'none';
        this.view.get_votePanel().style.display = (panel === this.view.get_votePanel()) ? '' : 'none';
        this.view.get_voteCompletePanel().style.display = (panel === this.view.get_voteCompletePanel()) ? '' : 'none';
    },
    _initialFacebookLoggedIn: false,
    _currentFacebookLoggedIn: false,
    _currentFacebookConnected: false,
    _currentFacebookUID: '0',
    _initialFacebookUID: '0',
    _currentFacebookAuthResponse: null,
    _updateCurrentFacebookLoginStatus: function SpottedScript_MixmagGreatest_Home_Controller$_updateCurrentFacebookLoginStatus(statusResponse) {
        /// <param name="statusResponse" type="Object">
        /// </param>
        this._currentFacebookConnected = ImportedUtilities.U.exists(statusResponse, 'status') && ImportedUtilities.U.get(statusResponse, 'status').toString() === 'connected';
        this._currentFacebookLoggedIn = ImportedUtilities.U.exists(statusResponse, 'status') && ImportedUtilities.U.get(statusResponse, 'status').toString() !== 'unknown';
        this._currentFacebookUID = (this._currentFacebookConnected) ? ImportedUtilities.U.get(statusResponse, 'authResponse/userID').toString() : '0';
        this._currentFacebookAuthResponse = (this._currentFacebookConnected) ? ImportedUtilities.U.get(statusResponse, 'authResponse') : null;
        if (this._safariKludge) {
            if (ImportedUtilities.U.exists(statusResponse, 'authResponse')) {
                var thisAsyncOperation1 = this._registerStartAsync('Loading...');
                this.server.setCookie(ImportedUtilities.U.get(statusResponse, 'authResponse/signedRequest').toString(), Function.createDelegate(this, function(response) {
                    if (this._registerEndAsync(thisAsyncOperation1)) {
                        return;
                    }
                    if (ImportedUtilities.U.isTrue(response, 'Exception')) {
                        this._showError('Looks like we had a problem...');
                    }
                    else if (!ImportedUtilities.U.isTrue(response, 'Done')) {
                        this._showError(ImportedUtilities.U.get(response, 'Message').toString());
                    }
                    else {
                    }
                }));
            }
        }
    },
    debug: function SpottedScript_MixmagGreatest_Home_Controller$debug(txt) {
        /// <param name="txt" type="String">
        /// </param>
        this.view.get_debugOutput().innerHTML = txt + '\n' + this.view.get_debugOutput().innerHTML;
    },
    _asyncInProgress: false,
    _asyncOperationCounter: 0,
    _registerStartAsync: function SpottedScript_MixmagGreatest_Home_Controller$_registerStartAsync(text) {
        /// <param name="text" type="String">
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        return this._registerStartAsyncGeneric(text, true, true);
    },
    _registerStartAsyncGeneric: function SpottedScript_MixmagGreatest_Home_Controller$_registerStartAsyncGeneric(text, setTimer, showLoadingLabel) {
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
            if (text.length > 0) {
                this.view.get_loadingLabel().innerHTML = '<p>' + text + '<p>';
            }
            else {
                this.view.get_loadingLabel().innerHTML = '<p>Loading...<p>';
            }
            this.view.get_loadingLabel().style.display = '';
        }
        if (setTimer) {
            window.setTimeout(Function.createDelegate(this, function() {
                if (this._asyncInProgress && thisAsyncOperationCounter === this._asyncOperationCounter) {
                    this.view.get_loadingLabel().innerHTML = '<p>This seems to be taking a long time... <button id=\"LoadingLabel_CancelLink\">Cancel</button></p>';
                    $addHandler(document.getElementById('LoadingLabel_CancelLink'), 'click', Function.createDelegate(this, function(e) {
                        e.preventDefault();
                        this._cancelledAsyncOperations[thisAsyncOperationCounter.toString()] = true;
                        this._asyncInProgress = false;
                        if (this.view.get_loadingLabel() != null) {
                            this.view.get_loadingLabel().style.display = 'none';
                        }
                        this._initialiseForm();
                    }));
                }
            }), 5000);
        }
        return thisAsyncOperationCounter;
    },
    _registerEndAsync: function SpottedScript_MixmagGreatest_Home_Controller$_registerEndAsync(asyncOperationCounter) {
        /// <param name="asyncOperationCounter" type="Number" integer="true">
        /// </param>
        /// <returns type="Boolean"></returns>
        if (this._cancelledAsyncOperations[asyncOperationCounter.toString()] != null && this._cancelledAsyncOperations[asyncOperationCounter.toString()]) {
            return true;
        }
        this._asyncInProgress = false;
        if (this.view.get_loadingLabel() != null) {
            this.view.get_loadingLabel().style.display = 'none';
        }
        return false;
    },
    _showError: function SpottedScript_MixmagGreatest_Home_Controller$_showError(message) {
        /// <param name="message" type="String">
        /// </param>
        alert(message);
        this._initialiseForm();
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.MixmagGreatest.Home.Server
SpottedScript.MixmagGreatest.Home.Server = function SpottedScript_MixmagGreatest_Home_Server() {
}
SpottedScript.MixmagGreatest.Home.Server.prototype = {
    setCookie: function SpottedScript_MixmagGreatest_Home_Server$setCookie(signedRequest, response) {
        /// <param name="signedRequest" type="String">
        /// </param>
        /// <param name="response" type="ScriptSharpLibrary.Response">
        /// </param>
        var paramArr = [ signedRequest ];
        var req = eval('PageMethods.ClientRequest');
        if (req != null) {
            try {
                req('Spotted.MixmagGreatest.Home, Spotted, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 'SetCookie', paramArr, response, response);
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
    voteNow: function SpottedScript_MixmagGreatest_Home_Server$voteNow(mixmagGreatestDjK, facebookSource, facebookMessage, response) {
        /// <param name="mixmagGreatestDjK" type="Number" integer="true">
        /// </param>
        /// <param name="facebookSource" type="String">
        /// </param>
        /// <param name="facebookMessage" type="Boolean">
        /// </param>
        /// <param name="response" type="ScriptSharpLibrary.Response">
        /// </param>
        var paramArr = [ mixmagGreatestDjK, facebookSource, facebookMessage ];
        var req = eval('PageMethods.ClientRequest');
        if (req != null) {
            try {
                req('Spotted.MixmagGreatest.Home, Spotted, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 'VoteNow', paramArr, response, response);
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
// SpottedScript.MixmagGreatest.Home.View
SpottedScript.MixmagGreatest.Home.View = function SpottedScript_MixmagGreatest_Home_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    /// <field name="server" type="SpottedScript.MixmagGreatest.Home.Server">
    /// </field>
    SpottedScript.MixmagGreatest.Home.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
    this.server = new SpottedScript.MixmagGreatest.Home.Server();
}
SpottedScript.MixmagGreatest.Home.View.prototype = {
    clientId: null,
    server: null,
    get_pageIdToLike: function SpottedScript_MixmagGreatest_Home_View$get_pageIdToLike() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PageIdToLike');
    },
    get_likedPage: function SpottedScript_MixmagGreatest_Home_View$get_likedPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LikedPage');
    },
    get_mixmagGreatestDjK: function SpottedScript_MixmagGreatest_Home_View$get_mixmagGreatestDjK() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MixmagGreatestDjK');
    },
    get_facebookSource: function SpottedScript_MixmagGreatest_Home_View$get_facebookSource() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FacebookSource');
    },
    get_signedRequest: function SpottedScript_MixmagGreatest_Home_View$get_signedRequest() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SignedRequest');
    },
    get_doneRefresh: function SpottedScript_MixmagGreatest_Home_View$get_doneRefresh() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DoneRefresh');
    },
    get_safariKludge: function SpottedScript_MixmagGreatest_Home_View$get_safariKludge() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SafariKludge');
    },
    get_testP: function SpottedScript_MixmagGreatest_Home_View$get_testP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TestP');
    },
    get_nominationsPanel: function SpottedScript_MixmagGreatest_Home_View$get_nominationsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NominationsPanel');
    },
    get_nominationsLikeButtonHolder: function SpottedScript_MixmagGreatest_Home_View$get_nominationsLikeButtonHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NominationsLikeButtonHolder');
    },
    get_nominationsHolder: function SpottedScript_MixmagGreatest_Home_View$get_nominationsHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NominationsHolder');
    },
    get_notRunningPanel: function SpottedScript_MixmagGreatest_Home_View$get_notRunningPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NotRunningPanel');
    },
    get_runningPanel: function SpottedScript_MixmagGreatest_Home_View$get_runningPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RunningPanel');
    },
    get_publicDjsHolder: function SpottedScript_MixmagGreatest_Home_View$get_publicDjsHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PublicDjsHolder');
    },
    get_votePanel: function SpottedScript_MixmagGreatest_Home_View$get_votePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VotePanel');
    },
    get_voteImg: function SpottedScript_MixmagGreatest_Home_View$get_voteImg() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VoteImg');
    },
    get_voteName: function SpottedScript_MixmagGreatest_Home_View$get_voteName() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VoteName');
    },
    get_voteDescriptionP: function SpottedScript_MixmagGreatest_Home_View$get_voteDescriptionP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VoteDescriptionP');
    },
    get_voteLikeHolder: function SpottedScript_MixmagGreatest_Home_View$get_voteLikeHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VoteLikeHolder');
    },
    get_voteFollowHolder: function SpottedScript_MixmagGreatest_Home_View$get_voteFollowHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VoteFollowHolder');
    },
    get_voteFollowPrompt: function SpottedScript_MixmagGreatest_Home_View$get_voteFollowPrompt() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VoteFollowPrompt');
    },
    get_voteFollowSkipLink: function SpottedScript_MixmagGreatest_Home_View$get_voteFollowSkipLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VoteFollowSkipLink');
    },
    get_voteTwitterSkipButton: function SpottedScript_MixmagGreatest_Home_View$get_voteTwitterSkipButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VoteTwitterSkipButton');
    },
    get_voteTweetHolder: function SpottedScript_MixmagGreatest_Home_View$get_voteTweetHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VoteTweetHolder');
    },
    get_voteTweetPrompt: function SpottedScript_MixmagGreatest_Home_View$get_voteTweetPrompt() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VoteTweetPrompt');
    },
    get_voteTweetButton: function SpottedScript_MixmagGreatest_Home_View$get_voteTweetButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VoteTweetButton');
    },
    get_voteTweetSkipLink: function SpottedScript_MixmagGreatest_Home_View$get_voteTweetSkipLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VoteTweetSkipLink');
    },
    get_voteButtonHolder: function SpottedScript_MixmagGreatest_Home_View$get_voteButtonHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VoteButtonHolder');
    },
    get_voteButtonPrompt: function SpottedScript_MixmagGreatest_Home_View$get_voteButtonPrompt() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VoteButtonPrompt');
    },
    get_voteButton: function SpottedScript_MixmagGreatest_Home_View$get_voteButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VoteButton');
    },
    get_voteFacebookUpdateCheckbox1: function SpottedScript_MixmagGreatest_Home_View$get_voteFacebookUpdateCheckbox1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VoteFacebookUpdateCheckbox1');
    },
    get_voteConfirmHolder: function SpottedScript_MixmagGreatest_Home_View$get_voteConfirmHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VoteConfirmHolder');
    },
    get_confirm_Img: function SpottedScript_MixmagGreatest_Home_View$get_confirm_Img() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Confirm_Img');
    },
    get_confirm_Link: function SpottedScript_MixmagGreatest_Home_View$get_confirm_Link() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Confirm_Link');
    },
    get_confirm_YesButton: function SpottedScript_MixmagGreatest_Home_View$get_confirm_YesButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Confirm_YesButton');
    },
    get_confirm_NoButton: function SpottedScript_MixmagGreatest_Home_View$get_confirm_NoButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Confirm_NoButton');
    },
    get_voteLoggedOutHolder: function SpottedScript_MixmagGreatest_Home_View$get_voteLoggedOutHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VoteLoggedOutHolder');
    },
    get_loggedOutButton: function SpottedScript_MixmagGreatest_Home_View$get_loggedOutButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LoggedOutButton');
    },
    get_voteVideo1: function SpottedScript_MixmagGreatest_Home_View$get_voteVideo1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VoteVideo1');
    },
    get_voteVideo2: function SpottedScript_MixmagGreatest_Home_View$get_voteVideo2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VoteVideo2');
    },
    get_facebookComments: function SpottedScript_MixmagGreatest_Home_View$get_facebookComments() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FacebookComments');
    },
    get_voteCompletePanel: function SpottedScript_MixmagGreatest_Home_View$get_voteCompletePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VoteCompletePanel');
    },
    get_facebookComments2: function SpottedScript_MixmagGreatest_Home_View$get_facebookComments2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FacebookComments2');
    },
    get_debugOutput: function SpottedScript_MixmagGreatest_Home_View$get_debugOutput() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DebugOutput');
    },
    get_loadingLabel: function SpottedScript_MixmagGreatest_Home_View$get_loadingLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LoadingLabel');
    },
    get_genericContainerPage: function SpottedScript_MixmagGreatest_Home_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.MixmagGreatest.Home.Controller.registerClass('SpottedScript.MixmagGreatest.Home.Controller');
SpottedScript.MixmagGreatest.Home.Server.registerClass('SpottedScript.MixmagGreatest.Home.Server');
SpottedScript.MixmagGreatest.Home.View.registerClass('SpottedScript.MixmagGreatest.Home.View', SpottedScript.MixmagGreatestUserControl.View);
SpottedScript.MixmagGreatest.Home.Controller.instance = null;
