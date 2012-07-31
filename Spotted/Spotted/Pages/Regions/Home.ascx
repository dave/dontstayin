<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Home.ascx.cs" Inherits="Spotted.Pages.Regions.Home" %>

<asp:Panel ID="Panel1" Runat="server" EnableViewState="false">
	<dsi:h1 runat="server" id="PageHeadingNoPlace">Places in ???</dsi:h1>
	<div class="ContentBorder">
		<table cellpadding="0" cellspacing="0" border="0" width="100%">
			<tr>
				<td valign="top" style="padding-right:7px;">
					<p>
						<a href="" runat="server" id="NoPlaceSelectedFlagAnchor"><img src="" runat="server" id="NoPlaceSelectedFlagImg" class="BorderBlack All" border="0"/></a>
					</p>
				</td>
				<td width="100%" valign="top">
					<p>
						On this page we're only showing places in 
						<asp:Label runat="server" ID="RegionNameLabel" />, 
						<a href="" runat="server" id="NoPlaceSelectedCountryLink"></a>.
					</p>
					<p>
						You can tell us where you go out by visiting the 
						<a href="/pages/placesivisit">places I visit</a> page. 
					</p>
				</td>
			</tr>
		</table>
		
		<p>
			Order by:
			<asp:LinkButton Runat="server" OnCommand="ReOrder" CommandArgument="Size" ID="SizeOrderLink" CausesValidation="False">size</asp:LinkButton>, 
			<asp:LinkButton Runat="server" OnCommand="ReOrder" CommandArgument="Name" ID="NameOrderLink" CausesValidation="False">name</asp:LinkButton>, 
			<asp:LinkButton Runat="server" OnCommand="ReOrder" CommandArgument="Latitude" ID="LatitudeOrderLink" CausesValidation="False">latitude (north -&gt; south)</asp:LinkButton>,
			<asp:LinkButton Runat="server" OnCommand="ReOrder" CommandArgument="Longitude" ID="LongitudeOrderLink" CausesValidation="False">longitude (west -&gt; east)</asp:LinkButton> or 
			<asp:LinkButton Runat="server" OnCommand="ReOrder" CommandArgument="Event" ID="EventOrderLink" CausesValidation="False">events</asp:LinkButton>
		</p>
		<p>
			Chose a place by clicking below:
		</p>
		<asp:DataList Runat="server" ID="PlacesDataList" OnItemDataBound="PlacesDataList_OnItemDataBound" ItemStyle-Width="33%" RepeatDirection="Vertical" RepeatColumns="3" CellSpacing="3" Width="100%" CssClass="CleanLinks" />
		<p>
			<small>The numbers in brackets is the number of events we have in our database in this place.</small>
		</p>
	</div>
</asp:Panel>
