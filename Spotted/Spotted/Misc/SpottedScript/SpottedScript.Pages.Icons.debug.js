Type.registerNamespace('SpottedScript.Pages.Icons');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Icons.View
SpottedScript.Pages.Icons.View = function SpottedScript_Pages_Icons_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Icons.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Icons.View.prototype = {
    clientId: null,
    get_donateLoggedOut: function SpottedScript_Pages_Icons_View$get_donateLoggedOut() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DonateLoggedOut');
    },
    get_uiDonateText: function SpottedScript_Pages_Icons_View$get_uiDonateText() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiDonateText');
    },
    get_donateLoggedIn: function SpottedScript_Pages_Icons_View$get_donateLoggedIn() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DonateLoggedIn');
    },
    get_payment: function SpottedScript_Pages_Icons_View$get_payment() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Payment');
    },
    get_donatedMessagePanel: function SpottedScript_Pages_Icons_View$get_donatedMessagePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DonatedMessagePanel');
    },
    get_donateRemainPanel: function SpottedScript_Pages_Icons_View$get_donateRemainPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DonateRemainPanel');
    },
    get_h1: function SpottedScript_Pages_Icons_View$get_h1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1');
    },
    get_donationIconsHtml: function SpottedScript_Pages_Icons_View$get_donationIconsHtml() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DonationIconsHtml');
    },
    get_genericContainerPage: function SpottedScript_Pages_Icons_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Icons.View.registerClass('SpottedScript.Pages.Icons.View', SpottedScript.DsiUserControl.View);
