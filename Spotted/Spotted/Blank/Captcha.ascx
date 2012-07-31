<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Captcha.ascx.cs" Inherits="Spotted.Blank.Captcha" %>
<link rel="stylesheet" type="text/css" href="/support/style.css?a=4"/>
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

<table width=400 cellpadding="0" cellspacing="0" border="0">
			<tr>
				<td valign=bottom align=left width="400" rowspan="2">
				
				
<center>
<div style="background-color:#ffffff; padding:10px; margin:10px;">
<img src="/gfx/logo-200-90.jpg" border="0" />
</div>
</center>

<div style="padding:10px;">
<h1>
	<span class="Inner">
		Hi!
	</span>
</h1>
<div class="ContentBorder">
	<p>
		You've been quite busy today... To help fight the evil spammers, we've got a little test for you.
	</p>
	<p>
		A spam-bot won't be able to read the letters below, but you probably can... There should be five uppercase letters - just enter them below.
	</p>
	<table>
		<tr>
			<td>
				<p>
					<img src="/support/hipimage.aspx" runat="server" id="HipImage" style="border:1px solid #000000;" width="150" height="50" />
				</p>
			</td>
			<td style="padding-left:8px;">
				<p>
					Enter the letters here: <asp:TextBox runat="server" ID="HipChallengeTextBox" MaxLength="5"></asp:TextBox>
				</p>
				<p>
					<asp:Button runat="server" OnClick="Done_Click" Text="Done!" ID="DoneButton" />
				</p>
			</td>
		</tr>
	</table>
	<asp:CustomValidator Runat="server" Display="Dynamic" EnableClientScript="False" 
		OnServerValidate="HipVal" 
		ErrorMessage="<p>You got the verification letters wrong... You should see 5 upper-case letters. Try half-closing your eyes...</p>" 
		ID="Customvalidator10"/>
	
</div>


</td></tr></table>
</center>
