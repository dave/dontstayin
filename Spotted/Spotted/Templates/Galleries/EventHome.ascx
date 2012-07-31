<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventHome.ascx.cs" Inherits="Spotted.Templates.Galleries.EventHome" %>
<%#NewHtmlStart%>
<a href="<%#CurrentGallery.Url()%>">
	<b><%#CurrentGallery.Name%></b>
</a><br>
<a href="<%#CurrentGallery.Url()%>" class="NoStyle">
	<img src="<%#CurrentGallery.PicPath%>" border="0" class="BorderBlack All" style="margin-top:5px;margin-bottom:5px;" width="100" height="100">
</a><br>
<small>
	Added by <a href="<%#CurrentGallery.Owner.Url()%>" <%#CurrentGallery.Owner.Rollover%>><%#CurrentGallery.Owner.NickNameSafe%></a><br>
	<%#CurrentGallery.LivePhotos%> photo<asp:label Runat="Server" ID="LivePotosPlural"></asp:label>
</small>
<%#NewHtmlEnd%>
