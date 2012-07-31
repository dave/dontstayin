<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Inbox.ascx.cs" Inherits="Spotted.Pages.Inbox" %>
<%@ Register TagPrefix="Controls" TagName="Inbox" Src="/Controls/Inbox.ascx" %>
<%@ Register TagPrefix="AddThread" TagName="Thread" Src="/Controls/AddThread.ascx" %>
<asp:Panel Runat="server" ID="PanelInbox">
	<h1 class="TabHolder">
		<a href="/chat" class="TabbedHeading">Chat<!--<img src="/gfx/icon-discuss.png" class="TabbedHeadingIcon" border="0" height="21" width="26">--></a>
		<a href="/pages/inbox" class="TabbedHeading Selected" onclick="try { return WhenLoggedInAnchor(this); } catch(ex) { return false; }">Inbox<!--<img src="/gfx/icon-inbox-up.png" class="TabbedHeadingIcon" border="0" height="21" width="26">--></a>
		<a href="/pages/favourites" class="TabbedHeading" onclick="try { return WhenLoggedInAnchor(this); } catch(ex) { return false; }">Favourites<!--<img src="/gfx/icon-star-26-up.png" class="TabbedHeadingIcon" border="0" height="21" width="26">--></a>
		<a href="/pages/watching" class="TabbedHeading" onclick="try { return WhenLoggedInAnchor(this); } catch(ex) { return false; }">Watching<!--<img src="/gfx/icon-eye-up.png" class="TabbedHeadingIcon" border="0" height="21" width="26">--></a>
		<a href="<%= Bobs.Usr.Current != null ? Bobs.Usr.Current.UrlMyComments() : "/pages/mycomments" %>" class="TabbedHeading" onclick="try { return WhenLoggedInAnchor(this); } catch(ex) { return false; }">My&nbsp;comments<!--<img src="/gfx/icon-me-up.png" class="TabbedHeadingIcon" border="0" height="21" width="26">--></a>
	</h1>
	<div class="ContentBorder">
		<p>
			<a href="/" onclick="document.getElementById('<%= InboxIconHelp.ClientID %>').style.display=document.getElementById('<%= InboxIconHelp.ClientID %>').style.display == 'none' ? '' : 'none';return false;">
				What do the icons mean?</a> | 
			<a href="/" onclick="document.getElementById('<%= InboxEmails.ClientID %>').style.display=document.getElementById('<%= InboxEmails.ClientID %>').style.display == 'none' ? '' : 'none';return false;">
				inbox emails on / off</a> |
			<a href="/pages/spam">spam helper page</a> | 
			<a href="/" onclick="document.getElementById('<%= ClearMyInbox.ClientID %>').style.display=document.getElementById('<%= ClearMyInbox.ClientID %>').style.display == 'none' ? '' : 'none';return false;">
				clear my inbox</a>
		</p>
	</div>
	
	<div runat="server" id="InboxIconHelp" style="display:none;">
		<dsi:h1 runat="server">What do the icons mean?</dsi:h1>
		<div class="ContentBorder">
			<h2>
				<img src="/gfx/icon-eye-up.png" border="0" align="absmiddle" height="21" width="26" style="margin-right:3px;">The watch / ignore button
			</h2>
			<p>
				If you've had enough of a topic, click this button so it turns <img src="/gfx/icon-eye-dn.png" border="0" align="absmiddle" height="21" width="26">.
				It'll be removed from your inbox, and it <b>won't</b> come back.
			</p>
			
			<h2>
				<img src="/gfx/icon-inbox-up.png" border="0" align="absmiddle" height="21" width="26" style="margin-right:3px;">The inbox button
			</h2>
			<p>
				Once you've read a topic, click this button so it turns <img src="/gfx/icon-inbox-dn.png" border="0" align="absmiddle" height="21" width="26">. 
				It'll <b>come back</b> into your inbox when the next messages is posted.
			</p>
			
			
			<h2>
				<img src="/gfx/icon-star-26-up.png" border="0" align="absmiddle" height="21" width="26" style="margin-right:3px;">The favourite button
			</h2>
			<p>
				If you need to find a topic later, don't leave it in your inbox! Click this button so it turns <img src="/gfx/icon-star-26-up.png" border="0" align="absmiddle" height="21" width="26">.
				It'll be listed on your favourite topics page.
			</p>
		</div>
	</div>
	<div runat="server" id="InboxEmails" style="display:none;">
		<dsi:h1 runat="server">Inbox emails</dsi:h1>
		<div class="ContentBorder">
			<p>
				When something new comes into your inbox, we can send you an email. If you get loads of stuff in your inbox, 
				you may want to turn these alert emails off.
			</p>
			<p>
				<asp:RadioButton Runat="server" GroupName="InboxEmails" ID="InboxEmailsYes" Text="<b>Send me inbox emails</b>"></asp:RadioButton>
				<asp:RadioButton Runat="server" GroupName="InboxEmails" ID="InboxEmailsNo" Text="<b>Stop sending me inbox emails</b>"></asp:RadioButton>&nbsp;
				<button runat="server" onserverclick="UpdateInboxEmails" ID="Button1" CausesValidation="False">Update</button>
			</p>
		</div>
	</div>
	<div runat="server" id="ClearMyInbox" style="display:none;">
		<a name="ClearMyInbox"></a>
		<dsi:h1 runat="server">Clear my inbox</dsi:h1>
			<div class="ContentBorder">
			<p>
				If you've let your inbox get too big and you just want to start again, we can remove everything from your inbox.
			</p>
			<p>
				WAIT! Have you used the <a href="/pages/spam">Spam helper</a> page yet? It's a good idea to use that page before clearing your inbox.
			</p>
			<p>	
				To clear your inbox, enter your password in the box below to confirm:
			</p>
			<p>
				<asp:TextBox runat="server" ID="Password" TextMode="Password" Columns="20" /> <asp:Button ID="Button2" runat="server" Text="Confirm" OnClick="Go" CausesValidation="false" />
			</p>
			<p class="ForegroundAttentionRed" runat="server" id="Error" visible="false">
				Wrong password!
			</p>
		</div>
	</div>
	
	<a name="Threads"></a>
	<dsi:h1 runat="server" ID="H12" NAME="H13">Topics</dsi:h1>
	<div class="ContentBorder">
	
		<p class="BigCenter" id="AddThreadLinkP">
			<a href="/" onclick="SendPrivateMessageClick();return false;">Send a private message</a>
			<dsi:InlineScript ID="InlineScript1" runat="server">
				<script>
					function SendPrivateMessageClick() { document.getElementById('AddThreadPanel').style.display = ''; document.getElementById('AddThreadLinkP').style.display = 'none'; }
				</script>
			</dsi:InlineScript>
		</p>
		
		<div ID="AddThreadPanel" style="display:none">
			<AddThread:Thread runat="server" ID="AddThread"/>
		</div>
		
		<Controls:Inbox runat="server" id="InboxControl" />

	</div>
</asp:Panel>
