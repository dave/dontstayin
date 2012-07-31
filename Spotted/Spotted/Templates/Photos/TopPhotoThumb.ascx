<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopPhotoThumb.ascx.cs" Inherits="Spotted.Templates.Photos.TopPhotoThumb" %>
<a href="<%#CurrentPhoto.Url()%>" class="NoStyle"><img src="<%#CurrentPhoto.ThumbPath%>" border="0" style="margin-bottom:3px;display:block;" class="BorderBlack All" height="<%#CurrentPhoto.ThumbHeight%>" width="<%#CurrentPhoto.ThumbWidth%>"></a>
<%#HttpUtility.HtmlEncode(CurrentPhoto.PhotoOfWeekCaption)%><br>
<small><%#Cambro.Misc.Utility.FriendlyDate(CurrentPhoto.PhotoOfWeekDateTime,true,false)%><br>
<asp:PlaceHolder Runat="server" ID="UsrPh"></asp:PlaceHolder><asp:Label Runat="server" ID="StatsLabel"/></small><br />&nbsp;<br />
