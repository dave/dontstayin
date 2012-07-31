Type.registerNamespace('SpottedScript.Admin.BankExport');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.BankExport.View
SpottedScript.Admin.BankExport.View = function SpottedScript_Admin_BankExport_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.BankExport.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.BankExport.View.prototype = {
    clientId: null,
    get_h1: function SpottedScript_Admin_BankExport_View$get_h1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1');
    },
    get_searchBankExportPanel: function SpottedScript_Admin_BankExport_View$get_searchBankExportPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SearchBankExportPanel');
    },
    get_bankExportRadioButtonList: function SpottedScript_Admin_BankExport_View$get_bankExportRadioButtonList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BankExportRadioButtonList');
    },
    get_searchBankExportHeader: function SpottedScript_Admin_BankExport_View$get_searchBankExportHeader() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SearchBankExportHeader');
    },
    get_bankExportLinkP: function SpottedScript_Admin_BankExport_View$get_bankExportLinkP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BankExportLinkP');
    },
    get_bankExportGeneratorLinkLabel: function SpottedScript_Admin_BankExport_View$get_bankExportGeneratorLinkLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BankExportGeneratorLinkLabel');
    },
    get_searchCriteriaTable: function SpottedScript_Admin_BankExport_View$get_searchCriteriaTable() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SearchCriteriaTable');
    },
    get_statusDropDownList: function SpottedScript_Admin_BankExport_View$get_statusDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_StatusDropDownList');
    },
    get_typeDropDownList: function SpottedScript_Admin_BankExport_View$get_typeDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TypeDropDownList');
    },
    get_exportDateCal: function SpottedScript_Admin_BankExport_View$get_exportDateCal() {
        /// <value type="SpottedScript.CustomControls.Cal.Controller"></value>
        return eval(this.clientId + '_ExportDateCalController');
    },
    get_batchRefTextBox: function SpottedScript_Admin_BankExport_View$get_batchRefTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BatchRefTextBox');
    },
    get_uiPromoterHtmlAutoComplete: function SpottedScript_Admin_BankExport_View$get_uiPromoterHtmlAutoComplete() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiPromoterHtmlAutoCompleteBehaviour');
    },
    get_searchButton: function SpottedScript_Admin_BankExport_View$get_searchButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SearchButton');
    },
    get_clearButton: function SpottedScript_Admin_BankExport_View$get_clearButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ClearButton');
    },
    get_searchBankExportGridView: function SpottedScript_Admin_BankExport_View$get_searchBankExportGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SearchBankExportGridView');
    },
    get_searchResultsMessageLabel: function SpottedScript_Admin_BankExport_View$get_searchResultsMessageLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SearchResultsMessageLabel');
    },
    get_searchSummaryTable: function SpottedScript_Admin_BankExport_View$get_searchSummaryTable() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SearchSummaryTable');
    },
    get_fundsClientToCurrentLabel: function SpottedScript_Admin_BankExport_View$get_fundsClientToCurrentLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FundsClientToCurrentLabel');
    },
    get_fundsClientToPromoterLabel: function SpottedScript_Admin_BankExport_View$get_fundsClientToPromoterLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FundsClientToPromoterLabel');
    },
    get_fundsCurrentToPromoterLabel: function SpottedScript_Admin_BankExport_View$get_fundsCurrentToPromoterLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FundsCurrentToPromoterLabel');
    },
    get_fundsCurrentToClientLabel: function SpottedScript_Admin_BankExport_View$get_fundsCurrentToClientLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FundsCurrentToClientLabel');
    },
    get_genericContainerPage: function SpottedScript_Admin_BankExport_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.BankExport.View.registerClass('SpottedScript.Admin.BankExport.View', SpottedScript.AdminUserControl.View);
