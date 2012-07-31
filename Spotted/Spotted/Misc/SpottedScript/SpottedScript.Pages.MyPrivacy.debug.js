Type.registerNamespace('SpottedScript.Pages.MyPrivacy');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.MyPrivacy.View
SpottedScript.Pages.MyPrivacy.View = function SpottedScript_Pages_MyPrivacy_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.MyPrivacy.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.MyPrivacy.View.prototype = {
    clientId: null,
    get_panelChange: function SpottedScript_Pages_MyPrivacy_View$get_panelChange() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelChange');
    },
    get_sendSpottedEmails: function SpottedScript_Pages_MyPrivacy_View$get_sendSpottedEmails() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SendSpottedEmails');
    },
    get_sendInvites: function SpottedScript_Pages_MyPrivacy_View$get_sendInvites() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SendInvites');
    },
    get_h17a: function SpottedScript_Pages_MyPrivacy_View$get_h17a() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H17a');
    },
    get_facebookStory: function SpottedScript_Pages_MyPrivacy_View$get_facebookStory() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FacebookStory');
    },
    get_facebookStoryPanel: function SpottedScript_Pages_MyPrivacy_View$get_facebookStoryPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FacebookStoryPanel');
    },
    get_facebookStoryAttendEvent: function SpottedScript_Pages_MyPrivacy_View$get_facebookStoryAttendEvent() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FacebookStoryAttendEvent');
    },
    get_facebookStorySpotted: function SpottedScript_Pages_MyPrivacy_View$get_facebookStorySpotted() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FacebookStorySpotted');
    },
    get_facebookStoryUploadPhoto: function SpottedScript_Pages_MyPrivacy_View$get_facebookStoryUploadPhoto() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FacebookStoryUploadPhoto');
    },
    get_facebookStoryPhotoFeatured: function SpottedScript_Pages_MyPrivacy_View$get_facebookStoryPhotoFeatured() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FacebookStoryPhotoFeatured');
    },
    get_facebookStoryNewTopic: function SpottedScript_Pages_MyPrivacy_View$get_facebookStoryNewTopic() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FacebookStoryNewTopic');
    },
    get_facebookStoryLaugh: function SpottedScript_Pages_MyPrivacy_View$get_facebookStoryLaugh() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FacebookStoryLaugh');
    },
    get_facebookStoryFavourite: function SpottedScript_Pages_MyPrivacy_View$get_facebookStoryFavourite() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FacebookStoryFavourite');
    },
    get_facebookStoryFavouriteTopic: function SpottedScript_Pages_MyPrivacy_View$get_facebookStoryFavouriteTopic() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FacebookStoryFavouriteTopic');
    },
    get_facebookStoryPostNews: function SpottedScript_Pages_MyPrivacy_View$get_facebookStoryPostNews() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FacebookStoryPostNews');
    },
    get_facebookStoryEventReview: function SpottedScript_Pages_MyPrivacy_View$get_facebookStoryEventReview() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FacebookStoryEventReview');
    },
    get_facebookStoryPublishArticle: function SpottedScript_Pages_MyPrivacy_View$get_facebookStoryPublishArticle() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FacebookStoryPublishArticle');
    },
    get_facebookStoryNewBuddy: function SpottedScript_Pages_MyPrivacy_View$get_facebookStoryNewBuddy() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FacebookStoryNewBuddy');
    },
    get_facebookStoryJoinGroup: function SpottedScript_Pages_MyPrivacy_View$get_facebookStoryJoinGroup() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FacebookStoryJoinGroup');
    },
    get_facebookStoryBuyTicket: function SpottedScript_Pages_MyPrivacy_View$get_facebookStoryBuyTicket() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FacebookStoryBuyTicket');
    },
    get_facebookEventAdd: function SpottedScript_Pages_MyPrivacy_View$get_facebookEventAdd() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FacebookEventAdd');
    },
    get_facebookEventAttend: function SpottedScript_Pages_MyPrivacy_View$get_facebookEventAttend() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FacebookEventAttend');
    },
    get_h17: function SpottedScript_Pages_MyPrivacy_View$get_h17() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H17');
    },
    get_inboxEmailsCheckBox: function SpottedScript_Pages_MyPrivacy_View$get_inboxEmailsCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InboxEmailsCheckBox');
    },
    get_h18: function SpottedScript_Pages_MyPrivacy_View$get_h18() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H18');
    },
    get_unsubscribeCheckBox: function SpottedScript_Pages_MyPrivacy_View$get_unsubscribeCheckBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UnsubscribeCheckBox');
    },
    get_h19: function SpottedScript_Pages_MyPrivacy_View$get_h19() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H19');
    },
    get_h12: function SpottedScript_Pages_MyPrivacy_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_enhancedSecurity: function SpottedScript_Pages_MyPrivacy_View$get_enhancedSecurity() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EnhancedSecurity');
    },
    get_exDirectory: function SpottedScript_Pages_MyPrivacy_View$get_exDirectory() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ExDirectory');
    },
    get_prefsUpdateButton: function SpottedScript_Pages_MyPrivacy_View$get_prefsUpdateButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PrefsUpdateButton');
    },
    get_successLabel: function SpottedScript_Pages_MyPrivacy_View$get_successLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SuccessLabel');
    },
    get_genericContainerPage: function SpottedScript_Pages_MyPrivacy_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.MyPrivacy.View.registerClass('SpottedScript.Pages.MyPrivacy.View', SpottedScript.DsiUserControl.View);
