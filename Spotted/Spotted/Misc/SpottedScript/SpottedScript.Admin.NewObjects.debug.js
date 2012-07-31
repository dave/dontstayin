Type.registerNamespace('SpottedScript.Admin.NewObjects');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Admin.NewObjects.View
SpottedScript.Admin.NewObjects.View = function SpottedScript_Admin_NewObjects_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Admin.NewObjects.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Admin.NewObjects.View.prototype = {
    clientId: null,
    get_panelUnconfirmedVenues: function SpottedScript_Admin_NewObjects_View$get_panelUnconfirmedVenues() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelUnconfirmedVenues');
    },
    get_venueRepeater: function SpottedScript_Admin_NewObjects_View$get_venueRepeater() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VenueRepeater');
    },
    get_newSpotterNumberLabel: function SpottedScript_Admin_NewObjects_View$get_newSpotterNumberLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NewSpotterNumberLabel');
    },
    get_spotterCardRequestLabel: function SpottedScript_Admin_NewObjects_View$get_spotterCardRequestLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterCardRequestLabel');
    },
    get_spotterDl: function SpottedScript_Admin_NewObjects_View$get_spotterDl() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterDl');
    },
    get_galleriesDataGrid: function SpottedScript_Admin_NewObjects_View$get_galleriesDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GalleriesDataGrid');
    },
    get_newEvents: function SpottedScript_Admin_NewObjects_View$get_newEvents() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NewEvents');
    },
    get_liveEmailBanners: function SpottedScript_Admin_NewObjects_View$get_liveEmailBanners() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LiveEmailBanners');
    },
    get_livePhotoBanners: function SpottedScript_Admin_NewObjects_View$get_livePhotoBanners() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LivePhotoBanners');
    },
    get_liveHotBoxes: function SpottedScript_Admin_NewObjects_View$get_liveHotBoxes() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LiveHotBoxes');
    },
    get_liveLeaderboards: function SpottedScript_Admin_NewObjects_View$get_liveLeaderboards() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LiveLeaderboards');
    },
    get_liveSkyscrapers: function SpottedScript_Admin_NewObjects_View$get_liveSkyscrapers() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LiveSkyscrapers');
    },
    get_paidForBanners: function SpottedScript_Admin_NewObjects_View$get_paidForBanners() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PaidForBanners');
    },
    get_notPaidForBanners: function SpottedScript_Admin_NewObjects_View$get_notPaidForBanners() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NotPaidForBanners');
    },
    get_bannedUsrDataGrid: function SpottedScript_Admin_NewObjects_View$get_bannedUsrDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BannedUsrDataGrid');
    },
    get_guestlistDataGrid: function SpottedScript_Admin_NewObjects_View$get_guestlistDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GuestlistDataGrid');
    },
    get_brandRepeater: function SpottedScript_Admin_NewObjects_View$get_brandRepeater() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BrandRepeater');
    },
    get_panelUnconfirmedBrands: function SpottedScript_Admin_NewObjects_View$get_panelUnconfirmedBrands() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelUnconfirmedBrands');
    },
    get_updateDonePanel: function SpottedScript_Admin_NewObjects_View$get_updateDonePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UpdateDonePanel');
    },
    get_genericContainerPage: function SpottedScript_Admin_NewObjects_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Admin.NewObjects.View.registerClass('SpottedScript.Admin.NewObjects.View', SpottedScript.AdminUserControl.View);
