<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Merge.ascx.cs" Inherits="Spotted.Pages.Events.Merge" %>
<%@ Register TagPrefix="DbCombo" Namespace="Cambro.Web.DbCombo" Assembly="Cambro.Web.DbCombo" %>
<%@ Register src="~/Controls/Picker.ascx" tagname="Picker" tagprefix="uc1" %>
<asp:Panel Runat="server" ID="PanelMerge">
	<dsi:h1 runat="server" ID="H11" NAME="H18">Merge events...</dsi:h1>
	<div class="ContentBorder">
		<p>
			To merge two duplicate events, select them below. The first number in the 
			selector is the unique key for the event. Check you&#39;re merging the right events 
			by comparing this number with the last number in the URL of the event page.
		</p>
		<center>
		<table cellspacing="0" cellpadding="5">
			<tr>
				<td style="text-align:center" style="width:400px;"><h2>Master event</h2></td>
				<td></td>
				<td style="text-align:center" style="width:400px;"><h2>Event to delete</h2></td>
			</tr>
			<tr>
				<td valign="top" style="border:1px solid black;" style="width:400px;">
					<p style="width:400px;">
						<uc1:Picker ID="uiMainEventFinder" runat="server" OptionKey="true" />
					</p>
				</td>
				<td></td>
				<td valign="top" style="border:1px solid black;" style="width:400px;">
					<p style="width:400px;">
						<uc1:Picker ID="uiMergeEventFinder" runat="server" OptionKey="true" />
					</p>
				</td>
			</tr>
			
			<tr>
				<td>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Name<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Short details<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Long details<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Picture*<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Date<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Venue<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Capacity
				</td>
				<td></td>
				<td>
					<img src="/gfx/icon-cross-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Name<br>
					<img src="/gfx/icon-cross-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Short details<br>
					<img src="/gfx/icon-cross-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Long details<br>
					<img src="/gfx/icon-cross-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Picture*<br>
					<img src="/gfx/icon-cross-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Date<br>
					<img src="/gfx/icon-cross-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Venue<br>
					<img src="/gfx/icon-cross-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Capacity
				</td>
			</tr>
			<tr>
				<td style="border-left:1px solid #A58319;border-top:1px solid #A58319;border-bottom:1px solid #A58319;">
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Articles<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Banners<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Competitions<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Music types<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Brands<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Galleries<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Photos<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Chat topics<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Users attending<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Spotters<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Guestlist**<br>
				</td>
				<td style="border-top:1px solid #A58319;border-bottom:1px solid #A58319;" align="center">
					<img src="/gfx/icon-back-12.png" style="margin-right:3px;" width="12" height="21" align="absmiddle" border="0"><b>Merged</b>
				</td>
				<td style="border-right:1px solid #A58319;border-top:1px solid #A58319;border-bottom:1px solid #A58319;">
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Articles<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Banners<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Competitions<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Music types<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Brands<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Galleries<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Photos<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Chat topics<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Users attending<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Spotters<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Guestlist**<br>
				</td>
			</tr>
			<tr>
				<td ></td>
				<td align="center">
					<p>
						<asp:Button Runat="server" ID="MergeButton" OnClick="Merge_Click" Text="Merge now"></asp:Button>
					</p>
				</td>
				<td ></td>
			</tr>
		</table>
		</center>
		<p>
			<small>* Picture is copied from the event to delete if the master event doesn't have a photo.</small>
		</p>
		<p>
			<small>** If the event to delete has a guestlist, it will be copied over to the master event.</small>
		</p>
	</div>
</asp:Panel>
