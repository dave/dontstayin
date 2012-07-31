<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyTickets.ascx.cs" Inherits="Spotted.Styled.MyTickets" %>
<h2>My tickets</h2>
<hr />
<div class="InnerDiv">
	<p>
		<center>
			<asp:LinkButton ID="SelectCurrentDateRangeLinkButton" class="Link" runat="server" OnClick="TicketRunDateRangeCurrentSelect">Current event tickets</asp:LinkButton> | 
			<asp:LinkButton ID="SelectPastDateRangeLinkButton" class="Link" runat="server" OnClick="TicketRunDateRangePastSelect">old event tickets</asp:LinkButton> | 
			<asp:LinkButton ID="SelectAllDateRangeLinkButton" class="Link" runat="server" OnClick="TicketRunDateRangeAllSelect">all event tickets</asp:LinkButton>
		</center>
	</p>
	<br />
	<asp:Panel ID="HasTickets" runat="server">
		<p class="CleanLinks">
			<table cellpadding="3" cellspacing="0" border="0">
			<asp:Repeater ID="MyEventTicketsRepeater" runat="server" OnItemDataBound="MyEventTicketsRepeater_ItemDataBound">
				<HeaderTemplate>
					<tr id="TicketRunHeaderRow" runat="server" valign="top">
						<td>#</td>
						<td colspan="2">Ticket</td>
						<td align="center" id="CodeHeader" runat="server"></td>
						<td align="right"><nobr>Card digits</nobr></td>								
						<td align="center">Print</td>
						<td>&nbsp;</td>
					</tr>
				</HeaderTemplate>
				<ItemTemplate>
					<tr id="EventDetailsRow" class="MyTicketsEventDetailsRow" runat="server">
						<td colspan="3"><div class="Link"><div class="EventLink"><b><a href="<%# StyledObject.UrlStyledEvent(((Event)Container.DataItem).K)%>" class="Link"><span class="EventDate"><nobr><%# ((Event)Container.DataItem).DateTime.ToString("MMM dd, yy")%></nobr></span><span class="EventLinkSpacer">&nbsp;:&nbsp;</span><span class="EventName"><%# ((Event)Container.DataItem).Name%></span><span class="ClubName"> @ <%# ((Event)Container.DataItem).Venue.Name%></span><span class="CityName"> in <%# ((Event)Container.DataItem).Venue.Place.Name%></span></a></b></div></div></td>
						<td colspan="4">&nbsp;</td>
					</tr>
					<asp:Repeater ID="MyTicketsRepeater" runat="server" OnItemDataBound="MyTicketsRepeater_ItemDataBound"></asp:Repeater>
				</ItemTemplate>
				<FooterTemplate>
					<tr>
						<td colspan="6" align="center"><asp:LinkButton ID="PrevPageLinkButton" runat="server" Enabled="false" OnClick="PrevPageLinkButton_Click">prev page</asp:LinkButton> ...
							<asp:LinkButton ID="NextPageLinkButton" runat="server" Enabled="false" OnClick="NextPageLinkButton_Click">next page</asp:LinkButton></td>
					</tr>
				</FooterTemplate>
			</asp:Repeater>				
			</table>
		</p>
 
	</asp:Panel>
	<asp:Panel ID="NoTickets" runat="server">
		<p>
			You do not have any tickets for the selected date range.
		</p>
	</asp:Panel>
</div>
