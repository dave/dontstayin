<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HomeContentTop.ascx.cs" Inherits="Spotted.Pages.Countries.HomeContentTop" %>
<!--%@ OutputCache Duration="600" VaryByCustom="Country;MusicPref;PageName;" VaryByParam="*" %-->


<table cellpadding="0" cellspacing="0" width="100%">
	<tr>
		<td valign="top" width="50%" style="padding-right:5px;">
			<dsi:h1 runat="server" ID="IntroHeader"></dsi:h1>
			<div class="ContentBorder">
				<table cellpadding="0" cellspacing="0" border="0" width="100%">
					<tr>
						<td valign="top" style="padding-right:7px;">
							<p>
								<img src="" runat="server" id="FlagImg" class="BorderBlack All" width="60" height="36"/>
							</p>
						</td>
						<td width="100%" valign="top">
							<p>
								<a runat="server" id="HotTopicsLink"><img src="/gfx/icon-discuss.png" width="26" height="21" border="0" align="absmiddle" style="margin-right:3px;">Hot <asp:Label Runat="server" id="HotTopicsLinkCountryLabel"></asp:Label> forums</a>
							</p>
							<p>
								<a runat="server" id="DiscussionLink"><img src="/gfx/icon-discuss.png" width="26" height="21" border="0" align="absmiddle" style="margin-right:3px;">All <asp:Label Runat="server" id="DiscussionLinkCountryLabel"></asp:Label> chat</a>
							</p>
							<p>
								<a href="" runat="server" id="CalendarLink"><img src="/gfx/icon-calendar.png" width="26" height="21" border="0" align="absmiddle" style="margin-right:3px;"><asp:Label Runat="server" id="CalendarLinkCountryLabel"></asp:Label> calendar</a>
							</p>
							<p>
								<a href="" runat="server" id="HotTicketsLink"><img src="/gfx/icon-hottickets.png" width="26" height="21" border="0" align="absmiddle" style="margin-right:3px;">Hot <asp:Label Runat="server" id="HotTicketsLinkCountryLabel"></asp:Label> tickets</a>
							</p>
							<p>
								<a href="" runat="server" id="TicketsLink"><img src="/gfx/icon-tickets.png" width="26" height="21" border="0" align="absmiddle" style="margin-right:3px;"><asp:Label Runat="server" id="TicketsLinkCountryLabel"></asp:Label> tickets calendar</a>
							</p>
							<p>
								<a href="" runat="server" id="FreeGuestlistLink"><img src="/gfx/icon-freeguestlist.png" width="26" height="21" border="0" align="absmiddle" style="margin-right:3px;"><asp:Label Runat="server" id="FreeGuestlistLinkCountryLabel"></asp:Label> Free Guestlist calendar</a>
							</p>
							<p>
								<a href="/">International home page</a>
							</p>
							<p>
								<b><a href="" runat="server" id="HomePageLink">Make ? my home country</a></b>
							</p>
						</td>
					</tr>
				</table>
			</div>
		</td>
		<td valign="top" width="50%" style="padding-left:5px;">
			<asp:Panel Runat="server" ID="TopPlacesPanel" EnableViewState="False">
				<dsi:h1 runat="server" ID="H12" NAME="H11">Top places</dsi:h1>
				<div class="ContentBorder">
					<p align="center" class="CleanLinks">
						<asp:DataList Runat="server" ID="TopPlacesDataList" RepeatColumns="2" RepeatLayout="Table" RepeatDirection="Horizontal" Width="100%" ItemStyle-Width="50%" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top"/>
					</p>
					<asp:Panel Runat="server" ID="TopPlacesAnchorPanel">
						<p><a href="" runat="server" id="TopPlacesAnchor">Show a list of all places in ???</a></p>
					</asp:Panel>
					<p><small>In brackets is the number of events we have listed</small></p>
				</div>
			</asp:Panel>
		</td>
	</tr>
</table>

<asp:Panel Runat="server" ID="CustomHtmlPanel" EnableViewState="False">
	<dsi:h1 runat="server" ID="CustomHtmlHeader">Check out...</dsi:h1><asp:PlaceHolder Runat="server" ID="CustomHtmlPlaceHolder"/>
</asp:Panel>

<asp:Panel Runat="server" ID="TopPhotosPanel" EnableViewState="False">
	<dsi:h1 runat="server" ID="H11" NAME="H11">Top photos</dsi:h1>
	<div class="ContentBorder">
		<p align="center">
			<asp:DataList Runat="server" ID="TopPhotosDataList" RepeatColumns="4" 
				RepeatLayout="Table" RepeatDirection="Horizontal" Width="100%" 
				ItemStyle-Width="25%" ItemStyle-HorizontalAlign="Center" 
				ItemStyle-VerticalAlign="Top"/>
		</p>
		<p align="center" style="font-weight:bold;font-size:12px;">
			<a href="/pages/top/photos">Top photos archive</a>
		</p>
		<p align="center">
			<small><a href="/groups/top-photo-suggestions">Suggest a top photo</a></small>
		</p>
	</div>
</asp:Panel>
