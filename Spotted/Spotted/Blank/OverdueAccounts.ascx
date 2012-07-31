<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OverdueAccounts.ascx.cs" Inherits="Spotted.Blank.OverdueAccounts" %>
<%@ Register TagPrefix="Promoters" TagName="AccountsWarning" Src="/Controls/Promoters/AccountsWarning.ascx" %>
<link rel="stylesheet" type="text/css" href="/support/style.css?a=8"/>
<table width="600" align="center">
	<tr>
		<td align="center"><img src="/gfx/logo-200-90.jpg" border="0" style="margin:13px;" /></td>
	</tr>
	<tr>
		<td style="margin-top:13px; margin-bottom:13px;"><asp:Panel Runat="server" ID="LoggedInPanel" EnableViewState="False" HorizontalAlign="Center">
			<nobr>You're logged in as <asp:Label runat="server" id="LoggedInAs"></asp:Label>. 
			Please <asp:LinkButton Runat="server" OnClick="LogOutClick" CausesValidation="False" ID="LogOutLink">log off</asp:LinkButton> 
			when you're done.</nobr></asp:Panel></td>
	</tr>
	<tr>
		<td><Promoters:AccountsWarning runat="server" ID="PromoterAccountsWarningControl"/></td>
	</tr>
</table>
	
