//! Photos.debug.js
//

(function($) {

Type.registerNamespace('Js.Pages.Articles.Photos');

////////////////////////////////////////////////////////////////////////////////
// Js.Pages.Articles.Photos.Controller

Js.Pages.Articles.Photos.Controller = function Js_Pages_Articles_Photos_Controller(view) {
    /// <param name="view" type="Js.Pages.Articles.Photos.View">
    /// </param>
    /// <field name="_view$1" type="Js.Pages.Articles.Photos.View">
    /// </field>
    /// <field name="_photoProvider$1" type="Js.Controls.PhotoBrowser.PhotoProvider">
    /// </field>
    Js.Pages.Articles.Photos.Controller.initializeBase(this);
    this._view$1 = view;
    this.setupController();
}
Js.Pages.Articles.Photos.Controller.prototype = {
    _view$1: null,
    
    get_photoControl: function Js_Pages_Articles_Photos_Controller$get_photoControl() {
        /// <value type="Js.Controls.PhotoControl.Controller"></value>
        return this._view$1.get_uiPhotoControl();
    },
    
    get_photoBrowser: function Js_Pages_Articles_Photos_Controller$get_photoBrowser() {
        /// <value type="Js.Controls.PhotoBrowser.Controller"></value>
        return this._view$1.get_uiPhotoBrowser();
    },
    
    get_threadControl: function Js_Pages_Articles_Photos_Controller$get_threadControl() {
        /// <value type="Js.Controls.ThreadControl.Controller"></value>
        return this._view$1.get_uiThreadControl();
    },
    
    _photoProvider$1: null,
    
    get_photoProvider: function Js_Pages_Articles_Photos_Controller$get_photoProvider() {
        /// <value type="Js.Controls.PhotoBrowser.PhotoProvider"></value>
        if (this._photoProvider$1 == null) {
            this._photoProvider$1 = new Js.Controls.PhotoBrowser.ArticlePhotoProvider(parseInt(this._view$1.get_uiArticleK().value));
        }
        return this._photoProvider$1;
    },
    
    get_latestChatController: function Js_Pages_Articles_Photos_Controller$get_latestChatController() {
        /// <value type="Js.Controls.LatestChat.Controller"></value>
        return this._view$1.get_uiLatestChat();
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Pages.Articles.Photos.View

Js.Pages.Articles.Photos.View = function Js_Pages_Articles_Photos_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    /// <field name="_uiTitle$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiTitleJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_uiArticleInfoSpan$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiArticleInfoSpanJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_uiArticleK$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiArticleKJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_GenericContainerPage$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_GenericContainerPageJ$2" type="jQueryObject">
    /// </field>
    Js.Pages.Articles.Photos.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
Js.Pages.Articles.Photos.View.prototype = {
    clientId: null,
    
    get_uiTitle: function Js_Pages_Articles_Photos_View$get_uiTitle() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiTitle$2 == null) {
            this._uiTitle$2 = document.getElementById(this.clientId + '_uiTitle');
        }
        return this._uiTitle$2;
    },
    
    _uiTitle$2: null,
    
    get_uiTitleJ: function Js_Pages_Articles_Photos_View$get_uiTitleJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiTitleJ$2 == null) {
            this._uiTitleJ$2 = $('#' + this.clientId + '_uiTitle');
        }
        return this._uiTitleJ$2;
    },
    
    _uiTitleJ$2: null,
    
    get_uiArticleInfoSpan: function Js_Pages_Articles_Photos_View$get_uiArticleInfoSpan() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiArticleInfoSpan$2 == null) {
            this._uiArticleInfoSpan$2 = document.getElementById(this.clientId + '_uiArticleInfoSpan');
        }
        return this._uiArticleInfoSpan$2;
    },
    
    _uiArticleInfoSpan$2: null,
    
    get_uiArticleInfoSpanJ: function Js_Pages_Articles_Photos_View$get_uiArticleInfoSpanJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiArticleInfoSpanJ$2 == null) {
            this._uiArticleInfoSpanJ$2 = $('#' + this.clientId + '_uiArticleInfoSpan');
        }
        return this._uiArticleInfoSpanJ$2;
    },
    
    _uiArticleInfoSpanJ$2: null,
    
    get_uiPhotoBrowser: function Js_Pages_Articles_Photos_View$get_uiPhotoBrowser() {
        /// <value type="Js.Controls.PhotoBrowser.Controller"></value>
        return eval(this.clientId + '_uiPhotoBrowserController');
    },
    
    get_uiPhotoControl: function Js_Pages_Articles_Photos_View$get_uiPhotoControl() {
        /// <value type="Js.Controls.PhotoControl.Controller"></value>
        return eval(this.clientId + '_uiPhotoControlController');
    },
    
    get_uiLatestChat: function Js_Pages_Articles_Photos_View$get_uiLatestChat() {
        /// <value type="Js.Controls.LatestChat.Controller"></value>
        return eval(this.clientId + '_uiLatestChatController');
    },
    
    get_uiThreadControl: function Js_Pages_Articles_Photos_View$get_uiThreadControl() {
        /// <value type="Js.Controls.ThreadControl.Controller"></value>
        return eval(this.clientId + '_uiThreadControlController');
    },
    
    get_uiArticleK: function Js_Pages_Articles_Photos_View$get_uiArticleK() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiArticleK$2 == null) {
            this._uiArticleK$2 = document.getElementById(this.clientId + '_uiArticleK');
        }
        return this._uiArticleK$2;
    },
    
    _uiArticleK$2: null,
    
    get_uiArticleKJ: function Js_Pages_Articles_Photos_View$get_uiArticleKJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiArticleKJ$2 == null) {
            this._uiArticleKJ$2 = $('#' + this.clientId + '_uiArticleK');
        }
        return this._uiArticleKJ$2;
    },
    
    _uiArticleKJ$2: null,
    
    get_genericContainerPage: function Js_Pages_Articles_Photos_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        if (this._GenericContainerPage$2 == null) {
            this._GenericContainerPage$2 = document.getElementById(this.clientId + '_GenericContainerPage');
        }
        return this._GenericContainerPage$2;
    },
    
    _GenericContainerPage$2: null,
    
    get_genericContainerPageJ: function Js_Pages_Articles_Photos_View$get_genericContainerPageJ() {
        /// <value type="jQueryObject"></value>
        if (this._GenericContainerPageJ$2 == null) {
            this._GenericContainerPageJ$2 = $('#' + this.clientId + '_GenericContainerPage');
        }
        return this._GenericContainerPageJ$2;
    },
    
    _GenericContainerPageJ$2: null
}


Js.Pages.Articles.Photos.Controller.registerClass('Js.Pages.Articles.Photos.Controller', Js.Controls.PhotoBrowser.PhotosController);
Js.Pages.Articles.Photos.View.registerClass('Js.Pages.Articles.Photos.View', Js.DsiUserControl.View);
})(jQuery);

//! This script was generated using Script# v0.7.4.0
