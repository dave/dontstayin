Type.registerNamespace('SpottedScript.Pages.Groups.Home');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Groups.Home.View
SpottedScript.Pages.Groups.Home.View = function SpottedScript_Pages_Groups_Home_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Groups.Home.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Groups.Home.View.prototype = {
    clientId: null,
    get_panelGroup: function SpottedScript_Pages_Groups_Home_View$get_panelGroup() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelGroup');
    },
    get_panelPrivate: function SpottedScript_Pages_Groups_Home_View$get_panelPrivate() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelPrivate');
    },
    get_infoNameLabel: function SpottedScript_Pages_Groups_Home_View$get_infoNameLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InfoNameLabel');
    },
    get_infoMembersLink: function SpottedScript_Pages_Groups_Home_View$get_infoMembersLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InfoMembersLink');
    },
    get_privacySpan: function SpottedScript_Pages_Groups_Home_View$get_privacySpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PrivacySpan');
    },
    get_infoLeaveButton: function SpottedScript_Pages_Groups_Home_View$get_infoLeaveButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InfoLeaveButton');
    },
    get_infoInviteRejectButton: function SpottedScript_Pages_Groups_Home_View$get_infoInviteRejectButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InfoInviteRejectButton');
    },
    get_infoJoinLoggedOutP: function SpottedScript_Pages_Groups_Home_View$get_infoJoinLoggedOutP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InfoJoinLoggedOutP');
    },
    get_infoJoinP: function SpottedScript_Pages_Groups_Home_View$get_infoJoinP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InfoJoinP');
    },
    get_infoLeaveP: function SpottedScript_Pages_Groups_Home_View$get_infoLeaveP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InfoLeaveP');
    },
    get_infoMemberStatusP: function SpottedScript_Pages_Groups_Home_View$get_infoMemberStatusP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InfoMemberStatusP');
    },
    get_infoInviteP: function SpottedScript_Pages_Groups_Home_View$get_infoInviteP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InfoInviteP');
    },
    get_infoFavouriteGroupButtonPanel: function SpottedScript_Pages_Groups_Home_View$get_infoFavouriteGroupButtonPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InfoFavouriteGroupButtonPanel');
    },
    get_commentAlertButtonPanel: function SpottedScript_Pages_Groups_Home_View$get_commentAlertButtonPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CommentAlertButtonPanel');
    },
    get_latest: function SpottedScript_Pages_Groups_Home_View$get_latest() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Latest');
    },
    get_h13: function SpottedScript_Pages_Groups_Home_View$get_h13() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H13');
    },
    get_miscInfoPanel: function SpottedScript_Pages_Groups_Home_View$get_miscInfoPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_MiscInfoPanel');
    },
    get_h1: function SpottedScript_Pages_Groups_Home_View$get_h1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H1');
    },
    get_groupName: function SpottedScript_Pages_Groups_Home_View$get_groupName() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GroupName');
    },
    get_groupPicCell: function SpottedScript_Pages_Groups_Home_View$get_groupPicCell() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GroupPicCell');
    },
    get_groupPicImg: function SpottedScript_Pages_Groups_Home_View$get_groupPicImg() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GroupPicImg');
    },
    get_publicChatP: function SpottedScript_Pages_Groups_Home_View$get_publicChatP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PublicChatP');
    },
    get_publicChatLink: function SpottedScript_Pages_Groups_Home_View$get_publicChatLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PublicChatLink');
    },
    get_publicChatLinkLabel: function SpottedScript_Pages_Groups_Home_View$get_publicChatLinkLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PublicChatLinkLabel');
    },
    get_groupChatP: function SpottedScript_Pages_Groups_Home_View$get_groupChatP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GroupChatP');
    },
    get_groupChatLink: function SpottedScript_Pages_Groups_Home_View$get_groupChatLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GroupChatLink');
    },
    get_groupChatLinkLabel: function SpottedScript_Pages_Groups_Home_View$get_groupChatLinkLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GroupChatLinkLabel');
    },
    get_calendarP: function SpottedScript_Pages_Groups_Home_View$get_calendarP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CalendarP');
    },
    get_calendarLink: function SpottedScript_Pages_Groups_Home_View$get_calendarLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CalendarLink');
    },
    get_calendarLinkLabel: function SpottedScript_Pages_Groups_Home_View$get_calendarLinkLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CalendarLinkLabel');
    },
    get_hotTicketsP: function SpottedScript_Pages_Groups_Home_View$get_hotTicketsP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_HotTicketsP');
    },
    get_hotTicketsLink: function SpottedScript_Pages_Groups_Home_View$get_hotTicketsLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_HotTicketsLink');
    },
    get_hotTicketsLinkLabel: function SpottedScript_Pages_Groups_Home_View$get_hotTicketsLinkLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_HotTicketsLinkLabel');
    },
    get_ticketsP: function SpottedScript_Pages_Groups_Home_View$get_ticketsP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketsP');
    },
    get_ticketsLink: function SpottedScript_Pages_Groups_Home_View$get_ticketsLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketsLink');
    },
    get_ticketsLinkLabel: function SpottedScript_Pages_Groups_Home_View$get_ticketsLinkLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_TicketsLinkLabel');
    },
    get_freeGuestlistP: function SpottedScript_Pages_Groups_Home_View$get_freeGuestlistP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FreeGuestlistP');
    },
    get_freeGuestlistLink: function SpottedScript_Pages_Groups_Home_View$get_freeGuestlistLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FreeGuestlistLink');
    },
    get_freeGuestlistLinkLabel: function SpottedScript_Pages_Groups_Home_View$get_freeGuestlistLinkLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FreeGuestlistLinkLabel');
    },
    get_infoJoinButton: function SpottedScript_Pages_Groups_Home_View$get_infoJoinButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InfoJoinButton');
    },
    get_nextEventCell: function SpottedScript_Pages_Groups_Home_View$get_nextEventCell() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NextEventCell');
    },
    get_h19: function SpottedScript_Pages_Groups_Home_View$get_h19() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H19');
    },
    get_nextEventDataList: function SpottedScript_Pages_Groups_Home_View$get_nextEventDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NextEventDataList');
    },
    get_htmlPanel: function SpottedScript_Pages_Groups_Home_View$get_htmlPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_HtmlPanel');
    },
    get_h11: function SpottedScript_Pages_Groups_Home_View$get_h11() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H11');
    },
    get_groupName1: function SpottedScript_Pages_Groups_Home_View$get_groupName1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GroupName1');
    },
    get_uiCompetitionPanel1: function SpottedScript_Pages_Groups_Home_View$get_uiCompetitionPanel1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiCompetitionPanel1');
    },
    get_captionCompetitionPanel: function SpottedScript_Pages_Groups_Home_View$get_captionCompetitionPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CaptionCompetitionPanel');
    },
    get_h2: function SpottedScript_Pages_Groups_Home_View$get_h2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H2');
    },
    get_captionCompetitionPhotoDataList: function SpottedScript_Pages_Groups_Home_View$get_captionCompetitionPhotoDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CaptionCompetitionPhotoDataList');
    },
    get_uiCompetitionPanel2: function SpottedScript_Pages_Groups_Home_View$get_uiCompetitionPanel2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiCompetitionPanel2');
    },
    get_groupPhotoPanel: function SpottedScript_Pages_Groups_Home_View$get_groupPhotoPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GroupPhotoPanel');
    },
    get_uiTopPhotosHeader: function SpottedScript_Pages_Groups_Home_View$get_uiTopPhotosHeader() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiTopPhotosHeader');
    },
    get_uiTopPhotosDiv: function SpottedScript_Pages_Groups_Home_View$get_uiTopPhotosDiv() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiTopPhotosDiv');
    },
    get_groupPhotoDataList: function SpottedScript_Pages_Groups_Home_View$get_groupPhotoDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GroupPhotoDataList');
    },
    get_groupPhotoArchiveLinkP: function SpottedScript_Pages_Groups_Home_View$get_groupPhotoArchiveLinkP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GroupPhotoArchiveLinkP');
    },
    get_uiCompetitionPanel3: function SpottedScript_Pages_Groups_Home_View$get_uiCompetitionPanel3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiCompetitionPanel3');
    },
    get_groupPhotoModPanelPanel: function SpottedScript_Pages_Groups_Home_View$get_groupPhotoModPanelPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GroupPhotoModPanelPanel');
    },
    get_h3: function SpottedScript_Pages_Groups_Home_View$get_h3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H3');
    },
    get_competitionPhotoPanel: function SpottedScript_Pages_Groups_Home_View$get_competitionPhotoPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CompetitionPhotoPanel');
    },
    get_uiCompetitionPhotosHeader: function SpottedScript_Pages_Groups_Home_View$get_uiCompetitionPhotosHeader() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_uiCompetitionPhotosHeader');
    },
    get_competitionPhotosDataGrid: function SpottedScript_Pages_Groups_Home_View$get_competitionPhotosDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CompetitionPhotosDataGrid');
    },
    get_genericContainerPage: function SpottedScript_Pages_Groups_Home_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Groups.Home.View.registerClass('SpottedScript.Pages.Groups.Home.View', SpottedScript.DsiUserControl.View);
