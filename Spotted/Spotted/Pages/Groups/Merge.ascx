<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Merge.ascx.cs" Inherits="Spotted.Pages.Groups.Merge" %>

<%@ Register TagPrefix="DbCombo" Namespace="Cambro.Web.DbCombo" Assembly="Cambro.Web.DbCombo" %>
<asp:Panel Runat="server" ID="PanelMerge">
	<dsi:h1 runat="server" ID="Header" NAME="H18">Merge groups...</dsi:h1>
	<div class="ContentBorder">
		<p>
			<img src="/gfx/icon-group.png" border="0" align="absmiddle" style="margin-right:3px;">To 
			merge two similar groups, select them below. The text in the box is the unique url name of the group. 
			Take special care when merging two groups with similar names - make a note of the url of the group 
			homepage.http://localhost/pages/groups/merge
		</p>
		<center>
		<table cellspacing="0" cellpadding="5">
			<tr>
				<td><h2>Master group</h2></td>
				<td>
				</td>
				<td><h2>Group to delete*</h2></td>
			</tr>
			<tr>
				<td>
					<js:HtmlAutoComplete runat="server" ID="uiMasterGroupAutoComplete" WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetGroups" Width="300px" Watermark="Party brands and groups"/>
				</td>
				<td></td>
				<td>
					<js:HtmlAutoComplete runat="server" ID="uiMergeGroupAutoComplete" WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetGroupsNoBrands"  Width="300px" Watermark="Groups"/>
				</td>
			</tr>
			<tr>
				<td>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Name<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Description<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Posting rules<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Picture<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Theme<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Music type<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Location
				</td>
				<td></td>
				<td>
					<img src="/gfx/icon-cross-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Name<br>
					<img src="/gfx/icon-cross-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Description<br>
					<img src="/gfx/icon-cross-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Posting rules<br>
					<img src="/gfx/icon-cross-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Picture<br>
					<img src="/gfx/icon-cross-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Theme<br>
					<img src="/gfx/icon-cross-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Music type<br>
					<img src="/gfx/icon-cross-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Location
				</td>
			</tr>
			<tr>
				<td style="border-left:1px solid #A58319;border-top:1px solid #A58319;border-bottom:1px solid #A58319;">
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Members<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Moderators<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Recommended events<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Chat posts<br>
				</td>
				<td style="border-top:1px solid #A58319;border-bottom:1px solid #A58319;" align="center">
					<img src="/gfx/icon-back-12.png" style="margin-right:3px;" width="12" height="21" align="absmiddle" border="0"><b>Merged</b>
				</td>
				<td style="border-right:1px solid #A58319;border-top:1px solid #A58319;border-bottom:1px solid #A58319;">
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Members<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Moderators<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Recommended events<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Chat posts<br>
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
			<small>
				* Note we can't delete a party brand group. We can however merge into a party brand group.
			</small>
		</p>
	</div>
</asp:Panel>
