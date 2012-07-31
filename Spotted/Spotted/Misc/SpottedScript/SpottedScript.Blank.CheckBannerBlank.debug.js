Type.registerNamespace('SpottedScript.Blank.CheckBannerBlank');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.CheckBannerBlank.View
SpottedScript.Blank.CheckBannerBlank.View = function SpottedScript_Blank_CheckBannerBlank_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.CheckBannerBlank.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.CheckBannerBlank.View.prototype = {
    clientId: null,
    get_h12: function SpottedScript_Blank_CheckBannerBlank_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_genericContainerPage: function SpottedScript_Blank_CheckBannerBlank_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.CheckBannerBlank.View.registerClass('SpottedScript.Blank.CheckBannerBlank.View', SpottedScript.BlankUserControl.View);
