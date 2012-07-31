<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TicketRun.ascx.cs" Inherits="Spotted.Pages.Promoters.TicketRun" %>

<%@ Register TagPrefix="DSIControls" TagName="TimeControl" Src="/Controls/TimeControl.ascx" %>
<script language="JavaScript">
  function PromoterTicketRunScreenToggleAdvancedOptions()
  {
	var	tbl = document.getElementById("<%= TicketRunTable.ClientID  %>");
	var advOptionsCheckBox = document.getElementById("<%= AdvancedOptionsCheckBox.ClientID  %>");
	if(tbl != null && advOptionsCheckBox != null)
	{
		var len = tbl.rows.length;
		var hide = !advOptionsCheckBox.checked;
		var vStyle = (hide) ? "none":"";

		for(i=0 ; i < len; i++)
		{
			if(tbl.rows[i].name != null && tbl.rows[i].name.indexOf("AdvancedOptionRow") >= 0)
				tbl.rows[i].style.display = vStyle;
		}
	}
  }
  function PromoterTicketRunScreenToggleAdminOverrideBookingFee()
  {
	var overrideBookingFeeCheckBox = document.getElementById("<%= OverrideBookingFeeCheckBox.ClientID  %>");
	if(overrideBookingFeeCheckBox != null)
	{
		document.getElementById("<%= BookingFeeTextBox.ClientID  %>").style.display = overrideBookingFeeCheckBox.checked ? "" : "none"; 
	}
  }
</script>
<dsi:PromoterIntro runat="server" ID="PromoterIntro" Header="Sell tickets">
	<asp:Panel Runat="server" ID="NoEventsPanel" Visible="false">
		<h2>You have no events</h2>
		<p>
			Click "add an event", fill out all the details and add your promoter account or brand to the event. After the event is saved you can come back here to sell tickets for that event.
		</p>
		<p>
			<a href="/pages/events/edit"><img src="/gfx/icon-add.png" border="0"  width="26" height="21"
					align="absmiddle" style="margin-right:3px;">add an event</a>
		</p>
	</asp:Panel>
	<asp:Panel Runat="server" ID="HasEventsPanel">
		<p>
			<a href="<%= CurrentPromoter.UrlApp("allticketruns") %>"><img src="/gfx/icon-view.png" width="26" height="21" border="0" 
				align="absmiddle" style="margin-right:3px;">view all ticket runs</a>
		</p>
		<p id="ErrorMessageP" runat="server" visible="false" style="color:Red; font-weight:bold;">
			<br />
			<%= NO_TICKET_SALES_FOR_NONUK_PROMOTERS %>
		</p>
	</asp:Panel>
</dsi:PromoterIntro>

