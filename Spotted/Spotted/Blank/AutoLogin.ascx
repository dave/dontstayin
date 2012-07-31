<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AutoLogin.ascx.cs" Inherits="Spotted.Blank.AutoLogin" %>
<style>
		
		.{
			font-family: Verdana, sans-serif;
			font-size:10px;
		}
		p
		{
			margin-bottom:3px;
			margin-top:3px;
			line-height:130%;
		}
		
 a:link, 
 a:visited         { color:#000000; }
 a:hover           { color:#FF0000; }
	</style>
&nbsp;<br>&nbsp;
<center>
<table width=295 cellpadding="0" cellspacing="0" border="0">
			<tr>
				<td valign=bottom align=left width="295" rowspan="2">
				
				
<center>
<a href="http://www.dontstayin.com/"><img src="http://www.dontstayin.com/gfx/dsi-sign-100.png" border=0 style="border:1px solid #000000;"></a>
</center>

<div style="padding:10px;">
<div style="background-color:333333;color:#ffffff;font-weight:bold;padding:2px 4px 2px 4px;font-size:12px;">
	Hi <%= AttemptUsr.NickName %>,
</div>
<div style="width:100%;border:solid 1px #000000;padding:2px 4px 2px 4px; margin:0px 0px 13px 0px;">
	<p>
		You've tried to log in as <%= AttemptUsr.NickName %>, but you have enhanced security 
		enabled. You must enter your password to log on.
	</p>
	<p>
		<b>Password:</b>
	</p>
	<p>
		<asp:TextBox Runat="server" ID="PasswordTextBox" TextMode="Password" Columns="30"></asp:TextBox>
	</p>
	<p>
		<asp:Button ID="Button1" Runat="server" OnClick="LogOn_Click" Text="Log on"></asp:Button>
		<asp:Button ID="Button2" Runat="server" OnClick="Cancel_Click" Text="Cancel"></asp:Button>
	</p>
	<p runat="server" id="ErrorP" visible="false" style="color:red;font-weight:bold;">
		Sorry, this password is not correct. Please try again.
	</p>
</div>


</td></tr></table>
</center>
