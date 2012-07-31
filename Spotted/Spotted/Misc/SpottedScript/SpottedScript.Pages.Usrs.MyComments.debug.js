Type.registerNamespace('SpottedScript.Pages.Usrs.MyComments');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Usrs.MyComments.View
SpottedScript.Pages.Usrs.MyComments.View = function SpottedScript_Pages_Usrs_MyComments_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Usrs.MyComments.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Usrs.MyComments.View.prototype = {
    clientId: null,
    get_myChatLinksPanel: function SpottedScript_Pages_Usrs_MyComments_View$get_myChatLinksPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MyChatLinksPanel');
    },
    get_usrIntro: function SpottedScript_Pages_Usrs_MyComments_View$get_usrIntro() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrIntro');
    },
    get_cal: function SpottedScript_Pages_Usrs_MyComments_View$get_cal() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Cal');
    },
    get_panelMyComments: function SpottedScript_Pages_Usrs_MyComments_View$get_panelMyComments() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelMyComments');
    },
    get_threadsPanel: function SpottedScript_Pages_Usrs_MyComments_View$get_threadsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsPanel');
    },
    get_noThreadsPanel: function SpottedScript_Pages_Usrs_MyComments_View$get_noThreadsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoThreadsPanel');
    },
    get_threadsPageP: function SpottedScript_Pages_Usrs_MyComments_View$get_threadsPageP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsPageP');
    },
    get_threadsPageP1: function SpottedScript_Pages_Usrs_MyComments_View$get_threadsPageP1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsPageP1');
    },
    get_threadsNextPageLink: function SpottedScript_Pages_Usrs_MyComments_View$get_threadsNextPageLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsNextPageLink');
    },
    get_threadsNextPageLink1: function SpottedScript_Pages_Usrs_MyComments_View$get_threadsNextPageLink1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsNextPageLink1');
    },
    get_threadsPrevPageLink: function SpottedScript_Pages_Usrs_MyComments_View$get_threadsPrevPageLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsPrevPageLink');
    },
    get_threadsPrevPageLink1: function SpottedScript_Pages_Usrs_MyComments_View$get_threadsPrevPageLink1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsPrevPageLink1');
    },
    get_threadsDataGrid: function SpottedScript_Pages_Usrs_MyComments_View$get_threadsDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsDataGrid');
    },
    get_h12: function SpottedScript_Pages_Usrs_MyComments_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_h13: function SpottedScript_Pages_Usrs_MyComments_View$get_h13() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H13');
    },
    get_inlineScript3: function SpottedScript_Pages_Usrs_MyComments_View$get_inlineScript3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InlineScript3');
    },
    get_genericContainerPage: function SpottedScript_Pages_Usrs_MyComments_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Usrs.MyComments.View.registerClass('SpottedScript.Pages.Usrs.MyComments.View', SpottedScript.Pages.Usrs.UsrUserControl.View);
