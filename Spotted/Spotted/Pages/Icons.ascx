<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Icons.ascx.cs" Inherits="Spotted.Pages.Icons" %>
<%@ Register TagPrefix="Controls" TagName="Payment" Src="/Controls/Payment.ascx" %>
<%@ Register TagPrefix="Controls" TagName="DonateText" Src="/Controls/DonateText/DonateTextControl.ascx" %>

<asp:Panel Runat="server" ID="DonateLoggedOut">
	<Controls:DonateText runat="server" id="uiDonateText"></Controls:DonateText>
		<p>
			<b>
				You'll have to log in first:
			</b>
		</p>
		<TABLE cellSpacing="15" width="100%">
			<TR>
				<TD vAlign="top" align="center" width="50%">
					<P style="FONT-WEIGHT: bold; FONT-SIZE: 18px">
						<A onclick="DsiPageShowLoginNew();return false;" href="">Log in</A>
					</P>
					<P>If you've already signed-up</P>
				</TD>
				<TD vAlign="top" align="center" width="50%">
					<P style="FONT-WEIGHT: bold; FONT-SIZE: 18px">
						<A onclick="DsiPageShowLoginNew();return false;" href="">Sign up FREE!</A>
					</P>
					<P>If you've not used the site before</P>
				</TD>
			</TR>
		</TABLE>
	</div>
</asp:Panel>
<asp:Panel Runat="server" ID="DonateLoggedIn">
	<Controls:DonateText runat="server"></Controls:DonateText>
		<p>
			<center>
				<div style="text-align:left;width:285px;">
					<Controls:Payment id="Payment" Runat="server" OnPaymentDone="PaymentReceived"/>
				</div>
			</center>
		</p>
	</div>
</asp:Panel>
<asp:Panel Runat="server" ID="DonatedMessagePanel" Visible="False">
	<dsi:h1 runat="server">Thanks!</dsi:h1>
	<div class="ContentBorder">
		<p align="center">
			<b>Thanks for your purchase. You now have a <%= DonationIcon.IconName%>:</b>
		</p>
		<p align="center">
			<b><img src="<%= DonationIcon.IconPath %>" border="0" align="absMiddle" style="MARGIN-RIGHT:3px"><%= DonationIcon.IconText %></b>
		</p>
		<p class="BigCenter">
			<a href="/chat/k-<%= DonationIcon.ThreadK %>">Tell us how much you love your <%= DonationIcon.IconName%></a>
		</p>
	</div>
</asp:Panel>
<asp:Panel Runat="server" ID="DonateRemainPanel">
	<dsi:h1 ID="H1" runat="server">Icons I still need!</dsi:h1>
	<div class="ContentBorder">
		<p>	
			Icons I need to complete the set:
		</p>
		<div>
			<table cellpadding="0" cellspacing="0">
				<tr>
					<asp:Literal runat="server" ID="DonationIconsHtml"></asp:Literal>
				</tr>
			</table>
		</div>
		<p>
			If you collect all the icons, you'll get the special LEGEND icon:
		</p>
		<p>
			<center>
				<b>
					<img src="/gfx/don/legend.png" width="26" height="21" border="0" align="absMiddle" style="margin-right:3px;" />LEGEND!
				</b>
			</center>
		</p>
	</div>
</asp:Panel>
