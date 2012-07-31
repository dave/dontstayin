<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ModerateNews.ascx.cs" Inherits="Spotted.Pages.ModerateNews" %>

<dsi:h1 runat="server" ID="H16">Moderators</dsi:h1>
<div class="ContentBorder">
	<p class="BigCenter">
		Please moderate other people's news - start with the people that are NOT online.
	</p>
	<p>
		<asp:DataGrid Runat="server" ID="ModeratorsDataGrid" 
			GridLines="None" AutoGenerateColumns="False"
			BorderWidth=0 CellPadding=3 CssClass=dataGrid 
			AlternatingItemStyle-CssClass="dataGridAltItem"
			HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" 
			ItemStyle-VerticalAlign="Top">
			<Columns>
				<asp:TemplateColumn HeaderText="Name">
					<ItemTemplate>
						<nobr><%#((Bobs.Usr)Container.DataItem).Link()%></nobr>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Online">
					<ItemTemplate>
						<%#((Bobs.Usr)Container.DataItem).LoggedInNow?"online":""%>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="News items">
					<ItemTemplate>
						<%#((Bobs.Usr)Container.DataItem).NewNewsToModerateHtml%>
					</ItemTemplate>
				</asp:TemplateColumn>
			</Columns>
		</asp:DataGrid>
	</p>
</div>
<dsi:h1 runat="server" ID="H13">Actions</dsi:h1>
<div class="ContentBorder">
	<p style="font-size:12px; font-weight:bold; margin:10px; padding:10px;" class="BorderBlack All">
		Changes 25/02/2006:<br />
		I've changed the way news works. When the public posts news, they don't choose the news level. 
		You have to choose a level each time you enable news. People can also now suggest news about photos.
	</p>
	<p>
		This page lists suggested news. Right now it's live on the site as a normal chat thread. 
		You have to assess if it's news-worthy, and if so, what news-level it should get.
	</p>
	<p>
		There are 6 news levels:
	</p>
	<p>
		<b>News level 10 - Event news:</b><br>
		e.g. Announcements about forthcoming events, DJ lineups etc.<br>
		Usually should be posted by promoters in their group
	</p>
	<p>
		<b>News level 20 - Minor local news:</b><br>
		e.g. New club night<br>
		Someone lost their wallet at a club<br>
		Small club night (under 500 capacity) moves to new venue
	</p>
	<p>
		<b>News level 30 - Major local news:</b><br>
		e.g. New club opens (over 500 capacity)<br>
		Club night (over 500 capacity) moves to new venue
	</p>
	<p>
		<b>News level 40 - Major DSI news:</b><br>
		e.g. Member news - marrage, birth, death etc.<br>
		New system announcements
	</p>
	<p>
		<b>News level 50 - National news:</b><br>
		e.g. Large club opens (over 1000 capacity)<br>
		Large club night (over 1000 capacity) moves to new venue
	</p>
	<p>
		<b>News level 60 - World news:</b><br>
		e.g. Superclub opens (over 2000 capacity)<br>
		Major international news stories
	</p>
	<script>
		var html10 = "<b>News level 10 - Event news:</b><br>e.g. Announcements about forthcoming events, DJ lineups etc.<br>Usually should be posted by promoters in their group";
		var html20 = "<b>News level 20 - Minor local news:</b><br>e.g. New club night<br>Someone lost their wallet at a club<br>Small club night (under 500 capacity) moves to new venue";
		var html30 = "<b>News level 30 - Major local news:</b><br>e.g. New club opens (over 500 capacity)<br>Club night (over 500 capacity) moves to new venue";
		var html40 = "<b>News level 40 - Major DSI news:</b><br>e.g. Member news - marrage, birth, death etc.<br>New system announcements";
		var html50 = "<b>News level 50 - National news:</b><br>e.g. Large club opens (over 1000 capacity)<br>Large club night (over 1000 capacity) moves to new venue";
		var html60 = "<b>News level 60 - World news:</b><br>e.g. Superclub opens (over 2000 capacity)<br>Major international news stories";
	</script>
</div>

<asp:Panel Runat="server" id="ItemsPanel" EnableViewState="False">
	<dsi:h1 runat="server" ID="H11">Items</dsi:h1>
	<div class="ContentBorder">
		<p>
			<asp:Repeater Runat="server" ID="ItemsRepeater"/>
		</p>
	</div>
</asp:Panel>
<style>
th
{
	vertical-align:top;
	text-align:right;
}
.AdminEventTable td
{
	border:1px solid #A58319;
	padding:5px;
}
</style>
