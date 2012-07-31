<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Default.ascx.cs" Inherits="Spotted.Templates.Usrs.Default" %>
<a href="<%#CurrentUsr.Url()%>"><img src="<%#CurrentUsr.AnyPicPath%>" <%#CurrentUsr.RolloverNoPic%> width="100" height="100" class="BorderBlack All" align="top" border="0"></a><br>
<div style=margin-top:4px;>
	<%#BoldStart%><a href="<%#CurrentUsr.Url()%>"><%#CurrentUsr.NickNameSafe%></a><%#BoldEnd%>
</div>
<div style=margin-top:3px;>
	<%#Online%>
</div>
