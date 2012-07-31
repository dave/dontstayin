<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InvoiceScreen.ascx.cs" Inherits="Spotted.Admin.InvoiceScreen" %>

<%@ Register TagPrefix="DbCombo" Namespace="Cambro.Web.DbCombo" Assembly="Cambro.Web.DbCombo" %>
<%@ Register TagPrefix="DSIControls" TagName="AddOnlyTextBox" Src="/Controls/AddOnlyTextBox.ascx" %>
<script language="JavaScript">
  function InvoiceScreenToggleOverrideDueDate()
  {
	var dueDateCheckBox = document.getElementById("<%= OverrideDueDateCheckBox.ClientID %>");
	var dueDatePanel = document.getElementById("<%= OverrideDueDatePanel.ClientID %>");
	
	if(dueDateCheckBox != null && dueDatePanel != null)
		dueDatePanel.style.display = dueDateCheckBox.checked?'':'none';
  }
  function InvoiceScreenToggleOverrideTaxDate()
  {
	var taxDateCheckBox = document.getElementById("<%= OverrideTaxDateCheckBox.ClientID %>");
	var taxDatePanel = document.getElementById("<%= OverrideTaxDatePanel.ClientID %>");
	
	if(taxDateCheckBox != null && taxDatePanel != null)
		taxDatePanel.style.display = taxDateCheckBox.checked?'':'none';
  }
  function InvoiceScreenToggleOverrideEmailRecipients()
  {
  	var emailRecipientsCheckBox = document.getElementById("<%= OverrideEmailRecipientsCheckBox.ClientID %>");
	var emailRecipientsTD = document.getElementById("<%= OverrideEmailRecipientTD.ClientID %>");
	
	if(emailRecipientsCheckBox != null && emailRecipientsTD != null)
		emailRecipientsTD.style.display = emailRecipientsCheckBox.checked?'':'none';
  }
  
