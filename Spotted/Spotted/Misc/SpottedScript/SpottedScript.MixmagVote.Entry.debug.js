Type.registerNamespace('SpottedScript.MixmagVote.Entry');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.MixmagVote.Entry.PageImplementation
function FacebookReady() {
    SpottedScript.MixmagVote.Entry.Controller.instance.facebookReady();
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.MixmagVote.Entry.Controller
SpottedScript.MixmagVote.Entry.Controller = function SpottedScript_MixmagVote_Entry_Controller(v) {
    /// <param name="v" type="SpottedScript.MixmagVote.Entry.View">
    /// </param>
    /// <field name="_compK" type="Number" integer="true">
    /// </field>
    /// <field name="_imageUrl" type="String">
    /// </field>
    /// <field name="_pageIdToLike" type="Number" integer="true">
    /// </field>
    /// <field name="view" type="SpottedScript.MixmagVote.Entry.View">
    /// </field>
    /// <field name="instance" type="SpottedScript.MixmagVote.Entry.Controller" static="true">
    /// </field>
    /// <field name="server" type="SpottedScript.MixmagVote.Entry.Server">
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
    /// <field name="_currentFacebookAuthResponse" type="Object">
    /// </field>
    /// <field name="_facebookAccountConfirmationStepDone" type="Boolean">
    /// </field>
    this._cancelledAsyncOperations = {};
    SpottedScript.MixmagVote.Entry.Controller.instance = this;
    this.view = v;
    this.server = v.server;
    if (SpottedScript.Misc.get_browserIsIE()) {
        jQuery(document.body).ready(Function.createDelegate(this, this._initialise));
    }
    else {
        this._initialise();
    }
}
SpottedScript.MixmagVote.Entry.Controller.prototype = {
    _compK: 0,
    _imageUrl: null,
    _pageIdToLike: 0,
    view: null,
    server: null,
    _initialise: function SpottedScript_MixmagVote_Entry_Controller$_initialise() {
        $addHandler(this.view.get_entry1Button(), 'click', Function.createDelegate(this, this.entry1ButtonClick));
        $addHandler(this.view.get_entryConfirm_YesButton(), 'click', Function.createDelegate(this, this._entryConfirmYesButtonClick));
        $addHandler(this.view.get_entryConfirm_NoButton(), 'click', Function.createDelegate(this, this._entryConfirmNoButtonClick));
        this._compK = Number.parseInvariant(this.view.get_compK().value);
        this._imageUrl = this.view.get_imageUrl().value;
        this._pageIdToLike = Number.parseInvariant(this.view.get_pageIdToLike().value);
        this._doneControllerInit = true;
        this.facebookReady();
    },
    facebookReady: function SpottedScript_MixmagVote_Entry_Controller$facebookReady() {
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
    _initialiseForm: function SpottedScript_MixmagVote_Entry_Controller$_initialiseForm() {
        this._changePanel(this.view.get_entry1Panel());
    },
    entry1ButtonClick: function SpottedScript_MixmagVote_Entry_Controller$entry1ButtonClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        this._entry1ButtonClickInternal();
    },
    _entry1ButtonClickInternal: function SpottedScript_MixmagVote_Entry_Controller$_entry1ButtonClickInternal() {
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
            }), F.d('scope', 'email,publish_stream'));
        }
        FB.Event.subscribe('edge.create', Function.createDelegate(this, function(edgeCreateResponse) {
            this.debug('edge.create');
        }));
    },
    _confirmFacebookAccount: function SpottedScript_MixmagVote_Entry_Controller$_confirmFacebookAccount() {
        this.debug('InitialFacebookLoggedIn = ' + this._initialFacebookLoggedIn.toString());
        this.debug('FacebookAccountConfirmationStepDone = ' + this._facebookAccountConfirmationStepDone.toString());
        if (this._initialFacebookLoggedIn && !this._facebookAccountConfirmationStepDone) {
            var thisAsyncOperation1 = this._registerStartAsync('Loading...');
            FB.api('/me', Function.createDelegate(this, function(meResponse) {
                if (this._registerEndAsync(thisAsyncOperation1)) {
                    return;
                }
                this.view.get_entryConfirm_Link().innerHTML = ImportedUtilities.U.get(meResponse, 'name').toString();
                this.view.get_entryConfirm_Link().href = ImportedUtilities.U.get(meResponse, 'link').toString();
                this.view.get_entryConfirm_Img().src = 'http://graph.facebook.com/' + ImportedUtilities.U.get(meResponse, 'id').toString() + '/picture';
                this._changePanel(this.view.get_entryConfirmPanel());
            }));
        }
        else {
            this._enterComp();
        }
    },
    _entryConfirmYesButtonClick: function SpottedScript_MixmagVote_Entry_Controller$_entryConfirmYesButtonClick(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        this._facebookAccountConfirmationStepDone = true;
        this._enterComp();
    },
    _entryConfirmNoButtonClick: function SpottedScript_MixmagVote_Entry_Controller$_entryConfirmNoButtonClick(e) {
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
            this._changePanel(this.view.get_entry1Panel());
        }));
    },
    _enterComp: function SpottedScript_MixmagVote_Entry_Controller$_enterComp() {
        var thisAsyncOperation1 = this._registerStartAsync('Entering the competition...');
        this.server.enterComp(this._compK, this._imageUrl, this.view.get_entry1FacebookMessageTextbox().value, this.view.get_entry1DailyEmailCheckbox().checked, Function.createDelegate(this, function(response) {
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
                this.debug(ImportedUtilities.U.toString(response));
                this._entry2Initialise(ImportedUtilities.U.get(response, 'MixmagEntryK'));
            }
        }));
    },
    _entry2Initialise: function SpottedScript_MixmagVote_Entry_Controller$_entry2Initialise(k) {
        /// <param name="k" type="Number" integer="true">
        /// </param>
        var thisAsyncOperation1 = this._registerStartAsync('Loading...');
        FB.api(F.d('method', 'fql.query', 'query', 'SELECT type, created_time FROM page_fan WHERE page_id=\"' + this._pageIdToLike.toString() + '\" AND uid=\"' + this._currentFacebookUID + '\"'), Function.createDelegate(this, function(likeFqlResponse) {
            if (this._registerEndAsync(thisAsyncOperation1)) {
                return;
            }
            this.debug(ImportedUtilities.U.toString(likeFqlResponse));
            if (ImportedUtilities.U.exists(likeFqlResponse, '/value/type')) {
                this.view.get_entry2LikeButtonHolder().style.display = 'none';
            }
            this._changePanel(this.view.get_entry2Panel());
            this.view.get_entry2LinkTextbox().value = 'http://mixmag-vote.com/' + k.toString();
        }));
    },
    _changePanel: function SpottedScript_MixmagVote_Entry_Controller$_changePanel(panel) {
        /// <param name="panel" type="Object" domElement="true">
        /// </param>
        this.view.get_entry1Panel().style.display = (panel === this.view.get_entry1Panel()) ? '' : 'none';
        this.view.get_entryConfirmPanel().style.display = (panel === this.view.get_entryConfirmPanel()) ? '' : 'none';
        this.view.get_entry2Panel().style.display = (panel === this.view.get_entry2Panel()) ? '' : 'none';
    },
    _showError: function SpottedScript_MixmagVote_Entry_Controller$_showError(message) {
        /// <param name="message" type="String">
        /// </param>
        alert(message);
        this._changePanel(this.view.get_entry1Panel());
    },
    debug: function SpottedScript_MixmagVote_Entry_Controller$debug(txt) {
        /// <param name="txt" type="String">
        /// </param>
        this.view.get_debugOutput().innerHTML = txt + '\n' + this.view.get_debugOutput().innerHTML;
    },
    _asyncInProgress: false,
    _asyncOperationCounter: 0,
    _registerStartAsync: function SpottedScript_MixmagVote_Entry_Controller$_registerStartAsync(text) {
        /// <param name="text" type="String">
        /// </param>
        /// <returns type="Number" integer="true"></returns>
        return this._registerStartAsyncGeneric(text, true, true);
    },
    _registerStartAsyncGeneric: function SpottedScript_MixmagVote_Entry_Controller$_registerStartAsyncGeneric(text, setTimer, showLoadingLabel) {
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
    _registerEndAsync: function SpottedScript_MixmagVote_Entry_Controller$_registerEndAsync(asyncOperationCounter) {
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
    _currentFacebookAuthResponse: null,
    _facebookAccountConfirmationStepDone: false,
    _updateCurrentFacebookLoginStatus: function SpottedScript_MixmagVote_Entry_Controller$_updateCurrentFacebookLoginStatus(statusResponse) {
        /// <param name="statusResponse" type="Object">
        /// </param>
        this.debug('START updateCurrentFacebookLoginStatus CurrentFacebookConnected = ' + this._currentFacebookConnected.toString() + ', CurrentFacebookLoggedIn = ' + this._currentFacebookLoggedIn.toString());
        this._currentFacebookConnected = ImportedUtilities.U.exists(statusResponse, 'status') && ImportedUtilities.U.get(statusResponse, 'status').toString() === 'connected';
        this._currentFacebookLoggedIn = ImportedUtilities.U.exists(statusResponse, 'status') && ImportedUtilities.U.get(statusResponse, 'status').toString() !== 'unknown';
        this._currentFacebookUID = (this._currentFacebookConnected) ? ImportedUtilities.U.get(statusResponse, 'authResponse/userID').toString() : '0';
        this._currentFacebookAuthResponse = (this._currentFacebookConnected) ? ImportedUtilities.U.get(statusResponse, 'authResponse') : null;
        this.debug('DONE updateCurrentFacebookLoginStatus CurrentFacebookConnected = ' + this._currentFacebookConnected.toString() + ', CurrentFacebookLoggedIn = ' + this._currentFacebookLoggedIn.toString());
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.MixmagVote.Entry.Server
SpottedScript.MixmagVote.Entry.Server = function SpottedScript_MixmagVote_Entry_Server() {
}
SpottedScript.MixmagVote.Entry.Server.prototype = {
    enterComp: function SpottedScript_MixmagVote_Entry_Server$enterComp(compK, imageUrl, facebookMessage, sendEmails, response) {
        /// <param name="compK" type="Number" integer="true">
        /// </param>
        /// <param name="imageUrl" type="String">
        /// </param>
        /// <param name="facebookMessage" type="String">
        /// </param>
        /// <param name="sendEmails" type="Boolean">
        /// </param>
        /// <param name="response" type="ScriptSharpLibrary.Response">
        /// </param>
        var paramArr = [ compK, imageUrl, facebookMessage, sendEmails ];
        var req = eval('PageMethods.ClientRequest');
        if (req != null) {
            try {
                req('Spotted.MixmagVote.Entry, Spotted, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null', 'EnterComp', paramArr, response, response);
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
// SpottedScript.MixmagVote.Entry.View
SpottedScript.MixmagVote.Entry.View = function SpottedScript_MixmagVote_Entry_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    /// <field name="server" type="SpottedScript.MixmagVote.Entry.Server">
    /// </field>
    SpottedScript.MixmagVote.Entry.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
    this.server = new SpottedScript.MixmagVote.Entry.Server();
}
SpottedScript.MixmagVote.Entry.View.prototype = {
    clientId: null,
    server: null,
    get_loadingLabel: function SpottedScript_MixmagVote_Entry_View$get_loadingLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LoadingLabel');
    },
    get_imageUrl: function SpottedScript_MixmagVote_Entry_View$get_imageUrl() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ImageUrl');
    },
    get_pageIdToLike: function SpottedScript_MixmagVote_Entry_View$get_pageIdToLike() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PageIdToLike');
    },
    get_compK: function SpottedScript_MixmagVote_Entry_View$get_compK() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CompK');
    },
    get_entryClosedPanel: function SpottedScript_MixmagVote_Entry_View$get_entryClosedPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EntryClosedPanel');
    },
    get_entry1Panel: function SpottedScript_MixmagVote_Entry_View$get_entry1Panel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Entry1Panel');
    },
    get_entry1Img: function SpottedScript_MixmagVote_Entry_View$get_entry1Img() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Entry1Img');
    },
    get_entry1FacebookMessageTextbox: function SpottedScript_MixmagVote_Entry_View$get_entry1FacebookMessageTextbox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Entry1FacebookMessageTextbox');
    },
    get_entry1DailyEmailCheckboxPara: function SpottedScript_MixmagVote_Entry_View$get_entry1DailyEmailCheckboxPara() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Entry1DailyEmailCheckboxPara');
    },
    get_entry1DailyEmailCheckbox: function SpottedScript_MixmagVote_Entry_View$get_entry1DailyEmailCheckbox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Entry1DailyEmailCheckbox');
    },
    get_entry1Button: function SpottedScript_MixmagVote_Entry_View$get_entry1Button() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Entry1Button');
    },
    get_entryConfirmPanel: function SpottedScript_MixmagVote_Entry_View$get_entryConfirmPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EntryConfirmPanel');
    },
    get_entryConfirm_Img: function SpottedScript_MixmagVote_Entry_View$get_entryConfirm_Img() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EntryConfirm_Img');
    },
    get_entryConfirm_Link: function SpottedScript_MixmagVote_Entry_View$get_entryConfirm_Link() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EntryConfirm_Link');
    },
    get_entryConfirm_YesButton: function SpottedScript_MixmagVote_Entry_View$get_entryConfirm_YesButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EntryConfirm_YesButton');
    },
    get_entryConfirm_NoButton: function SpottedScript_MixmagVote_Entry_View$get_entryConfirm_NoButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EntryConfirm_NoButton');
    },
    get_entry2Panel: function SpottedScript_MixmagVote_Entry_View$get_entry2Panel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Entry2Panel');
    },
    get_entry2Img: function SpottedScript_MixmagVote_Entry_View$get_entry2Img() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Entry2Img');
    },
    get_entry2LinkTextbox: function SpottedScript_MixmagVote_Entry_View$get_entry2LinkTextbox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Entry2LinkTextbox');
    },
    get_entry2LikeButtonHolder: function SpottedScript_MixmagVote_Entry_View$get_entry2LikeButtonHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Entry2LikeButtonHolder');
    },
    get_debugOutput: function SpottedScript_MixmagVote_Entry_View$get_debugOutput() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DebugOutput');
    },
    get_genericContainerPage: function SpottedScript_MixmagVote_Entry_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.MixmagVote.Entry.Controller.registerClass('SpottedScript.MixmagVote.Entry.Controller');
SpottedScript.MixmagVote.Entry.Server.registerClass('SpottedScript.MixmagVote.Entry.Server');
SpottedScript.MixmagVote.Entry.View.registerClass('SpottedScript.MixmagVote.Entry.View', SpottedScript.MixmagVoteUserControl.View);
SpottedScript.MixmagVote.Entry.Controller.instance = null;
