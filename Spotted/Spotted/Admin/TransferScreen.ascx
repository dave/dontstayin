<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TransferScreen.ascx.cs" Inherits="Spotted.Admin.TransferScreen" %>
<%@ Register TagPrefix="DbCombo" Namespace="Cambro.Web.DbCombo" Assembly="Cambro.Web.DbCombo" %>
<%@ Register TagPrefix="DSIControls" TagName="AddOnlyTextBox" Src="/Controls/AddOnlyTextBox.ascx" %>
<dsi:h1 runat="server" ID="H14">Transfer</dsi:h1>
<div class="ContentBorder">
	<asp:Panel ID="MainPanel" runat="server" Width="600px">
		<table width="600px">
			<tr>
				<td colspan="5"><h2>Transfer details</h2></td>
			</tr>
			<tr>
				<td style=" width: 120px;"><asp:Label ID="TransferKLabel" runat="server" Text="Transfer&nbsp;K"></asp:Label></td>
				<td colspan="2"><asp:Label runat="server" ID="TransferKValueLabel" Width="140px"></asp:Label></td>
				<td style="width: 90px;"><asp:Label ID="MethodLabel" runat="server" Text="Method"></asp:Label></td>
				<td style="width: 220px;">
					<asp:DropDownList ID="MethodDropDownList" runat="server" TabIndex="9" Width="140px" AutoPostBack="True" OnSelectedIndexChanged="MethodDropDownList_SelectedIndexChanged"></asp:DropDownList>
					<asp:TextBox ID="MethodTextBox" runat="server" CssClass="disabledTextBox" ReadOnly="True" Visible="False"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td style="width: 120px;"><asp:Label ID="PromoterLabel" runat="server" Text="Promoter"></asp:Label></td>
				<td colspan="2"><js:HtmlAutoComplete ID="uiPromoterAutoComplete" runat="server" WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetPromotersWithK" tabindex="1" Width="150px" AutoPostBack="True" />
					<asp:Label ID="PromoterValueLabel" runat="server" Visible="False"></asp:Label>
				</td>
				<td style="width: 90px;"><asp:Label ID="StatusLabel" runat="server" Text="Status"></asp:Label></td>
				<td style="width: 220px;"><asp:DropDownList ID="StatusDropDownList" runat="server" TabIndex="10" Width="140px">
					</asp:DropDownList><asp:TextBox ID="StatusTextBox" runat="server" CssClass="disabledTextBox"
						ReadOnly="True" Visible="False"></asp:TextBox></td>
			</tr>
			<tr>
				<td style="width: 120px"><asp:Label ID="UserLabel" runat="server" Text="User"></asp:Label></td>
				<td colspan="2"><js:HtmlAutoComplete Width="150px" ID="uiUsersAutoComplete" runat="server"  WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetUsersWithK" TabIndex="2"/><asp:DropDownList ID="UserDropDownList" runat="server" Visible="false">
					</asp:DropDownList><asp:Label ID="UserValueLabel" runat="server" Visible="False"></asp:Label><asp:CustomValidator
					ID="PromoterAndUserCustomValidator" runat="server" EnableClientScript="False" OnServerValidate="PromoterAndUserVal" ErrorMessage="Must select promoter or user" Display="None"></asp:CustomValidator></td>
				<td style=" width: 130px;"><asp:Label ID="AmountLabel" runat="server" Text="Amount"></asp:Label></td>
				<td style=" width: 140px;"><asp:TextBox ID="AmountTextBox" runat="server" Width="87px" TabIndex="11"></asp:TextBox>
				<asp:RequiredFieldValidator ID="AmountRequiredFieldValidator" runat="server" ControlToValidate="AmountTextBox" Display="None" EnableClientScript="False"
					ErrorMessage="Must enter valid amount"></asp:RequiredFieldValidator></td>
			</tr>
			<tr>
				<td style="width: 120px"><asp:Label ID="ActionUserLabel" runat="server" Text="Action&nbsp;user"></asp:Label></td>
				<td colspan="2"><js:HtmlAutoComplete Width="150px" ID="uiActionUserAutoComplete" runat="server"  WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetUsersWithK"/>
					<asp:Label ID="ActionUserValueLabel" runat="server" Visible="False"></asp:Label><asp:RequiredFieldValidator ID="ActionUserRequiredFieldValidator" runat="server"
						ControlToValidate="uiActionUserAutoComplete" ErrorMessage="Must select action user" Display="None"></asp:RequiredFieldValidator></td>
				<td style="width: 130px"><asp:Label ID="CreatedDateLabel" runat="server" Text="Created&nbsp;date" Visible="False"></asp:Label></td>
				<td style="width: 140px"><asp:TextBox ID="CreatedDateTextBox" runat="server" TabIndex="19" Width="95px" MaxLength="20" ReadOnly="True" Visible="False"></asp:TextBox></td>
			</tr>
			<tr>
				<td style="width: 120px;"><asp:Label ID="TypeLabel" runat="server" Text="Type"></asp:Label></td>
				<td colspan="2"><asp:DropDownList ID="TypeDropDownList" runat="server" TabIndex="5" Width="140px" AutoPostBack="True" OnSelectedIndexChanged="TypeDropDownList_SelectedIndexChanged">
					</asp:DropDownList><asp:TextBox ID="TypeTextBox" runat="server" CssClass="disabledTextBox" ReadOnly="True" Visible="False" Width="140px"></asp:TextBox></td>
				<td style="width: 130px;"><asp:Label ID="CompletionDateLabel" runat="server" Text="Completion&nbsp;date" Visible="False"></asp:Label></td>
				<td style="width: 140px;"><asp:TextBox ID="CompletionDateTextBox" runat="server" CssClass="disabledTextBox"	ReadOnly="True" Visible="False" Width="95px"></asp:TextBox></td>
			</tr>
			<tr>
				<td><asp:Label ID="RefundLabel" runat="server" Text="Original&nbsp;transfer" Visible="False"></asp:Label></td>
				<td><asp:HyperLink ID="RefundHyperLink" runat="server" Text="Transfer&nbsp;#98765" Visible="False"></asp:HyperLink><asp:TextBox
					ID="TransferRefundKHiddenTextBox" runat="server" MaxLength="10" ReadOnly="True"	TabIndex="-1" Visible="False" Width="1px"></asp:TextBox></td>
			</tr>
			<tr>
				<td style="width: 120px;" valign="top"><asp:Label ID="NotesLabel" runat="server" Text="Notes"></asp:Label></td>
				<td colspan="4"><DSIControls:AddOnlyTextBox id="NotesAddOnlyTextBox" runat="server"></DSIControls:AddOnlyTextBox></td>
			</tr>
		</table>
	</asp:Panel>
	<asp:Panel ID="CardDetailsPanel" runat="server" Width="600px" TabIndex="30">
		<table width="600">
			<tr>
				<td colspan="5"><h2>Card details</h2></td>
			</tr>
			<tr>
				<td style="width: 150px;"><asp:Label ID="CardTypeLabel" runat="server" Text="Type"></asp:Label></td><td colspan="2">
					<asp:DropDownList ID="CardTypeDropDownList" runat="server" Width="140px" TabIndex="31">
					</asp:DropDownList><asp:TextBox ID="CardTypeTextBox" runat="server" CssClass="disabledTextBox"
						ReadOnly="True" Visible="False" Width="140px"></asp:TextBox>&nbsp;</td>
				<td style="width: 150px;"><asp:Label ID="CardNumberLabel" runat="server" Text="Card&nbsp;number"></asp:Label></td>
				<td style="width: 170px;"><asp:TextBox ID="CardNumberTextBox" runat="server" Width="140px" TabIndex="40" MaxLength="19"></asp:TextBox></td>
			</tr>
			<tr>
				<td style="width: 130px;"><asp:Label ID="CardNameLabel" runat="server" Text="Name"></asp:Label></td>
				<td colspan="2"><asp:TextBox ID="CardNameTextBox" runat="server" Width="140px" TabIndex="32"></asp:TextBox><asp:Label
						ID="CardNameValueLabel" runat="server" Text="" Visible="false"></asp:Label></td>
				<td style="width: 150px;"><asp:Label ID="CardCV2Label" runat="server" Text="CV2"></asp:Label></td>
				<td style="width: 170px;"><asp:TextBox ID="CardCV2TextBox" runat="server" Width="42px" TabIndex="41" MaxLength="3"></asp:TextBox></td>
			</tr>
			<tr>
				<td style="width: 130px;"><asp:Label ID="CardAddressLabel" runat="server" Text="Address"></asp:Label></td>
				<td colspan="2"><asp:TextBox ID="CardAddress1TextBox" runat="server" Width="140px" TabIndex="33"></asp:TextBox><asp:Label
						ID="CardAddress1ValueLabel" runat="server" Text="" Visible="false"></asp:Label></td>
				<td style="width: 130px"><asp:Label ID="CardStartDateLabel" runat="server" Text="Start&nbsp;date"></asp:Label></td>
				<td style="width: 140px"><asp:TextBox ID="CardStartDateMonthTextBox" runat="server" TabIndex="49" Width="20px" MaxLength="2"></asp:TextBox><asp:Label
						ID="CardStartDateDividerLabel" runat="server" Font-Bold="True" Text="/" Width="5px"></asp:Label><asp:TextBox
							ID="CardStartDateYearTextBox" runat="server" MaxLength="2" TabIndex="50" Width="20px"></asp:TextBox></td>
			</tr>
			<tr>
				<td style="width: 130px"></td>
				<td colspan="2"></td>
				<td style="width: 130px"><asp:Label ID="CardExpiryDateLabel" runat="server" Text="Expiry&nbsp;date"></asp:Label></td>
				<td style="width: 140px"><asp:TextBox ID="CardExpiryDateMonthTextBox" runat="server"
						TabIndex="54" Width="20px" MaxLength="2"></asp:TextBox><asp:Label ID="CardExpiryDateDividerLabel"
							runat="server" Font-Bold="True" Text="/" Width="5px"></asp:Label><asp:TextBox ID="CardExpiryDateYearTextBox"
								runat="server" MaxLength="2" TabIndex="55" Width="20px"></asp:TextBox></td>
			</tr>
			<tr>
				<td style=" width: 130px;"><asp:Label ID="CardPostCodeLabel" runat="server" Text="Post&nbsp;code"></asp:Label></td>
				<td colspan="2"><asp:TextBox ID="CardPostCodeTextBox" runat="server" Width="140px" TabIndex="35" MaxLength="9"></asp:TextBox></td>
				<td style="width: 130px;"><asp:Label ID="CardIssueNumberLabel" runat="server" Text="Issue #"></asp:Label></td>
				<td style="width: 140px;"><asp:TextBox ID="CardIssueNumberTextBox" runat="server" Width="42px" TabIndex="60" MaxLength="2"></asp:TextBox></td>
			</tr>
		</table>
	</asp:Panel>
	<asp:Panel ID="CardAdminDetailsPanel" runat="server" Width="600px">
		<table width="600">
			<tr>
				<td colspan="4"><h2>Card admin details</h2></td>
			</tr>
			<tr>
				<td style="width: 130px;"><asp:Label ID="CardAuthorizationCodeLabel" runat="server" Text="Authorization&nbsp;code"></asp:Label></td>
				<td style="width: 150px;"><asp:TextBox ID="CardAuthorizationCodeTextBox" runat="server" Width="140px" TabIndex="62"></asp:TextBox><asp:Label
						ID="CardAuthorizationCodeValueLabel" runat="server" Text="" Visible="false"></asp:Label></td>
				<td style="width: 120px;"><asp:Label ID="CardResponseMessageLabel" runat="server" Text="Response&nbsp;message"></asp:Label></td>
				<td style="width: 140px;"><asp:TextBox ID="CardResponseMessageTextBox" runat="server" Width="140px" TabIndex="64"></asp:TextBox><asp:Label
						ID="CardResponseMessageValueLabel" runat="server" Text="" Visible="false"></asp:Label></td>
			</tr>
			<tr>
				<td style="width: 130px"><asp:Label ID="CardResponseCV2AVSLabel" runat="server" Text="Response&nbsp;CV2&nbsp;AVS"></asp:Label></td>
				<td style="width: 150px"><asp:TextBox ID="CardResponseCV2AVSTextBox" runat="server" Width="140px" TabIndex="63"></asp:TextBox><asp:Label
						ID="CardResponseCV2AVSValueLabel" runat="server" Text="" Visible="false"></asp:Label></td>
				<td style="width: 120px"><asp:Label ID="CardResponseRespCodeLabel" runat="server" Text="Response&nbsp;resp&nbsp;code"></asp:Label></td>
				<td style="width: 140px"><asp:TextBox ID="CardResponseRespCodeTextBox" runat="server" Width="140px" TabIndex="65"></asp:TextBox><asp:Label
						ID="CardResponseRespCodeValueLabel" runat="server" Text="" Visible="false"></asp:Label></td>
			</tr>
		</table>
	</asp:Panel>
	<asp:Panel ID="BankDetailsPanel" runat="server" Width="600px" TabIndex="69">
		<table width="600">
			<tr>
				<td colspan="4"><h2>Bank details</h2></td>
			</tr>
			<tr>
				<td style="width: 130px;"><asp:Label ID="BankNameLabel" runat="server" Text="Bank&nbsp;name"></asp:Label></td>
				<td style="width: 150px;"><asp:TextBox ID="BankNameTextBox" runat="server" Width="140px" TabIndex="70" MaxLength="50"></asp:TextBox><asp:Label
						ID="BankNameValueLabel" runat="server" Text="" Visible="false"></asp:Label></td>
				<td style="width: 120px;"><asp:Label ID="BankAccountNumberLabel" runat="server" Text="Account&nbsp;number"></asp:Label></td>
				<td style="width: 140px;"><asp:TextBox ID="BankAccountNumberTextBox" runat="server" Width="140px" TabIndex="78" MaxLength="50"></asp:TextBox><asp:Label
						ID="BankAccountNumberValueLabel" runat="server" Text="" Visible="false"></asp:Label></td>
			</tr>
			<tr>
				<td style="width: 130px"><asp:Label ID="BankAccountNameLabel" runat="server" Text="Account&nbsp;name"></asp:Label></td>
				<td style="width: 150px"><asp:TextBox ID="BankAccountNameTextBox" runat="server" Width="140px" TabIndex="71" MaxLength="50"></asp:TextBox><asp:Label
						ID="BankAccountNameValueLabel" runat="server" Text="" Visible="false"></asp:Label></td>
				<td style="width: 120px"><asp:Label ID="BankTransferNumberLabel" runat="server" Text="Reference&nbsp;number"></asp:Label></td>
				<td style="width: 140px"><asp:TextBox ID="BankTransferNumberTextBox" runat="server" Width="140px" TabIndex="79" MaxLength="50"></asp:TextBox><asp:Label
						ID="BankTransferNumberValueLabel" runat="server" Text="" Visible="false"></asp:Label></td>
			</tr>
			<tr>
				<td style="width: 130px"><asp:Label ID="BankSortCodeLabel" runat="server" Text="Sort&nbsp;code"></asp:Label></td><td style="width: 150px">
					<asp:TextBox ID="BankSortCodeTextBox" runat="server" Width="140px" TabIndex="72" MaxLength="50"></asp:TextBox><asp:Label
						ID="BankSortCodeValueLabel" runat="server" Text="" Visible="false"></asp:Label></td>
				<td style="width: 120px"></td>
				<td style="width: 140px"></td>
			</tr>
		</table>
	</asp:Panel>
	<asp:Panel ID="ChequeDetailsPanel" runat="server" Width="600px" TabIndex="69" Visible="false">
		<table width="300">
			<tr>
				<td colspan="2"><h2>Cheque details</h2></td>
			</tr>
			<tr>
				<td style="width: 130px;"><asp:Label ID="ChequeReferenceNumberLabel" runat="server" Text="<nobr>Cheque reference #</nobr>"></asp:Label></td>
				<td style="width: 150px;"><asp:TextBox ID="ChequeReferenceNumberTextBox" runat="server" Width="140px" TabIndex="70" MaxLength="50"></asp:TextBox><asp:Label
						ID="ChequeReferenceNumberValueLabel" runat="server" Text="" Visible="false"></asp:Label></td>
			</tr>
		</table>
	</asp:Panel>
	<asp:Panel ID="InvoiceTransferPanel" runat="server" Width="600px" TabIndex="69" Visible="False">
		<table width="600">
			<tr>
				<td colspan="5">
					<h2><asp:Label ID="InvoiceCreditLabel" runat="server" Text="Invoices"></asp:Label></h2>
					<h2>
						<asp:GridView ID="InvoiceTransferGridView" runat="server" AlternatingRowStyle-CssClass="dataGridAltItem"
							AlternatingRowStyle-VerticalAlign="Top" AutoGenerateColumns="False" BorderWidth="0px"
							CellPadding="3" CssClass="dataGrid" GridLines="None" HeaderStyle-CssClass="dataGridHeader"
							RowStyle-VerticalAlign="Top" SelectedRowStyle-CssClass="dataGridSelectedItem" TabIndex="50">
							<Columns>
								<asp:BoundField DataField="InvoiceK" HeaderText="Invoice&#160;K">
									<HeaderStyle Wrap="False" />
								</asp:BoundField>
								<asp:BoundField DataField="Amount" DataFormatString="{0:c}" HeaderText="Amount"
									HtmlEncode="False" >
									<ItemStyle HorizontalAlign="Right" />
								</asp:BoundField>
								<asp:HyperLinkField DataNavigateUrlFields="InvoiceK" DataNavigateUrlFormatString="/admin/invoicescreen/K-{0}"
									DataTextField="InvoiceK" DataTextFormatString="Invoice&#160;#{0}" HeaderText="View&#160;Invoice" />
							</Columns>
							<RowStyle VerticalAlign="Top" />
							<EmptyDataTemplate>
								<asp:Label ID="EmptyDataLabel" runat="server" Text="Not&nbsp;applied"></asp:Label>
							</EmptyDataTemplate>
							<SelectedRowStyle CssClass="dataGridSelectedItem" />
							<HeaderStyle CssClass="dataGridHeader" />
							<AlternatingRowStyle CssClass="dataGridAltItem" VerticalAlign="Top" />
						</asp:GridView>
						</h2>
				</td>
			</tr>
		</table>
	</asp:Panel>
	<h2>
	Company
	</h2>
	<p>
		<asp:DropDownList ID="CompanyDropDownList" runat="server" TabIndex="9" Width="100px">
			<asp:ListItem Value="0" Text="Unknown" />
			<asp:ListItem Value="1" Text="DSI" />
			<asp:ListItem Value="2" Text="DH" />
		</asp:DropDownList>
	</p>
	<asp:Panel ID="RefundTransferPanel" runat="server" Width="600px" TabIndex="70" Visible="False">
		<table width="600">
			<tr>
				<td colspan="5">
					<h2><asp:Label ID="RefundTransferLabel" runat="server" Text="Refunds"></asp:Label></h2>
					<h2>
						<asp:GridView ID="RefundTransferGridView" runat="server" AlternatingRowStyle-CssClass="dataGridAltItem"
							AlternatingRowStyle-VerticalAlign="Top" AutoGenerateColumns="False" BorderWidth="0px"
							CellPadding="3" CssClass="dataGrid" GridLines="None" HeaderStyle-CssClass="dataGridHeader"
							RowStyle-VerticalAlign="Top" SelectedRowStyle-CssClass="dataGridSelectedItem"
							TabIndex="50">
							<Columns>
								<asp:BoundField DataField="K" HeaderText="Transfer&#160;K">
									<HeaderStyle Wrap="False" />
								</asp:BoundField>
								<asp:BoundField DataField="Amount" DataFormatString="{0:c}" HeaderText="Amount"
									HtmlEncode="False" >
									<ItemStyle HorizontalAlign="Right" />
								</asp:BoundField>
								<asp:HyperLinkField DataNavigateUrlFields="K" DataNavigateUrlFormatString="/admin/transferscreen/K-{0}"
									DataTextField="K" DataTextFormatString="Refund&#160;#{0}" HeaderText="View&#160;Refund" />
							</Columns>
							<RowStyle VerticalAlign="Top" />
							<EmptyDataTemplate>
								<asp:Label ID="EmptyDataLabel" runat="server" Text="No&nbsp;refunds"></asp:Label>
							</EmptyDataTemplate>
							<SelectedRowStyle CssClass="dataGridSelectedItem" />
							<HeaderStyle CssClass="dataGridHeader" />
							<AlternatingRowStyle CssClass="dataGridAltItem" VerticalAlign="Top" />
						</asp:GridView>
					</h2>
				</td>
			</tr>
		</table>
	</asp:Panel>
	<table width="600">
		<tr>
			<td colspan="2" valign="top"><br />
				<asp:ValidationSummary ID="TransferValidationSummary" runat="server" CssClass="PaymentValidationSummary" 
					DisplayMode="BulletList" EnableClientScript="False" Font-Bold="True" HeaderText="There were some errors:"
					ShowSummary="True" Width="600" /><asp:RequiredFieldValidator ID="CardNameRequiredFieldValidator" runat="server" ControlToValidate="CardNameTextBox"
					Display="None" EnableClientScript="False" ErrorMessage="Check Name on the card"></asp:RequiredFieldValidator><asp:RequiredFieldValidator
					ID="CardAddressRequiredFieldValidator" runat="server" ControlToValidate="CardAddress1TextBox"
					Display="None" EnableClientScript="False" ErrorMessage="Check Address (line 1) on the card"></asp:RequiredFieldValidator><asp:RequiredFieldValidator
					ID="CardPostCodeRequiredFieldValidator" runat="server" ControlToValidate="CardPostCodeTextBox" 
					Display="None" EnableClientScript="False" ErrorMessage="Check Post Code for card billing"></asp:RequiredFieldValidator><asp:CustomValidator ID="CardNumberCustomValidator" Runat="server" Display="None" EnableClientScript="False" OnServerValidate="CardNumVal" 
					 ErrorMessage="Check Card Number - it should be 16, 18 or 19 digits"/><asp:RequiredFieldValidator
					ID="CardCV2RequiredFieldValidator" runat="server" ControlToValidate="CardCV2TextBox" Display="None" EnableClientScript="False" 
					 ErrorMessage="Check CV2 code - its the last 3 digits on the signature strip on the back of the card"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
					ID="CardCV2RegularExpressionValidator" runat="server" ControlToValidate="CardCV2TextBox" Display="None" EnableClientScript="False"  
					ErrorMessage="Check CV2 code - its the last 3 digits on the signature strip on the back of the card"
					ValidationExpression="^\d{3}$"></asp:RegularExpressionValidator><asp:CustomValidator
					ID="CardStartDateCustomValidator" runat="server" Display="None" EnableClientScript="False" 
					ErrorMessage="Check Start Date - it should be in the format MM/YY. If the card doesn't have a start date, leave this blank"
					OnServerValidate="StartDateVal"></asp:CustomValidator><asp:CustomValidator ID="CardExpiryDateCustomValidator" 
					runat="server" Display="None" EnableClientScript="False" ErrorMessage="Check Expiry Date - it should be in the format MM/YY"
					OnServerValidate="ExpiryDateVal"></asp:CustomValidator><asp:RequiredFieldValidator ID="BankNameRequiredFieldValidator" runat="server" ControlToValidate="BankNameTextBox"
					Display="None" EnableClientScript="False" ErrorMessage="Check Bank Name"></asp:RequiredFieldValidator><asp:RequiredFieldValidator ID="BankAccountNameRequiredFieldValidator" runat="server" ControlToValidate="BankAccountNameTextBox"
					Display="None" EnableClientScript="False" ErrorMessage="Check Bank Account Name"></asp:RequiredFieldValidator><asp:RequiredFieldValidator ID="BankSortCodeRequiredFieldValidator" runat="server" ControlToValidate="BankSortCodeTextBox"
					Display="None" EnableClientScript="False" ErrorMessage="Check Bank Sort Code"></asp:RequiredFieldValidator><asp:RequiredFieldValidator ID="BankAccountNumberRequiredFieldValidator" runat="server" ControlToValidate="BankAccountNumberTextBox"
					Display="None" EnableClientScript="False" ErrorMessage="Check Bank Account Number"></asp:RequiredFieldValidator><asp:RequiredFieldValidator ID="BankTransferRequiredFieldValidator" runat="server"
					ControlToValidate="BankTransferNumberTextBox" Display="None" EnableClientScript="False"
					ErrorMessage="Check Bank Reference Number"></asp:RequiredFieldValidator><asp:CustomValidator ID="ProcessingCustomValidator" 
					runat="server" Display="None" EnableClientScript="False" ErrorMessage="Error processing your card. Please check all details."></asp:CustomValidator><asp:CustomValidator
						ID="ProcessingVal" runat="server" Display="None" EnableClientScript="False" ErrorMessage=""></asp:CustomValidator></td>
		</tr>
		<tr>
			<td align="left" width="160"><asp:Label ID="RefundAmountLabel" runat="server" Text="Refund&nbsp;amount" Visible="false"></asp:Label><br /><asp:TextBox ID="RefundAmountTextBox" runat="server" Visible="False" Width="87px">&#163;0.00</asp:TextBox>
				<asp:Button ID="RefundButton" runat="server" Text="Refund this transfer" Visible="False" Font-Bold="False" OnClick="RefundButton_Click" TabIndex="87" Width="140px" /></td>
			<td align="right" valign="bottom"><nobr><button id="CreateCampaignCreditsButton" runat="server" onserverclick="CreateCampaignCreditsButton_Click" causesvalidation="false">Create campaign credits</button>&nbsp;
				<asp:Button ID="DownloadButton" runat="server" Text="Download" OnClick="DownloadButton_Click" TabIndex="89" Width="80px" CausesValidation="False"/>&nbsp;
				<asp:Button ID="SaveButton" runat="server" Text="Save" Font-Bold="False" Width="80px" OnClick="SaveButton_Click" TabIndex="90" />&nbsp;
				<asp:Button ID="CancelButton" runat="server" Text="Cancel" Font-Bold="False" Width="80px" OnClick="CancelButton_Click" TabIndex="95" CausesValidation="False" /></nobr></td>
		</tr>
	</table>
</div>
