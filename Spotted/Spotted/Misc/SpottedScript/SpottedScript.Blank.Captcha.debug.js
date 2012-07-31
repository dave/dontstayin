Type.registerNamespace('SpottedScript.Blank.Captcha');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Blank.Captcha.View
SpottedScript.Blank.Captcha.View = function SpottedScript_Blank_Captcha_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Blank.Captcha.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Blank.Captcha.View.prototype = {
    clientId: null,
    get_hipImage: function SpottedScript_Blank_Captcha_View$get_hipImage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_HipImage');
    },
    get_hipChallengeTextBox: function SpottedScript_Blank_Captcha_View$get_hipChallengeTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_HipChallengeTextBox');
    },
    get_doneButton: function SpottedScript_Blank_Captcha_View$get_doneButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DoneButton');
    },
    get_customvalidator10: function SpottedScript_Blank_Captcha_View$get_customvalidator10() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator10');
    },
    get_genericContainerPage: function SpottedScript_Blank_Captcha_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Blank.Captcha.View.registerClass('SpottedScript.Blank.Captcha.View', SpottedScript.BlankUserControl.View);
