Type.registerNamespace('SpottedScript.Blank.MailingLabels');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.MailingLabels.View
SpottedScript.Blank.MailingLabels.View = function SpottedScript_Blank_MailingLabels_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.MailingLabels.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.MailingLabels.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Blank_MailingLabels_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.MailingLabels.View.registerClass('SpottedScript.Blank.MailingLabels.View', SpottedScript.BlankUserControl.View);
