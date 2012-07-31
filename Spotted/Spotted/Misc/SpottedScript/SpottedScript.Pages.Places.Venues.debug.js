Type.registerNamespace('SpottedScript.Pages.Places.Venues');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Places.Venues.View
SpottedScript.Pages.Places.Venues.View = function SpottedScript_Pages_Places_Venues_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Places.Venues.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Places.Venues.View.prototype = {
    clientId: null,
    get_allVenuesPanel: function SpottedScript_Pages_Places_Venues_View$get_allVenuesPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AllVenuesPanel');
    },
    get_largeRegularVenuePanel: function SpottedScript_Pages_Places_Venues_View$get_largeRegularVenuePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LargeRegularVenuePanel');
    },
    get_mediumRegularVenuePanel: function SpottedScript_Pages_Places_Venues_View$get_mediumRegularVenuePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MediumRegularVenuePanel');
    },
    get_smallRegularVenuePanel: function SpottedScript_Pages_Places_Venues_View$get_smallRegularVenuePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SmallRegularVenuePanel');
    },
    get_notRegularVenuePanel: function SpottedScript_Pages_Places_Venues_View$get_notRegularVenuePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NotRegularVenuePanel');
    },
    get_largeRegularVenueDataList: function SpottedScript_Pages_Places_Venues_View$get_largeRegularVenueDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LargeRegularVenueDataList');
    },
    get_mediumRegularVenueDataList: function SpottedScript_Pages_Places_Venues_View$get_mediumRegularVenueDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MediumRegularVenueDataList');
    },
    get_smallRegularVenueDataList: function SpottedScript_Pages_Places_Venues_View$get_smallRegularVenueDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SmallRegularVenueDataList');
    },
    get_notRegularVenueDataList: function SpottedScript_Pages_Places_Venues_View$get_notRegularVenueDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NotRegularVenueDataList');
    },
    get_allVenuesPlaceLink: function SpottedScript_Pages_Places_Venues_View$get_allVenuesPlaceLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AllVenuesPlaceLink');
    },
    get_pageHeadingAllVenues: function SpottedScript_Pages_Places_Venues_View$get_pageHeadingAllVenues() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PageHeadingAllVenues');
    },
    get_allVenuesPanelNoVenues: function SpottedScript_Pages_Places_Venues_View$get_allVenuesPanelNoVenues() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AllVenuesPanelNoVenues');
    },
    get_genericContainerPage: function SpottedScript_Pages_Places_Venues_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Places.Venues.View.registerClass('SpottedScript.Pages.Places.Venues.View', SpottedScript.DsiUserControl.View);
