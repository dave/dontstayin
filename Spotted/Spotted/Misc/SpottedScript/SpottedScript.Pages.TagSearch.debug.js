Type.registerNamespace('SpottedScript.Pages.TagSearch');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.TagSearch.Controller
SpottedScript.Pages.TagSearch.Controller = function SpottedScript_Pages_TagSearch_Controller(view) {
    /// <param name="view" type="SpottedScript.Pages.TagSearch.View">
    /// </param>
    /// <field name="_view$1" type="SpottedScript.Pages.TagSearch.View">
    /// </field>
    /// <field name="_photoProvider$1" type="SpottedScript.Controls.PhotoBrowser.PhotoProvider">
    /// </field>
    SpottedScript.Pages.TagSearch.Controller.initializeBase(this);
    this._view$1 = view;
    this.setupController();
}
SpottedScript.Pages.TagSearch.Controller.prototype = {
    _view$1: null,
    get_photoControl: function SpottedScript_Pages_TagSearch_Controller$get_photoControl() {
        /// <value type="SpottedScript.Controls.PhotoControl.Controller"></value>
        return this._view$1.get_uiPhotoControl();
    },
    get_photoBrowser: function SpottedScript_Pages_TagSearch_Controller$get_photoBrowser() {
        /// <value type="SpottedScript.Controls.PhotoBrowser.Controller"></value>
        return this._view$1.get_uiPhotoBrowser();
    },
    get_threadControl: function SpottedScript_Pages_TagSearch_Controller$get_threadControl() {
        /// <value type="SpottedScript.Controls.ThreadControl.Controller"></value>
        return this._view$1.get_uiThreadControl();
    },
    _photoProvider$1: null,
    get_photoProvider: function SpottedScript_Pages_TagSearch_Controller$get_photoProvider() {
        /// <value type="SpottedScript.Controls.PhotoBrowser.PhotoProvider"></value>
        if (this._photoProvider$1 == null) {
            this._photoProvider$1 = new SpottedScript.Controls.PhotoBrowser.TagPhotoProvider(Number.parseInvariant(this._view$1.get_uiTagK().value));
        }
        return this._photoProvider$1;
    },
    get_latestChatController: function SpottedScript_Pages_TagSearch_Controller$get_latestChatController() {
        /// <value type="SpottedScript.Controls.LatestChat.Controller"></value>
        return this._view$1.get_uiLatestChat();
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.TagSearch.View
SpottedScript.Pages.TagSearch.View = function SpottedScript_Pages_TagSearch_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.TagSearch.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.TagSearch.View.prototype = {
    clientId: null,
    get_uiTitle: function SpottedScript_Pages_TagSearch_View$get_uiTitle() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiTitle');
    },
    get_uiTagCloud: function SpottedScript_Pages_TagSearch_View$get_uiTagCloud() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiTagCloud');
    },
    get_uiSearchBoxPanel: function SpottedScript_Pages_TagSearch_View$get_uiSearchBoxPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSearchBoxPanel');
    },
    get_uiSearchBoxControl: function SpottedScript_Pages_TagSearch_View$get_uiSearchBoxControl() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiSearchBoxControl');
    },
    get_uiPhotoBrowser: function SpottedScript_Pages_TagSearch_View$get_uiPhotoBrowser() {
        /// <value type="SpottedScript.Controls.PhotoBrowser.Controller"></value>
        return eval(this.clientId + '_uiPhotoBrowserController');
    },
    get_uiPhotoControl: function SpottedScript_Pages_TagSearch_View$get_uiPhotoControl() {
        /// <value type="SpottedScript.Controls.PhotoControl.Controller"></value>
        return eval(this.clientId + '_uiPhotoControlController');
    },
    get_uiLatestChat: function SpottedScript_Pages_TagSearch_View$get_uiLatestChat() {
        /// <value type="SpottedScript.Controls.LatestChat.Controller"></value>
        return eval(this.clientId + '_uiLatestChatController');
    },
    get_uiThreadControl: function SpottedScript_Pages_TagSearch_View$get_uiThreadControl() {
        /// <value type="SpottedScript.Controls.ThreadControl.Controller"></value>
        return eval(this.clientId + '_uiThreadControlController');
    },
    get_uiTagK: function SpottedScript_Pages_TagSearch_View$get_uiTagK() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiTagK');
    },
    get_genericContainerPage: function SpottedScript_Pages_TagSearch_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.TagSearch.Controller.registerClass('SpottedScript.Pages.TagSearch.Controller', SpottedScript.Controls.PhotoBrowser.PhotosController);
SpottedScript.Pages.TagSearch.View.registerClass('SpottedScript.Pages.TagSearch.View', SpottedScript.DsiUserControl.View);
