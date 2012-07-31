<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GalleriesByEvent.ascx.cs" Inherits="Spotted.Controls.GalleriesByEvent" %>

<p><b><%= ThisEvent.FriendlyHtml(true, true, true, false) %></b></p>

<p runat="server" id="uiNoGalleriesForThisEventP" visible="false">We don't have any photos uploaded for this event yet.
Click "I was there" on the <a href="<%= ThisEvent.Url() %>">event page</a> and we'll send you notifications when new galleries are uploaded.</p>
<p class="CleanLinks">
	<asp:DataList runat="server" ID="uiGalleriesDataList" RepeatColumns="4" RepeatLayout="Table" RepeatDirection="Horizontal" CellPadding="5" ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Top" Width="100%"  ItemStyle-Width="25%"/>
</p>

<asp:Panel runat="server" ID="uiGalleriesShowAllLinkPanel">
	<p class="BigCenter">
		<a href="<%= ThisEvent.Url() %>#galleries">Show all galleries for <%= ThisEvent.Name%></a>
	</p>
</asp:Panel>
