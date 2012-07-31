<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AccountsWarning.ascx.cs" Inherits="Spotted.Controls.Promoters.AccountsWarning" %>
<asp:Panel Runat="server" ID="PromoterAccountsOutstandingPanel">
	<dsi:h1 id="PromoterAccountsOutstandingHeader" runat="server">Your promoter account balance is outstanding</dsi:h1>
	<div class="ContentBorder">
		<h2 id="PromoterAccountsOutstandingMessage" runat="server" visible="false"></h2>
		<p runat="server" id="AccountLockoutHelp" visible="false">Click "Pay Now" or call our promoter hotline on 0207 835 5599</p>
		<table id="PromoterAccountsOutstandingTable" runat="server" cellpadding="3" cellspacing="0"></table>		
	</div>
</asp:Panel>
