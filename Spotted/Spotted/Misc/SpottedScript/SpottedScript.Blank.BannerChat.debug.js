Type.registerNamespace('SpottedScript.Blank.BannerChat');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.BannerChat.View
SpottedScript.Blank.BannerChat.View = function SpottedScript_Blank_BannerChat_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.BannerChat.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.BannerChat.View.prototype = {
    clientId: null,
    get_genericContainerPage: function SpottedScript_Blank_BannerChat_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.BannerChat.View.registerClass('SpottedScript.Blank.BannerChat.View', SpottedScript.BlankUserControl.View);
