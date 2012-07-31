<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RecentlyEndedBanners.ascx.cs" Inherits="Spotted.Admin.RecentlyEndedBanners" %>

<p>
	<nobr>
		Show banners ending between <dsi:Cal runat="server" id="uiFirstDate"></dsi:Cal> and <dsi:Cal runat="server" id="uiSecondDate"></dsi:Cal><asp:Button ID="uiChangeDateRange" runat="server" Text="Change date range"></asp:Button>
	</nobr>
</p>
<asp:GridView ID="uiBanners" runat="server" AutoGenerateColumns="false">
	<Columns>
		<asp:TemplateField HeaderText="K">
			<ItemTemplate>
				<%# ((Banner) Container.DataItem).K.ToString() %>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="Promoter">
			<ItemTemplate>
				<a href='<%# ((Banner) Container.DataItem).Promoter.Url() %>' ><%# ((Banner) Container.DataItem).Promoter.Name %></a>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="Name">
			<ItemTemplate>
				<a href='<%# ((Banner) Container.DataItem).Url() %>' ><%# ((Banner) Container.DataItem).Name %></a>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="LastDay">
			<ItemTemplate>
				<%# ((Banner) Container.DataItem).LastDay.ToShortDateString() %>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="Required hits">
			<ItemTemplate>
				<%# ((Banner) Container.DataItem).TotalRequiredImpressions.ToString("n0") %>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="Actual hits">
			<ItemTemplate>
				<%# ((Banner) Container.DataItem).TotalHits.ToString("n0") %>
			</ItemTemplate>
		</asp:TemplateField>		
		<asp:TemplateField HeaderText="Hits (%)">
			<ItemTemplate>
				<nobr><%# (Convert.ToDouble(((Banner)Container.DataItem).TotalHits) / (Convert.ToDouble(((Banner)Container.DataItem).TotalRequiredImpressions))).ToString("p")%></nobr>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="Position">
			<ItemTemplate>
				<%# ((Banner) Container.DataItem).Position %>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="Cancelled?">
			<ItemTemplate>
				<%# ((Banner)Container.DataItem).StatusEnabled ? "" : "Y"%>
			</ItemTemplate>
		</asp:TemplateField>
		
	</Columns>
</asp:GridView>
