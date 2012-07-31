<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Moderate.ascx.cs" Inherits="Spotted.Pages.Galleries.Moderate" %>

<asp:Panel Runat="server" id="DonePanel" Visible="false">
	<dsi:h1 runat="server" ID="H16ds">Finished</dsi:h1>
	<div class="ContentBorder">
		<p>
			Well done, no more galleries to moderate. Sorry for that horrible message!
		</p>
	</div>
</asp:Panel>
<asp:Panel Runat="server" id="InfoPanel">
	
	<dsi:h1 runat="server" ID="H18">Changes</dsi:h1>
	<div class="ContentBorder">
		<p class="BigCenter">
			There are <%= GalleryCount %> galleries waiting...
		</p>
		<p>
			This page now DOESN'T list all the galleries - it only lists the current gallery. When you enable all it 
			jumps to a new gallery to moderate. To jump to another gallery, click <a href="/pages/galleries/moderate/mode-random">here</a>.
		</p>
		<p>
			Some galleries don't show photos because the database is a little funny... click this button to fix them: 
			<button runat="server" onserverclick="FixDodgey" ID="Button5">Fix dodgy galleries</button>
		</p>
	</div>
		
		
	<asp:Panel Runat="server" ID="GalleriesPanel">
		<a name="Galleries"></a>
		<dsi:h1 runat="server" ID="H11">Galleries with new photos</dsi:h1>
		<div class="ContentBorder">
			<p>
				This is the current gallery (this does NOT show a list of all the waiting galleries):
			</p>
			<p>
				<asp:DataGrid Runat="server" ID="GalleriesDataGrid" 
					OnItemDataBound="GalleriesDataGrid_ItemDataBound"
					GridLines="None" AutoGenerateColumns="False"
					BorderWidth=0 CellPadding=3 CssClass=dataGrid 
					HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" 
					ItemStyle-VerticalAlign="Top"
					PageSize="10" PagerStyle-Mode="NumericPages">
					<Columns>
						<asp:TemplateColumn HeaderText="Title">
							<ItemTemplate>
								<a href="<%#((Bobs.Gallery)(Container.DataItem)).UrlApp("moderate")%>#Photos"><img src="<%#((Bobs.Gallery)(Container.DataItem)).PicPath%>" width="50" height="50" class="BorderBlack All"></a>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Gallery name">
							<ItemTemplate>
								<a href="<%#((Bobs.Gallery)(Container.DataItem)).UrlApp("moderate")%>#Photos"><nobr><%#((Bobs.Gallery)(Container.DataItem)).Name%></nobr></a><br>
								<small>
									<a href="<%#((Bobs.Gallery)(Container.DataItem)).Url()%>" target="_blank">quick browser</a><br>
									<a href="<%#((Bobs.Gallery)(Container.DataItem)).PagedUrl()%>" target="_blank">gallery</a>
								</small>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Owner">
							<ItemTemplate>
								<a href="<%#((Bobs.Gallery)(Container.DataItem)).Owner.Url()%>"><%#((Bobs.Gallery)(Container.DataItem)).Owner.NickNameSafe%></a>
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
					</Columns>
				</asp:DataGrid>
			</p>
		</div>
	</asp:Panel>


	<a name="Photos"></a>
	<asp:Panel Runat="server" ID="PhotosPanel">
		
		
		<dsi:h1 runat="server" ID="H14">Admin note</dsi:h1>
		<div class="ContentBorder">
			<p>
				Please read this before enabling photos. If you need to query something before enabling / deleting the photos,
				please put a note in here.
			</p>
			<p>
				<asp:TextBox Runat="server" ID="AdminNoteTextBox" Columns="80" Rows="10" TextMode="MultiLine"></asp:TextBox><br>
				<asp:Button Runat="server" OnClick="SaveAdminNote" Text="Save admin note" ID="Button1"></asp:Button>
			</p>
		</div>
		
		
		<a name="ActionsPanel"></a>
		<dsi:h1 runat="server" ID="H15">Actions</dsi:h1>
		<div class="ContentBorder">
			<h2>
				All photos
			</h2>
			<p>
				<asp:Button Runat="server" onclick="Enable" Text="Enable all photos" ID="Button2"></asp:Button>&nbsp;&nbsp;&nbsp;
				<asp:Button Runat="server" onclick="Delete" ID="DeleteButton" Text="Delete all photos"></asp:Button>
			</p>
			
			<h2>
				Selected photos
			</h2>
			<p>
				<asp:Button Runat="server" onclick="EnableSelected" Text="Enable selected photos" ID="Button3"></asp:Button>&nbsp;&nbsp;&nbsp;
				<asp:Button Runat="server" onclick="DeleteSelected" ID="DeleteSelectedButton" Text="Delete selected photos"></asp:Button>
			</p>
			<p runat="server" id="SelectedOutputP"></p>
		</div>
		
		<dsi:h1 runat="server" ID="H12">New photos in this gallery</dsi:h1>
		<div class="ContentBorder">
			
			<p>
				<asp:DataList Runat="server" Width="100%" ID="PhotoDataList" ItemStyle-HorizontalAlign="Center" RepeatColumns="3" RepeatDirection="Horizontal" RepeatLayout="Table" ItemStyle-VerticalAlign=top CellPadding="8"  />
			</p>
			<p class="BigCenter"><a href="#ActionsPanel">Skip to the top</a></p>
		</div>
		
		<dsi:h1 runat="server" ID="H17">Actions</dsi:h1>
		<div class="ContentBorder">
			<h2>
				All photos
			</h2>
			<p>
				<asp:Button Runat="server" onclick="Enable" Text="Enable all photos" ID="Button4"></asp:Button>&nbsp;&nbsp;&nbsp;
				<asp:Button Runat="server" onclick="Delete" ID="DeleteButton1" Text="Delete all photos"></asp:Button>
			</p>
			
			<h2>
				Selected photos
			</h2>
			<p>
				<asp:Button Runat="server" onclick="EnableSelected" Text="Enable selected photos" ID="Button6"></asp:Button>&nbsp;&nbsp;&nbsp;
				<asp:Button Runat="server" onclick="DeleteSelected" ID="DeleteSelectedButton1" Text="Delete selected photos"></asp:Button>
			</p>
		</div>
		
		<dsi:h1 runat="server" ID="H13">Current photo guidelines</dsi:h1>
		<div class="ContentBorder">
			<p>
				Please delete photos that:
			</p>
			<ul>
				<li>need rotating, and you must notify the user to rotate the originals and re-upload.</li>
				<li>are duplicated.</li>
				<li>are out of focus.</li>
