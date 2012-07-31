<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default1.aspx.cs" Inherits="MixmagGreatest.Default1" %>
	<head>
		<title>Who is the greatest DJ of all time?</title>
	</head>
	<style>
		body{
			font-family: Calibri, Arial, Helvetica, sans-serif;
		}
		
		.GreatestBox
		{
			margin:15px;
			padding-left:15px;
			padding-right:15px;
			border:2px solid #cccccc;
			background-color: #f8f8f8;
		}
		h1
		{
			font-size:30px;
		}
		h2
		{
			font-size:20px;
		}
		
		a.NoStyle:link, 
		a.NoStyle:visited   { background-color:transparent!important; }
		a.NoStyle:hover     { background-color:transparent!important; }
					
		a:link, 
		a:visited   { text-decoration:none!important; color:#000000; background-color:#fecd07; }
		a:hover     { text-decoration:none!important; color:#fecd07; background-color:#000000; }
		
		.ClearAfter:after 
		{
			display: block;
			height: 0;
			clear: both;
			visibility: hidden;
		}
		/* Hides from IE-mac \*/
		* html .ClearAfter {height: 1%;}
		/* End hide from IE-mac */
		
		div.Spacer
		{
			width:520px;
			height:12px;
			background-color:#000000;
			
			margin-top:15px;
			margin-bottom:15px;
		}
		div.SpacerDotted
		{
			width:520px;
			height:1px;
			
			border-top-width:1px;
			border-top-style:dashed;
			border-bottom-color:#000000;
			
			margin-top:15px;
			margin-bottom:15px;
		}
		p.Header
		{
			font-family:Arial Black;
			font-size:25px;
			font-weight:bold;
			margin-top:-10px;
			margin-bottom:-10px;
			padding-top:0px;
			padding-bottom:0px;
		}
		p.Text, div.Text
		{
			font-family:Arial, Sans-Serif;
			font-size:13px;
			font-weight:bold;
			
		}
		
		.TextQuote
		{
			font-family:Arial, Sans-Serif;
			font-size:18px;
			min-height:80px;
		}
	</style>

	<div>

		TESTING
		<!--
		<fb:visible-to-connection>
			<fb:else>
				<div style="position:absolute;">
					<img src="http://greatest.dj/gfx/like-prompt.gif?b=1" width="520" height="483" />
				</div>
			</fb:else>
		</fb:visible-to-connection>

		<fb:visible-to-connection>
			<div style="width:520px;">
				<p>
					<img src="http://greatest.dj/gfx/mm_greatest_dj_withmm_logo.jpg" width="520" height="299" />
				</p>
				<p>
					<div class="Spacer"/>
				</p>
				<p class="Text">
					Forget this year's latest hype DJ. They'd be nowhere without the legends in this poll. Watch as the bona fide biggest stars in dance music explain who they think should be crowned The Greatest DJ Of All Time, then make up your own mind and vote... in the only DJ poll that will ever matter.
				</p>
				<asp:PlaceHolder runat="server" ID="DjsPh" />
				<p>
					<div class="Spacer"/>
				</p>
			</div>
		</fb:visible-to-connection>
		-->
			
	</div>
		
