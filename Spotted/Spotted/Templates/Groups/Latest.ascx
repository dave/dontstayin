<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Latest.ascx.cs" Inherits="Spotted.Templates.Groups.Latest" %>
<div class="LatestDiv">
	<a name="LatestGroupK-<%#CurrentGroup.K%>"></a>
	<table cellpadding="0" cellspacing="0" border="0">
		<tr>
			<td runat="server" id="ImageCell" style="padding-right:3px;padding-top:2px;" valign="top">
				<a href="<%#CurrentGroup.Url()%>"><img src="<%#CurrentGroup.PicPath%>" align="right" border="0" class="LatestPic" hspace="0" width="50" height="50"></a>
			</td>
			<td valign="top">
				<a href="<%#CurrentGroup.Url()%>"><%#CurrentGroup.FriendlyName%></a><br>
				<small><%#CurrentGroup.Description%></small>
			</td>
		</tr>
	</table>
</div>
