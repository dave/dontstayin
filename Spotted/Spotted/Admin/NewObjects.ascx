<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewObjects.ascx.cs" Inherits="Spotted.Admin.NewObjects" %>
<%@ Register TagPrefix="Controls" TagName="Banners" Src="/Controls/Admin/BannerDataGrid.ascx" %>

<asp:Panel runat="server" Visible="false">
<h1>Spotters</h1>
<div class="ContentBorder">
	<p>
		Accounts csv <a href="/popup/accounts?date=<%= DateTime.Today.AddDays(-1).ToString("yyyy/MM/dd") %>" target="_blank">Accounts</a>.
	</p>
</div>

<h1>New/edited events/venues</h1>
<div class="ContentBorder">
	<p>
		<asp:Label Runat="server" ID="NewEvents"></asp:Label>
	</p>
</div>
</asp:Panel>

<asp:Panel Runat="server" ID="PanelUnconfirmedBrands" Visible="false">
	<h1>Unconfirmed brands</h1>
	<div class="ContentBorder">
		<p>
			Unconfirmed brands on promoters that are enabled:
		</p>
		<p>
			<asp:Repeater Runat="server" ID="BrandRepeater">
				<ItemTemplate>
					<a href="<%#((Bobs.Brand)Container.DataItem).Url()%>"><%#((Bobs.Brand)Container.DataItem).Name%></a></ItemTemplate>
				<SeparatorTemplate>, </SeparatorTemplate>
			</asp:Repeater>
		</p>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="PanelUnconfirmedVenues" Visible="false">
	<h1>Unconfirmed venues</h1>
	<div class="ContentBorder">
		<p>
			Unconfirmed venues on promoters that are enabled:
		</p>
		<p>
			<asp:Repeater Runat="server" ID="VenueRepeater">
				<ItemTemplate>
					<a href="<%#((Bobs.Venue)Container.DataItem).Url()%>"><%#((Bobs.Venue)Container.DataItem).Name%></a></ItemTemplate>
				<SeparatorTemplate>, </SeparatorTemplate>
			</asp:Repeater>
		</p>
	</div>
</asp:Panel>

<asp:Panel runat="server" Visible="false">
	<h1>Guestlists</h1>
	<div class="ContentBorder">
		<p>
			<asp:DataGrid Runat="server" ID="GuestlistDataGrid" 
				GridLines="None" AutoGenerateColumns="False"
				BorderWidth=0 CellPadding=3 CssClass=dataGrid 
				AlternatingItemStyle-CssClass="dataGridAltItem"
				HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" 
				ItemStyle-VerticalAlign="Top"
				PageSize="10" PagerStyle-Mode="NumericPages">
				<Columns>
					<asp:TemplateColumn HeaderText="Date">
						<ItemTemplate>
							<nobr><%#Cambro.Misc.Utility.FriendlyDate(((Bobs.Event)Container.DataItem).DateTime,true,false)%></nobr>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Event">
						<ItemTemplate>
							<span class="CleanLinks">
								<b><a href="<%#((Bobs.Event)Container.DataItem).GuestlistPromoter.Url()%>" target="_blank"><%#((Bobs.Event)(Container.DataItem)).GuestlistPromoter.Name%></a></b> - 
								<small><a href="<%#((Bobs.Event)Container.DataItem).Url()%>" target="_blank"><%#((Bobs.Event)(Container.DataItem)).Name%></a> @ <a href="<%#((Bobs.Event)Container.DataItem).Venue.Url()%>" target="_blank"><%#((Bobs.Event)(Container.DataItem)).Venue.Name%></a> in <a href="<%#((Bobs.Event)Container.DataItem).Venue.Place.Url()%>" target="_blank"><%#((Bobs.Event)(Container.DataItem)).Venue.Place.Name%></a></small>
							</span>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Status">
						<ItemTemplate>
							<%#((Bobs.Event)Container.DataItem).GuestlistOpen?"Open":(((Bobs.Event)(Container.DataItem)).GuestlistFinished?"Closed":"Paused")%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="On list /<br>Limit /<br>Spaces">
						<ItemTemplate>
							<nobr><%#((Bobs.Event)Container.DataItem).GuestlistCount%> / 
							<%#((Bobs.Event)Container.DataItem).GuestlistLimit%> / 
							<%#((Bobs.Event)Container.DataItem).GuestlistLimit - ((Bobs.Event)(Container.DataItem)).GuestlistCount%></nobr>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Regular<br>price">
						<ItemTemplate>
							<%#((Bobs.Event)Container.DataItem).GuestlistRegularPrice.ToString("£0.##")%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Guestlist<br>price">
						<ItemTemplate>
							<%#((Bobs.Event)Container.DataItem).GuestlistPrice.ToString("£0.##")%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Options">
						<ItemTemplate>
							<nobr><%#((Bobs.Event)Container.DataItem).GuestlistOptionsHtml%></nobr>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:DataGrid>
		</p>
	</div>
