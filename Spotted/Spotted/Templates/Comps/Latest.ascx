<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Latest.ascx.cs" Inherits="Spotted.Templates.Comps.Latest" %>
<div class="LatestDiv">
	<table cellpadding="0" cellspacing="0" border="0">
		<tr>
			<td runat="server" id="ImageCell" style="padding-right:3px;padding-top:2px;" valign="top">
				<a href="<%#CurrentComp.Url()%>"><img src="<%#CurrentComp.AnyPicPath%>" align="right" border="0" class="LatestPic" hspace="0" width="50" height="50"></a>
			</td>
			<td valign="top">
				<a href="<%#CurrentComp.Url()%>">Win <%#CurrentComp.Name%></a>
				<%#Details%>
			</td>
		</tr>
	</table>
</div>
