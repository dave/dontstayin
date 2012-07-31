Type.registerNamespace('SpottedScript.Admin.TicketSearch');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.TicketSearch.View
SpottedScript.Admin.TicketSearch.View = function SpottedScript_Admin_TicketSearch_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.TicketSearch.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.TicketSearch.View.prototype = {
    clientId: null,
    get_ticketSearchCriteriaPanel: function SpottedScript_Admin_TicketSearch_View$get_ticketSearchCriteriaPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketSearchCriteriaPanel');
    },
    get_uiPromotersAutoComplete: function SpottedScript_Admin_TicketSearch_View$get_uiPromotersAutoComplete() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiPromotersAutoCompleteBehaviour');
    },
    get_firstNameTextBox: function SpottedScript_Admin_TicketSearch_View$get_firstNameTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FirstNameTextBox');
    },
    get_ticketRunKTextBox: function SpottedScript_Admin_TicketSearch_View$get_ticketRunKTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketRunKTextBox');
    },
    get_uiUsersAutoComplete: function SpottedScript_Admin_TicketSearch_View$get_uiUsersAutoComplete() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiUsersAutoCompleteBehaviour');
    },
    get_lastNameTextBox: function SpottedScript_Admin_TicketSearch_View$get_lastNameTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LastNameTextBox');
    },
    get_statusDropDownList: function SpottedScript_Admin_TicketSearch_View$get_statusDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_StatusDropDownList');
    },
    get_cardDigitsTextBox: function SpottedScript_Admin_TicketSearch_View$get_cardDigitsTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CardDigitsTextBox');
    },
    get_postCodeTextBox: function SpottedScript_Admin_TicketSearch_View$get_postCodeTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PostCodeTextBox');
    },
    get_feedbackDropDownList: function SpottedScript_Admin_TicketSearch_View$get_feedbackDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FeedbackDropDownList');
    },
    get_searchButton: function SpottedScript_Admin_TicketSearch_View$get_searchButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SearchButton');
    },
    get_clearButton: function SpottedScript_Admin_TicketSearch_View$get_clearButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ClearButton');
    },
    get_errorLabel: function SpottedScript_Admin_TicketSearch_View$get_errorLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ErrorLabel');
    },
    get_searchResultsTicketsGridView: function SpottedScript_Admin_TicketSearch_View$get_searchResultsTicketsGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SearchResultsTicketsGridView');
    },
    get_searchResultsMessageLabel: function SpottedScript_Admin_TicketSearch_View$get_searchResultsMessageLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SearchResultsMessageLabel');
    },
    get_genericContainerPage: function SpottedScript_Admin_TicketSearch_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.TicketSearch.View.registerClass('SpottedScript.Admin.TicketSearch.View', SpottedScript.AdminUserControl.View);
