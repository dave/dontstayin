<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminMainAccounting.ascx.cs" Inherits="Spotted.Admin.AdminMainAccounting" %>
<%@ Register TagPrefix="DbCombo" Namespace="Cambro.Web.DbCombo" Assembly="Cambro.Web.DbCombo" %>

<dsi:h1 runat="server" id="H1">Admin accounting</dsi:h1>
<div class="ContentBorder">
	<asp:Panel ID="AdminMainAccountingPanel" runat="server">
		<table id="TABLE1" width="600">
			<tr>
				<td align="center" colspan="4">
					<p>
						<button ID="CreateNewInvoiceButton" runat="server" style="width:185px; margin-right:10px" TabIndex="1" onserverclick="CreateNewInvoiceButton_Click">Create New Invoice</button>
						<button ID="CreateNewTransferButton" runat="server" style="width:185px; margin-right:10px" TabIndex="2" onserverclick="CreateNewTransferButton_Click">Create New Transfer</button>						
						<button ID="SubmitSuccessfulTransfersButton" runat="server" style="width:185px;" TabIndex="4" onserverclick="SubmitSuccessfulTransfersButton_Click">Submit Successful Transfers</button>
					</p>
					<p>
						<button ID="CreateNewCampaignCreditButton" runat="server" style="width:185px; margin-right:10px" TabIndex="3" onserverclick="CreateNewCampaignCreditButton_Click">Create New Campaign Credit</button>&nbsp;&nbsp;&nbsp;
						<button ID="CreateNewInsertionOrder" runat="server" style="width:185px;" TabIndex="3" onserverclick="CreateNewInsertionOrderButton_Click">Create New Insertion Order</button>						
					</p>
					<table>
						<tr><td colspan="4"><h2>Export To Sage</h2></td></tr>
						<tr>
							<td><asp:Label ID="SageFromDateLabel" runat="server" Text="From"></asp:Label></td>
							<td><dsi:Cal id="SageFromDateCal" runat="server"></dsi:Cal></td>
							<td><asp:DropDownList ID="ExportToSageTypeDropDownList" runat="server" Width="120px" TabIndex="9">
								</asp:DropDownList></td>
							<td>
								<button ID="ExportToSageButton" runat="server" style="width:76px;" TabIndex="10" onserverclick="ExportToSageButton_Click">Export</button>
							</td>
						</tr>
						<tr>
							<td><asp:Label ID="SageToDateLabel" runat="server" Text="To"></asp:Label></td>
							<td><dsi:Cal id="SageToDateCal" runat="server"></dsi:Cal></td><td colspan="2">
								<asp:Label ID="SageErrorLabel" runat="server" Font-Bold="True" Font-Italic="True"
									ForeColor="Red" Visible="False"></asp:Label></td>
						</tr>
					</table>
					<p>
						<small>("ALL" excludes sales receipts and campaign credits)</small>
					</p>
				</td>
			</tr>
			<tr>
				<td colspan="4"><h2>Search Invoices / Transfers / Campaign Credits / Insertion Orders</h2></td>
			</tr>
			<tr>
				<td style="width: 168px"><asp:Label ID="TypeLabel" runat="server" Text="Type"></asp:Label></td>
				<td style="width: 262px"><asp:DropDownList ID="TypeDropDownList" runat="server" Width="167px" OnSelectedIndexChanged="TypeDropDownList_SelectedIndexChanged" AutoPostBack="True" TabIndex="11"></asp:DropDownList></td>
				<td style="width: 437px;">
					<asp:Label ID="NominalCodeLabel" runat="server" Text="<nobr>Nominal code</nobr>" Visible="False"></asp:Label>
					<asp:Label ID="TransferTypeLabel" runat="server" Text="Transfer&nbsp;type" Visible="False"></asp:Label>
					<asp:Label ID="InvoiceTypeLabel" runat="server" Text="Invoice&nbsp;type" Visible="True"></asp:Label>
				</td>
				<td style="width: 355px;">
					<asp:TextBox ID="NominalCodeTextBox" runat="server" Width="80px" Visible="False" TabIndex="17"></asp:TextBox>
					<asp:DropDownList ID="TransferTypeDropDownList" runat="server" Width="140px" TabIndex="18" Visible="False"></asp:DropDownList>
					<asp:DropDownList ID="InvoiceTypeDropDownList" runat="server" Width="140px" TabIndex="18" Visible="True"></asp:DropDownList>
					<asp:DropDownList ID="TransferCompanyDropDownList" runat="server" Width="75px" TabIndex="18" Visible="False"><asp:ListItem Text="" Value=""/><asp:ListItem Text="Unknown" Value="0"/><asp:ListItem Text="DSI" Value="1"/><asp:ListItem Text="DH" Value="2"/></asp:DropDownList>
				</td>
			</tr>
			<tr>
				<td style="width: 168px;"><asp:Label ID="KNumberLabel" runat="server" Text="K #"></asp:Label></td>
				<td style="width: 262px;"><asp:TextBox ID="KNumberTextBox" runat="server" Width="80px" TabIndex="12"></asp:TextBox></td>
				<td style="width: 437px;"><asp:Label ID="StatusLabel" runat="server" Text="Status"></asp:Label></td>
				<td style="width: 305px;"><asp:DropDownList ID="StatusDropDownList" runat="server" Width="140px" TabIndex="18">
					</asp:DropDownList></td>
			</tr>
			<tr>
				<td style="width: 168px;"><asp:Label ID="PromoterLabel" runat="server" Text="Promoter"></asp:Label><asp:Label ID="InvoiceItemTypeLabel" runat="server" Text="Type" Visible="false"></asp:Label></td>
				<td style="width: 262px;"><js:HtmlAutoComplete Width="168px" ID="uiPromotersAutoComplete" runat="server"  WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetPromotersWithK"/><asp:DropDownList ID="InvoiceItemTypeDropDownList" runat="server" TabIndex="14" Visible="False"/></td>
				<td style="width: 437px">
					<asp:Label ID="FromDateLabel" runat="server" Text="<nobr>From date</nobr>"></asp:Label></td>
				<td style="width: 305px"><dsi:Cal id="FromDateCal" runat="server" TabIndex="18"></dsi:Cal></td>
			</tr>
			<tr>
				<td style="width: 168px;">
					<asp:Label ID="UserLabel" runat="server" Text="User"></asp:Label></td>
				<td style="width: 262px;"><js:HtmlAutoComplete Width="168px" ID="uiUserAutoComplete" runat="server" WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetUsersWithK"/>
				</td>
				<td style="width: 437px;">
					<asp:Label ID="ToDateLabel" runat="server" Text="<nobr>To date</nobr>"></asp:Label></td>
				<td style="width: 305px;"><dsi:Cal id="ToDateCal" runat="server" TabIndex="18"></dsi:Cal></td>
			</tr>
			<tr><td style="width: 168px;">
					<asp:Label ID="SalesUserLabel" runat="server" Text="<nobr>Sales user</nobr>"></asp:Label><asp:Label ID="TransferMethodLabel" runat="server" Text="Method" Visible="False"></asp:Label></td>
				<td style="width: 262px;">
					<asp:DropDownList ID="SalesUserDropDownList" runat="server" Width="167px" TabIndex="14">
					</asp:DropDownList><asp:DropDownList ID="TransferMethodDropDownList" runat="server" Width="167px" TabIndex="14" Visible="False">
					</asp:DropDownList></td>
				<td style="width: 437px;">
					<asp:Label ID="DateTypeLabel" runat="server" Text="<nobr>Date type</nobr>"></asp:Label></td>
				<td style="width: 305px;"><asp:DropDownList ID="DateTypeDropDownList" runat="server" Width="140px" TabIndex="18">
				</asp:DropDownList></td>
			</tr>
			<tr>
				<td><asp:Label ID="BankAccountLabel" runat="server" Text="<nobr>Bank account</nobr>" Visible="False"></asp:Label></td>
				<td><asp:DropDownList ID="BankAccountDropDownList" runat="server" Width="167px" Visible="False"></asp:DropDownList></td>
				<td></td>
				<td style="width: 305px">
					<asp:Button ID="SearchButton" runat="server" Text="Search" Width="100px" OnClick="SearchButton_Click" TabIndex="30" />&nbsp;&nbsp;<asp:Button ID="ClearButton" runat="server" Text="Clear" Width="100px" OnClick="ClearButton_Click" TabIndex="30" /></td>
			</tr>
		</table>
		<table width="800">
			<tr>
				<td><asp:Label ID="SearchResultsMessageLabel" runat="server" Font-Italic="True" Visible="False"></asp:Label>
					<asp:GridView ID="SearchResultsInvoiceGridView" runat="server" AllowPaging="True" AutoGenerateColumns="False" Visible="false" OnDataBound="SearchResultsInvoiceGridView_DataBound"
						OnPageIndexChanging="SearchResultsInvoiceGridView_PageIndexChanging" TabIndex="41" CssClass="dataGrid" BorderWidth="0" CellPadding="3"
						AlternatingRowStyle-CssClass="dataGridAltItem" RowStyle-CssClass="dataGridItem" SelectedRowStyle-CssClass="dataGridSelectedItem"  PagerStyle-HorizontalAlign="Right"
						PageSize="20" HeaderStyle-CssClass="dataGridHeader" AlternatingRowStyle-VerticalAlign="Top" RowStyle-VerticalAlign="Top">
						<Columns>
							<asp:TemplateField HeaderText="<nobr>K #</nobr>" SortExpression="K" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<%#((Bobs.Invoice)Container.DataItem).K.ToString()%>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="<nobr>Created date</nobr>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<%#Utilities.DateToString(((Bobs.Invoice)Container.DataItem).CreatedDateTime)%>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="<nobr>Paid date</nobr>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<%#Utilities.DateToString(((Bobs.Invoice)Container.DataItem).PaidDateTime)%>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="<nobr>Tax date</nobr>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<%#Utilities.DateToString(((Bobs.Invoice)Container.DataItem).TaxDateTime)%>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="<nobr>Total</nobr>" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<%#Utilities.MoneyToHTML(((Bobs.Invoice)Container.DataItem).Total)%>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="<nobr>Paid</nobr>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<%#Utilities.BooleanToYesNo(((Bobs.Invoice)Container.DataItem).Paid)%>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="<nobr>Due date</nobr>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<%#Utilities.DateToString(((Bobs.Invoice)Container.DataItem).DueDateTime)%>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="<nobr>Promoter / user</nobr>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<nobr><%# ((Bobs.Invoice)Container.DataItem).PromoterK > 0 ? ((Bobs.Invoice)Container.DataItem).Promoter.LinkNewWindow() : ((Bobs.Invoice)Container.DataItem).Usr.LinkNewWindow()%></nobr>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="<nobr>View invoice</nobr>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<nobr><%#((Bobs.Invoice)Container.DataItem).AdminLinkNewWindow()%></nobr>
								</ItemTemplate>
							</asp:TemplateField>
						</Columns>
					</asp:GridView>
					<asp:GridView ID="SearchResultsTransferGridView" runat="server" AllowPaging="True" AutoGenerateColumns="False" Visible="false"
						OnPageIndexChanging="SearchResultsTransferGridView_PageIndexChanging" TabIndex="41" CssClass="dataGrid" BorderWidth="0" CellPadding="3"
						AlternatingRowStyle-CssClass="dataGridAltItem" RowStyle-CssClass="dataGridItem" SelectedRowStyle-CssClass="dataGridSelectedItem"  PagerStyle-HorizontalAlign="Right"
						PageSize="20" HeaderStyle-CssClass="dataGridHeader" AlternatingRowStyle-VerticalAlign="Top" RowStyle-VerticalAlign="Top">
						<Columns>
							<asp:TemplateField HeaderText="<nobr>K #</nobr>" SortExpression="K" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<%#((Bobs.Transfer)Container.DataItem).K.ToString()%>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="<nobr>Created date</nobr>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<%#Utilities.DateToString(((Bobs.Transfer)Container.DataItem).DateTimeCreated)%>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="<nobr>Amount</nobr>" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<%#Utilities.MoneyToHTML(((Bobs.Transfer)Container.DataItem).Amount)%>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="<nobr>Method</nobr>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<%#Utilities.CamelCaseToString(((Bobs.Transfer)Container.DataItem).Method.ToString())%>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="<nobr>Company</nobr>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<%#Utilities.CamelCaseToString(((Bobs.Transfer)Container.DataItem).Company.ToString())%>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="<nobr>Status</nobr>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<%#((Bobs.Transfer)Container.DataItem).Status.ToString()%>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="<nobr>Completion date</nobr>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<%#Utilities.DateToString(((Bobs.Transfer)Container.DataItem).DateTimeComplete)%>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="<nobr>Promoter / user</nobr>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<nobr><%# ((Bobs.Transfer)Container.DataItem).PromoterK > 0 ? ((Bobs.Transfer)Container.DataItem).Promoter.LinkNewWindow() : ((Bobs.Transfer)Container.DataItem).Usr.LinkNewWindow()%></nobr>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="<nobr>View transfer</nobr>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<nobr><%#((Bobs.Transfer)Container.DataItem).AdminLinkNewWindow()%></nobr>
								</ItemTemplate>
							</asp:TemplateField>
						</Columns>
					</asp:GridView>
					<asp:GridView ID="SearchResultsInvoiceItemGridView" runat="server" AllowPaging="True" AutoGenerateColumns="False" Visible="false" OnDataBound="SearchResultsInvoiceItemGridView_DataBound"
						OnPageIndexChanging="SearchResultsInvoiceItemGridView_PageIndexChanging" TabIndex="41" CssClass="dataGrid" BorderWidth="0" CellPadding="3"
						AlternatingRowStyle-CssClass="dataGridAltItem" RowStyle-CssClass="dataGridItem" SelectedRowStyle-CssClass="dataGridSelectedItem"  PagerStyle-HorizontalAlign="Right"
						PageSize="20" HeaderStyle-CssClass="dataGridHeader" AlternatingRowStyle-VerticalAlign="Top" RowStyle-VerticalAlign="Top">
						<Columns>
							<asp:TemplateField HeaderText="<nobr>K #</nobr>" SortExpression="K" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<%#((Bobs.InvoiceItem)Container.DataItem).K.ToString()%>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="<nobr>Revenue start</nobr>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<%#Utilities.DateToString(((Bobs.InvoiceItem)Container.DataItem).RevenueStartDate)%>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="<nobr>Revenue end</nobr>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<%#Utilities.DateToString(((Bobs.InvoiceItem)Container.DataItem).RevenueEndDate)%>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="<nobr>Type</nobr>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<%#Utilities.CamelCaseToString(((Bobs.InvoiceItem)Container.DataItem).Type.ToString())%>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="<nobr>Total</nobr>" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<nobr><%#((Bobs.InvoiceItem)Container.DataItem).Total.ToString("c")%></nobr>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="<nobr>Description</nobr>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<%#((Bobs.InvoiceItem)Container.DataItem).Description%>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="<nobr>Promoter / user</nobr>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<nobr><%# ((Bobs.InvoiceItem)Container.DataItem).Invoice.PromoterK > 0 ? ((Bobs.InvoiceItem)Container.DataItem).Invoice.Promoter.LinkNewWindow() : ((Bobs.InvoiceItem)Container.DataItem).Invoice.Usr.LinkNewWindow()%></nobr>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="<nobr>View invoice</nobr>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<nobr><%#((Bobs.InvoiceItem)Container.DataItem).Invoice.AdminLinkNewWindow()%></nobr>
								</ItemTemplate>
							</asp:TemplateField>
						</Columns>
					</asp:GridView>
					<asp:GridView ID="SearchResultsInsertionOrderGridView" runat="server" AllowPaging="True" AutoGenerateColumns="false" Visible="false" OnDataBound="SearchResultsInsertionOrderGridView_DataBound"
						OnPageIndexChanging="SearchResultsInsertionOrderGridView_PageIndexChanging" TabIndex="41" CssClass="dataGrid" BorderWidth="0" CellPadding="3"
						AlternatingRowStyle-CssClass="dataGridAltItem" RowStyle-CssClass="dataGridItem" SelectedRowStyle-CssClass="dataGridSelectedItem"  PagerStyle-HorizontalAlign="Right"
						PageSize="20" HeaderStyle-CssClass="dataGridHeader" AlternatingRowStyle-VerticalAlign="Top" RowStyle-VerticalAlign="Top">
						<Columns>
							<asp:TemplateField HeaderText="<nobr>K #</nobr>" SortExpression="K" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<%#((Bobs.InsertionOrder)Container.DataItem).K.ToString()%>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="<nobr>Credits</nobr>" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<%#((Bobs.InsertionOrder)Container.DataItem).CampaignCredits.ToString()%>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="<nobr>Status</nobr>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<%#((Bobs.InsertionOrder)Container.DataItem).Status.ToString()%>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="<nobr>Campaign name</nobr>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<%#((Bobs.InsertionOrder)Container.DataItem).CampaignName.ToString()%>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="<nobr>Start date</nobr>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<%#Utilities.DateToString(((Bobs.InsertionOrder)Container.DataItem).CampaignStartDate)%>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="<nobr>End date</nobr>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<%#Utilities.DateToString(((Bobs.InsertionOrder)Container.DataItem).CampaignEndDate)%>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="<nobr>Promoter</nobr>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<%#((Bobs.InsertionOrder)Container.DataItem).PromoterK > 0 ? ((Bobs.InsertionOrder)Container.DataItem).Promoter.LinkNewWindow() : "" %>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="<nobr>Created date</nobr>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<%#Utilities.DateToString(((Bobs.InsertionOrder)Container.DataItem).DateTimeCreated)%>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="<nobr>Due date</nobr>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<%#Utilities.DateToString(((Bobs.InsertionOrder)Container.DataItem).NextInvoiceDue)%>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="<nobr>Client ref</nobr>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<nobr><%#((Bobs.InsertionOrder)Container.DataItem).ClientRef%></nobr>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="<nobr>Invoice period</nobr>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<nobr><%#((Bobs.InsertionOrder)Container.DataItem).InvoicePeriod%></nobr>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="<nobr>View insertion order</nobr>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<nobr><%#((Bobs.InsertionOrder)Container.DataItem).AdminLinkNewWindow()%></nobr>
								</ItemTemplate>
							</asp:TemplateField>
						</Columns>
					</asp:GridView>
					<asp:GridView ID="SearchResultsCampaignCreditGridView" runat="server" AllowPaging="True" AutoGenerateColumns="False" Visible="false" 
						OnPageIndexChanging="SearchResultsCampaignCreditGridView_PageIndexChanging" TabIndex="41" CssClass="dataGrid" BorderWidth="0" CellPadding="3"
						AlternatingRowStyle-CssClass="dataGridAltItem" RowStyle-CssClass="dataGridItem" SelectedRowStyle-CssClass="dataGridSelectedItem" PagerStyle-HorizontalAlign="Right"
						PageSize="20" HeaderStyle-CssClass="dataGridHeader" AlternatingRowStyle-VerticalAlign="Top" RowStyle-VerticalAlign="Top">
						<Columns>
							<asp:TemplateField HeaderText="<nobr>K #</nobr>" SortExpression="K"  ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<%#((Bobs.CampaignCredit)Container.DataItem).K.ToString()%>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="<nobr>Action date</nobr>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<%#Utilities.DateToString(((Bobs.CampaignCredit)Container.DataItem).ActionDateTime)%>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="<nobr>Object</nobr>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<%#BuyableObjectLink((Bobs.CampaignCredit)Container.DataItem)%>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="<nobr>Credits</nobr>" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<%#((Bobs.CampaignCredit)Container.DataItem).Credits.ToString("N0")%>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="<nobr>Promoter</nobr>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<%#((Bobs.CampaignCredit)Container.DataItem).Promoter.LinkNewWindow()%>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="<nobr>View</nobr>" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
								<ItemTemplate>
									<nobr><%#((Bobs.CampaignCredit)Container.DataItem).AdminLinkNewWindow()%></nobr>
								</ItemTemplate>
							</asp:TemplateField>							
						</Columns>
					</asp:GridView>
				</td>
			</tr>
		</table>
		<br />
		<table style="font-weight:bold;">
			<tr runat="server" ID="TotalExVatRow" visible="false">
				<td><nobr><asp:Label ID="TotalExVatLabel" runat="server" Text="Total ex VAT:"></asp:Label></nobr></td>
				<td align="right"><nobr><asp:Label ID="TotalExVatValueLabel" runat="server" Text=""></asp:Label></nobr></td>
				<td style="padding-left:24px;"><nobr>Ticket sales ex VAT:</nobr></td>
				<td align="right"><nobr><asp:Label ID="TicketSalesExVATValueLabel" runat="server" Text=""></asp:Label></nobr></td>
				<td style="padding-left:24px;"><nobr>Booking fees ex VAT:</nobr></td>
				<td align="right"><nobr><asp:Label ID="BookingFeeExVATValueLabel" runat="server" Text=""></asp:Label></nobr></td>
			</tr>
			<tr runat="server" ID="TotalVatRow" visible="false">
				<td><nobr><asp:Label ID="TotalVatLabel" runat="server" Text="Total VAT:"></asp:Label></nobr></td>
				<td align="right"><nobr><asp:Label ID="TotalVatValueLabel" runat="server" Text=""></asp:Label></nobr></td>
				<td style="padding-left:24px;"><nobr>Ticket sales VAT:</nobr></td>
				<td align="right"><nobr><asp:Label ID="TicketSalesVATValueLabel" runat="server" Text=""></asp:Label></nobr></td>
				<td style="padding-left:24px;"><nobr>Booking fees VAT:</nobr></td>
				<td align="right"><nobr><asp:Label ID="BookingFeeVATValueLabel" runat="server" Text=""></asp:Label></nobr></td>
			</tr>
			<tr runat="server" ID="TotalRow" visible="false">
				<td><nobr><asp:Label ID="TotalLabel" runat="server" Text="Total:"></asp:Label></nobr></td>
				<td align="right"><nobr><asp:Label ID="TotalValueLabel" runat="server" Text=""></asp:Label></nobr></td>
				<td style="padding-left:24px;"><nobr>Ticket sales total:</nobr></td>
				<td align="right"><nobr><asp:Label ID="TicketSalesTotalValueLabel" runat="server" Text=""></asp:Label></nobr></td>
				<td style="padding-left:24px;"><nobr>Booking fees total:</nobr></td>
				<td align="right"><nobr><asp:Label ID="BookingFeeTotalValueLabel" runat="server" Text=""></asp:Label></nobr></td>
			</tr>
			<tr runat="server" ID="TotalTransferRow" visible="false">
				<td><nobr>Total: </nobr></td>
				<td align="right"><nobr><asp:Label ID="TotalTransferValueLabel" runat="server" Text=""></asp:Label></nobr></td>
			</tr>
		</table>
	</asp:Panel>
</div>
