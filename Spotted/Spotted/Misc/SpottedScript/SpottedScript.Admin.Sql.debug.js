Type.registerNamespace('SpottedScript.Admin.Sql');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.Sql.View
SpottedScript.Admin.Sql.View = function SpottedScript_Admin_Sql_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.Sql.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.Sql.View.prototype = {
    clientId: null,
    get_password: function SpottedScript_Admin_Sql_View$get_password() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Password');
    },
    get_query: function SpottedScript_Admin_Sql_View$get_query() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Query');
    },
    get_genericContainerPage: function SpottedScript_Admin_Sql_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.Sql.View.registerClass('SpottedScript.Admin.Sql.View', SpottedScript.AdminUserControl.View);
