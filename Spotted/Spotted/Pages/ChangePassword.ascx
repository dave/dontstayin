<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.ascx.cs" Inherits="Spotted.Pages.ChangePassword" %>
<asp:Panel runat="server" ID="PanelChange">
	<dsi:h1 runat="server">Change password</dsi:h1>
	<div class="ContentBorder">
		<p>
			If you wish to change your password, enter your current password in the top box, and your new password in the bottom boxes.
		</p>
		<p>
			This password is only used if you disconnect your Facebook account, and want to reconnect to this account later. This page won't change your Facebook password.
		</p>
		<p>
			If you don't remember your current password, we can <a href="/pages/password">send you a reset link by email</a>.
		</p>
		<p>
			<table cellSpacing="0" cellPadding="2" border="0">
				<tr>
					<td align="right">
						Current password:
					</td>
					<td>
						<asp:TextBox id="CurrentPassword" Runat="server" Columns="20" TextMode="Password"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td align="right">
						New password:
					</td>
					<td>
						<asp:TextBox id="Password1" Runat="server" Columns="20" TextMode="Password" MaxLength="20"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td align="right">
						Confirm your new password:
					</td>
					<td>
						<asp:TextBox id="Password2" Runat="server" Columns="20" TextMode="Password" MaxLength="20"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td align="right">
						&nbsp;
					</td>
					<td>
						<asp:Button ID="Button1" runat="server" OnClick="Change_Click" Text="Change my password" />
					</td>
				</tr>
			</table>
		</p>
		<asp:CustomValidator ID="CustomValidator1" runat="server" Display="dynamic" ControlToValidate="CurrentPassword" EnableClientScript="false" OnServerValidate="CurrentPassword_Val" ErrorMessage="<p>Your current password is wrong. If you can't remember it, we can <a href='/pages/password'>send you a reset link by email</a>.</p>" />
		<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="CurrentPassword" ErrorMessage="<p>Please enter your current password</p>"/>
		<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="Password2" ErrorMessage="<p>Please enter your new password</p>"/>
		<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="dynamic" ControlToValidate="Password2" ValidationExpression="^.{4,20}$" ErrorMessage="<p>Please enter between 4 and 20 characters for your new password</p>" />
		<asp:CompareValidator ID="CompareValidator1" runat="server" Display="dynamic" ControlToCompare="Password1" ControlToValidate="Password2" Type="String" Operator="Equal" ErrorMessage="<p>The passwords you entered don't match. Please try again.</p>" />
	</div>
</asp:Panel>
<asp:Panel runat="server" ID="PanelDone">
	<dsi:h1 runat="server">Done!</dsi:h1>
	<div class="ContentBorder">
		<p>
			You've changed your password.
		</p>
	</div>
</asp:Panel>
