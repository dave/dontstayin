<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TicketFundsRelease.ascx.cs" Inherits="Spotted.Admin.TicketFundsRelease" %>
<%@ Register TagPrefix="DbCombo" Namespace="Cambro.Web.DbCombo" Assembly="Cambro.Web.DbCombo" %>
<asp:Panel Runat="server" ID="AllTicketRunsPanel">
	<div class="ContentBorder">
		<p style="display:none;">
			<asp:LinkButton ID="SelectAwaitingFundsReleaseLinkButton" runat="server">Awaiting funds release</asp:LinkButton> | 
			<asp:LinkButton ID="SelectLockedFundsLinkButton" runat="server">Locked funds</asp:LinkButton> | 
			<asp:LinkButton ID="SelectAllTicketFundsLinkButton" runat="server">All ticket funds</asp:LinkButton>
		</p>
		<table ID="SearchTable" runat="server" cellpadding="5" cellspacing="0" border="0">
			<tr>
				<td><nobr>Promoter</nobr></td>
				<td><js:HtmlAutoComplete Width="140px" ID="uiPromotersAutoComplete" runat="server"  WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetPromotersWithK"/></td>
				<td><nobr>Event date from</nobr></td>
				<td><dsi:Cal id="FromDateCal" runat="server" TabIndex="30"></dsi:Cal></td>
				<td colspan="2"><asp:CheckBox ID="ShowZeroMoneyTicketRunsCheckBox" runat="server" Text="Show £0 earnt ticket runs" /></td>
			</tr>
			<tr>
				<td><nobr>Sales person</nobr></td>
				<td><asp:DropDownList ID="SalesUsrDropDownList" runat="server" TabIndex="10"></asp:DropDownList></td>
				<td><nobr>Event date to</nobr></td>
				<td><dsi:Cal id="ToDateCal" runat="server" TabIndex="30"></dsi:Cal></td>
				<td><nobr>Status</nobr></td>
				<td><asp:DropDownList ID="StatusDropDownList" runat="server" TabIndex="10"></asp:DropDownList></td>
				<td><asp:Button ID="SearchButton" runat="server" Text="Search" Width="80px" OnClick="SearchButton_Click" TabIndex="30"/></td>
				<td><asp:Button ID="ClearButton" runat="server" Text="Clear" Width="80px" OnClick="ClearButton_Click" TabIndex="30"/></td>
			</tr>
		</table>
		<table ID="SalesPersonQuickButtonTable" runat="server" cellpadding="3" cellspacing="0" border="0">
			<tr style="font-weight:bold;">
				<td><asp:RadioButton ID="MyPromotersRadioButton" runat="server" Text="My promoters" GroupName="SalesUsrRadioButtonGroup" AutoPostBack="true" OnCheckedChanged="MyPromotersRadioButton_CheckedChanged" /></td>
				<td><asp:RadioButton ID="MyPromotersWithFundsNotSpentRadioButton" runat="server" Text="My promoters with released funds not spent" GroupName="SalesUsrRadioButtonGroup" AutoPostBack="true" OnCheckedChanged="MyPromotersWithFundsNotSpentRadioButton_CheckedChanged" /></td>
				<td><asp:RadioButton ID="MyPromotersWithFundsNotReleasedRadioButton" runat="server" Text="My promoters with funds waiting to be released" GroupName="SalesUsrRadioButtonGroup" AutoPostBack="true" OnCheckedChanged="MyPromotersWithFundsNotReleasedRadioButton_CheckedChanged" /></td>
				<td><asp:RadioButton ID="MyPromotersWithFundsSoonToBeReleasedRadioButton" runat="server" Text="My promoters with a ticket event soon to end" GroupName="SalesUsrRadioButtonGroup" AutoPostBack="true" OnCheckedChanged="MyPromotersWithFundsSoonToBeReleasedRadioButton_CheckedChanged" /></td>
			</tr>
		</table>		
		<asp:GridView ID="TicketPromoterEventFundsGridView" runat="server" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="TicketPromoterEventFundsGridView_PageIndexChanging"
			OnRowCommand="TicketPromoterEventFundsGridView_RowCommand" CssClass="dataGrid" EnableViewState="true" EmptyDataText="No results. Please redefine your search."
			AlternatingRowStyle-CssClass="dataGridAltItem" GridLines="None" BorderWidth="0" CellPadding="3" HeaderStyle-CssClass="dataGridHeader"
			SelectedRowStyle-CssClass="dataGridSelectedItem" AlternatingRowStyle-VerticalAlign="Top" RowStyle-VerticalAlign="Top">
			<Columns>
				<asp:TemplateField HeaderText="Promoter">
					<ItemTemplate><asp:TextBox ID="PromoterKTextBox" runat="server" Text="<%#((Bobs.TicketPromoterEvent)(Container.DataItem)).PromoterK%>" Visible="false"></asp:TextBox>
						<%#((Bobs.TicketPromoterEvent)(Container.DataItem)).Promoter.LinkNewWindow()%>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Event">
					<ItemTemplate><asp:TextBox ID="EventKTextBox" runat="server" Text="<%#((Bobs.TicketPromoterEvent)(Container.DataItem)).EventK%>" Visible="false"></asp:TextBox>
						<%#((Bobs.TicketPromoterEvent)(Container.DataItem)).Event.LinkShort(80)%>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Event date">
					<ItemTemplate>
						<%#((Bobs.TicketPromoterEvent)(Container.DataItem)).Event.FriendlyDate(true)%>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Ticket runs" ItemStyle-HorizontalAlign="Right">
					<ItemTemplate>
						<%#((Bobs.TicketPromoterEvent)(Container.DataItem)).Event.TicketRuns.Count.ToString()%>
					</ItemTemplate>
				</asp:TemplateField>				
				<asp:TemplateField HeaderText="Tickets sold" ItemStyle-HorizontalAlign="Right">
					<ItemTemplate>
						<%#((Bobs.TicketPromoterEvent)(Container.DataItem)).SoldTickets%>
						<%#((Bobs.TicketPromoterEvent)(Container.DataItem)).CancelledTickets > 0 ? "(" + ((Bobs.TicketPromoterEvent)(Container.DataItem)).CancelledTickets.ToString() + ")" : "" %>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Total funds (STORED)" ItemStyle-HorizontalAlign="Right">
					<ItemTemplate>
						<b><%#((Bobs.TicketPromoterEvent)(Container.DataItem)).TotalFunds.ToString("c")%></b>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Total funds (CALCULATED)" ItemStyle-HorizontalAlign="Right">
					<ItemTemplate>
						<b><%#((Bobs.TicketPromoterEvent)(Container.DataItem)).GetTotalFunds().ToString("c")%></b>
					</ItemTemplate>
				</asp:TemplateField>
				
				<asp:TemplateField HeaderText="Event<br>ended" ItemStyle-HorizontalAlign="Center">
					<ItemTemplate>
						<%#Utilities.TickCrossHtml(((Bobs.TicketPromoterEvent)(Container.DataItem)).Event.DateTime < DateTime.Today)%>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Edit /<br>View" ItemStyle-HorizontalAlign="Center">
					<ItemTemplate>
						<asp:LinkButton ID="EditTicketPromoterEventLinkButton" runat="server" CommandName="EditTicketPromoterEvent"><%# ((Bobs.TicketPromoterEvent)(Container.DataItem)).FundsReleased ? Utilities.IconHtml(Utilities.Icon.View) : Utilities.IconHtml(Utilities.Icon.Edit)%></asp:LinkButton>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Funds<br>released" ItemStyle-HorizontalAlign="Center">
					<ItemTemplate>
						<%#Utilities.TickCrossHtml(((Bobs.TicketPromoterEvent)(Container.DataItem)).FundsReleased)%>
					</ItemTemplate>
				</asp:TemplateField>
			</Columns>	
		</asp:GridView>
		<asp:Label ID="SearchResultsMessageLabel" runat="server" Font-Italic="True" Visible="false"></asp:Label>
		<p id="TotalsP" runat="server" visible="false">
			<table style="font-weight:bold;">
				<tr>
					<td>Total funds:</td>
					<td align="right"><asp:Label ID="TotalFundsLabel" runat="server"></asp:Label></td>
				</tr>
				<tr>
					<td>Total booking fees:</td>
					<td align="right"><asp:Label ID="TotalBookingFeesLabel" runat="server"></asp:Label></td>
				</tr>
				<tr>
					<td>Total tickets:</td>
					<td align="right"><asp:Label ID="TotalTicketsLabel" runat="server"></asp:Label></td>
				</tr>
			</table>
		</p>
		<table width="100%" style="display:none;">
			<tr>
				<td align="left">
					<asp:Panel ID="PaginationPanel" runat="server">
						<asp:LinkButton ID="PrevPageLinkButton" runat="server" ><img src="/gfx/icon-back-12.png" alt="Prev page" style="margin-right:3px;" width="12" height="21" align="absmiddle" border="0">prev page</asp:LinkButton> ...
						<asp:LinkButton ID="NextPageLinkButton" runat="server">next page<img src="/gfx/icon-forward-12.png" alt="Next page" style="margin-left:3px;" width="12" height="21" align="absmiddle" border="0"></asp:LinkButton>
					</asp:Panel>
				</td>
			</tr>
		</table>
	</div>
	<asp:Panel ID="TicketPromoterEventDetailsPanel" runat="server" Visible="false">
		<dsi:h1 runat="server" ID="H1EditTitle">Edit funds release</dsi:h1>
		<div class="ContentBorder">			
			<table cellpadding="3" cellspacing="0" border="0">
				<tr valign="top">
					<td width="100">Event</td>
					<td width="250"><asp:Label ID="EventLabel" runat="server"></asp:Label></td>
					<td width="150" colspan="2" rowspan="5" align="left" valign="top">
						<table cellpadding="3" cellspacing="0" border="0"> 
							<tr>
								<td><nobr>Promoter plus enabled</nobr></td>
								<td><asp:Label ID="PromoterTicketsEnabledLabel" runat="server"></asp:Label></td>
							</tr>
							<tr>
								<td><nobr>Event ended</nobr></td>
								<td><asp:Label ID="EventEndedLabel" runat="server"></asp:Label></td>
							</tr>
							<tr>
								<td><nobr>Valid user responses</nobr></td>
								<td><asp:Label ID="ValidUserResponsesLabel" runat="server"></asp:Label></td>
							</tr>
							<tr>
								<td><nobr>Valid IPs</nobr></td>
								<td><asp:Label ID="ValidIPDuplicateLabel" runat="server"></asp:Label></td>
							</tr>
							<tr>
								<td><nobr>Valid IP countries</nobr></td>
								<td><asp:Label ID="ValidIPCountryLabel" runat="server"></asp:Label></td>
							</tr>
							<tr>
								<td><nobr>Valid browser Guids</nobr></td>
								<td><asp:Label ID="ValidBrowserGuidsLabel" runat="server"></asp:Label></td>
							</tr>
						</table>				
					</td>
				</tr>
				<tr valign="top">
					<td>Promoter</td>
					<td><asp:Label ID="PromoterLabel" runat="server"></asp:Label></td>
				</tr>
				<tr valign="top">
					<td>Tickets sold</td>
					<td><asp:Label ID="TicketRunsLabel" runat="server"></asp:Label></td>
				</tr>
				<tr>
					<td><nobr>Total funds</nobr></td>
					<td><asp:Label ID="AmountLabel" runat="server" Font-Bold="true"></asp:Label></td>
				</tr>
				<tr>
					<td><nobr>Total VAT</nobr></td>
					<td><asp:Label ID="TotalVatLabel" runat="server" Font-Bold="true"></asp:Label></td>
				</tr>
				<tr>
					<td><nobr>Total booking fees</nobr></td>
					<td><asp:Label ID="BookingFeesLabel" runat="server" Font-Bold="true"></asp:Label></td>
				</tr>
				<tr id="LockTextRow" runat="server" valign="top" visible="false">
					<td>Lock text</td>
					<td colspan="3"><asp:Label ID="LockTextLabel" runat="server"></asp:Label></td>
				</tr>
				<tr id="ManualLockRow" runat="server" visible="false">
					<td>Manual lock</td>
					<td colspan="3"><asp:Label ID="ManualLockLabel" runat="server"></asp:Label><div id="ManualLockNotSetDiv" runat="server"><asp:TextBox ID="ManualLockNoteTextBox" Width="300" runat="server"></asp:TextBox>
						<asp:RequiredFieldValidator ID="ManualLockRequiredFieldValidator" runat="server" ValidationGroup="ManualLockValidationGroup" Display="Dynamic" ErrorMessage="* must enter" ControlToValidate="ManualLockNoteTextBox"></asp:RequiredFieldValidator>
						<asp:Button ID="ManualLockButton" runat="server" Width="110" OnClick="ManualLockButton_Click" OnClientClick="return confirm('Are you sure you want to manually lock these funds?')" Text="Manual lock" ValidationGroup="ManualLockValidationGroup" /></div></td>
				</tr>
				<tr id="OverrideLockRow" runat="server" visible="false">
					<td>Override lock</td>
					<td colspan="3"><asp:Label ID="OverrideLockLabel" runat="server"></asp:Label><div id="OverrideLockNotSetDiv" runat="server"><asp:TextBox ID="OverrideLockNoteTextBox" Width="300" runat="server"></asp:TextBox>
						<asp:RequiredFieldValidator ID="OverrideTextRequiredFieldValidator" runat="server" ValidationGroup="ManualOverrideValidationGroup" Display="Dynamic" ErrorMessage="* must enter" ControlToValidate="OverrideLockNoteTextBox"></asp:RequiredFieldValidator>
						<asp:Button ID="OverrideLockButton" runat="server" Width="110" OnClick="OverrideLockButton_Click" OnClientClick="return confirm('Are you sure you want to override the lock and make funds available?')" Text="Manual override" ValidationGroup="ManualOverrideValidationGroup" /></div></td>
				</tr>
				<tr id="RerunFundsLocksChecksRow" valign="top" runat="server">
					<td>&nbsp;</td>
					<td colspan="3"><asp:Button ID="RerunFundsLocksChecksButton" runat="server" OnClick="RerunFundsLocksChecksButton_Click" Text="Re-run funds locks checks" CausesValidation="false"/></td>
				</tr>
				<tr id="FundsReleasedRow" runat="server" visible="false">
					<td>Ticket funds</td>
					<td colspan="3"><asp:Label ID="FundsReleasedLabel" runat="server"></asp:Label>
						<asp:Label ID="ReleaseTransferLabel" runat="server"></asp:Label></td>
				</tr>
				<tr id="TicketFundsAppliedRow" runat="server" visible="false">
					<td>Ticket funds applied</td>
					<td colspan="3"><asp:Label ID="InvoicesAppliedToLabel" runat="server"></asp:Label></td>
				</tr>
				<tr id="TicketPaymentRow" runat="server" visible="false">
					<td>Ticket payment</td>
					<td colspan="3"><asp:Label ID="TicketPaymentMadeLabel" runat="server"></asp:Label>
						<asp:Label ID="TicketPaymentTransferLabel" runat="server"></asp:Label></td>
				</tr>
				<tr id="PayFundsToPromoterBankAccountRow" runat="server" visible="false">
					<td colspan="4"><button id="PayFundsToPromoterBankAccountButton" runat="server" onserverclick="PayFundsToPromoterBankAccountButton_Click" causesvalidation="false">Pay funds to promoter bank account</button>
						<button id="CreateCampaignCreditsButton" runat="server" onserverclick="CreateCampaignCreditsButton_Click" causesvalidation="false">Create campaign credits</button></td>
				</tr>
			</table>
			<asp:CustomValidator ID="ProcessingVal" Runat="server" Display="None" EnableClientScript="False" ErrorMessage="Error processing. Please try again."/>
			<asp:ValidationSummary ID="TicketFundsReleaseValidationSummary" BorderWidth="2" Runat="server" EnableClientScript="False" ShowSummary="True" HeaderText="&nbsp;There were some errors:" CssClass="PaymentValidationSummary" Font-Bold="True" DisplayMode="BulletList"/>	
		</div>
	</asp:Panel>
	<p>
		<asp:Button ID="GetLatestEventsWithTicketsButton" runat="server" Visible="false" OnClick="GetLatestEventsWithTicketsButton_Click" Text="Get latest events with tickets that recently ended" onmouseover="stt('Click here if an event with tickets recently ended but is not showing.');" onmouseout="htm();" />
	</p>
</asp:Panel>
<asp:Label ID="ReleaseFundsOptionsJavascriptLabel" runat="server" Visible="false"></asp:Label>