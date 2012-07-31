<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Legal.ascx.cs" Inherits="Spotted.Pages.Legal" %>
<dsi:h1 runat="server">Terms and conditions</dsi:h1>
<div class="ContentBorder">
	<p><a href="/pages/legaltermsuser/">User terms and conditions</a><br /><br /><a href="/pages/legaltermspromoter/">Promoter terms and conditions</a></p>
</div>
<dsi:h1 runat="server">Privacy policy</dsi:h1>
<div class="ContentBorder">
	<p><a href="/pages/legalinformationpolicy">Privacy policy</a></p>
</div>
<dsi:h1 runat="server">Development Hell Limited</dsi:h1>
<div class="ContentBorder">
	<p><%= Vars.DSI_POSTAL_DETAILS_HTML %>
	<br /><br /><br />
	<%= Vars.DSI_VAT_DETAILS_HTML%>
	<br /><br />
	<%= Vars.DSI_REGOFFICE_DETAILS_HTML%></p>
</div>
