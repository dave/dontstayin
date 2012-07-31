Type.registerNamespace('SpottedScript.Pages.Promoters.AllTicketRuns');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Promoters.AllTicketRuns.View
SpottedScript.Pages.Promoters.AllTicketRuns.View = function SpottedScript_Pages_Promoters_AllTicketRuns_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Promoters.AllTicketRuns.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Promoters.AllTicketRuns.View.prototype = {
    clientId: null,
    get_promoterIntro: function SpottedScript_Pages_Promoters_AllTicketRuns_View$get_promoterIntro() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterIntro');
    },
    get_addTicketRunPanel: function SpottedScript_Pages_Promoters_AllTicketRuns_View$get_addTicketRunPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddTicketRunPanel');
    },
    get_ticketSalesSummaryTable: function SpottedScript_Pages_Promoters_AllTicketRuns_View$get_ticketSalesSummaryTable() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketSalesSummaryTable');
    },
    get_totalTicketRunsLabel: function SpottedScript_Pages_Promoters_AllTicketRuns_View$get_totalTicketRunsLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TotalTicketRunsLabel');
    },
    get_totalTicketsSoldLabel: function SpottedScript_Pages_Promoters_AllTicketRuns_View$get_totalTicketsSoldLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TotalTicketsSoldLabel');
    },
    get_ticketFundsReleasedLabel: function SpottedScript_Pages_Promoters_AllTicketRuns_View$get_ticketFundsReleasedLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketFundsReleasedLabel');
    },
    get_ticketFundsInWaitingLabel: function SpottedScript_Pages_Promoters_AllTicketRuns_View$get_ticketFundsInWaitingLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketFundsInWaitingLabel');
    },
    get_allTicketRunsPanel: function SpottedScript_Pages_Promoters_AllTicketRuns_View$get_allTicketRunsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AllTicketRunsPanel');
    },
    get_h1Title: function SpottedScript_Pages_Promoters_AllTicketRuns_View$get_h1Title() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1Title');
    },
    get_selectCurrentDateRangeLinkButton: function SpottedScript_Pages_Promoters_AllTicketRuns_View$get_selectCurrentDateRangeLinkButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SelectCurrentDateRangeLinkButton');
    },
    get_selectPastDateRangeLinkButton: function SpottedScript_Pages_Promoters_AllTicketRuns_View$get_selectPastDateRangeLinkButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SelectPastDateRangeLinkButton');
    },
    get_selectAllDateRangeLinkButton: function SpottedScript_Pages_Promoters_AllTicketRuns_View$get_selectAllDateRangeLinkButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SelectAllDateRangeLinkButton');
    },
    get_ticketRunsGridView: function SpottedScript_Pages_Promoters_AllTicketRuns_View$get_ticketRunsGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketRunsGridView');
    },
    get_paginationPanel: function SpottedScript_Pages_Promoters_AllTicketRuns_View$get_paginationPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PaginationPanel');
    },
    get_prevPageLinkButton: function SpottedScript_Pages_Promoters_AllTicketRuns_View$get_prevPageLinkButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PrevPageLinkButton');
    },
    get_nextPageLinkButton: function SpottedScript_Pages_Promoters_AllTicketRuns_View$get_nextPageLinkButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NextPageLinkButton');
    },
    get_adminLinksPanel: function SpottedScript_Pages_Promoters_AllTicketRuns_View$get_adminLinksPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AdminLinksPanel');
    },
    get_genericContainerPage: function SpottedScript_Pages_Promoters_AllTicketRuns_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Promoters.AllTicketRuns.View.registerClass('SpottedScript.Pages.Promoters.AllTicketRuns.View', SpottedScript.Pages.Promoters.PromoterUserControl.View);
