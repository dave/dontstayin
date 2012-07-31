<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Home.ascx.cs" Inherits="Spotted.Pages.Venues.Home" %>
<%@ Register TagPrefix="Spotted" TagName="EventList" Src="/Controls/EventList.ascx" %>
<%@ Register TagPrefix="Spotted" TagName="Latest" Src="/Controls/Latest.ascx" %>

<asp:Panel Runat="server" id="VenueSelectedPanel">

	<a name="InfoPanel"></a>
	<dsi:h1 runat="server" ID="H12"><asp:Label Runat="server" ID="VenueHeader"/></dsi:h1>
	<div id="Div1" class="ContentBorder" runat="server" EnableViewState="False">
		<table cellpadding="0" cellspacing="0" border="0" width="100%">
			<tr>
				<td valign="top" runat="server" id="VenuePicCell" style="padding-right:7px;">
					<p>
						<img src="" runat="server" id="VenuePicImg" class="BorderBlack All" width="100" height="100"/>
					</p>
				</td>
				<td width="100%" valign="top">
					
					<p><small><asp:Label Runat="server" ID="VenueNameLabel"/> is in <a href="" Runat="server" ID="PlaceNameLink"/></small></p>
					
					<p>
						<span Runat="server" ID="MapSpan">
							<a href="" runat="server" id="MapLink" onclick="mapOpen('');return false;"><img src="/gfx/icon-map.png" width="26" height="21" border="0" align="absmiddle" style="margin-right:3px;">Google map of this venue</a> and <a href="/" runat="server" id="DirectionsLink" onclick="alert('To show directions, you first need to enter your home postcode on the \'My details\' page.');return false;">directions</a>
							<script>
								function mapOpen(url)
								{
									var popUp = window.open(url, popUp, 'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,width=890,height=600');
									popUp.focus();
								}
								function overrideMapOpen(url)
								{
									var popUp = window.open(url, popUp, 'toolbar=1,scrollbars=1,location=0,statusbar=1,menubar=1,resizable=1,width=600,height=500');
									popUp.focus();
								}
							</script>
						</span>
					</p>
					<p>
						<a runat="server" id="DiscussionLink"><img src="/gfx/icon-discuss.png" width="26" height="21" border="0" align="absmiddle" style="margin-right:3px;">Chat about <asp:Label Runat="server" id="DiscussionLinkVenueLabel"></asp:Label><asp:Label Runat="server" ID="DiscussionLinkCommentsLabel"/></a>
					</p>
					<p>
						<a href="" runat="server" id="CalendarLink"><img src="/gfx/icon-calendar.png" width="26" height="21" border="0" align="absmiddle" style="margin-right:3px;"><asp:Label Runat="server" id="CalendarLinkVenueLabel"></asp:Label> calendar</a>
					</p>
					<p runat="server" visible="false">
						<a href="" runat="server" id="HotTicketsLink"><img src="/gfx/icon-hottickets.png" width="26" height="21" border="0" align="absmiddle" style="margin-right:3px;">Hot <asp:Label Runat="server" id="HotTicketsLinkVenueLabel"></asp:Label> tickets</a>
					</p>
					<p runat="server" visible="false">
						<a href="" runat="server" id="TicketsLink"><img src="/gfx/icon-tickets.png" width="26" height="21" border="0" align="absmiddle" style="margin-right:3px;"><asp:Label Runat="server" id="TicketsLinkVenueLabel"></asp:Label> tickets calendar</a>
					</p>
					<p runat="server" visible="false">
						<a href="" runat="server" id="FreeGuestlistLink"><img src="/gfx/icon-freeguestlist.png" width="26" height="21" border="0" align="absmiddle" style="margin-right:3px;"><asp:Label Runat="server" id="FreeGuestlistLinkVenueLabel"></asp:Label> Free Guestlist calendar</a></a>
					</p>
				</td>
			</tr>
		</table>
	</div>
	
	<asp:Panel ID="Panel1" Runat="server" Visible="False" EnableViewState="False">
	<dsi:h1 runat="server" ID="H11">Map!</dsi:h1>
	<div class="ContentBorder">
			<script src="http://maps.google.com/maps?file=api&v=1&key=ABQIAAAAGbW6WzQCNSxIkAXNHaLIIxSMITbL05T4T5ADLKmY_-0Blw8gmRSfcp-4kUleN_HyKZLwyNZwds065Q" type="text/javascript"></script>
		    <div id="map" style="width: 500px; height: 400px"></div>
			<script type="text/javascript">
			//<![CDATA[
		    
			var map = new GMap(document.getElementById("map"));
			map.addControl(new GSmallMapControl());
			map.centerAndZoom(new GPoint(-122.1419, 37.4419), 4);
		    
			//]]>
			</script>

	</div>
	</asp:Panel>
	
	<asp:PlaceHolder Runat="server" ID="VenueDetailsPlainPh" EnableViewState="False"/>
	
	<asp:Panel Runat="server" ID="InfoPanel" EnableViewState="False">
				
		<dsi:h1 runat="server" id="EventBodyTitle">Info</dsi:h1>
		<div class="ContentBorder" style="width:600px;overflow:hidden;">
			<asp:PlaceHolder Runat="server" ID="VenueBody"/>
		</div>
		
	</asp:Panel>

	<Spotted:Latest runat="server" ID="Latest" ParentObjectType="Venue" Items="5" />
	
</asp:Panel>
	
