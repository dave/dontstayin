Type.registerNamespace('SpottedScript.Pages.Promoters.Banners');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Promoters.Banners.View
SpottedScript.Pages.Promoters.Banners.View = function SpottedScript_Pages_Promoters_Banners_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Promoters.Banners.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Promoters.Banners.View.prototype = {
    clientId: null,
    get_panelBannerList: function SpottedScript_Pages_Promoters_Banners_View$get_panelBannerList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelBannerList');
    },
    get_bannerListHeader: function SpottedScript_Pages_Promoters_Banners_View$get_bannerListHeader() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BannerListHeader');
    },
    get_bannerListAddLink: function SpottedScript_Pages_Promoters_Banners_View$get_bannerListAddLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BannerListAddLink');
    },
    get_bannerListDataGrid: function SpottedScript_Pages_Promoters_Banners_View$get_bannerListDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BannerListDataGrid');
    },
    get_promoterintro1: function SpottedScript_Pages_Promoters_Banners_View$get_promoterintro1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Promoterintro1');
    },
    get_folderDropDown: function SpottedScript_Pages_Promoters_Banners_View$get_folderDropDown() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FolderDropDown');
    },
    get_genericContainerPage: function SpottedScript_Pages_Promoters_Banners_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Promoters.Banners.View.registerClass('SpottedScript.Pages.Promoters.Banners.View', SpottedScript.Pages.Promoters.PromoterUserControl.View);
