Type.registerNamespace('SpottedScript.Pages.MixmagRss');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.MixmagRss.View
SpottedScript.Pages.MixmagRss.View = function SpottedScript_Pages_MixmagRss_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.MixmagRss.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.MixmagRss.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Pages_MixmagRss_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.MixmagRss.View.registerClass('SpottedScript.Pages.MixmagRss.View', SpottedScript.DsiUserControl.View);
