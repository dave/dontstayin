Type.registerNamespace('SpottedScript.Pages.Regions.Home');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Regions.Home.View
SpottedScript.Pages.Regions.Home.View = function SpottedScript_Pages_Regions_Home_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Regions.Home.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Regions.Home.View.prototype = {
    clientId: null,
    get_panel1: function SpottedScript_Pages_Regions_Home_View$get_panel1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Panel1');
    },
    get_pageHeadingNoPlace: function SpottedScript_Pages_Regions_Home_View$get_pageHeadingNoPlace() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PageHeadingNoPlace');
    },
    get_noPlaceSelectedFlagAnchor: function SpottedScript_Pages_Regions_Home_View$get_noPlaceSelectedFlagAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoPlaceSelectedFlagAnchor');
    },
    get_noPlaceSelectedFlagImg: function SpottedScript_Pages_Regions_Home_View$get_noPlaceSelectedFlagImg() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoPlaceSelectedFlagImg');
    },
    get_regionNameLabel: function SpottedScript_Pages_Regions_Home_View$get_regionNameLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RegionNameLabel');
    },
    get_noPlaceSelectedCountryLink: function SpottedScript_Pages_Regions_Home_View$get_noPlaceSelectedCountryLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoPlaceSelectedCountryLink');
    },
    get_sizeOrderLink: function SpottedScript_Pages_Regions_Home_View$get_sizeOrderLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SizeOrderLink');
    },
    get_nameOrderLink: function SpottedScript_Pages_Regions_Home_View$get_nameOrderLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NameOrderLink');
    },
    get_latitudeOrderLink: function SpottedScript_Pages_Regions_Home_View$get_latitudeOrderLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LatitudeOrderLink');
    },
    get_longitudeOrderLink: function SpottedScript_Pages_Regions_Home_View$get_longitudeOrderLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LongitudeOrderLink');
    },
    get_eventOrderLink: function SpottedScript_Pages_Regions_Home_View$get_eventOrderLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventOrderLink');
    },
    get_placesDataList: function SpottedScript_Pages_Regions_Home_View$get_placesDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PlacesDataList');
    },
    get_genericContainerPage: function SpottedScript_Pages_Regions_Home_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Regions.Home.View.registerClass('SpottedScript.Pages.Regions.Home.View', SpottedScript.DsiUserControl.View);
