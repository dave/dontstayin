<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Delete.ascx.cs" Inherits="Spotted.Pages.Events.Delete" %>
<asp:Panel ID="PanelDelete" runat="server">
	<dsi:h1 id="H10" runat="server">Delete an event</dsi:h1>
	<div class="ContentBorder">
		<p>
			Delete this event:
		</p>
		<p id="EventDescriptionP" runat="server">
		</p>
		<p>
			Confirm your password:
		</p>
		<p>
			<asp:TextBox ID="Password" runat="server" TextMode="Password"/>
		</p>
		<p style="border:1px solid #ff0000; padding:5px; margin:5px;">
			<b>Before deleting duplicate events, always consider merging them together.</b>
		</p>
		<p>
			Delete now:
		</p>
		<p>
			<asp:Button runat="server" OnClick="DeleteNow" Text="Delete now" />
		</p>
	</div>
</asp:Panel>
<asp:Panel ID="PanelError" runat="server">
	<dsi:h1 id="H11" runat="server">Delete event failed!</dsi:h1>
	<div class="ContentBorder">
		<p id="DeleteFailedP" runat="server"/>
		<p>
			<a href="" runat="server" id="ErrorBackAnchor">Back</a>
		</p>
	</div>
</asp:Panel>
<asp:Panel ID="PanelDone" runat="server">
	<dsi:h1 id="H12" runat="server">Event deleted</dsi:h1>
	<div class="ContentBorder">
		<p>
			Event deleted.
		</p>
	</div>
</asp:Panel>
