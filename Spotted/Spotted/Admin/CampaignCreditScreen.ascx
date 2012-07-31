<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CampaignCreditScreen.ascx.cs" Inherits="Spotted.Admin.CampaignCreditScreen" %>
<%@ Register TagPrefix="DbCombo" Namespace="Cambro.Web.DbCombo" Assembly="Cambro.Web.DbCombo" %>
<%@ Register TagPrefix="DSIControls" TagName="AddOnlyTextBox" Src="/Controls/AddOnlyTextBox.ascx" %>
<dsi:h1 runat="server" id="H1_1">Campaign Credit</dsi:h1>
<div class="ContentBorder">
    <asp:Panel ID="MainPanel" runat="server" Width="600px">
        <table width="600" border="0" cellpadding="3" cellspacing="0">
            <tr>
				<td><nobr>Campaign Credit K</nobr></td>
				<td><nobr><asp:Label ID="CampaignCreditKLabel" runat="server"></asp:Label></nobr></td>
				
            </tr>
             <tr>
				<td><nobr>Action date</nobr></td>
				<td><nobr><asp:Label ID="ActionDateTimeLabel" runat="server"></asp:Label></nobr></td>
            </tr>
            <tr>
				<td><nobr>Promoter</nobr></td>
				<td>
				<js:HtmlAutoComplete Width="168px" ID="uiPromotersAutoComplete" runat="server"  WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetPromotersWithK" AutoPostBack="true" TabIndex="1"/>
                    <asp:Label ID="PromoterValueLabel" runat="server"></asp:Label>
					<asp:Label ID="PromoterCampaignCreditsLabel" runat="server"></asp:Label><asp:RequiredFieldValidator ID="PromoterRequiredFieldValidator" runat="server" ControlToValidate="uiPromotersAutoComplete"
                        ErrorMessage="Must select promoter" Display="None"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
				<td><nobr>User</nobr></td>
				<td><asp:DropDownList ID="UsrDropDownList" runat="server"></asp:DropDownList>
                    <asp:Label ID="UsrValueLabel" runat="server"></asp:Label></td>
            </tr>
            <tr>
				<td><nobr>Action user</nobr></td>
				<td><js:HtmlAutoComplete ID="uiActionUserAutoComplete" runat="server" WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetUsersWithK"/>
					<asp:Label ID="ActionUsrValueLabel" runat="server"></asp:Label><asp:RequiredFieldValidator ID="ActionUsrRequiredFieldValidator" runat="server" ControlToValidate="uiActionUserAutoComplete"
                        ErrorMessage="Must select action user" Display="None"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
				<td><nobr>Buyable Object</nobr></td>
				<td><nobr><asp:DropDownList ID="BuyableObjectTypeDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="BuyableObjectTypeDropDownList_SelectedItemChanged"></asp:DropDownList><asp:Label ID="BuyableObjectTypeValueLabel" runat="server"></asp:Label></nobr></td>
            </tr>
            <tr id="BuyableObjectKRow" runat="server">
				<td><nobr>Buyable Object K</nobr></td>
				<td><nobr><asp:TextBox ID="BuyableObjectKTextBox" runat="server" Width="45"></asp:TextBox></nobr>
					<asp:CustomValidator ID="BuyableObjectCustomValidator" runat="server" Display="None" ErrorMessage="Buyable object does not exist in the database" ControlToValidate="BuyableObjectKTextBox" OnServerValidate="BuyableObjectVal"></asp:CustomValidator></td>
            </tr>
            <tr>
				<td><nobr>Invoice Item Type</nobr></td>
				<td><nobr><asp:DropDownList ID="InvoiceItemTypeDropDownList" runat="server"></asp:DropDownList><asp:Label ID="InvoiceItemTypeValueLabel" runat="server"></asp:Label></nobr></td>
            </tr>
            <tr>
				<td><nobr>Credits</nobr></td>
				<td><nobr><asp:TextBox ID="CreditsTextBox" runat="server" Width="45"></asp:TextBox></nobr><asp:RequiredFieldValidator ID="CreditsRequiredFieldValidator" runat="server" ControlToValidate="CreditsTextBox"
                        ErrorMessage="Must enter number of credits" Display="None"></asp:RequiredFieldValidator><asp:RangeValidator ID="CreditsRangeValidator" runat="server" Display="Dynamic" ErrorMessage=" * Credit must be a number" ControlToValidate="CreditsTextBox" Type="Integer" MinimumValue="-500000" MaximumValue="500000"></asp:RangeValidator></td>
            </tr>
            <tr>
				<td><nobr>Description</nobr></td>
				<td><nobr><asp:TextBox ID="DescriptionTextBox" runat="server" Width="520"></asp:TextBox></nobr></td>
            </tr>
            <tr>
				<td valign="top"><nobr>Notes</nobr></td>
				<td valign="top"><nobr><DSIControls:AddOnlyTextBox id="NotesAddOnlyTextBox" runat="server" TabIndex="20" CssClass="notesAddOnly"></DSIControls:AddOnlyTextBox></nobr></td>
            </tr>            
            <tr>
				<td colspan="2"><asp:ValidationSummary id="CampaignCreditValidationSummary" Width="600" runat="server" Font-Bold="True" 
					CssClass="PaymentValidationSummary" HeaderText="There were some errors:" EnableClientScript="False" ShowSummary="True" DisplayMode="BulletList"></asp:ValidationSummary></td>
            </tr>
            <tr>
				<td colspan="2"><button id="SaveButton" runat="server" style="padding-left:4px; padding-right:4px;" onserverclick="SaveButton_Click">Save</button><button id="CancelButton" runat="server" style="margin-left:10px;" onserverclick="CancelButton_Click" causesvalidation="false">Cancel</button></td>
            </tr>            
        </table>
    </asp:Panel>
</div>
