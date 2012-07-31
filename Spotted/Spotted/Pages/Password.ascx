<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="Password.ascx.cs" Inherits="Spotted.Pages.Password" %>

<asp:Panel Runat="server" ID="PanelPassword">
	<dsi:h1 runat="server" ID="Header99" NAME="H18">Forgotten your password?</dsi:h1>
	<div class="ContentBorder">
		<p>
			If you've forgotten your password, we can send you a link by email that 
			will let you reset your password. Enter your nickname or email address 
			in the box below and click the button:
		</p>
		<h2>1) Enter your email or nickname:</h2>
		<p>
			<asp:TextBox Runat="server" ID="EmailTextBox"/>
		</p>
		<h2>2) Click below to send a reset link by email:</h2>
		<p>
			<asp:Button ID="Button1" runat="server" OnClick="SendReminder" Text="Send password reset email"/>
		</p>
		<h2>
			3) Wait for the email to arrive
		</h2>
		<h2>
			4) Click the link in the email
		</h2>
		<h2>
			5) Enter your new password
		</h2>
		<asp:Panel Runat="server" ID="ErrorPanel" Visible="False">
			<p style="color:ff0000;">
				This email or nickname isn't in our database. Try again.
			</p>
		</asp:Panel>
		<asp:Panel Runat="server" ID="DonePanel" Visible="False">
			<p style="color:0000ff;">
				Done. If this email doesn't arrive 
				shortly, check in your bulk-mail or spam folders. It doesn't arrive 
				after waiting a few hours, maybe your email provider doesn't accept 
				mail from us. If you want to change your email provider, we recommend 
				Google Mail (<a href="http://www.googlemail.com" target="_blank">www.googlemail.com</a>)
			</p>
		</asp:Panel>
	</div>
</asp:Panel>
<asp:Panel runat="server" ID="PanelReset">
	<dsi:h1 runat="server" ID="Header1" NAME="H18">Password reset</dsi:h1>
	<div class="ContentBorder">
		<p>
			You've clicked the link in a password reset email. Please enter your new password below:
		</p>
		<h2>
			Enter your new password here:
		</h2>
		<p>
			<asp:TextBox runat="server" ID="Password1" TextMode="Password" MaxLength="20"></asp:TextBox>
		</p>
		<h2>
			Confirm your new password here:
		</h2>
		<p>
			<asp:TextBox runat="server" ID="Password2" TextMode="Password" MaxLength="20"></asp:TextBox>
		</p>
		<p>
			<asp:Button runat="server" OnClick="PasswordResetChange_Click" Text="Change password" />
		</p>
		<asp:RequiredFieldValidator runat="server" Display="Dynamic" ControlToValidate="Password2" ErrorMessage="<p>Please enter a password</p>"/>
		<asp:RegularExpressionValidator runat="server" Display="dynamic" ControlToValidate="Password2" ValidationExpression="^.{4,20}$" ErrorMessage="<p>Please enter between 4 and 20 characters for your password</p>" />
		<asp:CompareValidator runat="server" Display="dynamic" ControlToCompare="Password1" ControlToValidate="Password2" Type="String" Operator="Equal" ErrorMessage="<p>The passwords you entered don't match. Please try again.</p>" />
	</div>
</asp:Panel>
<asp:Panel runat="server" ID="PanelResetCancelled">
	<dsi:h1 runat="server" ID="Header2" NAME="H18">Password reset cancelled</dsi:h1>
	<div class="ContentBorder">
		<p>
			You've cancelled the password reset request.
		</p>
	</div>
</asp:Panel>
<asp:Panel runat="server" ID="PanelResetDone">
	<dsi:h1 runat="server" ID="Header3" NAME="H18">Password reset done</dsi:h1>
	<div class="ContentBorder">
		<p>
			You've changed your password. <a href="/pages/login" onclick="try { ConnectButtonClick(); } catch(ex) { } return false;">Click here to log in</a>.
		</p>
	</div>
</asp:Panel>
<asp:Panel runat="server" ID="PanelResetError">
	<dsi:h1 runat="server" ID="Header4" NAME="H18">Password reset error</dsi:h1>
	<div class="ContentBorder">
		<p>
			You've clicked a link in a password reset email, but the link doesn't match our
			records. 
			Perhaps you clicked the "reset my password" link several times, or you clicked 
			the "cancel this request" link... 
		</p>
		<p>
			To continue, delete all password reset emails you have in your email inbox and 
			<a href="/pages/password">try again</a>.
		</p>
	</div>
</asp:Panel>
