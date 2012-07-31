<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TicketConfirmation.ascx.cs" Inherits="Spotted.Pages.Events.TicketConfirmation" %>
<asp:Panel ID="EventTicketConfirmationPanel" runat="server">
	<dsi:h1 runat="Server" id="TicketsHeading"><%= CurrentEvent.FriendlyName %></dsi:h1>
	<div class="ContentBorder">	
		<p>
			<h2><asp:Label ID="TicketConfirmationLabel" runat="server"></asp:Label></h2>
		</p>
		<p class="BigCenter">
			<%= CurrentEvent.LinkFriendlyName%>
		</p>
		<br />
		<br />
		<p><h2><center>View all your DSI tickets by clicking <%=Utilities.Link(Usr.Current.UrlApp("mytickets"), "My tickets") %></center></h2></p>
	</div>
</asp:Panel>
