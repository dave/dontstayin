Type.registerNamespace('SpottedScript.Pages.Home');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Home.View
SpottedScript.Pages.Home.View = function SpottedScript_Pages_Home_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Home.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Home.View.prototype = {
    clientId: null,
    get_topPhotoHolder: function SpottedScript_Pages_Home_View$get_topPhotoHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TopPhotoHolder');
    },
    get_photoLinksHolder: function SpottedScript_Pages_Home_View$get_photoLinksHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotoLinksHolder');
    },
    get_photoSoptterHolder: function SpottedScript_Pages_Home_View$get_photoSoptterHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotoSoptterHolder');
    },
    get_photoSpotterLink: function SpottedScript_Pages_Home_View$get_photoSpotterLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotoSpotterLink');
    },
    get_topPhotoArchiveHolder: function SpottedScript_Pages_Home_View$get_topPhotoArchiveHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TopPhotoArchiveHolder');
    },
    get_photoAnchor: function SpottedScript_Pages_Home_View$get_photoAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotoAnchor');
    },
    get_photoImg: function SpottedScript_Pages_Home_View$get_photoImg() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotoImg');
    },
    get_photoCaption: function SpottedScript_Pages_Home_View$get_photoCaption() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotoCaption');
    },
    get_newArticlesPanel: function SpottedScript_Pages_Home_View$get_newArticlesPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NewArticlesPanel');
    },
    get_newArticlesDataList: function SpottedScript_Pages_Home_View$get_newArticlesDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NewArticlesDataList');
    },
    get_outBox: function SpottedScript_Pages_Home_View$get_outBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OutBox');
    },
    get_exploreBox: function SpottedScript_Pages_Home_View$get_exploreBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ExploreBox');
    },
    get_spotterCode: function SpottedScript_Pages_Home_View$get_spotterCode() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterCode');
    },
    get_spotterCodeButton: function SpottedScript_Pages_Home_View$get_spotterCodeButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterCodeButton');
    },
    get_spotterCodeError: function SpottedScript_Pages_Home_View$get_spotterCodeError() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterCodeError');
    },
    get_homeContent: function SpottedScript_Pages_Home_View$get_homeContent() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_HomeContent');
    },
    get_latest: function SpottedScript_Pages_Home_View$get_latest() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Latest');
    },
    get_genericContainerPage: function SpottedScript_Pages_Home_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Home.View.registerClass('SpottedScript.Pages.Home.View', SpottedScript.DsiUserControl.View);
