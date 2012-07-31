<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ItemTemplate.ascx.cs" Inherits="Spotted.Controls.PagedData.Templates.Articles.ItemTemplate" %>
<table cellpadding="0" cellspacing="0" border="0">
	<tr>
		<td id="ImageCell" style="padding-right:3px;padding-top:2px;" valign="top">
			<a href="{<%= Columns.Url %>}"><img src="{<%= Columns.PicPath %>}" align="right" border="0" class="LatestPic" hspace="0" width="50" height="50"></a>
		</td>
		<td valign="top">
			<b>{<%= Columns.Date %>} <a href="{<%= Columns.Url %>}">{<%= Columns.Title %>}</a></b>
			<br />{<%= Columns.Summary %>}
			<br>By: <a href="{<%= Columns.OwnerUrl %>}">{<%= Columns.OwnerNickName %>}</a> <a href="{<%= Columns.DiscussionUrl %>}"> Comments:</a> {<%= Columns.Comments %>} Views: {<%= Columns.Views %>}
			
		</td>
	</tr>
</table>
