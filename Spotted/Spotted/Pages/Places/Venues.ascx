<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Venues.ascx.cs" Inherits="Spotted.Pages.Places.Venues" %>

<asp:Panel Runat="server" ID="AllVenuesPanel" EnableViewState="false">
	<dsi:h1 runat="server" id="PageHeadingAllVenues">Place</dsi:h1>
	<div class="ContentBorder">
		<p>
			All venues in <a href="" Runat="server" ID="AllVenuesPlaceLink"/>:
		</p>
		<asp:Panel Runat="server" ID="LargeRegularVenuePanel">
			<h2>Large venues (800+ capacity)</h2>
			<p class="CleanLinks">
				<asp:DataList Runat="server" ID="LargeRegularVenueDataList" Width="100%" RepeatColumns="3" 
					RepeatDirection="Vertical" RepeatLayout="Table" CellPadding="0" CellSpacing="0" ItemStyle-Width="33%" />
			</p>
		</asp:Panel>
		<asp:Panel Runat="server" ID="MediumRegularVenuePanel">
			<h2>Medium venues (300+ capacity)</h2>
			<p class="CleanLinks">
				<asp:DataList Runat="server" ID="MediumRegularVenueDataList" Width="100%" RepeatColumns="3" 
					RepeatDirection="Vertical" RepeatLayout="Table" CellPadding="0" CellSpacing="0" ItemStyle-Width="33%" />
			</p>
		</asp:Panel>
		<asp:Panel Runat="server" ID="SmallRegularVenuePanel">
			<h2>Small venues (under 300 capacity)</h2>
			<p class="CleanLinks">
				<asp:DataList Runat="server" ID="SmallRegularVenueDataList" Width="100%" RepeatColumns="3" 
					RepeatDirection="Vertical" RepeatLayout="Table" CellPadding="0" CellSpacing="0" ItemStyle-Width="33%" />
			</p>
		</asp:Panel>
		<asp:Panel Runat="server" ID="NotRegularVenuePanel">
			<h2>Venues that don't host regular events</h2>
			<p class="CleanLinks">
				<asp:DataList Runat="server" ID="NotRegularVenueDataList" Width="100%" RepeatColumns="3" 
					RepeatDirection="Vertical" RepeatLayout="Table" CellPadding="0" CellSpacing="0" ItemStyle-Width="33%" />
			</p>
		</asp:Panel>
		<p runat="server" id="AllVenuesPanelNoVenues" visible="false">No venues</p>
	</div>
</asp:Panel>
