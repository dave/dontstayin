<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SetupPayment.ascx.cs" Inherits="Spotted.Controls.SetupPayment" %>
<script language="javascript">
function PaymentTogglePayNowOrLater()
{
	document.getElementById("<%=  BankTransferPaymentInfoRow.ClientID  %>").style.display = document.getElementById("<%=  PayOptionRadioButtonSetupChequePayment.ClientID  %>").checked?'none':'';
	document.getElementById("<%=  ChequePaymentInfoRow.ClientID  %>").style.display = document.getElementById("<%=  PayOptionRadioButtonSetupChequePayment.ClientID  %>").checked?'':'none';
	document.getElementById("ConfirmRow").style.display = '';	
}
</script>
<div style="border:1px solid #CBA21E; background-color:#<%= BackgroundColour %>; padding:2px; margin:3px; width:390px; " class="PaymentDiv" onload="PaymentTogglePayNowOrLater();">
	<a name="<%= this.ClientID %>_SetupPaymentAnchor"></a>
	<asp:Panel ID="PromoterItemListPanel" runat="server">
		<table cellpadding="0" cellspacing="0" Width="100%">
			<tr>
				<td align="left" style="background-color:transparent;">Invoice</td>
				<td align="right" style="background-color:transparent;">
					<asp:Label ID="PromoterItemListColumn2HeaderLabel" runat="server" Text="Total"></asp:Label></td>
				<td align="right" style="background-color:transparent;"><asp:Label ID="PromoterItemListColumn3HeaderLabel" runat="server" Text="Due"></asp:Label></td>
			</tr>
			<tbody runat="server" id="InvoicesBody"/>
			<tr runat="server" id="InvoiceTotalRow">
				<td style="background-color:transparent;" colspan="2" align="right" valign="top"><b>Total</b></td>
				<td style="background-color:transparent;" align="right" valign="top"><asp:Label ID="InvoiceTotalLabel" runat="server" Font-Bold="true" Text=""></asp:Label></td>
			</tr>
        </table>
	</asp:Panel>

	<asp:Panel ID="PayOptionsPanel" runat="server">
		<table id="PaymentOptionsHeaderTable" runat="server" cellpadding="0" cellspacing="0" width="100%">
			<tr>
				<td style="background-color:transparent;border-top:dotted 1px #CBA21E;" width="100%">
					Payment options
				</td>
			</tr>
		</table>
		<table width="100%" border="0" cellspacing="0" cellpadding="0">
			<tr>
				<td style="background-color:transparent;" valign="top">
					<asp:RadioButton runat="server" ID="PayOptionRadioButtonSetupChequePayment" GroupName="PayOptionRadioButton" onselect="PaymentTogglePayNowOrLater();" onclick="PaymentTogglePayNowOrLater();" Text="Setup cheque payment" />
				</td>
			</tr>
			<tr>
				<td style="background-color:transparent;" valign="top">
					<asp:RadioButton runat="server" ID="PayOptionRadioButtonSetupBankTransferPayment" GroupName="PayOptionRadioButton" onselect="PaymentTogglePayNowOrLater();" onclick="PaymentTogglePayNowOrLater();" Text="Setup bank transfer payment" />
				</td>
			</tr>
			<tr id="ChequePaymentInfoRow" runat="server" style="display: none; height:200px;">
				<td>This will setup a cheque payment in our system to allow for faster processing when your cheque clears.<br /><br />
					<u><b>Please make the cheque payable to</b></u><br />
					<table cellpadding="0" cellspacing="0">
						<tr>
							<td><nobr>Development Hell Limited</nobr></td>
						</tr>
						<tr>
							<td><nobr>Greenhill House, Thorpe Road</nobr></td>
						</tr>
						<tr>
							<td><nobr>Peterborough</nobr></td>
						</tr>
						<tr>
							<td><nobr>PE3 6RU</nobr></td>
						</tr>					
					</table>
					<br />
					<asp:Label runat="server" ID="ChequeClickConfirmLabel" Text="Please click 'Confirm' now. You will be given a reference number. "></asp:Label>Please write the reference number on your cheque.<br />
					<small>Please note that your invoices will remain marked as unpaid until your cheque clears.</small>
				</td>
			</tr>
			<tr id="BankTransferPaymentInfoRow" runat="server" style="display: none; height:200px;">
				<td>This will setup a bank transfer payment in our system to allow for faster processing when your bank transfer clears.<br /><br />
					<u><b>Bank transfer details</b></u><br />
					<table cellpadding="0" cellspacing="0">
						<tr>
							<td><nobr>Bank name:</nobr></td>
							<td><nobr>Barclays Bank PLC</nobr></td>
						</tr>
						<tr>
							<td><nobr>Branch name:</nobr></td>
							<td><nobr>Commercial Bank Basingstoke</nobr></td>
						</tr>
						<tr>
							<td><nobr>Sort Code:</nobr></td>
							<td><nobr>20-37-63</nobr></td>
						</tr>
						<tr>
							<td><nobr>Account #:</nobr></td>
							<td>00478377</td>
						</tr>
						<tr>
							<td colspan="2" style="padding-left:10px;">(IBAN for international transfers: GB04 BARC 2037 6300 4783 77)</td>
						</tr>
					</table>
					<br />
					<asp:Label runat="server" ID="TransferClickConfirmLabel" Text="Please click 'Confirm' now. You will be given a reference number. "></asp:Label>Please add the reference number to your bank transfer.<br />
					<small>Please note that your invoices will remain marked as unpaid until your bank transfer clears.</small>
				</td>
			</tr>
			<tr id="ConfirmRow" style="display: none;">
				<td style="background-color:transparent; padding-left:3px;" valign="top">
					<asp:Button runat="server" ID="ConfirmButton" Text="Confirm" OnClick="ConfirmButton_Click" />
				</td>
			</tr>
			<tr id="ReferenceNumberRow" runat="server" visible="false">
				<td style="background-color:transparent;" valign="top"><font size="+1"><asp:Label ID="ReferenceNumberLabel" runat="server" Text="Your reference number is " Font-Bold="true"></asp:Label><asp:Label ID="ReferenceNumberValueLabel" runat="server" Font-Bold="true"></asp:Label></font></td>
			</tr>
        </table>
	</asp:Panel>
</div>
