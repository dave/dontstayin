Type.registerNamespace('SpottedScript.Pages.Offer');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Offer.View
SpottedScript.Pages.Offer.View = function SpottedScript_Pages_Offer_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Offer.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Offer.View.prototype = {
    clientId: null,
    get_panelEnd: function SpottedScript_Pages_Offer_View$get_panelEnd() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelEnd');
    },
    get_h5: function SpottedScript_Pages_Offer_View$get_h5() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H5');
    },
    get_genericContainerPage: function SpottedScript_Pages_Offer_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Offer.View.registerClass('SpottedScript.Pages.Offer.View', SpottedScript.DsiUserControl.View);
