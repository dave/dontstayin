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
