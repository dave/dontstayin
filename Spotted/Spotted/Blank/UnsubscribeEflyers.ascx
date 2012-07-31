<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UnsubscribeEflyers.ascx.cs" Inherits="Spotted.Blank.UnsubscribeEflyers" %>
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
			<asp:Panel runat="server" ID="uiOptionsPanel">
				<p>
					You can control whether you receive DontStayIn e-flyers by choosing from the options below.
				</p>
				<div style="margin:10px;padding:10px;border:1px solid #ff0000;">
					<p><b>DontStayIn is kept online by advertising. Please help support us by opting-in to our e-flyer service.</b></p>
				</div>
				<p>
					You're logged on as <b><%= CurrentUsr.NickName %></b> - your 
					email address is <b><%= CurrentUsr.Email %></b>.
				</p>
				<p>
					<asp:RadioButtonList ID="uiSendEflyersOptions" runat="server">
						<asp:ListItem Value="True" Text="Please send me e-flyers about amazing events."></asp:ListItem>
						<asp:ListItem Value="False" Text="Don't send me any more e-flyers."></asp:ListItem>
					</asp:RadioButtonList>
				</p>
				<p>
					<button runat="server" onserverclick="Update">Save</button>
				</p>
			</asp:Panel>
			<asp:Panel runat="server" ID="uiSavedPanel">
				<p>Your settings have been saved as <asp:Label runat="server" ID="uiSavedSettingLabel" Font-Bold="true"></asp:Label></p>
				<p><b><a href="/">Back to DontStayIn</a></b></p>
			</asp:Panel>
		</div>
	</div>
</center>
</body>
</html>
