<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Home.ascx.cs" Inherits="Spotted.Pages.Places.Home" %>
<%@ Register TagPrefix="Spotted" TagName="Latest" Src="/Controls/Latest.ascx" %>


<asp:Panel Runat="server" ID="PlaceSelectedPanel">
	<asp:Panel Runat="server" ID="TopPhotosNewsPanel">
		<dsi:h1 runat="server" id="PageHeading">Place</dsi:h1>
		<div class="ContentBorder">
			<table cellpadding="0" cellspacing="0" border="0" width="100%">
				<tr>
					<td valign="top" runat="server" id="PlaceImgCell" style="padding-right:7px;">
						<p>
							<img src="" runat="server" id="PlacePicImg" class="BorderBlack All" width="100" height="100"/>
						</p>
					</td>
					<td width="100%" valign="top">
						<p>
							<asp:PlaceHolder Runat="server" ID="RegionCountryPh" EnableViewState="False"></asp:PlaceHolder>, and has a population of <asp:Label Runat="server" ID="PlacePopulationLabel"/>.
						</p>
						<p class="CleanLinks">
							<asp:PlaceHolder Runat="server" ID="NearestPlacesPh" EnableViewState="False"></asp:PlaceHolder>
						</p>
						<p>
							<a href='http://www.laterooms.com/en/p4852/Hotels.aspx?k=<%= CurrentPlace.NamePlain %>&mapType=1'><img src="/gfx/icon-hotel.png" width="26" height="21" border="0" align="absmiddle" style="margin-right:3px;" >Find a discount hotel in <%= CurrentPlace.Name %></a><% if (DateTime.Now < new DateTime(2008, 11, 17)){%><span style="font-weight:bold;color:Red;">&nbsp;NEW</span>&nbsp;<%} %>
						</p>

						<p>
							<a runat="server" id="DiscussionLink"><img src="/gfx/icon-discuss.png" width="26" height="21" border="0" align="absmiddle" style="margin-right:3px;">Chat about <asp:Label Runat="server" id="DiscussionLinkPlaceLabel"></asp:Label><asp:Label Runat="server" ID="DiscussionLinkCommentsLabel"/></a>
						</p>
						<p>
							<a href="" runat="server" id="QuickLinksCalendar"><img src="/gfx/icon-calendar.png" width="26" height="21" border="0" align="absmiddle" style="margin-right:3px;"><asp:Label Runat="server" id="CalendarLinkPlaceLabel"></asp:Label> calendar</a>
						</p>
						<p>
							<a href="" runat="server" id="QuickLinksHotTickets"><img src="/gfx/icon-hottickets.png" width="26" height="21" border="0" align="absmiddle" style="margin-right:3px;">Hot <asp:Label Runat="server" id="HotTicketsLinkPlaceLabel"></asp:Label> tickets</a>
						</p>
						<p>
							<a href="" runat="server" id="QuickLinksTickets"><img src="/gfx/icon-tickets.png" width="26" height="21" border="0" align="absmiddle" style="margin-right:3px;"><asp:Label Runat="server" id="TicketsLinkPlaceLabel"></asp:Label> tickets calendar</a>
						</p>
						<p>
							<a href="" runat="server" id="QuickLinksFreeGuestlist"><img src="/gfx/icon-freeguestlist.png" width="26" height="21" border="0" align="absmiddle" style="margin-right:3px;"><asp:Label Runat="server" id="FreeGuestlistLinkPlaceLabel"></asp:Label> Free Guestlist calendar</a></a>
						</p>
						<p>
							<asp:CheckBox Runat="server" Text=" I visit ???" AutoPostBack="True" OnCheckedChanged="VisitCheckChange" ID="VisitCheck"/>
						</p>
					</td>
				</tr>
			</table>
		</div>
		
		<asp:PlaceHolder runat="server" id="PlaceDetailsHtmlPlaceHolder" EnableViewState="false"/>
		
		<asp:DataList runat="server" ID="FeaturedVenuesDataList" RepeatDirection="Horizontal" RepeatColumns="5"/>
		
		<asp:Panel Runat="server" ID="VenuesPanel" EnableViewState="false">
			<a name="Venues"></a>
			<dsi:h1 runat="server" TopLink="true" ID="H13">Top venues in <asp:Label Runat="server" ID="PlaceNameLabel1"/></dsi:h1>
			<div class="ContentBorder" runat="server" id="VenueDataListDiv">
				
				
				<p class="CleanLinks">
					<asp:DataList Runat="server" ID="VenueDataList" Width="100%" ItemStyle-Width="33%" ItemStyle-VerticalAlign="Top"
						RepeatColumns="3" RepeatDirection="Vertical" RepeatLayout="Table" CellPadding="0" CellSpacing="0" />
				</p>
				
				<asp:Panel Runat="server" ID="VenuesMorePanel">
					<p>
						These aren't all the venues in <asp:Label Runat="server" ID="PlaceNameLabel2"/>. <a href="" runat="server" id="VenuesMoreLink">Show all venues</a>.
					</p>
				</asp:Panel>
				<p>
					Are we missing anywhere? <a href="" runat="server" id="SuggestVenueLink">Add a new venue</a>.
				</p>
			</div>
			<div class="ContentBorder" runat="server" id="NoVenuesDiv">
				<p>
					We don't currently have any venues in <asp:Label Runat="server" ID="PlaceNameLabel3"/> in 
					our database. You can <a href="" runat="server" id="SuggestVenueLink1">add a new venue</a>.
				</p>
			</div>
		</asp:Panel>
		
	</asp:Panel>
	
	<Spotted:Latest runat="server" ID="Latest" ParentObjectType="Place" Items="5" />
	
</asp:Panel>
