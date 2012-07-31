Type.registerNamespace('SpottedScript.Blank.ResetPhoneIp');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.ResetPhoneIp.View
SpottedScript.Blank.ResetPhoneIp.View = function SpottedScript_Blank_ResetPhoneIp_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.ResetPhoneIp.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.ResetPhoneIp.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Blank_ResetPhoneIp_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.ResetPhoneIp.View.registerClass('SpottedScript.Blank.ResetPhoneIp.View', SpottedScript.BlankUserControl.View);
