Type.registerNamespace('SpottedScript.Pages.Usrs.Home');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Usrs.Home.View
SpottedScript.Pages.Usrs.Home.View = function SpottedScript_Pages_Usrs_Home_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Usrs.Home.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Usrs.Home.View.prototype = {
    clientId: null,
    get_panelNoDetails: function SpottedScript_Pages_Usrs_Home_View$get_panelNoDetails() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelNoDetails');
    },
    get_panelDetails: function SpottedScript_Pages_Usrs_Home_View$get_panelDetails() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelDetails');
    },
    get_h11: function SpottedScript_Pages_Usrs_Home_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_picCell: function SpottedScript_Pages_Usrs_Home_View$get_picCell() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PicCell');
    },
    get_picAnchor: function SpottedScript_Pages_Usrs_Home_View$get_picAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PicAnchor');
    },
    get_picImg: function SpottedScript_Pages_Usrs_Home_View$get_picImg() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PicImg');
    },
    get_newUserIconPanel: function SpottedScript_Pages_Usrs_Home_View$get_newUserIconPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NewUserIconPanel');
    },
    get_adminIconPanel: function SpottedScript_Pages_Usrs_Home_View$get_adminIconPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AdminIconPanel');
    },
    get_eventModeratorIconPanel: function SpottedScript_Pages_Usrs_Home_View$get_eventModeratorIconPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventModeratorIconPanel');
    },
    get_moderatorIconPanel: function SpottedScript_Pages_Usrs_Home_View$get_moderatorIconPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ModeratorIconPanel');
    },
    get_discussionModeratorIconPanel: function SpottedScript_Pages_Usrs_Home_View$get_discussionModeratorIconPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DiscussionModeratorIconPanel');
    },
    get_superIconPanel: function SpottedScript_Pages_Usrs_Home_View$get_superIconPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SuperIconPanel');
    },
    get_djIconPanel: function SpottedScript_Pages_Usrs_Home_View$get_djIconPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DjIconPanel');
    },
    get_spotterIconPanel: function SpottedScript_Pages_Usrs_Home_View$get_spotterIconPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterIconPanel');
    },
    get_spotterLink: function SpottedScript_Pages_Usrs_Home_View$get_spotterLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterLink');
    },
    get_spotterIcon: function SpottedScript_Pages_Usrs_Home_View$get_spotterIcon() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterIcon');
    },
    get_spotterLabel: function SpottedScript_Pages_Usrs_Home_View$get_spotterLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterLabel');
    },
    get_spotterSpottingsLabel: function SpottedScript_Pages_Usrs_Home_View$get_spotterSpottingsLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpotterSpottingsLabel');
    },
    get_extraIconPanel: function SpottedScript_Pages_Usrs_Home_View$get_extraIconPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ExtraIconPanel');
    },
    get_ticketIconPanel: function SpottedScript_Pages_Usrs_Home_View$get_ticketIconPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketIconPanel');
    },
    get_chatterboxIconPanel: function SpottedScript_Pages_Usrs_Home_View$get_chatterboxIconPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ChatterboxIconPanel');
    },
    get_dsiRegularIconPanel: function SpottedScript_Pages_Usrs_Home_View$get_dsiRegularIconPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DsiRegularIconPanel');
    },
    get_donationIconTopP: function SpottedScript_Pages_Usrs_Home_View$get_donationIconTopP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DonationIconTopP');
    },
    get_donationIconLegendP: function SpottedScript_Pages_Usrs_Home_View$get_donationIconLegendP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DonationIconLegendP');
    },
    get_otherUsrPanel: function SpottedScript_Pages_Usrs_Home_View$get_otherUsrPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_OtherUsrPanel');
    },
    get_groupInviteAnchor: function SpottedScript_Pages_Usrs_Home_View$get_groupInviteAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GroupInviteAnchor');
    },
    get_usrChatArchiveAnchorP: function SpottedScript_Pages_Usrs_Home_View$get_usrChatArchiveAnchorP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrChatArchiveAnchorP');
    },
    get_usrChatArchiveAnchor: function SpottedScript_Pages_Usrs_Home_View$get_usrChatArchiveAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrChatArchiveAnchor');
    },
    get_chatCell: function SpottedScript_Pages_Usrs_Home_View$get_chatCell() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ChatCell');
    },
    get_h15: function SpottedScript_Pages_Usrs_Home_View$get_h15() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H15');
    },
    get_uiDonationIconsPanel: function SpottedScript_Pages_Usrs_Home_View$get_uiDonationIconsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiDonationIconsPanel');
    },
    get_h1: function SpottedScript_Pages_Usrs_Home_View$get_h1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1');
    },
    get_uiDonationIconsHtml: function SpottedScript_Pages_Usrs_Home_View$get_uiDonationIconsHtml() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiDonationIconsHtml');
    },
    get_donationRolloverP: function SpottedScript_Pages_Usrs_Home_View$get_donationRolloverP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DonationRolloverP');
    },
    get_donationRolloverImage: function SpottedScript_Pages_Usrs_Home_View$get_donationRolloverImage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DonationRolloverImage');
    },
    get_donationRolloverDrop: function SpottedScript_Pages_Usrs_Home_View$get_donationRolloverDrop() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_DonationRolloverDrop');
    },
    get_personalStatementUpdatePanel: function SpottedScript_Pages_Usrs_Home_View$get_personalStatementUpdatePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PersonalStatementUpdatePanel');
    },
    get_editPersonalStatementPanel: function SpottedScript_Pages_Usrs_Home_View$get_editPersonalStatementPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EditPersonalStatementPanel');
    },
    get_h14: function SpottedScript_Pages_Usrs_Home_View$get_h14() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H14');
    },
    get_personalStatementHtml: function SpottedScript_Pages_Usrs_Home_View$get_personalStatementHtml() {
        /// <value type="SpottedScript.Controls.Html.Controller"></value>
        return eval(this.clientId + '_PersonalStatementHtmlController');
    },
    get_personalStatementPanel: function SpottedScript_Pages_Usrs_Home_View$get_personalStatementPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PersonalStatementPanel');
    },
    get_h16: function SpottedScript_Pages_Usrs_Home_View$get_h16() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H16');
    },
    get_usrCommentsLabel: function SpottedScript_Pages_Usrs_Home_View$get_usrCommentsLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrCommentsLabel');
    },
    get_usrChatLabel: function SpottedScript_Pages_Usrs_Home_View$get_usrChatLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrChatLabel');
    },
    get_usrPhotoLabel: function SpottedScript_Pages_Usrs_Home_View$get_usrPhotoLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrPhotoLabel');
    },
    get_usrMusicTypesPanel: function SpottedScript_Pages_Usrs_Home_View$get_usrMusicTypesPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrMusicTypesPanel');
    },
    get_usrMusicTypesLabel: function SpottedScript_Pages_Usrs_Home_View$get_usrMusicTypesLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrMusicTypesLabel');
    },
    get_usrPlaceVisitPanel: function SpottedScript_Pages_Usrs_Home_View$get_usrPlaceVisitPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrPlaceVisitPanel');
    },
    get_usrPlaceVisitLabel: function SpottedScript_Pages_Usrs_Home_View$get_usrPlaceVisitLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrPlaceVisitLabel');
    },
    get_favouriteGroupsPanel: function SpottedScript_Pages_Usrs_Home_View$get_favouriteGroupsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FavouriteGroupsPanel');
    },
    get_favouriteGroupsDataList: function SpottedScript_Pages_Usrs_Home_View$get_favouriteGroupsDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FavouriteGroupsDataList');
    },
    get_sendMessagePanel: function SpottedScript_Pages_Usrs_Home_View$get_sendMessagePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SendMessagePanel');
    },
    get_h17: function SpottedScript_Pages_Usrs_Home_View$get_h17() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H17');
    },
    get_requiredfieldvalidator1: function SpottedScript_Pages_Usrs_Home_View$get_requiredfieldvalidator1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Requiredfieldvalidator1');
    },
    get_requiredfieldvalidator2: function SpottedScript_Pages_Usrs_Home_View$get_requiredfieldvalidator2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Requiredfieldvalidator2');
    },
    get_addThreadSubjectTextBox: function SpottedScript_Pages_Usrs_Home_View$get_addThreadSubjectTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadSubjectTextBox');
    },
    get_addThreadCommentHtml: function SpottedScript_Pages_Usrs_Home_View$get_addThreadCommentHtml() {
        /// <value type="SpottedScript.Controls.Html.Controller"></value>
        return eval(this.clientId + '_AddThreadCommentHtmlController');
    },
    get_addThreadLoginPanel: function SpottedScript_Pages_Usrs_Home_View$get_addThreadLoginPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadLoginPanel');
    },
    get_addThreadEmailVerifyPanel: function SpottedScript_Pages_Usrs_Home_View$get_addThreadEmailVerifyPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadEmailVerifyPanel');
    },
    get_addThreadAddBuddyP: function SpottedScript_Pages_Usrs_Home_View$get_addThreadAddBuddyP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadAddBuddyP');
    },
    get_addThreadAddBuddy: function SpottedScript_Pages_Usrs_Home_View$get_addThreadAddBuddy() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddThreadAddBuddy');
    },
    get_photosMePanel: function SpottedScript_Pages_Usrs_Home_View$get_photosMePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotosMePanel');
    },
    get_h112: function SpottedScript_Pages_Usrs_Home_View$get_h112() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H112');
    },
    get_photosMeDataList: function SpottedScript_Pages_Usrs_Home_View$get_photosMeDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotosMeDataList');
    },
    get_photosMeShowAllLinkPanel: function SpottedScript_Pages_Usrs_Home_View$get_photosMeShowAllLinkPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PhotosMeShowAllLinkPanel');
    },
    get_favouritePhotoPanel: function SpottedScript_Pages_Usrs_Home_View$get_favouritePhotoPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FavouritePhotoPanel');
    },
    get_h12: function SpottedScript_Pages_Usrs_Home_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_favouritePhotoDataList: function SpottedScript_Pages_Usrs_Home_View$get_favouritePhotoDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FavouritePhotoDataList');
    },
    get_favouritePhotosShowAllLinkPanel: function SpottedScript_Pages_Usrs_Home_View$get_favouritePhotosShowAllLinkPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FavouritePhotosShowAllLinkPanel');
    },
    get_eventsAttendFuturePanel: function SpottedScript_Pages_Usrs_Home_View$get_eventsAttendFuturePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventsAttendFuturePanel');
    },
    get_h113: function SpottedScript_Pages_Usrs_Home_View$get_h113() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H113');
    },
    get_eventsAttendFutureDataList: function SpottedScript_Pages_Usrs_Home_View$get_eventsAttendFutureDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventsAttendFutureDataList');
    },
    get_eventsAttendedPanel: function SpottedScript_Pages_Usrs_Home_View$get_eventsAttendedPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventsAttendedPanel');
    },
    get_h114: function SpottedScript_Pages_Usrs_Home_View$get_h114() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H114');
    },
    get_eventsAttendedDataList: function SpottedScript_Pages_Usrs_Home_View$get_eventsAttendedDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_EventsAttendedDataList');
    },
    get_galleriesPanel: function SpottedScript_Pages_Usrs_Home_View$get_galleriesPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GalleriesPanel');
    },
    get_h111: function SpottedScript_Pages_Usrs_Home_View$get_h111() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H111');
    },
    get_uiRecentGalleries: function SpottedScript_Pages_Usrs_Home_View$get_uiRecentGalleries() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiRecentGalleries');
    },
    get_usrsSpottedPanel: function SpottedScript_Pages_Usrs_Home_View$get_usrsSpottedPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrsSpottedPanel');
    },
    get_h18: function SpottedScript_Pages_Usrs_Home_View$get_h18() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H18');
    },
    get_usrsSpottedStatsP: function SpottedScript_Pages_Usrs_Home_View$get_usrsSpottedStatsP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrsSpottedStatsP');
    },
    get_usrsSpottedDataList: function SpottedScript_Pages_Usrs_Home_View$get_usrsSpottedDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrsSpottedDataList');
    },
    get_usrsSpottedShowAllLinkPanel: function SpottedScript_Pages_Usrs_Home_View$get_usrsSpottedShowAllLinkPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrsSpottedShowAllLinkPanel');
    },
    get_usrsSpottedFootnoteP: function SpottedScript_Pages_Usrs_Home_View$get_usrsSpottedFootnoteP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_UsrsSpottedFootnoteP');
    },
    get_panelBan: function SpottedScript_Pages_Usrs_Home_View$get_panelBan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelBan');
    },
    get_h19: function SpottedScript_Pages_Usrs_Home_View$get_h19() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H19');
    },
    get_banReasonTextBox: function SpottedScript_Pages_Usrs_Home_View$get_banReasonTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BanReasonTextBox');
    },
    get_banButton: function SpottedScript_Pages_Usrs_Home_View$get_banButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_BanButton');
    },
    get_genericContainerPage: function SpottedScript_Pages_Usrs_Home_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Usrs.Home.View.registerClass('SpottedScript.Pages.Usrs.Home.View', SpottedScript.Pages.Usrs.UsrUserControl.View);
