Type.registerNamespace('SpottedScript.Admin.MultiDelete');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.MultiDelete.View
SpottedScript.Admin.MultiDelete.View = function SpottedScript_Admin_MultiDelete_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.MultiDelete.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.MultiDelete.View.prototype = {
    clientId: null,
    get_deleteButton: function SpottedScript_Admin_MultiDelete_View$get_deleteButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DeleteButton');
    },
    get_objectTypeDropDown: function SpottedScript_Admin_MultiDelete_View$get_objectTypeDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ObjectTypeDropDown');
    },
    get_objectKTextBox: function SpottedScript_Admin_MultiDelete_View$get_objectKTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ObjectKTextBox');
    },
    get_doneLabel: function SpottedScript_Admin_MultiDelete_View$get_doneLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DoneLabel');
    },
    get_genericContainerPage: function SpottedScript_Admin_MultiDelete_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.MultiDelete.View.registerClass('SpottedScript.Admin.MultiDelete.View', SpottedScript.AdminUserControl.View);
