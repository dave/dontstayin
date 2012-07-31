<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BankExport.ascx.cs" Inherits="Spotted.Admin.BankExport" %>
<%@ Register TagPrefix="DbCombo" Namespace="Cambro.Web.DbCombo" Assembly="Cambro.Web.DbCombo" %>
<script language="javascript">
function SelectAllCheckBoxes(gridView, spanChk)
{

	var oItem = spanChk.children;

	var theBox=(spanChk.type=="checkbox")?spanChk:spanChk.children.item[0];

	xState=theBox.checked;
	elm=theBox.form.elements;

	for(i=0;i<elm.length;i++)
	{
		if(elm[i].type=="checkbox" && elm[i].id!=theBox.id && elm[i].id.indexOf(gridView.id) >= 0)
		{
			if(elm[i].checked!=xState)
				elm[i].checked=xState;
		}
	}
}

function SetAllCheckBox(gridView, spanChk)
{
	var oItem = spanChk.children;
	var theBox = (spanChk.type=="checkbox") ? spanChk : spanChk.children.item[0];
	var isAllChecked = true;
	elm=theBox.form.elements;

	for(i=0;i<elm.length;i++)
	{
		if(elm[i].type=="checkbox" && elm[i].id!=theBox.id && elm[i].id.indexOf(gridView.id) >= 0)
		{
			if(elm[i].checked == false)
			{
				isAllChecked = false;
				break;
			}
		}
	}
	spanChk.checked = isAllChecked;
}

