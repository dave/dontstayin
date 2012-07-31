<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Guestlist.ascx.cs" Inherits="Spotted.Templates.Usrs.Guestlist" %>
<table cellspacing="5" cellpadding="0" width="100%">
	<tr>
		<td valign="top" align="right" style="padding-top:1px; padding-right:7px;border-right:1px solid #999999;" rowspan="2">
			<a href="<%#CurrentUsr.Url()%>"><img src="<%#CurrentUsr.AnyPicPath%>" width="100" height="100" class="BorderBlack All" align="top" border="0" class="BorderBlack All"></a></td>
		<td valign="middle" style="padding-left:2px;padding-right:10px;font-size:18px;font-weight:bold;font-family: Arial, sans-serif;" width="100%">
			<%#CurrentUsr.FirstName%> <%#CurrentUsr.LastName%><br><span style="color:cccccc;"><%#CurrentUsr.NickNameSafe%></span>
		</td>
	</tr>
</table>
<br>
