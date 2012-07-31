<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ModerateUsrTags.ascx.cs" Inherits="Spotted.Pages.ModerateUsrTags" %>

<dsi:h1 runat="server" ID="H16">Moderate tags</dsi:h1>
<div class="ContentBorder">

	<p>Tags made by <a href="<%= ThisUsr.Url() %>" <%= ThisUsr.Rollover %>><%= ThisUsr.NickName %></a></p>

	<asp:RadioButtonList ID="uiTypeOfAction" runat="server" AutoPostBack="True">
		<asp:ListItem Selected="True">Active actions</asp:ListItem>
		<asp:ListItem>All actions</asp:ListItem>

	</asp:RadioButtonList>

	<p runat="server" id="uiNoTags">This user has tagged no photos.</p>

<asp:GridView runat="server" ID="uiInfo" AutoGenerateColumns="false" OnRowCommand="OnRowCommand" OnRowDataBound="OnRowDataBound"
	GridLines="None" BorderWidth=0 CellPadding=3 CssClass=dataGrid AlternatingItemStyle-CssClass="dataGridAltItem"
	HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" ItemStyle-VerticalAlign="Top" EnableViewState="false" >
	<Columns>
		<asp:TemplateField HeaderText="Tag">
			<ItemTemplate>
				<%# ((TagPhotoHistory)Container.DataItem).TagPhoto.Tag.TagText %>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="Photo">
			<ItemTemplate>
				<a href="<%# ((TagPhotoHistory)Container.DataItem).TagPhoto.Photo.Url()%>">
					<img src="<%# ((TagPhotoHistory)Container.DataItem).TagPhoto.Photo.IconPath %>" width="20px" height="20px" border="0" />
				</a>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="Last action time">
			<ItemTemplate>
				<%# ((TagPhotoHistory)Container.DataItem).DateTime.ToString("F")%>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="Last action">
			<ItemTemplate>
				<%# ((TagPhotoHistory)Container.DataItem).Action.ToString()%>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField>
			<ItemTemplate>
				<asp:LinkButton ID="LinkButton1" runat="server" Text="Disable" CommandArgument="<%# ((TagPhotoHistory)Container.DataItem).K %>" CommandName="Disable" Visible="<%# ((TagPhotoHistory)Container.DataItem).K == ((TagPhotoHistory)Container.DataItem).TagPhoto.MostRecentTagPhotoHistory.K && !((TagPhotoHistory)Container.DataItem).TagPhoto.Disabled %>" />
				<asp:LinkButton ID="LinkButton2" runat="server" Text="Reenable" CommandArgument="<%# ((TagPhotoHistory)Container.DataItem).K %>" CommandName="Enable" Visible="<%# ((TagPhotoHistory)Container.DataItem).K == ((TagPhotoHistory)Container.DataItem).TagPhoto.MostRecentTagPhotoHistory.K && ((TagPhotoHistory)Container.DataItem).TagPhoto.Disabled %>" />
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField>
			<ItemTemplate >
				<asp:LinkButton runat="server" ID="uiBlockTag" Text="Block this tag" CommandArgument="<%# ((TagPhotoHistory)Container.DataItem).K %>" CommandName="Block" Visible="<%# Usr.Current.IsAdmin && ((TagPhotoHistory)Container.DataItem).K == ((TagPhotoHistory)Container.DataItem).TagPhoto.MostRecentTagPhotoHistory.K && !((TagPhotoHistory)Container.DataItem).TagPhoto.Tag.Blocked %>"/>
				<asp:LinkButton runat="server" ID="uiUnblock" Text="Unblock this tag" CommandArgument="<%# ((TagPhotoHistory)Container.DataItem).K %>" CommandName="Unblock" Visible="<%# Usr.Current.IsAdmin && ((TagPhotoHistory)Container.DataItem).K == ((TagPhotoHistory)Container.DataItem).TagPhoto.MostRecentTagPhotoHistory.K && ((TagPhotoHistory)Container.DataItem).TagPhoto.Tag.Blocked %>"/>
			</ItemTemplate>
		</asp:TemplateField>
	</Columns>
</asp:GridView>

</div>
