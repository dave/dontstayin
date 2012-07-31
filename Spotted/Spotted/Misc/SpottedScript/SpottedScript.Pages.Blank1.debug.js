Type.registerNamespace('SpottedScript.Pages.Blank1');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Blank1.Controller
SpottedScript.Pages.Blank1.Controller = function SpottedScript_Pages_Blank1_Controller(v) {
    /// <param name="v" type="SpottedScript.Pages.Blank1.View">
    /// </param>
    /// <field name="view" type="SpottedScript.Pages.Blank1.View">
    /// </field>
    /// <field name="instance" type="SpottedScript.Pages.Blank1.Controller" static="true">
    /// </field>
    /// <field name="_currentFacebookLoggedIn" type="Boolean">
    /// </field>
    /// <field name="_currentFacebookConnected" type="Boolean">
    /// </field>
    /// <field name="_currentFacebookUID" type="String">
    /// </field>
    /// <field name="_currentFacebookSession" type="Object">
    /// </field>
    SpottedScript.Pages.Blank1.Controller.instance = this;
    this.view = v;
    if (SpottedScript.Misc.get_browserIsIE()) {
        jQuery(document.body).ready(Function.createDelegate(this, this._initialise));
    }
    else {
        this._initialise();
    }
}
SpottedScript.Pages.Blank1.Controller.prototype = {
    view: null,
    _initialise: function SpottedScript_Pages_Blank1_Controller$_initialise() {
        $addHandler(this.view.get_button1(), 'click', Function.createDelegate(this, this.button1Click));
        $addHandler(this.view.get_button2(), 'click', Function.createDelegate(this, this.button2Click));
    },
    _currentFacebookLoggedIn: false,
    _currentFacebookConnected: false,
    _currentFacebookUID: '0',
    _currentFacebookSession: null,
    _updateCurrentFacebookLoginStatus: function SpottedScript_Pages_Blank1_Controller$_updateCurrentFacebookLoginStatus(statusResponse) {
        /// <param name="statusResponse" type="Object">
        /// </param>
        this._currentFacebookConnected = ImportedUtilities.U.exists(statusResponse, 'status') && ImportedUtilities.U.get(statusResponse, 'status').toString() === 'connected';
        this._currentFacebookLoggedIn = ImportedUtilities.U.exists(statusResponse, 'status') && ImportedUtilities.U.get(statusResponse, 'status').toString() !== 'unknown';
        this._currentFacebookUID = (this._currentFacebookConnected) ? ImportedUtilities.U.get(statusResponse, 'session/uid').toString() : '0';
        this._currentFacebookSession = (this._currentFacebookConnected) ? ImportedUtilities.U.get(statusResponse, 'session') : null;
        this.debug(this._currentFacebookUID);
    },
    button1Click: function SpottedScript_Pages_Blank1_Controller$button1Click(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        this.debug('click!');
        FB.getLoginStatus(Function.createDelegate(this, function(statusResponse) {
            this._updateCurrentFacebookLoginStatus(statusResponse);
        }));
        FB.Event.subscribe('edge.create', Function.createDelegate(this, function(edgeCreateResponse) {
            this.debug('edge.create');
        }));
    },
    button2Click: function SpottedScript_Pages_Blank1_Controller$button2Click(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        e.preventDefault();
        FB.api(F.d('method', 'fql.query', 'query', 'SELECT type, created_time FROM page_fan WHERE page_id=\"18658586341\" AND uid=\"' + this._currentFacebookUID + '\"'), Function.createDelegate(this, function(likeFqlResponse) {
            this.debug(ImportedUtilities.U.toString(likeFqlResponse));
        }));
    },
    debug: function SpottedScript_Pages_Blank1_Controller$debug(txt) {
        /// <param name="txt" type="String">
        /// </param>
        this.view.get_output().innerHTML = txt + '\n' + this.view.get_output().innerHTML;
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Blank1.View
SpottedScript.Pages.Blank1.View = function SpottedScript_Pages_Blank1_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Blank1.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Blank1.View.prototype = {
    clientId: null,
    get_button1: function SpottedScript_Pages_Blank1_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_button2: function SpottedScript_Pages_Blank1_View$get_button2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button2');
    },
    get_output: function SpottedScript_Pages_Blank1_View$get_output() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Output');
    },
    get_genericContainerPage: function SpottedScript_Pages_Blank1_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Blank1.Controller.registerClass('SpottedScript.Pages.Blank1.Controller');
SpottedScript.Pages.Blank1.View.registerClass('SpottedScript.Pages.Blank1.View', SpottedScript.DsiUserControl.View);
SpottedScript.Pages.Blank1.Controller.instance = null;
