Type.registerNamespace('SpottedScript.Pages.ChangePassword');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.ChangePassword.View
SpottedScript.Pages.ChangePassword.View = function SpottedScript_Pages_ChangePassword_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.ChangePassword.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.ChangePassword.View.prototype = {
    clientId: null,
    get_panelChange: function SpottedScript_Pages_ChangePassword_View$get_panelChange() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelChange');
    },
    get_currentPassword: function SpottedScript_Pages_ChangePassword_View$get_currentPassword() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CurrentPassword');
    },
    get_password1: function SpottedScript_Pages_ChangePassword_View$get_password1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Password1');
    },
    get_password2: function SpottedScript_Pages_ChangePassword_View$get_password2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Password2');
    },
    get_button1: function SpottedScript_Pages_ChangePassword_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_customValidator1: function SpottedScript_Pages_ChangePassword_View$get_customValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CustomValidator1');
    },
    get_requiredFieldValidator2: function SpottedScript_Pages_ChangePassword_View$get_requiredFieldValidator2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFieldValidator2');
    },
    get_requiredFieldValidator1: function SpottedScript_Pages_ChangePassword_View$get_requiredFieldValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RequiredFieldValidator1');
    },
    get_regularExpressionValidator1: function SpottedScript_Pages_ChangePassword_View$get_regularExpressionValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RegularExpressionValidator1');
    },
    get_compareValidator1: function SpottedScript_Pages_ChangePassword_View$get_compareValidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CompareValidator1');
    },
    get_panelDone: function SpottedScript_Pages_ChangePassword_View$get_panelDone() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelDone');
    },
    get_genericContainerPage: function SpottedScript_Pages_ChangePassword_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.ChangePassword.View.registerClass('SpottedScript.Pages.ChangePassword.View', SpottedScript.DsiUserControl.View);
