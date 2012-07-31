Type.registerNamespace('SpottedScript.Pages.Spam');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Spam.View
SpottedScript.Pages.Spam.View = function SpottedScript_Pages_Spam_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Spam.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Spam.View.prototype = {
    clientId: null,
    get_inboxSpamPanel: function SpottedScript_Pages_Spam_View$get_inboxSpamPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InboxSpamPanel');
    },
    get_invitePanel: function SpottedScript_Pages_Spam_View$get_invitePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InvitePanel');
    },
    get_inviteGridView: function SpottedScript_Pages_Spam_View$get_inviteGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InviteGridView');
    },
    get_venueWatchPanel: function SpottedScript_Pages_Spam_View$get_venueWatchPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VenueWatchPanel');
    },
    get_venueWatchGridView: function SpottedScript_Pages_Spam_View$get_venueWatchGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_VenueWatchGridView');
    },
    get_brandWatchPanel: function SpottedScript_Pages_Spam_View$get_brandWatchPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BrandWatchPanel');
    },
    get_brandWatchGridView: function SpottedScript_Pages_Spam_View$get_brandWatchGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BrandWatchGridView');
    },
    get_eventWatchPanel: function SpottedScript_Pages_Spam_View$get_eventWatchPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventWatchPanel');
    },
    get_eventWatchGridView: function SpottedScript_Pages_Spam_View$get_eventWatchGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventWatchGridView');
    },
    get_groupWatchPanel: function SpottedScript_Pages_Spam_View$get_groupWatchPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GroupWatchPanel');
    },
    get_groupWatchGridView: function SpottedScript_Pages_Spam_View$get_groupWatchGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GroupWatchGridView');
    },
    get_groupNewsPanel: function SpottedScript_Pages_Spam_View$get_groupNewsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GroupNewsPanel');
    },
    get_groupNewsGridView: function SpottedScript_Pages_Spam_View$get_groupNewsGridView() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GroupNewsGridView');
    },
    get_watchPhotoPanel: function SpottedScript_Pages_Spam_View$get_watchPhotoPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_WatchPhotoPanel');
    },
    get_numberOfTopicsFromWatchingPhotosLabel: function SpottedScript_Pages_Spam_View$get_numberOfTopicsFromWatchingPhotosLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NumberOfTopicsFromWatchingPhotosLabel');
    },
    get_watchPhotoTopicOptionsPanel: function SpottedScript_Pages_Spam_View$get_watchPhotoTopicOptionsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_WatchPhotoTopicOptionsPanel');
    },
    get_photoTopicOptionsDropDownList: function SpottedScript_Pages_Spam_View$get_photoTopicOptionsDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotoTopicOptionsDropDownList');
    },
    get_photoOptionsUpdateButton: function SpottedScript_Pages_Spam_View$get_photoOptionsUpdateButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotoOptionsUpdateButton');
    },
    get_watchPhotoTopicOptionsLabel: function SpottedScript_Pages_Spam_View$get_watchPhotoTopicOptionsLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_WatchPhotoTopicOptionsLabel');
    },
    get_watchArticlePanel: function SpottedScript_Pages_Spam_View$get_watchArticlePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_WatchArticlePanel');
    },
    get_numberOfTopicsFromWatchingArticlesLabel: function SpottedScript_Pages_Spam_View$get_numberOfTopicsFromWatchingArticlesLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NumberOfTopicsFromWatchingArticlesLabel');
    },
    get_watchArticleTopicOptionsPanel: function SpottedScript_Pages_Spam_View$get_watchArticleTopicOptionsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_WatchArticleTopicOptionsPanel');
    },
    get_articleTopicOptionsDropDownList: function SpottedScript_Pages_Spam_View$get_articleTopicOptionsDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ArticleTopicOptionsDropDownList');
    },
    get_articleOptionsUpdateButton: function SpottedScript_Pages_Spam_View$get_articleOptionsUpdateButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ArticleOptionsUpdateButton');
    },
    get_watchArticleTopicOptionsLabel: function SpottedScript_Pages_Spam_View$get_watchArticleTopicOptionsLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_WatchArticleTopicOptionsLabel');
    },
    get_watchOtherThreadPanel: function SpottedScript_Pages_Spam_View$get_watchOtherThreadPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_WatchOtherThreadPanel');
    },
    get_numberOfTopicsFromWatchingOtherThreadLabel: function SpottedScript_Pages_Spam_View$get_numberOfTopicsFromWatchingOtherThreadLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NumberOfTopicsFromWatchingOtherThreadLabel');
    },
    get_watchOtherTopicOptionsPanel: function SpottedScript_Pages_Spam_View$get_watchOtherTopicOptionsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_WatchOtherTopicOptionsPanel');
    },
    get_otherTopicOptionsDropDownList: function SpottedScript_Pages_Spam_View$get_otherTopicOptionsDropDownList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OtherTopicOptionsDropDownList');
    },
    get_otherOptionsUpdateButton: function SpottedScript_Pages_Spam_View$get_otherOptionsUpdateButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OtherOptionsUpdateButton');
    },
    get_watchOtherTopicOptionsLabel: function SpottedScript_Pages_Spam_View$get_watchOtherTopicOptionsLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_WatchOtherTopicOptionsLabel');
    },
    get_refreshButton: function SpottedScript_Pages_Spam_View$get_refreshButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RefreshButton');
    },
    get_noInboxSpamPanel: function SpottedScript_Pages_Spam_View$get_noInboxSpamPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoInboxSpamPanel');
    },
    get_h1: function SpottedScript_Pages_Spam_View$get_h1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1');
    },
    get_refreshButton2: function SpottedScript_Pages_Spam_View$get_refreshButton2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_RefreshButton2');
    },
    get_genericContainerPage: function SpottedScript_Pages_Spam_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Spam.View.registerClass('SpottedScript.Pages.Spam.View', SpottedScript.DsiUserControl.View);
