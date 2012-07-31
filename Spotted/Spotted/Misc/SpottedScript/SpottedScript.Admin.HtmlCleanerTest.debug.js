Type.registerNamespace('SpottedScript.Admin.HtmlCleanerTest');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.HtmlCleanerTest.View
SpottedScript.Admin.HtmlCleanerTest.View = function SpottedScript_Admin_HtmlCleanerTest_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.HtmlCleanerTest.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.HtmlCleanerTest.View.prototype = {
    clientId: null,
    get_input: function SpottedScript_Admin_HtmlCleanerTest_View$get_input() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Input');
    },
    get_button1: function SpottedScript_Admin_HtmlCleanerTest_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_output: function SpottedScript_Admin_HtmlCleanerTest_View$get_output() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Output');
    },
    get_genericContainerPage: function SpottedScript_Admin_HtmlCleanerTest_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.HtmlCleanerTest.View.registerClass('SpottedScript.Admin.HtmlCleanerTest.View', SpottedScript.AdminUserControl.View);
