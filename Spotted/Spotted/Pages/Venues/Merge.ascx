<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Merge.ascx.cs" Inherits="Spotted.Pages.Venues.Merge" %>
<%@ Register TagPrefix="DbCombo" Namespace="Cambro.Web.DbCombo" Assembly="Cambro.Web.DbCombo" %>
<%@ Register src="/Controls/Picker.ascx" tagname="Picker" tagprefix="dsi" %>
<asp:Panel Runat="server" ID="PanelMerge">
	<dsi:h1 runat="server" ID="H11" NAME="H18">Merge venues...</dsi:h1>
	<div class="ContentBorder">
		<p>
			To merge two duplicate venues, select them below.
		</p>
		<center>
		<table cellspacing="0" cellpadding="5">
			<tr>
				<td><h2>Master venue</h2></td>
				<td>
				</td>
				<td><h2>Venue to delete</h2></td>
			</tr>
			<tr>
				<td>
					<p>
						<dsi:Picker ID="uiMasterVenuePicker" runat="server" OptionEvent="false" OptionDate="false" OptionBrand="false" ValidationType="venue" />
					</p>
				</td>
				<td></td>
				<td>
					<p>
						<dsi:Picker ID="uiMergeVenuePicker" runat="server" OptionEvent="false" OptionDate="false" OptionBrand="false" ValidationType="venue" />
					</p>
				</td>
			</tr>
			<tr>
				<td>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Name<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Short details<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Long details<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Picture*<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Capacity
				</td>
				<td></td>
				<td>
					<img src="/gfx/icon-cross-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Name<br>
					<img src="/gfx/icon-cross-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Short details<br>
					<img src="/gfx/icon-cross-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Long details<br>
					<img src="/gfx/icon-cross-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Picture*<br>
					<img src="/gfx/icon-cross-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Capacity
				</td>
			</tr>
			<tr>
				<td style="border-left:1px solid #A58319;border-top:1px solid #A58319;border-bottom:1px solid #A58319;">
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Articles<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Events<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Chat topics
				</td>
				<td style="border-top:1px solid #A58319;border-bottom:1px solid #A58319;" align="center">
					<img src="/gfx/icon-back-12.png" style="margin-right:3px;" width="12" height="21" align="absmiddle" border="0"><b>Merged</b>
				</td>
				<td style="border-right:1px solid #A58319;border-top:1px solid #A58319;border-bottom:1px solid #A58319;">
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Articles<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Events<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Chat topics
				</td>
			</tr>
			<tr>
				<td></td>
				<td align="center">
					<p>
						<asp:Button Runat="server" ID="MergeButton" OnClick="Merge_Click" Text="Merge now"></asp:Button>
					</p>
				</td>
				<td></td>
			</tr>
		</table>
		</center>
		<p>
			<small>* Picture is copied from the venue to delete if the master venue doesn't have a picture.</small>
		</p>
	</div>
</asp:Panel>
