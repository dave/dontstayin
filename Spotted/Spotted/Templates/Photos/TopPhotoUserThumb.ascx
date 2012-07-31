<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopPhotoUserThumb.ascx.cs" Inherits="Spotted.Templates.Photos.TopPhotoUserThumb" %>
<a href="<%#CurrentPhoto.Url()%>" class="NoStyle"><img src="<%#CurrentPhoto.ThumbPath%>" border="0" style="margin-bottom:3px;" class="BorderBlack All" height="<%#CurrentPhoto.ThumbHeight%>" width="<%#CurrentPhoto.ThumbWidth%>"></a>
<div style="width:150px; overflow:hidden;"><%#HttpUtility.HtmlEncode(CurrentPhoto.PhotoOfWeekUserCaption)%></div>
<small><%#Cambro.Misc.Utility.FriendlyDate(CurrentPhoto.PhotoOfWeekUserDateTime,true,false)%><br>
<asp:PlaceHolder Runat="server" ID="UsrPh"></asp:PlaceHolder><asp:Label Runat="server" ID="StatsLabel"/></small>
