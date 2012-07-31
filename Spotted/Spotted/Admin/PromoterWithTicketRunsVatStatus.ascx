<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PromoterWithTicketRunsVatStatus.ascx.cs" Inherits="Spotted.Admin.PromoterWithTicketRunsVatStatus" %>
<asp:Panel Runat="server" ID="PromoterWithTicketRunsVatStatusPanel">
	<div class="ContentBorder">
		<p>
			<table cellpadding="3" cellspacing="0" border="0">
				<tr>
					<td><nobr>VAT Status</nobr></td>
					<td><asp:DropDownList ID="VatStatusDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="VatStatusDropDownList_SelectedIndexChanged"></asp:DropDownList></td>
					<td style="padding-left:20px;"><button id="SendReminderEmailForUnknownVatStatusPromotersButton" runat="server" onserverclick="SendReminderEmailForUnknownVatStatusPromotersButton_Click" disabled="disabled">Email reminder to promoter's with VAT status Unknown</button></td>
				</tr>
			</table>
		</p>
		<asp:GridView ID="UnknownPromoterVatStatusGridView" runat="server" AllowPaging="True" AutoGenerateColumns="False" PageSize="25" OnPageIndexChanging="UnknownPromoterVatStatusGridView_PageIndexChanging"
			CssClass="dataGrid" EnableViewState="true" AlternatingRowStyle-CssClass="dataGridAltItem" GridLines="None" BorderWidth="0" CellPadding="3" HeaderStyle-CssClass="dataGridHeader"
			SelectedRowStyle-CssClass="dataGridSelectedItem" AlternatingRowStyle-VerticalAlign="Top" RowStyle-VerticalAlign="Top" EmptyDataText="* No promoters found.">
			<Columns>
				<asp:TemplateField HeaderText="K" ItemStyle-HorizontalAlign="Right">
					<ItemTemplate>
						<%#((Bobs.Promoter)Container.DataItem).K %>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Promoter">
					<ItemTemplate>
						<%#((Bobs.Promoter)Container.DataItem).LinkNewWindow() %>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Contact">
					<ItemTemplate>
						<%#((Bobs.Promoter)Container.DataItem).ContactName %>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="<nobr>Primary user</nobr>">
					<ItemStyle VerticalAlign="Top" />
					<ItemTemplate>
						<asp:Image runat="server" ID="PrimaryUsrOnlineImage" ImageAlign="TextTop" ImageUrl="/gfx/icon-me-small-up.png" Visible="<%# ((Bobs.Promoter)Container.DataItem).PrimaryUsr == null ? false : ((Bobs.Promoter)Container.DataItem).PrimaryUsr.LoggedInNow %>" />
						<asp:Image runat="server" ID="PrimaryUsrOfflineImage" ImageAlign="TextTop" ImageUrl="/gfx/icon-me-small-dn.png" Visible="<%# ((Bobs.Promoter)Container.DataItem).PrimaryUsr == null ? true : !((Bobs.Promoter)Container.DataItem).PrimaryUsr.LoggedInNow %>" />
						<%#((Bobs.Promoter)Container.DataItem).PrimaryUsrLink%>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="<nobr>VAT status</nobr>">
					<ItemTemplate>
						<%#Utilities.CamelCaseToString(((Bobs.Promoter)Container.DataItem).VatStatus.ToString()) %>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="<nobr>VAT registration #</nobr>">
					<ItemTemplate>
						<%#((Bobs.Promoter)Container.DataItem).VatNumber %>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="<nobr>VAT registration country</nobr>">
					<ItemTemplate>
						<%#((Bobs.Promoter)Container.DataItem).VatCountry != null ? ((Bobs.Promoter)Container.DataItem).VatCountry.Name : "&nbsp;" %>
					</ItemTemplate>
				</asp:TemplateField>
			</Columns>
		</asp:GridView>
	</div>
</asp:Panel>
