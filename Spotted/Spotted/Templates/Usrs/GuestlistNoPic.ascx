<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GuestlistNoPic.ascx.cs" Inherits="Spotted.Templates.Usrs.GuestlistNoPic" %>
<div style="padding-left:2px;padding-right:10px;font-size:18px;font-weight:bold;font-family: Arial, sans-serif;line-height:130%;">
	<%#CurrentUsr.FirstName%> <%#CurrentUsr.LastName%> <span style="color:cccccc;">(<%#CurrentUsr.NickNameSafe%>)</span>
</div>
