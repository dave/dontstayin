Type.registerNamespace('SpottedScript.Pages.Promoters.PromoterUserControl');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Promoters.PromoterUserControl.View
SpottedScript.Pages.Promoters.PromoterUserControl.View = function SpottedScript_Pages_Promoters_PromoterUserControl_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Promoters.PromoterUserControl.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Promoters.PromoterUserControl.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Pages_Promoters_PromoterUserControl_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Promoters.PromoterUserControl.View.registerClass('SpottedScript.Pages.Promoters.PromoterUserControl.View', SpottedScript.DsiUserControl.View);
