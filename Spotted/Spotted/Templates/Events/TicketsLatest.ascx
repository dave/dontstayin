<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TicketsLatest.ascx.cs" Inherits="Spotted.Templates.Events.TicketsLatest" %>
<div class="LatestDiv">
	<table cellpadding="0" cellspacing="0" border="0">
		<tr>
			<td runat="server" id="ImageCell" style="padding-right:3px;padding-top:2px;" valign="top">
				<a href="<%#CurrentEvent.Url()%>"><img src="<%#CurrentEvent.AnyPicPath%>" align="right" border="0" class="LatestPic" hspace="0" width="50" height="50"></a>
			</td>
			<td valign="top">
				<a href="<%#CurrentEvent.Url()%>" runat="server" id="TicketsIconAnchor"><img src="/gfx/icon-tickets-small.png" width="20" height="16" align="left" border="0" style="margin-top:2px;margin-right:3px;" onmouseover="stt('Tickets available');" onmouseout="htm();" /></a><a href="<%#CurrentEvent.Url()%>" runat="server" id="FreeGuestlistIconAnchor"><img src="/gfx/icon-freeguestlist-small.png" width="20" height="16" align="left" border="0" style="margin-top:2px;margin-right:3px;" onmouseover="stt('Free Guestlist available');" onmouseout="htm();" /></a><%#CurrentEvent.TitleNoteHtml%><a href="<%#CurrentEvent.Url()%>"><%#CurrentEvent.Name%></a>
				<small>
					<%#Links%> - <%#CurrentEvent.FriendlyDate(false)%><br />
					<%#Details%><%#MusicTypeText%>
				</small>
			</td>
		</tr>
	</table>
</div>
