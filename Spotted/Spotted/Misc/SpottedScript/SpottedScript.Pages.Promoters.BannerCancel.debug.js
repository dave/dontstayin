Type.registerNamespace('SpottedScript.Pages.Promoters.BannerCancel');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Promoters.BannerCancel.View
SpottedScript.Pages.Promoters.BannerCancel.View = function SpottedScript_Pages_Promoters_BannerCancel_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Promoters.BannerCancel.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Promoters.BannerCancel.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Pages_Promoters_BannerCancel_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Promoters.BannerCancel.View.registerClass('SpottedScript.Pages.Promoters.BannerCancel.View', SpottedScript.Pages.Promoters.PromoterUserControl.View);
