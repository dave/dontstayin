<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatePhotoList.ascx.cs" Inherits="Spotted.Templates.Photos.DatePhotoList" %>
<td><a href="" onclick="showDatePic(<%#CurrentPhoto.K%>,'<%#CurrentPhoto.WebPath%>');return false;" id="P<%#CurrentPhoto.K%>"><img src="<%#CurrentPhoto.IconPath%>" border="0" width="100" height="100"></a></td>
