<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Home.ascx.cs" Inherits="Spotted.Pages.Promoters.Home" %>
<%@ Register TagPrefix="DsiControls" TagName="TimeControl" Src="/Controls/TimeControl.ascx" %>
<%@ Register TagPrefix="DbCombo" Namespace="Cambro.Web.DbCombo" Assembly="Cambro.Web.DbCombo" %>

<asp:Panel Runat="server" ID="AdminPanel">
	<script language="JavaScript">
	  function PromoterHomeScreenToggleOverrideInvoiceDueDays()
	  {
		document.getElementById("<%=  InvoiceDueDaysTextBox.ClientID  %>").style.display = document.getElementById("<%=  OverrideInvoiceDueDaysCheckBox.ClientID  %>").checked?'':'none';
		if(!document.getElementById("<%=  OverrideInvoiceDueDaysCheckBox.ClientID  %>").checked)
			document.getElementById("<%=  InvoiceDueDaysTextBox.ClientID  %>").value = '0';
	  }
	</script>
	<dsi:h1 ID="AdminH1" runat="server">Admin-only panel</dsi:h1>
	<div class="AdminBorder" runat="server" id="AdminDiv">
		<h2>
			Client status
		</h2>
		<p id="StatusP" runat="server" />
		<table id="SalesAccountTable" runat="server" border="0" cellpadding="0" 
		cellspacing="0" visible="false">
			<tr style="padding-bottom:3px">
				<td id="SalesUsrTD1" runat="server" style="padding-right:15px">
					<nobr>
					<b>Account manager</b></nobr></td>
				<td id="SalesStatusTD1" runat="server" style="padding-right:15px">
					<nobr>
					<b>Sales status</b></nobr></td>
				<td id="SalesStatusExpiresTD1" runat="server" style="padding-right:15px">
					<b>Expires</b></td>
				<td>
				</td>
			</tr>
			<tr>
				<td id="SalesUsrTD2" runat="server" style="padding-right:15px">
					<asp:DropDownList ID="SalesPersonsDropDownList" runat="server">
					</asp:DropDownList>
				</td>
				<td id="SalesStatusTD2" runat="server" style="padding-right:15px">
					<asp:DropDownList ID="SalesStatusDropDownList" runat="server">
					</asp:DropDownList>
				</td>
				<td id="SalesStatusExpiresTD2" runat="server" style="padding-right:15px">
					<dsi:Cal ID="SalesStatusExpiresCal" runat="server" />
				</td>
				<td>
					<button id="SaveSalesAccountButton" runat="server" causesvalidation="false" onmouseover="htm();" onserverclick="SaveSalesAccountButton_Click">Save</button>
					<asp:Label ID="SalesAccountSavedLabel" runat="server" EnableViewState="false" ForeColor="blue" Visible="false">Saved</asp:Label>
				</td>
			</tr>
		</table>
		<h2>
			Admin users
		</h2>
		<p id="AdminUsrP" runat="server" />
			&nbsp;<asp:Panel ID="ImportantCallsPanel" runat="server">
			<h2>
				Important calls / notes
			</h2>
			<asp:GridView ID="ImportantCallsGridView" runat="server" AllowPaging="False" 
			AlternatingRowStyle-VerticalAlign="Top" AutoGenerateColumns="False" 
			BorderWidth="0" CellPadding="3" GridLines="None" 
			HeaderStyle-CssClass="dataGridHeader" RowStyle-CssClass="CleanLinks" 
			RowStyle-VerticalAlign="Top" TabIndex="25" Width="565">
				<columns>
					<asp:templatefield HeaderText="SalesCallK" SortExpression="K" Visible="False">
						<itemstyle horizontalalign="Left" />
						<itemtemplate>
							<asp:Label ID="SalesCallKLabel" runat="server" 
							Text="<%# ((SalesCall)Container.DataItem).K %>"></asp:Label>
						</itemtemplate>
						<headerstyle horizontalalign="Left" />
					</asp:templatefield>
					<asp:templatefield HeaderText="" SortExpression="">
						<itemstyle horizontalalign="Left" width="535" />
						<itemtemplate>
							<asp:Label ID="TypeLabel" runat="server" 
							Text="<%# SalesCallToString((SalesCall)Container.DataItem) %>"></asp:Label>
						</itemtemplate>
						<headerstyle horizontalalign="Left" />
					</asp:templatefield>
					<asp:templatefield HeaderStyle-HorizontalAlign="Justify" HeaderText="Important" 
					ShowHeader="true">
						<itemstyle horizontalalign="Center" />
						<itemtemplate>
							<asp:CheckBox ID="ImportantSalesCallCheckBox" runat="server" 
							AutoPostBack="True" 
							Checked="<%# ((SalesCall)Container.DataItem).IsImportant %>" 
							OnCheckedChanged="ImportantSalesCallCheckBox_CheckedChanged" />
						</itemtemplate>
					</asp:templatefield>
				</columns>
			</asp:GridView>
		</asp:Panel>
		<asp:Panel ID="CallsPanel" runat="server"><br />
			<h2>
				Calls / notes
			</h2>
			<div id="CallsDiv" runat="server">
				<asp:GridView ID="CallsGridView" runat="server" AllowPaging="False" 
				AlternatingRowStyle-VerticalAlign="Top" AutoGenerateColumns="False" 
				BorderWidth="0" CellPadding="3" GridLines="None" 
				HeaderStyle-CssClass="dataGridHeader" RowStyle-CssClass="CleanLinks" 
				RowStyle-VerticalAlign="Top" TabIndex="25" Width="565">
					<columns>
						<asp:templatefield HeaderText="SalesCallK" SortExpression="K" Visible="False">
						
						
						
						
						
							<itemstyle horizontalalign="Left" />
							<itemtemplate>
								<asp:Label ID="SalesCallKLabel" runat="server" 
								Text="<%# ((SalesCall)Container.DataItem).K %>"></asp:Label>
							</itemtemplate>
						
						
						
						
						
							<headerstyle horizontalalign="Left" />
						</asp:templatefield>
						<asp:templatefield HeaderText="" SortExpression="">
						
						
						
						
						
							<itemstyle horizontalalign="Left" width="535" />
							<itemtemplate>
								<asp:Label ID="TypeLabel" runat="server" 
								Text="<%# SalesCallToString((SalesCall)Container.DataItem) %>"></asp:Label>
							</itemtemplate>
						
						
						
						
						
							<headerstyle horizontalalign="Left" />
						</asp:templatefield>
						<asp:templatefield HeaderStyle-HorizontalAlign="Justify" HeaderText="Important" 
						ShowHeader="true">
						
						
						
						
						
							<itemstyle horizontalalign="Center" />
							<itemtemplate>
								<asp:CheckBox ID="ImportantSalesCallCheckBox" runat="server" 
								AutoPostBack="True" 
								Checked="<%# ((SalesCall)Container.DataItem).IsImportant %>" 
								OnCheckedChanged="ImportantSalesCallCheckBox_CheckedChanged" />
							</itemtemplate>
						
						
						
						
						
						</asp:templatefield>
					</columns>
				</asp:GridView>
			</div>
		</asp:Panel>
		<div style="width:290px;">
			<table border="0" cellpadding="3" cellspacing="0">
				<tr>
					<td valign="bottom">
						<h2>
							Add a note
						</h2>
					</td>
					<td align="center" valign="bottom">
						<small>Important</small>
					</td>
					<td></td>
				</tr>
				<tr>
					<td>
						<asp:TextBox ID="NoteTextBox" runat="server" Columns="45" 
						Text="add a note here..."></asp:TextBox>
					</td>
					<td align="center">
						<asp:CheckBox ID="ImportantNoteCheckBox" runat="server" EnableViewState="false" />
					</td>
					<td>
						<button id="SaveNoteButton" runat="server" causesvalidation="false" onmouseover="htm();" onserverclick="SaveNoteClick">Save</button>
						<asp:Label ID="NoteSavedLabel" runat="server" EnableViewState="false" ForeColor="blue" Visible="false">Saved</asp:Label>
					</td>
				</tr>
			</table>
		</div>
		<div style="width:190px;">
			<table border="0" cellpadding="3" cellspacing="0">
				<tr>
					<td colspan="2" valign="bottom">
						<h2>
							<nobr>
							Next call
							<asp:Label ID="SalesHoldLabel" runat="server"></asp:Label>
							</nobr>
						</h2>
					</td>
					<td valign="bottom">
					</td>
					<td valign="bottom">
						<small>Snooze</small></td>
					<td align="center" valign="bottom">
						<small>Alarm</small></td>
					<td>
					</td>
				</tr>
				<tr>
					<td>
						<nobr><dsi:Cal ID="NextCallCal" runat="server" /></nobr>
					</td>
					<td>
						<DSIControls:TimeControl ID="NextCallTime" runat="server" />
					</td>
					<td>
						<nobr>
						or
						</nobr>
					</td>
					<td>
						<asp:DropDownList ID="SnoozeDropDownList" runat="server" 
						EnableViewState="false">
							<asp:ListItem Value=""></asp:ListItem>
							<asp:ListItem Value="5">5 mins</asp:ListItem>
							<asp:ListItem Value="30">30 mins</asp:ListItem>
							<asp:ListItem Value="60">1 hour</asp:ListItem>
							<asp:ListItem Value="120">2 hours</asp:ListItem>
							<asp:ListItem Value="240">4 hours</asp:ListItem>
							<asp:ListItem Value="1440">1 day</asp:ListItem>
							<asp:ListItem Value="2880">2 days</asp:ListItem>
							<asp:ListItem Value="4320">3 days</asp:ListItem>
							<asp:ListItem Value="10080">1 week</asp:ListItem>
							<asp:ListItem Value="20160">2 weeks</asp:ListItem>
						</asp:DropDownList>
						<asp:CustomValidator ID="NextCallTimeVal" runat="server" Display="Dynamic" 
						ErrorMessage="&lt;nobr&gt;Select time OR snooze&lt;/nobr&gt;" 
						OnServerValidate="NextCallTimeValidation" 
						ValidationGroup="PromoterNextCallTime"></asp:CustomValidator>
					</td>
					<td align="center">
						<asp:CheckBox ID="AlarmCheckBox" runat="server" Enabled="true" 
						EnableViewState="false" />
					</td>
					<td>
						<nobr>
							<button id="NextCallSaveButton" runat="server" causesvalidation="false" onserverclick="NextCallSave">Save</button>
							<button id="Button1" runat="server" causesvalidation="false" onserverclick="SalesCallHold">Hold</button>
						</nobr>
						<asp:Label ID="SaveNextCallDoneLabel" runat="server" EnableViewState="false" ForeColor="blue" Visible="false">Saved</asp:Label>
						<asp:Label ID="SaveNextCallErrorLabel" runat="server" EnableViewState="false" ForeColor="red" Visible="false">ERROR!</asp:Label>
					</td>
				</tr>
			</table>
		</div>
		<div style="width:90px; padding-left:3px;">
			<h2>
				Sales
			</h2>
			<p>
				<asp:DropDownList ID="SalesEstimate" runat="server" AutoPostBack="true"	OnSelectedIndexChanged="SalesEstimateChange"></asp:DropDownList>
				<asp:Label ID="SalesEstimateSavedLabel" runat="server" EnableViewState="false" ForeColor="blue" Visible="false">Saved</asp:Label>
			</p>
		</div>
		<div style="padding-left:3px;">
			<h2>
				Outgoing calls
			</h2>
			<p>
				<asp:DropDownList ID="AdminPhoneNumbersDropDown" runat="server" />
				<button id="SalesCallButton" runat="server" causesvalidation="false" onserverclick="MakeSalesCall">Sales call</button>
				<button id="MiscCallButton" runat="server" causesvalidation="false" onserverclick="MakeMiscCall">Misc call</button>
				<asp:LinkButton ID="ChangeNumberButton" runat="server" OnClick="ChangeNumber" Text="Add / change number" />
				<asp:Label ID="SalesCallError" runat="server" EnableViewState="false" ForeColor="red" Visible="false" />
			</p>
		</div>
		<div style="width:140px;float:left; padding-left:3px;">
			<h2>
				Incoming calls
			</h2>
			<p>
				<button id="TakeIncomingCallButton" runat="server" causesvalidation="false" onserverclick="TakeIncomingCall">Incoming call</button>
				<asp:Label ID="IncomingCallError" runat="server" EnableViewState="false" ForeColor="red" Visible="false" />
			</p>
		</div>
		<div style="width:150px;">
			<h2>
				Status: <%= CurrentPromoter.Status %>
			</h2>
			<p>
				<button id="ActivateButton" runat="server" causesvalidation="false" onserverclick="Activate_Click">Activate</button>
				<button id="DisableButton" runat="server" causesvalidation="false" onserverclick="Disable_Click">Disable</button>
			</p>
		</div>
		<h2>
			Sales Person
		</h2>
		<h2>
			Party brands
		</h2>
		<p>
			<asp:Label ID="NoBrandsLabel" runat="server" Text="No party brands" Visible="false" />
			<asp:Repeater ID="AdminBrandRepeater" Runat="server">
				<itemtemplate>
					<p>
						<b><a href="<%#((Bobs.Brand)Container.DataItem).Url()%>">
						<%#((Bobs.Brand)Container.DataItem).Name%></a></b>-
						<a href='<%#((Bobs.Brand)(Container.DataItem)).UrlApp("edit")%>'>rename</a> -
						<a href='<%#((Bobs.Brand)(Container.DataItem)).UrlApp("edit","page","pic")%>'>add/change picture</a>
						<%#((Bobs.Brand)Container.DataItem).PromoterStatus.Equals(Bobs.Brand.PromoterStatusEnum.Unconfirmed)?" (unconfirmed - <a href=\""+CurrentPromoter.UrlApp("edit","mode","confirmbrand","k",((Bobs.Brand)(Container.DataItem)).K.ToString())+"\">confirm</a>)":""%></p>
				</itemtemplate>
			</asp:Repeater>
			<p>
			</p>
			<h2>
				Venues
			</h2>
			<p>
				<asp:Label ID="NoVenuesLabel" runat="server" Text="No venues" Visible="false" />
				<asp:Repeater ID="AdminVenueRepeater" Runat="server">
					<itemtemplate>
						<p>
							<b><a href="<%#((Bobs.Venue)Container.DataItem).Url()%>">
							<%#((Bobs.Venue)Container.DataItem).Name%></a></b>-
							<a href='<%#((Bobs.Venue)(Container.DataItem)).UrlApp("edit")%>'>edit</a> -
							<a href='<%#((Bobs.Venue)(Container.DataItem)).UrlApp("edit","page","pic")%>'>
							add/change picture</a>
							<%#((Bobs.Venue)Container.DataItem).PromoterStatus.Equals(Bobs.Venue.PromoterStatusEnum.Unconfirmed) ? " (unconfirmed - <a href=\"" + CurrentPromoter.UrlApp("edit", "mode", "confirmvenue", "k", ((Bobs.Venue)(Container.DataItem)).K.ToString()) + "\">confirm</a>)" : ""%>
						</p>
					</itemtemplate>
				</asp:Repeater>
				<p>
				</p>
				<asp:Panel ID="SalesSummaryPanel" runat="server">
					<table>
						<tr valign="top">
							<td>
								<h2>
									Sales summary : Last 6 months
								</h2>
								<table ID="SalesSummaryTable" runat="server" cellpadding="3" cellspacing="0" 
									class="dataGrid">
								</table>
							</td>
							<td style="padding-left:50px;">
								<h2>
									Ticket sales summary
								</h2>
								<table ID="TicketSalesSummaryTable" runat="server" cellpadding="3" 
									cellspacing="0">
									<tr>
										<td>
											Total ticket runs:</td>
										<td align="right">
											<asp:Label ID="TotalTicketRunsLabel" runat="server" Text="0"></asp:Label>
										</td>
									</tr>
									<tr>
										<td>
											Total tickets sold:</td>
										<td align="right">
											<asp:Label ID="TotalTicketsSoldLabel" runat="server" Text="0"></asp:Label>
										</td>
									</tr>
									<tr>
										<td>
											Ticket funds released:</td>
										<td align="right">
											<asp:Label ID="TicketFundsReleasedLabel" runat="server" Text="0"></asp:Label>
										</td>
									</tr>
									<tr>
										<td>
											Ticket funds in waiting:</td>
										<td align="right">
											<asp:Label ID="TicketFundsInWaitingLabel" runat="server" Text="0"></asp:Label>
										</td>
									</tr>
									<tr>
										<td style="padding-top:15px;">
											Ticket funds available:</td>
										<td align="right" style="padding-top:15px;">
											<asp:Label ID="TicketFundsAvailableLabel" runat="server" Text="0"></asp:Label>
										</td>
									</tr>
									<tr ID="TicketFundsToCampaignCreditsRow" runat="server" visible="true">
										<td colspan="2">
											<button ID="CreateCampaignCreditsButton" runat="server" 
												causesvalidation="false" onserverclick="CreateCampaignCreditsButton_Click" 
												tabindex="88">
												Create campaign credits
											</button>
											<br />
											<asp:Label ID="CreateCampaignCreditsResponseLabel" runat="server" 
												ForeColor="Blue" Visible="false"></asp:Label>
										</td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</asp:Panel>
				<asp:Panel ID="AdminEditPanel" runat="server" Visible="false">
					<h2>
						Admin promoter credit details
					</h2>
					<p>
						<table border="0" cellpadding="0" cellspacing="0">
							<tr style="padding-bottom:3px;">
								<td style="padding-right:15px;">
									<nobr>
									<b>Credit limit</b></nobr></td>
								<td style="padding-right:15px;">
									<nobr>
									<b>Invoice due days</b></nobr></td>
								<td style="padding-right:15px;">
									<nobr>
									<b>Plus account</b></nobr></td>
								<td>
									<b>Suppress reminder email</b></td>
							</tr>
							<tr style="padding-bottom:8px;">
								<td style="padding-right:15px; padding-top:1px;" valign="top">
									<asp:TextBox ID="CreditLimitTextBox" runat="server" MaxLength="10" 
										onmouseout="htm();" onmouseover="stt('Credit limit in £GBP.');" Width="70"></asp:TextBox>
									<asp:CustomValidator ID="CreditLimitCustomValidator" runat="server" 
										ControlToValidate="CreditLimitTextBox" Display="Dynamic" 
										ErrorMessage="* invalid" OnServerValidate="CreditLimitTextBoxVal" 
										ValidationGroup="EditPromoter"></asp:CustomValidator>
								</td>
								<td style="padding-right:15px;" valign="top">
									<asp:TextBox ID="InvoiceDueDaysTextBox" runat="server" MaxLength="2" 
										onmouseout="htm();" onmouseover="stt('In days (enter 0 for default).');" 
										Width="30"></asp:TextBox>
									<asp:RangeValidator ID="InvoiceDueDaysRangeValidator" runat="server" 
										ControlToValidate="InvoiceDueDaysTextBox" Display="Dynamic" 
										ErrorMessage="* invalid " MaximumValue="60" MinimumValue="0" Type="Integer" 
										ValidationGroup="EditPromoter"></asp:RangeValidator>
									<asp:CheckBox ID="OverrideInvoiceDueDaysCheckBox" runat="server" 
										onclick="PromoterHomeScreenToggleOverrideInvoiceDueDays();" 
										onselect="PromoterHomeScreenToggleOverrideInvoiceDueDays()" 
										Text="Override&nbsp;default" />
								</td>
								<td style="padding-right:15px;" valign="top">
									<asp:CheckBox ID="EnableTicketsCheckBox" runat="server" Text="Enable" />
									<asp:CustomValidator ID="SaveEditPromoterCustomVal" runat="server" 
										Display="Dynamic" ErrorMessage="Error saving promoter" 
										ValidationGroup="EditPromoter"></asp:CustomValidator>
								</td>
								<td><asp:CheckBox ID="uiEnableSuppressReminderEmailCheckBox" runat="server" /></td>
							</tr>
							<tr style="padding-bottom:3px;">
								<td style="padding-right:15px;">
									<nobr>
									<b>Overdue redirect</b></nobr></td>
								<td style="padding-right:15px;">
									<nobr>
									<b>Auto-apply ticket funds to invoices</b></nobr></td>
								<td style="padding-right:15px;">
									<nobr>
									<b>Discount</b></nobr></td>
								<td>
								</td>
							</tr>
							<tr>
								<td style="padding-right:15px;">
									<asp:CheckBox ID="DisableOverdueRedirectCheckBox" runat="server" 
										Text="Disable" />
								</td>
								<td style="padding-right:15px;">
									<asp:CheckBox ID="OverrideAutoApplyTicketFundsToInvoicesCheckBox" 
										runat="server" Text="Disable" />
								</td>
								<td style="padding-right:15px;">
									<asp:TextBox ID="DiscountTextBox" runat="server" MaxLength="5" 
										onmouseout="htm();" 
										onmouseover="stt('In percent (between -1000 and 99). \'20\' gives them a 20% discount, -100 charges them double.');" 
										Width="30"></asp:TextBox>
									<b>%</b>
									<asp:RangeValidator ID="RangeValidator1" runat="server" 
										ControlToValidate="DiscountTextBox" Display="Dynamic" ErrorMessage="* invalid " 
										MaximumValue="99" MinimumValue="-1000" Type="Integer" 
										ValidationGroup="EditPromoter"></asp:RangeValidator>
								</td>
								<td style="padding-top:1px;">
									<button ID="SaveEditPromoterButton" runat="server" causesvalidation="true" 
										onmouseover="htm();" onserverclick="SaveEditPromoterButton_Click" 
										validationgroup="EditPromoter">
										Save
									</button>
								</td>
							</tr>
						</table>
					</p>
				</asp:Panel>
				<asp:Panel ID="InitSkeletonAccountPanel" runat="server">
					<h2>
						Skeleton account
					</h2>
					<p>
						This is a skeleton account without a primary user. You can assign a primary user 
						and initialise the account below:
					</p>
					<p>
						<js:HtmlAutoComplete ID="uiInitSkeletonAccountAutoComplete" runat="server" 
							WebServiceMethod="GetUsrsPublic" 
							WebServiceUrl="/WebServices/AutoComplete.asmx" />
					</p>
					<p>
						<button runat="server" onserverclick="InitSkeletonAccount_Click">
							Init account
						</button>
					</p>
					<p>
						If they don&#39;t have a user account, tell them to go to dontstayin.com/offer and 
						type in the following code:
					</p>
					<p class="BigCenter">
						<asp:Label ID="InitSkeletonAccountCodeLabel" runat="server"></asp:Label>
					</p>
				</asp:Panel>
				<p>
				</p>
				<p>
				</p>
				<p>
				</p>
				<p>
				</p>
			</p>
		</p>
	</div>
	<script language="JavaScript">
		// run this script after the page has loaded
		PromoterHomeScreenToggleOverrideInvoiceDueDays();
	</script>
