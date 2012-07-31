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
