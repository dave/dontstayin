Type.registerNamespace('SpottedScript.Pages.Favourites');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Favourites.View
SpottedScript.Pages.Favourites.View = function SpottedScript_Pages_Favourites_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Favourites.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Favourites.View.prototype = {
    clientId: null,
    get_panelFavourites: function SpottedScript_Pages_Favourites_View$get_panelFavourites() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelFavourites');
    },
    get_threadsPanel: function SpottedScript_Pages_Favourites_View$get_threadsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsPanel');
    },
    get_noThreadsPanel: function SpottedScript_Pages_Favourites_View$get_noThreadsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoThreadsPanel');
    },
    get_threadsPageP: function SpottedScript_Pages_Favourites_View$get_threadsPageP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsPageP');
    },
    get_threadsPageP1: function SpottedScript_Pages_Favourites_View$get_threadsPageP1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsPageP1');
    },
    get_threadsNextPageLink: function SpottedScript_Pages_Favourites_View$get_threadsNextPageLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsNextPageLink');
    },
    get_threadsNextPageLink1: function SpottedScript_Pages_Favourites_View$get_threadsNextPageLink1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsNextPageLink1');
    },
    get_threadsPrevPageLink: function SpottedScript_Pages_Favourites_View$get_threadsPrevPageLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsPrevPageLink');
    },
    get_threadsPrevPageLink1: function SpottedScript_Pages_Favourites_View$get_threadsPrevPageLink1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsPrevPageLink1');
    },
    get_threadsDataGrid: function SpottedScript_Pages_Favourites_View$get_threadsDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsDataGrid');
    },
    get_threadsPageLinksP: function SpottedScript_Pages_Favourites_View$get_threadsPageLinksP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsPageLinksP');
    },
    get_threadsPageLinksP1: function SpottedScript_Pages_Favourites_View$get_threadsPageLinksP1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsPageLinksP1');
    },
    get_h12: function SpottedScript_Pages_Favourites_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_h13: function SpottedScript_Pages_Favourites_View$get_h13() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H13');
    },
    get_inlineScript3: function SpottedScript_Pages_Favourites_View$get_inlineScript3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InlineScript3');
    },
    get_genericContainerPage: function SpottedScript_Pages_Favourites_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Favourites.View.registerClass('SpottedScript.Pages.Favourites.View', SpottedScript.DsiUserControl.View);
