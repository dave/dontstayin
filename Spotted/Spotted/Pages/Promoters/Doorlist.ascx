<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Doorlist.ascx.cs" Inherits="Spotted.Pages.Promoters.Doorlist" %>
<dsi:PromoterIntro runat="server" ID="PromoterIntro" Header="Print doorlist">
	<asp:Panel Runat="server">
		<p>
			<a href="<%= CurrentPromoter.UrlApp("ticketrun") %>"><img src="/gfx/icon-add.png" width="26" height="21" border="0" 
				align="absmiddle" style="margin-right:3px;">sell tickets now</a>
		</p>
		<p>
			<a href="<%= CurrentPromoter.UrlApp("allticketruns") %>"><img src="/gfx/icon-view.png" width="26" height="21" border="0" 
				align="absmiddle" style="margin-right:3px;">view all ticket runs</a>
		</p>
	</asp:Panel>
</dsi:PromoterIntro>
<asp:Panel Runat="server" ID="DoorlistPanel">
	<dsi:h1 runat="server" ID="H1Title">Doorlist selection</dsi:h1>
	<div class="ContentBorder">
		<p id="HasTicketsP" runat="server">
			Event: <asp:DropDownList ID="EventDropDownList" runat="server"></asp:DropDownList> <asp:Button ID="DoorlistButton" runat="server" Text="Doorlist" OnClick="DoorlistButton_Click" />
		</p>
		<p id="NoTicketsP" runat="server">
			You do not have any recent ticket runs that have sold any tickets.<br />Click <a href="<%= CurrentPromoter.UrlApp("allticketruns") %>">view all ticket runs</a> to view details on any past ticket runs.
		</p>
	</div>
</asp:Panel>

