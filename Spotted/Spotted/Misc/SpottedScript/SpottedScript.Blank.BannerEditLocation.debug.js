Type.registerNamespace('SpottedScript.Blank.BannerEditLocation');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.BannerEditLocation.View
SpottedScript.Blank.BannerEditLocation.View = function SpottedScript_Blank_BannerEditLocation_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.BannerEditLocation.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.BannerEditLocation.View.prototype = {
    clientId: null,
    get_h12: function SpottedScript_Blank_BannerEditLocation_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_uiPlacesChooser: function SpottedScript_Blank_BannerEditLocation_View$get_uiPlacesChooser() {
        /// <value type="SpottedScript.Controls.PlacesChooser.Controller"></value>
        return eval(this.clientId + '_uiPlacesChooserController');
    },
    get_genericContainerPage: function SpottedScript_Blank_BannerEditLocation_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.BannerEditLocation.View.registerClass('SpottedScript.Blank.BannerEditLocation.View', SpottedScript.BlankUserControl.View);
