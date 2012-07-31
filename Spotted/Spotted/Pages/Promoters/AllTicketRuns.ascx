<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AllTicketRuns.ascx.cs" Inherits="Spotted.Pages.Promoters.AllTicketRuns" %>

<dsi:PromoterIntro runat="server" ID="PromoterIntro" Header="All ticket runs">
	<asp:Panel Runat="server" ID="AddTicketRunPanel">
		<p>
			<table>
				<tr>
					<td valign="top"><a href="<%= CurrentPromoter.UrlApp("ticketrun") %>"><img src="/gfx/icon-add.png" width="26" height="21" border="0" align="absmiddle" style="margin-right:3px;">sell tickets now</a>
						<br>
						<a href="<%= CurrentPromoter.UrlApp("doorlist") %>" target="_blank"><img src="/gfx/icon-print.png" width="26" height="21" border="0" align="absmiddle" style="margin-right:3px;">print door list</a>
					</td>
					<td valign="top" style="padding-left:50px;"><table runat="server" ID="TicketSalesSummaryTable" cellpadding="3" cellspacing="0" visible="false">
							<tr>
								<td>Total ticket runs:</td>
								<td align="right"><asp:Label ID="TotalTicketRunsLabel" runat="server" Text="0"></asp:Label></td>
							</tr>
							<tr>
								<td>Total tickets sold:</td>
								<td align="right"><asp:Label ID="TotalTicketsSoldLabel" runat="server" Text="0"></asp:Label></td>
							</tr>
							<tr>
								<td>Ticket funds released:</td>
								<td align="right"><asp:Label ID="TicketFundsReleasedLabel" runat="server" Text="0"></asp:Label></td>
							</tr>
							<tr>
								<td>Ticket funds in waiting:</td>
								<td align="right"><asp:Label ID="TicketFundsInWaitingLabel" runat="server" Text="0"></asp:Label></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			
		</p>
	</asp:Panel>
