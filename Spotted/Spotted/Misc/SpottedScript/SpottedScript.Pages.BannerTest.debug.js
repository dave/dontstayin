Type.registerNamespace('SpottedScript.Pages.BannerTest');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.BannerTest.View
SpottedScript.Pages.BannerTest.View = function SpottedScript_Pages_BannerTest_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.BannerTest.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.BannerTest.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Pages_BannerTest_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.BannerTest.View.registerClass('SpottedScript.Pages.BannerTest.View', SpottedScript.DsiUserControl.View);
