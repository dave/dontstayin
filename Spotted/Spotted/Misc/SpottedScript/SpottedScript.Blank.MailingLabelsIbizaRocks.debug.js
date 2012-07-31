Type.registerNamespace('SpottedScript.Blank.MailingLabelsIbizaRocks');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.MailingLabelsIbizaRocks.View
SpottedScript.Blank.MailingLabelsIbizaRocks.View = function SpottedScript_Blank_MailingLabelsIbizaRocks_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.MailingLabelsIbizaRocks.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.MailingLabelsIbizaRocks.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Blank_MailingLabelsIbizaRocks_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.MailingLabelsIbizaRocks.View.registerClass('SpottedScript.Blank.MailingLabelsIbizaRocks.View', SpottedScript.BlankUserControl.View);
