Type.registerNamespace('SpottedScript.Pages.Watching');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Watching.View
SpottedScript.Pages.Watching.View = function SpottedScript_Pages_Watching_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Watching.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Watching.View.prototype = {
    clientId: null,
    get_h12: function SpottedScript_Pages_Watching_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_h13: function SpottedScript_Pages_Watching_View$get_h13() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H13');
    },
    get_inlineScript3: function SpottedScript_Pages_Watching_View$get_inlineScript3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InlineScript3');
    },
    get_panelWatching: function SpottedScript_Pages_Watching_View$get_panelWatching() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelWatching');
    },
    get_threadsPanel: function SpottedScript_Pages_Watching_View$get_threadsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsPanel');
    },
    get_noThreadsPanel: function SpottedScript_Pages_Watching_View$get_noThreadsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoThreadsPanel');
    },
    get_threadsPageP: function SpottedScript_Pages_Watching_View$get_threadsPageP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsPageP');
    },
    get_threadsPageP1: function SpottedScript_Pages_Watching_View$get_threadsPageP1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsPageP1');
    },
    get_threadsNextPageLink: function SpottedScript_Pages_Watching_View$get_threadsNextPageLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsNextPageLink');
    },
    get_threadsNextPageLink1: function SpottedScript_Pages_Watching_View$get_threadsNextPageLink1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsNextPageLink1');
    },
    get_threadsPrevPageLink: function SpottedScript_Pages_Watching_View$get_threadsPrevPageLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsPrevPageLink');
    },
    get_threadsPrevPageLink1: function SpottedScript_Pages_Watching_View$get_threadsPrevPageLink1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsPrevPageLink1');
    },
    get_threadsDataGrid: function SpottedScript_Pages_Watching_View$get_threadsDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsDataGrid');
    },
    get_genericContainerPage: function SpottedScript_Pages_Watching_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Watching.View.registerClass('SpottedScript.Pages.Watching.View', SpottedScript.DsiUserControl.View);
