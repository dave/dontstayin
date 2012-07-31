Type.registerNamespace('SpottedScript.Pages.Promoters.Offers');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Promoters.Offers.View
SpottedScript.Pages.Promoters.Offers.View = function SpottedScript_Pages_Promoters_Offers_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Promoters.Offers.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Promoters.Offers.View.prototype = {
    clientId: null,
    get_h12: function SpottedScript_Pages_Promoters_Offers_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_genericContainerPage: function SpottedScript_Pages_Promoters_Offers_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Promoters.Offers.View.registerClass('SpottedScript.Pages.Promoters.Offers.View', SpottedScript.DsiUserControl.View);
