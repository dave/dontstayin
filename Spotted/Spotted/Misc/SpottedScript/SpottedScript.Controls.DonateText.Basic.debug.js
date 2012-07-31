Type.registerNamespace('SpottedScript.Controls.DonateText.Basic');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.DonateText.Basic.View
SpottedScript.Controls.DonateText.Basic.View = function SpottedScript_Controls_DonateText_Basic_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Controls.DonateText.Basic.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Controls.DonateText.Basic.View.prototype = {
    clientId: null,
    get_uiBasic: function SpottedScript_Controls_DonateText_Basic_View$get_uiBasic() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiBasic');
    },
    get_uiDefault: function SpottedScript_Controls_DonateText_Basic_View$get_uiDefault() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiDefault');
    },
    get_uiMonkey: function SpottedScript_Controls_DonateText_Basic_View$get_uiMonkey() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiMonkey');
    },
    get_genericContainerPage: function SpottedScript_Controls_DonateText_Basic_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Controls.DonateText.Basic.View.registerClass('SpottedScript.Controls.DonateText.Basic.View', SpottedScript.Controls.DonateText.DonateTextControl.View);
