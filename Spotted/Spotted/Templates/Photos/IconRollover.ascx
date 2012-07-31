<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IconRollover.ascx.cs" Inherits="Spotted.Templates.Photos.IconRollover" %>
<a href="<%#CurrentPhoto.Url()%>" <%#Attribs%> id="P<%#CurrentPhoto.K%>" class="NoStyle"><img src="<%#CurrentPhoto.IconPath%>" class="BorderBlack All" border="0" width="100" height="100"></a>
