Type.registerNamespace('SpottedScript.Admin.Blank');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.Blank.View
SpottedScript.Admin.Blank.View = function SpottedScript_Admin_Blank_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.Blank.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.Blank.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Admin_Blank_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.Blank.View.registerClass('SpottedScript.Admin.Blank.View', SpottedScript.AdminUserControl.View);
