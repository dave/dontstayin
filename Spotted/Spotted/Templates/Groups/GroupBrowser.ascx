<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupBrowser.ascx.cs" Inherits="Spotted.Templates.Groups.GroupBrowser" %>
<p class="CleanLinks">
	<a href="<%#CurrentGroup.Url()%>"><b><%#CurrentGroup.FriendlyName%></b></a> <small>- <%#CurrentGroup.TotalMembers.ToString("#,##0")%> member<%#CurrentGroup.TotalMembers==1?"":"s"%>, <%#CurrentGroup.TotalComments.ToString("#,##0")%> comment<%#CurrentGroup.TotalComments==1?"":"s"%></small><br>
	<%#CurrentGroup.Description%>
</p>