<asp:Panel Runat="server" ID="AddEditTicketRunPanel">
	<dsi:h1 runat="server" ID="AddEditTicketRunH1">Add a ticket run</dsi:h1>
	<div class="ContentBorder">
		<p>
			<asp:Label ID="TicketRunDefaultNoteLabel" runat="server" Visible="false" Text="<p>We've chosen some defaults based on your event - please check them. If you have any changes, just click &quot;Advanced options&quot;. If you're happy with the details, click &quot;Save ticket run&quot;.</p>"></asp:Label><asp:Label ID="TicketRunNote" runat="server" style="padding-left:3px;" Font-Bold="true"></asp:Label>
			<table id="TicketRunTable" runat="server" cellpadding="3" cellspacing="0" border="0" width="550">
				<tr valign="top" name="EventRow">
					<td width="130"><nobr><small>Event</small></nobr></td>
					<td width="420"><nobr><asp:Label ID="EventLabel" runat="server" Text="" EnableViewState="true"></asp:Label><div runat="server" id="NonEventSpecificDiv"><asp:DropDownList ID="EventDropDownList" runat="server">
						</asp:DropDownList> <a href="/pages/events/edit">add an event</a></div></nobr></td>
				</tr>
				<tr valign="top" name="TicketPriceRow">
					<td><nobr><small>Ticket price (£)</small></nobr></td>
					<td><nobr><asp:TextBox ID="TicketPriceTextBox" runat="server" Width="70" MaxLength="7"></asp:TextBox><asp:Label ID="PriceAndBookingFeeLabel" runat="server" Text=""></asp:Label>
						<asp:RequiredFieldValidator ID="TicketPriceRequiredFieldValidator" runat="server" ErrorMessage="* must enter price" Display="Dynamic" ControlToValidate="TicketPriceTextBox" ValidationGroup="SaveTicketRun"></asp:RequiredFieldValidator></nobr></td>
				</tr>
				<tr valign="top" name="TicketBookingFeeRow" id="TicketBookingFeeRow" runat="server" visible="false">
					<td><nobr><small>Booking fee (£)</small></nobr></td>
					<td><nobr><asp:TextBox ID="BookingFeeTextBox" runat="server" Width="70" MaxLength="7" style="display:none;"></asp:TextBox><asp:CheckBox ID="OverrideBookingFeeCheckBox" runat="server" Text="Override" onselect="PromoterTicketRunScreenToggleAdminOverrideBookingFee();" onclick="PromoterTicketRunScreenToggleAdminOverrideBookingFee();"/><asp:Label ID="BookingFeeLabel" runat="server" Text=""></asp:Label> <small>(Admin only option)</small></nobr></td>
				</tr>
				<tr valign="top" id="AdvOptionsCheckRow" name="AdvOptionsCheckRow" runat="server" visible="false">
					<td><asp:CheckBox ID="AdvancedOptionsCheckBox" runat="server" Checked="false" Text="<nobr>Advanced options</nobr>" onselect="PromoterTicketRunScreenToggleAdvancedOptions();" onclick="PromoterTicketRunScreenToggleAdvancedOptions();" /></td>
					<td><img src="/gfx/spacer.gif" width="420" height="1" /></td>
				</tr>
				<tr valign="top" id="TicketNameRow" runat="server" name="AdvancedOptionRow1" visible="false">
					<td><nobr><small>Ticket name</small></nobr></td>
					<td><asp:TextBox ID="TicketNameTextBox" runat="server" MaxLength="30" Width="310"></asp:TextBox><asp:Label ID="TicketNameHelperLabel" runat="server" Text="<br><small>This should be used to differentiate between ticket runs. E.g. 'Early Bird tickets', 'Standard tickets', 'VIP tickets', etc. Keep this short - one or two words. This name is optional.</small>"></asp:Label></td>
				</tr>
				<tr valign="top" id="TicketDescriptionRow" runat="server" name="AdvancedOptionRow2" visible="false">
					<td><nobr><small>Description</small></nobr></td>
					<td><asp:TextBox ID="TicketDescriptionTextBox" runat="server" Width="400" TextMode="MultiLine"></asp:TextBox>
						<asp:Label ID="TicketDescriptionHelperLabel" runat="server" Text="<br><small>This should explain the ticket run. If it's for VIP tickets, explain the benefits. If it's for early bird tickets, explain how many will be sold or what dates they run out on. This description is optional.</small>"></asp:Label>
						<asp:Label ID="TicketDescriptionLabel" runat="server" Text=""></asp:Label></td>
				</tr>
				<tr valign="top" id="BrandsRow" name="AdvancedOptionRow3" runat="server" visible="false">
					<td><nobr><small>Brand</small></nobr></td>
					<td><asp:DropDownList ID="BrandDropDownList" runat="server"></asp:DropDownList><asp:Label ID="BrandLabel" runat="server" Text=""></asp:Label></td>
				</tr>
				<tr valign="top" id="FollowsTicketRunRow" name="AdvancedOptionRow4" runat="server" visible="false">
					<td><nobr><small>Follows ticket run</small></nobr></td>
					<td><asp:DropDownList ID="FollowsTicketRunDropDownList" runat="server"></asp:DropDownList>
						<asp:Label ID="FollowsTicketRunHelperLabel" runat="server" Text="<br><small>This ticket run will not start before the ticket run it follows has ended or sold out. For example, Regular tickets would follow Early Bird tickets. This is optional.</small>"></asp:Label>
						<asp:Label ID="FollowsTicketRunLabel" runat="server" Text=""></asp:Label></td>
				</tr>
				<tr valign="top" id="StartDateRow" runat="server" name="AdvancedOptionRow5">
					<td><nobr><small>Starts</small></nobr></td>
					<td><table cellpadding="0" cellspacing="0" border="0" id="StartTicketRunTable" runat="server">
							<tr>
								<td><dsi:Cal runat="server" ID="StartTicketRunCal" /></td>
								<td><DSIControls:TimeControl runat="server" ID="StartTicketRunTime" /></td>
							</tr>
						</table><asp:Label ID="StartTicketRunLabel" runat="server" Text=""></asp:Label>
					</td>
				</tr>
				<tr valign="top" id="EndDateRow" runat="server" name="AdvancedOptionRow6">
					<td><nobr><small>Ends</small></nobr></td>
					<td><table cellpadding="0" cellspacing="0" border="0" id="EndTicketRunTable" runat="server">
							<tr>
								<td><dsi:Cal runat="server" ID="EndTicketRunCal" /></td>
								<td><DSIControls:TimeControl runat="server" ID="EndTicketRunTime" /></td>
							</tr>
						</table><asp:Label ID="EndTicketRunLabel" runat="server" Text=""></asp:Label>
					</td>
				</tr>
				<tr valign="top" id="TicketsSoldRow" name="AdvancedOptionRow7" runat="server" visible="false">
					<td><nobr><small>Tickets sold already</small></nobr></td>
					<td><nobr><asp:Label ID="TicketsSoldLabel" runat="server" Text="0"></asp:Label></nobr></td>
				</tr>
				<tr valign="top" id="MaxTicketsRow" runat="server" name="AdvancedOptionRow8">
					<td><nobr><small>Max tickets to sell</small></nobr></td>
					<td><asp:TextBox ID="MaxTicketsTextBox" runat="server" Width="70" MaxLength="6"></asp:TextBox>
						<asp:RangeValidator ID="MaxTicketsRangeValidator" runat="server" ErrorMessage="* invalid number" ControlToValidate="MaxTicketsTextBox" MinimumValue="1" MaximumValue="750000" Type="Integer" Display="Dynamic" ValidationGroup="SaveTicketRun"></asp:RangeValidator></td>
				</tr>
				<tr valign="top" id="ContactEmailRow" runat="server">
					<td><nobr><small>Contact email</small></nobr></td>
					<td><asp:TextBox ID="ContactEmailTextBox" runat="server" Width="310"></asp:TextBox>
						<asp:RequiredFieldValidator id="ContactEmailRequiredFieldValidator" Runat="server" ControlToValidate="ContactEmailTextBox" ErrorMessage="* invalid email" Display="Dynamic" ValidationGroup="SaveTicketRun"></asp:RequiredFieldValidator>
						<asp:RegularExpressionValidator id="ContactEmailRegularExpressionValidator" Runat="server" ControlToValidate="ContactEmailTextBox"	ErrorMessage="* invalid email" Display="Dynamic" ValidationGroup="SaveTicketRun"></asp:RegularExpressionValidator>
					</td>	
				</tr>
				<tr valign="top" id="OrderInTheListRow" runat="server" name="AdvancedOptionRow9" visible="false">
					<td><nobr><small>Order in the list</small></nobr></td>
					<td><asp:TextBox ID="OrderInTheListTextBox" runat="server" Width="70" Text="1.00"></asp:TextBox>
						<asp:Label ID="OrderInTheListHelperLabel" runat="server" Text="<small>(Admin only option)<br>Numeric value that determines where this ticket run will be shown in the list of ticket runs for this event. Default = 1.00.</small>"></asp:Label>
						<asp:RangeValidator ID="OrderInTheListRangeValidator" runat="server" ErrorMessage="* invalid number" ControlToValidate="OrderInTheListTextBox" MinimumValue="0" MaximumValue="100" Type="Double" Display="Dynamic" ValidationGroup="SaveTicketRun"></asp:RangeValidator></td>
				</tr>
				<tr valign="top" id="UpdateButtonsRow" runat="server" name="UpdateButtonsRow">
					<td colspan="2"><asp:Button ID="GoToConfirmationButton" runat="server" Text="Next ->" ValidationGroup="SaveTicketRun" OnClick="GoToConfirmationButton_Click" style="padding-right:10px;"/>
						<asp:Button ID="PauseResumeButton" runat="server" Text="Pause ticket run" OnClick="PauseResumeButton_Click" Visible="false"  style="padding-right:10px;"/>
						<asp:Button ID="StopButton" runat="server" Text="Stop ticket run" OnClick="StopButton_Click" OnClientClick="return confirm('Are you sure you want to stop this ticket run? Once its stopped it cannot be restarted.')" Visible="false"/></td>
				</tr>
				<tr valign="top" id="ConfirmationButtonsRow" runat="server" name="ConfirmationButtonsRow" visible="false">
					<td colspan="2"><asp:Button ID="BackButton" runat="server" Text="&lt;- Back" CausesValidation="false" OnClick="BackButton_Click" style="padding-right:10px;"/>
						<asp:Button ID="AdvancedOptionsButton" runat="server" Text="Advanced options" CausesValidation="false" OnClick="AdvancedOptionsButton_Click" style="padding-right:10px;"/>
						<asp:Button ID="SaveTicketRunButton" runat="server" Text="Save ticket run" OnClick="SaveTicketRunButton_Click" ValidationGroup="SaveTicketRun"/></td>
				</tr>
				<tr valign="top" id="RefundButtonRow" runat="server" name="RefundButtonRow" visible="false">
					<td colspan="2"><asp:Button ID="RefundButton" runat="server" Text="Refund this ticket run" CausesValidation="false" OnClick="RefundButton_Click" OnClientClick="return confirm('Are you sure you want to refund all tickets in this ticket run?')"/></td>
				</tr>
			</table>
			<asp:CustomValidator ID="TicketPriceCustomValidator" runat="server" ErrorMessage="Ticket price must be greater than 0" Display="None" ControlToValidate="TicketPriceTextBox" OnServerValidate="MoneyTextBoxVal" ValidationGroup="SaveTicketRun"></asp:CustomValidator>
			<asp:CustomValidator ID="TicketDescriptionCustomValidator" runat="server" ErrorMessage="Ticket description is too long" Display="None" ControlToValidate="TicketDescriptionTextBox" OnServerValidate="DescriptionTextBoxVal" ValidationGroup="SaveTicketRun"></asp:CustomValidator>
			<asp:CustomValidator ID="StartDateCustomValidator" runat="server" ErrorMessage="Start date must be before end date" Display="None" OnServerValidate="StartDateVal" ValidationGroup="SaveTicketRun"></asp:CustomValidator>
			<asp:CustomValidator ID="EndDateCustomValidator" runat="server" ErrorMessage="Invalid end date" Display="None" OnServerValidate="EndDateVal" ValidationGroup="SaveTicketRun"></asp:CustomValidator>		
			<asp:CustomValidator ID="CircularTicketRunDependencyCustomValidator" runat="server" Display="None" ErrorMessage="Cannot follow that ticket run, as it already follows this ticket run." ControlToValidate="FollowsTicketRunDropDownList" OnServerValidate="CircularTicketRunDependencyVal" ValidationGroup="SaveTicketRun"></asp:CustomValidator>
			<asp:CustomValidator ID="MaxTicketsCustomValidator" runat="server" ErrorMessage="Max tickets cannot be less # of tickets already sold" Display="None" ControlToValidate="MaxTicketsTextBox" OnServerValidate="MaxTicketsVal" ValidationGroup="SaveTicketRun"></asp:CustomValidator>
			<asp:CustomValidator ID="SavingErrorCustomValidator" runat="server" ErrorMessage="Error occurred in saving this ticket run. Please try again. If problem persists, please contact an administrator." Display="None" ValidationGroup="SaveTicketRun"></asp:CustomValidator>
			<asp:CustomValidator ID="ErrorMessageCustomValidator" runat="server" ErrorMessage="Error occurred. Please try again." Display="None" ValidationGroup="SaveTicketRun"></asp:CustomValidator>
			<asp:ValidationSummary id="TicketRunValidationSummary" Width="550" runat="server" Font-Bold="True" ValidationGroup="SaveTicketRun"
					CssClass="PaymentValidationSummary" HeaderText="There were some errors:" EnableClientScript="False" ShowSummary="True" DisplayMode="BulletList"></asp:ValidationSummary>
		</p>
	</div>
	<asp:Label ID="JavascriptLabel" runat="server" Visible="false"></asp:Label>
</asp:Panel>

<script language="JavaScript">
	// run this script after the page has loaded
	PromoterTicketRunScreenToggleAdvancedOptions();
	PromoterTicketRunScreenToggleAdminOverrideBookingFee();
</script>