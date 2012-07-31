Type.registerNamespace('SpottedScript.Admin.ExceptionLogging');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.ExceptionLogging.View
SpottedScript.Admin.ExceptionLogging.View = function SpottedScript_Admin_ExceptionLogging_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.ExceptionLogging.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.ExceptionLogging.View.prototype = {
    clientId: null,
    get_gridView: function SpottedScript_Admin_ExceptionLogging_View$get_gridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GridView');
    },
    get_genericContainerPage: function SpottedScript_Admin_ExceptionLogging_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.ExceptionLogging.View.registerClass('SpottedScript.Admin.ExceptionLogging.View', SpottedScript.AdminUserControl.View);