</script>
<dsi:h1 runat="server" id="H1">Export to bank</dsi:h1>
<asp:Panel ID="SearchBankExportPanel" runat="server">
	<div class="ContentBorder">
		<p>
			<asp:RadioButtonList ID="BankExportRadioButtonList" runat="server" RepeatDirection="Horizontal" RepeatColumns="3" RepeatLayout="Table" CellPadding="3" AutoPostBack="true" OnSelectedIndexChanged="BankExportRadioButtonList_SelectedIndexChanged" Font-Size="XX-Large" Font-Bold="true" style="border-bottom:dotted 1px #000000; font-size:xx-large;" >
			</asp:RadioButtonList>
			<h2 id="SearchBankExportHeader" runat="server">Next batch! Please review and verify details before exporting to bank.</h2>
			<p id="BankExportLinkP" runat="server" visible="false">Copy and paste link below to generate the next batch export for the bank.<br /><asp:Label ID="BankExportGeneratorLinkLabel" runat="server" Font-Bold="true" onclick="setTimeout('window.location.reload( false )', 6000)"></asp:Label></p>
			
			
			<table id="SearchCriteriaTable" runat="server" cellpadding="3" cellspacing="0" border="0" visible="false">
				<tr>
					<td>Status:</td>
					<td><asp:DropDownList ID="StatusDropDownList" runat="server"></asp:DropDownList></td>
					<td>Type:</td>
					<td><asp:DropDownList ID="TypeDropDownList" runat="server"></asp:DropDownList></td>
					<td>Export date:</td>
					<td>
						<dsi:Cal ID="ExportDateCal" runat="server" TabIndex="18"></dsi:Cal>
					</td>
				</tr>
				<tr>
					<td>Batch ref:</td>
					<td><asp:TextBox ID="BatchRefTextBox" runat="server"></asp:TextBox></td>
					<td>Promoter:</td>
					<td>
						<js:HtmlAutoComplete runat="server" ID="uiPromoterHtmlAutoComplete" WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetPromotersWithK" Width="200px" />
					</td>
					<td><button id="SearchButton" runat="server" onserverclick="SearchButton_Click">Search</button></td>
					<td><button id="ClearButton" runat="server" onserverclick="ClearButton_Click">Clear</button></td>
				</tr>
			</table>
			<asp:GridView ID="SearchBankExportGridView" runat="server" CssClass="dataGrid" AutoGenerateColumns="false" EnableViewState="true" ShowFooter="true" ShowHeader="true" AllowPaging="false" PageSize="20"
				AlternatingRowStyle-CssClass="dataGridAltItem" GridLines="None" BorderWidth="0" CellPadding="3" HeaderStyle-CssClass="dataGridHeader" OnRowDataBound="SearchBankExportGridView_RowDataBound" OnDataBound="SearchBankExportGridView_DataBound"
				SelectedRowStyle-CssClass="dataGridSelectedItem" AlternatingRowStyle-VerticalAlign="Top" RowStyle-VerticalAlign="Top" FooterStyle-CssClass="dataGridFooter" OnPageIndexChanging="SearchBankExportGrivView_PageIndexChanging">
				<Columns>
					<asp:TemplateField HeaderText="<nobr>Payment ref</nobr>">
						<ItemTemplate>
							<asp:TextBox ID="BankExportKTextBox" runat="server" Visible="false" Text="<%# ((Bobs.BankExport)(Container.DataItem)).K.ToString()%>"></asp:TextBox>
							<nobr><%#((Bobs.BankExport)(Container.DataItem)).PaymentRef%></nobr>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Added">
						<ItemTemplate>
							<nobr><%#((Bobs.BankExport)(Container.DataItem)).AddedDateTime.ToString("dd/MM/yy HH:mm")%></nobr>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Exported">
						<ItemTemplate>
							<nobr><%#((Bobs.BankExport)(Container.DataItem)).OutputDateTime > DateTime.MinValue ? ((Bobs.BankExport)(Container.DataItem)).OutputDateTime.ToString("dd/MM/yy HH:mm") : "&nbsp;"%></nobr>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Processed">
						<ItemTemplate>
							<nobr><%#((Bobs.BankExport)(Container.DataItem)).ProcessingDateTime > DateTime.MinValue ? ((Bobs.BankExport)(Container.DataItem)).ProcessingDateTime.ToString("dd/MM/yy HH:mm") : "&nbsp;"%></nobr>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Right">
						<ItemTemplate>
							<nobr><%#((Bobs.BankExport)(Container.DataItem)).Amount.ToString("c")%></nobr>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Status">
						<ItemTemplate>
							<nobr><%# Utilities.CamelCaseToString(((Bobs.BankExport)(Container.DataItem)).Status.ToString())%></nobr>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Type">
						<ItemTemplate>
							<nobr><%# Utilities.CamelCaseToString(((Bobs.BankExport)(Container.DataItem)).Type.ToString())%></nobr>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Beneficiary">
						<ItemTemplate>
							<%#((Bobs.BankExport)(Container.DataItem)).BeneficiaryLink%>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Transfer">
						<ItemTemplate>
							<nobr><%#((Bobs.BankExport)(Container.DataItem)).Transfer != null ? ((Bobs.BankExport)(Container.DataItem)).Transfer.AdminLinkNewWindow() : ""%></nobr>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="<nobr>Sort code</nobr>">
						<ItemTemplate>
							<%#((Bobs.BankExport)(Container.DataItem)).BankAccountSortCode%>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="<nobr>Account #</nobr>">
						<ItemTemplate>
							<%#((Bobs.BankExport)(Container.DataItem)).BankAccountNumber%>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="<nobr>Batch ref</nobr>">
						<ItemTemplate>
							<nobr><%#((Bobs.BankExport)(Container.DataItem)).BatchRef%></nobr>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
						<HeaderTemplate>
							<asp:CheckBox ID="BankExportSelectAllCheckBox" runat="server" style="margin-left:1px;"/>
						</HeaderTemplate>
						<ItemTemplate>
							<asp:CheckBox ID="BankExportSelectCheckBox" runat="server"/>
						</ItemTemplate>
						<FooterTemplate>
							<button id="UpdateStatusButton" runat="server" onserverclick="UpdateSearchGridViewStatusButton_Click">Update</button><asp:DropDownList id="BankExportStatusDropDownList" runat="server"></asp:DropDownList>
						</FooterTemplate>
					</asp:TemplateField>
				</Columns>
			</asp:GridView>
			<asp:Label ID="SearchResultsMessageLabel" runat="server" Font-Italic="True" Visible="false"></asp:Label>			
		</p>
		<br />
		<table id="SearchSummaryTable" runat="server" style="font-weight:bold;">
			<tr>
				<td>Current account to promoter:</td>
				<td style="text-align:right;"><asp:Label ID="FundsCurrentToPromoterLabel" runat="server"></asp:Label></td>
			</tr>
		</table>
	</div>
</asp:Panel>

