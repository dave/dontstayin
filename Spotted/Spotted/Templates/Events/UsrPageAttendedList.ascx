<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UsrPageAttendedList.ascx.cs" Inherits="Spotted.Templates.Events.UsrPageAttendedList" %>
<%@ Import Namespace="Bobs" %>
<table cellpadding="0" cellspacing="0" border="0" style="margin-bottom:10px;">
	<tr>
		<td runat="server" id="ImageCell" style="padding-right:3px;padding-top:2px;" valign="top">
			<a href="<%#CurrentEvent.Url()%>" runat="server" id="PicAnchor"><img src="<%#CurrentEvent.AnyPicPath%>" align="right" border="0" runat="server" id="BigPic" class="BorderBlack All" style="display:none;position:absolute;margin-left:3px;" width="100" height="100"><img src="<%#CurrentEvent.AnyPicPath%>" align="right" border="0" runat="server" id="Pic" class="BorderBlack All" width="50" height="50"></a>
		</td>
		<td valign="top">
			<%#SpotterImg%>
			<a href="<%#CurrentEvent.Url()%>" runat="server" id="TicketsIconAnchor"><img src="/gfx/icon-tickets-small.png" width="20" height="16" align="left" border="0" style="margin-top:2px;margin-right:3px;" onmouseover="stt('Tickets available');" onmouseout="htm();" /></a><a href="<%#CurrentEvent.Url()%>" runat="server" id="FreeGuestlistIconAnchor"><img src="/gfx/icon-freeguestlist-small.png" width="20" height="16" align="left" border="0" style="margin-top:2px;margin-right:3px;" onmouseover="stt('Free Guestlist available');" onmouseout="htm();" /></a>
			<%#CurrentEvent.TitleNoteHtml%><b><a href="<%#CurrentEvent.Url()%>"><%#CurrentEvent.Name%></a></b>
			<small>
				@ <a href="<%#CurrentEvent.Venue.Url()%>"><%#CurrentEvent.Venue.Name%></a> 
				in <a href="<%#CurrentEvent.Venue.Place.Url()%>"><%#CurrentEvent.Venue.Place.Name%></a>, 
				<%#CurrentEvent.FriendlyDate(false)%><br>
				<%#MusicTypeText%>
			</small>
		</td>
	</tr>
</table>
