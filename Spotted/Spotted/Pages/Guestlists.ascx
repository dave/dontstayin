<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Guestlists.ascx.cs" Inherits="Spotted.Pages.Guestlists" %>

<dsi:h1 runat="server" ID="H17">You're already on a guestlist</dsi:h1>
<div class="ContentBorder">
	<p>
		You're already on a guestlist for <%= CurrentEvent.FriendlyDate(false) %>. You can 
		only be on one guestlist per day. Use the buttons below to select which event you'd 
		like to go to:
	</p>
	<p>
		<table cellspacing="10">
			<tr>
				<td valign="top">
					<asp:Button Runat="server" OnClick="CurrentEvent_Click" Text="This one" ID="Button1" NAME="Button1"></asp:Button>
				</td>
				<td valign="top">
					<asp:DataList Runat="server" ID="CurrentEventDataList" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="1" Width="100%" CellPadding="0" CellSpacing="0" ItemStyle-VerticalAlign="Top" />
					
				</td>
				
			</tr>
			<tr>
				<td valign="top">
					<asp:Button Runat="server" OnClick="OtherEvent_Click" Text="This one" ID="Button2" NAME="Button1"></asp:Button>
				</td>
				<td valign="top">
					<asp:DataList Runat="server" ID="OtherEventDataList" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="1" Width="100%" CellPadding="0" CellSpacing="0" ItemStyle-VerticalAlign="Top" />
				</td>
			</tr>
		</table>
	</p>
	
</div>
