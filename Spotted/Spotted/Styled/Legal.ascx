<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Legal.ascx.cs" Inherits="Spotted.Styled.Legal" %>
<h2>Legal information</h2>
<p><a href='<%= this.StyledObject.UrlStyledApp("legaltermsuser")%>' class="Link"><b>User terms and conditions</b></a></p>

<p><a href='<%= this.StyledObject.UrlStyledApp("legalinformationpolicy")%>' class="Link"><b>Privacy policy</b></a></p>
<p><%= Vars.DSI_POSTAL_DETAILS_HTML %>
<br /><br />
<%= Vars.DSI_VAT_DETAILS_HTML%>
<br />
<%= Vars.DSI_REGOFFICE_DETAILS_HTML%></p>
