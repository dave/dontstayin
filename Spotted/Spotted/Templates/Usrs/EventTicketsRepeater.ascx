<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventTicketsRepeater.ascx.cs" Inherits="Spotted.Templates.Usrs.EventTicketsRepeater" %>
<%@ Import Namespace="Bobs" %>
<tr id="TicketRunDetailsRow" runat="server" valign="top" style="font-weight:bold;border-left:solid 1px #CBA21E;">
	<td align="right"><asp:TextBox ID="TicketKTextBox" runat="server" Text="<%#CurrentTicket.K%>" Visible="false"></asp:TextBox><nobr><asp:Label id="NumberOfTicketsLabel" runat="server" style="text-align:right;" Text="<%#CurrentTicket.Quantity%>"></asp:Label> x</nobr></td>
	<td align="right" width="30"><%#Convert.ToDouble(CurrentTicket.ExtraSelectElements["TicketRunPrice"]).ToString("c")%></td>
	<td width="600"><%#CurrentTicket.ExtraSelectElements["TicketRunName"]%></td>
	<td align="center"><%# CurrentTicket.Code.Length > 0 ? "\"" + CurrentTicket.Code + "\"" : ""%></td>
	<td align="center"><%# CurrentTicketRunDeliveryType != TicketRun.DeliveryMethodType.E_Ticket ? "" : "\"" + CurrentTicket.CardNumberEnd + "\"" %></td>
	<td align="center" rowspan="<%# TicketRunHasDescription && TicketRunHasContactEmail ? 3 : (TicketRunHasDescription || TicketRunHasContactEmail ? 2 : 1)%>"><%# CurrentTicketRunDeliveryType != TicketRun.DeliveryMethodType.E_Ticket ? "" : Utilities.LinkNewWindow(CurrentTicket.UrlReport(), Utilities.IconHtml(Utilities.Icon.Printer))%></td>
	<td rowspan="<%#TicketRunHasDescription && TicketRunHasContactEmail ? 3 : (TicketRunHasDescription || TicketRunHasContactEmail ? 2 : 1)%>"><asp:Label ID="CancelledLabel" runat="server" Text='<%#CurrentTicket.Cancelled ? "CANCELLED" : ""%>' ForeColor="Red"></asp:Label></td>
</tr>
<tr id="TicketRunDescriptionRow" runat="server" style="padding-top:0px;border-left:solid 1px #CBA21E;" valign="top" visible="<%#TicketRunHasDescription%>">
	<td></td>
	<td colspan="4"><%#CurrentTicket.ExtraSelectElements["TicketRunDescription"]%></td>
</tr>
<tr id="TicketRunDeliveryAddress" style="background-color:transparent;" runat="server" visible='<%# CurrentTicketRunDeliveryType != TicketRun.DeliveryMethodType.E_Ticket %>'>
	<td /><td colspan="4">Delivery address: <%# CurrentTicket.SingleLineAddress %></td>
</tr>
<tr id="Tr1" runat="server" style="background-color:transparent;" visible='<%# CurrentTicketRunDeliveryType == TicketRun.DeliveryMethodType.E_Ticket %>'>
	<td /><td colspan="4">This is an e-ticket. You must take your debit/credit card on the night to gain entry.</td>
</tr>
<tr id="TicketRunDeliveryDate" runat="server" style="background-color:transparent;" visible='<%# CurrentTicketRunDeliveryType != TicketRun.DeliveryMethodType.E_Ticket %>'>
	<td /><td colspan="6">Delivery date: around <%# CurrentDeliveryDate == null ? "" : CurrentDeliveryDate.Value.ToLongDateString()%></td>
</tr>

<tr id="TicketContactEmailRow" runat="server" style="padding-top:0px;border-left:solid 1px #CBA21E;" valign="top" visible="<%#TicketRunHasContactEmail%>">
	<td></td>
	<td colspan="6">Support email: <a href="mailto:<%#CurrentTicket.ExtraSelectElements["ContactEmail"]%>"><%#CurrentTicket.ExtraSelectElements["ContactEmail"]%></a></td>
</tr>
