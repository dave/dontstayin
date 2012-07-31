Type.registerNamespace('SpottedScript.Pages.Promoters.BannerFolders');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Promoters.BannerFolders.View
SpottedScript.Pages.Promoters.BannerFolders.View = function SpottedScript_Pages_Promoters_BannerFolders_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Promoters.BannerFolders.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Promoters.BannerFolders.View.prototype = {
    clientId: null,
    get_promoterintro1: function SpottedScript_Pages_Promoters_BannerFolders_View$get_promoterintro1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Promoterintro1');
    },
    get_bannerListHeader: function SpottedScript_Pages_Promoters_BannerFolders_View$get_bannerListHeader() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BannerListHeader');
    },
    get_pnlContent: function SpottedScript_Pages_Promoters_BannerFolders_View$get_pnlContent() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_pnlContent');
    },
    get_uiBannerFolderRepeater: function SpottedScript_Pages_Promoters_BannerFolders_View$get_uiBannerFolderRepeater() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiBannerFolderRepeater');
    },
    get_uiPaginationControl: function SpottedScript_Pages_Promoters_BannerFolders_View$get_uiPaginationControl() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPaginationControl');
    },
    get_genericContainerPage: function SpottedScript_Pages_Promoters_BannerFolders_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Promoters.BannerFolders.View.registerClass('SpottedScript.Pages.Promoters.BannerFolders.View', SpottedScript.Pages.Promoters.PromoterUserControl.View);
