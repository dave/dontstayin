<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Invite.ascx.cs" Inherits="Spotted.Pages.Usrs.Invite" %>

<asp:Panel Runat="server" ID="PanelInvite">
	<dsi:h1 runat="server" ID="Header" NAME="H18"/>
	<div class="ContentBorder">
		<p>
			<img src="/gfx/icon-group.png" border="0" align="absmiddle" width="26" height="21" style="margin-right:3px;">To 
			invite <a href="" Runat="server" ID="InviteUsrAnchor" /> to join a group, select 
			the group from the drop-down below and click "Invite".
		</p>
		<p>
			<asp:DropDownList Runat="server" ID="GroupDropDown"></asp:DropDownList>
		</p>
		<p>
			Write a short message to this user inviting them to the group:
		</p>
		<p>
			<asp:TextBox Runat="server" ID="InviteMessage" TextMode="MultiLine" Columns="60" Rows="7"/>
		</p>
		<p>
			<asp:Button Runat="server" Text="Invite" OnClick="Invite_Click" ID="Button1"/>
		</p>
		<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Runat="server" Display="Dynamic" ControlToValidate="InviteMessage"
			ErrorMessage="<p>You must write a message before inviting this person!</p>"/>
		<asp:Panel Runat="server" ID="InviteErrorPanel" visible="false" EnableViewState="False">
			<h2>Error sending invite</h2>
			<p runat="server" id="InviteErrorMessage"/>
		</asp:Panel>
		<asp:Panel Runat="server" ID="InviteSentPanel" visible="false" EnableViewState="False">
			<h2>Invitation sent</h2>
			<p runat="server" id="InviteSentMessage"/>
		</asp:Panel>
	</div>
</asp:Panel>
<asp:Panel Runat="server" ID="PanelNoGroups">
	<dsi:h1 runat="server" ID="H11" NAME="H18">Oops!</dsi:h1>
	<div class="ContentBorder">
		<p>
			You're not a member of any groups. You must join some 
			groups before you can invite anyone into them!
		</p>
	</div>
</asp:Panel>
