<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Home.ascx.cs" Inherits="Spotted.Templates.Countries.Home" %>
<table cellspacing="5" cellpadding="0" width="100%">
	<tr>
		<td valign="top" align="right" style="padding-top:1px; padding-right:7px;" rowspan="2">
			<center>
				<a href="<%#CurrentCountry.Url()%>"><img src="<%#CurrentCountry.FlagUrl()%>" width="30" height="18" align="top" border="0" style="margin-bottom:3px;" class="BorderBlack All"></a><br />
				<a href="<%#CurrentCountry.Url()%>"><%#CurrentCountry.FriendlyName%></a>
			</center>
		</td>
	</tr>
</table>