</asp:Panel>

<asp:Panel Runat="server" ID="PanelNotEnabled">
	<dsi:h1 runat="server" ID="H12"><%= CurrentPromoter.Name %></dsi:h1>
	<div class="ContentBorder">
		<h2>Call us to enable your account</h2>
		<p>
			Your promoter account is disabled. Just call our promoter hotline
			on 0207 835 5599 to enable it.
		</p>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="PanelEnabled">
	<dsi:PromoterIntro ID="PromoterIntro" runat="server" Header="Promoter options">
		<asp:Panel ID="UsersPanel" Runat="server">
			<p>
				People with access to this promoter account:
				<asp:Repeater ID="UsersRepeater" Runat="server">
					<itemtemplate>
						<a <%#((Bobs.Usr)Container.DataItem).Rollover%>="" 
						href="<%#((Bobs.Usr)Container.DataItem).Url()%>">
						<%#((Bobs.Usr)Container.DataItem).NickName%></a></itemtemplate>
					<separatortemplate>, </separatortemplate>
				</asp:Repeater>
			</p>
		</asp:Panel>
		<p>
			<img align="absmiddle" border="0" src="/gfx/icon-phone.png" style="margin-right:3px;"/>promoter hotline: 0207 835 5599
		</p>
		<p>
			<a href="/chat/k-<%= CurrentPromoter.QuestionsThreadK %>">
			<img align="absmiddle" border="0" src="/gfx/icon-discuss.png" 
			style="margin-right:3px;"/>any questions?</a> &nbsp;
			<a href="/pages/legaltermspromoter/">Terms and conditions for promoters</a>
		</p>
		<p>
			<a href='<%= CurrentPromoter.UrlApp("edit") %>'>
			<img align="absmiddle" border="0" src="/gfx/icon-edit.png" width="26" height="21" 
			style="margin-right:3px;"/>edit account details</a>
		</p>
	</dsi:PromoterIntro>
	<asp:Panel ID="NoEventsPanel" Runat="server">
		<dsi:h1 ID="H15f" runat="server">
			Events
		</dsi:h1>
		<div class="ContentBorder" style="background-color:#ff6666;">
			<p>
				<b>You don't have any events linked to your promoter account. Just give us a quick call on
				0207 835 5599 and we'll set up your account properly. Once you've done that, we will list 
				all your upcoming events here. </b>
			</p>
			<p>
				<b>If you're not an event promoter, please ignore this message. </b>
			</p>
		</div>
	</asp:Panel>
	<asp:Panel ID="IntroOfferPanel" Runat="server">
		<dsi:h1 ID="H15fg" runat="server">
			Introductory offer</dsi:h1>
		<div class="ContentBorder" style="background-color:#99ff66;">
			<p>
				<b>Congratulations on becoming a promoter. To get you started, we are 
				offering you the chance to double your exposure for free. </b>
			</p>
			<p>
				<b>Remember - this offer is only available for a limited time, but you can 
				book any future banners at this reduced rate now. </b>
			</p>
			<p>
				<b>Once you've set up your banner, give us a quick call on 0207 835 5599, and we'll
				double up your slots. e.g. Pay for 2 slots, and get 4 slots. </b>
			</p>
			<p class="BigCenter">
				This offer will expire in
				<asp:Label ID="OfferExpireTimeSpan" runat="server" />
			</p>
		</div>
	</asp:Panel>
		
	<asp:Panel ID="SalesUsrPanel" runat="server">
		<dsi:h1 runat="server">Your account manager</dsi:h1>
		<div class="ContentBorder">
			<p>
				<asp:Label ID="SalesUsrDSIPhotoLinkLabel" runat="server"></asp:Label>
				<span style="font-size:12px;line-height:17px;"><b>Hi, I'm <asp:Label ID="SalesUsrNameLabel" runat="server"></asp:Label> - your account manager. You can phone me any time on my direct line <asp:Label ID="SalesUsrNumberLabel" runat="server"></asp:Label>, send me an email <asp:Label ID="SalesUsrEmailLabel" runat="server"></asp:Label>, or send a private message <asp:Label ID="SalesUsrDSILinkLabel" runat="server"></asp:Label>.</b></span>
			</p>
		</div>
	</asp:Panel>
	
	<table cellpadding="0" cellspacing="0">
		<tr>
			<td width="50%" style="padding-right:13px;" valign="top">
				<dsi:h1 runat="server">Campaign credits</dsi:h1>
				<div class="ContentBorder">
					<p>
						Campaign credits are used for buying stuff on the site, like advertising.
					</p>
					<h2>
						Current campaign credits: <%= CurrentPromoter.CampaignCredits.ToString() %></h2>
					<p>
						<a href="<%= CurrentPromoter.UrlApp("campaigncredits") %>" >
						<img src="/Gfx/icon-add.png" border="0" align="absmiddle" style="margin-right:3px" width="26" height="21"/>add campaign credits</a>
					</p>
					<p>
						<a href="<%= CurrentPromoter.UrlApp("campaigncredithistory") %>" >
						<img src="/Gfx/icon-view.png" border="0" align="absmiddle" style="margin-right:3px" width="26" height="21"/>view campaign credit history</a>
					</p>
				</div>
			</td>
			<td width="50%" valign="top">
				<dsi:h1 runat="server">Invoices</dsi:h1>
				<div class="ContentBorder">
					<p>
						Your account balance is <asp:Label ID="AccountBalanceLabel" runat="server" Font-Bold="true"></asp:Label>.
					</p>
					<p>
						<asp:Label ID="CreditLabel" runat="server"></asp:Label>
					</p>
					<p id="TicketFundsP" runat="server" visible="false"></p>
					<p id="InvoicesOutstandingP" runat="server" visible="false"></p>
					<p id="InvoicesOutstandingPayP" runat="server" visible="false">
						<a href="<%= CurrentPromoter.UrlApp("invoices", "payoutstanding", "true") %>" >
						<img src="/gfx/icon-document-tick.png" border="0" align="absmiddle" width="26" height="21" style="margin-right:3px" />make a payment</a>
					</p>
					<p>
						<a href="<%= CurrentPromoter.UrlApp("invoices") %>" >
						<img src="/gfx/icon-view.png" border="0" align="absmiddle" style="margin-right:3px" width="26" height="21" />view invoices</a>
					</p>
				</div>
			</td>
		</tr>
	</table>
	
	<table cellpadding="0" cellspacing="0">
		<tr>
			<td width="50%" style="padding-right:13px;" valign="top">
				<dsi:h1 ID="H15" runat="server">Tickets</dsi:h1>
				<div class="ContentBorder">
					<p runat="server" id="SellTicketsDisabled">
						You can sell tickets now! To withdraw the proceeds, you have to complete the <a href="<%= CurrentPromoter.UrlApp("plus") %>">plus account application form</a>.
					</p>
					<p runat="server" id="SellTicketsEnabled">
						You can sell tickets and withdraw proceeds.
					</p>
					<p>
						<a href="<%= CurrentPromoter.UrlApp("allticketruns") %>"><img src="/gfx/icon-view.png" border="0" width="26" height="21"
							align="absmiddle" style="margin-right:3px;">view all ticket runs</a>
					</p>
					<p>
						<a href="<%= CurrentPromoter.UrlApp("ticketrun", "ReferringPage", Convert.ToInt32(Spotted.Pages.Promoters.TicketRun.ReferringPageType.Promoter).ToString()) %>"><img src="/gfx/icon-add.png" border="0" width="26" height="21"
							align="absmiddle" style="margin-right:3px;">sell tickets now</a>
					</p>
					<p>
						<a href="<%= CurrentPromoter.UrlApp("doorlist") %>" target="_blank"><img src="/gfx/icon-print.png" border="0" width="26" height="21"
							align="absmiddle" style="margin-right:3px;">print door list</a>
					</p>
					<p runat="server" id="uiConfirmCardDetailsLink">
						<a href="<%= CurrentPromoter.UrlApp("confirmcarddetails") %>"><img src="/gfx/icon-document-tick.png" border="0" width="26" height="21"
							align="absmiddle" style="margin-right:3px;">confirm card details</a>
					</p>
					<p>
						<a href="/article-7105"><img src="/gfx/icon-document-tickets.png" border="0" width="26" height="21"
							align="absmiddle" style="margin-right:3px;">FAQ</a>
					</p>
				</div>
			</td>
			<td width="50%" valign="top">
				<dsi:h1 ID="H15sadf" runat="server">Banners</dsi:h1>
				<div class="ContentBorder">
					<p>
						We have strict rules for animated banners, so you must download these instructions and send them to your designer:	
						<b><a href="/misc/banners.pdf" target="_blank">banner instructions for designers</a></b>
					</p>
					<p>
						<a href="<%= CurrentPromoter.UrlApp("banners") %>"><img src="/gfx/icon-view.png" border="0" width="26" height="21"
							align="absmiddle" style="margin-right:3px;">view banners</a>
					</p>
					<p>
						<a href="<%= CurrentPromoter.UrlApp("banneredit","mode","add") %>"><img src="/gfx/icon-add.png" border="0" width="26" height="21"
							align="absmiddle" style="margin-right:3px;">add a banner</a>
					</p>
					<p>
						<a href="<%= CurrentPromoter.UrlApp("bannerspending") %>"><img src="/gfx/icon-document-tick.png" border="0" width="26" height="21"
							align="absmiddle" style="margin-right:3px;">book pending banners</a>
					</p>
				</div>
			</td>
		</tr>
	</table>
	
	<dsi:h1 ID="H1" runat="server">Printing</dsi:h1>
	<div class="ContentBorder">
		<div class="ClearAfter">
			<p>
				<a href="/popup/flyerclick/k-1350" target="_blank"><img src="/images/flyer/1350.jpg" width="600" height="142" align="right" style="xmargin-left:10px;" /></a>
			</p>
		</div>
		<div class="ClearAfter">
			<p>
				<a href="/popup/flyerclick/k-1350" target="_blank"><img src="/gfx/icon-print.png" border="0" width="26" height="21" align="absmiddle" style="margin-right:3px;">visit outofhand.co.uk</a>
			</p>
		</div>
	</div>
	

	<asp:Panel runat="server" ID="QuickViewPanel">
		<a name="QuickViewPanel"></a>
		<dsi:h1 ID="H14" runat="server">Quick view</dsi:h1>
		<div class="ContentBorder">
		
			<div style="margin-top:20px;margin-bottom:20px;">
				<p>
					<center>
						<span style="font-size:18px;font-weight:bold;">
							<a href="#" onclick="document.getElementById('<%= EventsPanel.ClientID %>').style.display='';document.getElementById('<%= BrandsPanelOuter.ClientID %>').style.display='none';document.getElementById('<%= VenuesPanelOuter.ClientID %>').style.display='none';document.getElementById('<%= DomainsPanelOuter.ClientID %>').style.display='none';return false;">Upcoming events</a> |
							<a href="#" onclick="document.getElementById('<%= EventsPanel.ClientID %>').style.display='none';document.getElementById('<%= BrandsPanelOuter.ClientID %>').style.display='';document.getElementById('<%= VenuesPanelOuter.ClientID %>').style.display='none';document.getElementById('<%= DomainsPanelOuter.ClientID %>').style.display='none';return false;">Parties / groups</a> |
							<a href="#" onclick="document.getElementById('<%= EventsPanel.ClientID %>').style.display='none';document.getElementById('<%= BrandsPanelOuter.ClientID %>').style.display='none';document.getElementById('<%= VenuesPanelOuter.ClientID %>').style.display='';document.getElementById('<%= DomainsPanelOuter.ClientID %>').style.display='none';return false;">Venues</a> |
							<a href="#" onclick="document.getElementById('<%= EventsPanel.ClientID %>').style.display='none';document.getElementById('<%= BrandsPanelOuter.ClientID %>').style.display='none';document.getElementById('<%= VenuesPanelOuter.ClientID %>').style.display='none';document.getElementById('<%= DomainsPanelOuter.ClientID %>').style.display='';return false;">Domains</a>
						</span>
					</center>
				</p>
			</div>

			<asp:Panel ID="EventsPanel" Runat="server">
				<p>
					You have full control over all your events - you can make changes to any events you promote. Here are your upcoming events:
				</p>
				<asp:Panel ID="EventsPanelEventsNoEvents" Runat="server">
					<p>
						You don't have any upcoming events. Click the link below to add one.
					</p>
				</asp:Panel>
				<asp:Panel ID="EventsPanelEvents" Runat="server">
					<p>
						<style>
							.Vert{
								/*writing-mode: tb-rl;filter: flipv fliph;*/
							}
						</style>
						<asp:GridView ID="EventsGridView" Runat="server" AllowPaging="True" 
							AlternatingRowStyle-CssClass="dataGridAltItem" AutoGenerateColumns="False" 
							BorderWidth="0" CellPadding="3" CssClass="dataGrid" GridLines="None" 
							HeaderStyle-CssClass="dataGridHeader" 
							OnPageIndexChanging="EventsGridViewChangePage" PagerSettings-Mode="Numeric" 
							PageSize="10" RowStyle-VerticalAlign="Top" 
							SelectedRowStyle-CssClass="dataGridSelectedItem">
							<columns>
								<asp:templatefield HeaderText="Pic">
									<itemtemplate>
										<%# Bobs.Promoter.PicHtml((Bobs.Event)(Container.DataItem))%>
									</itemtemplate>
								</asp:templatefield>
								<asp:templatefield HeaderText="Name">
									<itemtemplate>
										<a href="<%#CurrentPromoter.UrlEventOptions((Bobs.Event)(Container.DataItem))%>">
										<%#((Bobs.Event)(Container.DataItem)).Name%></a><br/><small>
										<%#((Bobs.Event)(Container.DataItem)).FriendlyDate(true)%></small>
									</itemtemplate>
								</asp:templatefield>
								<asp:templatefield HeaderText="Options">
									<itemtemplate>
										<%#Utilities.Link(CurrentPromoter.UrlEventOptions((Bobs.Event)(Container.DataItem)), Utilities.IconHtml(Utilities.Icon.Edit, "Options", ""))%>
									</itemtemplate>
								</asp:templatefield>
								
								
								<asp:templatefield HeaderText="<b>NEW!<br>Spotter<br>invite</b>">
									<itemtemplate>
										<%#((Bobs.Event)(Container.DataItem)).PromoterHtmlSpotterInvite(CurrentPromoter)%>
									</itemtemplate>
								</asp:templatefield>
								
								<asp:templatefield HeaderText="Tickets">
									<itemtemplate>
										<%#((Bobs.Event)(Container.DataItem)).PromoterHtmlTickets(CurrentPromoter)%>
									</itemtemplate>
								</asp:templatefield>
								<asp:templatefield HeaderText="Banners">
									<itemtemplate>
										<%#((Bobs.Event)(Container.DataItem)).PromoterHtmlBanner(CurrentPromoter)%>
									</itemtemplate>
								</asp:templatefield>
								<asp:templatefield HeaderText="Highlight">
									<itemtemplate>
										<%#((Bobs.Event)(Container.DataItem)).PromoterHtmlEventDonate(CurrentPromoter)%>
									</itemtemplate>
								</asp:templatefield>
								
								
								<asp:templatefield HeaderText="Group<br>news">
									<itemtemplate>
										<%#((Bobs.Event)(Container.DataItem)).PromoterHtmlNews(CurrentPromoter)%>
									</itemtemplate>
								</asp:templatefield>
								<asp:templatefield HeaderText="Article">
									<itemtemplate>
										<%#((Bobs.Event)(Container.DataItem)).PromoterHtmlArticle(CurrentPromoter)%>
									</itemtemplate>
								</asp:templatefield>
								<asp:templatefield HeaderText="Comp-<br>etition">
									<itemtemplate>
										<%#((Bobs.Event)(Container.DataItem)).PromoterHtmlCompetition(CurrentPromoter)%>
									</itemtemplate>
								</asp:templatefield>
							</columns>
						</asp:GridView>
					</p>
				</asp:Panel>
				<p>
					If there's an event on DontStayIn that you promote, but it's not listed 
					here, call our promoter hotline on 0207 835 5599 and we'll add it to your account.
				</p>
				<p>
					<a href="/pages/events/edit"><img align="absmiddle" border="0" src="/gfx/icon-add.png" width="26" height="21" style="margin-right:3px;"/>add an event</a>
				</p>
				<p>
					<a href='<%= CurrentPromoter.UrlApp("events") %>'>
					<img align="absmiddle" border="0" src="/gfx/icon-view.png" width="26" height="21" 
					style="margin-right:3px;"/>view past events</a>
				</p>
			</asp:Panel>
		
			<asp:Panel runat="server" ID="BrandsPanelOuter" style="display:none;">
				<asp:Panel Runat="server" ID="BrandsPanel">
					<p>
						Your promoter account controls the party brands listed below. You can add new brands using 
						the selector on the <a href="<%= CurrentPromoter.UrlApp("edit") %>">account details page</a>.
						Please call our promoter hotline on 0207 835 5599 to enable any unconfirmed brands.
					</p>
					<p>
						<asp:DataGrid Runat="server" ID="BrandDataGrid" 
							GridLines="None" AutoGenerateColumns="False"
							BorderWidth=0 CellPadding=3 CssClass=dataGrid 
							AlternatingItemStyle-CssClass="dataGridAltItem"
							HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" 
							ItemStyle-VerticalAlign="Top" AllowPaging="True" OnPageIndexChanged="BrandDataGridChangePage"
							PageSize="10" PagerStyle-Mode="NumericPages">
							<Columns>
								<asp:TemplateColumn HeaderText="Pic">
									<ItemTemplate>
										<%#Bobs.Promoter.PicHtml((Bobs.Brand)(Container.DataItem))%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Name">
									<ItemTemplate>
										<div style="padding-top:5px;">
											<a href="<%#((Bobs.Brand)Container.DataItem).Url()%>"><%#((Bobs.Brand)Container.DataItem).Name%></a><%#((Bobs.Brand)Container.DataItem).PromoterStatus.Equals(Bobs.Brand.PromoterStatusEnum.Unconfirmed) ? " <b>(<a href=\"" + CurrentPromoter.UrlApp("unconfirmed") + "\">unconfirmed</a>)</b>" : ""%></div>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Name">
									<ItemTemplate>
										<%#BrandAdminHtml("Name",(Bobs.Brand)Container.DataItem)%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Picture">
									<ItemTemplate>
										<%#BrandAdminHtml("Picture",(Bobs.Brand)Container.DataItem)%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Location">
									<ItemTemplate>
										<%#GroupAdminHtml("Location", (Bobs.Brand)Container.DataItem, ((Bobs.Brand)Container.DataItem).Group)%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="<nobr>Music type</nobr>">
									<ItemTemplate>
										<%#GroupAdminHtml("MusicType", (Bobs.Brand)Container.DataItem, ((Bobs.Brand)Container.DataItem).Group)%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Details">
									<ItemTemplate>
										<%#GroupAdminHtml("Details", (Bobs.Brand)Container.DataItem, ((Bobs.Brand)Container.DataItem).Group)%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Styled" ItemStyle-HorizontalAlign="Center">
									<ItemTemplate>
										<div style="padding-top:5px;"><small><%#Utilities.Link(((Bobs.Brand)Container.DataItem).UrlStyledSetup(), "[Edit]")%></small></div>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:DataGrid>
					</p>
				</asp:Panel>
				<asp:Panel Runat="server" ID="NoBrandsPanel">
					<p>
						You don't have any party brands attached to your promoter account. You can add new brands using 
						the selector on the <a href="<%= CurrentPromoter.UrlApp("edit") %>">account details page</a>. If 
						you have trouble adding brands to your account, call us on 0207 835 5599.
					</p>
				</asp:Panel>
			</asp:Panel>
			
			<asp:Panel Runat="server" ID="VenuesPanelOuter" style="display:none;">	
				<asp:Panel Runat="server" ID="VenuesPanel">
					<p>
						Your promoter account controls the venues listed below. You can add new venues using 
						the selector on the <a href="<%= CurrentPromoter.UrlApp("edit") %>">account details page</a>. 
						You should update the details to keep them current. Please do not include descriptions 
						of upcoming events in the venue description - it should just describe the venue!
					</p>
					<p>
						<asp:DataGrid Runat="server" ID="VenueDataGrid" 
							GridLines="None" AutoGenerateColumns="False"
							BorderWidth=0 CellPadding=3 CssClass=dataGrid 
							AlternatingItemStyle-CssClass="dataGridAltItem"
							HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" 
							ItemStyle-VerticalAlign="Top" AllowPaging="True" OnPageIndexChanged="VenueDataGridChangePage"
							PageSize="10" PagerStyle-Mode="NumericPages">
							<Columns>
								<asp:TemplateColumn HeaderText="Pic">
									<ItemTemplate>
										<%#Bobs.Promoter.PicHtml((Bobs.Venue)(Container.DataItem))%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Name">
									<ItemTemplate>
										<div style="padding-top:5px;">
											<a href="<%#((Bobs.Venue)Container.DataItem).Url()%>"><%#((Bobs.Venue)Container.DataItem).Name%></a><%#((Bobs.Venue)Container.DataItem).PromoterStatus.Equals(Bobs.Venue.PromoterStatusEnum.Unconfirmed) ? " <b>(<a href=\"" + CurrentPromoter.UrlApp("unconfirmed") + "\">unconfirmed</a>)</b>" : ""%></div>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Events">
									<ItemTemplate>
										<div style="padding-top:5px;"><a href="/pages/events/edit/venuek-<%#((Bobs.Venue)(Container.DataItem)).K%>/promoterk-<%= CurrentPromoter.K %>">add&nbsp;an&nbsp;event</a></div>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Picture">
									<ItemTemplate>
										<a href="<%#((Bobs.Venue)(Container.DataItem)).UrlApp("edit","page","pic")%>"><img src="<%#((Bobs.Venue)(Container.DataItem)).HasPic?"/gfx/icon-tick.png":"/gfx/icon-cross.png"%>" border="0" height="21" width="26" align="absmiddle">change</a>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Details">
									<ItemTemplate>
										<a href="<%#((Bobs.Venue)(Container.DataItem)).UrlApp("edit")%>"><img src="<%#((Bobs.Venue)(Container.DataItem)).DetailsHtml.Length>0?"/gfx/icon-tick.png":"/gfx/icon-cross.png"%>" border="0" height="21" width="26" align="absmiddle">change</a>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Styled" ItemStyle-HorizontalAlign="Center">
									<ItemTemplate>
										<div style="padding-top:5px;"><small><%#Utilities.Link(((Bobs.Venue)Container.DataItem).UrlStyledSetup(), "[Edit]")%></small></div>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:DataGrid>
					</p>
				</asp:Panel>
				<asp:Panel Runat="server" ID="NoVenuesPanel">
					<p>
						You don't have any venues attached to your promoter account. You can add new venues using 
						the selector on the <a href="<%= CurrentPromoter.UrlApp("edit") %>">account details page</a>. 
						If you have trouble adding venues to your account, call us on 0207 835 5599.
					</p>
				</asp:Panel>
			</asp:Panel>
			
			<asp:Panel Runat="server" ID="DomainsPanelOuter" style="display:none;">	
				<asp:Panel Runat="server" ID="DomainsPanel">
					<p>
						We've registered the domains below for you. If you would like to register a new domain, or 
						change the page that the domain leads to, give us a ring on 0207 835 5599.
					</p>
					<p>
						<asp:DataGrid Runat="server" ID="DomainsDataGrid" 
							GridLines="None" AutoGenerateColumns="False"
							BorderWidth=0 CellPadding=3 CssClass=dataGrid 
							AlternatingItemStyle-CssClass="dataGridAltItem"
							HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" 
							ItemStyle-VerticalAlign="Top" AllowPaging="True" OnPageIndexChanged="DomainsDataGridChangePage"
							PageSize="10" PagerStyle-Mode="NumericPages">
							<Columns>
								<asp:TemplateColumn HeaderText="Name">
									<ItemTemplate>
										<a href="http://www.<%#((Bobs.Domain)Container.DataItem).DomainName%>" target="_blank"><%#((Bobs.Domain)Container.DataItem).DomainName%></a>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Links to...">
									<ItemTemplate>
										<%#((Bobs.Domain)Container.DataItem).RedirectUrlComplete.Replace("http://","")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Total<br>visitors">
									<ItemTemplate>
										<%#hitsTotal((Bobs.Domain)Container.DataItem)%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Visitors in<br>the last<br>month">
									<ItemTemplate>
										<%#hitsMonth((Bobs.Domain)Container.DataItem)%>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:DataGrid>
					</p>
				</asp:Panel>
				<asp:Panel Runat="server" ID="NoDomainsPanel">
					<p>
						We've not registered any for you. If you would like to register a new domain to point 
						at any of your DontStayIn pages, give us a ring on 0207 835 5599.
					</p>
				</asp:Panel>
			</asp:Panel>
			
		</div>
	</asp:Panel>
		
	<asp:Panel ID="MoreOptionsPanel" runat="server">
		<a name="MoreOptions"/>
		<dsi:h1 ID="H15sd" runat="server">
			More options</dsi:h1>
		<div class="ContentBorder">
		
			<asp:Panel Runat="server" ID="GuestlistPanel">
				<h2>
					Guestlist
				</h2>
				<p>
					The guestlist system is being phased out. You can still view existing guestlists, but can't add new ones.
				</p>
				<p>
					<a href="<%= CurrentPromoter.UrlApp("guestlists") %>"><img src="/gfx/icon-view.png" width="26" height="21" border="0" 
						align="absmiddle" style="margin-right:3px;">view guestlists</a>
				</p>
				<p>&nbsp;</p>
			</asp:Panel>
			
			
			<h2>
				Competitions
			</h2>
			<p>
				You can add competitions that our members can enter.
			</p>
			<p>
				<a href="<%= CurrentPromoter.UrlApp("competitions") %>"><img src="/gfx/icon-view.png" width="26" height="21" border="0" 
					align="absmiddle" style="margin-right:3px;">view competitions</a>
			</p>
			<p>
				<a href="<%= CurrentPromoter.UrlApp("competitions","mode","add") %>"><img src="/gfx/icon-add.png" width="26" height="21" border="0" 
					align="absmiddle" style="margin-right:3px;">add a competition</a>
			</p>
			<p>&nbsp;</p>
			
			<h2>
				Articles
			</h2>
			<p>
				You can add articles with text and pictures.
			</p>
			<p>
				<a href="<%= CurrentPromoter.UrlApp("articles") %>"><img src="/gfx/icon-view.png" width="26" height="21" border="0" 
					align="absmiddle" style="margin-right:3px;">view articles</a>
			</p>
			<p>
				<a href="/pages/myarticles/mode-add"><img src="/gfx/icon-add.png" border="0"  width="26" height="21"
					align="absmiddle" style="margin-right:3px;">add an article</a>
			</p>
			
			<p>&nbsp;</p>

			<h2>
				Files
			</h2>
			<p>
				You can upload gif or jpg files for linking on the DontStayIn site - for example 
				you could upload graphics files to customise your event pages.
			</p>
			<p>
				<a href="<%= CurrentPromoter.UrlApp("files") %>"><img src="/gfx/icon-view.png" width="26" height="21" border="0" 
					align="absmiddle" style="margin-right:3px;"/>view files</a>
			</p>
			<p>
				<a href="<%= CurrentPromoter.UrlApp("files","mode","upload") %>"><img src="/gfx/icon-add.png" width="26" height="21" border="0" 
					align="absmiddle" style="margin-right:3px;"/>add files</a>
			</p>

		</div>
	</asp:Panel>
	
</asp:Panel>

