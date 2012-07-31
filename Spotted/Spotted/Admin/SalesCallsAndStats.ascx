<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalesCallsAndStats.ascx.cs" Inherits="Spotted.Admin.SalesCallsAndStats" %>

<script language="JavaScript">
  function SalesCallsAndStatsScreenToggleOverrideDates()
  {
	document.getElementById("<%=  OverrideFromDatesRow.ClientID  %>").style.display = document.getElementById("<%=  OverrideDateCheckBox.ClientID  %>").checked?'':'none';
	document.getElementById("<%=  OverrideToDatesRow.ClientID  %>").style.display = document.getElementById("<%=  OverrideDateCheckBox.ClientID  %>").checked?'':'none';
  }  
</script>
<dsi:h1 runat="server" id="H1">Sales Calls and Stats</dsi:h1>
<div class="ContentBorder">
	<asp:Panel ID="HeaderPanel" runat="server" Width="430">
		<table border="0" width="100%" cellpadding="3" cellspacing="0">
			<tr>
				<td align="left"><asp:Button ID="MyTodayButton" runat="server" Text="My Day" OnClick="MyTodayButton_Click" Width="100px" CausesValidation="False" />&nbsp;&nbsp;<asp:Button ID="MyMonthButton"
						runat="server" Text="My Month" OnClick="MyMonthButton_Click" Width="100px" CausesValidation="False" />&nbsp;&nbsp;<asp:Button ID="TeamTodayButton"
						runat="server" Text="Team Day" OnClick="TeamTodayButton_Click" Width="100px" CausesValidation="False" />&nbsp;&nbsp;<asp:Button ID="TeamMonthButton"
						runat="server" Text="Team Month" OnClick="TeamMonthButton_Click" Width="100px" CausesValidation="False" /></td>
			</tr>
			<tr><td style="background-color:transparent;border-bottom:dotted 1px #000000;" width="100%">&nbsp;</td></tr>
		</table>
		<br />
		<table border="0" cellpadding="3" cellspacing="0">
			<tr>
				<td align="right" valign="middle"><asp:Label ID="UserLabel" runat="server" Text="User"></asp:Label></td>
				<td align="left" valign="bottom"><asp:Image ID="SpacerImage1" runat="server" ImageUrl="/gfx/spacer.gif" Width="1" /><asp:DropDownList ID="SalesPersonsDropDownList" runat="server">
					</asp:DropDownList></td>				
			</tr>
			<tr>
				<td></td>
				<td><asp:CheckBox ID="OverrideDateCheckBox" runat="server" Text="Override default dates" onselect="SalesCallsAndStatsScreenToggleOverrideDates()" onclick="SalesCallsAndStatsScreenToggleOverrideDates();"/></td>
			</tr>
			<tr id="OverrideFromDatesRow" style="display:none;" runat="server">
				<td align="right" valign="middle"><asp:Label ID="FromLabel" runat="server" Text="From"></asp:Label></td>
				<td valign="bottom"><dsi:Cal id="FromDateCal" runat="server"></dsi:Cal></td>
			</tr>
			<tr id="OverrideToDatesRow" style="display:none;" runat="server">
				<td align="right" valign="middle"><asp:Label ID="ToLabel" runat="server" Text="To"></asp:Label></td>
				<td valign="bottom"><dsi:Cal id="ToDateCal" runat="server"></dsi:Cal><asp:CustomValidator ID="DateRangeCustomValidator" runat="server" Display="None" ErrorMessage="Invalid date range" OnServerValidate="DateRangeVal"></asp:CustomValidator></td>
			</tr>
			<tr>
				<td colspan="2"><asp:Button ID="SalesCallsDailyButton" runat="server" Text="Daily" OnClick="SalesCallsDailyButton_Click" Width="100px" />&nbsp;&nbsp;<asp:Button ID="SalesCallsWeeklyButton" 
				runat="server" Text="Weekly" OnClick="SalesCallsWeeklyButton_Click" Width="100px" />&nbsp;&nbsp;<asp:Button ID="SalesCallsMonthlyButton" 
				runat="server" Text="Monthly" OnClick="SalesCallsMonthlyButton_Click" Width="100px" />&nbsp;&nbsp;<asp:Button ID="SalesStatsButton" 
				runat="server" Text="Detail" OnClick="SalesStatsButton_Click" Width="100px" /></td>
			</tr>
		</table>
		<table border="0" width="100%" cellpadding="3" cellspacing="0">
			<tr><td style="background-color:transparent;border-bottom:dotted 1px #000000;" width="100%">&nbsp;</td></tr>
		</table>
		<asp:CustomValidator ID="NotSalesPersonCustomValidator" runat="server" ErrorMessage="You are not a sales person" Display="None"></asp:CustomValidator>
		<asp:ValidationSummary id="SalesValidationSummary" Width="600" runat="server" Font-Bold="True" 
            CssClass="PaymentValidationSummary" HeaderText="There were some errors:" EnableClientScript="False" ShowSummary="True" DisplayMode="BulletList"></asp:ValidationSummary>
		<asp:Panel ID="ResultsPanel" runat="server">
			<br />
			<asp:Label ID="DateRangeLabel" runat="server" Text="Date range: " Visible="false"></asp:Label><asp:Label ID="DateRangeValueLabel" runat="server" Font-Bold="true" Text="" Visible="false"></asp:Label>
			<br />
			<br />
			<table id="SalesCallsResultsTable" runat="server" cellpadding="3" cellspacing="0" class="dataGrid" visible="false"/>
			<table id="SalesStatsResultsTable" runat="server" cellpadding="3" cellspacing="0" class="dataGrid" visible="false"/>
		</asp:Panel>
	</asp:Panel>

</div>
<script language="JavaScript">
	// run this script after the page has loaded
	SalesCallsAndStatsScreenToggleOverrideDates();
</script>
