Type.registerNamespace('SpottedScript.Admin.DomainStats');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.DomainStats.View
SpottedScript.Admin.DomainStats.View = function SpottedScript_Admin_DomainStats_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.DomainStats.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.DomainStats.View.prototype = {
    clientId: null,
    get_uiPromoterHtmlAutoComplete: function SpottedScript_Admin_DomainStats_View$get_uiPromoterHtmlAutoComplete() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiPromoterHtmlAutoCompleteBehaviour');
    },
    get_uiDomainsList: function SpottedScript_Admin_DomainStats_View$get_uiDomainsList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiDomainsList');
    },
    get_uiSelectDomainButton: function SpottedScript_Admin_DomainStats_View$get_uiSelectDomainButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSelectDomainButton');
    },
    get_uiGridView: function SpottedScript_Admin_DomainStats_View$get_uiGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiGridView');
    },
    get_genericContainerPage: function SpottedScript_Admin_DomainStats_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.DomainStats.View.registerClass('SpottedScript.Admin.DomainStats.View', SpottedScript.AdminUserControl.View);
