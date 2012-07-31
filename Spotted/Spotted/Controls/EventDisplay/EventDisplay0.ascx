<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventDisplay0.ascx.cs" Inherits="Spotted.Controls.EventDisplay.EventDisplay0" %>
<div runat="server" id="MainDiv" style="margin-bottom:13px;" class="CleanLinks">
	<table cellpadding="0" cellspacing="0" border="0">
		<tr>
			<td runat="server" id="ImageCell" style="padding-right:3px;padding-top:2px;" valign="top">
				<a href="<%= Url %>"><img src="<%= CurrentEvent.AnyPicPath %>" align="right" border="0" class="BorderBlack All" width="100" height="100" /></a>
			</td>
			
			<td valign="top" width="100%">
				<%=TicketsIconHtml%><%=FreeGuestlistIconHtml%>
				<div style="font-size:13px;<%= CurrentEvent.IsTicketsAvailable || (CurrentEvent.SpotterRequest.HasValue && CurrentEvent.SpotterRequest.Value) ? "padding-top:1px;" :"" %>">
					<%=CurrentEvent.TitleNoteHtml%><b><a href="<%=Url%>"><%=CurrentEvent.Name%></a></b>
				</div>
				<asp:PlaceHolder Runat="server" ID="LinksPh" EnableViewState="False" />
				<div style="margin-top:4px; margin-bottom:4px;">
					<%=EventText%>
				</div>
				<%=MusicTypeText%>
			</td>
		</tr>
	</table>
</div>
