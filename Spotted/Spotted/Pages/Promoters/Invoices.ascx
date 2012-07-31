<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Invoices.ascx.cs" Inherits="Spotted.Pages.Promoters.Invoices" %>
<%@ Register TagPrefix="Controls" TagName="Payment" Src="/Controls/Payment.ascx" %>
<%@ Register TagPrefix="ControlsSetup" TagName="SetupPayment" Src="/Controls/SetupPayment.ascx" %>

<!--
<dsi:PromoterIntro runat="server" ID="PromoterIntro" Header="Invoices">
		<p>
		Below are listed your invoices and credits.<br />To view a specific date range, please select the month and year and click "View Summary".
	</p>
</dsi:PromoterIntro>
<dsi:h1 runat="server" ID="H11">Your invoices and credits</dsi:h1>
-->
<script>
function InvoicesTogglePlusMinus(subRowIDs, id)
{
	var elem;
	var img;
	
	var arry = subRowIDs.split(','); 
	img = document.getElementById('showHideImage' + id);

	for(var i=0; i<arry.length; i++)
	{
		elem = document.getElementById(arry[i]);
		if(elem)
		{			
			elem.style.display = elem.style.display == 'none' ? '' : 'none';
			img.src = elem.style.display == 'none' ? "/gfx/plus.gif" : "/gfx/minus.gif";
		}
	}
}
</script>
<dsi:PromoterIntro runat="server" ID="PromoterIntro1" Header="Account">
    <p>Below are listed your account details:</p>
</dsi:PromoterIntro>
	
