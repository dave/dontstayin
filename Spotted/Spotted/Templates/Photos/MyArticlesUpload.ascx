<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyArticlesUpload.ascx.cs" Inherits="Spotted.Templates.Photos.MyArticlesUpload" %>
<a name="Photo<%#CurrentPhoto.K%>"></a>
<img src="<%#CurrentPhoto.ThumbPath%>" class="BorderBlack All" onmouseover="stm('<img src=<%#CurrentPhoto.WebPath%> width=<%#CurrentPhoto.WebWidth%> height=<%#CurrentPhoto.WebHeight%> class=Block />');" onmouseout="htm();" height="<%#CurrentPhoto.ThumbHeight%>" width="<%#CurrentPhoto.ThumbWidth%>">
