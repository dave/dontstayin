<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Moderate.ascx.cs" Inherits="Spotted.Pages.Venues.Moderate" %>


<dsi:h1 runat="server" ID="H16">Moderators</dsi:h1>
<div class="ContentBorder">
	<P class=BigCenter>
		Please moderate other people's items - start with the people 
		that are NOT online.
	</P>
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
				<asp:TemplateColumn HeaderText="New<br>venues">
					<ItemTemplate>
						<%#((Bobs.Usr)Container.DataItem).NewVenuesToModerateHtml%>
					</ItemTemplate>
				</asp:TemplateColumn>
			</Columns>
		</asp:DataGrid>
	</p>
</div>
<a name="ActionsPanel"></a>
<dsi:h1 runat="server" ID="H13">Actions</dsi:h1>
<div class="ContentBorder">
	<p>
		This page lists new venues. They go live on the site as soon as they're added, but they're
		also listed on this page until they're enabled.
	</p>
	<p>
		People that add events and venues to the site don't always check the site as 
		thoroughly as they should before adding. If duplicate venues get events added to them, it will make 
		the site into a right mess, so please look carefully before enabling.
	</p>
	<p>
		Please read through the text of each venue before enabling it. If a venue has blatantly
		incorrect details, please delete or edit it.
	</p>
	<p>
		To make it easier, you can select multiple venues with the <b>Select</b> check boxes, and delete
		or enable all selected items using the buttons below.
	</p>
	<p>
		<asp:Button ID="Button1" Runat="server" Text="Enable selected" OnClick="EnableSelected"/>
		<asp:Button Runat="server" Text="Delete selected" OnClick="DeleteSelected" ID="DeleteSelectedButton"/>
	</p>
	<p runat="server" id="OutputP" visible="false"></p>
</div>

<asp:Panel Runat="server" id="ItemsPanel" EnableViewState="False">
	<dsi:h1 runat="server" ID="H11">Items</dsi:h1>
	<div class="ContentBorder">
		<p>
			<asp:Repeater Runat="server" ID="ItemsRepeater"/>
		</p>
		<p class="BigCenter"><a href="#ActionsPanel">Skip to the top</a></p>
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
