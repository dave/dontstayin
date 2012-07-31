Type.registerNamespace('SpottedScript.Blank.MailingReport');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.MailingReport.View
SpottedScript.Blank.MailingReport.View = function SpottedScript_Blank_MailingReport_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.MailingReport.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.MailingReport.View.prototype = {
    clientId: null,
    get_button2: function SpottedScript_Blank_MailingReport_View$get_button2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button2');
    },
    get_pass: function SpottedScript_Blank_MailingReport_View$get_pass() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Pass');
    },
    get_genericContainerPage: function SpottedScript_Blank_MailingReport_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.MailingReport.View.registerClass('SpottedScript.Blank.MailingReport.View', SpottedScript.BlankUserControl.View);
