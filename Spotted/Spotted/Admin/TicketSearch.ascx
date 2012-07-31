<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TicketSearch.ascx.cs" Inherits="Spotted.Admin.TicketSearch" %>
<%@ Register TagPrefix="DbCombo" Namespace="Cambro.Web.DbCombo" Assembly="Cambro.Web.DbCombo" %>
<asp:Panel ID="TicketSearchCriteriaPanel" runat="server">
	<div class="ContentBorder">
		<table cellpadding="3" cellspacing="0" border="0">
			<tr>
				<td><nobr>Promoter</nobr></td>
				<td><js:HtmlAutoComplete Width="140px" ID="uiPromotersAutoComplete" runat="server"  WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetPromotersWithK"/></td>
				<td><nobr>First name</nobr></td>
				<td><asp:TextBox ID="FirstNameTextBox" runat="server" Width="100px" TabIndex="20"></asp:TextBox></td>
				<td><nobr>Ticket run K</nobr></td>
				<td><asp:TextBox ID="TicketRunKTextBox" runat="server" Width="80px" TabIndex="30"></asp:TextBox></td>
			</tr>
			<tr>
				<td><nobr>Buyer user</nobr></td>
				<td><js:HtmlAutoComplete Width="140px" TabIndex="10" ID="uiUsersAutoComplete" runat="server"  WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetUsersWithK"/></td>
				<td><nobr>Last name</nobr></td>
				<td><asp:TextBox ID="LastNameTextBox" runat="server" Width="100px" TabIndex="20"></asp:TextBox></td>
				<td><nobr>Status</nobr></td>
				<td><asp:DropDownList ID="StatusDropDownList" runat="server" Width="80px" TabIndex="30"></asp:DropDownList></td>
			</tr>
			<tr>
				<td><nobr>Card end digits</nobr></td>
				<td><asp:TextBox ID="CardDigitsTextBox" runat="server" Width="60px" TabIndex="10" MaxLength="4"></asp:TextBox></td>
				<td><nobr>Post code</nobr></td>
				<td><asp:TextBox ID="PostCodeTextBox" runat="server" Width="80px" TabIndex="20"></asp:TextBox></td>
				<td><nobr>Feedback</nobr></td>
				<td><asp:DropDownList ID="FeedbackDropDownList" runat="server" Width="80px" TabIndex="30"></asp:DropDownList></td>
				<td></td>
				<td><asp:Button ID="SearchButton" runat="server" Text="Search" Width="80px" OnClick="SearchButton_Click" TabIndex="30"/></td>
				<td><asp:Button ID="ClearButton" runat="server" Text="Clear" Width="80px" OnClick="ClearButton_Click" TabIndex="30"/></td>
			</tr>
		</table>
		<asp:Label ID="ErrorLabel" runat="server" style="border:2px; color:Red; font-weight:bold;" Visible="false"></asp:Label>
		<asp:GridView ID="SearchResultsTicketsGridView" runat="server" AllowPaging="True" AutoGenerateColumns="False" 
			OnPageIndexChanging="SearchResultsTicketsGridView_PageIndexChanging" CssClass="dataGrid" BorderWidth="0" CellPadding="3"
			AlternatingRowStyle-CssClass="dataGridAltItem" RowStyle-CssClass="dataGridItem" SelectedRowStyle-CssClass="dataGridSelectedItem" 
			PageSize="25" HeaderStyle-CssClass="dataGridHeader" AlternatingRowStyle-VerticalAlign="Top" RowStyle-VerticalAlign="Top">
			<PagerStyle HorizontalAlign="Right" />
			<Columns>
				<asp:TemplateField HeaderText="<nobr>View</nobr>">
					<ItemStyle HorizontalAlign="Left" />
					<ItemTemplate>
						<%#Utilities.LinkNewWindow(((Bobs.Ticket)Container.DataItem).UrlAdmin(), Utilities.IconHtml(Utilities.Icon.View))%>
					</ItemTemplate>
					<HeaderStyle HorizontalAlign="Left" />
				</asp:TemplateField>
				<asp:TemplateField HeaderText="<nobr>K #</nobr>" SortExpression="K">
					<ItemStyle HorizontalAlign="Right" />
					<ItemTemplate>
						<%#((Bobs.Ticket)Container.DataItem).K.ToString()%>
					</ItemTemplate>
					<HeaderStyle HorizontalAlign="Left" />
				</asp:TemplateField>
				<asp:TemplateField HeaderText="<nobr>Date</nobr>" SortExpression="BuyDateTime">
					<ItemStyle HorizontalAlign="Right" />
					<ItemTemplate>
						<nobr><%#((Bobs.Ticket)Container.DataItem).BuyDateTime.ToString("ddd dd/MM/yy HH:mm")%></nobr>
					</ItemTemplate>
					<HeaderStyle HorizontalAlign="Left" />
				</asp:TemplateField>
				<asp:TemplateField HeaderText="<nobr>User</nobr>">
					<ItemStyle HorizontalAlign="Left" />
					<ItemTemplate>
						<%#((Bobs.Ticket)Container.DataItem).BuyerUsr.Link()%>
					</ItemTemplate>
					<HeaderStyle HorizontalAlign="Left" />
				</asp:TemplateField>
				<asp:TemplateField HeaderText="<nobr>First</nobr>">
					<ItemStyle HorizontalAlign="Left" />
					<ItemTemplate>
						<%#((Bobs.Ticket)Container.DataItem).FirstName%>
					</ItemTemplate>
					<HeaderStyle HorizontalAlign="Left" />
				</asp:TemplateField>
				<asp:TemplateField HeaderText="<nobr>Last</nobr>">
					<ItemStyle HorizontalAlign="Left" />
					<ItemTemplate>
						<%#((Bobs.Ticket)Container.DataItem).LastName%>
					</ItemTemplate>
					<HeaderStyle HorizontalAlign="Left" />
				</asp:TemplateField>
				<asp:TemplateField HeaderText="<nobr>Post</nobr>">
					<ItemStyle HorizontalAlign="Left" />
					<ItemTemplate>
						<%#((Bobs.Ticket)Container.DataItem).AddressPostcode%>
					</ItemTemplate>
					<HeaderStyle HorizontalAlign="Left" />
				</asp:TemplateField>
				<asp:TemplateField HeaderText="<nobr>Card</nobr>">
					<ItemStyle HorizontalAlign="Left" />
					<ItemTemplate>
						<%#((Bobs.Ticket)Container.DataItem).CardNumberEnd%>
					</ItemTemplate>
					<HeaderStyle HorizontalAlign="Left" />
				</asp:TemplateField>
				<asp:TemplateField HeaderText="<nobr>Tickets</nobr>">
					<ItemStyle HorizontalAlign="Right" />
					<ItemTemplate>
						<span style="margin-right:10px;"><%#((Bobs.Ticket)Container.DataItem).Quantity.ToString()%></span>
					</ItemTemplate>
					<HeaderStyle HorizontalAlign="Left" />
				</asp:TemplateField>
				<asp:TemplateField HeaderText="<nobr>Ticket run</nobr>">
					<ItemStyle HorizontalAlign="Left" />
					<ItemTemplate>
						<%#((Bobs.Ticket)Container.DataItem).TicketRun.LinkPriceBrandName%>
					</ItemTemplate>
					<HeaderStyle HorizontalAlign="Left" />
				</asp:TemplateField>
				<asp:TemplateField HeaderText="<nobr>Promoter</nobr>">
					<ItemStyle HorizontalAlign="Left" />
					<ItemTemplate>
						<%#((Bobs.Ticket)Container.DataItem).TicketRun.Promoter.Link()%>
					</ItemTemplate>
					<HeaderStyle HorizontalAlign="Left" />
				</asp:TemplateField>
				<asp:TemplateField HeaderText="<nobr>Event</nobr>">
					<ItemStyle HorizontalAlign="Left" />
					<ItemTemplate>
						<%#((Bobs.Ticket)Container.DataItem).Event.LinkShort(30)%>
					</ItemTemplate>
					<HeaderStyle HorizontalAlign="Left" />
				</asp:TemplateField>
				<asp:TemplateField HeaderText="<nobr>Event date</nobr>">
					<ItemStyle HorizontalAlign="Right" />
					<ItemTemplate>
						<%#Utilities.DateToString(((Bobs.Ticket)Container.DataItem).Event.DateTime)%>
					</ItemTemplate>
					<HeaderStyle HorizontalAlign="Left" />
				</asp:TemplateField>
				<asp:TemplateField HeaderText="<nobr>Feedback</nobr>">
					<ItemStyle HorizontalAlign="Center" />
					<ItemTemplate>
						<%#((Bobs.Ticket)Container.DataItem).Feedback != Ticket.FeedbackEnum.None ? Utilities.TickCrossHtml(((Bobs.Ticket)Container.DataItem).Feedback == Ticket.FeedbackEnum.Good) : ""%>
					</ItemTemplate>
					<HeaderStyle HorizontalAlign="Left" />
				</asp:TemplateField>
				<asp:TemplateField HeaderText="<nobr>Cancelled</nobr>">
					<ItemStyle HorizontalAlign="Center" />
					<ItemTemplate>
						<%# ((Bobs.Ticket)Container.DataItem).Cancelled ? Utilities.IconHtml(Utilities.Icon.Cross, "Cancelled", "") : ""%>
					</ItemTemplate>
					<HeaderStyle HorizontalAlign="Left" />
				</asp:TemplateField>
			</Columns>
		</asp:GridView>
		<asp:Label ID="SearchResultsMessageLabel" runat="server" Font-Italic="True" Visible="false"></asp:Label>
	</div>
</asp:Panel>
