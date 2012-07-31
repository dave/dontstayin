<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Fb.ascx.cs" Inherits="Spotted.MixmagGreatest.Fb" %>
<img src="<%= Current.Image200Url %>" border="0" width="200" height="200" class="rounded-corners" style="margin-right:20px; position:absolute;" />
<h1 style="margin-left:220px;"><%= Current.Name %></h1>
<p style="margin-top:-15px; margin-left:220px; margin-bottom:25px;">
	<%= Current.Name %>. <%= Current.Description %>
</p>
<p style="margin-top:-15px; margin-left:220px; margin-bottom:25px; font-size:30px; font-weight:bold;">
	<a href="/<%=Current.UrlName %>">Click here to vote</a>
</p>
<div class="fb-comments" data-href="" data-num-posts="10" data-width="500" runat="server" id="FacebookComments"></div>
