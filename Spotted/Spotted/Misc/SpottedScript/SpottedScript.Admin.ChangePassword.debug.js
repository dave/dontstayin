Type.registerNamespace('SpottedScript.Admin.ChangePassword');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.ChangePassword.View
SpottedScript.Admin.ChangePassword.View = function SpottedScript_Admin_ChangePassword_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.ChangePassword.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.ChangePassword.View.prototype = {
    clientId: null,
    get_usrK: function SpottedScript_Admin_ChangePassword_View$get_usrK() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrK');
    },
    get_usrPassword: function SpottedScript_Admin_ChangePassword_View$get_usrPassword() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrPassword');
    },
    get_button2: function SpottedScript_Admin_ChangePassword_View$get_button2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button2');
    },
    get_outLabel: function SpottedScript_Admin_ChangePassword_View$get_outLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OutLabel');
    },
    get_genericContainerPage: function SpottedScript_Admin_ChangePassword_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.ChangePassword.View.registerClass('SpottedScript.Admin.ChangePassword.View', SpottedScript.AdminUserControl.View);
