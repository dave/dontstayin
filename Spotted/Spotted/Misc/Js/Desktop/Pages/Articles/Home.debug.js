//! Home.debug.js
//

(function($) {

Type.registerNamespace('Js.Pages.Articles.Home');

////////////////////////////////////////////////////////////////////////////////
// Js.Pages.Articles.Home.Controller

Js.Pages.Articles.Home.Controller = function Js_Pages_Articles_Home_Controller(view) {
    /// <param name="view" type="Js.Pages.Articles.Home.View">
    /// </param>
    view.get_threadControl().get_uiCommentsDisplay().showComments(parseInt(view.get_uiThreadK().value), parseInt(view.get_uiPageNumber().value));
    view.get_threadControl().set_currentParentObjectK(parseInt(view.get_uiArticleK().value));
}


////////////////////////////////////////////////////////////////////////////////
// Js.Pages.Articles.Home.View

Js.Pages.Articles.Home.View = function Js_Pages_Articles_Home_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    /// <field name="_HomeContent$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_HomeContentJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_uiThreadK$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiThreadKJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_uiArticleK$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiArticleKJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_uiPageNumber$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiPageNumberJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_GenericContainerPage$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_GenericContainerPageJ$2" type="jQueryObject">
    /// </field>
    Js.Pages.Articles.Home.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
Js.Pages.Articles.Home.View.prototype = {
    clientId: null,
    
    get_homeContent: function Js_Pages_Articles_Home_View$get_homeContent() {
        /// <value type="Object" domElement="true"></value>
        if (this._HomeContent$2 == null) {
            this._HomeContent$2 = document.getElementById(this.clientId + '_HomeContent');
        }
        return this._HomeContent$2;
    },
    
    _HomeContent$2: null,
    
    get_homeContentJ: function Js_Pages_Articles_Home_View$get_homeContentJ() {
        /// <value type="jQueryObject"></value>
        if (this._HomeContentJ$2 == null) {
            this._HomeContentJ$2 = $('#' + this.clientId + '_HomeContent');
        }
        return this._HomeContentJ$2;
    },
    
    _HomeContentJ$2: null,
    
    get_latestChat: function Js_Pages_Articles_Home_View$get_latestChat() {
        /// <value type="Js.Controls.LatestChat.Controller"></value>
        return eval(this.clientId + '_LatestChatController');
    },
    
    get_threadControl: function Js_Pages_Articles_Home_View$get_threadControl() {
        /// <value type="Js.Controls.ThreadControl.Controller"></value>
        return eval(this.clientId + '_ThreadControlController');
    },
    
    get_uiThreadK: function Js_Pages_Articles_Home_View$get_uiThreadK() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiThreadK$2 == null) {
            this._uiThreadK$2 = document.getElementById(this.clientId + '_uiThreadK');
        }
        return this._uiThreadK$2;
    },
    
    _uiThreadK$2: null,
    
    get_uiThreadKJ: function Js_Pages_Articles_Home_View$get_uiThreadKJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiThreadKJ$2 == null) {
            this._uiThreadKJ$2 = $('#' + this.clientId + '_uiThreadK');
        }
        return this._uiThreadKJ$2;
    },
    
    _uiThreadKJ$2: null,
    
    get_uiArticleK: function Js_Pages_Articles_Home_View$get_uiArticleK() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiArticleK$2 == null) {
            this._uiArticleK$2 = document.getElementById(this.clientId + '_uiArticleK');
        }
        return this._uiArticleK$2;
    },
    
    _uiArticleK$2: null,
    
    get_uiArticleKJ: function Js_Pages_Articles_Home_View$get_uiArticleKJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiArticleKJ$2 == null) {
            this._uiArticleKJ$2 = $('#' + this.clientId + '_uiArticleK');
        }
        return this._uiArticleKJ$2;
    },
    
    _uiArticleKJ$2: null,
    
    get_uiPageNumber: function Js_Pages_Articles_Home_View$get_uiPageNumber() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiPageNumber$2 == null) {
            this._uiPageNumber$2 = document.getElementById(this.clientId + '_uiPageNumber');
        }
        return this._uiPageNumber$2;
    },
    
    _uiPageNumber$2: null,
    
    get_uiPageNumberJ: function Js_Pages_Articles_Home_View$get_uiPageNumberJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiPageNumberJ$2 == null) {
            this._uiPageNumberJ$2 = $('#' + this.clientId + '_uiPageNumber');
        }
        return this._uiPageNumberJ$2;
    },
    
    _uiPageNumberJ$2: null,
    
    get_genericContainerPage: function Js_Pages_Articles_Home_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        if (this._GenericContainerPage$2 == null) {
            this._GenericContainerPage$2 = document.getElementById(this.clientId + '_GenericContainerPage');
        }
        return this._GenericContainerPage$2;
    },
    
    _GenericContainerPage$2: null,
    
    get_genericContainerPageJ: function Js_Pages_Articles_Home_View$get_genericContainerPageJ() {
        /// <value type="jQueryObject"></value>
        if (this._GenericContainerPageJ$2 == null) {
            this._GenericContainerPageJ$2 = $('#' + this.clientId + '_GenericContainerPage');
        }
        return this._GenericContainerPageJ$2;
    },
    
    _GenericContainerPageJ$2: null
}


Js.Pages.Articles.Home.Controller.registerClass('Js.Pages.Articles.Home.Controller');
Js.Pages.Articles.Home.View.registerClass('Js.Pages.Articles.Home.View', Js.DsiUserControl.View);
})(jQuery);

//! This script was generated using Script# v0.7.4.0
