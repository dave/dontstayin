<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Payment.ascx.cs" Inherits="Spotted.Controls.Payment" %>
<script language="javascript">
function PaymentTogglePayOptions()
{
	<%= PaymentTogglePayOptionsScript %>
}
function PaymentToggleSavedCardOptions()
{
	document.getElementById("<%=  NewCardPanel.ClientID  %>").style.display = document.getElementById("<%=  SavedCardOptionsSavedCard.ClientID  %>").checked?'none':'';
	document.getElementById("<%=  SavedCardInnerPanel.ClientID  %>").style.display = document.getElementById("<%=  SavedCardOptionsSavedCard.ClientID  %>").checked?'':'none';
}

</script>
<div class="PaymentDiv" onload="PaymentTogglePayOptions();">
	<a name="<%= this.ClientID %>_PaymentAnchor"></a>
	<table cellpadding="0" cellspacing="0" Width="100%" class="PaymentTable">
		<tr>
			<td style="background-color:transparent;" width="100%" colspan="2">
				<small>This page is 100% secure - <a href="https://www.dontstayin.com/popup/secure" onclick="openPopup('https://www.dontstayin.com/popup/secure');return false;">click here for info</a></small>
			</td>
		</tr>
	</table>
	<asp:Panel ID="UserItemListPanel" runat="server">
	<table cellpadding="0" cellspacing="0" Width="100%" class="PaymentTable">
		<tr>
			<td style="background-color:transparent;padding-bottom:1px;" width="100%">
				Items
			</td>
		</tr>
		<tbody runat="server" id="ItemsBody"/>
	</table>
	</asp:Panel>
	<asp:Panel ID="BuyerItemListPanel" runat="server">
		<table cellpadding="0" cellspacing="0" Width="100%" class="PaymentTable">
			<tr>
				<td align="left" style="background-color:transparent;"><asp:Label ID="BuyerItemListColumn1HeaderLabel" runat="server" Text="Invoice"></asp:Label></td>
				<td align="right" style="background-color:transparent;"><asp:Label ID="BuyerItemListColumn2HeaderLabel" runat="server" Text="Total"></asp:Label></td>
				<td align="right" style="background-color:transparent;"><asp:Label ID="BuyerItemListColumn3HeaderLabel" runat="server" Text="Due"></asp:Label></td>
			</tr>
			<tbody runat="server" id="InvoicesBody"/>
			<tr runat="server" id="FromCampaignCreditsRow" visible="false">
				<td style="background-color:transparent;" colspan="2" align="right" valign="top">Campaign credit balance</td>
				<td style="background-color:transparent;" align="right" valign="top"><asp:Label ID="FromCampaignCreditBalanceLabel" runat="server" Text=""></asp:Label></td>
			</tr>
			<tr runat="server" id="TotalCreditsRow" visible="false">
				<td style="background-color:transparent;" colspan="2" align="right" valign="top"><b>Total credits</b></td>
				<td style="background-color:transparent;" align="right" valign="top"><asp:Label ID="InvoiceTotalAsCampaignCreditsLabel" Font-Bold="true" runat="server" Text=""></asp:Label></td>
			</tr>
			<tr runat="server" id="TotalCreditsAsMoneyRow" visible="false">
				<td style="background-color:transparent;" colspan="2" align="right" valign="top"><b><asp:Label ID="Discount" runat="server"></asp:Label></b></td>
				<td style="background-color:transparent;" align="right" valign="top"><asp:Label ID="CreditsToMoneyTotalLabel" runat="server" Font-Bold="true" Text=""></asp:Label></td>
			</tr>
			<tr runat="server" id="VatRow" visible="false">
				<td style="background-color:transparent;" colspan="2" align="right" valign="top">VAT</td>
				<td style="background-color:transparent;" align="right" valign="top"><asp:Label ID="VatLabel" runat="server" Text=""></asp:Label></td>
			</tr>
			<tr runat="server" id="InvoiceTotalRow" visible="false">
				<td style="background-color:transparent;" colspan="2" align="right" valign="top"><b>Total</b></td>
				<td style="background-color:transparent;" align="right" valign="top"><asp:Label ID="InvoiceTotalLabel" runat="server" Font-Bold="true" Text=""></asp:Label></td>
			</tr>
			<tr runat="server" id="FromBalanceRow" visible="false">
				<td style="background-color:transparent;" colspan="2" align="right" valign="top">From balance</td>
				<td style="background-color:transparent;" align="right" valign="top"><asp:Label ID="FromBalanceAmountLabel" runat="server" Text=""></asp:Label></td>
			</tr>
			<tr runat="server" id="FromCreditRow" visible="false">
				<td style="background-color:transparent;" colspan="2" align="right" valign="top">From credit</td>
				<td style="background-color:transparent;" align="right" valign="top"><asp:Label ID="FromCreditAmountLabel" runat="server" Text=""></asp:Label></td>
			</tr>
			<tr runat="server" id="InvoiceTotalAfterBalanceAndCreditRow" visible="false">
				<td style="background-color:transparent;" colspan="2" align="right" valign="top"><b>Total</b></td>
				<td style="background-color:transparent;" align="right" valign="top"><asp:Label ID="InvoiceTotalAfterBalanceAndCreditLabel" Font-Bold="true" runat="server" Text=""></asp:Label></td>
			</tr>
        </table>
	</asp:Panel>
	
	<asp:Panel runat="server" ID="uiTicketsEntryCheckboxPanel">
		<table cellpadding="0" cellspacing="0" width="100%" class="PaymentTable">
			<tr>
				<td style="background-color:transparent;border-top:dotted 1px <%= BorderColour %>;" width="100%">
					Ticket confirmation
				</td>
			</tr>
		</table>
		<table width="100%" border="0" cellspacing="0" cellpadding="0" class="PaymentTable">
			<tr>
				<td style="background-color:transparent;" valign="top">
					<asp:CheckBox runat="server" Text="" ID="TicketsEntryCheckbox" />
				</td>
				<td style="background-color:transparent;" valign="top">
					<label runat="server" id="uiTicketConfirmationMessage" style="font-size:12px; font-weight:bold; line-height:150%;" for="<%= TicketsEntryCheckbox.ClientID %>"></label>
				</td>
			</tr>
		</table>
		<asp:CustomValidator ID="TicketsEntryCheckboxValidator2" runat="server"  Display="None" EnableClientScript="False" ErrorMessage="You must read, understand and tick the box in the ticket confirmation section!" OnServerValidate="TicketsEntryCheckboxVal" ValidationGroup="PaymentValidation"/>
	</asp:Panel>
	

	<asp:Panel ID="PayOptionsPanel" runat="server">
		<table cellpadding="0" cellspacing="0" width="100%" class="PaymentTable">
			<tr>
				<td style="background-color:transparent;border-top:dotted 1px <%= BorderColour %>;" width="100%">
					Payment options
				</td>
			</tr>
		</table>
		<table width="100%" border="0" cellspacing="0" cellpadding="0" class="PaymentTable">
			<tr runat="server" id="PayOptionRadioButtonPayNowHolder">
				<td style="background-color:transparent;" valign="top">
					<asp:RadioButton runat="server" ID="PayOptionRadioButtonPayNow" GroupName="PayOptionRadioButton" onselect="PaymentTogglePayOptions();" onclick="PaymentTogglePayOptions();" Text="Pay now <small>(by card)</small>" />
				</td>
			</tr>
			<tr runat="server" id="PayOptionRadioButtonPayLaterHolder">
				<td style="background-color:transparent;" valign="top">
					<asp:RadioButton runat="server" ID="PayOptionRadioButtonPayLater" GroupName="PayOptionRadioButton" onselect="PaymentTogglePayOptions();" onclick="PaymentTogglePayOptions();" Text="Pay later <small>(create an invoice)</small>" />
				</td>
			</tr>
			<tr runat="server" id="PayOptionRadioButtonPayWithBalanceHolder">
				<td style="background-color:transparent;" valign="top">
					<asp:RadioButton runat="server" ID="PayOptionRadioButtonPayWithBalance" GroupName="PayOptionRadioButton" onselect="PaymentTogglePayOptions();" onclick="PaymentTogglePayOptions();" Text="Pay with balance" />
				</td>
			</tr>
			<tr runat="server" id="PayOptionRadioButtonPayWithCampaignCreditHolder">
				<td style="background-color:transparent;" valign="top">
					<asp:RadioButton runat="server" Enabled="false" ID="PayOptionRadioButtonPayWithCampaignCredit" GroupName="PayOptionRadioButton" onselect="PaymentTogglePayOptions();" onclick="PaymentTogglePayOptions();" Text="Pay using campaign credits" />
				</td>
			</tr>
        </table>
	</asp:Panel>

	<asp:Panel ID="PayNowPanel" runat="server">
		<asp:Panel ID="SavedCardPanel" runat="server">
			<table cellpadding="0" cellspacing="0" width="100%" class="PaymentTable">
				<tr>
					<td style="background-color:transparent;border-top:dotted 1px <%= BorderColour %>;" width="100%">
						Card options
					</td>
				</tr>
			</table>
			<table width="100%" border="0" cellspacing="0" cellpadding="0" class="PaymentTable">
				<tr>
					<td style="background-color:transparent;" valign="top">
						<asp:RadioButton runat="server" ID="SavedCardOptionsSavedCard" GroupName="SavedCardOptions" onselect="PaymentToggleSavedCardOptions();" onclick="PaymentToggleSavedCardOptions();" Text="Use a saved card" Checked="true" />
					</td>
				</tr>
				<tr>
					<td style="background-color:transparent;" valign="top">
						<asp:RadioButton runat="server" ID="SavedCardOptionsNewCard" GroupName="SavedCardOptions" onselect="PaymentToggleSavedCardOptions();" onclick="PaymentToggleSavedCardOptions();" Text="Enter new card details" />
					</td>
				</tr>
			</table>
			<asp:Panel runat="server" ID="SavedCardInnerPanel">
				<table cellpadding="0" cellspacing="0" class="PaymentTable">
					<tr>
						<td style="background-color: transparent;">
							<small>Saved cards</small><br />
							<asp:DropDownList ID="SavedCardDropDownList" runat="server" CssClass="paymentInput" onmouseover="stt('Select saved card.');" onmouseout="htm();"></asp:DropDownList>
						</td>
						<td style="background-color: transparent; padding-bottom:6px;" valign="bottom">
							<small><asp:LinkButton runat="server" CausesValidation="false" onmouseover="stt('Click here to remove this card from the saved list.');" onmouseout="htm();" ID="DeleteCardLinkButton" OnClick="DeleteCardLinkButton_Click" Text="Delete card" /></small>
						</td>
					</tr>
				</table>
				<table cellpadding="0" cellspacing="0" class="PaymentTable">
					<tr>
						<td style="background-color: transparent;">
							<small>Enter your password</small><br />
							<asp:TextBox ID="PasswordTextBox" TextMode="Password" CssClass="paymentInput" runat="server" onmouseover="stt('Enter your password for verification.');" onmouseout="htm();"></asp:TextBox>
						</td>
						<td valign="bottom" style="background-color: transparent; vertical-align: bottom;">
							<button id="PayWithSavedCardButton" runat="server" onmouseover="stt('Click to pay using the selected card.');" onmouseout="htm();" class="thickbox" onserverclick="PayWithSavedCard_Click" validationgroup="PaymentValidation" causesvalidation="true">Pay now</button>
							&nbsp;<a href="https://www.dontstayin.com/popup/secure" onclick="openPopup('https://www.dontstayin.com/popup/secure');return false;"><img src="/gfx/icon-lock-small.png" border="0" width="17" height="21" onmouseover="stt('This page is 100% secure. Click for more information.');" onmouseout="htm();" /></a>
    					</td>
					</tr>
				</table>
			</asp:Panel>
			<asp:CustomValidator ID="PayWithSavedCardPasswordValidator" runat="server" Display="None" ValidationGroup="PaymentValidation" EnableClientScript="False" ErrorMessage="Incorrect password" OnServerValidate="SavedCardPasswordVal"/>
		</asp:Panel>
		<asp:Panel runat="server" ID="NewCardPanel">
			<table cellpadding="0" cellspacing="0" class="PaymentTable">
				<tr>
					<td style="background-color:transparent;" runat="server" id="NameCell">
						<small>Name&nbsp;on&nbsp;card</small><br />
						<asp:TextBox runat="server" ID="Name" MaxLength="150" CssClass="paymentInput" onmouseover="stt('Enter your full name.');" onmouseout="htm();"/>
						<asp:RequiredFieldValidator ID="NameVal" Runat="server" Display="None" ValidationGroup="PaymentValidation" EnableClientScript="False" ControlToValidate="Name" ErrorMessage="Please enter a name"/>
					</td>
					<td style="background-color:transparent;vertical-align:bottom;padding-bottom:2px;">
						<small>Cards&nbsp;accepted</small><br />
						<img src="/gfx/cards.gif" width="149" height="16" style="margin-top:1px;" onmouseover="stt('DELTA<br>SWITCH<br>MasterCard<br>SOLO<br>VISA<br>VISA Electron<br>Maestro');" onmouseout="htm();">
					</td>
				</tr>
			</table>
			<table cellpadding="0" cellspacing="0" class="PaymentTable" visible="false">
				<tr>
					<td style="background-color:transparent;" runat="server" id="AddressCell">
						<small>First&nbsp;line&nbsp;of&nbsp;address</small><br />
						<asp:TextBox runat="server" ID="Address" MaxLength="150" Columns="30" CssClass="paymentInput" onmouseover="stt('Enter the house number and street name that the card is registered to.');" onmouseout="htm();"/>
						<asp:RequiredFieldValidator ID="AddressVal" Runat="server" Display="None" ValidationGroup="PaymentValidation" EnableClientScript="False" ControlToValidate="Address" ErrorMessage="Please enter an address"/>
					</td>
					<td style="background-color:transparent;" runat="server" id="PostcodeCell">
						<small>Postcode/Zip</small><br />
						<asp:TextBox runat="server" ID="Postcode" Columns="12" CssClass="paymentInput" MaxLength="9" onmouseover="stt('Enter the postcode / zipcode that the card is registered to.');" onmouseout="htm();"/>
						<asp:RequiredFieldValidator ID="PostcodeVal" Runat="server" Display="None" ValidationGroup="PaymentValidation" EnableClientScript="False" ControlToValidate="Postcode" ErrorMessage="Please enter a postcode"/>
						<asp:RegularExpressionValidator ID="PostcodeVal1" Runat="server" Enabled="False" Display="None" ValidationGroup="PaymentValidation" EnableClientScript="False" ControlToValidate="Postcode" ErrorMessage="Please check your postcode - it should be a full UK postcode"/>
					</td>
				</tr>
				<tr runat="server" id="uiFullAddressRow1" visible="false">
					<td>
						<small>Second&nbsp;line&nbsp;of&nbsp;address</small><br />
						<asp:TextBox runat="server" ID="uiAddressArea" MaxLength="150" Columns="30" CssClass="paymentInput" onmouseover="stt('Enter the second line of the address that the card is registered to.');" onmouseout="htm();"/>
					</td>
					<td>
						<small>Town/City</small><br />
						<asp:TextBox runat="server" ID="uiAddressTown" MaxLength="150" Columns="12" CssClass="paymentInput" onmouseover="stt('Enter the town that the card is registered to.');" onmouseout="htm();"/>
					</td>
				</tr>
				<tr runat="server" id="uiFullAddressRow2" visible="false">
					<td>
						
					</td>
					<td>
						<small>County</small><br />
						<asp:TextBox runat="server" ID="uiAddressCounty" Columns="12" CssClass="paymentInput" MaxLength="9" onmouseover="stt('Enter the county that the card is registered to.');" onmouseout="htm();"/>
					</td>
				</tr>
			</table>
			<table cellpadding="0" cellspacing="0" id="CountryTable" runat="server" visible="false" class="PaymentTable">
				<tr>
					<td style="background-color:transparent;" runat="server" id="CountryCell">
						<small>Country</small><br />
						<asp:DropDownList runat="server" ID="CountryDropDownList" CssClass="paymentInput" Width="265" onmouseover="stt('Enter the country that the card is registered to.');" onmouseout="htm();"/>
					</td>
				</tr>
			</table>
			<table cellpadding="0" cellspacing="0" class="PaymentTable">
				<tr>
					<td style="background-color:transparent;">
						<small>Card&nbsp;number</small><br />
						<asp:TextBox runat="server" id="CardNumber" columns="24" CssClass="paymentInput" maxlength="20" onmouseover="stt('This is the long number printed across the front of your card.');" onmouseout="htm();"/>
					</td>
					<td style="background-color:transparent;">
						<small>CV2</small><br />
						<asp:TextBox runat="server" ID="Cv2" Columns="3" CssClass="paymentInput" MaxLength="3" onmouseover="stt('This is the last 3 digits of the number printed<br>in the signature strip on the back of your card.');" onmouseout="htm();"/>
					</td>
					<td style="background-color:transparent;padding-top:14px;">
						<asp:CheckBox ID="SaveCardCheckBox" runat="server" Text="<nobr><small>Save card</small></nobr>" Checked="true" />
					</td>
				</tr>
			</table>
			<table cellpadding="0" cellspacing="0" class="PaymentTable">
				<tr>
					<td style="background-color:transparent;">
						<small>Valid&nbsp;from</small><br />
						<nobr><asp:TextBox runat="server" ID="StartDateMonth" width="28" CssClass="paymentInput" MaxLength="2" onmouseover="stt('Enter the \'valid from\' date in the MM/YY format.<br>If your card doesn\'t have a valid from date, leave this blank.');" onmouseout="htm();"/><small>/</small><asp:TextBox runat="server" ID="StartDateYear" width="28" CssClass="paymentInput" MaxLength="2" onmouseover="stt('Enter the \'valid from\' date in the MM/YY format.<br>If your card doesn\'t have a valid from date, leave this blank.');" onmouseout="htm();"/></nobr></td>
					<td style="background-color:transparent;">
						<small>Expires&nbsp;end</small><br />
						<nobr><asp:TextBox runat="server" ID="EndDateMonth" width="28" CssClass="paymentInput" MaxLength="2" onmouseover="stt('Enter the \'expires end\' date in the MM/YY format.');" onmouseout="htm();"/><small>/</small><asp:TextBox runat="server" ID="EndDateYear" width="28" CssClass="paymentInput" MaxLength="2" onmouseover="stt('Enter the \'expires end\' date in the MM/YY format.');" onmouseout="htm();"/></nobr></td>
					<td style="background-color:transparent;">
						<small>Issue</small><br />
						<asp:TextBox runat="server" ID="Issue" width="20" CssClass="paymentInput" MaxLength="2" onmouseover="stt('Enter the issue number here.<br>If your card doesn\'t have an issue number, leave this blank.');" onmouseout="htm();"/>
					</td>
					<td valign="bottom" style="background-color:transparent; vertical-align:bottom;">
						<button id="PayWithNewCardButton" runat="server" onmouseover="stt('Click to make your payment.');" onmouseout="htm();" class="thickbox" onserverclick="PayWithNewCard_Click" validationgroup="PaymentValidation" causesvalidation="true">Pay now</button>
					</td>
					<td valign="bottom" style="background-color:transparent; vertical-align:bottom; padding-left:0px;">
						<a href="https://www.dontstayin.com/popup/secure" onclick="openPopup('https://www.dontstayin.com/popup/secure');return false;"><img src="/gfx/icon-lock-small.png" border="0" width="17" height="21" onmouseover="stt('This page is 100% secure. Click for more information.');" onmouseout="htm();"/></a>
					</td>
				</tr>
			</table>
			
			<asp:CustomValidator ID="CardValidator" Runat="server" Display="None" ValidationGroup="PaymentValidation" EnableClientScript="False" OnServerValidate="CardVal1" ErrorMessage="Please enter your card number"/>
			<asp:CustomValidator ID="CardNumValidator" Runat="server" Display="None" ValidationGroup="PaymentValidation" EnableClientScript="False" OnServerValidate="CardNumVal" ErrorMessage="Please check your card number - it should be 16, 18, 19, or 20 digits"/>
			
			<asp:CustomValidator ID="CardCv2Validator" Runat="server" Display="None" ValidationGroup="PaymentValidation" EnableClientScript="False" OnServerValidate="CardCv2Val" ErrorMessage="Please enter the CV2 code - this is the last 3 digits of the number in the signature strip on the back of your card"/>
			<asp:CustomValidator ID="CardCv2RegexValidator" Runat="server" Display="None" ValidationGroup="PaymentValidation" EnableClientScript="False" OnServerValidate="CardCv2RegexVal" ErrorMessage="Please check the CV2 code - this is the last 3 digits of the number in the signature strip on the back of your card"/>
			
			<asp:CustomValidator ID="CardEndDateMonthValidator" Runat="server" Display="None" ValidationGroup="PaymentValidation" EnableClientScript="False" OnServerValidate="CardEndDateMonthVal" ErrorMessage="Please enter an expiry date (month)"/>
			<asp:CustomValidator ID="CardEndDateYearValidator" Runat="server" Display="None" ValidationGroup="PaymentValidation" EnableClientScript="False" OnServerValidate="CardEndDateYearVal" ErrorMessage="Please enter an expiry date (year)"/>
			
			<asp:CustomValidator ID="ValidFromVal" Runat="server" Display="None" ValidationGroup="PaymentValidation" EnableClientScript="False" OnServerValidate="CardStartDateVal" ErrorMessage="Please check the valid from date - it should be in the format MM/YY. If your card doesn't have a valid from date, leave this blank"/>
			<asp:CustomValidator ID="ExpiryVal" Runat="server" Display="None" ValidationGroup="PaymentValidation" EnableClientScript="False" OnServerValidate="CardEndDateVal" ErrorMessage="Please check the expiry date - it should be in the format MM/YY"/>
		</asp:Panel>
	</asp:Panel>
	<asp:Panel ID="PayLaterPanel" runat="server">
		<asp:Panel runat="server" ID="PayLaterIssueInvoicePanel">
			<table cellpadding="0" cellspacing="0" Width="100%" class="PaymentTable">
				<tr>
					<td style="background-color:transparent;" width="100%">
						<asp:Label runat="server" ID="PayLaterIssueInvoiceLabel"></asp:Label>
					</td>
				</tr>
				<tr>
					<td style="background-color:transparent;" width="100%">
						<button id="Button2" runat="server" onmouseover="stt('Click to pay using credit.');" onmouseout="htm();" class="thickbox" onserverclick="PayLaterIssueInvoice_Click" causesvalidation="false">Pay on credit</button>
					</td>
				</tr>
			</table>
		</asp:Panel>
		<asp:Panel runat="server" ID="PayLaterNoCreditLimitPanel">
			<table cellpadding="0" cellspacing="0" Width="100%" class="PaymentTable">
				<tr>
					<td style="background-color:transparent;" width="100%">
						To apply for a credit account, <a href="" runat="server" id="CreditApplicationLink">click here</a>. To pay now by card, click the <b>Pay Now</b> button above.
					</td>
				</tr>
			</table>
		</asp:Panel>
		<asp:Panel runat="server" ID="PayLaterNoCreditAvailablePanel">
			<table cellpadding="0" cellspacing="0" Width="100%" class="PaymentTable">
				<tr>
					<td style="background-color:transparent;" width="100%">
						Your account has no credit available. To extend your credit, call us on 0207 835 5599. To pay now by card, click the <b>Pay Now</b> button above.
					</td>
				</tr>
			</table>
		</asp:Panel>
		<asp:Panel runat="server" ID="PayLaterPartialPaymentPanel">
			<table cellpadding="0" cellspacing="0" Width="100%" class="PaymentTable">
				<tr>
					<td style="background-color:transparent;" width="100%">
						<asp:Label runat="server" ID="PayLaterPartialPaymentLabel"></asp:Label>
					</td>
				</tr>
				<tr>
					<td style="background-color:transparent;" width="100%">
						<button id="Button3" runat="server" onmouseover="stt('Click to pay by card.');" onmouseout="htm();" onserverclick="PayPartCardButton_Click" causesvalidation="false">Pay by card</button>
					</td>
				</tr>
			</table>
		</asp:Panel>
	</asp:Panel>
	<asp:Panel ID="PayWithBalancePanel" runat="server">
		<table class="PaymentTable">
			<tr>
				<td style="background-color:transparent;" valign="top"><button id="PayWithBalanceButton" runat="server" onmouseover="stt('Click to pay using existing balance.');" onmouseout="htm();" class="thickbox" onserverclick="PayWithBalanceButton_Click" causesvalidation="false">Pay with balance</button></td>
			</tr>
		</table>
	</asp:Panel>
	<asp:Panel ID="PayWithCampaignCreditPanel" runat="server">
		<asp:Panel ID="NotEnoughCampaignCreditsPanel" runat="server">
			<table cellpadding="0" cellspacing="0" Width="100%" class="PaymentTable">
				<tr>
					<td style="background-color:transparent;" width="100%">
						<asp:Label runat="server" ID="NotEnoughCampaignCreditsLabel"></asp:Label>
					</td>
				</tr>
				<tr>
					<td style="background-color:transparent;" width="100%">
						To buy more campaign credits, click <a href="<%= CurrentPromoter.UrlApp("campaigncredits") %>">here</a>. Alternatively, you can use another payment method above.
					</td>
				</tr>
			</table>
		</asp:Panel>
		<asp:Panel ID="CampaignCreditsRemainingPanel" runat="server">
			<table class="PaymentTable">
				<tr>
					<td style="background-color:transparent;">
						Current campaign credits: <asp:Label ID="CurrentCampaignCreditsLabel" Font-Bold="true" runat="server"></asp:Label>
					</td>
				</tr>
				<tr id="RemainingCampaignCreditsRow" runat="server">
					<td style="background-color:transparent;">
						After this you will have <asp:Label ID="RemainingCampaignCreditsLabel" Font-Bold="true" runat="server"></asp:Label> credits remaining.
					</td>
				</tr>
				<tr>
					<td style="background-color:transparent;">
						<button id="ApplyCampaignCreditBalanceButton" runat="server" onmouseover="stt('Click to apply campaign credit balance.');" onmouseout="htm();" onserverclick="ApplyCampaignCreditBalanceButton_Click" causesvalidation="false">Apply campaign credits</button>
						<button id="PayWithCampaignCreditsButton" runat="server" onmouseover="stt('Click to pay using campaign credits.');" onmouseout="htm();" class="thickbox" onserverclick="PayWithCampaignCreditsButton_Click" causesvalidation="false">Pay with campaign credits</button>
					</td>
				</tr>
			</table>
		</asp:Panel>
	</asp:Panel>
	
	<asp:CustomValidator ID="ProcessingVal" Runat="server" Display="None" ValidationGroup="PaymentValidation" EnableClientScript="False" ErrorMessage="Error processing your card. Please check all details."/>
	<asp:ValidationSummary ID="PaymentValidationSummary" Runat="server" EnableClientScript="False" ShowSummary="True" HeaderText="There were some errors:" CssClass="PaymentValidationSummary" Width="264" Font-Bold="True" DisplayMode="BulletList" ValidationGroup="PaymentValidation"/>	
</div>
