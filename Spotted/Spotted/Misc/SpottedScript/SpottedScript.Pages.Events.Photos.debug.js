Type.registerNamespace('SpottedScript.Pages.Events.Photos');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Events.Photos.Controller
SpottedScript.Pages.Events.Photos.Controller = function SpottedScript_Pages_Events_Photos_Controller(view) {
    /// <param name="view" type="SpottedScript.Pages.Events.Photos.View">
    /// </param>
    /// <field name="_view$1" type="SpottedScript.Pages.Events.Photos.View">
    /// </field>
    /// <field name="_photoProvider$1" type="SpottedScript.Controls.PhotoBrowser.PhotoProvider">
    /// </field>
    SpottedScript.Pages.Events.Photos.Controller.initializeBase(this);
    this._view$1 = view;
    $addHandler(view.get_uiCurrentGallery(), 'change', Function.createDelegate(this, this._galleryChanged$1));
    this.setupController();
}
SpottedScript.Pages.Events.Photos.Controller.prototype = {
    _view$1: null,
    _galleryChanged$1: function SpottedScript_Pages_Events_Photos_Controller$_galleryChanged$1(e) {
        /// <param name="e" type="Sys.UI.DomEvent">
        /// </param>
        for (var i = 0; i < this._view$1.get_uiCurrentGallery().childNodes.length; i++) {
            if ((this._view$1.get_uiCurrentGallery().childNodes[i]).selected) {
                var galleryK = Number.parseInvariant((this._view$1.get_uiCurrentGallery().childNodes[i]).value);
                (this.get_photoProvider())._setGallery(galleryK);
                this._view$1.get_uiPhotoControl().isGallerySelectedChanged(galleryK > 0);
                this._view$1.get_uiPhotoBrowser().set__selectedIndex(0);
                this.get_photoBrowser().get__paginationControl().set__currentPage(1);
            }
        }
    },
    get_photoControl: function SpottedScript_Pages_Events_Photos_Controller$get_photoControl() {
        /// <value type="SpottedScript.Controls.PhotoControl.Controller"></value>
        return this._view$1.get_uiPhotoControl();
    },
    get_photoBrowser: function SpottedScript_Pages_Events_Photos_Controller$get_photoBrowser() {
        /// <value type="SpottedScript.Controls.PhotoBrowser.Controller"></value>
        return this._view$1.get_uiPhotoBrowser();
    },
    get_threadControl: function SpottedScript_Pages_Events_Photos_Controller$get_threadControl() {
        /// <value type="SpottedScript.Controls.ThreadControl.Controller"></value>
        return this._view$1.get_uiThreadControl();
    },
    _photoProvider$1: null,
    get_photoProvider: function SpottedScript_Pages_Events_Photos_Controller$get_photoProvider() {
        /// <value type="SpottedScript.Controls.PhotoBrowser.PhotoProvider"></value>
        if (this._photoProvider$1 == null) {
            this._photoProvider$1 = new SpottedScript.Controls.PhotoBrowser.EventPhotoProvider(Number.parseInvariant(this._view$1.get_uiGalleryK().value), Number.parseInvariant(this._view$1.get_uiEventK().value));
        }
        return this._photoProvider$1;
    },
    get_latestChatController: function SpottedScript_Pages_Events_Photos_Controller$get_latestChatController() {
        /// <value type="SpottedScript.Controls.LatestChat.Controller"></value>
        return this._view$1.get_uiLatestChat();
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Events.Photos.View
SpottedScript.Pages.Events.Photos.View = function SpottedScript_Pages_Events_Photos_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Events.Photos.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Events.Photos.View.prototype = {
    clientId: null,
    get_galleryHasNoPhotos: function SpottedScript_Pages_Events_Photos_View$get_galleryHasNoPhotos() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GalleryHasNoPhotos');
    },
    get_noPhotosRetryLink: function SpottedScript_Pages_Events_Photos_View$get_noPhotosRetryLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoPhotosRetryLink');
    },
    get_noPhotosEventLink: function SpottedScript_Pages_Events_Photos_View$get_noPhotosEventLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoPhotosEventLink');
    },
    get_noPhotosGalleryEditLink: function SpottedScript_Pages_Events_Photos_View$get_noPhotosGalleryEditLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoPhotosGalleryEditLink');
    },
    get_galleryHasPhotos: function SpottedScript_Pages_Events_Photos_View$get_galleryHasPhotos() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GalleryHasPhotos');
    },
    get_uiTitle: function SpottedScript_Pages_Events_Photos_View$get_uiTitle() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiTitle');
    },
    get_uiEventInfoSpan: function SpottedScript_Pages_Events_Photos_View$get_uiEventInfoSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiEventInfoSpan');
    },
    get_uiCurrentGallery: function SpottedScript_Pages_Events_Photos_View$get_uiCurrentGallery() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiCurrentGallery');
    },
    get_uiPhotoBrowser: function SpottedScript_Pages_Events_Photos_View$get_uiPhotoBrowser() {
        /// <value type="SpottedScript.Controls.PhotoBrowser.Controller"></value>
        return eval(this.clientId + '_uiPhotoBrowserController');
    },
    get_uiPhotoControl: function SpottedScript_Pages_Events_Photos_View$get_uiPhotoControl() {
        /// <value type="SpottedScript.Controls.PhotoControl.Controller"></value>
        return eval(this.clientId + '_uiPhotoControlController');
    },
    get_uiUpdatePanel: function SpottedScript_Pages_Events_Photos_View$get_uiUpdatePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiUpdatePanel');
    },
    get_uiLatestChat: function SpottedScript_Pages_Events_Photos_View$get_uiLatestChat() {
        /// <value type="SpottedScript.Controls.LatestChat.Controller"></value>
        return eval(this.clientId + '_uiLatestChatController');
    },
    get_uiThreadControl: function SpottedScript_Pages_Events_Photos_View$get_uiThreadControl() {
        /// <value type="SpottedScript.Controls.ThreadControl.Controller"></value>
        return eval(this.clientId + '_uiThreadControlController');
    },
    get_uiGalleryK: function SpottedScript_Pages_Events_Photos_View$get_uiGalleryK() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiGalleryK');
    },
    get_uiEventK: function SpottedScript_Pages_Events_Photos_View$get_uiEventK() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiEventK');
    },
    get_genericContainerPage: function SpottedScript_Pages_Events_Photos_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Events.Photos.Controller.registerClass('SpottedScript.Pages.Events.Photos.Controller', SpottedScript.Controls.PhotoBrowser.PhotosController);
SpottedScript.Pages.Events.Photos.View.registerClass('SpottedScript.Pages.Events.Photos.View', SpottedScript.Pages.Events.EventUserControl.View);
