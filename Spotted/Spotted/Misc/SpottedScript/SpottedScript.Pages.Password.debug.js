Type.registerNamespace('SpottedScript.Pages.Password');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Password.View
SpottedScript.Pages.Password.View = function SpottedScript_Pages_Password_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Password.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Password.View.prototype = {
    clientId: null,
    get_panelPassword: function SpottedScript_Pages_Password_View$get_panelPassword() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelPassword');
    },
    get_header99: function SpottedScript_Pages_Password_View$get_header99() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Header99');
    },
    get_emailTextBox: function SpottedScript_Pages_Password_View$get_emailTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EmailTextBox');
    },
    get_button1: function SpottedScript_Pages_Password_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_errorPanel: function SpottedScript_Pages_Password_View$get_errorPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ErrorPanel');
    },
    get_donePanel: function SpottedScript_Pages_Password_View$get_donePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DonePanel');
    },
    get_panelReset: function SpottedScript_Pages_Password_View$get_panelReset() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelReset');
    },
    get_header1: function SpottedScript_Pages_Password_View$get_header1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Header1');
    },
    get_password1: function SpottedScript_Pages_Password_View$get_password1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Password1');
    },
    get_password2: function SpottedScript_Pages_Password_View$get_password2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Password2');
    },
    get_panelResetCancelled: function SpottedScript_Pages_Password_View$get_panelResetCancelled() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelResetCancelled');
    },
    get_header2: function SpottedScript_Pages_Password_View$get_header2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Header2');
    },
    get_panelResetDone: function SpottedScript_Pages_Password_View$get_panelResetDone() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelResetDone');
    },
    get_header3: function SpottedScript_Pages_Password_View$get_header3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Header3');
    },
    get_panelResetError: function SpottedScript_Pages_Password_View$get_panelResetError() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelResetError');
    },
    get_header4: function SpottedScript_Pages_Password_View$get_header4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Header4');
    },
    get_genericContainerPage: function SpottedScript_Pages_Password_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Password.View.registerClass('SpottedScript.Pages.Password.View', SpottedScript.DsiUserControl.View);
