<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventOptions.ascx.cs" Inherits="Spotted.Pages.Promoters.EventOptions" %>

<%@ Register TagPrefix="Controls" TagName="AddThread" Src="/Controls/AddThread.ascx" %>
<asp:Panel Runat="server" ID="PanelEvent">
	<dsi:PromoterIntro runat="server" ID="PromoterIntro" Header="Promoter event page">
		<p>
			This is the promoter event page. It lists all the promotional items connected to your event:
		</p>
		<p runat="server" id="EventLinksP" style="font-weight:bold;font-size:14px;"/>
	</dsi:PromoterIntro>
	
	<dsi:h1 runat="server" ID="Header" NAME="H18">Event details</dsi:h1>
	<div class="ContentBorder" style="padding:0px;">
		<table cellspacing="0" cellpadding="5" border="0" width="100%" class="padding5">
			<tr>
				<td valign="top"><small>Date</small></td>
				<td valign="top"><%= CurrentEvent.FriendlyDate(true) %></td>
				<td valign="top"><small><a href="/event-<%= CurrentEvent.K %>/edit/promoterk-<%= CurrentPromoter.K %>/page-date">Change</a></small></td>
			</tr>
			<tr class="dataGridAltItem">
				<td valign="top"><small>Name</small></td>
				<td valign="top"><%= CurrentEvent.Name %></td>
				<td valign="top"><small><a href="/event-<%= CurrentEvent.K %>/edit/promoterk-<%= CurrentPromoter.K %>/page-details">Change</a></small></td>
			</tr>
			<tr>
				<td valign="top"><small>Venue</small></td>
				<td valign="top"><%= CurrentEvent.Venue.FriendlyHtml(true,false) %></td>
				<td valign="top"><small><a href="/event-<%= CurrentEvent.K %>/edit/promoterk-<%= CurrentPromoter.K %>/page-details">Change</a></small></td>
			</tr>
			<tr class="dataGridAltItem">
				<td valign="top"><small>Start&nbsp;time</small></td>
				<td valign="top"><%= CurrentEvent.StartTime %></td>
				<td valign="top"><small><a href="/event-<%= CurrentEvent.K %>/edit/promoterk-<%= CurrentPromoter.K %>/page-details">Change</a></small></td>
			</tr>
			<tr>
				<td valign="top"><small>Short&nbsp;details</small></td>
				<td valign="top"><%= CurrentEvent.ShortDetailsHtmlRender.Replace("<p>","").Replace("</p>","") %></td>
				<td valign="top"><small><a href="/event-<%= CurrentEvent.K %>/edit/promoterk-<%= CurrentPromoter.K %>/page-details">Change</a></small></td>
			</tr>
			<tr class="dataGridAltItem">
				<td valign="top"><small>Long&nbsp;details</small></td>
				<td valign="top"><small>[long details not shown here]</small></td>
				<td valign="top"><small><a href="/event-<%= CurrentEvent.K %>/edit/promoterk-<%= CurrentPromoter.K %>/page-details">Change</a></small></td>
			</tr>
			<tr>
				<td valign="top"><small>Capacity</small></td>
				<td valign="top"><%= CurrentEvent.Capacity.ToString("#,##0") %></td>
				<td valign="top"><small><a href="/event-<%= CurrentEvent.K %>/edit/promoterk-<%= CurrentPromoter.K %>/page-details">Change</a></small></td>
			</tr>
			<tr class="dataGridAltItem">
				<td valign="top"><small>Party&nbsp;brands</small></td>
				<td valign="top"><%= CurrentEvent.BrandsHtml %></td>
				<td valign="top"><small><a href="/event-<%= CurrentEvent.K %>/edit/promoterk-<%= CurrentPromoter.K %>/page-brand">Change</a></small></td>
			</tr>
			<tr>
				<td valign="top"><small>Music&nbsp;types</small></td>
				<td valign="top"><%= CurrentEvent.MusicTypesString %></td>
				<td valign="top"><small><a href="/event-<%= CurrentEvent.K %>/edit/promoterk-<%= CurrentPromoter.K %>/page-musictype">Change</a></small></td>
			</tr>
			<tr class="dataGridAltItem">
				<td valign="top"><small>Picture</small></td>
				<td valign="top" runat="server" id="EventDetailsPicCell"></td>
				<td valign="top"><small><a href="/event-<%= CurrentEvent.K %>/edit/promoterk-<%= CurrentPromoter.K %>/page-pic">Change</a></small></td>
			</tr>
		</table>
	</div>
	
	
	<asp:Panel Runat="server" ID="SpotterRequestYesPanel">
		<a name="SpotterRequestPanel"/>
		<dsi:h1 runat="server" ID="H1" NAME="H18a">Spotter invite</dsi:h1>
		<div class="ContentBorder">
			<p>
				<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="left" style="margin-right:3px;">
				You've invited spotters to contact you to arrange a guestlist. 
				The contact details are: <asp:Label runat="server" ID="SpotterRequestDetails" />.
			</p>
			<p>
				To change your spotter invite options, <a href="" runat="server" id="SpotterRequestYesLink">click here</a>
			</p>
		</div>
	</asp:Panel>
		
	<asp:Panel Runat="server" ID="SpotterRequestNoPanel">
		<a name="SpotterRequestPanel"/>
		<dsi:h1 runat="server" ID="H2" NAME="H18b">No spotter invites</dsi:h1>
		<div class="ContentBorder">
			<p>
				<img src="/gfx/icon-warning.png" border="0" width="26" height="21" align="absmiddle" style="margin-right:3px;"><b>Warning!</b> You haven't invited any spotters!
			</p>
			<p class="MedCenter">
				<a href="" runat="server" id="SpotterRequestNoLink"><img src="/gfx/icon-add.png" border="0" width="26" height="21"
					align="absmiddle" style="margin-right:3px;">invite a spotter</a>
			</p>
		</div>
	</asp:Panel>
	
	
	
	<asp:Panel Runat="server" ID="TicketRunPanel">
		<a name="TicketRunPanel"/>
		<dsi:h1 runat="server" ID="H10" NAME="H18">Tickets</dsi:h1>
		<div class="ContentBorder" style="padding-left:0px;padding-right:0px;">
			<p>
				<asp:GridView ID="TicketRunsGridView" runat="server" AllowPaging="False" AutoGenerateColumns="False" 
					OnRowCommand="TicketRunsGridView_RowCommand" CssClass="dataGrid" EnableViewState="true"
					AlternatingRowStyle-CssClass="dataGridAltItem" GridLines="None" BorderWidth="0" CellPadding="3" HeaderStyle-CssClass="dataGridHeader"
					SelectedRowStyle-CssClass="dataGridSelectedItem" AlternatingRowStyle-VerticalAlign="Top" RowStyle-VerticalAlign="Top" Width="100%">
					<Columns>
						<asp:TemplateField HeaderText="Tickets"  ItemStyle-BorderWidth="0">
							<ItemTemplate><asp:TextBox ID="TicketRunKTextBox" runat="server" Visible="false" Text='<%#((Bobs.TicketRun)(Container.DataItem)).K%>'></asp:TextBox>
								<%#((Bobs.TicketRun)(Container.DataItem)).LinkPriceBrandName%>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Price" ItemStyle-HorizontalAlign="Right" Visible="false">
							<ItemTemplate>
								<%#((Bobs.TicketRun)(Container.DataItem)).Price.ToString("c")%>
							</ItemTemplate>
						</asp:TemplateField>				
						<asp:TemplateField HeaderText="Tickets<br>start" Visible="false">
							<ItemTemplate>
								<%#((Bobs.TicketRun)(Container.DataItem)).StartDateTime.ToString("dd/MM/yy HH:mm")%> 
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Tickets<br>end" Visible="false">
							<ItemTemplate>
								<%#((Bobs.TicketRun)(Container.DataItem)).EndDateTime.ToString("dd/MM/yy HH:mm")%> 
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Sold" ItemStyle-HorizontalAlign="Right">
							<ItemTemplate>
								<%#((Bobs.TicketRun)(Container.DataItem)).SoldTickets.ToString()%><%# ((Bobs.TicketRun)(Container.DataItem)).CancelledTicketQuantity > 0 ? "(" + ((Bobs.TicketRun)(Container.DataItem)).CancelledTicketQuantity.ToString() + ")" : "" %>
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
						<asp:TemplateField HeaderText="Door<br>list" Visible="false">
							<ItemTemplate>
								<%#((Bobs.TicketRun)(Container.DataItem)).Event.DoorlistIconLinkHtml %>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Actions">
							<ItemTemplate>
								<nobr><asp:LinkButton ID="PauseResumeTicketRunButton" runat="server" CommandArgument='<%# Container.DataItemIndex %>' CommandName="PauseResumeTicketRun" CausesValidation="false"><%#((Bobs.TicketRun)(Container.DataItem)).PauseResumeIconHtml%></asp:LinkButton>
								<asp:LinkButton ID="StopTicketRunButton" runat="server" CommandArgument='<%# Container.DataItemIndex %>' CommandName="StopTicketRun" CausesValidation="false" OnClientClick="return confirm('Are you sure you wish to stop this ticket run? Once it is stopped it cannot be restarted.')" Visible='<%#((Bobs.TicketRun)(Container.DataItem)).IsUpdateable %>'><%#((Bobs.TicketRun)(Container.DataItem)).StopIconHtml %></asp:LinkButton></nobr>
							</ItemTemplate>
						</asp:TemplateField>
					</Columns>	
				</asp:GridView>
			</p>
			<p style="padding-left:8px;padding-right:8px;" id="NoTicketRunsP" runat="server" visible="false">
				<img src="/gfx/icon-warning.png" border="0" width="26" height="21" align="absmiddle" style="margin-right:3px;"><b>Warning!</b> You are not selling tickets for this event! It's FREE to sell tickets!
			</p>
			<p class="MedCenter" id="SellTicketsP" runat="server">
				<a href="" runat="server" id="SellTicketsLink"><img src="/gfx/icon-add.png" border="0" width="26" height="21"
					align="absmiddle" style="margin-right:3px;">sell tickets</a>
			</p>
			<p class="MedCenter" id="NoSellTicketsP" runat="server">
				You can no longer sell tickets for this event.
			</p>
			
			<p class="MedCenter" id="DoorlistP" runat="server">
				<a href="" runat="server" id="DoorlistLink"><img src="/gfx/icon-print.png" border="0"  width="26" height="21"
					align="absmiddle" style="margin-right:3px;">print door list</a>
			</p>
			<p style="padding-left:8px;padding-right:8px;">
