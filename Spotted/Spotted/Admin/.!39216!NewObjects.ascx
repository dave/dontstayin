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
