<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Calendar.ascx.cs" Inherits="Spotted.Controls.Calendar" %>
<%@ Register TagPrefix="DsiControl" TagName="MusicTypeDropDownList" Src="/Controls/MusicTypeDropDownList.ascx" %>

<dsi:h1 runat="server" ID="TabHolder" ClassAttribute="TabHolder">
	<a href="/pages/findevents/mode-calendar" class="TabbedHeading" runat="server" id="EventFinderTab">Event finder</a>
	<a href="/pages/mycalendar" class="TabbedHeading" runat="server" id="MyCalendarTab" onclick="try { return WhenLoggedInAnchor(this); } catch(ex) { return false; }">Suggested events</a>
	<a href="/pages/mycalendar/type-buddy" class="TabbedHeading" runat="server" id="BuddyCalendarTab" onclick="try { return WhenLoggedInAnchor(this); } catch(ex) { return false; }">Where my buddies are going</a>
</dsi:h1>
<div class="ContentBorder" style="padding-right:0px;padding-left:0px;padding-bottom:0px;">	
	<p align="center" style="font-weight:bold;margin-right:8px;margin-left:8px;margin-bottom:0px;">
		<img src="/gfx/icon-calendar.png" border="0" runat="server" width="26" height="21" id="TopIcon">
	</p>
	
	<asp:Panel runat="server" ID="HotTicketsIntroPanel">
	
		<p align="center" style="font-weight:bold;margin-right:8px;margin-left:8px;" runat="server" id="HotTicketsIntroPanelWorldwideP">
			This page shows hot tickets worldwide. <a href="" runat="server" id="HotTicketsIntroPanelWorldwideHomeCountryLink">We can restrict this to ???</a>.
		</p>	
		<p align="center" style="font-weight:bold;margin-right:8px;margin-left:8px;" runat="server" id="HotTicketsIntroPanelBrandP">
			This page shows hot tickets for <a href="" runat="server" id="HotTicketsIntroPanelBrandLink"></a> events.
		</p>
		<p align="center" style="font-weight:bold;margin-right:8px;margin-left:8px;" runat="server" id="HotTicketsIntroPanelNonBrandP">
			This page shows hot tickets for events <asp:Label runat="server" id="HotTicketsIntroPanelNonBrandInAtLabel"/> <a href="" runat="server" id="HotTicketsIntroPanelNonBrandObjectLink"></a>.
		</p>
		
		<p align="center" style="margin-right:8px;margin-left:8px;">
			<small>
				If we're not showing the event you're looking for, how about trying the 
				<a href="" runat="server" id="HotTicketsIntroPanelTicketsCalendarLink">tickets calendar</a>?
			</small>
		</p>
		
	</asp:Panel>
	
	<asp:Panel runat="server" ID="NotHotTicketsIntroPanel">
	
		<asp:Panel runat="server" ID="AllEventsIntroPanel">
			<p align="center" style="font-weight:bold;margin-right:8px;margin-left:8px;">
				This calendar is showing all <asp:Label runat="server" id="AllEventsIntroPanelEventsLabel1"/> worldwide. 
				<a href="" runat="server" id="AllEventsIntroPanelHomeCountryLink">We can restrict this to ???</a>.
			</p>
			<p align="center" style="margin-right:8px;margin-left:8px;">
				<small>
					The <a href="/pages/mycalendar">My calendar</a> page shows events in 
					your area that play music you listen to.
				</small>
			</p>
			<p align="center" style="margin-right:8px;margin-left:8px;">
				<small>
					The <a href="/pages/mycalendar/type-buddy">Buddy calendar</a> 
					shows where you and your buddies are going.
				</small>
			</p>
			<p align="center" style="margin-right:8px;margin-left:8px;">
				<small>
					The <a href="/pages/ticketscalendar">Tickets calendar</a> shows 
					tickets you might be interested in.
				</small>
			</p>
		</asp:Panel>
		
		<asp:Panel runat="server" ID="ObjectCalendarIntroPanel">
			<p align="center" style="font-weight:bold;margin-right:8px;margin-left:8px;" runat="server" id="ObjectCalendarIntroBrand">
				This calendar shows all <a href="" runat="server" id="ObjectCalendarIntroBrandAnchor"></a> <asp:Label runat="server" id="AllEventsIntroPanelEventsLabel2"/>.
			</p>
			<p align="center" style="font-weight:bold;margin-right:8px;margin-left:8px;" runat="server" id="ObjectCalendarIntroNonBrand">
				This calendar shows all <asp:Label runat="server" id="AllEventsIntroPanelEventsLabel3"/> 
				<asp:Label Runat="server" ID="ObjectCalendarIntroInAtLabel"/> 
				<a href="" runat="server" id="ObjectCalendarIntroObjectLink"></a>.
			</p>
			<p align="center" style="margin-right:8px;margin-left:8px;">
				<small>
					The <a href="/pages/mycalendar">My calendar</a> page shows events in 
					your area that play music you listen to.
				</small>
			</p>
			<p align="center" style="margin-right:8px;margin-left:8px;">
				<small>
					The <a href="/pages/mycalendar/type-buddy">Buddy calendar</a> 
					shows where you and your buddies are going.
				</small>
			</p>
			<p align="center" style="margin-right:8px;margin-left:8px;">
				<small>
					The <a href="/pages/ticketscalendar">Tickets calendar</a> shows 
					tickets you might be interested in.
				</small>
			</p>
		</asp:Panel>
								
		<asp:Panel runat="server" ID="MyCalendarIntroPanel">
			<p align="center" style="font-weight:bold;margin-right:8px;margin-left:8px;">
				This calendar shows events in <a href="/pages/placesivisit">places you visit</a>, 
				that are playing <a href="/pages/mymusic">music you listen to</a>.
			</p>
			<p align="center" style="margin-right:8px;margin-left:8px;">
				<small>
					The <a href="/pages/mycalendar/type-buddy">Buddy calendar</a> 
					shows where you and your buddies are going.
				</small>
			</p>
			<p align="center" style="margin-right:8px;margin-left:8px;">
				<small>
					The <a href="/pages/ticketscalendar">Tickets calendar</a> shows 
					tickets you might be interested in.
				</small>
			</p>
		</asp:Panel>
		
		<asp:Panel runat="server" ID="BuddyCalendarIntroPanel">
			<p align="center" style="font-weight:bold;margin-right:8px;margin-left:8px;">
				This is your buddy calendar. It shows where you and your buddies are going.<br>
				Events in boxes are where you're going.
			</p>
			<p align="center" style="margin-right:8px;margin-left:8px;">
				<small>
					The <a href="/pages/mycalendar">My calendar</a> page shows events in 
					your area that play music you listen to.
				</small>
			</p>
			<p align="center" style="margin-right:8px;margin-left:8px;">
				<small>
					The <a href="/pages/ticketscalendar">Tickets calendar</a> shows 
					tickets you might be interested in.
				</small>
			</p>
		</asp:Panel>
		
		<asp:Panel runat="server" ID="TicketsCalendarIntroPanel">
			<p align="center" style="font-weight:bold;margin-right:8px;margin-left:8px;">
				This is the tickets calendar. It shows events that have tickets available 
				in <a href="/pages/placesivisit">places you visit</a>, that are playing 
				<a href="/pages/mymusic">music you listen to</a>.
			</p>
			<p align="center" style="margin-right:8px;margin-left:8px;">
				<small>
					The <a href="/pages/mycalendar">My calendar</a> page shows events in 
					your area that play music you listen to.
				</small>
			</p>
			<p align="center" style="margin-right:8px;margin-left:8px;">
				<small>
					The <a href="/pages/mycalendar/type-buddy">Buddy calendar</a>
					shows where you and your buddies are going.
				</small>
			</p>
		</asp:Panel>
	
	</asp:Panel>
	
	<asp:Panel Runat="server" ID="MusicTypeDropDownPanel">
		<center>
		<table cellpadding="0" cellspacing="0" border="0" style="margin:7px 5px 10px 5px;">
			<tr>
				<td valign="top" style="padding-right:8px;padding-top:8px;">
					<img src="/gfx/icon-music.png" border="0" width="26" height="21">
				</td>
				<td valign="top" style="padding-top:8px;">
					<DsiControl:MusicTypeDropDownList runat="server"></DsiControl:MusicTypeDropDownList>
				</td>
			</tr>
		</table>
		</center>
	</asp:Panel>
	
	<asp:Panel runat="server" ID="MonthViewPanel">
	
		<p align="center" style="font-weight:bold;margin-right:8px;margin-left:8px;">
			<a href="" runat="server" id="BackLink"></a> - 
			<asp:Label Runat="server" ID="MonthNameLabel"></asp:Label> - 
			<a href="" runat="server" id="NextLink"></a>
		</p>
	
		<asp:PlaceHolder runat="server" ID="uiCalPlaceHolder"></asp:PlaceHolder>
		
		<p align="center" style="font-weight:bold;margin-right:8px;margin-left:8px;">
			<a href="" runat="server" id="BackLink1"></a> - 
			<asp:Label Runat="server" ID="MonthNameLabel1"></asp:Label> - 
			<a href="" runat="server" id="NextLink1"></a>
		</p>
		
	</asp:Panel>

	<asp:Panel runat="server" ID="DayViewPanel">
	
		<p align="center" style="margin-right:8px;margin-left:8px;">
			<a href="" runat="server" id="DayMonthLink"></a>
		</p>
		
		<p align="center" style="font-weight:bold;margin-right:8px;margin-left:8px;">
			<a href="" runat="server" id="DayBackLink"></a> - 
			<asp:Label Runat="server" ID="DayNameLabel"></asp:Label> - 
			<a href="" runat="server" id="DayNextLink"></a>
		</p>

		<p runat="server" id="DayViewNoEventsP">
			<center><small>We don't have any events listed on this day. <asp:Label runat="server" id="MusicFilterLabel1">Check the music filter drop-down above - perhaps you have a music type selected?</asp:Label></small></center>
		</p>
		
		<div style="margin-right:8px;margin-left:8px;margin-top:15px;" runat="server" id="DayViewEventsDiv">
			<asp:DataList runat="server" id="DayViewDataList" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="1" CellPadding="0" CellSpacing="0" ItemStyle-VerticalAlign="Top" />
		</div>
		
		<p align="center" style="font-weight:bold;margin-right:8px;margin-left:8px;">
			<a href="" runat="server" id="DayBackLink1"></a> - 
			<asp:Label Runat="server" ID="DayNameLabel1"></asp:Label> - 
			<a href="" runat="server" id="DayNextLink1"></a>
		</p>
		
		<p align="center" style="margin-right:8px;margin-left:8px;">
			<a href="" runat="server" id="DayMonthLink1"></a>
		</p>
		
	</asp:Panel>
	
	<asp:Panel runat="server" ID="HotTicketsEventListPanel">
	
		<p runat="server" id="HotTicketsEventListNoEventsP">
			<center><small>We don't have any events listed on this day. <asp:Label runat="server" id="MusicFilterLabel2">Check the music filter drop-down above - perhaps you have a music type selected?</asp:Label></small></center>
		</p>
		
		<div style="margin-right:8px;margin-left:8px;margin-top:15px;" runat="server" id="HotTicketsEventListEventsDiv">
			<asp:DataList runat="server" id="HotTicketsEventList" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="1" CellPadding="0" CellSpacing="0" ItemStyle-VerticalAlign="Top" />
		</div>
		
	</asp:Panel>
	
</div>
