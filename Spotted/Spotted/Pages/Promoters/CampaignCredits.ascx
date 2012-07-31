<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CampaignCredits.ascx.cs" Inherits="Spotted.Pages.Promoters.CampaignCredits" %>
<%@ Register TagPrefix="Controls" TagName="Payment" Src="/Controls/Payment.ascx" %>

<script>
	var SingleCreditPrice = <%= CurrentPromoter.CostPerCampaignCredit.ToString("0.00") %>;
	var DiscountCredits = new Array(<%= DiscountCreditsString %>);
	var DiscountLevels = new Array(<%= DiscountLevelsString %>);
	function DiscountMoneys(i) { return DiscountCredits[i] * (1.0 - DiscountLevels[i]); }
	var PromoterDiscount = <%= CurrentPromoter.Discount.ToString() %>;
	var maxCreditTotal = 50000;
	var maxCreditTotalString = "£" + tsep((maxCreditTotal).toFixed(2))
	var exceedingMaxCreditTotalMsg = "For amounts greater than " + maxCreditTotalString + ", please contact one of our sales staff"

	function CustomCampaignCreditsIsValid(sender, args)
	{
		if(<%= Convert.ToInt32(CustomRadioButton.ClientID != null) %>)
		{
			args.IsValid = !document.getElementById('<%= CustomRadioButton.ClientID %>').checked || !isNaN(parseInt(document.getElementById('<%= CustomCreditsTextBox.ClientID %>').value));
		}
		else
			args.IsValid = true;	
	}
	
	function EnsureCampaignCreditsSelected(sender, args)
	{
		if(<%= Convert.ToInt32(SelectedCredits.ClientID != null) %>)
		{
			args.IsValid = !(document.getElementById('<%= SelectedCredits.ClientID %>').value == '');
		}
		else
			args.IsValid = true;	
	
	}
		
	function GetCashPrice(credits)
	{
		return credits * SingleCreditPrice * (1 - (GetDiscount(credits) / 100.0));
	}
	
	function GetDiscount(credits)
	{
		var i = 0;
		var discountLevel = 0;
		while(credits>=DiscountCredits[i] && i<DiscountCredits.length)
		{
			discountLevel = DiscountLevels[i];
			i++;
		}
		if (PromoterDiscount > discountLevel)
			return PromoterDiscount;
		else
			return discountLevel;
	}
	
	function GetDiscountForMoney(money)
	{
		var i = 0;
		var discountLevel = 0;
		while(money>=DiscountMoneys(i) && i<DiscountCredits.length)
		{
			discountLevel = DiscountLevels[i];
			i++;
		}
		if (PromoterDiscount > discountLevel)
			return PromoterDiscount;
		else
			return discountLevel;
	}
	function tsep(n)
	{ 
		var ts=",", ds=".";
		var ns = String(n),ps=ns,ss="";
		var i = ns.indexOf("."); 
		if (i != -1)
		{
			ps = ns.substring(0,i); 
			ss = ds+ns.substring(i+1); 
		} 
		return ps.replace(/(\d)(?=(\d{3})+([.]|$))/g,"$1"+ts)+ss; 
	} 
	
	function CustomCampaignCreditsEnter()
	{
		if(<%= Convert.ToInt32(CustomRadioButton.ClientID != null) %>)
		{
			document.getElementById('<%= CustomRadioButton.ClientID %>').checked = true;
			CalculateCustomCampaignCreditsAndTotal();
		}
	}
	
	function CustomCampaignCreditsMoneyEnter()
	{
		if(<%= Convert.ToInt32(CustomRadioButton.ClientID != null) %>)
		{
			document.getElementById('<%= CustomRadioButton.ClientID %>').checked = true;
			CalculateCustomCampaignCreditsFromCustomMoney();
		}
	}
	
	function CalculateCustomCampaignCredits()
	{
		if(<%= Convert.ToInt32(CustomCreditsTextBox.ClientID != null) %>)
		{
			var credits = parseInt(document.getElementById('<%= CustomCreditsTextBox.ClientID %>').value.replace(/,/g,''));
			document.getElementById('<%= SelectedCredits.ClientID %>').value = isNaN(credits) ? '' : credits;
			
			var price = credits * SingleCreditPrice;
			document.getElementById('<%= CustomPriceLabel.ClientID %>').innerHTML = "<small><strike>" + (isNaN(price) ? "£0.00" : "£" + tsep((price).toFixed(2))) + "</strike></small> ";
			var discount = GetDiscount(credits);
			document.getElementById('<%= CustomDiscountLabel.ClientID %>').innerHTML = isNaN(discount) ? "0" : discount + "% off";
			//var saving = credits * SingleCreditPrice * (GetDiscount(credits)/100);
			//document.getElementById('<!--%= CustomSavingLabel.ClientID %-->').innerHTML = "<small>" + (isNaN(saving) ? "n/a" : "£" + tsep((saving).toFixed(2))) + "</small>";
		}
	}
			
	function CalculateCustomCampaignCreditsAndTotal()
	{
		if(<%= Convert.ToInt32(CustomCreditsTextBox.ClientID != null) %>)
		{
			CalculateCustomCampaignCredits();
			var credits = parseInt(document.getElementById('<%= CustomCreditsTextBox.ClientID %>').value.replace(/,/g,''));
			var total = GetCashPrice(credits);
			if(total > maxCreditTotal)
			{
				total = maxCreditTotal;
				
				document.getElementById('<%= CustomTotalTextBox.ClientID %>').value = maxCreditTotalString;
				alert(exceedingMaxCreditTotalMsg);
				CalculateCustomCampaignCreditsFromCustomMoney();
			}
			else
			{
				document.getElementById('<%= CustomTotalTextBox.ClientID %>').value = isNaN(total) ? "" : "£" + tsep((total).toFixed(2));
			}
		}
	}
	
	function CalculateCustomCampaignCreditsFromCustomMoney()
	{
		if(<%= Convert.ToInt32(CustomTotalTextBox.ClientID != null) %>)
		{
			var total = parseFloat(document.getElementById('<%= CustomTotalTextBox.ClientID %>').value.replace(/£/g, '').replace(/,/g,''));
			if(isNaN(total))
				total = 0;
			if(total > maxCreditTotal)
			{
				total = maxCreditTotal;
				document.getElementById('<%= CustomTotalTextBox.ClientID %>').value = maxCreditTotalString;
				alert(exceedingMaxCreditTotalMsg);			
			}
			
			var costPerCredit = SingleCreditPrice * (1 - (GetDiscountForMoney(total) / 100.0));
			var credits = 0;
			if(!isNaN(costPerCredit) && costPerCredit != 0)
				credits = parseInt(total / costPerCredit);
			
			document.getElementById('<%= CustomCreditsTextBox.ClientID %>').value = credits;
			CalculateCustomCampaignCredits();
		}
	}
