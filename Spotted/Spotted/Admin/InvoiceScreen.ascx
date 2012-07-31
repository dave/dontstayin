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
								<asp:TextBox ID="EditPriceBeforeDiscountTextBox" runat="server" Width="65" Text='' onmouseover="stt('Price in £GBP.');" onmouseout="htm();"></asp:TextBox>
							    <asp:CustomValidator ID="CustomValidator5" runat="server" ErrorMessage="* Invalid price" Display="Dynamic" ControlToValidate="EditPriceBeforeDiscountTextBox" OnServerValidate="MoneyTextBoxVal" ValidationGroup="InvoiceItemUpdate" Enabled="false"></asp:CustomValidator>
							</EditItemTemplate>
							<FooterTemplate>
								<asp:TextBox ID="NewPriceBeforeDiscountTextBox" runat="server" Width="65" Text='' onmouseover="stt('Price in £GBP.');" onmouseout="htm();" ></asp:TextBox>
						        <asp:CustomValidator ID="CustomValidator6" runat="server" ErrorMessage="* Invalid price" Display="Dynamic" ControlToValidate="NewPriceBeforeDiscountTextBox" OnServerValidate="MoneyTextBoxVal" ValidationGroup="InvoiceItemNew" Enabled="false"></asp:CustomValidator>
							</FooterTemplate>
					    </asp:TemplateField>
					    <asp:TemplateField HeaderText="Item discount" SortExpression="Discount" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right">
							<ItemTemplate>
								<nobr><asp:Label ID="uiItemDiscount" runat="server" Text='<%# Bind("Discount", "{0:p}") %>' Width="55" style="text-align:right"></asp:Label></nobr>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox ID="uiEditDiscountTextBox" Width="55" runat="server" Text='<%#Bind("Discount", "{0:p}") %>'></asp:TextBox>
							</EditItemTemplate>
							<FooterTemplate>
								<asp:TextBox ID="uiNewDiscountTextBox" runat="server" Width="55"></asp:TextBox>
							</FooterTemplate>
					    </asp:TemplateField>
					    <asp:TemplateField HeaderText="Price after discount" SortExpression="PriceBeforeAgencyDiscount" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right">
							<ItemTemplate>
								<nobr><asp:Label ID="uiItemPriceBeforeAgencyDiscount" runat="server" Text='<%# Bind("PriceBeforeAgencyDiscount", "{0:c}") %>'  style="text-align:right"></asp:Label></nobr>
							</ItemTemplate>
					    </asp:TemplateField>
					    <asp:TemplateField HeaderText="Agency discount" SortExpression="AgencyDiscount" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right">
							<ItemTemplate>
								<nobr><asp:Label ID="uiItemAgencyDiscount" runat="server" Text='<%# Bind("AgencyDiscount", "{0:p}") %>'  style="text-align:right"></asp:Label></nobr>								
							</ItemTemplate>
					    </asp:TemplateField>					
	                    <asp:TemplateField HeaderText="Price" SortExpression="Price" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right">
						    <ItemTemplate>
							    <nobr><asp:Label ID="PriceLabel" runat="server" Text='<%# Bind("Price", "{0:c}") %>'></asp:Label></nobr>
						    </ItemTemplate>					    
					    </asp:TemplateField>
					     <asp:TemplateField HeaderText="VAT" SortExpression="Vat" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
						    <EditItemTemplate>
							    <asp:TextBox ID="EditVatTextBox" runat="server" Visible="false" Width="60" Text='<%# Bind("Vat") %>' ReadOnly="true" onmouseover="stt('VAT calculated from VAT Code.');" onmouseout="htm();"></asp:TextBox>
						    </EditItemTemplate>
						    <ItemTemplate>
							    <nobr><asp:Label ID="VatLabel" runat="server" Text='<%# Bind("Vat", "{0:c}") %>'  style="text-align:right"></asp:Label></nobr>
						    </ItemTemplate>
						    <FooterTemplate>
						        <asp:TextBox ID="NewVatTextBox" runat="server" Width="60" Visible="false" Text='' ReadOnly="true" TabIndex="-1" onmouseover="stt('VAT calculated from VAT Code.');" onmouseout="htm();"></asp:TextBox>
						    </FooterTemplate>
					    </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total" SortExpression="Total" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
						    <EditItemTemplate>
							    <asp:TextBox ID="EditTotalTextBox" runat="server" Width="65" Text='<%# Bind("Total") %>' onmouseover="stt('Total Price incl. VAT in £GBP.');" onmouseout="htm();"></asp:TextBox>
							    <asp:CustomValidator ID="CustomValidator8a" runat="server" ErrorMessage="* Invalid total" Display="Dynamic" ControlToValidate="EditTotalTextBox" OnServerValidate="MoneyTextBoxVal" ValidationGroup="InvoiceItemUpdate"></asp:CustomValidator>
							    <asp:CustomValidator ID="CustomValidator8" runat="server" ErrorMessage="* Must enter price or total" Display="Dynamic" ControlToValidate="EditTotalTextBox" OnServerValidate="EditPriceBeforeDiscountTotalVal" ValidationGroup="InvoiceItemUpdate"></asp:CustomValidator>
						    </EditItemTemplate>
						    <ItemTemplate>
							    <nobr><asp:Label ID="TotalLabel" runat="server" Text='<%# Bind("Total", "{0:c}") %>' style="text-align:right"></asp:Label></nobr>
						    </ItemTemplate>
						    <FooterTemplate>
						        <asp:TextBox ID="NewTotalTextBox" runat="server" Width="65" Text='' onmouseover="stt('Total Price incl. VAT in £GBP.');" onmouseout="htm();"></asp:TextBox>
						        <asp:CustomValidator ID="CustomValidator9" runat="server" ErrorMessage="* Invalid total" Display="Dynamic" ControlToValidate="NewTotalTextBox" OnServerValidate="MoneyTextBoxVal" ValidationGroup="InvoiceItemNew"></asp:CustomValidator>
						        <asp:CustomValidator ID="CustomValidator10" runat="server" ErrorMessage="* Must enter price or total" Display="Dynamic" ControlToValidate="NewTotalTextBox" OnServerValidate="NewPriceBeforeDiscountTotalVal" ValidationGroup="InvoiceItemNew"></asp:CustomValidator>
						    </FooterTemplate>
					    </asp:TemplateField>
                        <asp:TemplateField HeaderText="VAT code" SortExpression="VatCode" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
						    <EditItemTemplate>
							   <asp:DropDownList ID="EditVatCodeDropDownList" runat="server"></asp:DropDownList>							    
						    </EditItemTemplate>
						    <ItemTemplate>
							    <asp:Label ID="VatCodeLabel" runat="server" Text='<%# Bind("VatCode") %>'></asp:Label>
						    </ItemTemplate>
						    <FooterTemplate>
						        <asp:DropDownList ID="NewVatCodeDropDownList" runat="server"></asp:DropDownList>	
						    </FooterTemplate>
					    </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="EditLinkButton" CommandName="Edit"  runat="server" CausesValidation="False"><asp:Image ID="Image1" runat="server" ImageUrl="~/Gfx/icon-edit.png" Width="26" Height="21" AlternateText="Edit" /></asp:LinkButton>&nbsp;
                                <asp:LinkButton ID="DeleteLinkButton" CommandName="Delete" runat="server" CausesValidation="False"><asp:Image ID="Image2" runat="server" ImageUrl="~/Gfx/button-delete.gif" AlternateText="Delete" /></asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:LinkButton ID="UpdateLinkButton" runat="server" CommandName="Update" ValidationGroup="InvoiceItemUpdate"><asp:Image ID="Image3" runat="server" ImageUrl="~/Gfx/icon-save.png" Width="26" Height="21" AlternateText="Save" /></asp:LinkButton>&nbsp;
                                <asp:LinkButton ID="CancelLinkButton" runat="server" CommandName="Cancel" CausesValidation="false"><asp:Image ID="Image4" runat="server" ImageUrl="~/Gfx/icon-cancel.png" Width="26" Height="21" AlternateText="Cancel" /></asp:LinkButton>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:LinkButton ID="AddLinkButton" runat="server" CommandName="Add" ValidationGroup="InvoiceItemNew"><asp:Image ID="Image3" runat="server" ImageUrl="~/Gfx/icon-add.png" Width="26" Height="21" AlternateText="Add" /></asp:LinkButton>
                            </FooterTemplate>
                         </asp:TemplateField>
                       </Columns>
                    </asp:GridView>
					<asp:Label ID="InvoiceItemsMessageLabel" runat="server" Text="" Visible="false" ForeColor="red"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="TransfersPanel" runat="server" Width="600px">
        <table width="600">
            <tr>
                <td>
					<h2>Transfers</h2>
					<asp:GridView ID="InvoiceTransferGridView" runat="server" AutoGenerateColumns="False" 
						ShowFooter="True" OnRowCreated="InvoiceTransferGridView_RowCreated" OnRowCommand="InvoiceTransferGridView_RowCommand" 
						OnRowDataBound="InvoiceTransferGridView_RowDataBound" OnRowDeleting="InvoiceTransferGridView_RowDeleting" TabIndex="30"
						CssClass="dataGrid" AlternatingRowStyle-CssClass="dataGridAltItem" GridLines="None" BorderWidth="0" CellPadding="3"
						HeaderStyle-CssClass="dataGridHeader" SelectedRowStyle-CssClass="dataGridSelectedItem" AlternatingRowStyle-VerticalAlign="Top"
						RowStyle-VerticalAlign="Top">
                        <EmptyDataTemplate>
                            <asp:Label ID="EmptyDataLabel" runat="server" Text="No Transfers Applied"></asp:Label>
                        </EmptyDataTemplate>
                        <Columns>
							<asp:TemplateField HeaderText="Transfer&nbsp;K" SortExpression="TransferK">
								<ItemStyle HorizontalAlign="Left" />
								<ItemTemplate>
									<asp:Label ID="TransferKLabel" runat="server" Text='<%# Container.DataItem != null ? ((Bobs.InvoiceTransferDataHolder)(Container.DataItem)).TransferK.ToString() : "" %>'></asp:Label>
								</ItemTemplate>
								<HeaderStyle HorizontalAlign="Left" />
								<FooterTemplate>
									<asp:DropDownList ID="NewTransferKDropDownList" runat="server"></asp:DropDownList>
								</FooterTemplate>							        
							</asp:TemplateField>
							<asp:TemplateField HeaderText="Type" SortExpression="Type">
								<ItemStyle HorizontalAlign="Left" />
								<ItemTemplate>
									<asp:Label ID="TypeLabel" runat="server" Text='<%# Container.DataItem != null ? ((Bobs.InvoiceTransferDataHolder)(Container.DataItem)).Type.ToString() : "" %>'></asp:Label>
								</ItemTemplate>
								<HeaderStyle HorizontalAlign="Left" />
							</asp:TemplateField>
							<asp:TemplateField HeaderText="Method" SortExpression="Method">
								<ItemStyle HorizontalAlign="Left" />
								<ItemTemplate>
									<asp:Label ID="MethodLabel" runat="server" Text='<%# Container.DataItem != null ? Utilities.CamelCaseToString(((Bobs.InvoiceTransferDataHolder)(Container.DataItem)).Method.ToString()) : "" %>'></asp:Label>
								</ItemTemplate>
								<HeaderStyle HorizontalAlign="Left" />
							</asp:TemplateField>
							<asp:TemplateField HeaderText="Status" SortExpression="Status">
								<ItemStyle HorizontalAlign="Left" />
								<ItemTemplate>
									<asp:Label ID="StatusLabel" runat="server" Text='<%# Container.DataItem != null ? ((Bobs.InvoiceTransferDataHolder)(Container.DataItem)).Status.ToString() : "" %>'></asp:Label>
								</ItemTemplate>
								<HeaderStyle HorizontalAlign="Left" />
							</asp:TemplateField>
							<asp:TemplateField HeaderText="User" SortExpression="User">
								<ItemStyle HorizontalAlign="Left" />
								<ItemTemplate>
									<asp:Label ID="UserLabel" runat="server" Text='<%# Container.DataItem != null ? ((Bobs.InvoiceTransferDataHolder)(Container.DataItem)).UsrName : "" %>'></asp:Label>
								</ItemTemplate>
								<HeaderStyle HorizontalAlign="Left" />
							</asp:TemplateField>					        
							<asp:TemplateField HeaderText="Amount" SortExpression="Amount">
								<ItemStyle HorizontalAlign="Right" />
								<ItemTemplate>
									<asp:Label ID="AmountLabel" runat="server" Text='<%# Container.DataItem != null ? ((Bobs.InvoiceTransferDataHolder)(Container.DataItem)).Amount.ToString("c") : "" %>'></asp:Label>
								</ItemTemplate>
								<HeaderStyle HorizontalAlign="Center" />
								<FooterTemplate>
									<asp:TextBox ID="NewAmountTextBox" runat="server" Text='' Width="70"></asp:TextBox><asp:RangeValidator ID="RangeValidator1" runat="server" Display="Dynamic" ErrorMessage="* Positive amount only" ControlToValidate="NewAmountTextBox" Type="Currency" MinimumValue="0.01" MaximumValue="99999" ValidationGroup="TransferNew"></asp:RangeValidator>
								</FooterTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="View&nbsp;transfer" SortExpression="K">
								<ItemStyle HorizontalAlign="Left" />
								<ItemTemplate>
									<%# Container.DataItem != null ? ((Bobs.InvoiceTransferDataHolder)(Container.DataItem)).LinkAdminTransfer : ""%>
								</ItemTemplate>
								<HeaderStyle HorizontalAlign="Left" />
							</asp:TemplateField>
							<asp:TemplateField ShowHeader="False">
								<ItemTemplate>
									<asp:LinkButton ID="DeleteLinkButton" CommandName="Delete" runat="server" CausesValidation="False"><asp:Image ID="Image2" runat="server" ImageUrl="~/Gfx/button-delete.gif" AlternateText="Delete" /></asp:LinkButton>
								</ItemTemplate>
								<FooterTemplate>
									<asp:LinkButton ID="AddLinkButton" runat="server" CommandName="Add" ValidationGroup="TransferNew"><asp:Image ID="Image3" runat="server" ImageUrl="~/Gfx/icon-add.png" width="26" height="21" AlternateText="Add" /></asp:LinkButton>
								</FooterTemplate>
							 </asp:TemplateField>
							</Columns>
                    </asp:GridView>
                    <br />
                    <asp:HyperLink ID="CreateTransferHyperLink" runat="server" NavigateUrl="/admin/transferscreen/"
                        Target="_blank">Create transfer for this invoice</asp:HyperLink>
                    &nbsp;&nbsp;
                    <asp:HyperLink ID="SearchForTransferHyperLink" runat="server" NavigateUrl="/admin/adminmainaccounting/"
                        Target="_blank">Search for transfers</asp:HyperLink>
				</td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="CreditsPanel" runat="server">
		<table width="600">
            <tr>
                <td>
					<br />
                    <asp:Button ID="CreditInvoiceButton" runat="server" Text="Credit this invoice" Width="135px" OnClick="CreditInvoiceButton_Click" TabIndex="40" CausesValidation="False" />
                    &nbsp;
                    <asp:Button ID="RefundInvoiceButton" runat="server" Text="Refund This Invoice" Width="135px" OnClick="RefundInvoiceButton_Click" TabIndex="40" CausesValidation="False" Visible="false" Enabled="false"
                        onmouseover="stt('Invoice refund only available for invoices paid immediately by credit card and have not been credited.');" onmouseout="htm();"/>
                    <br />
					<h2>Credits</h2>
					<asp:GridView ID="InvoiceCreditGridView" runat="server" AutoGenerateColumns="False" TabIndex="50" GridLines="None" BorderWidth="0" CellPadding="3" CssClass="dataGrid" 
						AlternatingRowStyle-CssClass="dataGridAltItem" HeaderStyle-CssClass="dataGridHeader" SelectedRowStyle-CssClass="dataGridSelectedItem" AlternatingRowStyle-VerticalAlign="Top" RowStyle-VerticalAlign="Top">
                        <EmptyDataTemplate>
							<asp:Label ID="EmptyDataLabel" runat="server" Text="No Credits Applied"></asp:Label>
                        </EmptyDataTemplate>
						<Columns>
							<asp:BoundField DataField="CreditK" HeaderText="Credit&nbsp;K">
								<ItemStyle Width="40px" />
                                <HeaderStyle Width="70px" Wrap="False" />
							</asp:BoundField>
                            <asp:BoundField DataField="CreatedDateTime" HeaderText="Date" DataFormatString="{0:dd/MM/yy}" HtmlEncode="False" />
                            <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:c}" HtmlEncode="False"/>
                            <asp:HyperLinkField DataTextField="CreditK" HeaderText="View Credit" DataNavigateUrlFields="CreditK" DataNavigateUrlFormatString="/admin/creditscreen/K-{0}" DataTextFormatString="Credit&nbsp;#{0}" />                                
						</Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table width="600">
        <tr>
            <td colspan="2" align="left" style="width: 596px">
				<asp:CustomValidator ID="ProcessingVal" Runat="server" Display="None" EnableClientScript="False" ErrorMessage=""/><asp:CustomValidator ID="NoEditCustomVal" Runat="server" 
				Display="None" EnableClientScript="False" ErrorMessage=""/><asp:ValidationSummary id="InvoiceValidationSummary" Width="600" runat="server" Font-Bold="True" 
				CssClass="PaymentValidationSummary" HeaderText="There were some errors:" EnableClientScript="False" ShowSummary="True" DisplayMode="BulletList"></asp:ValidationSummary></td>
		</tr>
        <tr>
			<td>
				<table cellpadding="0" cellspacing="0" style="display:none;">
					<tr>
						<td runat="server" ID="OverrideEmailRecipientTD" style="display:none;">Email <asp:TextBox runat="server" ID="OverrideEmailRecipientTextBox" Width="150" onmouseover="stt('Email address (i.e. user@host.com).');" onmouseout="htm();">
							</asp:TextBox><asp:CustomValidator ID="EmailRecipientCustomValidator" runat="server" Display="None" ErrorMessage="<nobr>Invalid email address</nobr>" ControlToValidate="OverrideEmailRecipientTextBox" OnServerValidate="OverrideEmailAddressVal" ValidationGroup="EmailValidation"></asp:CustomValidator></td>
						<td><asp:CheckBox runat="server" ID="OverrideEmailRecipientsCheckBox" Text="All promoter admins" onselect="InvoiceScreenToggleOverrideEmailRecipients()" onclick="InvoiceScreenToggleOverrideEmailRecipients();" Checked="true"/></td>
					</tr>
				</table>
				<asp:Button ID="EmailButton" runat="server" Text="Email invoice" OnClick="EmailButton_Click" Visible="False" Width="150px"/><asp:Label runat="server" ID="EmailSentLabel" ForeColor="blue" Visible="false" EnableViewState="false"><nobr>Email sent</nobr></asp:Label><asp:Label runat="server" ID="EmailFailedLabel" ForeColor="red" Visible="false" EnableViewState="false"><nobr>Email failed</nobr></asp:Label></td>
			<td align="right" valign="bottom">
				<nobr><asp:Button ID="DownloadButton" runat="server" Text="Download" OnClick="DownloadButton_Click" TabIndex="89" Width="100px" CausesValidation="False"/>&nbsp; &nbsp;
                <asp:Button ID="SaveButton" runat="server" OnClick="SaveButton_Click" TabIndex="90" Text="Save" Width="100px" />
                &nbsp; &nbsp;<asp:Button ID="CancelButton" runat="server" OnClick="CancelButton_Click" TabIndex="95" Text="Cancel" Width="100px" CausesValidation="False" /></nobr></td>
        </tr>
    </table>
</div>
<script language="JavaScript">
	// run this script after the page has loaded
	InvoiceScreenToggleOverrideDueDate();
	InvoiceScreenToggleOverrideTaxDate();
//	InvoiceScreenToggleOverrideEmailRecipients();
</script>