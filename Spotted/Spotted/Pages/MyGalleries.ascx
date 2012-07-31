<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyGalleries.ascx.cs" Inherits="Spotted.Pages.MyGalleries" %>


<asp:Panel Runat="server" id="GalleriesPanel">
	<dsi:h1 runat="server" ID="H11">Galleries</dsi:h1>
	<div class="ContentBorder">
		<h2>Your current galleries</h2>
		<p>
			You have added the galleries below. Use the Edit button to add/delete photos, 
			change the name or change the title photo.
		</p>
		<p>
			<asp:DataGrid Runat="server" ID="GalleriesDataGrid" 
				GridLines="None" AutoGenerateColumns="False"
				BorderWidth=0 CellPadding=3 CssClass=dataGrid 
				AlternatingItemStyle-CssClass="dataGridAltItem"
				HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" 
				ItemStyle-VerticalAlign="Top" AllowPaging="True" OnPageIndexChanged="GalleriesDataGridChangePage"
				PageSize="10" PagerStyle-Mode="NumericPages">
				<Columns>
					<asp:TemplateColumn HeaderText="Title">
						<ItemTemplate>
							<img src="<%#((Bobs.Gallery)(Container.DataItem)).PicPath%>" width="50" height="50" class="BorderBlack All">
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Gallery name">
						<ItemTemplate>
							<nobr><%#((Bobs.Gallery)(Container.DataItem)).Name%></nobr>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Parent">
					<ItemTemplate>
						<%#((Bobs.Gallery)(Container.DataItem)).ParentObjectHtml(true)%>
					</ItemTemplate>
				</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Photos">
						<ItemTemplate>
							<%#((Bobs.Gallery)(Container.DataItem)).TotalPhotos%>&nbsp;total, (<%#((Bobs.Gallery)(Container.DataItem)).LivePhotos%>&nbsp;live)
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Options">
						<ItemTemplate>
							<a href="<%#((Bobs.Gallery)(Container.DataItem)).UrlApp("edit")%>">Edit</a><br>
							<a href="<%#((Bobs.Gallery)(Container.DataItem)).Url()%>">Preview</a>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:DataGrid>
		</p>
	</div>
</asp:Panel>
<asp:Panel Runat="server" id="NoGalleriesPanel" Visible="False">
	<dsi:h1 runat="server" ID="H12">Galleries</dsi:h1>
	<div class="ContentBorder">
		<p>
			You haven't added any galleries yet. To add a gallery, find the event you want to 
			add photos to and click the "Add your gallery" button.
			You can find events by using the <a href="/pages/calendar">calendar</a>, or if we don't have the 
			event listed, you can <a href="/pages/events/edit">add it</a>.
		</p>
	</div>
</asp:Panel>
