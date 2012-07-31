Type.registerNamespace('SpottedScript.Pages.MyVenues');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.MyVenues.View
SpottedScript.Pages.MyVenues.View = function SpottedScript_Pages_MyVenues_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.MyVenues.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.MyVenues.View.prototype = {
    clientId: null,
    get_h11: function SpottedScript_Pages_MyVenues_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_venuesPanel: function SpottedScript_Pages_MyVenues_View$get_venuesPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VenuesPanel');
    },
    get_venuesDataGrid: function SpottedScript_Pages_MyVenues_View$get_venuesDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VenuesDataGrid');
    },
    get_genericContainerPage: function SpottedScript_Pages_MyVenues_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.MyVenues.View.registerClass('SpottedScript.Pages.MyVenues.View', SpottedScript.DsiUserControl.View);
