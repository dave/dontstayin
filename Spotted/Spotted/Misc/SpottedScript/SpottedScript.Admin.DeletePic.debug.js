Type.registerNamespace('SpottedScript.Admin.DeletePic');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.DeletePic.View
SpottedScript.Admin.DeletePic.View = function SpottedScript_Admin_DeletePic_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.DeletePic.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.DeletePic.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Admin_DeletePic_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.DeletePic.View.registerClass('SpottedScript.Admin.DeletePic.View', SpottedScript.AdminUserControl.View);
