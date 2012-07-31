<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChatClient.ascx.cs" Inherits="Spotted.Controls.ChatClient" %>
<div runat="server" id="OuterMain" style="position:relative;">
	<h1 class="TabHolder">
		<a xhref="/" class="TabbedHeading Selected" runat="server" id="TabsChatLink">Chat</a>
	</h1>
	<div runat="server" id="TabChatHolder">
		<div class="ContentBorder Padding ChatClientMain">
			<div runat="server" id="RoomsMain">
				<div runat="server" id="RoomList" class="ChatClientRoomList ClearAfter" />
				<asp:DropDownList runat="server" ID="PrivateChatDrop" CssClass="ChatClientPrivateChatDrop" EnableViewState="false">
					<asp:ListItem Text="Chat with..." Value="0" />
				</asp:DropDownList>
				<div runat="server" id="RoomPrivateListDivider" class="ChatClientRoomListDivider" visible="false" />
				<div runat="server" id="RoomPrivateList" class="ChatClientRoomPrivateList ClearAfter" />
					
				<div runat="server" id="RoomGuestListDivider" class="ChatClientRoomListDivider" />
				<div runat="server" id="RoomGuestList" class="ChatClientRoomGuestList ClearAfter" />
			</div>
			<div runat="server" id="PrivateChatDropMain" class="ChatClientPrivateChatDropMain"></div>
			<div runat="server" id="MessagesMain">
				<div runat="server" class="ChatClientTextBoxHolder" id="TextBoxHolder"><input runat="server" class="ChatClientTextBoxWatermark" id="TextBox" type="text" value="Enter your message here..." autocomplete="off" maxlength="200"></div>
				<div runat="server" class="ChatClientMessageList" id="MessageList" style="height:300px;">
					<div runat="server" id="WrongSessionHolder" style="display:none;" class="ChatClientMessageListHiddenHolder">
						Chat has been paused because you have another window open.
						To continue chatting in this window, <a href="#" runat="server" id="WrongSessionResumeLink">click here</a>.
					</div>
					<div runat="server" id="TimeoutHolder" style="display:none;" class="ChatClientMessageListHiddenHolder">
						Chat has been paused because you have been idle for 5 minutes.
						To continue chatting, <a href="#" runat="server" id="TimeoutResumeLink">click here</a>.
					</div>
				</div>
				<div runat="server" id="DeleteArchiveHolder" class="ChatClientDeleteArchiveHolder">
					Had a private chat? <a href="#" runat="server" id="DeleteArchiveAnchor">Delete archived messages</a>. 
					<asp:label runat="server" ID="DeleteArchiveDoneLabel" style="display:none;"><b>OK</b>.</asp:label>
				</div>
				<div class="ChatClientDeleteArchiveHolder">
					Random stream:
				</div>
				<div runat="server" class="ChatClientMessageList" id="StreamList" style="height:150px;"></div>
				<p runat="server" visible="false">
					<asp:CheckBox ID="PopupsCheckBox" runat="server" Text="Popups" />
					<asp:CheckBox ID="StickyCheckBox" runat="server" Text="Sticky" onclick="if (document.getElementById('SkyscraperOuterDiv') != null) document.getElementById('SkyscraperOuterDiv').style.display = this.checked ? 'none' : ''; if (this.checked) RepositionChatBox(); else RepositionChatBoxAtTop();" />
				</p>
			</div>
			
			

		</div>
		<div runat="server" class="ContentBorder ChatClientMain" id="DownlevelMain" visible="false">
			<p>
				Your browser looks like it's not compatible with our live chat box. We recommend <a href="http://www.firefox.com/" target="_blank">FireFox</a>.
			</p>
		</div>
		
	</div>
	<input type="hidden" runat="server" id="InitGo" />
	<input type="hidden" runat="server" id="InitUsrK" />
	<input type="hidden" runat="server" id="InitClientID" />
	<input type="hidden" runat="server" id="InitLastActionTicks" />
	<input type="hidden" runat="server" id="InitSystemMessagesRoomGuid" />
	<input type="hidden" runat="server" id="InitInboxUpdatesRoomGuid" />
	<input type="hidden" runat="server" id="InitBuddyAlertsRoomGuid" />
	<input type="hidden" runat="server" id="InitBuddyStreamRoomGuid" />
	<input type="hidden" runat="server" id="InitAnimatePopups" />
	<input type="hidden" runat="server" id="InitTopPhoto" />
</div>
