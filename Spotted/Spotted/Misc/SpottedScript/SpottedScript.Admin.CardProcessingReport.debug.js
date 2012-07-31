Type.registerNamespace('SpottedScript.Admin.CardProcessingReport');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.CardProcessingReport.View
SpottedScript.Admin.CardProcessingReport.View = function SpottedScript_Admin_CardProcessingReport_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.CardProcessingReport.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.CardProcessingReport.View.prototype = {
    clientId: null,
    get_h1: function SpottedScript_Admin_CardProcessingReport_View$get_h1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1');
    },
    get_cardnetProcessingPanel: function SpottedScript_Admin_CardProcessingReport_View$get_cardnetProcessingPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardnetProcessingPanel');
    },
    get_cardnetProcessingSearchCriteriaTable: function SpottedScript_Admin_CardProcessingReport_View$get_cardnetProcessingSearchCriteriaTable() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardnetProcessingSearchCriteriaTable');
    },
    get_fromDateCal: function SpottedScript_Admin_CardProcessingReport_View$get_fromDateCal() {
        /// <value type="SpottedScript.CustomControls.Cal.Controller"></value>
        return eval(this.clientId + '_FromDateCalController');
    },
    get_toDateCal: function SpottedScript_Admin_CardProcessingReport_View$get_toDateCal() {
        /// <value type="SpottedScript.CustomControls.Cal.Controller"></value>
        return eval(this.clientId + '_ToDateCalController');
    },
    get_searchButton: function SpottedScript_Admin_CardProcessingReport_View$get_searchButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SearchButton');
    },
    get_cardnetAccountGridView: function SpottedScript_Admin_CardProcessingReport_View$get_cardnetAccountGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardnetAccountGridView');
    },
    get_sumAccountLabel: function SpottedScript_Admin_CardProcessingReport_View$get_sumAccountLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SumAccountLabel');
    },
    get_sumTransferCountLabel: function SpottedScript_Admin_CardProcessingReport_View$get_sumTransferCountLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SumTransferCountLabel');
    },
    get_genericContainerPage: function SpottedScript_Admin_CardProcessingReport_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.CardProcessingReport.View.registerClass('SpottedScript.Admin.CardProcessingReport.View', SpottedScript.AdminUserControl.View);
