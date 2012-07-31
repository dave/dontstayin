//! Photos.debug.js
//

(function($) {

Type.registerNamespace('Js.Pages.Usrs.Photos');

////////////////////////////////////////////////////////////////////////////////
// Js.Pages.Usrs.Photos.Controller

Js.Pages.Usrs.Photos.Controller = function Js_Pages_Usrs_Photos_Controller(view) {
    /// <param name="view" type="Js.Pages.Usrs.Photos.View">
    /// </param>
    /// <field name="_view$1" type="Js.Pages.Usrs.Photos.View">
    /// </field>
    /// <field name="_photoProvider$1" type="Js.Controls.PhotoBrowser.PhotoProvider">
    /// </field>
    Js.Pages.Usrs.Photos.Controller.initializeBase(this);
    this._view$1 = view;
    this.setupController();
}
Js.Pages.Usrs.Photos.Controller.prototype = {
    _view$1: null,
    
    get_photoControl: function Js_Pages_Usrs_Photos_Controller$get_photoControl() {
        /// <value type="Js.Controls.PhotoControl.Controller"></value>
        return this._view$1.get_uiPhotoControl();
    },
    
    get_photoBrowser: function Js_Pages_Usrs_Photos_Controller$get_photoBrowser() {
        /// <value type="Js.Controls.PhotoBrowser.Controller"></value>
        return this._view$1.get_uiPhotoBrowser();
    },
    
    get_threadControl: function Js_Pages_Usrs_Photos_Controller$get_threadControl() {
        /// <value type="Js.Controls.ThreadControl.Controller"></value>
        return this._view$1.get_uiThreadControl();
    },
    
    _photoProvider$1: null,
    
    get_photoProvider: function Js_Pages_Usrs_Photos_Controller$get_photoProvider() {
        /// <value type="Js.Controls.PhotoBrowser.PhotoProvider"></value>
        return this._photoProvider$1 || (this._photoProvider$1 = new Js.Controls.PhotoBrowser.PhotosOfUsrProvider(parseInt(this._view$1.get_uiUsrK().value), parseInt(this._view$1.get_uiSpottedByUsrK().value)));
    },
    
    get_latestChatController: function Js_Pages_Usrs_Photos_Controller$get_latestChatController() {
        /// <value type="Js.Controls.LatestChat.Controller"></value>
        return this._view$1.get_uiLatestChat();
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Pages.Usrs.Photos.View

Js.Pages.Usrs.Photos.View = function Js_Pages_Usrs_Photos_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    /// <field name="_UsrIntro$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_UsrIntroJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_TakenBySpan$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_TakenBySpanJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_uiUpdatePanel$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiUpdatePanelJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_uiUsrK$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiUsrKJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_uiSpottedByUsrK$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiSpottedByUsrKJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_GenericContainerPage$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_GenericContainerPageJ$2" type="jQueryObject">
    /// </field>
    Js.Pages.Usrs.Photos.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
Js.Pages.Usrs.Photos.View.prototype = {
    clientId: null,
    
    get_usrIntro: function Js_Pages_Usrs_Photos_View$get_usrIntro() {
        /// <value type="Object" domElement="true"></value>
        if (this._UsrIntro$2 == null) {
            this._UsrIntro$2 = document.getElementById(this.clientId + '_UsrIntro');
        }
        return this._UsrIntro$2;
    },
    
    _UsrIntro$2: null,
    
    get_usrIntroJ: function Js_Pages_Usrs_Photos_View$get_usrIntroJ() {
        /// <value type="jQueryObject"></value>
        if (this._UsrIntroJ$2 == null) {
            this._UsrIntroJ$2 = $('#' + this.clientId + '_UsrIntro');
        }
        return this._UsrIntroJ$2;
    },
    
    _UsrIntroJ$2: null,
    
    get_takenBySpan: function Js_Pages_Usrs_Photos_View$get_takenBySpan() {
        /// <value type="Object" domElement="true"></value>
        if (this._TakenBySpan$2 == null) {
            this._TakenBySpan$2 = document.getElementById(this.clientId + '_TakenBySpan');
        }
        return this._TakenBySpan$2;
    },
    
    _TakenBySpan$2: null,
    
    get_takenBySpanJ: function Js_Pages_Usrs_Photos_View$get_takenBySpanJ() {
        /// <value type="jQueryObject"></value>
        if (this._TakenBySpanJ$2 == null) {
            this._TakenBySpanJ$2 = $('#' + this.clientId + '_TakenBySpan');
        }
        return this._TakenBySpanJ$2;
    },
    
    _TakenBySpanJ$2: null,
    
    get_uiPhotoBrowser: function Js_Pages_Usrs_Photos_View$get_uiPhotoBrowser() {
        /// <value type="Js.Controls.PhotoBrowser.Controller"></value>
        return eval(this.clientId + '_uiPhotoBrowserController');
    },
    
    get_uiPhotoControl: function Js_Pages_Usrs_Photos_View$get_uiPhotoControl() {
        /// <value type="Js.Controls.PhotoControl.Controller"></value>
        return eval(this.clientId + '_uiPhotoControlController');
    },
    
    get_uiUpdatePanel: function Js_Pages_Usrs_Photos_View$get_uiUpdatePanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiUpdatePanel$2 == null) {
            this._uiUpdatePanel$2 = document.getElementById(this.clientId + '_uiUpdatePanel');
        }
        return this._uiUpdatePanel$2;
    },
    
    _uiUpdatePanel$2: null,
    
    get_uiUpdatePanelJ: function Js_Pages_Usrs_Photos_View$get_uiUpdatePanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiUpdatePanelJ$2 == null) {
            this._uiUpdatePanelJ$2 = $('#' + this.clientId + '_uiUpdatePanel');
        }
        return this._uiUpdatePanelJ$2;
    },
    
    _uiUpdatePanelJ$2: null,
    
    get_uiLatestChat: function Js_Pages_Usrs_Photos_View$get_uiLatestChat() {
        /// <value type="Js.Controls.LatestChat.Controller"></value>
        return eval(this.clientId + '_uiLatestChatController');
    },
    
    get_uiThreadControl: function Js_Pages_Usrs_Photos_View$get_uiThreadControl() {
        /// <value type="Js.Controls.ThreadControl.Controller"></value>
        return eval(this.clientId + '_uiThreadControlController');
    },
    
    get_uiUsrK: function Js_Pages_Usrs_Photos_View$get_uiUsrK() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiUsrK$2 == null) {
            this._uiUsrK$2 = document.getElementById(this.clientId + '_uiUsrK');
        }
        return this._uiUsrK$2;
    },
    
    _uiUsrK$2: null,
    
    get_uiUsrKJ: function Js_Pages_Usrs_Photos_View$get_uiUsrKJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiUsrKJ$2 == null) {
            this._uiUsrKJ$2 = $('#' + this.clientId + '_uiUsrK');
        }
        return this._uiUsrKJ$2;
    },
    
    _uiUsrKJ$2: null,
    
    get_uiSpottedByUsrK: function Js_Pages_Usrs_Photos_View$get_uiSpottedByUsrK() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiSpottedByUsrK$2 == null) {
            this._uiSpottedByUsrK$2 = document.getElementById(this.clientId + '_uiSpottedByUsrK');
        }
        return this._uiSpottedByUsrK$2;
    },
    
    _uiSpottedByUsrK$2: null,
    
    get_uiSpottedByUsrKJ: function Js_Pages_Usrs_Photos_View$get_uiSpottedByUsrKJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiSpottedByUsrKJ$2 == null) {
            this._uiSpottedByUsrKJ$2 = $('#' + this.clientId + '_uiSpottedByUsrK');
        }
        return this._uiSpottedByUsrKJ$2;
    },
    
    _uiSpottedByUsrKJ$2: null,
    
    get_genericContainerPage: function Js_Pages_Usrs_Photos_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        if (this._GenericContainerPage$2 == null) {
            this._GenericContainerPage$2 = document.getElementById(this.clientId + '_GenericContainerPage');
        }
        return this._GenericContainerPage$2;
    },
    
    _GenericContainerPage$2: null,
    
    get_genericContainerPageJ: function Js_Pages_Usrs_Photos_View$get_genericContainerPageJ() {
        /// <value type="jQueryObject"></value>
        if (this._GenericContainerPageJ$2 == null) {
            this._GenericContainerPageJ$2 = $('#' + this.clientId + '_GenericContainerPage');
        }
        return this._GenericContainerPageJ$2;
    },
    
    _GenericContainerPageJ$2: null
}


Js.Pages.Usrs.Photos.Controller.registerClass('Js.Pages.Usrs.Photos.Controller', Js.Controls.PhotoBrowser.PhotosController);
Js.Pages.Usrs.Photos.View.registerClass('Js.Pages.Usrs.Photos.View', Js.DsiUserControl.View);
})(jQuery);

//! This script was generated using Script# v0.7.4.0
