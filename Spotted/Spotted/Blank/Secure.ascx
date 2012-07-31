<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Secure.ascx.cs" Inherits="Spotted.Blank.Secure" %>

<script>window.focus();</script>
<link rel="stylesheet" type="text/css" href="/support/style.css?a=2"/>
<style>.ClearAfter:after {	content: "<%= Vars.Opera ? "" : "." %>"; }</style>
<table cellpadding=0 cellspacing=0 border=0><tr><td>
<div class="Content">
<dsi:h1 runat="server" ID="IntroHeader">Secure pages</dsi:h1>
<div class="ContentBorder">
	<p>
		This page is using a secure server. All transactions carried 
		out using this page are protected by this secure server.
	</p>
	<p>
		You can tell we are using a secure server because of the 
		padlock symbol that appears on the browser window below, 
		and also the website URL changes to https:// (see below).
	</p>
	<table width="100%">
		<tr>
			<td width="50%" align="center">
				<img src="/gfx/secure1.gif" width="161" height="132">
			</td>
			<td width="50%" align="center">
				<img src="/gfx/secure2.gif" width="161" height="132">
			</td>
		</tr>
	</table>
	<p>
		SSL is Short for Secure Sockets Layer, a protocol developed 
		by Netscape for transmitting private documents via the 
		Internet. SSL works by using a private key to encrypt data 
		that's transferred over the SSL connection. Both Netscape 
		Navigator and Internet Explorer support SSL, and many Web 
		sites use the protocol to obtain confidential user information, 
		such as credit card numbers. By convention, URLs that 
		require an SSL connection start with https: instead of http:.
	</p>
</div>
</div>
</td>
</tr>
</table>
