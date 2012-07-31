<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MultiBuddyChooser.ascx.cs"	Inherits="Spotted.Controls.MultiBuddyChooser" EnableViewState="false" %>
<p>
	<js:MultiSelector ID="uiBuddyMultiSelector" runat="server" 
		WebServiceUrl="/WebServices/AutoComplete.asmx"
		WebServiceMethod="GetBuddies"
		Watermark="Type nicknames or email addresses here" height="120px"/>
</p>
<p>
	When I type in the box above, search:
</p>
<p style="margin-left:20px;">
	<asp:RadioButton ID="uiJustBuddiesRadio" runat="server" Checked="true" GroupName="WhichUsers"/><label style="cursor:default;" for="<%= uiJustBuddiesRadio.ClientID %>">Just my buddies</label>
</p>
<p style="margin-left:20px;">
	<asp:RadioButton ID="uiAllMembersRadio" runat="server" GroupName="WhichUsers"/><label style="cursor:default;" for="<%= uiAllMembersRadio.ClientID %>">All members (might be slower)</label>
</p>
<p>
	Advanced options:
</p>
<p style="margin-left:20px;">
	<asp:CheckBox ID="uiShowBuddyList" runat="server"/><label style="cursor:default;" for="<%= uiShowBuddyList.ClientID %>">Pick my buddies from a list...</label>
</p>
<p>
	<asp:Panel id="uiBuddyListPanel" runat="server" style="display:none;margin-left:40px;">
		<asp:ListBox ID="uiBuddyList" runat="server" Width="250px" Rows="10"></asp:ListBox>
	</asp:Panel>
</p>
<p style="margin-left:20px;">
	<asp:CheckBox ID="uiShowAddAll" runat="server"/><label style="cursor:default;" for="<%= uiShowAddAll.ClientID %>">Add all my buddies...</label>
</p>
<div style="margin-left:40px; display: none;" id="uiAddAll" runat="server">
	<p>
		<button runat="server" id="uiAddAllButton" causesvalidation="False">Add all my buddies!</button>
	</p>
</div>
<p style="margin-left:20px;">
	<asp:CheckBox ID="uiShowAddBy" runat="server" /><label style="cursor:default;" for="<%= uiShowAddBy.ClientID %>">Add buddies by town / music...</label>
</p>
<div id="uiAddBy" runat="server" style="display:none;">
	<p style="margin-left:40px;">
		...who visit <asp:DropDownList ID="uiPlaces" runat="server" style="width:150px;" />, and listen to <asp:DropDownList ID="uiMusicTypes" runat="server" style="width:150px;" />
	</p>
	<p style="margin-left:40px;">
		<button runat="server" id="uiAddByMusicAndPlace" causesvalidation="False" >Add buddies!</button> <asp:CheckBox ID="uiShowAllTownsAndMusic" runat="server" Text="show all towns / music types in the drop-downs" />
	</p>
</div>
