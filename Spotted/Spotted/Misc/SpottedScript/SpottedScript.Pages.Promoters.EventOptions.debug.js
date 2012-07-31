Type.registerNamespace('SpottedScript.Pages.Promoters.EventOptions');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Promoters.EventOptions.View
SpottedScript.Pages.Promoters.EventOptions.View = function SpottedScript_Pages_Promoters_EventOptions_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Promoters.EventOptions.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Promoters.EventOptions.View.prototype = {
    clientId: null,
    get_panelEvent: function SpottedScript_Pages_Promoters_EventOptions_View$get_panelEvent() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelEvent');
    },
    get_promoterIntro: function SpottedScript_Pages_Promoters_EventOptions_View$get_promoterIntro() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterIntro');
    },
    get_eventLinksP: function SpottedScript_Pages_Promoters_EventOptions_View$get_eventLinksP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventLinksP');
    },
    get_eventDetailsPicCell: function SpottedScript_Pages_Promoters_EventOptions_View$get_eventDetailsPicCell() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventDetailsPicCell');
    },
    get_bannersPanel: function SpottedScript_Pages_Promoters_EventOptions_View$get_bannersPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BannersPanel');
    },
    get_noBannersPanel: function SpottedScript_Pages_Promoters_EventOptions_View$get_noBannersPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoBannersPanel');
    },
    get_bannerAddLink: function SpottedScript_Pages_Promoters_EventOptions_View$get_bannerAddLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BannerAddLink');
    },
    get_bannerAddLink1: function SpottedScript_Pages_Promoters_EventOptions_View$get_bannerAddLink1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BannerAddLink1');
    },
    get_bannerDataGrid: function SpottedScript_Pages_Promoters_EventOptions_View$get_bannerDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BannerDataGrid');
    },
    get_newsPanel: function SpottedScript_Pages_Promoters_EventOptions_View$get_newsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NewsPanel');
    },
    get_noNewsPanel: function SpottedScript_Pages_Promoters_EventOptions_View$get_noNewsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoNewsPanel');
    },
    get_newsDataGrid: function SpottedScript_Pages_Promoters_EventOptions_View$get_newsDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NewsDataGrid');
    },
    get_newsAddThread: function SpottedScript_Pages_Promoters_EventOptions_View$get_newsAddThread() {
        /// <value type="SpottedScript.Controls.AddThread.Controller"></value>
        return eval(this.clientId + '_NewsAddThreadController');
    },
    get_newsAddThreadLinkP: function SpottedScript_Pages_Promoters_EventOptions_View$get_newsAddThreadLinkP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NewsAddThreadLinkP');
    },
    get_newsAddThreadStatusHidden: function SpottedScript_Pages_Promoters_EventOptions_View$get_newsAddThreadStatusHidden() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NewsAddThreadStatusHidden');
    },
    get_newsAddThreadPanel: function SpottedScript_Pages_Promoters_EventOptions_View$get_newsAddThreadPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NewsAddThreadPanel');
    },
    get_newsPostPanel: function SpottedScript_Pages_Promoters_EventOptions_View$get_newsPostPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NewsPostPanel');
    },
    get_noNewsPostPanel: function SpottedScript_Pages_Promoters_EventOptions_View$get_noNewsPostPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoNewsPostPanel');
    },
    get_articlePanel: function SpottedScript_Pages_Promoters_EventOptions_View$get_articlePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ArticlePanel');
    },
    get_noArticlePanel: function SpottedScript_Pages_Promoters_EventOptions_View$get_noArticlePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoArticlePanel');
    },
    get_articleAddLink: function SpottedScript_Pages_Promoters_EventOptions_View$get_articleAddLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ArticleAddLink');
    },
    get_articleAddLink1: function SpottedScript_Pages_Promoters_EventOptions_View$get_articleAddLink1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ArticleAddLink1');
    },
    get_articleDataGrid: function SpottedScript_Pages_Promoters_EventOptions_View$get_articleDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ArticleDataGrid');
    },
    get_compPanel: function SpottedScript_Pages_Promoters_EventOptions_View$get_compPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CompPanel');
    },
    get_noCompPanel: function SpottedScript_Pages_Promoters_EventOptions_View$get_noCompPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoCompPanel');
    },
    get_compAddLink: function SpottedScript_Pages_Promoters_EventOptions_View$get_compAddLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CompAddLink');
    },
    get_compAddLink1: function SpottedScript_Pages_Promoters_EventOptions_View$get_compAddLink1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CompAddLink1');
    },
    get_compDataGrid: function SpottedScript_Pages_Promoters_EventOptions_View$get_compDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CompDataGrid');
    },
    get_guestlistPanel: function SpottedScript_Pages_Promoters_EventOptions_View$get_guestlistPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GuestlistPanel');
    },
    get_noGuestlistPanel: function SpottedScript_Pages_Promoters_EventOptions_View$get_noGuestlistPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoGuestlistPanel');
    },
    get_guestlistAddLink: function SpottedScript_Pages_Promoters_EventOptions_View$get_guestlistAddLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GuestlistAddLink');
    },
    get_guestlistDataGrid: function SpottedScript_Pages_Promoters_EventOptions_View$get_guestlistDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GuestlistDataGrid');
    },
    get_header: function SpottedScript_Pages_Promoters_EventOptions_View$get_header() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Header');
    },
    get_spotterRequestYesPanel: function SpottedScript_Pages_Promoters_EventOptions_View$get_spotterRequestYesPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterRequestYesPanel');
    },
    get_h1: function SpottedScript_Pages_Promoters_EventOptions_View$get_h1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1');
    },
    get_spotterRequestDetails: function SpottedScript_Pages_Promoters_EventOptions_View$get_spotterRequestDetails() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterRequestDetails');
    },
    get_spotterRequestYesLink: function SpottedScript_Pages_Promoters_EventOptions_View$get_spotterRequestYesLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterRequestYesLink');
    },
    get_spotterRequestNoPanel: function SpottedScript_Pages_Promoters_EventOptions_View$get_spotterRequestNoPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterRequestNoPanel');
    },
    get_h2: function SpottedScript_Pages_Promoters_EventOptions_View$get_h2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H2');
    },
    get_spotterRequestNoLink: function SpottedScript_Pages_Promoters_EventOptions_View$get_spotterRequestNoLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterRequestNoLink');
    },
    get_ticketRunPanel: function SpottedScript_Pages_Promoters_EventOptions_View$get_ticketRunPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketRunPanel');
    },
    get_h10: function SpottedScript_Pages_Promoters_EventOptions_View$get_h10() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H10');
    },
    get_ticketRunsGridView: function SpottedScript_Pages_Promoters_EventOptions_View$get_ticketRunsGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketRunsGridView');
    },
    get_noTicketRunsP: function SpottedScript_Pages_Promoters_EventOptions_View$get_noTicketRunsP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoTicketRunsP');
    },
    get_sellTicketsP: function SpottedScript_Pages_Promoters_EventOptions_View$get_sellTicketsP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SellTicketsP');
    },
    get_sellTicketsLink: function SpottedScript_Pages_Promoters_EventOptions_View$get_sellTicketsLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SellTicketsLink');
    },
    get_noSellTicketsP: function SpottedScript_Pages_Promoters_EventOptions_View$get_noSellTicketsP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoSellTicketsP');
    },
    get_doorlistP: function SpottedScript_Pages_Promoters_EventOptions_View$get_doorlistP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DoorlistP');
    },
    get_doorlistLink: function SpottedScript_Pages_Promoters_EventOptions_View$get_doorlistLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DoorlistLink');
    },
    get_adminLinksPanel: function SpottedScript_Pages_Promoters_EventOptions_View$get_adminLinksPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AdminLinksPanel');
    },
    get_h11: function SpottedScript_Pages_Promoters_EventOptions_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_h17: function SpottedScript_Pages_Promoters_EventOptions_View$get_h17() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H17');
    },
    get_eventHighlightPanel: function SpottedScript_Pages_Promoters_EventOptions_View$get_eventHighlightPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventHighlightPanel');
    },
    get_h12a: function SpottedScript_Pages_Promoters_EventOptions_View$get_h12a() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12a');
    },
    get_noEventHighlightPanel: function SpottedScript_Pages_Promoters_EventOptions_View$get_noEventHighlightPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoEventHighlightPanel');
    },
    get_h12b: function SpottedScript_Pages_Promoters_EventOptions_View$get_h12b() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12b');
    },
    get_eventHighlightLink: function SpottedScript_Pages_Promoters_EventOptions_View$get_eventHighlightLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventHighlightLink');
    },
    get_h12: function SpottedScript_Pages_Promoters_EventOptions_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_h110: function SpottedScript_Pages_Promoters_EventOptions_View$get_h110() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H110');
    },
    get_h111: function SpottedScript_Pages_Promoters_EventOptions_View$get_h111() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H111');
    },
    get_h13: function SpottedScript_Pages_Promoters_EventOptions_View$get_h13() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H13');
    },
    get_h19: function SpottedScript_Pages_Promoters_EventOptions_View$get_h19() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H19');
    },
    get_h14: function SpottedScript_Pages_Promoters_EventOptions_View$get_h14() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H14');
    },
    get_h112: function SpottedScript_Pages_Promoters_EventOptions_View$get_h112() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H112');
    },
    get_spotterInvitePanel: function SpottedScript_Pages_Promoters_EventOptions_View$get_spotterInvitePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterInvitePanel');
    },
    get_h16: function SpottedScript_Pages_Promoters_EventOptions_View$get_h16() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H16');
    },
    get_genericContainerPage: function SpottedScript_Pages_Promoters_EventOptions_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Promoters.EventOptions.View.registerClass('SpottedScript.Pages.Promoters.EventOptions.View', SpottedScript.Pages.Promoters.PromoterUserControl.View);
