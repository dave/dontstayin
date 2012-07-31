Type.registerNamespace('SpottedScript.Blank.BannerEditMusic');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.BannerEditMusic.View
SpottedScript.Blank.BannerEditMusic.View = function SpottedScript_Blank_BannerEditMusic_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.BannerEditMusic.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.BannerEditMusic.View.prototype = {
    clientId: null,
    get_uiMusicTypesControl: function SpottedScript_Blank_BannerEditMusic_View$get_uiMusicTypesControl() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiMusicTypesControl');
    },
    get_uiSaveButton: function SpottedScript_Blank_BannerEditMusic_View$get_uiSaveButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSaveButton');
    },
    get_genericContainerPage: function SpottedScript_Blank_BannerEditMusic_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.BannerEditMusic.View.registerClass('SpottedScript.Blank.BannerEditMusic.View', SpottedScript.BlankUserControl.View);
