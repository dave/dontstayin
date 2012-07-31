<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventHomeSmall.ascx.cs" Inherits="Spotted.Templates.Galleries.EventHomeSmall" %>
<%#NewHtmlStart%><a href="<%#CurrentGallery.Url()%>"><img src="<%#CurrentGallery.PicPath%>" border="0" class="BorderBlack All" align="left" style="margin-right:5px;margin-bottom:2px;" width="40" height="40">
<%#NewHtmlTitle%><%#CurrentGallery.Name%></a><br>
<small>
	Added by <a href="<%#CurrentGallery.Owner.Url()%>" <%#CurrentGallery.Owner.Rollover%>><%#CurrentGallery.Owner.NickNameSafe%></a><br>
	<%#CurrentGallery.LivePhotos%> photo<asp:label Runat="Server" ID="LivePotosPlural"></asp:label>: <a href="<%#CurrentGallery.Url()%>">quick browser</a> or <a href="<%#CurrentGallery.PagedUrl()%>">gallery</a>
</small><%#NewHtmlEnd%>
