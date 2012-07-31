Type.registerNamespace('SpottedScript.Pages.Groups.TopPhotos');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Groups.TopPhotos.View
SpottedScript.Pages.Groups.TopPhotos.View = function SpottedScript_Pages_Groups_TopPhotos_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Groups.TopPhotos.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Groups.TopPhotos.View.prototype = {
    clientId: null,
    get_panelItems: function SpottedScript_Pages_Groups_TopPhotos_View$get_panelItems() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelItems');
    },
    get_noItemsPanel: function SpottedScript_Pages_Groups_TopPhotos_View$get_noItemsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoItemsPanel');
    },
    get_h12: function SpottedScript_Pages_Groups_TopPhotos_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_itemsPanel: function SpottedScript_Pages_Groups_TopPhotos_View$get_itemsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ItemsPanel');
    },
    get_h13: function SpottedScript_Pages_Groups_TopPhotos_View$get_h13() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H13');
    },
    get_pageP: function SpottedScript_Pages_Groups_TopPhotos_View$get_pageP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PageP');
    },
    get_prevPageLink: function SpottedScript_Pages_Groups_TopPhotos_View$get_prevPageLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PrevPageLink');
    },
    get_nextPageLink: function SpottedScript_Pages_Groups_TopPhotos_View$get_nextPageLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NextPageLink');
    },
    get_dataList: function SpottedScript_Pages_Groups_TopPhotos_View$get_dataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DataList');
    },
    get_pageP1: function SpottedScript_Pages_Groups_TopPhotos_View$get_pageP1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PageP1');
    },
    get_prevPageLink1: function SpottedScript_Pages_Groups_TopPhotos_View$get_prevPageLink1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PrevPageLink1');
    },
    get_nextPageLink1: function SpottedScript_Pages_Groups_TopPhotos_View$get_nextPageLink1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NextPageLink1');
    },
    get_groupIntro: function SpottedScript_Pages_Groups_TopPhotos_View$get_groupIntro() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GroupIntro');
    },
    get_cal: function SpottedScript_Pages_Groups_TopPhotos_View$get_cal() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Cal');
    },
    get_genericContainerPage: function SpottedScript_Pages_Groups_TopPhotos_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Groups.TopPhotos.View.registerClass('SpottedScript.Pages.Groups.TopPhotos.View', SpottedScript.DsiUserControl.View);
