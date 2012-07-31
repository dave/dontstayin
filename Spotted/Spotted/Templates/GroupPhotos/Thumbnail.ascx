<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Thumbnail.ascx.cs" Inherits="Spotted.Templates.GroupPhotos.Thumbnail" %>
<a href="<%#CurrentPhoto.Url()%>"><img src="<%#CurrentPhoto.ThumbPath%>" border="0" style="margin-bottom:3px;" class="BorderBlack All" height="<%#CurrentPhoto.ThumbHeight%>" width="<%#CurrentPhoto.ThumbWidth%>"></a><br>
<%#HttpUtility.HtmlEncode(CurrentPhoto.JoinedGroupPhoto.Caption)%><br>
<small><%#Cambro.Misc.Utility.FriendlyDate(CurrentPhoto.JoinedGroupPhoto.DateTime,true,false)%><br>
<asp:PlaceHolder Runat="server" ID="UsrPh"></asp:PlaceHolder><asp:Label Runat="server" ID="StatsLabel"/></small>
