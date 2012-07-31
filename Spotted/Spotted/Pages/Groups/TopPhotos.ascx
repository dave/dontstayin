<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopPhotos.ascx.cs" Inherits="Spotted.Pages.Groups.TopPhotos" %>

<%@ Register TagPrefix="Spotted" TagName="Cal" Src="/Controls/Cal.ascx" %>
<asp:Panel Runat="server" ID="PanelItems">
	<dsi:GroupIntro runat="server" ID="GroupIntro">
		<p>
			This page shows all the top photos in the <a href="<%= CurrentGroup.Url() %>"><%= CurrentGroup.FriendlyName %></a> group.
			You can also look for top photos by date using the calendar:
		</p>
		<p>
			<a href="<%= CurrentGroup.UrlGroupPhotosMonth(DateTime.Today) %>"><img src="/gfx/icon-calendar.png" style="margin-right:3px;" border="0" height="21" width="26" align="absmiddle">Calendar</a>
		</p>
	</dsi:GroupIntro>
	
	<Spotted:Cal Runat="server" ID="Cal"/>
	
	<asp:Panel Runat="server" ID="NoItemsPanel" EnableViewState="False">
		<dsi:h1 runat="server" ID="H12" NAME="H13">Top photos</dsi:h1>
		<div class="ContentBorder">
			<p align="center">
				No top photos here!
			</p>
		</div>
	</asp:Panel>
	
	<asp:Panel Runat="server" ID="ItemsPanel" EnableViewState="False">
		<dsi:h1 runat="server" ID="H13" NAME="H13">Top photos</dsi:h1>
		<div class="ContentBorder">
			<p runat="server" id="PageP" align="center">
				<asp:HyperLink runat="server" id="PrevPageLink"><img src="/gfx/icon-back-12.png" style="margin-right:3px;" width="12" height="21" align="absmiddle" border="0">prev page</asp:HyperLink> ... 
				<asp:HyperLink runat="server" id="NextPageLink">next page<img src="/gfx/icon-forward-12.png" style="margin-left:3px;" width="12" height="21" align="absmiddle" border="0"></asp:HyperLink>
			</p>
			<p class="CleanLinks">
				<asp:DataList runat="server" ID="DataList" RepeatColumns="3" RepeatLayout="Table" RepeatDirection="Horizontal" CellPadding="5" ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Top" Width="100%" ItemStyle-Width="33%" />
			</p>
			<p runat="server" id="PageP1" align="center">
				<asp:HyperLink runat="server" id="PrevPageLink1"><img src="/gfx/icon-back-12.png" style="margin-right:3px;" width="12" height="21" align="absmiddle" border="0">prev page</asp:HyperLink> ... 
				<asp:HyperLink runat="server" id="NextPageLink1">next page<img src="/gfx/icon-forward-12.png" style="margin-left:3px;" width="12" height="21" align="absmiddle" border="0"></asp:HyperLink>
			</p>
		</div>
	</asp:Panel>
	
</asp:Panel>
