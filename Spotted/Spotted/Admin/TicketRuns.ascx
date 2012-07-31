<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TicketRuns.ascx.cs" Inherits="Spotted.Admin.TicketRuns" %>
<%@ Register TagPrefix="DbCombo" Namespace="Cambro.Web.DbCombo" Assembly="Cambro.Web.DbCombo" %>
<asp:Panel Runat="server" ID="AllTicketRunsPanel">
	<div class="ContentBorder">
		<p>
			<table cellpadding="3" cellspacing="0" border="0">
				<tr>
					<td><nobr>Promoter</nobr></td>
					<td><js:HtmlAutoComplete Width="160px" ID="uiPromotersAutoComplete" runat="server"  WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetPromotersWithK"/></td>
					<td><nobr>Ticket run K</nobr></td>
					<td><asp:TextBox ID="TicketRunKTextBox" runat="server" Width="80px" TabIndex="30"></asp:TextBox></td>
					<td colspan="2"><asp:CheckBox ID="OnlyShowTicketRunsWithSoldTicketsCheckBox" runat="server" Text="Only show ones with tickets sold" /></td>
				</tr>
				<tr>
					<td><nobr>Event</nobr></td>
					<td><js:HtmlAutoComplete ID="uiEventAutoComplete" runat="server" TabIndex="10" Width="160px" WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetEvents"  /></td>
					<td><nobr>Status</nobr></td>
					<td><asp:DropDownList ID="StatusDropDownList" runat="server" TabIndex="30"></asp:DropDownList></td>
					<td><asp:Button ID="SearchButton" runat="server" Text="Search" Width="80px" OnClick="SearchButton_Click" TabIndex="30"/></td>
					<td><asp:Button ID="ClearButton" runat="server" Text="Clear" Width="80px" OnClick="ClearButton_Click" TabIndex="30"/></td>
				</tr>
			</table>
		</p>
		<dsi:h1 runat="server" ID="H1Title" visible="false">All ticket runs</dsi:h1>
		<asp:GridView ID="TicketRunsGridView" runat="server" AllowPaging="True" AutoGenerateColumns="False" PageSize="25" OnPageIndexChanging="TicketRunsGridView_PageIndexChanging"
			OnRowCommand="TicketRunsGridView_RowCommand" CssClass="dataGrid" EnableViewState="true" OnDataBound="TicketRunsGridView_DataBound"
			AlternatingRowStyle-CssClass="dataGridAltItem" GridLines="None" BorderWidth="0" CellPadding="3" HeaderStyle-CssClass="dataGridHeader"
			SelectedRowStyle-CssClass="dataGridSelectedItem" AlternatingRowStyle-VerticalAlign="Top" RowStyle-VerticalAlign="Top">
			<Columns>
				<asp:TemplateField HeaderText="Tickets" ItemStyle-HorizontalAlign="Center">
					<ItemTemplate>
						<%# ((Bobs.TicketRun)(Container.DataItem)).SoldTickets > 0 ? ((Bobs.TicketRun)(Container.DataItem)).LinkTicketsIcon : "&nbsp;"%>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Ticket run">
					<ItemTemplate><asp:TextBox ID="TicketRunKTextBox" runat="server" Visible="false" Text='<%#((Bobs.TicketRun)(Container.DataItem)).K%>'></asp:TextBox>
						<%#((Bobs.TicketRun)(Container.DataItem)).LinkPriceBrandName%>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Promoter">
					<ItemTemplate>
						<%#((Bobs.TicketRun)(Container.DataItem)).Promoter.LinkNewWindow() %>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Event">
					<ItemTemplate>
						<%#((Bobs.TicketRun)(Container.DataItem)).Event.LinkShortNewWindow(40) %>
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
				<asp:TemplateField HeaderText="Live" ItemStyle-HorizontalAlign="Center">
					<ItemTemplate>
						<%#Utilities.TickCrossHtml(((Bobs.TicketRun)(Container.DataItem)).Status.Equals(Bobs.TicketRun.TicketRunStatus.Running))%>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Edit">
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
						<nobr><asp:LinkButton ID="PauseResumeTicketRunButton" runat="server" CommandArgument='<%# Container.DataItemIndex %>' CommandName="PauseResumeTicketRun" OnClientClick="return confirm('Are you sure you want to edit this ticket run?')"><%#((Bobs.TicketRun)(Container.DataItem)).PauseResumeIconHtml%></asp:LinkButton>
						<asp:LinkButton ID="StopTicketRunButton" runat="server" CommandArgument='<%# Container.DataItemIndex %>' CommandName="StopTicketRun" OnClientClick="return confirm('Are you sure you want to stop this ticket run? Once it is stopped it cannot be restarted.')" Visible='<%#((Bobs.TicketRun)(Container.DataItem)).IsUpdateable %>'><%#((Bobs.TicketRun)(Container.DataItem)).StopIconHtml %></asp:LinkButton></nobr>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="Refund">
					<ItemTemplate>
						<asp:Button ID="RefundTicketRunButton" runat="server" Text="Refund" Width="50" Visible="<%#((Bobs.TicketRun)(Container.DataItem)).SoldTickets > 0 && ((Bobs.TicketRun)(Container.DataItem)).Status == Bobs.TicketRun.TicketRunStatus.Ended%>" 
							OnClientClick="return (confirm('Are you sure you want to refund all tickets on this ticket run?') && confirm('Are you double sure?'))" OnClick="RefundTicketRunButton_Click"></asp:Button>
					</ItemTemplate>
				</asp:TemplateField>
			</Columns>	
		</asp:GridView>
		<asp:Label ID="SearchResultsMessageLabel" runat="server" Font-Italic="True" Visible="false"></asp:Label>
	</div>
	<asp:Label ID="TicketRunsJavascriptLabel" runat="server" Visible="false"></asp:Label>
</asp:Panel>
