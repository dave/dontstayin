//! Photos.debug.js
//

(function($) {

Type.registerNamespace('Js.Pages.Events.Photos');

////////////////////////////////////////////////////////////////////////////////
// Js.Pages.Events.Photos.Controller

Js.Pages.Events.Photos.Controller = function Js_Pages_Events_Photos_Controller(view) {
    /// <param name="view" type="Js.Pages.Events.Photos.View">
    /// </param>
    /// <field name="_view$1" type="Js.Pages.Events.Photos.View">
    /// </field>
    /// <field name="_photoProvider$1" type="Js.Controls.PhotoBrowser.PhotoProvider">
    /// </field>
    Js.Pages.Events.Photos.Controller.initializeBase(this);
    this._view$1 = view;
    view.get_uiCurrentGalleryJ().change(ss.Delegate.create(this, this._galleryChanged$1));
    this.setupController();
}
Js.Pages.Events.Photos.Controller.prototype = {
    _view$1: null,
    
    _galleryChanged$1: function Js_Pages_Events_Photos_Controller$_galleryChanged$1(e) {
        /// <param name="e" type="jQueryEvent">
        /// </param>
        for (var i = 0; i < this._view$1.get_uiCurrentGallery().childNodes.length; i++) {
            if ((this._view$1.get_uiCurrentGallery().childNodes[i]).selected) {
                var galleryK = parseInt((this._view$1.get_uiCurrentGallery().childNodes[i]).value);
                (this.get_photoProvider()).setGallery(galleryK);
                this._view$1.get_uiPhotoControl().isGallerySelectedChanged(galleryK > 0);
                this._view$1.get_uiPhotoBrowser().set_selectedIndex(0);
                this.get_photoBrowser().get_paginationControl().set_currentPage(1);
            }
        }
    },
    
    get_photoControl: function Js_Pages_Events_Photos_Controller$get_photoControl() {
        /// <value type="Js.Controls.PhotoControl.Controller"></value>
        return this._view$1.get_uiPhotoControl();
    },
    
    get_photoBrowser: function Js_Pages_Events_Photos_Controller$get_photoBrowser() {
        /// <value type="Js.Controls.PhotoBrowser.Controller"></value>
        return this._view$1.get_uiPhotoBrowser();
    },
    
    get_threadControl: function Js_Pages_Events_Photos_Controller$get_threadControl() {
        /// <value type="Js.Controls.ThreadControl.Controller"></value>
        return this._view$1.get_uiThreadControl();
    },
    
    _photoProvider$1: null,
    
    get_photoProvider: function Js_Pages_Events_Photos_Controller$get_photoProvider() {
        /// <value type="Js.Controls.PhotoBrowser.PhotoProvider"></value>
        if (this._photoProvider$1 == null) {
            this._photoProvider$1 = new Js.Controls.PhotoBrowser.EventPhotoProvider(parseInt(this._view$1.get_uiGalleryK().value), parseInt(this._view$1.get_uiEventK().value));
        }
        return this._photoProvider$1;
    },
    
    get_latestChatController: function Js_Pages_Events_Photos_Controller$get_latestChatController() {
        /// <value type="Js.Controls.LatestChat.Controller"></value>
        return this._view$1.get_uiLatestChat();
    }
}


////////////////////////////////////////////////////////////////////////////////
// Js.Pages.Events.Photos.View

Js.Pages.Events.Photos.View = function Js_Pages_Events_Photos_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    /// <field name="_GalleryHasNoPhotos$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_GalleryHasNoPhotosJ$3" type="jQueryObject">
    /// </field>
    /// <field name="_NoPhotosRetryLink$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_NoPhotosRetryLinkJ$3" type="jQueryObject">
    /// </field>
    /// <field name="_NoPhotosEventLink$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_NoPhotosEventLinkJ$3" type="jQueryObject">
    /// </field>
    /// <field name="_NoPhotosGalleryEditLink$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_NoPhotosGalleryEditLinkJ$3" type="jQueryObject">
    /// </field>
    /// <field name="_GalleryHasPhotos$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_GalleryHasPhotosJ$3" type="jQueryObject">
    /// </field>
    /// <field name="_uiTitle$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiTitleJ$3" type="jQueryObject">
    /// </field>
    /// <field name="_uiEventInfoSpan$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiEventInfoSpanJ$3" type="jQueryObject">
    /// </field>
    /// <field name="_uiCurrentGallery$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiCurrentGalleryJ$3" type="jQueryObject">
    /// </field>
    /// <field name="_uiUpdatePanel$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiUpdatePanelJ$3" type="jQueryObject">
    /// </field>
    /// <field name="_uiGalleryK$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiGalleryKJ$3" type="jQueryObject">
    /// </field>
    /// <field name="_uiEventK$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_uiEventKJ$3" type="jQueryObject">
    /// </field>
    /// <field name="_GenericContainerPage$3" type="Object" domElement="true">
    /// </field>
    /// <field name="_GenericContainerPageJ$3" type="jQueryObject">
    /// </field>
    Js.Pages.Events.Photos.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
Js.Pages.Events.Photos.View.prototype = {
    clientId: null,
    
    get_galleryHasNoPhotos: function Js_Pages_Events_Photos_View$get_galleryHasNoPhotos() {
        /// <value type="Object" domElement="true"></value>
        if (this._GalleryHasNoPhotos$3 == null) {
            this._GalleryHasNoPhotos$3 = document.getElementById(this.clientId + '_GalleryHasNoPhotos');
        }
        return this._GalleryHasNoPhotos$3;
    },
    
    _GalleryHasNoPhotos$3: null,
    
    get_galleryHasNoPhotosJ: function Js_Pages_Events_Photos_View$get_galleryHasNoPhotosJ() {
        /// <value type="jQueryObject"></value>
        if (this._GalleryHasNoPhotosJ$3 == null) {
            this._GalleryHasNoPhotosJ$3 = $('#' + this.clientId + '_GalleryHasNoPhotos');
        }
        return this._GalleryHasNoPhotosJ$3;
    },
    
    _GalleryHasNoPhotosJ$3: null,
    
    get_noPhotosRetryLink: function Js_Pages_Events_Photos_View$get_noPhotosRetryLink() {
        /// <value type="Object" domElement="true"></value>
        if (this._NoPhotosRetryLink$3 == null) {
            this._NoPhotosRetryLink$3 = document.getElementById(this.clientId + '_NoPhotosRetryLink');
        }
        return this._NoPhotosRetryLink$3;
    },
    
    _NoPhotosRetryLink$3: null,
    
    get_noPhotosRetryLinkJ: function Js_Pages_Events_Photos_View$get_noPhotosRetryLinkJ() {
        /// <value type="jQueryObject"></value>
        if (this._NoPhotosRetryLinkJ$3 == null) {
            this._NoPhotosRetryLinkJ$3 = $('#' + this.clientId + '_NoPhotosRetryLink');
        }
        return this._NoPhotosRetryLinkJ$3;
    },
    
    _NoPhotosRetryLinkJ$3: null,
    
    get_noPhotosEventLink: function Js_Pages_Events_Photos_View$get_noPhotosEventLink() {
        /// <value type="Object" domElement="true"></value>
        if (this._NoPhotosEventLink$3 == null) {
            this._NoPhotosEventLink$3 = document.getElementById(this.clientId + '_NoPhotosEventLink');
        }
        return this._NoPhotosEventLink$3;
    },
    
    _NoPhotosEventLink$3: null,
    
    get_noPhotosEventLinkJ: function Js_Pages_Events_Photos_View$get_noPhotosEventLinkJ() {
        /// <value type="jQueryObject"></value>
        if (this._NoPhotosEventLinkJ$3 == null) {
            this._NoPhotosEventLinkJ$3 = $('#' + this.clientId + '_NoPhotosEventLink');
        }
        return this._NoPhotosEventLinkJ$3;
    },
    
    _NoPhotosEventLinkJ$3: null,
    
    get_noPhotosGalleryEditLink: function Js_Pages_Events_Photos_View$get_noPhotosGalleryEditLink() {
        /// <value type="Object" domElement="true"></value>
        if (this._NoPhotosGalleryEditLink$3 == null) {
            this._NoPhotosGalleryEditLink$3 = document.getElementById(this.clientId + '_NoPhotosGalleryEditLink');
        }
        return this._NoPhotosGalleryEditLink$3;
    },
    
    _NoPhotosGalleryEditLink$3: null,
    
    get_noPhotosGalleryEditLinkJ: function Js_Pages_Events_Photos_View$get_noPhotosGalleryEditLinkJ() {
        /// <value type="jQueryObject"></value>
        if (this._NoPhotosGalleryEditLinkJ$3 == null) {
            this._NoPhotosGalleryEditLinkJ$3 = $('#' + this.clientId + '_NoPhotosGalleryEditLink');
        }
        return this._NoPhotosGalleryEditLinkJ$3;
    },
    
    _NoPhotosGalleryEditLinkJ$3: null,
    
    get_galleryHasPhotos: function Js_Pages_Events_Photos_View$get_galleryHasPhotos() {
        /// <value type="Object" domElement="true"></value>
        if (this._GalleryHasPhotos$3 == null) {
            this._GalleryHasPhotos$3 = document.getElementById(this.clientId + '_GalleryHasPhotos');
        }
        return this._GalleryHasPhotos$3;
    },
    
    _GalleryHasPhotos$3: null,
    
    get_galleryHasPhotosJ: function Js_Pages_Events_Photos_View$get_galleryHasPhotosJ() {
        /// <value type="jQueryObject"></value>
        if (this._GalleryHasPhotosJ$3 == null) {
            this._GalleryHasPhotosJ$3 = $('#' + this.clientId + '_GalleryHasPhotos');
        }
        return this._GalleryHasPhotosJ$3;
    },
    
    _GalleryHasPhotosJ$3: null,
    
    get_uiTitle: function Js_Pages_Events_Photos_View$get_uiTitle() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiTitle$3 == null) {
            this._uiTitle$3 = document.getElementById(this.clientId + '_uiTitle');
        }
        return this._uiTitle$3;
    },
    
    _uiTitle$3: null,
    
    get_uiTitleJ: function Js_Pages_Events_Photos_View$get_uiTitleJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiTitleJ$3 == null) {
            this._uiTitleJ$3 = $('#' + this.clientId + '_uiTitle');
        }
        return this._uiTitleJ$3;
    },
    
    _uiTitleJ$3: null,
    
    get_uiEventInfoSpan: function Js_Pages_Events_Photos_View$get_uiEventInfoSpan() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiEventInfoSpan$3 == null) {
            this._uiEventInfoSpan$3 = document.getElementById(this.clientId + '_uiEventInfoSpan');
        }
        return this._uiEventInfoSpan$3;
    },
    
    _uiEventInfoSpan$3: null,
    
    get_uiEventInfoSpanJ: function Js_Pages_Events_Photos_View$get_uiEventInfoSpanJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiEventInfoSpanJ$3 == null) {
            this._uiEventInfoSpanJ$3 = $('#' + this.clientId + '_uiEventInfoSpan');
        }
        return this._uiEventInfoSpanJ$3;
    },
    
    _uiEventInfoSpanJ$3: null,
    
    get_uiCurrentGallery: function Js_Pages_Events_Photos_View$get_uiCurrentGallery() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiCurrentGallery$3 == null) {
            this._uiCurrentGallery$3 = document.getElementById(this.clientId + '_uiCurrentGallery');
        }
        return this._uiCurrentGallery$3;
    },
    
    _uiCurrentGallery$3: null,
    
    get_uiCurrentGalleryJ: function Js_Pages_Events_Photos_View$get_uiCurrentGalleryJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiCurrentGalleryJ$3 == null) {
            this._uiCurrentGalleryJ$3 = $('#' + this.clientId + '_uiCurrentGallery');
        }
        return this._uiCurrentGalleryJ$3;
    },
    
    _uiCurrentGalleryJ$3: null,
    
    get_uiPhotoBrowser: function Js_Pages_Events_Photos_View$get_uiPhotoBrowser() {
        /// <value type="Js.Controls.PhotoBrowser.Controller"></value>
        return eval(this.clientId + '_uiPhotoBrowserController');
    },
    
    get_uiPhotoControl: function Js_Pages_Events_Photos_View$get_uiPhotoControl() {
        /// <value type="Js.Controls.PhotoControl.Controller"></value>
        return eval(this.clientId + '_uiPhotoControlController');
    },
    
    get_uiUpdatePanel: function Js_Pages_Events_Photos_View$get_uiUpdatePanel() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiUpdatePanel$3 == null) {
            this._uiUpdatePanel$3 = document.getElementById(this.clientId + '_uiUpdatePanel');
        }
        return this._uiUpdatePanel$3;
    },
    
    _uiUpdatePanel$3: null,
    
    get_uiUpdatePanelJ: function Js_Pages_Events_Photos_View$get_uiUpdatePanelJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiUpdatePanelJ$3 == null) {
            this._uiUpdatePanelJ$3 = $('#' + this.clientId + '_uiUpdatePanel');
        }
        return this._uiUpdatePanelJ$3;
    },
    
    _uiUpdatePanelJ$3: null,
    
    get_uiLatestChat: function Js_Pages_Events_Photos_View$get_uiLatestChat() {
        /// <value type="Js.Controls.LatestChat.Controller"></value>
        return eval(this.clientId + '_uiLatestChatController');
    },
    
    get_uiThreadControl: function Js_Pages_Events_Photos_View$get_uiThreadControl() {
        /// <value type="Js.Controls.ThreadControl.Controller"></value>
        return eval(this.clientId + '_uiThreadControlController');
    },
    
    get_uiGalleryK: function Js_Pages_Events_Photos_View$get_uiGalleryK() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiGalleryK$3 == null) {
            this._uiGalleryK$3 = document.getElementById(this.clientId + '_uiGalleryK');
        }
        return this._uiGalleryK$3;
    },
    
    _uiGalleryK$3: null,
    
    get_uiGalleryKJ: function Js_Pages_Events_Photos_View$get_uiGalleryKJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiGalleryKJ$3 == null) {
            this._uiGalleryKJ$3 = $('#' + this.clientId + '_uiGalleryK');
        }
        return this._uiGalleryKJ$3;
    },
    
    _uiGalleryKJ$3: null,
    
    get_uiEventK: function Js_Pages_Events_Photos_View$get_uiEventK() {
        /// <value type="Object" domElement="true"></value>
        if (this._uiEventK$3 == null) {
            this._uiEventK$3 = document.getElementById(this.clientId + '_uiEventK');
        }
        return this._uiEventK$3;
    },
    
    _uiEventK$3: null,
    
    get_uiEventKJ: function Js_Pages_Events_Photos_View$get_uiEventKJ() {
        /// <value type="jQueryObject"></value>
        if (this._uiEventKJ$3 == null) {
            this._uiEventKJ$3 = $('#' + this.clientId + '_uiEventK');
        }
        return this._uiEventKJ$3;
    },
    
    _uiEventKJ$3: null,
    
    get_genericContainerPage: function Js_Pages_Events_Photos_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        if (this._GenericContainerPage$3 == null) {
            this._GenericContainerPage$3 = document.getElementById(this.clientId + '_GenericContainerPage');
        }
        return this._GenericContainerPage$3;
    },
    
    _GenericContainerPage$3: null,
    
    get_genericContainerPageJ: function Js_Pages_Events_Photos_View$get_genericContainerPageJ() {
        /// <value type="jQueryObject"></value>
        if (this._GenericContainerPageJ$3 == null) {
            this._GenericContainerPageJ$3 = $('#' + this.clientId + '_GenericContainerPage');
        }
        return this._GenericContainerPageJ$3;
    },
    
    _GenericContainerPageJ$3: null
}


Js.Pages.Events.Photos.Controller.registerClass('Js.Pages.Events.Photos.Controller', Js.Controls.PhotoBrowser.PhotosController);
Js.Pages.Events.Photos.View.registerClass('Js.Pages.Events.Photos.View', Js.Pages.Events.EventUserControl.View);
})(jQuery);

//! This script was generated using Script# v0.7.4.0
