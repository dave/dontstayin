<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RecentDonator.ascx.cs" Inherits="Spotted.Templates.Usrs.RecentDonator" %>
<div style="padding:4px 6px 4px 6px;border-top:1px solid #6B6B6B;" class="ClearAfter">
	<a href="<%#CurrentUsr.Url()%>" <%#CurrentUsr.Rollover%>><img src="<%#CurrentUsr.AnyPicPath%>" height="25" width="25" class="BorderBlack All" align="left" border="0"/></a>
	<b><a href="<%#CurrentUsr.Url()%>" <%#CurrentUsr.Rollover%>><%#CurrentUsr.NickNameSafe%></a></b>
</div>
