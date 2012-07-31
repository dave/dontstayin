<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Home.ascx.cs" Inherits="Spotted.Styled.Home" %>

<h2>Events</h2>
	<hr />
	<div class="InnerDiv">
		<p class="Link">
			<asp:Repeater ID="EventLinkRepeater" runat="server">
				<ItemTemplate>
					<%# StyledObject.UrlStyledEventLink((Event)Container.DataItem) %>					
				</ItemTemplate>
			</asp:Repeater>
			<asp:Label id="NoEventsLabel" runat="server" Text="No upcoming events." Visible="false"></asp:Label>
		</p>
</div>

