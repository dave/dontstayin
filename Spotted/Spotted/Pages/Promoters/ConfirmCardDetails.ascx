<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConfirmCardDetails.ascx.cs" Inherits="Spotted.Pages.Promoters.ConfirmCardDetails" %>
<%@ Register TagPrefix="DsiControl" TagName="Doorlist" Src="/Controls/Doorlist.ascx" %>

<dsi:PromoterIntro runat="server" ID="PromoterIntro" Header="Confirm card details">
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

<dsi:h1 runat="server">Credit Card CV2 Check</dsi:h1>
<div class="ContentBorder">
	<asp:Panel runat="server" ID="uiSelect">
		<p>Please select an event:</p>
		<asp:DropDownList runat="server" ID="uiEvents" OnSelectedIndexChanged="LoadTickets" AutoPostBack="true"></asp:DropDownList>
	</asp:Panel>
	<asp:Panel runat="server" ID="uiNoEvents">
		<p>There are currently no events to check.</p>
	</asp:Panel>
	<br />
	
	<asp:Panel runat="server" ID="uiDoorlistPanel" Visible="false">
		<DsiControl:Doorlist runat="server" ID="uiDoorlist"></DsiControl:Doorlist>
		<button runat="server" id="uiSave" onserverclick="Save">Save</button>
		<asp:Label runat="server" ID="uiSomeWrongLabel" Visible="false" ForeColor="Red" Text="Some CV2s didn't match, please check the list"></asp:Label>
	</asp:Panel>
</div>
