<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Doorlist.ascx.cs" Inherits="Spotted.Controls.Doorlist" %>

<style media="print">
	.HideOnPrint
	{
		display:none;
	}
	.ShowOnPrintOnly
	{
		display:block;
	}
</style>
<style media="screen">
	.ShowOnPrintOnly
	{
		display:none;
	}
</style>

<table cellpadding="3" cellspacing="0" border="0" width="100%" style="font-size:large; font-weight:bold;" class="BorderBlack Bottom">
	<tr>
		<td colspan="2">Doorlist</td>
		<td rowspan="2" align="right" runat="server" id="uiLogo"><img src='http://www.dontstayin.com/gfx/dsi-60.gif' valign='bottom' /></td>			
	</tr>
	<tr valign="top">
		<td width="70">Event:</td>
		<td><div id="EventName" runat="server"></div></td>
	</tr>
	<asp:Repeater ID="TicketRunSelectRepeater" runat="server">
		<ItemTemplate>
			<tr id="TicketRunRow" runat="server" >
				<td colspan="3"><asp:TextBox ID="TicketRunKTextBox" runat="server" Text='<%# ((TicketRun)Container.DataItem).K%>' Visible="false"></asp:TextBox><asp:CheckBox ID="TicketRunCheckBox" runat="server" Checked="true" AutoPostBack="true" OnCheckedChanged="TicketRunCheckBox_CheckedChanged" Text='<%# ((TicketRun)Container.DataItem).PriceBrandName%>' /></td>
			</tr>
		</ItemTemplate>
	</asp:Repeater>
	<tr id="TicketRunTitleRow" runat="server" valign="top" visible="false">
		<td width="70">Tickets:</td>
		<td><font style="font-size:large; font-weight:bold;" id="TicketRunName" runat="server"></font></td>
	</tr>
</table>
<p class="ShowOnPrintOnly">
	<font style="font-size:large; font-weight:bold;">You MUST check that the person has brought the right card.<br />If they have forgotten their card, they should be denied entrance.</font>
</p>
<p class="HideOnPrint" id="HideOnPrintP" runat="server">
	<table cellpadding="3" cellspacing="0" border="0" style="padding-bottom:3px;" width="100%">
		<tr>
			<td width="57"><nobr>Order by</nobr></td>
			<td width="90"><asp:DropDownList ID="OrderByDropDownList" runat="server" OnSelectedIndexChanged="OrderByDropDownList_SelectedIndexChanged" AutoPostBack="true">
				</asp:DropDownList></td>
			<td align="right"><div runat="server" id="uiExportOptions"><a href="#" onClick="window.print();return false"><img src="/gfx/icon32-printer.gif" height="32" width="32" border="0" style="vertical-align:middle" />Print doorlist</a>&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="ExportToExcelLinkButton" runat="server" OnClick="ExportToExcelLinkButton_Click"><img src="/gfx/icon32-excel.gif" height="32" width="32" border="0" style="vertical-align:middle" />Download in Excel format</asp:LinkButton>&nbsp;&nbsp;&nbsp;
				<asp:LinkButton ID="ExportToCSVLinkButton" runat="server" OnClick="ExportToCSVLinkButton_Click"><img src="/gfx/icon32-csv.gif" height="32" width="32" border="0" style="vertical-align:middle" />Download in CSV format</asp:LinkButton></div></td>
		</tr>
	</table>
