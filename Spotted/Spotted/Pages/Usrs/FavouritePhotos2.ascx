<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FavouritePhotos2.ascx.cs" Inherits="Spotted.Pages.Usrs.FavouritePhotos2" %>
<%@ Register TagPrefix="Controls" TagName="Cal" Src="/Controls/Cal.ascx" %>
<asp:Panel Runat="server" ID="PanelPhotos">
	<dsi:UsrIntro runat="server" ID="UsrIntro">
		<p>
			This page shows all <a href="<%= ThisUsr.Url() %>" <%= ThisUsr.Rollover %>><%= ThisUsr.NickName %>'s</a> favourite photos.
			You can look for photos taken on a certain day by using the calendar:
		</p>
		<p>
			<a href="<%= ThisUsr.UrlFavouritePhotosMonth(DateTime.Today) %>"><img src="/gfx/icon-calendar.png" style="margin-right:3px;" border="0" height="21" width="26" align="absmiddle">Calendar</a>
		</p>
	</dsi:UsrIntro>
	
	<Controls:Cal Runat="server" ID="Cal"/>
	
	<asp:Panel Runat="server" ID="NoPhotosPanel" EnableViewState="False">
		<dsi:h1 runat="server" ID="H12" NAME="H13">Photos</dsi:h1>
		<div class="ContentBorder">
			<p align="center">
				<a href="<%= ThisUsr.Url() %>" <%= ThisUsr.Rollover %>><%= ThisUsr.NickName %></a> hasn't got any photos here.
			</p>
		</div>
	</asp:Panel>
	
	<asp:Panel Runat="server" ID="PhotosPanel" EnableViewState="False">
		<dsi:h1 runat="server" ID="H13" NAME="H13">Photos</dsi:h1>
		<div class="ContentBorder">
			<p runat="server" id="PhotosPageP" align="center">
				<asp:HyperLink runat="server" id="PhotosPrevPageLink"><img src="/gfx/icon-back-12.png" style="margin-right:3px;" width="12" height="21" align="absmiddle" border="0">prev page</asp:HyperLink> ... 
				<asp:HyperLink runat="server" id="PhotosNextPageLink">next page<img src="/gfx/icon-forward-12.png" style="margin-left:3px;" width="12" height="21" align="absmiddle" border="0"></asp:HyperLink>
			</p>
			<p class="CleanLinks">
				<asp:DataList runat="server" ID="PhotosDataList" RepeatColumns="3" RepeatLayout="Table" RepeatDirection="Horizontal" CellPadding="5" ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Top" Width="100%" ItemStyle-Width="33%" />
			</p>
			<p runat="server" id="PhotosPageP1" align="center">
				<asp:HyperLink runat="server" id="PhotosPrevPageLink1"><img src="/gfx/icon-back-12.png" style="margin-right:3px;" width="12" height="21" align="absmiddle" border="0">prev page</asp:HyperLink> ... 
				<asp:HyperLink runat="server" id="PhotosNextPageLink1">next page<img src="/gfx/icon-forward-12.png" style="margin-left:3px;" width="12" height="21" align="absmiddle" border="0"></asp:HyperLink>
			</p>
		</div>
	</asp:Panel>
	
</asp:Panel>
