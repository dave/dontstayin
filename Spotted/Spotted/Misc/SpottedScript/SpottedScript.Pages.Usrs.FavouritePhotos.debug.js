Type.registerNamespace('SpottedScript.Pages.Usrs.FavouritePhotos');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Usrs.FavouritePhotos.Controller
SpottedScript.Pages.Usrs.FavouritePhotos.Controller = function SpottedScript_Pages_Usrs_FavouritePhotos_Controller(view) {
    /// <param name="view" type="SpottedScript.Pages.Usrs.FavouritePhotos.View">
    /// </param>
    /// <field name="_view$1" type="SpottedScript.Pages.Usrs.FavouritePhotos.View">
    /// </field>
    /// <field name="_photoProvider$1" type="SpottedScript.Controls.PhotoBrowser.PhotoProvider">
    /// </field>
    SpottedScript.Pages.Usrs.FavouritePhotos.Controller.initializeBase(this);
    this._view$1 = view;
    this.setupController();
}
SpottedScript.Pages.Usrs.FavouritePhotos.Controller.prototype = {
    _view$1: null,
    get_photoControl: function SpottedScript_Pages_Usrs_FavouritePhotos_Controller$get_photoControl() {
        /// <value type="SpottedScript.Controls.PhotoControl.Controller"></value>
        return this._view$1.get_uiPhotoControl();
    },
    get_photoBrowser: function SpottedScript_Pages_Usrs_FavouritePhotos_Controller$get_photoBrowser() {
        /// <value type="SpottedScript.Controls.PhotoBrowser.Controller"></value>
        return this._view$1.get_uiPhotoBrowser();
    },
    get_threadControl: function SpottedScript_Pages_Usrs_FavouritePhotos_Controller$get_threadControl() {
        /// <value type="SpottedScript.Controls.ThreadControl.Controller"></value>
        return this._view$1.get_uiThreadControl();
    },
    _photoProvider$1: null,
    get_photoProvider: function SpottedScript_Pages_Usrs_FavouritePhotos_Controller$get_photoProvider() {
        /// <value type="SpottedScript.Controls.PhotoBrowser.PhotoProvider"></value>
        return this._photoProvider$1 || (this._photoProvider$1 = new SpottedScript.Controls.PhotoBrowser.FavouritePhotosOfUsrProvider(Number.parseInvariant(this._view$1.get_uiUsrK().value)));
    },
    get_latestChatController: function SpottedScript_Pages_Usrs_FavouritePhotos_Controller$get_latestChatController() {
        /// <value type="SpottedScript.Controls.LatestChat.Controller"></value>
        return this._view$1.get_uiLatestChat();
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Usrs.FavouritePhotos.View
SpottedScript.Pages.Usrs.FavouritePhotos.View = function SpottedScript_Pages_Usrs_FavouritePhotos_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Usrs.FavouritePhotos.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Usrs.FavouritePhotos.View.prototype = {
    clientId: null,
    get_uiTitle: function SpottedScript_Pages_Usrs_FavouritePhotos_View$get_uiTitle() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiTitle');
    },
    get_uiPhotoBrowser: function SpottedScript_Pages_Usrs_FavouritePhotos_View$get_uiPhotoBrowser() {
        /// <value type="SpottedScript.Controls.PhotoBrowser.Controller"></value>
        return eval(this.clientId + '_uiPhotoBrowserController');
    },
    get_uiPhotoControl: function SpottedScript_Pages_Usrs_FavouritePhotos_View$get_uiPhotoControl() {
        /// <value type="SpottedScript.Controls.PhotoControl.Controller"></value>
        return eval(this.clientId + '_uiPhotoControlController');
    },
    get_uiUpdatePanel: function SpottedScript_Pages_Usrs_FavouritePhotos_View$get_uiUpdatePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiUpdatePanel');
    },
    get_uiLatestChat: function SpottedScript_Pages_Usrs_FavouritePhotos_View$get_uiLatestChat() {
        /// <value type="SpottedScript.Controls.LatestChat.Controller"></value>
        return eval(this.clientId + '_uiLatestChatController');
    },
    get_uiThreadControl: function SpottedScript_Pages_Usrs_FavouritePhotos_View$get_uiThreadControl() {
        /// <value type="SpottedScript.Controls.ThreadControl.Controller"></value>
        return eval(this.clientId + '_uiThreadControlController');
    },
    get_uiUsrK: function SpottedScript_Pages_Usrs_FavouritePhotos_View$get_uiUsrK() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiUsrK');
    },
    get_genericContainerPage: function SpottedScript_Pages_Usrs_FavouritePhotos_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Usrs.FavouritePhotos.Controller.registerClass('SpottedScript.Pages.Usrs.FavouritePhotos.Controller', SpottedScript.Controls.PhotoBrowser.PhotosController);
SpottedScript.Pages.Usrs.FavouritePhotos.View.registerClass('SpottedScript.Pages.Usrs.FavouritePhotos.View', SpottedScript.DsiUserControl.View);