</dsi:PromoterIntro>
<asp:Panel Runat="server" ID="AllTicketRunsPanel">
	<dsi:h1 runat="server" ID="H1Title">All ticket runs</dsi:h1>
	<div class="ContentBorder" style="padding-left:0px;padding-right:0px;">
		<p>
			<center>
				<asp:LinkButton ID="SelectCurrentDateRangeLinkButton" runat="server" OnClick="TicketRunDateRangeCurrentSelect">Current event tickets</asp:LinkButton> | 
				<asp:LinkButton ID="SelectPastDateRangeLinkButton" runat="server" OnClick="TicketRunDateRangePastSelect">old event tickets</asp:LinkButton> | 
				<asp:LinkButton ID="SelectAllDateRangeLinkButton" runat="server" OnClick="TicketRunDateRangeAllSelect">all event tickets</asp:LinkButton>
			</center>
		</p>
		<p>
			<asp:GridView ID="TicketRunsGridView" runat="server" AutoGenerateColumns="False" OnRowCommand="TicketRunsGridView_RowCommand" CssClass="dataGrid" EnableViewState="true"
				AlternatingRowStyle-CssClass="dataGridAltItem" GridLines="None" BorderWidth="0" CellPadding="3" HeaderStyle-CssClass="dataGridHeader"
				SelectedRowStyle-CssClass="dataGridSelectedItem" AlternatingRowStyle-VerticalAlign="Top" RowStyle-VerticalAlign="Top" Width="100%">
				<Columns>
					<asp:TemplateField HeaderText="Tickets" ItemStyle-BorderWidth="0">
						<ItemTemplate><asp:TextBox ID="TicketRunKTextBox" runat="server" Visible="false" Text='<%#((Bobs.TicketRun)(Container.DataItem)).K%>'></asp:TextBox>
							<%#((Bobs.TicketRun)(Container.DataItem)).LinkPriceBrandName%>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Event">
						<ItemTemplate>
							<%#((Bobs.TicketRun)(Container.DataItem)).Event.LinkShort(20) %>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Event date">
						<ItemTemplate>
							<%#((Bobs.TicketRun)(Container.DataItem)).Event.FriendlyDate(true)%>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Price" ItemStyle-HorizontalAlign="Right" Visible="false">
						<ItemTemplate>
							<%#((Bobs.TicketRun)(Container.DataItem)).Price.ToString("c")%>
						</ItemTemplate>
					</asp:TemplateField>				
					<asp:TemplateField HeaderText="Sold" ItemStyle-HorizontalAlign="Right">
						<ItemTemplate>
							<nobr><%#((Bobs.TicketRun)(Container.DataItem)).SoldTickets.ToString()%><%# ((Bobs.TicketRun)(Container.DataItem)).CancelledTicketQuantity > 0 ? "(" + ((Bobs.TicketRun)(Container.DataItem)).CancelledTicketQuantity.ToString() + ")" : "" %></nobr>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Max" ItemStyle-HorizontalAlign="Right">
						<ItemTemplate>
							<%#((Bobs.TicketRun)(Container.DataItem)).MaxTickets.ToString()%>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Status">
						<ItemTemplate>
							<%#Utilities.CamelCaseToString(((Bobs.TicketRun)(Container.DataItem)).Status.ToString())%>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Live" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="26">
						<ItemTemplate>
							<%#Utilities.TickCrossHtml(((Bobs.TicketRun)(Container.DataItem)).Status.Equals(Bobs.TicketRun.TicketRunStatus.Running))%>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="26">
						<ItemTemplate>
							<%#((Bobs.TicketRun)(Container.DataItem)).LinkEditIconHtml %>
						</ItemTemplate>
					</asp:TemplateField>				
					<asp:TemplateField HeaderText="Door<br>list" Visible="true">
						<ItemTemplate>
							<%#((Bobs.TicketRun)(Container.DataItem)).Event.DoorlistIconLinkHtml %>
						</ItemTemplate>
					</asp:TemplateField>
					<asp:TemplateField HeaderText="Actions">
						<ItemTemplate>
							<nobr><asp:LinkButton ID="PauseResumeTicketRunButton" runat="server" CommandArgument='<%# Container.DataItemIndex %>' CausesValidation="false"  CommandName="PauseResumeTicketRun"><%#((Bobs.TicketRun)(Container.DataItem)).PauseResumeIconHtml%></asp:LinkButton>
							<asp:LinkButton ID="StopTicketRunButton" runat="server" CommandArgument='<%# Container.DataItemIndex %>' CommandName="StopTicketRun" CausesValidation="false"  OnClientClick="return confirm('Are you sure you wish to stop this ticket run? Once it is stopped it cannot be restarted.')" Visible='<%#((Bobs.TicketRun)(Container.DataItem)).IsUpdateable %>'><%#((Bobs.TicketRun)(Container.DataItem)).StopIconHtml %></asp:LinkButton></nobr>
						</ItemTemplate>
					</asp:TemplateField>
				</Columns>	
			</asp:GridView>
		</p>
		<table width="100%">
			<tr>
				<td align="center">
					<asp:Panel ID="PaginationPanel" runat="server">
						<asp:LinkButton ID="PrevPageLinkButton" runat="server" OnClick="PrevPageLinkButton_Click"><img src="/gfx/icon-back-12.png" alt="Prev page" style="margin-right:3px;" width="12" height="21" align="absmiddle" border="0">prev page</asp:LinkButton> ...
						<asp:LinkButton ID="NextPageLinkButton" runat="server" OnClick="NextPageLinkButton_Click">next page<img src="/gfx/icon-forward-12.png" alt="Next page" style="margin-left:3px;" width="12" height="21" align="absmiddle" border="0"></asp:LinkButton>
					</asp:Panel>
				</td>
			</tr>
		</table>
		<asp:Panel ID="AdminLinksPanel" runat="server" Visible="false">
			<div style="padding-left:8px;padding-right:8px;">
				<p>
					<small><a href='/admin/ticketfundsrelease/promoterk-<%= CurrentPromoter.K.ToString() %>'>[Ticket funds release]</a></small>
				</p>
				<p>
					<small><a href='/admin/ticketsearch/promoterk-<%= CurrentPromoter.K.ToString() %>'>[Ticket search]</a></small>
				</p>
			</div>
		</asp:Panel>
	</div>
</asp:Panel>
