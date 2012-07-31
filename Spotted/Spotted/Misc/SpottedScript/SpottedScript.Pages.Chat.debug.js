Type.registerNamespace('SpottedScript.Pages.Chat');

////////////////////////////////////////////////////////////////////////////////
// SpottedScript.Pages.Chat.View
SpottedScript.Pages.Chat.View = function SpottedScript_Pages_Chat_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    SpottedScript.Pages.Chat.View.initializeBase(this, [ clientId ]);
    this.clientId = clientId;
}
SpottedScript.Pages.Chat.View.prototype = {
    clientId: null,
    get_panelForum: function SpottedScript_Pages_Chat_View$get_panelForum() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelForum');
    },
    get_panelThreadDescTypeNone: function SpottedScript_Pages_Chat_View$get_panelThreadDescTypeNone() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelThreadDescTypeNone');
    },
    get_threadDescWorldwideHomeCountryLink: function SpottedScript_Pages_Chat_View$get_threadDescWorldwideHomeCountryLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadDescWorldwideHomeCountryLink');
    },
    get_panelThreadDescTypeCountry: function SpottedScript_Pages_Chat_View$get_panelThreadDescTypeCountry() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelThreadDescTypeCountry');
    },
    get_threadDescCountryLink: function SpottedScript_Pages_Chat_View$get_threadDescCountryLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadDescCountryLink');
    },
    get_threadDescCountryLabel: function SpottedScript_Pages_Chat_View$get_threadDescCountryLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadDescCountryLabel');
    },
    get_panelThreadDescTypeEvent: function SpottedScript_Pages_Chat_View$get_panelThreadDescTypeEvent() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelThreadDescTypeEvent');
    },
    get_threadDescEventEventLink: function SpottedScript_Pages_Chat_View$get_threadDescEventEventLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadDescEventEventLink');
    },
    get_threadDescEventVenueLink: function SpottedScript_Pages_Chat_View$get_threadDescEventVenueLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadDescEventVenueLink');
    },
    get_threadDescEventPlaceLink: function SpottedScript_Pages_Chat_View$get_threadDescEventPlaceLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadDescEventPlaceLink');
    },
    get_threadDescEventDateLabel: function SpottedScript_Pages_Chat_View$get_threadDescEventDateLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadDescEventDateLabel');
    },
    get_panelThreadDescTypeVenue: function SpottedScript_Pages_Chat_View$get_panelThreadDescTypeVenue() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelThreadDescTypeVenue');
    },
    get_threadDescVenueVenueLink: function SpottedScript_Pages_Chat_View$get_threadDescVenueVenueLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadDescVenueVenueLink');
    },
    get_threadDescVenuePlaceLink: function SpottedScript_Pages_Chat_View$get_threadDescVenuePlaceLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadDescVenuePlaceLink');
    },
    get_panelThreadDescTypePlace: function SpottedScript_Pages_Chat_View$get_panelThreadDescTypePlace() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelThreadDescTypePlace');
    },
    get_threadDescPlacePlaceLink: function SpottedScript_Pages_Chat_View$get_threadDescPlacePlaceLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadDescPlacePlaceLink');
    },
    get_panelThreadDescTypeArticle: function SpottedScript_Pages_Chat_View$get_panelThreadDescTypeArticle() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelThreadDescTypeArticle');
    },
    get_threadDescArticleArticleLink: function SpottedScript_Pages_Chat_View$get_threadDescArticleArticleLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadDescArticleArticleLink');
    },
    get_panelThreadDescTypeBrand: function SpottedScript_Pages_Chat_View$get_panelThreadDescTypeBrand() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelThreadDescTypeBrand');
    },
    get_threadDescBrandBrandLink: function SpottedScript_Pages_Chat_View$get_threadDescBrandBrandLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadDescBrandBrandLink');
    },
    get_panelThreadDescTypeGroup: function SpottedScript_Pages_Chat_View$get_panelThreadDescTypeGroup() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelThreadDescTypeGroup');
    },
    get_threadDescGroupGroupLink: function SpottedScript_Pages_Chat_View$get_threadDescGroupGroupLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadDescGroupGroupLink');
    },
    get_panelThreadDescGroupBrandPanel: function SpottedScript_Pages_Chat_View$get_panelThreadDescGroupBrandPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelThreadDescGroupBrandPanel');
    },
    get_panelThreadDescGroupBrandAnchor: function SpottedScript_Pages_Chat_View$get_panelThreadDescGroupBrandAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelThreadDescGroupBrandAnchor');
    },
    get_panelThreadDescGroupBrandCommentsLabel: function SpottedScript_Pages_Chat_View$get_panelThreadDescGroupBrandCommentsLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelThreadDescGroupBrandCommentsLabel');
    },
    get_panelThreadDescBrandPanel: function SpottedScript_Pages_Chat_View$get_panelThreadDescBrandPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelThreadDescBrandPanel');
    },
    get_panelThreadDescBrandGroupChatAnchor: function SpottedScript_Pages_Chat_View$get_panelThreadDescBrandGroupChatAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelThreadDescBrandGroupChatAnchor');
    },
    get_panelThreadDescBrandGroupChatCommentsLabel: function SpottedScript_Pages_Chat_View$get_panelThreadDescBrandGroupChatCommentsLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelThreadDescBrandGroupChatCommentsLabel');
    },
    get_commentAlertPanel: function SpottedScript_Pages_Chat_View$get_commentAlertPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CommentAlertPanel');
    },
    get_favouriteGroupPanel: function SpottedScript_Pages_Chat_View$get_favouriteGroupPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FavouriteGroupPanel');
    },
    get_panelThreadDescRelatedPanel: function SpottedScript_Pages_Chat_View$get_panelThreadDescRelatedPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelThreadDescRelatedPanel');
    },
    get_noThreadsPanel: function SpottedScript_Pages_Chat_View$get_noThreadsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_NoThreadsPanel');
    },
    get_threadsPanel: function SpottedScript_Pages_Chat_View$get_threadsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsPanel');
    },
    get_threadsPageP: function SpottedScript_Pages_Chat_View$get_threadsPageP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsPageP');
    },
    get_threadsPrevPageLink: function SpottedScript_Pages_Chat_View$get_threadsPrevPageLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsPrevPageLink');
    },
    get_threadsNextPageLink: function SpottedScript_Pages_Chat_View$get_threadsNextPageLink() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsNextPageLink');
    },
    get_inlineScript3: function SpottedScript_Pages_Chat_View$get_inlineScript3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InlineScript3');
    },
    get_threadsDataGrid: function SpottedScript_Pages_Chat_View$get_threadsDataGrid() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsDataGrid');
    },
    get_threadsPageP1: function SpottedScript_Pages_Chat_View$get_threadsPageP1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsPageP1');
    },
    get_threadsPrevPageLink1: function SpottedScript_Pages_Chat_View$get_threadsPrevPageLink1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsPrevPageLink1');
    },
    get_threadsNextPageLink1: function SpottedScript_Pages_Chat_View$get_threadsNextPageLink1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadsNextPageLink1');
    },
    get_h13: function SpottedScript_Pages_Chat_View$get_h13() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H13');
    },
    get_addThread: function SpottedScript_Pages_Chat_View$get_addThread() {
        /// <value type="SpottedScript.Controls.AddThread.Controller"></value>
        return eval(this.clientId + '_AddThreadController');
    },
    get_panelForumPrivate: function SpottedScript_Pages_Chat_View$get_panelForumPrivate() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelForumPrivate');
    },
    get_h114: function SpottedScript_Pages_Chat_View$get_h114() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H114');
    },
    get_panelThread: function SpottedScript_Pages_Chat_View$get_panelThread() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelThread');
    },
    get_panel1: function SpottedScript_Pages_Chat_View$get_panel1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Panel1');
    },
    get_h112: function SpottedScript_Pages_Chat_View$get_h112() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H112');
    },
    get_panelCommentsDescriptionP: function SpottedScript_Pages_Chat_View$get_panelCommentsDescriptionP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelCommentsDescriptionP');
    },
    get_threadDetailAdvancedOptionsP: function SpottedScript_Pages_Chat_View$get_threadDetailAdvancedOptionsP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadDetailAdvancedOptionsP');
    },
    get_threadDetailAdvancedOptionsAnchor: function SpottedScript_Pages_Chat_View$get_threadDetailAdvancedOptionsAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadDetailAdvancedOptionsAnchor');
    },
    get_threadDetailInboxPanel: function SpottedScript_Pages_Chat_View$get_threadDetailInboxPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadDetailInboxPanel');
    },
    get_threadDetailsSpamPanel: function SpottedScript_Pages_Chat_View$get_threadDetailsSpamPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadDetailsSpamPanel');
    },
    get_spamSourceLabel: function SpottedScript_Pages_Chat_View$get_spamSourceLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SpamSourceLabel');
    },
    get_threadSpamOptionsPanel: function SpottedScript_Pages_Chat_View$get_threadSpamOptionsPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadSpamOptionsPanel');
    },
    get_threadSpamOption1RadioButton: function SpottedScript_Pages_Chat_View$get_threadSpamOption1RadioButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadSpamOption1RadioButton');
    },
    get_threadSpamOption2RadioButton: function SpottedScript_Pages_Chat_View$get_threadSpamOption2RadioButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadSpamOption2RadioButton');
    },
    get_threadSpamOption3RadioButton: function SpottedScript_Pages_Chat_View$get_threadSpamOption3RadioButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadSpamOption3RadioButton');
    },
    get_threadSpamUpdateButton: function SpottedScript_Pages_Chat_View$get_threadSpamUpdateButton() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadSpamUpdateButton');
    },
    get_panelParticipants: function SpottedScript_Pages_Chat_View$get_panelParticipants() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelParticipants');
    },
    get_h111: function SpottedScript_Pages_Chat_View$get_h111() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H111');
    },
    get_participantsHiddenPanel: function SpottedScript_Pages_Chat_View$get_participantsHiddenPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ParticipantsHiddenPanel');
    },
    get_participantsLabel: function SpottedScript_Pages_Chat_View$get_participantsLabel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ParticipantsLabel');
    },
    get_linkbutton1: function SpottedScript_Pages_Chat_View$get_linkbutton1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Linkbutton1');
    },
    get_participantsListPanel: function SpottedScript_Pages_Chat_View$get_participantsListPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ParticipantsListPanel');
    },
    get_participantsDataList: function SpottedScript_Pages_Chat_View$get_participantsDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ParticipantsDataList');
    },
    get_participantsRefreshAnchor: function SpottedScript_Pages_Chat_View$get_participantsRefreshAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ParticipantsRefreshAnchor');
    },
    get_panelThreadSubject: function SpottedScript_Pages_Chat_View$get_panelThreadSubject() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelThreadSubject');
    },
    get_h18: function SpottedScript_Pages_Chat_View$get_h18() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H18');
    },
    get_panelThreadSubjectHeadP: function SpottedScript_Pages_Chat_View$get_panelThreadSubjectHeadP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelThreadSubjectHeadP');
    },
    get_panelThreadSubjectSubHeadP: function SpottedScript_Pages_Chat_View$get_panelThreadSubjectSubHeadP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelThreadSubjectSubHeadP');
    },
    get_panelThreadSubjectPhotoP: function SpottedScript_Pages_Chat_View$get_panelThreadSubjectPhotoP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelThreadSubjectPhotoP');
    },
    get_threadSubjectPhotoAnchor: function SpottedScript_Pages_Chat_View$get_threadSubjectPhotoAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadSubjectPhotoAnchor');
    },
    get_threadSubjectPhotoImg: function SpottedScript_Pages_Chat_View$get_threadSubjectPhotoImg() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadSubjectPhotoImg');
    },
    get_panelThreadSubjectPhotoMePanel: function SpottedScript_Pages_Chat_View$get_panelThreadSubjectPhotoMePanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelThreadSubjectPhotoMePanel');
    },
    get_panelThreadSubjectArticleP: function SpottedScript_Pages_Chat_View$get_panelThreadSubjectArticleP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelThreadSubjectArticleP');
    },
    get_panelThreadSubjectArticleMoreP: function SpottedScript_Pages_Chat_View$get_panelThreadSubjectArticleMoreP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelThreadSubjectArticleMoreP');
    },
    get_initialCommentPanel: function SpottedScript_Pages_Chat_View$get_initialCommentPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InitialCommentPanel');
    },
    get_initialCommentH1: function SpottedScript_Pages_Chat_View$get_initialCommentH1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InitialCommentH1');
    },
    get_subjectPanel1: function SpottedScript_Pages_Chat_View$get_subjectPanel1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SubjectPanel1');
    },
    get_threadSubject1: function SpottedScript_Pages_Chat_View$get_threadSubject1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadSubject1');
    },
    get_initialCommentDataList: function SpottedScript_Pages_Chat_View$get_initialCommentDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InitialCommentDataList');
    },
    get_commentsSubjectH1: function SpottedScript_Pages_Chat_View$get_commentsSubjectH1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CommentsSubjectH1');
    },
    get_p1: function SpottedScript_Pages_Chat_View$get_p1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_P1');
    },
    get_threadDetailBackLink1: function SpottedScript_Pages_Chat_View$get_threadDetailBackLink1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadDetailBackLink1');
    },
    get_threadDetailBackLinkLabel1: function SpottedScript_Pages_Chat_View$get_threadDetailBackLinkLabel1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadDetailBackLinkLabel1');
    },
    get_commentsPageTopHolder: function SpottedScript_Pages_Chat_View$get_commentsPageTopHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CommentsPageTopHolder');
    },
    get_commentsPrevPageLink1: function SpottedScript_Pages_Chat_View$get_commentsPrevPageLink1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CommentsPrevPageLink1');
    },
    get_commentsNextPageLink1: function SpottedScript_Pages_Chat_View$get_commentsNextPageLink1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CommentsNextPageLink1');
    },
    get_commentsPageP1: function SpottedScript_Pages_Chat_View$get_commentsPageP1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CommentsPageP1');
    },
    get_subjectPanel2: function SpottedScript_Pages_Chat_View$get_subjectPanel2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_SubjectPanel2');
    },
    get_threadSubject2: function SpottedScript_Pages_Chat_View$get_threadSubject2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadSubject2');
    },
    get_commentsDataList: function SpottedScript_Pages_Chat_View$get_commentsDataList() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CommentsDataList');
    },
    get_commentsPageP2: function SpottedScript_Pages_Chat_View$get_commentsPageP2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CommentsPageP2');
    },
    get_p2: function SpottedScript_Pages_Chat_View$get_p2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_P2');
    },
    get_threadDetailBackLink2: function SpottedScript_Pages_Chat_View$get_threadDetailBackLink2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadDetailBackLink2');
    },
    get_threadDetailBackLinkLabel2: function SpottedScript_Pages_Chat_View$get_threadDetailBackLinkLabel2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadDetailBackLinkLabel2');
    },
    get_threadDetailInboxBottomSpan: function SpottedScript_Pages_Chat_View$get_threadDetailInboxBottomSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_ThreadDetailInboxBottomSpan');
    },
    get_inboxBottomButtonHolder: function SpottedScript_Pages_Chat_View$get_inboxBottomButtonHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InboxBottomButtonHolder');
    },
    get_watchBottomButtonHolder: function SpottedScript_Pages_Chat_View$get_watchBottomButtonHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_WatchBottomButtonHolder');
    },
    get_inlineScript1: function SpottedScript_Pages_Chat_View$get_inlineScript1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InlineScript1');
    },
    get_favouriteBottomButtonHolder: function SpottedScript_Pages_Chat_View$get_favouriteBottomButtonHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_FavouriteBottomButtonHolder');
    },
    get_inlineScript2: function SpottedScript_Pages_Chat_View$get_inlineScript2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_InlineScript2');
    },
    get_commentsPageBottomHolder: function SpottedScript_Pages_Chat_View$get_commentsPageBottomHolder() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CommentsPageBottomHolder');
    },
    get_commentsPrevPageLink2: function SpottedScript_Pages_Chat_View$get_commentsPrevPageLink2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CommentsPrevPageLink2');
    },
    get_commentsNextPageLink2: function SpottedScript_Pages_Chat_View$get_commentsNextPageLink2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CommentsNextPageLink2');
    },
    get_captionEntryPanel: function SpottedScript_Pages_Chat_View$get_captionEntryPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CaptionEntryPanel');
    },
    get_h19: function SpottedScript_Pages_Chat_View$get_h19() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H19');
    },
    get_captionTextBox: function SpottedScript_Pages_Chat_View$get_captionTextBox() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CaptionTextBox');
    },
    get_captionButtonP: function SpottedScript_Pages_Chat_View$get_captionButtonP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CaptionButtonP');
    },
    get_button2: function SpottedScript_Pages_Chat_View$get_button2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button2');
    },
    get_captionLoginPanel: function SpottedScript_Pages_Chat_View$get_captionLoginPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CaptionLoginPanel');
    },
    get_captionErrorP: function SpottedScript_Pages_Chat_View$get_captionErrorP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_CaptionErrorP');
    },
    get_panelAddCommentClosed: function SpottedScript_Pages_Chat_View$get_panelAddCommentClosed() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelAddCommentClosed');
    },
    get_h19dfs: function SpottedScript_Pages_Chat_View$get_h19dfs() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H19dfs');
    },
    get_panelAddComment: function SpottedScript_Pages_Chat_View$get_panelAddComment() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelAddComment');
    },
    get_addCommentGroupMemberPanel: function SpottedScript_Pages_Chat_View$get_addCommentGroupMemberPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddCommentGroupMemberPanel');
    },
    get_addCommentGroupMemberAnchor: function SpottedScript_Pages_Chat_View$get_addCommentGroupMemberAnchor() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddCommentGroupMemberAnchor');
    },
    get_addCommentLoginPanel: function SpottedScript_Pages_Chat_View$get_addCommentLoginPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddCommentLoginPanel');
    },
    get_addCommentEmailVerifyPanel: function SpottedScript_Pages_Chat_View$get_addCommentEmailVerifyPanel() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_AddCommentEmailVerifyPanel');
    },
    get_requiredfieldvalidator3: function SpottedScript_Pages_Chat_View$get_requiredfieldvalidator3() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Requiredfieldvalidator3');
    },
    get_addCommentHtml: function SpottedScript_Pages_Chat_View$get_addCommentHtml() {
        /// <value type="SpottedScript.Controls.Html.Controller"></value>
        return eval(this.clientId + '_AddCommentHtmlController');
    },
    get_panelInviteBuddiesSealed: function SpottedScript_Pages_Chat_View$get_panelInviteBuddiesSealed() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelInviteBuddiesSealed');
    },
    get_h110: function SpottedScript_Pages_Chat_View$get_h110() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H110');
    },
    get_panelInviteBuddies: function SpottedScript_Pages_Chat_View$get_panelInviteBuddies() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelInviteBuddies');
    },
    get_h12: function SpottedScript_Pages_Chat_View$get_h12() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H12');
    },
    get_uiMultiBuddyChooser: function SpottedScript_Pages_Chat_View$get_uiMultiBuddyChooser() {
        /// <value type="SpottedScript.Controls.MultiBuddyChooser.Controller"></value>
        return eval(this.clientId + '_uiMultiBuddyChooserController');
    },
    get_button1: function SpottedScript_Pages_Chat_View$get_button1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_Button1');
    },
    get_panelInviteErrorP: function SpottedScript_Pages_Chat_View$get_panelInviteErrorP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelInviteErrorP');
    },
    get_panelInviteDoneP: function SpottedScript_Pages_Chat_View$get_panelInviteDoneP() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelInviteDoneP');
    },
    get_panelThreadDiasbled: function SpottedScript_Pages_Chat_View$get_panelThreadDiasbled() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelThreadDiasbled');
    },
    get_h16: function SpottedScript_Pages_Chat_View$get_h16() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H16');
    },
    get_panelPrivateThread: function SpottedScript_Pages_Chat_View$get_panelPrivateThread() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelPrivateThread');
    },
    get_h17: function SpottedScript_Pages_Chat_View$get_h17() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H17');
    },
    get_panelGroupPrivateWaiting: function SpottedScript_Pages_Chat_View$get_panelGroupPrivateWaiting() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelGroupPrivateWaiting');
    },
    get_h118: function SpottedScript_Pages_Chat_View$get_h118() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H118');
    },
    get_panelGroupPrivateWaitingAnchor1: function SpottedScript_Pages_Chat_View$get_panelGroupPrivateWaitingAnchor1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelGroupPrivateWaitingAnchor1');
    },
    get_panelGroupPrivateWaitingAnchor2: function SpottedScript_Pages_Chat_View$get_panelGroupPrivateWaitingAnchor2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelGroupPrivateWaitingAnchor2');
    },
    get_panelGroupPrivateCanJoin: function SpottedScript_Pages_Chat_View$get_panelGroupPrivateCanJoin() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelGroupPrivateCanJoin');
    },
    get_h115: function SpottedScript_Pages_Chat_View$get_h115() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H115');
    },
    get_panelGroupPrivateCanJoinGroupAnchor1: function SpottedScript_Pages_Chat_View$get_panelGroupPrivateCanJoinGroupAnchor1() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelGroupPrivateCanJoinGroupAnchor1');
    },
    get_panelGroupPrivateCanJoinGroupAnchor2: function SpottedScript_Pages_Chat_View$get_panelGroupPrivateCanJoinGroupAnchor2() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelGroupPrivateCanJoinGroupAnchor2');
    },
    get_panelGroupPrivateRecommend: function SpottedScript_Pages_Chat_View$get_panelGroupPrivateRecommend() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelGroupPrivateRecommend');
    },
    get_h116: function SpottedScript_Pages_Chat_View$get_h116() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H116');
    },
    get_panelGroupPrivateRecommendGroupSpan: function SpottedScript_Pages_Chat_View$get_panelGroupPrivateRecommendGroupSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelGroupPrivateRecommendGroupSpan');
    },
    get_panelGroupPrivateRecommendInvitingUsrSpan: function SpottedScript_Pages_Chat_View$get_panelGroupPrivateRecommendInvitingUsrSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelGroupPrivateRecommendInvitingUsrSpan');
    },
    get_panelGroupPrivateRecommendRejected: function SpottedScript_Pages_Chat_View$get_panelGroupPrivateRecommendRejected() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelGroupPrivateRecommendRejected');
    },
    get_h117: function SpottedScript_Pages_Chat_View$get_h117() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_H117');
    },
    get_panelGroupPrivateRecommendRejectedGroupSpan: function SpottedScript_Pages_Chat_View$get_panelGroupPrivateRecommendRejectedGroupSpan() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelGroupPrivateRecommendRejectedGroupSpan');
    },
    get_panelThreadSubjectArticleExtended: function SpottedScript_Pages_Chat_View$get_panelThreadSubjectArticleExtended() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_PanelThreadSubjectArticleExtended');
    },
    get_genericContainerPage: function SpottedScript_Pages_Chat_View$get_genericContainerPage() {
        /// <value type="Object" domElement="true"></value>
        return document.getElementById(this.clientId + '_GenericContainerPage');
    }
}
SpottedScript.Pages.Chat.View.registerClass('SpottedScript.Pages.Chat.View', SpottedScript.DsiUserControl.View);
