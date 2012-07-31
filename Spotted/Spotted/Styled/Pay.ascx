<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Pay.ascx.cs" Inherits="Spotted.Styled.Pay" %>
<%@ Register TagPrefix="Controls" TagName="Payment" Src="/Controls/Payment.ascx" %>
<h2 class="EventText" id="EventHeader" runat="server"></h2>
<hr />
<div class="InnerDiv">
	<p>
		<div class="TicketDetails" id="TicketDetails" runat="server">You've selected 2 x £14 Super special early bird saver ticket</div>
		<style>
			table.PaymentTable, .thickbox, .paymentInput, .cancelButton
			{
				font-size: 8pt;
				font-weight:normal;

				text-decoration:none;
				font-style:normal;
				font-variant:normal;
				direction:ltr;	
			}
			table.PaymentTable, .paymentInput
			{
				text-align:left;
			}
			tr.dataGridHeader td
			{
				border-bottom: solid 1px black;
			}
		</style>
		<Controls:Payment Runat="server" id="Payment" OnPaymentDone="PaymentReceived"/>
		<asp:Label ID="ErrorMessageLabel" runat="server" Visible="false" style="font-weight:bold; color:Red;"></asp:Label>
		<div class="cancelButton"><button id="CancelTicketPaymentButton" runat="server" CausesValidation="false" onserverclick="CancelTicketPaymentButton_Click" class="cancelButton"><- Cancel</button></div>
	</p>
	<asp:Panel ID="MyTicketsPanel" runat="server" Visible="false">
		<div>
			<p>
				<br />
				<b>NOTE! You have already purchased the following tickets for this event</b>
				<br />
				<br />
				<asp:Repeater ID="MyTicketsRepeater" runat="server" EnableViewState="true" OnItemDataBound="MyTicketsRepeater_ItemDataBound">
					<HeaderTemplate>
						<table cellpadding="3" cellspacing="0" border="0" width="100%">
							<tr id="TicketRunHeaderRow" runat="server" valign="top" class="dataGridHeader">
								<td>#</td>
								<td colspan="2">Ticket</td>
								<td align="center" id="CodeHeader" runat="server">&nbsp;</td>
								<td align="right"><nobr>Card digits</nobr></td>
								<td align="center">Print</td>
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
</div>
