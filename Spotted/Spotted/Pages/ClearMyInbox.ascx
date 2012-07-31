<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClearMyInbox.ascx.cs" Inherits="Spotted.Pages.ClearMyInbox" %>
<a name="Details" />
<dsi:h1 runat="server">
	Clear my inbox
</dsi:h1>
<div class="ContentBorder">
	<p>
		<center>
			<button style="width:300px; height:100px;" onclick="document.getElementById('<% = ConfirmDiv.ClientID %>').style.display = '';return false;">
				Clear my friggin' inbox, damn it!
			</button>
		</center>
	</p>
	<p>
		WAIT! Have you used the <a href="/pages/spam">Spam helper</a> page yet? It's a good idea to use that page before clearing your inbox.
	</p>
	<center>
		<div runat="server" style="display:none;padding:10px;width:280px;" class="BackgroundVeryLight BorderKeyline All" id="ConfirmDiv">
			<p>
				Are you sure? Enter your password below to confirm:
			</p>
			<p>
				<asp:TextBox runat="server" ID="Password" TextMode="Password" Columns="20" />
			</p>
			<p>
				<asp:Button runat="server" Text="Confirm" OnClick="Go" />
			</p>
			<p class="ForegroundAttentionRed" runat="server" id="Error" visible="false">
				Wrong password!
			</p>
			<p runat="server" id="Done" visible="false">
				Done! Check out your <a href="/pages/inbox">lovely empty Inbox</a>!
			</p>
		</div>
	</center>
</div>
