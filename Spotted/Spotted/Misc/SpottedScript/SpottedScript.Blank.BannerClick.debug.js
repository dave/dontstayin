Type.registerNamespace('SpottedScript.Blank.BannerClick');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.BannerClick.View
SpottedScript.Blank.BannerClick.View = function SpottedScript_Blank_BannerClick_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.BannerClick.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.BannerClick.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Blank_BannerClick_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.BannerClick.View.registerClass('SpottedScript.Blank.BannerClick.View', SpottedScript.BlankUserControl.View);
