<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ModeratePhotoTags.ascx.cs" Inherits="Spotted.Pages.ModeratePhotoTags" %>

<dsi:h1 runat="server" ID="H16">Moderate tags</dsi:h1>
<div class="ContentBorder">
<center><a href="<%= Photo.Url() %>"><img runat="server" ID="uiPhotoImg" src="" class="BorderBlack All" border="0"></a></center>

<p runat="server" id="uiNoTags">This photo has no tags.</p>

<asp:GridView runat="server" ID="uiPhotoTags" AutoGenerateColumns="false" OnRowCommand="OnRowCommand" OnRowDataBound="OnRowDataBound"
	GridLines="None" BorderWidth=0 CellPadding=3 CssClass=dataGrid AlternatingItemStyle-CssClass="dataGridAltItem"
	HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" ItemStyle-VerticalAlign="Top" EnableViewState="false">
	<Columns>
		<asp:TemplateField HeaderText="Tag">
			<ItemTemplate>
				<%# ((TagPhoto)Container.DataItem).Tag.TagText %>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="Last action by">
			<ItemTemplate>
				<%# ((TagPhoto)Container.DataItem).MostRecentTagPhotoHistory.Usr.Link()%>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="Last action time">
			<ItemTemplate>
				<%# ((TagPhoto)Container.DataItem).MostRecentTagPhotoHistory.DateTime.ToString("F")%>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="Last action">
			<ItemTemplate>
				<%# ((TagPhoto)Container.DataItem).MostRecentTagPhotoHistory.Action.ToString() %>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField>
			<ItemTemplate>
				<asp:LinkButton ID="LinkButton1" runat="server" Text="Disable" CommandArgument="<%# ((TagPhoto)Container.DataItem).K %>" CommandName="Disable" Visible="<%# !((TagPhoto)Container.DataItem).Disabled %>" />
				<asp:LinkButton ID="LinkButton2" runat="server" Text="Reenable" CommandArgument="<%# ((TagPhoto)Container.DataItem).K %>" CommandName="Enable" Visible="<%# ((TagPhoto)Container.DataItem).Disabled %>" />
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField>
			<ItemTemplate >
				<asp:LinkButton runat="server" ID="uiBlockTag" Text="Block this tag" CommandArgument="<%# ((TagPhoto)Container.DataItem).TagK %>" CommandName="Block" Visible="<%# Usr.Current.IsAdmin && !((TagPhoto)Container.DataItem).Tag.Blocked %>"/>
				<asp:LinkButton runat="server" ID="uiUnblock" Text="Unblock this tag" CommandArgument="<%# ((TagPhoto)Container.DataItem).TagK %>" CommandName="Unblock" Visible="<%# Usr.Current.IsAdmin && ((TagPhoto)Container.DataItem).Tag.Blocked %>"/>
			</ItemTemplate>
		</asp:TemplateField>
	</Columns>
</asp:GridView>

</div>
