Type.registerNamespace('SpottedScript.Admin.ReportABug');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.ReportABug.View
SpottedScript.Admin.ReportABug.View = function SpottedScript_Admin_ReportABug_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.ReportABug.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.ReportABug.View.prototype = {
    clientId: null,
    get_uiBugFormPanel: function SpottedScript_Admin_ReportABug_View$get_uiBugFormPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiBugFormPanel');
    },
    get_uiTitle: function SpottedScript_Admin_ReportABug_View$get_uiTitle() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiTitle');
    },
    get_requiredFieldValidator1: function SpottedScript_Admin_ReportABug_View$get_requiredFieldValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFieldValidator1');
    },
    get_uiDescription: function SpottedScript_Admin_ReportABug_View$get_uiDescription() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiDescription');
    },
    get_uiUrl: function SpottedScript_Admin_ReportABug_View$get_uiUrl() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiUrl');
    },
    get_uiSubmit: function SpottedScript_Admin_ReportABug_View$get_uiSubmit() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSubmit');
    },
    get_uiSuccessPanel: function SpottedScript_Admin_ReportABug_View$get_uiSuccessPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSuccessPanel');
    },
    get_genericContainerPage: function SpottedScript_Admin_ReportABug_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.ReportABug.View.registerClass('SpottedScript.Admin.ReportABug.View', SpottedScript.AdminUserControl.View);
