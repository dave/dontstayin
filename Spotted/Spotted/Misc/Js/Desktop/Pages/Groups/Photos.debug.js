//! Photos.debug.js
//

(function($) {

Type.registerNamespace('Js.Pages.Groups.Photos');

////////////////////////////////////////////////////////////////////////////////
// Js.Pages.Groups.Photos.Controller

Js.Pages.Groups.Photos.Controller = function Js_Pages_Groups_Photos_Controller(view) {
    /// <param name="view" type="Js.Pages.Groups.Photos.View">
    /// </param>
    /// <field name="_view$1" type="Js.Pages.Groups.Photos.View">
    /// </field>
    /// <field name="_photoProvider$1" type="Js.Controls.PhotoBrowser.PhotoProvider">
    /// </field>
    Js.Pages.Groups.Photos.Controller.initializeBase(this);
    this._view$1 = view;
    this.setupController();
}
Js.Pages.Groups.Photos.Controller.prototype = {
    _view$1: null,
    
    get_photoControl: function Js_Pages_Groups_Photos_Controller$get_photoControl() {
        /// <value type="Js.Controls.PhotoControl.Controller"></value>
        return this._view$1.get_uiPhotoControl();
    },
    
    get_photoBrowser: function Js_Pages_Groups_Photos_Controller$get_photoBrowser() {
        /// <value type="Js.Controls.PhotoBrowser.Controller"></value>
        return this._view$1.get_uiPhotoBrowser();
    },
    
    get_threadControl: function Js_Pages_Groups_Photos_Controller$get_threadControl() {
        /// <value type="Js.Controls.ThreadControl.Controller"></value>
        return this._view$1.get_uiThreadControl();
    },
    
    _photoProvider$1: null,
    
    get_photoProvider: function Js_Pages_Groups_Photos_Controller$get_photoProvider() {
        /// <value type="Js.Controls.PhotoBrowser.PhotoProvider"></value>
        return this._photoProvider$1 || (this._photoProvider$1 = new Js.Controls.PhotoBrowser.GroupPhotoProvider(parseInt(this._view$1.get_uiGroupK().value)));
    },
    
    get_latestChatController: function Js_Pages_Groups_Photos_Controller$get_latestChatController() {
        /// <value type="Js.Controls.LatestChat.Controller"></value>
        return this._view$1.get_uiLatestChat();
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Pages.Groups.Photos.View

Js.Pages.Groups.Photos.View = function Js_Pages_Groups_Photos_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    /// <field name="_uiTitle$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiTitleJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_uiUpdatePanel$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiUpdatePanelJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_uiGroupK$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiGroupKJ$2" type="jQueryObject">
    /// </field>
    /// <field name="_GenericContainerPage$2" type="Object" domElement="true">
    /// </field>
    /// <field name="_GenericContainerPageJ$2" type="jQueryObject">
    /// </field>
    Js.Pages.Groups.Photos.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
Js.Pages.Groups.Photos.View.prototype = {
    clientId: null,
    
    get_uiTitle: function Js_Pages_Groups_Photos_View$get_uiTitle() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiTitle$2 == null) {
            this._uiTitle$2 = document.getElementById(this.clientId + '_uiTitle');
        }
        return this._uiTitle$2;
    },
    
    _uiTitle$2: null,
    
    get_uiTitleJ: function Js_Pages_Groups_Photos_View$get_uiTitleJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiTitleJ$2 == null) {
            this._uiTitleJ$2 = $('#' + this.clientId + '_uiTitle');
        }
        return this._uiTitleJ$2;
    },
    
    _uiTitleJ$2: null,
    
    get_uiPhotoBrowser: function Js_Pages_Groups_Photos_View$get_uiPhotoBrowser() {
        /// <value type="Js.Controls.PhotoBrowser.Controller"></value>
        return eval(this.clientId + '_uiPhotoBrowserController');
    },
    
    get_uiPhotoControl: function Js_Pages_Groups_Photos_View$get_uiPhotoControl() {
        /// <value type="Js.Controls.PhotoControl.Controller"></value>
        return eval(this.clientId + '_uiPhotoControlController');
    },
    
    get_uiUpdatePanel: function Js_Pages_Groups_Photos_View$get_uiUpdatePanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiUpdatePanel$2 == null) {
            this._uiUpdatePanel$2 = document.getElementById(this.clientId + '_uiUpdatePanel');
        }
        return this._uiUpdatePanel$2;
    },
    
    _uiUpdatePanel$2: null,
    
    get_uiUpdatePanelJ: function Js_Pages_Groups_Photos_View$get_uiUpdatePanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiUpdatePanelJ$2 == null) {
            this._uiUpdatePanelJ$2 = $('#' + this.clientId + '_uiUpdatePanel');
        }
        return this._uiUpdatePanelJ$2;
    },
    
    _uiUpdatePanelJ$2: null,
    
    get_uiLatestChat: function Js_Pages_Groups_Photos_View$get_uiLatestChat() {
        /// <value type="Js.Controls.LatestChat.Controller"></value>
        return eval(this.clientId + '_uiLatestChatController');
    },
    
    get_uiThreadControl: function Js_Pages_Groups_Photos_View$get_uiThreadControl() {
        /// <value type="Js.Controls.ThreadControl.Controller"></value>
        return eval(this.clientId + '_uiThreadControlController');
    },
    
    get_uiGroupK: function Js_Pages_Groups_Photos_View$get_uiGroupK() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiGroupK$2 == null) {
            this._uiGroupK$2 = document.getElementById(this.clientId + '_uiGroupK');
        }
        return this._uiGroupK$2;
    },
    
    _uiGroupK$2: null,
    
    get_uiGroupKJ: function Js_Pages_Groups_Photos_View$get_uiGroupKJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiGroupKJ$2 == null) {
            this._uiGroupKJ$2 = $('#' + this.clientId + '_uiGroupK');
        }
        return this._uiGroupKJ$2;
    },
    
    _uiGroupKJ$2: null,
    
    get_genericContainerPage: function Js_Pages_Groups_Photos_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        if (this._GenericContainerPage$2 == null) {
            this._GenericContainerPage$2 = document.getElementById(this.clientId + '_GenericContainerPage');
        }
        return this._GenericContainerPage$2;
    },
    
    _GenericContainerPage$2: null,
    
    get_genericContainerPageJ: function Js_Pages_Groups_Photos_View$get_genericContainerPageJ() {
        /// <value type="jQueryObject"></value>
        if (this._GenericContainerPageJ$2 == null) {
            this._GenericContainerPageJ$2 = $('#' + this.clientId + '_GenericContainerPage');
        }
        return this._GenericContainerPageJ$2;
    },
    
    _GenericContainerPageJ$2: null
}


Js.Pages.Groups.Photos.Controller.registerClass('Js.Pages.Groups.Photos.Controller', Js.Controls.PhotoBrowser.PhotosController);
Js.Pages.Groups.Photos.View.registerClass('Js.Pages.Groups.Photos.View', Js.DsiUserControl.View);
})(jQuery);

//! This script was generated using Script# v0.7.4.0
