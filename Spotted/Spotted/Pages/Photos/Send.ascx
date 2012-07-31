<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Send.ascx.cs" Inherits="Spotted.Pages.Photos.Send" %>
<%@ Register TagPrefix="dsi" TagName="Html" Src="/Controls/Html.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultiBuddyChooser" Src="~/Controls/MultiBuddyChooser.ascx" %>
<a name="EmailToFriend"></a>
<dsi:h1 runat="server" ID="H11">Email this photo to a friend</dsi:h1>
<div class="ContentBorder">
	<p align="center">
		<a href="" runat="server" id="PhotoAnchor"><img src="" runat="server" id="PhotoImg" class="BorderBlack All" border="0"></a>
	</p>
	<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Runat="server" Display="Dynamic" ErrorMessage="<p>Please enter a message</p>" 
		ControlToValidate="MessageHtml"/>
	<p>
		<dsi:Html runat="server" id="MessageHtml" PreviewType="Comment" DisableContainer="true" DisableSaveButton="true" TabIndexBase="100" />
	</p>
	<asp:Panel Runat="server" ID="BuddyPanel">
		<p>
			<a name="DropAnchor"></a>
			Choose who to send the photo to
			<uc1:MultiBuddyChooser runat="server" ID="MultiBuddyChooser" TabIndexBase="120" />
		</p>
	</asp:Panel>
	<p>
		<asp:Button Runat="server" onclick="SendEmails" Text="Send Emails" ID="Button1" EnableViewState="False" TabIndex="140"/>
		<asp:Label Runat="server" ID="SentEmailsLabel" ForeColor="#0000ff" />
	</p>
</div>
