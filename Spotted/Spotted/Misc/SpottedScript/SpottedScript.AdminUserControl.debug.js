Type.registerNamespace('SpottedScript.AdminUserControl');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.AdminUserControl.View
SpottedScript.AdminUserControl.View = function SpottedScript_AdminUserControl_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.AdminUserControl.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.AdminUserControl.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_AdminUserControl_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.AdminUserControl.View.registerClass('SpottedScript.AdminUserControl.View', SpottedScript.GenericUserControl.View);
