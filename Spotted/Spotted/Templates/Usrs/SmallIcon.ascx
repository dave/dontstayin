<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupMembers.ascx.cs" Inherits="Spotted.Templates.Usrs.GroupMembers" %>
<a href="<%#CurrentUsr.Url()%>"><img src="<%#CurrentUsr.AnyPicPath%>" <%#CurrentUsr.RolloverNoPic%> width="50" height="50" class="BorderBlack All" align="top" border="0"></a><br>
<div style="margin-top:4px;" class="ForceNarrow">
	<%#BuddyStart%><a href="<%#CurrentUsr.Url()%>"><%#CurrentUsr.NickNameSafe%></a><%#BuddyEnd%>
</div>
