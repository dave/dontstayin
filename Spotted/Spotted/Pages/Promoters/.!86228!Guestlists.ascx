<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Guestlists.ascx.cs" Inherits="Spotted.Pages.Promoters.Guestlists" %>

<%@ Register TagPrefix="Controls" TagName="Payment" Src="/Controls/Payment.ascx" %>



<asp:Panel Runat="server" ID="PanelBuy" Visible="false">
	<dsi:h1 runat="server" ID="H17">Top-up your guestlist account with credits</dsi:h1>
	<div class="ContentBorder">
		<p>
			You can buy extra guestlist credits here. Guestlist credits cost 
			<%= CurrentPromoter.GuestlistCharge.ToString("c") %> each.
		</p>
		<p>
			Enter a number below and click purchase. Your account will be 
			credited when we receive confirmation of your transaction.
		</p>
		<p>
			Buy <asp:TextBox Runat="server" ID="BuyCredits" Columns="5">100</asp:TextBox> credits <small>(minimum 100 credits)</small>
		</p>
		<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Runat="server" Display="Dynamic" ControlToValidate="BuyCredits" 
			ErrorMessage="<p>Please enter a number above</p>"/>
		<asp:CompareValidator ID="CompareValidator1" Runat="server" Display="Dynamic" ControlToValidate="BuyCredits" Type="Integer" Operator="GreaterThanEqual" ValueToCompare="100"
			ErrorMessage="<p>The minimum number of credits you can buy is 100.</p>"/>
		<p>
			<button Runat="server" onserverclick="Buy_Cancel" causesvalidation="false" ID="Button1">&lt;- Cancel</button>
			<asp:Button ID="Button2" Runat="server" Text="Buy now -&gt;" OnClick="Buy_Click"></asp:Button>
		</p>
	</div>
</asp:Panel>
<asp:Panel Runat="server" ID="PanelPay"  Visible="false">
	<dsi:h1 runat="server" ID="H18">Top-up your guestlist account with credits</dsi:h1>
	<div class="ContentBorder">
		<p>
			<Controls:Payment id="Payment" Runat="server" 
				OnPaymentDone="PaymentReceived" />
		</p>
		<p>
			<button Runat="server" onserverclick="Pay_Cancel" causesvalidation="false" ID="Button3">&lt;- Cancel</button>
		</p>
	</div>
</asp:Panel>
<asp:Panel Runat="server" ID="PanelPayDone" Visible="false">
	<dsi:h1 runat="server" ID="H19">Top-up complete</dsi:h1>
	<div class="ContentBorder">
		<p>
			Thanks for topping up your account. <a href="<%= CurrentPromoter.UrlApp("guestlists") %>">Back to guestlist options</a>
		</p>
	</div>
</asp:Panel>
<asp:Panel Runat="server" ID="PanelEdit" Visible="false">
	<dsi:h1 runat="server" ID="H14">Add a guestlist</dsi:h1>
	<div class="ContentBorder">
		<table cellpadding="5" cellspacing="2">
			<tr runat="server" id="EditEventTr">
				<td colspan="3">
					Select the event:
				</td>
			</tr>
			<tr runat="server" id="EditEventTr1">
				<td colspan="3">
					<asp:DropDownList Runat="server" ID="EditEventDropDown"/>
					<asp:CustomValidator ID="CustomValidator1" Runat="server" Display="Dynamic" EnableClientScript="False" OnServerValidate="EditEvent_Val" ErrorMessage="<br>This event already has a guestlist. Please choose another."/>
				</td>
			</tr>
			<tr>
