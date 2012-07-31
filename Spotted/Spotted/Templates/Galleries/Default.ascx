<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Default.ascx.cs" Inherits="Spotted.Templates.Galleries.Default" %>
<div style=margin-bottom:10px;>
	<a href="<%#CurrentGallery.Url()%>"><img src="<%#CurrentGallery.PicPath%>" border="0" class="BorderBlack All" style="margin-bottom:4px;display:block;" width="100" height="100"></a>
	<div style="padding-top:0px;padding-left:7px;padding-right:7px;">
		<a href="" runat="server" id="NameAnchor"/><br>
		<small>
			<asp:label Runat="server" ID="EventLabel"/> - 
			<asp:label Runat="server" ID="LivePhotosLabel"/>
		</small>
	</div>
</div>
