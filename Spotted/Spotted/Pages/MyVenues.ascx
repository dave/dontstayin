<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyVenues.ascx.cs" Inherits="Spotted.Pages.MyVenues" %>


<asp:Panel Runat="server" ID="VenuesPanel">
	<dsi:h1 runat="server" ID="H11">Venues</dsi:h1>
	<div class="ContentBorder">
		<h2>Adding a new venue</h2>
		<p>
			You can add a new venue in the <a href="/pages/events/edit">add event wizard</a>, 
			or <a href="/pages/venues/edit">click here to add a venue on its own</a>.
		</p>
		<h2>Venues you've already added</h2>
		<p>
			You have added the venues below. Click <b>Edit</b> to change the details or
			<b>Add an event here</b> to add an event at this venue.
		</p>
		<p>
			<asp:DataGrid Runat="server" ID="VenuesDataGrid" 
				GridLines="None" AutoGenerateColumns="False"
				BorderWidth=0 CellPadding=3 CssClass=dataGrid 
				AlternatingItemStyle-CssClass="dataGridAltItem"
				HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" 
				ItemStyle-VerticalAlign="Top" AllowPaging="True" OnPageIndexChanged="VenuesDataGridChangePage"
				PageSize="20" PagerStyle-Mode="NumericPages">
				<Columns>
					<asp:TemplateColumn HeaderText="Name">
						<ItemTemplate>
							<a href="<%#((Bobs.Venue)(Container.DataItem)).Url()%>"><%#((Bobs.Venue)(Container.DataItem)).Name%></a><small> in <%#((Bobs.Venue)(Container.DataItem)).Place.Name%></small>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Edit">
						<ItemTemplate>
							<%#EditHtml((Bobs.Venue)(Container.DataItem))%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Options">
						<ItemTemplate>
							<a href="/pages/events/edit/venuek-<%#((Bobs.Venue)(Container.DataItem)).K%>">Add an event</a>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:DataGrid>
		</p>
	</div>
</asp:Panel>
