<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Add.ascx.cs" Inherits="Spotted.Pages.Galleries.Add" %>

<%@ Import Namespace=Bobs %>


<asp:Panel Runat="server" ID="FutureEventPanel">
	<dsi:h1 runat="server" ID="Header">Event is in the future!</dsi:h1>
	<div class="ContentBorder">
		<p>
			You're trying to add a gallery to <a href="<%= ((IPage)CurrentParent).Url() %>"><%= ((IName)CurrentParent).FriendlyName %></a>
		</p>
		<p>
			You can't add a gallery to an event that hasn't happened yet!
		</p>
		<p>
			<a href="<%= ((IPage)CurrentParent).Url() %>">Back to the <%= ((IBobType)CurrentParent).TypeName.ToLower() %> page</a>
		</p>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="NoEditArticlePanel">
	<dsi:h1 runat="server" ID="H14">Article is not editable!</dsi:h1>
	<div class="ContentBorder">
		<p>
			You're trying to add a gallery to <a href="<%= ((IPage)CurrentParent).Url() %>"><%= ((IName)CurrentParent).FriendlyName %></a>
		</p>
		<p>
			You can't add a gallery to an article that is status <i>New</i>. 
			Articles in <i>Editorial</i> or <i>Enabled</i> mode can't be editied or have photos added.
		</p>
		<p>
			<a href="<%= ((IPage)CurrentParent).Url() %>">Back to the <%= ((IBobType)CurrentParent).TypeName.ToLower() %> page</a>
		</p>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="PanelNoPhoto">
	<dsi:h1 runat="server" ID="H13">Can't add new gallery</dsi:h1>
	<div class="ContentBorder">
		<p>
			This venue or promoter has a no-photo policy.
		</p>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="CantAddGallery">
	<dsi:h1 runat="server" ID="H12">Can't add new gallery</dsi:h1>
	<div class="ContentBorder">
		<p>
			You're trying to add a gallery to <a href="<%= ((IPage)CurrentParent).Url() %>"><%= ((IName)CurrentParent).FriendlyName %></a>
		</p>
		<p>
			<b>Only spotters can add multiple galleries to the same event.</b>
		</p>
		<p>
			Signing up as a spotter is FREE, and we'll send you cards to give out to 
			anyone you take a photo of. <a href="/pages/spotters">Sign up as a spotter here</a>.
		</p>
		<p>
			You may edit your current gallery: <a href="" runat="server" id="EditCurrentGalleryLink"></a>
		</p>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="EventHasGalleriesPanel">
	<dsi:h1 runat="server" ID="H11">Galleries</dsi:h1>
	<div class="ContentBorder">
		<p>
			Below are listed all your galleries for <a href="<%= ((IPage)CurrentParent).Url() %>"><%= ((IName)CurrentParent).FriendlyName %></a>
		</p>
		<p>
			You may <a href="/pages/galleries/add/eventk-<%= (CurrentEvent!=null?CurrentEvent.K.ToString():"0") %>/new-1">add another gallery</a>, or add photos to a current gallery.
		</p>
		<p style="font-size:120%;font-weight:bold;">
			
		</p>
		<p>
			<asp:DataGrid Runat="server" ID="GalleriesDataGrid" 
				GridLines="None" AutoGenerateColumns="False"
				BorderWidth=0 CellPadding=3 CssClass=dataGrid 
				AlternatingItemStyle-CssClass="dataGridAltItem"
				HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" 
				ItemStyle-VerticalAlign="Top" 
				PageSize="20" PagerStyle-Mode="NumericPages">
				<Columns>
					<asp:TemplateColumn HeaderText="Title">
						<ItemTemplate>
							<a href="<%#((Bobs.Gallery)(Container.DataItem)).UrlApp("edit")%>"><img src="<%#((Bobs.Gallery)(Container.DataItem)).PicPath%>" width="50" height="50" class="BorderBlack All"></a>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Gallery name">
						<ItemTemplate>
							<nobr><a href="<%#((Bobs.Gallery)(Container.DataItem)).UrlApp("edit")%>"><%#((Bobs.Gallery)(Container.DataItem)).Name%></a></nobr>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Photos">
						<ItemTemplate>
							<%#((Bobs.Gallery)(Container.DataItem)).TotalPhotos%>&nbsp;total, (<%#((Bobs.Gallery)(Container.DataItem)).LivePhotos%>&nbsp;live)
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:DataGrid>
		</p>
	</div>
</asp:Panel>
