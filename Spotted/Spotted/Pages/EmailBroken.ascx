<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EmailBroken.ascx.cs" Inherits="Spotted.Pages.EmailBroken" %>


<asp:Panel Runat=server ID=EmailBrokenPanel>
	<dsi:h1 runat="Server">Your email is bouncing</dsi:h1>
	<div class="ContentBorder">
		<p>
			Your email address (<% = Usr.Current.Email %>) is not getting through. We've received a bounce message, so we've stopped sending email to this address.
		</p>
		<p>
			Maybe you've fixed the problem? We can continue sending email to this address - just <asp:LinkButton ID="LinkButton1" Runat="server" OnClick="DisableBrokenFlag">click here</asp:LinkButton>. 
		</p>
		<p>
			Maybe you've changed your email address? You can update it on the <a href="/pages/mydetails">My details</a> page.
		</p>

		<p runat=server id=DoneP style="color:blue;" visible="false">
			Done - we'll continue to send emails to <% = Usr.Current.Email %>. If we receive more bounce messages we'll stop again.
		</p>
	</div>
</asp:Panel>

<asp:Panel Runat=server ID=EmailNotBrokenPanel>
	<dsi:h1 ID="H1" runat="Server">Your email is OK</dsi:h1>
	<div class="ContentBorder">
		<p>
			Everything is OK. Don't panic.
		</p>
	</div>
</asp:Panel>