</script>

<dsi:PromoterIntro runat="server" ID="PromoterIntro" Header="Buy Campaign Credits">
	<p>
		Campaign credits are used for buying stuff on the site, like advertising.
	</p>
	<h2>
		Current balance: <%= CurrentPromoter.CampaignCredits.ToString() %></h2>
	<p>
		<a href="<%= CurrentPromoter.UrlApp("campaigncredithistory") %>" >
		<img src="/Gfx/icon-view.png" border="0" align="absmiddle" style="margin-right:3px"  width="26" height="21"/>view campaign credit history</a>
	</p>
</dsi:PromoterIntro>

<dsi:h1 runat="server" ID="H1Title">Campaign Credits</dsi:h1>
<div class="ContentBorder">
	<asp:Panel runat="server" ID="CreditsPanel">
		<p>
			Choose from one of the following campaign credit packages:
		</p>
		<asp:HiddenField ID="SelectedCredits" runat="server"/>
		<asp:Table runat="server" ID="CreditsTable" EnableViewState="true"/>

		<p>
			<button onclick="window.location='<%= CurrentPromoter.Url() %>';">Back</button>
			<asp:Button runat="server" ID="BuyButton" Text="Next" OnClick="BuyButton_Click" CausesValidation="true"/>
		</p>
		
		<asp:CustomValidator ID="EnsureCampaignCreditsSelectedValidator" runat="server" Display="Dynamic" ClientValidationFunction="EnsureCampaignCreditsSelected" 
			OnServerValidate="EnsureCampaignCreditsSelected" ErrorMessage="<p>Please select a credit option!</p>" ></asp:CustomValidator>
		<asp:CustomValidator ID="CustomCampaignCreditsCustomValidator" runat="server" Display="Dynamic"	ClientValidationFunction="CustomCampaignCreditsIsValid" 
			OnServerValidate="CustomCampaignCreditsIsValid" ErrorMessage="<p>Amount of credits entered is invalid!</p>" ></asp:CustomValidator>

		<h2>Example campaign budgets</h2>
		<p>
			Once you've bought one of the packages above, you can spend the credits on any of our campaigns:
		</p>
		<p>
			Light banner campaign: <b>100 credits per week</b>
		</p>
		<p>
			Medium banner campaign: <b>200 credits per week</b>
		</p>
		<p>
			Heavy banner campaign: <b>350 credits per week</b>
		</p>
	</asp:Panel>
	
	<asp:Panel ID="PaymentPanel" runat="server">
		<Controls:Payment Runat="server" id="Payment" OnPaymentDone="PaymentReceived"/>
		<p id="AdminPriceEditP" style="margin-left:0px;" runat="server" visible="false">
			<table cellpadding="0" cellspacing="0" border="0" style="border:0px;">
				<tr>
					<td style="border:0px" valign="top"><asp:TextBox ID="FixPriceTextBox" runat="server" onmouseover="stt('Enter fixed price (£) or fixed discount (%).<br>Leave blank and click to undo price fix.');" onmouseout="htm();" Width="58"></asp:TextBox></td>
					<td style="border:0px">
						<div>
							<button id="FixPriceExVatButton" runat="server" style="width:120px" onserverclick="FixPriceExVatButton_Click" onmouseover="htm();" ValidationGroup="FixPriceValidationGroup"><nobr>Fix price exVAT (£)</nobr></button>&nbsp;
							<button id="FixPriceIncVatButton" runat="server" style="width:120px" onserverclick="FixPriceIncVatButton_Click" onmouseover="htm();" ValidationGroup="FixPriceValidationGroup"><nobr>Fix price incVAT (£)</nobr></button>
						</div>
						<div style="margin-top:2px">
							<button id="FixPriceDiscountButton" runat="server" style="width:120px" onserverclick="FixPriceDiscountButton_Click" onmouseover="htm();" ValidationGroup="FixPriceValidationGroup"><nobr>Fix discount (%)</nobr></button>&nbsp;
							<button id="ClearFixDiscountButton" runat="server" style="width:120px" onserverclick="ClearFixDiscountButton_Click" onmouseover="htm();" ValidationGroup="FixPriceValidationGroup"><nobr>Clear fixed discount</nobr></button>
						</div>
					</td>
				</tr>
			</table>
		</p>
		<p><asp:Button runat="server" ID="BackToCreditOptionsButton" Text="Back" OnClick="BackToCreditOptionsButton_Click"/></p>
	</asp:Panel>
	
	<asp:Panel ID="SuccessPanel" runat="server">
		<p><h2>Thank you! Your payment has been successful.</h2></p>
		<p><h2><% = SelectedCredits.Value %> campaign credits have been added onto your account.</h2></p>
		<p><a href="<%= CurrentPromoter.Url() %>">Return to promoter home page.</a></p>
	</asp:Panel>
</div>
<script language="JavaScript">
	// run this script after the page has loaded
	CalculateCustomCampaignCreditsAndTotal();
</script>
