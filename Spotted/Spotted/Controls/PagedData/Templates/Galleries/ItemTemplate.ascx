<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemTemplate.ascx.cs" Inherits="Spotted.Controls.PagedData.Templates.Galleries.ItemTemplate" %>
<table>
	<tr>
		<td>
			<a href="{<%=Columns.Url%>}">
				<img src="{<%=Columns.PicPath%>}" border="0" class="BorderBlack All" align="left" style="margin-right:5px;margin-bottom:2px;" width="40" height="40" />
			</a>
		</td>	
		<td>
			<b>{<%=Columns.NewHtmlTitle%>}</b><a href="{<%= Columns.Url %>}">{<%=Columns.Name%>}</a> from <a href="{<%= Columns.EventUrl %>}" />{<%= Columns.EventName%>}</a> @ <a href="{<%= Columns.VenueUrl %>}" />{<%= Columns.VenueName%>}</a> on {<%= Columns.Date  %>}</a><br>
			<small>
				Added by <a href="{<%=Columns.OwnerUrl%>}" {<%= Columns.OwnerRollover %>} >{<%=Columns.OwnerNickNameSafe%>}</a><br>
				Photos: {<%=Columns.LivePhotos%>} <a href="{<%=Columns.Url%>}">quick browser</a> or <a href="{<%=Columns.PagedUrl%>}">gallery</a>
			</small>
		</td>
	</tr>
</table>
