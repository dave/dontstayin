Type.registerNamespace('SpottedScript.Pages.Venues.Home');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Venues.Home.View
SpottedScript.Pages.Venues.Home.View = function SpottedScript_Pages_Venues_Home_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Venues.Home.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Venues.Home.View.prototype = {
    clientId: null,
    get_venueHeader: function SpottedScript_Pages_Venues_Home_View$get_venueHeader() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VenueHeader');
    },
    get_venueHeader1: function SpottedScript_Pages_Venues_Home_View$get_venueHeader1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VenueHeader1');
    },
    get_venueSelectedPanel: function SpottedScript_Pages_Venues_Home_View$get_venueSelectedPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VenueSelectedPanel');
    },
    get_venuePicImg: function SpottedScript_Pages_Venues_Home_View$get_venuePicImg() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VenuePicImg');
    },
    get_venuePicCell: function SpottedScript_Pages_Venues_Home_View$get_venuePicCell() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VenuePicCell');
    },
    get_discussionLink: function SpottedScript_Pages_Venues_Home_View$get_discussionLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DiscussionLink');
    },
    get_mapSpan: function SpottedScript_Pages_Venues_Home_View$get_mapSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MapSpan');
    },
    get_mapLink: function SpottedScript_Pages_Venues_Home_View$get_mapLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MapLink');
    },
    get_directionsLink: function SpottedScript_Pages_Venues_Home_View$get_directionsLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DirectionsLink');
    },
    get_discussionLinkVenueLabel: function SpottedScript_Pages_Venues_Home_View$get_discussionLinkVenueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DiscussionLinkVenueLabel');
    },
    get_calendarLinkVenueLabel: function SpottedScript_Pages_Venues_Home_View$get_calendarLinkVenueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CalendarLinkVenueLabel');
    },
    get_infoPanel: function SpottedScript_Pages_Venues_Home_View$get_infoPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InfoPanel');
    },
    get_eventBodyTitle: function SpottedScript_Pages_Venues_Home_View$get_eventBodyTitle() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventBodyTitle');
    },
    get_musicTypeLabel: function SpottedScript_Pages_Venues_Home_View$get_musicTypeLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MusicTypeLabel');
    },
    get_latest: function SpottedScript_Pages_Venues_Home_View$get_latest() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Latest');
    },
    get_discussionLinkCommentsLabel: function SpottedScript_Pages_Venues_Home_View$get_discussionLinkCommentsLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DiscussionLinkCommentsLabel');
    },
    get_upcomingAllEventsPanel: function SpottedScript_Pages_Venues_Home_View$get_upcomingAllEventsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UpcomingAllEventsPanel');
    },
    get_previousAllEventsPanel: function SpottedScript_Pages_Venues_Home_View$get_previousAllEventsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PreviousAllEventsPanel');
    },
    get_venueNameLabel: function SpottedScript_Pages_Venues_Home_View$get_venueNameLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VenueNameLabel');
    },
    get_placeNameLink: function SpottedScript_Pages_Venues_Home_View$get_placeNameLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PlaceNameLink');
    },
    get_calendarLink: function SpottedScript_Pages_Venues_Home_View$get_calendarLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CalendarLink');
    },
    get_h12: function SpottedScript_Pages_Venues_Home_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_div1: function SpottedScript_Pages_Venues_Home_View$get_div1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Div1');
    },
    get_hotTicketsLink: function SpottedScript_Pages_Venues_Home_View$get_hotTicketsLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_HotTicketsLink');
    },
    get_hotTicketsLinkVenueLabel: function SpottedScript_Pages_Venues_Home_View$get_hotTicketsLinkVenueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_HotTicketsLinkVenueLabel');
    },
    get_ticketsLink: function SpottedScript_Pages_Venues_Home_View$get_ticketsLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketsLink');
    },
    get_ticketsLinkVenueLabel: function SpottedScript_Pages_Venues_Home_View$get_ticketsLinkVenueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketsLinkVenueLabel');
    },
    get_freeGuestlistLink: function SpottedScript_Pages_Venues_Home_View$get_freeGuestlistLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FreeGuestlistLink');
    },
    get_freeGuestlistLinkVenueLabel: function SpottedScript_Pages_Venues_Home_View$get_freeGuestlistLinkVenueLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FreeGuestlistLinkVenueLabel');
    },
    get_panel1: function SpottedScript_Pages_Venues_Home_View$get_panel1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Panel1');
    },
    get_h11: function SpottedScript_Pages_Venues_Home_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_genericContainerPage: function SpottedScript_Pages_Venues_Home_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Venues.Home.View.registerClass('SpottedScript.Pages.Venues.Home.View', SpottedScript.DsiUserControl.View);
