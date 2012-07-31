<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BannerDesignStaticJpg.ascx.cs" Inherits="Spotted.Blank.BannerDesignStaticJpg" %>

<link rel="stylesheet" type="text/css" href="/support/style.css"/>
<style>.ClearAfter:after {	content: "<%= Vars.Opera ? "" : "." %>"; }</style>
<script>window.focus();</script>
<dsi:h1 runat="server" ID="H13dx">DontStayIn design service</dsi:h1>
<div class="ContentBorder">
	<h2>
		Static jpg design service
	</h2>
	<ul>
		<li>Static (no animation)</li>
		<li>Takes roughly one working day after logos and base artwork are received</li>
		<li>Examples:</li>
	</ul>
	<p style="margin-left:41px;">
		<a href="http://www.dontstayin.com/" target="_blank"><img src="<%= Bobs.Storage.Path(new Guid("37e26a80-5eac-4d1e-a49d-d900bf4c0438")) %>" width="191" height="191" border="0" style="border:1px solid #000000;margin-right:8px;" /></a>
		<a href="http://www.dontstayin.com/" target="_blank"><img src="<%= Bobs.Storage.Path(new Guid("0eba8e73-335b-48fb-9630-7b732ad0203d"), "gif") %>" width="191" height="191" border="0" style="border:1px solid #000000;" /></a>
	</p>
</div>
