<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Places.ascx.cs" Inherits="Spotted.Pages.Countries.Places" %>

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
					<asp:Panel Runat="server" ID="NoPlaceSelectedHomeCountryPanel">
						<p>
							On this page we're only showing places in your <b>home country</b>.
							The flag of your home country is displayed at the top of the page 
							to the right. To change your home country, use the 
							<b>Make this my home country</b> link on the country home page. 
							You can find links to all the <b>country home pages</b> on the 
							<a href="/pages/countries/list">countries list</a> page.
						</p>
					</asp:Panel>
					<asp:Panel Runat="server" ID="NoPlaceSelectedCountryPanel">
						<p>
							On this page we're only showing places in 
							<a href="" runat="server" id="NoPlaceSelectedCountryLink"></a>.
						</p>
					</asp:Panel>
					<p>
						You can tell us where you go out by visiting the 
						<a href="/pages/placesivisit">places I visit</a> page. 
					</p>
					<p>
						If your location isn't listed, choose the nearest large town.
					</p>
					<p>		
						<b>
							We're not adding any more UK towns to the list - there's just too many for 
							the drop-downs. You'll have to look at a map and use the nearest big town.
						</b>
					</p>
					<p>
						If there's a large town outside the UK that's missing, send 
						<a href="/members/daveb-dsi">DaveB</a> a private message and he can add it.
					</p>
				</td>
			</tr>
		</table>
		
		<p>
			Order by: 
			<asp:LinkButton Runat="server" OnCommand="ReOrder" CommandArgument="Size" ID="SizeOrderLink" CausesValidation="False">size</asp:LinkButton>, 
			<asp:LinkButton Runat="server" OnCommand="ReOrder" CommandArgument="Name" ID="NameOrderLink" CausesValidation="False">name</asp:LinkButton>, 
			<asp:LinkButton Runat="server" OnCommand="ReOrder" CommandArgument="Latitude" ID="LatitudeOrderLink" CausesValidation="False">latitude (north -&gt; south)</asp:LinkButton>,
			<asp:LinkButton Runat="server" OnCommand="ReOrder" CommandArgument="Longitude" ID="LongitudeOrderLink" CausesValidation="False">longitude (west -&gt; east)</asp:LinkButton>,
			<asp:LinkButton Runat="server" OnCommand="ReOrder" CommandArgument="Region" ID="RegionOrderLink" CausesValidation="False">region</asp:LinkButton> or 
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
