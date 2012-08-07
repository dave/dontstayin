<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TicketFundsRelease.ascx.cs" Inherits="Spotted.Admin.TicketFundsRelease" %>
<%@ Register TagPrefix="DbCombo" Namespace="Cambro.Web.DbCombo" Assembly="Cambro.Web.DbCombo" %>
<asp:Panel Runat="server" ID="AllTicketRunsPanel">
	<div class="ContentBorder">
		<p style="display:none;">
			<asp:LinkButton ID="SelectAwaitingFundsReleaseLinkButton" runat="server">Awaiting funds release</asp:LinkButton> | 
			<asp:LinkButton ID="SelectLockedFundsLinkButton" runat="server">Locked funds</asp:LinkButton> | 
			<asp:LinkButton ID="SelectAllTicketFundsLinkButton" runat="server">All ticket funds</asp:LinkButton>
		</p>
		<table ID="SearchTable" runat="server" cellpadding="5" cellspacing="0" border="0">
			<tr>
				<td><nobr>Promoter</nobr></td>
				<td><js:HtmlAutoComplete Width="140px" ID="uiPromotersAutoComplete" runat="server"  WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetPromotersWithK"/></td>
				<td><nobr>Event date from</nobr></td>
				<td><dsi:Cal id="FromDateCal" runat="server" TabIndex="30"></dsi:Cal></td>
