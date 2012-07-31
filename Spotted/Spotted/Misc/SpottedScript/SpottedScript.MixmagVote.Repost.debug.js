Type.registerNamespace('SpottedScript.MixmagVote.Repost');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.MixmagVote.Repost.PageImplementation
function FacebookReady() {
    SpottedScript.MixmagVote.Repost.Controller.instance.facebookReady();
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.MixmagVote.Repost.Controller
SpottedScript.MixmagVote.Repost.Controller = function SpottedScript_MixmagVote_Repost_Controller(v) {
    /// <param name="v" type="SpottedScript.MixmagVote.Repost.View">
    /// </param>
    /// <field name="_entryK" type="Number" integer="true">
    /// </field>
    /// <field name="view" type="SpottedScript.MixmagVote.Repost.View">
    /// </field>
    /// <field name="instance" type="SpottedScript.MixmagVote.Repost.Controller" static="true">
    /// </field>
    /// <field name="server" type="SpottedScript.MixmagVote.Repost.Server">
    /// </field>
    /// <field name="_doneControllerInit" type="Boolean">
    /// </field>
    /// <field name="_asyncInProgress" type="Boolean">
    /// </field>
    /// <field name="_asyncOperationCounter" type="Number" integer="true">
    /// </field>
    /// <field name="_cancelledAsyncOperations" type="Object">
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
    /// <field name="_currentFacebookSession" type="Object">
    /// </field>
    /// <field name="_facebookAccountConfirmationStepDone" type="Boolean">
    /// </field>
    this._cancelledAsyncOperations = {};
    SpottedScript.MixmagVote.Repost.Controller.instance = this;
    this.view = v;
    this.server = v.server;
    if (SpottedScript.Misc.get_browserIsIE()) {
        jQuery(document.body).ready(Function.createDelegate(this, this._initialise));
    }
    else {
        this._initialise();
    }
}
SpottedScript.MixmagVote.Repost.Controller.prototype = {
    _entryK: 0,
    view: null,
    server: null,
    _initialise: function SpottedScript_MixmagVote_Repost_Controller$_initialise() {
        $addHandler(this.view.get_repost1Button(), 'click', Function.createDelegate(this, this.repost1ButtonClick));
        $addHandler(this.view.get_repostConfirm_YesButton(), 'click', Function.createDelegate(this, this._repostConfirmYesButtonClick));
        $addHandler(this.view.get_repostConfirm_NoButton(), 'click', Function.createDelegate(this, this._repostConfirmNoButtonClick));
        this._entryK = Number.parseInvariant(this.view.get_entryK().value);
        this._doneControllerInit = true;
        this.facebookReady();
    },
    facebookReady: function SpottedScript_MixmagVote_Repost_Controller$facebookReady() {
        this.debug('FacebookReady start: DoneControllerInit = ' + this._doneControllerInit.toString() + ', DoneFbAsyncInit = ' + (eval('DoneFbAsyncInit')).toString());
        if (this._doneControllerInit && eval('DoneFbAsyncInit')) {
            this.debug('FacebookReady running');
            FB.Event.subscribe('auth.statusChange', Function.createDelegate(this, function(statusResponse) {
                this._updateCurrentFacebookLoginStatus(statusResponse);
            }));
            FB.getLoginStatus(Function.createDelegate(this, function(statusResponse) {
                this._updateCurrentFacebookLoginStatus(statusResponse);
                this.debug(ImportedUtilities.U.toString(statusResponse));
                this._initialFacebookLoggedIn = this._currentFacebookLoggedIn;
                this._initialFacebookUID = this._currentFacebookUID;
            }));
            this._initialiseForm();
        }
    },
    _doneControllerInit: false,
    _initialiseForm: function SpottedScript_MixmagVote_Repost_Controller$_initialiseForm() {
        this._changePanel(this.view.get_repost1Panel());
    },
    repost1ButtonClick: function SpottedScript_MixmagVote_Repost_Controller$repost1ButtonClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        this._repost1ButtonClickInternal();
    },
    _repost1ButtonClickInternal: function SpottedScript_MixmagVote_Repost_Controller$_repost1ButtonClickInternal() {
        this.debug('entry1ButtonClickInternal: CurrentFacebookLoggedIn = ' + this._currentFacebookLoggedIn.toString() + ', CurrentFacebookConnected = ' + this._currentFacebookConnected.toString());
        if (this._currentFacebookLoggedIn && this._currentFacebookConnected) {
            this._confirmFacebookAccount();
        }
        else {
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
            }), F.d('perms', 'email,publish_stream'));
        }
        FB.Event.subscribe('edge.create', Function.createDelegate(this, function(edgeCreateResponse) {
            this.debug('edge.create');
        }));
    },
    _confirmFacebookAccount: function SpottedScript_MixmagVote_Repost_Controller$_confirmFacebookAccount() {
        this.debug('InitialFacebookLoggedIn = ' + this._initialFacebookLoggedIn.toString());
        this.debug('FacebookAccountConfirmationStepDone = ' + this._facebookAccountConfirmationStepDone.toString());
        if (this._initialFacebookLoggedIn && !this._facebookAccountConfirmationStepDone) {
            var thisAsyncOperation1 = this._registerStartAsync('Loading...');
            FB.api('/me', Function.createDelegate(this, function(meResponse) {
                if (this._registerEndAsync(thisAsyncOperation1)) {
                    return;
                }
                this.view.get_repostConfirm_Link().innerHTML = ImportedUtilities.U.get(meResponse, 'name').toString();
                this.view.get_repostConfirm_Link().href = ImportedUtilities.U.get(meResponse, 'link').toString();
                this.view.get_repostConfirm_Img().src = 'http://graph.facebook.com/' + ImportedUtilities.U.get(meResponse, 'id').toString() + '/picture';
                this._changePanel(this.view.get_repostConfirmPanel());
            }));
        }
        else {
            this._repost();
        }
    },
    _repostConfirmYesButtonClick: function SpottedScript_MixmagVote_Repost_Controller$_repostConfirmYesButtonClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        this._facebookAccountConfirmationStepDone = true;
        this._repost();
    },
    _repostConfirmNoButtonClick: function SpottedScript_MixmagVote_Repost_Controller$_repostConfirmNoButtonClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        this.debug('entryConfirmNoButtonClick');
        this._facebookAccountConfirmationStepDone = true;
        var thisAsyncOperation = this._registerStartAsync('Logging out...');
        FB.logout(Function.createDelegate(this, function(logoutResponse) {
            if (this._registerEndAsync(thisAsyncOperation)) {
                return;
            }
            this._changePanel(this.view.get_repost1Panel());
        }));
    },
    _repost: function SpottedScript_MixmagVote_Repost_Controller$_repost() {
        var thisAsyncOperation1 = this._registerStartAsync('Posting your message...');
        this.server.repostNow(this._entryK, this.view.get_repost1FacebookMessageTextbox().value, Function.createDelegate(this, function(response) {
            if (this._registerEndAsync(thisAsyncOperation1)) {
                return;
            }
            if (ImportedUtilities.U.isTrue(response, 'Exception') || !ImportedUtilities.U.isTrue(response, 'Done')) {
                this._showError('Looks like we had a problem...');
            }
            else {
                this.debug(ImportedUtilities.U.toString(response));
                this._changePanel(this.view.get_repost2Panel());
            }
        }));
    },
    _changePanel: function SpottedScript_MixmagVote_Repost_Controller$_changePanel(panel) {
        /// <param name="panel" type="Object" domElement="true">
        /// </param>
        this.view.get_repost1Panel().style.display = (panel === this.view.get_repost1Panel()) ? '' : 'none';
        this.view.get_repostConfirmPanel().style.display = (panel === this.view.get_repostConfirmPanel()) ? '' : 'none';
        this.view.get_repost2Panel().style.display = (panel === this.view.get_repost2Panel()) ? '' : 'none';
    },
    _showError: function SpottedScript_MixmagVote_Repost_Controller$_showError(message) {
        /// <param name="message" type="String">
        /// </param>
        alert(message);
        this._changePanel(this.view.get_repost1Panel());
    },
    debug: function SpottedScript_MixmagVote_Repost_Controller$debug(txt) {
        /// <param name="txt" type="String">
        /// </param>
        this.view.get_debugOutput().innerHTML = txt + '\n' + this.view.get_debugOutput().innerHTML;
    },
    _asyncInProgress: false,
    _asyncOperationCounter: 0,
    _registerStartAsync: function SpottedScript_MixmagVote_Repost_Controller$_registerStartAsync(text) {
        /// <param name="text" type="String">
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        return this._registerStartAsyncGeneric(text, true, true);
    },
    _registerStartAsyncGeneric: function SpottedScript_MixmagVote_Repost_Controller$_registerStartAsyncGeneric(text, setTimer, showLoadingLabel) {
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
    _registerEndAsync: function SpottedScript_MixmagVote_Repost_Controller$_registerEndAsync(asyncOperationCounter) {
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
    _initialFacebookLoggedIn: false,
    _currentFacebookLoggedIn: false,
    _currentFacebookConnected: false,
    _currentFacebookUID: '0',
    _initialFacebookUID: '0',
    _currentFacebookSession: null,
    _facebookAccountConfirmationStepDone: false,
    _updateCurrentFacebookLoginStatus: function SpottedScript_MixmagVote_Repost_Controller$_updateCurrentFacebookLoginStatus(statusResponse) {
        /// <param name="statusResponse" type="Object">
        /// </param>
        this.debug('START updateCurrentFacebookLoginStatus CurrentFacebookConnected = ' + this._currentFacebookConnected.toString() + ', CurrentFacebookLoggedIn = ' + this._currentFacebookLoggedIn.toString());
        this._currentFacebookConnected = ImportedUtilities.U.exists(statusResponse, 'status') && ImportedUtilities.U.get(statusResponse, 'status').toString() === 'connected';
        this._currentFacebookLoggedIn = ImportedUtilities.U.exists(statusResponse, 'status') && ImportedUtilities.U.get(statusResponse, 'status').toString() !== 'unknown';
        this._currentFacebookUID = (this._currentFacebookConnected) ? ImportedUtilities.U.get(statusResponse, 'session/uid').toString() : '0';
        this._currentFacebookSession = (this._currentFacebookConnected) ? ImportedUtilities.U.get(statusResponse, 'session') : null;
        this.debug('DONE updateCurrentFacebookLoginStatus CurrentFacebookConnected = ' + this._currentFacebookConnected.toString() + ', CurrentFacebookLoggedIn = ' + this._currentFacebookLoggedIn.toString());
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.MixmagVote.Repost.Server
SpottedScript.MixmagVote.Repost.Server = function SpottedScript_MixmagVote_Repost_Server() {
}
SpottedScript.MixmagVote.Repost.Server.prototype = {
    repostNow: function SpottedScript_MixmagVote_Repost_Server$repostNow(entryK, message, response) {
        /// <param name="entryK" type="Number" integer="true">
        /// </param>
        /// <param name="message" type="String">
        /// </param>
        /// <param name="response" type="ScriptSharpLibrary.Response">
        /// </param>
        var paramArr = [ entryK, message ];
        var req = eval('PageMethods.ClientRequest');
        if (req != null) {
            try {
                req('Spotted.MixmagVote.Repost, Spotted, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 'RepostNow', paramArr, response, response);
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
// SpottedScript.MixmagVote.Repost.View
SpottedScript.MixmagVote.Repost.View = function SpottedScript_MixmagVote_Repost_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    /// <field name="server" type="SpottedScript.MixmagVote.Repost.Server">
    /// </field>
    SpottedScript.MixmagVote.Repost.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
    this.server = new SpottedScript.MixmagVote.Repost.Server();
}
SpottedScript.MixmagVote.Repost.View.prototype = {
    clientId: null,
    server: null,
    get_entryK: function SpottedScript_MixmagVote_Repost_View$get_entryK() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EntryK');
    },
    get_repost1Panel: function SpottedScript_MixmagVote_Repost_View$get_repost1Panel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Repost1Panel');
    },
    get_repost1Img: function SpottedScript_MixmagVote_Repost_View$get_repost1Img() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Repost1Img');
    },
    get_repost1FacebookMessageTextbox: function SpottedScript_MixmagVote_Repost_View$get_repost1FacebookMessageTextbox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Repost1FacebookMessageTextbox');
    },
    get_repost1Button: function SpottedScript_MixmagVote_Repost_View$get_repost1Button() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Repost1Button');
    },
    get_repostConfirmPanel: function SpottedScript_MixmagVote_Repost_View$get_repostConfirmPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RepostConfirmPanel');
    },
    get_repostConfirm_Img: function SpottedScript_MixmagVote_Repost_View$get_repostConfirm_Img() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RepostConfirm_Img');
    },
    get_repostConfirm_Link: function SpottedScript_MixmagVote_Repost_View$get_repostConfirm_Link() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RepostConfirm_Link');
    },
    get_repostConfirm_YesButton: function SpottedScript_MixmagVote_Repost_View$get_repostConfirm_YesButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RepostConfirm_YesButton');
    },
    get_repostConfirm_NoButton: function SpottedScript_MixmagVote_Repost_View$get_repostConfirm_NoButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RepostConfirm_NoButton');
    },
    get_repost2Panel: function SpottedScript_MixmagVote_Repost_View$get_repost2Panel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Repost2Panel');
    },
    get_debugOutput: function SpottedScript_MixmagVote_Repost_View$get_debugOutput() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DebugOutput');
    },
    get_loadingLabel: function SpottedScript_MixmagVote_Repost_View$get_loadingLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LoadingLabel');
    },
    get_genericContainerPage: function SpottedScript_MixmagVote_Repost_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.MixmagVote.Repost.Controller.registerClass('SpottedScript.MixmagVote.Repost.Controller');
SpottedScript.MixmagVote.Repost.Server.registerClass('SpottedScript.MixmagVote.Repost.Server');
SpottedScript.MixmagVote.Repost.View.registerClass('SpottedScript.MixmagVote.Repost.View', SpottedScript.MixmagVoteUserControl.View);
SpottedScript.MixmagVote.Repost.Controller.instance = null;
