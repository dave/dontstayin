Type.registerNamespace('SpottedScript.Pages.ChatAdmin');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.ChatAdmin.View
SpottedScript.Pages.ChatAdmin.View = function SpottedScript_Pages_ChatAdmin_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.ChatAdmin.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.ChatAdmin.View.prototype = {
    clientId: null,
    get_adminPanel: function SpottedScript_Pages_ChatAdmin_View$get_adminPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AdminPanel');
    },
    get_panelOptions: function SpottedScript_Pages_ChatAdmin_View$get_panelOptions() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelOptions');
    },
    get_header: function SpottedScript_Pages_ChatAdmin_View$get_header() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Header');
    },
    get_threadSubjectAnchor: function SpottedScript_Pages_ChatAdmin_View$get_threadSubjectAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadSubjectAnchor');
    },
    get_threadForumAnchor: function SpottedScript_Pages_ChatAdmin_View$get_threadForumAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadForumAnchor');
    },
    get_threadGroupAnchor: function SpottedScript_Pages_ChatAdmin_View$get_threadGroupAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadGroupAnchor');
    },
    get_h12: function SpottedScript_Pages_ChatAdmin_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_closedSpan: function SpottedScript_Pages_ChatAdmin_View$get_closedSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ClosedSpan');
    },
    get_closedCheckBox: function SpottedScript_Pages_ChatAdmin_View$get_closedCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ClosedCheckBox');
    },
    get_newsSpan: function SpottedScript_Pages_ChatAdmin_View$get_newsSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NewsSpan');
    },
    get_newsCheckBox: function SpottedScript_Pages_ChatAdmin_View$get_newsCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NewsCheckBox');
    },
    get_privateSpan: function SpottedScript_Pages_ChatAdmin_View$get_privateSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PrivateSpan');
    },
    get_privateCheckBox: function SpottedScript_Pages_ChatAdmin_View$get_privateCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PrivateCheckBox');
    },
    get_sealedSpan: function SpottedScript_Pages_ChatAdmin_View$get_sealedSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SealedSpan');
    },
    get_sealedCheckBox: function SpottedScript_Pages_ChatAdmin_View$get_sealedCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SealedCheckBox');
    },
    get_button1: function SpottedScript_Pages_ChatAdmin_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_changeForumPanel: function SpottedScript_Pages_ChatAdmin_View$get_changeForumPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ChangeForumPanel');
    },
    get_h11: function SpottedScript_Pages_ChatAdmin_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_scopeEvent: function SpottedScript_Pages_ChatAdmin_View$get_scopeEvent() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ScopeEvent');
    },
    get_scopeEventFuture: function SpottedScript_Pages_ChatAdmin_View$get_scopeEventFuture() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ScopeEventFuture');
    },
    get_scopeEventAttend: function SpottedScript_Pages_ChatAdmin_View$get_scopeEventAttend() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ScopeEventAttend');
    },
    get_scopeVenue: function SpottedScript_Pages_ChatAdmin_View$get_scopeVenue() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ScopeVenue');
    },
    get_scopePlace: function SpottedScript_Pages_ChatAdmin_View$get_scopePlace() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ScopePlace');
    },
    get_scopeCountry: function SpottedScript_Pages_ChatAdmin_View$get_scopeCountry() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ScopeCountry');
    },
    get_scopeGeneral: function SpottedScript_Pages_ChatAdmin_View$get_scopeGeneral() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ScopeGeneral');
    },
    get_customvalidator3: function SpottedScript_Pages_ChatAdmin_View$get_customvalidator3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator3');
    },
    get_uiObjectMultiComplete: function SpottedScript_Pages_ChatAdmin_View$get_uiObjectMultiComplete() {
        /// <value type="ScriptSharpLibrary.HtmlAutoCompleteBehaviour"></value>
        return eval(this.clientId + '_uiObjectMultiCompleteBehaviour');
    },
    get_customvalidator4: function SpottedScript_Pages_ChatAdmin_View$get_customvalidator4() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Customvalidator4');
    },
    get_button2: function SpottedScript_Pages_ChatAdmin_View$get_button2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button2');
    },
    get_genericContainerPage: function SpottedScript_Pages_ChatAdmin_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.ChatAdmin.View.registerClass('SpottedScript.Pages.ChatAdmin.View', SpottedScript.DsiUserControl.View);
