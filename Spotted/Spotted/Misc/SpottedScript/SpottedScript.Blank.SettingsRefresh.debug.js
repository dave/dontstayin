Type.registerNamespace('SpottedScript.Blank.SettingsRefresh');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.SettingsRefresh.View
SpottedScript.Blank.SettingsRefresh.View = function SpottedScript_Blank_SettingsRefresh_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.SettingsRefresh.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.SettingsRefresh.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Blank_SettingsRefresh_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.SettingsRefresh.View.registerClass('SpottedScript.Blank.SettingsRefresh.View', SpottedScript.BlankUserControl.View);
