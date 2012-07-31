<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PlaceVenuesList.ascx.cs" Inherits="Spotted.Templates.Venues.PlaceVenuesList" %>
<table cellspacing="5" cellpadding="0" width="100%">
	<tr>
		<td valign="top" align="right" style="padding-top:1px; padding-right:7px;" rowspan="2" class="BorderKeyline Right">
			<a href="<%#CurrentVenue.Url()%>"><img src="<%#CurrentVenue.AnyPicPath%>" width="50" height="50" class="BorderBlack All" align="top" border="0"></a></td>
		<td valign="top" style="padding-left:2px;padding-right:10px;" width="100%">
			<%#Start%><a href="<%#CurrentVenue.Url()%>"><%#CurrentVenue.Name%></a><%#End%>
		</td>
	</tr>
</table>
