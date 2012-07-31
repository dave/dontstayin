Type.registerNamespace('SpottedScript.Controls.DonateText.Default');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.DonateText.Default.View
SpottedScript.Controls.DonateText.Default.View = function SpottedScript_Controls_DonateText_Default_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Controls.DonateText.Default.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Controls.DonateText.Default.View.prototype = {
    clientId: null,
    get_uiBasic: function SpottedScript_Controls_DonateText_Default_View$get_uiBasic() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiBasic');
    },
    get_uiDefault: function SpottedScript_Controls_DonateText_Default_View$get_uiDefault() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiDefault');
    },
    get_uiMonkey: function SpottedScript_Controls_DonateText_Default_View$get_uiMonkey() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiMonkey');
    },
    get_genericContainerPage: function SpottedScript_Controls_DonateText_Default_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Controls.DonateText.Default.View.registerClass('SpottedScript.Controls.DonateText.Default.View', SpottedScript.Controls.DonateText.DonateTextControl.View);
