//! ChatClient.debug.js
//

(function($) {

Type.registerNamespace('Js.Controls.ChatClient');

////////////////////////////////////////////////////////////////////////////////
// Js.Controls.ChatClient.View

Js.Controls.ChatClient.View = function Js_Controls_ChatClient_View(clientId) {
    /// <param name="clientId" type="String">
    /// </param>
    /// <field name="clientId" type="String">
    /// </field>
    /// <field name="_OuterMain" type="Object" domElement="true">
    /// </field>
    /// <field name="_OuterMainJ" type="jQueryObject">
    /// </field>
    /// <field name="_TabsChatLink" type="Object" domElement="true">
    /// </field>
    /// <field name="_TabsChatLinkJ" type="jQueryObject">
    /// </field>
    /// <field name="_TabChatHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_TabChatHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_RoomsMain" type="Object" domElement="true">
    /// </field>
    /// <field name="_RoomsMainJ" type="jQueryObject">
    /// </field>
    /// <field name="_RoomList" type="Object" domElement="true">
    /// </field>
    /// <field name="_RoomListJ" type="jQueryObject">
    /// </field>
    /// <field name="_PrivateChatDrop" type="Object" domElement="true">
    /// </field>
    /// <field name="_PrivateChatDropJ" type="jQueryObject">
    /// </field>
    /// <field name="_RoomPrivateListDivider" type="Object" domElement="true">
    /// </field>
    /// <field name="_RoomPrivateListDividerJ" type="jQueryObject">
    /// </field>
    /// <field name="_RoomPrivateList" type="Object" domElement="true">
    /// </field>
    /// <field name="_RoomPrivateListJ" type="jQueryObject">
    /// </field>
    /// <field name="_RoomGuestListDivider" type="Object" domElement="true">
    /// </field>
    /// <field name="_RoomGuestListDividerJ" type="jQueryObject">
    /// </field>
    /// <field name="_RoomGuestList" type="Object" domElement="true">
    /// </field>
    /// <field name="_RoomGuestListJ" type="jQueryObject">
    /// </field>
    /// <field name="_PrivateChatDropMain" type="Object" domElement="true">
    /// </field>
    /// <field name="_PrivateChatDropMainJ" type="jQueryObject">
    /// </field>
    /// <field name="_MessagesMain" type="Object" domElement="true">
    /// </field>
    /// <field name="_MessagesMainJ" type="jQueryObject">
    /// </field>
    /// <field name="_TextBoxHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_TextBoxHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_TextBox" type="Object" domElement="true">
    /// </field>
    /// <field name="_TextBoxJ" type="jQueryObject">
    /// </field>
    /// <field name="_MessageList" type="Object" domElement="true">
    /// </field>
    /// <field name="_MessageListJ" type="jQueryObject">
    /// </field>
    /// <field name="_WrongSessionHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_WrongSessionHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_WrongSessionResumeLink" type="Object" domElement="true">
    /// </field>
    /// <field name="_WrongSessionResumeLinkJ" type="jQueryObject">
    /// </field>
    /// <field name="_TimeoutHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_TimeoutHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_TimeoutResumeLink" type="Object" domElement="true">
    /// </field>
    /// <field name="_TimeoutResumeLinkJ" type="jQueryObject">
    /// </field>
    /// <field name="_DeleteArchiveHolder" type="Object" domElement="true">
    /// </field>
    /// <field name="_DeleteArchiveHolderJ" type="jQueryObject">
    /// </field>
    /// <field name="_DeleteArchiveAnchor" type="Object" domElement="true">
    /// </field>
    /// <field name="_DeleteArchiveAnchorJ" type="jQueryObject">
    /// </field>
    /// <field name="_DeleteArchiveDoneLabel" type="Object" domElement="true">
    /// </field>
    /// <field name="_DeleteArchiveDoneLabelJ" type="jQueryObject">
    /// </field>
    /// <field name="_StreamList" type="Object" domElement="true">
    /// </field>
    /// <field name="_StreamListJ" type="jQueryObject">
    /// </field>
    /// <field name="_PopupsCheckBox" type="Object" domElement="true">
    /// </field>
    /// <field name="_PopupsCheckBoxJ" type="jQueryObject">
    /// </field>
    /// <field name="_StickyCheckBox" type="Object" domElement="true">
    /// </field>
    /// <field name="_StickyCheckBoxJ" type="jQueryObject">
    /// </field>
    /// <field name="_DownlevelMain" type="Object" domElement="true">
    /// </field>
    /// <field name="_DownlevelMainJ" type="jQueryObject">
    /// </field>
    /// <field name="_InitGo" type="Object" domElement="true">
    /// </field>
    /// <field name="_InitGoJ" type="jQueryObject">
    /// </field>
    /// <field name="_InitUsrK" type="Object" domElement="true">
    /// </field>
    /// <field name="_InitUsrKJ" type="jQueryObject">
    /// </field>
    /// <field name="_InitClientID" type="Object" domElement="true">
    /// </field>
    /// <field name="_InitClientIDJ" type="jQueryObject">
    /// </field>
    /// <field name="_InitLastActionTicks" type="Object" domElement="true">
    /// </field>
    /// <field name="_InitLastActionTicksJ" type="jQueryObject">
    /// </field>
    /// <field name="_InitSystemMessagesRoomGuid" type="Object" domElement="true">
    /// </field>
    /// <field name="_InitSystemMessagesRoomGuidJ" type="jQueryObject">
    /// </field>
    /// <field name="_InitInboxUpdatesRoomGuid" type="Object" domElement="true">
    /// </field>
    /// <field name="_InitInboxUpdatesRoomGuidJ" type="jQueryObject">
    /// </field>
    /// <field name="_InitBuddyAlertsRoomGuid" type="Object" domElement="true">
    /// </field>
    /// <field name="_InitBuddyAlertsRoomGuidJ" type="jQueryObject">
    /// </field>
    /// <field name="_InitBuddyStreamRoomGuid" type="Object" domElement="true">
    /// </field>
    /// <field name="_InitBuddyStreamRoomGuidJ" type="jQueryObject">
    /// </field>
    /// <field name="_InitAnimatePopups" type="Object" domElement="true">
    /// </field>
    /// <field name="_InitAnimatePopupsJ" type="jQueryObject">
    /// </field>
    /// <field name="_InitTopPhoto" type="Object" domElement="true">
    /// </field>
    /// <field name="_InitTopPhotoJ" type="jQueryObject">
    /// </field>
    this.clientId = clientId;
}
Js.Controls.ChatClient.View.prototype = {
    clientId: null,
    
    get_outerMain: function Js_Controls_ChatClient_View$get_outerMain() {
        /// <value type="Object" domElement="true"></value>
        if (this._OuterMain == null) {
            this._OuterMain = document.getElementById(this.clientId + '_OuterMain');
        }
        return this._OuterMain;
    },
    
    _OuterMain: null,
    
    get_outerMainJ: function Js_Controls_ChatClient_View$get_outerMainJ() {
        /// <value type="jQueryObject"></value>
        if (this._OuterMainJ == null) {
            this._OuterMainJ = $('#' + this.clientId + '_OuterMain');
        }
        return this._OuterMainJ;
    },
    
    _OuterMainJ: null,
    
    get_tabsChatLink: function Js_Controls_ChatClient_View$get_tabsChatLink() {
        /// <value type="Object" domElement="true"></value>
        if (this._TabsChatLink == null) {
            this._TabsChatLink = document.getElementById(this.clientId + '_TabsChatLink');
        }
        return this._TabsChatLink;
    },
    
    _TabsChatLink: null,
    
    get_tabsChatLinkJ: function Js_Controls_ChatClient_View$get_tabsChatLinkJ() {
        /// <value type="jQueryObject"></value>
        if (this._TabsChatLinkJ == null) {
            this._TabsChatLinkJ = $('#' + this.clientId + '_TabsChatLink');
        }
        return this._TabsChatLinkJ;
    },
    
    _TabsChatLinkJ: null,
    
    get_tabChatHolder: function Js_Controls_ChatClient_View$get_tabChatHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._TabChatHolder == null) {
            this._TabChatHolder = document.getElementById(this.clientId + '_TabChatHolder');
        }
        return this._TabChatHolder;
    },
    
    _TabChatHolder: null,
    
    get_tabChatHolderJ: function Js_Controls_ChatClient_View$get_tabChatHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._TabChatHolderJ == null) {
            this._TabChatHolderJ = $('#' + this.clientId + '_TabChatHolder');
        }
        return this._TabChatHolderJ;
    },
    
    _TabChatHolderJ: null,
    
    get_roomsMain: function Js_Controls_ChatClient_View$get_roomsMain() {
        /// <value type="Object" domElement="true"></value>
        if (this._RoomsMain == null) {
            this._RoomsMain = document.getElementById(this.clientId + '_RoomsMain');
        }
        return this._RoomsMain;
    },
    
    _RoomsMain: null,
    
    get_roomsMainJ: function Js_Controls_ChatClient_View$get_roomsMainJ() {
        /// <value type="jQueryObject"></value>
        if (this._RoomsMainJ == null) {
            this._RoomsMainJ = $('#' + this.clientId + '_RoomsMain');
        }
        return this._RoomsMainJ;
    },
    
    _RoomsMainJ: null,
    
    get_roomList: function Js_Controls_ChatClient_View$get_roomList() {
        /// <value type="Object" domElement="true"></value>
        if (this._RoomList == null) {
            this._RoomList = document.getElementById(this.clientId + '_RoomList');
        }
        return this._RoomList;
    },
    
    _RoomList: null,
    
    get_roomListJ: function Js_Controls_ChatClient_View$get_roomListJ() {
        /// <value type="jQueryObject"></value>
        if (this._RoomListJ == null) {
            this._RoomListJ = $('#' + this.clientId + '_RoomList');
        }
        return this._RoomListJ;
    },
    
    _RoomListJ: null,
    
    get_privateChatDrop: function Js_Controls_ChatClient_View$get_privateChatDrop() {
        /// <value type="Object" domElement="true"></value>
        if (this._PrivateChatDrop == null) {
            this._PrivateChatDrop = document.getElementById(this.clientId + '_PrivateChatDrop');
        }
        return this._PrivateChatDrop;
    },
    
    _PrivateChatDrop: null,
    
    get_privateChatDropJ: function Js_Controls_ChatClient_View$get_privateChatDropJ() {
        /// <value type="jQueryObject"></value>
        if (this._PrivateChatDropJ == null) {
            this._PrivateChatDropJ = $('#' + this.clientId + '_PrivateChatDrop');
        }
        return this._PrivateChatDropJ;
    },
    
    _PrivateChatDropJ: null,
    
    get_roomPrivateListDivider: function Js_Controls_ChatClient_View$get_roomPrivateListDivider() {
        /// <value type="Object" domElement="true"></value>
        if (this._RoomPrivateListDivider == null) {
            this._RoomPrivateListDivider = document.getElementById(this.clientId + '_RoomPrivateListDivider');
        }
        return this._RoomPrivateListDivider;
    },
    
    _RoomPrivateListDivider: null,
    
    get_roomPrivateListDividerJ: function Js_Controls_ChatClient_View$get_roomPrivateListDividerJ() {
        /// <value type="jQueryObject"></value>
        if (this._RoomPrivateListDividerJ == null) {
            this._RoomPrivateListDividerJ = $('#' + this.clientId + '_RoomPrivateListDivider');
        }
        return this._RoomPrivateListDividerJ;
    },
    
    _RoomPrivateListDividerJ: null,
    
    get_roomPrivateList: function Js_Controls_ChatClient_View$get_roomPrivateList() {
        /// <value type="Object" domElement="true"></value>
        if (this._RoomPrivateList == null) {
            this._RoomPrivateList = document.getElementById(this.clientId + '_RoomPrivateList');
        }
        return this._RoomPrivateList;
    },
    
    _RoomPrivateList: null,
    
    get_roomPrivateListJ: function Js_Controls_ChatClient_View$get_roomPrivateListJ() {
        /// <value type="jQueryObject"></value>
        if (this._RoomPrivateListJ == null) {
            this._RoomPrivateListJ = $('#' + this.clientId + '_RoomPrivateList');
        }
        return this._RoomPrivateListJ;
    },
    
    _RoomPrivateListJ: null,
    
    get_roomGuestListDivider: function Js_Controls_ChatClient_View$get_roomGuestListDivider() {
        /// <value type="Object" domElement="true"></value>
        if (this._RoomGuestListDivider == null) {
            this._RoomGuestListDivider = document.getElementById(this.clientId + '_RoomGuestListDivider');
        }
        return this._RoomGuestListDivider;
    },
    
    _RoomGuestListDivider: null,
    
    get_roomGuestListDividerJ: function Js_Controls_ChatClient_View$get_roomGuestListDividerJ() {
        /// <value type="jQueryObject"></value>
        if (this._RoomGuestListDividerJ == null) {
            this._RoomGuestListDividerJ = $('#' + this.clientId + '_RoomGuestListDivider');
        }
        return this._RoomGuestListDividerJ;
    },
    
    _RoomGuestListDividerJ: null,
    
    get_roomGuestList: function Js_Controls_ChatClient_View$get_roomGuestList() {
        /// <value type="Object" domElement="true"></value>
        if (this._RoomGuestList == null) {
            this._RoomGuestList = document.getElementById(this.clientId + '_RoomGuestList');
        }
        return this._RoomGuestList;
    },
    
    _RoomGuestList: null,
    
    get_roomGuestListJ: function Js_Controls_ChatClient_View$get_roomGuestListJ() {
        /// <value type="jQueryObject"></value>
        if (this._RoomGuestListJ == null) {
            this._RoomGuestListJ = $('#' + this.clientId + '_RoomGuestList');
        }
        return this._RoomGuestListJ;
    },
    
    _RoomGuestListJ: null,
    
    get_privateChatDropMain: function Js_Controls_ChatClient_View$get_privateChatDropMain() {
        /// <value type="Object" domElement="true"></value>
        if (this._PrivateChatDropMain == null) {
            this._PrivateChatDropMain = document.getElementById(this.clientId + '_PrivateChatDropMain');
        }
        return this._PrivateChatDropMain;
    },
    
    _PrivateChatDropMain: null,
    
    get_privateChatDropMainJ: function Js_Controls_ChatClient_View$get_privateChatDropMainJ() {
        /// <value type="jQueryObject"></value>
        if (this._PrivateChatDropMainJ == null) {
            this._PrivateChatDropMainJ = $('#' + this.clientId + '_PrivateChatDropMain');
        }
        return this._PrivateChatDropMainJ;
    },
    
    _PrivateChatDropMainJ: null,
    
    get_messagesMain: function Js_Controls_ChatClient_View$get_messagesMain() {
        /// <value type="Object" domElement="true"></value>
        if (this._MessagesMain == null) {
            this._MessagesMain = document.getElementById(this.clientId + '_MessagesMain');
        }
        return this._MessagesMain;
    },
    
    _MessagesMain: null,
    
    get_messagesMainJ: function Js_Controls_ChatClient_View$get_messagesMainJ() {
        /// <value type="jQueryObject"></value>
        if (this._MessagesMainJ == null) {
            this._MessagesMainJ = $('#' + this.clientId + '_MessagesMain');
        }
        return this._MessagesMainJ;
    },
    
    _MessagesMainJ: null,
    
    get_textBoxHolder: function Js_Controls_ChatClient_View$get_textBoxHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._TextBoxHolder == null) {
            this._TextBoxHolder = document.getElementById(this.clientId + '_TextBoxHolder');
        }
        return this._TextBoxHolder;
    },
    
    _TextBoxHolder: null,
    
    get_textBoxHolderJ: function Js_Controls_ChatClient_View$get_textBoxHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._TextBoxHolderJ == null) {
            this._TextBoxHolderJ = $('#' + this.clientId + '_TextBoxHolder');
        }
        return this._TextBoxHolderJ;
    },
    
    _TextBoxHolderJ: null,
    
    get_textBox: function Js_Controls_ChatClient_View$get_textBox() {
        /// <value type="Object" domElement="true"></value>
        if (this._TextBox == null) {
            this._TextBox = document.getElementById(this.clientId + '_TextBox');
        }
        return this._TextBox;
    },
    
    _TextBox: null,
    
    get_textBoxJ: function Js_Controls_ChatClient_View$get_textBoxJ() {
        /// <value type="jQueryObject"></value>
        if (this._TextBoxJ == null) {
            this._TextBoxJ = $('#' + this.clientId + '_TextBox');
        }
        return this._TextBoxJ;
    },
    
    _TextBoxJ: null,
    
    get_messageList: function Js_Controls_ChatClient_View$get_messageList() {
        /// <value type="Object" domElement="true"></value>
        if (this._MessageList == null) {
            this._MessageList = document.getElementById(this.clientId + '_MessageList');
        }
        return this._MessageList;
    },
    
    _MessageList: null,
    
    get_messageListJ: function Js_Controls_ChatClient_View$get_messageListJ() {
        /// <value type="jQueryObject"></value>
        if (this._MessageListJ == null) {
            this._MessageListJ = $('#' + this.clientId + '_MessageList');
        }
        return this._MessageListJ;
    },
    
    _MessageListJ: null,
    
    get_wrongSessionHolder: function Js_Controls_ChatClient_View$get_wrongSessionHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._WrongSessionHolder == null) {
            this._WrongSessionHolder = document.getElementById(this.clientId + '_WrongSessionHolder');
        }
        return this._WrongSessionHolder;
    },
    
    _WrongSessionHolder: null,
    
    get_wrongSessionHolderJ: function Js_Controls_ChatClient_View$get_wrongSessionHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._WrongSessionHolderJ == null) {
            this._WrongSessionHolderJ = $('#' + this.clientId + '_WrongSessionHolder');
        }
        return this._WrongSessionHolderJ;
    },
    
    _WrongSessionHolderJ: null,
    
    get_wrongSessionResumeLink: function Js_Controls_ChatClient_View$get_wrongSessionResumeLink() {
        /// <value type="Object" domElement="true"></value>
        if (this._WrongSessionResumeLink == null) {
            this._WrongSessionResumeLink = document.getElementById(this.clientId + '_WrongSessionResumeLink');
        }
        return this._WrongSessionResumeLink;
    },
    
    _WrongSessionResumeLink: null,
    
    get_wrongSessionResumeLinkJ: function Js_Controls_ChatClient_View$get_wrongSessionResumeLinkJ() {
        /// <value type="jQueryObject"></value>
        if (this._WrongSessionResumeLinkJ == null) {
            this._WrongSessionResumeLinkJ = $('#' + this.clientId + '_WrongSessionResumeLink');
        }
        return this._WrongSessionResumeLinkJ;
    },
    
    _WrongSessionResumeLinkJ: null,
    
    get_timeoutHolder: function Js_Controls_ChatClient_View$get_timeoutHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._TimeoutHolder == null) {
            this._TimeoutHolder = document.getElementById(this.clientId + '_TimeoutHolder');
        }
        return this._TimeoutHolder;
    },
    
    _TimeoutHolder: null,
    
    get_timeoutHolderJ: function Js_Controls_ChatClient_View$get_timeoutHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._TimeoutHolderJ == null) {
            this._TimeoutHolderJ = $('#' + this.clientId + '_TimeoutHolder');
        }
        return this._TimeoutHolderJ;
    },
    
    _TimeoutHolderJ: null,
    
    get_timeoutResumeLink: function Js_Controls_ChatClient_View$get_timeoutResumeLink() {
        /// <value type="Object" domElement="true"></value>
        if (this._TimeoutResumeLink == null) {
            this._TimeoutResumeLink = document.getElementById(this.clientId + '_TimeoutResumeLink');
        }
        return this._TimeoutResumeLink;
    },
    
    _TimeoutResumeLink: null,
    
    get_timeoutResumeLinkJ: function Js_Controls_ChatClient_View$get_timeoutResumeLinkJ() {
        /// <value type="jQueryObject"></value>
        if (this._TimeoutResumeLinkJ == null) {
            this._TimeoutResumeLinkJ = $('#' + this.clientId + '_TimeoutResumeLink');
        }
        return this._TimeoutResumeLinkJ;
    },
    
    _TimeoutResumeLinkJ: null,
    
    get_deleteArchiveHolder: function Js_Controls_ChatClient_View$get_deleteArchiveHolder() {
        /// <value type="Object" domElement="true"></value>
        if (this._DeleteArchiveHolder == null) {
            this._DeleteArchiveHolder = document.getElementById(this.clientId + '_DeleteArchiveHolder');
        }
        return this._DeleteArchiveHolder;
    },
    
    _DeleteArchiveHolder: null,
    
    get_deleteArchiveHolderJ: function Js_Controls_ChatClient_View$get_deleteArchiveHolderJ() {
        /// <value type="jQueryObject"></value>
        if (this._DeleteArchiveHolderJ == null) {
            this._DeleteArchiveHolderJ = $('#' + this.clientId + '_DeleteArchiveHolder');
        }
        return this._DeleteArchiveHolderJ;
    },
    
    _DeleteArchiveHolderJ: null,
    
    get_deleteArchiveAnchor: function Js_Controls_ChatClient_View$get_deleteArchiveAnchor() {
        /// <value type="Object" domElement="true"></value>
        if (this._DeleteArchiveAnchor == null) {
            this._DeleteArchiveAnchor = document.getElementById(this.clientId + '_DeleteArchiveAnchor');
        }
        return this._DeleteArchiveAnchor;
    },
    
    _DeleteArchiveAnchor: null,
    
    get_deleteArchiveAnchorJ: function Js_Controls_ChatClient_View$get_deleteArchiveAnchorJ() {
        /// <value type="jQueryObject"></value>
        if (this._DeleteArchiveAnchorJ == null) {
            this._DeleteArchiveAnchorJ = $('#' + this.clientId + '_DeleteArchiveAnchor');
        }
        return this._DeleteArchiveAnchorJ;
    },
    
    _DeleteArchiveAnchorJ: null,
    
    get_deleteArchiveDoneLabel: function Js_Controls_ChatClient_View$get_deleteArchiveDoneLabel() {
        /// <value type="Object" domElement="true"></value>
        if (this._DeleteArchiveDoneLabel == null) {
            this._DeleteArchiveDoneLabel = document.getElementById(this.clientId + '_DeleteArchiveDoneLabel');
        }
        return this._DeleteArchiveDoneLabel;
    },
    
    _DeleteArchiveDoneLabel: null,
    
    get_deleteArchiveDoneLabelJ: function Js_Controls_ChatClient_View$get_deleteArchiveDoneLabelJ() {
        /// <value type="jQueryObject"></value>
        if (this._DeleteArchiveDoneLabelJ == null) {
            this._DeleteArchiveDoneLabelJ = $('#' + this.clientId + '_DeleteArchiveDoneLabel');
        }
        return this._DeleteArchiveDoneLabelJ;
    },
    
    _DeleteArchiveDoneLabelJ: null,
    
    get_streamList: function Js_Controls_ChatClient_View$get_streamList() {
        /// <value type="Object" domElement="true"></value>
        if (this._StreamList == null) {
            this._StreamList = document.getElementById(this.clientId + '_StreamList');
        }
        return this._StreamList;
    },
    
    _StreamList: null,
    
    get_streamListJ: function Js_Controls_ChatClient_View$get_streamListJ() {
        /// <value type="jQueryObject"></value>
        if (this._StreamListJ == null) {
            this._StreamListJ = $('#' + this.clientId + '_StreamList');
        }
        return this._StreamListJ;
    },
    
    _StreamListJ: null,
    
    get_popupsCheckBox: function Js_Controls_ChatClient_View$get_popupsCheckBox() {
        /// <value type="Object" domElement="true"></value>
        if (this._PopupsCheckBox == null) {
            this._PopupsCheckBox = document.getElementById(this.clientId + '_PopupsCheckBox');
        }
        return this._PopupsCheckBox;
    },
    
    _PopupsCheckBox: null,
    
    get_popupsCheckBoxJ: function Js_Controls_ChatClient_View$get_popupsCheckBoxJ() {
        /// <value type="jQueryObject"></value>
        if (this._PopupsCheckBoxJ == null) {
            this._PopupsCheckBoxJ = $('#' + this.clientId + '_PopupsCheckBox');
        }
        return this._PopupsCheckBoxJ;
    },
    
    _PopupsCheckBoxJ: null,
    
    get_stickyCheckBox: function Js_Controls_ChatClient_View$get_stickyCheckBox() {
        /// <value type="Object" domElement="true"></value>
        if (this._StickyCheckBox == null) {
            this._StickyCheckBox = document.getElementById(this.clientId + '_StickyCheckBox');
        }
        return this._StickyCheckBox;
    },
    
    _StickyCheckBox: null,
    
    get_stickyCheckBoxJ: function Js_Controls_ChatClient_View$get_stickyCheckBoxJ() {
        /// <value type="jQueryObject"></value>
        if (this._StickyCheckBoxJ == null) {
            this._StickyCheckBoxJ = $('#' + this.clientId + '_StickyCheckBox');
        }
        return this._StickyCheckBoxJ;
    },
    
    _StickyCheckBoxJ: null,
    
    get_downlevelMain: function Js_Controls_ChatClient_View$get_downlevelMain() {
        /// <value type="Object" domElement="true"></value>
        if (this._DownlevelMain == null) {
            this._DownlevelMain = document.getElementById(this.clientId + '_DownlevelMain');
        }
        return this._DownlevelMain;
    },
    
    _DownlevelMain: null,
    
    get_downlevelMainJ: function Js_Controls_ChatClient_View$get_downlevelMainJ() {
        /// <value type="jQueryObject"></value>
        if (this._DownlevelMainJ == null) {
            this._DownlevelMainJ = $('#' + this.clientId + '_DownlevelMain');
        }
        return this._DownlevelMainJ;
    },
    
    _DownlevelMainJ: null,
    
    get_initGo: function Js_Controls_ChatClient_View$get_initGo() {
        /// <value type="Object" domElement="true"></value>
        if (this._InitGo == null) {
            this._InitGo = document.getElementById(this.clientId + '_InitGo');
        }
        return this._InitGo;
    },
    
    _InitGo: null,
    
    get_initGoJ: function Js_Controls_ChatClient_View$get_initGoJ() {
        /// <value type="jQueryObject"></value>
        if (this._InitGoJ == null) {
            this._InitGoJ = $('#' + this.clientId + '_InitGo');
        }
        return this._InitGoJ;
    },
    
    _InitGoJ: null,
    
    get_initUsrK: function Js_Controls_ChatClient_View$get_initUsrK() {
        /// <value type="Object" domElement="true"></value>
        if (this._InitUsrK == null) {
            this._InitUsrK = document.getElementById(this.clientId + '_InitUsrK');
        }
        return this._InitUsrK;
    },
    
    _InitUsrK: null,
    
    get_initUsrKJ: function Js_Controls_ChatClient_View$get_initUsrKJ() {
        /// <value type="jQueryObject"></value>
        if (this._InitUsrKJ == null) {
            this._InitUsrKJ = $('#' + this.clientId + '_InitUsrK');
        }
        return this._InitUsrKJ;
    },
    
    _InitUsrKJ: null,
    
    get_initClientID: function Js_Controls_ChatClient_View$get_initClientID() {
        /// <value type="Object" domElement="true"></value>
        if (this._InitClientID == null) {
            this._InitClientID = document.getElementById(this.clientId + '_InitClientID');
        }
        return this._InitClientID;
    },
    
    _InitClientID: null,
    
    get_initClientIDJ: function Js_Controls_ChatClient_View$get_initClientIDJ() {
        /// <value type="jQueryObject"></value>
        if (this._InitClientIDJ == null) {
            this._InitClientIDJ = $('#' + this.clientId + '_InitClientID');
        }
        return this._InitClientIDJ;
    },
    
    _InitClientIDJ: null,
    
    get_initLastActionTicks: function Js_Controls_ChatClient_View$get_initLastActionTicks() {
        /// <value type="Object" domElement="true"></value>
        if (this._InitLastActionTicks == null) {
            this._InitLastActionTicks = document.getElementById(this.clientId + '_InitLastActionTicks');
        }
        return this._InitLastActionTicks;
    },
    
    _InitLastActionTicks: null,
    
    get_initLastActionTicksJ: function Js_Controls_ChatClient_View$get_initLastActionTicksJ() {
        /// <value type="jQueryObject"></value>
        if (this._InitLastActionTicksJ == null) {
            this._InitLastActionTicksJ = $('#' + this.clientId + '_InitLastActionTicks');
        }
        return this._InitLastActionTicksJ;
    },
    
    _InitLastActionTicksJ: null,
    
    get_initSystemMessagesRoomGuid: function Js_Controls_ChatClient_View$get_initSystemMessagesRoomGuid() {
        /// <value type="Object" domElement="true"></value>
        if (this._InitSystemMessagesRoomGuid == null) {
            this._InitSystemMessagesRoomGuid = document.getElementById(this.clientId + '_InitSystemMessagesRoomGuid');
        }
        return this._InitSystemMessagesRoomGuid;
    },
    
    _InitSystemMessagesRoomGuid: null,
    
    get_initSystemMessagesRoomGuidJ: function Js_Controls_ChatClient_View$get_initSystemMessagesRoomGuidJ() {
        /// <value type="jQueryObject"></value>
        if (this._InitSystemMessagesRoomGuidJ == null) {
            this._InitSystemMessagesRoomGuidJ = $('#' + this.clientId + '_InitSystemMessagesRoomGuid');
        }
        return this._InitSystemMessagesRoomGuidJ;
    },
    
    _InitSystemMessagesRoomGuidJ: null,
    
    get_initInboxUpdatesRoomGuid: function Js_Controls_ChatClient_View$get_initInboxUpdatesRoomGuid() {
        /// <value type="Object" domElement="true"></value>
        if (this._InitInboxUpdatesRoomGuid == null) {
            this._InitInboxUpdatesRoomGuid = document.getElementById(this.clientId + '_InitInboxUpdatesRoomGuid');
        }
        return this._InitInboxUpdatesRoomGuid;
    },
    
    _InitInboxUpdatesRoomGuid: null,
    
    get_initInboxUpdatesRoomGuidJ: function Js_Controls_ChatClient_View$get_initInboxUpdatesRoomGuidJ() {
        /// <value type="jQueryObject"></value>
        if (this._InitInboxUpdatesRoomGuidJ == null) {
            this._InitInboxUpdatesRoomGuidJ = $('#' + this.clientId + '_InitInboxUpdatesRoomGuid');
        }
        return this._InitInboxUpdatesRoomGuidJ;
    },
    
    _InitInboxUpdatesRoomGuidJ: null,
    
    get_initBuddyAlertsRoomGuid: function Js_Controls_ChatClient_View$get_initBuddyAlertsRoomGuid() {
        /// <value type="Object" domElement="true"></value>
        if (this._InitBuddyAlertsRoomGuid == null) {
            this._InitBuddyAlertsRoomGuid = document.getElementById(this.clientId + '_InitBuddyAlertsRoomGuid');
        }
        return this._InitBuddyAlertsRoomGuid;
    },
    
    _InitBuddyAlertsRoomGuid: null,
    
    get_initBuddyAlertsRoomGuidJ: function Js_Controls_ChatClient_View$get_initBuddyAlertsRoomGuidJ() {
        /// <value type="jQueryObject"></value>
        if (this._InitBuddyAlertsRoomGuidJ == null) {
            this._InitBuddyAlertsRoomGuidJ = $('#' + this.clientId + '_InitBuddyAlertsRoomGuid');
        }
        return this._InitBuddyAlertsRoomGuidJ;
    },
    
    _InitBuddyAlertsRoomGuidJ: null,
    
    get_initBuddyStreamRoomGuid: function Js_Controls_ChatClient_View$get_initBuddyStreamRoomGuid() {
        /// <value type="Object" domElement="true"></value>
        if (this._InitBuddyStreamRoomGuid == null) {
            this._InitBuddyStreamRoomGuid = document.getElementById(this.clientId + '_InitBuddyStreamRoomGuid');
        }
        return this._InitBuddyStreamRoomGuid;
    },
    
    _InitBuddyStreamRoomGuid: null,
    
    get_initBuddyStreamRoomGuidJ: function Js_Controls_ChatClient_View$get_initBuddyStreamRoomGuidJ() {
        /// <value type="jQueryObject"></value>
        if (this._InitBuddyStreamRoomGuidJ == null) {
            this._InitBuddyStreamRoomGuidJ = $('#' + this.clientId + '_InitBuddyStreamRoomGuid');
        }
        return this._InitBuddyStreamRoomGuidJ;
    },
    
    _InitBuddyStreamRoomGuidJ: null,
    
    get_initAnimatePopups: function Js_Controls_ChatClient_View$get_initAnimatePopups() {
        /// <value type="Object" domElement="true"></value>
        if (this._InitAnimatePopups == null) {
            this._InitAnimatePopups = document.getElementById(this.clientId + '_InitAnimatePopups');
        }
        return this._InitAnimatePopups;
    },
    
    _InitAnimatePopups: null,
    
    get_initAnimatePopupsJ: function Js_Controls_ChatClient_View$get_initAnimatePopupsJ() {
        /// <value type="jQueryObject"></value>
        if (this._InitAnimatePopupsJ == null) {
            this._InitAnimatePopupsJ = $('#' + this.clientId + '_InitAnimatePopups');
        }
        return this._InitAnimatePopupsJ;
    },
    
    _InitAnimatePopupsJ: null,
    
    get_initTopPhoto: function Js_Controls_ChatClient_View$get_initTopPhoto() {
        /// <value type="Object" domElement="true"></value>
        if (this._InitTopPhoto == null) {
            this._InitTopPhoto = document.getElementById(this.clientId + '_InitTopPhoto');
        }
        return this._InitTopPhoto;
    },
    
    _InitTopPhoto: null,
    
    get_initTopPhotoJ: function Js_Controls_ChatClient_View$get_initTopPhotoJ() {
        /// <value type="jQueryObject"></value>
        if (this._InitTopPhotoJ == null) {
            this._InitTopPhotoJ = $('#' + this.clientId + '_InitTopPhoto');
        }
        return this._InitTopPhotoJ;
    },
    
    _InitTopPhotoJ: null
}


////////////////////////////////////////////////////////////////////////////////
// Js.Controls.ChatClient.Controller

Js.Controls.ChatClient.Controller = function Js_Controls_ChatClient_Controller() {
}


Js.Controls.ChatClient.View.registerClass('Js.Controls.ChatClient.View');
Js.Controls.ChatClient.Controller.registerClass('Js.Controls.ChatClient.Controller');
})(jQuery);

//! This script was generated using Script# v0.7.4.0
