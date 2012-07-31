<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default3.aspx.cs" Inherits="MixmagGreatest.Default3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html>
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
	<body style="background-image:url(/gfx/background.gif); background-repeat:repeat;">
		<div style="width:520px; padding:15px; background-color:#ffffff; margin-left:auto; margin-right:auto; margin-top:15px; margin-bottom:15px; clear:both;">
			<div style="margin-bottom:15px;">
				<img src="http://greatest.dj/gfx/mm_greatest_dj_withmm_logo.jpg" width="520" height="299" border="0" />
			</div>
			<asp:PlaceHolder runat="server" ID="TestPh" />
			<asp:PlaceHolder runat="server" ID="DjsPh" />
			<p>
				<div class="Spacer"></div>
			</p>
		</div>
	</body>
</html>
