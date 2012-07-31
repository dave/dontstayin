<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Unsubscribe.ascx.cs" Inherits="Spotted.Blank.Unsubscribe" %>
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
		font-size:12px;
	}
	td, th{
		font-size:10px;
	}
	td.Large{
		font-size:12px;
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
	}
	a:link, 
	a:visited         { color:#000000; }
	a:hover           { color:#FF0000; }

	small a:link, 
	small a:visited   { color:#A58319; }
	small a:hover     { color:#FF0000; }	
	
</style>
<head>
	<title>Welcome to DontStayIn!</title>
</head>
<body>
<center>
	<div style="width:500px;">
		<img src="/gfx/welcome-top-2.jpg" class="WelcomeHeaderTop"><div class="WelcomeBox">
			<p>
				You can stop DontStayIn from sending you any further emails by 
				using this page. You won't be able to use DontStayIn while you're 
				unsubscribed.
			</p>
			<p>
				<%= (CurrentUsr.NickName.Length > 0) ? "You're logged on as <b>" + CurrentUsr.NickName + "</b> - your" : "Your"%>
				email address is <b><%= CurrentUsr.Email %></b>.
			</p>
			<asp:Panel Runat="server" ID="SubscribedPanel">
				<p>
					Your current status is <b>subscribed</b> - DontStayIn can send you 
					emails.
				</p>
			</asp:Panel>
			<asp:Panel Runat="server" ID="UnsubscribedPanel">
				<p>
					Your current status is <b>unsubscribed</b> - DontStayIn will not send 
					you any more emails. You can't use the site when you're logged on in this 
					mode - click the log off button below to use the site.
				</p>
			</asp:Panel>
		</div>
		
		<div class="WelcomeBox" style="border-top:3px solid #000000;padding-top:10px;padding-bottom:10px;" runat="server" id="AddedByUsrDiv">
			<h2>
				Do you want to be unsubscribed?
			</h2>
			<p>
				<asp:Button Runat="server" ID="UnsubscribeButton" Text="Yes, unsubscribe me!" OnClick="UnsubscribeClick"></asp:Button>
			</p>
			<p>
				<asp:Button Runat="server" ID="SubscribeButton" Text="No, please send me emails!" OnClick="SubscribeClick"></asp:Button>
			</p>
		</div>
	
		<div class="WelcomeBox" style="border-top:3px solid #000000;padding-top:10px;padding-bottom:10px;" runat="server" id="Div1">
			<p>
				Other stuff:
			</p>
			<p>
				<asp:Button Runat="server" ID="LogOffButton" Text="Log off - use the site logged off" OnClick="LogOff"></asp:Button>
			</p>
			<asp:Panel Runat="server" ID="CancelPanel">
				<p>
					<asp:Button Runat="server" ID="Button2" Text="Cancel - get me out of here!" OnClick="Cancel"></asp:Button>
				</p>
			</asp:Panel>
		</div>
	</div>
</center>
</body>
</html>
