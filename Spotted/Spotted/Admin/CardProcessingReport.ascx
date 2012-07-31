<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CardProcessingReport.ascx.cs" Inherits="Spotted.Admin.CardProcessingReport" %>
<%@ Register TagPrefix="DbCombo" Namespace="Cambro.Web.DbCombo" Assembly="Cambro.Web.DbCombo" %>
<dsi:h1 runat="server" id="H1">Cardnet Processing Report</dsi:h1>
<asp:Panel ID="CardnetProcessingPanel" runat="server">
	<div class="ContentBorder">
		<p>
			<table id="CardnetProcessingSearchCriteriaTable" runat="server" cellpadding="3" cellspacing="0" border="0">
				<tr>
					<td>From date:</td>
					<td>
						<dsi:Cal ID="FromDateCal" runat="server" TabIndex="18"></dsi:Cal>
					</td>
				</tr>
				<tr>
					<td>To date:</td>
					<td>
						<dsi:Cal ID="ToDateCal" runat="server" TabIndex="18"></dsi:Cal>
					</td>
					<td><button id="SearchButton" runat="server" onserverclick="SearchButton_Click">Search</button></td>
				</tr>
			</table>
			<table cellpadding="3" cellspacing="0" border="0">
				<tr>
					<td colspan="2" valign="top"><b>To DSI Bank Account</b>
						<asp:GridView ID="CardnetAccountGridView" runat="server" CssClass="dataGrid" AutoGenerateColumns="false" EnableViewState="true" ShowHeader="true" AllowPaging="true" PageSize="40"
							AlternatingRowStyle-CssClass="dataGridAltItem" GridLines="None" BorderWidth="0" CellPadding="3" HeaderStyle-CssClass="dataGridHeader" EmptyDataText="* Zero results" EmptyDataRowStyle-Font-Italic="true"
							SelectedRowStyle-CssClass="dataGridSelectedItem" AlternatingRowStyle-VerticalAlign="Top" RowStyle-VerticalAlign="Top" OnPageIndexChanging="CardnetAccountGridView_PageIndexChanging">
							<Columns>
								<asp:TemplateField HeaderText="<nobr>Purchase date</nobr>" ItemStyle-HorizontalAlign="Right">
									<ItemTemplate>
										<%# LinkAdmin((Bobs.Transfer)Container.DataItem)%>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="<nobr>Transfers</nobr>" ItemStyle-HorizontalAlign="Right">
									<ItemTemplate>
										<%#((Bobs.Transfer)(Container.DataItem)).ExtraSelectElements["CountTransfers"]%>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Right">
									<ItemTemplate>
										<%#Utilities.MoneyToHTML(((decimal)((Bobs.Transfer)(Container.DataItem)).ExtraSelectElements["SumAmount"]))%>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField HeaderText="<nobr>Process date</nobr>" ItemStyle-HorizontalAlign="Right">
									<ItemTemplate>
										<%#Utilities.CardnetWait(((DateTime)((Bobs.Transfer)(Container.DataItem)).ExtraSelectElements["Date"])).ToString("ddd dd/MM/yy")%>
									</ItemTemplate>
								</asp:TemplateField>
							</Columns>
						</asp:GridView>
					</td>
					
				</tr>
				<tr style="font-weight:bold;">
					<td>Sum amount:</td>
					<td><asp:Label ID="SumAccountLabel" runat="server" Text="£0.00"></asp:Label></td>
				</tr>
				<tr style="font-weight:bold;">
					<td>Transfer count:</td>
					<td><asp:Label ID="SumTransferCountLabel" runat="server" Text="£0.00"></asp:Label></td>
				</tr>

			</table>			
		</p>
	</div>
</asp:Panel>
