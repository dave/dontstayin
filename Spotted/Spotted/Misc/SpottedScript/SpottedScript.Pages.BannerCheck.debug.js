Type.registerNamespace('SpottedScript.Pages.BannerCheck');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.BannerCheck.View
SpottedScript.Pages.BannerCheck.View = function SpottedScript_Pages_BannerCheck_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.BannerCheck.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.BannerCheck.View.prototype = {
    clientId: null,
    get_panelList: function SpottedScript_Pages_BannerCheck_View$get_panelList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelList');
    },
    get_miscDataGrid: function SpottedScript_Pages_BannerCheck_View$get_miscDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MiscDataGrid');
    },
    get_h11: function SpottedScript_Pages_BannerCheck_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_panelView: function SpottedScript_Pages_BannerCheck_View$get_panelView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelView');
    },
    get_h12: function SpottedScript_Pages_BannerCheck_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_viewPopupP: function SpottedScript_Pages_BannerCheck_View$get_viewPopupP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ViewPopupP');
    },
    get_viewFlashPanel: function SpottedScript_Pages_BannerCheck_View$get_viewFlashPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ViewFlashPanel');
    },
    get_viewFlashLinkTagYes: function SpottedScript_Pages_BannerCheck_View$get_viewFlashLinkTagYes() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ViewFlashLinkTagYes');
    },
    get_viewFlashLinkTagNo: function SpottedScript_Pages_BannerCheck_View$get_viewFlashLinkTagNo() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ViewFlashLinkTagNo');
    },
    get_passButton: function SpottedScript_Pages_BannerCheck_View$get_passButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PassButton');
    },
    get_failButton: function SpottedScript_Pages_BannerCheck_View$get_failButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FailButton');
    },
    get_failTextBox: function SpottedScript_Pages_BannerCheck_View$get_failTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FailTextBox');
    },
    get_genericContainerPage: function SpottedScript_Pages_BannerCheck_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.BannerCheck.View.registerClass('SpottedScript.Pages.BannerCheck.View', SpottedScript.DsiUserControl.View);
