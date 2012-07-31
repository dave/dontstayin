<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmailVerify.ascx.cs" Inherits="Spotted.Pages.EmailVerify" %>


<asp:Panel Runat=server ID=enableCommsPanel>
	<dsi:h1 runat="Server" ID="enableCommsH1">Email verification</dsi:h1>
	<div class="ContentBorder">
		<p>
			Your email address must be verified for us to send you emails.
		</p>
		<p>
			When you first logged in, we sent you an email containing a link that will verify your email address. If you can't find it - try looking in your 'spam' folder.
		</p>
		<p>
			We can <asp:LinkButton ID="LinkButton1" Runat="server" OnClick="EnableCommsClick">send another copy</asp:LinkButton> of this email if you don't have it.
		</p>
		<p>
			You can change your email address on the <a href="/pages/mydetails">My details</a> page.
		</p>
		<p runat=server id=emailSentP style="color:blue;" visible="false">
			We have sent you an email.
		</p>
	</div>
</asp:Panel>
<asp:Panel Runat=server ID=disableCommsPanel>
	<dsi:h1 runat="Server" ID="H11">Email verification</dsi:h1>
	<div class="ContentBorder">
		<h2>Your email address has been verified</h2>
		<p>
			We can now send you emails.
		</p>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="PanelError">
	<dsi:h1 runat="Server" ID="H12">Email verification</dsi:h1>
	<div class="ContentBorder">
		<h2>Error</h2>
		<p>
			It looks like you've clicked a link in one of our email verification emails, but we can't verify you. This is probably because you clicked the link in an old email. You must find the latest email that we've sent you.
		</p>
		<p>
			We can send you another copy of the email, but you'll have to log in first (use the 'connect' button above).
		</p>
	</div>
</asp:Panel>
