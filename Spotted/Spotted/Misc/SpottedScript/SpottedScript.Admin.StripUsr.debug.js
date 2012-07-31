Type.registerNamespace('SpottedScript.Admin.StripUsr');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.StripUsr.View
SpottedScript.Admin.StripUsr.View = function SpottedScript_Admin_StripUsr_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.StripUsr.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.StripUsr.View.prototype = {
    clientId: null,
    get_objectKTextBox: function SpottedScript_Admin_StripUsr_View$get_objectKTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ObjectKTextBox');
    },
    get_deleteButton: function SpottedScript_Admin_StripUsr_View$get_deleteButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DeleteButton');
    },
    get_doneLabel: function SpottedScript_Admin_StripUsr_View$get_doneLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DoneLabel');
    },
    get_genericContainerPage: function SpottedScript_Admin_StripUsr_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.StripUsr.View.registerClass('SpottedScript.Admin.StripUsr.View', SpottedScript.AdminUserControl.View);
