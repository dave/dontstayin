<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Unconfirmed.ascx.cs" Inherits="Spotted.Pages.Promoters.Unconfirmed" %>

<asp:Panel Runat="server" ID="PanelUnconfirmed">
	<dsi:PromoterIntro runat="server" ID="PromoterIntro" Header="Unconfirmed brands">
		<p>
			We haven't yet been able to confirm that you're the owner of this 
			brand / venue. You won't be able to use this brand / venue until you 
			have confirmed ownership of it.
		</p>
		<p>
			You can help us speed up the process by sending a fax on company headed paper 
			to +44 (0) 870 068 8822, or an email to 
			<img src="/gfx/john-email.gif" align="absbottom">. Please state clearly 
			all the relevant details.
		</p>
	</dsi:PromoterIntro>
</asp:Panel>
