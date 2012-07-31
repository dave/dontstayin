<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Mixmag.ascx.cs" Inherits="Spotted.Blank.Mixmag" %>
<html>
<style>
	* {font-family:Verdana,Arial,Helvetica,sans-serif;} 

	img.WelcomeHeaderTop
	{
		position:relative;
		left:36px;
	}
	div.WelcomeBox{
		position:relative;
		left:36px;
		padding:3px 13px 3px 13px;
		border-left:3px solid #000000;
		border-right:3px solid #000000;
		border-bottom:3px solid #000000;
		background-color:FECA26;
		margin-bottom:10px;	
		text-align:left;
	}
	.ValidationSummary{
		position:relative;
		left:35px;
		padding:9px 13px 13px 13px;
		border:3px solid #000000;
		background-color:FECA26;
		margin-bottom:10px;	
		text-align:left;
	}
	div{
		font-size:10px;
	}
	td, th{
		font-size:10px;
	}
	td.Large{
		font-size:10px;
	}
	th{
		text-align:left;
		font-weight:normal;
	}

	button, input, select, textarea
	{
		font-size:10px;
	}
	button.LargeButton, input.LargeButton
	{
		font-size:12px;
	}
	p
	{
		margin-top:6px;
		margin-bottom:10px;
	}
	td p
	{
		margin-top:1px;
		margin-bottom:3px;
	}
	td div
	{
		font-size:10px;
	}
	small{
		color:#A58319;
		font-size:10px;
	}
	a:link, 
	a:visited         { color:#000000; }
	a:hover           { color:#FF0000; }

	small a:link, 
	small a:visited   { color:#A58319; }
	small a:hover     { color:#FF0000; }	
	
	p.BigCenter
	{
		font-size:18px;
		font-weight:bold;
		margin-top:10px;
		margin-bottom:10px;
		text-align:center;
	}
</style>
<script>
	function openPopup(url)
	{
		var popUp = window.open(url, popUp, 'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,width=770,height=600');
	}
</script>
<head>
	<title>Welcome to DontStayIn!</title>
</head>
<body>
<center><br />
	<div style="width:500px;">
		<img src="/gfx/welcome-top-2.jpg" class="WelcomeHeaderTop"><div class="WelcomeBox">
			<p style="font-size:12px;">
				Mixmag is the world's biggest dance music and clubbing magazine. To get a FREE copy of Mixmag delivered to your email each month, click below:
			</p>
			<p align="center" style="font-size:12px;">
				<button class="LargeButton" onclick="openPopup('/popup/mixmag?go=1');return false;">I want my Mixmag!</button>
				&nbsp;or&nbsp;&nbsp;
				<button class="LargeButton" runat="server" ID="Button3" causesvalidation="false" onserverclick="Skip">continue to Don't Stay In</button>
			</p>
			<p align="center">
				<img src="http://pix-eu.dontstayin.com/2e26bd8b-b914-4f22-a480-20f88d17e7eb.jpg" width="425" height="338" border="0">
			</p>
		</div>
	</div>
</center>
</body>
</html>
