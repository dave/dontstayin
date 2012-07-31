Type.registerNamespace('SpottedScript.MixmagVote.Home');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.MixmagVote.Home.Controller
SpottedScript.MixmagVote.Home.Controller = function SpottedScript_MixmagVote_Home_Controller(v) {
    /// <param name="v" type="SpottedScript.MixmagVote.Home.View">
    /// </param>
    /// <field name="view" type="SpottedScript.MixmagVote.Home.View">
    /// </field>
    /// <field name="instance" type="SpottedScript.MixmagVote.Home.Controller" static="true">
    /// </field>
    /// <field name="_currentFacebookLoggedIn" type="Boolean">
    /// </field>
    /// <field name="_currentFacebookConnected" type="Boolean">
    /// </field>
    /// <field name="_currentFacebookUID" type="String">
    /// </field>
    /// <field name="_currentFacebookSession" type="Object">
    /// </field>
    SpottedScript.MixmagVote.Home.Controller.instance = this;
    this.view = v;
    if (SpottedScript.Misc.get_browserIsIE()) {
        jQuery(document.body).ready(Function.createDelegate(this, this._initialise));
    }
    else {
        this._initialise();
    }
}
SpottedScript.MixmagVote.Home.Controller.prototype = {
    view: null,
    _initialise: function SpottedScript_MixmagVote_Home_Controller$_initialise() {
    },
    _currentFacebookLoggedIn: false,
    _currentFacebookConnected: false,
    _currentFacebookUID: '0',
    _currentFacebookSession: null,
    _updateCurrentFacebookLoginStatus: function SpottedScript_MixmagVote_Home_Controller$_updateCurrentFacebookLoginStatus(statusResponse) {
        /// <param name="statusResponse" type="Object">
        /// </param>
        this._currentFacebookConnected = ImportedUtilities.U.exists(statusResponse, 'status') && ImportedUtilities.U.get(statusResponse, 'status').toString() === 'connected';
        this._currentFacebookLoggedIn = ImportedUtilities.U.exists(statusResponse, 'status') && ImportedUtilities.U.get(statusResponse, 'status').toString() !== 'unknown';
        this._currentFacebookUID = (this._currentFacebookConnected) ? ImportedUtilities.U.get(statusResponse, 'session/uid').toString() : '0';
        this._currentFacebookSession = (this._currentFacebookConnected) ? ImportedUtilities.U.get(statusResponse, 'session') : null;
        this.debug(this._currentFacebookUID);
    },
    debug: function SpottedScript_MixmagVote_Home_Controller$debug(txt) {
        /// <param name="txt" type="String">
        /// </param>
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.MixmagVote.Home.View
SpottedScript.MixmagVote.Home.View = function SpottedScript_MixmagVote_Home_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.MixmagVote.Home.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.MixmagVote.Home.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_MixmagVote_Home_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.MixmagVote.Home.Controller.registerClass('SpottedScript.MixmagVote.Home.Controller');
SpottedScript.MixmagVote.Home.View.registerClass('SpottedScript.MixmagVote.Home.View', SpottedScript.MixmagVoteUserControl.View);
SpottedScript.MixmagVote.Home.Controller.instance = null;
