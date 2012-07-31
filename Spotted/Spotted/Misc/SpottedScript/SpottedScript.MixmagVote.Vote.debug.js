Type.registerNamespace('SpottedScript.MixmagVote.Vote');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.MixmagVote.Vote.PageImplementation
function FacebookReady() {
    SpottedScript.MixmagVote.Vote.Controller.instance.facebookReady();
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.MixmagVote.Vote.Controller
SpottedScript.MixmagVote.Vote.Controller = function SpottedScript_MixmagVote_Vote_Controller(v) {
    /// <param name="v" type="SpottedScript.MixmagVote.Vote.View">
    /// </param>
    /// <field name="view" type="SpottedScript.MixmagVote.Vote.View">
    /// </field>
    /// <field name="instance" type="SpottedScript.MixmagVote.Vote.Controller" static="true">
    /// </field>
    /// <field name="server" type="SpottedScript.MixmagVote.Vote.Server">
    /// </field>
    /// <field name="_pageIdToLike" type="Number" integer="true">
    /// </field>
    /// <field name="_entryK" type="Number" integer="true">
    /// </field>
    /// <field name="_compK" type="Number" integer="true">
    /// </field>
    /// <field name="_imageUrl" type="String">
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
    SpottedScript.MixmagVote.Vote.Controller.instance = this;
    this.view = v;
    this.server = v.server;
    if (SpottedScript.Misc.get_browserIsIE()) {
        jQuery(document.body).ready(Function.createDelegate(this, this._initialise));
    }
    else {
        this._initialise();
    }
}
SpottedScript.MixmagVote.Vote.Controller.prototype = {
    view: null,
    server: null,
    _pageIdToLike: 0,
    _entryK: 0,
    _compK: 0,
    _imageUrl: null,
    _doneControllerInit: false,
    _initialise: function SpottedScript_MixmagVote_Vote_Controller$_initialise() {
        this._doneControllerInit = true;
        this.facebookReady();
        this._pageIdToLike = Number.parseInvariant(this.view.get_pageIdToLike().value);
        this._entryK = Number.parseInvariant(this.view.get_entryK().value);
        this._compK = Number.parseInvariant(this.view.get_compK().value);
        this._imageUrl = this.view.get_imageUrl().value;
        $addHandler(this.view.get_vote1VoteButton(), 'click', Function.createDelegate(this, this._vote1VoteButtonClick));
        $addHandler(this.view.get_voteConfirm_YesButton(), 'click', Function.createDelegate(this, this._voteConfirmYesButtonClick));
        $addHandler(this.view.get_voteConfirm_NoButton(), 'click', Function.createDelegate(this, this._voteConfirmNoButtonClick));
        if (this._compK === 2) {
            $addHandler(this.view.get_armaniSubmitButton(), 'click', Function.createDelegate(this, this._armaniSaveQuestionClick));
        }
    },
    facebookReady: function SpottedScript_MixmagVote_Vote_Controller$facebookReady() {
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
    _initialiseForm: function SpottedScript_MixmagVote_Vote_Controller$_initialiseForm() {
        this._changePanel(this.view.get_vote1Panel());
    },
    _vote1VoteButtonClick: function SpottedScript_MixmagVote_Vote_Controller$_vote1VoteButtonClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        this.debug('vote1VoteButtonClick');
        e.preventDefault();
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
            }), F.d('scope', 'email,publish_stream'));
        }
    },
    _facebookAccountConfirmationStepDone: false,
    _confirmFacebookAccount: function SpottedScript_MixmagVote_Vote_Controller$_confirmFacebookAccount() {
        this.debug('confirmFacebookAccount');
        if (this._initialFacebookLoggedIn && !this._facebookAccountConfirmationStepDone) {
            var thisAsyncOperation1 = this._registerStartAsync('Loading...');
            FB.api('/me', Function.createDelegate(this, function(meResponse) {
                if (this._registerEndAsync(thisAsyncOperation1)) {
                    return;
                }
                this.view.get_voteConfirm_Link().innerHTML = ImportedUtilities.U.get(meResponse, 'name').toString();
                this.view.get_voteConfirm_Link().href = ImportedUtilities.U.get(meResponse, 'link').toString();
                this.view.get_voteConfirm_Img().src = 'http://graph.facebook.com/' + ImportedUtilities.U.get(meResponse, 'id').toString() + '/picture';
                this._changePanel(this.view.get_voteConfirmPanel());
            }));
        }
        else {
            this._showLikePanelIfNeeded();
        }
    },
    _voteConfirmYesButtonClick: function SpottedScript_MixmagVote_Vote_Controller$_voteConfirmYesButtonClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        this._facebookAccountConfirmationStepDone = true;
        this._showLikePanelIfNeeded();
    },
    _voteConfirmNoButtonClick: function SpottedScript_MixmagVote_Vote_Controller$_voteConfirmNoButtonClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        this._facebookAccountConfirmationStepDone = true;
        var thisAsyncOperation = this._registerStartAsync('Logging out...');
        FB.logout(Function.createDelegate(this, function(logoutResponse) {
            if (this._registerEndAsync(thisAsyncOperation)) {
                return;
            }
            this._changePanel(this.view.get_vote1Panel());
        }));
    },
    _showLikePanelIfNeeded: function SpottedScript_MixmagVote_Vote_Controller$_showLikePanelIfNeeded() {
        this.debug('showLikePanelIfNeeded');
        var thisAsyncOperation1 = this._registerStartAsync('Loading...');
        FB.api(F.d('method', 'fql.query', 'query', 'SELECT type, created_time FROM page_fan WHERE page_id=\"' + this._pageIdToLike.toString() + '\" AND uid=\"' + this._currentFacebookUID + '\"'), Function.createDelegate(this, function(likeFqlResponse) {
            if (this._registerEndAsync(thisAsyncOperation1)) {
                return;
            }
            if (ImportedUtilities.U.exists(likeFqlResponse, '/value/type')) {
                this._voteNow();
            }
            else {
                this._changePanel(this.view.get_voteLikePanel());
            }
        }));
    },
    _edgeCreate: function SpottedScript_MixmagVote_Vote_Controller$_edgeCreate() {
        this._voteNow();
    },
    _voteNow: function SpottedScript_MixmagVote_Vote_Controller$_voteNow() {
        this.debug('voteNow');
        var thisAsyncOperation1 = this._registerStartAsync('Voting...');
        this.server.voteNow(this._entryK, this._compK, this._imageUrl, Function.createDelegate(this, function(response) {
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
                this._changePanel(this.view.get_vote2Panel());
                this.view.get_armaniTextFieldPanel().style.display = (this._compK === 2) ? '' : 'none';
            }
        }));
    },
    _armaniSaveQuestionClick: function SpottedScript_MixmagVote_Vote_Controller$_armaniSaveQuestionClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        var thisAsyncOperation1 = this._registerStartAsync('Saving...');
        this.server.saveQuestion(this._entryK, this._compK, this._imageUrl, this.view.get_armaniTextField().value, Function.createDelegate(this, function(response) {
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
                this._changePanel(this.view.get_vote2Panel());
                this.view.get_armaniSavedLabel().style.display = '';
            }
        }));
    },
    _changePanel: function SpottedScript_MixmagVote_Vote_Controller$_changePanel(panel) {
        /// <param name="panel" type="Object" domElement="true">
        /// </param>
        this.view.get_vote1Panel().style.display = (panel === this.view.get_vote1Panel()) ? '' : 'none';
        this.view.get_voteConfirmPanel().style.display = (panel === this.view.get_voteConfirmPanel()) ? '' : 'none';
        this.view.get_voteLikePanel().style.display = (panel === this.view.get_voteLikePanel()) ? '' : 'none';
        this.view.get_vote2Panel().style.display = (panel === this.view.get_vote2Panel()) ? '' : 'none';
    },
    _initialFacebookLoggedIn: false,
    _currentFacebookLoggedIn: false,
    _currentFacebookConnected: false,
    _currentFacebookUID: '0',
    _initialFacebookUID: '0',
    _currentFacebookAuthResponse: null,
    _updateCurrentFacebookLoginStatus: function SpottedScript_MixmagVote_Vote_Controller$_updateCurrentFacebookLoginStatus(statusResponse) {
        /// <param name="statusResponse" type="Object">
        /// </param>
        this._currentFacebookConnected = ImportedUtilities.U.exists(statusResponse, 'status') && ImportedUtilities.U.get(statusResponse, 'status').toString() === 'connected';
        this._currentFacebookLoggedIn = ImportedUtilities.U.exists(statusResponse, 'status') && ImportedUtilities.U.get(statusResponse, 'status').toString() !== 'unknown';
        this._currentFacebookUID = (this._currentFacebookConnected) ? ImportedUtilities.U.get(statusResponse, 'authResponse/userID').toString() : '0';
        this._currentFacebookAuthResponse = (this._currentFacebookConnected) ? ImportedUtilities.U.get(statusResponse, 'authResponse') : null;
    },
    debug: function SpottedScript_MixmagVote_Vote_Controller$debug(txt) {
        /// <param name="txt" type="String">
        /// </param>
        this.view.get_debugOutput().innerHTML = txt + '\n' + this.view.get_debugOutput().innerHTML;
    },
    _asyncInProgress: false,
    _asyncOperationCounter: 0,
    _registerStartAsync: function SpottedScript_MixmagVote_Vote_Controller$_registerStartAsync(text) {
        /// <param name="text" type="String">
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        return this._registerStartAsyncGeneric(text, true, true);
    },
    _registerStartAsyncGeneric: function SpottedScript_MixmagVote_Vote_Controller$_registerStartAsyncGeneric(text, setTimer, showLoadingLabel) {
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
    _registerEndAsync: function SpottedScript_MixmagVote_Vote_Controller$_registerEndAsync(asyncOperationCounter) {
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
    _showError: function SpottedScript_MixmagVote_Vote_Controller$_showError(message) {
        /// <param name="message" type="String">
        /// </param>
        alert(message);
        this._changePanel(this.view.get_vote1Panel());
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.MixmagVote.Vote.Server
SpottedScript.MixmagVote.Vote.Server = function SpottedScript_MixmagVote_Vote_Server() {
}
SpottedScript.MixmagVote.Vote.Server.prototype = {
    voteNow: function SpottedScript_MixmagVote_Vote_Server$voteNow(entryK, compK, imageUrl, response) {
        /// <param name="entryK" type="Number" integer="true">
        /// </param>
        /// <param name="compK" type="Number" integer="true">
        /// </param>
        /// <param name="imageUrl" type="String">
        /// </param>
        /// <param name="response" type="ScriptSharpLibrary.Response">
        /// </param>
        var paramArr = [ entryK, compK, imageUrl ];
        var req = eval('PageMethods.ClientRequest');
        if (req != null) {
            try {
                req('Spotted.MixmagVote.Vote, Spotted, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 'VoteNow', paramArr, response, response);
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
    saveQuestion: function SpottedScript_MixmagVote_Vote_Server$saveQuestion(entryK, compK, imageUrl, questionString, response) {
        /// <param name="entryK" type="Number" integer="true">
        /// </param>
        /// <param name="compK" type="Number" integer="true">
        /// </param>
        /// <param name="imageUrl" type="String">
        /// </param>
        /// <param name="questionString" type="String">
        /// </param>
        /// <param name="response" type="ScriptSharpLibrary.Response">
        /// </param>
        var paramArr = [ entryK, compK, imageUrl, questionString ];
        var req = eval('PageMethods.ClientRequest');
        if (req != null) {
            try {
                req('Spotted.MixmagVote.Vote, Spotted, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 'SaveQuestion', paramArr, response, response);
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
// SpottedScript.MixmagVote.Vote.View
SpottedScript.MixmagVote.Vote.View = function SpottedScript_MixmagVote_Vote_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    /// <field name="server" type="SpottedScript.MixmagVote.Vote.Server">
    /// </field>
    SpottedScript.MixmagVote.Vote.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
    this.server = new SpottedScript.MixmagVote.Vote.Server();
}
SpottedScript.MixmagVote.Vote.View.prototype = {
    clientId: null,
    server: null,
    get_compK: function SpottedScript_MixmagVote_Vote_View$get_compK() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CompK');
    },
    get_imageUrl: function SpottedScript_MixmagVote_Vote_View$get_imageUrl() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImageUrl');
    },
    get_entryK: function SpottedScript_MixmagVote_Vote_View$get_entryK() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EntryK');
    },
    get_pageIdToLike: function SpottedScript_MixmagVote_Vote_View$get_pageIdToLike() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PageIdToLike');
    },
    get_voteClosedPanel: function SpottedScript_MixmagVote_Vote_View$get_voteClosedPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VoteClosedPanel');
    },
    get_vote1Panel: function SpottedScript_MixmagVote_Vote_View$get_vote1Panel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Vote1Panel');
    },
    get_vote1Img: function SpottedScript_MixmagVote_Vote_View$get_vote1Img() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Vote1Img');
    },
    get_vote1VoteButton: function SpottedScript_MixmagVote_Vote_View$get_vote1VoteButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Vote1VoteButton');
    },
    get_voteConfirmPanel: function SpottedScript_MixmagVote_Vote_View$get_voteConfirmPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VoteConfirmPanel');
    },
    get_voteConfirm_Img: function SpottedScript_MixmagVote_Vote_View$get_voteConfirm_Img() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VoteConfirm_Img');
    },
    get_voteConfirm_Link: function SpottedScript_MixmagVote_Vote_View$get_voteConfirm_Link() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VoteConfirm_Link');
    },
    get_voteConfirm_YesButton: function SpottedScript_MixmagVote_Vote_View$get_voteConfirm_YesButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VoteConfirm_YesButton');
    },
    get_voteConfirm_NoButton: function SpottedScript_MixmagVote_Vote_View$get_voteConfirm_NoButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VoteConfirm_NoButton');
    },
    get_voteLikePanel: function SpottedScript_MixmagVote_Vote_View$get_voteLikePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VoteLikePanel');
    },
    get_vote2Panel: function SpottedScript_MixmagVote_Vote_View$get_vote2Panel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Vote2Panel');
    },
    get_armaniTextFieldPanel: function SpottedScript_MixmagVote_Vote_View$get_armaniTextFieldPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ArmaniTextFieldPanel');
    },
    get_armaniTextField: function SpottedScript_MixmagVote_Vote_View$get_armaniTextField() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ArmaniTextField');
    },
    get_armaniSubmitButton: function SpottedScript_MixmagVote_Vote_View$get_armaniSubmitButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ArmaniSubmitButton');
    },
    get_armaniSavedLabel: function SpottedScript_MixmagVote_Vote_View$get_armaniSavedLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ArmaniSavedLabel');
    },
    get_debugOutput: function SpottedScript_MixmagVote_Vote_View$get_debugOutput() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DebugOutput');
    },
    get_loadingLabel: function SpottedScript_MixmagVote_Vote_View$get_loadingLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LoadingLabel');
    },
    get_genericContainerPage: function SpottedScript_MixmagVote_Vote_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.MixmagVote.Vote.Controller.registerClass('SpottedScript.MixmagVote.Vote.Controller');
SpottedScript.MixmagVote.Vote.Server.registerClass('SpottedScript.MixmagVote.Vote.Server');
SpottedScript.MixmagVote.Vote.View.registerClass('SpottedScript.MixmagVote.Vote.View', SpottedScript.MixmagVoteUserControl.View);
SpottedScript.MixmagVote.Vote.Controller.instance = null;
