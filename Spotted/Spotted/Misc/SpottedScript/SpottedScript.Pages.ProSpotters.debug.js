Type.registerNamespace('SpottedScript.Pages.ProSpotters');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.ProSpotters.View
SpottedScript.Pages.ProSpotters.View = function SpottedScript_Pages_ProSpotters_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.ProSpotters.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.ProSpotters.View.prototype = {
    clientId: null,
    get_panelProSpotters: function SpottedScript_Pages_ProSpotters_View$get_panelProSpotters() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelProSpotters');
    },
    get_h11: function SpottedScript_Pages_ProSpotters_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_proSpottersDataList: function SpottedScript_Pages_ProSpotters_View$get_proSpottersDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ProSpottersDataList');
    },
    get_genericContainerPage: function SpottedScript_Pages_ProSpotters_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.ProSpotters.View.registerClass('SpottedScript.Pages.ProSpotters.View', SpottedScript.DsiUserControl.View);
