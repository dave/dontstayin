<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyTickets.ascx.cs" Inherits="Spotted.Pages.Usrs.MyTickets" %>
<asp:Panel runat="server" ID="MyTicketsPanel">
	<dsi:h1 runat="Server" id="MyTicketsHeading">My tickets</dsi:h1>
	<div class="ContentBorder">
		<p>
			<center>
				<asp:LinkButton ID="SelectCurrentDateRangeLinkButton" runat="server" OnClick="TicketRunDateRangeCurrentSelect">Current event tickets</asp:LinkButton> | 
				<asp:LinkButton ID="SelectPastDateRangeLinkButton" runat="server" OnClick="TicketRunDateRangePastSelect">old event tickets</asp:LinkButton> | 
				<asp:LinkButton ID="SelectAllDateRangeLinkButton" runat="server" OnClick="TicketRunDateRangeAllSelect">all event tickets</asp:LinkButton>
			</center>
		</p>
		<br />
		<asp:Panel ID="uiHasETickets" runat="server">
			<p class="CleanLinks">
				<asp:Repeater ID="MyEventTicketsRepeater" runat="server" OnItemDataBound="MyEventTicketsRepeater_ItemDataBound">
					<HeaderTemplate>
						<table cellpadding="3" cellspacing="0" border="0">
							<tr id="TicketRunHeaderRow" runat="server" valign="top">
								<td><small>#</small></td>
								<td colspan="2"><small>Ticket</small></td>
								<td align="center" id="CodeHeader" runat="server"></td>
								<td align="right"><small><nobr>Card digits</nobr></small></td>								
								<td align="center"><small>Print</small></td>
								<td>&nbsp;</td>
							</tr>
					</HeaderTemplate>
					<ItemTemplate>
						<tr id="EventDetailsRow" runat="server">
							<td colspan="3" style="border-top:solid 1px #CBA21E;"><b><%# ((Event)Container.DataItem).FriendlyHtml()%></b></td>
							<td colspan="4" style="border-top:solid 1px #CBA21E;">&nbsp;</td>
						</tr>
						<asp:Repeater ID="MyTicketsRepeater" runat="server" OnItemDataBound="MyTicketsRepeater_ItemDataBound"></asp:Repeater>
					</ItemTemplate>
					<FooterTemplate>
						<tr>
							<td colspan="6" align="center" style="border-top:solid 1px #CBA21E;"><asp:LinkButton ID="PrevPageLinkButton" runat="server" Enabled="false" OnClick="PrevPageLinkButton_Click"><img src="/gfx/icon-back-12.png" alt="Prev page" style="margin-right:3px;" width="12" height="21" align="absmiddle" border="0" />prev page</asp:LinkButton> ...
								<asp:LinkButton ID="NextPageLinkButton" runat="server" Enabled="false" OnClick="NextPageLinkButton_Click">next page<img src="/gfx/icon-forward-12.png" alt="Next page" style="margin-left:3px;" width="12" height="21" align="absmiddle" border="0" /></asp:LinkButton></td>
						</tr>
						</table>
					</FooterTemplate>
				</asp:Repeater>				
			</p>
		</asp:Panel>
		<asp:Panel ID="NoTickets" runat="server">
			<p>
				You do not have any tickets for the selected date range.
			</p>
		</asp:Panel>
	</div>
</asp:Panel>
