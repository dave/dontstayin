Type.registerNamespace('SpottedScript.Pages.Promoters.Articles');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Promoters.Articles.View
SpottedScript.Pages.Promoters.Articles.View = function SpottedScript_Pages_Promoters_Articles_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Promoters.Articles.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Promoters.Articles.View.prototype = {
    clientId: null,
    get_h110: function SpottedScript_Pages_Promoters_Articles_View$get_h110() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H110');
    },
    get_h111: function SpottedScript_Pages_Promoters_Articles_View$get_h111() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H111');
    },
    get_promoterIntro: function SpottedScript_Pages_Promoters_Articles_View$get_promoterIntro() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PromoterIntro');
    },
    get_articlePanel: function SpottedScript_Pages_Promoters_Articles_View$get_articlePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ArticlePanel');
    },
    get_noArticlePanel: function SpottedScript_Pages_Promoters_Articles_View$get_noArticlePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoArticlePanel');
    },
    get_articleAddLink: function SpottedScript_Pages_Promoters_Articles_View$get_articleAddLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ArticleAddLink');
    },
    get_articleDataGrid: function SpottedScript_Pages_Promoters_Articles_View$get_articleDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ArticleDataGrid');
    },
    get_genericContainerPage: function SpottedScript_Pages_Promoters_Articles_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Promoters.Articles.View.registerClass('SpottedScript.Pages.Promoters.Articles.View', SpottedScript.Pages.Promoters.PromoterUserControl.View);
