<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuickBrowser.ascx.cs" Inherits="Spotted.Templates.Galleries.QuickBrowser" %>
<%#NewHtmlStart%><a href="<%#CurrentGallery.Url()%>" onclick="SwitchGallery(<%#CurrentGallery.K%>,'<%#Order%>','<%#HttpUtility.UrlEncodeUnicode(HttpUtility.HtmlEncode(CurrentGallery.Name)).Replace("'","\\'")%>',<%#CurrentGallery.LivePhotos%>);return false;"><b><%#CurrentGallery.Name%></b><br>
<img src="<%#CurrentGallery.PicPath%>" border="0" class="BorderBlack All" style="margin-top:5px;margin-bottom:5px;" width="100" height="100"></a><br>
<small>
	Added by <a href="<%#CurrentGallery.Owner.Url()%>" <%#CurrentGallery.Owner.Rollover%>><%#CurrentGallery.Owner.NickNameSafe%></a><br>
	<%#CurrentGallery.LivePhotos%> photo<asp:label Runat="Server" ID="LivePotosPlural"></asp:label>: <a href="<%#CurrentGallery.Url()%>" onclick="SwitchGallery(<%#CurrentGallery.K%>,'<%#Order%>','<%#HttpUtility.UrlEncodeUnicode(HttpUtility.HtmlEncode(CurrentGallery.Name)).Replace("'","\\'")%>',<%#CurrentGallery.LivePhotos%>);return false;">quick browser</a> or <a href="<%#CurrentGallery.PagedUrl()%>">gallery</a>
</small><%#NewHtmlEnd%>
