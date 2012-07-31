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
									<asp:TextBox ID="EditPriceTextBox" runat="server" Width="70" Text='' onmouseover="stt('Price in £GBP.');" onmouseout="htm();"></asp:TextBox>
									<asp:CustomValidator ID="CustomValidator5" runat="server" ErrorMessage="* Invalid price" Display="Dynamic" ControlToValidate="EditPriceTextBox" OnServerValidate="MoneyTextBoxVal" ValidationGroup="CreditItemUpdate" Enabled="false"></asp:CustomValidator>
								</EditItemTemplate>
								<ItemStyle HorizontalAlign="Right" />
								<ItemTemplate>
									<nobr><asp:Label ID="PriceLabel" runat="server" Width="70" Text='<%# Bind("Price", "{0:c}") %>'></asp:Label></nobr>
								</ItemTemplate>
								<HeaderStyle HorizontalAlign="Center" />
								 <FooterTemplate>
									<asp:TextBox ID="NewPriceTextBox" runat="server" Width="70" Text='' onmouseover="stt('Price in £GBP.');" onmouseout="htm();"></asp:TextBox>
									<asp:CustomValidator ID="CustomValidator6" runat="server" ErrorMessage="* Invalid price" Display="Dynamic" ControlToValidate="NewPriceTextBox" OnServerValidate="MoneyTextBoxVal" ValidationGroup="CreditItemNew" Enabled="false"></asp:CustomValidator>
								</FooterTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="VAT" SortExpression="Vat">
								<EditItemTemplate>
									<asp:TextBox ID="EditVatTextBox" runat="server" Visible="false" Width="60" Text='<%# Bind("Vat") %>' ReadOnly="true"></asp:TextBox>
								    
								</EditItemTemplate>
								<ItemStyle HorizontalAlign="Right" />
								<ItemTemplate>
									<nobr><asp:Label ID="VatLabel" runat="server" Width="60" Text='<%# Bind("Vat", "{0:c}") %>'></asp:Label></nobr>
								</ItemTemplate>
								<HeaderStyle HorizontalAlign="Center" />
								<FooterTemplate>
									<asp:TextBox ID="NewVatTextBox" runat="server" Visible="false" Width="60" Text='' ReadOnly="true" TabIndex="-1"></asp:TextBox>						        
								</FooterTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="Total" SortExpression="Total">
								<EditItemTemplate>
									<asp:TextBox ID="EditTotalTextBox" runat="server" Width="70" Text='<%# Bind("Total") %>' onmouseover="stt('Total Price incl. VAT in £GBP.');" onmouseout="htm();"></asp:TextBox>							    
									<asp:CustomValidator ID="CustomValidator8a" runat="server" ErrorMessage="* Invalid total" Display="Dynamic" ControlToValidate="EditTotalTextBox" OnServerValidate="MoneyTextBoxVal" ValidationGroup="CreditItemUpdate" Enabled="false"></asp:CustomValidator>
									<asp:CustomValidator ID="CustomValidator8" runat="server" ErrorMessage="* Must enter price or total" Display="Dynamic" ControlToValidate="EditTotalTextBox" OnServerValidate="EditPriceTotalVal" ValidationGroup="CreditItemUpdate"></asp:CustomValidator>
								</EditItemTemplate>
								<ItemStyle HorizontalAlign="Right" />
								<ItemTemplate>
									<nobr><asp:Label ID="TotalLabel" runat="server" Width="70" Text='<%# Bind("Total", "{0:c}") %>'></asp:Label></nobr>
								</ItemTemplate>
								<HeaderStyle HorizontalAlign="Center" />
								<FooterTemplate>
									<asp:TextBox ID="NewTotalTextBox" runat="server" Width="70" Text='' onmouseover="stt('Total Price incl. VAT in £GBP.');" onmouseout="htm();"></asp:TextBox>
									<asp:CustomValidator ID="CustomValidator9" runat="server" ErrorMessage="* Invalid total" Display="Dynamic" ControlToValidate="NewTotalTextBox" OnServerValidate="MoneyTextBoxVal" ValidationGroup="CreditItemNew" Enabled="false"></asp:CustomValidator>
									<asp:CustomValidator ID="CustomValidator10" runat="server" ErrorMessage="* Must enter price or total" Display="Dynamic" ControlToValidate="NewTotalTextBox" OnServerValidate="NewPriceTotalVal" ValidationGroup="CreditItemNew"></asp:CustomValidator>
								</FooterTemplate>
							</asp:TemplateField>
						    
							<asp:TemplateField HeaderText="VAT code" SortExpression="VatCode">
								<EditItemTemplate>
								   <asp:DropDownList ID="EditVatCodeDropDownList" runat="server">
									</asp:DropDownList>							    
								</EditItemTemplate>
								<ItemStyle HorizontalAlign="Center" />
								<ItemTemplate>
									<asp:Label ID="VatCodeLabel" runat="server" Text='<%# Bind("VatCode") %>'></asp:Label>
								</ItemTemplate>
								<HeaderStyle HorizontalAlign="Center" />
								<FooterTemplate>
									<asp:DropDownList ID="NewVatCodeDropDownList" runat="server">
									  </asp:DropDownList>	
								</FooterTemplate>
							</asp:TemplateField>
							<asp:TemplateField ShowHeader="False" HeaderText="Update">
								<ItemTemplate>
									<asp:LinkButton ID="EditLinkButton" CommandName="Edit"  runat="server" CausesValidation="False"><asp:Image ID="Image1" runat="server" ImageUrl="~/Gfx/icon-edit.png" Width="26" Height="21" AlternateText="Edit" /></asp:LinkButton>&nbsp;
									<asp:LinkButton ID="DeleteLinkButton" CommandName="Delete" runat="server" CausesValidation="False"><asp:Image ID="Image2" runat="server" ImageUrl="~/Gfx/button-delete.gif" AlternateText="Delete" /></asp:LinkButton>
								</ItemTemplate>
								<EditItemTemplate>
									<asp:LinkButton ID="UpdateLinkButton" runat="server" CommandName="Update" ValidationGroup="CreditItemUpdate"><asp:Image ID="Image3" runat="server" ImageUrl="~/Gfx/icon-save.png" AlternateText="Save" Width="26" Height="21" /></asp:LinkButton>&nbsp;
									<asp:LinkButton ID="CancelLinkButton" runat="server" CommandName="Cancel" CausesValidation="false"><asp:Image ID="Image4" runat="server" ImageUrl="~/Gfx/icon-cancel.png" AlternateText="Cancel" Width="26" Height="21" /></asp:LinkButton>
								</EditItemTemplate>
								<FooterTemplate>
									<asp:LinkButton ID="AddLinkButton" runat="server" CommandName="Add" ValidationGroup="CreditItemNew"><asp:Image ID="Image3" runat="server" ImageUrl="~/Gfx/icon-add.png" Width="26" Height="21" AlternateText="Add" /></asp:LinkButton>
								</FooterTemplate>
							 </asp:TemplateField>
                       </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="TransfersPanel" runat="server" Width="600px" Visible="false">
        <table width="600">
            <tr>
                <td colspan="5"><h2>Transfers</h2><asp:GridView ID="CreditTransferGridView" runat="server" AutoGenerateColumns="False" 
                        ShowFooter="True" OnRowCreated="CreditTransferGridView_RowCreated" OnRowCommand="CreditTransferGridView_RowCommand" 
                        OnRowDataBound="CreditTransferGridView_RowDataBound" OnRowDeleting="CreditTransferGridView_RowDeleting" TabIndex="30"
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
							        <asp:Label ID="TransferKLabel" runat="server" Text='<%# Bind("TransferK") %>'></asp:Label>
						        </ItemTemplate>
						        <HeaderStyle HorizontalAlign="Left" />
						        <FooterTemplate>
                                    <asp:DropDownList ID="NewTransferKDropDownList" runat="server">
                                    </asp:DropDownList>
						        </FooterTemplate>
						        
					        </asp:TemplateField>

                             <asp:TemplateField HeaderText="Type" SortExpression="Type">
						        <ItemStyle HorizontalAlign="Left" />
						        <ItemTemplate>
							        <asp:Label ID="TypeLabel" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
						        </ItemTemplate>
						        <HeaderStyle HorizontalAlign="Left" />

					        </asp:TemplateField>
					        <asp:TemplateField HeaderText="Method" SortExpression="Method">
						        <ItemStyle HorizontalAlign="Left" />
						        <ItemTemplate>
							        <asp:Label ID="MethodLabel" runat="server" Text='<%# Bind("Method") %>'></asp:Label>
						        </ItemTemplate>
						        <HeaderStyle HorizontalAlign="Left" />

					        </asp:TemplateField>
					        <asp:TemplateField HeaderText="Status" SortExpression="Status">
						        <ItemStyle HorizontalAlign="Left" />
						        <ItemTemplate>
							        <asp:Label ID="StatusLabel" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
						        </ItemTemplate>
						        <HeaderStyle HorizontalAlign="Left" />

					        </asp:TemplateField>
					        <asp:TemplateField HeaderText="User" SortExpression="User">
						        <ItemStyle HorizontalAlign="Left" />
						        <ItemTemplate>
							        <asp:Label ID="UserLabel" runat="server" Text='<%# Bind("UsrName") %>'></asp:Label>
						        </ItemTemplate>
						        <HeaderStyle HorizontalAlign="Left" />

					        </asp:TemplateField>
					        
					        <asp:TemplateField HeaderText="Amount" SortExpression="Amount">
						        <ItemStyle HorizontalAlign="Right" />
						        <ItemTemplate>
							        <asp:Label ID="AmountLabel" runat="server" Text='<%# Bind("Amount", "{0:c}") %>'></asp:Label>
						        </ItemTemplate>
						        <HeaderStyle HorizontalAlign="Center" />
						        <FooterTemplate>
						            <asp:TextBox ID="NewAmountTextBox" runat="server" Text='' Width="70"></asp:TextBox><asp:RangeValidator ID="RangeValidator1" runat="server" Display="Dynamic" ErrorMessage="* Positive amount only" ControlToValidate="NewAmountTextBox" Type="Currency" MinimumValue="-99999" MaximumValue="-0.01" ValidationGroup="TransferNew"></asp:RangeValidator>
						        </FooterTemplate>
					        </asp:TemplateField>
					        <asp:TemplateField HeaderText="View&nbsp;transfer" SortExpression="TransferK">
						        <ItemStyle HorizontalAlign="Left" />
						        <ItemTemplate>
                                    <asp:HyperLink ID="TransferHyperLink" runat="server" Text='<%# Bind("TransferK", "Transfer&nbsp;{0}") %>' NavigateUrl='<%# Bind("TransferK", "/admin/transferscreen/K-{0}") %>'></asp:HyperLink>
						        </ItemTemplate>
						        <HeaderStyle HorizontalAlign="Left" />

					        </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">

                            <ItemTemplate>
                                <asp:LinkButton ID="DeleteLinkButton" CommandName="Delete" runat="server" CausesValidation="False"><asp:Image ID="Image2" runat="server" ImageUrl="~/Gfx/button-delete.gif" AlternateText="Delete" /></asp:LinkButton>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:LinkButton ID="AddLinkButton" runat="server" CommandName="Add" ValidationGroup="TransferNew"><asp:Image ID="Image3" runat="server" ImageUrl="~/Gfx/icon-add.png" Width="26" Height="21" AlternateText="Add" /></asp:LinkButton>
                            </FooterTemplate>

                         </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    <br />
					<asp:HyperLink ID="CreateTransferHyperLink" runat="server" NavigateUrl="/admin/transferscreen/"
						Target="_blank">Create transfer for this credit</asp:HyperLink>
					&nbsp;&nbsp;
					<asp:HyperLink ID="SearchForTransferHyperLink" runat="server" NavigateUrl="/admin/adminmainaccounting/"
						Target="_blank">Search for transfers</asp:HyperLink></td>
            </tr>
        </table>
    </asp:Panel>
    <table width="600">
        <tr>
            <td align="left" style="width: 596px">
                <asp:CustomValidator ID="ProcessingVal" Runat="server" Display="None" EnableClientScript="False" ErrorMessage=""/><asp:ValidationSummary ID="CreditValidationSummary" runat="server" CssClass="PaymentValidationSummary"
                    DisplayMode="BulletList" EnableClientScript="False" Font-Bold="True" HeaderText="There were some errors:" ShowSummary="True" Width="600" /></td>
        </tr>
		<tr>
			<td align="right" style="width: 596px"><asp:Button ID="DownloadButton" runat="server" Text="Download" OnClick="DownloadButton_Click" TabIndex="89" Width="100px" CausesValidation="False"/>&nbsp; &nbsp;<asp:Button ID="SaveButton" runat="server" Font-Bold="False" OnClick="SaveButton_Click" TabIndex="90" Text="Save" Width="100px" />
                &nbsp; &nbsp;<asp:Button ID="CancelButton" runat="server" Font-Bold="False" OnClick="CancelButton_Click" TabIndex="95" Text="Cancel" Width="100px" /></td>
        </tr>
    </table>
</div>
<script language="JavaScript">
	// run this script after the page has loaded
	CreditScreenToggleOverrideTaxDate();
</script>