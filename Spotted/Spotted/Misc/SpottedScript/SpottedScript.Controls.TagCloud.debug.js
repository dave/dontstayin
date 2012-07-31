Type.registerNamespace('SpottedScript.Controls.TagCloud');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.TagCloud.View
SpottedScript.Controls.TagCloud.View = function SpottedScript_Controls_TagCloud_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Controls.TagCloud.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Controls.TagCloud.View.prototype = {
    clientId: null,
    get_uiTitle: function SpottedScript_Controls_TagCloud_View$get_uiTitle() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiTitle');
    },
    get_uiPanel: function SpottedScript_Controls_TagCloud_View$get_uiPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPanel');
    },
    get_uiSearchControl: function SpottedScript_Controls_TagCloud_View$get_uiSearchControl() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSearchControl');
    },
    get_uiLinkCloud: function SpottedScript_Controls_TagCloud_View$get_uiLinkCloud() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiLinkCloud');
    },
    get_genericContainerPage: function SpottedScript_Controls_TagCloud_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Controls.TagCloud.View.registerClass('SpottedScript.Controls.TagCloud.View', SpottedScript.DsiUserControl.View);
