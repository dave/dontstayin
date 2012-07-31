<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BannerDesignAnimatedGif.ascx.cs" Inherits="Spotted.Blank.BannerDesignAnimatedGif" %>

<link rel="stylesheet" type="text/css" href="/support/style.css"/>
<style>.ClearAfter:after {	content: "<%= Vars.Opera ? "" : "." %>"; }</style>
<script>window.focus();</script>
<dsi:h1 runat="server" ID="H13dx">DontStayIn design service</dsi:h1>
<div class="ContentBorder">
	<h2>
		Animated gif design service
	</h2>
	<ul>
		<li>Simple animation - three to five frames</li>
		<li>Takes roughly two working days after logos and base artwork are received</li>
		<li>Examples:</li>
	</ul>
	<p style="margin-left:41px;">
		<a href="http://www.dontstayin.com/" target="_blank"><img src="<%= Bobs.Storage.Path(new Guid("cba01318-d2e7-4a3b-98d1-7e9fcef249c3"), "gif") %>" width="191" height="191" border="0" style="border:1px solid #000000;margin-right:8px;" /></a>
		<a href="http://www.dontstayin.com/" target="_blank"><img src="<%= Bobs.Storage.Path(new Guid("619bb45e-838f-4e31-9388-d8f7edf78bb3"), "gif") %>" width="191" height="191" border="0" style="border:1px solid #000000;" /></a>
	</p>
</div>
