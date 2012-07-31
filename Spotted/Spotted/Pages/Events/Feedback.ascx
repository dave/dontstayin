<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Feedback.ascx.cs" Inherits="Spotted.Pages.Events.Feedback" %>
<asp:Panel ID="EventTicketFeedbackPanel" runat="server">
	<dsi:h1 runat="Server" id="TicketsHeading"><%= CurrentEvent.FriendlyName %></dsi:h1>
	<div class="ContentBorder">	
		<p><asp:DataList Runat="server" ID="TicketEventDataList" RepeatDirection="Vertical" RepeatLayout="Table" RepeatColumns="1" Width="100%" CellPadding="0" CellSpacing="0" ItemStyle-VerticalAlign="Top" ItemStyle-Width="100%" /></p>
		<p id="TicketFeedbackP" runat="server">
			<h2>Ticket feedback</h2>
			<asp:Label ID="UsrTicketFeedbackHeaderLabel" runat="server" Text="In order to provide a better service in future, we would like to know if everything went OK with getting into the event..."></asp:Label>
			<div id="UsrTicketResponseGoodLinkButtonDiv" runat="server" style='font-size:12px; font-weight:bold;'><asp:LinkButton ID="UsrTicketResponseGoodLinkButton" runat="server" OnClick="UsrTicketResponseGoodLinkButton_Click" CausesValidation="false"><img src='http://www.dontstayin.com/gfx/icon-tick-up.png' border="0" height="21" width="26" style="vertical-align:middle;"/>Yes, all OK</asp:LinkButton><br /></div>
			<div id="UsrTicketResponseGoodDiv" runat="server" style='font-size:12px; font-weight:bold;'><img src='http://www.dontstayin.com/gfx/icon-tick-up.png' border="0" height="21" width="26" style="vertical-align:middle;"/>Yes, all OK</div>
			<div id="UsrTicketResponseBadLinkButtonDiv" runat="server" style='font-size:12px; font-weight:bold;'><asp:LinkButton ID="UsrTicketResponseBadLinkButton" runat="server" OnClick="UsrTicketResponseBadLinkButton_Click" CausesValidation="false"><img src='http://www.dontstayin.com/gfx/icon-cross-up.png' border="0" height="21" width="26" style="vertical-align:middle;"/>No, there was a problem</asp:LinkButton><br /></div>
			<div id="UsrTicketResponseBadDiv" runat="server" style='font-size:12px; font-weight:bold;'><img src='http://www.dontstayin.com/gfx/icon-cross-up.png' border="0" height="21" width="26" style="vertical-align:middle;"/>No, there was a problem</div>
			<div id="UsrTicketFeedbackTextDiv" runat="server" visible="false" style="padding-left:3px;"><p>We are sorry you had trouble using this ticket. Please explain the problems you, so we can make sure it does not happen in the future:</p><p><asp:TextBox ID="UsrTicketFeedbackTextBox" runat="server" Width="500" TextMode="MultiLine"></asp:TextBox>
				<asp:Label ID="UsrTicketFeedbackLabel" runat="server"></asp:Label><button id="UsrTicketFeedbackTextSubmitButton" runat="server" onserverclick="UsrTicketFeedbackTextSubmitButton_Click" causesvalidation="false">Submit</button></p></div>
				
		</p>
	</div>
	<asp:Panel ID="SuccessfulTicketEventPanel" runat="server" Visible=false>
		<div id="JoinBrandRegularsGroup" runat="server" class="ContentBorder">
			<p>
				Did you enjoy this party? Want to find out more about future parties?
			</p>
		</div>
		
		<asp:Repeater ID="BrandGroupRepeater" runat="server" OnItemDataBound="BrandGroupRepeater_ItemDataBound">
			<ItemTemplate>
				<asp:PlaceHolder ID="BrandGroupJoinPlaceHolder" runat="server"></asp:PlaceHolder>
			</ItemTemplate>
		</asp:Repeater>
	</asp:Panel>
</asp:Panel>
