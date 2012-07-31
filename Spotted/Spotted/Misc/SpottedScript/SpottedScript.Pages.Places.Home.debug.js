Type.registerNamespace('SpottedScript.Pages.Places.Home');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Places.Home.View
SpottedScript.Pages.Places.Home.View = function SpottedScript_Pages_Places_Home_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Places.Home.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Places.Home.View.prototype = {
    clientId: null,
    get_topPhotosNewsPanel: function SpottedScript_Pages_Places_Home_View$get_topPhotosNewsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TopPhotosNewsPanel');
    },
    get_quickLinksHotTickets: function SpottedScript_Pages_Places_Home_View$get_quickLinksHotTickets() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_QuickLinksHotTickets');
    },
    get_quickLinksFreeGuestlist: function SpottedScript_Pages_Places_Home_View$get_quickLinksFreeGuestlist() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_QuickLinksFreeGuestlist');
    },
    get_hotTicketsLinkPlaceLabel: function SpottedScript_Pages_Places_Home_View$get_hotTicketsLinkPlaceLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_HotTicketsLinkPlaceLabel');
    },
    get_freeGuestlistLinkPlaceLabel: function SpottedScript_Pages_Places_Home_View$get_freeGuestlistLinkPlaceLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FreeGuestlistLinkPlaceLabel');
    },
    get_featuredVenuesDataList: function SpottedScript_Pages_Places_Home_View$get_featuredVenuesDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FeaturedVenuesDataList');
    },
    get_h13: function SpottedScript_Pages_Places_Home_View$get_h13() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H13');
    },
    get_pageHeading: function SpottedScript_Pages_Places_Home_View$get_pageHeading() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PageHeading');
    },
    get_placeSelectedPanel: function SpottedScript_Pages_Places_Home_View$get_placeSelectedPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PlaceSelectedPanel');
    },
    get_placePopulationLabel: function SpottedScript_Pages_Places_Home_View$get_placePopulationLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PlacePopulationLabel');
    },
    get_quickLinksCalendar: function SpottedScript_Pages_Places_Home_View$get_quickLinksCalendar() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_QuickLinksCalendar');
    },
    get_quickLinksTickets: function SpottedScript_Pages_Places_Home_View$get_quickLinksTickets() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_QuickLinksTickets');
    },
    get_placePicImg: function SpottedScript_Pages_Places_Home_View$get_placePicImg() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PlacePicImg');
    },
    get_discussionLink: function SpottedScript_Pages_Places_Home_View$get_discussionLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DiscussionLink');
    },
    get_placeImgCell: function SpottedScript_Pages_Places_Home_View$get_placeImgCell() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PlaceImgCell');
    },
    get_discussionLinkPlaceLabel: function SpottedScript_Pages_Places_Home_View$get_discussionLinkPlaceLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DiscussionLinkPlaceLabel');
    },
    get_calendarLinkPlaceLabel: function SpottedScript_Pages_Places_Home_View$get_calendarLinkPlaceLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CalendarLinkPlaceLabel');
    },
    get_ticketsLinkPlaceLabel: function SpottedScript_Pages_Places_Home_View$get_ticketsLinkPlaceLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketsLinkPlaceLabel');
    },
    get_latest: function SpottedScript_Pages_Places_Home_View$get_latest() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Latest');
    },
    get_visitCheck: function SpottedScript_Pages_Places_Home_View$get_visitCheck() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VisitCheck');
    },
    get_discussionLinkCommentsLabel: function SpottedScript_Pages_Places_Home_View$get_discussionLinkCommentsLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DiscussionLinkCommentsLabel');
    },
    get_placeNameLabel1: function SpottedScript_Pages_Places_Home_View$get_placeNameLabel1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PlaceNameLabel1');
    },
    get_placeNameLabel3: function SpottedScript_Pages_Places_Home_View$get_placeNameLabel3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PlaceNameLabel3');
    },
    get_placeNameLabel2: function SpottedScript_Pages_Places_Home_View$get_placeNameLabel2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PlaceNameLabel2');
    },
    get_venueDataListDiv: function SpottedScript_Pages_Places_Home_View$get_venueDataListDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VenueDataListDiv');
    },
    get_noVenuesDiv: function SpottedScript_Pages_Places_Home_View$get_noVenuesDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoVenuesDiv');
    },
    get_suggestVenueLink: function SpottedScript_Pages_Places_Home_View$get_suggestVenueLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SuggestVenueLink');
    },
    get_suggestVenueLink1: function SpottedScript_Pages_Places_Home_View$get_suggestVenueLink1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SuggestVenueLink1');
    },
    get_venuesMoreLink: function SpottedScript_Pages_Places_Home_View$get_venuesMoreLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VenuesMoreLink');
    },
    get_venueDataList: function SpottedScript_Pages_Places_Home_View$get_venueDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VenueDataList');
    },
    get_venuesMorePanel: function SpottedScript_Pages_Places_Home_View$get_venuesMorePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VenuesMorePanel');
    },
    get_venuesPanel: function SpottedScript_Pages_Places_Home_View$get_venuesPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VenuesPanel');
    },
    get_genericContainerPage: function SpottedScript_Pages_Places_Home_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Places.Home.View.registerClass('SpottedScript.Pages.Places.Home.View', SpottedScript.DsiUserControl.View);
