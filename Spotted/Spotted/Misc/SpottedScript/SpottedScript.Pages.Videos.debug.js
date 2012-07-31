Type.registerNamespace('SpottedScript.Pages.Videos');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Videos.Controller
SpottedScript.Pages.Videos.Controller = function SpottedScript_Pages_Videos_Controller(view) {
    /// <param name="view" type="SpottedScript.Pages.Videos.View">
    /// </param>
    /// <field name="_view$1" type="SpottedScript.Pages.Videos.View">
    /// </field>
    /// <field name="_photoProvider$1" type="SpottedScript.Controls.PhotoBrowser.PhotoProvider">
    /// </field>
    SpottedScript.Pages.Videos.Controller.initializeBase(this);
    this._view$1 = view;
    this.setupController();
}
SpottedScript.Pages.Videos.Controller.prototype = {
    _view$1: null,
    get_photoControl: function SpottedScript_Pages_Videos_Controller$get_photoControl() {
        /// <value type="SpottedScript.Controls.PhotoControl.Controller"></value>
        return this._view$1.get_uiVideoControl();
    },
    get_photoBrowser: function SpottedScript_Pages_Videos_Controller$get_photoBrowser() {
        /// <value type="SpottedScript.Controls.PhotoBrowser.Controller"></value>
        return this._view$1.get_uiVideoBrowser();
    },
    get_threadControl: function SpottedScript_Pages_Videos_Controller$get_threadControl() {
        /// <value type="SpottedScript.Controls.ThreadControl.Controller"></value>
        return this._view$1.get_uiThreadControl();
    },
    _photoProvider$1: null,
    get_photoProvider: function SpottedScript_Pages_Videos_Controller$get_photoProvider() {
        /// <value type="SpottedScript.Controls.PhotoBrowser.PhotoProvider"></value>
        if (this._photoProvider$1 == null) {
            this._photoProvider$1 = new SpottedScript.Controls.PhotoBrowser.VideoPhotoProvider();
        }
        return this._photoProvider$1;
    },
    get_latestChatController: function SpottedScript_Pages_Videos_Controller$get_latestChatController() {
        /// <value type="SpottedScript.Controls.LatestChat.Controller"></value>
        return this._view$1.get_uiLatestChat();
    }
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Videos.View
SpottedScript.Pages.Videos.View = function SpottedScript_Pages_Videos_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Videos.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Videos.View.prototype = {
    clientId: null,
    get_uiTitle: function SpottedScript_Pages_Videos_View$get_uiTitle() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiTitle');
    },
    get_uiVideoBrowser: function SpottedScript_Pages_Videos_View$get_uiVideoBrowser() {
        /// <value type="SpottedScript.Controls.PhotoBrowser.Controller"></value>
        return eval(this.clientId + '_uiVideoBrowserController');
    },
    get_uiVideoControl: function SpottedScript_Pages_Videos_View$get_uiVideoControl() {
        /// <value type="SpottedScript.Controls.PhotoControl.Controller"></value>
        return eval(this.clientId + '_uiVideoControlController');
    },
    get_uiLatestChat: function SpottedScript_Pages_Videos_View$get_uiLatestChat() {
        /// <value type="SpottedScript.Controls.LatestChat.Controller"></value>
        return eval(this.clientId + '_uiLatestChatController');
    },
    get_uiThreadControl: function SpottedScript_Pages_Videos_View$get_uiThreadControl() {
        /// <value type="SpottedScript.Controls.ThreadControl.Controller"></value>
        return eval(this.clientId + '_uiThreadControlController');
    },
    get_genericContainerPage: function SpottedScript_Pages_Videos_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Videos.Controller.registerClass('SpottedScript.Pages.Videos.Controller', SpottedScript.Controls.PhotoBrowser.PhotosController);
SpottedScript.Pages.Videos.View.registerClass('SpottedScript.Pages.Videos.View', SpottedScript.DsiUserControl.View);
