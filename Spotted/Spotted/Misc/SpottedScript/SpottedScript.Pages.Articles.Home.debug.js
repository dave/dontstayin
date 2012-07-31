Type.registerNamespace('SpottedScript.Pages.Articles.Home');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Articles.Home.Controller
SpottedScript.Pages.Articles.Home.Controller = function SpottedScript_Pages_Articles_Home_Controller(view) {
    /// <param name="view" type="SpottedScript.Pages.Articles.Home.View">
    /// </param>
    view.get_threadControl().get_uiCommentsDisplay().showComments(Number.parseInvariant(view.get_uiThreadK().value), Number.parseInvariant(view.get_uiPageNumber().value));
    view.get_threadControl().set_currentParentObjectK(Number.parseInvariant(view.get_uiArticleK().value));
}
////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Articles.Home.View
SpottedScript.Pages.Articles.Home.View = function SpottedScript_Pages_Articles_Home_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Articles.Home.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Articles.Home.View.prototype = {
    clientId: null,
    get_homeContent: function SpottedScript_Pages_Articles_Home_View$get_homeContent() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_HomeContent');
    },
    get_latestChat: function SpottedScript_Pages_Articles_Home_View$get_latestChat() {
        /// <value type="SpottedScript.Controls.LatestChat.Controller"></value>
        return eval(this.clientId + '_LatestChatController');
    },
    get_threadControl: function SpottedScript_Pages_Articles_Home_View$get_threadControl() {
        /// <value type="SpottedScript.Controls.ThreadControl.Controller"></value>
        return eval(this.clientId + '_ThreadControlController');
    },
    get_uiThreadK: function SpottedScript_Pages_Articles_Home_View$get_uiThreadK() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiThreadK');
    },
    get_uiArticleK: function SpottedScript_Pages_Articles_Home_View$get_uiArticleK() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiArticleK');
    },
    get_uiPageNumber: function SpottedScript_Pages_Articles_Home_View$get_uiPageNumber() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiPageNumber');
    },
    get_genericContainerPage: function SpottedScript_Pages_Articles_Home_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Articles.Home.Controller.registerClass('SpottedScript.Pages.Articles.Home.Controller');
SpottedScript.Pages.Articles.Home.View.registerClass('SpottedScript.Pages.Articles.Home.View', SpottedScript.DsiUserControl.View);
