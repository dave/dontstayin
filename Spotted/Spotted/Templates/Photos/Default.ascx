<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Default.ascx.cs" Inherits="Spotted.Templates.Photos.Default" %>
<a href="<%#CurrentPhoto.Url()%>" class="NoStyle"><img src="<%#CurrentPhoto.ThumbPath%>" border="0" class="BorderBlack All" height="<%#CurrentPhoto.ThumbHeight%>" width="<%#CurrentPhoto.ThumbWidth%>"></a>
<asp:PlaceHolder Runat="server" ID="UsrPh"></asp:PlaceHolder>
