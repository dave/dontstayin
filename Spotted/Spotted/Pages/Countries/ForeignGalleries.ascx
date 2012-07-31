<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ForeignGalleries.ascx.cs" Inherits="Spotted.Pages.Countries.ForeignGalleries" %>

<%@ Register TagPrefix="Controls" TagName="Cal" Src="/Controls/Cal.ascx" %>
<asp:Panel Runat="server" ID="PanelItems">
	
	<dsi:h1 runat="server" ID="Hdgfd12" NAME="H1fg3">Foreign galleries</dsi:h1>
	<div class="ContentBorder">
		<p>
			This page shows galleries that were not taken in <a href="<%= CurrentCountry.Url() %>"><%= CurrentCountry.FriendlyName %></a>.
			You can look for galleries taken on a certain date by using the calendar:
		</p>
		<p>
			<a href="<%= CurrentCountry.UrlForeignGalleriesMonth(DateTime.Today) %>"><img src="/gfx/icon-calendar.png" style="margin-right:3px;" border="0" height="21" width="26" align="absmiddle">Calendar</a>
		</p>
	</div>
	
	
	<Controls:Cal Runat="server" ID="Cal"/>
	
	<asp:Panel Runat="server" ID="NoItemsPanel" EnableViewState="False">
		<dsi:h1 runat="server" ID="H12" NAME="H13">Galleries</dsi:h1>
		<div class="ContentBorder">
			<p align="center">
				There are no foreign galleries here.
			</p>
		</div>
	</asp:Panel>
	
	<asp:Panel Runat="server" ID="ItemsPanel" EnableViewState="False">
		<dsi:h1 runat="server" ID="H13" NAME="H13">Galleries</dsi:h1>
		<div class="ContentBorder">
			<p runat="server" id="PageP" align="center">
				<asp:HyperLink runat="server" id="PrevPageLink"><img src="/gfx/icon-back-12.png" style="margin-right:3px;" width="12" height="21" align="absmiddle" border="0">prev page</asp:HyperLink> ... 
				<asp:HyperLink runat="server" id="NextPageLink">next page<img src="/gfx/icon-forward-12.png" style="margin-left:3px;" width="12" height="21" align="absmiddle" border="0"></asp:HyperLink>
			</p>
			<p class="CleanLinks">
				<asp:DataList runat="server" ID="DataList" RepeatColumns="4" RepeatLayout="Table" RepeatDirection="Horizontal" CellPadding="5" ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Top" Width="100%" ItemStyle-Width="25%" />
			</p>
			<p runat="server" id="PageP1" align="center">
				<asp:HyperLink runat="server" id="PrevPageLink1"><img src="/gfx/icon-back-12.png" style="margin-right:3px;" width="12" height="21" align="absmiddle" border="0">prev page</asp:HyperLink> ... 
				<asp:HyperLink runat="server" id="NextPageLink1">next page<img src="/gfx/icon-forward-12.png" style="margin-left:3px;" width="12" height="21" align="absmiddle" border="0"></asp:HyperLink>
			</p>
		</div>
	</asp:Panel>
	
</asp:Panel>
