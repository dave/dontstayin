<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TicketConfirmation.ascx.cs" Inherits="Spotted.Styled.TicketConfirmation" %>
<h2>Ticket confirmation</h2>
<hr />
<div class="InnerDiv">
	<p>
		<asp:Label ID="TicketConfirmationLabel" runat="server"></asp:Label>
	</p>
	<p>
		<asp:Label ID="EventLinkLabel" runat="server"></asp:Label>
	</p>
	<p>View all your tickets by clicking <%=Utilities.Link(this.StyledObject.UrlStyledApp("mytickets"), "my tickets", "class=\"Link\"") %></p>
</div>
