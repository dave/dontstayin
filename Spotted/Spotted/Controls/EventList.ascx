<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventList.ascx.cs" Inherits="Spotted.Controls.EventList" %>
<%@ Register TagPrefix="DsiControl" TagName="MusicTypeDropDownList" Src="/Controls/MusicTypeDropDownList.ascx" %>

<a name="UcEventList"></a>
<asp:Panel Runat="server" ID="EventsPanel">
	<p runat="server" id="MoreEventsP">
		<table cellpadding="0" cellspacing="0" border="0" style="margin:7px 5px 10px 5px;">
			<tr>
				<td valign="top" style="padding-right:8px;">
					<a href="/pages/calendar" runat="server" id="CalendarLink1"><img src="/gfx/icon-calendar.png" border="0" width="26" height="21"></a>
				</td>
				<td valign="top">
					We're not showing all the events we have listed. Why not check out the 
					<a href="/pages/calendar" runat="server" id="CalendarLink">calendar</a> for full listings.
				</td>
				
			</tr>
		</table>
	</p>
	<p runat="server" id="OnlyPhotosP">
		<table cellpadding="0" cellspacing="0" border="0" style="margin:7px 5px 10px 5px;">
			<tr>
				<td valign="top" style="padding-right:8px;">
					<a href="/pages/calendar"><img src="/gfx/icon-calendar.png" border="0" width="26" height="21"></a>
				</td>
				<td valign="top">
					We're only showing events that have photos added or a spotter at them. 
					Check out the <a href="/pages/calendar">calendar</a> for full listings.
				</td>
				
			</tr>
		</table>
	</p>
	<p runat="server" id="MusicFilterP">
		<table cellpadding="0" cellspacing="0" border="0" style="margin:7px 5px 10px 5px;">
			<tr>
				<td valign="top" style="padding-right:8px;">
					<img src="/gfx/icon-music.png" border="0" width="26" height="21">
				</td>
				<td valign="top">
					<DsiControl:MusicTypeDropDownList runat="server"></DsiControl:MusicTypeDropDownList>
				</td>
			</tr>
		</table>
	</p>
	<p style="padding-top:2px;">
		<asp:DataList Runat="server" ID="EventsDataList" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="1" Width="100%" CellPadding="0" CellSpacing="0" ItemStyle-VerticalAlign="Top" />
	</p>
	<p runat="server" id="MoreEventsBottomP" style="margin-top:0px;">
		<b>To show full listings for <asp:label Runat="server" ID="CalendarLinkBottomDateLabel"/>, see 
		the <a href="/pages/calendar" runat="server" id="CalendarLinkBottom">calendar</a>.</b>
	</p>
	<p>
		If your event isn't listed, <a href="/pages/events/edit" runat="server" id="AddEventLink1">why don't you add it</a>?
	</p>
</asp:Panel>
<asp:Panel Runat="server" ID="NoEventsPanel">
	<p runat="server" id="MusicFilterP1">
		<table cellpadding="0" cellspacing="0" border="0" style="margin:7px 5px 10px 5px;">
			<tr>
				<td valign="top" style="padding-right:8px;">
					<img src="/gfx/icon-music.png" border="0" width="26" height="21">
				</td>
				<td valign="top">
					<DsiControl:MusicTypeDropDownList runat="server"></DsiControl:MusicTypeDropDownList>
				</td>
			</tr>
		</table>
	</p>
	<p>
		We don't have any events listed. Why don't you <a href="/pages/events/edit" runat="server" id="AddEventLink">add one</a>?
	</p>
</asp:Panel>
