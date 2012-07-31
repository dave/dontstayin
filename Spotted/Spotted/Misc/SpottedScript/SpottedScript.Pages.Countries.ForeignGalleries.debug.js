Type.registerNamespace('SpottedScript.Pages.Countries.ForeignGalleries');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Countries.ForeignGalleries.View
SpottedScript.Pages.Countries.ForeignGalleries.View = function SpottedScript_Pages_Countries_ForeignGalleries_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Countries.ForeignGalleries.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Countries.ForeignGalleries.View.prototype = {
    clientId: null,
    get_panelItems: function SpottedScript_Pages_Countries_ForeignGalleries_View$get_panelItems() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelItems');
    },
    get_hdgfd12: function SpottedScript_Pages_Countries_ForeignGalleries_View$get_hdgfd12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Hdgfd12');
    },
    get_cal: function SpottedScript_Pages_Countries_ForeignGalleries_View$get_cal() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Cal');
    },
    get_noItemsPanel: function SpottedScript_Pages_Countries_ForeignGalleries_View$get_noItemsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoItemsPanel');
    },
    get_h12: function SpottedScript_Pages_Countries_ForeignGalleries_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_itemsPanel: function SpottedScript_Pages_Countries_ForeignGalleries_View$get_itemsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ItemsPanel');
    },
    get_h13: function SpottedScript_Pages_Countries_ForeignGalleries_View$get_h13() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H13');
    },
    get_pageP: function SpottedScript_Pages_Countries_ForeignGalleries_View$get_pageP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PageP');
    },
    get_prevPageLink: function SpottedScript_Pages_Countries_ForeignGalleries_View$get_prevPageLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PrevPageLink');
    },
    get_nextPageLink: function SpottedScript_Pages_Countries_ForeignGalleries_View$get_nextPageLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NextPageLink');
    },
    get_dataList: function SpottedScript_Pages_Countries_ForeignGalleries_View$get_dataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DataList');
    },
    get_pageP1: function SpottedScript_Pages_Countries_ForeignGalleries_View$get_pageP1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PageP1');
    },
    get_prevPageLink1: function SpottedScript_Pages_Countries_ForeignGalleries_View$get_prevPageLink1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PrevPageLink1');
    },
    get_nextPageLink1: function SpottedScript_Pages_Countries_ForeignGalleries_View$get_nextPageLink1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NextPageLink1');
    },
    get_genericContainerPage: function SpottedScript_Pages_Countries_ForeignGalleries_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Countries.ForeignGalleries.View.registerClass('SpottedScript.Pages.Countries.ForeignGalleries.View', SpottedScript.DsiUserControl.View);
