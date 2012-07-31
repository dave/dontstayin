//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Controls.ChatClient
{
	public partial class View
	{
		public string clientId;
		public View(string clientId)
		{
			this.clientId = clientId;
		}
		public Element OuterMain {get {if (_OuterMain == null) {_OuterMain = (Element)Document.GetElementById(clientId + "_OuterMain");}; return _OuterMain;}} private Element _OuterMain;
		public jQueryObject OuterMainJ {get {if (_OuterMainJ == null) {_OuterMainJ = jQuery.Select("#" + clientId + "_OuterMain");}; return _OuterMainJ;}} private jQueryObject _OuterMainJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public AnchorElement TabsChatLink {get {if (_TabsChatLink == null) {_TabsChatLink = (AnchorElement)Document.GetElementById(clientId + "_TabsChatLink");}; return _TabsChatLink;}} private AnchorElement _TabsChatLink;
		public jQueryObject TabsChatLinkJ {get {if (_TabsChatLinkJ == null) {_TabsChatLinkJ = jQuery.Select("#" + clientId + "_TabsChatLink");}; return _TabsChatLinkJ;}} private jQueryObject _TabsChatLinkJ;
		public Element TabChatHolder {get {if (_TabChatHolder == null) {_TabChatHolder = (Element)Document.GetElementById(clientId + "_TabChatHolder");}; return _TabChatHolder;}} private Element _TabChatHolder;
		public jQueryObject TabChatHolderJ {get {if (_TabChatHolderJ == null) {_TabChatHolderJ = jQuery.Select("#" + clientId + "_TabChatHolder");}; return _TabChatHolderJ;}} private jQueryObject _TabChatHolderJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element RoomsMain {get {if (_RoomsMain == null) {_RoomsMain = (Element)Document.GetElementById(clientId + "_RoomsMain");}; return _RoomsMain;}} private Element _RoomsMain;
		public jQueryObject RoomsMainJ {get {if (_RoomsMainJ == null) {_RoomsMainJ = jQuery.Select("#" + clientId + "_RoomsMain");}; return _RoomsMainJ;}} private jQueryObject _RoomsMainJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element RoomList {get {if (_RoomList == null) {_RoomList = (Element)Document.GetElementById(clientId + "_RoomList");}; return _RoomList;}} private Element _RoomList;
		public jQueryObject RoomListJ {get {if (_RoomListJ == null) {_RoomListJ = jQuery.Select("#" + clientId + "_RoomList");}; return _RoomListJ;}} private jQueryObject _RoomListJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public SelectElement PrivateChatDrop {get {if (_PrivateChatDrop == null) {_PrivateChatDrop = (SelectElement)Document.GetElementById(clientId + "_PrivateChatDrop");}; return _PrivateChatDrop;}} private SelectElement _PrivateChatDrop;
		public jQueryObject PrivateChatDropJ {get {if (_PrivateChatDropJ == null) {_PrivateChatDropJ = jQuery.Select("#" + clientId + "_PrivateChatDrop");}; return _PrivateChatDropJ;}} private jQueryObject _PrivateChatDropJ;
		public Element RoomPrivateListDivider {get {if (_RoomPrivateListDivider == null) {_RoomPrivateListDivider = (Element)Document.GetElementById(clientId + "_RoomPrivateListDivider");}; return _RoomPrivateListDivider;}} private Element _RoomPrivateListDivider;
		public jQueryObject RoomPrivateListDividerJ {get {if (_RoomPrivateListDividerJ == null) {_RoomPrivateListDividerJ = jQuery.Select("#" + clientId + "_RoomPrivateListDivider");}; return _RoomPrivateListDividerJ;}} private jQueryObject _RoomPrivateListDividerJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element RoomPrivateList {get {if (_RoomPrivateList == null) {_RoomPrivateList = (Element)Document.GetElementById(clientId + "_RoomPrivateList");}; return _RoomPrivateList;}} private Element _RoomPrivateList;
		public jQueryObject RoomPrivateListJ {get {if (_RoomPrivateListJ == null) {_RoomPrivateListJ = jQuery.Select("#" + clientId + "_RoomPrivateList");}; return _RoomPrivateListJ;}} private jQueryObject _RoomPrivateListJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element RoomGuestListDivider {get {if (_RoomGuestListDivider == null) {_RoomGuestListDivider = (Element)Document.GetElementById(clientId + "_RoomGuestListDivider");}; return _RoomGuestListDivider;}} private Element _RoomGuestListDivider;
		public jQueryObject RoomGuestListDividerJ {get {if (_RoomGuestListDividerJ == null) {_RoomGuestListDividerJ = jQuery.Select("#" + clientId + "_RoomGuestListDivider");}; return _RoomGuestListDividerJ;}} private jQueryObject _RoomGuestListDividerJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element RoomGuestList {get {if (_RoomGuestList == null) {_RoomGuestList = (Element)Document.GetElementById(clientId + "_RoomGuestList");}; return _RoomGuestList;}} private Element _RoomGuestList;
		public jQueryObject RoomGuestListJ {get {if (_RoomGuestListJ == null) {_RoomGuestListJ = jQuery.Select("#" + clientId + "_RoomGuestList");}; return _RoomGuestListJ;}} private jQueryObject _RoomGuestListJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element PrivateChatDropMain {get {if (_PrivateChatDropMain == null) {_PrivateChatDropMain = (Element)Document.GetElementById(clientId + "_PrivateChatDropMain");}; return _PrivateChatDropMain;}} private Element _PrivateChatDropMain;
		public jQueryObject PrivateChatDropMainJ {get {if (_PrivateChatDropMainJ == null) {_PrivateChatDropMainJ = jQuery.Select("#" + clientId + "_PrivateChatDropMain");}; return _PrivateChatDropMainJ;}} private jQueryObject _PrivateChatDropMainJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element MessagesMain {get {if (_MessagesMain == null) {_MessagesMain = (Element)Document.GetElementById(clientId + "_MessagesMain");}; return _MessagesMain;}} private Element _MessagesMain;
		public jQueryObject MessagesMainJ {get {if (_MessagesMainJ == null) {_MessagesMainJ = jQuery.Select("#" + clientId + "_MessagesMain");}; return _MessagesMainJ;}} private jQueryObject _MessagesMainJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element TextBoxHolder {get {if (_TextBoxHolder == null) {_TextBoxHolder = (Element)Document.GetElementById(clientId + "_TextBoxHolder");}; return _TextBoxHolder;}} private Element _TextBoxHolder;
		public jQueryObject TextBoxHolderJ {get {if (_TextBoxHolderJ == null) {_TextBoxHolderJ = jQuery.Select("#" + clientId + "_TextBoxHolder");}; return _TextBoxHolderJ;}} private jQueryObject _TextBoxHolderJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public InputElement TextBox {get {if (_TextBox == null) {_TextBox = (InputElement)Document.GetElementById(clientId + "_TextBox");}; return _TextBox;}} private InputElement _TextBox;
		public jQueryObject TextBoxJ {get {if (_TextBoxJ == null) {_TextBoxJ = jQuery.Select("#" + clientId + "_TextBox");}; return _TextBoxJ;}} private jQueryObject _TextBoxJ;
		public Element MessageList {get {if (_MessageList == null) {_MessageList = (Element)Document.GetElementById(clientId + "_MessageList");}; return _MessageList;}} private Element _MessageList;
		public jQueryObject MessageListJ {get {if (_MessageListJ == null) {_MessageListJ = jQuery.Select("#" + clientId + "_MessageList");}; return _MessageListJ;}} private jQueryObject _MessageListJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public Element WrongSessionHolder {get {if (_WrongSessionHolder == null) {_WrongSessionHolder = (Element)Document.GetElementById(clientId + "_WrongSessionHolder");}; return _WrongSessionHolder;}} private Element _WrongSessionHolder;
		public jQueryObject WrongSessionHolderJ {get {if (_WrongSessionHolderJ == null) {_WrongSessionHolderJ = jQuery.Select("#" + clientId + "_WrongSessionHolder");}; return _WrongSessionHolderJ;}} private jQueryObject _WrongSessionHolderJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public AnchorElement WrongSessionResumeLink {get {if (_WrongSessionResumeLink == null) {_WrongSessionResumeLink = (AnchorElement)Document.GetElementById(clientId + "_WrongSessionResumeLink");}; return _WrongSessionResumeLink;}} private AnchorElement _WrongSessionResumeLink;
		public jQueryObject WrongSessionResumeLinkJ {get {if (_WrongSessionResumeLinkJ == null) {_WrongSessionResumeLinkJ = jQuery.Select("#" + clientId + "_WrongSessionResumeLink");}; return _WrongSessionResumeLinkJ;}} private jQueryObject _WrongSessionResumeLinkJ;
		public Element TimeoutHolder {get {if (_TimeoutHolder == null) {_TimeoutHolder = (Element)Document.GetElementById(clientId + "_TimeoutHolder");}; return _TimeoutHolder;}} private Element _TimeoutHolder;
		public jQueryObject TimeoutHolderJ {get {if (_TimeoutHolderJ == null) {_TimeoutHolderJ = jQuery.Select("#" + clientId + "_TimeoutHolder");}; return _TimeoutHolderJ;}} private jQueryObject _TimeoutHolderJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public AnchorElement TimeoutResumeLink {get {if (_TimeoutResumeLink == null) {_TimeoutResumeLink = (AnchorElement)Document.GetElementById(clientId + "_TimeoutResumeLink");}; return _TimeoutResumeLink;}} private AnchorElement _TimeoutResumeLink;
		public jQueryObject TimeoutResumeLinkJ {get {if (_TimeoutResumeLinkJ == null) {_TimeoutResumeLinkJ = jQuery.Select("#" + clientId + "_TimeoutResumeLink");}; return _TimeoutResumeLinkJ;}} private jQueryObject _TimeoutResumeLinkJ;
		public Element DeleteArchiveHolder {get {if (_DeleteArchiveHolder == null) {_DeleteArchiveHolder = (Element)Document.GetElementById(clientId + "_DeleteArchiveHolder");}; return _DeleteArchiveHolder;}} private Element _DeleteArchiveHolder;
		public jQueryObject DeleteArchiveHolderJ {get {if (_DeleteArchiveHolderJ == null) {_DeleteArchiveHolderJ = jQuery.Select("#" + clientId + "_DeleteArchiveHolder");}; return _DeleteArchiveHolderJ;}} private jQueryObject _DeleteArchiveHolderJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public AnchorElement DeleteArchiveAnchor {get {if (_DeleteArchiveAnchor == null) {_DeleteArchiveAnchor = (AnchorElement)Document.GetElementById(clientId + "_DeleteArchiveAnchor");}; return _DeleteArchiveAnchor;}} private AnchorElement _DeleteArchiveAnchor;
		public jQueryObject DeleteArchiveAnchorJ {get {if (_DeleteArchiveAnchorJ == null) {_DeleteArchiveAnchorJ = jQuery.Select("#" + clientId + "_DeleteArchiveAnchor");}; return _DeleteArchiveAnchorJ;}} private jQueryObject _DeleteArchiveAnchorJ;
		public Element DeleteArchiveDoneLabel {get {if (_DeleteArchiveDoneLabel == null) {_DeleteArchiveDoneLabel = (Element)Document.GetElementById(clientId + "_DeleteArchiveDoneLabel");}; return _DeleteArchiveDoneLabel;}} private Element _DeleteArchiveDoneLabel;
		public jQueryObject DeleteArchiveDoneLabelJ {get {if (_DeleteArchiveDoneLabelJ == null) {_DeleteArchiveDoneLabelJ = jQuery.Select("#" + clientId + "_DeleteArchiveDoneLabel");}; return _DeleteArchiveDoneLabelJ;}} private jQueryObject _DeleteArchiveDoneLabelJ;//mappings.Add("System.Web.UI.WebControls.Label", ElementGetter("Element"));
		public Element StreamList {get {if (_StreamList == null) {_StreamList = (Element)Document.GetElementById(clientId + "_StreamList");}; return _StreamList;}} private Element _StreamList;
		public jQueryObject StreamListJ {get {if (_StreamListJ == null) {_StreamListJ = jQuery.Select("#" + clientId + "_StreamList");}; return _StreamListJ;}} private jQueryObject _StreamListJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public CheckBoxElement PopupsCheckBox {get {if (_PopupsCheckBox == null) {_PopupsCheckBox = (CheckBoxElement)Document.GetElementById(clientId + "_PopupsCheckBox");}; return _PopupsCheckBox;}} private CheckBoxElement _PopupsCheckBox;
		public jQueryObject PopupsCheckBoxJ {get {if (_PopupsCheckBoxJ == null) {_PopupsCheckBoxJ = jQuery.Select("#" + clientId + "_PopupsCheckBox");}; return _PopupsCheckBoxJ;}} private jQueryObject _PopupsCheckBoxJ;
		public CheckBoxElement StickyCheckBox {get {if (_StickyCheckBox == null) {_StickyCheckBox = (CheckBoxElement)Document.GetElementById(clientId + "_StickyCheckBox");}; return _StickyCheckBox;}} private CheckBoxElement _StickyCheckBox;
		public jQueryObject StickyCheckBoxJ {get {if (_StickyCheckBoxJ == null) {_StickyCheckBoxJ = jQuery.Select("#" + clientId + "_StickyCheckBox");}; return _StickyCheckBoxJ;}} private jQueryObject _StickyCheckBoxJ;
		public Element DownlevelMain {get {if (_DownlevelMain == null) {_DownlevelMain = (Element)Document.GetElementById(clientId + "_DownlevelMain");}; return _DownlevelMain;}} private Element _DownlevelMain;
		public jQueryObject DownlevelMainJ {get {if (_DownlevelMainJ == null) {_DownlevelMainJ = jQuery.Select("#" + clientId + "_DownlevelMain");}; return _DownlevelMainJ;}} private jQueryObject _DownlevelMainJ;//mappings.Add("System.Web.UI.HtmlControls.HtmlGenericControl", ElementGetter("Element"));
		public InputElement InitGo {get {if (_InitGo == null) {_InitGo = (InputElement)Document.GetElementById(clientId + "_InitGo");}; return _InitGo;}} private InputElement _InitGo;
		public jQueryObject InitGoJ {get {if (_InitGoJ == null) {_InitGoJ = jQuery.Select("#" + clientId + "_InitGo");}; return _InitGoJ;}} private jQueryObject _InitGoJ;
		public InputElement InitUsrK {get {if (_InitUsrK == null) {_InitUsrK = (InputElement)Document.GetElementById(clientId + "_InitUsrK");}; return _InitUsrK;}} private InputElement _InitUsrK;
		public jQueryObject InitUsrKJ {get {if (_InitUsrKJ == null) {_InitUsrKJ = jQuery.Select("#" + clientId + "_InitUsrK");}; return _InitUsrKJ;}} private jQueryObject _InitUsrKJ;
		public InputElement InitClientID {get {if (_InitClientID == null) {_InitClientID = (InputElement)Document.GetElementById(clientId + "_InitClientID");}; return _InitClientID;}} private InputElement _InitClientID;
		public jQueryObject InitClientIDJ {get {if (_InitClientIDJ == null) {_InitClientIDJ = jQuery.Select("#" + clientId + "_InitClientID");}; return _InitClientIDJ;}} private jQueryObject _InitClientIDJ;
		public InputElement InitLastActionTicks {get {if (_InitLastActionTicks == null) {_InitLastActionTicks = (InputElement)Document.GetElementById(clientId + "_InitLastActionTicks");}; return _InitLastActionTicks;}} private InputElement _InitLastActionTicks;
		public jQueryObject InitLastActionTicksJ {get {if (_InitLastActionTicksJ == null) {_InitLastActionTicksJ = jQuery.Select("#" + clientId + "_InitLastActionTicks");}; return _InitLastActionTicksJ;}} private jQueryObject _InitLastActionTicksJ;
		public InputElement InitSystemMessagesRoomGuid {get {if (_InitSystemMessagesRoomGuid == null) {_InitSystemMessagesRoomGuid = (InputElement)Document.GetElementById(clientId + "_InitSystemMessagesRoomGuid");}; return _InitSystemMessagesRoomGuid;}} private InputElement _InitSystemMessagesRoomGuid;
		public jQueryObject InitSystemMessagesRoomGuidJ {get {if (_InitSystemMessagesRoomGuidJ == null) {_InitSystemMessagesRoomGuidJ = jQuery.Select("#" + clientId + "_InitSystemMessagesRoomGuid");}; return _InitSystemMessagesRoomGuidJ;}} private jQueryObject _InitSystemMessagesRoomGuidJ;
		public InputElement InitInboxUpdatesRoomGuid {get {if (_InitInboxUpdatesRoomGuid == null) {_InitInboxUpdatesRoomGuid = (InputElement)Document.GetElementById(clientId + "_InitInboxUpdatesRoomGuid");}; return _InitInboxUpdatesRoomGuid;}} private InputElement _InitInboxUpdatesRoomGuid;
		public jQueryObject InitInboxUpdatesRoomGuidJ {get {if (_InitInboxUpdatesRoomGuidJ == null) {_InitInboxUpdatesRoomGuidJ = jQuery.Select("#" + clientId + "_InitInboxUpdatesRoomGuid");}; return _InitInboxUpdatesRoomGuidJ;}} private jQueryObject _InitInboxUpdatesRoomGuidJ;
		public InputElement InitBuddyAlertsRoomGuid {get {if (_InitBuddyAlertsRoomGuid == null) {_InitBuddyAlertsRoomGuid = (InputElement)Document.GetElementById(clientId + "_InitBuddyAlertsRoomGuid");}; return _InitBuddyAlertsRoomGuid;}} private InputElement _InitBuddyAlertsRoomGuid;
		public jQueryObject InitBuddyAlertsRoomGuidJ {get {if (_InitBuddyAlertsRoomGuidJ == null) {_InitBuddyAlertsRoomGuidJ = jQuery.Select("#" + clientId + "_InitBuddyAlertsRoomGuid");}; return _InitBuddyAlertsRoomGuidJ;}} private jQueryObject _InitBuddyAlertsRoomGuidJ;
		public InputElement InitBuddyStreamRoomGuid {get {if (_InitBuddyStreamRoomGuid == null) {_InitBuddyStreamRoomGuid = (InputElement)Document.GetElementById(clientId + "_InitBuddyStreamRoomGuid");}; return _InitBuddyStreamRoomGuid;}} private InputElement _InitBuddyStreamRoomGuid;
		public jQueryObject InitBuddyStreamRoomGuidJ {get {if (_InitBuddyStreamRoomGuidJ == null) {_InitBuddyStreamRoomGuidJ = jQuery.Select("#" + clientId + "_InitBuddyStreamRoomGuid");}; return _InitBuddyStreamRoomGuidJ;}} private jQueryObject _InitBuddyStreamRoomGuidJ;
		public InputElement InitAnimatePopups {get {if (_InitAnimatePopups == null) {_InitAnimatePopups = (InputElement)Document.GetElementById(clientId + "_InitAnimatePopups");}; return _InitAnimatePopups;}} private InputElement _InitAnimatePopups;
		public jQueryObject InitAnimatePopupsJ {get {if (_InitAnimatePopupsJ == null) {_InitAnimatePopupsJ = jQuery.Select("#" + clientId + "_InitAnimatePopups");}; return _InitAnimatePopupsJ;}} private jQueryObject _InitAnimatePopupsJ;
		public InputElement InitTopPhoto {get {if (_InitTopPhoto == null) {_InitTopPhoto = (InputElement)Document.GetElementById(clientId + "_InitTopPhoto");}; return _InitTopPhoto;}} private InputElement _InitTopPhoto;
		public jQueryObject InitTopPhotoJ {get {if (_InitTopPhotoJ == null) {_InitTopPhotoJ = jQuery.Select("#" + clientId + "_InitTopPhoto");}; return _InitTopPhotoJ;}} private jQueryObject _InitTopPhotoJ;
	}
}
