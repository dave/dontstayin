<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyBuddies.ascx.cs" Inherits="Spotted.Pages.MyBuddies" %>

<asp:Panel Runat="server" ID="PanelNoBuddies">
	<dsi:h1 runat="server" ID="H12" NAME="H12">Your buddies</dsi:h1>
	<div class="ContentBorder">
		<p>
			You don't have anyone on your buddy list. To invite someone, first 
			find their profile, and click "Add to my buddy list".
		</p>
	</div>
</asp:Panel>
<asp:Panel Runat="server" ID="PanelBuddies">
	<asp:Panel runat="server" id="FullBuddyListPanel" EnableViewState="False">
		<dsi:h1 runat="server" ID="H11" NAME="H12">Your buddies</dsi:h1>
		<div class="ContentBorder">
			<p>
				These are your buddies:
			</p>
			<p>
				<asp:DataList Runat="server" ID="FullBuddyList" RepeatColumns="2" RepeatLayout="Table" Width="100%" ItemStyle-VerticalAlign="Top"></asp:DataList>
			</p>
		</div>
	</asp:Panel>
	
	<asp:Panel runat="server" id="ReverseHalfBuddyListPanel" EnableViewState="False">
		<dsi:h1 runat="server" ID="H14" NAME="H12">Buddy invites sent to you</dsi:h1>
		<div class="ContentBorder">
			<p>
				These people have invited you to their buddy lists. You need 
				to add them to your list to accept the invite:
			</p>
			<p>
				<asp:DataList Runat="server" ID="ReverseHalfBuddyList" RepeatColumns="2" RepeatLayout="Table" Width="100%" ItemStyle-VerticalAlign="Top"></asp:DataList>
			</p>
		</div>
	</asp:Panel>
	
	<asp:Panel runat="server" id="HalfBuddyListPanel" EnableViewState="False">
		<dsi:h1 runat="server" ID="H13" NAME="H12">Buddy invites you've sent</dsi:h1>
		<div class="ContentBorder">
			<p>
				You've invited these people to your buddy list, but they haven't 
				accepted yet:
			</p>
			<p>
				<asp:DataList Runat="server" ID="HalfBuddyList" RepeatColumns="2" RepeatLayout="Table" Width="100%" ItemStyle-VerticalAlign="Top"></asp:DataList>
			</p>
		</div>
	</asp:Panel>
	
	
	
</asp:Panel>
