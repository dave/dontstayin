<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PlaceMissing.ascx.cs" Inherits="Spotted.Blank.PlaceMissing" %>

<script>window.focus();</script>
<link rel="stylesheet" type="text/css" href="/support/style.css"/>
<style>.ClearAfter:after {	content: "<%= Vars.Opera ? "" : "." %>"; }</style>
<table cellpadding=0 cellspacing=0 border=0><tr><td>
<div class="Content">
<dsi:h1 runat="server" ID="IntroHeader">Are we not listing the town you want?</dsi:h1>
<div class="ContentBorder">
	<p>
		What seems to be the problem? Check below for the solutions to two common problems.
	</p>
	<h2>The drop-down lists towns in my country, but the town I want isn't listed!</h2>
	<p>
		Solution... The town drop-downs on DontStayIn only list <b>large towns</b>. We need 
		to do this, or the list would be huge! <b>Just choose the nearest large town that's 
		in the list.</b>
	</p>
			
	<h2>The drop-down lists towns in another country!</h2>

	<p>
		Solution... 
		The town drop-downs on DontStayIn only list towns in your <b>home country</b>.
		You can change your home country - find your country homepage and click <b>Make 
		xxx my home country</b>. After you do that, the drop-down should list towns 
		in your country.
	</p>
</div>
</div>
</td></tr></table>
