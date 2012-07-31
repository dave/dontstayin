<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.ascx.cs" Inherits="Spotted.Admin.ChangePassword" %>
<h1>Change password</h1>
<div class="ContentBorder">
	<p>
		This will change the password of any user account.
	</p>
	<p>
		UsrK: <asp:TextBox Runat="server" ID="UsrK"></asp:TextBox>
	</p>
	<p>
		New password: <asp:TextBox Runat="server" ID="UsrPassword"></asp:TextBox>
	</p>
	<p>
		
		<asp:Button ID="Button2" Runat="server" OnClick="Change" Text="Change" />
	</p>
	<p>
		<asp:Label Runat="server" ID="OutLabel"/>
	</p>
</div>
