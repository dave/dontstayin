Type.registerNamespace('SpottedScript.Admin.TicketRuns');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.TicketRuns.View
SpottedScript.Admin.TicketRuns.View = function SpottedScript_Admin_TicketRuns_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.TicketRuns.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.TicketRuns.View.prototype = {
    clientId: null,
    get_allTicketRunsPanel: function SpottedScript_Admin_TicketRuns_View$get_allTicketRunsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AllTicketRunsPanel');
    },
    get_uiPromotersAutoComplete: function SpottedScript_Admin_TicketRuns_View$get_uiPromotersAutoComplete() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiPromotersAutoCompleteBehaviour');
    },
    get_ticketRunKTextBox: function SpottedScript_Admin_TicketRuns_View$get_ticketRunKTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketRunKTextBox');
    },
    get_onlyShowTicketRunsWithSoldTicketsCheckBox: function SpottedScript_Admin_TicketRuns_View$get_onlyShowTicketRunsWithSoldTicketsCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OnlyShowTicketRunsWithSoldTicketsCheckBox');
    },
    get_uiEventAutoComplete: function SpottedScript_Admin_TicketRuns_View$get_uiEventAutoComplete() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiEventAutoCompleteBehaviour');
    },
    get_statusDropDownList: function SpottedScript_Admin_TicketRuns_View$get_statusDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_StatusDropDownList');
    },
    get_searchButton: function SpottedScript_Admin_TicketRuns_View$get_searchButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SearchButton');
    },
    get_clearButton: function SpottedScript_Admin_TicketRuns_View$get_clearButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ClearButton');
    },
    get_h1Title: function SpottedScript_Admin_TicketRuns_View$get_h1Title() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1Title');
    },
    get_ticketRunsGridView: function SpottedScript_Admin_TicketRuns_View$get_ticketRunsGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketRunsGridView');
    },
    get_searchResultsMessageLabel: function SpottedScript_Admin_TicketRuns_View$get_searchResultsMessageLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SearchResultsMessageLabel');
    },
    get_ticketRunsJavascriptLabel: function SpottedScript_Admin_TicketRuns_View$get_ticketRunsJavascriptLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketRunsJavascriptLabel');
    },
    get_genericContainerPage: function SpottedScript_Admin_TicketRuns_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.TicketRuns.View.registerClass('SpottedScript.Admin.TicketRuns.View', SpottedScript.AdminUserControl.View);
