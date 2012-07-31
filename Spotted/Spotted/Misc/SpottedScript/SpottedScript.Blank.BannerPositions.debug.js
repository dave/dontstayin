Type.registerNamespace('SpottedScript.Blank.BannerPositions');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.BannerPositions.View
SpottedScript.Blank.BannerPositions.View = function SpottedScript_Blank_BannerPositions_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.BannerPositions.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.BannerPositions.View.prototype = {
    clientId: null,
    get_h13dx: function SpottedScript_Blank_BannerPositions_View$get_h13dx() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H13dx');
    },
    get_genericContainerPage: function SpottedScript_Blank_BannerPositions_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.BannerPositions.View.registerClass('SpottedScript.Blank.BannerPositions.View', SpottedScript.BlankUserControl.View);
