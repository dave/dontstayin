<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="Spotted.Admin.Settings" %>

<dsi:h1 runat="server">Settings</dsi:h1>
<div class="ContentBorder">
	<asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
	<br />
	<asp:Button ID="btnSave" runat="server" Text="Save changes to settings and refresh" onclick="btnSave_Click" />
</div>
