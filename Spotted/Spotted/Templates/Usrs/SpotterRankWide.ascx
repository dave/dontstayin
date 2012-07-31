<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SpotterRankWide.ascx.cs" Inherits="Spotted.Templates.Usrs.SpotterRankWide" %>
<div style="padding:4px 6px 0px 6px;">
	<b>#<%#CurrentUsr.SpottingsMonthRank%></b> - <a href="<%#CurrentUsr.Url()%>" <%#CurrentUsr.Rollover%>><%#CurrentUsr.NickNameSafe%></a> <small>(<%#CurrentUsr.SpottingsMonth.ToString("###,##0")%>)</small>
</div>
