<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Spottings.ascx.cs" Inherits="Spotted.Templates.Usrs.Spottings" %>
<a href="<%#CurrentUsr.Url()%>"><img src="<%#CurrentUsr.AnyPicPath%>" <%#CurrentUsr.RolloverNoPic%> width="50" height="50" class="BorderBlack All" align="top" border="0"></a><br>
<div style="margin-top:4px;" class="ForceNarrow">
	<a href="<%#CurrentUsr.Url()%>"><%#CurrentUsr.NickNameSafe%></a><br />
	<small><a href="<%#CurrentUsr.UrlMyPhotosTakenBy(((Spotted.Master.DsiPage)Page).Url.ObjectFilterUsr)%>">photos</a></small>
</div>
