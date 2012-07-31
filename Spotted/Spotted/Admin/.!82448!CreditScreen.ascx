<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreditScreen.ascx.cs" Inherits="Spotted.Admin.CreditScreen" %>
<%@ Register TagPrefix="DbCombo" Namespace="Cambro.Web.DbCombo" Assembly="Cambro.Web.DbCombo" %>
<%@ Register TagPrefix="DSIControls" TagName="AddOnlyTextBox" Src="/Controls/AddOnlyTextBox.ascx" %>
<script language="JavaScript">
  function CreditScreenToggleOverrideTaxDate()
  {
	var taxDateCheckBox = document.getElementById("<%= OverrideTaxDateCheckBox.ClientID %>");
	var taxDatePanel = document.getElementById("<%= OverrideTaxDatePanel.ClientID %>");
	
	if(taxDateCheckBox != null && taxDatePanel != null)
		taxDatePanel.style.display = taxDateCheckBox.checked?'':'none';
  }
</script>
<dsi:h1 runat="server" id="H1_1">Credit</dsi:h1>
<div class="ContentBorder">
    <asp:Panel ID="MainPanel" runat="server" Width="600px">
        <table width="600">
            <tr><td colspan="5"><h2>Credit details</h2></td></tr>
            <tr>
                <td style="width: 90px;"><asp:Label ID="CreditKLabel" runat="server" Text="Credit&nbsp;K"></asp:Label></td>
                <td style="width: 220px;"><asp:Label runat="server" ID="CreditKValueLabel" Width="95px"></asp:Label><asp:TextBox ID="CreditKTextBox" runat="server" ReadOnly="True" Width="95px" CssClass="disabledTextBox"></asp:TextBox></td>
                <td style="width: 20px;">&nbsp;</td>
                <td style="width: 130px;"><asp:Label ID="CreatedDateLabel" runat="server" Text="Created&nbsp;date"></asp:Label></td>
                <td style="width: 140px;"><asp:TextBox ID="CreatedDateTextBox" runat="server" MaxLength="20" ReadOnly="True" TabIndex="-1" Width="95px" CssClass="disabledTextBox"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 90px;"><asp:Label ID="InvoiceKLabel" runat="server" Text="Invoice&nbsp;K"></asp:Label></td>
                <td colspan="2"><asp:Label runat="server" ID="InvoiceKValueLabel" Width="95px"></asp:Label><asp:HyperLink ID="ViewInvoiceHyperLink" runat="server" Visible="False">View invoice</asp:HyperLink></td>
                <td style="width: 130px;"><asp:Label ID="PaidDateLabel" runat="server" Text="Paid&nbsp;date"></asp:Label></td>
                <td style="width: 140px;"><asp:TextBox ID="PaidDateTextBox" runat="server" MaxLength="20" ReadOnly="True" TabIndex="-1"
                        Width="95px" CssClass="disabledTextBox"></asp:TextBox></td>
            </tr>
            <tr><td style="width: 90px;"><asp:Label ID="PromoterLabel" runat="server" Text="Promoter"></asp:Label></td>
                <td colspan="2"><asp:Label ID="PromoterValueLabel" runat="server"></asp:Label><asp:TextBox ID="PromoterKHiddenTextBox" runat="server" MaxLength="10" ReadOnly="True" 
					TabIndex="-1" Visible="False" Width="1px"></asp:TextBox></td>
				<td style="width: 120px">
					<asp:Label ID="TaxDateLabel" runat="server" Text="Tax&nbsp;date"></asp:Label></td>
				<td style="width: 190px">
					<asp:CheckBox ID="OverrideTaxDateCheckBox" runat="server" onclick="CreditScreenToggleOverrideTaxDate();"
						onselect="CreditScreenToggleOverrideTaxDate()" Text="Override" Width="80px" /><asp:Panel ID="OverrideTaxDatePanel" runat="server">
							<dsi:Cal id="TaxDateCal" runat="server"></dsi:Cal></asp:Panel>
					<asp:Label ID="TaxDateValueLabel" runat="server" Visible="False" Width="95px"></asp:Label><asp:CustomValidator
						ID="TaxDateCustomValidator" runat="server" Display="None" ErrorMessage="Invalid tax date" ControlToValidate="TaxDateCal"></asp:CustomValidator></td>
            </tr>
            <tr>
                <td style="width: 90px;"><asp:Label ID="UserLabel" runat="server" Text="User"></asp:Label></td>
                <td colspan="2"><asp:Label ID="UserValueLabel" runat="server"></asp:Label><asp:TextBox ID="UserKHiddenTextBox" runat="server" MaxLength="10" ReadOnly="True" TabIndex="-1" Visible="False" Width="1px"></asp:TextBox></td>
                <td style="width: 130px;"><asp:Label ID="PriceLabel" runat="server" Text="Price"></asp:Label></td>
                <td style="width: 140px;"><asp:TextBox ID="PriceTextBox" runat="server" ReadOnly="True" TabIndex="-1" Width="95px" CssClass="disabledTextBox"></asp:TextBox></td>
            </tr>
            <tr><td style="width: 90px;"><asp:Label ID="ActionUserLabel" runat="server" Text="Action&nbsp;user"></asp:Label></td>
                <td colspan="2"><asp:Label ID="ActionUserValueLabel" runat="server"></asp:Label><asp:TextBox ID="ActionUserKHiddenTextBox"
                        runat="server" MaxLength="10" ReadOnly="True" TabIndex="-1" Visible="False" Width="1px"></asp:TextBox></td>
                <td style="width: 130px;"><asp:Label ID="VATLabel" runat="server" Text="VAT"></asp:Label></td>
                <td style="width: 140px;"><asp:TextBox ID="VATTextBox" runat="server" ReadOnly="True" TabIndex="-1" Width="95px" CssClass="disabledTextBox"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 90px;"><asp:Label ID="PaidLabel" runat="server" Text="Refunded"></asp:Label></td>
                <td style="width: 220px;"><asp:CheckBox ID="PaidCheckBox" runat="server" onclick="ToggleDisplay('Content_DueDateCal');" Visible="False" />
                    <asp:Image ID="PaidImage" runat="server" ImageUrl="/gfx/icon-tick-up.png" Visible="False" /><asp:Image ID="NotPaidImage" runat="server" ImageUrl="/gfx/icon-cross-up.png" Visible="False" /></td>
                <td style="width: 20px;"></td>
                <td style="width: 130px;"><asp:Label ID="TotalLabel" runat="server" Text="Total"></asp:Label></td>
                <td style="width: 140px;"><asp:TextBox ID="TotalTextBox" runat="server" ReadOnly="True" TabIndex="-1" Width="95px" CssClass="disabledTextBox"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 90px;">
                    <asp:Label ID="VATCodeLabel" runat="server" Text="VAT&nbsp;code"></asp:Label></td>
                <td style="width: 220px;">
                    <asp:TextBox ID="VATCodeTextBox" runat="server" ReadOnly="True" TabIndex="-1" Width="95px" CssClass="disabledTextBox"></asp:TextBox><asp:TextBox ID="VATCodeNumberHiddenTextBox" runat="server" 
						MaxLength="10" ReadOnly="True" TabIndex="-1" Visible="False" Width="1px"></asp:TextBox></td>
				<td></td>
				<td></td>
				<td></td>
            </tr>
			<tr>
				<td style="width: 90px">
					<asp:Label ID="SalesUsrLabel" runat="server" Text="Sales&nbsp;user" Visible="False"></asp:Label></td>
				<td style="width: 180px">
					<asp:Label ID="SalesUsrValueLabel" runat="server" Visible="False"></asp:Label><asp:TextBox
						ID="SalesUserKHiddenTextBox" runat="server" MaxLength="10" ReadOnly="True" TabIndex="-1"
						Visible="False" Width="1px"></asp:TextBox></td>
				<td>
				</td>
				<td>
				</td>
				<td>
				</td>
			</tr>
			<tr>
				<td style="width: 90px">
					<asp:Label ID="SalesAmountLabel" runat="server" Text="Sales&nbsp;amount" Visible="False"></asp:Label></td>
				<td style="width: 180px">
					<asp:TextBox ID="SalesAmountTextBox" runat="server" Visible="False" Width="140px"></asp:TextBox>
					<asp:CustomValidator ID="SalesAmountCustomValidator" runat="server" ControlToValidate="SalesAmountTextBox"
						Display="None" ErrorMessage="Sales amount must < 0" OnServerValidate="MoneyTextBoxVal"></asp:CustomValidator></td>
				<td>
				</td>
				<td>
				</td>
				<td>
				</td>
			</tr>
            <tr>
                <td style="width: 90px;" valign="top"><asp:Label ID="NotesLabel" runat="server" Text="Notes"></asp:Label></td>
                <td colspan="4"><DSIControls:AddOnlyTextBox id="NotesAddOnlyTextBox" runat="server"></DSIControls:AddOnlyTextBox></td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="CreditItemsPanel" runat="server" TabIndex="30" Width="600px">
        <table width="600">
            <tr>
                <td colspan="5">
					<h2>Credit items</h2>
					<asp:GridView ID="CreditItemsGridView" runat="server" AllowPaging="False" AutoGenerateColumns="False" 
						OnRowCommand="CreditItemsGridView_RowCommand" OnPageIndexChanging="CreditItemsGridView_PageIndexChanging" 
						OnRowCancelingEdit="CreditItemsGridView_RowCancelingEdit" OnRowEditing="CreditItemsGridView_RowEditing" 
						OnRowUpdating="CreditItemsGridView_RowUpdating" OnRowDataBound="CreditItemsGridView_RowDataBound" ShowFooter="True" 
						OnRowDeleting="CreditItemsGridView_RowDeleting" OnRowCreated="CreditItemsGridView_RowCreated" TabIndex="25"
						 CssClass="dataGrid" AlternatingRowStyle-CssClass="dataGridAltItem" GridLines="None"
						HeaderStyle-CssClass="dataGridHeader" SelectedRowStyle-CssClass="dataGridSelectedItem" RowStyle-VerticalAlign="Top">
						<Columns>
							<asp:TemplateField HeaderText="Credit&nbsp;item&nbsp;K" SortExpression="K" Visible="False">
								<EditItemTemplate>
									<asp:Label ID="CreditItemKLabel" runat="server" Text='<%# Bind("K") %>'></asp:Label>
								</EditItemTemplate>
								<ItemStyle HorizontalAlign="Left" />
								<ItemTemplate>
									<asp:Label ID="CreditItemKLabel" runat="server" Text='<%# Bind("K") %>'></asp:Label>
								</ItemTemplate>
								<HeaderStyle HorizontalAlign="Left" />
							</asp:TemplateField>
							<asp:TemplateField HeaderText="Type" SortExpression="Type">
								<EditItemTemplate>
									<asp:DropDownList ID="EditTypeDropDownList" runat="server"></asp:DropDownList>
								</EditItemTemplate>
								<ItemStyle HorizontalAlign="Left" />
								<ItemTemplate>
									<asp:Label ID="TypeLabel" runat="server" Text='<%# CreditItemTypeToString(Container.DataItem) %>'></asp:Label>
								</ItemTemplate>
								<HeaderStyle HorizontalAlign="Left" />
								<FooterTemplate>
									<asp:DropDownList ID="NewTypeDropDownList" runat="server"></asp:DropDownList>
								</FooterTemplate>
							</asp:TemplateField>
						    
							<asp:TemplateField HeaderText="Description" SortExpression="Description">
								<EditItemTemplate>
									<asp:TextBox ID="EditDescriptionTextBox" runat="server" Text='<%# Bind("Description") %>' Width="150"></asp:TextBox>
									<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="CreditItemUpdate" runat="server" ControlToValidate="EditDescriptionTextBox" Display="Dynamic" ErrorMessage="* Must not be empty"></asp:RequiredFieldValidator>
								</EditItemTemplate>
								<ItemStyle HorizontalAlign="Left" />
								<ItemTemplate>
									<asp:Label ID="DescriptionLabel" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
								</ItemTemplate>
								<HeaderStyle HorizontalAlign="Left" />
								<FooterTemplate>
									<asp:TextBox ID="NewDescriptionTextBox" runat="server" Text='' Width="150"></asp:TextBox>
									<asp:RequiredFieldValidator ID="RequiredFieldValidator2a" ValidationGroup="CreditItemNew" runat="server" ControlToValidate="NewDescriptionTextBox" Display="Dynamic" ErrorMessage="* Must not be empty"></asp:RequiredFieldValidator>
								</FooterTemplate>
							</asp:TemplateField>
						    
							<asp:TemplateField HeaderText="Start&nbsp;date" SortExpression="RevenueStartDate">
								<EditItemTemplate>
									<dsi:Cal id="EditRevenueStartDateCal" runat="server" Date='<%# Bind("RevenueStartDate") %>'></dsi:Cal>
									<asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="* Invalid date" Display="Dynamic" ControlToValidate="EditRevenueStartDateCal" OnServerValidate="DateVal" ValidationGroup="CreditItemUpdate"></asp:CustomValidator>
								</EditItemTemplate>
								<ItemStyle HorizontalAlign="Left" />
								<ItemTemplate>
									<asp:Label ID="RevenueStartDateLabel" runat="server" Text='<%# Bind("RevenueStartDate", "{0:dd/MM/yy}") %>'></asp:Label>
								</ItemTemplate>
								<HeaderStyle HorizontalAlign="Left" />
								 <FooterTemplate>
									<dsi:Cal id="NewRevenueStartDateCal" runat="server"></dsi:Cal>
									<asp:CustomValidator ID="CustomValidator2" runat="server" ErrorMessage="* Invalid date" Display="Dynamic" ControlToValidate="NewRevenueStartDateCal" OnServerValidate="DateVal" ValidationGroup="CreditItemNew"></asp:CustomValidator>
								</FooterTemplate>
							</asp:TemplateField>
				    
							 <asp:TemplateField HeaderText="End&nbsp;date" SortExpression="RevenueEndDate">
								<EditItemTemplate>
									<dsi:Cal id="EditRevenueEndDateCal" runat="server" Date='<%# Bind("RevenueEndDate") %>'></dsi:Cal>
									<asp:CustomValidator ID="CustomValidator3" runat="server" ErrorMessage="* Invalid date" ControlToValidate="EditRevenueEndDateCal" OnServerValidate="DateVal" ValidationGroup="CreditItemUpdate"></asp:CustomValidator>
									<asp:CustomValidator ID="CustomValidator11" runat="server" ErrorMessage="* Invalid date" Display="Dynamic" ControlToValidate="EditRevenueEndDateCal" OnServerValidate="EditRevenueEndDateVal" ValidationGroup="CreditItemUpdate"></asp:CustomValidator>
								</EditItemTemplate>
								<ItemStyle HorizontalAlign="Left" />
								<ItemTemplate>
									<asp:Label ID="RevenueEndDateLabel" runat="server" Text='<%# Bind("RevenueEndDate", "{0:dd/MM/yy}") %>'></asp:Label>
								</ItemTemplate>
								<HeaderStyle HorizontalAlign="Left" />
								<FooterTemplate>
									<dsi:Cal id="NewRevenueEndDateCal" runat="server" Date='<%# Bind("RevenueEndDate") %>'></dsi:Cal>
									<asp:CustomValidator ID="CustomValidator4" runat="server" ErrorMessage="* Invalid date" Display="Dynamic" ControlToValidate="NewRevenueEndDateCal" OnServerValidate="DateVal" ValidationGroup="CreditItemNew"></asp:CustomValidator>
									<asp:CustomValidator ID="CustomValidator12" runat="server" ErrorMessage="* Invalid date" Display="Dynamic" ControlToValidate="NewRevenueEndDateCal" OnServerValidate="NewRevenueEndDateVal" ValidationGroup="CreditItemNew"></asp:CustomValidator>
								</FooterTemplate>
							</asp:TemplateField>
						    
							<asp:TemplateField HeaderText="Price" SortExpression="Price">
								<EditItemTemplate>
