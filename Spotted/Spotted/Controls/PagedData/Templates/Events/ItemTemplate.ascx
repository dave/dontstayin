0<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemTemplate.ascx.cs" Inherits="Spotted.Controls.PagedData.Templates.Events.ItemTemplate" %>
<table>
	<td>
		<a href="{<%=Columns.Url%>}"><img src="{<%=Columns.PicPath%>}" border="0" class="BorderBlack All" align="left" style="margin-right:5px;margin-bottom:2px;" width="40" height="40" /></a>
	</td>
	<td>
		<span style="color:Blue">{<%= Columns.Time %>}</span> <a href={<%=Columns.Url%>} >{<%=Columns.Name%>}</a> @ <a href="{<%= Columns.VenueUrl %>}" >{<%= Columns.Venue%>}</a>
		<br>
		
	</td>
</table>
