Type.registerNamespace('SpottedScript.Blank.OverdueAccounts');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.OverdueAccounts.View
SpottedScript.Blank.OverdueAccounts.View = function SpottedScript_Blank_OverdueAccounts_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.OverdueAccounts.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.OverdueAccounts.View.prototype = {
    clientId: null,
    get_loggedInPanel: function SpottedScript_Blank_OverdueAccounts_View$get_loggedInPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LoggedInPanel');
    },
    get_loggedInLink: function SpottedScript_Blank_OverdueAccounts_View$get_loggedInLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LoggedInLink');
    },
    get_logOutLink: function SpottedScript_Blank_OverdueAccounts_View$get_logOutLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LogOutLink');
    },
    get_loggedInAs: function SpottedScript_Blank_OverdueAccounts_View$get_loggedInAs() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LoggedInAs');
    },
    get_promoterAccountsWarningControl: function SpottedScript_Blank_OverdueAccounts_View$get_promoterAccountsWarningControl() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterAccountsWarningControl');
    },
    get_genericContainerPage: function SpottedScript_Blank_OverdueAccounts_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.OverdueAccounts.View.registerClass('SpottedScript.Blank.OverdueAccounts.View', SpottedScript.BlankUserControl.View);