</asp:Panel>

<h1>Current banners</h1>
<div class="ContentBorder">
	
	<h2>
		Banners NOT paid for, not live:
	</h2>
	<p>
		<Controls:Banners Runat="Server" ID="NotPaidForBanners"/>
	</p>
	<h2>
		Banners paid for, not live:
	</h2>
	<p>
		<Controls:Banners Runat="Server" ID="PaidForBanners"/>
	</p>
	<h2>
		Live Hotboxes
	</h2>
	<p>
		<Controls:Banners Runat="Server" ID="LiveHotBoxes"/>
	</p>
	<h2>
		Live leaderboards
	</h2>
	<p>
		<Controls:Banners Runat="Server" ID="LiveLeaderboards"/>
	</p>
	<h2>
		Live Photo banners
	</h2>
	<p>
		<Controls:Banners Runat="Server" ID="LivePhotoBanners"/>
	</p>
	<h2>
		Live Email banners
	</h2>
	<p>
		<Controls:Banners Runat="Server" ID="LiveEmailBanners"/>
	</p>
	<h2>
		Live skyscrapers
	</h2>
	<p>
		<Controls:Banners Runat="Server" ID="LiveSkyscrapers"/>
	</p>
	<p>
		<a href="/promoters/auto-event-banners/banners">Edit auto event banners</a>
	</p>
</div>

<h1>Last 10 banned users</h1>
<div class="ContentBorder">
	<p>
		<asp:DataGrid Runat="server" ID="BannedUsrDataGrid" 
			GridLines="None" AutoGenerateColumns="False"
			BorderWidth=0 CellPadding=3 CssClass=dataGrid 
			AlternatingItemStyle-CssClass="dataGridAltItem"
			HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" 
			ItemStyle-VerticalAlign="Top"
			PageSize="10" PagerStyle-Mode="NumericPages">
			<Columns>
				<asp:TemplateColumn HeaderText="Banned user">
					<ItemTemplate>
						<a href="<%#((Bobs.Usr)(Container.DataItem)).Url()%>" target="_blank"><%#((Bobs.Usr)(Container.DataItem)).NickName%></a> (<%#((Bobs.Usr)(Container.DataItem)).K%>, <%#((Bobs.Usr)(Container.DataItem)).Email%>)<br>
						<%#((Bobs.Usr)(Container.DataItem)).EventCount%> events, <%#((Bobs.Usr)(Container.DataItem)).CommentCount%> comments, <%#((Bobs.Usr)(Container.DataItem)).ChatMessageCount%> chat messages
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Banned by">
					<ItemTemplate>
						<a href="<%#((Bobs.Usr)(Container.DataItem)).BannedByUsr.Url()%>" target="_blank"><%#((Bobs.Usr)(Container.DataItem)).BannedByUsr.NickName%></a>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="When?">
					<ItemTemplate>
						<%#Cambro.Misc.Utility.FriendlyDate(((Bobs.Usr)(Container.DataItem)).BannedDateTime,true,false)%>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Why?">
					<ItemTemplate>
						<%#((Bobs.Usr)(Container.DataItem)).BannedReason%>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Photos">
					<ItemTemplate>
						<a href="/admin/stripusr?UsrK=<%#((Bobs.Usr)(Container.DataItem)).K%>" onclick="return confirm('are you sure?');">Strip</a>
						<a href="/admin/multidelete?ObjectType=Usr&ObjectK=<%#((Bobs.Usr)(Container.DataItem)).K%>" onclick="return confirm('are you sure?');">Delete</a>
					</ItemTemplate>
				</asp:TemplateColumn>
			</Columns>
		</asp:DataGrid>
	</p>
</div>
<h1>New photos</h1>
<div class="ContentBorder">
	<p>
		The galleries below have new photos:
	</p>
	<p>
		<asp:DataGrid Runat="server" ID="GalleriesDataGrid" 
			GridLines="None" AutoGenerateColumns="False"
			BorderWidth=0 CellPadding=3 CssClass=dataGrid 
			AlternatingItemStyle-CssClass="dataGridAltItem"
			HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" 
			ItemStyle-VerticalAlign="Top"
			PageSize="10" PagerStyle-Mode="NumericPages">
			<Columns>
				<asp:TemplateColumn HeaderText="Title">
					<ItemTemplate>
						<a href="<%#((Bobs.Gallery)(Container.DataItem)).UrlApp("moderate")%>"><img src="<%#((Bobs.Gallery)(Container.DataItem)).PicPath%>" width="50" height="50" style="border:1px solid #000000;"></a>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Gallery name">
					<ItemTemplate>
						<a href="<%#((Bobs.Gallery)(Container.DataItem)).UrlApp("moderate")%>"><nobr><%#((Bobs.Gallery)(Container.DataItem)).Name%></nobr></a>							
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
