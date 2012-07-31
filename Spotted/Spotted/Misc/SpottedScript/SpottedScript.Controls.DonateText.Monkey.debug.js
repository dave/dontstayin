Type.registerNamespace('SpottedScript.Controls.DonateText.Monkey');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Controls.DonateText.Monkey.View
SpottedScript.Controls.DonateText.Monkey.View = function SpottedScript_Controls_DonateText_Monkey_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Controls.DonateText.Monkey.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Controls.DonateText.Monkey.View.prototype = {
    clientId: null,
    get_uiBasic: function SpottedScript_Controls_DonateText_Monkey_View$get_uiBasic() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiBasic');
    },
    get_uiDefault: function SpottedScript_Controls_DonateText_Monkey_View$get_uiDefault() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiDefault');
    },
    get_uiMonkey: function SpottedScript_Controls_DonateText_Monkey_View$get_uiMonkey() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiMonkey');
    },
    get_genericContainerPage: function SpottedScript_Controls_DonateText_Monkey_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Controls.DonateText.Monkey.View.registerClass('SpottedScript.Controls.DonateText.Monkey.View', SpottedScript.Controls.DonateText.DonateTextControl.View);
