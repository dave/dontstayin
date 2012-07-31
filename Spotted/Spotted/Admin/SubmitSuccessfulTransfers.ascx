<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SubmitSuccessfulTransfers.ascx.cs" Inherits="Spotted.Admin.SubmitSuccessfulTransfers" %>
<dsi:h1 runat="server" id="H1">Submit Successful Transfers</dsi:h1>
<div class="ContentBorder">
<asp:Panel ID="SuccessfulTransferPanel" runat="server" Width="600px">
<table width="600px">
<tr>
    <td valign="top"><asp:Label ID="ErrorLabel" runat="server" ForeColor="Red" Visible="False" Font-Bold="True"></asp:Label>
        <br />
        <asp:GridView ID="SuccessfulTransferGridView" runat="server" Width="600px" AutoGenerateColumns="False" ShowFooter="True" OnRowCommand="SuccessfulTransferGridView_RowCommand" 
            OnRowCreated="SuccessfulTransferGridView_RowCreated" OnRowDataBound="SuccessfulTransferGridView_RowDataBound" OnRowDeleting="SuccessfulTransferGridView_RowDeleting" 
            OnRowEditing="SuccessfulTransferGridView_RowEditing" OnRowUpdating="SuccessfulTransferGridView_RowUpdating" OnRowCancelingEdit="SuccessfulTransferGridView_RowCancelingEdit"
            CssClass="dataGrid" AlternatingRowStyle-CssClass="dataGridAltItem" GridLines="None" BorderWidth="0" CellPadding="3"
            HeaderStyle-CssClass="dataGridHeader" SelectedRowStyle-CssClass="dataGridSelectedItem" AlternatingRowStyle-VerticalAlign="Top" RowStyle-VerticalAlign="Top">
                                    <Columns>
                            <asp:TemplateField HeaderText="Result" SortExpression="" Visible="false">
						        <ItemStyle HorizontalAlign="Left" />
						         <EditItemTemplate>                                    
						        </EditItemTemplate>
						        <ItemTemplate>
                                    <asp:Image ID="SuccessImage" runat="server" ImageUrl="/gfx/icon-tick-up.png" Visible="false"/>
                                    <asp:Image ID="FailedImage" runat="server" ImageUrl="/gfx/icon-cross-up.png" Visible="false"/>							        
						        </ItemTemplate>
						        <HeaderStyle HorizontalAlign="Center" />
						        <FooterTemplate>                                
						        </FooterTemplate>
					        </asp:TemplateField>
                            <asp:TemplateField HeaderText="Transfer K" SortExpression="K">
						        <ItemStyle HorizontalAlign="Left" />
						         <EditItemTemplate>
                                    <asp:TextBox ID="EditTransferKTextBox" runat="server" Text='<%# Bind("K") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="TransferEdit" runat="server" ControlToValidate="EditTransferKTextBox" Display="Dynamic" ErrorMessage="* Must not be empty"></asp:RequiredFieldValidator>
						        </EditItemTemplate>
						        <ItemTemplate>
							        <asp:Label ID="TransferKLabel" runat="server" Text='<%# Bind("K") %>'></asp:Label>
						        </ItemTemplate>
						        <HeaderStyle HorizontalAlign="Center" />
						        <FooterTemplate>
                                    <asp:TextBox ID="NewTransferKTextBox" runat="server" Text=''></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="TransferNew" runat="server" ControlToValidate="NewTransferKTextBox" Display="Dynamic" ErrorMessage="* Must not be empty"></asp:RequiredFieldValidator>
						        </FooterTemplate>
					        </asp:TemplateField>
					        <asp:TemplateField HeaderText="Amount" SortExpression="Amount">
						        <ItemStyle HorizontalAlign="Right" />
						         <EditItemTemplate>
                                    <asp:TextBox ID="EditAmountTextBox" runat="server" Text='<%# Bind("Amount") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="TransferEdit" runat="server" ControlToValidate="EditAmountTextBox" Display="Dynamic" ErrorMessage="* Must not be empty"></asp:RequiredFieldValidator>
						        </EditItemTemplate>
						        <ItemTemplate>
							        <asp:Label ID="AmountLabel" runat="server" Text='<%# Bind("Amount", "{0:&#163;0.00}") %>'></asp:Label>
						        </ItemTemplate>
						        <HeaderStyle HorizontalAlign="Center" />
						        <FooterTemplate>
						            <asp:TextBox ID="NewAmountTextBox" runat="server" Text=''></asp:TextBox>
						            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="TransferNew" runat="server" ControlToValidate="NewAmountTextBox" Display="Dynamic" ErrorMessage="* Must not be empty"></asp:RequiredFieldValidator>
						        </FooterTemplate>
					        </asp:TemplateField>
					        <asp:TemplateField HeaderText="Reference #">
						        <ItemStyle HorizontalAlign="Right" />
						         <EditItemTemplate>
                                    <asp:TextBox ID="EditReferenceNumberTextBox" runat="server" Text='<%# Bind("ReferenceNumber") %>'></asp:TextBox>
						        </EditItemTemplate>
						        <ItemTemplate>
							        <asp:Label ID="ReferenceNumberLabel" runat="server" Text='<%# Bind("ReferenceNumber") %>'></asp:Label>
						        </ItemTemplate>
						        <HeaderStyle HorizontalAlign="Center" />
						        <FooterTemplate>
						            <asp:TextBox ID="NewReferenceNumberTextBox" runat="server" Text=''></asp:TextBox>
						        </FooterTemplate>
					        </asp:TemplateField>
					        <asp:TemplateField ShowHeader="False">

                            <ItemTemplate>
                                <asp:LinkButton ID="EditLinkButton" CommandName="Edit"  runat="server" CausesValidation="False"><asp:Image ID="Image1" runat="server" ImageUrl="~/Gfx/icon-edit.png" Width="26" Height="21" AlternateText="Edit" /></asp:LinkButton>&nbsp;
                                <asp:LinkButton ID="DeleteLinkButton" CommandName="Delete" runat="server" CausesValidation="False"><asp:Image ID="Image2" runat="server" ImageUrl="~/Gfx/button-delete.gif" AlternateText="Delete" /></asp:LinkButton>
                                
                            </ItemTemplate>

                            <EditItemTemplate>
                                <asp:LinkButton ID="UpdateLinkButton" runat="server" CommandName="Update" ValidationGroup="InvoiceItemUpdate"><asp:Image ID="Image3" runat="server" ImageUrl="~/Gfx/icon-save.png" Width="26" Height="21" AlternateText="Save" /></asp:LinkButton>
                                <asp:LinkButton ID="CancelLinkButton" runat="server" CommandName="Cancel" CausesValidation="false"><asp:Image ID="Image4" runat="server" ImageUrl="~/Gfx/icon-cancel.png" Width="26" Height="21" AlternateText="Cancel" /></asp:LinkButton>
                            </EditItemTemplate>

                            <FooterTemplate>
                                <asp:LinkButton ID="AddLinkButton" runat="server" CommandName="Add" ValidationGroup="InvoiceItemNew"><asp:Image ID="Image3" runat="server" ImageUrl="~/Gfx/icon-add.png" width="26" height="21" AlternateText="Add" /></asp:LinkButton>
                            </FooterTemplate>

                         </asp:TemplateField>
                            </Columns>
        </asp:GridView></td>

    </tr><tr>
    <td align="right">
        <asp:Button ID="SaveButton" runat="server" Text="SAVE" Font-Bold="True" Width="100px" OnClick="SaveButton_Click" />&nbsp;&nbsp;<asp:Button ID="CancelButton"
            runat="server" Text="CANCEL" Font-Bold="True" Width="100px" CausesValidation="False" OnClick="CancelButton_Click" /></td>
</tr>
</table>
</asp:Panel>
</div>
