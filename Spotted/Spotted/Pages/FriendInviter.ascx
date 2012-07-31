<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FriendInviter.ascx.cs" Inherits="Spotted.Pages.FriendInviter" %>
<%@ Register TagPrefix="DsiControls" TagName="BuddyImporter" Src="/Controls/BuddyImporter.ascx" %>

<dsi:h1 runat="server" ID="uiH1" NAME="H1" PaddingLeftPx="0">Invite your friends to DontStayIn!</dsi:h1>
<div class="ContentBorder">
	<asp:Panel runat="server" ID="uiIntroPanel" Visible="true">
		<p>We can find all your contacts from your email account and invite them to DontStayIn!</p>
	</asp:Panel>
	
	<DsiControls:BuddyImporter runat="server" ID="uiBuddyImporter"></DsiControls:BuddyImporter>
	
	<asp:Panel runat="server" ID="uiSuccessPanel" Visible="false">
		<p><a href="<%= HttpContext.Current.Request.Url %>"><b>Go again with another email account</b></a></p>
		<p><a href="<%= Usr.Current.UrlBuddyRequestsIveSent() %>"><b>Check the buddy requests I've sent</b></a></p>
		<p><a href="<%= Usr.Current.Url() %>"><b>Back to my profile</b></a></p>
	</asp:Panel>
	
</div>
