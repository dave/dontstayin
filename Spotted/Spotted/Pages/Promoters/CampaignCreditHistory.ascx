<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CampaignCreditHistory.ascx.cs" Inherits="Spotted.Pages.Promoters.CampaignCreditHistory" %>
<%@ Register src="/Controls/PaginationControl.ascx" tagname="PaginationControl" tagprefix="uc1" %>

<dsi:PromoterIntro runat="server" ID="PromoterIntro" Header="Buy Campaign Credits">
	<p>
		Campaign credits are used for buying stuff on the site, like advertising.
	</p>
	<h2>
		Current balance: <%= CurrentPromoter.CampaignCredits.ToString() %></h2>
	<p>
		<a href="<%= CurrentPromoter.UrlApp("campaigncredits") %>" >
		<img src="/gfx/icon-add.png" border="0" align="absmiddle" style="margin-right:3px"  width="26" height="21"/>add campaign credits</a>
	</p>
</dsi:PromoterIntro>
<dsi:h1 runat="server" ID="H1Title">Campaign Credits History</dsi:h1>
<asp:Panel runat="server" ID="CampaignCreditsHistoryPanel" CssClass="ContentBorder">
	
	<p>
		<asp:GridView ID="CampaignCreditHistoryGridView" runat="server" AllowPaging="True" AutoGenerateColumns="False" PageSize="20" CssClass="dataGrid" EnableViewState="true" 
			AlternatingRowStyle-CssClass="dataGridAltItem" GridLines="None" BorderWidth="0" CellPadding="3" HeaderStyle-CssClass="dataGridHeader" SelectedRowStyle-CssClass="dataGridSelectedItem"
			AlternatingRowStyle-VerticalAlign="Top" RowStyle-VerticalAlign="Top">
			<Columns>
				<asp:TemplateField HeaderText="Date">
					<ItemTemplate>
						<nobr><%# ((Bobs.CampaignCredit)Container.DataItem).ActionDateTime.ToString("ddd dd/MM/yy HH:mm")%></nobr>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Description" ItemStyle-Width="315px">
					<ItemTemplate>
						<%#((Bobs.CampaignCredit)Container.DataItem).FriendlyLinkDescription %>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="In" ItemStyle-HorizontalAlign=Right ItemStyle-Font-Bold=true ItemStyle-Width="25px">
					<ItemTemplate>
						<nobr><%#((Bobs.CampaignCredit)Container.DataItem).Credits > 0 ? ((Bobs.CampaignCredit)Container.DataItem).Credits.ToString() : "&nbsp;"%></nobr>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Out" ItemStyle-HorizontalAlign=Right ItemStyle-Font-Bold=true ItemStyle-Width="25px">
					<ItemTemplate>
						<nobr><%#((Bobs.CampaignCredit)Container.DataItem).Credits < 0 ? (-1 * ((Bobs.CampaignCredit)Container.DataItem).Credits).ToString() : "&nbsp;" %></nobr>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Balance" ItemStyle-HorizontalAlign=Right ItemStyle-Font-Bold=true >
					<ItemTemplate>
						<nobr><%#((Bobs.CampaignCredit)Container.DataItem).BalanceToDate %></nobr>
					</ItemTemplate>
				</asp:TemplateField>
			</Columns>	
		</asp:GridView>
	</p>
	<p>
		<uc1:PaginationControl ID="uiPaginationControl" runat="server"/>
	</p>
</asp:Panel>
