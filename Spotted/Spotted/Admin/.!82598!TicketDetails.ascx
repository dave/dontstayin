<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TicketDetails.ascx.cs" Inherits="Spotted.Admin.TicketDetails" %>
<script language="JavaScript">
  function TicketDetailsToggleChargeToPromoter()
  {
	var chargePromoterCheckBox = document.getElementById("<%= ChargePromoterForRefundCheckBox.ClientID %>");
	var chargePromoterTextBox = document.getElementById("<%= ChargePromoterAmountTextBox.ClientID %>");
	
	if(chargePromoterCheckBox != null && chargePromoterTextBox != null)
		chargePromoterTextBox.style.display = chargePromoterCheckBox.checked?'':'none';
  }
</script>
<asp:Panel ID="TicketDetailsPanel" runat="server">
	<div class="ContentBorder">
		<table cellpadding="3" cellspacing="0" border="0">
			<tr id="CancelledRow" runat="server" visible="false" valign="top">
				<td style="color:Red; font-size:medium;" colspan="2"><strong>CANCELLED</strong></td>
			</tr>
			<tr valign="top">
				<td><nobr>K</nobr></td>
				<td><asp:Label ID="TicketKLabel" runat="server"></asp:Label></td>
			</tr>
			<tr valign="top">
				<td><nobr>User</nobr></td>
				<td><asp:Label ID="UserNickNameLabel" runat="server"></asp:Label></td>
			</tr>
			<tr valign="top">
				<td><nobr>Full name</nobr></td>
				<td><asp:Label ID="FullNameLabel" runat="server"></asp:Label></td>
			</tr>
			<tr valign="top">
				<td><nobr>Purchase date</nobr></td>
				<td><asp:Label ID="PurchaseDateLabel" runat="server"></asp:Label></td>
			</tr>
			<tr valign="top">
				<td><nobr>Event</nobr></td>
				<td><asp:Label ID="EventLabel" runat="server"></asp:Label></td>
			</tr>
			<tr valign="top">
				<td><nobr>Ticket run</nobr></td>
				<td><asp:Label ID="TicketRunLabel" runat="server"></asp:Label></td>
			</tr>
			<tr valign="top">
				<td><nobr># of tickets</nobr></td>
				<td><asp:Label ID="QuantityLabel" runat="server"></asp:Label></td>
			</tr>
			<tr valign="top">
				<td><nobr>Price</nobr></td>
				<td><asp:Label ID="PriceLabel" runat="server"></asp:Label></td>
			</tr>
			<tr valign="top">
				<td><nobr>Booking fee</nobr></td>
				<td><asp:Label ID="BookingFeeLabel" runat="server"></asp:Label></td>
			</tr>
			<tr valign="top">
				<td><nobr>Invoice</nobr></td>
				<td><asp:Label ID="InvoiceLabel" runat="server"></asp:Label></td>
			</tr>
			<tr valign="top">
				<td><nobr>Card end digits</nobr></td>
				<td><asp:Label ID="CardNumberEndLabel" runat="server"></asp:Label></td>
			</tr>
			<tr valign="top">
				<td><nobr>Code</nobr></td>
				<td><asp:Label ID="CodeLabel" runat="server"></asp:Label></td>
			</tr>
			<tr valign="top">
				<td><nobr>Address</nobr></td>
				<td><asp:Label ID="AddressLabel" runat="server"></asp:Label></td>
			</tr>
			<tr>
				<td><nobr>Feedback</nobr></td>
				<td><asp:Label ID="FeedbackLabel" runat="server"></asp:Label></td>
			</tr>
			<tr id="FeedbackNoteRow" runat="server" visible="false" valign="top">
				<td><nobr>Feedback note</nobr></td>
				<td><asp:Label ID="FeedbackNoteLabel" runat="server"></asp:Label></td>
			</tr>
			<tr>
				<td></td>
				<td><asp:Button id="RefundButton" runat="server" Text="Refund" OnClick="RefundButton_Click" OnClientClick="return confirm('Are you sure you want to refund this ticket?');"></asp:Button>&nbsp;
					&nbsp;<asp:Button id="RefundFullButton" runat="server" Text="Refund with Booking Fee" OnClick="RefundFullButton_Click" OnClientClick="return confirm('Are you sure you want to refund this ticket and booking fee?');"></asp:Button>&nbsp;