<asp:Panel ID="MainPanel" runat="server">
	<dsi:h1 runat="server" ID="H1Header">Your account details</dsi:h1>
	<div class="ContentBorder">    
		<table cellpadding="3">
			<tr>
				<td style="width: 80px"><asp:Label ID="BalanceLabel" runat="server" Text="Current&nbsp;balance"></asp:Label>&nbsp;</td>
				<td style="text-align:right"><asp:Label ID="BalanceValueLabel" runat="server" Text="£0.00" Font-Bold="True" Font-Italic="False"></asp:Label>&nbsp;</td>
				<td style="text-align:left">&nbsp;<asp:Label ID="OutstandingBalanceLabel" runat="server" Text=" You have an outstanding balance. Please pay your balance now." Font-Bold="True"></asp:Label></td>
			</tr>
			<tr id="CreditLimitTR" runat="server">
				<td style="width: 80px"><asp:Label ID="CreditLimitLabel" runat="server" Text="Credit&nbsp;limit"></asp:Label>&nbsp;</td>
				<td style="text-align:right"><asp:Label ID="CreditLimitValueLabel" runat="server" Font-Bold="True" Text="£0.00"></asp:Label></td>
			</tr>
			<tr id="FundsAvailableTR" runat="server">
				<td style="width: 80px"><asp:Label ID="FundsAvailableLabel" runat="server" Text="Funds&nbsp;available"></asp:Label>&nbsp;</td>
				<td style="text-align:right"><asp:Label ID="FundsAvailableValueLabel" runat="server" Font-Bold="True" Text="£0.00"></asp:Label></td>
			</tr>
			<tr id="TicketFundsTR" runat="server" visible="false">
				<td style="text-align:left">Ticket funds&nbsp;</td>
				<td style="text-align:right"><asp:Label ID="TicketFundsValueLabel" runat="server" Font-Bold="True" Text="£0.00"></asp:Label></td>
			</tr>
		</table>
		<asp:Panel ID="HeaderPanel" runat="server">
			<table width="575">
				<tr>
					<td></td>
					<td><small>Month</small></td>
					<td><small>Year</small></td>
					<td></td>
				</tr>
				<tr>
					<td valign="top"><asp:Label ID="ViewSummaryLabel" runat="server" Text="View summary:"></asp:Label></td>
					<td valign="top"><asp:DropDownList ID="MonthDropDownList" runat="server">
						<asp:ListItem Value=""></asp:ListItem>
						<asp:ListItem Value="1">January</asp:ListItem>
						<asp:ListItem Value="2">February</asp:ListItem>
						<asp:ListItem Value="3">March</asp:ListItem>
						<asp:ListItem Value="4">April</asp:ListItem>
						<asp:ListItem Value="5">May</asp:ListItem>
						<asp:ListItem Value="6">June</asp:ListItem>
						<asp:ListItem Value="7">July</asp:ListItem>
						<asp:ListItem Value="8">August</asp:ListItem>
						<asp:ListItem Value="9">September</asp:ListItem>
						<asp:ListItem Value="10">October</asp:ListItem>
						<asp:ListItem Value="11">November</asp:ListItem>
						<asp:ListItem Value="12">December</asp:ListItem>
					</asp:DropDownList></td>
					<td valign="top"><asp:DropDownList ID="YearDropDownList" runat="server"></asp:DropDownList></td>
					<td colspan="2" valign="top"><asp:Button ID="ViewSummaryButton" runat="server" Text="View summary" Font-Bold="False" OnClick="ViewSummaryButton_Click" Width="140px" />
						&nbsp;&nbsp;&nbsp;&nbsp;<asp:HyperLink ID="ViewStatementHyperLink" runat="server" Target="_blank">View statement</asp:HyperLink>
						<asp:CustomValidator ID="ViewSummaryCustomValidator" runat="server" Display="Dynamic"
							ErrorMessage="<p>* If month is selected, then year must be selected</p>"></asp:CustomValidator></td>
				</tr>
			</table>
		</asp:Panel>
    </div>
	<asp:Panel ID="SummaryPanel" runat="server">
		<dsi:h1 runat="server" ID="H1Summmary"><asp:Label ID="SummaryHeaderLabel" runat="server" Text="Invoices"></asp:Label></dsi:h1>
		<div class="ContentBorder">
			<table width="500" cellspacing="0" cellpadding="0">
				<tr>
					<td><asp:Button ID="PayOutstandingButton" runat="server" Text="Pay Outstanding Invoices" Font-Bold="False" OnClick="PayOutstandingButton_Click" Width="175"/><asp:Button
						ID="PayNowButton" runat="server" Font-Bold="False" Text="Make Card Payment" Visible="False" OnClick="PayNowButton_Click" /></td>
					<td><asp:Button ID="SetupTransferButton" runat="server" Font-Bold="False" Text="Make Other Payment" Visible="False" OnClick="SetupTransferButton_Click"  Width="175"/></td>
					<td colspan="2" align="right">
						<asp:Label ID="FilterLabel" runat="server" Text="Filter: "></asp:Label><asp:DropDownList
							ID="FilterDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="FilterDropDownList_SelectedIndexChanged"></asp:DropDownList></td>    
				</tr>
				<tr>
					<td colspan="4">
						<asp:Repeater ID="PromoterAccountItemRepeater" runat="server" OnItemDataBound="PromoterAccountItemRepeater_ItemDataBound" EnableViewState="true">
							<HeaderTemplate>
								<table width="582" border="0" cellspacing="0" cellpadding="3" class="dataGrid">
								<tr class="dataGridHeader">
									<th align="left" width="140">Ref #</th>
									<!-- Renaming of "Date" to "Created", as per Dave's request 7/2/07 -->
									<th align="left">Created</th>
									<th align="left">Total</th>
									<th align="left">Outstanding</th>
									<th align="left">Status</th>
									<th align="left">View</th>
									<th id="AdminEditTH" runat="server" align="left" visible="false">Edit</th>
								</tr>
							</HeaderTemplate>
							<ItemTemplate>
								<tr id="PromoterAccountItemRow" runat="server" class="dataGridItem">
									<td><asp:CheckBox ID="OutstandingCheckBox" runat="server" /><asp:Label ID="InvoiceKLabel" runat="server" Text='<%# ((PromoterAccountItem)Container.DataItem).K.ToString()%>' Visible="false"></asp:Label><asp:Literal ID="PromoterAccountItemLink" runat="server"></asp:Literal></td>
									<td align="left"><%# Utilities.DateToString(((PromoterAccountItem)Container.DataItem).Date)%></td>
									<td align="right"><%# ((PromoterAccountItem)Container.DataItem).Total%></td>
									<td align="right"><%# ((PromoterAccountItem)Container.DataItem).Outstanding%></td>
									<td><%# ((PromoterAccountItem)Container.DataItem).Status%></td>
									<td><%# ((PromoterAccountItem)Container.DataItem).ViewLink %></td>
									<td id="AdminEditTD" runat="server" visible="false"><small><%# ((PromoterAccountItem)Container.DataItem).EditLink %></small></td>
								</tr>
								<asp:Repeater ID="PromoterAccountSubItemRepeater" runat="server" OnItemDataBound="PromoterAccountSubItemRepeater_ItemDataBound">
									<ItemTemplate>
										<tr id="PromoterAccountSubItemRow" runat="server" class="dataGridItem" style="display:none;" >
											<td style="padding-left: 16px;"><small><%# Utilities.CamelCaseToString(((PromoterAccountItem)Container.DataItem).OriginalType.ToString())%> #<%# ((PromoterAccountItem)Container.DataItem).K.ToString()%></small></td>
											<td align="left"><small><%# Utilities.DateToString(((PromoterAccountItem)Container.DataItem).Date)%></small></td>
											<td align="right"><small><%# ((PromoterAccountItem)Container.DataItem).Total%></small></td>
											<td align="right"><small>&nbsp;</small></td>
											<td><small><%# ((PromoterAccountItem)Container.DataItem).Status%></small></td>
											<td><small><%# ((PromoterAccountItem)Container.DataItem).ViewLink %></small></td>
											<td id="AdminEditTD" runat="server" visible="false"><small><%# ((PromoterAccountItem)Container.DataItem).EditLink %></small></td>
										</tr>
									</ItemTemplate>
								</asp:Repeater>
								<tr class="dataGridItem" id="NoSubItemRow" style="display:none;" runat="server">
									<td style="padding-left: 16px;">No items</td>
									<td>&nbsp;</td>
									<td>&nbsp;</td>
									<td>&nbsp;</td>
									<td>&nbsp;</td>
									<td>&nbsp;</td>
									<td>&nbsp;</td>
								</tr>
							</ItemTemplate>
							<FooterTemplate>
								</table>
							</FooterTemplate>
						</asp:Repeater>
						<table width="575">
							<tr>
								<td colspan="4" align="right">
									<asp:Panel ID="PaginationPanel" runat="server">
										<asp:LinkButton ID="PrevPageLinkButton" runat="server" OnClick="PrevPageLinkButton_Click"><img src="/gfx/icon-back-12.png" alt="Prev page" style="margin-right:3px;" width="12" height="21" align="absmiddle" border="0">prev page</asp:LinkButton> ...
										<asp:LinkButton ID="NextPageLinkButton" runat="server" OnClick="NextPageLinkButton_Click">next page<img src="/gfx/icon-forward-12.png" alt="Next page" style="margin-left:3px;" width="12" height="21" align="absmiddle" border="0"></asp:LinkButton>
									</asp:Panel>
								</td>
							</tr>
						</table>						
					</td>
				</tr>
			</table>
			<asp:Panel runat="server" ID="AdminPanel" Visible="false">
				<p>
					<small><asp:Label runat="server" ID="AdminInvoiceLinkLabel"></asp:Label>&nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="AdminTransferLinkLabel"></asp:Label></small>
				</p>
			</asp:Panel>
		</div>
	</asp:Panel>

	<asp:Panel Runat="server" ID="PaymentPanel" Visible="False">
		<dsi:h1 runat="server" ID="H13" NAME="H11">Make Payment</dsi:h1>
		<div class="ContentBorder">
			<p>
				<Controls:Payment id="Payment" Runat="server" BackgroundColour="FED551" OnPaymentDone="PaymentReceived" />
			</p>
			<p>
				<asp:Button Runat="server" onclick="Pay_Cancel" Text="&lt;- Cancel" ID="Button2"/>
			</p>
		</div>
	</asp:Panel>
	<asp:Panel Runat="server" ID="PaidMessagePanel" Visible="False">
		<dsi:h1 runat="server" ID="H14" NAME="H11">Payment received</dsi:h1>
		<div class="ContentBorder">
			<p align="center">
				<b>Your payment has been received.  It will be applied to all the invoices you selected.</b>
			</p>
		</div>
	</asp:Panel>
	<asp:Panel Runat="server" ID="SetupTransferPanel" Visible="False">
		<dsi:h1 runat="server" ID="H15" NAME="H15">Setup Transfer</dsi:h1>
		<div class="ContentBorder">
			<p><ControlsSetup:SetupPayment id="SetupPayment" Runat="server" BackgroundColour="FED551" /></p>
		</div>
	</asp:Panel>
</asp:Panel>