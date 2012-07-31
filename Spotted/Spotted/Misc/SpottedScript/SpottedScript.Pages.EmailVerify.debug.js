Type.registerNamespace('SpottedScript.Pages.EmailVerify');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.EmailVerify.View
SpottedScript.Pages.EmailVerify.View = function SpottedScript_Pages_EmailVerify_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.EmailVerify.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.EmailVerify.View.prototype = {
    clientId: null,
    get_h11: function SpottedScript_Pages_EmailVerify_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_h12: function SpottedScript_Pages_EmailVerify_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_enableCommsPanel: function SpottedScript_Pages_EmailVerify_View$get_enableCommsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_enableCommsPanel');
    },
    get_disableCommsPanel: function SpottedScript_Pages_EmailVerify_View$get_disableCommsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_disableCommsPanel');
    },
    get_emailSentP: function SpottedScript_Pages_EmailVerify_View$get_emailSentP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_emailSentP');
    },
    get_linkButton1: function SpottedScript_Pages_EmailVerify_View$get_linkButton1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinkButton1');
    },
    get_panelError: function SpottedScript_Pages_EmailVerify_View$get_panelError() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelError');
    },
    get_enableCommsH1: function SpottedScript_Pages_EmailVerify_View$get_enableCommsH1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_enableCommsH1');
    },
    get_genericContainerPage: function SpottedScript_Pages_EmailVerify_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.EmailVerify.View.registerClass('SpottedScript.Pages.EmailVerify.View', SpottedScript.DsiUserControl.View);
