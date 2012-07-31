<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SpotterRank.ascx.cs" Inherits="Spotted.Templates.Usrs.SpotterRank" %>
<div style="padding:4px 6px 4px 6px;border-top:1px solid #6B6B6B;" class="ClearAfter">
	<a href="<%#CurrentUsr.Url()%>" <%#CurrentUsr.Rollover%>><img src="<%#CurrentUsr.AnyPicPath%>" height="25" width="25" class="BorderBlack All" align="left" border="0"/></a>
	<b>#<%#CurrentUsr.SpottingsMonthRank%></b> <small>(<%#CurrentUsr.SpottingsMonth.ToString("###,##0")%>)</small><br />
	<b><a href="<%#CurrentUsr.Url()%>" <%#CurrentUsr.Rollover%>><%#CurrentUsr.NickNameSafe%></a></b>
</div>
