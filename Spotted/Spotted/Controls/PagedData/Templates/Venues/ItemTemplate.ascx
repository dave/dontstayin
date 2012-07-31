<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemTemplate.ascx.cs" Inherits="Spotted.Controls.PagedData.Templates.Venues.ItemTemplate" %>
<table>
	<tr>
		<td valign="top" align="right" style="padding-top:1px; padding-right:7px;" rowspan="2">
			<a href="{<%= Columns.Url %>}"><img src="{<%=Columns.AnyPicPath%>}" width="50" height="50" class="BorderBlack All" align="top" border="0"></a></td>
		<td valign="top" style="padding-left:2px;padding-right:10px;" width="100%">
			<b><a href="{<%=Columns.Url%>}">{<%=Columns.Name%>}</a></b>
			<br />Next event: {<%= Columns.NextEventDate %>} <a href={<%=Columns.NextEventUrl%>} >{<%=Columns.NextEventName%>}</a> 
			<br />Latest chat: {<%= Columns.LastChatDate %>} <a href={<%=Columns.LastChatUrl%>} >{<%=Columns.LastChatName%>}</a> 
		</td>
	</tr>
</table>
