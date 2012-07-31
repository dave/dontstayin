<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NextEventBoxLarge.ascx.cs" Inherits="Spotted.Templates.Events.NextEventBoxLarge" %>
<%@ Import Namespace="Bobs" %>
<asp:Panel Runat="server" ID="SmallItem" HorizontalAlign="Center">
	<p>
		<a href="<%#CurrentEvent.Url()%>"><b><%#CurrentEvent.Name%></b></a><br>
		<a href="<%#CurrentEvent.Url()%>" runat="server" id="PicLink"><img src="<%#CurrentEvent.AnyPicPath%>" border="0" width="100" height="100" class="BorderBlack All" style="margin-bottom:2px;margin-top:5px;"></a><br>
		<small>@ <a href="<%#CurrentEvent.Venue.Url()%>"><%#CurrentEvent.Venue.Name%></a><br>
		<a href="<%#CurrentEvent.Venue.Place.Url()%>"><%#CurrentEvent.Venue.Place.Name%></a><br>
		<%#CurrentEvent.FriendlyDate(false)%></small>
	</p>
</asp:Panel>
