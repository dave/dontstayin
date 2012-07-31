Type.registerNamespace('SpottedScript.Pages.Login');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Login.View
SpottedScript.Pages.Login.View = function SpottedScript_Pages_Login_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Login.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Login.View.prototype = {
    clientId: null,
    get_logoutButton: function SpottedScript_Pages_Login_View$get_logoutButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LogoutButton');
    },
    get_loginPanel: function SpottedScript_Pages_Login_View$get_loginPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LoginPanel');
    },
    get_loggedInPanel: function SpottedScript_Pages_Login_View$get_loggedInPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LoggedInPanel');
    },
    get_loginCountLabel: function SpottedScript_Pages_Login_View$get_loginCountLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_loginCountLabel');
    },
    get_publicProfileLink: function SpottedScript_Pages_Login_View$get_publicProfileLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PublicProfileLink');
    },
    get_errorPanel1: function SpottedScript_Pages_Login_View$get_errorPanel1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ErrorPanel1');
    },
    get_genericContainerPage: function SpottedScript_Pages_Login_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Login.View.registerClass('SpottedScript.Pages.Login.View', SpottedScript.DsiUserControl.View);
