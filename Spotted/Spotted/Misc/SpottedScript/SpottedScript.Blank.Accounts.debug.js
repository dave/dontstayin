Type.registerNamespace('SpottedScript.Blank.Accounts');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.Accounts.View
SpottedScript.Blank.Accounts.View = function SpottedScript_Blank_Accounts_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.Accounts.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.Accounts.View.prototype = {
    clientId: null,
    get_spotterLetterRepeater: function SpottedScript_Blank_Accounts_View$get_spotterLetterRepeater() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterLetterRepeater');
    },
    get_markAllSent: function SpottedScript_Blank_Accounts_View$get_markAllSent() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MarkAllSent');
    },
    get_successLabel: function SpottedScript_Blank_Accounts_View$get_successLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SuccessLabel');
    },
    get_failLabel: function SpottedScript_Blank_Accounts_View$get_failLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FailLabel');
    },
    get_genericContainerPage: function SpottedScript_Blank_Accounts_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.Accounts.View.registerClass('SpottedScript.Blank.Accounts.View', SpottedScript.DsiUserControl.View);
