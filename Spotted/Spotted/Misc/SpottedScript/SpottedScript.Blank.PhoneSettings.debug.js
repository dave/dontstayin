Type.registerNamespace('SpottedScript.Blank.PhoneSettings');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.PhoneSettings.View
SpottedScript.Blank.PhoneSettings.View = function SpottedScript_Blank_PhoneSettings_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.PhoneSettings.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.PhoneSettings.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Blank_PhoneSettings_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.PhoneSettings.View.registerClass('SpottedScript.Blank.PhoneSettings.View', SpottedScript.BlankUserControl.View);
