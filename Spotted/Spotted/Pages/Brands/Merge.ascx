<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Merge.ascx.cs" Inherits="Spotted.Pages.Brands.Merge" %>

<%@ Register TagPrefix="DbCombo" Namespace="Cambro.Web.DbCombo" Assembly="Cambro.Web.DbCombo" %>
<asp:Panel Runat="server" ID="PanelMerge">
	<dsi:h1 runat="server" ID="Header" NAME="H18">Merge party brands...</dsi:h1>
	<div class="ContentBorder">
		<p>
			<img src="/gfx/icon-group.png" border="0" align="absmiddle" style="margin-right:3px;">To 
			merge two duplicate party brands, select them below. The text in the box is the unique 
			url name of the brand. Take special care when merging two brands with similar names - 
			make a note of the url of the brand homepage.
		</p>
		<center>
		<table cellspacing="0" cellpadding="5">
			<tr>
				<td><h2>Master brand</h2></td>
				<td>
				</td>
				<td><h2>Brand to delete</h2></td>
			</tr>
			<tr>
				<td>
					<js:HtmlAutoComplete ID="uiMasterBrandUrl" runat="server" WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetBrands" showK="true" Width="300px"/>
				</td>
				<td></td>
				<td>
					<js:HtmlAutoComplete ID="uiMergeBrandUrl" runat="server" WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetBrands" showK="true" Width="300px"/>
				</td>
			</tr>
			<tr>
				<td>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Name<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Group description<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Group posting rules<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Picture<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Music type<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Location
				</td>
				<td></td>
				<td>
					<img src="/gfx/icon-cross-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Name<br>
					<img src="/gfx/icon-cross-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Group description<br>
					<img src="/gfx/icon-cross-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Group posting rules<br>
					<img src="/gfx/icon-cross-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Picture<br>
					<img src="/gfx/icon-cross-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Music type<br>
					<img src="/gfx/icon-cross-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Location
				</td>
			</tr>
			<tr>
				<td style="border-left:1px solid #A58319;border-top:1px solid #A58319;border-bottom:1px solid #A58319;">
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Events<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Group members<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Group moderators<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Chat posts<br>
				</td>
				<td style="border-top:1px solid #A58319;border-bottom:1px solid #A58319;" align="center">
					<img src="/gfx/icon-back-12.png" style="margin-right:3px;" width="12" height="21" align="absmiddle" border="0"><b>Merged</b>
				</td>
				<td style="border-right:1px solid #A58319;border-top:1px solid #A58319;border-bottom:1px solid #A58319;">
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Events<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Group members<br>
					<img src="/gfx/icon-tick-up.png" border="0" height="21" width="26" align="absmiddle" style="margin-right:3px;">Group moderators<br>
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
	</div>
</asp:Panel>
