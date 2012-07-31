Type.registerNamespace('SpottedScript.Pages.Groups.Photos');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Groups.Photos.Controller
SpottedScript.Pages.Groups.Photos.Controller = function SpottedScript_Pages_Groups_Photos_Controller(view) {
    /// <param name="view" type="SpottedScript.Pages.Groups.Photos.View">
    /// </param>
    /// <field name="_view$1" type="SpottedScript.Pages.Groups.Photos.View">
    /// </field>
    /// <field name="_photoProvider$1" type="SpottedScript.Controls.PhotoBrowser.PhotoProvider">
    /// </field>
    SpottedScript.Pages.Groups.Photos.Controller.initializeBase(this);
    this._view$1 = view;
    this.setupController();
}
SpottedScript.Pages.Groups.Photos.Controller.prototype = {
    _view$1: null,
    get_photoControl: function SpottedScript_Pages_Groups_Photos_Controller$get_photoControl() {
        /// <value type="SpottedScript.Controls.PhotoControl.Controller"></value>
        return this._view$1.get_uiPhotoControl();
    },
    get_photoBrowser: function SpottedScript_Pages_Groups_Photos_Controller$get_photoBrowser() {
        /// <value type="SpottedScript.Controls.PhotoBrowser.Controller"></value>
        return this._view$1.get_uiPhotoBrowser();
    },
    get_threadControl: function SpottedScript_Pages_Groups_Photos_Controller$get_threadControl() {
        /// <value type="SpottedScript.Controls.ThreadControl.Controller"></value>
        return this._view$1.get_uiThreadControl();
    },
    _photoProvider$1: null,
    get_photoProvider: function SpottedScript_Pages_Groups_Photos_Controller$get_photoProvider() {
        /// <value type="SpottedScript.Controls.PhotoBrowser.PhotoProvider"></value>
        return this._photoProvider$1 || (this._photoProvider$1 = new SpottedScript.Controls.PhotoBrowser.GroupPhotoProvider(Number.parseInvariant(this._view$1.get_uiGroupK().value)));
    },
    get_latestChatController: function SpottedScript_Pages_Groups_Photos_Controller$get_latestChatController() {
        /// <value type="SpottedScript.Controls.LatestChat.Controller"></value>
        return this._view$1.get_uiLatestChat();
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Groups.Photos.View
SpottedScript.Pages.Groups.Photos.View = function SpottedScript_Pages_Groups_Photos_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Groups.Photos.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Groups.Photos.View.prototype = {
    clientId: null,
    get_uiTitle: function SpottedScript_Pages_Groups_Photos_View$get_uiTitle() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiTitle');
    },
    get_uiPhotoBrowser: function SpottedScript_Pages_Groups_Photos_View$get_uiPhotoBrowser() {
        /// <value type="SpottedScript.Controls.PhotoBrowser.Controller"></value>
        return eval(this.clientId + '_uiPhotoBrowserController');
    },
    get_uiPhotoControl: function SpottedScript_Pages_Groups_Photos_View$get_uiPhotoControl() {
        /// <value type="SpottedScript.Controls.PhotoControl.Controller"></value>
        return eval(this.clientId + '_uiPhotoControlController');
    },
    get_uiUpdatePanel: function SpottedScript_Pages_Groups_Photos_View$get_uiUpdatePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiUpdatePanel');
    },
    get_uiLatestChat: function SpottedScript_Pages_Groups_Photos_View$get_uiLatestChat() {
        /// <value type="SpottedScript.Controls.LatestChat.Controller"></value>
        return eval(this.clientId + '_uiLatestChatController');
    },
    get_uiThreadControl: function SpottedScript_Pages_Groups_Photos_View$get_uiThreadControl() {
        /// <value type="SpottedScript.Controls.ThreadControl.Controller"></value>
        return eval(this.clientId + '_uiThreadControlController');
    },
    get_uiGroupK: function SpottedScript_Pages_Groups_Photos_View$get_uiGroupK() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiGroupK');
    },
    get_genericContainerPage: function SpottedScript_Pages_Groups_Photos_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Groups.Photos.Controller.registerClass('SpottedScript.Pages.Groups.Photos.Controller', SpottedScript.Controls.PhotoBrowser.PhotosController);
SpottedScript.Pages.Groups.Photos.View.registerClass('SpottedScript.Pages.Groups.Photos.View', SpottedScript.DsiUserControl.View);
