Type.registerNamespace('SpottedScript.Blank.CheckBannerSelf');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.CheckBannerSelf.View
SpottedScript.Blank.CheckBannerSelf.View = function SpottedScript_Blank_CheckBannerSelf_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.CheckBannerSelf.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.CheckBannerSelf.View.prototype = {
    clientId: null,
    get_h12: function SpottedScript_Blank_CheckBannerSelf_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_genericContainerPage: function SpottedScript_Blank_CheckBannerSelf_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.CheckBannerSelf.View.registerClass('SpottedScript.Blank.CheckBannerSelf.View', SpottedScript.BlankUserControl.View);
