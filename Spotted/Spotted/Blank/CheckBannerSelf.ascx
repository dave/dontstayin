<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CheckBannerSelf.ascx.cs" Inherits="Spotted.Blank.CheckBannerSelf" %>

<script>window.focus();</script>
<link rel="stylesheet" type="text/css" href="/support/style.css"/>
<style>.ClearAfter:after {	content: "<%= Vars.Opera ? "" : "." %>"; }</style>
<table cellpadding=0 cellspacing=0 border=0><tr><td>
<div class="Content">
	<dsi:h1 runat="server" ID="H12" NAME="H11">Banner test</dsi:h1>
	<div class="ContentBorder">
		<h2>
			This banner is using the linkTag correctly.
		</h2>
		<h2>
			This page should load in THE MAIN WINDOW! If this page loaded in a new window (a popup), the banner is NOT using the targetTag correctly.
		</h2>
		
		<p>
			<a href="" onclick="window.close();return false;">Click here to close this window if it's a popup</a>
		</p>
		<p>
			<a href="" onclick="history.go(-1);return false;">Click here to go back if it's the main window</a>
		</p>
	</div>
</div>
