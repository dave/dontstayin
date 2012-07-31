<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MultiUsrDrop.ascx.cs" Inherits="Spotted.Controls.MultiUsrDrop" %>
<%@ Register TagPrefix="DbCombo" Namespace="Cambro.Web.DbCombo" Assembly="Cambro.Web.DbCombo" %>
<div class="MultiUsrDiv" style="border:solid 1px #A58319;background-color:#FED551;width:<%= (Width+123).ToString() %>;padding-top:10px;padding-left:10px;padding-right:7px;padding-bottom:10px;">
<table cellpadding="0" cellspacing="0" border="0" runat="server" id="BuddiesTable">
	<tr runat="server" ID="Tr1">
		<td valign="top" runat="server" ID="Td1">
			<asp:DropDownList Runat="server" ID="Drop" NAME="Drop" EnableViewState="False"/><br>
			<div runat="server" id="ClipSpan" style=";">
				<select runat="server" id="SelectBox" NAME="SelectBox" multiple/>
			</div>
		</td>
		<td valign="top" style="position:relative;left:-2px;" rowspan="2" runat="server" ID="Td2">
			<button tabindex="-1" runat="server" id="AddButton" style="margin-bottom:-1px;border:solid 1px #999999;font-weight:bold;width:42px;height:18px;">Add</button>
			<button tabindex="-1" runat="server" id="RemoveButton">Remove</button><br>
			<span runat="server" id="PicHolder" style="border-bottom:1px solid #999999;"><img src="/gfx/dsi-sign-100.png" width="100" height="100" style="border-top:1px solid #999999;border-left:1px solid #999999;border-right:1px solid #999999;"></span><br>
			<button tabindex="-1" runat="server" id="MoreButton" CausesValidation="False" style="margin-top:-1px;border:solid 1px #999999;font-weight:bold;width:102px;height:18px;" onserverclick="More_Click">More...</button>
		</td>
	</tr>
</table>
<asp:Panel runat="server" id="BuddiesRemovedPanel">
	<p>
		<small>
			* <asp:Label Runat="server" id="BuddiesRemovedLabel"></asp:Label> from this selector 
			because they've already seen or been invited to this topic. (You can't invite someone 
			to a topic that they've already seen / been invited to).
		</small>
	</p>
</asp:Panel>
<asp:Panel runat="server" id="BuddiesRemovedGroupPanel">
	<p>
		<small>
			* <asp:Label Runat="server" id="BuddiesRemovedGroupLabel"></asp:Label> from this selector 
			because they are already members, or they've already been invited.
		</small>
	</p>
</asp:Panel>
<asp:Panel runat="server" id="NoBuddiesPanel" EnableViewState="False">
	<h2>No buddies...</h2>
	<p>
		You don't have any buddies yet. You can invite someone to your buddy 
		list by ticking the box on their profile page, or enter their emailin 
		the box below:
	</p>
</asp:Panel>
<asp:Panel runat="server" id="NoBuddiesThreadPanel" EnableViewState="False">
	<h2>No buddies to select...</h2>
	<p>
		You can't invite any of your buddies to this topic, because 
		they've all already seen or been invited to it. (You can't 
		invite someone to a topic that they've already seen / been 
		invited to).
		You can use the box below to invite new buddies by email.
	</p>
</asp:Panel>

<asp:Panel runat="server" id="AddAllPanel" visible="false">
	<h2 style="margin-top:15px;">Add all my buddies...</h2>
	<p>
		that visit <asp:DropDownList Runat="server" ID="AddAllPlaceDrop"/>
	</p>
	<p>
		and listen to <asp:DropDownList Runat="server" ID="AddAllMusicDrop"/>
	</p>
	<p>
		<button runat="server" id="Button1" CausesValidation="False" style="border:solid 1px #999999;font-weight:bold;width:102px;height:18px;" onserverclick="AddAllNow_Click">Add now</button>
		<small><asp:CheckBox Runat="server" CausesValidation="False" ID="AddAllShowAllItemsCheck" Text="Show all music types / towns" AutoPostBack="True" OnCheckedChanged="AddAllShowAllItemsCheck_Change"/></small>
	</p>
</asp:Panel>
<asp:PlaceHolder Runat="server" ID="AddAllMessagePlaceHolder" EnableViewState="False"/>

<asp:Panel runat="server" id="AddMorePanel" visible="false">
	<h2 style="margin-top:15px;">Add by email address...</h2>
	<p runat="server" id="AddMoreP">
		Even if your mates aren't signed up to DontStayIn yet, you can enter their email addresses below:
	</p>
	<p>
		<asp:TextBox Runat="server" ID="AddMoreTextBox" TextMode="MultiLine" Rows="5"/>
	</p>
	<p runat="server" id="AddMoreP1">
		Enter one email per line - we'll send them a quick invitation, and add them to your buddy list.
	</p>
	<p>
		<button runat="server" id="AddMoreButton" CausesValidation="False" 
			style="border:solid 1px #999999;font-weight:bold;width:102px;height:18px;" 
			onserverclick="AddMore_Click">Add now</button> 
			<small><asp:CheckBox Runat="server" ID="AddMoreAddBuddyCheckBox" Checked="True" Text="Add buddies"/></small>
	</p>
</asp:Panel>
<asp:PlaceHolder Runat="server" ID="AddMoreMessagePlaceHolder" EnableViewState="False"/>

<input runat="server" type="hidden" id="Values" value=""/>
<input runat="server" type="hidden" id="Texts" value=""/>
<div id="debugHolder"></div>
</div>
