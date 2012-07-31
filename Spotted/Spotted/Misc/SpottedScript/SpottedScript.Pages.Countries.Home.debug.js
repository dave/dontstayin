Type.registerNamespace('SpottedScript.Pages.Countries.Home');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Countries.Home.View
SpottedScript.Pages.Countries.Home.View = function SpottedScript_Pages_Countries_Home_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Countries.Home.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Countries.Home.View.prototype = {
    clientId: null,
    get_homeContentTopUc: function SpottedScript_Pages_Countries_Home_View$get_homeContentTopUc() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_HomeContentTopUc');
    },
    get_latest: function SpottedScript_Pages_Countries_Home_View$get_latest() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Latest');
    },
    get_genericContainerPage: function SpottedScript_Pages_Countries_Home_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Countries.Home.View.registerClass('SpottedScript.Pages.Countries.Home.View', SpottedScript.DsiUserControl.View);
