<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DavesTest.ascx.cs" Inherits="Spotted.Pages.DavesTest" %>
<dsi:h1 runat="server">Test</dsi:h1>
<div class="ContentBorder">
	<p>
		Hello
	</p>
	<p>
		<a href="/pages/davestest">reset</a>
	</p>
	<p>
		<asp:Button runat="server" OnClick="ServerClick" ID="MyAspButton" Text="Asp button click" />
	</p>
	<p>
		<button runat="server" onserverclick="ServerClick" id="MyButton">Html button click</button>
	</p>
	<p style="background-color:#ccffcc; display:none;" runat="server" id="ServerP">
		Server click!!!
	</p>
	<p style="background-color:#ccccff; display:none;" runat="server" id="ClientP">
		Client click!!!
	</p>
</div>
