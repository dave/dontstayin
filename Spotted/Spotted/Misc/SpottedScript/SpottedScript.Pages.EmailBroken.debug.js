Type.registerNamespace('SpottedScript.Pages.EmailBroken');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.EmailBroken.View
SpottedScript.Pages.EmailBroken.View = function SpottedScript_Pages_EmailBroken_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.EmailBroken.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.EmailBroken.View.prototype = {
    clientId: null,
    get_emailBrokenPanel: function SpottedScript_Pages_EmailBroken_View$get_emailBrokenPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EmailBrokenPanel');
    },
    get_linkButton1: function SpottedScript_Pages_EmailBroken_View$get_linkButton1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_LinkButton1');
    },
    get_doneP: function SpottedScript_Pages_EmailBroken_View$get_doneP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DoneP');
    },
    get_emailNotBrokenPanel: function SpottedScript_Pages_EmailBroken_View$get_emailNotBrokenPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EmailNotBrokenPanel');
    },
    get_h1: function SpottedScript_Pages_EmailBroken_View$get_h1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1');
    },
    get_genericContainerPage: function SpottedScript_Pages_EmailBroken_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.EmailBroken.View.registerClass('SpottedScript.Pages.EmailBroken.View', SpottedScript.DsiUserControl.View);