</script>
<dsi:h1 runat="server" id="H1">Invoice</dsi:h1>
<div class="ContentBorder">
    <asp:Panel ID="MainPanel" runat="server" Width="600px" >
        <table width="600">
            <tr>
				<td colspan="5"><asp:Label id="ErrorLabel" runat="server" Font-Italic="True" ForeColor="Red" Visible="False" Font-Bold="True"></asp:Label>
				<h2>Invoice details</h2></td>
			</tr>
            <tr>
                <td style="width: 90px;"><asp:Label ID="InvoiceKLabel" runat="server" Text="Invoice&nbsp;K"></asp:Label></td>
                <td style="width: 180px;"><asp:Label runat="server" ID="InvoiceKValueLabel" Width="140px"></asp:Label></td>
                <td style="width: 20px;">&nbsp;</td>
                <td style="width: 120px;"><asp:Label ID="CreatedDateLabel" runat="server" Text="Created&nbsp;date"></asp:Label></td>
                <td style="width: 190px;"><asp:TextBox ID="CreatedDateTextBox" runat="server" MaxLength="20" ReadOnly="True" TabIndex="-1" Width="95px" CssClass="disabledTextBox"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 90px;"><asp:Label ID="PromoterLabel" runat="server" Text="Promoter"></asp:Label></td>
                <td colspan="2" >
					<js:HtmlAutoComplete Width="168px" ID="uiPromotersAutoComplete" runat="server" WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetPromotersWithK" AutoPostBack="true" TabIndex="1"/>
                    <asp:Label ID="PromoterValueLabel" runat="server"></asp:Label>
					<asp:Label ID="PromoterAvailableFundsLabel" runat="server"></asp:Label>
				</td>
                <td style="width: 120px;"><asp:Label ID="DueDateLabel" runat="server" Text="Due&nbsp;date"></asp:Label></td>
                <td style="width: 190px;"><asp:CheckBox ID="OverrideDueDateCheckBox" runat="server" Text="Override" onselect="InvoiceScreenToggleOverrideDueDate()" onclick="InvoiceScreenToggleOverrideDueDate();" Width="80px"/>
					<asp:Panel ID="OverrideDueDatePanel" runat="server" style="display:none;"><dsi:Cal ID="DueDateCal" runat="server" /></asp:Panel>
					<asp:Label ID="DueDateValueLabel" runat="server" Visible="False" Width="95px"></asp:Label><asp:CustomValidator ID="DueDateCustomValidator" runat="server" Display="None" ErrorMessage="Invalid due date"></asp:CustomValidator></td>
            </tr>
            <tr>
                <td style="width: 90px;"><asp:Label ID="UserLabel" runat="server" Text="User"></asp:Label></td>
                <td colspan="2">
					<js:HtmlAutoComplete Width="150px" TabIndex="2" ID="uiUsersAutoComplete" runat="server"  WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetUsersWithK"/>
					<asp:DropDownList ID="UserDropDownList" runat="server" TabIndex="2" Visible="false"></asp:DropDownList><asp:Label ID="UserValueLabel" runat="server"></asp:Label>
					<asp:CustomValidator ID="PromoterAndUserCustomValidator" runat="server" OnServerValidate="PromoterAndUserVal" ErrorMessage="Must select promoter or user" Display="None"></asp:CustomValidator></td>
                <td style="width: 120px;"><asp:Label ID="TaxDateLabel" runat="server" Text="Tax&nbsp;date"></asp:Label></td>
				<td style="width: 190px;"><asp:CheckBox ID="OverrideTaxDateCheckBox" runat="server" Text="Override" onselect="InvoiceScreenToggleOverrideTaxDate()" onclick="InvoiceScreenToggleOverrideTaxDate();" Width="80px"/>
					<asp:Panel ID="OverrideTaxDatePanel" runat="server"><dsi:Cal ID="TaxDateCal" runat="server" /></asp:Panel>
					<asp:Label ID="TaxDateValueLabel" runat="server" Visible="False" Width="95px"></asp:Label>
					<asp:CustomValidator ID="TaxDateCustomValidator" runat="server" Display="None" ErrorMessage="Invalid tax date"></asp:CustomValidator>
				</td>
            </tr>
            <tr>
				<td style="width: 90px;"><asp:Label ID="ActionUserLabel" runat="server" Text="Action&nbsp;user"></asp:Label></td>
                <td colspan="2" >
					<js:HtmlAutoComplete Width="168px" ID="uiActionUserAutoComplete" runat="server"  WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetUsersWithK" TabIndex="3"/>
                    <asp:Label ID="ActionUserValueLabel" runat="server"></asp:Label><asp:RequiredFieldValidator ID="ActionUserRequiredFieldValidator" runat="server" ControlToValidate="uiActionUserAutoComplete"
                        ErrorMessage="Must select action user" Display="None"></asp:RequiredFieldValidator></td>                
                <td style="width: 120px;"><asp:Label ID="PaidDateLabel" runat="server" Text="Paid&nbsp;date" Visible="False"></asp:Label></td>
                <td style="width: 190px;"><asp:TextBox ID="PaidDateTextBox" runat="server" MaxLength="20" ReadOnly="True" TabIndex="-1" Width="95px" CssClass="disabledTextBox"></asp:TextBox></td>
            </tr>
            <tr><td style="width: 90px;"><asp:Label ID="PaidLabel" runat="server" Text="Paid"></asp:Label></td>
                <td style="width: 180px;"><asp:CheckBox ID="PaidCheckBox" runat="server" Visible="False"/><asp:Image ID="PaidImage" runat="server" ImageUrl="/gfx/icon-tick-up.png" Visible="False" /><asp:Image ID="NotPaidImage" runat="server" ImageUrl="/gfx/icon-cross-up.png" Visible="False" /></td>
                <td style="width: 20px;"></td>
                <td style="width: 120px;"><asp:Label ID="PriceLabel" runat="server" Text="Price"></asp:Label></td>
                <td style="width: 190px;"><asp:TextBox ID="PriceTextBox" runat="server" ReadOnly="True" TabIndex="-1" Width="95px" CssClass="disabledTextBox"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 90px;"><asp:Label ID="VATCodeLabel" runat="server" Text="VAT&nbsp;code"></asp:Label></td>
                <td style="width: 180px;"><asp:DropDownList ID="VatCodeDropDownList" runat="server" TabIndex="7" Width="140px" OnSelectedIndexChanged="VatCodeDropDownList_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList><asp:TextBox ID="VatCodeTextBox" runat="server" CssClass="disabledTextBox" ReadOnly="True" Visible="False"></asp:TextBox></td>
                <td style="width: 20px;"></td>
                <td style="width: 120px;"><asp:Label ID="VATLabel" runat="server" Text="VAT"></asp:Label></td>
                <td style="width: 190px;"><asp:TextBox ID="VATTextBox" runat="server" ReadOnly="True" TabIndex="-1" Width="95px" CssClass="disabledTextBox"></asp:TextBox></td>
            </tr>
             <tr>
                <td style="width: 90px;"><asp:Label ID="SalesUsrLabel" runat="server" Text="Sales&nbsp;user"></asp:Label></td>
                <td style="width: 180px;"><asp:DropDownList ID="SalesUsrDropDownList" runat="server" Width="140px"></asp:DropDownList><asp:Label ID="SalesUsrValueLabel" runat="server"></asp:Label></td>
                <td style="width: 20px; "></td>
                <td style="width: 120px;"><asp:Label ID="TotalLabel" runat="server" Text="Total"></asp:Label></td>
                <td style="width: 190px;"><asp:TextBox ID="TotalTextBox" runat="server" ReadOnly="True" TabIndex="-1" Width="95px" CssClass="disabledTextBox"></asp:TextBox></td>
            </tr>
            <tr>
				<td style="width: 90px;"><asp:Label ID="SalesAmountLabel" runat="server" Text="Sales&nbsp;amount"></asp:Label></td>
                <td style="width: 180px;"><asp:TextBox ID="SalesAmountTextBox" runat="server" Width="140px"></asp:TextBox>
					<asp:CustomValidator ID="SalesAmountCustomValidator" runat="server" Display="None" ErrorMessage="Sales amount must be a positive amount" ControlToValidate="SalesAmountTextBox" OnServerValidate="MoneyTextBoxVal"></asp:CustomValidator></td>
                <td></td>
                <td style="width: 120px;"><asp:Label ID="AmountDueLabel" runat="server" Text="Amount&nbsp;due"></asp:Label></td>
                <td style="width: 190px;"><asp:TextBox ID="AmountDueTextBox" runat="server" ReadOnly="True" TabIndex="-1" Width="95px" CssClass="disabledTextBox"></asp:TextBox></td>
            </tr>
			<tr>
				<td style="width: 90px;"><asp:Label ID="PurchaseOrderNumberLabel" runat="server" Text="<nobr>Purchase Order #</nobr>"></asp:Label></td>
                <td style="width: 180px;"><asp:TextBox ID="PurchaseOrderNumberTextBox" runat="server" Width="140px"></asp:TextBox></td>
                <td></td>
                <td><asp:Label runat="server" ID="uiAgencyDiscountLabel" Text="Agency discount"></asp:Label></td>
                <td><asp:TextBox runat="server" ID="uiAgencyDiscountTextBox"  Width="140px" OnTextChanged="uiAgencyDiscountLabel_TextChanged" AutoPostBack="True"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 90px;" valign="top"><asp:Label ID="NotesLabel" runat="server" Text="Notes"></asp:Label></td>
                <td colspan="4"><DSIControls:AddOnlyTextBox id="NotesAddOnlyTextBox" runat="server" TabIndex="20" CssClass="notesAddOnly"></DSIControls:AddOnlyTextBox></td>
            </tr>
        </table>
		&nbsp;
    </asp:Panel>
    <asp:Panel ID="InvoiceItemsPanel" runat="server" TabIndex="30" Width="600px">
        <table width="600">
            <tr>
                <td>
					<h2>Invoice items</h2>
				</td>
			</tr>
			<tr id="AddBannerInvoiceItemRow" runat="server" visible="false">
				<td>
					<asp:DropDownList ID="AddBannerInvoiceItemDropDownList" runat="server"></asp:DropDownList>&nbsp;<asp:Button ID="AddBannerInvoiceItemButton" runat="server" Text="Add Banner" OnClick="AddBannerInvoiceItemButton_Click" CausesValidation="false"/></td>
			</tr>
			<tr>
				<td>
					<asp:GridView ID="InvoiceItemsGridView" runat="server" AllowPaging="False" AutoGenerateColumns="False" 
						OnRowCommand="InvoiceItemsGridView_RowCommand" OnPageIndexChanging="InvoiceItemsGridView_PageIndexChanging" 
						OnRowCancelingEdit="InvoiceItemsGridView_RowCancelingEdit" OnRowEditing="InvoiceItemsGridView_RowEditing" 
						OnRowUpdating="InvoiceItemsGridView_RowUpdating" OnRowDataBound="InvoiceItemsGridView_RowDataBound" ShowFooter="True" 
						OnRowDeleting="InvoiceItemsGridView_RowDeleting" OnRowCreated="InvoiceItemsGridView_RowCreated" TabIndex="25" CssClass="dataGrid"
						AlternatingRowStyle-CssClass="dataGridAltItem" GridLines="None" BorderWidth="0" CellPadding="3" HeaderStyle-CssClass="dataGridHeader"
						SelectedRowStyle-CssClass="dataGridSelectedItem" AlternatingRowStyle-VerticalAlign="Top" RowStyle-VerticalAlign="Top">
                        <Columns>
                        <asp:TemplateField HeaderText="Invoice&nbsp;item&nbsp;K" SortExpression="K" Visible="False" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
						    <EditItemTemplate>
							    <asp:Label ID="InvoiceItemKLabel" runat="server" Text='<%# Bind("K") %>'></asp:Label>
						    </EditItemTemplate>
						    <ItemTemplate>
							    <asp:Label ID="InvoiceItemKLabel" runat="server" Text='<%# Bind("K") %>'></asp:Label>
						    </ItemTemplate>
					    </asp:TemplateField>
					    <asp:TemplateField HeaderText="Type" SortExpression="Type" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
						    <EditItemTemplate>
                                <asp:DropDownList ID="EditTypeDropDownList" runat="server"></asp:DropDownList>
						    </EditItemTemplate>
						    <ItemTemplate>
							    <asp:Label ID="TypeLabel" runat="server" Text='<%# InvoiceItemTypeToString(Container.DataItem) %>'></asp:Label>
						    </ItemTemplate>
						    <FooterTemplate>
	                            <asp:DropDownList ID="NewTypeDropDownList" runat="server"></asp:DropDownList>
						    </FooterTemplate>
					    </asp:TemplateField>
					    <asp:TemplateField HeaderText="Description" SortExpression="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
						    <EditItemTemplate>
							    <asp:TextBox ID="EditDescriptionTextBox" runat="server" Text='<%# Bind("Description") %>' Width="170" MaxLength="255" onmouseover="stt('Invoice item description.');" onmouseout="htm();"></asp:TextBox>
							    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="InvoiceItemUpdate" runat="server" ControlToValidate="EditDescriptionTextBox" Display="Dynamic" ErrorMessage="* Must not be empty"></asp:RequiredFieldValidator>
						    </EditItemTemplate>
						    <ItemTemplate>
							    <asp:Label ID="DescriptionLabel" runat="server" Width="170" Text='<%# Bind("Description") %>'></asp:Label>
						    </ItemTemplate>
						    <FooterTemplate>
						        <asp:TextBox ID="NewDescriptionTextBox" runat="server" Text='' Width="170" MaxLength="255" onmouseover="stt('Invoice item description.');" onmouseout="htm();"></asp:TextBox>
						        <asp:RequiredFieldValidator ID="RequiredFieldValidator2a" ValidationGroup="InvoiceItemNew" runat="server" ControlToValidate="NewDescriptionTextBox" Display="Dynamic" ErrorMessage="* Must not be empty"></asp:RequiredFieldValidator>
						    </FooterTemplate>
					    </asp:TemplateField>
					    <asp:TemplateField HeaderText="Start&nbsp;date" SortExpression="RevenueStartDate" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
						    <EditItemTemplate>
							    <dsi:Cal ID="EditRevenueStartDateCal" runat="server" Date='<%# Bind("RevenueStartDate") %>' />
							    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="* Invalid date" Display="Dynamic" ControlToValidate="EditRevenueStartDateCal" OnServerValidate="DateVal" ValidationGroup="InvoiceItemUpdate"></asp:CustomValidator>
						    </EditItemTemplate>
						    <ItemTemplate>
							    <asp:Label ID="RevenueStartDateLabel" runat="server" Text='<%# Bind("RevenueStartDate", "{0:dd/MM/yy}") %>'></asp:Label>
						    </ItemTemplate>
						    <FooterTemplate>
						        <dsi:Cal ID="NewRevenueStartDateCal" runat="server" />
                                <asp:CustomValidator ID="NewRevenueStartDateCalCustomValidator" runat="server" ErrorMessage="* Invalid date" Display="Dynamic" ControlToValidate="NewRevenueStartDateCal" OnServerValidate="DateVal" ValidationGroup="InvoiceItemNew" Enabled="false"></asp:CustomValidator>
						    </FooterTemplate>
					    </asp:TemplateField>
                        <asp:TemplateField HeaderText="End&nbsp;date" SortExpression="RevenueEndDate" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
						    <EditItemTemplate>
						        <dsi:Cal ID="EditRevenueEndDateCal" runat="server" Date='<%# Bind("RevenueEndDate") %>' />
						        <asp:CustomValidator ID="CustomValidator3" runat="server" ErrorMessage="* Invalid date" Display="Dynamic" ControlToValidate="EditRevenueEndDateCal" OnServerValidate="DateVal" ValidationGroup="InvoiceItemUpdate"></asp:CustomValidator>
						        <asp:CustomValidator ID="CustomValidator7" runat="server" ErrorMessage="* Invalid date" Display="Dynamic" ControlToValidate="EditRevenueEndDateCal" OnServerValidate="EditRevenueEndDateVal" ValidationGroup="InvoiceItemUpdate"></asp:CustomValidator>
						    </EditItemTemplate>
						    <ItemTemplate>
							    <asp:Label ID="RevenueEndDateLabel" runat="server" Text='<%# Bind("RevenueEndDate", "{0:dd/MM/yy}") %>'></asp:Label>
						    </ItemTemplate>
						    <FooterTemplate>
						        <dsi:Cal ID="NewRevenueEndDateCal" runat="server" Date='<%# Bind("RevenueEndDate") %>' />
                                <asp:CustomValidator ID="NewRevenueEndDateCalCustomValidator1" runat="server" ErrorMessage="* Invalid date" Display="Dynamic" ControlToValidate="NewRevenueEndDateCal" OnServerValidate="DateVal" ValidationGroup="InvoiceItemNew" Enabled="false"></asp:CustomValidator>
                                <asp:CustomValidator ID="NewRevenueEndDateCalCustomValidator2" runat="server" ErrorMessage="* Invalid date" Display="Dynamic" ControlToValidate="NewRevenueEndDateCal" OnServerValidate="NewRevenueEndDateVal" ValidationGroup="InvoiceItemNew" Enabled="false"></asp:CustomValidator>
						    </FooterTemplate>
					    </asp:TemplateField>
					    <asp:TemplateField HeaderText="Gross price" SortExpression="PriceBeforeDiscount" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right">
							<ItemTemplate>
								<nobr><asp:Label ID="uiItemPriceBeforeDiscount" runat="server" Text='<%# Bind("PriceBeforeDiscount", "{0:c}") %>'  style="text-align:right"></asp:Label></nobr>
							</ItemTemplate>
							<EditItemTemplate>
