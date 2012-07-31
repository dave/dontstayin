<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventHighlight.ascx.cs" Inherits="Spotted.Pages.Promoters.EventHighlight" %>
<%@ Register TagPrefix="Controls" TagName="Payment" Src="/Controls/Payment.ascx" %>

<dsi:PromoterIntro runat="server" ID="PromoterIntro" Header="Event hilight">
	<p style="font-weight:bold;font-size:14px;">
		<a href="" runat="server" id="IntroBannerListLink">Event options page</a>
	</p>
</dsi:PromoterIntro>

<asp:Panel Runat="server" ID="ChoicePanel">
	
	<dsi:h1 runat="server" ID="H13" NAME="H11">Highlight your event with a donation to DontStayIn</dsi:h1>
	<div class="ContentBorder">
		<p>
			Highlighted events are <b>enclosed in a highlight box</b>, and at the <b>top of event lists</b>. Check out below for some examples:
		</p>
		<p>
			<center><img src="/gfx/event-hilight.gif" width="578" height="578"></center>
		</p>
		<p>
			It's very simple to gen an event highlight - just click the <i>Buy Now</i> button below.
		</p>
		<p>
			<table cellpadding="3" cellspacing="0" class="DataGrid">
				<tr class="DataGridHeader">
					<td>Item</td><td>Price</td><td>Click here to order</td>
				</tr>
				<tr>
					<td runat="server" id="RecommendedCell">Evnt highlight for ? capacity venue</td>
					<td runat="server" id="RecommendedCellPrice" style="font-weight:bold; text-align:right;"></td>
					<td align="center">
						<asp:Button ID="Button1" Runat="server" OnClick="Donation_Click" Text="Buy now"/>
					</td>
				</tr>
			</table>
		</p>
	</div>


</asp:Panel>

<asp:Panel Runat="server" ID="PayPanel" Visible="False">
	<dsi:h1 runat="server" ID="H15" NAME="H11">Pay here</dsi:h1>
	<div class="ContentBorder">
		<p>
			<Controls:Payment id="Payment" Runat="server" OnPaymentDone="PaymentReceived"/>
		</p>
		<p>
			<button Runat="server" onserverclick="Pay_Cancel" causesvalidation="false" ID="Button2">&lt;- Cancel</button>
		</p>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="PayDonePanel" Visible="False">
	<dsi:h1 runat="server" ID="H16" NAME="H11">Payment successful</dsi:h1>
	<div class="ContentBorder">
		<p>
			Your payment has been successful. Your event is now highlighted. <a href="<%=CurrentPromoter.UrlEventOptions(CurrentEvent) %>">Click here to return to the event options page</a>
		</p>
	</div>
</asp:Panel>
