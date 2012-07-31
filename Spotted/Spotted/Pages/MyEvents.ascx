<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyEvents.ascx.cs" Inherits="Spotted.Pages.MyEvents" %>

<asp:Panel Runat="server" ID="EventsPanel">
	<dsi:h1 runat="server" ID="H11">Events</dsi:h1>
	<div class="ContentBorder">
		<h2>Adding a new event</h2>
		<p>
			<a href="/pages/events/edit">Click here to add a new event</a>
		</p>
		<h2>Events you've already added</h2>
		<p>
			You have added the events below. Click <b>Edit</b> to change the details,  
			click <b>Add photos</b> to upload your photos, or click <b>Review</b> to 
			add a review of the event.
		</p>
		<p>
			<asp:DataGrid Runat="server" ID="EventsDataGrid" 
				GridLines="None" AutoGenerateColumns="False"
				BorderWidth=0 CellPadding=3 CssClass=dataGrid 
				AlternatingItemStyle-CssClass="dataGridAltItem"
				HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" 
				ItemStyle-VerticalAlign="Top" AllowPaging="True" OnPageIndexChanged="EventsDataGridChangePage"
				PageSize="20" PagerStyle-Mode="NumericPages">
				<Columns>
					<asp:TemplateColumn HeaderText="Name">
						<ItemTemplate>
							<a href="<%#((Bobs.Event)(Container.DataItem)).Url()%>"><%#((Bobs.Event)(Container.DataItem)).Name%></a> <small>@ <a href="<%#((Bobs.Event)(Container.DataItem)).Venue.Url()%>"><%#((Bobs.Event)(Container.DataItem)).Venue.Name%></a></small>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Date">
						<ItemTemplate>
							<nobr><%#((Bobs.Event)(Container.DataItem)).FriendlyDate(true)%></nobr>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Edit">
						<ItemTemplate>
							<%#EditHtml((Bobs.Event)(Container.DataItem))%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Photos">
						<ItemTemplate>
							<%#PhotosHtml((Bobs.Event)(Container.DataItem))%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Review">
						<ItemTemplate>
							<%#ReviewHtml((Bobs.Event)(Container.DataItem))%>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:DataGrid>
		</p>
	</div>
</asp:Panel>
