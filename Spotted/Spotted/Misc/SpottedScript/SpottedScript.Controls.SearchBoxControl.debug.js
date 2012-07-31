Type.registerNamespace('SpottedScript.Controls.SearchBoxControl');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.SearchBoxControl.View
SpottedScript.Controls.SearchBoxControl.View = function SpottedScript_Controls_SearchBoxControl_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    this.clientId = clientId;
}
SpottedScript.Controls.SearchBoxControl.View.prototype = {
    clientId: null,
    get_uiSearchQuery: function SpottedScript_Controls_SearchBoxControl_View$get_uiSearchQuery() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSearchQuery');
    },
    get_uiTagAutoComplete: function SpottedScript_Controls_SearchBoxControl_View$get_uiTagAutoComplete() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiTagAutoComplete');
    },
    get_uiSubmitSearchButton: function SpottedScript_Controls_SearchBoxControl_View$get_uiSubmitSearchButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSubmitSearchButton');
    }
}
SpottedScript.Controls.SearchBoxControl.View.registerClass('SpottedScript.Controls.SearchBoxControl.View');
