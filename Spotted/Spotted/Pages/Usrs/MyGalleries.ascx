<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyGalleries.ascx.cs" Inherits="Spotted.Pages.Usrs.MyGalleries" %>

<%@ Register TagPrefix="Controls" TagName="Cal" Src="/Controls/Cal.ascx" %>
<asp:Panel Runat="server" ID="PanelItems">
	<dsi:UsrIntro runat="server" ID="UsrIntro">
		<p>
			This page shows all the galleries that <a href="<%= ThisUsr.Url() %>" <%= ThisUsr.Rollover %>><%= ThisUsr.NickName %></a> has added to the site. 
			You can look for galleries taken on a certain date by using the calendar:
		</p>
		<p>
			<a href="<%= ThisUsr.UrlGalleriesMonth(DateTime.Today) %>"><img src="/gfx/icon-calendar.png" style="margin-right:3px;" border="0" height="21" width="26" align="absmiddle">Calendar</a>
		</p>
	</dsi:UsrIntro>
	
	<Controls:Cal Runat="server" ID="Cal"/>
	
	<asp:Panel Runat="server" ID="NoItemsPanel" EnableViewState="False">
		<dsi:h1 runat="server" ID="H12" NAME="H13">Galleries</dsi:h1>
		<div class="ContentBorder">
			<p align="center">
				<a href="<%= ThisUsr.Url() %>" <%= ThisUsr.Rollover %>><%= ThisUsr.NickName %></a> doesn't have any galleries here.
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
