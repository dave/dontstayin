Type.registerNamespace('SpottedScript.Controls.DonateText.DonateTextControl');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.DonateText.DonateTextControl.View
SpottedScript.Controls.DonateText.DonateTextControl.View = function SpottedScript_Controls_DonateText_DonateTextControl_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Controls.DonateText.DonateTextControl.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Controls.DonateText.DonateTextControl.View.prototype = {
    clientId: null,
    get_uiBasic: function SpottedScript_Controls_DonateText_DonateTextControl_View$get_uiBasic() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiBasic');
    },
    get_uiDefault: function SpottedScript_Controls_DonateText_DonateTextControl_View$get_uiDefault() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiDefault');
    },
    get_uiMonkey: function SpottedScript_Controls_DonateText_DonateTextControl_View$get_uiMonkey() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiMonkey');
    },
    get_genericContainerPage: function SpottedScript_Controls_DonateText_DonateTextControl_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Controls.DonateText.DonateTextControl.View.registerClass('SpottedScript.Controls.DonateText.DonateTextControl.View', SpottedScript.DsiUserControl.View);