</p>
<asp:GridView ID="DoorlistGridView" runat="server" DataKeyNames="K" AllowPaging="False" AutoGenerateColumns="False" CssClass="dataGrid"
	AlternatingRowStyle-CssClass="dataGridAltItem" GridLines="None" BorderWidth="0" CellPadding="3" HeaderStyle-CssClass="dataGridHeader"
	SelectedRowStyle-CssClass="dataGridSelectedItem" AlternatingRowStyle-VerticalAlign="Top" RowStyle-VerticalAlign="Top" Width="100%">
	<Columns>
	
	
		<asp:TemplateField HeaderText="<nobr>First name</nobr>" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Left" Visible="false">
			<ItemTemplate>
				<%#((Ticket)(Container.DataItem)).FirstName.ToUpper()%>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="<nobr>Last name</nobr>" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Left" Visible="false">
			<ItemTemplate>
				<%#((Ticket)(Container.DataItem)).LastName.ToUpper()%>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="<nobr>Code</nobr>" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Left" Visible="false">
			<ItemTemplate>
				<%#((Ticket)(Container.DataItem)).Code%>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="<nobr>Digits</nobr>" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Left" Visible="false">
			<ItemTemplate>
				<asp:Label ID="SpaceToPreserveLeadingZerosInExcel0" runat="server" Text="&amp;nbsp;" Visible="<%# Convert.ToBoolean(isExportXLSFile) %>"></asp:Label><%#((Ticket)(Container.DataItem)).CardNumberEnd%>
			</ItemTemplate>
		</asp:TemplateField>
	
		
		
		<asp:TemplateField HeaderText="<nobr>First name</nobr>" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Left" Visible="false">
			<ItemTemplate>
				<%#((Ticket)(Container.DataItem)).FirstName.ToUpper()%>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="<nobr>Last name</nobr>" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Left" Visible="false">
			<ItemTemplate>
				<%#((Ticket)(Container.DataItem)).LastName.ToUpper()%>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="<nobr>Code</nobr>" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Left" Visible="false">
			<ItemTemplate>
				<%#((Ticket)(Container.DataItem)).Code%>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="<nobr>Digits</nobr>" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Left" Visible="false">
			<ItemTemplate>
				<asp:Label ID="SpaceToPreserveLeadingZerosInExcel1" runat="server" Text="&amp;nbsp;" Visible="<%# Convert.ToBoolean(isExportXLSFile) %>"></asp:Label><%#((Ticket)(Container.DataItem)).CardNumberEnd%>
			</ItemTemplate>
		</asp:TemplateField>

		
		
		
		
		<asp:TemplateField HeaderText="<nobr>Ticket run</nobr>" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Left">
			<ItemTemplate>
				<%#Cambro.Misc.Utility.Snip((string)((Ticket)(Container.DataItem)).ExtraSelectElements["TicketRunPriceName"], 24)%>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="Tickets" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Left">
			<ItemTemplate>
				<%#((Ticket)(Container.DataItem)).Quantity.ToString()%> <%#TicketCheckBoxes((Ticket)(Container.DataItem))%>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="CV2" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Left">
			<ItemTemplate>
				<table id="Table1" runat="server" cellpadding="0" width="54" cellspacing="0" border="1" visible='<%# ForExport && ((Ticket)(Container.DataItem)).ExtraSelectElements["Promoter_WillCheckCardsForPurchasedTickets"].ToString() == bool.TrueString %>'><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr></table>
				<asp:TextBox runat="server" ID="uiCV2" Columns="3" MaxLength="3" Width="100%" Visible='<%# !ForExport && ((Ticket)(Container.DataItem)).ExtraSelectElements["Promoter_WillCheckCardsForPurchasedTickets"].ToString() == bool.TrueString && ((Ticket)(Container.DataItem)).CardCheckedByPromoter == false && ((Ticket)(Container.DataItem)).HasExceededCardCheckAttempts == false %>'></asp:TextBox>
				<img runat="server" ID="uiTick" src="/gfx/icon-tick-up.png" Visible='<%# !ForExport && ((Ticket)(Container.DataItem)).ExtraSelectElements["Promoter_WillCheckCardsForPurchasedTickets"].ToString() == bool.TrueString && ((Ticket)(Container.DataItem)).CardCheckedByPromoter == true %>'></img>
				<img runat="server" id="uiCross" src="/gfx/icon-cross-up.png" Visible='<%# !ForExport && ((Ticket)(Container.DataItem)).ExtraSelectElements["Promoter_WillCheckCardsForPurchasedTickets"].ToString() == bool.TrueString && ((Ticket)(Container.DataItem)).CardCheckedByPromoter == false && ((Ticket)(Container.DataItem)).HasExceededCardCheckAttempts == true %>'></img>
				<asp:Label ID="Label2" runat="server" Text="N/A" Visible='<%# !ForExport && ((Ticket)(Container.DataItem)).ExtraSelectElements["Promoter_WillCheckCardsForPurchasedTickets"].ToString() != bool.TrueString %>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
	</Columns>
</asp:GridView>
<asp:Label runat="server" ID="uiNoTickets" Text="No tickets to display"></asp:Label>
<asp:Panel ID="EndAllTicketRunsPanel" runat="server" Visible="false">
	<p style="font-size:x-large;" align="center">
		<br />Some of your ticket runs for this event have not ended.<br />You must end all ticket runs before printing off the doorlist.<br /><br /><br />
		<asp:Button id="EndAllTicketRunsButton" runat="server" style="font-size:large; width:250px;" OnClientClick="return confirm('Are you sure you want to end all ticket runs for this event?');" OnClick="EndAllTicketRunsButton_Click" Text="End all ticket runs" /><asp:Button ID="CancelButton" runat="server" style="font-size:large; margin-left:60px; width:250px;" OnClientClick="window.close();" OnClick="CancelButton_Click" Text="Cancel" />
	</p>
</asp:Panel>
