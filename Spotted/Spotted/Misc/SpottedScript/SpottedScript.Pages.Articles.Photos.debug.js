Type.registerNamespace('SpottedScript.Pages.Articles.Photos');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Articles.Photos.Controller
SpottedScript.Pages.Articles.Photos.Controller = function SpottedScript_Pages_Articles_Photos_Controller(view) {
    /// <param name="view" type="SpottedScript.Pages.Articles.Photos.View">
    /// </param>
    /// <field name="_view$1" type="SpottedScript.Pages.Articles.Photos.View">
    /// </field>
    /// <field name="_photoProvider$1" type="SpottedScript.Controls.PhotoBrowser.PhotoProvider">
    /// </field>
    SpottedScript.Pages.Articles.Photos.Controller.initializeBase(this);
    this._view$1 = view;
    this.setupController();
}
SpottedScript.Pages.Articles.Photos.Controller.prototype = {
    _view$1: null,
    get_photoControl: function SpottedScript_Pages_Articles_Photos_Controller$get_photoControl() {
        /// <value type="SpottedScript.Controls.PhotoControl.Controller"></value>
        return this._view$1.get_uiPhotoControl();
    },
    get_photoBrowser: function SpottedScript_Pages_Articles_Photos_Controller$get_photoBrowser() {
        /// <value type="SpottedScript.Controls.PhotoBrowser.Controller"></value>
        return this._view$1.get_uiPhotoBrowser();
    },
    get_threadControl: function SpottedScript_Pages_Articles_Photos_Controller$get_threadControl() {
        /// <value type="SpottedScript.Controls.ThreadControl.Controller"></value>
        return this._view$1.get_uiThreadControl();
    },
    _photoProvider$1: null,
    get_photoProvider: function SpottedScript_Pages_Articles_Photos_Controller$get_photoProvider() {
        /// <value type="SpottedScript.Controls.PhotoBrowser.PhotoProvider"></value>
        if (this._photoProvider$1 == null) {
            this._photoProvider$1 = new SpottedScript.Controls.PhotoBrowser.ArticlePhotoProvider(Number.parseInvariant(this._view$1.get_uiArticleK().value));
        }
        return this._photoProvider$1;
    },
    get_latestChatController: function SpottedScript_Pages_Articles_Photos_Controller$get_latestChatController() {
        /// <value type="SpottedScript.Controls.LatestChat.Controller"></value>
        return this._view$1.get_uiLatestChat();
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Articles.Photos.View
SpottedScript.Pages.Articles.Photos.View = function SpottedScript_Pages_Articles_Photos_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Articles.Photos.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Articles.Photos.View.prototype = {
    clientId: null,
    get_uiTitle: function SpottedScript_Pages_Articles_Photos_View$get_uiTitle() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiTitle');
    },
    get_uiArticleInfoSpan: function SpottedScript_Pages_Articles_Photos_View$get_uiArticleInfoSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiArticleInfoSpan');
    },
    get_uiPhotoBrowser: function SpottedScript_Pages_Articles_Photos_View$get_uiPhotoBrowser() {
        /// <value type="SpottedScript.Controls.PhotoBrowser.Controller"></value>
        return eval(this.clientId + '_uiPhotoBrowserController');
    },
    get_uiPhotoControl: function SpottedScript_Pages_Articles_Photos_View$get_uiPhotoControl() {
        /// <value type="SpottedScript.Controls.PhotoControl.Controller"></value>
        return eval(this.clientId + '_uiPhotoControlController');
    },
    get_uiLatestChat: function SpottedScript_Pages_Articles_Photos_View$get_uiLatestChat() {
        /// <value type="SpottedScript.Controls.LatestChat.Controller"></value>
        return eval(this.clientId + '_uiLatestChatController');
    },
    get_uiThreadControl: function SpottedScript_Pages_Articles_Photos_View$get_uiThreadControl() {
        /// <value type="SpottedScript.Controls.ThreadControl.Controller"></value>
        return eval(this.clientId + '_uiThreadControlController');
    },
    get_uiArticleK: function SpottedScript_Pages_Articles_Photos_View$get_uiArticleK() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiArticleK');
    },
    get_genericContainerPage: function SpottedScript_Pages_Articles_Photos_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Articles.Photos.Controller.registerClass('SpottedScript.Pages.Articles.Photos.Controller', SpottedScript.Controls.PhotoBrowser.PhotosController);
SpottedScript.Pages.Articles.Photos.View.registerClass('SpottedScript.Pages.Articles.Photos.View', SpottedScript.DsiUserControl.View);
