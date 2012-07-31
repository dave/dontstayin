<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BuyTickets.ascx.cs" Inherits="Spotted.Pages.Events.BuyTickets" %>
<%@ Register TagPrefix="Headers" TagName="Event" Src="/Controls/Headers/EventHeader.ascx" %>
<%@ Register TagPrefix="Controls" TagName="Payment" Src="/Controls/Payment.ascx" %>
<asp:Panel Runat="server" ID="EventSelectedPanel">
	<Headers:Event runat="server" SuppressLink="true">
		<asp:Panel Runat="server" ID="PromoterPanel">
			<p>
				<small>
					Do you promote or organise this event? <a href="/pages/promoters/intro">Sign up as a promoter</a>
					and our team will help you get the most out of DontStayIn.
				</small>
			</p>
		</asp:Panel>
	</Headers:Event>
	<asp:Panel runat="server" ID="PayForTicketsPanel">
		<dsi:h1 runat="Server" id="PayForTicketsHeading">Pay for tickets</dsi:h1>
		<div class="ContentBorder">
			<p>
				<Controls:Payment Runat="server" id="Payment" OnPaymentDone="PaymentReceived"/>
				<asp:Label ID="ErrorMessageLabel" runat="server" Visible="false" style="font-weight:bold; color:Red;"></asp:Label>
				<button id="CancelTicketPaymentButton" runat="server" CausesValidation="false" onserverclick="CancelTicketPaymentButton_Click"><- Cancel</button>
			</p>
		</div>
	</asp:Panel>
	<asp:Panel ID="MyTicketsPanel" runat="server" Visible="true">
		<dsi:h1 runat="Server" id="H1">My tickets</dsi:h1>
		<div class="ContentBorder">
			<p>
				<b>NOTE! You have already purchased the following tickets for this event</b>
				<br />
				<br />
				<asp:Repeater ID="MyTicketsRepeater" runat="server" EnableViewState="true" OnItemDataBound="MyTicketsRepeater_ItemDataBound">
					<HeaderTemplate>
						<table cellpadding="3" cellspacing="0" border="0" width="100%">
							<tr id="TicketRunHeaderRow" runat="server" valign="top" class="dataGridHeader">
								<td>#</td>
								<td colspan="2">Ticket</td>
								<td align="center" id="CodeHeader" runat="server"></td>
								<td align="right"><nobr>Card digits</nobr></td>
								<td align="center"><small>Print</small></td>
								<td>&nbsp;</td>
							</tr>
					</HeaderTemplate>
					
					<FooterTemplate>
						</table>
					</FooterTemplate>
				</asp:Repeater>
			</p>
		</div>
	</asp:Panel>
</asp:Panel>
