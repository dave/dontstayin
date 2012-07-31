<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BuddiesDropDown.ascx.cs" Inherits="Spotted.Controls.BuddiesDropDown" %>
<!--%@ OutputCache Duration="1200" VaryByCustom="Usr;LastBuddyChange" VaryByParam="None" %-->
<dsi:h1 runat="server" ID="H12" NAME="H12">Is&nbsp;this&nbsp;one&nbsp;of&nbsp;your&nbsp;buddies?</dsi:h1>
<div class="ContentBorder">
	<p>
		<nobr>
			Yes, it's:
			<asp:DropDownList Runat="server" ID="UsrPhotoBuddyDropDown"/>&nbsp;
			<button onclick="__doPostBack('BuddySpotted', 'BuddySpotted');">Save</button>
		</nobr>
	</p>
</div>
