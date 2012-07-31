<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GalleriesBySpotter.ascx.cs" Inherits="Spotted.Controls.GalleriesBySpotter" %>

<p runat="server" id="uiNoGalleriesForThisSpotterP" visible="false">This spotter hasn't uploaded any galleries yet!</p>

<p class="CleanLinks">
	<asp:DataList runat="server" ID="uiGalleriesDataList" RepeatColumns="4" RepeatLayout="Table" RepeatDirection="Horizontal" CellPadding="5" ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Top" Width="100%"  ItemStyle-Width="25%"/>
</p>
<asp:Panel Runat="server" ID="uiGalleriesShowAllLinkPanel">
	<p class="BigCenter">
		<a href="/" runat="server" id="AllGalleriesLink">Show all XXX's galleries</a>
	</p>
</asp:Panel>
